Public Class UCDoLoadDataSetFromXML
    Inherits ZRuleControl

    Dim CurrentRule As IDoLoadDataSetFromXML
    Public Sub New(ByRef DoLoadDataSet As IDoLoadDataSetFromXML, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoLoadDataSet, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = DoLoadDataSet

        tbEndTag.Text = CurrentRule.EndTag
        tbOpenTag.Text = CurrentRule.StartTag
        tbXML.Text = CurrentRule.XMLSource
        tbVariable.Text = CurrentRule.DataSetName
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Try
            If tbVariable.Text = String.Empty Then
                MessageBox.Show("Debe ingresar un nombre al conjunto de resultados", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            CurrentRule.StartTag = tbOpenTag.Text
            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.StartTag)

            CurrentRule.EndTag = tbEndTag.Text
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.EndTag)

            CurrentRule.XMLSource = tbXML.Text
            WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.XMLSource)

            CurrentRule.DataSetName = tbVariable.Text
            WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.DataSetName)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Error al salvar")
        End Try
    End Sub

End Class
