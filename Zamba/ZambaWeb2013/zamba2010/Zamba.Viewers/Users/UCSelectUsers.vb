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

#End Region

#Region "Constructores"

    Public Sub New(ByVal typeID As GroupToNotifyTypes, ByVal idDeAgrupamiento As Int64, ByVal mostrarAceptarCancelar As Boolean)
        'Me.New(typeID, idDeAgrupamiento)
        InitializeComponent()
        Me.TypeId = typeID
        Me.GroupId = idDeAgrupamiento
        If Not mostrarAceptarCancelar Then
            Me.btnAceptar.Visible = False
            Me.btnCancelar.Visible = False
        End If
        LoadData()
    End Sub

    Public Sub New(ByVal _typeId As GroupToNotifyTypes, ByVal idDeAgrupamiento As Int64, Optional ByVal participantIds As Generic.List(Of Int64) = Nothing)
        InitializeComponent()
        Me.TypeId = _typeId
        Me.GroupId = idDeAgrupamiento
        LoadData(participantIds)
    End Sub

    Public Sub New(ByVal _typeId As GroupToNotifyTypes, ByVal idDeAgrupamiento As Generic.List(Of Int64))
        InitializeComponent()
        Me.TypeId = _typeId
        Me.docIds = idDeAgrupamiento
        LoadData()
    End Sub

    Public Sub New(ByVal _typeId As GroupToNotifyTypes, ByVal idDeAgrupamiento As Int64, ByVal doctypeid As Int64)
        'Diego: esta sobrecarga la uso para incoorporar una opcion a nivel doctype
        'por si se quiere Mandar un mail, o msj Interno
        InitializeComponent()


        notifyByMail = UserBusiness.Rights.GetUserRights(ObjectTypes.DocTypes, RightsType.NotifyOptions, doctypeid)



        Me.TypeId = _typeId
        Me.GroupId = idDeAgrupamiento

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

        Me.TypeId = _typeId
        Me.GroupId = ruleid

        LoadData()
    End Sub
#End Region

#Region "Eventos"

    'Quita el usuario de la lista NoSelected y lo agrega a Selected
    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click

        Try
            If TxtUsuariosExternos.Text = String.Empty Then
                'Creo una copia por valor de la lista para que no me de
                'error mientras la recorro y quito elementos.
                _selectedUsers = New Generic.List(Of User)
                For Each u As iuser In Me.lstNoSelectedUsers.SelectedItems
                    _selectedUsers.Add(DirectCast(u, User))
                Next
                _selectedGroups = New Generic.List(Of UserGroup)
                For Each g As iusergroup In Me.lstNoSelectedGroups.SelectedItems
                    _selectedGroups.Add(DirectCast(g, UserGroup))
                Next

                'Se cargan y se descargan los ListBox
                For Each u As iuser In _selectedUsers
                    Me.lstSelectedUsers.Items.Add(u)
                    Me.lstNoSelectedUsers.Items.Remove(u)
                    'Me.lstNoSelectedUsers.Items.Remove(u)
                Next
                For Each g As iusergroup In _selectedGroups
                    Me.lstSelectedGroups.Items.Add(g)
                    Me.lstNoSelectedGroups.Items.Remove(g)
                Next


                'Logica interna: aca se carga la lista UsuariosAgregados y se
                'descarga de UsuariosBorrados (validando en los dos casos)
                For Each u As iuser In _selectedUsers
                    If Not Me.usuariosAgregados.Contains(DirectCast(u, User)) Then
                        Me.usuariosAgregados.Add(DirectCast(u, User))
                    End If
                    If Me.usuariosBorrados.Contains(DirectCast(u, User)) Then
                        Me.usuariosBorrados.Remove(DirectCast(u, User))
                    End If
                Next
                For Each g As iusergroup In _selectedGroups
                    If Not Me.gruposAgregados.Contains(DirectCast(g, UserGroup)) Then
                        Me.gruposAgregados.Add(DirectCast(g, UserGroup))
                    End If
                    If Me.gruposBorrados.Contains(DirectCast(g, UserGroup)) Then
                        Me.gruposBorrados.Remove(DirectCast(g, UserGroup))
                    End If
                Next

                Me.lstSelectedUsers.Refresh()
                Me.lstNoSelectedUsers.Refresh()
                Me.lstSelectedGroups.Refresh()
                Me.lstNoSelectedGroups.Refresh()
            Else

                If TxtUsuariosExternos.Text.Contains("@") AndAlso TxtUsuariosExternos.Text.Contains(".") Then
                    If lstSelectedUsers.Items.Contains(TxtUsuariosExternos.Text) Then
                        MessageBox.Show("La direccion de mail ya se encuentra insertada", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        Me.lstSelectedUsers.Items.Add(TxtUsuariosExternos.Text)
                        extmails.Add(TxtUsuariosExternos.Text)
                        TxtUsuariosExternos.Text = String.Empty
                    End If
                Else
                    MessageBox.Show("Debe ingresar una direccion de mail valida", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TxtUsuariosExternos.Text = String.Empty
                End If
            End If

        Catch ex As Exception
            raiseerror(ex)
        End Try

    End Sub

    'Quita el usuario de la lista Selected y lo agrega a NoSelected
    Private Sub btnQuitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitar.Click

        Try
            If Not IsNothing(Me.lstSelectedUsers.SelectedItem) Then

                If lstSelectedUsers.SelectedItem.ToString.Contains("@") AndAlso lstSelectedUsers.SelectedItem.ToString.Contains(".") Then
                    extmails.Remove(lstSelectedUsers.SelectedItem.ToString)
                    NotifyBusiness.DeleteMailToNotify(Me.GroupId, lstSelectedUsers.SelectedItem.ToString)
                    lstSelectedUsers.Items.Remove(lstSelectedUsers.SelectedItem.ToString)

                Else
                    'Creo una copia por valor de la lista para que no me de
                    'error mientras la recorro y quito elementos.
                    _selectedUsers = New Generic.List(Of User)
                    For Each u As iuser In Me.lstSelectedUsers.SelectedItems
                        _selectedUsers.Add(DirectCast(u, User))
                    Next

                    'Se cargan y se descargan los ListBox
                    For Each u As iuser In _selectedUsers
                        Me.lstNoSelectedUsers.Items.Add(u)
                        Me.lstSelectedUsers.Items.Remove(u)
                    Next

                    'Logica interna: aca se carga la lista UsuariosBorrados y se
                    'descarga de UsuariosAgregados (validando en los dos casos)
                    For Each u As iuser In _selectedUsers
                        If Not Me.usuariosBorrados.Contains(DirectCast(u, User)) Then
                            Me.usuariosBorrados.Add(DirectCast(u, User))
                        End If
                        If Me.usuariosAgregados.Contains(DirectCast(u, User)) Then
                            Me.usuariosAgregados.Remove(DirectCast(u, User))
                        End If
                    Next

                    Me.lstSelectedUsers.Refresh()
                    Me.lstNoSelectedUsers.Refresh()

                End If
            End If

            If Not IsNothing(Me.lstSelectedGroups.SelectedItem) Then

                'Creo una copia por valor de la lista para que no me de
                'error mientras la recorro y quito elementos.
                _selectedGroups = New Generic.List(Of UserGroup)
                For Each g As iusergroup In Me.lstSelectedGroups.SelectedItems
                    _selectedGroups.Add(DirectCast(g, UserGroup))
                Next

                'Se cargan y se descargan los ListBox
                For Each g As iusergroup In _selectedGroups
                    Me.lstNoSelectedGroups.Items.Add(g)
                    Me.lstSelectedGroups.Items.Remove(g)
                Next

                'Logica interna: aca se carga la lista UsuariosBorrados y se
                'descarga de UsuariosAgregados (validando en los dos casos)
                For Each g As iusergroup In _selectedGroups
                    If Not Me.gruposBorrados.Contains(DirectCast(g, UserGroup)) Then
                        Me.gruposBorrados.Add(DirectCast(g, UserGroup))
                    End If
                    If Me.gruposAgregados.Contains(DirectCast(g, UserGroup)) Then
                        Me.gruposAgregados.Remove(DirectCast(g, UserGroup))
                    End If
                Next

                Me.lstSelectedGroups.Refresh()
                Me.lstNoSelectedGroups.Refresh()

            End If

        Catch ex As Exception
            raiseerror(ex)
        End Try

    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        Me.Aceptar()

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        RaiseEvent eCerrar(False)

    End Sub

    'Guarda los usuarios seleccionados en la lista resultUsers
    Public Sub Aceptar()

        For Each u As IUser In Me.usuariosAgregados
            If Me.GroupId <> 0 Then
                NotifyBusiness.SetNewUserToNotify(Me.TypeId, Me.GroupId, u.ID)
            Else
                'For Each DocId As Long In docIds

                NotifyBusiness.SetNewUserToNotify(Me.TypeId, docIds.Item(0), u.ID)

                'Next
            End If
        Next

        For Each u As IUser In Me.usuariosBorrados
            If Me.GroupId <> 0 Then
                NotifyBusiness.DeleteUserToNotify(Me.GroupId, u.ID)
            Else
                'For Each docid As Long In docIds
                NotifyBusiness.DeleteUserToNotify(docIds.Item(0), u.ID)
                'Next
            End If
        Next

        For Each g As IUserGroup In Me.gruposAgregados
            If Me.GroupId <> 0 Then
                NotifyBusiness.SetNewUserGroupToNotify(Me.TypeId, Me.GroupId, g.ID)
            Else
                'For Each docid As Long In docIds
                NotifyBusiness.SetNewUserGroupToNotify(Me.TypeId, docIds.Item(0), g.ID)
                'Next
            End If
        Next

        For Each g As IUserGroup In Me.gruposBorrados
            If Me.GroupId <> 0 Then
                NotifyBusiness.DeleteUserGroupToNotify(Me.GroupId, g.ID)
            Else
                'For Each docid As Long In docIds
                NotifyBusiness.DeleteUserGroupToNotify(docIds.Item(0), g.ID)
                'Next
            End If
        Next

        For Each mail As String In extmails

            If Me.GroupId <> 0 Then
                NotifyBusiness.SetNewMailToNotify(Me.TypeId, Me.GroupId, mail)
            Else
                'For Each docid As Long In docIds
                NotifyBusiness.SetNewMailToNotify(Me.TypeId, docIds(0), mail)
                'Next
            End If
        Next

        RaiseEvent eCerrar(True)

    End Sub


#End Region

#Region "Metodos"
    Private Sub LoadData(Optional ByVal participantIds As Generic.List(Of Int64) = Nothing)


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
                If Me.docIds.Count > 0 Then
                    idRecipesUsers = NotifyBusiness.GetGroupToNotifyUsers(Me.TypeId, Me.docIds)
                Else
                    idRecipesUsers = NotifyBusiness.GetGroupToNotifyUsers(Me.TypeId, Me.GroupId)
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
                If Me.docIds.Count > 0 Then
                    externalmail = NotifyBusiness.GetGroupExternalMails(Me.TypeId, Me.docIds)
                Else
                    externalmail = NotifyBusiness.GetGroupExternalMails(Me.TypeId, Me.GroupId)
                End If
            End If

            'Paso los IDs de Grupo seleccionados a UserGroups
            If Me.docIds.Count > 0 Then
                idSelectedGroups = NotifyBusiness.GetGroupToNotifyGroups(Me.TypeId, Me.docIds)
            Else
                idSelectedGroups = NotifyBusiness.GetGroupToNotifyGroups(Me.TypeId, Me.GroupId)
            End If

            For Each groupID As Int64 In idSelectedGroups
                tmpGroup = UserGroupBusiness.GetUserGroupAsUserGroup(groupID)
                recipesUserGroups.Add(DirectCast(tmpGroup, UserGroup))
                tmpGroup = Nothing
            Next

            'Cargo los usuarios seleccionados
            For Each rU As IUser In recipesUsers
                For Each u As IUser In usuarios
                    If rU.ID = u.ID Then
                        Me.selectedUsers.Add(DirectCast(u, User))
                    End If
                Next
            Next

            For Each rG As IUserGroup In recipesUserGroups
                For Each g As IUserGroup In TempGroupList
                    If rG.ID = g.ID Then
                        Me.selectedGroups.Add(DirectCast(g, UserGroup))
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
                    Me.nonSelectedGroups.Add(DirectCast(G, UserGroup))
                End If
            Next

            'Cargo los usuarios no seleccionados
            For Each U As IUser In usuarios
                If Not selectedUsers.Contains(DirectCast(U, User)) Then
                    Me.nonSelectedUsers.Add(DirectCast(U, User))
                End If
            Next

            'Muestro los usuarios seleccionados
            For Each u As IUser In selectedUsers
                Me.lstSelectedUsers.Items.Add(u)
            Next

            'Muestro los grupos seleccionados
            For Each G As IUserGroup In Me.selectedGroups
                Me.lstSelectedGroups.Items.Add(G)
            Next

            'Muestro los usuarios no seleccionados
            For Each u As IUser In nonSelectedUsers
                Me.lstNoSelectedUsers.Items.Add(u)
            Next

            'Muestro los grupos no seleccionados
            For Each g As IUserGroup In Me.nonSelectedGroups
                Me.lstNoSelectedGroups.Items.Add(g)
            Next

            'Me.lstNoSelectedUsers.DataSource = nonSelectedUsers

            'Configuro lo que deben mostrar los ListBox
            Me.lstSelectedUsers.DisplayMember = "Description"
            Me.lstNoSelectedUsers.DisplayMember = "Description"
            Me.lstSelectedGroups.DisplayMember = "Name"
            Me.lstNoSelectedGroups.DisplayMember = "Name"
            For Each exmail As String In externalmail
                Me.lstSelectedUsers.Items.Add(exmail)
            Next

        Catch ex As Exception
            raiseerror(ex)

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

            idRecipesUsers = NotifyBusiness.GetGroupToNotifyUsers(Me.TypeId, Me.GroupId)
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
                externalmail = NotifyBusiness.GetGroupExternalMails(Me.TypeId, Me.GroupId)
            End If

            'Paso los IDs de Grupo seleccionados a UserGroups
            idSelectedGroups = NotifyBusiness.GetGroupToNotifyGroups(Me.TypeId, Me.GroupId)
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
                        Me.selectedUsers.Add(DirectCast(u, User))
                    End If
                Next
            Next

            For Each rG As IUserGroup In recipesUserGroups
                For Each g As IUserGroup In TempGroupList
                    If rG.ID = g.ID Then
                        Me.selectedGroups.Add(DirectCast(g, UserGroup))
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
                    Me.nonSelectedGroups.Add(DirectCast(G, UserGroup))
                End If
            Next

            'Cargo los usuarios no seleccionados
            For Each U As IUser In usuarios
                If Not selectedUsers.Contains(DirectCast(U, User)) Then
                    Me.nonSelectedUsers.Add(DirectCast(U, User))
                End If
            Next

            'Muestro los usuarios seleccionados
            For Each u As IUser In selectedUsers
                Me.lstSelectedUsers.Items.Add(u)
            Next

            'Muestro los grupos seleccionados
            For Each G As IUserGroup In Me.selectedGroups
                Me.lstSelectedGroups.Items.Add(G)
            Next

            'Muestro los usuarios no seleccionados
            For Each u As IUser In nonSelectedUsers
                Me.lstNoSelectedUsers.Items.Add(u)
            Next

            'Muestro los grupos no seleccionados
            For Each g As IUserGroup In Me.nonSelectedGroups
                Me.lstNoSelectedGroups.Items.Add(g)
            Next

            'Me.lstNoSelectedUsers.DataSource = nonSelectedUsers

            'Configuro lo que deben mostrar los ListBox
            Me.lstSelectedUsers.DisplayMember = "Description"
            Me.lstNoSelectedUsers.DisplayMember = "Description"
            Me.lstSelectedGroups.DisplayMember = "Name"
            Me.lstNoSelectedGroups.DisplayMember = "Name"
            For Each exmail As String In externalmail
                Me.lstSelectedUsers.Items.Add(exmail)
            Next

        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

#End Region

    Private Sub lstNoSelectedGroups_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstNoSelectedGroups.DoubleClick
        If lstNoSelectedGroups.SelectedItems.Count > 0 Then
            _selectedGroups = New Generic.List(Of UserGroup)
            For Each g As iusergroup In Me.lstNoSelectedGroups.SelectedItems
                _selectedGroups.Add(DirectCast(g, UserGroup))
            Next

            For Each g As iusergroup In _selectedGroups
                Me.lstSelectedGroups.Items.Add(g)
                Me.lstNoSelectedGroups.Items.Remove(g)
            Next

            For Each g As iusergroup In _selectedGroups
                If Not Me.gruposAgregados.Contains(DirectCast(g, UserGroup)) Then
                    Me.gruposAgregados.Add(DirectCast(g, UserGroup))
                End If
                If Me.gruposBorrados.Contains(DirectCast(g, UserGroup)) Then
                    Me.gruposBorrados.Remove(DirectCast(g, UserGroup))
                End If
            Next

            Me.lstSelectedGroups.Refresh()
            Me.lstNoSelectedGroups.Refresh()

        End If
    End Sub


    Private Sub lstNoSelectedUsers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstNoSelectedUsers.DoubleClick
        If lstNoSelectedUsers.SelectedItems.Count > 0 Then
            _selectedUsers = New Generic.List(Of User)
            For Each u As iuser In Me.lstNoSelectedUsers.SelectedItems
                _selectedUsers.Add(DirectCast(u, User))
            Next



            'Se cargan y se descargan los ListBox
            For Each u As iuser In _selectedUsers
                Me.lstSelectedUsers.Items.Add(u)
                Me.lstNoSelectedUsers.Items.Remove(u)
                'Me.lstNoSelectedUsers.Items.Remove(u)
            Next


            'Logica interna: aca se carga la lista UsuariosAgregados y se
            'descarga de UsuariosBorrados (validando en los dos casos)
            For Each u As iuser In _selectedUsers
                If Not Me.usuariosAgregados.Contains(DirectCast(u, User)) Then
                    Me.usuariosAgregados.Add(DirectCast(u, User))
                End If
                If Me.usuariosBorrados.Contains(DirectCast(u, User)) Then
                    Me.usuariosBorrados.Remove(DirectCast(u, User))
                End If
            Next

        End If

        Me.lstSelectedUsers.Refresh()
        Me.lstNoSelectedUsers.Refresh()
    End Sub

    Private Sub lstSelectedGroups_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSelectedGroups.DoubleClick
        If lstSelectedGroups.SelectedItems.Count > 0 Then

            'Creo una copia por valor de la lista para que no me de
            'error mientras la recorro y quito elementos.
            _selectedGroups = New Generic.List(Of UserGroup)
            For Each g As iusergroup In Me.lstSelectedGroups.SelectedItems
                _selectedGroups.Add(DirectCast(g, UserGroup))
            Next

            'Se cargan y se descargan los ListBox
            For Each g As iusergroup In _selectedGroups
                Me.lstNoSelectedGroups.Items.Add(g)
                Me.lstSelectedGroups.Items.Remove(g)
            Next

            'Logica interna: aca se carga la lista UsuariosBorrados y se
            'descarga de UsuariosAgregados (validando en los dos casos)
            For Each g As iusergroup In _selectedGroups
                If Not Me.gruposBorrados.Contains(DirectCast(g, UserGroup)) Then
                    Me.gruposBorrados.Add(DirectCast(g, UserGroup))
                End If
                If Me.gruposAgregados.Contains(DirectCast(g, UserGroup)) Then
                    Me.gruposAgregados.Remove(DirectCast(g, UserGroup))
                End If
            Next

            Me.lstSelectedGroups.Refresh()
            Me.lstNoSelectedGroups.Refresh()

        End If
    End Sub


    Private Sub lstSelectedUsers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSelectedUsers.DoubleClick
        If lstSelectedUsers.SelectedItems.Count > 0 Then
            If lstSelectedUsers.SelectedItem.ToString.Contains("@") AndAlso lstSelectedUsers.SelectedItem.ToString.Contains(".") Then
                extmails.Remove(lstSelectedUsers.SelectedItem.ToString)
                NotifyBusiness.DeleteMailToNotify(Me.GroupId, lstSelectedUsers.SelectedItem.ToString)
                lstSelectedUsers.Items.Remove(lstSelectedUsers.SelectedItem.ToString)
            Else

                'Creo una copia por valor de la lista para que no me de
                'error mientras la recorro y quito elementos.
                _selectedUsers = New Generic.List(Of User)
                For Each u As iuser In Me.lstSelectedUsers.SelectedItems
                    _selectedUsers.Add(DirectCast(u, User))
                Next

                'Se cargan y se descargan los ListBox
                For Each u As iuser In _selectedUsers
                    Me.lstNoSelectedUsers.Items.Add(u)
                    Me.lstSelectedUsers.Items.Remove(u)
                Next

                'Logica interna: aca se carga la lista UsuariosBorrados y se
                'descarga de UsuariosAgregados (validando en los dos casos)
                For Each u As iuser In _selectedUsers
                    If Not Me.usuariosBorrados.Contains(DirectCast(u, User)) Then
                        Me.usuariosBorrados.Add(DirectCast(u, User))
                    End If
                    If Me.usuariosAgregados.Contains(DirectCast(u, User)) Then
                        Me.usuariosAgregados.Remove(DirectCast(u, User))
                    End If
                Next
            End If
        End If
        Me.lstSelectedUsers.Refresh()
        Me.lstNoSelectedUsers.Refresh()
    End Sub

    Private Sub lstNoSelectedGroups_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstNoSelectedGroups.SelectedIndexChanged

    End Sub

End Class
