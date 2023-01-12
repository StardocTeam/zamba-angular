Imports Zamba.Core

Public Class PlayDoReplaceText

    Private _myRule As IDoReplaceText
    Private texttoreplace As String
    Private replacetext As String
    Private replaceto As String

    Sub New(ByVal rule As IDoReplaceText)
        Me._myRule = rule
    End Sub

    ''' <summary>
    ''' Play de la Regla DoReplaceText
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  Modified    20/12/2010  Se agrega reconocimiento de texto inteligente y zvar dentro del texto introducido
    ''' </history>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            If results.Count > 0 Then
                For Each r As ITaskResult In results
                    Me.texttoreplace = String.Empty
                    Me.texttoreplace = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Text, r)
                    Me.texttoreplace = WFRuleParent.ReconocerVariables(Me.texttoreplace).TrimEnd

                    If Me._myRule.IsFile Then
                        Using reader As New System.IO.StreamReader(Me.texttoreplace)
                            Me.texttoreplace = reader.ReadToEnd
                        End Using
                    End If


                    Me.replacetext = String.Empty
                    Me.replaceto = String.Empty

                    For Each replacefield As String In Me._myRule.ReplaceFields.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)
                        Me.replacetext = replacefield.Split("¶")(0)
                        Me.replaceto = replacefield.Split("¶")(1)
                        Me.replacetext = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.replacetext, r)
                        If Me.replacetext = "  " Then
                            Me.replacetext = Convert.ToChar(32)
                        Else
                            Me.replacetext = WFRuleParent.ReconocerVariables(Me.replacetext).TrimEnd
                        End If

                        Me.replaceto = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.replaceto, r)
                        If WFRuleParent.ReconocerVariables(Me.replaceto) = " " Then
                            Me.replaceto = Convert.ToChar(32)
                        Else
                            Me.replaceto = WFRuleParent.ReconocerVariables(Me.replaceto).TrimEnd
                        End If

                        If Me.replacetext = "\r\n" And Me.texttoreplace.Contains(vbCrLf) Then
                            Me.texttoreplace = Me.texttoreplace.Replace(vbCrLf, Me.replaceto)
                        Else
                            Me.texttoreplace = Me.texttoreplace.Replace(Me.replacetext, Me.replaceto)
                        End If
                    Next

                    Me.texttoreplace = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.texttoreplace, r).TrimEnd()
                    Me.texttoreplace = WFRuleParent.ReconocerVariables(Me.texttoreplace)

                    Trace.WriteLineIf(ZTrace.IsInfo, "Texto Final: " & Me.texttoreplace)

                    If VariablesInterReglas.ContainsKey(Me._myRule.SaveTextAs) = False Then
                        VariablesInterReglas.Add(Me._myRule.SaveTextAs, Me.texttoreplace, False)
                    Else
                        VariablesInterReglas.Item(Me._myRule.SaveTextAs) = Me.texttoreplace
                    End If
                Next
            Else
                Me.texttoreplace = String.Empty
                Me.texttoreplace = WFRuleParent.ReconocerVariables(Me._myRule.Text).TrimEnd

                If Me._myRule.IsFile Then
                    Using reader As New System.IO.StreamReader(Me.texttoreplace)
                        Me.texttoreplace = reader.ReadToEnd
                    End Using
                End If

                Me.replacetext = String.Empty
                Me.replaceto = String.Empty
                For Each replacefield As String In Me._myRule.ReplaceFields.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)
                    Me.replacetext = replacefield.Split("¶")(0)
                    Me.replaceto = replacefield.Split("¶")(1)
                    Me.replacetext = WFRuleParent.ReconocerVariables(Me.replacetext)
                    Me.replaceto = WFRuleParent.ReconocerVariables(Me.replaceto)
                    Me.texttoreplace = Me.texttoreplace.Replace(Me.replacetext, Me.replaceto)
                Next

                Me.texttoreplace = WFRuleParent.ReconocerVariables(Me.texttoreplace)

                Trace.WriteLineIf(ZTrace.IsInfo, "Texto Final: " & Me.texttoreplace)
                If VariablesInterReglas.ContainsKey(Me._myRule.SaveTextAs) = False Then
                    VariablesInterReglas.Add(Me._myRule.SaveTextAs, Me.texttoreplace, False)
                Else
                    VariablesInterReglas.Item(Me._myRule.SaveTextAs) = Me.texttoreplace
                End If
            End If
        Finally

            Me.texttoreplace = Nothing
            Me.replacetext = Nothing
            Me.replaceto = Nothing
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
