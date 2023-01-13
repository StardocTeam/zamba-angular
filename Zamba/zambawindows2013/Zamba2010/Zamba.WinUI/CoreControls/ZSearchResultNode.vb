Public Class ZSearchResultNode
    Inherits ZNode

    Public Sub New(ByVal ZambaCore As iZambaCore)
        MyBase.New(ZambaCore)
        NodeType = ZNode.NodeTypes.SearchResultNode
    End Sub
End Class
