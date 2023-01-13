Imports Zamba.Grid.Grid
Imports Zamba.Filters
Imports Zamba.Core
Imports Zamba.Core.DocTypes.DocAsociated
Imports Telerik.WinControls.UI
Imports System.Collections.Generic
Imports Zamba.Core.WF.WF
Imports Zamba.Data

Public Class UCFusion2
    Inherits ZControl
    Implements IGrid
    Implements IMenuContextContainer

    'Dim IL As New ZIconsList
    Dim IconsHelper As New Zamba.AppBlock.WFNodeHelper

    Public gridsort As Boolean = True

#Region " Código generado por el Diseñador de Windows Forms "
    Public Enum Modes
        Results
        AsociatedResults
        TasksResults
        AsociatedTasksResults
    End Enum
    Private pmode As Modes = Modes.Results
    Public currentContextMenu As UCGridContextMenu
    Dim CurrentUserId As Int64 = 0
    Dim _result As IResult
    Public ReadOnly Property MenuContextContainer As IMenuContextContainer
    Dim _fc As New FiltersComponent
    Public Property FC() As IFiltersComponent Implements IGrid.Fc
        Get
            Return _fc
        End Get
        Set(ByVal value As IFiltersComponent)
            _fc = value
        End Set
    End Property
    Public ReadOnly Property FilterCount() As Int32
        Get
            Return GridView.FilterCount
        End Get
    End Property

    Public Sub New(ByVal Mode As Modes, ByVal CurrentUserId As Int64, ByVal res As IResult, ByRef MenuContextContainer As IMenuContextContainer)
        MyBase.New()

        'Se declaran estas variables antes del InitializeComponent porque el método las requiere completas
        Me.CurrentUserId = CurrentUserId
        _result = res
        Me.MenuContextContainer = MenuContextContainer
        currentContextMenu = New UCGridContextMenu(Me)
        FC = New Filters.FiltersComponent()
        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        Try

            'ContextMenu
            pmode = Mode

            If pmode = Modes.AsociatedResults Or pmode = Modes.AsociatedTasksResults Then
                If Not IsNothing(GridView) Then
                    GridView.filterType = FilterTypes.Asoc
                End If
            End If
            RemoveHandler GridView.NewGrid.ContextMenuOpening, AddressOf newGrid_ContextMenuOpening

            RemoveHandler GridView.OnRightClick, AddressOf ResultGrid_RightClick
            RemoveHandler GridView.OnRowClick, AddressOf GridView_Click
            RemoveHandler GridView.OnDoubleClick, AddressOf GridView_DoubleClick
            RemoveHandler GridView.OnRefreshClick, AddressOf refreshGridMethod

            AddHandler GridView.OnRightClick, AddressOf ResultGrid_RightClick
            AddHandler GridView.OnDoubleClick, AddressOf GridView_DoubleClick
            AddHandler GridView.OnRowClick, AddressOf GridView_Click
            AddHandler GridView.OnRefreshClick, AddressOf refreshGridMethod


            AddHandler GridView.NewGrid.ContextMenuOpening, AddressOf newGrid_ContextMenuOpening

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub ResultGrid_RightClick(ByVal sender As Object, ByVal e As EventArgs)

        Dim a As Zamba.Grid.NewGrid = DirectCast(sender, Grid.NewGrid)
        Dim CellEvent As ContextMenuOpeningEventArgs = DirectCast(e, ContextMenuOpeningEventArgs)

    End Sub


    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            If disposing Then
                If Not (components Is Nothing) Then components.Dispose()

                RaiseEvent ClearReferences(Me)
                If GridView IsNot Nothing Then
                    RemoveHandler GridView.OnRowClick, AddressOf GridView_Click
                    RemoveHandler GridView.OnDoubleClick, AddressOf GridView_DoubleClick
                    RemoveHandler GridView.OnRefreshClick, AddressOf refreshGridMethod
                    RemoveHandler GridView.OnRightClick, AddressOf ResultGrid_RightClick
                    GridView.Dispose()
                End If
                If dt IsNot Nothing Then dt.Dispose()
                If IndexToFilter IsNot Nothing Then IndexToFilter.Dispose()
            End If
            MyBase.Dispose(disposing)
            isDisposed = True
        End If
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    Private isDisposed As Boolean
    ' Public WithEvents ZIconList As System.Windows.Forms.ImageList

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Public WithEvents GridView As GroupGrid
    'Friend WithEvents ButtonItem5 As TD.SandBar.ButtonItem
    <DebuggerStepThrough()> Private Sub InitializeComponent()

        components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCFusion2))
        GridView = New GroupGrid(True, CurrentUserId, Me, FilterTypes.Document)
        ' Me.ZIconList = New System.Windows.Forms.ImageList(Me.components)
        SuspendLayout()
        '
        'GridView
        '
        GridView.BackColor = System.Drawing.Color.White
        GridView.DataSource = Nothing
        GridView.Dock = System.Windows.Forms.DockStyle.Fill
        GridView.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        GridView.Location = New System.Drawing.Point(0, 0)
        GridView.Name = "GridView"
        GridView.Size = New System.Drawing.Size(912, 306)
        GridView.TabIndex = 0
        GridView.WithExcel = False
        'UCFusion2
        '
        BackColor = System.Drawing.Color.White
        Controls.Add(GridView)
        Name = "UCFusion2"
        Size = New System.Drawing.Size(912, 306)
        ResumeLayout(False)

    End Sub

#End Region

#Region "grilla"

    Public Event ResultDoubleClick(ByRef Result As Result)
    Public Event _RefreshGrid(ByVal ReloadHistory As Boolean)
    'Dim resultList As New SortedList()
    ''DataTable de la Grilla
    Private dt As DataTable = New DataTable("DataSource")
    Public useVersion As Boolean
    Public HaveVersions As Boolean = False
    Private IndexToFilter As New DataTable()
    Dim LastSearch As ISearch

    Dim currentPage As Integer

    ''Inicializa los valores de la grilla
    Public Sub inicializarGrilla()
        If Not isDisposed Then
            Try
                If IsNothing(GridView) Then
                    GridView = New GroupGrid(True, CurrentUserId, Me, FilterTypes.Document)
                End If
                GridView.AlwaysFit = True
                GridView.UseZamba = True
                GridView.WithExcel = True
                GridView.showRefreshButton = True
                GridView.ShortDateFormat = Boolean.Parse(UserPreferences.getValue("GridShortDateFormat", UPSections.UserPreferences, "True"))
                HaveVersions = False
                ClearSearchs()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
    End Sub


    ''Recarga el datatable en la grilla
    'si el datatable viene en nothing carga el original, sino, el ordenado por versiones
    ''' <history>
    ''' [Tomas] - 14/05/2009 - Modified - Se mueve de lugar la columna "DocTypeId"
    ''' [Sebastian] 18-08-09 MODIFIED - se agrega metodo para llena el combo de filtros de la grilla
    ''' [Sebastian] 12-11-2009 Modified Llamada de forma correcta al refresco de la grilla en los filtros.
    ''' </history>
    Public Sub refreshGrid(ByVal dtOrderedVersions As DataTable, ByRef ItsInsertedForm As Boolean, ByVal DeleteResultState As Boolean, Optional ByVal dtIds As Generic.List(Of Int64) = Nothing)
        Try
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Grilla de Documentos: Cargando la grilla")

                If Not dt.Columns.Contains(GridColumns.IMAGEN_COLUMNNAME) Then
                    dt.Columns.Add(GridColumns.IMAGEN_COLUMNNAME, GetType(Image))
                End If
                If Not dt.Columns.Contains(GridColumns.SITUACIONICON_COLUMNNAME) Then
                    dt.Columns.Add(GridColumns.SITUACIONICON_COLUMNNAME, GetType(Image))
                End If

                Dim AnyStateAsigned As Boolean
                Dim DiferentEntites As Boolean

                For Each dr As DataRow In dt.Rows

                    Dim iconID As Int32

                    If dt.Columns.Contains(GridColumns.ICON_ID_COLUMNNAME) Then
                        If Int32.TryParse(dr.Item(GridColumns.ICON_ID_COLUMNNAME).ToString(), iconID) Then
                            dr.Item(GridColumns.IMAGEN_COLUMNNAME) = IconsHelper.GetIcon(iconID, GridView.currentRowHeight - 5, GridView.currentRowHeight - 5)
                        End If
                    End If

                    '    Desasignada = 0
                    '   Asignada = 1
                    '  Ejecucion = 2
                    ' Servicio = 6
                    If dt.Columns.Contains(GridColumns.SITUACION_COLUMNNAME) AndAlso dt.Columns.Contains(GridColumns.SITUACIONICON_COLUMNNAME) Then
                        Dim Situacion As String = dr.Item(GridColumns.SITUACION_COLUMNNAME).ToString()
                        Select Case Situacion
                            Case "Desasignada"
                                                    'dr.Item(GridColumns.SITUACIONICON_COLUMNNAME) = IL.ZIconList.Images(49)
                            Case "Asignada"
                                dr.Item(GridColumns.SITUACIONICON_COLUMNNAME) = IconsHelper.GetIcon(50, GridView.currentRowHeight - 5, GridView.currentRowHeight - 5)
                            Case "Ejecucion"
                                dr.Item(GridColumns.SITUACIONICON_COLUMNNAME) = IconsHelper.GetIcon(52, GridView.currentRowHeight - 5, GridView.currentRowHeight - 5)
                            Case "Servicio"
                                IconsHelper.GetIcon(51, GridView.currentRowHeight - 5, GridView.currentRowHeight - 5)
                        End Select
                    End If

                    If AnyStateAsigned = False AndAlso dt.Columns.Contains(GridColumns.STATE_COLUMNNAME) Then
                        If dr.Item(GridColumns.STATE_COLUMNNAME).ToString().Length > 0 Then
                            AnyStateAsigned = True
                        End If
                    End If

                Next

                If dt.Columns.Contains(GridColumns.STATE_COLUMNNAME) AndAlso AnyStateAsigned = False Then
                    If GridColumns.ColumnsVisibility.ContainsKey(GridColumns.STATE_COLUMNNAME.ToLower()) Then
                        GridColumns.ColumnsVisibility(GridColumns.STATE_COLUMNNAME.ToLower()) = False
                    Else
                        GridColumns.ColumnsVisibility.Add(GridColumns.STATE_COLUMNNAME.ToLower(), False)
                    End If
                End If

                If dt.Columns.Contains(GridColumns.IMAGEN_COLUMNNAME) Then
                    dt.Columns.Item(GridColumns.IMAGEN_COLUMNNAME).SetOrdinal(0)
                End If
                If dt.Columns.Contains(GridColumns.SITUACIONICON_COLUMNNAME) Then
                    dt.Columns.Item(GridColumns.SITUACIONICON_COLUMNNAME).SetOrdinal(2)
                End If
                If dt.Columns.Contains(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME) Then
                    dt.Columns.Item(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME).SetOrdinal(1)
                End If
                If dt.Columns.Contains(GridColumns.DOC_TYPE_NAME_COLUMNNAME) Then
                    dt.Columns.Item(GridColumns.DOC_TYPE_NAME_COLUMNNAME).SetOrdinal(3)
                End If
                If dt.Columns.Contains(GridColumns.CHECKIN_COLUMNNAME) Then
                    dt.Columns.Item(GridColumns.CHECKIN_COLUMNNAME).SetOrdinal(dt.Columns.Count - 1)
                End If
                If dt.Columns.Contains(GridColumns.EXPIREDATE_COLUMNNAME) Then
                    dt.Columns.Item(GridColumns.EXPIREDATE_COLUMNNAME).SetOrdinal(dt.Columns.Count - 1)
                End If
                If dt.Columns.Contains(GridColumns.CRDATE_COLUMNNAME) Then
                    dt.Columns.Item(GridColumns.CRDATE_COLUMNNAME).SetOrdinal(dt.Columns.Count - 1)
                End If
                If dt.Columns.Contains(GridColumns.LASTUPDATE_COLUMNNAME) Then
                    dt.Columns.Item(GridColumns.LASTUPDATE_COLUMNNAME).SetOrdinal(dt.Columns.Count - 1)
                End If
                If dt.Columns.Contains(GridColumns.WORKFLOW_COLUMNAME) Then
                    dt.Columns.Item(GridColumns.WORKFLOW_COLUMNAME).SetOrdinal(dt.Columns.Count - 1)
                End If
                If dt.Columns.Contains(GridColumns.SITUACION_COLUMNNAME) Then
                    dt.Columns.Item(GridColumns.SITUACION_COLUMNNAME).SetOrdinal(dt.Columns.Count - 1)
                End If

                If dt.Columns.Contains(GridColumns.ORIGINAL_FILENAME_COLUMNNAME) Then
                    dt.Columns.Item(GridColumns.ORIGINAL_FILENAME_COLUMNNAME).SetOrdinal(dt.Columns.Count - 1)
                End If
                If dt.Columns.Contains(GridColumns.DOC_TYPE_ID_COLUMNNAME) Then
                    dt.Columns.Item(GridColumns.DOC_TYPE_ID_COLUMNNAME).SetOrdinal(dt.Columns.Count - 1)
                End If

                dt.AcceptChanges()

                Try
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        If useVersion AndAlso dt.Columns.Contains(GridColumns.NUMERO_DE_VERSION_COLUMNNAME) Then
                            dt.Columns.Item(GridColumns.NUMERO_DE_VERSION_COLUMNNAME).SetOrdinal(dt.Columns.Count - 1)
                            dt.Columns.Item(GridColumns.VER_PARENT_ID_COLUMNNAME).SetOrdinal(dt.Columns.Count - 1)
                            dt.Columns.Item(GridColumns.DOC_ID_COLUMNNAME).SetOrdinal(dt.Columns.Count - 1)
                            dt.Columns.Item("ver_Parent_Id").ColumnName = "ver_Parent_Id"
                            dt.AcceptChanges()

                            If HaveVersions = True AndAlso IsNothing(dtOrderedVersions) Then
                                OrderDocumentsByVersions()
                            ElseIf HaveVersions = True Then
                                GridView.DataSource = dtOrderedVersions
                            Else
                                GridView.DataSource = dt
                            End If
                        Else
                            GridView.DataSource = dt
                        End If
                    End If

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se recargaran los filtros.")

                    If dtIds Is Nothing OrElse dtIds.Count = 0 Then
                        GridView.ReloadLsvFilters()
                    Else
                        GridView.ReloadLsvFilters(dtIds)
                    End If

                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                If dt.Rows.Count > 0 Then

                    GridController.SetColumnsVisibility(GridView)

                    'If GridView.NewGrid.Rows.Count > 0 Then

                    '    'GridView.NewGrid.Rows(0).IsSelected = True

                    '    'Dim SelectedList As List(Of IResult) = SelectedResults(True, dt.Rows)
                    '    'If SelectedList IsNot Nothing Then
                    '    '    Dim selectedResult As Result = SelectedList(0)
                    '    'End If

                    'End If

                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Grilla de Documentos: Carga finalizada")
                '          GridView_Click(Nothing, Nothing)
            Else
                GridView.DataSource = Nothing
                GridView.SetRegisterText()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Public Sub FillResults(ByVal Results As DataTable, ByVal Search As ISearch)
        Try
            If Search IsNot Nothing Then LastSearch = Search

            If Results Is Nothing Then
                dt.Rows.Clear()
                dt.Columns.Clear()
            Else
                dt = Results.Clone()
                dt.Merge(Results)
            End If

            If Search Is Nothing Then
                refreshGrid(Nothing, False, False)
            Else
                Dim dtIds As New List(Of Long)
                For i As Int32 = 0 To Search.Doctypes.Count - 1
                    dtIds.Add(DirectCast(Search.Doctypes(i), IDocType).ID)
                Next
                refreshGrid(Nothing, False, False, dtIds)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''Castea el tipo de un Atributo a Type
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


    ''' <summary>
    ''' Sirve para explicarle al usuario el porque de una grilla vacía
    ''' </summary>
    ''' <param name="message">Mensaje para informar al usuario</param>
    ''' <remarks>Esta hecho como para no mostrar un MessageBox que resulte intrusivo</remarks>
    Public Sub AddMessageRow(ByVal message As String)
        If GridView.NewGrid.RowCount = 0 Then
            GridView.NewGrid.Columns.Add("message", "Mensaje")
            GridView.NewGrid.Rows.Add(New Object() {message})
            GridView.NewGrid.Columns(0).BestFit()
            'GridView.NewGrid.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        End If
    End Sub

    ''Agrega un result a la grilla
    ''' [Tomas] - 14/05/2009 - Modified - Se mueve de lugar la columna "DocTypeId"
    Public Sub AddResult(ByVal Result As Result, Optional ByVal exists As Boolean = False) 'Implements IUCFusion2.AddResult
        Try
            Dim ColumnType As Type = GetType(String)
            Dim ItsInsertedForm As Boolean = True
            If Not IndexToFilter.Columns.Contains("Index") Then IndexToFilter.Columns.Add("Index", ColumnType)
            If Not IndexToFilter.Columns.Contains("IndexName") Then IndexToFilter.Columns.Add("IndexName", ColumnType)

            If Not dt.Columns.Contains("imagen") AndAlso Not dt.Columns.Contains(GridColumns.IMAGEN_COLUMNNAME) Then
                dt.Columns.Add(GridColumns.IMAGEN_COLUMNNAME, GetType(Image))
                dt.Columns.Add("doc_id")

                dt.Columns.Add("disk_group_id")
                dt.Columns.Add("platter_id")
                dt.Columns.Add("vol_id")
                dt.Columns.Add("doc_file")
                dt.Columns.Add("offset")
                dt.Columns.Add("doc_type_id")
                dt.Columns.Add(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME)
                dt.Columns.Add("icon_id")
                dt.Columns.Add("shared")
                dt.Columns.Add("ver_parent_id")
                dt.Columns.Add("rootid")
                dt.Columns.Add(GridColumns.ORIGINAL_FILENAME_COLUMNNAME)
                dt.Columns.Add(GridColumns.VERSION_COLUMNNAME)
                dt.Columns.Add(GridColumns.NUMERO_DE_VERSION_COLUMNNAME)
                dt.Columns.Add("disk_vol_id")
                dt.Columns.Add("disk_vol_path")
                dt.Columns.Add(GridColumns.DOC_TYPE_NAME_COLUMNNAME)
                dt.Columns.Add(GridColumns.CRDATE_COLUMNNAME)
                dt.Columns.Add(GridColumns.LASTUPDATE_COLUMNNAME)
            End If

            'Por cada Atributo
            For Each i As Index In Result.Indexs
                'Si todavía no fue creada una columna con ese Atributo
                If Not dt.Columns.Contains(i.Name) Then
                    '[Sebastian] cargo los atributos que voy a usar para filtrar la grilla.

                    'Creo un tipo por default String
                    Dim iType As Type = GetType(String)

                    'Y si el tipo del indice no es nada, le cargo el type
                    If Not IsNothing(i.Type) Then
                        If i.DropDown = IndexAdditionalType.AutoSustitución _
                            Or i.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                            iType = GetType(String)
                            dt.Columns.Add("I" & i.ID, iType)
                        Else
                            iType = GetIndexType(i.Type)
                        End If
                    End If

                    'Y por último genero la columna con ese tipo
                    dt.Columns.Add(i.Name.Trim, iType)
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
                row.Item(GridColumns.NUMERO_DE_VERSION_COLUMNNAME) = Result.HasVersion
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                row.Item("RootId") = Result.RootDocumentId
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                row.Item(GridColumns.ORIGINAL_FILENAME_COLUMNNAME) = Result.OriginalName
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                row.Item(GridColumns.NUMERO_DE_VERSION_COLUMNNAME) = Result.VersionNumber
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

            row.Item(GridColumns.CRDATE_COLUMNNAME) = Result.CreateDate

            If dt.Columns.Contains("icon_id") Then
                row.Item("icon_id") = Result.IconId
            End If
            row.Item(GridColumns.IMAGEN_COLUMNNAME) = IconsHelper.GetIcon(Result.IconId, GridView.currentRowHeight - 5, GridView.currentRowHeight - 5)


            If Result.ISVIRTUAL Then
                row.Item(GridColumns.IMAGEN_COLUMNNAME) = IconsHelper.GetIcon(30, GridView.currentRowHeight - 5, GridView.currentRowHeight - 5)

            End If

            If Not Result.Name Is Nothing Then
                row.Item(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME) = Result.Name
            Else
                row.Item(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME) = Result.DocType.Name
            End If

            Dim t As Type = GetType(String)

            For Each indice As Index In Result.Indexs
                Try
                    'Si Data tiene un valor que se le asigne al Item
                    If String.Compare(String.Empty, indice.Data) <> 0 Then
                        If indice.DropDown <> IndexAdditionalType.AutoSustitución _
                            Or indice.DropDown <> IndexAdditionalType.AutoSustitución Then
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
                    row.Item(GridColumns.NUMERO_DE_VERSION_COLUMNNAME) = Result.VersionNumber
                    'Comento esta linea xq tira exception, hay que revisar su funcionalidad
                    'row.Item("IdDoc") = Result.ID
                End If
                row.Item(GridColumns.DOC_TYPE_NAME_COLUMNNAME) = Result.Parent.Name
                row.Item("Doc_id") = Result.ID
                row.Item("Doc_type_Id") = Result.DocTypeId
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            If exists = False Then
                dt.Rows.Add(row)
            End If
            dt.AcceptChanges()
            IndexToFilter.AcceptChanges()
            refreshGrid(Nothing, ItsInsertedForm, False)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub ClearSearchs()
        Try
            'Se hace dispose y new por un error existente que al hacer clear sobre las columnas le faltaba eliminar una
            If dt IsNot Nothing Then
                dt.Dispose()
                dt = New DataTable
            End If
            If GridView IsNot Nothing AndAlso GridView.NewGrid IsNot Nothing AndAlso GridView.NewGrid.Columns.Count > 0 Then
                GridView.NewGrid.Columns.Clear()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub SelectResult(ByVal Result As Result)
        Dim S As GridViewRowInfo
        Try
            GridView.Refresh()
            S = FindSubResultRoot(Result)
            If Not IsNothing(S) Then
                If GridView.NewGrid.SelectedRows(0) Is S Then
                    Exit Sub
                End If
                SelectAllToFalse()

                S.IsSelected = True
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            S = Nothing
        End Try
    End Sub
    Public Sub SelectResult(ByVal Result As Result, ByVal selectNewVersion As Boolean) 'Implements IUCFusion2.SelectResult
        'Die Sobrecarga creada para que se seleccione el primer result y ponga en negrita el result
        'quize poner un parametro opcional pero no me lo permitia por tener un delegado referenciando el metodo
        Dim S As GridViewRowInfo
        Try
            S = FindSubResultRoot(Result)
            If Not IsNothing(S) Then
                If GridView.NewGrid.SelectedRows(0) Is S Then
                    Exit Sub
                End If
                SelectAllToFalse()

                If selectNewVersion Then
                    Dim BoldFont As New Font(Font, FontStyle.Bold)
                    For index As Integer = 0 To S.Cells.Count - 1
                        S.Cells(index).Style.Font = BoldFont
                    Next
                End If

                S.IsSelected = True
                GridView.Refresh()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            S = Nothing
        End Try
    End Sub
    ''Deselecciona todas las filas de la grilla
    Private Sub SelectAllToFalse()
        Try
            For Each Item As GridViewRowInfo In GridView.NewGrid.Rows
                Item.IsSelected = False
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''Quita del dataset la fila que corresponde con el result

    ''Selecciona el ultimo valor de la grilla
    Public Function SelectFirstResultOfLastSearch() As Boolean 'Implements IUCFusion2.SelectFirstResultOfLastSearch
        Try
            If GridView.NewGrid.Rows.Count > 0 Then
                GridView.NewGrid.Rows(GridView.NewGrid.Rows.Count - 1).IsSelected = True
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
    Public Sub SelectAndShowResult(ByVal _result As Result)
        Try
            If GridView.NewGrid.Rows.Count > 0 Then

                GridView.Refresh()
                Dim S As GridViewRowInfo = FindSubResultRoot(_result)

                If S IsNot Nothing Then

                    SelectAllToFalse()
                    S.IsSelected = True

                End If

                Dim arrayResults As List(Of IResult) = SelectedResults(True, GetSelectedRows())

                If Not IsNothing(arrayResults) AndAlso (arrayResults.Count > 0) Then
                    arrayResults(0).CurrentFormID = _result.CurrentFormID
                    RaiseEvent ResultDoubleClick(arrayResults(0))
                End If

                S = Nothing

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    'Actualiza el result(por ahora borra y vuelve a agregarlo)
    Public Sub UpdateResult(ByVal Result As Result) 'Implements IUCFusion2.UpdateResult
        AddResult(Result, True)
        'refreshGrid(Nothing, False, False)
        SelectResult(Result)
    End Sub

    'Devuelve la fila seleccionada si la encuentra, sino nothing
    Private Function FindSubResultRoot(ByVal Result As Result) As GridViewRowInfo
        If Not IsNothing(GridView.NewGrid.Rows) AndAlso GridView.NewGrid.Rows.Count > 0 Then
            Try
                For Each row As GridViewRowInfo In GridView.NewGrid.Rows
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

    ''' <summary>
    ''' [Sebastian] 10-06-2009 Modified se agrego cast para salvar warnings
    ''' </summary>
    ''' <returns>Devuelve un array con los results seleccionados o nothing si no hay ninguno</returns>
    ''' <history> Marcelo modified 20/08/2009 </history>
    ''' <remarks></remarks>
    Public Function SelectedResultsList() As List(Of IResult)
        Try
            Dim Results As New List(Of IResult)
            For Each row As GridViewRowInfo In GetSelectedRows()
                If Not IsNothing(row.Cells("doc_id").Value) Then
                    Dim doctypeid As Int64 = CInt(row.Cells("doc_type_id").Value)
                    Dim r As Result = New Result(CInt(row.Cells("doc_id").Value), DocTypesBusiness.GetDocType(doctypeid, True), row.Cells(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME).Value.ToString(), 0)
                    Results_Business.CompleteDocument(r)
                    Results.Add(r)
                End If
            Next
            Return Results
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Public Function SelectedResults(ByVal SelectFirstResult As Boolean, ByVal selectedRows As ICollection) As List(Of IResult)
        Dim WFTB As New WFTaskBusiness
        Try
            Dim a As Int32 = 0
            Dim listResults As New List(Of IResult)

            If TypeOf selectedRows Is DataRowCollection Then

                For Each row As DataRow In selectedRows

                    If Not IsNothing(row.Item("doc_id")) Then

                        If Cache.DocTypesAndIndexs.hsDocTypes.Contains(CLng(row.Item("doc_type_id"))) = False Then

                            Dim _doctype As New DocType(CInt(row.Item("doc_type_id")),
                                                    row.Item(GridColumns.DOC_TYPE_NAME_COLUMNNAME).ToString(),
                                                    0)

                            _doctype.Indexs = ZCore.FilterIndex(CInt(_doctype.ID))

                            Cache.DocTypesAndIndexs.hsDocTypes.Add(CLng(row.Item("doc_type_id")), _doctype)

                        End If

                        Dim doctypeid As Int64 = CInt(row.Item("doc_type_id"))

                        If row.Table.Columns.Contains("task_id") AndAlso Not IsDBNull(row.Item("task_id")) Then

                            Dim r As TaskResult = WFTB.GetTask(CInt(row.Item("task_id")))
                            If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.Use, r.StepId) Then
                                Results_Business.CompleteDocument(r)
                                listResults.Add(r)
                            Else
                                Dim rd As Result = New Result(CInt(row("doc_id")),
                                                         DocTypesBusiness.GetDocType(doctypeid, True),
                                                         row(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME).ToString(),
                                                         0)
                                Results_Business.CompleteDocument(rd)
                                listResults.Add(rd)
                            End If

                        Else

                            If CInt(row.Item("doc_id")) = 0 Then

                                Dim r As NewResult = New NewResult()
                                r.DocTypeId = doctypeid
                                Results_Business.CompleteDocument(r)
                                listResults.Add(r)

                            Else

                                Dim r As Result = New Result(CInt(row.Item("doc_id")),
                                                         DocTypesBusiness.GetDocType(doctypeid, True),
                                                         row.Item(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME).ToString(),
                                                         0)
                                Results_Business.CompleteDocument(r)
                                listResults.Add(r)

                            End If

                        End If

                        If SelectFirstResult Then Exit For

                    End If

                Next

            Else

                For Each row As GridViewRowInfo In selectedRows

                    If Not IsNothing(row.Cells("doc_id").Value) Then

                        If Cache.DocTypesAndIndexs.hsDocTypes.Contains(CLng(row.Cells("doc_type_id").Value)) = False Then

                            Dim _doctype As New DocType(CInt(row.Cells("doc_type_id").Value),
                                                    row.Cells(GridColumns.DOC_TYPE_NAME_COLUMNNAME).Value.ToString(),
                                                    0)

                            _doctype.Indexs = ZCore.FilterIndex(CInt(_doctype.ID))

                            Cache.DocTypesAndIndexs.hsDocTypes.Add(CLng(row.Cells("doc_type_id").Value), _doctype)

                        End If

                        Dim doctypeid As Int64 = CInt(row.Cells("doc_type_id").Value)

                        If Not row.Cells("task_id") Is Nothing AndAlso Not IsDBNull(row.Cells("task_id").Value) Then

                            Dim r As TaskResult = WFTB.GetTask(CInt(row.Cells("task_id").Value))
                            If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.Use, r.StepId) Then
                                Results_Business.CompleteDocument(r)
                                listResults.Add(r)
                            Else
                                Dim rd As Result = New Result(CInt(row.Cells("doc_id").Value),
                                                         DocTypesBusiness.GetDocType(doctypeid, True),
                                                         row.Cells(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME).Value.ToString(),
                                                         0)
                                Results_Business.CompleteDocument(rd)
                                listResults.Add(rd)
                            End If

                        Else

                            If CInt(row.Cells("doc_id").Value) = 0 Then

                                Dim r As NewResult = New NewResult()
                                r.DocTypeId = doctypeid
                                Results_Business.CompleteDocument(r)
                                listResults.Add(r)

                            Else
                                Dim Name As String
                                If row.Cells(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME) Is Nothing Then

                                    Name = row.Cells("Nombre del Documento").Value.ToString()
                                Else
                                    Name = row.Cells(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME).Value.ToString()
                                End If

                                Dim r As Result = New Result(CInt(row.Cells("doc_id").Value),
                                                         DocTypesBusiness.GetDocType(doctypeid, True),
                                                         Name,
                                                         0)
                                Results_Business.CompleteDocument(r)
                                listResults.Add(r)

                            End If

                        End If

                        If SelectFirstResult Then Exit For

                    End If

                Next

            End If

            If listResults.Count > 0 Then Return listResults

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        Finally
            WFTB = Nothing
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

            If Core.Cache.DocTypesAndIndexs.hsDocTypes.Contains(docTypeId) = False Then
                Dim _doctype As DocType

                If resultAsDr.Table().Columns.Contains(GridColumns.DOC_TYPE_NAME_COLUMNNAME) Then
                    _doctype = New DocType(docTypeId, resultAsDr.Item(GridColumns.DOC_TYPE_NAME_COLUMNNAME).ToString(), 0)
                Else
                    _doctype = New DocType(docTypeId, resultAsDr.Item("Entidad").ToString(), 0)
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
                    r = New Result(docId, DocTypesBusiness.GetDocType(docTypeId, True), resultAsDr.Item("Nombre del Documento").ToString(), 0)
                Else
                    r = New Result(docId, DocTypesBusiness.GetDocType(docTypeId, True), resultAsDr.Item(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME).ToString(), 0)
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

    Public Sub DisableFilters()
        GridView.DisableAllFilters()

    End Sub

    'Dispara el evento doubleClick pasando como parametro el primer result selecionado
    Private Sub GridView_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If GridView.NewGrid.SelectedRows.Count > 0 Then
                Dim arrayResults As List(Of IResult) = SelectedResults(True, GetSelectedRows())
                If Not IsNothing(arrayResults) AndAlso (arrayResults.Count > 0) Then
                    RaiseEvent ResultDoubleClick(arrayResults(0))
                End If
            End If
            [Select]()
            GridView.FixColumns()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub refreshGridMethod()
        RaiseEvent _RefreshGrid(False)
    End Sub

    'Dispara el evento ResultSelected pasando como parametro el primer result selecionado
    Private Sub GridView_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If GridView.NewGrid.SelectedRows.Count > 0 Then
                Dim arrayResults As List(Of IResult) = SelectedResults(True, GetSelectedRows())
                If Not IsNothing(arrayResults) AndAlso arrayResults.Count > 0 Then
                    RaiseEvent ResultSelected(arrayResults(0))
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub



    'Dispara el evento ResultSelected pasando como parametro el primer result selecionado
    Public Sub SelectFirstResult()
        Try
            If GridView.NewGrid.Rows.Count > 0 Then
                GridView.NewGrid.Rows(0).IsSelected = True
                Dim arrayResults As List(Of IResult) = SelectedResults(True, GetSelectedRows())
                If Not IsNothing(arrayResults) AndAlso (arrayResults.Count > 0) Then
                    RaiseEvent ResultDoubleClick(arrayResults(0))
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    'Dispara el evento ResultSelected pasando como parametro el primer result selecionado
    Public Sub SelectDataRowResult(ByVal results As DataRowCollection)
        Try
            If GridView.NewGrid.Rows.Count > 0 Then

                GridView.NewGrid.Rows(0).IsSelected = True
                Dim selectedResult As Result = SelectedResults(True, results)(0)

                If Not IsNothing(selectedResult) Then
                    RaiseEvent ResultDoubleClick(selectedResult)
                End If

            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Carga las tareas del WF con este result seleccionado
    ''' </summary>
    ''' <param name="Result">El result que va a estar seleccionado</param>
    ''' <remarks></remarks>

    Private Sub UCFusion2_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ResizeRedraw = True
        useVersion = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.ModuleVersions, RightsType.Use)
        'Grilla
        inicializarGrilla()
    End Sub

#End Region

    Public Event CambiarNombre(ByRef Result As Result)
    Public Event ResultSelected(ByRef resul As Result)
    Public Event GenerarListado(ByVal Result As Result)
    Public Event ExportarAExcel(ByRef Result As Result)
    Public Event CloseResult(ByRef Result As Core.Result)
    Public Event ClearReferences(ByRef control As UCFusion2)


    ''' <summary>
    ''' Método que se ejecuta cuando se selecciona el elemento "Agregar a Workflow" del menú contextual
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]   07/05/2009  Modified     Se verifica la existencia de documentos con id 0, si es así entonces aparece un mensaje de error
    ''' </history>

    Private Sub _ExportarAExcel()
        Dim results As List(Of IResult) = SelectedResults(True, GetSelectedRows())
        If Not IsNothing(results) AndAlso results.Count > 0 Then
            RaiseEvent ExportarAExcel(results(0))
        End If
    End Sub


    Public Event ShowComment(ByVal result As Result)
    Private Sub ShowVersionComment()
        RaiseEvent ShowComment(Nothing)
    End Sub


    Private Sub _CambiarNombre()
        Dim results As List(Of IResult) = SelectedResults(True, GetSelectedRows())
        If Not IsNothing(results) AndAlso results.Count > 0 Then
            RaiseEvent CambiarNombre(results(0))
        End If
    End Sub


    Public Function GetSelectedRows() As List(Of GridViewRowInfo)
        Dim rowsArray = GridView.NewGrid.SelectedRows.ToList()
        Return rowsArray
    End Function




    Public Sub OrderDocumentsByVersions()
        Try
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
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub getChilds(ByVal dt1 As DataTable, ByVal parentid As Int64, ByVal dtCloned As DataTable, ByVal name As String)

        Dim drChilds As DataRow() = dtCloned.Select("ver_Parent_Id = " + parentid.ToString, GridColumns.NUMERO_DE_VERSION_COLUMNNAME)
        'Dim DV As New DataView
        'DV.Table = dtCloned
        'DV.RowFilter = 
        'DV.Sort = "Version"
        For Each drc As DataRow In drChilds

            'Dim Nombre As String = drc.Item(0).ToString
            'Nombre = Nombre.Insert(0, name & "--")
            'drc.Item(0) = Nombre.ToString

            dt1.Rows.Add(drc.ItemArray())
            parentid = Int64.Parse(drc.Item("doc_id").ToString)
            getChilds(dt1, parentid, dtCloned, name & "  ")

        Next
    End Sub


    ''' <summary>
    ''' Creo el result a partir de la fila de la grilla
    ''' </summary>
    ''' <param name="_Result"></param>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Sub CompleteDocument(ByRef _Result As Result, ByVal dr As GridViewRowInfo)
        Try
            If (dr.ViewTemplate.Columns.Contains(GridColumns.DISKGROUPIDCOLUMNNAME)) Then
                _Result.Disk_Group_Id = CInt(dr.Cells(GridColumns.DISKGROUPIDCOLUMNNAME).Value)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.PLATTER_ID_COLUMNNAME) Then
                _Result.Platter_Id = CInt(dr.Cells(GridColumns.PLATTER_ID_COLUMNNAME).Value)
            End If
        Catch ex As Exception
        End Try
        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.DOC_FILE_COLUMNNAME) Then
                _Result.Doc_File = dr.Cells(GridColumns.DOC_FILE_COLUMNNAME).Value.ToString()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.OFFSET_COLUMNNAME) Then
                _Result.OffSet = CInt(dr.Cells(GridColumns.OFFSET_COLUMNNAME).Value)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.SHARED_COLUMNNAME) Then
                If CInt(dr.Cells(GridColumns.SHARED_COLUMNNAME).Value) = 1 Then
                    _Result.isShared = True
                Else
                    _Result.isShared = False
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.VER_PARENT_ID_COLUMNNAME) Then
                _Result.ParentVerId = CInt(dr.Cells(GridColumns.VER_PARENT_ID_COLUMNNAME).Value)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.VERSION_COLUMNNAME) Then
                If Not IsNothing(dr.Cells(GridColumns.VERSION_COLUMNNAME)) AndAlso Not IsDBNull(dr.Cells(GridColumns.VERSION_COLUMNNAME).Value) Then
                    Int32.TryParse(dr.Cells(GridColumns.VERSION_COLUMNNAME).Value, _Result.HasVersion)
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.ROOTID_COLUMNNAME) Then
                _Result.RootDocumentId = CInt(dr.Cells("RootId").Value)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        If dr.ViewTemplate.Columns.Contains(GridColumns.ORIGINAL_FILENAME_COLUMNNAME) Then
            If Not IsDBNull(dr.Cells(GridColumns.ORIGINAL_FILENAME_COLUMNNAME).Value) Then
                Try
                    _Result.OriginalName = dr.Cells(GridColumns.ORIGINAL_FILENAME_COLUMNNAME).Value.ToString()
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        End If

        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.NUMERO_DE_VERSION_COLUMNNAME) Then
                _Result.VersionNumber = CInt(dr.Cells(GridColumns.NUMERO_DE_VERSION_COLUMNNAME).Value)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.DISKGROUPIDCOLUMNNAME) Then
                _Result.Disk_Group_Id = CInt(dr.Cells(GridColumns.DISKGROUPIDCOLUMNNAME).Value)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.DISK_VOL_PATH_COLUMNNAME) Then
                _Result.DISK_VOL_PATH = dr.Cells(GridColumns.DISK_VOL_PATH_COLUMNNAME).Value.ToString()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.UserID = _Result.Platter_Id
            _Result.OwnerID = _Result.Platter_Id
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        If dr.ViewTemplate.Columns.Contains(GridColumns.CRDATE_COLUMNNAME) Then
            If Not IsDBNull(dr.Cells(GridColumns.CRDATE_COLUMNNAME).Value) AndAlso Not IsNothing(dr.Cells(GridColumns.CRDATE_COLUMNNAME).Value) Then
                If Not String.IsNullOrEmpty(dr.Cells(GridColumns.CRDATE_COLUMNNAME).Value.ToString()) Then
                    Try
                        If Not IsNothing(_Result) Then
                            _Result.CreateDate = DateTime.Parse(dr.Cells(GridColumns.CRDATE_COLUMNNAME).Value.ToString())
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End If
            End If
        End If

        Try
            For Each indice As IIndex In _Result.DocType.Indexs
                If dr.ViewTemplate.Columns.Contains(indice.Name) Then
                    If indice.DropDown = IndexAdditionalType.AutoSustitución _
                       Or indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                        If Not IsDBNull(dr.Cells(indice.Name).Value) Then
                            indice.Data = dr.Cells("I" & indice.ID).Value.ToString()
                            indice.dataDescription = dr.Cells(indice.Name).Value.ToString()
                        End If
                    Else
                        indice.Data = dr.Cells(indice.Name).Value.ToString()
                    End If
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

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
            If dr.Table.Columns.Contains(GridColumns.DISKGROUPIDCOLUMNNAME) Then
                Int32.TryParse(dr.Item(GridColumns.DISKGROUPIDCOLUMNNAME).ToString, _Result.Disk_Group_Id)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.Table.Columns.Contains(GridColumns.PLATTER_ID_COLUMNNAME) Then
                Int32.TryParse(dr.Item(GridColumns.PLATTER_ID_COLUMNNAME).ToString, _Result.Platter_Id)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.Table.Columns.Contains(GridColumns.DOC_FILE_COLUMNNAME) Then
                _Result.Doc_File = dr.Item(GridColumns.DOC_FILE_COLUMNNAME).ToString
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.Table.Columns.Contains(GridColumns.OFFSET_COLUMNNAME) Then
                Int32.TryParse(dr.Item(GridColumns.OFFSET_COLUMNNAME).ToString, _Result.OffSet)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.Table.Columns.Contains(GridColumns.SHARED_COLUMNNAME) Then
                If CInt(dr.Item(GridColumns.SHARED_COLUMNNAME).ToString) = 1 Then
                    _Result.isShared = True
                Else
                    _Result.isShared = False
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.Table().Columns.Contains(GridColumns.VER_PARENT_ID_COLUMNNAME) Then
                Int32.TryParse(dr.Item(GridColumns.VER_PARENT_ID_COLUMNNAME).ToString, _Result.ParentVerId)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.Table().Columns.Contains(GridColumns.VERSION_COLUMNNAME) Then
                Int32.TryParse(dr.Item(GridColumns.VERSION_COLUMNNAME).ToString, _Result.HasVersion)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.Table().Columns.Contains(GridColumns.ROOTID_COLUMNNAME) Then
                Int32.TryParse(dr.Item(GridColumns.ROOTID_COLUMNNAME).ToString, _Result.RootDocumentId)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.Table.Columns.Contains(GridColumns.ORIGINAL_FILENAME_COLUMNNAME) Then
                _Result.OriginalName = dr.Item(GridColumns.ORIGINAL_FILENAME_COLUMNNAME).ToString
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.Table().Columns.Contains(GridColumns.NUMERO_DE_VERSION_COLUMNNAME) Then
                Int32.TryParse(dr.Item(GridColumns.NUMERO_DE_VERSION_COLUMNNAME).ToString, _Result.VersionNumber)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Try
            If dr.Table().Columns.Contains(GridColumns.DISKGROUPIDCOLUMNNAME) Then
                Int32.TryParse(dr.Item(GridColumns.DISKGROUPIDCOLUMNNAME).ToString, _Result.Disk_Group_Id)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.Table.Columns.Contains(GridColumns.DISKGROUPIDCOLUMNNAME) Then
                _Result.DISK_VOL_PATH = dr.Item(GridColumns.DISKGROUPIDCOLUMNNAME).ToString
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            _Result.UserID = _Result.Platter_Id
            _Result.OwnerID = _Result.Platter_Id
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        If Not IsDBNull(dr.Item(GridColumns.CRDATE_COLUMNNAME).ToString) AndAlso Not IsNothing(dr.Item(GridColumns.CRDATE_COLUMNNAME).ToString) Then
            If Not String.IsNullOrEmpty(dr.Item(GridColumns.CRDATE_COLUMNNAME).ToString) Then
                Try
                    If Not IsNothing(_Result) Then
                        _Result.CreateDate = DateTime.Parse(dr.Item(GridColumns.CRDATE_COLUMNNAME).ToString)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        End If

        For Each indice As IIndex In _Result.DocType.Indexs
            Try
                If dr.Table.Columns.Contains(indice.Name) Then
                    If indice.DropDown = IndexAdditionalType.AutoSustitución _
                        Or indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                        If Not IsDBNull(dr.Item(indice.Name).ToString) Then
                            indice.Data = dr.Item("I" & indice.ID).ToString
                            indice.dataDescription = dr.Item(indice.Name).ToString
                        End If
                    Else
                        If Not IsDBNull(dr.Item(indice.Name).ToString) Then
                            indice.Data = dr.Item(indice.Name).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        Next
        _Result.Indexs = _Result.DocType.Indexs
    End Sub

    Public Sub ResetGrid()
        GridView.ResetGrid()
    End Sub

    ''' <summary>
    ''' Creo el result a partir de la fila de la grilla
    ''' </summary>
    ''' <param name="_Result"></param>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Sub CompleteDocument(ByRef _Result As NewResult, ByVal dr As GridViewRowInfo)
        _Result.DocType = DocTypesBusiness.GetDocType(_Result.DocTypeId, True)
        _Result.Name = dr.Cells(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME).Value.ToString()

        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.DISKGROUPIDCOLUMNNAME) Then
                _Result.Disk_Group_Id = CInt(dr.Cells(GridColumns.DISKGROUPIDCOLUMNNAME).Value)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.PLATTER_ID_COLUMNNAME) Then
                _Result.Platter_Id = CInt(dr.Cells(GridColumns.PLATTER_ID_COLUMNNAME).Value)
            End If
        Catch ex As Exception
        End Try

        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.DOC_FILE_COLUMNNAME) Then
                _Result.Doc_File = dr.Cells(GridColumns.DOC_FILE_COLUMNNAME).Value.ToString()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.OFFSET_COLUMNNAME) Then
                _Result.OffSet = CInt(dr.Cells(GridColumns.OFFSET_COLUMNNAME).Value)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.SHARED_COLUMNNAME) Then
                If CInt(dr.Cells(GridColumns.SHARED_COLUMNNAME).Value) = 1 Then
                    _Result.isShared = True
                Else
                    _Result.isShared = False
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.VER_PARENT_ID_COLUMNNAME) Then
                _Result.ParentVerId = CInt(dr.Cells(GridColumns.VER_PARENT_ID_COLUMNNAME).Value)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.VERSION_COLUMNNAME) Then
                _Result.HasVersion = CInt(dr.Cells(GridColumns.VERSION_COLUMNNAME).Value)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.ROOTID_COLUMNNAME) Then
                _Result.RootDocumentId = CInt(dr.Cells(GridColumns.ROOTID_COLUMNNAME).Value)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.ORIGINAL_FILENAME_COLUMNNAME) Then
                _Result.OriginalName = dr.Cells(GridColumns.ORIGINAL_FILENAME_COLUMNNAME).Value.ToString()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.NUMERO_DE_VERSION_COLUMNNAME) Then
                _Result.VersionNumber = CInt(dr.Cells(GridColumns.NUMERO_DE_VERSION_COLUMNNAME).Value)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.DISKGROUPIDCOLUMNNAME) Then
                _Result.Disk_Group_Id = CInt(dr.Cells(GridColumns.DISKGROUPIDCOLUMNNAME).Value)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.ViewTemplate.Columns.Contains(GridColumns.DISK_VOL_PATH_COLUMNNAME) Then
                _Result.DISK_VOL_PATH = dr.Cells(GridColumns.DISK_VOL_PATH_COLUMNNAME).Value.ToString()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            _Result.UserID = _Result.Platter_Id
            _Result.OwnerID = _Result.Platter_Id
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        If Not IsDBNull(dr.Cells(GridColumns.CRDATE_COLUMNNAME).Value) AndAlso Not IsNothing(dr.Cells(GridColumns.CRDATE_COLUMNNAME).Value) Then
            If Not String.IsNullOrEmpty(dr.Cells(GridColumns.CRDATE_COLUMNNAME).Value.ToString()) Then
                Try
                    If Not IsNothing(_Result) Then
                        _Result.CreateDate = DateTime.Parse(dr.Cells(GridColumns.CRDATE_COLUMNNAME).Value.ToString())
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        End If

        Try
            For Each indice As IIndex In _Result.DocType.Indexs
                If dr.ViewTemplate.Columns.Contains(indice.Name) Then
                    If indice.DropDown = IndexAdditionalType.AutoSustitución _
                        Or indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                        If Not IsDBNull(dr.Cells("I" & indice.ID).Value) Then
                            indice.Data = dr.Cells("I" & indice.ID).Value.ToString()
                            indice.dataDescription = dr.Cells(indice.Name).Value.ToString()
                        End If
                    Else
                        indice.Data = dr.Cells(indice.Name).Value.ToString()
                    End If
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

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
        _Result.Name = dr.Item(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME).ToString()

        Try
            If dr.Table.Columns.Contains(GridColumns.DISKGROUPIDCOLUMNNAME) Then
                _Result.Disk_Group_Id = CInt(dr.Item(GridColumns.DISKGROUPIDCOLUMNNAME).ToString)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.Table.Columns.Contains(GridColumns.PLATTER_ID_COLUMNNAME) Then
                _Result.Platter_Id = CInt(dr.Item(GridColumns.PLATTER_ID_COLUMNNAME).ToString)
            End If
        Catch ex As Exception
        End Try
        Try
            If dr.Table.Columns.Contains(GridColumns.DOC_FILE_COLUMNNAME) Then
                _Result.Doc_File = dr.Item(GridColumns.DOC_FILE_COLUMNNAME).ToString()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.Table.Columns.Contains(GridColumns.OFFSET_COLUMNNAME) Then
                _Result.OffSet = CInt(dr.Item(GridColumns.OFFSET_COLUMNNAME).ToString)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.Table.Columns.Contains(GridColumns.SHARED_COLUMNNAME) Then
                If CInt(dr.Item(GridColumns.SHARED_COLUMNNAME).ToString) = 1 Then
                    _Result.isShared = True
                Else
                    _Result.isShared = False
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.Table.Columns.Contains(GridColumns.VER_PARENT_ID_COLUMNNAME) Then
                _Result.ParentVerId = CInt(dr.Item(GridColumns.VER_PARENT_ID_COLUMNNAME).ToString)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.Table.Columns.Contains(GridColumns.VERSION_COLUMNNAME) Then
                _Result.HasVersion = CInt(dr.Item(GridColumns.VERSION_COLUMNNAME).ToString)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.Table.Columns.Contains(GridColumns.ROOTID_COLUMNNAME) Then
                _Result.RootDocumentId = CInt(dr.Item(GridColumns.ROOTID_COLUMNNAME).ToString)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.Table.Columns.Contains(GridColumns.ORIGINAL_FILENAME_COLUMNNAME) Then
                _Result.OriginalName = dr.Item(GridColumns.ORIGINAL_FILENAME_COLUMNNAME).ToString
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If dr.Table().Columns.Contains(GridColumns.DOC_TYPE_NAME_COLUMNNAME) Then
                _Result.VersionNumber = CInt(dr.Item(GridColumns.NUMERO_DE_VERSION_COLUMNNAME).ToString)
            Else
                _Result.VersionNumber = CInt(dr.Item("Numero de Version").ToString)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            If dr.Table.Columns.Contains(GridColumns.DISKGROUPIDCOLUMNNAME) Then
                _Result.Disk_Group_Id = CInt(dr.Item(GridColumns.DISKGROUPIDCOLUMNNAME).ToString)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If dr.Table.Columns.Contains(GridColumns.DISKGROUPIDCOLUMNNAME) Then
                _Result.DISK_VOL_PATH = dr.Item(GridColumns.DISKGROUPIDCOLUMNNAME).ToString
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.UserID = _Result.Platter_Id
            _Result.OwnerID = _Result.Platter_Id
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        If Not IsDBNull(dr.Item(GridColumns.CRDATE_COLUMNNAME).ToString) AndAlso Not IsNothing(dr.Item(GridColumns.CRDATE_COLUMNNAME).ToString) Then
            If Not String.IsNullOrEmpty(dr.Item(GridColumns.CRDATE_COLUMNNAME).ToString) Then
                Try
                    If Not IsNothing(_Result) Then
                        _Result.CreateDate = DateTime.Parse(dr.Item(GridColumns.CRDATE_COLUMNNAME).ToString)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        End If

        Try
            For Each indice As IIndex In _Result.DocType.Indexs
                If dr.Table.Columns.Contains(indice.Name) Then
                    If indice.DropDown = IndexAdditionalType.AutoSustitución _
                        Or indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                        If Not IsDBNull(dr.Item("I" & indice.ID).ToString) Then
                            indice.Data = dr.Item("I" & indice.ID).ToString
                            indice.dataDescription = dr.Item(indice.Name).ToString
                        End If
                    Else
                        indice.Data = dr.Item(indice.Name).ToString
                    End If
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        _Result.Indexs = _Result.DocType.Indexs
    End Sub

    Private Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Private _lastPage As Integer

    Public Property LastPage() As Integer Implements IGrid.LastPage
        Get
            Return _lastPage
        End Get
        Set(ByVal value As Integer)
            _lastPage = value
        End Set
    End Property

    Private _pageSize As Integer
    Public Property PageSize As Integer Implements IGrid.PageSize
        Get
            Return _pageSize
        End Get
        Set(value As Integer)
            _pageSize = value
        End Set
    End Property

    Private _exporting As Boolean
    Public Property Exporting As Boolean Implements IGrid.Exporting
        Get
            Return _exporting
        End Get
        Set(value As Boolean)
            _exporting = value
        End Set
    End Property

    Private _exportSize As Int32
    Public Property ExportSize As Integer Implements IGrid.ExportSize
        Get
            Return _exportSize
        End Get
        Set(value As Integer)
            _exportSize = value
        End Set
    End Property

    Private _SaveSearch As Int32
    Private _filtersChanged As Boolean

    Public Property SaveSearch As Boolean Implements IGrid.SaveSearch
        Get
            Return _SaveSearch
        End Get
        Set(value As Boolean)
            _SaveSearch = value
        End Set
    End Property

    Public Property SortChanged As Boolean Implements IOrder.SortChanged

    Public Property FiltersChanged As Boolean Implements IFilter.FiltersChanged
        Get
            Return _filtersChanged
        End Get
        Set(value As Boolean)
            _filtersChanged = value
        End Set
    End Property

    Public Property FontSizeChanged As Boolean Implements IGrid.FontSizeChanged

    ''' <summary>
    ''' Carga la grilla y le aplica filtros
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowTaskOfDT() Implements IGrid.ShowTaskOfDT

        Dim dt As DataTable
        PageSize = Int32.Parse(UserPreferences.getValue("CantidadFilas", UPSections.UserPreferences, 100))

        If Not IsNothing(LastSearch) Then

            Dim FT As FilterTypes

            Select Case pmode
                Case Modes.Results
                    FT = FilterTypes.Document
                Case Modes.TasksResults
                    FT = FilterTypes.Task
                Case Modes.AsociatedResults, Modes.AsociatedTasksResults
                    FT = FilterTypes.Asoc
                Case Else
                    FT = FilterTypes.None
            End Select

            dt = Zamba.Core.Search.ModDocuments.DoSearch(LastSearch,
                                                         Membership.MembershipHelper.CurrentUser.ID,
                                                         FC,
                                                         LastPage,
                                                         If(Exporting, ExportSize, PageSize),
                                                         False,
                                                         False,
                                                         FT,
                                                         True,
                                                         Nothing,
                                                         False,
                                                         ,
                                                         SortChanged,
                                                         FiltersChanged
                                                         )

        Else

            dt = DocAsociatedBusiness.getAsociatedDTResultsFromResult(_result, LastPage, False, FC)

        End If

        FillResults(dt, LastSearch)

    End Sub

    Private Sub newGrid_ContextMenuOpening(sender As Object, e As ContextMenuOpeningEventArgs)
        Try
            If TypeOf e.ContextMenuProvider Is GridDataCellElement Then
                Dim celda As GridDataCellElement = CType(e.ContextMenuProvider, GridDataCellElement)
                If celda.RowInfo.Index >= 0 AndAlso celda.RowInfo.Index < celda.ViewInfo.Rows.Count Then
                    currentContextMenu.loadRights(SelectedResultsList(0))
                    e.ContextMenu = currentContextMenu.contextMenu.DropDown
                End If
            ElseIf TypeOf e.ContextMenuProvider Is GridHeaderCellElement Then
                For item As Int32 = 0 To e.ContextMenu.Items.Count - 1
                    If e.ContextMenu.Items(item).Text = "Formato condicional" OrElse e.ContextMenu.Items(item).Text = "Selector de columnas" Then
                        'hide the Conditional Formatting option from the header row context menu
                        e.ContextMenu.Items(item).Visibility = Telerik.WinControls.ElementVisibility.Collapsed
                        If TypeOf e.ContextMenu.Items(item + 1) Is RadMenuSeparatorItem Then
                            'hide the separator below the CF option
                            e.ContextMenu.Items(item + 1).Visibility = Telerik.WinControls.ElementVisibility.Collapsed
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub AddOrderComponent(orderString As String) Implements IOrder.AddOrderComponent
        If LastSearch IsNot Nothing Then
            LastSearch.SetOrderBy(orderString)
        End If
    End Sub
    Public Sub AddGroupByComponent(GroupByString As String) Implements IGrid.AddGroupByComponent
        If LastSearch IsNot Nothing Then
            LastSearch.SetGroupBy(GroupByString)
        End If
    End Sub
    Private Function IsIndex(Column As String) As Boolean
        If IndexsBusiness.GetIndexIdByName(Column) > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Function GetSelectedResults() As List(Of IResult) Implements IMenuContextContainer.GetSelectedResults
        Return SelectedResultsList()
    End Function

    Public Sub RefreshResults() Implements IMenuContextContainer.RefreshResults
        refreshGridMethod()
    End Sub

    Public Sub SetFontSizeUp()
        GridView.SetFontSizeUp()
    End Sub

    Public Sub SetFontSizeDown()
        GridView.SetFontSizeDown()
    End Sub

    Private Sub currentContextMenuClick(Action As String, listResults As List(Of IResult), ContextMenuContainer As IMenuContextContainer) Implements IMenuContextContainer.currentContextMenuClick
        MenuContextContainer.currentContextMenuClick(Action, listResults, ContextMenuContainer)
    End Sub
End Class
