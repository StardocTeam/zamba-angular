Public Interface IDOMail
    Inherits IRule
    Property Para() As String
    Property CC() As String
    Property CCO() As String
    Property Asunto() As String
    Property Body() As String
    Property SendDocument() As Boolean
    Property AttachAssociatedDocuments() As Boolean
    Property AttachLink() As Boolean
    Property ImagesNames() As String
    Property PathImages() As String
    Property groupMailTo() As Boolean
    Property DTType() As IMailConfigDocAsoc.DTTypes
    Property Selection() As IMailConfigDocAsoc.Selections
    Property DocTypes() As String
    Property Index() As Int32
    Property Oper() As Comparadores
    Property IndexValue() As String
    Property Automatic() As Boolean
    Property UseSMTPConfig() As Boolean
    Property SmtpServer() As String
    Property SmtpPort() As String
    Property SmtpMail() As String
    Property SmtpUser() As String
    Property SmtpPass() As String
    Property SmtpEnableSsl() As Boolean
    Property KeepAssociatedDocsName() As Boolean
    Property EmbedImages() As Boolean
    Property SaveMailPath() As Boolean
    Property MailPath() As String
    Property DisableHistory() As Boolean
    Property FilterDocID() As String
    Property RuleID() As Int64
    Property BtnName() As String
    Property VarAttachs() As String
    Property ColumnName() As String
    Property ColumnRoute() As String

    'funcionalidad ejecucion de regla 12-03-2012
    Property ExecuteAdditionalRuleID() As Int64
    Property BtnAdditionalRuleName() As String
    'viewOriginalDocumentTab
    Property ViewOriginal() As Boolean
    Property ViewAssociateDocuments() As Boolean
    Property AdditionalRuleColumnName() As String
    Property AdditionalRuleColumnRoute() As String

    'Estas tres opciones se utilizan para configurar la opción para adjuntar documentos binarios o asociados con binarios
    Property AttachTableVar() As String
    Property AttachTableColDocTypeId() As String
    Property AttachTableColDocId() As String
    Property AttachTableColDocName() As String
    'Estas tres opciones se utilizan para configurar la opción de responder
    Property Answer() As Boolean
End Interface