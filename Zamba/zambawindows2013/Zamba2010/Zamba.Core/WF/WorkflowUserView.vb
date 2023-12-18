﻿Public Class EntityView
    Inherits ZambaCore

    Private mChildCount As Int64
    Private mSubEntity As System.Collections.Generic.List(Of EntityView)

    Public Sub New(ByVal pID As Int64, ByVal pName As String, ByVal pChildCount As Int64)
        ID = pID
        Name = pName
        ChildCount = pChildCount
    End Sub

    Public Property ChildsEntities() As System.Collections.Generic.List(Of EntityView)
        Get
            Return mSubEntity
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of EntityView))
            mSubEntity = value
            'mChildCount = mSubEntity.Count
        End Set
    End Property


    Public Property ChildCount() As Int64
        Get
            Return mChildCount
        End Get
        Set(ByVal value As Int64)
            mChildCount = value
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

    Public ReadOnly Property Nombre() As String
        Get
            Return Name
        End Get
    End Property

End Class