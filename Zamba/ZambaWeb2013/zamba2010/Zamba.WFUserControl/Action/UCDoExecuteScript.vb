Public Class UCDoExecuteScript
    Inherits ZRuleControl

    Private CurrentRule As IDoExecuteScript

    Public Sub New(ByRef rule As IDoExecuteScript, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = rule

        Try
            RadRichTextEditor1.Text = CurrentRule.Script
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnSaveValues_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSaveValues.Click
        Try
            CurrentRule.Script = Me.RadRichTextEditor1.Text
            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Script)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
