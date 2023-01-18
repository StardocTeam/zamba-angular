Public Class UCDoCloseTask
    Inherits ZRuleControl

    Private CurrentRule As IDoCloseTask

    Public Sub New(ByRef rule As IDoCloseTask, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = rule

        Try
            txtTaskId.Text = CurrentRule.TaskId
            If CurrentRule.ParentAction = 1 Then
                rdrefreshparent.CheckState = CheckState.Checked
            ElseIf CurrentRule.ParentAction = 2 Then
                rdcloseparenttoo.CheckState = CheckState.Checked
            ElseIf CurrentRule.ParentAction = 3 Then
                rdcloseparentonly.CheckState = CheckState.Checked
            ElseIf CurrentRule.ParentAction = 0 Then
                rdrefreshparent.CheckState = CheckState.Unchecked
                rdcloseparenttoo.CheckState = CheckState.Unchecked
                rdcloseparentonly.CheckState = CheckState.Unchecked
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnSaveValues_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSaveValues.Click
        Try
            CurrentRule.TaskId = txtTaskId.Text
            If Me.rdrefreshparent.CheckState = CheckState.Checked Then
                CurrentRule.ParentAction = 1
            ElseIf Me.rdcloseparenttoo.CheckState = CheckState.Checked Then
                CurrentRule.ParentAction = 2
            ElseIf Me.rdcloseparentonly.CheckState = CheckState.Checked Then
                CurrentRule.ParentAction = 3
            Else
                CurrentRule.ParentAction = 0
            End If

            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.TaskId)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.ParentAction)

            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
