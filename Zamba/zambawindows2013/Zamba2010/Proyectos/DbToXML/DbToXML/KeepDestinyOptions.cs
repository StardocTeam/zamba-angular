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
    public partial class KeepDestinyOptions : Form
    {
        private DataRow r2;
        private TableStructure value;

        public AnalizeResults ConflictResult { get; private set; }
        public bool DontAskAgain { get; internal set; }

        public KeepDestinyOptions()
        {
            InitializeComponent();
        }

        public KeepDestinyOptions(DataRow r2, TableStructure value)
        {
            InitializeComponent();
            this.r2 = r2;
            this.value = value;
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            this.ConflictResult = AnalizeResults.KeepBoth;
                this.DialogResult = DialogResult.OK;
            this.DontAskAgain = chkdontaskagain.Checked;
            this.Close();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CompareSchema_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable cloneTable2;
                cloneTable2 = r2.Table.Clone();
                cloneTable2.ImportRow(r2);
                this.gridSource.DataSource = cloneTable2;
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ConflictResult = AnalizeResults.DeleteOrigin;
            this.DialogResult = DialogResult.OK;
            this.DontAskAgain = chkdontaskagain.Checked;
            this.Close();

        }
    }
}
