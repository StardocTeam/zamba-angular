Public Class UCDOGetTask
    Inherits ZRuleControl

    Public Sub New(ByRef rule As IDoGetTask, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
    End Sub

#Region "Load"

    Private Sub UCDoGetTask_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load
        Try
            txtVarTaskId.Text = MyRule.TaskIdVariable
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

    Public Shadows ReadOnly Property MyRule() As IDoGetTask
        Get
            Return DirectCast(Rule, IDoGetTask)
        End Get
    End Property


    Private Sub btnsaverule_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnsaverule.Click
        Try
            MyRule.TaskIdVariable = txtVarTaskId.Text

            WFRulesBusiness.UpdateParamItem(MyRule.ID, 0, MyRule.TaskIdVariable)
            UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
End Class