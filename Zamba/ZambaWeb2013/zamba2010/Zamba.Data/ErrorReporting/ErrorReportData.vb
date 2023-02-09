Imports Zamba.Core
Imports Zamba.Servers
Imports System.Data.SqlClient

Public Class ErrorReportData

    ''' <summary>
    ''' Agrega un reporte de error
    ''' </summary>
    ''' <param name="errorReport"></param>
    ''' <param name="t"></param>
    ''' <remarks></remarks>
    Public Sub AddError(ByVal report As ErrorReport, _
                        ByVal t As Transaction)
        Dim datestring As String = "GETDATE()"
        If Server.isOracle Then
            datestring = "SYSDATE()"
        End if
        Dim params () as Object = New Object() {report.Id, report.Subject, report.Description, report.UserId, report.WinUser, report.Machine, report.Version}
        t.Con.ExecuteNonQuery(t.Transaction,"ZSP_ERROR_200_AddReport", params)
    '    t.Con.ExecuteNonQuery(CommandType.Text, String.Format(" INSERT INTO ZErrorReports
    '  VALUES({0}, '{1}','{2}', 1, {3}, {7},{7}, '', '{4}', '{5}', '{6}')",report.Id, report.Subject, report.Description, report.UserId, report.WinUser, report.Machine, report.Version, datestring))
    End Sub

      Public Sub DeleteErrors()
        Dim datestring As String = "GETDATE()-30"
        If Server.isOracle Then
            datestring = "SYSDATE()-30"
        End if

        Server.Con.ExecuteNonQuery(CommandType.Text, "delete ZErrorReportAttachments where ID in (select id from ZErrorReports where Updated < (GETDATE()-30))")
        Server.Con.ExecuteNonQuery(CommandType.Text, "delete ZErrorReports where Updated < (GETDATE()-30)")
    End Sub

    ''' <summary>
    ''' Agrega el adjunto de un reporte de error
    ''' </summary>
    ''' <param name="errorId"></param>
    ''' <param name="attach"></param>
    ''' <param name="t"></param>
    ''' <remarks></remarks>
    Public Sub AddErrorAttach(ByVal errorId As Int64, _
                                    ByVal attach As ErrorReportAttachment, _
                                    ByVal t As Transaction)
        If Server.isOracle Then
            Throw New NotImplementedException()
        Else
            Dim pId As SqlParameter
            pId = New SqlParameter("@id", SqlDbType.Int)
            pId.Value = attach.Id

            Dim pErrorId As SqlParameter
            pErrorId = New SqlParameter("@errorId", SqlDbType.Int)
            pErrorId.Value = errorId

            Dim pFileName As SqlParameter
            pFileName = New SqlParameter("@fileName", SqlDbType.NVarChar, 256)
            pFileName.Value = attach.FileName

            Dim pAttachment As SqlParameter
            Dim sql_version As String = Server.Con.ExecuteScalar(CommandType.Text, "select @@version")
            If sql_version.StartsWith("Microsoft SQL Server  2000") Then
                pAttachment = New SqlParameter("@attachment", SqlDbType.Image)
            Else
                pAttachment = New SqlParameter("@attachment", SqlDbType.VarBinary)
            End If
            pAttachment.Value = attach.File

            Dim params As IDbDataParameter() = {pId, pErrorId, pFileName, pAttachment}

            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, "INSERT INTO ZErrorReportAttachments VALUES(@id, @errorId, @fileName, @attachment)", params)
            pId = Nothing
            pErrorId = Nothing
            pFileName = Nothing
            pAttachment = Nothing
            params = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Obtiene todos los reportes de error sin los adjuntos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllErrorReports() As DataTable
        If Server.isOracle Then
            Throw New NotImplementedException()
        Else
            Return Server.Con.ExecuteDataset("ZSP_ERROR_100_GetReports").Tables(0)
        End If
    End Function

    ''' <summary>
    ''' Obtiene los adjuntos de un reporte de error sin los bytes
    ''' </summary>
    ''' <param name="errorId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAttachments(ByVal errorId As Int64) As DataTable
        If Server.isOracle Then
            Throw New NotImplementedException()
        Else
            Return Server.Con.ExecuteDataset("ZSP_ERROR_100_GetReportAttachments", New Object() {errorId}).Tables(0)
        End If
    End Function

    ''' <summary>
    ''' Obtiene el binario de un adjunto
    ''' </summary>
    ''' <param name="attachId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAttachment(ByVal attachId As Int64) As Byte()
        If Server.isOracle Then
            Throw New NotImplementedException()
        Else
            Return Server.Con.ExecuteScalar("ZSP_ERROR_100_GetAttachmentFile", New Object() {attachId})
        End If
    End Function

    ''' <summary>
    ''' Edita el estado y comentarios de un reporte de error
    ''' </summary>
    ''' <param name="errorId"></param>
    ''' <param name="errorState"></param>
    ''' <param name="comments"></param>
    ''' <remarks></remarks>
    Public Sub EditReport(ByVal errorId As Int64, ByVal errorState As ErrorReportStates, ByVal comments As String)
        If Server.isOracle Then
            Throw New NotImplementedException()
        Else
            Server.Con.ExecuteNonQuery("ZSP_ERROR_100_EditReport", New Object() {errorId, errorState, comments})
        End If
    End Sub

    ''' <summary>
    ''' Obtiene un DataSet con los reportes no exportados. 
    ''' </summary>
    Public Function GetReportsToExport(ByVal lastExportedReportId As Int64) As DataSet
        Return Server.Con.ExecuteDataset("ZSP_ERRORS_100_GetReportsToExport", New Object() {lastExportedReportId})
    End Function

End Class
