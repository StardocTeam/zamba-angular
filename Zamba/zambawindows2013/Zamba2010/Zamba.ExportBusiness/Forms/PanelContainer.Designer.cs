using Zamba.AppBlock;

namespace OutlookPanel
{
    partial class PanelContainer
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
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnShowBar = new ZButton();
            this.pnlContainer.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContainer.AutoSize = true;
            this.pnlContainer.Controls.Add(this.splitContainer1);
            this.pnlContainer.Location = new System.Drawing.Point(5, 1);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(215, 284);
            this.pnlContainer.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnShowBar);
            this.splitContainer1.Panel1MinSize = 15;
            this.splitContainer1.Size = new System.Drawing.Size(215, 284);
            this.splitContainer1.SplitterDistance = 15;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnShowBar
            // 
            this.btnShowBar.AutoSize = true;
            this.btnShowBar.BackgroundImage = global::Zamba.ExportBusiness.Properties.Resources.Banner2;
            this.btnShowBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnShowBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnShowBar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowBar.Location = new System.Drawing.Point(0, 0);
            this.btnShowBar.Name = "btnShowBar";
            this.btnShowBar.Size = new System.Drawing.Size(15, 284);
            this.btnShowBar.TabIndex = 2;
            this.btnShowBar.UseVisualStyleBackColor = true;
            // 
            // PanelContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.pnlContainer);
            this.Name = "PanelContainer";
            this.Size = new System.Drawing.Size(232, 286);
            this.Load += new System.EventHandler(this.BarControlContainer_Load);
            this.pnlContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        internal System.Windows.Forms.SplitContainer splitContainer1;
        internal ZButton btnShowBar;

    }
}