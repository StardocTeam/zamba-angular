Imports ZAMBA.Core
Imports ZAMBA.appblock
Public Class ZXSearchRootNode
    Inherits ZXNode

    Public Sub New(ByVal ZambaCore As ZambaCore)
        MyBase.New(ZambaCore)
        Me.NodeType = ZXNode.NodeTypes.SearchRootNode
    End Sub
End Class
