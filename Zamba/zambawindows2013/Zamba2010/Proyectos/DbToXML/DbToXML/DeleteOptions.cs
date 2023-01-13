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
    public partial class DeleteOptions : Form
    {
        private DataRow r1;
        private TableStructure value;

        public AnalizeResults ConflictResult { get; private set; }
        public bool DontAskAgain { get; internal set; }

        public DeleteOptions()
        {
            InitializeComponent();
        }

        public DeleteOptions(DataRow r1, TableStructure value)
        {
            InitializeComponent();
            this.r1 = r1;
            this.value = value;
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            this.ConflictResult = AnalizeResults.DeleteOrigin;

            this.DialogResult = DialogResult.OK;
            this.DontAskAgain = chkdontaskagain.Checked;

            this.Close();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.DontAskAgain = chkdontaskagain.Checked;
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

            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }

        }

    }
}
