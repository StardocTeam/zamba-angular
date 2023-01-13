Public Class ZInsertResultNode
    Inherits ZNode

    Public Sub New(ByVal ZambaCore As IZambaCore)
        MyBase.New(ZambaCore)
        NodeType = ZNode.NodeTypes.InsertResultNode
    End Sub
End Class
