Public Class UCDoInsertDocToBlob
    Inherits ZRuleControl

    Private Property CurrentRule As IDoInsertDocToBlob
    Public Sub New(ByRef DoInsert As IDoInsertDocToBlob, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoInsert, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = DoInsert
        LoadControls()
    End Sub

    Private Sub ZButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSaveConfig.Click
        Try
            If String.IsNullOrEmpty(txtDocId.Text) OrElse String.IsNullOrEmpty(txtDocId.Text) Then
                MessageBox.Show("Debe completar tanto el id del documento como el id de la entidad", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            CurrentRule.DocID = txtDocId.Text.Trim
            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.DocID)

            CurrentRule.DocTypeID = txtDocType.Text
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.DocTypeID)

            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub LoadControls()
        txtDocId.Text = CurrentRule.DocID
        txtDocType.Text = CurrentRule.DocTypeID
    End Sub

End Class
