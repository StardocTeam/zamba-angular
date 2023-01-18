
Public Class Nullable
    Inherits Constraint

#Region "Atributos"

#End Region

#Region "Propiedades"

#End Region

#Region "Constructores"
    Public Sub New(ByVal columnName As String)
        MyBase.New(columnName)
    End Sub
    Public Sub New(ByVal columnName As String, ByVal table As Table)
        MyBase.New(columnName, table)
    End Sub
#End Region

End Class
