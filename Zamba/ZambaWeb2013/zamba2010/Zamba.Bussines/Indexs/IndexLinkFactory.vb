Imports Zamba.Servers
Imports Zamba.Core
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

End Class
