Public Interface IDOGenerateOutlook
    Inherits IRule
    Property Para() As String
    Property CC() As String
    Property CCO() As String
    Property Asunto() As String
    Property Body() As String
    Property SendDocument() As Boolean
    Property AttachLink() As Boolean
    Property ImagesNames() As String
    Property PathImages() As String
    Property sendTimeOut() As Integer
    Property automaticSend() As Boolean
    Property ReplyMail() As Boolean
    Property ReplyMsgPath() As String
End Interface