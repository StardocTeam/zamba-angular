Public Class UCDoCloseZamba
    Inherits ZRuleControl

    Private _currentRule As IDoCloseZamba
    Private _ucTemplate As Zamba.Controls.UCTemplatesNew
    Private atributos As SortedList = New SortedList()

    Public Sub New(ByRef CurrentRule As IDoCloseZamba, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        _currentRule = CurrentRule
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click

    End Sub

    Private Sub tbRule_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles tbRule.Click

    End Sub
End Class
