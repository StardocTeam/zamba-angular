Public Class PlayDoImport


    Private myRule As IDoImport
    Sub New(ByVal rule As IDoImport)
        myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Importando...")
            For Each r As Core.TaskResult In results
                For Each field As String In myRule.ListToParse.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)
                    Dim startindedx As Int32 = CInt(field.Split(",")(0))
                    Dim endindex As Int32 = CInt(field.Split(",")(1).Split("¶")(0))
                    Dim varaux As String = field.Split(",")(1).Split("¶")(1)

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, startindedx.ToString() & "," & endindex.ToString() & ":" & varaux)

                    Dim textaux As String = myRule.TextToParse
                    textaux = TextoInteligente.ReconocerCodigo(myRule.TextToParse, r)
                    textaux = WFRuleParent.ReconocerVariables(textaux).TrimEnd

                    Dim textparsed As String
                    If String.IsNullOrEmpty(endindex) OrElse endindex > textaux.Substring(startindedx).Length Then
                        textparsed = textaux.Substring(startindedx)
                    Else
                        textparsed = textaux.Substring(startindedx, endindex)
                    End If

                    TextoInteligente.AsignItemFromSmartText(varaux, r, textparsed)



                    If VariablesInterReglas.ContainsKey(varaux) = False Then
                        VariablesInterReglas.Add(varaux, textparsed, False)
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Variable Creada")
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, textparsed)
                    Else
                        VariablesInterReglas.Item(varaux) = textparsed
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Variable Guardada")
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, textparsed)
                    End If
                    ZTrace.WriteLineIf(ZTrace.IsWarning, startindedx.ToString & "," & endindex.ToString & ":" & varaux & "=" & textparsed)
                    ZTrace.WriteLineIf(ZTrace.IsWarning, "_______________________________________________________________________________")
                Next
            Next
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Importación realizada con éxito!")
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
        Finally


        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
