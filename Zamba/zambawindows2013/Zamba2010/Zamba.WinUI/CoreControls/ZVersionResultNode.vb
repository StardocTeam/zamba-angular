Public Class ZVersionResultNode
    Inherits ZNode

    Public Sub New(ByVal ZambaCore As iZambaCore)
        MyBase.New(ZambaCore)
        NodeType = ZNode.NodeTypes.VersionResultNode
    End Sub
End Class
