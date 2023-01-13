Imports Zamba.Core.WF.WF
Imports Zamba.Filters
Imports Zamba.AppBlock
Imports Zamba.Core
Imports Zamba.Data
Imports System.Collections.Generic
Imports Zamba.Filters.Interfaces
Imports Telerik.WinControls.UI


Namespace WF.TasksCtls


    ''' <summary>
    ''' Esta clase es la que maneja la grilla de tareas
    ''' </summary>
    ''' <history> Marcelo modified 18/09/2008 </history>
    ''' <history> Ezequiel modified 22/1/2001 - Se agrego un menu contextual y sus funciones respectivas </history>
    ''' <remarks></remarks>
    Public Class UCOthersWFTaskGrid
        Inherits ZControl
        Implements IGrid

#Region " Código generado por el Diseñador de Windows Forms "
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
        ' Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
        'Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
        'Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
        'Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
        'Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
        'Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
        'Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
        Friend WithEvents GridView As Grid.PageGroupGrid.PageGroupGrid
        Public Event Propiedades(ByRef Result As Result)
        Public Event Imprimir(ByVal Results() As Result, ByVal OnlyIndexs As Boolean)
        Public Event ConvertToPdf(ByVal Results() As Result)
        Public Event EditarPag(ByRef Result As Result)
        Public Event Historial(ByRef Result As Result)
        Public useVersion As Boolean

        Dim resultList As New SortedList()
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCOthersWFTaskGrid))
            Me.GridView = New Zamba.Grid.PageGroupGrid.PageGroupGrid(CurrentUserId, Me)
            Me.SuspendLayout()
            '
            'GridView
            '
            Me.GridView.AlwaysFit = False
            Me.GridView.BackColor = System.Drawing.Color.LightSteelBlue
            Me.GridView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GridView.ForeColor = System.Drawing.Color.Black
            Me.GridView.Location = New System.Drawing.Point(0, 0)
            Me.GridView.Name = "GridView"
            Me.GridView.PageSize = 100
            Me.GridView.ShortDateFormat = False
            Me.GridView.Size = New System.Drawing.Size(752, 424)
            Me.GridView.TabIndex = 0
            Me.GridView.UseColor = True
            '
            'UCOthersWFTaskGrid
            '
            Me.BackColor = System.Drawing.Color.White
            Me.Controls.Add(Me.GridView)
            Me.Name = "UCOthersWFTaskGrid"
            Me.Size = New System.Drawing.Size(752, 424)
            Me.ResumeLayout(False)

        End Sub

#End Region

#Region "Constructor"
        Dim CurrentUserId As Int64
        Dim FC As New FiltersComponent

        Public Sub New(ByVal CurrentUserId As Int64, ByRef FC As FiltersComponent)

            MyBase.New()
            InitializeComponent()
            Me.CurrentUserId = CurrentUserId
            'Dim IL As New Zamba.AppBlock.ZIconsList
            'Me.SmallImageList = IL.ZIconList
            RemoveHandler GridView.view.OnRowClick, AddressOf GridView_Click
            RemoveHandler GridView.view.OnDoubleClick, AddressOf GridView_DoubleClick
            RemoveHandler GridView.view.SelectAllClick, AddressOf SelectedTasks
            RemoveHandler GridView.view.DeselectAllClick, AddressOf clearRulesOfToolBar
            RemoveHandler GridView.view.UpdateDs, AddressOf updateDataGridView
            AddHandler GridView.view.OnRowClick, AddressOf GridView_Click
            AddHandler GridView.view.OnDoubleClick, AddressOf GridView_DoubleClick
            AddHandler GridView.view.SelectAllClick, AddressOf SelectedTasks
            AddHandler GridView.view.DeselectAllClick, AddressOf clearRulesOfToolBar
            AddHandler GridView.view.UpdateDs, AddressOf updateDataGridView
            'Me.GridView.ContextMenuStrip = DirectCast(contextMenuResult, ContextMenuStrip)
            Me.FC = FC

            AddHandler Me.GridView.NewGrid.ContextMenuOpening, AddressOf newGrid_ContextMenuOpening



            'MenuShowVersionComment
            '
            MenuShowVersionComment.Name = "MenuShowVersionComment"
            MenuShowVersionComment.Size = New System.Drawing.Size(205, 22)
            MenuShowVersionComment.Text = "Versión del documento"
            MenuShowVersionComment.Tag = "ShowVersion"
            AddHandler MenuShowVersionComment.Click, AddressOf contextMenuResult_ItemClicked
            '
            'ExportarAPDFToolStripMenuItem
            '
            ExportarAPDFToolStripMenuItem.Name = "ExportarAPDFToolStripMenuItem"
            ExportarAPDFToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
            ExportarAPDFToolStripMenuItem.Text = "Exportar a PDF"
            ExportarAPDFToolStripMenuItem.Tag = "ExportToPDF"
            AddHandler ExportarAPDFToolStripMenuItem.Click, AddressOf contextMenuResult_ItemClicked
            '
            'ImprimirIndicesToolStripMenuItem
            '
            ImprimirIndicesToolStripMenuItem.Name = "ImprimirIndicesToolStripMenuItem"
            ImprimirIndicesToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
            ImprimirIndicesToolStripMenuItem.Text = "Imprimir Atributos"
            ImprimirIndicesToolStripMenuItem.Tag = "PrintIndexs"
            AddHandler ImprimirIndicesToolStripMenuItem.Click, AddressOf contextMenuResult_ItemClicked
            '
            'ToolStripSeparator1
            '
            ToolStripSeparator1.Name = "ToolStripSeparator1"
            ToolStripSeparator1.Size = New System.Drawing.Size(202, 6)
            '
            'btnEditar
            '
            btnEditar.Name = "btnEditar"
            btnEditar.Size = New System.Drawing.Size(205, 22)
            btnEditar.Text = "Editor de TIF"
            btnEditar.Tag = "EditTIF"
            AddHandler btnEditar.Click, AddressOf contextMenuResult_ItemClicked
            '
            'ToolStripSeparator6
            '
            ToolStripSeparator6.Name = "ToolStripSeparator6"
            ToolStripSeparator6.Size = New System.Drawing.Size(202, 6)
            '
            'btHistorial
            '
            btHistorial.Name = "btHistorial"
            btHistorial.Size = New System.Drawing.Size(205, 22)
            btHistorial.Text = "Historial"
            btHistorial.Tag = "History"
            AddHandler btHistorial.Click, AddressOf contextMenuResult_ItemClicked
            '
            'PropiedadesToolStripMenuItem
            '
            PropiedadesToolStripMenuItem.Name = "PropiedadesToolStripMenuItem"
            PropiedadesToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
            PropiedadesToolStripMenuItem.Text = "Propiedades"
            PropiedadesToolStripMenuItem.Tag = "Property"
            AddHandler PropiedadesToolStripMenuItem.Click, AddressOf contextMenuResult_ItemClicked
            '
            'ToolStripSeparator5
            '
            ToolStripSeparator5.Name = "ToolStripSeparator5"
            ToolStripSeparator5.Size = New System.Drawing.Size(202, 6)
            '
            'UCContextResult
            '
            UCContextResult.Items.AddRange(New RadMenuItem() {ExportarAPDFToolStripMenuItem, ToolStripSeparator1, btnEditar, ToolStripSeparator6, btHistorial, PropiedadesToolStripMenuItem, ToolStripSeparator5, ImprimirIndicesToolStripMenuItem, MenuShowVersionComment})

        End Sub

#End Region

#Region "Atributos"

        Dim WithEvents UCContextResult As RadContextMenu

        Dim ExportarAPDFToolStripMenuItem As New RadMenuItem
        Dim ToolStripSeparator1 As New RadMenuItem
        Dim btnEditar As New RadMenuItem
        Dim ToolStripSeparator6 As New RadMenuItem
        Dim btHistorial As New RadMenuItem
        Dim PropiedadesToolStripMenuItem As New RadMenuItem
        Dim ToolStripSeparator5 As New RadMenuItem
        Dim ImprimirIndicesToolStripMenuItem As New RadMenuItem
        Dim MenuShowVersionComment As New RadMenuItem

        Dim IL As New Zamba.AppBlock.ZIconsList
        Private dt As DataTable = New DataTable("DataSource")
        Private allowExecuteTasksAssignedToOtherUsers As Boolean
        'Public ZambaCore As ZambaCore
        Public objectId As Int64
        Public stepid As Int64

        Public Enum GridTypes
            All
            WorkFlow
            WFStep
        End Enum

        Public GridType As GridTypes

#End Region

#Region "Eventos"

        Public Event TaskSelected(ByRef Task As TaskResult)
        Public Event GridTasksSelected(ByRef Tasks As List(Of GridTaskResult))

        ' Si se clickeo sobre la grilla pero no hay ningun result chequeado dispara un evento para limpiar la toolbar de tareas
        Public Event NotAnyResultSelected()
        ' Si realizo un click en una (o mas tareas) se verifica si la tiene asignada el usuario actual u otro usuario 
        ' asignado. Luego, después de verificar los permisos que tenga el usuario actual para ejecutar tareas de otros 
        ' usuarios, se dispara el evento que muestra o oculta las reglas según el permiso
        Public Event visibleOrInvisibleButtonsRule(ByRef state As Boolean)

        ' Devuelve un array con los results seleccionados o nothing si no hay ninguno
        Public Function SelectedTaskResults(ByVal useCheck As Boolean) As Generic.List(Of GridTaskResult)
            Try
                Dim a As Int32 = 0
                Dim arrayResults As New Generic.List(Of GridTaskResult)
                If (useCheck = True) Then
                    For Each row As GridViewRowInfo In Me.GridView.NewGrid.Rows()
                        If Not (IsNothing(row.Cells("Ver").Value)) Then
                            If (Boolean.Parse(row.Cells("Ver").Value.ToString()) = True) Then
                                If Not (IsNothing(row.Cells("Task_Id").Value)) Then
                                    If (String.Compare(row.Cells("Task_Id").Value.ToString(), String.Empty) <> 0) Then
                                        Dim key As Int64 = Int64.Parse(row.Cells("Task_Id").Value.ToString())

                                        'Dim tasks As Hashtable = WFTaskBusiness.GetHashTableTasksByStep(stepid)
                                        'For Each t As TaskResult In tasks.Values
                                        '    If t.TaskId = key Then
                                        '        arrayResults.Add(t)
                                        '    End If
                                        'Next

                                        arrayResults.Add(New GridTaskResult(CInt(row.Cells("WfStepId").Value), CLng(row.Cells("Task_Id").Value), CLng(row.Cells("DocId").Value), CLng(row.Cells("DoctypeId").Value), row.Cells("Nombre Documento").Value.ToString, DirectCast(System.Enum.Parse(GetType(TaskStates), row.Cells("Situacion").Value.ToString), TaskStates), UserBusiness.GetUserID(row.Cells("Asignado").Value.ToString), CLng(row.Cells("Do_State_ID").Value)))

                                        '   arrayResults.Add(WFTaskBusiness.GetTaskById(key, WFStepBusiness.GetStepById(stepid)))
                                    End If
                                    a += 1
                                End If
                            End If
                        End If
                    Next
                Else
                    For Each row As GridViewRowInfo In Me.GridView.NewGrid.SelectedRows()
                        If Not (IsNothing(row.Cells("Task_Id").Value)) Then
                            If (String.Compare(row.Cells("Task_Id").Value.ToString(), String.Empty) <> 0) Then
                                Dim key As Int64 = Int64.Parse(row.Cells("Task_Id").Value.ToString())
                                'Dim tasks As Hashtable = WFTaskBusiness.GetHashTableTasksByStep(stepid)
                                'For Each t As TaskResult In tasks.Values
                                '    If (t.TaskId = key) Then
                                '        t.WfStep.Rules = WFRulesBusiness.GetCompleteHashTableRulesByStep(t.StepId)
                                '        arrayResults.Add(t)
                                '    End If
                                'Next

                                arrayResults.Add(New GridTaskResult(CLng(row.Cells("WfStepId").Value), CLng(row.Cells("Task_Id").Value), CLng(row.Cells("DocId").Value), CLng(row.Cells("DoctypeId").Value), row.Cells("Nombre Documento").Value.ToString, DirectCast(System.Enum.Parse(GetType(TaskStates), row.Cells("Situacion").Value.ToString), TaskStates), UserBusiness.GetUserID(row.Cells("Asignado").Value.ToString), CLng(row.Cells("task_state_id").Value)))
                                '    arrayResults.Add(WFTaskBusiness.GetTaskById(key, WFStepBusiness.GetStepById(stepid)))
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
                    Dim row As GridViewRowInfo = Me.FindSubResultRoot(task.TaskId)
                    If Not IsNothing(row) Then
                        If useCheck = True Then
                            If Not (IsNothing(row.Cells("Ver").Value)) Then
                                row.Cells("Ver").Value = True
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

        Dim CurrentTaskId As Int64
        ''' <summary>
        ''' Dispara el evento ResultSelected pasando como parametro el primer result selecionado
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    24/07/2008  Modified
        '''     [Ezequiel]  22/01/2009  Modified
        '''     [Javier]    06/12/2010  Modified    GetTaskByTaskIdAndDocTypeIdAndStepId is now a instance method     
        ''' </history>
        Private Sub GridView_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim taskbs As New WFTaskBusiness()
            Try
                If GridView.NewGrid.SelectedRows.Count > 0 Then
                    Dim arrayResults As Generic.List(Of GridTaskResult) = SelectedTaskResults(False)
                    If Not IsNothing(arrayResults) AndAlso (arrayResults.Count > 0) Then
                        Dim CurrentTask As ITaskResult = taskbs.GetTaskByTaskIdAndDocTypeIdAndStepId(arrayResults(0).TaskId, arrayResults(0).doctypeid, arrayResults(0).StepId, 0)
                        If Not IsNothing(CurrentTask) Then
                            LoadRights(CurrentTask)
                            CurrentTaskId = CurrentTask.TaskId
                        End If
                        SelectedTasks()
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try


            'If (GridView.OutlookGrid.SelectedRows.Count > 0) Then
            '    SelectedTasks()
            'End If

        End Sub


        Private Sub newGrid_ContextMenuOpening(sender As Object, e As ContextMenuOpeningEventArgs)
            Try
                Dim celda As GridDataCellElement = CType(e.ContextMenuProvider, GridDataCellElement)
                If celda.RowInfo.Index >= 0 AndAlso celda.RowInfo.Index < celda.ViewInfo.Rows.Count Then
                    e.ContextMenu = Me.UCContextResult.DropDown
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub contextMenuResult_ItemClicked(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Select Case CStr(sender)
                    Case "Property"
                        _Propiedades()
                    Case "ExportToPDF"
                        _ConvertToPdf()
                    Case "EditTIF"
                        RaiseEvent EditarPag(Me.SelectedResults(0))
                    Case "History"
                        _Historial()
                    Case "PrintIndexs"
                        Me._Imprimir(True)
                    Case "ShowVersion"
                        Me.ShowVersionComment()
                End Select
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub _Propiedades()
            Dim arrayResults As Generic.List(Of GridTaskResult) = SelectedTaskResults(False)
            Dim results As Result = Results_Business.GetResult(arrayResults(0).ID, arrayResults(0).doctypeid)
            If Not IsNothing(results) Then
                RaiseEvent Propiedades(results)
            End If
        End Sub

        Private Sub _Imprimir(ByVal OnlyIndexs As Boolean)
            Dim arrayResults As Generic.List(Of GridTaskResult) = SelectedTaskResults(False)
            Dim results() As Result = {Results_Business.GetResult(arrayResults(0).ID, arrayResults(0).doctypeid)}
            If Not IsNothing(results) Then
                RaiseEvent Imprimir(results, OnlyIndexs)
            End If
        End Sub
        Private Sub _EditTiff()
            Dim arrayResults As Generic.List(Of GridTaskResult) = SelectedTaskResults(False)
            Dim results() As Result = {Results_Business.GetResult(arrayResults(0).ID, arrayResults(0).doctypeid)}
            If Not IsNothing(results) Then
                RaiseEvent EditarPag(results(0))
            End If
        End Sub
        Private Sub _ConvertToPdf()
            Dim arrayResults As Generic.List(Of GridTaskResult) = SelectedTaskResults(False)
            Dim results() As Result = {Results_Business.GetResult(arrayResults(0).ID, arrayResults(0).doctypeid)}
            If Not IsNothing(results) Then
                RaiseEvent ConvertToPdf(results)
            End If
        End Sub
        Private Sub _Historial()
            Dim results() As Result = Me.SelectedResults
            If Not IsNothing(results) Then
                RaiseEvent Historial(results(0))
            End If
        End Sub
        Public Event ShowComment(ByVal result As Result)
        Private Sub ShowVersionComment()
            Dim re As Result() = Me.SelectedResults()
            RaiseEvent ShowComment(re(0))
        End Sub
        Public Function SelectedResults() As Result()
            Dim arrayResults As Generic.List(Of GridTaskResult) = SelectedTaskResults(False)
            Dim results() As Result = {Results_Business.GetResult(arrayResults(0).ID, arrayResults(0).doctypeid)}
            Return results
        End Function

#Region "Permisos"
        Private Sub LoadRights(ByRef Result As ITaskResult)

            'HISTORIAL
            If Not UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.FrmDocHistory, RightsType.View) Then btHistorial.Enabled = False

            'EDITOR TIF
            If UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.Edit, Result.DocType.ID) = True AndAlso Result.IsTif Then
                btnEditar.Enabled = True
            Else
                btnEditar.Enabled = False
            End If

            'EXOPORT TO PDF
            If Result.IsImage Then
                ExportarAPDFToolStripMenuItem.Enabled = True
            Else
                ExportarAPDFToolStripMenuItem.Enabled = False
            End If

            'PROPIEDADES
            If Not UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.FrmDocProperty, RightsType.View) Then PropiedadesToolStripMenuItem.Enabled = False

            'VERSIONES
            useVersion = UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.ModuleVersions, RightsType.Use)
            MenuShowVersionComment.Enabled = useVersion

        End Sub

#End Region

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
        Private Sub SelectedTasks()
            Try
                Dim arrayResults As Generic.List(Of GridTaskResult) = SelectedTaskResults(True)
                Dim taskbs As New WFTaskBusiness()

                'Diego. Comento esta para que aunque sea nothing mande el evento
                'Esto me sirve para crear la toolbar de la tarea o no mostrarla en caso de no este seleccionada ninguna tarea
                If Not (IsNothing(arrayResults)) Then
                    If (arrayResults.Count = 1) Then
                        ' Para mostrar historial de la tarea seleccionada en la solapa Listado de aprobaciones
                        RaiseEvent TaskSelected(DirectCast(taskbs.GetTaskByTaskIdAndDocTypeIdAndStepId(arrayResults(0).TaskId, arrayResults(0).doctypeid, arrayResults(0).StepId, 0), TaskResult))
                        RaiseEvent GridTasksSelected(arrayResults)
                    ElseIf (arrayResults.Count > 1) Then

                        ' Para mostrar historial de las tareas seleccionadas en la solapa Acciones pendientes
                        RaiseEvent GridTasksSelected(arrayResults)

                        'SE COMENTA ESTE CODIGO YA QUE SE TRASLADA ESTA LOGICA A UCPANELS AL METODO QUE MUESTRA LAS REGLAS EN COMUN EN LA TOOLBAR
                        ' Si el usuario no tiene permiso para ejecutar tareas asignadas a otros usuarios
                        '''''''If (allowExecuteTasksAssignedToOtherUsers = False) Then
                        '''''''    ' Se verifica si hay alguna tarea seleccionada que pertenece a otro usuario, distinto al
                        '''''''    ' usuario actual. Si hay, entonces no se muestran las reglas
                        '''''''    verifyIfThereAreTaskAssignedToOtherUser(arrayResults)
                        '''''''End If
                        'se se clickeo sobre la grilla pero no hay ningun result chequeado dispara un evento para limpiar la toolbar de tareas
                        'RaiseEvent NotAnyResultSelected()
                        ' Ya no hay más tareas seleccionadas
                        'Controller.selectedTaskIds.Clear()
                    Else
                        'se se clickeo sobre la grilla pero no hay ningun result chequeado dispara un evento para limpiar la toolbar de tareas
                        RaiseEvent NotAnyResultSelected()
                        ' Ya no hay más tareas seleccionadas
                        Controller.selectedTaskIds.Clear()
                    End If
                Else
                    'se se clickeo sobre la grilla pero no hay ningun result chequeado dispara un evento para limpiar la toolbar de tareas
                    RaiseEvent NotAnyResultSelected()
                    ' Ya no hay más tareas seleccionadas
                    Controller.selectedTaskIds.Clear()
                End If

                Me.Select()

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        Private Sub SelectedTasks(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim arrayResults As Generic.List(Of GridTaskResult) = SelectedTaskResults(True)
                Dim taskbs As New WFTaskBusiness()

                'Diego. Comento esta para que aunque sea nothing mande el evento
                'Esto me sirve para crear la toolbar de la tarea o no mostrarla en caso de no este seleccionada ninguna tarea
                If Not (IsNothing(arrayResults)) Then
                    If (arrayResults.Count = 1) Then
                        ' Para mostrar historial de la tarea seleccionada en la solapa Listado de aprobaciones
                        RaiseEvent TaskSelected(DirectCast(taskbs.GetTaskByTaskIdAndDocTypeIdAndStepId(arrayResults(0).TaskId, arrayResults(0).doctypeid, arrayResults(0).StepId, 0), TaskResult))
                        RaiseEvent GridTasksSelected(arrayResults)
                    ElseIf (arrayResults.Count > 1) Then

                        ' Para mostrar historial de las tareas seleccionadas en la solapa Acciones pendientes
                        RaiseEvent GridTasksSelected(arrayResults)

                        'SE COMENTA ESTE CODIGO YA QUE SE TRASLADA ESTA LOGICA A UCPANELS AL METODO QUE MUESTRA LAS REGLAS EN COMUN EN LA TOOLBAR
                        ' Si el usuario no tiene permiso para ejecutar tareas asignadas a otros usuarios
                        '''''''If (allowExecuteTasksAssignedToOtherUsers = False) Then
                        '''''''    ' Se verifica si hay alguna tarea seleccionada que pertenece a otro usuario, distinto al
                        '''''''    ' usuario actual. Si hay, entonces no se muestran las reglas
                        '''''''    verifyIfThereAreTaskAssignedToOtherUser(arrayResults)
                        '''''''End If
                        'se se clickeo sobre la grilla pero no hay ningun result chequeado dispara un evento para limpiar la toolbar de tareas
                        'RaiseEvent NotAnyResultSelected()
                        ' Ya no hay más tareas seleccionadas
                        'Controller.selectedTaskIds.Clear()
                    Else
                        'se se clickeo sobre la grilla pero no hay ningun result chequeado dispara un evento para limpiar la toolbar de tareas
                        RaiseEvent NotAnyResultSelected()
                        ' Ya no hay más tareas seleccionadas
                        Controller.selectedTaskIds.Clear()
                    End If
                Else
                    'se se clickeo sobre la grilla pero no hay ningun result chequeado dispara un evento para limpiar la toolbar de tareas
                    RaiseEvent NotAnyResultSelected()
                    ' Ya no hay más tareas seleccionadas
                    Controller.selectedTaskIds.Clear()
                End If

                Me.Select()

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Método que ejecuta un evento encargado de limpiar las reglas de la toolbar
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    05/09/2008  Created     
        ''' </history>
        Private Sub clearRulesOfToolBar(Optional ByVal sender As Object = Nothing, Optional ByVal e As System.EventArgs = Nothing)
            RaiseEvent NotAnyResultSelected()
            Controller.selectedTaskIds.Clear()
        End Sub

        ''' <summary>
        ''' Método que sirve para actualizar el DataSource del GridView sólo cuando se haya distribuido una o más tareas. Este método es llamado por
        ''' el evento ColumnHeaderMouseClick (del OutlookGrid1) que es cuando se quiere ordenar por una columna, por el filtrar, por el actualizar y 
        ''' por el inicial. Es necesario para para obtener el datatable actualizado (que no contiene las tareas que se distribuyeron). 
        ''' Se hace preciso actualizar el DataSource porque sino al ordenar por columna o al filtrar se mostrarian las tareas que se distribuyeron 
        ''' (ya que el DataSource seguiría conteniendo el datatable desactualizado)
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    11/11/2008  Created     
        ''' </history>
        Private Sub updateDataGridView(Optional ByVal sender As Object = Nothing, Optional ByVal e As System.EventArgs = Nothing)

            If Not (String.IsNullOrEmpty(CType(Me.GridView.Tag, String))) Then
                Me.GridView.DataSource = dt
                Me.GridView.Tag = Nothing
            End If

        End Sub

        ''' <summary>
        ''' Método que sirve para obtener las tareas seleccionadas actualmente y ejecutar el evento TasksSelected que 
        ''' se encarga de mostrar un historial de las tareas seleccionadas que ejecutaron RequestAction 
        ''' (Este método sólo se ejecuta cuando el usuario presiona el botón Actualizar ubicado adentro de la solapa 
        ''' que se encarga de mostrar el historial de las tareas con RequestAction) 
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    25/07/2008 Created
        ''' </history>
        Public Sub updateResultsOfRequestAction()
            RaiseEvent GridTasksSelected(SelectedTaskResults(True))
        End Sub

        Public Event TaskDoubleClick(ByRef TASKID As Int64)

        'Dispara el evento doubleClick pasando como parametro el primer result selecionado
        Private Sub GridView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                If GridView.NewGrid.SelectedRows.Count > 0 Then
                    Dim arrayResults As Generic.List(Of GridTaskResult) = SelectedTaskResults(False)
                    If Not IsNothing(arrayResults) AndAlso (arrayResults.Count > 0) Then
                        If arrayResults(0).AsignedToId = 0 Or arrayResults(0).AsignedToId = Membership.MembershipHelper.CurrentUser.ID Then
                            RaiseEvent TaskDoubleClick(arrayResults(0).TaskId)
                        Else
                            Dim users As Generic.List(Of Long) = UserGroupBusiness.GetUsersIds(arrayResults(0).AsignedToId, True)
                            If users.Contains(Membership.MembershipHelper.CurrentUser.ID) Then
                                RaiseEvent TaskDoubleClick(arrayResults(0).TaskId)
                            Else
                                'todo: hacer que taskviewer sea readonly y qeu deja abrirla avisando
                                MessageBox.Show("La tarea se encuentra tomada por otro usuario o usted no pertence al grupo", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                    End If
                End If
                Me.Select()
                Me.GridView.FixColumns()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#End Region

#Region "Métodos públicos"
        Dim CurrentStepId As Int64
        ''' <summary>
        ''' Muestra las tareas en las que se encuentra el documento seleccionado
        ''' </summary>
        ''' <param name="wfstep"></param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Ezequiel]  04/03/2009  Created
        '''     [Javier]    06/12/2010  Modified    GetTaskByTaskIdAndDocTypeIdAndStepId is now a instance method
        ''' </history>
        Public Sub ShowAllTasksInOthersWFOfaTask(ByVal Result As TaskResult)
            Try
                Dim taskbs As New WFTaskBusiness()

                GridType = GridTypes.WFStep
                Dim table As DataTable = WFTaskBusiness.GetTasksByTask(Result).Tables(0)
                SyncLock (dt)
                    dt.Rows.Clear()
                    dt.PrimaryKey = New DataColumn() {}
                    dt.Columns.Clear()
                    dt.AcceptChanges()
                    LastResultDocTypeId = 0
                    Me.GridView.NewGrid.Columns.Clear()
                    Me.GridView.AlwaysFit = True

                    If table.Rows.Count > 0 Then

                        dt.Columns.Add("Ver", GetType(Boolean))
                        dt.Columns.Add("WorkFlow", GetType(String))
                        dt.Columns.Add("Etapa", GetType(String))
                        dt.Columns.Add("Nombre Documento", GetType(String))
                        dt.Columns.Add("Imagen", GetType(Image))
                        dt.Columns.Add("Asignado", GetType(String))
                        dt.Columns.Add("Situacion", GetType(String))
                        dt.Columns.Add("Estado Tarea", GetType(String))

                        If table.Columns.Contains("ExpireDate") Then
                            dt.Columns.Add("Vencimiento", table.Columns("ExpireDate").DataType)
                        Else
                            dt.Columns.Add("Vencimiento", table.Columns("Vencimiento").DataType)
                        End If

                        If table.Columns.Contains("Ver") = False Then
                            table.Columns.Add("State", GetType(String))
                            table.Columns.Add("Ver", GetType(Boolean))
                            'table.Columns.Add("Imagen", GetType(Image))
                            table.Columns.Add("Asignado", GetType(String))
                            'table.Columns.Add("Estado Tarea", GetType(String))
                            table.Columns.Add("TaskColor", GetType(String))
                            table.Columns.Add("Situacion", GetType(String))

                            table.Columns("name").ColumnName = "Nombre Documento"
                            table.Columns("doc_id").ColumnName = "DocId"
                            table.Columns("step_id").ColumnName = "WfStepId"
                            table.Columns("doc_type_id").ColumnName = "DoctypeId"
                            table.Columns("ExpireDate").ColumnName = "Vencimiento"
                            table.Columns("State").ColumnName = "Estado Tarea"
                        End If
                        'dt.Columns.Add("TaskColor", GetType(String))

                        For Each r As DataRow In table.Rows
                            'r.Item("Imagen") = IL.ZIconList.Images(Int32.Parse(r.Item("IconId")))
                            r.Item("Asignado") = UserGroupBusiness.GetUserorGroupNamebyId(CLng(r.Item("User_Asigned")))
                            r.Item("Situacion") = [Enum].GetName(GetType(Zamba.Core.TaskStates), Int32.Parse(r.Item("task_state_id").ToString))

                            Try
                                'todo: cambiar para que acepte la fecha guardada auqnue la pc tenga otro formato de fecha, si no tira error
                                'If r.Item("Vencimiento") = "#12:00:00 AM#" Then
                                If String.Compare(r.Item("Vencimiento").ToString, "#12:00:00 AM#") = 0 Then
                                    r.Item("Vencimiento") = ""
                                End If
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try

                            r.Item("Ver") = False
                        Next
                        'AddTaskToGrid(r)
                        If Not IsNothing(table) AndAlso Not IsNothing(dt) Then
                            Try
                                If table.Rows.Count > 0 AndAlso dt.Columns.Count > 0 Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valores de la grilla de tareas")
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad filas: " & table.Rows.Count)
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad columnas: " & dt.Columns.Count)

                                    dt.AcceptChanges()
                                    table.AcceptChanges()
                                    dt.Merge(table)
                                    For Each r As DataRow In dt.Rows
                                        Dim ContainsStepId As Integer = CInt(r.Item("Do_State_ID"))
                                        If (Not IsNothing(Result.State)) Then
                                            r.Item("Estado Tarea") = Result.State.Name
                                        Else
                                            r.Item("Estado Tarea") = "[Ninguno]"
                                        End If
                                        r.Item("WorkFlow") = WFBusiness.GetWorkflowNameByWFId(CLng(r.Item("Work_Id")))
                                        r.Item("Etapa") = WFStepBusiness.GetStepNameById(CLng(r.Item("WfStepId")))
                                    Next
                                End If
                            Catch ex As Exception
                                dt.WriteXml(Tools.EnvironmentUtil.GetTempDir("\Exceptions").FullName & "\DTResults2.xml")
                                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                                Zamba.Core.ZClass.raiseerror(ex)
                                'table = WFTaskBusiness.GetHashTableTasksByStep(wfstepId, RefreshList, 0, True)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad filas: " & table.Rows.Count)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad filas cargadas: " & dt.Rows.Count)
                                dt.Rows.Clear()
                                dt.Merge(table)
                                For Each r As DataRow In dt.Rows
                                    Dim ContainsStepId As Integer = CInt(r.Item("Do_State_ID"))
                                    If (Not IsNothing(Result.State)) Then
                                        r.Item("Estado Tarea") = Result.State.Name
                                    Else
                                        r.Item("Estado Tarea") = "[Ninguno]"
                                    End If
                                    r.Item("WorkFlow") = WFBusiness.GetWorkflowNameByWFId(CType(r.Item("Work_Id"), Long))
                                    r.Item("Etapa") = WFStepBusiness.GetStepNameById(CLng(r.Item("WfStepId")))
                                Next
                            End Try
                        End If
                    End If

                    Me.GridView.DataTable = dt
                    Me.GridView.FixColumns()

                    If Me.CurrentStepId = 0 AndAlso Me.CurrentStepId <> stepid Then
                        Me.CurrentStepId = stepid
                        Me.GridView.clearFilters()
                    End If

                    Me.GridView.FilterAfterRefresh()

                    Me.GridView.Update()
                    Me.GridView.Refresh()
                    Me.GridView.Tag = Nothing

                    'se evita el refresco asincronico para que no pierdan los cambios relizadados los filtros. [sebastian]
                    Me.GridView.setColumnVisible("Task_Id", False)
                    Me.GridView.setColumnVisible("Ver", False)
                    Me.GridView.setColumnVisible("Do_State_Id", False)
                    Me.GridView.setColumnVisible("IconId", False)
                    Me.GridView.setColumnVisible("User_Asigned_By", False)
                    Me.GridView.setColumnVisible("Date_asigned_By", False)
                    Me.GridView.setColumnVisible("Task_State_Id", False)
                    Me.GridView.setColumnVisible("Remark", False)
                    Me.GridView.setColumnVisible("Tag", False)
                    Me.GridView.setColumnVisible("Work_Id", False)
                    Me.GridView.setColumnVisible("State", False)
                    Me.GridView.setColumnVisible("WfStepId", False)
                    Me.GridView.setColumnVisible("DocId", False)
                    Me.GridView.setColumnVisible("DoctypeId", False)
                    Me.GridView.setColumnVisible("TaskColor", False)
                End SyncLock

                If Me.CurrentTaskId <> 0 Then
                    Me.SelectTask(Me.CurrentTaskId)
                    RaiseEvent TaskSelected(DirectCast(taskbs.GetTaskByTaskIdAndDocTypeIdAndStepId(CurrentTaskId, Result.DocTypeId, stepid, 0), TaskResult))
                    Dim arrayResults As Generic.List(Of GridTaskResult) = SelectedTaskResults(False)
                    RaiseEvent GridTasksSelected(arrayResults)
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
        Private Sub colorDisplayForOverdueTasks(ByRef dtVenc As DataTable)
            Try
                Dim daysAndColors As Hashtable = New Hashtable()
                Dim counter As Integer = 0

                For Each row As DataRow In dtVenc.Rows
                    daysAndColors.Add(row("OBJTWO").ToString(), row("OBJEXTRADATA").ToString())
                Next

                For Each row As DataRow In dt.Rows
                    verifyIfOverdueTask(row, counter, daysAndColors)
                    counter = counter + 1
                Next

                daysAndColors.Clear()

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
        ''' </history>
        Private Sub verifyIfOverdueTask(ByRef row As DataRow, ByRef rowPos As Integer, ByRef daysAndColors As Hashtable)
            If Not (IsNothing(row.Item("Vencimiento"))) Then

                ' Si la fecha de vencimiento es menor a la fecha actual
                If (DateTime.Compare((DirectCast(row.Item("Vencimiento"), DateTime)).Date, DateTime.Now.Date) = -1) Then

                    Dim numbersMinorsToDifDays As ArrayList = New ArrayList()
                    Dim dif As TimeSpan = DateTime.Now.Date - (DirectCast(row.Item("Vencimiento"), DateTime)).Date

                    For Each elem As DictionaryEntry In daysAndColors

                        ' Si la cantidad de días de diferencia que se llevan la fecha de vencimiento y la fecha actual es mayor al número de la colección
                        If (CType(dif.TotalDays, Integer) > CType(elem.Key, Integer)) Then
                            ' Agregar número a colección temporal
                            numbersMinorsToDifDays.Add(CType(elem.Key, Integer))
                        End If

                    Next

                    If (numbersMinorsToDifDays.Count > 0) Then

                        Dim previousNumber As Integer = 0

                        ' Busco el número más grande de la colección
                        For Each elem As Integer In numbersMinorsToDifDays

                            If (previousNumber = 0) Then
                                previousNumber = elem
                            Else
                                If (elem > previousNumber) Then
                                    previousNumber = elem
                                End If
                            End If

                        Next

                        putColorInRow(row, rowPos, previousNumber, daysAndColors)
                        numbersMinorsToDifDays.Clear()

                    End If
                Else
                    row.Item("TaskColor") = "color: NEGRO"
                End If
            End If
        End Sub

        ''' <summary>
        ''' Método que sirve para verificar el color del dia o mes configurado en el administrador y colocar en la columna "TaskColor" el color que
        ''' debería llevar la fila
        ''' </summary>
        ''' <param name="row"></param>
        ''' <param name="rowPos"></param>
        ''' <param name="previousNumber"></param>
        ''' <param name="daysAndColors"></param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    27/10/2008   Created    
        ''' </history>
        Private Sub putColorInRow(ByRef row As DataRow, ByRef rowPos As Integer, ByRef previousNumber As Integer, ByRef daysAndColors As Hashtable)
            If (daysAndColors.Contains(previousNumber.ToString())) Then
                Select Case DirectCast(daysAndColors(previousNumber.ToString()), String).ToUpper()
                    Case "ROJO"
                        row.Item("TaskColor") = "color: ROJO"
                    Case "VERDE"
                        row.Item("TaskColor") = "color: VERDE"
                    Case "AMARILLO"
                        row.Item("TaskColor") = "color: AMARILLO"
                    Case "AZUL"
                        row.Item("TaskColor") = "color: AZUL"
                    Case "VIOLETA"
                        row.Item("TaskColor") = "color: VIOLETA"
                    Case "GRIS"
                        row.Item("TaskColor") = "color: GRIS"
                End Select
            End If
        End Sub


        ''' <summary>
        ''' Metodo que actualiza la grilla
        ''' </summary>
        ''' <param name="dt">Datatable con la que se cargara la grilla</param>
        ''' <remarks></remarks>
        ''' <history>
        '''     Marcelo     modified 18/09/2008
        '''     [Gaston]    27/10/2008 Modified     Se verifica si la etapa puede mostrar tareas vencidas de distinto color
        ''' </history>
        Private Sub RefreshGrid(ByVal dt As DataTable)

            'Guardo los items seleccionados
            Dim arraySelectedResults As Generic.List(Of GridTaskResult) = SelectedTaskResults(False)
            Dim arrayCheckedResults As Generic.List(Of GridTaskResult) = SelectedTaskResults(True)

            If Not IsNothing(arraySelectedResults) Then
                LoadRights(DirectCast(Results_Business.GetResult(arraySelectedResults(0).ID, arraySelectedResults(0).doctypeid), ITaskResult))
            End If
            If (dt.Rows.Count > 0) Then

                Dim dtVenc As DataTable = WFStepBusiness.getTypesOfPermit(CLng(dt.Rows(0).Item("WfStepId")), True, TypesofPermits.Expired)

                If Not (IsNothing(dtVenc)) Then
                    If (dtVenc.Rows.Count > 0) Then
                        colorDisplayForOverdueTasks(dtVenc)
                        dtVenc.Dispose()
                    End If
                End If

            End If

            Me.GridView.DataTable = dt
            Me.GridView.FixColumns()
            'Me.GridView.clearFilters()'se comento la linea para no perder los filtros por defecto del list view
            ' de filtrado.[sebastian]
            Me.GridView.NewGrid.ClearSelection()
            'Me.GridView.Update()
            'Me.GridView.Refresh()

            'Vuelvo a marcar los items
            If (arraySelectedResults.Count > 0) Then
                SelectTaskResults(False, arraySelectedResults)
            End If
            If (arrayCheckedResults.Count > 0) Then
                SelectTaskResults(True, arrayCheckedResults)
            End If
        End Sub

        ''' <summary>
        ''' Quita la tarea de la grilla
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]    01/07/2008  Modified    Cuando se remueve una tarea se actualiza la grilla
        '''     [Gaston]    11/11/2008  Modified    Se remueve de la grilla la fila afectada (y también del dt), no hace falta volver a colocar como
        '''                                         origen de datos de la grilla el dt, excepto cuando se quiera ordenar por columna, filtrar, presionar
        '''                                         el botón Actualizar o el Inicial donde habrá que actualizar el datasource del GridView, ya que 
        '''                                         seguira trabajando con el datatable anterior, y no con el actualizado que es el que contiene las 
        '''                                         filas eliminadas
        ''' </history>
        Public Sub RemoveTask(ByRef Result As TaskResult)

            Try

                For Each row As DataRow In Me.dt.Rows
                    If (String.Compare(row.Item("Task_Id").ToString(), Result.TaskId.ToString()) = 0) Then
                        Me.dt.Rows.Remove(row)
                        Me.dt.AcceptChanges()
                        Exit For
                    End If

                Next

                For counter As Integer = 0 To Me.GridView.NewGrid.Rows.Count - 1
                    '[sebastian 03/12/2008] así es como estaba y cuando entraba en la coparacion daba exception
                    'If (String.Compare(Me.GridView.NewGrid.Rows.Item(counter).Cells("TaskId").Value.ToString(), Result.TaskId.ToString()) = 0) Then
                    If (String.Compare(Me.GridView.NewGrid.Rows.Item(counter).Cells("Task_Id").Value.ToString(), Result.TaskId.ToString()) = 0) Then
                        Me.GridView.NewGrid.Rows.RemoveAt(counter)
                        ' Tag que actua como bandera por si el usuario quiere ordenar por columna, quiere filtrar, presiona el botón Actualizar o el
                        ' botón Inicial. De esta forma si aparece en el Tag "rowDelete" entonces antes de que se ordene por columna, se filtre, etc, se
                        ' actualiza el datasource del GridView con el dt actualizado
                        Me.GridView.Tag = "rowDelete"
                        Exit For
                    End If

                Next

                'Me.GridView.DataSource = dt
                'Me.GridView.Update()
                'Me.GridView.Refresh()

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

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

                Me.GridView.DataSource = dt
                Me.GridView.Update()
                Me.GridView.Refresh()

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

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
                dt.Columns.Add("TaskId", GetType(String))
                dt.Columns.Add("Ver", GetType(Boolean))
                dt.Columns.Add("Nombre Documento", GetType(String))
                dt.Columns.Add("Imagen", GetType(Image))
                dt.Columns.Add("Asignado", GetType(String))
                'dt.Columns.Add("Etapa", GetType(String))
                dt.Columns.Add("Situacion", GetType(String))
                dt.Columns.Add("Estado Tarea", GetType(String))
                dt.Columns.Add("Vencimiento", GetType(DateTime))
                dt.Columns.Add("WfStepId", GetType(Int64))
                dt.Columns.Add("DocId", GetType(Int64))
                dt.Columns.Add("DoctypeId", GetType(Int64))
                dt.Columns.Add("TaskColor", GetType(String))
                dt.AcceptChanges()
            End If

            row = dt.NewRow

            row.Item("Imagen") = IL.ZIconList.Images(r.IconId)
            row.Item("Nombre Documento") = r.Name
            row.Item("Asignado") = UserGroupBusiness.GetUserorGroupNamebyId(r.AsignedToId)
            row.Item("WfStepId") = r.StepId
            row.Item("DocId") = r.ID
            row.Item("DoctypeId") = r.DocTypeId
            'row.Item("Etapa") = r.WfStep.Name
            row.Item("Situacion") = r.TaskState.ToString
            'row.Item("TaskColor") = "Negro"

            If (Not IsNothing(r.State)) Then
                row.Item("Estado Tarea") = r.State.Name
            Else
                row.Item("Estado Tarea") = "[Ninguno]"
            End If

            Try
                'todo: cambiar para que acepte la fecha guardada auqnue la pc tenga otro formato de fecha, si no tira error
                If Not r.ExpireDate = #12:00:00 AM# Then
                    row.Item("ExpireDate") = r.ExpireDate
                Else
                    row.Item("ExpireDate") = ""
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            row.Item("TaskId") = r.TaskId
            row.Item("Ver") = False

            addColumnsIndex(r)
            addIndexData(r, row)
            dt.Rows.Add(row)
        End Sub

        Private Sub AddTaskToGridAfterDelete(ByVal r As TaskResult)
            Dim row As DataRow = Nothing

            If dt.Columns.Count <= 2 Then

                dt.Columns.Add("Task_Id", GetType(String))
                dt.Columns.Add("Ver", GetType(Boolean))
                dt.Columns.Add("Nombre Documento", GetType(String))
                dt.Columns.Add("Imagen", GetType(Image))
                dt.Columns.Add("Asignado", GetType(String))
                'dt.Columns.Add("Etapa", GetType(String))
                dt.Columns.Add("Situacion", GetType(String))
                dt.Columns.Add("Estado Tarea", GetType(String))
                dt.Columns.Add("Vencimiento", GetType(DateTime))
                dt.Columns.Add("WfStepId", GetType(Int64))
                dt.Columns.Add("DocId", GetType(Int64))
                dt.Columns.Add("DoctypeId", GetType(Int64))
                dt.Columns.Add("TaskColor", GetType(String))
                dt.AcceptChanges()

            End If

            row = dt.NewRow

            row.Item("Imagen") = IL.ZIconList.Images(r.IconId)
            row.Item("Nombre Documento") = r.Name
            row.Item("Asignado") = UserGroupBusiness.GetUserorGroupNamebyId(r.AsignedToId)
            row.Item("WfStepId") = r.StepId
            row.Item("DocId") = r.ID
            row.Item("DoctypeId") = r.DocTypeId
            'row.Item("Etapa") = r.WfStep.Name
            row.Item("Situacion") = r.TaskState.ToString
            ' row.Item("TaskColor") = "Negro"

            If (Not IsNothing(r.State)) Then
                row.Item("Estado Tarea") = r.State.Name
            Else
                row.Item("Estado Tarea") = "[Ninguno]"
            End If

            Try
                'todo: cambiar para que acepte la fecha guardada auqnue la pc tenga otro formato de fecha, si no tira error
                If Not r.ExpireDate = #12:00:00 AM# Then
                    row.Item("Vencimiento") = r.ExpireDate
                Else
                    row.Item("Vencimiento") = ""
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            row.Item("Task_Id") = r.TaskId
            row.Item("Ver") = False

            addColumnsIndex(r)
            addIndexData(r, row)
            'dt.Rows.Add(row)
            Dim fila As Int32 = 0
            For Each dtrow As DataRow In dt.Rows
                If (String.Compare(dtrow.Item("Task_Id").ToString(), r.TaskId.ToString()) = 0) Then
                    'For Each item As Object In row.ItemArray
                    dt.Rows(fila).ItemArray = row.ItemArray
                    Exit For
                End If    ' Next
                fila = fila + 1
            Next
        End Sub
        Private LastResultDocTypeId As Int64 = 0
        ''' <summary>
        ''' Método que sirve para crear las columnas Atributo de un result
        ''' </summary>
        ''' <param name="r"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]    21/08/2008  Created    Código tomado de UCFusion2
        ''' </history>
        Private Sub addColumnsIndex(ByRef r As TaskResult)
            'This If is to avoid trying to create recursive columns that are already created. Martin
            If LastResultDocTypeId = 0 OrElse LastResultDocTypeId <> r.DocType.ID Then
                LastResultDocTypeId = r.DocType.ID
                'Por cada Atributo
                For Each i As Index In r.Indexs
                    'todo optimizar esto para que se llame una sola vez
                    Dim visible As Boolean = UserBusiness.Rights.GetIndexRightValue(r.DocTypeId, i.ID, CInt(Membership.MembershipHelper.CurrentUser.ID), RightsType.IndexView)
                    If visible = True Then
                        'Si todavía no fue creada una columna con ese Atributo
                        If Not (Me.dt.Columns.Contains(i.Name)) Then

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
                            Me.dt.Columns.Add(i.Name.Trim, iType)

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
            Dim t As Type

            For Each Atributo As Index In r.Indexs
                Try
                    If dt.Columns.Contains(Atributo.Name) = True Then
                        'todo: cambiar para que acepte la fecha guardada auqnue la pc tenga otro formato de fecha, si no tira error
                        If Not IsNothing(Atributo.Type) Then
                            t = GetIndexType(Atributo.Type)
                        Else
                            t = GetType(String)
                        End If

                        'Si Data tiene un valor que se le asigne al Item
                        If String.Compare(String.Empty, Atributo.Data) <> 0 Then
                            If Atributo.DropDown = IndexAdditionalType.LineText Then
                                row.Item(Atributo.Name) = Atributo.Data
                            Else
                                If String.IsNullOrEmpty(Atributo.dataDescription) Then
                                    row.Item(Atributo.Name) = Atributo.Data
                                Else
                                    row.Item(Atributo.Name) = Atributo.dataDescription
                                End If
                            End If
                            'Si Data no tiene valor se le asigna el de DataDescription
                            '(si es que no esta vacío)
                        ElseIf String.Compare(String.Empty, Atributo.dataDescription) <> 0 Then
                            row.Item(Atributo.Name) = Atributo.dataDescription
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

        ''' <summary>
        ''' Agrega una nueva tarea
        ''' </summary>
        ''' <param name="r"></param>
        ''' <remarks></remarks>
        Public Sub AddTask(ByRef r As TaskResult)
            Try
                AddTaskToGrid(r)

                dt.AcceptChanges()
                Me.GridView.DataSource = dt
                Me.GridView.Update()
                Me.GridView.Refresh()

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
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
                    If (Int32.Parse(oldRow.Item("Task_Id").ToString()) = Result.TaskId) Then
                        row = oldRow
                        Select Case SubItemIndex
                            Case 1 'AsignedTo
                                row.Item("Asignado") = UserGroupBusiness.GetUserorGroupNamebyId(Result.AsignedToId)
                                'Case 2 'WfStep
                                '   row.Item("Etapa") = Result.WfStep.Name
                            Case 3 'TaskState
                                row.Item("Situacion") = Result.TaskState.ToString
                            Case 4 'State
                                row.Item("Estado Tarea") = Result.State.Name
                            Case 5 'ExpireDate
                                If Not Result.ExpireDate = #12:00:00 AM# Then
                                    row.Item("Vencimiento") = Result.ExpireDate.ToString
                                Else
                                    row.Item("Vencimiento") = ""
                                End If
                        End Select
                    End If
                Next
                dt.AcceptChanges()
                Me.GridView.DataSource = dt
                Me.GridView.FixColumns()
                Me.GridView.Refresh()
                Me.GridView.Update()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub
        'Devuelve la fila seleccionada si la encuentra, sino nothing
        Private Function FindSubResultRoot(ByVal TaskId As Int64) As GridViewRowInfo
            If Not IsNothing(GridView.NewGrid.Rows) AndAlso GridView.NewGrid.Rows.Count > 0 Then
                Try
                    For Each row As GridViewRowInfo In Me.GridView.NewGrid.Rows
                        If Not IsNothing(row.Cells("Task_Id").Value) AndAlso Int32.Parse(row.Cells("Task_Id").Value.ToString()) = TaskId Then
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
                For Each Item As GridViewRowInfo In Me.GridView.NewGrid.Rows
                    Item.IsSelected = False
                Next
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        ''Selecciona la fila correspondiente al result
        Public Sub SelectTask(ByVal TaskId As Int64) 'Implements IUCFusion2.SelectResult
            Dim S As GridViewRowInfo
            Try
                S = Me.FindSubResultRoot(TaskId)
                If Not IsNothing(S) Then
                    If Me.GridView.NewGrid.SelectedRows(0) Is S Then
                        Exit Sub
                    End If
                    SelectAllToFalse()

                    S.IsSelected = True
                    Me.GridView.Refresh()
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                S = Nothing
            End Try
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
                If CLng(r.Item("Task_Id")) = taskid Then
                    r.Item("Asignado") = userasignedName
                    Exit For
                End If
            Next
            GridView.DataSource = dt
        End Sub
#End Region


        Private Sub GridView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView.Load

        End Sub

        Public Property Fc1() As IFiltersComponent Implements IGrid.Fc
            Get

            End Get
            Set(ByVal value As IFiltersComponent)

            End Set
        End Property
        Private _lastPage As Integer

        Public Property LastPage() As Integer Implements IGrid.LastPage
            Get
                Return _lastPage
            End Get
            Set(ByVal value As Integer)
                _lastPage = value
            End Set
        End Property

        Public Property PageSize As Integer Implements IGrid.PageSize
            Get
                Throw New NotImplementedException()
            End Get
            Set(value As Integer)
                Throw New NotImplementedException()
            End Set
        End Property

        Public Property Exporting As Boolean Implements IGrid.Exporting
            Get
                Throw New NotImplementedException()
            End Get
            Set(value As Boolean)
                Throw New NotImplementedException()
            End Set
        End Property

        Public Property ExportSize As Integer Implements IGrid.ExportSize
            Get
                Throw New NotImplementedException()
            End Get
            Set(value As Integer)
                Throw New NotImplementedException()
            End Set
        End Property

        Public Sub ShowTaskOfDT() Implements IGrid.ShowTaskOfDT

        End Sub

        Public Sub AddOrderComponent(newValue As String, propertyName As String) Implements IGrid.AddOrderComponent

        End Sub
    End Class
End Namespace