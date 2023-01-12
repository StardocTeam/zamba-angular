Public Class UCDoExportHTMLToPDF
    Inherits ZRuleControl

    Dim CurrentRule As IDoExportHTMLToPDF

    Public Sub New(ByRef DoExportHTMLToPDF As IDoExportHTMLToPDF, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoExportHTMLToPDF, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = DoExportHTMLToPDF
        tbToExportContent.Text = CurrentRule.Content
        tbReturnFileName.Text = CurrentRule.ReturnFileName
        cbEditable.Checked = CurrentRule.CanEditable
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Try
            If String.IsNullOrEmpty(tbToExportContent.Text) Then
                MessageBox.Show("Debe ingresar un contenido a exportar", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            CurrentRule.Content = tbToExportContent.Text
            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Content)

            CurrentRule.CanEditable = cbEditable.Checked
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.CanEditable)

            CurrentRule.ReturnFileName = tbReturnFileName.Text
            WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.ReturnFileName)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Error al salvar")
        End Try
    End Sub

End Class
