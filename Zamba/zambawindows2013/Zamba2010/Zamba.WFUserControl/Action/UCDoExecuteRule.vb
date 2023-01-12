Public Class UCDoExecuteRule
    Inherits Zamba.WFUserControl.ZRuleControl

    Private Event OpenMissedRule(ByVal workflowId As Int64, ByVal ruleId As Int64)
    Private Delegate Sub ChangeCursorDelegate(ByVal cur As Cursor)

    Private Sub ChangeCursor(ByVal cur As Cursor)
        Try
            Cursor = cur
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            MyRule.Mode = Not rbSelectRule.Checked
            If rbSelectRule.Checked Then
                If Not IsNothing(CBORules.SelectedValue) Then

                    MyRule.RuleID = Int64.Parse(CBORules.SelectedValue)
                    WFRulesBusiness.UpdateParamItem(MyRule, 0, Int64.Parse(CBORules.SelectedValue))
                    WFRulesBusiness.UpdateParamItem(MyRule, 2, MyRule.RuleID)

                    MyRule.Name = "Ejecutar: " & WFRulesBusiness.GetRuleNameById(MyRule.RuleID)
                    RaiseUpdateMaskName()
                End If
            Else
                MyRule.IDRule = txtIDRule.Text
                WFRulesBusiness.UpdateParamItem(MyRule, 0, Int64.Parse(MyRule.IDRule))
                WFRulesBusiness.UpdateParamItem(MyRule, 2, MyRule.IDRule)

                MyRule.RuleID = Int64.Parse(txtIDRule.Text)
                MyRule.Name = "Ejecutar: " & WFRulesBusiness.GetRuleNameById(MyRule.RuleID)
                RaiseUpdateMaskName()
            End If

            WFRulesBusiness.UpdateParamItem(MyRule, 3, MyRule.Mode)
            WFRulesBusiness.UpdateParamItem(MyRule, 1, False)

            lblSaveMessage.Text = "Modificaciones aplicadas de manera exitosa"
            lblSaveMessage.ForeColor = Color.FromArgb(76, 76, 76)
            UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & " (" & MyRule.ID & ")")

        Catch ex As Exception
            ZClass.raiseerror(ex)
            lblSaveMessage.Text = "Error al aplicar las modificaciones"
            lblSaveMessage.ForeColor = Color.Red
        End Try
    End Sub

    Public Sub New(ByRef DoExecuteRule As IDOExecuteRule, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoExecuteRule, _wfPanelCircuit)
        InitializeComponent()
        this_Load()

        Try
            RemoveHandler OpenMissedRule, AddressOf _wfPanelCircuit.OpenMissedRule
            AddHandler OpenMissedRule, AddressOf _wfPanelCircuit.OpenMissedRule
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


#Region "Metodos Locales"
    Private Sub this_Load()
        Try
            FillRules()
            If MyRule.IDRule.ToLower() = "false" Then
                MyRule.IDRule = String.Empty
            End If
            txtIDRule.Text = MyRule.IDRule
            rbSelectRule.Checked = Not MyRule.Mode
            rbIDRule.Checked = MyRule.Mode
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub FillRules()
        Try
            chkRemote.Enabled = True
            Dim dt As DataTable = WFRulesBusiness.GetDoExecuteRules().Tables(0)
            CBORules.DataSource = dt
            CBORules.DisplayMember = dt.Columns(0).ColumnName '  "NAME"
            CBORules.ValueMember = dt.Columns(1).ColumnName    '"ID"
            CBORules.SelectedValue = MyRule.RuleID

            ''btnGoRule.Top = (CBORules.Bottom + 5)
            ''btnGoRule.Left = CBORules.Left
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

    Private Sub btnGoRule_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnGoRule.Click
        Dim wfbe As New WFBusinessExt
        Try
            Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.WaitCursor})
            Dim ruleId As Int64 = Int64.Parse(MyRule.IDRule)
            RaiseEvent OpenMissedRule(wfbe.GetWorkflowIdByRule(ruleId), ruleId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            wfbe = Nothing
            Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.Default})
        End Try
    End Sub

    Public Shadows ReadOnly Property MyRule() As IDOExecuteRule
        Get
            Return DirectCast(Rule, IDOExecuteRule)
        End Get
    End Property

    Private Sub rbSelectRule_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbSelectRule.CheckedChanged
        txtIDRule.Enabled = False
        CBORules.Enabled = True
    End Sub

    Private Sub rbIDRule_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbIDRule.CheckedChanged
        CBORules.Enabled = False
        txtIDRule.Enabled = True
    End Sub

    Private Sub txtIDRule_TextChanged(sender As Object, e As EventArgs) Handles txtIDRule.TextChanged
        Try
            MyRule.IDRule = txtIDRule.Text
            CBORules.SelectedValue = MyRule.IDRule
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CBORules_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBORules.SelectedIndexChanged
        Try
            MyRule.IDRule = CBORules.SelectedValue
        Catch ex As Exception

        End Try
    End Sub
End Class
