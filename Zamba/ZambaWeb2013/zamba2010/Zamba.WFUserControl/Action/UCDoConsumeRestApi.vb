Imports System.Net
Imports System.Text
Imports System.IO

Public Class UCDoConsumeRestApi
    Inherits ZRuleControl
    Dim CurrentRule As IDoConsumeRestApi
    Private oCookies As CookieCollection

    Public Sub New(ByVal CurrentRule As IDoConsumeRestApi, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()

        Me.CurrentRule = CurrentRule
        txtUrl.Text = CurrentRule.Url
        txtResult.Text = CurrentRule.ResultVar
        txtUrl.Multiline = True
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        CurrentRule.Url = txtUrl.Text
        CurrentRule.ResultVar = txtResult.Text

        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Url)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.ResultVar)

        UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        lblCambios.Text = "Cambios aplicados con éxito"
    End Sub

    Private Sub TextoInteligente1_TextChanged(sender As Object, e As EventArgs) Handles txtResult.TextChanged

    End Sub

    Private Sub lblAyudaUrl_Click(sender As Object, e As EventArgs) Handles lblAyudaUrl.Click

    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click

        Dim loHttp As HttpWebRequest = WebRequest.Create(txtUrl.Text)
        loHttp.Timeout = 10000
        loHttp.UserAgent = "Code Sample Web Client"
        loHttp.CookieContainer = New CookieContainer()

        If (Not IsNothing(oCookies)) AndAlso (oCookies.Count > 0) Then
            loHttp.CookieContainer.Add(oCookies)
        End If

        Dim loWebResponse As HttpWebResponse = loHttp.GetResponse()

        If (loWebResponse.Cookies.Count > 0) Then
            If (oCookies) Is Nothing Then
                oCookies = loWebResponse.Cookies
            End If
        Else
            For Each oRespCookie As Cookie In loWebResponse.Cookies
                Dim bMatch As Boolean = False
                For Each oReqCookie As Cookie In oCookies
                    If (oReqCookie.Name = oRespCookie.Name) Then
                        oReqCookie.Value = oRespCookie.Name
                        bMatch = True
                        ' break()
                    End If
                Next
                If (bMatch = False) Then
                    oCookies.Add(oRespCookie)
                End If

            Next
        End If
        Dim enc As Encoding = Encoding.GetEncoding(1252)

        If (loWebResponse.ContentEncoding.Length > 0) Then
            enc = Encoding.GetEncoding(loWebResponse.ContentEncoding)
        End If
        Dim loResponseStream As StreamReader = New StreamReader(loWebResponse.GetResponseStream(), enc)
        MsgBox(loResponseStream.ReadToEnd(), vbInformation, "resultado REST")

        loResponseStream.Close()
        loWebResponse.Close()

    End Sub

    Private Sub txtUrl_TextChanged(sender As Object, e As EventArgs) Handles txtUrl.TextChanged

    End Sub
End Class
