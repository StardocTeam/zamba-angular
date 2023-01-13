Imports System.Net
Imports System.Text
Imports System.IO

Public Class PlayDoConsumeRestApi
    Private myRule As IDoConsumeRestApi
    Private url As String
    Private oCookies As CookieCollection

    Sub New(ByVal rule As IDoConsumeRestApi)
        myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim url As String
        Dim jsonMessage As String
        Dim method As String

        url = myRule.Url
        method = myRule.Method
        jsonMessage = myRule.JsonMessage

        If url.Contains("zvar") Then
            Dim VarInterReglas As New VariablesInterReglas()
            url = VarInterReglas.ReconocerVariables(url)
            VarInterReglas = Nothing
        End If

        If results(0) IsNot Nothing Then
            url = TextoInteligente.ReconocerCodigo(url, results(0))
        End If

        Dim HRA As New RestApiHelper
        Dim VarValue As String = HRA.ExecuteService(url, method, jsonMessage)

        If VariablesInterReglas.ContainsKey(myRule.ResultVar) = False Then
            VariablesInterReglas.Add(myRule.ResultVar, VarValue)
        Else
            VariablesInterReglas.Item(myRule.ResultVar) = VarValue
        End If
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado " & VarValue)


        Dim VB As New Zamba.Core.VarsBusiness
        VB.PersistVariable(myRule.ResultVar, results(0).TaskId, VarValue)
        VB = Nothing

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado " & VarValue)
        Return results
    End Function

    Public Function PlayTest() As Boolean
        Try
            Dim url As String
            Dim jsonMessage As String
            Dim method As String

            url = myRule.Url
            jsonMessage = myRule.JsonMessage
            method = myRule.Method

            If url.Contains("zvar") Then
                Dim VarInterReglas As New VariablesInterReglas()
                url = VarInterReglas.ReconocerVariables(url)
                VarInterReglas = Nothing
            End If

            'If Results(0) IsNot Nothing Then
            '    url = TextoInteligente.ReconocerCodigo(url, Results(0))
            'End If

            Dim HRA As New RestApiHelper
            Dim VarValue As String = HRA.test(url, method, jsonMessage)

            If VariablesInterReglas.ContainsKey(myRule.ResultVar) = False Then
                VariablesInterReglas.Add(myRule.ResultVar, VarValue)
            Else
                VariablesInterReglas.Item(myRule.ResultVar) = VarValue
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado " & VarValue)

            MsgBox(VarValue)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
