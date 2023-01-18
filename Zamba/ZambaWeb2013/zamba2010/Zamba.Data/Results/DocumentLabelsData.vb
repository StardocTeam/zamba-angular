Public Class DocumentLabelsData

    Public Sub UpdateImportantLabels(ByVal docTypeId As Int64, ByVal docId As Int64, ByVal important As Boolean, ByVal userId As Int64)
        Dim labelValue As Int32
        If important Then
            labelValue = 1
        Else
            labelValue = 0
        End If
        If Server.isOracle Then
            '            Dim query As String = String.Format("BEGIN UPDATE DocumentLabels SET favorite = {3}, udate = SYSDATE WHERE  userId = {0} And doctypeid = {1} And docid = {2} END IF ( sql%rowcount = 0 ) THEN BEGIN INSERT INTO DocumentLabels (CRDATE, UDATE, USERID, DOCTYPEID, DOCID, IMPORTANCE, FAVORITE) VALUES (SYSDATE, SYSDATE, {0}, {1}, {2}, {3},0) END END IF", 0, docTypeId, docId, labelValue)
            Dim query As String = String.Format("SELECT count(1) FROM DocumentLabels WHERE doctypeid = {0} And	docid = {1} and userid = {3}", docTypeId, docId, labelValue, userId)
            Dim count As Int64 = Server.Con.ExecuteScalar(CommandType.Text, query)
            If IsDBNull(count) = False AndAlso count > 0 Then
                Dim Uquery As String = String.Format("UPDATE DocumentLabels SET importance = '{2}', udate = SYSDATE WHERE doctypeid = {0} And docid = {1} and userid = {3} ", docTypeId, docId, labelValue, userId)
                Server.Con.ExecuteNonQuery(CommandType.Text, Uquery)
            Else
                Dim Iquery As String = String.Format("INSERT INTO DocumentLabels (CRDATE, UDATE, USERID, DOCTYPEID, DOCID, IMPORTANCE, FAVORITE) VALUES (SYSDATE, SYSDATE,{3}, {0}, {1}, {2},0)", docTypeId, docId, labelValue, userId)
                Server.Con.ExecuteNonQuery(CommandType.Text, Iquery)
            End If
        Else
            Dim query As String = String.Format("If EXISTS(SELECT * FROM DocumentLabels WHERE doctypeid = {0} And docid = {1} and userid = {3}) UPDATE DocumentLabels SET IMPORTANCE = '{2}', udate = getdate() WHERE doctypeid = {0} And docid = {1} and userid = {3} Else INSERT INTO DocumentLabels (CRDATE, UDATE, USERID, DOCTYPEID, DOCID, IMPORTANCE, FAVORITE) VALUES (GETDATE(), getdate(), {3}, {0}, {1}, {2},0)", docTypeId, docId, labelValue, userId)
            Server.Con.ExecuteNonQuery(CommandType.Text, query)
        End If

    End Sub

    Public Sub UpdateFavoriteLabels(ByVal docTypeId As Int64, ByVal docId As Int64, ByVal favorite As Boolean, ByVal userId As Int64)
        Dim labelValue As Int32
        If favorite Then
            labelValue = 1
        Else
            labelValue = 0
        End If
        If Server.isOracle Then
            '             Dim query As String = String.Format("BEGIN UPDATE DocumentLabels SET favorite = {3}, udate = SYSDATE WHERE userId = {0} And doctypeid = {1} And docid = {2} END IF ( sql%rowcount = 0 ) THEN BEGIN INSERT INTO DocumentLabels (CRDATE, UDATE, USERID, DOCTYPEID, DOCID, IMPORTANCE, FAVORITE) VALUES (SYSDATE, SYSDATE, {0}, {1}, {2}, 0, {3}) END END IF", userId, docTypeId, docId, labelValue)
            Dim query As String = String.Format("SELECT count(1) FROM DocumentLabels WHERE doctypeid = {0} And	docid = {1} and userid = {3}", docTypeId, docId, labelValue, userId)
            Dim count As Int64 = Server.Con.ExecuteScalar(CommandType.Text, query)
            If IsDBNull(count) = False AndAlso count > 0 Then
                Dim Uquery As String = String.Format("UPDATE DocumentLabels SET favorite = '{2}', udate = SYSDATE WHERE doctypeid = {0} And docid = {1} and userid = {3} ", docTypeId, docId, labelValue, userId)
                Server.Con.ExecuteNonQuery(CommandType.Text, Uquery)
            Else
                Dim Iquery As String = String.Format("INSERT INTO DocumentLabels (CRDATE, UDATE, USERID, DOCTYPEID, DOCID, IMPORTANCE, FAVORITE) VALUES (SYSDATE, SYSDATE,{3}, {0}, {1}, 0, {2})", docTypeId, docId, labelValue, userId)
                Server.Con.ExecuteNonQuery(CommandType.Text, Iquery)
            End If
        Else
            Dim query As String = String.Format("If EXISTS(SELECT * FROM DocumentLabels WHERE doctypeid = {0} And docid = {1} and userid = {3}) UPDATE DocumentLabels SET favorite = '{2}', udate = getdate() WHERE doctypeid = {0} And docid = {1} and userid = {3} Else INSERT INTO DocumentLabels (CRDATE, UDATE, USERID, DOCTYPEID, DOCID, IMPORTANCE, FAVORITE) VALUES (GETDATE(), getdate(),{3}, {0}, {1}, 0, {2})", docTypeId, docId, labelValue, userId)
            Server.Con.ExecuteNonQuery(CommandType.Text, query)
        End If
    End Sub

    Public Function GetDocumentLabelsByUser(ByVal userId As Int64, ByVal docId As Int64) As DataSet
        Dim query As String = String.Format("  SELECT 'IMPORTANCE' as LABEL, COUNT(1) as FLAG FROM DocumentLabels WHERE userid = 0 AND docid = {1} AND importance = 1 UNION ALL SELECT 'FAVORITE' as LABEL, COUNT(1) as FLAG FROM DocumentLabels WHERE userid = {0} AND docid = {1} AND favorite = 1", userId, docId)
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query)
        Return ds
    End Function

    Public Function GetBookmarks(ByVal userId As Int64) As DataSet
        Dim query As String = String.Format("SELECT distinct DoctypeId FROM DocumentLabels WHERE userid = {0}", userId)
        Dim dtDoctype As DataTable = Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
        Dim ds As New DataSet
        Dim DtTareas As New DataTable
        Dim PrimerItem As Boolean = True
        Dim RF As New RightFactory
        For Each item As DataRow In dtDoctype.Rows
            If RF.GetUserRights(userId, ObjectTypes.DocTypes, Core.RightsType.View, Int64.Parse(item.ItemArray(0))) Then
                Dim querySlect As String = String.Format("Select TOP(100) T.name Tarea ,CONVERT(VARCHAR, udate, 103) Fecha ,s.name Etapa ,isnull(g.name, '') Asignado ,CONVERT(VARCHAR, w.checkin, 103) Ingreso ,CONVERT(VARCHAR, w.expiredate, 103) Vencimiento ,docid ,doctypeid ,task_id FROM DocumentLabels l INNER JOIN DOC_T{1}  T ON T.Doc_ID = l.docid left JOIN wfdocument w ON w.Doc_ID = l.docid AND w.DOC_TYPE_ID = l.doctypeid left JOIN wfstep s ON s.step_id = w.step_id LEFT JOIN zuser_or_group g ON g.id = w.user_asigned WHERE userid = {0} AND favorite = 1 ORDER BY udate DESC", userId, item.ItemArray(0))
                Dim dtRows As DataTable = Server.Con.ExecuteDataset(CommandType.Text, querySlect).Tables(0)

                If PrimerItem Then
                    DtTareas = dtRows.Copy
                    PrimerItem = False
                Else
                    For Each Row As DataRow In dtRows.Rows
                        DtTareas.ImportRow(Row)
                    Next
                End If
            End If

        Next

        If DtTareas IsNot Nothing Then
            ds.Tables.Add(DtTareas)
        End If

        Return ds
    End Function

    Public Function GetBookmarksCount(ByVal userId As Int64) As Int64
        Dim query As String = String.Format("SELECT count(1) FROM DocumentLabels l inner join wfdocument w on w.Doc_ID = l.docid and w.DOC_TYPE_ID = l.doctypeid inner join wfstep s on s.step_id = w.step_id left join zuser_or_group g on g.id = w.user_asigned  WHERE userid = {0} AND  favorite = 1  ", userId)
        Return Server.Con.ExecuteScalar(CommandType.Text, query)

    End Function

    Public Function GetStars(ByVal userId As Int64) As DataSet
        ' user id es cero para que traiga todos los importantes de todos los usuarios
        Dim query As String = String.Format("SELECT distinct DoctypeId FROM DocumentLabels WHERE userid = {0}", userId)
        Dim dtDoctype As DataTable = Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
        Dim ds As New DataSet
        Dim DtTareas As New DataTable
        Dim PrimerItem As Boolean = True
        Dim RF As New RightFactory
        For Each item As DataRow In dtDoctype.Rows
            If RF.GetUserRights(userId, ObjectTypes.DocTypes, Core.RightsType.View, Int64.Parse(item.ItemArray(0))) Then
                Dim querySlect As String = String.Format("Select TOP(100) T.name Tarea ,CONVERT(VARCHAR, udate, 103) Fecha ,s.name Etapa ,isnull(g.name, '') Asignado ,CONVERT(VARCHAR, w.checkin, 103) Ingreso ,CONVERT(VARCHAR, w.expiredate, 103) Vencimiento ,docid ,doctypeid ,task_id FROM DocumentLabels l INNER JOIN DOC_T{1}  T ON T.Doc_ID = l.docid left JOIN wfdocument w ON w.Doc_ID = l.docid AND w.DOC_TYPE_ID = l.doctypeid left JOIN wfstep s ON s.step_id = w.step_id LEFT JOIN zuser_or_group g ON g.id = w.user_asigned WHERE userid = {0} AND importance = 1 ORDER BY udate DESC", userId, item.ItemArray(0))
                Dim dtRows As DataTable = Server.Con.ExecuteDataset(CommandType.Text, querySlect).Tables(0)

                If PrimerItem Then
                    DtTareas = dtRows.Copy
                    PrimerItem = False
                Else
                    For Each Row As DataRow In dtRows.Rows
                        DtTareas.ImportRow(Row)
                    Next
                End If
            End If
        Next

        ds.Tables.Add(DtTareas)
        Return ds
    End Function

    Public Function GetStarsCount(ByVal userId As Int64) As Int64
        Dim query As String = String.Format("SELECT count(1) FROM DocumentLabels l inner join wfdocument w on w.Doc_ID = l.docid and w.DOC_TYPE_ID = l.doctypeid inner join wfstep s on s.step_id = w.step_id left join zuser_or_group g on g.id = w.user_asigned  WHERE userid = {0} AND  importance = 1 ", userId)
        Return Server.Con.ExecuteScalar(CommandType.Text, query)

    End Function

End Class
