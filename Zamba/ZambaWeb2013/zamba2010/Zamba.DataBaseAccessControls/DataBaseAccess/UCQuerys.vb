Imports ZAMBA.Servers
Imports Zamba.AppBlock

Public Class UCQuerys
    Inherits System.Windows.Forms.UserControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents Panel1 As ZBluePanel
    Friend WithEvents Panel2 As ZBluePanel
    Friend WithEvents Button1 As Zamba.AppBlock.ZButton
    Friend WithEvents tpSelect As ZTabsPage
    Friend WithEvents tpUpdate As ZTabsPage
    Friend WithEvents tpInsert As ZTabsPage
    Friend WithEvents tpDelete As ZTabsPage
    Friend WithEvents tpSentence As ZTabsPage
    Friend WithEvents grdSentence As System.Windows.Forms.DataGrid
    Friend WithEvents tbQuerys As ZTabs
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Panel3 As ZBluePanel
    Friend WithEvents Panel4 As ZBluePanel
    Friend WithEvents Panel5 As ZBluePanel
    Friend WithEvents Panel6 As ZBluePanel
    Friend WithEvents Panel7 As ZBluePanel
    Friend WithEvents Panel8 As ZBluePanel
    Friend WithEvents Panel9 As ZBluePanel
    Friend WithEvents Panel10 As ZBluePanel
    Friend WithEvents Panel11 As ZBluePanel
    Friend WithEvents Panel12 As ZBluePanel
    Friend WithEvents Panel13 As ZBluePanel
    Friend WithEvents grdUpd As System.Windows.Forms.DataGrid
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents DataGrid2 As System.Windows.Forms.DataGrid
    Friend WithEvents DataGrid3 As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTableStyle2 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents tbResult As System.Windows.Forms.TabPage
    Friend WithEvents dgvSelectResult As System.Windows.Forms.DataGrid
    Friend WithEvents Button2 As Zamba.AppBlock.ZButton1
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New Zamba.AppBlock.ZBluePanel
        Me.Panel2 = New Zamba.AppBlock.ZBluePanel
        Me.Button2 = New Zamba.AppBlock.ZButton1
        Me.Button1 = New Zamba.AppBlock.ZButton
        Me.tbQuerys = New Zamba.AppBlock.ZTabs
        Me.tpSelect = New Zamba.AppBlock.ZTabsPage
        Me.Panel3 = New Zamba.AppBlock.ZBluePanel
        Me.Panel13 = New Zamba.AppBlock.ZBluePanel
        Me.DataGrid3 = New System.Windows.Forms.DataGrid
        Me.Panel10 = New Zamba.AppBlock.ZBluePanel
        Me.tpInsert = New Zamba.AppBlock.ZTabsPage
        Me.Panel12 = New Zamba.AppBlock.ZBluePanel
        Me.DataGrid2 = New System.Windows.Forms.DataGrid
        Me.Panel9 = New Zamba.AppBlock.ZBluePanel
        Me.tpUpdate = New Zamba.AppBlock.ZTabsPage
        Me.Panel7 = New Zamba.AppBlock.ZBluePanel
        Me.grdUpd = New System.Windows.Forms.DataGrid
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.Panel6 = New Zamba.AppBlock.ZBluePanel
        Me.tpDelete = New Zamba.AppBlock.ZTabsPage
        Me.Panel11 = New Zamba.AppBlock.ZBluePanel
        Me.DataGrid1 = New System.Windows.Forms.DataGrid
        Me.Panel8 = New Zamba.AppBlock.ZBluePanel
        Me.tpSentence = New Zamba.AppBlock.ZTabsPage
        Me.Panel4 = New Zamba.AppBlock.ZBluePanel
        Me.grdSentence = New System.Windows.Forms.DataGrid
        Me.DataGridTableStyle2 = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.Panel5 = New Zamba.AppBlock.ZBluePanel
        Me.tbResult = New System.Windows.Forms.TabPage
        Me.dgvSelectResult = New System.Windows.Forms.DataGrid
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.tbQuerys.SuspendLayout()
        Me.tpSelect.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel13.SuspendLayout()
        CType(Me.DataGrid3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpInsert.SuspendLayout()
        Me.Panel12.SuspendLayout()
        CType(Me.DataGrid2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpUpdate.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.grdUpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpDelete.SuspendLayout()
        Me.Panel11.SuspendLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpSentence.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.grdSentence, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbResult.SuspendLayout()
        CType(Me.dgvSelectResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel1.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.tbQuerys)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(632, 520)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel2.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 464)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(632, 56)
        Me.Panel2.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.None
        Me.Button2.Location = New System.Drawing.Point(360, 16)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(96, 32)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Salir"
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.None
        Me.Button1.Location = New System.Drawing.Point(464, 16)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(120, 32)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Ejecutar"
        '
        'tbQuerys
        '
        Me.tbQuerys.Controls.Add(Me.tpSelect)
        Me.tbQuerys.Controls.Add(Me.tpInsert)
        Me.tbQuerys.Controls.Add(Me.tpUpdate)
        Me.tbQuerys.Controls.Add(Me.tpDelete)
        Me.tbQuerys.Controls.Add(Me.tpSentence)
        Me.tbQuerys.Controls.Add(Me.tbResult)
        Me.tbQuerys.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbQuerys.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbQuerys.ItemSize = New System.Drawing.Size(59, 18)
        Me.tbQuerys.Location = New System.Drawing.Point(0, 0)
        Me.tbQuerys.Name = "tbQuerys"
        Me.tbQuerys.SelectedIndex = 0
        Me.tbQuerys.Size = New System.Drawing.Size(632, 520)
        Me.tbQuerys.TabIndex = 0
        Me.tbQuerys.Text = "Resultado"
        '
        'tpSelect
        '
        Me.tpSelect.Color1 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpSelect.Color2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpSelect.Controls.Add(Me.Panel3)
        Me.tpSelect.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.tpSelect.IncludeBackground = True
        Me.tpSelect.Location = New System.Drawing.Point(4, 22)
        Me.tpSelect.Name = "tpSelect"
        Me.tpSelect.Size = New System.Drawing.Size(624, 494)
        Me.tpSelect.TabIndex = 0
        Me.tpSelect.Text = "Selección"
        Me.tpSelect.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel3.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Panel13)
        Me.Panel3.Controls.Add(Me.Panel10)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(624, 494)
        Me.Panel3.TabIndex = 2
        '
        'Panel13
        '
        Me.Panel13.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel13.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel13.Controls.Add(Me.DataGrid3)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel13.Location = New System.Drawing.Point(0, 0)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(624, 438)
        Me.Panel13.TabIndex = 4
        '
        'DataGrid3
        '
        Me.DataGrid3.AlternatingBackColor = System.Drawing.Color.White
        Me.DataGrid3.BackColor = System.Drawing.Color.White
        Me.DataGrid3.BackgroundColor = System.Drawing.Color.White
        Me.DataGrid3.CaptionBackColor = System.Drawing.Color.White
        Me.DataGrid3.CaptionFont = New System.Drawing.Font("Verdana", 10.0!)
        Me.DataGrid3.CaptionForeColor = System.Drawing.Color.White
        Me.DataGrid3.DataMember = ""
        Me.DataGrid3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGrid3.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGrid3.ForeColor = System.Drawing.Color.Black
        Me.DataGrid3.GridLineColor = System.Drawing.Color.White
        Me.DataGrid3.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.DataGrid3.HeaderBackColor = System.Drawing.Color.LightSteelBlue
        Me.DataGrid3.HeaderForeColor = System.Drawing.Color.Black
        Me.DataGrid3.LinkColor = System.Drawing.Color.White
        Me.DataGrid3.Location = New System.Drawing.Point(0, 0)
        Me.DataGrid3.Name = "DataGrid3"
        Me.DataGrid3.ParentRowsBackColor = System.Drawing.Color.White
        Me.DataGrid3.ParentRowsForeColor = System.Drawing.Color.White
        Me.DataGrid3.PreferredRowHeight = 18
        Me.DataGrid3.ReadOnly = True
        Me.DataGrid3.SelectionBackColor = System.Drawing.Color.White
        Me.DataGrid3.SelectionForeColor = System.Drawing.Color.Black
        Me.DataGrid3.Size = New System.Drawing.Size(624, 438)
        Me.DataGrid3.TabIndex = 1
        '
        'Panel10
        '
        Me.Panel10.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel10.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel10.Location = New System.Drawing.Point(0, 438)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(624, 56)
        Me.Panel10.TabIndex = 3
        '
        'tpInsert
        '
        Me.tpInsert.Color1 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpInsert.Color2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpInsert.Controls.Add(Me.Panel12)
        Me.tpInsert.Controls.Add(Me.Panel9)
        Me.tpInsert.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.tpInsert.IncludeBackground = True
        Me.tpInsert.Location = New System.Drawing.Point(4, 22)
        Me.tpInsert.Name = "tpInsert"
        Me.tpInsert.Size = New System.Drawing.Size(624, 494)
        Me.tpInsert.TabIndex = 1
        Me.tpInsert.Text = "Inserción"
        Me.tpInsert.UseVisualStyleBackColor = True
        '
        'Panel12
        '
        Me.Panel12.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel12.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel12.Controls.Add(Me.DataGrid2)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(624, 438)
        Me.Panel12.TabIndex = 4
        '
        'DataGrid2
        '
        Me.DataGrid2.AlternatingBackColor = System.Drawing.Color.White
        Me.DataGrid2.BackColor = System.Drawing.Color.White
        Me.DataGrid2.BackgroundColor = System.Drawing.Color.White
        Me.DataGrid2.CaptionBackColor = System.Drawing.Color.White
        Me.DataGrid2.CaptionFont = New System.Drawing.Font("Verdana", 10.0!)
        Me.DataGrid2.CaptionForeColor = System.Drawing.Color.White
        Me.DataGrid2.DataMember = ""
        Me.DataGrid2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGrid2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGrid2.ForeColor = System.Drawing.Color.Black
        Me.DataGrid2.GridLineColor = System.Drawing.Color.White
        Me.DataGrid2.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.DataGrid2.HeaderBackColor = System.Drawing.Color.LightSteelBlue
        Me.DataGrid2.HeaderForeColor = System.Drawing.Color.Black
        Me.DataGrid2.LinkColor = System.Drawing.Color.White
        Me.DataGrid2.Location = New System.Drawing.Point(0, 0)
        Me.DataGrid2.Name = "DataGrid2"
        Me.DataGrid2.ParentRowsBackColor = System.Drawing.Color.White
        Me.DataGrid2.ParentRowsForeColor = System.Drawing.Color.White
        Me.DataGrid2.PreferredRowHeight = 18
        Me.DataGrid2.ReadOnly = True
        Me.DataGrid2.SelectionBackColor = System.Drawing.Color.White
        Me.DataGrid2.SelectionForeColor = System.Drawing.Color.Black
        Me.DataGrid2.Size = New System.Drawing.Size(624, 438)
        Me.DataGrid2.TabIndex = 1
        '
        'Panel9
        '
        Me.Panel9.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel9.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel9.Location = New System.Drawing.Point(0, 438)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(624, 56)
        Me.Panel9.TabIndex = 3
        '
        'tpUpdate
        '
        Me.tpUpdate.Color1 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpUpdate.Color2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpUpdate.Controls.Add(Me.Panel7)
        Me.tpUpdate.Controls.Add(Me.Panel6)
        Me.tpUpdate.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.tpUpdate.IncludeBackground = True
        Me.tpUpdate.Location = New System.Drawing.Point(4, 22)
        Me.tpUpdate.Name = "tpUpdate"
        Me.tpUpdate.Size = New System.Drawing.Size(624, 494)
        Me.tpUpdate.TabIndex = 2
        Me.tpUpdate.Text = "Actualización"
        Me.tpUpdate.UseVisualStyleBackColor = True
        '
        'Panel7
        '
        Me.Panel7.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel7.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel7.Controls.Add(Me.grdUpd)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(624, 438)
        Me.Panel7.TabIndex = 3
        '
        'grdUpd
        '
        Me.grdUpd.AlternatingBackColor = System.Drawing.Color.White
        Me.grdUpd.BackColor = System.Drawing.Color.White
        Me.grdUpd.BackgroundColor = System.Drawing.Color.White
        Me.grdUpd.CaptionBackColor = System.Drawing.Color.White
        Me.grdUpd.CaptionFont = New System.Drawing.Font("Verdana", 10.0!)
        Me.grdUpd.CaptionForeColor = System.Drawing.Color.White
        Me.grdUpd.DataMember = ""
        Me.grdUpd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdUpd.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdUpd.ForeColor = System.Drawing.Color.Black
        Me.grdUpd.GridLineColor = System.Drawing.Color.White
        Me.grdUpd.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.grdUpd.HeaderBackColor = System.Drawing.Color.LightSteelBlue
        Me.grdUpd.HeaderForeColor = System.Drawing.Color.Black
        Me.grdUpd.LinkColor = System.Drawing.Color.White
        Me.grdUpd.Location = New System.Drawing.Point(0, 0)
        Me.grdUpd.Name = "grdUpd"
        Me.grdUpd.ParentRowsBackColor = System.Drawing.Color.White
        Me.grdUpd.ParentRowsForeColor = System.Drawing.Color.White
        Me.grdUpd.PreferredRowHeight = 18
        Me.grdUpd.ReadOnly = True
        Me.grdUpd.SelectionBackColor = System.Drawing.Color.White
        Me.grdUpd.SelectionForeColor = System.Drawing.Color.Black
        Me.grdUpd.Size = New System.Drawing.Size(624, 438)
        Me.grdUpd.TabIndex = 1
        Me.grdUpd.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.grdUpd
        Me.DataGridTableStyle1.ForeColor = System.Drawing.Color.Black
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7})
        Me.DataGridTableStyle1.GridLineColor = System.Drawing.Color.White
        Me.DataGridTableStyle1.HeaderBackColor = System.Drawing.Color.LightSteelBlue
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.Color.Black
        Me.DataGridTableStyle1.LinkColor = System.Drawing.Color.White
        Me.DataGridTableStyle1.MappingName = "Table"
        Me.DataGridTableStyle1.PreferredRowHeight = 18
        Me.DataGridTableStyle1.ReadOnly = True
        Me.DataGridTableStyle1.SelectionBackColor = System.Drawing.Color.White
        Me.DataGridTableStyle1.SelectionForeColor = System.Drawing.Color.Black
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Id"
        Me.DataGridTextBoxColumn6.MappingName = "id"
        Me.DataGridTextBoxColumn6.Width = 35
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Nombre"
        Me.DataGridTextBoxColumn7.MappingName = "nombre"
        Me.DataGridTextBoxColumn7.Width = 500
        '
        'Panel6
        '
        Me.Panel6.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel6.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(0, 438)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(624, 56)
        Me.Panel6.TabIndex = 2
        '
        'tpDelete
        '
        Me.tpDelete.Color1 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpDelete.Color2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpDelete.Controls.Add(Me.Panel11)
        Me.tpDelete.Controls.Add(Me.Panel8)
        Me.tpDelete.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.tpDelete.IncludeBackground = True
        Me.tpDelete.Location = New System.Drawing.Point(4, 22)
        Me.tpDelete.Name = "tpDelete"
        Me.tpDelete.Size = New System.Drawing.Size(624, 494)
        Me.tpDelete.TabIndex = 3
        Me.tpDelete.Text = "Eliminación"
        Me.tpDelete.UseVisualStyleBackColor = True
        '
        'Panel11
        '
        Me.Panel11.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel11.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel11.Controls.Add(Me.DataGrid1)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(624, 438)
        Me.Panel11.TabIndex = 4
        '
        'DataGrid1
        '
        Me.DataGrid1.AlternatingBackColor = System.Drawing.Color.White
        Me.DataGrid1.BackColor = System.Drawing.Color.White
        Me.DataGrid1.BackgroundColor = System.Drawing.Color.White
        Me.DataGrid1.CaptionBackColor = System.Drawing.Color.White
        Me.DataGrid1.CaptionFont = New System.Drawing.Font("Verdana", 10.0!)
        Me.DataGrid1.CaptionForeColor = System.Drawing.Color.White
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGrid1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGrid1.ForeColor = System.Drawing.Color.Black
        Me.DataGrid1.GridLineColor = System.Drawing.Color.White
        Me.DataGrid1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.DataGrid1.HeaderBackColor = System.Drawing.Color.LightSteelBlue
        Me.DataGrid1.HeaderForeColor = System.Drawing.Color.Black
        Me.DataGrid1.LinkColor = System.Drawing.Color.White
        Me.DataGrid1.Location = New System.Drawing.Point(0, 0)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.ParentRowsBackColor = System.Drawing.Color.White
        Me.DataGrid1.ParentRowsForeColor = System.Drawing.Color.White
        Me.DataGrid1.PreferredRowHeight = 18
        Me.DataGrid1.ReadOnly = True
        Me.DataGrid1.SelectionBackColor = System.Drawing.Color.White
        Me.DataGrid1.SelectionForeColor = System.Drawing.Color.Black
        Me.DataGrid1.Size = New System.Drawing.Size(624, 438)
        Me.DataGrid1.TabIndex = 1
        '
        'Panel8
        '
        Me.Panel8.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel8.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel8.Location = New System.Drawing.Point(0, 438)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(624, 56)
        Me.Panel8.TabIndex = 3
        '
        'tpSentence
        '
        Me.tpSentence.Color1 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpSentence.Color2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpSentence.Controls.Add(Me.Panel4)
        Me.tpSentence.Controls.Add(Me.Panel5)
        Me.tpSentence.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.tpSentence.IncludeBackground = True
        Me.tpSentence.Location = New System.Drawing.Point(4, 22)
        Me.tpSentence.Name = "tpSentence"
        Me.tpSentence.Size = New System.Drawing.Size(624, 494)
        Me.tpSentence.TabIndex = 4
        Me.tpSentence.Text = "Sentencia"
        Me.tpSentence.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel4.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel4.Controls.Add(Me.grdSentence)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(624, 438)
        Me.Panel4.TabIndex = 1
        '
        'grdSentence
        '
        Me.grdSentence.AlternatingBackColor = System.Drawing.Color.White
        Me.grdSentence.BackColor = System.Drawing.Color.White
        Me.grdSentence.BackgroundColor = System.Drawing.Color.White
        Me.grdSentence.CaptionBackColor = System.Drawing.Color.White
        Me.grdSentence.CaptionFont = New System.Drawing.Font("Verdana", 10.0!)
        Me.grdSentence.CaptionForeColor = System.Drawing.Color.White
        Me.grdSentence.DataMember = ""
        Me.grdSentence.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSentence.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSentence.ForeColor = System.Drawing.Color.Black
        Me.grdSentence.GridLineColor = System.Drawing.Color.White
        Me.grdSentence.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.grdSentence.HeaderBackColor = System.Drawing.Color.LightSteelBlue
        Me.grdSentence.HeaderForeColor = System.Drawing.Color.Black
        Me.grdSentence.LinkColor = System.Drawing.Color.White
        Me.grdSentence.Location = New System.Drawing.Point(0, 0)
        Me.grdSentence.Name = "grdSentence"
        Me.grdSentence.ParentRowsBackColor = System.Drawing.Color.White
        Me.grdSentence.ParentRowsForeColor = System.Drawing.Color.White
        Me.grdSentence.PreferredRowHeight = 18
        Me.grdSentence.ReadOnly = True
        Me.grdSentence.SelectionBackColor = System.Drawing.Color.White
        Me.grdSentence.SelectionForeColor = System.Drawing.Color.Black
        Me.grdSentence.Size = New System.Drawing.Size(624, 438)
        Me.grdSentence.TabIndex = 0
        Me.grdSentence.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle2})
        '
        'DataGridTableStyle2
        '
        Me.DataGridTableStyle2.AlternatingBackColor = System.Drawing.Color.White
        Me.DataGridTableStyle2.BackColor = System.Drawing.Color.White
        Me.DataGridTableStyle2.DataGrid = Me.grdSentence
        Me.DataGridTableStyle2.ForeColor = System.Drawing.Color.Black
        Me.DataGridTableStyle2.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn10})
        Me.DataGridTableStyle2.GridLineColor = System.Drawing.Color.White
        Me.DataGridTableStyle2.HeaderBackColor = System.Drawing.Color.LightSteelBlue
        Me.DataGridTableStyle2.HeaderForeColor = System.Drawing.Color.Black
        Me.DataGridTableStyle2.LinkColor = System.Drawing.Color.White
        Me.DataGridTableStyle2.MappingName = "Table"
        Me.DataGridTableStyle2.PreferredRowHeight = 18
        Me.DataGridTableStyle2.ReadOnly = True
        Me.DataGridTableStyle2.SelectionBackColor = System.Drawing.Color.White
        Me.DataGridTableStyle2.SelectionForeColor = System.Drawing.Color.Black
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "Id"
        Me.DataGridTextBoxColumn8.MappingName = "Id"
        Me.DataGridTextBoxColumn8.Width = 35
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "nombre"
        Me.DataGridTextBoxColumn9.MappingName = "Nombre"
        Me.DataGridTextBoxColumn9.Width = 425
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.HeaderText = "Tipo"
        Me.DataGridTextBoxColumn10.MappingName = "Tipo"
        Me.DataGridTextBoxColumn10.Width = 120
        '
        'Panel5
        '
        Me.Panel5.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel5.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(0, 438)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(624, 56)
        Me.Panel5.TabIndex = 2
        '
        'tbResult
        '
        Me.tbResult.Controls.Add(Me.dgvSelectResult)
        Me.tbResult.Location = New System.Drawing.Point(4, 22)
        Me.tbResult.Name = "tbResult"
        Me.tbResult.Padding = New System.Windows.Forms.Padding(3)
        Me.tbResult.Size = New System.Drawing.Size(624, 494)
        Me.tbResult.TabIndex = 5
        Me.tbResult.Text = "Resultado"
        Me.tbResult.UseVisualStyleBackColor = True
        '
        'dgvSelectResult
        '
        Me.dgvSelectResult.AlternatingBackColor = System.Drawing.Color.White
        Me.dgvSelectResult.BackColor = System.Drawing.Color.White
        Me.dgvSelectResult.BackgroundColor = System.Drawing.Color.White
        Me.dgvSelectResult.CaptionBackColor = System.Drawing.Color.White
        Me.dgvSelectResult.CaptionFont = New System.Drawing.Font("Verdana", 10.0!)
        Me.dgvSelectResult.CaptionForeColor = System.Drawing.Color.White
        Me.dgvSelectResult.DataMember = ""
        Me.dgvSelectResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSelectResult.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvSelectResult.ForeColor = System.Drawing.Color.Black
        Me.dgvSelectResult.GridLineColor = System.Drawing.Color.White
        Me.dgvSelectResult.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.dgvSelectResult.HeaderBackColor = System.Drawing.Color.LightSteelBlue
        Me.dgvSelectResult.HeaderForeColor = System.Drawing.Color.Black
        Me.dgvSelectResult.LinkColor = System.Drawing.Color.White
        Me.dgvSelectResult.Location = New System.Drawing.Point(3, 3)
        Me.dgvSelectResult.Name = "dgvSelectResult"
        Me.dgvSelectResult.ParentRowsBackColor = System.Drawing.Color.White
        Me.dgvSelectResult.ParentRowsForeColor = System.Drawing.Color.White
        Me.dgvSelectResult.PreferredRowHeight = 18
        Me.dgvSelectResult.ReadOnly = True
        Me.dgvSelectResult.SelectionBackColor = System.Drawing.Color.White
        Me.dgvSelectResult.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvSelectResult.Size = New System.Drawing.Size(618, 488)
        Me.dgvSelectResult.TabIndex = 2
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Id"
        Me.DataGridTextBoxColumn1.MappingName = "Id"
        Me.DataGridTextBoxColumn1.ReadOnly = True
        Me.DataGridTextBoxColumn1.Width = 40
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Nombre"
        Me.DataGridTextBoxColumn2.MappingName = "Nombre"
        Me.DataGridTextBoxColumn2.ReadOnly = True
        Me.DataGridTextBoxColumn2.Width = 280
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Id"
        Me.DataGridTextBoxColumn3.MappingName = "id"
        Me.DataGridTextBoxColumn3.Width = 40
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Nombre"
        Me.DataGridTextBoxColumn4.MappingName = "Nombre"
        Me.DataGridTextBoxColumn4.Width = 280
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Tipo"
        Me.DataGridTextBoxColumn5.MappingName = "Tipo"
        Me.DataGridTextBoxColumn5.Width = 80
        '
        'UCQuerys
        '
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UCQuerys"
        Me.Size = New System.Drawing.Size(632, 520)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.tbQuerys.ResumeLayout(False)
        Me.tpSelect.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        CType(Me.DataGrid3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpInsert.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        CType(Me.DataGrid2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpUpdate.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        CType(Me.grdUpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpDelete.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpSentence.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.grdSentence, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbResult.ResumeLayout(False)
        CType(Me.dgvSelectResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub UCQuerys_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Sentencia temporal hasta terminar las otras solapas
        Me.tbQuerys.SelectedIndex = 2

        LoadUpdates()
        LoadSentences()
        LoadSelects()
        LoadDelete()
        LoadInsert()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Select Case Me.tbQuerys.SelectedIndex

            Case 0

                Dim dsResult As New DataSet
                dsResult = UCSelect.ExecuteSelect(CInt(Me.DataGrid3.Item(Me.DataGrid3.CurrentRowIndex, 0)))
                If dsResult.Tables(0).Rows.Count > 0 Then
                    dgvSelectResult.DataSource = dsResult.Tables(0)
                    tbQuerys.SelectTab(tbResult)
                Else
                    MessageBox.Show("No se encontraron resultados para la consulta", "Zamba Administrador")
                End If
                Exit Select
            Case 1
                Dim modified As Int32
                modified = UCInsert.ExecuteInsert(CInt(Me.DataGrid2.Item(Me.DataGrid2.CurrentRowIndex, 0)))
                If modified > 0 Then
                    MessageBox.Show("Se insertaron " & modified & " registro/s en la base de datos", "Zamba Administrador")
                Else
                    MessageBox.Show("No se produjeron resultados para la consulta", "Zamba Administrador")
                End If
                Exit Select
            Case 2
                UCUpdate.ExecuteUpdate(CInt(Me.grdUpd.Item(Me.grdUpd.CurrentRowIndex, 0)))
                Exit Select
            Case 3
                Dim modified As Int32
                modified = UCDeleteWz.ExecuteDelete(CInt(Me.DataGrid1.Item(Me.DataGrid1.CurrentRowIndex, 0)))
                If modified > 0 Then
                    MessageBox.Show("Se borraron " & modified & " registro/s de la base de datos", "Zamba Administrador")
                Else
                    MessageBox.Show("No se produjeron resultados para la consulta", "Zamba Administrador")
                End If
            Case 4
                UCSentence.ExecuteSentence(CInt(Me.grdSentence.Item(Me.grdSentence.CurrentRowIndex, 0)), Me.grdSentence.Item(Me.grdSentence.CurrentRowIndex, 2).ToString)
                Exit Select

        End Select

    End Sub

#Region "Loads de Grillas"
    Private Sub LoadDelete()
        Try

        
            Dim ds As New DataSet
            Dim strSelect As String = "select id,nombre from zqueryname where querytype='Delete' order by id"
            ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            Me.DataGrid1.DataSource = ds.Tables(0)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LoadInsert()
        Dim ds As New DataSet
        Try



            Dim strSelect As String = "select id,nombre from zqueryname where querytype='Insert' order by id"
            ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            Me.DataGrid2.DataSource = ds.Tables(0)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
           
        End Try
    End Sub
    ''' <summary>
    ''' [Sebastián 24-04-09] Carga las consultas tipo select en la solapa select
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadSelects()
        Dim ds As New DataSet
        Try


            Dim strSelect As String = "select id,nombre from zqueryname where querytype='Select' order by id"
            ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            Me.DataGrid3.DataSource = ds.Tables(0)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
           
        End Try
    End Sub
    Private Sub LoadUpdates()
        Dim ds As New DataSet
        Try

            Dim strUpdate As String = "select id,nombre from zqueryname where querytype='Update' order by id"
            ds = Server.Con.ExecuteDataset(CommandType.Text, strUpdate)
            Me.grdUpd.DataSource = ds.Tables(0)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
           
        End Try
    End Sub
    Private Sub LoadSentences()
        Dim ds As New DataSet
        Try



            Dim strUpdate As String = "select zqsentence.id,nombre as Nombre,type as Tipo from zqsentence,zqueryname where zqueryname.id=zqsentence.id order by zqueryname.id"
            ds = Server.Con.ExecuteDataset(CommandType.Text, strUpdate)
            Me.grdSentence.DataSource = ds.Tables(0)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
           
        End Try
    End Sub
#End Region

    Public Event close()
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        RaiseEvent close()
    End Sub

    Private Sub DataGrid2_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles DataGrid2.Navigate

    End Sub

    Private Sub DataGrid3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid3.Click

    End Sub

    Private Sub DataGrid1_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles DataGrid1.Navigate

    End Sub
End Class
