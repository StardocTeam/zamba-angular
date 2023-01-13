Imports System.Globalization
Imports Zamba.AdminControls
Imports System.Windows.Forms
Imports System.IO
Imports Zamba.OfficeCommon

Public Class PlayDOCreateOutlookCalendar

    Private _myRule As IDOCreateOutlookCalendar
    Private pathDirectory As String

    Sub New(ByVal rule As IDOCreateOutlookCalendar)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As List(Of ITaskResult)) As List(Of Core.ITaskResult)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Play de la DOCreateOutlookCalendar")

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

        allDayEvent = _myRule.AllDayAppointment

        Try

            Select Case _myRule.AppointmentDateType

                Case OLAppointmentDateType.SinEspecificar

                    startDate = DateTime.Now
                    endDate = DateTime.Now

                Case OLAppointmentDateType.FechaFija

                    'Fecha seteada en la config de la regla
                    startDate = DateTime.Parse(_myRule.AppointmentStartDate + " " + _myRule.AppointmentStartTime, culture, Globalization.DateTimeStyles.None)
                    endDate = DateTime.Parse(_myRule.AppointmentEndDate + " " + _myRule.AppointmentEndTime, culture, Globalization.DateTimeStyles.None)

                Case OLAppointmentDateType.TextoInteligente

                    auxFecha = TextoInteligente.ReconocerCodigo(_myRule.AppointmentStartDate, res)
                    auxFecha = WFRuleParent.ReconocerVariablesValuesSoloTexto(auxFecha)

                    auxHora = TextoInteligente.ReconocerCodigo(_myRule.AppointmentStartTime, res)
                    auxHora = WFRuleParent.ReconocerVariablesValuesSoloTexto(auxHora)

                    If String.IsNullOrEmpty(auxFecha) OrElse String.IsNullOrEmpty(auxHora) Then
                        ZTrace.WriteLineIf(ZTrace.IsError, "La fecha y hora de inicio no pueden estar vacias")
                        Throw New Exception("La fecha y hora de inicio no pueden estar vacias")
                    Else
                        startDate = DateTime.Parse(auxFecha + " " + auxHora, culture, Globalization.DateTimeStyles.None)
                    End If

                    auxFecha = TextoInteligente.ReconocerCodigo(_myRule.AppointmentEndDate, res)
                    auxFecha = WFRuleParent.ReconocerVariablesValuesSoloTexto(auxFecha)

                    auxHora = TextoInteligente.ReconocerCodigo(_myRule.AppointmentEndTime, res)
                    auxHora = WFRuleParent.ReconocerVariablesValuesSoloTexto(auxHora)

                    If String.IsNullOrEmpty(auxFecha) OrElse String.IsNullOrEmpty(auxHora) Then
                        ZTrace.WriteLineIf(ZTrace.IsError, "La fecha y hora de finalizacion no pueden estar vacias")
                        Throw New Exception("La fecha y hora de finalizacion no pueden estar vacias")
                    Else
                        endDate = DateTime.Parse(auxFecha + " " + auxHora, culture, Globalization.DateTimeStyles.None)
                    End If

            End Select

            If Not String.IsNullOrEmpty(_myRule.Subject) Then
                subject = TextoInteligente.ReconocerCodigo(_myRule.Subject, res)
                subject = WFRuleParent.ReconocerVariablesValuesSoloTexto(subject)
            End If

            If Not String.IsNullOrEmpty(_myRule.Location) Then
                location = TextoInteligente.ReconocerCodigo(_myRule.Location, res)
                location = WFRuleParent.ReconocerVariablesValuesSoloTexto(location)
            End If

            If Not String.IsNullOrEmpty(_myRule.Body) Then
                body = TextoInteligente.ReconocerCodigo(_myRule.Body, res)
                body = WFRuleParent.ReconocerVariablesValuesSoloTexto(body)
            End If

            If Not String.IsNullOrEmpty(_myRule.ToMails) Then
                toMails = TextoInteligente.ReconocerCodigo(_myRule.ToMails, res)
                toMails = WFRuleParent.ReconocerVariablesValuesSoloTexto(toMails)
            End If

            If Not String.IsNullOrEmpty(_myRule.Organizer) Then
                organizer = TextoInteligente.ReconocerCodigo(_myRule.Organizer, res)
                organizer = WFRuleParent.ReconocerVariablesValuesSoloTexto(organizer)
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando calendario")

            'generar el calendario como ics
            icsBody = Zamba.Office.Outlook.SharedOutlook.GetOutlook().NewCalendar(organizer, toMails, subject, location, body, startDate, endDate, allDayEvent)

            'generar el nombre del archivo donde sera guardado
            CalendarFile = GetCalendarFileName(subject)

            'guardar el archivo con el calendario
            SaveCalendar(CalendarFile, icsBody)

            If _myRule.ShowGeneratedCalendar Then 'abrir el archivo generado
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

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo nombre de archivo para el calendario")

        If Not String.IsNullOrEmpty(subject) Then
            FilePath = New FileInfo(FileBusiness.GetUniqueFileName(pathDirectory & "\", subject, ".ics")).FullName
        Else
            FilePath = New FileInfo(FileBusiness.GetUniqueFileName(pathDirectory & "\", "OutlookCalendar", ".ics")).FullName
        End If

        Try
            If File.Exists(FilePath) Then
                File.Delete(FilePath)
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo borrar el archivo temporal al generar el nombre del archivo: " + FilePath)
        End Try

        Return FilePath

    End Function

    Private Sub SaveCalendar(ByVal FileName As String, ByVal ICS As String)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando ics en: " + FileName)

        Dim sw As New StreamWriter(FileName, False)
        sw.Write(ICS)
        sw.Close()
        sw = Nothing

    End Sub

    Private Sub ShowCalendar(ByVal FilePath As String)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando shell para el calendario")

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
        Dim FI As FileInfo

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Enviando por email el calendario")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Destinatarios: " & ToMails)

        IcsResult.ID = res.ID
        IcsResult.DocTypeId = res.DocTypeId


        Results.Add(IcsResult)

        If Membership.MembershipHelper.CurrentUser.eMail.Type = MailTypes.OutLookMail Then

            FI = New FileInfo(FileBusiness.GetUniqueFileName(pathDirectory, "OutlookCalendarMail", ".msg"))

            Params.Add(OutlookMailParameters.ATTACH_PATHS, IcsResult.FullPath.Split("§")) 'el split es solo para obtener un array
            Params.Add(OutlookMailParameters.TO, ToMails)
            Params.Add(OutlookMailParameters.SUBJECT, Subject)
            Params.Add(OutlookMailParameters.BODY, Body)

            If Not MessagesBusiness.GetNewMailAndSaveHistory(IcsResult.ID, IcsResult.DocTypeId, Params, _myRule.AutomaticSend) Then
                ZTrace.WriteLineIf(ZTrace.IsError, "No se pudo enviar el email")
                Throw New Exception("No se pudo enviar el email")
            End If

        Else

            Dim frmMessage As New Form

            If _myRule.AutomaticSend Then

                frmMessage = clsMessages.getMailForm(False, -1, String.Empty, String.Empty,
                                                     String.Empty, String.Empty, Nothing, -1,
                                                     String.Empty, String.Empty, String.Empty, res)


                DirectCast(frmMessage, IZMessageForm).EspecificarDatos(ToMails, String.Empty, String.Empty, Subject, Body, IcsResult.FullPath.Split("§"), Nothing, _myRule.AutomaticSend, Nothing)

            Else

                frmMessage = Zamba.AdminControls.clsMessages.getMailFormFromForum(Results, ToMails, Subject, Body)

                If Not frmMessage Is Nothing Then
                    frmMessage.WindowState = FormWindowState.Normal
                    frmMessage.ShowInTaskbar = True
                    frmMessage.ShowDialog()
                    frmMessage.Focus()
                Else
                    ZTrace.WriteLineIf(ZTrace.IsError, "El usuario no tiene configurado el correo")
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
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo borrar el archivo temporal luego de enviar el email: " + FilePath)
        End Try

    End Sub

End Class