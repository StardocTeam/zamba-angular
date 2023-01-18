Imports ZAMBA.Core
Imports ZAMBA.appblock
Public Class ZXRootNode
    Inherits ZXNode

    Public Sub New(ByVal ZambaCore As ZambaCore)
        MyBase.New(ZambaCore)
        Me.NodeType = ZXNode.NodeTypes.RootNode
    End Sub
End Class
