Public Class PlayDoOpenUrl
    Private _myRule As IDoOpenUrl
    Private url As String
    Private openMode As OpenType

    Sub New(ByVal rule As IDoOpenUrl)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ruta Ejecutable: " & _myRule.Url)

            If results.Count > 0 Then
                For Each taskresult As TaskResult In results

                    Url = TextoInteligente.ReconocerCodigo(_myRule.Url, taskresult)
                    Url = WFRuleParent.ReconocerVariables(Url.Trim())


                    If (String.IsNullOrEmpty(Url)) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Sin Ruta")
                    Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Shell: " & Url)
                                System.Diagnostics.Process.Start(Url.Trim())
                    End If
                Next
            Else
                Url = WFRuleParent.ReconocerVariables(_myRule.Url)
                If (String.IsNullOrEmpty(Url.Trim())) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Sin Ruta")
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando aplicación mediante línea de comandos.")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Shell: " & Url.Trim())
                            System.Diagnostics.Process.Start(Url.Trim())
                End If
            End If
        Finally

            Url = Nothing
        End Try

        Return (results)
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
