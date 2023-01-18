Imports ZAMBA.Core
Imports ZAMBA.appblock
Public Class ZVSearchResultNode
    Inherits ZVNode

    Public Sub New(ByVal ZambaCore As ZambaCore)
        MyBase.New(ZambaCore)
        Me.NodeType = ZVNode.NodeTypes.SearchResultNode
    End Sub
End Class
