Imports Zamba.Core

Public Class UCFiles
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Public WithEvents TreeView1 As System.Windows.Forms.TreeView
    Public WithEvents DSGroupTypes As Data_Group_Doc
    Friend WithEvents IconList As ImageList
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(UCFiles))
        TreeView1 = New System.Windows.Forms.TreeView()
        IconList = New ImageList(components)
        DSGroupTypes = New Zamba.Core.Data_Group_Doc()
        CType(DSGroupTypes, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'TreeView1
        '
        TreeView1.BackColor = System.Drawing.Color.White
        TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        TreeView1.Font = AppBlock.ZambaUIHelpers.GetFontFamily()
        TreeView1.ForeColor = AppBlock.ZambaUIHelpers.GetFontsColor()
        TreeView1.HideSelection = False
        TreeView1.ImageIndex = 4
        TreeView1.ImageList = IconList
        TreeView1.Indent = 19
        TreeView1.ItemHeight = 22
        TreeView1.Location = New System.Drawing.Point(0, 0)
        TreeView1.Name = "TreeView1"
        TreeView1.SelectedImageIndex = 1
        TreeView1.Size = New System.Drawing.Size(224, 264)
        TreeView1.Sorted = True
        TreeView1.TabIndex = 3
        '
        'IconList
        '
        IconList.ImageStream = CType(resources.GetObject("IconList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        IconList.TransparentColor = System.Drawing.Color.Transparent
        IconList.Images.SetKeyName(0, "")
        IconList.Images.SetKeyName(1, "appbar.cabinet.files.variant.png")
        IconList.Images.SetKeyName(2, "appbar.folder.open.png")
        IconList.Images.SetKeyName(3, "")
        IconList.Images.SetKeyName(4, "appbar.cabinet.variant.png")
        IconList.Images.SetKeyName(5, "appbar.box.layered.png")
        '
        'DSGroupTypes
        '
        DSGroupTypes.DataSetName = "Data_Group_Doc"
        DSGroupTypes.Locale = New System.Globalization.CultureInfo("es-ES")
        DSGroupTypes.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'UCFiles
        '
        Controls.Add(TreeView1)
        Name = "UCFiles"
        Size = New System.Drawing.Size(224, 264)
        CType(DSGroupTypes, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

#End Region

    'Declarations
    Private Root As TreeNode
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        GetDocGroups()
    End Sub
    Private Sub GetDocGroups()
        Try
            DSGroupTypes = UserBusiness.Rights.GetArchivosUserRight(True)
        Catch ex As SqlClient.SqlException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "FillTreeView"
    Private Sub FillTreeView()
        Try
            TreeView1.Nodes.Clear()
            Dim NoOfNodes As Integer = DSGroupTypes.Doc_Type_Group.Count - 1
            If NoOfNodes = -1 Then
                Root = New TreeNode("No hay Secciones definidas")
                Root.ImageIndex = 5
                TreeView1.Nodes.Clear()
                TreeView1.Nodes.Add(Root)
                Exit Sub
            End If
            Root = New System.Windows.Forms.TreeNode("SECCIONES")
            Root.ForeColor = Color.FromArgb(0, 157, 224)
            Root.ImageIndex = 5
            Root.Tag = 0
            TreeView1.Nodes.Add(Root)
            '[Sebastian 04-06-2009] se sacaron las declaraciones afuera para no declarar en cada ciclo 
            'como estaba antes. Y agregaron dos mas para hacer casteo
            Dim DocTypeGroupId As Decimal = 0
            Dim ParentId As Decimal = 0
            Dim id As Int64 = 0
            Dim parent_id As Int64 = 0

            For x As Int32 = 0 To NoOfNodes

                If DSGroupTypes.Doc_Type_Group(x).Parent_Id = 0 Then
                    Dim Value As String = DSGroupTypes.Doc_Type_Group(x).Doc_Type_Group_Name
                    '[Sebastian 04-06-2009] se parsearon las dos lineas de codigo para salvar warnings
                    DocTypeGroupId = DSGroupTypes.Doc_Type_Group(x).Doc_Type_Group_ID
                    ParentId = DSGroupTypes.Doc_Type_Group(x).Parent_Id
                    'Dim Id As Int64 = Me.DSGroupTypes.Doc_Type_Group(x).Doc_Type_Group_ID
                    'Dim Parent_ID As Int32 = Me.DSGroupTypes.Doc_Type_Group(x).Parent_Id
                    id = Int64.Parse(DocTypeGroupId.ToString)
                    parent_id = Int64.Parse(ParentId.ToString)
                    '[Sebastian 04-06-2009]se agrego parse para salvar warning
                    addnode(Value, Int32.Parse(id.ToString), Root)
                End If
            Next
            Root.Expand()
            Try
                ExpandNodes(Root)
                SelectLastUsedSection
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


    Public LastSectionSelected As String

    Private Sub SelectLastUsedSection()
        If LastSectionSelected Is Nothing Then
            LastSectionSelected = UserPreferences.getValue("LastSectionSelected", UPSections.Search, 0)
            For Each node As TreeNode In TreeView1.Nodes(0).Nodes
                If node.Tag = LastSectionSelected Then
                    TreeView1.SelectedNode = node
                    Exit For
                End If
            Next
        End If
        '        Me.TreeView1_AfterSelect(Node, New TreeViewEventArgs(Node, TreeViewAction.ByMouse))
    End Sub

    Private Sub addnode(ByVal Value As String, ByVal id As Integer, ByVal root As TreeNode)
        Dim Root1 As New System.Windows.Forms.TreeNode(Value)
        Root1.Tag = id
        Root1.ImageIndex = 4
        root.Nodes.Add(Root1)
        Dim Subnodes As DataView
        '[Sebastian 04-06-2009] se agrego parse para salvar warning
        Subnodes = searchforsubnodes(Int32.Parse(Root1.Tag.ToString))
        Dim SUBNODESCOUNT As Int32 = Subnodes.Count
        If SUBNODESCOUNT <> 0 Then
            Dim i As Integer
            For i = 0 To SUBNODESCOUNT - 1
                '[Sebastian 04-06-2009] se agregaron parse para salvar los warning
                addnode(Subnodes.Item(i).Item("Doc_Type_Group_Name").ToString, Int32.Parse(Subnodes.Item(i).Item("Doc_Type_Group_ID").ToString), Root1)
            Next
        End If
    End Sub
    Private Function searchforsubnodes(ByVal id As Integer) As DataView
        Dim FilteredDS As DataView
        FilteredDS = New DataView(DSGroupTypes.Doc_Type_Group, "Parent_Id = " & id, "Doc_Type_Group_ID", DataViewRowState.CurrentRows)
        Return FilteredDS
    End Function
    Public Sub ExpandNodes(Optional ByVal Node As TreeNode = Nothing)
        If IsNothing(Node) Then Node = Root
        If Node.GetNodeCount(False) > 0 Then
            Node.Nodes(0).ExpandAll()
            ExpandNodes(Node.Nodes(0))
        Else
            Node.Expand()
            '            Me.TreeView1.SelectedNode = Nothing
            '           Me.TreeView1.SelectedNode = Node
            'Me.TreeView1_AfterSelect(Node, New TreeViewEventArgs(Node, TreeViewAction.ByMouse))
        End If
    End Sub

    Private Sub UCFiles_Load(sender As Object, e As EventArgs) Handles Me.Load
        FillTreeView()

    End Sub
#End Region

    'Friend Sub SelectFirstFile()
    '    Try
    '        If Not IsNothing(Root.Nodes(0)) Then
    '            Me.TreeView1.SelectedNode = Root.Nodes(0)
    '        End If
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub
    'Private Sub TreeView1_BeforeSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeView1.BeforeSelect
    '    Try
    '        If Me.TreeView1.SelectedNode Is Root Then
    '            Me.TreeView1.SelectedImageIndex = 5
    '        Else
    '            Me.TreeView1.SelectedImageIndex = 1
    '        End If
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub

    '  Public Event SaveAllObjects()

End Class
