Imports System.Collections.Generic
Imports Zamba.Core.Analysis

Namespace Analysis
    ''' <summary>
    ''' Representa el analisis de una etapa con sus hijos y padre.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WfStep
        Implements IWfStep(Of WfStep)

#Region "Atributos"
        ''' <summary>
        '''  Las etapas hijas de esta etapa.
        ''' </summary>
        ''' <remarks></remarks>
        Private _childs As List(Of IWFStep) = Nothing
        ''' <summary>
        ''' La etapa padre 
        ''' </summary>
        ''' <remarks></remarks>
        Private _parent As IWFStep = Nothing
        ''' <summary>
        ''' Representa el porcentaje de tareas que tiene esta etapa en relacion al conjunto de tareas seleccionados.
        ''' </summary>
        ''' <remarks></remarks>
        Private _percent As Double
        ''' <summary>
        ''' Representa la cantidad de tareas en esta etapa
        ''' </summary>
        Private _taskCount As Int64
        ''' <summary>
        ''' To detect redundant calls
        ''' </summary>
        ''' <remarks></remarks>
        Private _disposedValue As Boolean = False
#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Las etapas hijas de esta etapa.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Childs() As List(Of IWfStep) Implements IWfStep(Of WfStep).Childs
            Get
                Return _childs
            End Get
        End Property
        ''' <summary>
        ''' Representa el porcentaje de tareas que tiene esta etapa en relacion al conjunto de tareas seleccionados.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property TasksPercent() As Double Implements IWfStep(Of WfStep).TasksPercent
            Get
                Return _percent
            End Get
            Set(ByVal value As Double)
                _percent = value
            End Set
        End Property
        ''' <summary>
        ''' La etapa padre 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Parent() As IWfStep Implements IWfStep(Of WfStep).Parent
            Get
                Return _parent
            End Get
        End Property
        ''' <summary>
        ''' Valida si esta etapa tiene etapas hijas
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property HasChilds() As Boolean Implements IHistory(Of IWfStep).HasChilds
            Get
                Dim HasChildItems As Boolean = True

                If IsNothing(_childs) OrElse _childs.Count > 0 Then
                    HasChildItems = False
                End If

                Return HasChildItems
            End Get
        End Property
        ''' <summary>
        ''' Valida si esta etapa tiene una etapa padre.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property HasParent() As Boolean Implements IHistory(Of IWfStep).HasParent
            Get
                Dim HasParentItem As Boolean = True

                If IsNothing(_childs) Then
                    HasParentItem = False
                End If

                Return HasParentItem
            End Get
        End Property

        ''' <summary>
        ''' Representa el tiempo promedio que tardan las tareas en pasar del la etapa padre a esta.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property TasksTimeSpent() As TimeSpan Implements IWfStep(Of WfStep).TasksTimeSpent
            Get

            End Get
            Set(ByVal value As System.TimeSpan)

            End Set
        End Property

        ''' <summary>
        ''' Representa la cantidad de tareas en esta etapa
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property TasksCount() As Long Implements IWfStep(Of WfStep).TasksCount
            Get
                Return _taskCount
            End Get
            Set(ByVal value As Long)
                _taskCount = value
            End Set
        End Property
#End Region

#Region "Constructores"
        ''' <summary>
        ''' Instancia un StepAnalisis
        ''' </summary>
        ''' <param name="taskCount">Cantidad de tareas que tiene esta etapa</param>
        ''' <param name="childs">Las etapas hijas de esta etapa. Si no tiene , pasarle un null/nothing </param>
        ''' <param name="parent">La etapa padre. Si no tiene , pasarle un null/nothing</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal taskCount As Int64, ByVal childs As List(Of IWFStep), ByVal parent As IWFStep)
            _taskCount = taskCount

            If IsNothing(childs) Then
                _childs = New List(Of IWFStep)
            Else
                _childs = childs
            End If

            _parent = parent
        End Sub
#End Region

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me._disposedValue Then
                If disposing Then

                    If Not IsNothing(_childs) Then
                        _childs.Clear()
                        _childs = Nothing
                    End If

                    If Not IsNothing(_parent) Then
                        _parent.Dispose()
                        _parent = Nothing
                    End If
                End If

            End If
            Me._disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub


    End Class
End Namespace