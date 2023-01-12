Public Interface IDOShowEditTable
    Inherits IRule
    Property SQLSelectId() As Int32
    Property VarSource() As String
    Property ShowColumns() As String
    Property SelectMultiRow() As Boolean
    Property VarDestiny() As String
    Property GetSelectedCols() As String
    Property ShowCheckColumn() As Boolean
    Property ShowDataOnly() As Boolean
    Property CheckedItems() As String
    Property CheckedItemsColumn() As String
    Property EditColumns() As String
End Interface