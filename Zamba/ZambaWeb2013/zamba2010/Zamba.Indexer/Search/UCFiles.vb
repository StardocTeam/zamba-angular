Imports Zamba.Core
Imports Zamba.AppBlock

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
    Friend WithEvents IconList As System.Windows.Forms.ImageList
    Friend WithEvents PanelTop As Zamba.AppBlock.ZColorLabel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(UCFiles))
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.IconList = New System.Windows.Forms.ImageList(Me.components)
        Me.DSGroupTypes = New Zamba.Core.Data_Group_Doc
        Me.PanelTop = New Zamba.AppBlock.ZColorLabel
        CType(Me.DSGroupTypes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TreeView1
        '
        Me.TreeView1.BackColor = System.Drawing.Color.White
        Me.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.ForeColor = System.Drawing.Color.Black
        Me.TreeView1.HideSelection = False
        Me.TreeView1.ImageIndex = 4
        Me.TreeView1.ImageList = Me.IconList
        Me.TreeView1.Indent = 19
        Me.TreeView1.ItemHeight = 16
        Me.TreeView1.Location = New System.Drawing.Point(0, 24)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.SelectedImageIndex = 1
        Me.TreeView1.Size = New System.Drawing.Size(224, 240)
        Me.TreeView1.Sorted = True
        Me.TreeView1.TabIndex = 3
        '
        'IconList
        '
        Me.IconList.ImageSize = New System.Drawing.Size(16, 16)
        Me.IconList.ImageStream = CType(resources.GetObject("IconList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.IconList.TransparentColor = System.Drawing.Color.Transparent
        '
        'DSGroupTypes
        '
        Me.DSGroupTypes.DataSetName = "Data_Group_Doc"
        Me.DSGroupTypes.Locale = New System.Globalization.CultureInfo("es-ES")
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.White
        Me.PanelTop.Color1 = System.Drawing.Color.FromArgb(CType(90, Byte), CType(148, Byte), CType(229, Byte))
        Me.PanelTop.Color2 = System.Drawing.Color.Navy
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelTop.ForeColor = System.Drawing.Color.White
        Me.PanelTop.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(224, 24)
        Me.PanelTop.TabIndex = 9
        Me.PanelTop.Text = " Archivos"
        Me.PanelTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCFiles
        '
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.PanelTop)
        Me.Name = "UCFiles"
        Me.Size = New System.Drawing.Size(224, 264)
        CType(Me.DSGroupTypes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    'Declarations
    Private Root As System.Windows.Forms.TreeNode
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        GetDocGroups()
        FillTreeView()
    End Sub
    Private Sub GetDocGroups()
        Try
            DSGroupTypes.Doc_Type_Group.Clear()
            Me.DSGroupTypes.AcceptChanges()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Try
            UserBusiness.Rights.GetArchivosUserRight(Me.DSGroupTypes)
        Catch ex As SqlClient.SqlException
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

#Region "FillTreeView"
    Private Sub FillTreeView()
        Try
            Me.TreeView1.Nodes.Clear()
            Dim NoOfNodes As Integer = Me.DSGroupTypes.Doc_Type_Group.Count - 1
            If NoOfNodes = -1 Then
                Root = New System.Windows.Forms.TreeNode("No hay Archivos definidos")
                Root.ImageIndex = 5
                TreeView1.Nodes.Clear()
                TreeView1.Nodes.Add(Root)
                Exit Sub
            End If
            Root = New System.Windows.Forms.TreeNode("- Archivos -")
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
               
                If Me.DSGroupTypes.Doc_Type_Group(x).Parent_Id = 0 Then
                    Dim Value As String = Me.DSGroupTypes.Doc_Type_Group(x).Doc_Type_Group_Name
                    '[Sebastian 04-06-2009] se parsearon las dos lineas de codigo para salvar warnings
                    DocTypeGroupId = Me.DSGroupTypes.Doc_Type_Group(x).Doc_Type_Group_ID
                    ParentId = Me.DSGroupTypes.Doc_Type_Group(x).Parent_Id
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
                Me.ExpandNodes(Root)
            Catch ex As Exception
                zamba.core.zclass.raiseerror(ex)
            End Try
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
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
            Me.ExpandNodes(Node.Nodes(0))
        Else
            Node.Expand()
            Me.TreeView1.SelectedNode = Nothing
            Me.TreeView1.SelectedNode = Node
            'Me.TreeView1_AfterSelect(Node, New TreeViewEventArgs(Node, TreeViewAction.ByMouse))
        End If
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
