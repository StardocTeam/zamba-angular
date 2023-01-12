Imports System.Collections.Generic
Imports Zamba.Office
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
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As List(Of ITaskResult)) As List(Of Core.ITaskResult)

        'If results.Count > 1 Then
        '    For Each result As IResult In results
        '        doCreateOutlookAppointment(result)
        '    Next
        'ElseIf results.Count = 1 Then
        '    doCreateOutlookAppointment(DirectCast(results(0), ITaskResult))
        'End If

        Return results

    End Function

    'Private Sub doCreateOutlookAppointment(ByVal res As ITaskResult)

    '    Dim ol As Zamba.Office.Outlook.OutlookInterop
    '    Dim subject As String
    '    Dim location As String
    '    Dim body As String
    '    Dim startDate As DateTime
    '    Dim endDate As DateTime
    '    Dim auxFecha As String
    '    Dim auxHora As String

    '    Dim culture As CultureInfo = CultureInfo.CreateSpecificCulture("es-AR")
    '    Dim VarInterReglas As New VariablesInterReglas()
    '    Try

    '        Select Case Me._myRule.AppointmentDateType

    '            Case OLAppointmentDateType.SinEspecificar

    '                startDate = DateTime.Now
    '                endDate = DateTime.Now

    '            Case OLAppointmentDateType.FechaFija

    '                'Fecha seteada en la config de la regla
    '                startDate = DateTime.Parse(Me._myRule.AppointmentStartDate + " " + Me._myRule.AppointmentStartTime, culture, Globalization.DateTimeStyles.None)
    '                endDate = DateTime.Parse(Me._myRule.AppointmentEndDate + " " + Me._myRule.AppointmentEndTime, culture, Globalization.DateTimeStyles.None)

    '            Case OLAppointmentDateType.TextoInteligente

    '                auxFecha = TextoInteligente.ReconocerCodigo(Me._myRule.AppointmentStartDate, res)

    '                auxFecha = VarInterReglas.ReconocerVariablesValuesSoloTexto(auxFecha)

    '                auxHora = TextoInteligente.ReconocerCodigo(Me._myRule.AppointmentStartTime, res)
    '                auxHora = VarInterReglas.ReconocerVariablesValuesSoloTexto(auxHora)

    '                If String.IsNullOrEmpty(auxFecha) OrElse String.IsNullOrEmpty(auxHora) Then
    '                    Throw New Exception("La fecha y hora de inicio no pueden estar vacias")
    '                Else
    '                    startDate = DateTime.Parse(auxFecha + " " + auxHora, culture, Globalization.DateTimeStyles.None)
    '                End If

    '                auxFecha = TextoInteligente.ReconocerCodigo(Me._myRule.AppointmentEndDate, res)
    '                auxFecha = VarInterReglas.ReconocerVariablesValuesSoloTexto(auxFecha)

    '                auxHora = TextoInteligente.ReconocerCodigo(Me._myRule.AppointmentEndTime, res)
    '                auxHora = VarInterReglas.ReconocerVariablesValuesSoloTexto(auxHora)

    '                If String.IsNullOrEmpty(auxFecha) OrElse String.IsNullOrEmpty(auxHora) Then
    '                    Throw New Exception("La fecha y hora de finalizacion no pueden estar vacias")
    '                Else
    '                    endDate = DateTime.Parse(auxFecha + " " + auxHora, culture, Globalization.DateTimeStyles.None)
    '                End If

    '        End Select

    '        If Not String.IsNullOrEmpty(Me._myRule.Subject) Then
    '            subject = TextoInteligente.ReconocerCodigo(Me._myRule.Subject, res)
    '            subject = VarInterReglas.ReconocerVariablesValuesSoloTexto(subject)
    '        End If

    '        If Not String.IsNullOrEmpty(Me._myRule.Location) Then
    '            location = TextoInteligente.ReconocerCodigo(Me._myRule.Location, res)
    '            location = VarInterReglas.ReconocerVariablesValuesSoloTexto(location)
    '        End If

    '        If Not String.IsNullOrEmpty(Me._myRule.Body) Then
    '            body = TextoInteligente.ReconocerCodigo(Me._myRule.Body, res)
    '            body = VarInterReglas.ReconocerVariablesValuesSoloTexto(body)
    '        End If

    '        ol = Zamba.Office.Outlook.SharedOutlook.GetOutlook()
    '        ol.NewAppointment(subject, location, body, startDate, endDate, Me._myRule.ShowAppointmentForm)

    '    Catch ex As Exception
    '        Zamba.Core.ZClass.raiseerror(ex)
    '    Finally
    '        VarInterReglas = Nothing
    '    End Try

    'End Sub

End Class