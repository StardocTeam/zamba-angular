Imports Zamba.Servers
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Servers.Server
Imports Zamba.Core.ZClass
Imports System.Text
Imports System.Collections.Generic

'Imports Zamba.Users.Factory
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Data
''' Class	 : Data.WFFactory
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Factory de WorkFlows
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
'''     [Gaston]    23/04/2008  Modified
''' </history>
''' -----------------------------------------------------------------------------

Public Class WFFactory

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
        Dim StrSelect As String
        If Server.isOracle Then
            StrSelect = "SELECT work_id, Wstat_id, Name, nvl(Description,''), nvl(Help,''), CreateDate, EditDate, Refreshrate, InitialStepId FROM WFworkflow order by name"
        Else
            StrSelect = "SELECT work_id, Wstat_id, Name, Description, Help, CreateDate, EditDate, Refreshrate, InitialStepId FROM WFworkflow order by name"
        End If

        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Dim Ds As New DsWF

        Dstemp.Tables(0).TableName = Ds.WF.TableName
        Ds.Merge(Dstemp)

        Dim WFs(Ds.WF.Count - 1) As WorkFlow

        Dim i As Int16
        For Each r As DsWF.WFRow In Ds.WF.Rows
            Dim wf As New WorkFlow(r.Work_ID, r.Name, r.Description, r.Help, r.WStat_Id, r.CreateDate, r.EditDate, r.RefreshRate, r.InitialStepId)
            WFs(i) = wf
            i += 1
        Next
        Return WFs
    End Function
    ''' <summary>
    ''' Get WFId of the Document
    ''' </summary>
    ''' <param name="docId">docTypeId</param>
    ''' <history>Pablo 30/11/09 Created </history>
    ''' <remarks></remarks>
    Public Function GetWFAssociationByDocTypeId(ByVal docTypeId As Int64) As Int64
        Dim QueryBuilder As New StringBuilder
        Try
            QueryBuilder.Append("select wfId from wf_dt where docTypeId=")
            QueryBuilder.Append(docTypeId.ToString())

            Return Server.Con.ExecuteScalar(CommandType.Text, QueryBuilder.ToString())
        Finally
            QueryBuilder.Remove(0, QueryBuilder.Length)
            QueryBuilder = Nothing
        End Try
    End Function



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que obtiene los WorkFlows para los cuales el usuario actual tiene permisos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	18/08/2008	Created
    '''     [Tomas]     18/06/2009  Modified    Se modifica el método completo para trabajar con procedimientos.
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetUserWFIdsAndNames(ByVal userId As Int64) As DataSet
        Dim Ds As DataSet = Nothing
        If Server.isOracle Then
            Dim parNames() As String = {"user_id", "io_cursor"}
            ' Dim parTypes() As Object = {13, 5}
            Dim parValues() As Object = {userId, 2}
            Ds = Server.Con.ExecuteDataset("zsp_workflow_100.GetUserWFIdsAndNames", parValues)

        Else
            Dim parameters() As Object = {(userId)}
            Ds = Server.Con.ExecuteDataset("zsp_workflow_100_GetUserWFIdsAndNames", parameters)

        End If
        Return Ds
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
    Public Function GetWFToAddDocuments(ByVal wfid As Int32) As WorkFlow
        Dim UsrKey As New System.Text.StringBuilder()
        UsrKey.Append("SELECT work_id, Wstat_id, Name, Description, Help, CreateDate, EditDate, Refreshrate, InitialStepId  FROM WFworkflow where work_id = " & wfid)

        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, UsrKey.ToString)
        Dim Ds As New DsWF

        Dstemp.Tables(0).TableName = Ds.WF.TableName
        Ds.Merge(Dstemp)

        Dim wf As WorkFlow = Nothing
        For Each r As DsWF.WFRow In Ds.WF.Rows
            wf = New WorkFlow(r.Work_ID, r.Name, r.Description, r.Help, r.WStat_Id, r.CreateDate, r.EditDate, r.RefreshRate, r.InitialStepId)
        Next
        Return wf
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
    '''     [Gaston]	14/10/2008	Modified    Si el usuario no tiene grupos se los busca
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetWFsToAddDocuments() As ArrayList

        Dim UsrKey As New System.Text.StringBuilder
        UsrKey.Append("SELECT work_id, Wstat_id, Name, Description, Help, CreateDate, EditDate, Refreshrate, InitialStepId FROM WFworkflow where work_id in (select aditional from USR_RIGHTS where objid=55 and rtype=40")

        If (RightFactory.CurrentUser.Groups.Count > 0) Then

            UsrKey.Append(" and (")

            For Each g As IUserGroup In RightFactory.CurrentUser.Groups
                UsrKey.Append("groupid=")
                UsrKey.Append(g.ID.ToString)
                UsrKey.Append(" or ")
            Next

            'borro el ultimo or
            UsrKey.Remove(UsrKey.Length - 4, 4)
            UsrKey.Append("))")

        Else



            If (RightFactory.CurrentUser.Groups.Count > 0) Then

                UsrKey.Append(" and (")

                For Each g As IUserGroup In RightFactory.CurrentUser.Groups
                    UsrKey.Append("groupid=")
                    UsrKey.Append(g.ID.ToString)
                    UsrKey.Append(" or ")
                Next

                UsrKey.Remove(UsrKey.Length - 4, 4)
                UsrKey.Append("))")

            Else
                Return (Nothing)
            End If

        End If

        Dim Dstemp As DataSet
        Dim Ds As New DsWF
        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, UsrKey.ToString)
        Dstemp.Tables(0).TableName = Ds.WF.TableName
        Ds.Merge(Dstemp)

        'WF
        Dim WFs As New ArrayList

        ' Dim i As Int16
        For Each r As DsWF.WFRow In Ds.WF.Rows
            Dim wf As New WorkFlow(r.Work_ID, r.Name, r.Description, r.Help, r.WStat_Id, r.CreateDate, r.EditDate, r.RefreshRate, r.InitialStepId)

            'TODO: VALIDO QUE NO ESTE EN CERO EL INICIALSTEPID, PUEDE ESTAR EN CERO? O HAY QUE ASIGNARLE UN INICIALSTEPID SIEMPRE?
            If r.InitialStepId <> 0 Then
                wf.InitialStep = WFStepsFactory.GetStep(wf, r.InitialStepId)
            End If
            WFs.Add(wf)
        Next

        Return WFs

    End Function
    Public Function GetWf(ByVal r As DsWF.WFRow) As WorkFlow
        If IsDBNull(r.Description) Then
            r.Description = String.Empty
        End If
        Return New WorkFlow(r.Work_ID, r.Name, r.Description, r.Help, r.WStat_Id, r.CreateDate, r.EditDate, r.RefreshRate, r.InitialStepId)
    End Function
    'NO COMENTAR - POR FAVOR... [.:OSCAR:.]
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Dataset tipeado con las propiedades del workflow en base al ID
    ''' </summary>
    ''' <param name="WfId">Id del Workflow</param>
    ''' <returns>Dataset DsWF</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetWfById(ByVal WfId As Int32) As DsWF
        'Dim StrSelect As String = "Select * from ZViewWF where work_id = " & WfId
        Dim StrSelect As String
        StrSelect = "Select work_id, Wstat_id, Name, Description, Help, CreateDate, EditDate, Refreshrate, InitialStepId from WFWorkflow where work_id = " & WfId
        Dim Dstemp As DataSet
        Dim DsWF As New DsWF
        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Dstemp.Tables(0).TableName = DsWF.WF.TableName
        DsWF.Merge(Dstemp)
        Return DsWF
    End Function
    Public Function GetWfByIdAsDataSet(ByVal WfId As Int32) As DataSet
        'Dim StrSelect As String = "Select * from ZViewWF where work_id = " & WfId
        Dim StrSelect As String
        StrSelect = "Select work_id, Wstat_id, Name, Description, Help, CreateDate, EditDate, Refreshrate, InitialStepId from WFWorkflow where work_id = " & WfId
        Dim Dstemp As DataSet
        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Return Dstemp
    End Function
    Public Function GetWorkflowIdByStepId(ByVal stepId As Int64) As Int64
        Dim wfId As Int64 = 0
        Dim QueryBuilder As New System.Text.StringBuilder()
        QueryBuilder.Append("SELECT work_id FROM WFStep WHERE step_Id = ")
        QueryBuilder.Append(stepId.ToString())

        wfId = Server.Con.ExecuteScalar(CommandType.Text, QueryBuilder.ToString)
        Return wfId
    End Function
    Public Function GetWorkflowNameByWFId(ByVal workId As Int64) As String
        Dim wfName As String
        Dim QueryBuilder As New System.Text.StringBuilder()
        QueryBuilder.Append("SELECT name FROM WFWorkflow WHERE work_Id = ")
        QueryBuilder.Append(workId.ToString())

        wfName = Server.Con.ExecuteScalar(CommandType.Text, QueryBuilder.ToString)
        Return wfName
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

    Public Function GetWfNameById(ByVal WfId As Int32) As String
        Dim oDsWf As DsWF
        oDsWf = GetWfById(WfId)
        If Not oDsWf.WF(0) Is Nothing And IsDBNull(oDsWf.WF(0).Item("Name")) = False Then
            Return oDsWf.WF(0).Item("Name")
        Else
            Return String.Empty
        End If
    End Function
    '''' -----------------------------------------------------------------------------
    '''' <summary>
    '''' Obtiene un Dataset tipeado con las propiedades del workflow en base al ID
    '''' </summary>
    '''' <param name="WfId">Id del Workflow</param>
    '''' <returns>Dataset DsWF</returns>
    '''' <remarks>
    '''' </remarks>
    '''' <history>
    '''' 	[Hernan]	29/05/2006	Created
    '''' </history>
    '''' -----------------------------------------------------------------------------
    'Public Function GetWF(ByVal WfId As Int32) As DsWF
    '    'Dim StrSelect As String = "Select * from ZViewWF where work_id = " & WfId
    '    Dim StrSelect As String = "Select * from Zvw_ZViewWF_100 where work_id = " & WfId
    '    Dim Dstemp As DataSet
    '    Dim DsWF As New DsWF
    '    Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
    '    Dstemp.Tables(0).TableName = DsWF.WF.TableName
    '    DsWF.Merge(Dstemp)
    '    Return DsWF
    'End Function

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

        'TODO que solo me traiga los WF, que tiene permisos este usuario 
        Dim StrSelect As String
        StrSelect = "SELECT work_id, Wstat_id, Name, Description, Help, CreateDate, EditDate, Refreshrate, InitialStepId FROM WFWorkflow ORDER BY Name"
        Dim Dstemp As DataSet
        Dim DsWF As New DsWF
        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Dstemp.Tables(0).TableName = DsWF.WF.TableName
        DsWF.Merge(Dstemp)
        Return DsWF
    End Function

    '''-----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que devuelve un dataset tipeado con todos los Workflows para los cuales el usuario tiene permisos
    ''' </summary>
    ''' <param name="UserID">Id de Usuario</param>
    ''' <returns>Dataset DsWF</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	27/11/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetWFsByUserRightsEDIT(ByVal groupsIds As Generic.List(Of Int64)) As DsWF
        'Solo va a traer los workflows sobre el cual el usuario tiene permiso para EDITAR Y MONITORIAR- NO para AGREGAR DOCUMENTO
        Dim OnlyOnce As Boolean = False
        Dim count As Int32
        Dim query As New System.Text.StringBuilder()
        query.Append("select DISTINCT aditional from Usr_rights where objid = 55 and")
        query.Append(" rtype = 2 and ( groupid = ")
        For Each grpid As Int64 In groupsIds
            count += 1
            If OnlyOnce = False Then
                OnlyOnce = True
                query.Append(grpid.ToString)
                If count = groupsIds.Count Then query.Append(" )")
            Else
                query.Append(" OR groupid = " & grpid.ToString)
                If count = groupsIds.Count Then query.Append(" )")
            End If
        Next
        OnlyOnce = False
        Dim Dstemp As New DataSet
        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
        If Not IsNothing(Dstemp) AndAlso Not IsNothing(Dstemp) AndAlso Dstemp.Tables(0).Rows.Count > 0 Then
            Dim wfids As New Generic.List(Of Int64)
            For Each r As DataRow In Dstemp.Tables(0).Rows
                If Not IsDBNull(r.Item(0)) Then wfids.Add(Int64.Parse(r.Item(0).ToString))
            Next
            Dim StrSelect As String
            StrSelect = "SELECT work_id, Wstat_id, Name, Description, Help, CreateDate, EditDate, Refreshrate, InitialStepId FROM WFWorkflow "
            For Each wfid As Int64 In wfids
                If OnlyOnce = False Then
                    OnlyOnce = True
                    StrSelect += " Where work_id = " & wfid.ToString
                Else
                    StrSelect += " OR work_id = " & wfid.ToString
                End If
            Next
            StrSelect += " ORDER BY Name"
            Dim DsWF As New DsWF
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
            Dstemp.Tables(0).TableName = DsWF.WF.TableName
            DsWF.Merge(Dstemp)
            Return DsWF
        End If
        Return Nothing
    End Function

    '''-----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que devuelve un dataset tipeado con todos los Workflows para los cuales el usuario tiene permisos PARA MONITORERAR
    ''' </summary>
    ''' <param name="UserID">Id de Usuario</param>
    ''' <returns>Dataset DsWF</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	27/11/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetWFsByUserRightMONITORING(ByVal groupsIds As Generic.List(Of Int64)) As DsWF
        'Solo va a traer los workflows sobre el cual el usuario tiene permiso para MONITOREAR
        Dim OnlyOnce As Boolean = False
        Dim count As Int32
        Dim query As New System.Text.StringBuilder()
        query.Append("select DISTINCT aditional from Usr_rights where objid = 55 and")
        query.Append(" rtype = 39 and ( groupid = ")
        For Each grpid As Int64 In groupsIds
            count += 1
            If OnlyOnce = False Then
                OnlyOnce = True
                query.Append(grpid.ToString)
                If count = groupsIds.Count Then query.Append(" )")
            Else
                query.Append(" OR groupid = " & grpid.ToString)
                If count = groupsIds.Count Then query.Append(" )")
            End If
        Next
        OnlyOnce = False
        Dim Dstemp As New DataSet
        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
        If Not IsNothing(Dstemp) AndAlso Not IsNothing(Dstemp) AndAlso Dstemp.Tables(0).Rows.Count > 0 Then
            Dim wfids As New Generic.List(Of Int64)
            For Each r As DataRow In Dstemp.Tables(0).Rows
                If Not IsDBNull(r.Item(0)) Then wfids.Add(Int64.Parse(r.Item(0).ToString))
            Next
            Dim StrSelect As String
            StrSelect = "SELECT work_id, Wstat_id, Name, Description, Help, CreateDate, EditDate, Refreshrate, InitialStepId FROM WFWorkflow "
            For Each wfid As Int64 In wfids
                If OnlyOnce = False Then
                    OnlyOnce = True
                    StrSelect += " Where work_id = " & wfid.ToString
                Else
                    StrSelect += " OR work_id = " & wfid.ToString
                End If
            Next
            StrSelect += " ORDER BY Name"
            Dim DsWF As New DsWF
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
            Dstemp.Tables(0).TableName = DsWF.WF.TableName
            DsWF.Merge(Dstemp)
            Return DsWF
        End If
        Return Nothing
    End Function

    Public Function GetWorkflows() As DataSet
        Const query As String = "SELECT work_id, Wstat_id, Name, Description, Help, CreateDate, EditDate, Refreshrate, InitialStepId , (SELECT count(1) FROM WFDocument WHERE work_id = WFWorkflow.work_id ) as TaskCount , (SELECT count(1) FROM WFDocument WHERE expiredate < getdate() and work_id = WFWorkflow.work_id ) as ExpiredTasksCount FROM WFWorkflow ORDER BY Name"
        Return Server.Con.ExecuteDataset(CommandType.Text, query)
    End Function

    Public Function fillBalanceByWF(ByVal work_Id As Int32) As DataSet
        Dim ds As New DataSet
        ds = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, "SELECT COUNT(WFDocument.Task_State_id), WFTask_States.Task_State_Name FROM WFDocument, WFTask_States WHERE(work_id = " + work_Id.ToString + " And WFDocument.Task_State_ID = WFTask_States.Task_State_Id) GROUP BY WFTask_States.Task_State_Name")
        Return ds
    End Function


    Public Function GetRulesByStep() As DsRulesByStep
        Dim dsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "RulesByStep")
        Dim dsRulesByStep As New DsRulesByStep
        dsTemp.Tables(0).TableName = dsRulesByStep.Tables(0).TableName
        dsRulesByStep.Merge(dsTemp)
        Return dsRulesByStep
    End Function
    Public Function GetStepsByWF() As DStepsByWorkflow
        Dim dsTemp As New DataSet
        dsTemp = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "StepsByWF")
        Dim dsStepsByWorkflow As New DStepsByWorkflow
        dsTemp.Tables(0).TableName = "DsStepsByWorkflow"
        dsStepsByWorkflow.Merge(dsTemp)
        Return dsStepsByWorkflow
    End Function
    Public Function GetDocsByWF() As DsDocsbyWF
        Dim dsTemp As New DataSet
        dsTemp = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "DocsByWF")
        Dim dsDocsByWF As New DsDocsbyWF
        dsTemp.Tables(0).TableName = "DsDocsByWF"
        dsDocsByWF.Merge(dsTemp)
        Return dsDocsByWF
    End Function


    Public Function GetDelayed() As DsDelayedDocs
        Dim dsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "SP_WfDocDemorados")
        Dim dsDelayedDocs As New DsDelayedDocs
        dsTemp.Tables(0).TableName = dsDelayedDocs.Tables(0).TableName
        dsDelayedDocs.Merge(dsTemp)
        Return dsDelayedDocs
    End Function
    Public Function GetExpired() As DsExpiredDocs
        Dim dsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "ExpiredDocs")
        Dim dsExpiredDocs As New DsExpiredDocs
        dsTemp.Tables(0).TableName = dsExpiredDocs.Tables(0).TableName
        dsExpiredDocs.Merge(dsTemp)
        Return dsExpiredDocs
    End Function
    Public Function AsignedDocsByUser() As DsDocsByUser
        Dim dsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "DocsByUser")
        Dim dsDocsByUser As New DsDocsByUser
        dsTemp.Tables(0).TableName = dsDocsByUser.Tables(0).TableName
        dsDocsByUser.Merge(dsTemp)
        Return dsDocsByUser
    End Function
    Public Function GetDocumentsByStep() As DsDocsByStep
        Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "DocsByStep")
        Dim dsDocsByStep As New DsDocsByStep
        dstemp.Tables(0).TableName = dsDocsByStep.Tables(0).TableName
        dsDocsByStep.Merge(dstemp)
        Return dsDocsByStep
    End Function
    Public Function GetSteps() As DStepsByWorkflow
        Dim dsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "WFStepsByWF")
        Dim dsStepByWF As New DStepsByWorkflow
        dsTemp.Tables(0).TableName = dsStepByWF.Tables(0).TableName
        dsStepByWF.Merge(dsTemp)
        Return dsStepByWF
    End Function
    Public Function GetWFsByDocType() As DsWFsByDocType
        Dim dsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "WFsByDocType")
        Dim dsWFsByDocType As New DsWFsByDocType
        dsTemp.Tables(0).TableName = dsWFsByDocType.Tables(0).TableName
        dsWFsByDocType.Merge(dsTemp)
        Return dsWFsByDocType
    End Function
    Public Function GetWFStepsDetails() As DsWFStepDetails
        Dim dsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "WFStepDetails")
        Dim dsWFsStepDetails As New DsWFStepDetails
        dsTemp.Tables(0).TableName = dsWFsStepDetails.Tables(0).TableName
        dsWFsStepDetails.Merge(dsTemp)
        Return dsWFsStepDetails
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
        Return (Server.Con.ExecuteScalar(CommandType.Text, "SELECT InitialStepId FROM WFWorkflow Where work_id = " & wfId))
    End Function

#End Region

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
        Dim sql As String = String.Empty
        Try
            Dim DtWF As DsWF.WFDataTable = DsWf.WF.GetChanges(DataRowState.Added)
            'TODO pasar todo a Store Procedure
            If IsNothing(DtWF) = False Then
                For i As Int32 = 0 To DtWF.Count - 1
                    If Server.isOracle Then
                        'TODO: pasar a Store en Oracle
                        sql = "Insert into wfworkflow (work_id,Wstat_id,name,help,description,createdate,editdate,refreshrate,initialstepid) VALUES (" & DtWF(i).Work_ID & "," & DtWF(i).WStat_Id & ",'" & DtWF(i).Name & "','" & DtWF(i).Help & "','" & DtWF(i).Description & "'," & Server.Con.ConvertDateTime(DtWF(i).CreateDate) & "," & Server.Con.ConvertDateTime(DtWF(i).EditDate) & "," & DtWF(i).RefreshRate & "," & DtWF(i).InitialStepId & ")"
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                    Else
                        'Dim ParValues() As Object = {DtWF(i).Work_ID, DtWF(i).WStat_Id, DtWF(i).Name, DtWF(i).Help, DtWF(i).Description, Server.Con.ConvertDateTime(DtWF(i).CreateDate), Server.Con.ConvertDateTime(DtWF(i).EditDate), DtWF(i).RefreshRate, DtWF(i).InitialStepId}
                        Dim ParValues() As Object = {DtWF(i).Work_ID, DtWF(i).WStat_Id, DtWF(i).Name, DtWF(i).Help, DtWF(i).Description, DtWF(i).CreateDate, DtWF(i).EditDate, DtWF(i).RefreshRate, DtWF(i).InitialStepId}
                        Server.Con.ExecuteNonQuery("Zsp_workflow_100_InsWF", ParValues)
                        ParValues = Nothing
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New System.Exception(ex.Message)
        End Try
        'DtWF = DsWf.WF.GetChanges(DataRowState.Modified)
        'If IsNothing(DtWF) = False Then
        '    For i As Int32 = 0 To DtWF.Count - 1
        '        sql = "UPDATE wfworkflow SET wstat_id = " & DtWF(i).WStat_Id & ",name = '" & DtWF(i).Name & "',help = '" & DtWF(i).Help & "',description = '" & DtWF(i).Description & "',editdate = " & Server.Con.ConvertDateTime(DtWF(i).EditDate) & ",refreshrate = " & DtWF(i).RefreshRate & ",InitialStepId = " & DtWF(i).InitialStepId & " where work_id = " & DtWF(i).Work_ID
        '        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        '    Next
        'End If

        'SP 29/12/2005
        'If IsNothing(DtWF) = False Then
        '    For i = 0 To DtWF.Count - 1
        '        'update de las reglas modificadas
        '        Try
        '            Dim ParValues() As Object = {DtWF(i).WStat_Id, DtWF(i).Name, DtWF(i).Help, DtWF(i).Description, Server.Con.ConvertDateTime(DtWF(i).EditDate), DtWF(i).RefreshRate, DtWF(i).InitialStepId, DtWF(i).Work_ID}
        '            'Dim ParNames() As Object = {"pWStat_Id", "pName", "pHelp", "pDescription", "pEditDate", "pRefreshRate", "pStepId", "pWork_ID"}
        '            ' Dim parTypes() As Object = {17, 7, 7, 7, 7, 13, 13, 13}
        '            if Server.IsOracle then
        '                Server.Con.ExecuteNonQuery("ZWFUpd_pkg.ZUpdWfByWfID", parValues)
        '            Else
        '                Server.Con.ExecuteNonQuery("ZUpdWfByWfID", ParValues)
        '            End If
        '        Catch
        '        End Try
        '    Next
        'End If

        'DtWF = DsWf.WF.GetChanges(DataRowState.Deleted)
        'If IsNothing(DtWF) = False Then
        '    For i As Int32 = 0 To DtWF.Count - 1
        '        sql = "DELETE wfworkflow where work_id = " & DtWF(i).Work_ID
        '        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        '    Next
        'End If
        ''SP 29/12/2005
        'If IsNothing(DtWF) = False Then
        '    For i = 0 To DtWF.Count - 1
        '        'update de las reglas modificadas
        '        Try
        '            Dim ParValues() As Object = {DtWF(i).Work_ID}
        '            'Dim ParNames() As Object = {"pWork_ID"}
        '            ' Dim parTypes() As Object = {13}
        '            if Server.IsOracle then
        '                Server.Con.ExecuteNonQuery("ZWFDel_pkg.ZDelWfByWfId", parValues)
        '            Else
        '                'Server.Con.ExecuteNonQuery("ZDelWfByWfId", ParValues)
        '                Server.Con.ExecuteNonQuery("zsp_workflow_100_DeleteWorkFlowByWfId", ParValues)
        '            End If
        '        Catch
        '        End Try
        '    Next
        'End If
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo que modifica el nombre a un Workflow en base a su ID
    ''' </summary>
    ''' <param name="Name">Nuevo Nombre</param>
    ''' <param name="Work_Id">ID de WorkFlow</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    '''     [Gaston]    29/05/2008  Modified    Se comento el procedimiento almacenado en Oracle porque no existe (al menos en zambatst)
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub SaveNewName(ByVal Name As String, ByVal Work_Id As Int32)

        'Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE wfworkflow SET name = '" & Name & "' where work_id = " & Work_Id)
        'SP 29/12/2005
        Try

            ''Dim ParNames() As Object = {"pName", "pWork_Id"}
            '' Dim parTypes() As Object = {7, 13}

            If Server.isOracle Then
                'Server.Con.ExecuteNonQuery("ZWFUpd_pkg.ZUpdWfNameByWfId", parValues)
                Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE wfworkflow SET name = '" & Name & "' Where work_id = " & Work_Id)
            Else
                Dim ParValues() As Object = {Name, Work_Id}
                Server.Con.ExecuteNonQuery("Zsp_workflow_100_UpdWfNameByWfId", ParValues)
            End If

        Catch
        End Try

    End Sub

    Public Sub SaveWfInterval(ByVal Wfid As Int32, ByVal interval As Int32)
        'Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE wfworkflow SET  refreshrate = " & interval & " where work_id = " & Wfid)
        'SP 29/12/2005
        Try
            Dim ParValues() As Object = {interval, Wfid}
            'Dim ParNames() As Object = {"pInterval", "pWfid"}
            ' Dim parTypes() As Object = {13, 13}
            If Server.isOracle Then
                'Server.Con.ExecuteNonQuery("ZWFUpd_pkg.ZupdWfRefreshRateByWfId", parValues)
                Dim query As New System.Text.StringBuilder
                query.Append("UPDATE wfworkflow SET  refreshrate = ")
                query.Append(interval)
                query.Append("where work_id = ")
                query.Append(Wfid)
                Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
            Else
                Server.Con.ExecuteNonQuery("Zsp_workflow_100_UpdWfRefreshRateByWfId", ParValues)
            End If
        Catch
        End Try
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
        'Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE wfworkflow SET  initialstepId = " & InitialStepId & " where work_id = " & Wfid)
        'SP 29/12/2005
        Try
            Dim ParValues() As Object = {InitialStepId, Wfid}
            'Dim ParNames() As Object = {"pIStepId", "pWfid"}
            ' Dim parTypes() As Object = {13, 13}
            If Server.isOracle Then
                'Server.Con.ExecuteNonQuery("ZWFUpd_pkg.ZUpdWfInitialStepByWfId", parValues)
                Dim query As New System.Text.StringBuilder
                query.Append("UPDATE wfworkflow SET  initialstepId = ")
                query.Append(InitialStepId)
                query.Append(" where work_id = ")
                query.Append(Wfid)
                Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
            Else
                Server.Con.ExecuteNonQuery("Zsp_workflow_100_UpdWfInitialStepByWfId", ParValues)
            End If
        Catch
        End Try
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para eliminar un Workflow
    ''' </summary>
    ''' <param name="wf_id">Id del workflow que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    '''     [Gaston]    29/05/2008  Modified    Se comento el procedimiento almacenado en Oracle porque no existe (al menos en zambatst)
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub RemoveWorkFlow(ByVal wf_id As Int32)

        'SP 29/12/2005
        Try

            ''Dim ParNames() As Object = {"pWfid"}
            '' Dim parTypes() As Object = {13}

            If Server.isOracle Then
                ' Server.Con.ExecuteNonQuery("ZWFDel_pkg.ZDelWfByWfId", parValues)
                Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE wfworkflow Where work_id = " & wf_id)
            Else
                'Server.Con.ExecuteNonQuery("ZDelWfByWfId", ParValues)
                Dim ParValues() As Object = {wf_id}
                Server.Con.ExecuteNonQuery("zsp_workflow_100_DeleteWorkFlowByWfId", ParValues)
            End If

        Catch
        End Try

    End Sub

#End Region

#Region "Users & Groups"
    'Por que no se usa lo ya existente?
    'todo wf: que solo pueda ser llamado desde Business, ya que valida si usa el existente o recarga
    Public Function GetAllUsers() As SortedList
        Dim AllUsers As New SortedList
        Dim Strselect As String = "Select distinct id,name from usrtable"
        Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)

        Dim UserNull As New User
        UserNull.ID = 0
        UserNull.Name = "[Ninguno]"
        AllUsers.Add(UserNull.ID, UserNull)

        For Each row As DataRow In dstemp.Tables(0).Rows
            Dim u As New User
            u.ID = CInt(row.Item("Id"))
            u.Name = CStr(row.Item("Name"))
            AllUsers.Add(u.ID, u)
        Next

        UserNull.Dispose()

        Return AllUsers
    End Function

    ''' <summary>
    ''' Obtiene todos los grupos de usuarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Deberia utilizarse lo ya existente</remarks>
    Public Function GetAllGroups() As SortedList
        Dim AllGroups As New SortedList
        Dim Strselect As String = "Select distinct id,name from USRGROUP"
        Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)

        For Each row As DataRow In dstemp.Tables(0).Rows
            Dim g As New UserGroup
            g.ID = CInt(row.Item("Id"))
            g.Name = CStr(row.Item("Name"))
            AllGroups.Add(g.ID, g)
        Next
        Return AllGroups
    End Function


    ''' <summary>
    ''' Obtiene permisos de usuario por stepId
    ''' </summary>
    ''' <param name="wfstepid"></param>
    ''' <history> 
    '''     (pablo)    06/01/2011  Created        
    ''' </history>
    ''' <remarks></remarks>
    Public Shared Function GeUsersOnlUsersByUserIdAndRightsType(ByVal stepid As Int64) As DataTable
        Dim Strselect As String
        If Server.isOracle Then
            Strselect = "Select usrtable.id as ID,(usrtable.Apellido || ' ' || usrtable.Nombres) as NAME from usrtable where state = 0 order by usrtable.name"
        Else
            Strselect = "Select usrtable.id as ID,(usrtable.Apellido + ' ' + usrtable.Nombres) as NAME from usrtable where state = 0 order by usrtable.name"
        End If

        Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Return dstemp.Tables(0)
    End Function

    Public Shared Function GetGroupsUserGroupsIdsByStepID(ByVal stepid As Int64) As DataTable
        Dim Strselect As String = "Select distinct (zvwg.groupid) as ID, replace(ug.Name,'Zamba_','') as NAME from zvw_usr_r_groupsandtheirinh zvwg
        inner join usrgroup ug on zvwg.groupId = ug.id order by Name" 'and objid = " & Zamba.ObjectTypes.WFSteps & ""
        Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Return dstemp.Tables(0)
    End Function

#End Region

#Region "WorkFlow"

    'Alejandro 09/08/07

    'Inserta un nuevo registro en ZWFI
    Public Sub ZWFIInsert(ByVal _WI As Int32, ByVal _DTID As Int32, ByVal _RuleID As Int64)

        Dim sqlBuilder As New System.Text.StringBuilder()

        Try
            sqlBuilder.Append("INSERT INTO ZWFI (WI, DTID, RuleID) VALUES ('")
            sqlBuilder.Append(_WI.ToString())
            sqlBuilder.Append("', '")
            sqlBuilder.Append(_DTID.ToString())
            sqlBuilder.Append("', '")
            sqlBuilder.Append(_RuleID.ToString())
            sqlBuilder.Append("')")

            'Oracle inserta el semiColon automáticamente, asi que lo ponemos en caso SQL
            If ServerType = DBTYPES.MSSQLServer7Up Or ServerType = DBTYPES.MSSQLServer Then
                sqlBuilder.Append(";")
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    'Inserta un nuevo registro en ZWFII
    Public Sub ZWFIIInsert(ByVal _WI As Int32, ByVal _IID As Int32, ByVal _IValue As String)
        Try
            Dim sqlBuilder As New System.Text.StringBuilder()

            sqlBuilder.Append("INSERT INTO ZWFII (WI, IID, IValue) VALUES ('")
            sqlBuilder.Append(_WI.ToString())
            sqlBuilder.Append("', '")
            sqlBuilder.Append(_IID.ToString())
            sqlBuilder.Append("', '")
            sqlBuilder.Append(_IValue.ToString())
            sqlBuilder.Append("')")

            'Oracle inserta el semiColon automáticamente, asi que lo ponemos en caso SQL
            If ServerType = DBTYPES.MSSQLServer7Up Or ServerType = DBTYPES.MSSQLServer Then
                sqlBuilder.Append(";")
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
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
        Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE wfworkflow SET description = '" & description & "', help = '" & help & "' Where work_id = " & WFID)
    End Sub

#End Region

#Region "Validate"
    Public Function ValidateDocIdInWF(ByVal DocId As Int64, ByVal wfid As Int64, Entityid As Int64) As Int32
        Dim Docscount As New Int32
        Docscount = Server.Con.ExecuteScalar(CommandType.Text, "SELECT COUNT(1) FROM WFDocument  " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & "  WHERE Doc_ID = " + DocId.ToString + " AND work_Id = " + wfid.ToString & " AND DOC_TYPE_ID = " & Entityid)
        Return Docscount
    End Function

    Public Function ValidateDocIdInWF(ByVal DocId As Int64, ByVal wfid As Int64, Entityid As Int64, ByVal t As Transaction) As Int32
        Dim Docscount As New Int32
        Docscount = t.Con.ExecuteScalar(t.Transaction, CommandType.Text, "SELECT COUNT(1) FROM WFDocument  " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & "  WHERE Doc_ID = " & DocId.ToString & " AND work_Id = " & wfid.ToString & " AND doc_type_id = " & Entityid)
        Return Docscount
    End Function

#End Region

    'todo Andres : volvi a agregar este metodo porque tenia una llamada desde wfBusiness (marcelo)
    Public Function GetWorkflowsAndTaskCount() As DataSet
        Dim query As String = "SELECT work_id, Wstat_id, Name, Description, Help, CreateDate, EditDate, Refreshrate, InitialStepId , (SELECT count(1) FROM WFDocument  " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & "  WHERE work_id = WFWorkflow.work_id ) as TaskCount FROM WFWorkflow  " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "")

        Return Server.Con.ExecuteDataset(query)

    End Function

#Region "Rules"
    ''' <summary>
    ''' Setea las preferencias para las reglas de wf en tabla ZRuleOpt
    ''' </summary>
    ''' <param name="ruleid"></param>
    ''' <param name="item"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetRulesPreferences(ByVal ruleid As Int64, ByVal RuleSectionid As Int32, ByVal Objid As Int32, ByVal Objvalue As Int32, Optional ByVal ObjOperator As String = "")
        Dim query As New StringBuilder
        query.Append("SELECT count(1) FROM ZRuleOptbase " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & " WHERE Ruleid = " & ruleid)
        query.Append(" and SectionId = " & RuleSectionid)
        query.Append(" and ObjectId = " & Objid)

        If Server.Con.ExecuteScalar(CommandType.Text, query.ToString) = 0 Then
            query.Remove(0, query.Length)
            query.Append("INSERT INTO ZRuleOpt(RuleId, SectionId, ObjectId, ObjExtraData, ObjValue)")
            query.Append("VALUES(")
            query.Append(ruleid)
            query.Append(",")
            query.Append(RuleSectionid)
            query.Append(",")
            query.Append(Objid)
            query.Append(",'")
            query.Append(ObjOperator)
            query.Append("',")
            query.Append(Objvalue)
            query.Append(")")
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        Else
            query.Remove(0, query.Length)
            query.Append("UPDATE ZRuleOpt SET RuleId=")
            query.Append(ruleid)
            query.Append(", SectionId=")
            query.Append(RuleSectionid)

            query.Append(", ObjectId=")
            query.Append(Objid)
            query.Append(", ObjExtraData='")
            query.Append(ObjOperator)
            query.Append("'")
            query.Append(", ObjValue=")
            query.Append(Objvalue)
            query.Append(" WHERE Ruleid =")
            query.Append(ruleid)
            query.Append(" And SectionId =")
            query.Append(RuleSectionid)
            query.Append(" And ObjectId =")
            query.Append(Objid)
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        End If

    End Function


    ''' <summary>
    ''' Copia un conjunto de preferencias a un id de regla determinado
    ''' </summary>
    ''' <param name="preferences">Datatable con el conjunto de preferencias a copiar</param>
    ''' <param name="copyRuleId">Id de la regla donde se copiaran las preferencias</param>
    ''' <remarks>Se utiliza al copiar y cortar reglas. Por eso es que se utiliza el insert y no un update</remarks>
    ''' <history>
    '''     [Tomas] 23/03/2011  Created
    ''' </history>
    Public Sub CopyRulesPreferences(ByVal preferences As DataTable, ByVal copyRuleId As Int64)
        Dim query As New StringBuilder()

        For Each r As DataRow In preferences.Rows
            query.Remove(0, query.Length)
            query.Append("INSERT INTO ZRuleOpt(RuleId,SectionId,ObjectId,ObjExtraData,ObjValue)")
            query.Append("VALUES(")
            query.Append(copyRuleId)
            query.Append(",")
            query.Append(r.Item("SectionId"))
            query.Append(",")
            query.Append(r.Item("ObjectId"))
            query.Append(",'")
            query.Append(r.Item("ObjExtraData"))
            query.Append("',")
            query.Append(r.Item("ObjValue"))
            query.Append(")")
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        Next

    End Sub

    ''' <summary>
    ''' Setea las preferencias para las reglas de wf en tabla ZRuleOpt, pero para la búsqueda no se realiza un ObjectId
    ''' (Ejemplo de utilización: Solapa Asignación)
    ''' </summary>
    ''' <param name="ruleid"></param>
    ''' <param name="RuleSectionid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Emiliano]	30/03/2009	Created   
    ''' </history> 
    Public Function DeleteRulesPreferencesSinObjectId(ByVal ruleid As Int64, ByVal RuleSectionid As Int32)
        Dim query As New System.Text.StringBuilder
        query.Append("delete ZRuleOpt WHERE Ruleid = " & ruleid)
        query.Append(" and SectionId = " & RuleSectionid)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)

    End Function

    ''' <summary>
    ''' Setea las preferencias para las reglas de wf en tabla ZRuleOpt, pero para la búsqueda no se realiza un ObjectId
    ''' (Ejemplo de utilización: Solapa Asignación)
    ''' </summary>
    ''' <param name="ruleid"></param>
    ''' <param name="RuleSectionid"></param>
    ''' <param name="Objid"></param>
    ''' <param name="Objvalue"></param>
    ''' <param name="ObjOperator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	04/07/2008	Created   
    ''' </history> 
    Public Function SetRulesPreferencesSinObjectId(ByVal ruleid As Int64, ByVal RuleSectionid As Int32, ByVal Objid As Int32, ByVal Objvalue As Int32, Optional ByVal ObjOperator As String = "")

        Dim query As New System.Text.StringBuilder
        query.Append("SELECT count(1) FROM ZRuleOptbase " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & " WHERE Ruleid = " & ruleid)
        query.Append(" and SectionId = " & RuleSectionid)

        If (Server.Con.ExecuteScalar(CommandType.Text, query.ToString) = 0) Then

            query.Remove(0, query.Length)
            query.Append("INSERT INTO ZRuleOpt(RuleId, SectionId, ObjectId, ObjExtraData, ObjValue)")
            query.Append("VALUES(")
            query.Append(ruleid)
            query.Append(",")
            query.Append(RuleSectionid)
            query.Append(",")
            query.Append(Objid)
            query.Append(",'")
            query.Append(ObjOperator)
            query.Append("',")
            query.Append(Objvalue)
            query.Append(")")
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)

        Else
            query.Remove(0, query.Length)
            query.Append("UPDATE ZRuleOpt SET RuleId=")
            query.Append(ruleid)
            query.Append(", SectionId=")
            query.Append(RuleSectionid)
            query.Append(", ObjectId=")
            query.Append(Objid)
            query.Append(", ObjExtraData='")
            query.Append(ObjOperator)
            query.Append("'")
            query.Append(", ObjValue=")
            query.Append(Objvalue)
            query.Append(" WHERE Ruleid =")
            query.Append(ruleid)
            query.Append(" And SectionId =")
            query.Append(RuleSectionid)
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)

        End If

    End Function

    ''' <summary>
    ''' Método que sirve para guardar un elemento en la tabla ZRuleOpt o actualizarlo 
    ''' (Uso en: Solapa Habilitación: Para guardar el tipo de radioButton que quedo seleccionado al presionar guardar (Estado, Usuario o Ambos)
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="ruleSectionId"></param>
    ''' <param name="objId"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	27/05/2008	Created
    ''' </history>
    Public Sub saveItemSelectedThatCanBe_StateOrUserOrGrupo(ByVal ruleId As Int64, ByVal ruleSectionId As RuleSectionOptions, ByVal objId As RulePreferences, ByVal objIdActual As RulePreferences)
        Dim query As New System.Text.StringBuilder

        If (objIdActual <> objId) Then

            If (objIdActual = 0) Then

                query.Append("INSERT INTO ZRuleOpt(RuleId, SectionId, ObjectId, ObjExtraData, ObjValue) ")
                query.Append("VALUES(" & ruleId & "," & ruleSectionId & "," & objId & ",'0',0)")

            Else

                query.Append("UPDATE ZRuleOpt SET ObjectId = " & objId)
                query.Append(" Where RuleId = " & ruleId & " AND ObjectId = " & objIdActual)

            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())

        End If

    End Sub

    ''' <summary>
    ''' Método utilizado para saber que checkbox quedo seleccionado cuando el usuario presiono guardar, así, cuando vuelve a la solapa habilitación
    ''' (Administrador -> Workflow -> Selección de una regla) recupera el checkbox que selecciono
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	Aprox. 25/05/2008	Created
    ''' </history>
    'Public Function recoverItemSelectedThatCanBe_StateOrUserOrGroup(ByVal ruleId As Int64) As RulePreferences

    '    Dim query As New System.Text.StringBuilder
    '    query.Append("SELECT ObjectId FROM ZRuleOpt ")
    '    query.Append("Where RuleId = " & ruleId & " AND SectionId = " & RuleSectionOptions.Habilitacion & " AND ")
    '    query.Append("(ObjectId = " & RulePreferences.HabilitationSelectionState & " OR ObjectId = " & RulePreferences.HabilitationSelectionUser & _
    '                 " OR ObjectId = " & RulePreferences.HabilitationSelectionBoth & ")")

    '    Return (Con.ExecuteScalar(CommandType.Text, query.ToString()))

    'End Function

    ''' <summary>
    ''' Método que sirve para recuperar el radioButton seleccionado asi como el elemento seleccionado 
    ''' Ejemplo: usuario, grupo, etc. Ejemplo de utilización: Solapa Asignación
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="sectionId"></param>
    ''' <param name="item1"></param>
    ''' <param name="item2"></param>
    ''' <param name="item3"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	04/07/2008	Created
    ''' </history>
    'Public Function recoverItemsSelected(ByVal ruleId As Int64, ByVal sectionId As Integer, ByVal item1 As RulePreferences, ByVal item2 As RulePreferences, ByVal item3 As RulePreferences) As DataSet

    '    Dim query As New System.Text.StringBuilder
    '    query.Append("SELECT ObjectId, ObjExtraData, ObjValue FROM ZRuleOpt ")
    '    query.Append("Where RuleId = " & ruleId & " AND SectionId = " & sectionId & " AND ")
    '    query.Append("(ObjectId = " & item1 & " OR ObjectId = " & item2 & _
    '                 " OR ObjectId = " & item3 & ")")
    '    Return (Server.Con.ExecuteDataset(CommandType.Text, query.ToString()))

    'End Function

    ''' <summary>
    ''' Método utilizado para recuperar los estados y usuarios deshabilitados o bien, los estados y grupos deshabilitados (Solapa Habilitación)
    ''' </summary>
    ''' <param name="ruleId:        Número de regla"></param>
    ''' <param name="ruleSectionId: Número de solapa"></param>
    ''' <param name="objId:         Tipo de elemento (Usuario o Grupo)"></param>
    ''' <param name="stateId:       id de estado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	28/05/2008	Created
    ''' </history>
    Public Function recoverUsers_Or_Groups_belongingToAState(ByVal ruleId As Int64, ByVal ruleSectionId As RuleSectionOptions, ByVal objId As RulePreferences, ByVal stateId As String) As DataSet

        Dim query As New System.Text.StringBuilder

        query.Append("SELECT ObjValue FROM ZRuleOptbase " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", ""))
        query.Append(" Where RuleId = " & ruleId & " AND SectionId = " & ruleSectionId & " AND ObjectId = " & objId & " AND ObjExtraData = '" & stateId & "'")

        Return (Server.Con.ExecuteDataset(CommandType.Text, query.ToString()))

    End Function

    ''' <summary>
    ''' Método que llama a un método que sirve para guardar los elementos deshabilitados (estados, usuarios o grupos) (Solapa Habilitación)
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="ruleSectionId"></param>
    ''' <param name="objId"></param>
    ''' <param name="idCollectionDisabled"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	27/05/2008	Created
    ''' </history>
    Public Sub setRulesPreferencesForStatesUsersOrGroups(ByVal ruleId As Int64, ByVal ruleSectionId As RuleSectionOptions, ByVal objId As RulePreferences, ByRef idCollectionDisabled As Generic.List(Of Integer))
        SetRulesPreferences(ruleId, ruleSectionId, objId, -1, idCollectionDisabled)
    End Sub

    ''' <summary>
    ''' Método que llama a un método que sirve para guardar los estados y usuarios deshabilitados o bien, los estados y grupos deshabilitados (Solapa Habilitación)
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
    Public Sub setRulesPreferencesFor_StatesAndUsers_Or_StatesAndGroups(ByVal ruleId As Int64, ByVal ruleSectionId As RuleSectionOptions, ByVal objId As RulePreferences, ByVal stateId As Integer, ByRef idCollectionDisabled As Generic.List(Of Integer))
        SetRulesPreferences(ruleId, ruleSectionId, objId, stateId, idCollectionDisabled)
    End Sub

    ''' <summary>
    ''' Devuelve los items deshabilitados para la regla cuando esta seleccionado el conjunto de habilitacion
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>Marcelo Created 24/05/2011</history>
    Public Function recoverDisableItemsBoth(ByVal ruleId As Int64) As DataSet
        Dim query As New System.Text.StringBuilder

        query.Append("SELECT ObjExtraData, ObjValue, ObjectId FROM ZRuleOptbase " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", ""))
        query.Append(" Where RuleId = " & ruleId & " AND SectionId = 3 AND ObjectId in (37,38,62)")

        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString())
    End Function

    ''' <summary>
    ''' Se insertan los elementos deshabilitados (estados, usuarios o grupos) o bien, se insertan los estados y usuarios, o estados y grupos
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <param name="RuleSectionId"></param>
    ''' <param name="ObjId"></param>
    ''' <param name="idState"></param>
    ''' <param name="idCollectionDisabled"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	27/05/2008	Created
    ''' </history>
    Private Shared Sub setRulesPreferences(ByRef ruleId As Int64, ByRef RuleSectionId As RuleSectionOptions, ByRef ObjId As RulePreferences, ByRef idState As Integer, ByRef idCollectionDisabled As Generic.List(Of Integer))

        Dim query As System.Text.StringBuilder

        For Each idItemDisable As Integer In idCollectionDisabled

            query = New System.Text.StringBuilder
            ' Número de Regla, Identificación de solapa Habilitación, tipo de elemento que se agrega (Ej: tipo Estado), 0, id de elemento
            query.Append("INSERT INTO ZRuleOpt(RuleId, SectionId, ObjectId, ObjExtraData, ObjValue)")

            If (idState = -1) Then
                ' Se guarda el número de regla, sección de regla, tipo de elemento, 0 y id de estado
                query.Append("VALUES(" & ruleId & "," & RuleSectionId & "," & ObjId & ",'0'," & idItemDisable & ")")
            Else
                ' Se guarda el número de regla, sección de regla, tipo de elemento, id de estado y id de elemento (usuario o grupo)
                query.Append("VALUES(" & ruleId & "," & RuleSectionId & "," & ObjId & ", '" & idState.ToString() & "'," & idItemDisable & ")")
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())

        Next

    End Sub

    ''' <summary>
    ''' Se remueven los antiguos elementos deshabilitados de la tabla ZRuleOpt (estados, usuarios o grupos) (estados y usuarios, estados y grupos)
    ''' Uso en: Solapa Habilitación. Si se guardan determinados elementos deshabilitados, pero después esos elementos se pasan a habilitados, 
    ''' entonces se deben borrar de la tabla ZRuleOpt los elementos ex - deshabilitados
    ''' Nota: Se puede utilizar como función general
    ''' </summary>
    ''' <param name="ruleid"></param>
    ''' <param name="RuleSectionid"></param>
    ''' <param name="Objid"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	27/05/2008	Created
    ''' </history>
    Public Sub removeOldItemsThatWereDisabled(ByVal ruleid As Int64, ByVal RuleSectionid As RuleSectionOptions, ByVal Objid As RulePreferences)

        ' Se borran todos los elementos (estados, usuarios o grupos) deshabilitados que están guardados en la tabla según el ObjectId
        Dim query As New System.Text.StringBuilder

        query.Append("DELETE FROM ZRuleOptbase " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & " where RuleId = " & ruleid)
        query.Append(" and SectionID = " & RuleSectionid)
        query.Append(" and ObjectId = " & Objid)

        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())

    End Sub

    ''' <summary>
    ''' Obtiene el valor de las preferencias para las reglas de wf en tabla ZRuleOpt 
    ''' </summary>
    ''' <param name="ruleid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] 22/06/2009  Modified    Se adapta el método para trabajar con stored procedures 
    ''' </history>
    Public Function GetRulesPreferences(ByVal ruleid As Int64) As DataSet
        Dim Ds As DataSet = Nothing
        If Server.isOracle Then
            Dim parNames() As String = {"rule_id", "io_cursor"}
            ' Dim parTypes() As Object = {OracleType.Number, OracleType.Cursor}
            Dim parValues() As Object = {ruleid, 2}
            Ds = Server.Con.ExecuteDataset("zsp_workflow_400.GetRulesPreferences", parValues)
        Else
            Dim parameters() As Object = {(ruleid)}
            Ds = Server.Con.ExecuteDataset("zsp_workflow_300_GetRulesPreferences", parameters)
        End If
        Return Ds
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

        Dim query As New System.Text.StringBuilder

        ' Para obtener el valor que contiene si el estado está habilitado o deshabilitado se necesita el id de la regla, sección del id 
        ' (solapa Habilitación - es para mayor seguridad) y id del estado
        query.Append("Select ObjExtraData FROM ZRuleOptbase " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & " Where ruleId = ")
        query.Append(ruleId)
        query.Append(" And SectionId = ")
        query.Append(3)
        query.Append(" And ObjValue = ")
        query.Append(stateId)

        Dim value As String = Server.Con.ExecuteScalar(CommandType.Text, query.ToString())
        If IsNothing(value) = False Then
            If (value = "1") Then
                Return (True)
            Else
                Return (False)
            End If
        Else
            Return True
        End If

    End Function


#End Region

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que obtiene los WorkFlows para los cuales el usuario actual tiene permisos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Javier]	13/10/2010	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetStepsByUserRestrictedDoctypes(ByVal userId As Int64) As DataSet
        Dim Ds As DataSet = Nothing
        If Server.isOracle Then
            Dim query As New System.Text.StringBuilder

            query.Append("Select DISTINCT ws.step_Id FROM doc_restrictions r ")
            query.Append(" INNER JOIN doc_restriction_r_user d On r.restriction_id = d.restriction_id ")
            query.Append(" INNER JOIN Zwfviewwfdoctypes wf On wf.DOC_TYPE_ID=r.DOC_TYPE_ID ")
            query.Append(" INNER JOIN WFStep ws On wf.wfid = ws.work_id ")
            query.Append(" WHERE (d.user_id = " & userId & " Or d.user_id In ")
            query.Append("(Select groupid FROM (Select usrid, groupid from usr_r_group Union ALL Select usrid, InheritedUserGroup from usr_r_group inner join group_r_group On UserGroup=groupid) q WHERE usrid = " & userId & "))")
            Ds = Server.Con.ExecuteDataset(CommandType.Text, query.ToString())

        Else
            Dim parameters() As Object = {(userId)}
            Ds = Server.Con.ExecuteDataset("zsp_workflow_100_GetStepsByUserRestrictedDoctypes", parameters)

        End If
        Return Ds
    End Function

    ''' <summary>
    ''' Trae todas las etapas de un workflow de las que el usuario
    ''' tiene permiso de ver junto con todas las reglas de esa etapa
    ''' con parentid = 0 que sean acciones de usuario y reglas generales
    ''' y si el usuario la tiene habilitada o no.
    ''' </summary>
    ''' <param name="groupId"></param>
    ''' <param name="workflowId"></param>
    ''' <returns>COLUMNAS: Etapa, Regla, Id, Habilitada</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     Tomas   11/02/2011  Created
    ''' </history>
    Public Function GetUserHabilitatedRules(ByVal groupId As Int64, ByVal workflowId As Int64) As DataTable
        Dim dt As DataTable
        dt = Server.Con.ExecuteDataset(CommandType.Text, String.Format("(Select S.Name As Etapa, R.Name As Regla, R.Id, 'NO' AS Habilitada FROM WFStep S INNER JOIN wfrules R ON R.step_id = S.step_id INNER JOIN zruleopt O on O.ruleid = R.id WHERE S.step_Id IN (SELECT aditional FROM USR_RIGHTS WHERE objid = 42 and rtype = 19 and groupid = {0}) AND S.work_id = @wid AND (O.SectionId = 3 AND O.ObjectId = 36 AND O.ObjValue = {0}) AND R.ParentID = 0 AND (R.ParentType = 5 OR R.ParentType = 14) GROUP BY s.Name, r.Name, R.id) UNION ALL (SELECT S.Name AS Etapa, R.Name AS Regla, R.id, 'SI' AS Habilitada FROM WFStep S INNER JOIN wfrules R ON R.step_id = S.step_id LEFT JOIN zruleopt O on o.ruleid = R.id WHERE S.step_Id IN (SELECT aditional FROM USR_RIGHTS WHERE objid = 42 and rtype = 19 and groupid = {0}) AND S.work_id = {1} AND R.ParentID = 0 AND (R.ParentType = 5 OR R.ParentType = 14) AND R.id NOT IN  (SELECT DISTINCT(R.id) FROM WFStep S INNER JOIN wfrules R ON R.step_id = S.step_id INNER JOIN zruleopt O on o.ruleid = R.id WHERE S.step_Id IN (SELECT aditional FROM USR_RIGHTS WHERE objid = 42 and rtype = 19 and groupid = {0})  AND S.work_id = {1} AND (O.SectionId = 3 AND O.ObjectId = 36 AND O.ObjValue = {0}) AND R.ParentID = 0 AND (R.ParentType = 5 OR R.ParentType = 14)) GROUP BY s.Name, r.Name, R.id) ORDER BY Etapa, Habilitada, Regla ", groupId, workflowId)).Tables(0)
        Return dt
    End Function

    Function CanExecuteRules(ByVal ruleId As Long, ByVal userid As Long) As Boolean
        Dim AssigmentCount As Int64 = Int64.Parse(Server.Con.ExecuteScalar(CommandType.Text, String.Format("SELECT count(1) FROM WFStep s inner join wfrules r on s.step_id=r.step_id WHERE s.step_id IN (SELECT aditional FROM USR_RIGHTS WHERE objid=42 and rtype=19 and (groupid = {0}       OR groupid IN (SELECT groupid FROM zvw_usr_r_groupsandtheirinh WHERE usrid = {0}       ))) and r.id = {1}", userid, ruleId)).ToString())
        If AssigmentCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    'Public Function GetRulesPreferencesByStepId(ByVal StepId As Int64) As DataTable
    '    Dim Ds As DataSet = Nothing
    '    Ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT distinct o.ruleid,ObjValue,ObjExtraData,ObjectId,SectionId,r.step_id FROM ZRuleOptbase o inner join wfrules r on r.id = o.ruleid and  r.step_id = " & StepId)
    '    If IsNothing(Ds) = False AndAlso Ds.Tables.Count > 0 Then
    '        Return Ds.Tables(0)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

End Class
