Public Interface IDoGenerateCoverPage
    Inherits IRule

    Property DocTypeId() As Int64
    Property PrintIndexs() As Boolean
    Property Note() As String
    Property SetPrinter() As Boolean
    Property continueWithGeneratedDocument() As Boolean
    Property DontOpenTaskAfterInsert() As Boolean
    Property Copies() As String
    Property UseTemplate() As Boolean
    Property TemplatePath() As String
    Property UseCurrentTask() As Boolean
    Property CopiesCount As String
    Property templateWidth As Single
    Property templateHeight As Single

End Interface
