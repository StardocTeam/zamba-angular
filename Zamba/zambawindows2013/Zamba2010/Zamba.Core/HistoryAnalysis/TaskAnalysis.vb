Imports System.Collections.Generic

Namespace Analysis
    Public Class Task
        Implements ITask

#Region "Atributos"
        ''' <summary>
        '''  Las tareas hijas de esta tarea.
        ''' </summary>
        ''' <remarks></remarks>
        Private _childs As List(Of ITaskResult) = Nothing
        ''' <summary>
        ''' La etapa padre 
        ''' </summary>
        ''' <remarks></remarks>
        Private _parent As ITaskResult = Nothing
        ''' <summary>
        ''' To detect redundant calls
        ''' </summary>
        ''' <remarks></remarks>
        Private _disposedValue As Boolean = False
        ''' <summary>
        ''' Etapa en la que se encuentra la tarea
        ''' </summary>
        ''' <remarks></remarks>
        Private _step As IWfStep(Of IWfStep) = Nothing
        ''' <summary>
        ''' Historial de la tarea.
        ''' </summary>
        ''' <remarks></remarks>
        Private _history As List(Of IRuleNode) = Nothing
        ''' <summary>
        ''' Estado de la tarea.
        ''' </summary>
        ''' <remarks></remarks>
        Private _state As String = Nothing
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Las tareas hijas de esta tarea.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Childs() As List(Of ITaskResult) Implements IHistory(Of ITaskResult).Childs
            Get
                Return _childs
            End Get
        End Property
        ''' <summary>
        ''' Valida si esta tarea tiene tareas hijas
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property HasChilds() As Boolean Implements IHistory(Of ITaskResult).HasChilds
            Get
                Dim HasChildItems As Boolean = True

                If IsNothing(_childs) OrElse _childs.Count > 0 Then
                    HasChildItems = False
                End If

                Return HasChildItems
            End Get
        End Property
        ''' <summary>
        ''' Valida si esta tarea tiene una tarea padre.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property HasParent() As Boolean Implements IHistory(Of ITaskResult).HasParent
            Get
                Dim HasParentItem As Boolean = True

                If IsNothing(_childs) Then
                    HasParentItem = False
                End If

                Return HasParentItem
            End Get
        End Property
        ''' <summary>
        ''' La etapa padre 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Parent() As ITaskResult Implements IHistory(Of ITaskResult).Parent
            Get
                Return _parent
            End Get
        End Property
        ''' <summary>
        ''' Etapa en la que se encuentra la tarea
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentStep() As IWfStep(Of IWfStep) Implements ITask.CurrentStep
            Get
                Return _step
            End Get
        End Property
        ''' <summary>
        ''' Historial de la tarea.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property History() As List(Of IRuleNode) Implements ITask.History
            Get
                Return _history
            End Get
        End Property
        ''' <summary>
        ''' Estado de la tarea.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property State() As String Implements ITask.State
            Get
                Return _state
            End Get
        End Property
#End Region

#Region "Constructores"

        ''' <summary>
        ''' Instancia una tarea de analisis de historial
        ''' </summary>
        ''' <param name="childs">Las tareas hijas de esta tarea. Si no tiene , pasarle un null/nothing</param>
        ''' <param name="parent">La tarea padre de esta tarea. Si no tiene , pasarle un null/nothing</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal childs As List(Of ITaskResult), ByVal parent As ITaskResult, ByVal wfStep As IWfStep(Of IWfStep), ByVal state As String, ByVal history As List(Of IRuleNode))
            If IsNothing(childs) Then
                _childs = New List(Of ITaskResult)
            Else
                _childs = childs
            End If

            If IsNothing(history) Then
                _history = New List(Of IRuleNode)
            Else
                _history = history
            End If

            _state = state
            _step = wfStep
            _parent = parent

        End Sub
#End Region

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not _disposedValue Then
                If disposing Then

                    If Not IsNothing(_childs) Then
                        _childs.Clear()
                        _childs = Nothing
                    End If

                    If Not IsNothing(_parent) Then
                        _parent.Dispose()
                        _parent = Nothing
                    End If

                    If Not IsNothing(_step) Then
                        _step.Dispose()
                        _step = Nothing
                    End If

                    If Not IsNothing(_history) Then
                        _history.Clear()
                        _history = Nothing
                    End If

                    _state = Nothing

                End If

            End If
            _disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Public Sub AddHistoryNode(ByVal node As IDoRuleNodeHistory) Implements ITask.AddHistoryNode
            _history.Add(node)
        End Sub

        Public Sub AddHistoryNode(ByVal node As IIfRuleNodeHistory) Implements ITask.AddHistoryNode
            _history.Add(node)
        End Sub
    End Class
End Namespace
