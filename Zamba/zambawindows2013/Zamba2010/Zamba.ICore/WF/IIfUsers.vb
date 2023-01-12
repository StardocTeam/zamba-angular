Public Interface IIfUsers
    Inherits IRule
    Property UserIdsList() As List(Of Int64)
    Property Comparator() As UserComparators
End Interface
