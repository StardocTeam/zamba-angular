using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zamba.ZTC
{
    public partial class frmSelectReportType : Form
    {
        public frmSelectReportType()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectedResumeMode = TCResumeBusiness.ResumeMode.PrepareExecution;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedResumeMode = TCResumeBusiness.ResumeMode.ListTC;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            selectedResumeMode = TCResumeBusiness.ResumeMode.Full;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        TCResumeBusiness.ResumeMode SelectedResumeMode;

        internal TCResumeBusiness.ResumeMode selectedResumeMode
        {
            get { return SelectedResumeMode; }
            set { SelectedResumeMode = value; }
        }

    }
}
