Imports ZAMBA.AppBlock
Imports ZAMBA.Servers
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports Zamba.Outlook.Outlook
Imports System.Collections.Generic


Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates

Public Class Message_Factory
    Private Const _COMA As String = ","
    Private Const _PUNTOYCOMA As String = ";"
    Private arrSeparador As String() = {";"}

    Public Function SendMailNet(ByVal mail As ISendMailConfig) As Boolean
        Dim msg As MailMessage = Nothing
        Dim mailSent As Boolean = False
        Dim imagelink As LinkedResource = Nothing
        Dim SMTP As SmtpClient = Nothing
        Dim smtpConfig As NetworkCredential = Nothing
        Dim htmlView As AlternateView = Nothing

        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando el envío de mail.")
            msg = New MailMessage()

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Remitente: " & mail.From)
            msg.From = New MailAddress(mail.From)

            'ZTrace.WriteLineIf(ZTrace.IsVerbose, "Cuerpo: " & mail.Body)
            msg.Body = mail.Body

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Asunto: " & mail.Subject)
            msg.Subject = mail.Subject

            msg.IsBodyHtml = mail.IsBodyHtml

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Destinatarios TO: " & mail.MailTo)
            AddMailAddress(mail.MailTo, msg.To)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Destinatarios CC: " & mail.Cc)
            AddMailAddress(mail.Cc, msg.CC)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Destinatarios CCO: " & mail.Cco)
            AddMailAddress(mail.Cco, msg.Bcc)

            If mail.AttachFileNames IsNot Nothing Then
                For Each fileName As String In mail.AttachFileNames
                    Try
                        If File.Exists(fileName) Then
                            msg.Attachments.Add(New System.Net.Mail.Attachment(fileName))
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Next
            End If

            If mail.Attachments IsNot Nothing Then
                For Each attach As Attachment In mail.Attachments
                    Try
                        msg.Attachments.Add(attach)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Next
            End If

            If mail.AttachesZip IsNot Nothing Then
                'For Each fileName As String In mail.AttachesZip
                Try
                    Dim RB As Results_Business

                    'Dim Path = RB.GetTempFileFromResult(mail.AttachesZip.Name, True)
                    msg.Attachments.Add(New System.Net.Mail.Attachment(Zamba.Membership.MembershipHelper.AppTempPath + "\temp\" + mail.AttachesZip.Name))
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

            End If





            If mail.OriginalDocument IsNot Nothing Then
                Try
                    msg.Attachments.Add(New Attachment(New MemoryStream(mail.OriginalDocument), mail.OriginalDocumentFileName))
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If

            If mail.Attaches IsNot Nothing Then
                Dim ms As MemoryStream
                For Each blob As BlobDocument In mail.Attaches
                    Try
                        ms = New MemoryStream(blob.BlobFile)
                        msg.Attachments.Add(New Attachment(ms, blob.Name))
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Next
            End If


            ZTrace.WriteLineIf(ZTrace.IsInfo, "ProveedorSMTP: " & mail.SMTPServer & " Puerto: " & mail.SMTPPort & " SSL: " & mail.EnableSsl)
            SMTP = New SmtpClient(mail.SMTPServer, mail.SMTPPort)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando credenciales - Usuario: " & mail.SMTPUserName)
            If String.IsNullOrEmpty(mail.SMPTPassword) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Contraseña: La contraseña esta vacía.")
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Contraseña: Completa.")
            End If
            smtpConfig = New NetworkCredential(mail.SMTPUserName, mail.SMPTPassword)
            'smtpConfig = New NetworkCredential(mail.From, mail.SMPTPassword)

            SMTP.EnableSsl = mail.EnableSsl
            SMTP.DeliveryMethod = SmtpDeliveryMethod.Network

            If Not String.IsNullOrEmpty(mail.SMTPUserName) AndAlso Not String.IsNullOrEmpty(mail.SMPTPassword) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se asignan credenciales SMTP")
                SMTP.UseDefaultCredentials = False
                SMTP.Credentials = smtpConfig
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Las credenciales SMTP no han sido asignadas debido a que el usuario o contraseña se encontraban en blanco.")
            End If

            If mail.ImagesToEmbedPaths IsNot Nothing Then
                htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, Nothing, "text/html")

                For Each path As String In mail.ImagesToEmbedPaths
                    imagelink = New LinkedResource(path, "image/jpeg")
                    imagelink.ContentId = New FileInfo(path).Name
                    imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64
                    htmlView.LinkedResources.Add(imagelink)
                Next

                msg.AlternateViews.Add(htmlView)
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Enviando email...")

            ServicePointManager.ServerCertificateValidationCallback = Function(s As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) True

            Dim count As Int32 = 0

            While mailSent = False AndAlso count < 5
                Try
                    count = count + 1
                    SMTP.Send(msg)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El mail ha sido enviado.")
                    mailSent = True
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Falló intento de envio de mail")

                    If count = 5 Then
                        Throw
                    Else
                        Threading.Thread.Sleep(1000)
                    End If
                End Try
            End While

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, "Error al enviar el email: " + ex.ToString())
            ZClass.raiseerror(ex)
            mailSent = False
            Throw

        Finally
            Try
                If htmlView IsNot Nothing Then
                    htmlView.Dispose()
                    htmlView = Nothing
                End If
                If imagelink IsNot Nothing Then
                    imagelink.Dispose()
                    imagelink = Nothing
                End If
                If msg.Attachments IsNot Nothing AndAlso msg.Attachments.Count > 0 Then
                    For i As Int32 = 0 To msg.Attachments.Count - 1
                        Try
                            msg.Attachments(i).Dispose()
                            msg.Attachments(i) = Nothing

                        Catch
                        End Try
                    Next

                    msg.Attachments.Clear()
                End If

                If msg.AlternateViews IsNot Nothing AndAlso msg.AlternateViews.Count > 0 Then
                    For i As Int32 = 0 To msg.AlternateViews.Count - 1
                        Try
                            msg.AlternateViews(i).Dispose()
                            msg.AlternateViews(i) = Nothing
                        Catch
                        End Try
                    Next
                    msg.AlternateViews.Clear()
                End If
                smtpConfig = Nothing
                If SMTP IsNot Nothing Then
                    SMTP.Dispose()
                    SMTP = Nothing
                End If
                If msg IsNot Nothing Then
                    msg.Dispose()
                    msg = Nothing
                End If
            Catch
            End Try
        End Try

        Return mailSent

    End Function

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
    Private Sub AddMailAddress(ByVal mail As String, ByRef mails As Generic.List(Of String))
        For Each paraValue As String In mail.Split(_PUNTOYCOMA)
            mails.Add(paraValue)
        Next
    End Sub

    ''' <summary>
    ''' Formatea mails y los agrega a una colección
    ''' </summary>
    ''' <param name="mails">Correos formateados</param>
    ''' <param name="addressCol">Listado donde se agregaran los correos</param>
    ''' <remarks></remarks>
    Private Sub AddMailAddress(ByVal mails As String, ByRef addressCol As MailAddressCollection)
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

            For Each mail As String In mails.Split(_PUNTOYCOMA)
                addressCol.Add(New MailAddress(mail))
            Next
        End If
    End Sub

End Class
