'Imports Zamba.WFBusiness


Public Class UCDoForEach
    Inherits ZRuleControl

#Region "Constructor"
    Dim CurrentRule As IDoForEach
    Public Sub New(ByRef DoForEach As IDoForEach, ByRef _wfPanelCircuit As IWFPanelCircuit)

        MyBase.New(DoForEach, _wfPanelCircuit)

        Try
            InitializeComponent()
            CurrentRule = DoForEach

            txtValue.Text = CurrentRule.Value

            lbldescription.Text = lbldescription.Text.Replace("Nombre de esta Regla", CurrentRule.Name)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

#Region "Metodos"
    Private Sub btnAcept_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAcept.Click

        CurrentRule.Value = txtValue.Text
        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Value)
        UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
    End Sub


#End Region
End Class
