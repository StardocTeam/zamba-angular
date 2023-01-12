Public Interface IDoGenerateCoverPage_v2
    Inherits IRule

    Property DocTypeId() As Int64
    Property PrintIndexs() As Boolean
    Property Note() As String
    Property SetPrinter() As Boolean
    Property Copies() As Int16
End Interface
