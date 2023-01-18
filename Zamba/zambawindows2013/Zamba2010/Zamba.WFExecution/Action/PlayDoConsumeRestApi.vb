Imports System.Net
Imports System.Text
Imports System.IO

Public Class PlayDoConsumeRestApi
    Private myRule As IDoConsumeRestApi
    Private url As String
    Private jsonMessage As String
    Private method As String
    Private oCookies As CookieCollection
    Private HelperRestaApi As Zamba.Core.RestApiHelper
    Sub New(ByVal rule As IDoConsumeRestApi)
        myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        url = myRule.Url
        method = myRule.Method
        jsonMessage = myRule.JsonMessage
        If url.Contains("zvar") Then
            Dim VarInterReglas As New VariablesInterReglas()
            url = VarInterReglas.ReconocerVariables(url)
            VarInterReglas = Nothing
        End If
        If jsonMessage.Contains("zvar") Then
            Dim VarInterReglas As New VariablesInterReglas()
            jsonMessage = VarInterReglas.ReconocerVariables(jsonMessage)
            VarInterReglas = Nothing
        End If
        If results(0) IsNot Nothing Then
            url = TextoInteligente.ReconocerCodigo(url, results(0))
            jsonMessage = TextoInteligente.ReconocerCodigo(jsonMessage, results(0))
        End If
        Dim VarValue As String = HelperRestaApi.ExecuteService(url, method, jsonMessage)
        If VariablesInterReglas.ContainsKey(myRule.ResultVar) = False Then
            VariablesInterReglas.Add(myRule.ResultVar, VarValue, False)
        Else
            VariablesInterReglas.Item(myRule.ResultVar) = VarValue
        End If
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado " & VarValue)
        Return results
    End Function

    Public Function PlayTest() As Boolean
        Try
            url = myRule.Url
            jsonMessage = myRule.JsonMessage
            method = myRule.Method
            Dim VarValue As String = HelperRestaApi.test(url, method, jsonMessage)
            MsgBox(VarValue)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Function DiscoverParams() As List(Of String)

    End Function
End Class
