
<Serializable()> Public Class Correo
    Implements ICorreo

#Region " Atributos "
    Private _base As String
    Private _servidor As String
    Private _type As MailTypes
    Private _mail As String
    Private _userName As String
    Private _proveedorSMTP As String
    Private _puerto As Int16
    Private _password As String
    Private _enableSsl As Boolean
#End Region

#Region " Propiedades "
    Public Property Type() As MailTypes Implements ICorreo.Type
        Get
            Return _type
        End Get
        Set(ByVal value As MailTypes)
            _type = value
        End Set
    End Property
    Public Property Servidor() As String Implements ICorreo.Servidor
        Get
            Return _servidor
        End Get
        Set(ByVal value As String)
            _servidor = value
        End Set
    End Property
    Public Property Base() As String Implements ICorreo.Base
        Get
            Return _base
        End Get
        Set(ByVal value As String)
            _base = value
        End Set
    End Property
    Public Property Mail() As String Implements ICorreo.Mail
        Get
            Return _mail
        End Get
        Set(ByVal value As String)
            _mail = value
        End Set
    End Property
    Public Property UserName() As String Implements ICorreo.UserName
        Get
            Return _userName
        End Get
        Set(ByVal value As String)
            _userName = value
        End Set
    End Property
    Public Property ProveedorSMTP() As String Implements ICorreo.ProveedorSMTP
        Get
            Return _proveedorSMTP
        End Get
        Set(ByVal value As String)
            _proveedorSMTP = value
        End Set
    End Property
    Public Property Puerto() As Int16 Implements ICorreo.Puerto
        Get
            Return _puerto
        End Get
        Set(ByVal value As Int16)
            _puerto = value
        End Set
    End Property
    Public Property Password() As String Implements ICorreo.Password
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property
    Public Property EnableSsl As Boolean Implements ICorreo.EnableSsl
        Get
            Return _enableSsl
        End Get
        Set(value As Boolean)
            _enableSsl = value
        End Set
    End Property
#End Region

End Class