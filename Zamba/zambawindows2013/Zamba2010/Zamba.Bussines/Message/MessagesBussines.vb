Imports Zamba.Data
Imports System.Net.Mail
Imports Zamba.Core
Imports System.Collections.Generic
Imports System.Text
'Imports Zamba.Mail
'Imports Zamba.CommonLibrary
Imports System.Net
Imports System.IO
Imports Zamba.Servers
Imports Zamba.DataExt.WSResult.Consume

Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Xml.Serialization
Imports Zamba.Office.Outlook
Imports System.ComponentModel

Public Class MessagesBusiness
    Inherits ZClass

    Public Shared Function isAlreadyRead(ByVal msgid As Int32, ByVal usr As String) As Boolean
        Dim leido As Int32 = MessagesFactory.isAlreadyRead(msgid, usr)
        Return leido <> 0
    End Function



    ''' [Alejandro] 05-03-2010 - Created
    ''' <summary>
    '''     Metodo para chequear que este configurada la ruta para guardar el historial de emails
    ''' </summary>
    Public Shared Sub CheckHistoryExportPath()
        Dim Path As String = Email_Factory.GetEmailExportPath()
        Dim booleangetvalue As Boolean
        Boolean.TryParse(ZOptFactory.GetValue("UseBlobMails"), booleangetvalue)


        If booleangetvalue Then 'String.IsNullOrEmpty(value) Then
            Return
        End If
        If String.IsNullOrEmpty(Path) Then
            Throw New Exception("No se ha configurado la ruta para exportar el historial de emails.")
        Else
            If Not Directory.Exists(Path) Then
                If Not Directory.CreateDirectory(Path).Exists Then
                    Throw New Exception("No existe la ruta configurada para exportar el historial de emails o no se tiene acceso a la misma.")
                End If
            End If
        End If
    End Sub

    ''' [Alejandro] 02-12-2009 - Created
    ''' <summary>
    '''     Metodo para guardar el historial del envio de un email
    ''' </summary>
    ''' <param name="msg">Objeto LotusMail que se envió</param>
    ''' <param name="DocId">Id del documento que se envió en el email</param>
    ''' <param name="DocTypeId">Id de la entidad que se envió en el email</param>
    ''' <history>
    '''     Alejandro   Created     02/12/2009
    '''     Javier      Modified    30/11/2010  Se agrega attach solo si no viene vacío
    ''' </history>
    Public Shared Function SaveHistory(ByVal msg As IMailMessage, ByVal docId As Int64, ByVal docTypeId As Int64)

        Dim Attachs As New List(Of String)

        If TypeOf msg Is ILotusMail Then
            Dim attachments As ArrayList = DirectCast(msg, ILotusMail).Attachments
            ZTrace.WriteLineIf(ZTrace.IsInfo, "LOTUS - Savehistory - Cantidad Adjuntos : " & attachments.Count)
            For Each attach As String In attachments
                If String.IsNullOrEmpty(attach) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "LOTUS - Savehistory - attach vacío")
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "LOTUS - Savehistory - attach: " & attach)
                    Attachs.Add(attach.ToString())
                End If
            Next
            attachments = Nothing
        End If

        If TypeOf msg Is IInternalMessage Then
            For Each attach As String In DirectCast(msg, IInternalMessage).AttachList
                Attachs.Add(attach.ToString())
            Next
        End If

        If TypeOf msg.MailTo Is List(Of String) Then msg.MailTo = ListToString(msg.MailTo)
        If TypeOf msg.CC Is List(Of String) Then msg.CC = ListToString(msg.CC)
        If TypeOf msg.CCO Is List(Of String) Then msg.CCO = ListToString(msg.CCO)

        Email_Factory.SaveHistory(msg.MailTo, msg.CC, msg.CCO, msg.Subject, msg.Body, Attachs, docId, docTypeId, Membership.MembershipHelper.CurrentUser, String.Empty, ZOptFactory.GetValue("UseBlobMails"))

    End Function

    Public Shared Function SaveHistory(ByVal para As String,
                                       ByVal cc As String,
                                       ByVal cco As String,
                                       ByVal subject As String,
                                       ByVal body As String,
                                       ByVal attachs As List(Of String),
                                       ByVal docId As Int64,
                                       ByVal docTypeId As Int64,
                                       ByVal exportPath As String)
        Dim booleanGetValue As Boolean = Boolean.TryParse(ZOptFactory.GetValue("UseBlobMails"), booleanGetValue)

        Email_Factory.SaveHistory(para, cc, cco, subject, body, attachs, docId, docTypeId, Membership.MembershipHelper.CurrentUser, exportPath, booleanGetValue)

    End Function

    Public Shared Function ListToString(ByVal recipients As List(Of String), Optional ByVal sep As String = ";") As String
        Dim rec As String = String.Empty
        Dim sbRecipients As New StringBuilder()

        For Each recipient As String In recipients
            sbRecipients.Append(rec)
            sbRecipients.Append(recipient)
            sbRecipients.Append(sep)
            'rec = rec & recipient.ToString() & sep
        Next

        rec = sbRecipients.ToString()
        sbRecipients = Nothing
        ListToString = rec.Remove(rec.Length - 1, 1).Trim()
    End Function

    Private Shared Function ListToString(ByVal recipients As ArrayList, Optional ByVal sep As String = ";") As String
        Dim rec As String = String.Empty
        Dim sbRecipients As New StringBuilder()

        For Each recipient As String In recipients
            sbRecipients.Append(recipient)
            sbRecipients.Append(sep)
            'rec = rec & recipient.ToString() & sep
        Next

        rec = sbRecipients.ToString()
        sbRecipients = Nothing
        ListToString = rec.Remove(rec.Length - 1, 1).Trim()
    End Function

    Public Shared Function EmbedImages(ByRef HTML As String, ByRef ImagesPaths As ArrayList)

        Dim TempHTML As String
        Dim srcstartindex As Integer
        Dim srcendindex As Integer
        Dim currentindex As Integer
        Dim ImgSrc As String
        Dim CID As String
        Dim IniHTML As String
        Dim FinHTML As String
        Dim FI As FileInfo

        ImagesPaths = New ArrayList

        TempHTML = HTML.ToLower

        currentindex = TempHTML.IndexOf("<img", StringComparison.CurrentCultureIgnoreCase)

        While currentindex <> -1

            srcstartindex = CInt(TempHTML.IndexOf("src=" & Chr(34), currentindex, StringComparison.CurrentCultureIgnoreCase)) + 5
            srcendindex = TempHTML.IndexOf(Chr(34), srcstartindex)
            ImgSrc = TempHTML.Substring(srcstartindex, srcendindex - srcstartindex)

            CID = String.Empty

            If Not String.IsNullOrEmpty(ImgSrc) Then
                Try
                    FI = New FileInfo(ImgSrc)
                    CID = "cid:" & FI.Name
                    ImagesPaths.Add(ImgSrc)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If

            IniHTML = HTML.Substring(0, srcstartindex)
            FinHTML = HTML.Substring(srcendindex, HTML.Length - srcstartindex - ImgSrc.Length)

            HTML = IniHTML & CID & FinHTML

            currentindex = TempHTML.IndexOf("<img", srcstartindex + 1)

        End While

    End Function

    Public Shared Function GetNewFile(ByVal fileFullPath As String, ByVal fileName As String) As String
        Try
            If String.IsNullOrEmpty(fileFullPath) Then
                Return String.Empty
            End If
            Dim newFileExtension As String
            Dim newFileName As String
            Const officeTemp As String = "\OfficeTemp\"

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ruta del mail adjunto: " & fileFullPath)

            'Se descomenta el siguiente foreach ya que el nombre de muchos results pasados por parámetro
            'contienen caracteres que son inválidos para el nombre de un archivo. 
            For Each invalidChar As Char In IO.Path.GetInvalidFileNameChars
                fileName = fileName.Replace(invalidChar, String.Empty)
            Next
            'For Each invalidChar As Char In IO.Path.GetInvalidPathChars
            '    fileFullPath = fileFullPath.Replace(invalidChar, String.Empty)
            'Next

            If Not IO.Directory.Exists(Tools.EnvironmentUtil.GetTempDir(String.Empty).FullName & officeTemp) Then
                IO.Directory.CreateDirectory(Tools.EnvironmentUtil.GetTempDir(String.Empty).FullName & officeTemp)
            End If

            newFileExtension = IO.Path.GetExtension(fileFullPath)
            newFileName = Tools.EnvironmentUtil.GetTempDir(String.Empty).FullName & officeTemp & fileName

            'Se verifica si la extensión del archivo existe y si es la misma que el archivo de origen.
            If String.Compare(IO.Path.GetExtension(newFileName).ToLower(), newFileExtension.ToLower()) <> 0 Then
                newFileName &= newFileExtension
            End If

            Dim i As Int16 = 0
            Do While IO.File.Exists(newFileName)
                If i = 0 Then
                    newFileName = newFileName.Substring(0, newFileName.LastIndexOf(".")) & "(" & i.ToString() & ")" & newFileExtension
                Else
                    newFileName = newFileName.Substring(0, newFileName.LastIndexOf("(")) & "(" & i.ToString() & ")" & newFileExtension
                End If

                i += 1
            Loop

            IO.File.Copy(fileFullPath, newFileName, True)
            Return newFileName
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Public Shared Function CheckMessages() As Integer
        Try
            Dim messages As Integer = MessagesFactory.countNewMessages(Membership.MembershipHelper.CurrentUser.ID)
            Return messages
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Metodo para el envio de AutoMails
    ''' </summary>
    ''' <param name="automail">Objeto automail que se desea enviar</param>
    ''' <param name="Result">Objeto Result con los datos cargados para enviar</param>
    ''' <param name="AddDocument"></param>
    ''' <param name="AddLink">Agrega link a Zamba</param>
    ''' <param name="AddIndexs"></param>
    ''' <param name="ASKConfirm">True, pide confirmacion por parte del usuario</param>
    ''' <remarks>Sobrecarga. Se especifica por parametro si se requiere la interaccion del usuario</remarks>
    Public Overloads Shared Sub AutoMail_SMTP(ByVal automail As AutoMail, ByVal currentResult As Result, ByVal addDocument As Boolean, ByVal AddLink As Boolean, ByVal AddIndexs As Boolean, ByVal askConfirm As Boolean, ByVal smtp As SMTP_Validada, ByVal indexsNames As List(Of String))

        Dim body As New System.Text.StringBuilder()
        body.Append(Zamba.Core.TextoInteligente.ReconocerCodigo(automail.Body, currentResult))

        If AddIndexs Then
            Dim TextoIndices As New System.Text.StringBuilder
            TextoIndices.Append("Atributos: ")
            TextoIndices.AppendLine()

            If Not IsNothing(indexsNames) AndAlso indexsNames.Count > 0 Then
                For Each index As Core.Index In currentResult.Indexs
                    For Each name As String In indexsNames
                        If String.Compare(name, index.Name, True) = 0 Then
                            Dim Dato As String = index.Data
                            If String.IsNullOrEmpty(Dato) Then
                                Dato = index.Data2
                            End If

                            Dato &= " " & index.dataDescription
                            If String.IsNullOrEmpty(Dato) Then
                                Dato = index.dataDescription2
                            End If

                            TextoIndices.Append(index.Name.Trim.ToUpper)
                            TextoIndices.Append(": ")
                            TextoIndices.Append(Dato)
                            TextoIndices.AppendLine()

                            Exit For
                        End If
                    Next
                Next
            ElseIf indexsNames.Count = 0 Then
                For Each index As Core.Index In currentResult.Indexs
                    Dim Dato As String = index.Data

                    If String.IsNullOrEmpty(Dato) Then
                        Dato = index.Data2
                    End If

                    Dato &= " " & index.dataDescription

                    If String.IsNullOrEmpty(Dato) Then
                        Dato = index.dataDescription2
                    End If

                    TextoIndices.Append(index.Name.Trim.ToUpper)
                    TextoIndices.Append(": ")
                    TextoIndices.Append(Dato)
                    TextoIndices.AppendLine()

                Next
            End If

            body.AppendLine()
            body.AppendLine()
            body.Append(TextoIndices.ToString())
        End If

        If AddLink Then
            body.AppendLine()
            body.Append("Link:")
            body.AppendLine()
            body.Append(Results_Business.GetLinkFromResult(currentResult))
        End If

        Try
            Dim sUseWebService As String = UserPreferences.getValueForMachine("WinUseWSSendMail", Sections.Mail, "False")
            Dim useWebService As Boolean

            If String.IsNullOrEmpty(sUseWebService) Then
                useWebService = False
            Else
                useWebService = Boolean.Parse(sUseWebService)
            End If

            Dim CurrentMessage As New MailMessage(smtp.User, automail.MailTo, automail.Subject, body.ToString())
            SendMail(MailTypes.NetMail, CurrentMessage.From.ToString, smtp.Server, smtp.Port, smtp.User, smtp.Password, CurrentMessage.To.ToString, CurrentMessage.CC.ToString, CurrentMessage.Bcc.ToString, CurrentMessage.Subject, CurrentMessage.Body, CurrentMessage.IsBodyHtml, Nothing, smtp.User, Nothing, Nothing, String.Empty, False, useWebService)

        Catch smtpEx As SmtpException
            Dim ErrorMessage As New System.Text.StringBuilder()
            ErrorMessage.Append("Error al enviar probar la configuración:")
            ErrorMessage.AppendLine()
            ErrorMessage.Append(smtpEx.Message)
            MessageBox.Show(ErrorMessage.ToString(), "Error")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Envia un mail, partiendo de una plantilla de mail (Automail).
    ''' El envio puede ser por smtp autenticado.
    ''' </summary>
    ''' <param name="automail">Plantilla de mail(Automail).</param>
    ''' <param name="smtpAutenticado">Datos para la validacion para un envio SMTP Autenticado.</param>    
    ''' <remarks> El parametro smtpAutenticado se debe pasar 
    ''            como nulo si el mail no requiere autenticacion.</remarks>
    Public Overloads Shared Sub AutoMail_SMTP(ByVal automail As AutoMail, ByVal smtpAutenticado As SMTP_Validada)
        If IsNothing(smtpAutenticado) Then
            Try
                Dim m As IMailMessage = MessagesBusiness.GetMessage(Zamba.Core.MailTypes.NetMail)

                If IsNothing(automail._Attach) Then
                    m.Attachs = New ArrayList
                Else
                    m.Attachs = automail._Attach
                End If

                m.Body = Zamba.Core.TextoInteligente.ReconocerCodigo(automail.Body.Trim, Nothing)

                m.De = smtpAutenticado.User
                m.CC = automail.CC
                m.CCO = automail.CCO
                m.MailTo = automail.MailTo
                m.Subject = automail.Subject
                m.send()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        Else
            Try
                Dim mail As MailMessage
                Dim splitchar As Char = ""
                If automail.MailTo.Contains(";") Then
                    splitchar = ";"
                ElseIf automail.MailTo.Contains(",") Then
                    splitchar = ","
                End If
                If splitchar <> "" Then
                    Dim mailto As String = automail.MailTo
                    mail = New MailMessage()
                    mail.From = New MailAddress(smtpAutenticado.User)
                    mail.Body = automail.Body
                    mail.Subject = automail.Subject
                    While Not String.IsNullOrEmpty(mailto)
                        mail.To.Add(mailto.Split(splitchar)(0))
                        mailto = mailto.Remove(0, mailto.Split(splitchar)(0).Length).Trim()
                        If mailto.Length > 0 Then
                            mailto = mailto.Remove(0, 1).Trim()
                        End If
                    End While
                Else
                    mail = New MailMessage(smtpAutenticado.User, automail.MailTo, automail.Subject, automail.Body)
                End If


                'For Each s As String In automail._Attach
                '    mail.Attachments.Add(New Attachment(s))
                'Next


                Dim attachList As New List(Of String)

                If Not IsNothing(automail._Attach) Then
                    For Each attach As String In automail._Attach
                        attachList.Add(attach)
                    Next
                End If



                'TODO: MessageBusines.AutoMail_SMTP .Contempla un solo destinatario en la copia.
                If Not String.IsNullOrEmpty(automail.CC) Then
                    mail.CC.Add(New MailAddress(automail.CC))
                End If

                'SMTP_Mail.EnviarMail(mail, smtpAutenticado.User, smtpAutenticado.Password, _
                '                               smtpAutenticado.Port, smtpAutenticado.Server)

                Dim correo As ICorreo = UserBusiness.Mail.FillUserMailConfig(Membership.MembershipHelper.CurrentUser.ID)
                Dim proveedorSmtp As String = correo.ProveedorSmtp

                Message_Factory.SendMail(MailTypes.NetMail, mail.From.ToString, proveedorSmtp, smtpAutenticado.Port, smtpAutenticado.User, smtpAutenticado.Password, mail.To.ToString, mail.CC.ToString, mail.Bcc.ToString, mail.Subject, mail.Body, mail.IsBodyHtml, True, mail.ReplyTo.ToString, attachList, Membership.MembershipHelper.CurrentUser.ID, True, Nothing, 0, Nothing, Nothing, String.Empty, correo.EnableSsl)

            Catch smtpEx As SmtpException
                Dim ErrorMessage As New System.Text.StringBuilder()
                ErrorMessage.Append("Error al enviar probar la configuración:")
                ErrorMessage.AppendLine()
                ErrorMessage.Append(smtpEx.StatusCode.ToString())
                MessageBox.Show(ErrorMessage.ToString(), "Error")

            End Try
        End If
    End Sub

    Public Shared Sub Fill(ByRef instance As IMensajeForo)
        If IsNothing(instance.Fecha) Then

        End If
    End Sub

    ''' <summary>
    ''' Método que sirve para completar el objeto AutoMail con los datos que se traen de la base de datos
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    11/06/2008  Modified    Se agrego código de validación para validar si un elemento es DBNull
    ''' </history>
    Public Shared Function GetAutomailById(ByVal Id As Int32) As AutoMail
        Dim ds As New DataSet
        ds = MessagesFactory.GetAutomailById(Id)
        If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim ir As DataRow = ds.Tables(0).Rows(0)
            Dim Am As New AutoMail

            Am.CC = DBNull(ir.Item("CC"))
            Am.CCO = DBNull(ir.Item("CCO"))
            Am.MailTo = DBNull(ir.Item("MailTO"))
            Am.Name = ir.Item("Name")
            If IsDBNull(ir.Item("Confirmation")) Then
                Am.Confirmation = False
            ElseIf String.IsNullOrEmpty(ir.Item("Confirmation")) Then
                Am.Confirmation = False
            Else
                Am.Confirmation = CBool(DBNull(ir.Item("Confirmation")))
            End If
            Am.Body = DBNull(ir.Item("Body"))
            Am.Subject = DBNull(ir.Item("subject"))
            Am.ID = CInt(ir.Item("ID"))

            Am.AttachmentsPaths.AddRange(DBNull(ir.Item("PathFiles")).ToString().Split(";"))
            Am.PathImages.AddRange(DBNull(ir.Item("PathImages")).ToString().Split(";"))

            Return Am
        Else
            Return New AutoMail
        End If
    End Function

    Public Shared Function GetAutomailAttachments(ByVal id As Int32) As List(Of String)
        Dim ds As DataSet = Nothing 'MessagesFactory.GetAutomailAttachments(id)

        Dim list As New List(Of String)(ds.Tables(0).Rows.Count)

        For Each row As DataRow In ds.Tables(0).Rows
            list.Add(row("Path").ToString())
        Next

        Return list
    End Function

    ''' <summary>
    ''' Método que sirve para comprobar si un elemento es DBNull, si es se devuelve un String.Empty, sino, se devuelve el objeto tal como llega
    ''' </summary>
    ''' <param name="elem"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    11/06/2008  Created    
    ''' </history>
    Private Shared Function DBNull(ByVal elem As Object) As Object

        If (IsDBNull(elem)) Then
            Return (String.Empty)
        Else
            Return (elem)
        End If

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un objeto "AutoMail" en base al nombre
    ''' </summary>
    ''' <param name="Name">Nombre con que se conoce al "AutoMail"</param>
    ''' <returns>Objeto AutoMail</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetAutomailByName(ByVal Name As String) As AutoMail
        Dim strselect As String = "select * from AutoMail where Name='" & Name & "'"
        Dim con As IConnection
        Dim ir As IDataReader
        Dim Am As New AutoMail

        Try
            con = Server.Con
            ir = con.ExecuteReader(CommandType.Text, strselect)

            If Not ir.Read() Then
                ir.Close()
                ir.Dispose()
                ir = Nothing
                Return Nothing
            End If

            Am.CC = ir.Item("CC")
            Am.CCO = ir.Item("CCO")
            Am.MailTo = ir.Item("MailTO")
            Am.Name = ir.Item("Name")
            Am.Confirmation = CBool(ir.Item("Confirmation"))
            Am.Body = ir.Item("Body")
            Am.Subject = ir.Item("subject")
            Am.ID = CInt(ir.Item("ID"))
        Finally
            If Not ir.Read() Then
                ir.Close()
                ir.Dispose()
                ir = Nothing
            End If
            If Not IsNothing(con) Then
                con.Close()
                con.dispose()
                con = Nothing
            End If
        End Try
        Return Am
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un objeto "AutoMail" en base al Id
    ''' </summary>
    ''' <param name="Id">Id con que se conoce al "AutoMail"</param>
    ''' <returns>Objeto AutoMail</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetAutomailNameByid(ByVal Id As Int32) As String
        Return MessagesFactory.GetAutomailNameByid(Id)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que devuelve un Dataset tipeado con los Mensajes que tiene un usuario en la bandeja de mensajes 
    ''' </summary>
    '''     ''' <param name="UserId">Id del Usuario</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function getMessages(ByVal UserId As Int32) As dsMessages
        Return MessagesFactory.getMessages(UserId)
    End Function

    Public Shared Function GetMessageFile(ByVal id As Long) As Byte()
        Return MessagesFactory.GetMessageFile(id)
    End Function

    Public Shared Sub SaveMessageFile(ByVal file As Byte(), ByVal url As String)
        MessagesFactory.SaveMessageFile(file, url)
    End Sub

    Public Shared Function SendMail(ByVal para As String, ByVal CC As String, ByVal CCO As String, ByVal Asunto As String, ByVal Body As String, ByVal isBodyHtml As Boolean, Optional ByVal AttachFileNames As List(Of String) = Nothing, Optional ByVal ImagesToEmbedPaths As Generic.List(Of String) = Nothing) As Boolean


        If Not Membership.MembershipHelper.CurrentUser Is Nothing Then
            Dim UserId As Int64 = Membership.MembershipHelper.CurrentUser.ID

            Dim Correo As ICorreo
            Dim rw As DataRow
            Correo = Zamba.Core.UserBusiness.Mail.FillUserMailConfig(UserId)
            If Not Correo Is Nothing Then

                Dim ProveedorSMTP As String = Correo.ProveedorSmtp
                Dim puerto As String = Correo.Puerto
                Dim eMailType As Int32 = Membership.MembershipHelper.CurrentUser.eMail.Type
                Dim from As String = Membership.MembershipHelper.CurrentUser.eMail.Mail
                Dim UserName As String = Membership.MembershipHelper.CurrentUser.eMail.UserName
                Dim Password As String = Membership.MembershipHelper.CurrentUser.eMail.Password
                Dim BaseMail As String = Membership.MembershipHelper.CurrentUser.eMail.Base

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Proveedor SMTP: " & ProveedorSMTP & " - " & "Puerto: " & puerto & " - " & "Tipo Mail: " & eMailType.ToString() & " - " & "From: " & from & " - " & "Nombre Usuario: " & UserName & " - " & "Base: " & BaseMail)

                Dim sUseWebService As String = UserPreferences.getValueForMachine("WinUseWSSendMail", Sections.Mail, "False")
                Dim useWebService As Boolean

                If String.IsNullOrEmpty(sUseWebService) Then
                    useWebService = False
                Else
                    useWebService = Boolean.Parse(sUseWebService)
                End If

                SendMail(eMailType, from, ProveedorSMTP, puerto, UserName, Password, para, CC, CCO, Asunto,
                                       Body, isBodyHtml, AttachFileNames, UserId, Nothing, Nothing, BaseMail, Correo.EnableSsl, useWebService)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Envio de mail ejecutado")

            Else
                Throw New Exception("El usuario no tiene configurada correctamente los datos del mail")
            End If
        Else
            Throw New Exception("No se ha iniciado session con ningun usuario")
        End If
        Return True
    End Function

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
    '''           Ezequiel Modified 27/01/2009 - Se modifico ya que faltaban pasar por parametros 2 atributos. 
    '''           Javier   Modified 17/11/2010 - Se agrega parametro basemail utilizado en lotus.
    '''</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Sub SendMail(ByVal eMailType As MailTypes,
                                    ByVal _from As String,
                                    ByVal proveedorSmtp As String,
                                    ByVal puerto As String,
                                    ByVal userName As String,
                                    ByVal password As String,
                                    ByVal para As String,
                                    ByVal cc As String,
                                    ByVal cco As String,
                                    ByVal asunto As String,
                                    ByVal body As String,
                                    ByVal isBodyHtml As Boolean,
                                    ByVal attachFileNames As List(Of String),
                                    ByVal userId As Int64,
                                    ByVal arrayLinks As ArrayList,
                                    ByVal imagesToEmbedPaths As List(Of String),
                                    ByVal basemail As String,
                                    ByVal enableSsl As Boolean)
        Dim SendAslink As Boolean = True
        Dim SaveOnSend As Boolean = True
        Message_Factory.SendMail(eMailType, _from, proveedorSmtp, puerto, userName, password, para, cc, cco, asunto, body, isBodyHtml, String.Empty, SendAslink, attachFileNames, userId, SaveOnSend, Nothing, 0, arrayLinks, imagesToEmbedPaths, basemail, enableSsl)
    End Sub

    ''' <summary>
    ''' Realiza el envio de mail normal pero si useWebService está en true, usará WSResults para llevar a cabo la accion
    ''' </summary>
    ''' <param name="eMailType"></param>
    ''' <param name="from"></param>
    ''' <param name="ProveedorSMTP"></param>
    ''' <param name="Puerto"></param>
    ''' <param name="UserName"></param>
    ''' <param name="Password"></param>
    ''' <param name="para"></param>
    ''' <param name="cc"></param>
    ''' <param name="cco"></param>
    ''' <param name="asunto"></param>
    ''' <param name="body"></param>
    ''' <param name="isBodyHtml"></param>
    ''' <param name="attachFileNames"></param>
    ''' <param name="userId"></param>
    ''' <param name="ArrayLinks"></param>
    ''' <param name="ImagesToEmbedPaths"></param>
    ''' <param name="basemail"></param>
    ''' <param name="originalDocument"></param>
    ''' <param name="originalDocumentFileName"></param>
    ''' <param name="enableSsl"></param>
    ''' <param name="useWebService"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Sub SendMail(ByVal eMailType As MailTypes,
                                    ByVal _from As String,
                                    ByVal proveedorSmtp As String,
                                    ByVal puerto As String,
                                    ByVal userName As String,
                                    ByVal password As String,
                                    ByVal para As String,
                                    ByVal cc As String,
                                    ByVal cco As String,
                                    ByVal asunto As String,
                                    ByVal body As String,
                                    ByVal isBodyHtml As Boolean,
                                    ByVal attachFileNames As List(Of String),
                                    ByVal userId As Int64,
                                    ByVal arrayLinks As ArrayList,
                                    ByVal imagesToEmbedPaths As List(Of String),
                                    ByVal basemail As String,
                                    ByVal enableSsl As Boolean,
                                     ByVal useWebService As Boolean)

        Dim SendAslink As Boolean = True
        Dim SaveOnSend As Boolean = True
        If useWebService Then
            Dim wsFactory As New WSResultsFactory()

            ZTrace.WriteLineIf(ZTrace.IsInfo, "useWebService en true, se envia por WS")

            'Obtengo los attaches para mandarlos por WS
            Dim wsAttaches As List(Of Zamba.DataWS.WSResults.BlobDocument) = GetBlobFilesAttaches(attachFileNames)

            'Invoco el WS
            ZTrace.WriteLineIf(ZTrace.IsInfo, "From:" & _from)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "proveedorSmtp:" & proveedorSmtp)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "puerto:" & puerto)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "userName:" & userName)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "password:" & password)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "para:" & para)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "cc:" & cc)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "cco:" & cco)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "asunto:" & asunto)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "body:" & body)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "userId:" & userId)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "enableSsl:" & enableSsl)
            If wsFactory.ConsumeZSendMailWithAttaches(_from, proveedorSmtp, puerto, userName, password, para, cc, cco, asunto, body, wsAttaches, userId, enableSsl) = False Then
                Throw New Exception("WebService no pudo enviar el Mail")
            End If
            wsFactory.Dispose()
            wsFactory = Nothing

        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "useWebService en false, se envia directo")
            Message_Factory.SendMail(eMailType, _from, proveedorSmtp, puerto, userName, password, para, cc, cco, asunto, body, isBodyHtml, String.Empty, SendAslink, attachFileNames, userId, SaveOnSend, Nothing, 0, arrayLinks, imagesToEmbedPaths, basemail, enableSsl)
        End If
    End Sub

    Public Shared Sub SendMail(ByVal para As String, ByVal CC As String, ByVal CCO As String, ByVal Asunto As String, ByVal Body As String, ByVal isBodyHtml As Boolean, ByVal addLinks As Boolean, ByVal attachResult As Result, Optional ByVal AttachFileNames As List(Of String) = Nothing)
        Dim sBuilder As New StringBuilder
        If addLinks Then

            Dim _attachResult As Result = attachResult

            Dim attachResultLink As String = Results_Business.GetHtmlLinkFromResult(_attachResult.DocType.ID, _attachResult.ID, "Acceso al documento.")
            sBuilder.Append("<br><br>Link Cliente Windows: ")
            sBuilder.Append(attachResultLink)

            If Not IsNothing(ZOptBusiness.GetValue("WebAccessDocIdPath")) Then
                sBuilder.Append("<br>Link Cliente Web: ")
                sBuilder.Append("<a href=")
                sBuilder.Append(Convert.ToChar(34))
                sBuilder.Append(ZOptBusiness.GetValue("WebAccessDocIdPath") & "?docid=" & _attachResult.ID & "&doctid=" & _attachResult.DocType.ID)
                sBuilder.Append(Convert.ToChar(34))
                sBuilder.Append(">")
                sBuilder.Append("Acceso al documento.")
                sBuilder.Append("</a>")
            End If

        End If
        Body = Body & Chr(13) & Chr(13) & sBuilder.ToString

        SendMail(para, CC, CCO, Asunto, Body, isBodyHtml, AttachFileNames)

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un entero que representa la cantidad de mensajes no leidos por el usuario
    ''' </summary>
    ''' <param name="User">Objeto usuario del cual se obtendran los mensajes no leídos</param>
    ''' <returns>Numero Entero</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function countNewMessages(ByVal User As IUser) As Integer
        Return MessagesFactory.countNewMessages(User.ID)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Dataset Tipeado con los adjuntos de los mensajes para un usuario
    ''' </summary>
    ''' <param name="user_id">ID del usuario</param>
    ''' <returns>Dataset DSAttach</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function getMyMessageAttachs(ByVal user_id As Integer) As DSAttach
        Return MessagesFactory.getMyMessageAttachs(user_id)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para eliminar un mensaje determinado
    ''' </summary>
    ''' <param name="idMessage">Id del mensaje que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DeleteMessage(ByVal idMessage As Int32)
        MessagesFactory.DeleteMessage(idMessage)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene el path para la libreta de direcciones (XML) para un usuario determinado
    ''' </summary>
    ''' <param name="usr_id">Id del Usuario</param>
    ''' <returns>String, ruta deseada</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function getAddressBookPath(ByVal usr_id As Int64) As String
        Return MessagesFactory.getAddressBookPath(usr_id)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' Project	 : Zamba.MessagesControls
    ''' Class	 : Controls.MessageFactory
    ''' 
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Fabrica de Mensajes en base al Tipo de servidor que se disponga para el envio del mismo
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un objeto que implementa la interfaz IMessage
    ''' </summary>
    ''' <param name="mt"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Dependiendo del tipo de servidor se puede obtener, LotusMail, NetMail u OutlookMail
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetMessage(ByVal mt As Zamba.Core.MailTypes) As IMailMessage
        Select Case mt
            Case Zamba.Core.MailTypes.LotusNotesMail
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Mail: Lotus")
                Return New LotusMail
            Case Zamba.Core.MailTypes.NetMail
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Mail: NetMail")
                Return New NetMailMessage(SMTP)
            Case Zamba.Core.MailTypes.OutLookMail
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Mail: Outlook")
                Return New NetMailMessage(SMTP)
        End Select
        Return Nothing
    End Function
    Public Shared ReadOnly Property SMTP() As String
        Get
            Return Membership.MembershipHelper.CurrentUser.eMail.ProveedorSmtp
        End Get
    End Property

    Public Overrides Sub Dispose()

    End Sub

    ''' <summary>
    ''' Permite levantar una lista de path y transformarlos en una lista de BlobDocuments(serializarlos y guardar su nombre)
    ''' </summary>
    ''' <param name="attachFileNames"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetBlobFilesAttaches(ByVal attachFileNames As List(Of String)) As List(Of Zamba.DataWS.WSResults.BlobDocument)
        'Si hay paths para levantar
        If attachFileNames IsNot Nothing AndAlso attachFileNames.Count > 0 Then
            Dim blobAttach As Zamba.DataWS.WSResults.BlobDocument
            Dim filenfo As FileInfo
            Dim attachesBytes As Byte()

            Try
                Dim wsAttaches As New List(Of Zamba.DataWS.WSResults.BlobDocument)

                'Por cada path
                For Each sFile As String In attachFileNames
                    If Not String.IsNullOrEmpty(sFile) Then

                        filenfo = New FileInfo(sFile)
                        'Verifico que el archivo exista
                        If filenfo IsNot Nothing AndAlso filenfo.Exists Then
                            'Creo el blobDocument, serializo el archivo y lo añado a la lista para devolver
                            blobAttach = New Zamba.DataWS.WSResults.BlobDocument()
                            blobAttach.Name = filenfo.Name
                            blobAttach.BlobFile = FileEncode.Encode(sFile)
                            wsAttaches.Add(blobAttach)
                        End If
                    End If
                Next
                Return wsAttaches
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                blobAttach = Nothing
                filenfo = Nothing
                attachesBytes = Nothing
            End Try
        End If
        Return Nothing
    End Function


    Private Shared Sub QuequeMail(conf As ISendMailConfig)
        Dim xml_serializer As New XmlSerializer(conf.GetType)
        Dim string_writer As New StringWriter
        xml_serializer.Serialize(string_writer, conf)

        Dim MailSerialized As String = string_writer.ToString()
        string_writer.Close()

        Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("Insert into ZQM (UserId, Mail, State) values ({0},{1},0)", conf.UserId, MailSerialized))


    End Sub

    Enum QuequedStates
        Pending = 0
        Processing = 1
        Sended = 2
        WithError = 3
    End Enum
    Public Sub PeekQuequedMail(QuequedState As QuequedStates)
        Dim Id As String = Zamba.Servers.Server.Con.ExecuteScalar(CommandType.Text, String.Format("select top 1 Id from ZQM where state = {0}", QuequedState))
        Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("update  ZQM set state = 1 where id = {0}", Id))
        Dim MailSerialized As String = Zamba.Servers.Server.Con.ExecuteScalar(CommandType.Text, String.Format("select Mail from ZQM where id = {0}", Id))

        If Not MailSerialized Is Nothing Then

            Dim string_reader As StringReader
            Dim xml_serializer As New XmlSerializer(GetType(ISendMailConfig))

            string_reader = New StringReader(MailSerialized)
            Dim conf As ISendMailConfig = xml_serializer.Deserialize(string_reader)

            '            If SendMail(conf) Then
            If SendMail(conf.MailTo, conf.Cc, conf.Cco, conf.Subject, conf.Body, conf.IsBodyHtml, conf.AttachFileNames, conf.ImagesToEmbedPaths) Then
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("update  ZQM set state = 2 where id = {0}", Id))
            Else
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("update  ZQM set state = 3 where id = {0}", Id))
            End If

        End If


    End Sub

    Public Shared Function GetNewMailAndSaveHistory(ByVal docId As Long, ByVal docTypeId As Long, Optional ByVal _params As Hashtable = Nothing, Optional ByVal _automaticSend As Boolean = False) As Boolean
        Try
            Dim outlookInstance As Office.Outlook.OutlookInterop = Office.Outlook.SharedOutlook.GetOutlook()
            outlookInstance.SendMail(outlookInstance.GetNewMailItem(_params), _automaticSend)
            SaveParamInDB(outlookInstance.LastMailItem.To, outlookInstance.LastMailItem.Subject, docId, docTypeId)

            Dim WP As New BackgroundWorker()
            AddHandler WP.DoWork, AddressOf SearchMailAndSaveAsync

            WP.RunWorkerAsync(outlookInstance)
            Return True

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try
    End Function


    Public Shared Sub SearchMailAndSaveAsync(sender As Object, e As DoWorkEventArgs)
        Try


            Dim outlookInstance As Office.Outlook.OutlookInterop = e.Argument

            Dim mails As DataTable = SearchMailInDB()


            If Not IsNothing(mails) AndAlso mails.Rows.Count > 0 Then

                If outlookInstance Is Nothing Then
                    outlookInstance = SharedOutlook.GetOutlook()
                End If

                Dim _pathDirectory As String = Tools.EnvironmentUtil.GetTempDir("\IndexerTemp").FullName
                Dim NuevoFile As New IO.FileInfo(FileBusiness.GetUniqueFileName(_pathDirectory, "OutlookMail", ".msg"))
                Dim mailObtenido As Microsoft.Office.Interop.Outlook.MailItem
                Dim obtuveOutlook As Boolean = False
                Dim timeOut As Integer = 60

                For Each mail As DataRow In mails.Rows

                    obtuveOutlook = False

                    While Not obtuveOutlook And timeOut > 0
                        mailObtenido = outlookInstance.SearchMailInOutlook(mail, obtuveOutlook)

                        If Not IsNothing(mailObtenido) Then
                            SaveMailItem(outlookInstance, mailObtenido, NuevoFile.FullName, mail("docid"), mail("doctypeid"))
                            Server.Con.ExecuteNonQuery(CommandType.Text, "Delete from ZparamsItemMail where id = " & mail("id"))
                        End If

                        timeOut = timeOut - 1
                        Threading.Thread.Sleep(1000)
                    End While

                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Shared Sub SaveMailItem(ByVal outlookInstance As OutlookInterop, ByVal mailItemFind As Microsoft.Office.Interop.Outlook.MailItem, ByVal nameFile As String, ByVal docId As Long, ByVal docTypeId As Long)
        Try
            outlookInstance.updateOutParams(mailItemFind)
            mailItemFind.SaveAs(nameFile.Replace(".msg", ".html"), Microsoft.Office.Interop.Outlook.OlSaveAsType.olHTML)
            mailItemFind.SaveAs(nameFile, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG)

            Dim m_Subject As String = Nothing
            Dim m_TO As String = Nothing
            Dim m_CC As String = Nothing
            Dim m_BCC As String = Nothing
            Dim m_Body As String = Nothing
            Dim m_Attachs As List(Of String) = Nothing

            outlookInstance.LoadParams(m_Subject, m_TO, m_CC, m_BCC, m_Body, m_Attachs)

            UserBusiness.Rights.SaveAction(0, ObjectTypes.ModuleMail, Zamba.Core.RightsType.EnviarPorMail, "Se ha enviado un mail desde el documento " & docId & "(dtid=" & docTypeId & ") con asunto '" & m_Subject & "'")

            MessagesBusiness.SaveHistory(m_TO, m_CC, m_BCC, m_Subject, m_Body, m_Attachs, docId, docTypeId, nameFile)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Historial de emails", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Shared Sub SaveParamInDB(ByVal toSend As String, ByVal subject As String, ByVal docId As Integer, ByVal docTypeId As Integer)
        Try
            Dim strQuery As New StringBuilder
            strQuery.Append("Insert into Zparamsitemmail ")
            strQuery.Append("(idUser,docId,doctypeId,toSend,subject) values (")
            strQuery.Append(Membership.MembershipHelper.CurrentUser.ID & ",")
            strQuery.Append(docId & ", ")
            strQuery.Append(docTypeId & ", '")
            strQuery.Append(toSend.ToString & "', '")
            strQuery.Append(subject.ToString & "')")
            Server.Con.ExecuteNonQuery(CommandType.Text, strQuery.ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Function SearchMailInDB() As DataTable
        Try
            Dim paramsItemsData As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "Select * from ZparamsItemMail where idUser = " & Membership.MembershipHelper.CurrentUser.ID.ToString)
            If paramsItemsData IsNot Nothing AndAlso paramsItemsData.Tables.Count > 0 Then
                Return paramsItemsData.Tables(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

End Class

