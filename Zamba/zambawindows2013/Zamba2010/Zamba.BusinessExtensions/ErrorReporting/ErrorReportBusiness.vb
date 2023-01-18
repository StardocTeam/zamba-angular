Imports Zamba.Data
Imports Zamba.Servers
Imports System.IO
Imports Ionic.Zip
Imports System.Text

Public Class ErrorReportBusiness
    Implements IErrorReportBusiness

    ''' <summary>
    ''' Agrega un reporte de error junto a sus adjuntos
    ''' </summary>
    ''' <param name="errorReport">Reporte de error</param>
    ''' <remarks>Se realiza todo dentro de en una transacción</remarks>
    Public Sub AddErrorReport(ByVal errorReport As IErrorReport) Implements IErrorReportBusiness.AddErrorReport
        Dim errorReportingData As New ErrorReportData()
        Dim t As Transaction = Nothing

        Try
            t = New Transaction(Server.Con(False))

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
                attach.FileName = Path.GetFileName(attach.FileName)

                errorReportingData.AddErrorAttach(errorReport.Id, attach, t)
            Next

            t.Commit()
        Catch ex As Exception
            If t IsNot Nothing AndAlso t.Transaction IsNot Nothing AndAlso t.Transaction.Connection.State <> ConnectionState.Closed Then
                t.Rollback()
            End If
            Throw New Exception("Ha ocurrido un error al generar un reporte de error de Zamba Software en Windows: " + ex.ToString() + "Error interno: " + ex.InnerException.ToString + "Mensaje de excepcion: " + ex.Message, ex)

        Finally
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
    End Sub

    ''' <summary>
    ''' Obtiene todos los reportes de error
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetErrorReports() As List(Of IErrorReport) Implements IErrorReportBusiness.GetErrorReports
        Dim lstErrors As New List(Of IErrorReport)
        Dim errorData As New ErrorReportData
        Dim dtErrors As DataTable = errorData.GetAllErrorReports

        For Each row As DataRow In dtErrors.Rows
            lstErrors.Add(New ErrorReport(row("Id"),
                                          ConvertDbNull(row("Subject")),
                                          row("Description"),
                                          row("StateId"),
                                          row("UserId"),
                                          row("Created"),
                                          row("Updated"),
                                          ConvertDbNull(row("Comments")),
                                          ConvertDbNull(row("WinUser")),
                                          ConvertDbNull(row("Machine")),
                                          ConvertDbNull(row("Version"))))
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
    Public Function GetAttachments(ByVal errorId As Int64) As List(Of IErrorReportAttachment) Implements IErrorReportBusiness.GetAttachments
        Dim lstAttachs As New List(Of IErrorReportAttachment)
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
    Public Function GetAttachment(ByVal attachId As Int64) As Byte() Implements IErrorReportBusiness.GetAttachment
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
    Public Sub EditReport(ByVal errorId As Int64, ByVal errorState As ErrorReportStates, ByVal comments As String) Implements IErrorReportBusiness.EditReport
        Dim errorData As New ErrorReportData
        errorData.EditReport(errorId, errorState, comments)
        errorData = Nothing
    End Sub

    ''' <summary>
    ''' Agrega una Exception de Zamba como reporte
    ''' </summary>
    ''' <param name="ex"></param>
    ''' <remarks></remarks>
    Public Sub AddException(ByVal ex As Exception) Implements IErrorReportBusiness.AddException
        If Server.ConInitialized = True Then
            Using report As New ErrorReport(ex.Message, ex.ToString)
                AddErrorReport(report)
            End Using

            '            Dim ZipFile As String = AttachLogsToZip()


        End If
    End Sub

    Private Shared Function SendMail(ByVal ZipFile As String) As Boolean
        Try

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally

        End Try

    End Function


    Const TIMELAPSEERROR As Int32 = 7

    Private _DBWriter As IDBWriter
    Public Property DBWriter As IDBWriter Implements IErrorReportBusiness.DBWriter
        Get
            If _DBWriter Is Nothing Then
                _DBWriter = New DBWriter()
            End If
            Return _DBWriter
        End Get
        Set(value As IDBWriter)
            _DBWriter = value
        End Set
    End Property

    ''' <summary>
    ''' Genera un archivo zip de los logs de zamba y lo adjunta para enviar por mail
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function AttachLogsToZip() As String
        Dim zipPath As String = String.Empty
        Dim extraDataFileName As String = String.Empty

        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando los logs para adjuntarlos al reporte.")

            'Obtiene los trace y exceptions que se hayan escrito en la última media hora
            Dim directorySelected As DirectoryInfo = Tools.EnvironmentUtil.GetTempDir("\Exceptions\")
            zipPath = FileBusiness.GetUniqueFileName(directorySelected.FullName, "Exceptions", "zip")

            extraDataFileName = FileBusiness.GetUniqueFileName(directorySelected.FullName, "ReportExtraData", "txt")

            Dim _Files As New List(Of String)

            For Each file As FileInfo In directorySelected.GetFiles()
                If file.LastWriteTime > Now.AddDays(-TIMELAPSEERROR) AndAlso Not _
                                ((file.Name.StartsWith("ScreenCapture") AndAlso String.Compare(file.Extension, ".jpeg") = 0) OrElse
                                (file.Name.StartsWith("Exceptions") AndAlso String.Compare(file.Extension, ".zip") = 0) OrElse
                                (file.Name.StartsWith("ReportExtraData") AndAlso String.Compare(file.Extension, ".txt") = 0)) Then
                    _Files.Add(file.FullName)
                End If
            Next

            For Each dir As DirectoryInfo In directorySelected.GetDirectories()
                For Each _file As String In Directory.GetFiles(dir.FullName.ToString())
                    Dim file As New FileInfo(_file)
                    If file.LastWriteTime > Now.AddDays(-TIMELAPSEERROR) AndAlso Not _
                               ((file.Name.StartsWith("ScreenCapture") AndAlso String.Compare(file.Extension, ".jpeg") = 0) OrElse
                               (file.Name.StartsWith("Exceptions") AndAlso String.Compare(file.Extension, ".zip") = 0) OrElse
                               (file.Name.StartsWith("ReportExtraData") AndAlso String.Compare(file.Extension, ".txt") = 0)) Then
                        _Files.Add(file.FullName)
                    End If
                Next
            Next

            'Dim files = From c In directorySelected.GetFiles()
            '            Where c.LastWriteTime > Now.AddDays(-TIMELAPSEERROR) AndAlso Not _
            '            ((c.Name.StartsWith("ScreenCapture") AndAlso String.Compare(c.Extension, ".jpeg") = 0) OrElse
            '            (c.Name.StartsWith("Exceptions") AndAlso String.Compare(c.Extension, ".zip") = 0) OrElse
            '            (c.Name.StartsWith("ReportExtraData") AndAlso String.Compare(c.Extension, ".txt") = 0))
            '            Select c.FullName

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Comenzando la compresión de los logs.")
            Using zipLib As New ZipFile()
                If GenerateExtraData(extraDataFileName) Then
                    zipLib.AddFile(extraDataFileName, String.Empty)
                End If

                zipLib.AddFiles(_Files, String.Empty)
                zipLib.Save(zipPath)
            End Using

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Adjuntando los logs al reporte de error.")

            directorySelected = Nothing
            _Files = Nothing
            Return zipPath
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, "Ha ocurrido un error al detener los logs.")
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Genera un bloque de información sobre el usuario de zamba, usuario de windows, entorno del usuario y preferencias del usuario de Zamba.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Function GenerateExtraData(ByVal extraDataFileName As String) As Boolean
        Dim sbExtraData As New StringBuilder

        Try
            With sbExtraData
                .Append("# Usuario de Zamba: ")
                .AppendLine(Membership.MembershipHelper.CurrentUser.Name)
                .Append("# ID usuario de Zamba: ")
                .AppendLine(Membership.MembershipHelper.CurrentUser.ID)
                .Append("# Versión de Zamba: ")
                .AppendLine(Application.ProductVersion.ToString)
                .Append("# Usuario de Windows: ")
                .AppendLine(My.User.Name)
                .Append("# Puesto: ")
                .AppendLine(Environment.MachineName)
                .AppendLine("# Sistema Operativo:")
                .AppendLine(vbTab & Environment.OSVersion.VersionString)
                .AppendLine(vbTab & My.Computer.Info.OSFullName)
                .AppendLine(vbTab & My.Computer.Info.OSPlatform & " " & My.Computer.Info.OSVersion)
                .Append("# Lenguaje SO: ")
                .AppendLine(My.Computer.Info.InstalledUICulture.NativeName)
                .Append("# Código de lenguaje: ")
                .AppendLine(My.Computer.Info.InstalledUICulture.ToString)
                .AppendLine("# Formato SO:")
                .AppendLine(vbTab & "Separador de decimales: " & My.Computer.Info.InstalledUICulture.NumberFormat.CurrencyDecimalSeparator)
                .AppendLine(vbTab & "Separador de miles: " & (My.Computer.Info.InstalledUICulture.NumberFormat.CurrencyGroupSeparator))
                .Append("# Versión de Office: ")
                .AppendLine([Enum].GetName(GetType(Tools.EnvironmentUtil.OfficeVersions), Tools.EnvironmentUtil.GetOfficeVersion()))
                .AppendLine()

                Try
                    Dim conDataTools As New DBTools
                    Dim conData As ArrayList = conDataTools.GetActualConfig
                    conDataTools = Nothing
                    .AppendLine("# Conexión de zamba")
                    .Append("Servidor: ")
                    .AppendLine(conData(0))
                    .Append("Base de datos: ")
                    .AppendLine(conData(1))
                    .Append("Usuario: ")
                    .AppendLine(conData(2))
                    .Append("Variable WIN_AUTHENTICATION: ")
                    .AppendLine(conData(4))
                    conData.Clear()
                    conData = Nothing
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    .AppendLine("Ocurrió un error al obtener la configuración de conexión: ")
                    .AppendLine(ex.Message)
                Finally
                    .AppendLine()
                End Try

                'Try
                '    Dim userPreferenceExt As New UserPreferencesExt
                '    Dim lstPreferences As List(Of String) = userPreferenceExt.GetUserPreferencesForPrint()
                '    userPreferenceExt = Nothing
                '    .AppendLine("# Preferencias de usuario:")
                '    For i As Int32 = 0 To lstPreferences.Count - 1
                '        .AppendLine(lstPreferences(i))
                '    Next
                '    lstPreferences = Nothing
                'Catch ex As Exception
                '    ZClass.raiseerror(ex)
                '    .AppendLine("Ocurrió un error al obtener las preferencias de usuario: ")
                '    .AppendLine(ex.Message)
                'End Try
            End With

            'Se guarda el log en un archivo
            Using sw As New StreamWriter(extraDataFileName)
                sw.Write(sbExtraData.ToString())
                sw.Close()
            End Using

            Return True
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, "Ha ocurrido un error al detener los logs.")
            ZClass.raiseerror(ex)
            Return False

        Finally
            sbExtraData = Nothing
        End Try
    End Function


    ''' <summary>
    ''' Agrega los logs de problemas de performance sql como reporte
    ''' </summary>
    ''' <param name="subject"></param>
    ''' <param name="description"></param>
    ''' <remarks></remarks>
    Public Sub AddPerformanceIssue(ByVal subject As String, ByVal description As String) Implements IErrorReportBusiness.AddPerformanceIssue
        Using report As New ErrorReport(subject, description)
            AddErrorReport(report)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene un listado de todos los reportes a exportar
    ''' </summary>
    ''' <returns>List(Of ErrorReport)</returns>
    ''' <remarks></remarks>
    Public Function GetReportsToExport() As IErrorReport() Implements IErrorReportBusiness.GetReportsToExport
        Dim dsReports As DataSet = GetDsReportsToExport()
        Dim lookForAttachments As Boolean = dsReports.Tables("attachments").Rows.Count > 0
        'Dim reports As List(Of ErrorReport)
        Dim reports(dsReports.Tables("reports").Rows.Count - 1) As ErrorReport
        Dim i As Int32 = 0

        For Each rowR As DataRow In dsReports.Tables("reports").Rows
            Dim report As New ErrorReport(rowR("Id"),
                                          ConvertDbNull(rowR("Subject")),
                                          rowR("Description"),
                                          rowR("StateId"),
                                          rowR("UserId"),
                                          rowR("Created"),
                                          rowR("Updated"),
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

            reports(i) = (report)
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
        Dim lastExportedReportId As Long = ZOptBusiness.GetValueOrDefault("LastExportedReportId", 0)
        Dim erd As New ErrorReportData
        Dim dsReports As DataSet = erd.GetReportsToExport(lastExportedReportId)
        dsReports.Tables(0).TableName = "reports"
        dsReports.Tables(1).TableName = "attachments"
        erd = Nothing
        Return dsReports
    End Function

    Public Sub SendException(ex As Exception) Implements IErrorReportBusiness.SendException

    End Sub

    Public Sub SendPerformanceIssue(subject As String, description As String) Implements IErrorReportBusiness.SendPerformanceIssue

    End Sub
End Class
