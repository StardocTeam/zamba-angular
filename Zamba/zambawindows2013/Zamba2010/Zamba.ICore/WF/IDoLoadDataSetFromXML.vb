Public Interface IDoLoadDataSetFromXML
    Inherits IRule

    Property StartTag() As String

    Property EndTag() As String

    Property XMLSource() As String

    Property DataSetName() As String
End Interface
