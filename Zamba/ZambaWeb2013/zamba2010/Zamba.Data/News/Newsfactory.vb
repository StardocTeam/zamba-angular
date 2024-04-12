Imports System.Collections.Generic
Imports System.Text
Imports Zamba.Core

Public Class Newsfactory
    ''' <summary>
    ''' Guarda la notificacion
    ''' </summary>
    ''' <param name="NewsID"></param>
    ''' <param name="DocID"></param>
    ''' <param name="DocTypeID"></param>
    ''' <param name="comment"></param>
    ''' <remarks></remarks>
    Public Sub SaveNews(ByVal NewsID As Int64, ByVal DocID As Int64, ByVal DocTypeID As Int64, ByVal comment As String, Userid As Int64, details As String)
        Dim Query As New StringBuilder
        Query.Append("Insert into ZNews values(")
        Query.Append(NewsID)
        Query.Append(",")
        Query.Append(DocID)
        Query.Append(",")
        Query.Append(DocTypeID)
        Query.Append(",'")
        Query.Append(comment.Trim())
        Query.Append("',")
        If Server.isOracle Then
            Query.Append("sysdate")
        Else
            Query.Append("getdate()")
        End If
        'Query.Append(",")
        'Query.Append(Userid)
        'Query.Append(",'")
        'Query.Append(details.Trim())
        'Query.Append("'")
        Query.Append(")")
        Server.Con.ExecuteNonQuery(CommandType.Text, Query.ToString())
    End Sub


    ''' <summary>
    ''' Set news as read
    ''' </summary>
    ''' <param name="Users">Users ID separate by ,</param>
    ''' <param name="newsID">News ID</param>
    ''' <remarks></remarks>
    Public Sub SetRead(ByVal Users As String, ByVal NewsID As Int64)
        Dim Query As New StringBuilder
        If Server.isOracle Then
            For Each UserID As String In Users.Split(",")
                Dim PUserID = UserID, PNewsID = PNewsID, PStatus = 1
                Dim parValues() As Object = {PNewsID, PUserID, PStatus}
                Server.Con.ExecuteNonQuery("zsp_News_100_InsertRead.UpdateNewsUsers", parValues)
            Next
        Else
            For Each UserID As String In Users.Split(",")
                Query.AppendLine("exec zsp_News_100_UpdateNewsUsers " + NewsID.ToString() + "," + UserID + ",1")
            Next
            Server.Con.ExecuteNonQuery(CommandType.Text, Query.ToString())
        End If

    End Sub

    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''        [German] 04/04/11 - Created.
    ''' </history>
    Public Function GetAllNewsSummary(userid As Long, Optional searchType As NewsSearchType = NewsSearchType.UNREAD) As DataSet

        Dim strselect As New StringBuilder()

        Dim valueCol As String = If(Server.isOracle, "c_value", "value")

        strselect.Append($"Select znu.NewsId, zn.docid, zn.doctypeid, zn.{valueCol}, Zn.crdate, znu.status as isRead from ZNewsUsers as znu ")
        strselect.Append("inner join ZNews as Zn on znu.newsid = zn.newsid ")
        strselect.Append($"where znu.userid = {userid} ")

        If searchType = NewsSearchType.UNREAD Then
            strselect.Append($"and znu.status = 0 ")
        ElseIf searchType = NewsSearchType.READ Then
            strselect.Append($"and znu.status = 1 ")
        End If

        strselect.Append("order by zn.crdate desc")

        Return Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString())

    End Function

    Public Function GetNews(userId As Long, resultId As Long, AsociatedEntityIds As String) As DataSet
        Dim strselect As New StringBuilder()

        If Server.isOracle Then
            strselect.Append("select zn.NewsId, zn.docid,zn.doctypeid,zn.c_value accion,Zn.crdate Fecha, zn.userid,zn.details detalle, (u.Apellido || ' ' || u.Nombres) Usuario from ZNews  zn ")
        Else
            strselect.Append("select zn.NewsId, zn.docid,zn.doctypeid,zn.value,Zn.crdate, zn.userid,zn.details, (u.Apellido + ' ' + u.Nombres) Usuario from ZNews  zn ")
        End If
        '        strselect.Append("left join ZNewsUsers  Znu ")
        '       strselect.Append("on znu.newsid = zn.newsid ")
        strselect.Append("left join usrtable  u ")
        strselect.Append("on u.id = zn.userid ")
        strselect.Append(" where  zn.docid = ")
        strselect.Append(resultId)

        If (AsociatedEntityIds.Trim <> String.Empty) Then
            strselect.Append(" and zn.doctypeid in (")
            strselect.Append(AsociatedEntityIds.Trim)
            strselect.Append(")")
        End If

        strselect.Append("  order by zn.crdate desc")

        Return Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString())
    End Function


#Region "WebParts"

    Public Function GetTasksAverageTimeInSteps(ByVal workflowid As Int64) As Hashtable
        Dim query As New StringBuilder()
        query.Append("SELECT Doc_id,Stepid,UserName,Accion,Fecha")
        query.Append(" FROM WFStepHst")
        query.Append(" WHERE workflowid = ")
        query.Append(workflowid.ToString)
        query.Append(" AND ( accion = 'Ingreso' OR accion = 'Egreso')")
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

        Dim steps As SortedList = WFStepsFactory.GetStepsByWorkId(workflowid)

        Dim docsIds As New List(Of Int64)
        Dim DocsIngresos As New Generic.List(Of DataRow)
        Dim DocsEgresos As New Generic.List(Of DataRow)

        Dim TiempoIngEgr As New SortedList
        Dim averagetimeByStep As New Hashtable
        Dim StepId As Int64

        For Each wfs As WFStep In steps.Values
            StepId = wfs.ID
            TiempoIngEgr.Clear()
            docsIds.Clear()
            DocsIngresos.Clear()
            DocsEgresos.Clear()
            If Not IsNothing(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim rows As DataRow() = ds.Tables(0).Select("StepId = " & StepId.ToString)
                For Each r As DataRow In rows
                    If docsIds.Contains(Int64.Parse(r.Item(0).ToString)) = False Then
                        docsIds.Add(Int64.Parse(r.Item(0).ToString))
                    End If
                    Select Case r.Item("Accion").ToString
                        Case "Ingreso"
                            DocsIngresos.Add(r)
                        Case "Egreso"
                            DocsEgresos.Add(r)
                        Case Else

                    End Select
                Next
                Dim TiempoIngreso As New DateTime
                Dim TiempoEgreso As New DateTime
                Dim docidEgr As Int64

                For Each DocId As Int64 In docsIds
                    TiempoIngreso = Nothing
                    TiempoEgreso = Nothing
                    For Each r As DataRow In DocsIngresos
                        If DocId = Int64.Parse(r.Item(0).ToString) Then
                            TiempoIngreso = DateTime.Parse(r.Item(4).ToString)
                            For Each row As DataRow In DocsEgresos
                                docidEgr = Int64.Parse(row.Item(0).ToString)
                                If DocId = docidEgr Then
                                    TiempoEgreso = DateTime.Parse(row.Item(4).ToString)
                                    If Not IsNothing(TiempoIngreso) AndAlso Not IsNothing(TiempoEgreso) Then
                                        If TiempoIngEgr.ContainsKey(docidEgr) = False Then
                                            TiempoIngEgr.Add(Int64.Parse(row.Item(0).ToString), System.Convert.ToInt32(TiempoEgreso.Subtract(TiempoIngreso).TotalMinutes))
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Next
                Next
            End If
            Dim sum As Int64 = 0
            Dim cont As Int32 = 0
            For Each docid As Int64 In TiempoIngEgr.Keys
                cont += 1
                sum += TiempoIngEgr.Item(docid).ToString
            Next

            If sum > 0 Then
                averagetimeByStep.Add(StepId, sum / cont)
            End If
        Next
        Return averagetimeByStep
    End Function

    ''' <summary>
    ''' Método que sirve para obtener el tiempo promedio de una etapa
    ''' </summary>
    ''' <param name="name">Nombre de la consulta</param>
    ''' <param name="tables"></param>
    ''' <param name="fields"></param>
    ''' <param name="relations"></param>
    ''' <param name="conditions"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	26/11/2008	Modified    Se agrego a la consulta el inner join
    ''' </history>
    Public Function GetTasksAverageTimeByStep(ByVal stepid As Int64) As Hashtable

        Dim query As New StringBuilder()
        query.Append("SELECT WFStepHst.Doc_id,Stepid,UserName,Accion,Fecha")
        query.Append(" FROM WFStepHst")
        query.Append(" inner join wfdocument on WFStepHst.Doc_id = wfdocument.Doc_id")
        query.Append(" WHERE Stepid = ")
        query.Append(stepid.ToString)
        query.Append(" AND ( accion = 'Ingreso' OR accion = 'Egreso')")

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

        Dim docsIds As New List(Of Int64)
        Dim DocsIngresos As New Generic.List(Of DataRow)
        Dim DocsEgresos As New Generic.List(Of DataRow)

        Dim TiempoIngEgr As New SortedList
        Dim averagetimeByStep As New Hashtable

        If Not IsNothing(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
            For Each r As DataRow In ds.Tables(0).Rows
                If docsIds.Contains(Int64.Parse(r.Item(0).ToString)) = False Then
                    docsIds.Add(Int64.Parse(r.Item(0).ToString))
                End If
                Select Case r.Item("Accion").ToString
                    Case "Ingreso"
                        DocsIngresos.Add(r)
                    Case "Egreso"
                        DocsEgresos.Add(r)
                    Case Else

                End Select
            Next
            Dim TiempoIngreso As New DateTime
            Dim TiempoEgreso As New DateTime
            Dim docidEgr As Int64

            For Each DocId As Int64 In docsIds
                TiempoIngreso = Nothing
                TiempoEgreso = Nothing
                For Each r As DataRow In DocsIngresos
                    If DocId = Int64.Parse(r.Item(0).ToString) Then
                        TiempoIngreso = DateTime.Parse(r.Item(4).ToString)
                        For Each row As DataRow In DocsEgresos
                            docidEgr = Int64.Parse(row.Item(0).ToString)
                            If DocId = docidEgr Then
                                TiempoEgreso = DateTime.Parse(row.Item(4).ToString)
                                If Not IsNothing(TiempoIngreso) AndAlso Not IsNothing(TiempoEgreso) Then
                                    If TiempoIngEgr.ContainsKey(docidEgr) = False Then
                                        TiempoIngEgr.Add(Int64.Parse(row.Item(0).ToString), System.Convert.ToInt32(TiempoEgreso.Subtract(TiempoIngreso).TotalMinutes))
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    End If
                Next
            Next
        End If
        Dim sum As Int64
        Dim cont As Int32
        For Each docid As Int64 In TiempoIngEgr.Keys
            cont += 1
            sum += TiempoIngEgr.Item(docid).ToString
        Next

        If sum > 0 Then
            averagetimeByStep.Add(stepid, sum / cont)
        End If

        Return averagetimeByStep

    End Function

    Public Function GetTasksToExpireGroupByStep(ByVal workflowid As Int64, ByVal FromHours As Int32, ByVal ToHours As Int32) As DataSet
        Dim parvalues() As Object = {workflowid, FromHours, ToHours}
        Return Server.Con.ExecuteDataset("sp_GetTaskToExpireGroupByStep", parvalues)
    End Function
    Public Function GetExpiredTasksGroupByUser(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_GetExpiredTasksGroupByUser", parvalues)
    End Function

    Public Function GetExpiredTasksGroupByStep(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_GetExpiredTasksGroupByStep", parvalues)
    End Function

    Public Function GetTasksToExpireGroupByUser(ByVal workflowid As Int64, ByVal FromHours As Int32, ByVal ToHours As Int32) As DataSet
        Dim parvalues() As Object = {workflowid, FromHours, ToHours}
        Return Server.Con.ExecuteDataset("sp_GetTaskToExpireGroupByUser", parvalues)
    End Function

    Public Function GetTasksBalanceGroupByWorkflow(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_GetTasksBalanceByWorkflow", parvalues)
    End Function

    Public Function GetTasksBalanceGroupByStep(ByVal stepid As Int64) As DataSet
        Dim parvalues() As Object = {stepid}
        Return Server.Con.ExecuteDataset("sp_GetTasksBalanceByStep", parvalues)
    End Function

    Public Function GetMyTasks(userId As Int64) As DataSet
        If Server.isSQLServer Then
            Return Server.Con.ExecuteDataset(CommandType.Text, String.Format("select Task_id, Tarea, Etapa, Asignado,Ingreso, Vencimiento from (select  distinct  top(200) Task_Id, WF.Name Tarea, s.name Etapa, isnull(g.name,'') Asignado, wf.checkin Ingreso, wf.expiredate Vencimiento, wf.user_asigned  from wfdocument wf  with (nolock)  inner join wfstep s  with (nolock) on s.step_id = wf.step_id left join zuser_or_group g  with (nolock) on g.id = wf.user_asigned where user_asigned = {0} or user_asigned in (select groupid from usr_r_group r  with (nolock) where r.USRID = {0})   order by checkin  desc ) q ", userId))
        Else
            Return Server.Con.ExecuteDataset(CommandType.Text, String.Format("select Task_id, Tarea, Etapa, Asignado,CONVERT(varchar,Ingreso,103) Ingreso, CONVERT(varchar,Vencimiento,103) Vencimiento from (select  distinct  top(200) Task_Id, WF.Name Tarea, s.name Etapa, isnull(g.name,'') Asignado, wf.checkin Ingreso, wf.expiredate Vencimiento, wf.user_asigned  from wfdocument wf  inner join wfstep s on s.step_id = wf.step_id left join zuser_or_group g on g.id = wf.user_asigned where user_asigned = {0} or user_asigned in (select groupid from usr_r_group r where r.USRID = {0})   order by checkin  desc ) q", userId))
        End If
    End Function
    Public Function GetMyTasksCount(userId As Int64) As Int64
        If Server.isSQLServer Then
            Return Server.Con.ExecuteScalar(CommandType.Text, String.Format("select count(1)   from wfdocument wf  with (nolock)   where user_asigned = {0} or user_asigned in (select groupid from usr_r_group r  with (nolock) where r.USRID = {0})   ", userId))
        Else
            Return Server.Con.ExecuteScalar(CommandType.Text, String.Format("select count(1)   from wfdocument wf                  where user_asigned = {0} or user_asigned in (select groupid from usr_r_group r where r.USRID = {0})  ", userId))
        End If
    End Function

    Public Function GetRecentTasks(userId As Int64) As DataSet
        If Server.isSQLServer Then
            Dim query As String = String.Format("SELECT distinct s_object_id FROM USER_HST u WITH (NOLOCK) WHERE USER_ID = {0} AND ACTION_TYPE IN ( 1 ,71 ) AND OBJECT_TYPE_ID = 6 AND ISNUMERIC(S_OBJECT_ID) > 0", userId)
            Dim dtDoctype As DataTable = Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
            Dim ds As New DataSet
            Dim DtTareas As DataTable
            Dim PrimerItem As Boolean = True
            Dim entitiesIds As New StringBuilder()
            For Each item As DataRow In dtDoctype.Rows
                entitiesIds.Append("'")
                entitiesIds.Append(item.ItemArray(0))
                entitiesIds.Append("',")
            Next
            entitiesIds.Remove(entitiesIds.Length - 1, 1)


            Dim querySlect As String = String.Format("SELECT wf.Task_id ,q.doc_id ,q.EntityId DOC_TYPE_ID ,wf.name Tarea ,Fecha ,s.name Etapa ,isnull(us.name, '') Asignado ,wf.checkin Ingreso ,wf.expiredate Vencimiento FROM ( SELECT TOP (20) object_id doc_Id ,MAX(ACTION_DATE) Fecha ,s_object_id EntityId ,USER_ID FROM USER_HST u WITH (NOLOCK) WHERE USER_ID = {0} AND ACTION_TYPE IN ( 1 ,71 ) AND OBJECT_TYPE_ID = 6 AND S_OBJECT_ID NOT LIKE '%.%' AND ISNUMERIC(S_OBJECT_ID) > 0 and s_object_id in ({1}) GROUP BY object_id ,S_OBJECT_ID ,USER_ID ORDER BY FECHA DESC ) q INNER JOIN  usrtable us ON us.ID = q.USER_ID INNER JOIN wfdocument wf WITH (NOLOCK) ON wf.doc_id = q.doc_id INNER JOIN wfstep s WITH (NOLOCK) ON s.step_id = wf.step_id", userId, entitiesIds.ToString())
            Dim dtRows As DataTable = Server.Con.ExecuteDataset(CommandType.Text, querySlect).Tables(0)

                If PrimerItem Then
                    DtTareas = dtRows.Copy
                    PrimerItem = False
                Else
                    For Each Row As DataRow In dtRows.Rows
                        DtTareas.ImportRow(Row)
                    Next
                End If



            ds.Tables.Add(DtTareas)
            Return ds
            'Return Server.Con.ExecuteDataset(CommandType.Text, String.Format("select  wf.Task_id, q.doc_id, wf.DOC_TYPE_ID,wf.name Tarea,Fecha ,s.name Etapa, isnull(g.name,'') Asignado, wf.checkin Ingreso, wf.expiredate Vencimiento from (select  top(20) object_id doc_Id, MAX(ACTION_DATE) Fecha  from USER_HST u  with (nolock)  where USER_ID = {0} and ACTION_TYPE in (1,71) and OBJECT_TYPE_ID = 6  group by object_id order by FECHA desc ) q inner join wfdocument wf  with (nolock) on wf.doc_id = q.doc_id inner join wfstep s  with (nolock) on s.step_id = wf.step_id left join zuser_or_group g  with (nolock) on g.id = wf.user_asigned", userId))
        Else
            Return Server.Con.ExecuteDataset(CommandType.Text, String.Format("select  wf.Task_id, wf.doc_id, wf.DOC_TYPE_ID,wf.name Tarea,Fecha ,s.name Etapa, isnull(g.name,'') Asignado, wf.checkin Ingreso, wf.expiredate Vencimiento from (select   object_id doc_Id, MAX(ACTION_DATE) Fecha  from USER_HST u    where USER_ID = {0} and ACTION_TYPE in (1,71) and OBJECT_TYPE_ID = 6  group by object_id order by FECHA desc ) q inner join wfdocument wf   on wf.doc_id = q.doc_id inner join wfstep s   on s.step_id = wf.step_id left join zuser_or_group g   on g.id = wf.user_asigned where rownum < 20", userId))
        End If
    End Function

    Public Function GetRecentTasksCount(userId As Int64) As Int64
        If Server.isSQLServer Then
            Return Server.Con.ExecuteScalar(CommandType.Text, String.Format("select  count(1) from (select  top(20) object_id doc_Id, MAX(ACTION_DATE) Fecha  from USER_HST u  with (nolock)  where USER_ID = {0} and ACTION_TYPE in (1,71) and OBJECT_TYPE_ID = 6  group by object_id order by FECHA desc ) q left join wfdocument wf  with (nolock) on wf.doc_id = q.doc_id left join wfstep s  with (nolock) on s.step_id = wf.step_id left join zuser_or_group g  with (nolock) on g.id = wf.user_asigned", userId))
        Else
            Return Server.Con.ExecuteScalar(CommandType.Text, String.Format("select  count(1) from (select   object_id doc_Id, MAX(ACTION_DATE) Fecha  from USER_HST u    where USER_ID = {0} and ACTION_TYPE in (1,71) and OBJECT_TYPE_ID = 6  group by object_id order by FECHA desc ) q left join wfdocument wf   on wf.doc_id = q.doc_id left join wfstep s   on s.step_id = wf.step_id left join zuser_or_group g   on g.id = wf.user_asigned where rownum < 20", userId))
        End If
    End Function

    Public Function GetAsignedTasksCountsGroupByUser(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_AsignedTasksCountsGroupByUser", parvalues)
    End Function

    Public Function GetAsignedTasksCountsGroupByStep(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_AsignedTasksCountsGroupByStep", parvalues)
    End Function

    Public Function GetTaskConsumedMinutesByWorkflowGroupByUsers(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_Sarasa", parvalues)
    End Function

    Public Function GetTaskConsumedMinutesByStepGroupByUsers(ByVal stepid As Int64) As DataSet
        Dim parvalues() As Object = {stepid}
        Return Server.Con.ExecuteDataset("sp_Sarasa", parvalues)
    End Function

#End Region
End Class
