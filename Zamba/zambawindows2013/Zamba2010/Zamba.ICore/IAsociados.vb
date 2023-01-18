Public Interface IAsociados
    Inherits IZClass
    Property Description() As String
    Property DocType2() As Int64
    Property DocType1() As Int64
    Property Index2() As IIndex
    Property Index1() As IIndex
    Property ParentId() As Int64
    Property ParentType() As DocAsocRelation
    Property IndexKey() As Int64
End Interface