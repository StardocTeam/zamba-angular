Imports Zamba.Filters
Imports Zamba.Core
Imports Zamba.Indexs
Imports System.Collections.Generic
Imports Telerik.WinControls.UI
'Comente esta linea porque tiraba warning de ningun metodo disponible - Marcelo
'Imports Zamba.Barcode.Factory

Public Class UCBarCode
    Inherits Zamba.AppBlock.ZControl
    Implements IGrid

#Region "Atributos"
    Friend WithEvents tablaCaratulas As Zamba.Grid.Grid.GroupGrid
#End Region

#Region "Variables"
    Private _barcodeImage As Graphics
    'Private _currentThread As Thread
    Private _loadedResult As IResult
    Private RePrint As Int16 = 0
    Private CoverIdToPrint As Int32 = 0
    Public Property CurrentResult() As IResult
        Get
            Return _loadedResult
        End Get
        Set(ByVal value As IResult)
            _loadedResult = value
        End Set
    End Property
    Private _tmpDocIdTypeList As List(Of DocItem)
    Private CurrentUserId As Int64
    Private _barcodeId As Integer
    Private _oldId As Int32
    Private WithEvents pdocum As New Printing.PrintDocument

    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents cboDocType As ListBox
    Friend WithEvents chkReplicas As CheckBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents PanelIndexs As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents txtRemark As TextBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents chkPrintIndex As CheckBox
    Friend WithEvents PanelListView As Panel
    Friend WithEvents vista As Panel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents panelMostarCaratulas As Panel
    Friend WithEvents panelRangoFechas As Panel
    Friend WithEvents btVer As ZButton
    Friend WithEvents rbCaratulasPropiasPorFechas As RadioButton
    Friend WithEvents dtpFechaInicial As DateTimePicker
    Friend WithEvents lbFechaFinal As ZLabel
    Friend WithEvents dtpFechaFinal As DateTimePicker
    Friend WithEvents lbFechaInicial As ZLabel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents rbCaratulasUsuarioPropiasHoy As RadioButton
    Friend WithEvents rbCaratulasUsuarioPropias As RadioButton
    Friend WithEvents ToolBar1 As ZToolBar
    Friend WithEvents BtnPrint As ToolStripButton
    Friend WithEvents ButtonItem5 As ToolStripButton
    Friend WithEvents BtnReplicar As ToolStripButton
    Friend WithEvents ButtonItem3 As ToolStripButton
    Friend WithEvents ButtonItem4 As ToolStripButton
    Friend WithEvents Panel4 As Panel
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents PnlLastCaratulas As ZPanel
    Friend WithEvents btnRePrint As ZButton


    Private WithEvents pdocumdoctypes As New Printing.PrintDocument
    'Dim UserId As Integer
    'Dim DocTypeId As Integer
#End Region

#Region "Delegados"
    Private Delegate Sub dDelegateWithOutParams()
    Private Delegate Sub dloadViewCaratulas(ByVal dt As DataTable)
#End Region

#Region "Constructores"
    Public Sub New(ByVal CurrentUserId As Int64)
        MyBase.New()
        InitializeComponent()
        ToolBar1.BackColor = AppBlock.ZambaUIHelpers.GetToolbarsAndButtonsColor
        ToolBar1.ForeColor = AppBlock.ZambaUIHelpers.GetToolbarsAndButtonsFontColor
        ToolBar1.Font = AppBlock.ZambaUIHelpers.GetFontFamily

        Me.CurrentUserId = CurrentUserId
        _fc = New FiltersComponent
        _tmpDocIdTypeList = New List(Of DocItem)

        tablaCaratulas = New Grid.Grid.GroupGrid(False, CurrentUserId, Me, FilterTypes.History)
        tablaCaratulas.BackColor = Color.White
        tablaCaratulas.BorderStyle = BorderStyle.None
        tablaCaratulas.Dock = DockStyle.Fill
        tablaCaratulas.Location = New Point(0, 21)
        tablaCaratulas.Name = "tablaCaratulas"
        tablaCaratulas.ShortDateFormat = False
        tablaCaratulas.NewGrid.MultiSelect = False
        tablaCaratulas.NewGrid.SelectionMode = GridViewSelectionMode.FullRowSelect
        'Me.tablaCaratulas.Size = New System.Drawing.Size(593, 375)
        tablaCaratulas.TabIndex = 30
        tablaCaratulas.ShowFiltersPanel = False
        tablaCaratulas.AllowTelerikGridFilter = True
        vista.Controls.Add(tablaCaratulas)
        tablaCaratulas.BringToFront()

        RemoveHandler tablaCaratulas.OnRowClick, AddressOf tablaCaratulas_Click
        AddHandler tablaCaratulas.OnRowClick, AddressOf tablaCaratulas_Click
    End Sub

    Public Sub New(ByRef Result As Result, ByVal CurrentUserId As Int64)
        Me.New(CurrentUserId)
        _loadedResult = Result
    End Sub
#End Region

#Region " Código generado por el Diseñador de Windows Forms "

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Public Shadows Event Finish()
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            If _ucViewer IsNot Nothing Then
                _ucViewer.Dispose()
                _ucViewer = Nothing
            End If
        End If
        MyBase.Dispose(disposing)
        RaiseEvent Finish()
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents ContextMenu1 As ContextMenu
    Friend WithEvents mnuEliminar As System.Windows.Forms.MenuItem

    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCBarCode))
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.mnuEliminar = New System.Windows.Forms.MenuItem()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkReplicas = New System.Windows.Forms.CheckBox()
        Me.cboDocType = New System.Windows.Forms.ListBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.PanelIndexs = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label5 = New Zamba.AppBlock.ZLabel()
        Me.chkPrintIndex = New System.Windows.Forms.CheckBox()
        Me.PanelListView = New System.Windows.Forms.Panel()
        Me.vista = New System.Windows.Forms.Panel()
        Me.PnlLastCaratulas = New Zamba.AppBlock.ZPanel()
        Me.btnRePrint = New Zamba.AppBlock.ZButton()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.panelMostarCaratulas = New System.Windows.Forms.Panel()
        Me.panelRangoFechas = New System.Windows.Forms.Panel()
        Me.btVer = New Zamba.AppBlock.ZButton()
        Me.dtpFechaInicial = New System.Windows.Forms.DateTimePicker()
        Me.lbFechaFinal = New Zamba.AppBlock.ZLabel()
        Me.dtpFechaFinal = New System.Windows.Forms.DateTimePicker()
        Me.lbFechaInicial = New Zamba.AppBlock.ZLabel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.rbCaratulasPropiasPorFechas = New System.Windows.Forms.RadioButton()
        Me.rbCaratulasUsuarioPropias = New System.Windows.Forms.RadioButton()
        Me.rbCaratulasUsuarioPropiasHoy = New System.Windows.Forms.RadioButton()
        Me.ToolBar1 = New Zamba.AppBlock.ZToolBar()
        Me.BtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ButtonItem5 = New System.Windows.Forms.ToolStripButton()
        Me.BtnReplicar = New System.Windows.Forms.ToolStripButton()
        Me.ButtonItem3 = New System.Windows.Forms.ToolStripButton()
        Me.ButtonItem4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.PanelListView.SuspendLayout()
        Me.vista.SuspendLayout()
        Me.PnlLastCaratulas.SuspendLayout()
        Me.panelMostarCaratulas.SuspendLayout()
        Me.panelRangoFechas.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.ToolBar1.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = """xml"""
        Me.OpenFileDialog1.Filter = "xml files (*.xml) |*.xml"
        Me.OpenFileDialog1.RestoreDirectory = True
        Me.OpenFileDialog1.ShowHelp = True
        Me.OpenFileDialog1.ShowReadOnly = True
        Me.OpenFileDialog1.Title = """Select a xml file"""
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuEliminar})
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Index = 0
        Me.mnuEliminar.Text = "Eliminar"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BackColor = System.Drawing.Color.DimGray
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(2, 41)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1118, 758)
        Me.SplitContainer2.SplitterDistance = 307
        Me.SplitContainer2.SplitterWidth = 1
        Me.SplitContainer2.TabIndex = 110
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.chkReplicas)
        Me.Panel1.Controls.Add(Me.cboDocType)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(15)
        Me.Panel1.Size = New System.Drawing.Size(307, 758)
        Me.Panel1.TabIndex = 108
        '
        'chkReplicas
        '
        Me.chkReplicas.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkReplicas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.chkReplicas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkReplicas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkReplicas.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReplicas.ForeColor = System.Drawing.Color.Blue
        Me.chkReplicas.Location = New System.Drawing.Point(17, 721)
        Me.chkReplicas.Name = "chkReplicas"
        Me.chkReplicas.Size = New System.Drawing.Size(200, 29)
        Me.chkReplicas.TabIndex = 26
        Me.chkReplicas.Text = "Imprimir Replicas"
        Me.chkReplicas.Visible = False
        '
        'cboDocType
        '
        Me.cboDocType.BackColor = System.Drawing.Color.White
        Me.cboDocType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.cboDocType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboDocType.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDocType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.cboDocType.ItemHeight = 18
        Me.cboDocType.Location = New System.Drawing.Point(15, 15)
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(277, 728)
        Me.cboDocType.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.Color.DimGray
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.PanelIndexs)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.PanelListView)
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Panel2MinSize = 400
        Me.SplitContainer1.Size = New System.Drawing.Size(810, 758)
        Me.SplitContainer1.SplitterDistance = 25
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 0
        '
        'PanelIndexs
        '
        Me.PanelIndexs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.PanelIndexs.BackColor = System.Drawing.Color.White
        Me.PanelIndexs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelIndexs.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelIndexs.Location = New System.Drawing.Point(0, 0)
        Me.PanelIndexs.MinimumSize = New System.Drawing.Size(292, 0)
        Me.PanelIndexs.Name = "PanelIndexs"
        Me.PanelIndexs.Size = New System.Drawing.Size(810, 657)
        Me.PanelIndexs.TabIndex = 22
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel3.Controls.Add(Me.txtRemark)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.chkPrintIndex)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 657)
        Me.Panel3.MinimumSize = New System.Drawing.Size(292, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(810, 101)
        Me.Panel3.TabIndex = 108
        '
        'txtRemark
        '
        Me.txtRemark.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRemark.BackColor = System.Drawing.Color.White
        Me.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemark.ForeColor = System.Drawing.Color.Black
        Me.txtRemark.Location = New System.Drawing.Point(17, 22)
        Me.txtRemark.MaxLength = 200
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(768, 50)
        Me.txtRemark.TabIndex = 106
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label5.FontSize = 9.75!
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(4, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 20)
        Me.Label5.TabIndex = 107
        Me.Label5.Text = "Nota :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkPrintIndex
        '
        Me.chkPrintIndex.AutoSize = True
        Me.chkPrintIndex.BackColor = System.Drawing.Color.Transparent
        Me.chkPrintIndex.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkPrintIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkPrintIndex.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintIndex.ForeColor = System.Drawing.Color.Black
        Me.chkPrintIndex.Location = New System.Drawing.Point(6, 78)
        Me.chkPrintIndex.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
        Me.chkPrintIndex.Name = "chkPrintIndex"
        Me.chkPrintIndex.Size = New System.Drawing.Size(557, 24)
        Me.chkPrintIndex.TabIndex = 102
        Me.chkPrintIndex.Text = " Imprimir atributos en blanco y códigos de barra de atributos"
        Me.chkPrintIndex.UseVisualStyleBackColor = False
        '
        'PanelListView
        '
        Me.PanelListView.BackColor = System.Drawing.Color.White
        Me.PanelListView.Controls.Add(Me.vista)
        Me.PanelListView.Controls.Add(Me.panelMostarCaratulas)
        Me.PanelListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelListView.Location = New System.Drawing.Point(0, 0)
        Me.PanelListView.MinimumSize = New System.Drawing.Size(533, 0)
        Me.PanelListView.Name = "PanelListView"
        Me.PanelListView.Size = New System.Drawing.Size(533, 100)
        Me.PanelListView.TabIndex = 21
        '
        'vista
        '
        Me.vista.AutoSize = True
        Me.vista.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.vista.BackColor = System.Drawing.Color.White
        Me.vista.Controls.Add(Me.PnlLastCaratulas)
        Me.vista.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vista.Location = New System.Drawing.Point(0, 100)
        Me.vista.Name = "vista"
        Me.vista.Size = New System.Drawing.Size(533, 0)
        Me.vista.TabIndex = 4
        '
        'PnlLastCaratulas
        '
        Me.PnlLastCaratulas.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.PnlLastCaratulas.Controls.Add(Me.btnRePrint)
        Me.PnlLastCaratulas.Controls.Add(Me.Label2)
        Me.PnlLastCaratulas.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlLastCaratulas.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlLastCaratulas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.PnlLastCaratulas.Location = New System.Drawing.Point(0, 0)
        Me.PnlLastCaratulas.Name = "PnlLastCaratulas"
        Me.PnlLastCaratulas.Size = New System.Drawing.Size(533, 34)
        Me.PnlLastCaratulas.TabIndex = 30
        '
        'btnRePrint
        '
        Me.btnRePrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnRePrint.FlatAppearance.BorderSize = 0
        Me.btnRePrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRePrint.ForeColor = System.Drawing.Color.White
        Me.btnRePrint.Location = New System.Drawing.Point(3, 5)
        Me.btnRePrint.Name = "btnRePrint"
        Me.btnRePrint.Size = New System.Drawing.Size(83, 25)
        Me.btnRePrint.TabIndex = 30
        Me.btnRePrint.Text = "Reimprimir"
        Me.btnRePrint.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Gainsboro
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label2.Font = New System.Drawing.Font("Verdana", 10.08!)
        Me.Label2.FontSize = 10.08!
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(533, 34)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Caratulas Anteriores"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'panelMostarCaratulas
        '
        Me.panelMostarCaratulas.AutoSize = True
        Me.panelMostarCaratulas.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.panelMostarCaratulas.BackColor = System.Drawing.Color.White
        Me.panelMostarCaratulas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelMostarCaratulas.Controls.Add(Me.panelRangoFechas)
        Me.panelMostarCaratulas.Controls.Add(Me.Panel4)
        Me.panelMostarCaratulas.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelMostarCaratulas.ForeColor = System.Drawing.Color.Black
        Me.panelMostarCaratulas.Location = New System.Drawing.Point(0, 0)
        Me.panelMostarCaratulas.MinimumSize = New System.Drawing.Size(0, 100)
        Me.panelMostarCaratulas.Name = "panelMostarCaratulas"
        Me.panelMostarCaratulas.Size = New System.Drawing.Size(533, 100)
        Me.panelMostarCaratulas.TabIndex = 113
        '
        'panelRangoFechas
        '
        Me.panelRangoFechas.Controls.Add(Me.btVer)
        Me.panelRangoFechas.Controls.Add(Me.dtpFechaInicial)
        Me.panelRangoFechas.Controls.Add(Me.lbFechaFinal)
        Me.panelRangoFechas.Controls.Add(Me.dtpFechaFinal)
        Me.panelRangoFechas.Controls.Add(Me.lbFechaInicial)
        Me.panelRangoFechas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelRangoFechas.Location = New System.Drawing.Point(0, 25)
        Me.panelRangoFechas.Name = "panelRangoFechas"
        Me.panelRangoFechas.Size = New System.Drawing.Size(531, 73)
        Me.panelRangoFechas.TabIndex = 122
        '
        'btVer
        '
        Me.btVer.BackColor = System.Drawing.Color.White
        Me.btVer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btVer.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btVer.ForeColor = System.Drawing.Color.Black
        Me.btVer.Location = New System.Drawing.Point(319, 34)
        Me.btVer.Margin = New System.Windows.Forms.Padding(0)
        Me.btVer.Name = "btVer"
        Me.btVer.Size = New System.Drawing.Size(69, 23)
        Me.btVer.TabIndex = 114
        Me.btVer.Text = "Aplicar"
        Me.btVer.UseVisualStyleBackColor = False
        '
        'dtpFechaInicial
        '
        Me.dtpFechaInicial.Location = New System.Drawing.Point(64, 5)
        Me.dtpFechaInicial.Name = "dtpFechaInicial"
        Me.dtpFechaInicial.Size = New System.Drawing.Size(247, 27)
        Me.dtpFechaInicial.TabIndex = 120
        '
        'lbFechaFinal
        '
        Me.lbFechaFinal.AutoSize = True
        Me.lbFechaFinal.BackColor = System.Drawing.Color.Transparent
        Me.lbFechaFinal.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lbFechaFinal.FontSize = 9.75!
        Me.lbFechaFinal.ForeColor = System.Drawing.Color.Black
        Me.lbFechaFinal.Location = New System.Drawing.Point(4, 39)
        Me.lbFechaFinal.Name = "lbFechaFinal"
        Me.lbFechaFinal.Size = New System.Drawing.Size(66, 20)
        Me.lbFechaFinal.TabIndex = 119
        Me.lbFechaFinal.Text = "Hasta:"
        Me.lbFechaFinal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFechaFinal
        '
        Me.dtpFechaFinal.Location = New System.Drawing.Point(64, 34)
        Me.dtpFechaFinal.Name = "dtpFechaFinal"
        Me.dtpFechaFinal.Size = New System.Drawing.Size(247, 27)
        Me.dtpFechaFinal.TabIndex = 121
        '
        'lbFechaInicial
        '
        Me.lbFechaInicial.AutoSize = True
        Me.lbFechaInicial.BackColor = System.Drawing.Color.Transparent
        Me.lbFechaInicial.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lbFechaInicial.FontSize = 9.75!
        Me.lbFechaInicial.ForeColor = System.Drawing.Color.Black
        Me.lbFechaInicial.Location = New System.Drawing.Point(4, 8)
        Me.lbFechaInicial.Name = "lbFechaInicial"
        Me.lbFechaInicial.Size = New System.Drawing.Size(70, 20)
        Me.lbFechaInicial.TabIndex = 118
        Me.lbFechaInicial.Text = "Desde:"
        Me.lbFechaInicial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.rbCaratulasPropiasPorFechas)
        Me.Panel4.Controls.Add(Me.rbCaratulasUsuarioPropias)
        Me.Panel4.Controls.Add(Me.rbCaratulasUsuarioPropiasHoy)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(531, 25)
        Me.Panel4.TabIndex = 123
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label1.FontSize = 9.75!
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(2, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 20)
        Me.Label1.TabIndex = 116
        Me.Label1.Text = "Mostrar"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rbCaratulasPropiasPorFechas
        '
        Me.rbCaratulasPropiasPorFechas.AutoSize = True
        Me.rbCaratulasPropiasPorFechas.Checked = True
        Me.rbCaratulasPropiasPorFechas.ForeColor = System.Drawing.Color.Black
        Me.rbCaratulasPropiasPorFechas.Location = New System.Drawing.Point(303, 3)
        Me.rbCaratulasPropiasPorFechas.Name = "rbCaratulasPropiasPorFechas"
        Me.rbCaratulasPropiasPorFechas.Size = New System.Drawing.Size(191, 24)
        Me.rbCaratulasPropiasPorFechas.TabIndex = 117
        Me.rbCaratulasPropiasPorFechas.TabStop = True
        Me.rbCaratulasPropiasPorFechas.Text = "Propias por Fechas"
        Me.rbCaratulasPropiasPorFechas.UseVisualStyleBackColor = True
        Me.rbCaratulasPropiasPorFechas.Visible = False
        '
        'rbCaratulasUsuarioPropias
        '
        Me.rbCaratulasUsuarioPropias.AutoSize = True
        Me.rbCaratulasUsuarioPropias.ForeColor = System.Drawing.Color.Black
        Me.rbCaratulasUsuarioPropias.Location = New System.Drawing.Point(62, 3)
        Me.rbCaratulasUsuarioPropias.Name = "rbCaratulasUsuarioPropias"
        Me.rbCaratulasUsuarioPropias.Size = New System.Drawing.Size(136, 24)
        Me.rbCaratulasUsuarioPropias.TabIndex = 113
        Me.rbCaratulasUsuarioPropias.Text = "Solo propias"
        Me.rbCaratulasUsuarioPropias.UseVisualStyleBackColor = True
        '
        'rbCaratulasUsuarioPropiasHoy
        '
        Me.rbCaratulasUsuarioPropiasHoy.AutoSize = True
        Me.rbCaratulasUsuarioPropiasHoy.ForeColor = System.Drawing.Color.Black
        Me.rbCaratulasUsuarioPropiasHoy.Location = New System.Drawing.Point(174, 3)
        Me.rbCaratulasUsuarioPropiasHoy.Name = "rbCaratulasUsuarioPropiasHoy"
        Me.rbCaratulasUsuarioPropiasHoy.Size = New System.Drawing.Size(156, 24)
        Me.rbCaratulasUsuarioPropiasHoy.TabIndex = 115
        Me.rbCaratulasUsuarioPropiasHoy.TabStop = True
        Me.rbCaratulasUsuarioPropiasHoy.Text = "Propias de hoy"
        Me.rbCaratulasUsuarioPropiasHoy.UseVisualStyleBackColor = True
        '
        'ToolBar1
        '
        Me.ToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolBar1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolBar1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnPrint, Me.ButtonItem5, Me.BtnReplicar, Me.ButtonItem3, Me.ButtonItem4, Me.ToolStripSeparator1, Me.ToolStripButton1})
        Me.ToolBar1.Location = New System.Drawing.Point(2, 2)
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.Size = New System.Drawing.Size(1118, 39)
        Me.ToolBar1.Stretch = True
        Me.ToolBar1.TabIndex = 99
        '
        'BtnPrint
        '
        Me.BtnPrint.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_printer2
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(109, 36)
        Me.BtnPrint.Tag = "IMPRIMIR"
        Me.BtnPrint.Text = "IMPRIMIR"
        Me.BtnPrint.ToolTipText = "IMPRIMIR CARATULA"
        '
        'ButtonItem5
        '
        Me.ButtonItem5.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_projector_screen
        Me.ButtonItem5.Name = "ButtonItem5"
        Me.ButtonItem5.Size = New System.Drawing.Size(150, 36)
        Me.ButtonItem5.Tag = "PREVISUALIZAR"
        Me.ButtonItem5.Text = "PREVISUALIZAR"
        Me.ButtonItem5.ToolTipText = "MUESTRA  LA PREVISUALIZACION DE LA CARATULA"
        '
        'BtnReplicar
        '
        Me.BtnReplicar.Enabled = False
        Me.BtnReplicar.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_page1
        Me.BtnReplicar.Name = "BtnReplicar"
        Me.BtnReplicar.Size = New System.Drawing.Size(109, 36)
        Me.BtnReplicar.Tag = "REPLICAR"
        Me.BtnReplicar.Text = "REPLICAR"
        Me.BtnReplicar.ToolTipText = "REPLICAR CARATULA"
        '
        'ButtonItem3
        '
        Me.ButtonItem3.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_clean
        Me.ButtonItem3.Name = "ButtonItem3"
        Me.ButtonItem3.Size = New System.Drawing.Size(179, 36)
        Me.ButtonItem3.Tag = "LIMPIAR"
        Me.ButtonItem3.Text = "LIMPIAR ATRIBUTOS"
        Me.ButtonItem3.ToolTipText = "LIMPIAR ATRIBUTOS"
        '
        'ButtonItem4
        '
        Me.ButtonItem4.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_refresh
        Me.ButtonItem4.Name = "ButtonItem4"
        Me.ButtonItem4.Size = New System.Drawing.Size(171, 36)
        Me.ButtonItem4.Tag = "ACTUALIZAR"
        Me.ButtonItem4.Text = "ACTUALIZAR LISTA"
        Me.ButtonItem4.ToolTipText = "ACTUALIZAR LISTA"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.CheckOnClick = True
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(84, 36)
        Me.ToolStripButton1.Text = "HISTORIAL"
        '
        'UCBarCode
        '
        Me.Controls.Add(Me.SplitContainer2)
        Me.Controls.Add(Me.ToolBar1)
        Me.ForeColor = System.Drawing.Color.Black
        Me.Name = "UCBarCode"
        Me.Padding = New System.Windows.Forms.Padding(2)
        Me.Size = New System.Drawing.Size(1122, 801)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.PanelListView.ResumeLayout(False)
        Me.PanelListView.PerformLayout()
        Me.vista.ResumeLayout(False)
        Me.PnlLastCaratulas.ResumeLayout(False)
        Me.panelMostarCaratulas.ResumeLayout(False)
        Me.panelRangoFechas.ResumeLayout(False)
        Me.panelRangoFechas.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ToolBar1.ResumeLayout(False)
        Me.ToolBar1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    ''' <summary>
    ''' Evento que se ejecuta cuando se carga el formulario que muestra las caratulas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    25/06/2008  Modified  Se agrego el método verifyTablaCaratulas()
    ''' </history>
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            RemoveHandler rbCaratulasUsuarioPropiasHoy.CheckedChanged, AddressOf rbCaratulasUsuarioPropiasHoy_CheckedChanged
            RemoveHandler rbCaratulasUsuarioPropias.CheckedChanged, AddressOf rbCaratulasUsuarioPropias_CheckedChanged
            rbCaratulasUsuarioPropiasHoy.Checked = True
            LoadAvaibleDocTypes()
            AddHandler rbCaratulasUsuarioPropiasHoy.CheckedChanged, AddressOf rbCaratulasUsuarioPropiasHoy_CheckedChanged
            AddHandler rbCaratulasUsuarioPropias.CheckedChanged, AddressOf rbCaratulasUsuarioPropias_CheckedChanged
            loadIndex()
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show(ex.ToString, "ZAMBA", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Autocompletar"
    Private Sub autocomplete(ByRef Result As IResult, ByVal _index As IIndex)
        Try
            Dim newFrmGrilla As New Viewers.frmGrilla()
            If Not AutocompleteBCBusiness.ExecuteAutoComplete(Result, _index, newFrmGrilla, True) Is Nothing Then
                LoadIndexViewer(Result)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Region ListView"

#Region "Eventos..."

    Private Sub chkTodasLasCaractulas_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        mostarCaratulasDeTodosLosUsuarios()
    End Sub
    Private Sub rbCaratulasUsuarioPropiasHoy_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbCaratulasUsuarioPropiasHoy.CheckedChanged
        If (rbCaratulasUsuarioPropiasHoy.Checked) Then
            mostarCaratulasPorUsuarioLogueadoYporFechaHoy()
        End If
    End Sub
    Private Sub rbCaratulasUsuarioPropias_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbCaratulasUsuarioPropias.CheckedChanged
        If (rbCaratulasUsuarioPropias.Checked) Then
            mostarCaratulasPorUsuarioLogueado()
        End If

    End Sub
    Private Sub tablaCaratulas_Click(ByVal sender As System.Object, ByVal e As EventArgs)

        Dim Data As String = String.Empty

        Try
            If tablaCaratulas.NewGrid.SelectedRows.Count > 0 Then
                '' Dim docItem As DocItem = docItem.getItem(Me.tmpDocIdTypeList, _
                '' Me.tablaCaratulas.OutlookGrid.SelectedRows(0).Index)
                RePrint = 1
                NewResult = Results_Business.GetNewNewResult(Convert.ToInt64(tablaCaratulas.NewGrid.SelectedRows(0).Cells("doc_id").Value),
                DocTypesBusiness.GetDocType(Convert.ToInt64(tablaCaratulas.NewGrid.SelectedRows(0).Cells("doc_type_id").Value), True))
                NewResult.Indexs = ZCore.FilterIndex(CInt(NewResult.DocType.ID))
                Dim RB As New Results_Business
                RB.CompleteIndexData(NewResult, False)
                RB = Nothing

                CoverIdToPrint = CInt(tablaCaratulas.NewGrid.SelectedRows(0).Cells("caratula").Value)
                LoadIndexViewer(NewResult, True)

                For Each CurrentDocType As DocType In cboDocType.Items
                    If NewResult.DocTypeId = CurrentDocType.ID Then
                        RemoveHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChanged
                        cboDocType.SelectedItem = CurrentDocType
                        AddHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChanged
                        Exit For
                    End If
                Next

                If tablaCaratulas.NewGrid.SelectedRows(0).Cells("escaneado").Value.ToString.ToLower.Equals("no") Then
                    _oldId = tablaCaratulas.NewGrid.SelectedRows(0).Cells("caratula").Value
                    Me.BtnReplicar.Enabled = True
                Else
                    Me.BtnReplicar.Enabled = False
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try


        'Try
        '        RePrint = 1
        '        NewResult = Results_Business.GetNewNewResult(Convert.ToInt64(e), DocTypesBusiness.GetDocType(Convert.ToInt64(e, True)))
        '        NewResult.Indexs = ZCore.FilterIndex(CInt(NewResult.DocType.ID))
        '        For Each CurrentIndex As Index In NewResult.Indexs
        '            Data = Results_Business.GetIndexValueFromDoc_I(NewResult.DocType.ID, NewResult.ID, CurrentIndex.ID)
        '            CurrentIndex.Data = Data
        '            CurrentIndex.DataTemp = Data
        '        Next
        '        CoverIdToPrint = CInt(e)
        '        LoadIndexViewer(NewResult, True)
        'Catch ex As Exception
        '    Zamba.Core.ZClass.raiseerror(ex)
        'End Try
    End Sub
#End Region

#Region "Metodos..."
    ' Imprime las caratulas en la vista actual..
    'Protected Sub imprimirCaratulas()
    '    Try
    '        ' Si hay datos en la vista => hay posibilidad de imprimirlos...
    '        If Not IsNothing(Me.tablaCaratulas.DataSource) Then
    '            Dim dt As DataTable = DirectCast(Me.tablaCaratulas.DataSource, DataTable).Copy()
    '            Zamba.Print.PrintGrilla.Imprimir(dt, dt.TableName)
    '        End If
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub


    ' Muestra datos en la vista de lista de caratulas
    ' y habilita la impresion..
    Private Sub loadViewCaratulas(ByVal dt As DataTable)
        If Not IsNothing(dt) Then
            tablaCaratulas.DataSource = dt
        End If
        setDefaultCursor()
        'Me.btImprimir.Enabled = True
        panelMostarCaratulas.Enabled = True
    End Sub


    ' Deshabilita la impresion de la lista caratulas
    ' mientras estas se cargan en la vista...
    Private Sub setInitLoadCaratulas()
        'Me.btImprimir.Enabled = False
        setWaitCursor()
        panelMostarCaratulas.Enabled = False
    End Sub


    ' Muestra el cursor Wait para 
    ' la vista de Lista de caratulas
    Private Sub setWaitCursor()
        tablaCaratulas.Cursor = Cursors.WaitCursor
        panelMostarCaratulas.Cursor = Cursors.WaitCursor
    End Sub


    ' Muestra el cursor por defecto para 
    ' la vista de Lista de caratulas
    Private Sub setDefaultCursor()
        tablaCaratulas.Cursor = Cursors.Default
        panelMostarCaratulas.Cursor = Cursors.Default
    End Sub


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Llena una lista con las cartulas creadas 
    ''' </summary>
    ''' <remarks>
    '''   Llamada asincronica al metodo load Caratulas..
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	  13/07/2006	Created
    ''' 	[Adrian]  14/11/2006	Modified
    ''' 	[Marcelo] 17/05/2006	Modified
    '''     [Gaston]  25/06/2008	Modified   Se agrego el método verifyTablaCaratulas() y se cambio la condición
    '''                                        que valida las filas del dataset
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Protected Sub LoadCaractulasAsincronico(ByVal ds As DataTable)

        Try
            ' Para la carga de la vista lista de caratulas
            ' que este en curso..
            'If Not IsNothing(T) Then
            '    If T.ThreadState <> ThreadState.Stopped Then
            '        T.Interrupt()
            '    End If
            'End If

            ' Limpia la vista de lista de caratulas
            ' de datos anteriores...
            tablaCaratulas.ResetGrid()
            'tablaCaratulas.DataSource = Nothing

            ' lanza la carga de la vista lista de caratulas
            ' solo si hay caratulas disponibles..
            If (ds.Rows.Count > 0) Then
                tablaCaratulas.DataSource = ds
            End If

            verifyTablaCaratulas()

            ' Lanza la crga de la vista lista de caratulas...
            'T = New Thread(New ParameterizedThreadStart(AddressOf Me.LoadCaractulas))
            'T.Start(ds)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub


#End Region

#End Region

#Region "DocTypes / Indexs"
    Private _ucViewer As UCIndexIndexerViewer
    Private _NewResult As NewResult

    Private Property NewResult() As NewResult
        Get
            Return _NewResult
        End Get
        Set(ByVal Value As NewResult)
            _NewResult = Value
        End Set
    End Property

    Private Sub LoadAvaibleDocTypes()
        Try
            Dim AvaibleUserRights As List(Of IDocType) = DocTypesBusiness.GetDocTypesbyUserRightsOfView(UserBusiness.Rights.CurrentUser.ID, RightsType.Create)

            If AvaibleUserRights.Count > 0 Then
                RemoveHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChanged
                cboDocType.DisplayMember = "Name"
                cboDocType.ValueMember = "ID"
                For Each E As IDocType In AvaibleUserRights
                    cboDocType.Items.Add(E)
                Next
                AddHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChanged

                If Not IsNothing(_loadedResult) Then
                    For Each CurrentDocType As DocType In cboDocType.Items
                        If _loadedResult.DocType.ID = CurrentDocType.ID Then
                            cboDocType.SelectedItem = CurrentDocType
                            Exit For
                        End If
                    Next
                End If
                If cboDocType.SelectedIndex = -1 Then
                    cboDocType.SelectedIndex = 0
                End If

            Else
                MessageBox.Show("Error 013: No hay definidos Entidades para realizar la indexacion o no tiene Permisos para crear Documentos", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: 014: Ocurrio un Error al cargar los Entidades para la Indexación ", "Zamba Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub SelectDocType()
        cboDocType.SelectedIndex = 0
        SelectedIndexChanged(cboDocType, New EventArgs)
    End Sub

    Private Sub loadIndex()
        Try
            NewResult = New NewResult
            If cboDocType.SelectedIndex > -1 Then
                RePrint = 0
                NewResult.Parent = cboDocType.SelectedItem

                NewResult.Indexs = IndexsBusiness.GetIndexsSchemaAsListOfDT(NewResult.DocTypeId, True)

                If _ucViewer IsNot Nothing Then
                    _ucViewer.cleanIndexs()
                End If

                LoadIndexViewer(NewResult)
                _loadedResult = NewResult
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        loadIndex()
    End Sub

    Private Sub LoadIndexViewer(ByVal newResult As NewResult, Optional ByVal replaceData As Boolean = False)
        If IsNothing(_ucViewer) Then
            _ucViewer = New UCIndexIndexerViewer
            _ucViewer.Dock = DockStyle.Fill
            PanelIndexs.Controls.Add(_ucViewer)
            _ucViewer.BringToFront()
        End If
        'TODO Poner un IF para verificar si esta checkeado la opcion autocomplete.

        RemoveHandler _ucViewer.IndexChanged, AddressOf autocomplete
        AddHandler _ucViewer.IndexChanged, AddressOf autocomplete

        _ucViewer.ShowDocument(DirectCast(newResult, NewResult), replaceData)
    End Sub

    Private Sub LoadIndexViewer(ByRef Result As IResult)
        If IsNothing(_ucViewer) Then
            _ucViewer = New UCIndexIndexerViewer
            _ucViewer.Dock = DockStyle.Fill
            PanelIndexs.Controls.Add(_ucViewer)
            _ucViewer.BringToFront()
        End If

        RemoveHandler _ucViewer.IndexChanged, AddressOf autocomplete
        AddHandler _ucViewer.IndexChanged, AddressOf autocomplete

        _ucViewer.ShowDocument(Result, True)
    End Sub
#End Region

#Region "Eventos Botones"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Imprime y muestar una previsualizacion de una caratula
    ''' </summary>
    ''' <param name="bImprimir">Por defecto, es verdadero y esto indica 
    '''             que la caratula se va a imprimir.
    '''             De lo contrario si seria Falso, muestra una previsualizacion
    ''' </param>
    ''' <remarks>
    '''     Utiliza un formulario personalizado para realizar la previsualizacion.
    '''     El objeto printDocument se le debe pasar en el constructor.
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	    22/06/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub Evento_btnGenerar(Optional ByVal bImprimir As Boolean = True)
        'Se agrega el doevents para que los eventos de indexchange se procesen correctamente antes de generar las impresiones
        Focus()
        Application.DoEvents()

        Dim frm As frmPrintPreView
        Dim dlg As PrintDialog
        frm = Nothing
        dlg = Nothing
        If NewResult IsNot Nothing Then

            Try
                NewResult.DocType.Indexs = NewResult.Indexs
                '(pablo) 06-04-2011
                'se valida que los atributos de sustitucion completados tengan valores coherentes
                If Results_Business.ValidateSlstIlstDescription(NewResult) = False Then
                    Dim response As Microsoft.VisualBasic.MsgBoxResult
                    response = MessageBox.Show("Alguno de los atributos ingresados no coindice con los datos de su lista. ¿Desea Continuar?", "Zamba", MessageBoxButtons.YesNo)
                    If response = MsgBoxResult.No Then
                        Exit Sub
                    End If
                End If
                If Results_Business.ValidateIndexDataFromDoctype(NewResult) <> False Then
                    If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, CInt(NewResult.DocType.ID)) AndAlso Results_Business.ValidateIndexData(NewResult) = False Then
                        MessageBox.Show("Hay atributos requeridos incompletos", "Zamba", MessageBoxButtons.OK)
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Hay atributos requeridos incompletos", "Zamba", MessageBoxButtons.OK)
                    Exit Sub
                End If

                If bImprimir And RePrint = 1 Then


                    dlg = New PrintDialog()
                    dlg.Document = pdocumdoctypes
                    dlg.UseEXDialog = True
                    If dlg.ShowDialog() = DialogResult.OK Then
                        _barcodeId = CoverIdToPrint

                        _oldId = CoverIdToPrint
                        BtnReplicar.Enabled = True
                        GetIndexsFromTemp()
                        pdocumdoctypes.Print()
                        UserBusiness.Rights.SaveAction(NewResult.ID, ObjectTypes.ModuleBarCode, RightsType.Create, "Usuario RE Imprimio Caratula")
                        _barcodeId = 0
                    Else
                        Exit Sub
                    End If
                ElseIf bImprimir = True Then
                    dlg = New PrintDialog()
                    dlg.Document = pdocumdoctypes
                    dlg.UseEXDialog = True

                    If dlg.ShowDialog() = DialogResult.OK Then
                        _barcodeId = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.Caratulas)
                        _oldId = _barcodeId
                        NewResult.ID = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.DOCID)

                        GetIndexsFromTemp()
                        If BarcodesBusiness.Insert(NewResult, CInt(NewResult.Parent.ID), CInt(Membership.MembershipHelper.CurrentUser.ID), _barcodeId, True) Then
                            pdocumdoctypes.Print()
                            UserBusiness.Rights.SaveAction(NewResult.ID, ObjectTypes.ModuleBarCode, RightsType.Create, "Usuario Imprimio Caratula")
                        Else
                            MessageBox.Show("No se pudo insertar el código de barras", "Error en Inserción", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If


                    End If
                Else
                    If 0 = _barcodeId Then
                        _barcodeId = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.Caratulas)
                    End If
                    _oldId = _barcodeId
                    If Not IsNothing(NewResult) Then
                        NewResult.ID = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.DOCID)

                        GetIndexsFromTemp()
                        frm = New frmPrintPreView(pdocumdoctypes)
                        frm.WindowState = FormWindowState.Maximized
                        frm.StartPosition = FormStartPosition.CenterScreen
                        frm.ShowDialog(Me)
                    Else
                        MsgBox("Verifique los datos ingresados y seleccionados y vuelva a intentarlo")
                    End If
                End If

                'Refrescar la grilla
                RefreshGrid()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            Finally
                If Not IsNothing(frm) Then
                    frm.Close()
                    frm.Dispose()
                    frm = Nothing
                End If
                If Not IsNothing(dlg) Then
                    dlg.Dispose()
                    dlg = Nothing
                End If
            End Try
        End If

    End Sub

    Private Sub RefreshGrid()
        If Not SplitContainer2.Panel2Collapsed Then
            If rbCaratulasUsuarioPropiasHoy.Checked Then
                mostarCaratulasPorUsuarioLogueadoYporFechaHoy()
            ElseIf rbCaratulasUsuarioPropias.Checked Then
                mostarCaratulasPorUsuarioLogueado()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se hace click en el botón Replicar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    25/06/2008  Modify  Se agrego código de validación que verifica que el NewResult no sea 
    '''                                     Nothing. Si es, entonces aparece un mensaje de error
    ''' </history>
    Private Sub Evento_btnReplicar_Click_1()

        If (_oldId = 0) Then
            MessageBox.Show("Primero debe imprimir la caratula, para poder replicar", "Replicar", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        'Si hay alguna caratula seleccionada en la grilla historial
        If (NewResult IsNot Nothing) Then
            Try
                'Mismo id que la anterior caratula
                _barcodeId = _oldId
                NewResult.ID = ToolsBusiness.GetNewID(IdTypes.DOCID)
                GetIndexsFromTemp()
                If BarcodesBusiness.Insert(NewResult, NewResult.Parent.ID, Membership.MembershipHelper.CurrentUser.ID, _barcodeId, True) Then
                    If chkReplicas.Checked Then
                        pdocumdoctypes.Print()
                    End If
                    UserBusiness.Rights.SaveAction(NewResult.ID, ObjectTypes.ModuleBarCode, RightsType.Replicate, "Usuario Replico Caratula")
                Else
                    MessageBox.Show("No se pudo insertar el código de barras", "Error en Inserción", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                'Refrescar la grilla
                RefreshGrid()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        Else
            MessageBox.Show("No tiene permiso para replicar esta caratula", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub GetIndexsFromTemp()
        Try
            If _ucViewer.IsValid Then
                For Each _index As Index In NewResult.Indexs
                    _index.Data = _index.DataTemp
                    _index.dataDescription = _index.dataDescriptionTemp
                Next
            Else

            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally

        End Try
    End Sub
    Private Sub Evento_btnLimpiar_Click()
        Try
            _ucViewer.cleanIndexs()
            _ucViewer.ShowDocument(NewResult, False)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub



#Region "Mostrar Caratulas"
    ''' <summary>
    ''' Carga las caratulas en el Formulario filtrando por Usuario Logeado y Fecha Actual
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub mostarCaratulasPorUsuarioLogueadoYporFechaHoy()
        Try
            LoadCaractulasAsincronico(BarcodesBusiness.dsFilterCaratulas(CInt(Membership.MembershipHelper.CurrentUser.ID), Date.Today))
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        'LoadCaractulasAsincronico(dsFilterCaratulasPorUsuarioYFechaActual(Membership.MembershipHelper.CurrentUser.ID, Date.Today))
    End Sub
    ''' <summary>
    ''' Carga las caratulas en el Formulario filtrando rango de Fechas
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub mostrarTodasLasCaratulasPorFecha(ByVal fechaInicial As DateTime, ByVal fechaFinal As DateTime)
        LoadCaractulasAsincronico(BarcodesBusiness.dsFilterCaratulas(fechaInicial, fechaFinal))
    End Sub
    ''' <summary>
    ''' Carga las caratulas en el Formulario filtrando por Usuario Logeado y rango de Fechas
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub mostarCaratulasPorUsuarioLogueadoYporFechas(ByVal m_fechaInicial As DateTime, ByVal m_fechaFinal As DateTime)
        LoadCaractulasAsincronico(BarcodesBusiness.dsFilterCaratulas(CInt(Membership.MembershipHelper.CurrentUser.ID), m_fechaInicial, m_fechaFinal))
    End Sub
    ''' <summary>
    ''' Carga las caratulas en el Formulario filtrando por Usuario Logeado 
    ''' </summary>
    ''' 
    ''' <remarks></remarks>
    Protected Sub mostarCaratulasPorUsuarioLogueado()
        LoadCaractulasAsincronico(BarcodesBusiness.dsFilterCaratulas((CInt(Membership.MembershipHelper.CurrentUser.ID))))
    End Sub
    ''' <summary>
    ''' Carga las caratulas en el Formulario sin filtrar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub mostarCaratulasDeTodosLosUsuarios()
        LoadCaractulasAsincronico(BarcodesBusiness.dsAllCaratulas())
    End Sub
#End Region
    Private Sub mnuEliminar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuEliminar.Click
        Evento_mnuEliminar_Click()
    End Sub

    Private Sub btVer_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btVer.Click
        Try
            mostarCaratulasPorUsuarioLogueadoYporFechas(dtpFechaInicial.Value, dtpFechaFinal.Value)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    '''Devuelve el id de caratula de la fila seleccionada 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function getCarIdSelectecRow() As System.Int64
        If Not tablaCaratulas.NewGrid.RowCount < 1 Then
            Return System.Convert.ToInt64(tablaCaratulas.NewGrid.SelectedRows(0).Cells(1))
        End If

        Return -1
    End Function

    Private Sub Evento_mnuEliminar_Click()
        Try
            Dim carId As System.Int64 = getCarIdSelectecRow()
            If carId >= 0 Then
                If MessageBox.Show("Esta seguro que desea eliminar la caratula " _
                 & carId.ToString & " ?    ",
                 "Generador de Codigo de Barras",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question) = DialogResult.Yes Then
                    Try
                        BarcodesBusiness.Delete(CInt(carId))
                    Catch ex As Exception
                        MessageBox.Show("No se pudo borrar la caratula seleccionada",
                        "Generador de Codigo de Barras",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

    Private Sub FrmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            Evento_btnGenerar()
        End If
    End Sub



#Region "Print"
    Private Sub pdocumdoctypes_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdocumdoctypes.PrintPage
        Evento_pdocumdoctypes_PrintPage(e)
    End Sub

    Private Sub Evento_pdocumdoctypes_PrintPage(ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        If False Then

        Else
            Dim y As Integer = 240
            Dim y2 As Integer = 270
            Dim y3 As Integer = 252
            Dim ylst As Integer = 282
            Dim ynam As Integer = 282

            'Dim doctypeALL As String
            Dim doctypeID As Int64
            Dim indexALL As String = String.Empty
            Dim dataBC As String = String.Empty
            Dim dataALL As String = String.Empty
            Dim IndexCount As Integer = 0
            Dim PrintBarcodeIndexOption As Boolean = chkPrintIndex.Checked

            Dim header As String = Now.ToString & " - " &
                                    UserBusiness.Rights.CurrentUser.Nombres &
                                    " " & UserBusiness.Rights.CurrentUser.Apellidos &
                                    " - " & UserBusiness.Rights.CurrentUser.Name() &
                                    " (" & Membership.MembershipHelper.CurrentUser.ID.ToString & ")"
            'Fecha y Hora
            e.Graphics.DrawString(header,
                                  New Font("Times New Roman", 9, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte)),
                                  Brushes.Black,
                                  400,
                                  75)

            ' e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, 100, 735, 70))

            'Caratula
            e.Graphics.DrawString("Caratula Nro:", New Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold), Brushes.Black, 40, 125)
            e.Graphics.DrawString(_barcodeId.ToString, New Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold), Brushes.Black, 40, 138)
            If UserPreferences.getValue("UseCodaBar", UPSections.Barcode, False) = False Then
                _barcodeImage = Barcode_Motor.Print(e, _barcodeId.ToString, 150, 112)
            Else
                Dim spire As New FileTools.SpireTools()
                e.Graphics.DrawImage(spire.GenerateBarcodeImage(_barcodeId, 1), 150, 112)
            End If

            'e.Graphics.DrawLine(New Pen(Color.Black), 390, 100, 390, 170)
            'e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, 170, 735, 70))

            'User
            'e.Graphics.DrawString(UserBusiness.Rights.CurrentUser.Name(), New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 400, 125)
            'e.Graphics.DrawString("(" & Membership.MembershipHelper.CurrentUser.ID.ToString & ")", New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 400, 138)
            '  _barcodeImage = Barcode_Motor.Print(e, Membership.MembershipHelper.CurrentUser.ID.ToString, 520, 110)

            'docType
            '        doctypeID = Me.DsDocTypes.DOC_TYPE(CboDocType.SelectedIndex).DOC_TYPE_ID
            e.Graphics.DrawString(NewResult.DocType.Name.Trim & "    (" & NewResult.DocType.ID.ToString & ")", New Font(FontFamily.GenericSansSerif, 11, FontStyle.Regular), Brushes.Black, 40, 199)
            ' _barcodeImage = Barcode_Motor.Print(e, doctypeID.ToString, 520, 182)

            IndexCount = 0
            For Each printIndex As Index In NewResult.Indexs
                'Imprimo comentario y salgo
                If IndexCount = 11 Then
                    e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, y, 735, 70))
                    e.Graphics.DrawString("Nota: ", New Font(FontFamily.GenericSerif, 9, FontStyle.Bold), Brushes.Black, 40, y2)
                    If txtRemark.Text.Length > 100 Then
                        'e.Graphics.DrawString(Me.txtRemark.Text.Substring(0, 100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
                        'e.Graphics.DrawString(Me.txtRemark.Text.Substring(100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2 + 13)
                        e.Graphics.DrawString(txtRemark.Text.Substring(0, 100) & vbCrLf & txtRemark.Text.Substring(100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
                    Else
                        e.Graphics.DrawString(txtRemark.Text, New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
                    End If
                    Exit Sub
                End If

                'Si el atributo esta vacio no imprimo rectangulo
                If Not String.IsNullOrEmpty(printIndex.Data) OrElse PrintBarcodeIndexOption = True Then
                    IndexCount += 1
                    Try
                        e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, y, 735, 70))
                        e.Graphics.DrawLine(New Pen(Color.Black), 210, y, 210, y + 70)

                        'index.name
                        indexALL = printIndex.Name & "    (" & printIndex.ID & ")"
                        If indexALL.Length <= 25 Then
                            e.Graphics.DrawString(indexALL, New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 40, y2)
                        Else
                            'e.Graphics.DrawString(indexALL.Substring(0, 25), New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 40, y2)
                            'e.Graphics.DrawString(indexALL.Substring(25), New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 40, ynam)
                            e.Graphics.DrawString(indexALL.Substring(0, 25) & vbCrLf & indexALL.Substring(25), New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 40, y2)
                        End If

                        'barCode
                        'valido si es numerico para no imprimir codigo de barra
                        Dim flagChar As Boolean = False
                        'For Each c As Char In printIndex.Data
                        '    If c <> "0" AndAlso c <> "1" AndAlso c <> "2" AndAlso c <> "3" AndAlso c <> "4" AndAlso c <> "5" AndAlso c <> "6" AndAlso c <> "7" AndAlso c <> "8" AndAlso c <> "9" Then
                        '        flagChar = True
                        '        Exit For
                        '    End If
                        'Next
                        If Not IsNumeric(printIndex.Data) Then
                            flagChar = True
                        End If
                        If IsDate(printIndex.Data) Then
                            flagChar = False
                        End If
                        If Not String.IsNullOrEmpty(printIndex.Data) Then
                            Try
                                If IsNumeric(printIndex.Data) AndAlso printIndex.Data.Length <= 10 Then
                                    dataBC = convertCodeBar(CInt(printIndex.Data))
                                Else
                                    dataBC = printIndex.Data
                                End If
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                            End Try
                            If Not flagChar = True Then
                                Try
                                    If PrintBarcodeIndexOption = True Then
                                        _barcodeImage = Barcode_Motor.Print(e, dataBC, 470, y3)
                                    End If
                                Catch ex As Exception
                                    Zamba.Core.ZClass.raiseerror(ex)
                                End Try
                            End If

                            'index.data
                            If printIndex.DropDown <> IndexAdditionalType.AutoSustitución _
                                AndAlso printIndex.DropDown <> IndexAdditionalType.AutoSustituciónJerarquico Then

                                '1
                                If flagChar = True Then
                                    If MyClass.chkPrintIndex.Checked = True Then
                                        If printIndex.Data.Length > 35 Then
                                            e.Graphics.DrawString(printIndex.Data.Substring(0, 35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                            If printIndex.Data.Length > 70 Then
                                                e.Graphics.DrawString(printIndex.Data.Substring(35, 35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                            Else
                                                e.Graphics.DrawString(printIndex.Data.Substring(35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                            End If
                                        Else
                                            e.Graphics.DrawString(printIndex.Data, New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                        End If
                                    Else
                                        If printIndex.Data.Length > 80 Then
                                            e.Graphics.DrawString(printIndex.Data.Substring(0, 80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                            If printIndex.Data.Length > 160 Then
                                                e.Graphics.DrawString(printIndex.Data.Substring(80, 80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                            Else
                                                e.Graphics.DrawString(printIndex.Data.Substring(80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                            End If
                                        Else
                                            e.Graphics.DrawString(printIndex.Data, New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                        End If
                                    End If

                                Else

                                    '2
                                    dataALL = printIndex.Data & "    (" & dataBC & ")"
                                    If MyClass.chkPrintIndex.Checked = True Then

                                        If dataALL.Length > 35 Then
                                            e.Graphics.DrawString(dataALL.Substring(0, 35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                            If dataALL.Length > 70 Then
                                                e.Graphics.DrawString(dataALL.Substring(35, 35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                            Else
                                                e.Graphics.DrawString(dataALL.Substring(35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                            End If
                                        Else
                                            e.Graphics.DrawString(dataALL, New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                        End If
                                    Else
                                        If dataALL.Length > 80 Then
                                            e.Graphics.DrawString(dataALL.Substring(0, 80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                            If dataALL.Length > 160 Then
                                                e.Graphics.DrawString(dataALL.Substring(80, 80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                            Else
                                                e.Graphics.DrawString(dataALL.Substring(80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2 + 12)
                                            End If
                                        Else
                                            e.Graphics.DrawString(dataALL, New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                        End If
                                    End If
                                End If
                            Else


                                '3
                                'lista de sustitucion
                                dataALL = printIndex.Data & " - " & printIndex.dataDescription & "  (" & dataBC & ")"
                                If MyClass.chkPrintIndex.Checked = True Then
                                    If dataALL.Length > 35 Then
                                        e.Graphics.DrawString(dataALL.Substring(0, 35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                        If dataALL.Length > 70 Then
                                            e.Graphics.DrawString(dataALL.Substring(35, 35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, ylst)
                                        Else
                                            e.Graphics.DrawString(dataALL.Substring(35), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, ylst)
                                        End If
                                    Else
                                        e.Graphics.DrawString(dataALL, New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                    End If
                                Else
                                    If dataALL.Length > 80 Then
                                        e.Graphics.DrawString(dataALL.Substring(0, 80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                        If dataALL.Length > 160 Then
                                            e.Graphics.DrawString(dataALL.Substring(80, 80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, ylst)
                                        Else
                                            e.Graphics.DrawString(dataALL.Substring(80), New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, ylst)
                                        End If
                                    Else
                                        e.Graphics.DrawString(dataALL, New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular), Brushes.Black, 220, y2)
                                    End If
                                End If
                            End If
                        End If
                    Catch
                    End Try

                    y += 70
                    y2 += 70
                    y3 += 70
                    ylst += 70
                End If
                'Try

                'Catch ex As Exception
                '   zamba.core.zclass.raiseerror(ex)
                'End Try

            Next

            'Comentario en caso de menos de 10 atributos
            e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, y, 735, 70))
            e.Graphics.DrawString("Nota: ", New Font(FontFamily.GenericSerif, 9, FontStyle.Bold), Brushes.Black, 40, y2)
            If txtRemark.Text.Length > 100 Then
                e.Graphics.DrawString(txtRemark.Text.Substring(0, 100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
                e.Graphics.DrawString(txtRemark.Text.Substring(100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2 + 13)
            Else
                e.Graphics.DrawString(txtRemark.Text, New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
            End If

            If Not IsNothing(indexALL) Then indexALL = Nothing
            If Not IsNothing(dataBC) Then dataBC = Nothing
            If Not IsNothing(dataALL) Then dataALL = Nothing
        End If

    End Sub



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Agrega ceros al value del barcode 
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Shared Function convertCodeBar(ByVal data As Integer) As String
        Dim s As New System.Text.StringBuilder
        Try
            s.Append(data)
            If s.Length <= 9 Then
                s.Insert(0, "0", 9 - s.Length)
            End If
            Return s.ToString
        Finally
            s = Nothing
        End Try
    End Function
#End Region


#Region "Remark"
    'Dim remarks As ArrayList
    'Private Sub loadRemarks()
    '    'remarks = BarcodeRemark_Motor.LoadRemarks(UserId)
    '    'Try
    '    '    cboRemark.BeginUpdate()
    '    '    cboRemark.DisplayMember = "Remark"
    '    '    cboRemark.ValueMember = "Order"
    '    '    cboRemark.DataSource = remarks
    '    '    ChkPreProcess.EndUpdate()
    '    'Catch ex As Exception
    '    'End Try
    'End Sub
    'Private Sub saveRemark()
    '    'BarcodeRemark_Motor.SaveRemark(UserId, cboRemark.Text)
    '    'BarcodeRemark_Motor.LoadRemarks(UserId)
    'End Sub
#End Region

#Region "Informe Caratulas"


    ''' <summary>
    ''' Muestra una tabla en un formulario modal
    ''' </summary>
    ''' <param name="table">tabla</param>
    Private Sub showReport(ByRef table As DataSet, ByVal frmTitle As String)
        If Not IsNothing(table) AndAlso table.Tables.Count > 0 Then
            Dim informe As New ZForm
            Dim vista As New DataGridView
            vista.ReadOnly = True
            vista.RowHeadersVisible = False
            vista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            informe.Size = New Size(New Point(700, 600))
            vista.Dock = DockStyle.Fill
            informe.Controls.Add(vista)
            informe.StartPosition = FormStartPosition.CenterParent
            informe.Text = String.Concat(informe.Text, " - ", frmTitle)
            vista.DataSource = table.Tables(0)
            informe.WindowState = FormWindowState.Maximized
            informe.ShowDialog(Me)
        End If
    End Sub

    '    Private Sub btInforme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        showReport(BarcodesBusiness.getInforme(), btInforme.Text)
    '    End Sub

#End Region

    Private Sub dtpFechaInicial_ValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles dtpFechaInicial.ValueChanged
        If dtpFechaInicial.Value > dtpFechaFinal.Value Then
            dtpFechaInicial.Value = dtpFechaFinal.Value
        End If
    End Sub

    Private Sub dtpFechaFinal_ValueChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles dtpFechaFinal.ValueChanged
        If dtpFechaInicial.Value > dtpFechaFinal.Value Then
            dtpFechaFinal.Value = dtpFechaInicial.Value
        End If
    End Sub

    ''' <summary>
    ''' Método que verifica si la grilla que contiene las caratulas tiene elementos y si hay algún elemento
    ''' seleccionado. Si es así, entonces se activa el botón Replicar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    25/06/2008  Created 
    ''' </history>
    Private Sub verifyTablaCaratulas()

        If (tablaCaratulas IsNot Nothing) Then
            ' Si la grilla que contiene las caratulas es mayor a cero y hay al menos una fila seleccionada en la
            ' grilla activar botón Replicar
            If ((tablaCaratulas.NewGrid.Rows.Count > 0) AndAlso (tablaCaratulas.NewGrid.SelectedRows IsNot Nothing)) Then
                BtnReplicar.Enabled = True
                btnRePrint.Enabled = True
            Else
                BtnReplicar.Enabled = False
                btnRePrint.Enabled = False
            End If

        End If

    End Sub

    Private _fc As IFiltersComponent

    Public Property Fc() As IFiltersComponent Implements IGrid.Fc
        Get
            Return _fc
        End Get
        Set(ByVal value As IFiltersComponent)
            _fc = value
        End Set
    End Property
    Private _lastPage As Integer

    Public Property LastPage() As Integer Implements IGrid.LastPage
        Get
            Return _lastPage
        End Get
        Set(ByVal value As Integer)
            _lastPage = value
        End Set
    End Property

    Public Property PageSize As Integer Implements IGrid.PageSize


    Public Property Exporting As Boolean Implements IGrid.Exporting


    Public Property ExportSize As Integer Implements IGrid.ExportSize

    Public Property SaveSearch As Boolean Implements IGrid.SaveSearch
        Get
        End Get
        Set(value As Boolean)
        End Set
    End Property

    Public Property SortChanged As Boolean Implements IOrder.SortChanged

    Public Property FiltersChanged As Boolean Implements IFilter.FiltersChanged
        Get

        End Get
        Set(value As Boolean)

        End Set
    End Property

    Public Property FontSizeChanged As Boolean Implements IGrid.FontSizeChanged

    Public Sub ShowTaskOfDT() Implements IGrid.ShowTaskOfDT

    End Sub

    Public Sub AddOrderComponent(orderString As String) Implements IOrder.AddOrderComponent
    End Sub
    Public Sub AddGroupByComponent(v As String) Implements IGrid.AddGroupByComponent

    End Sub
    Private Sub ToolBar1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolBar1.ItemClicked
        Try
            Select Case CStr(e.ClickedItem.Tag)
                Case "IMPRIMIR"
                    If Not IsNothing(NewResult) And RePrint = 0 Then
                        Evento_btnGenerar()
                        'Else
                        '    Evento_btnGenerar(True)
                        '    RePrint = 0
                    End If
                Case "REPLICAR"
                    Evento_btnReplicar_Click_1()
                Case "ACTUALIZAR"
                    RefreshGrid()
                Case "LIMPIAR"
                    Evento_btnLimpiar_Click()
                Case "PREVISUALIZAR"
                    Evento_btnGenerar(False)
            End Select
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            If SplitContainer1.Panel2Collapsed = False Then
                SplitContainer1.Panel2Collapsed = True
                ToolStripButton1.ForeColor = Color.WhiteSmoke
            Else
                SplitContainer1.Panel2Collapsed = False
                SplitContainer1.SplitterDistance = SplitContainer1.Width / 2
                ToolStripButton1.ForeColor = Color.White
                Me.SuspendLayout()
                RefreshGrid()
                verifyTablaCaratulas()
                Me.ResumeLayout()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnRePrint_Click(sender As Object, e As EventArgs) Handles btnRePrint.Click
        Try
            If tablaCaratulas.NewGrid.Rows.Count > 0 Then
                If tablaCaratulas.NewGrid.SelectedRows.Count = 1 Then
                    If Not IsNothing(NewResult) AndAlso RePrint = 1 Then
                        Evento_btnGenerar(True)
                        RePrint = 0
                    End If
                Else
                    MessageBox.Show("Debe seleccionar una caratula para reimprimir")
                End If
            Else
                MessageBox.Show("La grilla no tiene caratulas para reimprimir")
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            RePrint = 0
        End Try
    End Sub

    Private Sub cboDocType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDocType.SelectedIndexChanged

    End Sub
End Class