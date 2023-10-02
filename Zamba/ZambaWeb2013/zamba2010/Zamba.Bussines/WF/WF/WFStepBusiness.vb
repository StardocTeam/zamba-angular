Imports Zamba.Core
Imports Zamba.Data
Imports System.Collections.Generic
Imports Zamba.Servers
Imports Zamba.Core.Caching
Imports Zamba.Membership
Imports System.Text

Public Class WFStepBusiness

    Private Shared dsStepNames As DataSet = Nothing
    Private Shared _useIndexCache As String

#Region "Constants"
    Private Const COLUMN_COLOR As String = "Color"
    Private Const COLUMN_CREATION_DATE As String = "CreateDate"
    Private Const COLUMN_DESCRIPTION As String = "Description"
    Private Const COLUMN_EDITION_DATE As String = "EditDate"
    Private Const COLUMN_HEIGHT As String = "Height"
    Private Const COLUMN_HELP As String = "Help"
    Private Const COLUMN_IMAGE_INDEX As String = "ImageIndex"
    Private Const COLUMN_LOCALITION_X As String = "LocationX"
    Private Const COLUMN_LOCALITION_Y As String = "LocationY"
    Private Const COLUMN_MAX_HOURS As String = "max_Hours"
    Private Const COLUMN_MAX_DOCUMENTS As String = "max_docs"
    Private Const COLUMN_NAME As String = "Name"
    Private Const COLUMN_START_AT_OPEN_DOC As String = "StartAtOpenDoc"
    Private Const COLUMN_STEP_ID As String = "step_id"
    Private Const COLUMN_WIDTH As String = "Width"
    Private Const COLUMN_WIDTH_2 As String = "Width2"
    Private Const COLUMN_WORK_ID As String = "work_id"
    Private Const COLUMN_TASKS_COUNT As String = "TasksCount"
    Private Const COLUMN_EXPIRED_TASKS_COUNT As String = "ExpiredTasksCount"
#End Region

    Private Shared Function Build(ByVal dr As DataRow) As IWFStep
        Dim CurrentStep As IWFStep = Nothing

        If Not IsNothing(dr) Then
            Try
                CurrentStep = New WFStep()

                If Not IsNothing(dr(COLUMN_STEP_ID)) AndAlso Not IsDBNull(dr(COLUMN_STEP_ID)) Then
                    Dim tryValue As Int64

                    If Int64.TryParse(dr(COLUMN_STEP_ID).ToString(), tryValue) Then
                        CurrentStep.ID = tryValue
                    Else
                        CurrentStep.ID = 0
                    End If
                End If

                If Not IsNothing(dr(COLUMN_WORK_ID)) AndAlso Not IsDBNull(dr(COLUMN_WORK_ID)) Then
                    Dim tryValue As Int32

                    If Int32.TryParse(dr(COLUMN_WORK_ID).ToString(), tryValue) Then
                        CurrentStep.WorkId = tryValue
                    Else
                        CurrentStep.WorkId = 0
                    End If
                End If


                If Not IsNothing(dr(COLUMN_NAME)) AndAlso Not IsDBNull(dr(COLUMN_NAME)) Then
                    CurrentStep.Name = dr(COLUMN_NAME).ToString()
                End If

                If Not IsNothing(dr(COLUMN_HELP)) AndAlso Not IsDBNull(dr(COLUMN_HELP)) Then
                    CurrentStep.Help = dr(COLUMN_HELP).ToString()
                End If

                If Not IsNothing(dr(COLUMN_DESCRIPTION)) AndAlso Not IsDBNull(dr(COLUMN_DESCRIPTION)) Then
                    CurrentStep.Description = dr(COLUMN_DESCRIPTION).ToString()
                End If

                'TODO: IMAGEINDEX

                'If Not IsNothing(dr(COLUMN_IMAGE_INDEX)) Then
                '    Dim tryValue As Int64

                '    If Int64.TryParse(dr("work_id").ToString(), tryValue) AndAlso Not IsDBNull(dr("work_id")) Then
                '        CurrentStep.ID = tryValue
                '    Else
                '        CurrentStep.ID = 0
                '    End If
                'End If

                If Not IsNothing(dr(COLUMN_EXPIRED_TASKS_COUNT)) AndAlso Not IsDBNull(dr(COLUMN_EXPIRED_TASKS_COUNT)) Then
                    Dim tryValue As Int32

                    If Int32.TryParse(dr(COLUMN_EXPIRED_TASKS_COUNT).ToString(), tryValue) Then
                        CurrentStep.ExpiredTasksCount = tryValue
                    Else
                        CurrentStep.ExpiredTasksCount = 0
                    End If
                End If

                If Not IsNothing(dr(COLUMN_TASKS_COUNT)) AndAlso Not IsDBNull(dr(COLUMN_TASKS_COUNT)) Then
                    Dim tryValue As Int32

                    If Int32.TryParse(dr(COLUMN_TASKS_COUNT).ToString(), tryValue) Then
                        CurrentStep.TasksCount = tryValue
                    Else
                        CurrentStep.TasksCount = 0
                    End If
                End If

                If Not IsNothing(COLUMN_EDITION_DATE) AndAlso Not IsDBNull(COLUMN_EDITION_DATE) Then
                    Dim tryValue As Date

                    If Date.TryParse(dr(COLUMN_EDITION_DATE).ToString(), tryValue) Then
                        CurrentStep.EditDate = tryValue
                    End If
                End If

                If Not IsNothing(dr(COLUMN_CREATION_DATE)) AndAlso Not IsDBNull(dr(COLUMN_CREATION_DATE)) Then
                    Dim tryValue As Date

                    If Date.TryParse(dr(COLUMN_CREATION_DATE).ToString(), tryValue) Then
                        CurrentStep.CreateDate = tryValue
                    End If
                End If

                If Not IsNothing(dr(COLUMN_LOCALITION_X)) AndAlso Not IsDBNull(dr(COLUMN_LOCALITION_X)) AndAlso Not IsNothing(dr(COLUMN_LOCALITION_Y)) AndAlso Not IsDBNull(dr(COLUMN_LOCALITION_Y)) Then
                    Dim tryValueX As Int32
                    Dim tryValueY As Int32

                    If Int32.TryParse(dr(COLUMN_LOCALITION_X).ToString(), tryValueX) AndAlso Int32.TryParse(dr(COLUMN_LOCALITION_Y).ToString(), tryValueY) Then
                        CurrentStep.Location = New Drawing.Point(tryValueX, tryValueY)
                    End If
                End If

                If Not IsNothing(dr(COLUMN_MAX_HOURS)) AndAlso Not IsDBNull(dr(COLUMN_MAX_HOURS)) Then
                    Dim tryValue As Decimal

                    If Decimal.TryParse(dr(COLUMN_MAX_HOURS).ToString(), tryValue) Then
                        CurrentStep.MaxHours = tryValue
                    End If
                End If

                If Not IsNothing(dr(COLUMN_MAX_DOCUMENTS)) AndAlso Not IsDBNull(dr(COLUMN_MAX_DOCUMENTS)) Then
                    Dim tryValue As Int32

                    If Int32.TryParse(dr(COLUMN_MAX_DOCUMENTS).ToString(), tryValue) Then
                        CurrentStep.MaxDocs = tryValue
                    End If
                End If


                If Not IsNothing(dr(COLUMN_START_AT_OPEN_DOC)) AndAlso Not IsDBNull(dr(COLUMN_START_AT_OPEN_DOC)) Then
                    Dim tryValue As Boolean

                    If Boolean.TryParse(dr(COLUMN_START_AT_OPEN_DOC).ToString(), tryValue) Then
                        CurrentStep.StartAtOpenDoc = tryValue
                    End If
                End If

                If Not IsNothing(dr(COLUMN_COLOR)) AndAlso Not IsDBNull(dr(COLUMN_COLOR)) Then
                    CurrentStep.color = dr(COLUMN_COLOR).ToString()
                End If


                If Not IsNothing(dr(COLUMN_HEIGHT)) AndAlso Not IsDBNull(dr(COLUMN_HEIGHT)) Then
                    Dim tryValue As Int32

                    If Int32.TryParse(dr(COLUMN_HEIGHT).ToString(), tryValue) Then
                        CurrentStep.Height = tryValue
                    End If

                End If

                If Not IsNothing(dr(COLUMN_WIDTH)) AndAlso Not IsDBNull(dr(COLUMN_WIDTH)) Then
                    Dim tryValue As Int32

                    If Int32.TryParse(dr(COLUMN_WIDTH).ToString(), tryValue) Then
                        CurrentStep.Width = tryValue
                    End If

                End If


            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If

        Return CurrentStep
    End Function

#Region "Get"
    Public Shared Function GetStepByWorkIdAndStepId(ByVal wfid As Int64, ByVal wfstepid As Int64) As IWFStep
        Dim WFB As New WFBusiness
        Dim wf As IWorkFlow
        wf = WFB.GetWFbyId(wfid)
        WFB = Nothing
        Return wf.Steps.Item(Convert.ToDecimal(wfstepid))
    End Function

    Public Shared Function GetStepIdByRuleId(ByVal RuleId As Int64) As Int64
        Return WFStepsFactory.GetStepIdByRuleId(RuleId)
    End Function

    Public Shared Function GetStepsByWorkflow(ByVal workflowId As Int64) As List(Of IWFStep)
        Dim StepList As New List(Of IWFStep)

        Dim ds As DataSet = WFStepsFactory.GetStepsByWorkflow(workflowId)

        If ds IsNot Nothing Then
            For Each CurrentRow As DataRow In ds.Tables(0).Rows
                StepList.Add(WFStepBusiness.Build(CurrentRow))
            Next
            ds.Dispose() 'validar referencias
            ds = Nothing
        End If

        Return StepList
    End Function

    ''' <summary>
    ''' Devuelve una coleccion de ids de etapas
    ''' </summary>
    ''' <param name="WorkflowId"></param>
    ''' <history>Marcelo created 10/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetStepsIdAndNamebyWorkflowId(ByVal WorkflowId As Int64) As Dictionary(Of Int64, String)
        Return WFStepsFactory.GetStepsIdAndNamebyWorkflowId(WorkflowId)
    End Function

    Public Shared Function GetStepsByWorkflows(ByVal workflowIds As List(Of Int64)) As List(Of IWFStep)

        Dim StepList As New List(Of IWFStep)
        If (Not IsNothing(workflowIds) AndAlso workflowIds.Count > 0) Then

            Dim dsSteps As DataSet = WFStepsFactory.GetStepsByWorkflows(workflowIds)

            If dsSteps IsNot Nothing Then
                If dsSteps.Tables.Count > 0 Then
                    For Each CurrentRow As DataRow In dsSteps.Tables(0).Rows
                        StepList.Add(WFStepBusiness.Build(CurrentRow))
                    Next
                End If
                dsSteps.Dispose()
                dsSteps = Nothing
            End If
        End If

        Return StepList
    End Function

    Friend Shared Sub FillSteps(ByVal WF As WorkFlow)
        Dim dsSteps As DsSteps
        If Not Cache.Workflows.hsWFStepsDS.ContainsKey(WF.ID) Then
            SyncLock (Cache.Workflows.hsWFStepsDS)
                If Not Cache.Workflows.hsWFStepsDS.ContainsKey(WF.ID) Then
                    Cache.Workflows.hsWFStepsDS.Add(WF.ID, WFStepsFactory.FillSteps(WF))
                End If
            End SyncLock
        End If
        dsSteps = DirectCast(Cache.Workflows.hsWFStepsDS.Item(WF.ID), DsSteps)

        Dim dsStates As DsStepState
        If Not Cache.Workflows.hsWFStepStatesDS.ContainsKey(WF.ID) Then
            SyncLock (Cache.Workflows.hsWFStepStatesDS)
                If Not Cache.Workflows.hsWFStepStatesDS.ContainsKey(WF.ID) Then
                    Cache.Workflows.hsWFStepStatesDS.Add(WF.ID, WFStepStatesFactory.GetAllStates(dsSteps))
                End If
            End SyncLock
        End If
        dsStates = DirectCast(Cache.Workflows.hsWFStepStatesDS.Item(WF.ID), DsStepState)

        For Each r As DsSteps.WFStepsRow In dsSteps.WFSteps.Rows
            Try
                Dim wfstep As New WFStep(WF.ID, r.Step_Id, r.Name, r.Help, r.Description, New Drawing.Point(r.LocationX, r.LocationY), r.ImageIndex, r.Max_Docs, r.Max_Hours, r.StartAtOpenDoc, r.Color, r.Width, r.Height, 0, 0)
                If WF.Steps.ContainsKey(wfstep.ID) Then
                    WF.Steps(wfstep.ID) = wfstep
                Else
                    WF.Steps.Add(wfstep.ID, wfstep)
                End If
                WFStepStatesFactory.FillState(WF.Steps(wfstep.ID), dsStates)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        Next

        dsSteps.Dispose() 'validar referencia con cache
        dsSteps = Nothing
        dsStates.Dispose() 'validar referencia
        dsStates = Nothing

        'Seteo Estapa Inicial
        WF.SetInitialStep()
    End Sub

    Public Shared Function GetDsSteps(ByVal WFId As Int64) As DsSteps
        Return WFStepsFactory.GetDsSteps(WFId)
    End Function

    Public Shared Function GetDsSteps(ByVal WFId As List(Of Int64)) As DsSteps
        Return WFStepsFactory.GetDsSteps(WFId)
    End Function

    Public Shared Function GetDsAllSteps() As DsSteps
        Return WFStepsFactory.GetDsAllSteps()
    End Function

    ''' <summary>
    ''' Limpia los hashtables
    ''' </summary>
    ''' <param name="WFId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Sub ClearHashTables()
        If Not IsNothing(Cache.Workflows.hsSteps) Then
            Cache.Workflows.hsSteps.Clear()
            Cache.Workflows.hsSteps = Nothing
            Cache.Workflows.hsSteps = New SynchronizedHashtable()
        End If
        If Not IsNothing(Cache.Workflows.hsWFStepStatesDS) Then
            Cache.Workflows.hsWFStepStatesDS.Clear()
            Cache.Workflows.hsWFStepStatesDS = Nothing
            Cache.Workflows.hsWFStepStatesDS = New SynchronizedHashtable()
        End If
        If Not IsNothing(Cache.Workflows.hsWFStepsDS) Then
            Cache.Workflows.hsWFStepsDS.Clear()
            Cache.Workflows.hsWFStepsDS = Nothing
            Cache.Workflows.hsWFStepsDS = New SynchronizedHashtable()
        End If
        If Not IsNothing(Cache.Workflows.hsStepsColors) Then
            Cache.Workflows.hsStepsColors.Clear()
            Cache.Workflows.hsStepsColors = Nothing
            Cache.Workflows.hsStepsColors = New SynchronizedHashtable()
        End If
        If Not IsNothing(Cache.Workflows.hsDocTypesAsWFbyStepId) Then
            Cache.Workflows.hsDocTypesAsWFbyStepId.Clear()
            Cache.Workflows.hsDocTypesAsWFbyStepId = Nothing
            Cache.Workflows.hsDocTypesAsWFbyStepId = New SynchronizedHashtable()
        End If

        Cache.Rules.RemoveCurrentInstance()
        ZCore.RemoveCurrentInstance()
    End Sub

    ''' <summary>
    ''' Devuelve una coleccion de ids de etapas las cuales poseen reglas en el evento Abrir Zamba y que el usuario tiene permiso de ejecutar.
    ''' </summary>
    ''' <param name="WorkflowId"></param>
    ''' <history>Ezequiel created 27/07/2009</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetStepsWithZOpenEvent(ByVal UsrID As Int64) As Dictionary(Of Int64, String)
        Return WFStepsFactory.GetStepsWithZOpenEvent(UsrID)
    End Function

    Public Shared Function GetSteps(ByVal stepIds As List(Of Int64)) As List(Of IWFStep)
        Dim StepList As New List(Of IWFStep)
        Dim ds As DataSet = WFStepsFactory.GetStepsDs(stepIds)

        If Not IsNothing(ds) AndAlso ds.Tables.Count = 1 AndAlso ds.Tables(0).Rows.Count = 1 Then
            StepList.Add(WFStepBusiness.Build(ds.Tables(0).Rows(0)))
        End If
        ds.Dispose() 'verificar referencia
        ds = Nothing

        Return StepList
    End Function

    ''' <summary>
    ''' Obtiene la etapa por el id
    ''' </summary>
    ''' <param name="StepId">Id de la etapa</param>
    ''' <param name="isReload">True para forzar la carga</param>
    ''' <param name="typeofrule">Si <> de 0 devuelve las reglas segun tipo en enumerators.typesOfRule</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStepById(ByVal StepId As Int64) As IWFStep
        If Cache.Workflows.hsSteps.ContainsKey(StepId) Then
            Return Cache.Workflows.hsSteps(StepId)
        Else
            Dim ds As DataSet = WFStepsFactory.GetStepById(StepId)
            If ds IsNot Nothing Then
                If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Dim dr As DataRow = ds.Tables(0).Rows(0)
                    Dim newStep As WFStep = New WFStep(dr("work_id"), StepId, dr("Name"), dr("Help"), dr("Description"), New Drawing.Point(dr("LocationX"), dr("LocationY")), dr("ImageIndex"), dr("Max_Docs"), dr("Max_Hours"), dr("StartAtOpenDoc"), dr("color"), dr("Height"), dr("width"), 0, 0)
                    dr = Nothing
                    newStep.States = WFStepStatesComponent.GetStepStatesByStepId(StepId)

                    newStep.InitialState = WFStepStatesComponent.getInitialStateFromList(newStep.States)
                    Dim WFRulesBusiness As New WFRulesBusiness
                    Dim rules As List(Of IWFRuleParent) = WFRulesBusiness.GetCompleteHashTableRulesByStep(newStep.ID)
                    WFRulesBusiness = Nothing

                    If Not IsNothing(rules) Then
                        For i As Int32 = 0 To rules.Count - 1
                            If newStep.Rules.Contains(rules(i)) = False Then newStep.Rules.Add(rules(i))
                        Next
                    End If
                    SyncLock (Cache.Workflows.hsSteps)
                        If Cache.Workflows.hsSteps.ContainsKey(StepId) = False Then Cache.Workflows.hsSteps.Add(StepId, newStep)
                    End SyncLock
                End If

                ds.Dispose()
                ds = Nothing
            End If
        End If
        Return Cache.Workflows.hsSteps(StepId)
    End Function


    Public Shared Function GetStepIdByTaskId(ByVal TaskId As Int64) As Int64
        Return WFStepsFactory.GetStepIdByTaskId(TaskId)
    End Function



    Public Function GetDocTypesByWfStepAsDT(ByVal stepId As Int64, Optional ByVal userId As Long = -1) As DataTable
        Dim strDocsCode As String = stepId & "-" & userId
        If Cache.DocTypesWF.GetInstance().hsDocTypesWF.ContainsKey(strDocsCode) = False Then
            Cache.DocTypesWF.GetInstance().hsDocTypesWF.Add(strDocsCode, WFStepsFactory.GetDocTypesByWfStep(stepId))
            'Si no hay tareas en la etapa y luego se distribuye una tarea ahi, se recarga la cache
        ElseIf IsNothing(Cache.DocTypesWF.GetInstance().hsDocTypesWF(strDocsCode)) Then
            Cache.DocTypesWF.GetInstance().hsDocTypesWF(strDocsCode) = WFStepsFactory.GetDocTypesByWfStep(stepId)
            'Si no hay tareas en la etapa y luego se distribuye una tarea ahi, se recarga la cache
        ElseIf DirectCast(Cache.DocTypesWF.GetInstance().hsDocTypesWF(strDocsCode), DataTable).Rows.Count = 0 Then
            Cache.DocTypesWF.GetInstance().hsDocTypesWF(strDocsCode) = WFStepsFactory.GetDocTypesByWfStep(stepId)
        End If
        Return Cache.DocTypesWF.GetInstance().hsDocTypesWF(strDocsCode)
    End Function

    ''' <summary>
    ''' Obtiene las id de tipos de documento que se encuentran asociados a un WF, por su stepid
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Javier] 14/10/2010  Created.
    ''' </history>
    Public Shared Function GetDocTypesAssociatedToWFbyStepId(ByVal stepId As Int64) As List(Of Int64)
        If Cache.Workflows.hsDocTypesAsWFbyStepId.Contains(stepId) = False Then
            Dim ar As New List(Of Int64)
            Dim dtids As DataTable = WFStepsFactory.GetDocTypesAssociatedToWFbyStepId(stepId)

            If dtids IsNot Nothing Then
                For Each r As DataRow In dtids.Rows
                    ar.Add(r(0))
                Next
                dtids.Dispose()
                dtids = Nothing
            End If

            SyncLock (Cache.Workflows.hsDocTypesAsWFbyStepId)
                Cache.Workflows.hsDocTypesAsWFbyStepId.Add(stepId, ar)
            End SyncLock
        End If
        Return Cache.Workflows.hsDocTypesAsWFbyStepId(stepId)
    End Function

    ''' <summary>
    ''' Devuelve el nombre de una etapa por su ID
    ''' </summary>
    ''' <param name="StepId">ID de la etapa</param>
    ''' <history> Marcelo Modified 18/01/10</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetStepNameById(ByVal StepId As Int64) As String
        If StepId > 0 Then
            If (Cache.Workflows.hsStepsNames.Contains(StepId)) Then
                Return Cache.Workflows.hsStepsNames(StepId)
            Else
                Dim StepName As String = WFStepsFactory.GetStepNameById(StepId)
                SyncLock Cache.Workflows.hsStepsNames
                    If (Cache.Workflows.hsStepsNames.Contains(StepId) = False) Then
                        Cache.Workflows.hsStepsNames.Add(StepId, StepName)
                    End If
                End SyncLock
                Return StepName
            End If
        Else
                Return String.Empty
        End If

    End Function

    Public Shared Function GetTasksConsumedMinutes(ByVal stepId As Int64) As List(Of Int32)
        Dim DtConsumedMinutes As DataTable = WFStepsFactory.GetTasksConsumedMinutes(stepId)
        Dim ConsumedMinutes As New List(Of Int32)

        If DtConsumedMinutes IsNot Nothing Then
            For Each CurrentRow As DataRow In DtConsumedMinutes.Rows
                ConsumedMinutes.Add(CurrentRow.Item(0))
            Next
            DtConsumedMinutes.Dispose() 'validar referencia
            DtConsumedMinutes = Nothing
        End If

        Return ConsumedMinutes
    End Function
#End Region

#Region "Users"

#Region "StepsUsersIdsAndNames"
    Private Shared StepsUsersIdsAndNames As New Hashtable

    Public Shared Function GetStepUsersIdsAndNames(ByVal wfstepid As Int64) As Generic.List(Of IZBaseCore)
        Return GetStepUsersIdsAndNames(wfstepid, False)
    End Function

    Public Shared Function GetStepUsersIdsAndNames(ByVal wfstepid As Int64, ByVal ReLoad As Boolean) As Generic.List(Of IZBaseCore)

        If Not StepsUsersIdsAndNames.ContainsKey(wfstepid) Then
            LoadStepUsersIdsAndNames(wfstepid)
        ElseIf ReLoad Then
            StepsUsersIdsAndNames.Remove(wfstepid)
            LoadStepUsersIdsAndNames(wfstepid)
        End If

        Return StepsUsersIdsAndNames(wfstepid)

    End Function

    ''' <summary>
    ''' Busca si existe el permiso de usuario en la etapa
    ''' </summary>
    ''' <param name="wfstepID">Id de la etapa</param>
    ''' <param name="reload"></param>
    ''' <param name="userId">Id del usuario a buscar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetIfUserInStep(ByVal wfstepID As Int64, ByVal reload As Boolean, ByVal userId As Int64)
        Dim users As Generic.List(Of IZBaseCore) = GetStepUsersIdsAndNames(wfstepID, reload)

        For Each User As IZBaseCore In users
            If User.ID = userId Then
                Return True
            End If
        Next

        Return False
    End Function

    '------------------------------------------------------------
    'osanchez
    '2009.04.13
    'Carga la lista estatica con los valores de la consulta 
    '-------------------------------------------------------------
    Private Shared Sub LoadStepUsersIdsAndNames(ByRef wfstepid As Int64)
        Dim UsersIdsAndNames As New Generic.List(Of IZBaseCore)
        Dim Dt As DataTable = WFStepsFactory.GetStepOnlyUsersIdsAndNames(wfstepid)
        For Each dr As DataRow In Dt.Rows
            Dim ZBC As IZBaseCore
            ZBC = New ZCoreView(dr.Item("id"), dr.Item("name"))
            UsersIdsAndNames.Add(ZBC)
        Next
        StepsUsersIdsAndNames.Add(wfstepid, UsersIdsAndNames)
    End Sub
#End Region
#Region "StepsUserGroupsIdsAndNames"
    Private Shared StepsUserGroupsIdsAndNames As New Hashtable
    Private Shared StepsUserGroupsIdsByUser As New Dictionary(Of String, DataTable)

    Public Shared Function GetStepUserGroupsIdsAndNames(ByRef wfstepid As Int64) As Generic.List(Of IZBaseCore)
        Return GetStepUserGroupsIdsAndNames(wfstepid, False)
    End Function

    Public Shared Function GetStepUserGroupsIdsAndNames(ByRef wfstepid As Int64, ByVal ReLoad As Boolean) As Generic.List(Of IZBaseCore)

        If Not StepsUserGroupsIdsAndNames.ContainsKey(wfstepid) Then
            LoadStepUserGroupsIdsAndNames(wfstepid)
        ElseIf ReLoad Then
            StepsUserGroupsIdsAndNames.Remove(wfstepid)
            LoadStepUserGroupsIdsAndNames(wfstepid)
        End If

        Return StepsUserGroupsIdsAndNames(wfstepid)
    End Function

    Public Shared Function GetStepUserGroupsIdsAndNamesAsDS(ByRef wfstepid As Int64) As DataTable
        Return WFStepsFactory.GetStepUserGroupsIdsAndNames(wfstepid)
    End Function

    Public Shared Function GetStepUserGroupsIdsAsDS(ByVal wfstepid As Int64, ByVal userID As Int64) As DataTable
        Try
            If StepsUserGroupsIdsByUser.ContainsKey(wfstepid & " - " & userID) = False Then
                StepsUserGroupsIdsByUser.Add(wfstepid & " - " & userID, WFStepsFactory.GetStepUserGroupsIdsByUserID(wfstepid, userID))
            End If
            Return StepsUserGroupsIdsByUser(wfstepid & " - " & userID)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return New DataTable
        End Try
    End Function

    '------------------------------------------------------------
    'osanchez
    '2009.04.13
    'Carga la lista estatica con los valores de la consulta 
    '-------------------------------------------------------------
    Private Shared Sub LoadStepUserGroupsIdsAndNames(ByRef wfstepid As Int64)
        Dim UserGroupsIdsAndNames As New Generic.List(Of IZBaseCore)
        Dim Dt As DataTable = WFStepsFactory.GetStepUserGroupsIdsAndNames(wfstepid)
        For Each dr As DataRow In Dt.Rows
            Dim ZBC As IZBaseCore
            ZBC = New ZCoreView(dr.Item("id"), dr.Item("name"))
            UserGroupsIdsAndNames.Add(ZBC)
        Next
        StepsUserGroupsIdsAndNames.Add(wfstepid, UserGroupsIdsAndNames)
    End Sub
#End Region
#End Region

#Region "Initial"
    Friend Shared Sub SetInitial(ByVal StepNode As EditStepNode, Optional ByVal ask As Boolean = True)
        Dim WFB As New WFBusiness
        Try
            If ask = False OrElse MessageBox.Show("¿Desea poner la etapa seleccionada como inicial?", "Edicion de Etapas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim wf As IWorkFlow
                wf = WFB.GetWFbyId(StepNode.WFStep.WorkId)
                wf.InitialStep = StepNode.WFStep

                WFStepsFactory.UpdateInitialStep(StepNode.WFStep.WorkId, wf.InitialStep.ID)
                For Each n As TreeNode In StepNode.Parent.Nodes
                    If n Is StepNode Then
                        DirectCast(n, EditStepNode).IsInitialStep(True)
                    Else
                        DirectCast(n, EditStepNode).IsInitialStep(False)
                    End If
                Next
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            WFB = Nothing
        End Try
    End Sub
#End Region

    ''' <summary>
    ''' Método que remueve de la tabla WFStepOpt un tipo de permiso
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <param name="typeOfPermit"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    29/09/2008  Created        
    ''' </history>
    Public Shared Sub removeTypeOfPermit(ByVal stepId As Long, ByVal typeOfPermit As Integer)
        WFStepsFactory.removeTypeOfPermit(stepId, typeOfPermit)
    End Sub

    ''' <summary>
    ''' Método que llama a un método que inserta en la tabla WFStepOpt un tipo de permiso 
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <param name="typeOfPermit"></param>
    ''' <param name="numberOfDays"></param>
    ''' <param name="nameColor"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    29/09/2008  Created        
    ''' </history>
    Public Shared Sub addTypeOfPermit(ByVal stepId As Long, ByVal typeOfPermit As Integer, ByVal objTwo As Integer, ByVal objExtraData As String)
        WFStepsFactory.addTypeOfPermit(stepId, typeOfPermit, objTwo, objExtraData)
    End Sub

    ''' <summary>
    ''' Traigo las opciones de la etapa
    ''' [Sebastian 17-09-09] Se actualizo el hash de los colores en caso de que se remuevan o cambie de etapa
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''      [Ezequiel] - 28/09/09 - Se modifico la manera de utilizar cache.
    ''' </history>
    Public Shared Function getTypesOfPermit(ByVal stepId As Long, ByVal typeOfPermision As TypesofPermits) As DataTable
        Dim StepsOpt As Cache.StepsOpt = Cache.StepsOpt.GetInstance()
        If Not StepsOpt.hsStepsOpt.ContainsKey(stepId & " - " & typeOfPermision) Then
            StepsOpt.hsStepsOpt.Add(stepId & " - " & typeOfPermision, WFStepsFactory.getTypesOfPermit(stepId, typeOfPermision).Tables(0))
        End If
        Return StepsOpt.hsStepsOpt(stepId & " - " & typeOfPermision)
    End Function


    ''' <summary>
    ''' Trae los colores de la etapa
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''   [Ezequiel] - 28/09/09 - Created.
    ''' </history>
    Public Function GetStepColors(ByVal stepId As Long) As Hashtable
        If Not Cache.Workflows.hsStepsColors.ContainsKey(stepId) Then
            Dim dtAux As DataTable = WFStepBusiness.getTypesOfPermit(stepId, TypesofPermits.Expired)

            If dtAux IsNot Nothing Then
                Dim daysAndColors As Hashtable = New Hashtable()
                For Each row As DataRow In dtAux.Rows
                    daysAndColors.Add(row("OBJTWO").ToString(), row("OBJEXTRADATA").ToString())
                Next
                dtAux.Dispose() 'ver referencia
                dtAux = Nothing

                SyncLock (Cache.Workflows.hsStepsColors)
                    Cache.Workflows.hsStepsColors.Add(stepId, daysAndColors)
                End SyncLock
            End If
        End If
        Return Cache.Workflows.hsStepsColors.Item(stepId)
    End Function


#Region "Get"
    Public Shared Function GetUserSteps() As SortedList
        Try
            Dim SL As New SortedList
            Dim StrSelect As String = "Select * from wfstep"
            Dim where As String
            '            For Each wfc As WorkFlow In WFs
            '            where += "work_id=" & wfc.Id & " or "
            '            Next
            'borro el ultimo 'or' de mas
            '  where = where.Substring(0, where.Length - 3)

            where = " where wfstep.step_id in (select aditional from USR_RIGHTS where (groupid in (" & Zamba.Membership.MembershipHelper.CurrentUser.ID & ","

            For Each g As IUserGroup In Zamba.Membership.MembershipHelper.CurrentUser.Groups
                where += g.ID & ","
            Next
            where.Remove(where.Length - 1, 1)
            where += ")) and objid=42 and rtype=19)"


            Dim Dstemp As DataSet
            Dim DsSteps As New DsSteps
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect & where)
            Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
            DsSteps.Merge(Dstemp)

            Dim DsStates As DsStepState = WFStepStatesFactory.GetAllStates(DsSteps)

            '      For Each wf As WorkFlow In WFs
            '            Dim Rows() As DsSteps.WFStepsRow = DsSteps.WFSteps.Select("work_id=" & wf.Id)
            For Each r As DsSteps.WFStepsRow In DsSteps.WFSteps
                Try
                    'Dim ImagePath As String = ""
                    'Try
                    '    ImagePath = r.ImagePath
                    'Catch ex As Exception
                    'End Try
                    'Dim wfstep As New wfstep(Nothing, r.Step_Id, r.Name, r.Help, r.Description, New Drawing.Point(r.LocationX, r.LocationY), r.ImageIndex, r.Max_Docs, r.Max_Hours, r.StartAtOpenDoc, ImagePath)
                    Dim WFStep As New WFStep(Nothing, r.Step_Id, r.Name, r.Help, r.Description, New Drawing.Point(r.LocationX, r.LocationY), r.ImageIndex, r.Max_Docs, r.Max_Hours, r.StartAtOpenDoc, "", 50, 150, 0, 0)
                    SL.Add(WFStep.ID, WFStep)
                    WFStepStatesFactory.FillState(WFStep, DsStates)
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
            Next
            '     Next
            Return SL
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function
#End Region

#Region "ABM STEPS"
    Public Shared Function GetStep(ByVal Wf As WorkFlow, ByVal StepId As Int32) As WFStep
        Dim Dstemp As DataSet
        Dim DsSteps As New DsSteps
        Dim Strselect As String = "Select * from wfstep where step_id = " & StepId
        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)

        Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
        DsSteps.Merge(Dstemp)

        Dim Wfstep As WFStep = New WFStep(Wf.ID, DsSteps.WFSteps(0).Step_Id, DsSteps.WFSteps(0).Name, DsSteps.WFSteps(0).Help, DsSteps.WFSteps(0).Description, New Drawing.Point(DsSteps.WFSteps(0).LocationX, DsSteps.WFSteps(0).LocationY), DsSteps.WFSteps(0).ImageIndex, DsSteps.WFSteps(0).Max_Docs, DsSteps.WFSteps(0).Max_Hours, DsSteps.WFSteps(0).StartAtOpenDoc, "", 50, 150, 0, 0)

        Dim DsStates As DsStepState = WFStepStatesFactory.GetAllStates(DsSteps)
        WFStepStatesFactory.FillState(Wfstep, DsStates)

        Return Wfstep
    End Function
    Public Shared Function GetSteps(ByVal WF As WorkFlow) As SortedList
        Dim Dstemp As DataSet
        Dim DsSteps As New DsSteps

        If Server.isOracle Then
            Dim ParValues() As Object = {WF.ID, 2}
            'Dim ParNames() As Object = {"pWFId", "io_cursor"}
            '' Dim parTypes() As Object = {13, 5}
            Dstemp = Server.Con.ExecuteDataset("ZWFStepsFactory_pkg.ZGetViewWFStepsByWfID", ParValues)
        Else
            Dim ParValues() As Object = {WF.ID}
            Dstemp = Server.Con.ExecuteDataset("ZGetViewWFStepsByWfID", ParValues)
        End If

        Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
        DsSteps.Merge(Dstemp)
        Dim Steps As New SortedList
        Dim i As Int32

        For i = 0 To DsSteps.WFSteps.Count - 1
            Dim Wfstep As WFStep = New WFStep(WF.ID, DsSteps.WFSteps(i).Step_Id, DsSteps.WFSteps(i).Name, DsSteps.WFSteps(i).Help, DsSteps.WFSteps(i).Description, New Drawing.Point(DsSteps.WFSteps(i).LocationX, DsSteps.WFSteps(i).LocationY), DsSteps.WFSteps(i).ImageIndex, DsSteps.WFSteps(i).Max_Docs, DsSteps.WFSteps(i).Max_Hours, DsSteps.WFSteps(i).StartAtOpenDoc, "", 50, 150, 0, 0)
            Dim DsStates As DsStepState = WFStepStatesFactory.GetAllStates(DsSteps)
            WFStepStatesFactory.FillState(Wfstep, DsStates)
            Steps.Add(DsSteps.WFSteps(i).Step_Id, Wfstep)
        Next
        Return Steps
    End Function

    Public Shared Sub GetStepsIdName(ByRef h As SortedList)
        Try
            Dim StrSelect As String = "Select distinct step_id,name from wfstep,USR_RIGHTS"
            Dim where As String = ""

            If Not Zamba.Membership.MembershipHelper.CurrentUser.Groups.Count < 1 Then
                For Each g As IUserGroup In Zamba.Membership.MembershipHelper.CurrentUser.Groups
                    where += "USR_RIGHTS.groupid=" & g.ID & " or "
                Next
            End If

            where += "USR_RIGHTS.groupid=" & Zamba.Membership.MembershipHelper.CurrentUser.ID

            where = " where (" & where & ") and wfstep.step_id=USR_RIGHTS.aditional order by 2"

            Dim Dstemp As DataSet
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect & where)

            For i As Int32 = 0 To Dstemp.Tables(0).Rows.Count - 1
                h.Add(CInt(Dstemp.Tables(0).Rows(i).Item(0)), Dstemp.Tables(0).Rows(i).Item(1))
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para obtener Table con Id y Nombre de Etapas por usuario y sus permisos. 
    ''' </summary>
    ''' <param name="user">Usuario del que se quieren ver etapas.</param>
    ''' <returns>Tabla con ID y Nombre de las etapas.</returns>
    Public Function GetWFsAndStepsAndCountByUser(userId As Long) As DataTable

        Dim stepsTable As DataTable
        stepsTable = WFStepsFactory.GetWFAndStepIdsAndNamesAndTaskCount(userId)

        Return stepsTable

    End Function

    ''' <summary>
    ''' Método que sirve para obtener las etapas según un id de workflow
    ''' </summary>
    ''' <param name="WF"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <remarks></remarks>
    ''' 	[Gaston]	06/06/2008	Modified    ' Se comento el procedimiento almacenado (inexistente) de Oracle y se agrego una consulta en el código
    ''' </history>
    Public Shared Function GetStepsDictionary(ByVal WF As WorkFlow) As SortedList
        Dim Dstemp As DataSet
        Dim DsSteps As New DsSteps

        'SP 29/12/05
        If Server.isOracle Then
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM WFStep where WORK_ID = " & WF.ID)
        Else
            Dim ParValues() As Object = {WF.ID}
            Dstemp = Server.Con.ExecuteDataset("ZGetViewWFStepsByWfID", ParValues)
        End If

        Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
        DsSteps.Merge(Dstemp)
        Dim Steps As New SortedList
        Dim wfStep As WFStep
        Dim i As Int32

        For i = 0 To DsSteps.WFSteps.Count - 1
            wfStep = New WFStep(WF.ID, DsSteps.WFSteps(i).Step_Id, DsSteps.WFSteps(i).Name, DsSteps.WFSteps(i).Help, DsSteps.WFSteps(i).Description, New Drawing.Point(DsSteps.WFSteps(i).LocationX, DsSteps.WFSteps(i).LocationY), DsSteps.WFSteps(i).ImageIndex, DsSteps.WFSteps(i).Max_Docs, DsSteps.WFSteps(i).Max_Hours, DsSteps.WFSteps(i).StartAtOpenDoc, "", 50, 150, 0, 0)
            Steps.Add(wfStep.ID, wfStep)
        Next
        Return Steps

    End Function
#End Region

    Public Shared Sub UpdateExpiredDateTask(ByRef Result As TaskResult)
        Dim strupdate As String = "UPDATE WFDOCUMENT SET EXPIREDATE=" & Server.Con.ConvertDateTime(Result.ExpireDate) & " WHERE DOC_ID = " & Result.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
    End Sub
    Public Shared Function GetTasksCount(ByVal StepId As Int32) As Int32
        Dim Count As Int32

        If Server.isOracle Then
            Dim ParValues() As Object = {StepId, 2}
            'Dim ParNames() As Object = {"pStepId", "io_cursor"}
            '' Dim parTypes() As Object = {13, 5}
            Count = Server.Con.ExecuteScalar("ZWFStepsFactory_pkg.ZGetDCountbyStepId", ParValues)
        Else
            Dim ParValues() As Object = {StepId}
            Count = Server.Con.ExecuteScalar("zsp_workflow_100_GetDocCountByStepId", ParValues)
        End If

        Return Count
    End Function
    Public Shared Function CheckTransicions(ByRef wfstep As WFStep) As ArrayList
        Dim Destiny As New ArrayList
        Dim DestinySteps As New ArrayList

        If IsNothing(wfstep.Rules) = False Then
            For Each R As WFRuleParent In wfstep.Rules
                If IsNothing(R) = False Then CheckRules(R, Destiny)
            Next
        End If

        For Each R As WFRuleParent In Destiny
            DestinySteps.Add(DirectCast(R, Object).newwfstep)
        Next
        Return DestinySteps
    End Function
    Private Shared Sub CheckRules(ByRef wfrule As WFRuleParent, ByVal Destiny As ArrayList)
        If wfrule.Name.ToLower = "dodistribuir" Then
            Destiny.Add(wfrule)
        Else
            'For Each R As WFRuleParent In wfrule.ChildRules
            '    CheckRules(R, Destiny)
            'Next
        End If
    End Sub

    Public Shared Sub UpdateInitialStep(ByVal WF As WorkFlow)
        Dim sql As String = "update WFWorkflow set InitialStepId=" & WF.InitialStep.ID & " where work_id=" & WF.ID
        sql = Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub

End Class
