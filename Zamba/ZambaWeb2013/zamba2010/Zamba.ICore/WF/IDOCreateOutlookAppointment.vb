Public Interface IDOCreateOutlookAppointment
    Inherits IRule
    Property Subject() As String
    Property Body() As String
    Property Location() As String
    Property AppointmentDateType() As OLAppointmentDateType
    Property AppointmentStartDate() As String
    Property AppointmentStartTime() As String
    Property AppointmentEndDate() As String
    Property AppointmentEndTime() As String
    Property ShowAppointmentForm() As Boolean
End Interface
