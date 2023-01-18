Imports Zamba.Servers
Imports System.IO

public Class IndexFileBusiness

    Public Sub PeekQuequedFile(estadoInicial As IndexedState)

        Dim docId As Integer

        If Server.isOracle Then
            docId = Server.Con.ExecuteScalar(CommandType.Text, String.Format("SELECT * FROM (SELECT DOCID FROM ZINDEXERSTATE WHERE STATE = {0} order by ""DATE"" desc) WHERE ROWNUM = 1", Int32.Parse(estadoInicial)))
        Else
            docId = Server.Con.ExecuteScalar(CommandType.Text, String.Format("SELECT TOP(1) DOCID FROM ZINDEXERSTATE WHERE STATE = {0} order by Date desc ", Int32.Parse(estadoInicial)))
        End If

        If Not IsDBNull(docId) AndAlso docId <> 0 Then

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Indexación por servicio: DocID:" + docId.ToString)

            'Se cambia el estado del documento a 'Indexado en proceso'
            Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("UPDATE ZINDEXERSTATE SET STATE = 6 WHERE DOCID = {0}", docId.ToString))

            Dim docTypeId As Integer = Server.Con.ExecuteScalar(CommandType.Text, String.Format("SELECT DOCTYPE FROM ZINDEXERSTATE WHERE DOCID = {0}", docId))

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo path del documento")

            Dim RB As New Results_Business
            Dim path As String = RB.GetFullPath(docId, docTypeId)

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Path obtenido:" + path)

            Dim resultado As IndexedState = RB.IndexFile(path, docId, docTypeId)

            Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("UPDATE ZINDEXERSTATE SET STATE = {0} WHERE DOCID = {1}", resultado.GetHashCode(), docId.ToString))

        Else
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "No hay documentos para Indexar")
        End If

    End Sub

End Class

