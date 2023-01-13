Imports System.Collections.Generic

Public Class SectionDiagram
    Inherits ZambaCore
    Implements ISectionDiagram

#Region " Atributos "
    Private _ChildSection As List(Of ISectionDiagram) = New List(Of ISectionDiagram)
    Private _EntitiesSection As List(Of IEntityDiagram) = New List(Of IEntityDiagram)
    Private _IconID As Int32
    Private _ObjectTypeId As Int32
    Private _ParentSectionId As Int64
    Private _isFull As Boolean
    Private _isLoaded As Boolean
#End Region


#Region " Propiedades "
    Public Property EntitiesSection As List(Of IEntityDiagram) Implements ISectionDiagram.EntitiesSection
        Get
            Return _EntitiesSection
        End Get
        Set(value As List(Of IEntityDiagram))
            _EntitiesSection = value
        End Set
    End Property
    Public Property ChildSection As List(Of ISectionDiagram) Implements ISectionDiagram.ChildSection
        Get
            Return _ChildSection
        End Get
        Set(value As List(Of ISectionDiagram))
            _ChildSection = value
        End Set
    End Property



    Public Property IconId As Integer Implements ISectionDiagram.IconId
        Get
            Return _IconID
        End Get
        Set(value As Integer)
            _IconID = value
        End Set
    End Property

    Public Property ObjectTypeId As Integer Implements ISectionDiagram.ObjectTypeId
        Get
            Return _ObjectTypeId
        End Get
        Set(value As Integer)
            _ObjectTypeId = value
        End Set
    End Property

    Public Property ParentSectionId As Integer Implements ISectionDiagram.ParentSectionId
        Get
            Return _ParentSectionId
        End Get
        Set(value As Integer)
            _ParentSectionId = value
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
#End Region

#Region " Constructores "
    Public Sub New()
    End Sub
#End Region


    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
End Class
