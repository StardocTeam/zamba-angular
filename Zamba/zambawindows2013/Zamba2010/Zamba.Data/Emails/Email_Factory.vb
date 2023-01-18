Imports Zamba.Core
Imports System.IO
Imports System.Text
Imports System.Data.SqlClient

Public NotInheritable Class Email_Factory

    Inherits ZClass

    Public Shared Function getExportBody() As Boolean
        Dim Resul As Boolean = False
        Try
            Dim Strselect As String = "Select Value from Zopt where Item = 'EXPORT_EMAIL_BODY'"
            Boolean.TryParse(Server.Con.ExecuteScalar(CommandType.Text, Strselect), Resul)
        Catch ex As Exception
            raiseerror(ex)
        End Try
        Return Resul
    End Function

    Public Shared Sub SaveExportBody(ByVal value As Boolean)
        Try
            Dim Str As String = "Select Value from Zopt where Item = 'EXPORT_EMAIL_BODY'"

            If Server.Con.ExecuteScalar(CommandType.Text, Str) Is Nothing Then
                Str = "insert into Zopt (item,value) Values ('EXPORT_EMAIL_BODY', '" & value.ToString() & "')"
            Else
                Str = "Update Zopt set Value = '" & value.ToString() & "' where item = 'EXPORT_EMAIL_BODY'"
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, Str)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Public Shared Function getExportDoc() As Boolean
        Dim Resul As Boolean = False
        Try
            Dim Strselect As String = "Select Value from Zopt where Item = 'EXPORT_EMAIL_DOC'"
            Boolean.TryParse(Server.Con.ExecuteScalar(CommandType.Text, Strselect), Resul)
        Catch ex As Exception
            raiseerror(ex)
        End Try
        Return Resul
    End Function

    Public Shared Sub SaveExportDoc(ByVal value As Boolean)
        Try
            Dim Str As String = "Select Value from Zopt where Item = 'EXPORT_EMAIL_DOC'"

            If Server.Con.ExecuteScalar(CommandType.Text, Str) Is Nothing Then
                Str = "insert into Zopt (item,value) Values ('EXPORT_EMAIL_DOC', '" & value.ToString() & "')"
            Else
                Str = "Update Zopt set Value = '" & value.ToString() & "' where item = 'EXPORT_EMAIL_DOC'"
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, Str)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub SaveMailHistory(ByVal value As Boolean)
        Try
            Dim Str As String = "Select Value from Zopt where Item = 'MailHistoryEnabled'"

            If Server.Con.ExecuteScalar(CommandType.Text, Str) Is Nothing Then
                Str = "insert into Zopt (item,value) Values ('MailHistoryEnabled', '" & value.ToString() & "')"
            Else
                Str = "Update Zopt set Value = '" & value.ToString() & "' where item = 'MailHistoryEnabled'"
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, Str)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Public Shared Function GetEmailExportPath() As String
        Try
            Dim Strselect As String = "Select Value from Zopt where Item = 'EMAILSPATH'"
            Return Server.Con.ExecuteScalar(CommandType.Text, Strselect)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se obtiente ruta de insersion de mail: " & Strselect)
        Catch ex As Exception
            raiseerror(ex)
            Return String.Empty
        End Try
    End Function

    Public Shared Function GetMailHistoryEnabled() As Boolean
        Dim res As Boolean = False
        Try
            Dim Strselect As String = "Select Value from Zopt where Item = 'MailHistoryEnabled'"
            Boolean.TryParse(Server.Con.ExecuteScalar(CommandType.Text, Strselect), res)
        Catch ex As Exception
            raiseerror(ex)
        End Try
        Return res
    End Function

    Public Shared Sub SaveEmailExportPath(ByVal path As String)
        Try
            Dim Strinsert As String =
            "insert into Zopt (item,value) Values ('EMAILSPATH', '" & path & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub UpdateEmailExportPath(ByVal path As String)
        Try
            Dim Strupdate As String =
            "Update Zopt set Value = '" & path & "' where item = 'EMAILSPATH'"
            Server.Con.ExecuteNonQuery(CommandType.Text, Strupdate)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Public Shared Function getHistory(ByVal DocId As Long) As DataSet

        Return Server.Con.ExecuteDataset(CommandType.Text, String.Format("SELECT H.Fecha, USR.NAME Usuario, H.MSG_TO Para, H.MSG_CC CC, H.MSG_BCC BCC, H.MSG_SUBJECT Asunto, H.PATH , H.ID FROM ZMAIL_HISTORY H INNER JOIN USRTABLE USR ON H.USR_ZMB = USR.ID WHERE H.DOC_ID = {0} ORDER BY H.FECHA DESC", DocId))

    End Function

    Public Shared Function SaveHistory(ByVal para As String,
                                        ByVal cc As String,
                                        ByVal cco As String,
                                        ByVal subject As String,
                                        ByVal body As String,
                                        ByVal attachs As Generic.List(Of String),
                                        ByVal docId As Int64,
                                        ByVal docTypeId As Int64,
                                        ByVal user As IUser,
                                        ByVal message As String, ByVal UseBlobMails As Boolean) As Boolean

        Dim bResul As Boolean = False
        Dim bSaved As Boolean = False
        Dim userId As Long = user.ID
        Dim pcName As String = user.puesto
        Dim exportPath As String
        Dim fullPath As String = String.Empty


        ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando historial de Mail")
        Try

            If UseBlobMails And Server.isSQLServer Then
                'TODO: ver como se va a manejar con los atachments en blob.
                Dim filebytes As Byte()

                If message.Length > 0 Then
                    Try
                        Using fs As New FileStream(message, FileMode.Open, FileAccess.Read)
                            filebytes = New Byte(fs.Length - 1) {}
                            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length))
                            fs.Close()
                        End Using
                    Catch
                        filebytes = Encoding.Unicode.GetBytes(body)
                    End Try
                Else
                    filebytes = Encoding.Unicode.GetBytes(body)
                End If

                'Guardar el historial en la base               
#Region "SQL"
                Dim sql_version As String = Server.Con.ExecuteScalar(CommandType.Text, "select @@version")
                Dim pDocFile As SqlParameter
                If sql_version.StartsWith("Microsoft SQL Server  2000") Then
                    pDocFile = New SqlParameter("@m_fileBlob", SqlDbType.Image)
                Else
                    pDocFile = New SqlParameter("@m_fileBlob", SqlDbType.VarBinary)
                End If
                pDocFile.Value = filebytes

                Dim pUserId As SqlParameter = New SqlParameter("@m_usr_zmb", SqlDbType.Int)
                pUserId.Value = userId

                Dim pPCName As SqlParameter = New SqlParameter("@m_usr_pc", SqlDbType.VarChar, 200)
                pPCName.Value = String.Empty

                Dim pDocId As SqlParameter = New SqlParameter("@m_doc_id", SqlDbType.Int)
                pDocId.Value = docId

                Dim pDocTypeId As SqlParameter = New SqlParameter("@m_doc_type", SqlDbType.Int)
                pDocTypeId.Value = docTypeId

                Dim pPara As SqlParameter = New SqlParameter("@m_to", SqlDbType.VarChar, 200)
                pPara.Value = para

                Dim pCC As SqlParameter = New SqlParameter("@m_CC", SqlDbType.VarChar, 200)
                pCC.Value = cc

                Dim pCCO As SqlParameter = New SqlParameter("@m_bcc", SqlDbType.VarChar, 200)
                pCCO.Value = cco

                Dim pAsunto As SqlParameter = New SqlParameter("@m_subject", SqlDbType.VarChar, 200)
                pAsunto.Value = subject

                Dim pFullPath As SqlParameter = New SqlParameter("@m_path", SqlDbType.VarChar, 200)
                If Not String.IsNullOrEmpty(fullPath) Then
                    pFullPath.Value = fullPath
                Else
                    If message.Length > 0 Then
                        pFullPath.Value = "\" & docId & ".msg"
                    Else
                        pFullPath.Value = "\" & docId & ".html"
                    End If
                End If

                Dim parvalues As IDbDataParameter() = {pUserId, pPCName, pDocId, pDocTypeId, pPara, pCC, pCCO, pAsunto, pFullPath, pDocFile}
                Server.Con.ExecuteNonQuery(CommandType.StoredProcedure, "ZSP_ZMAIL_HISTORY_200_InsertarBlob", parvalues)

                parvalues = Nothing
                pUserId = Nothing
                pPCName = Nothing
                pDocId = Nothing
                pDocTypeId = Nothing
                pPara = Nothing
                pCC = Nothing
                pCCO = Nothing
                pAsunto = Nothing
                pFullPath = Nothing
                pDocFile = Nothing
                filebytes = Nothing
#End Region

            Else

                If getExportDoc() AndAlso docId > 0 Then
                    exportPath = CreateExportFolder(docId)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ruta exportacion: " & exportPath)

                    If String.IsNullOrEmpty(message) Then
                        'Guardar los attachs y el body
                        bSaved = SaveHistoryFiles(docId, body, attachs, exportPath)
                        fullPath = exportPath & "\body.html"
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Exportado archivo a: " & fullPath)

                    Else
                        'domail, se guardo el msg en forma local, copiarlo al servidor
                        If message.Contains("\OutlookMail") Then
                            fullPath = exportPath & "\" & docId & ".msg"
                            File.Copy(message, fullPath, True)

                            bSaved = True
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se exporto el archivo desde " & message & " a " & fullPath)

                        Else
                            'dogenerateoutlook, ya esta copiado el msg al servidor
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se paso como ruta del archivo: " & message)
                            If File.Exists(message) Then
                                bSaved = True
                                fullPath = message
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo no existe")
                            End If
                        End If
                    End If
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se guarda archivo fisico")
                End If

                If docId > 0 Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando historial en el documento de Zamba: " & docId)

                    'Verifica que no existan posibles datos nulos
                    Dim parvalues As String()

                    If IsNothing(cc) Then
                        cc = String.Empty
                    Else cc = cc.Replace("'", " ")
                    End If

                    If IsNothing(cco) Then
                        cco = String.Empty
                    Else cco = cco.Replace("'", " ")
                    End If

                    If IsNothing(para) Then
                        para = String.Empty
                    Else para = para.Replace("'", " ")
                    End If

                    If IsNothing(subject) Then subject = String.Empty
                    If IsNothing(pcName) Then pcName = String.Empty


                    If Server.isOracle Then
#Region "ORACLE"
                        Try
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "usuario: " & userId)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "pc: " & pcName)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "docid: " & docId)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "doctypeid: " & docTypeId)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "para: " & para)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "copia: " & cc)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "copia oculta: " & cco)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "tema: " & subject)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "ubicacion: " & fullPath)

                            Dim query As New StringBuilder

                            query.Append("INSERT INTO ZMAIL_HISTORY VALUES( ")
                            query.Append(Server.Con.ConvertDateTime(Now))
                            query.Append(" , ")
                            query.Append(userId.ToString())
                            query.Append(", '")
                            query.Append(pcName.ToString())
                            query.Append("', ")
                            query.Append(docId.ToString())
                            query.Append(", ")
                            query.Append(docTypeId.ToString())
                            query.Append(", '")
                            query.Append(para.ToString())
                            query.Append("','")
                            query.Append(cc.ToString())
                            query.Append("','")
                            query.Append(cco.ToString())
                            query.Append("','")
                            query.Append(subject.ToString())
                            query.Append("','")
                            query.Append(fullPath.ToString())
                            query.Append("','','')")

                            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inserto el mail en el historial correctamente")

                        Catch ex As Exception
                            Throw ex

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ocurrio un error al insertar el Historial de mail")
                        Finally

                        End Try
#End Region

                    Else
                        parvalues = {userId, pcName, docId, docTypeId, para, cc, cco, subject, fullPath}
                        Server.Con.ExecuteNonQuery("ZSP_ZMAIL_HISTORY_300_Insertar", parvalues)
                    End If

                    parvalues = Nothing
                    bResul = True
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No hay ID de documento para el logueo de historial de Mail")
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(pcName) Then pcName = Nothing
            If Not IsNothing(exportPath) Then exportPath = Nothing
        End Try

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Fin de logueo de historial de Mail")
        Return bResul
    End Function

    Private Shared Function CreateExportFolder(ByVal DocId As Int32) As String

        Dim PathServidor As String = String.Empty
        Dim ExportPath As String = String.Empty

        Try

            ExportPath = Email_Factory.GetEmailExportPath()

            If Not String.IsNullOrEmpty(ExportPath) Then

                ExportPath = ExportPath & "\" & DocId.ToString() & "\" & (getHistoryCount(DocId) + 1)

                'ver si existe la carpeta de este documento en el volumen
                If Not Directory.Exists(ExportPath) Then
                    Directory.CreateDirectory(ExportPath)
                End If

            End If

        Catch ex As Exception

            ExportPath = String.Empty
            Throw ex

        End Try

        CreateExportFolder = ExportPath

    End Function

    Private Shared Function SaveHistoryFiles(ByVal DocId As Int32, ByVal Body As String, ByVal Attachs As Generic.List(Of String), ByVal ExportPath As String) As Boolean

        Dim bSaveAttachs As Boolean = getExportDoc() 'EXPORT_EMAIL_DOC
        Dim bExportBody As Boolean = getExportBody() 'EXPORT_EMAIL_BODY

        Dim Resul As Boolean = True

        If Not String.IsNullOrEmpty(ExportPath) Then

            If (bSaveAttachs Or bExportBody) AndAlso DocId > 0 Then

                Try

                    If bSaveAttachs Then

                        Dim attachName As String
                        Dim Pos As Integer

                        'Exportar los adjuntos
                        If Not Attachs Is Nothing Then
                            For Each attach As String In Attachs
                                If Not attach Is Nothing AndAlso attach <> String.Empty Then
                                    Pos = attach.LastIndexOf("\") + 1
                                    attachName = attach.Substring(Pos, attach.Length - Pos)
                                    File.Copy(attach, ExportPath & "\" & attachName, True)
                                End If
                            Next
                        End If

                    End If

                    If bExportBody Then

                        'Exportar el body del email a html
                        Dim sw As StreamWriter = File.CreateText(ExportPath & "\body.html")

                        sw.WriteLine("<html><body>")
                        sw.WriteLine(Body)
                        sw.WriteLine("</body></html>")
                        sw.Close()

                        sw = Nothing

                    End If

                Catch ex As Exception
                    Resul = False
                    raiseerror(ex)
                End Try

            End If

        Else

            Resul = False

        End If

        Return Resul

    End Function

    Private Shared Function getHistoryCount(ByVal DocId As Int32) As Integer

        Dim CantExport As Integer = 0
        Dim ds As New DataSet

        Try

            If Server.isOracle Then


                Dim parvalues As String() = {DocId, 2}


                ds = Server.Con.ExecuteDataset("ZSP_ZMAIL_HISTORY_100.CantExportaciones", parvalues)

            Else
                Dim parvalues As String() = {DocId}
                ds = Server.Con.ExecuteDataset("ZSP_ZMAIL_HISTORY_100_CantExportaciones", parvalues)
            End If


            If ds.Tables(0).Rows.Count > 0 Then
                CantExport = ds.Tables(0).Rows(0)(0)

            End If

        Catch ex As Exception
            raiseerror(ex)
        End Try

        getHistoryCount = CantExport

    End Function
    Public Overrides Sub Dispose()

    End Sub

End Class
