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
    public partial class CompareAttributes : Form
    {
        private DataRow r1;
        private DataRow r2;
        private IIndex index;

        public DataRow SelectedResult { get; private set; }

        public CompareAttributes()
        {
            InitializeComponent();
        }

        public CompareAttributes(DataRow r1, DataRow r2, IIndex index)
        {
            InitializeComponent();
            this.r1 = r1;
            this.r2 = r2;
            this.index = index;
            this.label1.Text = index.Name + " Tipo: " + index.Type.ToString() + " Longitud: " + index.Len.ToString();
        }

        public AnalizeResults ConflictResult { get; private set; }

        private void btnok_Click(object sender, EventArgs e)
        {
            if (this.RegenrateDestiny.Checked || this.RegenerateSource.Checked || this.UpdateSource.Checked || this.UpdateDestiny.Checked || this.insert.Checked)
            {
                if (this.RegenrateDestiny.Checked)
                {
                    this.ConflictResult = AnalizeResults.ReCreateDestiny;
                }
                else if (this.RegenerateSource.Checked)
                {
                    this.ConflictResult = AnalizeResults.ReCreateOrigin;
                }
                else if (this.UpdateDestiny.Checked)
                {
                    this.ConflictResult = AnalizeResults.UpdateDestiny;
                }
                else if (this.UpdateSource.Checked)
                {
                    this.ConflictResult = AnalizeResults.UpdateSource;
                }
                else if (this.insert.Checked)
                {
                    this.ConflictResult = AnalizeResults.Insert;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una opcion");
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
                DataTable cloneTable1;
                cloneTable1 = r1.Table.Clone();
                cloneTable1.ImportRow(r1);
                this.gridSource.DataSource = cloneTable1;

                DataTable cloneTable2;
                cloneTable2 = r2.Table.Clone();
                cloneTable2.ImportRow(r2);
                this.dataGridView1.DataSource = cloneTable2;
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }

        }

     
    }
}
