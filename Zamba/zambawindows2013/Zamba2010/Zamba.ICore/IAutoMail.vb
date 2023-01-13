Public Interface IAutoMail
    Property Body() As String
    Property Subject() As String
    Property From() As String
    Property Name() As String
    Property MailTo() As String
    Property CC() As String
    Property CCO() As String
    Property Confirmation() As Boolean
    Property AddDocument() As Boolean
    Property AddLink() As Boolean
    Property AttachmentsPaths() As Collections.Generic.List(Of String)
    Property ID() As Integer
    Property _Attach() As ArrayList
    Property PathImages() As Collections.Generic.List(Of String)
    Property TaskID() As Integer
    Property DocTypeID() As Integer
End Interface