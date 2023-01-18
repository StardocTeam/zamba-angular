Public Class UCDoIndexFile
    Inherits ZRuleControl

    Private m_currentRule As IDoIndexFile

    Public Sub New(ByRef DoIndexFile As IDoIndexFile, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoIndexFile, _wfPanelCircuit)
        InitializeComponent()
        m_currentRule = DoIndexFile
    End Sub



    Private Sub UCDoFillVar_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        txtDocTypeId.Text = m_currentRule.DocTypeId
        txtDocId.Text = m_currentRule.DocId
        txtDocumentPath.Text = m_currentRule.DocumentPath
        txtVarName.Text = m_currentRule.VarName
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAccept.Click
        Try
            If (String.IsNullOrEmpty(txtDocTypeId.Text) Or (String.IsNullOrEmpty(txtDocId.Text)) Or
               (String.IsNullOrEmpty(txtDocumentPath.Text)) Or (String.IsNullOrEmpty(txtVarName.Text))) Then
                MessageBox.Show("Hay campos sin completar", "Zamba Software", MessageBoxButtons.OK)
            Else
                m_currentRule.DocTypeId = txtDocTypeId.Text
                m_currentRule.DocId = txtDocId.Text
                m_currentRule.DocumentPath = txtDocumentPath.Text
                m_currentRule.VarName = txtVarName.Text

                WFRulesBusiness.UpdateParamItem(m_currentRule.ID, 0, m_currentRule.DocTypeId)
                WFRulesBusiness.UpdateParamItem(m_currentRule.ID, 1, m_currentRule.DocId)
                WFRulesBusiness.UpdateParamItem(m_currentRule.ID, 2, m_currentRule.DocumentPath)
                WFRulesBusiness.UpdateParamItem(m_currentRule.ID, 3, m_currentRule.VarName)

            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
