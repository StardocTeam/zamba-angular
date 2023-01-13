Imports ZAMBA.Core
Imports ZAMBA.appblock
Public Class ZXSearchResultNode
    Inherits ZXNode

    Public Sub New(ByVal ZambaCore As ZambaCore)
        MyBase.New(ZambaCore)
        Me.NodeType = ZXNode.NodeTypes.SearchResultNode
    End Sub
End Class
