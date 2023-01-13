Public Class UCDoRefreshWfTree
    Inherits ZRuleControl

    Public Sub New(ByRef rule As IDoRefreshWfTree, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
    End Sub
End Class