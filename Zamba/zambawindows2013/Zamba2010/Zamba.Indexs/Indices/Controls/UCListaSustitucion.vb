Imports Zamba.Core
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Collections.Generic

Public Class UCListaSustitucion
    Inherits ZControl

    Const CODE_COLUMN As Int32 = 0 'Devuelve el numero de columna donde esta el codigo de la tabla
    Public Shared Event CreatingSustTableOrList(ByVal TrueOrFalse As Boolean)
    Public Event RowSelected(ByVal Codigo As String, ByVal Descripcion As String, ByVal Index As Int64)
    Public Shared Event EndOfCreatingTableOrList()
    Public Shared Event SavedIndex(ByVal salvado As Boolean)
    Public Shared Event dsModified(ByVal ds As DataSet)

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByVal IndexID As Int64, ByVal Reload As Boolean, ByVal Admin As Boolean)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Me.Admin = Admin
        btnSave.Visible = _Admin
        btnMassiveInsert.Visible = _Admin
        btnOK.Visible = Not _Admin

        If Not _Admin Then
            pnlSearch.Height = 35
        End If

        Me.IndexId = IndexID
        Tabla = "SLST_S" & IndexID
        CargarGrilla(Reload)
    End Sub

    Public Sub New(ByVal IndexID As Int64, ByVal table As DataTable, Optional ByVal admin As Boolean = False)
        MyBase.New()

        Try

            'El Diseñador de Windows Forms requiere esta llamada.
            InitializeComponent()

            'Agregar cualquier inicialización después de la llamada a InitializeComponent()
            _Admin = admin
            Me.IndexId = IndexID
            dgListaSustitucion.ReadOnly = Not admin
            Tabla = "SLST_S" & IndexID

            btnSave.Visible = _Admin
            btnMassiveInsert.Visible = _Admin
            btnOK.Visible = Not _Admin
            btnInsert.Visible = _Admin
            btEliminar.Visible = _Admin
            btnModify.Visible = _Admin
            btnExport.Visible = _Admin

            If Not _Admin Then
                pnlSearch.Height = 35
            End If

            ds.Merge(table)
            dgListaSustitucion.DataSource = ds.Tables(0)
            If ds.Tables(0).Rows.Count > 0 Then dgListaSustitucion.Rows(0).IsSelected = True
        Catch ex As OutOfMemoryException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    'Private Sub LoadFewRecordsinGrid(busqueda As String)
    '    Dim dv As DataView = Nothing
    '    Try
    '        If busqueda.Length > 0 Then
    '            If Not _ds Is Nothing AndAlso _ds.Tables.Count > 0 Then
    '                dv = New DataView(_ds.Tables(0))
    '                If IsNumeric(busqueda) Then
    '                    dv.RowFilter = "Descripcion LIKE '%" & busqueda & "%' OR Codigo LIKE '%" & busqueda & "%'"
    '                Else
    '                    dv.RowFilter = "Descripcion LIKE '%" & busqueda & "%'"
    '                End If
    '                dgListaSustitucion.DataSource = dv.ToTable
    '                If dv.Count > 0 Then dgListaSustitucion.Select(0)
    '            End If
    '        Else
    '            dgListaSustitucion.DataSource = _ds.Tables(0)
    '        End If
    '    Catch ex As Exception
    '        ZTrace.WriteLineIf(ZTrace.IsError, ex.Message)
    '    Finally
    '        If dv IsNot Nothing Then
    '            dv.Dispose()
    '            dv = Nothing
    '        End If
    '    End Try

    'End Sub

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
                If _ds IsNot Nothing Then
                    _ds.Dispose()
                    _ds = Nothing
                End If
                If _addedSustitutionItems IsNot Nothing Then
                    _addedSustitutionItems.Clear()
                    _addedSustitutionItems = Nothing
                End If
                If _removeSustitutionItems IsNot Nothing Then
                    _removeSustitutionItems.Clear()
                    _removeSustitutionItems = Nothing
                End If

                If pnlSearch IsNot Nothing Then
                    pnlSearch.Dispose()
                    pnlSearch = Nothing
                End If
                If pnlBottom IsNot Nothing Then
                    pnlBottom.Dispose()
                    pnlBottom = Nothing
                End If
                If dgListaSustitucion IsNot Nothing Then
                    dgListaSustitucion.Dispose()
                    dgListaSustitucion = Nothing
                End If
                If btnOK IsNot Nothing Then
                    btnOK.Dispose()
                    btnOK = Nothing
                End If

                If colCode IsNot Nothing Then
                    colCode.Dispose()
                    colCode = Nothing
                End If
                If colDescription IsNot Nothing Then
                    colDescription.Dispose()
                    colDescription = Nothing
                End If
                If btnSave IsNot Nothing Then
                    btnSave.Dispose()
                    btnSave = Nothing
                End If
                If tbBuscar IsNot Nothing Then
                    tbBuscar.Dispose()
                    tbBuscar = Nothing
                End If
                If btnMassiveInsert IsNot Nothing Then
                    btnMassiveInsert.Dispose()
                    btnMassiveInsert = Nothing
                End If
                If lblSearch IsNot Nothing Then
                    lblSearch.Dispose()
                    lblSearch = Nothing
                End If
                If btnInsert IsNot Nothing Then
                    btnInsert.Dispose()
                    btnInsert = Nothing
                End If
                If btEliminar IsNot Nothing Then
                    btEliminar.Dispose()
                    btEliminar = Nothing
                End If
                If btnExport IsNot Nothing Then
                    btnExport.Dispose()
                    btnExport = Nothing
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
    Friend WithEvents pnlSearch As ZPanel
    Private WithEvents dgListaSustitucion As RadGridView
    Friend WithEvents pnlBottom As ZPanel
    Friend WithEvents btnOK As ZButton
    Friend WithEvents colCode As DataGridTextBoxColumn
    Friend WithEvents colDescription As DataGridTextBoxColumn
    Friend WithEvents btnSave As ZButton
    Friend WithEvents tbBuscar As TextBox
    Friend WithEvents btnMassiveInsert As ZButton
    Friend WithEvents lblSearch As ZLabel
    Friend WithEvents btnInsert As ZButton
    Friend WithEvents btEliminar As ZButton
    Friend WithEvents btnExport As ZButton
    Private TelerikMetroBlueTheme1 As Themes.TelerikMetroBlueTheme
    Friend WithEvents btnRefresh As ZButton
    Private radThemeManager1 As RadThemeManager
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        TelerikMetroBlueTheme1 = New Telerik.WinControls.Themes.TelerikMetroBlueTheme()
        radThemeManager1 = New Telerik.WinControls.RadThemeManager()
        dgListaSustitucion = New Telerik.WinControls.UI.RadGridView()
        colCode = New System.Windows.Forms.DataGridTextBoxColumn()
        colDescription = New System.Windows.Forms.DataGridTextBoxColumn()
        pnlSearch = New Zamba.AppBlock.ZPanel()
        btnModify = New ZButton()
        btEliminar = New ZButton()
        btnInsert = New ZButton()
        btnExport = New ZButton()
        lblSearch = New ZLabel()
        tbBuscar = New TextBox()
        btnRefresh = New ZButton()
        pnlBottom = New Zamba.AppBlock.ZPanel()
        btnMassiveInsert = New ZButton()
        btnSave = New ZButton()
        btnOK = New ZButton()
        CType(dgListaSustitucion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(dgListaSustitucion.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        pnlSearch.SuspendLayout()
        pnlBottom.SuspendLayout()
        SuspendLayout()
        '
        'dgListaSustitucion
        '
        dgListaSustitucion.BackColor = Color.White
        dgListaSustitucion.Dock = System.Windows.Forms.DockStyle.Fill
        dgListaSustitucion.Font = New Font("Microsoft Sans Serif", 8.0!)
        dgListaSustitucion.ForeColor = Color.Black
        dgListaSustitucion.ImeMode = System.Windows.Forms.ImeMode.[On]
        dgListaSustitucion.Location = New Point(0, 72)
        '
        '
        '
        dgListaSustitucion.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        dgListaSustitucion.MasterTemplate.EnableAlternatingRowColor = True
        dgListaSustitucion.MasterTemplate.ViewDefinition = TableViewDefinition2
        dgListaSustitucion.Name = "dgListaSustitucion"
        dgListaSustitucion.ReadOnly = True
        '
        '
        '
        dgListaSustitucion.RootElement.ControlBounds = New System.Drawing.Rectangle(0, 72, 240, 150)
        dgListaSustitucion.Size = New Size(419, 282)
        dgListaSustitucion.TabIndex = 1
        dgListaSustitucion.ThemeName = "TelerikMetroBlue"
        '
        'colCode
        '
        colCode.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        colCode.Format = ""
        colCode.FormatInfo = Nothing
        colCode.HeaderText = "Codigo"
        colCode.MappingName = "Codigo"
        colCode.NullText = ""
        colCode.Width = 50
        '
        'colDescription
        '
        colDescription.Format = ""
        colDescription.FormatInfo = Nothing
        colDescription.HeaderText = "Descripcion"
        colDescription.MappingName = "Descripcion"
        colDescription.NullText = ""
        colDescription.Width = 300
        '
        'pnlSearch
        '
        pnlSearch.BackColor = Color.FromArgb(214, 213, 217)
        pnlSearch.Controls.Add(btnModify)
        pnlSearch.Controls.Add(btEliminar)
        pnlSearch.Controls.Add(btnInsert)
        pnlSearch.Controls.Add(btnExport)
        pnlSearch.Controls.Add(lblSearch)
        pnlSearch.Controls.Add(tbBuscar)
        pnlSearch.Controls.Add(btnRefresh)
        pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        pnlSearch.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        pnlSearch.ForeColor = Color.FromArgb(76, 76, 76)
        pnlSearch.Location = New Point(0, 0)
        pnlSearch.Name = "pnlSearch"
        pnlSearch.Size = New Size(419, 72)
        pnlSearch.TabIndex = 0
        '
        'btnModify
        '
        btnModify.BackColor = Color.FromArgb(0, 157, 224)
        btnModify.FlatStyle = FlatStyle.Flat
        btnModify.ForeColor = Color.White
        btnModify.Location = New Point(301, 38)
        btnModify.Name = "btnModify"
        btnModify.Size = New Size(78, 26)
        btnModify.TabIndex = 5
        btnModify.Text = "Modificar"
        btnModify.UseVisualStyleBackColor = False
        '
        'btEliminar
        '
        btEliminar.BackColor = Color.FromArgb(0, 157, 224)
        btEliminar.FlatStyle = FlatStyle.Flat
        btEliminar.ForeColor = Color.White
        btEliminar.Location = New Point(138, 38)
        btEliminar.Name = "btEliminar"
        btEliminar.Size = New Size(78, 26)
        btEliminar.TabIndex = 4
        btEliminar.Text = "Eliminar"
        btEliminar.UseVisualStyleBackColor = False
        '
        'btnInsert
        '
        btnInsert.BackColor = Color.FromArgb(0, 157, 224)
        btnInsert.FlatStyle = FlatStyle.Flat
        btnInsert.ForeColor = Color.White
        btnInsert.Location = New Point(57, 38)
        btnInsert.Name = "btnInsert"
        btnInsert.Size = New Size(78, 26)
        btnInsert.TabIndex = 3
        btnInsert.Text = "Insertar"
        btnInsert.UseVisualStyleBackColor = False
        '
        'btnExport
        '
        btnExport.BackColor = Color.FromArgb(0, 157, 224)
        btnExport.FlatStyle = FlatStyle.Flat
        btnExport.ForeColor = Color.White
        btnExport.Location = New Point(219, 38)
        btnExport.Name = "btnExport"
        btnExport.Size = New Size(78, 26)
        btnExport.TabIndex = 2
        btnExport.Text = "Exportar"
        btnExport.UseVisualStyleBackColor = False
        '
        'lblSearch
        '
        lblSearch.BackColor = Color.Transparent
        lblSearch.Font = New Font("Verdana", 9.75!)
        lblSearch.FontSize = 9.75!
        lblSearch.ForeColor = Color.FromArgb(76, 76, 76)
        lblSearch.Location = New Point(8, 12)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(43, 22)
        lblSearch.TabIndex = 1
        lblSearch.Text = "Buscar:"
        lblSearch.TextAlign = ContentAlignment.MiddleLeft
        '
        'tbBuscar
        '
        tbBuscar.BackColor = Color.White
        tbBuscar.Font = New Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        tbBuscar.Location = New Point(57, 10)
        tbBuscar.Name = "tbBuscar"
        tbBuscar.Size = New Size(321, 22)
        tbBuscar.TabIndex = 0
        '
        'btnRefresh
        '
        btnRefresh.BackColor = Color.FromArgb(0, 157, 224)
        btnRefresh.BackgroundImage = Global.Zamba.Indexs.My.Resources.Resources.appbar1
        btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.ForeColor = Color.White
        btnRefresh.Location = New Point(382, 10)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(19, 19)
        btnRefresh.TabIndex = 6
        btnRefresh.UseVisualStyleBackColor = False
        '
        'pnlBottom
        '
        pnlBottom.AutoSize = True
        pnlBottom.BackColor = Color.Silver
        pnlBottom.Controls.Add(btnMassiveInsert)
        pnlBottom.Controls.Add(btnSave)
        pnlBottom.Controls.Add(btnOK)
        pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        pnlBottom.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        pnlBottom.ForeColor = Color.FromArgb(76, 76, 76)
        pnlBottom.Location = New Point(0, 354)
        pnlBottom.Name = "pnlBottom"
        pnlBottom.Size = New Size(419, 96)
        pnlBottom.TabIndex = 1
        '
        'btnMassiveInsert
        '
        btnMassiveInsert.BackColor = Color.FromArgb(0, 157, 224)
        btnMassiveInsert.Dock = System.Windows.Forms.DockStyle.Bottom
        btnMassiveInsert.FlatStyle = FlatStyle.Flat
        btnMassiveInsert.Font = New Font("Tahoma", 9.0!, FontStyle.Bold, GraphicsUnit.Point, 0)
        btnMassiveInsert.ForeColor = Color.White
        btnMassiveInsert.Location = New Point(0, 0)
        btnMassiveInsert.Name = "btnMassiveInsert"
        btnMassiveInsert.Size = New Size(419, 32)
        btnMassiveInsert.TabIndex = 0
        btnMassiveInsert.Text = "Inserción masiva"
        btnMassiveInsert.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        btnSave.BackColor = Color.FromArgb(0, 157, 224)
        btnSave.Dock = System.Windows.Forms.DockStyle.Bottom
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.Font = New Font("Tahoma", 9.0!, FontStyle.Bold, GraphicsUnit.Point, 0)
        btnSave.ForeColor = Color.White
        btnSave.Location = New Point(0, 32)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(419, 32)
        btnSave.TabIndex = 0
        btnSave.Text = "Guardar Listado"
        btnSave.UseVisualStyleBackColor = False
        '
        'btnOK
        '
        btnOK.BackColor = Color.FromArgb(0, 157, 224)
        btnOK.Dock = System.Windows.Forms.DockStyle.Bottom
        btnOK.FlatStyle = FlatStyle.Flat
        btnOK.Font = New Font("Tahoma", 9.0!, FontStyle.Bold)
        btnOK.ForeColor = Color.White
        btnOK.Location = New Point(0, 64)
        btnOK.Name = "btnOK"
        btnOK.Size = New Size(419, 32)
        btnOK.TabIndex = 0
        btnOK.Text = "Aceptar"
        btnOK.UseVisualStyleBackColor = False
        '
        'UCListaSustitucion
        '
        AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Controls.Add(dgListaSustitucion)
        Controls.Add(pnlBottom)
        Controls.Add(pnlSearch)
        Name = "UCListaSustitucion"
        Size = New Size(419, 450)
        CType(dgListaSustitucion.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(dgListaSustitucion, System.ComponentModel.ISupportInitialize).EndInit()
        pnlSearch.ResumeLayout(False)
        pnlSearch.PerformLayout()
        pnlBottom.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region

#Region "Atributos y Propiedades"

    Private _addedSustitutionItems As New Generic.List(Of SustitutionItem)
    Friend WithEvents btnModify As ZButton
    Private _removeSustitutionItems As New List(Of String)

    Public Property ds() As DataSet = New DataSet

    Public Property Tabla() As String

    Public Property IndexId() As Integer
    Public Property Admin() As Boolean

#End Region

    Private Sub UCListaSustitucion_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            Width = 450
            Height = 450
            tbBuscar.Focus()
            tbBuscar.Select()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub CargarGrilla(ByVal Reload As Boolean)
        Try
            ds.Merge(AutoSubstitutionBusiness.GetIndexData(IndexId, Reload))
            dgListaSustitucion.DataSource = ds.Tables(0)
            If ds.Tables(0).Rows.Count > 0 Then dgListaSustitucion.Rows(0).IsSelected = True
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Error al mostrar los valores del atributo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnOK.Click
        Try
            Accept()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnActualizar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSave.Click
        Actualizar(ds, Tabla)
        CargarGrilla(True)
        RaiseEvent EndOfCreatingTableOrList()
    End Sub

    Private Sub tbBuscar_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles tbBuscar.TextChanged
        Dim busqueda As String
        Dim dv As DataView = Nothing
        ' Dim dt As DataTable

        Try
            busqueda = tbBuscar.Text.Trim()
            If busqueda.Length > 0 Then
                If Not _ds Is Nothing AndAlso _ds.Tables.Count > 0 Then
                    dv = New DataView(_ds.Tables(0))

                    If IsNumeric(busqueda) Then
                        dv.RowFilter = "Descripcion LIKE '%" & busqueda & "%' OR Codigo LIKE '%" & busqueda & "%'"
                    Else
                        dv.RowFilter = "Descripcion LIKE '%" & busqueda & "%'"
                    End If

                    dgListaSustitucion.DataSource = dv.ToTable
                    If dv.Count > 0 Then dgListaSustitucion.Rows(0).IsSelected = True
                End If
            Else
                dgListaSustitucion.DataSource = _ds.Tables(0)
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, ex.Message)
        Finally
            If dv IsNot Nothing Then
                dv.Dispose()
                dv = Nothing
            End If
        End Try
    End Sub

    Private Sub BulkInsert_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnMassiveInsert.Click
        Try
            Dim frm As New FrmBulkInsert(IndexId)
            RemoveHandler frm.RefillGrid, AddressOf ReloadGrid
            AddHandler frm.RefillGrid, AddressOf ReloadGrid
            frm.ShowDialog()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnExport_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExport.Click
        Try
            Dim frmexport As New FrmExportList(IndexId)
            frmexport.ShowDialog()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub tbBuscar_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles tbBuscar.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtnAceptar_Click(btnOK, New EventArgs)
        End If
    End Sub

    Private Sub dgListaSustitucion_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles dgListaSustitucion.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtnAceptar_Click(btnOK, New EventArgs)
        End If
    End Sub

    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        MyBase.OnLoad(e)
    End Sub

    Private Sub BtnInsertar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInsert.Click
        Try
            Dim frmInsertItem As frmInsertItemListaSustitucion
            frmInsertItem = New frmInsertItemListaSustitucion(IndexId, GetExistingCodes())
            AddHandler frmInsertItem.NewItem, AddressOf AddItem
            frmInsertItem.ShowDialog()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btEliminar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btEliminar.Click
        Try
            Dim lista As New List(Of DataRow)
            For Each r As GridViewRowInfo In dgListaSustitucion.SelectedRows
                For Each row As DataRow In ds.Tables(0).Rows
                    If Not String.IsNullOrEmpty(row.ItemArray(0) AndAlso Not IsDBNull(row.ItemArray(0))) Then
                        If row.ItemArray(0) = r.Cells(0).Value Then
                            lista.Add(row)
                            _removeSustitutionItems.Add(r.DataBoundItem.Row("CODIGO"))
                            Exit For
                        End If
                    End If
                Next
            Next

            For Each item As DataRow In lista
                ds.Tables(0).Rows.Remove(item)
            Next
            dgListaSustitucion.Refresh()
            'For Each index As String In _removeSustitutionItems
            '    Dim index As String =
            '    ds.Tables(0).Rows.RemoveAt(dgListaSustitucion.SelectedRows)
            'Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Function GetExistingCodes() As List(Of String)
        Dim Codes As New List(Of String)

        For Each r As GridViewRowInfo In dgListaSustitucion.Rows
            Codes.Add(r.Cells(0).Value.ToString)
        Next

        Return Codes
    End Function

    Private Sub AddItem(ByVal Atributo As Int32, ByVal Codigo As String, ByVal Descripcion As String)
        If ds.Tables(0).Columns.Count = 0 Then
            ds.Tables(0).Columns.Add("Codigo")
            ds.Tables(0).Columns.Add("Descripcion")
        End If

        _addedSustitutionItems.Add(New SustitutionItem(Codigo, Descripcion))
        Dim NewRow As DataRow = ds.Tables(0).NewRow
        NewRow.Item(0) = Codigo
        NewRow.Item(1) = Descripcion
        ds.Tables(0).Rows.Add(NewRow)

        RaiseEvent CreatingSustTableOrList(True)
    End Sub

    Private Sub Actualizar(ByVal ds As DataSet, ByVal Tabla As String)
        Try
            If _addedSustitutionItems.Count > 0 Then
                AutoSubstitutionBusiness.AddItems(_addedSustitutionItems, IndexId)
                _addedSustitutionItems.Clear()
            End If

            If _removeSustitutionItems.Count > 0 Then
                AutoSubstitutionBusiness.RemoveItems(_removeSustitutionItems, IndexId)
                _removeSustitutionItems.Clear()
            End If

            MessageBox.Show("Tabla actualizada", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ocurrio un error al actualizar: " & ex.ToString, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ReloadGrid()
        Try
            dgListaSustitucion.DataSource = Nothing
            dgListaSustitucion.DataSource = ds.Tables(0)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub dgListaSustitucion_Navigate(ByVal sender As System.Object, ByVal ne As MouseEventArgs) Handles dgListaSustitucion.DoubleClick
        Try
            Accept()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub Accept()
        Dim CurrentIndex As Integer = dgListaSustitucion.CurrentRow.Index
        Dim Codigo As String = dgListaSustitucion.SelectedRows(0).Cells(0).Value.ToString()
        Dim Descripcion As String = dgListaSustitucion.SelectedRows(0).Cells(1).Value.ToString().Trim()
        tbBuscar.Text = ""
        RaiseEvent RowSelected(Codigo, Descripcion, CurrentIndex)
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnModify.Click
        Dim frmUpdate As New frmInsertItemListaSustitucion(IndexId)
        RemoveHandler frmInsertItemListaSustitucion.MidifiedIndex, AddressOf SaveModifiedIndex
        AddHandler frmInsertItemListaSustitucion.MidifiedIndex, AddressOf SaveModifiedIndex
        frmUpdate.txtCodigo.Text = dgListaSustitucion.SelectedRows(0).Cells(0).Value.ToString
        frmUpdate.txtDescripcion.Text = dgListaSustitucion.SelectedRows(0).Cells(1).Value.ToString
        frmUpdate.cmdAceptar.Visible = False
        frmUpdate.btnModify.Visible = True
        frmUpdate.ShowDialog()
        If frmUpdate.DialogResult = DialogResult.OK Then
            frmUpdate.btnModify.Visible = False
            frmUpdate.Dispose()
        End If
    End Sub

    Private Sub SaveModifiedIndex(ByVal codigo As String, ByVal description As String)
        Try
            Dim LastCode As String = dgListaSustitucion.SelectedRows(0).Cells(0).Value.ToString
            For Each row As DataRow In ds.Tables(0).Rows
                If String.Compare(row("codigo").ToString, LastCode) = 0 Then
                    row("codigo") = codigo
                    row("descripcion") = description
                End If
            Next
            ds.AcceptChanges()
            AutoSubstitutionBusiness.UpdateAddItem(codigo, description, LastCode, IndexId.ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Public Sub ClearSearch()
        Try
            tbBuscar.Text = String.Empty
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            ds.Clear()
            CargarGrilla(True)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
