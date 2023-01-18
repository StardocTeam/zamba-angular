Public Interface IIndex
    Inherits ICore

    Property Type() As IndexDataType
    Property Len() As Integer
    Property AutoFill() As Boolean
    Property NoIndex() As Boolean
    Property AutoDisplay() As Boolean
    Property Invisible() As Boolean
    Property Object_Type_Id() As Integer
    Property AutoFill1() As Integer
    Property NoIndex1() As Integer
    Property AutoDisplay1() As Integer
    Property Invisible1() As Integer
    Property IndexTypes() As ArrayList
    Property Datachange() As Boolean
    Property [Operator]() As String
    Property DropDownList() As ArrayList
    Property OrderSort() As OrderSorts
    Property dataDescription() As String
    Property dataDescription2() As String
    Property DataTemp() As String
    Property DataTemp2() As String
    Property dataDescriptionTemp() As String
    Property dataDescriptionTemp2() As String
    Property Data() As String
    Property Data2() As String
    Property Required() As Boolean
    Function isvalid() As Boolean
    Property DropDown() As IndexAdditionalType
    Property Visible() As Boolean
    ReadOnly Property Column() As String
    Property DefaultValue() As String
    Property AutoIncremental() As Boolean
    Property isReference() As Boolean
    Property Enabled() As Boolean
    Property HierarchicalParentID() As Integer
    Property HierarchicalChildID() As List(Of Long)
    Property HierarchicalDataTableName() As String
End Interface