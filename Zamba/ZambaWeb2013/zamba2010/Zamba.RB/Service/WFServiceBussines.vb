Imports Zamba.Core.WF.WF
Imports Zamba.Core.Enumerators
Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.Core.WFService
Imports System.Collections.Generic
Imports Zamba.ZTimers
Imports System.Threading

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.WFServiceBusiness
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Zamba WorkFlow
''' Servicio que se ejecuta en el servidor y controla las tareas y los tiempos de las mismas
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
'''     [Marcelo]   10/12/2008  Modified 
'''     [Ezequiel]   14/05/2009  Modified: Se adapto con ZTimers
''' </history>
''' -----------------------------------------------------------------------------
Public Class WFServiceBusiness
    Implements IDisposable, IService

#Region "Atributos"
    Public CheckAState As New ServiceState
    Public CheckPState As New ServiceState
    Public Running As Boolean
    Public WfServ As WFService
    Dim ServiceType As Int16
    Dim _TService As ClientType
    Dim CurrentUserId As Int64
    Dim _ServiceClearVariables As Boolean
    Dim _TickHourString As String
    Dim _WFServiceActExecute As Boolean
    Dim _WFServicePlanExecute As Boolean
    Dim _TimeStartT As Int32
    Dim _TimeEndT As Int32
    Private oldClientType As ClientType

    'Check : A|C|E|M|P
    Public Event CheckManagerStatus()
    Public Event WfStepsRefresh()
    Public Event ShowMsg(ByVal Msg As String)
    Private bolRunning As Boolean
    Private PService As New PService
    Private AService As New AService
    Private LastCheckPHourExecuted As Int32 = 24
    Private LastCheckPDayExecuted As Int32 = 32

    'Timers
    Dim TimerPlanificado As ZTimer
    Dim TimerActualizar As ZTimer
    Dim CBTimerP As New Threading.TimerCallback(AddressOf CheckP)
    Dim CBTimerA As New Threading.TimerCallback(AddressOf CheckA)
#End Region

#Region "Constructor"
    Public Sub New(ByVal WFIds As System.Collections.Generic.List(Of Int64), ByVal RefreshRate As Double, ByVal CurrentUserId As Int64, ByVal ServiceClearVariables As Boolean, ByVal TickHourString As String, ByVal TService As ClientType, ByVal WFServiceActExecute As String, ByVal WFServicePlanExecute As String, ByVal TimeStartT As Int32, ByVal TimeEndT As Int32, ByVal serviceID As Int64)
        Me.CurrentUserId = CurrentUserId
        If IsNothing(Me.WfServ) Then
            Me.WfServ = New WFService(WFIds, RefreshRate, serviceID)
        End If
        WfServ.RefreshRate = RefreshRate
        _ServiceClearVariables = ServiceClearVariables
        _TickHourString = TickHourString
        _TService = TService
        _WFServiceActExecute = WFServiceActExecute
        _WFServicePlanExecute = WFServicePlanExecute
        _TimeStartT = TimeStartT
        _TimeEndT = TimeEndT

        AddHandler PService.ShowMsg, AddressOf RaiseShowMsg
        AddHandler AService.ShowMsg, AddressOf RaiseShowMsg
    End Sub
#End Region

#Region "Check : A|C|E|M|P"
    Private Sub RaiseShowMsg(ByVal Msg As String)
        RaiseEvent ShowMsg(Msg)
    End Sub

    Public Sub CheckP(ByVal myState As Object)
        SyncLock PService
            SyncLock WfServ
                Try
                    RaiseEvent ShowMsg("Ejecutando Reglas de Planificacion")

                    Trace.WriteLineIf(ZTrace.IsInfo, "Horas planificacion: " & _TickHourString)
                    RaiseEvent ShowMsg("Horas planificacion: " & _TickHourString)
                    Dim TickHours() As String = _TickHourString.Split(Char.Parse(","))

                    Trace.WriteLineIf(ZTrace.IsInfo, "LastCheckHour: " & LastCheckPHourExecuted)
                    Trace.WriteLineIf(ZTrace.IsInfo, "LastCheckDay: " & LastCheckPDayExecuted)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Comparando con hora: " & DateTime.Now.Hour)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Comparando con dia: " & DateTime.Now.Day)

                    For Each S As String In TickHours
                        Trace.WriteLineIf(ZTrace.IsInfo, "Hora planificacion a comparar: " & S)
                        If (IsNumeric(S) AndAlso (Int32.Parse(S) <> LastCheckPHourExecuted Or LastCheckPDayExecuted <> DateTime.Now.Day) AndAlso Int32.Parse(S) = DateTime.Now.Hour) Then
                            Trace.WriteLineIf(ZTrace.IsInfo, "Ejecutando servicio")
                            LastCheckPHourExecuted = Int32.Parse(S)
                            LastCheckPDayExecuted = Now.Day

                            Me.CheckPState = ServiceState.Running
                            WfServ.DateLastRefresh = Now.ToString()
                            Trace.WriteLineIf(ZTrace.IsInfo, "Hora de comienzo: " & DateTime.Now)
                            RaiseEvent ShowMsg("Chequeando " & WfServ.WFIds.Count & " WorkFlows")
                            PService.ExecuteService(WfServ, CurrentUserId)
                        End If
                    Next
                Catch ex As Exception
                    raiseerror(ex)
                End Try
                RaiseEvent ShowMsg("Fin Reglas de Planificacion")
            End SyncLock
        End SyncLock
        GC.Collect()
        'Se comenta para ver si esto es lo que causa el "cuelgue" del servicio.
        'El problema es que el servicio queda como iniciado pero sin actividad.
        'La llamada a este método es probable que tenga que ver según estos artículos:
        '- http://msdn.microsoft.com/en-us/library/system.gc.waitforpendingfinalizers.aspx
        '- http://answers.unity3d.com/questions/386803/how-do-you-free-a-manually-created-texture.html
        'GC.WaitForPendingFinalizers()
    End Sub

    ''' <summary>
    ''' Ejecuta las reglas de actualizacion para determinados WF's
    ''' </summary>
    ''' <history>Marcelo Modified 10/12/2008</history>
    ''' <param name="myState"></param>
    ''' <remarks></remarks>
    Public Sub CheckA(ByVal myState As Object)
        'Evita que multiples hilos se encolen cuando uno de 
        'ellos se encuentra realizando una carga pesada
        If Monitor.TryEnter(AService) Then
            Try
                'SyncLock (AService)
                SyncLock (WfServ)
                    Try
                        If TimerActualizar IsNot Nothing Then TimerActualizar.Pause()
                        DirectCast(myState, WFServiceBusiness).CheckAState = ServiceState.Running
                        WfServ.DateLastRefresh = Now.ToString()

                        RaiseEvent ShowMsg("__________________________________________________________________")
                        RaiseEvent ShowMsg("Comienza un nuevo ciclo de ejecución de reglas de actualización")
                        RaiseEvent ShowMsg("Verificando " & WfServ.WFIds.Count & " workflows")
                        Trace.WriteLineIf(ZTrace.IsInfo, "_______________________________________________________________________________________________________")
                        Trace.WriteLineIf(ZTrace.IsInfo, "# Comienzo de un nuevo ciclo de ejecución de reglas de actualización (" & DateTime.Now & ")")

                        If _ServiceClearVariables = True Then
                            Trace.WriteLineIf(ZTrace.IsInfo, "Limpiando variables")
                            VariablesInterReglas.Clear()
                            Trace.WriteLineIf(ZTrace.IsInfo, "Variables limpiadas con exito")
                        End If

                        AService.ExecuteService(WfServ, CurrentUserId)

                        RaiseEvent ShowMsg("Fin Ejecucion Reglas de Actualizacion")
                        Trace.WriteLineIf(ZTrace.IsInfo, "# Finaliza el ciclo de ejecución. Tiempo de ejecucion: " & (DateTime.Now - WfServ.DateLastRefresh).ToString)
                        Trace.WriteLineIf(ZTrace.IsInfo, "_______________________________________________________________________________________________________")
                    Catch ex As Exception
                        raiseerror(ex)
                    End Try

                    If oldClientType <> ClientType.Desktop Then
                        Try
                            RaiseEvent CheckManagerStatus()
                        Catch ex As Exception
                            raiseerror(ex)
                        End Try
                    End If

                    If TimerActualizar IsNot Nothing Then
                        TimerActualizar.Resume()
                    End If
                End SyncLock
                'End SyncLock
            Catch ex As Exception
                raiseerror(ex)
            Finally
                'Sin estas dos lineas, el servicio no vuelve a ser ejecutado
                Monitor.Exit(AService)
                'Monitor.Exit(WfServ)

                GC.Collect()
                'Se comenta para ver si esto es lo que causa el "cuelgue" del servicio.
                'El problema es que el servicio queda como iniciado pero sin actividad.
                'La llamada a este método es probable que tenga que ver según estos artículos:
                '- http://msdn.microsoft.com/en-us/library/system.gc.waitforpendingfinalizers.aspx
                '- http://answers.unity3d.com/questions/386803/how-do-you-free-a-manually-created-texture.html
                'GC.WaitForPendingFinalizers()
            End Try
        End If
    End Sub
#End Region

#Region "Public Functions"
    ''' <summary>
    ''' Modifica el lapso de tiempo en que el servicio se refresca
    ''' </summary>
    ''' <param name="interval">Valor en minutos. También puede expresarse en segundos con valores entre el 0 y 1.</param>
    ''' <remarks></remarks>
    Public Sub RefreshRateChanged(ByVal interval As Double)
        Try
            WfServ.RefreshRate = interval
            If TimerActualizar Is Nothing = False Then TimerActualizar.Change(0, interval * 60000)
            If TimerPlanificado Is Nothing = False Then TimerPlanificado.Change(0, interval * 60000)
            WfServ.DateReStarted = Now.ToString()
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

    Public Sub StartService() Implements IService.StartService
        Try
            'guardo el tipo anterior para restaurarlo cuando se pare el servicio
            oldClientType = Membership.MembershipHelper.ClientType

            Membership.MembershipHelper.ClientType = _TService

            Trace.WriteLineIf(ZTrace.IsInfo, "-------------------------")
            Trace.WriteLineIf(ZTrace.IsInfo, "Dentro de StartService")
            Trace.WriteLineIf(ZTrace.IsInfo, "Hora de comienzo del servicio: " & DateTime.Now)
            WfServ.ServiceType = _TService
            Trace.WriteLineIf(ZTrace.IsInfo, "Tipo de servicio: " & _TService)

            Trace.WriteLineIf(ZTrace.IsInfo, "Actualizacion: " & _WFServiceActExecute)
            If _WFServiceActExecute = True Then
                Me.TimerActualizar = New ZTimer(CBTimerA, Me, 0, WfServ.RefreshRate * 60000, _TimeStartT, _TimeEndT)
            End If

            Trace.WriteLineIf(ZTrace.IsInfo, "Planificacion: " & _WFServicePlanExecute)
            If _WFServicePlanExecute = True Then
                Me.TimerPlanificado = New ZTimer(CBTimerP, Me, 0, WfServ.RefreshRate * 60000, _TimeStartT, _TimeEndT)
            End If

            Running = True
            WfServ.DateStarted = Now.ToString()

            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(WfServ.ServiceType, ObjectTypes.Services, RightsType.Iniciar, "Iniciando servicio " & WfServ.ServiceType.ToString() & "(" & WfServ.ServiceID & ")")
        Catch ex As Threading.SynchronizationLockException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadAbortException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadInterruptedException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadStateException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            Running = False
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Detiene el servicio
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StopService() Implements IService.StopService
        Trace.WriteLineIf(ZTrace.IsInfo, "-------------------------------------------")
        Trace.WriteLineIf(ZTrace.IsInfo, "Dentro de StopService")

        Try
            'recupero el tipo anterior
            Membership.MembershipHelper.ClientType = oldClientType

            Running = False
            WfServ.DateStoped = Now.ToString()

            'Libera los recursos de Planificacion
            If TimerPlanificado Is Nothing = False Then
                Me.TimerPlanificado.Pause()
                Me.TimerPlanificado.Dispose()
                Me.TimerPlanificado = Nothing
            End If
            Me.CheckPState = ServiceState.Stopped
            CBTimerP = Nothing
            PService = Nothing

            'Libera los recursos de Actualizacion
            If TimerActualizar Is Nothing = False Then
                Me.TimerActualizar.Pause()
                Me.TimerActualizar.Dispose()
                Me.TimerActualizar = Nothing
            End If
            Me.CheckAState = ServiceState.Stopped
            CBTimerA = Nothing
            AService = Nothing

            Trace.WriteLineIf(ZTrace.IsInfo, "Timer detenido.")
            RaiseEvent ShowMsg("Se detiene timer de servicio de workflow.")

            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(WfServ.ServiceType, ObjectTypes.Services, RightsType.Terminar, "Deteniendo servicio " & WfServ.ServiceType.ToString() & "(" & WfServ.ServiceID & ")")
        Catch ex As Threading.SynchronizationLockException
            ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadAbortException
            ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadInterruptedException
            ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadStateException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Trace.WriteLineIf(ZTrace.IsInfo, "Tiempo de detención: " & DateTime.Now)
    End Sub
#End Region

#Region "Dispose"

    Public Overridable Sub Dispose() Implements IDisposable.Dispose
        Try
            Me.StopService()
        Catch ex As StackOverflowException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

End Class


Public Class AService
    Public Event ShowMsg(ByVal Msg As String)
    Private lstWFs As New Dictionary(Of Int64, Dictionary(Of Int64, String))

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="WFService"></param>
    ''' <remarks>
    ''' <history>
    ''' [Ezequiel] 14/09/09 - Modified - Se cambio el dictionary por un hash en el cual se guarda un array donde su primer elemento es
    '''                                  la fecha de modificacion del wf y el segundo sus etapas.
    ''' </history>
    '''</remarks>
    Private Sub InitializeWF(ByVal WFService As WFService)
        Dim currentWFStep As IWFStep
        Dim stepId As Int64
        Dim lstStepsToRemove As New List(Of Int64)

        For Each WFId As Int64 In WFService.WFIds
            'todo: verificar si el wf cambio, validando la fecha de modificacion con respecto de la de carga.
            'Guardo en el hashtable un array donde el primer elemento es la fecha de modificacion y el segundo las etapas del wf.
            'TODO2: Se comenta todo lo relacionado al uso del array de fecha y objeto, ya que para obtener la fecha debe 
            'cargar el workflow y pierde performance. Cuando realmente se implemente solo hay que descomentar.
            If Not lstWFs.ContainsKey(WFId) Then
                lstWFs.Add(WFId, WFStepBusiness.GetStepsIdAndNamebyWorkflowId(WFId))

                For Each Wfstep As KeyValuePair(Of Int64, String) In lstWFs.Item(WFId)
                    Try
                        stepId = Wfstep.Key

                        'Verifica si la etapa contiene reglas para ejecutar
                        If WFStepBusiness.CheckWfServiceUsage(stepId) Then
                            Log("Cargando la definición de la etapa " & Wfstep.Value & " (" & stepId.ToString & ")")
                            currentWFStep = WFStepBusiness.GetStepById(stepId)
                        Else
                            Log("Descartando la etapa " & Wfstep.Value & " (" & stepId.ToString & ")")
                            lstStepsToRemove.Add(stepId)
                        End If
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                        Log("Ocurrió un error al cargar la etapa.")
                    End Try
                Next

                'Se dejan unicamente las etapas a procesar para no generar carga extra
                For Each id As Int64 In lstStepsToRemove
                    If lstWFs.Item(WFId).ContainsKey(id) Then
                        lstWFs.Item(WFId).Remove(id)
                    End If
                Next
                lstStepsToRemove.Clear()

            End If
        Next

        currentWFStep = Nothing
        lstStepsToRemove.Clear()
        lstStepsToRemove = Nothing
    End Sub

    ''' <summary>
    ''' Ejecuta el servicio de Actualizacion
    ''' </summary>
    ''' <param name="WFService">Clase del servicio</param>
    ''' <remarks></remarks>
    Public Sub ExecuteService(ByVal wfService As WFService, ByVal userId As Int64)
        If wfService Is Nothing Then
            Dim ex As New ArgumentNullException("WFService", "La instancia del servicio se encontraba en nulo y no podrá ser ejecutado.")
            Zamba.Core.ZClass.raiseerror(ex)
            Log("La instancia del servicio se encontraba en nulo y no podrá ser ejecutado.")

        Else
            InitializeWF(wfService)

            Try
                'La llamada al metodo UserBusiness.GetUserById(CurrentUserId) de abajo solo se utiliza para
                'cargar el membership para que el metodo updateServiceDate pueda funcionar sin inconvenientes
                If Membership.MembershipHelper.CurrentUser Is Nothing Then
                    Membership.MembershipHelper.SetCurrentUser(UserBusiness.GetUserById(userId))
                End If
                ServiceBusiness.UpdateServiceDate(wfService.ServiceID, ServiceTypes.Workflow)
            Catch ex As Exception
                raiseerror(ex)
            End Try

            Dim WFRS As New WFRulesBusiness
            Dim lstSteps As Dictionary(Of Int64, String) = Nothing
            Dim currentWFStep As IWFStep = Nothing
            Dim DVDSRules As DataView = Nothing
            Dim wftb As New WFTaskBusiness
            Dim tasks As List(Of ITaskResult) = Nothing
            Dim taskIds As List(Of Int64) = Nothing
            Dim rule As IWFRuleParent = Nothing
            Dim list As List(Of ITaskResult) = Nothing
            Dim i As Int32
            Dim executed As Boolean = False

            For Each stepInfo As Dictionary(Of Int64, String) In lstWFs.Values
                lstSteps = stepInfo
                Log("Verificando " & lstSteps.Count & " etapas.")

                For Each wfStep As KeyValuePair(Of Int64, String) In lstSteps
                    Try
                        currentWFStep = Zamba.Core.WFStepBusiness.GetStepById(wfStep.Key)
                        If Not IsNothing(currentWFStep.DSRules) Then

                            Log("Verificando etapa: " & wfStep.Value)
                            For Each R As DataRow In currentWFStep.DSRules.WFRules.Select("ParentType = 8")

                                rule = WFRulesBusiness.GetInstanceRuleById(R("Id"), currentWFStep.ID, True)
                                Log("Ejecutando regla: " & rule.Name & " (" & rule.ID.ToString & ")")

                                If WFRulesBusiness.IsExecutionTaskByTask(rule.WFStepId, rule.ID, True) Then
                                    If taskIds Is Nothing Then
                                        taskIds = wftb.GetTaskIdsByStepId(rule.WFStepId)
                                        If taskIds IsNot Nothing Then
                                            Log("Procesando " & taskIds.Count & " tareas")
                                        Else
                                            Exit For
                                        End If
                                    End If

                                    For i = 0 To taskIds.Count - 1
                                        Dim task As ITaskResult = Nothing

                                        Try
                                            If WFTaskBusiness.LockTask(taskIds(i)) Then
                                                task = WFTaskBusiness.GetTask(taskIds(i))

                                                If task Is Nothing Then
                                                    Log("No se encontraron datos de la tarea con ID " & taskIds(i).ToString)
                                                    Continue For
                                                End If
                                                If task.StepId <> currentWFStep.ID Then
                                                    Log("La tarea fue distribuida a otra etapa y no será procesada")
                                                    Continue For
                                                End If
                                                If task.TaskState = TaskStates.Ejecucion AndAlso task.AsignedToId <> userId Then
                                                    Log("La tarea se encontraba en ejecución por otro usuario y no será procesada")
                                                    Continue For
                                                End If
                                                Log("Procesando tarea: " & task.Name)

                                                'Inicia la tarea para evitar que otros usuarios la ejecuten
                                                task.TaskState = TaskStates.Servicio
                                                WFTasksFactory.UpdateTaskState(task)
                                                executed = True

                                                list = New List(Of ITaskResult)
                                                list.Add(task)
                                                list = WFRS.ExecutePrimaryRule(rule, list, Nothing)
                                            End If
                                        Catch ex As Exception
                                            raiseerror(ex)
                                            If task IsNot Nothing Then
                                                Log("Error en la ejecución de la tarea: " & task.Name & " (ID " & task.ID & ")")
                                            Else
                                                Log("Error en la ejecución de la tarea")
                                            End If

                                        Finally
                                            If task IsNot Nothing Then
                                                If executed AndAlso task.TaskState = TaskStates.Servicio Then
                                                    executed = False
                                                    Try
                                                        If task.AsignedToId > 0 Then
                                                            task.TaskState = TaskStates.Asignada
                                                        Else
                                                            task.TaskState = TaskStates.Desasignada
                                                        End If
                                                        WFTasksFactory.UpdateTaskState(task)
                                                    Catch ex As Exception
                                                        raiseerror(ex)
                                                    End Try
                                                End If
                                                WFTaskBusiness.UnLockTask(task.TaskId)
                                                task.Dispose()
                                                task = Nothing
                                            End If
                                        End Try
                                    Next

                                Else

                                    If tasks Is Nothing Then
                                        tasks = wftb.GetTasksByStepId(wfStep.Key, False, userId, 0, 0)
                                        If tasks IsNot Nothing Then
                                            Log("Procesando " & tasks.Count & " tareas")
                                        Else
                                            Exit For
                                        End If
                                    End If

                                    list = WFRS.ExecutePrimaryRule(rule, tasks, Nothing)

                                End If
                            Next

                            If tasks IsNot Nothing Then
                                For i = 0 To tasks.Count - 1
                                    If Not IsNothing(tasks(i)) Then
                                        tasks(i).Dispose()
                                        tasks(i) = Nothing
                                    End If
                                Next
                                tasks.Clear()
                                tasks = Nothing
                            End If
                            If taskIds IsNot Nothing Then
                                taskIds.Clear()
                                taskIds = Nothing
                            End If
                        End If
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                        Log("Ocurrió un error en la ejecución de la tarea.")
                    End Try
                Next
            Next

            Try
                wftb = Nothing
                WFRS = Nothing
                If currentWFStep IsNot Nothing Then
                    currentWFStep.Dispose()
                    currentWFStep = Nothing
                End If
                If tasks IsNot Nothing Then
                    For i = 0 To tasks.Count - 1
                        If Not IsNothing(tasks(i)) Then
                            tasks(i).Dispose()
                            tasks(i) = Nothing
                        End If
                    Next
                    tasks.Clear()
                    tasks = Nothing
                End If
                If taskIds IsNot Nothing Then
                    taskIds.Clear()
                    taskIds = Nothing
                End If
                'A los siguientes objetos no aplicarles clear o dispose:
                'If lstSteps IsNot Nothing Then
                '    lstSteps.Clear()
                '    lstSteps = Nothing
                'End If
                'If list IsNot Nothing Then
                '    list.Clear()
                '    list = Nothing
                'End If
                'If rule IsNot Nothing Then
                '    rule.Dispose()
                '    rule = Nothing
                'End If
                rule = Nothing
                list = Nothing
                lstSteps = Nothing
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                Log("Ocurrió un error al liberar la memoria del ciclo de ejecución.")
            End Try
        End If
    End Sub

    Private Sub Log(message As String)
        Trace.WriteLineIf(ZTrace.IsInfo, message)
        RaiseEvent ShowMsg(message)
    End Sub
End Class

Public Class PService
    Public Event ShowMsg(ByVal Msg As String)

    Dim lstWFs As New Hashtable

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="WFService"></param>
    ''' <remarks>
    ''' <history>
    ''' [Ezequiel] 14/09/09 - Modified - Se cambio el dictionary por un hash en el cual se guarda un array donde su primer elemento es
    '''                                  la fecha de modificacion del wf y el segundo sus etapas.
    ''' </history>
    '''</remarks>
    Private Sub InitializeWF(ByVal WFService As WFService)
        For Each WFId As Int64 In WFService.WFIds
            'todo: verificar si el wf cambio, validando la fecha de modificacion con respecto de la de carga.
            'Guardo en el hashtable un array donde el primer elemento es la fecha de modificacion y el segundo las etapas del wf.
            If Not lstWFs.ContainsKey(WFId) Then
                lstWFs.Add(WFId, New Object() {WFBusiness.GetWorkFlow(WFId).EditDate, WFStepBusiness.GetStepsIdAndNamebyWorkflowId(WFId)})
            End If
            RaiseEvent ShowMsg("Cargando " & lstWFs.Item(WFId)(1).Count & " Etapas del WF " & WFId)
            For Each Wfstep As KeyValuePair(Of Int64, String) In lstWFs.Item(WFId)(1)
                Try
                    'analizar si conviene cargarlas al inicio o a medida que se necesitan
                    Dim currentWFStep As IWFStep = Zamba.Core.WFStepBusiness.GetStepById(Wfstep.Key)
                    'WFRulesBusiness.GetCompleteHashTableRulesByStep(WFStep.Key, False)
                Catch ex As StackOverflowException
                    Zamba.Core.ZClass.raiseerror(ex)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Ocurrió un error al cargar la etapa.")
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Ocurrió un error al cargar la etapa.")
                End Try
            Next
        Next

    End Sub

    ''' <summary>
    ''' Ejecuta el servicio de Planificacion
    ''' </summary>
    ''' <param name="WFService">Clase del servicio</param>
    ''' <remarks></remarks>
    Public Sub ExecuteService(ByVal WFService As WFService, ByVal CurrentUserId As Int64)
        InitializeWF(WFService)

        Try
            ServiceBusiness.UpdateServiceDate(WFService.ServiceID, ServiceTypes.Workflow)
        Catch ex As Exception
            raiseerror(ex)
        End Try
        Dim wftb As New WFTaskBusiness
        For Each ar As Object In lstWFs.Values
            Dim lstSteps As Dictionary(Of Int64, String) = ar(1)
            RaiseEvent ShowMsg("Chequeando " & lstSteps.Count & " Etapas")

            For Each Wfstep As KeyValuePair(Of Int64, String) In lstSteps
                Try
                    Dim currentWFStep As IWFStep = Zamba.Core.WFStepBusiness.GetStepById(Wfstep.Key)
                    'Dim Rules As DsRules = WFRulesBusiness.GetCompleteHashTableRulesByStep(WFStep.Key)
                    If Not IsNothing(currentWFStep.DSRules) Then
                        Dim DVDSRules As New DataView(currentWFStep.DSRules.WFRules)
                        DVDSRules.RowFilter = "ParentType = 9"
                        Dim PRules As DataTable = DVDSRules.ToTable

                        If PRules.Rows.Count > 0 Then
                            Dim tasks As List(Of ITaskResult) = wftb.GetTasksByStepId(Wfstep.Key, False, CurrentUserId, 0, 0)

                            RaiseEvent ShowMsg("Chequeando Etapa: " & Wfstep.Value & " con " & tasks.Count & " Tareas")
                            Dim WFRS As New WFRulesBusiness

                            For Each R As DataRow In PRules.Rows
                                Dim rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(R("Id"), currentWFStep.ID, True)
                                'For Each Rule As IWFRuleParent In PRules.Rows

                                RaiseEvent ShowMsg("Ejecutando Regla: " & rule.Name)
                                Dim list As List(Of ITaskResult) = WFRS.ExecutePrimaryRule(rule, tasks, Nothing)
                                list = Nothing
                            Next

                            If Not IsNothing(tasks) Then
                                Dim i As Int32
                                For i = 0 To tasks.Count - 1
                                    If Not IsNothing(tasks(i)) Then
                                        tasks(i).Dispose()
                                        tasks(i) = Nothing
                                    End If
                                Next
                                tasks.Clear()
                                tasks = Nothing
                            End If
                            WFRS = Nothing
                        End If
                    Else
                        RaiseEvent ShowMsg("No hay reglas para la etapa: " & Wfstep.Value)
                    End If
                Catch ex As StackOverflowException
                    Zamba.Core.ZClass.raiseerror(ex)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Ocurrió un error en la ejecución de la tarea.")
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Ocurrió un error en la ejecución de la tarea.")
                End Try
            Next

        Next
        wftb = Nothing
    End Sub
End Class