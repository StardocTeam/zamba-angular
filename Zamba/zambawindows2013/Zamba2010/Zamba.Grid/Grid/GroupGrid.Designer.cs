using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls;
using Zamba.AppBlock;

namespace Zamba.Grid.Grid
{
    sealed partial class GroupGrid
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

                    if (newGrid != null)
                    {
                        this.newGrid.TableElement.VScrollBar.Scroll -= VScrollBar_Scroll;
                        this.newGrid.MouseWheel -= newGrid_MouseWheel;
                        this.newGrid.SelectionChanged -= new System.EventHandler(this.newGrid_SelectionChanged);
                        this.newGrid.CellClick -= new Telerik.WinControls.UI.GridViewCellEventHandler(this.newGrid_CellClick);
                        this.newGrid.CellDoubleClick -= new Telerik.WinControls.UI.GridViewCellEventHandler(this.newGrid_CellDoubleClick);
                        this.newGrid.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.newGrid_KeyDown);
                        this.newGrid.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.newGrid_KeyPress);
                        this.newGrid.KeyUp -= new System.Windows.Forms.KeyEventHandler(this.newGrid_KeyUp);
                        newGrid.Dispose();
                        newGrid = null;
                    }
                    if (btnAdd != null)
                    {
                        this.btnAdd.Click -= new System.EventHandler(this.BtnAddClick);
                        btnAdd.Dispose();
                        btnAdd = null;
                    }


                    if (btnExport != null)
                    {
                        this.btnExport.Click -= new System.EventHandler(this.BtnExport);
                        btnExport.Dispose();
                        btnExport = null;
                    }

                    if (btnFit != null)
                    {
                        this.btnFit.Click -= new System.EventHandler(this.TxtFitClick);
                        btnFit.Dispose();
                        btnFit = null;
                    }
                    //if (btnGraphic != null)
                    //{
                    //    this.btnGraphic.Click -= new System.EventHandler(this.BtnGraphicClick);
                    //    btnGraphic.Dispose();
                    //    btnGraphic = null;
                    //}

                    if (btnHideFilter != null)
                    {
                        this.btnHideFilter.Click -= new System.EventHandler(this.HideClick);
                        btnHideFilter.Dispose();
                        btnHideFilter = null;
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

                    if (splitGrid != null)
                    {
                        splitGrid.Dispose();
                        splitGrid = null;
                    }
                    if (ZToolBar1 != null)
                    {
                        ZToolBar1.Dispose();
                        ZToolBar1 = null;
                    }



                    if (ToolStripSeparator1 != null)
                    {
                        ToolStripSeparator1.Dispose();
                        ToolStripSeparator1 = null;
                    }
                    if (ToolStripSeparator2 != null)
                    {
                        ToolStripSeparator2.Dispose();
                        ToolStripSeparator2 = null;
                    }
                    if (ToolStripSeparator3 != null)
                    {
                        ToolStripSeparator3.Dispose();
                        ToolStripSeparator3 = null;
                    }
                    if (ToolStripSeparator4 != null)
                    {
                        ToolStripSeparator4.Dispose();
                        ToolStripSeparator4 = null;
                    }
                    if (ToolStripSeparator6 != null)
                    {
                        ToolStripSeparator6.Dispose();
                        ToolStripSeparator6 = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupGrid));
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.btnRemoveAllFilters = new Zamba.AppBlock.ZButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lblRows = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelectAll = new System.Windows.Forms.ToolStripButton();
            this.ZToolBar1 = new Zamba.AppBlock.ZToolBar();
            this.btnFit = new System.Windows.Forms.ToolStripButton();
            this.cmbDocType = new System.Windows.Forms.ToolStripComboBox();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnHideFilter = new System.Windows.Forms.ToolStripButton();
            this.btnCollapseAll = new System.Windows.Forms.ToolStripButton();
            this.btnExpandAll = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.lblSearchColumn = new System.Windows.Forms.ToolStripLabel();
            this.cmbSearchColumn = new System.Windows.Forms.ToolStripComboBox();
            this.btnfontsizeup = new System.Windows.Forms.ToolStripButton();
            this.btnfontsizedown = new System.Windows.Forms.ToolStripButton();
            this.lsvFilters = new System.Windows.Forms.CheckedListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.quitarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitarTodosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitGrid = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbFilterColumn = new System.Windows.Forms.ComboBox();
            this.cmbOperator = new System.Windows.Forms.ComboBox();
            this.pbFiltersHelp = new System.Windows.Forms.PictureBox();
            this.pnlCtrlIndex = new System.Windows.Forms.Panel();
            this.btnAdd = new Zamba.AppBlock.ZButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUncheckAllFilters = new Zamba.AppBlock.ZButton();
            this.pnlTaskGrid = new System.Windows.Forms.Panel();
            this.newGrid = new Zamba.Grid.NewGrid();
            this.TelerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
            this.radThemeManager1 = new Telerik.WinControls.RadThemeManager();
            this.ZToolBar1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGrid)).BeginInit();
            this.splitGrid.Panel1.SuspendLayout();
            this.splitGrid.Panel2.SuspendLayout();
            this.splitGrid.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFiltersHelp)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlTaskGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newGrid.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRemoveAllFilters
            // 
            this.btnRemoveAllFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.btnRemoveAllFilters.Enabled = false;
            this.btnRemoveAllFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveAllFilters.ForeColor = System.Drawing.Color.White;
            this.btnRemoveAllFilters.Location = new System.Drawing.Point(1, 29);
            this.btnRemoveAllFilters.Name = "btnRemoveAllFilters";
            this.btnRemoveAllFilters.Size = new System.Drawing.Size(103, 22);
            this.btnRemoveAllFilters.TabIndex = 8;
            this.btnRemoveAllFilters.Text = "Quitar Todos";
            this.btnRemoveAllFilters.UseVisualStyleBackColor = true;
            this.btnRemoveAllFilters.Click += new System.EventHandler(this.BtnRemoveClick);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // lblRows
            // 
            this.lblRows.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(41, 29);
            this.lblRows.Text = "Filas: 0";
            // 
            // ToolStripSeparator6
            // 
            this.ToolStripSeparator6.Name = "ToolStripSeparator6";
            this.ToolStripSeparator6.Size = new System.Drawing.Size(6, 32);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSelectAll.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(95, 29);
            this.btnSelectAll.Tag = "0";
            this.btnSelectAll.Text = "Seleccionar todos";
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAllClick);
            // 
            // ZToolBar1
            // 
            this.ZToolBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ZToolBar1.ImageScalingSize = new System.Drawing.Size(10, 10);
            this.ZToolBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFit,
            this.cmbDocType,
            this.ToolStripSeparator4,
            this.lblRows,
            this.ToolStripSeparator2,
            this.btnHideFilter,
            this.btnCollapseAll,
            this.btnExpandAll,
            this.ToolStripSeparator3,
            this.btnExport,
            this.ToolStripSeparator1,
            this.btnSelectAll,
            this.ToolStripSeparator6,
            this.btnRefresh,
            this.toolStripSeparator5,
            this.lblSearchColumn,
            this.cmbSearchColumn,
            this.btnfontsizeup,
            this.btnfontsizedown});
            this.ZToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ZToolBar1.Name = "ZToolBar1";
            this.ZToolBar1.Size = new System.Drawing.Size(841, 32);
            this.ZToolBar1.TabIndex = 5;
            this.ZToolBar1.Text = "ZToolBar1";



            this.ZToolBar1.CanOverflow = true;
            this.ZToolBar1.DefaultDropDownDirection = ToolStripDropDownDirection.BelowRight;
            this.ZToolBar1.OverflowButton.AutoSize = false;
            this.ZToolBar1.OverflowButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.ZToolBar1.Stretch = true;
            this.ZToolBar1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            
                       


            // btnFit
            // 
            this.btnFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFit.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnFit.Image = ((System.Drawing.Image)(resources.GetObject("btnFit.Image")));
            this.btnFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFit.Name = "btnFit";
            this.btnFit.Size = new System.Drawing.Size(46, 29);
            this.btnFit.Text = "Ajustar";
            this.btnFit.Click += new System.EventHandler(this.TxtFitClick);
            // 
            // cmbDocType
            // 
            this.cmbDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocType.Enabled = false;
            this.cmbDocType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDocType.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cmbDocType.Name = "cmbDocType";
            this.cmbDocType.Size = new System.Drawing.Size(121, 32);
            this.cmbDocType.ToolTipText = "Entidad a visualizar en la grilla.";
            this.cmbDocType.Visible = false;
            this.cmbDocType.SelectedIndexChanged += new System.EventHandler(this.CmbDocTypeSelectedIndexChanged);
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 32);
            this.ToolStripSeparator4.Visible = false;
            // 
            // btnHideFilter
            // 
            this.btnHideFilter.Checked = true;
            this.btnHideFilter.CheckOnClick = true;
            this.btnHideFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnHideFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnHideFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHideFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnHideFilter.Image")));
            this.btnHideFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHideFilter.Name = "btnHideFilter";
            this.btnHideFilter.Size = new System.Drawing.Size(45, 29);
            this.btnHideFilter.Text = "Filtros";
            this.btnHideFilter.Click += new System.EventHandler(this.HideClick);
            // 
            // btnCollapseAll
            // 
            this.btnCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCollapseAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCollapseAll.Image = ((System.Drawing.Image)(resources.GetObject("btnCollapseAll.Image")));
            this.btnCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCollapseAll.Name = "btnCollapseAll";
            this.btnCollapseAll.Size = new System.Drawing.Size(23, 29);
            this.btnCollapseAll.Text = "Contraer Todo";
            this.btnCollapseAll.Visible = false;
            // 
            // btnExpandAll
            // 
            this.btnExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExpandAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("btnExpandAll.Image")));
            this.btnExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExpandAll.Name = "btnExpandAll";
            this.btnExpandAll.Size = new System.Drawing.Size(23, 29);
            this.btnExpandAll.Text = "Expandir Todo";
            this.btnExpandAll.Visible = false;
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnExport.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(53, 29);
            this.btnExport.Text = "Exportar";
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.BtnExport);
            // 
            // btnRefresh
            // 
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(57, 29);
            this.btnRefresh.Text = "Actualizar";
            this.btnRefresh.Visible = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 32);
            // 
            // lblSearchColumn
            // 
            this.lblSearchColumn.Name = "lblSearchColumn";
            this.lblSearchColumn.Size = new System.Drawing.Size(86, 29);
            this.lblSearchColumn.Text = "Buscar columna:";
            // 
            // cmbSearchColumn
            // 
            this.cmbSearchColumn.Name = "cmbSearchColumn";
            this.cmbSearchColumn.Size = new System.Drawing.Size(121, 32);
            this.cmbSearchColumn.DropDownClosed += new System.EventHandler(this.cmbSearchColumn_DropDownClosed);
            this.cmbSearchColumn.SelectedIndexChanged += new System.EventHandler(this.cmbSearchColumn_SelectedIndexChanged);
            this.cmbSearchColumn.TextUpdate += new System.EventHandler(this.cmbSearchColumn_TextUpdate);
            // 
            // btnfontsizeup
            // 
            this.btnfontsizeup.AutoSize = false;
            this.btnfontsizeup.BackgroundImage = global::Zamba.Grid.Properties.Resources.appbar_text_size_up;
            this.btnfontsizeup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnfontsizeup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnfontsizeup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnfontsizeup.Margin = new System.Windows.Forms.Padding(0);
            this.btnfontsizeup.Name = "btnfontsizeup";
            this.btnfontsizeup.Size = new System.Drawing.Size(32, 32);
            this.btnfontsizeup.Text = "Aumentar Fuente";
            this.btnfontsizeup.Click += new System.EventHandler(this.btnfontsizeup_Click);
            // 
            // btnfontsizedown
            // 
            this.btnfontsizedown.AutoSize = false;
            this.btnfontsizedown.BackgroundImage = global::Zamba.Grid.Properties.Resources.appbar_text_size_down;
            this.btnfontsizedown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnfontsizedown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnfontsizedown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnfontsizedown.Margin = new System.Windows.Forms.Padding(0);
            this.btnfontsizedown.Name = "btnfontsizedown";
            this.btnfontsizedown.Size = new System.Drawing.Size(32, 32);
            this.btnfontsizedown.Text = "Disminuir Fuente";
            this.btnfontsizedown.Click += new System.EventHandler(this.btnfontsizedown_Click);
            // 
            // lsvFilters
            // 
            this.lsvFilters.BackColor = System.Drawing.Color.White;
            this.lsvFilters.ColumnWidth = 600;
            this.lsvFilters.ContextMenuStrip = this.contextMenuStrip1;
            this.lsvFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvFilters.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvFilters.ForeColor = System.Drawing.Color.Black;
            this.lsvFilters.HorizontalScrollbar = true;
            this.lsvFilters.Location = new System.Drawing.Point(112, 0);
            this.lsvFilters.MultiColumn = true;
            this.lsvFilters.Name = "lsvFilters";
            this.lsvFilters.Size = new System.Drawing.Size(729, 55);
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 48);
            // 
            // quitarToolStripMenuItem
            // 
            this.quitarToolStripMenuItem.Name = "quitarToolStripMenuItem";
            this.quitarToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.quitarToolStripMenuItem.Text = "Quitar";
            this.quitarToolStripMenuItem.Click += new System.EventHandler(this.QuitarToolStripMenuItemClick);
            // 
            // quitarTodosToolStripMenuItem
            // 
            this.quitarTodosToolStripMenuItem.Name = "quitarTodosToolStripMenuItem";
            this.quitarTodosToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
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
            this.splitGrid.Panel1.Controls.Add(this.flowLayoutPanel1);
            this.splitGrid.Panel1.Controls.Add(this.panel1);
            this.splitGrid.Panel1.ForeColor = System.Drawing.Color.Black;
            this.splitGrid.Panel1MinSize = 130;
            // 
            // splitGrid.Panel2
            // 
            this.splitGrid.Panel2.Controls.Add(this.pnlTaskGrid);
            this.splitGrid.Panel2.Controls.Add(this.ZToolBar1);
            this.splitGrid.Size = new System.Drawing.Size(841, 436);
            this.splitGrid.SplitterDistance = 130;
            this.splitGrid.SplitterWidth = 1;
            this.splitGrid.TabIndex = 11;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.cmbFilterColumn);
            this.flowLayoutPanel1.Controls.Add(this.cmbOperator);
            this.flowLayoutPanel1.Controls.Add(this.pbFiltersHelp);
            this.flowLayoutPanel1.Controls.Add(this.pnlCtrlIndex);
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 55);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(841, 75);
            this.flowLayoutPanel1.TabIndex = 19;
            // 
            // cmbFilterColumn
            // 
            this.cmbFilterColumn.BackColor = System.Drawing.Color.White;
            this.cmbFilterColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterColumn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbFilterColumn.Location = new System.Drawing.Point(3, 3);
            this.cmbFilterColumn.Name = "cmbFilterColumn";
            this.cmbFilterColumn.Size = new System.Drawing.Size(142, 21);
            this.cmbFilterColumn.Sorted = true;
            this.cmbFilterColumn.TabIndex = 12;
            this.cmbFilterColumn.SelectedIndexChanged += new System.EventHandler(this.CmbFilterColumnSelectedIndexChanged);
            // 
            // cmbOperator
            // 
            this.cmbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            "No Contiene",
            "Alguno"});
            this.cmbOperator.Location = new System.Drawing.Point(151, 3);
            this.cmbOperator.Name = "cmbOperator";
            this.cmbOperator.Size = new System.Drawing.Size(99, 21);
            this.cmbOperator.TabIndex = 13;
            this.cmbOperator.SelectedIndexChanged += new System.EventHandler(this.CmbOperatorSelectedIndexChanged);
            // 
            // pbFiltersHelp
            // 
            this.pbFiltersHelp.BackColor = System.Drawing.Color.Transparent;
            this.pbFiltersHelp.Image = global::Zamba.Grid.Properties.Resources.appbar1;
            this.pbFiltersHelp.Location = new System.Drawing.Point(256, 3);
            this.pbFiltersHelp.Name = "pbFiltersHelp";
            this.pbFiltersHelp.Size = new System.Drawing.Size(20, 20);
            this.pbFiltersHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFiltersHelp.TabIndex = 17;
            this.pbFiltersHelp.TabStop = false;
            // 
            // pnlCtrlIndex
            // 
            this.pnlCtrlIndex.Location = new System.Drawing.Point(282, 3);
            this.pnlCtrlIndex.Name = "pnlCtrlIndex";
            this.pnlCtrlIndex.Size = new System.Drawing.Size(309, 20);
            this.pnlCtrlIndex.TabIndex = 16;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(597, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(103, 22);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "Agregar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAddClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lsvFilters);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(841, 55);
            this.panel1.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnUncheckAllFilters);
            this.panel2.Controls.Add(this.btnRemoveAllFilters);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(112, 55);
            this.panel2.TabIndex = 19;
            // 
            // btnUncheckAllFilters
            // 
            this.btnUncheckAllFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.btnUncheckAllFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUncheckAllFilters.ForeColor = System.Drawing.Color.White;
            this.btnUncheckAllFilters.Location = new System.Drawing.Point(1, 2);
            this.btnUncheckAllFilters.Name = "btnUncheckAllFilters";
            this.btnUncheckAllFilters.Size = new System.Drawing.Size(103, 22);
            this.btnUncheckAllFilters.TabIndex = 18;
            this.btnUncheckAllFilters.Text = "Deshabilitar Todos";
            this.btnUncheckAllFilters.UseVisualStyleBackColor = true;
            this.btnUncheckAllFilters.Click += new System.EventHandler(this.BtnUncheckAllFiltersClick);
            // 
            // pnlTaskGrid
            // 
            this.pnlTaskGrid.Controls.Add(this.newGrid);
            this.pnlTaskGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTaskGrid.ForeColor = System.Drawing.Color.Black;
            this.pnlTaskGrid.Location = new System.Drawing.Point(0, 32);
            this.pnlTaskGrid.Name = "pnlTaskGrid";
            this.pnlTaskGrid.Size = new System.Drawing.Size(841, 273);
            this.pnlTaskGrid.TabIndex = 6;
            // 
            // newGrid
            // 
            this.newGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newGrid.AutoScroll = true;
            this.newGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.newGrid.CurrentHorizontalScrollValue = 0;
            this.newGrid.CurrentVerticalScrollValue = 0;
            this.newGrid.LastHorizontalScrollValue = 0;
            this.newGrid.LastVerticalScrollValue = 0;
            this.newGrid.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.newGrid.MasterTemplate.MultiSelect = true;
            this.newGrid.MasterTemplate.PageSize = 1000;
            this.newGrid.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.newGrid.Name = "newGrid";
            this.newGrid.ReadOnly = true;
            // 
            // 
            // 
            this.newGrid.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 240, 150);
            this.newGrid.Size = new System.Drawing.Size(841, 273);
            this.newGrid.TabIndex = 8;
            this.newGrid.Text = "newGrid1";
            this.newGrid.ThemeName = "TelerikMetroBlue";
            this.newGrid.SelectionChanged += new System.EventHandler(this.newGrid_SelectionChanged);
            this.newGrid.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.newGrid_CellClick);
            this.newGrid.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.newGrid_CellDoubleClick);
            this.newGrid.Click += new System.EventHandler(this.newGrid_Click);
            this.newGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.newGrid_KeyDown);
            this.newGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.newGrid_KeyPress);
            this.newGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.newGrid_KeyUp);
            // 
            // GroupGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitGrid);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "GroupGrid";
            this.Size = new System.Drawing.Size(841, 436);
            this.Load += new System.EventHandler(this.Form1Load);
            this.ZToolBar1.ResumeLayout(false);
            this.ZToolBar1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitGrid.Panel1.ResumeLayout(false);
            this.splitGrid.Panel2.ResumeLayout(false);
            this.splitGrid.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGrid)).EndInit();
            this.splitGrid.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFiltersHelp)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlTaskGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.newGrid.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newGrid)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private ZButton btnRemoveAllFilters;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.ToolStripLabel lblRows;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnCollapseAll;
        private System.Windows.Forms.ToolStripButton btnExpandAll;
        private System.Windows.Forms.ToolStripButton btnFit;
        private System.Windows.Forms.ToolStripButton btnSelectAll;
        private ZToolBar ZToolBar1;
        private System.Windows.Forms.CheckedListBox lsvFilters;
        private System.Windows.Forms.SplitContainer splitGrid;
        private System.Windows.Forms.ToolStripButton btnHideFilter;
        private System.Windows.Forms.Panel pnlTaskGrid;
        //private System.Windows.Forms.ToolStripButton btnGraphic;
        private System.Windows.Forms.ComboBox cmbOperator;
        private System.Windows.Forms.ComboBox cmbFilterColumn;
        private ZButton btnAdd;
        public System.Windows.Forms.ToolStripComboBox cmbDocType;
        public System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        private System.Windows.Forms.Panel pnlCtrlIndex;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quitarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitarTodosToolStripMenuItem;
        private ZButton btnUncheckAllFilters;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private NewGrid newGrid;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Telerik.WinControls.Themes.TelerikMetroBlueTheme TelerikMetroBlueTheme1;
        private RadThemeManager radThemeManager1;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripComboBox cmbSearchColumn;
        private ToolStripLabel lblSearchColumn;
        private PictureBox pbFiltersHelp;
        private ToolStripButton btnfontsizeup;
        private ToolStripButton btnfontsizedown;
    }
}
