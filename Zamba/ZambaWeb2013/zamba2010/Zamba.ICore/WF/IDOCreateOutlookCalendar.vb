Public Interface IDOCreateOutlookCalendar
    Inherits IRule
    Property Organizer() As String
    Property ToMails() As String
    Property Subject() As String
    Property Body() As String
    Property Location() As String
    Property AppointmentDateType() As OLAppointmentDateType
    Property AppointmentStartDate() As String
    Property AppointmentStartTime() As String
    Property AppointmentEndDate() As String
    Property AppointmentEndTime() As String
    Property AllDayAppointment() As Boolean
    Property ShowGeneratedCalendar() As Boolean
    Property AutomaticSend() As Boolean
End Interface
