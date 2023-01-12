using Zamba.Filters;


namespace Zamba.Grid
{
    partial class GroupGridTest
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

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupGridTest));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pageGroupGrid = new Zamba.Grid.PageGroupGrid.PageGroupGrid(CurrentUserId,ref _gridController);
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 414);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(696, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pageGroupGrid
            // 
            this.pageGroupGrid.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pageGroupGrid.DataSource = ((object)(resources.GetObject("pageGroupGrid.DataSource")));
            this.pageGroupGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageGroupGrid.Location = new System.Drawing.Point(0, 0);
            this.pageGroupGrid.Name = "pageGroupGrid";
            this.pageGroupGrid.Size = new System.Drawing.Size(696, 414);
            this.pageGroupGrid.TabIndex = 4;
            // 
            // GroupGridTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(696, 436);
            this.Controls.Add(this.pageGroupGrid);
            this.Controls.Add(this.statusStrip1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GroupGridTest";
            this.Text = "Reporte";
            //this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private Zamba.Grid.PageGroupGrid.PageGroupGrid pageGroupGrid;
    }
}

