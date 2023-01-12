Imports Zamba.Data

Public Class UCDoReadInboxMail
    Inherits Zamba.WFUserControl.ZRuleControl

    Public Sub New(ByRef _CurrentRule As IDoReadInboxMail, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(_CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        this_Load()
    End Sub
#Region "Metodos Locales"
    Private Sub this_Load()
        Try
            txtPop3Server.Text = MyRule.Pop3Server
            txtPop3Port.Text = MyRule.Pop3Port
            txtPop3User.Text = MyRule.Pop3User
            txtPop3Password.Text = MyRule.Pop3Password
            chkEnableSSL.Checked = MyRule.Pop3EnableSSL
            txtEndDate.Text = MyRule.EndDate
            txtPathToExport.Text = MyRule.PathToExport
            txtStartDate.Text = MyRule.StartDate
            txtZvarName.Text = MyRule.Zvarname
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

    Public Shadows ReadOnly Property MyRule() As IDoReadInboxMail
        Get
            Return DirectCast(Rule, IDoReadInboxMail)
        End Get
    End Property

    Private Sub Btn_Save_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Btn_Save.Click
        If String.IsNullOrEmpty(txtPop3Server.Text) OrElse _
            String.IsNullOrEmpty(txtPop3Port.Text) OrElse _
            String.IsNullOrEmpty(txtPop3User.Text) OrElse _
            String.IsNullOrEmpty(txtPop3Password.Text) OrElse _
            String.IsNullOrEmpty(txtEndDate.Text) OrElse _
            String.IsNullOrEmpty(txtPathToExport.Text) OrElse _
            String.IsNullOrEmpty(txtStartDate.Text) OrElse _
            String.IsNullOrEmpty(txtZvarName.Text) Then
            MessageBox.Show("Todos los campos deben estar completos para continuar", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        MyRule.Pop3Server = txtPop3Server.Text
        MyRule.Pop3Port = txtPop3Port.Text
        MyRule.Pop3User = txtPop3User.Text
        MyRule.Pop3Password = txtPop3Password.Text
        MyRule.EndDate = txtEndDate.Text
        MyRule.PathToExport = txtPathToExport.Text
        MyRule.StartDate = txtStartDate.Text
        MyRule.Zvarname = txtZvarName.Text
        MyRule.Pop3EnableSSL = chkEnableSSL.Checked

        WFRulesBusiness.UpdateParamItem(MyRule, 0, MyRule.Pop3Server)
        WFRulesBusiness.UpdateParamItem(MyRule, 1, MyRule.Pop3Port)
        WFRulesBusiness.UpdateParamItem(MyRule, 2, MyRule.Pop3User)
        WFRulesBusiness.UpdateParamItem(MyRule, 3, MyRule.Pop3Password)
        WFRulesBusiness.UpdateParamItem(MyRule, 4, MyRule.Pop3EnableSSL)
        WFRulesBusiness.UpdateParamItem(MyRule, 5, MyRule.PathToExport)
        WFRulesBusiness.UpdateParamItem(MyRule, 6, MyRule.Zvarname)
        WFRulesBusiness.UpdateParamItem(MyRule, 7, MyRule.StartDate)
        WFRulesBusiness.UpdateParamItem(MyRule, 8, MyRule.EndDate)

        UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
    End Sub
End Class
