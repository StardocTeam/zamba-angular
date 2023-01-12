Imports Zamba.Core.WF.WF
Imports Zamba.Core.Enumerators
Imports Zamba.Data
Imports System.Reflection
Imports System.Text
Imports System.Drawing
Imports Telerik.WinControls.UI

Public Class WFRulesBusiness

#Region "Constantes"
    Private Const RULE_ID As String = "Id"
    Private Const RULE_NAME As String = "Name"
    Private Const RULE_STEP_ID As String = "step_Id"
    Private Const RULE_TYPE As String = "Type"
    Private Const RULE_PARENT_ID As String = "ParentId"
    Private Const RULE_PARENT_TYPE As String = "ParentType"
    Private Const RULE_ENABLED As String = "Enable"
    Private Const RULE_VERSION As String = "Version"
    Private Const GENERAL_RULES As String = "Reglas Generales"
#End Region

#Region "Eventos"
    Public Shared Event RefreshTimeOut()
#End Region

#Region "Get"

    Public Shared Function GetRuleInfo(ByVal id As Int64, Optional ByVal workflowId As Int64 = -1) As DataTable
        Return WFRulesFactory.GetRuleInfo(id, workflowId)
    End Function
    Public Shared Function GetRuleInfo(ByVal name As String, Optional ByVal workflowId As Int64 = -1) As DataTable
        Return WFRulesFactory.GetRuleInfo(name, workflowId)
    End Function
    Public Shared Function GetRuleInfoBySettings(ByVal settings As String, Optional ByVal workflowId As Int64 = -1) As DataTable
        Return WFRulesFactory.GetRuleInfoBySettings(settings, workflowId)
    End Function

    ''' <summary>
    ''' Obtiene el icono que representa a la regla
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <returns></returns>
    ''' <remarks>Metodo no tan bueno como el que acepta un WFRuleParent</remarks>
    Public Shared Function GetIcon(ruleEnable As Boolean, ruleClass As String, DisableChildRules As Boolean, RefreshTask As Boolean, IconId As Int32, RuleName As String) As Icons
        If ruleEnable = 0 OrElse DisableChildRules Then
            Return Icons.Disable
        ElseIf Convert.ToBoolean(RefreshTask) Then
            Return Icons.Refresh
        ElseIf IconId > 0 AndAlso IconId <> 31 Then
            Return IconId
        Else
            Return GetRuleIconBasedOnClass(ruleClass, RuleName)
        End If
    End Function

    Public Shared Function GetRuleIconBasedOnClass(RuleClass As String, rulename As String)
        Dim strtocompare As String = (RuleClass.ToLower & " " & rulename.ToLower).ToString()
        If strtocompare.Contains("excel") Then
            Return Icons.Excel2
        ElseIf strtocompare.Contains("word") Then
            Return Icons.Word
        ElseIf strtocompare.Contains("explorer") Then
            Return Icons.IE2
        ElseIf strtocompare.Contains("mail") Then
            Return Icons.Mail
        ElseIf strtocompare.Contains("outlook") Then
            Return Icons.Outlook2007
        ElseIf strtocompare.Contains("pdf") Then
            Return Icons.Pdf
        ElseIf strtocompare.Contains("asign") Then
            Return Icons.User
        ElseIf strtocompare.Contains("user") Then
            Return Icons.User
        ElseIf strtocompare.Contains("group") Then
            Return Icons.User
        ElseIf strtocompare.Contains("aprobar") Then
            Return Icons.OK
        ElseIf strtocompare.Contains("rechazar") Then
            Return Icons.Reject
        ElseIf strtocompare.Contains("valida") Then
            Return Icons.Validate
        ElseIf strtocompare.Contains("alta") Then
            Return Icons.[New]
        ElseIf strtocompare.Contains("eliminar") Then
            Return Icons.Delete
        ElseIf strtocompare.Contains("borrar") Then
            Return Icons.Delete
        ElseIf strtocompare.Contains("quitar") Then
            Return Icons.Delete
        ElseIf strtocompare.Contains("asociar") Then
            Return Icons.Add
        ElseIf strtocompare.Contains("nuevo") Then
            Return Icons.New
        ElseIf strtocompare.Contains("agregar") Then
            Return Icons.Add
        ElseIf strtocompare.Contains("asociar") Then
            Return Icons.Add
        ElseIf strtocompare.Contains("digitalizar") Then
            Return Icons.Scan
        ElseIf strtocompare.Contains("crear") OrElse strtocompare.Contains("generar") Then
            Return Icons.New
        ElseIf strtocompare.Contains("consulta") Then
            Return Icons.Chat
        ElseIf strtocompare.Contains("derivar") Then
            Return Icons.Foward
        ElseIf strtocompare.Contains("modificar") OrElse strtocompare.Contains("editar") Then
            Return Icons.Edit
        ElseIf strtocompare.Contains("solicitud") OrElse strtocompare.Contains("solicitar") Then
            Return Icons.Ask
        Else
            Return Icons.YellowBall
        End If
    End Function


    Public Shared Function GetBreakPointsByUserID(UserID As Long) As List(Of BreakPoint)
        Try
            Dim list As New List(Of BreakPoint)
            For Each row As DataRow In WFFactory.GetBreakPointsIdsByUserID(UserID).Rows
                list.Add(New BreakPoint With {.ID = row.Item("ID"), .RuleID = row.Item("ruleid"), .UserID = row.Item("userid"), .Conditions = row.Item("conditionals")})
            Next
            Return list
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function


    ''' <summary>
    ''' LImpia los HashTables
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub ClearHashTables()
        SyncLock (Cache.Workflows.HSRules.Lock)
            Cache.Workflows.HSRules.ClearAll()
        End SyncLock
    End Sub

    Public Shared Function GetRulesByTask(ByVal taskId As Int64, ByVal ruleType As TypesofRules) As List(Of IWFRuleParent)
        Dim Rules As New List(Of IWFRuleParent)
        Return Rules
    End Function

    ''' <summary>
    ''' Obtiene reglas con posibilidades de ser removidas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRulesToRemove() As DataTable
        Return WFRulesFactory.GetRulesToRemove()
    End Function

    Public Shared Function GetNeverSavedRule() As DataTable
        Return WFRulesFactory.GetNeverSavedRule()
    End Function

    Public Overloads Shared Function GetDoExecuteRules() As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = WFRulesFactory.GetDoExecuteRules()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return ds
    End Function

    '[Ezequiel] - 4/5/11 - Metodo el cual devuelve un diccionario con todas las reglas de una etapa
    Public Shared Function GetRulesByStepId(ByVal stepid As Int64, ByVal useCache As Boolean) As List(Of IWFRuleParent)
        Dim rulelist As New List(Of IWFRuleParent)
        Dim i_dsRule As DsRules = GetRulesDSByStepID(stepid, useCache)

        If Not i_dsRule Is Nothing Then
            Dim rulesRows() As DsRules.WFRulesRow = i_dsRule.WFRules.Select("ParentId=0")
            For Each row As DsRules.WFRulesRow In rulesRows
                If useCache = True Then
                    If Cache.Workflows.HSRules.Contains(row.Id) Then
                        rulelist.Add(Cache.Workflows.HSRules.GetByRuleID(row.Id))
                    Else
                        Dim ruleInstancesList As New List(Of Int64)
                        Dim rule As WFRuleParent = GetInstanceRuleById(row.Id, stepid, True)

                        ruleInstancesList.Clear()
                        ruleInstancesList = Nothing

                        If Not rule Is Nothing Then
                            Cache.Workflows.HSRules.Add(rule)
                            rulelist.Add(rule)
                        End If
                    End If
                Else
                    Dim ruleInstancesList As New List(Of Int64)
                    Dim rule As WFRuleParent = WFRulesBusiness.GetInstanceRuleById(row.Id, stepid, True)

                    ruleInstancesList.Clear()
                    ruleInstancesList = Nothing

                    If Not rule Is Nothing Then
                        rulelist.Add(rule)
                    End If
                End If
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
    Public Overloads Shared Function GetRulesByWorkFlowIDAsDataSet(ByVal _workFlowID As Int64) As DataSet
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
    ''' Ejecuta una regla a partir de su id, el id del step y la collecion de resultsa ejecutarse
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="stepId"></param>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExecuteRule(ByVal ruleId As Int64, ByVal stepId As Int64, ByVal _TaskResults As List(Of ITaskResult), ByVal isPrimaryRule As Boolean, ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Dim CurrentRule As IWFRuleParent = Nothing
        Try
            Dim ruleInstancesList As New List(Of Int64)
            CurrentRule = WFRulesBusiness.GetInstanceRuleById(ruleId, stepId, True, ruleInstancesList)

            If Not IsNothing(ruleInstancesList) Then ruleInstancesList.Clear()
            ruleInstancesList = Nothing

            If IsNothing(CurrentRule) Then
                Throw New Exception("Error al instanciar la regla " & ruleId)
            End If

            If isPrimaryRule Then
                refreshTasks = New List(Of Int64)
                Return ExecutePrimaryRule(CurrentRule, _TaskResults, refreshTasks)
            Else
                Return ExecuteSecondaryRule(CurrentRule, _TaskResults, refreshTasks)
            End If
        Catch ex As System.ComponentModel.Win32Exception
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
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
    Public Function ExecutePrimaryRule(ByRef rule As WFRuleParent, ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Try

            Dim newresults As New List(Of ITaskResult)
            If IsNothing(refreshTasks) Then
                refreshTasks = New List(Of Int64)
            End If

            'Disparamos el evento que refresca el timeout
            RaiseEvent RefreshTimeOut()

            ' Se recupera de la base de datos el id del estado de la regla
            Dim dt As DataTable = WFRulesBusiness.GetRuleOption(rule.WFStepId, rule.ID, RuleSectionOptions.Estado, RulePreferences.StateTypeState, 0, True)
            If (Not IsNothing(dt) AndAlso dt.Rows.Count > 0) Then
                Dim stateid As Int64 = Int64.Parse(dt.Rows(0).Item("ObjValue").ToString())
                ' Se recorren los results para asignarle a cada uno el nuevo estado, y de esta forma, colocarlo en el combobox de estados en el panel
                ' Tareas del cliente
                For Each result As ITaskResult In results
                    Dim state As IWFStepState = WFStepStatesComponent.getStateFromList(stateid, WFStepStatesComponent.GetStepStatesByStepId(result.StepId))
                    WFTaskBusiness.ChangeState(result, state)
                Next
            End If

            '''Esto es para texto inteligente
            AllObjects.Tareas = results


            'Verifica si debe ejecutar todas las tareas juntas o de a una.
            If results.Count = 1 OrElse IsExecutionTaskByTask(rule.WFStepId, rule.ID, True) Then

                Dim tempNewResults As List(Of ITaskResult)
                Dim tempTaskResults As List(Of ITaskResult)

                'Se ejecuta la cadena de reglas completa por tarea.
                For Each task As ITaskResult In results

                    AllObjects.Tarea = task

                    tempTaskResults = New List(Of ITaskResult)
                    tempTaskResults.Add(task)
                    tempNewResults = rule.ExecuteRule(tempTaskResults, New WFTaskBusiness(), refreshTasks)

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

                newresults = rule.ExecuteRule(results, New WFTaskBusiness(), refreshTasks)
            End If

            'PREFERENCIA DE REGLA PARA NOTIFICAR LUEGO DE LA EJECUCION
            If rule.AlertExecution = True Then SendNotificationAlert(rule, results)

            For j As Int16 = 0 To refreshTasks.Count - 1
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando Tarea: " & refreshTasks(j))
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo la tarea de la BD")

                Dim WFTaskBusiness As New WFTaskBusiness()

                Dim task As ITaskResult = WFTaskBusiness.GetTask(refreshTasks(j))

                If Not IsNothing(task) Then
                    ZClass.HandleModule(ResultActions.RefreshIndexs, task)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Tarea inexistente")
                End If
            Next

            Return newresults
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Throw ex
        Finally
            If Not IsNothing(refreshTasks) Then
                For j As Int16 = 0 To refreshTasks.Count - 1
                    'refreshTasks(j).Dispose()
                    refreshTasks(j) = Nothing
                Next

                refreshTasks.Clear()
                refreshTasks = Nothing
            End If
            AllObjects.Tareas = Nothing
            AllObjects.Tarea = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Se ejecuta la regla. Lo modificado es que se agrego el método ChangeStateTask para que se pase al nuevo estado cuando se ejecuta la regla
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <history> 
    ''' [Tomas] 25/8/2013
    '''</history>
    ''' <remarks></remarks>
    Public Function TestRule(ByRef rule As IWFRuleParent, ByRef dictionary As DataTable) As Boolean
        Try
            Dim zvarList As New List(Of String)
            For Each col As DataColumn In dictionary.Columns
                zvarList.Add(col.ColumnName)
            Next

            Dim tr As New List(Of ITaskResult)
            Dim key As String
            Dim value As String

            For Each test As DataRow In dictionary.Rows
                For Each zvarCode As String In zvarList
                    key = zvarCode.Replace("zvar(", String.Empty).Replace(")", String.Empty)
                    value = test(zvarCode)

                    If VariablesInterReglas.ContainsKey(key) Then
                        VariablesInterReglas.Item(key) = value
                    Else
                        VariablesInterReglas.Add(key, value, False)
                    End If
                Next
                Dim T As ITaskResult
                T = New TaskResult
                tr.Add(T)
                rule.TestRule(tr)
                tr.Clear()
            Next

            tr = Nothing
            zvarList.Clear()
            zvarList = Nothing

            Return True

        Catch ex As Exception
            Return False
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
    Public Function ExecuteSecondaryRule(ByRef rule As WFRuleParent, ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Try
            Dim newresults As New List(Of ITaskResult)

            'Disparamos el evento que refresca el timeout
            RaiseEvent RefreshTimeOut()

            ' Se recupera de la base de datos el id del estado de la regla
            Dim dt As DataTable = WFRulesBusiness.GetRuleOption(rule.WFStepId, rule.ID, RuleSectionOptions.Estado, RulePreferences.StateTypeState, 0, True)
            If (Not IsNothing(dt) AndAlso dt.Rows.Count > 0) Then
                Dim stateid As Int64 = Int64.Parse(dt.Rows(0).Item("ObjValue").ToString())
                ' Se recorren los results para asignarle a cada uno el nuevo estado, y de esta forma, colocarlo en el combobox de estados en el panel
                ' Tareas del cliente
                For Each result As ITaskResult In results
                    Dim state As IWFStepState = WFStepStatesComponent.getStateFromList(stateid, WFStepStatesComponent.GetStepStatesByStepId(result.StepId))

                    WFTaskBusiness.ChangeState(result, state)
                Next
            End If

            '''Esto es para texto inteligente
            AllObjects.Tareas = results


            'Verifica si debe ejecutar todas las tareas juntas o de a una.
            If results.Count = 1 OrElse IsExecutionTaskByTask(rule.WFStepId, rule.ID, True) Then

                Dim tempNewResults As List(Of ITaskResult)
                Dim tempTaskResults As List(Of ITaskResult)

                'Se ejecuta la cadena de reglas completa por tarea.
                For Each task As ITaskResult In results

                    AllObjects.Tarea = task

                    tempTaskResults = New List(Of ITaskResult)
                    tempTaskResults.Add(task)
                    tempNewResults = rule.ExecuteRule(tempTaskResults, New WFTaskBusiness(), refreshTasks)

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

                newresults = rule.ExecuteRule(results, New WFTaskBusiness(), refreshTasks)
            End If

            'PREFERENCIA DE REGLA PARA NOTIFICAR LUEGO DE LA EJECUCION
            If rule.AlertExecution = True Then SendNotificationAlert(rule, results)

            Return newresults
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
        End Try
    End Function


    ''' <summary>
    ''' Verifica el modo de ejecución de la regla padre, con respecto a las tareas que recibe.
    ''' </summary>
    ''' <param name="ruleId">RuleId de la regla padre (Int64)</param>
    ''' <returns>True si debe ejecutar la cadena de reglas por cada regla.</returns>
    ''' <remarks></remarks>
    Public Shared Function IsExecutionTaskByTask(ByVal stepid As Int64, ByVal ruleId As Int64, ByVal useCache As Boolean) As Boolean
        'Obtiene la configuración de la regla.
        Dim dt As DataTable = GetRuleOption(stepid, ruleId, RuleSectionOptions.Configuracion, RulePreferences.TaskExecutionMode, 0, useCache)

        'Verifica que existan datos.
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim value As String = dt.Rows(0).Item("ObjValue").ToString

            dt.Dispose()
            dt = Nothing

            If String.Compare(value, "0") = 0 Then
                Return True
            Else
                Return False
            End If
        Else
            'Inserta como opción por defecto la ejecución de a una tarea.
            WFBusiness.SetRulesPreferences(ruleId, RuleSectionOptions.Configuracion, RulePreferences.TaskExecutionMode, 0)
            Return True
        End If
    End Function

    Public Sub ExecuteZopenRules(ByVal TasksDT As DataTable, ByVal StepId As Int64)
        Dim Wfstep As IWFStep = WFStepBusiness.GetStepById(StepId)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtengo regla del Evento Abrir Zamba")
        Dim zopenrule As WFRuleParent
        Dim DVDSRules As New DataView(Wfstep.DSRules.WFRules)
        DVDSRules.RowFilter = "Type = 40"
        For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
            Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), Wfstep.ID, True)
            zopenrule = Rule
            Exit For
        Next
        DVDSRules.Dispose()
        DVDSRules = Nothing

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecuto tareas")
        Dim WFTB As New WFTaskBusiness
        For Each CurrentRow As DataRow In TasksDT.Rows
            Try
                Dim task As TaskResult = WFTB.NewResult(CurrentRow, Wfstep)
                Dim tasks As New System.Collections.Generic.List(Of ITaskResult)
                tasks.Add(task)
                tasks = ExecutePrimaryRule(zopenrule, tasks, Nothing)
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
            Catch ex As Exception
                ZClass.raiseerror(ex)
                MessageBox.Show("Ocurrio un error en la ejecución de reglas de la tarea." & vbCrLf & "Contactese con el administrador del sistema.", "Error en la ejecución", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Next
        WFTB = Nothing
    End Sub


    'Martin: Se comenta este codigo que presuntamente se hizo para doforeach para probar de llamar al metodo que utiliza el executerule de la wfruleparent, que es mas completo
    'Public Shared Function ExecuteRuleAsResult(ByVal ruleId As Int64, ByVal stepId As Int64, ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
    '    Return ExecuteRuleNewAsResult(ruleId, stepId, results, Application.StartupPath)
    'End Function
    ''' <summary>
    ''' Devuelve un listado de ids y nombres de reglas perteneciente a 1 etapa
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRulesIdsAndNames(ByVal stepId As Int64) As Dictionary(Of Int64, String)
        Dim dt As DataTable = WFRulesFactory.GetRulesIdsAndNames(stepId)
        Dim rules As New Dictionary(Of Int64, String)(dt.Rows.Count)
        For Each dr As DataRow In dt.Rows
            rules.Add(Int64.Parse(dr("Id").ToString()), dr("name").ToString())
        Next
        Return rules
    End Function

    ''' <summary>
    ''' Obtiene una regla de workflow instanciada.
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="useCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetInstanceRuleById(ByVal p_iRuleId As Int64, ByVal useCache As Boolean) As IWFRuleParent
        Dim stepid As Int64 = WFStepBusiness.GetStepIdByRuleId(p_iRuleId, useCache)
        Dim ruleInstancesList As New List(Of Int64)
        Return GetInstanceRuleById(p_iRuleId, stepid, useCache, ruleInstancesList)
        ruleInstancesList.Clear()
        ruleInstancesList = Nothing
    End Function
    Public Shared Function GetInstanceRuleById(ByVal p_iRuleId As Int64, ByVal p_WfStepID As Int64, ByVal useCache As Boolean) As IWFRuleParent
        Dim ruleInstancesList As New List(Of Int64)
        Return GetInstanceRuleById(p_iRuleId, p_WfStepID, useCache, ruleInstancesList)
        ruleInstancesList.Clear()
        ruleInstancesList = Nothing
    End Function

    ''' <summary>
    ''' Obtiene una regla de workflow instanciada.  
    ''' </summary>
    ''' <param name="p_iRuleId">id de la regla.</param>
    ''' <param name="p_WfStep">Etapa del workflow.</param>
    ''' <returns>WFRuleParent</returns>
    ''' <remarks></remarks>
    Private Shared Function GetInstanceRuleById(ByVal p_iRuleId As Int64, ByVal p_WfStepID As Int64, ByVal useCache As Boolean, ByVal ruleInstanceList As List(Of Int64)) As IWFRuleParent
        'La designacion i_ se utiliza para especificar que la variable esta declarada en el
        'metodo, dado que este nombre es identico al nombre de la clase de este tipo de variable.
        Dim i_dsRule As DsRules = Nothing
        Dim rule As IWFRuleParent = Nothing
        Dim DsRules As DsRules = Nothing

        Try
            'Listado de reglas para evitar loops de reglas circulares
            If useCache Then
                SyncLock Cache.Workflows.HSRules.Lock
                    If Cache.Workflows.HSRules.Contains(p_iRuleId) Then
                        Return Cache.Workflows.HSRules.GetByRuleID(p_iRuleId)
                    Else
                        If Cache.Workflows.hsSteps.ContainsKey(p_WfStepID) Then
                            DsRules = DirectCast(Cache.Workflows.hsSteps(p_WfStepID), IWFStep).DSRules

                            Dim ruleRow As DsRules.WFRulesRow = DsRules.WFRules.Rows.Find(p_iRuleId)
                            If ruleRow IsNot Nothing Then
                                Dim parentrule As IWFRuleParent
                                If ruleRow.ParentId > 0 Then
                                    parentrule = GetInstanceRuleById(ruleRow.ParentId, True)
                                End If
                                rule = ReturnInstanceRule(DsRules.WFRules, DsRules.WFRuleParamItems, ruleRow, p_WfStepID, ruleRow.ParentType, ruleInstanceList, parentrule)
                                If Not Cache.Workflows.HSRules.Contains(p_iRuleId) Then Cache.Workflows.HSRules.Add(rule)
                            End If

                            Return rule
                        End If
                    End If
                End SyncLock
            End If

            '//Solo si es administrador o no existe aun en cache
            i_dsRule = GetRulesDSByStepID(p_WfStepID, useCache)

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Búsqueda de la regla " & p_iRuleId)
            Dim r As DsRules.WFRulesRow = i_dsRule.WFRules.Rows.Find(p_iRuleId)
            If r IsNot Nothing Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se encontro la regla con id " & p_iRuleId & " se pide la instancia de la misma")
                Dim parentrule As IWFRuleParent
                If r.ParentId > 0 Then
                    parentrule = GetInstanceRuleById(r.ParentId, True)
                End If
                rule = ReturnInstanceRule(i_dsRule.WFRules, i_dsRule.WFRuleParamItems, r, p_WfStepID, r.ParentType, ruleInstanceList, parentrule)
                If Not Cache.Workflows.HSRules.Contains(p_iRuleId) Then Cache.Workflows.HSRules.Add(rule)
            End If

            Return rule

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al instanciar la regla  " & ex.ToString)
            ZClass.raiseerror(ex)

        Finally
            'Analizar si no afecta por referencia el dataset del cache de donde sale
            If i_dsRule IsNot Nothing Then
                i_dsRule.Dispose()
                i_dsRule = Nothing
            End If
            If DsRules IsNot Nothing Then
                DsRules.Dispose()
                DsRules = Nothing
            End If
        End Try

        If useCache AndAlso Not Cache.Workflows.HSRules.Contains(p_iRuleId) Then
            Cache.Workflows.HSRules.Add(rule)
        End If

        Return rule
    End Function

    Public Shared Function GetInstanceRules(ByVal stepID As Int64, ByVal rulesType As Core.Enumerators.TypesofRules, ByVal isEvent As Boolean) As IWFRuleParent()
        Dim key As String = stepID.ToString & "-" & rulesType.ToString()

        If Not Cache.Workflows.HSRulesByStepAndType.ContainsKey(key) Then
            Cache.Workflows.HSRulesByStepAndType.Add(key, WFRulesFactory.GetRulesIdByStep(stepID, rulesType, isEvent))
        End If

        Dim ruleIds As DataTable = Cache.Workflows.HSRulesByStepAndType.Item(key)
        key = Nothing

        If ruleIds IsNot Nothing AndAlso ruleIds.Rows.Count > 0 Then
            Dim rules(ruleIds.Rows.Count - 1) As IWFRuleParent

            For i As Int32 = 0 To ruleIds.Rows.Count - 1
                Dim ruleInstancesList As New List(Of Int64)
                rules(i) = GetInstanceRuleById(ruleIds.Rows(i)(0), stepID, True)

                ruleInstancesList.Clear()
                ruleInstancesList = Nothing
            Next

            ruleIds = Nothing
            Return rules
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene un dataset tipado de las reglas por el id de la etapa
    ''' </summary>
    ''' <param name="p_WFstepID">ID de etapa</param>
    ''' <param name="useCache"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRulesDSByStepID(ByVal p_WFstepID As Int64, ByVal useCache As Boolean) As DsRules
        If p_WFstepID > 0 Then
            SyncLock Cache.Workflows.hsStepsRulesDS
                If useCache Then
                    If Cache.Workflows.hsStepsRulesDS.Contains(p_WFstepID) = False Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargo la definicion de las reglas de la etapa: " & p_WFstepID)
                        Cache.Workflows.hsStepsRulesDS.Add(p_WFstepID, WFRulesFactory.GetRulesDSByStepID(p_WFstepID))
                    End If
                    Return Cache.Workflows.hsStepsRulesDS(p_WFstepID)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargo la definicion de las reglas de la etapa: " & p_WFstepID)
                    Return WFRulesFactory.GetRulesDSByStepID(p_WFstepID)
                End If
            End SyncLock
        Else
            Return Nothing
        End If
    End Function



    Public Shared Function GetIsRuleEnabled(ByVal UserRules As Hashtable, ByVal Rule As IWFRuleParent) As Boolean

        If Rule.Enable Then

            If UserRules.ContainsKey(Rule.ID) Then
                'Lista que en la posicion 0 guarda si esta habilitada la regla o no
                'y en la 1 si se acumula a la habilitacion de las solapas o no
                Dim lstRulesEnabled As List(Of Boolean) = DirectCast(UserRules(Rule.ID), List(Of Boolean))
                Return lstRulesEnabled(0)
            Else
                'Obtiene el estado
                Return WFRulesBusiness.GetInstanceRuleById(Rule.ID, True).Enable
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
    Public Shared Function GetStateOfHabilitationOfState(ByVal Rule As IWFRuleParent, ByVal StateID As Int64) As Boolean
        If WFFactory.GetStateOfHabilitationOfState(Rule.ID, StateID) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    '''  Obtiene el nombre de una regla.
    ''' </summary>
    ''' <param name="p_iRuleId">Id de regla</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function GetRuleName(ByVal p_iRuleId As Int64) As String
        Dim rule As IWFRuleParent = GetInstanceRuleById(p_iRuleId, True)
        If Not rule Is Nothing Then
            Return rule.Name
        Else
            Return WFRulesFactory.GetRuleNameById(p_iRuleId)
        End If
    End Function


    Public Shared Function GetWFStepIdbyRuleID(ByVal ruleId As Int64) As Int64
        SyncLock (Cache.Workflows.hsWFStepId)
            If Cache.Workflows.hsWFStepId.Contains(ruleId) = False Then
                Cache.Workflows.hsWFStepId.Add(ruleId, WFRulesFactory.GetWFStepIdbyRuleID(ruleId))
            End If
            Return Cache.Workflows.hsWFStepId(ruleId)
        End SyncLock
    End Function

    'Metodo para Administrador
    Public Shared Function GetRuleNameById(ByVal p_iRuleId As Int64) As String
        Return WFRulesFactory.GetRuleNameById(p_iRuleId)
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

    Private Shared DsStepNode As DataSet = New DataSet
    Public Enum ValueTypes
        Normal
        Extra
    End Enum
    Public Shared Function GetRuleOption(ByVal ruleid As Int64, ByVal RuleSection As RuleSectionOptions, ByVal _RulePreference As RulePreferences, ByVal DestType As Int32, ByVal useCache As Boolean, stepid As Int64, ValueType As ValueTypes) As String
        'DestType = 1 ( Obtiene los usuarios a notificar)
        'DestType = 2 ( Obtiene los usuarios de un Grupo a notificar)
        'DestType = 3 ( Obtiene los Mails Externos que se puedan llegar a cargar)

        Dim dt As DataTable
        Dim dv As DataView = Nothing

        Try
            dt = GetRuleOptionsDT(useCache, stepid)

            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                dv = New DataView(dt)
                dv.RowFilter = "ruleid = " & ruleid & " and SectionId= " & RuleSection & " And ObjectId =" & _RulePreference & " And ObjValue = " & DestType
                If dv.ToTable.Rows.Count > 0 Then
                    If ValueType = ValueTypes.Normal Then
                        Return dv.ToTable().Rows(0).Item("ObjValue")
                    Else
                        Return dv.ToTable().Rows(0).Item("ObjExtraData")
                    End If
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Finally
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
        End Try
    End Function
    Public Shared Function GetRuleOption(ByVal ruleid As Int64, ByVal RuleSection As RuleSectionOptions, ByVal _RulePreference As RulePreferences, ByVal DestType As Int32, ValueType As ValueTypes, dt As DataTable) As String
        Dim dv As DataView = Nothing

        Try
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                dv = New DataView(dt)
                dv.RowFilter = "ruleid = " & ruleid & " and SectionId= " & RuleSection & " And ObjectId =" & _RulePreference & " And ObjValue = " & DestType
                If dv.ToTable.Rows.Count > 0 Then
                    If ValueType = ValueTypes.Normal Then
                        Return dv.ToTable().Rows(0).Item("ObjValue")
                    Else
                        Return dv.ToTable().Rows(0).Item("ObjExtraData")
                    End If
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Finally
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
        End Try
    End Function
    Public Shared Function GetRuleOption(stepid As Int64, ByVal ruleid As Int64, ByVal RuleSection As RuleSectionOptions, ByVal _RulePreference As RulePreferences, ByVal DestType As Int32, ByVal useCache As Boolean) As DataTable
        Dim dt As DataTable
        Dim dv As DataView = Nothing

        Try
            dt = GetRuleOptionsDT(useCache, stepid)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dv = New DataView(dt)
                If DestType > 0 Then
                    dv.RowFilter = "ruleid = " & ruleid & " and SectionId= " & RuleSection & " And ObjectId =" & _RulePreference & " And ObjValue = " & DestType
                Else
                    dv.RowFilter = "ruleid = " & ruleid & " and SectionId= " & RuleSection & " And ObjectId =" & _RulePreference
                End If

                dt = dv.ToTable
                If dt.Rows.Count > 0 Then
                    Return dt
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Finally
            If dv IsNot Nothing Then
                dv.Dispose()
                dv = Nothing
            End If
        End Try
    End Function

    Public Shared Function GetRuleOptionsDT(ByVal useCache As Boolean, stepid As Int64) As DataTable

        Dim dt As DataTable

        SyncLock (Cache.Workflows.HsRulesPreferencesByStepId)
            If useCache Then
                If Not Cache.Workflows.HsRulesPreferencesByStepId.ContainsKey(stepid) Then
                    Cache.Workflows.HsRulesPreferencesByStepId.Add(stepid, WFFactory.GetRulesPreferencesByStepId(stepid))
                End If
                Return Cache.Workflows.HsRulesPreferencesByStepId(stepid)
            Else
                Return WFFactory.GetRulesPreferencesByStepId(stepid)
            End If
        End SyncLock

        Return dt
    End Function



    ''' <summary>
    ''' inserta los Usuario que se van a notificar sobre la ejecucion de la regla
    ''' </summary>
    ''' <param name="ruleid"></param>
    ''' <param name="RuleSection"></param>
    ''' <param name="_RulePreference"></param>
    ''' <param name="DestType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Sub InsertUsersToNotifyAboutRuleExecution(ByVal ruleid As Int64, ByVal RuleSection As RuleSectionOptions, ByVal _RulePreference As RulePreferences, ByVal DestType As Int32, ByVal items As List(Of String))
        WFRulesFactory.InsertUsersToNotifyAboutRuleExecution(ruleid, RuleSection, _RulePreference, DestType, items)
    End Sub

#End Region

#Region "Fill"

    'Public Shared Function 
    'Friend Shared Sub FillRules(ByVal wfs() As WorkFlow, ByVal withcache As Boolean)
    '    Try
    '        For Each wf As WorkFlow In wfs
    '            FillRules(wf, withcache)
    '        Next
    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try
    'End Sub

    'Public Shared Sub FillRules(ByVal wf As IWorkFlow, ByVal useCache As Boolean)
    '    Try
    '        For Each s As WFStep In wf.Steps.Values
    '            FillRules(s, useCache)
    '        Next
    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try
    'End Sub

    'Public Shared Sub FillRules(ByVal wfStep As IWFStep, ByVal useCache As Boolean)
    '    Try
    '        If useCache = True AndAlso Cache.Workflows.hsSteps.Contains(wfStep.ID) Then
    '            If wfStep.Rules.Count = 0 Then
    '                wfStep.Rules.InsertRange(0, DirectCast(Cache.Workflows.hsSteps(wfStep.ID), WFStep).Rules)
    '                Exit Sub
    '            End If
    '        End If
    '        Dim listRule As List(Of IWFRuleParent)
    '        listRule = GetRulesByStepId(wfStep.ID, useCache)

    '        If Not listRule Is Nothing Then
    '            SetRulesInstances(listRule, wfStep)
    '        End If
    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try
    'End Sub

    'Private Shared Function ContainsRule(ByRef wfstep As WFStep, ByRef wfrule As WFRuleParent) As IWFRuleParent
    '    For Each R As WFRuleParent In wfstep.Rules
    '        If R.ID = wfrule.ID Then
    '            Return R
    '        End If
    '    Next
    '    Return Nothing
    'End Function

#End Region

#Region "Add"
    Public Shared Function Add(ByVal ruleName As String, ByVal baseNode As BaseWFNode, ByVal RuleNameFromUser As String, ByVal typeofRule As TypesofRules) As Int64
        If GetRuleParentType(baseNode) = TypesofRules.Salida AndAlso ruleName.IndexOf("distribuir", StringComparison.CurrentCultureIgnoreCase) <> -1 Then
            Throw New Exception("No se puede distribuir una tarea en la salida de una etapa, Error de diseño del circuito")
        ElseIf TypeOf baseNode Is RuleNode AndAlso DirectCast(baseNode, RuleNode).RuleClass.ToUpper.StartsWith("IF") AndAlso DirectCast(baseNode, RuleNode).RuleClass.ToLower() <> "ifbranch" Then
            Throw New Exception("No se puede agregar reglas sobre una regla de condicion, debe hacerlo sobre una de sus ramas")
        Else
            'AddRule(baseNode, GetNewRule(baseNode, RuleNameFromUser, ruleName, typeofRule))

            Dim rule As IRule = GetNewRule(baseNode, RuleNameFromUser, ruleName, typeofRule)

            rule.ExecuteWhenResult = True
            AddRule(baseNode, rule)

            '[German]:Si la regla es padre , la categoria sera 1
            If (IsNothing(rule.ParentRule) Or rule.RuleClass.ToLower.Contains("dodesign") Or rule.RuleClass.ToLower.Contains("if") Or rule.RuleClass.ToLower.Contains("doshowform") Or rule.RuleClass.ToLower.Contains("doexecute") Or rule.RuleClass.ToLower.Contains("doconsumewebservice")) Then
                rule.Category = 1
            Else
                rule.Category = 2
            End If

            '(pablo) guardo el log de la creacion de regla
            UserBusiness.Rights.SaveAction(rule.ID, ObjectTypes.WFRules, RightsType.Create, "Creación de regla: '" & rule.Name & "' en Etapa '" & rule.WFStepId & "'")
            '[German]: Insertamos los nuevos valores en la tabla ZRuleOpt.
            WFFactory.SetRulesPreferences(rule.ID, RuleSectionOptions.Configuracion, RulePreferences.RuleCategory, rule.Category)
            Return rule.ID
        End If
    End Function
    Public Shared Function AddChild(ByVal ruleName As String, ByVal parentRule As IRule, ByVal RuleNameFromUser As String, ByVal typeofRule As TypesofRules) As IRule
        Dim rule As IRule = GetNewChildRule(parentRule, RuleNameFromUser, ruleName, typeofRule)

        rule.ExecuteWhenResult = True
        AddChildRule(parentRule, rule)

        '[German]:Si la regla es padre , la categoria sera 1
        If (IsNothing(rule.ParentRule) Or rule.RuleClass.ToLower.Contains("dodesign") Or rule.RuleClass.ToLower.Contains("if") Or rule.RuleClass.ToLower.Contains("doshowform") Or rule.RuleClass.ToLower.Contains("doexecute") Or rule.RuleClass.ToLower.Contains("doconsumewebservice")) Then
            rule.Category = 1
        Else
            rule.Category = 2
        End If

        '(pablo) guardo el log de la creacion de regla
        UserBusiness.Rights.SaveAction(rule.ID, ObjectTypes.WFRules, RightsType.Create, "Creación de regla: '" & rule.Name & "' en Etapa '" & rule.WFStepId & "'")
        '[German]: Insertamos los nuevos valores en la tabla ZRuleOpt.
        WFFactory.SetRulesPreferences(rule.ID, RuleSectionOptions.Configuracion, RulePreferences.RuleCategory, rule.Category)
        Return rule

    End Function
    ''' <summary>
    ''' Modifica el nombre de la clase
    ''' </summary>
    ''' <param name="RuleId">Id de la regla</param>
    ''' <param name="ClassName">Nombre de la clase</param>
    ''' <history>   Marcelo 02/09/2009  Created</history>
    ''' <remarks></remarks>
    Public Shared Sub updateClass(ByVal RuleId As Int64, ByVal ClassName As String)
        WFRulesFactory.updateClass(RuleId, ClassName)
    End Sub


#Region "Reflection"


    Private Shared Sub InstanceRule(ByRef rule As WFRuleParent, ByVal paramItems As DataRow(), ByVal ruleClass As String, ByVal ruleId As Int64, ByVal ruleName As String, ByVal ruleType As TypesofRules, ByVal stepId As Int64)
        Dim t As System.Type
        Dim c As ConstructorInfo
        Dim i As Byte
        Dim y As Byte

        Try
            t = DBBusiness.RulesAssembly.GetType("Zamba.WFActivity.Regular." & ruleClass, True, True)
            c = t.GetConstructors.GetValue(0)

            If Not c Is Nothing Then
                Dim Args(c.GetParameters.Length - 1) As Object
                Args(0) = ruleId
                Args(1) = ruleName
                Args(2) = stepId

                For Each p As ParameterInfo In c.GetParameters
                    If i > 2 Then
                        If paramItems.Length >= y + 1 Then
                            FillArgsValues(p, Args(i), paramItems(y).Item("Value"))
                        Else
                            FillArgsValues(p, Args(i), GetArgInitialValues(p))
                        End If
                        y += 1
                    End If
                    i += 1
                Next
                If Not t Is Nothing Then
                    'ZTrace.WriteLineIf(ZTrace.IsInfo,"Instanciando" & ruleClass)
                    rule = Activator.CreateInstance(t, Args)
                    rule.RuleType = ruleType

                    For Each o As Object In t.GetCustomAttributes(True)
                        If String.Compare(o.GetType().Name.ToLower, "RuleFeatures".ToLower) = 0 Then
                            rule.IsUI = DirectCast(o, RuleFeatures).IsUI
                            Exit For
                        End If
                    Next
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Tipo no reconocido: " & ruleClass)
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
        End Try
    End Sub

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
    ''' </history>
    Private Shared Sub FillArgsValues(ByVal p As ParameterInfo, ByRef o As Object, ByVal value As Object)

        If value Is Nothing OrElse IsDBNull(value) Then
            value = String.Empty
        End If

        Select Case Type.GetTypeCode(p.ParameterType)

            Case TypeCode.Object
                o = Nothing

            Case TypeCode.Boolean

                If value.GetType() Is GetType(String) Then
                    If (String.IsNullOrEmpty(value)) Then
                        value = "0"
                    ElseIf (String.IsNullOrEmpty(value.Trim())) Then
                        value = "0"
                    ElseIf ((value <> "0") And (value <> "1") And (value.ToUpper() <> "TRUE") And
                           (value.ToUpper() <> "FALSE")) Then
                        value = "0"
                    ElseIf value.ToString().ToLower().Contains("true") Then
                        value = "1"
                    ElseIf value.ToString().ToLower().Contains("false") Then
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
                    If ((Servers.Server.isOracle) AndAlso (String.IsNullOrEmpty(value))) Then value = "0"
                    o = Convert.ChangeType(value, p.ParameterType)
                End If

        End Select

    End Sub

    Private Shared Function GetArgInitialValues(ByVal p As ParameterInfo) As Object

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

    Private Shared Sub FillArgs(ByVal args() As Object, ByVal c As ConstructorInfo, ByVal ruleType As TypesofRules)
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

    Public Shared Function GetRuleArgs(ByRef rule As WFRuleParent) As ArrayList
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


    Private Shared Function GetNewRule(ByVal BaseNode As BaseWFNode, ByVal RuleNameFromUser As String, ByVal RuleName As String, ByVal TypeOfRule As TypesofRules) As WFRuleParent
        Dim t As System.Type = DBBusiness.RulesAssembly.GetType("Zamba.WFActivity.Regular." & RuleName, True, True)
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
        Rule.ParentType = GetRuleType(BaseNode)

        Return Rule
    End Function
    Private Shared Function GetNewChildRule(ByVal parentRule As IRule, ByVal RuleNameFromUser As String, ByVal RuleName As String, ByVal TypeOfRule As TypesofRules) As WFRuleParent
        Dim t As System.Type = DBBusiness.RulesAssembly.GetType("Zamba.WFActivity.Regular." & RuleName, True, True)
        Dim c As ConstructorInfo = t.GetConstructors.GetValue(0)
        Dim Args(c.GetParameters.Length - 1) As Object

        'Primeros dos parametros Standarts del Ctor
        Args(0) = CoreData.GetNewID(IdTypes.WFRule)
        'todo: ver de preguntar por el nombre o tomar el nombre basico de maskname
        Args(1) = RuleNameFromUser
        Args(2) = parentRule.WFStepId
        FillArgs(Args, c, TypesofRules.Regla)
        Dim Rule As WFRuleParent = Activator.CreateInstance(t, Args)

        Rule.ID = Args(0)
        Rule.RuleType = TypeOfRule
        Rule.Enable = True
        Rule.ParentType = parentRule.RuleType
        Rule.ParentRule = parentRule
        Return Rule
    End Function
    Private Shared Function GetNewRule(ByVal WFStep As IWFStep, ByVal RuleNameFromUser As String, ByVal RuleClass As String, ByVal TypeOfRule As TypesofRules) As WFRuleParent
        Dim t As System.Type = DBBusiness.RulesAssembly.GetType("Zamba.WFActivity.Regular." & RuleClass, True, True)
        Dim c As ConstructorInfo = t.GetConstructors.GetValue(0)
        Dim Args(c.GetParameters.Length - 1) As Object

        'Primeros dos parametros Standarts del Ctor
        Args(0) = CoreData.GetNewID(IdTypes.WFRule)
        'todo: ver de preguntar por el nombre o tomar el nombre basico de maskname
        Args(1) = RuleNameFromUser
        Args(2) = WFStep.ID
        FillArgs(Args, c, TypesofRules.Regla)
        Dim Rule As WFRuleParent = Activator.CreateInstance(t, Args)

        Rule.ID = Args(0)
        Rule.RuleType = TypeOfRule
        Rule.Enable = True

        Return Rule
    End Function
    Public Shared Function GetRuleType(baseNode As BaseWFNode) As TypesofRules
        Dim baseNodeType As String = baseNode.GetType().ToString.Replace("Zamba.Core.", String.Empty)
        Select Case baseNodeType
            Case "RuleNode"
                Return DirectCast(baseNode, RuleNode).RuleType
            Case "RuleTypeNode"
                Return DirectCast(baseNode, RuleTypeNode).RuleParentType
            Case Else
                Return 0
        End Select
    End Function

    Private Shared Function GetIfBranch(ByVal cond As Boolean, ByVal WFStepId As Int64, ByVal IfBaseRule As IWFRuleParent) As IWFRuleParent

        Dim t As System.Type = DBBusiness.RulesAssembly.GetType("Zamba.WFActivity.Regular.Ifbranch", True, True)
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



#End Region


    Public Shared Function GetRuleParentType(ByVal BaseNode As BaseWFNode) As TypesofRules
        Try
            Dim pn As RadTreeNode = BaseNode
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
    Private Shared Function GetStepId(ByVal myBaseNode As BaseWFNode) As Int64
        Try
            If TypeOf myBaseNode Is RuleTypeNode Then
                Return DirectCast(myBaseNode.Parent, EditStepNode).WFStep.ID
            ElseIf TypeOf myBaseNode Is RuleNode Then
                Return DirectCast(myBaseNode, RuleNode).WFStepId
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return 0
    End Function
    Public Shared Sub SetRuleExecutionMode(ByVal stepid As Int64, ByVal ruleParentId As Int64, ByVal newRuleId As Int64)
        Try
            If WFRulesBusiness.IsExecutionTaskByTask(stepid, ruleParentId, False) Then
                WFBusiness.SetRulesPreferences(newRuleId, RuleSectionOptions.Configuracion, RulePreferences.TaskExecutionMode, 0)
            Else
                WFBusiness.SetRulesPreferences(newRuleId, RuleSectionOptions.Configuracion, RulePreferences.TaskExecutionMode, 1)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' Se comenta este metodo para evaluar su falta de uso, 
    '''metodo addrules, que tiene hardcoded la regla dodistribuir.
    ''' Si alguien ve que esto provoca un errror, verlo con Martin.
    '''Se utiliza para agregar una regla dodistribuir entre 2 etapas de wfShapes, si alguien tiene algun problema, verlo con Marcelo
    Public Shared Function addDoDistribuirRule(ByVal ParentType As Int64, ByVal RuleName As String, ByVal WFStep1 As IWFStep, ByVal WFStep2 As IWFStep) As Int64
        Dim NewRule As WFRuleParent
        NewRule = GetNewRules(WFStep1, "DoDistribuir", RuleName)
        NewRule.ParentType = ParentType
        AddNewRule(NewRule, 0)
        WFRulesBusiness.UpdateParamItem(NewRule, 0, WFStep2.ID.ToString())
        WFRulesBusiness.UpdateParamItem(NewRule, 1, CInt(True).ToString())
        Return NewRule.ID
    End Function
    Public Shared Sub AddRule(ByVal myBaseNode As BaseWFNode, ByVal NewRule As WFRuleParent)
        Dim parentId As Int64
        Dim NewNode As BaseWFNode

        Try
            If TypeOf myBaseNode Is RuleTypeNode Then
                'Regla Padre
                Dim RuleTypeNode As RuleTypeNode = myBaseNode
                NewRule.ParentType = RuleTypeNode.RuleParentType
                parentId = 0

                'Si el tipo de regla es Evento, la regla será 
                'agregada al nodo que le corresponda
                If NewRule.ParentType = TypesofRules.Eventos Then
                    AddEventRuleNode(RuleTypeNode, Nothing, NewRule, 0)
                Else
                    NewNode = AddRuleNode(RuleTypeNode, Nothing, False, NewRule, 0)
                End If

                'Actualizo text de nodo de accion de usuario y si se modifico lo almaceno en la base
                If RuleTypeNode.RuleParentType = TypesofRules.AccionUsuario Then
                    RuleTypeNode.UpdateUserActionNodeName(NewRule.Name)
                End If
            ElseIf TypeOf myBaseNode Is RuleNode Then
                'Regla de accion
                NewNode = AddRuleNode(myBaseNode, Nothing, False, NewRule, DirectCast(myBaseNode, RuleNode).RuleId)
                NewRule.ParentType = DirectCast(myBaseNode, RuleNode).RuleType
                parentId = DirectCast(myBaseNode, RuleNode).RuleId

                If (NewRule.RuleClass.ToLower.Contains("if")) Then
                    NewRule.Category = 1
                End If
            End If

            AddNewRule(NewRule, parentId)

            If NewNode IsNot Nothing Then
                If NewRule.RuleClass.ToUpper.StartsWith("IF") AndAlso NewRule.RuleClass.ToLower() <> "ifbranch" AndAlso (Not IsNothing(NewNode) OrElse myBaseNode.Text.ToLower.CompareTo("eventos de zamba") = 0) Then
                    Dim rule As WFRuleParent = GetIfBranch(True, NewRule.WFStepId, NewRule)
                    AddRule(NewNode, rule)
                    WFRulesBusiness.UpdateParamItem(rule, 0, "True")
                    Dim rule2 As WFRuleParent = GetIfBranch(False, NewRule.WFStepId, NewRule)
                    AddRule(NewNode, rule2)
                    WFRulesBusiness.UpdateParamItem(rule2, 0, "False")
                End If

                'Si sobre el evento ya existian reglas creadas, se configura el modo de ejecución de la nueva regla.
                If NewRule.ParentType = TypesofRules.Eventos AndAlso NewNode.Parent.Nodes.Count > 1 Then
                    SetRuleExecutionMode(NewRule.WFStepId, DirectCast(NewNode.Parent.Nodes(0), RuleNode).RuleId, NewRule.ID)
                End If

                'Activa la preferencia para Ejecutar cuando exista result
                WFBusiness.SetRulesPreferences(NewRule.ID, RuleSectionOptions.Configuracion, RulePreferences.ConfigurationExecuteWhenResult, 1)
            End If
        Catch ex As Exception
            NewNode.Remove()
            ZClass.raiseerror(ex)
            Throw
        End Try
    End Sub
    Public Shared Function AddChildRule(ByVal parentRule As IRule, ByRef NewRule As WFRuleParent) As IRule

        Try
            'Regla de accion
            NewRule.ParentType = parentRule.RuleType
            NewRule.ParentRule = parentRule
            If (NewRule.RuleClass.ToLower.Contains("if")) Then
                NewRule.Category = 1
            End If


            AddNewRule(NewRule, parentRule.ID)

            If NewRule.RuleClass.ToUpper.StartsWith("IF") AndAlso NewRule.RuleClass.ToLower() <> "ifbranch" Then
                Dim rule As WFRuleParent = GetIfBranch(True, NewRule.WFStepId, NewRule)
                NewRule.ChildRules.Add(AddChildRule(NewRule, rule))
                rule.IfType = True
                WFRulesBusiness.UpdateParamItem(rule, 0, "True")
                Dim rule2 As WFRuleParent = GetIfBranch(False, NewRule.WFStepId, NewRule)
                NewRule.ChildRules.Add(AddChildRule(NewRule, rule2))
                rule2.IfType = False
                WFRulesBusiness.UpdateParamItem(rule2, 0, "False")
            End If

            'Activa la preferencia para Ejecutar cuando exista result
            WFBusiness.SetRulesPreferences(NewRule.ID, RuleSectionOptions.Configuracion, RulePreferences.ConfigurationExecuteWhenResult, 1)
            Return NewRule
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Throw ex
        End Try
    End Function
    Public Shared Function GetNewRule(ByVal ParentType As Int32, ByVal RuleName As String, ByVal WFStep As IWFStep, ByVal ParentId As Int64, RuleClass As String, RuleType As TypesofRules) As IRule
        Dim NewRule As WFRuleParent
        NewRule = GetNewRule(WFStep, RuleClass, RuleName, RuleType)
        NewRule.ParentType = ParentType
        AddNewRule(NewRule, 0)
        Return NewRule
    End Function


    Public Shared Sub DeleteRuleByID(ByVal id As Int64)
        Try
            WFRulesFactory.DeleteRule(id)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' Se comenta este metodo para evaluar su falta de uso, 
    '''era llamado por otro metodo addrules, que tiene hardcoded la regla dodistribuir.
    ''' Si alguien ve que esto provoca un errror, verlo con Martin.
    '''Se utiliza para agregar una regla dodistribuir entre 2 etapas de wfShapes, si alguien tiene algun problema, verlo con Marcelo
    Private Shared Function GetNewRules(ByVal WFStep1 As WFStep, ByVal RuleName As String, ByVal strName As String) As WFRuleParent
        Dim t As System.Type = DBBusiness.RulesAssembly.GetType("Zamba.WFActivity.Regular." & RuleName, True, True)
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
    Public Shared Function CreateNewRule(ByVal RuleName As String, ByVal RuleNameFromUser As String, ByVal stepid As Int64, ByVal TypeOfRule As TypesofRules, ByVal parentID As Int64, ByVal parentType As TypesofRules) As IRule
        Dim t As System.Type = DBBusiness.RulesAssembly.GetType("Zamba.WFActivity.Regular." & RuleName, True, True)
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
    Private Shared Sub AddNewRule(ByVal NewRule As IRule, ByVal parentID As Int64)
        'Inserto Regla 
        WFRulesFactory.InsertRule(NewRule.ID, parentID, NewRule.WFStepId, NewRule.Name, NewRule.RuleType, NewRule.ParentType, NewRule.RuleClass, NewRule.Enable, NewRule.Version)
        'Inserto Paremtros de Regla
        'WFRulesFactory.InsertRuleParam(NewRule)
        'Inserto Items de Regla
        AddRuleItems(NewRule)
    End Sub
    Private Shared Sub AddRuleItems(ByRef rule As WFRuleParent)
        Try

            Dim Arr As ArrayList = GetRuleArgs(rule)
            Dim i As Int32
            For Each o As Object In Arr
                WFRulesFactory.InsertRuleParamItem(rule, i, WFBusiness.ConvertToPersist(o))
                i += 1
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
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
    Public Shared Sub UpdateParamItem(ByVal RuleActionId As Int64, ByVal Item As Int32, ByVal Value As String, Optional ByVal objectTypes As Int32 = ObjectTypes.None, Optional ByVal carp As Boolean = False)
        WFRulesFactory.UpdateParamItem(RuleActionId, Item, Value, objectTypes, carp)

    End Sub

#End Region


    Public Shared Sub UpdateRuleNameByID(ByVal Id As Int64, ByVal Name As String)
        Try
            WFRulesFactory.UpdateRuleNameByID(Id, Name)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "Remove"
    Shared lastParentId As Int32

    Shared NodesToRemove As New List(Of RadTreeNode)

    '''Saca la regla de la base y el arbol
    Friend Shared Sub Remove(ByVal myBaseNode As BaseWFNode, Optional ByVal ask As Boolean = True, Optional ByVal askForAllRules As Boolean = True)
        Try
            lastParentId = 0

            If TypeOf myBaseNode Is RuleNode Then
                Dim dr As DialogResult
                If ask Then
                    'TODO: esto no deberia estar aca 
                    dr = MessageBox.Show("Presione SI para eliminar toda la cadena de reglas." & vbCrLf &
                                                          "Presione NO para eliminar solo la regla seleccionada." & vbCrLf &
                                                          "Presione CANCELAR para no eliminar reglas.", "Eliminar",
                                                          MessageBoxButtons.YesNoCancel,
                                                          MessageBoxIcon.Question,
                                                          MessageBoxDefaultButton.Button3)
                End If

                If Not ask OrElse dr <> DialogResult.Cancel Then
                    Dim nodeToRemove As RuleNode = myBaseNode
                    If nodeToRemove.Nodes.Count > 0 Then
                        If askForAllRules = False OrElse dr = DialogResult.No Then
                            '
                            ' SE ELIMINA SOLO LA REGLA SELECCIONADA SIN BORRAR LAS HIJAS
                            '
                            If nodeToRemove.RuleClass.StartsWith("If") AndAlso Not nodeToRemove.RuleClass.StartsWith("IfB") Then
                                For Each n As RadTreeNode In nodeToRemove.Nodes
                                    NodesToRemove.Add(n)
                                Next
                            End If

                            Dim parentId As Int64
                            Dim category As Int64
                            Dim createNewUserActions As Boolean = (nodeToRemove.ParentType = TypesofRules.AccionUsuario) AndAlso nodeToRemove.Nodes.Count > 1
                            Dim childNode As RuleNode
                            While nodeToRemove.Nodes.Count > 0
                                childNode = nodeToRemove.Nodes(0)

                                'Se pasan los datos del nodo padre del nodo a remover, al nodo hijo
                                childNode.ParentType = nodeToRemove.ParentType
                                childNode.RuleType = nodeToRemove.RuleType

                                'Se quita el nodo hijo y se ubica en el padre
                                childNode.Remove()

                                If createNewUserActions Then
                                    Dim newUserActionNode As New RuleTypeNode(TypesofRules.AccionUsuario, nodeToRemove.WFStepId)
                                    nodeToRemove.Parent.Parent.Nodes.Insert(nodeToRemove.Parent.Index, newUserActionNode)
                                    newUserActionNode.Nodes.Add(childNode)
                                Else
                                    nodeToRemove.Parent.Nodes.Add(childNode)
                                End If

                                parentId = 0
                                If childNode.Parent IsNot Nothing AndAlso TypeOf childNode.Parent Is RuleNode Then
                                    'Si el nodo a remover era hijo de un nodo regla, entonces obtengo el parentId para asignarlo al hijo
                                    parentId = DirectCast(childNode.Parent, RuleNode).RuleId
                                End If
                                If childNode.ParentType = TypesofRules.AccionUsuario Then
                                    'Modifica el nombre de la acción de usuario con la regla hija que le sigue a la borrada
                                    DirectCast(childNode.Parent, RuleTypeNode).UpdateUserActionNodeName(childNode.RuleName)
                                End If
                                If childNode.Parent IsNot Nothing OrElse String.Equals(childNode.Name, "DoDesign") Then
                                    category = 1
                                Else
                                    category = 2
                                End If

                                WFFactory.SetRulesPreferences(childNode.RuleId, RuleSectionOptions.Configuracion, RulePreferences.RuleCategory, category)
                                WFRulesFactory.UpdateRule(childNode.RuleId, childNode.RuleType, parentId, childNode.ParentType, childNode.WFStepId)
                            End While
                            childNode = Nothing

                            If createNewUserActions Then
                                'La acción de usuario original se remueve
                                nodeToRemove.Parent.Remove()
                            End If

                            RemoveNodesFromTree(myBaseNode, nodeToRemove, False)

                        Else
                            '
                            ' SE ELIMINA TODA LA CADENA DE REGLAS JUNTO CON SUS HIJAS
                            '
                            If Not IsNothing(nodeToRemove.RuleId) Then
                                'Borra a las hijas
                                Dim i As Int32

                                For Each childrulenode As RuleNode In nodeToRemove.Nodes
                                    RemoveRuleAndChilds(childrulenode)
                                Next

                                RemoveNodesFromTree(myBaseNode, nodeToRemove)
                            End If
                        End If
                    Else
                        '
                        ' SE ELIMINA LA REGLA SELECCIONADA QUE NO CONTIENE HIJAS
                        '
                        RemoveNodesFromTree(myBaseNode, nodeToRemove)
                    End If

                    RemoveRule(nodeToRemove)

                    If NodesToRemove.Count > 0 Then
                        Dim N As RadTreeNode = NodesToRemove(0)
                        NodesToRemove.Remove(N)
                        WFRulesBusiness.Remove(N, False, False)
                    End If

                End If

            ElseIf TypeOf myBaseNode Is RuleTypeNode Then
                If HaveNodes(myBaseNode) Then
                    Dim SubBaseNode As BaseWFNode = myBaseNode.Nodes(0)
                    RemoveRule(DirectCast(SubBaseNode, RuleNode))
                End If
                myBaseNode.Remove()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método que sirve eliminar las reglas del tree.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Tomas] 21/04/2009  Created
    ''' </history>
    Shared Sub RemoveNodesFromTree(ByRef mybasenode As BaseWFNode, ByRef _RuleNode As RuleNode, Optional ByVal deleteChilds As Boolean = True)
        Try
            'Si la regla pertenecia a un evento se comprueba que el nodo del evento no se encuentre vacio o
            'Si la regla pertenece a una acción de usuario
            If (mybasenode.Parent.Nodes.Count = 1 AndAlso _RuleNode.ParentType = TypesofRules.Eventos) OrElse
                (_RuleNode.ParentType = TypesofRules.AccionUsuario And deleteChilds) Then
                mybasenode.Parent.Remove()
            Else
                _RuleNode.Remove()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método que sirve limpiar los nodos de evento que no tienen reglas adentro.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Tomas] 23/04/2009  Created
    ''' </history>
    Shared Sub CleanUnusedEventNodes(ByRef NodoEventos As RadTreeNode)
        Try
            For Each nodo As RadTreeNode In NodoEventos.Nodes
                If nodo.Nodes.Count = 0 Then
                    nodo.Remove()
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Shared Sub RemoveNodes(ByVal r As List(Of RuleNode))

        For Each CurrentRuleNode As RuleNode In r
            CurrentRuleNode.Parent.Remove()
        Next

        'For index As Integer = 0 To r.Count - 1
        '   DirectCast(r.Item(index), RuleNode).Parent.Remove()
        'Next
    End Sub

    Private Shared Function HaveNodes(ByVal BaseNode As BaseWFNode) As Boolean
        If IsNothing(BaseNode.Nodes) Then
            Return False
        Else
            Return BaseNode.Nodes.Count > 0
        End If
    End Function

    Private Shared Sub RemoveRule(ByRef ruleNode As RuleNode)
        WFRulesFactory.DeleteRule(ruleNode.RuleId)
        UserBusiness.Rights.SaveAction(ruleNode.RuleId, ObjectTypes.WFRules, RightsType.Edit, "Eliminación de regla: '" & ruleNode.RuleName & " (" & ruleNode.RuleId & ")")

        ruleNode.Remove()
        ruleNode = Nothing
    End Sub

    Private Shared Sub RemoveRuleAndChilds(ByRef ruleNode As RuleNode)
        For Each childNode As RuleNode In ruleNode.Nodes
            RemoveRuleAndChilds(childNode)
        Next

        WFRulesFactory.DeleteRule(ruleNode.RuleId)
        UserBusiness.Rights.SaveAction(ruleNode.RuleId, ObjectTypes.WFRules, RightsType.Edit, "Eliminación de regla: '" & ruleNode.RuleName & " (" & ruleNode.RuleId & ")")
    End Sub

#End Region

#Region "COPY, CUT AND PASTE"
    Public Shared Property CuttedRuleNode As RuleNode
    Public Shared Property CopiedRuleNode As RuleNode

    ''' <summary>
    ''' Método que muestra un mensaje que dice si la regla que se va a copiar se copiara sola o junta con sus reglas hijas (si tiene)
    ''' </summary>
    ''' <param name="ruleNode"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	16/09/2008	Created
    ''' </history>
    Public Shared Sub Copy(ByVal ruleToCopy As RuleNode, ByVal copyChildRules As Boolean)
        'Se obtiene la regla a copiar. En realidad el proceso completo se encuentra en el pegar.
        WFRulesBusiness.CopiedRuleNode = ruleToCopy.Clone

        If copyChildRules Then
            RuleNode.CloneChilds(ruleToCopy, CopiedRuleNode)
        End If

        'Si el usuario hizo click en cortar regla, y luego en copiar regla entonces el cortar regla se cancela
        WFRulesBusiness.CuttedRuleNode = Nothing
    End Sub

    ''' <summary>
    ''' Método que corta las correspondientes reglas
    ''' </summary>
    ''' <param name="ruleNode"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	07/10/2008	Modified
    '''                 08/10/2008	Modified    Mejoras en el código. Las reglas hijas de una regla cortada se pasan al ChildRules del padre de la
    '''                                         regla cortada, que después será el ex-padre. También se corrigio la manera de colocar las reglas
    '''                                         cortadas en la parte de Reglas Huerfanas
    ''' </history>
    Public Shared Sub Cut(ByVal cuttedNode As RuleNode)
        Try
            'cortamos tal cual esta la regla, con sus hijas
            WFRulesFactory.SetFloatingRule(cuttedNode.RuleId)
            Dim tempParentType As TypesofRules = cuttedNode.ParentType

            If cuttedNode.Nodes.Count = 0 OrElse MessageBox.Show("¿Desea cortar toda la cadena de reglas?", "Edición de Reglas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                cuttedNode.ParentType = TypesofRules.Floating
                cuttedNode.ParentId = 0
                cuttedNode.RuleType = TypesofRules.Regla
                cuttedNode.NodeWFType = NodeWFTypes.FloatingRule

                'Si es una acción de usuario o evento va en true, ya que luego el parentType se iguala a Floating 
                If cuttedNode.Nodes.Count > 0 AndAlso cuttedNode.RuleType <> tempParentType Then
                    UpdateParentTypeChildNodes(cuttedNode, cuttedNode.RuleType)
                End If

                Dim generalRulesNode As RadTreeNode = cuttedNode.Parent
                If generalRulesNode IsNot Nothing Then
                    'Se busca el nodo de Reglas Generales
                    While generalRulesNode.Parent IsNot Nothing
                        For Each TreeNode As RadTreeNode In generalRulesNode.Nodes
                            If String.Compare(TreeNode.Text, GENERAL_RULES) = 0 Then
                                generalRulesNode = TreeNode
                                Exit While
                            End If
                        Next
                        generalRulesNode = generalRulesNode.Parent
                    End While

                    If TypeOf cuttedNode.Parent Is RuleNode Then
                        'Se quita el nodo de su padre
                        cuttedNode.Remove()
                    ElseIf cuttedNode.Parent IsNot Nothing Then
                        If cuttedNode.Parent.Parent IsNot Nothing AndAlso cuttedNode.Parent.Parent.Text = "Eventos de Zamba" Then
                            Dim eventNode As RadTreeNode = cuttedNode.Parent.Parent
                            cuttedNode.Remove()
                            CleanUnusedEventNodes(eventNode)
                        ElseIf Not IsNothing(cuttedNode.Parent.Parent) AndAlso tempParentType = TypesofRules.AccionUsuario Then
                            cuttedNode.Parent.Remove()
                        Else
                            cuttedNode.Remove()
                        End If
                    End If

                    generalRulesNode.Nodes.Add(cuttedNode)
                End If
            Else
                Dim parentId As Int64
                Dim category As Int64
                Dim createNewUserActions As Boolean = (cuttedNode.ParentType = TypesofRules.AccionUsuario AndAlso cuttedNode.Nodes.Count > 1)
                Dim childNode As RuleNode
                Dim parentNotNothing As Boolean

                While cuttedNode.Nodes.Count > 0
                    childNode = cuttedNode.Nodes(0)

                    'Se pasan los datos del nodo padre del nodo a cortar, al nodo hijo
                    childNode.ParentType = cuttedNode.ParentType
                    childNode.RuleType = cuttedNode.RuleType
                    childNode.Remove()

                    ' Se agrega el nodo ex-hijo al nuevo nodo padre
                    If createNewUserActions Then
                        Dim newUserActionNode As New RuleTypeNode(TypesofRules.AccionUsuario, cuttedNode.WFStepId)
                        cuttedNode.Parent.Parent.Nodes.Insert(cuttedNode.Parent.Index, newUserActionNode)
                        newUserActionNode.Nodes.Add(childNode)
                    Else
                        cuttedNode.Parent.Nodes.Add(childNode)
                    End If

                    parentId = 0
                    parentNotNothing = childNode.Parent IsNot Nothing
                    If parentNotNothing AndAlso TypeOf childNode.Parent Is RuleNode Then
                        'Si el nodo a remover era hijo de un nodo regla, entonces obtengo el parentId para asignarlo al hijo
                        parentId = DirectCast(childNode.Parent, RuleNode).RuleId
                    End If
                    If parentNotNothing AndAlso childNode.ParentType = TypesofRules.AccionUsuario Then
                        childNode.Parent.Text = childNode.RuleName
                    End If
                    If parentNotNothing OrElse String.Equals(childNode.Name, "DoDesign") Then
                        category = 1
                    Else
                        category = 2
                    End If

                    WFFactory.SetRulesPreferences(childNode.RuleId, RuleSectionOptions.Configuracion, RulePreferences.RuleCategory, category)
                    WFRulesFactory.UpdateRule(childNode.RuleId, parentId, childNode.ParentType, childNode.WFStepId)
                End While
                childNode = Nothing

                cuttedNode.ParentType = TypesofRules.Floating
                cuttedNode.ParentId = 0
                cuttedNode.RuleType = TypesofRules.Regla
                cuttedNode.NodeWFType = NodeWFTypes.FloatingRule

                'Si es una acción de usuario o evento va en true, ya que luego el parentType se iguala a Floating 
                If cuttedNode.Nodes.Count > 0 AndAlso cuttedNode.RuleType <> tempParentType Then
                    UpdateParentTypeChildNodes(cuttedNode, cuttedNode.RuleType)
                End If

                'Se busca el nodo de reglas generales
                Dim generalRulesNode As RadTreeNode = cuttedNode.Parent
                While Not (IsNothing(generalRulesNode.Parent))
                    For Each RadTreeNode As RadTreeNode In generalRulesNode.Nodes
                        If RadTreeNode.Text.StartsWith("Reglas Generales") Then
                            generalRulesNode = RadTreeNode
                            Exit While
                        End If
                    Next
                    generalRulesNode = generalRulesNode.Parent
                End While

                If createNewUserActions Then
                    'La acción de usuario original se remueve
                    cuttedNode.Parent.Remove()
                End If
                cuttedNode.Remove()

                generalRulesNode.Nodes.Add(cuttedNode)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método que sirve para pegar reglas en el árbol y actualizar la base de datos de las reglas 
    ''' </summary>
    ''' <param name="myRuleNode">Nodo regla que se quiere copiar o cortar</param>
    ''' <param name="myBaseNode">Nodo sobre el cuál se va a pegar la regla</param>
    ''' <param name="isCopyNode">Bandera que indica si la acción es copiar o cortar</param>
    ''' <remarks></remarks>
    Public Shared Sub PASTE(ByVal myRuleNode As RuleNode, ByVal myBaseNode As BaseWFNode, ByVal isCopyNode As Boolean)
        Try
            If TypeOf myBaseNode Is RuleTypeNode Then
                If Not isCopyNode Then

                    ' Si la regla sobre la que se quiere pegar es una acción de usuario y Si ya tiene una regla principal
                    If GetRuleParentType(myBaseNode) = TypesofRules.AccionUsuario AndAlso myBaseNode.Nodes.Count > 0 Then
                        ' Se crea una nueva acción de usuario para la regla que se quiere pegar
                        AddUserAction(DirectCast(myBaseNode.Parent, EditStepNode).WFStep.ID, DirectCast(myBaseNode.Parent, EditStepNode))

                        For Each TreeNode As RadTreeNode In DirectCast(myBaseNode.Parent, EditStepNode).Nodes
                            If (String.Compare(TreeNode.Text, "Acción de Usuario") = 0) Then
                                ' Se agrega la regla que se acaba de cortar a la acción de usuario
                                myRuleNode.Remove()
                                DirectCast(TreeNode, BaseWFNode).Nodes.Add(myRuleNode)
                                myRuleNode.ParentId = Nothing
                                myRuleNode.ParentNode = Nothing
                                myRuleNode.ParentType = GetRuleParentType(myBaseNode)
                                myRuleNode.NodeWFType = NodeWFTypes.Regla
                                Dim antStepId As Long = myRuleNode.WFStepId
                                myRuleNode.WFStepId = DirectCast(myBaseNode.Parent, EditStepNode).WFStep.ID
                                WFRulesFactory.AttachAFloatingRule(myRuleNode.RuleId, myRuleNode.WFStepId, myRuleNode.RuleType, 0, GetRuleParentType(myBaseNode))
                                DirectCast(myRuleNode.Parent, RuleTypeNode).UpdateUserActionNodeName(myRuleNode.RuleName)

                                ' Si la regla cortada tiene reglas hijas
                                If (myRuleNode.Nodes.Count > 0) Then
                                    ' Si la regla cortada se pego en otra etapa
                                    If (antStepId <> myRuleNode.WFStepId) Then
                                        ' Se actualizan los stepsId de las reglas hijas
                                        updatestepIdsOfChildRules(myRuleNode, myRuleNode.WFStepId, False)
                                    End If
                                End If
                                Exit For
                            End If
                        Next
                        Exit Sub
                    End If

                    Dim tempStepId As Long = myRuleNode.WFStepId
                    Dim tempRuleType As TypesofRules = myRuleNode.ParentType

                    myRuleNode.ParentId = 0
                    myRuleNode.ParentNode = Nothing
                    myRuleNode.NodeWFType = NodeWFTypes.Regla
                    myRuleNode.ParentType = GetRuleParentType(myBaseNode)
                    myRuleNode.WFStepId = DirectCast(myBaseNode.Parent, EditStepNode).WFStep.ID

                    WFRulesFactory.AttachAFloatingRule(myRuleNode.RuleId, myRuleNode.WFStepId, myRuleNode.RuleType, 0, myRuleNode.ParentType)

                    ' Si la regla cortada tiene reglas hijas y Si la regla cortada se pego en otra etapa
                    If myRuleNode.Nodes.Count > 0 Then
                        If tempStepId <> myRuleNode.WFStepId Then
                            ' Se actualizan los stepsId de las reglas hijas
                            updatestepIdsOfChildRules(myRuleNode, myRuleNode.WFStepId, False)
                        End If

                        If myRuleNode.RuleType <> tempRuleType Then
                            UpdateParentTypeChildNodes(myRuleNode, myRuleNode.RuleType)
                        End If
                    End If

                    Dim Nodo As RadTreeNode
                    Nodo = myRuleNode.Parent
                    myRuleNode.Remove()

                    If myRuleNode.ParentType = TypesofRules.Eventos Then

                        AddEventRuleNode(myBaseNode, myRuleNode)
                        Dim parentRuleNode As RuleNode = GetEventFirstRule(myRuleNode.RuleType, myBaseNode)

                        'Si sobre el evento ya existian reglas creadas, se configura el modo de ejecución de la nueva regla.
                        If parentRuleNode IsNot Nothing AndAlso parentRuleNode.RuleId <> myRuleNode.RuleId Then
                            SetRuleExecutionMode(myRuleNode.WFStepId, parentRuleNode.RuleId, myRuleNode.RuleId)
                        End If
                    Else
                        myBaseNode.Nodes.Add(myRuleNode)
                    End If

                    ' Si la regla sobre la que se quiere pegar es una acción de usuario se actualiza el texto del nodo con el nombre de la regla principal
                    If (GetRuleParentType(myBaseNode) = TypesofRules.AccionUsuario) Then
                        DirectCast(myRuleNode.Parent, RuleTypeNode).UpdateUserActionNodeName(myRuleNode.RuleId)
                    End If
                Else
                    Dim childNodes As Boolean = False

                    ' Si el nodo sobre el que se quiere pegar la regla ya contiene nodos hijo
                    If (myBaseNode.Nodes.Count > 0) Then
                        childNodes = True
                        ' de lo contrario, puede ser un nodo "Acción de Usuario" sin nada u otros nodos que no sean reglas y que no tengan nodos hijo
                    Else
                        ' Si el nodo es "Acción de Usuario" entonces se concatena en el texto del nodo el nombre de la regla que se quiere pegar
                        If (String.Compare(myBaseNode.Text.ToLower(), "acción de usuario") = 0) Then
                            myBaseNode.Text = "Acción de Usuario - " & myRuleNode.Text
                        End If
                    End If

                    myBaseNode.Nodes.Add(myRuleNode)
                    DuplicateRule(myRuleNode, myBaseNode)

                    'Si es un evento de zamba lo agrego a su nodo correspondiente
                    If String.Compare(myBaseNode.Text.ToLower(), "eventos de zamba") = 0 Then
                        MoveEventRulesToTheirEventNodes(myBaseNode)
                        Dim parentRuleNode As RuleNode = GetEventFirstRule(myRuleNode.RuleType, myBaseNode)

                        'Si sobre el evento ya existian reglas creadas, se configura el modo de ejecución de la nueva regla.
                        If parentRuleNode IsNot Nothing AndAlso parentRuleNode.RuleId <> myRuleNode.RuleId Then
                            SetRuleExecutionMode(myRuleNode.WFStepId, parentRuleNode.RuleId, myRuleNode.RuleId)
                        End If
                    End If

                    ' Si la regla sobre la que se quiere pegar es una acción de usuario
                    If GetRuleParentType(myBaseNode) = TypesofRules.AccionUsuario Then
                        ' Si ya tiene una regla principal
                        If childNodes Then
                            ' Se crea una nueva acción de usuario para la regla que se quiere pegar
                            AddUserAction(DirectCast(myBaseNode.Parent, EditStepNode).WFStep.ID, DirectCast(myBaseNode.Parent, EditStepNode))

                            For Each TreeNode As RadTreeNode In DirectCast(myBaseNode.Parent, EditStepNode).Nodes
                                If (String.Compare(TreeNode.Text.ToLower, "acción de usuario") = 0) Then
                                    myRuleNode.Remove()
                                    DirectCast(TreeNode, BaseWFNode).Nodes.Add(myRuleNode)
                                    DirectCast(myRuleNode.Parent, RuleTypeNode).UpdateUserActionNodeName(myRuleNode.RuleId)
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If
            ElseIf TypeOf myBaseNode Is RuleNode Then
                If isCopyNode Then
                    DuplicateRule(myRuleNode, myBaseNode)
                    myBaseNode.Nodes.Add(myRuleNode)
                Else
                    Dim tempStepId As Long = myRuleNode.WFStepId
                    Dim tempRuleType As TypesofRules = myRuleNode.ParentType

                    myRuleNode.ParentId = DirectCast(myBaseNode, RuleNode).RuleId
                    myRuleNode.ParentNode = DirectCast(myBaseNode, RuleNode)
                    myRuleNode.NodeWFType = NodeWFTypes.Regla
                    myRuleNode.ParentType = TypesofRules.Regla
                    myRuleNode.WFStepId = DirectCast(myBaseNode, RuleNode).WFStepId

                    WFRulesFactory.AttachAFloatingRule(myRuleNode.RuleId, myRuleNode.WFStepId, myRuleNode.RuleType, myRuleNode.ParentId, TypesofRules.Regla)

                    If myRuleNode.Nodes.Count > 0 Then
                        If tempStepId <> myRuleNode.WFStepId Then
                            updatestepIdsOfChildRules(myRuleNode, myRuleNode.WFStepId, False)
                        End If

                        If myRuleNode.RuleType <> tempRuleType Then
                            UpdateParentTypeChildNodes(myRuleNode, myRuleNode.RuleType)
                        End If
                    End If

                    myRuleNode.Remove()
                    myBaseNode.Nodes.Add(myRuleNode)
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            CuttedRuleNode = Nothing
            CopiedRuleNode = Nothing
        End Try
    End Sub

    Private Shared Sub UpdateParentTypeChildNodes(baseNode As RuleNode, typeToUpdate As TypesofRules)
        'Solo los hijos directos tienen el parentType diferente
        For Each node As RuleNode In baseNode.Nodes
            WFRulesFactory.UpdateParentType(node.RuleId, typeToUpdate)
            node.ParentType = typeToUpdate
        Next
    End Sub

    ''' <summary>
    ''' Método que sirve mover una regla entre los eventos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] 23/04/2009  Created
    ''' </history>
    Shared Sub MoveEventRulesToTheirEventNodes(ByRef eventNodes As BaseWFNode)

        Dim contieneEvento As Boolean = False
        Dim eventoIndex As Int32
        Try
            'Comienzo buscando por el final ya que se agregan al fondo
            For i As Int32 = eventNodes.Nodes.Count - 1 To eventNodes.Nodes.Count - 1 Step -1

                'Compruebo que el nodo sea el nodo de la regla agregada
                If TypeOf (eventNodes.Nodes(i)) Is RuleNode Then

                    'Obtengo la regla
                    Dim rule As RuleNode = eventNodes.Nodes(i)

                    'Comprueba que el nodo con el nombre del evento exista
                    For Each nodo As RadTreeNode In eventNodes.Nodes
                        If String.Equals(nodo.Text.Trim, rule.RuleType.ToString.Trim) Then
                            contieneEvento = True
                            eventoIndex = nodo.Index
                            Exit For
                        End If
                    Next

                    'En caso de existir se agrega a ese nodo, en caso de no 
                    'existir, lo crea y luego se agrega al nodo.
                    If contieneEvento Then

                        'Obtiene el nodo del evento donde se agregará la regla
                        Dim Node As RadTreeNode = eventNodes.Nodes(eventoIndex)
                        '   Se elimina la regla de "Eventos de Zamba"
                        rule.Remove()
                        '                        eventNodes.TreeView.Nodes.Remove(rule)
                        'Agrego la regla al nodo del evento que corresponde
                        Node.Nodes.Add(rule)

                    Else
                        'Agrega el nodo del evento
                        Dim Node As New RuleTypeNode(rule.RuleType, rule.WFStepId)
                        Node.Font = New Font(FontFamily.GenericSansSerif, 7, FontStyle.Regular)
                        Node.ImageIndex = 28
                        'Node.SelectedImageIndex = 28
                        eventNodes.Nodes.Add(Node)
                        '   Se elimina la regla de "Eventos de Zamba"
                        rule.Remove()
                        '                        eventNodes.TreeView.Nodes.Remove(rule)
                        'Agrega el nodo de la regla al nodo del evento y luego sus hijos.
                        Node.Nodes.Add(rule)

                    End If

                Else
                    'Si es un nodo de eventos termino la búsqueda
                    Exit For
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Método recursivo que sirve para duplicar las reglas que se quieren copiar, guardarlas en la base de datos y generar la instancia correspondiente
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <param name="ruleFather"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    17/09/2008	Created
    '''     [Gaston]    01/10/2008	Modified
    '''     [Gaston]    03/10/2008	Modified
    '''     [Gaston]    22/10/2008	Modified    Se comento el childRules.add
    ''' </history>
    ''' <remarks>
    ''' 
    ' Línea comentada debido al siguiente caso: 
    'If (TypeOf ruleFather Is RuleNode) Then
    'DirectCast(ruleFather, RuleNode).Rule.ChildRules.Add(ruleNode.Rule)
    'End If
    ' Ejemplo: Se tiene el siguiente conjunto de reglas:
    ' 1 
    '   ---> 3
    '        ----> 2
    ' Se copian a otra etapa, y se pegan en salida por ejemplo. Se pegan correctamente, ahora bien se decide copiar nuevamente este conjunto de
    ' reglas, pero se copian las reglas duplicadas, y se pegan en el 3 de las reglas duplicadas. No funcionaría porque el 3 al que se quiere 
    ' pegar tiene id (1) por ejemplo y el hijo del 1 que es el 3 tiene el mismo id que el 3 al que se quiere pegar, por lo tanto se entraria 
    ' en un ciclo infinito... ya que al agregar la regla 1 a la colección ChildRules de la regla 3 también se estaría actualizando el childRules
    ' del hijo del 1, que es el 3
    ''' </remarks>
    Private Shared Sub DuplicateRule(ByVal ruleNode As RuleNode, ByVal ruleFather As Object)
        'Se obtiene la instancia de la regla de origen
        Dim rule As WFRuleParent = WFRulesBusiness.GetInstanceRuleById(ruleNode.RuleId, False)

        'Si el nodo de origen era un evento y luego el padre donde se pega no lo es
        'o bien el nodo padre es una regla, se sobreescribe el tipo de regla a Regla.
        If (rule.ParentType = TypesofRules.Eventos AndAlso
            TypeOf (ruleFather) Is RuleTypeNode AndAlso
             DirectCast(ruleFather, RuleTypeNode).RuleParentType <> TypesofRules.Eventos) OrElse
            TypeOf (ruleFather) Is RuleNode Then
            rule.RuleType = TypesofRules.Regla
            ruleNode.RuleType = TypesofRules.Regla
        End If

        'Si la regla copiada va a ser pegada en eventos el ruleType vuelve a ser las etapas.
        'Se valida si es de tipo RuleTypeNode para que cuando se castee no genere exceptions
        'en caso de que la regla a duplicar sea una regla hija.
        If ruleNode.ParentType = TypesofRules.Eventos OrElse
            (TypeOf (ruleFather) Is RuleTypeNode AndAlso
             DirectCast(ruleFather, RuleTypeNode).RuleParentType = TypesofRules.Eventos) Then
            rule.RuleType = ruleNode.RuleType
        Else
            ruleNode.RuleType = rule.RuleType
        End If

        'Se crea la regla duplicada en base a la regla original
        rule = createDuplicateRule(rule, ruleFather)

        'Se actualizan los datos del nodo en base a la regla
        ruleNode.Text = rule.Name & " (" & rule.ID.ToString & ")"
        ruleNode.RuleId = rule.ID
        ruleNode.RuleClass = rule.RuleClass
        ruleNode.RuleEnabled = rule.Enable
        ruleNode.RuleName = rule.Name
        ruleNode.WFStepId = rule.WFStepId
        ruleNode.ParentType = rule.ParentType
        ruleNode.ImageIndex = rule.IconId
        If rule.ParentRule Is Nothing Then
            ruleNode.ParentId = 0
        Else
            ruleNode.ParentId = rule.ParentRule.ID
        End If

        ' Si el usuario no quiere copiar las reglas hijas
        ' Si la regla duplicada tiene reglas hijas, entonces se duplican las reglas hijas en base a las reglas hijas originales
        If ruleNode.Nodes.Count > 0 Then
            For Each childRulenode As RuleNode In ruleNode.Nodes
                DuplicateRule(childRulenode, ruleNode)
            Next
        End If
    End Sub

    ''' </summary>
    ''' Método que sirve para duplicar una regla, idéntica a la regla original
    ''' <param name="originalRule"></param>
    ''' <param name="ruleFather"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    01/10/2008	Created
    '''     [Gaston]    28/10/2008	Modified    Se agrega el método SetRulesPreferences
    '''     [Gaston]    30/10/2008	Modified    Si la regla original no tiene activado el checkbox de Configuración la duplicada tampoco lo tendrá
    '''     [Gaston]    30/12/2008	Modified    Llamada al método que permite duplicar las solapas "Alertas" y "Configuración"
    '''     [Gaston]    19/01/2009	Modified    Llamada al método que permite duplicar las solapas "Habilitación", "Estado" y "Asignación"
    ''' </history>
    Public Shared Function createDuplicateRule(ByRef originalRule As WFRuleParent, ByRef ruleFather As BaseWFNode) As WFRuleParent

        Try
            ' Se crea un nuevo id que es el que tendra la regla duplicada
            Dim duplicateRuleId As Long = CoreData.GetNewID(IdTypes.WFRule)
            Dim ruleType As TypesofRules

            If Not String.IsNullOrEmpty(originalRule.PrintName) Then
                ruleType = originalRule.PrintName
            Else
                ruleType = originalRule.RuleType
            End If

            ' Se inserta la regla duplicada en la base de datos y sus argumentos iguales a la regla original (si es que tiene)
            Dim ruleParamRowItems() As DsRules.WFRuleParamItemsRow = insertDuplicateRule(duplicateRuleId, ruleType, originalRule, ruleFather)
            Dim newRule As IWFRuleParent = createNewRule(duplicateRuleId, originalRule, ruleFather, ruleParamRowItems)

            'Se duplica toda la configuracion de la regla
            CopyRulesPreferences(originalRule.WFStepId, originalRule.ID, duplicateRuleId, newRule.WFStepId)

            ' Se retorna la instancia de la regla
            Return newRule

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try

    End Function

#Region "Duplicación de las solapas 'Alertas', 'Configuración', 'Habilitación', 'Estado' y 'Asignación'"

    '    'Private Shared Sub duplicateRuleConfiguration(ByVal originalRuleId As Long, ByVal duplicateRuleId As Long)
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
    '    'Private Shared Sub duplicateAlertsTab(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

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
    '    'Private Shared Sub duplicateConfigurationTab(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

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
    '    'Private Shared Sub duplicateHabilitationTab(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByRef ruleFather As BaseWFNode)

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
    '    'Private Shared Sub duplicateStateTab(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

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
    '    'Private Shared Sub duplicateAsignationTab(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

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
    '    'Private Shared Sub duplicateCheckBoxs(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)
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
    '    'Private Shared Sub duplicateInternalMessage(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

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
    '    'Private Shared Sub duplicateAutomailMessage(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

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
    '    'Private Shared Sub duplicateMailMessage(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

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
    '    'Private Shared Sub insertGroupsUsersAndExternalUsers(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByVal rule As RulePreferences)

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
    '    'Private Shared Sub insert(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByVal rule As RulePreferences, ByRef desTypes As Integer)
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
    '    'Private Shared Sub insertSubjectOrBody(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByVal rule As RulePreferences)
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
    '    'Private Shared Sub insertAttachDocument(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByVal rule As RulePreferences)
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
    '    'Private Shared Sub insertElementOfAutomail(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByRef rule As RulePreferences)
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
    '    'Private Shared Sub duplicateRadioButtons(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)
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
    '    'Private Shared Sub duplicateItemOfState(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByRef rulePreferences As RulePreferences)
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
    '    'Private Shared Sub duplicateItem(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long)

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
    '    'Private Shared Sub duplicateElementsDisableOrSelectedFromDataBase(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, _
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
    '    'Private Shared Sub duplicateElementsDisableOrSelectedFromDataBase(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByRef ruleFather As BaseWFNode)

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
    '    'Private Shared Sub loadStates(ByVal p_iStepId As Long, ByRef collection As ArrayList)

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
    '    'Private Shared Sub loadStatesForTabHabilitation(ByRef Wfstep As WFStep, ByRef collection As ArrayList)

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
    '    'Private Shared Sub recoverAndLoad_StatesAndUsers_Or_StatesAndGroupsDisable(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, _
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
    '    'Private Shared Sub recoverAndLoadStates_Or_Groups_belongingToAStateDisable(ByRef originalRuleId As Long, _
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
    '    'Private Shared Sub saveTabHabilitationInDuplicateRule(ByRef originalRuleId As Long, ByRef duplicateRuleId As Long, ByRef collection As ArrayList)

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
    Private Shared Function insertDuplicateRule(ByRef duplicateRuleId As Long, ByRef ruleType As TypesofRules, ByRef originalRule As WFRuleParent, ByVal ruleFather As BaseWFNode) As DsRules.WFRuleParamItemsRow()

        Dim ds As DsRules = Nothing

        Try

            If (TypeOf ruleFather Is RuleNode) Then

                Try
                    WFRulesFactory.InsertRule(duplicateRuleId, DirectCast(ruleFather, RuleNode).RuleId, DirectCast(ruleFather, RuleNode).WFStepId,
                                              originalRule.Name, ruleType, DirectCast(ruleFather, RuleNode).RuleType, originalRule.RuleClass, originalRule.Enable,
                                              originalRule.Version)
                Catch ex As Exception
                    WFRulesFactory.InsertRule(duplicateRuleId, DirectCast(ruleFather, RuleNode).RuleId, DirectCast(ruleFather.Parent, EditStepNode).WFStep.ID,
                    originalRule.Name, ruleType, DirectCast(ruleFather, RuleNode).RuleType, originalRule.RuleClass, originalRule.Enable,
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
            If (ds.WFRuleParamItems.Rows.Count > 0) Then

                For Each row As DataRow In ds.WFRuleParamItems.Rows

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
            Return (ds.WFRuleParamItems.Select("Rule_id=" & originalRule.ID))

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
    Private Shared Function createNewRule(ByRef duplicateRuleId As Long, ByRef originalRule As WFRuleParent, ByRef ruleFather As BaseWFNode, ByRef ruleParamRowItems() As DsRules.WFRuleParamItemsRow) As WFRuleParent
        Dim t As System.Type = DBBusiness.RulesAssembly.GetType("Zamba.WFActivity.Regular." & originalRule.RuleClass, True, True)
        Dim c As ConstructorInfo = t.GetConstructors.GetValue(0)
        Dim Args(c.GetParameters.Length - 1) As Object
        Dim i As Byte
        Dim y As Byte

        Dim i_dsRule As DsRules = Nothing

        ' Se genera el ID | Nombre de la regla según el usuario | ID de etapa
        Args(0) = duplicateRuleId
        Args(1) = originalRule.Name

        Try
            Args(2) = DirectCast(ruleFather.Parent, EditStepNode).WFStep.ID
        Catch ex As Exception
            Args(2) = DirectCast(ruleFather, RuleNode).WFStepId
        End Try

        For Each p As ParameterInfo In c.GetParameters
            If i > 2 Then
                If (ruleParamRowItems.Length >= y + 1) Then
                    FillArgsValues(p, Args(i), ruleParamRowItems(y).Value)
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
        DuplicateRuleProperties(Rule, originalRule)

        If (TypeOf ruleFather Is RuleNode) Then
            Rule.ParentRule = WFRulesBusiness.GetInstanceRuleById(DirectCast(ruleFather, RuleNode).RuleId, False)
            Rule.Parent = WFRulesBusiness.GetInstanceRuleById(DirectCast(ruleFather, RuleNode).RuleId, False)
            Rule.ParentType = DirectCast(ruleFather, RuleNode).RuleType
        Else
            Rule.Parent = Nothing
            Rule.ParentRule = Nothing
            Rule.ParentType = DirectCast(ruleFather, RuleTypeNode).RuleParentType
        End If

        Return (Rule)

    End Function

    ''' <summary>
    ''' Este metodo se encarga de duplicar las propiedades de la regla
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <param name="originalRule"></param>
    ''' <remarks></remarks>
    Private Shared Sub DuplicateRuleProperties(rule As WFRuleParent, originalRule As WFRuleParent)
        rule.Enable = originalRule.Enable
        rule.Version = originalRule.Version
        rule.AlertExecution = originalRule.AlertExecution
        rule.Asynchronous = originalRule.Asynchronous
        rule.CleanRule = originalRule.CleanRule
        rule.CloseTask = originalRule.CloseTask
        rule.ContinueWithError = originalRule.ContinueWithError
        rule.DisableChildRules = originalRule.DisableChildRules
        rule.ExecuteRuleInCaseOfError = originalRule.ExecuteRuleInCaseOfError
        rule.ExecuteWhenResult = originalRule.ExecuteWhenResult
        rule.IconId = originalRule.IconId
        rule.MessageToShowInCaseOfError = originalRule.MessageToShowInCaseOfError
        rule.Name = originalRule.Name
        rule.RefreshRule = originalRule.RefreshRule
        rule.RuleIdToExecuteAfterError = originalRule.RuleIdToExecuteAfterError
        rule.RuleType = originalRule.RuleType
        rule.SaveUpdate = originalRule.SaveUpdate
        rule.SaveUpdateInHistory = originalRule.SaveUpdateInHistory
        rule.StartTime = originalRule.StartTime
        rule.ThrowExceptionIfCancel = originalRule.ThrowExceptionIfCancel
        rule.UpdateComment = originalRule.UpdateComment
        rule.Help = originalRule.Help
    End Sub

    ''' <summary>
    ''' Método que sirve para actualizar los stepIds de las reglas hijas cortadas y que se quieren pegar a otra etapa
    ''' </summary>
    ''' <param name="rule">Instancia de regla</param>
    ''' <param name="ban">La regla padre no se actualiza, ya está actualizada</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	04/11/2008	Created
    ''' </history>
    Private Shared Sub updatestepIdsOfChildRules(ByRef rulenode As RuleNode, wfstepid As Int64, ByRef ban As Boolean)

        If (ban = True) Then
            ' Se actualiza el step id de la regla en la base de datos
            WFRulesFactory.updateStepIdofRule(rulenode.RuleId, wfstepid)
        End If

        For Each childRulenode As RuleNode In rulenode.Nodes
            childRulenode.WFStepId = wfstepid
            updatestepIdsOfChildRules(childRulenode, wfstepid, True)
        Next

    End Sub

#End Region

#Region "UserAction"
    Public Shared Sub AddUserAction(ByVal stepId As Int64, ByVal stepNode As EditStepNode)
        Try
            Dim UserActionNode As New RuleTypeNode(TypesofRules.AccionUsuario, stepId)
            stepNode.Nodes.Insert(stepNode.Nodes.IndexOf(DirectCast(stepNode.ScheduleNode, RuleTypeNode)), UserActionNode)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub



#End Region

#Region "Load"
    Public Shared Sub LoadRules(ByVal wf As WorkFlow, ByVal treeView As RadTreeView, ByVal LoadTreePanel As Boolean)
        Try
            If Not LoadTreePanel Then
                treeView.Nodes.Clear()
            End If

            'Nodo Inicial
            Dim Toproot As New WFNode(wf)
            Toproot.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, 0)
            Toproot.ForeColor = Color.FromArgb(70, 70, 70)

            treeView.Nodes.Add(Toproot)
            'Nodos de Etapas
            For Each s As WFStep In wf.Steps.Values
                Try
                    Dim StepNode As New EditStepNode(s, wf.InitialStep)
                    If LoadTreePanel Then
                        '(pablo)
                        'quito los nodos innecesarios para el
                        'arbol de habilitacion de reglas
                        StepNode.Nodes.Remove(DirectCast(StepNode.RightNode, RightNode))
                        StepNode.Nodes.Remove(DirectCast(StepNode.EventNode, RuleTypeNode))
                        StepNode.Nodes.Remove(DirectCast(StepNode.InputNode, RuleTypeNode))
                        StepNode.Nodes.Remove(DirectCast(StepNode.InputValidationNode, RuleTypeNode))
                        StepNode.Nodes.Remove(DirectCast(StepNode.OutputNode, RuleTypeNode))
                        StepNode.Nodes.Remove(DirectCast(StepNode.OutputValidationNode, RuleTypeNode))
                        StepNode.Nodes.Remove(DirectCast(StepNode.UpdateNode, RuleTypeNode))
                        StepNode.Nodes.Remove(DirectCast(StepNode.ScheduleNode, RuleTypeNode))
                    End If
                    StepNode.ImageIndex = -1
                    Toproot.Nodes.Add(StepNode)

                    AddRulesNodes(StepNode, LoadTreePanel)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Next

            If Toproot.Nodes.Count = 1 Then
                Toproot.Nodes(0).Expand()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Shared Sub AddRulesNodes(ByVal stepNode As EditStepNode, ByVal LoadTreePanel As Boolean)
        Dim IsFirst As Boolean = True
        Dim Name As String = String.Empty
        Dim dt As DataTable = Nothing

        Dim DTStepsRulesOptions As DataTable = WFRulesBusiness.GetRuleOptionsDT(False, stepNode.WFStep.ID)
        Dim DVRuleOptions As DataView
        If DTStepsRulesOptions IsNot Nothing Then
            DVRuleOptions = New DataView(DTStepsRulesOptions)
        End If

        Try
            For Each r As DsRules.WFRulesRow In stepNode.WFStep.DSRules.WFRules.Rows
                If Not DVRuleOptions Is Nothing Then DVRuleOptions.RowFilter = "RuleId = " & r.Id

                If r.ParentId = 0 Then


                    Select Case r.ParentType
                        Case TypesofRules.Entrada
                            AddRuleNode(stepNode.InputNode, stepNode.WFStep.DSRules, LoadTreePanel, r, DVRuleOptions)
                        Case TypesofRules.Eventos
                            AddEventRuleNode(stepNode.EventNode, stepNode.WFStep.DSRules, r, DVRuleOptions)
                        Case TypesofRules.AccionUsuario
                            Dim UserActionNode As RuleTypeNode
                            If IsFirst Then
                                UserActionNode = stepNode.UserActionNode
                                IsFirst = False
                            Else
                                UserActionNode = New RuleTypeNode(TypesofRules.AccionUsuario, stepNode.WFStep.ID)
                                stepNode.Nodes.Insert(stepNode.Nodes.IndexOf(DirectCast(stepNode.ScheduleNode, RuleTypeNode)), UserActionNode)
                            End If

                            AddRuleNode(UserActionNode, stepNode.WFStep.DSRules, LoadTreePanel, r, DVRuleOptions)

                            'Actualiza el nombre
                            Try
                                dt = GetRuleOption(r.step_Id, r.Id, 0, Icons.Warning, 0, True)
                                If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                                    Name = dt.Rows(0).Item("ObjExtraData").ToString()
                                End If
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try

                            If String.IsNullOrEmpty(Name) Then
                                If String.Compare(r._Class, "DOExecuteRule") = 0 AndAlso r.Name.StartsWith("Ejecutar Regla ") Then
                                    UserActionNode.UpdateUserActionNodeName(r.Name.Replace("Ejecutar Regla ", String.Empty))
                                Else
                                    UserActionNode.UpdateUserActionNodeName(r.Name)
                                End If
                            Else
                                UserActionNode.UpdateUserActionNodeName(Name)
                            End If
                            Name = String.Empty
                        Case TypesofRules.Floating
                            AddRuleNode(stepNode.FloatingNode, stepNode.WFStep.DSRules, LoadTreePanel, r, DVRuleOptions)
                        Case TypesofRules.Actualizacion
                            AddRuleNode(stepNode.UpdateNode, stepNode.WFStep.DSRules, LoadTreePanel, r, DVRuleOptions)
                        Case TypesofRules.Planificada
                            AddRuleNode(stepNode.ScheduleNode, stepNode.WFStep.DSRules, LoadTreePanel, r, DVRuleOptions)
                        Case TypesofRules.ValidacionEntrada
                            AddRuleNode(stepNode.InputValidationNode, stepNode.WFStep.DSRules, LoadTreePanel, r, DVRuleOptions)
                        Case TypesofRules.Salida
                            AddRuleNode(stepNode.OutputNode, stepNode.WFStep.DSRules, LoadTreePanel, r, DVRuleOptions)
                        Case TypesofRules.ValidacionSalida
                            AddRuleNode(stepNode.OutputValidationNode, stepNode.WFStep.DSRules, LoadTreePanel, r, DVRuleOptions)
                    End Select
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            If dt IsNot Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Sub

    Private Shared Function AddRuleNode(parentNode As BaseWFNode,
                                        DSRules As DsRules,
                                        loadPanel As Boolean,
                                        rule As WFRuleParent,
                                        parentId As Int64) As RuleNode
        Return AddRuleNode(parentNode,
                           DSRules,
                           loadPanel,
                           rule.ID,
                           rule.Name,
                           rule.RuleClass,
                           rule.Enable,
                           rule.RuleType,
                           parentId,
                           rule.ParentType,
                           rule.WFStepId, Nothing)
    End Function
    Private Shared Function AddRuleNode(parentNode As BaseWFNode,
                                        DSRules As DsRules,
                                        loadPanel As Boolean,
                                        rule As DsRules.WFRulesRow, DVRuleOptions As DataView) As RuleNode
        Return AddRuleNode(parentNode,
                   DSRules,
                   loadPanel,
                   rule.Id,
                   rule.Name,
                   rule._Class,
                   rule.Enable,
                   rule.Type,
                   rule.ParentId,
                   rule.ParentType,
                   rule.step_Id, DVRuleOptions)
    End Function
    Private Shared Function AddRuleNode(parentNode As BaseWFNode,
                                        DSRules As DsRules,
                                        loadPanel As Boolean,
                                        ruleId As Int64,
                                        ruleName As String,
                                        ruleClass As String,
                                        ruleEnable As Boolean,
                                        ruleType As TypesofRules,
                                        ruleParentId As Int64,
                                        ruleParentType As TypesofRules,
                                        stepId As Int64, DVRuleOptions As DataView) As RuleNode
        Try
            Dim icon As Icons = Icons.YellowBall


            If Not DVRuleOptions Is Nothing Then

                Dim DTRuleOptions As DataTable = DVRuleOptions.ToTable()

                ruleEnable = True
                Dim DisableChildRules As Boolean = False
                Dim RefreshTask As Boolean = False
                Dim IconId As Int32 = Icons.YellowBall
                If DTRuleOptions.Rows.Count > 0 Then
                    For Each o As DataRow In DTRuleOptions.Rows
                        Select Case o("ObjectId")
                            Case 0
                                ruleEnable = o("ObjValue") 'DirectCast(o("ObjValue"), Decimal)
                            Case 59
                                DisableChildRules = o("ObjValue")
                            Case 42
                                RefreshTask = o("ObjValue")
                            Case 63
                                IconId = o("ObjValue")
                        End Select
                    Next
                End If
                icon = GetIcon(ruleEnable, ruleClass, DisableChildRules, RefreshTask, IconId, ruleName)
            End If

            'agrega la regla al nodo y verifica si tiene child para agregar
            Dim ruleNode As New RuleNode(ruleId, ruleName, ruleClass, ruleEnable, ruleType, ruleParentId, ruleParentType, stepId, icon)
            parentNode.Nodes.Add(ruleNode)

            If Not loadPanel AndAlso DSRules IsNot Nothing Then
                For Each r As DsRules.WFRulesRow In DSRules.WFRules.Select("ParentId = " & ruleId)
                    '                    If r.ParentId = ruleId Then
                    If Not DVRuleOptions Is Nothing Then DVRuleOptions.RowFilter = "RuleId = " & r.Id
                    AddRuleNode(ruleNode, DSRules, loadPanel, r, DVRuleOptions)
                    'End If
                Next
            End If

            'Dim breakpointId = WFFactory.GetBreakpointIDByRuleIdAndUserID(ruleId, Membership.MembershipHelper.CurrentUser.ID)
            'ruleNode.Checked = breakpointId > 0

            Return ruleNode
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Private Shared Sub AddEventRuleNode(parentNode As BaseWFNode,
                                        dsRules As DsRules,
                                        rule As WFRuleParent,
                                        ruleParentId As Int64)
        AddEventRuleNode(parentNode,
                         dsRules,
                         rule.ID,
                         rule.Name,
                         rule.RuleClass,
                         rule.Enable,
                         rule.RuleType,
                         ruleParentId,
                         rule.ParentType,
                         rule.WFStepId, Nothing)
    End Sub
    Private Shared Sub AddEventRuleNode(parentNode As BaseWFNode,
                                        dsRules As DsRules,
                                        rule As DsRules.WFRulesRow, DVRuleOptions As DataView)
        AddEventRuleNode(parentNode,
                         dsRules,
                         rule.Id,
                         rule.Name,
                         rule._Class,
                         rule.Enable,
                         rule.Type,
                         rule.ParentId,
                         rule.ParentType,
                         rule.step_Id, DVRuleOptions)
    End Sub
    Private Shared Sub AddEventRuleNode(parentNode As BaseWFNode,
                                        dsRules As DsRules,
                                        rule As RuleNode, DVRuleOptions As DataView)
        AddEventRuleNode(parentNode,
                 dsRules,
                 rule.RuleId,
                 rule.Name,
                 rule.RuleClass,
                 rule.RuleEnabled,
                 rule.RuleType,
                 rule.ParentId,
                 rule.ParentType,
                 rule.WFStepId, DVRuleOptions)
    End Sub
    Private Shared Sub AddEventRuleNode(parentNode As BaseWFNode,
                                        dsRules As DsRules,
                                        ruleId As Int64,
                                        ruleName As String,
                                        ruleClass As String,
                                       ruleEnable As Boolean,
                                        ruleType As TypesofRules,
                                       ruleParentId As Int64,
                                       ruleParentType As TypesofRules,
                                       stepId As Int64, DVRuleOptions As DataView)
        Dim contieneEvento As Boolean = False
        Dim eventoIndex As Int32
        Dim ruleTypeName As String

        Try

            Dim icon As Icons = Icons.YellowBall


            If Not DVRuleOptions Is Nothing Then

                Dim DTRuleOptions As DataTable = DVRuleOptions.ToTable()

                ruleEnable = True
                Dim DisableChildRules As Boolean = False
                Dim RefreshTask As Boolean = False
                Dim IconId As Int32 = Icons.YellowBall
                If DTRuleOptions.Rows.Count > 0 Then
                    For Each o As DataRow In DTRuleOptions.Rows
                        Select Case o("ObjectId")
                            Case 0
                                If o("ObjValue") Is Nothing OrElse o("ObjValue").ToString.Length = 0 OrElse o("ObjValue").ToString = 0 Then

                                    ruleEnable = False
                                Else
                                    ruleEnable = True
                                End If
                            Case 59
                                If o("ObjValue") Is Nothing OrElse o("ObjValue").ToString.Length = 0 OrElse o("ObjValue").ToString = 0 Then

                                    DisableChildRules = False
                                Else
                                    DisableChildRules = True
                                End If


                            Case 42
                                If o("ObjValue") Is Nothing OrElse o("ObjValue").ToString.Length = 0 OrElse o("ObjValue").ToString = 0 Then

                                    RefreshTask = False
                                Else
                                    RefreshTask = True
                                End If

                            Case 63
                                IconId = o("ObjValue")
                        End Select
                    Next
                End If
                icon = GetIcon(ruleEnable, ruleClass, DisableChildRules, RefreshTask, IconId, ruleName)
            End If

            Dim ruleNode As New RuleNode(ruleId, ruleName, ruleClass, ruleEnable, ruleType, ruleParentId, ruleParentType, stepId, icon)
            ruleNode.RuleType = ruleType

            'Comprueba que el nodo con el nombre del evento exista
            ruleTypeName = ruleType.ToString()
            For Each nodo As RadTreeNode In parentNode.Nodes
                If String.Compare(nodo.Text, ruleTypeName) = 0 Then
                    contieneEvento = True
                    eventoIndex = nodo.Index
                    Exit For
                End If
            Next

            'En caso de existir se agrega a ese nodo, en caso de no 
            'existir, lo crea y luego se agrega al nodo.
            Dim eventNode As RuleTypeNode
            If contieneEvento Then
                eventNode = parentNode.Nodes(eventoIndex)
                eventNode.Nodes.Add(ruleNode)
            Else
                eventNode = New RuleTypeNode(ruleType, stepId)
                eventNode.Font = New Font(FontFamily.GenericSansSerif, 7, FontStyle.Regular)
                eventNode.ImageIndex = 28
                ' eventNode.SelectedImageIndex = 28
                parentNode.Nodes.Add(eventNode)
                eventNode.Nodes.Add(ruleNode)
            End If

            If dsRules IsNot Nothing Then
                For Each r As DsRules.WFRulesRow In dsRules.WFRules.Rows
                    If r.ParentId = ruleId Then
                        If Not DVRuleOptions Is Nothing Then DVRuleOptions.RowFilter = "RuleId = " & r.Id
                        AddRuleNode(ruleNode, dsRules, False, r, DVRuleOptions)
                    End If
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            contieneEvento = Nothing
            eventoIndex = Nothing
        End Try
    End Sub
    Private Shared Sub AddEventRuleNode(ByVal parentNode As BaseWFNode,
                                        ByVal ruleNode As RuleNode)
        Dim contieneEvento As Boolean = False
        Dim eventoIndex As Int32

        Try
            'Comprueba que el nodo con el nombre del evento exista
            For Each nodo As RadTreeNode In parentNode.Nodes
                If String.Compare(nodo.Text, ruleNode.RuleType.ToString()) = 0 Then
                    contieneEvento = True
                    eventoIndex = nodo.Index
                    Exit For
                End If
            Next

            'En caso de existir se agrega a ese nodo, en caso de no 
            'existir, lo crea y luego se agrega al nodo.
            If contieneEvento Then
                Dim EventNode As RuleTypeNode = parentNode.Nodes(eventoIndex)
                EventNode.Nodes.Add(ruleNode)
            Else
                Dim EventNode As New RuleTypeNode(ruleNode.RuleType, ruleNode.WFStepId)
                EventNode.Font = New Font(FontFamily.GenericSansSerif, 7, FontStyle.Regular)
                EventNode.ImageIndex = 28
                'EventNode.SelectedImageIndex = 28
                parentNode.Nodes.Add(EventNode)
                EventNode.Nodes.Add(ruleNode)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Shared Function GetEventFirstRule(ByVal nodeType As TypesofRules, ByVal eventNode As BaseWFNode) As RuleNode
        Try
            Dim eventIndex As Int32
            Dim nodeTypeName As String = nodeType.ToString()

            'Encuentra el Atributo del tipo de evento
            For Each node As RadTreeNode In eventNode.Nodes
                If String.Compare(node.Text, nodeTypeName) = 0 Then
                    eventIndex = node.Index
                    Exit For
                End If
            Next

            Return DirectCast(eventNode.Nodes(eventIndex).Nodes(0), RuleNode)

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Public Shared Sub LoadMonitorRules(ByRef wfstep As WFStep, ByVal TreeView As RadTreeView)
        Try
            TreeView.Nodes.Clear()
            Dim wf As IWorkFlow
            wf = WFBusiness.GetWFbyId(wfstep.WorkId)
            Dim StepNode As New EditStepNode(wfstep, wf.InitialStep)
            AddRulesNodes(StepNode, False)
            'agrego los subnodos de wfstepnode al treeview
            For Each n As RadTreeNode In StepNode.Nodes
                If Not TypeOf n Is RightNode Then
                    TreeView.Nodes.Add(n)
                End If
            Next
            TreeView.ExpandAll()
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
    Private Shared Sub SendInternalMessageAlert(ByVal stepid As Int64, ByVal ruleid As Int64)
        Try
            Dim _Body As String
            Dim ToMails As Generic.List(Of User)
            Dim CCMails As Generic.List(Of User)
            Dim CCOMails As Generic.List(Of User)
            Dim _Subject As String
            Dim dt As DataTable
            'Todo: Mensaje Interno Falta Adjuntar Documento

            'Obtengo el Body
            _Body = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertInternalMessageBody, 0, True, ValueTypes.Extra)

            'Obtengo El Subject
            _Subject = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertInternalMessageSubject, 0, True, ValueTypes.Extra)


            Dim tempUser As User

            'Obtengo los usuarios (PARA)
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailFor, 1, True)

            If Not IsNothing(dt) Then

                For Each dr As DataRow In dt.Rows
                    If IsNothing(ToMails) Then
                        ToMails = New Generic.List(Of User)
                    End If
                    tempUser = UserBusiness.GetUserById(Int64.Parse(dr.Item(0)))

                    If ToMails.Contains(tempUser) = False Then
                        ToMails.Add(tempUser)
                    End If
                Next
            End If

            'Obtengo los usuarios de los grupos (PARA)
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailFor, 2, True)
            If Not IsNothing(dt) Then
                For Each dr As DataRow In dt.Rows
                    For Each Item As User In UserGroupBusiness.GetUsersByGroup(Int64.Parse(dr.Item(0).ToString))
                        If IsNothing(ToMails) Then
                            ToMails = New Generic.List(Of User)
                        End If
                        If ToMails.Contains(Item) = False Then
                            ToMails.Add(Item)
                        End If
                    Next
                Next
            End If


            'Obtengo los usuarios (CC)
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCC, 1, True)

            If Not IsNothing(dt) Then
                For Each dr As DataRow In dt.Rows
                    If IsNothing(CCMails) Then
                        CCMails = New Generic.List(Of User)
                    End If
                    tempUser = UserBusiness.GetUserById(Int64.Parse(dr.Item(0)))

                    If CCMails.Contains(tempUser) = False Then
                        CCMails.Add(tempUser)
                    End If
                Next
            End If

            'Obtengo los usuarios de los grupos (CC)

            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCC, 2, True)
            If Not IsNothing(dt) Then
                For Each dr As DataRow In dt.Rows
                    For Each Item As User In UserGroupBusiness.GetUsersByGroup(Int64.Parse(dr.Item(0).ToString))
                        If IsNothing(CCMails) Then
                            CCMails = New Generic.List(Of User)
                        End If
                        If CCMails.Contains(Item) = False Then
                            CCMails.Add(Item)
                        End If
                    Next
                Next
            End If
            'Obtengo los usuarios (CCO)
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCCO, 1, True)
            If Not IsNothing(dt) Then

                For Each dr As DataRow In dt.Rows
                    If IsNothing(CCOMails) Then
                        CCOMails = New Generic.List(Of User)
                    End If
                    tempUser = UserBusiness.GetUserById(Int64.Parse(dr.Item(0)))

                    If CCOMails.Contains(tempUser) = False Then
                        CCOMails.Add(tempUser)
                    End If
                Next
            End If
            'Obtengo los usuarios de los grupos (CCO)
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCCO, 2, True)
            If Not IsNothing(dt) Then
                For Each dr As DataRow In dt.Rows
                    For Each Item As User In UserGroupBusiness.GetUsersByGroup(Int64.Parse(dr.Item(0).ToString))
                        If IsNothing(CCOMails) Then
                            CCOMails = New Generic.List(Of User)
                        End If
                        If CCOMails.Contains(Item) = False Then
                            CCOMails.Add(Item)
                        End If
                    Next
                Next
            End If


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
    Private Shared Sub SendMailAlert(ByVal stepid As Int64, ByVal ruleid As Int64, ByVal results As List(Of ITaskResult))

        Try
            Dim _body As String
            Dim _Subject As String
            Dim ToMails As StringBuilder '[sebastian 06-04-09] it changed to stringbuilder
            Dim CCMails As List(Of String)
            Dim CCOMails As List(Of String)
            Dim attach As Boolean
            Dim dt As DataTable

            'Obtengo el Body
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailBody, 0, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                _body = dt.Rows(0).Item("ObjExtraData").ToString
            End If

            'Obtengo el Subject
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailSubject, 0, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                _Subject = dt.Rows(0).Item("ObjExtraData").ToString
            End If

            'Adjunta o no los documentos
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailAttachDocument, 0, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("ObjValue") = 1 Then
                    attach = True
                Else
                    attach = False
                End If
            End If

            Dim tempMail As String

            'Obtengo los usuarios (PARA)
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailFor, 1, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then

                For Each dr As DataRow In dt.Rows
                    If IsNothing(ToMails) Then
                        ToMails = New StringBuilder
                    End If
                    tempMail = (UserBusiness.GetUserById(Int64.Parse(dr.Item("ObjValue")))).eMail.Mail

                    If ToMails.ToString.Contains(tempMail) = False Then
                        ToMails.Append(tempMail)
                        ToMails.Append(";")
                    End If
                Next
            End If

            'Obtengo los usuarios de los grupos (PARA)
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailFor, 2, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
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
            End If

            'Obtengo los Mails Externos (PARA)
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailFor, 3, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If IsNothing(ToMails) Then
                        ToMails = New StringBuilder
                    End If
                    If ToMails.ToString.Contains(dr.Item("ObjValue").ToString) = False Then
                        ToMails.Append(dr.Item("ObjValue").ToString)
                        ToMails.Append(";")
                    End If
                Next
            End If

            'Obtengo los usuarios (CC)
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCC, 1, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then

                For Each dr As DataRow In dt.Rows
                    If IsNothing(CCMails) Then
                        CCMails = New List(Of String)
                    End If
                    tempMail = (UserBusiness.GetUserById(Int64.Parse(dr.Item("ObjValue")))).eMail.Mail

                    If CCMails.Contains(tempMail) = False Then
                        CCMails.Add(tempMail)
                    End If
                Next
            End If

            'Obtengo los usuarios de los grupos (CC)
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCC, 2, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    For Each Item As User In UserGroupBusiness.GetUsersByGroup(Int64.Parse(dr.Item("ObjValue").ToString))
                        If IsNothing(CCMails) Then
                            CCMails = New List(Of String)
                        End If
                        If CCMails.Contains(Item.eMail.Mail) = False Then
                            CCMails.Add(Item.eMail.Mail)
                        End If
                    Next
                Next
            End If

            'Obtengo los Mails Externos (CC)
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCC, 3, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If IsNothing(CCMails) Then
                        CCMails = New List(Of String)
                    End If
                    If CCMails.Contains(dr.Item("ObjValue").ToString) = False Then
                        CCMails.Add(dr.Item("ObjValue").ToString)
                    End If
                Next
            End If

            'Obtengo los usuarios (CCO)
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCCO, 1, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If IsNothing(CCOMails) Then
                        CCOMails = New List(Of String)
                    End If
                    tempMail = (UserBusiness.GetUserById(Int64.Parse(dr.Item("ObjValue")))).eMail.Mail

                    If CCOMails.Contains(tempMail) = False Then
                        CCOMails.Add(tempMail)
                    End If
                Next
            End If

            'Obtengo los usuarios de los grupos (CCO)
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCCO, 2, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    For Each Item As User In UserGroupBusiness.GetUsersByGroup(Int64.Parse(dr.Item("ObjValue").ToString))
                        If IsNothing(CCOMails) Then
                            CCOMails = New List(Of String)
                        End If
                        If CCOMails.Contains(Item.eMail.Mail) = False Then
                            CCOMails.Add(Item.eMail.Mail)
                        End If
                    Next
                Next
            End If

            'Obtengo los Mails Externos (CCO)
            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertMailCCO, 3, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If IsNothing(CCOMails) Then
                        CCOMails = New List(Of String)
                    End If
                    If CCOMails.Contains(dr.Item("ObjValue").ToString) = False Then
                        CCOMails.Add(dr.Item("ObjValue").ToString)
                    End If
                Next
            End If

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

            MessagesBusiness.SendMail(ToMails.ToString, cc, cco, _Subject, _body, True)

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Envia una alerta de ejecucion de regla Mediante Automail, Recuperando datos de la base ZRuleOpt
    ''' </summary>
    ''' <param name="ruleid"></param>
    ''' <remarks></remarks>
    Private Shared Sub SendAutomailAlert(ByVal stepid As Int64, ByVal ruleid As Int64)
        Try
            Dim dt As DataTable
            Dim _automail As AutoMail
            Dim _userName As String
            Dim _userPass As String
            Dim _port As String
            Dim _server As String

            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailId, 0, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                _automail = MessagesBusiness.GetAutomailById(dt.Rows(0).Item("ObjValue"))
            End If

            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPUser, 0, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                _userName = dt.Rows(0).Item("ObjExtraData").ToString
            End If

            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPPass, 0, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                _userPass = dt.Rows(0).Item("ObjExtraData").ToString
            End If

            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPPort, 0, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                _port = dt.Rows(0).Item("ObjExtraData").ToString
            End If

            dt = WFRulesBusiness.GetRuleOption(stepid, ruleid, RuleSectionOptions.Alerta, RulePreferences.AlertAutomailSMTPProvider, 0, True)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                _server = dt.Rows(0).Item("ObjExtraData").ToString
            End If

            Dim smtp As SMTP_Validada = New SMTP_Validada(_userName, _userPass, Int32.Parse(_port), _server)
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
    Private Shared Sub SendNotificationAlert(ByRef rule As WFRuleParent, ByVal results As List(Of ITaskResult))
        Dim dt As DataTable = WFRulesBusiness.GetRuleOption(rule.WFStepId, rule.ID, RuleSectionOptions.Alerta, RulePreferences.AlertNotificationMode, 0, True)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item(0) = 1 Then
                For Each Item As String In dt.Rows(0).Item(1).ToString.ToUpper.Split("|")
                    If Not String.IsNullOrEmpty(Item) Then
                        Select Case Item
                            Case "MENSAJE INTERNO"
                                SendInternalMessageAlert(rule.WFStepId, rule.ID)
                            Case "MAIL"
                                SendMailAlert(rule.WFStepId, rule.ID, results)
                            Case "AUTOMÁTICO"
                                SendAutomailAlert(rule.WFStepId, rule.ID)
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
        Try

            If (taskResults.Count < 1 AndAlso IsNothing(taskResults(0))) Then Exit Sub

            Dim wfstep As IWFStep = WFStepBusiness.GetStepById(taskResults(0).StepId, False)
            Dim DVDSRulesStart As New DataView(wfstep.DSRules.WFRules)
            DVDSRulesStart.RowFilter = "Type = 30"
            For Each RuleRow As DataRow In DVDSRulesStart.ToTable().Rows ' wfstep.Rules
                '     If Rule.RuleType = TypesofRules.Iniciar Then
                Dim WFRB As New WFRulesBusiness
                Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), wfstep.ID)
                taskResults = WFRB.ExecutePrimaryRule(Rule, taskResults, Nothing)
                'End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub



    ''' <summary>
    ''' Setea el usuario asignado segun preferencia de regla
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <param name="newresults"></param>
    ''' <remarks></remarks>
    ''' <history>[Diego]28-07-2008 Created</history>
    Public Shared Sub SetTasksAsignedUser(ByVal stepid As Int64, ByVal ruleId As Int64, ByRef newresults As List(Of ITaskResult))
        Try
            Dim dt As DataTable = WFBusiness.recoverItemsSelected(stepid, ruleId, RuleSectionOptions.Asignacion, RulePreferences.AsignationTypeUser, RulePreferences.AsignationTypeGroup, RulePreferences.AsignationTypeManual, True)

            If dt.Rows.Count < 1 Then Exit Sub

            Dim id As Long = 0
            Dim Username As String = String.Empty

            Dim Selection As Integer = dt.Rows(0).Item("OBJECTID")
            Select Case Selection
                Case RulePreferences.AsignationTypeUser, RulePreferences.AsignationTypeGroup
                    id = dt.Rows(0).Item("OBJVALUE")
                    Username = UserGroupBusiness.GetUserorGroupNamebyId(id)
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

            For Each r As ITaskResult In newresults
                WFTaskBusiness.Asign(r, id, Membership.MembershipHelper.CurrentUser.ID, Username)
            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Setea el usuario asignado segun preferencia de regla
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <param name="newresults"></param>
    ''' <remarks></remarks>
    ''' <history>[Diego]28-07-2008 Created</history>
    Public Shared Sub SetTasksAsignedUser(ByVal rule As WFRuleParent, ByRef newresults As List(Of ITaskResult))
        Dim dt As DataTable = WFBusiness.recoverItemsSelected(rule.WFStepId, rule.ID, RuleSectionOptions.Asignacion, RulePreferences.AsignationTypeUser, RulePreferences.AsignationTypeGroup, RulePreferences.AsignationTypeManual, True)

        If dt.Rows.Count < 1 Then Exit Sub

        Dim id As Long = 0
        Dim Username As String = String.Empty

        Dim Selection As Integer = dt.Rows(0).Item("OBJECTID")
        Select Case Selection
            Case RulePreferences.AsignationTypeUser, RulePreferences.AsignationTypeGroup
                id = dt.Rows(0).Item("OBJVALUE")
                Username = UserGroupBusiness.GetUserorGroupNamebyId(id)
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

        For Each r As ITaskResult In newresults
            WFTaskBusiness.Asign(r, id, Membership.MembershipHelper.CurrentUser.ID, Username)
        Next
    End Sub

    Private Shared Function ReturnInstanceRule(ByVal dtSubRules As DataTable, ByVal dtParams As DataTable, ByVal r As DataRow, ByVal stepid As Int64, ByVal parentType As TypesofRules, ByVal ruleInstanceList As List(Of Int64), ByVal parentRule As WFRuleParent) As WFRuleParent
        Dim ru As WFRuleParent
        Dim paramItems As DataRow()

        Try
            paramItems = dtParams.Select("Rule_id=" & r.Item("Id").ToString, "Item")

            Dim Rule As WFRuleParent = Nothing
            InstanceRule(Rule, paramItems, r, stepid)

            If Not Rule Is Nothing Then
                ruleInstanceList.Add(Rule.ID)
                Rule.ParentRule = parentRule
                Rule.ParentType = parentType
                Rule.ID = Int64.Parse(r.Item("Id").ToString)
                Rule.Name = r.Item("Name").ToString
                Rule.Version = Int32.Parse(r.Item("Version").ToString)
                Rule.Enable = r.Item("Enable")

                FillRulePreference(Rule)

                If Rule.IsUI AndAlso Membership.MembershipHelper.ClientType = ClientType.Service Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se deshabilita la regla " + Rule.ID.ToString + " por tener UI y estar ejecutandose desde un servicio")
                    Rule.Enable = False
                End If

                For Each subRule As DataRow In dtSubRules.Select("ParentId = " & Rule.ID)
                    Try
                        If ruleInstanceList.Contains(subRule("ID")) = False Then
                            If Cache.Workflows.HSRules.Contains(subRule("ID")) Then
                                ru = Cache.Workflows.HSRules.GetByRuleID(subRule("ID"))
                                If IsNothing(ru) Then Throw New Exception("La regla hija es nothing con id " & subRule("Id") & " de la regla padre " & Rule.ID & " - " & Rule.Name)
                                Rule.ChildRules.Add(ru)
                            Else
                                ru = ReturnInstanceRule(dtSubRules, dtParams, subRule, stepid, TypesofRules.Regla, ruleInstanceList, Rule)
                                If IsNothing(ru) Then Throw New Exception("La regla hija es nothing con id " & subRule("Id") & " de la regla padre " & Rule.ID & " - " & Rule.Name)
                                Rule.ChildRules.Add(ru)
                                If Cache.Workflows.HSRules.Contains(ru.ID) = False Then
                                    Cache.Workflows.HSRules.Add(ru)
                                End If
                            End If
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "La regla " & subRule("ID") & " ya se encuentra en el workflow. Verificar en WFRules para el parentID " & Rule.ID)
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                    End Try
                Next

                If Rule.RuleClass = "DoExecuteRule" Then

                    Dim RuleToExecuteId As Int64 = DirectCast(Rule, IDOExecuteRule).IDRule

                    Try
                        If ruleInstanceList.Contains(RuleToExecuteId) = False Then
                            If Cache.Workflows.HSRules.Contains(RuleToExecuteId) Then
                                ru = Cache.Workflows.HSRules.GetByRuleID(RuleToExecuteId)
                                If IsNothing(ru) Then Throw New Exception("La regla hija es nothing con id " & RuleToExecuteId & " de la regla padre " & Rule.ID & " - " & Rule.Name)
                                Rule.ChildRules.Add(ru)
                            Else
                                ru = WFRulesBusiness.GetInstanceRuleById(RuleToExecuteId, True)
                                If IsNothing(ru) Then Throw New Exception("La regla hija es nothing con id " & RuleToExecuteId & " de la regla padre " & Rule.ID & " - " & Rule.Name)
                                Rule.ChildRules.Add(ru)
                                If Cache.Workflows.HSRules.Contains(ru.ID) = False Then
                                    Cache.Workflows.HSRules.Add(ru)
                                End If
                            End If
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "La regla " & RuleToExecuteId & " ya se encuentra en el workflow. Verificar en WFRules para el parentID " & Rule.ID)
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                    End Try
                End If


                If Cache.Workflows.HSRules.Contains(Rule.ID) = False Then
                    Cache.Workflows.HSRules.Add(Rule)
                End If
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "La regla " & r.Item("Id").ToString & " no pudo ser instanciada")
            End If

            Return Rule
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function



    Private Shared Sub InstanceRule(ByRef rule As WFRuleParent, ByVal paramItems As DataRow(), ByVal ruleRow As DataRow, ByVal stepId As Int64)
        InstanceRule(rule, paramItems, ruleRow.Item("Class"), ruleRow.Item("Id"), ruleRow.Item("Name"), ruleRow.Item("Type"), stepId)
    End Sub

    Public Shared Function GetRuleParamItems(ByVal p_iRuleID As Int64, ByVal withcache As Boolean) As DsRules
        Try
            Dim newDsrules As New DsRules
            If withcache = False Then Return WFRulesFactory.GetRuleParamItems(p_iRuleID)
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
    Public Shared Sub SetRuleEstado(ByVal p_iRuleId As Int64, ByVal p_bEstado As Boolean)
        WFRulesFactory.UpdateRuleById(p_iRuleId, p_bEstado)
    End Sub

    'Public Shared Sub SetRulesInstances(ByVal listrule As List(Of IWFRuleParent), ByRef s As WFStep)
    '    SyncLock (s)
    '        For Each rule As IWFRuleParent In listrule
    '            If rule.ParentRule Is Nothing Then
    '                Try
    '                    'WFRulesBusiness.GetRuleEstado(rule.ID, True)
    '                    Dim ru As WFRuleParent = ContainsRule(s, rule)
    '                    If IsNothing(ru) Then
    '                        s.Rules.Add(rule)
    '                    Else
    '                        ru = rule
    '                    End If
    '                Catch ex As Exception
    '                    ZClass.raiseerror(ex)
    '                End Try
    '            End If
    '        Next
    '    End SyncLock
    'End Sub
#End Region

#Region "Conectores"
    Public Function FillTransitions(ByVal wf As WorkFlow) As ArrayList
        Dim transitions As New ArrayList

        Try
            Dim toStepId As Int64
            Dim filter As String

            If DBTools.GetServerType.Contains("Oracle") Then
                filter = "RULE_ID={0} and ITEM=0"
            Else
                filter = "Rule_id={0} and Item=0"
            End If

            For Each s As WFStep In wf.Steps.Values
                For Each r As DataRow In s.DsRules.WFRules.Select("Class = 'DoDistribuir'")
                    toStepId = CLng(s.DsRules.WFRuleParamItems.Select(String.Format(filter, r("Id").ToString))(0)("Value"))

                    If toStepId > 0 Then
                        transitions.Add(New String() {r("step_Id"), toStepId, r("Id")})
                    End If
                Next
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return transitions
    End Function



    Shared Sub UpdateParentRuleId(ByVal ruleID As Int64, ByVal parentRuleID As Int64)
        WFRulesFactory.UpdateParentRuleId(ruleID, parentRuleID)
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
    Public Shared Sub FillRulePreference(ByRef instance As IWFRuleParent)
        Dim dt As DataTable = GetRuleOptionsDT(True, instance.WFStepId)

        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Select("ruleid=" & instance.ID)
                Dim pref As RulePreferences = dr("ObjectId")

                Select Case pref
                    Case RulePreferences.ConfigurationExecuteWhenResult
                        If dr.Item("Objvalue").ToString = "0" Then
                            instance.ExecuteWhenResult = False
                        Else
                            instance.ExecuteWhenResult = True
                        End If

                    Case RulePreferences.RefreshRule
                        If dr.Item("Objvalue").ToString = "0" Then
                            instance.RefreshRule = False
                        Else
                            instance.RefreshRule = True
                        End If

                    Case RulePreferences.AlertNotificationMode
                        If dr.Item("Objvalue") = 1 Then
                            'Tiene algun Checkbox de notificacion Checkeado
                            instance.AlertExecution = True
                        Else
                            instance.AlertExecution = False
                        End If

                    Case RulePreferences.ContinueWithError
                        If dr.Item("Objvalue").ToString = "0" Then
                            instance.ContinueWithError = False
                        Else
                            instance.ContinueWithError = True
                        End If

                    Case RulePreferences.CloseTask
                        If dr.Item("Objvalue").ToString = "0" Then
                            instance.CloseTask = False
                        Else
                            instance.CloseTask = True
                        End If

                    Case RulePreferences.RuleHelp
                        If Not IsDBNull(dr.Item("ObjExtraData")) Then
                            instance.Description = dr.Item("ObjExtraData")
                        Else
                            instance.Description = String.Empty
                        End If

                    Case RulePreferences.CleanRule
                        If dr.Item("Objvalue").ToString = "0" Then
                            instance.CleanRule = False
                        Else
                            instance.CleanRule = True
                        End If

                    Case RulePreferences.RuleCategory
                        instance.Category = Int16.Parse(dr.Item("Objvalue").ToString)

                    Case RulePreferences.SaveUpdate
                        If dr.Item("Objvalue").ToString = "0" Then
                            instance.SaveUpdate = False
                        Else
                            instance.SaveUpdate = True
                        End If

                    Case RulePreferences.Comment
                        If Not IsDBNull(dr.Item("ObjExtraData")) Then
                            instance.UpdateComment = dr.Item("ObjExtraData")
                        Else
                            instance.UpdateComment = String.Empty
                        End If

                    Case RulePreferences.SaveUpdateInHistory
                        If dr.Item("Objvalue").ToString = "0" Then
                            instance.SaveUpdateInHistory = False
                        Else
                            instance.SaveUpdateInHistory = True
                        End If

                    Case RulePreferences.Asynchronous
                        If dr.Item("Objvalue").ToString = "0" Then
                            instance.Asynchronous = False
                        Else
                            instance.Asynchronous = True
                        End If

                    Case RulePreferences.RuleIdToExecuteAfterError
                        If Not IsDBNull(dr.Item("Objvalue")) Then
                            instance.RuleIdToExecuteAfterError = Int32.Parse(dr.Item("Objvalue"))
                        Else
                            instance.RuleIdToExecuteAfterError = 0
                        End If

                    Case RulePreferences.MessageToShowInCaseOfError
                        If Not IsDBNull(dr.Item("ObjExtraData")) Then
                            instance.MessageToShowInCaseOfError = dr.Item("ObjExtraData")
                        Else
                            instance.MessageToShowInCaseOfError = String.Empty
                        End If

                    Case RulePreferences.ExecuteRuleInCaseOfError
                        If dr.Item("Objvalue").ToString = "0" Then
                            instance.ExecuteRuleInCaseOfError = False
                        Else
                            instance.ExecuteRuleInCaseOfError = True
                        End If

                    Case RulePreferences.ThrowExceptionIfCancel
                        If dr.Item("Objvalue").ToString = "0" Then
                            instance.ThrowExceptionIfCancel = False
                        Else
                            instance.ThrowExceptionIfCancel = True
                        End If

                    Case RulePreferences.DisableChildRules
                        If dr.Item("Objvalue").ToString = "0" Then
                            instance.DisableChildRules = False
                        Else
                            instance.DisableChildRules = True
                        End If
                End Select
            Next
            dt = Nothing
        Else
            Exit Sub
        End If
    End Sub
    Public Shared Function GetRulePreferencesByRuleId(ByVal ruleid As Int64, wfstepid As Int64, RulePreference As RulePreferences) As String

        Dim dt As DataTable = WFRulesBusiness.GetRuleOptionsDT(True, wfstepid)

        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            Dim dv As New DataView(dt)
            dv.RowFilter = "ruleid = " & ruleid & " and ObjectId = " & RulePreference

            dt = dv.ToTable()
            dv.Dispose()
            dv = Nothing

            For Each dr As DataRow In dt.Rows
                Dim pref As RulePreferences = dr("ObjectId")

                Select Case pref
                    Case RulePreferences.ConfigurationExecuteWhenResult
                        If dr.Item("Objvalue").ToString = "0" Then
                            Return False
                        Else
                            Return True
                        End If

                    Case RulePreferences.RefreshRule
                        If dr.Item("Objvalue").ToString = "0" Then
                            Return False
                        Else
                            Return True
                        End If

                    Case RulePreferences.AlertNotificationMode
                        If dr.Item("Objvalue") = 1 Then
                            'Tiene algun Checkbox de notificacion Checkeado
                            Return True
                        Else
                            Return False
                        End If

                    Case RulePreferences.ContinueWithError
                        If dr.Item("Objvalue").ToString = "0" Then
                            Return False
                        Else
                            Return True
                        End If

                    Case RulePreferences.CloseTask
                        If dr.Item("Objvalue").ToString = "0" Then
                            Return False
                        Else
                            Return True
                        End If

                    Case RulePreferences.RuleHelp
                        If Not IsDBNull(dr.Item("ObjExtraData")) Then
                            Return dr.Item("ObjExtraData")
                        Else
                            Return String.Empty
                        End If

                    Case RulePreferences.CleanRule
                        If dr.Item("Objvalue").ToString = "0" Then
                            Return False
                        Else
                            Return True
                        End If

                    Case RulePreferences.RuleCategory
                        Return Int16.Parse(dr.Item("Objvalue").ToString)

                    Case RulePreferences.SaveUpdate
                        If dr.Item("Objvalue").ToString = "0" Then
                            Return False
                        Else
                            Return True
                        End If

                    Case RulePreferences.Comment
                        If Not IsDBNull(dr.Item("ObjExtraData")) Then
                            Return dr.Item("ObjExtraData")
                        Else
                            Return String.Empty
                        End If

                    Case RulePreferences.SaveUpdateInHistory
                        If dr.Item("Objvalue").ToString = "0" Then
                            Return False
                        Else
                            Return True
                        End If

                    Case RulePreferences.Asynchronous
                        If dr.Item("Objvalue").ToString = "0" Then
                            Return False
                        Else
                            Return True
                        End If

                    Case RulePreferences.RuleIdToExecuteAfterError
                        If Not IsDBNull(dr.Item("Objvalue")) Then
                            Return Int32.Parse(dr.Item("Objvalue"))
                        Else
                            Return 0
                        End If

                    Case RulePreferences.MessageToShowInCaseOfError
                        If Not IsDBNull(dr.Item("ObjExtraData")) Then
                            Return dr.Item("ObjExtraData")
                        Else
                            Return String.Empty
                        End If

                    Case RulePreferences.ExecuteRuleInCaseOfError
                        If dr.Item("Objvalue").ToString = "0" Then
                            Return False
                        Else
                            Return True
                        End If

                    Case RulePreferences.ThrowExceptionIfCancel
                        If dr.Item("Objvalue").ToString = "0" Then
                            Return False
                        Else
                            Return True
                        End If

                    Case RulePreferences.DisableChildRules
                        If dr.Item("Objvalue").ToString = "0" Then
                            Return False
                        Else
                            Return True
                        End If
                End Select
            Next
        Else
            Exit Function
        End If
    End Function

#Region "UCZRuleValueFunction Members"
    Public Shared Sub ReconocerRuleValueFunctions(ByRef codedText As String)

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

    Private Shared Function GetZRuleValueFunctions() As List(Of String)
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
    Private Shared Function IsZRuleValueFunction(ByVal Word As String) As Boolean

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

#Region "RuleOpt"
    ''' <summary>
    ''' Copia todas las preferencias de una regla a otra
    ''' </summary>
    ''' <param name="originalRuleId">Id de Regla donde se obtendrán las preferencias</param>
    ''' <param name="copyRuleId">Id de Regla donde se copiaran las preferencias</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] 23/03/2011  Created
    ''' </history>
    Public Shared Sub CopyRulesPreferences(originalstepid As Int64, ByVal originalRuleId As Int64, ByVal copyRuleId As Int64, destinystepid As Int64)
        Dim preferences As DataTable = GetRuleOptionsDT(False, originalstepid)

        If preferences IsNot Nothing AndAlso preferences.Rows.Count > 0 Then
            Dim dv As New DataView(preferences)
            dv.RowFilter = "ruleid = " & originalRuleId
            preferences = dv.ToTable()
            dv.Dispose()
            dv = Nothing

            WFFactory.CopyRulesPreferences(preferences, copyRuleId)
        End If
        Cache.Workflows.HsRulesPreferencesByStepId.Remove(destinystepid)
    End Sub

#End Region

End Class
