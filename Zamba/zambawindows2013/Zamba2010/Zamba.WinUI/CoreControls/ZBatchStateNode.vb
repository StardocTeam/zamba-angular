Public Class ZBatchStateNode
    Inherits ZNode

    Public Sub New(ByVal ZambaCore As iZambaCore)
        MyBase.New(ZambaCore)
        NodeType = ZNode.NodeTypes.BachStateNode
    End Sub
End Class
