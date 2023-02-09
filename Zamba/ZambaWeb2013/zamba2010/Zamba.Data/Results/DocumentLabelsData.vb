Public Class DocumentLabelsData

    Public Shared Sub UpdateImportantLabels(ByVal docTypeId As Int64, ByVal docId As Int64, ByVal important As Boolean)
        Dim labelValue As Int32
        If important Then
            labelValue = 1
        Else
            labelValue = 0
        End If
        If Server.isOracle Then
            '            Dim query As String = String.Format("BEGIN UPDATE DocumentLabels SET favorite = {3}, udate = SYSDATE WHERE  userId = {0} And doctypeid = {1} And docid = {2} END IF ( sql%rowcount = 0 ) THEN BEGIN INSERT INTO DocumentLabels (CRDATE, UDATE, USERID, DOCTYPEID, DOCID, IMPORTANCE, FAVORITE) VALUES (SYSDATE, SYSDATE, {0}, {1}, {2}, {3},0) END END IF", 0, docTypeId, docId, labelValue)
            Dim query As String = String.Format("SELECT count(1) FROM DocumentLabels WHERE doctypeid = {0} And	docid = {1} and userid = {3}", docTypeId, docId, labelValue, 0)
            Dim count As Int64 = Server.Con.ExecuteScalar(CommandType.Text, query)
            If IsDBNull(count) = False AndAlso count > 0 Then
                Dim Uquery As String = String.Format("UPDATE DocumentLabels SET importance = '{2}', udate = SYSDATE WHERE doctypeid = {0} And docid = {1} and userid = {3} ", docTypeId, docId, labelValue, 0)
                Server.Con.ExecuteNonQuery(CommandType.Text, Uquery)
            Else
                Dim Iquery As String = String.Format("INSERT INTO DocumentLabels (CRDATE, UDATE, USERID, DOCTYPEID, DOCID, IMPORTANCE, FAVORITE) VALUES (SYSDATE, SYSDATE,{3}, {0}, {1}, {2},0)", docTypeId, docId, labelValue, 0)
                Server.Con.ExecuteNonQuery(CommandType.Text, Iquery)
            End If
        Else
            Dim query As String = String.Format("If EXISTS(SELECT * FROM DocumentLabels WHERE doctypeid = {0} And docid = {1} and userid = 0) UPDATE DocumentLabels SET IMPORTANCE = '{2}', udate = getdate() WHERE doctypeid = {0} And docid = {1} and userid = 0 Else INSERT INTO DocumentLabels (CRDATE, UDATE, USERID, DOCTYPEID, DOCID, IMPORTANCE, FAVORITE) VALUES (GETDATE(), getdate(),0, {0}, {1}, {2},0)", docTypeId, docId, labelValue)
            Server.Con.ExecuteNonQuery(CommandType.Text, query)
        End If

    End Sub

    Public Shared Sub UpdateFavoriteLabels(ByVal docTypeId As Int64, ByVal docId As Int64, ByVal favorite As Boolean, ByVal userId As Int64)
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

    Public Shared Function GetDocumentLabelsByUser(ByVal userId As Int64, ByVal docId As Int64) As DataSet
        Dim query As String = String.Format("  SELECT 'IMPORTANCE' as LABEL, COUNT(1) as FLAG FROM DocumentLabels WHERE userid = 0 AND docid = {1} AND importance = 1 UNION ALL SELECT 'FAVORITE' as LABEL, COUNT(1) as FLAG FROM DocumentLabels WHERE userid = {0} AND docid = {1} AND favorite = 1", userId, docId)
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query)
        Return ds
    End Function
End Class
