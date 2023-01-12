using System.Windows.Forms;
using Zamba.AppBlock;

namespace Zamba.ZChromium
{
    partial class GraphViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphViewer));
            this.ZToolBar1 = new ZToolBar();
            this.ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.ZToolBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ZToolBar1
            // 
            this.ZToolBar1.Items.AddRange(new ToolStripItem[] {
            this.ToolStripButton1,
            this.ToolStripButton2});
            this.ZToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ZToolBar1.Name = "ZToolBar1";
            this.ZToolBar1.Size = new System.Drawing.Size(1274, 25);
            this.ZToolBar1.TabIndex = 0;
            this.ZToolBar1.Text = "ZToolBar1";
            this.ZToolBar1.GripStyle = ToolStripGripStyle.Hidden;
            this.ZToolBar1.Renderer = new Zamba.AppBlock.MyStripRender();
            // 
            // ToolStripButton1
            // 
            this.ToolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.ToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton1.Image")));
            this.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton1.Name = "ToolStripButton1";
            this.ToolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButton1.Text = "ToolStripButton1";
            this.ToolStripButton1.Click += new System.EventHandler(this.ToolStripButton1_Click);
            // 
            // ToolStripButton2
            // 
            this.ToolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.ToolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton2.Image")));
            this.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton2.Name = "ToolStripButton2";
            this.ToolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButton2.Text = "ToolStripButton2";
            // 
            // GraphViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.ZToolBar1);
            this.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.Name = "GraphViewer";
            this.Size = new System.Drawing.Size(1274, 848);
            this.ZToolBar1.ResumeLayout(false);
            this.ZToolBar1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZToolBar ZToolBar1;
        private System.Windows.Forms.ToolStripButton ToolStripButton1;
        private System.Windows.Forms.ToolStripButton ToolStripButton2;
    }
}