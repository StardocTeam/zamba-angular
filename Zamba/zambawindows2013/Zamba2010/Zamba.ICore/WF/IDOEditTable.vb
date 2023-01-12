Public Interface IDOEditTable
    Inherits IRule
    Property VarSource() As String
    Property KeyColumn() As String
    Property EditColumn() As String
    Property EditType() As Int64
    Property VarDestiny() As String
End Interface