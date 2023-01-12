
Namespace Analysis
    Public Interface IIfRuleNode
        Inherits IRuleNode

        ReadOnly Property Validation() As String
        ReadOnly Property ValidCount() As Int64
        ReadOnly Property InvalidCount() As Int64

    End Interface
End Namespace