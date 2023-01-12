Imports System.Collections.Generic
Imports ZAMBA.data
Imports ZAMBA.AppBlock
Imports Zamba.Core
'Imports Zamba.Data
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.MessagesControls
''' Class	 : Controls.Message
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase mensaje, para crear objetos que contengan las propiedades de un correo
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class InternalMessage
    Implements IInternalMessage

#Region "Atributos"
    Private _id As Int64
    Private _de As Integer
    'Andres 8/8/07 - Comentado , se guardaba el tipo pero nunca se usaba.
    'Private _username As String
    Private _asunto As String
    Private _body As String
    Private _Fecha As Date
    Private _Read As Boolean
    Private _attachList As New ArrayList
    '  Private _attachs As New Hashtable
    Private _attachsNames As New ArrayList
    Private _User As IUser
    Private _confirmation As Boolean
    Private del As Boolean
    Private _destinatarios As New ArrayList
    Private _ownerUserID As Integer
    'Andres 8/8/07 - Comentado , se guardaba el tipo pero nunca se usaba.
    'Private _CCStr As String
    '   Private _cc As New Hashtable
    'Andres 8/8/07 - Comentado , se guardaba el tipo pero nunca se usaba.
    'Private _CCOStr As String
    '    Private _cco As New Hashtable
    '   Private _toUser As New Hashtable
    'Andres 8/8/07 - Comentado , se guardaba el tipo pero nunca se usaba.
    'Private _ToStr As String
    'Andres 8/8/07 - Comentado , se guardaba el tipo pero nunca se usaba.
    'Private _DeStr As String
#End Region

#Region "Propiedades"
    Public Property User() As IUser
        Get
            Return _User
        End Get
        Set(ByVal value As IUser)
            _User = value
        End Set
    End Property
    Public Property Destinatarios() As ArrayList Implements IInternalMessage.Destinatarios
        Get
            Return _destinatarios
        End Get
        Set(ByVal value As ArrayList)
            _destinatarios = value
        End Set
    End Property
    Public Property Owner_User_ID() As Integer Implements IInternalMessage.Owner_User_ID
        Get
            Return _ownerUserID
        End Get
        Set(ByVal value As Integer)
            _ownerUserID = value
        End Set
    End Property
    Public Property Deleted() As Boolean Implements IInternalMessage.Deleted
        Get
            Return del
        End Get
        Set(ByVal Value As Boolean)
            del = Value
        End Set
    End Property
    Public Property Id() As Int64 Implements IInternalMessage.Id
        Get
            Return _id
        End Get
        Set(ByVal Value As Int64)
            _id = Value
        End Set
    End Property
    Public Property Fecha() As Date Implements IInternalMessage.Fecha
        Get
            Return _Fecha
        End Get
        Set(ByVal Value As Date)
            _Fecha = Value
        End Set
    End Property
    Public Property Read() As Boolean Implements IInternalMessage.Read
        Get
            If _Read = False Then
                Dim d As Destinatario
                For Each d In Me.Destinatarios
                    If d.UserID = Me._User.ID Then 'Me.Owner_User_ID Then
                        If d.Readed = True Then
                            Me._Read = True
                            Return True
                        End If
                    End If
                Next
            Else
                Return True
            End If
            Return False
        End Get
        Set(ByVal Value As Boolean)
            Dim d As Destinatario
            Me._Read = Value
            For Each d In Me.Destinatarios
                If d.UserID = Me.Owner_User_ID Then
                    d.Readed = Value
                End If
            Next

        End Set
    End Property
    Public Property De() As Integer Implements IInternalMessage.De
        Get
            Return _de
        End Get
        Set(ByVal Value As Integer)
            _de = Value
        End Set
    End Property
    Public Property UserName() As String Implements IInternalMessage.UserName
        Get
            Return Me._User.Name
        End Get
        Set(ByVal Value As String)
            '_username = Value
        End Set
    End Property
    Public Property Subject() As String Implements IInternalMessage.Subject
        Get
            Return _asunto
        End Get
        Set(ByVal Value As String)
            If Value = String.Empty Then
                _asunto = " "
            Else
                _asunto = Value
            End If
        End Set
    End Property
    Public Property Body() As String Implements IInternalMessage.Body
        Get
            Return _body
        End Get
        Set(ByVal Value As String)
            If Value <> String.Empty Then
                _body = Value
            Else
                _body = " "
            End If

        End Set
    End Property
    Public Property Confirmation() As Boolean Implements IInternalMessage.Confirmation
        Get
            Return _confirmation
        End Get
        Set(ByVal Value As Boolean)
            _confirmation = Value
        End Set
    End Property
    Public Property AttachList() As ArrayList Implements IInternalMessage.AttachList
        Get
            Return Me._attachList
        End Get
        Set(ByVal Value As ArrayList)
            Me._attachList = Value
        End Set
    End Property
    Public Property AttachsNames() As ArrayList Implements IInternalMessage.AttachsNames
        Get
            Return Me._attachsNames
        End Get
        Set(ByVal Value As ArrayList)
            Me._attachsNames = Value
        End Set
    End Property
    'Public Property CC() As Hashtable
    '    Get
    '        Return _cc
    '    End Get
    '    Set(ByVal value As Hashtable)
    '        _cc = value
    '    End Set
    'End Property
    Public ReadOnly Property CC() As ArrayList Implements IInternalMessage.CC
        Get
            Dim aCC As New ArrayList
            Dim dest As Destinatario
            Dim i As Integer
            For i = 0 To Me.Destinatarios.Count - 1
                dest = DirectCast(Me.Destinatarios(i), Destinatario)
                If dest.Type = MessageType.MailCC Then
                    aCC.Add(dest)
                End If
            Next
            Return aCC
        End Get
    End Property
    Public Property CCStr() As String Implements IInternalMessage.CCStr
        Get
            '            Return _CCStr
            Dim sb As New System.Text.StringBuilder
            Dim dest As Destinatario
            Dim i, j As Integer
            For i = 0 To Me.Destinatarios.Count - 1
                dest = DirectCast(Me.Destinatarios(i), Destinatario)
                If dest.Type = MessageType.MailCC Then
                    If j <> 0 Then
                        sb.Append(";")
                    End If
                    sb.Append(dest.UserName)
                    j += 1
                End If
            Next
            Return sb.ToString
        End Get
        Set(ByVal value As String)
            '_CCStr = value
        End Set
    End Property
    'Public Property CCO() As Hashtable
    '    Get
    '        Return _cco
    '    End Get
    '    Set(ByVal value As Hashtable)
    '        _cco = value
    '    End Set
    'End Property
    Public ReadOnly Property CCO() As ArrayList Implements IInternalMessage.CCO
        Get
            Dim aCCO As New ArrayList
            Dim dest As Destinatario
            Dim i As Integer
            For i = 0 To Me.Destinatarios.Count - 1
                dest = DirectCast(Me.Destinatarios(i), Destinatario)
                If dest.Type = MessageType.MailCCO Then
                    aCCO.Add(dest)
                End If
            Next
            Return aCCO
        End Get
    End Property
    Public Property CCOStr() As String Implements IInternalMessage.CCOStr
        Get
            '           Return _CCOStr
            Dim sb As New System.Text.StringBuilder
            Dim dest As Destinatario
            Dim i, j As Integer

            For i = 0 To Me.Destinatarios.Count - 1
                dest = DirectCast(Me.Destinatarios(i), Destinatario)
                If dest.Type = MessageType.MailCCO Then
                    If j <> 0 Then
                        sb.Append(";")
                    End If
                    sb.Append(dest.UserName)
                    j += 1
                End If
            Next
            Return sb.ToString
        End Get
        Set(ByVal value As String)
            'Andres 8/8/07 - Comentado , se guardaba el tipo pero nunca se usaba.
            '_CCOStr = value
        End Set
    End Property
    Public Property ToStr() As String Implements IInternalMessage.ToStr
        Get
            '          Return _ToStr
            Dim sb As New System.Text.StringBuilder
            Dim dest As Destinatario
            Dim i, j As Integer
            For i = 0 To Me.Destinatarios.Count - 1
                dest = DirectCast(Me.Destinatarios(i), Destinatario)
                If dest.Type = MessageType.MailTo Then
                    If j <> 0 Then
                        sb.Append(";")
                    End If
                    sb.Append(dest.UserName)
                    j += 1
                End If
            Next
            Return sb.ToString
        End Get
        Set(ByVal value As String)
            '_ToStr = value
        End Set
    End Property
    'Public Property ToUser() As Hashtable 'List(Of User)
    '    Get
    '        Return _toUser
    '    End Get
    '    Set(ByVal value As Hashtable)
    '        _toUser = value
    '    End Set
    'End Property
    Public ReadOnly Property TOUSER() As ArrayList Implements IInternalMessage.TOUSER
        Get
            Dim atouser As New ArrayList
            Dim dest As Destinatario
            Dim i As Integer
            For i = 0 To Me.Destinatarios.Count - 1
                dest = DirectCast(Me.Destinatarios(i), Destinatario)
                If dest.Type = MessageType.MailTo Then
                    atouser.Add(dest)
                End If
            Next
            Return atouser
        End Get
    End Property
    Public Property DeStr() As String Implements IInternalMessage.DeStr
        Get
            Return UserGroupBusiness.GetUserorGroupNamebyId(Me.De)
        End Get
        Set(ByVal value As String)
            '_DeStr = value
        End Set
    End Property
#End Region

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que devuelve si un usuario leyo ya el correo. El usuario debe figurar entre los destinatarios del correo
    ''' </summary>
    ''' <param name="userid">Id del usuario</param>
    ''' <returns>Boolean</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function userRead(ByVal userid As Integer) As Boolean Implements IInternalMessage.userRead
        Dim d As Destinatario
        For Each d In Me.Destinatarios
            If d.UserID = userid Then
                Return d.Readed
            End If
        Next
        Return False
    End Function
    Public Sub New()
        Me._destinatarios = New ArrayList
        Me._attachList = New ArrayList
        '        ToUser = New Hashtable
        '        CC = New Hashtable
        '        CCO = New Hashtable
        '        Attachs = New Hashtable
    End Sub
    Public Sub New(ByVal userid As Integer)
        Me._User = UserBusiness.GetUserById(userid)
    End Sub
    Public Sub New(ByVal user As IUser)
        Me._User = user
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo que elimina a un usuario de la lista de destinatarios
    ''' </summary>
    ''' <param name="userId">Id del usuario que se quitará</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub clearDest(ByVal userId As Integer) Implements IInternalMessage.clearDest
        Dim des As Destinatario
        For Each des In Me.Destinatarios
            If des.UserID = userId Then
                Me.Destinatarios.Remove(des)
            End If
        Next
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un objeto Mensaje con las propiedades de un Mensaje Reenviado para el usuario actual
    ''' </summary>
    ''' <returns>Objeto Message</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function getResend() As IInternalMessage Implements IInternalMessage.getResend
        Dim msg As New InternalMessage(Membership.MembershipHelper.CurrentUser)
        msg.Body = ControlChars.NewLine & ControlChars.NewLine & ControlChars.NewLine & "Mensaje Enviado el " & Me.Fecha.ToShortDateString & " por:" & Me.UserName & ControlChars.NewLine & "Para:" & Me.ToStr & ControlChars.NewLine & "Con Copia a:" & Me.CCStr & ControlChars.NewLine & Me.Body
        msg.Subject = "RV:" & Me.Subject
        'msg.Destinatarios = Me.ResendDest
        msg.AttachList = Me.AttachList
        msg.De = Me.Owner_User_ID
        Return msg
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un objeto Mensaje con las propiedades de un Mensaje Respondido para el usuario actual
    ''' </summary>
    ''' <returns>Objeto Message</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetResponse() As IInternalMessage Implements IInternalMessage.GetResponse
        'TODO COMPROBAR SI LA SIGUIENTE LINEA ESTA BEN, ESTABA VIENIENDO EN CERO
        Me.Owner_User_ID = Convert.ToInt32(_User.ID)
        Dim msg As New InternalMessage(Me.Owner_User_ID)
        msg.Body = ControlChars.NewLine & ControlChars.NewLine & ControlChars.NewLine & "Mensaje Enviado el " & Me.Fecha.ToShortDateString & " por:" + Me.UserName + ControlChars.NewLine + "Para:" & Me.ToStr & ControlChars.NewLine & "Con Copia a:" & Me.CCStr & ControlChars.NewLine & Me.Body
        msg.Subject = "RE:" & Me.Subject
        msg.De = Me.Owner_User_ID
        msg.AttachList = New ArrayList

        Dim dest As New ArrayList
        dest.Add(New Destinatario(UserBusiness.GetUserById(Me.De), MessageType.MailTo))
        msg.Destinatarios = dest
        Return msg
    End Function
    'Private Sub RemoveMyDest()
    '    Dim des As Destinatario
    '    For Each des In Me.Destinatarios
    '        If des.UserID = Me.Owner_User_ID Then
    '            Me.Destinatarios.Remove(des)
    '        End If
    '    Next
    'End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un objeto Mensaje con las propiedades de un Mensaje Respondido a Todos para el usuario actual
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function getResponseAll() As IInternalMessage Implements IInternalMessage.getResponseAll
        Dim msg As New InternalMessage(Membership.MembershipHelper.CurrentUser)
        Me.Owner_User_ID = Convert.ToInt32(Me._User.ID)
        Try
            msg.Destinatarios.Add(New Destinatario(UserBusiness.GetUserById(Me.De), MessageType.MailTo))
            msg.Body = ControlChars.NewLine & ControlChars.NewLine & ControlChars.NewLine & "Mensaje Enviado el " & Me.Fecha.ToShortDateString & " por:" + Me.UserName + ControlChars.NewLine + "Para:" & Me.ToStr & ControlChars.NewLine & "Con Copia a:" & Me.CCStr & ControlChars.NewLine & Me.Body
            msg.Subject = "RE:" & Me.Subject
            msg.AttachList = New ArrayList
            msg.De = Me.Owner_User_ID

            'msg.RemoveMyDest()
            Dim name As String = ""
            msg.Destinatarios.AddRange(Me.getResponseAllDestino(name))
            msg.UserName = name
            Return msg
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Arraylist con las respuestas a todos
    ''' </summary>
    ''' <param name="duserName"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function getResponseAllDestino(ByRef duserName As String) As ArrayList
        Dim des As Destinatario
        Dim result As New ArrayList
        Try
            For Each des In Me.Destinatarios
                If des.Type <> MessageType.MailCCO Then
                    If des.UserID <> Me.Owner_User_ID Then
                        result.Add(des)
                    Else
                        duserName = des.UserName
                    End If
                End If
            Next
            Return result
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            'MessageBox.Show(ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo que envia el mensaje actual
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub Send() Implements IInternalMessage.Send
        Me.Id = Zamba.Core.CoreBusiness.GetNewID(IdTypes.INTERNALMAIL)
        Me.De = Convert.ToInt32(Membership.MembershipHelper.CurrentUser.ID) 'Id Usuario que envia mensaje

        MessageRegister() 'registro el mensaje en la base
        RegisterDestinatarios() 'Registro los destinatarios

        Try
            RegisterAllAttach() 'Registro los attach 
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Me.rollBack()
            Throw New Exception("Ocurrió un error al registrar los adjuntos del mensaje")
        End Try





        'UserBusiness.Rights.SaveAction(2, ObjectTypes.ModuleInsert, RightsType.AgregarDocumento)


    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo que elimina el objeto Message actual
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub Delete() Implements IInternalMessage.Delete
        If Me.Owner_User_ID = Me.De Then
            Me.DeleteMessageSended()
        Else
            Me.DeleteMessageRecived()
        End If
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo que cambia el estado del mensaje actual a leido
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub SetAsRead() Implements IInternalMessage.SetAsRead
        Try
            MessagesFactory.SetAsRead(Me.Id.ToString, Me._User.ID)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Cancela el mensaje creado
    ''' </summary>
    ''' <remarks>
    ''' Se utiliza si se produce una falla en el envio del mensaje
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub rollBack()
        Try
            MessagesFactory.rollBack(Me.Id)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "Send"
    'Private Sub registerAllCC()
    '    Dim i As Integer
    '    For i = 0 To Me._cc.Count - 1
    '        Try
    '            Me.MessageDestRegister(Me._cc(i), MailType.MailCC)
    '        Catch ex As Exception

    '        End Try
    '    Next
    'End Sub
    'Private Sub registerAllCCO()
    '    Dim i As Integer
    '    For i = 0 To Me._cco.Count - 1
    '        Try
    '            Me.MessageDestRegister(Me._cco(i), MailType.MailCCO)
    '        Catch
    '        End Try
    '    Next
    'End Sub
    'Private Sub registerAllTo()

    'End Sub
    Private Sub RegisterDestinatarios()
        Try
            For Each item As Object In Me.Destinatarios
                Try
                    Select Case item.GetType().FullName

                        Case "Zamba.Controls.Destinatario"

                            Dim user As Destinatario = DirectCast(item, Destinatario)
                            InternalMessage.RegisterMailToUser(Convert.ToInt32(Me.Id), Convert.ToInt32(user.UserID), user.Type, user.UserName)
                            user = Nothing
                            item = Nothing

                        Case "Zamba.Core.Destinatario"

                            Dim user As Core.Destinatario = DirectCast(item, Core.Destinatario)
                            InternalMessage.RegisterMailToUser(Convert.ToInt32(Me.Id), Convert.ToInt32(user.UserID), user.Type, user.UserName)
                            user = Nothing
                            item = Nothing

                        Case Else

                            Dim user As Core.Destinatario = DirectCast(item, Core.Destinatario)
                            InternalMessage.RegisterMailToUser(Convert.ToInt32(Me.Id), Convert.ToInt32(user.UserID), user.Type, user.UserName)
                            user = Nothing
                            item = Nothing

                    End Select

                Catch ex As Exception
                    rollBack()
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Shared Sub RegisterMailToUser(ByVal mailId As Integer, ByVal userId As Integer, ByVal mailType As MessageType, ByVal userName As String)
        MessagesFactory.RegisterMailToUser(mailId, userId, mailType, userName)
    End Sub
    Private Sub RegisterAllAttach()
        If IsNothing(_attachList) Or _attachList.Count = 0 Then Exit Sub 'There are no Attachments

        Try
            Dim i As Integer
            For i = 0 To Me._attachList.Count - 1
                Dim attachment As Object = Me._attachList(i)
                If IsNothing(attachment) Then Exit For

                Dim attach As Result = DirectCast(attachment, Result)
                attachment = Nothing

                MessagesFactory.InsertAttach(Id, attach.ID, attach.DocType.ID, attach.Index, attach.Name, attach.IconId, attach.Disk_Group_Id, attach.Doc_File.Trim, attach.OffSet, attach.DISK_VOL_PATH)
            Next
        Catch ex As Exception
            rollBack()
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub
    Private Sub MessageRegister()
        Try
            Dim ConfirmChar As Int32 = getConfirmChar()
            MessagesFactory.MessageRegister(Id, De, Body, Subject, ConfirmChar)
        Catch ex As Exception
            rollBack()
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    
#End Region
#Region "Delete"
    Public Sub DeleteMessageRecived() Implements IInternalMessage.DeleteMessageRecived
        Try
            MessagesFactory.DeleteMessageRecived(Me.Id, Me._User.ID)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub DeleteMessageSended()
        Try
            MessagesFactory.DeleteMessageSended(Me.Id)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

    Private Function getConfirmChar() As Integer
        If Me.Confirmation Then
            Return 1
        Else
            Return 0
        End If
    End Function
    Public Sub addDest(ByVal id As Integer, ByVal User_Name As String, ByVal desttype As MessageType) Implements IInternalMessage.addDest
        'Revisar, esta mal la instancia
        Dim dest As New Destinatario(id.ToString(), DirectCast(Int32.Parse(User_Name), MessageType), desttype.ToString())
        Me.Destinatarios.Add(dest)
    End Sub
    Public Sub addDest(ByVal dest As IDestinatario) Implements IInternalMessage.addDest
        If dest.UserID = Me.Owner_User_ID Then
            Me._Read = dest.Readed
        End If
        Me.Destinatarios.Add(dest)
    End Sub
    'Public Sub addCC(ByVal id As Integer, ByVal User_Name As String)
    '    Me._cc.Add(new )
    'End Sub
    'Public Sub addTo(ByVal value As Integer)
    '    Me._to.Add(value)
    'End Sub
    'Public Sub addCCO(ByVal value As Integer)
    '    Me._cco.Add(value)
    'End Sub

    'Public Sub addCCRange(ByVal values As ArrayList)
    '    Dim val As Integer
    '    For Each val In values
    '        Me.addCC(val)
    '    Next
    'End Sub
    'Public Sub addCCORange(ByVal values As ArrayList)
    '    Dim val As Integer
    '    For Each val In values
    '        Me.addCCO(val)
    '    Next
    'End Sub
    'Public Sub addToRange(ByVal values As ArrayList)
    '    Dim val As Integer
    '    For Each val In values
    '        Me.addTo(val)
    '    Next
    'End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo que agrega un Arraylist de destinatarios
    ''' </summary>
    ''' <param name="arr">Arraylist con destinatarios que se desea agregar a la propiedad destinatarios del objeto Message actual</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub AddDestRange(ByVal arr As ArrayList) Implements IInternalMessage.AddDestRange
        Me.Destinatarios.AddRange(arr)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que devuelve un objeto DisplayMessage en base el objeto Message actual
    ''' </summary>
    ''' <returns>Objeto DisplayMessage</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function getDispMessage() As IDisplayMessages Implements IInternalMessage.getDispMessage
        Return New DisplayMessages(Me)
    End Function
    'Public Shared Sub addAttach(ByVal obj As Object)

    'End Sub

    Public Shared Sub SendInternalMessage(ByVal subject As String, ByVal fecha As Date, ByVal body As String, ByVal receiptUsers As Generic.List(Of User), ByVal SendType As MessageType)
        Dim msj As New InternalMessage(Membership.MembershipHelper.CurrentUser.ID)
        msj.Body = body
        msj.Subject = subject
        msj.Fecha = fecha
        For Each _user As IUser In receiptUsers
            If Not IsNothing(_user) Then
                Dim dest As New Destinatario(DirectCast(_user, User), SendType)
                msj.addDest(dest)
            End If
        Next
        msj.Send()
    End Sub
End Class

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.MessagesControls
''' Class	 : Controls.DisplayMessages
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Objeto que muestra las propiedades visibles del mail
''' Se agrega tambien un funcion para comparar dos mails
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class DisplayMessages
    Implements IDisplayMessages
    Private msg As InternalMessage
#Region "Propiedades"
    Public ReadOnly Property Usuario() As String Implements IDisplayMessages.Usuario
        Get
            Return msg.UserName
        End Get
    End Property
    Public ReadOnly Property Asunto() As String Implements IDisplayMessages.Asunto
        Get
            Return msg.Subject
        End Get
    End Property
    Public ReadOnly Property Fecha() As String Implements IDisplayMessages.Fecha
        Get
            Return Me.msg.Fecha.ToString()
        End Get
    End Property
    Public ReadOnly Property De() As String Implements IDisplayMessages.De
        Get
            'Add
            Return msg.DeStr
        End Get
    End Property
    Public ReadOnly Property Para() As String Implements IDisplayMessages.Para
        Get
            Return msg.ToStr
        End Get
    End Property
    Public ReadOnly Property ConCopia() As String Implements IDisplayMessages.ConCopia
        Get
            Return msg.CCStr
        End Get
    End Property
#End Region
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Compara el objeto Message actual con un objeto message pasado por parametro
    ''' </summary>
    ''' <param name="msge">Objeto Message que se desea comparar con el objeto Message actual</param>
    ''' <returns>
    ''' True si son los mismos
    ''' </returns>
    ''' <remarks>
    ''' Solo se comparan los IDs de los mensajes
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function Compare(ByVal msge As IInternalMessage) As Boolean Implements IDisplayMessages.Compare
        If msg.Id = msge.Id Then
            Return True
        End If
        Return False
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="message">Objeto Message</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal message As InternalMessage)
        msg = message
    End Sub
End Class