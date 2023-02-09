Imports Zamba.Data
Imports System.Collections.Generic

Public Class ErrorReportBusiness

    Public Shared Adding As Boolean
    Public Sub AddErrorReport(ByVal errorReport As ErrorReport)
        If Adding = False Then
            Adding = True

            Dim errorReportingData As New ErrorReportData()
            Dim t As Transaction = Nothing

            Try
                t = New Transaction

                'Se agrega el reporte de error
                If errorReport.Id = 0 Then
                    errorReport.Id = Zamba.Data.CoreData.GetNewID(IdTypes.ErrorReport)
                End If
                errorReportingData.AddError(errorReport, t)

                'Se agregan los adjuntos del reporte de error
                For Each attach As ErrorReportAttachment In errorReport.Attachments
                    If attach.Id = 0 Then
                        attach.Id = Zamba.Data.CoreData.GetNewID(IdTypes.ErrorReportAttach)
                    End If

                    If attach.File Is Nothing Then
                        attach.File = FileEncode.Encode(attach.FileName)
                    End If
                    attach.FileName = IO.Path.GetFileName(attach.FileName)
                    ZTrace.WriteLineIf(TraceLevel.Verbose, errorReport.Description & " " & errorReport.Description)
                    errorReportingData.AddErrorAttach(errorReport.Id, attach, t)
                Next

                t.Commit()
            Catch ex As Exception
                If t IsNot Nothing AndAlso t.Transaction IsNot Nothing AndAlso t.Transaction.Connection.State <> ConnectionState.Closed Then
                    t.Rollback()
                End If
                ZTrace.WriteLineIf(TraceLevel.Error, ex.ToString())
            Finally
                Adding = False
                errorReportingData = Nothing
                If t IsNot Nothing Then
                    If t.Con IsNot Nothing Then
                        t.Con.Close()
                        t.Con.dispose()
                        t.Con = Nothing
                    End If
                    t.Dispose()
                    t = Nothing
                End If
            End Try
        End If
    End Sub

    Public Sub CleanDBErrors()
        Dim errorReportingData As New ErrorReportData()
        Try
            errorReportingData.DeleteErrors()
        Finally
            errorReportingData = Nothing          
        End Try
        End Sub

    ''' <summary>
    ''' Obtiene todos los reportes de error
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetErrorReports() As List(Of ErrorReport)
        Dim lstErrors As New List(Of ErrorReport)
        Dim errorData As New ErrorReportData
        Dim dtErrors As DataTable = errorData.GetAllErrorReports

        For Each row As DataRow In dtErrors.Rows
            lstErrors.Add(New ErrorReport(CLng(row("Id")), _
                                          row("Subject"), _
                                          row("Description"), _
                                          DirectCast(row("StateId"), ErrorReportStates), _
                                          CLng(row("UserId")), _
                                          CDate(row("Created")), _
                                          CDate(row("Updated")), _
                                          row("Comments"), _
                                          row("WinUser"), _
                                          row("Machine"), _
                                          row("Version")))
        Next

        errorData = Nothing
        dtErrors.Dispose()
        dtErrors = Nothing
        Return lstErrors
    End Function

    ''' <summary>
    ''' Obtiene los adjuntos de un reporte de error sin los bytes
    ''' </summary>
    ''' <param name="errorId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAttachments(ByVal errorId As Int64) As List(Of ErrorReportAttachment)
        Dim lstAttachs As New List(Of ErrorReportAttachment)
        Dim errorData As New ErrorReportData
        Dim dtAttachs As DataTable = errorData.GetAttachments(errorId)

        For Each row As DataRow In dtAttachs.Rows
            lstAttachs.Add(New ErrorReportAttachment(row("FileName"), CLng(row("Id"))))
        Next

        errorData = Nothing
        dtAttachs.Dispose()
        dtAttachs = Nothing
        Return lstAttachs
    End Function

    ''' <summary>
    ''' Obtiene el binario de un adjunto
    ''' </summary>
    ''' <param name="attachId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAttachment(ByVal attachId As Int64) As Byte()
        Dim errorData As New ErrorReportData
        Return errorData.GetAttachment(attachId)
        errorData = Nothing
    End Function

    ''' <summary>
    ''' Edita el estado y comentarios de un reporte de error
    ''' </summary>
    ''' <param name="errorId"></param>
    ''' <param name="errorState"></param>
    ''' <param name="comments"></param>
    ''' <remarks></remarks>
    Public Sub EditReport(ByVal errorId As Int64, ByVal errorState As ErrorReportStates, ByVal comments As String)
        Dim errorData As New ErrorReportData
        errorData.EditReport(errorId, errorState, comments)
        errorData = Nothing
    End Sub

    Public Sub AddException(ByVal ex As Exception)
        Using report As New ErrorReport(ex.Message, ex.ToString)
            AddErrorReport(report)
        End Using
    End Sub

    Public Sub AddPerformanceIssue(ByVal subject As String, ByVal description As String)
        Using report As New ErrorReport(subject, description)
            AddErrorReport(report)
        End Using
    End Sub



    ''' <summary>
    ''' Obtiene un listado de todos los reportes a exportar
    ''' </summary>
    ''' <returns>List(Of ErrorReport)</returns>
    ''' <remarks></remarks>
    Public Function GetReportsToExport() As ErrorReport()
        Dim dsReports As DataSet = GetDsReportsToExport()
        Dim lookForAttachments As Boolean = dsReports.Tables("attachments").Rows.Count > 0
        Dim reports(dsReports.Tables("reports").Rows.Count - 1) As ErrorReport
        Dim i As Int32 = 0

        For Each rowR As DataRow In dsReports.Tables("reports").Rows
            Dim report As New ErrorReport(CLng(rowR("Id")),
                                          ConvertDbNull(rowR("Subject")),
                                          rowR("Description"),
                                          DirectCast(rowR("StateId"), ErrorReportStates),
                                          CLng(rowR("UserId")),
                                          CDate(rowR("Created")),
                                          CDate(rowR("Updated")),
                                          ConvertDbNull(rowR("Comments")),
                                          ConvertDbNull(rowR("WinUser")),
                                          ConvertDbNull(rowR("Machine")),
                                          ConvertDbNull(rowR("Version")))

            If lookForAttachments Then
                For Each rowA As DataRow In dsReports.Tables("attachments").Select("ReportId=" & report.Id)
                    Dim attach As New ErrorReportAttachment()
                    attach.Id = 0 'Cero para que genere un nuevo ID
                    attach.FileName = rowA("FileName")
                    attach.File = DirectCast(rowA("Attachment"), Byte())
                    report.Attachments.Add(attach)
                Next
            End If

            reports(i) = report
            i += 1
        Next

        If dsReports IsNot Nothing Then
            dsReports.Dispose()
            dsReports = Nothing
        End If

        Return reports
    End Function

    ''' <summary>
    ''' Convierte valores de tipo DbNull en String.Empty
    ''' </summary>
    ''' <param name="o"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertDbNull(ByVal o As Object) As String
        If IsDBNull(o) Then
            Return String.Empty
        Else
            Return o.ToString
        End If
    End Function

    ''' <summary>
    ''' Obtiene un DataSet con los reportes no exportados. 
    ''' El DataSet contiene dos tablas: reports y attachments.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDsReportsToExport()
        Dim lastExportedReportId As Int64 = CLng(ZOptBusiness.GetValueOrDefault("LastExportedReportId", 0))
        Dim erd As New ErrorReportData
        Dim dsReports As DataSet = erd.GetReportsToExport(lastExportedReportId)
        dsReports.Tables(0).TableName = "reports"
        dsReports.Tables(1).TableName = "attachments"
        erd = Nothing
        Return dsReports
    End Function

End Class
