Public Class UCDoWait
    Inherits ZRuleControl

#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDoWait
    Public Sub New(ByRef DoWait As IDoWait, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoWait, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = DoWait

        txtWaitTime.Text = CurrentRule.WaitTime
    End Sub
#End Region

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ZButton.Click
        Try
            CurrentRule.WaitTime = Convert.ToInt32(txtWaitTime.Text.Trim())
            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.WaitTime)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

End Class
