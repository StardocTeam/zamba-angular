Public Class UCDoAsk
    Inherits ZRuleControl

    Dim CurrentRule As IDOAsk
    ''' <summary>
    ''' se modifico el constructor para que se pueda cargar los valores de la regla en el administrador
    ''' </summary>
    ''' <param name="CurrentRule"></param>
    ''' <remarks>sebastian 12/12/2008</remarks>
    Public Sub New(ByVal CurrentRule As IDOAsk, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()

        Me.CurrentRule = CurrentRule
        txtMensaje.Text = CurrentRule.Mensaje
        txtVariable.Text = CurrentRule.Variable
        txtValorPorDefecto.Text = CurrentRule.ValorPorDefecto
        'Txtespacio.Text = CType(CurrentRule.tama�o, String)
        HasBeenModified = False

    End Sub


    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        CurrentRule.Mensaje = txtMensaje.Text
        CurrentRule.Variable = txtVariable.Text
        CurrentRule.ValorPorDefecto = txtValorPorDefecto.Text

        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Mensaje)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.Variable)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 2, 0)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.ValorPorDefecto)
        UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
        HasBeenModified = False
    End Sub

    Private Sub txtMensaje_TextChanged(sender As Object, e As EventArgs) Handles txtMensaje.TextChanged
        HasBeenModified = True
    End Sub

    Private Sub txtValorPorDefecto_TextChanged(sender As Object, e As EventArgs) Handles txtValorPorDefecto.TextChanged
        HasBeenModified = True
    End Sub

    Private Sub txtVariable_TextChanged(sender As Object, e As EventArgs) Handles txtVariable.TextChanged
        HasBeenModified = True
    End Sub

End Class