<Serializable>
Public Class ErrorReportAttachment
    Implements IDisposable
    Implements IErrorReportAttachment

    Public Property Id() As Int64 Implements IErrorReportAttachment.Id
    Public Property FileName() As String Implements IErrorReportAttachment.FileName
    Public Property File() As Byte() Implements IErrorReportAttachment.File

    Public Sub New()

    End Sub

    Public Sub New(fileName As String, Optional id As Int64 = 0)
        _id = id
        _fileName = fileName
        _file = Nothing
    End Sub

    Public Sub New(fileName As String, file As Byte(), Optional id As Int64 = 0)
        _id = id
        _fileName = fileName
        _file = file
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _file = Nothing
            End If
        End If
        disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose, IErrorReportAttachment.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
