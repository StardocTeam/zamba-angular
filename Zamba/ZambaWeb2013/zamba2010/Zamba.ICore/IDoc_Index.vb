Public Interface IDoc_Index1
    Property [Operator]() As String
    Property AutoDisplay() As Boolean
    ReadOnly Property Column() As String
    Property Data() As String
    Property Data2() As String
    Property DropDown() As IndexAdditionalType
    Property AutoFill() As Boolean
    Property Id() As Integer
    Property IndexTypes() As ArrayList
    Property Invisible() As Boolean
    Property Len() As Integer
    Property Name() As String
    Property NoIndex() As Boolean
    Property Object_Type_Id() As Integer
    Property OrderSort() As OrderSorts
    Property SoloLectura1() As Boolean
    Property Type() As IndexDataType
    Property DropDownList() As ArrayList
    Event LogError()
    Function Validate() As Boolean
    Function Validate2() As Boolean
    Sub FillIndexData(ByVal index As DataRow)
End Interface