using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace UserImport
{
    public partial class UserImport : Form
    {
        private DataTable dTable;
        private OleDbConnection excelConnection;
        private OleDbCommand dbCommand;
        private OleDbDataAdapter dataAdapter;

        public UserImport()
        {
            InitializeComponent();
        }

        private void cmbExaminar_Click(object sender, EventArgs e)
        {
            openFile.ShowDialog();
            txtExcel.Text = openFile.FileName;

            cmbVerificar.Enabled = true;
        }

        private void UserImport_Load(object sender, EventArgs e)
        {
            cmbImportar.Enabled = false;
            cmbVerificar.Enabled = false;

            lstActualizaran.Enabled = false;
            lstAgregaran.Enabled = false;
        }

        private void cmbVerificar_Click(object sender, EventArgs e)
        {
            VerficarUsuarios();
            cmbImportar.Enabled = true;
        }

        private void cmbImportar_Click(object sender, EventArgs e)
        {
            ImportarUsuarios();
        }

        private void ConectarExcel()
        {            
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtExcel.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;\"";

            string strSQL = "SELECT * FROM [Hoja1$]";

            excelConnection = new OleDbConnection(connectionString);
            excelConnection.Open(); // this will open an Excel file
            dbCommand = new OleDbCommand(strSQL, excelConnection);
            dataAdapter = new OleDbDataAdapter(dbCommand);

            // create data table
            dTable = new DataTable();

            dataAdapter.Fill(dTable);
        }

        private void VerficarUsuarios()
        {
            string user;

            ConectarExcel();

            foreach (DataRow row in dTable.Rows)
            {
                user = row["apellido"] + ", " + row["nombre"];

                if (UsuarioExiste(user))
                    lstActualizaran.Items.Add(user);
                else
                    lstAgregaran.Items.Add(user);
            }

            lstActualizaran.Enabled = true;
            lstAgregaran.Enabled = true;
        }

        private void ImportarUsuarios()
        {

        }

        private bool UsuarioExiste(string user)
        {
            return true;
        }
    }
}
