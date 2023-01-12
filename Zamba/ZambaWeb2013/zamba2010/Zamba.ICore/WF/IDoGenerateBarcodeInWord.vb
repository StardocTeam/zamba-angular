Public Interface IDoGenerateBarcodeInWord
    Property docTypeId() As Int64
    Property Indices() As String
    Property FilePath() As String
    Property Top() As String
    Property Left() As String
    Property ContinueWithCurrentTasks() As Boolean
    Property AutoPrint() As Boolean
    Property InsertBarcode() As Boolean
    Property SaveDocPathVar() As Boolean
    Property DocPathVar() As String
    Property WithoutInsert() As Boolean
End Interface
