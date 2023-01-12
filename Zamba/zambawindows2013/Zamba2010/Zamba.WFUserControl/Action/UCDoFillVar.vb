''' -----------------------------------------------------------------------------
''' Project	 : Zamba.WFUserControl
''' Class	 : UCDoFillVar
''' -----------------------------------------------------------------------------
''' <summary>
''' Control de Usuario de la regla "DoFillVar" que permite configurar la regla
''' </summary>
''' <remarks>
''' Hereda WFRuleParent
''' </remarks>
''' <history>
''' 	[Gaston]	16/12/2008	Created
''' 	[Marcelo]	15/09/2009	Modified - Se agrega concatenacion
''' </history>
''' -----------------------------------------------------------------------------

Public Class UCDoFillVar
    Inherits ZRuleControl

    Private m_currentRule As IDoFillVar

    Public Sub New(ByRef DoFillVar As IDoFillVar, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoFillVar, _wfPanelCircuit)
        InitializeComponent()
        m_currentRule = DoFillVar
    End Sub

    Private Sub UCDoFillVar_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        txtVariableName.Text = m_currentRule.variableName
        txtVariableValue.Text = m_currentRule.variableValue
        chkConc.Checked = m_currentRule.useConc
        txtConc.Text = m_currentRule.concValue
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAccept.Click
        Try
            If String.IsNullOrEmpty(txtVariableValue.Text) Then
                txtVariableValue.Text = String.Empty
            End If

            If String.IsNullOrEmpty(txtVariableName.Text) Then
                MessageBox.Show("El nombre de la variable no pueden esta vacío", "Zamba Software", MessageBoxButtons.OK)
            Else
                m_currentRule.variableName = txtVariableName.Text
                m_currentRule.variableValue = txtVariableValue.Text
                m_currentRule.useConc = chkConc.Checked
                m_currentRule.concValue = txtConc.Text

                WFRulesBusiness.UpdateParamItem(m_currentRule.ID, 0, m_currentRule.variableName)
                WFRulesBusiness.UpdateParamItem(m_currentRule.ID, 1, m_currentRule.variableValue)
                WFRulesBusiness.UpdateParamItem(m_currentRule.ID, 2, m_currentRule.useConc)
                WFRulesBusiness.UpdateParamItem(m_currentRule.ID, 3, m_currentRule.concValue)
                UserBusiness.Rights.SaveAction(m_currentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & m_currentRule.Name & "(" & m_currentRule.ID & ")")
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
End Class