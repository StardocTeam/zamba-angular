Imports System.Collections.Generic
Imports Zamba.Core

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Foro
''' Class	 : Foro.Mensaje
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para crear mensajes utilizados en el foro
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class MensajeForo
    Inherits ZambaCore
    Implements IMensajeForo


#Region "Atributos"
    Private _DocId As Int32
    Private _ParentId As Int32
    Private _Mensaje As String
    Private _Fecha As Date
    Private _UserId As Integer
    Private _StateId As Integer
    Private _GroupId As Int64
    Private _userName As String
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _diasVto As Int32

#End Region

#Region "Propiedades"
    Public Property UserName() As String Implements IMensajeForo.UserName
        Get
            Return _userName
        End Get
        Set(ByVal value As String)
            _userName = value
        End Set
    End Property
    Public Property DocId() As Integer Implements IMensajeForo.DocId
        Get
            Return _DocId
        End Get
        Set(ByVal Value As Integer)
            _DocId = Value
        End Set
    End Property
    Public Property ParentId() As Integer Implements IMensajeForo.ParentId
        Get
            Return _ParentId
        End Get
        Set(ByVal Value As Integer)
            _ParentId = Value
        End Set
    End Property
    Public Property Mensaje() As String Implements IMensajeForo.Mensaje
        Get
            Return _Mensaje
        End Get
        Set(ByVal Value As String)
            _Mensaje = Value
        End Set
    End Property
    Public Property Fecha() As Date Implements IMensajeForo.Fecha
        Get
            Return _Fecha
        End Get
        Set(ByVal Value As Date)
            _Fecha = Value
        End Set
    End Property
    Public Property UserId() As Integer Implements IMensajeForo.UserId
        Get
            Return _UserId
        End Get
        Set(ByVal Value As Integer)
            _UserId = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Estado del comentario
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property StateId() As Integer Implements IMensajeForo.StateId
        Get
            Return _StateId
        End Get
        Set(ByVal Value As Integer)
            _StateId = Value
        End Set
    End Property
    Public Property GroupId() As Int64 Implements IMensajeForo.GroupId
        Get
            Return Me._GroupId
        End Get
        Set(ByVal value As Int64)
            Me._GroupId = value
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

    Public Property DiasVto() As Int32 Implements IMensajeForo.DiasVto
        Get
            Return _diasVto
        End Get
        Set(ByVal Value As Integer)
            _diasVto = Value
        End Set
    End Property


    Public Property MensajesForo As List(Of MensajeForo)

#End Region

    Public Overrides Sub Dispose()

    End Sub
    'Comente esto porque daba error de propiedades repetidas - Marcelo
    'Private _isFull As Boolean
    'Private _isLoaded As Boolean
    'Public Overrides ReadOnly Property IsFull() As Boolean
    '    Get
    '        Return _isFull
    '    End Get
    'End Property
    'Public Overrides ReadOnly Property IsLoaded() As Boolean
    '    Get
    '        Return _isLoaded
    '    End Get
    'End Property
    'Public Overrides Sub FullLoad()
    'End Sub
    'Public Overrides Sub Load()
    'End Sub
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
End Class
