Imports Zamba.Servers
Imports Zamba.Core
Imports System.IO
Imports System.Text
Imports System.Net.Mail
Imports System.Data.SqlClient

Public NotInheritable Class Email_Factory

    Inherits ZClass

    Public Shared Function getExportBody() As Boolean
        Dim Resul As Boolean = False
        Try
            Dim Strselect As String = "Select Value from Zopt where Item = 'EXPORT_EMAIL_BODY'"
            Boolean.TryParse(Server.Con.ExecuteScalar(CommandType.Text, Strselect), Resul)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return Resul
    End Function

    Public Shared Sub SaveExportBody(ByVal value As Boolean)
        Try
            Dim Str As String = "Select Value from Zopt where Item = 'EXPORT_EMAIL_BODY'"

            If Server.Con.ExecuteScalar(CommandType.Text, Str) Is Nothing Then
                Str = "insert into Zopt (item,value) Values ('EXPORT_EMAIL_BODY', '" & value & "')"
            Else
                Str = "Update Zopt set Value = '" & value & "' where item = 'EXPORT_EMAIL_BODY'"
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, Str)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Function getExportDoc() As Boolean
        Dim Resul As Boolean = False
        Try
            Dim Strselect As String = "Select Value from Zopt where Item = 'EXPORT_EMAIL_DOC'"
            Boolean.TryParse(Server.Con.ExecuteScalar(CommandType.Text, Strselect), Resul)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return Resul
    End Function

    Public Shared Sub SaveExportDoc(ByVal value As Boolean)
        Try
            Dim Str As String = "Select Value from Zopt where Item = 'EXPORT_EMAIL_DOC'"

            If Server.Con.ExecuteScalar(CommandType.Text, Str) Is Nothing Then
                Str = "insert into Zopt (item,value) Values ('EXPORT_EMAIL_DOC', '" & value & "')"
            Else
                Str = "Update Zopt set Value = '" & value & "' where item = 'EXPORT_EMAIL_DOC'"
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, Str)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub SaveMailHistory(ByVal value As Boolean)
        Try
            Dim Str As String = "Select Value from Zopt where Item = 'MailHistoryEnabled'"

            If Server.Con.ExecuteScalar(CommandType.Text, Str) Is Nothing Then
                Str = "insert into Zopt (item,value) Values ('MailHistoryEnabled', '" & value & "')"
            Else
                Str = "Update Zopt set Value = '" & value & "' where item = 'MailHistoryEnabled'"
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, Str)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Function GetEmailExportPath() As String
        Try
            Dim Strselect As String = "Select Value from Zopt where Item = 'EMAILSPATH'"
            Return Server.Con.ExecuteScalar(CommandType.Text, Strselect)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return String.Empty
        End Try
    End Function

    Public Shared Function GetMailHistoryEnabled() As Boolean
        Dim res As Boolean = False
        Try
            Dim Strselect As String = "Select Value from Zopt where Item = 'MailHistoryEnabled'"
            Boolean.TryParse(Server.Con.ExecuteScalar(CommandType.Text, Strselect), res)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return res
    End Function

    Public Shared Sub SaveEmailExportPath(ByVal path As String)
        Try
            Dim Strinsert As String =
            "insert into Zopt (item,value) Values ('EMAILSPATH', '" & path & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub UpdateEmailExportPath(ByVal path As String)
        Try
            Dim Strupdate As String =
            "Update Zopt set Value = '" & path & "' where item = 'EMAILSPATH'"
            Server.Con.ExecuteNonQuery(CommandType.Text, Strupdate)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


    Public Shared Function getHistory(ByVal DocId As Long) As DataSet
        Dim ds As DataSet = Nothing
        Dim strSelect As String = "SELECT Fecha, USR.NAME Usuario, MSG_TO Para, MSG_CC CC, MSG_BCC BCC, MSG_SUBJECT Asunto, H.ID,H.Attachs [Archivos adjuntos] FROM ZMAIL_HISTORY H INNER JOIN USRTABLE USR ON H.USR_ZMB = USR.ID WHERE DOC_ID = " & DocId & " ORDER BY FECHA DESC"
        If Server.isOracle Then
            strSelect = "SELECT Fecha, USR.NAME Usuario, MSG_TO Para, MSG_CC CC, MSG_BCC BCC, MSG_SUBJECT Asunto, H.ID FROM ZMAIL_HISTORY H INNER JOIN USRTABLE USR ON H.USR_ZMB = USR.ID WHERE DOC_ID = " & DocId & " ORDER BY FECHA DESC"
        End If
        Try
            ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            strSelect = Nothing
        End Try

        getHistory = ds
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
                                          Optional ByVal message As String = "",
                                          Optional ByRef PathToReturn As String = "",
                                          Optional ByRef MailPathVariable As String = "",
                                          Optional ByRef remitente As String = "") As Boolean
        Dim bResul As Boolean = False
        Dim bSaved As Boolean = False
        Dim userId As Long = user.ID

        Dim exportPath As String
        Dim fullPath As String = String.Empty
        Dim useBlobMails As Boolean
        Dim StrAttachFiles As String
        StrAttachFiles = Join(attachs.ToArray, ";")
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando historial de Mail")
        Try
            Dim zopt As New ZOptFactory()
            Boolean.TryParse(zopt.GetValue("UseBlobMails"), useBlobMails)
            zopt = Nothing

            If useBlobMails Then
                Dim filebytes As Byte()

                If message.Length > 0 Then
                    Try
                        Using fs As New FileStream(message, FileMode.Open, FileAccess.Read, FileShare.Write)
                            filebytes = New Byte(fs.Length - 1) {}
                            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length))
                            fs.Close()
                        End Using
                    Catch
                        filebytes = Encoding.UTF8.GetBytes(body)
                    End Try
                Else
                    filebytes = Encoding.UTF8.GetBytes(body)
                End If

                If Server.isSQLServer Then

                    'Guardar el historial en la base
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
                    pPCName.Value = ""

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
                    Dim pAttachs As SqlParameter = New SqlParameter("@m_attachs", SqlDbType.VarChar, 1000)
                    pAttachs.Value = StrAttachFiles
                    Dim parvalues As IDbDataParameter() = {pUserId, pPCName, pDocId, pDocTypeId, pPara, pCC, pCCO, pAsunto, pFullPath, pDocFile, pAttachs}
                    Server.Con.ExecuteNonQuery(CommandType.StoredProcedure, "ZSP_ZMAIL_HISTORY_200_InsertarBlob", parvalues)
                    bResul = True

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
                    pAttachs = Nothing
                    filebytes = Nothing

                Else
                    'ORACLE
                End If


                If Server.isOracle Then

                    Dim pFullPath As String

                    If Not String.IsNullOrEmpty(fullPath) Then
                        pFullPath = fullPath
                    Else
                        If message.Length > 0 Then
                            pFullPath = "\" & docId & ".msg"
                        Else
                            pFullPath = "\" & docId & ".html"
                        End If
                    End If



                    Dim insertZMSG As New StringBuilder()

                    Dim MSG As String = Encoding.Default.GetString(filebytes)

                    'Dim distMSG1 As String = MSG.Substring(0, 69)
                    'Dim distMSG2 As String = MSG.Substring(70, 69)
                    'Dim distMSG3 As String = MSG.Substring(139, MSG.Length - 139)


                    If MSG.Length > 50001 Then




                        Dim hasta As Integer
                        If MSG.Length > 25001 Then
                            hasta = 25001
                        Else
                            hasta = MSG.Length - 1
                        End If
                        Dim distMSG1 As String = MSG.Substring(0, hasta)

                        If MSG.Length > 25001 AndAlso MSG.Length > 50000 Then
                            hasta = 25000
                        Else
                            hasta = MSG.Length - 25001
                        End If
                        Dim distMSG2 As String = MSG.Substring(25001, hasta)

                        If MSG.Length > 50001 Then
                            hasta = MSG.Length - 50001
                        End If

                        Dim distMSG3 As String = MSG.Substring(50001, hasta)



                        insertZMSG.Append("DECLARE ")
                        insertZMSG.Append("MSGClob CLOB;")
                        insertZMSG.Append("BEGIN ")
                        insertZMSG.Append("MSGClob := MSGClob || '" & distMSG1 & "';")
                        insertZMSG.Append("MSGClob := MSGClob || '" & distMSG2 & "' ;")
                        insertZMSG.Append("MSGClob := MSGClob || '" & distMSG3 & "' ;")

                    Else
                        insertZMSG.Append("DECLARE ")
                        insertZMSG.Append("MSGClob CLOB;")
                        insertZMSG.Append("BEGIN ")
                        insertZMSG.Append("MSGClob := MSGClob || '" & MSG & "';")
                    End If

                    insertZMSG.Append("INSERT INTO ZMAIL_HISTORY (fecha, usr_zmb, usr_pc, doc_id, doc_type, msg_to, msg_cc,msg_bcc, msg_subject, ""PATH"", ENCODEFILE ) VALUES (" & "sysdate" & "," & userId & "," & "' '" & "," & docId & "," & docTypeId & "," & "'" & para & "'" & "," & "'" & cc & "'" & "," & "'" & cco & "'" & "," & "'" & subject & "'" & "," & "'" & pFullPath & "'" & ", 'MSGClob' );")

                    insertZMSG.Append("End;")
                    bResul = True
                    Server.Con.ExecuteNonQuery(CommandType.Text, insertZMSG.ToString())


                End If


            Else

                If getExportDoc() AndAlso docId > 0 Then
                    exportPath = CreateExportFolder(docId)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Ruta exportacion: " & exportPath)

                    If String.IsNullOrEmpty(message) Then
                        SaveBodyHtml(body, attachs, docId, bSaved, exportPath, fullPath)

                        If MailPathVariable IsNot Nothing And MailPathVariable IsNot String.Empty Then
                            SaveMsgFromDomail(para, cc, subject, body, docId, MailPathVariable, remitente, exportPath)
                        End If

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
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "No se guarda archivo fisico")
                End If

                If docId > 0 Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando historial en el documento de Zamba: " & docId)
                    'Verifica que no existan posibles datos nulos
                    If IsNothing(cc) Then cc = String.Empty
                    If IsNothing(cco) Then cco = String.Empty
                    If IsNothing(subject) Then subject = String.Empty

                    Dim parvalues As String()

                    If Server.isOracle Then
                        'Dim Id As Int32 = Zamba.Data.CoreData.GetNewID(IdTypes.MailHistory)
                        'parvalues = {userId, pcName, docId, docTypeId, para, cc, cco, subject, fullPath, Id}
                        parvalues = {userId, String.Empty, docId, docTypeId, para, cc, cco, subject, fullPath}

                        Server.Con.ExecuteNonQuery("ZSP_ZMAIL_HISTORY_100.Insertar", parvalues)
                    Else
                        parvalues = {userId, String.Empty, docId, docTypeId, para, cc, cco, subject, fullPath, StrAttachFiles}
                        Server.Con.ExecuteNonQuery("ZSP_ZMAIL_HISTORY_300_Insertar", parvalues)
                    End If

                    parvalues = Nothing
                    bResul = True
                Else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "No hay ID de documento para el logueo de historial de Mail")
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally

            If Not IsNothing(exportPath) Then exportPath = Nothing
        End Try

        PathToReturn = fullPath
        Return bResul
    End Function

    Private Shared Sub SaveBodyHtml(body As String, attachs As Generic.List(Of String), docId As Long, ByRef bSaved As Boolean, exportPath As String, ByRef fullPath As String)
        'Guardar los attachs y el body
        bSaved = SaveHistoryFiles(docId, body, attachs, exportPath)
        fullPath = exportPath & "\body.html"
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Exportado archivo a: " & fullPath)
    End Sub

    Public Shared Sub SaveMsgFromDomail(ByRef para As String, ByRef cc As String, subject As String, body As String, ByRef docId As Long, MailPathVariable As String, remitente As String, ByRef exportPath As String)
        Dim SC As SmtpClient = New SmtpClient()
        Dim CreationDate As String = DateTime.Now().Ticks
        Dim _COMA As String = ","
        Dim _PUNTOYCOMA As String = ";"
        Dim arrSeparador As String() = {";"}

        'Arma la ruta donde se alojara el EML que se creara.
        'Se utiliza una ruta lo mas exclusiva posible para este archivo ya que...
        'luego obtendremos el primer archivo del directorio creado.
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "[SaveMsgFromDomail] - DOC_ID " & docId & ": Armando ruta local para guardar el proximo archivo EML")
        Dim AuxExportPath As String = Email_Factory.GetEmailExportPath() & "\" & docId.ToString() & "\" & "Mails" & "\" & CreationDate
        exportPath = Email_Factory.GetEmailExportPath() & "\" & docId.ToString() & "\" & "Mails"

        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Creando directorio: " & AuxExportPath)
        Directory.CreateDirectory(AuxExportPath)

        'Configuro el SMTP para guardar un EML al enviar el correo con SC.Send()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Configuracion del SMTP para guardar un EML al enviar el correo con SC.Send()")
        SC.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory
        SC.PickupDirectoryLocation = AuxExportPath

        'Se arma el correo para crearlo como EML con SMTPClient
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se arma el correo para crearlo como EML con SMTPClient")
        Dim msg As MailMessage = Nothing
        CreateEmlMail(para, cc, subject, body, remitente, _COMA, _PUNTOYCOMA, arrSeparador, msg)

        'TODO: Este envio esta duplicado ya que antes de llegar aca ya se envio este mismo correo, corregir comportamiento.
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Comienza el envio del correo con STMPclient 'SC.Send()'")
        SC.Send(msg)

        'Obtengo el EML creado
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtengo el EML creado.")
        Dim di As DirectoryInfo = New DirectoryInfo(AuxExportPath)
        Dim MsgCreated As FileInfo = di.GetFiles()(0)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ruta del EML: " & MsgCreated.FullName)

        'Asigno Nombre del archivo final
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignacion del nombre del archivo final.")
        Dim FinalFileName = subject

        'Valido que el nombre no tenga caracteres ilegales
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Validacion de caracteres ilegales en el nombre del archivo.")
        Dim invalid As String = New String(Path.GetInvalidFileNameChars()) + New String(Path.GetInvalidPathChars())
        For Each c As Char In invalid
            FinalFileName = FinalFileName.Replace(c.ToString(), "")
        Next

        'Armo la ruta del archivo donde se guardara el msg.
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Armado de la ruta del archivo donde se guardara el msg.")
        Dim exportPathMSG As String = AuxExportPath & "\" & FinalFileName & ".msg"

        'Se convierte el EML a MSG
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Convercion de EML a MSG, resultado almacenado en: " & exportPathMSG)
        MsgKit.Converter.ConvertEmlToMsg(MsgCreated.FullName, exportPathMSG)

        'Guardo la ruta final en la ZVAR
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardo la ruta final en la ZVAR: " & MailPathVariable)
        If VariablesInterReglas.ContainsKey(MailPathVariable) = Nothing Then
            VariablesInterReglas.Add(MailPathVariable, exportPathMSG)
        Else
            VariablesInterReglas.Item(MailPathVariable) = exportPathMSG
        End If

        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Exportado archivo MSG a: " & exportPathMSG)

        'Se elimina el archivo y la carpeta creados
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Eliminacion de archivos y directorios temporales.")
        File.Delete(MsgCreated.FullName)
    End Sub


    Private Shared Sub CreateEmlMail(ByRef para As String, ByRef cc As String, subject As String, body As String, remitente As String, _COMA As String, _PUNTOYCOMA As String, arrSeparador() As String, ByRef msg As MailMessage)
        msg = New MailMessage()
        msg.From = New MailAddress(remitente)
        msg.Body = body
        msg.Subject = subject
        msg.IsBodyHtml = True

        If Not String.IsNullOrEmpty(cc) Then
            Dim tmpMails As String = cc.Trim.Replace(_COMA, _PUNTOYCOMA)
            cc = String.Empty

            For Each mail As String In tmpMails.Split(arrSeparador, StringSplitOptions.RemoveEmptyEntries)
                If Not String.IsNullOrEmpty(mail.Trim) Then
                    cc = cc & _PUNTOYCOMA & mail.Trim
                End If
            Next
            If cc.Length > 0 Then
                'Quita el primer punto y coma agregado
                cc = cc.Remove(0, 1)
            End If

            For Each mail As String In cc.Split(_PUNTOYCOMA)
                msg.CC.Add(New MailAddress(mail))
            Next
        End If

        If Not String.IsNullOrEmpty(para) Then
            Dim tmpMails As String = para.Trim.Replace(_COMA, _PUNTOYCOMA)
            para = String.Empty

            For Each mail As String In tmpMails.Split(arrSeparador, StringSplitOptions.RemoveEmptyEntries)
                If Not String.IsNullOrEmpty(mail.Trim) Then
                    para = para & _PUNTOYCOMA & mail.Trim
                End If
            Next
            If para.Length > 0 Then
                'Quita el primer punto y coma agregado
                para = para.Remove(0, 1)
            End If

            For Each mail As String In para.Split(_PUNTOYCOMA)
                msg.To.Add(New MailAddress(mail))
            Next
        End If
    End Sub

    Public Shared Function CreateExportFolder(ByVal DocId As Int32) As String

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

        Dim bExportBody As Boolean = getExportBody()

        Dim Resul As Boolean = True

        If Not String.IsNullOrEmpty(ExportPath) Then

            If (bExportBody) AndAlso DocId > 0 Then

                Try
                    If bExportBody Then

                        'Exportar el body del email a html
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ruta a crear archivo: " & ExportPath)

                        Dim sw As StreamWriter = File.CreateText(ExportPath & "\body.html")

                        sw.WriteLine("<html><body>")
                        sw.WriteLine(Body)
                        sw.WriteLine("</body></html>")
                        sw.Close()

                        sw = Nothing

                    End If

                Catch ex As Exception
                    Resul = False

                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())

                    ZClass.raiseerror(ex)

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

                Dim parnames As String() = {"mc_doc_id", "io_cursor"}
                Dim parvalues As String() = {DocId, 2}
                Dim partypes As String() = {13, 5}

                ds = Server.Con.ExecuteDataset("ZSP_ZMAIL_HISTORY_100.CantExportaciones", parvalues)

            Else
                Dim parvalues As String() = {DocId}
                ds = Server.Con.ExecuteDataset("ZSP_ZMAIL_HISTORY_100_CantExportaciones", parvalues)
            End If


            If ds.Tables(0).Rows.Count > 0 Then
                CantExport = ds.Tables(0).Rows(0)(0)

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        getHistoryCount = CantExport

    End Function

    Public Overrides Sub Dispose()

    End Sub

    Public Shared Function GetMailPath(id As Long) As String
        Dim data As String = "SELECT PATH FROM ZMAIL_HISTORY WHERE ID=" & id
        data = Server.Con.ExecuteScalar(CommandType.Text, data)
        Return data
    End Function


    Public Shared Function getAllImapProcesses() As DataTable
        Dim strQuery As StringBuilder = New StringBuilder()
        strQuery.Append("SELECT * FROM Z_ME_P INNER JOIN Z_ME_PD ON z_me_p.PROCESS_ID = z_me_pd.PROCESS_ID INNER JOIN Z_ME_Pm ON  z_me_p.PROCESS_ID= Z_ME_Pm.PROCESS_ID")
        Dim dt As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
        If dt IsNot Nothing Then
            Return dt.Tables(0)
        End If
    End Function


    Public Shared Function getImapProcess(Id) As DataTable
        Dim strQuery As StringBuilder = New StringBuilder()
        strQuery.Append("SELECT * FROM Z_ME_P INNER JOIN Z_ME_PD ON z_me_p.PROCESS_ID = z_me_pd.PROCESS_ID INNER JOIN Z_ME_Pm ON  z_me_p.PROCESS_ID= Z_ME_Pm.PROCESS_ID WHERE z_me_p.PROCESS_ID = " + Id)
        Dim dt As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
        If dt IsNot Nothing Then
            Return dt.Tables(0)
        End If
    End Function


    Public Shared Function DeleteProcessImap(ByVal processId As Int64) As Boolean
        Dim t As New Transaction()
        Try

            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, "DELETE From Z_ME_PM Where PROCESS_ID = " + processId.ToString())
            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, "DELETE From Z_ME_PD Where PROCESS_ID = " + processId.ToString())
            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, "DELETE From Z_ME_P Where PROCESS_ID = " + processId.ToString())

            t.Commit()
            Return True
        Catch ex As Exception
            t.Rollback()
            Return False
        Finally
            t.Con.Close()
            t.Con.dispose()
            t.Con = Nothing
        End Try

    End Function

    Public Shared Function SetProcessActiveState(ByVal processId As Int64, ByVal activeState As Int64) As Boolean
        Try

            Dim strSQLSentence As StringBuilder = New StringBuilder()
            strSQLSentence.Append("UPDATE Z_ME_P SET IS_ACTIVE = " + activeState.ToString() + " WHERE PROCESS_ID = " + processId.ToString())
            Server.Con.ExecuteNonQuery(CommandType.Text, strSQLSentence.ToString())
            Return True
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try

    End Function


    Public Shared Function InsertImapObject(dtoObjectImap As Zamba.Core.DTOObjectImap) As Boolean
        Dim t As New Transaction()
        Try
            Dim query_z_me_p As New StringBuilder
            Dim query_z_me_pd As New StringBuilder
            Dim query_z_me_pm As New StringBuilder

            query_z_me_p.Append("INSERT INTO Z_ME_P(")
            query_z_me_p.Append("USER_NAME")
            query_z_me_p.Append(", PROCESS_NAME")
            query_z_me_p.Append(", EMAIL")
            query_z_me_p.Append(", USER_ID")
            query_z_me_p.Append(", USER_PASSWORD")
            query_z_me_p.Append(", IS_ACTIVE")
            query_z_me_p.Append(") values(")

            query_z_me_p.Append("'" + dtoObjectImap.Nombre_usuario + "',")
            query_z_me_p.Append("'" + dtoObjectImap.Nombre_proceso + "',")
            query_z_me_p.Append("'" + dtoObjectImap.Correo_electronico + "',")
            query_z_me_p.Append(dtoObjectImap.Id_usuario.ToString() + ",")
            query_z_me_p.Append("'" + dtoObjectImap.Password + "',")
            query_z_me_p.Append("'" + dtoObjectImap.Is_Active.ToString() + "'")
            query_z_me_p.Append(")")

            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query_z_me_p.ToString())
            Dim lastProcessId = t.Con.ExecuteScalar(t.Transaction, CommandType.Text, "select PROCESS_ID from z_me_p where rownum = 1 order by PROCESS_ID desc")

            query_z_me_pd.Append("INSERT INTO Z_ME_PD(")
            query_z_me_pd.Append("IP_ADDRESS")
            query_z_me_pd.Append(",FIELD_PORT")
            query_z_me_pd.Append(",FIELD_PROTOCOL")
            query_z_me_pd.Append(",HAS_FILTERS")
            query_z_me_pd.Append(",FILTER_FIELD")
            query_z_me_pd.Append(",FILTER_VALUE")
            query_z_me_pd.Append(",FILTER_RECENTS")
            query_z_me_pd.Append(",FILTER_NOT_READS")
            query_z_me_pd.Append(",EXPORT_ATTACHMENTS_SEPARATELY")
            query_z_me_pd.Append(",PROCESS_ID")
            query_z_me_pd.Append(",GENERIC_INBOX")
            query_z_me_pd.Append(") values(")

            query_z_me_pd.Append("'" + dtoObjectImap.Direccion_servidor + "',")
            query_z_me_pd.Append(dtoObjectImap.Puerto.ToString() + ",")
            query_z_me_pd.Append("'" + dtoObjectImap.Protocolo + "',")
            query_z_me_pd.Append(dtoObjectImap.Filtrado.ToString() + ",")
            query_z_me_pd.Append("'" + dtoObjectImap.Filtro_campo + "',")
            query_z_me_pd.Append("'" + dtoObjectImap.Filtro_valor + "',")
            query_z_me_pd.Append(dtoObjectImap.Filtro_recientes.ToString() + ",")
            query_z_me_pd.Append(dtoObjectImap.Filtro_noleidos.ToString() + ",")
            query_z_me_pd.Append(dtoObjectImap.Exportar_adjunto_por_separado.ToString() + ",")
            query_z_me_pd.Append(lastProcessId.ToString() + ",")
            query_z_me_pd.Append(dtoObjectImap.GenericInbox.ToString() + ",")
            query_z_me_pd.Append(")")

            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query_z_me_pd.ToString())

            query_z_me_pm.Append("INSERT INTO Z_ME_PM(")
            query_z_me_pm.Append("FOLDER_NAME")
            query_z_me_pm.Append(",FOLDER_NAME_DEST")
            query_z_me_pm.Append(",ENTITY_ID")
            query_z_me_pm.Append(",SENT_BY")
            query_z_me_pm.Append(",FIELD_TO")
            query_z_me_pm.Append(",CC")
            query_z_me_pm.Append(",CCO")
            query_z_me_pm.Append(",SUBJECT")
            query_z_me_pm.Append(",FIELD_BODY")
            query_z_me_pm.Append(",FIELD_DATE")
            query_z_me_pm.Append(",Z_USER")
            query_z_me_pm.Append(",CODE_MAIL")
            query_z_me_pm.Append(",TYPE_OF_EXPORT")
            query_z_me_pm.Append(",AUT_INCREMENT")
            query_z_me_pm.Append(",PROCESS_ID")
            query_z_me_pm.Append(") values(")

            query_z_me_pm.Append("'" + dtoObjectImap.Carpeta + "',")
            query_z_me_pm.Append("'" + dtoObjectImap.CarpetaDest + "',")
            query_z_me_pm.Append(dtoObjectImap.Entidad.ToString() + ",")
            query_z_me_pm.Append(dtoObjectImap.Enviado_por.ToString() + ",")
            query_z_me_pm.Append(dtoObjectImap.Para.ToString() + ",")
            query_z_me_pm.Append(dtoObjectImap.Cc.ToString() + ",")
            query_z_me_pm.Append(dtoObjectImap.Cco.ToString() + ",")
            query_z_me_pm.Append(dtoObjectImap.Asunto.ToString() + ",")
            query_z_me_pm.Append(dtoObjectImap.Body.ToString() + ",")
            query_z_me_pm.Append(dtoObjectImap.Fecha.ToString() + ",")
            query_z_me_pm.Append(dtoObjectImap.Usuario_zamba.ToString() + ",")
            query_z_me_pm.Append(dtoObjectImap.Codigo_mail.ToString() + ",")
            query_z_me_pm.Append(dtoObjectImap.Tipo_exportacion.ToString() + ",")
            query_z_me_pm.Append(dtoObjectImap.Autoincremento.ToString() + ",")
            query_z_me_pm.Append(lastProcessId)
            query_z_me_pm.Append(")")

            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query_z_me_pm.ToString())
            t.Commit()
            Return True
        Catch ex As Exception
            t.Rollback()
            Return False
        Finally
            t.Con.Close()
            t.Con.dispose()
            t.Con = Nothing
        End Try

    End Function

    Public Shared Function UpdateImapProcess(dtoObjectImap As Zamba.Core.DTOObjectImap) As Boolean
        Dim t As New Transaction()
        Try
            Dim query_z_me_p As New StringBuilder
            Dim query_z_me_pd As New StringBuilder
            Dim query_z_me_pm As New StringBuilder

            query_z_me_p.Append("UPDATE Z_ME_P ")
            query_z_me_p.Append("SET ")
            query_z_me_p.Append("IS_ACTIVE = " + "'" + dtoObjectImap.Is_Active.ToString() + "',")
            query_z_me_p.Append("USER_NAME = " + "'" + dtoObjectImap.Nombre_usuario.ToString() + "',")
            query_z_me_p.Append("PROCESS_NAME = " + "'" + dtoObjectImap.Nombre_proceso.ToString() + "',")
            query_z_me_p.Append("EMAIL = " + "'" + dtoObjectImap.Correo_electronico.ToString() + "',")
            query_z_me_p.Append("USER_ID = " + "'" + dtoObjectImap.Id_usuario.ToString() + "',")
            query_z_me_p.Append("USER_PASSWORD = " + "'" + dtoObjectImap.Password.ToString() + "'")
            query_z_me_p.Append(" WHERE")
            query_z_me_p.Append(" PROCESS_ID = " + "'" + dtoObjectImap.Id_proceso.ToString() + "'")

            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query_z_me_p.ToString())

            query_z_me_pd.Append("UPDATE Z_ME_PD ")
            query_z_me_pd.Append("SET ")
            query_z_me_pd.Append("IP_ADDRESS = " + "'" + dtoObjectImap.Direccion_servidor.ToString() + "',")
            query_z_me_pd.Append("FIELD_PORT = " + "'" + dtoObjectImap.Puerto.ToString() + "',")
            query_z_me_pd.Append("FIELD_PROTOCOL = " + "'" + dtoObjectImap.Protocolo.ToString() + "',")
            query_z_me_pd.Append("HAS_FILTERS = " + "'" + dtoObjectImap.Filtrado.ToString() + "',")
            query_z_me_pd.Append("FILTER_FIELD = " + "'" + dtoObjectImap.Filtro_campo.ToString() + "',")
            query_z_me_pd.Append("FILTER_VALUE = " + "'" + dtoObjectImap.Filtro_valor.ToString() + "',")
            query_z_me_pd.Append("FILTER_RECENTS = " + "'" + dtoObjectImap.Filtro_recientes.ToString() + "',")
            query_z_me_pd.Append("FILTER_NOT_READS = " + "'" + dtoObjectImap.Filtro_noleidos.ToString() + "',")
            query_z_me_pd.Append("EXPORT_ATTACHMENTS_SEPARATELY = " + "'" + dtoObjectImap.Exportar_adjunto_por_separado.ToString() + "',")
            query_z_me_pd.Append("GENERIC_INBOX = " + "'" + dtoObjectImap.GenericInbox.ToString() + "'")
            query_z_me_pd.Append(" WHERE")
            query_z_me_pd.Append(" PROCESS_ID = " + "'" + dtoObjectImap.Id_proceso.ToString() + "'")

            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query_z_me_pd.ToString())

            query_z_me_pm.Append("UPDATE Z_ME_PM ")
            query_z_me_pm.Append("SET ")
            query_z_me_pm.Append("FOLDER_NAME = " + "'" + dtoObjectImap.Carpeta.ToString() + "',")
            query_z_me_pm.Append("FOLDER_NAME_DEST = " + "'" + dtoObjectImap.CarpetaDest.ToString() + "',")
            query_z_me_pm.Append("ENTITY_ID = " + "'" + dtoObjectImap.Entidad.ToString() + "',")
            query_z_me_pm.Append("SENT_BY = " + "'" + dtoObjectImap.Enviado_por.ToString() + "',")
            query_z_me_pm.Append("FIELD_TO = " + "'" + dtoObjectImap.Para.ToString() + "',")
            query_z_me_pm.Append("CC = " + "'" + dtoObjectImap.Cc.ToString() + "',")
            query_z_me_pm.Append("CCO = " + "'" + dtoObjectImap.Cco.ToString() + "',")
            query_z_me_pm.Append("SUBJECT = " + "'" + dtoObjectImap.Asunto.ToString() + "',")
            query_z_me_pm.Append("FIELD_BODY = " + "'" + dtoObjectImap.Body.ToString() + "',")
            query_z_me_pm.Append("FIELD_DATE = " + "'" + dtoObjectImap.Fecha.ToString() + "',")
            query_z_me_pm.Append("Z_USER = " + "'" + dtoObjectImap.Usuario_zamba.ToString() + "',")
            query_z_me_pm.Append("CODE_MAIL = " + "'" + dtoObjectImap.Codigo_mail.ToString() + "',")
            query_z_me_pm.Append("TYPE_OF_EXPORT = " + "'" + dtoObjectImap.Tipo_exportacion.ToString() + "',")
            query_z_me_pm.Append("AUT_INCREMENT = " + "'" + dtoObjectImap.Autoincremento.ToString() + "'")
            query_z_me_pm.Append(" WHERE")
            query_z_me_pm.Append(" PROCESS_ID = " + "'" + dtoObjectImap.Id_proceso.ToString() + "'")

            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query_z_me_pm.ToString())

            t.Commit()
            Return True
        Catch ex As Exception
            t.Rollback()
            Return False
        Finally
            t.Con.Close()
            t.Con.dispose()
            t.Con = Nothing
        End Try

    End Function

End Class
