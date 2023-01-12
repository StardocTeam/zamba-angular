Public Class ZNewResultNode
    Inherits ZNode

    Public Sub New(ByVal ZambaCore As iZambaCore)
        MyBase.New(ZambaCore)
        ImageIndex = ZambaCore.IconId
        NodeType = ZNode.NodeTypes.NewResultNode
    End Sub

End Class
