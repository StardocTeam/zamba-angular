Imports System.Collections.Generic

''' <summary>
''' Representa un servidor utilizado por Zamba
''' </summary>
''' <remarks></remarks>
Public Class ServerEntity
    Implements IServer

#Region "Attributes and Properties"
    Private _id As Int64
    Private _name As String
    Private _ip As String
    Private _databases As List(Of IDataBase)

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
    ''' <remarks>Host</remarks>
    Public Property Name As String Implements ICore.Name
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    ''' <summary>
    ''' IP del servidor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IP As String Implements IServer.IP
        Get
            Return _ip
        End Get
        Set(ByVal value As String)
            _ip = value
        End Set
    End Property

    ''' <summary>
    ''' Lista de las bases de datos del servidor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Databases As List(Of IDataBase) Implements IServer.Databases
        Get
            Return _databases
        End Get
        Set(ByVal value As List(Of IDataBase))
            _databases = value
        End Set
    End Property
#End Region

#Region "Constructors"
    ''' <summary>
    ''' Genera un nuevo objeto de tipo Server
    ''' </summary>
    ''' <param name="id">Id del servidor</param>
    ''' <param name="name">Nombre del servidor</param>
    ''' <param name="ip">IP del servidor</param>
    ''' <param name="dataBases">Bases de datos del servidor</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal id As Int64, _
                   ByVal name As String, _
                   Optional ByVal ip As String = "", _
                   Optional ByVal dataBases As List(Of IDataBase) = Nothing)
        _id = id
        _name = name
        _ip = ip
        _databases = dataBases
    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If _databases IsNot Nothing Then
                    For i As Int32 = 0 To _databases.Count - 1
                        If _databases(i) IsNot Nothing Then _databases(i).Dispose()
                    Next
                    _databases.Clear()
                End If
            End If

            _id = Nothing
            _name = Nothing
            _ip = Nothing
            _databases = Nothing
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
