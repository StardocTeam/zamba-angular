using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace TestUpdateImage
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

                QueryBuilder.Append("INSERT INTO DocumentKeyIndexs(Id, IndexId, IndexValue) ");
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
        ///     [Gaston]    27/05/2009  Modified        Funcionalidad para Oracle  
        /// </history>
        private void btInsert_Click(object sender, EventArgs e)
        {
            try
            {
                #region Query

                StringBuilder QueryBuilder = new StringBuilder();
                QueryBuilder.Append("INSERT INTO RemoteUpdate ");

                if(Zamba.Servers.Server.isSQLServer)
                {
                    QueryBuilder.Append("(DocTypeId) ");
                    QueryBuilder.Append("VALUES( ");
                    QueryBuilder.Append(txtDocTypeId.Text);
                    QueryBuilder.Append(")");
                }
                else if(Zamba.Servers.Server.isOracle)
                {
                    object tempIdObj = (object)Zamba.Servers.Server.get_Con(false, false, false).ExecuteScalar(CommandType.Text, "SELECT MAX(TEMPORARYID) AS LastId FROM REMOTEUPDATE");
                    Int32 tempId ;  

                    if(tempIdObj is DBNull)
                        tempId = 1;
                    else
                        tempId = Convert.ToInt32(tempIdObj) + 1;

                    QueryBuilder.Append("(TemporaryID, DocTypeId, Status, Information) ");
                    QueryBuilder.Append("VALUES (" + tempId + ", " + txtDocTypeId.Text + ", 0, '')");
                }

                #endregion

                Zamba.Servers.Server.get_Con(false, false, false).ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString());

                txtDocIdUpd.Text = txtDocId.Text = GetLastInsertedId().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        Zamba.Core.RemoteUpdate RU = new Zamba.Core.RemoteUpdate();

        /// <summary>
        /// Método que se utiliza para obtener el máximo temporaryID de la tabla "RemoteUpdate"
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///     [Gaston]    27/05/2009  Modified      Funcionalidad para Oracle
        /// </history>
        private static Int32 GetLastInsertedId()
        {
            Int32 Value = 0;
            Object ReturnId = null;

            if(Zamba.Servers.Server.isSQLServer)
                ReturnId = Zamba.Servers.Server.get_Con(false, false, false).ExecuteScalar(CommandType.Text, "select max(temporaryId) as 'Id' from remoteUpdate");
            else if(Zamba.Servers.Server.isOracle)
                ReturnId = Zamba.Servers.Server.get_Con(false, false, false).ExecuteScalar(CommandType.Text, "select max(temporaryId) as Id from remoteUpdate");

            if (null != ReturnId)
                Int32.TryParse(ReturnId.ToString(), out Value);

            return Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder QueryBuilder = new StringBuilder();

                QueryBuilder.Append("INSERT INTO DocumentUpdateIndexs(Id, IndexId, IndexValue) ");
                QueryBuilder.Append("VALUES (");
                QueryBuilder.Append(txtDocIdUpd.Text);
                QueryBuilder.Append(",");
                QueryBuilder.Append(txtIndexIdUpd.Text);
                QueryBuilder.Append(",'");
                QueryBuilder.Append(txtIndexValueUpd.Text);
                QueryBuilder.Append("')");

                Zamba.Servers.Server.get_Con(false, false, false).ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateState_Click(object sender, EventArgs e)
        {
            RU.SaveDocumentRemoteStatus(Int64.Parse(txtDocId.Text), 3);
        }
    }
}