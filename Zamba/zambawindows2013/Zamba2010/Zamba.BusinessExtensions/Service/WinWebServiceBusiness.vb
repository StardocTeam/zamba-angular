Imports Zamba.DataExt.WSResult.Consume

Public Class WinWebServiceBusiness : Implements IDisposable

    Private _wsClient As WSResultsFactory

    Public Sub New()
        _wsClient = New WSResultsFactory()
    End Sub

    Function GetMessageFile(ByVal url As String, ByVal userid As Long) As Byte()
        Dim mail As Byte()
        Try
            mail = _wsClient.ConsumeGetMail(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(url)), userid)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return mail
    End Function

    Function SaveMessageFile(ByVal url As String, ByVal file() As Byte) As Boolean
        Try
            Return _wsClient.ConsumeSaveMessageFileBlob(url, file)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return False
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If _wsClient IsNot Nothing Then
                    _wsClient.Dispose()
                    _wsClient = Nothing
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
