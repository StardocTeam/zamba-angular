Imports ZAMBA.Core
Imports ZAMBA.appblock
Public Class ZVSearchNode
    Inherits ZVNode

    Public Sub New(ByVal ZambaCore As ZambaCore)
        MyBase.New(ZambaCore)
        Me.NodeType = ZVNode.NodeTypes.SearchNode
    End Sub
End Class
