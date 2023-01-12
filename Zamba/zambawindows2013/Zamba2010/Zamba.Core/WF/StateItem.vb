Imports System.Windows.Forms

#Region "Clase privada StateItem que representa un elemento de lstHabilitationStates"

Public Class StateItem
    Inherits ListViewItem

#Region "Atributos"
    ' Lista que contiene los id de usuarios deshabilitados
    Private mUsersDisabled As New Generic.List(Of Int64)
    ' Lista que contiene los id de grupos deshabilitados
    Private mGroupsDisabled As New Generic.List(Of Int64)
#End Region

#Region "Constructores"

    ' 1º Constructor : Lo utilizan los estados de la solapa Habilitación y Estado
    Sub New(ByVal State As WFStepState)
        Text = State.Name
        Tag = State.ID
    End Sub

#End Region

#Region "Propiedades"

    Public ReadOnly Property UsersDisabled() As Generic.List(Of Int64)
        Get
            Return (mUsersDisabled)
        End Get
    End Property

    Public ReadOnly Property GroupsDisabled() As Generic.List(Of Int64)
        Get
            Return (mGroupsDisabled)
        End Get
    End Property

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Se agrega un id de usuario con checkbox deshabilitado
    ''' </summary>
    ''' <param name="tableUsers"></param>
    ''' <remarks></remarks>
    Public Sub addUserDisabled(ByVal idUser As Integer)
        mUsersDisabled.Add(idUser)
    End Sub

    ''' <summary>
    ''' Se elimina un id de usuario con checkbox deshabilitado
    ''' </summary>
    ''' <param name="idUser"></param>
    ''' <remarks></remarks>
    Public Sub deleteUserDisabled(ByVal idUser As Integer)
        mUsersDisabled.Remove(idUser)
    End Sub

    ''' <summary>
    ''' Se agrega un grupo con checkbox deshabilitado
    ''' </summary>
    ''' <param name="tableGroups"></param>
    ''' <remarks></remarks>
    Private Sub addGroupDisabled(ByVal idGroup As Integer)
        mGroupsDisabled.Add(idGroup)
    End Sub

    Private Sub deleteGroupDisabled(ByVal idGroup As Integer)
        mGroupsDisabled.Remove(idGroup)
    End Sub

#End Region

End Class

#End Region

#Region "Clase privada UserItem que representa un elemento de lstUsers"

Public Class UserItem
    Inherits ListViewItem

#Region "Constructor"

    ' Contructor utilizado para crear un elemento usuario y guardarlo adentro de lstUsers
    Sub New(ByVal id As Integer, ByVal name As String)
        Tag = id
        Text = name
    End Sub

#End Region

End Class

#End Region

#Region "Clase privada GroupItem que representa un elemento de lstUserGroups"

Public Class GroupItem
    Inherits ListViewItem

#Region "Constructor"

    ' Contructor utilizado para crear un elemento grupo y guardarlo adentro de lstUserGroups
    Sub New(ByVal id As Integer, ByVal name As String)
        Tag = id
        Text = name
    End Sub

#End Region

End Class

#End Region

#Region "Clase privada BothItem que representa un elemento combinado"

Public Class BothItem

#Region "Atributos"
    Private _userID As Int64
    Private _stateID As Int64
    Private _IndexAndVariableID As Int64
    Private _enabled As Boolean
#End Region

#Region "Constructores"
    Sub New(ByVal StateID As Int64, ByVal userID As Int64, ByVal indexAndVariableID As Int64, ByVal enabled As Int64)
        _userID = userID
        _stateID = StateID
        _IndexAndVariableID = indexAndVariableID
        _enabled = enabled
    End Sub
#End Region

#Region "Propiedades"
    Public ReadOnly Property UserID() As Int64
        Get
            Return _userID
        End Get
    End Property
    Public ReadOnly Property StateID() As Int64
        Get
            Return _stateID
        End Get
    End Property
    Public ReadOnly Property IndexAndVariableID() As Int64
        Get
            Return _IndexAndVariableID
        End Get
    End Property
    Public Property Enabled() As Boolean
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            _enabled = value
        End Set
    End Property
#End Region

End Class
#End Region