

<Serializable()> Public Class UserGroup
    Inherits ZBaseCore
    Implements IUserGroup

#Region " Atributos "
    Private _state As UserState = UserState.Activo
    Private _description As String = String.Empty
    Private _users As ArrayList = Nothing
    Private _createDate As String = String.Empty
    Private _pictureid As Integer
    Private _isFull As Boolean
    Private _isLoaded As Boolean
#End Region

#Region " Propiedades "
    Private Property Password() As String Implements IUserGroup.Password
        Get
            Return String.Empty
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property
    Public Property State() As UserState Implements IUserGroup.State
        Get
            Return _state
        End Get
        Set(ByVal value As UserState)
            _state = value
        End Set
    End Property
    Public Property Type() As Usertypes Implements IUserGroup.Type
        Get
            Return Usertypes.Group
        End Get
        Set(ByVal value As Usertypes)

        End Set
    End Property
    Public Property Description() As String Implements IUserGroup.Description
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property
    Public Property Users() As ArrayList Implements IUserGroup.Users
        Get
            If IsNothing(_users) Then CallForceLoad(Me)
            If IsNothing(_users) Then _users = New ArrayList()

            Return _users
        End Get
        Set(ByVal value As ArrayList)
            _users = value
        End Set
    End Property
    Public Property CreateDate() As String Implements IUserGroup.CreateDate
        Get
            Return _createDate
        End Get
        Set(ByVal value As String)
            _createDate = value
        End Set
    End Property
    Public Property PictureId1() As Integer Implements IUserGroup.PictureId
        Get
            Return _pictureid
        End Get
        Set(ByVal value As Integer)
            _pictureid = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal id As Int32, ByVal name As String, ByVal state As UserState)
        Me.New()
        Me.ID = id
        Me.Name = name
        Me.State = state
    End Sub
#End Region


    Public Overrides Sub FullLoad()
        CallForceLoad(Me)
    End Sub
    Public Overrides Sub Load()
        CallForceLoad(Me)
    End Sub
    Public Overrides Sub Dispose()
    End Sub
End Class