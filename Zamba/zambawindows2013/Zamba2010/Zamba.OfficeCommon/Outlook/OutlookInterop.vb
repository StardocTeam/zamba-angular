''' <summary>
''' Se supone que esta clase unificaba los métodos comunes de outlook, pero no se puede implementar ya que 
''' tenía agregada la dll de Office2007, generando errores en las implementaciones de Office2003 y menores
''' </summary>
''' <remarks></remarks>
Public Class OutlookInterop

    Public Shared Function NewCalendar(ByVal organizer As String, ByVal toMails As String, ByVal subject As String, ByVal location As String, ByVal body As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal AllDayEvent As Boolean) As String

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

End Class


