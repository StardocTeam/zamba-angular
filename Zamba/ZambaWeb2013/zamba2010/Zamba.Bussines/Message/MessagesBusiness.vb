Imports Zamba.Data
Imports System.Net.Mail
Imports Zamba.Core
Imports System.Collections.Generic
Imports System.Text
'Imports Zamba.Mail
'Imports Zamba.CommonLibrary
Imports System.Net
Imports System.IO
Imports Zamba.DataExt.WSResult.Consume
Imports Zamba.Membership
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Xml.Serialization
Imports Zamba.Servers
Imports Zamba.FileTools

Public Class MessagesBusiness

    Inherits ZClass

    Public Shared Function isAlreadyRead(ByVal msgid As Int32, ByVal usr As String) As Boolean
        Dim leido As Int32 = MessagesFactory.isAlreadyRead(msgid, usr)
        Return leido <> 0
    End Function

    Public Shared Function IsEmailHistoryEnabled() As Boolean
        Dim MailHistoryEnabled As Boolean
        Dim ZOPTB As New ZOptBusiness

        If Not Boolean.TryParse(ZOPTB.GetValue("MailHistoryEnabled"), MailHistoryEnabled) Then
            ZOPTB = Nothing
            Return False
        End If
        ZOPTB = Nothing
        Return MailHistoryEnabled
    End Function

    ''' [Alejandro] 05-03-2010 - Created
    ''' <summary>
    '''     Metodo para chequear que este configurada la ruta para guardar el historial de emails
    ''' </summary>
    Public Shared Sub CheckHistoryExportPath()
        Dim ZOPTB As New ZOptBusiness
        Dim Path As String = ZOPTB.GetValue("EMAILSPATH")

        If IsEmailHistoryEnabled() Then
            If String.IsNullOrEmpty(Path) Then
                Throw New Exception("No se ha configurado la ruta para exportar el historial de emails.")
            Else
                'Si se fuerza Web con Blob, no se realiza la verificacion del directorio
                Dim ForceBlob As String = ZOPTB.GetValue("ForceBlob")
                ZOPTB = Nothing

                If String.IsNullOrEmpty(ForceBlob) OrElse Not Boolean.Parse(ForceBlob) Then
                    If Not Directory.Exists(Path) Then
                        If Not Directory.CreateDirectory(Path).Exists Then
                            Throw New Exception("No existe la ruta configurada para exportar el historial de emails o no se tiene acceso a la misma.")
                        End If
                    End If
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
        If IsEmailHistoryEnabled() Then
            Dim Attachs As New List(Of String)

            If TypeOf msg Is ILotusMail Then
                Dim attachments As ArrayList = DirectCast(msg, ILotusMail).Attachments
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "LOTUS - Savehistory - Cantidad Adjuntos : " & attachments.Count)
                For Each attach As String In attachments
                    If String.IsNullOrEmpty(attach) Then
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "LOTUS - Savehistory - attach vacío")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "LOTUS - Savehistory - attach: " & attach)
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

            Email_Factory.SaveHistory(msg.MailTo, msg.CC, msg.CCO, msg.Subject, msg.Body, Attachs, docId, docTypeId, Membership.MembershipHelper.CurrentUser)
        End If
    End Function

    Public Shared Function SaveHistory(ByVal para As String,
                                       ByVal cc As String,
                                       ByVal cco As String,
                                       ByVal subject As String,
                                       ByVal body As String,
                                       ByVal attachs As List(Of String),
                                       ByVal docId As Int64,
                                       ByVal docTypeId As Int64,
                                       ByVal userId As Int64,
                                       ByVal exportPath As String,
                                       Optional ByVal MailPathVariable As String = "",
                                       Optional ByVal remitente As String = "") As Boolean
        Dim success As Boolean = False

        If IsEmailHistoryEnabled() Then
            Dim UserBusiness As New UserBusiness
            Dim user As IUser = UserBusiness.GetUserById(userId)
            UserBusiness = Nothing
            'TODO (Emiliano 29/8/22): Revisar si algo esta mal en el parametro pocional 'exportPath'
            'no se condice con la firma del metodo. Puse "" para coincidir
            'la invocacion al metodo con la nueva firma que ahora tiene un opcional mas.
            success = Email_Factory.SaveHistory(para, cc, cco, subject, body, attachs, docId, docTypeId, user, exportPath, "", MailPathVariable, remitente)
        End If

        Return success
    End Function

    ' [Alejandro] 02-12-2009 - Created
    Private Shared Function ListToString(ByVal recipients As List(Of String), Optional ByVal sep As String = ";") As String
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

    ' [Alejandro] 02-12-2009 - Created
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

        currentindex = TempHTML.IndexOf("<img")

        While currentindex <> -1

            srcstartindex = CInt(TempHTML.IndexOf("src=" & Chr(34), currentindex)) + 5
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
            If String.IsNullOrEmpty(fileFullPath) OrElse New FileInfo(fileFullPath).Exists = False Then
                Return String.Empty
            Else
                Dim newFileExtension As String
                Dim newFileName As String
                Const officeTemp As String = "\OfficeTemp\"

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ruta del mail adjunto: " & fileFullPath)

                'For Each invalidChar As Char In IO.Path.GetInvalidFileNameChars
                '    fileName = fileName.Replace(invalidChar, String.Empty)
                'Next
                'For Each invalidChar As Char In IO.Path.GetInvalidPathChars
                '    fileFullPath = fileFullPath.Replace(invalidChar, String.Empty)
                'Next

                If Not IO.Directory.Exists(Zamba.Membership.MembershipHelper.AppTempDir(String.Empty).FullName & officeTemp) Then
                    IO.Directory.CreateDirectory(Zamba.Membership.MembershipHelper.AppTempDir(String.Empty).FullName & officeTemp)
                End If

                newFileName = fileFullPath
                newFileExtension = IO.Path.GetExtension(newFileName)

                newFileName = Membership.MembershipHelper.AppTempDir(String.Empty).FullName & officeTemp & fileName.Replace("/", " ").Replace("\", " ").Replace(":", " ")
                If String.IsNullOrEmpty(IO.Path.GetExtension(newFileName)) Then
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

                newFileName = newFileName.Replace("\\", "\")
                IO.File.Copy(fileFullPath, newFileName, True)

                Return newFileName
            End If
        Catch ex As Exception
            raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Public Shared Function CheckMessages() As Integer
        Try
            Dim messages As Integer = MessagesFactory.countNewMessages(Zamba.Membership.MembershipHelper.CurrentUser.ID)
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
            body.AppendLine()
            body.AppendLine()
            Dim indexText As String = GenerateIndexText(indexsNames, currentResult)
            body.Append(indexText)
        End If

        Dim mail As New SendMailConfig

        Try
            mail.LoadMailData(automail)
            mail.LoadSmtpData(smtp)
            mail.MailType = MailTypes.NetMail
            mail.IsBodyHtml = True
            mail.LinkToZamba = AddLink
            MessagesBusiness.SendQuickMail(mail)
        Catch ex As SmtpException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)

        Finally
            mail.Dispose()
            mail = Nothing
        End Try
    End Sub

    Private Shared Function GenerateIndexText(ByVal indexsNames As List(Of String),
                                              ByVal currentResult As Result) As String
        Dim TextoIndices As New System.Text.StringBuilder
        TextoIndices.Append("Indices: ")
        TextoIndices.AppendLine()
        Dim indexData As String

        If indexsNames IsNot Nothing AndAlso indexsNames.Count > 0 Then
            For Each index As Core.Index In currentResult.Indexs
                For Each name As String In indexsNames
                    If String.Compare(name, index.Name, True) = 0 Then
                        indexData = GetIndexData(index)
                        TextoIndices.Append(index.Name.Trim.ToUpper)
                        TextoIndices.Append(": ")
                        TextoIndices.Append(indexData)
                        TextoIndices.AppendLine()
                        Exit For
                    End If
                Next
            Next
        ElseIf indexsNames.Count = 0 Then
            For Each index As Core.Index In currentResult.Indexs
                indexData = GetIndexData(index)
                TextoIndices.Append(index.Name.Trim.ToUpper)
                TextoIndices.Append(": ")
                TextoIndices.Append(indexData)
                TextoIndices.AppendLine()
            Next
        End If

        indexData = TextoIndices.ToString
        TextoIndices.Remove(0, TextoIndices.Length)
        TextoIndices = Nothing

        Return indexData
    End Function

    Private Shared Function GetIndexData(ByRef index As Index) As String
        Dim indexData As String = index.Data
        If String.IsNullOrEmpty(indexData) Then
            indexData = index.Data2
        End If

        indexData &= " " & index.dataDescription
        If String.IsNullOrEmpty(indexData) Then
            indexData = index.dataDescription2
        End If

        Return indexData
    End Function

    ''' <summary>
    ''' Envia un mail, partiendo de una plantilla de mail (Automail).
    ''' El envio puede ser por smtp autenticado.
    ''' </summary>
    ''' <param name="automail">Plantilla de mail(Automail).</param>
    ''' <param name="smtpAutenticado">Datos para la validacion para un envio SMTP Autenticado.</param>    
    ''' <remarks> El parametro smtpAutenticado se debe pasar 
    ''            como nulo si el mail no requiere autenticacion.</remarks>
    Public Overloads Shared Sub AutoMail_SMTP(ByVal automail As AutoMail, ByVal smtpAutenticado As SMTP_Validada)
        If smtpAutenticado Is Nothing Then
            Dim m As IMailMessage
            Try
                m = MessagesBusiness.GetMessage(Zamba.Core.MailTypes.NetMail)

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
            Finally
                m = Nothing
            End Try

        Else
            Dim mf As New Message_Factory
            Dim mail As SendMailConfig = Nothing

            Try
                If String.IsNullOrEmpty(smtpAutenticado.Server) Then
                    Dim UB As New UserBusiness()
                    Dim server As ICorreo = UB.FillUserMailConfig(Zamba.Membership.MembershipHelper.CurrentUser.ID)
                    smtpAutenticado.Server = server.ProveedorSMTP
                    server = Nothing
                End If

                mail = New SendMailConfig()
                mail.LoadMailData(automail)
                mail.LoadSmtpData(smtpAutenticado)
                mail.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID
                mail.MailType = MailTypes.NetMail
                mail.SaveHistory = IsEmailHistoryEnabled()

                mf.SendMailNet(mail)

            Catch ex As SmtpException
                ZClass.raiseerror(ex)
            Finally
                mf = Nothing
                If mail IsNot Nothing Then
                    mail.Dispose()
                    mail = Nothing
                End If
            End Try
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
        Dim con As IConnection = Nothing
        Dim ir As IDataReader
        Dim Am As New AutoMail

        Try
            con = Zamba.Servers.Server.Con
            ir = con.ExecuteReader(CommandType.Text, strselect)

            If Not ir.Read() Then
                ir.Close()
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
            If IsNothing(ir) = False Then
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

    ''' <summary>
    ''' </summary>
    ''' <history> Ezequiel Modified 27/01/2009 - Se modifico la asignacion de parametros ya que tiraba muchas excepciones. 
    '''           Javier   Modified 17/11/2010 - Se agrega parametro basemail utilizado en lotus.
    ''' </history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SendQuickMail(ByVal mail As ISendMailConfig) As Boolean
        Dim UB As New UserBusiness
        Dim UserId As Int64 = Zamba.Membership.MembershipHelper.CurrentUser.ID
        Dim Correo As ICorreo = UB.FillUserMailConfig(UserId)
        If Correo IsNot Nothing Then
            Dim ProveedorSMTP As String = Correo.ProveedorSMTP
            Dim puerto As String = Correo.Puerto
            Dim eMailType As Int32 = Correo.Type
            Dim from As String = Correo.Mail
            Dim UserName As String = Correo.UserName
            Dim Password As String = Correo.Password
            Dim BaseMail As String = Correo.Base
            Dim enableSsl As Boolean = Correo.EnableSsl

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Proveedor SMTP: " & ProveedorSMTP)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Puerto: " & puerto)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Tipo Mail: " & eMailType.ToString())
            ZTrace.WriteLineIf(ZTrace.IsInfo, "From: " & from)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre Usuario: " & UserName)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Base: " & BaseMail)

            mail.Basemail = Correo.Base
            mail.EnableSsl = Correo.EnableSsl
            mail.SMPTPassword = Correo.Password
            mail.SMTPServer = Correo.ProveedorSMTP
            mail.SMTPPort = Correo.Puerto
            mail.SMTPServer = Correo.ProveedorSMTP
            mail.SMTPUserName = Correo.UserName
            mail.From = Correo.Mail


            Return SendMail(mail)
        Else
            Throw New Exception("El usuario no tiene configurada correctamente los datos del mail")
        End If
    End Function

    ''' <summary>
    ''' Realiza el envio de mail normal pero si useWebService está en true, usará WSResults para llevar a cabo la accion
    ''' </summary>
    Public Shared Function SendMail(ByVal conf As ISendMailConfig, Optional ByVal MailPathVariable As String = "") As Boolean
        If conf.LinkToZamba Then
            conf.Body += MakeHtmlLink(0, 0, conf.SourceDocId, conf.SourceDocTypeId, conf.TaskName)
        End If

        conf.Body = FormatHTMLBody(conf.Body)

        Dim returnVal As Boolean = False
        If conf.UseWebService Then
            Using wsFactory As New WSResultsFactory()
                Try
                    returnVal = SendMailByWS(conf, wsFactory)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End Using
        Else
            Dim mf As New Message_Factory
            returnVal = mf.SendMailNet(conf)
            mf = Nothing
        End If
        If conf.AttachFileNames.Count = 0 Then
            For Each FileAttach As Attachment In conf.Attachments
                conf.AttachFileNames.Add(FileAttach.Name)
            Next
        End If

        If returnVal Then
            Try
                If conf.SaveHistory Then
                    If conf.UseWebService Then
                        Dim mB As New MessagesBusiness
                        returnVal = mB.SaveMailHistoryWS(conf.MailTo, conf.Cc, conf.Cco, conf.Subject, conf.Body, conf.AttachFileNames, conf.SourceDocId, conf.SourceDocTypeId, conf.UserId, String.Empty)

                        mB = Nothing
                    Else

                        returnVal = SaveHistory(conf.MailTo, conf.Cc, conf.Cco, conf.Subject, conf.Body,
                         conf.AttachFileNames, conf.SourceDocId, conf.SourceDocTypeId, conf.UserId, String.Empty, MailPathVariable, conf.From)
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        Else
            'Guardo el Mail para reintentar mas tarde.
            QuequeMail(conf)
        End If

        Return returnVal
    End Function


    ''' <summary>
    ''' Realiza el envio de mail normal pero si useWebService está en true, usará WSResults para llevar a cabo la accion
    ''' </summary>
    Public Shared Function SendMailbyZip(ByVal conf As ISendMailConfig, Optional ByVal MailPathVariable As String = "") As Boolean
        If conf.LinkToZamba Then
            conf.Body += MakeHtmlLink(0, 0, conf.SourceDocId, conf.SourceDocTypeId, conf.TaskName)
        End If

        conf.Body = FormatHTMLBody(conf.Body)

        Dim returnVal As Boolean = False
        If conf.UseWebService Then
            Using wsFactory As New WSResultsFactory()
                Try
                    returnVal = SendMailByWS(conf, wsFactory)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End Using
        Else
            Dim mf As New Message_Factory
            returnVal = mf.SendMailNet(conf)
            mf = Nothing
        End If

        Return returnVal
    End Function


    Public Shared Function SendMailbyZipHistory(ByVal conf As ISendMailConfig, Optional ByVal MailPathVariable As String = "") As Boolean

        Dim returnVal As Boolean = True

        If returnVal Then
            Try
                If conf.SaveHistory Then
                    If conf.UseWebService Then
                        Dim mB As New MessagesBusiness
                        returnVal = mB.SaveMailHistoryWS(conf.MailTo, conf.Cc, conf.Cco, conf.Subject, conf.Body, conf.AttachFileNames, conf.SourceDocId, conf.SourceDocTypeId, conf.UserId, String.Empty)

                        mB = Nothing
                    Else
                        returnVal = SaveHistory(conf.MailTo, conf.Cc, conf.Cco, conf.Subject, conf.Body,
                         Nothing, conf.SourceDocId, conf.SourceDocTypeId, conf.UserId, String.Empty, MailPathVariable)
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        Else
            'Guardo el Mail para reintentar mas tarde.
            QuequeMail(conf)
        End If

        Return returnVal
    End Function

    Private Shared Function SendMailByWS(conf As ISendMailConfig, wsFactory As WSResultsFactory) As Boolean
        Dim returnVal As Boolean = False

        If conf.Attaches Is Nothing Then
            returnVal = wsFactory.ConsumeZSendMail(conf.From, conf.SMTPServer, conf.SMTPPort, conf.SMTPUserName,
                                                   conf.SMPTPassword, conf.MailTo, conf.Cc, conf.Cco, conf.Subject,
                                                   conf.Body, conf.AttachFileNames, conf.UserId, conf.OriginalDocument,
                                                   conf.OriginalDocumentFileName, conf.EnableSsl)
        Else
            Dim wsAttaches As New List(Of Zamba.DataExt.WSResults.BlobDocument)
            Dim blobDoc As Zamba.DataExt.WSResults.BlobDocument
            For Each attach As BlobDocument In conf.Attaches
                blobDoc = New Zamba.DataExt.WSResults.BlobDocument()
                blobDoc.BlobFile = attach.BlobFile
                blobDoc.Description = attach.Description
                blobDoc.ID = attach.ID
                blobDoc.Name = attach.Name
                blobDoc.UpdateDate = attach.UpdateDate
                blobDoc.Updateuser = attach.Updateuser
                wsAttaches.Add(blobDoc)
            Next

            returnVal = wsFactory.ConsumeZSendMailWithAttaches(conf.From, conf.SMTPServer, conf.SMTPPort, conf.SMTPUserName,
                                                          conf.SMPTPassword, conf.MailTo, conf.Cc, conf.Cco,
                                                          conf.Subject, conf.Body, wsAttaches, conf.UserId, conf.EnableSsl)
        End If

        Return returnVal
    End Function

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
            Return Zamba.Membership.MembershipHelper.CurrentUser.eMail.ProveedorSMTP
        End Get
    End Property

    Public Overrides Sub Dispose()

    End Sub

    ''' <summary>
    ''' Obtiene el mensaje en bytes 
    ''' </summary>
    ''' <param name="url">Url del html o msg en el servidor</param>
    ''' <returns>Documento en un array de bytes</returns>
    ''' <remarks>El único id que se puede encontrar es la ruta del documento que será única</remarks>
    Public Shared Function GetMessageFile(ByVal id As Int64) As Byte()
        Return MessagesFactory.GetMessageFile(id)
    End Function

    Public Shared Function GetMessage(ByVal id As Int64) As SendMailConfig
        Dim dt As DataTable = MessagesFactory.GetMessage(id)

        If dt.Rows.Count > 0 Then
            Dim drMail As DataRow = dt.Rows(0)
            Dim mail As New SendMailConfig()

            With mail
                '.Fecha =  drMail.Item("FECHA") - NO EXISTE ESTA PROPIEDAD
                .UserId = drMail.Item("USR_ZMB")
                '.Puesto =  drMail.Item("USR_PC") - NO EXISTE ESTA PROPIEDAD
                .SourceDocId = drMail.Item("DOC_ID")
                .SourceDocTypeId = drMail.Item("DOC_TYPE")
                .MailDateTime = Convert.ToDateTime(drMail.Item("FECHA"))
                .MailTo = drMail.Item("MSG_TO")
                .Cc = drMail.Item("MSG_CC")
                .Cco = drMail.Item("MSG_BCC")
                .Subject = drMail.Item("MSG_SUBJECT")
                .OriginalDocumentFileName = drMail.Item("PATH")

                If drMail.Item("EncodeFile").ToString() <> String.Empty Then
                    .OriginalDocument = drMail.Item("EncodeFile")
                End If

                '.ID = drMail.Item("ID") - NO EXISTE ESTA PROPIEDAD
            End With

            dt.Dispose()
            dt = Nothing
            Return mail
        Else
            dt.Dispose()
            dt = Nothing
            Return Nothing
        End If
    End Function

    Public Shared Sub SaveMessageFile(ByVal file As Byte(), ByVal id As Int64)
        MessagesFactory.SaveMessageFile(file, id)
    End Sub

    ''' <summary>
    ''' Llama al WS para guardar el mail en historial
    ''' </summary>
    ''' <param name="to"></param>
    ''' <param name="cC"></param>
    ''' <param name="cCO"></param>
    ''' <param name="subject"></param>
    ''' <param name="body"></param>
    ''' <param name="attachs"></param>
    ''' <param name="docId"></param>
    ''' <param name="docTypeID"></param>
    ''' <param name="userID"></param>
    ''' <param name="exportPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveMailHistoryWS(ByVal [to] As String, ByVal cC As String, ByVal cCO As String, ByVal subject As String, ByVal body As String, ByVal attachs As List(Of String), ByVal docId As Long, ByVal docTypeID As Long, ByVal userID As Long, ByVal exportPath As String) As Boolean
        Dim wsFactory As New WSResultsFactory()
        Dim returnVal As Boolean = False
        Try
            returnVal = wsFactory.ConsumeSaveMailHistory([to], cC, cCO, subject, body, attachs, docId, docTypeID, userID, exportPath)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            wsFactory.Dispose()
        End Try

        Return returnVal
    End Function

    ''' <summary>
    ''' Obtiene el mail de historial llamando al WS
    ''' </summary>
    ''' <param name="url"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMailWS(ByVal id As Int64, ByVal userId As Long) As Byte()
        Dim wsFactory As New WSResultsFactory()
        Dim returnVal As Byte() = Nothing
        Try
            returnVal = wsFactory.ConsumeGetMail(id, userId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            wsFactory.Dispose()
        End Try

        Return returnVal
    End Function

    ''' <summary>
    ''' Obtiene el link que se incluye en el cuerpo de los mails para poder acceder a documentos y tareas
    ''' </summary>
    ''' <param name="taskId"></param>
    ''' <param name="wfStepId"></param>
    ''' <param name="docId"></param>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MakeHtmlLink(taskId As Int64,
                                 wfStepId As Int64,
                                 docId As Int64,
                                 docTypeId As Int64, TaskName As String) As String
        Dim html As String

        Dim zopt As New ZOptBusiness
        html = $"<h4>{TaskName}</h4>"
        html += GetHtmlLinkWithZOpt(docId, docTypeId, taskId, wfStepId)
        Dim webPath As Object = zopt.GetValue("MultiPlatformMailLinks")
        zopt = Nothing
        If webPath IsNot Nothing AndAlso Boolean.Parse(webPath) Then
            Dim RB As New Results_Business
            html += "Link Cliente Desktop: "
            html += RB.GetHtmlLinkFromResult(docTypeId, docId)
            RB = Nothing
        End If
        webPath = Nothing

        Return html
    End Function

    Private Shared Function GetHtmlLinkWithZOpt(docId As Int64, docTypeId As Int64, taskId As Int64, wfStepId As Int64) As String
        Dim url As String = ZOptBusiness.GetValueOrDefault("ThisDomainPublic", "https://zamba.com.ar/bpm")
        Dim result As String

        If (url.Length > 0) Then
            result = String.Format("<br><br>Link Cliente Web: <a href=""{0}/Views/WF/TaskSelector.ashx?docid={1}&doctype={2}&taskid={3}&wfstepid={4}"">Acceder al documento</a><br>", url, docId, docTypeId, taskId, wfStepId)
        End If

        Return result
    End Function

    Private Shared Function FormatHTMLBody(ByVal htmlBody As String) As String
        Return htmlBody.Replace(vbCrLf, "<br>")
    End Function

    Private Shared Sub QuequeMail(conf As ISendMailConfig)
        Dim xml_serializer As New XmlSerializer(conf.GetType())
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

            If SendMail(conf) Then
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("update  ZQM set state = 2 where id = {0}", Id))
            Else
                Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("update  ZQM set state = 3 where id = {0}", Id))
            End If

        End If


    End Sub


    Public Function ExtractAttachsAsFilesWithSpire(spireTool As ISpireTools, MsgFile As String, newFilesDirectory As String, extensions As List(Of String)) As List(Of String)

        Dim spTools As ISpireTools = spireTool

        Dim _attachs As New List(Of String)
        Dim Attachs As New Dictionary(Of String, Stream)()

        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Extrayendo adjuntos del msg: {0}", MsgFile))
        Attachs = spTools.GetEmailAttachs(MsgFile)
        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Adjuntos encontrados {0}", Attachs.Count))

        If Attachs.Count > 0 Then

            If Not Directory.Exists(newFilesDirectory) Then
                Directory.CreateDirectory(newFilesDirectory)
            End If

            Dim AllowedExtension As Boolean
            Dim storeName As String
            For Each attach As KeyValuePair(Of String, Stream) In Attachs
                Try
                    'esta viendo si el mail esta entre las extensiones permitidas, ver si se puede simplificar
                    AllowedExtension = False
                    If extensions.Count > 0 Then
                        For Each ext As String In extensions
                            If attach.Key.ToLower.EndsWith(ext.ToLower) Then
                                AllowedExtension = True
                            End If
                        Next
                    Else
                        AllowedExtension = True
                    End If


                    If AllowedExtension Then
                        storeName = Path.Combine(newFilesDirectory, attach.Key)
                        Dim i As Int64 = 0
                        While File.Exists(storeName)
                            i += 1
                            storeName = Path.Combine(newFilesDirectory, Path.GetFileNameWithoutExtension(attach.Key) & "(" & i & ")" & Path.GetExtension(attach.Key))
                        End While
                        If storeName.Length > 254 Then
                            storeName = storeName.Substring(0, 254 - Path.GetExtension(attach.Key).Length) + Path.GetExtension(attach.Key)
                        End If

                        'Guarda el adjunto
                        Using fs = File.Create(storeName)
                            attach.Value.CopyTo(fs)
                        End Using
                        _attachs.Add(storeName)
                    End If

                Catch ex As Exception
                    raiseerror(ex)
                End Try
            Next

        End If
        Return _attachs
    End Function

    Public Function ExtractFirstAttachsInMailWithSpire(spireTool As ISpireTools, MsgFile As String, newFilesDirectory As String, extensions As List(Of String)) As String

        Dim spTools As ISpireTools = spireTool

        Dim _attachs As New List(Of String)
        Dim Attachs As New Dictionary(Of String, Stream)()

        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Extrayendo adjuntos del msg: {0}", MsgFile))
        Attachs = spTools.GetEmailAttachs(MsgFile)
        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Adjuntos encontrados {0}", Attachs.Count))

        If Attachs.Count > 0 Then

            If Not Directory.Exists(newFilesDirectory) Then
                Directory.CreateDirectory(newFilesDirectory)
            End If

            Dim AllowedExtension As Boolean
            Dim storeName As String
            For Each attach As KeyValuePair(Of String, Stream) In Attachs
                Try
                    'esta viendo si el mail esta entre las extensiones permitidas, ver si se puede simplificar
                    AllowedExtension = False
                    If extensions.Count > 0 Then
                        For Each ext As String In extensions
                            If attach.Key.ToLower.EndsWith(ext.ToLower) Then
                                AllowedExtension = True
                            End If
                        Next
                    Else
                        AllowedExtension = True
                    End If


                    If AllowedExtension Then
                        storeName = Path.Combine(newFilesDirectory, attach.Key)
                        Dim i As Int64 = 0
                        While File.Exists(storeName)
                            i += 1
                            storeName = Path.Combine(newFilesDirectory, Path.GetFileNameWithoutExtension(attach.Key) & "(" & i & ")" & Path.GetExtension(attach.Key))
                        End While
                        If storeName.Length > 254 Then
                            storeName = storeName.Substring(0, 254 - Path.GetExtension(attach.Key).Length) + Path.GetExtension(attach.Key)
                        End If

                        'Guarda el adjunto
                        Using fs = File.Create(storeName)
                            attach.Value.CopyTo(fs)
                        End Using
                        Return storeName
                    End If

                Catch ex As Exception
                    raiseerror(ex)
                End Try
            Next

        End If

        Return String.Empty

    End Function

    Public Function CountAttachsAsFilesWithSpire(spireTool As ISpireTools, MsgFile As String, extensions As List(Of String)) As Int64

        Dim spTools As ISpireTools = spireTool
        Dim attachsCount As Long

        Try
            If Not String.IsNullOrEmpty(MsgFile) Then

                If Not extensions.Count > 0 Then

                    attachsCount = spTools.GetEmailAttachsCount(MsgFile)

                Else

                    Dim attachs As List(Of String) = spTools.GetEmailAttachsNames(MsgFile)

                    For Each _attach As String In attachs
                        For Each ext As String In extensions
                            If _attach.ToLower.EndsWith(ext.ToLower) Then
                                attachsCount += 1
                            End If
                        Next
                    Next

                End If

                Return attachsCount
            End If

        Catch ex As Exception
            raiseerror(ex)
        End Try

        Return 0

    End Function


End Class

