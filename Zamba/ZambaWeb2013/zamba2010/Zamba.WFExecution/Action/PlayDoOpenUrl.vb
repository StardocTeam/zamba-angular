Public Class PlayDoOpenUrl
    Private _myRule As IDoOpenUrl
    Private url As String
    Private openMode As OpenType

    Sub New(ByVal rule As IDoOpenUrl)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return PlayWeb(results, Nothing)
    End Function

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByRef params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        url = _myRule.Url
        openMode = _myRule.OpenMode
        Try
            If url.Contains("zvar") Then
                Dim VarInterReglas As New VariablesInterReglas()
                url = VarInterReglas.ReconocerVariables(url)
                VarInterReglas = Nothing
            End If
            If Not IsNothing(results(0)) Then
                url = TextoInteligente.ReconocerCodigo(url, results(0))
            End If

            params.Add("url", url)

            'If openMode.Contains("zvar") Then
            '    Dim VarInterReglas As New VariablesInterReglas()
            '    openMode = VarInterReglas.ReconocerVariables(openMode)
            '    VarInterReglas = Nothing
            'End If
            'If Not IsNothing(results(1)) Then
            '    openMode = Zamba.Core.TextoInteligente.ReconocerCodigo(openMode, results(1))
            'End If
            params.Add("OpenMode", openMode)
        Finally
            url = Nothing
            openMode = Nothing
        End Try

        Return results
    End Function
End Class
