Public Class UCDoDecodeFile
    Inherits ZRuleControl

#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDoDecodeFile
    Public Sub New(ByRef DoDecodeFile As IDoDecodeFile, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoDecodeFile, _wfPanelCircuit)
        Try
            CurrentRule = DoDecodeFile
            InitializeComponent()
            txtPath.Text = CurrentRule.path
            txtFileName.Text = CurrentRule.fname
            txtBinary.Text = CurrentRule.binary
            txtVar.Text = CurrentRule.varpath
            txtstart.Text = CurrentRule.textstart
            txtend.Text = CurrentRule.textend
            txtext.Text = CurrentRule.extfile
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
#End Region

    Private Sub btnguardar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnguardar.Click
        CurrentRule.path = txtPath.Text
        CurrentRule.fname = txtFileName.Text
        CurrentRule.binary = txtBinary.Text
        CurrentRule.varpath = txtVar.Text
        CurrentRule.textstart = txtstart.Text
        CurrentRule.textend = txtend.Text
        CurrentRule.extfile = txtext.Text

        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.binary)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.path)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.fname)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.varpath)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.textstart)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.textend)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 6, CurrentRule.extfile)
        UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
    End Sub
End Class
