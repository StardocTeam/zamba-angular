Imports System.Globalization

''' <summary>
''' [AlejandroR] - Created - 28/12/09
''' Regla que permite crear un nuevo appointment en Outlook de manera automatica o
''' dejando que el usuario vea el formulario y realice los cambios que necesite.
''' </summary>
''' <remarks></remarks>

Public Class PlayDOCreateOutlookAppointment

    Private _myRule As IDOCreateOutlookAppointment

    Sub New(ByVal rule As IDOCreateOutlookAppointment)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As List(Of ITaskResult)) As List(Of Core.ITaskResult)

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

        Dim ol As Zamba.Office.Outlook.OutlookInterop
        Dim subject As String
        Dim location As String
        Dim body As String
        Dim startDate As DateTime
        Dim endDate As DateTime
        Dim auxFecha As String
        Dim auxHora As String

        Dim culture As CultureInfo = CultureInfo.CreateSpecificCulture("es-AR")

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
                        Throw New Exception("La fecha y hora de inicio no pueden estar vacias")
                    Else
                        startDate = DateTime.Parse(auxFecha + " " + auxHora, culture, Globalization.DateTimeStyles.None)
                    End If

                    auxFecha = TextoInteligente.ReconocerCodigo(_myRule.AppointmentEndDate, res)
                    auxFecha = WFRuleParent.ReconocerVariablesValuesSoloTexto(auxFecha)

                    auxHora = TextoInteligente.ReconocerCodigo(_myRule.AppointmentEndTime, res)
                    auxHora = WFRuleParent.ReconocerVariablesValuesSoloTexto(auxHora)

                    If String.IsNullOrEmpty(auxFecha) OrElse String.IsNullOrEmpty(auxHora) Then
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

            ol = Zamba.Office.Outlook.SharedOutlook.GetOutlook()
            ol.NewAppointment(subject, location, body, startDate, endDate, _myRule.ShowAppointmentForm)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

End Class