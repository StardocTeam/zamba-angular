Imports System.Net
Imports System.Text
Imports System.IO
Imports Zamba.Bus


Public Class UCDoConsumeRestApi
    Inherits ZRuleControl
    Dim CurrentRule As IDoConsumeRestApi
    Private oCookies As CookieCollection

    Public Sub New(ByVal CurrentRule As IDoConsumeRestApi, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        Me.CurrentRule = CurrentRule
        txtUrl.Text = CurrentRule.Url
        txtJSONMessage.Text = CurrentRule.JsonMessage
        txtResult.Text = CurrentRule.ResultVar
        cboMethod.SelectedItem = CurrentRule.Method
        txtUrl.Multiline = False
        txtJSONMessage.Multiline = True
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        CurrentRule.Url = txtUrl.Text
        CurrentRule.JsonMessage = txtJSONMessage.Text
        CurrentRule.Method = cboMethod.SelectedItem
        CurrentRule.ResultVar = txtResult.Text

        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Url)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.ResultVar)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.JsonMessage)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.Method)

        UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        lblCambios.Text = "Cambios aplicados con éxito"
    End Sub

    Private Sub TextoInteligente1_TextChanged(sender As Object, e As EventArgs) Handles txtResult.TextChanged

    End Sub

    Private Sub lblAyudaUrl_Click(sender As Object, e As EventArgs) Handles lblAyudaUrl.Click

    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Dim RestApiHelper As New Zamba.Core.RestApiHelper
        RestApiHelper.test(txtUrl.Text, cboMethod.SelectedItem, txtJSONMessage.Text)
    End Sub

    Private Sub txtUrl_TextChanged(sender As Object, e As EventArgs) Handles txtUrl.TextChanged

    End Sub

    Private Sub TbRule_Click(sender As Object, e As EventArgs) Handles tbRule.Click

    End Sub
End Class
