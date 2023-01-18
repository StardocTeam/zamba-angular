Public Class PlayDOShell

    Private _myRule As IDOSHELL
    Private _filePath As String
    Private parameter As String

    Sub New(ByVal rule As IDOSHELL)
        _myRule = rule
    End Sub


    ''' <summary>
    ''' Play de la regla DoShell
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myRule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	31/10/2008	Modified    Se agregaron trim() para quitar espacios en blanco de más y una validación
    '''     [marcelo]	06/01/2009	Modified    Se agrego trace y se corrigieron errores de logica
    ''' </history>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ruta Ejecutable: " & _myRule.Filepath)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Parametros: " & _myRule.Parameter)

            If results.Count > 0 Then
                For Each taskresult As TaskResult In results

                    _filePath = TextoInteligente.ReconocerCodigo(_myRule.Filepath, taskresult)

                    parameter = TextoInteligente.ReconocerCodigo(_myRule.Parameter, taskresult)
                    _filePath = WFRuleParent.ReconocerVariables(_filePath.Trim())
                    parameter = WFRuleParent.ReconocerVariables(parameter.Trim())


                    If (String.IsNullOrEmpty(_filePath)) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Sin Ruta")
                    Else
                        If (String.IsNullOrEmpty(parameter)) Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Shell: " & _filePath)

                            If _myRule.UseProcess = True Then
                                System.Diagnostics.Process.Start(_filePath.Trim())
                            Else
                                Shell(_filePath.Trim(), AppWinStyle.NormalFocus)
                            End If
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Shell: " & Chr(34) & _filePath & Chr(34) & " " & parameter)
                            If _myRule.UseProcess = True Then
                                System.Diagnostics.Process.Start(_filePath.Trim(), parameter)
                            Else
                                Shell(Chr(34) & _filePath.Trim() & Chr(34) & " " & parameter, AppWinStyle.NormalFocus)
                            End If
                        End If
                    End If
                Next
            Else
                _filePath = WFRuleParent.ReconocerVariables(_myRule.Filepath)
                parameter = WFRuleParent.ReconocerVariables(_myRule.Parameter)
                If (String.IsNullOrEmpty(_filePath.Trim())) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Sin Ruta")
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando aplicación mediante línea de comandos.")
                    If (String.IsNullOrEmpty(parameter.Trim())) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Sin Parametros")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Shell: " & _filePath.Trim())
                        If _myRule.UseProcess = True Then
                            System.Diagnostics.Process.Start(_filePath.Trim())
                        Else
                            Shell(_filePath.Trim(), AppWinStyle.NormalFocus)
                        End If
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Con parametros")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Shell: " & Chr(34) & _filePath.Trim() & Chr(34) & " " & parameter.Trim())
                        If _myRule.UseProcess = True Then
                            System.Diagnostics.Process.Start(_filePath.Trim(), parameter.Trim())
                        Else
                            Shell(Chr(34) & _filePath.Trim() & Chr(34) & " " & parameter.Trim(), AppWinStyle.NormalFocus)
                        End If
                    End If
                End If
            End If
        Finally

            _filePath = Nothing
            parameter = Nothing
        End Try

        Return (results)
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class