namespace Zamba.WorkFlow.Execution.Control
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
            this.pnlDesigner = new System.Windows.Forms.Panel();
            this.workflowViewSplitter = new System.Windows.Forms.SplitContainer();
            this.pnlDesigner.SuspendLayout();
            this.workflowViewSplitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDesigner
            // 
            this.pnlDesigner.Controls.Add(this.workflowViewSplitter);
            this.pnlDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDesigner.Location = new System.Drawing.Point(0, 0);
            this.pnlDesigner.Name = "pnlDesigner";
            this.pnlDesigner.Size = new System.Drawing.Size(699, 399);
            this.pnlDesigner.TabIndex = 1;
            this.pnlDesigner.TabStop = true;
            // 
            // workflowViewSplitter
            // 
            this.workflowViewSplitter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.workflowViewSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workflowViewSplitter.Location = new System.Drawing.Point(0, 0);
            this.workflowViewSplitter.Name = "workflowViewSplitter";
            this.workflowViewSplitter.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.workflowViewSplitter.Size = new System.Drawing.Size(699, 399);
            this.workflowViewSplitter.SplitterDistance = 120;
            this.workflowViewSplitter.TabIndex = 0;
            this.workflowViewSplitter.Text = "splitContainer1";
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
    }
}
