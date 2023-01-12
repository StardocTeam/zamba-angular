Public MustInherit Class ZNode
    Inherits ZTreenode

    Private Sub New()
        MyBase.New()
    End Sub
    Public ZambaCore As iZambaCore
    Public NodeType As NodeTypes
    Public Sub New(ByVal ZambaCore As iZambaCore)
        Me.New()
        Me.ZambaCore = ZambaCore
        Text = ZambaCore.Name
        Tag = ZambaCore.ID
        ImageIndex = ZambaCore.IconId
        SelectedImageIndex = ZambaCore.IconId
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
