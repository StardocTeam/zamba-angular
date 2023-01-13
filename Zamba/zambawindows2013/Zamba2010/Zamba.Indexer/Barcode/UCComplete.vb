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
    Friend WithEvents CboComparator As ComboBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents btnDeleteAll As ZButton
    Friend WithEvents btnDelete As ZButton
    Friend WithEvents btnConfig As ZButton
    Friend WithEvents ZButton As ZButton
    Friend WithEvents ZButton2 As ZButton
    Friend WithEvents cboindexs As ComboBox
    Friend WithEvents cbodoctype As ComboBox
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents BtnAgregar As ZButton
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents descrip As ZLabel
    Friend WithEvents lstIndexs As ListBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents cboIsIndexKey As ComboBox
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents txtColumna As TextBox
    Friend WithEvents txttabla As TextBox
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents btnSig As ZButton
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents lblInsertedFilters As ZLabel
    Friend WithEvents txtFilter As TextBox
    Friend WithEvents btnParcialConfig As ZButton
    Friend WithEvents ZPanel As ZPanel
    Friend WithEvents btnExecute As ZButton
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
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        CboComparator = New ComboBox
        Label5 = New ZLabel
        btnDeleteAll = New ZButton
        btnDelete = New ZButton
        btnConfig = New ZButton
        ZButton = New ZButton
        ZButton2 = New ZButton
        cboindexs = New ComboBox
        cbodoctype = New ComboBox
        DataGrid1 = New System.Windows.Forms.DataGrid
        GroupBox1 = New GroupBox
        BtnAgregar = New ZButton
        Label2 = New ZLabel
        Panel1 = New System.Windows.Forms.Panel
        descrip = New ZLabel
        lstIndexs = New ListBox
        Label4 = New ZLabel
        cboIsIndexKey = New ComboBox
        Label3 = New ZLabel
        txtColumna = New TextBox
        txttabla = New TextBox
        Label1 = New ZLabel
        btnSig = New ZButton
        DataGridView1 = New System.Windows.Forms.DataGridView
        DocTypeID = New System.Windows.Forms.DataGridViewTextBoxColumn
        DocTypeName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Indice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Tabla = New System.Windows.Forms.DataGridViewTextBoxColumn
        ColumnaAsociada = New System.Windows.Forms.DataGridViewTextBoxColumn
        Clave = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Ejecutar = New System.Windows.Forms.DataGridViewCheckBoxColumn
        lblInsertedFilters = New ZLabel
        txtFilter = New TextBox
        btnParcialConfig = New ZButton
        ZPanel = New ZPanel
        chkIndexGroup = New System.Windows.Forms.CheckBox
        btnExecute = New ZButton
        chkFilterByIndex = New System.Windows.Forms.CheckBox
        CType(DataGrid1, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        ZPanel.SuspendLayout()
        SuspendLayout()
        '
        'CboComparator
        '
        CboComparator.BackColor = System.Drawing.Color.White
        CboComparator.DropDownStyle = ComboBoxStyle.DropDownList
        CboComparator.Items.AddRange(New Object() {"=", "Like"})
        CboComparator.Location = New System.Drawing.Point(360, 200)
        CboComparator.Name = "CboComparator"
        CboComparator.Size = New System.Drawing.Size(56, 21)
        CboComparator.TabIndex = 15
        CboComparator.Visible = False
        '
        'Label5
        '
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Location = New System.Drawing.Point(320, 181)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(72, 16)
        Label5.TabIndex = 16
        Label5.Text = "Comparador:"
        Label5.Visible = False
        '
        'btnDeleteAll
        '
        btnDeleteAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnDeleteAll.DialogResult = System.Windows.Forms.DialogResult.None
        btnDeleteAll.Location = New System.Drawing.Point(301, 315)
        btnDeleteAll.Name = "btnDeleteAll"
        btnDeleteAll.Size = New System.Drawing.Size(112, 24)
        btnDeleteAll.TabIndex = 13
        btnDeleteAll.Text = "Eliminar Todo"
        btnDeleteAll.Visible = False
        '
        'btnDelete
        '
        btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnDelete.DialogResult = System.Windows.Forms.DialogResult.None
        btnDelete.Location = New System.Drawing.Point(221, 315)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New System.Drawing.Size(74, 24)
        btnDelete.TabIndex = 13
        btnDelete.Text = "Eliminar"
        btnDelete.Visible = False
        '
        'btnConfig
        '
        btnConfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnConfig.DialogResult = System.Windows.Forms.DialogResult.None
        btnConfig.Location = New System.Drawing.Point(104, 315)
        btnConfig.Name = "btnConfig"
        btnConfig.Size = New System.Drawing.Size(110, 26)
        btnConfig.TabIndex = 13
        btnConfig.Text = "Ver Todo"
        btnConfig.Visible = False
        '
        'ZButton
        '
        ZButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ZButton.DialogResult = System.Windows.Forms.DialogResult.None
        ZButton.Location = New System.Drawing.Point(8, 315)
        ZButton.Name = "ZButton"
        ZButton.Size = New System.Drawing.Size(90, 24)
        ZButton.TabIndex = 13
        ZButton.Text = "Cerrar"
        '
        'ZButton2
        '
        ZButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ZButton2.DialogResult = System.Windows.Forms.DialogResult.None
        ZButton2.Location = New System.Drawing.Point(221, 315)
        ZButton2.Name = "ZButton2"
        ZButton2.Size = New System.Drawing.Size(112, 24)
        ZButton2.TabIndex = 13
        ZButton2.Text = "< Atras"
        '
        'cboindexs
        '
        cboindexs.BackColor = System.Drawing.Color.White
        cboindexs.Location = New System.Drawing.Point(448, 48)
        cboindexs.Name = "cboindexs"
        cboindexs.Size = New System.Drawing.Size(40, 21)
        cboindexs.TabIndex = 2
        cboindexs.Visible = False
        '
        'cbodoctype
        '
        cbodoctype.BackColor = System.Drawing.Color.White
        cbodoctype.DropDownStyle = ComboBoxStyle.DropDownList
        cbodoctype.Location = New System.Drawing.Point(168, 176)
        cbodoctype.Name = "cbodoctype"
        cbodoctype.Size = New System.Drawing.Size(192, 21)
        cbodoctype.TabIndex = 1
        '
        'DataGrid1
        '
        DataGrid1.AlternatingBackColor = System.Drawing.Color.Lavender
        DataGrid1.BackColor = System.Drawing.Color.WhiteSmoke
        DataGrid1.BackgroundColor = System.Drawing.Color.LightGray
        DataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGrid1.CaptionBackColor = System.Drawing.Color.LightSteelBlue
        DataGrid1.CaptionFont = New Font("Microsoft Sans Serif", 8.0!)
        DataGrid1.CaptionForeColor = System.Drawing.Color.MidnightBlue
        DataGrid1.DataMember = ""
        DataGrid1.FlatMode = True
        DataGrid1.Font = New Font("Microsoft Sans Serif", 8.0!)
        DataGrid1.ForeColor = System.Drawing.Color.MidnightBlue
        DataGrid1.GridLineColor = System.Drawing.Color.Gainsboro
        DataGrid1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        DataGrid1.HeaderBackColor = System.Drawing.Color.MidnightBlue
        DataGrid1.HeaderFont = New Font("Microsoft Sans Serif", 8.0!)
        DataGrid1.HeaderForeColor = System.Drawing.Color.WhiteSmoke
        DataGrid1.LinkColor = System.Drawing.Color.Teal
        DataGrid1.Location = New System.Drawing.Point(488, 64)
        DataGrid1.Name = "DataGrid1"
        DataGrid1.ParentRowsBackColor = System.Drawing.Color.Gainsboro
        DataGrid1.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        DataGrid1.SelectionBackColor = System.Drawing.Color.CadetBlue
        DataGrid1.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        DataGrid1.Size = New System.Drawing.Size(16, 16)
        DataGrid1.TabIndex = 6
        DataGrid1.Visible = False
        '
        'GroupBox1
        '
        GroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        GroupBox1.Location = New System.Drawing.Point(616, 56)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(16, 16)
        GroupBox1.TabIndex = 4
        GroupBox1.TabStop = False
        GroupBox1.Visible = False
        '
        'BtnAgregar
        '
        BtnAgregar.DialogResult = System.Windows.Forms.DialogResult.None
        BtnAgregar.Location = New System.Drawing.Point(416, 344)
        BtnAgregar.Name = "BtnAgregar"
        BtnAgregar.Size = New System.Drawing.Size(88, 24)
        BtnAgregar.TabIndex = 5
        BtnAgregar.Text = "Agregar"
        BtnAgregar.Visible = False
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Tahoma", 14.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(8, 8)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(504, 32)
        Label2.TabIndex = 7
        Label2.Text = "Asistente para la configuración de Autocompletar"
        '
        'Panel1
        '
        Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel1.Location = New System.Drawing.Point(8, 32)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(496, 1)
        Panel1.TabIndex = 8
        '
        'descrip
        '
        descrip.BackColor = System.Drawing.Color.Transparent
        descrip.Font = New Font("Tahoma", 11.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        descrip.Location = New System.Drawing.Point(8, 40)
        descrip.Name = "descrip"
        descrip.Size = New System.Drawing.Size(504, 56)
        descrip.TabIndex = 9
        '
        'lstIndexs
        '
        lstIndexs.BackColor = System.Drawing.Color.White
        lstIndexs.Location = New System.Drawing.Point(120, 136)
        lstIndexs.Name = "lstIndexs"
        lstIndexs.Size = New System.Drawing.Size(192, 121)
        lstIndexs.TabIndex = 10
        lstIndexs.Visible = False
        '
        'Label4
        '
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Location = New System.Drawing.Point(320, 136)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(64, 16)
        Label4.TabIndex = 12
        Label4.Text = "Clave:"
        Label4.Visible = False
        '
        'cboIsIndexKey
        '
        cboIsIndexKey.BackColor = System.Drawing.Color.White
        cboIsIndexKey.DropDownStyle = ComboBoxStyle.DropDownList
        cboIsIndexKey.Items.AddRange(New Object() {"Si", "No"})
        cboIsIndexKey.Location = New System.Drawing.Point(360, 136)
        cboIsIndexKey.Name = "cboIsIndexKey"
        cboIsIndexKey.Size = New System.Drawing.Size(56, 21)
        cboIsIndexKey.TabIndex = 11
        cboIsIndexKey.Visible = False
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.White
        Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label3.Location = New System.Drawing.Point(160, 160)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(100, 23)
        Label3.TabIndex = 1
        Label3.Text = "Columna"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        Label3.Visible = False
        '
        'txtColumna
        '
        txtColumna.BackColor = System.Drawing.Color.White
        txtColumna.Location = New System.Drawing.Point(264, 160)
        txtColumna.Name = "txtColumna"
        txtColumna.Size = New System.Drawing.Size(96, 21)
        txtColumna.TabIndex = 0
        txtColumna.Visible = False
        '
        'txttabla
        '
        txttabla.BackColor = System.Drawing.Color.White
        txttabla.Location = New System.Drawing.Point(264, 208)
        txttabla.Name = "txttabla"
        txttabla.Size = New System.Drawing.Size(96, 21)
        txttabla.TabIndex = 1
        txttabla.Visible = False
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.White
        Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label1.Location = New System.Drawing.Point(160, 208)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(100, 23)
        Label1.TabIndex = 1
        Label1.Text = "Tabla o Vista"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        Label1.Visible = False
        '
        'btnSig
        '
        btnSig.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnSig.DialogResult = System.Windows.Forms.DialogResult.None
        btnSig.Location = New System.Drawing.Point(342, 315)
        btnSig.Name = "btnSig"
        btnSig.Size = New System.Drawing.Size(112, 24)
        btnSig.TabIndex = 13
        btnSig.Text = "Siguiente >"
        '
        'DataGridView1
        '
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AllowUserToResizeRows = False
        DataGridView1.BackgroundColor = System.Drawing.Color.White
        DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {DocTypeID, DocTypeName, Indice, Tabla, ColumnaAsociada, Clave, Ejecutar})
        DataGridView1.Location = New System.Drawing.Point(12, 43)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 4
        DataGridView1.Size = New System.Drawing.Size(583, 266)
        DataGridView1.TabIndex = 17
        DataGridView1.Visible = False
        '
        'DocTypeID
        '
        DocTypeID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DocTypeID.HeaderText = "ID de la Entidad"
        DocTypeID.Name = "DocTypeID"
        DocTypeID.ReadOnly = True
        DocTypeID.Width = 142
        '
        'DocTypeName
        '
        DocTypeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DocTypeName.HeaderText = "Entidad"
        DocTypeName.Name = "DocTypeName"
        DocTypeName.ReadOnly = True
        DocTypeName.Width = 114
        '
        'Indice
        '
        Indice.HeaderText = "Atributo"
        Indice.Name = "Atributo"
        Indice.ReadOnly = True
        Indice.Width = 130
        '
        'Tabla
        '
        Tabla.HeaderText = "Tabla Asociada"
        Tabla.Name = "Tabla"
        Tabla.ReadOnly = True
        Tabla.Width = 80
        '
        'ColumnaAsociada
        '
        ColumnaAsociada.HeaderText = "Columna"
        ColumnaAsociada.Name = "ColumnaAsociada"
        ColumnaAsociada.ReadOnly = True
        ColumnaAsociada.Width = 80
        '
        'Clave
        '
        Clave.HeaderText = "Clave"
        Clave.Name = "Clave"
        Clave.ReadOnly = True
        Clave.Width = 40
        '
        'Ejecutar
        '
        Ejecutar.FillWeight = 60.0!
        Ejecutar.HeaderText = "Ejecutar"
        Ejecutar.MinimumWidth = 20
        Ejecutar.Name = "Ejecutar"
        Ejecutar.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Ejecutar.Width = 60
        '
        'lblInsertedFilters
        '
        lblInsertedFilters.AutoSize = True
        lblInsertedFilters.Location = New System.Drawing.Point(74, 147)
        lblInsertedFilters.Name = "lblInsertedFilters"
        lblInsertedFilters.Size = New System.Drawing.Size(75, 13)
        lblInsertedFilters.TabIndex = 19
        lblInsertedFilters.Text = "Valor del Filtro"
        lblInsertedFilters.Visible = False
        '
        'txtFilter
        '
        txtFilter.Location = New System.Drawing.Point(77, 163)
        txtFilter.Name = "txtFilter"
        txtFilter.Size = New System.Drawing.Size(351, 21)
        txtFilter.TabIndex = 22
        txtFilter.Visible = False
        '
        'btnParcialConfig
        '
        btnParcialConfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnParcialConfig.DialogResult = System.Windows.Forms.DialogResult.None
        btnParcialConfig.Location = New System.Drawing.Point(104, 315)
        btnParcialConfig.Name = "btnParcialConfig"
        btnParcialConfig.Size = New System.Drawing.Size(110, 26)
        btnParcialConfig.TabIndex = 23
        btnParcialConfig.Text = "Config. Actual"
        '
        'ZPanel
        '
        ZPanel.Controls.Add(chkIndexGroup)
        ZPanel.Controls.Add(btnExecute)
        ZPanel.Controls.Add(btnParcialConfig)
        ZPanel.Controls.Add(txtFilter)
        ZPanel.Controls.Add(lblInsertedFilters)
        ZPanel.Controls.Add(DataGridView1)
        ZPanel.Controls.Add(btnSig)
        ZPanel.Controls.Add(Label1)
        ZPanel.Controls.Add(txttabla)
        ZPanel.Controls.Add(txtColumna)
        ZPanel.Controls.Add(Label3)
        ZPanel.Controls.Add(cboIsIndexKey)
        ZPanel.Controls.Add(Label4)
        ZPanel.Controls.Add(lstIndexs)
        ZPanel.Controls.Add(descrip)
        ZPanel.Controls.Add(Panel1)
        ZPanel.Controls.Add(Label2)
        ZPanel.Controls.Add(BtnAgregar)
        ZPanel.Controls.Add(GroupBox1)
        ZPanel.Controls.Add(DataGrid1)
        ZPanel.Controls.Add(cbodoctype)
        ZPanel.Controls.Add(cboindexs)
        ZPanel.Controls.Add(ZButton2)
        ZPanel.Controls.Add(ZButton)
        ZPanel.Controls.Add(btnConfig)
        ZPanel.Controls.Add(btnDelete)
        ZPanel.Controls.Add(btnDeleteAll)
        ZPanel.Controls.Add(Label5)
        ZPanel.Controls.Add(CboComparator)
        ZPanel.Controls.Add(chkFilterByIndex)
        ZPanel.Dock = System.Windows.Forms.DockStyle.Fill
        ZPanel.Location = New System.Drawing.Point(0, 0)
        ZPanel.Name = "ZPanel"
        ZPanel.Size = New System.Drawing.Size(631, 344)
        ZPanel.TabIndex = 7
        '
        'chkIndexGroup
        '
        chkIndexGroup.AutoSize = True
        chkIndexGroup.Location = New System.Drawing.Point(75, 200)
        chkIndexGroup.Name = "chkIndexGroup"
        chkIndexGroup.Size = New System.Drawing.Size(95, 17)
        chkIndexGroup.TabIndex = 25
        chkIndexGroup.Text = "Agrupar Atributo"
        chkIndexGroup.UseVisualStyleBackColor = True
        '
        'btnExecute
        '
        btnExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnExecute.DialogResult = System.Windows.Forms.DialogResult.None
        btnExecute.Location = New System.Drawing.Point(419, 315)
        btnExecute.Name = "btnExecute"
        btnExecute.Size = New System.Drawing.Size(82, 24)
        btnExecute.TabIndex = 24
        btnExecute.Text = "Ejecutar"
        btnExecute.Visible = False
        '
        'chkFilterByIndex
        '
        chkFilterByIndex.AutoSize = True
        chkFilterByIndex.BackColor = System.Drawing.Color.Transparent
        chkFilterByIndex.Location = New System.Drawing.Point(137, 212)
        chkFilterByIndex.Name = "chkFilterByIndex"
        chkFilterByIndex.Size = New System.Drawing.Size(291, 17)
        chkFilterByIndex.TabIndex = 26
        chkFilterByIndex.Text = "Filtrar los resultados por un indice especifico al ejecutar"
        chkFilterByIndex.UseVisualStyleBackColor = False
        '
        'UCComplete
        '
        Controls.Add(ZPanel)
        Name = "UCComplete"
        Size = New System.Drawing.Size(631, 344)
        CType(DataGrid1, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ZPanel.ResumeLayout(False)
        ZPanel.PerformLayout()
        ResumeLayout(False)

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
            cbodoctype.DataSource = dsDocTypes.Tables(0)
            cbodoctype.DisplayMember = dsDocTypes.Tables(0).Columns(0).ColumnName '"DOC_TYPE_NAME"
            cbodoctype.ValueMember = dsDocTypes.Tables(0).Columns(9).ColumnName '"DOC_TYPE_ID"
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

    Private Sub BtnAgregar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnAgregar.Click
        cbodoctype.Enabled = False
        Agregar()
    End Sub
    Private Sub Agregar()
        Try
            If MessageBox.Show("¿El campo es clave?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                IndexKey = DirectCast(lstIndexs.SelectedValue, Boolean)
            End If
            _columna = InputBox("El Dato se obtendra de " & txttabla.Text & ". Ingrese el nombre de la columna: ", "Zamba")
            If txttabla.Text.Trim = "" Then
                MessageBox.Show("Complete la tabla de origen", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If _columna <> "" Then
                BarcodesBusiness.Insertar(CInt(cbodoctype.SelectedValue), CInt(lstIndexs.SelectedValue), txttabla.Text, _columna, IndexKey, chkIndexGroup.Checked, chkFilterByIndex.Checked, CboComparator.SelectedItem.ToString)
            End If
        Catch
        End Try
    End Sub

    Private Sub cbodoctype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cbodoctype.SelectedIndexChanged
        cargarIndices()
    End Sub

    Private Sub UCComplete_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        LoadDocTypes()
        descrip.Text = "Seleccione la Entidad a configurar el Autocompletar."
        chkIndexGroup.Visible = False

        ' cbodoctype.Text = "Tipo de Doc."
        '  cbodoctype.SelectedIndex = -1
    End Sub

    Private Sub btnSig_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSig.Click
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

                        descrip.Text = "Elija el atributo a configurar, indique si es Campo Clave y  elija la comparacion."
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
                        MessageBox.Show("Debe seleccionar un entidad.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Case 2

                    If ((lstIndexs.SelectedIndex <> -1) AndAlso (cboIsIndexKey.Text <> "")) Then

                        If (cboIsIndexKey.Text = "Si") Then
                            IndexKey = True
                        Else
                            IndexKey = False
                        End If

                        descrip.Text = "Ingrese el nombre de la tabla y la columna donde se encuentra el atributo."
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
                        MessageBox.Show("Debe seleccionar un Atributo y su condicion de clave.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

                    ' [Gaston]  23/12/2008  Si el Atributo es clave el checkbox "Agrupar Atributo" no se muestra 
                    If (cboIsIndexKey.Text = "Si") Then
                        chkIndexGroup.Visible = False
                    Else
                        chkIndexGroup.Visible = True
                    End If

                Case 4

                    If ((txttabla.Text <> "") AndAlso (txtColumna.Text <> "")) Then

                        _columna = txtColumna.Text.Trim
                        zstep = 4
                        'tmpIndex = lstIndexs.SelectedValue

                        If IndexKey = True Then
                            descrip.Text = "Esta a punto de establecer la funcion autocompletar para el atributo como CAMPO CLAVE!"
                        Else
                            descrip.Text = "Esta a punto de establecer la funcion autocompletar para el atributo."
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
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Salvando Autocompletar")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Entidad:" & cbodoctype.SelectedValue.ToString())
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Atributo:" & lstIndexs.SelectedValue.ToString())
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Tabla:" & txttabla.Text)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Columna:" & _columna)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Clave:" & IndexKey)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparador:" & CboComparator.SelectedItem.ToString())
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor para where: " & txtFilter.Text)
                    BarcodesBusiness.Insertar(Int32.Parse(cbodoctype.SelectedValue.ToString), Int32.Parse(lstIndexs.SelectedValue.ToString), txttabla.Text, _columna, IndexKey, chkIndexGroup.Checked, chkFilterByIndex.Checked, CboComparator.SelectedItem.ToString, txtFilter.Text)
                    tempArray.Add(IndexKey)

                    If MessageBox.Show("Atributo configurado. " & ControlChars.NewLine & "Desea configurar otro Atributo?", "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        'invertIndices()
                        ParentForm.Dispose()
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

                    descrip.Text = "Seleccione la Entidad a configurar el Autocompletar."
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

                    descrip.Text = "Elija el atributo a configurar, e indique si es Campo Clave."
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
                        IndexKey = True
                    Else
                        IndexKey = False
                    End If

                    descrip.Text = "Ingrese el nombre de la tabla y la columna donde se encuentra el atributo."
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

                    ' [Gaston]  23/12/2008  Si el Atributo es clave el checkbox "Agrupar Atributo" no se muestra 
                    If (cboIsIndexKey.Text = "Si") Then
                        chkIndexGroup.Visible = False
                    Else
                        chkIndexGroup.Visible = True
                    End If

            End Select

        End If

    End Sub

    Private Sub ZButton2_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ZButton2.Click
        cambioStep(False)
    End Sub

    Private Sub ZButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ZButton.Click
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
            ParentForm.Dispose()
        End If
    End Sub

    Private Sub ZButton3_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnConfig.Click
        cargarLista()
        DataGridView1.Columns("Ejecutar").Visible = False
        btnConfig.Visible = False
        btnParcialConfig.Visible = True
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

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnDelete.Click
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
    ''' Cargar Atributos
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
                Dim DSIndex As New DataSet
                'DSIndex = Core.Indexs_Factory.GetIndexSchema(cbodoctype.SelectedValue)
                DSIndex = Zamba.Core.Indexs.Schema.GetIndexSchema(CInt(cbodoctype.SelectedValue))
                lstIndexs.DataSource = DSIndex.Tables("DOC_INDEX")
                lstIndexs.DisplayMember = DSIndex.Tables("DOC_INDEX").Columns(1).ColumnName ' "INDEX_NAME")
                lstIndexs.ValueMember = DSIndex.Tables("DOC_INDEX").Columns(0).ColumnName    ' "INDEX_ID"
                lstIndexs.Refresh()
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

    Private Sub ZButton4_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnDeleteAll.Click
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
            ZButton.Visible = Value
        End Set
    End Property
    
    

    ''' <summary>
    ''' este método realiza una carga en la grilla de los autocomplete que
    ''' pero mostrando solo los campos claves por distinto doctype.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnParcialConfig_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnParcialConfig.Click
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
        DocTypeID.Visible = False

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

        DataGridView1.Columns("Ejecutar").Visible = True
        btnConfig.Visible = True
        btnParcialConfig.Visible = False
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
    Private Sub btnExecute_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExecute.Click
        'Colección con los id de tipos de documento que se seleccionan con la columna "Ejecutar"
        Dim selected As Boolean
        Dim doExecute As Boolean
        Dim IndexFilters As Dictionary(Of String, String) = Nothing
        Dim DocTypeId As Integer

        For Each row As DataGridViewRow In DataGridView1.Rows
            ' Si la celda tiene un tilde
            If (CType(row.Cells("Ejecutar").Value, Boolean) = True) Then

                DocTypeId = row.Cells("DocTypeId").Value

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando autocomplete """ & row.Cells(1).Value & """ - DocTypeId: " & DocTypeId)

                selected = True
                doExecute = True

                'Si tiene filtro por indice especifico pedir los datos
                If BarcodesBusiness.HasSpecificIndexFilters(DocTypeId) Then

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Autocompletar configurado con atributos especificos")

                    RemoveHandler ZClass.eHandleEventDialogReseult, AddressOf DialogResultEventHandler
                    AddHandler ZClass.eHandleEventDialogReseult, AddressOf DialogResultEventHandler

                    Dim ucIndexAutoComplete As New UCIndexViewerAutocompleteManual(False, True,  False, False)
                    Dim f As New Form

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando atributos en el formulario de seleccion de atributos")
                    ucIndexAutoComplete.ShowIndexs(New Results_Business().GetNewResult(DocTypeId, String.Empty))

                    ucIndexAutoComplete.Dock = DockStyle.Fill
                    ucIndexAutoComplete.ShowPanelWithAceptandCancel()
                    ucIndexAutoComplete.Refresh()

                    f.ShowIcon = False
                    f.Text = "Atributos para filtrar"
                    f.ShowInTaskbar = False
                    f.Controls.Add(ucIndexAutoComplete)
                    f.StartPosition = FormStartPosition.CenterScreen
                    f.MaximizeBox = False
                    f.ControlBox = False

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Mostrando formulario de seleccion de atributos")

                    f.ShowDialog()

                    If _dialogRes = DialogResult.OK Then
                        Dim Atributos As ArrayList = ucIndexAutoComplete.GetIndexs()

                        IndexFilters = New Dictionary(Of String, String)

                        For Each ind As Index In Atributos
                            If Not String.IsNullOrEmpty(ind.DataTemp) Then
                                IndexFilters.Add(ind.ID, ind.DataTemp)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Atributo seleccionado: """ & ind.Name & """ - ID: " & ind.ID & " - Valor: """ & ind.DataTemp & """")
                            End If
                        Next
                    Else
                        If MessageBox.Show("¿Desea ejecutar el autocomplete sin aplicar ningun filtro?", "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se decidio ejecutar el autocomplete sin ingresar valores para los atributos")
                            doExecute = False
                        End If
                    End If

                    ucIndexAutoComplete.Dispose()
                    ucIndexAutoComplete = Nothing

                    f.Dispose()
                    f = Nothing

                End If

                If doExecute Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando autocomplete")
                    AutoCompleteBarcode_FactoryBusiness.ExecuteAutocomplete(DocTypeId.ToString(), IndexFilters)
                End If
            End If
        Next

        If selected Then
            If doExecute Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Finalizacion ejecucion autocomplete")
                MessageBox.Show("Se ha terminado de ejecutar el proceso", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se cancelo la ejecucion del autocomplete")
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