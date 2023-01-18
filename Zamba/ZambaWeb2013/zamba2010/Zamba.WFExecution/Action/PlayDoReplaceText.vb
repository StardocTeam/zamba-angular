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
        Dim VarInterReglas As New VariablesInterReglas()
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Play de la DoReplaceText")


            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtener las variables")
            If results.Count > 0 Then
                For Each r As ITaskResult In results
                    Me.texttoreplace = String.Empty
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                    Me.texttoreplace = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Text, r)
                    Me.texttoreplace = VarInterReglas.ReconocerVariables(Me.texttoreplace).TrimEnd

                    If Me._myRule.IsFile Then
                        Using reader As New System.IO.StreamReader(Me.texttoreplace)
                            Me.texttoreplace = reader.ReadToEnd
                        End Using
                    End If

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto a reemplazar: " & Me.texttoreplace)
                    Me.replacetext = String.Empty
                    Me.replaceto = String.Empty
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de campos a reemplazar: " & Me._myRule.ReplaceFields.Split("§").Length)
                    For Each replacefield As String In Me._myRule.ReplaceFields.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)
                        Me.replacetext = replacefield.Split("¶")(0)
                        Me.replaceto = replacefield.Split("¶")(1)
                        Me.replacetext = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.replacetext, r)
                        If Me.replacetext = "  " Then
                            Me.replacetext = Convert.ToChar(32)
                        Else
                            Me.replacetext = VarInterReglas.ReconocerVariables(Me.replacetext)
                        End If

                        Me.replaceto = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.replaceto, r)
                        If VarInterReglas.ReconocerVariables(Me.replaceto) = " " Then
                            Me.replaceto = Convert.ToChar(32)
                        Else
                            Me.replaceto = VarInterReglas.ReconocerVariables(Me.replaceto)
                        End If
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reemplazando " & Chr(34) & Me.replacetext & Chr(34) & " por " & Chr(34) & Me.replaceto & Chr(34))

                        If Me.replacetext = "\r\n" And Me.texttoreplace.Contains(vbCrLf) Then
                            Me.texttoreplace = Me.texttoreplace.Replace(vbCrLf, Me.replaceto)
                        Else
                            If (Me.replacetext <> String.Empty) Then
                                Me.texttoreplace = Me.texttoreplace.Replace(Me.replacetext, Me.replaceto)
                            End If
                        End If
                    Next

                    Me.texttoreplace = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.texttoreplace, r)
                    Me.texttoreplace = VarInterReglas.ReconocerVariables(Me.texttoreplace)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Final: " & Me.texttoreplace)

                    If VariablesInterReglas.ContainsKey(Me._myRule.SaveTextAs) = False Then
                        VariablesInterReglas.Add(Me._myRule.SaveTextAs, Me.texttoreplace)
                    Else
                        VariablesInterReglas.Item(Me._myRule.SaveTextAs) = Me.texttoreplace
                    End If
                Next
            Else
                Me.texttoreplace = String.Empty
                Me.texttoreplace = VarInterReglas.ReconocerVariables(Me._myRule.Text).TrimEnd
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto a reemplazar: " & Me.texttoreplace)

                If Me._myRule.IsFile Then
                    Using reader As New System.IO.StreamReader(Me.texttoreplace)
                        Me.texttoreplace = reader.ReadToEnd
                    End Using
                End If

                Me.replacetext = String.Empty
                Me.replaceto = String.Empty
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de campos a reemplazar: " & Me._myRule.ReplaceFields.Split("§").Length)
                For Each replacefield As String In Me._myRule.ReplaceFields.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)
                    Me.replacetext = replacefield.Split("¶")(0)
                    Me.replaceto = replacefield.Split("¶")(1)
                    Me.replacetext = VarInterReglas.ReconocerVariables(Me.replacetext)
                    Me.replaceto = VarInterReglas.ReconocerVariables(Me.replaceto)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Reemplazando " & Chr(34) & Me.replacetext & Chr(34) & " por " & Chr(34) & Me.replaceto & Chr(34))
                    Me.texttoreplace = Me.texttoreplace.Replace(Me.replacetext, Me.replaceto)
                Next

                Me.texttoreplace = VarInterReglas.ReconocerVariables(Me.texttoreplace)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Final: " & Me.texttoreplace)
                If VariablesInterReglas.ContainsKey(Me._myRule.SaveTextAs) = False Then
                    VariablesInterReglas.Add(Me._myRule.SaveTextAs, Me.texttoreplace)
                Else
                    VariablesInterReglas.Item(Me._myRule.SaveTextAs) = Me.texttoreplace
                End If
            End If
        Finally
            VarInterReglas = Nothing
            Me.texttoreplace = Nothing
            Me.replacetext = Nothing
            Me.replaceto = Nothing
        End Try

        Return results
    End Function
End Class
