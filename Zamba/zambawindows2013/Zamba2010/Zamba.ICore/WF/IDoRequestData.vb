Public Interface IDoRequestData
    Inherits IRule
    Function JoinIds() As String
    Property DocTypeId() As Int64
    Property ArrayIds() As List(Of Int64)
End Interface