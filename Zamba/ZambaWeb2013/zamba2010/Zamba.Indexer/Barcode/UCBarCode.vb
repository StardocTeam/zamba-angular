Imports Zamba.Filters.Interfaces
Imports Zamba.Filters
Imports Zamba.Grid
Imports Zamba.Core
Imports Zamba.Indexs
Imports Zamba.AppBlock
Imports System.Drawing.Printing
Imports System.Drawing
Imports System.Threading
Imports System.Collections.Generic
Imports System.Windows.Forms.ListView
'Comente esta linea porque tiraba warning de ningun metodo disponible - Marcelo
'Imports Zamba.Barcode.Factory

Public Class UCBarCode
    Inherits Zamba.AppBlock.ZControl
    Implements IFilter
#Region "Atributos"
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents PanelIndexs As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkPrintIndex As System.Windows.Forms.CheckBox
    Friend WithEvents PanelListView As System.Windows.Forms.Panel
    Friend WithEvents panelMostarCaratulas As System.Windows.Forms.Panel
    Friend WithEvents panelRangoFechas As System.Windows.Forms.Panel
    Friend WithEvents btVer As Zamba.AppBlock.ZButton
    Friend WithEvents dtpFechaInicial As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbFechaFinal As System.Windows.Forms.Label
    Friend WithEvents dtpFechaFinal As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbFechaInicial As System.Windows.Forms.Label
    Friend WithEvents rbCaratulasPropiasPorFechas As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rbCaratulasUsuarioPropiasHoy As System.Windows.Forms.RadioButton
    Friend WithEvents rbCaratulasUsuarioPropias As System.Windows.Forms.RadioButton
    Friend WithEvents vista As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents tablaCaratulas As Zamba.Grid.PageGroupGrid.PageGroupGrid
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
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
    Private _barcodeId As Integer
    Private _oldId As Int32
    Private WithEvents pdocum As New Printing.PrintDocument
    Private WithEvents pdocumdoctypes As New Printing.PrintDocument
    'Dim UserId As Integer
    'Dim DocTypeId As Integer
#End Region

#Region "Delegados"
    Private Delegate Sub dDelegateWithOutParams()
    Private Delegate Sub dloadViewCaratulas(ByVal dt As DataTable)
#End Region

    Dim CurrentUserId As Int64
    Public Sub New(ByVal CurrentUserId As Int64)
        MyBase.New()
        InitializeComponent()
        Me.CurrentUserId = CurrentUserId
        Me._fc = New FiltersComponent
        _tmpDocIdTypeList = New List(Of DocItem)
        AddHandler tablaCaratulas.OnGridCDoubleClick, AddressOf tablaCaratulas_Click
    End Sub

    Public Sub New(ByRef Result As Result, ByVal CurrentUserId As Int64)
        MyBase.New()
        InitializeComponent()
        Me.CurrentUserId = CurrentUserId
        Me._fc = New FiltersComponent
        _loadedResult = Result
        _tmpDocIdTypeList = New List(Of DocItem)
        AddHandler tablaCaratulas.OnGridCDoubleClick, AddressOf tablaCaratulas_Click
    End Sub

#Region " Código generado por el Diseñador de Windows Forms "

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Public Shadows Event Finish()
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
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
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents cboDocType As System.Windows.Forms.ListBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Splitter1 As ZSplitter
    Friend WithEvents chkReplicas As System.Windows.Forms.CheckBox
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuEliminar As System.Windows.Forms.MenuItem
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ChkPreProcess As System.Windows.Forms.CheckedListBox
    Friend WithEvents PanelTop As ZColorPanel
    Friend WithEvents PanelMain As Panel
    Friend WithEvents PanelMIddle As System.Windows.Forms.Panel
    Friend WithEvents ToolBar1 As TD.SandBar.ToolBar
    Friend WithEvents ButtonItem1 As TD.SandBar.ButtonItem
    Friend WithEvents ButtonItem2 As TD.SandBar.ButtonItem
    Friend WithEvents ButtonItem3 As TD.SandBar.ButtonItem
    Friend WithEvents ButtonItem4 As TD.SandBar.ButtonItem
    Friend WithEvents ButtonItem5 As TD.SandBar.ButtonItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCBarCode))
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.PanelMain = New System.Windows.Forms.Panel
        Me.PanelMIddle = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.PanelIndexs = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.chkPrintIndex = New System.Windows.Forms.CheckBox
        Me.PanelListView = New System.Windows.Forms.Panel
        Me.panelMostarCaratulas = New System.Windows.Forms.Panel
        Me.panelRangoFechas = New System.Windows.Forms.Panel
        Me.btVer = New Zamba.AppBlock.ZButton
        Me.rbCaratulasPropiasPorFechas = New System.Windows.Forms.RadioButton
        Me.dtpFechaInicial = New System.Windows.Forms.DateTimePicker
        Me.lbFechaFinal = New System.Windows.Forms.Label
        Me.dtpFechaFinal = New System.Windows.Forms.DateTimePicker
        Me.lbFechaInicial = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.rbCaratulasUsuarioPropiasHoy = New System.Windows.Forms.RadioButton
        Me.rbCaratulasUsuarioPropias = New System.Windows.Forms.RadioButton
        Me.vista = New System.Windows.Forms.Panel
        Me.tablaCaratulas = New Zamba.Grid.PageGroupGrid.PageGroupGrid(CurrentUserId, Me)
        Me.Label2 = New System.Windows.Forms.Label
        Me.Splitter1 = New Zamba.AppBlock.ZSplitter
        Me.PanelTop = New Zamba.AppBlock.ZColorPanel
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cboDocType = New System.Windows.Forms.ListBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.chkReplicas = New System.Windows.Forms.CheckBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.ChkPreProcess = New System.Windows.Forms.CheckedListBox
        Me.ToolBar1 = New TD.SandBar.ToolBar
        Me.ButtonItem1 = New TD.SandBar.ButtonItem
        Me.ButtonItem5 = New TD.SandBar.ButtonItem
        Me.ButtonItem2 = New TD.SandBar.ButtonItem
        Me.ButtonItem3 = New TD.SandBar.ButtonItem
        Me.ButtonItem4 = New TD.SandBar.ButtonItem
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.mnuEliminar = New System.Windows.Forms.MenuItem
        Me.PanelMain.SuspendLayout()
        Me.PanelMIddle.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.PanelListView.SuspendLayout()
        Me.panelMostarCaratulas.SuspendLayout()
        Me.panelRangoFechas.SuspendLayout()
        Me.vista.SuspendLayout()
        Me.PanelTop.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
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
        'PanelMain
        '
        Me.PanelMain.BackColor = System.Drawing.Color.CornflowerBlue
        Me.PanelMain.Controls.Add(Me.PanelMIddle)
        Me.PanelMain.Controls.Add(Me.Splitter1)
        Me.PanelMain.Controls.Add(Me.PanelTop)
        Me.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMain.Location = New System.Drawing.Point(2, 2)
        Me.PanelMain.Name = "PanelMain"
        Me.PanelMain.Size = New System.Drawing.Size(1022, 744)
        Me.PanelMain.TabIndex = 0
        '
        'PanelMIddle
        '
        Me.PanelMIddle.AutoScroll = True
        Me.PanelMIddle.AutoScrollMinSize = New System.Drawing.Size(530, 0)
        Me.PanelMIddle.BackColor = System.Drawing.Color.CornflowerBlue
        Me.PanelMIddle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelMIddle.Controls.Add(Me.Panel5)
        Me.PanelMIddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMIddle.Location = New System.Drawing.Point(3, 226)
        Me.PanelMIddle.Margin = New System.Windows.Forms.Padding(5, 3, 5, 10)
        Me.PanelMIddle.Name = "PanelMIddle"
        Me.PanelMIddle.Size = New System.Drawing.Size(1019, 518)
        Me.PanelMIddle.TabIndex = 0
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel10)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1015, 514)
        Me.Panel5.TabIndex = 2
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel10.Controls.Add(Me.SplitContainer1)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(1015, 514)
        Me.Panel10.TabIndex = 17
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.Color.LightSteelBlue
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
        Me.SplitContainer1.Size = New System.Drawing.Size(1015, 514)
        Me.SplitContainer1.SplitterDistance = 400
        Me.SplitContainer1.SplitterWidth = 8
        Me.SplitContainer1.TabIndex = 0
        '
        'PanelIndexs
        '
        Me.PanelIndexs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.PanelIndexs.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PanelIndexs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelIndexs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelIndexs.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelIndexs.Location = New System.Drawing.Point(0, 0)
        Me.PanelIndexs.MinimumSize = New System.Drawing.Size(292, 0)
        Me.PanelIndexs.Name = "PanelIndexs"
        Me.PanelIndexs.Size = New System.Drawing.Size(400, 413)
        Me.PanelIndexs.TabIndex = 22
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.txtRemark)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.chkPrintIndex)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 413)
        Me.Panel3.MinimumSize = New System.Drawing.Size(292, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(400, 101)
        Me.Panel3.TabIndex = 108
        '
        'txtRemark
        '
        Me.txtRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRemark.BackColor = System.Drawing.Color.White
        Me.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemark.ForeColor = System.Drawing.Color.Black
        Me.txtRemark.Location = New System.Drawing.Point(49, 10)
        Me.txtRemark.MaxLength = 200
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(339, 63)
        Me.txtRemark.TabIndex = 106
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.Label5.Location = New System.Drawing.Point(2, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 14)
        Me.Label5.TabIndex = 107
        Me.Label5.Text = "Nota"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkPrintIndex
        '
        Me.chkPrintIndex.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkPrintIndex.BackColor = System.Drawing.Color.Transparent
        Me.chkPrintIndex.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkPrintIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkPrintIndex.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintIndex.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.chkPrintIndex.Location = New System.Drawing.Point(6, 74)
        Me.chkPrintIndex.Name = "chkPrintIndex"
        Me.chkPrintIndex.Size = New System.Drawing.Size(390, 23)
        Me.chkPrintIndex.TabIndex = 102
        Me.chkPrintIndex.Text = " Imprimir índices en blanco y códigos de barra de índices"
        Me.chkPrintIndex.UseVisualStyleBackColor = False
        '
        'PanelListView
        '
        Me.PanelListView.BackColor = System.Drawing.Color.LightSteelBlue
        Me.PanelListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelListView.Controls.Add(Me.panelMostarCaratulas)
        Me.PanelListView.Controls.Add(Me.vista)
        Me.PanelListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelListView.Location = New System.Drawing.Point(0, 0)
        Me.PanelListView.MinimumSize = New System.Drawing.Size(533, 0)
        Me.PanelListView.Name = "PanelListView"
        Me.PanelListView.Size = New System.Drawing.Size(607, 514)
        Me.PanelListView.TabIndex = 21
        '
        'panelMostarCaratulas
        '
        Me.panelMostarCaratulas.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelMostarCaratulas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelMostarCaratulas.Controls.Add(Me.panelRangoFechas)
        Me.panelMostarCaratulas.Controls.Add(Me.Label1)
        Me.panelMostarCaratulas.Controls.Add(Me.rbCaratulasUsuarioPropiasHoy)
        Me.panelMostarCaratulas.Controls.Add(Me.rbCaratulasUsuarioPropias)
        Me.panelMostarCaratulas.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelMostarCaratulas.ForeColor = System.Drawing.Color.Black
        Me.panelMostarCaratulas.Location = New System.Drawing.Point(0, 406)
        Me.panelMostarCaratulas.Name = "panelMostarCaratulas"
        Me.panelMostarCaratulas.Size = New System.Drawing.Size(605, 106)
        Me.panelMostarCaratulas.TabIndex = 113
        '
        'panelRangoFechas
        '
        Me.panelRangoFechas.Controls.Add(Me.btVer)
        Me.panelRangoFechas.Controls.Add(Me.rbCaratulasPropiasPorFechas)
        Me.panelRangoFechas.Controls.Add(Me.dtpFechaInicial)
        Me.panelRangoFechas.Controls.Add(Me.lbFechaFinal)
        Me.panelRangoFechas.Controls.Add(Me.dtpFechaFinal)
        Me.panelRangoFechas.Controls.Add(Me.lbFechaInicial)
        Me.panelRangoFechas.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelRangoFechas.Location = New System.Drawing.Point(0, 30)
        Me.panelRangoFechas.Name = "panelRangoFechas"
        Me.panelRangoFechas.Size = New System.Drawing.Size(603, 74)
        Me.panelRangoFechas.TabIndex = 122
        '
        'btVer
        '
        Me.btVer.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btVer.Location = New System.Drawing.Point(316, 26)
        Me.btVer.Margin = New System.Windows.Forms.Padding(0)
        Me.btVer.Name = "btVer"
        Me.btVer.Size = New System.Drawing.Size(107, 40)
        Me.btVer.TabIndex = 114
        Me.btVer.Text = "Aplicar filtro por fechas"
        '
        'rbCaratulasPropiasPorFechas
        '
        Me.rbCaratulasPropiasPorFechas.AutoSize = True
        Me.rbCaratulasPropiasPorFechas.Checked = True
        Me.rbCaratulasPropiasPorFechas.ForeColor = System.Drawing.Color.Black
        Me.rbCaratulasPropiasPorFechas.Location = New System.Drawing.Point(317, 47)
        Me.rbCaratulasPropiasPorFechas.Name = "rbCaratulasPropiasPorFechas"
        Me.rbCaratulasPropiasPorFechas.Size = New System.Drawing.Size(116, 17)
        Me.rbCaratulasPropiasPorFechas.TabIndex = 117
        Me.rbCaratulasPropiasPorFechas.TabStop = True
        Me.rbCaratulasPropiasPorFechas.Text = "Propias por Fechas"
        Me.rbCaratulasPropiasPorFechas.UseVisualStyleBackColor = True
        Me.rbCaratulasPropiasPorFechas.Visible = False
        '
        'dtpFechaInicial
        '
        Me.dtpFechaInicial.Location = New System.Drawing.Point(78, 24)
        Me.dtpFechaInicial.Name = "dtpFechaInicial"
        Me.dtpFechaInicial.Size = New System.Drawing.Size(222, 21)
        Me.dtpFechaInicial.TabIndex = 120
        '
        'lbFechaFinal
        '
        Me.lbFechaFinal.AutoSize = True
        Me.lbFechaFinal.ForeColor = System.Drawing.Color.Black
        Me.lbFechaFinal.Location = New System.Drawing.Point(4, 49)
        Me.lbFechaFinal.Name = "lbFechaFinal"
        Me.lbFechaFinal.Size = New System.Drawing.Size(61, 13)
        Me.lbFechaFinal.TabIndex = 119
        Me.lbFechaFinal.Text = "Fecha Final"
        '
        'dtpFechaFinal
        '
        Me.dtpFechaFinal.Location = New System.Drawing.Point(78, 47)
        Me.dtpFechaFinal.Name = "dtpFechaFinal"
        Me.dtpFechaFinal.Size = New System.Drawing.Size(222, 21)
        Me.dtpFechaFinal.TabIndex = 121
        '
        'lbFechaInicial
        '
        Me.lbFechaInicial.AutoSize = True
        Me.lbFechaInicial.ForeColor = System.Drawing.Color.Black
        Me.lbFechaInicial.Location = New System.Drawing.Point(4, 26)
        Me.lbFechaInicial.Name = "lbFechaInicial"
        Me.lbFechaInicial.Size = New System.Drawing.Size(66, 13)
        Me.lbFechaInicial.TabIndex = 118
        Me.lbFechaInicial.Text = "Fecha Inicial"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(3, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 116
        Me.Label1.Text = "Mostrar"
        '
        'rbCaratulasUsuarioPropiasHoy
        '
        Me.rbCaratulasUsuarioPropiasHoy.AutoSize = True
        Me.rbCaratulasUsuarioPropiasHoy.Checked = True
        Me.rbCaratulasUsuarioPropiasHoy.ForeColor = System.Drawing.Color.Black
        Me.rbCaratulasUsuarioPropiasHoy.Location = New System.Drawing.Point(165, 5)
        Me.rbCaratulasUsuarioPropiasHoy.Name = "rbCaratulasUsuarioPropiasHoy"
        Me.rbCaratulasUsuarioPropiasHoy.Size = New System.Drawing.Size(96, 17)
        Me.rbCaratulasUsuarioPropiasHoy.TabIndex = 115
        Me.rbCaratulasUsuarioPropiasHoy.TabStop = True
        Me.rbCaratulasUsuarioPropiasHoy.Text = "Propias de hoy"
        Me.rbCaratulasUsuarioPropiasHoy.UseVisualStyleBackColor = True
        '
        'rbCaratulasUsuarioPropias
        '
        Me.rbCaratulasUsuarioPropias.AutoSize = True
        Me.rbCaratulasUsuarioPropias.ForeColor = System.Drawing.Color.Black
        Me.rbCaratulasUsuarioPropias.Location = New System.Drawing.Point(64, 4)
        Me.rbCaratulasUsuarioPropias.Name = "rbCaratulasUsuarioPropias"
        Me.rbCaratulasUsuarioPropias.Size = New System.Drawing.Size(83, 17)
        Me.rbCaratulasUsuarioPropias.TabIndex = 113
        Me.rbCaratulasUsuarioPropias.Text = "Solo propias"
        Me.rbCaratulasUsuarioPropias.UseVisualStyleBackColor = True
        '
        'vista
        '
        Me.vista.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vista.BackColor = System.Drawing.Color.CornflowerBlue
        Me.vista.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.vista.Controls.Add(Me.tablaCaratulas)
        Me.vista.Controls.Add(Me.Label2)
        Me.vista.Location = New System.Drawing.Point(5, 3)
        Me.vista.Name = "vista"
        Me.vista.Size = New System.Drawing.Size(595, 398)
        Me.vista.TabIndex = 4
        '
        'tablaCaratulas
        '
        Me.tablaCaratulas.BackColor = System.Drawing.Color.CornflowerBlue
        Me.tablaCaratulas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tablaCaratulas.DataSource = CType(resources.GetObject("tablaCaratulas.DataSource"), Object)
        Me.tablaCaratulas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tablaCaratulas.Location = New System.Drawing.Point(0, 21)
        Me.tablaCaratulas.Name = "tablaCaratulas"
        Me.tablaCaratulas.ShortDateFormat = False
        Me.tablaCaratulas.Size = New System.Drawing.Size(593, 375)
        Me.tablaCaratulas.TabIndex = 30
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label2.Font = New System.Drawing.Font("Verdana", 10.08!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(593, 21)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Caratulas Anteriores"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Splitter1.Location = New System.Drawing.Point(0, 226)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 518)
        Me.Splitter1.TabIndex = 14
        Me.Splitter1.TabStop = False
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.CornflowerBlue
        Me.PanelTop.Color1 = System.Drawing.Color.WhiteSmoke
        Me.PanelTop.Color2 = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PanelTop.Controls.Add(Me.SplitContainer2)
        Me.PanelTop.Controls.Add(Me.ToolBar1)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(1022, 226)
        Me.PanelTop.TabIndex = 3
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BackColor = System.Drawing.Color.Gray
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 42)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer2.Size = New System.Drawing.Size(1022, 184)
        Me.SplitContainer2.SplitterDistance = 486
        Me.SplitContainer2.TabIndex = 110
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel1.Controls.Add(Me.cboDocType)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.CheckBox1)
        Me.Panel1.Controls.Add(Me.chkReplicas)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(486, 184)
        Me.Panel1.TabIndex = 108
        '
        'cboDocType
        '
        Me.cboDocType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboDocType.BackColor = System.Drawing.Color.White
        Me.cboDocType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cboDocType.DisplayMember = "Doc_Type.Doc_Type_Name"
        Me.cboDocType.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDocType.ForeColor = System.Drawing.Color.Black
        Me.cboDocType.ItemHeight = 14
        Me.cboDocType.Location = New System.Drawing.Point(12, 30)
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(460, 114)
        Me.cboDocType.TabIndex = 0
        Me.cboDocType.ValueMember = "Doc_Type.Doc_Type_Id"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.Label6.Location = New System.Drawing.Point(11, 11)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(219, 14)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "Seleccione el tipo de documento"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.CheckBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CheckBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CheckBox1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.CheckBox1.Location = New System.Drawing.Point(12, 149)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(200, 29)
        Me.CheckBox1.TabIndex = 26
        Me.CheckBox1.Text = "Imprimir Replicas"
        '
        'chkReplicas
        '
        Me.chkReplicas.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkReplicas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.chkReplicas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkReplicas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkReplicas.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReplicas.ForeColor = System.Drawing.Color.Blue
        Me.chkReplicas.Location = New System.Drawing.Point(17, 149)
        Me.chkReplicas.Name = "chkReplicas"
        Me.chkReplicas.Size = New System.Drawing.Size(200, 29)
        Me.chkReplicas.TabIndex = 26
        Me.chkReplicas.Text = "Imprimir Replicas"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.ChkPreProcess)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(532, 184)
        Me.Panel2.TabIndex = 109
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(13, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(159, 14)
        Me.Label3.TabIndex = 95
        Me.Label3.Text = "Acciones automatizadas"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ChkPreProcess
        '
        Me.ChkPreProcess.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChkPreProcess.BackColor = System.Drawing.Color.White
        Me.ChkPreProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ChkPreProcess.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPreProcess.ForeColor = System.Drawing.Color.Black
        Me.ChkPreProcess.Location = New System.Drawing.Point(15, 27)
        Me.ChkPreProcess.Name = "ChkPreProcess"
        Me.ChkPreProcess.Size = New System.Drawing.Size(503, 138)
        Me.ChkPreProcess.TabIndex = 94
        '
        'ToolBar1
        '
        Me.ToolBar1.Buttons.AddRange(New TD.SandBar.ToolbarItemBase() {Me.ButtonItem1, Me.ButtonItem5, Me.ButtonItem2, Me.ButtonItem3, Me.ButtonItem4})
        Me.ToolBar1.Closable = False
        Me.ToolBar1.DockLine = 1
        Me.ToolBar1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolBar1.Guid = New System.Guid("87e13489-b608-4639-94b7-264d69f2b0d4")
        Me.ToolBar1.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.Size = New System.Drawing.Size(1022, 42)
        Me.ToolBar1.Stretch = True
        Me.ToolBar1.TabIndex = 99
        Me.ToolBar1.Text = ""
        '
        'ButtonItem1
        '
        Me.ButtonItem1.Icon = CType(resources.GetObject("ButtonItem1.Icon"), System.Drawing.Icon)
        Me.ButtonItem1.IconSize = New System.Drawing.Size(32, 32)
        Me.ButtonItem1.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.Highest
        Me.ButtonItem1.Tag = "IMPRIMIR"
        Me.ButtonItem1.Text = "IMPRIMIR"
        Me.ButtonItem1.ToolTipText = "IMPRIMIR CARATULA"
        '
        'ButtonItem5
        '
        Me.ButtonItem5.Icon = CType(resources.GetObject("ButtonItem5.Icon"), System.Drawing.Icon)
        Me.ButtonItem5.IconSize = New System.Drawing.Size(32, 32)
        Me.ButtonItem5.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.Highest
        Me.ButtonItem5.Tag = "PREVISUALIZAR"
        Me.ButtonItem5.Text = "PREVISUALIZAR"
        Me.ButtonItem5.ToolTipText = "MUESTRA  LA PREVISUALIZACION DE LA CARATULA"
        '
        'ButtonItem2
        '
        Me.ButtonItem2.Icon = CType(resources.GetObject("ButtonItem2.Icon"), System.Drawing.Icon)
        Me.ButtonItem2.IconSize = New System.Drawing.Size(32, 32)
        Me.ButtonItem2.Importance = TD.SandBar.ToolbarItemBase.ToolBarItemImportance.Highest
        Me.ButtonItem2.Tag = "REPLICAR"
        Me.ButtonItem2.Text = "REPLICAR"
        Me.ButtonItem2.ToolTipText = "REPLICAR CARATULA"
        '
        'ButtonItem3
        '
        Me.ButtonItem3.BeginGroup = True
        Me.ButtonItem3.Icon = CType(resources.GetObject("ButtonItem3.Icon"), System.Drawing.Icon)
        Me.ButtonItem3.IconSize = New System.Drawing.Size(32, 32)
        Me.ButtonItem3.Tag = "LIMPIAR"
        Me.ButtonItem3.Text = "LIMPIAR INDICES"
        Me.ButtonItem3.ToolTipText = "LIMPIAR INDICES"
        '
        'ButtonItem4
        '
        Me.ButtonItem4.Icon = CType(resources.GetObject("ButtonItem4.Icon"), System.Drawing.Icon)
        Me.ButtonItem4.IconSize = New System.Drawing.Size(32, 32)
        Me.ButtonItem4.Tag = "ACTUALIZAR"
        Me.ButtonItem4.Text = "ACTUALIZAR LISTA"
        Me.ButtonItem4.ToolTipText = "ACTUALIZAR LISTA"
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
        'UCBarCode
        '
        Me.Controls.Add(Me.PanelMain)
        Me.Name = "UCBarCode"
        Me.Padding = New System.Windows.Forms.Padding(2)
        Me.Size = New System.Drawing.Size(1026, 748)
        Me.PanelMain.ResumeLayout(False)
        Me.PanelMIddle.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.PanelListView.ResumeLayout(False)
        Me.panelMostarCaratulas.ResumeLayout(False)
        Me.panelMostarCaratulas.PerformLayout()
        Me.panelRangoFechas.ResumeLayout(False)
        Me.panelRangoFechas.PerformLayout()
        Me.vista.ResumeLayout(False)
        Me.PanelTop.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    '#Region "Validaciones"
    'Private Sub txtcambios_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIndice.TextChanged, txtValor.TextChanged
    '    validar_btnimprimir()
    'End Sub

    'Private Sub validar_btnimprimir()
    '    If txtIndice.Text = "" Or txtValor.Text = "" Then
    '        Me.bttnImprimir.Enabled = False
    '    Else
    '        Me.bttnImprimir.Enabled = True
    '    End If
    'End Sub
    'Private Sub txtAncho_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If Not IsNumeric(e.KeyValue) Then
    '        'txtAncho.Text = txtAncho.Text.Substring(0, txtAncho.Text.Length - 1)
    '        Beep() : Beep() : Beep()
    '    End If
    'End Sub
    '#End Region

    '#Region "Imprimir XML"
    'Dim ds As New dsDatos
    'Private WithEvents pdocumXml As New Printing.PrintDocument
    'Private Sub bttnImprimirXml_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bttnImprimirXml.Click
    '    Try
    '        Dim ds As New dsDatos
    '        If load_xml() Then
    '            '				Vals.Width = CInt(Me.txtAncho.Text)
    '            '				Vals.Height = CInt(Me.txtLargo.Text)
    '            '				Vals.Ratio = CInt(Me.txtRadio.Text)
    '            '				Vals.Scale = CInt(Me.txtEscala.Text)
    '            pdocumXml.Print()
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try
    'End Sub
    'Private Function load_xml() As Boolean
    '    Dim filename As String
    '    Try
    '        If Me.OpenFileDialog1.ShowDialog = DialogResult.OK Then
    '            filename = Me.OpenFileDialog1.FileName
    '            ds.ReadXml(filename)
    '            load_xml = True
    '        Else
    '            load_xml = False
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Function
    'Private Sub pdocumXml_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdocumXml.PrintPage
    '    Dim i As Integer
    '    Dim y As Integer = 1
    '    Dim y2 As Integer = 40
    '    Dim y3 As Integer = 25
    '    For i = 0 To ds.Tables(0).Rows.Count - 1
    '        e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(15, y, 765, 100))
    '        e.Graphics.DrawString(ds.Tables(0).Rows(i).Item(0), New Font(FontFamily.GenericSansSerif.GenericSansSerif, 10, FontStyle.Regular), Brushes.Black, 40, y2)
    '        e.Graphics.DrawString(ds.Tables(0).Rows(i).Item(1), New Font(FontFamily.GenericSansSerif.GenericSansSerif, 10, FontStyle.Regular), Brushes.Black, 250, y2)

    '        Dim BC As New Barcodectrl
    '        BC.BarCode = ds.Tables(0).Rows(i).Item(1)
    '        BC.HeaderText = ds.Tables(0).Rows(i).Item(0)
    '        BC.LeftMargin = 5
    '        BC.TopMargin = 10
    '        e = BC.PrintImage(e)

    '        barcodeimg = Barcode_Motor.Print(e, ds.Tables(0).Rows(i).Item(1), 520, y3)

    '        y += 100
    '        y2 += 100
    '        y3 += 100
    '    Next
    'End Sub
    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim dset As New dsDatos
    '    Dim fi As New System.IO.FileInfo("d:\pablo\vb.net\barcode.xml")
    '    Dim row As dsDatos.BarraCodigosRow = dset.BarraCodigos.NewBarraCodigosRow
    '    Dim row2 As dsDatos.BarraCodigosRow = dset.BarraCodigos.NewBarraCodigosRow
    '    row.nombre = "fecha"
    '    row.valor = "787"
    '    row2.nombre = "seccion"
    '    row2.valor = "4"
    '    dset.BarraCodigos.AddBarraCodigosRow(row)
    '    dset.BarraCodigos.AddBarraCodigosRow(row2)
    '    dset.WriteXml(fi.FullName)
    'End Sub
    '#End Region

    '#Region "Guardar XML"
    'Private Sub ZButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZButton1.Click
    'Try
    '	Dim ds As New dsDatos
    '	For Each Index As Index In Me.NewResult.Indexs
    '		Dim row as new dsDatos.BarraCodigosRow				    row.nombre = Index.Name
    '		row.valor = Index.dataDescription
    '		row.documento = ""
    '		ds.Tables(0).Rows.Add(row)
    '	Next
    '	ds.Tables(0).Rows(0).Item(2) = Me.DocTypes(CboDocType.SelectedIndex).Name.Trim
    'Catch ex As Exception
    '	zamba.core.zclass.raiseerror(ex)
    '	Exit Sub
    'End Try
    'End Sub
    '#End Region

    ''' <summary>
    ''' Evento que se ejecuta cuando se carga el formulario que muestra las caratulas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    25/06/2008  Modified  Se agrego el método verifyTablaCaratulas()
    ''' </history>
     Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            LoadAvaibleDocTypes()
            LoadPreprocess()
            loadPendingBarCode()
            RefreshGrid()

            verifyTablaCaratulas()

        Catch ex As Exception

            Zamba.Core.ZClass.raiseerror(ex)
            MessageBox.Show(ex.ToString, "ZAMBA", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As TD.SandBar.ToolBarItemEventArgs) Handles ToolBar1.ButtonClick
        Try
            Select Case CStr(e.Item.Tag)
                Case "IMPRIMIR"
                    If Not IsNothing(Me.NewResult) And RePrint = 0 Then
                        Evento_btnGenerar()
                    Else
                        Evento_btnGenerar(True)
                        RePrint = 0
                    End If
                Case "REPLICAR"
                    Evento_btnReplicar_Click_1()
                Case "ACTUALIZAR"
                    Me.loadPendingBarCode()
                Case "LIMPIAR"
                    Evento_btnLimpiar_Click()
                Case "PREVISUALIZAR"
                    Evento_btnGenerar(False)
            End Select
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "Autocompletar"
    Private Sub autocomplete(ByRef Result As IResult, ByVal _index As IIndex)
        Try
            Dim newFrmGrilla As New Viewers.frmGrilla()
            If Not AutocompleteBCBusiness.ExecuteAutoComplete(Result, _index, newFrmGrilla) Is Nothing Then
                LoadIndexViewer(Result)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Region ListView"

#Region "Eventos..."

    Private Sub chkTodasLasCaractulas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.mostarCaratulasDeTodosLosUsuarios()
    End Sub
    Private Sub rbCaratulasUsuarioPropiasHoy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCaratulasUsuarioPropiasHoy.CheckedChanged
        Me.mostarCaratulasPorUsuarioLogueadoYporFechaHoy()
    End Sub
    Private Sub rbCaratulasUsuarioPropias_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCaratulasUsuarioPropias.CheckedChanged
        Me.mostarCaratulasPorUsuarioLogueado()
    End Sub
    Private Sub tablaCaratulas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Data As String = String.Empty

        Try
            If Me.tablaCaratulas.OutLookGrid.SelectedRows.Count > 0 Then
                '' Dim docItem As DocItem = docItem.getItem(Me.tmpDocIdTypeList, _
                '' Me.tablaCaratulas.OutLookGrid.SelectedRows(0).Index)
                RePrint = 1
                NewResult = Results_Business.GetNewNewResult(Convert.ToInt64(Me.tablaCaratulas.OutLookGrid.SelectedRows(0).Cells("doc_id").Value), _
                DocTypesBusiness.GetDocType(Convert.ToInt64(Me.tablaCaratulas.OutLookGrid.SelectedRows(0).Cells("doc_type_id").Value), True))
                NewResult.Indexs = ZCore.FilterIndex(CInt(NewResult.DocType.ID))
                For Each CurrentIndex As Index In NewResult.Indexs
                    Data = Results_Business.GetIndexValueFromDoc_I(NewResult.DocType.ID, NewResult.ID, CurrentIndex.ID)
                    CurrentIndex.Data = Data
                    CurrentIndex.DataTemp = Data
                Next
                CoverIdToPrint = CInt(Me.tablaCaratulas.OutLookGrid.SelectedRows(0).Cells("caratula").Value)
                LoadIndexViewer(NewResult, True)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
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
            Me.tablaCaratulas.DataSource = dt
        End If
        Me.setDefaultCursor()
        'Me.btImprimir.Enabled = True
        Me.panelMostarCaratulas.Enabled = True
    End Sub


    ' Deshabilita la impresion de la lista caratulas
    ' mientras estas se cargan en la vista...
    Private Sub setInitLoadCaratulas()
        'Me.btImprimir.Enabled = False
        Me.setWaitCursor()
        Me.panelMostarCaratulas.Enabled = False
    End Sub


    ' Muestra el cursor Wait para 
    ' la vista de Lista de caratulas
    Private Sub setWaitCursor()
        Me.tablaCaratulas.Cursor = Cursors.WaitCursor
        Me.panelMostarCaratulas.Cursor = Cursors.WaitCursor
    End Sub


    ' Muestra el cursor por defecto para 
    ' la vista de Lista de caratulas
    Private Sub setDefaultCursor()
        Me.tablaCaratulas.Cursor = Cursors.Default
        Me.panelMostarCaratulas.Cursor = Cursors.Default
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
            Me.tablaCaratulas.DataSource = Nothing

            ' lanza la carga de la vista lista de caratulas
            ' solo si hay caratulas disponibles..
            If (ds.Rows.Count > 0) Then
                Me.tablaCaratulas.DataSource = ds
            End If

            verifyTablaCaratulas()

            ' Lanza la crga de la vista lista de caratulas...
            'T = New Thread(New ParameterizedThreadStart(AddressOf Me.LoadCaractulas))
            'T.Start(ds)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Llena una lista con las cartulas creadas 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	13/07/2006	Created
    ''' 	[Adrian] 14/11/2006	Modified 
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Private Sub LoadCaractulas(ByVal o As Object)
    '    'Try
    '    '    'Dim ds As DataSet = CType(o, DataSet)


    '    '    'Me.tablaCaratulas.DataSource = ds

    '    '    '' borra la vista..
    '    '    'Me.Invoke(New dDelegateWithOutParams(AddressOf Me.setInitLoadCaratulas))
    '    '    'Dim dsCaratulas As DsCaratulas = New DsCaratulas()


    '    '    'Thread.Sleep(0)

    '    '    '' Toma los nombres de las columnas
    '    '    ''  que quedaran finalmente en la tabla...
    '    '    'Dim columnasVisibles As New List(Of String)
    '    '    'For Each columna As DataColumn In dsCaratulas.Tables(0).Columns() 'Columnas del dataset tipado
    '    '    '    columnasVisibles.Add(columna.Caption)
    '    '    'Next

    '    '    'Thread.Sleep(0)

    '    '    '' Toma los nombres de las columnas 
    '    '    '' que no se desea ver  de la tabla...
    '    '    'Dim ColumnasNoVisibles As New List(Of String)
    '    '    'For Each columna As DataColumn In ds.Tables(0).Columns 'Columnas de el dataset posta
    '    '    '    columnasVisibles.Sort()
    '    '    '    If 0 > columnasVisibles.BinarySearch(columna.Caption) Then
    '    '    '        ColumnasNoVisibles.Add(columna.Caption)
    '    '    '    End If
    '    '    'Next

    '    '    'Thread.Sleep(0)

    '    '    '' Agrega la tabla a un DataSet tipado con el formato de la vista..
    '    '    'ds.Tables(0).TableName = dsCaratulas.ZBarCode.TableName
    '    '    'If ds.Tables(0).Rows.Count > 0 Then
    '    '    '    Try
    '    '    '        dsCaratulas.Merge(ds)
    '    '    '    Catch ex As Exception
    '    '    '       zamba.core.zclass.raiseerror(ex)
    '    '    '    End Try

    '    '    '    ' Crea una lista paralela con (docId,docType,posicion en la lista vista)
    '    '    '    ' para luego por medio de la posicion seleccionada 
    '    '    '    ' poder cargar los indices de una caratula con docId y docType
    '    '    '    tmpDocIdTypeList = DocItem.getDocItemList(ds.Tables(0), ds.Tables(0).Columns.IndexOf("doc_id"), ds.Tables(0).Columns.IndexOf("doc_type_id"))

    '    '    '    For Each name As String In ColumnasNoVisibles
    '    '    '        If dsCaratulas.Tables(0).Columns.IndexOf(name) <> 0 Then
    '    '    '            dsCaratulas.Tables(0).Columns.Remove(name)
    '    '    '        End If
    '    '    '    Next
    '    '    'End If
    '    '    'Thread.Sleep(0)

    '    '    '' carga la vista con la tabla formateada..
    '    '    'Me.Invoke(New dloadViewCaratulas(AddressOf Me.loadViewCaratulas), _
    '    '    ' dsCaratulas.Tables(0))

    '    'Catch ex1 As ThreadInterruptedException
    '    '    Return
    '    'Catch ex As Exception
    '    '   zamba.core.zclass.raiseerror(ex)
    '    'End Try
    'End Sub
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
            Dim AvaibleUserRights As ArrayList = DocTypesBusiness.GetDocTypesbyUserRightsOfView(UserBusiness.Rights.CurrentUser.ID, Zamba.Core.RightsType.Create)

            If AvaibleUserRights.Count > 0 Then
                RemoveHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChanged
                cboDocType.BeginUpdate()
                cboDocType.DisplayMember = "Name"
                cboDocType.ValueMember = "Id"
                cboDocType.DataSource = AvaibleUserRights
                cboDocType.EndUpdate()
                AddHandler cboDocType.SelectedIndexChanged, AddressOf SelectedIndexChanged

                If Not IsNothing(_loadedResult) Then
                    For Each CurrentDocType As DocType In cboDocType.Items
                        If _loadedResult.DocType.ID = CurrentDocType.ID Then
                            cboDocType.SelectedItem = CurrentDocType
                            Exit For
                        End If
                    Next
                End If

            Else
                MessageBox.Show("Error 013: No hay definidos Tipos de Documentos para realizar la indexacion o no tiene Permisos para crear Documentos", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: 014: Ocurrio un Error al cargar los Tipos de Documentos para la Indexación ", "Zamba Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub SelectDocType()
        cboDocType.SelectedIndex = 0
        SelectedIndexChanged(cboDocType, New System.EventArgs)
    End Sub

    Private Sub SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            NewResult = New NewResult
            If cboDocType.SelectedIndex > -1 Then
                RePrint = 0
                NewResult.Parent = DocTypesBusiness.GetDocType(System.Convert.ToInt32((cboDocType.SelectedValue)), True)
                'NewResult.DocTypeName = Me.DsDocTypes.DOC_TYPE.Rows(Me.CboDocType.SelectedIndex).Item("DOC_TYPE_NAME")
                NewResult.DocumentalId = 0
                NewResult.Indexs = ZCore.FilterIndex(CInt(NewResult.Parent.ID))

                If IsNothing(Me._loadedResult) = False Then
                    For Each I As Index In Me._loadedResult.Indexs
                        For Each I1 As Index In NewResult.Indexs
                            If I.ID = I1.ID Then
                                I1.Data = I.Data
                                I1.DataTemp = I.DataTemp
                                I1.dataDescription = I.dataDescription
                                I1.dataDescriptionTemp = I.dataDescriptionTemp
                                Exit For
                            End If
                        Next
                    Next
                End If
                LoadIndexViewer(NewResult)
                Me._loadedResult = NewResult
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub LoadIndexViewer(ByVal newResult As NewResult, Optional ByVal replaceData As Boolean = False)
        If IsNothing(_ucViewer) Then
            _ucViewer = New UCIndexIndexerViewer
            _ucViewer.Dock = DockStyle.Fill
            PanelIndexs.Controls.Add(_ucViewer)
            _ucViewer.BringToFront()
        End If
        'TODO Poner un IF para verificar si esta checkeado la opcion autocomplete.

        RemoveHandler _ucViewer.IndexChanged, AddressOf Me.autocomplete
        AddHandler _ucViewer.IndexChanged, AddressOf Me.autocomplete

        'Try
        '    CODIGO PARA CORREGIR EL AUTOSCROLL PARA QUE SE VEA BIEN, POR POCKET
        '    CType(Me.UcViewer.Panel5.Parent, UCIndexIndexerViewer).AutoScroll = False
        '    Me.UcViewer.Panel5.AutoScroll = True
        'Catch ex As Exception
        '   zamba.core.zclass.raiseerror(ex)
        'End Try

        _ucViewer.ShowDocument(DirectCast(newResult, NewResult), replaceData)
    End Sub

    Private Sub LoadIndexViewer(ByRef Result As IResult)
        If IsNothing(_ucViewer) Then
            _ucViewer = New UCIndexIndexerViewer
            _ucViewer.Dock = DockStyle.Fill
            PanelIndexs.Controls.Add(_ucViewer)
            _ucViewer.BringToFront()
        End If

        RemoveHandler _ucViewer.IndexChanged, AddressOf Me.autocomplete
        AddHandler _ucViewer.IndexChanged, AddressOf Me.autocomplete

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
        Dim frm As frmPrintPreView
        Dim dlg As PrintDialog
        frm = Nothing
        dlg = Nothing

        Try
            Me.NewResult.DocType.Indexs = Me.NewResult.Indexs
            '(pablo) 06-04-2011
            'se valida que los indices de sustitucion completados tengan valores coherentes
            If Results_Business.ValidateDescriptionInSustIndex(NewResult) = False Then
                Dim response As Microsoft.VisualBasic.MsgBoxResult
                response = MessageBox.Show("Alguno de los indices ingresados no coindice con los datos de su lista. ¿Desea Continuar?", "Zamba", MessageBoxButtons.YesNo)
                If response = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
            If Results_Business.ValidateIndexDataFromDoctype(NewResult) <> False Then
                If UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, CInt(NewResult.DocType.ID)) AndAlso Results_Business.ValidateIndexData(NewResult) = False Then
                    MessageBox.Show("Hay indices requeridos incompletos", "Zamba", MessageBoxButtons.OK)
                    Exit Sub
                End If
            Else
                MessageBox.Show("Hay indices requeridos incompletos", "Zamba", MessageBoxButtons.OK)
                Exit Sub
            End If

            If bImprimir And RePrint = 1 Then
                'dlg = New PrintDialog()
                'dlg.Document = pdocumdoctypes
                'If dlg.ShowDialog() = DialogResult.OK Then
                '    Me._barcodeId = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.Caratulas)
                '    Me._oldId = Me._barcodeId
                '    NewResult.ID = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.DOCID)
                '    NewResult.FolderId = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.FOLDERID)
                '    GetIndexsFromTemp()
                '    If BarcodesBusiness.Insert(NewResult, CInt(NewResult.Parent.ID), CInt(UserBusiness.CurrentUser.ID), _barcodeId) Then
                '        pdocumdoctypes.Print()
                '        UserBusiness.Rights.SaveAction(NewResult.ID, ObjectTypes.ModuleBarCode, RightsType.Create, "Usuario Imprimio Caratula")
                '    Else
                '        MessageBox.Show("No se pudo insertar el código de barras", "Error en Inserción", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    End If
                'End If         

                dlg = New PrintDialog()
                dlg.Document = pdocumdoctypes
                If dlg.ShowDialog() = DialogResult.OK Then
                    Me._barcodeId = CoverIdToPrint
                    'Me._oldId = Me._barcodeId
                    'NewResult.ID = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.DOCID)
                    'NewResult.FolderId = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.FOLDERID)
                    Me._oldId = CoverIdToPrint
                    GetIndexsFromTemp()
                    pdocumdoctypes.Print()
                    UserBusiness.Rights.SaveAction(NewResult.ID, ObjectTypes.ModuleBarCode, RightsType.Create, "Usuario RE Imprimio Caratula")
                    Me._barcodeId = 0
                Else
                    Exit Sub
                End If
            ElseIf bImprimir = True Then


                dlg = New PrintDialog()
                dlg.Document = pdocumdoctypes
                If dlg.ShowDialog() = DialogResult.OK Then
                    Me._barcodeId = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.Caratulas)
                    Me._oldId = Me._barcodeId
                    NewResult.ID = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.DOCID)
                    NewResult.FolderId = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.FOLDERID)
                    GetIndexsFromTemp()
                    If BarcodesBusiness.Insert(NewResult, CInt(NewResult.Parent.ID), CInt(UserBusiness.CurrentUser.ID), _barcodeId, True) Then
                        pdocumdoctypes.Print()
                        UserBusiness.Rights.SaveAction(NewResult.ID, ObjectTypes.ModuleBarCode, RightsType.Create, "Usuario Imprimio Caratula")
                    Else
                        MessageBox.Show("No se pudo insertar el código de barras", "Error en Inserción", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    'dlg = New PrintDialog()
                    'dlg.Document = pdocumdoctypes
                    'If dlg.ShowDialog() = DialogResult.OK Then
                    '    Me._barcodeId = CoverIdToPrint
                    '    'Me._oldId = Me._barcodeId
                    '    'NewResult.ID = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.DOCID)
                    '    'NewResult.FolderId = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.FOLDERID)
                    '    Me._oldId = CoverIdToPrint
                    '    GetIndexsFromTemp()
                    '    pdocumdoctypes.Print()
                    '    UserBusiness.Rights.SaveAction(NewResult.ID, ObjectTypes.ModuleBarCode, RightsType.Create, "Usuario RE Imprimio Caratula")
                    '    Me._barcodeId = 0
                    'Else
                    '    Exit Sub
                    'End If
                End If
            Else
                If 0 = Me._barcodeId Then
                    Me._barcodeId = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.Caratulas)
                End If
                Me._oldId = Me._barcodeId
                If IsNothing(NewResult) = False Then
                    NewResult.ID = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.DOCID)
                    NewResult.FolderId = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.FOLDERID)
                    GetIndexsFromTemp()
                    frm = New frmPrintPreView(pdocumdoctypes)
                    frm.WindowState = FormWindowState.Maximized
                    frm.StartPosition = FormStartPosition.CenterScreen
                    frm.ShowDialog(Me)
                Else
                    MsgBox("Verifique los datos ingresados y seleccionados y vuelva a intentarlo")
                End If
            End If

            loadPendingBarCode()
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

    End Sub

    Private Sub RefreshGrid()
        'Refrescar la grilla
        '        If rbCaratulasTodas.Checked = True Then
        'Me.mostarCaratulasDeTodosLosUsuarios()
        If rbCaratulasUsuarioPropiasHoy.Checked Then
            Me.mostarCaratulasPorUsuarioLogueadoYporFechaHoy()
        ElseIf rbCaratulasUsuarioPropias.Checked Then
            Me.mostarCaratulasPorUsuarioLogueado()
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

        If (NewResult IsNot Nothing) Then

            Try
                'Mismo id que la anterior caratula
                'Me.CaratulaId = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.Caratulas) - 1
                Me._barcodeId = Me._oldId
                NewResult.ID = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.DOCID)
                NewResult.FolderId = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.FOLDERID)
                GetIndexsFromTemp()
                If BarcodesBusiness.Insert(NewResult, CInt(NewResult.Parent.ID), CInt(UserBusiness.CurrentUser.ID), _barcodeId, True) Then
                    If chkReplicas.Checked Then
                        pdocumdoctypes.Print()
                    End If
                    UserBusiness.Rights.SaveAction(NewResult.ID, ObjectTypes.ModuleBarCode, RightsType.Replicate, "Usuario Replico Caratula")
                Else
                    MessageBox.Show("No se pudo insertar el código de barras", "Error en Inserción", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                loadPendingBarCode()
                'Refrescar la grilla
                RefreshGrid()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

        Else
            MessageBox.Show("No tiene permiso para replicar esta caratula", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub

    Private Sub GetIndexsFromTemp()

        Try
            If _ucViewer.IsValid Then
                For Each _index As Index In Me.NewResult.Indexs
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
            Me._ucViewer.cleanIndexs()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


    ''' <summary>
    '''Se muestra la lista de caractulas filtrada segun
    '''la opcion elegida en "Mostrar Caratulas"(Vista)
    '''Nota: Este metodo encierra la logica de seleccion
    '''de los check, puesto que se debe llamar en ciertos
    '''metodos donde no se debe saber que check se selecciono
    ''' </summary>
    ''' <remarks>
    '''-------------------------------------------------------------
    '''		               Adrian 3/11/2006
    '''-------------------------------------------------------------
    ''' [Update - Andres 04/01/2007]
    ''' Se Agrego rbTodasPorFechas , que permite seleccionar TODAS las caratulas mias y de otros en un rango 
    ''' especifico de fechas.
    ''' </remarks>
    Protected Sub loadPendingBarCode()
        Try
            If rbCaratulasUsuarioPropiasHoy.Checked Then ' Filtra las caractulas del usuario del dia hoy...
                mostarCaratulasPorUsuarioLogueadoYporFechaHoy()
            ElseIf rbCaratulasUsuarioPropias.Checked Then ' Filtra las caractulas del usuario...
                mostarCaratulasPorUsuarioLogueado()
            End If
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
            LoadCaractulasAsincronico(BarcodesBusiness.dsFilterCaratulas(CInt(UserBusiness.CurrentUser.ID), Date.Today))
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        'LoadCaractulasAsincronico(dsFilterCaratulasPorUsuarioYFechaActual(UserBusiness.CurrentUser.ID, Date.Today))
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
        LoadCaractulasAsincronico(BarcodesBusiness.dsFilterCaratulas(CInt(UserBusiness.CurrentUser.ID), m_fechaInicial, m_fechaFinal))
    End Sub
    ''' <summary>
    ''' Carga las caratulas en el Formulario filtrando por Usuario Logeado 
    ''' </summary>
    ''' 
    ''' <remarks></remarks>
    Protected Sub mostarCaratulasPorUsuarioLogueado()
        LoadCaractulasAsincronico(BarcodesBusiness.dsFilterCaratulas((CInt(UserBusiness.CurrentUser.ID))))
    End Sub
    ''' <summary>
    ''' Carga las caratulas en el Formulario sin filtrar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub mostarCaratulasDeTodosLosUsuarios()
        LoadCaractulasAsincronico(BarcodesBusiness.dsAllCaratulas())
    End Sub
#End Region
    Private Sub mnuEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEliminar.Click
        Evento_mnuEliminar_Click()
    End Sub

    Private Sub btVer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btVer.Click
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
        If Not Me.tablaCaratulas.OutLookGrid.RowCount < 1 Then
            Return System.Convert.ToInt64(Me.tablaCaratulas.OutLookGrid.SelectedRows(0).Cells(1))
        End If

        Return -1
    End Function

    Private Sub Evento_mnuEliminar_Click()
        Try
            Dim carId As System.Int64 = Me.getCarIdSelectecRow()
            If carId >= 0 Then
                If MessageBox.Show("Esta seguro que desea eliminar la caratula " _
                 & carId.ToString & " ?    ", _
                 "Generador de Codigo de Barras", _
                 MessageBoxButtons.YesNo, _
                 MessageBoxIcon.Question) = DialogResult.Yes Then
                    Try
                        BarcodesBusiness.Delete(CInt(carId))
                    Catch ex As Exception
                        MessageBox.Show("No se pudo borrar la caratula seleccionada", _
                        "Generador de Codigo de Barras", _
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Print"
    Private Sub pdocumdoctypes_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdocumdoctypes.PrintPage
        Evento_pdocumdoctypes_PrintPage(e)
    End Sub

    Private Sub Evento_pdocumdoctypes_PrintPage(ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        ' Dim i As Integer
        Dim y As Integer = 240
        Dim y2 As Integer = 270
        Dim y3 As Integer = 252
        Dim ylst As Integer = 282
        Dim ynam As Integer = 282

        'Dim doctypeALL As String
        Dim doctypeID As Int32
        Dim indexALL As String = String.Empty
        Dim dataBC As String = String.Empty
        Dim dataALL As String = String.Empty
        Dim IndexCount As Integer = 0
        Dim PrintBarcodeIndexOption As Boolean = Me.chkPrintIndex.Checked

        'Fecha y Hora
        e.Graphics.DrawString(Now.ToString, New System.Drawing.Font("Times New Roman", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), Brushes.Black, 590, 75)
        e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, 100, 735, 70))

        'Caratula
        e.Graphics.DrawString("Caratula Nro:", New Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold), Brushes.Black, 40, 125)
        e.Graphics.DrawString(_barcodeId.ToString, New Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold), Brushes.Black, 40, 138)
        _barcodeImage = Barcode_Motor.Print(e, _barcodeId.ToString, 150, 112)
        e.Graphics.DrawLine(New Pen(Color.Black), 390, 100, 390, 170)
        e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, 170, 735, 70))

        'User
        e.Graphics.DrawString(UserBusiness.Rights.CurrentUser.Name(), New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 400, 125)
        e.Graphics.DrawString("(" & UserBusiness.CurrentUser.ID.ToString & ")", New Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Black, 400, 138)
        _barcodeImage = Barcode_Motor.Print(e, UserBusiness.CurrentUser.ID.ToString, 520, 110)

        'docType
        '        doctypeID = Me.DsDocTypes.DOC_TYPE(CboDocType.SelectedIndex).DOC_TYPE_ID
        e.Graphics.DrawString(Me.NewResult.DocType.Name.Trim & "    (" & Me.NewResult.DocType.ID.ToString & ")", New Font(FontFamily.GenericSansSerif, 11, FontStyle.Regular), Brushes.Black, 40, 199)
        _barcodeImage = Barcode_Motor.Print(e, doctypeID.ToString, 520, 182)

        IndexCount = 0
        For Each printIndex As Index In Me.NewResult.Indexs
            'Imprimo comentario y salgo
            If IndexCount = 11 Then
                e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, y, 735, 70))
                e.Graphics.DrawString("Nota: ", New Font(FontFamily.GenericSerif, 9, FontStyle.Bold), Brushes.Black, 40, y2)
                If Me.txtRemark.Text.Length > 100 Then
                    'e.Graphics.DrawString(Me.txtRemark.Text.Substring(0, 100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
                    'e.Graphics.DrawString(Me.txtRemark.Text.Substring(100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2 + 13)
                    e.Graphics.DrawString(Me.txtRemark.Text.Substring(0, 100) & vbCrLf & Me.txtRemark.Text.Substring(100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
                Else
                    e.Graphics.DrawString(Me.txtRemark.Text, New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
                End If
                Exit Sub
            End If

            'Si el indice esta vacio no imprimo rectangulo
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
                        If printIndex.DropDown <> IndexAdditionalType.AutoSustitución Then

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

        'Comentario en caso de menos de 10 indices
        e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(30, y, 735, 70))
        e.Graphics.DrawString("Nota: ", New Font(FontFamily.GenericSerif, 9, FontStyle.Bold), Brushes.Black, 40, y2)
        If Me.txtRemark.Text.Length > 100 Then
            e.Graphics.DrawString(Me.txtRemark.Text.Substring(0, 100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
            e.Graphics.DrawString(Me.txtRemark.Text.Substring(100), New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2 + 13)
        Else
            e.Graphics.DrawString(Me.txtRemark.Text, New Font(FontFamily.GenericSerif, 9, FontStyle.Regular), Brushes.Black, 80, y2)
        End If

        If Not IsNothing(indexALL) Then indexALL = Nothing
        If Not IsNothing(dataBC) Then dataBC = Nothing
        If Not IsNothing(dataALL) Then dataALL = Nothing

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

#Region "Preprocesos"
    Private preprocessList As ArrayList
    Dim user As IUser
    Private Sub LoadPreprocess()
        Try
            user = UserBusiness.Rights.CurrentUser
            Me.preprocessList = PreProcessFactory.GetProcessByUser(user)
            ChkPreProcess.BeginUpdate()
            ChkPreProcess.DisplayMember = "Name"
            ChkPreProcess.ValueMember = "Id"
            Me.ChkPreProcess.DataSource = preprocessList
            ChkPreProcess.EndUpdate()
        Catch e As Exception
            Zamba.Core.ZClass.raiseerror(e)
        End Try
    End Sub
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

    Private Sub dtpFechaInicial_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFechaInicial.ValueChanged
        If dtpFechaInicial.Value > dtpFechaFinal.Value Then
            dtpFechaInicial.Value = dtpFechaFinal.Value
        End If
    End Sub

    Private Sub dtpFechaFinal_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFechaFinal.ValueChanged
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

        If (Me.tablaCaratulas IsNot Nothing) Then

            ' Si la grilla que contiene las caratulas es mayor a cero y hay al menos una fila seleccionada en la
            ' grilla activar botón Replicar
            If ((Me.tablaCaratulas.OutLookGrid.Rows.Count > 0) AndAlso _
               (Me.tablaCaratulas.OutLookGrid.SelectedRows IsNot Nothing)) Then
                ButtonItem2.Enabled = True
            Else
                ButtonItem2.Enabled = False
            End If

        End If

    End Sub

    Private _fc As IFiltersComponent

    Public Property Fc() As IFiltersComponent Implements IFilter.Fc
        Get
            Return _fc
        End Get
        Set(ByVal value As IFiltersComponent)
            _fc = value
        End Set
    End Property
    Private _lastPage As Integer

    Public Property LastPage() As Integer Implements IFilter.LastPage
        Get
            Return _lastPage
        End Get
        Set(ByVal value As Integer)
            _lastPage = value
        End Set
    End Property
    Public Sub ShowTaskOfDT() Implements IFilter.ShowTaskOfDT

    End Sub
End Class