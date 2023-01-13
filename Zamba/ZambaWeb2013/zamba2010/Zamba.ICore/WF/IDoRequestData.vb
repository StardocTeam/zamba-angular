Public Interface IDoRequestData
    Inherits IRule
    Function JoinIds() As String
    Property DocTypeId() As Int32
    Property ArrayIds() As ArrayList
End Interface