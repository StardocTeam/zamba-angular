using System.Windows.Forms;
using Zamba.AppBlock;

namespace Zamba.Debugger
{
    partial class DebuggerForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebuggerForm));
            this.tcDebugger = new System.Windows.Forms.TabControl();
            this.TabGeneral = new System.Windows.Forms.TabPage();
            this.tabTrace = new System.Windows.Forms.TabPage();
            this.dtGridView = new System.Windows.Forms.DataGridView();
            this.ToolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolbarDebugCtrls = new Zamba.AppBlock.ZToolBar();
            this.tsBtnContinue = new System.Windows.Forms.ToolStripButton();
            this.tsBtnStop = new System.Windows.Forms.ToolStripButton();
            this.tsBtnPause = new System.Windows.Forms.ToolStripButton();
            this.tsBtnClear = new System.Windows.Forms.ToolStripButton();
            this.tsBtnDetails = new System.Windows.Forms.ToolStripButton();
            this.tcDebugger.SuspendLayout();
            this.tabTrace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridView)).BeginInit();
            this.ToolStripContainer1.ContentPanel.SuspendLayout();
            this.ToolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.ToolStripContainer1.SuspendLayout();
            this.toolbarDebugCtrls.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcDebugger
            // 
            this.tcDebugger.Controls.Add(this.TabGeneral);
            this.tcDebugger.Controls.Add(this.tabTrace);
            this.tcDebugger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDebugger.Location = new System.Drawing.Point(0, 0);
            this.tcDebugger.Name = "tcDebugger";
            this.tcDebugger.SelectedIndex = 0;
            this.tcDebugger.Size = new System.Drawing.Size(708, 388);
            this.tcDebugger.TabIndex = 0;
            this.tcDebugger.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged_1);
            // 
            // TabGeneral
            // 
            this.TabGeneral.BackColor = System.Drawing.Color.White;
            this.TabGeneral.Location = new System.Drawing.Point(4, 22);
            this.TabGeneral.Name = "TabGeneral";
            this.TabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.TabGeneral.Size = new System.Drawing.Size(700, 362);
            this.TabGeneral.TabIndex = 0;
            this.TabGeneral.Text = "General";
            this.TabGeneral.UseVisualStyleBackColor = true;
            // 
            // tabTrace
            // 
            this.tabTrace.Controls.Add(this.dtGridView);
            this.tabTrace.Location = new System.Drawing.Point(4, 22);
            this.tabTrace.Name = "tabTrace";
            this.tabTrace.Padding = new System.Windows.Forms.Padding(3);
            this.tabTrace.Size = new System.Drawing.Size(700, 362);
            this.tabTrace.TabIndex = 3;
            this.tabTrace.Text = "Trace";
            this.tabTrace.UseVisualStyleBackColor = true;
            // 
            // dtGridView
            // 
            this.dtGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGridView.Location = new System.Drawing.Point(3, 3);
            this.dtGridView.Name = "dtGridView";
            this.dtGridView.Size = new System.Drawing.Size(694, 356);
            this.dtGridView.TabIndex = 1;
            // 
            // ToolStripContainer1
            // 
            // 
            // ToolStripContainer1.ContentPanel
            // 
            this.ToolStripContainer1.ContentPanel.Controls.Add(this.tcDebugger);
            this.ToolStripContainer1.ContentPanel.Size = new System.Drawing.Size(708, 388);
            this.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.ToolStripContainer1.Name = "ToolStripContainer1";
            this.ToolStripContainer1.Size = new System.Drawing.Size(708, 413);
            this.ToolStripContainer1.TabIndex = 1;
            this.ToolStripContainer1.Text = "ToolStripContainer1";
            // 
            // ToolStripContainer1.TopToolStripPanel
            // 
            this.ToolStripContainer1.TopToolStripPanel.Controls.Add(this.toolbarDebugCtrls);
            // 
            // toolbarDebugCtrls
            // 
            this.toolbarDebugCtrls.BackColor = System.Drawing.Color.White;
            this.toolbarDebugCtrls.Dock = System.Windows.Forms.DockStyle.None;
            this.toolbarDebugCtrls.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbarDebugCtrls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnContinue,
            this.tsBtnStop,
            this.tsBtnPause,
            this.tsBtnClear,
            this.tsBtnDetails});
            this.toolbarDebugCtrls.Location = new System.Drawing.Point(0, 0);
            this.toolbarDebugCtrls.Name = "toolbarDebugCtrls";
            this.toolbarDebugCtrls.Size = new System.Drawing.Size(708, 25);
            this.toolbarDebugCtrls.Stretch = true;
            this.toolbarDebugCtrls.TabIndex = 0;
            // 
            // tsBtnContinue
            // 
            this.tsBtnContinue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.tsBtnContinue.Image = global::Zamba.HelpersControls.Properties.Resources.appbar_control_play;
            this.tsBtnContinue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnContinue.Name = "tsBtnContinue";
            this.tsBtnContinue.Size = new System.Drawing.Size(80, 22);
            this.tsBtnContinue.Text = "Continuar";
            this.tsBtnContinue.Click += new System.EventHandler(this.tsBtnContinue_Click);
            // 
            // tsBtnStop
            // 
            this.tsBtnStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.tsBtnStop.Image = global::Zamba.HelpersControls.Properties.Resources.appbar_debug_stop;
            this.tsBtnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnStop.Name = "tsBtnStop";
            this.tsBtnStop.Size = new System.Drawing.Size(68, 22);
            this.tsBtnStop.Text = "Detener";
            // 
            // tsBtnPause
            // 
            this.tsBtnPause.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.tsBtnPause.Image = global::Zamba.HelpersControls.Properties.Resources.appbar_control_pause;
            this.tsBtnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnPause.Name = "tsBtnPause";
            this.tsBtnPause.Size = new System.Drawing.Size(62, 22);
            this.tsBtnPause.Text = "Pausar";
            // 
            // tsBtnClear
            // 
            this.tsBtnClear.Image = global::Zamba.HelpersControls.Properties.Resources.appbar_clean;
            this.tsBtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnClear.Name = "tsBtnClear";
            this.tsBtnClear.Size = new System.Drawing.Size(67, 22);
            this.tsBtnClear.Text = "Limpiar";
            this.tsBtnClear.Click += new System.EventHandler(this.TsBtnClear_Click);
            // 
            // tsBtnDetails
            // 
            this.tsBtnDetails.CheckOnClick = true;
            this.tsBtnDetails.Image = global::Zamba.HelpersControls.Properties.Resources.appbar_sidebar_right_expand;
            this.tsBtnDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnDetails.Name = "tsBtnDetails";
            this.tsBtnDetails.Size = new System.Drawing.Size(100, 22);
            this.tsBtnDetails.Text = "Panel Detalles";
            this.tsBtnDetails.Click += new System.EventHandler(this.tsBtnDetails_Click);
            // 
            // DebuggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 413);
            this.Controls.Add(this.ToolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DebuggerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zamba Debugger";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebuggerForm_FormClosing);
            this.Load += new System.EventHandler(this.DebuggerForm_Load);
            this.tcDebugger.ResumeLayout(false);
            this.tabTrace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridView)).EndInit();
            this.ToolStripContainer1.ContentPanel.ResumeLayout(false);
            this.ToolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.ToolStripContainer1.TopToolStripPanel.PerformLayout();
            this.ToolStripContainer1.ResumeLayout(false);
            this.ToolStripContainer1.PerformLayout();
            this.toolbarDebugCtrls.ResumeLayout(false);
            this.toolbarDebugCtrls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcDebugger;
        private System.Windows.Forms.TabPage TabGeneral;
        private ToolStripContainer ToolStripContainer1;
        private ZToolBar toolbarDebugCtrls;
        private System.Windows.Forms.ToolStripButton tsBtnContinue;
        private System.Windows.Forms.ToolStripButton tsBtnStop;
        private System.Windows.Forms.ToolStripButton tsBtnPause;
        private TabPage tabTrace;
        private DataGridView dtGridView;
        private ToolStripButton tsBtnClear;
        private ToolStripButton tsBtnDetails;
    }
}