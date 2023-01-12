Imports Zamba.Data
Imports System.Collections.Generic

Public Class AgentBusiness

    ''' <summary>
    ''' Guarda la cantidad de licencias habilitadas en un cliente
    ''' </summary>
    ''' <param name="client">Cliente. El cliente configurado en el agent debe ser igual al de la tabla Clients.</param>
    ''' <param name="licenseType">Tipo de licencia. El id del enumerador debe ser igual al de la tabla Licenses.</param>
    ''' <param name="count">Cantidad de licencias. Debe estar desencriptado.</param>
    ''' <remarks></remarks>
    Public Function SaveEnabledLicCount(ByVal client As String, ByVal licenseType As Int32, ByVal count As String) As String
        Dim afe As New AgentFactoryExt()

        Try
            Dim updated As Integer = afe.SaveEnabledLicCount(client, licenseType, count)

            'Verifica si pudo realizar modificaciones
            If updated > 0 Then
                Return String.Empty
            Else
                Return "Error al actualizar las licencias habilitadas. Verifique haber configurado correctamente el campo nombre en el servicio. Debe coincidir con los provistos por Stardoc."
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Trace.WriteLine(ex.ToString())
            Return "Error al guardar las licencias habilitadas. Ex: " + ex.ToString()
        Finally
            afe = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda el reporte de licencias de exportacion
    ''' </summary>
    ''' <param name="dsIlm">Reporte de licencias</param>
    ''' <param name="client">Cliente</param>
    ''' <returns>Resultado de la operación. Es un mensaje.</returns>
    ''' <remarks></remarks>
    Public Function SaveILM(dsIlm As DataSet, client As String) As String
        If dsIlm IsNot Nothing Then
            If dsIlm.Tables.Count > 0 AndAlso dsIlm.Tables(0).Rows.Count > 0 Then
                Dim afe As New AgentFactoryExt()
                Dim addedRows As Integer = 0
                Dim totalRows As Integer
                Dim userId As String
                Dim docTypeId As String
                Dim crdate As DateTime
                Dim year As String
                Dim month As String
                Dim day As String
                Dim hour As String
                Dim codigoMail As String = String.Empty
                Dim docId As String = "0"
                Dim userName As String = String.Empty

                Try
                    totalRows = dsIlm.Tables(0).Rows.Count
                    With dsIlm.Tables(0)
                        Dim colCTime As Integer = .Columns("Fecha").Ordinal
                        Dim colUserId As Integer = .Columns("UserId").Ordinal
                        Dim colDocTypeId As Integer = .Columns("DOC_TYPE_ID").Ordinal
                        Dim colUserName As Int32 = .Columns("Name").Ordinal

                        For Each R As DataRow In .Rows
                            crdate = DateTime.Parse(R(colCTime).ToString())
                            userId = R(colUserId).ToString()
                            docTypeId = R(colDocTypeId).ToString()
                            year = crdate.Year.ToString()
                            month = crdate.Month.ToString()
                            day = crdate.Day.ToString()
                            hour = crdate.Hour.ToString()
                            userName = R(colUserName).ToString()

                            afe.AddILMClientData(userId, userName, year, month, day, hour, crdate.ToString(), client, codigoMail, docId, docTypeId)
                            addedRows += 1
                        Next
                    End With

                    Return "Registros ILM Insertados correctamente: " + addedRows.ToString + "/" + totalRows.ToString
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                    ZTrace.WriteLineIf(ZTrace.IsError, "Registros ILM Insertados: " + addedRows.ToString + "/" + totalRows.ToString + ". Error: " + ex.ToString() + " Error interno " + ex.InnerException.ToString)
                    Return String.Empty
                Finally
                    afe = Nothing
                End Try
            Else
                Return "Registros Insertados: 0. La tabla se encontraba sin registros."
            End If
        Else
            Return "Registros Insertados: 0. La tabla se encontraba nula."
        End If
    End Function

    ''' <summary>
    ''' Guarda el reporte de licencias documentales y de workflow
    ''' </summary>
    ''' <param name="dsUcm">Reporte</param>
    ''' <param name="client">Cliente</param>
    ''' <param name="dsDate">Fecha de reporte</param>
    ''' <param name="server">Servidor del cliente</param>
    ''' <param name="dataBase">Base de datos del cliente</param>
    ''' <returns>Resultado de la operación. Es un mensaje.</returns>
    ''' <remarks></remarks>
    Public Function SaveUCM(dsUcm As DataSet, client As String, dsDate As DateTime, server As String, dataBase As String) As String
        If dsUcm IsNot Nothing AndAlso dsUcm.Tables.Count > 0 AndAlso dsUcm.Tables(0).Rows.Count > 0 Then
            Dim afe As New AgentFactoryExt()
            Dim addedRows As Integer = 0
            Dim totalRows As Integer
            Dim cTime As String
            Dim uTime As String
            Dim userId As String
            Dim winUser As String
            Dim winPC As String
            Dim conId As String
            Dim timeOut As String
            Dim type As String

            Try
                totalRows = dsUcm.Tables(0).Rows.Count
                With dsUcm.Tables(0)
                    Dim colUserId As Integer = .Columns("ID").Ordinal
                    Dim colCTime As Integer = .Columns("C_TIME").Ordinal
                    Dim colUTime As Integer = .Columns("U_TIME").Ordinal
                    Dim colWinUser As Integer = .Columns("WINUSER").Ordinal
                    Dim colWinPC As Integer = .Columns("WINPC").Ordinal
                    Dim colConId As Integer = .Columns("CON_ID").Ordinal
                    Dim colTimeOut As Integer = .Columns("TIME_OUT").Ordinal
                    Dim colType As Integer = .Columns("TYPE").Ordinal

                    For Each R As DataRow In .Rows
                        userId = R(colUserId).ToString()
                        cTime = R(colCTime).ToString()
                        uTime = R(colUTime).ToString()
                        winUser = R(colWinUser).ToString()
                        winPC = R(colWinPC).ToString()
                        conId = R(colConId).ToString()
                        timeOut = R(colTimeOut).ToString()
                        type = R(colType).ToString()

                        afe.AddUcmClientData(userId, cTime, uTime, winUser, winPC, conId,
                         timeOut, type, client, dsDate, server, dataBase)
                        addedRows += 1
                    Next
                End With

                Return "Registros UCM Insertados correctamente: " + addedRows.ToString + "/" + totalRows.ToString
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                ZTrace.WriteLineIf(ZTrace.IsError, "Registros UCM Insertados: " + addedRows.ToString + "/" + totalRows.ToString + ". Error: " + ex.ToString() + " Error interno " + ex.InnerException.ToString)
                Return String.Empty
            Finally
                afe = Nothing
            End Try
        Else
            Return "Registros Insertados: 0. La tabla se encontraba vacía."
        End If
    End Function

    ''' <summary>
    ''' Guarda un listado de reportes de error y performance
    ''' </summary>
    ''' <param name="reports"></param>
    ''' <param name="client"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveErrorReports(reports As ErrorReport(), client As String) As String
        If reports IsNot Nothing Then
            If reports.Count > 0 Then
                Dim addedRows, totalRows As Int32
                Dim erb As New ErrorReportBusiness

                Try
                    addedRows = 0
                    totalRows = reports.Count

                    For Each report As ErrorReport In reports
                        'El cliente es agregado en la columna Comentarios.
                        If report.Comments.Length = 0 Then
                            report.Comments = client
                        Else
                            report.Comments = client & "; " & report.Comments
                        End If

                        'Cero para que genere un nuevo ID
                        report.Id = 0

                        erb.AddErrorReport(report)
                        addedRows += 1
                    Next

                    Return "Registros Insertados correctamente: " + addedRows.ToString + "/" + totalRows.ToString
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                    ZTrace.WriteLineIf(ZTrace.IsError, "Registros Insertados: " + addedRows.ToString + "/" + totalRows.ToString + ". Error: " + ex.ToString() + " Error interno " + ex.InnerException.ToString)
                    Return String.Empty
                Finally
                    erb = Nothing
                End Try
            Else
                Return "Registros Insertados: 0. La tabla se encontraba sin registros."
            End If
        Else
            Return "Registros Insertados: 0. La tabla se encontraba nula."
        End If
    End Function

End Class
