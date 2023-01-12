Public Class UCDOAskDesition
    Inherits ZRuleControl

    'Regla que se va a configurar
    Dim CurrentRule As IDOAskDesition

    'El New debe recibir la regla a configurar
    Public Sub New(ByRef CurrentRule As IDOAskDesition, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        Me.CurrentRule = CurrentRule
        Load_Values()
    End Sub

#Region "Metodos Locales"
    Private Sub Load_Values()
        Try
            TxtAsk.Text = CurrentRule.TXTAsk
            TxtVar.Text = CurrentRule.TXTVar

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region
    Public Shadows ReadOnly Property MyRule() As IDOAskDesition
        Get
            Return DirectCast(Rule, IDOAskDesition)
        End Get
    End Property

    Friend WithEvents Label4 As ZLabel
    Friend WithEvents btnsave As ZButton


    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnsave.Click
        Try
            WFRulesBusiness.UpdateParamItem(CurrentRule, 1, TxtAsk.Text)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 0, TxtVar.Text)
            CurrentRule.TXTAsk = TxtAsk.Text
            CurrentRule.TXTVar = TxtVar.Text
            UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


End Class

