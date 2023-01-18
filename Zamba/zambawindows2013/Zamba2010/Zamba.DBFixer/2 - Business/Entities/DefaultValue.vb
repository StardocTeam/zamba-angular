Imports System.Collections.Generic

Public Class DefaultValue
    Inherits Constraint

#Region "Atributos"
    Private _defaultValue As Object
    Private _objName As String
#End Region

#Region "Propiedades"
    Public Property DefaultValue() As Object
        Get
            Return _defaultValue
        End Get
        Set(ByVal value As Object)
            _defaultValue = value
        End Set
    End Property

    Public Property ObjName() As String
        Get
            Return _objName
        End Get
        Set(ByVal value As String)
            _objName = value
        End Set
    End Property


#End Region

#Region "Constructores"
    Public Sub New(ByVal columnName As String)
        MyBase.New(columnName)
    End Sub
    Public Sub New(ByVal columnName As String, ByVal table As Table)
        MyBase.New(columnName, table)
    End Sub
#End Region
End Class