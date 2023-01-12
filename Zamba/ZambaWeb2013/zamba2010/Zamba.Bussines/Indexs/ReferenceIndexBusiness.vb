Imports System.Collections.Generic
Imports Zamba.Data

Public Class ReferenceIndexBusiness

    Public Function GetReferenceIndexesByDoctypeId(doctypeId As Long) As List(Of ReferenceIndex)

        If Not Cache.DocTypesAndIndexs.hsReferenceIndexsByDoctype.ContainsKey(doctypeId) Then 'si esta en cache
            Dim refIndexs As List(Of ReferenceIndex) = (New ReferenceIndexFactory).GetReferenceIndexesByDoctypeId(doctypeId)
            SyncLock Cache.DocTypesAndIndexs.hsReferenceIndexsByDoctype
                If Not Cache.DocTypesAndIndexs.hsReferenceIndexsByDoctype.ContainsKey(doctypeId) Then 'si esta en cache
                    Cache.DocTypesAndIndexs.hsReferenceIndexsByDoctype.Add(doctypeId, refIndexs)
                    Return refIndexs
                End If
            End SyncLock
        End If

        Return Cache.DocTypesAndIndexs.hsReferenceIndexsByDoctype(doctypeId)

    End Function

End Class
