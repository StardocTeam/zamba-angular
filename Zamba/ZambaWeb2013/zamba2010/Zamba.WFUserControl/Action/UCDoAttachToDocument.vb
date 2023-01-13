Public Class UCDoAttachToDocument
    Inherits ZRuleControl

#Region "Atributos"

    Private _currentRule As IDoAttachToDocument

#End Region

#Region "Constructor"

    Public Sub New(ByRef currentrule As IDoAttachToDocument, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(currentrule, _wfPanelCircuit)
        InitializeComponent()
        _currentRule = currentrule
        loadValues()
    End Sub

#End Region


#Region "Metodos Privados"

    ''' <summary>
    ''' Carga los valores a la interfaz
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub loadValues()
        txtLimit.Text = _currentRule.LimitKB
        txtSize.Text = _currentRule.CurrentSize
        chkLimitKB.Checked = _currentRule.WithLimit
        'cargo todos los entidades 
        Dim ds As DataSet = DocTypesBusiness.GetAllDocTypes()

        If (ds.Tables.Count > 0) Then
            cmbDocTypes.DataSource = ds.Tables(0)
            cmbDocTypes.DisplayMember = "doc_type_name"
            cmbDocTypes.ValueMember = "doc_type_id"
        End If

        If cmbDocTypes.Items.Count > 0 Then
            cmbDocTypes.SelectedValue = _currentRule.DocTypeId
        End If

        lblLimit.Enabled = chkLimitKB.Checked
        txtLimit.Enabled = chkLimitKB.Checked

    End Sub


#End Region

#Region "Metodos Publicos"

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        _currentRule.LimitKB = txtLimit.Text
        _currentRule.WithLimit = chkLimitKB.Checked
        _currentRule.DocTypeId = cmbDocTypes.SelectedValue
        _currentRule.CurrentSize = txtSize.Text

        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, _currentRule.DocTypeId)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 1, _currentRule.LimitKB)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 2, _currentRule.WithLimit)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 3, _currentRule.CurrentSize)
        UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
    End Sub

#End Region


    Private Sub chkLimitKB_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkLimitKB.CheckedChanged
        lblLimit.Enabled = chkLimitKB.Checked
        txtLimit.Enabled = chkLimitKB.Checked
        txtSize.Enabled = chkLimitKB.Checked
    End Sub

End Class
