<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEntityData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEntityData))
        Me.btnStart = New ZButton()
        Me.btnPreview100Records = New ZButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.pnlAttributes = New System.Windows.Forms.Panel()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.lblSearch = New ZLabel()
        Me.btnSearchDown = New ZButton()
        Me.btnSearchUp = New ZButton()
        Me.txtSearchData = New System.Windows.Forms.TextBox()
        Me.btnStop = New ZButton()
        Me.lblCurrentTable = New ZLabel()
        Me.btnPreviewAllRecords = New ZButton()
        Me.clbTables = New System.Windows.Forms.CheckedListBox()
        Me.btnLoadTables = New ZButton()
        Me.pgbData = New System.Windows.Forms.ProgressBar()
        Me.bgwDataGeneration = New System.ComponentModel.BackgroundWorker()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.pnlAttributes.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStart.Enabled = False
        Me.btnStart.Location = New System.Drawing.Point(3, 561)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(80, 23)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Comenzar"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnPreview100Records
        '
        Me.btnPreview100Records.Enabled = False
        Me.btnPreview100Records.Location = New System.Drawing.Point(149, 3)
        Me.btnPreview100Records.Name = "btnPreview100Records"
        Me.btnPreview100Records.Size = New System.Drawing.Size(140, 23)
        Me.btnPreview100Records.TabIndex = 1
        Me.btnPreview100Records.Text = "Mostrar 100 registros"
        Me.btnPreview100Records.UseVisualStyleBackColor = True
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
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Size = New System.Drawing.Size(938, 587)
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.pnlAttributes)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnStop)
        Me.SplitContainer2.Panel2.Controls.Add(Me.lblCurrentTable)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPreviewAllRecords)
        Me.SplitContainer2.Panel2.Controls.Add(Me.clbTables)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnLoadTables)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnStart)
        Me.SplitContainer2.Panel2.Controls.Add(Me.pgbData)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPreview100Records)
        Me.SplitContainer2.Size = New System.Drawing.Size(938, 587)
        Me.SplitContainer2.SplitterDistance = 446
        Me.SplitContainer2.TabIndex = 14
        '
        'pnlAttributes
        '
        Me.pnlAttributes.Controls.Add(Me.pnlSearch)
        Me.pnlAttributes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAttributes.Location = New System.Drawing.Point(0, 0)
        Me.pnlAttributes.Name = "pnlAttributes"
        Me.pnlAttributes.Size = New System.Drawing.Size(446, 587)
        Me.pnlAttributes.TabIndex = 13
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
        Me.pnlSearch.Size = New System.Drawing.Size(446, 26)
        Me.pnlSearch.TabIndex = 0
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
        Me.btnSearchDown.Location = New System.Drawing.Point(387, 3)
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
        Me.btnSearchUp.Location = New System.Drawing.Point(418, 3)
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
        Me.txtSearchData.Size = New System.Drawing.Size(332, 20)
        Me.txtSearchData.TabIndex = 43
        '
        'btnStop
        '
        Me.btnStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(89, 561)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(80, 23)
        Me.btnStop.TabIndex = 15
        Me.btnStop.Text = "Detener"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'lblCurrentTable
        '
        Me.lblCurrentTable.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCurrentTable.AutoSize = True
        Me.lblCurrentTable.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentTable.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.lblCurrentTable.Location = New System.Drawing.Point(188, 566)
        Me.lblCurrentTable.Name = "lblCurrentTable"
        Me.lblCurrentTable.Size = New System.Drawing.Size(0, 13)
        Me.lblCurrentTable.TabIndex = 14
        '
        'btnPreviewAllRecords
        '
        Me.btnPreviewAllRecords.Enabled = False
        Me.btnPreviewAllRecords.Location = New System.Drawing.Point(295, 3)
        Me.btnPreviewAllRecords.Name = "btnPreviewAllRecords"
        Me.btnPreviewAllRecords.Size = New System.Drawing.Size(140, 23)
        Me.btnPreviewAllRecords.TabIndex = 13
        Me.btnPreviewAllRecords.Text = "Mostrar toda la tabla"
        Me.btnPreviewAllRecords.UseVisualStyleBackColor = True
        '
        'clbTables
        '
        Me.clbTables.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbTables.CheckOnClick = True
        Me.clbTables.Enabled = False
        Me.clbTables.FormattingEnabled = True
        Me.clbTables.Location = New System.Drawing.Point(3, 32)
        Me.clbTables.Name = "clbTables"
        Me.clbTables.Size = New System.Drawing.Size(482, 529)
        Me.clbTables.TabIndex = 12
        '
        'btnLoadTables
        '
        Me.btnLoadTables.Location = New System.Drawing.Point(3, 3)
        Me.btnLoadTables.Name = "btnLoadTables"
        Me.btnLoadTables.Size = New System.Drawing.Size(140, 23)
        Me.btnLoadTables.TabIndex = 11
        Me.btnLoadTables.Text = "Cargar tablas afectadas"
        Me.btnLoadTables.UseVisualStyleBackColor = True
        '
        'pgbData
        '
        Me.pgbData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgbData.Enabled = False
        Me.pgbData.Location = New System.Drawing.Point(175, 562)
        Me.pgbData.Name = "pgbData"
        Me.pgbData.Size = New System.Drawing.Size(308, 23)
        Me.pgbData.TabIndex = 10
        '
        'bgwDataGeneration
        '
        '
        'FrmEntityData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(938, 587)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmEntityData"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generador de datos para entidades"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        Me.pnlAttributes.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnStart As ZButton
    Friend WithEvents btnPreview100Records As ZButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents pgbData As System.Windows.Forms.ProgressBar
    Friend WithEvents bgwDataGeneration As System.ComponentModel.BackgroundWorker
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlAttributes As System.Windows.Forms.Panel
    Friend WithEvents btnLoadTables As ZButton
    Friend WithEvents clbTables As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnPreviewAllRecords As ZButton
    Friend WithEvents lblCurrentTable As ZLabel
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lblSearch As ZLabel
    Friend WithEvents btnSearchDown As ZButton
    Friend WithEvents btnSearchUp As ZButton
    Friend WithEvents txtSearchData As System.Windows.Forms.TextBox
    Friend WithEvents btnStop As ZButton

End Class
