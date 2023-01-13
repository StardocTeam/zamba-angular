Imports System.Collections.Generic
Imports Zamba.Framework
Imports Zamba.Searchs

Namespace Searchs
    Public Class Search
        Implements ISearch
        Public Property Doctypes() As List(Of IDocType) Implements ISearch.Doctypes
        Public ReadOnly Property RaiseResults As Boolean
        Public Property Indexs() As List(Of IIndex) Implements ISearch.Indexs

        Public Property ParentName() As String Implements ISearch.ParentName

        Public Property SQL As List(Of String) Implements ISearch.SQL

        Public Property SQLCount As List(Of String) Implements ISearch.SQLCount


        Public ReadOnly Property OrderBy() As String
        Public Property Textsearch() As String Implements ISearch.Textsearch
        Public Property CaseSensitive As Boolean Implements ISearch.CaseSensitive
        Public Property UseVersion As Boolean Implements ISearch.UseVersion
        Public Property ShowIndexOnGrid As Boolean Implements ISearch.ShowIndexOnGrid
        Public Property MaxResults As Integer Implements ISearch.MaxResults
        Public Property UserId As Long Implements ISearch.UserId
        Public Property WorkflowId As Long Implements ISearch.WorkflowId
        Public Property StepId As Long Implements ISearch.StepId
        Public Property StepStateId As Long Implements ISearch.StepStateId
        Public Property TaskStateId As Long Implements ISearch.TaskStateId
        Public Property Name As String Implements ISearch.Name
        Public Property _GroupBy As String
        Public Property SearchType As SearchTypes Implements ISearch.SearchType
        Public Property EntitiesEnabledForQuickSearch As List(Of IEntityEnabledForQuickSearch) Implements ISearch.EntitiesEnabledForQuickSearch

        Private Property ISearch_OrderBy As String Implements ISearch.OrderBy


        Public Property Filters As List(Of ikendoFilter) Implements ISearch.Filters


        Public Property View As String Implements ISearch.View

        Public Property Restriction As String Implements ISearch.Restriction

        Public Property Lista_ColumnasFiltradas As List(Of String) Implements ISearch.Lista_ColumnasFiltradas


        Public Sub New(ByVal indexs As List(Of IIndex), ByVal _Textsearch As String, ByVal doctypes As List(Of IDocType), ByVal RaiseResults As Boolean, ByVal ParentName As String)
            Me._Indexs = indexs
            Me.Textsearch = _Textsearch
            Me.Doctypes = doctypes
            Me.RaiseResults = RaiseResults
            Me.ParentName = ParentName
            Me.SQL = New List(Of String)
            Me.SQLCount = New List(Of String)
        End Sub

        Public Sub New()

        End Sub

        Public Sub AddIndex(index As IIndex) Implements ISearch.AddIndex
            If Indexs Is Nothing Then Indexs = New List(Of IIndex)
            Indexs.Add(index)
        End Sub

        Public Sub AddDocType(docType As IDocType) Implements ISearch.AddDocType
            If Doctypes Is Nothing Then Doctypes = New List(Of IDocType)
            Doctypes.Add(docType)
        End Sub

        Public Sub SetOrderBy(orderString As String) Implements ISearch.SetOrderBy
            _OrderBy = orderString
        End Sub

        Public Sub SetGroupBy(groupByString As String) Implements ISearch.SetGroupBy
            _GroupBy = groupByString
            '_GroupBy = ""
        End Sub

        Public Sub AddFilter(filter As ikendoFilter) Implements ISearch.AddFilter
        End Sub
    End Class

    Public Class LastSearch
        Implements IDisposable, ILastSearch

        Public Property Id() As Int64 Implements ILastSearch.Id
        Public Property Name() As String Implements ILastSearch.Name
        Public Property SerializedSearch() As String Implements ILastSearch.SerializedSearch


        Public Sub New(id As Int64, name As String)
            _Id = id
            _Name = name
            _SerializedSearch = String.Empty
        End Sub

        Public Sub New(id As Int64, name As String, SerializedSearch As String)
            _Id = id
            _Name = name
            _SerializedSearch = SerializedSearch
        End Sub

        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then

                End If
                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.

            End If
            disposedValue = True
        End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

    End Class

    Public Class EntityEnabledForQuickSearch
        Implements IEntityEnabledForQuickSearch

        Public Property EntityId As Int64 Implements IEntityEnabledForQuickSearch.EntityId
        Public Property IndexsIds As List(Of Int64) Implements IEntityEnabledForQuickSearch.IndexsIds
    End Class
End Namespace