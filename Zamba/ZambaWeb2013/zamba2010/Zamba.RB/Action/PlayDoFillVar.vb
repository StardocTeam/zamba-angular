''' -----------------------------------------------------------------------------
''' Project	 : Zamba.WFExecution
''' Class	 : PlayDoFillVar
''' -----------------------------------------------------------------------------
''' <summary>
''' Play de la regla DoFillVar
''' </summary>
''' <remarks>
''' Hereda WFRuleParent
''' </remarks>
''' <history>
''' 	[Gaston]	17/12/2008	Created
''' 	[Marcelo]	15/09/2009	Modified - Se agrega concatenacion
''' </history>
''' -----------------------------------------------------------------------------

Public Class PlayDoFillVar

    Private _myRule As IDoFillVar
    Private newMessage As String
    Private concValue As String
    Private _variableName As String

    Sub New(ByVal rule As IDoFillVar)
        Me._myRule = rule
    End Sub


    ''' <summary>
    ''' Play de la regla DOFillVar
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myrule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try


            For Each r As TaskResult In results
                   Me.newMessage = String.Empty
                newMessage = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.variableValue)
                newMessage = Zamba.Core.TextoInteligente.ReconocerCodigo(newMessage, r)

                Me._variableName = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.variableName)
                Me._variableName = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._variableName.TrimEnd, r).Trim

                 If Me._myRule.useConc = False Then

                    TextoInteligente.AsignItemFromSmartText(WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.variableName), r, newMessage)

                    If (VariablesInterReglas.ContainsKey(Me._variableName) = False) Then
                        VariablesInterReglas.Add(Me._variableName, newMessage, False)
                    Else
                         VariablesInterReglas.Item(Me._variableName) = newMessage
                    End If
                Else

                    If (VariablesInterReglas.ContainsKey(Me._variableName) = False) Then
                        VariablesInterReglas.Add(Me._variableName, newMessage, False)
                    Else
                         concValue = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.concValue)
                        concValue = TextoInteligente.ReconocerCodigo(concValue, r)

                        VariablesInterReglas.Item(Me._variableName) = VariablesInterReglas.Item(Me._variableName).ToString() & Me._myRule.concValue & newMessage
                    End If
                    If String.IsNullOrEmpty(Me._variableName) Then
                        TextoInteligente.AsignItemFromSmartText(WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.variableName), r, newMessage)
                    Else
                        TextoInteligente.AsignItemFromSmartText(WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.variableName), r, Me._variableName & Me._myRule.concValue & newMessage)
                    End If
                End If
                Trace.WriteLineIf(ZTrace.IsInfo, "Datos procesados con éxito!")
            Next
        Finally

            newMessage = Nothing
            concValue = Nothing
        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class