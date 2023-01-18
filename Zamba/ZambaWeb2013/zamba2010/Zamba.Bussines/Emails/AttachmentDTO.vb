Imports System.IO
Imports Spire.Email

Public Class AttachmentDTO
    Public Property Id As String
    Public Property FileName As String
    Public Property Size As String
    Public Property MediaType As String
    Public Property Data As String

    Public Sub New()
    End Sub

    Public Sub New(ByVal att As Attachment)
        Me.New()
        Me.Id = att.ContentId
        Me.FileName = att.ContentType.Name
        Me.Size = att.Data.Length.ToString()
        Me.MediaType = att.ContentType.MediaType
        Me.Data = System.Convert.ToBase64String((CType(att.Data, MemoryStream)).ToArray())
    End Sub
End Class
