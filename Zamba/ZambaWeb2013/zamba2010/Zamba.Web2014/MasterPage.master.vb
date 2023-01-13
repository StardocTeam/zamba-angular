Imports Zamba.Core
Imports Zamba.Services
Imports System.Collections.Generic

Partial Class MasterPage

    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim ZC As New Zamba.Core.ZCore
        ZC.InitializeSystem("Zamba.Web")
        Zamba.Core.UserPreferences.LoadAllMachineConfigValues()
        Zamba.Membership.MembershipHelper.OptionalAppTempPath = Zamba.Core.UserPreferences.getValue("AppTempPath", Zamba.Core.Sections.UserPreferences, String.Empty)

        Dim zoptb As New ZOptBusiness()
        Dim CurrentTheme As String = zoptb.GetValue("CurrentTheme")
        zoptb = Nothing


        If Session("UserId") Is Nothing Then
            FormsAuthentication.RedirectToLoginPage()
            Response.End()
        End If


        'icono del titulo
        lnkWebIcon.Attributes.Add("href", "~/App_Themes/" & CurrentTheme & "/Images/WebIcon.ico")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Not Session("User") Is Nothing Then
                    Dim user As IUser
                    user = Session("User")
                    Me.lblUsuarioActual2.InnerText = user.Name

                    hdnUserId.Value = user.ID.ToString()
                    Dim zopt As New ZOptBusiness
                    Dim link As String = zopt.GetValue("WebHomeLink")
                    Dim target As String = zopt.GetValue("WebHomeTarget")
                    zopt = Nothing
                    Dim sUserPref As New SUserPreferences
                    hdnRefreshWF.Value = sUserPref.getValue("WebRefreshWFTab", Sections.WorkFlow, False)

                    If String.IsNullOrEmpty(link) Then
                        hdnLink.Value = HttpContext.Current.Request.Url.ToString()
                    Else
                        hdnLink.Value = link
                    End If

                    If String.IsNullOrEmpty(target) Then
                        hdnTarget.Value = "_self"
                    Else
                        hdnTarget.Value = target
                    End If
                    'Me.UC_WFExecution.TaskID = Task_ID

                    Session("ListOfTask") = Nothing

                    'Actualiza el timemout
                    Dim rights As SRights = New SRights()
                    Dim type As Int32 = 0
                    If user.WFLic Then type = 1
                    If (user.ConnectionId > 0) Then
                        Dim SUserPreferences As New SUserPreferences()
                        rights.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type)
                        SUserPreferences = Nothing
                    Else
                        Response.Redirect("~/Views/Security/LogIn.aspx")
                    End If
                    rights = Nothing


                End If
            End If
            hdnUserId.Value = Session("UserId")
            hdnConnectionId.Value = Session("ConnectionId")
            hdnComputer.Value = Session("ComputerNameOrIP")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Protected Sub MasterLogout(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("~/Views/Security/Logout.aspx")
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

    Public Shared Function GetShowSessionRefreshLog() As String
        Dim zopt As New SZOptBusiness()
        Dim val As String = zopt.GetValue("ShowSessionRefreshLog")
        If String.IsNullOrEmpty(val) Then
            Return "false"
        Else
            Return val.ToLower()
        End If
    End Function

    Public Shared Function GetSessionTimeOut() As String
        Return HttpContext.Current.Session.Timeout
    End Function

    Public Shared Function RegisterThemeBundles() As IHtmlString

        Return Tools.RegisterThemeBundles(HttpContext.Current.Request)
    End Function

    Public Shared Function GetJqueryCoreScript() As IHtmlString
        Return Tools.GetJqueryCoreScript(HttpContext.Current.Request)
    End Function

    Public Shared Function GetIsOldBrowser() As String
        Return Tools.GetIsOldBrowser(HttpContext.Current.Request).ToString().ToLower()
    End Function
End Class