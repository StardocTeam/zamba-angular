Imports System.Collections.Generic
Imports Zamba.Servers

Public Class View

#Region "Atributos"
    Private _name As String
    Private _queries As Dictionary(Of Server.DBTYPES, String)
    Private _text As String
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
    Public Property Text() As String
        Get
            Return _text
        End Get
        Set(ByVal value As String)
            _text = value
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New(ByVal name As String)
        _name = name
        _queries = New Dictionary(Of Server.DBTYPES, String)
    End Sub
#End Region

    Public Function GetQuery(ByVal databaseType As Server.DBTYPES) As String
        Dim Query As String = String.Empty

        If _queries.ContainsKey(databaseType) Then

        Else
            Query = String.Empty
        End If

        Return Query
    End Function

    Public Sub SetQuery(ByVal databaseType As Server.DBTYPES, ByVal query As String)
        If _queries.ContainsKey(databaseType) Then

        Else
            _queries.Add(databaseType, query)
        End If
    End Sub
End Class
