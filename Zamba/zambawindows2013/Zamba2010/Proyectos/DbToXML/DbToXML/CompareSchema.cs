using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zamba;
using static DbToXML.DBToXML;

namespace DbToXML
{
    public partial class CompareSchema : Form
    {
        private DataTable t1;
        private IIndex index;

        public DataRow SelectedResult { get; private set; }

        public CompareSchema()
        {
            InitializeComponent();
        }

        public CompareSchema(DataTable t1, IIndex index)
        {
            InitializeComponent();
            this.t1 = t1;
            this.index = index;
            this.label1.Text = index.Name + " Tipo: " + index.Type.ToString() + " Longitud: " + index.Len.ToString();
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            if(this.gridSource.SelectedRows.Count == 1)
            {
                this.SelectedResult = this.t1.Rows[this.gridSource.SelectedRows[0].Index];
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila para tomar como definicion");
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CompareSchema_Load(object sender, EventArgs e)
        {
            try
            {
                this.gridSource.DataSource = t1;
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }

        }

     
    }
}
