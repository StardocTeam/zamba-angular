Imports Zamba.Controls.My.Resources
Imports Zamba.Core.WF
Imports Zamba.Core
Imports Zamba.Viewers
Imports Zamba.ZTimers
Imports System.Threading
Imports Zamba.AdminControls
Imports Zamba.Core.Searchs
Imports Telerik.WinControls.UI

Namespace WF.ResultsCtls

    Public Class UCWFTreeControlForClient
        Inherits ZControl
        Implements IDisposable

        Public Delegate Function IsNodeVisibleDelegate(ByVal node As RadTreeNode) As Boolean
        Public Delegate Function IsStepVisibleAndExpandedDelegate(ByVal stepNode As StepNodeIdAndName) As Boolean
        Private Delegate Sub delegatecheckActualizacion()

#Region "Dispose"
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing Then
                    AbortRefreshThread()
                    If components IsNot Nothing Then components.Dispose()

                    If GridController IsNot Nothing Then GridController = Nothing
                    If TCB IsNot Nothing Then TCB = Nothing
                    If state IsNot Nothing Then
                        state.Dispose()
                        state = Nothing
                    End If

                    If WfRefreshTimer IsNot Nothing Then
                        WfRefreshTimer.Dispose()
                        WfRefreshTimer = Nothing
                    End If
                    If ResultToInsert IsNot Nothing Then
                        ResultToInsert.Dispose()
                        ResultToInsert = Nothing
                    End If
                    If WfTreeView IsNot Nothing Then
                        RemoveHandler WfTreeView.SelectedNodeChanged, AddressOf TreeView1_AfterSelect

                        If WfTreeView.Nodes.Count > 0 Then
                            For Each wfn As WFNodeIdandName In WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes
                                wfn.Nodes.Clear()
                            Next
                            WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes.Clear()
                            WfTreeView.Nodes.Clear()
                        End If

                        WfTreeView.Dispose()
                        WfTreeView = Nothing
                    End If


                    If TSButtonActualizar IsNot Nothing Then
                        TSButtonActualizar.Dispose()
                        TSButtonActualizar = Nothing
                    End If
                    If TSButtonContraer IsNot Nothing Then
                        TSButtonContraer.Dispose()
                        TSButtonContraer = Nothing
                    End If
                    If ToolStripTextBoxSearch IsNot Nothing Then
                        ToolStripTextBoxSearch.Dispose()
                        ToolStripTextBoxSearch = Nothing
                    End If
                    If ToolBarWF IsNot Nothing Then
                        ToolBarWF.Dispose()
                        ToolBarWF = Nothing
                    End If
                End If
                refreshThread = Nothing
                RefreshNodeCountOnExpandThread = Nothing
                MyBase.Dispose(disposing)
            Catch ex As ThreadAbortException
            Catch ex As Exception
            End Try
        End Sub

#End Region

#Region " Código generado por el Diseñador de Windows Forms "
        'UserControl reemplaza a Dispose para limpiar la lista de componentes.


        'Requerido por el Diseñador de Windows Forms
        Private components As System.ComponentModel.IContainer

        'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
        'Puede modificarse utilizando el Diseñador de Windows Forms. 
        'No lo modifique con el editor de código.
        Private WithEvents WfTreeView As Telerik.WinControls.UI.RadTreeView
        Friend WithEvents ToolBarWF As ZToolBar
        Friend WithEvents TSButtonActualizar As System.Windows.Forms.ToolStripButton
        Friend WithEvents TSButtonContraer As System.Windows.Forms.ToolStripButton

        Friend WithEvents ToolStripTextBoxSearch As ToolStripTextBox
        Friend WithEvents TSSBConfActualizar As ToolStripSplitButton
        Friend WithEvents TSMIActualizar As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TSMITiempo As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TSMI1Min As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TSMI5Min As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TSMI10Min As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TSMI30Min As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TSMI60Min As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents TelerikMetroBlueTheme1 As Telerik.WinControls.Themes.TelerikMetroBlueTheme
        Private WithEvents radThemeManager1 As Telerik.WinControls.RadThemeManager
        Friend WithEvents ToolStripButton1 As ToolStripButton
        Friend WithEvents ToolStripButton2 As ToolStripButton
        Friend WithEvents TSButtonInsertar As System.Windows.Forms.ToolStripButton
        <DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.TelerikMetroBlueTheme1 = New Telerik.WinControls.Themes.TelerikMetroBlueTheme()
            Me.radThemeManager1 = New Telerik.WinControls.RadThemeManager()
            Me.WfTreeView = New Telerik.WinControls.UI.RadTreeView()
            Me.ToolBarWF = New Zamba.AppBlock.ZToolBar()
            Me.TSButtonActualizar = New System.Windows.Forms.ToolStripButton()
            Me.TSButtonContraer = New System.Windows.Forms.ToolStripButton()
            Me.TSButtonInsertar = New System.Windows.Forms.ToolStripButton()
            Me.TSSBConfActualizar = New System.Windows.Forms.ToolStripSplitButton()
            Me.TSMIActualizar = New System.Windows.Forms.ToolStripMenuItem()
            Me.TSMITiempo = New System.Windows.Forms.ToolStripMenuItem()
            Me.TSMI1Min = New System.Windows.Forms.ToolStripMenuItem()
            Me.TSMI5Min = New System.Windows.Forms.ToolStripMenuItem()
            Me.TSMI10Min = New System.Windows.Forms.ToolStripMenuItem()
            Me.TSMI30Min = New System.Windows.Forms.ToolStripMenuItem()
            Me.TSMI60Min = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripTextBoxSearch = New System.Windows.Forms.ToolStripTextBox()
            Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
            CType(Me.WfTreeView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ToolBarWF.SuspendLayout()
            Me.SuspendLayout()
            '
            'WfTreeView
            '
            Me.WfTreeView.AllowArbitraryItemHeight = True
            Me.WfTreeView.AllowDrop = True
            Me.WfTreeView.BackColor = System.Drawing.Color.White
            Me.WfTreeView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.WfTreeView.ExpandAnimation = Telerik.WinControls.UI.ExpandAnimation.None
            Me.WfTreeView.HideSelection = False
            Me.WfTreeView.ItemHeight = 22
            Me.WfTreeView.Location = New System.Drawing.Point(0, 29)
            Me.WfTreeView.Margin = New System.Windows.Forms.Padding(0)
            Me.WfTreeView.Name = "WfTreeView"
            Me.WfTreeView.PlusMinusAnimationStep = 1.0R
            '
            '
            '
            Me.WfTreeView.RootElement.AutoSize = False
            Me.WfTreeView.RootElement.ControlBounds = New System.Drawing.Rectangle(0, 29, 150, 250)
            Me.WfTreeView.ShowNodeToolTips = True
            Me.WfTreeView.Size = New System.Drawing.Size(274, 331)
            Me.WfTreeView.TabIndex = 0
            Me.WfTreeView.ThemeName = "TelerikMetroBlue"
            Me.WfTreeView.TreeIndent = 18
            '
            'ToolBarWF
            '
            Me.ToolBarWF.BackColor = System.Drawing.Color.White
            Me.ToolBarWF.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.ToolBarWF.ImageScalingSize = New System.Drawing.Size(22, 22)
            Me.ToolBarWF.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSButtonActualizar, Me.TSButtonContraer, Me.TSButtonInsertar, Me.TSSBConfActualizar, Me.ToolStripTextBoxSearch, Me.ToolStripButton1, Me.ToolStripButton2})
            Me.ToolBarWF.Location = New System.Drawing.Point(0, 0)
            Me.ToolBarWF.Name = "ToolBarWF"
            Me.ToolBarWF.Size = New System.Drawing.Size(274, 29)
            Me.ToolBarWF.TabIndex = 2
            '
            'TSButtonActualizar
            '
            Me.TSButtonActualizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.TSButtonActualizar.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_refresh
            Me.TSButtonActualizar.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.TSButtonActualizar.Name = "TSButtonActualizar"
            Me.TSButtonActualizar.Size = New System.Drawing.Size(26, 26)
            Me.TSButtonActualizar.Tag = "ACTUALIZAR"
            Me.TSButtonActualizar.Text = "Actualizar"
            Me.TSButtonActualizar.ToolTipText = "ACTUALIZAR"
            '
            'TSButtonContraer
            '
            Me.TSButtonContraer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.TSButtonContraer.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_arrow_collapsed
            Me.TSButtonContraer.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.TSButtonContraer.Name = "TSButtonContraer"
            Me.TSButtonContraer.Size = New System.Drawing.Size(26, 26)
            Me.TSButtonContraer.Tag = "CONTRAER"
            Me.TSButtonContraer.Text = "Contraer"
            Me.TSButtonContraer.ToolTipText = "CONTRAER"
            '
            'TSButtonInsertar
            '
            Me.TSButtonInsertar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.TSButtonInsertar.Image = Global.Zamba.Controls.My.Resources.Resources.appbar2
            Me.TSButtonInsertar.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.TSButtonInsertar.Name = "TSButtonInsertar"
            Me.TSButtonInsertar.Size = New System.Drawing.Size(26, 26)
            Me.TSButtonInsertar.Tag = "INSERTAR"
            Me.TSButtonInsertar.Text = "Insertar"
            Me.TSButtonInsertar.ToolTipText = "INSERTAR"
            '
            'TSSBConfActualizar
            '
            Me.TSSBConfActualizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.TSSBConfActualizar.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMIActualizar, Me.TSMITiempo})
            Me.TSSBConfActualizar.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_cog
            Me.TSSBConfActualizar.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.TSSBConfActualizar.Name = "TSSBConfActualizar"
            Me.TSSBConfActualizar.Size = New System.Drawing.Size(38, 26)
            Me.TSSBConfActualizar.ToolTipText = "Activar actualizacion automatica"
            '
            'TSMIActualizar
            '
            Me.TSMIActualizar.CheckOnClick = True
            Me.TSMIActualizar.Name = "TSMIActualizar"
            Me.TSMIActualizar.Size = New System.Drawing.Size(126, 22)
            Me.TSMIActualizar.Text = "Actualizar"
            '
            'TSMITiempo
            '
            Me.TSMITiempo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMI1Min, Me.TSMI5Min, Me.TSMI10Min, Me.TSMI30Min, Me.TSMI60Min})
            Me.TSMITiempo.Name = "TSMITiempo"
            Me.TSMITiempo.Size = New System.Drawing.Size(126, 22)
            Me.TSMITiempo.Text = "Tiempo"
            '
            'TSMI1Min
            '
            Me.TSMI1Min.CheckOnClick = True
            Me.TSMI1Min.Name = "TSMI1Min"
            Me.TSMI1Min.Size = New System.Drawing.Size(133, 22)
            Me.TSMI1Min.Text = "1 minuto"
            '
            'TSMI5Min
            '
            Me.TSMI5Min.CheckOnClick = True
            Me.TSMI5Min.Name = "TSMI5Min"
            Me.TSMI5Min.Size = New System.Drawing.Size(133, 22)
            Me.TSMI5Min.Text = "5 minutos"
            '
            'TSMI10Min
            '
            Me.TSMI10Min.CheckOnClick = True
            Me.TSMI10Min.Name = "TSMI10Min"
            Me.TSMI10Min.Size = New System.Drawing.Size(133, 22)
            Me.TSMI10Min.Text = "10 minutos"
            '
            'TSMI30Min
            '
            Me.TSMI30Min.CheckOnClick = True
            Me.TSMI30Min.Name = "TSMI30Min"
            Me.TSMI30Min.Size = New System.Drawing.Size(133, 22)
            Me.TSMI30Min.Text = "30 minutos"
            '
            'TSMI60Min
            '
            Me.TSMI60Min.CheckOnClick = True
            Me.TSMI60Min.Name = "TSMI60Min"
            Me.TSMI60Min.Size = New System.Drawing.Size(133, 22)
            Me.TSMI60Min.Text = "1 hora"
            '
            'ToolStripTextBoxSearch
            '
            Me.ToolStripTextBoxSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.ToolStripTextBoxSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList
            Me.ToolStripTextBoxSearch.BackColor = System.Drawing.Color.White
            Me.ToolStripTextBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ToolStripTextBoxSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
            Me.ToolStripTextBoxSearch.Name = "ToolStripTextBoxSearch"
            Me.ToolStripTextBoxSearch.Size = New System.Drawing.Size(110, 29)
            '
            'ToolStripButton1
            '
            Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ToolStripButton1.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_text_size_up
            Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ToolStripButton1.Name = "ToolStripButton1"
            Me.ToolStripButton1.Size = New System.Drawing.Size(26, 26)
            Me.ToolStripButton1.Text = "Aumentar Fuente"
            '
            'ToolStripButton2
            '
            Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.ToolStripButton2.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_text_size_down
            Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ToolStripButton2.Name = "ToolStripButton2"
            Me.ToolStripButton2.Size = New System.Drawing.Size(26, 26)
            Me.ToolStripButton2.Text = "Disminuir Fuente"
            '
            'UCWFTreeControlForClient
            '
            Me.BackColor = System.Drawing.Color.White
            Me.Controls.Add(Me.WfTreeView)
            Me.Controls.Add(Me.ToolBarWF)
            Me.Name = "UCWFTreeControlForClient"
            Me.Size = New System.Drawing.Size(274, 360)
            CType(Me.WfTreeView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ToolBarWF.ResumeLayout(False)
            Me.ToolBarWF.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

#End Region

        Private Const CM_REMOVE_ALL = "Quitar Todas las Busquedas"
        Private Const CM_REMOVE_SELECTED_NODE = "Quitar busqueda"

#Region "Atributos"
        Dim searchNode As New SearchNode("Busqueda")
        Dim insertNode As New InsertNode("Insercion")
        Dim insertedNode As New InsertNode("Insertados")
        Dim insertingNode As New InsertNode("A Insertar")

        Private _FromSearch As Boolean
        Public Property ComeFromSearch() As Boolean
            Get
                Return _FromSearch
            End Get
            Set(ByVal value As Boolean)
                _FromSearch = value
            End Set
        End Property

        Private Property Search As ISearch
        Private Property tiempoActualizacion As Long
        Private Const NODE_PROCESSES_NUMBER As Integer = 2

        Private PanelsController As WF.UInterface.Client.UCTasksPanel
        Private GridController As IGrid

        Private TCB As New Threading.TimerCallback(AddressOf InvokeRefreshWorkflow)
        Private state As Object
        Private WfRefreshTimer As ZTimer

        Private SelectedStepId As Int64
        Private Delegate Sub DRefreshWorkflow(ByVal oldStepID As Long, ByVal newStepID As Long, ByVal IsFirstTimeLoaded As Boolean)
        Private ResultToInsert As ZambaCore
        Private showTaskGrid As Boolean
        Private LastWFSelect As Int64
        Private refreshThread As Thread = Nothing
        Private RefreshNodeCountOnExpandThread As Thread = Nothing


        'Private refreshThreadStart As New ThreadStart(AddressOf RefreshStepCount)
        Private Delegate Sub DUpdateWfAndSteps(ByVal workflows As KeyValuePair(Of Int64, Int64), ByVal steps As Dictionary(Of Int64, Int64))
#End Region

#Region "Contructores y Load"
        Public Sub New(ByVal IL As Zamba.AppBlock.ZIconsList, ByRef ucTasksGrid As IGrid, Panel As WF.UInterface.Client.UCTasksPanel, ByVal fromSearch As Boolean)
            MyBase.New()
            InitializeComponent()
            PanelsController = Panel
            ToolStripTextBoxSearch.Visible = False
            GridController = ucTasksGrid
            FastCountEnabled = UserPreferences.getValue("FastCountEnabled", UPSections.UserPreferences, False)
            Try
                ' Se cargan los iconos predeterminados 
                ' para cada nodo de la vista de arbol...
                'Me.WfTreeView.ImageList = IL.ZIconList

                LoadWorkflowTree(fromSearch)

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Load del control
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' <history>                             
        '''     [Javier]    13/10/2010  Created     Se realiza llamada al método que actualiza el count de los steps
        '''                                         con restricciones para documentos, se llama de aquí ya que en el new el me.invoke falla
        ''' </history>
        Private Sub WFPanelResults_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            Dim searchNode As StepNodeIdAndName = GetStepNodeIdAndName(SelectedStepId)
            RemoveHandler WfTreeView.SelectedNodeChanged, AddressOf TreeView1_AfterSelect
            RemoveHandler WfTreeView.NodeExpandedChanged, AddressOf WfTreeView_AfterExpand

            AddHandler WfTreeView.TreeViewElement.NodeFormatting, AddressOf TreeViewElement_NodeFormatting

            WfTreeView.SelectedNode = searchNode
            'RemoveHandler WfTreeView.NodeMouseClick, AddressOf TreeView1_TreeViewNodeClick
            'AddHandler WfTreeView.NodeMouseClick, AddressOf TreeView1_TreeViewNodeClick

            RefreshWorkflow(-1, -1, True)
            AddHandler WfTreeView.SelectedNodeChanged, AddressOf TreeView1_AfterSelect
            AddHandler WfTreeView.NodeExpandedChanged, AddressOf WfTreeView_AfterExpand

            ' ivan 25/01/16 - Trae si inicia actualizacion automatica y tiempo de refresh.
            TSMIActualizar.Checked = Boolean.Parse(UserPreferences.getValue("RefreshWFClient", UPSections.WorkFlow, True))
            tiempoActualizacion = Int32.Parse(UserPreferences.getValue("RefreshWFClientTimer", UPSections.WorkFlow, 30))

        End Sub

#End Region

#Region "Public Methods"

        Public Sub LoadDynamicButtons()

            ToolBarWF.SuspendLayout()

            'Elimina las acciones anteriores
            ToolBarWF.Items.Clear()
            ToolBarWF.Items.AddRange(New ToolStripItem() {TSButtonActualizar, TSButtonContraer, TSButtonInsertar, ToolStripTextBoxSearch, TSSBConfActualizar, ToolStripButton1, ToolStripButton2})

            'Se cargan los botones dinámicos en la barra de workflow
            GenericRuleManager.LoadDynamicButtons(ToolBarWF, ToolBarWF.Items.IndexOf(TSButtonInsertar) + 1, False, ButtonPlace.ArbolTareas, Nothing, ButtonType.Rule, SelectedWfId)

            ToolBarWF.ResumeLayout(True)

        End Sub

        Private Sub SelectNode(ByVal node As BaseWFNode, Optional ByVal cancelEvents As Boolean = True)
            If cancelEvents Then
                RemoveHandler WfTreeView.SelectedNodeChanged, AddressOf TreeView1_AfterSelect
                WfTreeView.SelectedNode = node
                AddHandler WfTreeView.SelectedNodeChanged, AddressOf TreeView1_AfterSelect
            Else
                WfTreeView.SelectedNode = node
            End If
        End Sub

        ''' <summary>
        ''' Selecciona el id especificado
        ''' </summary>
        ''' <param name="wfstepID"></param>
        ''' <remarks></remarks>
        Public Sub SelectStep(ByVal wfStepId As Int64, ByVal refreshFilters As Boolean, ByVal showList As Boolean, ByVal showTaskGrid As Boolean)
            Try
                Dim searchNode As StepNodeIdAndName = GetStepNodeIdAndName(wfStepId)
                If Not IsNothing(searchNode) Then

                    If Not TypeOf (WfTreeView.SelectedNode) Is SearchNodeIdAndName Then
                        PanelsController.ShowController(wfStepId, refreshFilters, showList, 0, showTaskGrid, False)
                    Else
                        PanelsController.ShowController(wfStepId, refreshFilters, showList, 0, False, False)
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Selecciona el nodo del id
        ''' </summary>
        ''' <param name="wfId"></param>
        ''' <remarks></remarks>
        Public Sub SelectWf(ByVal wfId As Int64)
            Try
                Dim searchNode As WFNodeIdandName = GetWfNodeIdAndName(wfId)
                If Not IsNothing(searchNode) Then
                    RemoveHandler WfTreeView.SelectedNodeChanged, AddressOf TreeView1_AfterSelect
                    WfTreeView.SelectedNode = searchNode
                    AddHandler WfTreeView.SelectedNodeChanged, AddressOf TreeView1_AfterSelect
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Public Sub SelectInicio()
            Try
                RemoveHandler WfTreeView.SelectedNodeChanged, AddressOf TreeView1_AfterSelect
                WfTreeView.SelectedNode = WfTreeView.Nodes(NODE_PROCESSES_NUMBER)
                AddHandler WfTreeView.SelectedNodeChanged, AddressOf TreeView1_AfterSelect
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Devuelve el ID del WF seleccionado
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property SelectedWfId() As Int64
            Get
                Try
                    If Not TypeOf (WfTreeView.SelectedNode) Is SearchNodeIdAndName AndAlso Not TypeOf (WfTreeView.SelectedNode) Is InsertNodeIdAndName AndAlso Not TypeOf (WfTreeView.SelectedNode) Is InsertNode AndAlso Not TypeOf (WfTreeView.SelectedNode) Is SearchNode Then

                        If Not IsNothing(WfTreeView.SelectedNode) Then
                            If TypeOf (WfTreeView.SelectedNode) Is WFNodeIdandName Then
                                Return DirectCast(WfTreeView.SelectedNode, WFNodeIdandName).WorkFlowId
                            Else
                                If Not IsNothing(WfTreeView.SelectedNode.Parent) Then
                                    If TypeOf WfTreeView.SelectedNode Is StepStateNodeIdAndName Then

                                        Return DirectCast(WfTreeView.SelectedNode.Parent.Parent, WFNodeIdandName).WorkFlowId
                                    Else
                                        Return DirectCast(WfTreeView.SelectedNode.Parent, WFNodeIdandName).WorkFlowId
                                    End If
                                Else
                                    Return 0
                                End If
                            End If
                        Else
                            Return 0
                        End If

                    End If

                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    Return 0
                End Try
            End Get
        End Property

        Public Property RefreshEnabled As Boolean = True
        Public Property FastCountEnabled As Boolean = False

        Friend Sub RefreshSearchNode(count As Integer, Search As Search)
            Try
                Dim searchNode As SearchNodeIdAndName = DirectCast(WfTreeView.SelectedNode, SearchNodeIdAndName)
                searchNode.UpdateTasksCount(count)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#End Region

        ''' <summary>
        ''' Devuelve el nodo del workflow seleccionado
        ''' </summary>
        ''' <param name="wfId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetWfNodeIdAndName(ByVal wfId As Int64) As WFNodeIdandName
            If WfTreeView IsNot Nothing Then
                For Each n As RadTreeNode In WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes
                    If DirectCast(n, WFNodeIdandName).WorkFlowId = wfId Then
                        Return DirectCast(n, WFNodeIdandName)
                    End If
                Next
            End If
            Return Nothing
        End Function
        Dim Refreshing As Boolean

        Private Sub InvokeRefreshWorkflow()
            Try
                If Not IsNothing(Me) Then
                    If Refreshing = False Then
                        Refreshing = True
                        WfRefreshTimer.Pause()
                        Invoke(New DRefreshWorkflow(AddressOf RefreshWorkflow), New Object() {-2, -2, False})
                    End If

                End If

            Catch ex As InvalidOperationException
                'Comento esta excepcion - Marcelo
                'raiseerror(ex)
            Catch ex As Exception
            Finally
                Refreshing = False
                WfRefreshTimer.Resume()
            End Try
        End Sub

        Public Sub SetLastStepId(ByVal stepId As Int64)
            SelectedStepId = stepId
        End Sub

        Private Sub LoadWorkflowTree(ByVal fromSearch As Boolean)
            Dim workflows As List(Of EntityView) = Nothing
            Dim rootWFNode As InitNode = Nothing
            Dim lastWfUsed As String
            Dim lastWfStepUsed As String

            WfTreeView.BeginUpdate()
            WfTreeView.Font = New Font("Verdana", 8, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            If (currentFont Is Nothing) Then
                currentFont = WfTreeView.Font
                Dim size As String = UserPreferences.getValue("TreeFontSize", UPSections.UserPreferences, currentFont.Size.ToString())
                If Not String.IsNullOrEmpty(size) AndAlso size.Contains(",") Then
                    size = size.Replace(",", ".")
                End If
                currentFontSize = Integer.Parse(size, Globalization.NumberStyles.Integer, Globalization.CultureInfo.InvariantCulture)
                currentFont = New Font(currentFont.FontFamily, currentFontSize, currentFont.Style)
                currentRowHeight = currentFontSize * 3
            End If

            WfTreeView.ShowLines = False
            WfTreeView.ItemHeight = currentRowHeight
            'ivan 25/01/16 - agrega nodo para las tareas de la busqueda
            WfTreeView.Nodes.Add(searchNode)
            searchNode.ForeColor = Color.FromArgb(0, 157, 224)
            searchNode.Font = New Font("Verdana", currentFontSize, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
            'ivan 29/07/16 - agrega nodo para las inserciones
            WfTreeView.Nodes.Add(insertNode)
            insertNode.ForeColor = Color.FromArgb(0, 157, 224)
            insertNode.Font = New Font("Verdana", currentFontSize, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))

            insertingNode.ImageIndex = 999
            insertedNode.ImageIndex = 999

            insertNode.Nodes.Add(insertingNode)
            insertNode.Nodes.Add(insertedNode)

            Try
                'Obtiene los workflows
                workflows = WFBusiness.GetUserWFIdsAndNamesWithSteps(Membership.MembershipHelper.CurrentUser.ID)

                'Agrega el nodo padre de Procesos
                rootWFNode = New InitNode()
                rootWFNode.Text = WFPanelResults_Procesos
                rootWFNode.ForeColor = Color.FromArgb(0, 157, 224)
                rootWFNode.Font = New Font("Verdana", currentFontSize, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))

                WfTreeView.Nodes.Add(rootWFNode)

                If Not IsNothing(workflows) Then
                    'Recorre los workflows y los agrega
                    For Each wf As EntityView In workflows
                        Dim wfNode As New WFNodeIdandName(wf.ID, wf.Name)
                        wfNode.ForeColor = Color.FromArgb(70, 70, 70)
                        wfNode.Font = New Font("Verdana", currentFontSize, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
                        wfNode.ImageIndex = 999

                        rootWFNode.Nodes.Add(wfNode)
                        'Agrega las etapas
                        For Each wfStepNode As EntityView In wf.ChildsEntities
                            Dim stepNode As New StepNodeIdAndName(wfStepNode.ID, wfStepNode.Name)
                            stepNode.ForeColor = Color.FromArgb(76, 76, 76)
                            stepNode.Font = New Font("Verdana", currentFontSize, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))

                            stepNode.ImageIndex = 999
                            wfNode.Nodes.Add(stepNode)

                            'Agrega los estados
                            If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.ShowStates, wfStepNode.ID) Then
                                Dim states As List(Of IWFStepState) = WFStepStatesComponent.GetStepStatesByStepId(wfStepNode.ID, False)

                                For Each st As IWFStepState In states
                                    Dim StateNode As New StepStateNodeIdAndName(wfStepNode.ID, st.ID, st.Name)
                                    stepNode.Nodes.Add(StateNode)
                                Next
                            End If
                        Next
                    Next
                End If

                Try
                    If Not fromSearch Then
                        rootWFNode.Expand()
                        ' se para en ultimo WFUtilizado
                        lastWfUsed = UserPreferences.getValue("UltimoWFUtilizado", UPSections.WorkFlow, String.Empty)

                        If Not String.IsNullOrEmpty(lastWfUsed) AndAlso IsNumeric(lastWfUsed) Then
                            Dim wfNode As WFNodeIdandName = GetWfNodeIdAndName(lastWfUsed)
                            If wfNode IsNot Nothing Then wfNode.Expand()
                        End If
                        lastWfStepUsed = UserPreferences.getValue("UltimoWFStepUtilizado", UPSections.WorkFlow, String.Empty)
                        If IsNumeric(lastWfStepUsed) Then
                            SelectedStepId = CLng(lastWfStepUsed)
                        End If
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                WfTreeView.EndUpdate()
                workflows = Nothing
                rootWFNode = Nothing
                lastWfUsed = Nothing
                lastWfStepUsed = Nothing
            End Try
        End Sub

        Public Sub AddSearchNode(ByVal Search As ISearch, ByVal taskCount As Integer)
            Dim SearchNodeIdAndName As New SearchNodeIdAndName(Search, taskCount)
            SearchNodeIdAndName.SqlSearch = Search.SQL(0)
            SearchNodeIdAndName.SqlSearchCount = Search.SQLCount(0)
            searchNode.Nodes.Add(SearchNodeIdAndName)

            SelectNode(SearchNodeIdAndName)
        End Sub

        Public Sub AddAndSelectInsertFormNode(ByVal result As IResult)
            SelectNode(AddInsertFormNode(result))
        End Sub
        Public Sub AddAndSelectInsertedFormNode(ByVal result As IResult)
            SelectNode(AddInsertedFormNode(result))
        End Sub

        Private Function AddInsertFormNode(ByVal result As IResult) As BaseWFNode
            result.TempId = insertingNode.Nodes.Count + 1
            Dim insertNodeIdAndName As New InsertNodeIdAndName(result)
            insertNode.Nodes(insertingNode.Name).Nodes.Add(insertNodeIdAndName)
            Return insertNodeIdAndName
        End Function

        Private Function AddInsertedFormNode(ByVal result As IResult) As BaseWFNode
            'result.TempId = insertingNode.Nodes.Count + 1
            Dim insertNodeIdAndName As New InsertNodeIdAndName(result)
            insertedNode.Nodes.Add(insertNodeIdAndName)
            Return insertNodeIdAndName
        End Function
        Private Sub AddInsertedNode(ByVal nodo As BaseWFNode)
            If insertedNode IsNot Nothing AndAlso insertNode.Nodes.Contains(insertedNode) Then
                insertedNode.Nodes.Add(nodo)
            End If
        End Sub
        Private Sub TreeViewElement_NodeFormatting(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.TreeNodeFormattingEventArgs)
            Dim Node As IBaseWFNode = DirectCast(args.Node, IBaseWFNode)
            Dim NodeType As NodeWFTypes = Node.NodeWFType
            Dim NodeHelper As New WFNodeHelper
            Dim currentImage As Image = NodeHelper.GetnodeImage(NodeType)

            args.NodeElement.ImageElement.Image = currentImage
            args.NodeElement.ClipDrawing = False
            args.NodeElement.ImageElement.FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentBounds
            args.NodeElement.ContentElement.DisableHTMLRendering = True
            args.NodeElement.ImageElement.StretchHorizontally = False
            args.NodeElement.ImageElement.ImageLayout = ImageLayout.Stretch
            args.NodeElement.ImageElement.MinSize = New Size(20, 20)

            If NodeType = NodeWFTypes.nodoBusqueda Then
                args.NodeElement.ContentElement.ForeColor = Zamba.AppBlock.ZambaUIHelpers.GetTitlesColor
            ElseIf NodeType = NodeWFTypes.nodoInsercion Then
                args.NodeElement.ContentElement.ForeColor = Zamba.AppBlock.ZambaUIHelpers.GetTitlesColor
            Else
                args.NodeElement.ContentElement.ForeColor = Color.Black
            End If
            args.NodeElement.BackColor = Color.White
            args.NodeElement.BorderColor = Color.White


            If args.NodeElement.IsSelected Then
                args.NodeElement.BorderColor = Color.DarkOrange
                args.NodeElement.BorderBoxStyle = Telerik.WinControls.BorderBoxStyle.SingleBorder
                args.NodeElement.BorderGradientStyle = Telerik.WinControls.GradientStyles.Solid
                args.NodeElement.BackColor = Zamba.AppBlock.ZambaUIHelpers.GetTitlesColor
                args.NodeElement.ContentElement.ForeColor = Color.White
            End If
        End Sub


        Friend Sub RemoveInsertNode(tempId As Integer)

            If insertingNode IsNot Nothing AndAlso insertingNode.Nodes.Count > 0 Then
                For Each nodo As InsertNodeIdAndName In insertingNode.Nodes
                    If nodo.Result.TempId = tempId Then
                        insertingNode.Nodes.Remove(nodo)
                        If DirectCast(nodo.Result, NewResult).State = States.Insertado Then
                            AddInsertedNode(nodo)
                        End If
                    End If
                Next
            End If

        End Sub

        'Con este evento manejo cuando se hace click derecho sobre algun nodo de insercion o busqueda, para eliminar nodos.
        Private Sub TreeView1_TreeViewNodeClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles WfTreeView.MouseDown
            Try
                If e.Button = MouseButtons.Right Then
                    If WfTreeView.SelectedNode Is insertedNode OrElse WfTreeView.SelectedNode Is searchNode Then
                        If WfTreeView.SelectedNode.ContextMenu Is Nothing Then
                            WfTreeView.SelectedNode.ContextMenu = New RadContextMenu
                        End If
                        Dim menuItem As RadMenuItem
                        For Each item As RadMenuItem In WfTreeView.SelectedNode.ContextMenu.Items
                            If item.Text = CM_REMOVE_ALL Then
                                menuItem = item
                            End If
                            If item.Text = CM_REMOVE_SELECTED_NODE Then
                                item.Enabled = False
                            End If
                        Next
                        If menuItem Is Nothing Then
                            menuItem = New RadMenuItem(CM_REMOVE_ALL)
                            WfTreeView.SelectedNode.ContextMenu.Items.Add(menuItem)
                            RemoveHandler menuItem.Click, AddressOf cmMenuItemAll_Click
                            AddHandler menuItem.Click, AddressOf cmMenuItemAll_Click
                        End If
                        menuItem.Enabled = If(WfTreeView.SelectedNode.GetNodeCount(True) > 0, True, False)
                    ElseIf TypeOf WfTreeView.SelectedNode Is SearchNodeIdAndName OrElse (TypeOf WfTreeView.SelectedNode Is InsertNodeIdAndName AndAlso WfTreeView.SelectedNode.Parent Is insertedNode) Then
                        SelectNode(WfTreeView.SelectedNode)
                        Dim menuItem As RadMenuItem
                        If WfTreeView.SelectedNode.ContextMenu Is Nothing Then
                            WfTreeView.SelectedNode.ContextMenu = New RadContextMenu
                        End If
                        For Each item As RadMenuItem In WfTreeView.SelectedNode.ContextMenu.Items
                            If item.Text = CM_REMOVE_SELECTED_NODE Then
                                menuItem = item
                            End If
                            If item.Text = CM_REMOVE_ALL Then
                                item.Enabled = False
                            End If
                        Next
                        If menuItem Is Nothing Then
                            menuItem = New RadMenuItem(CM_REMOVE_SELECTED_NODE)
                            WfTreeView.SelectedNode.ContextMenu.Items.Add(menuItem)
                            RemoveHandler menuItem.Click, AddressOf cmMenuItem_Click
                            AddHandler menuItem.Click, AddressOf cmMenuItem_Click
                        End If
                        menuItem.Enabled = True
                    End If
                    'ElseIf TypeOf WfTreeView.SelectedNode Is InsertNodeIdAndName Then
                    '    TreeViewSelect(False, False)
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub cmMenuItemAll_Click(sender As Object, e As EventArgs)
            RemoveNode(True)
        End Sub

        Private Sub cmMenuItem_Click(sender As Object, e As EventArgs)
            RemoveNode(False)
        End Sub

        Private Sub RemoveNode(ByVal removeAllNodes As Boolean)
            If removeAllNodes Then
                WfTreeView.SelectedNode.Nodes.Clear()
            Else
                Dim node As BaseWFNode = WfTreeView.SelectedNode
                RemoveHandler WfTreeView.SelectedNodeChanged, AddressOf TreeView1_AfterSelect
                node.Remove()
                'WfTreeView.Nodes.Remove(node)
                AddHandler WfTreeView.SelectedNodeChanged, AddressOf TreeView1_AfterSelect
            End If

            'Vacio la grilla
            PanelsController.ClearGrid()
        End Sub

        Private SyncObject As New Object
        ''' <summary>
        ''' Proceso que refrezca el árbol de tareas asincrónicamente
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub RefreshWorkflow(ByVal oldStepID As Long, ByVal newStepID As Long, ByVal IsFirstTimeLoaded As Boolean)
            If IsHandleCreated AndAlso WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes.Count > 0 Then

                Dim selectedNode As IBaseWFNode = WfTreeView.SelectedNode

                If Not selectedNode Is Nothing AndAlso TypeOf selectedNode Is SearchNodeIdAndName AndAlso newStepID <> -2 Then
                    TreeViewSelect(False, True)
                End If

                'agrego los parametros para actualizar
                Dim params As New ArrayList()
                params.Add(oldStepID)
                params.Add(newStepID)
                params.Add(IsFirstTimeLoaded)

                If newStepID = -2 Then
                    SelectStep(SelectedStepId, False, False, False)
                Else
                    SelectStep(SelectedStepId, False, False, True)
                End If

                refreshThread = New Thread(AddressOf RefreshStepCount)
                refreshThread.IsBackground = True
                refreshThread.Name = "ThreadUpdateStepsCount"
                refreshThread.Start(params)

                Try
                    WfTreeView.Font = currentFont
                    WfTreeView.ItemHeight = currentRowHeight
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        End Sub

        Private Sub AbortRefreshThread()
            'Si el thread se encuentra vivo lo cancelo
            RefreshEnabled = False
            If refreshThread IsNot Nothing AndAlso refreshThread.IsAlive Then
                Try
                    SyncObject = New Object
                    refreshThread.Abort()
                Catch ex As ThreadAbortException
                Catch ex As Exception
                    'No debería ocurrir, pero por las dudas se pone.
                    ZClass.raiseerror(ex)
                End Try
            End If
        End Sub

        Private Sub AbortRefreshNodeCountOnExpandThread()
            'Si el thread se encuentra vivo lo cancelo
            If RefreshNodeCountOnExpandThread IsNot Nothing AndAlso RefreshNodeCountOnExpandThread.IsAlive Then
                Try
                    RefreshNodeCountOnExpandThread.Abort()
                Catch ex As ThreadAbortException
                Catch ex As Exception
                    'No debería ocurrir, pero por las dudas se pone.
                    ZClass.raiseerror(ex)
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Devuelve el nodo al que pertenece el id
        ''' </summary>
        ''' <param name="wfstep"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetStepNodeIdAndName(ByVal wfstepId As Int64) As StepNodeIdAndName
            Try
                For Each wn As RadTreeNode In WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes
                    For Each n As RadTreeNode In wn.Nodes
                        If DirectCast(n, StepNodeIdAndName).WFStepid = wfstepId Then
                            Return DirectCast(n, StepNodeIdAndName)
                        End If
                    Next
                Next
                Return Nothing
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return Nothing
            End Try
        End Function

        Private Sub CollapseStepsNodes()
            For Each parentNode As RadTreeNode In WfTreeView.Nodes
                If Not TypeOf parentNode Is SearchNode AndAlso Not TypeOf parentNode Is InsertNode Then
                    For Each childNode As RadTreeNode In parentNode.Nodes
                        childNode.Collapse()
                    Next
                Else
                    parentNode.Collapse()
                End If
            Next
        End Sub

        Private Sub Insertar()

            Try
                Dim frm As New VirtualDocumentSelector
                frm.ShowDialog()

                Select Case frm.DialogResult

                    Case DialogResult.OK, DialogResult.Yes
                        'todo: Hay que evaluar cuando haya una tarea que pertenezca a mas de una etapa del mismo u otro workflow, este metodo no serviria, hay que especificar el stepid del cual se quiere la tarea. Martin
                        Dim Result As NewResult = frm.GetInsertedDocument()

                        InsertVirtualResult(Result)

                    Case Else
                        Exit Sub
                End Select
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub InsertVirtualResult(ByRef Result As NewResult)

            If Not IsNothing(Result) Then

                Dim insertresult As InsertResult
                insertresult = IndexDocument(Result, True)
                If insertresult = InsertResult.Insertado Then
                    Zamba.Core.Result.HandleModule(ResultActions.ShowNewForm, Result, Nothing)
                End If
            End If

        End Sub

        Private Function IndexDocument(ByRef NewResult As NewResult, ByVal DisableAutomaticVersion As Boolean) As InsertResult
            Dim InsertResult As InsertResult = InsertResult.NoInsertado

            If Not IsNothing(NewResult) Then


                Try
                    '[ecanete]8-9-2009- Se comenta y se implementa la funcionalidad de autocomplete en el método insert en el
                    'branch branch existente
                    Dim dsIndexsToIncrement As DataSet = DocTypesBusiness.GetIndexsProperties(NewResult.DocType.ID)
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
                                        Dim Defaultvalue As String = CurrentRow("DefaultValue").ToString.Trim
                                        Defaultvalue = WFRuleParent.ReconocerVariablesValuesSoloTexto(Defaultvalue)
                                        Defaultvalue = TextoInteligente.ReconocerCodigo(Defaultvalue, Nothing)
                                        CurrentIndex.Data = Defaultvalue
                                        CurrentIndex.DataTemp = Defaultvalue
                                    End If

                                End If
                            Next
                        End If
                    Next

                    ResultToInsert = NewResult
                    'Para cuando inserto un formVirtual, que vaya de una a la solapa de insertado
                    InsertResult = InsertResult.Insertado

                Catch ex As Exception
                    InsertResult = InsertResult.NoInsertado
                    ZClass.raiseerror(ex)
                    MessageBox.Show(ex.Message, "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            End If

            Return InsertResult
        End Function

        Private Sub WfTreeView_AfterExpand(sender As Object, e As RadTreeViewEventArgs)

            Try
                Dim baseWfNode As BaseWFNode = DirectCast(e.Node, BaseWFNode)

                AbortRefreshNodeCountOnExpandThread()
                Dim params As New ArrayList()
                params.Add(baseWfNode)

                'Ejecuto nuevamente el refresco
                RefreshNodeCountOnExpandThread = New Threading.Thread(AddressOf RefreshNodeCountOnExpand)
                RefreshNodeCountOnExpandThread.IsBackground = True
                RefreshNodeCountOnExpandThread.Name = "RefreshNodeCountOnExpand"
                RefreshNodeCountOnExpandThread.Start(params)

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Sub

        Private Sub RefreshNodeCountOnExpand(baseWfNodeparam As Object)
            Try
                Dim baseWfNode As BaseWFNode = baseWfNodeparam(0)
                Dim steps As New Dictionary(Of Int64, Int64)
                Dim workflows As New KeyValuePair(Of Int64, Int64)(0, 0)

                Select Case baseWfNode.NodeWFType
                    Case NodeWFTypes.WorkFlow
                        Dim wtbExt As New WfTaskBussinesExt()
                        Dim workflowNode As WFNodeIdandName = DirectCast(baseWfNode, WFNodeIdandName)
                        If IsNodeVisible(workflowNode) Then
                            Dim stepid As Int64
                            For j As Int32 = 0 To workflowNode.Nodes.Count - 1
                                Dim StepNode As StepNodeIdAndName = DirectCast(workflowNode.Nodes(j), StepNodeIdAndName)
                                stepid = StepNode.WFStepid
                                'obtengo el count de tareas de la etapa
                                If IsNodeVisible(StepNode) Then
                                    Dim stepCount As Int64 = wtbExt.GetTaskCount(StepNode.WFStepid, True, Membership.MembershipHelper.CurrentUser.ID)
                                    steps.Add(StepNode.WFStepid, stepCount)
                                End If
                            Next

                            workflows = New KeyValuePair(Of Int64, Int64)(workflowNode.WorkFlowId, 0)
                        End If

                        'Se actualiza el arbol de workflows y etapas con sus conteos de tareas
                        Invoke(New DUpdateWfAndSteps(AddressOf UpdateWfAndNodesCount), New Object() {workflows, steps})
                End Select
            Catch ex As InvalidOperationException
            Catch ex As SynchronizationLockException
            Catch ex As ThreadAbortException
            Catch ex As ThreadInterruptedException
            Catch ex As ThreadStateException
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Function IsNodeVisible(ByVal node As RadTreeNode) As Boolean
            If InvokeRequired Then
                Return Invoke(New IsNodeVisibleDelegate(AddressOf IsNodeVisible), New Object() {node})
            Else
                Return node.Visible
            End If
        End Function

        Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As RadTreeViewEventArgs) Handles WfTreeView.SelectedNodeChanged
            Try
                'Verifica si se ejecuto el evento por workflow o al hacer click
                If e.Action = TreeViewAction.Unknown Then
                    showTaskGrid = False
                Else
                    showTaskGrid = True
                End If

                If SelectedStepId = 0 Then
                    TreeViewSelect(True, showTaskGrid)
                Else
                    TreeViewSelect(False, showTaskGrid)
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub TreeViewSelect(ByVal refreshFilters As Boolean, ByVal showGrid As Boolean)

            Try
                If Not IsNothing(WfTreeView.SelectedNode) Then

                    Dim WFchanged As Boolean
                    Dim TempLastWFSelect As Object = If(Not LastWFSelect > 0, UserPreferences.getValue("UltimoWFUtilizado", UPSections.WorkFlow, String.Empty), LastWFSelect)

                    If IsDBNull(TempLastWFSelect) OrElse String.IsNullOrEmpty(TempLastWFSelect) Then
                        TempLastWFSelect = 0
                    End If

                    Dim baseWfNode As BaseWFNode = DirectCast(WfTreeView.SelectedNode, BaseWFNode)
                    Select Case baseWfNode.NodeWFType
                        Case NodeWFTypes.Inicio
                            'RaiseEvent InicioSelect()
                        Case NodeWFTypes.nodoBusqueda

                        Case NodeWFTypes.WorkFlow
                            'LastWFSelect = DirectCast(baseWfNode, WFNodeIdandName).WorkFlowId
                            'WFchanged = TempLastWFSelect <> LastWFSelect
                            'UserPreferences.setValue("UltimoWFUtilizado", LastWFSelect, Sections.WorkFlow)
                            If baseWfNode.Nodes.Count > 0 Then
                                '  WfTreeView.SelectedNode = baseWfNode.Nodes(0)
                            End If
                            If baseWfNode.Expanded Then
                                '                                baseWfNode.Expand()
                            End If
                        Case NodeWFTypes.Etapa, NodeWFTypes.Estado
                            If TypeOf baseWfNode Is StepStateNodeIdAndName Then
                                LastWFSelect = DirectCast(baseWfNode.Parent.Parent, WFNodeIdandName).WorkFlowId
                                WFchanged = TempLastWFSelect <> LastWFSelect
                                SelectedStepId = DirectCast(baseWfNode, StepStateNodeIdAndName).WFStepid
                                UserPreferences.setValue("UltimoWFStepUtilizado", SelectedStepId, UPSections.WorkFlow)
                                UserPreferences.setValue("UltimoWFUtilizado", LastWFSelect, UPSections.WorkFlow)
                                PanelsController.ShowController(SelectedStepId, refreshFilters, showGrid, DirectCast(baseWfNode, StepStateNodeIdAndName).StateId, True, WFchanged)
                            Else
                                LastWFSelect = DirectCast(baseWfNode.Parent, WFNodeIdandName).WorkFlowId
                                WFchanged = TempLastWFSelect <> LastWFSelect
                                SelectedStepId = DirectCast(baseWfNode, StepNodeIdAndName).WFStepid
                                UserPreferences.setValue("UltimoWFStepUtilizado", SelectedStepId, UPSections.WorkFlow)
                                UserPreferences.setValue("UltimoWFUtilizado", LastWFSelect, UPSections.WorkFlow)
                                PanelsController.ShowController(SelectedStepId, refreshFilters, showGrid, 0, True, WFchanged)
                            End If
                            searchNode.Collapse()
                            insertedNode.Collapse()
                        Case NodeWFTypes.Busqueda
                            If PanelsController.MainFormTabControl.SelectedTab.Name.Equals("TabTareas") OrElse ComeFromSearch Then
                                Dim searchNode As SearchNodeIdAndName = DirectCast(WfTreeView.SelectedNode, SearchNodeIdAndName)
                                PanelsController.ShowController(searchNode.Search, searchNode.SqlSearch, searchNode.SqlSearchCount)
                            End If
                        Case NodeWFTypes.insercion
                            If PanelsController.MainFormTabControl.SelectedTab.Name.Equals("TabTareas") Then
                                Dim result As Result = DirectCast(WfTreeView.SelectedNode, InsertNodeIdAndName).Result
                                If TypeOf result Is TaskResult Then
                                    PanelsController.ShowInsertedResult(result, String.Empty)
                                Else
                                    If DirectCast(result, NewResult).State = States.Insertado Then
                                        'Abrir Tarea.
                                        PanelsController.OpenInsertedTask(result.ID)
                                    Else
                                        PanelsController.ShowInsertedResult(result, String.Empty)
                                    End If
                                End If
                            End If

                    End Select
                    baseWfNode = Nothing


                End If
            Catch ex As Exception

                Zamba.Core.ZClass.raiseerror(ex)
                'TODO: ML Limpiar Grilla ante error.
            End Try
        End Sub

        ''' <summary>
        ''' Devuelve el id del ultimo workflow seleccionado
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getLastWfID() As Int64
            Return LastWFSelect
        End Function

        Public Sub RefreshTaskGridAfterGenerateTask()
            PanelsController.RefreshWFs(-1, -1)
        End Sub

        ''' <summary>
        '''     Maneja los eventos de los botones de la toolbar
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' <history>   [Javier] 02/08/2010 Created</history>
        Private Sub TSButtons_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonActualizar.Click, TSButtonContraer.Click, TSButtonInsertar.Click
            Try
                Dim TSButton As ToolStripButton = DirectCast(sender, ToolStripButton)
                TSButtonsActions(TSButton.Tag)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Función que ejecuta las acciones de los botones sobre el treeview
        ''' </summary>
        ''' <param name="action">Nombre de la acción</param>
        ''' <remarks></remarks>
        ''' <history>   [Javier] 02/08/2010 Created</history>
        Private Sub TSButtonsActions(ByVal action As String)

            Select Case action.ToUpper

                Case "ACTUALIZAR"
                    'PanelsController.RefreshWFs(-2, -2)
                    RefreshWorkflow(-2, -2, True)

                Case "CONTRAER"
                    Try
                        CollapseStepsNodes()
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                Case "INSERTAR"
                    Try
                        Insertar()
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

            End Select

        End Sub

#Region "LoadRestrictedSteps"

        ''' <summary>
        ''' Actualiza el Treeview, colocando el count de la tarea
        ''' </summary>
        ''' <param name="stepid"></param>
        ''' <param name="count"></param>
        ''' <remarks></remarks>
        ''' <history>   [Javier] 13/10/2010 Created</history>
        Public Sub UpdateStepCount(ByVal stepid As Int64, ByVal count As Int64)
            For i As Int32 = 0 To WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes.Count - 1
                Dim nodeStep As StepNodeIdAndName

                For j As Int32 = 0 To WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes(i).Nodes.Count - 1
                    nodeStep = DirectCast(WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes(i).Nodes(j), StepNodeIdAndName)
                    If nodeStep.WFStepid = stepid Then
                        nodeStep.UpdateTasksCount(count)
                    End If
                Next
            Next
        End Sub

        ''' <summary>
        ''' Refresca el conteo de tareas por etapa
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub RefreshStepCount(ByVal StepIDs As Object)
            If Not IsDisposed AndAlso Not Disposing AndAlso RefreshEnabled Then
                SyncLock SyncObject
                    RefreshEnabled = False
                    Dim stepCount As Int64
                    Dim stepId As Int64
                    Dim workflowId As Int64
                    Dim steps As New Dictionary(Of Int64, Int64)
                    Dim stepNode As StepNodeIdAndName = Nothing
                    Dim workflowNode As WFNodeIdandName = Nothing
                    Dim wtbExt As New WfTaskBussinesExt()

                    Dim oldStepID, newStepID As Long

                    Dim IsFirstTimeLoaded As Boolean
                    Dim nodeCount As Integer
                    Try
                        oldStepID = StepIDs(0)
                        newStepID = StepIDs(1)
                        IsFirstTimeLoaded = StepIDs(2)

                        If FastCountEnabled AndAlso oldStepID > 0 AndAlso newStepID > 0 AndAlso oldStepID <> newStepID Then
                            WFStepBusiness.InsertStepCounts()
                            WFStepBusiness.UpdateStepCounts()
                        End If


                        Dim DsNodesCount As DataSet
                        Dim DtNodesCount As DataTable

                        If FastCountEnabled Then
                            DsNodesCount = Servers.Server.Con.ExecuteDataset(CommandType.Text, "select * from zobjcount where objectTypeid = 42")
                        End If


                        If (Not DsNodesCount Is Nothing AndAlso DsNodesCount.Tables.Count > 0 AndAlso DsNodesCount.Tables(0).Rows.Count > 0) Then
                            Dim Dv As New DataView(DsNodesCount.Tables(0))
                            Dv.Sort = "Updatedate asc"
                            Dim LastTimeUpdated As DateTime = Dv.ToTable().Rows(0).Item("UpdateDate")
                            If DateDiff(DateInterval.Minute, LastTimeUpdated, DateTime.Now) <= 1 Then
                                DtNodesCount = Dv.ToTable()
                            Else

                                WFStepBusiness.InsertStepCounts()
                                WFStepBusiness.UpdateStepCounts()
                            End If
                        ElseIf FastCountEnabled Then
                            WFStepBusiness.InsertStepCounts()
                            WFStepBusiness.UpdateStepCounts()
                        End If

                        If oldStepID >= 0 Then

                            Dim old_step As IWFStep = WFStepBusiness.GetStepById(oldStepID, False)
                            'agrego en el hash la etapa desde la que se distribuye
                            If DtNodesCount IsNot Nothing Then

                                Dim StepRows As DataRow() = DtNodesCount.Select(String.Format("ObjectTypeId = {0} and ObjectId = {1}", 42, oldStepID))

                                If (StepRows Is Nothing OrElse StepRows.Length = 0) Then

                                    nodeCount = wtbExt.GetTaskCount(oldStepID, True, Membership.MembershipHelper.CurrentUser.ID)
                                Else
                                    nodeCount = StepRows(0).Item("Count")
                                End If
                            Else
                                nodeCount = wtbExt.GetTaskCount(oldStepID, True, Membership.MembershipHelper.CurrentUser.ID)
                            End If

                            steps.Add(oldStepID, nodeCount)

                            If newStepID >= 0 Then
                                Dim new_step As IWFStep = WFStepBusiness.GetStepById(newStepID, False)

                                If Not steps.ContainsKey(newStepID) Then
                                    'agrego en el hash la etapa a la que se distribuirá
                                    If DtNodesCount IsNot Nothing Then

                                        Dim StepRows As DataRow() = DtNodesCount.Select(String.Format("ObjectTypeId = {0} and ObjectId = {1}", 42, newStepID))

                                        If (StepRows Is Nothing OrElse StepRows.Length = 0) Then
                                            nodeCount = wtbExt.GetTaskCount(newStepID, True, Membership.MembershipHelper.CurrentUser.ID)
                                        Else
                                            nodeCount = StepRows(0).Item("Count")
                                        End If

                                    Else
                                        nodeCount = wtbExt.GetTaskCount(newStepID, True, Membership.MembershipHelper.CurrentUser.ID)
                                    End If

                                    steps.Add(newStepID, nodeCount)
                                End If

                                'Se actualiza el arbol de workflows y etapas con sus conteos de tareas
                                Dim workflow As New KeyValuePair(Of Int64, Int64)(old_step.WorkId, 0)
                                Invoke(New DUpdateWfAndSteps(AddressOf UpdateWfAndNodesCount), New Object() {workflow, steps})
                                workflow = Nothing
                            End If
                        Else
                            '------------------------------------------------------------------------------------
                            'Recorre los nodos de los workflows
                            If WfTreeView IsNot Nothing Then
                                Dim LastStepId As Int64

                                For i As Int32 = 0 To WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes.Count - 1
                                    'Recorre los nodos de etapas por workflow
                                    Dim IsAlmostOneStepVisibleorExpanded As Boolean
                                    For j As Int32 = 0 To WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes(i).Nodes.Count - 1
                                        stepNode = DirectCast(WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes(i).Nodes(j), StepNodeIdAndName)
                                        LastStepId = stepNode.WFStepid

                                        If IsStepVisibleAndExpanded(stepNode) OrElse IsFirstTimeLoaded Then
                                            stepId = stepNode.WFStepid

                                            If DtNodesCount IsNot Nothing Then

                                                Dim StepRows As DataRow() = DtNodesCount.Select(String.Format("ObjectTypeId = {0} and ObjectId = {1}", 42, stepId))

                                                If (StepRows Is Nothing OrElse StepRows.Length = 0) Then
                                                    stepCount = wtbExt.GetTaskCount(stepId, True, Membership.MembershipHelper.CurrentUser.ID)
                                                Else
                                                    stepCount = StepRows(0).Item("Count")
                                                End If
                                            Else
                                                stepCount = wtbExt.GetTaskCount(stepId, True, Membership.MembershipHelper.CurrentUser.ID)
                                            End If


                                            steps.Add(stepId, stepCount)
                                            IsAlmostOneStepVisibleorExpanded = True
                                        End If
                                    Next
                                    If IsAlmostOneStepVisibleorExpanded OrElse IsFirstTimeLoaded Then
                                        'Se guarda la cuenta total del workflow para procesarlo al final
                                        workflowNode = DirectCast(WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes(i), WFNodeIdandName)
                                        workflowId = workflowNode.WorkFlowId
                                        Dim workflows As New KeyValuePair(Of Int64, Int64)(workflowId, 0)

                                        'Se actualiza el arbol de workflows y etapas con sus conteos de tareas
                                        Invoke(New DUpdateWfAndSteps(AddressOf UpdateWfAndNodesCount), New Object() {workflows, steps})
                                        workflows = Nothing
                                    End If
                                    IsAlmostOneStepVisibleorExpanded = False
                                Next
                            End If
                            '------------------------------------------------------------------------------------
                        End If
                    Catch ex As ArgumentOutOfRangeException
                    Catch ex As SynchronizationLockException
                    Catch ex As ThreadAbortException
                    Catch ex As ThreadInterruptedException
                    Catch ex As ThreadStateException
                    Catch ex As Exception
                        'Por múltiples razones pueden ocurrir errores en este método ya que trabaja con hilos y no se tiene control
                        'sobre el mismo. Es por eso que solo se guarda el mensaje de error cuando el nivel de Trace esta al máximo.
                        ZClass.raiseerror(ex)

                    Finally
                        RefreshEnabled = True
                        stepCount = Nothing
                        stepId = Nothing
                        workflowId = Nothing
                        stepNode = Nothing
                        workflowNode = Nothing
                        wtbExt = Nothing
                        steps = Nothing
                        'NO PONER LOS CLEAR YA QUE POR REFERENCIA AFECTA AL METODO "UpdateWfAndNodesCount"
                        'steps.Clear() 
                        'workflows.Clear()
                        If IsFirstTimeLoaded Then
                            ' Se crea el timer
                            InstanceAutoWFTimer()
                            ' Se fija si esta checkeado o no para iniciar el timer.
                            checkActualizacion()
                        End If
                    End Try

                End SyncLock
            End If
        End Sub

        Private Function IsStepVisibleAndExpanded(ByVal stepNode As StepNodeIdAndName) As Boolean
            If InvokeRequired Then
                Return Invoke(New IsStepVisibleAndExpandedDelegate(AddressOf IsStepVisibleAndExpanded), New Object() {stepNode})
            Else
                Return stepNode.Visible AndAlso stepNode.Parent.Visible AndAlso stepNode.Parent.Expanded
            End If
        End Function

        ''' <summary>
        ''' Devuelve el conteo total de tareas de un WF
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub GetWFCount(ByVal workid As String)
            If WfTreeView IsNot Nothing Then
                Dim rootNode As RadTreeNode = WfTreeView.Nodes(NODE_PROCESSES_NUMBER)
                Dim workflowNode As WFNodeIdandName = Nothing
                Dim stepNode As StepNodeIdAndName = Nothing
                Dim stepCount As Integer
                Dim wtbExt As New WfTaskBussinesExt()

                Try
                    For i As Int32 = 0 To WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes.Count - 1
                        workflowNode = DirectCast(rootNode.Nodes(i), WFNodeIdandName)
                        If workflowNode.Visible Then
                            If workflowNode.WorkFlowId = workid Then
                                For j As Int32 = 0 To WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes(i).Nodes.Count - 1
                                    stepNode = DirectCast(WfTreeView.Nodes(NODE_PROCESSES_NUMBER).Nodes(i).Nodes(j), StepNodeIdAndName)
                                    'obtengo el count de tareas de la etapa
                                    If stepNode.Visible Then
                                        stepCount = wtbExt.GetTaskCount(stepNode.WFStepid, True, Membership.MembershipHelper.CurrentUser.ID)

                                    End If
                                Next
                            End If
                        End If
                    Next


                Catch ex As ArgumentOutOfRangeException
                Catch ex As SynchronizationLockException
                Catch ex As ThreadAbortException
                Catch ex As ThreadInterruptedException
                Catch ex As ThreadStateException
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                Finally
                    workflowNode = Nothing
                    wtbExt = Nothing
                    stepNode = Nothing
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Actualiza el arbol de procesos y etapas agregandole a cada uno el conteo de tareas.
        ''' </summary>
        ''' <param name="workflows">Workflows a procesar</param>
        ''' <param name="steps">Etapas a procesar</param>
        ''' <remarks>El diccionario se compone de ID del objeto como clave mas el número de tareas como valor</remarks>
        Private Sub UpdateWfAndNodesCount(ByVal workflow As KeyValuePair(Of Int64, Int64), ByVal steps As Dictionary(Of Int64, Int64))

            If Not IsDisposed AndAlso Not Disposing AndAlso WfTreeView IsNot Nothing Then

                Dim stepNode As StepNodeIdAndName
                Dim workflowNode As WFNodeIdandName
                Dim rootNode As RadTreeNode

                Try
                    rootNode = WfTreeView.Nodes(NODE_PROCESSES_NUMBER)

                    For i As Int32 = 0 To rootNode.Nodes.Count - 1
                        'Se actualiza el nombre del proceso con la cuenta de tareas
                        workflowNode = DirectCast(rootNode.Nodes(i), WFNodeIdandName)
                        If workflow.Key = workflowNode.WorkFlowId Then

                            Dim WFCount As Int64
                            For j As Int32 = 0 To workflowNode.Nodes.Count - 1
                                'Se actualiza el nombre de etapa con la cuenta de tareas
                                stepNode = DirectCast(workflowNode.Nodes(j), StepNodeIdAndName)
                                If steps.ContainsKey(stepNode.WFStepid) Then
                                    Dim StepCount As Int64 = steps(stepNode.WFStepid)
                                    stepNode.UpdateTasksCount(StepCount)
                                    WFCount += +StepCount
                                End If
                            Next

                            workflowNode.UpdateTasksCountparent(WFCount)
                        End If
                    Next
                Catch ex As ArgumentOutOfRangeException
                Catch ex As SynchronizationLockException
                Catch ex As ThreadAbortException
                Catch ex As ThreadInterruptedException
                Catch ex As ThreadStateException
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                Finally
                    stepNode = Nothing
                    workflowNode = Nothing
                    rootNode = Nothing
                End Try
            End If
        End Sub

#End Region

#Region "Actualizacion automatica"

        Private Sub SetIntervalo(ByVal time As Long)

            tiempoActualizacion = time

            DescheckearItemsTiempo()

            Select Case time
                Case 1
                    TSMI1Min.Checked = True
                Case 5
                    TSMI5Min.Checked = True
                Case 10
                    TSMI10Min.Checked = True
                Case 30
                    TSMI30Min.Checked = True
                Case 60
                    TSMI60Min.Checked = True
            End Select

            UserPreferences.setValue("RefreshWFClientTimer", tiempoActualizacion, UPSections.WorkFlow)
            InstanceAutoWFTimer()
            WfRefreshTimer.Change(tiempoActualizacion * 60000, tiempoActualizacion * 60000)

        End Sub

        Private Sub InstanceAutoWFTimer()
            Try
                If WfRefreshTimer Is Nothing Then
                    WfRefreshTimer = New ZTimer(TCB, state, 360000, 360000, CShort(UserPreferences.getValue("TimeStartT", UPSections.UserPreferences, "0")), CShort(UserPreferences.getValue("TimeEndT", UPSections.UserPreferences, "24")))
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub checkActualizacion()
            Try
                If InvokeRequired Then
                    Invoke(New delegatecheckActualizacion(AddressOf checkActualizacion), Nothing)
                Else
                    ' Si esta checkeado
                    If TSMIActualizar.Checked Then
                        ' Cambia texto de toolTip, habilita menu de seleccion del tiempo y setea el tiempo del timer.
                        TSSBConfActualizar.ToolTipText = "Desactivar actualizacion automatica"
                        TSMITiempo.Enabled = True
                        SetIntervalo(tiempoActualizacion)
                        ' Inicia el timer.
                        WfRefreshTimer.Resume()
                    Else
                        ' Para el timer.
                        WfRefreshTimer.Pause()
                        ' Cambia texto de toolTip, inhabilita menu de seleccion del tiempo y le quita todos los checks.
                        TSSBConfActualizar.ToolTipText = "Activar actualizacion automatica"
                        TSMITiempo.Enabled = False
                        DescheckearItemsTiempo()
                    End If
                End If
                UserPreferences.setValue("refreshwfclient", TSMIActualizar.Checked, UPSections.WorkFlow)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        'ToolStripSplitButton 
        Private Sub TSSBConfActualizar_ButtonClick(sender As Object, e As EventArgs) Handles TSSBConfActualizar.ButtonClick

            ' Cambia estado del checked
            If TSMIActualizar.Checked Then

                TSMIActualizar.Checked = False

            Else

                TSMIActualizar.Checked = True

            End If


            checkActualizacion()

        End Sub

        ' ToolStripMenuItem
        Private Sub TSMIActualizar_Click(sender As Object, e As EventArgs) Handles TSMIActualizar.Click
            checkActualizacion()
        End Sub

        ''' <summary>
        ''' Quita el check de todos los items del menuDropDown 
        ''' para seleccion del tiempo de actualizacion.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub DescheckearItemsTiempo()

            For Each item As ToolStripMenuItem In TSMITiempo.DropDownItems
                item.Checked = False
            Next

        End Sub

        ' ToolStripMenuItem 1 minuto
        Private Sub TSMI1Min_Click(sender As Object, e As EventArgs) Handles TSMI1Min.Click
            SetIntervalo(1)
        End Sub

        ' ToolStripMenuItem 5 minutos
        Private Sub TSMI5Min_Click(sender As Object, e As EventArgs) Handles TSMI5Min.Click
            SetIntervalo(5)
        End Sub

        ' ToolStripMenuItem 10 minutos
        Private Sub TSMI10Min_Click(sender As Object, e As EventArgs) Handles TSMI10Min.Click
            SetIntervalo(10)
        End Sub

        ' ToolStripMenuItem 30 minutos
        Private Sub TSMI30Min_Click(sender As Object, e As EventArgs) Handles TSMI30Min.Click
            SetIntervalo(30)
        End Sub

        ' ToolStripMenuItem 60 minutos
        Private Sub TSMI60Min_Click(sender As Object, e As EventArgs) Handles TSMI60Min.Click
            SetIntervalo(60)
        End Sub

        Friend Function SearchNodeExist(search As ISearch) As Boolean
            For Each searchNode As SearchNodeIdAndName In Me.searchNode.Nodes
                If searchNode.Name.Equals(search.Name) Then
                    Return True
                End If
            Next
            Return False
        End Function

        Friend Sub SelectSearchNodeByName(searchName As String)
            For Each searchNode As SearchNodeIdAndName In Me.searchNode.Nodes
                If searchNode.Name.Equals(searchName) Then
                    SelectNode(searchNode, False)
                    Return
                End If
            Next
        End Sub

        Friend Function IsSearchNodeSelected(searchName As String) As Boolean
            For Each searchNode As SearchNodeIdAndName In Me.searchNode.Nodes
                If searchNode.Name.Equals(searchName) Then
                    Return searchNode.Selected
                End If
            Next
            Return False
        End Function

        Public currentFont As Font
        Public currentRowHeight As Integer = 24
        Public currentFontSize As Integer = 8

        Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
            Try
                currentFontSize = currentFontSize + 1
                UserPreferences.setValue("TreeFontSize", currentFont.Size.ToString(), UPSections.UserPreferences)
                currentFont = New Font(currentFont.FontFamily, currentFontSize, currentFont.Style)
                currentRowHeight = currentFontSize * 3
                WfTreeView.Font = currentFont
                WfTreeView.ItemHeight = currentRowHeight
                SetNodesFont(WfTreeView.Nodes)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
            Try
                currentFontSize = currentFontSize - 1
                UserPreferences.setValue("TreeFontSize", currentFont.Size.ToString(), UPSections.UserPreferences)
                currentFont = New Font(currentFont.FontFamily, currentFontSize, currentFont.Style)
                currentRowHeight = currentFontSize * 3
                WfTreeView.Font = currentFont
                WfTreeView.ItemHeight = currentRowHeight
                SetNodesFont(WfTreeView.Nodes)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub SetNodesFont(Nodes As RadTreeNodeCollection)
            For Each node As RadTreeNode In Nodes
                node.Font = New Font(currentFont.FontFamily, currentFontSize, node.Font.Style, node.Font.Unit)
                node.ItemHeight = currentRowHeight
                SetNodesFont(node.Nodes)
            Next
        End Sub


#End Region

    End Class
End Namespace

