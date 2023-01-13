Imports System.Windows.Forms
Public Class PlayDOAskDesition

    ''' <summary>
    ''' [Sebastián] 21-07-2009  Se agrego validación para en caso de que se haga clic en la cruz del formulario que hace
    ''' la pregunta al usuario, no continue con el resto de las reglas
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="Myrule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Private myRule As IDOAskDesition

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim formDesition As FrmAskDesition = Nothing
        Dim Valor As String = String.Empty


        Try


            formDesition = New FrmAskDesition()
            formDesition.Text = myRule.Name

            formDesition.txtQuestion.Text = WFRuleParent.ReconocerVariables(myRule.TXTAsk)
            If Not IsNothing(results) AndAlso results.Count > 0 Then
                formDesition.txtQuestion.Text = TextoInteligente.ReconocerCodigo(formDesition.txtQuestion.Text, results(0))
            End If


            formDesition.ShowDialog()

            Valor = formDesition.Ask_Desition
            Trace.WriteLineIf(ZTrace.IsInfo, "Valor obtenido del formulario: " & Valor)

            'Verifica si el usuario hizo click en la cruz
            If formDesition.DialogResult = DialogResult.Cancel Then
                'Verifica si al cancelar debe tirar exception o cancelar la ejecucion sin errores
                If myRule.ThrowExceptionIfCancel Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                    Throw New Exception("El usuario cancelo la ejecucion de la regla")
                Else
                    results = Nothing
                End If
            Else
                If IsNothing(Valor) = False Then
                    If VariablesInterReglas.ContainsKey(myRule.TXTVar) = False Then
                        VariablesInterReglas.Add(myRule.TXTVar, Valor, False)
                    Else
                        VariablesInterReglas.Item(myRule.TXTVar) = Valor
                    End If
                    'se le pasa el results(0) tomando como que es un solo result
                    TextoInteligente.AsignItemFromSmartText(myRule.TXTVar, results(0), Valor)
                ElseIf IsNothing(Valor) = True Then
                    results = Nothing
                End If
            End If
        Finally
            If formDesition IsNot Nothing Then
                formDesition.Close()
                formDesition.Dispose()
                formDesition = Nothing
            End If
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDOAskDesition)
        Me.myRule = rule
    End Sub
End Class