Imports Zamba.Core.WF.WF
Imports Zamba.Core.Enumerators
Imports Zamba.Data
Imports System.Reflection
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Text
Imports Zamba.Core.Caching
Imports System.Web.UI
Imports Zamba.Core.Cache
Imports Zamba.Servers
Imports Zamba.Membership
Imports System.Runtime.InteropServices

Public Class WFRulesBusiness
    Implements IWFRuleBusiness
    Dim WFTB As New WFTaskBusiness
    Dim WF As New WFFactory
    Dim UserBusiness As New UserBusiness
    Dim UserGroupBusiness As New UserGroupBusiness
#Region "Constantes"
    Private Const RULE_ID As String = "Id"
    Private Const RULE_NAME As String = "Name"
    Private Const RULE_STEP_ID As String = "step_Id"
    Private Const RULE_TYPE As String = "Type"
    Private Const RULE_PARENT_ID As String = "ParentId"
    Private Const RULE_PARENT_TYPE As String = "ParentType"
    Private Const RULE_ENABLED As String = "Enable"
    Private Const RULE_VERSION As String = "Version"
#End Region


#Region "Get"
    Public Function GetCompleteHashTableRulesByStep(ByVal stepid As Int64,
                                                    Optional ByVal typeofRule As TypesofRules = TypesofRules.Todas) As List(Of IWFRuleParent)

        Dim DsRule As DataSet = GetDSRulesByStepId(stepid)

        If Not DsRule Is Nothing Then
            Dim rulesRows() As DataRow
            If typeofRule = TypesofRules.Todas Then
                rulesRows = DsRule.Tables("WFRules").Select("ParentId=0 and step_Id=" & stepid)
            Else
                rulesRows = DsRule.Tables("WFRules").Select("ParentId=0 and step_Id=" & stepid & " and Type=" & typeofRule)
            End If

            Dim ruleId As Int64
            Dim rules As New List(Of IWFRuleParent)

            For Each r As DataRow In rulesRows
                Try
                    Dim rule As WFRuleParent
                    ruleId = CLng(r.Item("Id").ToString)


                    rule = ReturnInstanceRule(DsRule.Tables("WFRuleParamItems"), r, r("ParentType"))

                    If rule IsNot Nothing Then
                        rules.Add(rule)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Next

            rulesRows = Nothing
            Return rules
        End If

    End Function

    Public Function GetDSRulesByRuleId(ByVal ruleid As Int64) As DataSet

        If Not Rules.lsRulesDSRules.ContainsKey(ruleid) Then
            Dim DsRule As DataSet = WFRulesFactory.GetRuleParamItems(ruleid)

            If Not DsRule Is Nothing Then
                SyncLock Rules.lsRulesDSRules
                    If Not Rules.lsRulesDSRules.ContainsKey(ruleid) Then
                        Rules.lsRulesDSRules.Add(ruleid, DsRule)
                    End If
                End SyncLock
            End If
        End If

        Return Rules.lsRulesDSRules(ruleid)
    End Function

    Public Function GetDSRulesByStepId(ByVal stepid As Int64) As DataSet

        If Not Rules.lsStepDSRules.ContainsKey(stepid) Then
            Dim DsRule As DataSet = WFRulesFactory.GetRulesByWFStepID(stepid)

            If Not DsRule Is Nothing Then
                SyncLock Rules.lsStepDSRules
                    If Not Rules.lsStepDSRules.ContainsKey(stepid) Then
                        Rules.lsStepDSRules.Add(stepid, DsRule)
                    End If
                End SyncLock
            End If
        End If

        Return Rules.lsStepDSRules(stepid)
    End Function
    ''' <summary>
    ''' LImpia los HashTables
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub ClearHashTables()
        Cache.Rules.GetInstance().ClearAll()
    End Sub

    Public Function GetRulesByTask(ByVal taskId As Int64, ByVal ruleType As TypesofRules) As List(Of IWFRuleParent)
        Dim Rules As New List(Of IWFRuleParent)
        Return Rules
    End Function

    Public Overloads Function GetDoExecuteRules() As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = WFRulesFactory.GetDoExecuteRules()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return ds
    End Function

    Public Function GetRulesByStepId(ByVal stepid As Int64) As DataSet
        'Dim ds As DataSet = Nothing
        Return WFRulesFactory.GetRulesByStepId(stepid)
    End Function


    '[Ezequiel] - 4/5/11 - Metodo el cual devuelve un diccionario con todas las reglas de una etapa
    Public Function GetRulesByStepId(ByVal stepidlist As List(Of Int64)) As List(Of IWFRuleParent)
        Dim rulelist As New List(Of IWFRuleParent)
        Dim stepids As String

        For Each id As Int64 In stepidlist
            stepids = stepids & ", " & id.ToString
        Next

        Dim dswfrules As DataSet = WFRulesFactory.GetRulesByStepIds(stepids.Substring(1))
        Dim dsparamrules As DataSet = WFRulesFactory.GetRulesParamItemsByStepIds(stepids.Substring(1))

        If Not dswfrules Is Nothing Then
            Dim rulesRows() As DataRow = dswfrules.Tables(0).Select("ParentId=0")
            Dim rule As WFRuleParent
            For Each r As DataRow In rulesRows
                Try
                    rule = ReturnInstanceRule(dsparamrules.Tables(0), r, r.Item("ParentType"))

                    If Not rule Is Nothing Then
                        rulelist.Add(rule)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Next
        End If
        Return rulelist
    End Function

    ''' <summary>
    ''' Obtiene las reglas para un Workflow
    ''' </summary>
    ''' <param name="WF">Id del WorkFlow que se quiere conocer las Reglas</param>
    ''' <returns>Objeto DataSet</returns>
    ''' <history>[Alejandro]</history>
    Public Overloads Function GetRulesByWorkFlowIDAsDataSet(ByVal _workFlowID As Int64) As DataSet
        Dim ds As DataSet = Nothing
        Dim stepsList As New List(Of IWFStep)
        Dim Restriction As String = Nothing

        Try
            stepsList = WFStepBusiness.GetStepsByWorkflow(_workFlowID)
            Restriction = "("
            For Each s As WFStep In stepsList
                Restriction += "wfrules.step_id=" & s.ID & " or "
            Next
            ds = WFRulesFactory.GetRulesAsDataSet(Restriction)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return ds

    End Function



    ''' <summary>
    ''' Obtiene una regla de workflow instanciada.  
    ''' </summary>
    ''' <param name="p_iRuleId">id de la regla.</param>
    ''' <param name="p_WfStep">Etapa del workflow.</param>
    ''' <returns>WFRuleParent</returns>
    ''' <remarks></remarks>
    Public Function GetInstanceRuleById(ByVal ruleId As Int64) As IWFRuleParent Implements IWFRuleBusiness.GetInstanceRuleById
        Dim rule As IWFRuleParent = Nothing


        Dim dsRule As DataSet = Nothing
        Dim zoptb As New ZOptBusiness

        Try
            ' ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo Reglas de la Etapa = " & stepId)

            dsRule = GetDSRulesByRuleId(ruleId)

            rule = ReturnInstanceRule(dsRule.Tables("WFRuleParamItems"), dsRule.Tables("WFRules").Rows(0), TypesofRules.Regla)

            If rule Is Nothing Then
                Throw New Exception(String.Format("No se pudo obtener la Regla con Id {0}", ruleId))
            End If
        Catch ex As Exception
            ZClass.raiseerror(New Exception($"ERROR: al instanciar la regla con id: {ruleId}", ex))
        Finally
            zoptb = Nothing
            If dsRule IsNot Nothing Then
                dsRule.Dispose()
                dsRule = Nothing
            End If
        End Try
        Return rule
    End Function

    ''' <summary>
    ''' Ejecuta una regla a partir de el id, el id de la etapa y los results. El path de las dll lo toma desde donde
    ''' se ejecute el programa
    ''' </summary>
    ''' <param name="ruleId">Id de la regla</param>
    ''' <param name="stepId">Id de la etapa</param>
    ''' <param name="results">TaskResults a ser ejecutados</param>
    ''' <returns>List(Of ITaskResult)</returns>
    ''' <remarks></remarks>
    Public Function ExecuteRule(ByVal ruleId As Int64, ByVal _TaskResults As List(Of ITaskResult), ByVal IsAsync As Boolean) As List(Of ITaskResult)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Directorio para buscar las dll de las reglas: " & Membership.MembershipHelper.StartUpPath)
        Return ExecuteRule(ruleId, _TaskResults, Membership.MembershipHelper.StartUpPath, IsAsync)
    End Function

    ''' <summary>
    ''' Ejecuta una regla en modo Web
    ''' </summary>
    ''' <param name="ruleID">ID de la regla</param>
    ''' <param name="results"></param>
    ''' <param name="RulePendingEvent">Devuelve si hay algun evento pendiente</param>
    ''' <param name="ExecutionResult">Devuelve el estado de la ejecucion</param>
    ''' <param name="ExecutedIDs">Devuelve los IDs que se ejecutaron</param>
    ''' <param name="Params">Parametros de la regla (cuando vuelve por segunda vez)</param>
    ''' <param name="fileDirectory">Ruta de las dlls</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExecuteWebRule(ByVal ruleID As Int64, ByVal results As List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef ExecutedIDs As List(Of Int64), ByRef Params As Hashtable, ByRef PendingChildRules As List(Of Int64), ByRef RefreshRule As Boolean, ByVal TaskIdsToRefresh As List(Of Long), ByVal IsAsync As Boolean) As List(Of ITaskResult)
        Dim wfstepbuss As WFStepBusiness
        Dim rule As IWFRuleParent



        'Try
        'Instancio las variables
        If IsNothing(ExecutedIDs) Then
            ExecutedIDs = New List(Of Int64)
        End If
        RulePendingEvent = RulePendingEvents.NoPendingEvent
        ExecutionResult = RuleExecutionResult.NoExecution
        wfstepbuss = New WFStepBusiness()

        rule = GetInstanceRuleById(ruleID)
        rule.IsAsync = IsAsync
        Return ExecuteWebRule(rule, results, RulePendingEvent, ExecutionResult, ExecutedIDs, Params, PendingChildRules, RefreshRule, TaskIdsToRefresh, IsAsync)
        'Catch ex As Exception
        '    If Not IsNothing(PendingChildRules) Then
        '        PendingChildRules.Clear()
        '    End If
        '    Throw
        'Finally
        'Limpio las variables
        If Not IsNothing(wfstepbuss) Then
            wfstepbuss = Nothing
        End If
        'End Try
    End Function

    Private Function ExecuteWebRule(ByVal rule As WFRuleParent, ByVal results As List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents,
                                    ByRef ExecutionResult As RuleExecutionResult, ByRef ExecutedIDs As List(Of Int64), ByRef Params As Hashtable, ByRef PendingChildRules As List(Of Int64), ByRef RefreshRule As Boolean, ByRef TaskIdsToRefresh As List(Of Long), ByVal IsAsync As Boolean) As List(Of ITaskResult)
        'Si la regla ya se ejecuto, ejecuto los hijos
        '28/10/11:Se agrega pregunta para saber si la regla está habilitada.

        If (rule.ChildRulesIds Is Nothing OrElse rule.ChildRulesIds.Count = 0) Then
            rule.ChildRulesIds = GetChildRulesIds(rule.ID, rule.RuleClass, results)
        End If

        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Reglas Hijas obtenidas: {0}", rule.ChildRulesIds.Count))


        If ExecutedIDs.Contains(rule.ID) = False AndAlso rule.Enable Then


            If results.Count > 0 OrElse (results.Count = 0 AndAlso rule.ExecuteWhenResult = False) Then
                For Each T As TaskResult In results
                    AllObjects.Tarea() = T
                    Exit For
                Next

                AllObjects.Tareas = results


                Try

                    Dim newresults As New List(Of ITaskResult)

                    newresults = rule.ExecuteWebRule(results, WFTB, Me, RulePendingEvent, ExecutionResult, Params, IsAsync)

                    If Membership.MembershipHelper.CurrentUser IsNot Nothing AndAlso results IsNot Nothing AndAlso results.Count > 0 Then

                        Dim Result = results(0)
                        If rule.RuleType = TypesofRules.AccionUsuario Then

                            WFTB.LogTask(Result, "Ejecuto :" & rule.Name & $"({rule.ID})")

                        End If


                    End If

                    If (rule.RefreshRule.HasValue AndAlso rule.RefreshRule.Value = True) Then
                        RefreshRule = True
                        If (Not newresults Is Nothing AndAlso newresults.Count > 0) Then
                            If Not TaskIdsToRefresh Is Nothing AndAlso TaskIdsToRefresh.Contains(newresults(0).TaskId) = False Then
                                TaskIdsToRefresh.Add(newresults(0).TaskId)
                            End If
                        End If
                    End If

                    If ExecutionResult = RuleExecutionResult.FailedExecution OrElse ExecutionResult = RuleExecutionResult.PendingEventExecution Then
                        'Se agrega a la lista de ejecutados aunque se encuentre pendiente. 
                        'Se multiplica por -1 para no marcarlo como completamente ejecutado.
                        ExecutedIDs.Add(rule.ID * -1)
                        results = newresults
                    Else
                        'Una vez ejecutada la regla en su totalidad se agrega a la lista.
                        ExecutedIDs.Add(rule.ID)
                    End If

                    'PREFERENCIA DE REGLA PARA NOTIFICAR LUEGO DE LA EJECUCION
                    If rule.AlertExecution = True Then SendNotificationAlert(rule, results)
                    results = newresults
                Finally
                    'ZTrace.RemoveListener("Rule " & rule.RuleClass.ToString & " - " & rule.Name & " - " & rule.ID.ToString)
                End Try
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Rule Doesn´t have Tasks to Execute")
            End If

        Else
            Try
                ZTrace.WriteLineIf(ZTrace.IsInfo, "[X] La regla " & rule.ID.ToString & " no se ejecutó. REGLA YA EJECUTADA: " & ExecutedIDs.Contains(rule.ID).ToString & ". REGLA HABILITADA: " & rule.Enable.ToString)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If

        If ExecutionResult = RuleExecutionResult.FailedExecution Or ExecutionResult = ExecutionResult.PendingEventExecution Then
            Return results
        End If

        If (String.Compare(rule.RuleClass, "IfBranch", True) = 0 AndAlso results.Count = 0) Then
            'No se ejecuta la cadena hija, porque no hay resultados que hayan cumplido con la condicion y tipo de condicion
        Else
            '[AlejandroR] 01/03/2011 - verificar si la regla tiene configurado que no se ejecuten las hijas
            If Not rule.DisableChildRules.Value AndAlso rule.RuleClass <> "DoForEach" Then



                For Each RId As Int64 In rule.ChildRulesIds
                    ' ZTrace.GetInstance().IndentLevel = ZTrace.GetInstance().IndentLevel + 1
                    ZTrace.WriteLineIf(ZTrace.IsInfo, $"La regla {rule.Name} ({rule.ID}) ejecuta la regla hija Id: {RId}")

                    Dim R As WFRuleParent = GetInstanceRuleById(RId)
                    R.ParentRule = rule
                    R.IsAsync = IsAsync
                    'rule.ChildRules.Add(R)
                    Try
                        Dim newchildresults As New List(Of ITaskResult)()
                        newchildresults = ExecuteWebRule(R, results, RulePendingEvent, ExecutionResult, ExecutedIDs, Params, PendingChildRules, RefreshRule, TaskIdsToRefresh, IsAsync)
                        If IsNothing(newchildresults) Then
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("La Regla : {0} no ha devuelto tareas", R.ID))
                            results = Nothing
                        Else
                            'ML: Cancelacion de ifbranch proximo, si la cantidad de results devueltos por el primer ifbranch 
                            'es igual al ingresado, es decir que todas las tareas cumplieron con la primer condicion.
                            If String.Compare(R.RuleClass, "IfBranch", True) = 0 AndAlso newchildresults.Count = results.Count Then
                                Exit For
                            End If
                        End If
                        If ExecutionResult = RuleExecutionResult.FailedExecution Or ExecutionResult = ExecutionResult.PendingEventExecution Then
                            'Tomas: Se comenta porque los eventos pendientes deben ejecutarse sobre el nuevo set de resultados.
                            '       En el caso de tener una dogettask o dogetdocasoc y luego una dodistribuir, se trabajaba el
                            '       set de reglas pendientes con las tareas viejas en vez de las nuevas.
                            'Return results

                            'Javier: Si hay un evento pendiente se agregan las reglas hijas no ejecutadas para ejecutar luego

                            Dim ruls As List(Of Int64) = rule.ChildRulesIds

                            Dim execIds As List(Of Long) = ExecutedIDs
                            PendingChildRules.AddRange(From currRuleID As Int64 In rule.ChildRulesIds
                                                       Where currRuleID <> R.ID AndAlso Not execIds.Contains(currRuleID)
                                                       Select currRuleID)

                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Reglas hijas pendientes : {0}", String.Join("-", PendingChildRules)))
                            Return newchildresults
                        End If
                    Catch ex As Exception
                        '[Ezequiel] 08/06/09 - Valido si debo cortar la ejecucion de la tarea
                        ' en base a la regla que se esta ejecutando actualmente.
                        If R.ContinueWithError = False Then
                            If Not IsNothing(PendingChildRules) Then
                                PendingChildRules.Clear()
                            End If
                            '      ZTrace.GetInstance().IndentLevel = ZTrace.GetInstance().IndentLevel - 1
                            Throw New Exception("Se finaliza la ejecución de la tarea debido a un error en la regla: " & R.Name, ex)
                        Else
                            ZClass.raiseerror(ex)
                        End If
                    End Try
                    '   ZTrace.GetInstance().IndentLevel = ZTrace.GetInstance().IndentLevel - 1
                Next
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "- Child Rules Are Not Enabled -")
            End If

        End If

        Return results
    End Function

    ''' <summary>
    ''' Ejecuta una regla en forma web
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <param name="results"></param>
    ''' <param name="RulePendingEvent"></param>
    ''' <param name="ExecutionResult"></param>
    ''' <param name="ExecutedIDs"></param>
    ''' <param name="Params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>


    ''' <summary>
    ''' Ejecuta una regla a partir de su id, el id del step y la collecion de resultsa ejecutarse
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="stepId"></param>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExecuteRule(ByVal ruleId As Int64, ByVal _TaskResults As List(Of ITaskResult), ByVal rulesDirectory As String, ByVal IsAsync As Boolean) As List(Of ITaskResult)
        Dim CurrentRule As IWFRuleParent = Nothing
        Try

            CurrentRule = GetInstanceRuleById(ruleId)

            CurrentRule.IsAsync = IsAsync
            If IsNothing(CurrentRule) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al instanciar la regla")
                Throw New Exception("Error al instanciar la regla " & ruleId)
            End If

            'Martin: Se comento llamada directa a la regla, para utilizar metodos anidados de facil mantencion y unicos puntos de entrada
            Return ExecuteRule(CurrentRule, _TaskResults, IsAsync)
        Finally
            Try
                If Not IsNothing(CurrentRule) Then
                    CurrentRule.Dispose()
                    CurrentRule = Nothing
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Try

    End Function



    ''' <summary>
    ''' Se ejecuta la regla. Lo modificado es que se agrego el método ChangeStateTask para que se pase al nuevo estado cuando se ejecuta la regla
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <history> [Gaston] 14/04/2008 Modified 
    '''           [Diego] 28/07/2008 Modified 
    '''           [Javier] 23/07/2010 Modified - Se agrega refreshtimeout 
    '''           [Marcelo] - 30/10/2010 - Modified - Se modifica la llamada a la actualizacion del estado
    '''</history>
    ''' <remarks></remarks>
    Public Function ExecuteRule(ByVal rule As IWFRuleParent, ByVal results As List(Of ITaskResult), ByVal IsAsync As Boolean) As List(Of ITaskResult)
        Dim newresults As New List(Of ITaskResult)
        Dim WFTB As New WFTaskBusiness

        ' Se recupera de la base de datos el id del estado de la regla
        Dim dt As DataRow() = GetRuleOption(rule.ID, RuleSectionOptions.Estado, RulePreferences.StateTypeState)


        If (dt.Count > 0) Then
            Dim stateid As Int64 = Int64.Parse(dt(0).Item("ObjValue").ToString())
            ' Se recorren los results para asignarle a cada uno el nuevo estado, y de esta forma, colocarlo en el combobox de estados en el panel
            ' Tareas del cliente
            For Each result As ITaskResult In results
                Dim state As IWFStepState = WFStepStatesComponent.getStateFromList(stateid, WFStepStatesComponent.GetStepStatesByStepId(result.StepId))
                WFTB.ChangeState(result, state)
            Next
        End If

        'Esto es para texto inteligente
        AllObjects.Tareas = results

        'Verifica si debe ejecutar todas las tareas juntas o de a una.
        If results.Count = 1 OrElse IsExecutionTaskByTask(rule.ID) Then
            Dim tempNewResults As List(Of ITaskResult)
            Dim tempTaskResults As List(Of ITaskResult)

            'Se ejecuta la cadena de reglas completa por tarea.
            For Each task As ITaskResult In results

                AllObjects.Tarea = task

                tempTaskResults = New List(Of ITaskResult)
                tempTaskResults.Add(task)
                tempNewResults = rule.ExecuteRule(tempTaskResults, WFTB, Me, IsAsync) ' porque generaba aca la instancia de wfTaskBusiness??

                If tempNewResults IsNot Nothing Then
                    If newresults IsNot Nothing Then
                        newresults = New List(Of ITaskResult)
                    End If
                    'Se agrega la tarea recien ejecutada.
                    If tempNewResults.Count > 0 Then
                        newresults.Add(tempNewResults(0))
                    End If
                End If
            Next

            tempNewResults = Nothing
            tempTaskResults = Nothing
        Else
            'Se ejecuta el conjunto de tareas por cada regla de la cadena.
            For Each T As TaskResult In results
                AllObjects.Tarea = T
                Exit For
            Next

            newresults = rule.ExecuteRule(results, WFTB, Me, IsAsync) ' porque generaba aca la instancia de wfTaskBusiness??
        End If

        'PREFERENCIA DE REGLA PARA NOTIFICAR LUEGO DE LA EJECUCION
        If rule.AlertExecution Then SendNotificationAlert(rule, results)

        'salva la accion en la columna U_time de UCM para el manejo de la sesion y en user_hst para historial de acciones
        'UserBusiness.SaveAction(rule.ID, ObjectTypes.ModuleWorkFlow, RightsType.ExecuteRule, "usuario Ejecuto la regla " & rule.Name)
        WFTB = Nothing

        Return newresults
    End Function

    ''' <summary>
    ''' Verifica el modo de ejecución de la regla padre, con respecto a las tareas que recibe.
    ''' </summary>
    ''' <param name="ruleId">RuleId de la regla padre (Int64)</param>
    ''' <returns>True si debe ejecutar la cadena de reglas por cada regla.</returns>
    ''' <remarks></remarks>
    Public Function IsExecutionTaskByTask(ByVal ruleId As Int64) As Boolean
        'Obtiene la configuración de la regla.
        Dim dt As DataRow() = GetRuleOption(ruleId, RuleSectionOptions.Configuracion, RulePreferences.TaskExecutionMode, 1)

        'Verifica que existan datos.
        If dt IsNot Nothing Then
            'Verifica la opción seleccionada.
            If dt.Count > 0 Then
                If String.Compare(dt(0).Item("ObjValue").ToString, "0") = 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Sub ExecuteZopenRules(ByVal TasksDT As DataTable, ByVal StepId As Int64)
        Dim rules As List(Of IWFRuleParent)
        rules = GetCompleteHashTableRulesByStep(StepId)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtengo regla del Evento Abrir Zamba")
        Dim zopenrule As WFRuleParent
        For Each Rule As WFRuleParent In rules
            If Rule.RuleType = TypesofRules.AbrirZamba Then
                zopenrule = Rule
                Exit For
            End If
        Next

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecuto tareas")
        Dim WFSB As New WFStepBusiness

        Dim Wfstep As IWFStep = WFSB.GetStepById(StepId)
        WFSB = Nothing
        For Each CurrentRow As DataRow In TasksDT.Rows
            Try
                Dim task As TaskResult = New WFTaskBusiness().NewResult(CurrentRow, Wfstep.ID)
                Dim tasklst As New System.Collections.Generic.List(Of ITaskResult)
                tasklst.Add(task)
                ExecuteRule(zopenrule, tasklst, False)

            Catch ex As Exception
                ZClass.raiseerror(ex)
                MessageBox.Show("Ocurrio un error en la ejecución de reglas de la tarea." & vbCrLf & "Contactese con el administrador del sistema.", "Error en la ejecución", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Next
    End Sub

    ''' <summary>
    ''' Devuelve un listado de ids y nombres de reglas perteneciente a 1 etapa
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRulesIdsAndNames(ByVal stepId As Int64) As Dictionary(Of Int64, String)
        Dim dt As DataTable = WFRulesFactory.GetRulesIdsAndNames(stepId)
        Dim rules As New Dictionary(Of Int64, String)(dt.Rows.Count)
        For Each dr As DataRow In dt.Rows
            rules.Add(Int64.Parse(dr("Id").ToString()), dr("name").ToString())
        Next
        Return rules
    End Function

    ''' <summary>
    ''' obtiene el estado de una regla.
    ''' </summary>
    ''' <param name="p_iRuleId">id de la regla</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    'Public  Function GetRuleEstado(ByVal p_iRuleId As Int32) As Boolean
    '    Return WFRulesFactory.GetRuleStateById(p_iRuleId)
    'End Function

    Public Function GetIsRuleEnabled(ByVal UserRules As Hashtable, ByVal Rule As IWFRuleParent) As Boolean

        If Rule.Enable Then

            If UserRules.ContainsKey(Rule.ID) Then
                'Lista que en la posicion 0 guarda si esta habilitada la regla o no
                'y en la 1 si se acumula a la habilitacion de las solapas o no
                Dim lstRulesEnabled As List(Of Boolean) = DirectCast(UserRules(Rule.ID), List(Of Boolean))
                Return lstRulesEnabled(0)
            Else
                'Obtiene el estado
                Return GetRuleEstado(Rule.ID)
            End If
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Devuelve el estado de habilitación del estado, es decir si está habilitado o deshabilitado
    ''' </summary>
    ''' <param name="p_iRuleId">id de la regla</param>
    ''' <history>
    '''     <created> (pablo)
    ''' </history>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function GetStateOfHabilitationOfState(ByVal Rule As IWFRuleParent, ByVal StateID As Int64) As Boolean
        If WF.GetStateOfHabilitationOfState(Rule.ID, StateID) Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' obtiene el estado de una regla.
    ''' </summary>
    ''' <param name="p_iRuleId">id de la regla</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function GetRuleEstado(ByVal p_iRuleId As Int64) As Boolean
        Dim Enabled As Boolean
        Dim WFRF As New WFRulesFactory
        Try
            Return WFRF.GetRuleStateById(p_iRuleId)
        Finally
            WFRF = Nothing
        End Try
    End Function

    ''' <summary>
    '''  Obtiene el nombre de una regla.
    ''' </summary>
    ''' <param name="p_iRuleId">Id de regla</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>



    Public Function GetWFStepIdbyRuleID(ByVal ruleId As Int64) As Int64 Implements IWFRuleBusiness.GetWFStepIdbyRuleID
        If Cache.Workflows.hsWFStepId.Contains(ruleId) = False Then
            SyncLock (Cache.Workflows.hsWFStepId)
                Cache.Workflows.hsWFStepId.Add(ruleId, WFRulesFactory.GetWFStepIdbyRuleID(ruleId))
            End SyncLock
        End If
        Return Cache.Workflows.hsWFStepId(ruleId)
    End Function
    Public Function GetRuleNameById(ByVal p_iRuleId As Int64) As String

        Dim Rule As IWFRuleParent = GetInstanceRuleById(p_iRuleId)
        If Rule IsNot Nothing Then
            Return Rule.Name
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Obtiene todos los Usuario que se van a notificar sobre la ejecucion de la regla
    ''' </summary>
    ''' <param name="ruleid"></param>
    ''' <param name="RuleSection"></param>
    ''' <param name="_RulePreference"></param>
    ''' <param name="DestType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private DsStepNode As DataSet = New DataSet
    Public Function GetRuleOption(ByVal ruleid As Int64, ByVal RuleSection As RuleSectionOptions, ByVal _RulePreference As RulePreferences, ByVal DestType As Int32) As DataRow()
        'DestType = 1 ( Obtiene los usuarios a notificar)
        'DestType = 2 ( Obtiene los usuarios de un Grupo a notificar)
        'DestType = 3 ( Obtiene los Mails Externos que se puedan llegar a cargar)


        Dim dt As DataTable = Nothing

        If Not RulesOptions._DsRulesOptionsByRuleId.ContainsKey(ruleid) Then
            Dim ds As DataSet = WF.GetRulesPreferences(ruleid)
            If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 Then
                dt = ds.Tables(0)
                RulesOptions._DsRulesOptionsByRuleId.Add(ruleid, dt.Clone)
            Else
                Return Nothing
            End If
        Else
            dt = RulesOptions._DsRulesOptionsByRuleId(ruleid)
        End If



        Dim Rows As DataRow() = dt.Select("SectionId= " & RuleSection & " And ObjectId =" & _RulePreference & " And ObjValue = " & DestType)

        Return Rows

    End Function


    Public Function GetRuleOption(ByVal ruleid As Int64, ByVal RuleSection As RuleSectionOptions, ByVal _RulePreference As RulePreferences) As DataRow()
        If Cache.RulesOptions._DsRulesOptionsByRuleId.ContainsKey(ruleid) = False Then
            Dim WF As New WFRulesFactory()
            Dim dt As DataTable = WF.GetRulesOptionsDT(ruleid)
            SyncLock Cache.RulesOptions._DsRulesOptionsByRuleId
                If Cache.RulesOptions._DsRulesOptionsByRuleId.ContainsKey(ruleid) = False Then
                    Cache.RulesOptions._DsRulesOptionsByRuleId.Add(ruleid, dt)
                End If
            End SyncLock
            WF = Nothing
        End If

        Dim dto As DataTable = Cache.RulesOptions._DsRulesOptionsByRuleId(ruleid)

        If Not IsNothing(dto) AndAlso dto.Rows.Count = 0 Then
            dto.Rows.Add(New Object() {ruleid, Nothing, Nothing, Nothing, Nothing})
        End If

        Dim Rows As DataRow() = dto.Select("SectionId= " & RuleSection & " And ObjectId =" & _RulePreference)

        Return Rows

    End Function

#End Region

#Region "Fill"

    'Public  Function 
    'Friend  Sub FillRules(ByVal wfs() As WorkFlow)
    '    Try
    '        For Each wf As WorkFlow In wfs
    '            FillRules(wf, withcache)
    '        Next
    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try
    'End Sub

    Public Sub FillRules(ByVal wf As IWorkFlow)
        Try
            For Each s As WFStep In wf.Steps.Values
                FillRules(s)
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub FillRules(ByVal wfStep As IWFStep)
        Try
            Dim steplist As New List(Of Int64)
            steplist.Add(wfStep.ID)
            Dim listRule As List(Of IWFRuleParent) = GetRulesByStepId(steplist)

            If Not listRule Is Nothing Then
                SetRulesInstances(listRule, wfStep)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Function ContainsRule(ByRef wfstep As WFStep, ByRef wfrule As WFRuleParent) As IWFRuleParent
        For Each R As WFRuleParent In wfstep.Rules
            If R.ID = wfrule.ID Then
                Return R
            End If
        Next
        Return Nothing
    End Function

#End Region

#Region "Add"


    ''' <summary>
    ''' Modifica el nombre de la clase
    ''' </summary>
    ''' <param name="RuleId">Id de la regla</param>
    ''' <param name="ClassName">Nombre de la clase</param>
    ''' <history>   Marcelo 02/09/2009  Created</history>
    ''' <remarks></remarks>
    Public Sub updateClass(ByVal RuleId As Int64, ByVal ClassName As String)
        WFRulesFactory.updateClass(RuleId, ClassName)
    End Sub

    Private Function GetNewRule(ByVal BaseNode As BaseWFNode, ByVal RuleNameFromUser As String, ByVal RuleName As String, ByVal TypeOfRule As TypesofRules) As WFRuleParent
        'Dim t As System.Type = GetType(WFBusiness).Assembly.GetType("Zamba.WFBusiness." &  True, True)
        Dim tt As Assembly = Assembly.LoadFile(Application.StartupPath & "\Zamba.WFActivity.Regular.dll")
        Dim t As System.Type = tt.GetType("Zamba.WFActivity.Regular." & RuleName, True, True)
        Dim c As ConstructorInfo = t.GetConstructors.GetValue(0)
        Dim Args(c.GetParameters.Length - 1) As Object

        'Primeros dos parametros Standarts del Ctor
        Args(0) = CoreData.GetNewID(IdTypes.WFRule)
        'todo: ver de preguntar por el nombre o tomar el nombre basico de maskname
        Args(1) = RuleNameFromUser
        Args(2) = GetStepId(BaseNode)
        FillArgs(Args, c, TypesofRules.Regla)
        Dim Rule As WFRuleParent = Activator.CreateInstance(t, Args)

        Rule.ID = Args(0)
        Rule.RuleType = TypeOfRule
        Rule.Enable = True

        Return Rule
    End Function
    Private Function GetIfBranch(ByVal cond As Boolean, ByVal WFStepId As Int64, ByVal IfBaseRule As IWFRuleParent) As IWFRuleParent
        Dim tt As Assembly = Assembly.LoadFile(Application.StartupPath & "\Zamba.WFActivity.Regular.dll")
        Dim t As System.Type = tt.GetType("Zamba.WFActivity.Regular.Ifbranch", True, True)
        Dim ifc As ConstructorInfo = t.GetConstructors.GetValue(0)
        Dim ifArgs(ifc.GetParameters.Length - 1) As Object
        'Primeros dos parametros Standarts del Ctor
        ifArgs(0) = CoreData.GetNewID(IdTypes.WFRule)
        'todo: que tome el maskname de la padre y le agregue resultado positivo o negativo o como este escrito, pero que indique que es

        If (cond) Then
            ifArgs(1) = "SI"
        Else
            ifArgs(1) = "NO"
        End If

        'ifArgs(1) = IfBaseRule.Name & cond.ToString
        ifArgs(2) = WFStepId
        ifArgs(3) = cond
        Dim ifRule As WFRuleParent = Activator.CreateInstance(t, ifArgs)
        ifRule.ID = CoreData.GetNewID(IdTypes.WFRule)
        ifRule.RuleType = TypesofRules.Regla
        ifRule.Enable = True
        Return ifRule
    End Function
    Public Function GetRuleParentType(ByVal BaseNode As BaseWFNode) As TypesofRules
        Try
            Dim pn As TreeNode = BaseNode
            Do

                If TypeOf pn Is RuleTypeNode Then
                    Return DirectCast(pn, RuleTypeNode).RuleParentType
                End If
                pn = pn.Parent
            Loop Until pn Is Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function
    Private Function GetStepId(ByVal myBaseNode As BaseWFNode) As Int64
        Try
            If TypeOf myBaseNode Is RuleTypeNode Then
                Return DirectCast(myBaseNode.Parent, EditStepNode).WFStep.ID
            ElseIf TypeOf myBaseNode Is RuleNode Then
                Return DirectCast(myBaseNode, RuleNode).Rule.WFStepId
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return 0
    End Function

    ''' Se comenta este metodo para evaluar su falta de uso, 
    '''era llamado por otro metodo addrules, que tiene hardcoded la regla dodistribuir.
    ''' Si alguien ve que esto provoca un errror, verlo con Martin.
    '''Se utiliza para agregar una regla dodistribuir entre 2 etapas de wfShapes, si alguien tiene algun problema, verlo con Marcelo
    Private Function GetNewRules(ByVal WFStep1 As WFStep, ByVal RuleName As String, ByVal strName As String) As WFRuleParent
        Dim tt As Assembly = Assembly.LoadFile(Membership.MembershipHelper.StartUpPath & "\Zamba.WFActivity.Regular.dll")
        Dim t As System.Type = tt.GetType("Zamba.WFActivity.Regular." & RuleName, True, True)
        Dim c As ConstructorInfo = t.GetConstructors.GetValue(0)
        Dim Args(c.GetParameters.Length - 1) As Object

        'Primeros dos parametros Standarts del Ctor
        Args(0) = CoreData.GetNewID(IdTypes.WFRule)
        Args(1) = strName
        Args(2) = WFStep1.ID
        FillArgs(Args, c, TypesofRules.Regla)

        Dim Rule As WFRuleParent = Activator.CreateInstance(t, Args)
        Rule.ID = CoreData.GetNewID(IdTypes.WFRule)
        Rule.Enable = True

        Return Rule
    End Function


    ''' <summary>
    ''' Crea una nueva regla
    ''' </summary>
    ''' <param name="RuleName"></param>
    ''' <param name="RuleNameFromUser"></param>
    ''' <param name="stepid"></param>
    ''' <param name="TypeOfRule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateNewRule(ByVal RuleName As String, ByVal RuleNameFromUser As String, ByVal stepid As Int64, ByVal TypeOfRule As TypesofRules, ByVal parentID As Int64, ByVal parentType As TypesofRules) As IRule
        'Dim t As System.Type = GetType(WFBusiness).Assembly.GetType("Zamba.WFBusiness." &  True, True)
        Dim tt As Assembly = Assembly.LoadFile(Membership.MembershipHelper.StartUpPath & "\Zamba.WFActivity.Regular.dll")
        Dim t As System.Type = tt.GetType("Zamba.WFActivity.Regular." & RuleName, True, True)
        Dim c As ConstructorInfo = t.GetConstructors.GetValue(0)
        Dim Args(c.GetParameters.Length - 1) As Object

        'Primeros dos parametros Standarts del Ctor
        Args(0) = CoreData.GetNewID(IdTypes.WFRule)
        Args(1) = RuleNameFromUser
        Args(2) = stepid
        FillArgs(Args, c, TypesofRules.Regla)

        Dim Rule As WFRuleParent = Activator.CreateInstance(t, Args)
        Rule.Enable = True
        Rule.ParentType = parentType

        AddNewRule(Rule, parentID)

        Return Rule
    End Function

    ''' <summary>
    ''' Inserta una regla
    ''' </summary>
    ''' <param name="NewRule"></param>
    ''' <remarks></remarks>
    Private Sub AddNewRule(ByVal NewRule As IRule)
        'Inserto Regla 
        WFRulesFactory.InsertRule(NewRule.ID, NewRule.ParentRule, NewRule.WFStepId, NewRule.Name, NewRule.RuleType, NewRule.ParentType, NewRule.RuleClass, NewRule.Enable, NewRule.Version)
        'Inserto Paremtros de Regla
        'WFRulesFactory.InsertRuleParam(NewRule)
        'Inserto Items de Regla
        AddRuleItems(NewRule)
    End Sub

    ''' <summary>
    ''' Inserta una regla
    ''' </summary>
    ''' <param name="NewRule"></param>
    ''' <remarks></remarks>
    Private Sub AddNewRule(ByVal NewRule As IRule, ByVal parentID As Int64)
        'Inserto Regla 
        WFRulesFactory.InsertRule(NewRule.ID, parentID, NewRule.WFStepId, NewRule.Name, NewRule.RuleType, NewRule.ParentType, NewRule.RuleClass, NewRule.Enable, NewRule.Version)
        'Inserto Paremtros de Regla
        'WFRulesFactory.InsertRuleParam(NewRule)
        'Inserto Items de Regla
        AddRuleItems(NewRule)
    End Sub
    Private Sub AddRuleItems(ByRef rule As WFRuleParent)
        Dim WFB As New WFBusiness
        Try
            Dim Arr As ArrayList = GetRuleArgs(rule)
            Dim i As Int32
            For Each o As Object In Arr
                WFRulesFactory.InsertRuleParamItem(rule, i, WFB.ConvertToPersist(o))
                i += 1
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            WFB = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Método que sirve para actualizar una propiedad de una regla
    ''' </summary>
    ''' <param name="RuleAction"></param>
    ''' <param name="Item"></param>
    ''' <param name="Value"></param>
    ''' <param name="objectTypes"></param>
    ''' <param name="carp"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/07/2008	Modified    Se agrego un nuevo parametro Optional (ObjectTypes)
    ''' </history>
    <Obsolete("Usa solo el Id y recibe toda la entidad")>
    Public Shared Sub UpdateParamItem(ByVal RuleAction As WFRuleParent, ByVal Item As Int32, ByVal Value As String, Optional ByVal objectTypes As Int32 = ObjectTypes.None)
        WFRulesFactory.UpdateParamItem(RuleAction, Item, Value, objectTypes)
    End Sub

    ''' <summary>
    ''' Este evento lo que hace es reemplazar la comilla simple por comillas dobles al guardarse el select en la tabla UpdateParamItems.
    ''' </summary>
    ''' <param name="RuleActionId"></param>
    ''' <param name="Item"></param>
    ''' <param name="Value"></param>
    ''' <param name="carp"></param>
    ''' <history>  
    '''     mariela     19/06/2008  modified 
    '''     [Gaston]	10/07/2008	Modified    Se agrego un nuevo parametro Optional (ObjectTypes)
    ''' </history>
    ''' <remarks></remarks>
    Public Sub UpdateParamItem(ByVal RuleActionId As Int64, ByVal Item As Int32, ByVal Value As String, Optional ByVal objectTypes As Int32 = ObjectTypes.None, Optional ByVal carp As Boolean = False)

        WFRulesFactory.UpdateParamItem(RuleActionId, Item, Value, objectTypes, carp)

    End Sub

#End Region

#Region "Reflection"

    ''' <summary>
    ''' Para obtener valores
    ''' </summary>
    ''' <param name="p"></param>
    ''' <param name="o"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Diego]	    11/06/2008	Modified    Se modifico el else
    '''     [Gaston]    11/06/2008  Modified    Se modifico el If p.ParameterType.IsEnum
    '''                 10/07/2008  Modified    Se modifico el case boolean (value.toUpper() = TRUE O FALSE) para
    '''                                         Oracle, ya que por ejemplo si venía un true se guardaba un false
    '''     [Javier]        22/08/2012 Modified   Se modifica el parámetro value para agregar validacion por DBNull
    ''' </history>
    Private Sub FillArgsValues(ByVal p As ParameterInfo, ByRef o As Object, ByVal value As Object)
        If value Is Nothing OrElse IsDBNull(value) Then
            value = String.Empty
        End If

        Select Case Type.GetTypeCode(p.ParameterType)

            Case TypeCode.Object
                o = Nothing

            Case TypeCode.Boolean

                If (Servers.Server.isOracle) Then
                    If (String.IsNullOrEmpty(value)) Then
                        value = "0"
                    ElseIf (String.IsNullOrEmpty(value)) Then
                        value = "0"
                    ElseIf ((value <> "0") And (value <> "1") And (value.ToString().ToUpper() <> "TRUE") And
                           (value.ToString().ToUpper() <> "FALSE")) Then
                        value = "0"
                    End If
                End If

                Try
                    o = CBool(value)
                    ' Puede ser que value sea un string con letras, si se intenta convertir un string con letras a bool se captura la exception 
                Catch ex As Exception
                    If (ex.InnerException.ToString().Contains("Input string was not in a correct format")) Then
                        value = "0"
                    End If
                End Try

            Case TypeCode.DateTime
                o = CDate(value)

            Case TypeCode.String
                o = value

            Case Else

                If (p.ParameterType.IsEnum) Then
                    If (String.IsNullOrEmpty(value)) Then value = "0"
                    o = System.Enum.ToObject(p.ParameterType, CInt(value))
                Else
                    If (String.IsNullOrEmpty(value)) Then value = "0"
                    o = Convert.ChangeType(value, p.ParameterType)
                End If

        End Select

    End Sub

    Private Function GetArgInitialValues(ByVal p As ParameterInfo) As Object

        Select Case Type.GetTypeCode(p.ParameterType)
            Case TypeCode.Object
                Return Nothing
            Case TypeCode.Boolean
                Return CBool(0)
            Case TypeCode.DateTime
                Return New Date
            Case TypeCode.String
                Return String.Empty.ToString
            Case Else
                If p.ParameterType.IsEnum Then
                    Return System.Enum.GetValues(p.ParameterType).GetValue(0)
                Else
                    Return Convert.ChangeType(0, p.ParameterType)
                End If
        End Select
    End Function
    Private Sub FillArgs(ByVal args() As Object, ByVal c As ConstructorInfo, ByVal ruleType As TypesofRules)
        Try
            Dim i As Byte
            'i = Nro de Parametro de Ctor
            For Each p As ParameterInfo In c.GetParameters
                If (i > 2 AndAlso ruleType = TypesofRules.Regla) Then
                    args(i) = GetArgInitialValues(p)
                End If
                i += 1
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Function GetRuleArgs(ByRef rule As WFRuleParent) As ArrayList
        Dim Arr As New ArrayList()

        Try
            Dim c As ConstructorInfo = rule.GetType.GetConstructors.GetValue(0)
            Dim i As Byte
            For Each p As ParameterInfo In c.GetParameters
                If i > 2 Then
                    Arr.Add(GetArgInitialValues(p))
                End If
                i += 1
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return Arr
    End Function
    Public Sub UpdateRuleNameByID(ByVal Id As Int64, ByVal Name As String)
        Try
            WFRulesFactory.UpdateRuleNameByID(Id, Name)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    'Public  Function GetConditionParamArgs(byref rule As WFRuleParent) As ArrayList
    '    Dim Arr As New ArrayList
    '    Try
    '        Dim c As ConstructorInfo = Rule.GetType.GetConstructors.GetValue(0)
    '        Dim i As Byte
    '        For Each p As ParameterInfo In c.GetParameters
    '            If i > 2 Then
    '                Arr.Add(GetArgInitialValues(p))
    '            End If
    '            i += 1
    '        Next
    '        Return Arr
    '    Catch ex As Exception
    '       zclass.raiseerror(ex)
    '        Return New ArrayList
    '    End Try
    'End Function
#End Region

#Region "Remove"




    '''Saca la regla de la base y el arbol


    ''' <summary>
    ''' Método que sirve eliminar las reglas del tree.
    ''' </summary>
    ''' <remarks></remarks>

#End Region

#Region "COPY, CUT AND PASTE"


#Region "Duplicación de las solapas 'Alertas', 'Configuración', 'Habilitación', 'Estado' y 'Asignación'"

    '    'Private  Sub duplicateRuleConfiguration(ByVal originalRuleId As Long, ByVal duplicateRuleId As Long)
    '    '    Dim dt As DataTable = Nothing

    '    '    Try
    '    '        ' Se obtienen los checkboxs de la regla original
    '    '        dt = WFRulesBusiness.GetRuleOption(originalRuleId)

    '    '        If dt IsNot Nothing Then

    '    '        End If
    '    '    Finally
    '    '        If dt IsNot Nothing Then
    '    '            dt.Dispose()
    '    '            dt = Nothing
    '    '        End If
    '    '    End Try
    '    'End Sub


    '    '''' <summary>
    '    '''' Método que sirve para duplicar el contenido de la solapa "Alertas" de la regla original a la regla duplicada
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    30/12/2008	Created
    '    '''' </history>
    '    'Private  Sub duplicateAlertsTab(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

    '    '    duplicateCheckBoxs(originalRuleId, duplicateRuleId)
    '    '    duplicateInternalMessage(originalRuleId, duplicateRuleId)
    '    '    duplicateMailMessage(originalRuleId, duplicateRuleId)
    '    '    duplicateAutomailMessage(originalRuleId, duplicateRuleId)

    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para duplicar el contenido de la solapa "Configuración" de la regla original a la regla duplicada
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    30/12/2008	Created
    '    '''' </history>
    '    'Private  Sub duplicateConfigurationTab(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

    '    '    ' Se obtiene el valor de la regla original
    '    '    Dim dt As DataTable = WFRulesBusiness.GetRuleOption(originalRuleId, RuleSectionOptions.Configuracion, RulePreferences.ConfigurationExecuteWhenResult)

    '    '    If (dt.Rows.Count > 0) Then
    '    '        ' Si la regla original tiene el checkbox "Ejecutar regla cuando exista documento" activado
    '    '        If (dt.Rows(0).Item("OBJVALUE").ToString() = "1") Then
    '    '            ' Se inserta en la base de datos el valor 1 (true) para que el checkbox de la solapa Configuración este en true cuando se acceda
    '    '            ' con la regla duplicada a esta solapa
    '    '            WFBusiness.SetRulesPreferences(duplicateRuleId, RuleSectionOptions.Configuracion, RulePreferences.ConfigurationExecuteWhenResult, 1)
    '    '        End If

    '    '        If Not (IsNothing(dt)) Then
    '    '            dt.Dispose()
    '    '        End If
    '    '    End If

    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para duplicar el contenido de la solapa "Habilitación" de la regla original a la regla duplicada
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <param name="ruleFather">Nuevo padre de la regla. Necesario para obtener la etapa y obtener los estados</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    19/01/2009	Created
    '    '''' </history>
    '    'Private  Sub duplicateHabilitationTab(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByRef ruleFather As BaseWFNode)

    '    '    duplicateItem(originalRuleId, duplicateRuleId)

    '    '    ' Se duplican los estados deshabilitados si es que existen
    '    '    duplicateElementsDisableOrSelectedFromDataBase(originalRuleId, duplicateRuleId, RuleSectionOptions.Habilitacion, _
    '    '                                                   RulePreferences.HabilitationTypeState)

    '    '    ' Se duplican los usuarios deshabilitados si es que existen
    '    '    duplicateElementsDisableOrSelectedFromDataBase(originalRuleId, duplicateRuleId, RuleSectionOptions.Habilitacion, _
    '    '                                                   RulePreferences.HabilitationTypeUser)

    '    '    ' Se duplican los grupos deshabilitados si es que existen
    '    '    duplicateElementsDisableOrSelectedFromDataBase(originalRuleId, duplicateRuleId, RuleSectionOptions.Habilitacion, _
    '    '                                                   RulePreferences.HabilitationTypeGroup)

    '    '    ' Se duplican los estados y usuarios, así como los estados y grupos si es que existen
    '    '    duplicateElementsDisableOrSelectedFromDataBase(originalRuleId, duplicateRuleId, ruleFather)

    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para duplicar el contenido de la solapa "Estado" de la regla original a la regla duplicada
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    19/01/2009	Created
    '    '''' </history>
    '    'Private  Sub duplicateStateTab(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

    '    '    ' Se duplica la etapa seleccionada (cuando se guardo) de la regla original, de lo contrario se seleccionara la primera etapa por defecto
    '    '    duplicateItemOfState(originalRuleId, duplicateRuleId, RulePreferences.StateTypeStage)
    '    '    ' Se recupera el estado seleccionado (cuando se guardo), de lo contrario se seleccionara el primer estado por defecto
    '    '    duplicateItemOfState(originalRuleId, duplicateRuleId, RulePreferences.StateTypeState)

    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para duplicar el contenido de la solapa "Asignación" de la regla original a la regla duplicada
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    19/01/2009	Created
    '    '''' </history>
    '    'Private  Sub duplicateAsignationTab(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

    '    '    ' Se recupera el radioButton seleccionado
    '    '    Dim dt As DataTable = WFBusiness.recoverItemsSelected(originalRuleId, RuleSectionOptions.Asignacion, RulePreferences.AsignationTypeUser, _
    '    '                                                        RulePreferences.AsignationTypeGroup, RulePreferences.AsignationTypeManual)

    '    '    If (dt.Rows.Count > 0) Then

    '    '        Dim typeSelection As Integer = dt.Rows(0).Item("OBJECTID")

    '    '        Select Case typeSelection

    '    '            Case RulePreferences.AsignationTypeUser, RulePreferences.AsignationTypeGroup

    '    '                If (typeSelection = RulePreferences.AsignationTypeUser) Then

    '    '                    ' Parámetros: Id de regla - identificación de panel (Asignación) - identificación de radiobutton 
    '    '                    '             seleccionado (Usuario) - id de usuario seleccionado en la lista de usuarios
    '    '                    WFBusiness.SetRulesPreferencesSinObjectId(duplicateRuleId, RuleSectionOptions.Asignacion, _
    '    '                                                              RulePreferences.AsignationTypeUser, dt.Rows(0).Item("OBJVALUE"))

    '    '                Else

    '    '                    ' Parámetros: Id de regla - identificación de panel (Asignación) - identificación de radiobutton 
    '    '                    '             seleccionado (Grupo) - id de grupo seleccionado en la lista de grupos
    '    '                    WFBusiness.SetRulesPreferencesSinObjectId(duplicateRuleId, RuleSectionOptions.Asignacion, _
    '    '                                                              RulePreferences.AsignationTypeGroup, dt.Rows(0).Item("OBJVALUE"))

    '    '                End If

    '    '            Case RulePreferences.AsignationTypeManual

    '    '                ' Parámetros: Id de regla - identificación de panel (Asignación) - identificación de radiobutton 
    '    '                '             seleccionado (Manual) - ningún id seleccionado - contenido de la caja de texto 
    '    '                '             Ingresar usuario manualmente
    '    '                WFBusiness.SetRulesPreferencesSinObjectId(duplicateRuleId, RuleSectionOptions.Asignacion, RulePreferences.AsignationTypeManual, _
    '    '                                                          0, dt.Rows(0).Item("OBJEXTRADATA"))

    '    '        End Select

    '    '    End If

    '    'End Sub

    '#Region "Métodos para duplicar el contenido de la solapa 'Alertas'"

    '    '''' <summary>
    '    '''' Método que sirve para duplicar los checkboxs que estén cliqueados de la regla original (Mensaje interno, Mail automático y Mail)
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    30/12/2008	Created
    '    '''' </history>
    '    'Private  Sub duplicateCheckBoxs(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)
    '    '    Dim dt As DataTable = Nothing

    '    '    Try

    '    '        ' Se obtienen los checkboxs de la regla original
    '    '        dt = WFRulesBusiness.GetRuleOption(originalRuleId, RuleSectionOptions.Alerta, RulePreferences.AlertNotificationMode)

    '    '        If (dt.Rows.Count > 0) Then
    '    '            WFBusiness.SetRulesPreferences(duplicateRuleId, RuleSectionOptions.Alerta, RulePreferences.AlertNotificationMode, 1, _
    '    '                                           dt.Rows(0).Item(1).ToString())
    '    '        End If

    '    '    Catch ex As Exception
    '    '        ZClass.raiseerror(ex)
    '    '    Finally
    '    '        If Not IsNothing(dt) Then
    '    '            dt.Dispose()
    '    '            dt = Nothing
    '    '        End If
    '    '    End Try
    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para duplicar el contenido de la solapa "Mensaje Interno"
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    30/12/2008	Created
    '    '''' </history>
    '    'Private  Sub duplicateInternalMessage(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

    '    '    insertGroupsUsersAndExternalUsers(originalRuleId, duplicateRuleId, RulePreferences.AlertInternalMessageFor)
    '    '    insertGroupsUsersAndExternalUsers(originalRuleId, duplicateRuleId, RulePreferences.AlertInternalMessageCC)
    '    '    insertGroupsUsersAndExternalUsers(originalRuleId, duplicateRuleId, RulePreferences.AlertInternalMessageCCO)
    '    '    insertSubjectOrBody(originalRuleId, duplicateRuleId, RulePreferences.AlertInternalMessageSubject)
    '    '    insertAttachDocument(originalRuleId, duplicateRuleId, RulePreferences.AlertInternalMessageAttachDocument)
    '    '    insertSubjectOrBody(originalRuleId, duplicateRuleId, RulePreferences.AlertInternalMessageBody)

    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para duplicar el contenido de la solapa "Mensaje Automático"
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    16/01/2009	Created
    '    '''' </history>
    '    'Private  Sub duplicateAutomailMessage(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

    '    '    insertElementOfAutomail(originalRuleId, duplicateRuleId, RulePreferences.AlertAutomailId)
    '    '    insertElementOfAutomail(originalRuleId, duplicateRuleId, RulePreferences.AlertAutomailAttach)
    '    '    insertElementOfAutomail(originalRuleId, duplicateRuleId, RulePreferences.AlertAutomailEnableAttach)
    '    '    insertElementOfAutomail(originalRuleId, duplicateRuleId, RulePreferences.AlertAutomailSMTPGroupByDest)
    '    '    duplicateRadioButtons(originalRuleId, duplicateRuleId)

    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para duplicar el contenido de la solapa "Mail"
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    30/12/2008	Created
    '    '''' </history>
    '    'Private  Sub duplicateMailMessage(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

    '    '    insertGroupsUsersAndExternalUsers(originalRuleId, duplicateRuleId, RulePreferences.AlertMailFor)
    '    '    insertGroupsUsersAndExternalUsers(originalRuleId, duplicateRuleId, RulePreferences.AlertMailCC)
    '    '    insertGroupsUsersAndExternalUsers(originalRuleId, duplicateRuleId, RulePreferences.AlertMailCCO)
    '    '    insertSubjectOrBody(originalRuleId, duplicateRuleId, RulePreferences.AlertMailSubject)
    '    '    insertAttachDocument(originalRuleId, duplicateRuleId, RulePreferences.AlertMailAttachDocument)
    '    '    insertSubjectOrBody(originalRuleId, duplicateRuleId, RulePreferences.AlertMailBody)

    '    'End Sub

    '#Region "Métodos de la solapa 'Mensaje Interno' y de la solapa 'Mail'"

    '    '''' <summary>
    '    '''' Método que sirve para duplicar los usuarios, grupos y usuarios externos que se hayan colocado en Para, CC o CCO en la regla original
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <param name="rule">Tipo de elemento: AlertMailFor o AlertMailCC o AlertMailCCO</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    30/12/2008	Created
    '    '''' </history>
    '    'Private  Sub insertGroupsUsersAndExternalUsers(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByVal rule As RulePreferences)

    '    '    ' Usuarios
    '    '    insert(originalRuleId, duplicateRuleId, rule, 1)
    '    '    ' Grupos
    '    '    insert(originalRuleId, duplicateRuleId, rule, 2)
    '    '    ' Usuarios Externos
    '    '    insert(originalRuleId, duplicateRuleId, rule, 3)

    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para duplicar los usuarios, grupos o usuarios externos de la regla original y pasarlos a la regla duplicada
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <param name="rule">Tipo de elemento: AlertMailFor o AlertMailCC o AlertMailCCO </param>
    '    '''' <param name="desTypes">Tipo de elemento: 1(Usuarios), 2(Grupos) o 3(Usuarios Externos)</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    30/12/2008	Created
    '    '''' </history>
    '    'Private  Sub insert(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByVal rule As RulePreferences, ByRef desTypes As Integer)
    '    '    Dim dt As New DataTable

    '    '    Try

    '    '        ' Se obtienen los id's de los grupos del textbox (Para, CC o CCO) de la regla original
    '    '        dt = WFRulesBusiness.GetRuleOption(originalRuleId, RuleSectionOptions.Alerta, rule, desTypes)

    '    '        ' Si el datatable tiene filas quiere decir que hay grupos, usuarios o usuarios externos (Para, CC o CCO) en la regla original. Por lo 
    '    '        ' tanto todos esos grupos pasan a la regla duplicada
    '    '        If (dt.Rows.Count > 0) Then

    '    '            Dim collection As New Generic.List(Of String)

    '    '            For Each row As DataRow In dt.Rows
    '    '                ' Se guarda el ID del grupo, usuario o usuario externo
    '    '                collection.Add(row(0).ToString())
    '    '            Next

    '    '            ' Parámetros: Número de Regla, Número de Solapa, Origen del botón (For,CC,CCO), Número que identifica a un grupo, IDs de los grupos
    '    '            WFRulesBusiness.InsertUsersToNotifyAboutRuleExecution(duplicateRuleId, RuleSectionOptions.Alerta, rule, desTypes, collection)
    '    '            collection = Nothing

    '    '        End If

    '    '    Catch ex As Exception
    '    '        ZClass.raiseerror(ex)
    '    '    Finally
    '    '        If Not IsNothing(dt) Then
    '    '            dt.Dispose()
    '    '            dt = Nothing
    '    '        End If
    '    '    End Try
    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para duplicar el Asunto o el cuerpo del mensaje de la regla original y pasarlo a la regla duplicada
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <param name="rule">Tipo de elemento: AlertInternalMessageSubject o AlertInternalMessageBody</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    30/12/2008	Created
    '    '''' </history>
    '    'Private  Sub insertSubjectOrBody(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByVal rule As RulePreferences)
    '    '    Dim dt As DataTable = Nothing

    '    '    Try
    '    '        dt = WFRulesBusiness.GetRuleOption(originalRuleId, RuleSectionOptions.Alerta, rule)

    '    '        ' Si el datatable tiene files quiere decir que hay un texto en el subject o body del mensaje original. Por lo tanto, ese body pasa a la 
    '    '        ' regla duplicada
    '    '        If (dt.Rows.Count > 0) Then
    '    '            WFBusiness.SetRulesPreferences(duplicateRuleId, RuleSectionOptions.Alerta, rule, 0, dt.Rows(0).Item(1).ToString())
    '    '        End If

    '    '    Catch ex As Exception
    '    '        ZClass.raiseerror(ex)
    '    '    Finally
    '    '        If Not IsNothing(dt) Then
    '    '            dt.Dispose()
    '    '            dt = Nothing
    '    '        End If
    '    '    End Try
    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para duplicar el Adjuntar Documento de la regla original y pasarlo a la regla duplicada
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <param name="rule">Tipo de elemento: AlertInternalMessageAttachDocument</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    30/12/2008	Created
    '    '''' </history>
    '    'Private  Sub insertAttachDocument(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByVal rule As RulePreferences)
    '    '    Dim dt As DataTable = WFRulesBusiness.GetRuleOption(originalRuleId, RuleSectionOptions.Alerta, rule)

    '    '    If (dt.Rows.Count > 0) Then
    '    '        WFBusiness.SetRulesPreferences(duplicateRuleId, RuleSectionOptions.Alerta, rule, dt.Rows(0).Item(0))
    '    '    End If
    '    'End Sub

    '#End Region

    '#Region "Métodos de la solapa 'Mensaje Automático'"

    '    '''' <summary>
    '    '''' Método que sirve para duplicar un elemento de la regla original y pasarlo a la regla duplicada
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <param name="rule">Tipo de elemento: AlertAutomailId o AlertAutomailAttach o AlertAutomailEnableAttach o AlertAutomailSMTPGroupByDest</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    16/01/2009	Created
    '    '''' </history>
    '    'Private  Sub insertElementOfAutomail(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByRef rule As RulePreferences)
    '    '    Dim dt As DataTable = Nothing

    '    '    Try
    '    '        dt = WFRulesBusiness.GetRuleOption(originalRuleId, RuleSectionOptions.Alerta, rule)

    '    '        If (dt.Rows.Count > 0) Then
    '    '            WFBusiness.SetRulesPreferences(duplicateRuleId, RuleSectionOptions.Alerta, rule, dt.Rows(0).Item(0))
    '    '        End If
    '    '    Catch ex As Exception
    '    '        ZClass.raiseerror(ex)
    '    '    Finally
    '    '        If Not IsNothing(dt) Then
    '    '            dt.Dispose()
    '    '            dt = Nothing
    '    '        End If
    '    '    End Try
    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para duplicar el radioButton seleccionado (Adjuntar Link o Adjuntar Documento)
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    16/01/2009	Created
    '    '''' </history>
    '    'Private  Sub duplicateRadioButtons(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)
    '    '    Dim dt As DataTable = Nothing
    '    '    Try
    '    '        dt = WFRulesBusiness.GetRuleOption(originalRuleId, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailAttach)

    '    '        If (dt.Rows.Count > 0) Then
    '    '            WFBusiness.SetRulesPreferences(duplicateRuleId, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailAttach, 0, dt.Rows(0).Item(1))
    '    '        End If

    '    '    Catch ex As Exception
    '    '        ZClass.raiseerror(ex)
    '    '    Finally
    '    '        If Not IsNothing(dt) Then
    '    '            dt.Dispose()
    '    '            dt = Nothing
    '    '        End If
    '    '    End Try
    '    'End Sub
    '#End Region

    '#End Region

    '#Region "Métodos para duplicar el contenido de la solapa 'Estado'"

    '    '''' <summary>
    '    '''' Método que sirve para duplicar la etapa o estado seleccionado en la regla original
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <param name="rulePreferences">Tipo de elemento: Combinación o Estado</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    19/01/2009	Created
    '    '''' </history>
    '    'Private  Sub duplicateItemOfState(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByRef rulePreferences As RulePreferences)
    '    '    Dim dt As DataTable = Nothing
    '    '    Try
    '    '        dt = WFRulesBusiness.GetRuleOption(originalRuleId, RuleSectionOptions.Estado, rulePreferences)

    '    '        If (dt.Rows.Count > 0) Then
    '    '            ' Se guarda el id de la etapa selecionada (en el combobox) en la base de datos
    '    '            ' Parámetros: Número de Regla, Tipo de Solapa, Identifica a una Etapa, Id de la Etapa seleccionada o estado seleccionado
    '    '            WFBusiness.SetRulesPreferences(duplicateRuleId, RuleSectionOptions.Estado, rulePreferences, dt.Rows(0).Item(0))
    '    '        End If
    '    '    Catch ex As Exception
    '    '        ZClass.raiseerror(ex)
    '    '    Finally
    '    '        If Not IsNothing(dt) Then
    '    '            dt.Dispose()
    '    '            dt = Nothing
    '    '        End If
    '    '    End Try
    '    'End Sub

    '#End Region

    '#Region "Métodos para duplicar el contenido de la solapa 'Habilitación'"

    '    '''' <summary>
    '    '''' Método que sirve para duplicar el radioButton seleccionado en la regla original
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    19/01/2009	Created
    '    '''' </history>
    '    'Private  Sub duplicateItem(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

    '    '    Dim value As RulePreferences

    '    '    Try
    '    '        ' Se recupera el radioButton seleccionado (cuando se guardo), si no hay, entonces se obtiene un 0 de la regla original
    '    '        value = WFBusiness.recoverItemSelectedThatCanBe_StateOrUserOrGroup(originalRuleId)
    '    '    Catch ex As Exception
    '    '        ZClass.raiseerror(ex)
    '    '    End Try

    '    '    ' Se guarda el radioButton actualmente seleccionado (Estado, Usuario o Ambos) en la regla duplicada
    '    '    Select Case value
    '    '        Case RulePreferences.HabilitationSelectionState, 0
    '    '            WFBusiness.saveItemSelectedThatCanBe_StateOrUserOrGrupo(duplicateRuleId, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationSelectionState)
    '    '        Case RulePreferences.HabilitationSelectionUser
    '    '            WFBusiness.saveItemSelectedThatCanBe_StateOrUserOrGrupo(duplicateRuleId, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationSelectionUser)
    '    '        Case RulePreferences.HabilitationSelectionBoth
    '    '            WFBusiness.saveItemSelectedThatCanBe_StateOrUserOrGrupo(duplicateRuleId, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationSelectionBoth)
    '    '    End Select

    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para duplicar los estados, usuarios o grupos deshabilitados de la regla original
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <param name="ruleSection">Tipo de elemento: Habilitacion</param>
    '    '''' <param name="rulePreferences">Tipo de elemento: HabilitationSelectionState o HabilitationSelectionUser o HabilitationSelectionBoth</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    19/01/2009	Created
    '    '''' </history>
    '    'Private  Sub duplicateElementsDisableOrSelectedFromDataBase(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, _
    '    '                                                                  ByRef ruleSection As RuleSectionOptions, ByRef rulePreferences As RulePreferences)

    '    '    Dim dt As DataTable = Nothing

    '    '    Try

    '    '        dt = WFRulesBusiness.GetRuleOption(originalRuleId, ruleSection, rulePreferences)

    '    '        If (dt.Rows.Count > 0) Then

    '    '            ' Lista utilizada para guardar los id's de los estados, usuarios o grupos deshabilitados que se recuperan de la base de datos
    '    '            Dim idCollectionDisabled As New Generic.List(Of Integer)

    '    '            ' Se recorren los id's de los elementos deshabilitados (estados, usuarios o grupos) y se guardan en la colección correspondiente
    '    '            For Each row As DataRow In dt.Rows
    '    '                idCollectionDisabled.Add(CType(row.Item("ObjValue"), Int32))
    '    '            Next

    '    '            ' Si la colección que contiene los id's de elementos (estados, usuarios o grupos) deshabilitados es mayor a cero
    '    '            If (idCollectionDisabled.Count > 0) Then

    '    '                ' Parámetros: número de regla, identificación de solapa Habilitación, identificación de elemento que se guarda y colección que 
    '    '                ' contiene los id's de elementos deshabilitados (estados, usuarios o grupos)
    '    '                WFBusiness.SetRulesPreferencesForStatesUsersOrGroups(duplicateRuleId, ruleSection, rulePreferences, idCollectionDisabled)

    '    '            End If

    '    '            idCollectionDisabled = Nothing

    '    '        End If

    '    '    Catch ex As Exception
    '    '        ZClass.raiseerror(ex)
    '    '    Finally
    '    '        If Not IsNothing(dt) Then
    '    '            dt.Dispose()
    '    '            dt = Nothing
    '    '        End If
    '    '    End Try
    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para duplicar los estados y usuarios o estados y grupos deshabilitados de la regla original
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <param name="ruleFather">Nuevo padre de la regla. Necesario para obtener la etapa y obtener los estados</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    19/01/2009	Created
    '    '''' </history>
    '    'Private  Sub duplicateElementsDisableOrSelectedFromDataBase(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByRef ruleFather As BaseWFNode)

    '    '    Dim collection As New ArrayList

    '    '    If (TypeOf ruleFather Is RuleNode) Then
    '    '        loadStates(DirectCast(ruleFather, RuleNode).Rule.WFStepId, collection)
    '    '    Else
    '    '        loadStates(DirectCast(ruleFather.Parent, EditStepNode).WFStep.ID, collection)
    '    '    End If

    '    '    If (collection.Count > 0) Then
    '    '        recoverAndLoad_StatesAndUsers_Or_StatesAndGroupsDisable(originalRuleId, duplicateRuleId, collection)
    '    '    End If

    '    '    collection = Nothing

    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para cargar los estados de la etapa
    '    '''' </summary>
    '    '''' <param name="p_iStepId">Id de la etapa en donde se va a pegar la regla duplicada</param>
    '    '''' <param name="collection">Colección que contendrá elementos de tipo StateItem</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    19/01/2009	Created
    '    '''' </history>
    '    'Private  Sub loadStates(ByVal p_iStepId As Long, ByRef collection As ArrayList)

    '    '    Dim Wfstep As WFStep

    '    '    Try

    '    '        Wfstep = WFStepBusiness.GetStepById(p_iStepId)

    '    '        If (Not Wfstep Is Nothing) Then
    '    '            loadStatesForTabHabilitation(Wfstep, collection)
    '    '        End If

    '    '    Catch ex As Exception
    '    '        ZClass.raiseerror(ex)
    '    '    End Try

    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para cargar en los estados el nombre y el id de estado
    '    '''' </summary>
    '    '''' <param name="Wfstep">Etapa</param>
    '    '''' <param name="collection">Colección que contendrá elementos de tipo StateItem</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    19/01/2009	Created
    '    '''' </history>
    '    'Private  Sub loadStatesForTabHabilitation(ByRef Wfstep As WFStep, ByRef collection As ArrayList)

    '    '    For Each state As WFStepState In Wfstep.States
    '    '        ' Se agrega un elemento de tipo StateItem a la colección (contiene nombre y id de estado)
    '    '        collection.Add(New StateItem(state))
    '    '    Next

    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para recuperar los id's de usuarios y grupos deshabilitados que hayan en la regla original y guardarlos en la regla 
    '    '''' duplicada en su correspondiente estado
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <param name="collection">Colección que contendrá elementos de tipo StateItem</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    19/01/2009	Created
    '    '''' </history>
    '    'Private  Sub recoverAndLoad_StatesAndUsers_Or_StatesAndGroupsDisable(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, _
    '    '                                                                           ByRef collection As ArrayList)

    '    '    For Each state As StateItem In collection

    '    '        ' Se recuperan los id's de usuarios deshabilitados que corresponden a ese estado (si es que existen)
    '    '        ' Parámetros: id que identifica a estados y usuarios, id de estado y colección de id's de usuarios deshabilitados de ese estado
    '    '        recoverAndLoadStates_Or_Groups_belongingToAStateDisable(originalRuleId, RulePreferences.HabilitationTypeStateAndUser, state.Tag, state.UsersDisabled)
    '    '        ' Se recuperan los id's de grupos deshabilitados que corresponden a ese estado (si es que existen)
    '    '        ' Parámetros: id que identifica a estados y grupos, id de estado y colección de id's de grupos deshabilitados de ese estado
    '    '        recoverAndLoadStates_Or_Groups_belongingToAStateDisable(originalRuleId, RulePreferences.HabilitationTypeStateAndGroup, state.Tag, state.GroupsDisabled)

    '    '    Next

    '    '    saveTabHabilitationInDuplicateRule(originalRuleId, duplicateRuleId, collection)

    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para recuperar los usuarios deshabilitados de la regla original y guardar en idCollectionDisabledOfState sus id's
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="rulePreferences">Tipo de elemento: HabilitationTypeStateAndUser o HabilitationTypeStateAndGroup</param>
    '    '''' <param name="stateId">Id de estado</param>
    '    '''' <param name="idCollectionDisabledOfState">Colección que contendrá los id's de usuarios deshabilitados</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    19/01/2009	Created
    '    '''' </history>
    '    'Private  Sub recoverAndLoadStates_Or_Groups_belongingToAStateDisable(ByRef originalRuleId As Long, _
    '    '                                                                           ByVal rulePreferences As RulePreferences, ByVal stateId As String, _
    '    '                                                                           ByRef idCollectionDisabledOfState As Generic.List(Of Integer))

    '    '    Try

    '    '        ' Parámetros : id de Regla actual, id para identificar la solapa (Habilitación), id que identifica a estados y usuarios o estados y grupos,
    '    '        ' id de estado
    '    '        Dim ds As DataSet = WFBusiness.recoverUsers_Or_Groups_belongingToAState(originalRuleId, RuleSectionOptions.Habilitacion, rulePreferences, stateId)

    '    '        If (ds.Tables(0).Rows.Count > 0) Then

    '    '            For Each row As DataRow In ds.Tables(0).Rows
    '    '                idCollectionDisabledOfState.Add(row("ObjValue"))
    '    '            Next

    '    '        End If

    '    '    Catch ex As Exception
    '    '        ZClass.raiseerror(ex)
    '    '    End Try

    '    'End Sub

    '    '''' <summary>
    '    '''' Método que sirve para guardar los estados y usuarios, asi como también estados y grupos que haya en la regla original en la regla duplicada
    '    '''' </summary>
    '    '''' <param name="originalRuleId">Id de la regla original</param>
    '    '''' <param name="duplicateRuleId">Id de la regla duplicada</param>
    '    '''' <param name="collection">Colección de estados, cada uno con sus usuarios y grupos deshabilitados</param>
    '    '''' <remarks></remarks>
    '    '''' <history>
    '    ''''     [Gaston]    19/01/2009	Created
    '    '''' </history>
    '    'Private  Sub saveTabHabilitationInDuplicateRule(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByRef collection As ArrayList)

    '    '    ' Se recorren los estados uno por uno para comprobar si poseen al menos una colección (ya sea de id's de usuarios deshabilitados o id's de
    '    '    ' grupos deshabilitados, o ambos) para guardar en la base de datos
    '    '    For Each state As StateItem In collection

    '    '        If (state.UsersDisabled.Count > 0) Then

    '    '            ' Parámetros: Número de regla, identificación de solapa (Habilitación), id que identifica que lo que se guarda es un id de tipo
    '    '            ' StateAndUser, id del estado y colección que contiene los id's de usuarios deshabilitados que pertenecen al estado
    '    '            WFBusiness.SetRulesPreferencesFor_StatesAndUsers_Or_StatesAndGroups(duplicateRuleId, RuleSectionOptions.Habilitacion, _
    '    '                                                                                RulePreferences.HabilitationTypeStateAndUser, state.Tag, _
    '    '                                                                                state.UsersDisabled)

    '    '        End If

    '    '        ' Parámetros: Número de regla, identificación de solapa (Habilitación), id que identifica que lo que se guarda es un id de tipo
    '    '        ' StateAndGroup, id del estado y colección que contiene los id's de grupos deshabilitados que pertenecen al estado
    '    '        If (state.GroupsDisabled.Count > 0) Then

    '    '            WFBusiness.SetRulesPreferencesFor_StatesAndUsers_Or_StatesAndGroups(duplicateRuleId, RuleSectionOptions.Habilitacion, _
    '    '                                                                                RulePreferences.HabilitationTypeStateAndGroup, state.Tag, _
    '    '                                                                                state.GroupsDisabled)

    '    '        End If

    '    '    Next

    '    'End Sub

    '#Region "Clase privada StateItem que representa un elemento de lstHabilitationStates"

    '    '    Private Class StateItem
    '    '        Inherits ListViewItem

    '    '#Region "Atributos"

    '    '        ' Lista que contiene los id de usuarios deshabilitados
    '    '        Private mUsersDisabled As New Generic.List(Of Integer)
    '    '        ' Lista que contiene los id de grupos deshabilitados
    '    '        Private mGroupsDisabled As New Generic.List(Of Integer)

    '    '#End Region

    '    '#Region "Constructores"

    '    '        ' 1º Constructor : Lo utilizan los estados de la solapa Habilitación y Estado
    '    '        Sub New(ByVal State As WFStepState)
    '    '            Me.Text = State.Name
    '    '            Me.Tag = State.ID
    '    '        End Sub

    '    '#End Region

    '    '#Region "Propiedades"

    '    '        Public ReadOnly Property UsersDisabled() As Generic.List(Of Integer)
    '    '            Get
    '    '                Return (Me.mUsersDisabled)
    '    '            End Get
    '    '        End Property

    '    '        Public ReadOnly Property GroupsDisabled() As Generic.List(Of Integer)
    '    '            Get
    '    '                Return (Me.mGroupsDisabled)
    '    '            End Get
    '    '        End Property

    '    '#End Region

    '    '    End Class

    '#End Region

    '#End Region

#End Region

    ''' </summary>
    ''' Método que guarda en la base de datos la regla duplicada y sus argumentos, iguales a la regla original
    ''' <param name="duplicateRuleId"></param>
    ''' <param name="ruleType"></param>
    ''' <param name="originalRule"></param>
    ''' <param name="ruleFather"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	17/09/2008	Created
    '''                 01/10/2008  Modified
    '''                 07/10/2008  Modified    Validación de DBNull
    '''                 23/10/2008  Modified    Validación de DBNull para item, y para SQL y Oracle
    ''' </history>
    Private Function insertDuplicateRule(ByRef duplicateRuleId As Long, ByRef ruleType As TypesofRules, ByRef originalRule As WFRuleParent, ByVal ruleFather As BaseWFNode) As DataRow()

        Dim ds As DataSet = Nothing

        Try

            If (TypeOf ruleFather Is RuleNode) Then

                Try
                    WFRulesFactory.InsertRule(duplicateRuleId, DirectCast(ruleFather, RuleNode).Rule, DirectCast(ruleFather, RuleNode).Rule.WFStepId,
                                              originalRule.Name, ruleType, DirectCast(ruleFather, RuleNode).Rule.RuleType, originalRule.RuleClass, originalRule.Enable,
                                              originalRule.Version)
                Catch ex As Exception
                    WFRulesFactory.InsertRule(duplicateRuleId, DirectCast(ruleFather, RuleNode).Rule, DirectCast(ruleFather.Parent, EditStepNode).WFStep.ID,
                    originalRule.Name, ruleType, DirectCast(ruleFather, RuleNode).Rule.RuleType, originalRule.RuleClass, originalRule.Enable,
                    originalRule.Version)
                End Try

            Else
                WFRulesFactory.InsertRule(duplicateRuleId, 0, DirectCast(ruleFather.Parent, EditStepNode).WFStep.ID,
                                          originalRule.Name, ruleType, DirectCast(ruleFather, RuleTypeNode).RuleParentType, originalRule.RuleClass, originalRule.Enable,
                                          originalRule.Version)
            End If

            ' Se recuperan los argumentos de la regla original
            ds = WFRulesFactory.GetRuleParamItems(originalRule.ID)

            ' Se insertan los argumentos en WFRuleParamItems con el id de la regla duplicada
            If (ds.Tables("WFRuleParamItems").Rows.Count > 0) Then

                For Each row As DataRow In ds.Tables("WFRuleParamItems").Rows

                    If (IsDBNull(row("item"))) Then
                        row("item") = 0
                    End If

                    If (IsDBNull(row("value"))) Then
                        row("value") = String.Empty
                    End If

                    If (IsDBNull(row("objectTypes"))) Then
                        row("objectTypes") = 0
                    End If

                    WFRulesFactory.UpdateParamItem(duplicateRuleId, row("item"), row("value"), row("objectTypes"))

                Next

            End If

            ' Se guardan los argumentos de la regla en la colección ruleParamRowItems
            Return (ds.Tables("WFRuleParamItems").Select("Rule_id=" & originalRule.ID))
        Finally
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try

    End Function

    ''' </summary>
    ''' Método que sirve para crear una instancia de la regla duplicada
    ''' <param name="duplicateRuleId"></param>
    ''' <param name="originalRule"></param>
    ''' <param name="ruleFather"></param>
    ''' <param name="ruleParamRowItems"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    01/10/2008	Created
    ''' </history>
    Private Function createNewRule(ByRef duplicateRuleId As Long, ByRef originalRule As WFRuleParent, ByRef ruleFather As BaseWFNode, ByRef ruleParamRowItems() As DataRow) As WFRuleParent

        Dim tt As Assembly = Assembly.LoadFile(Membership.MembershipHelper.StartUpPath & "\Zamba.WFActivity.Regular.dll")
        Dim t As System.Type = tt.GetType("Zamba.WFActivity.Regular." & originalRule.RuleClass, True, True)
        Dim c As ConstructorInfo = t.GetConstructors.GetValue(0)
        Dim Args(c.GetParameters.Length - 1) As Object
        Dim i As Byte
        Dim y As Byte

        Dim i_dsRule As DataSet = Nothing

        ' Se genera el ID | Nombre de la regla según el usuario | ID de etapa
        Args(0) = duplicateRuleId
        Args(1) = originalRule.Name

        Try
            Args(2) = DirectCast(ruleFather.Parent, EditStepNode).WFStep.ID
        Catch ex As Exception
            Args(2) = DirectCast(ruleFather, RuleNode).Rule.WFStepId
        End Try

        For Each p As ParameterInfo In c.GetParameters
            If i > 2 Then
                If (ruleParamRowItems.Length >= y + 1) Then
                    FillArgsValues(p, Args(i), ruleParamRowItems(y)("Value"))
                Else
                    FillArgsValues(p, Args(i), GetArgInitialValues(p))
                End If
                y += 1
            End If
            i += 1
        Next

        ' Se crea la instancia de la regla 
        Dim Rule As WFRuleParent = Activator.CreateInstance(t, Args)

        ' Se pasan algunas propiedades de la regla original y del nuevo padre a la regla duplicada
        Rule.Enable = originalRule.Enable
        Rule.Version = originalRule.Version

        If (TypeOf ruleFather Is RuleNode) Then
            Rule.Parent = DirectCast(ruleFather, RuleNode).Rule
            Rule.ParentRule = DirectCast(ruleFather, RuleNode).Rule
            Rule.ParentType = DirectCast(ruleFather, RuleNode).Rule.RuleType
        Else
            Rule.Parent = Nothing
            Rule.ParentRule = Nothing
            Rule.ParentType = DirectCast(ruleFather, RuleTypeNode).RuleParentType
        End If

        Return (Rule)

    End Function

    ''' <summary>
    ''' Método recursivo que sirve para colocar en la colección childRules de cada regla (de un nodo) las correspondientes reglas hijas de los 
    ''' nodos hijos 
    ''' </summary>
    ''' <param name="rn"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    22/10/2008	Created
    ''' </history>
    'Private Sub addChildRules(ByRef rn As RuleNode)

    '    If (rn.Nodes.Count <> rn.Rule.ChildRules.Count) Then

    '        For Each a As RuleNode In rn.Nodes

    '            addChildRules(a)

    '            If Not (rn.Rule.ChildRules.Contains(a.Rule)) Then
    '                rn.Rule.ChildRules.Add(a.Rule)
    '            End If

    '        Next

    '    End If

    'End Sub

    ''' <summary>
    ''' Método que sirve para actualizar los stepIds de las reglas hijas cortadas y que se quieren pegar a otra etapa
    ''' </summary>
    ''' <param name="rule">Instancia de regla</param>
    ''' <param name="ban">La regla padre no se actualiza, ya está actualizada</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	04/11/2008	Created
    ''' </history>
    Private Sub updatestepIdsOfChildRules(ByRef rule As WFRuleParent, ByRef ban As Boolean)

        If (ban = True) Then
            ' Se actualiza el step id de la regla en la base de datos
            WFRulesFactory.updateStepIdofRule(rule.ID, rule.WFStepId)
        End If

        '        updatestepIdsOfChildRules(rule.ChildRulesIds, rule.WFStepId)

    End Sub

#End Region

#Region "UserAction"
    Friend Sub AddUserAction(ByVal stepNode As EditStepNode)
        Try
            Dim UserActionNode As New RuleTypeNode(TypesofRules.AccionUsuario)
            stepNode.Nodes.Insert(stepNode.Nodes.IndexOf(stepNode.ScheduleNode), UserActionNode)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Friend Sub ChangeName(ByVal userActionNode As RuleTypeNode)

        Try

            Dim BaseNode As BaseWFNode = userActionNode.Nodes(0)
            Dim WFRule As WFRuleParent
            WFRule = DirectCast(BaseNode, RuleNode).Rule
            Dim NewName As String = InputBox("Ingrese el nuevo nombre de la regla", "Edición de Reglas", WFRule.Name)

            If ((NewName <> String.Empty) AndAlso (NewName <> WFRule.Name)) Then
                WFRule.Name = NewName
                WFRulesFactory.UpdateRuleName(WFRule)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Método que sirve para cambiar el nombre de una acción de usuario

    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Tomás] 17/04/2009  Created
    ''' [Sebastian] 19/10/2009 Modified se agrego la llamada a un formulario que simula un input  box
    ''' </history>
    Friend Sub ChangeUserActionName(ByVal userActionNode As RuleTypeNode)
        Dim frm As frmInputBox
        Try
            'Obtiene el nodo de la acción de usuario
            Dim BaseNode As BaseWFNode = userActionNode.Nodes(0)
            Dim oldName As String = BaseNode.Parent.Text.Trim
            Dim NewName As String
            'Obtengo el nuevo nombre
            frm = New frmInputBox("Ingrese el nombre de la accion de usuario", 2000, oldName, "Ingrese el nuevo nombre de la accion de usuario", True)
            'Dim NewName As String = InputBox("Ingrese el nuevo nombre de la acción de usuario", "Edición de acción de usuario", oldName).Trim

            frm.StartPosition = FormStartPosition.CenterParent
            frm.BringToFront()
            frm.ShowDialog()
            If frm.DialogResult = DialogResult.OK Then
                NewName = frm.txtUserText.Text
            End If
            'Si este no es vacío se aplican los cambios
            If (Not String.IsNullOrEmpty(NewName) AndAlso String.Compare(NewName, oldName) <> 0) Then
                If (NewName.Length <= 2000) Then
                    'Obtiene el ID de la regla
                    Dim WFRule As WFRuleParent
                    WFRule = DirectCast(BaseNode, RuleNode).Rule

                    'Este método sirve tanto para el update como para el insert
                    WF.SetRulesPreferences(WFRule.ID, RuleSectionOptions.Regla, RulePreferences.UserActionName, 0, NewName)

                    'Actualiza el nodo del tree
                    userActionNode.UpdateUserActionNodeName(NewName)
                Else
                    Dim ex As New Exception("El tamaño máximo para el nombre de la acción de usuario excede los 2000 caracteres")
                    ZClass.raiseerror(ex)
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            frm.Dispose()
        End Try
    End Sub

    ''' <summary>
    ''' Método que sirve para cambiar el nombre de una regla
    ''' </summary>
    ''' <param name="ruleNode">Instancia de una regla seleccionada</param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    16/04/2009  Modified    Se agrego un límite para la cantidad de caracteres máximo que puede tener el nombre de una regla
    '''     [Tomas]     11/06/2009  Modified    Se modifica la forma en que cambia el nombre de la acción de usuario. Se valida para que
    '''                                         si tiene existe un nombre en la Zruleopt no lo modifique.
    '''     [Sebastian] 19/10/2009 Modified se agrego la llamada a un formulario que simula un input  box
    '''     [Tomas]     06/11/2009  Modified    Se modifica la validacion ya que arrojaba exceptions cuando no debia. Se libera la memoria.
    ''' </history>
    Friend Sub ChangeRuleName(ByVal ruleNode As RuleNode)
        Dim frm As frmInputBox
        Dim BaseNode As BaseWFNode = ruleNode
        Dim WFRule As WFRuleParent
        Dim NewName As String
        Try
            'Dim BaseNode As BaseWFNode = ruleNode
            'Dim WFRule As WFRuleParent
            'Dim NewName As String
            WFRule = DirectCast(BaseNode, RuleNode).Rule
            frm = New frmInputBox("Ingrese el nombre de la regla", 2000, WFRule.Name, "Ingrese el nuevo nombre de la regla", False)
            'Dim NewName As String  = InputBox("Ingrese el nuevo nombre de la regla", "Edicion de Reglas", WFRule.Name)
            frm.StartPosition = FormStartPosition.CenterParent
            frm.BringToFront()
            frm.ShowDialog()
            If frm.DialogResult <> DialogResult.Cancel Then
                NewName = frm.txtUserText.Text.Replace(Chr(39), "")
            End If
            If ((Not String.IsNullOrEmpty(NewName)) AndAlso String.Compare(NewName, WFRule.Name) <> 0) Then
                'If (NewName.Length <= 2000) Then
                WFRule.Name = NewName
                WFRulesFactory.UpdateRuleName(WFRule)
                ruleNode.UpdateRuleNodeName(WFRule)

                If (WFRule.ParentType = TypesofRules.AccionUsuario) Then
                    'Obtiene el dataset donde se encuentra nombre de la acción de usuario asociada a esa regla
                    Dim dt As DataRow() = GetRuleOption(WFRule.ID, 0, 43, 0)
                    'Valida si existen datos
                    If dt.Count = 0 Then
                        'Si no tiene nombre la acción de usuario lo modifica por defecto con el nombre de la primer regla
                        DirectCast(WFRule.RuleNode, RuleNode).PrevVisibleNode.Text = "Acción de Usuario - " & WFRule.Name
                    End If
                    'Else
                End If
                'End If

                'Else
                '    Dim ex As New Exception("El tamaño máximo para el nombre de la regla excede los 2000 caracteres")
                '    ZClass.raiseerror(ex)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            If Not IsNothing(frm) Then
                frm.Dispose()
                frm = Nothing
            End If
            If Not IsNothing(BaseNode) Then BaseNode = Nothing
            If Not IsNothing(WFRule) Then WFRule = Nothing
            If Not IsNothing(NewName) Then NewName = Nothing
        End Try
    End Sub

#End Region

#Region "Load"
    Friend Sub LoadRules(ByVal wf As WorkFlow, ByVal treeView As TreeView, ByVal LoadTreePanel As Boolean)
        Try
            If Not LoadTreePanel Then
                treeView.Nodes.Clear()
            End If

            'Nodo Inicial
            Dim Toproot As New WFNode(wf)
            treeView.Nodes.Add(Toproot)
            'Nodos de Etapas
            For Each s As WFStep In wf.Steps.Values
                Try
                    Dim StepNode As New EditStepNode(s, wf.InitialStep)
                    If LoadTreePanel Then
                        '(pablo)
                        'quito los nodos innecesarios para el
                        'arbol de habilitacion de reglas
                        StepNode.Nodes.Remove(StepNode.RightNode)
                        StepNode.Nodes.Remove(StepNode.EventNode)
                        StepNode.Nodes.Remove(StepNode.InputNode)
                        StepNode.Nodes.Remove(StepNode.InputValidationNode)
                        StepNode.Nodes.Remove(StepNode.OutputNode)
                        StepNode.Nodes.Remove(StepNode.OutputValidationNode)
                        StepNode.Nodes.Remove(StepNode.UpdateNode)
                        StepNode.Nodes.Remove(StepNode.ScheduleNode)
                    End If
                    Toproot.Nodes.Add(StepNode)

                    AddRulesNodes(StepNode, LoadTreePanel)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Next
            ' TreeView.ExpandAll()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Friend Sub LoadMonitorRules(ByRef wfstep As WFStep, ByVal TreeView As TreeView)
        Dim WF As New WFBusiness
        Try
            TreeView.Nodes.Clear()
            Dim workflow As IWorkFlow
            workflow = WF.GetWFbyId(wfstep.WorkId)
            Dim StepNode As New EditStepNode(wfstep, workflow.InitialStep)
            AddRulesNodes(StepNode, False)
            'agrego los subnodos de wfstepnode al treeview
            For Each n As TreeNode In StepNode.Nodes
                If Not TypeOf n Is RightNode Then
                    TreeView.Nodes.Add(n)
                End If
            Next
            TreeView.ExpandAll()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            WF = Nothing
        End Try
    End Sub
    Private Sub AddRulesNodes(ByVal stepNode As EditStepNode, ByVal LoadTreePanel As Boolean)
        Try
            'Agrega las reglas padres que dependen del primer nodo del tipo de reglas
            Dim IsFirst As Boolean = True
            For Each r As WFRuleParent In stepNode.WFStep.Rules
                If IsNothing(r.ParentRule) OrElse r.ParentRule.ID = 0 Then
                    Select Case r.ParentType
                        Case TypesofRules.Entrada
                            AddRuleNode(stepNode.InputNode, r, r.DisableChildRules, LoadTreePanel)
                        Case TypesofRules.ValidacionEntrada
                            AddRuleNode(stepNode.InputValidationNode, r, r.DisableChildRules, LoadTreePanel)
                        Case TypesofRules.Salida
                            AddRuleNode(stepNode.OutputNode, r, r.DisableChildRules, LoadTreePanel)
                        Case TypesofRules.ValidacionSalida
                            AddRuleNode(stepNode.OutputValidationNode, r, r.DisableChildRules, LoadTreePanel)
                        Case TypesofRules.Actualizacion
                            AddRuleNode(stepNode.UpdateNode, r, r.DisableChildRules, LoadTreePanel)
                        Case TypesofRules.Planificada
                            AddRuleNode(stepNode.ScheduleNode, r, r.DisableChildRules, LoadTreePanel)
                        Case TypesofRules.Eventos
                            AddEventRuleNode(stepNode.EventNode, r)
                        Case TypesofRules.AccionUsuario
                            Dim UserActionNode As RuleTypeNode
                            Dim Name As String = String.Empty

                            If IsFirst Then
                                UserActionNode = stepNode.UserActionNode
                                IsFirst = False
                            Else
                                UserActionNode = New RuleTypeNode(TypesofRules.AccionUsuario)
                                stepNode.Nodes.Insert(stepNode.Nodes.IndexOf(stepNode.ScheduleNode), UserActionNode)
                            End If

                            AddRuleNode(UserActionNode, r, r.DisableChildRules, LoadTreePanel)


                            'Actualiza el nombre
                            Try
                                Dim dt As DataRow() = GetRuleOption(r.ID, 0, 43, 0)
                                If dt.Count > 0 Then
                                    Name = dt(0).Item("ObjExtraData").ToString()
                                End If
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try

                            'Si el nombre es vacío entonces el nombre del nodo será
                            'tomado como antes se hacia (Accion de usuario - [regla]"),
                            'en cambio 
                            If String.IsNullOrEmpty(Name) Then
                                UserActionNode.UpdateUserActionNodeName(r)
                            Else
                                UserActionNode.UpdateUserActionNodeName(Name)
                            End If

                        Case TypesofRules.Floating
                            AddFloatingRuleNode(stepNode.FloatingNode, r, LoadTreePanel)
                    End Select
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Function AddRuleNode(ByVal parentNode As BaseWFNode, ByRef rule As WFRuleParent, ByVal DisableChilds As Boolean, ByVal LoadTreePanel As Boolean) As Boolean
        Try
            'agrega la regla al nodo y verifica si tiene child para agregar
            Dim RuleNode As New RuleNode(rule)
            'RuleNode.Tag = rule.WFStepId

            If rule.Enable = False Or DisableChilds = True Then
                RuleNode.ImageIndex = 37
                RuleNode.SelectedImageIndex = 37
            Else
                RuleNode.ImageIndex = rule.IconId
                RuleNode.SelectedImageIndex = rule.IconId
            End If
            parentNode.Nodes.Add(RuleNode)

            'If Not LoadTreePanel Then
            '    For Each R As WFRuleParent In rule.ChildRules
            '        AddRuleNode(RuleNode, R, DisableChilds, LoadTreePanel)
            '    Next
            'End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return DisableChilds
    End Function


    ''' <summary>
    ''' Método que sirve para agregar los nodos relacionados a eventos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Tomas] 21/04/2009  Created
    ''' </history>
    Private Sub AddEventRuleNode(ByVal parentNode As BaseWFNode, ByRef rule As WFRuleParent, Optional ByRef Rnode As RuleNode = Nothing)
        Dim contieneEvento As Boolean = False
        Dim eventoIndex As Int32
        Dim ruleTypeName As String = rule.RuleType.ToString()
        Try
            Dim RuleNode As RuleNode

            If Not Rnode Is Nothing Then
                RuleNode = Rnode
            Else
                RuleNode = New RuleNode(rule)
            End If

            'Comprueba que el nodo con el nombre del evento exista
            For Each nodo As TreeNode In parentNode.Nodes
                If String.Compare(nodo.Text, ruleTypeName) = 0 Then
                    contieneEvento = True
                    eventoIndex = nodo.Index
                    Exit For
                End If
            Next

            'En caso de existir se agrega a ese nodo, en caso de no 
            'existir, lo crea y luego se agrega al nodo.
            If contieneEvento Then
                'Obtiene el nodo del evento donde se agregará la regla
                Dim EventNode As TreeNode = parentNode.Nodes(eventoIndex)
                'Agrego la regla al nodo del evento que corresponde
                EventNode.Nodes.Add(RuleNode)
                'Agrego las reglas hijas a la regla principal
                'For Each R As WFRuleParent In rule.ChildRules
                '    AddRuleNode(RuleNode, R, R.DisableChildRules, False)
                'Next

            Else
                'Agrega el nodo del evento
                Dim EventNode As New TreeNode(rule.RuleType.ToString)
                EventNode.NodeFont = New Font(FontFamily.GenericSansSerif, 7, FontStyle.Regular)
                EventNode.ImageIndex = 28
                EventNode.SelectedImageIndex = 28

                parentNode.Nodes.Add(EventNode)
                'Agrega el nodo de la regla al nodo del evento y luego sus hijos.
                EventNode.Nodes.Add(RuleNode)
                'For Each R As WFRuleParent In rule.ChildRules
                '    AddRuleNode(RuleNode, R, R.DisableChildRules, False)
                'Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            contieneEvento = Nothing
            eventoIndex = Nothing
        End Try
    End Sub
    Private Function GetEventFirstRule(ByVal nodeType As TypesofRules, ByVal eventNode As BaseWFNode) As RuleNode
        Try
            Dim eventIndex As Int32
            Dim nodeTypeName As String = nodeType.ToString()

            'Encuentra el índice del tipo de evento
            For Each node As TreeNode In eventNode.Nodes
                If String.Compare(node.Text, nodeTypeName) = 0 Then
                    eventIndex = node.Index
                    Exit For
                End If
            Next

            Return DirectCast(eventNode.Nodes(eventIndex).Nodes(0), RuleNode)

        Catch ex As Exception
            ZCore.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Private Sub AddFloatingRuleNode(ByVal ParentNode As BaseWFNode, ByRef rule As WFRuleParent, ByVal LoadTreePanel As Boolean)
        Try
            'agrega la regla al nodo y verifica si tiene child para agregar
            Dim FRuleNode As New FloatingNode(rule)
            ParentNode.Nodes.Add(FRuleNode)
            If Not LoadTreePanel Then
                'For Each R As WFRuleParent In rule.ChildRules
                '    AddFloatingRuleNode(FRuleNode, R, LoadTreePanel)
                'Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Application"
    ''' <summary>
    ''' Envia una alerta de ejecucion de regla Mediante Mensaje Interno, Recuperando datos de la base ZRuleOpt
    ''' </summary>
    ''' <param name="ruleid"></param>
    ''' <remarks></remarks>
    Private Sub SendInternalMessageAlert(ByVal ruleid As Int64)
        Try
            Dim _Body As String
            Dim ToMails As Generic.List(Of User)
            Dim CCMails As Generic.List(Of User)
            Dim CCOMails As Generic.List(Of User)
            Dim _Subject As String
            Dim dt As DataRow()
            'Todo: Mensaje Interno Falta Adjuntar Documento

            'Obtengo el Body
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertInternalMessageBody)
            If dt.Count > 0 Then
                _Body = dt(0).Item("ObjExtraData").ToString
            End If

            'Obtengo El Subject
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertInternalMessageSubject)
            If dt.Count > 0 Then
                _Subject = dt(0).Item("ObjExtraData").ToString
            End If

            Dim tempUser As User

            'Obtengo los usuarios (PARA)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailFor, 1)

            For Each dr As DataRow In dt
                If IsNothing(ToMails) Then
                    ToMails = New Generic.List(Of User)
                End If
                tempUser = UserBusiness.GetUserById(Int64.Parse(dr.Item(0)))

                If ToMails.Contains(tempUser) = False Then
                    ToMails.Add(tempUser)
                End If
            Next

            'Obtengo los usuarios de los grupos (PARA)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailFor, 2)
            For Each dr As DataRow In dt
                For Each Item As User In UserGroupBusiness.GetUsersByGroup(Int64.Parse(dr.Item(0).ToString))
                    If IsNothing(ToMails) Then
                        ToMails = New Generic.List(Of User)
                    End If
                    If ToMails.Contains(Item) = False Then
                        ToMails.Add(Item)
                    End If
                Next
            Next



            'Obtengo los usuarios (CC)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCC, 1)

            For Each dr As DataRow In dt
                If IsNothing(CCMails) Then
                    CCMails = New Generic.List(Of User)
                End If
                tempUser = UserBusiness.GetUserById(Int64.Parse(dr.Item(0)))

                If CCMails.Contains(tempUser) = False Then
                    CCMails.Add(tempUser)
                End If
            Next

            'Obtengo los usuarios de los grupos (CC)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCC, 2)
            For Each dr As DataRow In dt
                For Each Item As User In UserGroupBusiness.GetUsersByGroup(Int64.Parse(dr.Item(0).ToString))
                    If IsNothing(CCMails) Then
                        CCMails = New Generic.List(Of User)
                    End If
                    If CCMails.Contains(Item) = False Then
                        CCMails.Add(Item)
                    End If
                Next
            Next

            'Obtengo los usuarios (CCO)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCCO, 1)

            For Each dr As DataRow In dt
                If IsNothing(CCOMails) Then
                    CCOMails = New Generic.List(Of User)
                End If
                tempUser = UserBusiness.GetUserById(Int64.Parse(dr.Item(0)))

                If CCOMails.Contains(tempUser) = False Then
                    CCOMails.Add(tempUser)
                End If
            Next

            'Obtengo los usuarios de los grupos (CCO)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCCO, 2)
            For Each dr As DataRow In dt
                For Each Item As User In UserGroupBusiness.GetUsersByGroup(Int64.Parse(dr.Item(0).ToString))
                    If IsNothing(CCOMails) Then
                        CCOMails = New Generic.List(Of User)
                    End If
                    If CCOMails.Contains(Item) = False Then
                        CCOMails.Add(Item)
                    End If
                Next
            Next



            'Envia Mensaje Interno Con destinatarios PARA
            If Not IsNothing(ToMails) Then
                InternalMessage.SendInternalMessage(_Subject, Date.Now, _Body, ToMails, MessageType.MailTo)
            End If
            'Envia Mensaje Interno Con destinatarios CC
            If Not IsNothing(CCMails) Then
                InternalMessage.SendInternalMessage(_Subject, Date.Now, _Body, CCMails, MessageType.MailCC)
            End If
            'Envia Mensaje Interno Con destinatarios CCO
            If Not IsNothing(CCOMails) Then
                InternalMessage.SendInternalMessage(_Subject, Date.Now, _Body, CCOMails, MessageType.MailCCO)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Envia una Alerta de ejecucion de regla Mediante un Mail, Recuperando datos de la base ZRuleOpt
    ''' [sebastian 06-04-09] [modified] se le modifico para pasar a send mail las dires de mail de los usuarios
    ''' </summary>
    ''' <param name="ruleid"></param>
    ''' <remarks></remarks>
    Private Sub SendMailAlert(ByVal ruleid As Int64, ByVal results As List(Of ITaskResult))
        Dim mail As New SendMailConfig

        Try
            Dim _body As String
            Dim _Subject As String
            Dim ToMails As StringBuilder '[sebastian 06-04-09] it changed to stringbuilder
            Dim CCMails As Generic.List(Of String)
            Dim CCOMails As Generic.List(Of String)
            Dim dt As DataRow()


            'Obtengo el Body
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailBody)
            If dt.Count > 0 Then
                _body = dt(0).Item("ObjExtraData").ToString
            End If

            'Obtengo el Subject
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailSubject)
            If dt.Count > 0 Then
                _Subject = dt(0).Item("ObjExtraData").ToString
            End If

            Dim tempMail As String

            'Obtengo los usuarios (PARA)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailFor, 1)

            For Each dr As DataRow In dt
                If IsNothing(ToMails) Then
                    ToMails = New StringBuilder
                End If
                tempMail = (UserBusiness.GetUserById(Int64.Parse(dr.Item("ObjValue")))).eMail.Mail

                If ToMails.ToString.Contains(tempMail) = False Then
                    ToMails.Append(tempMail)
                    ToMails.Append(";")
                End If
            Next

            'Obtengo los usuarios de los grupos (PARA)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailFor, 2)
            For Each dr As DataRow In dt
                For Each Item As User In UserGroupBusiness.GetUsersByGroup(Int64.Parse(dr.Item("ObjValue").ToString))
                    If IsNothing(ToMails) Then
                        ToMails = New StringBuilder
                    End If
                    If ToMails.ToString.Contains(Item.eMail.Mail) = False Then
                        ToMails.Append(Item.eMail.Mail)
                        ToMails.Append(";")
                    End If
                Next
            Next

            'Obtengo los Mails Externos (PARA)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailFor, 3)
            For Each dr As DataRow In dt
                If IsNothing(ToMails) Then
                    ToMails = New StringBuilder
                End If
                If ToMails.ToString.Contains(dr.Item("ObjValue").ToString) = False Then
                    ToMails.Append(dr.Item("ObjValue").ToString)
                    ToMails.Append(";")
                End If
            Next

            'Obtengo los usuarios (CC)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCC, 1)

            For Each dr As DataRow In dt
                If IsNothing(CCMails) Then
                    CCMails = New Generic.List(Of String)
                End If
                tempMail = (UserBusiness.GetUserById(Int64.Parse(dr.Item("ObjValue")))).eMail.Mail

                If CCMails.Contains(tempMail) = False Then
                    CCMails.Add(tempMail)
                End If
            Next

            'Obtengo los usuarios de los grupos (CC)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCC, 2)
            For Each dr As DataRow In dt
                For Each Item As User In UserGroupBusiness.GetUsersByGroup(Int64.Parse(dr.Item("ObjValue").ToString))
                    If IsNothing(CCMails) Then
                        CCMails = New Generic.List(Of String)
                    End If
                    If CCMails.Contains(Item.eMail.Mail) = False Then
                        CCMails.Add(Item.eMail.Mail)
                    End If
                Next
            Next

            'Obtengo los Mails Externos (CC)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCC, 3)
            For Each dr As DataRow In dt
                If IsNothing(CCMails) Then
                    CCMails = New Generic.List(Of String)
                End If
                If CCMails.Contains(dr.Item("ObjValue").ToString) = False Then
                    CCMails.Add(dr.Item("ObjValue").ToString)
                End If
            Next

            'Obtengo los usuarios (CCO)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCCO, 1)

            For Each dr As DataRow In dt
                If IsNothing(CCOMails) Then
                    CCOMails = New Generic.List(Of String)
                End If
                tempMail = (UserBusiness.GetUserById(Int64.Parse(dr.Item("ObjValue")))).eMail.Mail

                If CCOMails.Contains(tempMail) = False Then
                    CCOMails.Add(tempMail)
                End If
            Next

            'Obtengo los usuarios de los grupos (CCO)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCCO, 2)
            For Each dr As DataRow In dt
                For Each Item As User In UserGroupBusiness.GetUsersByGroup(Int64.Parse(dr.Item("ObjValue").ToString))
                    If IsNothing(CCOMails) Then
                        CCOMails = New Generic.List(Of String)
                    End If
                    If CCOMails.Contains(Item.eMail.Mail) = False Then
                        CCOMails.Add(Item.eMail.Mail)
                    End If
                Next
            Next

            'Obtengo los Mails Externos (CCO)
            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCCO, 3)
            For Each dr As DataRow In dt
                If IsNothing(CCOMails) Then
                    CCOMails = New Generic.List(Of String)
                End If
                If CCOMails.Contains(dr.Item("ObjValue").ToString) = False Then
                    CCOMails.Add(dr.Item("ObjValue").ToString)
                End If
            Next

            Dim cc As String
            If IsNothing(CCMails) = False Then
                For Each ccMail As String In CCMails
                    cc += ccMail
                    cc += ";"
                Next
                cc -= ";"
            End If

            Dim cco As String
            If IsNothing(CCOMails) = False Then
                For Each ccoMail As String In CCOMails
                    cco += ccoMail
                    cco += ";"
                Next
                cco -= ";"
            End If

            mail.MailTo = ToMails.ToString
            mail.Cc = cc
            mail.Cco = cco
            mail.Subject = _Subject
            mail.Body = _body
            mail.IsBodyHtml = True

            MessagesBusiness.SendQuickMail(mail)


        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            mail.Dispose()
            mail = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Envia una alerta de ejecucion de regla Mediante Automail, Recuperando datos de la base ZRuleOpt
    ''' </summary>
    ''' <param name="ruleid"></param>
    ''' <remarks></remarks>
    Private Sub SendAutomailAlert(ByVal ruleid As Int64)
        Try
            Dim dt As DataRow()
            Dim _automail As AutoMail = Nothing
            Dim _userName As String
            Dim _userPass As String
            Dim _port As String
            Dim _server As String
            Dim _ssl As Boolean


            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailId)
            If dt.Count > 0 Then _automail = MessagesBusiness.GetAutomailById(dt(0).Item("ObjValue"))
            If _automail Is Nothing Then Throw New ArgumentException("No existe configuración de automail")

            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPUser)
            If dt.Count > 0 Then _userName = dt(0).Item("ObjExtraData").ToString

            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPPass)
            If dt.Count > 0 Then _userPass = dt(0).Item("ObjExtraData").ToString

            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPPort)
            If dt.Count > 0 Then _port = dt(0).Item("ObjExtraData").ToString

            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPProvider)
            If dt.Count > 0 Then _server = dt(0).Item("ObjExtraData").ToString

            dt = GetRuleOption(ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPSSL)
            If dt.Count > 0 Then
                If Not Boolean.TryParse(dt(0).Item("ObjExtraData").ToString, _ssl) Then
                    _ssl = False
                End If
            End If

            Dim smtp As SMTP_Validada = New SMTP_Validada(_userName, _userPass, Int32.Parse(_port), _server, _ssl)
            MessagesBusiness.AutoMail_SMTP(_automail, smtp)


        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Envia una alerta de Notificacion cuando se ejecuta cierta regla ( definido por administrador)
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <remarks></remarks>
    Private Sub SendNotificationAlert(ByRef rule As WFRuleParent, ByVal results As List(Of ITaskResult))


        Dim dt As DataRow() = GetRuleOption(rule.ID, RuleSectionOptions.Alerta, RulePreferences.AlertNotificationMode)


        If dt.Count > 0 Then
            If dt(0).Item(0) = 1 Then
                For Each Item As String In dt(0).Item(1).ToString.ToUpper.Split("|")
                    If Not String.IsNullOrEmpty(Item) Then
                        Select Case Item
                            Case "MENSAJE INTERNO"
                                SendInternalMessageAlert(rule.ID)
                            Case "MAIL"
                                SendMailAlert(rule.ID, results)
                            Case "AUTOMÁTICO"
                                SendAutomailAlert(rule.ID)
                        End Select
                    End If
                Next
            End If
        End If
    End Sub


    ''' <summary>
    ''' Ejecuta las reglas de entrada
    ''' </summary>
    ''' <param name="taskResults">Coleccion de taskResults o tareas pasados por referencia</param>
    ''' <remarks></remarks>
    ''' <history>[Diego]02-07-2008 Modified</history>
    Public Sub ExecuteStartRules(ByRef taskResults As List(Of ITaskResult))
        Dim WFSB As New WFStepBusiness

        Try
            If (taskResults.Count < 1 AndAlso IsNothing(taskResults(0))) Then Exit Sub

            Dim wfstep As IWFStep = WFSB.GetStepById(taskResults(0).StepId)
            For Each Rule As WFRuleParent In wfstep.Rules
                If Rule.RuleType = TypesofRules.Iniciar Then

                    taskResults = ExecuteRule(Rule, taskResults, False)
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            WFSB = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' Setea el usuario asignado segun preferencia de regla
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <param name="newresults"></param>
    ''' <remarks></remarks>
    ''' <history>[Diego]28-07-2008 Created</history>
    Public Sub SetTasksAsignedUser(ByVal ruleId As Int64, ByRef newresults As List(Of ITaskResult))
        Dim WF As New WFBusiness
        Try
            Dim dt As DataTable = WF.recoverItemsSelected(ruleId, RuleSectionOptions.Asignacion, RulePreferences.AsignationTypeUser, RulePreferences.AsignationTypeGroup, RulePreferences.AsignationTypeManual)

            If dt.Rows.Count < 1 Then Exit Sub

            Dim id As Long = 0
            Dim Username As String = String.Empty

            Dim Selection As Integer = dt.Rows(0).Item("OBJECTID")
            Select Case Selection
                Case RulePreferences.AsignationTypeUser, RulePreferences.AsignationTypeGroup
                    id = dt.Rows(0).Item("OBJVALUE")
                    Dim IsGroup As Boolean
                    Username = UserGroupBusiness.GetUserorGroupNamebyId(id, IsGroup)
                Case RulePreferences.AsignationTypeManual
                    Dim alternateuser As String
                    alternateuser = dt.Rows(0).Item("OBJEXTRADATA")

                    'Martin: Se agrego para que se puedan contemplar el id de usuario o nombre de usuario en el campo manual de la asignacion del tab de tareas
                    If alternateuser.ToLower.IndexOf("zvar") <> -1 Then
                        If IsNumeric(VariablesInterReglas.Item(alternateuser)) Then
                            id = Int32.Parse(VariablesInterReglas.Item(alternateuser))
                        ElseIf (TypeOf (VariablesInterReglas.Item(alternateuser)) Is DataSet) Then
                            Dim dstemp As DataSet = VariablesInterReglas.Item(alternateuser)
                            'faltaria que en la configuracion se pudiera elegir la columna a tomar, hoy esta fijo en la columna 2 indice 1
                            id = Int32.Parse(dt.Rows(0)(1).ToString())
                        Else
                            Username = VariablesInterReglas.Item(alternateuser)
                            id = UserGroupBusiness.GetUserorGroupIdbyName(Username)
                        End If
                    Else
                        If IsNumeric(alternateuser) Then
                            id = alternateuser
                        Else
                            Username = alternateuser
                            id = UserGroupBusiness.GetUserorGroupIdbyName(Username)
                        End If
                    End If

            End Select
            Dim WFTB As New WFTaskBusiness
            For Each r As ITaskResult In newresults
                WFTB.Asign(r, id, Zamba.Membership.MembershipHelper.CurrentUser.ID, Username)
            Next
            WFTB = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            WF = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Setea el usuario asignado segun preferencia de regla
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <param name="newresults"></param>
    ''' <remarks></remarks>
    ''' <history>[Diego]28-07-2008 Created</history>
    Public Sub SetTasksAsignedUser(ByVal rule As WFRuleParent, ByRef newresults As List(Of ITaskResult))
        Dim WF As New WFBusiness
        Dim dt As DataTable = WF.recoverItemsSelected(rule.ID, RuleSectionOptions.Asignacion, RulePreferences.AsignationTypeUser, RulePreferences.AsignationTypeGroup, RulePreferences.AsignationTypeManual)
        WF = Nothing

        If dt.Rows.Count < 1 Then Exit Sub

        Dim id As Long = 0
        Dim Username As String = String.Empty

        Dim Selection As Integer = dt.Rows(0).Item("OBJECTID")
        Select Case Selection
            Case RulePreferences.AsignationTypeUser, RulePreferences.AsignationTypeGroup
                id = dt.Rows(0).Item("OBJVALUE")
                Dim IsGroup As Boolean
                Username = UserGroupBusiness.GetUserorGroupNamebyId(id, IsGroup)
            Case RulePreferences.AsignationTypeManual
                Dim alternateuser As String
                alternateuser = dt.Rows(0).Item("OBJEXTRADATA")

                'Martin: Se agrego para que se puedan contemplar el id de usuario o nombre de usuario en el campo manual de la asignacion del tab de tareas
                If alternateuser.ToLower.IndexOf("zvar") <> -1 Then
                    If IsNumeric(VariablesInterReglas.Item(alternateuser)) Then
                        id = Int32.Parse(VariablesInterReglas.Item(alternateuser))
                    ElseIf (TypeOf (VariablesInterReglas.Item(alternateuser)) Is DataSet) Then
                        Dim dstemp As DataSet = VariablesInterReglas.Item(alternateuser)
                        'faltaria que en la configuracion se pudiera elegir la columna a tomar, hoy esta fijo en la columna 2 indice 1
                        id = Int32.Parse(dt.Rows(0)(1).ToString())
                    Else
                        Username = VariablesInterReglas.Item(alternateuser)
                        id = UserGroupBusiness.GetUserorGroupIdbyName(Username)
                    End If
                Else
                    If IsNumeric(alternateuser) Then
                        id = alternateuser
                    Else
                        Username = alternateuser
                        id = UserGroupBusiness.GetUserorGroupIdbyName(Username)
                    End If
                End If

        End Select
        Dim WFTB As New WFTaskBusiness
        For Each r As ITaskResult In newresults
            WFTB.Asign(r, id, Zamba.Membership.MembershipHelper.CurrentUser.ID, Username)
        Next
        WFTB = Nothing
    End Sub

    Private Function ReturnInstanceRule(
                                        ByVal dsparamitemsrule As DataTable,
                                        ByVal r As DataRow,
                                       ByVal parentType As TypesofRules,
                                        Optional ByVal parentRule As WFRuleParent = Nothing) As WFRuleParent
        Try
            Dim ru As WFRuleParent
            Dim Rule As WFRuleParent = Nothing
            Dim ruleParamRowItems() As DataRow
            ruleParamRowItems = dsparamitemsrule.Select("Rule_id=" & r.Item("Id").ToString) ', "item"
            InstanceRule(Rule, ruleParamRowItems, r)

            If Not Rule Is Nothing Then
                Rule.ParentRule = parentRule
                Rule.ParentType = parentType
                Rule.ID = Int64.Parse(r.Item("Id").ToString)
                Rule.Name = r.Item("Name").ToString
                Rule.Version = Int32.Parse(r.Item("Version").ToString)
                Rule.Enable = r.Item("Enable")

                FillRulePreference(Rule)

                'Rule.ChildRulesIds = GetChildRulesIds(Rule.ID, Rule.RuleClass, Results)
                'For Each SubRule As DataRow In dsRules.Rows
                '    'If (SubRule.Item("ParentType") = 10 OrElse SubRule.Item("ParentType") >= 30) AndAlso SubRule.Item("ParentId") = Rule.ID Then
                '    If (SubRule.Item("ParentId") = Rule.ID Then
                '        Try
                '            ru = ReturnInstanceRule(dsRules, dsparamitemsrule, SubRule, stepid, TypesofRules.Regla, Rule)
                '            If IsNothing(ru) Then Throw New Exception("La regla hija es nothing " & Rule.Name & " " & Rule.ID)
                '            Rule.ChildRules.Add(ru)
                '        Catch ex As Exception
                '            ZClass.raiseerror(ex)
                '        End Try
                '    End If
                'Next
            End If

            Return Rule
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Private Sub InstanceRule(ByRef rule As WFRuleParent,
                             ByVal ruleParamRowItems() As DataRow,
                             ByVal ruleClass As String,
                             ByVal ruleId As Int64,
                             ByVal ruleName As String,
                             ByVal ruleType As TypesofRules,
                             ByVal stepId As Int64)
        Try
            Dim engine As Assembly = GetEngineAssembly()
            Dim classType As System.Type = GetClassType(engine, ruleClass)
            engine = Nothing

            If classType IsNot Nothing Then
                Dim classConstructor As ConstructorInfo = classType.GetConstructors.GetValue(0)
                Dim i As Byte
                Dim y As Byte

                If classConstructor IsNot Nothing Then
                    Dim args(classConstructor.GetParameters.Length - 1) As Object
                    args(0) = ruleId
                    args(1) = ruleName
                    args(2) = stepId

                    For Each p As ParameterInfo In classConstructor.GetParameters
                        If i > 2 Then
                            If ruleParamRowItems.Length >= y + 1 Then
                                FillArgsValues(p, args(i), ruleParamRowItems(y).Item("Value"))
                            Else
                                FillArgsValues(p, args(i), GetArgInitialValues(p))
                            End If
                            y += 1
                        End If
                        i += 1
                    Next

                    rule = Activator.CreateInstance(classType, args)
                    rule.RuleType = ruleType
                End If

                classType = Nothing
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Tipo no reconocido: " & ruleClass)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Function GetEngineAssembly() As Assembly
        If GlobalRulesEngine.GetInstance.IsAssemblyNull Then
            Dim engine As Assembly = Assembly.LoadFile(Zamba.Membership.MembershipHelper.StartUpPath & "\Zamba.WFActivity.Regular.dll")
            GlobalRulesEngine.GetInstance.AddAssembly(engine)
            Return engine
        Else
            Return GlobalRulesEngine.GetInstance.GetAssembly
        End If
    End Function

    Private Function GetClassType(ByVal engine As Assembly, ByVal className As String) As Type
        If Not GlobalRulesEngine.GetInstance.ContainsClass(className) Then
            Try
                Dim classType As Type = engine.GetType("Zamba.WFActivity.Regular." & className, True, True)
                GlobalRulesEngine.GetInstance.AddClassType(className, classType)
                Return classType
            Catch ex As Exception
                Return Nothing
            End Try
        Else
            Return GlobalRulesEngine.GetInstance.GetClassType(className)
        End If
    End Function

    Private Sub InstanceRule(ByRef rule As WFRuleParent,
                             ByVal ruleParamRowItems() As DataRow,
                             ByVal ruleRow As DataRow
                            )
        InstanceRule(rule, ruleParamRowItems, ruleRow("Class"), CLng(ruleRow("Id")), ruleRow("Name"), DirectCast(CInt(ruleRow("Type")), TypesofRules), ruleRow("Step_Id"))
    End Sub

    Public Function GetRuleParamItems(ByVal p_iRuleID As Int32) As DataSet
        Try
            Dim newDsrules As New DataSet

            If Cache.Workflows.hsRuleParamsDS.ContainsKey(p_iRuleID) = False Then
                Cache.Workflows.hsRuleParamsDS(p_iRuleID) = WFRulesFactory.GetRuleParamItems(p_iRuleID)
            End If
            newDsrules = Cache.Workflows.hsRuleParamsDS(p_iRuleID)

            Return newDsrules
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return Nothing
    End Function
#End Region

#Region "Set"
    Public Sub SetRuleEstado(ByVal p_iRuleId As Int32, ByVal p_bEstado As Boolean)
        WFRulesFactory.UpdateRuleById(p_iRuleId, p_bEstado)
    End Sub

    Public Sub SetRulesInstances(ByVal listrule As List(Of IWFRuleParent), ByRef s As WFStep)
        SyncLock (s)
            For Each rule As IWFRuleParent In listrule
                If rule.ParentRule Is Nothing Then
                    Try
                        'WFRulesBusiness.GetRuleEstado(rule.ID, True)
                        Dim ru As WFRuleParent = ContainsRule(s, rule)
                        If IsNothing(ru) Then
                            s.Rules.Add(rule)
                        Else
                            ru = rule
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End If
            Next
        End SyncLock
    End Sub
#End Region

#Region "Conectores"
    'Dim OPoints As New ArrayList
    'Dim DPoints As New ArrayList

    Public Function FillTransitions(ByVal wf As WorkFlow) As ArrayList
        Try
            Me.ArrayPares.Clear()
            For Each s As WFStep In wf.Steps.Values
                For Each r As WFRuleParent In s.Rules
                    FindRulesDerivates(r)
                Next
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return ArrayPares
    End Function

    Dim ArrayPares As New ArrayList
    'Dim HashWfSteps As New Hashtable

    Sub UpdateParentRuleId(ByVal ruleID As Int64, ByVal parentRuleID As Int64)
        WFRulesFactory.UpdateParentRuleId(ruleID, parentRuleID)
    End Sub

    Public Sub FindRulesDerivates(ByRef rule As WFRuleParent)
        If Not IsNothing(rule) Then
            If rule.RuleClass.ToLower = "dodistribuir" Then
                Dim DoDistribuir As IDoDistribuir = rule
                Dim Vector(2) As String
                Vector(0) = DoDistribuir.WFStepId
                If DoDistribuir.NewWFStepId > 0 Then
                    Vector(1) = DoDistribuir.NewWFStepId
                    Vector(2) = rule.ID
                    Me.ArrayPares.Add(Vector)
                End If
            End If
            'For Each R As WFRuleParent In rule.ChildRules
            '    FindRulesDerivates(R)
            'Next
        End If
    End Sub

#End Region


    ''' <summary>
    ''' Carga las preferencias de la regla
    ''' </summary>
    ''' <param name="instance"></param>
    ''' <history>
    '''     Marcelo Modified 01/10/2009 - Changed Method Name
    '''     Marcelo Modified 29/11/2010 - Add New Preference
    ''' <history>
    ''' <remarks></remarks>
    Public Sub FillRulePreference(ByRef instance As IWFRuleParent)
        Dim dt As DataRow()


        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.ConfigurationExecuteWhenResult)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If dt(0).Item("Objvalue").ToString = "0" Then
                instance.ExecuteWhenResult = False
            Else
                instance.ExecuteWhenResult = True
            End If
        Else
            instance.ExecuteWhenResult = False
        End If

        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.RefreshRule)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If dt(0).Item("Objvalue").ToString = "0" Then
                instance.RefreshRule = False
            Else
                instance.RefreshRule = True
            End If
        Else
            instance.RefreshRule = False
        End If

        dt = GetRuleOption(instance.ID, RuleSectionOptions.Alerta, RulePreferences.AlertNotificationMode)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If dt(0).Item("Objvalue") = 1 Then
                'Tiene algun Checkbox de notificacion Checkeado
                instance.AlertExecution = True
            Else
                instance.AlertExecution = False
            End If
        Else
            instance.AlertExecution = False
        End If
        'End If

        '[Ezequiel] 05/06/2009
        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.ContinueWithError)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If dt(0).Item("Objvalue").ToString = "0" Then
                instance.ContinueWithError = False
            Else
                instance.ContinueWithError = True
            End If
        Else
            instance.ContinueWithError = False
        End If

        '[Ezequiel] 22/06/2009
        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.CloseTask)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If dt(0).Item("Objvalue").ToString = "0" Then
                instance.CloseTask = False
            Else
                instance.CloseTask = True
            End If
        Else
            instance.CloseTask = False
        End If

        '[Marcelo] 02/09/2009
        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.RuleHelp)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If Not IsDBNull(dt(0).Item("ObjExtraData")) Then
                instance.Description = dt(0).Item("ObjExtraData")
            Else
                instance.Description = String.Empty
            End If
        Else
            instance.Description = String.Empty
        End If

        '[Tomas] 16/09/2009
        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.CleanRule)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If dt(0).Item("Objvalue").ToString = "0" Then
                instance.CleanRule = False
            Else
                instance.CleanRule = True
            End If
        Else
            instance.CleanRule = False
        End If

        '[Tomas] 16/09/2009
        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.RuleCategory)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            instance.Category = Int16.Parse(dt(0).Item("Objvalue").ToString)
        Else
            instance.Category = 1
        End If

        '[Marcelo] 26/11/2010
        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.SaveUpdate)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If dt(0).Item("Objvalue").ToString = "0" Then
                instance.SaveUpdate = False
            Else
                instance.SaveUpdate = True
            End If
        Else
            instance.SaveUpdate = False
        End If

        '[Marcelo] 26/11/2010
        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.Comment)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If Not IsDBNull(dt(0).Item("ObjExtraData")) Then
                instance.UpdateComment = dt(0).Item("ObjExtraData")
            Else
                instance.UpdateComment = String.Empty
            End If
        Else
            instance.UpdateComment = String.Empty
        End If

        '[Marcelo] 15/12/2010
        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.SaveUpdateInHistory)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If dt(0).Item("Objvalue").ToString = "0" Then
                instance.SaveUpdateInHistory = False
            Else
                instance.SaveUpdateInHistory = True
            End If
        Else
            instance.SaveUpdateInHistory = False
        End If

        '[Marcelo] 15/12/2010
        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.Asynchronous)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If dt(0).Item("Objvalue").ToString = "0" Then
                instance.Asynchronous = False
            Else
                instance.Asynchronous = True
            End If
        Else
            instance.Asynchronous = False
        End If

        '[AlejandroR] 14/01/2010
        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.RuleIdToExecuteAfterError)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If Not IsDBNull(dt(0).Item("Objvalue")) Then
                instance.RuleIdToExecuteAfterError = Int32.Parse(dt(0).Item("Objvalue"))
            Else
                instance.RuleIdToExecuteAfterError = 0
            End If
        Else
            instance.RuleIdToExecuteAfterError = 0
        End If

        '[AlejandroR] 14/01/2010
        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.MessageToShowInCaseOfError)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If Not IsDBNull(dt(0).Item("ObjExtraData")) Then
                instance.MessageToShowInCaseOfError = dt(0).Item("ObjExtraData")
            Else
                instance.MessageToShowInCaseOfError = String.Empty
            End If
        Else
            instance.MessageToShowInCaseOfError = String.Empty
        End If

        '[AlejandroR] 14/01/2010
        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.ExecuteRuleInCaseOfError)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If dt(0).Item("Objvalue").ToString = "0" Then
                instance.ExecuteRuleInCaseOfError = False
            Else
                instance.ExecuteRuleInCaseOfError = True
            End If
        Else
            instance.ExecuteRuleInCaseOfError = False
        End If

        '[AlejandroR] 17/01/2010
        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.ThrowExceptionIfCancel)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If dt(0).Item("Objvalue").ToString = "0" Then
                instance.ThrowExceptionIfCancel = False
            Else
                instance.ThrowExceptionIfCancel = True
            End If
        Else
            instance.ThrowExceptionIfCancel = False
        End If

        '[AlejandroR] 01/03/2011
        dt = GetRuleOption(instance.ID, RuleSectionOptions.Configuracion, RulePreferences.DisableChildRules)
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            If dt(0).Item("Objvalue").ToString = "0" Then
                instance.DisableChildRules = False
            Else
                instance.DisableChildRules = True
            End If
        Else
            instance.DisableChildRules = False
        End If



    End Sub

#Region "UCZRuleValueFunction Members"
    Public Sub ReconocerRuleValueFunctions(ByRef codedText As String)

        Dim mirrorString As String = String.Empty


        Dim regex As String = "(<ZF>\w*[,'/\d:\s\.\w]*</ZF>)"
        Dim options As System.Text.RegularExpressions.RegexOptions = ((System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace Or System.Text.RegularExpressions.RegexOptions.Multiline) _
                    Or System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        Dim reg As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex(regex, options)

        Dim params As List(Of String)
        'se hace un split por funciones
        For Each Item As String In reg.Split(codedText)


            If Item.StartsWith("<ZF>") Then
                'si es una funcion la parsea
                Item = Item.Replace("<ZF>", String.Empty)
                'todo ver que pasaria con decimales o con algo que tenga ,
                Dim functionitems As String() = Item.Split(",")

                params = New List(Of String)

                For index As Integer = 1 To functionitems.Length - 1
                    params.Add(functionitems(index).Replace("</ZF>", String.Empty))
                Next

                Dim _function As New ZRuleValueFunctionsItem(functionitems(0), params)

                mirrorString += _function.getValue()
            Else
                'si no es funcion lo concatena al texto
                mirrorString += Item
            End If
        Next

        codedText = mirrorString
    End Sub

    Private Class ZRuleValueFunctionsItem
        Sub New(ByVal functionName As String, ByVal params As List(Of String))

            _functionstr = functionName
            _params = params

        End Sub

        Private _functionstr As String
        Private _params As List(Of String)

        Public Function getValue() As String
            Dim exception As Boolean = False
            Dim returnValue As String = String.Empty
            Select Case _functionstr.ToUpper
                Case "ZADDDAYS"
                    Dim baseDate As DateTime
                    If DateTime.TryParse(_params(0), baseDate) Then

                        If IsNumeric(_params(1)) Then
                            returnValue = baseDate.AddDays(_params(1)).ToString
                        Else
                            exception = True
                        End If

                    Else
                        exception = True
                    End If

                Case "ZADDMONTHS"
                    Dim baseDate As DateTime
                    If DateTime.TryParse(_params(0), baseDate) Then

                        If IsNumeric(_params(1)) Then
                            returnValue = baseDate.AddMonths(_params(1)).ToString
                        Else
                            exception = True
                        End If

                    Else
                        exception = True
                    End If
                Case "ZADDYEARS"
                    Dim baseDate As DateTime
                    If DateTime.TryParse(_params(0), baseDate) Then

                        If IsNumeric(_params(1)) Then
                            returnValue = baseDate.AddYears(_params(1)).ToString
                        Else
                            exception = True
                        End If

                    Else
                        exception = True
                    End If
                Case "ZSUM"
                    Dim acumulador As Int64 = 0

                    For Each Item As String In _params
                        If IsNumeric(Item) Then
                            acumulador += Convert.ToInt64(Item)
                            returnValue = acumulador
                        Else
                            exception = True
                            Exit For
                        End If
                    Next


                Case "ZAVERAGE"
                    Dim acumulador As Int64 = 0
                    For Each Item As String In _params
                        If IsNumeric(Item) Then
                            acumulador += Convert.ToInt64(Item)
                            returnValue = acumulador
                        Else
                            exception = True
                            Exit For
                        End If
                    Next

                    If exception = False Then
                        returnValue = (acumulador / _params.Count).ToString
                    End If

                Case "ZMAX"
                    Dim max As Int64 = -99999999
                    For Each Item As String In _params
                        If IsNumeric(Item) Then
                            If Convert.ToInt64(Item) > max Then
                                max = Convert.ToInt64(Item)
                            End If
                        Else
                            exception = True
                            Exit For
                        End If
                    Next

                    If exception = False Then
                        returnValue = max.ToString
                    End If

                Case "ZMIN"
                    Dim min As Int64 = 99999999
                    For Each Item As String In _params
                        If IsNumeric(Item) Then
                            If Convert.ToInt64(Item) < min Then
                                min = Convert.ToInt64(Item)
                            End If
                        Else
                            exception = True
                            Exit For
                        End If
                    Next

                    If exception = False Then
                        returnValue = min.ToString
                    End If
                Case Else
                    exception = True
            End Select

            If exception = False Then
                Return returnValue
            Else
                Throw New Exception("Regla Configurada incorrectamente")
            End If

        End Function

    End Class

    Private Function GetZRuleValueFunctions() As List(Of String)
        Dim list As New List(Of String)
        list.Add("ZADDDAYS")
        list.Add("ZADDMONTHS")
        list.Add("ZADDYEARS")
        list.Add("ZSUM")
        list.Add("ZAVERAGE")
        list.Add("ZMAX")
        list.Add("ZMIN")
        Return list
    End Function
    Private Function IsZRuleValueFunction(ByVal Word As String) As Boolean

        Select Case Word.ToUpper
            Case "ZADDDAYS"
                Return True
            Case "ZADDMONTHS"
                Return True
            Case "ZADDYEARS"
                Return True
            Case "ZSUM"
                Return True
            Case "ZAVERAGE"
                Return True
            Case "ZMAX"
                Return True
            Case "ZMIN"
                Return True
            Case Else
                Return False
        End Select

    End Function

#End Region

    ''' <summary>
    ''' Método que sirve para cambiar el nombre de una regla
    ''' </summary>
    ''' <param name="ruleNode">Instancia de una regla seleccionada</param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    16/04/2009  Modified    Se agrego un límite para la cantidad de caracteres máximo que puede tener el nombre de una regla
    '''     [Tomas]     11/06/2009  Modified    Se modifica la forma en que cambia el nombre de la acción de usuario. Se valida para que
    '''                                         si tiene existe un nombre en la Zruleopt no lo modifique.
    '''     [Sebastian] 19/10/2009 Modified se agrego la llamada a un formulario que simula un input  box
    '''     [Tomas]     06/11/2009  Modified    Se modifica la validacion ya que arrojaba exceptions cuando no debia. Se libera la memoria.
    ''' </history>
    Public Sub ChangeRuleName(ByVal ruleNode As IRuleNode)
        Dim frm As frmInputBox
        Dim BaseNode As BaseWFNode = ruleNode
        Dim WFRule As WFRuleParent
        Dim NewName As String
        Try
            'Dim BaseNode As BaseWFNode = ruleNode
            'Dim WFRule As WFRuleParent
            'Dim NewName As String
            WFRule = DirectCast(BaseNode, RuleNode).Rule
            frm = New frmInputBox("Ingrese el nombre de la regla", 2000, WFRule.Name, "Ingrese el nuevo nombre de la regla", False)
            'Dim NewName As String  = InputBox("Ingrese el nuevo nombre de la regla", "Edicion de Reglas", WFRule.Name)
            frm.StartPosition = FormStartPosition.CenterParent
            frm.BringToFront()
            frm.ShowDialog()
            If frm.DialogResult <> DialogResult.Cancel Then
                NewName = frm.txtUserText.Text.Replace(Chr(39), "")
            End If
            If ((Not String.IsNullOrEmpty(NewName)) AndAlso String.Compare(NewName, WFRule.Name) <> 0) Then
                'If (NewName.Length <= 2000) Then
                WFRule.Name = NewName
                WFRulesFactory.UpdateRuleName(WFRule)
                ruleNode.UpdateRuleNodeName(WFRule)

                If (WFRule.ParentType = TypesofRules.AccionUsuario) Then
                    'Obtiene el dataset donde se encuentra nombre de la acción de usuario asociada a esa regla
                    Dim dt As DataRow() = GetRuleOption(WFRule.ID, 0, 43)
                    'Valida si existen datos
                    If dt.Count = 0 Then
                        'Si no tiene nombre la acción de usuario lo modifica por defecto con el nombre de la primer regla
                        DirectCast(WFRule.RuleNode, RuleNode).PrevVisibleNode.Text = "Acción de Usuario - " & WFRule.Name
                    End If
                    'Else
                End If
                'End If

                'Else
                '    Dim ex As New Exception("El tamaño máximo para el nombre de la regla excede los 2000 caracteres")
                '    ZClass.raiseerror(ex)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            If Not IsNothing(frm) Then
                frm.Dispose()
                frm = Nothing
            End If
            If Not IsNothing(BaseNode) Then BaseNode = Nothing
            If Not IsNothing(WFRule) Then WFRule = Nothing
            If Not IsNothing(NewName) Then NewName = Nothing
        End Try
    End Sub

    Public Function GetChildRulesIds(ByVal ParentRuleId As Int64, RuleClass As String, results As List(Of ITaskResult)) As List(Of Int64) Implements IWFRuleBusiness.GetChildRulesIds
        Try

            If Cache.Rules.ChildRules.ContainsKey(ParentRuleId) = False Then
                Dim ids As New List(Of Int64)
                Using dt As DataTable = WFRulesFactory.GetChildRulesIds(ParentRuleId)
                    For i As Int32 = 0 To dt.Rows.Count - 1
                        ids.Add(CLng(dt.Rows(i)(0)))
                    Next
                End Using
                If RuleClass = "DOExecuteRule" Then
                    Dim dsParams As DataSet = GetRuleParamItems(ParentRuleId)
                    If (dsParams.Tables.Count > 0 AndAlso dsParams.Tables(0).Rows.Count > 0) Then
                        Dim RuleIDToExecute As Int64
                        Dim RuleExecuteMode As Boolean = dsParams.Tables("WFRuleParamItems").Rows.Count > 3 AndAlso Boolean.Parse(dsParams.Tables("WFRuleParamItems").Rows(3).Item("Value"))
                        If RuleExecuteMode Then
                            Dim RuleToExecute As String = dsParams.Tables("WFRuleParamItems").Rows(2).Item("Value")
                            Dim VarInterReglas As New VariablesInterReglas()
                            RuleToExecute = Zamba.Core.TextoInteligente.ReconocerCodigo(RuleToExecute, results(0)).Trim
                            If RuleToExecute.Contains("zvar") = True Then
                                RuleToExecute = VarInterReglas.ReconocerVariablesValuesSoloTexto(RuleToExecute)
                            End If
                            RuleIDToExecute = Int64.Parse(RuleToExecute)
                        Else
                            RuleIDToExecute = Int64.Parse(dsParams.Tables("WFRuleParamItems").Rows(0).Item("Value"))
                        End If
                        ids.Insert(0, RuleIDToExecute)
                    End If
                End If

                SyncLock Cache.Rules.ChildRules
                    If Cache.Rules.ChildRules.ContainsKey(ParentRuleId) = False Then
                        Cache.Rules.ChildRules.Add(ParentRuleId, ids)
                    End If
                End SyncLock

                Return ids
            Else
                Return Cache.Rules.ChildRules(ParentRuleId)
            End If
        Catch ex As Exception
            ZClass.raiseerror(New Exception($"ERROR al cargar las reglas hijas de la regla: {ParentRuleId}", ex))
        End Try
    End Function



    Public Shared Function ResolveText(text As String, result As IResult) As String

        Dim VarInterReglas As New VariablesInterReglas()
        Dim resultText As String = text
        Dim LastIndSmartText = 0
        Dim LastIndZvar = 0

        While resultText.ToLower.Contains("<<") OrElse resultText.ToLower.Contains("zvar(")
            LastIndSmartText = resultText.ToLower().LastIndexOf("<<")
            LastIndZvar = resultText.ToLower().LastIndexOf("zvar(")

            If LastIndSmartText > LastIndZvar Then
                ' Reconocer Codigo Inteligente
                resultText = TextoInteligente.ReconocerCodigo(resultText, result)
            Else
                ' Reconocer variable
                resultText = VarInterReglas.ReconocerVariablesValuesSoloTexto(resultText)
            End If
        End While

        Return resultText

    End Function

End Class