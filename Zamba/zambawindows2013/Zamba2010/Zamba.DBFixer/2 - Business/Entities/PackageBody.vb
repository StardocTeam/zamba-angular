Imports System.Collections.Generic
Imports Zamba.Servers

Public Class PackageBody

#Region "Atributos"
    Private _name As String
    Private _text As String = String.Empty
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

#Region "Constructores"
    Public Sub New(ByVal spName As String, Optional ByVal spText As String = "")
        _name = spName
        _text = spText
    End Sub
#End Region

End Class
