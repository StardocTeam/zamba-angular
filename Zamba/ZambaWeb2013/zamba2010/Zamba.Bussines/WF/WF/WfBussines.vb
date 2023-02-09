Imports Zamba.Core.WF.WF
Imports Zamba.Core.Enumerators
Imports Zamba.Data
Imports System.Collections.Generic
Imports Zamba.Servers
Imports Zamba.Membership

Public Class WFBusiness
    'Inherits ZClass

    Dim WFF As New WFFactory
    Private Function Build(ByVal dr As DataRow) As IWorkFlow
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
    ''' <summary>
    ''' Devuelve los nombres y id's de los wf que puede ver el usuario + el nombre y id de estapas
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserWFIdsAndNamesWithSteps(ByVal userId As Int64) As System.Collections.Generic.List(Of Zamba.Core.EntityView)
        Dim hs As New System.Collections.Generic.List(Of Zamba.Core.EntityView)
        Dim dt As DataTable

        dt = Zamba.Data.WFStepsFactory.GetWFAndStepIdsAndNamesAndTask(userId)

        If dt.Rows.Count > 0 Then
            Dim WFID As Int64 = -1
            'Dim WFCount As Int64

            Dim W As Zamba.Core.EntityView
            'Dim DV As New DataView

            'DV.Table = dt
            'DV.Sort = "WfName,WFStepName"

            'Se cambia foreach por for y se instancian temporales
            'Dim tempTable As DataTable = DV.ToTable
            Dim rowCount As Long = dt.Rows.Count
            Dim dr As DataRow
            Dim RiB As New RightsBusiness
            'For Each dr As DataRow In DV.ToTable.Rows
            For i As Long = 0 To rowCount - 1
                'Oculta la visualización de la etapa
                dr = dt.Rows(i)

                If Not RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, Zamba.Core.RightsType.HideStepFromWfTree, Int64.Parse(dr("WFStepId"))) Then
                    If (WFID <> Int64.Parse(dr("WfId"))) Then
                        WFID = Int64.Parse(dr("WfId"))
                        'WFCount = 0

                        W = New Zamba.Core.EntityView(Int64.Parse(dr("WfId")), dr("WFName").ToString(), 0)
                        W.ChildsEntities = New System.Collections.Generic.List(Of EntityView)
                        'W.ChildCount = WFCount
                        hs.Add(W)
                    End If

                    'Dim Cantidad As Int64 = dr("Cantidad").ToString()
                    Dim s As New Zamba.Core.EntityView(Int64.Parse(dr("WFStepId")), dr("WFStepName").ToString(), 0)
                    'WFCount += Cantidad
                    W.ChildsEntities.Add(s)
                End If
            Next
            RiB = Nothing
        End If

        Return hs
    End Function




    ''' <summary>
    ''' Devuelve el WFId del entidad
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <returns>WFId</returns>
    ''' <history>
    '''        [Pablo]   22/10/2010  Created
    ''' </history>
    Public Function GetWFAssociationByDocTypeId(ByVal docTypeId As Int64) As Int64
        Return WFF.GetWFAssociationByDocTypeId(docTypeId)
    End Function

    Public Function GetAllWorkflows() As DsWF
        Return WFF.GetWFs(0)
    End Function
    Public Function GetWf(ByVal r As DsWF.WFRow) As WorkFlow
        Return WFF.GetWf(r)
    End Function
    Public Function GetWorkflowIdByStepId(ByVal stepId As Int64) As Int64
        Dim wf As IWorkFlow
        wf = GetWorkflowByStepId(stepId)
        Return wf.ID
    End Function
    Public Function GetWorkflowNameByWFId(ByVal workId As Int64) As String
        If workId > 0 Then
            Dim WF As New WFFactory
            Dim name As String = WF.GetWorkflowNameByWFId(workId)
            Return name
        Else
            Return String.Empty
        End If

    End Function
    ''' <summary>
    ''' Obtiene el nombre de una etapa apartir de su id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Ezequiel] 04/03/09 Created
    ''' </history>

    Public Function GetWFsByDocType() As DsWFsByDocType
        Return WFF.GetWFsByDocType()
    End Function
    Public Function GetWFStepsDetails() As DsWFStepDetails
        Return WFF.GetWFStepsDetails()
    End Function


    Public Function GetWorkflowByStepId(ByVal stepid As Int64) As IWorkFlow
        For Each w As IWorkFlow In Cache.Workflows.hsWorkflow.Values
            If w.Steps.ContainsKey(stepid) Then
                Return w
            End If
        Next
        Dim wfid As Int64
        wfid = WFF.GetWorkflowIdByStepId(stepid)
        Return GetWFbyId(wfid)
    End Function

    Public Function GetWorkFlow(ByVal wfID As Int64) As WorkFlow
        Try
            Dim tmpDS As DataSet = WFF.GetWfByIdAsDataSet(wfID)
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

    Public Function CreateWorkFlow(ByVal r As DataRow) As WorkFlow

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


    Public Function GetWorkflows() As List(Of IWorkFlow)
        Dim WorkflowsList As New List(Of IWorkFlow)()

        Dim dsWorkflows As DataSet = WFF.GetWorkflows()

        If Not IsNothing(WorkflowsList) Then
            For Each dr As DataRow In dsWorkflows.Tables(0).Rows
                WorkflowsList.Add(Build(dr))
            Next
        End If

        Return WorkflowsList
    End Function


    ''Devuelve la descripcion y la ayuda de cada regla en el assembly
    Public Function GetRulesByReflection(ByVal Assembly As Type) As System.Collections.Generic.List(Of ArrayList)
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
    Public Function GetRulesByStep() As DsRulesByStep
        Return WFF.GetRulesByStep()
    End Function

    Public Function GetStepsByWF() As DStepsByWorkflow
        Return WFF.GetStepsByWF()
    End Function
    Public Function GetDelayedDocs() As DsDelayedDocs
        Return WFF.GetDelayed()
    End Function
    Public Function GetExpiredDocs() As DsExpiredDocs
        Return WFF.GetExpired()
    End Function

    Public Function GetDocsByUser() As DsDocsByUser
        Return WFF.AsignedDocsByUser()
    End Function
    Public Function GetDocsByStep() As DsDocsByStep
        Return WFF.GetDocumentsByStep()
    End Function
    Public Function GetDocsByWF() As DsDocsByWF
        Return WFF.GetDocsByWF()
    End Function

#End Region

#Region "Convert Object To Persist"
    Public Function ConvertToPersist(ByVal o As Object) As String
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

    Public Function GetAllUsers() As SortedList
        Return WFF.GetAllUsers
    End Function
    Public Shared Function GeUsersOnlUsersByUserIdAndRightsType(ByVal stepId As Int64) As List(Of BaseImageFileResult)
        If Not Cache.UsersAndGroups.hsUsersInStep.Contains(stepId) Then
            Dim dsFull As DataTable = WFFactory.GeUsersOnlUsersByUserIdAndRightsType(stepId)
            Dim sort As List(Of BaseImageFileResult) = New List(Of BaseImageFileResult)()
            If Not IsNothing(dsFull) Then
                If dsFull.Rows.Count > 0 Then
                    For Each row As DataRow In dsFull.Rows
                        sort.Add(New BaseImageFileResult(Int64.Parse(row("ID").ToString()), row("NAME").ToString()))
                    Next
                End If
            End If
            SyncLock Cache.UsersAndGroups.hsUsersInStep.SyncRoot
                Cache.UsersAndGroups.hsUsersInStep.Add(stepId, sort)
            End SyncLock
        End If
        Return Cache.UsersAndGroups.hsUsersInStep(stepId)
    End Function

    Public Shared Function GetGroupsUserGroupsIdsByStepID(ByVal stepId As Int64) As List(Of BaseImageFileResult)
        If Not Cache.UsersAndGroups.hsUserGroupsInStep.Contains(stepId) Then
            Dim dsFull As DataTable = WFFactory.GetGroupsUserGroupsIdsByStepID(stepId)
            Dim sort As List(Of BaseImageFileResult) = New List(Of BaseImageFileResult)
            If Not IsNothing(dsFull) Then
                If dsFull.Rows.Count > 0 Then
                    For Each row As DataRow In dsFull.Rows
                        sort.Add(New BaseImageFileResult(Int64.Parse(row("ID").ToString()), row("NAME").ToString()))
                    Next
                End If
            End If
            SyncLock Cache.UsersAndGroups.hsUserGroupsInStep.SyncRoot
                Cache.UsersAndGroups.hsUserGroupsInStep.Add(stepId, sort)
            End SyncLock
        End If
        Return Cache.UsersAndGroups.hsUserGroupsInStep(stepId)
    End Function

    Public Function GetAllGroups() As SortedList
        Return WFF.GetAllGroups
    End Function
#End Region

#Region "Refresh"
    'Public Sub Refresh(ByVal WFs() As WorkFlow)
    '    WFTaskBusiness.Refresh(WFs)
    'End Sub
    'Public Sub Refresh(ByVal WF As WorkFlow)
    '    WFTaskBusiness.Refresh(WF)
    'End Sub
#End Region

#Region "Steps"
    Public Sub SetInitialStep(ByVal StepNode As IEditStepNode)
        WFStepBusiness.SetInitial(StepNode)
    End Sub
    'Public Sub FillUsersAndGroups(ByRef WfStep As WFStep)
    '    WFStepBusiness.FillUsersAndGroups(WfStep)
    'End Sub
#End Region

#Region "Rules"
    'Public Sub LoadRules(ByVal WF As WorkFlow, ByVal TreeView As TreeView, ByVal LoadTreePanel As Boolean)
    '    WFRulesBusiness.LoadRules(WF, TreeView, LoadTreePanel)
    'End Sub
    'Public Sub LoadMonitorRules(ByRef wfstep As WFStep, ByVal TreeView As TreeView)
    '    WFRulesBusiness.LoadMonitorRules(wfstep, TreeView)
    'End Sub
    'Public Sub AddRule(ByVal ruleName As String, ByVal BaseNode As IBaseWFNode, ByVal RuleNameFromUser As String, ByVal TypeofRule As TypesofRules)
    '    WFRulesBusiness.Add(ruleName, BaseNode, RuleNameFromUser, TypeofRule)
    'End Sub

    ''' <summary>
    ''' Método que llama al método copy de la clase WFRulesBusiness
    ''' </summary>
    ''' <param name="ruleNode"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	16/09/2008	Created
    ''' </history>
    'Public Sub copyRule(ByRef ruleNode As RuleNode)
    '    WFRulesBusiness.copy(ruleNode)
    'End Sub

    'Public Sub CutRule(ByVal RuleNode As RuleNode)
    '    WFRulesBusiness.Cut(RuleNode)
    'End Sub

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
    Public Function GetUserActionName(ByVal rule As IRule) As String
        Try
            Dim WFRB As New WFRulesBusiness

            Dim dt As DataRow() = WFRB.GetRuleOption(rule.ID, 0, 43, 0)
            WFRB = Nothing

            If dt.Count > 0 Then
                Return dt(0).Item("ObjExtraData").ToString()
            Else
                Return rule.Name
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function


    ''' <summary>
    ''' Método que llama al método que permite pegar una o más reglas en el árbol y actualizar la base de datos
    ''' </summary>
    ''' <param name="RuleNode"></param>
    ''' <param name="BaseNode"></param>
    ''' <param name="isCopyNode"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    17/09/2008	Modified    Se agrego un nuevo parámetro (isCopyNode)
    ''' </history>
    'Public Sub AttachRule(ByVal RuleNode As RuleNode, ByVal BaseNode As BaseWFNode, ByVal isCopyNode As Boolean)
    '    WFRulesBusiness.PASTE(RuleNode, BaseNode, isCopyNode)
    'End Sub

    'Public Sub AddUserAction(ByVal StepNode As EditStepNode)
    '    WFRulesBusiness.AddUserAction(StepNode)
    'End Sub
    'Public Sub ChangeNameRule(ByVal RuleNode As IRuleNode)
    '    WFRulesBusiness.ChangeRuleName(RuleNode)
    'End Sub

    'Public Sub ChangeNameUserAction(ByVal UserActionNode As RuleTypeNode)
    '    WFRulesBusiness.ChangeName(UserActionNode)
    'End Sub
    'Public Sub ChangeUserActionName(ByVal UserActionNode As RuleTypeNode)
    '    WFRulesBusiness.ChangeUserActionName(UserActionNode)
    'End Sub
    Public Sub RemoveTask(ByRef Result As ITaskResult, ByVal DeleteDocument As Boolean, ByVal CurrentUserId As Int64, ByVal DeleteFile As Boolean)
        Dim WFTB As New WFTaskBusiness
        WFTB.Remove(Result, DeleteDocument, CurrentUserId, DeleteFile)
        WFTB = Nothing
    End Sub
    'Public Sub GetWFs(ByRef Result As ITaskResult, ByVal DeleteDocument As Boolean, ByVal CurrentUserId As Int64, ByVal DeleteFile As Boolean)
    '    WFTaskBusiness.Remove(Result, DeleteDocument, CurrentUserId, DeleteFile)
    'End Sub

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
    Public Sub saveItemSelectedThatCanBe_StateOrUserOrGrupo(ByVal ruleId As Int64, ByVal ruleSectionId As RuleSectionOptions, ByVal objId As RulePreferences)
        Dim objIdActual As RulePreferences = recoverItemSelectedThatCanBe_StateOrUserOrGroup(ruleId)
        WFF.saveItemSelectedThatCanBe_StateOrUserOrGrupo(ruleId, ruleSectionId, objId, objIdActual)
    End Sub

    ''' <summary>
    ''' Método utilizado para saber que checkbox quedo seleccionado cuando el usuario presiono guardar, así, cuando vuelve a la solapa habilitación
    ''' (Administrador -> Workflow -> Selección de una regla) recupera el checkbox que selecciono
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="forceReload">True para forzar la carga de datos en el hash</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	Aprox. 25/05/2008	Created
    '''     [Tomas]     25/06/2009      Modified    Se agrega la posibilidad de forzar la carga de datos al hash
    ''' </history>
    Public Function recoverItemSelectedThatCanBe_StateOrUserOrGroup(ByVal ruleId As Int64, Optional ByVal forceHashReload As Boolean = False) As RulePreferences
        Dim dv As DataView = Nothing
        Try

            If Cache.RulesOptions._DsRulesOptionsByRuleId.ContainsKey(ruleId) = False Then
                Dim WF As New WFRulesFactory()
                Cache.RulesOptions._DsRulesOptionsByRuleId.Add(ruleId, WF.GetRulesOptionsDT(ruleId))
                WF = Nothing
            End If

            Dim dt As DataTable = Cache.RulesOptions._DsRulesOptionsByRuleId(ruleId)



            dv = New DataView(dt)
            dv.RowFilter = "SectionId = " & RuleSectionOptions.Habilitacion & " And (ObjectId = " &
             RulePreferences.HabilitationSelectionState & " OR ObjectId = " & RulePreferences.HabilitationSelectionIndexAndVariable & " OR ObjectId = " & RulePreferences.HabilitationSelectionUser _
             & " OR ObjectId = " & RulePreferences.HabilitationSelectionBoth & ")"

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
    Public Function recoverUsers_Or_Groups_belongingToAState(ByVal ruleId As Int64, ByVal ruleSectionId As RuleSectionOptions, ByVal objId As RulePreferences, ByVal stateId As String) As DataSet
        Return (WFF.recoverUsers_Or_Groups_belongingToAState(ruleId, ruleSectionId, objId, stateId))
    End Function

    ''' <summary>
    ''' Devuelve los items deshabilitados para la regla cuando esta seleccionado el conjunto de habilitacion
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>Marcelo Created 24/05/2011</history>
    Public Function recoverDisableItemsBoth(ByVal ruleId As Int64) As DataSet
        Return WFF.recoverDisableItemsBoth(ruleId)

    End Function

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
    Public Function recoverItemsSelected(ByVal ruleId As Int64, ByVal sectionId As Integer, ByVal item1 As RulePreferences, ByVal item2 As RulePreferences, ByVal item3 As RulePreferences) As DataTable
        Dim dv As DataView = Nothing
        Try


            If Cache.RulesOptions._DsRulesOptionsByRuleId.ContainsKey(ruleId) = False Then
                Dim WF As New WFRulesFactory()
                Cache.RulesOptions._DsRulesOptionsByRuleId.Add(ruleId, WF.GetRulesOptionsDT(ruleId))
                WF = Nothing
            End If

            Dim dt As DataTable = Cache.RulesOptions._DsRulesOptionsByRuleId(ruleId)


            dv = New DataView(dt)
            dv.RowFilter = "SectionId = " & sectionId & " And (ObjectId = " &
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
    ''' Método que sirve para obtener el estado de habilitación del estado, es decir si está habilitado o deshabilitado
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="stateId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    23/04/2008  Created
    ''' </history>
    Public Function GetStateOfHabilitationOfState(ByVal ruleId As Int64, ByVal stateId As Int64) As Boolean
        Return WFF.GetStateOfHabilitationOfState(ruleId, stateId)
    End Function

#End Region

#Region "Tasks"


    Public Sub ChangeExpireDateTask(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int32, ByVal docTypeName As String, ByVal stepId As Int32, ByVal stepName As String, ByVal stateId As Int32, ByVal userName As String, ByVal workflowId As Int32, ByVal workflowName As String)

        'LOG sp_GetLogInformation
    End Sub




#End Region

    Public Function FillTransitions(ByVal Wf As WorkFlow, ByVal ArrayPares As ArrayList) As ArrayList
        Try
            ArrayPares.Clear()
            For Each s As WFStep In Wf.Steps.Values
                For Each Rule As WFRuleParent In s.Rules
                    FindRuleDerivates(ArrayPares, Rule)
                Next
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return ArrayPares
    End Function

    Private Function FindRuleDerivates(ByVal ArrayPares As ArrayList, ByRef rule As WFRuleParent) As ArrayList
        If rule.RuleClass.ToLower = "dodistribuir" Then
            Dim DoDistribuir As IDoDistribuir = rule
            Dim Vector(1) As String
            Vector(0) = DoDistribuir.WFStepId.ToString()
            If DoDistribuir.NewWFStepId > 0 Then
                Vector(1) = DoDistribuir.NewWFStepId.ToString()
                ArrayPares.Add(Vector)
            End If
        End If
        'Dim ChildRulesIdsAndClass = WFRulesBusiness.GetChildRulesIdsAndClassbyRuleId(rule.ID)
        'For Each R As WFRuleParent In ChildRulesIdsAndClass
        '    ArrayPares = FindRuleDerivates(ArrayPares, R)
        'Next
        'Return ArrayPares
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
    Public Function GetWFs() As WorkFlow()
        Return WFF.GetWFs()
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
    Public Function GetWFsToAddDocuments() As ArrayList
        Return WFF.GetWFsToAddDocuments
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que devuelve un dataset tipeado con todos los Workflows para los cuales el usuario tiene permisos
    ''' </summary>
    ''' <param name="UserID">Id de Usuario</param>
    ''' <returns>Dataset DsWF</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetWFs(ByVal UserID As Int32) As DsWF
        Return WFF.GetWFs(UserID)
    End Function

    '''-----------------------------------------------------------------------------
    ''' <summary>
    ''' todos los Workflows para los cuales el usuario tiene permisos para EDITAR Y MONITOREAR
    ''' </summary>
    ''' <param name="UserID">Id de Usuario</param>
    ''' <returns>Dataset DsWF</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	27/11/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Public Function GetWFsByUserRightsEDIT(ByVal UserID As Int64) As DsWF
    '    Dim grpids As New Generic.List(Of Int64)
    '    grpids = UserBusiness.GetUserGroupsIdsByUserid(UserID)
    '    If Not IsNothing(grpids) Then
    '        Return WFF.GetWFsByUserRightsEDIT(grpids)
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
    'Public Function GetWFsByUserRightMONITORING(ByVal UserID As Int64) As DsWF
    '    Dim grpids As New Generic.List(Of Int64)
    '    grpids = UserBusiness.GetUserGroupsIdsByUserid(UserID)
    '    If Not IsNothing(grpids) Then
    '        Return WFF.GetWFsByUserRightMONITORING(grpids)
    '    End If
    'End Function
#End Region

    Public Sub Fill(ByRef instance As IWorkFlow)
        If IsNothing(instance.Steps) Then
            instance.Steps = New SortedList()

            For Each CurrentStep As IWFStep In WFStepBusiness.GetStepsByWorkflow(instance.ID)
                instance.Steps.Add(CurrentStep.ID, CurrentStep)
            Next
        End If

        If IsNothing(instance.InitialStep) Then
            For Each CurrentStep As IWFStep In instance.Steps
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
    ''' Metodo que guarda las modificaciones a un dataset de objetos WorkFlows
    ''' </summary>
    ''' <param name="DsWf">Dataset DSWF</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub SaveWfChanges(ByVal DsWf As DsWF)
        WFF.SaveWfChanges(DsWf)
    End Sub
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
    Public Sub SaveNewName(ByVal Name As String, ByVal WFId As Int32)
        WFF.SaveNewName(Name, WFId)
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
    Public Sub SetWFDescriptionAndHelp(ByVal WFID As Int64, ByVal description As String, ByVal help As String)
        WFF.SetWFDescriptionAndHelp(WFID, description, help)
    End Sub

    Public Sub SaveWfInterval(ByVal Wfid As Int32, ByVal interval As Int32)

        WFF.SaveWfInterval(Wfid, interval)
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
    Public Sub SaveWfInitialStep(ByVal Wfid As Int32, ByVal InitialStepId As Int32)
        WFF.SaveWfInitialStep(Wfid, InitialStepId)
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
    Public Sub RemoveWorkFlow(ByVal wfid As Int32)
        WFF.RemoveWorkFlow(wfid)
    End Sub
#End Region

#Region "IncomingDocument"


    'Inserta un nuevo documento esperando por otro.
    Public Function InsertWaitDoc(ByRef _rule As IDoWaitForDocument) As Boolean

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
    Private Sub ZWFIInsert(ByRef _rule As IDoWaitForDocument, ByVal _waitID As Int32)

        Try
            WFF.ZWFIInsert(_waitID, _rule.DocType, _rule.RuleID)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub
    'Inserta nuevos registros en la tabla ZWFII
    Private Sub ZWFIIInsert(ByRef _rule As IDoWaitForDocument, ByVal _waitID As Int32)

        Try
            Dim mander As Char = Char.Parse("|")

            If Not IsNothing(_rule.IndiceID) AndAlso Not String.IsNullOrEmpty(_rule.IndiceID) Then

                Dim _indexArray As String() = _rule.IndiceID.Split(mander)
                Dim _ivalueArray As String() = _rule.IValue.Split(mander)

                For i As Int32 = 0 To _indexArray.Length - 1

                    Dim indice As Int32 = Int32.Parse(_indexArray(i).Trim)

                    WFF.ZWFIIInsert(_waitID, indice, _ivalueArray(i).Trim)

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
    Public Function ValidateDocIdInWF(ByVal DocId As Int64, ByVal wfid As Int64) As Boolean
        Dim Docscount As New Int32
        Docscount = WFF.ValidateDocIdInWF(DocId, wfid)
        If Docscount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Valida la Existencia de un documento en un workflow ( TRUE = Existe )
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> Diego </history>
    Public Function ValidateDocIdInWF(ByVal DocId As Int64, ByVal wfid As Int64, ByVal t As Transaction) As Boolean
        Dim Docscount As New Int32
        Docscount = WFF.ValidateDocIdInWF(DocId, wfid, t)
        If Docscount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
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
    Public Function GetWFbyId(ByVal wfid As Int64) As WorkFlow
        If Not Cache.Workflows.hsWorkflow.Contains(wfid) Then
            Dim WFSB As New WFStepBusiness

            Try
                Dim wf As WorkFlow = WFF.GetWFToAddDocuments(wfid)

                'TODO: VALIDO QUE NO ESTE EN CERO EL INICIALSTEPID, PUEDE ESTAR EN CERO? O HAY QUE ASIGNARLE UN INICIALSTEPID SIEMPRE?
                If wf.InitialStepIdTEMP <> 0 Then
                    wf.InitialStep = WFSB.GetStepById(wf.InitialStepIdTEMP)
                End If

                Dim ds As DataSet = WFStepsFactory.GetStepsByWorkflow(wfid)
                If (Not IsNothing(ds) And ds.Tables.Count > 0) Then
                    For Each CurrentRow As DataRow In ds.Tables(0).Rows
                        Dim etapa As WFStep = WFSB.GetStepById(CurrentRow("Step_Id"))

                        wf.Steps.Add(Int64.Parse(CurrentRow("Step_Id").ToString()), WFSB.GetStepById(CurrentRow("Step_Id")))
                    Next
                End If
                ds.Dispose()
                ds = Nothing

                SyncLock (Cache.Workflows.hsWorkflow)
                    If Cache.Workflows.hsWorkflow.Contains(wfid) Then
                        Cache.Workflows.hsWorkflow(wfid) = wf
                    Else
                        Cache.Workflows.hsWorkflow.Add(wfid, wf)
                    End If
                End SyncLock

            Catch ex As Exception
                ZCore.raiseerror(ex)
            Finally
                WFSB = Nothing
            End Try
        End If

        Return Cache.Workflows.hsWorkflow(wfid)
    End Function

    ''' <summary>
    ''' Método que sirve para obtener la etapa inicial de un determinado workflow
    ''' </summary>
    ''' <param name="wfId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	05/08/2008	Created
    ''' </history>
    Public Function GetInitialStepOfWF(ByVal wfId As Integer) As Integer
        Return (WFF.GetInitialStepOfWF(wfId))
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
    Public Function GetStepsByUserRestrictedDoctypes(ByVal userId As Int64) As ArrayList
        Dim Steps As ArrayList = New ArrayList()

        Dim ds As DataSet = WFF.GetStepsByUserRestrictedDoctypes(userId)

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
    Public Function GetUserHabilitatedRules(ByVal groupId As Int64, ByVal workflowId As Int64) As DataTable
        Return WFF.GetUserHabilitatedRules(groupId, workflowId)
    End Function

    Public Sub ClearHashTables()
        If Not IsNothing(Cache.Workflows.hsWorkflow) Then
            Cache.Workflows.hsWorkflow.Clear()
            Cache.Workflows.hsWorkflow = Nothing
            Cache.Workflows.hsWorkflow = New SynchronizedHashtable()
        End If
        Cache.RulesOptions.ClearAll()
    End Sub

    ''' <summary>
    ''' Verifica si puede ejecutar la regla, evaluando permisos de etapa, habilitacion de configuracion y solapa de habilitacion
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="userid"></param>
    ''' <param name="state"></param>
    ''' <param name="result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CanExecuteRules(ByVal ruleId As Long, ByVal userid As Long, ByVal state As WFStepState, ByVal result As TaskResult) As Boolean
        'Si tiene permisos de etapa
        Dim viewRule As Boolean = CanExecuteRules(ruleId, userid)
        If viewRule = False Then
            Return False
        End If

        Dim ruleBusiness As New WFRulesBusiness
        Try
            'Si la regla esta habilitada(configuracion)
            Dim ruleEnabled As Boolean = ruleBusiness.GetRuleEstado(ruleId)

            If Not ruleEnabled Then
                Return False
            End If

            'Verifica si hay alguna configuracion en la solapa de habilitacion
            Dim selectionvalue As RulePreferences = recoverItemSelectedThatCanBe_StateOrUserOrGroup(ruleId)

            'Se Evalua el valor de la variable seleccion 
            Select Case selectionvalue
                'Caso de trabajo con Estados
                Case RulePreferences.HabilitationSelectionState
                    Return getRuleEnabledByState(ruleBusiness, ruleId, state)
                    'Caso de trabajo con Usuarios o Grupos
                Case RulePreferences.HabilitationSelectionUser
                    Return getRuleEnabledByUser(ruleBusiness, ruleId, result)
                Case RulePreferences.HabilitationSelectionIndexAndVariable
                    'Caso de trabajo con indices y variables
                    Return GetRuleEnablefByIndexAndVariable(ruleId, result, ruleBusiness)
                Case RulePreferences.HabilitationSelectionBoth
                    'Caso de con Usuarios/Grupos, estados e indices y variables
                    Return getRuleEnableByGroupStateAndVariables(ruleId, result, state)
                Case Else
                    'Si no hay nada devuelve habilitado
                    Return True
            End Select
        Finally
            ruleBusiness = Nothing
        End Try
    End Function


    Private Function CanExecuteRules(RuleId As Int64, UserId As Int64) As Boolean
        If Not Cache.UsersAndGroups.hsUserRuleViewRight.ContainsKey(UserId & "-" & RuleId) Then
            SyncLock Cache.UsersAndGroups.hsUserRuleViewRight.SyncRoot
                Cache.UsersAndGroups.hsUserRuleViewRight.Add(UserId & "-" & RuleId, WFF.CanExecuteRules(RuleId, UserId))
            End SyncLock
        End If
        Return Cache.UsersAndGroups.hsUserRuleViewRight.Item(UserId & "-" & RuleId)
    End Function
    ''' <summary>
    ''' Verifica si el estado dado habilita o no la regla, si el estado es nothing devuelve true
    ''' </summary>
    ''' <param name="ruleBusiness"></param>
    ''' <param name="ruleId"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getRuleEnabledByState(ByRef ruleBusiness As WFRulesBusiness, ByVal ruleId As Long, ByVal state As WFStepState) As Boolean
        If state Is Nothing Then
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se habilita la regla ya que el state viene en null")
            Return True
        End If

        Dim dt As DataRow()

        'Se Obtienen los ids de estados DESHABILITADOS
        dt = ruleBusiness.GetRuleOption(ruleId, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeState)
        'Por cada Id de estado se compara con el id de estado seleccionado, en cada de encontrar
        'Coincidencia, se deshabilita la regla
        For Each r As DataRow In dt
            If r.Item("Objvalue") = state.ID Then
                Return False
            End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' Verifica si habilitar la regla por usuario y/o grupo, si el usuario la tiene deshabilitada la deshabilita. Sino verifica si al menos un grupo la tiene habilitada, sino devuelve false.
    ''' </summary>
    ''' <param name="ruleBusiness"></param>
    ''' <param name="ruleId"></param>
    ''' <param name="result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getRuleEnabledByUser(ByRef ruleBusiness As WFRulesBusiness, ByVal ruleId As Long, ByVal result As ITaskResult) As Boolean
        Dim dtDisabled As DataRow()
        Dim dtGroups As DataTable
        Try
            'Se Obtienen los ids de USUARIOS DESHABILITADOS
            dtDisabled = ruleBusiness.GetRuleOption(ruleId, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeUser)

            'Por cada Id de usuario se compara con el id de usuario logeado, en cada de encontrar
            'Coincidencia, se verifican los grupos
            If dtDisabled IsNot Nothing Then
                For Each r As DataRow In dtDisabled
                    If r.Item("Objvalue") = Zamba.Membership.MembershipHelper.CurrentUser.ID Then
                        ZTrace.WriteLineIf(ZTrace.IsWarning, String.Format("HABILITACION DE REGLAS: La Regla {0} esta deshabilitada para el usuario: {1}", ruleId, Zamba.Membership.MembershipHelper.CurrentUser.ID))
                        Return False
                    End If
                Next
            End If

            Dim ruleEnabled As Boolean = True

            'Se Obtienen los ids de GRUPOS DESHABILITADOS
            dtDisabled = ruleBusiness.GetRuleOption(ruleId, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeGroup)
            Dim groupsIds As List(Of Long)
            Dim stepId As Long
            'Grupos del usuario con permiso en la etapa

            Dim UB As New UserGroupBusiness
            groupsIds = UB.GetGroupsAndInheritanceOfGroupsIds(Zamba.Membership.MembershipHelper.CurrentUser.ID, True)

            'groupsIds = (From row As DataRow In WFStepBusiness.GetStepUserGroupsIdsAsDS(stepId, Zamba.Membership.MembershipHelper.CurrentUser.ID).Rows
            '             Select Long.Parse(row.ItemArray(0).ToString)).ToList()

            ruleEnabled = True
            For Each group As Long In groupsIds
                For Each r As DataRow In dtDisabled
                    If r.Item("Objvalue") = group Then
                        ZTrace.WriteLineIf(ZTrace.IsWarning, String.Format("HABILITACION DE REGLAS: La Regla {0} esta deshabilitada por pertenecer al siguiente grupo: {1}", ruleId, group))
                        ruleEnabled = False
                        Exit For
                    End If
                Next
                If ruleEnabled = False Then
                    Exit For
                End If
            Next

            Return ruleEnabled
        Finally

            If dtGroups IsNot Nothing Then
                dtGroups.Dispose()
                dtGroups = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Verifica si habilitar la regla por atributos, texto inteligente y/o zvar
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="result"></param>
    ''' <param name="ruleBusiness"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRuleEnablefByIndexAndVariable(ByVal ruleId As Long, ByVal result As TaskResult, ByVal ruleBusiness As WFRulesBusiness) As Boolean
        If result Is Nothing Then
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se habilita la regla ya que el result viene en null")
            Return True
        End If

        'Se obtienen los ids de variables
        Dim WFIndexAndVariableBusiness As New WFIndexAndVariableBusiness()
        Dim indexsAndVariables As List(Of IndexAndVariable) = WFIndexAndVariableBusiness.GetIndexAndVariableByRuleID(ruleId)
        Dim dt As DataRow()

        Try
            'Se Obtienen los ids de variables DESHABILITADOS
            dt = ruleBusiness.GetRuleOption(ruleId, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeIndexAndVariable)

            For Each _IndexAndVariable As IndexAndVariable In indexsAndVariables
                If validar(_IndexAndVariable, result, WFIndexAndVariableBusiness) Then
                    For Each r As DataRow In dt
                        If r("ObjValue") = _IndexAndVariable.ID Then
                            Return True
                        End If
                    Next
                    Return False
                End If
            Next
            Return True
        Finally
            WFIndexAndVariableBusiness = Nothing
            If indexsAndVariables IsNot Nothing Then
                indexsAndVariables.Clear()
                indexsAndVariables = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Verifica si habilitar la regla por condiciones mixtas
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="result"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getRuleEnableByGroupStateAndVariables(ByVal ruleId As Long, ByVal result As TaskResult, ByVal state As WFStepState) As Boolean
        Dim ruleEnable As Boolean = True

        'Se Obtienen los ids de estados DESHABILITADOS
        Dim Dt As New List(Of DataRow)
        'dt= recoverDisableItemsBoth(ruleId).Tables(0)

        'Filtro por estado
        '        Dt.DefaultView.RowFilter = "ObjValue='" & state.ID & "' and ObjectId in (37,38)"
        'HabilitationTypeStateAndUser = 37
        'HabilitationTypeStateAndGroup = 38
        Dim WFRB As New WFRulesBusiness
        Dim Dt37 As DataRow() = WFRB.GetRuleOption(ruleId, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeStateAndUser)
        Dim Dt38 As DataRow() = WFRB.GetRuleOption(ruleId, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeStateAndGroup)
        Dt.AddRange(Dt37)
        Dt.AddRange(Dt38)

        If Dt.Count > 0 Then
            'Se obtienen los ids de grupo del usuario y que tienen permiso en la etapa
            Dim DtUsersAndGroup As DataTable = WFStepBusiness.GetStepUserGroupsIdsAsDS(result.StepId, Zamba.Membership.MembershipHelper.CurrentUser.ID)
            Dim WFIndexAndVariableBusiness As New WFIndexAndVariableBusiness()
            Dim indexsAndVariables As List(Of IndexAndVariable) = WFIndexAndVariableBusiness.GetIndexAndVariableByRuleID(ruleId)

            For Each r As DataRow In Dt
                'Valido por grupo y usuario
                If Int32.Parse(r.Item("ObjExtraData").ToString) = Zamba.Membership.MembershipHelper.CurrentUser.ID Then
                    ruleEnable = False
                    For Each _IndexAndVariable As IndexAndVariable In indexsAndVariables
                        If validar(_IndexAndVariable, result, WFIndexAndVariableBusiness) Then
                            If r("ObjExtraData") = _IndexAndVariable.ID AndAlso r("ObjectId") = 62 AndAlso r("ObjValue") = state.ID Then
                                ruleEnable = False
                            End If
                        End If
                    Next
                    ruleEnable = True
                End If

                For Each rUser As DataRow In DtUsersAndGroup.Rows
                    If rUser.Item(0).ToString() = r.Item("ObjExtraData").ToString() Then
                        ruleEnable = False
                        For Each _IndexAndVariable As IndexAndVariable In indexsAndVariables
                            If validar(_IndexAndVariable, result, WFIndexAndVariableBusiness) Then
                                If r("ObjExtraData") = _IndexAndVariable.ID AndAlso r("ObjectId") = 62 AndAlso r("ObjValue") = state.ID Then
                                    ruleEnable = False
                                End If
                            End If
                        Next
                        ruleEnable = True
                    End If
                Next
            Next
        End If

        Return ruleEnable
    End Function

    Public Function validar(ByVal _IndexAndVariable As IndexAndVariable, ByVal _TaskResult As TaskResult, ByVal IndexAndVariableBusiness As WFIndexAndVariableBusiness) As Boolean
        Dim IndexAndVariableConfList As List(Of IndexAndVariableConfiguration) = IndexAndVariableBusiness.GetIndexAndVariableConfiguration(_IndexAndVariable.ID)
        Dim TextoInteligente As New Zamba.Core.TextoInteligente()

        Try
            For Each IndexAndVariableConf As IndexAndVariableConfiguration In IndexAndVariableConfList
                Dim value1 As String = IndexAndVariableConf.Name
                Dim VarInterReglas As New VariablesInterReglas()
                If IndexAndVariableConf.Manual = "N" Then
                    For Each i As Index In _TaskResult.Indexs
                        If value1 = i.ID Then
                            value1 = i.Data
                            Exit For
                        End If
                    Next
                Else
                    value1 = VarInterReglas.ReconocerVariablesValuesSoloTexto(value1)
                    value1 = TextoInteligente.ReconocerCodigo(value1, _TaskResult)
                End If

                Dim value2 As String = IndexAndVariableConf.Value
                value2 = VarInterReglas.ReconocerVariablesValuesSoloTexto(value2)
                value2 = TextoInteligente.ReconocerCodigo(value2, _TaskResult)
                VarInterReglas = Nothing

                Dim comparator As Comparadores
                'Le asigno el comparador al IfIndex
                Select Case IndexAndVariableConf.Operador
                    Case "="
                        comparator = Comparadores.Igual
                    Case "<>"
                        comparator = Comparadores.Distinto
                    Case "<"
                        comparator = Comparadores.Menor
                    Case ">"
                        comparator = Comparadores.Mayor
                    Case "<="
                        comparator = Comparadores.IgualMenor
                    Case "Contiene"
                        comparator = Comparadores.Contiene
                    Case "Empieza"
                        comparator = Comparadores.Empieza
                    Case "Termina"
                        comparator = Comparadores.Termina
                    Case ">="
                        comparator = Comparadores.IgualMayor
                End Select

                If ToolsBusiness.ValidateComp(value1, value2, comparator, False) = False Then
                    Return False
                ElseIf _IndexAndVariable.Operador.ToLower() = "or" Then
                    Return True
                End If
            Next
        Finally
            TextoInteligente = Nothing
        End Try

        Return True
    End Function
End Class