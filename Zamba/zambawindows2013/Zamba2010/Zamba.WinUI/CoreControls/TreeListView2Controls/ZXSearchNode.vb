Imports ZAMBA.Core
Imports ZAMBA.appblock
Public Class ZXSearchNode
    Inherits ZXNode

    Public Sub New(ByVal ZambaCore As ZambaCore)
        MyBase.New(ZambaCore)
        Me.NodeType = ZXNode.NodeTypes.SearchNode
    End Sub
End Class
