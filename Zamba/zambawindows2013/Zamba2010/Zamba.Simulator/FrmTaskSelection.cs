using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zamba.Core;
using Zamba.Membership;

namespace Zamba.Simulator
{
    public partial class FrmTaskSelection : Form
    {
        long stepId;

        public FrmTaskSelection(long stepId)
        {
            InitializeComponent();
            this.stepId = stepId;
        }

        private void FrmTaskSelection_Load(object sender, EventArgs e)
        {
            try
            {
                Zamba.Core.WF.WF.WFTaskBusiness wftb = new Core.WF.WF.WFTaskBusiness();
                dgvTasks.DataSource = wftb.GetTasksByStepIdDataTable(stepId, false, MembershipHelper.CurrentUser.ID, 0, 100);
            }
            catch (Exception )
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }


    }
}
