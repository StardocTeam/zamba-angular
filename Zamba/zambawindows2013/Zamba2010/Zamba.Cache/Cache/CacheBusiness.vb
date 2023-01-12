Namespace Cache
    Public Class CacheBusiness
        Public Shared Sub ClearAllCache()

            Cache.Workflows.HSRules.ClearAll()
            Cache.Workflows.clearAll()
            Cache.DocTypesAndIndexs.clearAll()
            Cache.UsersAndGroups.ClearAll()
            Cache.WebServices.ClearAll()
            Cache.Outlook.clearAll()
            Cache.Results.clearAll()
            Cache.Search.ClearAll()
            Cache.Volumes.ClearAll()

        End Sub
    End Class
End Namespace
