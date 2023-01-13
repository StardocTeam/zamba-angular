Public Class BlobDocument
    Implements IBlobDocument

#Region "IBlobDocument Properties"
    Public Property BlobFile As Byte() Implements IBlobDocument.BlobFile
    Public Property Description As String Implements IBlobDocument.Description
    Public Property UpdateDate As Date Implements IBlobDocument.UpdateDate
    Public Property Updateuser As Long Implements IBlobDocument.Updateuser
    Public Property ID As Long Implements ICore.ID
    Public Property Name As String Implements ICore.Name
    Public Property Extension As String Implements IBlobDocument.Extension
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                BlobFile = Nothing
                Description = Nothing
                UpdateDate = Nothing
                Name = Nothing
                Extension = Nothing
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
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
