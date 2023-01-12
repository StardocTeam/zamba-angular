Public Class ZExportResultNode
    Inherits ZNode

    Public Sub New(ByVal ZambaCore As IZambaCore)
        MyBase.New(ZambaCore)
        NodeType = ZNode.NodeTypes.ExportResultNode
    End Sub
End Class
