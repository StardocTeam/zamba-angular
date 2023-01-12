Public Class PlayDoReplaceText

    Private _myRule As IDoReplaceText
    Private texttoreplace As String
    Private replacetext As String
    Private replaceto As String

    Sub New(ByVal rule As IDoReplaceText)
        _myRule = rule
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
                texttoreplace = String.Empty

                For Each r As ITaskResult In results
                    texttoreplace = TextoInteligente.ReconocerCodigo(_myRule.Text, r)
                    texttoreplace = WFRuleParent.ReconocerVariables(texttoreplace).TrimEnd

                    If _myRule.IsFile Then
                        Using reader As New System.IO.StreamReader(texttoreplace)
                            texttoreplace = reader.ReadToEnd
                        End Using
                    End If
                    If texttoreplace Is Nothing Then texttoreplace = String.Empty

                    replacetext = String.Empty
                    replaceto = String.Empty

                    For Each replacefield As String In _myRule.ReplaceFields.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)
                        replacetext = replacefield.Split("¶")(0)
                        replaceto = replacefield.Split("¶")(1)
                        replacetext = TextoInteligente.ReconocerCodigo(replacetext, r)
                        If replacetext = "  " Then
                            replacetext = Convert.ToChar(32)
                        Else
                            replacetext = WFRuleParent.ReconocerVariables(replacetext)
                        End If

                        replaceto = TextoInteligente.ReconocerCodigo(replaceto, r)
                        If WFRuleParent.ReconocerVariables(replaceto) = " " Then
                            replaceto = Convert.ToChar(32)
                        Else
                            replaceto = WFRuleParent.ReconocerVariables(replaceto).TrimEnd
                        End If

                        If replacetext = "\r\n" And texttoreplace.Contains(vbCrLf) Then
                            texttoreplace = Microsoft.VisualBasic.Strings.Replace(texttoreplace, vbCrLf, replaceto,,, CompareMethod.Text)
                        Else
                            texttoreplace = Microsoft.VisualBasic.Strings.Replace(texttoreplace, replacetext, replaceto,,, CompareMethod.Text)
                        End If
                        If texttoreplace Is Nothing Then texttoreplace = String.Empty
                        replacetext = String.Empty
                    Next

                    If texttoreplace.Length > 0 Then

                        texttoreplace = TextoInteligente.ReconocerCodigo(texttoreplace, r).TrimEnd()
                        texttoreplace = WFRuleParent.ReconocerVariables(texttoreplace)

                    End If

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Final: " & texttoreplace)

                    If VariablesInterReglas.ContainsKey(_myRule.SaveTextAs) = False Then
                        VariablesInterReglas.Add(_myRule.SaveTextAs, texttoreplace, False)
                    Else
                        VariablesInterReglas.Item(_myRule.SaveTextAs) = texttoreplace
                    End If
                Next
            Else
                texttoreplace = String.Empty
                texttoreplace = WFRuleParent.ReconocerVariables(_myRule.Text).TrimEnd

                If _myRule.IsFile Then
                    Using reader As New System.IO.StreamReader(texttoreplace)
                        texttoreplace = reader.ReadToEnd
                    End Using
                End If

                replacetext = String.Empty
                replaceto = String.Empty
                For Each replacefield As String In _myRule.ReplaceFields.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)
                    replacetext = replacefield.Split("¶")(0)
                    replaceto = replacefield.Split("¶")(1)
                    replacetext = WFRuleParent.ReconocerVariables(replacetext)
                    replaceto = WFRuleParent.ReconocerVariables(replaceto)
                    texttoreplace = Microsoft.VisualBasic.Strings.Replace(texttoreplace, replacetext, replaceto,,, CompareMethod.Text)
                Next

                texttoreplace = WFRuleParent.ReconocerVariables(texttoreplace)

FinalValue:

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Final: " & texttoreplace)
                If VariablesInterReglas.ContainsKey(_myRule.SaveTextAs) = False Then
                    VariablesInterReglas.Add(_myRule.SaveTextAs, texttoreplace, False)
                Else
                    VariablesInterReglas.Item(_myRule.SaveTextAs) = texttoreplace
                End If
            End If
        Finally

            texttoreplace = Nothing
            replacetext = Nothing
            replaceto = Nothing
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
