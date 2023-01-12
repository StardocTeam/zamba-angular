using System.Data;
using System;
using Telerik.WinControls.UI;
namespace Zamba.Grid.ZT
    {
    sealed partial class ZTGroupGrid
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Zamba.Grid.ZT.ZTGrid outlookGrid1;

        public void Dispose()
        {
            Dispose(true);
        }

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
                    if (components != null)
                    {
                        components.Dispose();
                        components = null;
                    }
                    if (_dataTable != null)
                    {
                        _dataTable.Dispose();
                        _dataTable = null;
                    }
                    if (LoadedIndexs != null)
                    {
                        LoadedIndexs.Clear();
                        LoadedIndexs = null;
                    }
                    if (outlookGrid1 != null)
                    {
                        this.outlookGrid1.CellDoubleClick -= new GridViewCellEventHandler(this.OutlookGrid1CellDoubleClick);
                        //this.outlookGrid1.ColumnHeaderMouseClick -= new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OutlookGrid1ColumnHeaderMouseClick);
                        this.outlookGrid1.DoubleClick -= new System.EventHandler(this.OutlookGrid1DoubleClick);
                        //this.outlookGrid1.CellMouseDown -= new GridViewCellMouseEventHandler(this.OutlookGrid1CellMouseDown);
                        //this.outlookGrid1.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.OutlookGrid1KeyPress);
                        this.outlookGrid1.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.OutlookGrid1KeyUp);
                        this.outlookGrid1.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.OutlookGrid_KeyDown);
                        this.outlookGrid1.CellClick -= new GridViewCellEventHandler(this.OutlookGrid1CellContentClick);
                        outlookGrid1.Dispose();
                        outlookGrid1 = null;
                    }
                    if (btnAdd != null)
                    {
                        this.btnAdd.Click -= new System.EventHandler(this.BtnAddClick);
                        btnAdd.Dispose();
                        btnAdd = null;
                    }
                    if (btnCollapseAll != null)
                    {
                       // this.btnCollapseAll.Click -= new System.EventHandler(this.BtnCollapseAllClick);
                        btnCollapseAll.Dispose();
                        btnCollapseAll = null;
                    }
                    if (btnExpandAll != null)
                    {
                      //  this.btnExpandAll.Click -= new System.EventHandler(this.BtnExpandAllClick);
                        btnExpandAll.Dispose();
                        btnExpandAll = null;
                    }
                    if (btnExportToExcel != null)
                    {
                        this.btnExportToExcel.Click -= new System.EventHandler(this.BtnExportToExcelClick);
                        btnExportToExcel.Dispose();
                        btnExportToExcel = null;
                    }
                    if (btnfirstpage != null)
                    {
                        this.btnfirstpage.Click -= new System.EventHandler(this.BtnfirstpageClick);
                        btnfirstpage.Dispose();
                        btnfirstpage = null;
                    }
                    if (btnFit != null)
                    {
                        //this.btnFit.Click -= new System.EventHandler(this.TxtFitClick);
                        btnFit.Dispose();
                        btnFit = null;
                    }
                    if (btnGraphic != null)
                    {
                        this.btnGraphic.Click -= new System.EventHandler(this.BtnGraphicClick);
                        btnGraphic.Dispose();
                        btnGraphic = null;
                    }
                    if (btnGroup != null)
                    {
                        //this.btnGroup.Click -= new System.EventHandler(this.BtnGroupClick);
                        btnGroup.Dispose();
                        btnGroup = null;
                    }
                    if (btnHideFilter != null)
                    {
                        this.btnHideFilter.Click -= new System.EventHandler(this.HideClick);
                        btnHideFilter.Dispose();
                        btnHideFilter = null;
                    }

                    if (btnlastpage != null)
                    {
                        this.btnlastpage.Click -= new System.EventHandler(this.BtnlastpageClick);
                        btnlastpage.Dispose();
                        btnlastpage = null;
                    }
                    if (btnnextpage != null)
                    {
                        this.btnnextpage.Click -= new System.EventHandler(this.BtnnextpageClick);
                        btnnextpage.Dispose();
                        btnnextpage = null;
                    }
                    if (btnpreviuspage != null)
                    {
                        this.btnpreviuspage.Click -= new System.EventHandler(this.BtnpreviuspageClick);
                        btnpreviuspage.Dispose();
                        btnpreviuspage = null;
                    }
                    if (btnPrint != null)
                    {
                       // this.btnPrint.Click -= new System.EventHandler(this.BtnPrintClick);
                        btnPrint.Dispose();
                        btnPrint = null;
                    }
                    if (btnRefresh != null)
                    {
                        this.btnRefresh.Click -= new System.EventHandler(this.btnRefresh_Click);
                        btnRefresh.Dispose();
                        btnRefresh = null;
                    }
                    if (btnRemoveAllFilters != null)
                    {
                        this.btnRemoveAllFilters.Click -= new System.EventHandler(this.BtnRemoveClick);
                        btnRemoveAllFilters.Dispose();
                        btnRemoveAllFilters = null;
                    }
                    if (btnSelectAll != null)
                    {
                        this.btnSelectAll.Click -= new System.EventHandler(this.BtnSelectAllClick);
                        btnSelectAll.Dispose();
                        btnSelectAll = null;
                    }
                    if (btnUncheckAllFilters != null)
                    {
                        this.btnUncheckAllFilters.Click -= new System.EventHandler(this.BtnUncheckAllFiltersClick);
                        btnUncheckAllFilters.Dispose();
                        btnUncheckAllFilters = null;
                    }
                    if (BtnUngroup != null)
                    {
                        //this.BtnUngroup.Click -= new System.EventHandler(this.BtnUngroupClick);
                        BtnUngroup.Dispose();
                        BtnUngroup = null;
                    }
                    if (cmbColumn != null)
                    {
                        cmbColumn.Dispose();
                        cmbColumn = null;
                    }
                    if (cmbDocType != null)
                    {
                        this.cmbDocType.SelectedIndexChanged -= new System.EventHandler(this.CmbDocTypeSelectedIndexChanged);

                        if (cmbDocType.ComboBox.DataSource != null)
                        {
                            if (cmbDocType.ComboBox.DataSource is IDisposable)
                            {
                                ((IDisposable)cmbDocType.ComboBox.DataSource).Dispose();
                            }
                        }

                        cmbDocType.Dispose();
                        cmbDocType = null;
                    }
                    if (cmbFilterColumn != null)
                    {
                        this.cmbFilterColumn.SelectedIndexChanged -= new System.EventHandler(this.CmbFilterColumnSelectedIndexChanged);
                        cmbFilterColumn.Dispose();
                        cmbFilterColumn = null;
                    }
                    if (cmbOperator != null)
                    {
                        this.cmbOperator.SelectedIndexChanged -= new System.EventHandler(this.CmbOperatorSelectedIndexChanged);
                        cmbOperator.Dispose();
                        cmbOperator = null;
                    }
                    if (contextMenuStrip1 != null)
                    {
                        contextMenuStrip1.Dispose();
                        contextMenuStrip1 = null;
                    }
                    if (lblRows != null)
                    {
                        lblRows.Dispose();
                        lblRows = null;
                    }
                    if (lsvFilters != null)
                    {
                        this.lsvFilters.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.LsvFiltersItemCheck);
                        this.lsvFilters.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.LsvFiltersKeyUp);
                        lsvFilters.Dispose();
                        lsvFilters = null;
                    }
                    if (Paginalbl != null)
                    {
                        Paginalbl.Dispose();
                        Paginalbl = null;
                    }
                    if (pnlCtrlIndex != null)
                    {
                        pnlCtrlIndex.Dispose();
                        pnlCtrlIndex = null;
                    }
                    if (pnlTaskGrid != null)
                    {
                        pnlTaskGrid.Dispose();
                        pnlTaskGrid = null;
                    }
                    if (quitarTodosToolStripMenuItem != null)
                    {
                        this.quitarTodosToolStripMenuItem.Click -= new System.EventHandler(this.QuitarTodosToolStripMenuItemClick);
                        quitarTodosToolStripMenuItem.Dispose();
                        quitarTodosToolStripMenuItem = null;
                    }
                    if (quitarToolStripMenuItem != null)
                    {
                        this.quitarToolStripMenuItem.Click -= new System.EventHandler(this.QuitarToolStripMenuItemClick);
                        quitarToolStripMenuItem.Dispose();
                        quitarToolStripMenuItem = null;
                    }
                    if (selecteddropdownlistpage != null)
                    {
                        this.selecteddropdownlistpage.SelectedIndexChanged -= new System.EventHandler(this.SelecteddropdownlistpageSelectedIndexChanged);
                        selecteddropdownlistpage.Dispose();
                        selecteddropdownlistpage = null;
                    }
                    if (splitGrid != null)
                    {
                        splitGrid.Dispose();
                        splitGrid = null;
                    }
                    if (toolStrip1 != null)
                    {
                        toolStrip1.Dispose();
                        toolStrip1 = null;
                    }
                    if (toolStrip2 != null)
                    {
                        toolStrip2.Dispose();
                        toolStrip2 = null;
                    }
                    if (toolStripLabel1 != null)
                    {
                        toolStripLabel1.Dispose();
                        toolStripLabel1 = null;
                    }

                    if (toolStripSeparator1 != null)
                    {
                        toolStripSeparator1.Dispose();
                        toolStripSeparator1 = null;
                    }
                    if (toolStripSeparator2 != null)
                    {
                        toolStripSeparator2.Dispose();
                        toolStripSeparator2 = null;
                    }
                    if (toolStripSeparator3 != null)
                    {
                        toolStripSeparator3.Dispose();
                        toolStripSeparator3 = null;
                    }
                    if (toolStripSeparator4 != null)
                    {
                        toolStripSeparator4.Dispose();
                        toolStripSeparator4 = null;
                    }
                    if (toolStripSeparator6 != null)
                    {
                        toolStripSeparator6.Dispose();
                        toolStripSeparator6 = null;
                    }
                }

                base.Dispose(disposing);
            }
            catch (System.Threading.ThreadAbortException ex) { }
            catch (Exception ex) { }           
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZTGroupGrid));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRemoveAllFilters = new System.Windows.Forms.Button();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbColumn = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lblRows = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelectAll = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFit = new System.Windows.Forms.ToolStripButton();
            this.cmbDocType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnHideFilter = new System.Windows.Forms.ToolStripButton();
            this.BtnUngroup = new System.Windows.Forms.ToolStripButton();
            this.btnCollapseAll = new System.Windows.Forms.ToolStripButton();
            this.btnExpandAll = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.btnGraphic = new System.Windows.Forms.ToolStripButton();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.lsvFilters = new System.Windows.Forms.CheckedListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.quitarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitarTodosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitGrid = new System.Windows.Forms.SplitContainer();
            this.btnUncheckAllFilters = new System.Windows.Forms.Button();
            this.pnlCtrlIndex = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbOperator = new System.Windows.Forms.ComboBox();
            this.cmbFilterColumn = new System.Windows.Forms.ComboBox();
            this.pnlTaskGrid = new System.Windows.Forms.Panel();
            this.outlookGrid1 = new Zamba.Grid.Grid.ZTGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnfirstpage = new System.Windows.Forms.ToolStripButton();
            this.btnpreviuspage = new System.Windows.Forms.ToolStripButton();
            this.Paginalbl = new System.Windows.Forms.ToolStripLabel();
            this.selecteddropdownlistpage = new System.Windows.Forms.ToolStripComboBox();
            this.btnnextpage = new System.Windows.Forms.ToolStripButton();
            this.btnlastpage = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGrid)).BeginInit();
            this.splitGrid.Panel1.SuspendLayout();
            this.splitGrid.Panel2.SuspendLayout();
            this.splitGrid.SuspendLayout();
            this.pnlTaskGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outlookGrid1)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRemoveAllFilters
            // 
            this.btnRemoveAllFilters.BackColor = System.Drawing.Color.White;
            this.btnRemoveAllFilters.Enabled = false;
            this.btnRemoveAllFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAllFilters.Location = new System.Drawing.Point(3, 50);
            this.btnRemoveAllFilters.Name = "btnRemoveAllFilters";
            this.btnRemoveAllFilters.Size = new System.Drawing.Size(94, 20);
            this.btnRemoveAllFilters.TabIndex = 8;
            this.btnRemoveAllFilters.Text = "Quitar Todos";
            this.btnRemoveAllFilters.UseVisualStyleBackColor = false;
            this.btnRemoveAllFilters.Click += new System.EventHandler(this.BtnRemoveClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Tahoma", 7F);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(49, 22);
            this.toolStripLabel1.Text = "Columna:";
            // 
            // cmbColumn
            // 
            this.cmbColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColumn.Font = new System.Drawing.Font("Tahoma", 7F);
            this.cmbColumn.Name = "cmbColumn";
            this.cmbColumn.Size = new System.Drawing.Size(121, 25);
            this.cmbColumn.Sorted = true;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGroup
            // 
            this.btnGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGroup.Font = new System.Drawing.Font("Tahoma", 7F);
            this.btnGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGroup.Name = "btnGroup";
            this.btnGroup.Size = new System.Drawing.Size(47, 22);
            this.btnGroup.Text = "Agrupar";
            this.btnGroup.Click += new System.EventHandler(this.BtnGroupClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // lblRows
            // 
            this.lblRows.Font = new System.Drawing.Font("Tahoma", 7F);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(36, 22);
            this.lblRows.Text = "Filas: 0";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSelectAll.Font = new System.Drawing.Font("Tahoma", 7F);
            this.btnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(88, 22);
            this.btnSelectAll.Tag = "0";
            this.btnSelectAll.Text = "Seleccionar todos";
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAllClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 6.5F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(10, 10);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFit,
            this.cmbDocType,
            this.toolStripSeparator4,
            this.lblRows,
            this.toolStripSeparator2,
            this.btnHideFilter,
            this.toolStripLabel1,
            this.cmbColumn,
            this.btnGroup,
            this.BtnUngroup,
            this.btnCollapseAll,
            this.btnExpandAll,
            this.toolStripSeparator3,
            this.btnPrint,
            this.btnExportToExcel,
            this.toolStripSeparator1,
            this.btnSelectAll,
            this.btnGraphic,
            this.toolStripSeparator6,
            this.btnRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(841, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnFit
            // 
            this.btnFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFit.Font = new System.Drawing.Font("Tahoma", 7F);
            this.btnFit.Image = ((System.Drawing.Image)(resources.GetObject("btnFit.Image")));
            this.btnFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFit.Name = "btnFit";
            this.btnFit.Size = new System.Drawing.Size(41, 22);
            this.btnFit.Text = "Ajustar";
            this.btnFit.Click += new System.EventHandler(this.TxtFitClick);
            // 
            // cmbDocType
            // 
            this.cmbDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocType.Enabled = false;
            this.cmbDocType.Font = new System.Drawing.Font("Tahoma", 7F);
            this.cmbDocType.Name = "cmbDocType";
            this.cmbDocType.Size = new System.Drawing.Size(121, 25);
            this.cmbDocType.ToolTipText = "Entidad a visualizar en la grilla.";
            this.cmbDocType.Visible = false;
            this.cmbDocType.SelectedIndexChanged += new System.EventHandler(this.CmbDocTypeSelectedIndexChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator4.Visible = false;
            // 
            // btnHideFilter
            // 
            this.btnHideFilter.Checked = true;
            this.btnHideFilter.CheckOnClick = true;
            this.btnHideFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnHideFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnHideFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHideFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnHideFilter.Image")));
            this.btnHideFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHideFilter.Name = "btnHideFilter";
            this.btnHideFilter.Size = new System.Drawing.Size(45, 22);
            this.btnHideFilter.Text = "Filtros";
            this.btnHideFilter.Click += new System.EventHandler(this.HideClick);
            // 
            // BtnUngroup
            // 
            this.BtnUngroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnUngroup.Font = new System.Drawing.Font("Tahoma", 7F);
            this.BtnUngroup.Image = ((System.Drawing.Image)(resources.GetObject("BtnUngroup.Image")));
            this.BtnUngroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnUngroup.Name = "BtnUngroup";
            this.BtnUngroup.Size = new System.Drawing.Size(61, 22);
            this.BtnUngroup.Text = "Desagrupar";
            //this.BtnUngroup.Click += new System.EventHandler(this.BtnUngroupClick);
            // 
            // btnCollapseAll
            // 
            this.btnCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCollapseAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCollapseAll.Image = ((System.Drawing.Image)(resources.GetObject("btnCollapseAll.Image")));
            this.btnCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCollapseAll.Name = "btnCollapseAll";
            this.btnCollapseAll.Size = new System.Drawing.Size(23, 22);
            this.btnCollapseAll.Text = "Contraer Todo";
            this.btnCollapseAll.Visible = false;
            //this.btnCollapseAll.Click += new System.EventHandler(this.BtnCollapseAllClick);
            // 
            // btnExpandAll
            // 
            this.btnExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExpandAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("btnExpandAll.Image")));
            this.btnExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExpandAll.Name = "btnExpandAll";
            this.btnExpandAll.Size = new System.Drawing.Size(23, 22);
            this.btnExpandAll.Text = "Expandir Todo";
            this.btnExpandAll.Visible = false;
            //this.btnExpandAll.Click += new System.EventHandler(this.BtnExpandAllClick);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 7F);
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(47, 22);
            this.btnPrint.Text = "Imprimir";
            this.btnPrint.Click += new System.EventHandler(this.BtnPrintClick);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnExportToExcel.Font = new System.Drawing.Font("Tahoma", 7F);
            this.btnExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportToExcel.Image")));
            this.btnExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(48, 22);
            this.btnExportToExcel.Text = "Exportar";
            this.btnExportToExcel.Visible = false;
            this.btnExportToExcel.Click += new System.EventHandler(this.BtnExportToExcelClick);
            // 
            // btnGraphic
            // 
            this.btnGraphic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGraphic.Font = new System.Drawing.Font("Tahoma", 7F);
            this.btnGraphic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGraphic.Name = "btnGraphic";
            this.btnGraphic.Size = new System.Drawing.Size(41, 22);
            this.btnGraphic.Text = "Grafico";
            this.btnGraphic.ToolTipText = "Generar grafico";
            this.btnGraphic.Click += new System.EventHandler(this.BtnGraphicClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(59, 22);
            this.btnRefresh.Text = "Actualizar";
            this.btnRefresh.Visible = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lsvFilters
            // 
            this.lsvFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvFilters.BackColor = System.Drawing.Color.White;
            this.lsvFilters.ColumnWidth = 600;
            this.lsvFilters.ContextMenuStrip = this.contextMenuStrip1;
            this.lsvFilters.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvFilters.ForeColor = System.Drawing.Color.Black;
            this.lsvFilters.HorizontalScrollbar = true;
            this.lsvFilters.Location = new System.Drawing.Point(100, 5);
            this.lsvFilters.MultiColumn = true;
            this.lsvFilters.Name = "lsvFilters";
            this.lsvFilters.Size = new System.Drawing.Size(738, 68);
            this.lsvFilters.TabIndex = 10;
            this.lsvFilters.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LsvFiltersItemCheck);
            this.lsvFilters.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LsvFiltersKeyUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitarToolStripMenuItem,
            this.quitarTodosToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 48);
            // 
            // quitarToolStripMenuItem
            // 
            this.quitarToolStripMenuItem.Name = "quitarToolStripMenuItem";
            this.quitarToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.quitarToolStripMenuItem.Text = "Quitar";
            this.quitarToolStripMenuItem.Click += new System.EventHandler(this.QuitarToolStripMenuItemClick);
            // 
            // quitarTodosToolStripMenuItem
            // 
            this.quitarTodosToolStripMenuItem.Name = "quitarTodosToolStripMenuItem";
            this.quitarTodosToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.quitarTodosToolStripMenuItem.Text = "Quitar Todos";
            this.quitarTodosToolStripMenuItem.Click += new System.EventHandler(this.QuitarTodosToolStripMenuItemClick);
            // 
            // splitGrid
            // 
            this.splitGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGrid.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitGrid.IsSplitterFixed = true;
            this.splitGrid.Location = new System.Drawing.Point(0, 0);
            this.splitGrid.Name = "splitGrid";
            this.splitGrid.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitGrid.Panel1
            // 
            this.splitGrid.Panel1.Controls.Add(this.btnUncheckAllFilters);
            this.splitGrid.Panel1.Controls.Add(this.lsvFilters);
            this.splitGrid.Panel1.Controls.Add(this.pnlCtrlIndex);
            this.splitGrid.Panel1.Controls.Add(this.btnAdd);
            this.splitGrid.Panel1.Controls.Add(this.cmbOperator);
            this.splitGrid.Panel1.Controls.Add(this.cmbFilterColumn);
            this.splitGrid.Panel1.Controls.Add(this.btnRemoveAllFilters);
            this.splitGrid.Panel1MinSize = 100;
            // 
            // splitGrid.Panel2
            // 
            this.splitGrid.Panel2.Controls.Add(this.pnlTaskGrid);
            this.splitGrid.Panel2.Controls.Add(this.toolStrip1);
            this.splitGrid.Size = new System.Drawing.Size(841, 436);
            this.splitGrid.SplitterDistance = 100;
            this.splitGrid.TabIndex = 11;
            // 
            // btnUncheckAllFilters
            // 
            this.btnUncheckAllFilters.BackColor = System.Drawing.Color.White;
            this.btnUncheckAllFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUncheckAllFilters.Location = new System.Drawing.Point(3, 5);
            this.btnUncheckAllFilters.Name = "btnUncheckAllFilters";
            this.btnUncheckAllFilters.Size = new System.Drawing.Size(94, 20);
            this.btnUncheckAllFilters.TabIndex = 18;
            this.btnUncheckAllFilters.Text = "Deshabilitar Todos";
            this.btnUncheckAllFilters.UseVisualStyleBackColor = false;
            this.btnUncheckAllFilters.Click += new System.EventHandler(this.BtnUncheckAllFiltersClick);
            // 
            // pnlCtrlIndex
            // 
            this.pnlCtrlIndex.Location = new System.Drawing.Point(259, 77);
            this.pnlCtrlIndex.Name = "pnlCtrlIndex";
            this.pnlCtrlIndex.Size = new System.Drawing.Size(309, 20);
            this.pnlCtrlIndex.TabIndex = 16;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(588, 76);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(73, 23);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "Agregar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAddClick);
            // 
            // cmbOperator
            // 
            this.cmbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperator.FormattingEnabled = true;
            this.cmbOperator.Items.AddRange(new object[] {
            "=",
            ">",
            "<",
            ">=",
            "<=",
            "<>",
            "Es Nulo",
            "No es Nulo",
            "Contiene",
            "No Contiene"});
            this.cmbOperator.Location = new System.Drawing.Point(154, 76);
            this.cmbOperator.Name = "cmbOperator";
            this.cmbOperator.Size = new System.Drawing.Size(99, 21);
            this.cmbOperator.TabIndex = 13;
            this.cmbOperator.SelectedIndexChanged += new System.EventHandler(this.CmbOperatorSelectedIndexChanged);
            // 
            // cmbFilterColumn
            // 
            this.cmbFilterColumn.BackColor = System.Drawing.Color.White;
            this.cmbFilterColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterColumn.Location = new System.Drawing.Point(6, 76);
            this.cmbFilterColumn.Name = "cmbFilterColumn";
            this.cmbFilterColumn.Size = new System.Drawing.Size(142, 21);
            this.cmbFilterColumn.Sorted = true;
            this.cmbFilterColumn.TabIndex = 12;
            this.cmbFilterColumn.SelectedIndexChanged += new System.EventHandler(this.CmbFilterColumnSelectedIndexChanged);
            // 
            // pnlTaskGrid
            // 
            this.pnlTaskGrid.Controls.Add(this.outlookGrid1);
            this.pnlTaskGrid.Controls.Add(this.toolStrip2);
            this.pnlTaskGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTaskGrid.Location = new System.Drawing.Point(0, 25);
            this.pnlTaskGrid.Name = "pnlTaskGrid";
            this.pnlTaskGrid.Size = new System.Drawing.Size(841, 307);
            this.pnlTaskGrid.TabIndex = 6;
            // 
            // outlookGrid1
            // 
            this.outlookGrid1.AccessibleName = "";
           // this.outlookGrid1.AllowUserToAddRows = false;
           // this.outlookGrid1.AllowUserToDeleteRows = false;
           // this.outlookGrid1.AllowUserToOrderColumns = true;
         //   this.outlookGrid1.AllowUserToResizeRows = false;
          //  this.outlookGrid1.BackgroundColor = System.Drawing.Color.White;
          //  this.outlookGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
         //   this.outlookGrid1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
          //  this.outlookGrid1.CollapseIcon = ((System.Drawing.Image)(resources.GetObject("outlookGrid1.CollapseIcon")));
          //  this.outlookGrid1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            //this.outlookGrid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            //this.outlookGrid1.DefaultCellStyle = dataGridViewCellStyle2;
            this.outlookGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.outlookGrid1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            //this.outlookGrid1.ExpandIcon = ((System.Drawing.Image)(resources.GetObject("outlookGrid1.ExpandIcon")));
            //this.outlookGrid1.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.outlookGrid1.Location = new System.Drawing.Point(0, 0);
            this.outlookGrid1.Name = "outlookGrid1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            //this.outlookGrid1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            //this.outlookGrid1.RowHeadersVisible = false;
            //this.outlookGrid1.ShowCellErrors = false;
            //this.outlookGrid1.ShowEditingIcon = false;
            //this.outlookGrid1.ShowRowErrors = false;
            this.outlookGrid1.Size = new System.Drawing.Size(841, 282);
            this.outlookGrid1.TabIndex = 0;
            this.outlookGrid1.CellClick += new GridViewCellEventHandler(this.OutlookGrid1CellContentClick);
            this.outlookGrid1.CellDoubleClick += new GridViewCellEventHandler(this.OutlookGrid1CellDoubleClick);
            //this.outlookGrid1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OutlookGrid1CellMouseDown);
            //this.outlookGrid1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OutlookGrid1ColumnHeaderMouseClick);
            this.outlookGrid1.DoubleClick += new System.EventHandler(this.OutlookGrid1DoubleClick);
            //this.outlookGrid1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OutlookGrid1KeyPress);
            this.outlookGrid1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OutlookGrid1KeyUp);
            this.outlookGrid1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OutlookGrid_KeyDown);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnfirstpage,
            this.btnpreviuspage,
            this.Paginalbl,
            this.selecteddropdownlistpage,
            this.btnnextpage,
            this.btnlastpage});
            this.toolStrip2.Location = new System.Drawing.Point(0, 282);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(841, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnfirstpage
            // 
            this.btnfirstpage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnfirstpage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnfirstpage.Name = "btnfirstpage";
            this.btnfirstpage.Size = new System.Drawing.Size(27, 22);
            this.btnfirstpage.Text = "<<";
            this.btnfirstpage.ToolTipText = "Primera";
            this.btnfirstpage.Click += new System.EventHandler(this.BtnfirstpageClick);
            // 
            // btnpreviuspage
            // 
            this.btnpreviuspage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnpreviuspage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnpreviuspage.Name = "btnpreviuspage";
            this.btnpreviuspage.Size = new System.Drawing.Size(23, 22);
            this.btnpreviuspage.Text = "<";
            this.btnpreviuspage.ToolTipText = "Anterior";
            this.btnpreviuspage.Click += new System.EventHandler(this.BtnpreviuspageClick);
            // 
            // Paginalbl
            // 
            this.Paginalbl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Paginalbl.Name = "Paginalbl";
            this.Paginalbl.Size = new System.Drawing.Size(39, 22);
            this.Paginalbl.Text = "Pagina";
            // 
            // selecteddropdownlistpage
            // 
            this.selecteddropdownlistpage.BackColor = System.Drawing.Color.White;
            this.selecteddropdownlistpage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selecteddropdownlistpage.ForeColor = System.Drawing.Color.Black;
            this.selecteddropdownlistpage.Name = "selecteddropdownlistpage";
            this.selecteddropdownlistpage.Size = new System.Drawing.Size(75, 25);
            this.selecteddropdownlistpage.SelectedIndexChanged += new System.EventHandler(this.SelecteddropdownlistpageSelectedIndexChanged);
            // 
            // btnnextpage
            // 
            this.btnnextpage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnnextpage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnnextpage.Name = "btnnextpage";
            this.btnnextpage.Size = new System.Drawing.Size(23, 22);
            this.btnnextpage.Text = ">";
            this.btnnextpage.ToolTipText = "Siguiente";
            this.btnnextpage.Click += new System.EventHandler(this.BtnnextpageClick);
            // 
            // btnlastpage
            // 
            this.btnlastpage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnlastpage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnlastpage.Name = "btnlastpage";
            this.btnlastpage.Size = new System.Drawing.Size(27, 22);
            this.btnlastpage.Text = ">>";
            this.btnlastpage.ToolTipText = "Ultima";
            this.btnlastpage.Click += new System.EventHandler(this.BtnlastpageClick);
            // 
            // ZTGroupGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitGrid);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ZTGroupGrid";
            this.Size = new System.Drawing.Size(841, 436);
            this.Load += new System.EventHandler(this.Form1Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitGrid.Panel1.ResumeLayout(false);
            this.splitGrid.Panel2.ResumeLayout(false);
            this.splitGrid.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGrid)).EndInit();
            this.splitGrid.ResumeLayout(false);
            this.pnlTaskGrid.ResumeLayout(false);
            this.pnlTaskGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outlookGrid1)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Button btnRemoveAllFilters;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmbColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnExportToExcel;
        private System.Windows.Forms.ToolStripLabel lblRows;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnCollapseAll;
        private System.Windows.Forms.ToolStripButton btnExpandAll;
        private System.Windows.Forms.ToolStripButton btnFit;
        private System.Windows.Forms.ToolStripButton btnSelectAll;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.CheckedListBox lsvFilters;
        private System.Windows.Forms.SplitContainer splitGrid;
        private System.Windows.Forms.ToolStripButton btnHideFilter;
        private System.Windows.Forms.Panel pnlTaskGrid;
        private System.Windows.Forms.ToolStripButton btnGraphic;
        private System.Windows.Forms.ComboBox cmbOperator;
        private System.Windows.Forms.ComboBox cmbFilterColumn;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ToolStripButton BtnUngroup;
        public System.Windows.Forms.ToolStripComboBox cmbDocType;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Panel pnlCtrlIndex;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quitarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitarTodosToolStripMenuItem;
        private System.Windows.Forms.Button btnUncheckAllFilters;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnfirstpage;
        private System.Windows.Forms.ToolStripButton btnpreviuspage;
        private System.Windows.Forms.ToolStripButton btnnextpage;
        private System.Windows.Forms.ToolStripButton btnlastpage;
        private System.Windows.Forms.ToolStripLabel Paginalbl;
        private System.Windows.Forms.ToolStripComboBox selecteddropdownlistpage;
        private System.Windows.Forms.ToolStripButton btnRefresh;
       // private System.Windows.Forms.ComboBox cmbLogicalOperator;
    }
}
