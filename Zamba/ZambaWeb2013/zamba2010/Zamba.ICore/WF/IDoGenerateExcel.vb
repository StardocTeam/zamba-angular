Public Interface IDoGenerateExcel
    Inherits IRule
    Property DocTypeId() As Int32
    Property Title() As String
    Property Index() As String
    Property Footer() As String
End Interface
