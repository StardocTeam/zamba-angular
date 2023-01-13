Imports System.Windows.Forms
Imports Zamba.Core
Imports Zamba.OfficeCommon
Imports Zamba.Servers

Namespace Outlook
    Public Class OutlookInterop

#Region "Atributos y Propiedades"

        Public Const SUBJECT_EMPTY = "Subject empty"


        Dim app As Microsoft.Office.Interop.Outlook.Application = Nothing
        Dim outlookNS As Microsoft.Office.Interop.Outlook.NameSpace = Nothing
        Public Event CloseMailItemEvent()
        Dim _mail As Microsoft.Office.Interop.Outlook.MailItem = Nothing
        Dim _closeFromControlbox As Boolean = False
        Dim _disposingParent As Boolean = False
        Dim _SendingMailItem As Boolean = False
        Dim _params As New Hashtable
        Dim _waitForSend As Boolean
        Dim _activeInspectorCaption As String
        Dim _inspectorMaximized As Boolean
        Dim _mailItem As Microsoft.Office.Interop.Outlook.MailItem
        Dim realSubject As String = String.Empty
        'Se configuran con SetHeaderLanguage
        Dim htmlFrom, htmlSent, htmlTo, htmlSubject As String
        Dim toRecived As String = String.Empty
        Dim subjectRecived As String = String.Empty
        Dim bodyRecived As String = String.Empty

        Public Property LastMailItemTo() As String
        Public Property LastMailItemSubject() As String

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
            SetHeaderLanguage()
        End Sub

        Public Function OpenMailItem(ByVal mailItemPath As String, ByVal modal As Boolean, Optional ByVal winState As FormWindowState = FormWindowState.Maximized, Optional ByVal UpdateCaption As Boolean = False) As IntPtr

            Dim handle As IntPtr
            Dim FI As New IO.FileInfo(mailItemPath)

            ZTrace.WriteLineIf(ZTrace.IsError, "Opening MailItem: " & mailItemPath)
            _mail = DirectCast(app.Session.OpenSharedItem(mailItemPath), Microsoft.Office.Interop.Outlook.MailItem)

            _activeInspectorCaption = _mail.GetInspector.Caption

            _mail.Display(modal)

            _mail.GetInspector.Activate()


            Dim processes As System.Diagnostics.Process() = System.Diagnostics.Process.GetProcesses()

            For Each process As System.Diagnostics.Process In processes
                If process.MainWindowTitle.StartsWith(_mail.Subject) Then
                    handle = process.MainWindowHandle
                End If
            Next

            Select Case winState
                Case FormWindowState.Maximized
                    _mail.GetInspector.WindowState = Microsoft.Office.Interop.Outlook.OlWindowState.olMaximized
                Case FormWindowState.Minimized
                    _mail.GetInspector.WindowState = Microsoft.Office.Interop.Outlook.OlWindowState.olMinimized
                Case FormWindowState.Normal
                    _mail.GetInspector.WindowState = Microsoft.Office.Interop.Outlook.OlWindowState.olNormalWindow
            End Select

            _mailItem = _mail

            RemoveHandler _mailItem.Close, AddressOf CloseMailItemEventHandler
            AddHandler _mailItem.Close, AddressOf CloseMailItemEventHandler

            ZTrace.WriteLineIf(ZTrace.IsError, "Item opened")
            Return handle
        End Function

        Public Function GetMailItemFromFile(ByVal mailItemPath As String, ByVal modal As Boolean, Optional ByVal winState As FormWindowState = FormWindowState.Maximized, Optional ByVal UpdateCaption As Boolean = False) As Microsoft.Office.Interop.Outlook.MailItem

            Dim FI As New IO.FileInfo(mailItemPath)

            ZTrace.WriteLineIf(ZTrace.IsError, "Opening MailItem: " & mailItemPath)
            _mail = DirectCast(app.Session.OpenSharedItem(mailItemPath), Microsoft.Office.Interop.Outlook.MailItem)

            _activeInspectorCaption = _mail.GetInspector.Caption

            _mail.Display(modal)

            _mail.GetInspector.Activate()

            Select Case winState
                Case FormWindowState.Maximized
                    _mail.GetInspector.WindowState = Microsoft.Office.Interop.Outlook.OlWindowState.olMaximized
                Case FormWindowState.Minimized
                    _mail.GetInspector.WindowState = Microsoft.Office.Interop.Outlook.OlWindowState.olMinimized
                Case FormWindowState.Normal
                    _mail.GetInspector.WindowState = Microsoft.Office.Interop.Outlook.OlWindowState.olNormalWindow
            End Select

            Return _mail
        End Function

        Public Function CloseMailItem(closeFromControlbox As Boolean) As Boolean
            Dim ret As Boolean = False
            If Not IsNothing(_mail) Then
                ZTrace.WriteLineIf(ZTrace.IsError, "Closing MailItem")
                _mail.Close(Microsoft.Office.Interop.Outlook.OlInspectorClose.olDiscard)
                If closeFromControlbox = False Then _mail.Close(False)
                CloseMailItemEventHandler(False)
                While System.Runtime.InteropServices.Marshal.ReleaseComObject(_mail) <> 0
                End While
                _mail = Nothing
                GC.Collect()
                GC.WaitForPendingFinalizers()
                GC.Collect()
                ZTrace.WriteLineIf(ZTrace.IsError, "MailItem was closed successfully")
                ret = True
            End If
            Return ret
        End Function

        Private Sub CloseMailItemEventHandler(ByRef Cancel As Boolean)

            If Not _disposingParent Then
                closeFromControlbox = True
                RaiseEvent CloseMailItemEvent()
                ZTrace.WriteLineIf(ZTrace.IsError, "CloseMailItemEvent was raised")
            End If
        End Sub

        Public Function SearchMailInOutlook(mail As DataRow, ByRef _obtuveOutlook As Boolean) As Microsoft.Office.Interop.Outlook.MailItem
            Try
                Dim subjectString As String = mail("subject").ToString
                'aca va klo de vacio
                If Server.isOracle AndAlso subjectString.Equals(SUBJECT_EMPTY) Then
                    subjectString = String.Empty
                End If

                Dim accountsOutlook As Microsoft.Office.Interop.Outlook.Accounts
                accountsOutlook = app.Session.Accounts

                For Each account As Microsoft.Office.Interop.Outlook.Account In accountsOutlook

                    Dim sentBoxMails As Microsoft.Office.Interop.Outlook.Items = account.Session.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderSentMail).Items

                    sentBoxMails.Sort("[ReceivedTime]", True)

                    For index = 1 To If(sentBoxMails.Count > 100, 100, sentBoxMails.Count)

                        Dim _toSend As String = sentBoxMails.Item(index).To

                        If _toSend.IndexOf("'") = 0 AndAlso _toSend.LastIndexOf("'") Then
                            _toSend = _toSend.Substring(1, _toSend.Length - 2)
                        End If

                        If mail("tosend").ToString.Equals(_toSend.ToString) AndAlso subjectString.Equals(sentBoxMails.Item(index).Subject.ToString) Then
                            _obtuveOutlook = True
                            Return sentBoxMails.Item(index)
                        End If
                    Next
                Next
            Catch ex As Exception
                Return Nothing
            End Try
        End Function



        Public Function GetNewMailItem(Params As Hashtable) As Microsoft.Office.Interop.Outlook.MailItem

            Dim isReply As Boolean = False
            Dim ret As Boolean = False
            Dim timeOut As Integer = 20
            Dim replyMsgPath As String = String.Empty

            'Se muestra el outlook
            If IsNothing(app.ActiveExplorer()) Then outlookNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox).Display()

            'Se crea el mail a enviar
            _mailItem = app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem)


            RemoveHandler _mailItem.Close, AddressOf CloseMailItemEventHandler
            RemoveHandler _mailItem.Send, AddressOf SendingMailItemHandler
            RemoveHandler _mailItem.Open, AddressOf InspectorActivateHandler
            RemoveHandler _mailItem.Write, AddressOf GetAllAtributesMailItem

            AddHandler _mailItem.Close, AddressOf CloseMailItemEventHandler
            AddHandler _mailItem.Send, AddressOf SendingMailItemHandler
            AddHandler _mailItem.Open, AddressOf InspectorActivateHandler
            AddHandler _mailItem.Write, AddressOf GetAllAtributesMailItem

            LoadParamsInMailItem(Params, isReply, timeOut, replyMsgPath)

            _SendingMailItem = False
            _inspectorMaximized = False

            Application.DoEvents()
            _activeInspectorCaption = _mailItem.GetInspector.Caption

            Return _mailItem
        End Function

        Public Sub SendMail(ByVal _mailItem As Microsoft.Office.Interop.Outlook.MailItem, automaticSend As Boolean)
            If Not automaticSend Then
                _mailItem.Display(True)
            Else
                _mailItem.Send()
                _mailItem.Save()
            End If
        End Sub



        Private Sub LoadParamsInMailItem(Params As Hashtable, ByRef isReply As Boolean, ByRef timeOut As Integer, ByRef replyMsgPath As String)
            If Params IsNot Nothing Then
                replyMsgPath = Params(OutlookMailParameters.REPLY_MSG_PATH)

                'Verifica si el mail debe ser una respuesta y si tiene una ruta cargada.
                ' Ahora no solo ingresa el mail como respuesta sino que tambien si es un reenvio JB 28/10/2016
                If Params(OutlookMailParameters.IS_REPLY) IsNot Nothing AndAlso Boolean.Parse(Params(OutlookMailParameters.IS_REPLY)) _
                AndAlso Not String.IsNullOrEmpty(replyMsgPath) Then

                    'Se crea un mailItem mediante el msg.
                    Dim tempMail As Microsoft.Office.Interop.Outlook.MailItem = app.CreateItemFromTemplate(replyMsgPath)
                    _mailItem = tempMail
                    'Agrega el encabezado de las respuestas con los datos.
                    AddReplyHeader(tempMail)
                    Dim formatb = tempMail.BodyFormat
                    'Se configura el mail a enviar con los datos obtenidos.
                    With _mailItem
                        .To = tempMail.SenderEmailAddress
                    End With

                    If Not IsNothing(Params(OutlookMailParameters.TO)) AndAlso Not IsNothing(_mailItem.To) Then _mailItem.To = Params(OutlookMailParameters.TO).ToString()
                    If Not IsNothing(Params(OutlookMailParameters.CC)) Then _mailItem.CC = Params(OutlookMailParameters.CC).ToString()
                    If Not IsNothing(Params(OutlookMailParameters.BCC)) Then _mailItem.BCC = Params(OutlookMailParameters.BCC).ToString()
                    Dim bodyparam, htmlbodyparam As String
                    bodyparam = Params(OutlookMailParameters.BODY)
                    htmlbodyparam = Params(OutlookMailParameters.HTMLBODY)
                    Dim bodyboolean As Boolean = Not IsNothing(bodyparam)
                    Dim body As String = IIf(bodyboolean, (bodyparam), String.Empty)
                    Dim htmlbody As String = IIf(Not IsNothing(htmlbodyparam), (htmlbodyparam), String.Empty)

                    _mailItem.HTMLBody = IIf(String.IsNullOrEmpty(body), htmlbody, body) + _mailItem.HTMLBody

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
                            attachPath = attachPath.Trim()
                            If Not String.IsNullOrEmpty(attachPath) AndAlso String.Compare(attachPath.ToLower(), "false") <> 0 Then
                                If IO.File.Exists(attachPath) Then
                                    _mailItem.Attachments.Add(attachPath, Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue, 1, IO.Path.GetFileName(attachPath))
                                Else
                                    ZTrace.WriteLineIf(ZTrace.IsError, "No se ha encontrado el adjunto: " & attachPath)
                                End If
                            End If
                        Next
                    End If

                End If

                If Not IsNothing(Params(OutlookMailParameters.SEND_TIMEOUT)) Then timeOut = Convert.ToInt32(Params(OutlookMailParameters.SEND_TIMEOUT))

                _params = Params
            End If
        End Sub

        Private Sub GetAllAtributesMailItem()
            Try
                If Not IsNothing(_mailItem) Then
                    LastMailItemTo = _mailItem.To
                    LastMailItemSubject = _mailItem.Subject
                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsError, "Exception:  " & ex.ToString())
            End Try
        End Sub

        ''' <summary>
        ''' Agrega el encabezado de una respuesta a un mail existente.
        ''' </summary>
        ''' <param name="mail">MailItem al que se le agregará el encabezado de respuesta</param>
        ''' <remarks></remarks>
        ''' <history>
        '''     Tomas   15/06/2010  Created
        ''' </history>
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
            Try
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
            Catch ex As System.Runtime.InteropServices.COMException
                ZClass.raiseerror(ex)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
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
            Try
                If Not _SendingMailItem AndAlso Not _inspectorMaximized Then
                    If Not app.ActiveInspector Is Nothing AndAlso Not app.ActiveInspector() Is Nothing AndAlso app.ActiveInspector().Caption = _activeInspectorCaption Then
                        app.ActiveInspector().WindowState = Microsoft.Office.Interop.Outlook.OlWindowState.olMaximized
                        app.ActiveInspector().Activate()
                        _inspectorMaximized = True
                    End If
                End If
            Catch ex As NullReferenceException
            Catch ex As Exception
            End Try

        End Sub

        Private Sub SendingMailItemHandler(ByRef cancel As Boolean)
            _SendingMailItem = True

            If Not _waitForSend Then
                updateOutParams(_mailItem)
            End If
        End Sub
        Public Function LoadParams(ByRef m_Subject As String, ByRef m_TO As String, ByRef m_CC As String, ByRef m_BCC As String, ByRef m_Body As String, ByRef m_Attachs As List(Of String))
            m_Subject = String.Empty
            If _params(OutlookMailParameters.SUBJECT) IsNot Nothing Then m_Subject = _params(OutlookMailParameters.SUBJECT)

            m_TO = String.Empty
            m_CC = String.Empty
            m_BCC = String.Empty
            m_Body = String.Empty
            m_Attachs = Nothing

            If _params(OutlookMailParameters.TO) IsNot Nothing Then m_TO = _params(OutlookMailParameters.TO)
            If _params(OutlookMailParameters.CC) IsNot Nothing Then m_CC = _params(OutlookMailParameters.CC)
            If _params(OutlookMailParameters.BCC) IsNot Nothing Then m_BCC = _params(OutlookMailParameters.BCC)
            If _params(OutlookMailParameters.BODY) IsNot Nothing Then m_Body = _params(OutlookMailParameters.BODY)
            If _params(OutlookMailParameters.ATTACH_PATHS) IsNot Nothing Then m_Attachs = DirectCast(_params(OutlookMailParameters.ATTACH_PATHS), String()).ToList()
        End Function
        Public Sub updateOutParams(ByVal mail As Microsoft.Office.Interop.Outlook.MailItem)

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

            ZTrace.WriteLineIf(ZTrace.IsError, "--GetSMTPAddress()--")

            ZTrace.WriteLineIf(ZTrace.IsError, "SenderEmailType: " & item.SenderEmailType)
            Try
                If item.SenderEmailType = "EX" Then
                    ZTrace.WriteLineIf(ZTrace.IsError, "Creating Outlook Contact..")
                    oCon = app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olContactItem)
                    ZTrace.WriteLineIf(ZTrace.IsError, "Outlook Contact Created")
                    oCon.Email1Address = strAddress
                    ZTrace.WriteLineIf(ZTrace.IsError, "Contact e-mail: " & oCon.Email1Address)

                    Dim longKey As Int64 = Int64.Parse(New Random().Next()) * Int64.Parse(100000)
                    strKey = "_" & (longKey.ToString() + DateTime.Now.ToShortDateString())
                    strKey = strKey.Replace(".", "").Replace("/", "")
                    oCon.FullName = strKey
                    ZTrace.WriteLineIf(ZTrace.IsError, "Contact FullName: " & oCon.FullName)

                    oCon.Save()
                    ZTrace.WriteLineIf(ZTrace.IsError, "Contacto Saved")

                    strRet = oCon.Email1DisplayName.Replace("(", "").Replace(")", "").Replace(strKey, "").Trim()

                    oCon.Delete()
                    ZTrace.WriteLineIf(ZTrace.IsError, "Contacto Deleted")
                    oCon = Nothing

                    oCon = app.Session.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderDeletedItems).Items.Find("[Subject]=" & strKey)

                    If Not IsNothing(oCon) Then
                        oCon.Delete()
                    End If

                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsError, "Exception: " & ex.ToString())
            End Try

            ZTrace.WriteLineIf(ZTrace.IsError, "e-mailAdress: " & strRet)
            ZTrace.WriteLineIf(ZTrace.IsError, "--GetSMTPAddress()--")

            Return strRet
        End Function

        Public Sub NewAppointment(ByVal subject As String, ByVal location As String, ByVal body As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal ShowForm As Boolean)
            'Dim office As New OfficeCommon.OutlookInterop()
            'office.NewAppointment(subject, location, body, StartDate, EndDate, ShowForm)

            Dim appointment As Microsoft.Office.Interop.Outlook.AppointmentItem

            appointment = app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olAppointmentItem)

            appointment.Subject = subject
            appointment.Body = body
            appointment.Location = location
            appointment.Start = StartDate
            appointment.End = EndDate

            If ShowForm Then
                appointment.Display(True)
            Else
                appointment.Save()
            End If
        End Sub

        Public Function NewCalendar(ByVal Organizer As String, ByVal toMails As String, ByVal subject As String, ByVal location As String, ByVal body As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal AllDayEvent As Boolean) As String
            'Dim office As New OfficeCommon.OutlookInterop()
            'Return office.NewCalendar(Organizer, toMails, subject, location, body, StartDate, EndDate, AllDayEvent)
            Return OfficeCommon.OutlookInterop.NewCalendar(Organizer, toMails, subject, location, body, StartDate, EndDate, AllDayEvent)
        End Function



    End Class

End Namespace



