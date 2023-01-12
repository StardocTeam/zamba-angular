Public Interface IDOCreateForm
    Inherits IRule
    Event FormCreated(ByRef Result As IResult)
    Property DocTypeIdVirtual() As Int32
    Property AddToWf() As Boolean
    Property HashTable() As String
End Interface