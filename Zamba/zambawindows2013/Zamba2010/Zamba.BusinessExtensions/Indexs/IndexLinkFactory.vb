Imports Zamba.Data

Public Class IndexLinkBusiness
    Public Shared Function GetindexsLinks() As ArrayList
        Return IndexLinkFactory.GetindexsLinks()
    End Function
    Public Shared Function GetLinks(ByVal IndexLink As IndexLink) As ArrayList
        Return IndexLinkFactory.GetLinks(IndexLink)
    End Function
    Public Shared Function GetlinkedIndexs() As DataSet
        Return IndexLinkFactory.GetlinkedIndexs()
    End Function
    Public Shared Sub UpdateInfo(ByVal ili As IndexLinkInfo)
        IndexLinkFactory.UpdateInfo(ili)
    End Sub
    Public Shared Sub SaveNewLink(ByVal ii As IndexLink)
        IndexLinkFactory.SaveNewLink(ii)
    End Sub
    Public Shared Sub Delete(ByVal il As IndexLink)
        IndexLinkFactory.Delete(il)
    End Sub
    Public Shared Sub Update(ByVal il As IndexLink)
        IndexLinkFactory.Update(il)
    End Sub
    Public Shared Sub DeleteindexLinkInfo(ByVal ili As IndexLinkInfo)
        IndexLinkFactory.DeleteindexLinkInfo(ili)
    End Sub
    Public Shared Sub linkindexs(ByVal DocTypeId1 As Int32, ByVal DocTypeId2 As Int32, ByVal Index1 As Index, ByVal Index2 As Index, ByVal CheckAll As Boolean)
        IndexLinkFactory.linkindexs(DocTypeId1, DocTypeId2, Index1, Index2, CheckAll)
    End Sub
    Public Shared Sub DeleteLinkedIndexs(ByVal DocTypeName1 As String, ByVal DocTypeName2 As String, ByVal IndexName1 As String, ByVal IndexName2 As String)
        IndexLinkFactory.DeleteLinkedIndexs(DocTypeName1, DocTypeName2, IndexName1, IndexName2)
    End Sub
    Public Shared Sub SaveLinkInfo(ByVal linfo As IndexLinkInfo)
        IndexLinkFactory.SaveLinkInfo(linfo)
    End Sub
    Public Shared Sub AddLinkInfo(ByVal indexlink As IndexLink, ByVal linfo As IndexLinkInfo)
        IndexLinkFactory.AddLinkInfo(indexlink, linfo)
    End Sub
End Class
