Public Class RemoteInsertEntry
    Implements IRemoteInsertEntry
    Implements IDisposable

#Region "Attributes"
    Private _temporaryId As Int64
    Private _docTypeId As Int64
    Private _documentName As String
    Private _fileExtension As String
    Private _serializedFile As Byte()
    Private _transactionId As Int64
#End Region

#Region "Properties"
    Public Property DocTypeId As Long Implements IRemoteInsertEntry.DocTypeId
        Get
            Return _docTypeId
        End Get
        Set(value As Long)
            _docTypeId = value
        End Set
    End Property

    Public Property DocumentName As String Implements IRemoteInsertEntry.DocumentName
        Get
            Return _documentName
        End Get
        Set(value As String)
            _documentName = value
        End Set
    End Property

    Public Property FileExtension As String Implements IRemoteInsertEntry.FileExtension
        Get
            Return _fileExtension
        End Get
        Set(value As String)
            _fileExtension = value
        End Set
    End Property

    Public Property SerializedFile As Byte() Implements IRemoteInsertEntry.SerializedFile
        Get
            Return _serializedFile
        End Get
        Set(value As Byte())
            _serializedFile = value
        End Set
    End Property

    Public Property TemporaryId As Long Implements IRemoteInsertEntry.TemporaryId
        Get
            Return _temporaryId
        End Get
        Set(value As Long)
            _temporaryId = value
        End Set
    End Property

    Public Property TransactionId As Long Implements IRemoteInsertEntry.TransactionId
        Get
            Return _transactionId
        End Get
        Set(value As Long)
            _transactionId = value
        End Set
    End Property
#End Region

#Region "Constructors"
    Public Sub New(ByVal temporaryId As Int64, _
                   ByVal documentName As String, _
                   ByVal docTypeId As Int64, _
                   ByVal fileExtension As String, _
                   ByVal serializedFile As Byte(), _
                   ByVal transactionId As Int64)
        _temporaryId = temporaryId
        _documentName = documentName
        _docTypeId = docTypeId
        _fileExtension = fileExtension
        _serializedFile = serializedFile
        _transactionId = transactionId
    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                If _serializedFile IsNot Nothing Then
                    Array.Clear(_serializedFile, 0, _serializedFile.Length)
                    _serializedFile = Nothing
                End If
                _fileExtension = Nothing
                _documentName = Nothing
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
