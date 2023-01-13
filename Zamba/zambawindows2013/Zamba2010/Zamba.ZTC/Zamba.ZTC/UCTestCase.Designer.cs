using System;
using System.Windows.Forms;

namespace Zamba.ZTC
{
    partial class UcTestCase
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcTestCase));
            this.ctrlTestCase = new Telerik.WinControls.UI.RadPageView();
            this.radPageViewPage1 = new Telerik.WinControls.UI.RadPageViewPage();
            this.ucTestCaseDescription1 = new Zamba.ZTC.UCTestCaseDescription(_rEdit, _rCreate, _rDelete, _rExecute);
            this.radPageViewPage3 = new Telerik.WinControls.UI.RadPageViewPage();
            this.ucTestCaseExecutionHistory1 = new Zamba.ZTC.UCTestCaseExecutionHistory();
            this.radPageViewPage2 = new Telerik.WinControls.UI.RadPageViewPage();
            this.ucTestCaseNewExecution1 = new Zamba.ZTC.UCTestCaseNewExecution(_rExecute);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.rtvTestCase = new Telerik.WinControls.UI.RadTreeView();
            this.radContextMenu = new Telerik.WinControls.UI.RadContextMenu(this.components);
            this.mnuCategoryHeader = new Telerik.WinControls.UI.RadMenuHeaderItem();
            this.mnuAddCategory = new Telerik.WinControls.UI.RadMenuItem();
            this.mnuCategoryName = new Telerik.WinControls.UI.RadMenuItem();
            this.mnuDeleteCategory = new Telerik.WinControls.UI.RadMenuItem();
            this.mnuTestCaseHeader = new Telerik.WinControls.UI.RadMenuHeaderItem();
            this.mnuAddTestCase = new Telerik.WinControls.UI.RadMenuItem();
            this.mnuCopyTestCase = new Telerik.WinControls.UI.RadMenuItem();
            this.mnuCutTestCase = new Telerik.WinControls.UI.RadMenuItem();
            this.mnuPasteTestCase = new Telerik.WinControls.UI.RadMenuItem();
            this.mnuDeleteTestCase = new Telerik.WinControls.UI.RadMenuItem();
            this.radCommandBar1 = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnnewcategory = new Telerik.WinControls.UI.CommandBarButton();
            this.btnNewTC = new Telerik.WinControls.UI.CommandBarButton();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlTestCase)).BeginInit();
            this.ctrlTestCase.SuspendLayout();
            this.radPageViewPage1.SuspendLayout();
            this.radPageViewPage3.SuspendLayout();
            this.radPageViewPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtvTestCase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlTestCase
            // 
            this.ctrlTestCase.AutoSize = false;
            this.ctrlTestCase.BackColor = System.Drawing.Color.White;
            this.ctrlTestCase.Controls.Add(this.radPageViewPage1);
            this.ctrlTestCase.Controls.Add(this.radPageViewPage3);
            this.ctrlTestCase.Controls.Add(this.radPageViewPage2);
            this.ctrlTestCase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlTestCase.ForeColor = System.Drawing.Color.FromArgb(76,76,76);
            this.ctrlTestCase.Location = new System.Drawing.Point(0, 0);
            this.ctrlTestCase.Name = "ctrlTestCase";
            // 
            // 
            // 
            this.ctrlTestCase.RootElement.ForeColor = System.Drawing.Color.FromArgb(76,76,76);
            this.ctrlTestCase.SelectedPage = this.radPageViewPage2;
            this.ctrlTestCase.Size = new System.Drawing.Size(775, 700);
            this.ctrlTestCase.TabIndex = 0;
            this.ctrlTestCase.ThemeName = "ControlDefault";
            this.ctrlTestCase.Visible = false;
            this.ctrlTestCase.SelectedPageChanged += new System.EventHandler(this.RadPageView1SelectedPageChanged);
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.ctrlTestCase.GetChildAt(0))).ItemContentOrientation = Telerik.WinControls.UI.PageViewContentOrientation.Horizontal;
            // 
            // radPageViewPage1
            // 
            this.radPageViewPage1.Controls.Add(this.ucTestCaseDescription1);
            this.radPageViewPage1.Location = new System.Drawing.Point(4, 4);
            this.radPageViewPage1.Name = "radPageViewPage1";
            this.radPageViewPage1.Size = new System.Drawing.Size(0, 0);
            this.radPageViewPage1.Text = "Caso de prueba";
            // ucTestCaseDescription1
            // 
            this.ucTestCaseDescription1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTestCaseDescription1.Location = new System.Drawing.Point(0, 0);
            this.ucTestCaseDescription1.Name = "ucTestCaseDescription1";
            this.ucTestCaseDescription1.Size = new System.Drawing.Size(0, 0);
            this.ucTestCaseDescription1.TabIndex = 0;
            // 
            // radPageViewPage3
            // 
            this.radPageViewPage3.Controls.Add(this.ucTestCaseExecutionHistory1);
            this.radPageViewPage3.Location = new System.Drawing.Point(4, 4);
            this.radPageViewPage3.Name = "radPageViewPage3";
            this.radPageViewPage3.Size = new System.Drawing.Size(0, 0);
            this.radPageViewPage3.Text = "Historial de Ejecucion Pruebas";
            // 
            // ucTestCaseExecutionHistory1
            // 
            this.ucTestCaseExecutionHistory1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTestCaseExecutionHistory1.Location = new System.Drawing.Point(0, 0);
            this.ucTestCaseExecutionHistory1.Name = "ucTestCaseExecutionHistory1";
            this.ucTestCaseExecutionHistory1.Size = new System.Drawing.Size(0, 0);
            this.ucTestCaseExecutionHistory1.TabIndex = 0;
            // 
            // radPageViewPage2
            // 
            this.radPageViewPage2.Controls.Add(this.ucTestCaseNewExecution1);
            this.radPageViewPage2.Location = new System.Drawing.Point(10, 37);
            this.radPageViewPage2.Name = "radPageViewPage2";
            this.radPageViewPage2.Size = new System.Drawing.Size(754, 652);
            this.radPageViewPage2.Text = "Ejecucion de Caso de Prueba";
            //
            // ucTestCaseNewExecution1
            // 
            this.ucTestCaseNewExecution1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTestCaseNewExecution1.ExecutionSaved = false;
            this.ucTestCaseNewExecution1.Location = new System.Drawing.Point(0, 0);
            this.ucTestCaseNewExecution1.Name = "ucTestCaseNewExecution1";
            this.ucTestCaseNewExecution1.Size = new System.Drawing.Size(754, 652);
            this.ucTestCaseNewExecution1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(150, 100);
            this.splitContainer1.TabIndex = 0;
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.Controls.Add(this.splitPanel1);
            this.radSplitContainer1.Controls.Add(this.splitPanel2);
            this.radSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.radSplitContainer1.Name = "radSplitContainer1";
            // 
            // 
            // 
            this.radSplitContainer1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.radSplitContainer1.Size = new System.Drawing.Size(1000, 700);
            this.radSplitContainer1.TabIndex = 1;
            this.radSplitContainer1.TabStop = false;
            this.radSplitContainer1.Text = "radSplitContainer1";
            // 
            // splitPanel1
            // 
            this.splitPanel1.Controls.Add(this.rtvTestCase);
            this.splitPanel1.Controls.Add(this.radCommandBar1);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(222, 700);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.277332F, 0F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-276, 0);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // rtvTestCase
            // 
            this.rtvTestCase.ChildMember = "TestCaseId";
            this.rtvTestCase.DisplayMember = "NodeDescription";
            this.rtvTestCase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtvTestCase.Location = new System.Drawing.Point(0, 30);
            this.rtvTestCase.Name = "rtvTestCase";
            this.rtvTestCase.ParentMember = "ParentNode";
            this.rtvTestCase.RadContextMenu = this.radContextMenu;
            this.rtvTestCase.ShowLines = true;
            this.rtvTestCase.Size = new System.Drawing.Size(222, 670);
            this.rtvTestCase.SpacingBetweenNodes = -1;
            this.rtvTestCase.TabIndex = 0;
            this.rtvTestCase.Text = "radTreeView1";
            this.rtvTestCase.ValueMember = "TestCaseId";
            this.rtvTestCase.SelectedNodeChanged += new Telerik.WinControls.UI.RadTreeView.RadTreeViewEventHandler(this.RtvCasesSelectedNodeChanged);
            this.rtvTestCase.ContextMenuOpening += new Telerik.WinControls.UI.TreeViewContextMenuOpeningEventHandler(this.RtvTestCaseContextMenuOpening);
            this.rtvTestCase.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RtvTestCaseMouseClick);
            // 
            // radContextMenu
            // 
            this.radContextMenu.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.mnuCategoryHeader,
            this.mnuAddCategory,
            this.mnuCategoryName,
            this.mnuDeleteCategory,
            this.mnuTestCaseHeader,
            this.mnuAddTestCase,
            this.mnuCopyTestCase,
            this.mnuCutTestCase,
            this.mnuPasteTestCase,
            this.mnuDeleteTestCase});
            // 
            // mnuCategoryHeader
            // 
            this.mnuCategoryHeader.AccessibleDescription = "Categorías";
            this.mnuCategoryHeader.AccessibleName = "Categorías";
            this.mnuCategoryHeader.Name = "mnuCategoryHeader";
            this.mnuCategoryHeader.Text = "Categorías";
            this.mnuCategoryHeader.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // mnuAddCategory
            // 
            this.mnuAddCategory.AccessibleDescription = "Agregar";
            this.mnuAddCategory.AccessibleName = "Agregar";
            this.mnuAddCategory.Name = "mnuAddCategory";
            this.mnuAddCategory.Text = "Agregar";
            this.mnuAddCategory.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.mnuAddCategory.Click += new System.EventHandler(this.MnuAddCategoryClick);
            // 
            // mnuCategoryName
            // 
            this.mnuCategoryName.AccessibleDescription = "Cambiar nombre";
            this.mnuCategoryName.AccessibleName = "Cambiar nombre";
            this.mnuCategoryName.Name = "mnuCategoryName";
            this.mnuCategoryName.Text = "Cambiar nombre";
            this.mnuCategoryName.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.mnuCategoryName.Click += new System.EventHandler(this.MnuCategoryNameClick);
            // 
            // mnuDeleteCategory
            // 
            this.mnuDeleteCategory.AccessibleDescription = "Eliminar";
            this.mnuDeleteCategory.AccessibleName = "Eliminar";
            this.mnuDeleteCategory.Name = "mnuDeleteCategory";
            this.mnuDeleteCategory.Text = "Eliminar";
            this.mnuDeleteCategory.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.mnuDeleteCategory.Click += new System.EventHandler(this.MnuDeleteCategoryClick);
            // 
            // mnuTestCaseHeader
            // 
            this.mnuTestCaseHeader.AccessibleDescription = "Casos de prueba";
            this.mnuTestCaseHeader.AccessibleName = "Casos de prueba";
            this.mnuTestCaseHeader.Name = "mnuTestCaseHeader";
            this.mnuTestCaseHeader.Text = "Casos de prueba";
            this.mnuTestCaseHeader.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // mnuAddTestCase
            // 
            this.mnuAddTestCase.AccessibleDescription = "Agregar";
            this.mnuAddTestCase.AccessibleName = "Agregar";
            this.mnuAddTestCase.Name = "mnuAddTestCase";
            this.mnuAddTestCase.Text = "Agregar";
            this.mnuAddTestCase.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.mnuAddTestCase.Click += new System.EventHandler(this.MnuAddCaseClick);
            // 
            // mnuCopyTestCase
            // 
            this.mnuCopyTestCase.AccessibleDescription = "Copiar";
            this.mnuCopyTestCase.AccessibleName = "Copiar";
            this.mnuCopyTestCase.Name = "mnuCopyTestCase";
            this.mnuCopyTestCase.Text = "Copiar";
            this.mnuCopyTestCase.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.mnuCopyTestCase.Click += new System.EventHandler(this.MnuCopyTestCaseClick);
            // 
            // mnuCutTestCase
            // 
            this.mnuCutTestCase.AccessibleDescription = "Cortar";
            this.mnuCutTestCase.AccessibleName = "Cortar";
            this.mnuCutTestCase.Name = "mnuCutTestCase";
            this.mnuCutTestCase.Text = "Cortar";
            this.mnuCutTestCase.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.mnuCutTestCase.Click += new System.EventHandler(this.MnuCutTestCaseClick);
            // 
            // mnuPasteTestCase
            // 
            this.mnuPasteTestCase.AccessibleDescription = "Pegar";
            this.mnuPasteTestCase.AccessibleName = "Pegar";
            this.mnuPasteTestCase.Name = "mnuPasteTestCase";
            this.mnuPasteTestCase.Text = "Pegar";
            this.mnuPasteTestCase.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.mnuPasteTestCase.Click += new System.EventHandler(this.MnuPasteTestCaseClick);
            // 
            // mnuDeleteTestCase
            // 
            this.mnuDeleteTestCase.AccessibleDescription = "Eliminar";
            this.mnuDeleteTestCase.AccessibleName = "Eliminar";
            this.mnuDeleteTestCase.Name = "mnuDeleteTestCase";
            this.mnuDeleteTestCase.Text = "Eliminar";
            this.mnuDeleteTestCase.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.mnuDeleteTestCase.Click += new System.EventHandler(this.MnuDeleteCaseClick);
            // 
            // radCommandBar1
            // 
            this.radCommandBar1.AutoSize = true;
            this.radCommandBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radCommandBar1.Location = new System.Drawing.Point(0, 0);
            this.radCommandBar1.Name = "radCommandBar1";
            this.radCommandBar1.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement1});
            this.radCommandBar1.Size = new System.Drawing.Size(222, 30);
            this.radCommandBar1.TabIndex = 1;
            this.radCommandBar1.Text = "radCommandBar1";
            // 
            // commandBarRowElement1
            // 
            this.commandBarRowElement1.DisplayName = null;
            this.commandBarRowElement1.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement1.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement1});
            // 
            // commandBarStripElement1
            // 
            this.commandBarStripElement1.DisplayName = "commandBarStripElement1";
            this.commandBarStripElement1.FloatingForm = null;
            this.commandBarStripElement1.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.btnnewcategory,
            this.btnNewTC});
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            this.commandBarStripElement1.Text = "";
            // 
            // btnnewcategory
            // 
            this.btnnewcategory.AccessibleDescription = "Nueva Categoria";
            this.btnnewcategory.AccessibleName = "Nueva Categoria";
            this.btnnewcategory.AutoToolTip = true;
            this.btnnewcategory.DisplayName = "btnnewcategory";
            this.btnnewcategory.DrawText = true;
            this.btnnewcategory.Image = null;
            this.btnnewcategory.Name = "btnnewcategory";
            this.btnnewcategory.Text = "Nueva Categoria";
            this.btnnewcategory.ToolTipText = "Nueva Categoria";
            this.btnnewcategory.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.btnnewcategory.VisibleInOverflowMenu = true;
            this.btnnewcategory.Click += new System.EventHandler(this.BtnnewcategoryClick);
            // 
            // btnNewTC
            // 
            this.btnNewTC.AccessibleDescription = "Nuevo Caso de Prueba";
            this.btnNewTC.AccessibleName = "Nuevo Caso de Prueba";
            this.btnNewTC.AutoToolTip = true;
            this.btnNewTC.DisplayName = "btnNewTC";
            this.btnNewTC.DrawText = true;
            this.btnNewTC.Image = null;
            this.btnNewTC.Name = "btnNewTC";
            this.btnNewTC.Text = "Nuevo Caso de Prueba";
            this.btnNewTC.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnNewTC.ToolTipText = "Nuevo Caso de Prueba";
            this.btnNewTC.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.btnNewTC.VisibleInOverflowMenu = true;
            this.btnNewTC.Click += new System.EventHandler(this.BtnNewTcClick);
            // 
            // splitPanel2
            // 
            this.splitPanel2.Controls.Add(this.ctrlTestCase);
            this.splitPanel2.Location = new System.Drawing.Point(225, 0);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(775, 700);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.277332F, 0F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(276, 0);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
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
            // UCTestCase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radSplitContainer1);
            this.Name = "UcTestCase";
            this.Size = new System.Drawing.Size(1000, 700);
            this.Load += new System.EventHandler(this.UcTestCaseLoad);
            ((System.ComponentModel.ISupportInitialize)(this.ctrlTestCase)).EndInit();
            this.ctrlTestCase.ResumeLayout(false);
            this.radPageViewPage1.ResumeLayout(false);
            this.radPageViewPage3.ResumeLayout(false);
            this.radPageViewPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            this.splitPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtvTestCase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.splitPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPageView ctrlTestCase;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage1;
        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.RadTreeView rtvTestCase;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage3;
        private UCTestCaseDescription ucTestCaseDescription1;
        private UCTestCaseExecutionHistory ucTestCaseExecutionHistory1;
        private Telerik.WinControls.UI.RadContextMenu radContextMenu;
        private Telerik.WinControls.UI.RadMenuHeaderItem mnuCategoryHeader;
        private Telerik.WinControls.UI.RadMenuHeaderItem mnuTestCaseHeader;
        private Telerik.WinControls.UI.RadMenuItem mnuAddCategory;
        private Telerik.WinControls.UI.RadMenuItem mnuDeleteCategory;
        private Telerik.WinControls.UI.RadMenuItem mnuAddTestCase;
        private Telerik.WinControls.UI.RadMenuItem mnuCopyTestCase;
        private Telerik.WinControls.UI.RadMenuItem mnuCutTestCase;
        private Telerik.WinControls.UI.RadMenuItem mnuPasteTestCase;
        private Telerik.WinControls.UI.RadMenuItem mnuDeleteTestCase;
        private Telerik.WinControls.UI.RadMenuItem mnuCategoryName;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage2;
        private UCTestCaseNewExecution ucTestCaseNewExecution1;
        private Telerik.WinControls.UI.RadCommandBar radCommandBar1;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private Telerik.WinControls.UI.CommandBarButton btnnewcategory;
        private Telerik.WinControls.UI.CommandBarButton btnNewTC;
        private System.Windows.Forms.ToolStripButton ToolStripButton1;
        private System.Windows.Forms.SplitContainer splitContainer1;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (ucTestCaseDescription1 != null)
                    {
                        ucTestCaseDescription1.TestCaseModified -= UcTestCaseDescription1TestCaseModified;
                        ucTestCaseDescription1.TestCaseNewExecution -= UcTestCaseDescription1TestCaseNewExecution;
                        //ucTestCaseDescription1.Dispose();
                    }
                    if (ucTestCaseExecutionHistory1 != null)
                    {
                        ucTestCaseExecutionHistory1.TestCaseEXSelected -= UcTestCaseExecutionHistory1TestCaseExSelected;
                        //ucTestCaseExecutionHistory1.Dispose();
                    }
                    if (ucTestCaseNewExecution1 != null)
                    {
                        ucTestCaseNewExecution1.TestCaseNewExecutionCanceled -= UcTestCaseNewExecution1TestCaseNewExecutionCanceled;
                        //ucTestCaseNewExecution1.Dispose();
                    }

                    if (_controlsFactory != null)
                    {
                        _controlsFactory.Dispose();
                        _controlsFactory = null;
                    }

                    if (components != null)
                    {
                        components.Dispose();
                    }
                }
                base.Dispose(disposing);
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }
    }
}
