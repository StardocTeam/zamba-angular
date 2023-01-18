Imports Zamba.Core
Imports Zamba.Data
Imports System.Collections.Generic
Imports Zamba.Servers


Public Class WFStepBusiness

    Private Shared dsStepNames As DataSet = Nothing

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
    '--------------------------------------------------------
    'osanchez - 150409
    'Retorna un wfstep a partir de un workflow.
    'Utiliza el workflow del cache
    '--------------------------------------------------------
    Public Shared Function GetStepByWorkIdAndStepId(ByVal wfid As Int64, ByVal wfstepid As Int64) As IWFStep
        Dim wf As IWorkFlow
        wf = WFBusiness.GetWFbyId(wfid)
        Return wf.Steps.Item(Convert.ToDecimal(wfstepid))
    End Function
    '--------------------------------------------------------

    ''' <summary>
    ''' Obtiene la etapa en la que esta la regla
    ''' </summary>
    ''' <param name="RuleId">ID de la regla</param>
    ''' <param name="useCache">Utilizar cache</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetStepIdByRuleId(ByVal RuleId As Int64, ByVal useCache As Boolean) As Int64
        If useCache = True AndAlso Cache.Workflows.HSRules.Contains(RuleId) Then
            Return Cache.Workflows.HSRules.GetByRuleID(RuleId).WFStepId
        End If

        Return WFStepsFactory.GetStepIdByRuleId(RuleId)
    End Function


    Public Shared Function GetStepsByWorkflow(ByVal workflowId As Int64) As List(Of IWFStep)
        Dim StepList As New List(Of IWFStep)

        Dim ds As DataSet = WFStepsFactory.GetStepsByWorkflow(workflowId)

        If (Not IsNothing(ds) And ds.Tables.Count > 0) Then

            For Each CurrentRow As DataRow In ds.Tables(0).Rows
                StepList.Add(WFStepBusiness.Build(CurrentRow))
            Next
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

            If (Not IsNothing(dsSteps) And dsSteps.Tables.Count > 0) Then

                For Each CurrentRow As DataRow In dsSteps.Tables(0).Rows
                    StepList.Add(WFStepBusiness.Build(CurrentRow))
                Next
            End If
        End If

        Return StepList
    End Function

    'Friend Shared Sub FillUserStepsWithTaskCount(ByVal WFs() As WorkFlow)

    '    For Each wf As WorkFlow In WFs

    '        WFStepsFactory.FillStepsWithTaskCount(wf, Membership.MembershipHelper.CurrentUser.ID)

    '    Next

    'End Sub

    'Friend Shared Sub FillUserSteps(ByVal WFs() As WorkFlow, ByVal withcache As Boolean)
    '    'WFStepsFactory.FillUserSteps(WFs)
    '    For Each wf As WorkFlow In WFs
    '        FillSteps(wf, withcache)
    '        ' For Each s As WFStep In wf.Steps.Values
    '        '    FillUsersAndGroups(s)
    '        'Next
    '    Next
    'End Sub

    Friend Shared Sub FillSteps(ByVal WF As WorkFlow, ByVal WithCache As Boolean)
        Dim dssteps As DsSteps

        If Not WithCache Then
            dssteps = WFStepsFactory.FillSteps(WF)
        Else
            SyncLock (Cache.Workflows.hsWFStepsDS)

                If Not Cache.Workflows.hsWFStepsDS.ContainsKey(WF.ID) Then
                    Cache.Workflows.hsWFStepsDS.Add(WF.ID, WFStepsFactory.FillSteps(WF))
                End If
                dssteps = DirectCast(Cache.Workflows.hsWFStepsDS.Item(WF.ID), DsSteps)

            End SyncLock
        End If

        Dim DsStates As DsStepState

        If Not WithCache Then
            DsStates = WFStepStatesFactory.GetAllStates(dssteps)
        Else
            SyncLock (Cache.Workflows.hsWFStepStatesDS)

                If Not Cache.Workflows.hsWFStepStatesDS.ContainsKey(WF.ID) Then
                    Cache.Workflows.hsWFStepStatesDS.Add(WF.ID, WFStepStatesFactory.GetAllStates(dssteps))
                End If
                DsStates = DirectCast(Cache.Workflows.hsWFStepStatesDS.Item(WF.ID), DsStepState)
            End SyncLock
        End If

        For Each r As DsSteps.WFStepsRow In dssteps.WFSteps.Rows
            Try
                Dim wfstep As WFStep = GetStepById(r.Step_Id, Not WithCache)
                'Dim wfstep As New WFStep(WF.ID, r.Step_Id, r.Name, r.Help, r.Description, New Drawing.Point(r.LocationX, r.LocationY), r.ImageIndex, r.Max_Docs, r.Max_Hours, r.StartAtOpenDoc, r.Color, r.Width, r.Height, 0, 0)
                If WF.Steps.ContainsKey(wfstep.ID) Then
                    WF.Steps(wfstep.ID) = wfstep
                Else
                    WF.Steps.Add(wfstep.ID, wfstep)
                End If
                'WFStepStatesFactory.FillState(WF.Steps(wfstep.ID), DsStates)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        Next
        'Seteo Estapa Inicial
        WF.SetInitialStep()
    End Sub

    ''' <summary>
    ''' Obtiene los nombres y id de las etapas
    ''' </summary>
    ''' <param name="WorkId">Id del workflow</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetOrderedWFAndStepIdsAndNames(ByVal WorkId As Int64) As DataTable
        Return WFStepsFactory.GetOrderedWFAndStepIdsAndNames(WorkId).Tables(0)
    End Function

    'Public Shared Function GetDsStepsByWFIdAndUserId(ByVal WFId As Int32, ByVal userid As Int64) As DsSteps
    '    Return WFStepsFactory.GetDsStepsByWFIdAndUserId(WFId, userid)
    'End Function
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
    ''' Devuelve una lista con los steps del WF
    ''' </summary>
    ''' <param name="WFId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Shared Function GetHashTableSteps(ByVal WFId As Int64, ByVal UserId As Int64) As Hashtable
    '    If steps.ContainsKey(WFId) Then
    '        Return steps(WFId)
    '    Else
    '        Dim ds As DataSet = WFStepsFactory.GetDsStepsByWFIdAndUserId(WFId, UserId)
    '        If Not IsNothing(ds) Then
    '            If ds.Tables.Count > 0 Then
    '                Dim hs As Hashtable = New Hashtable()
    '                For Each dr As DataRow In ds.Tables(0).Rows
    '                    hs.Add(dr(0), dr("Name"))
    '                Next
    '                Return hs
    '            End If
    '        End If
    '    End If
    '    Return Nothing
    'End Function

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
            Cache.Workflows.hsSteps = New Hashtable()
        End If
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

        Return StepList
    End Function

    'Public Shared Function GetSteps(ByVal stepId As Int64) As List(Of IWFStep)
    '    Dim StepList As New List(Of IWFStep)
    '    Dim ds As DataSet = WFStepsFactory.GetStepDs(stepId)

    '    If Not IsNothing(ds) AndAlso ds.Tables.Count = 1 AndAlso ds.Tables(0).Rows.Count = 1 Then
    '        StepList.Add(WFStepBusiness.Build(ds.Tables(0).Rows(0)))
    '    End If

    '    Return StepList
    'End Function

    'Public Shared Function GetStep(ByVal stepId As Int64) As IWFStep
    '    Dim CurrentStep As IWFStep = Nothing
    '    'Dim ds As DataSet = WFStepsFactory.GetStepDs(stepId)

    '    'If Not IsNothing(ds) AndAlso ds.Tables.Count = 1 Then
    '    '    CurrentStep = WFStepBusiness.Build(ds.Tables(0).Rows(0))
    '    'End If
    '    CurrentStep = WFStepsFactory.GetStepById(stepId)
    '    Return CurrentStep
    'End Function

    'Public Shared Function GetStepStates(ByVal StepId As Int32, ByVal WfId As Int32) As SortedList
    '    If StepId = 0 OrElse WfId = 0 Then Return Nothing

    '    Dim Wf As WorkFlow = Nothing
    '    For Each F As DsWF.WFRow In WFBusiness.GetAllWorkflows.WF
    '        If F.Item(0) = WfId Then
    '            Wf = WFBusiness.GetWf(F)
    '            WFBusiness.GetFullMonitorWF(Wf)
    '            Exit For
    '        End If
    '    Next

    '    If IsNothing(Wf) = False Then

    '        For Each WfStep As WFStep In Wf.Steps.Values
    '            If WfStep.ID = StepId Then
    '                Return WfStep.States
    '                Exit For
    '            End If
    '        Next
    '    End If
    '    Return Nothing
    'End Function

    ''' <summary>
    ''' Obtiene la etapa por el id
    ''' </summary>
    ''' <param name="StepId">Id de la etapa</param>
    ''' <param name="isReload">True para forzar la carga</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetStepById(ByVal StepId As Int64, Optional ByVal isReload As Boolean = False) As IWFStep
        SyncLock (Cache.Workflows.hsSteps)
            If isReload Then
                If Cache.Workflows.hsSteps.ContainsKey(StepId) Then
                    Cache.Workflows.hsSteps.Remove(StepId)
                End If
            End If

            If Cache.Workflows.hsSteps.ContainsKey(StepId) Then
                Return Cache.Workflows.hsSteps(StepId)
            Else
                Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo etapa: " & StepId)
                Dim ds As DataSet = WFStepsFactory.GetStepById(StepId)

                If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Dim dr As DataRow = ds.Tables(0).Rows(0)
                    Dim newStep As WFStep = New WFStep(dr("work_id"), StepId, dr("Name"), dr("Help"), dr("Description"), New Drawing.Point(dr("LocationX"), dr("LocationY")), dr("ImageIndex"), dr("Max_Docs"), dr("Max_Hours"), dr("StartAtOpenDoc"), dr("color"), dr("width"), dr("Height"), 0, 0)
                    dr = Nothing

                    newStep.States = WFStepStatesComponent.GetStepStatesByStepId(StepId, isReload)
                    newStep.InitialState = WFStepStatesComponent.getInitialStateFromList(newStep.States)

                    WFRulesBusiness.GetRuleOptionsDT(True, newStep.ID)

                    Dim rules As DsRules = WFRulesBusiness.GetRulesDSByStepID(StepId, False) 'WFRulesBusiness.GetCompleteHashTableRulesByStep(newStep.ID, isReload)
                    If Not IsNothing(rules) Then
                        newStep.DsRules = rules
                    End If
                    If Cache.Workflows.hsSteps.ContainsKey(StepId) = False Then Cache.Workflows.hsSteps.Add(StepId, newStep)
                End If
            End If
            Return Cache.Workflows.hsSteps(StepId)
        End SyncLock
    End Function

    ''' <summary>
    ''' Obtiene la etapa por el id
    ''' </summary>
    ''' <param name="StepId">Id de la etapa</param>
    ''' <param name="isReload">True para forzar la carga</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Shared Function GetStepByStepRow(ByVal StepR As DsSteps.WFStepsRow, Optional ByVal isReload As Boolean = False) As IWFStep

    '    SyncLock (Cache.Workflows.hsSteps)
    '        If isReload Then
    '            If Cache.Workflows.hsSteps.ContainsKey(StepR.Step_Id) Then
    '                Cache.Workflows.hsSteps.Remove(StepR.Step_Id)
    '            End If
    '        End If

    '        If Cache.Workflows.hsSteps.ContainsKey(StepR.Step_Id) Then
    '            Return Cache.Workflows.hsSteps(StepR.Step_Id)
    '        Else

    '            Dim newStep As WFStep = New WFStep(StepR.Work_Id, StepR.Step_Id, StepR.Name, StepR.Help, StepR.Description, New Drawing.Point(StepR.LocationX, StepR.LocationY), StepR.ImageIndex, StepR.Max_Docs, StepR.Max_Hours, StepR.StartAtOpenDoc, StepR.Color, StepR.Height, StepR.Width, 0, 0)
    '            newStep.States = WFStepStatesComponent.GetStepStatesByStepId(StepR.Step_Id, isReload)

    '            newStep.InitialState = WFStepStatesComponent.getInitialStateFromList(newStep.States)

    '            Dim rules As DsRules = WFRulesBusiness.GetCompleteHashTableRulesByStep(newStep.ID, isReload)
    '            If Not IsNothing(rules) Then
    '                SyncLock (newStep)
    '                    newStep.DsRules = rules
    '                    '   newStep.Rules.AddRange(rules)
    '                End SyncLock
    '            End If
    '            If Cache.Workflows.hsSteps.ContainsKey(StepR.Step_Id) = False Then Cache.Workflows.hsSteps.Add(StepR.Step_Id, newStep)
    '        End If
    '        Return Cache.Workflows.hsSteps(StepR.Step_Id)
    '    End SyncLock

    'End Function

    Public Shared Function GetStepByTaskId(ByVal TaskId As Int64) As IWFStep
        Dim stepid As Int64 = WFStepsFactory.GetStepIdByTaskId(TaskId)
        Return GetStepById(stepid, False)
    End Function
    Public Shared Function GetStepIdByTaskId(ByVal TaskId As Int64) As Int64
        Return WFStepsFactory.GetStepIdByTaskId(TaskId)
    End Function

    ''' <summary>
    ''' Obtiene las id de tipos de documento que se encuentran en una etapa.
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Ezequiel] 14/09/2009  Created.
    '''     [Javier]   14/10/2010  Deleted. Se utiliza GetDocTypesAssociatedToWFbyStepId ya que trae los doctypes asociados a un WF
    ''' </history>
    'Public Shared Function GetDocTypesByWfStep(ByVal stepId As Int64) As ArrayList
    '    If Cache.Workflows.hsWFStepId.Contains(stepId) = False Then
    '        Dim ar As New ArrayList
    '        Dim dtids As DataTable = WFStepsFactory.GetDocTypesByWfStep(stepId)
    '        For Each r As DataRow In dtids.Rows
    '            ar.Add(r(0))
    '        Next
    '        Cache.Workflows.hsWFStepId.Add(stepId, ar)
    '    End If
    '    Return Cache.Workflows.hsWFStepId(stepId)
    'End Function


    ''' <summary>
    ''' Obtiene los tipos de documento para la etapa correspondiente
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDocTypesByWfStepAsDT(ByVal stepId As Int64) As DataTable
        SyncLock Cache.DocTypesAndIndexs.hsDocTypesWF
            If Cache.DocTypesAndIndexs.hsDocTypesWF.ContainsKey(stepId) = False Then
                Cache.DocTypesAndIndexs.hsDocTypesWF.Add(stepId, WFStepsFactory.GetDocTypesByWfStep(stepId))
                'Si no hay tareas en la etapa y luego se distribuye una tarea ahi, se recarga la cache
            ElseIf IsNothing(Cache.DocTypesAndIndexs.hsDocTypesWF(stepId)) Then
                Cache.DocTypesAndIndexs.hsDocTypesWF(stepId) = WFStepsFactory.GetDocTypesByWfStep(stepId)
                'Si no hay tareas en la etapa y luego se distribuye una tarea ahi, se recarga la cache
            ElseIf DirectCast(Cache.DocTypesAndIndexs.hsDocTypesWF(stepId), DataTable).Rows.Count = 0 Then
                Cache.DocTypesAndIndexs.hsDocTypesWF(stepId) = WFStepsFactory.GetDocTypesByWfStep(stepId)
            End If
            Return Cache.DocTypesAndIndexs.hsDocTypesWF(stepId)
        End SyncLock
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
    Public Shared Function GetDocTypesAssociatedToWFbyStepId(ByVal stepId As Int64) As ArrayList
        SyncLock (Cache.Workflows.hsDocTypesAssWFbyStepId)
            If Cache.Workflows.hsDocTypesAssWFbyStepId.Contains(stepId) = False Then
                Dim ar As New ArrayList
                Dim dtids As DataTable = WFStepsFactory.GetDocTypesAssociatedToWFbyStepId(stepId)
                For Each r As DataRow In dtids.Rows
                    ar.Add(r(0))
                Next
                Cache.Workflows.hsDocTypesAssWFbyStepId.Add(stepId, ar)
            End If
            Return Cache.Workflows.hsDocTypesAssWFbyStepId(stepId)
        End SyncLock
    End Function

    ''' <summary>
    ''' Devuelve el nombre de una etapa por su ID
    ''' </summary>
    ''' <param name="StepId">ID de la etapa</param>
    ''' <history> Marcelo Modified 18/01/10</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetStepNameById(ByVal StepId As Int64) As String
        If (Cache.Workflows.hsSteps.Contains(StepId)) Then
            Return DirectCast(Cache.Workflows.hsSteps(StepId), WFStep).Name
        Else
            Return WFStepsFactory.GetStepNameById(StepId)
        End If
    End Function

    Public Shared Function GetTasksConsumedMinutes(ByVal stepId As Int64) As List(Of Int32)
        Dim DtConsumedMinutes As DataTable = WFStepsFactory.GetTasksConsumedMinutes(stepId)
        Dim ConsumedMinutes As New List(Of Int32)

        If Not IsNothing(DtConsumedMinutes) Then
            For Each CurrentRow As DataRow In DtConsumedMinutes.Rows
                ConsumedMinutes.Add(CurrentRow.Item(0))
            Next
        End If

        Return ConsumedMinutes
    End Function


#End Region

#Region "Update"
    Public Shared Sub UpdateStep(ByRef wfstep As IWFStep)
        Try
            WFStepsFactory.UpdateStep(wfstep)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Sub UpdateStepColor(ByVal color As String, ByVal ID As Long)
        Try
            Zamba.Data.WFStepsFactory.UpdateStepColor(color, ID)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Add"
    Public Shared Sub AddStep(ByVal NewWFStep As IWFStep, ByVal WF As IWorkFlow)
        WF.Steps.Add(NewWFStep.ID, NewWFStep)
        WFStepsFactory.InsertStep(NewWFStep)
    End Sub


#End Region

#Region "Users"
    Public Shared Function IsUserIdAsignedToStep(ByVal StepId As Int64, ByVal userid As Int32) As Boolean
        Return RightsBusiness.GetUserRights(Zamba.Core.UserBusiness.GetUserById(userid), Zamba.Core.ObjectTypes.WFSteps, Zamba.Core.RightsType.Use, StepId)
    End Function



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

    Public Shared Function GetStepUserGroupsIdsAndNames(ByRef wfstepid As Int64, ByVal ReLoad As Boolean) As Generic.List(Of IZBaseCore)
        If Not StepsUserGroupsIdsAndNames.ContainsKey(wfstepid) Then
            LoadStepUserGroupsIdsAndNames(wfstepid)
        ElseIf ReLoad Then
            StepsUserGroupsIdsAndNames.Remove(wfstepid)
            LoadStepUserGroupsIdsAndNames(wfstepid)
        End If

        Return StepsUserGroupsIdsAndNames(wfstepid)
    End Function

    Public Shared Function GetStepUserGroupsIdsAsDS(ByVal wfstepid As Int64, ByVal userID As Int64) As DataTable
        Return WFStepsFactory.GetStepUserGroupsIdsByUserID(wfstepid, userID)
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

#Region "Remove"
    Friend Shared Sub Remove(ByVal StepNode As EditStepNode)
        If WFStepsFactory.HasDocuments(StepNode.WFStep.ID) Then 'The Step HAS assocuated documents
            If MessageBox.Show(" La etapa seleccionada tiene documentos asociados ,  ¿Desea eliminarla de todas formas?   ", "Edicion de Reglas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If
        Else 'The Step has NO associated documents
            If MessageBox.Show("  ¿ Desea eliminar la Etapa?  ", "Edicion de Reglas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If
        End If

        'WFStepsFactory.DelStep(StepNode.WFStep)
        WFStepBusiness.DelStep(StepNode.WFStep)
        Dim wf As IWorkFlow
        'wf = WFBusiness.GetWorkFlowByID(StepNode.WFStep.WorkId)
        'wf.Steps.Remove(Decimal.Parse(46))
        wf = DirectCast(StepNode.Parent, WFNode).WorkFlow
        wf.Steps.Remove(StepNode.WFStep.ID)
        'StepNode.Remove()
        StepNode.TreeView.SelectedNode.Remove()

    End Sub
    Public Shared Sub DelStep(ByRef wfstep As IWFStep)
        Try
            WFStepsFactory.DelStep(wfstep)

            'Guardo log de baja
            Dim userId As Integer = Membership.MembershipHelper.CurrentUser.ID
            UserBusiness.Rights.SaveAction(userId, ObjectTypes.WFSteps, RightsType.Delete, "Se ha eliminado la etapa '" & wfstep.Name & "'")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Initial"
    Friend Shared Sub SetInitial(ByVal StepNode As EditStepNode, Optional ByVal ask As Boolean = True)
        Try
            If ask = False OrElse MessageBox.Show("¿Desea poner la etapa seleccionada como inicial?", "Edicion de Etapas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim wf As IWorkFlow
                wf = WFBusiness.GetWFbyId(StepNode.WFStep.WorkId)
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
        End Try
    End Sub
#End Region

    ''' <summary>
    ''' Actualiza e orden de la etapa
    ''' </summary>
    ''' <param name="StepID">ID de la etapa</param>
    ''' <param name="Orden">Orden de la etapa</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateStepOrder(ByVal StepId As Int64, ByVal Orden As Int64)
        WFStepsFactory.UpdateStepOrder(StepId, Orden)
    End Sub

    ''' <summary>
    ''' Actualiza la etapa para poner el valor maximo de orden
    ''' </summary>
    ''' <param name="StepID">ID de la etapa</param>
    ''' <param name="WorkID">ID del workflow</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateMaxStepOrder(ByVal StepId As Int64, ByVal WorkId As Int64)
        WFStepsFactory.UpdateMaxStepOrder(StepId, WorkId)
    End Sub

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
    Public Shared Function getTypesOfPermit(ByVal stepId As Long, ByVal withcache As Boolean, ByVal typeOfPermision As TypesofPermits) As DataTable
        If Not withcache Then Return WFStepsFactory.getTypesOfPermit(stepId, typeOfPermision).Tables(0)

        SyncLock (Cache.Workflows.hsStepsOpt)
            If Not Cache.Workflows.hsStepsOpt.ContainsKey(stepId & " - " & typeOfPermision) Then
                Cache.Workflows.hsStepsOpt.Add(stepId & " - " & typeOfPermision, WFStepsFactory.getTypesOfPermit(stepId, typeOfPermision).Tables(0))
            End If

            Return Cache.Workflows.hsStepsOpt(stepId & " - " & typeOfPermision)
        End SyncLock
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
    Public Shared Function GetStepColors(ByVal stepId As Long) As Hashtable
        SyncLock (Cache.Workflows.hsStepsColors)
            If Not Cache.Workflows.hsStepsColors.ContainsKey(stepId) Then
                Dim dtAux As DataTable = WFStepBusiness.getTypesOfPermit(stepId, True, TypesofPermits.Expired)
                Dim daysAndColors As Hashtable = New Hashtable()
                For Each row As DataRow In dtAux.Rows
                    daysAndColors.Add(row("OBJTWO").ToString(), row("OBJEXTRADATA").ToString())
                Next
                Cache.Workflows.hsStepsColors.Add(stepId, daysAndColors)
            End If
            Return Cache.Workflows.hsStepsColors.Item(stepId)
        End SyncLock
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

            where = " where wfstep.step_id in (select aditional from USR_RIGHTS where (groupid=" & Membership.MembershipHelper.CurrentUser.ID

            For Each g As IUserGroup In Membership.MembershipHelper.CurrentUser.Groups
                where += " or groupid=" & g.ID
            Next
            where += ") and objid=42 and rtype=19)"


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

    Public Shared Sub DelStep(ByRef wfstep As WFStep)
        Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE wfSTEP where STEP_id = " & wfstep.ID)

        'SP 29/12/05
        'Try
        '    Dim ParValues() As Object = {WFStep}
        '    Dim ParNames() As Object = {"pStepId"}
        '    Dim ParTypes() As Object = {13}
        '    if Server.IsOracle then
        '        Server.Con.ExecuteNonQuery("ZWFDelStepsFactory_pkg.ZDelWFStepByStepId",  ParValues)
        '    Else
        '        Server.Con.ExecuteNonQuery("ZDelWFStepByStepId", ParValues)
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try

    End Sub

    ''' <summary>
    ''' Método que sirve para actualizar los datos de una etapa
    ''' </summary>
    ''' <param name="wfstep"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/06/2008	Modified    
    ''' </history>
    Public Shared Sub UpdateStep(ByRef wfstep As WFStep)

        'Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE WFSTEP set Name = '" & WFStep.Name & "',Description = '" & WFStep.Description & "',Help = '" & WFStep.Help & "',EditDate = " & Server.Con.ConvertDateTime(WFStep.EditDate.ToString) & ",ImageIndex = " & WFStep.IconId & ",LocationX = " & WFStep.Location.X & ", LocationY = " & WFStep.Location.Y & ",StartAtopenDoc = " & CInt(WFStep.StartAtOpenDoc) & ",Max_Hours = " & WFStep.MaxHours & ",Max_Docs = " & WFStep.MaxDocs & " where step_id = " & WFStep.Id)
        'SP 29/12/05

        'Dim ParNames() As Object = {"pName", "pDescription", "pHelp", "pEditDate", "pImgInd", "pLocX", "pLocY", "pStart", "pMaxHours", "pMaxDocs", "pStepId"}
        'Dim ParTypes() As Object = {7, 7, 7, 7, 13, 17, 17, 13, 13, 13, 13}

        If Server.IsOracle Then

            Dim query As New System.Text.StringBuilder

            If String.IsNullOrEmpty(wfstep.Description) Then
                wfstep.Description = "Descripcion"
            End If

            If String.IsNullOrEmpty(wfstep.Help) Then
                wfstep.Help = "Ayuda"
            End If

            query.Append("UPDATE WFSTEP set Name = '" & wfstep.Name & "', Description = '" & wfstep.Description & "', Help = '" & wfstep.Help & "', ")
            query.Append("EditDate = " & Server.Con.ConvertDate(wfstep.EditDate) & ", ImageIndex = " & wfstep.IconId & ", LocationX = " & wfstep.Location.X & ", ")
            query.Append("LocationY = " & wfstep.Location.Y & ", StartAtopenDoc = ")
            If wfstep.StartAtOpenDoc Then
                query.Append("1")
            Else
                query.Append("0")
            End If
            query.Append(", Max_Hours = " & wfstep.MaxHours & ", ")
            query.Append("Max_Docs = " & wfstep.MaxDocs & " where step_id = " & wfstep.ID)
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())
            'Server.Con.ExecuteNonQuery("ZWFUpdStepsFactory_pkg.ZUpdWFStepByStepId",  ParValues)

        Else

            Dim ParValues() As Object = {wfstep.Name, wfstep.Description, wfstep.Help, wfstep.EditDate, wfstep.IconId, wfstep.Location.X, wfstep.Location.Y, wfstep.StartAtOpenDoc, wfstep.MaxHours, wfstep.MaxDocs, wfstep.ID}
            Server.Con.ExecuteNonQuery("Zsp_workflow_100_UpdWFStepByStepId", ParValues)

        End If

    End Sub

    Public Shared Sub UpdateStepPosition(ByRef wfstep As WFStep)
        Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE WFSTEP set LocationX = " & wfstep.Location.X & ", LocationY = " & wfstep.Location.Y & " where step_id = " & wfstep.ID)
    End Sub
    Public Shared Sub UpdateStepSize(ByRef wfstep As WFStep)
        Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE WFSTEP set Width = " & wfstep.Width & ", Height = " & wfstep.Height & " where step_id = " & wfstep.ID)
    End Sub
    Public Shared Sub UpdateStepImage(ByRef wfstep As WFStep)
        'Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE WFSTEP set ImagePath = '" & WFStep.ImagePath & "' where step_id = " & WFStep.Id)
    End Sub
    Public Shared Function NewStep(ByVal WF As WorkFlow, ByVal Name As String, ByVal Help As String, ByVal Description As String, ByVal Location As Drawing.Point, ByVal ImageIndex As Int32, ByVal MaxDocs As Int32, ByVal MaxHours As Int32, ByVal StartAtOpenDoc As Boolean, ByVal ImagePath As String) As WFStep
        Dim StepId As Int32 = CoreData.GetNewID(Zamba.Core.IdTypes.WFSTEP)
        'Dim WFStep As New WFStep(WF, StepId, Name, Help, Description, Location, ImageIndex, MaxDocs, MaxHours, StartAtOpenDoc, ImagePath)
        Dim WFStep As New WFStep(WF.ID, StepId, Name, Help, Description, Location, ImageIndex, MaxDocs, MaxHours, StartAtOpenDoc, "", 50, 150, 0, 0)

        Dim sql As String = "INSERT INTO WFSTEP (work_id,STEP_ID,Name,Description,Help,CreateDate,EditDate,ImageIndex,LocationX, LocationY,Max_Docs,Max_Hours,StartAtOpenDoc) VALUES (" & WF.ID & "," & StepId & ",'" & Name & "','',''," & Server.Con.ConvertDateTime(Now.ToString) & "," & Server.Con.ConvertDateTime(Now.ToString) & "," & ImageIndex & "," & Location.X & "," & Location.Y & "," & MaxDocs & "," & MaxHours & "," & (StartAtOpenDoc) & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

        ''SP 29/12/05
        'Try
        '    Dim ParValues() As Object = {WF.Id, StepId, Name, "", "", Server.Con.ConvertDateTime(Now.ToString), Server.Con.ConvertDateTime(Now.ToString), ImageIndex, Location.X, Location.Y, MaxDocs, MaxHours, Integer.Parse(StartAtOpenDoc)}
        '    Dim ParNames() As Object = {"pWFId", "pStepId", "pName", "pDesc", "pHelp", "pCDate", "pEDate", "pImgInd", "pLocX", "pLocY", "pMaxDocs", "pMaxHours", "pStartAt"}
        '    Dim ParTypes() As Object = {13, 13, 7, 7, 7, 7, 7, 13, 17, 17, 13, 13, 13}
        '    if Server.IsOracle then
        '        Server.Con.ExecuteNonQuery("ZWFInsStepsFactory_pkg.ZInsWFStep",  ParValues)
        '    Else
        '        Server.Con.ExecuteNonQuery("ZInsWFStep", ParValues)
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try


        Return WFStep
    End Function

    Public Shared Sub InsertStep(ByVal WF As WorkFlow, ByVal Name As String, ByVal Help As String, ByVal Description As String, ByVal Location As Drawing.Point, ByVal ImageIndex As Int32, ByVal MaxDocs As Int32, ByVal MaxHours As Int32, ByVal StartAtOpenDoc As Boolean)
        Dim StepId As Int32 = CoreData.GetNewID(Zamba.Core.IdTypes.WFSTEP)
        Dim Initial As Byte
        If StartAtOpenDoc Then
            Initial = 1
        Else
            Initial = 0
        End If
        Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFSTEP (work_id,STEP_ID,Name,Description,Help,CreateDate,EditDate,ImageIndex,LocationX, LocationY,Max_Docs,Max_Hours,StartAtOpenDoc)VALUES (" & WF.ID & "," & StepId & ",'" & Name & "','" & Description & "','" & Help & "'," & Server.Con.ConvertDateTime(Now.ToString) & "," & Server.Con.ConvertDateTime(Now.ToString) & "," & ImageIndex & "," & Location.X & "," & Location.Y & "," & MaxDocs & "," & MaxHours & "," & Initial & ")")

        ''SP 29/12/05
        'Try
        '    Dim ParValues() As Object = {WF.Id, StepId, Name, Description, Help, Server.Con.ConvertDateTime(Now.ToString), Server.Con.ConvertDateTime(Now.ToString), ImageIndex, Location.X, Location.Y, MaxDocs, MaxHours, Initial}
        '    Dim ParNames() As Object = {"pWFId", "pStepId", "pName", "pDesc", "pHelp", "pCDate", "pEDate", "pImgInd", "pLocX", "pLocY", "pMaxDocs", "pMaxHours", "pStartAt"}
        '    Dim ParTypes() As Object = {13, 13, 7, 7, 7, 7, 7, 13, 17, 17, 13, 13, 13}
        '    if Server.IsOracle then
        '        Server.Con.ExecuteNonQuery("ZWFInsStepsFactory_pkg.ZInsWFStep",  ParValues)
        '    Else
        '        Server.Con.ExecuteNonQuery("ZInsWFStep", ParValues)
        '    End If
    End Sub

    ''' <summary>
    ''' Método que sirve para colocar una nueva etapa en el workflow
    ''' </summary>
    ''' <param name="wfstep"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/10/2008	Modified    Se agrega el estado "[Ninguno]" por defecto  a las etapa del workflow
    ''' </history>
    Public Shared Sub InsertStep(ByRef wfstep As WFStep)

        Dim Initial As Byte

        If wfstep.StartAtOpenDoc Then
            Initial = 1
        Else
            Initial = 0
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFSTEP (work_id,STEP_ID,Name,Description,Help,CreateDate,EditDate,ImageIndex,LocationX, LocationY,Max_Docs,Max_Hours,StartAtOpenDoc)VALUES (" & wfstep.WorkId & "," & wfstep.ID & ",'" & wfstep.Name & "','" & wfstep.Description & "','" & wfstep.Help & "'," & Server.Con.ConvertDateTime(Now.ToString) & "," & Server.Con.ConvertDateTime(Now.ToString) & ",0," & wfstep.Location.X & "," & wfstep.Location.Y & "," & wfstep.MaxDocs & "," & wfstep.MaxHours & "," & Initial & ")")

        ' Se agrega el estado por defecto "[Ninguno]" a la etapa
        Dim NewState As New WFStepState(ToolsBusiness.GetNewID(IdTypes.WFSTEPSTATE), wfstep.Name, "Estado por defecto", True)
        wfstep.States.Add(NewState)
        WFStepStatesBusiness.AddState(NewState.ID, NewState.Description, NewState.Name, 1, wfstep)

    End Sub


    Public Shared Function GetStep(ByVal Wf As WorkFlow, ByVal StepId As Int32) As WFStep
        'Dim StrSelect As String = "Select * from ZViewWFSTEPS where WORK_ID = " & WF.Id
        Dim Dstemp As DataSet
        Dim DsSteps As New DsSteps


        Dim Strselect As String = "Select * from wfstep where step_id = " & StepId
        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)

        Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
        DsSteps.Merge(Dstemp)
        'Dim ImagePath As String = ""
        'Try
        '    ImagePath = DsSteps.WFSteps(0).ImagePath
        'Catch ex As Exception
        'End Try
        'Dim Wfstep As Wfstep = New Wfstep(Wf, DsSteps.WFSteps(0).Step_Id, DsSteps.WFSteps(0).Name, DsSteps.WFSteps(0).Help, DsSteps.WFSteps(0).Description, New Drawing.Point(DsSteps.WFSteps(0).LocationX, DsSteps.WFSteps(0).LocationY), DsSteps.WFSteps(0).ImageIndex, DsSteps.WFSteps(0).Max_Docs, DsSteps.WFSteps(0).Max_Hours, DsSteps.WFSteps(0).StartAtOpenDoc, ImagePath)
        Dim Wfstep As WFStep = New WFStep(Wf.ID, DsSteps.WFSteps(0).Step_Id, DsSteps.WFSteps(0).Name, DsSteps.WFSteps(0).Help, DsSteps.WFSteps(0).Description, New Drawing.Point(DsSteps.WFSteps(0).LocationX, DsSteps.WFSteps(0).LocationY), DsSteps.WFSteps(0).ImageIndex, DsSteps.WFSteps(0).Max_Docs, DsSteps.WFSteps(0).Max_Hours, DsSteps.WFSteps(0).StartAtOpenDoc, "", 50, 150, 0, 0)

        Dim DsStates As DsStepState = WFStepStatesFactory.GetAllStates(DsSteps)
        WFStepStatesFactory.FillState(Wfstep, DsStates)

        Return Wfstep
    End Function
    Public Shared Function GetSteps(ByVal WF As WorkFlow) As SortedList
        'Dim StrSelect As String = "Select * from ZViewWFSTEPS where WORK_ID = " & WF.Id
        Dim Dstemp As DataSet
        Dim DsSteps As New DsSteps
        'Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

        'SP 29/12/05


        If Server.IsOracle Then
            Dim ParValues() As Object = {WF.ID, 2}
            Dim ParNames() As Object = {"pWFId", "io_cursor"}
            Dim ParTypes() As Object = {13, 5}
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
            'Dim ImagePath As String = ""
            'Try
            '    ImagePath = DsSteps.WFSteps(i).ImagePath
            'Catch ex As Exception
            'End Try
            'Dim Wfstep As Wfstep = New Wfstep(WF, DsSteps.WFSteps(i).Step_Id, DsSteps.WFSteps(i).Name, DsSteps.WFSteps(i).Help, DsSteps.WFSteps(i).Description, New Drawing.Point(DsSteps.WFSteps(i).LocationX, DsSteps.WFSteps(i).LocationY), DsSteps.WFSteps(i).ImageIndex, DsSteps.WFSteps(i).Max_Docs, DsSteps.WFSteps(i).Max_Hours, DsSteps.WFSteps(i).StartAtOpenDoc, ImagePath)
            Dim Wfstep As WFStep = New WFStep(WF.ID, DsSteps.WFSteps(i).Step_Id, DsSteps.WFSteps(i).Name, DsSteps.WFSteps(i).Help, DsSteps.WFSteps(i).Description, New Drawing.Point(DsSteps.WFSteps(i).LocationX, DsSteps.WFSteps(i).LocationY), DsSteps.WFSteps(i).ImageIndex, DsSteps.WFSteps(i).Max_Docs, DsSteps.WFSteps(i).Max_Hours, DsSteps.WFSteps(i).StartAtOpenDoc, "", 50, 150, 0, 0)
            Dim DsStates As DsStepState = WFStepStatesFactory.GetAllStates(DsSteps)
            WFStepStatesFactory.FillState(Wfstep, DsStates)
            Steps.Add(DsSteps.WFSteps(i).Step_Id, Wfstep)
        Next
        Return Steps
    End Function

    'Public Shared Function GetStepsIdName(ByRef h As SortedList) As SortedList
    Public Shared Sub GetStepsIdName(ByRef h As SortedList)
        Try
            Dim StrSelect As String = "Select distinct step_id,name from wfstep,USR_RIGHTS"
            Dim where As String = ""

            If Not Membership.MembershipHelper.CurrentUser.Groups.Count < 1 Then
                For Each g As IUserGroup In Membership.MembershipHelper.CurrentUser.Groups
                    where += "USR_RIGHTS.groupid=" & g.ID & " or "
                Next
            End If

            where += "USR_RIGHTS.groupid=" & Membership.MembershipHelper.CurrentUser.ID

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
    ''' Método que sirve para obtener las etapas según un id de workflow
    ''' </summary>
    ''' <param name="WF"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <remarks></remarks>
    ''' 	[Gaston]	06/06/2008	Modified    ' Se comento el procedimiento almacenado (inexistente) de Oracle y se agrego una consulta en el código
    ''' </history>
    Public Shared Function GetStepsDictionary(ByVal WF As WorkFlow) As SortedList

        'Dim StrSelect As String = "Select * from ZViewWFSTEPS where WORK_ID = " & WF.Id
        Dim Dstemp As DataSet
        Dim DsSteps As New DsSteps
        'Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

        'SP 29/12/05
        If Server.IsOracle Then
            'Dim ParValues() As Object = {WF.ID, 2}
            'Dim ParNames() As Object = {"pWFId", "io_cursor"}
            'Dim ParTypes() As Object = {13, 5}
            'Dstemp = Server.Con.ExecuteDataset("ZWFStepsFactory_pkg.ZGetViewWFStepsByWfID",  ParValues)
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
            'Dim imagepath As String = ""
            'Try
            '    imagepath = DsSteps.WFSteps(i).ImagePath
            'Catch ex As Exception
            'End Try
            'wfStep = New wfStep(WF, DsSteps.WFSteps(i).Step_Id, DsSteps.WFSteps(i).Name, DsSteps.WFSteps(i).Help, DsSteps.WFSteps(i).Description, New Drawing.Point(DsSteps.WFSteps(i).LocationX, DsSteps.WFSteps(i).LocationY), DsSteps.WFSteps(i).ImageIndex, DsSteps.WFSteps(i).Max_Docs, DsSteps.WFSteps(i).Max_Hours, DsSteps.WFSteps(i).StartAtOpenDoc, imagepath)
            wfStep = New WFStep(WF.ID, DsSteps.WFSteps(i).Step_Id, DsSteps.WFSteps(i).Name, DsSteps.WFSteps(i).Help, DsSteps.WFSteps(i).Description, New Drawing.Point(DsSteps.WFSteps(i).LocationX, DsSteps.WFSteps(i).LocationY), DsSteps.WFSteps(i).ImageIndex, DsSteps.WFSteps(i).Max_Docs, DsSteps.WFSteps(i).Max_Hours, DsSteps.WFSteps(i).StartAtOpenDoc, "", 50, 150, 0, 0)
            Steps.Add(wfStep.ID, wfStep)
        Next
        Return Steps

    End Function

    'Public Shared Function GetStep(ByVal WF As WorkFlow, ByVal WfStepId As Int32) As WFStep
    '    'Dim StrSelect As String = "Select * from ZViewWFSTEPS where step_id = " & WfStepId
    '    Dim Dstemp As DataSet
    '    Dim DsSteps As New DsSteps
    '    'Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
    '    'SP 29/12/05
    '    Try
    '        if Server.IsOracle then
    '            Dim ParValues() As Object = {WfStepId, 2}
    '            Dim ParNames() As Object = {"pStepId", "io_cursor"}
    '            Dim ParTypes() As Object = {13, 5}
    '            Dstemp = Server.Con.ExecuteDataset("ZWFStepsFactory_pkg.ZGetVWFStepsByStepId",  ParValues)
    '        Else
    '            Dim ParValues() As Object = {WfStepId}
    '            Dstemp = Server.Con.ExecuteDataset("ZGetVWFStepsByStepId", ParValues)
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    '    Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
    '    DsSteps.Merge(Dstemp)
    '    Dim i As Int32 = 0
    '    Dim wfStep As wfStep = New wfStep(WF, DsSteps.WFSteps(i).Step_Id, DsSteps.WFSteps(i).Name, DsSteps.WFSteps(i).Help, DsSteps.WFSteps(i).Description, New Drawing.Point(DsSteps.WFSteps(i).LocationX, DsSteps.WFSteps(i).LocationY), DsSteps.WFSteps(i).ImageIndex, DsSteps.WFSteps(i).Max_Docs, DsSteps.WFSteps(i).Max_Hours, DsSteps.WFSteps(i).StartAtOpenDoc)
    '    wfStep.SelectedUsers = GetStepUsers(wfStep.Id)
    '    wfStep.SelectedUserGroups = GetStepUserGroups(wfStep.Id)
    '    Return wfStep
    'End Function
#End Region

    Public Shared Sub UpdateExpiredDateTask(ByRef Result As TaskResult)
        Dim strupdate As String = "UPDATE WFDOCUMENT SET EXPIREDATE=" & Server.Con.ConvertDateTime(Result.ExpireDate) & " WHERE DOC_ID = " & Result.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
        'SP 4/1/2006
        'Try
        '    if Server.IsOracle then
        '        Dim ParValues() As Object = {Server.Con.ConvertDateTime(Result.ExpireDate), Result.Id}
        '        Dim ParNames() As Object = {"pExpDate", "pDocId"}
        '        Dim ParTypes() As Object = {7, 13}
        '        Server.Con.ExecuteNonQuery("ZWFUpdStepsFactory_pkg.ZUpdWFDocExpireDateByDocId",  ParValues)
        '    Else
        '        Dim ParValues() As Object = {Server.Con.ConvertDateTime(Result.ExpireDate), Result.Id}
        '        Server.Con.ExecuteNonQuery("ZUpdWFDocExpireDateByDocId", ParValues)
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub
    Public Shared Function GetTasksCount(ByVal StepId As Int32) As Int32
        'Dim StrSelect As String = "SELECT DCOUNT FROM ZVIEWWFDOCUMENTCOUNT WHERE STEP_ID = " & StepId
        'Dim Count As Int32 = Server.Con.ExecuteScalar(CommandType.Text, StrSelect)
        Dim Count As Int32
        'SP 4/1/2006

        If Server.IsOracle Then
            Dim ParValues() As Object = {StepId, 2}
            Dim ParNames() As Object = {"pStepId", "io_cursor"}
            Dim ParTypes() As Object = {13, 5}
            Count = Server.Con.ExecuteScalar("ZWFStepsFactory_pkg.ZGetDCountbyStepId", ParValues)
        Else
            Dim ParValues() As Object = {StepId}
            'Count = Server.Con.ExecuteScalar("ZGetDCountbyStepId", ParValues)
            Count = Server.Con.ExecuteScalar("zsp_workflow_100_GetDocCountByStepId", ParValues)
        End If


        Return Count
    End Function
    Public Shared Function CheckTransicions(ByRef wfstep As WFStep) As ArrayList

        Dim Destiny As New ArrayList
        Dim DestinySteps As New ArrayList

        'If IsNothing(wfStep.RuleInput) = False Then CheckRules(wfStep.RuleInput, Destiny)
        'If IsNothing(wfStep.RuleOutput) = False Then CheckRules(wfStep.RuleOutput, Destiny)
        'If IsNothing(wfStep.RuleSchedule) = False Then CheckRules(wfStep.RuleSchedule, Destiny)
        'If IsNothing(wfStep.RuleUpdate) = False Then CheckRules(wfStep.RuleUpdate, Destiny)

        If IsNothing(wfstep.DsRules) = False Then
            For Each R As DsRules.WFRulesRow In wfstep.DsRules.WFRules.Rows
                If IsNothing(R) = False Then

                    Dim rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(R.Id, wfstep.ID, True)
                    CheckRules(rule, Destiny)
                End If
            Next
        End If
        'If IsNothing(wfStep.RuleOutput) = False Then
        '    For Each R As WFRuleParent In wfStep.RuleOutput.Values
        '        If IsNothing(r) = False Then CheckRules(R, Destiny)
        '    Next
        'End If
        'If IsNothing(wfStep.RuleSchedule) = False Then
        '    For Each R As WFRuleParent In wfStep.RuleSchedule.Values
        '        If IsNothing(r) = False Then CheckRules(R, Destiny)
        '    Next
        'End If
        'If IsNothing(wfStep.RuleUpdate) = False Then
        '    For Each R As WFRuleParent In wfStep.RuleUpdate.Values
        '        If IsNothing(r) = False Then CheckRules(R, Destiny)
        '    Next
        'End If
        'If IsNothing(wfStep.RuleUserAction) = False Then
        '    For Each R As WFRuleParent In wfStep.RuleUserAction.Values
        '        If IsNothing(r) = False Then CheckRules(R, Destiny)
        '    Next
        'End If
        For Each R As WFRuleParent In Destiny
            DestinySteps.Add(DirectCast(R, Object).newwfstep)
        Next
        Return DestinySteps
    End Function
    Private Shared Sub CheckRules(ByRef wfrule As WFRuleParent, ByVal Destiny As ArrayList)
        If wfrule.Name.ToLower = "dodistribuir" Then
            Destiny.Add(wfrule)
        Else
            For Each R As WFRuleParent In wfrule.ChildRules
                CheckRules(R, Destiny)
            Next
        End If
    End Sub

    Public Shared Sub UpdateInitialStep(ByVal WF As WorkFlow)
        Dim sql As String = "update WFWorkflow set InitialStepId=" & WF.InitialStep.ID & " where work_id=" & WF.ID
        sql = Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub

    ''' <summary>
    ''' Verifica si la etapa tiene en uso reglas de actualización
    ''' </summary>
    ''' <param name="stepId">Id de la etapa</param>
    ''' <returns>True en caso de que la etapa sea utilizada por el servicio de workflow</returns>
    ''' <remarks></remarks>
    Public Shared Function CheckWfServiceUsage(ByVal stepId As Int64) As Boolean
        Dim wfStepsFactoryExt As New WFStepsFactoryExt
        Dim ruleCount As Int32 = wfStepsFactoryExt.GetStepServiceRulesCount(stepId)
        wfStepsFactoryExt = Nothing

        If ruleCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class