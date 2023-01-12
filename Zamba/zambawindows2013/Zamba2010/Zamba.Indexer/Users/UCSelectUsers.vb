'Imports Zamba.AdminControls
Imports Zamba.Core

Public Class UCSelectUsers
    Inherits Zamba.AppBlock.ZControl
    Public Event eCerrar(ByVal OK As Boolean)


#Region "Atributos"

    Dim selectedUsers As New Generic.List(Of User)
    Dim nonSelectedUsers As New Generic.List(Of User)
    Dim usuariosAgregados As New Generic.List(Of User)
    Dim usuariosBorrados As New Generic.List(Of User)

    Dim selectedGroups As New Generic.List(Of UserGroup)
    Dim nonSelectedGroups As New Generic.List(Of UserGroup)
    Dim gruposAgregados As New Generic.List(Of UserGroup)
    Dim gruposBorrados As New Generic.List(Of UserGroup)

    Dim _selectedUsers As New Generic.List(Of User)
    Dim _selectedGroups As New Generic.List(Of UserGroup)
    Dim extmails As New Generic.List(Of String)
    Dim notifyByMail As Boolean

    Private TypeId As GroupToNotifyTypes
    Private GroupId As Int64
    Private docIds As New Generic.List(Of Int64)
    Private _messageID As Int32
#End Region

#Region "Constructores"

    Public Sub New(ByVal typeID As GroupToNotifyTypes, ByVal idDeAgrupamiento As Int64, ByVal mostrarAceptarCancelar As Boolean, ByVal MessageID As Int32)
        'Me.New(typeID, idDeAgrupamiento)
        InitializeComponent()
        Me.TypeId = typeID
        GroupId = idDeAgrupamiento
        If Not mostrarAceptarCancelar Then
            btnAceptar.Visible = False
            btnCancelar.Visible = False
        End If
        LoadData()
    End Sub

    Public Sub New(ByVal _typeId As GroupToNotifyTypes, ByVal idDeAgrupamiento As Int64, ByVal MessageID As Int32, Optional ByVal participantIds As Generic.List(Of Int64) = Nothing)
        InitializeComponent()
        TypeId = _typeId
        GroupId = idDeAgrupamiento
        _messageID = MessageID

        LoadData(participantIds)
    End Sub

    Public Sub New(ByVal _typeId As GroupToNotifyTypes, ByVal idDeAgrupamiento As Generic.List(Of Int64))
        InitializeComponent()
        TypeId = _typeId
        docIds = idDeAgrupamiento
        LoadData()
    End Sub

    Public Sub New(ByVal _typeId As GroupToNotifyTypes, ByVal idDeAgrupamiento As Int64, ByVal doctypeid As Int64)
        'Diego: esta sobrecarga la uso para incoorporar una opcion a nivel doctype
        'por si se quiere Mandar un mail, o msj Interno
        InitializeComponent()


        notifyByMail = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.NotifyOptions, doctypeid)



        TypeId = _typeId
        GroupId = idDeAgrupamiento

        Select Case notifyByMail
            Case True
                LoadData()
            Case False
                TxtUsuariosExternos.Visible = False
                lblExternalUsers.Text = "Notifica Mediante Mensaje Interno"
                'DONE:Aca debe ir el metodo LoadDate sobrecargado , que contemple que muestre todo los usuarios de zamba.
                ' No se debe contemplar si tienen o no mail configurado. [sebastian] 04/09/2008
                LoadDataMensajeInterno()
        End Select
    End Sub

    Public Sub New(ByVal _typeId As GroupToNotifyTypes, ByVal ruleid As Int64, ByVal NotifyBy As MailTypes, ByVal showAcceptCancelButtons As Boolean)
        'Diego: esta sobrecarga la uso para Enviar alertas de Reglas
        InitializeComponent()

        If NotifyBy = MailTypes.Internal Then
            notifyByMail = False
            TxtUsuariosExternos.Visible = False
            lblExternalUsers.Text = "Notifica Mediante Mensaje Interno"
        Else
            notifyByMail = True
        End If
        If showAcceptCancelButtons = False Then
            btnAceptar.Visible = False
            btnCancelar.Visible = False
        End If

        TypeId = _typeId
        GroupId = ruleid

        LoadData()
    End Sub
#End Region

#Region "Eventos"

    'Quita el usuario de la lista NoSelected y lo agrega a Selected
    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAgregar.Click

        Try
            If TxtUsuariosExternos.Text = String.Empty Then
                'Creo una copia por valor de la lista para que no me de
                'error mientras la recorro y quito elementos.
                _selectedUsers = New Generic.List(Of User)
                For Each u As iuser In lstNoSelectedUsers.SelectedItems
                    _selectedUsers.Add(DirectCast(u, User))
                Next
                _selectedGroups = New Generic.List(Of UserGroup)
                For Each g As iusergroup In lstNoSelectedGroups.SelectedItems
                    _selectedGroups.Add(DirectCast(g, UserGroup))
                Next

                'Se cargan y se descargan los ListBox
                For Each u As iuser In _selectedUsers
                    lstSelectedUsers.Items.Add(u)
                    lstNoSelectedUsers.Items.Remove(u)
                    'Me.lstNoSelectedUsers.Items.Remove(u)
                Next
                For Each g As iusergroup In _selectedGroups
                    lstSelectedGroups.Items.Add(g)
                    lstNoSelectedGroups.Items.Remove(g)
                Next


                'Logica interna: aca se carga la lista UsuariosAgregados y se
                'descarga de UsuariosBorrados (validando en los dos casos)
                For Each u As iuser In _selectedUsers
                    If Not usuariosAgregados.Contains(DirectCast(u, User)) Then
                        usuariosAgregados.Add(DirectCast(u, User))
                    End If
                    If usuariosBorrados.Contains(DirectCast(u, User)) Then
                        usuariosBorrados.Remove(DirectCast(u, User))
                    End If
                Next
                For Each g As iusergroup In _selectedGroups
                    If Not gruposAgregados.Contains(DirectCast(g, UserGroup)) Then
                        gruposAgregados.Add(DirectCast(g, UserGroup))
                    End If
                    If gruposBorrados.Contains(DirectCast(g, UserGroup)) Then
                        gruposBorrados.Remove(DirectCast(g, UserGroup))
                    End If
                Next

                lstSelectedUsers.Refresh()
                lstNoSelectedUsers.Refresh()
                lstSelectedGroups.Refresh()
                lstNoSelectedGroups.Refresh()
            Else

                If TxtUsuariosExternos.Text.Contains("@") AndAlso TxtUsuariosExternos.Text.Contains(".") Then
                    If lstSelectedUsers.Items.Contains(TxtUsuariosExternos.Text) Then
                        MessageBox.Show("La direccion de mail ya se encuentra insertada", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        lstSelectedUsers.Items.Add(TxtUsuariosExternos.Text)
                        extmails.Add(TxtUsuariosExternos.Text)
                        TxtUsuariosExternos.Text = String.Empty
                    End If
                Else
                    MessageBox.Show("Debe ingresar una direccion de mail valida", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TxtUsuariosExternos.Text = String.Empty
                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    'Quita el usuario de la lista Selected y lo agrega a NoSelected
    Private Sub btnQuitar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnQuitar.Click

        Try
            If Not IsNothing(lstSelectedUsers.SelectedItem) Then

                If lstSelectedUsers.SelectedItem.ToString.Contains("@") AndAlso lstSelectedUsers.SelectedItem.ToString.Contains(".") Then
                    extmails.Remove(lstSelectedUsers.SelectedItem.ToString)
                    'NotifyBusiness.DeleteMailToNotify(Me.GroupId, lstSelectedUsers.SelectedItem.ToString)
                    lstSelectedUsers.Items.Remove(lstSelectedUsers.SelectedItem.ToString)

                Else
                    'Creo una copia por valor de la lista para que no me de
                    'error mientras la recorro y quito elementos.
                    _selectedUsers = New Generic.List(Of User)
                    For Each u As IUser In lstSelectedUsers.SelectedItems
                        _selectedUsers.Add(DirectCast(u, User))
                    Next

                    'Se cargan y se descargan los ListBox
                    For Each u As IUser In _selectedUsers
                        lstNoSelectedUsers.Items.Add(u)
                        lstSelectedUsers.Items.Remove(u)
                    Next

                    'Logica interna: aca se carga la lista UsuariosBorrados y se
                    'descarga de UsuariosAgregados (validando en los dos casos)
                    For Each u As IUser In _selectedUsers
                        If Not usuariosBorrados.Contains(DirectCast(u, User)) Then
                            usuariosBorrados.Add(DirectCast(u, User))
                        End If
                        If usuariosAgregados.Contains(DirectCast(u, User)) Then
                            usuariosAgregados.Remove(DirectCast(u, User))
                        End If
                    Next

                    lstSelectedUsers.Refresh()
                    lstNoSelectedUsers.Refresh()

                End If
            End If

            If Not IsNothing(lstSelectedGroups.SelectedItem) Then

                'Creo una copia por valor de la lista para que no me de
                'error mientras la recorro y quito elementos.
                _selectedGroups = New Generic.List(Of UserGroup)
                For Each g As IUserGroup In lstSelectedGroups.SelectedItems
                    _selectedGroups.Add(DirectCast(g, UserGroup))
                Next

                'Se cargan y se descargan los ListBox
                For Each g As IUserGroup In _selectedGroups
                    lstNoSelectedGroups.Items.Add(g)
                    lstSelectedGroups.Items.Remove(g)
                Next

                'Logica interna: aca se carga la lista UsuariosBorrados y se
                'descarga de UsuariosAgregados (validando en los dos casos)
                For Each g As IUserGroup In _selectedGroups
                    If Not gruposBorrados.Contains(DirectCast(g, UserGroup)) Then
                        gruposBorrados.Add(DirectCast(g, UserGroup))
                    End If
                    If gruposAgregados.Contains(DirectCast(g, UserGroup)) Then
                        gruposAgregados.Remove(DirectCast(g, UserGroup))
                    End If
                Next

                lstSelectedGroups.Refresh()
                lstNoSelectedGroups.Refresh()

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click

        Aceptar()

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click

        RaiseEvent eCerrar(False)

    End Sub

    'Guarda los usuarios seleccionados en la lista resultUsers
    Public Sub Aceptar()


        For Each u As IUser In usuariosAgregados

            'If Me.GroupId <> 0 Then
            '    NotifyBusiness.SetNewUserToNotify(Me.TypeId, Me.GroupId, u.ID)
            'Else
            '    'For Each DocId As Long In docIds
            '    NotifyBusiness.SetNewUserToNotify(Me.TypeId, docIds.Item(0), u.ID)
            '    'Next
            'End If
            If _messageID > 0 Then
                ZForoBusiness.InsertMessageParticipant(_messageID, u.ID)
            End If

        Next

        For Each u As IUser In usuariosBorrados
            'If Me.GroupId <> 0 Then
            '    NotifyBusiness.DeleteUserToNotify(Me.GroupId, u.ID)
            'Else
            '    'For Each docid As Long In docIds
            '    NotifyBusiness.DeleteUserToNotify(docIds.Item(0), u.ID)
            '    'Next
            'End If

            If _messageID > 0 Then
                ZForoBusiness.RemoveParticipant(_messageID, u.ID)
            End If
        Next

        For Each g As IUserGroup In gruposAgregados
            'If Me.GroupId <> 0 Then
            '    NotifyBusiness.SetNewUserGroupToNotify(Me.TypeId, Me.GroupId, g.ID)
            'Else
            '    'For Each docid As Long In docIds
            '    NotifyBusiness.SetNewUserGroupToNotify(Me.TypeId, docIds.Item(0), g.ID)
            '    'Next
            'End If
            If _messageID > 0 Then
                ZForoBusiness.InsertMessageParticipant(_messageID, g.ID)
            End If
        Next

        For Each g As IUserGroup In gruposBorrados
            'If Me.GroupId <> 0 Then
            '    NotifyBusiness.DeleteUserGroupToNotify(Me.GroupId, g.ID)
            'Else
            '    'For Each docid As Long In docIds
            '    NotifyBusiness.DeleteUserGroupToNotify(docIds.Item(0), g.ID)
            '    'Next
            'End If

            If _messageID > 0 Then
                ZForoBusiness.RemoveParticipant(_messageID, g.ID)
            End If
        Next

        For Each mail As String In extmails

            'If Me.GroupId <> 0 Then
            '    NotifyBusiness.SetNewMailToNotify(Me.TypeId, Me.GroupId, mail)
            'Else
            '    'For Each docid As Long In docIds
            '    NotifyBusiness.SetNewMailToNotify(Me.TypeId, docIds(0), mail)
            '    'Next
            'End If
        Next

        RaiseEvent eCerrar(True)

    End Sub


#End Region

#Region "Metodos"
    Private Sub LoadData(Optional ByVal participantIds As Generic.List(Of Int64) = Nothing)

        lstSelectedGroups.Controls.Clear()
        lstSelectedUsers.Controls.Clear()
        'Todas estas listas se utilizan solo en el new
        Dim idRecipesUsers As Generic.List(Of Int64)
        Dim usuarios As New Generic.List(Of User)
        Dim recipesUsers As New Generic.List(Of User)
        Dim recipesUserGroups As New Generic.List(Of UserGroup)
        Dim _user As IUser
        Dim idSelectedGroups As Generic.List(Of Int64)
        Dim TempGroupList As Generic.List(Of UserGroup)
        Dim TempUserList As Generic.List(Of User)
        Dim externalmail As New ArrayList
        Dim tmpGroup As IUserGroup

        Try
            'Cargo todos los usuarios
            TempUserList = UserBusiness.GetUsersWithMailsNames()

            'Esta parte de filtrar los usuarios para que no muestre los que estan con nombres vacios es una crotada
            'pero funciona para las base de datos desactualizadas que permitian usuarios sin nombre y apellido.
            'El alta de usuario fue modificado para que sea imposible un user sin nombre ni apellido
            '[Andres] 20/03/2007
            For Each CurrentUser As IUser In TempUserList
                If String.IsNullOrEmpty(CurrentUser.Description.Trim) Then
                    CurrentUser.Description = CurrentUser.Apellidos & " " & CurrentUser.Nombres
                    If Not String.IsNullOrEmpty(CurrentUser.Description.Trim) Then
                        usuarios.Add(DirectCast(CurrentUser, User))
                    End If
                Else
                    usuarios.Add(DirectCast(CurrentUser, User))
                End If
            Next

            'Cargo todos los grupos
            TempGroupList = UserGroupBusiness.GetUserGroups()

            'Pasa los IDs de Usuario seleccionados a User
            If participantIds IsNot Nothing Then
                'Pasa al editarlos participantes de una conversación, una vez que el mensaje ya fué creado.
                idRecipesUsers = participantIds
            Else
                'Pasa la primera vez que se seleccionan los participantes de un mensaje.
                If docIds.Count > 0 Then
                    idRecipesUsers = ZForoBusiness.GetUsersToNotify(_messageID)
                Else
                    idRecipesUsers = ZForoBusiness.GetUsersToNotify(_messageID)
                End If
            End If


            For Each id As Int64 In idRecipesUsers
                _user = UserBusiness.GetUserById(id)
                If Not IsNothing(_user) Then
                    recipesUsers.Add(DirectCast(_user, User))
                End If
                _user = Nothing
            Next

            If notifyByMail = True Then
                If docIds.Count > 0 Then
                    externalmail = NotifyBusiness.GetGroupExternalMails(TypeId, docIds)
                Else
                    externalmail = NotifyBusiness.GetGroupExternalMails(TypeId, GroupId)
                End If
            End If

            'Paso los IDs de Grupo seleccionados a UserGroups
            idSelectedGroups = ZForoBusiness.GetGroupsToNotify(_messageID)
            'If Me.docIds.Count > 0 Then

            '    idSelectedGroups = NotifyBusiness.GetGroupToNotifyGroups(Me.TypeId, Me.docIds)
            'Else
            '    idSelectedGroups = NotifyBusiness.GetGroupToNotifyGroups(Me.TypeId, Me.GroupId)
            'End If

            For Each groupID As Int64 In idSelectedGroups
                tmpGroup = UserGroupBusiness.GetUserGroupAsUserGroup(groupID)
                recipesUserGroups.Add(DirectCast(tmpGroup, UserGroup))
                tmpGroup = Nothing
            Next

            'Cargo los usuarios seleccionados
            For Each rU As IUser In recipesUsers
                For Each u As IUser In usuarios
                    If rU.ID = u.ID Then
                        selectedUsers.Add(DirectCast(u, User))
                    End If
                Next
            Next

            For Each rG As IUserGroup In recipesUserGroups
                For Each g As IUserGroup In TempGroupList
                    If rG.ID = g.ID Then
                        selectedGroups.Add(DirectCast(g, UserGroup))
                    End If
                Next
            Next

            'Cargo los grupos no seleccionados
            For Each G As IUserGroup In TempGroupList
                If G.Name = "WF" Then
                    Dim i As Integer
                    i = 0
                End If
                If Not selectedGroups.Contains(DirectCast(G, UserGroup)) Then
                    nonSelectedGroups.Add(DirectCast(G, UserGroup))
                End If
            Next

            'Cargo los usuarios no seleccionados
            For Each U As IUser In usuarios
                If Not selectedUsers.Contains(DirectCast(U, User)) Then
                    nonSelectedUsers.Add(DirectCast(U, User))
                End If
            Next

            'Muestro los usuarios seleccionados
            For Each u As IUser In selectedUsers
                lstSelectedUsers.Items.Add(u)
            Next

            'Muestro los grupos seleccionados
            For Each G As IUserGroup In selectedGroups
                lstSelectedGroups.Items.Add(G)
            Next

            'Muestro los usuarios no seleccionados
            For Each u As IUser In nonSelectedUsers
                lstNoSelectedUsers.Items.Add(u)
            Next

            'Muestro los grupos no seleccionados
            For Each g As IUserGroup In nonSelectedGroups
                lstNoSelectedGroups.Items.Add(g)
            Next

            'Me.lstNoSelectedUsers.DataSource = nonSelectedUsers

            'Configuro lo que deben mostrar los ListBox
            lstSelectedUsers.DisplayMember = "Description"
            lstNoSelectedUsers.DisplayMember = "Description"
            lstSelectedGroups.DisplayMember = "Name"
            lstNoSelectedGroups.DisplayMember = "Name"
            For Each exmail As String In externalmail
                lstSelectedUsers.Items.Add(exmail)
            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)

        Finally
            idRecipesUsers = Nothing
            usuarios = Nothing
            recipesUsers = Nothing
            recipesUserGroups = Nothing
            _user = Nothing
            idSelectedGroups = Nothing
            TempGroupList = Nothing
            TempUserList = Nothing
            externalmail = Nothing
            tmpGroup = Nothing
        End Try
    End Sub

    '''
    '''Este metodo sirve para..
    '''
    '''
    Private Sub LoadDataMensajeInterno()


        'Todas estas listas se utilizan solo en el new
        Dim idRecipesUsers As Generic.List(Of Int64)
        Dim usuarios As New Generic.List(Of User)
        Dim recipesUsers As New Generic.List(Of User)
        Dim recipesUserGroups As New Generic.List(Of UserGroup)

        Dim idSelectedGroups As Generic.List(Of Int64)

        Try
            'Cargo todos los usuarios
            Dim TempUserList As Generic.List(Of User) = UserBusiness.GetUsersNames()

            'Esta parte de filtrar los usuarios para que no muestre los que estan con nombres vacios es una crotada
            'pero funciona para las base de datos desactualizadas que permitian usuarios sin nombre y apellido.
            'El alta de usuario fue modificado para que sea imposible un user sin nombre ni apellido
            '[Andres] 20/03/2007
            For Each CurrentUser As IUser In TempUserList
                If String.IsNullOrEmpty(CurrentUser.Description.Trim) Then
                    CurrentUser.Description = CurrentUser.Apellidos & " " & CurrentUser.Nombres
                    If Not String.IsNullOrEmpty(CurrentUser.Description.Trim) Then
                        usuarios.Add(DirectCast(CurrentUser, User))
                    End If
                Else
                    usuarios.Add(DirectCast(CurrentUser, User))
                End If
            Next

            'Cargo todos los grupos
            Dim TempGroupList As Generic.List(Of UserGroup) = UserGroupBusiness.GetUserGroups()

            'Paso los IDs de Usuario seleccionados a User

            idRecipesUsers = ZForoBusiness.GetUsersToNotify(_messageID)
            Dim _user As IUser
            For Each id As Int64 In idRecipesUsers
                _user = UserBusiness.GetUserById(id)
                If Not IsNothing(_user) Then
                    recipesUsers.Add(DirectCast(_user, User))
                End If
                _user = Nothing
            Next
            Dim externalmail As New ArrayList
            If notifyByMail = True Then
                externalmail = NotifyBusiness.GetGroupExternalMails(TypeId, GroupId)
            End If

            'Paso los IDs de Grupo seleccionados a UserGroups
            idSelectedGroups = ZForoBusiness.GetGroupsToNotify(_messageID)
            Dim tmpGroup As IUserGroup
            For Each groupID As Int64 In idSelectedGroups
                tmpGroup = UserGroupBusiness.GetUserGroupAsUserGroup(groupID)
                recipesUserGroups.Add(DirectCast(tmpGroup, UserGroup))
                tmpGroup = Nothing
            Next


            'Cargo los usuarios seleccionados
            For Each rU As IUser In recipesUsers
                For Each u As IUser In usuarios
                    If rU.ID = u.ID Then
                        selectedUsers.Add(DirectCast(u, User))
                    End If
                Next
            Next

            For Each rG As IUserGroup In recipesUserGroups
                For Each g As IUserGroup In TempGroupList
                    If rG.ID = g.ID Then
                        selectedGroups.Add(DirectCast(g, UserGroup))
                    End If
                Next
            Next

            'Cargo los grupos no seleccionados
            For Each G As IUserGroup In TempGroupList
                If G.Name = "WF" Then
                    Dim i As Integer
                    i = 0
                End If
                If Not selectedGroups.Contains(DirectCast(G, UserGroup)) Then
                    nonSelectedGroups.Add(DirectCast(G, UserGroup))
                End If
            Next

            'Cargo los usuarios no seleccionados
            For Each U As IUser In usuarios
                If Not selectedUsers.Contains(DirectCast(U, User)) Then
                    nonSelectedUsers.Add(DirectCast(U, User))
                End If
            Next

            'Muestro los usuarios seleccionados
            For Each u As IUser In selectedUsers
                lstSelectedUsers.Items.Add(u)
            Next

            'Muestro los grupos seleccionados
            For Each G As IUserGroup In selectedGroups
                lstSelectedGroups.Items.Add(G)
            Next

            'Muestro los usuarios no seleccionados
            For Each u As IUser In nonSelectedUsers
                lstNoSelectedUsers.Items.Add(u)
            Next

            'Muestro los grupos no seleccionados
            For Each g As IUserGroup In nonSelectedGroups
                lstNoSelectedGroups.Items.Add(g)
            Next

            'Me.lstNoSelectedUsers.DataSource = nonSelectedUsers

            'Configuro lo que deben mostrar los ListBox
            lstSelectedUsers.DisplayMember = "Description"
            lstNoSelectedUsers.DisplayMember = "Description"
            lstSelectedGroups.DisplayMember = "Name"
            lstNoSelectedGroups.DisplayMember = "Name"
            For Each exmail As String In externalmail
                lstSelectedUsers.Items.Add(exmail)
            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

    Private Sub lstNoSelectedGroups_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lstNoSelectedGroups.DoubleClick
        If lstNoSelectedGroups.SelectedItems.Count > 0 Then
            _selectedGroups = New Generic.List(Of UserGroup)
            For Each g As iusergroup In lstNoSelectedGroups.SelectedItems
                _selectedGroups.Add(DirectCast(g, UserGroup))
            Next

            For Each g As iusergroup In _selectedGroups
                lstSelectedGroups.Items.Add(g)
                lstNoSelectedGroups.Items.Remove(g)
            Next

            For Each g As iusergroup In _selectedGroups
                If Not gruposAgregados.Contains(DirectCast(g, UserGroup)) Then
                    gruposAgregados.Add(DirectCast(g, UserGroup))
                End If
                If gruposBorrados.Contains(DirectCast(g, UserGroup)) Then
                    gruposBorrados.Remove(DirectCast(g, UserGroup))
                End If
            Next

            lstSelectedGroups.Refresh()
            lstNoSelectedGroups.Refresh()

        End If
    End Sub


    Private Sub lstNoSelectedUsers_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lstNoSelectedUsers.DoubleClick
        If lstNoSelectedUsers.SelectedItems.Count > 0 Then
            _selectedUsers = New Generic.List(Of User)
            For Each u As iuser In lstNoSelectedUsers.SelectedItems
                _selectedUsers.Add(DirectCast(u, User))
            Next



            'Se cargan y se descargan los ListBox
            For Each u As iuser In _selectedUsers
                lstSelectedUsers.Items.Add(u)
                lstNoSelectedUsers.Items.Remove(u)
                'Me.lstNoSelectedUsers.Items.Remove(u)
            Next


            'Logica interna: aca se carga la lista UsuariosAgregados y se
            'descarga de UsuariosBorrados (validando en los dos casos)
            For Each u As iuser In _selectedUsers
                If Not usuariosAgregados.Contains(DirectCast(u, User)) Then
                    usuariosAgregados.Add(DirectCast(u, User))
                End If
                If usuariosBorrados.Contains(DirectCast(u, User)) Then
                    usuariosBorrados.Remove(DirectCast(u, User))
                End If
            Next

        End If

        lstSelectedUsers.Refresh()
        lstNoSelectedUsers.Refresh()
    End Sub

    Private Sub lstSelectedGroups_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lstSelectedGroups.DoubleClick
        If lstSelectedGroups.SelectedItems.Count > 0 Then

            'Creo una copia por valor de la lista para que no me de
            'error mientras la recorro y quito elementos.
            _selectedGroups = New Generic.List(Of UserGroup)
            For Each g As iusergroup In lstSelectedGroups.SelectedItems
                _selectedGroups.Add(DirectCast(g, UserGroup))
            Next

            'Se cargan y se descargan los ListBox
            For Each g As iusergroup In _selectedGroups
                lstNoSelectedGroups.Items.Add(g)
                lstSelectedGroups.Items.Remove(g)
            Next

            'Logica interna: aca se carga la lista UsuariosBorrados y se
            'descarga de UsuariosAgregados (validando en los dos casos)
            For Each g As iusergroup In _selectedGroups
                If Not gruposBorrados.Contains(DirectCast(g, UserGroup)) Then
                    gruposBorrados.Add(DirectCast(g, UserGroup))
                End If
                If gruposAgregados.Contains(DirectCast(g, UserGroup)) Then
                    gruposAgregados.Remove(DirectCast(g, UserGroup))
                End If
            Next

            lstSelectedGroups.Refresh()
            lstNoSelectedGroups.Refresh()

        End If
    End Sub


    Private Sub lstSelectedUsers_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lstSelectedUsers.DoubleClick
        If lstSelectedUsers.SelectedItems.Count > 0 Then
            If lstSelectedUsers.SelectedItem.ToString.Contains("@") AndAlso lstSelectedUsers.SelectedItem.ToString.Contains(".") Then
                extmails.Remove(lstSelectedUsers.SelectedItem.ToString)
                'NotifyBusiness.DeleteMailToNotify(Me.GroupId, lstSelectedUsers.SelectedItem.ToString)
                lstSelectedUsers.Items.Remove(lstSelectedUsers.SelectedItem.ToString)
            Else

                'Creo una copia por valor de la lista para que no me de
                'error mientras la recorro y quito elementos.
                _selectedUsers = New Generic.List(Of User)
                For Each u As iuser In lstSelectedUsers.SelectedItems
                    _selectedUsers.Add(DirectCast(u, User))
                Next

                'Se cargan y se descargan los ListBox
                For Each u As iuser In _selectedUsers
                    lstNoSelectedUsers.Items.Add(u)
                    lstSelectedUsers.Items.Remove(u)
                Next

                'Logica interna: aca se carga la lista UsuariosBorrados y se
                'descarga de UsuariosAgregados (validando en los dos casos)
                For Each u As iuser In _selectedUsers
                    If Not usuariosBorrados.Contains(DirectCast(u, User)) Then
                        usuariosBorrados.Add(DirectCast(u, User))
                    End If
                    If usuariosAgregados.Contains(DirectCast(u, User)) Then
                        usuariosAgregados.Remove(DirectCast(u, User))
                    End If
                Next
            End If
        End If
        lstSelectedUsers.Refresh()
        lstNoSelectedUsers.Refresh()
    End Sub

    Private Sub lstNoSelectedGroups_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstNoSelectedGroups.SelectedIndexChanged

    End Sub

End Class
