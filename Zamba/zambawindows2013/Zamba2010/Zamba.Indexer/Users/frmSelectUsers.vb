Imports Zamba.Core


Public Class frmSelectUsers
    Inherits Zamba.AppBlock.ZForm

    Friend WithEvents ucPrincipal As UCSelectUsers

    'Public Sub New(ByVal typeId As GroupToNotifyTypes, ByVal idGroup As Int64)

    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    Me.ucPrincipal = New UCSelectUsers(typeId, idGroup)
    '    Me.Controls.Add(Me.ucPrincipal)
    '    Me.ucPrincipal.Dock = DockStyle.Fill
    'End Sub

    Public Sub New(ByVal typeId As GroupToNotifyTypes, ByVal idGroup As Int64, ByVal doctypeid As Int64)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ucPrincipal = New UCSelectUsers(typeId, idGroup, doctypeid)
        Controls.Add(ucPrincipal)
        ucPrincipal.Dock = DockStyle.Fill
    End Sub

    Public Sub New(ByVal typeId As GroupToNotifyTypes, ByVal idGroup As Int64, ByVal MessageID As Int32, Optional ByVal participantIds As Generic.List(Of Int64) = Nothing)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ucPrincipal = New UCSelectUsers(typeId, idGroup, MessageID, participantIds)
        Controls.Add(ucPrincipal)
        ucPrincipal.Dock = DockStyle.Fill
    End Sub

    Public Sub New(ByVal typeId As GroupToNotifyTypes, ByVal docIds As Generic.List(Of Int64))

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ucPrincipal = New UCSelectUsers(typeId, docIds)
        Controls.Add(ucPrincipal)
        ucPrincipal.Dock = DockStyle.Fill
    End Sub

    Public Sub ucPrincipal_eCerrar(ByVal OK As Boolean) Handles ucPrincipal.eCerrar
        If OK Then
            DialogResult = DialogResult.OK
        Else
            DialogResult = DialogResult.Cancel
        End If
        Close()
    End Sub


#Region "Bkp26/09"
    '#Region "Atributos"

    '    Dim selectedUsers As New Generic.List(Of User)
    '    Dim nonSelectedUsers As New Generic.List(Of User)
    '    Dim usuariosAgregados As New Generic.List(Of User)
    '    Dim usuariosBorrados As New Generic.List(Of User)
    '    Dim _selectedUsers As New Generic.List(Of User)
    '    Dim extmails As New Generic.List(Of String)


    '    Private TypeId As GroupToNotifyTypes
    '    Private GroupId As Int64

    '#End Region

    '#Region "Constructor"
    '    Public Sub New(ByVal _typeId As GroupToNotifyTypes, ByVal _idGroup As Int64)


    '        ' This call is required by the Windows Form Designer.
    '        InitializeComponent()

    '        Me.TypeId = _typeId
    '        Me.GroupId = _idGroup

    '        'Todas estas listas se utilizan solo en el new
    '        Dim idRecipesUsers As ArrayList
    '        Dim usuarios As New Generic.List(Of User)
    '        Dim recipesUsers As New Generic.List(Of User)

    '        Try
    '            'Cargo todos los usuarios
    '            Dim TempUserList As Generic.List(Of User) = UserBusiness.GetUsersWithMailsNames()

    '            'Esta parte de filtrar los usuarios para que no muestre los que estan con nombres vacios es una crotada
    '            'pero funciona para las base de datos desactualizadas que permitian usuarios sin nombre y apellido.
    '            'El alta de usuario fue modificado para que sea imposible un user sin nombre ni apellido
    '            '[Andres] 20/03/2007
    '            For Each CurrentUser as iuser In TempUserList
    '                If String.IsNullOrEmpty(CurrentUser.Description.Trim) Then
    '                    CurrentUser.Description = CurrentUser.Apellidos & " " & CurrentUser.Nombres
    '                    If Not String.IsNullOrEmpty(CurrentUser.Description.Trim) Then
    '                        usuarios.Add(CurrentUser)
    '                    End If
    '                Else
    '                    usuarios.Add(CurrentUser)
    '                End If
    '            Next

    '            'Paso los ids seleccionados a usuarios
    '            idRecipesUsers = UserBusiness.GetGroupToNotify(Me.TypeId, Me.GroupId)
    '            Dim externalmail As ArrayList = UserBusiness.GetGroupExternalMails(Me.TypeId, Me.GroupId)
    '            Dim _user as iuser
    '            For Each id As Int64 In idRecipesUsers
    '                _user = UserBusiness.GetUserById(id)
    '                recipesUsers.Add(_user)
    '                _user = Nothing
    '            Next

    '            'Cargo los usuarios seleccionados
    '            For Each rU as iuser In recipesUsers
    '                For Each u as iuser In usuarios
    '                    If rU.ID = u.ID Then
    '                        selectedUsers.Add(u)
    '                    End If
    '                Next
    '            Next

    '            'Cargo los usuarios no seleccionados
    '            For Each U as iuser In usuarios
    '                If Not selectedUsers.Contains(U) Then
    '                    nonSelectedUsers.Add(U)
    '                End If
    '            Next

    '            'Muestro los usuarios seleccionados
    '            For Each u as iuser In selectedUsers
    '                Me.lstSelectedUsers.Items.Add(u)
    '            Next

    '            'Muestro los usuarios no seleccionados
    '            For Each u as iuser In nonSelectedUsers
    '                Me.lstNoSelectedUsers.Items.Add(u)
    '            Next

    '            'Me.lstNoSelectedUsers.DataSource = nonSelectedUsers

    '            'Configuro lo que deben mostrar los ListBox
    '            Me.lstSelectedUsers.DisplayMember = "Description"
    '            Me.lstNoSelectedUsers.DisplayMember = "Description"
    '            For Each exmail As String In externalmail
    '                Me.lstSelectedUsers.Items.Add(exmail)
    '            Next

    '        Catch ex As Exception
    '           ZClass.raiseerror(ex)
    '        End Try

    '    End Sub

    '#End Region

    '#Region "Eventos"
    '    'Quita el usuario de la lista NoSelected y lo agrega a Selected
    '    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click

    '        Try
    '            If TxtUsuariosExternos.text = String.Empty Then
    '                'Creo una copia por valor de la lista para que no me de
    '                'error mientras la recorro y quito elementos.
    '                _selectedUsers = New Generic.List(Of User)
    '                For Each u as iuser In Me.lstNoSelectedUsers.SelectedItems
    '                    _selectedUsers.Add(u)
    '                Next

    '                'Se cargan y se descargan los ListBox
    '                For Each u as iuser In _selectedUsers
    '                    Me.lstSelectedUsers.Items.Add(u)
    '                    Me.lstNoSelectedUsers.Items.Remove(u)
    '                    'Me.lstNoSelectedUsers.Items.Remove(u)
    '                Next


    '                'Logica interna: aca se carga la lista UsuariosAgregados y se
    '                'descarga de UsuariosBorrados (validando en los dos casos)
    '                For Each u as iuser In _selectedUsers
    '                    If Not Me.usuariosAgregados.Contains(u) Then
    '                        Me.usuariosAgregados.Add(u)
    '                    End If
    '                    If Me.usuariosBorrados.Contains(u) Then
    '                        Me.usuariosBorrados.Remove(u)
    '                    End If
    '                Next

    '                Me.lstSelectedUsers.Refresh()
    '                Me.lstNoSelectedUsers.Refresh()
    '            Else

    '                If TxtUsuariosExternos.Text.Contains("@") AndAlso TxtUsuariosExternos.Text.Contains(".") Then
    '                    If lstSelectedUsers.Items.Contains(TxtUsuariosExternos.Text) Then
    '                        MessageBox.Show("La direccion de mail ya se encuentra insertada", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    Else
    '                        Me.lstSelectedUsers.Items.Add(TxtUsuariosExternos.Text)
    '                        extmails.Add(TxtUsuariosExternos.Text)
    '                        TxtUsuariosExternos.Text = String.Empty
    '                    End If
    '                Else
    '                    MessageBox.Show("Debe ingresar una direccion de mail valida", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    TxtUsuariosExternos.Text = String.Empty
    '                End If
    '            End If

    '        Catch ex As Exception
    '           ZClass.raiseerror(ex)
    '        End Try

    '    End Sub

    '    'Quita el usuario de la lista Selected y lo agrega a NoSelected
    '    Private Sub btnQuitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitar.Click

    '        Try
    '            If lstSelectedUsers.SelectedItem.ToString.Contains("@") AndAlso lstSelectedUsers.SelectedItem.ToString.Contains(".") Then
    '                extmails.Remove(lstSelectedUsers.SelectedItem.ToString)
    '                UserBusiness.DeleteGroupToNotify(Me.GroupId, lstSelectedUsers.SelectedItem.ToString)
    '                lstSelectedUsers.Items.Remove(lstSelectedUsers.SelectedItem.ToString)

    '            Else
    '                'Creo una copia por valor de la lista para que no me de
    '                'error mientras la recorro y quito elementos.
    '                _selectedUsers = New Generic.List(Of User)
    '                For Each u as iuser In Me.lstSelectedUsers.SelectedItems
    '                    _selectedUsers.Add(u)
    '                Next


    '                'Se cargan y se descargan los ListBox
    '                For Each u as iuser In _selectedUsers
    '                    Me.lstNoSelectedUsers.Items.Add(u)
    '                    Me.lstSelectedUsers.Items.Remove(u)
    '                Next

    '                'Logica interna: aca se carga la lista UsuariosBorrados y se
    '                'descarga de UsuariosAgregados (validando en los dos casos)
    '                For Each u as iuser In _selectedUsers
    '                    If Not Me.usuariosBorrados.Contains(u) Then
    '                        Me.usuariosBorrados.Add(u)
    '                    End If
    '                    If Me.usuariosAgregados.Contains(u) Then
    '                        Me.usuariosAgregados.Remove(u)
    '                    End If
    '                Next

    '                Me.lstSelectedUsers.Refresh()
    '                Me.lstNoSelectedUsers.Refresh()
    '            End If
    '        Catch ex As Exception
    '           ZClass.raiseerror(ex)
    '        End Try

    '    End Sub

    '    'Guarda los usuarios seleccionados en la lista resultUsers
    '    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

    '        For Each u as iuser In Me.usuariosAgregados
    '            UserBusiness.SetNewGroupToNotify(Me.TypeId, Me.GroupId, u.ID)
    '        Next

    '        For Each u as iuser In Me.usuariosBorrados
    '            UserBusiness.DeleteGroupToNotify(Me.GroupId, u.ID)
    '        Next
    '        For Each mail As String In extmails
    '            UserBusiness.SetNewGroupToNotify(Me.TypeId, Me.GroupId, mail)
    '        Next
    '        Me.Close()

    '    End Sub

    '    'Cierra el formulario
    '    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
    '        Me.Close()
    '    End Sub

    '#End Region
#End Region

End Class
