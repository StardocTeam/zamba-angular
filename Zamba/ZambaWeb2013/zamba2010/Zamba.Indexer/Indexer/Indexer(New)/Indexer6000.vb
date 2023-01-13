Imports Zamba.AppBlock
Imports Zamba.Core

Imports Zamba.Viewers


Public Class Indexer6000
    Inherits ZControl
    Implements IViewerContainer


#Region "Variables"
    Private _flagAddDocument As Boolean = False
    Private _replacedocument As Boolean = False
    'Private _pathDirectory As String = Membership.MembershipHelper.StartUpPath & "\IndexerTemp"
    Private _lastFolderId As Int64
    Private _setFolderId As Boolean = False
    'Private _newDocument As String = String.Empty
    Private _activeId As Long = -1
    Private _flagDelete As Boolean = True
    Private _flagInsertingAll As Boolean = False
    Private _browserFiles As BrowserFiles = Nothing
    Private _arbol As Arbol = Nothing
    Private _hashIndexados As New Hashtable()
    Private _hashPorIndexar As New Hashtable()
    Private _docsInsertar As Int32
    Private _docsInsertados As Int32
    Public PublicFolderId As Int32
    Public PublicDocTypeId As Int32
    Public PublicIndexs As New ArrayList
    Public NodoPorIndexar As New TreeNode
    Public NodoIndexados As New TreeNode
    Public DocType As DocType
    Private _doctypeid As Int64
    Private _DocRelationId As Int32 = -1
    Private _isDoaddAsoc As Boolean = False
    Dim ViewerChanged As Boolean


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


    Private _relationatedDocument As Result
    Public Property RelationatedResult() As Result
        Get
            Return _relationatedDocument
        End Get
        Set(ByVal value As Result)
            _relationatedDocument = value
        End Set
    End Property

    Public Property LastFolderId() As Int64
        Get
            Return _lastFolderId
        End Get
        Set(ByVal Value As Int64)
            _lastFolderId = Value
        End Set
    End Property
    Public Property Indexs() As ArrayList
        Get
            Return PublicIndexs
        End Get
        Set(ByVal Value As ArrayList)
            PublicIndexs = Value
            _arbol.SetIndexs(Value)
        End Set
    End Property

    Public Event CambiarDockEvent(ByVal Sender As TabPage, ByVal Expander As Boolean)
    Public Event Indexer6000Closed()
    Private WithEvents extVis As ExternalVisualizer
    'Dim PanelVisualizador As New Panel
    'Dim ContentPlantillas As DockContent
    'Dim ContentVisualizador As DockContent
    'Dim FlagFirstTimePanels As Boolean
    'Dim WithEvents DockPanel As DockPanel
    'Dim WithEvents DockPanel3 As DockPanel
    'Dim WithEvents DockPanel2 As DockPanel
    'Dim ContentTreeview As ZContentTreeview
    'Dim FlagVolumeReady As Boolean
    'Private FileArray As New ArrayList
    'Private FileIndexed As New ArrayList
    'Dim resultlist As New resultlist
    'Private ImgCount As Integer = 0
    'Private OldImgCount As Integer = 0
    'Dim Cantidad As Int32 = 0
    'Dim UCToolBar As New UCToolBar
    'Dim FlagDelOriginal As Boolean
    'Private DocTypes() As DocType
    'Private _extVis As ExternalVisualizer
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

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TabMainControl As System.Windows.Forms.TabControl
    Friend WithEvents TabSelectDocuments As System.Windows.Forms.TabPage
    Friend WithEvents TabIndexer As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer

    Friend WithEvents lblInsertar As System.Windows.Forms.Label
    Friend WithEvents lblInsertados As System.Windows.Forms.Label
    '  Friend WithEvents PanelimageList As System.Windows.Forms.ImageList
    Friend WithEvents DesignSandBar As TD.SandBar.ToolBar
    Friend WithEvents IBAtras As TD.SandBar.ButtonItem
    Friend WithEvents IBSiguiente As TD.SandBar.ButtonItem
    Friend WithEvents btnInsertar As TD.SandBar.ButtonItem
    Friend WithEvents btnInsertarTodo As TD.SandBar.ButtonItem
    Friend WithEvents btnAgregar As TD.SandBar.ButtonItem
    Friend WithEvents chkRealizarOcr As TD.SandBar.ButtonItem
    Friend WithEvents BtnNOReplicar As TD.SandBar.ButtonItem
    Friend WithEvents ButtonItem2 As TD.SandBar.ButtonItem
    Friend WithEvents btnReplicar As TD.SandBar.ButtonItem
    Friend WithEvents TabViewers As System.Windows.Forms.TabControl
    Friend WithEvents BtnAgregarBarcode As TD.SandBar.ButtonItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Indexer6000))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.DesignSandBar = New TD.SandBar.ToolBar
        Me.IBAtras = New TD.SandBar.ButtonItem
        Me.IBSiguiente = New TD.SandBar.ButtonItem
        Me.btnInsertar = New TD.SandBar.ButtonItem
        Me.btnInsertarTodo = New TD.SandBar.ButtonItem
        Me.btnAgregar = New TD.SandBar.ButtonItem
        Me.BtnNOReplicar = New TD.SandBar.ButtonItem
        Me.btnReplicar = New TD.SandBar.ButtonItem
        Me.chkRealizarOcr = New TD.SandBar.ButtonItem
        Me.BtnAgregarBarcode = New TD.SandBar.ButtonItem
        Me.lblInsertar = New System.Windows.Forms.Label
        Me.lblInsertados = New System.Windows.Forms.Label
        Me.ButtonItem2 = New TD.SandBar.ButtonItem
        Me.TabMainControl = New System.Windows.Forms.TabControl
        Me.TabSelectDocuments = New System.Windows.Forms.TabPage
        Me.TabIndexer = New System.Windows.Forms.TabPage
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TabViewers = New System.Windows.Forms.TabControl
        Me.Panel1.SuspendLayout()
        Me.TabMainControl.SuspendLayout()
        Me.TabIndexer.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(153, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.Panel1.Controls.Add(Me.DesignSandBar)
        Me.Panel1.Controls.Add(Me.lblInsertar)
        Me.Panel1.Controls.Add(Me.lblInsertados)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(3, 409)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(974, 75)
        Me.Panel1.TabIndex = 1
        '
        'DesignSandBar
        '
        Me.DesignSandBar.Buttons.AddRange(New TD.SandBar.ToolbarItemBase() {Me.IBAtras, Me.IBSiguiente, Me.btnInsertar, Me.btnInsertarTodo, Me.btnAgregar, Me.BtnNOReplicar, Me.btnReplicar, Me.chkRealizarOcr, Me.BtnAgregarBarcode})
        Me.DesignSandBar.Closable = False
        Me.DesignSandBar.DockLine = 1
        Me.DesignSandBar.Guid = New System.Guid("1fd972d3-e337-4d0f-ab9e-b228d26929dd")
        Me.DesignSandBar.Location = New System.Drawing.Point(0, 0)
        Me.DesignSandBar.Name = "DesignSandBar"
        Me.DesignSandBar.Size = New System.Drawing.Size(974, 42)
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
        'btnAgregar
        '
        Me.btnAgregar.Icon = CType(resources.GetObject("btnAgregar.Icon"), System.Drawing.Icon)
        Me.btnAgregar.IconSize = New System.Drawing.Size(32, 32)
        Me.btnAgregar.Padding.Left = 5
        Me.btnAgregar.Padding.Right = 5
        Me.btnAgregar.Tag = "AGREGAR"
        Me.btnAgregar.Text = "AGREGAR"
        Me.btnAgregar.ToolTipText = "AGREGAR"
        '
        'BtnNOReplicar
        '
        Me.BtnNOReplicar.BeginGroup = True
        Me.BtnNOReplicar.Icon = CType(resources.GetObject("BtnNOReplicar.Icon"), System.Drawing.Icon)
        Me.BtnNOReplicar.Tag = "NOREPLICAR"
        Me.BtnNOReplicar.Text = "NO REPLICAR"
        Me.BtnNOReplicar.ToolTipText = "NO REPLICAR"
        '
        'btnReplicar
        '
        Me.btnReplicar.BeginGroup = True
        Me.btnReplicar.Icon = CType(resources.GetObject("btnReplicar.Icon"), System.Drawing.Icon)
        Me.btnReplicar.Tag = "REPLICAR"
        Me.btnReplicar.Text = "REPLICAR"
        Me.btnReplicar.ToolTipText = "REPLICAR"
        Me.btnReplicar.Visible = False
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
        'lblInsertar
        '
        Me.lblInsertar.AutoSize = True
        Me.lblInsertar.BackColor = System.Drawing.Color.Transparent
        Me.lblInsertar.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInsertar.ForeColor = System.Drawing.Color.Black
        Me.lblInsertar.Location = New System.Drawing.Point(245, 50)
        Me.lblInsertar.Name = "lblInsertar"
        Me.lblInsertar.Size = New System.Drawing.Size(215, 16)
        Me.lblInsertar.TabIndex = 7
        Me.lblInsertar.Text = "0 DOCUMENTOS POR INSERTAR"
        '
        'lblInsertados
        '
        Me.lblInsertados.AutoSize = True
        Me.lblInsertados.BackColor = System.Drawing.Color.Transparent
        Me.lblInsertados.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInsertados.ForeColor = System.Drawing.Color.Black
        Me.lblInsertados.Location = New System.Drawing.Point(10, 50)
        Me.lblInsertados.Name = "lblInsertados"
        Me.lblInsertados.Size = New System.Drawing.Size(204, 16)
        Me.lblInsertados.TabIndex = 6
        Me.lblInsertados.Text = "0 DOCUMENTOS INSERTADOS"
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
        Me.TabMainControl.Size = New System.Drawing.Size(988, 514)
        Me.TabMainControl.TabIndex = 2
        '
        'TabSelectDocuments
        '
        Me.TabSelectDocuments.Location = New System.Drawing.Point(4, 23)
        Me.TabSelectDocuments.Name = "TabSelectDocuments"
        Me.TabSelectDocuments.Padding = New System.Windows.Forms.Padding(3)
        Me.TabSelectDocuments.Size = New System.Drawing.Size(980, 487)
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
        Me.TabIndexer.Size = New System.Drawing.Size(980, 487)
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
        Me.SplitContainer1.Size = New System.Drawing.Size(974, 406)
        Me.SplitContainer1.SplitterDistance = 244
        Me.SplitContainer1.TabIndex = 2
        '
        'TabViewers
        '
        Me.TabViewers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabViewers.Location = New System.Drawing.Point(0, 0)
        Me.TabViewers.Name = "TabViewers"
        Me.TabViewers.SelectedIndex = 0
        Me.TabViewers.Size = New System.Drawing.Size(726, 406)
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
        Me.Size = New System.Drawing.Size(992, 518)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabMainControl.ResumeLayout(False)
        Me.TabIndexer.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Function AddNewResult(ByVal File As String) As NewResult
        Dim NewResult As NewResult = Results_Business.GetNewNewResult(Me.DocType, CInt(UserBusiness.Rights.CurrentUser.ID), File)

        NewResult.ID = ToolsBusiness.GetNewID(IdTypes.DOCID)
        Return NewResult
    End Function

    ''' <summary>
    ''' [Sebastian 12-05-09] Obtiene el primer doctype id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFirstDocType() As Int16
        _arbol.DocTypes = DocTypesBusiness.GetDocTypesbyUserRightsOfView(UserBusiness.Rights.CurrentUser.ID, RightsType.Create)

        If _arbol.DocTypes.Count <= 0 Then
            Return 0
        Else
            'Return CShort(_arbol.DocTypes(0).Id)
            Return CShort(DirectCast(_arbol.DocTypes(0), DocType).ID)
        End If
    End Function

    ''' <summary>
    ''' [Sebastian 12-05-09] COMMENTED indexa el documento que se esta insertando agregando los valores de los 
    '''                                indices al mismo
    ''' [Sebastian 12-05-09] MODIFIED  se agrego "elseif" para validar que si es form virtual y esta
    '''                                asociado a un wf lo inserte y luego lo muestre en el wf que se agrego.
    '''                                Se salvaron WARNINGS.
    ''' </summary>
    ''' <param name="NewResult"></param>
    ''' <param name="FolderId"></param>
    ''' <param name="DisableAutomaticVersion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 06/04/09 - Modified... Se valida las lineas donde se inserta el formulario en zamba.
    '''           [Sebastian] 04-11-2009 Modified Carga de valores por defecto, para formularios electronicos
    '''</history>
    Private Function IndexDocument(ByRef NewResult As NewResult, Optional ByVal FolderId As Int64 = 0, Optional ByVal DisableAutomaticVersion As Boolean = False) As InsertResult
        Dim InsertResult As InsertResult = InsertResult.NoInsertado
        Try
            If Not IsNothing(NewResult) Then
                NewResult.FolderId = FolderId

                Try
                    If Me.TabViewers.TabCount > 0 AndAlso IsNothing(Me.TabViewers.SelectedTab) = False Then
                        '[Sebastian 12-05-09] se agrego DirecCast para salvar el warning de casteo.
                        Dim NewUcViewer As UCDocumentViewer2 = DirectCast(Me.TabViewers.SelectedTab, UCDocumentViewer2)
                        NewUcViewer.CloseDocument(_flagDelete, DisableAutomaticVersion)
                        Me.TabViewers.TabPages.Remove(Me.TabViewers.SelectedTab)
                        NewUcViewer.Dispose()
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try
                    '[ecanete]8-9-2009- Se comenta y se implementa la funcionalidad de autocomplete en el método insert en el
                    'branch branch existente
                    Dim dsIndexsToIncrement As DataSet = DocTypesBusiness.GetIndexsProperties(NewResult.DocType.ID, True)
                    Dim IncrementedValue As Int64 = 0

                    For Each CurrentRow As DataRow In dsIndexsToIncrement.Tables(0).Rows
                        If IsDBNull(CurrentRow("autoincremental")) = False AndAlso Int64.Parse(CurrentRow("Autoincremental").ToString) = 1 Then
                            For Each CurrentIndex As Index In NewResult.Indexs
                                If String.Compare(CurrentRow("Index_Name").ToString.Trim, CurrentIndex.Name.Trim) = 0 Then
                                    If CurrentIndex.Data.Trim() = String.Empty Then
                                        IncrementedValue = IndexsBusiness.SelectMaxIndexValue(CurrentIndex.ID, NewResult.DocType.ID)
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
                                        CurrentIndex.Data = CurrentRow("DefaultValue").ToString.Trim
                                        CurrentIndex.DataTemp = CurrentRow("DefaultValue").ToString.Trim
                                    End If

                                End If
                            Next
                        End If
                    Next

                    Dim openTask As Boolean = True

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

                        If btnReplicar.Visible And Not IsNothing(NewResult.Doc_File) Then
                            'Si el archivo ya existe lo redirecciono y lo guardo
                            InsertResult = Results_Business.InsertDocument(NewResult, False, False, False, False, False, True, True, openTask)
                        ElseIf btnReplicar.Visible = True Then
                            'Si el archivo no existe lo creo y lo guardo
                            InsertResult = Results_Business.InsertDocument(NewResult, False, False, False, False, False, True, False, openTask)
                        Else
                            'Simplemente lo inserto
                            InsertResult = Results_Business.InsertDocument(NewResult, _flagDelete, False, Me._replacedocument, True, NewResult.ISVIRTUAL, False, False, openTask)
                        End If
                    ElseIf NewResult.ISVIRTUAL = True Then
                        'Para cuando inserto un formVirtual, que vaya de una a la solapa de insertado
                        InsertResult = InsertResult.Insertado
                    End If
                Catch ex As Exception
                    InsertResult = InsertResult.NoInsertado
                    raiseerror(ex)
                    MessageBox.Show(ex.Message, "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                Try
                    'If BtnNOReplicar.Visible Then
                    Me._hashIndexados.Remove(NewResult.ID)
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

    Private Function ChangeIndexs(ByVal NewResult As NewResult) As NewResult
        Dim FlagModified As Boolean = False
        Dim FlagAllBlank As Boolean = True

        Dim ArrayIndices As ArrayList = _arbol.UcIndexs.GetIndexs


        Dim DocTypeIndex As Int32 = _arbol.cboDocType.SelectedIndex

        For Each Index1 As Index In ArrayIndices
            For Each Index2 As Index In NewResult.Indexs
                If Index2.ID = Index1.ID Then
                    If Index2.DataTemp = "" AndAlso Index2.dataDescriptionTemp = "" Then
                        Index2.Data = Index1.Data
                        Index2.DataTemp = Index1.Data
                        Index2.dataDescription = Index1.dataDescription
                        Index2.dataDescriptionTemp = Index1.dataDescription
                        FlagModified = True
                    Else
                        FlagAllBlank = False
                    End If
                    Exit For
                End If
            Next
        Next

        If FlagAllBlank = True Then
            SelectedIndexChanged(DocTypeIndex)
            'NewResult.Parent = AllDocTypes(DocTypeIndex)
        Else
            _arbol.MostrarIndices(NewResult)

        End If

        If FlagModified = True Then
            Me._hashPorIndexar.Remove(NewResult.ID)
            FillHashPorIndexar(NewResult)
        End If

        Return NewResult
    End Function

    Private Function ChangeIndexs2(ByVal NewResult As NewResult) As NewResult
        Dim FlagModified As Boolean = False

        '  Dim OldResult As NewResult = Me.HashPorIndexar(NewResult.Id)

        Dim ArrayIndices As ArrayList = _arbol.UcIndexs.GetIndexs

        For Each Index1 As Index In ArrayIndices
            For Each Index2 As Index In NewResult.Indexs
                If Index2.ID = Index1.ID Then
                    If Index2.DataTemp = "" AndAlso Index2.dataDescriptionTemp = "" Then
                        Index2.Data = Index1.Data
                        Index2.DataTemp = Index1.Data
                        Index2.dataDescription = Index1.dataDescription
                        Index2.dataDescriptionTemp = Index1.dataDescription
                        FlagModified = True
                    End If
                    Exit For
                End If
            Next
        Next

        If FlagModified = True Then
            Me._hashPorIndexar.Remove(NewResult.ID)
            FillHashPorIndexar(NewResult)
        End If

        Return NewResult
    End Function


    Public Sub InicializaIndexer(ByRef Result As Result, ByVal docType As DocType, ByVal indexs As ArrayList, ByVal lastFolderId As Int64)
        Me.btnInsertar.Visible = False
        Me.btnInsertarTodo.Visible = False
        _lastFolderId = lastFolderId
        Me.Indexs = indexs
        Me.DocType = docType

        _arbol.DesHabilitarSelectedIndexChanged()
        _arbol.SetDocTypeId(CInt(docType.ID))
        _doctypeid = docType.ID
        _arbol.cboDocType.Text = docType.Name
        _arbol.UcIndexs.ShowIndexs(Result)
        _arbol.HabilitarSelectedIndexChanged()

        Me.TabMainControl.SelectedTab = Me.TabSelectDocuments

    End Sub
    Public Sub InicializaIndexer(ByRef Result As Result, ByVal docType As DocType, ByVal indexs As ArrayList, ByVal lastFolderId As Int64, ByVal TemplateId As Int32)
        Me.btnInsertar.Visible = False
        Me.btnInsertarTodo.Visible = False
        _lastFolderId = lastFolderId
        Me.Indexs = indexs
        Me.DocType = docType

        _arbol.DesHabilitarSelectedIndexChanged()
        _arbol.SetDocTypeId(CInt(docType.ID))
        _doctypeid = docType.ID
        _arbol.cboDocType.Text = docType.Name
        _arbol.UcIndexs.ShowIndexs(Result)
        _arbol.HabilitarSelectedIndexChanged()
        'Me._arbol.cboDocType.Visible = False
        LoadTemplate(TemplateId)
    End Sub
    Public Sub InicializaIndexerWithNewDocument(ByRef Result As Result, ByVal docType As DocType, ByVal indexs As ArrayList, ByVal lastFolderId As Int64, ByVal TypeId As Int32)
        Me.btnInsertar.Visible = False
        Me.btnInsertarTodo.Visible = False
        _lastFolderId = lastFolderId
        Me.Indexs = indexs
        Me.DocType = docType

        _arbol.DesHabilitarSelectedIndexChanged()
        _arbol.SetDocTypeId(CInt(docType.ID))
        _doctypeid = docType.ID
        _arbol.cboDocType.Text = docType.Name
        _arbol.UcIndexs.ShowIndexs(Result)
        _arbol.HabilitarSelectedIndexChanged()
        'Me._arbol.cboDocType.Visible = False
        If TypeId <> 0 Then
            LoadNewDocument(TypeId)
            Me.btnInsertar.Visible = True
            Me.btnInsertarTodo.Visible = True
        End If
    End Sub


    Private Sub LoadTemplate(ByVal TemplateId As Int32)
        Try
            Dim TemplatePath As New ArrayList
            Dim Templates As New TemplatesBusiness
            TemplatePath.Add(Templates.ObtainTemplatePath(TemplateId))
            AddFiles(TemplatePath, DocType)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LoadNewDocument(ByVal typeId As Int32)
        Dim _browserFiles As New BrowserFiles
        Select Case typeId
            Case OfficeTypes.Word
                Me._browserFiles.NuevoWord(DocType)
            Case OfficeTypes.Excel
                Me._browserFiles.NuevoExcel(DocType)
            Case OfficeTypes.PowerPoint
                Me._browserFiles.NuevoPowerpoint(DocType)
        End Select
    End Sub

    Private Sub InsertVirtualResult(ByRef Result As NewResult)

        If Not IsNothing(Result) Then

            'If Not IsNothing(Result.DocType) Then
            '    Try
            '        Result.DocType.IsReindex = True
            '    Catch ex As Exception
            '        ZClass.raiseerror(ex)
            '    End Try
            'End If

            For Each Index1 As Index In Me.Indexs
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

            Dim FolderId As Int64

            '[Ezequiel] 09/04/09 - Modified: Se comento las siguientes lineas ya que no estaba asociando
            ' por folder id.
            'If _setFolderId Then
            If LastFolderId = 0 Then LastFolderId = Results_Business.getNewFolderId
            FolderId = Me.LastFolderId
            'Else
            '    If FolderId = 0 Then
            '        FolderId = Results_Business.getNewFolderId
            '        Me.LastFolderId = FolderId
            '    End If
            'End If

            Dim insertresult As InsertResult
            insertresult = IndexDocument(Result, FolderId, True)

            If insertresult = insertresult.Insertado Then

                Dim hs As New Hashtable
                hs.Add("DontOpenTaskIfAsociatedToWF", _dontOpenTaskIfAsociatedToWF)
                Zamba.Core.Result.HandleModule(ResultActions.ShowNewForm, Result, hs)

            End If

        End If

    End Sub

    Public Sub AddFiles(ByVal Files As ArrayList, ByVal DocType As DocType)
        Me.TabMainControl.SelectTab(Me.TabIndexer)

        For Each File As String In Files
            Select Case File
                Case "NuevoWord"
                    Me._browserFiles.NuevoWord(Nothing)
                Case "NuevoExcel"
                    Me._browserFiles.NuevoExcel(Nothing)
                Case "NuevoPowerPoint"
                    Me._browserFiles.NuevoPowerpoint(Nothing)
            End Select
            If Not String.IsNullOrEmpty(File.Trim) AndAlso IO.File.Exists(File) = True Then
                Dim NewResult As NewResult = AddNewResult(File)
                NewResult.DocType = DocType
                'If Not IsNothing(NewResult.DocType) Then
                '    Try
                '        NewResult.DocType.IsReindex = True
                '    Catch ex As Exception
                '        ZClass.raiseerror(ex)
                '    End Try
                'End If
                Try
                    If IsNothing(_arbol.DocTypes) = True Then
                        GetFirstDocType()
                    End If
                    If _arbol.DocTypes.Count > 0 Then
                        NewResult.Parent = DirectCast(_arbol.DocTypes(0), IZambaCore)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                FillHashPorIndexar(NewResult)
                _arbol.CargarFiles(NewResult)
            End If
        Next

        _arbol.TreePorInsertar.ExpandAll()
        _docsInsertar += Files.Count
        'diego: Cambia el doctype y se pierde el seleccionado mediante la regla
        'If _doctypeid = 0 Then
        _arbol.SelectFirstFile()
        'End If
        If _docsInsertar = 1 Then
            lblInsertar.Text = "1 DOCUMENTO POR INSERTAR"
        ElseIf _docsInsertar > 1 Then
            lblInsertar.Text = _docsInsertar.ToString & " DOCUMENTOS POR INSERTAR"
        End If

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
            MyBase.new()
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

        Dim insertresult As InsertResult = insertresult.NoInsertado

        Try
            If UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, CInt(NewResult.DocType.ID)) Then
                'For Each row As DataRow In DocTypesBusiness.GetIndexsPropertiesWithIndexType(NewResult.DocType.ID).Tables(0).Rows
                'If row("mustcomplete") = 0 Then
                If Results_Business.ValidateIndexDataFromDoctype(NewResult) <> False Then
                    If Results_Business.ValidateIndexData(NewResult) = False Then
                        MessageBox.Show("Hay indices requeridos incompletos", "Zamba", MessageBoxButtons.OK)
                        Exit Function
                    End If
                Else
                    MessageBox.Show("Hay indices requeridos incompletos", "Zamba", MessageBoxButtons.OK)
                    Exit Function
                End If
                ' Next

            End If

            'Die: Agregado para guardar los documentos antes de insertar
            For Each Document As UCDocumentViewer2 In Me.TabViewers.TabPages
                If Document.Result.CurrentFormID <= 0 Then
                    Document.SaveDocument()
                End If
            Next

            If Not IsNothing(NewResult) Then
                _hashPorIndexar.Remove(NewResult.ID)
                'NewResult.DocType.IsReindex = False
                'Si se va a replicar que obtenga un id nuevo
                'If btnReplicar.Visible = True Then
                'Si ya esta insertado le cambio el ID
                'Dim ds As DataSet
                'ds = Results_Business.GetIndexData(newresult.DocType.ID,newresult.ID)
                'If ds.Tables(0).Rows.Count > 0 Then
                'Le pongo el ID en 0 para que obtenga uno nuevo
                NewResult.ID = 0
                'End If
                'ds.Dispose()
            End If

            Dim FolderId As Int64

            If _setFolderId Then
                If LastFolderId = 0 Then LastFolderId = Results_Business.getNewFolderId
                FolderId = Me.LastFolderId
            Else
                If FolderId = 0 Then
                    FolderId = Results_Business.getNewFolderId
                    Me.LastFolderId = FolderId
                End If
            End If

            'Graba el documento en la base
            If btnReplicar.Visible = True Then
                'Dim insertresult As Results_Factory.InsertResult
                'insertresult = Results_Factory.InsertDocument(NewResult, False, False, False, False, True)

                insertresult = IndexDocument(NewResult, FolderId, True)
                If insertresult = insertresult.Insertado Then
                    _hashPorIndexar.Remove(NewResult.ID)
                    'VERIFICA SI EXISTE UNA RELACION ENTRE DOCUMENTOS
                    If _DocRelationId <> -1 Then Results_Business.InsertDocumentRelation(RelationatedResult.ID, NewResult.ID, Me.DocumentRelationId)
                    ActualizarControles(DirectCast(_hashIndexados(NewResult.ID), NewResult))
                End If

                Me._hashPorIndexar.Add(NewResult.ID, NewResult)
            Else
                insertresult = IndexDocument(NewResult, FolderId, True)

                If insertresult = insertresult.Insertado Then
                    'VERIFICA SI EXISTE UNA RELACION ENTRE DOCUMENTOS
                    If _DocRelationId <> -1 Then Results_Business.InsertDocumentRelation(RelationatedResult.ID, NewResult.ID, Me.DocumentRelationId)
                    Try

                        If Me.chkRealizarOcr.Checked Then

                            'OCR
                            Dim TextoOCR As String = GenerateOCR(NewResult.NewFile)
                            If IO.File.Exists(Me._pathDirectory & "\TempOCR.txt") = True Then IO.File.Delete(Me._pathDirectory & "\TempOCR.txt")
                            SaveTextToFile(TextoOCR, Me._pathDirectory & "\TempOCR.txt", "")
                            Dim ResultOCR As NewResult = AddNewResult(Me._pathDirectory & "\TempOCR.txt")
                            ResultOCR.Parent = NewResult.Parent
                            ResultOCR.DocType = NewResult.DocType
                            ResultOCR.Indexs = NewResult.Indexs
                            ResultOCR.FolderId = FolderId
                            Results_Business.InsertDocument(ResultOCR, True, False, Me._replacedocument, True)
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
                    ActualizarControles(DirectCast(_hashIndexados(NewResult.ID), NewResult))
                    'If btnReplicar.Visible Then
                    '    'Me.HashPorIndexar.Remove(IDOriginal)
                    '    'Esta linea sola estaba al principio y agregaba todo el tiempo
                    '    Me.HashPorIndexar(NewResult.ID) = Me.HashIndexados(IDOriginal)
                    '    'Guardo el original en indexar
                    '    DirectCast(Me.HashPorIndexar(NewResult.ID), NewResult).ID = IDOriginal
                    'End If
                    'End If
                ElseIf insertresult = insertresult.NoInsertado Then    'si falla
                    _hashPorIndexar.Add(NewResult.ID, NewResult)
                    MessageBox.Show("Error al insertar el documento", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ElseIf insertresult = Core.InsertResult.ErrorIndicesIncompletos Then
                    _hashPorIndexar.Add(NewResult.ID, NewResult)
                    MessageBox.Show("Hay indices obligatorios sin completar", "Atencion", MessageBoxButtons.OK)
                ElseIf insertresult = Core.InsertResult.ErrorIndicesInvalidos Then
                    _hashPorIndexar.Add(NewResult.ID, NewResult)
                    MessageBox.Show("Hay indices con datos invalidos", "Atencion", MessageBoxButtons.OK)
                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return insertresult

    End Function

    Private Sub ActualizarControles(ByVal NewResult As NewResult)
        If Not IsNothing(NewResult) Then
            If btnReplicar.Visible = False Then
                Me._arbol.TreeView.SelectedNode.Remove()
            End If
            NewResult.FlagIndexEdited = True
            Dim NuevoFile As IO.FileInfo
            If Not NewResult.NewFile Is Nothing Then
                NuevoFile = New IO.FileInfo(NewResult.NewFile)
            Else
                NuevoFile = New IO.FileInfo(NewResult.File)
            End If
            Dim ZNewResultNode As New ZNewResultNode(NewResult)
            ZNewResultNode.Text = NuevoFile.Name
            Try
                Dim LoadedIcono As Icon = GetFileIcon(NuevoFile.FullName)
                Dim img As Bitmap = LoadedIcono.ToBitmap()
                img.MakeTransparent(img.GetPixel(0, 0))
                Me._arbol.IL.ZIconList.Images.Add(img)
                Dim ImageIndex As Integer = Me._arbol.IL.ZIconList.Images.Count - 1
                ZNewResultNode.ImageIndex = ImageIndex
                ZNewResultNode.SelectedImageIndex = ImageIndex
            Catch ex As Exception
                ZNewResultNode.ImageIndex = 2
                ZNewResultNode.SelectedImageIndex = 2
                ZClass.raiseerror(ex)
            End Try

            _arbol.TreeInsertados.Nodes.Add(ZNewResultNode)

            If btnReplicar.Visible = False Then
                _docsInsertar -= 1
            End If
            _docsInsertados += 1
            lblInsertar.Text = _docsInsertar.ToString & " DOCUMENTOS POR INSERTAR"
            lblInsertados.Text = _docsInsertados.ToString & " DOCUMENTOS INSERTADOS"
            Try
                If IsNothing(Me.TabViewers.SelectedTab) = False Then DirectCast(Me.TabViewers.SelectedTab, UCDocumentViewer2).CloseDocument()
                '  Me.TabViewers.TabPages.Remove(Me.TabViewers.SelectedTab)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
    End Sub

    Private Sub SelectedIndexChanged(ByVal DocType As Int32)
        Try
            GetActiveDocumentId()

            If _activeId = -1 OrElse _activeId = -2 Then Exit Sub
            PrevioSeleccionado(_activeId)
            Dim NewResult As NewResult = DirectCast(Me._hashPorIndexar(_activeId), NewResult)

            Try
                If _flagAddDocument = False Then

                    If IsNothing(_arbol.DocTypes) = True Then GetFirstDocType()

                    If _arbol.DocTypes.Count > 0 Then
                        If DocType < 0 Then DocType = 0
                        NewResult.DocType = DirectCast(_arbol.DocTypes(DocType), IDocType)
                    End If
                    '                    NewResult.Parent = AllDocTypes(DocType)
                    NewResult.DocType.IsReadOnly = False
                    'NewResult.DocType.IsReindex = True

                    '[Alejandro] 20-11-09 
                    ' Si el documento tiene un formulario virtual asociado
                    ' y se establecio en el usrconfig la opcion DisableInsertButton 
                    ' deshabilitar los botones de insertar y agregar
                    Dim disbutton As Boolean

                    disbutton = CBool(UserPreferences.getValue("DisableInsertButton", Sections.InsertPreferences, False))

                    Me.btnAgregar.Visible = CBool(UserPreferences.getValue("ShowInsertAddButton", Sections.InsertPreferences, True))
                    Me.btnInsertar.Visible = True
                    Me.btnInsertarTodo.Visible = True

                    If Not FormBusiness.GetAllForms(NewResult.DocType.ID) Is Nothing Then
                        If (FormBusiness.GetAllForms(NewResult.DocType.ID)).Length > 0 Then
                            If disbutton = True Then
                                Me.btnAgregar.Visible = False
                                Me.btnInsertar.Visible = False
                                Me.btnInsertarTodo.Visible = False
                            End If
                        End If
                    End If

                    '[Sebastian] 06-07-09 se agrego este blanqueo del form id porque sino cada vez elegimos
                    'un nuevo tipo de documento, en caso de tner un form asociado, me mantiene el form anterior
                    NewResult.CurrentFormID = -1
                    Me.DocType = DirectCast(NewResult.Parent, DocType)

                    '  NewResult.DocTypeName = Arbol.DocTypes(DocType).Name.Trim
                    Try
                        NewResult.DocumentalId = CInt(DirectCast(_arbol.DocTypes(DocType), DocType).DocumentalId)
                    Catch
                        NewResult.DocumentalId = 0
                    End Try
                End If
                Results_Business.LoadVolume(NewResult)
                'FlagVolumeReady = True
                ''''Catch ex As Zamba.AppBlock.VolFullException
                ''''     zclass.raiseerror(ex)
                ''''    'FlagVolumeReady = False
            Catch ex As Exception
                ZClass.raiseerror(ex)
                MessageBox.Show("El volumen asignado no se encuentra disponible", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If Me._flagAddDocument = False Then
                NewResult.Indexs = ZCore.FilterIndex(CInt(NewResult.DocType.ID), True)
                DirectCast(NewResult.Parent, DocType).Indexs = NewResult.Indexs
            End If


            NewResult = Me.ChangeIndexs2(NewResult)
            '_arbol.DesHabilitarSelectedIndexChanged()
            _arbol.MostrarIndices(NewResult)

            'If Me.FlagFirstTimePanels = False Then
            '    Me.ContentTreeview.Width += 1
            '    Me.ContentTreeview.Width -= 1
            '    Me.FlagFirstTimePanels = True
            'End If

            '_arbol.HabilitarSelectedIndexChanged()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub AddResults1(ByVal ArrayResults As ArrayList)
        Try
            'AGREGAR LOS RESULTS AL HASH DE POR INSERTAR
            'ArrayResults.Reverse()
            Dim i As Int32
            For i = 0 To ArrayResults.Count - 1
                Try
                    Me.FillHashPorIndexar(DirectCast(ArrayResults(i), NewResult))
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
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
                    Me.FillHashIndexados(DirectCast(ArrayResults(i), NewResult))
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

    Private Sub InsertarClick(Optional ByVal SetFolder As Boolean = False)

        Dim resul As InsertResult

        Try
            For Each Document As UCDocumentViewer2 In Me.TabViewers.TabPages
                If Document.Result.CurrentFormID <= 0 Then
                    Document.SaveDocument()
                End If
            Next

            Me.btnInsertarTodo.Visible = False
            GetActiveDocumentId()
            PrevioSeleccionado(_activeId)
            Me._setFolderId = SetFolder

            Try
                resul = Insertar(DirectCast(_hashPorIndexar(_activeId), NewResult))
                Me._isDoaddAsoc = False
            Catch ex As RequieredFieldException
                ' MsgBox("Debe completar los Indices obligatorios")
                Exit Sub
            End Try

            If resul = InsertResult.ErrorIndicesIncompletos OrElse resul = InsertResult.ErrorIndicesInvalidos Then
                Exit Sub
            End If

            'Me.ControlTreeview.TreeView.SelectedNode()
            If _arbol.TreePorInsertar.GetNodeCount(True) > 0 Then
                _arbol.TreeView.SelectedNode = Me._arbol.TreePorInsertar.Nodes(0)

                '            Me.ControlTreeview.TreeView.SelectedNode = Me.ControlTreeview.TreePorInsertar.Nodes(0)
                Try
                    'SELECCIONA UN NUEVO RESULT
                    Dim Znode As ZNewResultNode = DirectCast(Me._arbol.TreeView.SelectedNode, ZNewResultNode)
                    Seleccionado(Znode.ZambaCore.ID, False)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Else
                Me.ParentContainer.ReturnToPreviusTabPage()
            End If

            If UserBusiness.Rights.ValidateModuleLicense(ObjectTypes.ModuleExportBaseDeDatos, True) Then
                If DocTypesBusiness.LoadDocTypeRightValue(DocType.ID, DocTypeRights.ExportToDatabase) = 1 Then
                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
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
                    If Me.extVis.Controls.Contains(Sender.Parent) Then
                        Me.extVis.Close()
                    ElseIf IsNothing(Sender.Parent) = True Then
                        Me.extVis.Close()
                    Else
                        extVis = New ExternalVisualizer(DirectCast(Sender.Parent, TabControl))
                        Me.extVis.Show()
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
                    For Each Content As UCDocumentViewer2 In Me.TabViewers.TabPages
                        If TypeOf Content Is UCDocumentViewer2 Then
                            If DirectCast(Content, UCDocumentViewer2).Result.ID = Result.ID Then
                                ucviewer = Content
                                Me.TabViewers.SelectTab(Content)
                                Exit For
                            End If
                        End If
                    Next
                    If Not IsNothing(ucviewer) Then
                        Dim inputbox As New BarcodeViewer(Result)
                        'Ask for the code and the alignment
                        inputbox.ShowDialog()
                        If Not inputbox.texto = "" Then
                            Dim Height As Int32 = Int32.Parse(UserPreferences.getValue("BarCodeHeight", Sections.UserPreferences, "50"))
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
        If _isDoaddAsoc Then _arbol.cboDocType.Enabled = False
    End Sub

    Private Sub PrevioSeleccionado(ByVal SelectedId As Long)
        Dim NewResult As NewResult = DirectCast(Me._hashPorIndexar(SelectedId), NewResult)
        Dim INDICES As New ArrayList
        If Me._arbol.UcIndexs.GetIndexs.Count = 0 Then
            If IsNothing(Me.Indexs) = False AndAlso Me.Indexs.Count > 0 Then INDICES = Me.Indexs
        Else
            INDICES = Me._arbol.UcIndexs.GetIndexs
        End If

        If Not IsNothing(NewResult) Then
            For Each Index As Index In INDICES
                For Each Index2 As Index In NewResult.Indexs
                    If Index2.ID = Index.ID Then
                        Index2.DataTemp = Index.DataTemp
                        Index2.Data = Index.DataTemp
                        Index2.dataDescription = Index.dataDescriptionTemp
                        Index2.dataDescriptionTemp = Index.dataDescriptionTemp
                        Exit For
                    End If
                Next

            Next

            NewResult.Parent = DirectCast(Me._arbol.cboDocType.SelectedItem, IZambaCore)

            _hashPorIndexar.Remove(NewResult.ID)
            FillHashPorIndexar(NewResult)
        End If
    End Sub

    Private Sub Seleccionado_Insertado(ByVal SelectedId As Long)
        Try
            Me.SuspendLayout()

            RemoveHandler TabViewers.TabIndexChanged, AddressOf TabViewersIndexChanged

            If _flagInsertingAll = True Then Exit Sub

            Dim NewResult As NewResult = DirectCast(Me._hashIndexados(SelectedId), NewResult)
            'NewResult.DocType.IsReindex = False
            Me._arbol.MostrarIndices(NewResult)

            'SE FIJA SI ESTA EN PANTALLA

            For Each Content As UCDocumentViewer2 In Me.TabViewers.TabPages
                If TypeOf Content Is UCDocumentViewer2 Then
                    If DirectCast(Content, UCDocumentViewer2).Result.ID = SelectedId Then
                        Me.TabViewers.SelectedTab = Content
                        Exit Sub
                    End If
                End If
            Next

            'SI NO CREA UN CONTENT NUEVO Y LO MUESTRA

            Dim UcViewer As New UCDocumentViewer2(NewResult, False)
            UcViewer.Text = NewResult.Name
            UcViewer.Tag = NewResult.Name
            Me.TabViewers.TabPages.Add(UcViewer)
            Me.TabViewers.SelectTab(Me.TabViewers.TabPages.Count - 1)
            UcViewer.ShowDocument()
            RemoveHandler UcViewer.CambiarDock, AddressOf Me.CambiarDock
            AddHandler UcViewer.CambiarDock, AddressOf Me.CambiarDock
            RemoveHandler UcViewer.Ver_Original, AddressOf ShowOriginal
            AddHandler UcViewer.Ver_Original, AddressOf ShowOriginal

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            AddHandler TabViewers.TabIndexChanged, AddressOf TabViewersIndexChanged
            Me.ResumeLayout()
        End Try
    End Sub

    ''' <summary>
    ''' Cuando se selecciona un item, carga los indices y la visualizacion del mismo
    ''' </summary>
    ''' <param name="SelectedId"></param>
    ''' <history>   
    '''     Marcelo modified 05/02/2009
    '''     Javier  modified 25/11/2010           
    ''' </history>
    ''' <remarks></remarks>
    Private Sub Seleccionado(ByVal SelectedId As Int64, ByVal InsertedDoc As Boolean)

        Try
            Me.SuspendLayout()
            RemoveHandler TabViewers.TabIndexChanged, AddressOf TabViewersIndexChanged
            If _flagInsertingAll = True Then Exit Sub

            Dim NewResult As NewResult = DirectCast(Me._hashPorIndexar(SelectedId), NewResult)
            If Not IsNothing(NewResult) Then
                If _doctypeid = 0 Then
                    'NewResult.DocType.IsReindex = True
                    NewResult = Me.ChangeIndexs(NewResult)
                    'If Me.FlagFirstTimePanels = False Then
                    '    Me.ContentTreeview.Width += 1
                    '    Me.ContentTreeview.Width -= 1
                    '    Me.FlagFirstTimePanels = True
                    'End If
                    '_arbol.MostrarIndices(NewResult)
                End If
                'SE FIJA SI ESTA EN PANTALLA
                For Each Content As UCDocumentViewer2 In Me.TabViewers.TabPages
                    If TypeOf Content Is UCDocumentViewer2 Then
                        Dim Forms() As ZwebForm = FormBusiness.GetShowAndEditForms(Int32.Parse(DirectCast(Content, UCDocumentViewer2).Result.DocType.ID.ToString))
                        'Se fija si la solapa ya se encuentra seleccionada
                        If DirectCast(Content, UCDocumentViewer2).Result.ID = SelectedId And IsNothing(Forms) = True _
                            And ViewerChanged = False Then

                            Me.TabViewers.SelectTab(Content)
                            ViewerChanged = False
                            Exit Sub

                        ElseIf DirectCast(Content, UCDocumentViewer2).Result.ID = SelectedId And IsNothing(Forms) = False Then

                            Me.TabViewers.TabPages.Remove(Me.TabViewers.SelectedTab)
                            ViewerChanged = True
                        ElseIf DirectCast(Content, UCDocumentViewer2).Result.ID = SelectedId And ViewerChanged = True _
                                And IsNothing(Forms) = True Then

                            Me.TabViewers.TabPages.Remove(Me.TabViewers.SelectedTab)
                            ViewerChanged = False
                        ElseIf DirectCast(Content, UCDocumentViewer2).Result.ID = SelectedId And ViewerChanged = True _
                            And IsNothing(Forms) = False Then

                            Me.TabViewers.TabPages.Remove(Me.TabViewers.SelectedTab)
                            ViewerChanged = True
                        End If

                        'Se fija si el archivo se encuentra abierto en otro tab
                        'Este caso se puede dar al abrir intentar cargar un documento 2 veces sin haberlos insertado.
                        '[Sebastian 04-06-2009] se mejoro la codición, porque si el tab no existia lanza  exception
                        If String.Equals(NewResult.FullPath.Trim, DirectCast(Content, UCDocumentViewer2).Result.FullPath.Trim) AndAlso Me.TabViewers.Contains(Content) = True Then
                            Me.TabViewers.SelectTab(Content)
                            'MessageBox.Show("El documento que desea visualizar se encuentra abierto actualmente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If

                    End If
                Next

                'SI NO CREA UN CONTENT NUEVO Y LO MUESTRA
                Dim UcViewer As New UCDocumentViewer2(NewResult, False)
                UcViewer.Text = NewResult.Name
                UcViewer.Tag = NewResult.Name
                Me.TabViewers.TabPages.Add(UcViewer)
                Me.TabViewers.SelectTab(UcViewer)
                'Oculto los botones que no se tienen que mostrar al antes de insertar un doc.
                UcViewer.HideButtons()
                'Si el LastFolderId esta cargado es porque se esta usando el incorporar documento, en ese caso no se
                'deben ver los formularios virtuales asi el usuario ve q documento esta ingresando
                If LastFolderId <> 0 Then
                    Try
                        If UserPreferences.getValue("ShowFormOnAddAssociatedDocument", Sections.UserPreferences, "False") Then
                            UcViewer.ShowDocument(True, False, True, False)
                        Else
                            If Not IsNothing(UcViewer.Result) Then
                                UcViewer.Result.CurrentFormID = 0
                                UcViewer.ShowDocument(False)
                            End If
                        End If
                    Catch ex As Exception
                        UcViewer.ShowDocument(False)
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                    'UcViewer.ShowDocument(False)
                Else
                    If IsNothing(FormBusiness.GetShowAndEditForms(CInt(NewResult.DocType.ID))) = True Then
                        UcViewer.ShowDocument(False, False, InsertedDoc, False)
                    Else
                        UcViewer.ShowDocument(True, False, InsertedDoc, False)
                    End If
                End If
                RemoveHandler UcViewer.CambiarDock, AddressOf Me.CambiarDock
                AddHandler UcViewer.CambiarDock, AddressOf Me.CambiarDock
                RemoveHandler UcViewer.Ver_Original, AddressOf ShowOriginal
                AddHandler UcViewer.Ver_Original, AddressOf ShowOriginal
                'RemoveHandler UcViewer.formclose, AddressOf Me.formclose
                'AddHandler UcViewer.formclose, AddressOf Me.formclose

                '[Alejandro] 20/11/09 - Created
                RemoveHandler UcViewer.SaveDocumentWithVirtualForms, AddressOf SaveDocumentVirtualForm
                AddHandler UcViewer.SaveDocumentWithVirtualForms, AddressOf SaveDocumentVirtualForm

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            AddHandler TabViewers.TabIndexChanged, AddressOf TabViewersIndexChanged
            Me.ResumeLayout()
        End Try
    End Sub

    '[Alejandro] 20/11/09 - Created
    Private Sub SaveDocumentVirtualForm()
        Dim NewRes As NewResult = DirectCast(_hashPorIndexar(_activeId), NewResult)
        FillHashIndexados(NewRes)
        ActualizarControles(NewRes)
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
        For Each dc As TabPage In Me.TabViewers.TabPages
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
            Me.TabViewers.TabPages.Add(TabTarea)
            Me.TabViewers.SelectTab(TabTarea)
            'Configura la pestaña
            TabTarea.HideButtons()
            TabTarea.VerOriginalButtonVisible = False
            RemoveHandler TabTarea.CambiarDock, AddressOf Me.CambiarDock
            AddHandler TabTarea.CambiarDock, AddressOf Me.CambiarDock
            'Muestro el documento
            TabTarea.ShowDocument(False)
        End If

    End Sub
#Region "Split"

    Public Sub Split(ByVal Viewer As System.Windows.Forms.TabPage, ByVal Splited As Boolean) Implements Core.IViewerContainer.Split
        'No se implementa esta funcion, ya que no estan funcionales las demas solapas en esta instancia en el container
    End Sub

#End Region
    Private Sub TabViewersIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If IsNothing(Me.TabViewers.SelectedTab) = False Then

                Dim ContentDocumento As UCDocumentViewer2 = DirectCast(Me.TabViewers.SelectedTab, UCDocumentViewer2)
                Dim Id As Int32 = CInt(ContentDocumento.Result.ID)
                For Each Node As ZNewResultNode In Me._arbol.TreePorInsertar.Nodes
                    If Node.ZambaCore.ID = Id Then
                        Me._arbol.TreeView.SelectedNode = Node
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
            Me.TabMainControl.SelectedTab = Me.TabIndexer
            Me.SuspendLayout()

            _browserFiles = New BrowserFiles
            _browserFiles.Dock = DockStyle.Fill
            Me.TabSelectDocuments.Controls.Add(_browserFiles)

            _arbol = New Arbol
            _arbol.Dock = DockStyle.Fill
            SplitContainer1.Panel1.Controls.Add(_arbol)

            RemoveHandler _browserFiles.AddFiles, AddressOf AddFiles
            RemoveHandler _browserFiles.ShowResult, AddressOf InsertVirtualResult
            RemoveHandler _arbol.Seleccionado, AddressOf Seleccionado
            RemoveHandler _arbol.Seleccionado_Insertado, AddressOf Seleccionado_Insertado
            RemoveHandler _arbol.ShowBrowser, AddressOf ShowSelectDocumentsTab
            RemoveHandler _arbol.Eliminado, AddressOf Eliminado
            RemoveHandler _arbol.EliminadosTodos1, AddressOf EliminadosTodos1
            RemoveHandler _arbol.EliminadosTodos2, AddressOf EliminadosTodos2
            RemoveHandler _arbol.PrevioSeleccionado, AddressOf PrevioSeleccionado
            RemoveHandler _arbol.SeleccionadoParent, AddressOf SeleccionadoParent
            RemoveHandler _arbol.SelectedIndexChanged, AddressOf SelectedIndexChanged

            AddHandler _browserFiles.AddFiles, AddressOf AddFiles
            AddHandler _browserFiles.ShowResult, AddressOf InsertVirtualResult
            AddHandler _arbol.Seleccionado, AddressOf Seleccionado
            AddHandler _arbol.Seleccionado_Insertado, AddressOf Seleccionado_Insertado
            AddHandler _arbol.ShowBrowser, AddressOf ShowSelectDocumentsTab
            AddHandler _arbol.Eliminado, AddressOf Eliminado
            AddHandler _arbol.EliminadosTodos1, AddressOf EliminadosTodos1
            AddHandler _arbol.EliminadosTodos2, AddressOf EliminadosTodos2
            AddHandler _arbol.PrevioSeleccionado, AddressOf PrevioSeleccionado
            AddHandler _arbol.SelectedIndexChanged, AddressOf SelectedIndexChanged
            AddHandler _arbol.SeleccionadoParent, AddressOf SeleccionadoParent

            Me.TabMainControl.SelectedTab = Me.TabSelectDocuments
            Me.ResumeLayout()

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ShowSelectDocumentsTab()
        Try
            Me.TabMainControl.SelectTab(Me.TabSelectDocuments)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ShowVisualizadorContent()
        Try
            Me.TabMainControl.SelectTab(Me.TabIndexer)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub EliminadosTodos1()
        _docsInsertar = 0
        'lblInsertar.Text = _docsInsertar.ToString & " DOCUMENTOS POR INSERTAR"
        lblInsertar.Text = "0 DOCUMENTOS POR INSERTAR"

        'Dim Results As New ArrayList
        'Results.AddRange(Me._hashPorIndexar.Values)
        'For Each NewResult As NewResult In Results
        '    CloseDocument(NewResult.ID)
        'Next

        For Each NewResult As NewResult In _hashPorIndexar.Values
            CloseDocument(NewResult.ID)
        Next
        _hashPorIndexar.Clear()
    End Sub

    Private Sub EliminadosTodos2()
        _docsInsertados = 0
        'Me.lblInsertados.Text = _docsInsertados.ToString & " DOCUMENTOS INSERTADOS"
        lblInsertados.Text = "0 DOCUMENTOS INSERTADOS"
        Dim Results As New ArrayList
        Results.AddRange(Me._hashIndexados.Values)
        For Each NewResult As NewResult In Results
            CloseDocument(NewResult.ID)
        Next
        Me._hashIndexados.Clear()
    End Sub

    Private Sub CloseDocument(ByVal Id As Long)
        Try
            For Each Document As UCDocumentViewer2 In Me.TabViewers.TabPages
                If Document.Result.ID = Id Then
                    Document.Dispose()
                    Exit For
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub Eliminado(ByVal Node As ZNewResultNode)
        Me._docsInsertar -= 1
        Me.lblInsertar.Text = _docsInsertar.ToString & " DOCUMENTOS POR INSERTAR"
        Dim NewResult As NewResult = DirectCast(Node.ZambaCore, NewResult)
        Me._hashPorIndexar.Remove(NewResult.ID)
        Me.TabViewers.TabPages.Remove(Me.TabViewers.SelectedTab)
    End Sub

    Private Sub GetActiveDocumentId()
        Try
            If String.Compare(Me._arbol.TreeView.SelectedNode.GetType.ToString, "System.Windows.Forms.TreeNode", True) = 0 Then
                _activeId = -2
                Exit Sub
            End If

            Dim ZNode As ZNewResultNode = DirectCast(Me._arbol.TreeView.SelectedNode, ZNewResultNode)
            _activeId = ZNode.ZambaCore.ID
        Catch ex As Exception
            _activeId = -1
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub FillHashIndexados(ByVal NewResult As NewResult)
        Me._hashIndexados.Add(NewResult.ID, NewResult)
    End Sub

    Private Sub FillHashPorIndexar(ByVal NewResult As NewResult)
        Me._hashPorIndexar.Add(NewResult.ID, NewResult)
    End Sub

#Region "Eventos"


    Private Sub DesignSandBar_ButtonClick(ByVal sender As System.Object, ByVal e As TD.SandBar.ToolBarItemEventArgs) Handles DesignSandBar.ButtonClick
        Select Case CStr(e.Item.Tag)
            Case "ATRAS"
                Try
                    Me._arbol.UcIndexs.Focus()
                    Me._arbol.TreeView.SelectedNode = Me._arbol.TreeView.SelectedNode.PrevNode
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Case "ADELANTE"
                Try
                    Me._arbol.UcIndexs.Focus()
                    Me._arbol.TreeView.SelectedNode = Me._arbol.TreeView.SelectedNode.NextNode
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Case "INSERTAR"
                Me._arbol.UcIndexs.Focus()
                If Me._arbol.UcIndexs.IsValid Then
                    InsertarClick()
                End If
            Case "INSERTARTODO"
                Try
                    Me._arbol.UcIndexs.Focus()

                    If MessageBox.Show("¿DESEA INSERTAR TODOS LOS DOCUMENTOS CON LOS INDICES ESPECIFICADOS?", "", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

                    Me.GetActiveDocumentId()

                    Dim firstresult As NewResult

                    firstresult = DirectCast(Me._hashPorIndexar(_activeId), NewResult)

                    If Not IsNothing(firstresult) Then
                        _flagInsertingAll = True
                        Insertar(firstresult)

                        Me._setFolderId = True

                        Dim ArrayResults As New ArrayList
                        ArrayResults.AddRange(Me._hashPorIndexar.Values)

                        For Each NewResult As NewResult In ArrayResults
                            If Not IsNothing(NewResult) Then
                                If firstresult.ID <> NewResult.ID Then
                                    Dim XResult As NewResult
                                    XResult = Results_Business.CloneResult(firstresult, NewResult.File, False, True)
                                    If Not IsNothing(XResult) Then
                                        Insertar(XResult)
                                    End If
                                End If
                            End If
                        Next
                    End If

                    ' Se actualiza el U_TIME del usuario, un campo que indica el horario de cuando fue la última acción realizada por el usuario
                    ' En este caso, se actualizaría cuando se presiona el botón Insertar
                    UserBusiness.Rights.SaveAction(2, ObjectTypes.ModuleInsert, RightsType.insert, "Se inserto documento/s")

                Catch ex As Exception
                    ZClass.raiseerror(ex)
                Finally
                    _flagInsertingAll = False
                End Try
            Case "AGREGAR"
                Me._arbol.UcIndexs.Focus()
                If Me._arbol.UcIndexs.IsValid Then
                    InsertarClick(True)
                End If
                'UserBusiness.Rights.SaveAction(2, ObjectTypes.ModuleInsert, RightsType.AgregarDocumento)
            Case "REALIZAROCR"
                Me.chkRealizarOcr.Checked = Not Me.chkRealizarOcr.Checked
            Case "REPLICAR"
                BtnNOReplicar.Visible = True
                btnReplicar.Visible = False
            Case "NOREPLICAR"
                BtnNOReplicar.Visible = False
                btnReplicar.Visible = True
            Case "BARCODE"
                Me.GetActiveDocumentId()
                If Not IsNothing(Me._hashPorIndexar(_activeId)) Then
                    MostrarBarcode(DirectCast(_hashPorIndexar(_activeId), NewResult))
                Else
                    MostrarBarcode(DirectCast(_hashPorIndexar(_activeId), NewResult))
                End If
        End Select
    End Sub

    Private Sub Indexer6000_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        Try
            '            If Me.PublicFolderId > 0 Then
            'Me.Arbol.cboDocType.SelectedValue = Me.PublicDocTypeId
            'Me.Arbol.cboDocType.Text = Me.DocType.Name
            '           End If
        Catch ex As Exception
        End Try

        Me._hashPorIndexar.Clear()
        Me._hashIndexados.Clear()
        Me._docsInsertados = 0
        Me._docsInsertar = 0

        Try
            Me._arbol.AddSpecialNodes()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Me.BtnAgregarBarcode.Visible = Boolean.Parse(UserPreferences.getValue("UseBarcode", Sections.UserPreferences, True))
        Me.BtnNOReplicar.Visible = Boolean.Parse(UserPreferences.getValue("ReplicarIndexer", Sections.UserPreferences, True))
        Me.btnReplicar.Visible = False
        Me.chkRealizarOcr.Visible = Boolean.Parse(UserPreferences.getValue("UseOcr", Sections.UserPreferences, True))
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

    Public Sub DisableDTCombo(ByVal B As Boolean)
        Me._isDoaddAsoc = B
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
    ''' Method to execute directly Inserta Doc, Insertar Form, Scan Document or Insert Folder Option
    ''' </summary>
    ''' <param name="opt"></param>
    ''' <history>
    '''     Javier  30/12/2010  Created
    ''' </history>
    Public Sub OpenOptionToInsert(ByVal opt As Int32)
        Me._browserFiles.OpenOptionToInsert(opt)
    End Sub

End Class


'Private Sub formclose(ByVal sender As UCDocumentViewer2)
'    Me.TabViewers.TabPages.Remove(sender)
'End Sub
'''Add to the existing Word Document a Barcode
'Private Sub ContentActivated2(ByVal sender As Object, ByVal e As System.EventArgs)
'    Try
'        Dim ContentDocumento As UCDocumentViewer2 = Me.DockPanel.ActiveDocument
'        Dim Id As Int32 = ContentDocumento.Result.Id

'        For Each Node As ZNewResultNode In Me.Arbol.TreeInsertados.Nodes
'            If Node.ZambaCore.Id = Id Then
'                Me.Arbol.TreeView.SelectedNode = Node
'                Node.EnsureVisible()
'                Exit Sub
'            End If
'        Next
'    Catch ex As Exception
'        zclass.raiseerror(ex)
'    End Try
'End Sub
'Private Sub CambiarDock(ByVal Sender As TabPage, ByVal Expander As Boolean)
'    Try
'        If Expander = True Then
'            'Sender.Dock = DockStyle.None
'            'RaiseEvent CambiarDockEvent(Sender, Expander)
'            _extVis = New ExternalVisualizer(Sender)
'            _extVis.Show()
'        Else
'            'DirectCast(Sender, UCDocumentViewer2).parentName = Me.Name
'            'Me.TabViewers.TabPages.Add(Sender)
'            'RaiseEvent CambiarDockEvent(Sender, Expander)
'            Me.TabViewers.TabPages.Add(Sender)
'            Me.TabViewers.SelectTab(Sender)
'            If Not IsNothing(_extVis) Then
'                _extVis.Close()
'            End If
'        End If
'    Catch ex As Exception
'        zclass.raiseerror(ex)
'    End Try
'End Sub
