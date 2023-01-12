

''' -----------------------------------------------------------------------------
''' Project	 : ZMessages
''' Class	 : Messages.Destinatario
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para crear un Objeto Destinatario
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	08/04/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class Destinatario
    Implements IDestinatario

#Region " Atributos "
    Private _type As MessageType
    Private _readed As Boolean = False
    Private _address As String = String.Empty
    Private _user As IUser
#End Region

#Region " Propiedades "
    Public Property UserName() As String Implements IDestinatario.UserName
        Get
            Return User.Name
        End Get
        Set(ByVal Value As String)
            User.Name = Value
        End Set
    End Property
    Public Property UserID() As Int64 Implements IDestinatario.UserID
        Get
            Return User.ID
        End Get
        Set(ByVal Value As Int64)
            User.ID = Value
        End Set
    End Property
    Public Property Address() As String Implements IDestinatario.Address
        Get
            Return _address
        End Get
        Set(ByVal value As String)
            _address = value
        End Set
    End Property
    Public Property Readed() As Boolean Implements IDestinatario.Readed
        Get
            Return _readed
        End Get
        Set(ByVal value As Boolean)
            _readed = value
        End Set
    End Property
    Public Property Type() As MessageType Implements IDestinatario.Type
        Get
            Return _type
        End Get
        Set(ByVal value As MessageType)
            _type = value
        End Set
    End Property
    Public Property User() As IUser Implements IDestinatario.User
        Get
            Return _user
        End Get
        Set(ByVal value As IUser)
            _user = value
        End Set
    End Property

#End Region

#Region " Constructores "
    Public Sub New()
        MyBase.new()
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Constructor para destinatarios de mails de Lotus
    ''' </summary>
    ''' <param name="UserAddress"></param>
    ''' <param name="dt"></param>
    ''' <param name="Name"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	08/04/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal UserAddress As String, ByVal dt As MessageType, ByVal Name As String)
        Me.new()
        _user = New User()
        _type = dt
        _address = UserAddress
        UserName = Name
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Constructor para mensajes internos
    ''' </summary>
    ''' <param name="User"></param>
    ''' <param name="dt"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	08/04/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal User As IUser, ByVal dt As MessageType)
        Me.new()
        _user = User
        _type = dt
    End Sub
#End Region


End Class
