using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace TestInsertImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btInsertIndex_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder QueryBuilder = new StringBuilder();

                QueryBuilder.Append("INSERT INTO DocumentsIndexs(Id, IndexId, IndexValue) ");
                QueryBuilder.Append("VALUES (");
                QueryBuilder.Append(txtDocId.Text);
                QueryBuilder.Append(",");
                QueryBuilder.Append(txtIndexId.Text);
                QueryBuilder.Append(",'");
                QueryBuilder.Append(txtIndexValue.Text);
                QueryBuilder.Append("')");

                Zamba.Servers.Server.get_Con(false, false, false).ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al presionar el botón "Insertar Documento"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [Gaston]    26/05/2009  Modified      Funcionalidad para Oracle
        /// </history>
        private void btInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(txtPath.Text))
                {
                    MessageBox.Show("Archivo inexistente");
                    return;
                }

                #region Query

                StringBuilder QueryBuilder = new StringBuilder();
                QueryBuilder.Append("INSERT INTO RemoteInsert ");

                if(Zamba.Servers.Server.isSQLServer)
                {
                    QueryBuilder.Append("(DocumentName, DocTypeId, SerializedFile, FileExtension) ");
                    QueryBuilder.Append("VALUES('");
                }
                else if (Zamba.Servers.Server.isOracle)
                {
                    QueryBuilder.Append("(TemporaryID, DocumentName, DocTypeId, SerializedFile, FileExtension) ");
                    QueryBuilder.Append("VALUES(");
                    QueryBuilder.Append(":TempID,'");
                }
                
                QueryBuilder.Append(txtDocumentName.Text);
                QueryBuilder.Append("',");
                QueryBuilder.Append(txtDocTypeId.Text);

                if(Zamba.Servers.Server.isSQLServer)       
                    QueryBuilder.Append(",@Blob,'");
                else                                       
                    QueryBuilder.Append(",:Blob,'");
                
                FileInfo a = new FileInfo(txtPath.Text);
                QueryBuilder.Append(a.Extension);

                QueryBuilder.Append("')");
                #endregion

                FileStream fs = new FileStream(txtPath.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                br.BaseStream.Position = 0;

                byte[] BinaryImage = br.ReadBytes((int)br.BaseStream.Length);

                if (Zamba.Servers.Server.isSQLServer)
                {
                    SqlParameter SqlServerParameter = new SqlParameter("@Blob", BinaryImage);
                    SqlServerParameter.DbType = DbType.Binary;

                    IDbDataParameter[] Parameters = { (IDbDataParameter)SqlServerParameter };
                    Zamba.Servers.Server.get_Con(false, false, false).ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString(), Parameters);
                }
                else if (Zamba.Servers.Server.isOracle)
                {
                    object tempId = (object)Zamba.Servers.Server.get_Con(false, false, false).ExecuteScalar(CommandType.Text, "SELECT MAX(TEMPORARYID) AS LastId FROM REMOTEINSERT");
                    OracleParameter oracleParameter = new OracleParameter();
                    oracleParameter.ParameterName = ":TempID";
                    oracleParameter.DbType = DbType.Int32;

                    if (tempId is DBNull)
                        oracleParameter.Value = 1;
                    else
                        oracleParameter.Value = Convert.ToInt32(tempId.ToString()) + 1;
                    
                    OracleParameter oracleServerParameter = new OracleParameter(":Blob", BinaryImage);
                    oracleServerParameter.DbType = DbType.Binary;

                    IDbDataParameter[] Parameters = { (IDbDataParameter)oracleServerParameter, (IDbDataParameter)oracleParameter };

                    Zamba.Servers.Server.get_Con(false, false, false).ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString(),Parameters);
                }

                txtDocId.Text = GetLastInsertedId().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Método que se utiliza para obtener el máximo temporaryID de la tabla "RemoteInsert"
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///     [Gaston]    26/05/2009  Modified      Funcionalidad para Oracle
        /// </history>
        private static Int32 GetLastInsertedId()
        {
            Int32 Value = 0;
            Object ReturnId = null;

            if(Zamba.Servers.Server.isSQLServer)
                ReturnId = Zamba.Servers.Server.get_Con(false, false, false).ExecuteScalar(CommandType.Text, "select max(temporaryId) as 'Id' from remoteinsert");
            else if (Zamba.Servers.Server.isOracle)
                ReturnId = Zamba.Servers.Server.get_Con(false, false, false).ExecuteScalar(CommandType.Text, "select max(temporaryId) as Id from remoteinsert");

            if (null != ReturnId)
                Int32.TryParse(ReturnId.ToString(), out Value);

            return Value;
        }

        Zamba.Core.RemoteInsert RI = new Zamba.Core.RemoteInsert();
        private void btnUpdateState_Click(object sender, EventArgs e)
        {
           RI.SaveDocumentStatus(Int64.Parse(txtDocId.Text), 3,0);
        }
    }
}