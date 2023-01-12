Imports System.Collections.Generic

Public Class DataBase

#Region "Atributos"
    Private _name As String
    Private _views As List(Of View)
    Private _storedProcedures As List(Of StoredProcedure)
    Private _tables As List(Of Table)
#End Region

#Region "Propiedades"
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Property Views() As List(Of View)
        Get
            Return _views
        End Get
        Set(ByVal value As List(Of View))
            _views = value
        End Set
    End Property

    Public Property StoredProcedures() As List(Of StoredProcedure)
        Get
            Return _storedProcedures
        End Get
        Set(ByVal value As List(Of StoredProcedure))
            _storedProcedures = value
        End Set
    End Property

    Public Property Tables() As List(Of Table)
        Get
            Return _tables
        End Get
        Set(ByVal value As List(Of Table))
            _tables = value
        End Set
    End Property
#End Region

#Region "Constructores"
    Public Sub New(ByVal name As String)
        _name = name
        _views = New List(Of View)
        _storedProcedures = New List(Of StoredProcedure)
        _tables = New List(Of Table)
    End Sub
    Public Sub New(ByVal name As String, ByVal views As List(Of View), ByVal storedProcedures As List(Of StoredProcedure), ByVal tables As List(Of Table))
        _name = name
        _views = views
        _storedProcedures = storedProcedures
        _tables = tables
    End Sub
#End Region

End Class