Public Class SMTP_Validada
    Implements ISMTP_Validada

#Region " Atributos "
    Private _user As String = String.Empty
    Private _password As String = String.Empty
    Private _port As Integer
    Private _server As String = String.Empty
    Private _ssl As Boolean
#End Region

#Region " Propiedades "
    Public Property User() As String Implements ISMTP_Validada.User
        Get
            Return _user
        End Get
        Set(ByVal value As String)
            _user = value
        End Set
    End Property
    Public Property Password() As String Implements ISMTP_Validada.Password
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property
    Public Property Port() As Integer Implements ISMTP_Validada.Port
        Get
            Return _port
        End Get
        Set(ByVal value As Integer)
            _port = value
        End Set
    End Property
    Public Property Server() As String Implements ISMTP_Validada.Server
        Get
            Return _server
        End Get
        Set(ByVal value As String)
            _server = value
        End Set
    End Property
    Public Property SSL() As Boolean Implements ISMTP_Validada.SSL
        Get
            Return _ssl
        End Get
        Set(ByVal value As Boolean)
            _ssl = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByVal user As String, ByVal password As String, ByVal port As Integer, ByVal server As String, ByVal ssl As Boolean)
        _user = user
        _password = password
        _port = port
        _server = server
        _ssl = ssl
    End Sub
    Public Sub New()
    End Sub
#End Region

    Public Sub SMTP_Validada(ByVal user As String, ByVal password As String, ByVal port As Integer, ByVal server As String, ByVal ssl As Boolean) Implements ISMTP_Validada.SMTP_Validada
        _user = user
        _password = password
        _port = port
        _server = server
        _ssl = ssl
    End Sub

End Class