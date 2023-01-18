Imports System.IO

Public Class PlayDOCrearDocumento
    Private _myRule As IDOCrearDocumento

    Sub New(ByVal rule As IDOCrearDocumento)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDOCrearDocumento) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim sw As StreamWriter

        Try
            Dim test As String = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.text)
            Dim path As String = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.path)

            For Each r As Core.TaskResult In results
                NewList.Add(r)

                test = Zamba.Core.TextoInteligente.ReconocerCodigo(test, r)
                path = Zamba.Core.TextoInteligente.ReconocerCodigo(path, r)

                Trace.WriteLineIf(ZTrace.IsInfo, "Generando archivos")
                sw = New StreamWriter(path)

                sw.Write(test)

                sw.Close()
            Next
        Finally
            If Not IsNothing(Sw) Then
                sw.Dispose()
                sw = Nothing
            End If
        End Try
        Return NewList
    End Function
    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
