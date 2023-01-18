Imports System.Drawing
Imports System.Windows.Forms
Imports Zamba.Grid.Grid
Imports Zamba.Filters
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Zamba.Filters.Interfaces

Public Class UCFusion2
    Inherits ZControl
    Implements IFilter


    Dim IL As New Zamba.AppBlock.ZIconsList
    'diego: creo esta variable para que cuando veo un versionado no me desordene el mismo
    Public gridsort As Boolean = True
    Public nombreDocumento_UserConfig As String = UserPreferences.getValue("ColumnNameNombreDelDocumento", Sections.UserPreferences, "Nombre del Documento")
    Public imagen_UserConfig As String = UserPreferences.getValue("ColumnNameImagen", Sections.UserPreferences, "Imagen")
    Public TipoDocumento_UserConfig As String = UserPreferences.getValue("ColumnNameTipoDocumento", Sections.UserPreferences, "Tipo Documento")
    Public FechaCreacion_UserConfig As String = UserPreferences.getValue("ColumnNameFechaCreacion", Sections.UserPreferences, "Fecha Creacion")
    Public FechaModificacion_UserConfig As String = UserPreferences.getValue("ColumnNameFechaModificacion", Sections.UserPreferences, "Fecha Modificacion")
    Public nombreOriginal_UserConfig As String = UserPreferences.getValue("ColumnNameNombreOriginal", Sections.UserPreferences, "Nombre Original")
    Public version_UserConfig As String = UserPreferences.getValue("ColumnNameVersion", Sections.UserPreferences, "Version")
    Public NroVersion_UserConfig As String = UserPreferences.getValue("ColumnNameNroVersion", Sections.UserPreferences, "Nro Version")

#Region " Código generado por el Diseñador de Windows Forms "
    Public Enum Modes
        Results
        AsociatedResults
        TasksResults
        AsociatedTasksResults
    End Enum
    Private pmode As Modes = Modes.Results

    Dim CurrentUserId As Int64

    Dim _fc As New FiltersComponent
    Public Property FC() As IFiltersComponent Implements IFilter.Fc
        Get
            Return _fc
        End Get
        Set(ByVal value As IFiltersComponent)
            _fc = value
        End Set
    End Property
    Public Sub New(ByVal Mode As Modes, ByVal CurrentUserId As Int64)
        MyBase.New()
        Me.CurrentUserId = CurrentUserId
        Me.FC = New Filters.FiltersComponent()
        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        Try

            'ContextMenu
            Me.pmode = Mode
            Me.GridView.ContextMenuStrip = DirectCast(contextMenuResult, ContextMenuStrip)
            Me.GridView.ContextMenuStrip.SendToBack()
            RemoveHandler GridView.OnRowClick, AddressOf GridView_Click
            RemoveHandler GridView.OnDoubleClick, AddressOf GridView_DoubleClick
            RemoveHandler GridView.OnRightClick, AddressOf ResultGrid_RightClick
            RemoveHandler GridView.OnRefreshClick, AddressOf refreshGridMethod
            AddHandler GridView.OnDoubleClick, AddressOf GridView_DoubleClick
            AddHandler GridView.OnRightClick, AddressOf ResultGrid_RightClick
            AddHandler GridView.OnRowClick, AddressOf GridView_Click
            AddHandler GridView.OnRefreshClick, AddressOf refreshGridMethod



        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
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
    ' Public WithEvents ZIconList As System.Windows.Forms.ImageList

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Public WithEvents GridView As GroupGrid
    'Friend WithEvents ButtonItem5 As TD.SandBar.ButtonItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCFusion2))
        Me.GridView = New GroupGrid(True, CurrentUserId, Me)
        ' Me.ZIconList = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'GridView
        '
        Me.GridView.BackColor = System.Drawing.Color.LightSteelBlue
        Me.GridView.DataSource = Nothing
        Me.GridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridView.ForeColor = System.Drawing.Color.Black
        Me.GridView.Location = New System.Drawing.Point(0, 0)
        Me.GridView.Name = "GridView"
        Me.GridView.Size = New System.Drawing.Size(912, 306)
        Me.GridView.TabIndex = 0
        Me.GridView.withExcel = False
        '
        'ZIconList
        '
        '   Me.ZIconList.ImageStream = CType(resources.GetObject("ZIconList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        '   Me.ZIconList.TransparentColor = System.Drawing.Color.Transparent
        '   Me.ZIconList.Images.SetKeyName(0, "")
        '   Me.ZIconList.Images.SetKeyName(1, "")
        '   Me.ZIconList.Images.SetKeyName(2, "")
        '   Me.ZIconList.Images.SetKeyName(3, "")
        '   Me.ZIconList.Images.SetKeyName(4, "")
        '   Me.ZIconList.Images.SetKeyName(5, "")
        '   Me.ZIconList.Images.SetKeyName(6, "")
        '   Me.ZIconList.Images.SetKeyName(7, "")
        '   Me.ZIconList.Images.SetKeyName(8, "")
        '   Me.ZIconList.Images.SetKeyName(9, "")
        '   Me.ZIconList.Images.SetKeyName(10, "")
        '   Me.ZIconList.Images.SetKeyName(11, "")
        '   Me.ZIconList.Images.SetKeyName(12, "")
        '   Me.ZIconList.Images.SetKeyName(13, "")
        '    Me.ZIconList.Images.SetKeyName(14, "")
        '    Me.ZIconList.Images.SetKeyName(15, "")
        '    Me.ZIconList.Images.SetKeyName(16, "")
        '    Me.ZIconList.Images.SetKeyName(17, "")
        '    Me.ZIconList.Images.SetKeyName(18, "")
        '    Me.ZIconList.Images.SetKeyName(19, "")
        '    Me.ZIconList.Images.SetKeyName(20, "")
        '    Me.ZIconList.Images.SetKeyName(21, "")
        '    Me.ZIconList.Images.SetKeyName(22, "")
        '    Me.ZIconList.Images.SetKeyName(23, "")
        '   Me.ZIconList.Images.SetKeyName(24, "")
        '    Me.ZIconList.Images.SetKeyName(25, "")
        '    Me.ZIconList.Images.SetKeyName(26, "")
        '    Me.ZIconList.Images.SetKeyName(27, "")
        '    Me.ZIconList.Images.SetKeyName(28, "")
        '    Me.ZIconList.Images.SetKeyName(29, "")
        '    Me.ZIconList.Images.SetKeyName(30, "")
        '    Me.ZIconList.Images.SetKeyName(31, "")
        '    Me.ZIconList.Images.SetKeyName(32, "")
        '
        'UCFusion2
        '
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.GridView)
        Me.Name = "UCFusion2"
        Me.Size = New System.Drawing.Size(912, 306)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "grilla"
    Public Event ResultSelected(ByRef Result As Result)
    Public Event ResultDoubleClick(ByRef Result As Result)
    Public Event _RefreshGrid(ByVal ReloadHistory As Boolean)
    'Dim resultList As New SortedList()
    ''DataTable de la Grilla
    Private dt As DataTable = New DataTable("DataSource")
    Public useVersion As Boolean
    Public HaveVersions As Boolean = False
    Private IndexToFilter As New DataTable()
    ''Inicializa los valores de la grilla
    Public Sub inicializarGrilla()
        Try
            Me.LoadGeneralRights()
            Me.GridView.AlwaysFit = True
            Me.GridView.UseZamba = True
            Me.GridView.WithExcel = True
            Me.GridView.showRefreshButton = True
            Me.GridView.ShortDateFormat = Boolean.Parse(UserPreferences.getValue("GridShortDateFormat", Sections.UserPreferences, "True"))
            Me.HaveVersions = False
            Me.ClearSearchs()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ResultGrid_RightClick(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim a As Zamba.Grid.OutlookGrid = DirectCast(sender, Grid.OutlookGrid)
        Dim CellEvent As DataGridViewCellMouseEventArgs = DirectCast(e, System.Windows.Forms.DataGridViewCellMouseEventArgs)

        a.CurrentCell = a.Rows(CellEvent.RowIndex).Cells(CellEvent.ColumnIndex)
        'a.CurrentCell.Selected = True

    End Sub
    ''Recarga el datatable en la grilla
    'si el datatable viene en nothing carga el original, sino, el ordenado por versiones
    ''' <history>
    ''' [Tomas] - 14/05/2009 - Modified - Se mueve de lugar la columna "DocTypeId"
    ''' [Sebastian] 18-08-09 MODIFIED - se agrega metodo para llena el combo de filtros de la grilla
    ''' [Sebastian] 12-11-2009 Modified Llamada de forma correcta al refresco de la grilla en los filtros.
    ''' </history>
    Public Sub refreshGrid(ByVal dtOrderedVersions As DataTable, ByRef ItsInsertedForm As Boolean, ByVal DeleteResultState As Boolean)
        Try
            If Not IsNothing(dt) AndAlso dt.Columns.Count > 0 Then
                Trace.WriteLineIf(ZTrace.IsVerbose, "Cargando la grilla", "Grilla de Documentos")

                If dt.Columns.Contains(imagen_UserConfig) = False Then
                    Me.dt.Columns.Add(imagen_UserConfig, GetType(Image))
                End If


                For Each dr As DataRow In dt.Rows
                    Dim iconID As Int32
                    If Int32.TryParse(dr.Item("Icon_id").ToString(), iconID) Then
                        dr.Item(imagen_UserConfig) = IL.ZIconList.Images(iconID)
                    End If
                Next

                If ItsInsertedForm Then
                    dt.Columns.Item(nombreDocumento_UserConfig).ColumnName = nombreDocumento_UserConfig
                    dt.Columns.Item(imagen_UserConfig).ColumnName = imagen_UserConfig
                    dt.Columns.Item(TipoDocumento_UserConfig).ColumnName = TipoDocumento_UserConfig
                    dt.Columns.Item(FechaCreacion_UserConfig).ColumnName = FechaCreacion_UserConfig
                    dt.Columns.Item(FechaModificacion_UserConfig).ColumnName = FechaModificacion_UserConfig
                    dt.Columns.Item(nombreOriginal_UserConfig).ColumnName = nombreOriginal_UserConfig
                    dt.Columns.Item(version_UserConfig).ColumnName = version_UserConfig
                    dt.Columns.Item(NroVersion_UserConfig).ColumnName = NroVersion_UserConfig

                    Me.dt.Columns.Item(nombreDocumento_UserConfig).SetOrdinal(0)
                    Me.dt.Columns.Item(imagen_UserConfig).SetOrdinal(1)
                    Me.dt.Columns.Item(TipoDocumento_UserConfig).SetOrdinal(2)
                    Me.dt.Columns.Item(FechaCreacion_UserConfig).SetOrdinal(Me.dt.Columns.Count - 1)
                    Me.dt.Columns.Item(FechaModificacion_UserConfig).SetOrdinal(Me.dt.Columns.Count - 1)
                    Me.dt.Columns.Item(nombreOriginal_UserConfig).SetOrdinal(Me.dt.Columns.Count - 1)
                    Me.dt.Columns.Item("Doc_type_Id").SetOrdinal(Me.dt.Columns.Count - 1)
                Else
                    '(pablo)
                    'se agrega validacion: cuando elimino un documento y luego realizo el refresco
                    'este se hace sobre el mismo datatable, y al ser el mismo datatable no debe efectuarse
                    'nuevamente la re-asignacion de las columnas por UserConfig
                    If Not DeleteResultState Then
                        dt.Columns.Item("Nombre del Documento").ColumnName = nombreDocumento_UserConfig
                        dt.Columns.Item("Tipo de Documento").ColumnName = TipoDocumento_UserConfig
                        dt.Columns.Item("Fecha Creacion").ColumnName = FechaCreacion_UserConfig
                        dt.Columns.Item("Fecha Modificacion").ColumnName = FechaModificacion_UserConfig
                        dt.Columns.Item("Nombre Original").ColumnName = nombreOriginal_UserConfig
                        dt.Columns.Item("Version").ColumnName = version_UserConfig
                        dt.Columns.Item("Numero de Version").ColumnName = NroVersion_UserConfig
                        '  dt.Columns.Item("Imagen").ColumnName = imagen_UserConfig
                    End If

                    Me.dt.Columns.Item(nombreDocumento_UserConfig).SetOrdinal(0)
                    Me.dt.Columns.Item(imagen_UserConfig).SetOrdinal(1)
                    Me.dt.Columns.Item(TipoDocumento_UserConfig).SetOrdinal(2)
                    Me.dt.Columns.Item(FechaCreacion_UserConfig).SetOrdinal(Me.dt.Columns.Count - 1)
                    Me.dt.Columns.Item(FechaModificacion_UserConfig).SetOrdinal(Me.dt.Columns.Count - 1)
                    Me.dt.Columns.Item(nombreOriginal_UserConfig).SetOrdinal(Me.dt.Columns.Count - 1)
                    Me.dt.Columns.Item("Doc_type_Id").SetOrdinal(Me.dt.Columns.Count - 1)
                End If

                Me.dt.AcceptChanges()


                Try
                    If useVersion = True Then
                        If ItsInsertedForm Then
                            dt.Columns.Item("Version").ColumnName = version_UserConfig
                            Me.dt.Columns.Item("Version").SetOrdinal(Me.dt.Columns.Count - 1)
                        Else
                            dt.Columns.Item(version_UserConfig).ColumnName = version_UserConfig
                            Me.dt.Columns.Item(version_UserConfig).SetOrdinal(Me.dt.Columns.Count - 1)
                        End If
                        Me.dt.Columns.Item("ver_Parent_Id").SetOrdinal(Me.dt.Columns.Count - 1)
                        'Me.dt.Columns.Item("Iddoc").SetOrdinal(Me.dt.Columns.Count - 1)
                        Me.dt.Columns.Item("doc_id").SetOrdinal(Me.dt.Columns.Count - 1)


                        Me.dt.Columns.Item("ver_Parent_Id").ColumnName = "ver_Parent_Id"
                        Me.dt.AcceptChanges()

                        If HaveVersions = True AndAlso IsNothing(dtOrderedVersions) Then
                            OrderDocumentsByVersions()
                        ElseIf HaveVersions = True Then
                            Me.GridView.DataSource = dtOrderedVersions
                        Else
                            Me.GridView.DataSource = dt
                        End If
                    Else
                        Me.GridView.DataSource = dt
                    End If

                    Trace.WriteLineIf(ZTrace.IsVerbose, "Se recargaran los filtros.")
                    Me.GridView.ReloadLsvFilters()

                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                If dt.Rows.Count > 0 Then
                    'Oculto las columnas que no se deben mostrar
                    For Each column As DataColumn In dt.Columns
                        If getVisibility(column.ColumnName) = False Then
                            Me.GridView.SetColumnVisible(column.ColumnName, False)
                        End If
                    Next

                    Me.GridView.SetColumnFixed(nombreDocumento_UserConfig, True, Int32.Parse(UserPreferences.getValue("AnchoColumnaNombreDocumento", Sections.UserPreferences, 250)))
                    Me.GridView.SetColumnFixed(TipoDocumento_UserConfig, True, Int32.Parse(UserPreferences.getValue("AnchoColumnaTipoDocumento", Sections.UserPreferences, 150)))
                End If

                If Boolean.Parse(UserPreferences.getValue("GroupByFolder", Sections.UserPreferences, True)) = True Then

                    Me.GridView.Group(Me.GridView.OutLookGrid.Columns("Folder_Id"))
                End If

                ' Me.GridView.LoadDefaultSearchToListView(dt, ItsInsertedForm)
                '[Sebastian] 12-11-2009 se realizo la llamada correcta al refresco de la grilla para aplicar los filtros.
                Me.GridView.Refresh()
                Me.GridView.FixColumns()

                If Me.GridView.OutLookGrid.Rows.Count > 0 Then
                    Me.GridView.OutLookGrid.Rows(0).Selected = True
                    Dim selectedResult As Result = GetResultFromDataRow(dt.Rows(0))
                    If Not IsNothing(selectedResult) Then
                        LoadRights(selectedResult)
                    End If
                End If
                Trace.WriteLineIf(ZTrace.IsVerbose, "Carga finalizada", "Grilla de Documentos")
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private hideColumns As New ArrayList
    ''' <summary>
    ''' Obtiene la visibilidad de las columnas
    ''' </summary>
    ''' <history>   Marcelo Created 21/08/2009</history>
    ''' <remarks></remarks>
    Private Function getVisibility(ByVal columnName As String) As Boolean
        If hideColumns.Count = 0 Then
            hideColumns.Add("folder_id")
            hideColumns.Add("doc_id")
            hideColumns.Add("doc_type_id")
            hideColumns.Add("ver_parent_id")
            hideColumns.Add("disk_group_id")
            hideColumns.Add("platter_id")
            hideColumns.Add("vol_id")
            hideColumns.Add("offset")
            hideColumns.Add("name")
            hideColumns.Add("icon_id")
            hideColumns.Add("shared")
            hideColumns.Add("rootid")
            hideColumns.Add("original_filename")
            hideColumns.Add("disk_vol_id")
            hideColumns.Add("disk_vol_path")
            hideColumns.Add("doc_file")
            If useVersion = True Then
                hideColumns.Add("iddoc")
            Else
                hideColumns.Add(version_UserConfig.ToLower())
                hideColumns.Add(NroVersion_UserConfig.ToLower())
            End If
            'If Boolean.Parse(UserPreferences.getValue("ShowGridColumnNombreOriginal", Sections.UserPreferences, False)) = False Then
            '    hideColumns.Add("nombre original")
            'End If

            'If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowEntityNameColumn)) = True Then
            '    hideColumns.Add("nombre original")
            'End If

            Select Case pmode
                Case Modes.Results
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.ResultGridShowResultNameColumn)) = False Then
                        hideColumns.Add(nombreDocumento_UserConfig.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.ResultGridShowIconNameColumn)) = False Then
                        hideColumns.Add(imagen_UserConfig.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.ResultGridShowDocumentType)) = False Then
                        hideColumns.Add(TipoDocumento_UserConfig.ToLower)
                    End If
                    If useVersion And Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.ResultGridShowVersionNumberColumn)) = False Then
                        hideColumns.Add(NroVersion_UserConfig.ToLower)
                    End If
                    If useVersion And Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.ResultGridShowVersionColumn)) = False Then
                        hideColumns.Add(version_UserConfig.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.ResultGridShowCreatedDateColumn)) = False Then
                        hideColumns.Add(FechaCreacion_UserConfig.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.ResultGridShowLastEditDateColumn)) = False Then
                        hideColumns.Add(FechaModificacion_UserConfig.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.ResultGridShowEntityNameColumn)) = False Then
                        hideColumns.Add(nombreOriginal_UserConfig.ToLower)
                    End If
                Case Modes.AsociatedResults
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowResultNameColumn)) = False Then
                        hideColumns.Add(nombreDocumento_UserConfig.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowIconNameColumn)) = False Then
                        hideColumns.Add(imagen_UserConfig.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowDocumentType)) = False Then
                        hideColumns.Add(TipoDocumento_UserConfig.ToLower)
                    End If
                    If useVersion And Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowVersionColumn)) = False Then
                        hideColumns.Add(version_UserConfig.ToLower)
                    End If
                    If useVersion And Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowVersionNumberColumn)) = False Then
                        hideColumns.Add(NroVersion_UserConfig.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowCreatedDateColumn)) = False Then
                        hideColumns.Add(FechaCreacion_UserConfig.ToLower.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowLastEditDateColumn)) = False Then
                        hideColumns.Add(FechaModificacion_UserConfig.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.ResultGridShowEntityNameColumn)) = False Then
                        hideColumns.Add(nombreOriginal_UserConfig.ToLower)
                    End If
                Case Modes.AsociatedTasksResults
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowResultNameColumn)) = False Then
                        hideColumns.Add(nombreDocumento_UserConfig.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowIconNameColumn)) = False Then
                        hideColumns.Add(imagen_UserConfig.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowDocumentType)) = False Then
                        hideColumns.Add(TipoDocumento_UserConfig.ToLower)
                    End If
                    If useVersion And Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowVersionNumberColumn)) = False Then
                        hideColumns.Add(NroVersion_UserConfig.ToLower)
                    End If
                    If useVersion And Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowVersionColumn)) = False Then
                        hideColumns.Add(version_UserConfig.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowCreatedDateColumn)) = False Then
                        hideColumns.Add(FechaCreacion_UserConfig.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowLastEditDateColumn)) = False Then
                        hideColumns.Add(FechaModificacion_UserConfig.ToLower)
                    End If
                    If Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.AsociatedResultGridShowDocumentType)) = False Then
                        hideColumns.Add(nombreOriginal_UserConfig.ToLower)
                    End If
            End Select
        End If
        If hideColumns.Contains(columnName.ToLower()) Then
            Return False
        ElseIf columnName.StartsWith("I") AndAlso IsNumeric(columnName.Remove(0, 1)) Then
            Return False
        Else
            Return True
        End If
    End Function


    ''Llena la grilla
    Dim LastSearch As ISearch
    Public Sub FillResults(ByVal Results As DataTable, ByVal Search As ISearch) 'Implements IUCFusion2.FillResults
        Try

            If (Not Search Is Nothing) Then Me.LastSearch = Search
            If Results Is Nothing Then
                Me.dt.Rows.Clear()
                Me.dt.Columns.Clear()
            Else
                Me.dt = Results.Clone()
                Me.dt.Merge(Results)
            End If



            refreshGrid(Nothing, False, False)


        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''Castea el tipo de un índice a Type
    Private Function GetIndexType(ByVal iType As IndexDataType) As Type
        Dim indexType As Type

        Select Case iType
            Case IndexDataType.Alfanumerico
                indexType = GetType(String)
            Case IndexDataType.Alfanumerico_Largo
                indexType = GetType(String)
            Case IndexDataType.Fecha
                indexType = GetType(Date)
            Case IndexDataType.Fecha_Hora
                indexType = GetType(DateTime)
            Case IndexDataType.Moneda
                indexType = GetType(Decimal)
            Case IndexDataType.None
                indexType = GetType(String)
            Case IndexDataType.Numerico
                indexType = GetType(Int64)
            Case IndexDataType.Numerico_Decimales
                indexType = GetType(Decimal)
            Case IndexDataType.Numerico_Largo
                indexType = GetType(Decimal)
            Case IndexDataType.Si_No
                indexType = GetType(String)
            Case Else
                indexType = GetType(String)
        End Select

        Return indexType
    End Function


    ''Agrega un result a la grilla
    ''' [Tomas] - 14/05/2009 - Modified - Se mueve de lugar la columna "DocTypeId"
    Public Sub AddResult(ByVal Result As Result, Optional ByVal exists As Boolean = False) 'Implements IUCFusion2.AddResult
        Try
            Dim ColumnType As Type = GetType(String)
            Dim ItsInsertedForm As Boolean = True
            If Not IndexToFilter.Columns.Contains("Index") Then IndexToFilter.Columns.Add("Index", ColumnType)
            If Not IndexToFilter.Columns.Contains("IndexName") Then IndexToFilter.Columns.Add("IndexName", ColumnType)

            If dt.Columns.Contains("imagen") = False AndAlso dt.Columns.Contains(imagen_UserConfig) = False Then
                Me.dt.Columns.Add(imagen_UserConfig, GetType(Image))
                Me.dt.Columns.Add("doc_id")
                Me.dt.Columns.Add("folder_id")
                Me.dt.Columns.Add("disk_group_id")
                Me.dt.Columns.Add("platter_id")
                Me.dt.Columns.Add("vol_id")
                Me.dt.Columns.Add("doc_file")
                Me.dt.Columns.Add("offset")
                Me.dt.Columns.Add("doc_type_id")
                Me.dt.Columns.Add(nombreDocumento_UserConfig)
                Me.dt.Columns.Add("icon_id")
                Me.dt.Columns.Add("shared")
                Me.dt.Columns.Add("ver_parent_id")
                Me.dt.Columns.Add("rootid")
                Me.dt.Columns.Add(nombreOriginal_UserConfig)
                Me.dt.Columns.Add(version_UserConfig)
                Me.dt.Columns.Add(NroVersion_UserConfig)
                Me.dt.Columns.Add("disk_vol_id")
                Me.dt.Columns.Add("disk_vol_path")
                Me.dt.Columns.Add(TipoDocumento_UserConfig)
                Me.dt.Columns.Add(FechaCreacion_UserConfig)
                Me.dt.Columns.Add(FechaModificacion_UserConfig)
            End If

            'Por cada índice
            For Each i As Index In Result.Indexs
                'Si todavía no fue creada una columna con ese índice
                If Not Me.dt.Columns.Contains(i.Name) Then
                    '[Sebastian] cargo los indices que voy a usar para filtrar la grilla.

                    'Creo un tipo por default String
                    Dim iType As Type = GetType(String)

                    'Y si el tipo del indice no es nada, le cargo el type
                    If Not IsNothing(i.Type) Then
                        If i.DropDown = IndexAdditionalType.AutoSustitución Then
                            iType = GetType(String)
                            Me.dt.Columns.Add("I" & i.ID, iType)
                        Else
                            iType = GetIndexType(i.Type)
                        End If
                    End If

                    'Y por último genero la columna con ese tipo
                    Me.dt.Columns.Add(i.Name.Trim, iType)
                    Dim NewRow As DataRow = IndexToFilter.NewRow()
                    NewRow.Item("Index") = i.ID
                    NewRow.Item("IndexName") = i.Name
                    IndexToFilter.Rows.Add(NewRow)
                End If
            Next

            Dim row As DataRow = Nothing

            If exists = False Then
                row = dt.NewRow
            Else
                For Each oldRow As DataRow In dt.Rows
                    If Int32.Parse(oldRow.Item("doc_id").ToString()) = Result.ID Then
                        row = oldRow
                        Exit For
                    End If
                Next
            End If

            If row Is Nothing AndAlso dt.Rows.Count = 1 Then
                dt.Rows(0).Item("doc_id") = Result.ID
                row = dt.Rows(0)
            End If

            Try
                row.Item("DISK_GROUP_ID") = Result.Disk_Group_Id
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                row.Item("PLATTER_ID") = Result.Platter_Id
            Catch ex As Exception
            End Try
            Try
                row.Item("Doc_File") = Result.Doc_File
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                row.Item("OFFSET") = Result.OffSet
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                If Result.isShared = True Then
                    row.Item("SHARED") = 1
                Else
                    row.Item("SHARED") = 0
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                row.Item("ver_Parent_id") = Result.ParentVerId
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                row.Item(version_UserConfig) = Result.HasVersion
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                row.Item("RootId") = Result.RootDocumentId
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                row.Item(nombreOriginal_UserConfig) = Result.OriginalName
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                row.Item(NroVersion_UserConfig) = Result.VersionNumber
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
            Try
                row.Item("DISK_GROUP_ID") = Result.Disk_Group_Id
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                row.Item("Disk_vol_path") = Result.DISK_VOL_PATH
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            row.Item(FechaCreacion_UserConfig) = Result.CreateDate

            If dt.Columns.Contains("icon_id") Then
                row.Item("icon_id") = Result.IconId
            End If
            row.Item(imagen_UserConfig) = IL.ZIconList.Images(Result.IconId)

            If Result.ISVIRTUAL Then
                row.Item(imagen_UserConfig) = IL.ZIconList.Images(30)
            End If

            If Not Result.Name Is Nothing Then
                row.Item(nombreDocumento_UserConfig) = Result.Name
            Else
                row.Item(nombreDocumento_UserConfig) = Result.DocType.Name
            End If

            row.Item("Folder_Id") = Result.FolderId

            Dim t As Type = GetType(String)

            For Each indice As Index In Result.Indexs
                Try
                    'Si Data tiene un valor que se le asigne al Item
                    If String.Compare(String.Empty, indice.Data) <> 0 Then
                        If indice.DropDown <> IndexAdditionalType.AutoSustitución Then
                            row.Item(indice.Name) = indice.Data
                        Else
                            If String.Compare(String.Empty, indice.dataDescription) <> 0 Then
                                row.Item(indice.Name) = indice.dataDescription
                            Else
                                row.Item(indice.Name) = indice.Data
                            End If
                            row.Item("I" & indice.ID) = indice.Data
                        End If
                        'Si Data no tiene valor se le asigna el de DataDescription
                        '(si es que no esta vacío)
                    ElseIf String.Compare(String.Empty, indice.dataDescription) <> 0 Then
                        row.Item(indice.Name) = indice.dataDescription
                    End If
                Catch
                End Try
            Next

            Try
                If useVersion = True Then
                    row.Item("ver_Parent_Id") = Result.ParentVerId
                    row.Item(NroVersion_UserConfig) = Result.VersionNumber
                    'Comento esta linea xq tira exception, hay que revisar su funcionalidad
                    'row.Item("IdDoc") = Result.ID
                End If
                row.Item(TipoDocumento_UserConfig) = Result.Parent.Name
                row.Item("Doc_id") = Result.ID
                row.Item("Doc_type_Id") = Result.DocType.ID
            Catch ex As Exception
                useVersion = False
                ZClass.raiseerror(ex)
            End Try

            If exists = False Then
                dt.Rows.Add(row)
            End If
            Me.dt.AcceptChanges()
            Me.IndexToFilter.AcceptChanges()
            refreshGrid(Nothing, ItsInsertedForm, False)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''Limpia la grilla
    ''' [Tomas] - 14/05/2009 - Modified - Se mueve de lugar la columna "DocTypeId"
    Public Sub ClearSearchs() 'Implements IUCFusion2.ClearSearchs
        Try
            dt.Rows.Clear()
            dt.Columns.Clear()
            Me.GridView.OutLookGrid.Columns.Clear()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''Selecciona la fila correspondiente al result
    Public Sub SelectResult(ByVal Result As Result) 'Implements IUCFusion2.SelectResult
        Try
            Me.GridView.Refresh()
            Dim S As Zamba.Grid.OutlookGridRow = Me.FindSubResultRoot(Result)
            If Not IsNothing(S) Then
                If Me.GridView.OutLookGrid.SelectedRows(0) Is S Then
                    Exit Sub
                End If
                SelectAllToFalse()

                S.Selected = True
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub SelectResult(ByVal Result As Result, ByVal selectNewVersion As Boolean) 'Implements IUCFusion2.SelectResult
        'Die Sobrecarga creada para que se seleccione el primer result y ponga en negrita el result
        'quize poner un parametro opcional pero no me lo permitia por tener un delegado referenciando el metodo
        Try
            Dim S As Zamba.Grid.OutlookGridRow = Me.FindSubResultRoot(Result)
            If Not IsNothing(S) Then
                If Me.GridView.OutLookGrid.SelectedRows(0) Is S Then
                    Exit Sub
                End If
                SelectAllToFalse()

                If selectNewVersion Then
                    Dim BoldFont As New Font(Font, FontStyle.Bold)
                    For index As Integer = 0 To S.Cells.Count - 1
                        S.Cells(index).Style.Font = BoldFont
                    Next
                End If

                S.Selected = True
                Me.GridView.Refresh()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''Deselecciona todas las filas de la grilla
    Private Sub SelectAllToFalse()
        Try
            For Each Item As Zamba.Grid.OutlookGridRow In Me.GridView.OutLookGrid.Rows
                Item.Selected = False
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''Quita del dataset la fila que corresponde con el result
    Public Sub RemoveResult(ByVal Result As Result) 'Implements IUCFusion2.RemoveResult
        Try
            For Each row As DataRow In Me.dt.Rows
                If String.Compare(row.Item("Doc_Id").ToString(), Result.ID.ToString()) = 0 Then
                    Me.dt.Rows.Remove(row)
                    Me.dt.AcceptChanges()
                    Exit For
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''Selecciona el ultimo valor de la grilla
    Public Function SelectFirstResultOfLastSearch() As Boolean 'Implements IUCFusion2.SelectFirstResultOfLastSearch
        Try
            If Me.GridView.OutLookGrid.Rows.Count > 0 Then
                Me.GridView.OutLookGrid.Rows(Me.GridView.OutLookGrid.Rows.Count - 1).Selected = True
                Return True
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return False
    End Function


    ''' <summary>
    ''' Selecciona un result y lo muestra
    ''' </summary>
    ''' <param name="_result"></param>
    ''' <remarks></remarks>
    Public Sub SelectandShowResult(ByVal _result As Result)
        Try
            If GridView.OutLookGrid.Rows.Count > 0 Then
                Me.GridView.Refresh()
                Dim S As Zamba.Grid.OutlookGridRow = Me.FindSubResultRoot(_result)
                If Not IsNothing(S) Then
                    '                    If Me.GridView.OutLookGrid.SelectedRows(0) Is S Then
                    ''      Exit Sub
                    '  End If
                    SelectAllToFalse()

                    S.Selected = True
                End If
                Dim arrayResults() As Result = SelectedResults(True)
                If Not IsNothing(arrayResults) AndAlso (arrayResults.Length > 0) Then
                    arrayResults(0).CurrentFormID = _result.CurrentFormID
                    RaiseEvent ResultDoubleClick(arrayResults(0))
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    'Actualiza el result(por ahora borra y vuelve a agregarlo)
    Public Sub UpdateResult(ByVal Result As Result) 'Implements IUCFusion2.UpdateResult
        AddResult(Result, True)
        refreshGrid(Nothing, False, False)
        SelectResult(Result)
    End Sub

    'Devuelve la fila seleccionada si la encuentra, sino nothing
    Private Function FindSubResultRoot(ByVal Result As Result) As Grid.OutlookGridRow
        If Not IsNothing(GridView.OutLookGrid.Rows) AndAlso GridView.OutLookGrid.Rows.Count > 0 Then
            Try
                For Each row As Grid.OutlookGridRow In Me.GridView.OutLookGrid.Rows
                    If Not IsNothing(row.Cells("Doc_Id").Value) AndAlso Int32.Parse(row.Cells("Doc_Id").Value.ToString()) = Result.ID Then
                        Return row
                    End If
                Next
                Return Nothing
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return Nothing
            End Try
        Else
            Return Nothing
        End If
    End Function

    ''Devuelve un array con los results seleccionados o nothing si no hay ninguno
    ''' <summary>
    ''' [Sebastian] 10-06-2009 Modified se agrego cast para salvar warnings
    ''' </summary>
    ''' <returns></returns>
    ''' <history> Marcelo modified 20/08/2009 </history>
    ''' <remarks></remarks>
    Public Function SelectedResultsList() As Generic.List(Of IResult)
        Try
            Dim Results As New Generic.List(Of IResult)
            For Each row As Grid.OutlookGridRow In Me.GridView.OutLookGrid.SelectedRows()
                If Not IsNothing(row.Cells("doc_id").Value) Then
                    Dim doctypeid As Int64 = CInt(row.Cells("doc_type_id").Value)

                    Dim r As Result = New Result(CInt(row.Cells("doc_id").Value), DocTypesBusiness.GetDocType(doctypeid, True), row.Cells(nombreDocumento_UserConfig).Value.ToString(), 0, CInt(row.Cells("folder_id").Value))
                    CompleteDocument(r, row)
                    Results.Add(r)
                End If
            Next
            Return Results
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Devuelve un array con los results seleccionados o nothing si no hay ninguno
    ''' </summary>
    ''' <returns></returns>
    ''' <history> Marcelo modified 20/08/2009 </history>
    ''' <remarks></remarks>
    Public Function SelectedResults(ByVal SelectFirstResult As Boolean) As Result()
        Try
            Dim a As Int32 = 0
            Dim arrayResults As New ArrayList
            For Each row As Grid.OutlookGridRow In Me.GridView.OutLookGrid.SelectedRows()
                If Not IsNothing(row.Cells("doc_id").Value) Then
                    If Core.Cache.DocTypesAndIndexs.hsDocTypes.Contains(CLng(row.Cells("doc_type_id").Value)) = False Then
                        Dim _doctype As New DocType(CInt(row.Cells("doc_type_id").Value), row.Cells(TipoDocumento_UserConfig).Value.ToString(), 0)
                        _doctype.Indexs = ZCore.FilterIndex(CInt(_doctype.ID))
                        Core.Cache.DocTypesAndIndexs.hsDocTypes.Add(CLng(row.Cells("doc_type_id").Value), _doctype)
                    End If
                    Dim doctypeid As Int64 = CInt(row.Cells("doc_type_id").Value)

                    If CInt(row.Cells("doc_id").Value) = 0 Then
                        Dim r As NewResult = New NewResult()
                        r.DocTypeId = doctypeid
                        CompleteDocument(r, row)
                        arrayResults.Add(r)
                    Else
                        Dim r As Result = New Result(CInt(row.Cells("doc_id").Value), DocTypesBusiness.GetDocType(doctypeid, True), row.Cells(nombreDocumento_UserConfig).Value.ToString(), 0, CInt(row.Cells("folder_id").Value))
                        CompleteDocument(r, row)
                        arrayResults.Add(r)
                    End If
                End If
                If SelectFirstResult = True Then Exit For
            Next
            If arrayResults.Count > 0 Then
                Dim resultArray(arrayResults.Count - 1) As Result
                arrayResults.ToArray.CopyTo(resultArray, 0)
                Return resultArray
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
        Return Nothing
    End Function


    ''' <summary>
    ''' Devuelve un array con los results seleccionados o nothing si no hay ninguno
    ''' </summary>
    ''' <returns></returns>
    ''' <history> Tomas modified 07/01/2011</history>
    ''' <remarks></remarks>
    Public Function GetResultFromDataRow(ByVal resultAsDr As DataRow) As Result
        Try
            Dim docTypeId As Int64 = CLng(resultAsDr.Item("doc_type_id").ToString)
            Dim docId As Int64 = CLng(resultAsDr.Item("doc_id").ToString)
            Dim folderId As Int64 = CLng(resultAsDr.Item("folder_Id").ToString)

            If Core.Cache.DocTypesAndIndexs.hsDocTypes.Contains(docTypeId) = False Then
                Dim _doctype As DocType

                If resultAsDr.Table().Columns.Contains(TipoDocumento_UserConfig) Then
                    _doctype = New DocType(docTypeId, resultAsDr.Item(TipoDocumento_UserConfig).ToString(), 0)
                Else
                    _doctype = New DocType(docTypeId, resultAsDr.Item("Tipo de Documento").ToString(), 0)
                End If

                _doctype.Indexs = ZCore.FilterIndex(CInt(_doctype.ID))
                Core.Cache.DocTypesAndIndexs.hsDocTypes.Add(docTypeId, _doctype)
            End If

            If docId = 0 Then
                Dim r As NewResult = New NewResult()
                r.DocTypeId = docTypeId
                CompleteDocument(r, resultAsDr)
                Return r
            Else
                Dim r As Result
                If resultAsDr.Table.Columns.Contains("Nombre del Documento") Then
                    r = New Result(docId, DocTypesBusiness.GetDocType(docTypeId, True), resultAsDr.Item("Nombre del Documento").ToString(), 0, folderId)
                Else
                    r = New Result(docId, DocTypesBusiness.GetDocType(docTypeId, True), resultAsDr.Item(nombreDocumento_UserConfig).ToString(), 0, folderId)
                End If

                CompleteDocument(r, resultAsDr)
                Return r
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
        Return Nothing
    End Function


    'Dispara el evento doubleClick pasando como parametro el primer result selecionado
    Private Sub GridView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If GridView.OutLookGrid.SelectedRows.Count > 0 Then
                Dim arrayResults() As Result = SelectedResults(True)
                If Not IsNothing(arrayResults) AndAlso (arrayResults.Length > 0) Then
                    RaiseEvent ResultDoubleClick(arrayResults(0))
                End If
            End If
            Me.Select()
            Me.GridView.FixColumns()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub refreshGridMethod()
        RaiseEvent _RefreshGrid(False)
    End Sub
    'Dispara el evento ResultSelected pasando como parametro el primer result selecionado
    Private Sub GridView_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If GridView.OutLookGrid.SelectedRows.Count > 0 Then
                Dim arrayResults() As Result = SelectedResults(True)
                If Not IsNothing(arrayResults) Then
                    LoadRights(arrayResults(0))
                    RaiseEvent ResultSelected(arrayResults(0))
                End If
            End If
            'Me.Select()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    'Dispara el evento ResultSelected pasando como parametro el primer result selecionado
    Public Sub SelectFirstResult()
        Try
            If GridView.OutLookGrid.Rows.Count > 0 Then
                GridView.OutLookGrid.Rows(0).Selected = True
                Dim arrayResults() As Result = SelectedResults(True)
                If Not IsNothing(arrayResults) AndAlso (arrayResults.Length > 0) Then
                    RaiseEvent ResultDoubleClick(arrayResults(0))
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    'Dispara el evento ResultSelected pasando como parametro el primer result selecionado
    Public Sub SelectDataRowResult(ByVal resultAsDr As DataRow)
        Try
            If GridView.OutLookGrid.Rows.Count > 0 Then
                GridView.OutLookGrid.Rows(0).Selected = True
                Dim selectedResult As Result = GetResultFromDataRow(resultAsDr)
                If Not IsNothing(selectedResult) Then
                    RaiseEvent ResultDoubleClick(selectedResult)
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Permisos"
    Private Sub LoadRights(ByRef Result As Result)

        If Result.ID <> 0 Then
            'EIMINAR
            If Boolean.Parse(UserPreferences.getValue("ShowDeleteButton", Sections.UserPreferences, True)) = True AndAlso UserBusiness.Rights.GetUserRights(ObjectTypes.DocTypes, RightsType.Delete, Result.DocType.ID) = True Then
                Me.contextMenuResult.EliminarToolStripMenuItem.Visible = True
            Else
                Me.contextMenuResult.EliminarToolStripMenuItem.Visible = False
            End If

            'CAMBIAR NOMBRE
            If Boolean.Parse(UserPreferences.getValue("ShowChangeNameButton", Sections.UserPreferences, True)) = True AndAlso UserBusiness.Rights.GetUserRights(ObjectTypes.DocTypes, RightsType.ChangeName, Result.DocType.ID) = True Then
                Me.contextMenuResult.CambiarNombreToolStripMenuItem.Visible = True
            Else
                Me.contextMenuResult.CambiarNombreToolStripMenuItem.Visible = False
            End If

            'MOVER
            If Boolean.Parse(UserPreferences.getValue("ShowMoveButton", Sections.UserPreferences, True)) = True AndAlso UserBusiness.Rights.GetUserRights(ObjectTypes.DocTypes, RightsType.Move, Result.DocType.ID) = True Then
                Me.contextMenuResult.btMoverCopiarDocumento.Visible = True
            Else
                Me.contextMenuResult.btMoverCopiarDocumento.Visible = False
            End If


            'HISTORIAL
            If Not UserBusiness.Rights.GetUserRights(ObjectTypes.FrmDocHistory, RightsType.View) Then
                Me.contextMenuResult.btHistorial.Visible = False
            Else
                Me.contextMenuResult.btHistorial.Visible = True
            End If


            'EDITOR TIF
            If Boolean.Parse(UserPreferences.getValue("ShowTifEditorButton", Sections.UserPreferences, True)) = True AndAlso UserBusiness.Rights.GetUserRights(ObjectTypes.DocTypes, RightsType.Edit, Result.DocType.ID) = True AndAlso Result.IsTif Then
                Me.contextMenuResult.btnEditar.Visible = True
            Else
                Me.contextMenuResult.btnEditar.Visible = False
            End If

            'EXOPORT TO PDF
            If Result.IsImage Or Result.IsWord Then
                Me.contextMenuResult.ExportarAPDFToolStripMenuItem.Visible = True
            Else
                Me.contextMenuResult.ExportarAPDFToolStripMenuItem.Visible = False
            End If

            'PROPIEDADES
            If Not UserBusiness.Rights.GetUserRights(ObjectTypes.FrmDocProperty, RightsType.View) Then
                Me.contextMenuResult.PropiedadesToolStripMenuItem.Visible = False
            Else
                Me.contextMenuResult.PropiedadesToolStripMenuItem.Visible = True
            End If


            'WORKFLOW
            If Not UserBusiness.Rights.GetUserRights(ObjectTypes.ModuleWorkFlow, RightsType.Use) Then
                Me.contextMenuResult.AgregarAWorkFlowToolStripMenuItem.Visible = False
            Else
                Me.contextMenuResult.AgregarAWorkFlowToolStripMenuItem.Visible = True
            End If

            'VERSIONES
            useVersion = UserBusiness.Rights.GetUserRights(ObjectTypes.ModuleVersions, RightsType.Use)
            Me.contextMenuResult.MenuShowVersionComment.Visible = useVersion

            'OFFICE
            If Zamba.Tools.EnvironmentUtil.getOfficePlatform() > Tools.EnvironmentUtil.OfficeVersions.Office2000 Then
                Me.GridView.WithExcel = True
            End If

            'Se habilita el resto por defecto que no se encuentra controlado por permisos.
            Me.contextMenuResult.BorrarBusquedasToolStripMenuItem.Visible = True
            Me.contextMenuResult.GenerarLinkAResultadoToolStripMenuItem.Visible = True
            Me.contextMenuResult.GenerarLinkAResultadoWebToolStripMenuItem.Visible = True
            Me.contextMenuResult.ImprimirIndicesToolStripMenuItem.Visible = True
            Me.contextMenuResult.ToolStripSeparator1.Visible = True
            Me.contextMenuResult.ToolStripSeparator2.Visible = True
            Me.contextMenuResult.ToolStripSeparator3.Visible = True
            Me.contextMenuResult.ToolStripSeparator4.Visible = True
            Me.contextMenuResult.ToolStripSeparator5.Visible = True
            Me.contextMenuResult.ToolStripSeparator6.Visible = True

        Else
            'Si doc_id = 0 entonces es una insercion y no deberia cargarse ninguna opcion.
            Me.contextMenuResult.EliminarToolStripMenuItem.Visible = False
            Me.contextMenuResult.CambiarNombreToolStripMenuItem.Visible = False
            Me.contextMenuResult.btMoverCopiarDocumento.Visible = False
            Me.contextMenuResult.btHistorial.Visible = False
            Me.contextMenuResult.btnEditar.Visible = False
            Me.contextMenuResult.ExportarAPDFToolStripMenuItem.Visible = False
            Me.contextMenuResult.AgregarAWorkFlowToolStripMenuItem.Visible = False
            Me.contextMenuResult.ToolStripSeparator1.Visible = False
            Me.contextMenuResult.ToolStripSeparator2.Visible = False
            Me.contextMenuResult.ToolStripSeparator3.Visible = False
            Me.contextMenuResult.ToolStripSeparator4.Visible = False
            Me.contextMenuResult.ToolStripSeparator5.Visible = False
            Me.contextMenuResult.ToolStripSeparator6.Visible = False
            Me.contextMenuResult.BorrarBusquedasToolStripMenuItem.Visible = False
            Me.contextMenuResult.GenerarLinkAResultadoToolStripMenuItem.Visible = False
            Me.contextMenuResult.GenerarLinkAResultadoWebToolStripMenuItem.Visible = False
            Me.contextMenuResult.ImprimirIndicesToolStripMenuItem.Visible = False
            Me.contextMenuResult.MenuInsertDocument.Visible = False
            Me.contextMenuResult.PropiedadesToolStripMenuItem.Visible = False
            Me.contextMenuResult.MenuShowVersionComment.Visible = False
            useVersion = UserBusiness.Rights.GetUserRights(ObjectTypes.ModuleVersions, RightsType.Use)

            'OFFICE
            If Zamba.Tools.EnvironmentUtil.getOfficePlatform() > Tools.EnvironmentUtil.OfficeVersions.Office2000 Then
                Me.GridView.WithExcel = True
            End If
        End If


    End Sub
#End Region

#Region "Toolbar"

    Public Event Imprimir(ByVal Results() As Result, ByVal OnlyIndexs As Boolean)
    '   Public Event AdjuntarAEmail(ByVal Results() As Result)
    '  Public Event AdjuntarAMensaje(ByVal Results() As Result)
    '  Public Event AgregarDocumentoALaCarpeta(ByRef Result As Result)
    ' Public Event VerDocumentosAsociados(ByRef Result As Result)
    ' Public Event VerVersionesDelDocumento(ByRef Result As Result)
    ' Public Event AgregarNuevaVersion(ByRef Result As Result)
    '  Public Event GuardarDocumento()
    ' Public Event GuardarDocumentoComo(ByRef Result As Result)
    '  Public Event GuardarEnDiskete(ByRef Result As Result)
    ' Public Event ShowWfTask(ByRef Result As Result)
    '  Public Event ChangePosition()



    Private Sub _Imprimir(ByVal OnlyIndexs As Boolean)
        Dim results() As Result = Me.SelectedResults(False)
        If Not IsNothing(results) Then
            RaiseEvent Imprimir(results, OnlyIndexs)
        End If
    End Sub

    'Private Sub _AdjuntarAEmail()
    '    Dim results() As Result = Me.SelectedResults
    '    If Not IsNothing(results) Then
    '        RaiseEvent AdjuntarAEmail(results)
    '    End If
    'End Sub

    'Private Sub _AdjuntarAMensaje()
    '    Dim results() As Result = Me.SelectedResults
    '    If Not IsNothing(results) Then
    '        RaiseEvent AdjuntarAMensaje(results)
    '    End If
    'End Sub

    'Private Sub _AgregarDocumentoALaCarpeta()
    '    Dim results() As Result = Me.SelectedResults
    '    If Not IsNothing(results) Then
    '        RaiseEvent AgregarDocumentoALaCarpeta(results(0))
    '    End If
    'End Sub

    'Private Sub _VerDocumentosAsociados()
    '    Dim results() As Result = Me.SelectedResults
    '    If Not IsNothing(results) Then
    '        RaiseEvent VerDocumentosAsociados(results(0))
    '    End If
    'End Sub

    'Private Sub _VerVersionesDelDocumento()
    '    HaveVersions = True
    '    Dim results() As Result = Me.SelectedResults
    '    If Not IsNothing(results) Then
    '        RaiseEvent VerVersionesDelDocumento(results(0))
    '    End If
    'End Sub

    'Private Sub _AgregarNuevaVersion()
    '    Dim results() As Result = Me.SelectedResults
    '    If Not IsNothing(results) Then
    '        If results(0).HasVersion = 1 AndAlso UserBusiness.Rights.GetUserRights( ObjectTypes.DocTypes,  RightsType.AddFromVersions, results(0).DocType.ID) Then
    '            RaiseEvent AgregarNuevaVersion(results(0))
    '        Else
    '            If results(0).HasVersion = 0 Then
    '                RaiseEvent AgregarNuevaVersion(results(0))
    '            Else
    '                MessageBox.Show("No tiene los permisos necesarios para crear una nueva version de un documento Versionado", "Zamba", MessageBoxButtons.OK)
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub _GuardarEnDiskete()
    '    Dim results() As Result = Me.SelectedResults
    '    If Not IsNothing(results) Then
    '        RaiseEvent GuardarEnDiskete(results(0))
    '    End If
    'End Sub

    'Private Sub _ImprimirEnExcel()
    '    Dim results() As Result = Me.SelectedResults
    '    If Not IsNothing(results) Then
    '        RaiseEvent ImprimirEnExcel(results(0))
    '    End If
    'End Sub

    'Private Sub _GuardarDocumento()
    '    RaiseEvent GuardarDocumento()
    'End Sub

    'Private Sub _GuardarDocumentoComo()
    '    Dim results() As Result = Me.SelectedResults
    '    If Not IsNothing(results) Then
    '        RaiseEvent GuardarDocumentoComo(results(0))
    '    End If
    'End Sub

    ''' <summary>
    ''' Carga las tareas del WF con este result seleccionado
    ''' </summary>
    ''' <param name="Result">El result que va a estar seleccionado</param>
    ''' <remarks></remarks>
    'Private Sub ShowTask(ByRef Result As Result)
    '    Try
    '        RaiseEvent ShowWfTask(Result)
    '    Catch ex As Exception
    '         ZClass.raiseerror(ex)
    '    End Try
    'End Sub


#End Region

#Region "Clase"
    Private Sub UCFusion2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ResizeRedraw = True
        LoadGeneralRights()
        'Grilla
        inicializarGrilla()
    End Sub

    Private Sub LoadGeneralRights()
        useVersion = UserBusiness.Rights.GetUserRights(ObjectTypes.ModuleVersions, RightsType.Use)
        contextMenuResult.GenerarLinkAResultadoWebToolStripMenuItem.Enabled = Boolean.Parse(UserPreferences.getValue("ShowGenerateLinkContextMenu", Sections.UserPreferences, "True"))
        contextMenuResult.MenuInsertDocument.Enabled = Boolean.Parse(UserPreferences.getValue("ShowInsertRelationContextMenu", Sections.UserPreferences, "True"))
    End Sub

#End Region


#Region "ContextMenu"
    Public Event MoverCopiarDocumento(ByRef Result As Result)
    Public Event EditarPag(ByRef Result As Result)
    Public Event CambiarNombre(ByRef Result As Result)
    Public Event Eliminar(ByVal Results() As Result)
    Public Event Historial(ByRef Result As Result)
    Public Event Propiedades(ByRef Result As Result)
    Public Event GenerarLink(ByRef Result As Result, ByVal TResult As Boolean)
    Public Event SelecDocument(ByVal resul As Result)
    Public Event GenerarListado(ByVal Result As Result)
    Public Event EliminarBusqAnt()
    Public Event ExportaPdf(ByRef Result As Result)
    Public Event AdjuntarAWF(ByVal Results As ArrayList)
    Public Event ExportarAExcel(ByRef Result As Result)
    Public Event ConvertToPdf(ByVal Results() As Result)
    Public Event CloseResult(ByRef Result As Core.Result)
    Public Event InsertRelationedResult(ByVal result As Result, ByVal relationId As Int32)
    'Public Event ShowTask(ByVal TaskId As Int64)
    'Public Event ShowTasks()

    Friend WithEvents contextMenuResult As UCContextResult = New UCContextResult()

    Private Sub contextMenuResult_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles contextMenuResult.ItemClicked
        Try
            contextMenuResult.Close()
            Select Case CStr(e.ClickedItem.Tag)
                Case "GenerateLink"
                    _GenerarLink(True)
                Case "GenerateLinkWeb"
                    _GenerarLink(False)
                Case "ClearSearch"
                    _EliminarBusqAnt()
                Case "Property"
                    _Propiedades()
                Case "ChangeName"
                    _CambiarNombre()
                Case "Delete"
                    _Eliminar()
                Case "ExportToPDF"
                    _ConvertToPdf()
                Case "AddToWF"
                    _AdjuntarAWF()
                Case "MoveCopyDoc"
                    _MoverCopiarDocumento()
                Case "EditTIF"
                    RaiseEvent EditarPag(Me.SelectedResults(True)(0))
                Case "History"
                    _Historial()
                Case "PrintIndexs"
                    Me._Imprimir(True)
                Case "ShowVersion"
                    Me.ShowVersionComment()
                Case "InsertDocument"
                    Dim frm As New frmShowDocRelations
                    frm.ShowDialog()
                    If frm.DialogResult = DialogResult.OK Then
                        Dim relationid As Int32 = frm.SelectedRelationId
                        RaiseEvent InsertRelationedResult(Me.SelectedResults(True)(0), relationid)
                    End If
            End Select
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub




    Private Sub _GenerarLink(ByVal TResult As Boolean)
        Dim results() As Result = Me.SelectedResults(True)
        If Not IsNothing(results) Then
            RaiseEvent GenerarLink(results(0), TResult)
        End If
    End Sub

    Private Sub _ConvertToPdf()
        Dim results() As Result = Me.SelectedResults(False)
        If Not IsNothing(results) Then
            RaiseEvent ConvertToPdf(results)
        End If
    End Sub

    Private Sub _EliminarBusqAnt()
        RaiseEvent EliminarBusqAnt()
    End Sub

    ''' <summary>
    ''' Método que se ejecuta cuando se selecciona el elemento "Agregar a Workflow" del menú contextual
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]   07/05/2009  Modified     Se verifica la existencia de documentos con id 0, si es así entonces aparece un mensaje de error
    ''' </history>
    Private Sub _AdjuntarAWF()

        Dim results() As Result = Me.SelectedResults(False)

        If Not IsNothing(results) Then

            For Each Result As Result In results

                If (Result.ID = 0) Then
                    MessageBox.Show("No se puede ingresar el documento al workflow, ya que el mismo aún no fue insertado", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            Next

            RaiseEvent AdjuntarAWF(New ArrayList(results))

        End If

    End Sub

    Private Sub _ExportarAExcel()
        Dim results() As Result = Me.SelectedResults(True)
        If Not IsNothing(results) Then
            RaiseEvent ExportarAExcel(results(0))
        End If
    End Sub


    Public Event ShowComment()
    Private Sub ShowVersionComment()
        RaiseEvent ShowComment()
    End Sub

    Private Sub _MoverCopiarDocumento()
        Dim results() As Result = Me.SelectedResults(True)
        If Not IsNothing(results) Then
            RaiseEvent MoverCopiarDocumento(results(0))
        End If
    End Sub

    Private Sub _CambiarNombre()
        Dim results() As Result = Me.SelectedResults(True)
        If Not IsNothing(results) Then
            RaiseEvent CambiarNombre(results(0))
        End If
    End Sub

    Private Sub _Eliminar()
        Dim results() As Result = Me.SelectedResults(False)
        If Not IsNothing(results) Then
            RaiseEvent Eliminar(results)
            'FillResults(Results_Business.
            'refreshGrid(Nothing)
        End If
    End Sub


    Private Sub _Historial()
        Dim results() As Result = Me.SelectedResults(True)
        If Not IsNothing(results) Then
            RaiseEvent Historial(results(0))
        End If
    End Sub

    Private Sub _Propiedades()
        Dim results() As Result = Me.SelectedResults(True)
        If Not IsNothing(results) Then
            RaiseEvent Propiedades(results(0))
        End If
    End Sub








#End Region

#Region "Versiones"
    Public Sub OrderDocumentsByVersions()
        Dim dtCloned As DataTable = dt.Copy

        ' el Datatable.Select crea una copia por referencia, por lo tanto
        ' Hay que evitar que se modifiquen sus datos clonandola
        Dim dt1 As DataTable = New DataTable
        For Each col As DataColumn In dtCloned.Columns
            dt1.Columns.Add(col.ColumnName, col.DataType)
        Next
        'se consiguen los documentos roots (no tienen parents)
        Dim drRoots As DataRow() = dtCloned.Select("ver_Parent_Id = 0")
        Dim parentid As Int64
        'se recorre la coleccion en busca de childs
        'dtCloned.Columns("Version").DataType = GetType(Int32)

        For Each dr As DataRow In drRoots
            dt1.Rows.Add(dr.ItemArray())
            parentid = Int64.Parse(dr.Item("doc_id").ToString)
            getChilds(dt1, parentid, dtCloned, "  ")
        Next
        If dt1.Rows.Count > 0 Then
            refreshGrid(dt1, False, False)
        Else

            refreshGrid(dt, False, False)
        End If
    End Sub

    Private Sub getChilds(ByVal dt1 As DataTable, ByVal parentid As Int64, ByVal dtCloned As DataTable, ByVal name As String)
        Dim drChilds As DataRow() = dtCloned.Select("ver_Parent_Id = " + parentid.ToString, version_UserConfig)
        'Dim DV As New DataView
        'DV.Table = dtCloned
        'DV.RowFilter = 
        'DV.Sort = "Version"
        For Each drc As DataRow In drChilds
            Dim Nombre As String = drc.Item(0).ToString

            Nombre = Nombre.Insert(0, name & "--")

            drc.Item(0) = Nombre.ToString

            dt1.Rows.Add(drc.ItemArray())
            parentid = Int64.Parse(drc.Item("doc_id").ToString)
            getChilds(dt1, parentid, dtCloned, name & "  ")
        Next
    End Sub
#End Region

    ''' <summary>
    ''' Creo el result a partir de la fila de la grilla
    ''' </summary>
    ''' <param name="_Result"></param>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Sub CompleteDocument(ByRef _Result As Result, ByVal dr As Grid.OutlookGridRow)
        Try
            _Result.Disk_Group_Id = CInt(dr.Cells("DISK_GROUP_ID").Value)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.Platter_Id = CInt(dr.Cells("PLATTER_ID").Value)
        Catch ex As Exception
        End Try
        Try
            _Result.Doc_File = dr.Cells("Doc_File").Value.ToString()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.OffSet = CInt(dr.Cells("OFFSET").Value)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If CInt(dr.Cells("SHARED").Value) = 1 Then
                _Result.isShared = True
            Else
                _Result.isShared = False
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.ParentVerId = CInt(dr.Cells("ver_Parent_id").Value)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.HasVersion = CInt(dr.Cells(version_UserConfig).Value)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.RootDocumentId = CInt(dr.Cells("RootId").Value)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.OriginalName = dr.Cells(nombreOriginal_UserConfig).Value.ToString()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.VersionNumber = CInt(dr.Cells(NroVersion_UserConfig).Value)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            _Result.Disk_Group_Id = CInt(dr.Cells("DISK_GROUP_ID").Value)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.DISK_VOL_PATH = dr.Cells("Disk_vol_path").Value.ToString()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.UserID = _Result.Platter_Id
            _Result.OwnerID = _Result.Platter_Id
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        If Not IsDBNull(dr.Cells(FechaCreacion_UserConfig).Value) Then
            If Not IsNothing(dr.Cells(FechaCreacion_UserConfig).Value) Then
                If Not String.IsNullOrEmpty(dr.Cells(FechaCreacion_UserConfig).Value.ToString()) Then
                    Try
                        If Not IsNothing(_Result) Then
                            _Result.CreateDate = DateTime.Parse(dr.Cells(FechaCreacion_UserConfig).Value.ToString())
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End If
            End If
        End If

        For Each indice As IIndex In _Result.DocType.Indexs
            If indice.DropDown = IndexAdditionalType.AutoSustitución Then
                If Not IsDBNull(dr.Cells(indice.Name).Value) Then
                    indice.Data = dr.Cells("I" & indice.ID).Value.ToString()
                    indice.dataDescription = dr.Cells(indice.Name).Value.ToString()
                    'r.Table.Columns.Remove(r.Table.Columns("I" & indice.ID))
                End If
            Else
                indice.Data = dr.Cells(indice.Name).Value.ToString()
            End If
        Next
        _Result.Indexs = _Result.DocType.Indexs
    End Sub

    ''' <summary>
    ''' Creo el result a partir de la fila de la grilla
    ''' </summary>
    ''' <param name="_Result"></param>
    ''' <param name="dr"></param>}
    ''' <history>
    ''' Tomas   07/11/2011  Created     Se creo una sobrecarga del m{etodo para que acepte datarow.
    ''' </history>
    ''' <remarks></remarks>
    Private Sub CompleteDocument(ByRef _Result As Result, ByVal dr As DataRow)
        dr = dt.Rows(0)
        Try
            _Result.Disk_Group_Id = CInt(dr.Item("DISK_GROUP_ID").ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.Platter_Id = CInt(dr.Item("PLATTER_ID").ToString)
        Catch ex As Exception
        End Try
        Try
            _Result.Doc_File = dr.Item("Doc_File").ToString
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.OffSet = CInt(dr.Item("OFFSET").ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If CInt(dr.Item("SHARED").ToString) = 1 Then
                _Result.isShared = True
            Else
                _Result.isShared = False
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.ParentVerId = CInt(dr.Item("ver_Parent_id").ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.HasVersion = CInt(dr.Item(version_UserConfig).ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.RootDocumentId = CInt(dr.Item("RootId").ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.OriginalName = dr.Item(nombreOriginal_UserConfig).ToString
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.Table().Columns.Contains(TipoDocumento_UserConfig) Then
                _Result.VersionNumber = CInt(dr.Item(NroVersion_UserConfig).ToString)
            Else
                _Result.VersionNumber = CInt(dr.Item("Numero de Version").ToString)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            _Result.Disk_Group_Id = CInt(dr.Item("DISK_GROUP_ID").ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.DISK_VOL_PATH = dr.Item("Disk_vol_path").ToString
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            _Result.UserID = _Result.Platter_Id
            _Result.OwnerID = _Result.Platter_Id
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try


        If Not IsDBNull(dr.Item(FechaCreacion_UserConfig).ToString) Then
            If Not IsNothing(dr.Item(FechaCreacion_UserConfig).ToString) Then
                If Not String.IsNullOrEmpty(dr.Item(FechaCreacion_UserConfig).ToString) Then
                    Try
                        If Not IsNothing(_Result) Then
                            _Result.CreateDate = DateTime.Parse(dr.Item(FechaCreacion_UserConfig).ToString)
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End If
            End If
        End If
        For Each indice As IIndex In _Result.DocType.Indexs
            If indice.DropDown = IndexAdditionalType.AutoSustitución Then
                If Not IsDBNull(dr.Item(indice.Name).ToString) Then
                    indice.Data = dr.Item("I" & indice.ID).ToString
                    indice.dataDescription = dr.Item(indice.Name).ToString
                    'r.Table.Columns.Remove(r.Table.Columns("I" & indice.ID))
                End If
            Else
                indice.Data = dr.Item(indice.Name).ToString
            End If
        Next
        _Result.Indexs = _Result.DocType.Indexs
    End Sub

    ''' <summary>
    ''' Creo el result a partir de la fila de la grilla
    ''' </summary>
    ''' <param name="_Result"></param>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Sub CompleteDocument(ByRef _Result As NewResult, ByVal dr As Grid.OutlookGridRow)
        _Result.DocType = DocTypesBusiness.GetDocType(_Result.DocTypeId, True)
        _Result.Name = dr.Cells(nombreDocumento_UserConfig).Value.ToString()
        _Result.FolderId = CInt(dr.Cells("folder_id").Value)


        Try
            _Result.Disk_Group_Id = CInt(dr.Cells("DISK_GROUP_ID").Value)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.Platter_Id = CInt(dr.Cells("PLATTER_ID").Value)
        Catch ex As Exception
        End Try
        Try
            _Result.Doc_File = dr.Cells("Doc_File").Value.ToString()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.OffSet = CInt(dr.Cells("OFFSET").Value)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If CInt(dr.Cells("SHARED").Value) = 1 Then
                _Result.isShared = True
            Else
                _Result.isShared = False
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.ParentVerId = CInt(dr.Cells("ver_Parent_id").Value)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.HasVersion = CInt(dr.Cells("Version").Value)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.RootDocumentId = CInt(dr.Cells("RootId").Value)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.OriginalName = dr.Cells(nombreOriginal_UserConfig).Value.ToString()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.VersionNumber = CInt(dr.Cells(NroVersion_UserConfig).Value)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            _Result.Disk_Group_Id = CInt(dr.Cells("DISK_GROUP_ID").Value)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.DISK_VOL_PATH = dr.Cells("Disk_vol_path").Value.ToString()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.UserID = _Result.Platter_Id
            _Result.OwnerID = _Result.Platter_Id
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        If Not IsDBNull(dr.Cells(FechaCreacion_UserConfig).Value) Then
            If Not IsNothing(dr.Cells(FechaCreacion_UserConfig).Value) Then
                If Not String.IsNullOrEmpty(dr.Cells(FechaCreacion_UserConfig).Value.ToString()) Then
                    Try
                        If Not IsNothing(_Result) Then
                            _Result.CreateDate = DateTime.Parse(dr.Cells(FechaCreacion_UserConfig).Value.ToString())
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End If
            End If
        End If

        For Each indice As IIndex In _Result.DocType.Indexs
            If indice.DropDown = IndexAdditionalType.AutoSustitución Then
                If Not IsDBNull(dr.Cells("I" & indice.ID).Value) Then
                    indice.Data = dr.Cells("I" & indice.ID).Value.ToString()
                    indice.dataDescription = dr.Cells(indice.Name).Value.ToString()
                    'r.Table.Columns.Remove(r.Table.Columns("I" & indice.ID))
                End If
            Else
                indice.Data = dr.Cells(indice.Name).Value.ToString()
            End If
        Next
        _Result.Indexs = _Result.DocType.Indexs
    End Sub
    ''' <summary>
    ''' Creo el result a partir de la fila
    ''' </summary>
    ''' <param name="_Result"></param>
    ''' <param name="dr"></param>
    ''' <history>
    ''' Tomas   07/11/2011  Created     Se creo una sobrecarga del m{etodo para que acepte datarow.
    ''' </history>
    ''' <remarks></remarks>
    Private Sub CompleteDocument(ByRef _Result As NewResult, ByVal dr As DataRow)
        _Result.DocType = DocTypesBusiness.GetDocType(_Result.DocTypeId, True)
        _Result.Name = dr.Item(nombreDocumento_UserConfig).ToString()
        _Result.FolderId = CInt(dr.Item("folder_id").ToString)


        Try
            _Result.Disk_Group_Id = CInt(dr.Item("DISK_GROUP_ID").ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.Platter_Id = CInt(dr.Item("PLATTER_ID").ToString)
        Catch ex As Exception
        End Try
        Try
            _Result.Doc_File = dr.Item("Doc_File").ToString()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.OffSet = CInt(dr.Item("OFFSET").ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If CInt(dr.Item("SHARED").ToString) = 1 Then
                _Result.isShared = True
            Else
                _Result.isShared = False
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.ParentVerId = CInt(dr.Item("ver_Parent_id").ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.HasVersion = CInt(dr.Item("Version").ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.RootDocumentId = CInt(dr.Item("RootId").ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.OriginalName = dr.Item(nombreOriginal_UserConfig).ToString
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.Table().Columns.Contains(TipoDocumento_UserConfig) Then
                _Result.VersionNumber = CInt(dr.Item(NroVersion_UserConfig).ToString)
            Else
                _Result.VersionNumber = CInt(dr.Item("Numero de Version").ToString)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            _Result.Disk_Group_Id = CInt(dr.Item("DISK_GROUP_ID").ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.DISK_VOL_PATH = dr.Item("Disk_vol_path").ToString
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.UserID = _Result.Platter_Id
            _Result.OwnerID = _Result.Platter_Id
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        If Not IsDBNull(dr.Item(FechaCreacion_UserConfig).ToString) Then
            If Not IsNothing(dr.Item(FechaCreacion_UserConfig).ToString) Then
                If Not String.IsNullOrEmpty(dr.Item(FechaCreacion_UserConfig).ToString) Then
                    Try
                        If Not IsNothing(_Result) Then
                            _Result.CreateDate = DateTime.Parse(dr.Item(FechaCreacion_UserConfig).ToString)
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End If
            End If
        End If

        For Each indice As IIndex In _Result.DocType.Indexs
            If indice.DropDown = IndexAdditionalType.AutoSustitución Then
                If Not IsDBNull(dr.Item("I" & indice.ID).ToString) Then
                    indice.Data = dr.Item("I" & indice.ID).ToString
                    indice.dataDescription = dr.Item(indice.Name).ToString
                    'r.Table.Columns.Remove(r.Table.Columns("I" & indice.ID))
                End If
            Else
                indice.Data = dr.Item(indice.Name).ToString
            End If
        Next
        _Result.Indexs = _Result.DocType.Indexs
    End Sub

    Private Sub New()

    End Sub

    Private _lastPage As Integer

    Public Property LastPage() As Integer Implements IFilter.LastPage
        Get
            Return _lastPage
        End Get
        Set(ByVal value As Integer)
            _lastPage = value
        End Set
    End Property
    Public Sub ShowTaskOfDT() Implements IFilter.ShowTaskOfDT
        Dim dt As DataTable = Zamba.Core.Search.ModDocuments.DoSearch(LastSearch, Zamba.Core.UserBusiness.CurrentUser.ID, FC, LastPage, Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)), False, False, False, True)
        FillResults(dt, LastSearch)
    End Sub
End Class
