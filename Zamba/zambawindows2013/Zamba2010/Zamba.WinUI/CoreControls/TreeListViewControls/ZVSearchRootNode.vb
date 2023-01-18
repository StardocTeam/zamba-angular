Imports ZAMBA.Core
Imports ZAMBA.appblock
Public Class ZVSearchRootNode
    Inherits ZVNode

    Public Sub New(ByVal ZambaCore As ZambaCore)
        MyBase.New(ZambaCore)
        Me.NodeType = ZVNode.NodeTypes.SearchRootNode
    End Sub
End Class
