using System;
using System.Windows.Forms;
using Zamba.AppBlock;

partial class UCUseCase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCUseCase));
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.txtPrecondicion = new System.Windows.Forms.TextBox();
            this.label3 = new ZLabel();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label1 = new ZLabel();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ZToolBar1 = new ZToolBar();
            this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbExportTypes = new System.Windows.Forms.ToolStripComboBox();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.grdSteps = new Telerik.WinControls.UI.RadGridView();
            this.splitPanel3 = new Telerik.WinControls.UI.SplitPanel();
            this.txtPostCondition = new System.Windows.Forms.TextBox();
            this.label2 = new ZLabel();
            this.toolTipMod = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ZToolBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel3)).BeginInit();
            this.splitPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.Controls.Add(this.splitPanel1);
            this.radSplitContainer1.Controls.Add(this.splitPanel2);
            this.radSplitContainer1.Controls.Add(this.splitPanel3);
            this.radSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.radSplitContainer1.Name = "radSplitContainer1";
            this.radSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.radSplitContainer1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.radSplitContainer1.Size = new System.Drawing.Size(660, 523);
            this.radSplitContainer1.TabIndex = 0;
            this.radSplitContainer1.TabStop = false;
            this.radSplitContainer1.Text = "radSplitContainer1";
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.Color.White;
            this.splitPanel1.Controls.Add(this.txtPrecondicion);
            this.splitPanel1.Controls.Add(this.label3);
            this.splitPanel1.Controls.Add(this.txtTitle);
            this.splitPanel1.Controls.Add(this.label1);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 200, 200);
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(660, 155);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.03352677F);
            this.splitPanel1.SizeInfo.MaximumSize = new System.Drawing.Size(0, 132);
            this.splitPanel1.SizeInfo.MinimumSize = new System.Drawing.Size(25, 155);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -41);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "z";
            // 
            // txtPrecondicion
            // 
            this.txtPrecondicion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrecondicion.Enabled = false;
            this.txtPrecondicion.Location = new System.Drawing.Point(17, 61);
            this.txtPrecondicion.MaxLength = 10000;
            this.txtPrecondicion.Multiline = true;
            this.txtPrecondicion.Name = "txtPrecondicion";
            this.txtPrecondicion.Size = new System.Drawing.Size(624, 79);
            this.txtPrecondicion.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Precondicion:";
            // 
            // txtTitle
            // 
            this.txtTitle.Enabled = false;
            this.txtTitle.Location = new System.Drawing.Point(60, 14);
            this.txtTitle.MaxLength = 500;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(581, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Titulo:";
            // 
            // splitPanel2
            // 
            this.splitPanel2.BackColor = System.Drawing.Color.White;
            this.splitPanel2.Controls.Add(this.splitContainer1);
            this.splitPanel2.Location = new System.Drawing.Point(0, 158);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(660, 222);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.09606704F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 63);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ZToolBar1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grdSteps);
            this.splitContainer1.Size = new System.Drawing.Size(660, 222);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 1;
            // 
            // ZToolBar1
            // 
            this.ZToolBar1.Items.AddRange(new ToolStripItem[] {
            this.ToolStripLabel1,
            this.cmbExportTypes,
            this.btnExport});
            this.ZToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ZToolBar1.Name = "ZToolBar1";
            this.ZToolBar1.Size = new System.Drawing.Size(660, 25);
            this.ZToolBar1.TabIndex = 0;
            this.ZToolBar1.Text = "TestCaseBar";
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
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // grdSteps
            // 
            this.grdSteps.AutoScroll = true;
            this.grdSteps.AutoSizeRows = true;
            this.grdSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSteps.Location = new System.Drawing.Point(0, 0);
            // 
            // grdSteps
            // 
            this.grdSteps.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom;
            this.grdSteps.MasterTemplate.AllowAddNewRow = false;
            this.grdSteps.MasterTemplate.AllowCellContextMenu = false;
            this.grdSteps.MasterTemplate.AllowColumnReorder = false;
            this.grdSteps.MasterTemplate.AllowDeleteRow = false;
            this.grdSteps.MasterTemplate.AllowEditRow = false;
            this.grdSteps.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn1.FieldName = "Paso";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "Id";
            gridViewTextBoxColumn1.Name = "Paso";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.Width = 69;
            gridViewTextBoxColumn2.AllowHide = false;
            gridViewTextBoxColumn2.FieldName = "Descripcion";
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "Descripcion";
            gridViewTextBoxColumn2.Name = "Descripcion";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 572;
            this.grdSteps.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2});
            this.grdSteps.MasterTemplate.EnableAlternatingRowColor = true;
            this.grdSteps.MasterTemplate.EnableGrouping = false;
            this.grdSteps.MasterTemplate.EnableSorting = false;
            this.grdSteps.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
            this.grdSteps.Name = "grdSteps";
            this.grdSteps.Size = new System.Drawing.Size(660, 193);
            this.grdSteps.TabIndex = 1;
            this.grdSteps.Text = "radGridView1";
            // 
            // splitPanel3
            // 
            this.splitPanel3.BackColor = System.Drawing.Color.White;
            this.splitPanel3.Controls.Add(this.txtPostCondition);
            this.splitPanel3.Controls.Add(this.label2);
            this.splitPanel3.Location = new System.Drawing.Point(0, 383);
            this.splitPanel3.Name = "splitPanel3";
            // 
            // 
            // 
            this.splitPanel3.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel3.Size = new System.Drawing.Size(660, 140);
            this.splitPanel3.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.06254031F);
            this.splitPanel3.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -22);
            this.splitPanel3.TabIndex = 2;
            this.splitPanel3.TabStop = false;
            this.splitPanel3.Text = "splitPanel3";
            // 
            // txtPostCondition
            // 
            this.txtPostCondition.AcceptsReturn = true;
            this.txtPostCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPostCondition.Enabled = false;
            this.txtPostCondition.Location = new System.Drawing.Point(17, 24);
            this.txtPostCondition.MaxLength = 10000;
            this.txtPostCondition.Multiline = true;
            this.txtPostCondition.Name = "txtPostCondition";
            this.txtPostCondition.Size = new System.Drawing.Size(624, 101);
            this.txtPostCondition.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Poscondicion:";
            // 
            // UCUseCase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radSplitContainer1);
            this.Name = "UCUseCase";
            this.Size = new System.Drawing.Size(660, 523);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            this.splitPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ZToolBar1.ResumeLayout(false);
            this.ZToolBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel3)).EndInit();
            this.splitPanel3.ResumeLayout(false);
            this.splitPanel3.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
    private Telerik.WinControls.UI.SplitPanel splitPanel1;
    private System.Windows.Forms.TextBox txtPrecondicion;
    private ZLabel label3;
    private System.Windows.Forms.TextBox txtTitle;
    private ZLabel label1;
    private Telerik.WinControls.UI.SplitPanel splitPanel2;
    private Telerik.WinControls.UI.SplitPanel splitPanel3;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private Telerik.WinControls.UI.RadGridView grdSteps;
    private ZToolBar ZToolBar1;
    private System.Windows.Forms.ToolStripLabel ToolStripLabel1;
    private System.Windows.Forms.ToolStripComboBox cmbExportTypes;
    private System.Windows.Forms.ToolStripButton btnExport;
    private System.Windows.Forms.ToolTip toolTipMod;
    private System.Windows.Forms.TextBox txtPostCondition;
    private ZLabel label2;
}
