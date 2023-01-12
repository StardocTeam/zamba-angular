using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Zamba.Core;

namespace Zamba.WorkFlow.Execution.Control
{
    public partial class WorkflowDesignerControl : UserControl
    {
        Zamba.WorkFlow.Execution.Control.WorkflowControl wf;
        
        public WorkflowDesignerControl(bool showAdmin)
        {
            InitializeComponent();
            wf = new Zamba.WorkFlow.Execution.Control.WorkflowControl();
            wf.Dock = DockStyle.Fill;
            this.Controls.Add(wf);
            wf.BringToFront();
            AdminOrClient(showAdmin);
        }

        /// <summary>
        /// Muestra un xoml
        /// </summary>
        /// <_param name="stepID">Id de la etapa</_param>
        /// <_param name="showAdmin">Si es para el administrador muestro el salvar, si es para el 
        /// cliente, muestro el correr</_param>
        /// <_param name="isBasic">Si es el workflow basico</_param>
        public WorkflowDesignerControl(Int64 stepID, bool showAdmin, bool isBasic)
        {
            InitializeComponent();
            wf = new Zamba.WorkFlow.Execution.Control.WorkflowControl(stepID);
            wf.OnOpenRegionWF += new OpenRegionWF(this.OpenRegionWF);
            wf.Dock = DockStyle.Fill;
            this.Controls.Add(wf);
            wf.BringToFront();
            AdminOrClient(showAdmin);
            //Si es el workflow basico no muestro el boton salvar
            if (isBasic == true)
                this.btnSave.Visible = false;
        }

        public WorkflowDesignerControl(Int64 stepId,Int64 parentId, bool showAdmin)
        {
            InitializeComponent();
            wf = new Zamba.WorkFlow.Execution.Control.WorkflowControl(stepId,parentId);
            wf.Dock = DockStyle.Fill;
            this.Controls.Add(wf);
            wf.BringToFront();
            AdminOrClient(showAdmin);
        }

        
        /// <summary>
        /// Save the workflow
        /// </summary>
        /// <_param name="sender"></_param>
        /// <_param name="e"></_param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //this.wf.Save();
        }
        /// <summary>
        /// Run the workflow
        /// </summary>
        /// <_param name="sender"></_param>
        /// <_param name="e"></_param>
        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            this.wf.Run();
        }
        /// <summary>
        /// Change zoom of the workflow
        /// </summary>
        /// <_param name="sender"></_param>
        /// <_param name="e"></_param>
        private void btnZoom_Click(object sender, EventArgs e)
        {
            if (txtZoom.Text != "")
            {
                this.wf.ProcessZoom(int.Parse(txtZoom.Text));
            }
        }
        /// <summary>
        /// Set the txtZoom location over the tooltrip
        /// </summary>
        /// <_param name="e"></_param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            txtZoom.Size = new Size(txtFakeZoom.Size.Width, txtFakeZoom.Size.Height);
            txtZoom.Location = new Point(toolStrip.Size.Width - txtZoom.Width - 1 - btnZoom.Size.Width, toolStrip.Size.Height - txtZoom.Height - 2);
        }
        /// <summary>
        /// Set the controls to be show
        /// </summary>
        /// <_param name="showAdmin"></_param>
        private void AdminOrClient(Boolean showAdmin)
        {
            if (showAdmin == true)
            {
                this.btnEjecutar.Visible = false;
            }
            else
            {
                this.btnSave.Visible = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (dCloseWF != null)
                dCloseWF((TabPage)this.Parent);
        }

        public event CloseWF OnCloseWF
        {
            add
            {
                this.dCloseWF += value;
            }
            remove
            {
                this.dCloseWF -= value;
            }
        }
        private CloseWF dCloseWF = null;

        private void OpenRegionWF(Int64 stepId,Int64 parentId)
        {
            if (this.dOpenRegionWF != null)
                this.dOpenRegionWF(stepId,parentId);
        }

        /// <summary>
        /// Refresh workflow
        /// </summary>
        public event OpenRegionWF OnOpenRegionWF
        {
            add
            {
                this.dOpenRegionWF += value;
            }
            remove
            {
                this.dOpenRegionWF -= value;
            }
        }
        private OpenRegionWF dOpenRegionWF = null;
    }

    public delegate void CloseWF(TabPage padre);
}