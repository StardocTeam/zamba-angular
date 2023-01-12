Public Interface IDODecisionMatrix
    Inherits IRule
    Property EntityId As Int64
    Property OutputIndex As Int64
    Property AltOutputIndex As Int64
    Property OutputVariable As String
End Interface