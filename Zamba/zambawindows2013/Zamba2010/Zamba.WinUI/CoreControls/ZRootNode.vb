Public Class ZRootNode
    Inherits ZNode

    Public Sub New(ByVal ZambaCore As IZambaCore)
        MyBase.New(ZambaCore)
        NodeType = ZNode.NodeTypes.RootNode
    End Sub
End Class
