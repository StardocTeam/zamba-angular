Imports System
Imports System.Data
Imports System.Globalization
Imports System.Threading
Imports DayPilot.Web.Ui

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected ReadOnly Property Day() As DateTime
        Get
            If Not (Request.Params("day") Is Nothing) Then
                Try
                    Return Convert.ToDateTime(Request.Params("day"))
                Catch
                End Try
            End If
            Return DateTime.Now
        End Get
    End Property

    Private Sub Calendar1_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles Calendar1.DayRender
        Dim fontWeight As String = "normal"
        If isThereEvent(e.Day.Date) Then
            fontWeight = "bold"
        End If
        Dim color As String = "black"
        If e.Day.IsOtherMonth Then
            color = Me.Calendar1.OtherMonthDayStyle.ForeColor.Name
        End If
        e.Cell.Text = String.Format("<a href='WFCalendar.aspx?day={0:d}' style='color: " + color + ";text-decoration:none; font-weight:" + fontWeight + "'>{1}</a>", e.Day.Date, e.Day.Date.Day)
    End Sub

    Private Function isThereEvent(ByVal d As DateTime) As Boolean
        Dim today As DateTime = DateTime.Now
        If (d.DayOfYear = today.DayOfYear) AndAlso (d.Year = today.Year) Then
            Return True
        End If
        Return False
    End Function

    Protected ReadOnly Property getData() As DataTable
        Get
            Session("WfId") = 91
            Session("StepId") = 200

            Dim Eventos As New Eventos
            Dim ListaEventos As Generic.List(Of Evento) = Eventos.GetEventos(Session("WfId"), Session("StepId"))

            Dim DT As New DataTable
            DT.Columns.Add("start", GetType(DateTime))
            DT.Columns.Add("end", GetType(DateTime))
            DT.Columns.Add("name", GetType(String))
            DT.Columns.Add("id", GetType(String))

            For Each Evento As Evento In ListaEventos
                Dim DR As DataRow = DT.NewRow
                DR("id") = Evento.id
                DR("name") = Evento.name
                DR("start") = Evento.startTime
                DR("end") = Evento.endTime
                DT.Rows.Add(DR)
            Next

            Return DT
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Calendar1.SelectedDate = Day
        Me.Calendar1.VisibleDate = Day
        'Me.DayPilotCalendar1.StartDate = Day
        'Me.DayPilotCalendar1.EndDate = Day
        DataBind()
    End Sub
End Class
