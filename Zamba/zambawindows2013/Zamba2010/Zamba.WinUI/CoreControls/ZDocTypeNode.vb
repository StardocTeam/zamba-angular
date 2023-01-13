Public Class ZDocTypeNode
    Inherits ZNode

    Public Sub New(ByVal ZambaCore As iZambaCore)
        MyBase.New(ZambaCore)
        NodeType = ZNode.NodeTypes.DocTypeNode
    End Sub
End Class
