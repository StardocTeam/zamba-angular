Public Class ZLinkresultNode
    Inherits ZNode

    Public Sub New(ByVal ZambaCore As IZambaCore)
        MyBase.New(ZambaCore)
        NodeType = ZNode.NodeTypes.LinkResultNode
    End Sub
End Class
