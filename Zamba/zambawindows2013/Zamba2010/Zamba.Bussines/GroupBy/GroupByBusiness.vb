Imports System.Collections.Generic

Public Class GroupByBusiness

    Private _group As GridGroupBy
    Public Property Group() As GridGroupBy
        Get
            Return _group
        End Get
        Set(ByVal value As GridGroupBy)
            _group = value
        End Set
    End Property

    Private _description As String

    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Public Sub New(ByVal group As GridGroupBy, ByVal description As String)
        _group = group
        _description = description
    End Sub

    Public Shared Function GetGroupBy() As GridGroupBy
        Return DirectCast(CInt(UserPreferences.getValue("GridGroupBy", UPSections.UserPreferences, "1")), GridGroupBy)
    End Function

    Public Shared Function GetOptions() As List(Of GroupByBusiness)
        Dim lstGroups As New List(Of GroupByBusiness)
        lstGroups.Add(New GroupByBusiness(GridGroupBy.DontGroupBy, "Nada"))
        lstGroups.Add(New GroupByBusiness(GridGroupBy.DocTypeId, "Entidad"))
        Return lstGroups
    End Function

End Class
