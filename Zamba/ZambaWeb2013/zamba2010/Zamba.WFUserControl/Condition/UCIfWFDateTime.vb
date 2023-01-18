Public Class UCIfWFDateTime
    Inherits ZRuleControl

    Public Sub New(ByRef _rule As IIfWFDateTime, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(_rule, _wfPanelCircuit)
        InitializeComponent()
    End Sub


End Class
