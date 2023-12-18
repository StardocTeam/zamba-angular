﻿Imports System.Collections.Generic
Imports Zamba.Office
Imports System.Globalization
Imports Zamba.AdminControls
Imports System.Windows.Forms
Imports System.IO
Imports Zamba.OfficeCommon
Imports Zamba.Core

Public Class PlayDOCreateOutlookCalendar

    Private _myRule As IDOCreateOutlookCalendar
    Private pathDirectory As String

    Sub New(ByVal rule As IDOCreateOutlookCalendar)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As List(Of ITaskResult)) As List(Of Core.ITaskResult)

        Trace.WriteLineIf(ZTrace.IsInfo, "Play de la DOCreateOutlookCalendar")

        pathDirectory = Tools.EnvironmentUtil.GetTempDir("\IndexerTemp").FullName

        If results.Count > 1 Then
            For Each result As IResult In results
                doCreateOutlookAppointment(result)
            Next
        ElseIf results.Count = 1 Then
            doCreateOutlookAppointment(DirectCast(results(0), ITaskResult))
        End If

        Return results

    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Private Sub doCreateOutlookAppointment(ByVal res As ITaskResult)

        Dim organizer As String
        Dim toMails As String
        Dim subject As String
        Dim location As String
        Dim body As String
        Dim startDate As DateTime
        Dim endDate As DateTime
        Dim auxFecha As String
        Dim auxHora As String
        Dim allDayEvent As Boolean
        Dim icsBody As String
        Dim CalendarFile As String

        Dim culture As CultureInfo = CultureInfo.CreateSpecificCulture("es-AR")

        allDayEvent = Me._myRule.AllDayAppointment

        Try

            Select Case Me._myRule.AppointmentDateType

                Case OLAppointmentDateType.SinEspecificar

                    startDate = DateTime.Now
                    endDate = DateTime.Now

                Case OLAppointmentDateType.FechaFija

                    'Fecha seteada en la config de la regla
                    startDate = DateTime.Parse(Me._myRule.AppointmentStartDate + " " + Me._myRule.AppointmentStartTime, culture, Globalization.DateTimeStyles.None)
                    endDate = DateTime.Parse(Me._myRule.AppointmentEndDate + " " + Me._myRule.AppointmentEndTime, culture, Globalization.DateTimeStyles.None)

                Case OLAppointmentDateType.TextoInteligente

                    auxFecha = TextoInteligente.ReconocerCodigo(Me._myRule.AppointmentStartDate, res)
                    auxFecha = WFRuleParent.ReconocerVariablesValuesSoloTexto(auxFecha)

                    auxHora = TextoInteligente.ReconocerCodigo(Me._myRule.AppointmentStartTime, res)
                    auxHora = WFRuleParent.ReconocerVariablesValuesSoloTexto(auxHora)

                    If String.IsNullOrEmpty(auxFecha) OrElse String.IsNullOrEmpty(auxHora) Then
                        Trace.WriteLineIf(ZTrace.IsError, "La fecha y hora de inicio no pueden estar vacias")
                        Throw New Exception("La fecha y hora de inicio no pueden estar vacias")
                    Else
                        startDate = DateTime.Parse(auxFecha + " " + auxHora, culture, Globalization.DateTimeStyles.None)
                    End If

                    auxFecha = TextoInteligente.ReconocerCodigo(Me._myRule.AppointmentEndDate, res)
                    auxFecha = WFRuleParent.ReconocerVariablesValuesSoloTexto(auxFecha)

                    auxHora = TextoInteligente.ReconocerCodigo(Me._myRule.AppointmentEndTime, res)
                    auxHora = WFRuleParent.ReconocerVariablesValuesSoloTexto(auxHora)

                    If String.IsNullOrEmpty(auxFecha) OrElse String.IsNullOrEmpty(auxHora) Then
                        Trace.WriteLineIf(ZTrace.IsError, "La fecha y hora de finalizacion no pueden estar vacias")
                        Throw New Exception("La fecha y hora de finalizacion no pueden estar vacias")
                    Else
                        endDate = DateTime.Parse(auxFecha + " " + auxHora, culture, Globalization.DateTimeStyles.None)
                    End If

            End Select

            If Not String.IsNullOrEmpty(Me._myRule.Subject) Then
                subject = TextoInteligente.ReconocerCodigo(Me._myRule.Subject, res)
                subject = WFRuleParent.ReconocerVariablesValuesSoloTexto(subject)
            End If

            If Not String.IsNullOrEmpty(Me._myRule.Location) Then
                location = TextoInteligente.ReconocerCodigo(Me._myRule.Location, res)
                location = WFRuleParent.ReconocerVariablesValuesSoloTexto(location)
            End If

            If Not String.IsNullOrEmpty(Me._myRule.Body) Then
                body = TextoInteligente.ReconocerCodigo(Me._myRule.Body, res)
                body = WFRuleParent.ReconocerVariablesValuesSoloTexto(body)
            End If

            If Not String.IsNullOrEmpty(Me._myRule.ToMails) Then
                toMails = TextoInteligente.ReconocerCodigo(Me._myRule.ToMails, res)
                toMails = WFRuleParent.ReconocerVariablesValuesSoloTexto(toMails)
            End If

            If Not String.IsNullOrEmpty(Me._myRule.Organizer) Then
                organizer = TextoInteligente.ReconocerCodigo(Me._myRule.Organizer, res)
                organizer = WFRuleParent.ReconocerVariablesValuesSoloTexto(organizer)
            End If

            Trace.WriteLineIf(ZTrace.IsInfo, "Generando calendario")

            'generar el calendario como ics
            icsBody = Zamba.Office.Outlook.SharedOutlook.GetOutlook().NewCalendar(organizer, toMails, subject, location, body, startDate, endDate, allDayEvent)

            'generar el nombre del archivo donde sera guardado
            CalendarFile = GetCalendarFileName(subject)

            'guardar el archivo con el calendario
            SaveCalendar(CalendarFile, icsBody)

            If Me._myRule.ShowGeneratedCalendar Then 'abrir el archivo generado
                ShowCalendar(CalendarFile)
            Else
                SendMail(toMails, subject, body, CalendarFile, res)
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Throw ex
        End Try

    End Sub

    Private Function GetCalendarFileName(ByVal subject As String) As String

        Dim FilePath As String

        Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo nombre de archivo para el calendario")

        If Not String.IsNullOrEmpty(subject) Then
            FilePath = New IO.FileInfo(FileBusiness.GetUniqueFileName(pathDirectory & "\", subject, ".ics")).FullName
        Else
            FilePath = New IO.FileInfo(FileBusiness.GetUniqueFileName(pathDirectory & "\", "OutlookCalendar", ".ics")).FullName
        End If

        Try
            If File.Exists(FilePath) Then
                File.Delete(FilePath)
            End If
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsInfo, "No se pudo borrar el archivo temporal al generar el nombre del archivo: " + FilePath)
        End Try

        Return FilePath

    End Function

    Private Sub SaveCalendar(ByVal FileName As String, ByVal ICS As String)

        Trace.WriteLineIf(ZTrace.IsInfo, "Guardando ics en: " + FileName)

        Dim sw As New StreamWriter(FileName, False)
        sw.Write(ICS)
        sw.Close()
        sw = Nothing

    End Sub

    Private Sub ShowCalendar(ByVal FilePath As String)

        Trace.WriteLineIf(ZTrace.IsInfo, "Ejecutando shell para el calendario")

        Dim psi As New ProcessStartInfo()
        psi.UseShellExecute = True
        psi.FileName = FilePath
        System.Diagnostics.Process.Start(psi)
        psi = Nothing

    End Sub

    Private Sub SendMail(ByVal ToMails As String, ByVal Subject As String, ByVal Body As String, ByVal FilePath As String, ByVal res As ITaskResult)

        Dim IcsResult As New NewResult(FilePath)
        Dim Results As New Generic.List(Of IResult)
        Dim Params As New Hashtable()
        Dim FI As IO.FileInfo

        Trace.WriteLineIf(ZTrace.IsInfo, "Enviando por email el calendario")
        Trace.WriteLineIf(ZTrace.IsInfo, "Destinatarios: " & ToMails)

        IcsResult.ID = res.ID
        IcsResult.DocTypeId = res.DocTypeId


        Results.Add(IcsResult)

        If Membership.MembershipHelper.CurrentUser.eMail.Type = MailTypes.OutLookMail Then

            FI = New IO.FileInfo(FileBusiness.GetUniqueFileName(pathDirectory, "OutlookCalendarMail", ".msg"))

            Params.Add(OutlookMailParameters.ATTACH_PATHS, IcsResult.FullPath.Split("§")) 'el split es solo para obtener un array
            Params.Add(OutlookMailParameters.TO, ToMails)
            Params.Add(OutlookMailParameters.SUBJECT, Subject)
            Params.Add(OutlookMailParameters.BODY, Body)

            If Not Zamba.Office.Outlook.SharedOutlook.GetOutlook().GetNewMailItem(FI.FullName, True, False, Params, Me._myRule.AutomaticSend) Then
                Trace.WriteLineIf(ZTrace.IsError, "No se pudo enviar el email")
                Throw New Exception("No se pudo enviar el email")
            End If

        Else

            Dim frmMessage As New Form

            If Me._myRule.AutomaticSend Then

                frmMessage = clsMessages.getMailForm(False, -1, String.Empty, String.Empty, _
                                                     String.Empty, String.Empty, Nothing, -1, _
                                                     String.Empty, String.Empty, String.Empty, res)


                DirectCast(frmMessage, IZMessageForm).EspecificarDatos(ToMails, String.Empty, String.Empty, Subject, Body, IcsResult.FullPath.Split("§"), Nothing, Me._myRule.AutomaticSend, Nothing)

            Else

                frmMessage = Zamba.AdminControls.clsMessages.getMailFormFromForum(Results, ToMails, Subject, Body)

                If Not frmMessage Is Nothing Then
                    frmMessage.WindowState = FormWindowState.Normal
                    frmMessage.ShowInTaskbar = True
                    frmMessage.ShowDialog()
                    frmMessage.Focus()
                Else
                    Trace.WriteLineIf(ZTrace.IsError, "El usuario no tiene configurado el correo")
                    Throw New Exception("El usuario no tiene configurado el correo")
                End If

            End If

        End If

        Try
            'borrar temporal solo si se mando el mail, sino puede que este abierto por outlook
            If File.Exists(FilePath) Then
                File.Delete(FilePath)
            End If
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsInfo, "No se pudo borrar el archivo temporal luego de enviar el email: " + FilePath)
        End Try

    End Sub

End Class