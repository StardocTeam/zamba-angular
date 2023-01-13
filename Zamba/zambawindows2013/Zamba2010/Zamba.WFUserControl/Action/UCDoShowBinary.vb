Public Class UCDoShowBinary
    Inherits ZRuleControl

    Private CurrentRule As IDoShowBinary

    Public Sub New(ByRef rule As IDoShowBinary, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()

        Try
            CurrentRule = rule

            'Se cargan los controles
            txtBinary.Text = CurrentRule.BinaryFile

            'Se carga el combo de MIMEs
            cmbMime.Items.Add("application/pdf")
            If Not String.IsNullOrEmpty(CurrentRule.MimeType) AndAlso Not cmbMime.Items.Contains(CurrentRule.MimeType) Then
                cmbMime.Items.Add(CurrentRule.MimeType)
            End If
            cmbMime.Text = CurrentRule.MimeType

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            'Se guardan los cambios
            CurrentRule.BinaryFile = txtBinary.Text
            CurrentRule.MimeType = cmbMime.Text
            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.BinaryFile)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.MimeType)

            'Se guarda el historial
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")

            lblOk.Text = "Las modificaciones se han guardado correctamente"
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

End Class