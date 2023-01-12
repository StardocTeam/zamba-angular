Public Class BlobDocument
    Implements IBlobDocument

#Region "Members"
    Private _blobFile As Byte()
    Private _description As String
    Private _updateDate As Date
    Private _updateUser As Long
    Private _id As Long
    Private _name As String
#End Region

#Region "IBlobDocument Properties"
    Public Property BlobFile As Byte() Implements IBlobDocument.BlobFile
        Get
            Return _blobFile
        End Get
        Set(ByVal value As Byte())
            _blobFile = value
        End Set
    End Property

    Public Property Description As String Implements IBlobDocument.Description
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Public Property UpdateDate As Date Implements IBlobDocument.UpdateDate
        Get
            Return _updateDate
        End Get
        Set(ByVal value As Date)
            _updateDate = value
        End Set
    End Property

    Public Property Updateuser As Long Implements IBlobDocument.Updateuser
        Get
            Return _updateUser
        End Get
        Set(ByVal value As Long)
            _updateUser = value
        End Set
    End Property

    Public Property ID As Long Implements ICore.ID
        Get
            Return _id
        End Get
        Set(ByVal value As Long)
            _id = value
        End Set
    End Property

    Public Property Name As String Implements ICore.Name
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                _blobFile = Nothing
                _updateDate = Nothing
                _name = Nothing
                _description = Nothing
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
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
