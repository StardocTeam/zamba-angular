Imports Zamba.Core
Imports Zamba.AppBlock
Imports Zamba.Data

Public Class UCSelectMail

#Region "Atributos"

    Private TypeId As GroupToNotifyTypes
    Private GroupId As Int64
    Private notifyBy As MailTypes
    Private showAcceptCancelButtons As Boolean

#End Region

#Region "Constructores"

    Public Sub New(ByVal ruleId As Int64, ByVal _NotifyBy As NotifyTypes, ByVal _showAcceptCancelButtons As Boolean)

        InitializeComponent()

        Me.TypeId = ruleId
        Me.GroupId = ruleId
        Me.notifyBy = _NotifyBy
        Me.showAcceptCancelButtons = _showAcceptCancelButtons

        loadparameters()

    End Sub

#End Region

#Region "Eventos"

    Private Sub btnPara_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPara.Click, _
     btnCC.Click, btnCCO.Click
        Dim frmSelectMailUser As New frmSelectMailUser(Me.TypeId, Me.GroupId, Me.notifyBy, Me.showAcceptCancelButtons)
        frmSelectMailUser.ShowDialog()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        saveParameters()
    End Sub

#End Region

#Region "Metodos"

    ' Se guardan los datos en la base de datos
    Public Sub saveParameters()

        WFBusiness.SetRulesPreferences(Me.GroupId, RuleSectionOptions.Alerta, RulePreferences.AlertMailFor, 0, txtFor.Text)
        WFBusiness.SetRulesPreferences(Me.GroupId, RuleSectionOptions.Alerta, RulePreferences.AlertMailCC, 0, txtCC.Text)
        WFBusiness.SetRulesPreferences(Me.GroupId, RuleSectionOptions.Alerta, RulePreferences.AlertMailCCO, 0, txtCCO.Text)
        WFBusiness.SetRulesPreferences(Me.GroupId, RuleSectionOptions.Alerta, RulePreferences.AlertMailSubject, 0, txtSubject.Text)

        If (chkAttachDocument.Checked = True) Then
            WFBusiness.SetRulesPreferences(Me.GroupId, RuleSectionOptions.Alerta, RulePreferences.AlertMailAttachDocument, 1)
        Else
            WFBusiness.SetRulesPreferences(Me.GroupId, RuleSectionOptions.Alerta, RulePreferences.AlertMailAttachDocument, 0)
        End If

        WFBusiness.SetRulesPreferences(Me.GroupId, RuleSectionOptions.Alerta, RulePreferences.AlertMailBody, 0, txtBody.Text)

    End Sub

    ' Se recuperan los datos de la base de datos
    Private Sub loadparameters()

        Dim dataset As DataSet = WFBusiness.GetRulesPreferences(Me.GroupId, RuleSectionOptions.Alerta, RulePreferences.AlertMailFor)

        If (dataset.Tables(0).Rows.Count > 0) Then

            txtFor.Text = dataset.Tables(0).Rows(0).Item(0).ToString()

            dataset = WFBusiness.GetRulesPreferences(Me.GroupId, RuleSectionOptions.Alerta, RulePreferences.AlertMailCC)
            txtCC.Text = dataset.Tables(0).Rows(0).Item(0).ToString()

            dataset = WFBusiness.GetRulesPreferences(Me.GroupId, RuleSectionOptions.Alerta, RulePreferences.AlertMailCCO)
            txtCCO.Text = dataset.Tables(0).Rows(0).Item(0).ToString()

            dataset = WFBusiness.GetRulesPreferences(Me.GroupId, RuleSectionOptions.Alerta, RulePreferences.AlertMailSubject)
            txtSubject.Text = dataset.Tables(0).Rows(0).Item(0).ToString()

            dataset = WFBusiness.GetRulesPreferences(Me.GroupId, RuleSectionOptions.Alerta, RulePreferences.AlertMailAttachDocument)

            If (dataset.Tables(0).Rows(0).Item(0).ToString() = "1") Then
                chkAttachDocument.Checked = True
            Else
                chkAttachDocument.Checked = False
            End If

            dataset = WFBusiness.GetRulesPreferences(Me.GroupId, RuleSectionOptions.Alerta, RulePreferences.AlertMailBody)
            txtBody.Text = dataset.Tables(0).Rows(0).Item(0).ToString()

        End If

    End Sub

#End Region

End Class