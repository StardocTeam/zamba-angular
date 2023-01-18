Imports Zamba.Core.DocTypes.DocAsociated
Imports ZAMBA.Core
Imports ZAMBA.AppBlock
Imports Zamba.Viewers
'Imports Zamba.CoreControls
'Imports zamba.DocTypes.Factory
'Imports zamba.data
Public Class Arbol
    Inherits Zamba.AppBlock.ZControl

    Dim DocTypeId As Int32
    Public DocTypes As New ArrayList

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                If DesignSandBar IsNot Nothing Then
                    DesignSandBar.Dispose()
                    DesignSandBar = Nothing
                End If
                If PanelTop IsNot Nothing Then
                    PanelTop.Dispose()
                    PanelTop = Nothing
                End If
                If DesignSandBar IsNot Nothing Then
                    DesignSandBar.Dispose()
                    DesignSandBar = Nothing
                End If
                If TreeView IsNot Nothing Then
                    TreeView.Dispose()
                    TreeView = Nothing
                End If
                If Splitter1 IsNot Nothing Then
                    Splitter1.Dispose()
                    Splitter1 = Nothing
                End If
                If Panel1 IsNot Nothing Then
                    Panel1.Dispose()
                    Panel1 = Nothing
                End If
                If cboDocType IsNot Nothing Then
                    cboDocType.Dispose()
                    cboDocType = Nothing
                End If
                If ZColorLabel1 IsNot Nothing Then
                    ZColorLabel1.Dispose()
                    ZColorLabel1 = Nothing
                End If
                If btnagregar IsNot Nothing Then
                    btnagregar.Dispose()
                    btnagregar = Nothing
                End If
                If btneliminar IsNot Nothing Then
                    btneliminar.Dispose()
                    btneliminar = Nothing
                End If
                If btnexpandir IsNot Nothing Then
                    btnexpandir.Dispose()
                    btnexpandir = Nothing
                End If
                If btncontraer IsNot Nothing Then
                    btncontraer.Dispose()
                    btncontraer = Nothing
                End If
                If IL IsNot Nothing Then
                    IL.Dispose()
                    IL = Nothing
                End If
                If UcIndexs IsNot Nothing Then
                    UcIndexs.Dispose()
                    UcIndexs = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        Catch
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents PanelTop As Zamba.AppBlock.ZColorLabel
    Friend WithEvents DesignSandBar As TD.SandBar.ToolBar
    Friend WithEvents TreeView As System.Windows.Forms.TreeView
    'Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cboDocType As System.Windows.Forms.ComboBox
    Friend WithEvents ZColorLabel1 As Zamba.AppBlock.ZColorLabel
    Friend WithEvents btnagregar As TD.SandBar.ButtonItem
    Friend WithEvents btneliminar As TD.SandBar.ButtonItem
    Friend WithEvents btnexpandir As TD.SandBar.ButtonItem
    Friend WithEvents btncontraer As TD.SandBar.ButtonItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Arbol))
        Me.PanelTop = New Zamba.AppBlock.ZColorLabel
        Me.DesignSandBar = New TD.SandBar.ToolBar
        Me.btnagregar = New TD.SandBar.ButtonItem
        Me.btneliminar = New TD.SandBar.ButtonItem
        Me.btnexpandir = New TD.SandBar.ButtonItem
        Me.btncontraer = New TD.SandBar.ButtonItem
        Me.TreeView = New System.Windows.Forms.TreeView
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cboDocType = New System.Windows.Forms.ComboBox
        Me.ZColorLabel1 = New Zamba.AppBlock.ZColorLabel
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.White
        Me.PanelTop.Color1 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.PanelTop.Color2 = System.Drawing.Color.Navy
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelTop.ForeColor = System.Drawing.Color.White
        Me.PanelTop.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(272, 21)
        Me.PanelTop.TabIndex = 7
        Me.PanelTop.Text = "  Documentos a Insertar"
        Me.PanelTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DesignSandBar
        '
        Me.DesignSandBar.Buttons.AddRange(New TD.SandBar.ToolbarItemBase() {Me.btnagregar, Me.btneliminar, Me.btnexpandir, Me.btncontraer})
        Me.DesignSandBar.Closable = False
        Me.DesignSandBar.DockLine = 1
        Me.DesignSandBar.Guid = New System.Guid("1fd972d3-e337-4d0f-ab9e-b228d26929dd")
        Me.DesignSandBar.Location = New System.Drawing.Point(0, 21)
        Me.DesignSandBar.Name = "DesignSandBar"
        Me.DesignSandBar.Size = New System.Drawing.Size(272, 26)
        Me.DesignSandBar.TabIndex = 156
        Me.DesignSandBar.Text = ""
        '
        'btnagregar
        '
        Me.btnagregar.Icon = CType(resources.GetObject("btnagregar.Icon"), System.Drawing.Icon)
        Me.btnagregar.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.Lowest
        Me.btnagregar.Padding.Left = 5
        Me.btnagregar.Padding.Right = 5
        Me.btnagregar.Tag = "AGREGAR"
        Me.btnagregar.ToolTipText = "AGREGAR DOCUMENTOS PARA INSERTAR"
        '
        'btneliminar
        '
        Me.btneliminar.Icon = CType(resources.GetObject("btneliminar.Icon"), System.Drawing.Icon)
        Me.btneliminar.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.High
        Me.btneliminar.Padding.Left = 5
        Me.btneliminar.Padding.Right = 5
        Me.btneliminar.Tag = "ELIMINAR"
        Me.btneliminar.ToolTipText = "ELIMINAR DOCUMENTO(S) POR INSERTAR"
        '
        'btnexpandir
        '
        Me.btnexpandir.BeginGroup = True
        Me.btnexpandir.Icon = CType(resources.GetObject("btnexpandir.Icon"), System.Drawing.Icon)
        Me.btnexpandir.Padding.Left = 5
        Me.btnexpandir.Padding.Right = 5
        Me.btnexpandir.Tag = "EXPANDIR"
        Me.btnexpandir.ToolTipText = "EXPANDIR ARBOL DE DOCUMENTOS"
        '
        'btncontraer
        '
        Me.btncontraer.Icon = CType(resources.GetObject("btncontraer.Icon"), System.Drawing.Icon)
        Me.btncontraer.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.High
        Me.btncontraer.Padding.Left = 5
        Me.btncontraer.Padding.Right = 5
        Me.btncontraer.Tag = "CONTRAER"
        Me.btncontraer.ToolTipText = "CONTRAER ARBOL DE DOCUMENTOS"
        '
        'TreeView
        '
        Me.TreeView.BackColor = System.Drawing.Color.White
        Me.TreeView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TreeView.Dock = System.Windows.Forms.DockStyle.Top
        Me.TreeView.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeView.ForeColor = System.Drawing.Color.Black
        Me.TreeView.FullRowSelect = True
        Me.TreeView.HideSelection = False
        Me.TreeView.ItemHeight = 18
        Me.TreeView.Location = New System.Drawing.Point(0, 47)
        Me.TreeView.Name = "TreeView"
        Me.TreeView.Size = New System.Drawing.Size(272, 135)
        Me.TreeView.TabIndex = 157
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.Blue
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 182)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(272, 3)
        Me.Splitter1.TabIndex = 158
        Me.Splitter1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.cboDocType)
        Me.Panel1.Controls.Add(Me.ZColorLabel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 185)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(272, 287)
        Me.Panel1.TabIndex = 159
        '
        'cboDocType
        '
        Me.cboDocType.Dock = System.Windows.Forms.DockStyle.Top
        Me.cboDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocType.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDocType.Location = New System.Drawing.Point(0, 21)
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(272, 21)
        Me.cboDocType.TabIndex = 161
        '
        'ZColorLabel1
        '
        Me.ZColorLabel1.BackColor = System.Drawing.Color.White
        Me.ZColorLabel1.Color1 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.ZColorLabel1.Color2 = System.Drawing.Color.Navy
        Me.ZColorLabel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ZColorLabel1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZColorLabel1.ForeColor = System.Drawing.Color.White
        Me.ZColorLabel1.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.ZColorLabel1.Location = New System.Drawing.Point(0, 0)
        Me.ZColorLabel1.Name = "ZColorLabel1"
        Me.ZColorLabel1.Size = New System.Drawing.Size(272, 21)
        Me.ZColorLabel1.TabIndex = 160
        Me.ZColorLabel1.Text = " Tipo de Documento e Indices"
        Me.ZColorLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Arbol
        '
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.TreeView)
        Me.Controls.Add(Me.DesignSandBar)
        Me.Controls.Add(Me.PanelTop)
        Me.Name = "Arbol"
        Me.Size = New System.Drawing.Size(272, 472)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "DocTypes"
    Private Sub GetDocTypes()
        Try
            If DocTypes.Count = 0 Then DocTypes = DocTypesBusiness.GetDocTypesbyUserRightsOfView(UserBusiness.Rights.CurrentUser.ID, Zamba.Core.RightsType.Create)

            If DocTypes.Count <= 0 Then
                MessageBox.Show("Error 013: No hay definidos Tipos de Documentos para realizar la indexacion o no tiene Permisos para crear Documentos", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'Me.Close()
                Exit Sub
            End If

            If Boolean.Parse(UserPreferences.getValue("ViewVirtualOnComboBox", Sections.InsertPreferences, True)) = False Then
                Dim auxarrya As New ArrayList
                For Each dt As DocType In DocTypes
                    If DocAsociatedBusiness.getAsociatedFormsId(CInt(dt.ID)).Tables(0).Rows.Count = 0 Then
                        auxarrya.Add(dt)
                    End If
                Next
                DocTypes = auxarrya
            End If

            'RemoveHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChangedSub
            cboDocType.BeginUpdate()
            'For Each doctype As DocType In DocTypes
            cboDocType.DataSource = DocTypes
            cboDocType.DisplayMember = "Name"
            cboDocType.ValueMember = "Id"
            ' Next
            'cboDocType.DataSource = DocTypes
            cboDocType.EndUpdate()
            'AddHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChangedSub
            'Me.SelectDocType()
        Catch ex As Exception
            MessageBox.Show("Error: 014: Ocurrio un Error al cargar los Tipos de Documentos para la Indexación ", "Zamba Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
            'Me.Close()
        End Try
    End Sub
#End Region

    Public UcIndexs As New UCIndexViewer(False, True, True, True, True)
    Private Sub IndexerIndices_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UcIndexs.FlagIsIndexer = True
        UcIndexs.Dock = DockStyle.Fill
        Me.Panel1.Controls.Add(UcIndexs)
        Me.Panel1.AutoScroll = False
        UcIndexs.BringToFront()
        GetDocTypes()
    End Sub

    Public Sub SetIndexs(ByVal Array As ArrayList)

    End Sub

    Public Sub SetDocTypeId(ByVal Id As Int32)
        If Me.cboDocType.Items.Count = 0 Then GetDocTypes()
        Me.cboDocType.SelectedValue = Id
    End Sub

    Public Sub MostrarIndices(ByVal NewResult As NewResult)
        If Me.cboDocType.SelectedIndex = -1 Then Me.cboDocType.SelectedIndex = 0
        'If IsNothing(NewResult) OrElse NewResult.Id = 0 Then
        'aqui hacer un result con lo contenido en el combo
        'NewResult = Results_Factory.GetNewNewResult(Integer.Parse(Me.cboDocType.SelectedValue.ToString))
        'Dim dtTemp As New DocType
        'dtTemp = DocTypesBusiness.GetDocType(Integer.Parse(Me.cboDocType.SelectedValue.ToString))
        'NewResult.Parent = dtTemp.Parent
        'End If
        UcIndexs.ShowIndexs(DirectCast(NewResult, NewResult))

        'llamada original antes de los jerarquicos
        'UcIndexs.ShowIndexs(DirectCast(NewResult, NewResult))

        UcIndexs.ShowIndexs(DirectCast(NewResult, NewResult), False, Int32.Parse(Me.cboDocType.SelectedValue.ToString))

        Try
            DesHabilitarSelectedIndexChanged()
            Me.cboDocType.SelectedValue = NewResult.Parent.ID
            HabilitarSelectedIndexChanged()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Public Sub HabilitarSelectedIndexChanged()
        Try
            RemoveHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChangedSub
            AddHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChangedSub
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Public Sub DesHabilitarSelectedIndexChanged()
        Try
            RemoveHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChangedSub
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Public Event SelectedIndexChanged(ByVal DocType As Int32)

    ''' <summary>
    '''  [Sebastian 11-05-09] COMMETED lanza el evento selectedindexchanged cada vez
    '''  que se cambia el tipo de documento en el combo de tipo de documento en la
    '''  insercion de documentos.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SelectedIndexChangedSub(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.cboDocType.SelectedIndex <> -1 Then RaiseEvent SelectedIndexChanged(Me.cboDocType.SelectedIndex)
            '[Sebastian 11-05-09] se genero un nuevo evento dle tipo treeview para poder llamar a
            'after select y de esta forma actualizar en tiempo real la imagen del visualizador de insertar
            '(solapa indexer) documento.
            Dim a As New System.Windows.Forms.TreeViewEventArgs(TreeView.SelectedNode, TreeViewAction.Unknown)
            TreeView_AfterSelect(Nothing, a)

           
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public TreePorInsertar As New TreeNode
    Public TreeInsertados As New TreeNode
    Dim LastNode As TreeNode

    Public Sub AddSpecialNodes()
        Try
            TreePorInsertar = New TreeNode
            TreeInsertados = New TreeNode
            TreePorInsertar.Text = "Documentos a Insertar"
            TreePorInsertar.Tag = "Parent"
            TreePorInsertar.ImageIndex = 25
            TreePorInsertar.SelectedImageIndex = 1
            TreePorInsertar.ForeColor = Color.Black
            TreeInsertados.Text = "Documentos Insertados"
            TreeInsertados.Tag = "Parent2"
            TreeInsertados.ImageIndex = 24
            TreeInsertados.SelectedImageIndex = 0
            TreeInsertados.ForeColor = Color.Black
            Me.TreeView.Nodes.Add(TreePorInsertar)
            Me.TreeView.Nodes.Add(TreeInsertados)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

#Region "LoadIcons"

    Private Structure SHFILEINFO
        Public hIcon As IntPtr
        Public iIcon As Integer
        Public dwAttributes As Integer
        <VBFixedString(260), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=260)> Public szDisplayName As String
        <VBFixedString(80), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=80)> Public szTypeName As String
    End Structure
    Private Declare Function SHGetFileInfo Lib "shell32.dll" Alias "SHGetFileInfoA" (ByVal pszPath As String, ByVal dwFileAttributes As Integer, ByRef psfi As SHFILEINFO, ByVal cbFileInfo As Integer, ByVal uFlags As Integer) As Integer
    Private Const SHGFI_ICON As Integer = &H100
    Private Const SHGFI_LARGEICON As Integer = &H0
    Private Const SHGFI_SMALLICON As Integer = &H1
    Private Const SHGFI_USEFILEATTRIBUTES As Integer = &H10
    Private Const SHGFI_TYPENAME As Integer = &H400

    Public Shared Function GetFileIcon(ByVal mPath As String, Optional ByVal Large As Boolean = False) As Icon
        'Dim SFI As SHFILEINFO
        Dim SFI As New SHFILEINFO
        If System.IO.File.Exists(mPath) Then
            SHGetFileInfo(mPath, 0, SFI, System.Runtime.InteropServices.Marshal.SizeOf(SFI), CInt(SHGFI_ICON Or CInt(IIf(Large, SHGFI_LARGEICON, SHGFI_SMALLICON))))
        Else
            SHGetFileInfo(mPath, 0, SFI, System.Runtime.InteropServices.Marshal.SizeOf(SFI), CInt(SHGFI_ICON) Or SHGFI_USEFILEATTRIBUTES Or CInt(IIf(Large, SHGFI_LARGEICON, SHGFI_SMALLICON)))
        End If
        GetFileIcon = Icon.FromHandle(SFI.hIcon)
    End Function
#End Region
    Public IL As New Zamba.AppBlock.ZIconsList

    Public Sub CargarFiles(ByVal File As NewResult)
        'PARA CADA NUEVO FILE QUE RECIBE COMO NEWRESULT LO INSERTA EN EL TREEVIEW
        Dim NuevoFile As IO.FileInfo
        Dim ZNewResultNode As ZNewResultNode
        Try
            NuevoFile = New IO.FileInfo(File.Name)
            ZNewResultNode = New ZNewResultNode(File)

            Try
                Dim LoadedIcono As Icon = GetFileIcon(File.FullPath)
                Dim img As Bitmap = LoadedIcono.ToBitmap()
                img.MakeTransparent(img.GetPixel(0, 0))
                IL.ZIconList.Images.Add(img)
                Dim ImageIndex As Integer = IL.ZIconList.Images.Count - 1
                ZNewResultNode.ImageIndex = ImageIndex
                ZNewResultNode.SelectedImageIndex = ImageIndex
            Catch ex As Exception
                ZNewResultNode.ImageIndex = 2
                ZNewResultNode.SelectedImageIndex = 2
                zamba.core.zclass.raiseerror(ex)
            End Try

            ZNewResultNode.Text = NuevoFile.Name
            If IsNothing(Me.TreePorInsertar) = True Then TreePorInsertar = New TreeNode
            ZNewResultNode.ForeColor = Color.Black
            Me.TreePorInsertar.Nodes.Add(ZNewResultNode)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            NuevoFile = Nothing
            ZNewResultNode = Nothing
        End Try
    End Sub

    Public Sub CargarFiles2(ByVal File As NewResult)
        'PARA CADA NUEVO FILE QUE RECIBE COMO NEWRESULT LO INSERTA EN EL TREEVIEW
        Dim NuevoFile As IO.FileInfo
        Dim ZNewResultNode As ZNewResultNode
        Try
            NuevoFile = New IO.FileInfo(File.NewFile)
            ZNewResultNode = New ZNewResultNode(File)

            Try
                Dim LoadedIcono As Icon = GetFileIcon(File.NewFile)
                Dim img As Bitmap = LoadedIcono.ToBitmap()
                img.MakeTransparent(img.GetPixel(0, 0))
                IL.ZIconList.Images.Add(img)
                Dim ImageIndex As Integer = IL.ZIconList.Images.Count - 1
                ZNewResultNode.ImageIndex = ImageIndex
                ZNewResultNode.SelectedImageIndex = ImageIndex
            Catch ex As Exception
                ZNewResultNode.ImageIndex = 2
                ZNewResultNode.SelectedImageIndex = 2
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            'If File.Extension.ToUpper = ".DOC" Then
            '    ZNewResultNode.ImageIndex = 5
            'ElseIf File.Extension.ToUpper = ".XLS" Then
            '    ZNewResultNode.ImageIndex = 6
            'ElseIf File.Extension.ToUpper = ".PPT" Then
            '    ZNewResultNode.ImageIndex = 7
            'ElseIf File.Extension.ToUpper = ".HTML" OrElse File.Extension.ToUpper = ".HTM" OrElse File.Extension.ToUpper = ".MHT" Then
            '    ZNewResultNode.ImageIndex = 8
            'ElseIf File.Extension.ToUpper = ".GIF" OrElse File.Extension.ToUpper = ".JPG" OrElse File.Extension.ToUpper = ".BMP" OrElse File.Extension.ToUpper = ".TIF" OrElse File.Extension.ToUpper = ".TIFF" OrElse File.Extension.ToUpper = ".JPEG" Then
            '    ZNewResultNode.ImageIndex = 9
            'ElseIf File.Extension.ToUpper = ".TXT" Then
            '    ZNewResultNode.ImageIndex = 10
            'Else
            '    ZNewResultNode.ImageIndex = 2
            'End If

            ZNewResultNode.Text = NuevoFile.Name
            ZNewResultNode.ForeColor = Color.Black
            If IsNothing(Me.TreeInsertados) = True Then TreeInsertados = New TreeNode
            Me.TreeInsertados.Nodes.Add(ZNewResultNode)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            NuevoFile = Nothing
            ZNewResultNode = Nothing
        End Try
    End Sub
    Public Sub SelectFirstFile()
        Try
            '(pablo)
            'If Me.TreePorInsertar.Nodes.Count > 0 Then Me.TreeView.SelectedNode = Me.TreePorInsertar.FirstNode
            If Me.TreePorInsertar.Nodes.Count > 0 Then Me.TreeView.SelectedNode = Me.TreePorInsertar.Nodes(Me.TreePorInsertar.Nodes.Count - 1)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Event SeleccionadoParent(ByVal Estado As Boolean)
    Public Event PrevioSeleccionado(ByVal SelectedId As Long)
    Public Event Seleccionado(ByVal SelectedId As Long, ByVal InsertingDoc As Boolean)
    Public Event Seleccionado_Insertado(ByVal SelectedId As Long)
    Public Event ShowBrowser()
    Public Event Eliminado(ByVal Node As ZNewResultNode)
    Public Event EliminadosTodos1()
    Public Event EliminadosTodos2()
    Public Event TextSelected(ByVal Selected As Int32, ByVal Total As Int32)

    ''' <summary>
    ''' [Sebastian 11-08-2009] se agrego para ignorar el permiso de re index en la pantalla de insercion.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TreeView_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView.AfterSelect
        Try
            LastNode = e.Node
            'todo patricio: comente el raiseevent porque me dejaba enable false el arbol
            If Me.TreeView.SelectedNode.Tag.ToString = "Parent" OrElse Me.TreeView.SelectedNode.Tag.ToString = "Parent2" Then
                'RaiseEvent SeleccionadoParent(True)
                Exit Sub
            End If

            RaiseEvent SeleccionadoParent(False)
            '[Sebastian 11-05-09] se agrego direccast para salvar el warning.
            Dim ZNode As ZNewResultNode = DirectCast(e.Node, ZNewResultNode)
            Dim SelectedId As Long = ZNode.ZambaCore.ID

            If Me.TreeView.SelectedNode.Parent.Tag.ToString = "Parent2" Then
                RaiseEvent Seleccionado_Insertado(SelectedId)
            Else
                'aviso que estoy insertando y que obvie el permiso de reindex
                DocTypesBusiness.InsertingDoc(True)


                RaiseEvent Seleccionado(SelectedId, True)



                'seteo a false (no ignorar is re index), para que en caso de que el usuario cambie
                'de pantalla se aplique el permiso de re index. y quede todo en su estado ortiginal
                DocTypesBusiness.InsertingDoc(False)
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    '    RaiseEvent TextSelected(e.Node.Index, e.Node.GetNodeCount(True))


    Private Sub EliminarTodos1()
        Try
            Me.TreePorInsertar.Nodes.Clear()
            RaiseEvent EliminadosTodos1()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub EliminarTodos2()
        Try
            Me.TreeInsertados.Nodes.Clear()
            RaiseEvent EliminadosTodos2()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub


    Private Sub TreeView_BeforeSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeView.BeforeSelect
        Try
            If IsNothing(Me.TreeView.SelectedNode) OrElse Me.TreeView.SelectedNode.Tag.ToString = "Parent" OrElse Me.TreeView.SelectedNode.Tag.ToString = "Parent2" OrElse Me.TreeView.SelectedNode.Parent.Tag.ToString = "Parent2" Then Exit Sub
            '[Sebastian 11-05-09] se aplico direccast para salvar el warning.
            Dim Nodo As ZNewResultNode = DirectCast(LastNode, ZNewResultNode)
            Dim SelectedId As Long = Nodo.ZambaCore.ID
            RaiseEvent PrevioSeleccionado(SelectedId)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub



    Private Sub DesignSandBar_ButtonClick(ByVal sender As System.Object, ByVal e As TD.SandBar.ToolBarItemEventArgs) Handles DesignSandBar.ButtonClick
        Select Case CStr(e.Item.Tag).ToUpper
            Case "AGREGAR"
                RaiseEvent ShowBrowser()
            Case "ELIMINAR"
                Try
                    If Me.TreeView.SelectedNode.Tag.ToString = "Parent" Then
                        If MessageBox.Show("¿DESEA VACIAR LA LISTA DE DOCUMENTOS POR INDEXAR?", "", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            EliminarTodos1()
                        End If
                        Exit Sub
                    ElseIf Me.TreeView.SelectedNode.Tag.ToString = "Parent2" Then
                        If MessageBox.Show("¿DESEA VACIAR LA LISTA DE DOCUMENTOS INDEXADOS?", "", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            EliminarTodos2()
                        End If
                        Exit Sub
                    End If

                    'If Me.TreeView.SelectedNode.Parent.Tag.ToString = "Parent2" Then Exit Sub

                    RaiseEvent Eliminado(DirectCast(Me.TreeView.SelectedNode, ZNewResultNode))
                    Me.TreeView.SelectedNode.Remove()
                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try
            Case "EXPANDIR"
                Try
                    Me.TreeView.ExpandAll()
                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try
            Case "CONTRAER"
                Try
                    Me.TreeView.CollapseAll()
                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try
        End Select
    End Sub


    
    'Private Sub TreeView_BeforeLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs)

    'End Sub
End Class
