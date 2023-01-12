Imports System.Text
Imports System.Collections.Generic
Imports Zamba.Servers

Public Class ExportFactory
    ''' <summary>
    ''' Insert into zExportControl a new registry
    ''' </summary>
    ''' <param name="userId">User ID</param>
    ''' <param name="codigo">Mail Unique Code</param>
    ''' <param name="DocTypeId">DocType ID</param>
    ''' <history>   Marcelo     Created     26/11/2009</history>
    ''' <remarks></remarks>
    Public Shared Sub InsertIncomingMail(ByVal userId As Int64, ByVal codigo As String, ByVal DocTypeId As Int64)
        If Server.IsOracle Then
            Dim parNames() As String = {"ZUSERID", "ZCODIGO", "ZDOC_TYPE_ID"}
            ' Dim parTypes() As Object = {13, 13, 13}
            Dim parValues() As Object = {userId, codigo, DocTypeId}
            Server.Con.ExecuteNonQuery("ZSP_EXPORTMAIL_200.InsertIncomingMail", parValues)
        Else
            Dim parameters() As Object = {userId, codigo, DocTypeId}
            Server.Con.ExecuteNonQuery("ZSP_EXPORTMAIL_200_InsertIncomingMail", parameters)
        End If
    End Sub

    ''' <summary>
    ''' Update mail insert state
    ''' </summary>
    ''' <param name="codigo">Mail Unique Code</param>
    ''' <remarks></remarks>
    Public Shared Sub SetMailInserted(ByVal codigo As String)
        If Server.isOracle Then
            Dim parNames() As String = {"ZCODIGO"}
            ' Dim parTypes() As Object = {13}
            Dim parValues() As Object = {codigo}
            Server.Con.ExecuteNonQuery("ZSP_EXPORTMAIL_100.SetMailInserted", parValues)
        Else
            Dim parameters() As Object = {codigo}
            Server.Con.ExecuteNonQuery("ZSP_EXPORTMAIL_100_SetMailInserted", parameters)
        End If
    End Sub


    Public Shared Function InsertDocument(ByVal name As String, ByVal docTypeName As String, ByVal zambaLink As String) As Int64
        Dim QueryBuilder As New StringBuilder()

        QueryBuilder.Append("INSERT INTO ZambaExport(DocumentName, DocTypeName, Link, IsExported) VALUES('")
        QueryBuilder.Append(name)
        QueryBuilder.Append("','")
        QueryBuilder.Append(docTypeName)
        QueryBuilder.Append("','")
        QueryBuilder.Append(zambaLink)
        QueryBuilder.Append("',0) ")

        QueryBuilder.Append("SELECT MAX(temporaryid) FROM ZambaExport")

        Dim RemoteInsertId As Int64 = Convert.ToInt64(Server.Con.ExecuteScalar(CommandType.Text, QueryBuilder.ToString()))
        QueryBuilder.Remove(0, QueryBuilder.Length)

        Return RemoteInsertId
    End Function

    Public Shared Function InsertDocument(ByVal name As String, ByVal docTypeName As String, ByVal zambaLink As String, ByRef t As Transaction) As Int64
        Dim QueryBuilder As New StringBuilder()

        QueryBuilder.Append("INSERT INTO ZambaExport(DocumentName, DocTypeName, Link, IsExported) VALUES('")
        QueryBuilder.Append(name)
        QueryBuilder.Append("','")
        QueryBuilder.Append(docTypeName)
        QueryBuilder.Append("','")
        QueryBuilder.Append(zambaLink)
        QueryBuilder.Append("',0) ")

        QueryBuilder.Append("SELECT MAX(temporaryid) FROM ZambaExport")

        Dim RemoteInsertId As Int64 = Convert.ToInt64(t.Con.ExecuteScalar(t.Transaction, CommandType.Text, QueryBuilder.ToString()))
        QueryBuilder.Remove(0, QueryBuilder.Length)

        Return RemoteInsertId
    End Function

    Public Shared Sub InsertIndex(ByVal exportId As Int64, ByVal indexName As String, ByVal indexTypeName As String, ByVal indexValue As String)

        Dim QueryBuilder As New StringBuilder()

        QueryBuilder.Append("INSERT INTO ZambaExportIndexes(TemporaryId, IndexName, IndexTypeName, IndexValue) VALUES(")
        QueryBuilder.Append(exportId.ToString())
        QueryBuilder.Append(",'")
        QueryBuilder.Append(indexName)
        QueryBuilder.Append("','")
        QueryBuilder.Append(indexTypeName)
        QueryBuilder.Append("','")
        QueryBuilder.Append(indexValue)
        QueryBuilder.AppendLine("')")

        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())
        QueryBuilder.Remove(0, QueryBuilder.Length)
    End Sub

    Public Shared Sub InsertIndex(ByVal exportId As Int64, ByVal indexName As String, ByVal indexTypeName As String, ByVal indexValue As String, ByRef t As Transaction)

        Dim QueryBuilder As New StringBuilder()

        QueryBuilder.Append("INSERT INTO ZambaExportIndexes(TemporaryId, IndexName, IndexTypeName, IndexValue) VALUES(")
        QueryBuilder.Append(exportId.ToString())
        QueryBuilder.Append(",'")
        QueryBuilder.Append(indexName)
        QueryBuilder.Append("','")
        QueryBuilder.Append(indexTypeName)
        QueryBuilder.Append("','")
        QueryBuilder.Append(indexValue)
        QueryBuilder.AppendLine("')")

        t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, QueryBuilder.ToString())
        QueryBuilder.Remove(0, QueryBuilder.Length)
    End Sub

    Public Shared Sub InsertExportedPDF(ByVal UserID As Integer, ByVal DocID As Long, ByVal DocTypeID As Long, ByVal DocFile As String)

        Dim QueryBuilder As New StringBuilder()

        QueryBuilder.Append("INSERT INTO ZPRINT ")
        QueryBuilder.Append(" (CDATE, USERID, DOCID, DOCTYPE, DOCFILE, MOVED) ")
        QueryBuilder.Append(" VALUES(")

        If Server.isOracle Then
            QueryBuilder.Append("sysdate")
        Else
            QueryBuilder.Append("getdate()")
        End If

        QueryBuilder.Append(",")
        QueryBuilder.Append(UserID)
        QueryBuilder.Append(",")
        QueryBuilder.Append(DocID)
        QueryBuilder.Append(",")
        QueryBuilder.Append(DocTypeID)
        QueryBuilder.Append(",'")
        QueryBuilder.Append(DocFile)
        QueryBuilder.Append("', 0)")

        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())

    End Sub

    Public Shared Function getFolderToMovePDF(ByVal FileName As String) As String

        Dim QueryBuilder As New StringBuilder()
        Dim SQL As String
        Dim DocId As Long = 0
        Dim DocTypeId As Long = 0

        'eliminar la extension
        FileName = FileName.Split(".")(0)

        'obtener el doc_id del archivo
        SQL = "SELECT DOCTYPE FROM ZPRINT WHERE MOVED = 0 AND SUBSTR(DOCFILE, 0, " + FileName.Length.ToString + ")  = '" & FileName & "'"
        Long.TryParse(Server.Con.ExecuteScalar(CommandType.Text, SQL), DocTypeId)

        SQL = "SELECT DOCID FROM ZPRINT WHERE MOVED = 0 AND SUBSTR(DOCFILE, 0, " + FileName.Length.ToString + ") = '" & FileName & "'"
        Long.TryParse(Server.Con.ExecuteScalar(CommandType.Text, SQL), DocId)

        If DocId > 0 And DocTypeId > 0 Then

            'obtener el fullpath del archivo en el volumen
            QueryBuilder.Append("SELECT ")

            If Server.isOracle Then
                QueryBuilder.Append("  DISK_VOL_PATH || '\' || DT.DOC_TYPE_ID || '\' || DT.OFFSET || '\' ")
            Else
                QueryBuilder.Append("  DISK_VOL_PATH + '\' + DT.DOC_TYPE_ID + '\' + DT.OFFSET + '\' ")
            End If

            QueryBuilder.Append("FROM ")
            QueryBuilder.Append("   DOC_T" & DocTypeId.ToString() & " DT ")
            QueryBuilder.Append("   INNER JOIN DISK_VOLUME DV ON DT.VOL_ID = DV.DISK_VOL_ID ")
            QueryBuilder.Append("WHERE ")
            QueryBuilder.Append("   DT.DOC_ID = " & DocId.ToString())

            Return Server.Con.ExecuteScalar(CommandType.Text, QueryBuilder.ToString())

        Else

            Return String.Empty

        End If

    End Function

    Public Shared Sub updatePDFAsMoved(ByVal FileName As String)

        Dim SQL As String

        FileName = FileName.Split(".")(0)

        SQL = "UPDATE ZPRINT SET MOVED = 1, MOVED_DATE = "

        If Server.isOracle Then
            SQL = SQL & "sysdate"
        Else
            SQL = SQL & "getdate()"
        End If

        SQL = SQL & " WHERE SUBSTR(DOCFILE, 0, " + FileName.Length.ToString + ") = '" & FileName & "'"

        Server.Con.ExecuteNonQuery(CommandType.Text, SQL)

    End Sub

End Class
