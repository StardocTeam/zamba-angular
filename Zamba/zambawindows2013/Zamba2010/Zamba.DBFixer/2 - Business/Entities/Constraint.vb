
Public Class Constraint
    Inherits Column


    Public Sub New(ByVal columnName As String)
        MyBase.New(columnName)
    End Sub
    Public Sub New(ByVal columnName As String, ByVal table As Table)
        MyBase.New(columnName, table)
    End Sub
    Public Sub New(ByVal table As Table)
        MyBase.New("CheckConstraint")
    End Sub
End Class
