Public Class ZSearchNode
    Inherits ZNode

    Public Sub New(ByVal ZambaCore As iZambaCore)
        MyBase.New(ZambaCore)
        NodeType = ZNode.NodeTypes.SearchNode
    End Sub
End Class
