Public Class News

    Public Sub New(newsId As Long, docTypeID As Long, docId As Long, Name As String, time As String, isRead As Boolean)

        Me.Id = newsId
        Me.DocTypeId = docTypeID
        Me.DocId = docId
        Me.Name = Name
        Me.Time = time
        Me.IsRead = isRead

    End Sub


    Public Property Id() As Long

    Public Property DocTypeId() As Long

    Public Property DocId() As Long

    Public Property Name() As String

    Public Property Time() As String

    Public Property IsRead() As Boolean


End Class
