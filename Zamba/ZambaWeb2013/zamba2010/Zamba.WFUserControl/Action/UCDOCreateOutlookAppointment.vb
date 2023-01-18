Imports System.Globalization

''' <summary>
''' [AlejandroR] - Created - 28/12/09
''' Regla que permite crear un nuevo appointment en Outlook de manera automatica o
''' dejando que el usuario vea el formulario y realice los cambios que necesite.
''' </summary>
''' <remarks></remarks>

Public Class UCDOCreateOutlookAppointment
    Inherits ZRuleControl

    Dim CurrentRule As IDOCreateOutlookAppointment

    Public Sub New(ByRef DOCreateOutlookAppointment As IDOCreateOutlookAppointment, ByRef _wfPanelCircuit As IWFPanelCircuit)

        MyBase.New(DOCreateOutlookAppointment, _wfPanelCircuit)

        Dim culture As CultureInfo = CultureInfo.CreateSpecificCulture("es-AR")

        Try

            CurrentRule = DOCreateOutlookAppointment
            InitializeComponent()

            txtSubject.Text = CurrentRule.Subject
            txtBody.Text = CurrentRule.Body
            txtLocation.Text = CurrentRule.Location

            Select Case CurrentRule.AppointmentDateType

                Case OLAppointmentDateType.SinEspecificar

                    optFecha_SinEspecificar.Checked = True

                Case OLAppointmentDateType.FechaFija

                    optFecha_FechaFija.Checked = True

                    dtStartDate.Value = DateTime.Parse(CurrentRule.AppointmentStartDate, culture, Globalization.DateTimeStyles.None)
                    dtEndDate.Value = DateTime.Parse(CurrentRule.AppointmentEndDate, culture, Globalization.DateTimeStyles.None)

                    dtStartTime.Value = DateTime.Parse(CurrentRule.AppointmentStartTime, culture, Globalization.DateTimeStyles.None)
                    dtEndTime.Value = DateTime.Parse(CurrentRule.AppointmentEndTime, culture, Globalization.DateTimeStyles.None)

                Case OLAppointmentDateType.TextoInteligente

                    optFecha_TextoInteligente.Checked = True

                    txtStartDate.Text = CurrentRule.AppointmentStartDate
                    txtEndDate.Text = CurrentRule.AppointmentEndDate

                    txtStartTime.Text = CurrentRule.AppointmentStartTime
                    txtEndTime.Text = CurrentRule.AppointmentEndTime

            End Select

            If CurrentRule.ShowAppointmentForm Then
                optForm_Mostrar.Checked = True
            Else
                optForm_NoMostrar.Checked = True
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click

        If optFecha_SinEspecificar.Checked AndAlso optForm_NoMostrar.Checked Then
            MessageBox.Show("Si no se muestra el formulario se deben ingresar las fechas de inicio y de fin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If optFecha_TextoInteligente.Checked Then

            If String.IsNullOrEmpty(txtStartDate.Text) OrElse _
               String.IsNullOrEmpty(txtStartTime.Text) OrElse _
               String.IsNullOrEmpty(txtEndDate.Text) OrElse _
               String.IsNullOrEmpty(txtEndTime.Text) Then

                MessageBox.Show("Se deben ingresar todos los valores en las fechas de inicio y de fin para el texto inteligente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub

            End If

        End If

        Try

            CurrentRule.Subject = txtSubject.Text
            CurrentRule.Body = txtBody.Text
            CurrentRule.Location = txtLocation.Text
            CurrentRule.ShowAppointmentForm = optForm_Mostrar.Checked

            If optFecha_SinEspecificar.Checked Then

                CurrentRule.AppointmentDateType = OLAppointmentDateType.SinEspecificar

                CurrentRule.AppointmentStartDate = String.Empty
                CurrentRule.AppointmentEndDate = String.Empty

            ElseIf optFecha_FechaFija.Checked Then

                CurrentRule.AppointmentDateType = OLAppointmentDateType.FechaFija

                CurrentRule.AppointmentStartDate = dtStartDate.Value.ToString("dd/MM/yyyy")
                CurrentRule.AppointmentStartTime = dtStartTime.Value.ToString("HH:mm")
                CurrentRule.AppointmentEndDate = dtEndDate.Value.ToString("dd/MM/yyyy")
                CurrentRule.AppointmentEndTime = dtEndTime.Value.ToString("HH:mm")

            ElseIf optFecha_TextoInteligente.Checked Then

                CurrentRule.AppointmentDateType = OLAppointmentDateType.TextoInteligente

                CurrentRule.AppointmentStartDate = txtStartDate.Text
                CurrentRule.AppointmentStartTime = txtStartTime.Text
                CurrentRule.AppointmentEndDate = txtEndDate.Text
                CurrentRule.AppointmentEndTime = txtEndTime.Text

            End If

            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Subject)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.Body)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.Location)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.AppointmentDateType)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.AppointmentStartDate)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.AppointmentStartTime)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 6, CurrentRule.AppointmentEndDate)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 7, CurrentRule.AppointmentEndTime)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 8, CurrentRule.ShowAppointmentForm)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub optFecha_NoIngresa_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles optFecha_SinEspecificar.CheckedChanged
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

        If optFecha_FechaFija.Checked Then
            dtStartDate.Enabled = True
            dtEndDate.Enabled = True
            dtStartTime.Enabled = True
            dtEndTime.Enabled = True
        ElseIf optFecha_TextoInteligente.Checked Then
            txtStartDate.Enabled = True
            txtEndDate.Enabled = True
            txtStartTime.Enabled = True
            txtEndTime.Enabled = True
        End If
    End Sub

End Class