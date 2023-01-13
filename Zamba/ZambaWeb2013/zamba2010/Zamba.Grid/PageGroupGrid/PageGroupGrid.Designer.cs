
using Zamba.Filters;
using Zamba.Grid.Grid;

namespace Zamba.Grid.PageGroupGrid
{
    partial class PageGroupGrid
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.f
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageGroupGrid));
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.view = new GroupGrid(this.withExcel, CurrentUserId,ref GridController);
            this.pageBar = new Zamba.Grid.PageGroupGrid.DataNavigatorPageBar();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(122, 15);
            this.panel2.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.view);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(718, 466);
            this.splitContainer1.SplitterDistance = 437;
            this.splitContainer1.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pageBar);
            this.splitContainer2.Size = new System.Drawing.Size(718, 20);
            this.splitContainer2.SplitterDistance = 592;
            this.splitContainer2.TabIndex = 4;
            // 
            // view
            // 
            this.view.AlwaysFit = false;
            this.view.BackColor = System.Drawing.Color.CornflowerBlue;
            this.view.DataTable = null;
            this.view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view.ForeColor = System.Drawing.Color.Black;
            this.view.GridSort = true;
            this.view.Location = new System.Drawing.Point(0, 0);
            this.view.Name = "view";
            this.view.ShortDateFormat = false;
            this.view.Size = new System.Drawing.Size(718, 437);
            this.view.TabIndex = 0;
            this.view.UseColor = false;
            this.view.UseZamba = false;
            this.view.OnRowClick += new GroupGridRowClickEventHandler(this.view_OnClick);
            this.view.OnDoubleClick += new GroupDoubleGridClickEventHandler(this.view_OnDoubleClick);
            // 
            // pageBar
            // 
            this.pageBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pageBar.BackColor = System.Drawing.Color.Transparent;
            this.pageBar.DataSource = null;
            this.pageBar.ForeColor = System.Drawing.Color.LavenderBlush;
            this.pageBar.Location = new System.Drawing.Point(0, 0);
            this.pageBar.MinimumSize = new System.Drawing.Size(90, 13);
            this.pageBar.Name = "pageBar";
            this.pageBar.Size = new System.Drawing.Size(122, 13);
            this.pageBar.TabIndex = 3;
            this.pageBar.Visible = false;
            this.pageBar.VisiblePageCount = 10;
            // 
            // PageGroupGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Controls.Add(this.splitContainer1);
            this.Name = "PageGroupGrid";
            this.Size = new System.Drawing.Size(718, 466);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public  GroupGrid view;
        //private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DataNavigatorPageBar pageBar;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;

    }
}
