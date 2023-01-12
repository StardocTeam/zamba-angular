using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Zamba.AppBlock;

namespace Zamba.QuickSearch
{
    partial class frmQuickSearch
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuickSearch));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tmr1 = new System.Windows.Forms.Timer(this.components);
            this.tmr2 = new System.Windows.Forms.Timer(this.components);
            this.ZToolBar1 = new ZToolBar();
            this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
           
            this.ZToolBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Enabled = false;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Zamba Quick Search";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_Click);
            // 
            // tmr1
            // 
            this.tmr1.Interval = 1;
            this.tmr1.Tick += new System.EventHandler(this.tmr1_Tick);
            // 
            // tmr2
            // 
            this.tmr2.Interval = 1;
            this.tmr2.Tick += new System.EventHandler(this.tmr2_Tick);
            // 
            // ZToolBar1
            // 
            this.ZToolBar1.Items.AddRange(new ToolStripItem[] {
            this.ToolStripLabel1});
            this.ZToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ZToolBar1.Name = "ZToolBar1";
            this.ZToolBar1.Size = new System.Drawing.Size(784, 25);
            this.ZToolBar1.TabIndex = 0;
            this.ZToolBar1.Text = "ZToolBar1";
            this.ZToolBar1.Visible = false;
            this.ZToolBar1.GripStyle = ToolStripGripStyle.Hidden;
            this.ZToolBar1.Renderer = new Zamba.AppBlock.MyStripRender();
            // 
            // ToolStripLabel1
            // 
            this.ToolStripLabel1.Name = "ToolStripLabel1";
            this.ToolStripLabel1.Size = new System.Drawing.Size(86, 22);
            this.ToolStripLabel1.Text = "ToolStripLabel1";
           
            // 
            // frmQuickSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 421);
            this.Controls.Add(this.ZToolBar1);
            this.ForeColor = System.Drawing.Color.MidnightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(403, 451);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQuickSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Zamba Quick Search";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Red;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ZToolBar1.ResumeLayout(false);
            this.ZToolBar1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Timer tmr1;
        private System.Windows.Forms.Timer tmr2;
        private ZToolBar ZToolBar1;
        private System.Windows.Forms.ToolStripLabel ToolStripLabel1;
    }
}