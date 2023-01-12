''' <summary>
''' Representa una base de datos utilizada por Zamba
''' </summary>
''' <remarks></remarks>
Public Class DatabaseEntity
    Implements IDataBase

#Region "Attributes and Properties"
    Private _id As Int64
    Private _name As String
    Private _server As IServer
    Private _user As String
    Private _password As String
    Private _winAuthorization As Boolean

    ''' <summary>
    ''' Id del servidor
    ''' </summary>
    ''' <value>Campo numérico</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ID As Long Implements ICore.ID
        Get
            Return _id
        End Get
        Set(value As Long)
            _id = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre del servidor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Name As String Implements ICore.Name
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    ''' <summary>
    ''' Servidor de ubicación de la base de datos
    ''' </summary>
    ''' <value>Servidor de origen de la base de datos</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Server As IServer Implements IDataBase.Server
        Get
            Return _server
        End Get
        Set(ByVal value As IServer)
            _server = value
        End Set
    End Property


    ''' <summary>
    ''' Usuario de inicio de sesión
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property User As String Implements IDataBase.User
        Get
            Return _user
        End Get
        Set(value As String)
            _user = value
        End Set
    End Property

    ''' <summary>
    ''' Password del usuario para iniciar sesión
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Password As String Implements IDataBase.Password
        Get
            Return _password
        End Get
        Set(value As String)
            _password = value
        End Set
    End Property

    ''' <summary>
    ''' Con o sin la opción de autorización de windows
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WinAuthorization As Boolean Implements IDataBase.WinAuthorization
        Get
            Return _winAuthorization
        End Get
        Set(value As Boolean)
            _winAuthorization = value
        End Set
    End Property
#End Region

#Region "Constructors"

    ''' <summary>
    ''' Genera un nuevo objeto de tipo Base de datos
    ''' </summary>
    ''' <param name="id">Id del servidor</param>
    ''' <param name="name">Nombre del servidor</param>
    ''' <param name="server">Servidor donde se encuentra la base de datos</param>
    ''' <param name="user">Usuario utilizado para la conexión</param>
    ''' <param name="password">Contraseña del usuario</param>
    ''' <param name="winAuthorization">Con o sin autorización de windows</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal id As Int64, _
                   ByVal name As String, _
                   Optional ByVal server As IServer = Nothing, _
                   Optional ByVal user As String = "", _
                   Optional ByVal password As String = "", _
                   Optional ByVal winAuthorization As Boolean = False)
        _id = id
        _name = name
        _server = server
        _user = user
        _password = password
        _winAuthorization = winAuthorization
    End Sub

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If _server IsNot Nothing Then _server.Dispose()
            End If

            _id = Nothing
            _name = Nothing
            _server = Nothing
            _user = Nothing
            _password = Nothing
            _winAuthorization = Nothing
        End If
        disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
