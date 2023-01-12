//--------------------------------------------------------------------------------
// This file is a "Sample" as from Windows Workflow Foundation
// Beta 2 Hands on Labs
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft
// Development Tools and/or on-line documentation.  See these other
// materials for detailed information regarding Microsoft code samples.
// 
// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//--------------------------------------------------------------------------------

using System.Windows.Forms;
namespace Zamba.WorkFlow.Designer
{
    partial class WorkflowControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkflowControl));
            this.pnlDesigner = new System.Windows.Forms.Panel();
            this.workflowViewSplitter = new System.Windows.Forms.SplitContainer();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.pnlDesigner.SuspendLayout();
            this.workflowViewSplitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDesigner
            // 
            this.pnlDesigner.Controls.Add(this.workflowViewSplitter);
            this.pnlDesigner.Dock = DockStyle.Fill;
            this.pnlDesigner.Location = new System.Drawing.Point(0, 0);
            this.pnlDesigner.Name = "pnlDesigner";
            this.pnlDesigner.Size = new System.Drawing.Size(699, 399);
            this.pnlDesigner.TabIndex = 1;
            this.pnlDesigner.TabStop = true;
            // 
            // workflowViewSplitter
            // 
            this.workflowViewSplitter.BorderStyle = BorderStyle.FixedSingle;
            this.workflowViewSplitter.Dock = DockStyle.Fill;
            this.workflowViewSplitter.Location = new System.Drawing.Point(0, 0);
            this.workflowViewSplitter.Name = "workflowViewSplitter";
            this.workflowViewSplitter.Size = new System.Drawing.Size(699, 399);
            this.workflowViewSplitter.SplitterDistance = 150;
            this.workflowViewSplitter.TabIndex = 0;
            this.workflowViewSplitter.Text = "splitContainer1";
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(41, 22);
            this.btnDelete.Text = "Borrar";
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(34, 22);
            this.btnOpen.Text = "Abrir";
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(50, 22);
            this.btnSave.Text = "Guardar";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(53, 17);
            this.btnExport.Text = "Exportar";
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(49, 17);
            this.btnPrint.Text = "Imprimir";
            // 
            // WorkflowControl
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlDesigner);
            this.Name = "WorkflowControl";
            this.Size = new System.Drawing.Size(699, 399);
            this.pnlDesigner.ResumeLayout(false);
            this.workflowViewSplitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDesigner;
        private System.Windows.Forms.SplitContainer workflowViewSplitter;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.ToolStripButton btnPrint;
    }
}
