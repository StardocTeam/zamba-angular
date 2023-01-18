Imports Microsoft.Office.Interop
Imports System.IO
Imports Microsoft.Win32
Imports System.Diagnostics

Public Class OutlookInterop

    Dim app As Outlook.Application = Nothing
    Dim outlookNS As Outlook.NameSpace = Nothing
    Dim MAPIFolderSentMail As Outlook.MAPIFolder = Nothing

    Public Sub NewAppointment(ByVal subject As String, ByVal location As String, ByVal body As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal ShowForm As Boolean)
        Dim appointment As Outlook.AppointmentItem

        appointment = app.CreateItem(Outlook.OlItemType.olAppointmentItem)

        appointment.Subject = subject
        appointment.Body = body
        appointment.Location = location
        appointment.Start = StartDate
        appointment.End = EndDate

        If ShowForm Then
            appointment.Display(True)
        Else
            appointment.Save()
        End If

    End Sub

    Public Function NewCalendar(ByVal organizer As String, ByVal toMails As String, ByVal subject As String, ByVal location As String, ByVal body As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal AllDayEvent As Boolean) As String

        Dim evento As New ics.vEvent

        evento.UID = Guid.NewGuid().ToString()
        evento.DTStart = StartDate
        evento.DTEnd = EndDate
        evento.Location = location
        evento.Description = body
        evento.Summary = subject
        evento.AllDayEvent = AllDayEvent
        evento.Attendee = toMails.Split(";")

        If organizer.IndexOf("@") > 0 Then
            evento.Organizer = "mailto:" & organizer
        Else
            evento.Organizer = organizer
        End If

        Return New ics(evento).ToString()

    End Function

    Public Sub New()
        app = New Outlook.Application()
        outlookNS = app.GetNamespace(OutlookMailParameters.MAPI_NAMESPACE)
        MAPIFolderSentMail = outlookNS.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail)
        'AddHandler MAPIFolderSentMail.Items.ItemAdd, AddressOf SentMailItemHandler
    End Sub

End Class


