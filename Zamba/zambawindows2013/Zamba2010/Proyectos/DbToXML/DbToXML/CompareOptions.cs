using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DbToXML.DBToXML;

namespace DbToXML
{
    public partial class CompareOptions : Form
    {
        private DataRow r1;
        private DataRow r2;
        private TableStructure value;

        public AnalizeResults ConflictResult { get; private set; }
        public bool DontAskAgain { get; internal set; }

        public CompareOptions()
        {
            InitializeComponent();
        }

        public CompareOptions(DataRow r1, DataRow r2, TableStructure value)
        {
            InitializeComponent();
            this.r1 = r1;
            this.r2 = r2;
            this.value = value;
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked || this.radioButton2.Checked || this.radioButton3.Checked || this.radioButton4.Checked || this.rdkeppboth.Checked)
            {
                if (this.radioButton1.Checked)
                {
                    this.ConflictResult = AnalizeResults.Update;
                }
                else if (this.radioButton2.Checked)
                {
                    this.ConflictResult = AnalizeResults.ReCreateOrigin;
                }
                else if (this.radioButton3.Checked)
                {
                    this.ConflictResult = AnalizeResults.ReCreateDestiny;
                }
                else if (this.rdkeppboth.Checked)
                {
                    this.ConflictResult = AnalizeResults.KeepBoth;
                }
                else
                {
                    this.ConflictResult = AnalizeResults.DontInsert;
                }
                this.DialogResult = DialogResult.OK;
                this.DontAskAgain = chkdontaskagain.Checked;

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

        private void CompareOptions_Load(object sender, EventArgs e)
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
                this.gridDestiny.DataSource = cloneTable2;
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.gridResult.DataSource = r1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //addbothtonewtabletoshow
            this.gridResult.DataSource = r1; //generatenewid
            this.gridResult.DataSource = r2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //addbothtonewtabletoshow
            this.gridResult.DataSource = r1;
            this.gridResult.DataSource = r2; //generatenewid

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            this.gridResult.DataSource = r2;

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
