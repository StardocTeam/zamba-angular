Imports Zamba.Core.WF.WF
Imports Zamba.Filters
Imports Zamba.Core
Imports Zamba.Data
Imports Telerik.WinControls.UI

Namespace WF.TasksCtls
    ''' <summary>
    ''' Esta clase es la que maneja la grilla de tareas
    ''' </summary>
    ''' <history> Marcelo modified 18/09/2008 </history>
    ''' <history> Ezequiel modified 22/1/2009 - Se agrego un menu contextual y sus funciones respectivas </history>
    ''' <remarks></remarks>
    Public Class UCTaskGrid
        Inherits ZControl
        Implements IGrid
        Implements IDisposable
        Implements IMenuContextContainer

        'UserControl reemplaza a Dispose para limpiar la lista de componentes.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing Then
                    If components IsNot Nothing Then components.Dispose()

                    RemoveHandler WFTaskBusiness.ShowTask, AddressOf ShowTask

                    If dt IsNot Nothing Then
                        dt.Dispose()
                        dt = Nothing
                    End If

                    If _fc IsNot Nothing Then _fc = Nothing


                    'TODO: ver porque no estaba haciendo el dispose de la grilla.
                    If GridView IsNot Nothing Then
                        ReleaseGridViewHandlers()
                        GridView.Dispose()
                        GridView = Nothing
                    End If
                End If

                MyBase.Dispose(disposing)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
            End Try

        End Sub

        Public Sub ReleaseGridViewHandlers()
            If GridView IsNot Nothing Then
                If GridView.cmbDocType IsNot Nothing AndAlso GridView.cmbDocType.ComboBox IsNot Nothing Then
                    RemoveHandler GridView.cmbDocType.ComboBox.SelectedIndexChanged, Sub(sender, e) ShowTaskOfDT()
                End If
                RemoveHandler GridView.OnRowClick, AddressOf GridView_Click
                RemoveHandler GridView.OnDoubleClick, AddressOf GridView_DoubleClick
                RemoveHandler GridView.SelectAllClick, AddressOf SelectAllTasks
                RemoveHandler GridView.DeselectAllClick, AddressOf clearRulesOfToolBar
                RemoveHandler GridView.UpdateDs, AddressOf updateDataGridView
            End If
        End Sub

#Region " Código generado por el Diseñador de Windows Forms "

        'Requerido por el Diseñador de Windows Forms

        'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
        'Puede modificarse utilizando el Diseñador de Windows Forms. 
        'No lo modifique con el editor de código.
        Private components As System.ComponentModel.IContainer

        Friend WithEvents GridView As Grid.Grid.GroupGrid

        Public useVersion As Boolean
        Dim resultList As New SortedList()
        <DebuggerStepThrough()> Private Sub InitializeComponent()
            components = New ComponentModel.Container
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(UCTaskGrid))

            BackColor = System.Drawing.Color.White
            Size = New System.Drawing.Size(752, 424)
        End Sub

#End Region

#Region "Constructor"

        Dim TasksSearch As ISearch
        Dim CurrentUserId As Int64
        Public ReadOnly Property controller As Controller
        Public currentContextMenu As UCGridContextMenu

        '  Public WithEvents UCContextResult As RadContextMenu



        Public Sub New(ByVal CurrentUserId As Int64, controller As Controller)

            MyBase.New()

            'Se asigna primero el CurrentUserId ya que en InitializeComponent 
            'se lo pasa por parametro a GridView
            Me.CurrentUserId = CurrentUserId
            Me.controller = controller
            InitializeComponent()

            GridView = New Grid.Grid.GroupGrid(True, CurrentUserId, Me, FilterTypes.Task)
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
            GridView.WithExcel = True
            GridView.UseColor = True

            RemoveHandler GridView.OnRowClick, AddressOf GridView_Click
            RemoveHandler GridView.OnDoubleClick, AddressOf GridView_DoubleClick
            RemoveHandler GridView.SelectAllClick, AddressOf SelectAllTasks
            RemoveHandler GridView.DeselectAllClick, AddressOf clearRulesOfToolBar
            RemoveHandler GridView.UpdateDs, AddressOf updateDataGridView
            AddHandler GridView.OnRowClick, AddressOf GridView_Click
            AddHandler GridView.OnDoubleClick, AddressOf GridView_DoubleClick
            AddHandler GridView.SelectAllClick, AddressOf SelectAllTasks
            AddHandler GridView.DeselectAllClick, AddressOf clearRulesOfToolBar
            AddHandler GridView.UpdateDs, AddressOf updateDataGridView

            RemoveHandler WFTaskBusiness.ShowTask, AddressOf ShowTask
            AddHandler WFTaskBusiness.ShowTask, AddressOf ShowTask

            GridView.cmbDocType.Visible = True
            GridView.ShortDateFormat = Boolean.Parse(UserPreferences.getValue("GridShortDateFormat", UPSections.UserPreferences, "True"))
            currentContextMenu = New UCGridContextMenu(Me)

            Controls.Add(GridView)

            AddHandler GridView.NewGrid.ContextMenuOpening, AddressOf newGrid_ContextMenuOpening


        End Sub

#End Region

#Region "Atributos"
        Private daysAndColors As Hashtable = New Hashtable
        Private dt As DataTable = New DataTable("DataSource")
        Public objectId As Int64
        Public stepid As Int64
        Private stateid As Int64
        Private _reloadFilters As Boolean

        Public Enum GridTypes
            All
            WorkFlow
            WFStep
        End Enum

        Public GridType As GridTypes

#End Region

#Region "Eventos"

        Public Event GridTasksSelected(ByRef Tasks As List(Of GridTaskResult))

        ' Si se clickeo sobre la grilla pero no hay ningun result chequeado dispara un evento para limpiar la toolbar de tareas
        Public Event NotAnyResultSelected()
        ' Si realizo un click en una (o mas tareas) se verifica si la tiene asignada el usuario actual u otro usuario 
        ' asignado. Luego, después de verificar los permisos que tenga el usuario actual para ejecutar tareas de otros 
        ' usuarios, se dispara el evento que muestra o oculta las reglas según el permiso
        Public Event visibleOrInvisibleButtonsRule(ByRef state As Boolean)

        ''' <summary>
        ''' Selecciona las tareas en la grilla
        ''' </summary>
        ''' <param name="useCheck">True si seleccionamos por el check</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>Marcelo modified 18/09/2008</history>
        Public Sub SelectTaskResults(ByVal useCheck As Boolean, ByVal tasks As Generic.List(Of GridTaskResult))
            Try
                'Por cada tarea obtengo la row y la selecciono
                For Each task As GridTaskResult In tasks
                    Dim row As GridViewRowInfo = FindSubResultRoot(task.TaskId)
                    If Not IsNothing(row) Then
                        If useCheck = True Then
                            If Not (IsNothing(row.Cells(GridColumns.VER_COLUMNNAME).Value)) Then
                                row.Cells(GridColumns.VER_COLUMNNAME).Value = True
                            End If
                        Else
                            row.IsSelected = True
                        End If
                    End If
                    row = Nothing
                Next
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Public Sub ShowTask(ByVal result As Core.ITaskResult, ByVal OpenTaskAfterInsert As Boolean)
            RaiseEvent TaskDoubleClick(result)
        End Sub

        Dim CurrentTaskId As Int64

        ''' <summary>
        ''' Dispara el evento ResultSelected pasando como parametro el primer result selecionado
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    24/07/2008  Modified
        '''     [Ezequiel]    22/01/2009  Modified
        '''     [Sebastian] 29-10-2009
        ''' </history>
        Private Sub GridView_Click(ByVal sender As Object, ByVal e As EventArgs)
            If GridView.NewGrid.SelectedRows.Count > 0 Then
                Try
                    SelectAllTasks()
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                Finally
                    GridView.NewGrid.Enabled = True
                End Try
            End If
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

        Public Event ShowComment(ByVal result As Result)

        Private Sub ShowVersionComment()
            Dim arrayResults As Generic.List(Of GridTaskResult) = SelectedTaskResults(False)
            Dim results() As Result = {Results_Business.GetResult(arrayResults(0).ID, arrayResults(0).doctypeid)}
            If Not IsNothing(results) Then
                RaiseEvent ShowComment(results(0))
            End If

        End Sub
        Public Function SelectedResultsListExtern() As Generic.List(Of IResult)
            SyncLock _sync
                Return SelectedResultsList()
            End SyncLock
        End Function

        Public Function SelectedResultsList() As Generic.List(Of IResult) Implements IMenuContextContainer.GetSelectedResults
            Dim arrayResults As List(Of GridTaskResult) = SelectedTaskResults(False)
            Dim Results As New List(Of IResult)
            Dim currentResult As Result
            For Each GR As GridTaskResult In arrayResults
                currentResult = Results_Business.GetResult(GR.ID, GR.doctypeid)
                Results_Business.CompleteDocument(currentResult)
                Results.Add(currentResult)
            Next
            Return Results
        End Function

        Public Function SelectedTaskResultsExtern(ByVal useCheck As Boolean) As Generic.List(Of GridTaskResult)
            SyncLock _sync
                Return SelectedTaskResults(useCheck)
            End SyncLock
        End Function

        ' Devuelve un array con los results seleccionados o nothing si no hay ninguno
        ''' <summary>
        ''' [Sebastian 08-09-09]
        ''' Se agrego try parse para el booleano de "ver" porque luego de agrupar y hacer clic en l agrilla producia un error.
        ''' </summary>
        ''' <param name="useCheck"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SelectedTaskResults(ByVal useCheck As Boolean) As Generic.List(Of GridTaskResult)

            Try

                Dim a As Int32 = 0
                Dim BooleanValue As Boolean
                Dim arrayResults As New Generic.List(Of GridTaskResult)

                If (useCheck) Then

                    For Each row As GridViewRowInfo In GridView.NewGrid.Rows()

                        If Not IsNothing(row.Cells(GridColumns.VER_COLUMNNAME)) AndAlso Not (IsNothing(row.Cells(GridColumns.VER_COLUMNNAME).Value)) Then
                            '[Sebastian 08-09-09] Se agrego try parse
                            If (Boolean.TryParse(row.Cells(GridColumns.VER_COLUMNNAME).Value.ToString(), BooleanValue) = True) AndAlso BooleanValue = True Then

                                If Not (IsNothing(row.Cells(GridColumns.TASK_ID_COLUMNNAME).Value)) Then

                                    If (String.Compare(row.Cells(GridColumns.TASK_ID_COLUMNNAME).Value.ToString(), String.Empty) <> 0) Then

                                        If String.IsNullOrEmpty(row.Cells(GridColumns.SITUACION_COLUMNNAME).Value.ToString) Then

                                            arrayResults.Add(New GridTaskResult(CLng(row.Cells(GridColumns.WFSTEPID_COLUMNNAME).Value), CLng(row.Cells(GridColumns.TASK_ID_COLUMNNAME).Value), CLng(row.Cells(GridColumns.DOCID_COLUMNNAME).Value), CLng(row.Cells(GridColumns.DOCTYPEID_COLUMNNAME).Value), row.Cells(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME).Value.ToString, TaskStates.Desasignada, CLng(row.Cells(GridColumns.USER_ASIGNED_COLUMNNAME).Value), CLng(row.Cells(GridColumns.DO_STATE_ID_COLUMNNAME).Value)))

                                        Else

                                            arrayResults.Add(New GridTaskResult(CLng(row.Cells(GridColumns.WFSTEPID_COLUMNNAME).Value), CLng(row.Cells(GridColumns.TASK_ID_COLUMNNAME).Value), CLng(row.Cells(GridColumns.DOCID_COLUMNNAME).Value), CLng(row.Cells(GridColumns.DOCTYPEID_COLUMNNAME).Value), row.Cells(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME).Value.ToString, DirectCast(System.Enum.Parse(GetType(TaskStates), row.Cells(GridColumns.SITUACION_COLUMNNAME).Value.ToString), TaskStates), CLng(row.Cells(GridColumns.USER_ASIGNED_COLUMNNAME).Value), CLng(row.Cells(GridColumns.DO_STATE_ID_COLUMNNAME).Value)))

                                        End If

                                    End If

                                    a += 1

                                End If

                            End If

                        End If

                    Next

                Else

                    For Each row As GridViewRowInfo In GridView.NewGrid.SelectedRows()
                        If Not IsNothing(row.Cells(GridColumns.TASK_ID_COLUMNNAME)) AndAlso Not IsNothing(row.Cells(GridColumns.TASK_ID_COLUMNNAME).Value) Then
                            If (String.Compare(row.Cells(GridColumns.TASK_ID_COLUMNNAME).Value.ToString(), String.Empty) <> 0) Then

                                Dim TaskState As TaskStates = TaskStates.Desasignada
                                If System.Enum.IsDefined(GetType(TaskStates), row.Cells(GridColumns.SITUACION_COLUMNNAME).Value.ToString) Then
                                    TaskState = DirectCast(System.Enum.Parse(GetType(TaskStates), row.Cells(GridColumns.SITUACION_COLUMNNAME).Value.ToString), TaskStates)
                                End If

                                If row.Cells(GridColumns.WFSTEPID_COLUMNNAME) IsNot Nothing Then

                                    arrayResults.Add(New GridTaskResult(CLng(row.Cells(GridColumns.WFSTEPID_COLUMNNAME).Value),
                                                                        CLng(row.Cells(GridColumns.TASK_ID_COLUMNNAME).Value),
                                                                        CLng(row.Cells("DocId").Value),
                                                                        CLng(row.Cells("DoctypeId").Value),
                                                                        row.Cells(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME).Value.ToString,
                                                                        TaskState,
                                                                        CLng(row.Cells("user_asigned").Value),
                                                                        CLng(row.Cells("task_state_id").Value)))
                                End If

                            End If
                        End If
                    Next
                End If
                Return arrayResults

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Return Nothing
        End Function


        ''' <summary>
        ''' Método encargado de mostrar los historiales correspondientes a las tareas seleccionadas y mostrar las reglas
        ''' necesarias si el usuario tiene permisos
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    12/08/2008  Modified     El código original estaba en el evento GridView_Click
        '''                 Se coloco en un método, así, cuando se llame al botón "Seleccionar Todos" de GroupGrid y
        '''                 después de haber seleccionado todas las tareas se llama a este método. No se llamo directo
        '''                 a GridView_Click porque el evento contiene una validacion que no es necesaria al ejecutar
        '''                 el botón "Seleccionar Todos"
        '''     [Javier]    06/12/2010  Modified    GetTaskByTaskIdAndDocTypeIdAndStepId is now a instance method
        ''' </history>
        Private Sub SelectAllTasks(Optional ByVal sender As Object = Nothing, Optional ByVal e As EventArgs = Nothing)
            SyncLock _sync
                Dim arrayResults As Generic.List(Of GridTaskResult)

                Try
                    arrayResults = SelectedTaskResults(True)
                    'Diego. Comento esta para que aunque sea nothing mande el evento
                    'Esto me sirve para crear la toolbar de la tarea o no mostrarla en caso de no este seleccionada ninguna tarea
                    If arrayResults IsNot Nothing AndAlso arrayResults.Count > 0 Then
                        If arrayResults.Count = 1 Then
                            ' Para mostrar historial de la tarea seleccionada en la solapa Listado de aprobaciones
                            Dim taskbs As New WFTaskBusiness()
                            Dim CurrentTask As ITaskResult = taskbs.GetTaskByTaskIdAndDocTypeIdAndStepId(arrayResults(0).TaskId, arrayResults(0).doctypeid, arrayResults(0).StepId, 0)
                            taskbs = Nothing

                            If CurrentTask IsNot Nothing Then
                                CurrentTaskId = CurrentTask.TaskId
                                RaiseEvent GridTasksSelected(arrayResults)
                            End If
                        Else
                            CurrentTaskId = 0
                            RaiseEvent GridTasksSelected(arrayResults)
                        End If
                    Else
                        'se se clickeo sobre la grilla pero no hay ningun result chequeado dispara un evento para limpiar la toolbar de tareas
                        RaiseEvent NotAnyResultSelected()
                        ' Ya no hay más tareas seleccionadas
                        Controller.selectedTaskIds.Clear()
                    End If

                    [Select]()
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                Finally
                    arrayResults = Nothing
                End Try
            End SyncLock
        End Sub

        ''' <summary>
        ''' Método que ejecuta un evento encargado de limpiar las reglas de la toolbar
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    05/09/2008  Created     
        ''' </history>
        Private Sub clearRulesOfToolBar(Optional ByVal sender As Object = Nothing, Optional ByVal e As EventArgs = Nothing)
            RaiseEvent NotAnyResultSelected()
            Controller.selectedTaskIds.Clear()
        End Sub

        ''' <summary>
        ''' Método que sirve para actualizar el DataSource del GridView sólo cuando se haya distribuido una o más tareas. Este método es llamado por
        ''' el evento ColumnHeaderMouseClick (del outlookgrid1) que es cuando se quiere ordenar por una columna, por el filtrar, por el actualizar y 
        ''' por el inicial. Es necesario para para obtener el datatable actualizado (que no contiene las tareas que se distribuyeron). 
        ''' Se hace preciso actualizar el DataSource porque sino al ordenar por columna o al filtrar se mostrarian las tareas que se distribuyeron 
        ''' (ya que el DataSource seguiría conteniendo el datatable desactualizado)
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    11/11/2008  Created     
        ''' </history>
        Private Sub updateDataGridView()
            SyncLock _sync
                If (Not IsNothing(GridView.Tag) AndAlso Not String.IsNullOrEmpty(GridView.Tag.ToString())) Then
                    If (dt.Rows.Count > 0) Then
                        If Not IsNothing(dt.Columns(GridColumns.WFSTEPID_COLUMNNAME)) Then
                            colorDisplayForOverdueTasks(dt, Convert.ToInt64(dt.Rows(0)(GridColumns.WFSTEPID_COLUMNNAME)))
                        End If
                    End If
                    GridView.DataSource = dt
                    GridView.Tag = Nothing
                End If
            End SyncLock
        End Sub

        Public Event TaskDoubleClick(ByVal Task As ITaskResult)

        ''' <summary>
        ''' [Sebastian] 17-07-2009 MODIFIED permisos para ejecutar tareas de otros usuarios
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <history>
        '''     Javier  06/12/2010  Modified    GetTaskByTaskIdAndDocTypeIdAndStepId is now a instance method
        ''' </history>
        Private Sub GridView_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
            SyncLock _sync
                Try
                    If GridView.NewGrid.SelectedRows.Count > 0 Then
                        Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.WaitCursor})
                        GridView.NewGrid.Enabled = False

                        Dim arrayResults As Generic.List(Of GridTaskResult) = SelectedTaskResults(False)

                        If arrayResults IsNot Nothing Then
                            If arrayResults.Count > 0 Then
                                Dim AllowExecuteAllUserTask As Boolean = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.VerAsignadosAOtros, CInt(arrayResults(0).StepId))
                                Dim taskbs As New WFTaskBusiness()
                                Dim openTask As Boolean = False

                                'si la tarea esta: asignada a mi usuario o desasignada o mi usuario tiene permisos para ejecutar tareas de otros usuarios
                                'puede abrir la tarea
                                If arrayResults(0).AsignedToId = 0 OrElse
                                    arrayResults(0).AsignedToId = Membership.MembershipHelper.CurrentUser.ID OrElse
                                    AllowExecuteAllUserTask Then
                                    openTask = True
                                Else
                                    'si pertenece al grupo o tengo permiso para ejecutar tareas de otros usuarios
                                    Dim users As Generic.List(Of Long) = UserGroupBusiness.GetUsersIds(arrayResults(0).AsignedToId, True)
                                    If users.Contains(Membership.MembershipHelper.CurrentUser.ID) Or AllowExecuteAllUserTask Then
                                        openTask = True
                                    Else
                                        'todo: hacer que taskviewer sea readonly y qeu deja abrirla avisando
                                        MessageBox.Show("La tarea se encuentra tomada por otro usuario o usted no pertence al grupo", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    End If
                                    users.Clear()
                                    users = Nothing
                                End If

                                If openTask Then
                                    Dim GTR As GridTaskResult = arrayResults(0)
                                    Dim Tasks As IResult = taskbs.GetTaskByTaskIdAndDocTypeIdAndStepId(GTR.TaskId, GTR.doctypeid, GTR.StepId, 0)
                                    If RightsBusiness.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.FlagAsRead, GTR.doctypeid) = True Then
                                        GridView.UnFlagUnreadDocuments()
                                    End If
                                    GTR = Nothing
                                    RaiseEvent TaskDoubleClick(Tasks)
                                End If

                                taskbs = Nothing
                            End If

                            arrayResults.Clear()
                            arrayResults = Nothing
                        End If
                    End If
                    [Select]()
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                Finally
                    GridView.NewGrid.Enabled = True
                    Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.Default})
                End Try
            End SyncLock
        End Sub
#End Region

#Region "Métodos públicos"
        Public Sub OutLookGridClear()
            SyncLock _sync
                GridView.NewGrid.Rows.Clear()
                GridView.NewGrid.Columns.Clear()
            End SyncLock
        End Sub

        Dim CurrentStepId As Int64
        Shared _sync As New Object

        Private Delegate Sub ShowTasksDelegate(ByVal wfstepId As Int64, ByVal reloadFilters As Boolean, ByVal wfStateId As Int64)

        Public Sub ShowTasksExtern(ByVal wfstepId As Int64, ByVal reloadFilters As Boolean, ByVal wfStateId As Int64, ByVal WFchanged As Boolean)
            Try

                If WFchanged Then
                End If
                _orderString = String.Empty
                GridView.RemoveOrder()
                _GroupByString = String.Empty
                GridView.GroupByCountList = Nothing

                SyncLock _sync
                    Dim param() As Object = {wfstepId, reloadFilters, wfStateId}
                    BeginInvoke(New ShowTasksDelegate(AddressOf ShowTasks), param)
                End SyncLock

            Catch ex As Exception
                ZClass.raiseerror(ex)

            End Try
        End Sub


        Private Delegate Sub ChangeCursorDelegate(ByVal cur As Cursor)

        Private Sub ChangeCursor(ByVal cur As Cursor)
            Try
                Cursor = cur
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Dim blncancelrefresh As Boolean

        ''' <summary>
        ''' Muestra las tareas de la etapa
        ''' </summary>
        ''' <param name="wfstepId">Id de la etapa</param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    25/07/2008 Modified 
        '''     [Gaston]    27/10/2008 Modified     Se verifica si la etapa puede mostrar tareas vencidas de distinto color. Hide y Show de GridView
        '''     [Gaston]    11/11/2008 Modified     La propiedad Tag del GridView se coloca en Nothing, de esta forma si se ordena por columna, se 
        '''                                         filtra, etc, no se vuelve a actualizar el datasource (si se había distribuido tareas en otra etapa)
        '''     [Marcelo]   25/03/2009 Modified     Se agrego un parametro que dice si se actualizan o no los filtros
        '''     [Ezequiel]  25/09/09   Modified     Se modifico para que la grilla cargue tareas por entidad.
        '''     [Tomas]     06/05/11    Modified    Se corrige la carga del item "Todas las Tareas"
        ''' </history>
        Public Sub ShowTasks(ByVal wfstepId As Int64, ByVal reloadFilters As Boolean, ByVal wfStateId As Int64)

            Dim dt As DataTable
            Dim dv As DataView
            Dim dv2 As DataView
            Dim doccount As Int32

            Try
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.WaitCursor})
                LastPage = 0
                stepid = wfstepId
                stateid = wfStateId
                GridType = GridTypes.WFStep
                GridView.UseZamba = True
                _reloadFilters = reloadFilters
                '[Ezequiel] Obtengo los tipos de documento de que pertenecen a la etapa.
                dt = WFStepBusiness.GetDocTypesByWfStepAsDT(wfstepId, True)

                'Se obtiene la cantidad de documentos reales
                dv = New DataView(dt)
                dv.RowFilter = GridColumns.DOC_TYPE_ID_COLUMNNAME & " <> 0"
                doccount = dv.ToTable.Rows.Count

                '[Ezequiel] Cargo el combobox de la grilla con los datos de los tipos de documento.
                GridView.cmbDocType.ComboBox.DisplayMember = GridColumns.DOC_TYPE_NAME_COLUMNNAME
                GridView.cmbDocType.ComboBox.ValueMember = GridColumns.DOC_TYPE_ID_COLUMNNAME

                RemoveHandler GridView.cmbDocType.ComboBox.SelectedIndexChanged, Sub(sender, e) ShowTaskOfDT()

                GridView.cmbDocType.BeginUpdate()
                GridView.cmbDocType.Enabled = False
                'blncancelrefresh = True

                dv2 = New DataView(dt)
                dv2.Sort = GridColumns.DOC_TYPE_ID_COLUMNNAME & " ASC"
                dt = dv2.ToTable()

                GridView.cmbDocType.ComboBox.DataSource = dt
                GridView.cmbDocType.EndUpdate()

                'blncancelrefresh = False
                If doccount > 0 Then
                    '[Ezequiel] Si solo hay un entidad griso el combobox.
                    If doccount = 1 Then
                        GridView.cmbDocType.Enabled = False
                    Else
                        'Busca si ya existe la opcion de "Todas las tareas"
                        dv.RowFilter = GridColumns.DOC_TYPE_ID_COLUMNNAME & " = 0"
                        If dv.ToTable.Rows.Count = 0 Then
                            '[Ezequiel] Si hay varios agrego la opcion de ver todas las tareas.
                            Dim dr As DataRow = dt.NewRow
                            dr(GridColumns.DOC_TYPE_ID_COLUMNNAME) = 0
                            dr(GridColumns.DOC_TYPE_NAME_COLUMNNAME) = "Todas las tareas"
                            dt.Rows.InsertAt(dr, 0)
                        End If
                        GridView.cmbDocType.SelectedIndex = 0
                        GridView.cmbDocType.Enabled = True
                    End If
                Else
                    '[Ezequiel] La etapa no posee tareas entonces griso el combobox y borro datos de la grilla 
                    GridView.cmbDocType.Enabled = False
                    GridView.NewGrid.Columns.Clear()
                    GridView.ReloadLsvFilters()
                End If

                ShowTaskOfDT()

            Catch ex As Exception
                blncancelrefresh = False
                ZClass.raiseerror(ex)
            Finally
                If Not IsNothing(dv) Then
                    dv.Dispose()
                    dv = Nothing
                End If
                If Not IsNothing(dv2) Then
                    dv2.Dispose()
                    dv2 = Nothing
                End If
                If Not IsNothing(dt) Then
                    'No hacer dispose de este objeto ya que rompe la 
                    'paginacion y seleccion de la entidad en el combo
                    'dt.Dispose()
                    dt = Nothing
                End If
                doccount = Nothing
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.Default})
            End Try
        End Sub

        Private OldDocTypeId As Int64
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

        ''' <summary>
        ''' Metodo el cual esta asociado al combobox de la grilla que muestra las tareas de la entidad seleccionado en la misma.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Ezequiel] Created
        '''     [Tomas]     Modified    19/04/2011  Se quitan las validaciones de si el datatable tiene filas
        '''                                         ya que 
        ''' </history>
        Private Sub ShowTaskOfDT() Implements IGrid.ShowTaskOfDT
            Dim dtAux As DataTable
            Dim table As DataTable
            Try
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.WaitCursor})
                dtAux = New DataTable()
                Dim TemporalMinimunCapacity As Int32

                'Vacía los datos viejos
                GridView.DisposeSources()
                Dim GroupByCount As New Hashtable
                If stateid = 0 AndAlso UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.ShowStates, stepid) Then
                    table = New DataTable
                    table.MinimumCapacity = 0
                Else
                    Dim docTypeSelected As Long = GridView.cmbDocType.ComboBox.SelectedValue
                    Dim rowsPerPage As Int32 = (UserPreferences.getValue("CantidadFilas", UPSections.UserPreferences, 100))
                    Dim wftb As New WFTaskBusiness

                    If docTypeSelected <> 0 Then
                        table = wftb.GetTasksByStepandDocTypeId(stepid,
                                                                docTypeSelected,
                                                                True,
                                                                CurrentUserId,
                                                                FC,
                                                                LastPage,
                                                                rowsPerPage, SearchType.GridResults,
                                                                _orderString,
                                                                stateid,
                                                                _GroupByString,
                                                                GroupByCount)
                    Else
                        table = New DataTable
                        table.MinimumCapacity = 0
                        Dim tabletemp As DataTable
                        For Each id As DataRowView In GridView.cmbDocType.ComboBox.Items
                            If id.Row.ItemArray(0) > 0 Then
                                tabletemp = wftb.GetTasksByStepandDocTypeId(stepid,
                                                                            id.Row.ItemArray(0),
                                                                            True,
                                                                            CurrentUserId,
                                                                            FC,
                                                                            LastPage,
                                                                            rowsPerPage,
                                                                            SearchType.GridResults,
                                                                            _orderString,
                                                                            stateid)

                                table.Merge(tabletemp.Copy())
                                table.MinimumCapacity = table.MinimumCapacity + tabletemp.MinimumCapacity
                            End If
                        Next
                        If tabletemp IsNot Nothing Then
                            tabletemp.Dispose()
                            tabletemp = Nothing
                        End If
                    End If
                    wftb = Nothing
                End If

                If table IsNot Nothing Then
                    dtAux.MinimumCapacity = table.MinimumCapacity

                    SyncLock (dtAux)
                        SyncLock (GridView)
                            dtAux.Rows.Clear()
                            dtAux.PrimaryKey = New DataColumn() {}
                            dtAux.Columns.Clear()
                            dtAux.AcceptChanges()
                            LastResultDocTypeId = 0

                            If table.Rows.Count > 0 Then

                                For Each column As String In GridColumns.TaskGridColumnsToRemove
                                    If table.Columns.Contains(column) Then
                                        table.Columns.Remove(column)
                                    End If
                                Next

                                If Not table.Columns.Contains(GridColumns.VER_COLUMNNAME) Then table.Columns.Add(GridColumns.VER_COLUMNNAME, GetType(Boolean))
                                If Not table.Columns.Contains(GridColumns.IMAGEN_COLUMNNAME) Then table.Columns.Add(GridColumns.IMAGEN_COLUMNNAME, GetType(Image))
                                If Not table.Columns.Contains(GridColumns.SITUACIONICON_COLUMNNAME) Then table.Columns.Add(GridColumns.SITUACIONICON_COLUMNNAME, GetType(Image))
                                If Not table.Columns.Contains(GridColumns.EXPIREDATE_COLUMNNAME) Then table.Columns.Add(GridColumns.EXPIREDATE_COLUMNNAME, GetType(DateTime))
                                If Not table.Columns.Contains(GridColumns.TASKCOLOR_COLUMNNAME) Then table.Columns.Add(GridColumns.TASKCOLOR_COLUMNNAME, GetType(String))

                                If table.Columns.Contains(GridColumns.DOC_ID_COLUMNNAME) Then table.Columns(GridColumns.DOC_ID_COLUMNNAME).ColumnName = "DocId"
                                If table.Columns.Contains(GridColumns.STEP_ID_COLUMNNAME) Then table.Columns(GridColumns.STEP_ID_COLUMNNAME).ColumnName = "WfStepId"
                                If table.Columns.Contains(GridColumns.DOC_TYPE_ID_COLUMNNAME) Then table.Columns(GridColumns.DOC_TYPE_ID_COLUMNNAME).ColumnName = "DoctypeId"

                                daysAndColors = WFStepBusiness.GetStepColors(stepid)

                                Dim AnyStateAsigned As Boolean
                                Dim containImageAndIconCol As Boolean = table.Columns.Contains(GridColumns.IMAGEN_COLUMNNAME) AndAlso table.Columns.Contains(GridColumns.ICONID_COLUMNNAME)
                                Dim containAsignedCol As Boolean = table.Columns.Contains(GridColumns.ASIGNADO_COLUMNNAME)
                                Dim containExpireDateCol As Boolean = table.Columns.Contains(GridColumns.EXPIREDATE_COLUMNNAME)
                                Dim containSituationCol As Boolean = table.Columns.Contains(GridColumns.SITUACION_COLUMNNAME)
                                Dim containStateCol As Boolean = table.Columns.Contains(GridColumns.STATE_COLUMNNAME)

                                For Each row As DataRow In table.Rows
                                    If containImageAndIconCol Then
                                        row.Item(GridColumns.IMAGEN_COLUMNNAME) = IconsHelper.GetIcon(Int16.Parse(row.Item(GridColumns.ICONID_COLUMNNAME).ToString), GridView.currentRowHeight - 5, GridView.currentRowHeight - 5)
                                    End If
                                    If containAsignedCol Then
                                        row.Item(GridColumns.ASIGNADO_COLUMNNAME) = row.Item(GridColumns.USER_ASIGNEDNAME_COLUMNNAME)
                                    End If

                                    If containExpireDateCol Then
                                        Try
                                            If String.Compare(row.Item(GridColumns.EXPIREDATE_COLUMNNAME), "#12:00:00 AM#") = 0 Then
                                                row.Item(GridColumns.EXPIREDATE_COLUMNNAME) = String.Empty
                                            End If
                                        Catch ex As Exception
                                            ZClass.raiseerror(ex)
                                        End Try
                                    End If

                                    If containSituationCol Then
                                        Try
                                            Select Case row.Item(GridColumns.SITUACION_COLUMNNAME).ToString()
                                                Case "Asignada"
                                                    row.Item(GridColumns.SITUACIONICON_COLUMNNAME) = Global.Zamba.Controls.My.Resources.user_ok_man_male_profile_account_person_people_512.GetThumbnailImage(GridView.currentRowHeight, GridView.currentRowHeight, cb, cbd)
                                                Case "Ejecucion"
                                                    row.Item(GridColumns.SITUACIONICON_COLUMNNAME) = Global.Zamba.Controls.My.Resources.user_lock_man_male_profile_account_person_512.GetThumbnailImage(GridView.currentRowHeight, GridView.currentRowHeight, cb, cbd)
                                                Case "Servicio"
                                                    row.Item(GridColumns.SITUACIONICON_COLUMNNAME) = Global.Zamba.Controls.My.Resources.server.GetThumbnailImage(GridView.currentRowHeight, GridView.currentRowHeight, cb, cbd)
                                            End Select
                                        Catch ex As Exception
                                            ZClass.raiseerror(ex)
                                        End Try
                                    End If

                                    If Not AnyStateAsigned AndAlso containStateCol Then
                                        Try
                                            If row.Item(GridColumns.STATE_COLUMNNAME).ToString().Length > 0 Then
                                                AnyStateAsigned = True
                                            End If
                                        Catch ex As Exception
                                            ZClass.raiseerror(ex)
                                        End Try
                                    End If
                                    verifyIfOverdueTask(row)
                                Next

                                If containStateCol AndAlso Not AnyStateAsigned Then
                                    If GridColumns.ColumnsVisibility.ContainsKey(GridColumns.STATE_COLUMNNAME.ToLower()) Then
                                        GridColumns.ColumnsVisibility(GridColumns.STATE_COLUMNNAME.ToLower()) = False
                                    Else
                                        GridColumns.ColumnsVisibility.Add(GridColumns.STATE_COLUMNNAME.ToLower(), False)
                                    End If
                                End If

                                If table.Columns.Contains(GridColumns.VER_COLUMNNAME) Then table.Columns.Item(GridColumns.VER_COLUMNNAME).SetOrdinal(0)
                                If table.Columns.Contains(GridColumns.IMAGEN_COLUMNNAME) Then table.Columns.Item(GridColumns.IMAGEN_COLUMNNAME).SetOrdinal(1)
                                If table.Columns.Contains(GridColumns.SITUACIONICON_COLUMNNAME) Then table.Columns.Item(GridColumns.SITUACIONICON_COLUMNNAME).SetOrdinal(3)
                                If table.Columns.Contains(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME) Then table.Columns.Item(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME).SetOrdinal(2)
                                If table.Columns.Contains(GridColumns.DOC_TYPE_NAME_COLUMNNAME) Then table.Columns.Item(GridColumns.DOC_TYPE_NAME_COLUMNNAME).SetOrdinal(4)
                                If table.Columns.Contains(GridColumns.CHECKIN_COLUMNNAME) Then table.Columns.Item(GridColumns.CHECKIN_COLUMNNAME).SetOrdinal(table.Columns.Count - 1)
                                If table.Columns.Contains(GridColumns.EXPIREDATE_COLUMNNAME) Then table.Columns.Item(GridColumns.EXPIREDATE_COLUMNNAME).SetOrdinal(table.Columns.Count - 1)
                                If table.Columns.Contains(GridColumns.CRDATE_COLUMNNAME) Then table.Columns.Item(GridColumns.CRDATE_COLUMNNAME).SetOrdinal(table.Columns.Count - 1)
                                If table.Columns.Contains(GridColumns.LASTUPDATE_COLUMNNAME) Then table.Columns.Item(GridColumns.LASTUPDATE_COLUMNNAME).SetOrdinal(table.Columns.Count - 1)
                                If table.Columns.Contains(GridColumns.WORKFLOW_COLUMNAME) Then table.Columns.Item(GridColumns.WORKFLOW_COLUMNAME).SetOrdinal(table.Columns.Count - 1)
                                If table.Columns.Contains(GridColumns.SITUACION_COLUMNNAME) Then table.Columns.Item(GridColumns.SITUACION_COLUMNNAME).SetOrdinal(table.Columns.Count - 1)
                                If table.Columns.Contains(GridColumns.ORIGINAL_FILENAME_COLUMNNAME) Then table.Columns.Item(GridColumns.ORIGINAL_FILENAME_COLUMNNAME).SetOrdinal(table.Columns.Count - 1)
                                If table.Columns.Contains(GridColumns.DOC_TYPE_ID_COLUMNNAME) Then table.Columns.Item(GridColumns.DOC_TYPE_ID_COLUMNNAME).SetOrdinal(table.Columns.Count - 1)

                                If dtAux IsNot Nothing Then
                                    Try
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valores de la grilla de tareas  filas: " & table.Rows.Count & " columnas: " & dtAux.Columns.Count)
                                        dtAux.AcceptChanges()
                                        table.AcceptChanges()
                                        dtAux.Merge(table.Copy())
                                    Catch ex As Exception
                                        dtAux.WriteXml(Tools.EnvironmentUtil.GetTempDir("\Exceptions").FullName & "\DTResults2.xml")
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                                        Zamba.Core.ZClass.raiseerror(ex)
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad filas: " & table.Rows.Count & " Cantidad filas cargadas: " & dtAux.Rows.Count)
                                        dtAux.Rows.Clear()
                                        dtAux.Merge(table.Copy())
                                    End Try
                                End If
                            End If

                            GridView.GroupByCountList = GroupByCount
                            GridView.DataSource = dtAux
                            GridView.AlwaysFit = True
                            GridController.SetColumnsVisibility(GridView)

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se recargaran los filtros.")
                            GridView.ReloadLsvFilters()
                            GridView.Tag = Nothing
                        End SyncLock
                        dt = dtAux
                    End SyncLock
                Else
                    _orderString = String.Empty
                    _GroupByString = String.Empty
                    dtAux.MinimumCapacity = 0
                    GridView.DataSource = Nothing
                End If
            Catch ex As Threading.SynchronizationLockException
                Zamba.Core.ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadAbortException
                Zamba.Core.ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadInterruptedException
                Zamba.Core.ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadStateException
                Zamba.Core.ZClass.raiseerror(ex)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            Finally
                If Not IsNothing(dtAux) Then
                    dtAux.Dispose()
                    dtAux = Nothing
                End If
                If Not IsNothing(table) Then
                    table.Dispose()
                    table = Nothing
                End If
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.Default})
            End Try
        End Sub


        ''' <summary>
        ''' Método que sirve colocar en la columna "TaskColor" el color que debería tener la fila (tarea)
        ''' </summary>
        ''' <param name="dtVenc"></param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    27/10/2008   Created    
        ''' </history>
        Private Sub colorDisplayForOverdueTasks(ByRef dtVenc As DataTable, ByVal wfstepid As Int64)
            Try
                daysAndColors = WFStepBusiness.GetStepColors(wfstepid)

                For Each row As DataRow In dtVenc.Rows
                    verifyIfOverdueTask(row)
                Next
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Método que sirve para verificar si la fila (tarea) está vencida
        ''' </summary>
        ''' <param name="row"></param>
        ''' <param name="rowPos"></param>
        ''' <param name="daysAndColors"></param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    27/10/2008   Created    
        '''     [Gaston]    29/10/2008   Modified   Si la tarea no vencio entonces la fila se colocaría como negra
        '''     ivan 08/08/2016 - Modifique porque no daba color cuando habia mas de un color asignado, siempre usaba el ultimo color.
        ''' </history>
        Private Sub verifyIfOverdueTask(ByRef row As DataRow)
            If daysAndColors IsNot Nothing AndAlso daysAndColors.Count > 0 Then
                Dim expireDate As DateTime = DirectCast(row.Item(GridColumns.EXPIREDATE_COLUMNNAME), DateTime).Date
                If Not IsNothing(expireDate) Then
                    If (DateTime.Compare(expireDate, DateTime.Now.Date) < 0) Then
                        Dim expireDays As TimeSpan = DateTime.Now.Date - expireDate
                        If daysAndColors.ContainsKey(expireDays.Days.ToString()) Then
                            row.Item(GridColumns.TASKCOLOR_COLUMNNAME) = daysAndColors(expireDays.Days.ToString())
                        Else
                            Dim keys As List(Of Int32) = getNumericKeys(daysAndColors)
                            row.Item(GridColumns.TASKCOLOR_COLUMNNAME) = daysAndColors(getExpireDateKey(keys, expireDays.Days))
                        End If
                    Else
                        row.Item(GridColumns.TASKCOLOR_COLUMNNAME) = "color: NEGRO"
                    End If
                End If
            End If
        End Sub

        Private Function getExpireDateKey(keys As List(Of Integer), days As Int32) As String

            Dim correctKey As Int32

            For Each key As Int32 In keys
                If key > days Then
                    Exit For
                End If
                correctKey = key
            Next

            Return correctKey.ToString()

        End Function

        Private Function getNumericKeys(ByVal daysAndColors As Hashtable) As List(Of Integer)

            Dim numericList As New List(Of Int32)

            For Each key As String In daysAndColors.Keys
                numericList.Add(Int32.Parse(key))
            Next

            Return numericList

        End Function

        Private Delegate Sub dRefreshGrid(ByVal items As ArrayList)


        Public Sub RemoveTaskExtern(ByRef Result As TaskResult)
            SyncLock _sync
                RemoveTask(Result)
            End SyncLock
        End Sub
        Public Sub RemoveTask(ByRef Result As TaskResult)
            If Not Disposing AndAlso Result IsNot Nothing Then
                Try
                    If dt IsNot Nothing Then
                        For Each row As DataRow In dt.Rows
                            If (String.Compare(row.Item(GridColumns.TASK_ID_COLUMNNAME).ToString(), Result.TaskId.ToString()) = 0) Then
                                dt.Rows.Remove(row)
                                dt.AcceptChanges()
                                Exit For
                            End If

                        Next
                    End If

                    If GridView IsNot Nothing AndAlso GridView.NewGrid IsNot Nothing Then
                        For counter As Integer = 0 To GridView.NewGrid.Rows.Count - 1
                            '[sebastian 03/12/2008] así es como estaba y cuando entraba en la coparacion daba exception
                            'If (String.Compare(Me.GridView.OutLookGrid.Rows.Item(counter).Cells("TaskId").Value.ToString(), Result.TaskId.ToString()) = 0) Then
                            If Not IsNothing(GridView.NewGrid.Rows.Item(counter).Cells(GridColumns.TASK_ID_COLUMNNAME).Value.ToString()) And Not IsNothing(Result) Then
                                If (String.Compare(GridView.NewGrid.Rows.Item(counter).Cells(GridColumns.TASK_ID_COLUMNNAME).Value.ToString(), Result.TaskId.ToString()) = 0) Then
                                    GridView.NewGrid.Rows.RemoveAt(counter)
                                    ' Tag que actua como bandera por si el usuario quiere ordenar por columna, quiere filtrar, presiona el botón Actualizar o el
                                    ' botón Inicial. De esta forma si aparece en el Tag "rowDelete" entonces antes de que se ordene por columna, se filtre, etc, se
                                    ' actualiza el datasource del GridView con el dt actualizado
                                    GridView.Tag = "rowDelete"
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                Catch ex As NullReferenceException
                    ZTrace.WriteLineIf(ZTrace.IsError, "Error generado al refrescar los procesos de zamba y cerrar el panel de tareas." &
                                      vbCrLf & ex.Message & vbCrLf & ex.Source)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Actualiza la tarea a la que se le modifico algo desde una formulario electronico
        ''' </summary>
        ''' <param name="Result">result seleccionado de la grilla</param>
        ''' <remarks>sebastian</remarks>
        ''' <history>22/10/2008
        ''' </history>
        Public Sub UpdateTask(ByRef Result As TaskResult)
            Try
                AddTaskToGridAfterDelete(Result)
                GridView.DataSource = dt
                GridView.Update()
                GridView.Refresh()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Dim IconsHelper As New Zamba.AppBlock.WFNodeHelper

        ''' <summary>
        ''' Agrega la tarea a la grilla
        ''' </summary>
        ''' <param name="r"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]    05/08/2008  Modified    Validación para r.State
        '''     [Gaston]    31/10/2008  Modified    Si r.State es nothing no se coloca el estado "[Ninguno]" como antes
        ''' </history>
        Private Sub AddTaskToGrid(ByVal r As TaskResult)
            Dim row As DataRow = Nothing

            If (dt.Columns.Count <= 2) Then
                dt.Columns.Add(GridColumns.TASK_ID_COLUMNNAME, GetType(String))
                dt.Columns.Add(GridColumns.VER_COLUMNNAME, GetType(Boolean))
                dt.Columns.Add(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME, GetType(String))
                dt.Columns.Add(GridColumns.IMAGEN_COLUMNNAME, GetType(Image))
                dt.Columns.Add(GridColumns.SITUACIONICON_COLUMNNAME, GetType(Image))
                dt.Columns.Add(GridColumns.ASIGNADO_COLUMNNAME, GetType(String))
                dt.Columns.Add(GridColumns.SITUACION_COLUMNNAME, GetType(String))
                dt.Columns.Add(GridColumns.STATE_COLUMNNAME, GetType(String))
                dt.Columns.Add(GridColumns.EXPIREDATE_COLUMNNAME, GetType(DateTime))
                dt.Columns.Add(GridColumns.WFSTEPID_COLUMNNAME, GetType(Int64))
                dt.Columns.Add(GridColumns.DOCID_COLUMNNAME, GetType(Int64))
                dt.Columns.Add(GridColumns.DOCTYPEID_COLUMNNAME, GetType(Int64))
                dt.Columns.Add(GridColumns.TASKCOLOR_COLUMNNAME, GetType(String))
                dt.Columns.Add(GridColumns.CHECKIN_COLUMNNAME, GetType(DateTime))
                dt.AcceptChanges()
            End If

            row = dt.NewRow

            row.Item(GridColumns.IMAGEN_COLUMNNAME) = IconsHelper.GetIcon(r.IconId, GridView.currentRowHeight - 5, GridView.currentRowHeight - 5)
            row.Item(GridColumns.ASIGNADO_COLUMNNAME) = UserGroupBusiness.GetUserorGroupNamebyId(r.AsignedToId)
            row.Item(GridColumns.WFSTEPID_COLUMNNAME) = r.StepId
            row.Item(GridColumns.DOCID_COLUMNNAME) = r.ID
            row.Item(GridColumns.DOCTYPEID_COLUMNNAME) = r.DocTypeId

            If (Not IsNothing(r.State)) Then
                row.Item(GridColumns.STATE_COLUMNNAME) = r.State.Name
            Else
                row.Item(GridColumns.STATE_COLUMNNAME) = "[Ninguno]"
            End If

            Try
                'todo: cambiar para que acepte la fecha guardada auqnue la pc tenga otro formato de fecha, si no tira error
                If Not r.ExpireDate = #12:00:00 AM# Then
                    row.Item(GridColumns.EXPIREDATE_COLUMNNAME) = r.ExpireDate
                Else
                    row.Item(GridColumns.EXPIREDATE_COLUMNNAME) = ""
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            row.Item(GridColumns.TASK_ID_COLUMNNAME) = r.TaskId
            row.Item(GridColumns.VER_COLUMNNAME) = False

            addColumnsIndex(r)
            addIndexData(r, row)
            dt.Rows.Add(row)
        End Sub

        ''' <summary>
        ''' Método que sirve para reemplazar la antigua fila (que representa una tarea) por la nueva fila (que representa la misma tarea) tras realizar
        ''' una modificación de los atributos (por ejemplo, en el formulario dinámico tras presionar el botón "Guardar") de la propia tarea
        ''' </summary>
        ''' <param name="r">Instancia de la tarea seleccionada en la grilla</param>
        ''' <remarks></remarks>
        ''' <history>
        ''' 	[Gaston]	06/03/2009	Modified    Algunas columnas que estaban vacías ahora se rellenan con los datos necesarios
        '''     [German]    04/12/2012  Modified    Se cambia la validacion por la cual se agregaban las columnas, ahora se verifica que no existan para agregarlas.
        ''' </history>
        Private Sub AddTaskToGridAfterDelete(ByVal r As TaskResult)
            Try
                If Not IsNothing(r) Then
                    Dim row As DataRow = Nothing
                    Dim changed As Boolean
                    If Not dt.Columns.Contains(GridColumns.TASK_ID_COLUMNNAME) Then
                        dt.Columns.Add(GridColumns.TASK_ID_COLUMNNAME, GetType(String))
                        changed = True
                    End If
                    If Not dt.Columns.Contains(GridColumns.VER_COLUMNNAME) Then
                        dt.Columns.Add(GridColumns.VER_COLUMNNAME, GetType(Boolean))
                        changed = True
                    End If
                    If Not dt.Columns.Contains(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME) Then
                        dt.Columns.Add(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME, GetType(String))
                        changed = True
                    End If
                    If Not dt.Columns.Contains(GridColumns.IMAGEN_COLUMNNAME) Then
                        dt.Columns.Add(GridColumns.IMAGEN_COLUMNNAME, GetType(Image))
                        changed = True
                    End If
                    If Not dt.Columns.Contains(GridColumns.ASIGNADO_COLUMNNAME) Then
                        dt.Columns.Add(GridColumns.ASIGNADO_COLUMNNAME, GetType(String))
                        changed = True
                    End If
                    If Not dt.Columns.Contains(GridColumns.SITUACION_COLUMNNAME) Then
                        dt.Columns.Add(GridColumns.SITUACION_COLUMNNAME, GetType(String))
                        changed = True
                    End If
                    If Not dt.Columns.Contains(GridColumns.STATE_COLUMNNAME) Then
                        dt.Columns.Add(GridColumns.STATE_COLUMNNAME, GetType(String))
                        changed = True
                    End If
                    If Not dt.Columns.Contains(GridColumns.EXPIREDATE_COLUMNNAME) Then
                        dt.Columns.Add(GridColumns.EXPIREDATE_COLUMNNAME, GetType(DateTime))
                        changed = True
                    End If
                    If Not dt.Columns.Contains(GridColumns.WFSTEPID_COLUMNNAME) Then
                        dt.Columns.Add(GridColumns.WFSTEPID_COLUMNNAME, GetType(Int64))
                        changed = True
                    End If
                    If Not dt.Columns.Contains(GridColumns.DOCID_COLUMNNAME) Then
                        dt.Columns.Add(GridColumns.DOCID_COLUMNNAME, GetType(Int64))
                        changed = True
                    End If
                    If Not dt.Columns.Contains(GridColumns.DOCTYPEID_COLUMNNAME) Then
                        dt.Columns.Add(GridColumns.DOCTYPEID_COLUMNNAME, GetType(Int64))
                        changed = True
                    End If
                    If Not dt.Columns.Contains(GridColumns.TASKCOLOR_COLUMNNAME) Then
                        dt.Columns.Add(GridColumns.TASKCOLOR_COLUMNNAME, GetType(String))
                        changed = True
                    End If
                    If Not dt.Columns.Contains(GridColumns.WORK_ID_COLUMNNAME) Then
                        dt.Columns.Add(GridColumns.WORK_ID_COLUMNNAME, GetType(Int64))
                        changed = True
                    End If
                    If changed Then
                        dt.AcceptChanges()
                    End If

                    row = dt.NewRow

                    row.Item(GridColumns.VER_COLUMNNAME) = False
                    row.Item(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME) = r.Name
                    row.Item(GridColumns.IMAGEN_COLUMNNAME) = IconsHelper.GetIcon(r.IconId, GridView.currentRowHeight - 5, GridView.currentRowHeight - 5)
                    row.Item(GridColumns.ASIGNADO_COLUMNNAME) = UserGroupBusiness.GetUserorGroupNamebyId(r.AsignedToId)
                    row.Item(GridColumns.SITUACION_COLUMNNAME) = r.TaskState.ToString
                    If row.Table.Columns.Contains(GridColumns.CRDATE_COLUMNNAME) Then
                        row.Item(GridColumns.CRDATE_COLUMNNAME) = r.CheckIn
                    End If
                    If (Not IsNothing(r.State)) Then
                        row.Item(GridColumns.STATE_COLUMNNAME) = r.State.Name
                    Else
                        row.Item(GridColumns.STATE_COLUMNNAME) = "[Ninguno]"
                    End If

                    Try
                        'todo: cambiar para que acepte la fecha guardada auqnue la pc tenga otro formato de fecha, si no tira error
                        If Not (r.ExpireDate = #12:00:00 AM#) Then
                            row.Item(GridColumns.EXPIREDATE_COLUMNNAME) = r.ExpireDate
                        Else
                            row.Item(GridColumns.EXPIREDATE_COLUMNNAME) = String.Empty
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    row.Item(GridColumns.DOCID_COLUMNNAME) = r.ID
                    row.Item(GridColumns.DOCTYPEID_COLUMNNAME) = r.DocTypeId
                    row.Item(GridColumns.WFSTEPID_COLUMNNAME) = r.StepId
                    If row.Table.Columns.Contains(GridColumns.DO_STATE_ID_COLUMNNAME) Then
                        row.Item(GridColumns.DO_STATE_ID_COLUMNNAME) = r.EstadoId
                    End If
                    If row.Table.Columns.Contains(GridColumns.CHECKIN_COLUMNNAME) Then
                        row.Item(GridColumns.CHECKIN_COLUMNNAME) = r.CheckIn
                    End If
                    If row.Table.Columns.Contains(GridColumns.USER_ASIGNED_COLUMNNAME) Then
                        row.Item(GridColumns.USER_ASIGNED_COLUMNNAME) = r.UsuarioAsignadoId
                    End If
                    If row.Table.Columns.Contains(GridColumns.USER_ASIGNED_BY_COLUMNNAME) Then
                        row.Item(GridColumns.USER_ASIGNED_BY_COLUMNNAME) = r.AsignedById
                    End If

                    If row.Table.Columns.Contains(GridColumns.DATE_ASIGNED_BY_COLUMNNAME) Then
                        row.Item(GridColumns.DATE_ASIGNED_BY_COLUMNNAME) = r.AsignedDate
                    End If
                    row.Item(GridColumns.TASK_ID_COLUMNNAME) = r.TaskId
                    If row.Table.Columns.Contains(GridColumns.TASK_STATE_ID_COLUMNNAME) Then
                        row.Item(GridColumns.TASK_STATE_ID_COLUMNNAME) = r.TaskState
                    End If
                    row.Item(GridColumns.WORK_ID_COLUMNNAME) = r.WorkId

                    If RightsBusiness.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.FlagAsRead, r.DocTypeId) Then
                        If row.Table.Columns.Contains("READDATE") Then row.Item("READDATE") = DateTime.Now
                    End If

                    addColumnsIndex(r)
                    addIndexData(r, row)

                    Dim fila As Int32 = 0

                    For Each dtrow As DataRow In dt.Rows
                        If (String.Compare(dtrow.Item(GridColumns.TASK_ID_COLUMNNAME).ToString(), r.TaskId.ToString()) = 0) Then
                            dt.Rows(fila).ItemArray = row.ItemArray
                            row.Item(GridColumns.TASKCOLOR_COLUMNNAME) = dt.Columns.Item("TaskColor").Table.Rows(fila).ItemArray(row.ItemArray.Length - 1).ToString
                            Exit For
                        End If

                        fila = fila + 1
                    Next
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private LastResultDocTypeId As Int64
        ''' <summary>
        ''' Método que sirve para crear las columnas Atributo de un result
        ''' </summary>
        ''' <param name="r"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]    21/08/2008  Created    Código tomado de UCFusion2
        ''' </history>
        Private Sub addColumnsIndex(ByVal r As TaskResult)
            'This If is to avoid trying to create recursive columns that are already created. Martin
            If LastResultDocTypeId = 0 OrElse LastResultDocTypeId <> r.DocType.ID Then
                LastResultDocTypeId = r.DocType.ID
                'Por cada Atributo
                For Each i As Index In r.Indexs
                    'todo optimizar esto para que se llame una sola vez
                    Dim visible As Boolean = UserBusiness.Rights.GetIndexRightValue(r.DocTypeId, i.ID, CInt(Membership.MembershipHelper.CurrentUser.ID), RightsType.TaskGridIndexView)
                    If visible = True Then
                        'Si todavía no fue creada una columna con ese Atributo
                        If Not (dt.Columns.Contains(i.Name)) Then

                            'Creo un tipo por default String
                            Dim iType As Type = GetType(String)

                            'Y si el tipo del indice no es nada, le cargo el type
                            If Not IsNothing(i.Type) Then
                                If i.DropDown = IndexAdditionalType.LineText Then
                                    iType = GetIndexType(i.Type)
                                Else
                                    iType = GetType(String)
                                End If
                            End If

                            'Y por último genero la columna con ese tipo
                            dt.Columns.Add(i.Name.Trim, iType)

                        End If
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' Método que sirve para llenar los datos de las columnas Atributo
        ''' </summary>
        ''' <param name="r"></param>
        ''' <param name="row"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]    21/08/2008  Created    Código tomado de UCFusion2
        ''' </history>
        Private Sub addIndexData(ByRef r As TaskResult, ByRef row As DataRow)
            For Each Indice As Index In r.Indexs
                Try
                    If dt.Columns.Contains(Indice.Name) = True Then
                        'Si Data tiene un valor que se le asigne al Item
                        If String.Compare(String.Empty, Indice.Data) <> 0 Then
                            If Indice.DropDown = IndexAdditionalType.LineText Then
                                row.Item(Indice.Name) = Indice.Data
                            Else
                                If String.IsNullOrEmpty(Indice.dataDescription) Then
                                    row.Item(Indice.Name) = Indice.Data
                                Else
                                    row.Item(Indice.Name) = Indice.dataDescription
                                End If
                            End If
                            'Si Data no tiene valor se le asigna el de DataDescription
                            '(si es que no esta vacío)
                        ElseIf String.Compare(String.Empty, Indice.dataDescription) <> 0 Then
                            row.Item(Indice.Name) = Indice.dataDescription
                        End If
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Next
        End Sub

        'todo: este tipo de funciones se deben poner o en core o en tools
        ''' <summary>
        ''' Método que sirve para castear el tipo de un Atributo a Type
        ''' </summary>
        ''' <param name="iType"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]    21/08/2008  Created    Código tomado de UCFusion2
        ''' </history>
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
        Public Sub AddTaskExtern(ByRef r As TaskResult)
            SyncLock _sync
                AddTask(r)
            End SyncLock
        End Sub
        ''' <summary>
        ''' Agrega una nueva tarea
        ''' </summary>
        ''' <param name="r"></param>
        ''' <remarks></remarks>
        Public Sub AddTask(ByRef r As TaskResult)
            Try
                AddTaskToGrid(r)

                dt.AcceptChanges()
                GridView.DataSource = dt
                GridView.Update()
                GridView.Refresh()

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub
        Public Sub UpdateTaskItemExtern(ByRef Result As TaskResult, ByVal SubItemIndex As Byte)
            SyncLock _sync
                UpdateTaskItem(Result, SubItemIndex)
            End SyncLock
        End Sub
        ''' <summary>
        ''' Actualiza un valor del result
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <param name="SubItemIndex"></param>
        ''' <remarks></remarks>
        Public Sub UpdateTaskItem(ByRef Result As TaskResult, ByVal SubItemIndex As Byte)
            Try
                Dim row As DataRow = Nothing
                For Each oldRow As DataRow In dt.Rows
                    If (Int32.Parse(oldRow.Item(GridColumns.TASK_ID_COLUMNNAME).ToString()) = Result.TaskId) Then
                        row = oldRow
                        Select Case SubItemIndex
                            Case 1 'AsignedTo
                                row.Item(GridColumns.ASIGNADO_COLUMNNAME) = UserGroupBusiness.GetUserorGroupNamebyId(Result.AsignedToId)
                                row.Item(GridColumns.SITUACION_COLUMNNAME) = Result.TaskState.ToString
                                'Case 2 'WfStep
                                '   row.Item("Etapa") = Result.WfStep.Name
                                Exit For
                            Case 3 'TaskState
                                row.Item(GridColumns.SITUACION_COLUMNNAME) = Result.TaskState.ToString
                                Exit For
                            Case 4 'State
                                row.Item(GridColumns.STATE_COLUMNNAME) = Result.State.Name
                                Exit For
                            Case 5 'ExpireDate
                                If Not Result.ExpireDate = #12:00:00 AM# Then
                                    row.Item(GridColumns.EXPIREDATE_COLUMNNAME) = Result.ExpireDate.ToString
                                Else
                                    row.Item(GridColumns.EXPIREDATE_COLUMNNAME) = ""
                                End If
                                Exit For
                        End Select
                    End If
                Next
                dt.AcceptChanges()
                'TODO: ML Ver que refresuqe la grila con el DT ya cambiado y no ir alabase a buscar todo.
                ShowTaskOfDT()
                'Me.GridView.DataSource = dt
                'Me.GridView.FixColumns()
                'Me.GridView.Refresh()
                'Me.GridView.Update()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub
        'Devuelve la fila seleccionada si la encuentra, sino nothing
        Private Function FindSubResultRoot(ByVal TaskId As Int64) As GridViewRowInfo
            If Not IsNothing(GridView.NewGrid.Rows) AndAlso GridView.NewGrid.Rows.Count > 0 Then
                Try
                    For Each row As GridViewRowInfo In GridView.NewGrid.Rows
                        If row.Cells(GridColumns.TASK_ID_COLUMNNAME) IsNot Nothing AndAlso row.Cells(GridColumns.TASK_ID_COLUMNNAME).Value IsNot Nothing AndAlso Int32.Parse(row.Cells(GridColumns.TASK_ID_COLUMNNAME).Value.ToString()) = TaskId Then
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
        Public Sub SelectTaskExtern(ByVal TaskId As Int64)
            SyncLock _sync
                SelectTask(TaskId)
            End SyncLock
        End Sub
        ''Selecciona la fila correspondiente al result
        Public Sub SelectTask(ByVal TaskId As Int64) 'Implements IUCFusion2.SelectResult
            Try
                Dim S As GridViewRowInfo = FindSubResultRoot(TaskId)
                If Not IsNothing(S) Then
                    If GridView.NewGrid.SelectedRows(0) Is S Then
                        Exit Sub
                    End If
                    SelectAllToFalse()

                    S.IsSelected = True
                    GridView.Refresh()
                End If
                S = Nothing
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Deselecciona una fila de la grilla
        ''' </summary>
        ''' <param name="TaskId">Id de la tarea a ser deseleccionada</param>
        ''' <history>   Marcelo Created 09/11/09</history>
        ''' <remarks></remarks>
        Public Sub DeSelectTask(ByVal TaskId As Int64) 'Implements IUCFusion2.SelectResult
            Dim row As GridViewRowInfo
            Try
                row = FindSubResultRoot(TaskId)
                If Not IsNothing(row) Then
                    GridView.DeselectRow(row)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                row = Nothing
            End Try
        End Sub

        Public Sub UpdateTaskAsignedUserExtern(ByVal taskid As Int64, ByVal userasignedName As String)
            SyncLock _sync
                UpdateTaskAsignedUser(taskid, userasignedName)
            End SyncLock
        End Sub

        ''' <summary>
        ''' Actualiza el usuario asignado a la grilla de tareas
        ''' </summary>
        ''' <param name="taskid"></param>
        ''' <param name="userasignedName"></param>
        ''' <remarks></remarks>
        ''' <history>Diego 10-09-2008 [Created]</history>
        Public Sub UpdateTaskAsignedUser(ByVal taskid As Int64, ByVal userasignedName As String)
            Dim dt As DataTable = DirectCast(GridView.DataSource, DataTable)
            If IsNothing(dt) OrElse dt.Rows.Count = 0 Then Exit Sub
            For Each r As DataRow In dt.Rows
                If CLng(r.Item(GridColumns.TASK_ID_COLUMNNAME)) = taskid Then
                    r.Item(GridColumns.ASIGNADO_COLUMNNAME) = userasignedName
                    Exit For
                End If
            Next
            GridView.DataSource = dt
        End Sub

        Private _orderString As String

        Public Sub AddOrderComponent(orderString As String) Implements IOrder.AddOrderComponent
            _orderString = orderString
        End Sub

        Public Sub AddGroupByComponent(GroupByString As String) Implements IGrid.AddGroupByComponent
            _GroupByString = GroupByString
            'Me._GroupByString = ""
        End Sub

        Public Sub RefreshResults() Implements IMenuContextContainer.RefreshResults

        End Sub

#End Region

        Private _fc As New FiltersComponent

        Public Property FC() As IFiltersComponent Implements IGrid.Fc
            Get
                Return _fc
            End Get
            Set(ByVal value As IFiltersComponent)
                _fc = value
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
        Private ReadOnly cb As Image.GetThumbnailImageAbort
        Private cbd As IntPtr

        Public Property ExportSize As Integer Implements IGrid.ExportSize
            Get
                Return _exportSize
            End Get
            Set(value As Integer)
                _exportSize = value
            End Set
        End Property

        Public Property SaveSearch As Boolean Implements IGrid.SaveSearch
            Get
            End Get
            Set(value As Boolean)
            End Set
        End Property

        Public Property SortChanged As Boolean Implements IOrder.SortChanged

        Public Property FiltersChanged As Boolean Implements IFilter.FiltersChanged
            Get

            End Get
            Set(value As Boolean)

            End Set
        End Property

        Public Property _GroupByString As String

        Public Property FontSizeChanged As Boolean Implements IGrid.FontSizeChanged

        Friend Sub SetFontSizeUp()
            GridView.SetFontSizeUp()
        End Sub

        Friend Sub SetFontSizeDown()
            GridView.SetFontSizeDown()
        End Sub

        Private Sub currentContextMenuClick(Action As String, listResults As List(Of IResult), ContextMenuContainer As IMenuContextContainer) Implements IMenuContextContainer.currentContextMenuClick
            controller.currentContextMenuClick(Action, listResults, ContextMenuContainer)
        End Sub
    End Class
End Namespace
