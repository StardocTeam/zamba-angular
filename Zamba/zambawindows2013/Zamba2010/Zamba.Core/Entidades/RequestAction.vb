Imports System.Collections.Generic

Public Class RequestAction
    Implements IDisposable

#Region "Atributos"
    Private _id As Nullable(Of Int64)
    Private _requestDate As DateTime
    Private _finishDate As Nullable(Of DateTime)
    Private _isFinished As Boolean
    Private _requestUserId As Int64
    Private _rulesIds As List(Of Int64)
    Private _tasks As List(Of RequestActionTask)
    Private _executedTasks As List(Of RequestActionTask)
    Private _usersIds As List(Of Int64)
    Private _disposedValue As Boolean = False
    Private _serverLocation As String = Nothing
    Private _name As String
#End Region

#Region "Propiedades"
    Public Property RequestActionId() As Nullable(Of Int64)
        Get
            Return _id
        End Get
        Set(ByVal value As Nullable(Of Int64))
            _id = value
        End Set
    End Property
    ''' <summary>
    ''' Fecha que se considera al pedido finalizado , al tener todas las tareas finalizadas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FinishDate() As Nullable(Of DateTime)
        Get
            Return _finishDate
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _finishDate = value
        End Set
    End Property
    ''' <summary>
    ''' Fecha que se realizo el pedido
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RequestDate() As DateTime
        Get
            Return _requestDate
        End Get
        Set(ByVal value As Date)
            _requestDate = value
        End Set
    End Property
    ''' <summary>
    ''' Id de usuario que realizo el pedido
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RequestUserId() As Int64
        Get
            Return _requestUserId
        End Get
        Set(ByVal value As Int64)
            _requestUserId = value
        End Set
    End Property
    ''' <summary>
    ''' Valida si el pedido esta finalizado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsFinished() As Boolean
        Get
            Return _isFinished
        End Get
        Set(ByVal value As Boolean)
            _isFinished = value
        End Set
    End Property
    Public ReadOnly Property RulesIds() As List(Of Int64)
        Get
            Return _rulesIds
        End Get
    End Property
    ''' <summary>
    ''' Ids de las reglas que tiene como opciones el pedido
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Tasks() As List(Of RequestActionTask)
        Get
            Return _tasks
        End Get
    End Property
    ''' <summary>
    ''' Tareas que tiene el pedido.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ExecutedTasks() As List(Of RequestActionTask)
        Get
            Return _executedTasks
        End Get
    End Property
    ''' <summary>
    ''' Lista que contiene los ids de las tareas y etapas en relacion 1 a 1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TasksAndStepIds() As Dictionary(Of Int64, Int64)
        Get
            Dim TaskAndStepIds As New Dictionary(Of Int64, Int64)(_tasks.Count)

            For Each CurrentTask As RequestActionTask In _tasks
                TaskAndStepIds.Add(CurrentTask.TaskId, CurrentTask.StepId)
            Next

            Return TaskAndStepIds
        End Get
    End Property
    ''' <summary>
    ''' Ids de las tareas del pedido.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TaskIds() As List(Of Int64)
        Get
            Dim ReturnValue As List(Of Int64) = Nothing

            If Not IsNothing(_tasks) Then
                ReturnValue = New List(Of Int64)(_tasks.Count)

                For Each CurrentItem As RequestActionTask In _tasks
                    ReturnValue.Add(CurrentItem.TaskId)
                Next
            End If

            Return ReturnValue
        End Get
    End Property
    ''' <summary>
    ''' Path del servidor donde se encuentra la pagina que va a ser redirigido la notificacion.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ServerLocation() As String
        Get
            Return _serverLocation
        End Get
        Set(ByVal value As String)
            _serverLocation = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre de la regla RequestAction
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>   
    '''     [Gaston]    30/07/2008 Created
    ''' </history>
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    ''' <summary>
    ''' Ids de las etapas del pedido
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property StepIds() As List(Of Int64)
        Get
            Dim ReturnValue As List(Of Int64) = Nothing

            If Not IsNothing(_tasks) Then
                ReturnValue = New List(Of Int64)(_tasks.Count)

                For Each CurrentItem As RequestActionTask In _tasks
                    ReturnValue.Add(CurrentItem.StepId)
                Next
            End If

            Return ReturnValue
        End Get
    End Property
    ''' <summary>
    ''' Ids de los usuarios que reciben el pedido.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UsersIds() As List(Of Int64)
        Get
            Return _usersIds
        End Get
    End Property
#End Region

#Region "Constructores"

    Public Sub New()
        _rulesIds = New List(Of Int64)
        _tasks = New List(Of RequestActionTask)
        _executedTasks = New List(Of RequestActionTask)
        _usersIds = New List(Of Int64)
        _isFinished = False
    End Sub
#End Region

#Region "Dispose"
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not _disposedValue Then
            If disposing Then
                _finishDate = Nothing

                If Not IsNothing(_rulesIds) Then
                    _rulesIds.Clear()
                    _rulesIds = Nothing
                End If
                If Not IsNothing(_usersIds) Then
                    _usersIds.Clear()
                    _usersIds = Nothing
                End If
                If Not IsNothing(_tasks) Then
                    _tasks.Clear()
                    _tasks = Nothing
                End If
            End If
        End If

        _disposedValue = True
    End Sub

    Public Sub Dispose() Implements System.IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class