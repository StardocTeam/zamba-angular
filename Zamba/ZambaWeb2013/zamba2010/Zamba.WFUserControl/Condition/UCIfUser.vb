'Imports Zamba.WFBusiness

Public Class UCIfUser
    Inherits ZRuleControl

    Private _currentRule As IIfUser
    Private _availableUsers As New Hashtable

#Region " Constructor y Load "
    Public Sub New(ByRef rule As IIfUser, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = rule
    End Sub

    Private Sub UCIfUser_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load
        LoadListBox(True)
        LoadGroupBox()
        LoadLabel()
    End Sub
#End Region

#Region " Propiedades "
    Public Property CurrentRule() As IIfUser
        Get
            Return _currentRule
        End Get
        Set(ByVal value As IIfUser)
            _currentRule = value
        End Set
    End Property

    Public Property AvailableUsers() As Hashtable
        Get
            Return _availableUsers
        End Get
        Set(ByVal value As Hashtable)
            _availableUsers = value
        End Set
    End Property
#End Region

#Region "Inicialización de valores en pantalla"

    ' Se inicializan: el ListBox1, la comparación (GroupBox) y el label "Usuario:" (estos dos
    ' últimos en el caso de que haya valores en la regla)


    'Carga en el label 'NombreUser' el nombre del usuario (si es que hay alguno guardado en la regla).
    Private Sub LoadLabel()
        Try
            If Not IsNothing(CurrentRule) AndAlso Not IsNothing(CurrentRule.UserId) AndAlso CurrentRule.UserId <> 0 Then
                lbNombreUser.Text = UserGroupBusiness.GetUserorGroupNamebyId(CurrentRule.UserId)
            Else
                lbNombreUser.Text = ""
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try

    End Sub
    'Setea la lista lbUsuarios con los valores de Zamba.Core.UserBusiness.GetUsersArrayList
    Private Sub LoadListBox(LazyLoad As Boolean)
        Try
            lbUsuarios.DataSource = UserBusiness.GetUsersArrayList(LazyLoad)
            lbUsuarios.DisplayMember = "Name"
            lbUsuarios.ValueMember = "ID"
            If lbUsuarios.Items.Count() > 0 AndAlso Not IsNothing(CurrentRule) AndAlso Not IsNothing(CurrentRule.UserId) AndAlso CurrentRule.UserId <> 0 Then
                lbUsuarios.SelectedValue = CurrentRule.UserId
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try


    End Sub
    'Setea el GroupBox con el valor de CurrentRule.Comparator
    Private Sub LoadGroupBox()
        Try
            If Not IsNothing(CurrentRule) AndAlso Not IsNothing(CurrentRule.UserId) Then
                Select Case CurrentRule.Comparator
                    Case UserComparators.AssignedTo
                        rbAsignado.Checked = True
                    Case UserComparators.NotAsignedTo
                        rbNoAsignado.Checked = True
                    Case UserComparators.CurrentUser
                        rbUsuarioActual.Checked = True
                    Case UserComparators.NotCurrentUser
                        rbNoUsuarioActual.Checked = True
                    Case Else
                        rbAsignado.Checked = True
                End Select
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try

    End Sub

#End Region

#Region " Eventos "
    Private Sub lbUsuarios_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lbUsuarios.SelectedIndexChanged
        Dim _user As Zamba.Core.User = lbUsuarios.SelectedItem
        lbNombreUser.Text = _user.Name
    End Sub

    Private Sub btAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btAceptar.Click
        If rbAsignado.Checked Then
            CurrentRule.Comparator = UserComparators.AssignedTo
        ElseIf rbNoAsignado.Checked Then
            CurrentRule.Comparator = UserComparators.NotAsignedTo
        ElseIf rbUsuarioActual.Checked Then
            CurrentRule.Comparator = UserComparators.CurrentUser
        ElseIf rbNoUsuarioActual.Checked Then
            CurrentRule.Comparator = UserComparators.NotCurrentUser
        End If

        If Not IsNothing(lbUsuarios.SelectedItem) Then
            Dim _user As Zamba.Core.User = lbUsuarios.SelectedItem()
            CurrentRule.UserId = _user.ID

        End If

        WFRulesBusiness.UpdateParamItem(_currentRule, 0, _currentRule.UserId)
        WFRulesBusiness.UpdateParamItem(_currentRule, 1, _currentRule.Comparator)
        UserBusiness.Rights.SaveAction(_currentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & _currentRule.Name & "(" & _currentRule.ID & ")")

    End Sub

    Private Sub tbctrMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbctrMain.SelectedIndexChanged

    End Sub
#End Region


End Class
