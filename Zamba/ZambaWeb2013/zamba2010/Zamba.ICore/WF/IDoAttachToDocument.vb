Public Interface IDoAttachToDocument
    Inherits IRule

    Property DocTypeId() As Int64
    Property LimitKB() As String
    Property WithLimit() As Boolean
    Property CurrentSize() As String

End Interface
