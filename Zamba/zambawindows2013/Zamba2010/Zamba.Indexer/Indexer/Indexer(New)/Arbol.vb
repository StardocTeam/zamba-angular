Imports Zamba.Core
Imports Zamba.Viewers
Imports System.Collections.Generic
'Imports Zamba.CoreControls
'Imports zamba.DocTypes.Factory
'Imports zamba.data
Public Class Arbol
    Inherits Zamba.AppBlock.ZControl
    Implements IDisposable

    Dim DocTypeId As Int64
    Public DocTypes As New List(Of IDocType)

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            Try
                If disposing Then
                    If Not (components Is Nothing) Then
                        components.Dispose()
                    End If

                    RemoveHandler MyBase.Load, AddressOf IndexerIndices_Load

                    If DesignSandBar IsNot Nothing Then
                        RemoveHandler DesignSandBar.ButtonClick, AddressOf DesignSandBar_ButtonClick
                        DesignSandBar.Dispose()
                        DesignSandBar = Nothing
                    End If
                    If PanelTop IsNot Nothing Then
                        PanelTop.Dispose()
                        PanelTop = Nothing
                    End If


                    If TreeView IsNot Nothing Then
                        RemoveHandler TreeView.AfterSelect, AddressOf TreeView_AfterSelect
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
                        RemoveHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChangedSub
                        cboDocType.Dispose()
                        cboDocType = Nothing
                    End If
                    If ZLabel1 IsNot Nothing Then
                        ZLabel1.Dispose()
                        ZLabel1 = Nothing
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

                    If LastNode IsNot Nothing Then
                        'todo: si tiene newresult dentro aplicarle dispose
                        LastNode = Nothing
                    End If
                    If UcIndexs IsNot Nothing Then
                        UcIndexs.Dispose()
                        UcIndexs = Nothing
                    End If
                End If
                MyBase.Dispose(disposing)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
            End Try

            isDisposed = True
        End If
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    Private isDisposed As Boolean
    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents PanelTop As ZLabel
    Friend WithEvents DesignSandBar As TD.SandBar.ToolBar
    Friend WithEvents TreeView As System.Windows.Forms.TreeView
    'Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cboDocType As ComboBox
    Friend WithEvents ZLabel1 As ZLabel
    Friend WithEvents btnagregar As TD.SandBar.ButtonItem
    Friend WithEvents btneliminar As TD.SandBar.ButtonItem
    Friend WithEvents btnexpandir As TD.SandBar.ButtonItem
    Friend WithEvents btncontraer As TD.SandBar.ButtonItem
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(Arbol))
        PanelTop = New ZLabel()
        DesignSandBar = New TD.SandBar.ToolBar()
        btnagregar = New TD.SandBar.ButtonItem()
        btneliminar = New TD.SandBar.ButtonItem()
        btnexpandir = New TD.SandBar.ButtonItem()
        btncontraer = New TD.SandBar.ButtonItem()
        TreeView = New System.Windows.Forms.TreeView()
        Splitter1 = New System.Windows.Forms.Splitter()
        Panel1 = New System.Windows.Forms.Panel()
        cboDocType = New ComboBox()
        ZLabel1 = New ZLabel()
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'PanelTop
        '
        PanelTop.BackColor = System.Drawing.Color.White
        PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        PanelTop.FlatStyle = FlatStyle.Flat
        PanelTop.ForeColor = System.Drawing.Color.White
        PanelTop.Location = New System.Drawing.Point(0, 0)
        PanelTop.Name = "PanelTop"
        PanelTop.Size = New System.Drawing.Size(302, 25)
        PanelTop.TabIndex = 7
        PanelTop.Text = "  Documentos a Insertar"
        PanelTop.TextAlign = ContentAlignment.MiddleLeft
        '
        'DesignSandBar
        '
        DesignSandBar.Buttons.AddRange(New TD.SandBar.ToolbarItemBase() {btnagregar, btneliminar, btnexpandir, btncontraer})
        DesignSandBar.Closable = False
        DesignSandBar.DockLine = 1
        DesignSandBar.Guid = New System.Guid("1fd972d3-e337-4d0f-ab9e-b228d26929dd")
        DesignSandBar.Location = New System.Drawing.Point(0, 25)
        DesignSandBar.Name = "DesignSandBar"
        DesignSandBar.Size = New System.Drawing.Size(302, 26)
        DesignSandBar.TabIndex = 156
        DesignSandBar.Text = ""
        '
        'btnagregar
        '
        btnagregar.Icon = CType(resources.GetObject("btnagregar.Icon"), System.Drawing.Icon)
        btnagregar.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.Lowest
        btnagregar.Padding.Left = 5
        btnagregar.Padding.Right = 5
        btnagregar.Tag = "AGREGAR"
        btnagregar.ToolTipText = "AGREGAR DOCUMENTOS PARA INSERTAR"
        '
        'btneliminar
        '
        btneliminar.Icon = CType(resources.GetObject("btneliminar.Icon"), System.Drawing.Icon)
        btneliminar.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.High
        btneliminar.Padding.Left = 5
        btneliminar.Padding.Right = 5
        btneliminar.Tag = "ELIMINAR"
        btneliminar.ToolTipText = "ELIMINAR DOCUMENTO(S) POR INSERTAR"
        '
        'btnexpandir
        '
        btnexpandir.BeginGroup = True
        btnexpandir.Icon = CType(resources.GetObject("btnexpandir.Icon"), System.Drawing.Icon)
        btnexpandir.Padding.Left = 5
        btnexpandir.Padding.Right = 5
        btnexpandir.Tag = "EXPANDIR"
        btnexpandir.ToolTipText = "EXPANDIR ARBOL DE DOCUMENTOS"
        '
        'btncontraer
        '
        btncontraer.Icon = CType(resources.GetObject("btncontraer.Icon"), System.Drawing.Icon)
        btncontraer.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.High
        btncontraer.Padding.Left = 5
        btncontraer.Padding.Right = 5
        btncontraer.Tag = "CONTRAER"
        btncontraer.ToolTipText = "CONTRAER ARBOL DE DOCUMENTOS"
        '
        'TreeView
        '
        TreeView.BackColor = System.Drawing.Color.White
        TreeView.BorderStyle = System.Windows.Forms.BorderStyle.None
        TreeView.Dock = System.Windows.Forms.DockStyle.Top
        TreeView.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        TreeView.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        TreeView.FullRowSelect = True
        TreeView.HideSelection = False
        TreeView.ItemHeight = 18
        TreeView.Location = New System.Drawing.Point(0, 51)
        TreeView.Name = "TreeView"
        TreeView.Size = New System.Drawing.Size(302, 135)
        TreeView.TabIndex = 157
        '
        'Splitter1
        '
        Splitter1.BackColor = System.Drawing.Color.Gray
        Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Splitter1.Location = New System.Drawing.Point(0, 186)
        Splitter1.Name = "Splitter1"
        Splitter1.Size = New System.Drawing.Size(302, 1)
        Splitter1.TabIndex = 158
        Splitter1.TabStop = False
        '
        'Panel1
        '
        Panel1.BackColor = System.Drawing.Color.White
        Panel1.Controls.Add(cboDocType)
        Panel1.Controls.Add(ZLabel1)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 187)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(432, 322)
        Panel1.TabIndex = 159
        '
        'cboDocType
        '
        cboDocType.BackColor = System.Drawing.Color.White
        cboDocType.Dock = System.Windows.Forms.DockStyle.Top
        cboDocType.DropDownHeight = 110
        'Me.cboDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        cboDocType.FlatStyle = FlatStyle.Popup
        cboDocType.Font = New Font("Verdana", 9.75!)
        cboDocType.ForeColor = System.Drawing.Color.Black
        cboDocType.IntegralHeight = False
        cboDocType.Location = New System.Drawing.Point(0, 25)
        cboDocType.Name = "cboDocType"
        cboDocType.Size = New System.Drawing.Size(302, 40)
        cboDocType.TabIndex = 161
        '
        'ZLabel1
        '
        ZLabel1.BackColor = System.Drawing.Color.White
        ZLabel1.Dock = System.Windows.Forms.DockStyle.Top
        ZLabel1.FlatStyle = FlatStyle.Flat
        ZLabel1.ForeColor = System.Drawing.Color.White
        ZLabel1.Location = New System.Drawing.Point(0, 0)
        ZLabel1.Name = "ZLabel1"
        ZLabel1.Size = New System.Drawing.Size(302, 25)
        ZLabel1.TabIndex = 160
        ZLabel1.Text = " Entidad y Atributos"
        ZLabel1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Arbol
        '
        Controls.Add(Panel1)
        Controls.Add(Splitter1)
        Controls.Add(TreeView)
        Controls.Add(DesignSandBar)
        Controls.Add(PanelTop)
        Name = "Arbol"
        Size = New System.Drawing.Size(302, 509)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

#Region "DocTypes"
    Private Sub LoadDocTypesCombo()

        Try
            If DocTypes.Count = 0 Then DocTypes = DocTypesBusiness.GetDocTypesbyUserRightsOfView(UserBusiness.Rights.CurrentUser.ID, RightsType.Create)

            If DocTypes.Count <= 0 Then
                MessageBox.Show("Error 013: No hay definidos Entidades para realizar la indexacion o no tiene Permisos para crear Documentos", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'Me.Close()
                Exit Sub
            End If

            Dim auxarrya As New List(Of ICore)
            For Each dt As DocType In DocTypes
                Dim forms As Generic.List(Of ZwebForm) = FormBusiness.GetForms(dt.ID, FormTypes.Insert, True)
                If forms Is Nothing OrElse forms.Count = 0 Then
                    auxarrya.Add(dt)
                End If
            Next

            If Not auxarrya.Count.Equals(0) Then
                cboDocType.BeginUpdate()
                cboDocType.DataSource = auxarrya
                cboDocType.DisplayMember = "Name"
                cboDocType.ValueMember = "ID"
                cboDocType.SelectedIndex = 0
                cboDocType.EndUpdate()
            End If

        Catch ex As Exception
            MessageBox.Show("Error: 014: Ocurrio un Error al cargar los Entidades para la Indexación ", "Zamba Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
            'Me.Close()
        End Try
    End Sub
#End Region

    Public TreePorInsertar As New TreeNode
    Public TreeInsertados As New TreeNode
    Dim LastNode As TreeNode
    Public UcIndexs As New UCIndexViewer(True, True, True)
    Private Sub IndexerIndices_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        UcIndexs.FlagIsIndexer = True
        UcIndexs.Dock = DockStyle.Fill
        Panel1.Controls.Add(UcIndexs)
        Panel1.AutoScroll = False
        UcIndexs.BringToFront()
        LoadDocTypesCombo()
    End Sub

    Public Sub SetDocTypeId(ByVal Id As Int64)
        DesHabilitarSelectedIndexChanged()
        If cboDocType.Items.Count > 0 Then
            If Id > 0 Then
                For Each e As IDocType In cboDocType.Items
                    If e.ID = Id Then
                        cboDocType.SelectedItem = e
                        cboDocType.SelectedValue = Id
                    End If
                Next
            End If
            If cboDocType.SelectedIndex < 0 Then cboDocType.SelectedIndex = 0
        End If
        HabilitarSelectedIndexChanged()
    End Sub
    Public Function GetSelectedDocType() As IDocType
        If cboDocType.Items.Count > 0 Then
            Return cboDocType.SelectedItem
        End If
    End Function
    Public Sub MostrarIndices(ByVal NewResult As NewResult, IsInserting As Boolean)
        UcIndexs.ShowIndexsForInsert(NewResult, IsInserting)
    End Sub

    Public Sub HabilitarSelectedIndexChanged()
        Try
            RemoveHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChangedSub
            AddHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChangedSub
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub DesHabilitarSelectedIndexChanged()
        Try
            RemoveHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChangedSub
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Event SelectedIndexChanged(ByVal DocType As IDocType)

    ''' <summary>
    '''  [Sebastian 11-05-09] COMMETED lanza el evento selectedindexchanged cada vez
    '''  que se cambia la entidad en el combo de entidad en la
    '''  insercion de documentos.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SelectedIndexChangedSub(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If cboDocType.SelectedIndex <> -1 Then RaiseEvent SelectedIndexChanged(DirectCast(cboDocType.SelectedItem, IDocType))
            '[Sebastian 11-05-09] se genero un nuevo evento dle tipo treeview para poder llamar a
            'after select y de esta forma actualizar en tiempo real la imagen del visualizador de insertar
            '(solapa indexer) documento.
            Dim a As New System.Windows.Forms.TreeViewEventArgs(TreeView.SelectedNode, TreeViewAction.Unknown)
            TreeView_AfterSelect(Nothing, a)


        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub



    Public Sub AddSpecialNodes()
        Try
            TreePorInsertar = New TreeNode
            TreeInsertados = New TreeNode
            TreePorInsertar.Text = "Documentos a Insertar"
            TreePorInsertar.Tag = "Parent"
            TreePorInsertar.ImageIndex = 25
            TreePorInsertar.SelectedImageIndex = 1
            TreePorInsertar.ForeColor = Color.FromArgb(76, 76, 76)
            TreeInsertados.Text = "Documentos Insertados"
            TreeInsertados.Tag = "Parent2"
            TreeInsertados.ImageIndex = 24
            TreeInsertados.SelectedImageIndex = 0
            TreeInsertados.ForeColor = Color.FromArgb(76, 76, 76)
            TreeView.Nodes.Add(TreePorInsertar)
            TreeView.Nodes.Add(TreeInsertados)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
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
                Dim ImageIndex As Int32 = Results_Business.GetIcon(File.Extension)
                ZNewResultNode.ImageIndex = ImageIndex
                ZNewResultNode.SelectedImageIndex = ImageIndex
            Catch ex As Exception
                ZNewResultNode.ImageIndex = 2
                ZNewResultNode.SelectedImageIndex = 2
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            ZNewResultNode.Text = NuevoFile.Name
            If IsNothing(TreePorInsertar) Then TreePorInsertar = New TreeNode
            ZNewResultNode.ForeColor = Color.FromArgb(76, 76, 76)
            TreePorInsertar.Nodes.Add(ZNewResultNode)
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

            ZNewResultNode.Text = NuevoFile.Name
            ZNewResultNode.ForeColor = Color.FromArgb(76, 76, 76)
            If IsNothing(TreeInsertados) = True Then TreeInsertados = New TreeNode
            TreeInsertados.Nodes.Add(ZNewResultNode)
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
            If TreePorInsertar.Nodes.Count > 0 Then
                TreeView.SelectedNode = TreePorInsertar.FirstNode   'Me.TreePorInsertar.Nodes(Me.TreePorInsertar.Nodes.Count - 1)               
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Event SeleccionadoParent(ByVal Estado As Boolean)
    Public Event PrevioSeleccionado(ByVal NewResult As NewResult)
    Public Event Seleccionado(ByValNewResult As NewResult, ByVal InsertingDoc As Boolean)
    Public Event Seleccionado_Insertado(ByVal NewResult As NewResult)
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
            If TreeView.SelectedNode.Tag.ToString = "Parent" OrElse TreeView.SelectedNode.Tag.ToString = "Parent2" Then
                'RaiseEvent SeleccionadoParent(True)
                Exit Sub
            End If

            RaiseEvent SeleccionadoParent(False)
            '[Sebastian 11-05-09] se agrego direccast para salvar el warning.
            Dim ZNode As ZNewResultNode = DirectCast(e.Node, ZNewResultNode)

            If TreeView.SelectedNode.Parent.Tag.ToString = "Parent2" Then
                RaiseEvent Seleccionado_Insertado(ZNode.ZambaCore)
            Else
                'aviso que estoy insertando y que obvie el permiso de reindex
                DocTypesBusiness.InsertingDoc(True)


                RaiseEvent Seleccionado(ZNode.ZambaCore, True)



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
            TreePorInsertar.Nodes.Clear()
            RaiseEvent EliminadosTodos1()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub EliminarTodos2()
        Try
            TreeInsertados.Nodes.Clear()
            RaiseEvent EliminadosTodos2()
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
                    If TreeView.SelectedNode.Tag.ToString = "Parent" Then
                        If MessageBox.Show("¿DESEA VACIAR LA LISTA DE DOCUMENTOS POR INDEXAR?", "", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            EliminarTodos1()
                        End If
                        Exit Sub
                    ElseIf TreeView.SelectedNode.Tag.ToString = "Parent2" Then
                        If MessageBox.Show("¿DESEA VACIAR LA LISTA DE DOCUMENTOS INDEXADOS?", "", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            EliminarTodos2()
                        End If
                        Exit Sub
                    End If

                    'If Me.TreeView.SelectedNode.Parent.Tag.ToString = "Parent2" Then Exit Sub

                    RaiseEvent Eliminado(DirectCast(TreeView.SelectedNode, ZNewResultNode))
                    TreeView.SelectedNode.Remove()
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
            Case "EXPANDIR"
                Try
                    TreeView.ExpandAll()
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
            Case "CONTRAER"
                Try
                    TreeView.CollapseAll()
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
        End Select
    End Sub

End Class
