Public Class UCDoOpenTask
    Inherits Zamba.WFUserControl.ZRuleControl
    Public Sub New(ByRef _CurrentRule As IDOOpenTask, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(_CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        this_Load()
    End Sub
#Region "Metodos Locales"
    Private Sub this_Load()
        Try
            txtTaskID.Text = MyRule.TaskID
            txtDocID.Text = MyRule.DocID
            ChkUseCurrentTask.Checked = MyRule.UseCurrentTask
            If MyRule.OpenMode = 0 Then
                rdnewtab.CheckState = CheckState.Checked
            ElseIf MyRule.OpenMode = 1 Then
                rdmodal.CheckState = CheckState.Checked
            ElseIf MyRule.OpenMode = 2 Then
                rdself.CheckState = CheckState.Checked
            ElseIf MyRule.OpenMode = 3 Then
                rdnewwindow.CheckState = CheckState.Checked
            Else
                rdnewtab.CheckState = CheckState.Checked
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

    Public Shadows ReadOnly Property MyRule() As IDOOpenTask
        Get
            Return DirectCast(Rule, IDOOpenTask)
        End Get
    End Property

    Private Sub ChkUseCurrentTask_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ChkUseCurrentTask.CheckedChanged
        If ChkUseCurrentTask.Checked Then
            MyRule.UseCurrentTask = True
            txtTaskID.Enabled = False
            txtDocID.Enabled = False
        Else
            MyRule.UseCurrentTask = False
            txtTaskID.Enabled = True
            txtDocID.Enabled = True
        End If
    End Sub

    Private Sub Btn_Save_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Btn_Save.Click
        If ChkUseCurrentTask.Checked Then
            txtTaskID.Text = String.Empty
            txtDocID.Text = String.Empty
            MyRule.TaskID = String.Empty
            MyRule.DocID = String.Empty
        ElseIf Not String.IsNullOrEmpty(txtTaskID.Text) AndAlso Not String.IsNullOrEmpty(txtDocID.Text) Then
            MessageBox.Show("El campo DOC_ID y TASK_ID no pueden estar completos al mismo tiempo." & vbCrLf & "Limpie uno y guarde para continuar.")
            Exit Sub
        End If
        MyRule.TaskID = txtTaskID.Text
        MyRule.DocID = txtDocID.Text
        MyRule.UseCurrentTask = ChkUseCurrentTask.Checked
        If rdnewtab.CheckState = CheckState.Checked Then
            MyRule.OpenMode = 0
        ElseIf rdnewwindow.CheckState = CheckState.Checked Then
            MyRule.OpenMode = 3
        ElseIf rdmodal.CheckState = CheckState.Checked Then
            MyRule.OpenMode = 1
        ElseIf rdself.CheckState = CheckState.Checked Then
            MyRule.OpenMode = 2
        Else
            MyRule.OpenMode = 0
        End If
        WFRulesBusiness.UpdateParamItem(MyRule, 0, MyRule.TaskID)
        WFRulesBusiness.UpdateParamItem(MyRule, 1, MyRule.DocID)
        WFRulesBusiness.UpdateParamItem(MyRule, 2, MyRule.UseCurrentTask)
        WFRulesBusiness.UpdateParamItem(MyRule, 3, MyRule.OpenMode)
        UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
    End Sub


    Private Sub txtTaskID_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTaskID.SelectionChanged
        txtDocID.Text = String.Empty
    End Sub
    Private Sub txtDocID_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtDocID.SelectionChanged
        txtTaskID.Text = String.Empty
    End Sub
End Class
