
<Serializable()> Public Class User
    Inherits ZBaseCore
    Implements IUser, IActor, IDisposable

    Private _disposed As Boolean
    Public Overrides Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)

        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        'Para evitar que se haga dispose 2 veces
        If Not _disposed Then
            If disposing Then
                Dim i As Int16

                If Not IsNothing(_groups) Then
                    For i = 0 To _groups.Count - 1
                        If Not IsNothing(_groups(i)) Then
                            DirectCast(_groups(i), UserGroup).Dispose()
                            _groups(i) = Nothing
                        End If
                    Next
                    _groups.Clear()
                End If
            End If

            ' Indicates that the instance has been disposed.
            _disposed = True
            _groups = Nothing
        End If
    End Sub


#Region " Atributos "
    Private _pictureId As Integer
    Private _wfLic As Boolean
    Private _connectionId As Int32
    Private _state As UserState = UserState.Activo
    Private _password As String
    Private _creationDate As Date
    Private _expirationTime As Int32
    Private _lastModificationDate As Date
    Private _description As String
    Private _adressBook As String
    Private _expireDate As String
    Private _nombres As String
    Private _apellidos As String
    Private _puesto As String
    Private _telefono As String
    Private _firma As String
    Private _picture As String
    Private _email As ICorreo = Nothing
    Private _groups As ArrayList = Nothing
    Private _isFull As Boolean
    Private _isLoaded As Boolean
#End Region

#Region " Propiedades "
    Public Property Password() As String Implements IUser.Password
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property
    Public Property Crdate() As Date Implements IUser.Crdate
        Get
            Return _creationDate
        End Get
        Set(ByVal value As Date)
            _creationDate = value
        End Set
    End Property
    Public Property Lmoddate() As Date Implements IUser.Lmoddate
        Get
            Return _lastModificationDate
        End Get
        Set(ByVal value As Date)
            _lastModificationDate = value
        End Set
    End Property
    Public Property Expirationtime() As Integer Implements IUser.Expirationtime
        Get
            Return _expirationTime
        End Get
        Set(ByVal value As Integer)
            _expirationTime = value
        End Set
    End Property
    Public Property AddressBook() As String Implements IUser.AddressBook
        Get
            Return _adressBook
        End Get
        Set(ByVal value As String)
            _adressBook = value
        End Set
    End Property
    Public Property Expiredate() As String Implements IUser.Expiredate
        Get
            Return _expireDate
        End Get
        Set(ByVal value As String)
            _expireDate = value
        End Set
    End Property
    Public Property Nombres() As String Implements IUser.Nombres
        Get
            Return _nombres
        End Get
        Set(ByVal value As String)
            _nombres = value
        End Set
    End Property
    Public Property Apellidos() As String Implements IUser.Apellidos
        Get
            Return _apellidos
        End Get
        Set(ByVal value As String)
            _apellidos = value
        End Set
    End Property
    Public Property puesto() As String Implements IUser.puesto
        Get
            Return _puesto
        End Get
        Set(ByVal value As String)
            _puesto = value
        End Set
    End Property
    Public Property telefono() As String Implements IUser.telefono
        Get
            Return _telefono
        End Get
        Set(ByVal value As String)
            _telefono = value
        End Set
    End Property
    Public Property firma() As String Implements IUser.firma
        Get
            Return _firma
        End Get
        Set(ByVal value As String)
            _firma = value
        End Set
    End Property
    Public Property Picture() As String Implements IUser.Picture
        Get
            Return _picture
        End Get
        Set(ByVal value As String)
            _picture = value
        End Set
    End Property
    Public Property ConnectionId() As Integer Implements IUser.ConnectionId
        Get
            Return _connectionId
        End Get
        Set(ByVal value As Integer)
            _connectionId = value
        End Set
    End Property
    Public Property WFLic() As Boolean Implements IUser.WFLic
        Get
            Return _wfLic
        End Get
        Set(ByVal value As Boolean)
            _wfLic = value
        End Set
    End Property
    Public Property eMail() As ICorreo Implements IUser.eMail
        Get
            'If IsNothing(_email) Then CallForceLoad(Me)
            If IsNothing(_email) Then _email = New Correo()

            Return _email
        End Get
        Set(ByVal value As ICorreo)
            _email = value
        End Set
    End Property
    Public Property Groups() As ArrayList Implements IUser.Groups
        Get
            'If IsNothing(_groups) Then CallForceLoad(Me)
            If IsNothing(_groups) Then _groups = New ArrayList()
            Return _groups
        End Get
        Set(ByVal Value As ArrayList)
            _groups = Value
        End Set

    End Property
    Public Property PictureId1() As Integer Implements IUser.PictureId
        Get
            Return _pictureId
        End Get
        Set(ByVal value As Integer)
            _pictureId = value
        End Set
    End Property
    Public Property Type() As Usertypes Implements IUser.Type
        Get
            Return Usertypes.User
        End Get
        Set(ByVal value As Usertypes)

        End Set
    End Property
    Public Property Description() As String Implements IUser.Description
        Get
            Return _description
        End Get
        Set(ByVal Value As String)
            _description = Value
        End Set
    End Property
    Public Property State() As UserState Implements IUser.State
        Get
            Return _state
        End Get
        Set(ByVal value As UserState)
            _state = value
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
#End Region

#Region " Constructores "
    Public Sub New()
        eMail = New Correo
    End Sub
    Public Sub New(ByVal id As Int64)
        Me.New()
        Me.ID = id
    End Sub
#End Region

    Public Overrides Sub FullLoad()
        CallForceLoad(Me)
    End Sub
    Public Overrides Sub Load()
        CallForceLoad(Me)
    End Sub

    <Serializable()> Public Class UserView
        Implements IUserView

#Region " Atributos "
        Private _user As IUser
#End Region

#Region " Propiedades "
        Public ReadOnly Property Nombre() As String Implements IUserView.Nombre
            Get
                Return _user.Name
            End Get
        End Property
        Public ReadOnly Property Id() As Int32 Implements IUserView.Id
            Get
                Return Convert.ToInt32(_user.ID)
            End Get
        End Property
#End Region

#Region " Constructores "
        Public Sub New(ByVal user As IUser)
            _user = user
        End Sub
#End Region

    End Class

    Private mMailCoonfigLoaded As Boolean
    Public Property MailConfigLoaded() As Boolean Implements IUser.MailConfigLoaded
        Get
            Return mMailCoonfigLoaded
        End Get
        Set(ByVal value As Boolean)
            mMailCoonfigLoaded = value
        End Set
    End Property
End Class