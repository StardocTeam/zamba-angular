Imports ZAMBA.Core
Imports Zamba.ListControls
Imports ZAMBA.appblock

Public MustInherit Class ZVNode
    Inherits ZTreeListViewItem

    Private Sub New()
        MyBase.New()
    End Sub
    Public ZambaCore As ZambaCore
    Public NodeType As NodeTypes
    Public Sub New(ByVal ZambaCore As ZambaCore)
        Me.New()
        Try
            Me.ZambaCore = ZambaCore
            DirectCast(Me, Object).Text = ZambaCore.Name
            Me.Tag = ZambaCore.Id
            Me.ImageIndex = ZambaCore.IconId
            Me.StateImageIndex = ZambaCore.IconId
        Catch ex As Exception
            RaiseError(ex)
        End Try
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
