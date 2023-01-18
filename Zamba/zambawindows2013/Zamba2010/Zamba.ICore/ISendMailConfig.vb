Public Interface ISendMailConfig
    Property MailType As MailTypes
    Property From As String
    Property SMTPServer As String
    Property SMTPPort As String
    Property SMTPUserName As String
    Property SMPTPassword As String
    Property MailTo As String
    Property Cc As String
    Property Cco As String
    Property Subject As String
    Property Body As String
    Property IsBodyHtml As Boolean
    Property AttachFileNames As List(Of String)
    Property UserId As Int64
    Property ArrayLinks As ArrayList
    Property ImagesToEmbedPaths As List(Of String)
    Property Basemail As String
    Property OriginalDocument As Byte()
    Property OriginalDocumentFileName As String
    Property EnableSsl As Boolean
    Property UseWebService As Boolean
    Property SourceDocId As Long
    Property SourceDocTypeId As Long
    Property SaveHistory As Boolean
    Property Attaches As List(Of IBlobDocument)
    Property LinkToZamba As Boolean
End Interface
