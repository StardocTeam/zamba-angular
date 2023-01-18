Public Interface IDOADDTOWF
    Inherits IRule
    Property WorkId() As Int32
    Function WorkFlowName() As String
    Property openTask() As Boolean
End Interface