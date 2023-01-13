Imports Zamba.Data
Imports System.Collections.Generic
Imports System.Configuration

Public Class ErrorReportBusiness

    Public Shared Adding As Boolean
    Public Sub AddErrorReport(ByVal errorReport As ErrorReport)
        If Adding = False Then
            Adding = True

            Dim errorReportingData As New ErrorReportData()
            Dim t As Transaction = Nothing

            Try
                t = New Transaction

                'Se agrega el reporte de error
                If errorReport.Id = 0 Then
                    errorReport.Id = Zamba.Data.CoreData.GetNewID(IdTypes.ErrorReport)
                End If
                errorReportingData.AddError(errorReport, t)

                'Se agregan los adjuntos del reporte de error
                For Each attach As ErrorReportAttachment In errorReport.Attachments
                    If attach.Id = 0 Then
                        attach.Id = Zamba.Data.CoreData.GetNewID(IdTypes.ErrorReportAttach)
                    End If

                    If attach.File Is Nothing Then
                        attach.File = FileEncode.Encode(attach.FileName)
                    End If
                    attach.FileName = IO.Path.GetFileName(attach.FileName)
                    ZTrace.WriteLineIf(TraceLevel.Verbose, errorReport.Description & " " & errorReport.Description)
                    errorReportingData.AddErrorAttach(errorReport.Id, attach, t)
                Next

                t.Commit()
            Catch ex As Exception
                If t IsNot Nothing AndAlso t.Transaction IsNot Nothing AndAlso t.Transaction.Connection.State <> ConnectionState.Closed Then
                    t.Rollback()
                End If
                ZTrace.WriteLineIf(TraceLevel.Error, ex.ToString())
            Finally
                Adding = False
                errorReportingData = Nothing
                If t IsNot Nothing Then
                    If t.Con IsNot Nothing Then
                        t.Con.Close()
                        t.Con.dispose()
                        t.Con = Nothing
                    End If
                    t.Dispose()
                    t = Nothing
                End If
            End Try
        End If
    End Sub

    Public Sub CleanDBErrors()
        Dim errorReportingData As New ErrorReportData()
        Try
            errorReportingData.DeleteErrors()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            errorReportingData = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene todos los reportes de error
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetErrorReports() As List(Of ErrorReport)
        Dim lstErrors As New List(Of ErrorReport)
        Dim errorData As New ErrorReportData
        Dim dtErrors As DataTable = errorData.GetAllErrorReports

        For Each row As DataRow In dtErrors.Rows
            lstErrors.Add(New ErrorReport(CLng(row("Id")), _
                                          row("Subject"), _
                                          row("Description"), _
                                          DirectCast(row("StateId"), ErrorReportStates), _
                                          CLng(row("UserId")), _
                                          CDate(row("Created")), _
                                          CDate(row("Updated")), _
                                          row("Comments"), _
                                          row("WinUser"), _
                                          row("Machine"), _
                                          row("Version")))
        Next

        errorData = Nothing
        dtErrors.Dispose()
        dtErrors = Nothing
        Return lstErrors
    End Function

    ''' <summary>
    ''' Obtiene los adjuntos de un reporte de error sin los bytes
    ''' </summary>
    ''' <param name="errorId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAttachments(ByVal errorId As Int64) As List(Of ErrorReportAttachment)
        Dim lstAttachs As New List(Of ErrorReportAttachment)
        Dim errorData As New ErrorReportData
        Dim dtAttachs As DataTable = errorData.GetAttachments(errorId)

        For Each row As DataRow In dtAttachs.Rows
            lstAttachs.Add(New ErrorReportAttachment(row("FileName"), CLng(row("Id"))))
        Next

        errorData = Nothing
        dtAttachs.Dispose()
        dtAttachs = Nothing
        Return lstAttachs
    End Function

    ''' <summary>
    ''' Obtiene el binario de un adjunto
    ''' </summary>
    ''' <param name="attachId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAttachment(ByVal attachId As Int64) As Byte()
        Dim errorData As New ErrorReportData
        Return errorData.GetAttachment(attachId)
        errorData = Nothing
    End Function

    ''' <summary>
    ''' Edita el estado y comentarios de un reporte de error
    ''' </summary>
    ''' <param name="errorId"></param>
    ''' <param name="errorState"></param>
    ''' <param name="comments"></param>
    ''' <remarks></remarks>
    Public Sub EditReport(ByVal errorId As Int64, ByVal errorState As ErrorReportStates, ByVal comments As String)
        Dim errorData As New ErrorReportData
        errorData.EditReport(errorId, errorState, comments)
        errorData = Nothing
    End Sub

    Public Sub AddException(ByVal ex As Exception)
        Using report As New ErrorReport(ex.Message, ex.ToString)
            AddErrorReport(report)
        End Using
    End Sub

    Public Sub AddPerformanceIssue(ByVal subject As String, ByVal description As String)
        Using report As New ErrorReport(subject, description)
            AddErrorReport(report)
        End Using
    End Sub



    ''' <summary>
    ''' Obtiene un listado de todos los reportes a exportar
    ''' </summary>
    ''' <returns>List(Of ErrorReport)</returns>
    ''' <remarks></remarks>
    Public Function GetReportsToExport() As ErrorReport()
        Dim dsReports As DataSet = GetDsReportsToExport()
        Dim lookForAttachments As Boolean = dsReports.Tables("attachments").Rows.Count > 0
        Dim reports(dsReports.Tables("reports").Rows.Count - 1) As ErrorReport
        Dim i As Int32 = 0

        For Each rowR As DataRow In dsReports.Tables("reports").Rows
            Dim report As New ErrorReport(CLng(rowR("Id")),
                                          ConvertDbNull(rowR("Subject")),
                                          rowR("Description"),
                                          DirectCast(rowR("StateId"), ErrorReportStates),
                                          CLng(rowR("UserId")),
                                          CDate(rowR("Created")),
                                          CDate(rowR("Updated")),
                                          ConvertDbNull(rowR("Comments")),
                                          ConvertDbNull(rowR("WinUser")),
                                          ConvertDbNull(rowR("Machine")),
                                          ConvertDbNull(rowR("Version")))

            If lookForAttachments Then
                For Each rowA As DataRow In dsReports.Tables("attachments").Select("ReportId=" & report.Id)
                    Dim attach As New ErrorReportAttachment()
                    attach.Id = 0 'Cero para que genere un nuevo ID
                    attach.FileName = rowA("FileName")
                    attach.File = DirectCast(rowA("Attachment"), Byte())
                    report.Attachments.Add(attach)
                Next
            End If

            reports(i) = report
            i += 1
        Next

        If dsReports IsNot Nothing Then
            dsReports.Dispose()
            dsReports = Nothing
        End If

        Return reports
    End Function

    ''' <summary>
    ''' Convierte valores de tipo DbNull en String.Empty
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertDbNull(ByVal o As Object) As String
        If IsDBNull(o) Then
            Return String.Empty
        Else
            Return o.ToString
        End If
    End Function

    ''' <summary>
    ''' Obtiene un DataSet con los reportes no exportados. 
    ''' El DataSet contiene dos tablas: reports y attachments.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDsReportsToExport()
        Dim lastExportedReportId As Int64 = CLng(ZOptBusiness.GetValueOrDefault("LastExportedReportId", 0))
        Dim erd As New ErrorReportData
        Dim dsReports As DataSet = erd.GetReportsToExport(lastExportedReportId)
        dsReports.Tables(0).TableName = "reports"
        dsReports.Tables(1).TableName = "attachments"
        erd = Nothing
        Return dsReports
    End Function

    Public Function SendException(ex As Exception) As Boolean
        Dim zopt = New Zamba.Core.ZOptBusiness()
        Dim user = ""
        Dim pass = ""
        Dim from = ""
        Dim port = ""
        Dim smtp = ""
        Dim mailto = ""
        Dim enableSsl = False

        Dim UB As New UserBusiness
        Dim UserId As Int64 = Zamba.Membership.MembershipHelper.CurrentUser.ID
        Dim Correo As ICorreo = UB.FillUserMailConfig(UserId)

        If zopt.GetValue("UseEmailConfigFromAD") <> Nothing And Boolean.TryParse(zopt.GetValue("UseEmailConfigFromAD"), False) Then
            user = Zamba.Membership.MembershipHelper.CurrentUser.eMail.UserName
            pass = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Password
            from = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Mail
            port = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Puerto.ToString()
            smtp = Zamba.Membership.MembershipHelper.CurrentUser.eMail.ProveedorSMTP
            Boolean.TryParse(Zamba.Membership.MembershipHelper.CurrentUser.eMail.EnableSsl, enableSsl)
        Else
            If Boolean.Parse(zopt.GetValue("WebView_SendBySMTP")) And Boolean.TryParse(zopt.GetValue("UseEmailConfigFromAD"), False) Then
                user = zopt.GetValue("WebView_UserSMTP")
                pass = zopt.GetValue("WebView_PassSMTP")
                from = zopt.GetValue("WebView_FromSMTP")
                port = zopt.GetValue("WebView_PortSMTP")
                smtp = zopt.GetValue("WebView_SMTP")
                Boolean.TryParse(zopt.GetValue("WebView_SslSMTP"), enableSsl)
            ElseIf Zamba.Membership.MembershipHelper.CurrentUser.eMail.Type = MailTypes.NetMail Then
                user = Zamba.Membership.MembershipHelper.CurrentUser.eMail.UserName
                pass = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Password
                from = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Mail
                port = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Puerto.ToString()
                smtp = Zamba.Membership.MembershipHelper.CurrentUser.eMail.ProveedorSMTP
                enableSsl = Zamba.Membership.MembershipHelper.CurrentUser.eMail.EnableSsl
            ElseIf ConfigurationManager.AppSettings("WebView_SendBySMTP") <> String.Empty Then
                user = ConfigurationManager.AppSettings("WebView_UserSMTP")
                pass = ConfigurationManager.AppSettings("WebView_PassSMTP")
                from = ConfigurationManager.AppSettings("WebView_FromSMTP")
                port = ConfigurationManager.AppSettings("WebView_PortSMTP")
                smtp = ConfigurationManager.AppSettings("WebView_SMTP")
                Boolean.TryParse(ConfigurationManager.AppSettings("WebView_SslSMTP"), enableSsl)
            End If
        End If

        Dim mail As New SendMailConfig

        mail.From = from
        mail.Password = pass
        mail.Port = port
        mail.EnableSsl = enableSsl
        mail.SMTPServer = smtp
        mail.UserName = user
        mail.MailTo = from  'Se auto envia el correo.
        'mail.Basemail = Correo.Base

        mail.Body = ex.ToString()

        Return MessagesBusiness.SendMail(mail)
    End Function
End Class
