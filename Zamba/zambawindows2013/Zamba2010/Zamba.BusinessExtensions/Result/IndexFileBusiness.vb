Imports Zamba.Servers

Public Class IndexFileBusiness

    Dim RB As New Results_Business

    Public Sub PeekQuequedFile(estadoInicial As IndexedState, ServiceId As Int64)
        If Server.isOracle Then
            Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("UPDATE ZINDEXERSTATE SET STATE = {1} WHERE DOCID in (SELECT DOCID FROM (SELECT DOCID FROM ZINDEXERSTATE WHERE STATE = {0}  order by ""DOCID"" desc) WHERE ROWNUM < 100000)", Int32.Parse(estadoInicial), ServiceId + 100))
        Else
            Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("UPDATE ZINDEXERSTATE SET STATE = {1} WHERE DOCID in (SELECT TOP(100000) DOCID FROM ZINDEXERSTATE WHERE STATE = {0} order by DOCID desc) ", Int32.Parse(estadoInicial), ServiceId + 100))
        End If

        Dim currentDocument As DataSet = Server.Con.ExecuteDataset(CommandType.Text, String.Format("SELECT DOCID, DOCTYPE FROM ZINDEXERSTATE WHERE STATE = {0}", ServiceId + 100))

        If (currentDocument IsNot Nothing AndAlso currentDocument.Tables.Count > 0 AndAlso currentDocument.Tables(0).Rows.Count > 0) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Documentos para Indexar: " & currentDocument.Tables(0).Rows.Count)

            Dim docId As Integer
            Dim docTypeId As Integer
            For Each r As DataRow In currentDocument.Tables(0).Rows
                docId = Integer.Parse(r(0).ToString())
                docTypeId = Integer.Parse(r(1).ToString())
                ProcessDocument(docId, docTypeId, ServiceId)
            Next
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Documentos para Indexar: 0")

        End If

    End Sub

    Private Sub ProcessDocument(docId As Integer, docTypeId As Integer, ServiceId As Integer)

        If Not IsDBNull(docId) AndAlso docId <> 0 Then
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Indexación por servicio: DocID:" & docId.ToString & " EntityId: " & docTypeId.ToString())
            SyncLock (RB)
                Dim path As String = String.Empty
                'If (UserPreferences.getValueForMachine("IndexContent", Sections.Indexer, False) = True) Then
                Try
                    path = RB.GetFullPath(docId, docTypeId)
                Catch ex As Exception
                    If ex.Message = "Documento inexistente" Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("UPDATE ZINDEXERSTATE SET STATE = {0} WHERE DOCID = {1}", IndexedState.NoEncontrado.GetHashCode(), docId.ToString))
                        Exit Sub
                    Else
                        Throw ex
                    End If
                End Try

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Path obtenido:" & path)
                'End If
                Dim resultado As IndexedState = RB.IndexFile(path, docId, docTypeId, ServiceId)
                Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("UPDATE ZINDEXERSTATE SET STATE = {0} WHERE DOCID = {1}", resultado.GetHashCode(), docId.ToString))
            End SyncLock
        Else
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "No hay documentos para Indexar")
        End If
    End Sub
End Class

