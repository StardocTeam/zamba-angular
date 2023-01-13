Imports Zamba.Core
Imports System.Data.SqlClient

Public Class ErrorReportData

    ''' <summary>
    ''' Agrega un reporte de error
    ''' </summary>
    ''' <param name="errorReport"></param>
    ''' <param name="t"></param>
    ''' <remarks></remarks>
    Public Sub AddError(ByVal report As ErrorReport,
                        ByVal t As Transaction)
        If Server.isOracle Then

            Dim strinsert As New Text.StringBuilder

            strinsert.Append("DECLARE ")
            strinsert.Append("str NVARCHAR2(32767); ")
            strinsert.Append("BEGIN str:='")
            strinsert.Append(report.Description.Replace("'", "''"))
            strinsert.Append("'; ")
            strinsert.Append("INSERT INTO ZErrorReports (ID, SUBJECT, DESCRIPTION, STATEID, USERID, CREATED, UPDATED, COMMENTS, WINUSER, MACHINE, VERSION) VALUES(")
            strinsert.Append(report.Id)
            strinsert.Append(", ")
            strinsert.Append("'")
            strinsert.Append(report.Subject.Replace("'", "''"))
            strinsert.Append("', ")
            strinsert.Append("str")
            strinsert.Append(", ")
            strinsert.Append("1, ")
            strinsert.Append(report.UserId)
            strinsert.Append(", ")
            strinsert.Append("SYSDATE, ")
            strinsert.Append("SYSDATE, ")
            strinsert.Append("'', ")
            strinsert.Append("' ")
            strinsert.Append(report.WinUser)
            strinsert.Append("', ")
            strinsert.Append("' ")
            strinsert.Append(report.Machine)
            strinsert.Append("', ")
            strinsert.Append("' ")
            strinsert.Append(report.Version)
            strinsert.Append("');")
            strinsert.Append("END;")

            Server.Con.ExecuteNonQuery(CommandType.Text, strinsert.ToString())
        Else
            Dim params() As Object = New Object() {report.Id, report.Subject, report.Description, report.UserId, report.WinUser, report.Machine, report.Version}
            t.Con.ExecuteNonQuery(t.Transaction, "ZSP_ERROR_200_AddReport", params)
            params = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Agrega el adjunto de un reporte de error
    ''' </summary>
    ''' <param name="errorId"></param>
    ''' <param name="attach"></param>
    ''' <param name="t"></param>
    ''' <remarks></remarks>
    Public Sub AddErrorAttach(ByVal errorId As Int64,
                                    ByVal attach As ErrorReportAttachment,
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
        If Server.isOracle Then
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM ZERRORREPORTS WHERE ID>" & lastExportedReportId)

            'Todavia no esta implementado la parte de adjuntos en los errores en oracle.
            Dim dt As New DataTable()
            dt.Columns.Add("Id", GetType(Int64))
            dt.Columns.Add("ReportId", GetType(Int64))
            dt.Columns.Add("FileName", GetType(String))
            dt.Columns.Add("Attachment", GetType(Byte()))
            ds.Tables.Add(dt)

            Return ds
        Else
            Return Server.Con.ExecuteDataset("ZSP_ERRORS_100_GetReportsToExport", New Object() {lastExportedReportId})
        End If
    End Function

End Class
