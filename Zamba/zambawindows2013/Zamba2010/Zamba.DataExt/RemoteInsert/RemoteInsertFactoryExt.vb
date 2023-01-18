Imports System.Text

Public NotInheritable Class RemoteInsertFactoryExt
    Implements IDisposable

    Dim CurrentConnection As IConnection = Servers.Server.Con(False)

    Public Sub SaveDocumentError(ByVal temporaryId As Int64, ByVal errorMessage As String, ByVal statusId As Int32, ByVal transactionId As Int64)
        If Servers.Server.isOracle Then
            '
            'ORACLE ESTA OBSOLETO
            '
            errorMessage = errorMessage.Replace("'", String.Empty) ' Remplazo los "'" de la exception porque rompen las consultas SQL

            Dim Query As New StringBuilder()
            Query.Append("UPDATE RemoteInsert SET Status=")
            Query.Append(statusId.ToString())
            Query.Append(", Information='")
            Query.Append(errorMessage)
            Query.Append("' WHERE TemporaryID = ")
            Query.Append(temporaryId.ToString())

            CurrentConnection.ExecuteNonQuery(CommandType.Text, Query.ToString())

            Query.Remove(0, Query.Length)
            Query = Nothing
        Else
            Dim parValues() As Object = {temporaryId, errorMessage, statusId, transactionId}
            CurrentConnection.ExecuteNonQuery("ZSP_THREADPOOL_100_RemoteInsert_SaveDocumentError", parValues)
            parValues = Nothing
        End If
    End Sub

    Public Sub SaveDocumentStatus(ByVal temporaryId As Int64, ByVal statusId As Int32, ByVal ResultId As Int64, ByRef t As Servers.Transaction)
        If Servers.Server.isOracle Then
            Dim Query As New StringBuilder()
            Query.Append("UPDATE RemoteInsert SET Status=")
            Query.Append(statusId.ToString())
            Query.Append(", DocumentId=")
            Query.Append(ResultId.ToString())
            Query.Append(", InsertDate=getdate()")
            Query.Append(" WHERE TemporaryID=")
            Query.Append(temporaryId.ToString())

            If t Is Nothing Then
                CurrentConnection.ExecuteNonQuery(CommandType.Text, Query.ToString())
            Else
                t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, Query.ToString)
            End If

            Query.Remove(0, Query.Length)
            Query = Nothing
        Else
            Dim parValues() As Object = {statusId, ResultId, temporaryId}
            If t Is Nothing Then
                CurrentConnection.ExecuteNonQuery("zsp_100_remoteInsert_SaveDocumentStatus", parValues)
            Else
                t.Con.ExecuteNonQuery(t.Transaction, "zsp_100_remoteInsert_SaveDocumentStatus", parValues)
            End If

            parValues = Nothing
        End If
    End Sub

    Public Sub SaveTransactionStatus(ByVal transactionId As Int64, ByVal statusId As Int32, ByRef t As Servers.Transaction)
        If Servers.Server.isOracle Then
            Throw New NotImplementedException()
        Else
            Dim parValues() As Object = {transactionId, statusId}
            If t Is Nothing Then
                CurrentConnection.ExecuteNonQuery("ZSP_THREADPOOL_100_RemoteInsert_SaveTransactionStatus", parValues)
            Else
                t.Con.ExecuteNonQuery(t.Transaction, "ZSP_THREADPOOL_100_RemoteInsert_SaveTransactionStatus", parValues)
            End If

            parValues = Nothing
        End If
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If CurrentConnection IsNot Nothing AndAlso CurrentConnection.State = IConnection.ConnectionStates.Ready Then
                    CurrentConnection.Close()
                    CurrentConnection.dispose()
                    CurrentConnection = Nothing
                End If
            End If
        End If

        disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
