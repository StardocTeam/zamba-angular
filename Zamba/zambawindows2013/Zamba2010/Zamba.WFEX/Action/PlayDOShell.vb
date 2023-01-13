Imports Zamba.Core

Public Class PlayDOShell

    Private _myRule As IDOSHELL
    Private _filePath As String
    Private parameter As String

    Sub New(ByVal rule As IDOSHELL)
        Me._myRule = rule
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
            Trace.WriteLineIf(ZTrace.IsInfo, "Ruta Ejecutable: " & Me._myRule.Filepath)
            Trace.WriteLineIf(ZTrace.IsInfo, "Parametros: " & Me._myRule.Parameter)

            If results.Count > 0 Then
                For Each taskresult As TaskResult In results

                    Me._filePath = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Filepath, taskresult)

                    Me.parameter = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Parameter, taskresult)
                    Me._filePath = WFRuleParent.ReconocerVariables(Me._filePath.Trim())
                    parameter = WFRuleParent.ReconocerVariables(parameter.Trim())


                    If (String.IsNullOrEmpty(Me._filePath)) Then
                        Trace.WriteLineIf(ZTrace.IsInfo, "Sin Ruta")
                    Else
                        If (String.IsNullOrEmpty(parameter)) Then
                            Trace.WriteLineIf(ZTrace.IsInfo, "Shell: " & Me._filePath)

                            If Me._myRule.UseProcess = True Then
                                System.Diagnostics.Process.Start(Me._filePath.Trim())
                            Else
                                Shell(Me._filePath.Trim(), AppWinStyle.NormalFocus)
                            End If
                        Else
                            Trace.WriteLineIf(ZTrace.IsInfo, "Shell: " & Chr(34) & Me._filePath & Chr(34) & " " & parameter)
                            If Me._myRule.UseProcess = True Then
                                System.Diagnostics.Process.Start(Me._filePath.Trim(), parameter)
                            Else
                                Shell(Chr(34) & Me._filePath.Trim() & Chr(34) & " " & parameter, AppWinStyle.NormalFocus)
                            End If
                        End If
                    End If
                Next
            Else
                Me._filePath = WFRuleParent.ReconocerVariables(Me._myRule.Filepath)
                Me.parameter = WFRuleParent.ReconocerVariables(Me._myRule.Parameter)
                If (String.IsNullOrEmpty(Me._filePath.Trim())) Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "Sin Ruta")
                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "Ejecutando aplicación mediante línea de comandos.")
                    If (String.IsNullOrEmpty(parameter.Trim())) Then
                        Trace.WriteLineIf(ZTrace.IsInfo, "Sin Parametros")
                        Trace.WriteLineIf(ZTrace.IsInfo, "Shell: " & Me._filePath.Trim())
                        If Me._myRule.UseProcess = True Then
                            System.Diagnostics.Process.Start(Me._filePath.Trim())
                        Else
                            Shell(Me._filePath.Trim(), AppWinStyle.NormalFocus)
                        End If
                    Else
                        Trace.WriteLineIf(ZTrace.IsInfo, "Con parametros")
                        Trace.WriteLineIf(ZTrace.IsInfo, "Shell: " & Chr(34) & Me._filePath.Trim() & Chr(34) & " " & parameter.Trim())
                        If Me._myRule.UseProcess = True Then
                            System.Diagnostics.Process.Start(Me._filePath.Trim(), parameter.Trim())
                        Else
                            Shell(Chr(34) & Me._filePath.Trim() & Chr(34) & " " & parameter.Trim(), AppWinStyle.NormalFocus)
                        End If
                    End If
                End If
            End If
        Finally

            Me._filePath = Nothing
            Me.parameter = Nothing
        End Try

        Return (results)
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class