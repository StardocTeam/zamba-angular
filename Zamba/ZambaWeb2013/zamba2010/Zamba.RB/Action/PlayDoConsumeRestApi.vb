Imports System.Net
Imports System.Text
Imports System.IO

Public Class PlayDoConsumeRestApi
    Private myRule As IDoConsumeRestApi
    Private url As String
    Private oCookies As CookieCollection

    Sub New(ByVal rule As IDoConsumeRestApi)
        Me.myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        url = myRule.Url

        If url.Contains("zvar") Then
            Dim VarInterReglas As New VariablesInterReglas()
            url = VarInterReglas.ReconocerVariables(url)
            VarInterReglas = Nothing
        End If
        If results(0) IsNot Nothing Then
            url = Zamba.Core.TextoInteligente.ReconocerCodigo(url, results(0))
        End If
        Dim loHttp As HttpWebRequest = WebRequest.Create(url)
        loHttp.Timeout = 60000
        loHttp.UserAgent = "Code Sample Web Client"
        loHttp.CookieContainer = New CookieContainer()

        If (Not IsNothing(Me.oCookies)) AndAlso (Me.oCookies.Count > 0) Then
            loHttp.CookieContainer.Add(Me.oCookies)
        End If

        Dim loWebResponse As HttpWebResponse = loHttp.GetResponse()

        If (loWebResponse.Cookies.Count > 0) Then
            If (Me.oCookies) Is Nothing Then
                Me.oCookies = loWebResponse.Cookies
            End If
        Else
            For Each oRespCookie As Cookie In loWebResponse.Cookies
                Dim bMatch As Boolean = False
                For Each oReqCookie As Cookie In Me.oCookies
                    If (oReqCookie.Name = oRespCookie.Name) Then
                        oReqCookie.Value = oRespCookie.Name
                        bMatch = True
                        ' break()
                    End If
                Next
                If (bMatch = False) Then
                    Me.oCookies.Add(oRespCookie)
                End If

            Next
        End If
        Dim enc As Encoding = Encoding.GetEncoding(1252)

        If (loWebResponse.ContentEncoding.Length > 0) Then
            enc = Encoding.GetEncoding(loWebResponse.ContentEncoding)
        End If
        Dim loResponseStream As StreamReader = New StreamReader(loWebResponse.GetResponseStream(), enc)

        Dim VarValue As String = loResponseStream.ReadToEnd()

        If VariablesInterReglas.ContainsKey(myRule.ResultVar) = False Then
            VariablesInterReglas.Add(myRule.ResultVar, VarValue, False)
        Else
            VariablesInterReglas.Item(myRule.ResultVar) = VarValue
        End If

        Dim VB As New Zamba.Core.VarsBusiness
        VB.PersistVariable(myRule.ResultVar, results(0).TaskId, VarValue)
        VB = Nothing

        loResponseStream.Close()
        loWebResponse.Close()
        Trace.WriteLineIf(ZTrace.IsInfo, "Resultado " & VarValue)
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
