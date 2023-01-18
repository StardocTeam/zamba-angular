Public Interface IIfGroups
    Inherits IRule
    Property GroupList() As List(Of Int64)
    Property Comparator() As UserComparators
End Interface
