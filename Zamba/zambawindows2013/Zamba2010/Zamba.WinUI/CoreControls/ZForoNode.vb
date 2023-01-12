Public Class ZForoNode
    Inherits ZNode

    Public Sub New(ByVal ZambaCore As IZambaCore)
        MyBase.New(ZambaCore)
        NodeType = ZNode.NodeTypes.ForoNode
    End Sub

End Class
