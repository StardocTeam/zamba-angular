Imports ZAMBA.Core
Imports ZAMBA.appblock
Public Class ZVRootNode
    Inherits ZVNode

    Public Sub New(ByVal ZambaCore As ZambaCore)
        MyBase.New(ZambaCore)
        Me.NodeType = ZVNode.NodeTypes.RootNode
    End Sub
End Class
