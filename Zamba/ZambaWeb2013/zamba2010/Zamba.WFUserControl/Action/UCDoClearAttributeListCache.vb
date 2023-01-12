Public Class UCDoClearAttributeListCache
    Inherits ZRuleControl

    Dim CurrentRule As IDoClearAttributeListCache

    Public Sub New(ByVal CurrentRule As IDoClearAttributeListCache, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        Dim dsIndexs As DataSet = IndexsBusiness.GetIndexByDropDownValue(IndexAdditionalType.AutoSustitución)
        dsIndexs.Merge(IndexsBusiness.GetIndexByDropDownValue(IndexAdditionalType.AutoSustituciónJerarquico))
        dsIndexs.Merge(IndexsBusiness.GetIndexByDropDownValue(IndexAdditionalType.DropDown))
        dsIndexs.Merge(IndexsBusiness.GetIndexByDropDownValue(IndexAdditionalType.DropDownJerarquico))

        cmbAttribute.DataSource = dsIndexs.Tables(0)
        cmbAttribute.DisplayMember = "INDEX_NAME"
        cmbAttribute.ValueMember = "INDEX_ID"

        Me.CurrentRule = CurrentRule
        cmbAttribute.SelectedValue = CurrentRule.AttributeId
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        If cmbAttribute.SelectedValue Is Nothing Then
            MessageBox.Show("Debe seleccionar un atributo", "Zamba Software")
            Return
        End If

        CurrentRule.AttributeId = cmbAttribute.SelectedValue
        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.AttributeId)
        UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        lblCambios.Text = "Cambios aplicados con éxito"
    End Sub

End Class
