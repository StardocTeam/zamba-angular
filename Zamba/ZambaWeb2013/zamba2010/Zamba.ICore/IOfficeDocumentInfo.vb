Public Interface IOfficeDocumentInfo
    Inherits IZClass

    Property DoctypeId() As Int32
    Property Doc_ver() As Integer
    Property DocId() As Int64
    Property Parentver() As Int64
    Property Isinzamba() As String
    Property Revise() As Int16
    Property FullPath() As String
End Interface