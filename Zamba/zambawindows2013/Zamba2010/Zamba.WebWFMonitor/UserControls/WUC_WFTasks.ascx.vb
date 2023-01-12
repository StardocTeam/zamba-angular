Imports Zamba.Core
Imports Zamba.WFBusiness
Imports System.Web
Imports System.Web.UI
Imports System.Net.Mail
Imports System.Data

Partial Class WUC_WFTasks
    Inherits UserControl
    Public Event ShowTaskDetails(ByVal TaskId As Int32)
    ''' <summary>
    ''' Devuelve una lista de las tareas seleccionadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSelectedTasks() As Generic.List(Of TaskResult)
        Dim TasksArray As New Generic.List(Of TaskResult)
        Dim WorkflowID As Int32 = Session.Item("WfId")
        Dim i As Integer
        For i = 0 To gvTasks.Rows.Count - 1
            Dim Row As GridViewRow = gvTasks.Rows(i)
            Dim IsChecked As Boolean = CType(Row.FindControl("CheckBox1"), CheckBox).Checked
            If IsChecked Then
                Dim TaskId As Int32 = CInt(Val(gvTasks.DataKeys(i).Value))
                TasksArray.Add(Zamba.WFBusiness.WFTaskBussines.GetTaskByTaskId(TaskId, WorkflowID))
            End If
            Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
        Next
        Return TasksArray
    End Function
    Public Function HasSelectedTasks() As Boolean
        If IsNothing(gvTasks.Rows) Then
            Return False
        Else
            If gvTasks.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    ''' <summary>
    ''' Devuelve los IDs de las tareas seleccionadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSelectedIds() As Generic.List(Of Integer)

        Dim TasksIDs As New Generic.List(Of Integer)
        Dim i As Integer
        For i = 0 To gvTasks.Rows.Count
            Dim Row As GridViewRow = gvTasks.Rows(i)
            Dim IsChecked As Boolean = CType(Row.FindControl("CheckBox1"), CheckBox).Checked
            If IsChecked Then
                Dim TaskId As Int32 = CInt(Val(gvTasks.DataKeys(i).Value))
                TasksIDs.Add(TaskId)
            End If
            System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
        Next
        Return TasksIDs
    End Function

    ''' <summary>
    ''' Carga el Dropdownlist con todos los estados posibles de la etapa
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDataSource()
        Try

            'Dim StepId As Integer = Session("StepID")
            'Dim WorkflowId As Integer = Session("WfId")
            'Dim StepStates As SortedList = WFStepBussines.GetStepStates(StepId, WorkflowId)
            'If IsNothing(StepStates) Then
            '    'poner ninguno
            'Else
            '    DropDownList3.DataSource = StepStates.Values
            '    DropDownList3.DataBind()
            'End If
        Catch ex As Exception
            RaiseError(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Refresca el datasource del GridView
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Refresh()
        gvTasks.DataBind()
    End Sub

    Public Sub Imprimir()
        Session("PrintGridviewColumns") = gvTasks
        ' Session("PrintGridViewDataSource") = Me.ObjectDataSource1.Select
        Dim StrBuilder As StringBuilder = New StringBuilder
        StrBuilder.Append("<script>")
        StrBuilder.Append(Environment.NewLine)
        StrBuilder.Append("window.open(""PrintGridview.aspx"",""Print"",""top=5,left=5"");")
        StrBuilder.Append(Environment.NewLine)
        StrBuilder.Append("</script>")
        Page.ClientScript.RegisterStartupScript(Me.GetType, "print", StrBuilder.ToString)
    End Sub

    Public Sub sendemail()

        Dim config As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath)
        Dim settings As System.Net.Configuration.MailSettingsSectionGroup = CType(config.GetSectionGroup("system.net/mailSettings"), System.Net.Configuration.MailSettingsSectionGroup)

        Dim credential As New System.Net.NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password)

        Dim client As New System.Net.Mail.SmtpClient()
        client.Host = settings.Smtp.Network.Host
        client.Credentials = credential

        Dim email As New MailMessage
        email.From = New MailAddress("patricio.mosse@stardoc.com.ar")
        email.To.Add("patricio.mosse@stardoc.com.ar")
        email.Subject = "Test Email"
        email.IsBodyHtml = True
        email.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure

        Dim s As New StringBuilder

        s.AppendLine("<HTML>")
        s.AppendLine("</table>")
        s.AppendLine("<table width=" & Chr(34) & "80%" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " cellpadding=" & Chr(34) & "1" & Chr(34) & " cellspacing=" & Chr(34) & "1" & Chr(34) & " align=" & Chr(34) & "center" & ">")
        s.AppendLine("<tr bgcolor=" & Chr(34) & "white" & Chr(34) & ">")
        s.AppendLine("<td width=" & Chr(34) & " 40%" & Chr(34) & "align=" & Chr(34) & "left" & Chr(34) & "><font class=" & Chr(34) & "program_header" & Chr(34) & ">Nombre</font></td>")
        s.AppendLine("<td width=" & Chr(34) & "25%" & Chr(34) & " align=" & Chr(34) & "center" & Chr(34) & "><font class=" & Chr(34) & "program_header" & Chr(34) & ">Fecha de Expiracion</font></td>")
        s.AppendLine("</tr>")

        For Each R As GridViewRow In gvTasks.Rows
            s.AppendLine("<tr bgcolor=" & Chr(34) & "#ffffff" & Chr(34) & ">")
            s.AppendLine("<td width=" & Chr(34) & "40%" & Chr(34) & ">" & GetText(R.Cells(1).Controls(0)) & "</td>")
            s.AppendLine("<td width=" & Chr(34) & "25%" & Chr(34) & "align=" & Chr(34) & "center" & Chr(34) & ">" & R.Cells(4).Text & "</td>")
            s.AppendLine("</tr>")
        Next

        s.AppendLine("</table>")
        s.AppendLine("</HTML>")

        email.Body = s.ToString

        client.Send(email)

    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTasks.RowCommand
        Select Case e.CommandName

            Case "ShowDetails"
                Dim TaskId As Int32 = CInt(Val(gvTasks.DataKeys(e.CommandArgument).Value))
                Session.Add("TaskId", TaskId)
                If TaskId <> 0 Then
                    RaiseEvent ShowTaskDetails(TaskId)
                End If
        End Select
    End Sub

    Private Function GetText(ByVal M As Object) As String
        Return M.text
    End Function

End Class