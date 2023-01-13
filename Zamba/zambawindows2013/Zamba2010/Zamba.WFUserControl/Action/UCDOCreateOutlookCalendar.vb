Imports System.Globalization

''' <summary>
''' [AlejandroR] - Created - 08/03/10
''' </summary>
''' <remarks></remarks>

Public Class UCDOCreateOutlookCalendar
    Inherits ZRuleControl

    Dim CurrentRule As IDOCreateOutlookCalendar

    Public Sub New(ByRef DOCreateOutlookCalendar As IDOCreateOutlookCalendar, ByRef _wfPanelCircuit As IWFPanelCircuit)

        MyBase.New(DOCreateOutlookCalendar, _wfPanelCircuit)

        Dim culture As CultureInfo = CultureInfo.CreateSpecificCulture("es-AR")

        Try

            CurrentRule = DOCreateOutlookCalendar
            InitializeComponent()

            txtSubject.Text = CurrentRule.Subject
            txtBody.Text = CurrentRule.Body
            txtLocation.Text = CurrentRule.Location
            txtTo.Text = CurrentRule.ToMails
            txtOrganizador.Text = CurrentRule.Organizer

            Select Case CurrentRule.AppointmentDateType

                Case OLAppointmentDateType.FechaFija

                    optFecha_FechaFija.Checked = True

                    dtStartDate.Value = DateTime.Parse(CurrentRule.AppointmentStartDate, culture, Globalization.DateTimeStyles.None)
                    dtEndDate.Value = DateTime.Parse(CurrentRule.AppointmentEndDate, culture, Globalization.DateTimeStyles.None)

                    dtStartTime.Value = DateTime.Parse(CurrentRule.AppointmentStartTime, culture, Globalization.DateTimeStyles.None)
                    dtEndTime.Value = DateTime.Parse(CurrentRule.AppointmentEndTime, culture, Globalization.DateTimeStyles.None)

                    chkAllDay.Checked = CurrentRule.AllDayAppointment

                Case OLAppointmentDateType.TextoInteligente

                    optFecha_TextoInteligente.Checked = True

                    txtStartDate.Text = CurrentRule.AppointmentStartDate
                    txtEndDate.Text = CurrentRule.AppointmentEndDate

                    txtStartTime.Text = CurrentRule.AppointmentStartTime
                    txtEndTime.Text = CurrentRule.AppointmentEndTime

                    chkAllDayTxtInteligente.Checked = CurrentRule.AllDayAppointment

            End Select

            If CurrentRule.ShowGeneratedCalendar Then
                optForm_Abrir.Checked = True
                chkEnvioAutomatico.Checked = False
                chkEnvioAutomatico.Enabled = False
            Else
                optForm_EnviarMail.Checked = True
                chkEnvioAutomatico.Checked = CurrentRule.AutomaticSend
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click

        If optFecha_TextoInteligente.Checked Then

            If chkAllDayTxtInteligente.Checked Then
                If String.IsNullOrEmpty(txtStartDate.Text) OrElse String.IsNullOrEmpty(txtEndDate.Text) Then
                    MessageBox.Show("Se deben ingresar las fechas de inicio y de fin para el texto inteligente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Else
                If String.IsNullOrEmpty(txtStartDate.Text) OrElse _
                   String.IsNullOrEmpty(txtStartTime.Text) OrElse _
                   String.IsNullOrEmpty(txtEndDate.Text) OrElse _
                   String.IsNullOrEmpty(txtEndTime.Text) Then
                    MessageBox.Show("Se deben ingresar todos los valores en las fechas de inicio y de fin para el texto inteligente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If

        End If

        Try

            CurrentRule.Subject = txtSubject.Text
            CurrentRule.Body = txtBody.Text
            CurrentRule.Location = txtLocation.Text
            CurrentRule.ShowGeneratedCalendar = optForm_Abrir.Checked
            CurrentRule.ToMails = txtTo.Text
            CurrentRule.Organizer = txtOrganizador.Text
            CurrentRule.AutomaticSend = chkEnvioAutomatico.Checked

            If optFecha_FechaFija.Checked Then

                CurrentRule.AppointmentDateType = OLAppointmentDateType.FechaFija

                CurrentRule.AppointmentStartDate = dtStartDate.Value.ToString("dd/MM/yyyy")
                CurrentRule.AppointmentStartTime = dtStartTime.Value.ToString("HH:mm tt")
                CurrentRule.AppointmentEndDate = dtEndDate.Value.ToString("dd/MM/yyyy")
                CurrentRule.AppointmentEndTime = dtEndTime.Value.ToString("HH:mm tt")
                CurrentRule.AllDayAppointment = chkAllDay.Checked

            ElseIf optFecha_TextoInteligente.Checked Then

                CurrentRule.AppointmentDateType = OLAppointmentDateType.TextoInteligente

                CurrentRule.AppointmentStartDate = txtStartDate.Text
                CurrentRule.AppointmentStartTime = txtStartTime.Text
                CurrentRule.AppointmentEndDate = txtEndDate.Text
                CurrentRule.AppointmentEndTime = txtEndTime.Text
                CurrentRule.AllDayAppointment = chkAllDayTxtInteligente.Checked

                If chkAllDayTxtInteligente.Checked Then
                    CurrentRule.AppointmentStartTime = DateTime.Now.ToShortDateString
                    CurrentRule.AppointmentEndTime = DateTime.Now.ToShortDateString
                End If

            End If

            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Subject)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.Body)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.Location)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.AppointmentDateType)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.AppointmentStartDate)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.AppointmentStartTime)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 6, CurrentRule.AppointmentEndDate)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 7, CurrentRule.AppointmentEndTime)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 8, CurrentRule.ShowGeneratedCalendar)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 9, CurrentRule.AllDayAppointment)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 10, CurrentRule.ToMails)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 11, CurrentRule.Organizer)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 12, CurrentRule.AutomaticSend)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub optFecha_NoIngresa_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        updateEstadoControles()
    End Sub

    Private Sub optFecha_Especifica_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles optFecha_FechaFija.CheckedChanged
        updateEstadoControles()
    End Sub

    Private Sub optFecha_TextoInteligente_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles optFecha_TextoInteligente.CheckedChanged
        updateEstadoControles()
    End Sub

    Private Sub updateEstadoControles()
        dtStartDate.Enabled = False
        dtEndDate.Enabled = False
        dtStartTime.Enabled = False
        dtEndTime.Enabled = False
        txtStartDate.Enabled = False
        txtEndDate.Enabled = False
        txtStartTime.Enabled = False
        txtEndTime.Enabled = False
        chkAllDay.Enabled = False
        chkAllDay.Checked = False
        chkAllDayTxtInteligente.Enabled = False
        chkAllDayTxtInteligente.Checked = False
        txtEndDate.Text = String.Empty
        txtStartDate.Text = String.Empty
        txtEndTime.Text = String.Empty
        txtStartTime.Text = String.Empty
        dtStartDate.Value = Now
        dtEndDate.Value = Now
        dtStartTime.Value = Now
        dtEndTime.Value = Now

        If optFecha_FechaFija.Checked Then
            dtStartDate.Enabled = True
            dtEndDate.Enabled = True
            dtStartTime.Enabled = True
            dtEndTime.Enabled = True
            chkAllDay.Checked = False
            chkAllDay.Enabled = True
        ElseIf optFecha_TextoInteligente.Checked Then
            txtStartDate.Enabled = True
            txtEndDate.Enabled = True
            txtStartTime.Enabled = True
            txtEndTime.Enabled = True
            chkAllDayTxtInteligente.Checked = False
            chkAllDayTxtInteligente.Enabled = True
        End If
    End Sub

    Private Sub chkAllDay_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkAllDay.CheckedChanged
        dtStartTime.Enabled = Not chkAllDay.Checked
        dtEndTime.Enabled = Not chkAllDay.Checked
        dtEndDate.Enabled = Not chkAllDay.Checked

        If chkAllDay.Checked Then
            dtStartTime.Value = DateTime.Now.ToShortDateString
            dtEndTime.Value = DateTime.Now.ToShortDateString

            dtEndDate.Value = dtStartDate.Value
        End If
    End Sub

    Private Sub chkAllDayTxtInteligente_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkAllDayTxtInteligente.CheckedChanged
        txtStartTime.Enabled = Not chkAllDayTxtInteligente.Checked
        txtEndTime.Enabled = Not chkAllDayTxtInteligente.Checked
        txtEndDate.Enabled = Not chkAllDayTxtInteligente.Checked

        If chkAllDayTxtInteligente.Checked Then
            txtStartTime.Text = ""
            txtEndTime.Text = ""

            txtEndDate.Text = txtStartDate.Text
        End If
    End Sub

    Private Sub dtStartDate_ValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles dtStartDate.ValueChanged
        If chkAllDay.Checked Then
            dtEndDate.Value = dtStartDate.Value
        End If
    End Sub

    Private Sub txtStartDate_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtStartDate.TextChanged
        If chkAllDayTxtInteligente.Checked Then
            txtEndDate.Text = txtStartDate.Text
        End If
    End Sub

    Private Sub optForm_Abrir_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles optForm_Abrir.CheckedChanged
        chkEnvioAutomatico.Checked = False
        chkEnvioAutomatico.Enabled = False
    End Sub

    Private Sub optForm_EnviarMail_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles optForm_EnviarMail.CheckedChanged
        chkEnvioAutomatico.Checked = False
        chkEnvioAutomatico.Enabled = True
    End Sub

End Class