<Serializable>
Public Class ErrorReportAttachment
    Implements IDisposable

    Public Property Id() As Int64
    Public Property FileName() As String
    Public Property File() As Byte()

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
        If Not Me.disposedValue Then
            If disposing Then
                _file = Nothing
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
