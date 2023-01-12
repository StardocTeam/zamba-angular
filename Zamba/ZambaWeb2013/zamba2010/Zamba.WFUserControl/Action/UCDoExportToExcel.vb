'Imports Zamba.WFBusiness

Public Class UCDoExportToExcel
    Inherits ZRuleControl

    Public Sub New(ByRef rule As IDoExportToExcel, ByRef _wfPanelCircuit As IWFPanelCircuit)

        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
        txtRuta.Text = MyRule.Ruta

    End Sub
    Private Sub cmdAplicar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdAplicar.Click
        If txtRuta.Text.Length > 0 Then

            ' se agrego esta linea de codigo para poder ver la configuracion de la regla sin tener que salir
            'y volver a entrar.[sebastian 12/01/2009]
            MyRule.Ruta = txtRuta.Text
            WFRulesBusiness.UpdateParamItem(MyRule, 0, txtRuta.Text)
            UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
        Else
            MessageBox.Show("Debe seleccionar un ruta de destino", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Public Shadows ReadOnly Property MyRule() As IDoExportToExcel
        Get
            Return DirectCast(Rule, IDoExportToExcel)
        End Get
    End Property

    Private Sub cmdExaminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdExaminar.Click

        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            txtRuta.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub
End Class
