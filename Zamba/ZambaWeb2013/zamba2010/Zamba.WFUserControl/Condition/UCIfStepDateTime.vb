Public Class UCIfStepDateTime
    Inherits ZRuleControl
    Public Sub New(ByRef rule As IIfStepDateTime, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
    End Sub
End Class
