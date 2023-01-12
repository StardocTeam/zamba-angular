Public Interface IDoChangeState
    Inherits IRule
    ReadOnly Property StateName() As String
    Property StepId() As Int32
    Property StateId() As Int32
End Interface