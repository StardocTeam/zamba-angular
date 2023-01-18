Imports Zamba.Core.WF.WF
Imports Zamba.Data

Public Class WFBusiness
    'Inherits ZClass

    Private Shared Function Build(ByVal dr As DataRow) As IWorkFlow
        Dim CurrentWorkflow As IWorkFlow = Nothing

        If Not IsNothing(dr) Then
            Try
                CurrentWorkflow = New WorkFlow()

                If Not IsNothing(dr("Name")) AndAlso Not IsDBNull(dr("Name")) Then
                    CurrentWorkflow.Name = dr("Name").ToString()
                End If

                If Not IsNothing(dr("Help")) AndAlso Not IsDBNull(dr("Help")) Then
                    CurrentWorkflow.Help = dr("Help").ToString()
                End If

                If Not IsNothing(dr("Description")) AndAlso Not IsDBNull(dr("Description")) Then
                    CurrentWorkflow.Description = dr("Description").ToString()
                End If

                If Not IsNothing(dr("work_id")) Then
                    Dim tryValue As Int64

                    If Int64.TryParse(dr("work_id").ToString(), tryValue) AndAlso Not IsDBNull(dr("work_id")) Then
                        CurrentWorkflow.ID = tryValue
                    Else
                        CurrentWorkflow.ID = 0
                    End If
                End If

                If Not IsNothing(dr("Wstat_id")) AndAlso Not IsDBNull(dr("Wstat_id")) Then
                    Dim tryValue As Int32

                    If Int32.TryParse(dr("Wstat_id").ToString(), tryValue) Then
                        CurrentWorkflow.WFStat = DirectCast(tryValue, WFStats)
                    Else
                        CurrentWorkflow.WFStat = WFStats.Active
                    End If
                End If

                If Not IsNothing(dr("CreateDate")) AndAlso Not IsDBNull(dr("CreateDate")) Then
                    Dim tryValue As Date

                    If Date.TryParse(dr("CreateDate").ToString(), tryValue) Then
                        CurrentWorkflow.CreateDate = tryValue
                    End If
                End If

                If Not IsNothing(dr("EditDate")) AndAlso Not IsDBNull(dr("EditDate")) Then
                    Dim tryValue As Date

                    If Date.TryParse(dr("EditDate").ToString(), tryValue) Then
                        CurrentWorkflow.EditDate = tryValue
                    End If
                End If

                If Not IsNothing(dr("Refreshrate")) AndAlso Not IsDBNull(dr("Refreshrate")) Then
                    Dim tryValue As Int32

                    If Int32.TryParse(dr("Refreshrate").ToString(), tryValue) Then
                        CurrentWorkflow.RefreshRate = tryValue
                    End If
                End If

                If Not IsNothing(dr("InitialStepId")) AndAlso Not IsDBNull(dr("InitialStepId")) Then
                    Dim tryValue As Int32

                    If Int32.TryParse(dr("InitialStepId").ToString(), tryValue) Then
                        CurrentWorkflow.InitialStep = New WFStep(tryValue)
                    End If
                End If


                If Not IsNothing(dr("TaskCount")) AndAlso Not IsDBNull(dr("TaskCount")) Then
                    Dim tryValue As Int32

                    If Int32.TryParse(dr("TaskCount").ToString(), tryValue) Then
                        CurrentWorkflow.TasksCount = tryValue
                    Else
                        CurrentWorkflow.TasksCount = 0
                    End If
                End If


                If Not IsNothing(dr("ExpiredTasksCount")) AndAlso Not IsDBNull(dr("ExpiredTasksCount")) Then
                    Dim tryValue As Int32

                    If Int32.TryParse(dr("ExpiredTasksCount").ToString(), tryValue) Then
                        CurrentWorkflow.ExpiredTasksCount = tryValue
                    Else
                        CurrentWorkflow.ExpiredTasksCount = 0
                    End If
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If

        Return CurrentWorkflow
    End Function

#Region "Get"
    Public Shared Sub GetFullEditWF(ByVal WF As WorkFlow)

        'Steps
        WFStepBusiness.FillSteps(WF, False)

        'Rules
        'WFRulesBusiness.FillRules(WF, False)

    End Sub
    Public Shared Sub GetFullMonitorWF(ByVal WF As WorkFlow)

        'Steps
        WFStepBusiness.FillSteps(WF, False)

        'Rules
        'WFRulesBusiness.FillRules(WF, False)




    End Sub





    ''' <summary>
    ''' Devuelve los nombres y id's de los wf que puede ver el usuario + el nombre y id de estapas
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetUserWFIdsAndNamesWithSteps(ByVal userId As Int64) As System.Collections.Generic.List(Of Zamba.Core.EntityView)

        SyncLock (Cache.Workflows.hsUserWFIdsAndNamesWithSteps)
            If Cache.Workflows.hsUserWFIdsAndNamesWithSteps Is Nothing OrElse Cache.Workflows.hsUserWFIdsAndNamesWithSteps.Count = 0 Then

                Dim hs As New System.Collections.Generic.List(Of Zamba.Core.EntityView)

                Dim dt As DataTable

                dt = Zamba.Data.WFStepsFactory.GetWFAndStepIdsAndNamesAndTaskCount(userId)

                Dim WFID As Int64 = -1
                Dim WFCount As Int64

                Dim W As Zamba.Core.EntityView

                For Each dr As DataRow In dt.Rows

                    If (WFID <> Int64.Parse(dr("WfId"))) Then
                        WFID = Int64.Parse(dr("WfId"))
                        WFCount = 0

                        W = New Zamba.Core.EntityView(Int64.Parse(dr("WfId")), dr("WFName").ToString(), WFCount)
                        W.ChildsEntities = New System.Collections.Generic.List(Of EntityView)
                        W.ChildCount = WFCount
                        hs.Add(W)
                    End If

                    Dim Cantidad As Int64 = dr("Cantidad").ToString()

                    Dim s As New Zamba.Core.EntityView(Int64.Parse(dr("WFStepId")), dr("WFStepName").ToString(), Cantidad)
                    WFCount += Cantidad
                    W.ChildsEntities.Add(s)

                Next

                Cache.Workflows.hsUserWFIdsAndNamesWithSteps = hs
                Return hs

            Else
                Return Cache.Workflows.hsUserWFIdsAndNamesWithSteps
            End If
        End SyncLock
    End Function
    ''' <summary>
    ''' Devuelve el WFId de la entidad
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <returns>WFId</returns>
    ''' <history>
    '''        [Pablo]   22/10/2010  Created
    ''' </history>
    Public Shared Function GetWFAssociationByDocTypeId(ByVal docTypeId As Int64) As Int64
        Return WFFactory.GetWFAssociationByDocTypeId(docTypeId)
    End Function

    Public Shared Function GetWorkflowIdByStepId(ByVal stepId As Int64) As Int64
        Return WFFactory.GetWorkflowIdByStepId(stepId)
    End Function
    Public Shared Function GetWorkflowNameByWFId(ByVal workId As Int64) As String
        If Not Cache.Workflows.hsWorkflowNames.ContainsKey(workId) Then
            Cache.Workflows.hsWorkflowNames.Add(workId, WFFactory.GetWorkflowNameByWFId(workId))
        End If
        Return Cache.Workflows.hsWorkflowNames.Item(workId)
    End Function

    Public Shared Function GetWorkflowRelations() As DataTable
        Return WFFactory.GetWorkflowRelations()
    End Function


    Public Shared Function GetWorkflowByStepId(ByVal stepid As Int64) As IWorkFlow
        For Each w As IWorkFlow In Cache.Workflows.hsWorkflow.Values
            If w.Steps.ContainsKey(stepid) Then
                Return w
            End If
        Next
        Dim wfid As Int64
        wfid = WFFactory.GetWorkflowIdByStepId(stepid)
        Return WFBusiness.GetWFbyId(wfid)
    End Function

    Public Shared Function GetWorkFlow(ByVal wfID As Int64) As WorkFlow
        Try
            Dim tmpDS As DataSet = WFFactory.GetWfByIdAsDataSet(wfID)
            If Not IsNothing(tmpDS) AndAlso tmpDS.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In tmpDS.Tables(0).Rows
                    Return CreateWorkFlow(r)
                Next
            Else
                Return Nothing
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try

    End Function

    Public Shared Function CreateWorkFlow(ByVal r As DataRow) As WorkFlow

        Dim tmpWF As WorkFlow

        Dim tmpWFID As Int64 = Int64.Parse(r("work_id").ToString())
        Dim wfName As String = r("Name")
        Dim wfDescription As String = String.Empty
        If Not IsDBNull(r("Description")) Then wfDescription = r("Description")

        Dim wfHelp As String = String.Empty
        If Not IsDBNull(r("Help")) Then wfHelp = r("Help").ToString
        Dim wfState As WFStats
        If 0 = Int16.Parse(r("Wstat_id").ToString()) Then
            wfState = WFStats.Active
        Else
            wfState = WFStats.NonActive
        End If
        Dim wfCreateDate As Date = Date.Parse(r("CreateDate").ToString())
        Dim wfEditDate As Date = Date.Parse(r("EditDate").ToString())
        Dim wfRefreshRate As Int32 = Int32.Parse(r("Refreshrate").ToString())
        Dim wfInitialStepID As Int64 = Int64.Parse(r("InitialStepId").ToString())

        tmpWF = New WorkFlow(tmpWFID, wfName, wfDescription, wfHelp, wfState, wfCreateDate, wfEditDate, wfRefreshRate, wfInitialStepID)

        Return tmpWF

    End Function

    Public Shared Function GetWorkflows() As List(Of IWorkFlow)
        Dim WorkflowsList As New List(Of IWorkFlow)()

        Dim dsWorkflows As DataSet = WFFactory.GetWorkflows()

        If Not IsNothing(WorkflowsList) Then
            For Each dr As DataRow In dsWorkflows.Tables(0).Rows
                WorkflowsList.Add(WFBusiness.Build(dr))
            Next
        End If

        Return WorkflowsList
    End Function


    ''Devuelve la descripcion y la ayuda de cada regla en el assembly
    Public Shared Function GetRulesByReflection(ByVal Assembly As Type) As System.Collections.Generic.List(Of ArrayList)
        Dim list As New System.Collections.Generic.List(Of ArrayList)
        Try
            Dim a As Reflection.Assembly = System.Reflection.Assembly.GetAssembly(Assembly)
            For Each M As System.Type In a.GetTypes
                If M.Name.ToLower.StartsWith("if") OrElse M.Name.ToLower.StartsWith("do") Then
                    Try
                        Dim list2 As New ArrayList(3)
                        For Each o As Object In M.GetCustomAttributes(True)
                            If String.Compare(o.GetType().Name, "RuleDescription") = 0 Then
                                list2(0) = DirectCast(o, RuleDescription).Description
                            ElseIf String.Compare(o.GetType().Name, "RuleHelp") = 0 Then
                                list2(1) = DirectCast(o, RuleHelp).Help
                            End If
                        Next
                        list2(3) = M.Name
                        list.Add(list2)
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return list
    End Function
    Public Shared Function GetRulesByStep() As DsRulesByStep
        Return WFFactory.GetRulesByStep()
    End Function

    Public Shared Function GetStepsByWF() As DStepsByWorkflow
        Return WFFactory.GetStepsByWF()
    End Function
    Public Shared Function GetDelayedDocs() As DsDelayedDocs
        Return WFFactory.GetDelayed()
    End Function
    Public Shared Function GetExpiredDocs() As DsExpiredDocs
        Return WFFactory.GetExpired()
    End Function

    Public Shared Function GetDocsByUser() As DsDocsByUser
        Return WFFactory.AsignedDocsByUser()
    End Function
    Public Shared Function GetDocsByStep() As DsDocsByStep
        Return WFFactory.GetDocumentsByStep()
    End Function
    Public Shared Function GetDocsByWF() As DsDocsByWF
        Return WFFactory.GetDocsByWF()
    End Function

#End Region

#Region "Convert Object To Persist"
    Public Shared Function ConvertToPersist(ByVal o As Object) As String
        If IsNothing(o) Then Return ""
        Select Case Type.GetTypeCode(o.GetType)
            Case TypeCode.Object
                Return ""
            Case TypeCode.Boolean
                If CBool(o) = True Then
                    Return "1"
                Else
                    Return "0"
                End If
            Case TypeCode.DateTime
                If o Is Nothing Then
                    Return ""
                Else
                    Return o.ToString
                End If
            Case TypeCode.String
                Return o.ToString
            Case Else
                If o.GetType.IsEnum Then
                    Return CInt(o).ToString()
                Else
                    Return o.ToString
                End If
        End Select
    End Function
#End Region

#Region "Users & Groups"

    Public Shared Function GetAllUsers() As SortedList
        Return WFFactory.GetAllUsers
    End Function
    Public Shared Function GetAllGroups() As SortedList
        Return WFFactory.GetAllGroups
    End Function
#End Region

#Region "Refresh"
    'Public Shared Sub Refresh(ByVal WFs() As WorkFlow)
    '    WFTaskBusiness.Refresh(WFs)
    'End Sub
    'Public Shared Sub Refresh(ByVal WF As WorkFlow)
    '    WFTaskBusiness.Refresh(WF)
    'End Sub
#End Region

#Region "Remove"
    Public Shared Sub Remove(ByVal BaseNode As BaseWFNode)
        If BaseNode.GetType.Name.ToLower = "editstepnode" Then
            WFStepBusiness.Remove(BaseNode)
        Else
            WFRulesBusiness.Remove(BaseNode)
        End If
    End Sub
#End Region

#Region "Steps"
    Public Shared Sub SetInitialStep(ByVal StepNode As IEditStepNode)
        WFStepBusiness.SetInitial(StepNode)
    End Sub
    'Public Shared Sub FillUsersAndGroups(ByRef WfStep As WFStep)
    '    WFStepBusiness.FillUsersAndGroups(WfStep)
    'End Sub
#End Region

#Region "Rules"


    ''' <summary>
    ''' Obtiene el nombre de una acción de usuario donde se encuentra la regla principal
    ''' En caso de no tener nombre devuelve vacío
    ''' </summary>
    ''' <param name="rule">Regla padre</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas]     11/06/2009  Created
    '''     Marcelo     18/01/2010  Modified - Se modifica para que si no encuentra el nombre, 
    '''                                        le devuelva el nombre de la regla
    ''' </history>
    Public Function GetUserActionName(ByVal ruleid As Int64, wfstepid As Int64, rulename As String, ByVal WithCache As Boolean) As String
        Try
            Dim dt As DataTable = WFRulesBusiness.GetRuleOption(wfstepid, ruleid, 0, 43, 0, WithCache)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("ObjExtraData").ToString()
            Else
                Return rulename
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function





    Public Shared Sub RemoveTask(ByRef Result As ITaskResult, ByVal DeleteDocument As Boolean, ByVal CurrentUserId As Int64, ByVal DeleteFile As Boolean)
        WFTaskBusiness.Remove(Result, DeleteDocument, CurrentUserId, DeleteFile)
    End Sub
    Public Shared Sub GetWFs(ByRef Result As ITaskResult, ByVal DeleteDocument As Boolean, ByVal CurrentUserId As Int64, ByVal DeleteFile As Boolean)
        WFTaskBusiness.Remove(Result, DeleteDocument, CurrentUserId, DeleteFile)
    End Sub

    ''' <summary>
    ''' Método que sirve para guardar un elemento en la tabla ZRuleOpt o actualizarlo 
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="ruleSectionId"></param>
    ''' <param name="objId"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	27/05/2008	Created
    ''' </history>
    Public Shared Sub saveItemSelectedThatCanBe_StateOrUserOrGrupo(ByVal stepid As Int64, ByVal ruleId As Int64, ByVal ruleSectionId As RuleSectionOptions, ByVal objId As RulePreferences)
        Dim objIdActual As RulePreferences = recoverItemSelectedThatCanBe_StateOrUserOrGroup(stepid, ruleId, False)
        WFFactory.saveItemSelectedThatCanBe_StateOrUserOrGrupo(ruleId, ruleSectionId, objId, objIdActual)
    End Sub

    ''' <summary>
    ''' Método utilizado para saber que checkbox quedo seleccionado cuando el usuario presiono guardar
    ''' (Administrador -> Workflow -> Selección de una regla) recupera el checkbox que selecciono
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="forceReload">True para forzar la carga de datos en el hash</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	Aprox. 25/05/2008	Created
    '''     [Tomas]     25/06/2009      Modified    Se agrega la posibilidad de forzar la carga de datos al hash
    '''     [Marcelo]   06/05/2011      Modified    Se agrega Atributos y variables
    ''' </history>
    Public Shared Function recoverItemSelectedThatCanBe_StateOrUserOrGroup(ByVal stepid As Int64, ByVal ruleId As Int64, ByVal forceHashReload As Boolean) As RulePreferences
        Dim dv As DataView = Nothing
        Try

            Dim dt As DataTable = WFRulesBusiness.GetRuleOptionsDT(Not forceHashReload, stepid)

            dv = New DataView(dt)
            dv.RowFilter = "ruleid = " & ruleId.ToString & " And SectionId = " & RuleSectionOptions.Habilitacion & " And (ObjectId = " &
             RulePreferences.HabilitationSelectionState & " OR ObjectId = " & RulePreferences.HabilitationSelectionUser _
             & " OR ObjectId = " & RulePreferences.HabilitationSelectionBoth & " OR ObjectId = " & RulePreferences.HabilitationSelectionIndexAndVariable & ")"

            If dv.ToTable.Rows.Count > 0 Then
                Return dv.ToTable().Rows(0)("ObjectId")
            Else
                Return 0
            End If
        Finally
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Método que recupera los estados y usuarios deshabilitados, o bien, los estados y grupos deshabilitados
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="ruleSectionId"></param>
    ''' <param name="objId"></param>
    ''' <param name="stateId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	28/05/2008	Created
    ''' </history>
    Public Shared Function recoverUsers_Or_Groups_belongingToAState(ByVal ruleId As Int64, ByVal ruleSectionId As RuleSectionOptions, ByVal objId As RulePreferences, ByVal stateId As String) As DataSet
        Return WFFactory.recoverUsers_Or_Groups_belongingToAState(ruleId, ruleSectionId, objId, stateId)
    End Function

    ''' <summary>
    ''' Devuelve los items deshabilitados para la regla cuando esta seleccionado el conjunto de habilitacion
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>Marcelo Created 24/05/2011</history>
    Public Shared Function recoverDisableItemsBoth(ByVal ruleId As Int64) As DataSet
        Return WFFactory.recoverDisableItemsBoth(ruleId)
    End Function

    ''' <summary>
    ''' Borra los items deshabilitados para la regla cuando esta seleccionado el conjunto de habilitacion
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>Marcelo Created 24/05/2011</history>
    Public Shared Sub deleteDisableItemsBoth(ByVal ruleId As Int64)
        WFFactory.deleteDisableItemsBoth(ruleId)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ruleId">Id de la regla</param>
    ''' <param name="sectionId">Id de la seccion</param>
    ''' <param name="item1"></param>
    ''' <param name="item2"></param>
    ''' <param name="item3"></param>
    ''' <returns></returns>
    ''' <history>   Marcelo modified    22/06/09    
    ''' Se modifico para que no vaya mas a la base de datos</history>
    ''' <remarks></remarks>
    Public Shared Function recoverItemsSelected(stepid As Int64, ByVal ruleId As Int64, ByVal sectionId As Integer, ByVal item1 As RulePreferences, ByVal item2 As RulePreferences, ByVal item3 As RulePreferences, ByVal useCache As Boolean) As DataTable
        Dim dv As DataView = Nothing
        Try
            Dim dt As DataTable = WFRulesBusiness.GetRuleOptionsDT(useCache, stepid)

            dv = New DataView(dt)
            dv.RowFilter = "ruleid = " & ruleId & "and SectionId = " & sectionId & " And (ObjectId = " &
             item1 & " OR ObjectId = " & item2 _
             & " OR ObjectId = " & item3 & ")"

            Return dv.ToTable()

        Finally
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Setea las preferencias para las reglas de wf en tabla ZRuleOpt
    ''' </summary>
    ''' <param name="ruleid"></param>
    ''' <param name="item"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <history>   Marcelo 02/09/2009  Modified</history>
    ''' <remarks></remarks>
    Public Shared Sub SetRulesPreferences(ByVal ruleid As Int64, ByVal ruleSectionId As RuleSectionOptions, ByVal objId As RulePreferences, ByVal objValue As Int32, Optional ByVal ObjOperator As String = "")
        WFFactory.SetRulesPreferences(ruleid, ruleSectionId, objId, objValue, ObjOperator)
        'Limpio la cache
        Cache.Workflows.HsRulesPreferencesByStepId.Clear()
    End Sub

    ''' <summary>
    ''' Inserta (solamente) preferencias de reglas
    ''' </summary>
    ''' <param name="ruleid"></param>
    ''' <param name="ruleSectionId"></param>
    ''' <param name="objId"></param>
    ''' <param name="objValue"></param>
    ''' <param name="ObjOperator"></param>
    ''' <remarks></remarks>
    Public Shared Sub InsertRulesPreferences(ByVal ruleid As Int64, ByVal ruleSectionId As RuleSectionOptions, ByVal objId As RulePreferences, ByVal objValue As Int32, Optional ByVal ObjOperator As String = "")
        WFFactory.InsertRulesPreferences(ruleid, ruleSectionId, objId, objValue, ObjOperator)
    End Sub


    Public Shared Function DeleteRulesPreferencesSinObjectId(ByVal ruleid As Int64, ByVal RuleSectionid As Int32)
        WFFactory.DeleteRulesPreferencesSinObjectId(ruleid, RuleSectionid)
    End Function

    Public Shared Sub SetRulesPreferencesSinObjectId(ByVal ruleid As Int64, ByVal ruleSectionId As RuleSectionOptions, ByVal objId As RulePreferences, ByVal objValue As Int32, Optional ByVal ObjOperator As String = "")
        WFFactory.SetRulesPreferencesSinObjectId(ruleid, ruleSectionId, objId, objValue, ObjOperator)
        'Limpio la cache
        Cache.Workflows.HsRulesPreferencesByStepId.Clear()
    End Sub

    ''' <summary>
    ''' Método que sirve para guardar en la base de datos los elementos deshabilitados (estados, usuarios o grupos) (Solapa Habilitación)
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="ruleSectionId"></param>
    ''' <param name="objId"></param>
    ''' <param name="idCollectionDisabled"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	27/05/2008	Created
    ''' </history>
    Public Shared Sub SetRulesPreferencesForStatesUsersOrGroups(stepid As Int64, ByVal ruleId As Int64, ByVal ruleSectionId As RuleSectionOptions, ByVal objId As RulePreferences, ByRef idCollectionDisabled As Generic.List(Of Int64))
        WFFactory.setRulesPreferencesForStatesUsersOrGroups(ruleId, ruleSectionId, objId, idCollectionDisabled)
        'Limpio la cache
        Cache.Workflows.HsRulesPreferencesByStepId.Remove(stepid)
    End Sub

    ''' <summary>
    ''' Método que sirve para guardar en la base de datos los estados y usuarios o los estados y grupos (Solapa Habilitación)
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="ruleSectionId"></param>
    ''' <param name="objId"></param>
    ''' <param name="stateId"></param>
    ''' <param name="idCollectionDisabled"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	27/05/2008	Created
    ''' </history>
    Public Shared Sub SetRulesPreferencesFor_StatesAndUsers_Or_StatesAndGroups(stepid As Int64, ByVal ruleId As Int64, ByVal ruleSectionId As RuleSectionOptions, ByVal objId As RulePreferences, ByVal stateId As Integer, ByRef idCollectionDisabled As Generic.List(Of Int64))
        WFFactory.setRulesPreferencesFor_StatesAndUsers_Or_StatesAndGroups(ruleId, ruleSectionId, objId, stateId, idCollectionDisabled)
        'Limpio la cache
        Cache.Workflows.HsRulesPreferencesByStepId.Remove(stepid)
    End Sub

    ''' <summary>
    ''' Método que sirve para eliminar los antiguos elementos deshabilitados (estados, usuarios o grupos) (estados y usuarios o estados y grupos)
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="ruleSectionId"></param>
    ''' <param name="objId"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	27/05/2008	Created
    ''' </history>
    Public Shared Sub removeOldItemsThatWereDisabled(stepid As Int64, ByVal ruleId As Int64, ByVal ruleSectionId As RuleSectionOptions, ByVal objId As RulePreferences)
        WFFactory.removeOldItemsThatWereDisabled(ruleId, ruleSectionId, objId)
        'Limpio la cache
        Cache.Workflows.HsRulesPreferencesByStepId.Remove(stepid)
    End Sub




    ''' <summary>
    ''' Método que sirve para obtener el estado de habilitación del estado, es decir si está habilitado o deshabilitado
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="stateId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    23/04/2008  Created
    ''' </history>
    Public Shared Function GetStateOfHabilitationOfState(ByVal ruleId As Int64, ByVal stateId As Int64) As Boolean
        Return WFFactory.GetStateOfHabilitationOfState(ruleId, stateId)
    End Function

    Public Shared Function CanExecuteRulesInStep(ByVal stepid As Int64, ByVal userid As Int64) As Boolean
        If Zamba.Core.Cache.Workflows.hsCanExecuteRules.ContainsKey(stepid & "-" & userid) Then
            Return Zamba.Core.Cache.Workflows.hsCanExecuteRules(stepid & "-" & userid)
        Else
            Dim CanExecute As Boolean = WFFactory.CanExecuteRulesInStep(stepid, userid)
            If Zamba.Core.Cache.Workflows.hsCanExecuteRules.ContainsKey(stepid & "-" & userid) = False Then
                Zamba.Core.Cache.Workflows.hsCanExecuteRules.Add(stepid & "-" & userid, CanExecute)
            Else
                Zamba.Core.Cache.Workflows.hsCanExecuteRules(stepid & "-" & userid) = CanExecute

            End If
            Return CanExecute
        End If
    End Function

#End Region

#Region "Tasks"


    Public Shared Sub ChangeExpireDateTask(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int64, ByVal docTypeName As String, ByVal stepId As Int32, ByVal stepName As String, ByVal stateId As Int32, ByVal userName As String, ByVal workflowId As Int32, ByVal workflowName As String)

        'LOG sp_GetLogInformation
    End Sub




#End Region

    Public Shared Function FillTransitions(ByVal Wf As WorkFlow, ByVal ArrayPares As ArrayList) As ArrayList
        Try
            ArrayPares.Clear()
            For Each s As WFStep In Wf.Steps.Values
                For Each R As DsRules.WFRulesRow In s.DsRules.WFRules.Rows
                    Dim rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(R.Id, s.ID, True)
                    FindRuleDerivates(ArrayPares, rule)
                Next
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return ArrayPares
    End Function

    Private Shared Function FindRuleDerivates(ByVal ArrayPares As ArrayList, ByRef rule As WFRuleParent) As ArrayList
        If rule.RuleClass.ToLower = "dodistribuir" Then
            Dim DoDistribuir As IDoDistribuir = rule
            Dim Vector(1) As String
            Vector(0) = DoDistribuir.WFStepId.ToString()
            If DoDistribuir.NewWFStepId > 0 Then
                Vector(1) = DoDistribuir.NewWFStepId.ToString()
                ArrayPares.Add(Vector)
            End If
        End If
        For Each R As WFRuleParent In rule.ChildRules
            ArrayPares = FindRuleDerivates(ArrayPares, R)
        Next
        Return ArrayPares
    End Function

#Region "Get"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que retorna un conjunto de Objetos Workflows
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetWFs() As WorkFlow()
        Return WFFactory.GetWFs()
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que devuelve todos los Workflows para los cuales el usuario actual puede agregar documentos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetWFsToAddDocuments() As ArrayList
        Return WFFactory.GetWFsToAddDocuments(Membership.MembershipHelper.CurrentUser)
    End Function

    Public Shared Function GetWFsByUserRights(ByVal UserID As Int64) As List(Of WorkflowAdminDto)
        Dim grpids As New Generic.List(Of Int64)
        grpids = UserBusiness.GetUserGroupsIdsByUserid(UserID)
        If Not IsNothing(grpids) Then
            Dim WorkflowsToAdmin As New List(Of WorkflowAdminDto)
            Dim dstemp As DataSet
            dstemp = WFFactory.GetWFsByUserRights(grpids, RightsType.Edit)
            If dstemp IsNot Nothing Then
                For Each r As DataRow In dstemp.Tables(0).Rows
                    WorkflowsToAdmin.Add(New WorkflowAdminDto(Integer.Parse(r("Work_ID").ToString()), r("Name").ToString(), RightsType.Edit))
                Next
            End If
            dstemp = WFFactory.GetWFsByUserRights(grpids, RightsType.View)
            If dstemp IsNot Nothing Then
                For Each r As DataRow In dstemp.Tables(0).Rows
                    WorkflowsToAdmin.Add(New WorkflowAdminDto(Integer.Parse(r("Work_ID").ToString()), r("Name").ToString(), RightsType.View))
                Next
            End If
            Return WorkflowsToAdmin
        Else
            Return Nothing
        End If
    End Function
    'Public Shared Function GetWFsByUserRightsVIEW(ByVal UserID As Int64) As List(Of WorkflowAdminDto)
    '    Dim grpids As New Generic.List(Of Int64)
    '    grpids = UserBusiness.GetUserGroupsIdsByUserid(UserID)
    '    If Not IsNothing(grpids) Then
    '        Return WFFactory.GetWFsByUserRights(grpids, RightsType.View)
    '    Else
    '        Return Nothing
    '    End If
    'End Function
    '''-----------------------------------------------------------------------------
    ''' <summary>
    ''' todos los Workflows Que el usuario tenga asignado el permiso de MONITOREAR
    ''' </summary>
    ''' <param name="UserID">Id de Usuario</param>
    ''' <returns>Dataset DsWF</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	27/11/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Public Shared Function GetWFsByUserRightMONITORING(ByVal UserID As Int64) As List(Of WorkflowAdminDto)
    '    Dim grpids As New Generic.List(Of Int64)
    '    grpids = UserBusiness.GetUserGroupsIdsByUserid(UserID)
    '    If Not IsNothing(grpids) Then
    '        Return WFFactory.GetWFsByUserRightMONITORING(grpids)
    '    End If
    'End Function
#End Region

    Public Shared Sub Fill(ByRef instance As IWorkFlow)
        If IsNothing(instance.Steps) Then
            instance.Steps = New Dictionary(Of Int64, IWFStep)()

            For Each CurrentStep As IWFStep In WFStepBusiness.GetStepsByWorkflow(instance.ID)
                instance.Steps.Add(CurrentStep.ID, CurrentStep)
            Next
        End If

        If IsNothing(instance.InitialStep) Then
            For Each CurrentStep As IWFStep In instance.Steps.Values
                If CurrentStep.InitialState.Initial Or instance.InitialStepIdTEMP = CurrentStep.ID Then
                    instance.InitialStep = CurrentStep
                    instance.InitialStepIdTEMP = CurrentStep.ID
                End If

                If instance.InitialStepIdTEMP = CurrentStep.ID Then

                End If
            Next
        End If
        If IsNothing(instance.CreateDate) Then

        End If


    End Sub

#Region "ABM"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo que modifica el nombre a un Workflow en base a su ID
    ''' </summary>
    ''' <param name="Name">Nuevo Nombre</param>
    ''' <param name="WfId">ID de WorkFlow</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SaveNewName(ByVal Name As String, ByVal WFId As Int32)
        WFFactory.SaveNewName(Name, WFId)
    End Sub


    ''' <summary>
    ''' metodo el cual modifica la descripcion y ayuda del WF
    ''' </summary>
    ''' <param name="WFID"></param>
    ''' <param name="description"></param>
    ''' <param name="help"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[alan]	10/02/2010	Created
    ''' </history>
    Public Shared Sub SetWFDescriptionAndHelp(ByVal WFID As Int64, ByVal description As String, ByVal help As String)
        WFFactory.SetWFDescriptionAndHelp(WFID, description, help)
    End Sub

    Public Shared Sub UpdateWfCounts()
        Try
            WFFactory.UpdateWfCounts()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Public Shared Sub InsertWfCounts()
        Try
            WFFactory.InsertWfCounts()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub SaveWfInterval(ByVal Wfid As Int32, ByVal interval As Int32)
        WFFactory.SaveWfInterval(Wfid, interval)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para establecer la etapa inicial a un Workflow
    ''' </summary>
    ''' <param name="Wfid"></param>
    ''' <param name="InitialStepId"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SaveWfInitialStep(ByVal Wfid As Int32, ByVal InitialStepId As Int32)
        WFFactory.SaveWfInitialStep(Wfid, InitialStepId)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para eliminar un Workflow
    ''' </summary>
    ''' <param name="wfid">Id del workflow que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub RemoveWorkFlow(ByVal wfid As Int32)
        WFFactory.RemoveWorkFlow(wfid)
    End Sub
#End Region

#Region "IncomingDocument"


    'Inserta un nuevo documento esperando por otro.
    Public Shared Function InsertWaitDoc(ByRef _rule As IDoWaitForDocument) As Boolean

        Try
            Dim _waitID As Int32 = CoreData.GetNewID(IdTypes.Wait)

            ZWFIInsert(_rule, _waitID)
            ZWFIIInsert(_rule, _waitID)

            Return True

        Catch ex As Exception

            Zamba.Core.ZClass.raiseerror(ex)

        End Try

        Return False

    End Function

    'Inserta un nuevo registro en la tabla ZWFI
    Private Shared Sub ZWFIInsert(ByRef _rule As IDoWaitForDocument, ByVal _waitID As Int32)

        Try
            WFFactory.ZWFIInsert(_waitID, _rule.DocType, _rule.RuleID)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub
    'Inserta nuevos registros en la tabla ZWFII
    Private Shared Sub ZWFIIInsert(ByRef _rule As IDoWaitForDocument, ByVal _waitID As Int32)

        Try
            Dim mander As Char = Char.Parse("|")

            If Not IsNothing(_rule.IndiceID) AndAlso Not String.IsNullOrEmpty(_rule.IndiceID) Then

                Dim _indexArray As String() = _rule.IndiceID.Split(mander)
                Dim _ivalueArray As String() = _rule.IValue.Split(mander)

                For i As Int32 = 0 To _indexArray.Length - 1

                    Dim indice As Int32 = Int32.Parse(_indexArray(i).Trim)

                    WFFactory.ZWFIIInsert(_waitID, indice, _ivalueArray(i).Trim)

                Next

            End If


        Catch ex As Exception

            Zamba.Core.ZClass.raiseerror(ex)

        End Try

    End Sub


#End Region

#Region "Validate"
    ''' <summary>
    ''' Valida la Existencia de un documento en un workflow ( TRUE = Existe )
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> Diego </history>
    Public Shared Function ValidateDocIdInWF(ByVal DocId As Int64, ByVal wfid As Int64) As Boolean
        Dim Docscount As New Int32
        Docscount = WFFactory.ValidateDocIdInWF(DocId, wfid)
        If Docscount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    '''' <summary>
    '''' Valida la Existencia de un documento en un workflow ( TRUE = Existe )
    '''' </summary>
    '''' <remarks></remarks>
    '''' <history> Diego </history>
    'Public Shared Function ValidateDocIdInWF(ByVal DocId As Int64, ByVal wfid As Int64, ByVal tr As Transaction) As Boolean
    '    Dim Docscount As New Int32
    '    Docscount = WFFactory.ValidateDocIdInWF(DocId, wfid, tr)
    '    If Docscount > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function
#End Region

    'Todo Andres. Implemente este dispose, hay que ver que va adentro
    'Public Overrides Sub Dispose()

    'End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que devuelve todos los Workflows para los cuales el usuario actual puede agregar documentos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetWFbyId(ByVal wfid As Int64, Optional ByVal bolReload As Boolean = False) As WorkFlow
        SyncLock (Cache.Workflows.hsWorkflow)
            If Cache.Workflows.hsWorkflow.Contains(wfid) = False Or bolReload = True Then
                Try
                    Dim wf As WorkFlow = WFFactory.GetWFToAddDocuments(wfid)

                    'TODO: VALIDO QUE NO ESTE EN CERO EL INICIALSTEPID, PUEDE ESTAR EN CERO? O HAY QUE ASIGNARLE UN INICIALSTEPID SIEMPRE?
                    If wf.InitialStepIdTEMP <> 0 Then
                        wf.InitialStep = WFStepBusiness.GetStepById(wf.InitialStepIdTEMP, bolReload)
                    End If

                    Dim ds As DataSet = WFStepsFactory.GetStepsByWorkflow(wfid)

                    If (Not IsNothing(ds) And ds.Tables.Count > 0) Then
                        For Each CurrentRow As DataRow In ds.Tables(0).Rows
                            wf.Steps.Add(Int64.Parse(CurrentRow("Step_Id").ToString()), WFStepBusiness.GetStepById(CurrentRow("Step_Id")))
                        Next
                    End If

                    If Cache.Workflows.hsWorkflow.Contains(wfid) Then
                        Cache.Workflows.hsWorkflow(wfid) = wf
                    Else
                        Cache.Workflows.hsWorkflow.Add(wfid, wf)
                    End If

                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If

            Return Cache.Workflows.hsWorkflow(wfid)
        End SyncLock
    End Function
    Public Shared Function GetWFbyIdFilteredbyStepId(ByVal wfid As Int64, ByVal StepId As Generic.List(Of Zamba.Core.EntityView)) As WorkFlow
        Dim wf As WorkFlow = WFFactory.GetWFToAddDocuments(wfid)

        Try
            'TODO: VALIDO QUE NO ESTE EN CERO EL INICIALSTEPID, PUEDE ESTAR EN CERO? O HAY QUE ASIGNARLE UN INICIALSTEPID SIEMPRE?
            If wf.InitialStepIdTEMP <> 0 Then
                wf.InitialStep = WFStepBusiness.GetStepById(wf.InitialStepIdTEMP, True)
            End If

            If (Not IsNothing(StepId) And StepId.Count > 0) Then
                For Each E As EntityView In StepId
                    Dim etapa As WFStep = WFStepBusiness.GetStepById(E.ID, True)

                    wf.Steps.Add(Int64.Parse(E.ID.ToString()), WFStepBusiness.GetStepById(E.ID))
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return wf
    End Function

    '''' <summary>
    '''' Método que sirve para obtener la etapa inicial de un determinado workflow
    '''' </summary>
    '''' <param name="wfId"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    '''' <history>
    '''' 	[Gaston]	05/08/2008	Created
    '''' </history>
    'Public Shared Function GetInitialStepOfWF(ByVal wfId As Int64, ByVal t As Transaction) As Int64
    '    SyncLock (Cache.Workflows.HsInitialStepOfWFs)
    '        If Cache.Workflows.HsInitialStepOfWFs.Contains(wfId) = False Then
    '            Cache.Workflows.HsInitialStepOfWFs.Add(wfId, WFFactory.GetInitialStepOfWF(wfId, t))
    '            Return Cache.Workflows.HsInitialStepOfWFs(wfId)
    '        Else
    '            Return WFFactory.GetInitialStepOfWF(wfId, t)
    '        End If
    '    End SyncLock

    'End Function

    ''' <summary>
    ''' Método que sirve para obtener la etapa inicial de un determinado workflow
    ''' </summary>
    ''' <param name="wfId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	05/08/2008	Created
    ''' </history>
    Public Shared Function GetInitialStepOfWF(ByVal wfId As Int64) As Int64
        SyncLock (Cache.Workflows.HsInitialStepOfWFs)
            If Cache.Workflows.HsInitialStepOfWFs.Contains(wfId) = False Then
                Cache.Workflows.HsInitialStepOfWFs.Add(wfId, WFFactory.GetInitialStepOfWF(wfId))
                Return Cache.Workflows.HsInitialStepOfWFs(wfId)
            Else
                Return WFFactory.GetInitialStepOfWF(wfId)
            End If
        End SyncLock
    End Function

    ''' <summary>
    ''' Devuelve los id's de los doctypes asociados a wf que puede ver el usuario 
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	13/10/2010	Created
    ''' </history>
    Public Shared Function GetStepsByUserRestrictedDoctypes(ByVal userId As Int64) As ArrayList
        Dim Steps As ArrayList = New ArrayList()

        Dim ds As DataSet

        SyncLock (Cache.Workflows.hsStepsByUserRestrictedDoctypes)
            If Cache.Workflows.hsStepsByUserRestrictedDoctypes.Contains(userId) = False Then
                Cache.Workflows.hsStepsByUserRestrictedDoctypes.Add(userId, WFFactory.GetStepsByUserRestrictedDoctypes(userId))
            End If

            ds = Cache.Workflows.hsStepsByUserRestrictedDoctypes(userId)
        End SyncLock


        If Not IsNothing(ds) Then
            If ds.Tables.Count > 0 Then
                For Each dr As DataRow In ds.Tables(0).Rows
                    Dim Id As Int64 = dr(0)
                    Steps.Add(Id)
                Next
            End If
        End If

        Return Steps
    End Function

    Public Sub New()

    End Sub


    ''' <summary>
    ''' Trae todas las etapas de un workflow de las que el usuario
    ''' tiene permiso de ver junto con todas las reglas de esa etapa
    ''' con parentid = 0 que sean acciones de usuario y reglas generales
    ''' y si el usuario la tiene habilitada o no.
    ''' </summary>
    ''' <param name="groupId"></param>
    ''' <param name="workflowId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     Tomas   11/02/2011  Created
    ''' </history>
    Public Shared Function GetUserHabilitatedRules(ByVal groupId As Int64, ByVal workflowId As Int64) As DataTable
        Return WFFactory.GetUserHabilitatedRules(groupId, workflowId)
    End Function
End Class
