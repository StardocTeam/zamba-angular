Imports Zamba.Core
Imports Zamba.Viewers
Imports System.Collections.Generic

Public Class UCComplete
    Inherits Zamba.AppBlock.ZControl
    Public Enum eModo
        SinFondo = 1
    End Enum
#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()
        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect

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
    Friend WithEvents CboComparator As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnDeleteAll As Zamba.AppBlock.ZButton
    Friend WithEvents btnDelete As Zamba.AppBlock.ZButton
    Friend WithEvents btnConfig As Zamba.AppBlock.ZButton
    Friend WithEvents ZButton1 As Zamba.AppBlock.ZButton
    Friend WithEvents ZButton2 As Zamba.AppBlock.ZButton
    Friend WithEvents cboindexs As System.Windows.Forms.ComboBox
    Friend WithEvents cbodoctype As System.Windows.Forms.ComboBox
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnAgregar As Zamba.AppBlock.ZButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents descrip As System.Windows.Forms.Label
    Friend WithEvents lstIndexs As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboIsIndexKey As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtColumna As System.Windows.Forms.TextBox
    Friend WithEvents txttabla As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSig As Zamba.AppBlock.ZButton
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents lblInsertedFilters As System.Windows.Forms.Label
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents btnParcialConfig As Zamba.AppBlock.ZButton
    Friend WithEvents ZBluePanel1 As Zamba.AppBlock.ZBluePanel
    Friend WithEvents btnExecute As Zamba.AppBlock.ZButton
    Friend WithEvents DocTypeID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DocTypeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Indice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tabla As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnaAsociada As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Clave As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Ejecutar As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chkIndexGroup As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterByIndex As System.Windows.Forms.CheckBox

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.CboComparator = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnDeleteAll = New Zamba.AppBlock.ZButton
        Me.btnDelete = New Zamba.AppBlock.ZButton
        Me.btnConfig = New Zamba.AppBlock.ZButton
        Me.ZButton1 = New Zamba.AppBlock.ZButton
        Me.ZButton2 = New Zamba.AppBlock.ZButton
        Me.cboindexs = New System.Windows.Forms.ComboBox
        Me.cbodoctype = New System.Windows.Forms.ComboBox
        Me.DataGrid1 = New System.Windows.Forms.DataGrid
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BtnAgregar = New Zamba.AppBlock.ZButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.descrip = New System.Windows.Forms.Label
        Me.lstIndexs = New System.Windows.Forms.ListBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboIsIndexKey = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtColumna = New System.Windows.Forms.TextBox
        Me.txttabla = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnSig = New Zamba.AppBlock.ZButton
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.DocTypeID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DocTypeName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Indice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Tabla = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColumnaAsociada = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Clave = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Ejecutar = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.lblInsertedFilters = New System.Windows.Forms.Label
        Me.txtFilter = New System.Windows.Forms.TextBox
        Me.btnParcialConfig = New Zamba.AppBlock.ZButton
        Me.ZBluePanel1 = New Zamba.AppBlock.ZBluePanel
        Me.chkIndexGroup = New System.Windows.Forms.CheckBox
        Me.btnExecute = New Zamba.AppBlock.ZButton
        Me.chkFilterByIndex = New System.Windows.Forms.CheckBox
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ZBluePanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CboComparator
        '
        Me.CboComparator.BackColor = System.Drawing.Color.White
        Me.CboComparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboComparator.Items.AddRange(New Object() {"=", "Like"})
        Me.CboComparator.Location = New System.Drawing.Point(360, 200)
        Me.CboComparator.Name = "CboComparator"
        Me.CboComparator.Size = New System.Drawing.Size(56, 21)
        Me.CboComparator.TabIndex = 15
        Me.CboComparator.Visible = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(320, 181)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 16)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Comparador:"
        Me.Label5.Visible = False
        '
        'btnDeleteAll
        '
        Me.btnDeleteAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteAll.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnDeleteAll.Location = New System.Drawing.Point(301, 315)
        Me.btnDeleteAll.Name = "btnDeleteAll"
        Me.btnDeleteAll.Size = New System.Drawing.Size(112, 24)
        Me.btnDeleteAll.TabIndex = 13
        Me.btnDeleteAll.Text = "Eliminar Todo"
        Me.btnDeleteAll.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnDelete.Location = New System.Drawing.Point(221, 315)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(74, 24)
        Me.btnDelete.TabIndex = 13
        Me.btnDelete.Text = "Eliminar"
        Me.btnDelete.Visible = False
        '
        'btnConfig
        '
        Me.btnConfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConfig.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnConfig.Location = New System.Drawing.Point(104, 315)
        Me.btnConfig.Name = "btnConfig"
        Me.btnConfig.Size = New System.Drawing.Size(110, 26)
        Me.btnConfig.TabIndex = 13
        Me.btnConfig.Text = "Ver Todo"
        Me.btnConfig.Visible = False
        '
        'ZButton1
        '
        Me.ZButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ZButton1.DialogResult = System.Windows.Forms.DialogResult.None
        Me.ZButton1.Location = New System.Drawing.Point(8, 315)
        Me.ZButton1.Name = "ZButton1"
        Me.ZButton1.Size = New System.Drawing.Size(90, 24)
        Me.ZButton1.TabIndex = 13
        Me.ZButton1.Text = "Cerrar"
        '
        'ZButton2
        '
        Me.ZButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ZButton2.DialogResult = System.Windows.Forms.DialogResult.None
        Me.ZButton2.Location = New System.Drawing.Point(221, 315)
        Me.ZButton2.Name = "ZButton2"
        Me.ZButton2.Size = New System.Drawing.Size(112, 24)
        Me.ZButton2.TabIndex = 13
        Me.ZButton2.Text = "< Atras"
        '
        'cboindexs
        '
        Me.cboindexs.BackColor = System.Drawing.Color.White
        Me.cboindexs.Location = New System.Drawing.Point(448, 48)
        Me.cboindexs.Name = "cboindexs"
        Me.cboindexs.Size = New System.Drawing.Size(40, 21)
        Me.cboindexs.TabIndex = 2
        Me.cboindexs.Visible = False
        '
        'cbodoctype
        '
        Me.cbodoctype.BackColor = System.Drawing.Color.White
        Me.cbodoctype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbodoctype.Location = New System.Drawing.Point(168, 176)
        Me.cbodoctype.Name = "cbodoctype"
        Me.cbodoctype.Size = New System.Drawing.Size(192, 21)
        Me.cbodoctype.TabIndex = 1
        '
        'DataGrid1
        '
        Me.DataGrid1.AlternatingBackColor = System.Drawing.Color.Lavender
        Me.DataGrid1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DataGrid1.BackgroundColor = System.Drawing.Color.LightGray
        Me.DataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGrid1.CaptionBackColor = System.Drawing.Color.LightSteelBlue
        Me.DataGrid1.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.DataGrid1.CaptionForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.FlatMode = True
        Me.DataGrid1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.DataGrid1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.GridLineColor = System.Drawing.Color.Gainsboro
        Me.DataGrid1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.DataGrid1.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.DataGrid1.HeaderForeColor = System.Drawing.Color.WhiteSmoke
        Me.DataGrid1.LinkColor = System.Drawing.Color.Teal
        Me.DataGrid1.Location = New System.Drawing.Point(488, 64)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.ParentRowsBackColor = System.Drawing.Color.Gainsboro
        Me.DataGrid1.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.SelectionBackColor = System.Drawing.Color.CadetBlue
        Me.DataGrid1.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        Me.DataGrid1.Size = New System.Drawing.Size(16, 16)
        Me.DataGrid1.TabIndex = 6
        Me.DataGrid1.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.GroupBox1.Location = New System.Drawing.Point(616, 56)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(16, 16)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Visible = False
        '
        'BtnAgregar
        '
        Me.BtnAgregar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnAgregar.Location = New System.Drawing.Point(416, 344)
        Me.BtnAgregar.Name = "BtnAgregar"
        Me.BtnAgregar.Size = New System.Drawing.Size(88, 24)
        Me.BtnAgregar.TabIndex = 5
        Me.BtnAgregar.Text = "Agregar"
        Me.BtnAgregar.Visible = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(504, 32)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Asistente para la configuración de Autocompletar"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Location = New System.Drawing.Point(8, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(496, 1)
        Me.Panel1.TabIndex = 8
        '
        'descrip
        '
        Me.descrip.BackColor = System.Drawing.Color.Transparent
        Me.descrip.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.descrip.Location = New System.Drawing.Point(8, 40)
        Me.descrip.Name = "descrip"
        Me.descrip.Size = New System.Drawing.Size(504, 56)
        Me.descrip.TabIndex = 9
        '
        'lstIndexs
        '
        Me.lstIndexs.BackColor = System.Drawing.Color.White
        Me.lstIndexs.Location = New System.Drawing.Point(120, 136)
        Me.lstIndexs.Name = "lstIndexs"
        Me.lstIndexs.Size = New System.Drawing.Size(192, 121)
        Me.lstIndexs.TabIndex = 10
        Me.lstIndexs.Visible = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(320, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 16)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Clave:"
        Me.Label4.Visible = False
        '
        'cboIsIndexKey
        '
        Me.cboIsIndexKey.BackColor = System.Drawing.Color.White
        Me.cboIsIndexKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboIsIndexKey.Items.AddRange(New Object() {"Si", "No"})
        Me.cboIsIndexKey.Location = New System.Drawing.Point(360, 136)
        Me.cboIsIndexKey.Name = "cboIsIndexKey"
        Me.cboIsIndexKey.Size = New System.Drawing.Size(56, 21)
        Me.cboIsIndexKey.TabIndex = 11
        Me.cboIsIndexKey.Visible = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(160, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 23)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Columna"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label3.Visible = False
        '
        'txtColumna
        '
        Me.txtColumna.BackColor = System.Drawing.Color.White
        Me.txtColumna.Location = New System.Drawing.Point(264, 160)
        Me.txtColumna.Name = "txtColumna"
        Me.txtColumna.Size = New System.Drawing.Size(96, 21)
        Me.txtColumna.TabIndex = 0
        Me.txtColumna.Visible = False
        '
        'txttabla
        '
        Me.txttabla.BackColor = System.Drawing.Color.White
        Me.txttabla.Location = New System.Drawing.Point(264, 208)
        Me.txttabla.Name = "txttabla"
        Me.txttabla.Size = New System.Drawing.Size(96, 21)
        Me.txttabla.TabIndex = 1
        Me.txttabla.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(160, 208)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Tabla o Vista"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label1.Visible = False
        '
        'btnSig
        '
        Me.btnSig.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSig.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSig.Location = New System.Drawing.Point(342, 315)
        Me.btnSig.Name = "btnSig"
        Me.btnSig.Size = New System.Drawing.Size(112, 24)
        Me.btnSig.TabIndex = 13
        Me.btnSig.Text = "Siguiente >"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DocTypeID, Me.DocTypeName, Me.Indice, Me.Tabla, Me.ColumnaAsociada, Me.Clave, Me.Ejecutar})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 43)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 4
        Me.DataGridView1.Size = New System.Drawing.Size(583, 266)
        Me.DataGridView1.TabIndex = 17
        Me.DataGridView1.Visible = False
        '
        'DocTypeID
        '
        Me.DocTypeID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DocTypeID.HeaderText = "ID del Tipo de Documento"
        Me.DocTypeID.Name = "DocTypeID"
        Me.DocTypeID.ReadOnly = True
        Me.DocTypeID.Width = 142
        '
        'DocTypeName
        '
        Me.DocTypeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DocTypeName.HeaderText = "Tipo de Documento"
        Me.DocTypeName.Name = "DocTypeName"
        Me.DocTypeName.ReadOnly = True
        Me.DocTypeName.Width = 114
        '
        'Indice
        '
        Me.Indice.HeaderText = "Indice"
        Me.Indice.Name = "Indice"
        Me.Indice.ReadOnly = True
        Me.Indice.Width = 130
        '
        'Tabla
        '
        Me.Tabla.HeaderText = "Tabla Asociada"
        Me.Tabla.Name = "Tabla"
        Me.Tabla.ReadOnly = True
        Me.Tabla.Width = 80
        '
        'ColumnaAsociada
        '
        Me.ColumnaAsociada.HeaderText = "Columna"
        Me.ColumnaAsociada.Name = "ColumnaAsociada"
        Me.ColumnaAsociada.ReadOnly = True
        Me.ColumnaAsociada.Width = 80
        '
        'Clave
        '
        Me.Clave.HeaderText = "Clave"
        Me.Clave.Name = "Clave"
        Me.Clave.ReadOnly = True
        Me.Clave.Width = 40
        '
        'Ejecutar
        '
        Me.Ejecutar.FillWeight = 60.0!
        Me.Ejecutar.HeaderText = "Ejecutar"
        Me.Ejecutar.MinimumWidth = 20
        Me.Ejecutar.Name = "Ejecutar"
        Me.Ejecutar.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Ejecutar.Width = 60
        '
        'lblInsertedFilters
        '
        Me.lblInsertedFilters.AutoSize = True
        Me.lblInsertedFilters.Location = New System.Drawing.Point(74, 147)
        Me.lblInsertedFilters.Name = "lblInsertedFilters"
        Me.lblInsertedFilters.Size = New System.Drawing.Size(75, 13)
        Me.lblInsertedFilters.TabIndex = 19
        Me.lblInsertedFilters.Text = "Valor del Filtro"
        Me.lblInsertedFilters.Visible = False
        '
        'txtFilter
        '
        Me.txtFilter.Location = New System.Drawing.Point(77, 163)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(351, 21)
        Me.txtFilter.TabIndex = 22
        Me.txtFilter.Visible = False
        '
        'btnParcialConfig
        '
        Me.btnParcialConfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnParcialConfig.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnParcialConfig.Location = New System.Drawing.Point(104, 315)
        Me.btnParcialConfig.Name = "btnParcialConfig"
        Me.btnParcialConfig.Size = New System.Drawing.Size(110, 26)
        Me.btnParcialConfig.TabIndex = 23
        Me.btnParcialConfig.Text = "Config. Actual"
        '
        'ZBluePanel1
        '
        Me.ZBluePanel1.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ZBluePanel1.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ZBluePanel1.Controls.Add(Me.chkIndexGroup)
        Me.ZBluePanel1.Controls.Add(Me.btnExecute)
        Me.ZBluePanel1.Controls.Add(Me.btnParcialConfig)
        Me.ZBluePanel1.Controls.Add(Me.txtFilter)
        Me.ZBluePanel1.Controls.Add(Me.lblInsertedFilters)
        Me.ZBluePanel1.Controls.Add(Me.DataGridView1)
        Me.ZBluePanel1.Controls.Add(Me.btnSig)
        Me.ZBluePanel1.Controls.Add(Me.Label1)
        Me.ZBluePanel1.Controls.Add(Me.txttabla)
        Me.ZBluePanel1.Controls.Add(Me.txtColumna)
        Me.ZBluePanel1.Controls.Add(Me.Label3)
        Me.ZBluePanel1.Controls.Add(Me.cboIsIndexKey)
        Me.ZBluePanel1.Controls.Add(Me.Label4)
        Me.ZBluePanel1.Controls.Add(Me.lstIndexs)
        Me.ZBluePanel1.Controls.Add(Me.descrip)
        Me.ZBluePanel1.Controls.Add(Me.Panel1)
        Me.ZBluePanel1.Controls.Add(Me.Label2)
        Me.ZBluePanel1.Controls.Add(Me.BtnAgregar)
        Me.ZBluePanel1.Controls.Add(Me.GroupBox1)
        Me.ZBluePanel1.Controls.Add(Me.DataGrid1)
        Me.ZBluePanel1.Controls.Add(Me.cbodoctype)
        Me.ZBluePanel1.Controls.Add(Me.cboindexs)
        Me.ZBluePanel1.Controls.Add(Me.ZButton2)
        Me.ZBluePanel1.Controls.Add(Me.ZButton1)
        Me.ZBluePanel1.Controls.Add(Me.btnConfig)
        Me.ZBluePanel1.Controls.Add(Me.btnDelete)
        Me.ZBluePanel1.Controls.Add(Me.btnDeleteAll)
        Me.ZBluePanel1.Controls.Add(Me.Label5)
        Me.ZBluePanel1.Controls.Add(Me.CboComparator)
        Me.ZBluePanel1.Controls.Add(Me.chkFilterByIndex)
        Me.ZBluePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ZBluePanel1.Location = New System.Drawing.Point(0, 0)
        Me.ZBluePanel1.Name = "ZBluePanel1"
        Me.ZBluePanel1.Size = New System.Drawing.Size(631, 344)
        Me.ZBluePanel1.TabIndex = 7
        '
        'chkIndexGroup
        '
        Me.chkIndexGroup.AutoSize = True
        Me.chkIndexGroup.Location = New System.Drawing.Point(75, 200)
        Me.chkIndexGroup.Name = "chkIndexGroup"
        Me.chkIndexGroup.Size = New System.Drawing.Size(95, 17)
        Me.chkIndexGroup.TabIndex = 25
        Me.chkIndexGroup.Text = "Agrupar índice"
        Me.chkIndexGroup.UseVisualStyleBackColor = True
        '
        'btnExecute
        '
        Me.btnExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExecute.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnExecute.Location = New System.Drawing.Point(419, 315)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(82, 24)
        Me.btnExecute.TabIndex = 24
        Me.btnExecute.Text = "Ejecutar"
        Me.btnExecute.Visible = False
        '
        'chkFilterByIndex
        '
        Me.chkFilterByIndex.AutoSize = True
        Me.chkFilterByIndex.BackColor = System.Drawing.Color.Transparent
        Me.chkFilterByIndex.Location = New System.Drawing.Point(137, 212)
        Me.chkFilterByIndex.Name = "chkFilterByIndex"
        Me.chkFilterByIndex.Size = New System.Drawing.Size(291, 17)
        Me.chkFilterByIndex.TabIndex = 26
        Me.chkFilterByIndex.Text = "Filtrar los resultados por un indice especifico al ejecutar"
        Me.chkFilterByIndex.UseVisualStyleBackColor = False
        '
        'UCComplete
        '
        Me.Controls.Add(Me.ZBluePanel1)
        Me.Name = "UCComplete"
        Me.Size = New System.Drawing.Size(631, 344)
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ZBluePanel1.ResumeLayout(False)
        Me.ZBluePanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private _indexkey As Boolean
    Private _columna As String
    Private _dialogRes As DialogResult
    Private zstep As Int16 = 1
    Private tempArray As New ArrayList
    'Dim tmpIndex As New Zamba.Core.Index
    'Dim ac As AutocompleteBC
    Dim ds As New DataSet

    Private Sub LoadDocTypes()
        'RemoveHandler cbodoctype.SelectedIndexChanged, AddressOf LoadIndexs
        Try
            Dim dsDocTypes As DataSet = DocTypesBusiness.GetDocTypesDsDocType
            Me.cbodoctype.DataSource = dsDocTypes.Tables(0)
            Me.cbodoctype.DisplayMember = dsDocTypes.Tables(0).Columns(0).ColumnName '"DOC_TYPE_NAME"
            Me.cbodoctype.ValueMember = dsDocTypes.Tables(0).Columns(9).ColumnName '"DOC_TYPE_ID"
            RemoveHandler cbodoctype.SelectedIndexChanged, AddressOf LoadIndexs
            AddHandler cbodoctype.SelectedIndexChanged, AddressOf LoadIndexs
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LoadIndexs(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Public Property IndexKey() As Boolean
        Get
            Return _indexkey
        End Get
        Set(ByVal Value As Boolean)
            _indexkey = Value
        End Set
    End Property

    Private Sub BtnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAgregar.Click
        Me.cbodoctype.Enabled = False
        Me.Agregar()
    End Sub
    Private Sub Agregar()
        Try
            If MessageBox.Show("¿El campo es clave?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.IndexKey = DirectCast(Me.lstIndexs.SelectedValue, Boolean)
            End If
            Me._columna = InputBox("El Dato se obtendra de " & Me.txttabla.Text & ". Ingrese el nombre de la columna: ", "Zamba")
            If Me.txttabla.Text.Trim = "" Then
                MessageBox.Show("Complete la tabla de origen", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If Me._columna <> "" Then
                BarcodesBusiness.Insertar(CInt(Me.cbodoctype.SelectedValue), CInt(Me.lstIndexs.SelectedValue), Me.txttabla.Text, Me._columna, Me.IndexKey, Me.chkIndexGroup.Checked, Me.chkFilterByIndex.Checked, Me.CboComparator.SelectedItem.ToString)
            End If
        Catch
        End Try
    End Sub

    Private Sub cbodoctype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbodoctype.SelectedIndexChanged
        cargarIndices()
    End Sub

    Private Sub UCComplete_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.LoadDocTypes()
        descrip.Text = "Seleccione el Tipo de Documento a configurar el Autocompletar."
        Me.chkIndexGroup.Visible = False

        ' cbodoctype.Text = "Tipo de Doc."
        '  cbodoctype.SelectedIndex = -1
    End Sub

    Private Sub btnSig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSig.Click
        cambioStep(True)
    End Sub

    ''' <summary>
    ''' Método que sirve para cambiar el aspecto del formulario a medida que el usuario presiona el botón "Siguiente"
    ''' </summary>
    ''' <param name="fw"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	23/12/2008  Modified    
    ''' </history>
    Private Sub cambioStep(ByVal fw As Boolean)

        If fw Then

            Select Case zstep

                Case 0

                    zstep = 1

                Case 1

                    If (cbodoctype.SelectedIndex <> -1) Then

                        cargarIndices()

                        descrip.Text = "Elija el indice a configurar, indique si es Campo Clave y  elija la comparacion."
                        cbodoctype.Visible = False
                        lstIndexs.Visible = True
                        lstIndexs.SelectedIndex = -1
                        cboIsIndexKey.Visible = True
                        CboComparator.Visible = True
                        CboComparator.SelectedIndex = 0
                        Label4.Visible = True
                        txtFilter.Visible = False
                        chkIndexGroup.Visible = False
                        lblInsertedFilters.Visible = False
                        chkFilterByIndex.Visible = False

                        zstep = 2

                    Else
                        MessageBox.Show("Debe seleccionar un tipo de documento.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Case 2

                    If ((lstIndexs.SelectedIndex <> -1) AndAlso (cboIsIndexKey.Text <> "")) Then

                        If (cboIsIndexKey.Text = "Si") Then
                            Me.IndexKey = True
                        Else
                            Me.IndexKey = False
                        End If

                        descrip.Text = "Ingrese el nombre de la tabla y la columna donde se encuentra el indice."
                        lstIndexs.Visible = False
                        cboIsIndexKey.Visible = False
                        CboComparator.Visible = False
                        Label4.Visible = False
                        txttabla.Visible = True
                        txtColumna.Visible = True
                        Label1.Visible = True
                        Label3.Visible = True
                        txtFilter.Visible = False
                        chkIndexGroup.Visible = False
                        lblInsertedFilters.Visible = False
                        chkFilterByIndex.Visible = False
                        zstep = 3

                    Else
                        MessageBox.Show("Debe seleccionar un índice y su condicion de clave.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Case 3

                    descrip.Text = "Complete con el valor por el cual realizar el filtro en la consulta"
                    lstIndexs.Visible = False
                    cboIsIndexKey.Visible = False
                    CboComparator.Visible = False
                    Label4.Visible = False
                    txttabla.Visible = False
                    txtColumna.Visible = False
                    Label1.Visible = False
                    Label3.Visible = False
                    'se muestran los controles para realizar el filtro[10/12/2008]
                    lblInsertedFilters.Visible = True
                    txtFilter.Visible = True
                    chkIndexGroup.Visible = True
                    chkFilterByIndex.Visible = False
                    zstep = 4

                    ' [Gaston]  23/12/2008  Si el índice es clave el checkbox "Agrupar índice" no se muestra 
                    If (cboIsIndexKey.Text = "Si") Then
                        chkIndexGroup.Visible = False
                    Else
                        chkIndexGroup.Visible = True
                    End If

                Case 4

                    If ((txttabla.Text <> "") AndAlso (txtColumna.Text <> "")) Then

                        Me._columna = txtColumna.Text.Trim
                        zstep = 4
                        'tmpIndex = lstIndexs.SelectedValue

                        If Me.IndexKey = True Then
                            descrip.Text = "Esta a punto de establecer la funcion autocompletar para el indice como CAMPO CLAVE!"
                        Else
                            descrip.Text = "Esta a punto de establecer la funcion autocompletar para el indice."
                        End If

                        txttabla.Visible = False
                        txtColumna.Visible = False
                        Label1.Visible = False
                        Label3.Visible = False
                        txtFilter.Visible = False
                        chkIndexGroup.Visible = False
                        lblInsertedFilters.Visible = False
                        chkFilterByIndex.Visible = False
                        btnSig.Text = "Finalizar"
                        zstep = 5

                    Else
                        MessageBox.Show("Debe completar todos los campos.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Case 5

                    ' Dim intClaves As Int32 = cuentaClaves()
                    'If intClaves = 1 Then
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Salvando Autocompletar")
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Tipo de Documento:" & Me.cbodoctype.SelectedValue.ToString())
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Indice:" & lstIndexs.SelectedValue.ToString())
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Tabla:" & Me.txttabla.Text)
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Columna:" & Me._columna)
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Clave:" & Me.IndexKey)
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Comparador:" & CboComparator.SelectedItem.ToString())
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Valor para where: " & txtFilter.Text)
                    BarcodesBusiness.Insertar(Int32.Parse(Me.cbodoctype.SelectedValue.ToString), Int32.Parse(lstIndexs.SelectedValue.ToString), Me.txttabla.Text, Me._columna, Me.IndexKey, Me.chkIndexGroup.Checked, Me.chkFilterByIndex.Checked, Me.CboComparator.SelectedItem.ToString, txtFilter.Text)
                    tempArray.Add(Me.IndexKey)

                    If MessageBox.Show("Indice configurado. " & ControlChars.NewLine & "Desea configurar otro índice?", "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        'invertIndices()
                        Me.ParentForm.Dispose()
                    Else
                        zstep = 3
                        cambioStep(False)
                    End If

                    'Else
                    'If intClaves > 1 Then
                    'MessageBox.Show("Ya tiene configurado un campo clave.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'Else
                    '    MessageBox.Show("Debe configurar un campo clave.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'End If
                    'End If

            End Select

        Else

            Select Case zstep

                Case 1

                    zstep = 0

                Case 2

                    descrip.Text = "Seleccione el Tipo de Documento a configurar el Autocompletar."
                    '   ZButton3.Visible = False
                    cbodoctype.Visible = True
                    lstIndexs.Visible = False
                    lstIndexs.SelectedIndex = -1
                    cboIsIndexKey.Visible = False
                    CboComparator.Visible = False
                    Label4.Visible = False
                    txtFilter.Visible = False
                    chkIndexGroup.Visible = False
                    lblInsertedFilters.Visible = False
                    chkFilterByIndex.Visible = True
                    zstep = 1

                Case 3

                    descrip.Text = "Elija el indice a configurar, e indique si es Campo Clave."
                    txttabla.Visible = False
                    txtColumna.Visible = False
                    Label1.Visible = False
                    Label3.Visible = False
                    lstIndexs.Visible = True
                    Label4.Visible = True
                    cboIsIndexKey.Visible = True
                    CboComparator.Visible = True
                    btnSig.Text = "Siguiente >"
                    txtFilter.Visible = False
                    chkIndexGroup.Visible = False
                    lblInsertedFilters.Visible = False
                    chkFilterByIndex.Visible = False
                    zstep = 2

                Case 4

                    If (cboIsIndexKey.Text = "Si") Then
                        Me.IndexKey = True
                    Else
                        Me.IndexKey = False
                    End If

                    descrip.Text = "Ingrese el nombre de la tabla y la columna donde se encuentra el indice."
                    txttabla.Visible = True
                    txtColumna.Visible = True
                    Label1.Visible = True
                    Label3.Visible = True
                    btnSig.Text = "Siguiente >"
                    txtFilter.Visible = False
                    chkIndexGroup.Visible = False
                    lblInsertedFilters.Visible = False
                    chkFilterByIndex.Visible = False
                    zstep = 3

                Case 5

                    descrip.Text = "Complete con el valor por el cual realizar el filtro en la consulta"
                    lstIndexs.Visible = False
                    cboIsIndexKey.Visible = False
                    CboComparator.Visible = False
                    Label4.Visible = False
                    txttabla.Visible = False
                    txtColumna.Visible = False
                    Label1.Visible = False
                    Label3.Visible = False
                    'se muestran los controles para realizar el filtro[10/12/2008]
                    lblInsertedFilters.Visible = True
                    txtFilter.Visible = True
                    chkIndexGroup.Visible = True
                    chkFilterByIndex.Visible = False
                    zstep = 4

                    ' [Gaston]  23/12/2008  Si el índice es clave el checkbox "Agrupar índice" no se muestra 
                    If (cboIsIndexKey.Text = "Si") Then
                        chkIndexGroup.Visible = False
                    Else
                        chkIndexGroup.Visible = True
                    End If

            End Select

        End If

    End Sub

    Private Sub ZButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZButton2.Click
        cambioStep(False)
    End Sub

    Private Sub ZButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZButton1.Click
        'invertIndices()
        'FEDE
        'comentado para probar metodo de invertir luego descomentar
        If DataGridView1.Visible = True Then
            DataGridView1.Visible = False
            ZButton2.Visible = True
            btnSig.Visible = True
            btnParcialConfig.Visible = True
            btnDelete.Visible = False
            btnDeleteAll.Visible = False
            btnExecute.Visible = False
        Else
            Me.ParentForm.Dispose()
        End If
    End Sub

    Private Sub ZButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfig.Click
        cargarLista()
        Me.DataGridView1.Columns("Ejecutar").Visible = False
        Me.btnConfig.Visible = False
        Me.btnParcialConfig.Visible = True
    End Sub

    ''' <summary>
    ''' Carga la grilla
    ''' </summary>
    ''' <history>Marcelo modified 16/12/2008<history>
    ''' <remarks></remarks>
    Private Sub cargarLista()
        Try
            Dim dsBarcode As DataTable
            If DataGridView1.SelectedRows.Count > 0 Then
                'DataGridView1.Visible = True
                'btnDelete.Visible = True
                'btnDeleteAll.Visible = True
                'ZButton2.Visible = False
                'btnSig.Visible = False
                'ZButton3.Visible = False

                ''Dim strSelect As String = "select * from zbarcodecomplete where doctypeid=" & cbodoctype.SelectedValue.ToString
                'Dim strSelect As String = "SELECT * FROM ZBARCODECOMPLETE"

                'ds = Servers.Server.Con.ExecuteDataset(CommandType.Text, strSelect)
                dsBarcode = BarcodesBusiness.GetAutoCompleteIndexs(DataGridView1.SelectedRows(0).Cells("DocTypeID").Value.ToString())
                DataGridView1.Rows.Clear()
                If Not dsBarcode Is Nothing Then
                    'For i As Int32 = 0 To dsBarcode.Tables(0).Rows.Count - 1
                    'Dim strLine As String = String.Empty
                    'index = dsBarcode.Tables(0).Rows(i)("Columna").ToString
                    'clave = dsBarcode.Tables(0).Rows(i)("Clave").ToString()
                    'If clave = True Then
                    '    strLine += "   -   <<< CLAVE >>>"
                    '    lista.Items.Add(strLine)
                    'Else
                    '    lista.Items.Add(index)
                    'End If
                    'Next I

                    For Each r As DataRow In dsBarcode.Rows
                        DataGridView1.Rows.Add()
                        DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(0).Value = r.Item("DOCTYPEID").ToString()
                        DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(1).Value = DocTypesBusiness.GetDocTypeName(Int32.Parse(r.Item("DOCTYPEID").ToString), True)
                        DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(2).Value = IndexsBusiness.GetIndexName(Int32.Parse(r.Item("INDEXID").ToString), True)
                        DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(3).Value = r.Item("TABLA").ToString
                        DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(4).Value = r.Item("COLUMNA").ToString
                        If r.Item("CLAVE").ToString = "1" Then
                            DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(5).Value = True
                        Else
                            DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(5).Value = False
                        End If
                    Next
                End If
            Else
                MessageBox.Show("No ha seleccionado una fila para mostrar", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Function cuentaClaves() As Int32
        Dim con As Int32
        For i As Int16 = 0 To CShort(tempArray.Count - 1)
            If CType(tempArray.Item(i), Boolean) = True Then
                con += 1
            End If
        Next
        Return con
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If IsNothing(DataGridView1.SelectedRows) OrElse DataGridView1.SelectedRows.Count = 0 Then Exit Sub
        'Recorre cada Row Seleccionada de la grilla
        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            'Verifica que el campo sea clave
            If Boolean.Parse(row.Cells(5).Value.ToString) = True Then
                'Mensaje al usuario y procede a eliminar todos los registros de ese doctype, que quedarian invalidos
                If MessageBox.Show("Esta por eliminar un campo clave, Se eliminaran todos los registros asociados, ya que quedaran invalidos, Continuar?", "Zamba", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    BarcodesBusiness.deleteZbarcodecomplete(row.Cells(0).Value.ToString)
                End If
            Else
                BarcodesBusiness.deleteZbarcodecomplete(Int64.Parse(row.Cells(0).Value.ToString()), IndexsBusiness.GetIndexId(row.Cells(2).Value.ToString))
            End If
        Next

        CargarListaCompleto()
        'Dim i As Int32
        'Try
        '    i = DataGridView1.SelectedIndex
        '    If i > -1 Then
        '        Evento_btnDelete_Click()
        '        'Dim index As String = ds.Tables(0).Rows(i)("Columna")
        '        'Dim strdelete As String = "delete from zbarcodecomplete where doctypeid=" & cbodoctype.SelectedValue.ToString & " and Columna='" & index.ToString & "'"
        '        'Try
        '        '    Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
        '        'Catch ex As Exception
        '        '    MessageBox.Show(ex.ToString)
        '        'End Try
        '        'cargarLista()
        '    Else
        '        MessageBox.Show("Debe seleccionar una fila.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End If
        'Catch
        'End Try
    End Sub

    'Private Sub Evento_btnDelete_Click()
    '    Dim index As String = ds.Tables(0).Rows(0).Item("Columna").ToString()
    '    Try
    '        'Dim strdelete As String = "delete from zbarcodecomplete where doctypeid=" & cbodoctype.SelectedValue.ToString & " and Columna='" & index.ToString & "'"
    '        'Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    '        BarcodesBusiness.deleteZbarcodecomplete(cbodoctype.SelectedValue.ToString, index.ToString)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString)
    '    End Try
    '    cargarLista()
    'End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Cargar Indices
    ''' </summary>
    ''' <remarks>
    ''' Modificado, esto no deberia estar asi.
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	04/08/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub cargarIndices()
        Try
            'If cbodoctype.selectedvalue > 0 Then
            If cbodoctype.Items.Count > 0 Then
                Dim DSIndex As New Zamba.Core.DSIndex
                'DSIndex = Core.Indexs_Factory.GetIndexSchema(cbodoctype.SelectedValue)
                DSIndex = Zamba.Core.Indexs.Schema.GetIndexSchema(CInt(cbodoctype.SelectedValue))
                lstIndexs.DataSource = DSIndex.DOC_INDEX
                Me.lstIndexs.DisplayMember = DSIndex.DOC_INDEX.Columns(1).ColumnName ' "INDEX_NAME")
                Me.lstIndexs.ValueMember = DSIndex.DOC_INDEX.Columns(0).ColumnName    ' "INDEX_ID"
                Me.lstIndexs.Refresh()
            End If
        Catch
        End Try
    End Sub
    Private Sub invertIndices()
        Dim dsi As DataTable, i As Int32
        Dim TotalIndex As Int32
        Dim Tabla As New ArrayList, Columna As New ArrayList
        Dim Orden As New ArrayList, Clave As New ArrayList, Indexid As New ArrayList, newOrden As New ArrayList
        Try
            'dsi = Servers.Server.Con.ExecuteDataset(CommandType.Text, "select * from zbarcodecomplete where doctypeid=" & cbodoctype.SelectedValue.ToString)
            dsi = BarcodesBusiness.GetAutoCompleteIndexs(cbodoctype.SelectedValue.ToString)
            TotalIndex = dsi.Rows.Count - 1
            For i = 0 To TotalIndex
                Tabla.Add(dsi.Rows(i)("Tabla").ToString)
                Columna.Add(dsi.Rows(i)("Columna").ToString)
                Indexid.Add(dsi.Rows(i)("Indexid"))
                Clave.Add(dsi.Rows(i)("Clave"))
                Orden.Add(dsi.Rows(i)("Orden"))
            Next i
            Dim ti As Int32 = TotalIndex
            Dim k As Int32 = 0
            For k = 0 To TotalIndex
                newOrden.Add(Orden.Item(ti))
                ti -= 1
            Next
            'Servers.Server.Con.ExecuteNonQuery(CommandType.Text, "delete from zbarcodecomplete where doctypeid=" & cbodoctype.SelectedValue.ToString)
            BarcodesBusiness.deleteZbarcodecomplete(cbodoctype.SelectedValue.ToString)
            For i = 0 To TotalIndex
                BarcodesBusiness.insertZbarcodecomplete(CBool(Clave.Item(i)), cbodoctype.SelectedValue.ToString, Indexid.Item(i).ToString, Tabla.Item(i).ToString, Columna.Item(i).ToString, newOrden.Item(i).ToString)
                'Dim values As String
                'If Clave.Item(i) Then
                '    values = "(" & cbodoctype.SelectedValue.ToString & "," & Indexid.Item(i).ToString & ",'" & Tabla.Item(i).ToString & "','" & Columna.Item(i).ToString & "',1," & newOrden.Item(i).ToString & ")"
                'Else
                '    values = "(" & cbodoctype.SelectedValue.ToString & "," & Indexid.Item(i).ToString & ",'" & Tabla.Item(i).ToString & "','" & Columna.Item(i).ToString & "',0," & newOrden.Item(i).ToString & ")"
                'End If
                'Servers.Server.Con.ExecuteNonQuery(CommandType.Text, "insert into zbarcodecomplete (doctypeid,indexid,tabla,columna,clave,orden) values " & values)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub ZButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAll.Click
        Try
            'Servers.Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE FROM ZBARCODECOMPLETE")
            BarcodesBusiness.deleteZbarcodecomplete()
            cargarLista()
        Catch ex As Exception
            MessageBox.Show("Error al borrar de la tabla. - " & ex.ToString)
        End Try

    End Sub

    Public WriteOnly Property modo() As Boolean
        Set(ByVal Value As Boolean)
            ZButton1.Visible = Value
        End Set
    End Property

    Public Property color1() As System.Drawing.Color
        Get
            Return ZBluePanel1.Color1
        End Get
        Set(ByVal Value As System.Drawing.Color)
            ZBluePanel1.Color1 = Value
        End Set
    End Property

    Public Property color2() As System.Drawing.Color
        Get
            Return ZBluePanel1.Color2
        End Get
        Set(ByVal Value As System.Drawing.Color)
            ZBluePanel1.Color2 = Value
        End Set
    End Property

    ''' <summary>
    ''' este método realiza una carga en la grilla de los autocomplete que
    ''' pero mostrando solo los campos claves por distinto doctype.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnParcialConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnParcialConfig.Click
        CargarListaCompleto()
    End Sub

    ''' <summary>
    ''' Muestra la lista completa en la grilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarListaCompleto()
        Dim dsBarcode As DataSet = BarcodesBusiness.getZBARCODECOMPLETEWithDistinctDocType()
        DataGridView1.Visible = True
        DataGridView1.Rows.Clear()
        btnDelete.Visible = True
        btnDeleteAll.Visible = True
        btnExecute.Visible = True
        ZButton2.Visible = False
        btnSig.Visible = False
        Me.DocTypeID.Visible = False

        For Each r As DataRow In dsBarcode.Tables(0).Rows
            DataGridView1.Rows.Add()
            DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(0).Value = r.Item("DOCTYPEID").ToString()
            DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(1).Value = DocTypesBusiness.GetDocTypeName(Int32.Parse(r.Item("DOCTYPEID").ToString), True)
            DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(2).Value = IndexsBusiness.GetIndexName(Int32.Parse(r.Item("INDEXID").ToString), True)
            DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(3).Value = r.Item("TABLA").ToString()
            DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(4).Value = r.Item("COLUMNA").ToString()

            If r.Item("CLAVE").ToString = "1" Then
                DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(5).Value = True
            Else
                DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(5).Value = False
            End If
        Next

        Me.DataGridView1.Columns("Ejecutar").Visible = True
        Me.btnConfig.Visible = True
        Me.btnParcialConfig.Visible = False
    End Sub


    ''' <summary>
    ''' Se encarga de la ejecucion de los autocompletar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    ''' Gaston    11/12/2008      Created
    ''' Marcelo   17/12/2008      Modified
    ''' </history>
    ''' <remarks></remarks>
    Private Sub btnExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExecute.Click
        'Colección con los id de tipos de documento que se seleccionan con la columna "Ejecutar"
        Dim selected As Boolean
        Dim doExecute As Boolean
        Dim IndexFilters As Dictionary(Of String, String) = Nothing
        Dim DocTypeId As Integer

        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            ' Si la celda tiene un tilde
            If (CType(row.Cells("Ejecutar").Value, Boolean) = True) Then

                DocTypeId = row.Cells("DocTypeId").Value

                Trace.WriteLine("Ejecutando autocomplete """ & row.Cells(1).Value & """ - DocTypeId: " & DocTypeId)

                selected = True
                doExecute = True

                'Si tiene filtro por indice especifico pedir los datos
                If BarcodesBusiness.HasSpecificIndexFilters(DocTypeId) Then

                    Trace.WriteLine("Autocomplete configurado con indices especificos")

                    RemoveHandler ZClass.eHandleEventDialogReseult, AddressOf DialogResultEventHandler
                    AddHandler ZClass.eHandleEventDialogReseult, AddressOf DialogResultEventHandler

                    Dim ucIndexAutoComplete As New UCIndexViewerAutocompleteManual(True, False, True, True, False, False)
                    Dim f As New Form

                    Trace.WriteLine("Generando indices en el formulario de seleccion de indices")
                    ucIndexAutoComplete.ShowIndexs(New Results_Business().GetNewResult(DocTypeId, String.Empty))

                    ucIndexAutoComplete.Dock = DockStyle.Fill
                    ucIndexAutoComplete.ShowPanelWithAceptandCancel()
                    ucIndexAutoComplete.Refresh()

                    f.ShowIcon = False
                    f.Text = "Indices para filtrar"
                    f.ShowInTaskbar = False
                    f.Controls.Add(ucIndexAutoComplete)
                    f.StartPosition = FormStartPosition.CenterScreen
                    f.MaximizeBox = False
                    f.ControlBox = False

                    Trace.WriteLine("Mostrando formulario de seleccion de indices")

                    f.ShowDialog()

                    If _dialogRes = DialogResult.OK Then
                        Dim Indices As ArrayList = ucIndexAutoComplete.GetIndexs()

                        IndexFilters = New Dictionary(Of String, String)

                        For Each ind As Index In Indices
                            If Not String.IsNullOrEmpty(ind.DataTemp) Then
                                IndexFilters.Add(ind.ID, ind.DataTemp)
                                Trace.WriteLine("Indice seleccionado: """ & ind.Name & """ - ID: " & ind.ID & " - Valor: """ & ind.DataTemp & """")
                            End If
                        Next
                    Else
                        If MessageBox.Show("¿Desea ejecutar el autocomplete sin aplicar ningun filtro?", "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                            Trace.WriteLine("Se decidio ejecutar el autocomplete sin ingresar valores para los indices")
                            doExecute = False
                        End If
                    End If

                    ucIndexAutoComplete.Dispose()
                    ucIndexAutoComplete = Nothing

                    f.Dispose()
                    f = Nothing

                End If

                If doExecute Then
                    Trace.WriteLine("Ejecutando autocomplete")
                    AutoCompleteBarcode_FactoryBusiness.ExecuteAutocomplete(DocTypeId.ToString(), IndexFilters)
                End If
            End If
        Next

        If selected Then
            If doExecute Then
                Trace.WriteLine("Finalizacion ejecucion autocomplete")
                MessageBox.Show("Se ha terminado de ejecutar el proceso", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Trace.WriteLine("Se cancelo la ejecucion del autocomplete")
                MessageBox.Show("Se ha cancelado la ejecucion del proceso", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("Debe tildar la casilla ejecutar para correr el proceso", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub DialogResultEventHandler(ByVal res As DialogResult)
        _dialogRes = res
    End Sub

End Class
