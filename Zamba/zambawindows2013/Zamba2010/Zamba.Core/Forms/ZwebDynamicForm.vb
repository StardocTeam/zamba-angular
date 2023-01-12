Imports System.Collections.Generic

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Class	 : Core.Section
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase utilizada para la creación de formularios virtuales 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Tomas]     13/03/09    Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class ZwebDynamicForm
    Inherits ZwebForm

#Region "Atributos"

    Private _indexDescriptions As List(Of IndexDescription)
    Private _sections As List(Of Section)
    Private _conditions As List(Of Condition)

#End Region

#Region "Propiedades"

    Public Property IndexDescriptions() As List(Of IndexDescription)
        Get
            Return _indexDescriptions
        End Get
        Set(ByVal value As List(Of IndexDescription))
            _indexDescriptions = value
        End Set
    End Property



    Public Property Sections() As List(Of Section)
        Get
            Return _sections
        End Get
        Set(ByVal value As List(Of Section))
            _sections = value
        End Set
    End Property



    Public Property Conditions() As List(Of Condition)
        Get
            Return _conditions
        End Get
        Set(ByVal value As List(Of Condition))
            _conditions = value
        End Set
    End Property

#End Region

#Region "Constructores"

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal Id As Int32, ByVal Name As String, ByVal Description As String, ByVal Type As FormTypes, ByVal Objecttypeid As Int32, ByVal path As String, ByVal docTypeId As Int64)
        MyBase.New(Id, Name, Description, Type, Objecttypeid, path, docTypeId, DateTime.MinValue)
    End Sub

    Public Sub New(ByVal Id As Int32, ByVal Name As String, ByVal Description As String, ByVal Type As FormTypes, ByVal path As String, ByVal docTypeId As Int64, ByVal useRuleRights As Boolean)
        MyBase.New(Id, Name, Description, Type, path, docTypeId, useRuleRights, DateTime.MinValue)
    End Sub

    Public Sub New(ByVal Id As Int32, ByVal Name As String, ByVal Type As FormTypes, ByVal docTypeId As Int64, ByVal useRuleRights As Boolean)
        MyBase.New(Id, Name, String.Empty, Type, String.Empty, docTypeId, useRuleRights, DateTime.MinValue)
    End Sub

#End Region

    Public Overrides Sub Dispose()

    End Sub

End Class
