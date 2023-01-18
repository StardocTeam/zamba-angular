Imports System.Collections.Generic

''' <summary>
''' Representa un webservice consumido por Zamba
''' </summary>
''' <remarks></remarks>
Public Class WebService
    Implements IWebService

#Region "Attributes and Properties"

    Private _id As Int64
    Private _name As String
    Private _description As String
    Private _server As IServer
    Private _databases As List(Of IDataBase)
    Private _path As String
    Private _webMethods As List(Of String)

    ''' <summary>
    ''' Id del webservice
    ''' </summary>
    ''' <value>Campo numérico</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ID As Long Implements IWebService.Id
        Get
            Return _id
        End Get
        Set(value As Long)
            _id = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre del webservice
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Name As String Implements IWebService.Name
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    ''' <summary>
    ''' Descripción del webservice
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IWebService.Description
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
        End Set
    End Property

    ''' <summary>
    ''' Servidor de ubicación de la base de datos
    ''' </summary>
    ''' <value>Servidor de origen de la base de datos</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Server() As IServer Implements IWebService.Server
        Get
            Return _server
        End Get
        Set(ByVal value As IServer)
            _server = value
        End Set
    End Property

    ''' <summary>
    ''' Lista de las bases de datos donde el webservice obtiene o impacta los datos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Databases() As List(Of IDataBase) Implements IWebService.Databases
        Get
            Return _databases
        End Get
        Set(ByVal value As List(Of IDataBase))
            _databases = value
        End Set
    End Property

    ''' <summary>
    ''' Ruta del webservice
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Path() As String Implements IWebService.Path
        Get
            Return _path
        End Get
        Set(ByVal value As String)
            _path = value
        End Set
    End Property

    ''' <summary>
    ''' Métodos del WebService
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WebMethods() As List(Of String) Implements IWebService.WebMethods
        Get
            Return _webMethods
        End Get
        Set(ByVal value As List(Of String))
            _webMethods = value
        End Set
    End Property

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Genera un nuevo objeto de tipo Base de datos
    ''' </summary>
    ''' <param name="id">Id del servidor</param>
    ''' <param name="name">Nombre del servidor</param>
    ''' <param name="description">Descripción del servidor</param>
    ''' <param name="server">Servidor donde se encuentra ubicado el webservice</param>
    ''' <param name="databases">Bases de datos donde el webservice impacta</param>
    ''' <param name="path">Ruta del webservice</param>
    ''' <param name="webMethods">Metodos del WebService</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal id As Int64, _
                   ByVal name As String, _
                   Optional ByVal description As String = "", _
                   Optional ByVal server As IServer = Nothing, _
                   Optional ByVal databases As List(Of IDataBase) = Nothing, _
                   Optional ByVal path As String = "", _
                   Optional ByVal webMethods As List(Of String) = Nothing)
        _id = id
        _name = name
        _description = description
        _server = server
        _databases = databases
        _path = path
        _webMethods = webMethods
    End Sub

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If _server IsNot Nothing Then _server.Dispose()
                If _databases IsNot Nothing Then
                    For i As Int32 = 0 To _databases.Count - 1
                        If _databases(i) IsNot Nothing Then _databases(i).Dispose()
                    Next
                    _databases.Clear()
                End If
                If _webMethods IsNot Nothing Then _webMethods.Clear()
            End If

            _id = Nothing
            _name = Nothing
            _description = Nothing
            _server = Nothing
            _databases = Nothing
            _path = Nothing
            _webMethods = Nothing
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
