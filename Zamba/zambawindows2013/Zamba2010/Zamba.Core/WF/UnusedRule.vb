Public Class UnusedRule
    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Private _unusedType As Enumerators.UnusedRulesTypes
    Public Property UnusedType() As Enumerators.UnusedRulesTypes
        Get
            Return _unusedType
        End Get
        Set(ByVal value As Enumerators.UnusedRulesTypes)
            _unusedType = value
        End Set
    End Property

    Public Sub New(ByVal description As String, ByVal unusedType As Enumerators.UnusedRulesTypes)
        _description = description
        _unusedType = unusedType
    End Sub
End Class
