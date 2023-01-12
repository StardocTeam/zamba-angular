Public Interface IIfUsers
    Inherits IRule
    Property UserList() As Hashtable
    Property Comparator() As UserComparators
End Interface
