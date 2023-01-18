Imports Zamba.Core
Imports System.Collections.Generic
Imports System.Text

Public Class WFStepsFactory
    Inherits ZClass

#Region "Get"
    Private Shared Function GetDocumentCountByStepId(ByVal documentId As Integer) As Integer
        'Validar servers
        Dim query As String = "Select count(1) as CantidadColumnas from wfdocument where step_id = " & documentId.ToString()
        Return CInt(Server.Con.ExecuteScalar(CommandType.Text, query))
    End Function
    Public Shared Function GetTasksCountAllSteps() As DataSet
        Dim Ds As DataSet = Nothing
        Dim strSelect As String = String.Empty

        If Server.isOracle Then

            Dim Query As New StringBuilder
            Query.Append("Select Distinct (Select Name From WfWorkflow Where WfWorkflow.work_id =  t1.work_id) as " & Chr(34) & "Workflow" & Chr(34) & ", ")
            Query.Append(" t1.Name as " & Chr(34) & "Etapa" & Chr(34) & ", t2.DCOUNT as " & Chr(34) & "Documentos" & Chr(34) & " from (SELECT WFStep.step_Id, WFStep.work_id, WFStep.Name, WFStep.Description, ")
            Query.Append(" WFStep.Help, WFStep.CreateDate, WFStep.ImageIndex, WFStep.EditDate, WFStep.LocationX, WFStep.LocationY, ")
            Query.Append(" WFStep.max_docs, USR_RIGHTS.GROUPID, USR_RIGHTS.OBJID, USR_RIGHTS.RTYPE, WFStep.max_Hours, WFStep.StartAtOpenDoc FROM WFStep ")
            Query.Append(" INNER JOIN USR_RIGHTS ON WFStep.step_Id = USR_RIGHTS.ADITIONAL) t1 ")
            Query.Append(" inner join (SELECT count(1) AS DCOUNT, step_Id FROM WFDocument GROUP BY step_Id) t2 On t1.step_id=t2.step_id")

            Ds = Server.Con.ExecuteDataset(CommandType.Text, Query.ToString)
        Else
            'strSelect = "Select Distinct (Select Name From WfWorkflow Where WfWorkflow.work_id =  t1.work_id) as Workflow, t1.Name as Etapa, t2.DCOUNT as Documentos from  t1 inner join Zvw_WFDocumentCOUNT_100 t2 On t1.step_id=t2.step_id"
            'Ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            Dim parvalues() As Object = {}
            Ds = Server.Con.ExecuteDataset("Zsp_workflow_200_getWorkflows", parvalues)
        End If

        Return Ds
    End Function


    Public Shared Function GetTasksCount(ByVal StepId As Int32) As Int32

        Dim Count As Int32

        If Server.isOracle Then

            Dim query As String = "SELECT DCOUNT FROM ZVIEWWFDOCUMENTCOUNT WHERE STEP_ID = " & StepId
            Server.Con.ExecuteNonQuery(CommandType.Text, query)

        Else
            Dim ParValues() As Object = {StepId}

            Count = Server.Con.ExecuteScalar("zsp_workflow_100_GetDocCountByStepId", ParValues)
        End If

        Return Count
    End Function
    Public Shared Function GetTasksCountAllStep() As DsTasksUsers
        Dim Ds As DataSet = Nothing
        Dim strSelect As String = String.Empty

        If Server.isOracle Then
            Dim Query As New StringBuilder
            Query.Append("Select Distinct (Select Name From WfWorkflow Where WfWorkflow.work_id =  t1.work_id) as " & Chr(34) & "Workflow" & Chr(34) & ", ")
            Query.Append(" t1.Name as " & Chr(34) & "Etapa" & Chr(34) & ", t2.DCOUNT as " & Chr(34) & "Documentos" & Chr(34) & " from (SELECT WFStep.step_Id, WFStep.work_id, WFStep.Name, WFStep.Description, ")
            Query.Append(" WFStep.Help, WFStep.CreateDate, WFStep.ImageIndex, WFStep.EditDate, WFStep.LocationX, WFStep.LocationY, ")
            Query.Append(" WFStep.max_docs, USR_RIGHTS.GROUPID, USR_RIGHTS.OBJID, USR_RIGHTS.RTYPE, WFStep.max_Hours, WFStep.StartAtOpenDoc FROM WFStep ")
            Query.Append(" INNER JOIN USR_RIGHTS ON WFStep.step_Id = USR_RIGHTS.ADITIONAL) t1 ")
            Query.Append(" inner join (SELECT count(1) AS DCOUNT, step_Id FROM WFDocument GROUP BY step_Id) t2 On t1.step_id=t2.step_id")

            Ds = Server.Con.ExecuteDataset(CommandType.Text, Query.ToString)
        Else
            strSelect = "Select Distinct (Select Name From WfWorkflow Where WfWorkflow.work_id =  t1.work_id) as Workflow, t1.Name as Etapa, t2.DCOUNT as Documentos from Zvw_ZVIEWWFUserSTEPS_100 t1 inner join Zvw_WFDocumentCOUNT_100 t2 On t1.step_id=t2.step_id"
            Ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        End If
        Dim DsTaskUser As New DsTasksUsers
        Ds.Tables(0).TableName = DsTaskUser.Tables(0).TableName
        DsTaskUser.Merge(Ds)
        Return DsTaskUser
    End Function
    ''' <summary>
    ''' Método utilizado para recuperar las etapas de un workflow según el id del workflow y devolver un dataset
    ''' </summary>
    ''' <param name="WFId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	09/06/2008	Modified    Se comento el procedimiento almacenado (inexistente) de Oracle
    '''      	        12/09/2008	Modified    Se agrego a la consulta el "order by step_id"
    '''     [Ezequiel]  18/06/2008  Modified    Se cambio completamente el cuerpo de la funcion para llame a un store
    ''' </history>
    Public Shared Function GetDsStepsByWFIdAndUserId(ByVal WFId As Int32, ByVal UserId As Int64) As DsSteps

        Dim Dstemp As DataSet = Nothing
        Dim DsSteps As New DsSteps

        If Server.isOracle Then
            ''Dim parNames() As String = {"workid", "userid", "io_cursor"}
            'Dim parTypes() As Object = {13, 13, 5}
            Dim parValues() As Object = {WFId, UserId, 2}
            Dstemp = Server.Con.ExecuteDataset("zsp_workflow_100.GetStepsByWFIdAndUserId", parValues)

        Else
            Dim parameters() As Object = {WFId, UserId}
            Dstemp = Server.Con.ExecuteDataset("zsp_workflow_100_GetStepsByWFIdAndUserId", parameters)

        End If

        Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
        DsSteps.Merge(Dstemp)

        Return DsSteps

    End Function

    ''' <summary>
    ''' Obtiene el id, nombre y cantidad de tareas por etapa.
    ''' </summary>
    ''' <param name="UserId"></param>
    ''' <returns></returns>
    ''' <remarks>
    '''     Es una copia de GetDsStepsByWFIdAndUserId pero se optimizó la 
    '''     consulta obteniendo unicamente las columnas deseadas.
    ''' </remarks>
    Public Shared Function GetWFAndStepIdsAndNamesAndTaskCount(ByVal UserId As Int64) As DataTable
        Dim DS As DataSet = Nothing

        Dim query As New StringBuilder()
        Dim useridStr As String = UserId.ToString

        query.Append("((SELECT 0 AS Cantidad, WFWorkflow.work_id AS WFId, WFWorkflow.Name AS WFName, WFStep.step_Id AS WFStepId, WFStep.Name AS WFStepName FROM WFWorkflow INNER JOIN WFStep ON WFStep.work_id = WFWorkflow.work_id INNER JOIN USR_RIGHTS on aditional = step_id and objid = 42 and rtype = 19 and")

        query.Append("(groupid = ")
        query.Append(UserId)
        query.Append(" OR groupid IN (SELECT groupid FROM usr_r_group WHERE usrid = ")
        query.Append(UserId)
        query.Append(")) ")
        query.Append("GROUP BY WFWorkflow.work_id, WFWorkflow.Name, WFStep.orden, WFStep.Name ")
        query.Append(",WFStep.step_Id)) ORDER BY  WFWorkflow.Name, WFStep.Orden, WFStep.Name")
        DS = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

        Return DS.Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene los nombres y id de las etapas
    ''' </summary>
    ''' <param name="WorkId">Id del workflow</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetOrderedWFAndStepIdsAndNames(ByVal WorkId As Int64) As DataSet
        If Server.isOracle Then
            'TODO: PASAR ESTA CONSULTA A STORED
            Dim query As New StringBuilder()

            query.Append("select step_id, name, orden from wfstep where work_id=")
            query.Append(WorkId)
            query.Append(" order by orden, name, step_id")

            Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
        Else
            Dim params As Object() = {WorkId}
            Return Server.Con.ExecuteDataset("zsp_200_workflow_getOrderedSteps", params)
        End If
    End Function

    ''' <summary>
    ''' Método que sirve para traer las etapas según un id de workflow
    ''' </summary>
    ''' <param name="WFId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 	[Gaston]	06/06/2008	Modified    ' Se comento el procedimiento almacenado (inexistente) de Oracle y se agrego una consulta en el código
    ''' </history>
    Public Shared Function GetDsSteps(ByVal WFId As Int64) As DsSteps

        'Dim StrSelect As String = "Select * from ZViewWFSTEPS where WORK_ID = " & WFId
        Dim Dstemp As DataSet
        Dim DsSteps As New DsSteps
        'Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

        'SP 29/12/05

        If Server.isOracle Then
            'Dim ParValues() As Object = {WFId, 2}
            '''Dim parNames() As Object = {"pWFId", "io_cursor"}
            ''Dim parTypes() As Object = {13, 5}
            'Dstemp = Server.Con.ExecuteDataset("ZWFStepsFactory_pkg.ZGetViewWFStepsByWfID",  ParValues)
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, "Select * from wfstep where work_id = " & WFId)
        Else
            Dim ParValues() As Object = {WFId}
            Dstemp = Server.Con.ExecuteDataset("ZGetViewWFStepsByWfID", ParValues)
        End If


        Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
        DsSteps.Merge(Dstemp)
        Return DsSteps

    End Function


    ''' <summary>
    ''' Método que sirve para traer las etapas según un id de workflow
    ''' </summary>
    ''' <param name="WFId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 	[Ezequiel]	28/04/2011	Metodo el cual
    ''' </history>
    Public Shared Function GetDsSteps(ByVal Workflows As List(Of EntityView)) As DsSteps

        'Dim StrSelect As String = "Select * from ZViewWFSTEPS where WORK_ID = " & WFId
        Dim Dstemp As DataSet
        Dim DsSteps As New DsSteps
        'Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

        'SP 29/12/05
        Dim strids As String
        For Each WF As EntityView In Workflows
            strids = strids & "," & WF.ID.ToString
        Next
        If strids <> String.Empty Then
            If Server.isOracle Then
                'Dim ParValues() As Object = {WFId, 2}
                '''Dim parNames() As Object = {"pWFId", "io_cursor"}
                ''Dim parTypes() As Object = {13, 5}
                'Dstemp = Server.Con.ExecuteDataset("ZWFStepsFactory_pkg.ZGetViewWFStepsByWfID",  ParValues)
                Dstemp = Server.Con.ExecuteDataset(CommandType.Text, "Select * from wfstep where work_id in (" & strids.Substring(1) & ")")
            Else
                Dim ParValues() As Object = {strids.Substring(1)}
                Dstemp = Server.Con.ExecuteDataset("ZGetViewWFStepsByWfID_200", ParValues)
            End If
            Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
            DsSteps.Merge(Dstemp)

            Return DsSteps

        End If

        Return Nothing

    End Function


    ''' <summary>
    ''' Método utilizado para recuperar las etapas de un workflow según el id del workflow y devolver un dataset
    ''' </summary>
    ''' <param name="WFId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	09/06/2008	Modified    Se comento el procedimiento almacenado (inexistente) de Oracle
    ''' </history>
    'Public Shared Function GetDsSteps(ByVal WFId As Int32) As DsSteps

    '    Dim Dstemp As DataSet = Nothing
    '    Dim DsSteps As New DsSteps

    '    Dim query As New System.Text.StringBuilder
    '    query.Append("SELECT step_Id, work_id, Name, Description, Help, CreateDate, ImageIndex, EditDate, LocationX, LocationY, max_docs, ")
    '    query.Append("max_Hours, StartAtOpenDoc FROM WFStep ")
    '    query.Append("where WORK_ID = " & WFId)

    '    Dstemp = Server.Con.ExecuteDataset(CommandType.Text, query.ToString())

    '    Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
    '    DsSteps.Merge(DSTEMP)

    '    Return DsSteps

    'End Function

    Public Shared Function GetDsAllSteps() As DsSteps
        Dim StrSelect As String = "SELECT * FROM ZViewWFSTEPS"
        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

        Dim DsSteps As New DsSteps
        Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
        DsSteps.Merge(Dstemp)

        Return DsSteps
    End Function

    ''' <summary>
    ''' Devuelve una etapa por el id
    ''' </summary>
    ''' <param name="stepId">id de la etapa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] 19/06/2009
    ''' </history>
    Public Shared Function GetStepById(ByVal stepId As Int64) As DataSet
        Dim Ds As DataSet = Nothing
        If Server.isOracle Then
            Ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM WFStep WHERE step_id = " & stepId)
        Else
            Dim parameters() As Object = {(stepId)}
            Ds = Server.Con.ExecuteDataset("zsp_workflow_100_GetStepByStepId", parameters)
        End If
        Return Ds
    End Function

    Public Shared Function GetStepsDs(ByVal stepIds As List(Of Int64)) As DataSet
        If (IsNothing(stepIds) Or stepIds.Count = 0) Then
            Return Nothing
        Else
            Dim QueryBuilder As New StringBuilder
            QueryBuilder.Append("SELECT step_Id, work_id, Name, Description, Help, CreateDate, ImageIndex, ")
            QueryBuilder.Append("EditDate, LocationX, LocationY, max_Hours, max_docs, StartAtOpenDoc, Color, ")
            QueryBuilder.Append("Width, Height, Width2 FROM WFStep WHERE ")

            For Each CurrentId As Int32 In stepIds
                QueryBuilder.Append(" step_Id ")
                QueryBuilder.Append(CurrentId.ToString())
                QueryBuilder.Append(" Or ")
            Next

            QueryBuilder.Remove(QueryBuilder.Length - 4, 4)

            Return Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
        End If

        Return Nothing
    End Function



    Public Shared Function GetStep(ByVal Wf As WorkFlow, ByVal StepId As Int32) As WFStep
        'Dim StrSelect As String = "Select * from ZViewWFSTEPS where WORK_ID = " & WF.Id

        Dim DsSteps As New DsSteps

        'Dim Strselect As String = "Select * from wfstep where step_id = " & StepId
        Dim Strselect As String = "SELECT step_Id, work_id, Name, Description, Help, CreateDate, ImageIndex, EditDate, LocationX, LocationY, max_Hours, max_docs, StartAtOpenDoc, Color, Width, Height, Width2 FROM wfstep WHERE step_id = " & StepId
        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)

        Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
        DsSteps.Merge(Dstemp)

        If DsSteps.WFSteps.Rows.Count > 0 Then
            'Dim ImagePath As String = ""
            'Try
            '    If Not IsDBNull(DsSteps.WFSteps(0).ImagePath) Then ImagePath = DsSteps.WFSteps(0).ImagePath
            'Catch ex As Exception
            '   zclass.raiseerror(ex)
            'End Try
            'Dim Wfstep As Wfstep = New Wfstep(Wf, DsSteps.WFSteps(0).Step_Id, DsSteps.WFSteps(0).Name, DsSteps.WFSteps(0).Help, DsSteps.WFSteps(0).Description, New Drawing.Point(DsSteps.WFSteps(0).LocationX, DsSteps.WFSteps(0).LocationY), DsSteps.WFSteps(0).ImageIndex, DsSteps.WFSteps(0).Max_Docs, DsSteps.WFSteps(0).Max_Hours, DsSteps.WFSteps(0).StartAtOpenDoc, ImagePath)
            Dim Wfstep As WFStep = New WFStep(Wf.ID, DsSteps.WFSteps(0).Step_Id, DsSteps.WFSteps(0).Name, DsSteps.WFSteps(0).Help, DsSteps.WFSteps(0).Description, New Drawing.Point(DsSteps.WFSteps(0).LocationX, DsSteps.WFSteps(0).LocationY), DsSteps.WFSteps(0).ImageIndex, DsSteps.WFSteps(0).Max_Docs, DsSteps.WFSteps(0).Max_Hours, DsSteps.WFSteps(0).StartAtOpenDoc, "", 50, 150, 0, 0)
            Dim DsStates As DsStepState = WFStepStatesFactory.GetAllStates(DsSteps)
            WFStepStatesFactory.FillState(Wfstep, DsStates)
            Return Wfstep
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function GetStepById(ByVal stepId As Int64, ByVal wf As IWorkFlow) As IWFStep
        Dim Dstemp As DataSet
        Dim DsSteps As New DsSteps
        Dim Wfstep As WFStep = Nothing
        'Dim wf As WorkFlow = Nothing
        'Dim wfid As Int64 = WFFactory.GetWorkflowIdByStepId(stepId)
        'Dim Ds As DataSet = WFFactory.GetWfById(wfid)
        'wf = WFFactory.GetWf(Ds.Tables(0).Rows(0))
        'Dim Strselect As String = "Select * from wfstep where step_id = " & StepId
        Dim Strselect As String = "SELECT step_Id, work_id, Name, Description, Help, CreateDate, ImageIndex, EditDate, LocationX, LocationY, max_Hours, max_docs, StartAtOpenDoc, Color, Width, Height, Width2 FROM wfstep WHERE step_id = " & stepId

        Try
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
            Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
            DsSteps.Merge(Dstemp)

            If DsSteps.WFSteps.Rows.Count > 0 Then

                'Wfstep = New WFStep(Wf, DsSteps.WFSteps(0).Step_Id, DsSteps.WFSteps(0).Name, DsSteps.WFSteps(0).Help, DsSteps.WFSteps(0).Description, New Drawing.Point(DsSteps.WFSteps(0).LocationX, DsSteps.WFSteps(0).LocationY), DsSteps.WFSteps(0).ImageIndex, DsSteps.WFSteps(0).Max_Docs, DsSteps.WFSteps(0).Max_Hours, DsSteps.WFSteps(0).StartAtOpenDoc)
                Wfstep = New WFStep(wf.ID, DsSteps.WFSteps(0).Step_Id, DsSteps.WFSteps(0).Name, DsSteps.WFSteps(0).Help, DsSteps.WFSteps(0).Description, New Drawing.Point(DsSteps.WFSteps(0).LocationX, DsSteps.WFSteps(0).LocationY), DsSteps.WFSteps(0).ImageIndex, DsSteps.WFSteps(0).Max_Docs, DsSteps.WFSteps(0).Max_Hours, DsSteps.WFSteps(0).StartAtOpenDoc, "", 50, 150, 0, 0)
                Dim DsStates As DsStepState = WFStepStatesFactory.GetAllStates(DsSteps)
                WFStepStatesFactory.FillState(Wfstep, DsStates)
            End If
        Catch ex As Exception
            raiseerror(ex)
        Finally
            DsSteps.Dispose()
            DsSteps = Nothing
        End Try

        Return Wfstep
    End Function

    Public Shared Function GetStepNameById(ByVal stepId As Int64) As String
        Dim name As String = ""
        Dim Strselect As String = "SELECT Name FROM wfstep WHERE step_id = " & stepId.ToString()
        name = Server.Con.ExecuteScalar(CommandType.Text, Strselect)
        Return name
    End Function

    ''' <summary>
    ''' Método utilizado para recuperar las etapas de un workflow según el id del workflow y devolver un SortedList
    ''' </summary>
    ''' <param name="WfId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	09/06/2008	Modified    Se comento el procedimiento almacenado (inexistente) de Oracle
    ''' </history>
    Public Shared Function GetStepsByWorkId(ByVal WfId As Int64) As SortedList

        'Dim StrSelect As String = "Select * from ZViewWFSTEPS where WORK_ID = " & WF.Id
        Dim Dstemp As DataSet
        Dim DsSteps As New DsSteps
        'Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        'SP 29/12/05

        If Server.isOracle Then
            'Dim ParValues() As Object = {WfId, 2}
            '''Dim parNames() As Object = {"pWFId", "io_cursor"}
            ''Dim parTypes() As Object = {13, 5}
            'Dstemp = Server.Con.ExecuteDataset("ZWFStepsFactory_pkg.ZGetViewWFStepsByWfID",  ParValues)
            Dim query As New StringBuilder
            query.Append("SELECT step_Id, work_id, Name, Description,Help, CreateDate, ImageIndex, EditDate, LocationX, LocationY, max_docs, ")
            query.Append("max_Hours, StartAtOpenDoc FROM WFStep")
            query.Append(" where WORK_ID = " & WfId)
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, query.ToString())
        Else
            Dim ParValues() As Object = {WfId}
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
            Dim i_WfStep As WFStep = New WFStep(WfId, DsSteps.WFSteps(i).Step_Id, DsSteps.WFSteps(i).Name, DsSteps.WFSteps(i).Help, DsSteps.WFSteps(i).Description, New Drawing.Point(DsSteps.WFSteps(i).LocationX, DsSteps.WFSteps(i).LocationY), DsSteps.WFSteps(i).ImageIndex, DsSteps.WFSteps(i).Max_Docs, DsSteps.WFSteps(i).Max_Hours, DsSteps.WFSteps(i).StartAtOpenDoc, "", 50, 150, 0, 0)
            Dim DsStates As DsStepState = WFStepStatesFactory.GetAllStates(DsSteps)
            WFStepStatesFactory.FillState(i_WfStep, DsStates)
            Steps.Add(DsSteps.WFSteps(i).Step_Id, i_WfStep)
        Next

        Return Steps

    End Function

    ''' <summary>
    ''' Método que trae etapas de un workflow
    ''' </summary>
    ''' <param name="workflowId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    11/06/2008  Modified    Se agrego la consulta en código fuente para Oracle 
    ''' </history>
    Public Shared Function GetStepsByWorkflow(ByVal workflowId As Int64) As DataSet

        Dim QueryBuilder As New StringBuilder()

        QueryBuilder.Append("SELECT step_Id,work_id, Name, Description, Help, CreateDate, ")
        QueryBuilder.Append("ImageIndex, EditDate, LocationX, LocationY, max_Hours, max_docs, ")
        QueryBuilder.Append("StartAtOpenDoc, Color, Width, Height, Width2, ")

        If Server.isOracle Then

            QueryBuilder.Append("(SELECT count(1) FROM wfdocument WHERE wfdocument.step_Id = WFStep.step_Id) AS " & Chr(34) & "TasksCount" & Chr(34) & ", ")
            QueryBuilder.Append("(SELECT count(1) FROM wfdocument WHERE wfdocument.expiredate < sysdate And wfdocument.step_Id = WFStep.step_Id) AS " & Chr(34) & "ExpiredTasksCount" & Chr(34))
            QueryBuilder.Append(" FROM WFStep WHERE work_id = " & workflowId.ToString())

        Else
            QueryBuilder.Append("(SELECT count(1) FROM wfdocument WHERE wfdocument.step_Id = WFStep.step_Id) as TasksCount, ")
            QueryBuilder.Append("(SELECT count(1) FROM wfdocument WHERE wfdocument.expiredate < getdate() And wfdocument.step_Id = WFStep.step_Id )as ExpiredTasksCount ")
            QueryBuilder.Append("FROM WFStep WHERE work_id = " & workflowId.ToString())
        End If

        Return Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString)

    End Function

    Public Shared Function GetStepsByWorkflows(ByVal workflowIds As List(Of Int64)) As DataSet
        If (IsNothing(workflowIds) OrElse workflowIds.Count = 0) Then
            Return Nothing
        Else
            Dim QueryBuilder As New StringBuilder()
            QueryBuilder.Append("SELECT step_Id, work_id, Name, Description, Help, CreateDate, ")
            QueryBuilder.Append("ImageIndex, EditDate, LocationX, LocationY, max_Hours, max_docs, ")
            QueryBuilder.Append("StartAtOpenDoc, Color, Width, Height, Width2, ")
            QueryBuilder.Append("(SELECT count(1) FROM wfdocument WHERE wfdocument.step_Id = WFStep.step_Id )as TasksCount, ")
            QueryBuilder.Append("(SELECT count(1) FROM wfdocument WHERE wfdocument.expiredate < getdate() And wfdocument.step_Id = WFStep.step_Id )as ExpiredTasksCount ")
            QueryBuilder.Append("FROM WFStep WHERE ")

            For Each WorkflowId As Int32 In workflowIds
                QueryBuilder.Append("work_id =")
                QueryBuilder.Append(WorkflowId.ToString())
                QueryBuilder.Append(" Or ")
            Next
            QueryBuilder.Remove(QueryBuilder.Length - 4, 4)

            Return Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString)
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Método utilizado para recuperar las etapas de un workflow según el id del workflow y devolver un SortedList 
    ''' </summary>
    ''' <param name="WF"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	09/06/2008	Modified    Se comento el procedimiento almacenado (inexistente) de Oracle
    ''' </history>
    Public Shared Function GetSteps(ByVal WF As WorkFlow) As SortedList

        'Dim StrSelect As String = "Select * from ZViewWFSTEPS where WORK_ID = " & WF.Id
        Dim Dstemp As DataSet
        Dim DsSteps As New DsSteps
        'Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        'SP 29/12/05

        If Server.isOracle Then
            'Dim ParValues() As Object = {WF.ID, 2}
            '''Dim parNames() As Object = {"pWFId", "io_cursor"}
            ''Dim parTypes() As Object = {13, 5}
            'Dstemp = Server.Con.ExecuteDataset("ZWFStepsFactory_pkg.ZGetViewWFStepsByWfID",  ParValues)
            Dim query As New StringBuilder
            query.Append("SELECT step_Id, work_id, Name, Description, Help, CreateDate, ImageIndex, EditDate, LocationX, LocationY, max_docs, ")
            query.Append("max_Hours, StartAtOpenDoc FROM WFStep")
            query.Append("where WORK_ID = " & WF.ID)
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, query.ToString())
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

    ''' <summary>
    ''' Método utilizado para recuperar las etapas de un workflow según el id del workflow y devolver un SortedList
    ''' </summary>
    ''' <param name="WF"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	09/06/2008	Modified    Se comento el procedimiento almacenado (inexistente) de Oracle
    ''' </history>
    Public Shared Function GetStepsDictionary(ByVal WF As WorkFlow) As SortedList

        'Dim StrSelect As String = "Select * from ZViewWFSTEPS where WORK_ID = " & WF.Id
        Dim Dstemp As DataSet
        Dim DsSteps As New DsSteps
        'Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

        'SP 29/12/05
        If Server.isOracle Then
            'Dim ParValues() As Object = {WF.ID, 2}
            '''Dim parNames() As Object = {"pWFId", "io_cursor"}
            ''Dim parTypes() As Object = {13, 5}
            'Dstemp = Server.Con.ExecuteDataset("ZWFStepsFactory_pkg.ZGetViewWFStepsByWfID",  ParValues)
            Dim query As New StringBuilder
            query.Append("SELECT step_Id, work_id, Name, Description, Help, CreateDate, ImageIndex, EditDate, LocationX, LocationY, max_docs, ")
            query.Append("max_Hours, StartAtOpenDoc FROM WFStep")
            query.Append("where WORK_ID = " & WF.ID)
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, query.ToString())
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
            'Dim ImagePath As String = ""
            'Try
            '    ImagePath = DsSteps.WFSteps(i).ImagePath
            'Catch ex As Exception
            'End Try
            'wfStep = New wfStep(WF, DsSteps.WFSteps(i).Step_Id, DsSteps.WFSteps(i).Name, DsSteps.WFSteps(i).Help, DsSteps.WFSteps(i).Description, New Drawing.Point(DsSteps.WFSteps(i).LocationX, DsSteps.WFSteps(i).LocationY), DsSteps.WFSteps(i).ImageIndex, DsSteps.WFSteps(i).Max_Docs, DsSteps.WFSteps(i).Max_Hours, DsSteps.WFSteps(i).StartAtOpenDoc, ImagePath)
            wfStep = New WFStep(WF.ID, DsSteps.WFSteps(i).Step_Id, DsSteps.WFSteps(i).Name, DsSteps.WFSteps(i).Help, DsSteps.WFSteps(i).Description, New Drawing.Point(DsSteps.WFSteps(i).LocationX, DsSteps.WFSteps(i).LocationY), DsSteps.WFSteps(i).ImageIndex, DsSteps.WFSteps(i).Max_Docs, DsSteps.WFSteps(i).Max_Hours, DsSteps.WFSteps(i).StartAtOpenDoc, "", 50, 150, 0, 0)
            Steps.Add(wfStep.ID, wfStep)
        Next

        Return Steps

    End Function

    Public Shared Sub GetStepsIdName(ByRef h As SortedList, ByVal CurrentUser As IUser)
        Try
            Dim StrSelect As String = "Select distinct step_id,name from wfstep,USR_RIGHTS"
            Dim where As String = ""
            '  If Not RightFactory.CurrentUser.Groups.Count < 1 Then
            If Not CurrentUser.Groups.Count < 1 Then
                ' For Each g as iusergroup In RightFactory.CurrentUser.Groups
                For Each g As IUserGroup In CurrentUser.Groups
                    where += "USR_RIGHTS.groupid=" & g.ID & " Or "
                Next
            End If
            'where += "USR_RIGHTS.groupid=" & RightFactory.CurrentUser.Id
            where += "USR_RIGHTS.groupid=" & CurrentUser.ID

            where = " where (" & where & ") And wfstep.step_id=USR_RIGHTS.aditional order by 2"

            Dim Dstemp As DataSet
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect & where)

            For i As Int32 = 0 To Dstemp.Tables(0).Rows.Count - 1
                h.Add(CInt(Dstemp.Tables(0).Rows(i).Item(0)), Dstemp.Tables(0).Rows(i).Item(1))
            Next
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub
    Public Shared Function GetStepsIdNameAndTasksCountbyWorkflowId(ByVal WorkflowId As Int64, ByVal CurrentUser As IUser) As System.Collections.Generic.List(Of Zamba.Core.EntityView)
        Dim StrSelect As String = "Select distinct step_id,name from wfstep,USR_RIGHTS"
        Dim where As String = ""
        '  If Not RightFactory.CurrentUser.Groups.Count < 1 Then
        If Not CurrentUser.Groups.Count < 1 Then
            ' For Each g as iusergroup In RightFactory.CurrentUser.Groups
            For Each g As IUserGroup In CurrentUser.Groups
                where += "USR_RIGHTS.groupid=" & g.ID & " Or "
            Next
        End If
        'where += "USR_RIGHTS.groupid=" & RightFactory.CurrentUser.Id
        where += "USR_RIGHTS.groupid=" & CurrentUser.ID

        where = " where (" & where & ") And wfstep.step_id=USR_RIGHTS.aditional order by 2"

        Dim Dstemp As DataSet
        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect & where)

        Dim Steps As New System.Collections.Generic.List(Of Zamba.Core.EntityView)
        For i As Int32 = 0 To Dstemp.Tables(0).Rows.Count - 1
            Dim stepId As Int64 = CInt(Dstemp.Tables(0).Rows(i).Item(0))
            Steps.Add(New Zamba.Core.EntityView(stepId, Dstemp.Tables(0).Rows(i).Item(1), 0))
        Next
        Return Steps
    End Function

    Public Shared Sub InsertStepCounts()
        Dim query As String = String.Empty

        If Server.isOracle Then
            query = "INSERT INTO zobjcount " &
                    "SELECT 42, s.step_Id as StepId, nvl(count(1),0) as TaskCount, 0, SYSDATE " &
                    "FROM wfstep s left join WFDOCUMENT D on s.step_id = d.step_id " &
                    "WHERE s.step_Id NOT IN (SELECT objectId from zobjcount where objecttypeid = 42) " &
                    "AND s.step_Id is not null GROUP BY s.step_Id"
        Else
            query = "INSERT INTO zobjcount " &
                    "SELECT 42, s.step_Id as StepId, isnull(count(1),0) as TaskCount, 0, GETDATE() " &
                    "FROM wfstep s left join WFDOCUMENT D on s.step_id = d.step_id " &
                    "WHERE s.step_Id NOT IN (SELECT objectId from zobjcount where objecttypeid = 42) " &
                    "AND s.step_Id is not null GROUP BY s.step_Id"
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Sub UpdateStepCounts()
        Dim query As String = String.Empty

        If Server.isOracle Then
            query = "UPDATE zobjcount SET zobjcount.UPDATEDATE = SYSDATE, zobjcount.count = " &
                    "NVL((SELECT nvl(TaskCount,0) from " &
                    "(SELECT s.step_Id AS StepId ,nvl(count(1),0) AS TaskCount " &
                    "FROM wfstep s INNER JOIN WFDOCUMENT D ON s.step_id = d.step_id " &
                    "WHERE s.step_Id IN (SELECT objectId from zobjcount where objecttypeid = 42) " &
                    "AND s.step_Id is not null " &
                    "GROUP BY s.step_Id) q " &
                    "WHERE zobjcount.objectid = q.StepId),0) " &
                    "WHERE zobjcount.OBJECTTYPEID = 42"
        Else
            query = "UPDATE zobjcount SET zobjcount.count = a.StepId, zobjcount.UpdateDate = GETDATE() " &
                    "FROM (SELECT s.step_Id as StepId, isnull(count(1),0) as TaskCount " &
                    "FROM wfstep s INNER join WFDOCUMENT D on s.step_id = d.step_id " &
                    "WHERE s.step_Id IN (SELECT objectId from zobjcount where objecttypeid = 42) AND s.step_Id is not null " &
                    "GROUP BY s.step_Id) a " &
                    "WHERE zobjcount.objectid = a.StepId AND zobjcount.ObjectTypeId = 42"
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    ''' <summary>
    ''' Devuelve una coleccion de ids de etapas
    ''' </summary>
    ''' <param name="WorkflowId"></param>
    ''' <history>Marcelo created 10/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetStepsIdAndNamebyWorkflowId(ByVal WorkflowId As Int64) As Dictionary(Of Int64, String)
        Dim StrSelect As String = "Select distinct step_id,name from wfstep where work_id = " & WorkflowId & " order by step_id asc"

        Dim Dstemp As DataSet
        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

        Dim Steps As New Dictionary(Of Int64, String)
        For i As Int32 = 0 To Dstemp.Tables(0).Rows.Count - 1
            Dim intStepId As Int64 = CInt(Dstemp.Tables(0).Rows(i).Item(0))
            Dim strStepName As String = Dstemp.Tables(0).Rows(i).Item(1)
            Steps.Add(intStepId, strStepName)
        Next
        Return Steps
    End Function

    ''' <summary>
    ''' Devuelve una coleccion de ids de etapas las cuales poseen reglas en el evento Abrir Zamba y que el usuario tiene permiso de ejecutar.
    ''' </summary>
    ''' <param name="WorkflowId"></param>
    ''' <history>Ezequiel created 27/07/2009</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetStepsWithZOpenEvent(ByVal UsrID As Int64) As Dictionary(Of Int64, String)
        Dim Dstemp As DataSet
        If Server.isOracle Then
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, "SELECT distinct WFRules.step_Id, WFStep.name FROM WFRules INNER JOIN WFStep ON WFRules.step_Id = WFStep.step_Id WHERE type = 40 And WFRules.step_id IN (SELECT aditional FROM USR_RIGHTS WHERE objid=42 And (rtype=19 Or rtype=35) And (groupid= " & UsrID & " Or groupid IN (SELECT groupid FROM usr_r_group WHERE usrid= " & UsrID & "))) ")
        Else
            Dim parameters() As Object = {UsrID}
            Dstemp = Server.Con.ExecuteDataset("zsp_workflow_100_GetStepsWithZOpenEvent", parameters)
        End If
        Dim Steps As New Dictionary(Of Int64, String)
        For i As Int32 = 0 To Dstemp.Tables(0).Rows.Count - 1
            Dim intStepId As Int64 = CInt(Dstemp.Tables(0).Rows(i).Item(0))
            Dim strStepName As String = Dstemp.Tables(0).Rows(i).Item(1)
            Steps.Add(intStepId, strStepName)
        Next
        Return Steps
    End Function

    ''' <summary>
    ''' Obtiene las id de tipos de documento que se encuentran en una etapa.
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Ezequiel] 14/09/2009  Created.
    ''' </history>
    Public Shared Function GetDocTypesByWfStep(ByVal stepId As Int64) As DataTable
        Dim Ds As DataSet = Nothing
        'Dim query As String = String.Empty
        Ds = Server.Con.ExecuteDataset(CommandType.Text, String.Format("select distinct t.doc_type_id as {0}, T.DOC_TYPE_NAME as {1} from WFDocument W inner join doc_type T On w.DOC_TYPE_ID = T.DOC_TYPE_ID where w.step_Id = {2} ", GridColumns.DOC_TYPE_ID_COLUMNNAME, GridColumns.DOC_TYPE_NAME_COLUMNNAME, stepId))

        ' Ds = Server.Con.ExecuteDataset(CommandType.Text, query)

        'Ds = Server.Con.ExecuteDataset(CommandType.Text, String.Format("Select T.doc_type_id As {0}, T.DOC_TYPE_NAME As {1} from wf_dt R inner join WFStep S On S.work_id = R.WFId inner join doc_type T On R.DOCTYPEID = T.DOC_TYPE_ID where step_Id = {2}", GridColumns.DOC_TYPE_ID_COLUMNNAME, GridColumns.DOC_TYPE_NAME_COLUMNNAME, stepId))

        If Ds.Tables.Count = 1 Then
            Return Ds.Tables(0)
        End If

        Return Nothing
    End Function

    Public Shared Function GetTasksConsumedMinutes(ByVal stepId As Int64) As DataTable
        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("Select ConsumedMinutes ")
        QueryBuilder.Append("FROM WfStepPerformance ")
        QueryBuilder.Append("WHERE StepId = ")
        QueryBuilder.Append(stepId.ToString())
        Dim DsConsumedMinutes As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
        If Not IsNothing(DsConsumedMinutes) AndAlso DsConsumedMinutes.Tables.Count = 1 Then
            Return DsConsumedMinutes.Tables(0)
        End If
        Return Nothing
    End Function

    Public Shared Function GetStepIdByTaskId(ByVal TaskId As Int64) As Int64
        Dim stepid As Int64
        Dim strselect As String = "Select step_id from wfdocument where task_id = " & TaskId
        stepid = Server.Con.ExecuteScalar(CommandType.Text, strselect)
        Return stepid
    End Function

#End Region

#Region "Users & Group"
    Public Shared Function GetStepUsersIdsAndNames(ByVal wfstepid As Int64) As DataTable
        Dim Dstemp As DataSet = Nothing
        Dim DsSteps As New DsSteps
        If Server.isOracle Then
            Dim Strselect As String = "Select distinct usrtable.id As ID,usrtable.name As NAME from usrtable,USR_RIGHTS where (aditional = " & wfstepid & " And usrtable.id=USR_RIGHTS.groupid) order by usrtable.name"
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)

            Strselect = "Select ID, NAME from usr_r_group inner join usrtable On usrid=ID where groupid In (Select distinct(id) from USRGROUP inner join usr_Rights On usr_Rights.groupid = USRGROUP.id where ADITIONAL = " & wfstepid & ")"
            Dstemp.Merge(Server.Con.ExecuteDataset(CommandType.Text, Strselect))
        Else
            Dim ParValues() As Object = {wfstepid}
            Dstemp = Server.Con.ExecuteDataset("zsp_workflow_100_GetStepUsersByGroupAndUsers", ParValues)
        End If


        Return Dstemp.Tables(0)
    End Function
    ''' <summary>
    ''' Obtiene permisos de usuario por stepId
    ''' </summary>
    ''' <param name="wfstepid"></param>
    ''' <history> 
    '''     (pablo)    06/01/2011  Created        
    ''' </history>
    ''' <remarks></remarks>
    Public Shared Function GetStepOnlyUsersIdsAndNames(ByVal wfstepid As Int64) As DataTable
        Dim Strselect As String = "Select distinct usrtable.id As ID,usrtable.name As NAME from usrtable,USR_RIGHTS where (aditional = " & wfstepid & " And usrtable.id=USR_RIGHTS.groupid)"
        Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Return dstemp.Tables(0)
    End Function
    Public Shared Function GetStepUserGroupsIdsAndNames(ByVal wfstepid As Int64) As DataTable
        Dim Strselect As String = "Select distinct(id),name from USRGROUP inner join usr_Rights On usr_Rights.groupid = USRGROUP.id where ADITIONAL = " & wfstepid & " And objid=" & ObjectTypes.WFSteps & " order by name"
        Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Return dstemp.Tables(0)
    End Function

    Public Shared Function GetStepUserGroupsIdsByUserID(ByVal wfstepid As Int64, ByVal userID As Int64) As DataTable
        Dim Strselect As New StringBuilder

        Strselect.Append("Select distinct(i.groupid) from ")


        Strselect.Append("(Select usrid, groupid from usr_r_group ")
            Strselect.Append("Union Select usrid, InheritedUserGroup ")
            Strselect.Append("from usr_r_group inner join group_r_group On UserGroup=groupid) ")


            Strselect.Append(" i inner join usr_Rights r On r.groupid = i.groupid where ADITIONAL = " & wfstepid & " And usrID = " & userID & " And objid=" & ObjectTypes.WFSteps)

        Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect.ToString())

        Return dstemp.Tables(0)
    End Function




    Public Shared Function GetUserSteps(ByVal CurrentUser As IUser) As SortedList
        Try
            Dim SL As New SortedList
            Dim StrSelect As String = "Select * from wfstep"
            Dim where As String = " where wfstep.step_id In (Select aditional from USR_RIGHTS where (groupid in (" & Zamba.Membership.MembershipHelper.CurrentUser.ID & ","

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
            For Each r As DsSteps.WFStepsRow In DsSteps.WFSteps
                Try
                    Dim wfstep As New WFStep(Nothing, r.Step_Id, r.Name, r.Help, r.Description, New Drawing.Point(r.LocationX, r.LocationY), r.ImageIndex, r.Max_Docs, r.Max_Hours, r.StartAtOpenDoc, "", 50, 150, 0, 0)
                    SL.Add(wfstep.ID, wfstep)
                    WFStepStatesFactory.FillState(wfstep, DsStates)
                Catch ex As Exception
                    raiseerror(ex)
                End Try
            Next
            Return SL
        Catch ex As Exception
            raiseerror(ex)
        End Try
        Return Nothing
    End Function

    Public Shared Function FillSteps(ByVal WF As WorkFlow) As DsSteps
        Dim Dstemp As DataSet = Nothing
        Dim DsSteps As New DsSteps
        Try
            If Server.isOracle Then
                '''Dim parNames() As Object = {"pWorkId", "io_cursor"}
                ''Dim parTypes() As Object = {13, 5}
                'Dim ParValues() As Object = {WF.ID, 2}
                'Dstemp = Server.Con.ExecuteDataset("ZWFStepsFactory_pkg.ZGetWFStepByWorkId",  ParValues)
                Dim StrSelect As String = "Select * from wfstep where work_id=" & WF.ID & " order by orden"
                Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
            Else
                Dim ParValues() As Object = {WF.ID}
                Dstemp = Server.Con.ExecuteDataset("Zsp_workflow_300_FillSteps", ParValues)
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try

        Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
        DsSteps.Merge(Dstemp)

        Return DsSteps

    End Function

    '<summary>
    '<Modified>
    'Taskount Item append Value, fixed with new SP
    '</Modified>
    '</summary>
    'Alejandro Ruetalo

    Public Shared Sub FillStepsWithTaskCount(ByVal WF As WorkFlow, ByVal userId As Int64)
        Try
            Dim Dstemp As DataSet = Nothing
            Dim DsSteps As New DsSteps
            Try
                If Server.isOracle Then
                    Dim StrSelect As String = "Select * from wfstep where work_id=" & WF.ID
                    Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
                Else
                    Dim ParValues() As Object = {WF.ID, userId}
                    Dstemp = Server.Con.ExecuteDataset("Zsp_workflow_200_FillSteps", ParValues)
                End If
            Catch ex As Exception
                raiseerror(ex)
            End Try

            Dstemp.Tables(0).TableName = DsSteps.WFSteps.TableName
            DsSteps.Merge(Dstemp)


            Dim DsStates As DsStepState = WFStepStatesFactory.GetAllStates(DsSteps)

            For Each r As DsSteps.WFStepsRow In DsSteps.WFSteps.Rows

                Try
                    Dim wfstep As New WFStep(WF.ID, r.Step_Id, r.Name, r.Help, r.Description, New Drawing.Point(r.LocationX, r.LocationY), r.ImageIndex, r.Max_Docs, r.Max_Hours, r.StartAtOpenDoc, r.Color, r.Width, r.Height, 0, 0)

                    wfstep.TasksCount = r.Item("TaskCount")

                    If WF.Steps.ContainsKey(wfstep.ID) Then
                        WF.Steps(wfstep.ID) = wfstep
                    Else
                        WF.Steps.Add(wfstep.ID, wfstep)
                    End If

                    WFStepStatesFactory.FillState(WF.Steps(wfstep.ID), DsStates)

                Catch ex As Exception
                    raiseerror(ex)
                End Try
            Next

            'Seteo Estapa Inicial
            WF.SetInitialStep()
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "ABM"
    ''' <summary>
    ''' Elimina una etapa
    ''' </summary>
    ''' <param name="WFStep">Objeto WFSTEP que se desea eliminar</param>
    ''' <remarks></remarks>
    Public Shared Sub DelStep(ByRef wfstep As WFStep)
        If Server.ServerType = DBTYPES.MSSQLServer7Up Then
            Dim parvalues() As Object = {wfstep.ID}
            Server.Con.ExecuteNonQuery("zsp_workflow_100_DeleteStepById", parvalues)
            parvalues = Nothing
        Else
            Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE wfSTEP where STEP_id = " & wfstep.ID)
        End If
    End Sub

    ''' <summary>
    ''' Actualiza una etapa
    ''' </summary>
    ''' <param name="WFStep">Objeto WFSTEP que se desea actualizar</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	09/06/2008	Modified    Se comento el procedimiento almacenado (inexistente) de Oracle
    ''' </history>
    Public Shared Sub UpdateStep(ByRef wfstep As WFStep)
        'Se corrige el error de los campos nulos
        If String.IsNullOrEmpty(wfstep.Description) Then
            wfstep.Description = "Descripcion"
        End If
        If String.IsNullOrEmpty(wfstep.Help) Then
            wfstep.Help = "Ayuda"
        End If
        If String.IsNullOrEmpty(wfstep.color) Then
            wfstep.color = " "
        End If

        Dim ParValues() As Object = {wfstep.Name, wfstep.Description, wfstep.Help, wfstep.EditDate, wfstep.IconId, wfstep.Location.X, wfstep.Location.Y, wfstep.StartAtOpenDoc, wfstep.MaxHours, wfstep.MaxDocs, wfstep.ID}

        If Server.isOracle Then
            Dim query As New StringBuilder
            query.Append("UPDATE WFSTEP Set Name = '")
            query.Append(wfstep.Name)
            query.Append("', Description = '")
            query.Append(wfstep.Description)
            query.Append("', Help = '")
            query.Append(wfstep.Help)
            query.Append("', EditDate = " & Server.Con.ConvertDate(wfstep.EditDate) & ", ImageIndex = ")
            query.Append(wfstep.IconId & ", LocationX = " & wfstep.Location.X & ", ")
            query.Append("LocationY = " & wfstep.Location.Y & ", StartAtopenDoc = ")
            If wfstep.StartAtOpenDoc Then
                query.Append("1")
            Else
                query.Append("0")
            End If
            query.Append(", Max_Hours = " & wfstep.MaxHours & ", ")
            query.Append("Max_Docs = " & wfstep.MaxDocs & " where step_id = " & wfstep.ID)
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())
        Else
            Server.Con.ExecuteNonQuery("ZUpdWFStepByStepId", ParValues)
        End If

    End Sub

    ''' <summary>
    ''' Guarda en la base la posicion del Icono step, solo para mantener la apariencia
    ''' visual de las etapas
    ''' </summary>
    ''' <param name="WFStep">Objeto WFStep con las propiedades X e Y modificadas. X e Y representan un punto en el plano</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateStepPosition(ByRef wfstep As WFStep)
        If Server.isSQLServer Then
            Dim parvalues() As Object = {wfstep.Location.X, wfstep.Location.Y, wfstep.ID}
            Server.Con.ExecuteNonQuery("Zsp_workflow_100_SaveIcon", parvalues)
            parvalues = Nothing
        Else
            Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE WFSTEP set LocationX = " & wfstep.Location.X & ", LocationY = " & wfstep.Location.Y & " where step_id = " & wfstep.ID)
        End If
    End Sub
    ''' <summary>
    ''' Guarda en la base el color del Icono step
    ''' </summary>
    Public Shared Sub UpdateStepColor(ByVal color As String, ByVal ID As Long)
        'Se corrige el error de los campos nulos
        If String.IsNullOrEmpty(color) Then
            color = " "
        End If
        If Server.isSQLServer Then
            Dim parvalues() As Object = {color, ID}
            Server.Con.ExecuteNonQuery("Zsp_workflow_100_UpdateColor", parvalues)
        Else
            Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE WFSTEP set Color = '" & color & "' where step_id = " & ID)
        End If
    End Sub
    'Public Shared Sub UpdateStepImage(byref wfstep As WFStep)
    '    ' Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE WFSTEP set ImagePath = '" & WFStep.ImagePath & "' where step_id = " & WFStep.Id)
    'End Sub

    ''' <summary>
    ''' Persiste en la base la nueva Etapa asignado a un workflow
    ''' </summary>
    ''' <param name="WF">Objeto Workflow que contiene la etapa</param>
    ''' <param name="Name">Nombre de la etapa</param>
    ''' <param name="Help">Texto de ayuda</param>
    ''' <param name="Description">Descripcion de la etapa</param>
    ''' <param name="Location">Punto en el plano para situar el icono de la etapa y persistir la imagen</param>
    ''' <param name="ImageIndex">Atributo del icono</param>
    ''' <param name="MaxDocs">Maxima cantidad de documentos permitidos para la etapa</param>
    ''' <param name="MaxHours">Cantidad maxima de horas que puede permanecer un documento en esta etapa</param>
    ''' <param name="StartAtOpenDoc">Valor Verdadero/Falso que indica si la tarea comienza en forma automatica al abrir el documento</param>
    ''' <param name="ImagePath">Ruta a la imagen</param>
    ''' <returns>Objeto WFStep</returns>
    ''' <remarks></remarks>
    Public Shared Function NewStep(ByVal WF As WorkFlow, ByVal Name As String, ByVal Help As String, ByVal Description As String, ByVal Location As Drawing.Point, ByVal ImageIndex As Int32, ByVal MaxDocs As Int32, ByVal MaxHours As Int32, ByVal StartAtOpenDoc As Boolean, ByVal ImagePath As String) As WFStep
        Dim StepId As Int32 = CoreData.GetNewID(IdTypes.WFSTEP)
        If String.IsNullOrEmpty(Description) Then
            Description = "Descripcion"
        End If
        If String.IsNullOrEmpty(Help) Then
            Help = "Ayuda"
        End If
        'Dim WFStep As New WFStep(WF, StepId, Name, Help, Description, Location, ImageIndex, MaxDocs, MaxHours, StartAtOpenDoc, ImagePath)
        Dim WFStep As New WFStep(WF.ID, StepId, Name, Help, Description, Location, ImageIndex, MaxDocs, MaxHours, StartAtOpenDoc, "", 50, 150, 0, 0)
        If Server.isSQLServer Then
            Dim parvalues() As Object = {WF.ID, StepId, Name, WF.Description, WF.Help, ImageIndex, Location.X, Location.Y, MaxDocs, MaxHours, (StartAtOpenDoc), "Red", 150, 50}
            Server.Con.ExecuteNonQuery("Zsp_workflow_200_InsertWFStep", parvalues)
        Else
            Dim sql As String = "INSERT INTO WFSTEP (work_id,STEP_ID,Name,Description,Help,CreateDate,EditDate,ImageIndex,LocationX, LocationY,Max_Docs,Max_Hours,StartAtOpenDoc) VALUES (" & WF.ID & "," & StepId & ",'" & Name & "','',''," & Server.Con.ConvertDateTime(Now.ToString) & "," & Server.Con.ConvertDateTime(Now.ToString) & "," & ImageIndex & "," & Location.X & "," & Location.Y & "," & MaxDocs & "," & MaxHours & "," & (StartAtOpenDoc) & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        End If
        Return WFStep
    End Function
    ''' <summary>
    ''' Persiste en la base la nueva Etapa asignado a un workflow
    ''' </summary>
    ''' <param name="WF">Objeto Workflow que contiene la etapa</param>
    ''' <param name="Name">Nombre de la etapa</param>
    ''' <param name="Help">Texto de ayuda</param>
    ''' <param name="Description">Descripcion de la etapa</param>
    ''' <param name="Location">Punto en el plano para situar el icono de la etapa y persistir la imagen</param>
    ''' <param name="ImageIndex">Atributo del icono</param>
    ''' <param name="MaxDocs">Maxima cantidad de documentos permitidos para la etapa</param>
    ''' <param name="MaxHours">Cantidad maxima de horas que puede permanecer un documento en esta etapa</param>
    ''' <param name="StartAtOpenDoc">Valor Verdadero/Falso que indica si la tarea comienza en forma automatica al abrir el documento</param>
    ''' <remarks></remarks>
    Public Shared Function InsertStep(ByVal WFID As Int64, ByVal Name As String, ByVal Help As String, ByVal Description As String, ByVal Location As Drawing.Point, ByVal ImageIndex As Int32, ByVal MaxDocs As Int32, ByVal MaxHours As Int32, ByVal Initial As Boolean) As Int64
        Dim StepId As Int32 = CoreData.GetNewID(IdTypes.WFSTEP)
        'Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFSTEP (work_id,STEP_ID,Name,Description,Help,CreateDate,EditDate,ImageIndex,LocationX, LocationY,Max_Docs,Max_Hours,StartAtOpenDoc)VALUES (" & WF.ID & "," & StepId & ",'" & Name & "','" & Description & "','" & Help & "'," & Server.Con.ConvertDateTime(Now.ToString) & "," & Server.Con.ConvertDateTime(Now.ToString) & "," & ImageIndex & "," & Location.X & "," & Location.Y & "," & MaxDocs & "," & MaxHours & ",0)")
        'Se corrige el error de los campos nulos
        If String.IsNullOrEmpty(Description) Then
            Description = "Descripcion"
        End If
        If String.IsNullOrEmpty(Help) Then
            Help = "Ayuda"
        End If
        Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFSTEP (work_id,STEP_ID,Name,Description,Help,CreateDate,EditDate,ImageIndex,LocationX, LocationY,Max_Docs,Max_Hours,StartAtOpenDoc,Color,Width,Height)VALUES (" & WFID & "," & StepId & ",'" & Name & "','" & Description & "','" & Help & "'," & Server.Con.ConvertDateTime(Now.ToString) & "," & Server.Con.ConvertDateTime(Now.ToString) & "," & ImageIndex & "," & Location.X & "," & Location.Y & "," & MaxDocs & "," & MaxHours & ",0" & "," & "' '" & "," & 150 & "," & 50 & ")")
        If Initial Then
            UpdateInitialStep(WFID, StepId)
        End If
        Return StepId
    End Function
    Public Shared Sub InsertStep(ByVal wfstep As WFStep)
        Dim Initial As Byte
        If wfstep.StartAtOpenDoc Then
            Initial = 1
        Else
            Initial = 0
        End If
        'Se corrige el error de los campos nulos
        If String.IsNullOrEmpty(wfstep.Description) Then
            wfstep.Description = "Descripcion"
        End If
        If String.IsNullOrEmpty(wfstep.Help) Then
            wfstep.Help = "Ayuda"
        End If
        If String.IsNullOrEmpty(wfstep.color) Then
            wfstep.color = " "
        End If

        Dim sql As String = "INSERT INTO WFSTEP (work_id,STEP_ID,Name,Description,Help,CreateDate,EditDate,ImageIndex,LocationX, LocationY,Max_Docs,Max_Hours,StartAtOpenDoc,Color,Width,Height)VALUES (" & wfstep.WorkId & "," & wfstep.ID & ",'" & wfstep.Name & "','" & wfstep.Description & "','" & wfstep.Help & "'," & Server.Con.ConvertDateTime(Now.ToString) & "," & Server.Con.ConvertDateTime(Now.ToString) & ",0," & wfstep.Location.X & "," & wfstep.Location.Y & "," & wfstep.MaxDocs & "," & wfstep.MaxHours & "," & Initial & ",'" & wfstep.color & "'," & wfstep.Width & "," & wfstep.Height & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub
    Public Shared Sub InsertNewStep(ByVal wfstep As WFStep)
        Dim Initial As Byte
        'Se corrige el error de los campos nulos
        If String.IsNullOrEmpty(wfstep.Description) Then
            wfstep.Description = "Descripcion"
        End If
        If String.IsNullOrEmpty(wfstep.Help) Then
            wfstep.Help = "Ayuda"
        End If
        If String.IsNullOrEmpty(wfstep.color) Then
            wfstep.color = " "
        End If
        Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFSTEP (work_id,STEP_ID,Name,Description,Help,CreateDate,EditDate,ImageIndex,LocationX, LocationY,Max_Docs,Max_Hours,StartAtOpenDoc,Color,Width,Height)VALUES (" & wfstep.WorkId & "," & wfstep.ID & ",'" & wfstep.Name & "','" & wfstep.Description & "','" & wfstep.Help & "'," & Server.Con.ConvertDateTime(Now.ToString) & "," & Server.Con.ConvertDateTime(Now.ToString) & ",0," & wfstep.Location.X & "," & wfstep.Location.Y & "," & wfstep.MaxDocs & "," & wfstep.MaxHours & "," & Initial & "," & wfstep.color & "," & wfstep.Width & "," & wfstep.Height & ")")
    End Sub
#End Region

    ''' <summary>
    ''' Cambia la fecha de expiracion de una tarea
    ''' </summary>
    ''' <param name="Result">Objeto TaskResult con la fecha de expiración modificada</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateExpiredDateTask(ByRef Result As TaskResult)
        If Server.isSQLServer Then
            'TODO verificar el funcionamiento
            Dim parvalues() As Object = {Result.ID, Result.ExpireDate}
            Server.Con.ExecuteNonQuery("Zsp_workflow_100_UpdateExpiredDateTask", parvalues)
        Else
            Dim strupdate As String = "UPDATE WFDOCUMENT SET EXPIREDATE=" & Server.Con.ConvertDateTime(Result.ExpireDate) & " WHERE DOC_ID = " & Result.ID
            Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
        End If
        'SP 4/1/2006
        'Try
        '    if Server.IsOracle then
        '        Dim ParValues() As Object = {Server.Con.ConvertDateTime(Result.ExpireDate), Result.Id}
        '        ''Dim parNames() As Object = {"pExpDate", "pDocId"}
        '        'Dim parTypes() As Object = {7, 13}
        '        Server.Con.ExecuteNonQuery("ZWFUpdStepsFactory_pkg.ZUpdWFDocExpireDateByDocId",  ParValues)
        '    Else
        '        Dim ParValues() As Object = {Server.Con.ConvertDateTime(Result.ExpireDate), Result.Id}
        '        Server.Con.ExecuteNonQuery("ZUpdWFDocExpireDateByDocId", ParValues)
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub
    ''' <summary>
    ''' Obtiene la cantidad de tareas para una etapa
    ''' </summary>
    ''' <param name="StepId">ID de la etapa</param>
    ''' <returns>Cantidad de tareas dentro de la etapa</returns>
    ''' <remarks></remarks>

    Public Shared Function HasDocuments(ByVal documentId As Integer) As Boolean
        Dim documentCount As Integer = GetDocumentCountByStepId(documentId)
        If documentCount = 0 Then
            Return False
        ElseIf documentCount > 0 Then
            Return True
        Else 'The account of documents by Step cannot be negative , therefore this is an exception
            Dim ex As New Exception("Error al contabilizar la cantidad de documentos por etapa")
            raiseerror(ex)
        End If
    End Function

    Public Shared Function CheckTransicions(ByRef wfstep As WFStep) As ArrayList
        Dim Destiny As New DsRules.WFRulesDataTable
        Dim DestinySteps As New ArrayList

        If IsNothing(wfstep.DsRules) = False Then
            For Each R As DsRules.WFRulesRow In wfstep.DsRules.WFRules.Rows
                If IsNothing(R) = False Then CheckRules(R, Destiny, wfstep.DsRules)
            Next
        End If

        Dim DVDSRulesParams As New DataView(wfstep.DsRules.WFRuleParamItems)
        For Each R As DsRules.WFRulesRow In Destiny.Rows
            DVDSRulesParams.RowFilter = "rule_id = " & R.Id & " and itemid = 0"
            For Each RuleParamRow As DsRules.WFRuleParamItemsRow In DVDSRulesParams.ToTable().Rows ' wfstep.Rules
                DestinySteps.Add(RuleParamRow.Value)
            Next
        Next

        DVDSRulesParams.Dispose()
        DVDSRulesParams = Nothing

        Return DestinySteps
    End Function

    Private Shared Sub CheckRules(ByRef rule As DsRules.WFRulesRow, ByVal Destiny As DsRules.WFRulesDataTable, dsrules As DsRules)
        If rule.Name.ToLower = "dodistribuir" Then
            Destiny.AddWFRulesRow(rule)
        Else

            Dim DVDSRules As New DataView(dsrules.WFRules)
            DVDSRules.RowFilter = "ParentId = " & rule.Id
            For Each RuleRow As DataRow In DVDSRules.ToTable().Rows
                CheckRules(RuleRow, Destiny, dsrules)
            Next
            DVDSRules.Dispose()
            DVDSRules = Nothing

        End If
    End Sub

    'Comente este metodo porque cree una sobrecarga que recibe solo ids - MC
    ''' <summary>
    ''' Actualiza la etapa inicial dentro de un Workflow
    ''' </summary>
    ''' <param name="WF">Objeto Workflow con el initialStep modificado</param>D:\Zamba2007\Zamba.WFControls\WF\TasksCtls\UCTaskViewer.vb
    ''' <remarks></remarks>
    'Public Shared Sub UpdateInitialStep(ByVal WF As WorkFlow)
    '    If Server.ServerType = DBTYPES.MSSQLServer7Up OrElse Server.ServerType = DBTYPES.MSSQLServer Then
    '        Dim parvalues() As Object = {WF.InitialStep.ID, WF.ID}
    '        Server.Con.ExecuteNonQuery("Zsp_workflow_100_UpdateInitialStep", parvalues)
    '    Else
    '        Dim sql As String = "Update WFWorkflow set InitialStepId=" & WF.InitialStep.ID & " where work_id=" & WF.ID
    '        sql = Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    '    End If
    'End Sub

    ''' <summary>
    ''' Actualiza la etapa inicial dentro de un Workflow
    ''' </summary>
    ''' <param name="WF">Objeto Workflow con el initialStep modificado</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateInitialStep(ByVal WFId As Int64, ByVal InitialStepId As Int64)
        If Server.isSQLServer Then
            Dim parvalues() As Object = {InitialStepId, WFId}
            Server.Con.ExecuteNonQuery("Zsp_workflow_100_UpdateInitialStep", parvalues)
        Else
            Dim sql As String = "Update WFWorkflow set InitialStepId=" & InitialStepId & " where work_id=" & WFId
            sql = Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        End If
    End Sub

    ''' <summary>
    ''' Actualiza e orden de la etapa
    ''' </summary>
    ''' <param name="StepID">ID de la etapa</param>
    ''' <param name="Orden">Orden de la etapa</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateStepOrder(ByVal StepId As Int64, ByVal Orden As Int64)
        If Server.isSQLServer Then
            Dim parvalues() As Object = {StepId, Orden}
            Server.Con.ExecuteNonQuery("zsp_100_workflow_setStepOrder ", parvalues)
        Else
            Dim sql As String = "Update WFStep set Orden=" & Orden & " where step_id=" & StepId
            sql = Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        End If
    End Sub

    ''' <summary>
    ''' Actualiza la etapa para poner el valor maximo de orden
    ''' </summary>
    ''' <param name="StepID">ID de la etapa</param>
    ''' <param name="WorkID">ID del workflow</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateMaxStepOrder(ByVal StepId As Int64, ByVal WorkId As Int64)
        If Server.isSQLServer Then
            Dim parvalues() As Object = {StepId, WorkId}
            Server.Con.ExecuteNonQuery("zsp_100_workflow_setMaxStepOrder ", parvalues)
        Else
            Dim sql As String = "Update WFStep set Orden=(select max(orden) + 1 from wfstep where work_id=" & WorkId & ") where step_id=" & StepId
            sql = Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        End If
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
        Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE FROM WFSTEPOPT Where StepId = " & stepId & " AND ObjOne = " & typeOfPermit)
    End Sub

    ''' <summary>
    ''' Método que inserta en la tabla WFStepOpt un tipo de permiso 
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
        Dim str As String = "INSERT INTO WFSTEPOPT (StepId, ObjOne, objTwo, ObjExtraData) VALUES (" & stepId & ", " & typeOfPermit & ", " & objTwo & ", '" & objExtraData & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, str)
    End Sub

    ''' <summary>
    ''' Método que recupera los tipos de permiso según un id de etapa
    ''' </summary>
    ''' <param name="stepId">Id de etapa seleccionada de un determinado workflow (administrador)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    23/10/2008  Created        
    '''     Marcelo     24/06/2009  Modified
    ''' </history>
    Public Shared Function getTypesOfPermit(ByVal stepId As Long, ByVal typeOfPermision As TypesofPermits) As DataSet
        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM WFSTEPOPT Where StepId = " & stepId & " and objOne = " & typeOfPermision)
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
    Public Shared Function GetDocTypesAssociatedToWFbyStepId(ByVal stepId As Int64) As DataTable
        Dim Ds As DataSet = Nothing

        If Server.isOracle Then
            Dim query As New StringBuilder

            query.Append("Select DOC_TYPE_ID FROM Zwfviewwfdoctypes wf ")
            query.Append(" INNER JOIN WFStep ws on wf.wfid = ws.work_id ")
            query.Append(" WHERE ws.step_Id = " & stepId.ToString)
            Ds = Server.Con.ExecuteDataset(CommandType.Text, query.ToString())
        Else
            Dim parameters() As Object = {(stepId)}
            Ds = Server.Con.ExecuteDataset("zsp_workflow_100_GetDocTypesAssociatedToWFByStepId", parameters)
        End If

        If Ds.Tables.Count = 1 Then
            Return Ds.Tables(0)
        End If

        Return Nothing
    End Function

    Public Overrides Sub Dispose()

    End Sub

End Class