Public Interface IDoGenerateDinamicForm
    Inherits IRule

    Property DocType() As Int64
    Property Variable() As String
    Property Name() As String
    Property FormId() As Integer
    Property ColumnName() As String
End Interface


