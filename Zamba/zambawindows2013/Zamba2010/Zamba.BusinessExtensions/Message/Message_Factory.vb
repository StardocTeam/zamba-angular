Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports Zamba.Outlook.Outlook


Public Class Message_Factory

    Private Const _COMA As String = ","
    Private Const _PUNTOYCOMA As String = ";"
    Private Shared arrSeparador As String() = {";"}


    ''' <summary>
    ''' Metodo q recibe la configuracion completa del mail y lo envia
    ''' </summary>
    ''' <param name="from"></param>
    ''' <param name="proveedorSmtp"></param>
    ''' <param name="puerto"></param>
    ''' <param name="userName"></param>
    ''' <param name="password"></param>
    ''' <param name="para"></param>
    ''' <param name="cc"></param>
    ''' <param name="cco"></param>
    ''' <param name="asunto"></param>
    ''' <param name="body"></param>
    ''' <param name="isBodyHtml"></param>
    ''' <param name="attachFileNames"></param>
    ''' <history> Marcelo  created  19/09/2008 
    '''           Javier   Modified 17/11/2010 - Se agrega parametro basemail utilizado en lotus.
    ''' </history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Sub SendMail(ByVal eMailType As MailTypes, _
                                    ByVal _from As String, _
                                    ByVal proveedorSmtp As String, _
                                    ByVal puerto As String, _
                                    ByVal userName As String, _
                                    ByVal password As String, _
                                    ByVal para As String, _
                                    ByVal cc As String, _
                                    ByVal cco As String, _
                                    ByVal asunto As String, _
                                    ByVal body As String, _
                                    ByVal isBodyHtml As Boolean, _
                                    ByVal replyTo As String, _
                                    ByVal returnReceipt As Boolean, _
                                    ByVal attachFileNames As List(Of String), _
                                    ByVal userId As Int64, _
                                    ByVal saveOnSend As Boolean, _
                                    ByVal attachsResults As Generic.List(Of IResult), _
                                    ByVal docTypeId As Int64, _
                                    ByVal arrayLinks As ArrayList, _
                                    ByVal imagesToEmbedPaths As List(Of String), _
                                    ByVal basemail As String, _
                                    ByVal enableSsl As Boolean)

        para = FormatMails(para)
        cc = FormatMails(cc)
        cco = FormatMails(cco)

        If eMailType = MailTypes.OutLookMail Then
            SendMailCom(para, cc, cco, asunto, body, isBodyHtml, attachFileNames)

        ElseIf eMailType = MailTypes.LotusNotesMail Then
            Dim tempFileAttachList As New List(Of String)
            Dim attachs As ArrayList = New ArrayList()

            'Se crean los archivos temporales de los attach para poder
            'envíarlos con el nombre del DocType [Alejandro]
            If Not IsNothing(attachsResults) Then
                For Each rs As Result In attachsResults
                    Dim tempFileAttachPath As String = MessagesBusiness.GetNewFile(rs.FullPath, rs.Name)
                    attachs.Add(tempFileAttachPath)
                    tempFileAttachList.Add(tempFileAttachPath)
                Next
            End If

            If Not IsNothing(attachFileNames) Then
                For Each attach As String In attachFileNames
                    attachs.Add(attach)
                Next
            End If

            'ILM.LotusLibrary.LotusLibrary.EnviarMail(body, para, cc, cco, asunto, attachs, False, False, String.Empty, String.Empty, False)
            LotusTools.SendMail(body, para, cc, cco, asunto, attachs, saveOnSend, replyTo, returnReceipt, basemail, arrayLinks)
            UserBusiness.Rights.SaveAction(0, ObjectTypes.ModuleMail, RightsType.EnviarPorMail, asunto)


            'Se borran los archivos temporales creados lineas arriba [Alejandro]
            For Each tempFileAttachPath As String In tempFileAttachList
                If File.Exists(tempFileAttachPath) Then
                    Try
                        File.Delete(tempFileAttachPath)
                    Catch ex As UnauthorizedAccessException
                        Try
                            Dim f As New FileInfo(tempFileAttachPath)
                            f.Attributes = FileAttributes.Normal
                            f.Delete()
                        Catch
                        End Try
                    End Try
                End If
            Next





        ElseIf eMailType = MailTypes.Internal Then
            Dim msg As InternalMessage = New InternalMessage(userId)
            msg.ToStr = para
            msg.Subject = asunto
            msg.Body = body
            Dim attachs As ArrayList = New ArrayList()
            If Not IsNothing(attachFileNames) Then
                For Each attach As String In attachFileNames
                    attachs.Add(attach)
                Next
            End If
            msg.AttachList = attachs
            msg.CCStr = cc
            msg.CCOStr = cco
            msg.Destinatarios.Add(New Destinatario(UserBusiness.GetUserById(userId), MessageType.MailTo))
            msg.Send()

        Else
            SendMailNet(_from, proveedorSmtp, puerto, userName, password, para, cc, cco, asunto, body, isBodyHtml, attachFileNames, imagesToEmbedPaths, enableSsl)
        End If

    End Sub


    ''' <summary>
    ''' Metodo que envia el mail desde net recibiendo todos los parametros para el envio
    ''' </summary>
    ''' <param name="from"></param>
    ''' <param name="ProveedorSMTP"></param>
    ''' <param name="Puerto"></param>
    ''' <param name="UserName"></param>
    ''' <param name="Password"></param>
    ''' <param name="Para"></param>
    ''' <param name="CC"></param>
    ''' <param name="CCO"></param>
    ''' <param name="Asunto"></param>
    ''' <param name="Body"></param>
    ''' <param name="isBodyHtml"></param>
    ''' <param name="AttachFileNames"></param>
    ''' <history> Marcelo created 19/09/2008 </history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Sub SendMailNet(ByVal from As String, _
                                        ByVal proveedorSmtp As String, _
                                        ByVal puerto As String, _
                                        ByVal userName As String, _
                                        ByVal password As String, _
                                        ByVal para As String, _
                                        ByVal cc As String, _
                                        ByVal cco As String, _
                                        ByVal asunto As String, _
                                        ByVal body As String, _
                                        ByVal isBodyHtml As Boolean, _
                                        ByVal attachFileNames As List(Of String), _
                                        ByVal imagesToEmbedPaths As List(Of String), _
                                        ByVal enableSsl As Boolean)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Entrando a SendMailNet")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando MailMessage from: " & from)

        Dim msg As New MailMessage()
        msg.From = New MailAddress(from)
        msg.Body = body
        msg.DeliveryNotificationOptions = DeliveryNotificationOptions.Never


        ZTrace.WriteLineIf(ZTrace.IsInfo, "Asunto: " & asunto)
        msg.Subject = asunto.Replace(Chr(13), String.Empty).Replace(Chr(10), String.Empty)


        'Se agrega esta validación en caso de que se
        'quiera envíar un texto HTML
        ZTrace.WriteLineIf(ZTrace.IsInfo, "html: " & isBodyHtml.ToString())
        msg.IsBodyHtml = isBodyHtml

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Listado destinatarios: " & para)
        If Not String.IsNullOrEmpty(para) Then
            AddMailAddress(para, msg.To)
        End If

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Listado destinatarios con CC: " & cc)
        If Not String.IsNullOrEmpty(cc) Then
            AddMailAddress(cc, msg.CC)
        End If

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Listado destinatarios con CCO: " & cco)
        If Not String.IsNullOrEmpty(cco) Then
            AddMailAddress(cco, msg.Bcc)
        End If

        If Not IsNothing(attachFileNames) Then
            For Each Item As String In attachFileNames
                If File.Exists(Item) Then
                    Try
                        msg.Attachments.Add(New Attachment(Item))
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Adjunto: " & Item)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "El Adjunto NO Existe: " & Item)

                End If
            Next
        End If

        Dim mailSent As Boolean
        Dim smtp As SmtpClient = Nothing
        Dim smtpConfiguration As NetworkCredential = Nothing
        Dim imagelink As LinkedResource = Nothing
        Dim htmlView As AlternateView = Nothing


        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando SmtpClient")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "    ProveedorSMTP: " & proveedorSmtp)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "    Puerto: " & puerto)
            smtp = New SmtpClient(proveedorSmtp, puerto)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando smtpConfiguration")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "    Usuario: " & userName)
            If String.IsNullOrEmpty(password) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "    Password: NO OK")
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "    Password: OK")
            End If
            smtpConfiguration = New NetworkCredential(userName, password)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "    SSL: " & enableSsl)
            smtp.EnableSsl = enableSsl

            If Not String.IsNullOrEmpty(userName) AndAlso Not String.IsNullOrEmpty(password) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se asignan credenciales SMTP")
                smtp.Credentials = smtpConfiguration
            End If

            If Not imagesToEmbedPaths Is Nothing Then
                htmlView = AlternateView.CreateAlternateViewFromString(body, Nothing, "text/html")

                For Each p As String In imagesToEmbedPaths
                    imagelink = New LinkedResource(p, "image/jpeg")
                    imagelink.ContentId = New FileInfo(p).Name
                    imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64

                    htmlView.LinkedResources.Add(imagelink)
                Next

                msg.AlternateViews.Add(htmlView)
            End If


            'ZTrace.WriteLineIf(ZTrace.IsInfo, "Se agrega la confirmación de mail leido.")
            ' msg.Headers.Add("Disposition-Notification-To", "<" & from & ">")


            ZTrace.WriteLineIf(ZTrace.IsInfo, "Enviando email SMTP")
            smtp.Send(msg)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Mail SMTP enviado")
            mailSent = True
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Throw ex
        Finally
            smtpConfiguration = Nothing
            smtp = Nothing
            If imagelink IsNot Nothing Then
                imagelink.Dispose()
                imagelink = Nothing
            End If
            If htmlView IsNot Nothing Then
                htmlView.Dispose()
                htmlView = Nothing
            End If

            'Si este objetos no es liberado, los temporales adjuntos quedan tomados y no pueden ser eliminados.
            If msg IsNot Nothing Then
                msg.Dispose()
                msg = Nothing
            End If
        End Try

    End Sub


    ''' <summary>
    ''' Agrega una cadena de mails formateada previamente con el método "FormatMails" 
    ''' y luego los agrega a una lista genérica de Strings
    ''' </summary>
    ''' <param name="mail">Correos  formateados</param>
    ''' <param name="mails">Listado donde se agregaran los correos</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     Tomas   11/05/2011  Created
    ''' </history>
    Private Shared Sub AddMailAddress(ByVal mail As String, ByRef mails As List(Of String))
        For Each paraValue As String In mail.Split(_PUNTOYCOMA)
            mails.Add(paraValue)
        Next
    End Sub

    ''' <summary>
    ''' Agrega una cadena de mails formateada previamente con el método "FormatMails" 
    ''' y luego los agrega a un MailAddressCollection
    ''' </summary>
    ''' <param name="mails">Correos formateados</param>
    ''' <param name="addressCol">Listado donde se agregaran los correos</param>
    ''' <remarks></remarks>
    Private Shared Sub AddMailAddress(ByVal mails As String, ByRef addressCol As MailAddressCollection)
        For Each mail As String In mails.Split(_PUNTOYCOMA)
            addressCol.Add(New MailAddress(mail))
        Next
    End Sub

    ''' <summary>
    ''' Formatea una cadena de mails separados por coma o punto y coma
    ''' </summary>
    ''' <param name="mails">Mails no formateados</param>
    ''' <returns>Mails formateados</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     Tomas   11/05/2011  Created
    ''' </history>
    Private Shared Function FormatMails(ByVal mails As String) As String
        If Not String.IsNullOrEmpty(mails) Then
            Dim tmpMails As String = mails.Trim.Replace(_COMA, _PUNTOYCOMA)
            mails = String.Empty

            For Each mail As String In tmpMails.Split(arrSeparador, StringSplitOptions.RemoveEmptyEntries)
                If Not String.IsNullOrEmpty(mail.Trim) Then
                    mails = mails & _PUNTOYCOMA & mail.Trim
                End If
            Next
            If mails.Length > 0 Then
                'Quita el primer punto y coma agregado
                mails = mails.Remove(0, 1)
            End If
        End If

        Return mails
    End Function

    Private Shared Sub SendMailCom(ByVal Para As String, ByVal CC As String, ByVal CCO As String, ByVal Asunto As String, ByVal Body As String, ByVal isBodyHtml As Boolean, Optional ByVal AttachFileNames As List(Of String) = Nothing)

        Dim lstPara As New List(Of String)
        Dim lstCC As New List(Of String)
        Dim lstCCO As New List(Of String)

        If Not String.IsNullOrEmpty(Para) Then
            AddMailAddress(Para, lstPara)
        End If
        If Not String.IsNullOrEmpty(CC) Then
            AddMailAddress(CC, lstCC)
        End If
        If Not String.IsNullOrEmpty(CCO) Then
            AddMailAddress(CCO, lstCCO)
        End If

        Dim lstAttach As New List(Of String)
        If Not IsNothing(AttachFileNames) Then

            For Each Item As String In AttachFileNames
                If File.Exists(Item) Then lstAttach.Add(Item)
            Next
        End If

        Dim oMail As New Mail()
        oMail.SendMail(Body, isBodyHtml, Asunto, lstPara, lstCC, lstCCO, lstAttach)
    End Sub

    Private Shared Sub SendMailCom(ByVal Para As List(Of String), ByVal CC As String, ByVal CCO As String, ByVal Asunto As String, ByVal Body As String, ByVal isBodyHtml As Boolean, Optional ByVal AttachFileName As String = "")


        Dim lstCC As New List(Of String)
        Dim lstCCO As New List(Of String)

        If Not String.IsNullOrEmpty(CC) Then
            AddMailAddress(CC, lstCC)
        End If
        If Not String.IsNullOrEmpty(CCO) Then
            AddMailAddress(CCO, lstCCO)
        End If

        Dim lstAttach As New List(Of String)
        If Not String.IsNullOrEmpty(AttachFileName) Then
            lstAttach.Add(AttachFileName)
        End If

        Dim oMail As New Mail()
        oMail.SendMail(Body, isBodyHtml, Asunto, Para, lstCC, lstCCO, lstAttach)


    End Sub

End Class
