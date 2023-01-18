Imports System.Windows.Forms
Public Class PlayDOAskDesition
    Private myRule As IDOAskDesition

    Sub New(ByVal rule As IDOAskDesition)
        Me.myRule = rule
    End Sub

    ''' <summary>
    ''' [Sebastián] 21-07-2009  Se agrego validación para en caso de que se haga clic en la cruz del formulario que hace
    ''' la pregunta al usuario, no continue con el resto de las reglas
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="Myrule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim Valor As String = String.Empty
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando el formulario de la regla...")
            'Dim formDesition As New FrmAskDesition()
            'formDesition.Text = Myrule.Name

            'Dim VarInterReglas As New VariablesInterReglas()
            'formDesition.txtQuestion.Text = VarInterReglas.ReconocerVariables(myRule.TXTAsk)
            'VarInterReglas = Nothing

            'If Not IsNothing(results) AndAlso results.Count > 0 Then
            '    formDesition.txtQuestion.Text = TextoInteligente.ReconocerCodigo(formDesition.txtQuestion.Text, results(0))
            'End If
            'ZTrace.WriteLineIf(ZTrace.IsInfo, "Mostrando el formulario.")
            'formDesition.ShowDialog()

            'Valor = formDesition.Ask_Desition
            'ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor obtenido del formulario: " & Valor)

            ''es NOTHING  cuando hacen clic en la cruz del formulario
            'If Not IsNothing(formDesition) Then
            '    formDesition.Close()
            '    formDesition.Dispose()
            '    formDesition = Nothing
            'End If

            If IsNothing(Valor) = False Then
                If VariablesInterReglas.ContainsKey(myRule.TXTVar) = False Then
                    VariablesInterReglas.Add(myRule.TXTVar, Valor)
                Else
                    VariablesInterReglas.Item(myRule.TXTVar) = Valor
                End If
                'se le pasa el results(0) tomando como que es un solo result
                TextoInteligente.AsignItemFromSmartText(myRule.TXTVar, results(0), Valor)
            ElseIf IsNothing(Valor) = True Then
                results = Nothing
            End If
        Catch ex As Exception
            ZCore.raiseerror(ex)

        Finally

        End Try
        Return results
    End Function

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim Valor As String = String.Empty
        Try
            Dim TextoInteligente As New TextoInteligente()
            Dim txtQuestionText As String

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando el formulario de la regla...")
            Params.Add("Text", myRule.Name)

            'Dim formDesition As New FrmAskDesition()
            'formDesition.Text = Myrule.Name

            Dim VarInterReglas As New VariablesInterReglas()
            txtQuestionText = VarInterReglas.ReconocerVariables(myRule.TXTAsk)
            VarInterReglas = Nothing

            If Not IsNothing(results) AndAlso results.Count > 0 Then
                txtQuestionText = TextoInteligente.ReconocerCodigo(txtQuestionText, results(0))
            End If

            Params.Add("txtQuestionText", txtQuestionText)
            Params.Add("TXTVar", myRule.TXTVar)

        Catch ex As Exception
            ZClass.raiseerror(ex)

        Finally

        End Try
        Return results
    End Function

    Public Function PlayWebSecondExecution(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        If Params.Contains("valor") Then
            Dim valor As String = Params("valor")
            Dim TextoInteligente As New TextoInteligente()

            If myRule.TXTVar.Contains("zvar") Then
                myRule.TXTVar = myRule.TXTVar.Replace("zvar(", "")
                myRule.TXTVar = myRule.TXTVar.Replace(")", "")
            End If

            If IsNothing(valor) = False Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Guardo Variable: {0} el valor: {1}", myRule.TXTVar, valor))
                If VariablesInterReglas.ContainsKey(myRule.TXTVar) = False Then
                    VariablesInterReglas.Add(myRule.TXTVar, valor)
                Else
                    VariablesInterReglas.Item(myRule.TXTVar) = valor
                End If
                'se le pasa el results(0) tomando como que es un solo result
                TextoInteligente.AsignItemFromSmartText(myRule.TXTVar, results(0), valor)
            ElseIf IsNothing(valor) = True Then
                results = Nothing
            End If
        End If

        Params.Clear()

        Return results
    End Function
End Class


