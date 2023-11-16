﻿Imports System
Imports System.Reflection
Imports Zamba.Core
Imports System.Collections.Generic
Imports Zamba.WFExecution
Imports System.Xml.Serialization


''' <summary>
''' Toma el valor de una variable en zvar y carga sus valores en otras variables de acuerdo a la clase seleccionada del assembly
''' </summary>
''' <remarks></remarks>
<RuleCategory("Variables de Zamba"), RuleDescription("Permite obtener los valores de un objeto para almacenarlo en variables o indices"), RuleApproved("True"), RuleHelp(""), RuleFeatures(True)> <Serializable()> _
Public Class DoDismemberObject_v2
    Inherits WFRuleParent
    Implements IDoDismemberObject_v2

#Region "Atributos"
    Private _zvars As List(Of IDoDismemberObject_v2.IZvarVariable)
    Private _assemblyPath As String = String.Empty
    Private _objectName As String = String.Empty
    Private playRule As PlayDoDismemberObject_v2
#End Region

    Public Sub AddZvar(ByVal className As String, ByVal propertyName As String, ByVal zvarValue As String) Implements IDoDismemberObject_v2.AddZvar
        ParentClasses.Add(New ZvarVariable(className, propertyName, zvarValue))
    End Sub

    Public Property ObjectName() As String Implements Core.IDoDismemberObject_v2.ObjectName
        Get
            Return _objectName
        End Get
        Set(ByVal value As String)
            _objectName = value
        End Set
    End Property
    Public ReadOnly Property ParentClasses() As List(Of IDoDismemberObject_v2.IZvarVariable) Implements IDoDismemberObject_v2.Zvars
        Get
            Return _zvars
        End Get
    End Property
    Public Property AssemblyPath() As String Implements IDoDismemberObject_v2.AssemblyPath
        Get
            Return _assemblyPath
        End Get
        Set(ByVal value As String)
            _assemblyPath = value
        End Set
    End Property

    ''' <summary>
    ''' Get or Set del listado de padres con
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RawZvars() As String Implements IDoDismemberObject_v2.RawZvars
        Get
            Return ParseZvars(_zvars)
        End Get
        Set(ByVal value As String)
            _zvars = ParseZvar(value)
        End Set
    End Property



    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get

        End Get
    End Property

    Public Overrides Sub Load()

    End Sub

    Public Overrides ReadOnly Property MaskName() As String
        Get
            Return "Desmembrar Variable"
        End Get
    End Property

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overrides Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Sub New(ByVal id As Int64, ByVal name As String, ByVal stepId As Int64, ByVal assemblyPath As String, ByVal rawZvars As String, ByVal objectName As String)
        MyBase.New(id, name, stepId)
        _assemblyPath = assemblyPath
        _zvars = ParseZvar(rawZvars)
        _objectName = objectName
        Me.playRule = New PlayDoDismemberObject_v2(Me)
    End Sub


    Public Function ParseZvar(ByVal rawValue As String) As List(Of IDoDismemberObject_v2.IZvarVariable) Implements IDoDismemberObject_v2.ParseZvars
        Return ZvarVariable.ParseZvar(rawValue)
    End Function
    Public Function ParseZvars(ByVal rawValue As List(Of IDoDismemberObject_v2.IZvarVariable)) As String Implements IDoDismemberObject_v2.ParseZvar
        Return ZvarVariable.ParseZvars(rawValue)
    End Function


    Public Class ZvarVariable
        Implements IDoDismemberObject_v2.IZvarVariable

        Private _className As String
        Private _zvarName As String
        Private _propertyName As String

        Public Property ClassName() As String Implements IDoDismemberObject_v2.IZvarVariable.ClassName
            Get
                Return _className
            End Get
            Set(ByVal value As String)
                _className = value
            End Set
        End Property
        Public Property PropertyName() As String Implements IDoDismemberObject_v2.IZvarVariable.PropertyName
            Get
                Return _propertyName
            End Get
            Set(ByVal value As String)
                _propertyName = value
            End Set
        End Property
        Public Property ZvarName() As String Implements IDoDismemberObject_v2.IZvarVariable.ZvarName
            Get
                Return _zvarName
            End Get
            Set(ByVal value As String)
                _zvarName = value
            End Set
        End Property

        Public Sub New(ByVal className As String)
            _className = className
        End Sub

        Public Sub New(ByVal className As String, ByVal propertyName As String)
            _className = className
            _propertyName = propertyName
        End Sub
        Public Sub New(ByVal className As String, ByVal propertyName As String, ByVal zvarName As String)
            _className = className
            _propertyName = propertyName
            _zvarName = zvarName
        End Sub

        ''' <summary>
        ''' Deserializo un string a una lista de padres
        ''' </summary>
        ''' <param name="rawValue"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function ParseZvar(ByVal rawValue As String) As List(Of IDoDismemberObject_v2.IZvarVariable)

            If (String.IsNullOrEmpty(rawValue)) Then
                Return New List(Of IDoDismemberObject_v2.IZvarVariable)()
            End If

            Dim classes As New List(Of IDoDismemberObject_v2.IZvarVariable)

            For Each RawParentClass As String In rawValue.Split("]")
                If (RawParentClass.Trim.Length > 0) Then
                    classes.Add(ParseParent(RawParentClass))
                End If
            Next
            Return classes
        End Function
        ''' <summary>
        ''' Serializo una lista de padres a string 
        ''' </summary>
        ''' <param name="rawValue"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function ParseZvars(ByVal rawValue As List(Of IDoDismemberObject_v2.IZvarVariable)) As String
            Dim Builder As New Text.StringBuilder()

            For Each Parent As ZvarVariable In rawValue
                Builder.Append(ParseParent(Parent.ClassName, Parent.PropertyName, Parent.ZvarName))
            Next

            Return Builder.ToString()
        End Function

        Private Shared Function ParseParent(ByVal parentName As String, ByVal propertyName As String, ByVal value As String) As String
            'Armo la estructura [ClasePadre|Propiedad:valor]
            Return "[" + parentName + "|" + propertyName + ":" + value + "]"
        End Function

        Private Shared Function ParseParent(ByVal rawParent As String) As IDoDismemberObject_v2.IZvarVariable
            Dim ParentName As String = String.Empty
            Dim PropertyName As String = String.Empty
            Dim Value As String = String.Empty
            Try

                'La estructura es [ClasePadre|Propiedad:valor]

                rawParent = rawParent.Replace("[", String.Empty)
                rawParent = rawParent.Replace("]", String.Empty)

                ParentName = rawParent.Split("|")(0)
                Value = rawParent.Split(":")(1)
                PropertyName = rawParent.Split("|")(1).Split(":")(0)
            Catch ex As Exception

            End Try

            Return New ZvarVariable(ParentName, PropertyName, Value)
        End Function
    End Class


End Class