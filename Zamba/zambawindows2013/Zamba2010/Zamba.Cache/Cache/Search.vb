Imports Zamba.Core.Searchs

Namespace Cache
    Public Class Search
        Public Shared LastSearchs As New List(Of LastSearch)
        Public Shared FiltersCache As New Hashtable()
        Public Shared HsFilters As New Hashtable()
        Public Shared HsSearchSections As New Hashtable()
        Public Shared HsSearchEntities As New Hashtable()

        Public Shared Sub ClearAll()
            Try
                LastSearchs.Clear()
                FiltersCache.Clear()
                HsFilters.Clear()
                HsSearchSections.Clear()
                HsSearchEntities.Clear()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

    End Class
End Namespace

