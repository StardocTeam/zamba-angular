Public Class UCCategory
    Private Rule As IWFRuleParent
    Public Sub New(ByRef regla As IWFRuleParent)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        RemoveHandler cmbCategoria.SelectedIndexChanged, AddressOf cmbCategoria_SelectedIndexChanged
        If Not regla.Category Is Nothing Then
            cmbCategoria.SelectedIndex = regla.Category - 1
        Else
            cmbCategoria.SelectedIndex = 1
        End If
        AddHandler cmbCategoria.SelectedIndexChanged, AddressOf cmbCategoria_SelectedIndexChanged

        ' Add any initialization after the InitializeComponent() call.
        Rule = regla
    End Sub

    Private Sub cmbCategoria_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmbCategoria.SelectedIndexChanged
        Rule.Category = cmbCategoria.SelectedIndex + 1
        WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.RuleCategory, Rule.Category)
    End Sub
End Class
