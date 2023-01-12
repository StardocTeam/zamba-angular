using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Zamba.AppBlock;

namespace Zamba.WorkFlow.Designer
{
    public partial class WorkFlowDesignerControl : ZControl
    {
        Zamba.WorkFlow.Designer.WorkflowControl wf;
        
        public WorkFlowDesignerControl(Boolean Enabled)
        {
            InitializeComponent();
            wf = new Zamba.WorkFlow.Designer.WorkflowControl(Enabled);
            wf.Dock = DockStyle.Fill;
            this.Controls.Add(wf);
            this.wf.BringToFront();

            this.btnDelete.Visible = 
                this.btnSave.Visible = this.ToolStripSeparator1.Visible =
                Enabled;
        }

        public WorkFlowDesignerControl(string path, Boolean Enabled)
        {
            InitializeComponent();
            wf = new Zamba.WorkFlow.Designer.WorkflowControl(path, Enabled);
            wf.Dock = DockStyle.Fill;
            this.Controls.Add(wf);
            this.wf.BringToFront();
            this.btnDelete.Visible = 
                this.btnSave.Visible = this.ToolStripSeparator1.Visible =
                Enabled;
        }

        public WorkFlowDesignerControl()
        {
            InitializeComponent();
            wf = new Zamba.WorkFlow.Designer.WorkflowControl();
            wf.Dock = DockStyle.Fill;
            this.Controls.Add(wf);
            this.wf.BringToFront();
            wf.ShowDocAsociated += new WorkflowControl.DShowDocAsociated(SD);
        }

        private void SD(int Id)
        {
            try
            {
                if (this.dShowDocAsociated != null)
                    dShowDocAsociated(0);
            }
            catch 
            { }
        }
        public delegate void DShowDocAsociated(int ID);
        private DShowDocAsociated dShowDocAsociated;

        public event DShowDocAsociated ShowDocAsociated
        {
            add
            {
                this.dShowDocAsociated += value;
            }
            remove { this.dShowDocAsociated -= value; }
        }

        private void zoomDropDownMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

                int zoomFactor = 0;

                bool result = Int32.TryParse(menuItem.Tag.ToString(), out zoomFactor);

                if (result)
                {
                    this.wf.ProcessZoom(zoomFactor);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.wf.DeleteSelected();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.wf.Save(true);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this.wf.LoadExistingWorkflow();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.wf.Export();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.wf.Print();
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            this.wf.ChangeName();
        }

        private void btnDocAsoc_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Oculta o muestra la toolbar
        /// </summary>
        /// <param name="tool"></param>
        public void showToolbar(Boolean tool)
        {
            this.ZToolBar.Visible = tool;           
        }
    }
}
