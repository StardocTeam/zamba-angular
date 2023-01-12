Imports Zamba.AppBlock
Imports Zamba.Core
Imports System.Windows.Forms
Imports System.Data

Public Class UCListaSustitucion
    Inherits ZControl
    '[Sebastian 08-06-2009] se agrego "as" porque no lo tenia
    Const CODE_COLUMN As Int32 = 0 'Devuelve el numero de columna donde esta el codigo de la tabla
#Region "Atributos y Propiedades"
    Private _ds As New DataSet
    Private _tabla As String
    Private _admin As Boolean
    Private _indexId As Integer
    Private _addedSustitutionItems As New Generic.List(Of SustitutionItem)
    Friend WithEvents btnModify As Zamba.AppBlock.ZButton
    Private _removeSustitutionItems As New Generic.List(Of Integer)

    Public Property ds() As DataSet
        Get
            Return _ds
        End Get
        Set(ByVal value As DataSet)
            _ds = value
        End Set
    End Property
    Public Property Tabla() As String
        Get
            Return _tabla
        End Get
        Set(ByVal value As String)
            _tabla = value
        End Set
    End Property
    Public Property IndexId() As Integer
        Get
            Return _indexId
        End Get
        Set(ByVal value As Integer)
            _indexId = value
        End Set
    End Property
    Public Property Admin() As Boolean
        Get
            Return _admin
        End Get
        Set(ByVal value As Boolean)
            _admin = value
        End Set
    End Property
#End Region
#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByVal IndexID As Int64, ByVal Reload As Boolean, ByVal Admin As Boolean)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        _admin = Admin
        '[Sebastian 08-06-2009] se agrego cast para salvar el warning
        _indexId = Int32.Parse(IndexID.ToString)
        _tabla = "SLST_S" & IndexID
        Trace.WriteLineIf(ZTrace.IsVerbose, "Tabla: " & Me.Tabla)
        Trace.WriteLineIf(ZTrace.IsVerbose, "Cargando Grilla")
        CargarGrilla(Reload)
        Trace.WriteLineIf(ZTrace.IsVerbose, "Grilla Cargada")
        Panel3.Visible = Admin
        BtnInsert.Visible = Admin
        ZPanel1.Visible = Admin
        BtnAceptar.Visible = Not Admin
    End Sub

    Public Sub New(ByVal IndexID As Int64, ByVal table As DataTable, Optional ByVal admin As Boolean = False)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Me.Admin = admin
        '[Sebastian 08-06-2009] se agrego cast para salvar el warning
        Me.IndexId = Int32.Parse(IndexID.ToString)
        Me.dgListaSustitucion.ReadOnly = Not admin
        Me.dgListaSustitucion.ReadOnly = Not admin
        Me.Tabla = "SLST_S" & IndexID

        Panel3.Visible = Me.Admin
        ZPanel1.Visible = Me.Admin
        BtnAceptar.Visible = Not Me.Admin
        BtnInsert.Visible = Me.Admin
        btEliminar.Visible = Me.Admin
        btnModify.Visible = Me.Admin
        BtnExport.Visible = Me.Admin
        Me.ds.Merge(table)
        Me.dgListaSustitucion.DataSource = ds.Tables(0)
        If ds.Tables(0).Rows.Count > 0 Then Me.dgListaSustitucion.CurrentRowIndex = 0

    End Sub

    Private Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
    End Sub
    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        Catch
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents Panel4 As ZPanel
    Friend WithEvents dgListaSustitucion As System.Windows.Forms.DataGrid
    Friend WithEvents Panel2 As ZPanel
    Friend WithEvents BtnAceptar As ZButton
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Panel3 As ZPanel
    Friend WithEvents Panel31 As ZPanel
    Friend WithEvents BtnActualizar As ZButton
    Friend WithEvents tbBuscar As System.Windows.Forms.TextBox
    Friend WithEvents ZPanel1 As Zamba.AppBlock.ZPanel
    Friend WithEvents ZButton1 As Zamba.AppBlock.ZButton
    Friend WithEvents Label1 As Zamba.AppBlock.ZLabel
    Friend WithEvents BtnInsert As Zamba.AppBlock.ZButton
    Friend WithEvents btEliminar As Zamba.AppBlock.ZButton
    Friend WithEvents BtnExport As Zamba.AppBlock.ZButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New Zamba.AppBlock.ZPanel
        Me.dgListaSustitucion = New System.Windows.Forms.DataGrid
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.Panel4 = New Zamba.AppBlock.ZPanel
        Me.btnModify = New Zamba.AppBlock.ZButton
        Me.btEliminar = New Zamba.AppBlock.ZButton
        Me.BtnInsert = New Zamba.AppBlock.ZButton
        Me.BtnExport = New Zamba.AppBlock.ZButton
        Me.Label1 = New Zamba.AppBlock.ZLabel
        Me.tbBuscar = New System.Windows.Forms.TextBox
        Me.Panel2 = New Zamba.AppBlock.ZPanel
        Me.BtnAceptar = New Zamba.AppBlock.ZButton
        Me.Panel3 = New Zamba.AppBlock.ZPanel
        Me.BtnActualizar = New Zamba.AppBlock.ZButton
        Me.Panel31 = New Zamba.AppBlock.ZPanel
        Me.ZPanel1 = New Zamba.AppBlock.ZPanel
        Me.ZButton1 = New Zamba.AppBlock.ZButton
        Me.Panel1.SuspendLayout()
        CType(Me.dgListaSustitucion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.ZPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Silver
        Me.Panel1.Controls.Add(Me.dgListaSustitucion)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(0, 64)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(395, 290)
        Me.Panel1.TabIndex = 0
        '
        'dgListaSustitucion
        '
        Me.dgListaSustitucion.AlternatingBackColor = System.Drawing.Color.GhostWhite
        Me.dgListaSustitucion.BackColor = System.Drawing.Color.GhostWhite
        Me.dgListaSustitucion.BackgroundColor = System.Drawing.Color.Lavender
        Me.dgListaSustitucion.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgListaSustitucion.CaptionBackColor = System.Drawing.Color.RoyalBlue
        Me.dgListaSustitucion.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.dgListaSustitucion.CaptionForeColor = System.Drawing.Color.White
        Me.dgListaSustitucion.DataMember = ""
        Me.dgListaSustitucion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgListaSustitucion.FlatMode = True
        Me.dgListaSustitucion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.dgListaSustitucion.ForeColor = System.Drawing.Color.MidnightBlue
        Me.dgListaSustitucion.GridLineColor = System.Drawing.Color.RoyalBlue
        Me.dgListaSustitucion.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.dgListaSustitucion.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.dgListaSustitucion.HeaderForeColor = System.Drawing.Color.Lavender
        Me.dgListaSustitucion.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.dgListaSustitucion.LinkColor = System.Drawing.Color.Teal
        Me.dgListaSustitucion.Location = New System.Drawing.Point(0, 0)
        Me.dgListaSustitucion.Name = "dgListaSustitucion"
        Me.dgListaSustitucion.ParentRowsBackColor = System.Drawing.Color.Lavender
        Me.dgListaSustitucion.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.dgListaSustitucion.PreferredColumnWidth = 160
        Me.dgListaSustitucion.ReadOnly = True
        Me.dgListaSustitucion.SelectionBackColor = System.Drawing.Color.Teal
        Me.dgListaSustitucion.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.dgListaSustitucion.Size = New System.Drawing.Size(395, 290)
        Me.dgListaSustitucion.TabIndex = 1
        Me.dgListaSustitucion.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.dgListaSustitucion
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "DsSubstitucion"
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Codigo"
        Me.DataGridTextBoxColumn1.MappingName = "Codigo"
        Me.DataGridTextBoxColumn1.NullText = ""
        Me.DataGridTextBoxColumn1.Width = 50
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Descripcion"
        Me.DataGridTextBoxColumn2.MappingName = "Descripcion"
        Me.DataGridTextBoxColumn2.NullText = ""
        Me.DataGridTextBoxColumn2.Width = 300
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.Panel4.Controls.Add(Me.btnModify)
        Me.Panel4.Controls.Add(Me.btEliminar)
        Me.Panel4.Controls.Add(Me.BtnInsert)
        Me.Panel4.Controls.Add(Me.BtnExport)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.tbBuscar)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.ForeColor = System.Drawing.Color.Black
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(395, 64)
        Me.Panel4.TabIndex = 0
        '
        'btnModify
        '
        Me.btnModify.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnModify.Location = New System.Drawing.Point(274, 40)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(79, 18)
        Me.btnModify.TabIndex = 5
        Me.btnModify.Text = "Modificar"
        '
        'btEliminar
        '
        Me.btEliminar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btEliminar.Location = New System.Drawing.Point(94, 40)
        Me.btEliminar.Name = "btEliminar"
        Me.btEliminar.Size = New System.Drawing.Size(72, 18)
        Me.btEliminar.TabIndex = 4
        Me.btEliminar.Text = "Eliminar"
        '
        'BtnInsert
        '
        Me.BtnInsert.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnInsert.Location = New System.Drawing.Point(8, 40)
        Me.BtnInsert.Name = "BtnInsert"
        Me.BtnInsert.Size = New System.Drawing.Size(70, 18)
        Me.BtnInsert.TabIndex = 3
        Me.BtnInsert.Text = "Insertar"
        '
        'BtnExport
        '
        Me.BtnExport.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnExport.Location = New System.Drawing.Point(185, 39)
        Me.BtnExport.Name = "BtnExport"
        Me.BtnExport.Size = New System.Drawing.Size(73, 18)
        Me.BtnExport.TabIndex = 2
        Me.BtnExport.Text = "Exportar"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Buscar:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbBuscar
        '
        Me.tbBuscar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbBuscar.BackColor = System.Drawing.Color.White
        Me.tbBuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBuscar.Location = New System.Drawing.Point(72, 8)
        Me.tbBuscar.Name = "tbBuscar"
        Me.tbBuscar.Size = New System.Drawing.Size(303, 22)
        Me.tbBuscar.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Silver
        Me.Panel2.Controls.Add(Me.BtnAceptar)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.ForeColor = System.Drawing.Color.Black
        Me.Panel2.Location = New System.Drawing.Point(0, 418)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(395, 32)
        Me.Panel2.TabIndex = 1
        '
        'BtnAceptar
        '
        Me.BtnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.BtnAceptar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnAceptar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAceptar.Location = New System.Drawing.Point(0, 0)
        Me.BtnAceptar.Name = "BtnAceptar"
        Me.BtnAceptar.Size = New System.Drawing.Size(395, 32)
        Me.BtnAceptar.TabIndex = 0
        Me.BtnAceptar.Text = "Aceptar"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.Panel3.Controls.Add(Me.BtnActualizar)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.ForeColor = System.Drawing.Color.Black
        Me.Panel3.Location = New System.Drawing.Point(0, 386)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(395, 32)
        Me.Panel3.TabIndex = 2
        '
        'BtnActualizar
        '
        Me.BtnActualizar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnActualizar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnActualizar.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnActualizar.Location = New System.Drawing.Point(0, 0)
        Me.BtnActualizar.Name = "BtnActualizar"
        Me.BtnActualizar.Size = New System.Drawing.Size(395, 32)
        Me.BtnActualizar.TabIndex = 0
        Me.BtnActualizar.Text = "Guardar Listado"
        '
        'Panel31
        '
        Me.Panel31.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.Panel31.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel31.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel31.ForeColor = System.Drawing.Color.Black
        Me.Panel31.Location = New System.Drawing.Point(0, 386)
        Me.Panel31.Name = "Panel31"
        Me.Panel31.Size = New System.Drawing.Size(300, 32)
        Me.Panel31.TabIndex = 2
        '
        'ZPanel1
        '
        Me.ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.ZPanel1.Controls.Add(Me.ZButton1)
        Me.ZPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ZPanel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZPanel1.ForeColor = System.Drawing.Color.Black
        Me.ZPanel1.Location = New System.Drawing.Point(0, 354)
        Me.ZPanel1.Name = "ZPanel1"
        Me.ZPanel1.Size = New System.Drawing.Size(395, 32)
        Me.ZPanel1.TabIndex = 3
        '
        'ZButton1
        '
        Me.ZButton1.DialogResult = System.Windows.Forms.DialogResult.None
        Me.ZButton1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ZButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZButton1.Location = New System.Drawing.Point(0, 0)
        Me.ZButton1.Name = "ZButton1"
        Me.ZButton1.Size = New System.Drawing.Size(395, 32)
        Me.ZButton1.TabIndex = 0
        Me.ZButton1.Text = "Inserción masiva"
        '
        'UCListaSustitucion
        '
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ZPanel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Name = "UCListaSustitucion"
        Me.Size = New System.Drawing.Size(395, 450)
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgListaSustitucion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ZPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Grilla"

    Public Event RowSelected(ByVal Codigo As String, ByVal Descripcion As String, ByVal Index As Int64)

    Public Sub CargarGrilla(ByVal Reload As Boolean)
        ds.Merge(AutoSubstitutionBusiness.GetIndexData(Me.IndexId, Reload))
        dgListaSustitucion.DataSource = ds.Tables(0)
        Try
            If ds.Tables(0).Rows.Count > 0 Then dgListaSustitucion.CurrentRowIndex = 0
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        BtnActualizar.Visible = _admin
        BtnAceptar.Visible = Not _admin
        BtnInsert.Visible = _admin
    End Sub

    'Public Shared Function getDescription(ByVal Code As Int64, ByVal IndexId As Int32) As String
    '    Dim Tabla As String = "SLST_S" & IndexId
    '    Dim strselect As String
    '    strselect = "Select DESCRIPCION from " & Tabla & " Where Codigo=" & Code
    '    Return Server.Con.ExecuteScalar(CommandType.Text, strselect)
    'End Function

#End Region
#Region "Acceso a datos"
    Private Shared Sub InsertarDatos(ByVal Indice As Int32, ByVal Codigo As Int32, ByVal Descripcion As String)
        Dim cr As New CreateTablesBusiness
        cr.InsertIntoSustitucion("ilst_i" & Indice, Codigo, Descripcion)
    End Sub
    Private Shared Sub BorrarContenido(ByVal Indice As Int32)
        Dim cr As New CreateTablesBusiness
        cr.BorrarSustitucionTable(Indice)
    End Sub
    Private Shared Sub EliminarTabla(ByVal Indice As Int32)
        Dim cr As New CreateTablesBusiness
        cr.DropSustitucionTable(Indice)
    End Sub
#End Region

    Private Sub BtnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAceptar.Click
        Try
            Accept()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Event EndOfCreatingTableOrList()
    Private Sub BtnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnActualizar.Click
        Actualizar(ds, Tabla)
        RaiseEvent EndOfCreatingTableOrList()

    End Sub
    Private Sub tbBuscar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbBuscar.TextChanged
        Try
            dgListaSustitucion.UnSelect(dgListaSustitucion.CurrentCell.RowNumber)
            tbBuscar.Focus()
            Dim Busqueda As String = tbBuscar.Text.Trim()
            If String.Compare(Busqueda, String.Empty) <> 0 Then
                If IsNumeric(Busqueda) Then
                    Dim qrows As Int64 = ds.Tables(0).Rows.Count - 1
                    Dim i As Int64
                    For i = 0 To qrows
                        Try
                            '[Sebastian 08-06-2009] se agrego cast para salvar el warning
                            If Decimal.Parse(dgListaSustitucion.Item(Int32.Parse(i.ToString), 0).ToString) = CDec(Busqueda) Then
                                '[Sebastian 08-06-2009] se agrego cast para salvar el warning
                                Me.dgListaSustitucion.CurrentRowIndex = Int32.Parse(i.ToString)
                                Exit For
                            End If
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    Next
                Else
                    Dim qrows As Int64 = ds.Tables(0).Rows.Count - 1
                    Dim i As Int64
                    For i = 0 To qrows
                        Try
                            '[Sebastian 08-06-2009] se agrego cast para salvar el warning
                            Dim Description As String = dgListaSustitucion.Item(Int32.Parse(i.ToString), 1).ToString
                            If LCase(Description).IndexOf(LCase(Busqueda), 0, Busqueda.Trim.Length) <> -1 Then
                                dgListaSustitucion.CurrentRowIndex = CInt(i)
                                Exit For
                            End If
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    Next
                End If
                dgListaSustitucion.Select(dgListaSustitucion.CurrentCell.RowNumber)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub UCListaSustitucion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Width = 300
            Height = 450
            If dgListaSustitucion.CurrentCell.RowNumber > 0 Then
                dgListaSustitucion.UnSelect(dgListaSustitucion.CurrentCell.RowNumber)
            End If
            tbBuscar.Focus()
            tbBuscar.Select()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub BulkInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZButton1.Click
        Try
            Dim frm As New FrmBulkInsert(IndexId)
            RemoveHandler frm.RefillGrid, AddressOf ReloadGrid
            AddHandler frm.RefillGrid, AddressOf Me.ReloadGrid
            frm.ShowDialog()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExport.Click
        Try
            If UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.ExportarLista, Zamba.Core.RightsType.Execute) = False Then
                MessageBox.Show("No tiene permiso para Administrar esta función", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Else
                Dim frmexport As New FrmExportList(IndexId)
                frmexport.ShowDialog()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub tbBuscar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tbBuscar.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtnAceptar_Click(BtnAceptar, New System.EventArgs)
        End If
        'If e.KeyCode = Keys.Escape Then
        '    CloseControl()
        'End If
    End Sub

    Private Sub dgListaSustitucion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgListaSustitucion.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtnAceptar_Click(BtnAceptar, New System.EventArgs)
        End If
    End Sub

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
    End Sub

    Private Sub BtnInsertar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnInsert.Click
        Try
            Dim frmInsertItem As frmInsertItemListaSustitucion
            frmInsertItem = New frmInsertItemListaSustitucion(IndexId, GetExistingCodes())
            AddHandler frmInsertItem.NewItem, AddressOf AddItem
            frmInsertItem.ShowDialog()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEliminar.Click
        Dim SelectedIndexs As New ArrayList
        For i As Integer = dgListaSustitucion.VisibleRowCount - 1 To 0 Step -1
            If dgListaSustitucion.IsSelected(i) Then
                SelectedIndexs.Add(i)
                '[Sebastian 08-06-2009] se agrego cast para salvar el warning
                _removeSustitutionItems.Add(Int32.Parse(dgListaSustitucion.Item(i, CODE_COLUMN).ToString)) 'Agrego el codigo del item a eliminar
            End If
        Next

        Dim RemoveIndex As Integer
        For j As Integer = 0 To SelectedIndexs.Count - 1
            RemoveIndex = CInt(SelectedIndexs(j))
            ds.Tables(0).Rows.RemoveAt(RemoveIndex)
        Next
    End Sub
    ' se modifico para que pueda recibir codigos alfanumericos [sebastian 11/12/2008]
    'Private Function GetExistingCodes() As Generic.List(Of Integer )
    Private Function GetExistingCodes() As Generic.List(Of String)
        Dim Codes As New Generic.List(Of String)

        For i As Integer = 0 To dgListaSustitucion.VisibleRowCount - 2
            'Codes.Add(CInt(dgListaSustitucion.Item(i, CODE_COLUMN)))
            '[Sebastian 08-06-2009] se agrego cast para salvar el warning
            Codes.Add(dgListaSustitucion.Item(i, CODE_COLUMN).ToString)
        Next

        Return Codes
    End Function
    ''' <summary>
    ''' [Sebastian 29-05-2009] CREATED evento que utiliza para saber si se esta editando la tabla de sustitucion
    ''' </summary>
    ''' <param name="TrueOrFalse"></param>
    ''' <remarks></remarks>
    Public Shared Event CreatingSustTableOrList(ByVal TrueOrFalse As Boolean)
    Private Sub AddItem(ByVal Indice As Int32, ByVal Codigo As String, ByVal Descripcion As String)
        'Try
        '    InsertarDatos(Indice, Codigo, Descripcion)
        'Catch ex As Exception
        '   zamba.core.zclass.raiseerror(ex)
        'End Try

        If ds.Tables(0).Columns.Count = 0 Then
            ds.Tables(0).Columns.Add("Codigo")
            ds.Tables(0).Columns.Add("Descripcion")
        End If

        _addedSustitutionItems.Add(New SustitutionItem(Codigo, Descripcion))
        Dim NewRow As DataRow = ds.Tables(0).NewRow
        NewRow.Item(0) = Codigo
        NewRow.Item(1) = Descripcion
        ds.Tables(0).Rows.Add(NewRow)

        'ReloadGrid()
        RaiseEvent CreatingSustTableOrList(True)
    End Sub
    Private Sub Actualizar(ByVal ds As DataSet, ByVal Tabla As String)
        Try
            If _addedSustitutionItems.Count > 0 Then
                AutoSubstitutionBusiness.AddItems(_addedSustitutionItems, _indexId)
                _addedSustitutionItems.Clear()
            End If

            If _removeSustitutionItems.Count > 0 Then
                AutoSubstitutionBusiness.RemoveItems(_removeSustitutionItems, _indexId)
                _removeSustitutionItems.Clear()
            End If

            MessageBox.Show("Tabla actualizada", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            MessageBox.Show("Ocurrio un error al actualizar: " & ex.ToString, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Private Sub Actualizar(ByVal ds As DataSet, ByVal Tabla As String)
    '    Try
    '        Dim TabelBusiness As New CreateTablesBusiness
    '        'Dim FilasAgregadas As Boolean = False
    '        'Dim FilasModificadas As Boolean = False
    '        'Dim FilasEliminadas As Boolean = False

    '        If IsNothing(ds.GetChanges()) Then
    '            Exit Sub
    '        End If


    '        'SI CAMBIO EL DATASET DE LA TABLA DE SUSTITUCION

    '        'INSERTA LAS NUEVAS FILAS
    '        Dim i As Int32
    '        If IsNothing(ds.GetChanges(DataRowState.Added)) = False Then
    '            For i = 0 To ds.GetChanges(DataRowState.Added).Tables(0).Rows.Count - 1
    '                TabelBusiness.InsertIntoSustitucion(Tabla, CInt(ds.GetChanges(DataRowState.Added).Tables(0).Rows(i).ItemArray(0)), ds.GetChanges(DataRowState.Added).Tables(0).Rows(i).ItemArray(1))
    '            Next
    '        End If

    '        'ACTUALIZA LAS FILAS MODIFICADAS
    '        If IsNothing(ds.GetChanges(DataRowState.Modified)) = False Then
    '            For i = 0 To ds.GetChanges(DataRowState.Modified).Tables(0).Rows.Count - 1
    '                TabelBusiness.UpdateIntoSustitucion(Tabla, CInt(ds.GetChanges(DataRowState.Modified).Tables(0).Rows(i).ItemArray(0)), ds.GetChanges(DataRowState.Modified).Tables(0).Rows(i).ItemArray(1))
    '            Next
    '        End If

    '        Dim delRow As DataRow

    '        'ELIMINA LAS FILAS SUPRIMIDAS
    '        If IsNothing(ds.GetChanges(DataRowState.Deleted)) = False Then
    '            For i = 0 To ds.GetChanges(DataRowState.Deleted).Tables(0).Rows.Count - 1
    '                delRow = ds.GetChanges(DataRowState.Deleted).Tables(0).Rows(i)
    '                TabelBusiness.DeleteFromSustitucion(Tabla, CInt(delRow(0, DataRowVersion.Original)), delRow(1, DataRowVersion.Original))
    '            Next
    '        End If

    '        ds.AcceptChanges()

    '        MessageBox.Show("Tabla actualizada", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '        MessageBox.Show("Ocurrio un error al actualizar: " & ex.ToString, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Sub ReloadGrid()
        Try
            dgListaSustitucion.DataSource = Nothing
            dgListaSustitucion.DataSource = ds.Tables(0)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    'Private Class SustitutionItem
    '    Private _code As Integer
    '    Private _description As String

    '    Public Property Code() As Integer
    '        Get
    '            Return _code
    '        End Get
    '        Set(ByVal value As Integer)
    '            _code = value
    '        End Set
    '    End Property

    '    Public Property Description() As String
    '        Get
    '            Return _description
    '        End Get
    '        Set(ByVal value As String)
    '            _description = value
    '        End Set
    '    End Property

    '    Public Sub New(ByVal code As Integer, ByVal description As String)
    '        _code = code
    '        _description = description
    '    End Sub

    'End Class

    Private Sub dgListaSustitucion_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles dgListaSustitucion.Navigate
        Try
            Accept()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub Accept()
        Dim CurrentIndex As Integer = dgListaSustitucion.CurrentRowIndex
        Dim Codigo As String = CType(dgListaSustitucion.Item(CurrentIndex, 0), String)
        '[Sebastian 08-06-2009] se agrego cast para salvar el warning
        Dim Descripcion As String = Trim(dgListaSustitucion.Item(CurrentIndex, 1).ToString)
        tbBuscar.Text = ""
        RaiseEvent RowSelected(Codigo, Descripcion, CurrentIndex)
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click

        Dim frmUpdate As New frmInsertItemListaSustitucion(Me.IndexId)
        RemoveHandler frmInsertItemListaSustitucion.MidifiedIndex, AddressOf SaveModifiedIndex
        AddHandler frmInsertItemListaSustitucion.MidifiedIndex, AddressOf SaveModifiedIndex
        frmUpdate.txtCodigo.Text = dgListaSustitucion.Item(dgListaSustitucion.CurrentRowIndex, 0).ToString
        frmUpdate.txtDescripcion.Text = dgListaSustitucion.Item(dgListaSustitucion.CurrentRowIndex, 1).ToString
        frmUpdate.cmdAceptar.Visible = False
        frmUpdate.btnModify.Visible = True
        frmUpdate.ShowDialog()
        If frmUpdate.DialogResult = DialogResult.OK Then
            frmUpdate.btnModify.Visible = False
            frmUpdate.Dispose()
        End If
    End Sub
    Public Shared Event SavedIndex(ByVal salvado As Boolean)
    Public Shared Event dsModified(ByVal ds As DataSet)
    Private Sub SaveModifiedIndex(ByVal codigo As String, ByVal description As String)
        Dim LastCode As String = dgListaSustitucion.Item(dgListaSustitucion.CurrentRowIndex, 0).ToString
        For Each row As DataRow In ds.Tables(0).Rows
            If String.Compare(row("codigo").ToString, dgListaSustitucion.Item(dgListaSustitucion.CurrentRowIndex, 0).ToString) = 0 Then
                row("codigo") = codigo
                row("descripcion") = description
            End If
        Next
        ds.AcceptChanges()
        '[Sebastian 08-06-2009] Actualiza la tabla de sustitución. Un sólo indice en particular
        AutoSubstitutionBusiness.UpdateAddItem(codigo, description, LastCode, Me.IndexId.ToString)
        'RaiseEvent dsModified(ds)
    End Sub
End Class
