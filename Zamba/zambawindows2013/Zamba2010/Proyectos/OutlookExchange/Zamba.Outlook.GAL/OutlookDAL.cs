using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Servers;
using System.Diagnostics;
using Zamba.Core;
using System.Data;
using System.Configuration;

namespace Zamba.Outlook.GAL
{
    public class OutlookDAL
    {
        private Zamba.Data.Transaction _trans = null;
        private int _id;
        private bool _log_query = false;

        public int LastIdInserted 
        {
            get { return _id; }
        }

        public bool LogQuery
        {
            set { _log_query = value; }
        }

        public OutlookDAL(int lastId) 
        {
            _id = lastId;
        }

        public OutlookDAL()
        {
        }

        public void Begin()
        {
            _trans = new Zamba.Data.Transaction();            
        }

        public void Commit()
        {
            _trans.Commit();
        }

        public void Rollback()
        {
            _trans.Rollback();
        }

        // borra los contactos de la tabla real segun la libreta especificada
        public void deleteContacts(string libreta)
        {
            string SQL = "DELETE FROM GAL_CONTACTOS WHERE ADDRESSBOOK = '" + libreta + "'";

            if (_trans == null)
                Server.get_Con(false, true, false).ExecuteNonQuery(System.Data.CommandType.Text, SQL);
            else
                _trans.Con.ExecuteNonQuery(_trans.Transaction, System.Data.CommandType.Text, SQL);
        }

        // arma una lista con los contactos pendientes de resolver desde 
        // la tabla temporal de trabajo
        public List<OutlookContact> CargarContactosWork(string libreta)
        {
            int cant = int.Parse(ConfigurationManager.AppSettings["cant_items_ciclo_work"].ToString());

            string SQL = "";
            
            SQL = "SELECT * FROM GAL_CONTACTOS_WORK WHERE EMAIL IS NULL AND LOWER(ADDRESSBOOK) = '" + libreta.ToLower() + "'";
            
            if (cant>0)
                SQL = SQL + " AND ROWNUM <= " + cant.ToString();

            DataSet ds = Server.get_Con(false, true, false).ExecuteDataset(System.Data.CommandType.Text, SQL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                List<OutlookContact> lista = new List<OutlookContact>();
                OutlookContact contacto = new OutlookContact();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["Tipo"].ToString().ToUpper() == "SINGLE")
                        contacto = new OutlookContact(row["FullName"].ToString(), row["EX"].ToString(), OutlookContact.olContactTypes.SINGLE, OutlookContact.olAddressTypes.EXCHANGE);
                    else
                        contacto = new OutlookContact(row["FullName"].ToString(), row["EX"].ToString(), OutlookContact.olContactTypes.DISTRIBUTION_LIST , OutlookContact.olAddressTypes.EXCHANGE);

                    if (row["Email"].ToString() != string.Empty)
                    {
                        contacto.AddressType = OutlookContact.olAddressTypes.SMTP;
                        contacto.Resolved = true;
                    }

                    lista.Add(contacto);
                }

                return lista;
            }

            return null;
        }

        public void MoveContactos(string libreta)
        {
            string SQL;
            StringBuilder sb;

            try
            {
                Begin();

                SQL = "SELECT * FROM GAL_CONTACTOS_WORK WHERE LOWER(ADDRESSBOOK) = '" + libreta.ToLower() + "' AND EMAIL <> '-' ";

                DataSet ds = _trans.Con.ExecuteDataset(_trans.Transaction, System.Data.CommandType.Text, SQL);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    sb = new StringBuilder();

                    // borrarlo de la tabla real
                    sb.Append(" DELETE FROM GAL_CONTACTOS WHERE ");
                    sb.Append(" LOWER(EMAIL) = '" + row["EMAIL"].ToString().Replace("'", "''").ToLower() + "'");
                    sb.Append(" AND ADDRESSBOOK = '" + libreta + "'");

                    _trans.Con.ExecuteNonQuery(_trans.Transaction, CommandType.Text, sb.ToString());

                    sb = new StringBuilder();

                    // alta en la tabla real
                    sb.Append(" INSERT INTO GAL_CONTACTOS ");
                    sb.Append(" (FULLNAME, EMAIL, TIPO, ADDRESSBOOK) VALUES (");
                    sb.Append("'");
                    sb.Append(row["FullName"].ToString().Replace("'", "''"));
                    sb.Append("', '");
                    sb.Append(row["Email"].ToString().Replace("'", "''"));
                    sb.Append("', '");
                    sb.Append(row["Tipo"].ToString());
                    sb.Append("', '");
                    sb.Append(libreta);
                    sb.Append("')");

                    _trans.Con.ExecuteNonQuery(_trans.Transaction, CommandType.Text, sb.ToString());

                    sb = new StringBuilder();

                    // borrarlo de los pendientes
                    sb.Append(" DELETE FROM GAL_CONTACTOS_WORK WHERE ");
                    sb.Append(" LOWER(EMAIL) = '" + row["Email"].ToString().Replace("'", "''").ToLower() + "'");
                    sb.Append(" AND ADDRESSBOOK = '" + libreta + "'");

                    _trans.Con.ExecuteNonQuery(_trans.Transaction, CommandType.Text, sb.ToString());
                }

                Commit();
            }
            catch (Exception ex)
            {
                Rollback();
                ZClass.raiseerror(ex);                
            }
        }

        // devuelve la cantidad de items en la tabla de trabajo para una libreta
        public int cantContactosWork(string libreta)
        {
            string SQL = "SELECT COUNT(*) FROM GAL_CONTACTOS_WORK WHERE LOWER(ADDRESSBOOK) = '" + libreta.ToLower() + "'";
            return int.Parse(Server.get_Con(false, true, false).ExecuteScalar(System.Data.CommandType.Text, SQL).ToString());
        }

        public void UpdateContactoWork(OutlookContact contacto)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" UPDATE GAL_CONTACTOS_WORK ");
            sb.Append(" SET EMAIL = ");
            sb.Append("'");
            sb.Append(contacto.EmailAddress.Trim());
            sb.Append("'");
            sb.Append(" WHERE ");
            sb.Append(" FULLNAME = ");
            sb.Append("'");
            sb.Append(contacto.FullName.Trim());
            sb.Append("'");

            if (_trans == null)
                Server.get_Con(false, true, false).ExecuteNonQuery(System.Data.CommandType.Text, sb.ToString());
            else
                _trans.Con.ExecuteNonQuery(_trans.Transaction, System.Data.CommandType.Text, sb.ToString());
        }

        // guarda un contacto
        public void SaveContact(OutlookContact contacto)
        {
            doSaveContact(contacto, false);
        }

        // guarda un contacto en la tabla temporal de trabajo
        public void SaveContactWork(OutlookContact contacto)
        {
            doSaveContact(contacto, true);
        }

        private void doSaveContact(OutlookContact contacto, bool work)
        {
            string sql;
            int cant;

            if (work)
                sql = "SELECT COUNT(*) FROM GAL_CONTACTOS_WORK WHERE EX = '" + contacto.EmailAddress.Trim() + "'";
            else
                sql = "SELECT COUNT(*) FROM GAL_CONTACTOS WHERE FULLNAME = '" + contacto.FullName.Trim() + "'";
            
            if (_log_query)
            {
                string aux = DateTime.Now.ToString("hh:mm:ss") + ": " + sql;
                Trace.WriteLineIf(ZTrace.IsVerbose, aux);
            }

            if (_trans == null)
                cant = Convert.ToInt32(Server.get_Con(false, true, false).ExecuteScalar(System.Data.CommandType.Text, sql));
            else
                cant = Convert.ToInt32(_trans.Con.ExecuteScalar(_trans.Transaction, CommandType.Text, sql));
             
            // no existia previamente
            if (cant == 0)
            {
                StringBuilder sb = new StringBuilder();

                if (work)
                {
                    sb.Append(" INSERT INTO GAL_CONTACTOS_WORK ");
                    sb.Append(" (FULLNAME, EX, TIPO, ADDRESSBOOK) VALUES (");
                    sb.Append("'");
                    sb.Append(contacto.FullName.Trim());
                    sb.Append("', '");
                    sb.Append(contacto.EmailAddress.Trim());
                    sb.Append("', '");
                    sb.Append(contacto.ContactType.ToString());
                    sb.Append("', '");
                    sb.Append(contacto.AddressBookName.Replace("'", "''").Trim());
                    sb.Append("')");
                }
                else
                {
                    sb.Append(" INSERT INTO GAL_CONTACTOS ");
                    sb.Append(" (FULLNAME, EMAIL, TIPO, ADDRESSBOOK) VALUES (");
                    sb.Append("'");
                    sb.Append(contacto.FullName.Trim());
                    sb.Append("', '");
                    sb.Append(contacto.EmailAddress.Trim());
                    sb.Append("', '");
                    sb.Append(contacto.ContactType.ToString());
                    sb.Append("', '");
                    sb.Append(contacto.AddressBookName.Replace("'", "''").Trim());
                    sb.Append("')");
                }

                if (_log_query)
                {
                    string aux = DateTime.Now.ToString("hh:mm:ss") + ": " + sb.ToString();
                    Trace.WriteLineIf(ZTrace.IsVerbose, aux);
                }

                if (_trans == null)
                    Server.get_Con(false, true, false).ExecuteNonQuery(System.Data.CommandType.Text, sb.ToString());
                else
                    _trans.Con.ExecuteNonQuery(_trans.Transaction, System.Data.CommandType.Text, sb.ToString());
            }
        }
    }
}