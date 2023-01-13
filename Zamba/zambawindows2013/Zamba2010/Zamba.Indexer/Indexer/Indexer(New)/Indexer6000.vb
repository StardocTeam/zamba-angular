Imports Zamba.Core
Imports Zamba.Viewers
Imports System.Collections.Generic

Public Class Indexer6000
    Inherits ZControl
    Implements IViewerContainer
    Implements IDisposable


#Region "Variables"

    Private _replacedocument As Boolean = False

    Private _flagDelete As Boolean = True
    Private _flagInsertingAll As Boolean = False
    Private _browserFiles As BrowserFiles = Nothing
    Private _arbol As Arbol = Nothing
    Private _hashIndexados As New Hashtable()

    Private _docsInsertar As Int32
    Private _docsInsertados As Int32

    Public PublicDocTypeId As Int64
    Public PublicIndexs As New List(Of IIndex)
    Public NodoPorIndexar As New TreeNode
    Public NodoIndexados As New TreeNode


    Private _DocRelationId As Int32 = -1
    Private _disableDTCombo As Boolean = False
    Dim ViewerChanged As Boolean
    Private _relationatedDocument As Result
    Private WithEvents extVis As ExternalVisualizer
    Private _returnToPreviousTabPage As Boolean

    ''' <summary>
    ''' Propiedad que devuelve el path dependiendo si esta o no en applicationdata
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>


    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 11/03/09 - Created </history>
    Private ReadOnly Property _pathDirectory() As String
        Get
            Return Tools.EnvironmentUtil.GetTempDir("\IndexerTemp").FullName
        End Get
    End Property
    Public Property DocumentRelationId() As Int32
        Get
            Return _DocRelationId
        End Get
        Set(ByVal value As Int32)
            _DocRelationId = value
        End Set
    End Property
    Public Property ReturnToPreviousTabPage() As Boolean
        Get
            Return _returnToPreviousTabPage
        End Get
        Set(ByVal value As Boolean)
            _returnToPreviousTabPage = value
        End Set
    End Property
    Public Property RelationatedResult() As Result
        Get
            Return _relationatedDocument
        End Get
        Set(ByVal value As Result)
            _relationatedDocument = value
        End Set
    End Property

    Public Property Indexs() As List(Of IIndex)
        Get
            Return PublicIndexs
        End Get
        Set(ByVal Value As List(Of IIndex))
            PublicIndexs = Value
        End Set
    End Property


#End Region

#Region "Constructores"
    Private Sub New()

    End Sub
    Private ParentContainer As IParentTabControl

    Public Sub New(ByVal ParentContainer As IParentTabControl)
        MyBase.New()
        InitializeComponent()
        Me.ParentContainer = ParentContainer
        '_newDocument = newDocument
        LoadPanels()
    End Sub

    Public Sub New(ByVal asociatedDocument As Result, ByVal ParentContainer As IParentTabControl)
        MyBase.New()
        InitializeComponent()
        Me.ParentContainer = ParentContainer
        '_newDocument = String.Empty
        LoadPanels()
    End Sub
#End Region

#Region " Código generado por el Diseñador de Windows Forms "

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            Try
                If disposing Then
                    If Not (components Is Nothing) Then
                        components.Dispose()
                    End If

                    RemoveHandler MyBase.Load, AddressOf Indexer6000_Load

                    If _browserFiles IsNot Nothing Then
                        RemoveHandler _browserFiles.AddFiles, AddressOf AddFiles
                        RemoveHandler _browserFiles.ShowResult, AddressOf InsertVirtualResult
                        _browserFiles.Dispose()
                        _browserFiles = Nothing
                    End If
                    If _hashIndexados IsNot Nothing Then
                        'todo: liberar los objetos del hash
                        _hashIndexados.Clear()
                        _hashIndexados = Nothing
                    End If
                    If PublicIndexs IsNot Nothing Then
                        'todo: liberar los objetos del hash
                        PublicIndexs.Clear()
                        PublicIndexs = Nothing
                    End If
                    If _relationatedDocument IsNot Nothing Then
                        _relationatedDocument.Dispose()
                        _relationatedDocument = Nothing
                    End If
                    If extVis IsNot Nothing Then
                        extVis.Dispose()
                        extVis = Nothing
                    End If
                    If Panel1 IsNot Nothing Then
                        Panel1.Dispose()
                        Panel1 = Nothing
                    End If
                    If TabSelectDocuments IsNot Nothing Then
                        TabSelectDocuments.Dispose()
                        TabSelectDocuments = Nothing
                    End If
                    If TabIndexer IsNot Nothing Then
                        TabIndexer.Dispose()
                        TabIndexer = Nothing
                    End If
                    If TabMainControl IsNot Nothing Then
                        TabMainControl.Dispose()
                        TabMainControl = Nothing
                    End If
                    If SplitContainer1 IsNot Nothing Then
                        SplitContainer1.Dispose()
                        SplitContainer1 = Nothing
                    End If
                    If lblInsertar IsNot Nothing Then
                        lblInsertar.Dispose()
                        lblInsertar = Nothing
                    End If
                    If lblInsertados IsNot Nothing Then
                        lblInsertados.Dispose()
                        lblInsertados = Nothing
                    End If
                    If DesignSandBar IsNot Nothing Then
                        RemoveHandler DesignSandBar.ButtonClick, AddressOf DesignSandBar_ButtonClick
                        DesignSandBar.Close()
                        'DesignSandBar.Dispose()
                        DesignSandBar = Nothing
                    End If
                    If IBAtras IsNot Nothing Then
                        IBAtras.Dispose()
                        IBAtras = Nothing
                    End If
                    If IBSiguiente IsNot Nothing Then
                        IBSiguiente.Dispose()
                        IBSiguiente = Nothing
                    End If
                    If btnInsertar IsNot Nothing Then
                        btnInsertar.Dispose()
                        btnInsertar = Nothing
                    End If
                    If btnInsertarTodo IsNot Nothing Then
                        btnInsertarTodo.Dispose()
                        btnInsertarTodo = Nothing
                    End If
                    If chkRealizarOcr IsNot Nothing Then
                        chkRealizarOcr.Dispose()
                        chkRealizarOcr = Nothing
                    End If
                    If chkReplicar IsNot Nothing Then
                        chkReplicar.Dispose()
                        chkReplicar = Nothing
                    End If

                    If ButtonItem2 IsNot Nothing Then
                        ButtonItem2.Dispose()
                        ButtonItem2 = Nothing
                    End If

                    If TabViewers IsNot Nothing Then
                        TabViewers.Dispose()
                        TabViewers = Nothing
                    End If
                    If BtnAgregarBarcode IsNot Nothing Then
                        BtnAgregarBarcode.Dispose()
                        BtnAgregarBarcode = Nothing
                    End If

                    If _arbol IsNot Nothing Then
                        RemoveHandler _arbol.Seleccionado, AddressOf Seleccionado
                        RemoveHandler _arbol.Seleccionado_Insertado, AddressOf Seleccionado_Insertado
                        RemoveHandler _arbol.ShowBrowser, AddressOf ShowSelectDocumentsTab
                        RemoveHandler _arbol.Eliminado, AddressOf Eliminado
                        RemoveHandler _arbol.EliminadosTodos1, AddressOf EliminadosTodos1
                        RemoveHandler _arbol.EliminadosTodos2, AddressOf EliminadosTodos2
                        RemoveHandler _arbol.SeleccionadoParent, AddressOf SeleccionadoParent
                        RemoveHandler _arbol.SelectedIndexChanged, AddressOf SelectedIndexChanged

                        _arbol.Dispose()
                        _arbol = Nothing
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TabMainControl As System.Windows.Forms.TabControl
    Friend WithEvents TabSelectDocuments As System.Windows.Forms.TabPage
    Friend WithEvents TabIndexer As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer

    Friend WithEvents lblInsertar As ZLabel
    Friend WithEvents lblInsertados As ZLabel
    '  Friend WithEvents PanelimageList As System.Windows.Forms.ImageList
    Friend WithEvents DesignSandBar As TD.SandBar.ToolBar
    Friend WithEvents IBAtras As TD.SandBar.ButtonItem
    Friend WithEvents IBSiguiente As TD.SandBar.ButtonItem
    Friend WithEvents btnInsertar As TD.SandBar.ButtonItem
    Friend WithEvents btnInsertarTodo As TD.SandBar.ButtonItem
    Friend WithEvents chkRealizarOcr As TD.SandBar.ButtonItem
    Friend WithEvents chkReplicar As TD.SandBar.ButtonItem

    Friend WithEvents ButtonItem2 As TD.SandBar.ButtonItem

    Friend WithEvents TabViewers As System.Windows.Forms.TabControl
    Friend WithEvents BtnAgregarBarcode As TD.SandBar.ButtonItem
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Indexer6000))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DesignSandBar = New TD.SandBar.ToolBar()
        Me.IBAtras = New TD.SandBar.ButtonItem()
        Me.IBSiguiente = New TD.SandBar.ButtonItem()
        Me.btnInsertar = New TD.SandBar.ButtonItem()
        Me.btnInsertarTodo = New TD.SandBar.ButtonItem()
        Me.chkRealizarOcr = New TD.SandBar.ButtonItem()
        Me.BtnAgregarBarcode = New TD.SandBar.ButtonItem()
        Me.chkReplicar = New TD.SandBar.ButtonItem()
        Me.lblInsertar = New Zamba.AppBlock.ZLabel()
        Me.lblInsertados = New Zamba.AppBlock.ZLabel()
        Me.ButtonItem2 = New TD.SandBar.ButtonItem()
        Me.TabMainControl = New System.Windows.Forms.TabControl()
        Me.TabSelectDocuments = New System.Windows.Forms.TabPage()
        Me.TabIndexer = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TabViewers = New System.Windows.Forms.TabControl()
        Me.Panel1.SuspendLayout()
        Me.TabMainControl.SuspendLayout()
        Me.TabIndexer.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.DesignSandBar)
        Me.Panel1.Controls.Add(Me.lblInsertar)
        Me.Panel1.Controls.Add(Me.lblInsertados)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(3, 812)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1279, 75)
        Me.Panel1.TabIndex = 1
        '
        'DesignSandBar
        '
        Me.DesignSandBar.Buttons.AddRange(New TD.SandBar.ToolbarItemBase() {Me.IBAtras, Me.IBSiguiente, Me.btnInsertar, Me.btnInsertarTodo, Me.chkRealizarOcr, Me.BtnAgregarBarcode, Me.chkReplicar})
        Me.DesignSandBar.Closable = False
        Me.DesignSandBar.DockLine = 1
        Me.DesignSandBar.Guid = New System.Guid("1fd972d3-e337-4d0f-ab9e-b228d26929dd")
        Me.DesignSandBar.Location = New System.Drawing.Point(0, 0)
        Me.DesignSandBar.Name = "DesignSandBar"
        Me.DesignSandBar.Size = New System.Drawing.Size(1279, 42)
        Me.DesignSandBar.TabIndex = 155
        Me.DesignSandBar.Text = ""
        '
        'IBAtras
        '
        Me.IBAtras.Icon = CType(resources.GetObject("IBAtras.Icon"), System.Drawing.Icon)
        Me.IBAtras.IconSize = New System.Drawing.Size(32, 32)
        Me.IBAtras.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.Lowest
        Me.IBAtras.Padding.Left = 5
        Me.IBAtras.Padding.Right = 5
        Me.IBAtras.Tag = "ATRAS"
        Me.IBAtras.ToolTipText = "ANTERIOR"
        '
        'IBSiguiente
        '
        Me.IBSiguiente.Icon = CType(resources.GetObject("IBSiguiente.Icon"), System.Drawing.Icon)
        Me.IBSiguiente.IconSize = New System.Drawing.Size(32, 32)
        Me.IBSiguiente.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.Lowest
        Me.IBSiguiente.Padding.Left = 5
        Me.IBSiguiente.Padding.Right = 5
        Me.IBSiguiente.Tag = "ADELANTE"
        Me.IBSiguiente.ToolTipText = "SIGUIENTE"
        '
        'btnInsertar
        '
        Me.btnInsertar.BeginGroup = True
        Me.btnInsertar.Icon = CType(resources.GetObject("btnInsertar.Icon"), System.Drawing.Icon)
        Me.btnInsertar.IconSize = New System.Drawing.Size(32, 32)
        Me.btnInsertar.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.Highest
        Me.btnInsertar.Padding.Left = 5
        Me.btnInsertar.Padding.Right = 5
        Me.btnInsertar.Tag = "INSERTAR"
        Me.btnInsertar.Text = "INSERTAR"
        Me.btnInsertar.ToolTipText = "INSERTAR"
        '
        'btnInsertarTodo
        '
        Me.btnInsertarTodo.Icon = CType(resources.GetObject("btnInsertarTodo.Icon"), System.Drawing.Icon)
        Me.btnInsertarTodo.IconSize = New System.Drawing.Size(32, 32)
        Me.btnInsertarTodo.Padding.Left = 5
        Me.btnInsertarTodo.Padding.Right = 5
        Me.btnInsertarTodo.Tag = "INSERTARTODO"
        Me.btnInsertarTodo.Text = "INSERTAR TODO"
        Me.btnInsertarTodo.ToolTipText = "INSERTAR TODO"
        '
        'chkRealizarOcr
        '
        Me.chkRealizarOcr.BeginGroup = True
        Me.chkRealizarOcr.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.High
        Me.chkRealizarOcr.Padding.Left = 5
        Me.chkRealizarOcr.Padding.Right = 5
        Me.chkRealizarOcr.Tag = "REALIZAROCR"
        Me.chkRealizarOcr.Text = "REALIZAR OCR"
        Me.chkRealizarOcr.ToolTipText = "REALIZAR OCR"
        '
        'BtnAgregarBarcode
        '
        Me.BtnAgregarBarcode.BeginGroup = True
        Me.BtnAgregarBarcode.Tag = "BARCODE"
        Me.BtnAgregarBarcode.Text = "BARCODE"
        Me.BtnAgregarBarcode.ToolTipText = "BARCODE"
        '
        'chkReplicar
        '
        Me.chkReplicar.BeginGroup = True
        Me.chkReplicar.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.High
        Me.chkReplicar.Padding.Left = 5
        Me.chkReplicar.Padding.Right = 5
        Me.chkReplicar.Tag = "REPLICAR"
        Me.chkReplicar.Text = "REPLICAR"
        Me.chkReplicar.ToolTipText = "REPLICAR"
        '
        'lblInsertar
        '
        Me.lblInsertar.AutoSize = True
        Me.lblInsertar.BackColor = System.Drawing.Color.Transparent
        Me.lblInsertar.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblInsertar.FontSize = 9.75!
        Me.lblInsertar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblInsertar.Location = New System.Drawing.Point(245, 50)
        Me.lblInsertar.Name = "lblInsertar"
        Me.lblInsertar.Size = New System.Drawing.Size(214, 16)
        Me.lblInsertar.TabIndex = 7
        Me.lblInsertar.Text = "0 DOCUMENTOS POR INSERTAR"
        Me.lblInsertar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblInsertados
        '
        Me.lblInsertados.AutoSize = True
        Me.lblInsertados.BackColor = System.Drawing.Color.Transparent
        Me.lblInsertados.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblInsertados.FontSize = 9.75!
        Me.lblInsertados.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblInsertados.Location = New System.Drawing.Point(10, 50)
        Me.lblInsertados.Name = "lblInsertados"
        Me.lblInsertados.Size = New System.Drawing.Size(203, 16)
        Me.lblInsertados.TabIndex = 6
        Me.lblInsertados.Text = "0 DOCUMENTOS INSERTADOS"
        Me.lblInsertados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ButtonItem2
        '
        Me.ButtonItem2.Icon = CType(resources.GetObject("ButtonItem2.Icon"), System.Drawing.Icon)
        Me.ButtonItem2.IconSize = New System.Drawing.Size(32, 32)
        Me.ButtonItem2.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.Lowest
        Me.ButtonItem2.Padding.Left = 5
        Me.ButtonItem2.Padding.Right = 5
        Me.ButtonItem2.Tag = "ADELANTE"
        Me.ButtonItem2.ToolTipText = "SIGUIENTE"
        '
        'TabMainControl
        '
        Me.TabMainControl.Controls.Add(Me.TabSelectDocuments)
        Me.TabMainControl.Controls.Add(Me.TabIndexer)
        Me.TabMainControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabMainControl.Location = New System.Drawing.Point(2, 2)
        Me.TabMainControl.Name = "TabMainControl"
        Me.TabMainControl.SelectedIndex = 0
        Me.TabMainControl.Size = New System.Drawing.Size(1293, 917)
        Me.TabMainControl.TabIndex = 2
        '
        'TabSelectDocuments
        '
        Me.TabSelectDocuments.Location = New System.Drawing.Point(4, 23)
        Me.TabSelectDocuments.Name = "TabSelectDocuments"
        Me.TabSelectDocuments.Padding = New System.Windows.Forms.Padding(3)
        Me.TabSelectDocuments.Size = New System.Drawing.Size(1285, 890)
        Me.TabSelectDocuments.TabIndex = 0
        Me.TabSelectDocuments.Text = "Seleccionar Archivos"
        Me.TabSelectDocuments.UseVisualStyleBackColor = True
        '
        'TabIndexer
        '
        Me.TabIndexer.Controls.Add(Me.SplitContainer1)
        Me.TabIndexer.Controls.Add(Me.Panel1)
        Me.TabIndexer.Location = New System.Drawing.Point(4, 23)
        Me.TabIndexer.Name = "TabIndexer"
        Me.TabIndexer.Padding = New System.Windows.Forms.Padding(3)
        Me.TabIndexer.Size = New System.Drawing.Size(1285, 890)
        Me.TabIndexer.TabIndex = 1
        Me.TabIndexer.Text = "Indexar"
        Me.TabIndexer.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TabViewers)
        Me.SplitContainer1.Size = New System.Drawing.Size(1279, 809)
        Me.SplitContainer1.SplitterDistance = 319
        Me.SplitContainer1.TabIndex = 2
        '
        'TabViewers
        '
        Me.TabViewers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabViewers.Location = New System.Drawing.Point(0, 0)
        Me.TabViewers.Name = "TabViewers"
        Me.TabViewers.SelectedIndex = 0
        Me.TabViewers.Size = New System.Drawing.Size(956, 809)
        Me.TabViewers.TabIndex = 0
        '
        'Indexer6000
        '
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.TabMainControl)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(143, 0)
        Me.Name = "Indexer6000"
        Me.Padding = New System.Windows.Forms.Padding(2)
        Me.Size = New System.Drawing.Size(1297, 921)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabMainControl.ResumeLayout(False)
        Me.TabIndexer.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region



    ''' <summary>
    ''' [Sebastian 12-05-09] Obtiene el primer doctype id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetFirstDocType() As Int64
        If _arbol.DocTypes.Count <= 0 Then
            Return Nothing
        Else
            Return DirectCast(_arbol.DocTypes(0), DocType).ID
        End If
    End Function

    Private Function IndexDocument(ByRef NewResult As NewResult, ByVal DisableAutomaticVersion As Boolean) As InsertResult
        Dim InsertResult As InsertResult = InsertResult.NoInsertado
        Try
            If Not IsNothing(NewResult) Then
                Try
                    If TabViewers.TabCount > 0 AndAlso IsNothing(TabViewers.SelectedTab) = False Then
                        '[Sebastian 12-05-09] se agrego DirecCast para salvar el warning de casteo.
                        Dim NewUcViewer As UCDocumentViewer2 = DirectCast(TabViewers.SelectedTab, UCDocumentViewer2)
                        NewUcViewer.CloseDocument(False)
                        'Me.TabViewers.TabPages.Remove(Me.TabViewers.SelectedTab)
                        'NewUcViewer.Dispose()
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try
                    '[ecanete]8-9-2009- Se comenta y se implementa la funcionalidad de autocomplete en el método insert en el
                    'branch branch existente
                    Dim dsIndexsToIncrement As DataSet = DocTypesBusiness.GetIndexsProperties(NewResult.DocTypeId)
                    Dim IncrementedValue As Int64 = 0

                    For Each CurrentRow As DataRow In dsIndexsToIncrement.Tables(0).Rows
                        If IsDBNull(CurrentRow("autoincremental")) = False AndAlso Int64.Parse(CurrentRow("Autoincremental").ToString) = 1 Then
                            For Each CurrentIndex As Index In NewResult.Indexs
                                If String.Compare(CurrentRow("Index_Name").ToString.Trim, CurrentIndex.Name.Trim) = 0 Then
                                    If CurrentIndex.Data.Trim() = String.Empty Then
                                        IncrementedValue = IndexsBusiness.SelectMaxIndexValue(CurrentIndex.ID, NewResult.DocTypeId)
                                        CurrentIndex.Data = IncrementedValue.ToString
                                        CurrentIndex.DataTemp = IncrementedValue.ToString
                                    End If

                                End If
                            Next
                            '[Sebastian 04-11-2009] se carga el valor por defecto si lo tiene. Se hizo para formularios
                        ElseIf IsDBNull(CurrentRow("DefaultValue")) = False _
                                AndAlso String.Compare(CurrentRow("DefaultValue").ToString, String.Empty) <> 0 Then
                            For Each CurrentIndex As Index In NewResult.Indexs
                                If String.Compare(CurrentRow("Index_Name").ToString.Trim, CurrentIndex.Name.Trim) = 0 Then
                                    If CurrentIndex.Data.Trim() = String.Empty Then
                                        Dim Defaultvalue As String = CurrentRow("DefaultValue").ToString.Trim
                                        Defaultvalue = WFRuleParent.ReconocerVariablesValuesSoloTexto(Defaultvalue)
                                        Defaultvalue = TextoInteligente.ReconocerCodigo(Defaultvalue, Nothing)
                                        CurrentIndex.Data = Defaultvalue
                                        CurrentIndex.DataTemp = Defaultvalue
                                    End If
                                    Exit For
                                End If
                            Next
                        End If
                    Next

                    Dim openTask As Boolean = True
                    Dim OpenDocument As Boolean = False

                    '[Ezequiel] Se valida codigo ya que el furmulario no se debe crear de una
                    ' Se debe crear cuando el usuario presione el boton guardar
                    If Not NewResult.ISVIRTUAL Then

                        If RightsBusiness.GetUserRights(ObjectTypes.ModuleWorkFlow, RightsType.Use) Then
                            If _dontOpenTaskIfAsociatedToWF Then
                                openTask = False
                            End If
                        Else
                            openTask = False
                        End If

                        If _ParentTaskId <> 0 AndAlso _dontOpenTaskIfAsociatedToWF = False Then
                            OpenDocument = True
                        End If

                        If chkReplicar.Checked And Not IsNothing(NewResult.Doc_File) Then
                            'Si el archivo ya existe lo redirecciono y lo guardo
                            InsertResult = Results_Business.InsertDocument(NewResult, False, False, False, False, False, True, True, False, openTask, OpenDocument, ParentTaskId)
                        ElseIf chkReplicar.Checked Then
                            'Si el archivo no existe lo creo y lo guardo
                            InsertResult = Results_Business.InsertDocument(NewResult, False, False, False, False, False, True, False, False, openTask, OpenDocument, ParentTaskId)
                        Else
                            'Simplemente lo inserto
                            InsertResult = Results_Business.InsertDocument(NewResult, _flagDelete, False, _replacedocument, True, NewResult.ISVIRTUAL, False, False, False, openTask, OpenDocument, ParentTaskId)
                        End If
                    ElseIf NewResult.ISVIRTUAL = True Then
                        'Para cuando inserto un formVirtual, que vaya de una a la solapa de insertado
                        InsertResult = InsertResult.Insertado
                    End If
                Catch ex As Exception
                    InsertResult = InsertResult.NoInsertado
                    ZClass.raiseerror(ex)
                    MessageBox.Show(ex.Message, "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                Try
                    'If BtnNOReplicar.Visible Then
                    _hashIndexados.Remove(NewResult.ID)
                    'NewResult.DocType.IsReindex = False
                    FillHashIndexados(NewResult)
                    'End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ha ocurrido un error en la insercion", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return InsertResult
    End Function


    Private Function ChangeIndexs(ByRef NewResult As NewResult, KeepLocalData As Boolean) As NewResult
        If Me.keepIndexData = False Then
            For Each Index2 As Index In NewResult.Indexs
                Index2.Data = String.Empty
                Index2.DataTemp = String.Empty
                Index2.dataDescription = String.Empty
                Index2.dataDescriptionTemp = String.Empty
            Next
        End If
        If Indexs IsNot Nothing Then
            For Each Index1 As Index In Indexs
                For Each Index2 As Index In NewResult.Indexs
                    If Index2.ID = Index1.ID Then
                        If Index2.DataTemp = "" AndAlso Index2.dataDescriptionTemp = "" Then

                            Index2.DataTemp = Index1.Data
                            Index2.Data = Index1.Data
                            Index2.dataDescription = Index1.dataDescription
                            Index2.dataDescriptionTemp = Index1.dataDescription
                        End If
                        Exit For
                    End If
                Next
            Next
        End If

        If (KeepLocalData = False) Then
            'Se asignan los indices cargados hasta ese momento al newresult
            Dim ArrayIndices As List(Of IIndex) = _arbol.UcIndexs.GetIndexs
            For Each Index1 As Index In ArrayIndices
                For Each Index2 As Index In NewResult.Indexs
                    If Index2.ID = Index1.ID Then
                        If Index2.DataTemp = "" AndAlso Index2.dataDescriptionTemp = "" Then
                            Index2.Data = Index1.Data
                            Index2.DataTemp = Index1.Data
                            Index2.dataDescription = Index1.dataDescription
                            Index2.dataDescriptionTemp = Index1.dataDescription
                        End If
                        Exit For
                    End If
                Next
            Next
        End If

        _arbol.MostrarIndices(NewResult, True)
        Return NewResult
    End Function


    Public Sub InicializaIndexer(ByVal docType As DocType, ByVal indexs As List(Of IIndex))
        Try
            Visible = False
            btnInsertar.Visible = False
            btnInsertarTodo.Visible = False
            Me.Indexs = indexs

            ' selecciona la entidad, solo si existe en el combo de seleccion
            If docType Is Nothing Then
                _arbol.SetDocTypeId(0)
            Else
                _arbol.SetDocTypeId(CInt(docType.ID))
            End If
            TabMainControl.SelectedTab = TabSelectDocuments
            CheckInsertAllVisibility()
        Finally
            Visible = True
        End Try
    End Sub
    Public Sub InicializaIndexer(ByVal docType As DocType, ByVal indexs As List(Of IIndex), ByVal TemplateId As Int32)
        InicializaIndexer(docType, indexs)
        LoadTemplate(TemplateId, docType)
    End Sub
    Public Sub InicializaIndexerWithNewDocument(ByVal docType As DocType, ByVal indexs As List(Of IIndex), ByVal TypeId As Int32)
        InicializaIndexer(docType, indexs)
        If TypeId <> 0 Then
            LoadNewDocument(TypeId, docType)
        End If
    End Sub


    Private Sub LoadTemplate(ByVal TemplateId As Int32, RelatedDocType As IDocType)
        Try
            Dim TemplatePath As New ArrayList
            Dim Templates As New TemplatesBusiness
            TemplatePath.Add(Templates.ObtainTemplatePath(TemplateId))
            AddFiles(TemplatePath, RelatedDocType)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LoadNewDocument(ByVal typeId As Int32, RelatedDocType As IDocType)
        Dim _browserFiles As New BrowserFiles
        Select Case typeId
            Case OfficeTypes.Word
                Me._browserFiles.NuevoWord(RelatedDocType)
            Case OfficeTypes.Excel
                Me._browserFiles.NuevoExcel(RelatedDocType)
            Case OfficeTypes.PowerPoint
                Me._browserFiles.NuevoPowerpoint(RelatedDocType)
        End Select
    End Sub

    Private Sub InsertVirtualResult(ByRef Result As NewResult)

        If Not IsNothing(Result) Then
            If Indexs IsNot Nothing Then
                For Each Index1 As Index In Indexs
                    For Each Index2 As Index In Result.Indexs
                        If Index2.ID = Index1.ID Then
                            Index2.DataTemp = Index1.Data
                            Index2.Data = Index1.Data
                            Index2.dataDescription = Index1.dataDescription
                            Index2.dataDescriptionTemp = Index1.dataDescription
                            Exit For
                        End If
                    Next
                Next
            End If

            Dim insertresult As InsertResult
            insertresult = IndexDocument(Result, True)

            If insertresult = InsertResult.Insertado Then

                Dim hs As New Hashtable
                hs.Add("DontOpenTaskIfAsociatedToWF", _dontOpenTaskIfAsociatedToWF)
                Zamba.Core.Result.HandleModule(ResultActions.ShowNewForm, Result, hs)

            End If

        End If

    End Sub

    Public Sub AddFiles(ByVal Files As ArrayList, ByVal RelatedDocType As DocType)
        TabMainControl.SelectTab(TabIndexer)

        For Each File As String In Files
            Select Case File
                Case "NuevoWord"
                    _browserFiles.NuevoWord(Nothing)
                Case "NuevoExcel"
                    _browserFiles.NuevoExcel(Nothing)
                Case "NuevoPowerPoint"
                    _browserFiles.NuevoPowerpoint(Nothing)
            End Select
            If Not String.IsNullOrEmpty(File.Trim) AndAlso IO.File.Exists(File) Then
                Dim RB As New Results_Business
                Dim NewResult As NewResult = RB.GetNewNewResult(_arbol.GetSelectedDocType, CInt(UserBusiness.Rights.CurrentUser.ID), File)
                If KeepAsignedDocId > 0 Then
                    NewResult.ID = KeepAsignedDocId
                    KeepAsignedDocId = 0
                Else
                    NewResult.ID = ToolsBusiness.GetNewID(IdTypes.DOCID)
                End If
                _arbol.CargarFiles(NewResult)
            End If
        Next
        _arbol.TreePorInsertar.ExpandAll()
        _docsInsertar += Files.Count
        _arbol.SelectFirstFile()

        If _docsInsertar = 1 Then
            lblInsertar.Text = "1 DOCUMENTO POR INSERTAR"
        ElseIf _docsInsertar > 1 Then
            lblInsertar.Text = _docsInsertar.ToString & " DOCUMENTOS POR INSERTAR"
        End If
        CheckInsertAllVisibility()
    End Sub


    Public Shared Function SaveTextToFile(ByVal strData As String, ByVal FullPath As String, Optional ByVal ErrInfo As String = "") As Boolean
        'Dim Contents As String
        Dim bAns As Boolean = False
        Dim objReader As IO.StreamWriter = Nothing

        Try
            objReader = New IO.StreamWriter(FullPath)
            objReader.Write(strData)
            objReader.Close()
            bAns = True
        Catch Ex As Exception
            'ErrInfo = Ex.Message

        End Try

        Return bAns
    End Function

    Private Shared Function GenerateOCR(ByVal Path As String) As String
        'Dim OCR As New ZOCRLib.ZOCRLib
        'Return OCR.OCR(Path)
        Return Path
    End Function

    Friend Class RequieredFieldException
        Inherits Exception

        Public Sub New(ByVal indexs As ArrayList)
            MyBase.New()
            Me.indexs = indexs
        End Sub

        Private indexs As ArrayList
        Public Property Indexs1() As ArrayList
            Get
                Return indexs
            End Get
            Set(ByVal value As ArrayList)
                indexs = value
            End Set
        End Property

    End Class

    Private Function Insertar(ByRef NewResult As NewResult) As InsertResult

        Dim insertresult As InsertResult = InsertResult.NoInsertado

        Try
            If (NewResult.DocType Is Nothing AndAlso NewResult.DocTypeId > 0) Then
                NewResult.DocType = DocTypeBusinessExt.GetDocTypeByID(NewResult.DocTypeId)
                NewResult.DocTypeId = NewResult.DocTypeId
            End If
            If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, CInt(NewResult.DocTypeId)) Then
                'For Each row As DataRow In DocTypesBusiness.GetIndexsPropertiesWithIndexType(NewResult.DocType.ID).Tables(0).Rows
                'If row("mustcomplete") = 0 Then
                If Results_Business.ValidateIndexDataFromDoctype(NewResult) <> False Then
                    If Results_Business.ValidateIndexData(NewResult) = False Then
                        MessageBox.Show("Hay atributos requeridos incompletos", "Zamba", MessageBoxButtons.OK)
                        Exit Function
                    End If
                Else
                    MessageBox.Show("Hay atributos requeridos incompletos", "Zamba", MessageBoxButtons.OK)
                    Exit Function
                End If
                ' Next

            End If

            'Die: Agregado para guardar los documentos antes de insertar
            For Each Document As UCDocumentViewer2 In TabViewers.TabPages
                If Document.Result.CurrentFormID <= 0 Then
                    Document.SaveDocument()
                End If
            Next

            'Graba el documento en la base
            If chkReplicar.Checked Then

                insertresult = IndexDocument(NewResult, True)
                If insertresult = InsertResult.Insertado Then
                    'VERIFICA SI EXISTE UNA RELACION ENTRE DOCUMENTOS
                    If _DocRelationId <> -1 Then Results_Business.InsertDocumentRelation(RelationatedResult.ID, NewResult.ID, DocumentRelationId)
                    ActualizarControles(DirectCast(_hashIndexados(NewResult.ID), NewResult), True)
                End If


            Else
                insertresult = IndexDocument(NewResult, True)

                If insertresult = InsertResult.Insertado Then
                    'VERIFICA SI EXISTE UNA RELACION ENTRE DOCUMENTOS
                    If _DocRelationId <> -1 Then Results_Business.InsertDocumentRelation(RelationatedResult.ID, NewResult.ID, DocumentRelationId)
                    Try

                        If chkRealizarOcr.Checked Then

                            'OCR
                            Dim TextoOCR As String = GenerateOCR(NewResult.NewFile)
                            If IO.File.Exists(_pathDirectory & "\TempOCR.txt") = True Then IO.File.Delete(_pathDirectory & "\TempOCR.txt")
                            SaveTextToFile(TextoOCR, _pathDirectory & "\TempOCR.txt", "")
                        End If
                    Catch ex As Exception
                        Dim sb As New System.Text.StringBuilder
                        sb.Append("Excepción al realizar OCR al documento: ")
                        sb.Append(NewResult.AutoName)
                        sb.Append(". Error: ")
                        sb.Append(ex.Message)

                        Dim exn As New Exception(sb.ToString)
                        ZClass.raiseerror(exn)
                        sb = Nothing
                    End Try

                    '     If _flagInsertingAll = False Then
                    ActualizarControles(DirectCast(_hashIndexados(NewResult.ID), NewResult), False)

                ElseIf insertresult = InsertResult.NoInsertado Then    'si falla
                    MessageBox.Show("Error al insertar el documento", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ElseIf insertresult = Core.InsertResult.ErrorIndicesIncompletos Then
                    MessageBox.Show("Hay atributos obligatorios sin completar", "Atencion", MessageBoxButtons.OK)
                ElseIf insertresult = Core.InsertResult.ErrorIndicesInvalidos Then
                    MessageBox.Show("Hay atributos con datos invalidos", "Atencion", MessageBoxButtons.OK)
                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return insertresult

    End Function

    Private Sub ActualizarControles(ByVal NewResult As NewResult, ByVal closeTab As Boolean)
        If Not IsNothing(NewResult) Then
            If Not chkReplicar.Checked Then
                _arbol.TreeView.SelectedNode.Remove()
            End If
            NewResult.FlagIndexEdited = True
            Dim NuevoFile As IO.FileInfo
            If Not NewResult.NewFile Is Nothing Then
                NuevoFile = New IO.FileInfo(NewResult.NewFile)
                'NuevoFile = New IO.FileInfo(NewResult.File)
            Else
                NuevoFile = New IO.FileInfo(NewResult.File)
            End If
            Dim ZNewResultNode As New ZNewResultNode(NewResult)
            ZNewResultNode.Text = NuevoFile.Name
            Try
                Dim LoadedIcono As Icon = GetFileIcon(NuevoFile.FullName)
                Dim img As Bitmap = LoadedIcono.ToBitmap()
                img.MakeTransparent(img.GetPixel(0, 0))
                _arbol.IL.ZIconList.Images.Add(img)
                Dim ImageIndex As Integer = _arbol.IL.ZIconList.Images.Count - 1
                ZNewResultNode.ImageIndex = ImageIndex
                ZNewResultNode.SelectedImageIndex = ImageIndex
            Catch ex As Exception
                ZNewResultNode.ImageIndex = 2
                ZNewResultNode.SelectedImageIndex = 2
                ZClass.raiseerror(ex)
            End Try

            _arbol.TreeInsertados.Nodes.Add(ZNewResultNode)

            If Not chkReplicar.Checked Then
                _docsInsertar -= 1
            End If
            _docsInsertados += 1
            lblInsertar.Text = _docsInsertar.ToString & " DOCUMENTOS POR INSERTAR"
            lblInsertados.Text = _docsInsertados.ToString & " DOCUMENTOS INSERTADOS"
            Try
                If Not IsNothing(TabViewers.SelectedTab) AndAlso closeTab Then
                    DirectCast(TabViewers.SelectedTab, UCDocumentViewer2).CloseDocument()
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
    End Sub

    Private Sub SelectedIndexChanged(ByVal DocType As IDocType)
        Try
            Dim NewResult As NewResult = GetActiveDocumentId()

            NewResult.DocType = DocType
            NewResult.DocType.IsReadOnly = False
            NewResult.CurrentFormID = -1
            Try
                Dim RB As New Results_Business
                RB.LoadVolume(NewResult)
            Catch ex As Exception
                ZClass.raiseerror(ex)
                MessageBox.Show("El volumen asignado no se encuentra disponible", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            Results_Business.LoadIndexs(NewResult)
            CheckInsertAllVisibility()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub AddResults1(ByVal ArrayResults As ArrayList)
        Try
            'AGREGAR LOS RESULTS AL HASH DE POR INSERTAR
            Dim i As Int32
            For i = 0 To ArrayResults.Count - 1
                _arbol.CargarFiles(DirectCast(ArrayResults(i), NewResult))
                _docsInsertar += 1
                lblInsertar.Text = _docsInsertar.ToString & " DOCUMENTOS POR INSERTAR"
            Next
            _arbol.TreePorInsertar.ExpandAll()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub AddResults2(ByVal ArrayResults As ArrayList)
        Try
            'AGREGAR LOS RESULTS AL HASH DE INSERTADOS
            Dim i As Int32
            For i = 0 To ArrayResults.Count - 1
                Try
                    FillHashIndexados(DirectCast(ArrayResults(i), NewResult))
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                _arbol.CargarFiles2(DirectCast(ArrayResults(i), NewResult))
                _docsInsertados += 1
                lblInsertados.Text = _docsInsertados.ToString & " DOCUMENTOS INSERTADOS"
            Next

            _arbol.TreeInsertados.ExpandAll()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub InsertarClick()

        Dim resul As InsertResult

        Try
            For Each Document As UCDocumentViewer2 In TabViewers.TabPages
                If Document.Result.CurrentFormID <= 0 Then
                    Document.SaveDocument()
                End If
            Next

            'Me.btnInsertarTodo.Visible = False
            Dim NewResult As NewResult = GetActiveDocumentId()

            Try
                resul = Insertar(NewResult)
                _disableDTCombo = False
            Catch ex As RequieredFieldException
                ' MsgBox("Debe completar los Atributos obligatorios")
                Exit Sub
            End Try

            If resul = InsertResult.ErrorIndicesIncompletos OrElse resul = InsertResult.ErrorIndicesInvalidos Then
                Exit Sub
            End If

            If _arbol.TreePorInsertar.GetNodeCount(True) > 0 Then
                _arbol.TreeView.SelectedNode = _arbol.TreePorInsertar.Nodes(0)

                '            Me.ControlTreeview.TreeView.SelectedNode = Me.ControlTreeview.TreePorInsertar.Nodes(0)
                Try
                    'SELECCIONA UN NUEVO RESULT
                    Dim Znode As ZNewResultNode = DirectCast(_arbol.TreeView.SelectedNode, ZNewResultNode)
                    Seleccionado(Znode.ZambaCore, True)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Else
                If ReturnToPreviousTabPage Then
                    ParentContainer.comeFrom = Parent.Name
                    ParentContainer.ReturnToPreviusTabPage()
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            CheckInsertAllVisibility()
        End Try
    End Sub

    ''' <summary>
    ''' [Sebastian] 03-07-09 MODIFIED se corrigio la exception que lanzaba en insertar
    ''' al maximizar un form y hacer clic en el boton cerrar
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="ClosedFromCross"></param>
    ''' <remarks></remarks>
    Private Sub CambiarDock(ByVal Sender As TabPage, Optional ByVal ClosedFromCross As Boolean = False, Optional ByVal IsMaximize As Boolean = False)
        'Private Sub CambiarDock(ByVal Sender As TabPage)
        Try
            If ClosedFromCross = False Then
                If Not IsNothing(extVis) Then
                    If extVis.Controls.Contains(Sender.Parent) Then
                        extVis.Close()
                    ElseIf IsNothing(Sender.Parent) = True Then
                        extVis.Close()
                    Else
                        extVis = New ExternalVisualizer(DirectCast(Sender.Parent, TabControl))
                        extVis.Show()
                    End If
                Else
                    extVis = New ExternalVisualizer(DirectCast(Sender.Parent, TabControl))
                    extVis.Show()
                End If

            Else
                extVis = New ExternalVisualizer(DirectCast(Sender.Parent, TabControl))
                extVis.Show()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub



    Private Sub MostrarBarcode(ByRef Result As NewResult)
        Try
            If Not IsNothing(Result) Then
                If Result.IsWord AndAlso Result.ISVIRTUAL = False Then
                    'Dim ucviewer As  UCDocumentViewer2
                    Dim ucviewer As UCDocumentViewer2 = Nothing
                    For Each Content As UCDocumentViewer2 In TabViewers.TabPages
                        If TypeOf Content Is UCDocumentViewer2 Then
                            If DirectCast(Content, UCDocumentViewer2).Result.ID = Result.ID Then
                                ucviewer = Content
                                TabViewers.SelectTab(Content)
                                Exit For
                            End If
                        End If
                    Next
                    If Not IsNothing(ucviewer) Then
                        Dim inputbox As New BarcodeViewer(Result)
                        'Ask for the code and the alignment
                        inputbox.ShowDialog()
                        If Not inputbox.texto = "" Then
                            Dim Height As Int32 = Int32.Parse(UserPreferences.getValue("BarCodeHeight", UPSections.UserPreferences, "50"))
                            'Se comento la linea de ancho del codigo de barras para no modificar el estandar del mismo, ya 
                            'que el espacio entre barras afecta su lectura posterior.
                            'Dim Width As Int32 = Int32.Parse(UserPreferences.getValue("BarCodeWidth", Sections.UserPreferences, "424"))
                            Zamba.Office.OfficeInterop.BarcodeWord(ucviewer.traerDoc(), inputbox.texto, True, inputbox.Alignment, Height)
                        End If
                        inputbox.Dispose()
                        inputbox = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub SeleccionadoParent(ByVal Estado As Boolean)
        If Estado Then
            _arbol.cboDocType.Enabled = False
            _arbol.Enabled = False
        Else
            _arbol.cboDocType.Enabled = True
            _arbol.Enabled = True
        End If
        If _disableDTCombo Then _arbol.cboDocType.Enabled = False
    End Sub


    Private Sub Seleccionado_Insertado(NewResult As NewResult)
        Try
            SuspendLayout()

            RemoveHandler TabViewers.TabIndexChanged, AddressOf TabViewersIndexChanged

            If _flagInsertingAll = True Then Exit Sub
            _arbol.MostrarIndices(NewResult, True)

            'SE FIJA SI ESTA EN PANTALLA
            For Each Content As UCDocumentViewer2 In TabViewers.TabPages
                If TypeOf Content Is UCDocumentViewer2 Then
                    If Content.Result.ID = NewResult.ID Then
                        TabViewers.SelectedTab = Content
                        Exit Sub
                    End If
                End If
            Next

            'SI NO CREA UN CONTENT NUEVO Y LO MUESTRA
            Dim UcViewer As New UCDocumentViewer2(NewResult, False)
            UcViewer.Text = NewResult.Name
            UcViewer.Tag = NewResult.Name
            TabViewers.TabPages.Add(UcViewer)
            TabViewers.SelectTab(TabViewers.TabPages.Count - 1)

            Dim docInsertPreview As Boolean = Boolean.Parse(UserPreferences.getValue("DocInsertPreview", UPSections.InsertPreferences, "True"))
            If docInsertPreview Then
                UcViewer.ShowDocument(True, False, False, False, False)
                RemoveHandler UcViewer.CambiarDock, AddressOf CambiarDock
                AddHandler UcViewer.CambiarDock, AddressOf CambiarDock
                RemoveHandler UcViewer.ShowOriginal, AddressOf ShowOriginal
                AddHandler UcViewer.ShowOriginal, AddressOf ShowOriginal
            Else
                UcViewer.ShowNonPreviewMessage()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            AddHandler TabViewers.TabIndexChanged, AddressOf TabViewersIndexChanged
            ResumeLayout()
        End Try
    End Sub

    ''' <summary>
    ''' Cuando se selecciona un item, carga los atributos y la visualizacion del mismo
    ''' </summary>
    ''' <param name="SelectedId"></param>
    ''' <history>   
    '''     Marcelo modified 05/02/2009
    '''     Javier  modified 25/11/2010           
    ''' </history>
    ''' <remarks></remarks>
    Private Sub Seleccionado(ByVal NewResult As NewResult, ByVal InsertingDoc As Boolean)
        Try
            SuspendLayout()
            RemoveHandler TabViewers.TabIndexChanged, AddressOf TabViewersIndexChanged
            If _flagInsertingAll = True Then Exit Sub

            If Not IsNothing(NewResult) Then
                Dim RB As New Results_Business
                RB.ChangeDocTypeToNewResult(_arbol.GetSelectedDocType, NewResult)

                NewResult = ChangeIndexs(NewResult, ZOptBusiness.GetValueOrDefault("keepIndexsDataOnNewInsert", False))


                'SE FIJA SI ESTA EN PANTALLA
                For Each Content As UCDocumentViewer2 In TabViewers.TabPages
                    If TypeOf Content Is UCDocumentViewer2 Then
                        Dim contentString As String = TryCast(Content, UCDocumentViewer2).Result.ID.ToString
                        Dim Forms() As ZwebForm = FormBusiness.GetShowAndEditForms(Int32.Parse(contentString))

                        'Se fija si la solapa ya se encuentra seleccionada
                        If DirectCast(Content, UCDocumentViewer2).Result.ID = NewResult.ID And IsNothing(Forms) = True _
                            And ViewerChanged = False And Not DirectCast(Content, UCDocumentViewer2).FormBrowser Is Nothing Then

                            TabViewers.TabPages.Remove(TabViewers.SelectedTab)
                            ViewerChanged = True
                        ElseIf DirectCast(Content, UCDocumentViewer2).Result.ID = NewResult.ID And IsNothing(Forms) = True _
                        And ViewerChanged = False And DirectCast(Content, UCDocumentViewer2).FormBrowser Is Nothing Then

                            TabViewers.SelectTab(Content)
                            ViewerChanged = False
                            Exit Sub

                        ElseIf DirectCast(Content, UCDocumentViewer2).Result.ID = NewResult.ID And ViewerChanged = True _
                                And IsNothing(Forms) = True Then

                            TabViewers.TabPages.Remove(TabViewers.SelectedTab)
                            ViewerChanged = False
                        ElseIf DirectCast(Content, UCDocumentViewer2).Result.ID = NewResult.ID And ViewerChanged = True _
                            And IsNothing(Forms) = False Then

                            TabViewers.TabPages.Remove(TabViewers.SelectedTab)
                            ViewerChanged = True
                        End If

                        'Se fija si el archivo se encuentra abierto en otro tab
                        'Este caso se puede dar al abrir intentar cargar un documento 2 veces sin haberlos insertado.
                        '[Sebastian 04-06-2009] se mejoro la codición, porque si el tab no existia lanza  exception
                        If String.Equals(NewResult.FullPath.Trim, DirectCast(Content, UCDocumentViewer2).Result.FullPath.Trim) AndAlso TabViewers.Contains(Content) = True Then
                            TabViewers.SelectTab(Content)
                            'MessageBox.Show("El documento que desea visualizar se encuentra abierto actualmente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If

                    End If
                Next

                'SI NO CREA UN CONTENT NUEVO Y LO MUESTRA
                Dim UcViewer As New UCDocumentViewer2(NewResult, False)
                UcViewer.Text = NewResult.Name
                UcViewer.Tag = NewResult.Name
                TabViewers.TabPages.Add(UcViewer)
                TabViewers.SelectTab(UcViewer)

                Dim docInsertPreview As Boolean = Boolean.Parse(UserPreferences.getValue("DocInsertPreview", UPSections.InsertPreferences, "True"))
                If docInsertPreview Then
                    'Oculto los botones que no se tienen que mostrar al antes de insertar un doc.
                    UcViewer.HideButtons()

                    If IsNothing(FormBusiness.GetShowAndEditForms(CInt(NewResult.DocTypeId))) = True Then
                        UcViewer.ShowDocument(False, False, InsertingDoc, False, False)
                    Else
                        UcViewer.ShowDocument(True, False, InsertingDoc, False, False)
                    End If


                    RemoveHandler UcViewer.CambiarDock, AddressOf CambiarDock
                    AddHandler UcViewer.CambiarDock, AddressOf CambiarDock
                    RemoveHandler UcViewer.ShowOriginal, AddressOf ShowOriginal
                    AddHandler UcViewer.ShowOriginal, AddressOf ShowOriginal

                    RemoveHandler UcViewer.SaveDocumentWithVirtualForms, AddressOf SaveDocumentVirtualForm
                    AddHandler UcViewer.SaveDocumentWithVirtualForms, AddressOf SaveDocumentVirtualForm
                Else
                    UcViewer.ShowNonPreviewMessage()
                End If

                CheckInsertAllVisibility()

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            'Visualizacion botones
            chkRealizarOcr.Visible = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.UseOCR, NewResult.DocTypeId)
            chkReplicar.Visible = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.Replicate, NewResult.DocTypeId)
            BtnAgregarBarcode.Visible = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.RecognizeBarCode, NewResult.DocTypeId)
            AddHandler TabViewers.TabIndexChanged, AddressOf TabViewersIndexChanged
            ResumeLayout()
        End Try
    End Sub

    '[Alejandro] 20/11/09 - Created
    Private Sub SaveDocumentVirtualForm()
        Dim NewRes As NewResult = GetActiveDocumentId()
        FillHashIndexados(NewRes)
        ActualizarControles(NewRes, True)
    End Sub

    ''' <summary>
    ''' Muestra el documento original.
    ''' </summary>
    ''' <param name="results"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] - 12/05/2009 - Created - Método modificado de NewDocumentViewer de MainPanel.
    ''' </history>
    Private Sub ShowOriginal(ByRef Result As Result)

        Dim IsOpened As Boolean = False
        Dim TabCounter As Int32 = 0
        Dim TabTarea As UCDocumentViewer2

        'Comprueba si la pestaña se encuentra abierta
        For Each dc As TabPage In TabViewers.TabPages
            If TypeOf dc Is UCDocumentViewer2 Then
                If DirectCast(dc, UCDocumentViewer2).Result.ID = Result.ID Then
                    TabCounter = TabCounter + 1
                    If TabCounter > 1 Then
                        IsOpened = True
                    End If
                End If
            End If
        Next

        If IsOpened = False Then
            'Crea la pestaña correspondiente con el documento original
            TabTarea = New UCDocumentViewer2(Me, Result, False, False)
            TabTarea.Dock = DockStyle.Fill
            'Selecciona la pestaña
            TabViewers.TabPages.Add(TabTarea)
            TabViewers.SelectTab(TabTarea)
            'Configura la pestaña
            TabTarea.HideButtons()
            TabTarea.VerOriginalButtonVisible = False
            RemoveHandler TabTarea.CambiarDock, AddressOf CambiarDock
            AddHandler TabTarea.CambiarDock, AddressOf CambiarDock
            'Muestro el documento
            TabTarea.ShowDocument(False, False, False, False, False)
        End If

    End Sub
#Region "Split"

    Public Sub Split(ByVal Viewer As System.Windows.Forms.TabPage, ByVal Splited As Boolean) Implements Core.IViewerContainer.Split
        'No se implementa esta funcion, ya que no estan funcionales las demas solapas en esta instancia en el container
    End Sub
    Private Shadows Property Name As String Implements IViewerContainer.Name
        Get
            Return MyBase.Name
        End Get
        Set(value As String)
            MyBase.Name = value
        End Set
    End Property
#End Region
    Private Sub TabViewersIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If IsNothing(TabViewers.SelectedTab) = False Then

                Dim ContentDocumento As UCDocumentViewer2 = DirectCast(TabViewers.SelectedTab, UCDocumentViewer2)
                Dim Id As Int32 = CInt(ContentDocumento.Result.ID)
                For Each Node As ZNewResultNode In _arbol.TreePorInsertar.Nodes
                    If Node.ZambaCore.ID = Id Then
                        _arbol.TreeView.SelectedNode = Node
                        Node.EnsureVisible()
                        Exit Sub
                    End If
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub LoadPanels()
        Try
            TabMainControl.SelectedTab = TabIndexer
            SuspendLayout()

            _browserFiles = New BrowserFiles
            _browserFiles.Dock = DockStyle.Fill
            TabSelectDocuments.Controls.Add(_browserFiles)

            _arbol = New Arbol
            _arbol.Dock = DockStyle.Fill
            SplitContainer1.Panel1.Controls.Add(_arbol)

            RemoveHandler _browserFiles.AddFiles, AddressOf AddFiles
            RemoveHandler _browserFiles.ShowResult, AddressOf InsertVirtualResult
            RemoveHandler _browserFiles.ShowTemplatesAdmin, AddressOf ShowTemplatesAdmin

            RemoveHandler _arbol.Seleccionado, AddressOf Seleccionado
            RemoveHandler _arbol.Seleccionado_Insertado, AddressOf Seleccionado_Insertado
            RemoveHandler _arbol.ShowBrowser, AddressOf ShowSelectDocumentsTab
            RemoveHandler _arbol.Eliminado, AddressOf Eliminado
            RemoveHandler _arbol.EliminadosTodos1, AddressOf EliminadosTodos1
            RemoveHandler _arbol.EliminadosTodos2, AddressOf EliminadosTodos2
            RemoveHandler _arbol.SeleccionadoParent, AddressOf SeleccionadoParent
            RemoveHandler _arbol.SelectedIndexChanged, AddressOf SelectedIndexChanged

            AddHandler _browserFiles.AddFiles, AddressOf AddFiles
            AddHandler _browserFiles.ShowResult, AddressOf InsertVirtualResult
            AddHandler _browserFiles.ShowTemplatesAdmin, AddressOf ShowTemplatesAdmin

            AddHandler _arbol.Seleccionado, AddressOf Seleccionado
            AddHandler _arbol.Seleccionado_Insertado, AddressOf Seleccionado_Insertado
            AddHandler _arbol.ShowBrowser, AddressOf ShowSelectDocumentsTab
            AddHandler _arbol.Eliminado, AddressOf Eliminado
            AddHandler _arbol.EliminadosTodos1, AddressOf EliminadosTodos1
            AddHandler _arbol.EliminadosTodos2, AddressOf EliminadosTodos2
            AddHandler _arbol.SelectedIndexChanged, AddressOf SelectedIndexChanged
            AddHandler _arbol.SeleccionadoParent, AddressOf SeleccionadoParent

            TabMainControl.SelectedTab = TabSelectDocuments
            ResumeLayout()

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub ShowTemplatesAdmin()
        ParentContainer.OpenTemplatesAdmin()
    End Sub

    Private Sub ShowSelectDocumentsTab()
        Try
            TabMainControl.SelectTab(TabSelectDocuments)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ShowVisualizadorContent()
        Try
            TabMainControl.SelectTab(TabIndexer)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub EliminadosTodos1()
        _docsInsertar = 0
        lblInsertar.Text = "0 DOCUMENTOS POR INSERTAR"


        For Each NewResult As NewResult In GetAllDocuments()
            CloseDocument(NewResult.ID)
            RemoveFile(NewResult)
        Next
    End Sub

    Private Sub EliminadosTodos2()
        _docsInsertados = 0
        lblInsertados.Text = "0 DOCUMENTOS INSERTADOS"
        Dim Results As New ArrayList
        Results.AddRange(_hashIndexados.Values)
        For Each NewResult As NewResult In Results
            CloseDocument(NewResult.ID)
        Next
        _hashIndexados.Clear()
    End Sub

    Private Sub CloseDocument(ByVal Id As Long)
        Try
            For Each Document As UCDocumentViewer2 In TabViewers.TabPages
                If Document.Result.ID = Id Then
                    Document.Dispose()
                    Exit For
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub Eliminado()
        _docsInsertar -= 1
        lblInsertar.Text = _docsInsertar.ToString & " DOCUMENTOS POR INSERTAR"
        If TabViewers IsNot Nothing AndAlso TabViewers.SelectedTab IsNot Nothing Then
            TabViewers.TabPages.Remove(TabViewers.SelectedTab)
        End If
    End Sub

    Private Function GetActiveDocumentId() As NewResult
        Try
            If String.Compare(_arbol.TreeView.SelectedNode.GetType.ToString, "System.Windows.Forms.TreeNode", True) = 0 Then
                Return Nothing
            End If

            Dim ZNode As ZNewResultNode = DirectCast(_arbol.TreeView.SelectedNode, ZNewResultNode)
            Return ZNode.ZambaCore
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function
    Private Function GetAllDocuments() As List(Of NewResult)
        Try
            Dim listofdocuments As New List(Of NewResult)
            For Each currentnode As TreeNode In _arbol.TreePorInsertar.Nodes()
                If String.Compare(currentnode.GetType.ToString, "System.Windows.Forms.TreeNode", True) <> 0 Then
                    Dim ZNode As ZNewResultNode = DirectCast(currentnode, ZNewResultNode)
                    listofdocuments.Add(ZNode.ZambaCore)
                End If
            Next
            Return listofdocuments
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Private Sub RemoveFile(newresult As NewResult)
        Try
            For Each currentnode As TreeNode In _arbol.TreePorInsertar.Nodes()
                If String.Compare(currentnode.GetType.ToString, "System.Windows.Forms.TreeNode", True) <> 0 Then
                    Dim ZNode As ZNewResultNode = DirectCast(currentnode, ZNewResultNode)
                    If newresult.ID = ZNode.ZambaCore.ID Then
                        ZNode.Remove()
                    End If
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub FillHashIndexados(ByVal NewResult As NewResult)
        _hashIndexados.Add(NewResult.ID, NewResult)
    End Sub



#Region "Eventos"


    Private Sub DesignSandBar_ButtonClick(ByVal sender As System.Object, ByVal e As TD.SandBar.ToolBarItemEventArgs) Handles DesignSandBar.ButtonClick
        Select Case CStr(e.Item.Tag)
            Case "ATRAS"
                Try
                    _arbol.UcIndexs.Focus()
                    _arbol.TreeView.SelectedNode = _arbol.TreeView.SelectedNode.PrevNode
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Case "ADELANTE"
                Try
                    _arbol.UcIndexs.Focus()
                    _arbol.TreeView.SelectedNode = _arbol.TreeView.SelectedNode.NextNode
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Case "INSERTAR"
                _arbol.UcIndexs.Focus()
                If _arbol.UcIndexs.IsValid Then
                    InsertarClick()
                End If
            Case "INSERTARTODO"
                InsertAll()

            Case "REALIZAROCR"
                chkRealizarOcr.Checked = Not chkRealizarOcr.Checked
            Case "BARCODE"
                Dim Newresult As NewResult = GetActiveDocumentId()
                If Not IsNothing(Newresult) Then
                    MostrarBarcode(Newresult)
                Else
                    MostrarBarcode(Newresult)
                End If
            Case "REPLICAR"
                chkReplicar.Checked = Not chkReplicar.Checked
        End Select
    End Sub

    Private Sub Indexer6000_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        '        Try
        'MeResize()
        '       Catch ex As Exception
        '      zclass.raiseerror(ex)
        '     End Try

        Try
            Dim Directory As New IO.DirectoryInfo(_pathDirectory)
            If Directory.Exists = False Then Directory.Create()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        _hashIndexados.Clear()
        _docsInsertados = 0
        _docsInsertar = 0

        Try
            _arbol.AddSpecialNodes()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Funcion para insertar múltiples documentos tocando solo un botón.
    ''' Todos tendrán los mismos valores en sus atributos.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InsertAll()
        Try
            _arbol.UcIndexs.Focus()

            If MessageBox.Show("¿DESEA INSERTAR TODOS LOS DOCUMENTOS CON LOS ATRIBUTOS ESPECIFICADOS?", "Inserción múltiple",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If

            'Se obtiene el newresult y se completan sus datos a insertar
            Dim firstresult As NewResult = GetActiveDocumentId()

            If firstresult IsNot Nothing Then
                _flagInsertingAll = True
                Dim insertResult As InsertResult = Insertar(firstresult)

                If insertResult = Core.InsertResult.Insertado Then

                    For Each NewResult As NewResult In GetAllDocuments()
                        If Not IsNothing(NewResult) Then
                            If firstresult.ID <> NewResult.ID Then
                                Dim XResult As NewResult
                                XResult = Results_Business.CloneResult(firstresult, NewResult.File, False, True, True)
                                If XResult IsNot Nothing Then
                                    insertResult = Insertar(XResult)

                                    If insertResult = Core.InsertResult.Insertado Then
                                        RemoveFile(NewResult)
                                    End If
                                End If
                            End If
                        End If
                    Next

                    ' Se actualiza el U_TIME del usuario, un campo que indica el horario de cuando fue la última acción realizada por el usuario
                    ' En este caso, se actualizaría cuando se presiona el botón Insertar
                    UserBusiness.Rights.SaveAction(2, ObjectTypes.ModuleInsert, RightsType.insert, "Se inserto documento/s")
                End If
            End If

            If ReturnToPreviousTabPage Then
                ParentContainer.ReturnToPreviusTabPage()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            _flagInsertingAll = False
        End Try
    End Sub

#End Region

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
        Dim SFI As SHFILEINFO = Nothing
        If System.IO.File.Exists(mPath) Then
            SHGetFileInfo(mPath, 0, SFI, System.Runtime.InteropServices.Marshal.SizeOf(SFI), SHGFI_ICON Or CInt(IIf(Large, SHGFI_LARGEICON, SHGFI_SMALLICON)))
        Else
            SHGetFileInfo(mPath, 0, SFI, System.Runtime.InteropServices.Marshal.SizeOf(SFI), SHGFI_ICON Or SHGFI_USEFILEATTRIBUTES Or CInt(IIf(Large, SHGFI_LARGEICON, SHGFI_SMALLICON)))
        End If
        GetFileIcon = Icon.FromHandle(SFI.hIcon)
    End Function
#End Region

    ''' <summary>
    ''' Habilita o deshabilita el combo de entidades
    ''' </summary>
    ''' <param name="disable">True, para deshabilitar el combo de entidades</param>
    ''' <remarks></remarks>
    Public Sub DisableDTCombo(ByVal disable As Boolean)
        _disableDTCombo = disable
    End Sub

    ''' <summary>
    ''' Set if the document is opened after insert if its asociated to a wf, used for DoAddAsociatedDocuments
    ''' </summary>
    ''' <history>
    '''     AlejandroR  10/01/2010  Created
    ''' </history>
    Private _dontOpenTaskIfAsociatedToWF As Boolean

    Public WriteOnly Property DontOpenTaskIfAsociatedToWF() As Boolean
        Set(ByVal value As Boolean)
            _dontOpenTaskIfAsociatedToWF = value
        End Set
    End Property

    ''' <summary>
    ''' Set if the document is opened after insert if its asociated to a wf, used for DoAddAsociatedDocuments
    ''' </summary>
    ''' <history>
    '''     AlejandroR  10/01/2010  Created
    ''' </history>
    Private _ParentTaskId As Int64

    'Public WriteOnly Property ParentTaskId() As Int64
    Public Property ParentTaskId() As Int64
        Set(ByVal value As Int64)
            _ParentTaskId = value
        End Set
        Get
            Return _ParentTaskId
        End Get
    End Property

    Public Property KeepAsignedDocId As Int64
    Public Property keepIndexData As Boolean = True


    ''' <summary>
    ''' Method to execute directly Inserta Doc, Insertar Form, Scan Document or Insert Folder Option
    ''' </summary>
    ''' <param name="opt"></param>
    ''' <history>
    '''     Javier  30/12/2010  Created
    ''' </history>
    Public Sub OpenOptionToInsert(ByVal opt As Int32)
        _browserFiles.OpenOptionToInsert(opt)
    End Sub

    ''' <summary>
    ''' Si hay mas de un documento a insertar, se muestra el boton de insertar todos.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CheckInsertAllVisibility()
        If _arbol.TreePorInsertar.Nodes.Count > 1 Then
            btnInsertarTodo.Visible = True
            btnInsertar.Visible = True
        Else
            btnInsertarTodo.Visible = False
            btnInsertar.Visible = True
        End If
    End Sub
End Class


