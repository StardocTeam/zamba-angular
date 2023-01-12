'Imports Zamba.Core
'Imports Zamba.Services
Imports System.Collections.Generic

Partial Class MasterPage

    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If Session("UserId") Is Nothing Then
            'FormsAuthentication.RedirectToLoginPage()
            'Response.End()
        End If

        'icono del titulo
        lnkWebIcon.Attributes.Add("href", "~/App_Themes/" & Page.Theme & "/Images/WebIcon.ico")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Try
        '    If Not Page.IsPostBack Then
        '        If Not Session("User") Is Nothing Then
        '            Dim user As IUser
        '            user = Session("User")
        '            Me.lblUsuarioActual2.Text = user.Name
        '            Me.lblUsuarioActual.Text = user.Name
        '            hdnUserID.Value = user.ID.ToString()
        '            Dim zopt As New ZOptBusiness
        '            Dim link As String = zopt.GetValue("WebHomeLink")
        '            Dim target As String = zopt.GetValue("WebHomeTarget")
        '            zopt = Nothing
        '            Dim sUserPref As New SUserPreferences
        '            hdnRefreshWF.Value = sUserPref.getValue("WebRefreshWFTab", Sections.WorkFlow, False)

        '            If String.IsNullOrEmpty(link) Then
        '                hdnLink.Value = HttpContext.Current.Request.Url.ToString()
        '            Else
        '                hdnLink.Value = link
        '            End If

        '            If String.IsNullOrEmpty(target) Then
        '                hdnTarget.Value = "_self"
        '            Else
        '                hdnTarget.Value = target
        '            End If
        '            'Me.UC_WFExecution.TaskID = Task_ID

        '            Session("ListOfTask") = Nothing

        '            'Actualiza el timemout
        '            Dim rights As SRights = New SRights()
        '            Dim type As Int32 = 0
        '            If user.WFLic Then type = 1
        '            If (user.ConnectionId > 0) Then
        '                Dim SUserPreferences As New SUserPreferences()
        '                rights.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type)
        '                SUserPreferences = Nothing
        '            Else
        '                Response.Redirect("~/Views/Security/LogIn.aspx")
        '            End If
        '            rights = Nothing
        '        End If
        '    End If

        '    hdnUserID.Value = Session("UserId")
        '    hdnConnectionId.Value = Session("ConnectionId")
        '    hdnComputer.Value = Session("ComputerNameOrIP")
        'Catch ex As Exception
        '    ZClass.raiseerror(ex)
        'End Try
    End Sub

    Protected Sub MasterLogout(ByVal sender As Object, ByVal e As System.EventArgs)

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
End Class