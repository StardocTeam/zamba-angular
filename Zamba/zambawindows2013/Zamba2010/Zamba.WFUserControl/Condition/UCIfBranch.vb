Public Class UCIfBranch

    Inherits ZRuleControl

    Public Sub New(ByRef rule As IIfBranch, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()

        If rule.ifType = True Then
            lblmsj.Text = "La condicion se cumple"
        Else
            lblmsj.Text = "La condicion NO se cumple"
        End If
    End Sub

End Class
