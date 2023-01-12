namespace Zamba.Shapes.Views
{
    partial class UcDiagrams
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem5 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem6 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem7 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem8 = new Telerik.WinControls.UI.RadListDataItem();
            this.rpvDiagrams = new Telerik.WinControls.UI.RadPageView();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.lblview = new Telerik.WinControls.UI.CommandBarLabel();
            this.ddviews = new Telerik.WinControls.UI.CommandBarDropDownList();
            this.commandBarButton1 = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarButton2 = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarButton3 = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarSeparator1 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarButton4 = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarButton5 = new Telerik.WinControls.UI.CommandBarButton();
            this.radCommandBar1 = new Telerik.WinControls.UI.RadCommandBar();
            ((System.ComponentModel.ISupportInitialize)(this.rpvDiagrams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // rpvDiagrams
            // 
            this.rpvDiagrams.BackColor = System.Drawing.Color.White;
            this.rpvDiagrams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvDiagrams.ForeColor = System.Drawing.Color.FromArgb(76,76,76);
            this.rpvDiagrams.Location = new System.Drawing.Point(0, 36);
            this.rpvDiagrams.Name = "rpvDiagrams";
            // 
            // 
            // 
            this.rpvDiagrams.RootElement.ForeColor = System.Drawing.Color.FromArgb(76,76,76);
            this.rpvDiagrams.Size = new System.Drawing.Size(552, 348);
            this.rpvDiagrams.TabIndex = 1;
            this.rpvDiagrams.Text = "radPageView1";
            this.rpvDiagrams.PageRemoving += new System.EventHandler<Telerik.WinControls.UI.RadPageViewCancelEventArgs>(this.radPageView1_PageRemoving);
            this.rpvDiagrams.SelectedPageChanged += new System.EventHandler(this.rpvDiagrams_SelectedPageChanged);
            // 
            // commandBarRowElement1
            // 
            this.commandBarRowElement1.BorderDrawMode = Telerik.WinControls.BorderDrawModes.HorizontalOverVertical;
            this.commandBarRowElement1.BorderLeftShadowColor = System.Drawing.Color.Empty;
            this.commandBarRowElement1.DisplayName = null;
            this.commandBarRowElement1.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement1.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement1});
            this.commandBarRowElement1.Text = "";
            // 
            // commandBarStripElement1
            // 
            this.commandBarStripElement1.BorderDrawMode = Telerik.WinControls.BorderDrawModes.HorizontalOverVertical;
            this.commandBarStripElement1.BorderLeftShadowColor = System.Drawing.Color.Empty;
            this.commandBarStripElement1.DisplayName = "commandBarStripElement1";
            this.commandBarStripElement1.FloatingForm = null;
            this.commandBarStripElement1.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.lblview,
            this.ddviews,
            this.commandBarButton1,
            this.commandBarButton2,
            this.commandBarButton3,
            this.commandBarSeparator1,
            this.commandBarButton4,
            this.commandBarButton5});
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            this.commandBarStripElement1.Text = "";
            // 
            // lblview
            // 
            this.lblview.AccessibleDescription = "Visualizacion:";
            this.lblview.AccessibleName = "Visualizacion:";
            this.lblview.BorderDrawMode = Telerik.WinControls.BorderDrawModes.HorizontalOverVertical;
            this.lblview.BorderLeftShadowColor = System.Drawing.Color.Empty;
            this.lblview.DisplayName = "commandBarLabel1";
            this.lblview.Name = "lblview";
            this.lblview.Text = "Visualizacion:";
            this.lblview.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.lblview.VisibleInOverflowMenu = true;
            // 
            // ddviews
            // 
            this.ddviews.AccessibleDescription = "Arbol";
            this.ddviews.AccessibleName = "Arbol";
            this.ddviews.BorderDrawMode = Telerik.WinControls.BorderDrawModes.HorizontalOverVertical;
            this.ddviews.BorderLeftShadowColor = System.Drawing.Color.Empty;
            this.ddviews.DisplayName = "ddviews";
            this.ddviews.DropDownAnimationEnabled = true;
            radListDataItem1.Selected = true;
            radListDataItem1.Tag = "TreeLayout";
            radListDataItem1.Text = "Arbol";
            radListDataItem1.TextWrap = true;
            radListDataItem2.Tag = "FlowchartLayout";
            radListDataItem2.Text = "Flujograma";
            radListDataItem2.TextWrap = true;
            radListDataItem3.Tag = "HierarchicalLayout";
            radListDataItem3.Text = "Jerarquia";
            radListDataItem3.TextWrap = true;
            radListDataItem4.Tag = "LayeredLayout";
            radListDataItem4.Text = "Capas";
            radListDataItem4.TextWrap = true;
            radListDataItem5.Tag = "CircularLayout";
            radListDataItem5.Text = "Circular";
            radListDataItem5.TextWrap = true;
            radListDataItem6.Tag = "GridLayout";
            radListDataItem6.Text = "Grilla";
            radListDataItem6.TextWrap = true;
            radListDataItem7.Tag = "SpringLayour";
            radListDataItem7.Text = "Nodos";
            radListDataItem7.TextWrap = true;
            radListDataItem8.Tag = "SwinLane";
            radListDataItem8.Text = "Lineas";
            radListDataItem8.TextWrap = true;
            this.ddviews.Items.Add(radListDataItem1);
            this.ddviews.Items.Add(radListDataItem2);
            this.ddviews.Items.Add(radListDataItem3);
            this.ddviews.Items.Add(radListDataItem4);
            this.ddviews.Items.Add(radListDataItem5);
            this.ddviews.Items.Add(radListDataItem6);
            this.ddviews.Items.Add(radListDataItem7);
            this.ddviews.Items.Add(radListDataItem8);
            this.ddviews.Name = "ddviews";
            this.ddviews.Text = "Arbol";
            this.ddviews.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.ddviews.VisibleInOverflowMenu = true;
            this.ddviews.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddviews_SelectedIndexChanged);
            // 
            // commandBarButton1
            // 
            this.commandBarButton1.AccessibleDescription = "commandBarButton1";
            this.commandBarButton1.AccessibleName = "commandBarButton1";
            this.commandBarButton1.BorderDrawMode = Telerik.WinControls.BorderDrawModes.HorizontalOverVertical;
            this.commandBarButton1.BorderLeftShadowColor = System.Drawing.Color.Empty;
            this.commandBarButton1.DisplayName = "commandBarButton1";
            this.commandBarButton1.Image = global::Zamba.Shapes.Properties.Resources.pdfIcon;
            this.commandBarButton1.ImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.commandBarButton1.MaxSize = new System.Drawing.Size(32, 32);
            this.commandBarButton1.MinSize = new System.Drawing.Size(32, 32);
            this.commandBarButton1.Name = "commandBarButton1";
            this.commandBarButton1.Text = "commandBarButton1";
            this.commandBarButton1.ToolTipText = "Exportar a PDF";
            this.commandBarButton1.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.commandBarButton1.VisibleInOverflowMenu = true;
            this.commandBarButton1.Click += new System.EventHandler(this.commandBarButton1_Click_1);
            // 
            // commandBarButton2
            // 
            this.commandBarButton2.AccessibleDescription = "commandBarButton2";
            this.commandBarButton2.AccessibleName = "commandBarButton2";
            this.commandBarButton2.BorderDrawMode = Telerik.WinControls.BorderDrawModes.HorizontalOverVertical;
            this.commandBarButton2.BorderLeftShadowColor = System.Drawing.Color.Empty;
            this.commandBarButton2.DisplayName = "commandBarButton2";
            this.commandBarButton2.Image = global::Zamba.Shapes.Properties.Resources.svg_icon;
            this.commandBarButton2.ImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.commandBarButton2.MaxSize = new System.Drawing.Size(32, 32);
            this.commandBarButton2.MinSize = new System.Drawing.Size(32, 32);
            this.commandBarButton2.Name = "commandBarButton2";
            this.commandBarButton2.Text = "commandBarButton2";
            this.commandBarButton2.ToolTipText = "Exportar a SVG";
            this.commandBarButton2.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.commandBarButton2.VisibleInOverflowMenu = true;
            this.commandBarButton2.Click += new System.EventHandler(this.commandBarButton2_Click);
            // 
            // commandBarButton3
            // 
            this.commandBarButton3.AccessibleDescription = "commandBarButton3";
            this.commandBarButton3.AccessibleName = "commandBarButton3";
            this.commandBarButton3.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.FitToAvailableSize;
            this.commandBarButton3.BorderDrawMode = Telerik.WinControls.BorderDrawModes.HorizontalOverVertical;
            this.commandBarButton3.BorderLeftShadowColor = System.Drawing.Color.Empty;
            this.commandBarButton3.DisplayName = "commandBarButton3";
            this.commandBarButton3.Image = global::Zamba.Shapes.Properties.Resources.Image_20PNG_01;
            this.commandBarButton3.ImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.commandBarButton3.MaxSize = new System.Drawing.Size(32, 32);
            this.commandBarButton3.MinSize = new System.Drawing.Size(32, 32);
            this.commandBarButton3.Name = "commandBarButton3";
            this.commandBarButton3.Text = "commandBarButton3";
            this.commandBarButton3.ToolTipText = "Exportar a PNG";
            this.commandBarButton3.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.commandBarButton3.VisibleInOverflowMenu = true;
            this.commandBarButton3.Click += new System.EventHandler(this.commandBarButton3_Click);
            // 
            // commandBarSeparator1
            // 
            this.commandBarSeparator1.AccessibleDescription = "commandBarSeparator1";
            this.commandBarSeparator1.AccessibleName = "commandBarSeparator1";
            this.commandBarSeparator1.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator1.Name = "commandBarSeparator1";
            this.commandBarSeparator1.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.commandBarSeparator1.VisibleInOverflowMenu = false;
            // 
            // commandBarButton4
            // 
            this.commandBarButton4.AccessibleDescription = "commandBarButton4";
            this.commandBarButton4.AccessibleName = "commandBarButton4";
            this.commandBarButton4.DisplayName = "commandBarButton4";
            this.commandBarButton4.Image = global::Zamba.Shapes.Properties.Resources.zoom_in;
            this.commandBarButton4.Name = "commandBarButton4";
            this.commandBarButton4.Text = "commandBarButton4";
            this.commandBarButton4.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.commandBarButton4.VisibleInOverflowMenu = true;
            this.commandBarButton4.Click += new System.EventHandler(this.commandBarButton4_Click);
            // 
            // commandBarButton5
            // 
            this.commandBarButton5.AccessibleDescription = "commandBarButton5";
            this.commandBarButton5.AccessibleName = "commandBarButton5";
            this.commandBarButton5.DisplayName = "commandBarButton5";
            this.commandBarButton5.Image = global::Zamba.Shapes.Properties.Resources.zoom_out;
            this.commandBarButton5.Name = "commandBarButton5";
            this.commandBarButton5.Text = "commandBarButton5";
            this.commandBarButton5.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.commandBarButton5.VisibleInOverflowMenu = true;
            this.commandBarButton5.Click += new System.EventHandler(this.commandBarButton5_Click);
            // 
            // radCommandBar1
            // 
            this.radCommandBar1.AutoSize = true;
            this.radCommandBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radCommandBar1.Location = new System.Drawing.Point(0, 0);
            this.radCommandBar1.Name = "radCommandBar1";
            this.radCommandBar1.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement1});
            this.radCommandBar1.Size = new System.Drawing.Size(552, 36);
            this.radCommandBar1.TabIndex = 2;
            this.radCommandBar1.Text = "radCommandBar1";
            // 
            // UcDiagrams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rpvDiagrams);
            this.Controls.Add(this.radCommandBar1);
            this.Name = "UcDiagrams";
            this.Size = new System.Drawing.Size(552, 384);
            ((System.ComponentModel.ISupportInitialize)(this.rpvDiagrams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadPageView rpvDiagrams;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private Telerik.WinControls.UI.CommandBarLabel lblview;
        private Telerik.WinControls.UI.CommandBarDropDownList ddviews;
        private Telerik.WinControls.UI.CommandBarButton commandBarButton1;
        private Telerik.WinControls.UI.CommandBarButton commandBarButton2;
        private Telerik.WinControls.UI.CommandBarButton commandBarButton3;
        private Telerik.WinControls.UI.RadCommandBar radCommandBar1;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator1;
        private Telerik.WinControls.UI.CommandBarButton commandBarButton4;
        private Telerik.WinControls.UI.CommandBarButton commandBarButton5;


    }
}
