Imports ZAMBA.Core
Imports ZAMBA.AppBlock
Imports Zamba.ListControls

Public MustInherit Class ZXNode
    Inherits TreeListNode

    Private Sub New()
        MyBase.New()
    End Sub
    Public ZambaCore As ZambaCore
    Public NodeType As NodeTypes
    Public Sub New(ByVal ZambaCore As ZambaCore)
        Me.New()
            Me.ZambaCore = ZambaCore
            Me.Text = ZambaCore.Name
            Me.Tag = ZambaCore.Id
            Me.ImageIndex = ZambaCore.IconId
            Me.StateImageIndex = ZambaCore.IconId
    End Sub
    Public ReadOnly Property ObjectTypeId() As Int32
        Get
            Return ZambaCore.ObjecttypeId
        End Get
    End Property

    Enum NodeTypes
        BachNode
        BachStateNode
        DocTypeNode
        ExportResultNode
        GroupNode
        InsertResultNode
        LinkResultNode
        LinkRootNode
        LinkNode
        RootNode
        RuleNode
        RuleTypeNode
        RuleResultNode
        SearchNode
        SearchResultNode
        SearchRootNode
        RootInicio
        WFNode
        StepNode
        TaskResultNode
        VersionResultNode
        ForoNode
        NewResultNode
    End Enum

End Class
