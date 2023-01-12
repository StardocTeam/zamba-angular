using System.Windows.Forms;
using Zamba.AppBlock;

namespace Zamba.ZTC
{
    partial class UCTestCaseExecutionHistory
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radExecutionHistoryGrid = new Telerik.WinControls.UI.RadGridView();
            this.HistoryTaskCaseBar = new ZToolBar();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.ToolStripContainer1 = new ToolStripContainer();
            ((System.ComponentModel.ISupportInitialize)(this.radExecutionHistoryGrid)).BeginInit();
            this.HistoryTaskCaseBar.SuspendLayout();
            this.ToolStripContainer1.ContentPanel.SuspendLayout();
            this.ToolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radExecutionHistoryGrid
            // 
            this.radExecutionHistoryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radExecutionHistoryGrid.Location = new System.Drawing.Point(0, 0);
            // 
            // radExecutionHistoryGrid
            // 
            this.radExecutionHistoryGrid.MasterTemplate.AllowAddNewRow = false;
            this.radExecutionHistoryGrid.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.radExecutionHistoryGrid.Name = "radExecutionHistoryGrid";
            this.radExecutionHistoryGrid.ReadOnly = true;
            this.radExecutionHistoryGrid.Size = new System.Drawing.Size(853, 495);
            this.radExecutionHistoryGrid.TabIndex = 1;
            this.radExecutionHistoryGrid.Text = "radGridView1";
            this.radExecutionHistoryGrid.ToolTipTextNeeded += new Telerik.WinControls.ToolTipTextNeededEventHandler(this.radExecutionHistoryGrid_ToolTipTextNeeded);
            this.radExecutionHistoryGrid.Click += new System.EventHandler(this.radExecutionHistoryGrid_Click);
            // 
            // HistoryTaskCaseBar
            // 
            this.HistoryTaskCaseBar.Dock = System.Windows.Forms.DockStyle.None;
            this.HistoryTaskCaseBar.Items.AddRange(new ToolStripItem[] {
            this.btnRefresh});
            this.HistoryTaskCaseBar.Location = new System.Drawing.Point(49, 0);
            this.HistoryTaskCaseBar.Name = "HistoryTaskCaseBar";
            this.HistoryTaskCaseBar.Size = new System.Drawing.Size(35, 25);
            this.HistoryTaskCaseBar.TabIndex = 2;
            this.HistoryTaskCaseBar.Visible = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Zamba.ZTC.Properties.Resources.refresh_refrescar_actualizar;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnRefresh.Text = "Actualizar";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // ToolStripContainer1
            // 
            // 
            // ToolStripContainer1.ContentPanel
            // 
            this.ToolStripContainer1.ContentPanel.Controls.Add(this.HistoryTaskCaseBar);
            this.ToolStripContainer1.ContentPanel.Controls.Add(this.radExecutionHistoryGrid);
            this.ToolStripContainer1.ContentPanel.Size = new System.Drawing.Size(853, 495);
            this.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.ToolStripContainer1.Name = "ToolStripContainer1";
            this.ToolStripContainer1.Size = new System.Drawing.Size(853, 520);
            this.ToolStripContainer1.TabIndex = 3;
            this.ToolStripContainer1.Text = "ToolStripContainer1";
            // 
            // UCTestCaseExecutionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ToolStripContainer1);
            this.Name = "UCTestCaseExecutionHistory";
            this.Size = new System.Drawing.Size(853, 520);
            ((System.ComponentModel.ISupportInitialize)(this.radExecutionHistoryGrid)).EndInit();
            this.HistoryTaskCaseBar.ResumeLayout(false);
            this.HistoryTaskCaseBar.PerformLayout();
            this.ToolStripContainer1.ContentPanel.ResumeLayout(false);
            this.ToolStripContainer1.ContentPanel.PerformLayout();
            this.ToolStripContainer1.ResumeLayout(false);
            this.ToolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView radExecutionHistoryGrid;
        private ZToolBar HistoryTaskCaseBar;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private ToolStripContainer ToolStripContainer1;
  
    }
}
