Imports Zamba.Core

Public Class PlayDoImport


    Private myRule As IDoImport

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Try
            Trace.WriteLineIf(ZTrace.IsInfo, "Importando...")
            For Each r As Core.TaskResult In results
                For Each field As String In myRule.ListToParse.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)
                    Dim startindedx As Int32 = CInt(field.Split(",")(0))
                    Dim endindex As Int32 = CInt(field.Split(",")(1).Split("¶")(0))
                    Dim varaux As String = field.Split(",")(1).Split("¶")(1)

                    Trace.WriteLineIf(ZTrace.IsVerbose, startindedx.ToString() & "," & endindex.ToString() & ":" & varaux)

                    Dim textaux As String = myRule.TextToParse
                    textaux = Zamba.Core.TextoInteligente.ReconocerCodigo(myRule.TextToParse, r)
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
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Variable Creada")
                        Trace.WriteLineIf(ZTrace.IsVerbose, textparsed)
                    Else
                        VariablesInterReglas.Item(varaux) = textparsed
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Variable Guardada")
                        Trace.WriteLineIf(ZTrace.IsVerbose, textparsed)
                    End If
                    Trace.WriteLineIf(ZTrace.IsWarning, startindedx.ToString & "," & endindex.ToString & ":" & varaux & "=" & textparsed)
                    Trace.WriteLineIf(ZTrace.IsWarning, "_______________________________________________________________________________")
                Next
            Next
            Trace.WriteLineIf(ZTrace.IsInfo, "Importación realizada con éxito!")
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
        Finally


        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
