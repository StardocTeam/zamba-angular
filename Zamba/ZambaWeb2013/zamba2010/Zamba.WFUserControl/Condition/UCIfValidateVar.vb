
Public Class UCIfValidateVar
    Inherits ZRuleControl

    'Regla que se va a configurar
#Region "Propiedades"

    Public Shadows ReadOnly Property CurrentRule() As IIfValidateVar
        Get
            Return DirectCast(Rule, IIfValidateVar)
        End Get
    End Property

#End Region
    Private Shared sComparadores() As String = {"Igual", "Distinto", "Menor", "Mayor", "IgualMenor", "IgualMayor", "Contiene", "Empieza", "Termina"} '{"=", "<>", ">", "<", ">=", "<="}

    Public Sub New(ByRef IfValidateVar As IIfValidateVar, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(IfValidateVar, _wfPanelCircuit)

        InitializeComponent()

        If Not String.IsNullOrEmpty(CurrentRule.TxtVar) Then
            TxtVar.Text = CurrentRule.TxtVar
        End If
        If Not String.IsNullOrEmpty(CurrentRule.TxtValue) Then
            TxtValue.Text = CurrentRule.TxtValue
        End If
        If Not IsNothing(sComparadores) Then
            TxtOper.Items.AddRange(sComparadores)
        End If
        ChkCaseSensitive.Checked = CurrentRule.CaseInsensitive
        TxtOper.SelectedItem = CurrentRule.Operador.ToString
        'Select Case Me.CurrentRule.Operador
        '    Case Comparadores.Distinto
        '        Me.TxtOper.SelectedItem = Comparadores.Distinto.ToString
        '    Case Comparadores.Igual
        '        Me.TxtOper.SelectedItem = Comparadores.Igual.ToString
        '    Case Comparadores.IgualMayor
        '        Me.TxtOper.SelectedItem = Comparadores.IgualMayor.ToString
        '    Case Comparadores.IgualMenor
        '        Me.TxtOper.SelectedItem = Comparadores.IgualMenor.ToString
        '    Case Comparadores.Mayor
        '        Me.TxtOper.SelectedItem = Comparadores.Mayor.ToString
        '    Case Comparadores.Menor
        '        Me.TxtOper.SelectedItem = Comparadores.Menor.ToString
        'End Select
        HasBeenModified = False

    End Sub

    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSeleccionar.Click
        Try
            If Not TxtVar.Text = "" Or TxtOper.Text = "" Or TxtValue.Text = "" Then

                'Me.CurrentRule.Operador = Me.TxtOper.SelectedIndex
                Select Case TxtOper.SelectedIndex
                    Case 0
                        CurrentRule.Operador = Comparadores.Igual
                    Case 1
                        CurrentRule.Operador = Comparadores.Distinto
                    Case 2
                        CurrentRule.Operador = Comparadores.Menor
                    Case 3
                        CurrentRule.Operador = Comparadores.Mayor
                    Case 4
                        CurrentRule.Operador = Comparadores.IgualMenor
                    Case 5
                        CurrentRule.Operador = Comparadores.IgualMayor
                    Case 6
                        CurrentRule.Operador = Comparadores.Contiene
                    Case 7
                        CurrentRule.Operador = Comparadores.Empieza
                    Case 8
                        CurrentRule.Operador = Comparadores.Termina
                End Select

                CurrentRule.TxtValue = TxtValue.Text
                CurrentRule.TxtVar = TxtVar.Text
                CurrentRule.CaseInsensitive = ChkCaseSensitive.Checked

                WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 0, CurrentRule.TxtVar)
                WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 1, Int32.Parse(CurrentRule.Operador))
                WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 2, CurrentRule.TxtValue)
                WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 3, CurrentRule.CaseInsensitive)
                UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub UCIfValidateVar_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load

        'Select Case Me.CurrentRule.Operador
        '    Case Comparadores.Distinto
        '        Me.TxtOper.SelectedItem = Comparadores.Distinto.ToString
        '    Case Comparadores.Igual
        '        Me.TxtOper.SelectedItem = Comparadores.Igual.ToString
        '    Case Comparadores.IgualMayor
        '        Me.TxtOper.SelectedItem = Comparadores.IgualMayor.ToString
        '    Case Comparadores.IgualMenor
        '        Me.TxtOper.SelectedItem = Comparadores.IgualMenor.ToString
        '    Case Comparadores.Mayor
        '        Me.TxtOper.SelectedItem = Comparadores.Mayor.ToString
        '    Case Comparadores.Menor
        '        Me.TxtOper.SelectedItem = Comparadores.Menor.ToString
        'End Select
        HasBeenModified = False

    End Sub

    Private Sub TxtVar_TextChanged(sender As Object, e As EventArgs) Handles TxtVar.TextChanged
        HasBeenModified = True
    End Sub

    Private Sub TxtValue_TextChanged(sender As Object, e As EventArgs) Handles TxtValue.TextChanged
        HasBeenModified = True
    End Sub

    Private Sub ChkCaseSensitive_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCaseSensitive.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub tbctrMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbctrMain.SelectedIndexChanged

    End Sub
End Class
