Public Class UCDoDesign
    Inherits ZRuleControl

    Private CurrentRule As IDoDesign

    Public Sub New(ByRef DoDesign As IDoDesign, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoDesign, _wfPanelCircuit)
        InitializeComponent()
        Try
            CurrentRule = DoDesign
            txtHelp.Text = CurrentRule.Help
            RemoveHandler updatePanelCircuit, AddressOf _wfPanelCircuit.UpdateRuleType
            AddHandler updatePanelCircuit, AddressOf _wfPanelCircuit.UpdateRuleType
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
    Private Event updatePanelCircuit(ByVal RuleId As Int64)
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            If txtHelp.TextLength > 4000 Then
                MessageBox.Show("El texto de diseño no puede superar los 4000 caracteres", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                CurrentRule.Help = txtHelp.Text
                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Help)
                UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub btnConvertir_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnConvertir.Click
        Dim frmRule As New UCRules(False)

        frmRule.ShowDialog()
        Dim className As String = frmRule.ClassName()

        If Not String.IsNullOrEmpty(className) Then
            WFRulesBusiness.updateClass(Rule.ID, className)

            WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.RuleHelp, 0, txtHelp.Text)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "La regla " & Rule.Name & "(" & Rule.ID & ") de tipo DoDesign ha sido convertida a la clase " & className)
            Rule.Description = txtHelp.Text
            RaiseEvent updatePanelCircuit(Rule.ID)
        End If
    End Sub
End Class