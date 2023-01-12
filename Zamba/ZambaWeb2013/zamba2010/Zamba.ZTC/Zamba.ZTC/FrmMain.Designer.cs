using System.Windows.Forms;
using Zamba.AppBlock;

namespace Zamba.ZTC
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.radPageView1 = new Telerik.WinControls.UI.RadPageView();
            this.TabTC = new Telerik.WinControls.UI.RadPageViewItemPage();
            this.splitGrid = new System.Windows.Forms.SplitContainer();
            this.ZToolBar1 = new ZToolBar();
            this.btnRefreshGrid = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbExportTypes = new System.Windows.Forms.ToolStripComboBox();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lbl = new System.Windows.Forms.ToolStripLabel();
            this.MasterTemplate = new Telerik.WinControls.UI.RadGridView();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.commandBarStripElement2 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.commandBarSeparator5 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarRowElement2 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement3 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.ZToolBarMain = new ZToolBar();
            this.lblProyecto = new System.Windows.Forms.ToolStripLabel();
            this.cmbProjects = new System.Windows.Forms.ToolStripComboBox();
            this.commandBarSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.commandBarLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbTypes = new System.Windows.Forms.ToolStripComboBox();
            this.commandBarSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblobjetos = new System.Windows.Forms.ToolStripLabel();
            this.cmbObjects = new System.Windows.Forms.ToolStripComboBox();
            this.commandBarLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtObject = new ToolStripTextBox();
            this.btnNewTestCase = new System.Windows.Forms.ToolStripButton();
            this.commandBarSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.commandBarButton1 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).BeginInit();
            this.radPageView1.SuspendLayout();
            this.TabTC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGrid)).BeginInit();
            this.splitGrid.Panel1.SuspendLayout();
            this.splitGrid.Panel2.SuspendLayout();
            this.splitGrid.SuspendLayout();
            this.ZToolBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MasterTemplate.MasterTemplate)).BeginInit();
            this.ZToolBarMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // radPageView1
            // 
            this.radPageView1.Controls.Add(this.TabTC);
            this.radPageView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPageView1.Location = new System.Drawing.Point(0, 25);
            this.radPageView1.Name = "radPageView1";
            this.radPageView1.SelectedPage = this.TabTC;
            this.radPageView1.Size = new System.Drawing.Size(994, 457);
            this.radPageView1.TabIndex = 1;
            this.radPageView1.Text = "radPageView1";
            this.radPageView1.PageRemoving += new System.EventHandler<Telerik.WinControls.UI.RadPageViewCancelEventArgs>(this.RadPageView1PageRemoving);
            // 
            // TabTC
            // 
            this.TabTC.Controls.Add(this.splitGrid);
            this.TabTC.ItemType = Telerik.WinControls.UI.PageViewItemType.GroupHeaderItem;
            this.TabTC.Location = new System.Drawing.Point(10, 37);
            this.TabTC.Name = "TabTC";
            this.TabTC.Size = new System.Drawing.Size(973, 409);
            this.TabTC.Text = "Casos de Prueba";
            // 
            // splitGrid
            // 
            this.splitGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGrid.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitGrid.Location = new System.Drawing.Point(0, 0);
            this.splitGrid.Name = "splitGrid";
            this.splitGrid.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitGrid.Panel1
            // 
            this.splitGrid.Panel1.Controls.Add(this.ZToolBar1);
            // 
            // splitGrid.Panel2
            // 
            this.splitGrid.Panel2.Controls.Add(this.MasterTemplate);
            this.splitGrid.Size = new System.Drawing.Size(973, 409);
            this.splitGrid.SplitterDistance = 25;
            this.splitGrid.SplitterWidth = 1;
            this.splitGrid.TabIndex = 1;
            // 
            // ZToolBar1
            // 
            this.ZToolBar1.Items.AddRange(new ToolStripItem[] {
            this.btnRefreshGrid,
            this.ToolStripSeparator1,
            this.ToolStripLabel1,
            this.cmbExportTypes,
            this.btnExport,
            this.ToolStripSeparator2,
            this.lbl});
            this.ZToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ZToolBar1.Name = "ZToolBar1";
            this.ZToolBar1.Size = new System.Drawing.Size(973, 25);
            this.ZToolBar1.TabIndex = 0;
            this.ZToolBar1.Text = "ZToolBarAux";
            // 
            // btnRefreshGrid
            // 
            this.btnRefreshGrid.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnRefreshGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshGrid.Image")));
            this.btnRefreshGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshGrid.Name = "btnRefreshGrid";
            this.btnRefreshGrid.Size = new System.Drawing.Size(23, 22);
            this.btnRefreshGrid.Text = "Actualizar Casos de Prueba";
            this.btnRefreshGrid.Click += new System.EventHandler(this.BtnRefreshGridClick);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripLabel1
            // 
            this.ToolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ToolStripLabel1.Name = "ToolStripLabel1";
            this.ToolStripLabel1.Size = new System.Drawing.Size(67, 22);
            this.ToolStripLabel1.Text = "Exportar a:";
            // 
            // cmbExportTypes
            // 
            this.cmbExportTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExportTypes.Name = "cmbExportTypes";
            this.cmbExportTypes.Size = new System.Drawing.Size(80, 25);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(23, 22);
            this.btnExport.Text = "Exportar";
            this.btnExport.ToolTipText = "Exportar";
            this.btnExport.Click += new System.EventHandler(this.BtnExportClick);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lbl
            // 
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(333, 22);
            this.lbl.Text = "Para abrir un caso de prueba, haga doble click sobre el mismo";
            // 
            // MasterTemplate
            // 
            this.MasterTemplate.AutoScroll = true;
            this.MasterTemplate.AutoSizeRows = true;
            this.MasterTemplate.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MasterTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MasterTemplate.Location = new System.Drawing.Point(0, 0);
            // 
            // MasterTemplate
            // 
            this.MasterTemplate.MasterTemplate.AllowAddNewRow = false;
            this.MasterTemplate.MasterTemplate.AllowDeleteRow = false;
            this.MasterTemplate.MasterTemplate.AllowEditRow = false;
            this.MasterTemplate.MasterTemplate.AutoExpandGroups = true;
            this.MasterTemplate.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.MasterTemplate.MasterTemplate.EnableAlternatingRowColor = true;
            this.MasterTemplate.MasterTemplate.EnableFiltering = true;
            this.MasterTemplate.Name = "MasterTemplate";
            this.MasterTemplate.ReadOnly = true;
            this.MasterTemplate.Size = new System.Drawing.Size(973, 383);
            this.MasterTemplate.TabIndex = 0;
            this.MasterTemplate.Text = "MasterTemplate";
            this.MasterTemplate.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.MasterTemplateCellFormatting);
            this.MasterTemplate.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.MasterTemplateCellDoubleClick);
            this.MasterTemplate.ToolTipTextNeeded += new Telerik.WinControls.ToolTipTextNeededEventHandler(this.RadGridView1ToolTipTextNeeded);
            // 
            // commandBarRowElement1
            // 
            this.commandBarRowElement1.BorderDrawMode = Telerik.WinControls.BorderDrawModes.HorizontalOverVertical;
            this.commandBarRowElement1.BorderLeftShadowColor = System.Drawing.Color.Empty;
            this.commandBarRowElement1.DisplayName = null;
            this.commandBarRowElement1.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement1.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement1,
            this.commandBarStripElement2});
            this.commandBarRowElement1.Text = "";
            // 
            // commandBarStripElement1
            // 
            this.commandBarStripElement1.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.Auto;
            this.commandBarStripElement1.BorderDrawMode = Telerik.WinControls.BorderDrawModes.HorizontalOverVertical;
            this.commandBarStripElement1.BorderLeftShadowColor = System.Drawing.Color.Empty;
            this.commandBarStripElement1.DisplayName = "commandBarStripElement1";
            this.commandBarStripElement1.EnableDragging = false;
            this.commandBarStripElement1.FloatingForm = null;
            this.commandBarStripElement1.Text = "";
            // 
            // commandBarStripElement2
            // 
            this.commandBarStripElement2.DisplayName = "commandBarStripElement2";
            this.commandBarStripElement2.FloatingForm = null;
            this.commandBarStripElement2.Text = "";
            // 
            // ToolStripButton1
            // 
            this.ToolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.ToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton1.Image")));
            this.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton1.Name = "ToolStripButton1";
            this.ToolStripButton1.Size = new System.Drawing.Size(23, 23);
            this.ToolStripButton1.Text = "ToolStripButton1";
            // 
            // commandBarSeparator5
            // 
            this.commandBarSeparator5.AccessibleDescription = "commandBarSeparator4";
            this.commandBarSeparator5.AccessibleName = "commandBarSeparator4";
            this.commandBarSeparator5.BorderDrawMode = Telerik.WinControls.BorderDrawModes.HorizontalOverVertical;
            this.commandBarSeparator5.BorderLeftShadowColor = System.Drawing.Color.Empty;
            this.commandBarSeparator5.DisplayName = "commandBarSeparator4";
            this.commandBarSeparator5.Name = "commandBarSeparator5";
            this.commandBarSeparator5.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.commandBarSeparator5.VisibleInOverflowMenu = false;
            // 
            // commandBarRowElement2
            // 
            this.commandBarRowElement2.DisplayName = null;
            this.commandBarRowElement2.MinSize = new System.Drawing.Size(25, 25);
            // 
            // commandBarStripElement3
            // 
            this.commandBarStripElement3.DisplayName = "commandBarStripElement3";
            this.commandBarStripElement3.FloatingForm = null;
            this.commandBarStripElement3.Name = "commandBarStripElement3";
            this.commandBarStripElement3.Text = "";
            // 
            // ZToolBarMain
            // 
            this.ZToolBarMain.Items.AddRange(new ToolStripItem[] {
            this.lblProyecto,
            this.cmbProjects,
            this.commandBarSeparator1,
            this.commandBarLabel1,
            this.cmbTypes,
            this.commandBarSeparator2,
            this.lblobjetos,
            this.cmbObjects,
            this.commandBarLabel2,
            this.txtObject,
            this.btnNewTestCase,
            this.commandBarSeparator3,
            this.commandBarButton1});
            this.ZToolBarMain.Location = new System.Drawing.Point(0, 0);
            this.ZToolBarMain.Name = "ZToolBarMain";
            this.ZToolBarMain.Size = new System.Drawing.Size(994, 25);
            this.ZToolBarMain.TabIndex = 3;
            // 
            // lblProyecto
            // 
            this.lblProyecto.AccessibleDescription = "Proyecto:";
            this.lblProyecto.AccessibleName = "Proyecto:";
            this.lblProyecto.Name = "lblProyecto";
            this.lblProyecto.Size = new System.Drawing.Size(57, 22);
            this.lblProyecto.Text = "Proyecto:";
            // 
            // cmbProjects
            // 
            this.cmbProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProjects.Name = "cmbProjects";
            this.cmbProjects.Size = new System.Drawing.Size(121, 25);
            this.cmbProjects.SelectedIndexChanged += new System.EventHandler(this.CmbProjectsSelectedIndexChanged);
            // 
            // commandBarSeparator1
            // 
            this.commandBarSeparator1.AccessibleDescription = "commandBarSeparator1";
            this.commandBarSeparator1.AccessibleName = "commandBarSeparator1";
            this.commandBarSeparator1.Name = "commandBarSeparator1";
            this.commandBarSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // commandBarLabel1
            // 
            this.commandBarLabel1.AccessibleDescription = "Tipos:";
            this.commandBarLabel1.AccessibleName = "Tipos:";
            this.commandBarLabel1.Name = "commandBarLabel1";
            this.commandBarLabel1.Size = new System.Drawing.Size(39, 22);
            this.commandBarLabel1.Text = "Tipos:";
            // 
            // cmbTypes
            // 
            this.cmbTypes.AutoSize = false;
            this.cmbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypes.MaxDropDownItems = 20;
            this.cmbTypes.Name = "cmbTypes";
            this.cmbTypes.Size = new System.Drawing.Size(121, 23);
            this.cmbTypes.SelectedIndexChanged += new System.EventHandler(this.CmbTypesSelectedIndexChanged);
            // 
            // commandBarSeparator2
            // 
            this.commandBarSeparator2.AccessibleDescription = "commandBarSeparator2";
            this.commandBarSeparator2.AccessibleName = "commandBarSeparator2";
            this.commandBarSeparator2.Name = "commandBarSeparator2";
            this.commandBarSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lblobjetos
            // 
            this.lblobjetos.AccessibleDescription = "Objetos: ";
            this.lblobjetos.AccessibleName = "Objetos: ";
            this.lblobjetos.Name = "lblobjetos";
            this.lblobjetos.Size = new System.Drawing.Size(54, 22);
            this.lblobjetos.Text = "Objetos: ";
            // 
            // cmbObjects
            // 
            this.cmbObjects.AutoSize = false;
            this.cmbObjects.BackColor = System.Drawing.SystemColors.Window;
            this.cmbObjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObjects.MaxDropDownItems = 20;
            this.cmbObjects.Name = "cmbObjects";
            this.cmbObjects.Size = new System.Drawing.Size(150, 23);
            this.cmbObjects.SelectedIndexChanged += new System.EventHandler(this.CmbObjectsSelectedIndexChanged);
            // 
            // commandBarLabel2
            // 
            this.commandBarLabel2.AccessibleDescription = "Id:";
            this.commandBarLabel2.AccessibleName = "Id:";
            this.commandBarLabel2.Name = "commandBarLabel2";
            this.commandBarLabel2.Size = new System.Drawing.Size(59, 22);
            this.commandBarLabel2.Text = "Id Objeto:";
            // 
            // txtObject
            // 
            this.txtObject.Name = "txtObject";
            this.txtObject.Size = new System.Drawing.Size(100, 25);
            // 
            // btnNewTestCase
            // 
            this.btnNewTestCase.AccessibleDescription = "Ir";
            this.btnNewTestCase.AccessibleName = "Ir";
            this.btnNewTestCase.Name = "btnNewTestCase";
            this.btnNewTestCase.Size = new System.Drawing.Size(102, 22);
            this.btnNewTestCase.Text = "Ir a Set de Prueba";
            this.btnNewTestCase.Click += new System.EventHandler(this.BtnNewTestCaseClick);
            // 
            // commandBarSeparator3
            // 
            this.commandBarSeparator3.AccessibleDescription = "commandBarSeparator4";
            this.commandBarSeparator3.AccessibleName = "commandBarSeparator4";
            this.commandBarSeparator3.Name = "commandBarSeparator3";
            this.commandBarSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // commandBarButton1
            // 
            this.commandBarButton1.AccessibleDescription = "hace cosas";
            this.commandBarButton1.AccessibleName = "cosa";
            this.commandBarButton1.AccessibleRole = System.Windows.Forms.AccessibleRole.Column;
            this.commandBarButton1.Image = ((System.Drawing.Image)(resources.GetObject("commandBarButton1.Image")));
            this.commandBarButton1.Name = "commandBarButton1";
            this.commandBarButton1.Size = new System.Drawing.Size(113, 22);
            this.commandBarButton1.Text = "Generar Informe";
            this.commandBarButton1.ToolTipText = "Generar Informe";
            this.commandBarButton1.Click += new System.EventHandler(this.CommandBarButton1Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 482);
            this.Controls.Add(this.radPageView1);
            this.Controls.Add(this.ZToolBarMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zamba Casos de Prueba";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMainFormClosed);
            this.Load += new System.EventHandler(this.FrmTestLoad);
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).EndInit();
            this.radPageView1.ResumeLayout(false);
            this.TabTC.ResumeLayout(false);
            this.splitGrid.Panel1.ResumeLayout(false);
            this.splitGrid.Panel1.PerformLayout();
            this.splitGrid.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGrid)).EndInit();
            this.splitGrid.ResumeLayout(false);
            this.ZToolBar1.ResumeLayout(false);
            this.ZToolBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MasterTemplate.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MasterTemplate)).EndInit();
            this.ZToolBarMain.ResumeLayout(false);
            this.ZToolBarMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadPageView radPageView1;
        private Telerik.WinControls.UI.RadPageViewItemPage TabTC;
        private System.Windows.Forms.SplitContainer splitGrid;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private System.Windows.Forms.ToolStripButton ToolStripButton1;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator5;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement2;
        private Telerik.WinControls.UI.RadGridView MasterTemplate;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement2;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement3;
        private ZToolBar ZToolBar1;
        private System.Windows.Forms.ToolStripButton btnRefreshGrid;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel ToolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmbExportTypes;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lbl;
        private ZToolBar ZToolBarMain;
        private ToolStripLabel lblProyecto;
        private ToolStripComboBox cmbProjects;
        private ToolStripSeparator commandBarSeparator1;
        private ToolStripLabel commandBarLabel1;
        private ToolStripComboBox cmbTypes;
        private ToolStripSeparator commandBarSeparator2;
        private ToolStripLabel lblobjetos;
        private ToolStripComboBox cmbObjects;
        private ToolStripLabel commandBarLabel2;
        private ToolStripTextBox txtObject;
        private ToolStripSeparator commandBarSeparator3;
        private ToolStripButton btnNewTestCase;
        private ToolStripButton commandBarButton1;
    }
}
