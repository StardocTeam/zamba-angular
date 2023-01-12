Imports System.Collections.Generic

''' <summary>
''' Representa un objeto de tipo Caso de uso
''' </summary>
''' <remarks></remarks>
Public Class UseCase
    Implements ICore

#Region "Attributes and Properties"
    Private _id As Int64
    Private _name As String
    Private _rule As IRule
    Private _description As String
    Private _actors As List(Of IActor)
    Private _preconditions As List(Of UseCaseItems)
    Private _mainFlow As List(Of UseCaseItems)
    Private _alternativeFlow As List(Of UseCaseItems)
    Private _postConditions As List(Of UseCaseItems)

    ''' <summary>
    ''' Id del caso de uso
    ''' </summary>
    ''' <value></value>
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
    ''' Nombre del caso de uso
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
    ''' Regla principal que contiene el caso de uso donde el actor realizará la acción
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Rule() As IRule
        Get
            Return _rule
        End Get
        Set(ByVal value As IRule)
            _rule = value
        End Set
    End Property

    ''' <summary>
    ''' Descripción del caso de uso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    ''' <summary>
    ''' Actores relacionados al caso de uso. Estos pueden ser usuarios o grupos
    ''' </summary>
    ''' <value>Objeto que implementa IActor. Actualmente es User y UserGroup.</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Actors() As List(Of IActor)
        Get
            Return _actors
        End Get
        Set(ByVal value As List(Of IActor))
            _actors = value
        End Set
    End Property

    ''' <summary>
    ''' Precondiciones necesarias para iniciar con el caso de uso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Preconditions() As List(Of UseCaseItems)
        Get
            Return _preconditions
        End Get
        Set(ByVal value As List(Of UseCaseItems))
            _preconditions = value
        End Set
    End Property

    ''' <summary>
    ''' Flujo de eventos normal y principal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Son los pasos del caso de uso comunes o principales</remarks>
    Public Property MainFlow() As List(Of UseCaseItems)
        Get
            Return _mainFlow
        End Get
        Set(ByVal value As List(Of UseCaseItems))
            _mainFlow = value
        End Set
    End Property

    ''' <summary>
    ''' Flujo de eventos alternativos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Son los pasos del caso de uso alternativos</remarks>
    Public Property AlternativeFlow() As List(Of UseCaseItems)
        Get
            Return _alternativeFlow
        End Get
        Set(ByVal value As List(Of UseCaseItems))
            _alternativeFlow = value
        End Set
    End Property

    ''' <summary>
    ''' Resultado/s del caso de uso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostConditions() As List(Of UseCaseItems)
        Get
            Return _postConditions
        End Get
        Set(ByVal value As List(Of UseCaseItems))
            _postConditions = value
        End Set
    End Property
#End Region

#Region "Constructors"
    ''' <summary>
    ''' Genera un objeto de tipo Caso de uso
    ''' </summary>
    ''' <param name="id">Id del caso de uso</param>
    ''' <param name="name">Nombre del caso de uso</param>
    ''' <param name="rule">Regla principal que contiene el caso de uso donde el actor realizará la acción</param>
    ''' <param name="description">Descripción del caso de uso</param>
    ''' <param name="actors">Actores relacionados al caso de uso. Estos pueden ser usuarios o grupos</param>
    ''' <param name="preConditions">Precondiciones necesarias para iniciar con el caso de uso</param>
    ''' <param name="mainFlow">Flujo de eventos normal y principal</param>
    ''' <param name="alternativeFlow">Flujo de eventos alternativos</param>
    ''' <param name="postConditions">Resultado/s del caso de uso</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal id As Int64, _
                   ByVal name As String, _
                   Optional ByVal rule As IRule = Nothing, _
                   Optional ByVal description As String = "", _
                   Optional ByVal actors As List(Of IActor) = Nothing, _
                   Optional ByVal preConditions As List(Of UseCaseItems) = Nothing, _
                   Optional ByVal mainFlow As List(Of UseCaseItems) = Nothing, _
                   Optional ByVal alternativeFlow As List(Of UseCaseItems) = Nothing, _
                   Optional ByVal postConditions As List(Of UseCaseItems) = Nothing)
        _id = id
        _name = name
        _rule = rule
        _description = description
        _actors = actors
        _preconditions = preConditions
        _mainFlow = mainFlow
        _alternativeFlow = alternativeFlow
        _postConditions = postConditions
    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                Dim i As Int32

                If _actors IsNot Nothing Then
                    For i = 0 To _actors.Count - 1
                        If _actors(i) IsNot Nothing Then _actors(i).Dispose()
                    Next
                    _actors.Clear()
                End If

                If _preconditions IsNot Nothing Then
                    For i = 0 To _actors.Count - 1
                        If _preconditions(i) IsNot Nothing Then _preconditions(i).Dispose()
                    Next
                    _preconditions.Clear()
                End If

                If _mainFlow IsNot Nothing Then
                    For i = 0 To _actors.Count - 1
                        If _mainFlow(i) IsNot Nothing Then _mainFlow(i).Dispose()
                    Next
                    _mainFlow.Clear()
                End If

                If _alternativeFlow IsNot Nothing Then
                    For i = 0 To _actors.Count - 1
                        If _alternativeFlow(i) IsNot Nothing Then _alternativeFlow(i).Dispose()
                    Next
                    _alternativeFlow.Clear()
                End If

                If _postConditions IsNot Nothing Then
                    For i = 0 To _actors.Count - 1
                        If _postConditions(i) IsNot Nothing Then _postConditions(i).Dispose()
                    Next
                    _postConditions.Clear()
                End If

                i = Nothing
            End If

            _name = Nothing
            _rule = Nothing
            _description = Nothing
            _actors = Nothing
            _preconditions = Nothing
            _mainFlow = Nothing
            _alternativeFlow = Nothing
            _postConditions = Nothing
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
