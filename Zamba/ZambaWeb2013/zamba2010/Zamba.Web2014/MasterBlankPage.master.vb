Imports Zamba.Core
Imports System.IO

Partial Class MasterBlankPage

    Inherits System.Web.UI.MasterPage


    Protected Sub Page_Init(sender As Object, e As System.EventArgs)
        If Session("UserId") Is Nothing Then
            If Request.QueryString("UserId") Is Nothing AndAlso Request.QueryString("Token") Is Nothing Then
                FormsAuthentication.RedirectToLoginPage()
                Response.[End]()
            Else
                Dim userid As Integer = Integer.Parse(Request.QueryString("UserId"))
                Dim user As IUser = Zamba.Core.UserBusiness.Rights.ValidateLogIn(userid, Zamba.Core.ClientType.Web)
                Session("UserId") = userid
            End If
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        hdnUserId.Value = Session("UserId")
        If Request.Url.ToString.ToLower().Contains("wf.aspx") OrElse Request.Url.ToString.ToLower().Contains("search.aspx") OrElse Request.Url.ToString.ToLower().Contains("results.aspx") Then
            Me.EnableViewState = True
        Else
            Me.EnableViewState = False
        End If

        Dim wsReference As New System.Web.UI.ServiceReference("~/Services/IndexsService.asmx")
        ScriptManager1.Services.Add(wsReference)
    End Sub

    Public Shared Function GetAppRootUrl(ByVal endSlash As Boolean) As String
        Dim host As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)
        Dim appRootUrl As String = HttpContext.Current.Request.ApplicationPath
        If Not appRootUrl.EndsWith("/") Then
            'a virtual
            appRootUrl += "/"
        End If
        If Not endSlash Then
            appRootUrl = appRootUrl.Substring(0, appRootUrl.Length - 1)
        End If
        Return host + appRootUrl
    End Function

    Public Shared Function GetJqueryCoreScript() As IHtmlString
        Return Tools.GetJqueryCoreScript(HttpContext.Current.Request)
    End Function

    Public Shared Function GetIsOldBrowser() As String
        Return Tools.GetIsOldBrowser(HttpContext.Current.Request).ToString().ToLower()
    End Function
End Class