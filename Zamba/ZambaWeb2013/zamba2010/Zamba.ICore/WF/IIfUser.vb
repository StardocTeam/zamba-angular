Public Interface IIfUser
    Inherits IRule
    Property UserId() As Int64
    Property Comparator() As UserComparators
End Interface
