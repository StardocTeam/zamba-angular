<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGenericData
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGenericData))
        Me.lblPrimaryKey = New ZLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lbxTables = New System.Windows.Forms.ListBox()
        Me.pnlPreview = New System.Windows.Forms.Panel()
        Me.btnPreview100Records = New ZButton()
        Me.btnPreviewAllRecords = New ZButton()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.lblSearch = New ZLabel()
        Me.btnSearchDown = New ZButton()
        Me.btnSearchUp = New ZButton()
        Me.txtSearchData = New System.Windows.Forms.TextBox()
        Me.dgvColumns = New System.Windows.Forms.DataGridView()
        Me.pnlStart = New System.Windows.Forms.Panel()
        Me.btnStop = New ZButton()
        Me.btnStart = New ZButton()
        Me.pgbData = New System.Windows.Forms.ProgressBar()
        Me.pnlPrimaryKey = New System.Windows.Forms.Panel()
        Me.cboPrimaryKey = New System.Windows.Forms.ComboBox()
        Me.bgwDataGeneration = New System.ComponentModel.BackgroundWorker()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.pnlPreview.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.dgvColumns, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStart.SuspendLayout()
        Me.pnlPrimaryKey.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblPrimaryKey
        '
        Me.lblPrimaryKey.AutoSize = True
        Me.lblPrimaryKey.Location = New System.Drawing.Point(3, 8)
        Me.lblPrimaryKey.Name = "lblPrimaryKey"
        Me.lblPrimaryKey.Size = New System.Drawing.Size(74, 13)
        Me.lblPrimaryKey.TabIndex = 3
        Me.lblPrimaryKey.Text = "Clave Primaria"
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(150, 46)
        Me.Panel1.TabIndex = 10
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Size = New System.Drawing.Size(897, 533)
        Me.SplitContainer1.SplitterDistance = 248
        Me.SplitContainer1.TabIndex = 11
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.lbxTables)
        Me.SplitContainer2.Panel1.Controls.Add(Me.pnlPreview)
        Me.SplitContainer2.Panel1.Controls.Add(Me.pnlSearch)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgvColumns)
        Me.SplitContainer2.Panel2.Controls.Add(Me.pnlStart)
        Me.SplitContainer2.Panel2.Controls.Add(Me.pnlPrimaryKey)
        Me.SplitContainer2.Size = New System.Drawing.Size(897, 533)
        Me.SplitContainer2.SplitterDistance = 316
        Me.SplitContainer2.TabIndex = 0
        '
        'lbxTables
        '
        Me.lbxTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbxTables.FormattingEnabled = True
        Me.lbxTables.Location = New System.Drawing.Point(0, 27)
        Me.lbxTables.Name = "lbxTables"
        Me.lbxTables.Size = New System.Drawing.Size(316, 473)
        Me.lbxTables.TabIndex = 2
        '
        'pnlPreview
        '
        Me.pnlPreview.Controls.Add(Me.btnPreview100Records)
        Me.pnlPreview.Controls.Add(Me.btnPreviewAllRecords)
        Me.pnlPreview.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlPreview.Location = New System.Drawing.Point(0, 500)
        Me.pnlPreview.Name = "pnlPreview"
        Me.pnlPreview.Size = New System.Drawing.Size(316, 33)
        Me.pnlPreview.TabIndex = 47
        '
        'btnPreview100Records
        '
        Me.btnPreview100Records.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPreview100Records.Location = New System.Drawing.Point(6, 6)
        Me.btnPreview100Records.Name = "btnPreview100Records"
        Me.btnPreview100Records.Size = New System.Drawing.Size(140, 23)
        Me.btnPreview100Records.TabIndex = 47
        Me.btnPreview100Records.Text = "Mostrar 100 registros"
        Me.btnPreview100Records.UseVisualStyleBackColor = True
        '
        'btnPreviewAllRecords
        '
        Me.btnPreviewAllRecords.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPreviewAllRecords.Location = New System.Drawing.Point(152, 6)
        Me.btnPreviewAllRecords.Name = "btnPreviewAllRecords"
        Me.btnPreviewAllRecords.Size = New System.Drawing.Size(140, 23)
        Me.btnPreviewAllRecords.TabIndex = 48
        Me.btnPreviewAllRecords.Text = "Mostrar toda la tabla"
        Me.btnPreviewAllRecords.UseVisualStyleBackColor = True
        '
        'pnlSearch
        '
        Me.pnlSearch.Controls.Add(Me.lblSearch)
        Me.pnlSearch.Controls.Add(Me.btnSearchDown)
        Me.pnlSearch.Controls.Add(Me.btnSearchUp)
        Me.pnlSearch.Controls.Add(Me.txtSearchData)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(316, 27)
        Me.pnlSearch.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(3, 8)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(40, 13)
        Me.lblSearch.TabIndex = 46
        Me.lblSearch.Text = "Buscar"
        '
        'btnSearchDown
        '
        Me.btnSearchDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearchDown.BackColor = System.Drawing.Color.Transparent
        Me.btnSearchDown.BackgroundImage = CType(resources.GetObject("btnSearchDown.BackgroundImage"), System.Drawing.Image)
        Me.btnSearchDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchDown.Location = New System.Drawing.Point(257, 3)
        Me.btnSearchDown.Name = "btnSearchDown"
        Me.btnSearchDown.Size = New System.Drawing.Size(25, 22)
        Me.btnSearchDown.TabIndex = 45
        Me.btnSearchDown.UseVisualStyleBackColor = False
        '
        'btnSearchUp
        '
        Me.btnSearchUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearchUp.BackColor = System.Drawing.Color.Transparent
        Me.btnSearchUp.BackgroundImage = CType(resources.GetObject("btnSearchUp.BackgroundImage"), System.Drawing.Image)
        Me.btnSearchUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchUp.Location = New System.Drawing.Point(288, 3)
        Me.btnSearchUp.Name = "btnSearchUp"
        Me.btnSearchUp.Size = New System.Drawing.Size(25, 22)
        Me.btnSearchUp.TabIndex = 44
        Me.btnSearchUp.UseVisualStyleBackColor = False
        '
        'txtSearchData
        '
        Me.txtSearchData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchData.Location = New System.Drawing.Point(49, 3)
        Me.txtSearchData.Name = "txtSearchData"
        Me.txtSearchData.Size = New System.Drawing.Size(202, 20)
        Me.txtSearchData.TabIndex = 43
        '
        'dgvColumns
        '
        Me.dgvColumns.AllowUserToAddRows = False
        Me.dgvColumns.AllowUserToDeleteRows = False
        Me.dgvColumns.AllowUserToResizeRows = False
        Me.dgvColumns.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvColumns.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvColumns.Location = New System.Drawing.Point(0, 27)
        Me.dgvColumns.Name = "dgvColumns"
        Me.dgvColumns.RowHeadersVisible = False
        Me.dgvColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvColumns.ShowCellErrors = False
        Me.dgvColumns.ShowEditingIcon = False
        Me.dgvColumns.ShowRowErrors = False
        Me.dgvColumns.Size = New System.Drawing.Size(577, 473)
        Me.dgvColumns.TabIndex = 50
        '
        'pnlStart
        '
        Me.pnlStart.Controls.Add(Me.btnStop)
        Me.pnlStart.Controls.Add(Me.btnStart)
        Me.pnlStart.Controls.Add(Me.pgbData)
        Me.pnlStart.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlStart.Location = New System.Drawing.Point(0, 500)
        Me.pnlStart.Name = "pnlStart"
        Me.pnlStart.Size = New System.Drawing.Size(577, 33)
        Me.pnlStart.TabIndex = 49
        '
        'btnStop
        '
        Me.btnStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStop.Location = New System.Drawing.Point(92, 6)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(80, 23)
        Me.btnStop.TabIndex = 18
        Me.btnStop.Text = "Detener"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStart.Location = New System.Drawing.Point(6, 6)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(80, 23)
        Me.btnStart.TabIndex = 16
        Me.btnStart.Text = "Comenzar"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'pgbData
        '
        Me.pgbData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgbData.Location = New System.Drawing.Point(178, 6)
        Me.pgbData.Name = "pgbData"
        Me.pgbData.Size = New System.Drawing.Size(387, 23)
        Me.pgbData.TabIndex = 17
        '
        'pnlPrimaryKey
        '
        Me.pnlPrimaryKey.Controls.Add(Me.cboPrimaryKey)
        Me.pnlPrimaryKey.Controls.Add(Me.lblPrimaryKey)
        Me.pnlPrimaryKey.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPrimaryKey.Location = New System.Drawing.Point(0, 0)
        Me.pnlPrimaryKey.Name = "pnlPrimaryKey"
        Me.pnlPrimaryKey.Size = New System.Drawing.Size(577, 27)
        Me.pnlPrimaryKey.TabIndex = 48
        '
        'cboPrimaryKey
        '
        Me.cboPrimaryKey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboPrimaryKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrimaryKey.FormattingEnabled = True
        Me.cboPrimaryKey.Location = New System.Drawing.Point(83, 3)
        Me.cboPrimaryKey.Name = "cboPrimaryKey"
        Me.cboPrimaryKey.Size = New System.Drawing.Size(482, 21)
        Me.cboPrimaryKey.TabIndex = 10
        '
        'bgwDataGeneration
        '
        '
        'FrmGenericData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(897, 533)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmGenericData"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generador de datos para tablas genéricas"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.pnlPreview.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.dgvColumns, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStart.ResumeLayout(False)
        Me.pnlPrimaryKey.ResumeLayout(False)
        Me.pnlPrimaryKey.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblPrimaryKey As ZLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents bgwDataGeneration As System.ComponentModel.BackgroundWorker
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lblSearch As ZLabel
    Friend WithEvents btnSearchDown As ZButton
    Friend WithEvents btnSearchUp As ZButton
    Friend WithEvents txtSearchData As System.Windows.Forms.TextBox
    Friend WithEvents lbxTables As System.Windows.Forms.ListBox
    Friend WithEvents pnlPreview As System.Windows.Forms.Panel
    Friend WithEvents btnPreview100Records As ZButton
    Friend WithEvents btnPreviewAllRecords As ZButton
    Friend WithEvents pnlPrimaryKey As System.Windows.Forms.Panel
    Friend WithEvents cboPrimaryKey As System.Windows.Forms.ComboBox
    Friend WithEvents pnlStart As System.Windows.Forms.Panel
    Friend WithEvents btnStop As ZButton
    Friend WithEvents btnStart As ZButton
    Friend WithEvents pgbData As System.Windows.Forms.ProgressBar
    Friend WithEvents dgvColumns As System.Windows.Forms.DataGridView

End Class
