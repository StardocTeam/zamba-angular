Imports Zamba.Core

Public Class frmSelectMailUser

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
    Private _ruleid As Int64
    Public Event eCerrar()

#End Region

    Public Sub New(ByVal ruleid As Int64, ByVal NotifyBy As NotifyTypes, ByVal ShowAcceptCancelButtons As Boolean)
        InitializeComponent()

        If NotifyBy = NotifyTypes.InternalMessage Then
            notifyByMail = False
            TxtUsuariosExternos.Visible = False
            lblExternalUsers.Text = "Notifica Mediante Mensaje Interno"
        Else
            notifyByMail = True
        End If
        If ShowAcceptCancelButtons = False Then
            btnAceptar.Visible = False
            btnCancelar.Visible = False
        End If

        _ruleid = ruleid

        LoadData()
    End Sub

#Region "Eventos"

    'Quita el usuario de la lista NoSelected y lo agrega a Selected
    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click

        Try
            If TxtUsuariosExternos.Text = String.Empty Then
                'Creo una copia por valor de la lista para que no me de
                'error mientras la recorro y quito elementos.
                _selectedUsers = New Generic.List(Of User)
                For Each u As IUser In Me.lstNoSelectedUsers.SelectedItems
                    _selectedUsers.Add(u)
                Next
                _selectedGroups = New Generic.List(Of UserGroup)
                For Each g As IUserGroup In Me.lstNoSelectedGroups.SelectedItems
                    _selectedGroups.Add(g)
                Next

                'Se cargan y se descargan los ListBox
                For Each u As IUser In _selectedUsers
                    Me.lstSelectedUsers.Items.Add(u)
                    Me.lstNoSelectedUsers.Items.Remove(u)
                    'Me.lstNoSelectedUsers.Items.Remove(u)
                Next
                For Each g As IUserGroup In _selectedGroups
                    Me.lstSelectedGroups.Items.Add(g)
                    Me.lstNoSelectedGroups.Items.Remove(g)
                Next


                'Logica interna: aca se carga la lista UsuariosAgregados y se
                'descarga de UsuariosBorrados (validando en los dos casos)
                For Each u As IUser In _selectedUsers
                    If Not Me.usuariosAgregados.Contains(u) Then
                        Me.usuariosAgregados.Add(u)
                    End If
                    If Me.usuariosBorrados.Contains(u) Then
                        Me.usuariosBorrados.Remove(u)
                    End If
                Next
                For Each g As IUserGroup In _selectedGroups
                    If Not Me.gruposAgregados.Contains(g) Then
                        Me.gruposAgregados.Add(g)
                    End If
                    If Me.gruposBorrados.Contains(g) Then
                        Me.gruposBorrados.Remove(g)
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
            'If Not IsNothing(Me.lstSelectedUsers.SelectedItem) Then

            '    If lstSelectedUsers.SelectedItem.ToString.Contains("@") AndAlso lstSelectedUsers.SelectedItem.ToString.Contains(".") Then
            '        extmails.Remove(lstSelectedUsers.SelectedItem.ToString)
            '        NotifyBusiness.DeleteMailToNotify(Me.GroupId, lstSelectedUsers.SelectedItem.ToString)
            '        lstSelectedUsers.Items.Remove(lstSelectedUsers.SelectedItem.ToString)

            '    Else
            '        'Creo una copia por valor de la lista para que no me de
            '        'error mientras la recorro y quito elementos.
            '        _selectedUsers = New Generic.List(Of User)
            '        For Each u As IUser In Me.lstSelectedUsers.SelectedItems
            '            _selectedUsers.Add(u)
            '        Next

            '        'Se cargan y se descargan los ListBox
            '        For Each u As IUser In _selectedUsers
            '            Me.lstNoSelectedUsers.Items.Add(u)
            '            Me.lstSelectedUsers.Items.Remove(u)
            '        Next

            '        'Logica interna: aca se carga la lista UsuariosBorrados y se
            '        'descarga de UsuariosAgregados (validando en los dos casos)
            '        For Each u As IUser In _selectedUsers
            '            If Not Me.usuariosBorrados.Contains(u) Then
            '                Me.usuariosBorrados.Add(u)
            '            End If
            '            If Me.usuariosAgregados.Contains(u) Then
            '                Me.usuariosAgregados.Remove(u)
            '            End If
            '        Next

            '        Me.lstSelectedUsers.Refresh()
            '        Me.lstNoSelectedUsers.Refresh()

            '    End If
            'End If

            'If Not IsNothing(Me.lstSelectedGroups.SelectedItem) Then

            '    'Creo una copia por valor de la lista para que no me de
            '    'error mientras la recorro y quito elementos.
            '    _selectedGroups = New Generic.List(Of UserGroup)
            '    For Each g As IUserGroup In Me.lstSelectedGroups.SelectedItems
            '        _selectedGroups.Add(g)
            '    Next

            '    'Se cargan y se descargan los ListBox
            '    For Each g As IUserGroup In _selectedGroups
            '        Me.lstNoSelectedGroups.Items.Add(g)
            '        Me.lstSelectedGroups.Items.Remove(g)
            '    Next

            '    'Logica interna: aca se carga la lista UsuariosBorrados y se
            '    'descarga de UsuariosAgregados (validando en los dos casos)
            '    For Each g As IUserGroup In _selectedGroups
            '        If Not Me.gruposBorrados.Contains(g) Then
            '            Me.gruposBorrados.Add(g)
            '        End If
            '        If Me.gruposAgregados.Contains(g) Then
            '            Me.gruposAgregados.Remove(g)
            '        End If
            '    Next

            '    Me.lstSelectedGroups.Refresh()
            '    Me.lstNoSelectedGroups.Refresh()

            'End If

        Catch ex As Exception
            raiseerror(ex)
        End Try

    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        Me.Aceptar()

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        RaiseEvent eCerrar()

    End Sub

    Private Sub lstNoSelectedGroups_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstNoSelectedGroups.DoubleClick
        If lstNoSelectedGroups.SelectedItems.Count > 0 Then
            _selectedGroups = New Generic.List(Of UserGroup)
            For Each g As IUserGroup In Me.lstNoSelectedGroups.SelectedItems
                _selectedGroups.Add(g)
            Next

            For Each g As IUserGroup In _selectedGroups
                Me.lstSelectedGroups.Items.Add(g)
                Me.lstNoSelectedGroups.Items.Remove(g)
            Next

            For Each g As IUserGroup In _selectedGroups
                If Not Me.gruposAgregados.Contains(g) Then
                    Me.gruposAgregados.Add(g)
                End If
                If Me.gruposBorrados.Contains(g) Then
                    Me.gruposBorrados.Remove(g)
                End If
            Next

            Me.lstSelectedGroups.Refresh()
            Me.lstNoSelectedGroups.Refresh()

        End If
    End Sub

    Private Sub lstNoSelectedUsers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstNoSelectedUsers.DoubleClick
        If lstNoSelectedUsers.SelectedItems.Count > 0 Then
            _selectedUsers = New Generic.List(Of User)
            For Each u As IUser In Me.lstNoSelectedUsers.SelectedItems
                _selectedUsers.Add(u)
            Next

            'Se cargan y se descargan los ListBox
            For Each u As IUser In _selectedUsers
                Me.lstSelectedUsers.Items.Add(u)
                Me.lstNoSelectedUsers.Items.Remove(u)
                'Me.lstNoSelectedUsers.Items.Remove(u)
            Next

            'Logica interna: aca se carga la lista UsuariosAgregados y se
            'descarga de UsuariosBorrados (validando en los dos casos)
            For Each u As IUser In _selectedUsers
                If Not Me.usuariosAgregados.Contains(u) Then
                    Me.usuariosAgregados.Add(u)
                End If
                If Me.usuariosBorrados.Contains(u) Then
                    Me.usuariosBorrados.Remove(u)
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
            For Each g As IUserGroup In Me.lstSelectedGroups.SelectedItems
                _selectedGroups.Add(g)
            Next

            'Se cargan y se descargan los ListBox
            For Each g As IUserGroup In _selectedGroups
                Me.lstNoSelectedGroups.Items.Add(g)
                Me.lstSelectedGroups.Items.Remove(g)
            Next

            'Logica interna: aca se carga la lista UsuariosBorrados y se
            'descarga de UsuariosAgregados (validando en los dos casos)
            For Each g As IUserGroup In _selectedGroups
                If Not Me.gruposBorrados.Contains(g) Then
                    Me.gruposBorrados.Add(g)
                End If
                If Me.gruposAgregados.Contains(g) Then
                    Me.gruposAgregados.Remove(g)
                End If
            Next

            Me.lstSelectedGroups.Refresh()
            Me.lstNoSelectedGroups.Refresh()

        End If
    End Sub

    Private Sub lstSelectedUsers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSelectedUsers.DoubleClick
        'If lstSelectedUsers.SelectedItems.Count > 0 Then
        '    If lstSelectedUsers.SelectedItem.ToString.Contains("@") AndAlso lstSelectedUsers.SelectedItem.ToString.Contains(".") Then
        '        extmails.Remove(lstSelectedUsers.SelectedItem.ToString)
        '        NotifyBusiness.DeleteMailToNotify(Me.GroupId, lstSelectedUsers.SelectedItem.ToString)
        '        lstSelectedUsers.Items.Remove(lstSelectedUsers.SelectedItem.ToString)
        '    Else

        '        'Creo una copia por valor de la lista para que no me de
        '        'error mientras la recorro y quito elementos.
        '        _selectedUsers = New Generic.List(Of User)
        '        For Each u As IUser In Me.lstSelectedUsers.SelectedItems
        '            _selectedUsers.Add(u)
        '        Next

        '        'Se cargan y se descargan los ListBox
        '        For Each u As IUser In _selectedUsers
        '            Me.lstNoSelectedUsers.Items.Add(u)
        '            Me.lstSelectedUsers.Items.Remove(u)
        '        Next

        '        'Logica interna: aca se carga la lista UsuariosBorrados y se
        '        'descarga de UsuariosAgregados (validando en los dos casos)
        '        For Each u As IUser In _selectedUsers
        '            If Not Me.usuariosBorrados.Contains(u) Then
        '                Me.usuariosBorrados.Add(u)
        '            End If
        '            If Me.usuariosAgregados.Contains(u) Then
        '                Me.usuariosAgregados.Remove(u)
        '            End If
        '        Next
        '    End If
        'End If
        'Me.lstSelectedUsers.Refresh()
        'Me.lstNoSelectedUsers.Refresh()
    End Sub

#End Region

#Region "Metodos"

    Private Sub LoadData()

        Dim idRecipesUsers As Generic.List(Of Int64)
        Dim usuarios As New Generic.List(Of User)

        Try
            'Cargo todos los usuarios
            Dim TempUserList As Generic.List(Of User) = UserBusiness.GetUsersWithMailsNames()

            'Cargo todos los grupos
            Dim TempGroupList As Generic.List(Of UserGroup) = UserGroupBusiness.GetUserGroups()

            'Esta parte de filtrar los usuarios para que no muestre los que estan con nombres vacios es una crotada
            'pero funciona para las base de datos desactualizadas que permitian usuarios sin nombre y apellido.
            'El alta de usuario fue modificado para que sea imposible un user sin nombre ni apellido
            '[Andres] 20/03/2007
            For Each CurrentUser As IUser In TempUserList
                If String.IsNullOrEmpty(CurrentUser.Description.Trim) Then
                    CurrentUser.Description = CurrentUser.Apellidos & " " & CurrentUser.Nombres
                    If Not String.IsNullOrEmpty(CurrentUser.Description.Trim) Then
                        usuarios.Add(CurrentUser)
                    End If
                Else
                    usuarios.Add(CurrentUser)
                End If
            Next

            lstNoSelectedUsers.DataSource = usuarios
            lstNoSelectedGroups.DataSource = TempGroupList


            'Me.lstSelectedUsers.DisplayMember = "Description"
            Me.lstNoSelectedUsers.DisplayMember = "Description"
            'Me.lstSelectedGroups.DisplayMember = "Name"
            Me.lstNoSelectedGroups.DisplayMember = "Name"


        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    'Guarda los usuarios seleccionados en la lista resultUsers
    Public Sub Aceptar()

        'For Each u As iuser In Me.usuariosAgregados
        '    notifybusiness.SetNewUserToNotify(Me.TypeId, Me.GroupId, u.ID)
        'Next
        'For Each u As iuser In Me.usuariosBorrados
        '    notifybusiness.DeleteUserToNotify(Me.GroupId, u.ID)
        'Next

        'For Each g As iusergroup In Me.gruposAgregados
        '    notifybusiness.SetNewUserGroupToNotify(Me.TypeId, Me.GroupId, g.ID)
        'Next
        'For Each g As iusergroup In Me.gruposBorrados
        '    NotifyBusiness.DeleteUserGroupToNotify(Me.GroupId, g.ID)
        'Next

        'For Each mail As String In extmails
        '    NotifyBusiness.SetNewMailToNotify(Me.TypeId, Me.GroupId, mail)
        'Next

        'RaiseEvent eCerrar()

    End Sub

#End Region

End Class