Public Class UCDoOpenUrl
    Inherits ZRuleControl

    Dim CurrentRule As IDoOpenUrl


    Public Sub New(ByVal CurrentRule As IDoOpenUrl, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()

        Me.CurrentRule = CurrentRule
        txtUrl.Text = CurrentRule.Url

        Select Case CurrentRule.OpenMode
            Case OpenType.Home
                Rbt_Home.Checked = True
            Case OpenType.Modal
                Rbt_Modal.Checked = True
            Case OpenType.NewTab
                Rbt_NewTab.Checked = True
        End Select
        HasBeenModified = False

    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        CurrentRule.Url = txtUrl.Text
        If Rbt_Home.Checked Then
            CurrentRule.OpenMode = OpenType.Home
        ElseIf Rbt_Modal.Checked Then
            CurrentRule.OpenMode = OpenType.Modal
        Else
            CurrentRule.OpenMode = OpenType.NewTab
        End If

        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Url)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.OpenMode)

        UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        lblCambios.Text = "Modificaciones aplicadas de manera exitosa"
        HasBeenModified = False
    End Sub

    Private Sub txtUrl_TextChanged(sender As Object, e As EventArgs) Handles txtUrl.TextChanged
        HasBeenModified = True
    End Sub

    Private Sub Rbt_NewTab_CheckedChanged(sender As Object, e As EventArgs) Handles Rbt_NewTab.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub Rbt_Modal_CheckedChanged(sender As Object, e As EventArgs) Handles Rbt_Modal.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub Rbt_Home_CheckedChanged(sender As Object, e As EventArgs) Handles Rbt_Home.CheckedChanged
        HasBeenModified = True
    End Sub
End Class
