Imports System.Diagnostics
Imports Zamba.OfficeCommon

Namespace Outlook
    Public Class OutlookInterop

#Region "Atributos y Propiedades"
        Private flag As Boolean = False
        Private itemSent As Boolean = False
        Private mailTempPath As String = String.Empty
        Private myEntryId As String = String.Empty
        Dim app As Microsoft.Office.Interop.Outlook.Application = Nothing
        Dim outlookNS As Microsoft.Office.Interop.Outlook.NameSpace = Nothing
        Dim MAPIFolderSentMail As Microsoft.Office.Interop.Outlook.MAPIFolder = Nothing
        Public Event CloseMailItemEvent()
        Dim _mail As Microsoft.Office.Interop.Outlook.MailItem = Nothing
        Dim _closeFromControlbox As Boolean = False
        Dim _disposingParent As Boolean = False
        Dim _SendingMailItem As Boolean = False
        Dim _params As New Hashtable
        Dim _sentMailItem As Microsoft.Office.Interop.Outlook.MailItem = Nothing
        Dim _waitForSend As Boolean
        Dim _activeInspectorCaption As String
        Dim _inspectorMaximized As Boolean
        Dim _mailItem As Microsoft.Office.Interop.Outlook.MailItem
        'Se configuran con SetHeaderLanguage
        Dim htmlFrom, htmlSent, htmlTo, htmlSubject As String


        Property ActiveInspectorCaption() As String
            Get
                Return _activeInspectorCaption
            End Get
            Set(ByVal value As String)
                _activeInspectorCaption = value
            End Set
        End Property

        Property closeFromControlbox() As Boolean
            Get
                Return _closeFromControlbox
            End Get
            Set(ByVal value As Boolean)
                _closeFromControlbox = value
            End Set
        End Property

        Property DisposingParent() As Boolean
            Get
                Return _disposingParent

            End Get
            Set(ByVal value As Boolean)
                _disposingParent = value
            End Set
        End Property
#End Region

        Public Sub New(ByRef ap As Microsoft.Office.Interop.Outlook.Application, ByRef ns As Microsoft.Office.Interop.Outlook.NameSpace, ByRef mapi As Microsoft.Office.Interop.Outlook.MAPIFolder)
            app = ap
            outlookNS = ns
            MAPIFolderSentMail = mapi
            SetHeaderLanguage()
        End Sub

        Public Function OpenMailItem(ByVal mailItemPath As String, ByVal modal As Boolean, Optional ByVal winState As FormWindowState = FormWindowState.Maximized, Optional ByVal UpdateCaption As Boolean = False) As Boolean

            Dim ret As Boolean = False
            Dim FI As New IO.FileInfo(mailItemPath)

            Trace.WriteLine("Opening MailItem: " & mailItemPath)
            _mail = DirectCast(app.Session.OpenSharedItem(mailItemPath), Microsoft.Office.Interop.Outlook.MailItem)
            AddHandler _mail.Close, AddressOf CloseMailItemEventHandler

            If UpdateCaption Then
                Try
                    _mail.Subject = "Zamba_" & FI.Name.Replace(".msg", "").Split("_")(1) & " | " & _mail.Subject
                    Trace.WriteLine("Se cambio el caption del mensaje a: " & _mail.Subject)
                Catch ex As Exception
                    Trace.WriteLine("No se pudo cambiar el caption del mensaje")
                End Try
            End If

            _activeInspectorCaption = _mail.GetInspector.Caption
            _mail.Display(modal)

            Select Case winState
                Case FormWindowState.Maximized
                    _mail.GetInspector.WindowState = Microsoft.Office.Interop.Outlook.OlWindowState.olMaximized
                Case FormWindowState.Minimized
                    _mail.GetInspector.WindowState = Microsoft.Office.Interop.Outlook.OlWindowState.olMinimized
                Case FormWindowState.Normal
                    _mail.GetInspector.WindowState = Microsoft.Office.Interop.Outlook.OlWindowState.olNormalWindow
            End Select

            Trace.WriteLine("Item opened")
            ret = True
            Return ret
        End Function

        Public Function CloseMailItem() As Boolean
            Dim ret As Boolean = False
            If Not IsNothing(_mail) Then
                Trace.WriteLine("Closing MailItem")
                _mail.Close(Microsoft.Office.Interop.Outlook.OlInspectorClose.olDiscard)
                Trace.WriteLine("MailItem was closed successfully")
                ret = True

            End If
            Return ret
        End Function

        Private Sub CloseMailItemEventHandler(ByRef Cancel As Boolean)
            'System.Runtime.InteropServices.Marshal.ReleaseComObject(_mail)
            '_mail = Nothing
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            'GC.Collect()
            'Threading.Thread.CurrentThread.Sleep(1000)


            If Not _disposingParent Then
                closeFromControlbox = True
                RaiseEvent CloseMailItemEvent()
                Trace.WriteLine("CloseMailItemEvent was raised")
            End If
        End Sub
        Private Sub CloseMailItemEventHandler(ByVal SaveMode As Microsoft.Office.Interop.Outlook.OlInspectorClose)
            'System.Runtime.InteropServices.Marshal.ReleaseComObject(_mail)
            '_mail = Nothing
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            'GC.Collect()
            'Threading.Thread.CurrentThread.Sleep(1000)


            If Not DisposingParent Then
                closeFromControlbox = True
                RaiseEvent CloseMailItemEvent()
                Trace.WriteLine("CloseMailItemEvent was raised")
            End If
        End Sub

        Public Function GetNewMailItem(ByVal tempPath As String, Optional ByVal modal As Boolean = True, Optional ByVal waitForSend As Boolean = True, Optional ByRef Params As Hashtable = Nothing, Optional ByVal automaticSend As Boolean = False) As Boolean

            Dim isReply As Boolean = False
            Dim ret As Boolean = False
            Dim timeOut As Integer = 20
            Dim replyMsgPath As String = String.Empty
            Dim UserProp As Microsoft.Office.Interop.Outlook.UserProperty

            'Se muestra el outlook
            If IsNothing(app.ActiveExplorer()) Then outlookNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox).Display()

            'Se crea el mail a enviar
            _mailItem = app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem)
            AddHandler _mailItem.Close, AddressOf CloseMailItemEventHandler
            AddHandler _mailItem.Send, AddressOf SendingMailItemHandler
            AddHandler _mailItem.Open, AddressOf InspectorActivateHandler

            mailTempPath = tempPath
            _waitForSend = waitForSend
            myEntryId = Guid.NewGuid().ToString()
            UserProp = _mailItem.UserProperties.Add(OutlookMailParameters.ITEMGUID, Microsoft.Office.Interop.Outlook.OlUserPropertyType.olText)
            UserProp.Value = myEntryId

            If Params IsNot Nothing Then
                replyMsgPath = Params(OutlookMailParameters.REPLY_MSG_PATH)

                'Verifica si el mail debe ser una respuesta y si tiene una ruta cargada.
                If Params(OutlookMailParameters.IS_REPLY) IsNot Nothing AndAlso Boolean.Parse(Params(OutlookMailParameters.IS_REPLY)) _
                AndAlso Not String.IsNullOrEmpty(replyMsgPath) Then

                    'Se crea un mailItem mediante el msg.
                    Dim tempMail As Microsoft.Office.Interop.Outlook.MailItem = app.CreateItemFromTemplate(replyMsgPath)

                    'Agrega el encabezado de las respuestas con los datos.
                    AddReplyHeader(tempMail)

                    'Se configura el mail a enviar con los datos obtenidos.
                    With _mailItem
                        .To = tempMail.SenderEmailAddress
                        .CC = tempMail.CC
                        .BCC = tempMail.BCC
                        .Subject = tempMail.Subject
                        .Body = tempMail.Body
                        .BodyFormat = tempMail.BodyFormat
                        .HTMLBody = tempMail.HTMLBody
                    End With

                    tempMail = Nothing
                    isReply = True

                Else
                    _mailItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML

                    If Not IsNothing(Params(OutlookMailParameters.TO)) Then _mailItem.To = Params(OutlookMailParameters.TO).ToString()
                    If Not IsNothing(Params(OutlookMailParameters.CC)) Then _mailItem.CC = Params(OutlookMailParameters.CC).ToString()
                    If Not IsNothing(Params(OutlookMailParameters.BCC)) Then _mailItem.BCC = Params(OutlookMailParameters.BCC).ToString()
                    If Not IsNothing(Params(OutlookMailParameters.SUBJECT)) Then _mailItem.Subject = Params(OutlookMailParameters.SUBJECT).ToString()
                    If Not IsNothing(Params(OutlookMailParameters.BODY)) Then _mailItem.Body = Params(OutlookMailParameters.BODY).ToString()

                    Dim strHtmlLower As String = _mailItem.HTMLBody.ToLower()
                    Dim iniBodyTag As Integer = strHtmlLower.ToLower().IndexOf("<body")
                    Dim iniBodyTagClose As String = strHtmlLower.Substring(iniBodyTag).IndexOf(">") + 1
                    Dim totIndexOfBodyTag As Integer = iniBodyTag + iniBodyTagClose
                    Dim endBodyTab As Integer = _mailItem.HTMLBody.ToLower().IndexOf("</body>")
                    Dim strBody As String = _mailItem.HTMLBody.Substring(totIndexOfBodyTag, endBodyTab - totIndexOfBodyTag)
                    Dim strAux As String = String.Empty

                    If Not IsNothing(Params(OutlookMailParameters.HTML_BODY)) Then strAux = Params(OutlookMailParameters.HTML_BODY).ToString
                    If Not IsNothing(Params(OutlookMailParameters.HTML_LINK)) Then strAux &= Params(OutlookMailParameters.HTML_LINK).ToString
                    _mailItem.HTMLBody = _mailItem.HTMLBody.Replace(strBody, strAux)

                    If Not IsNothing(Params(OutlookMailParameters.ATTACH_PATHS)) Then
                        Dim PathImages() As String = DirectCast(Params(OutlookMailParameters.ATTACH_PATHS), String())

                        For Each attachPath As String In PathImages
                            If String.Compare(attachPath.Trim().ToLower(), "false") <> 0 AndAlso Not String.IsNullOrEmpty(attachPath.Trim()) Then
                                _mailItem.Attachments.Add(attachPath, Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue, 1, IO.Path.GetFileNameWithoutExtension(attachPath))
                            End If
                        Next
                    End If

                End If

                If Not IsNothing(Params(OutlookMailParameters.SEND_TIMEOUT)) Then timeOut = Convert.ToInt32(Params(OutlookMailParameters.SEND_TIMEOUT))

                'guardar los parametros originales
                _params = Params
            End If

            '_mailItem.Save()
            flag = waitForSend
            itemSent = False
            _SendingMailItem = False
            _inspectorMaximized = False

            Application.DoEvents()
            _activeInspectorCaption = _mailItem.GetInspector.Caption

            'Dim estadoActual As Outlook.OlWindowState
            'estadoActual = app.ActiveExplorer().WindowState
            'app.ActiveExplorer().Activate()
            'app.ActiveExplorer().Display()
            'Threading.Thread.Sleep(500)
            'app.ActiveExplorer().WindowState = Outlook.OlWindowState.olMaximized

            If Not automaticSend Then
                _mailItem.Display(modal)
            Else
                _mailItem.Send()
            End If

            ' app.ActiveExplorer().WindowState = estadoActual

            If waitForSend Then
                Dim timeOutCount As Integer = 0
                Dim ofrmSending As frmSending = New frmSending()

                If Not automaticSend AndAlso _SendingMailItem Then
                    ofrmSending.ShowSendingForm(timeOut)
                End If

                While itemSent = False AndAlso timeOutCount <= timeOut AndAlso _SendingMailItem
                    'Obtengo todos los mails enviados
                    Dim SentMailItems As Microsoft.Office.Interop.Outlook.Items = MAPIFolderSentMail.Items
                    Dim countItems As Integer = 10
                    'Los ordeno por fecha de envio de forma descendente
                    SentMailItems.Sort("[SentOn]", True)

                    Try
                        If SentMailItems.Count < 10 Then countItems = SentMailItems.Count

                        'Reviso si el mail enviado esta entre los primeros 10
                        For index As Integer = 1 To countItems
                            If TypeOf (SentMailItems.Item(index)) Is Microsoft.Office.Interop.Outlook.MailItem Then
                                Dim SentMailItem As Microsoft.Office.Interop.Outlook.MailItem = SentMailItems.Item(index)

                                'Si el mail es el mismo que estoy esperando...
                                If SentMailItem.UserProperties.Item(OutlookMailParameters.ITEMGUID).Value = myEntryId Then
                                    SentMailItemHandler(SentMailItem)
                                    Exit For
                                End If
                            End If
                        Next
                    Catch ex As Exception
                        Threading.Thread.Sleep(1000)
                    End Try

                    Trace.WriteLine("Espero 1000ms")
                    Threading.Thread.Sleep(1000)

                    timeOutCount = timeOutCount + 1

                    If Not automaticSend AndAlso _SendingMailItem Then
                        ofrmSending.ProgressPerformStep()
                    End If
                End While

                If Not automaticSend AndAlso _SendingMailItem Then
                    ofrmSending.Close()
                End If

                If timeOut > 0 Then
                    If timeOutCount >= timeOut Then
                        MsgBox("El e-mail no ha podido ser enviado.   " & Chr(13) & "Verificar la configuración de la cuenta de e-mail en Outlook. ", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Error al enviar...")
                    End If
                Else
                    itemSent = True
                End If

            Else
                itemSent = True
            End If

            Params = _params

            Return itemSent

        End Function



        ''' <summary>
        ''' Agrega el encabezado de una respuesta a un mail existente.
        ''' </summary>
        ''' <param name="mail">MailItem al que se le agregará el encabezado de respuesta</param>
        ''' <remarks></remarks>
        ''' <history>
        '''     Tomas   15/06/2010  Created
        ''' <history>
        Public Sub AddReplyHeader(ByVal mail As Microsoft.Office.Interop.Outlook.MailItem)
            Dim sb As New System.Text.StringBuilder()
            Dim indexPosition As Int32
            Dim fec As DateTime = mail.SentOn
            Dim sent As String = fec.ToLongDateString()
            Dim firstChar As String

            'Dado que el primer caracter viene en minúsculas, se lo pasa a mayúsculas tal como está en Outlook.
            firstChar = (Char.ToUpper(sent(0))).ToString()
            sent = firstChar + sent.Remove(0, 1)

            'Si el subject del mail no contiene el 'RE:', se lo agrega
            If Not mail.Subject.StartsWith("RE:") Then
                mail.Subject = "RE: " + mail.Subject
            End If

            'Se construye el encabezado de la respuesta
            sb.Append("<br><br><div><div style='border:none;border-top:solid #B5C4DF 1.0pt;padding:3.0pt 0cm 0cm 0cm'><p class=MsoNormal><b><span lang=EN-US style='font-size:10.0pt;font-family:""Tahoma"",""sans-serif"";mso-fareast-font-family:""Times New Roman"";mso-ansi-language:EN(-US) '>")
            sb.Append(htmlFrom)
            sb.Append("</span></b><span lang=EN-US style='font-size:10.0pt;font-family:""Tahoma"",""sans-serif"";mso-fareast-font-family:""Times New Roman"";mso-ansi-language:EN(-US) '>")
            sb.Append(mail.SenderName)
            sb.Append(" [mailto:")
            sb.Append(mail.SenderEmailAddress)
            sb.AppendLine("]")
            sb.Append("<br><b>")
            sb.Append(htmlSent)
            sb.Append("</b>")
            sb.Append(fec.Date.ToLongDateString())
            sb.Append(" ")
            sb.AppendLine(fec.ToShortTimeString())
            sb.Append("<br><b>")
            sb.Append(htmlTo)
            sb.Append("</b>")
            sb.AppendLine(mail.To)
            sb.Append("<br><b>")
            sb.Append(htmlSubject)
            sb.Append("</b>")
            sb.Append(mail.Subject)
            sb.AppendLine("<o:p></o:p></span></p></div></div><br>")

            'Se obtiene la posición donde se agregará el encabezado
            indexPosition = mail.HTMLBody.IndexOf(">", mail.HTMLBody.ToLower().IndexOf("<body")) + 1

            'Se agrega el encabezado
            mail.HTMLBody = mail.HTMLBody.Insert(indexPosition, sb.ToString())
        End Sub

        ''' <summary>
        ''' Si el lenguaje de outlook esta configurado para Spanish o SpanishArgentina
        ''' se setean las descripciones al español.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetHeaderLanguage()
            'El enumerador 2 es el de la interfaz de outlook.
            Dim languageId As Integer = app.LanguageSettings.LanguageID(2)

            Select Case languageId
                Case 1034 Or 11274
                    'Spanish y SpanishArgentina.
                    htmlSent = "Enviado el: "
                    htmlFrom = "De: "
                    htmlSubject = "Asunto: "
                    htmlTo = "Para: "

                Case Else
                    'El resto iría en ingles.
                    htmlSent = "Sent: "
                    htmlFrom = "From: "
                    htmlSubject = "Subject: "
                    htmlTo = "To: "
            End Select
        End Sub


        '''' <summary>
        '''' Obtiene la cuenta del usuario actual
        '''' </summary>
        '''' <returns></returns>
        '''' <remarks></remarks>
        'Public Function GetCurrentAccount() As Outlook.Account
        '    Dim userEntryId As String = app.Session.CurrentUser.EntryID

        '    For Each cuenta As Outlook.Account In app.Session.Accounts
        '        If String.Compare(cuenta.Session.CurrentUser.EntryID, userEntryId) = 0 Then
        '            Return cuenta
        '        End If
        '    Next

        '    Return Nothing
        'End Function

        Private Sub InspectorActivateHandler()

            If Not _SendingMailItem AndAlso Not _inspectorMaximized Then
                'Trace.WriteLine("Espero 2 seg a que se active el inspector para maximizar la ventana del mensaje")
                'Threading.Thread.Sleep(2000)
                If app.ActiveInspector().Caption = _activeInspectorCaption Then
                    app.ActiveInspector().WindowState = Microsoft.Office.Interop.Outlook.OlWindowState.olMaximized
                    app.ActiveInspector().Activate()
                    _inspectorMaximized = True
                End If
            End If

        End Sub

        Private Sub SendingMailItemHandler(ByRef cancel As Boolean)
            _SendingMailItem = True

            If Not _waitForSend Then
                itemSent = True
                updateOutParams(_mailItem)
            End If
        End Sub

        Private Sub SentMailItemHandler(ByVal sender As Object)

            If TypeOf (sender) Is Microsoft.Office.Interop.Outlook.MailItem Then

                Dim _mailItem As Microsoft.Office.Interop.Outlook.MailItem = sender

                If flag = True AndAlso mailTempPath <> String.Empty AndAlso _mailItem.UserProperties.Item(OutlookMailParameters.ITEMGUID).Value = myEntryId Then

                    updateOutParams(_mailItem)

                    _mailItem.SaveAs(mailTempPath.Replace(".msg", ".html"), Microsoft.Office.Interop.Outlook.OlSaveAsType.olHTML)
                    _mailItem.SaveAs(mailTempPath, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG)
                    _sentMailItem = _mailItem
                    itemSent = True
                    flag = False

                End If

            End If

        End Sub

        Private Sub updateOutParams(ByVal mail As Microsoft.Office.Interop.Outlook.MailItem)

            If mail Is Nothing Then Exit Sub

            If Not mail.To Is Nothing Then
                If _params.Contains(OutlookMailParameters.TO) Then
                    _params(OutlookMailParameters.TO) = mail.To.ToString()
                Else
                    _params.Add(OutlookMailParameters.TO, mail.To.ToString())
                End If
            Else
                If _params.Contains(OutlookMailParameters.TO) Then
                    _params.Remove(OutlookMailParameters.TO)
                End If
            End If

            If Not mail.CC Is Nothing Then
                If _params.Contains(OutlookMailParameters.CC) Then
                    _params(OutlookMailParameters.CC) = mail.CC.ToString()
                Else
                    _params.Add(OutlookMailParameters.CC, mail.CC.ToString())
                End If
            Else
                If _params.Contains(OutlookMailParameters.CC) Then
                    _params.Remove(OutlookMailParameters.CC)
                End If
            End If

            If Not mail.BCC Is Nothing Then
                If _params.Contains(OutlookMailParameters.BCC) Then
                    _params(OutlookMailParameters.BCC) = mail.BCC.ToString()
                Else
                    _params.Add(OutlookMailParameters.BCC, mail.BCC.ToString())
                End If
            Else
                If _params.Contains(OutlookMailParameters.BCC) Then
                    _params.Remove(OutlookMailParameters.BCC)
                End If
            End If

            If Not mail.Subject Is Nothing Then
                If _params.Contains(OutlookMailParameters.SUBJECT) Then
                    _params(OutlookMailParameters.SUBJECT) = mail.Subject.ToString()
                Else
                    _params.Add(OutlookMailParameters.SUBJECT, mail.Subject.ToString())
                End If
            Else
                If _params.Contains(OutlookMailParameters.SUBJECT) Then
                    _params.Remove(OutlookMailParameters.SUBJECT)
                End If
            End If

            If Not mail.Body Is Nothing Then
                If _params.Contains(OutlookMailParameters.BODY) Then
                    _params(OutlookMailParameters.BODY) = mail.Body.ToString()
                Else
                    _params.Add(OutlookMailParameters.BODY, mail.Body.ToString())
                End If
            Else
                If _params.Contains(OutlookMailParameters.BODY) Then
                    _params.Remove(OutlookMailParameters.BODY)
                End If
            End If

            If Not mail.EntryID Is Nothing Then
                If _params.Contains(OutlookMailParameters.ENTRY_ID) Then
                    _params(OutlookMailParameters.ENTRY_ID) = mail.EntryID
                Else
                    _params.Add(OutlookMailParameters.ENTRY_ID, mail.EntryID)
                End If
            Else
                If _params.Contains(OutlookMailParameters.ENTRY_ID) Then
                    _params.Remove(OutlookMailParameters.ENTRY_ID)
                End If
            End If

            If Not mail.ReceivedTime.ToString() Is Nothing Then
                If _params.Contains(OutlookMailParameters.RECEIVEDDATE) Then
                    _params(OutlookMailParameters.RECEIVEDDATE) = mail.ReceivedTime.ToString()
                Else
                    _params.Add(OutlookMailParameters.RECEIVEDDATE, mail.ReceivedTime.ToString())
                End If
            Else
                If _params.Contains(OutlookMailParameters.RECEIVEDDATE) Then
                    _params.Remove(OutlookMailParameters.RECEIVEDDATE)
                End If
            End If

            If Not mail.SenderEmailAddress Is Nothing Then
                If _params.Contains(OutlookMailParameters.SENDER) Then
                    _params(OutlookMailParameters.SENDER) = GetSMTPAddress(mail)
                Else
                    _params.Add(OutlookMailParameters.SENDER, GetSMTPAddress(mail))
                End If
            Else
                If _params.Contains(OutlookMailParameters.SENDER) Then
                    _params.Remove(OutlookMailParameters.SENDER)
                End If
            End If

        End Sub

        Private Function GetSMTPAddress(ByVal item As Microsoft.Office.Interop.Outlook.MailItem) As String
            Dim strAddress As String = item.SenderEmailAddress
            Dim oCon As Microsoft.Office.Interop.Outlook.ContactItem
            Dim strKey As String = String.Empty
            Dim strRet As String = item.SenderEmailAddress

            Trace.WriteLine("--GetSMTPAddress()--")

            Trace.WriteLine("SenderEmailType: " & item.SenderEmailType)
            Try
                If item.SenderEmailType = "EX" Then
                    Trace.WriteLine("Creating Outlook Contact..")
                    oCon = app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olContactItem)
                    Trace.WriteLine("Outlook Contact Created")
                    oCon.Email1Address = strAddress
                    Trace.WriteLine("Contact e-mail: " & oCon.Email1Address)

                    Dim longKey As Int64 = Int64.Parse(New Random().Next()) * Int64.Parse(100000)
                    strKey = "_" & (longKey.ToString() + DateTime.Now.ToShortDateString())
                    strKey = strKey.Replace(".", "").Replace("/", "")
                    oCon.FullName = strKey
                    Trace.WriteLine("Contact FullName: " & oCon.FullName)

                    oCon.Save()
                    Trace.WriteLine("Contacto Saved")

                    strRet = oCon.Email1DisplayName.Replace("(", "").Replace(")", "").Replace(strKey, "").Trim()

                    oCon.Delete()
                    Trace.WriteLine("Contacto Deleted")
                    oCon = Nothing

                    oCon = app.Session.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderDeletedItems).Items.Find("[Subject]=" & strKey)

                    If Not IsNothing(oCon) Then
                        oCon.Delete()
                    End If

                End If
            Catch ex As Exception
                Trace.WriteLine("Exception: " & ex.ToString())
            End Try

            Trace.WriteLine("e-mailAdress: " & strRet)
            Trace.WriteLine("--GetSMTPAddress()--")

            Return strRet
        End Function

        Public Sub NewAppointment(ByVal subject As String, ByVal location As String, ByVal body As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal ShowForm As Boolean)
            Dim office As New OfficeCommon.OutlookInterop()
            office.NewAppointment(subject, location, body, StartDate, EndDate, ShowForm)
        End Sub

        Public Function NewCalendar(ByVal Organizer As String, ByVal toMails As String, ByVal subject As String, ByVal location As String, ByVal body As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal AllDayEvent As Boolean) As String
            Dim office As New OfficeCommon.OutlookInterop()
            Return office.NewCalendar(Organizer, toMails, subject, location, body, StartDate, EndDate, AllDayEvent)
        End Function

    End Class
End Namespace



