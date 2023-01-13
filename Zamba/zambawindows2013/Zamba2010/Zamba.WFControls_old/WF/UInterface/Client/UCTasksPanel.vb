Imports Zamba.Core.WF.WF
Imports Zamba.Core.DocTypes.DocAsociated
Imports Zamba.Controls.WF.TasksCtls
Imports Zamba.Controls.WF.ResultsCtls
Imports Zamba.Core.Enumerators
Imports Zamba.Core
Imports Zamba.AdminControls
Imports Zamba.Print
Imports Zamba.Viewers
Imports Zamba.IETools
Imports System.IO

Namespace WF.UInterface.Client

    ''' <summary>
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UCTasksPanel
        Inherits ZControl
        Implements IDisposable

#Region " Código generado por el Diseñador de Windows Forms "
        Public Overloads Sub Dispose() Implements IDisposable.Dispose
            UpdateAllUserAsignedTasksState(CurrentUserId) 'Actualiza todas las tareas del usuario que esten en ejecucion
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        'Public Event Finish()
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If Not isDisposed Then
                Try
                    If disposing Then
                        If components IsNot Nothing Then components.Dispose()

                        If UcDocumentsParent IsNot Nothing Then
                            RemoveHandler UcDocumentsParent.ControlRemoved, AddressOf UcDocumentsParent_ControlRemoved
                            RemoveHandler UcDocumentsParent.TabIndexChanged, AddressOf DockPanelTasks_SelectContent

                            For Each ctrlviewer As Object In UcDocumentsParent.TabPages
                                If TypeOf (ctrlviewer) Is IDisposable AndAlso ctrlviewer IsNot Nothing Then
                                    ctrlviewer.Dispose()
                                End If
                            Next
                            UcDocumentsParent.Dispose()
                            UcDocumentsParent = Nothing
                        End If


                        RemoveHandler WFTaskBusiness.LastInsertedTask, AddressOf ShowTask
                        RemoveHandler WFTaskBusiness.Distributed, AddressOf RemoveTask
                        RemoveHandler WFTaskBusiness.refreshSteps, AddressOf refreshStepsAfterDistribute
                        RemoveHandler WFTaskBusiness.AsignedAndExpireDate, AddressOf RefreshAsignedTo

                        If ParamResult IsNot Nothing Then ParamResult = Nothing
                        If lista IsNot Nothing Then lista = Nothing
                        If WFRS IsNot Nothing Then WFRS = Nothing
                        If EvaluateRuleWFRS IsNot Nothing Then EvaluateRuleWFRS = Nothing
                        If _iconList IsNot Nothing Then
                            _iconList.Dispose()
                            _iconList = Nothing
                        End If
                        If WFTreeControlForClient IsNot Nothing Then
                            WFTreeControlForClient.Dispose()
                            WFTreeControlForClient = Nothing
                        End If
                        If WFShapeCircuit IsNot Nothing Then
                            WFShapeCircuit.Dispose()
                            WFShapeCircuit = Nothing
                        End If
                        If ParamWB IsNot Nothing Then
                            RemoveHandler ParamWB.Disposed, AddressOf ParamWB_Disposed
                            RemoveHandler ParamWB.DocumentCompleted, AddressOf ParamWB_DocumentCompleted
                            RemoveHandler ParamWB.HandleDestroyed, AddressOf ParamWB_HandleDestroyed
                            RemoveHandler ParamWB.Navigated, AddressOf ParamWB_Navigated
                            ParamWB.Dispose()
                            ParamWB = Nothing
                        End If
                        If BtnClose IsNot Nothing Then
                            RemoveHandler BtnClose.Click, AddressOf BtnCloseExplorerClick
                            BtnClose.Dispose()
                            BtnClose = Nothing
                        End If
                        If BtnShowUrl IsNot Nothing Then
                            RemoveHandler BtnShowUrl.Click, AddressOf BtnShowUrlClick
                            BtnShowUrl.Dispose()
                            BtnShowUrl = Nothing
                        End If
                        If ds IsNot Nothing Then
                            ds.Dispose()
                            ds = Nothing
                        End If
                        If Webform IsNot Nothing Then
                            RemoveHandler Webform.Shown, AddressOf WebForm_Shown
                            Webform.Dispose()
                            Webform = Nothing
                        End If
                        If DockTaskDetails IsNot Nothing Then
                            For Each ctrlviewer As Object In DockTaskDetails.Controls
                                If TypeOf (ctrlviewer) Is IDisposable AndAlso ctrlviewer IsNot Nothing Then
                                    ctrlviewer.Dispose()
                                End If
                            Next
                            DockTaskDetails.Dispose()
                            DockTaskDetails = Nothing
                        End If
                        If ControllerTask IsNot Nothing Then
                            If ControllerTask.UCTaskGrid IsNot Nothing Then
                                RemoveHandler ControllerTask.UCTaskGrid.ShowComment, AddressOf ShowVersionComment
                                RemoveHandler ControllerTask.UCTaskGrid.GridTasksSelected, AddressOf TaskSelected
                                RemoveHandler ControllerTask.UCTaskGrid.TaskDoubleClick, AddressOf ShowTask
                                RemoveHandler ControllerTask.UCTaskGrid.NotAnyResultSelected, AddressOf ClearUserActions
                                RemoveHandler ControllerTask.UCTaskGrid.NotAnyResultSelected, AddressOf LoadDynamicbuttonsOverGrid
                                RemoveHandler ControllerTask.UCTaskGrid.visibleOrInvisibleButtonsRule, AddressOf visibleOrInvisibleButtonsRule
                            End If
                            ControllerTask.Dispose()
                            ControllerTask = Nothing
                        End If
                        If UCIndexViewer IsNot Nothing Then
                            UCIndexViewer.Dispose()
                            UCIndexViewer = Nothing
                        End If
                        If extVis2 IsNot Nothing Then
                            RemoveHandler extVis2.FullScreenClosed, AddressOf CambiarDock
                            extVis2.Dispose()
                            extVis2 = Nothing
                        End If
                        If PanelLeft IsNot Nothing Then
                            PanelLeft.Dispose()
                            PanelLeft = Nothing
                        End If
                        If PanelBottom IsNot Nothing Then
                            PanelBottom.Dispose()
                            PanelBottom = Nothing
                        End If
                        If PanelRight IsNot Nothing Then
                            PanelRight.Dispose()
                            PanelRight = Nothing
                        End If
                        If PanelTop IsNot Nothing Then
                            PanelTop.Dispose()
                            PanelTop = Nothing
                        End If
                        If PanelResults IsNot Nothing Then
                            PanelResults.Dispose()
                            PanelResults = Nothing
                        End If
                        If Splitter1 IsNot Nothing Then
                            RemoveHandler Splitter1.SplitterMoved, AddressOf Splitter1_SplitterMoved
                            Splitter1.Dispose()
                            Splitter1 = Nothing
                        End If
                        If ToolBar1 IsNot Nothing Then
                            RemoveHandler ToolBar1.ItemClicked, AddressOf ToolBar1_ButtonClick
                            ToolBar1.Dispose()
                            ToolBar1 = Nothing
                        End If
                        If TabTaskDetails IsNot Nothing Then
                            TabTaskDetails.Dispose()
                            TabTaskDetails = Nothing
                        End If
                        If TabGraphic IsNot Nothing Then
                            TabGraphic.Dispose()
                            TabGraphic = Nothing
                        End If
                        If PanelMain IsNot Nothing Then
                            PanelMain.Dispose()
                            PanelMain = Nothing
                        End If
                        If ToolStripContainerTasks IsNot Nothing Then
                            ToolStripContainerTasks.Dispose()
                            ToolStripContainerTasks = Nothing
                        End If
                        If toolbar IsNot Nothing Then
                            RemoveHandler toolbar.ItemClicked, AddressOf toolbar_Click
                            toolbar.Dispose()
                            toolbar = Nothing
                        End If
                        If DockResults IsNot Nothing Then
                            DockResults.Dispose()
                            DockResults = Nothing
                        End If
                        If iplugin IsNot Nothing Then
                            RemoveHandler iplugin.Save, AddressOf Save
                            RemoveHandler iplugin.CloseDocument, AddressOf Close
                            iplugin.Dispose()
                            iplugin = Nothing
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
        Friend WithEvents PanelLeft As System.Windows.Forms.Panel
        Friend WithEvents PanelBottom As System.Windows.Forms.Panel
        Friend WithEvents PanelRight As System.Windows.Forms.Panel
        Friend WithEvents PanelTop As System.Windows.Forms.Panel
        Friend WithEvents PanelResults As System.Windows.Forms.Panel
        Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
        Friend WithEvents ToolBar1 As ZToolBar
        Friend WithEvents TabTaskDetails As System.Windows.Forms.TabPage
        Friend WithEvents TabGraphic As System.Windows.Forms.TabPage
        Friend WithEvents PanelMain As System.Windows.Forms.Panel
        Friend WithEvents ToolStripContainerTasks As ToolStripContainer
        Friend WithEvents toolbar As Zamba.Controls.UCToolbarResult
        Friend WithEvents BreadCrumb As ZToolBar
        Friend WithEvents lblBreadCrumb As ToolStripLabel
        Friend WithEvents ExportPDF As ToolStripButton
        Friend WithEvents btnToggle As ToolStripButton
        Friend WithEvents DockResults As TabControl
        <DebuggerStepThrough()> Private Sub InitializeComponent()
            components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCTasksPanel))
            PanelLeft = New System.Windows.Forms.Panel()
            PanelBottom = New System.Windows.Forms.Panel()
            PanelRight = New System.Windows.Forms.Panel()
            PanelTop = New System.Windows.Forms.Panel()
            PanelResults = New System.Windows.Forms.Panel()
            Splitter1 = New System.Windows.Forms.Splitter()
            PanelMain = New System.Windows.Forms.Panel()
            ToolStripContainerTasks = New System.Windows.Forms.ToolStripContainer()
            BreadCrumb = New Zamba.AppBlock.ZToolBar()
            btnToggle = New System.Windows.Forms.ToolStripButton()
            lblBreadCrumb = New System.Windows.Forms.ToolStripLabel()
            ToolBar1 = New Zamba.AppBlock.ZToolBar()
            toolbar = New Zamba.Controls.UCToolbarResult(components)
            ExportPDF = New System.Windows.Forms.ToolStripButton()
            TabTaskDetails = New System.Windows.Forms.TabPage()
            TabGraphic = New System.Windows.Forms.TabPage()
            DockResults = New System.Windows.Forms.TabControl()
            PanelMain.SuspendLayout()
            ToolStripContainerTasks.TopToolStripPanel.SuspendLayout()
            ToolStripContainerTasks.SuspendLayout()
            BreadCrumb.SuspendLayout()
            toolbar.SuspendLayout()
            SuspendLayout()
            '
            'PanelLeft
            '
            PanelLeft.BackColor = System.Drawing.Color.White
            PanelLeft.Dock = System.Windows.Forms.DockStyle.Left
            PanelLeft.Location = New System.Drawing.Point(0, 0)
            PanelLeft.Margin = New System.Windows.Forms.Padding(0)
            PanelLeft.Name = "PanelLeft"
            PanelLeft.Size = New System.Drawing.Size(3, 416)
            PanelLeft.TabIndex = 33
            '
            'PanelBottom
            '
            PanelBottom.BackColor = System.Drawing.Color.White
            PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
            PanelBottom.Location = New System.Drawing.Point(3, 413)
            PanelBottom.Margin = New System.Windows.Forms.Padding(0)
            PanelBottom.Name = "PanelBottom"
            PanelBottom.Size = New System.Drawing.Size(826, 3)
            PanelBottom.TabIndex = 34
            '
            'PanelRight
            '
            PanelRight.BackColor = System.Drawing.Color.White
            PanelRight.Dock = System.Windows.Forms.DockStyle.Right
            PanelRight.Location = New System.Drawing.Point(829, 0)
            PanelRight.Margin = New System.Windows.Forms.Padding(0)
            PanelRight.Name = "PanelRight"
            PanelRight.Size = New System.Drawing.Size(3, 416)
            PanelRight.TabIndex = 35
            '
            'PanelTop
            '
            PanelTop.BackColor = System.Drawing.Color.White
            PanelTop.Dock = System.Windows.Forms.DockStyle.Top
            PanelTop.Location = New System.Drawing.Point(3, 0)
            PanelTop.Margin = New System.Windows.Forms.Padding(0)
            PanelTop.Name = "PanelTop"
            PanelTop.Size = New System.Drawing.Size(826, 3)
            PanelTop.TabIndex = 36
            '
            'PanelResults
            '
            PanelResults.BackColor = System.Drawing.Color.White
            PanelResults.Dock = System.Windows.Forms.DockStyle.Left
            PanelResults.Location = New System.Drawing.Point(3, 3)
            PanelResults.Margin = New System.Windows.Forms.Padding(0)
            PanelResults.Name = "PanelResults"
            PanelResults.Size = New System.Drawing.Size(204, 410)
            PanelResults.TabIndex = 37
            '
            'Splitter1
            '
            Splitter1.BackColor = System.Drawing.Color.White
            Splitter1.Location = New System.Drawing.Point(207, 3)
            Splitter1.Margin = New System.Windows.Forms.Padding(0)
            Splitter1.Name = "Splitter1"
            Splitter1.Size = New System.Drawing.Size(4, 410)
            Splitter1.TabIndex = 38
            Splitter1.TabStop = False
            '
            'PanelMain
            '
            PanelMain.BackColor = System.Drawing.Color.White
            PanelMain.Controls.Add(ToolStripContainerTasks)
            PanelMain.Dock = System.Windows.Forms.DockStyle.Fill
            PanelMain.Location = New System.Drawing.Point(211, 3)
            PanelMain.Margin = New System.Windows.Forms.Padding(0)
            PanelMain.Name = "PanelMain"
            PanelMain.Size = New System.Drawing.Size(618, 410)
            PanelMain.TabIndex = 39
            '
            'ToolStripContainerTasks
            '
            '
            'ToolStripContainerTasks.ContentPanel
            '
            ToolStripContainerTasks.ContentPanel.Margin = New System.Windows.Forms.Padding(0)
            ToolStripContainerTasks.ContentPanel.Size = New System.Drawing.Size(618, 327)
            ToolStripContainerTasks.Dock = System.Windows.Forms.DockStyle.Fill
            ToolStripContainerTasks.Location = New System.Drawing.Point(0, 0)
            ToolStripContainerTasks.Name = "ToolStripContainerTasks"
            ToolStripContainerTasks.Size = New System.Drawing.Size(618, 410)
            ToolStripContainerTasks.TabIndex = 0
            ToolStripContainerTasks.Text = "ToolStripContainer1"
            '
            'ToolStripContainerTasks.TopToolStripPanel
            '
            ToolStripContainerTasks.TopToolStripPanel.BackColor = System.Drawing.Color.Transparent
            ToolStripContainerTasks.TopToolStripPanel.Controls.Add(BreadCrumb)
            ToolStripContainerTasks.TopToolStripPanel.Controls.Add(ToolBar1)
            ToolStripContainerTasks.TopToolStripPanel.Controls.Add(toolbar)
            '
            'BreadCrumb
            '
            BreadCrumb.AllowMerge = False
            BreadCrumb.BackColor = System.Drawing.Color.White
            BreadCrumb.CanOverflow = False
            BreadCrumb.Dock = System.Windows.Forms.DockStyle.None
            BreadCrumb.Font = New Font("Verdana", 12.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            BreadCrumb.GripMargin = New System.Windows.Forms.Padding(0)
            BreadCrumb.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            BreadCrumb.ImageScalingSize = New System.Drawing.Size(22, 22)
            BreadCrumb.Items.AddRange(New System.Windows.Forms.ToolStripItem() {btnToggle, lblBreadCrumb})
            BreadCrumb.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
            BreadCrumb.Location = New System.Drawing.Point(0, 0)
            BreadCrumb.Name = "BreadCrumb"
            BreadCrumb.Padding = New System.Windows.Forms.Padding(0)
            BreadCrumb.ShowItemToolTips = False
            BreadCrumb.Size = New System.Drawing.Size(618, 29)
            BreadCrumb.Stretch = True
            BreadCrumb.TabIndex = 61
            '
            'btnToggle
            '
            btnToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            btnToggle.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_navigate_previous
            btnToggle.ImageTransparentColor = System.Drawing.Color.Magenta
            btnToggle.Name = "btnToggle"
            btnToggle.Size = New System.Drawing.Size(26, 26)
            btnToggle.Text = "ToolStripButton1"
            '
            'lblBreadCrumb
            '
            lblBreadCrumb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
            lblBreadCrumb.Name = "lblBreadCrumb"
            lblBreadCrumb.Size = New System.Drawing.Size(0, 26)
            '
            'ToolBar1
            '
            ToolBar1.BackColor = System.Drawing.Color.White
            ToolBar1.Dock = System.Windows.Forms.DockStyle.None
            ToolBar1.Font = New Font("Verdana", 6.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            ToolBar1.GripMargin = New System.Windows.Forms.Padding(0)
            ToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            ToolBar1.ImageScalingSize = New System.Drawing.Size(22, 22)
            ToolBar1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
            ToolBar1.Location = New System.Drawing.Point(0, 29)
            ToolBar1.Name = "ToolBar1"
            ToolBar1.Padding = New System.Windows.Forms.Padding(0)
            ToolBar1.Size = New System.Drawing.Size(618, 25)
            ToolBar1.Stretch = True
            ToolBar1.TabIndex = 60
            '
            'toolbar
            '
            toolbar.AllowItemReorder = True
            toolbar.AutoSize = False
            toolbar.BackColor = System.Drawing.Color.White
            toolbar.CanOverflow = False
            toolbar.Dock = System.Windows.Forms.DockStyle.None
            toolbar.GripMargin = New System.Windows.Forms.Padding(0)
            toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            toolbar.ImageScalingSize = New System.Drawing.Size(22, 22)
            toolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ExportPDF})
            toolbar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
            toolbar.Location = New System.Drawing.Point(0, 54)
            toolbar.Name = "toolbar"
            toolbar.Padding = New System.Windows.Forms.Padding(0)
            toolbar.Size = New System.Drawing.Size(618, 29)
            toolbar.Stretch = True
            toolbar.TabIndex = 0
            '
            'ExportPDF
            '
            ExportPDF.Image = CType(resources.GetObject("ExportPDF.Image"), System.Drawing.Image)
            ExportPDF.ImageTransparentColor = System.Drawing.Color.Magenta
            ExportPDF.Name = "ExportPDF"
            ExportPDF.Size = New System.Drawing.Size(106, 26)
            ExportPDF.Tag = "EXPORTPDF"
            ExportPDF.Text = "Exportar a pdf"
            ExportPDF.Visible = False
            '
            'TabTaskDetails
            '
            TabTaskDetails.BackColor = System.Drawing.Color.White
            TabTaskDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
            TabTaskDetails.Location = New System.Drawing.Point(4, 20)
            TabTaskDetails.Margin = New System.Windows.Forms.Padding(0)
            TabTaskDetails.Name = "TabTaskDetails"
            TabTaskDetails.Size = New System.Drawing.Size(610, 386)
            TabTaskDetails.TabIndex = 2
            TabTaskDetails.UseVisualStyleBackColor = True
            '
            'TabGraphic
            '
            TabGraphic.BackColor = System.Drawing.Color.White
            TabGraphic.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
            TabGraphic.Location = New System.Drawing.Point(4, 20)
            TabGraphic.Margin = New System.Windows.Forms.Padding(0)
            TabGraphic.Name = "TabGraphic"
            TabGraphic.Size = New System.Drawing.Size(610, 386)
            TabGraphic.TabIndex = 3
            TabGraphic.UseVisualStyleBackColor = True
            '
            'DockResults
            '
            DockResults.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
            DockResults.Font = New Font("Tahoma", 7.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            DockResults.Location = New System.Drawing.Point(0, 0)
            DockResults.Name = "DockResults"
            DockResults.SelectedIndex = 0
            DockResults.Size = New System.Drawing.Size(200, 100)
            DockResults.TabIndex = 0
            '
            'UCTasksPanel
            '
            BackColor = System.Drawing.Color.White
            Controls.Add(PanelMain)
            Controls.Add(Splitter1)
            Controls.Add(PanelResults)
            Controls.Add(PanelBottom)
            Controls.Add(PanelTop)
            Controls.Add(PanelRight)
            Controls.Add(PanelLeft)
            Margin = New System.Windows.Forms.Padding(0)
            Name = "UCTasksPanel"
            Size = New System.Drawing.Size(832, 416)
            PanelMain.ResumeLayout(False)
            ToolStripContainerTasks.TopToolStripPanel.ResumeLayout(False)
            ToolStripContainerTasks.TopToolStripPanel.PerformLayout()
            ToolStripContainerTasks.ResumeLayout(False)
            ToolStripContainerTasks.PerformLayout()
            BreadCrumb.ResumeLayout(False)
            BreadCrumb.PerformLayout()
            toolbar.ResumeLayout(False)
            toolbar.PerformLayout()
            ResumeLayout(False)

        End Sub


#End Region

#Region "Propiedades"
        ''' <summary>
        ''' Propiedad de sólo lectura que retorna una lista (de tipo TaskResult) con las tareas que se seleccionaron
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    27/06/2008 Created 
        ''' </history>
        ReadOnly Property SelectedResult(ByVal UseCheck As Boolean) As List(Of GridTaskResult)

            Get
                Return (ControllerTask.UCTaskGrid.SelectedTaskResultsExtern(UseCheck))
            End Get

        End Property


#End Region

#Region "WORKFLOW"

#End Region
        Dim CurrentUserId As Int64

        Private _iconList As Zamba.AppBlock.ZIconsList
        Public WithEvents WFTreeControlForClient As UCWFTreeControlForClient
        Dim WFShapeCircuit As Zamba.WFShapes.Controls.MainForm
        Private Delegate Sub dLoadGraphicTab(ByVal wfId As Int64)
        Public WithEvents MainFormTabControl As TabControl

        ''' <summary>
        ''' Constructor del UCPanels
        ''' </summary>
        ''' <remarks></remarks>
        ''' <History>
        ''' ivan 25/01/16 - Agregue parametros taskSearchResult y Search, 
        ''' para guardar resultados y busqueda cuando se muestran tareas desde tabSearch.
        ''' </History>
        Public Sub New(ByVal CurrentUserId As Int64, ByVal MainFormTabControl As TabControl, ByVal fromSearch As Boolean)

            MyBase.New()
            Me.CurrentUserId = CurrentUserId
            Me.MainFormTabControl = MainFormTabControl

            InitializeComponent()

            SuspendLayout()
            Me.MainFormTabControl.TabPages.Add(TabTaskDetails)
            Me.MainFormTabControl.TabPages.Add(TabGraphic)

            Try
                _iconList = New Zamba.AppBlock.ZIconsList
                LoadUCPanels(fromSearch)

                RemoveHandler WFTaskBusiness.LastInsertedTask, AddressOf ShowTask
                AddHandler WFTaskBusiness.LastInsertedTask, AddressOf ShowTask

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            ResumeLayout()
        End Sub

#Region "Load"
        Private Sub LoadUCPanels(ByVal fromSearch As Boolean)
            Try
                LoadController() 'Controller
                LoadResults(fromSearch) 'WFResultPanel
                LoadViewer() 'Viewer
                RefreshHandlers() 'Refresh
                LoadTaskBarButtons() 'TaskBar
                ' WFTreeControlForClient.SelectCurrentStep(True, True)
                PosicionarSplitterWf()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub


        ''' <summary>
        ''' Posiciona el splitter de panel de wf como se habia dejado en la ultima instancia.
        ''' </summary>
        ''' <history>
        '''     [Tomas] 20/04/2011  Created
        ''' </history>
        ''' <remarks>En caso de superar la resolucion de la pantalla lo ajusta al valor por defecto.</remarks>
        Private Sub PosicionarSplitterWf()
            Dim ancho As Int32 = Int32.Parse(UserPreferences.getValue("PaneldeWFAncho", UPSections.WorkFlow, 200))

            If ancho > (My.Computer.Screen.WorkingArea.Width * 0.95) Then
                ancho = 200
            End If

            PanelResults.Width = ancho
        End Sub

        '''' <summary>
        '''' Actualiza el estado de todas las tareas que tenga el usuario en ejecucion y las pasa a Asignadas
        '''' </summary>
        '''' <param name="UserID"></param>
        '''' <remarks></remarks>
        Private Sub UpdateAllUserAsignedTasksState(ByVal UserID As Long)
            Try
                WFTaskBusiness.UpdateAllUserAsignedTasksState(UserID)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub


#End Region

#Region "Docks"
        Private Sub DockPanelTasks_SelectContent(ByVal o As Object, ByVal e As EventArgs)
            Try
                If Not IsNothing(UcDocumentsParent.SelectedTab) Then
                    If TypeOf UcDocumentsParent.SelectedTab Is ZTaskContent Then
                        ControllerTask.UCTaskGrid.SelectTaskExtern(DirectCast(UcDocumentsParent.SelectedTab, ZTaskContent).TaskResult.TaskId)
                        '''''''UcResults.SelectResult(DirectCast(UcDocumentsParent.SelectedTab, ZTaskContent).Result)
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region

#Region "UcResults"
        Private Sub LoadResults(ByVal fromSearch As Boolean)
            Try
                WFTreeControlForClient = New UCWFTreeControlForClient(_iconList, ControllerTask.UCTaskGrid, Me, fromSearch)
                WFTreeControlForClient.Dock = DockStyle.Fill
                PanelResults.Controls.Add(WFTreeControlForClient)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#Region "Interaccion con Reglas"
        Private RuleId As Int64
        Private ParamResult As System.Collections.Generic.List(Of Core.ITaskResult)
        Private WithEvents ParamWB As WebBrowser
        Private ContinueWithRule As Boolean
        Private CloseBrowser As Boolean = True
        Private EvaluateRuleID As Int64
        Private ExecuteElseID As Int32
        Private habilitar As Boolean
        Private CloseUrlTextbox As Boolean
        Private lista As List(Of ITaskResult)
        Private WFRS As WFRulesBusiness
        Private EvaluateRuleWFRS As WFRulesBusiness
        Private WFStepId As Int64
        Private EvaluateRuleWFStepId As Int64
        Private ds As DataSet
        Private zValue, zvar, rulevalue, TxtVar, Operador, Value As String
        Private BtnClose As New Button
        Private BtnShowUrl As New Button
        Private Webform As System.Windows.Forms.Form
        Private Url As TextBox
        ''' <summary>
        ''' Mueve los parametros necesarios para ejecutar la regla DoExecuteExplorer en UCWFTreeControlForClient
        ''' </summary>
        ''' <param name="params"></param>
        ''' <remarks>
        ''' (Pablo)	09/10/2010	Configura el ancho de la ventana del Browser de la regla
        ''' y lo restablece a su tamaño anterior al cerrarla
        '''</remarks>
        ''' <history>
        '''     (Pablo)	09/10/2010	Created
        ''' </history>
        Public Sub OpenBrowser(ByVal params As Hashtable)
            Cursor = Cursors.WaitCursor
            SuspendLayout()
            Dim OpenedTask As List(Of ITaskResult)
            OpenedTask = params.Item("paramResult")

            Try
                If params.Item("NewBrowser") = False Then
                    If params.Item("BrowserClosed") Then
                        If params.Item("changeVisuals") Then
                            ChangeVisualization(params)
                        End If
                    Else
                        params.Add("UCWidth", PanelResults.Width)
                        params.Add("UCHeight", PanelResults.Height)

                        'paso los parametros a UCTaskViewer
                        Dim ztc As UCTaskViewer = GetTaskViewer(OpenedTask(0).TaskId)
                        ztc.OpenBrowser(params)
                    End If
                Else
                    OpenWBrowser(params)
                End If
                ResumeLayout()
                Cursor = Cursors.Default
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        ''' <summary> 
        ''' Visualiza un nuevo WebBrowser en una nueva ventana
        ''' </summary> 
        ''' <remarks></remarks> 
        ''' <history>   [Pablo] 13/10/2010 Created</history> 
        Public Sub OpenWBrowser(ByVal params As Hashtable)
            Dim split As New SplitContainer
            split.Name = "split1"
            IECache.ClearCache()

            RuleId = Int32.Parse(params.Item("ruleId").ToString())
            ParamResult = params.Item("paramResult")
            ContinueWithRule = params.Item("ContinueWithRule")
            params.Item("BrowserCloser") = False
            EvaluateRuleID = params.Item("EvaluateRuleID")
            ExecuteElseID = params.Item("ExecuteElseID")
            habilitar = params.Item("Habilitar")
            TxtVar = params.Item("TxtVar")
            Operador = params.Item("Operador")
            Value = params.Item("Value")

            Try
                If Not ParamWB Is Nothing Then
                    ParamWB.Dispose()
                    ParamWB = Nothing
                End If
                If Not Webform Is Nothing Then
                    Webform.Controls.Clear()
                    Webform.Close()
                    Webform.Dispose()
                End If

                BtnClose = New Button
                BtnClose.Dock = DockStyle.Fill
                BtnShowUrl = New Button
                BtnShowUrl.Dock = DockStyle.Fill
                BtnClose.Text = "Cerrar Explorador"

                BtnShowUrl.BackgroundImage = Global.Zamba.Controls.My.Resources.Resources.showInfo
                BtnShowUrl.BackgroundImageLayout = ImageLayout.Stretch

                RemoveHandler BtnClose.Click, AddressOf BtnCloseExplorerClick
                AddHandler BtnClose.Click, AddressOf BtnCloseExplorerClick
                RemoveHandler BtnShowUrl.Click, AddressOf BtnShowUrlClick
                AddHandler BtnShowUrl.Click, AddressOf BtnShowUrlClick

                Webform = New Form
                RemoveHandler Webform.Shown, AddressOf WebForm_Shown
                AddHandler Webform.Shown, AddressOf WebForm_Shown

                Webform.Controls.Clear()
                Webform.WindowState = FormWindowState.Maximized

                split.Panel1.Controls.Add(BtnClose)
                split.Panel2.Controls.Add(BtnShowUrl)
                split.BorderStyle = BorderStyle.FixedSingle
                split.Dock = DockStyle.Top
                split.IsSplitterFixed = True
                split.Size = New System.Drawing.Point(Webform.Width, 22)
                'Configuración del webBrowser
                ParamWB = New WebBrowser
                ParamWB.Dock = DockStyle.Fill
                Webform.ControlBox = False
                Webform.Controls.Add(ParamWB)
                Webform.Controls.Add(split)
                'Webform.Controls.Add(BtnShowUrl)
                Webform.Name = "Zamba - Web Browser"
                Webform.Text = "Zamba - Web Browser"

                Webform.Show()
                ParamWB.BringToFront()

                If Not String.IsNullOrEmpty(UserPreferences.getValue("EnableJsStartSessionFunction", UPSections.WorkFlow, "")) Then
                    ParamWB.Navigate(UserPreferences.getValue("EnableJsStartSessionFunction", UPSections.WorkFlow, ""))
                End If

                ParamWB.Navigate(New Uri(params.Item("Route")))

                ZTrace.WriteLineIf(ZTrace.IsInfo, "La regla se ejecutó con exito")
            Catch ex As System.Runtime.InteropServices.COMException
                ParamWB = Nothing
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            Finally
                ResumeLayout()
                Cursor = Cursors.Default
            End Try
        End Sub
        Private Sub WebForm_Shown(ByVal sender As Object, ByVal e As EventArgs)
            DirectCast(DirectCast(sender, Form).Controls("split1"), SplitContainer).SplitterDistance = DirectCast(sender, Form).Width - 45
        End Sub
        ''' <summary>
        ''' Metodo que se ejecuta al apretar el boton de cerrar de la DoExecuteExplorer
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param
        ''' <remarks></remarks>
        Private Sub BtnCloseExplorerClick(ByVal sender As Object, ByVal e As EventArgs)
            CloseDoExplorerBrowser()
        End Sub
        Private Function CloseDoExplorerBrowser() As Boolean
            Try
                SuspendLayout()
                'Si se cierra el explorador de la DoExecuteExplorer, limpio todas las variables
                If closeExplorer() = True Then
                    RuleId = 0
                    ParamResult = Nothing
                    If Not String.IsNullOrEmpty(UserPreferences.getValue("EnableJsEndSessionFunction", UPSections.WorkFlow, "")) Then
                        ParamWB.Navigate(UserPreferences.getValue("EnableJsEndSessionFunction", UPSections.WorkFlow, ""))
                    End If

                    ContinueWithRule = False
                    EvaluateRuleID = 0
                    ExecuteElseID = 0
                    habilitar = False
                    TxtVar = String.Empty
                    Operador = String.Empty
                    Value = String.Empty
                    If Not ParamWB Is Nothing Then ParamWB.Dispose()
                    ParamWB = Nothing
                    Return True
                Else
                    Return False
                End If
            Catch ex As System.Runtime.InteropServices.COMException
                ParamWB = Nothing
            Catch ex As Exception
                ParamWB = Nothing
                ZClass.raiseerror(ex)
            Finally
                ResumeLayout()
            End Try
        End Function
        ''' <summary>
        ''' Metodo que se ejecuta para cerrar el explorador de la DOExecuteExplorer
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function closeExplorer() As Boolean
            If RuleId > 0 Then
                Dim params As New Hashtable
                Dim result As Zamba.Core.Result
                Dim close As Boolean = True
                params.Add("paramResult", ParamResult)
                params.Add("ExecuteElseID", ExecuteElseID)
                params.Add("ContinueWithRule", ContinueWithRule)
                params.Add("ruleId", RuleId)
                params.Add("changeVisuals", True)

                lista = New List(Of ITaskResult)
                EvaluateRuleWFRS = New WFRulesBusiness
                WFStepId = WFStepBusiness.GetStepIdByRuleId(EvaluateRuleID, True)
                WFRS = New WFRulesBusiness
                Try
                    If habilitar Then
                        If EvaluateRuleID <> -1 Then
                            lista = WFRS.ExecuteRule(EvaluateRuleID, WFStepId, ParamResult, True, Nothing)
                        Else
                            lista = params.Item("paramResult")
                        End If

                        If Zamba.Core.WF.WF.WFTaskBusiness.ValidateVar(lista(0), TxtVar, Operador, Value) = True Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado de la Validacion de Cierre: Verdadero")
                            params.Add("CloseBrowser", True)
                            RuleId = 0
                        Else
                            close = False
                            params.Add("CloseBrowser", False)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado de la Validacion de Cierre: Falso")
                        End If
                    End If

                    If params.Contains("CloseBrowser") = False Then
                        params.Add("CloseBrowser", True)
                    End If

                    If EvaluateRuleID > 0 Then
                        If ExecuteElseID > 0 Then

                            If params.Item("CloseBrowser") Then
                                Webform.Close()
                                Webform.Dispose()
                            Else
                                ZambaCore.HandleModule(ResultActions.CloseBrowser, result, params)
                                Return close
                            End If
                            ZambaCore.HandleModule(ResultActions.CloseBrowser, result, params)
                            Return close
                        End If
                        params.Add("BrowserClosed", True)
                    Else
                        'true: fullscreen mode
                        'false: inside Zamba
                        Webform.Close()
                        ZambaCore.HandleModule(ResultActions.CloseBrowser, result, params)
                    End If
                    ParamWB.Navigate("'about:blank")

                    If Not IsNothing(ParamWB) Then
                        ParamWB.Dispose()
                    End If

                    ParamWB = Nothing

                    Return close
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                    Return True
                End Try
            Else
                Webform.Close()
                Webform.Dispose()
                Return True
            End If
        End Function
        Private Sub BtnShowUrlClick(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If Not ParamWB Is Nothing AndAlso Not ParamWB.Url Is Nothing Then
                    If CloseUrlTextbox Then
                        ParamWB.Controls.Remove(Url)
                        Url.Dispose()
                        CloseUrlTextbox = False
                    Else
                        Url = New TextBox
                        Url.Text = ParamWB.Url.ToString
                        Url.Visible = True
                        Url.Name = "Url"
                        Url.Size = New System.Drawing.Size(157, 20)
                        Url.TabIndex = 0
                        Url.Dock = DockStyle.Top
                        ParamWB.Controls.Add(Url)
                        Url.Show()
                        Url.BringToFront()
                        CloseUrlTextbox = True
                    End If
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub

        Public Sub AddInsertFormNodeToWFTree(_currentResult As Result)
            WFTreeControlForClient.AddAndSelectInsertFormNode(_currentResult)
        End Sub
        Public Sub AddInsertedFormNodeToWFTree(_currentResult As Result)
            WFTreeControlForClient.AddAndSelectInsertedFormNode(_currentResult)
        End Sub

        Private Sub ParamWB_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles ParamWB.Disposed
            Try
                ZTrace.WriteLineIf(ZTrace.IsInfo, "ParamWB_Disposed: system.EventArgs no contiene URL para este evento")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub ParamWB_DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles ParamWB.DocumentCompleted
            Try
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Evento ParamWB_DocumentCompleted - Url: " + e.Url.ToString)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub ParamWB_HandleDestroyed(ByVal sender As Object, ByVal e As EventArgs) Handles ParamWB.HandleDestroyed
            Try
                ZTrace.WriteLineIf(ZTrace.IsInfo, "ParamWB_HandleDestroyed no contiene URL para este evento")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub ParamWB_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles ParamWB.Navigated
            Try
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Evento ParamWB_Navigated - Url: " & e.Url.ToString)

                If IsNothing(ParamWB.Url) Then
                    CloseDoExplorerBrowser()
                End If
                If String.Compare("about:blank", e.Url.ToString.Trim) = 0 Then
                    CloseDoExplorerBrowser()
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Cambia la visualizacion del webBrowser
        ''' </summary>
        ''' <param name="params"></param>
        ''' <remarks>
        ''' (Pablo)	01/12/2010
        '''</remarks>
        ''' <history>
        '''     (Pablo)	01/12/2010	Created
        ''' </history>
        Public Sub ChangeVisualization(ByVal params As Hashtable)
            Dim splitUcpanels As New SplitContainer

            If params.Item("changeVisuals") Then
                PanelResults.Controls.Clear()
                PanelResults.Controls.Add(params.Item("TaskViewerSplit"))

                BackColor = System.Drawing.Color.LightSkyBlue
                Controls.Add(PanelMain)
                Controls.Add(Splitter1)
                Controls.Add(PanelResults)
                Controls.Add(PanelBottom)
                Controls.Add(PanelTop)
                Controls.Add(PanelRight)
                Controls.Add(PanelLeft)

                Margin = New System.Windows.Forms.Padding(0)
                Name = "UCPanels"
                If params.Item("ItsHorizontalVisualization") Then
                    Size = New System.Drawing.Size(832, 416)
                Else
                    PanelResults.Size = New System.Drawing.Size(params.Item("UCWidth"), 410)
                End If

                PanelMain.ResumeLayout(False)
                ToolStripContainerTasks.TopToolStripPanel.ResumeLayout(False)
                ToolStripContainerTasks.TopToolStripPanel.PerformLayout()
                ToolStripContainerTasks.ResumeLayout(False)
                ToolStripContainerTasks.PerformLayout()
                ToolBar1.ResumeLayout(False)
                ToolBar1.PerformLayout()
                ResumeLayout(False)
            Else
                splitUcpanels = params.Item("TaskViewerSplit")
                Controls.Add(splitUcpanels)
                BringToFront()
            End If
        End Sub

        Friend Sub OpenInsertedTask(id As Long)
            'Obtengo la tarea
            lblBreadCrumb.Text = String.Empty
            Dim task As ITaskResult = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(id, 0)
            ShowTask(task)
        End Sub
#End Region




#Region "TaskViewer"
        Dim WithEvents UcDocumentsParent As UCDocumentsParent
        'utilizada para validar si el tab TabTaskDetails se encuentra dentro del tabControl tbDiagrama
        Public OpenInFullScreen As Boolean

        Private Sub LoadViewer()
            Try
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargo el UCDEocumentsParent " & Now.ToString)
                UcDocumentsParent = New UCDocumentsParent
                UcDocumentsParent.Dock = DockStyle.Fill
                TabTaskDetails.Controls.Add(UcDocumentsParent)
                'RemoveHandler UcDocumentsParent.ShowTaskList, AddressOf ShowTaskListScreen
                'AddHandler UcDocumentsParent.ShowTaskList, AddressOf ShowTaskListScreen
                DockResults.Dock = DockStyle.Fill
                DockResults.SendToBack()
                Controls.Add(DockResults)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Método que sirve para viusalizar la tarea sobre la que se hizo doble click
        ''' </summary>
        ''' <param name="TaskId"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	10/10/2008	Modified    Evento de UCTaskViewer y se cambio el parámetro que recibe el método GetTaskViewer  (taskId)
        '''     [Marcelo]	12/03/2009	Modified    Se agrego el parámetro del resultID asi no lo pedimos a la base de  datos
        '''     [Marcelo]	23/03/2009	Modified    Now it saves the editDate when the viewer is created
        ''' </history>
        Public Sub ShowTaskViewer(ByVal TaskId As Int64, ByVal stepId As Int64, ByVal docTypeId As Int64)
            Dim param() As Object = {TaskId, stepId, docTypeId}
            BeginInvoke(New ShowTaskViewerDelegate(AddressOf ShowTaskViewerLazy), param)
        End Sub

        Private Delegate Sub ShowTaskViewerDelegate(ByRef TaskId As Int64, ByVal stepId As Int64, ByVal docTypeId As Int64)


        Public Sub ShowDocumentInTaskViewer(ByVal ParentTaskId As Int64, ByVal Result As IResult)
            Dim param() As Object = {ParentTaskId, Result}
            BeginInvoke(New ShowDocumentInTaskViewerDelegate(AddressOf ShowDocumentInTaskViewerLazy), param)
        End Sub

        Private Delegate Sub ShowDocumentInTaskViewerDelegate(ByRef TaskId As Int64, ByVal Result As IResult)

        ''' <summary>
        ''' Carga la tarea
        ''' </summary>        
        ''' <param name="TaskId">Id de la tarea</param>
        ''' <history>
        '''     Marcelo     11/11/2009  Modified  
        '''     Sebastian   13/11/2009  Modified
        '''     Javier      06/12/2010  Modified    GetTaskByTaskIdAndDocTypeIdAndStepId is now a instance method
        ''' </history>
        ''' <remarks></remarks>
        Private Sub ShowTaskViewerLazy(ByRef TaskId As Int64, ByVal stepId As Int64, ByVal docTypeId As Int64)
            Cursor = Cursors.WaitCursor
            Dim ztc As UCTaskViewer = Nothing
            Dim taskbs As WFTaskBusiness
            Try
                taskbs = New WFTaskBusiness()
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargo el UCTaskViewer " & Now.ToString)
                RemoveHandler UcDocumentsParent.TabIndexChanged, AddressOf DockPanelTasks_SelectContent

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Busco si esta abierto el tab del UCTaskViewer " & Now.ToString)
                ztc = GetTaskViewer(TaskId)

                'Si no existe la solapa la creo, sino la actualizo...
                If (IsNothing(ztc)) Then
                    Dim task As ITaskResult

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No estaba abierto el UCTaskViewer " & Now.ToString)

                    task = taskbs.GetTaskByTaskIdAndDocTypeIdAndStepId(TaskId, docTypeId, stepId, 0)

                    'Verifica la existencia de la tarea
                    If task Is Nothing Then
                        MessageBox.Show("No se encontró el documento solicitado. Es posible que el mismo haya sido eliminado " &
                                        "o bien usted no tenga permisos para visualizarlo." & vbCrLf & "Consulte con el área de Sistemas.",
                                        "Documento no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    ' Carga el contenedor de tarea...
                    LoadTaskDetailsdContent(task)

                    ' instancia un visor de tarea...
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Instancio el UCTaskViewer " & Now.ToString)

                    ztc = New UCTaskViewer(task, CurrentUserId, Me)
                    RemoveHandler ztc.currentContextMenu.ItemClicked, AddressOf currentContextMenuClick
                    AddHandler ztc.currentContextMenu.ItemClicked, AddressOf currentContextMenuClick
                    RemoveHandler ztc.currentContextMenuItemClicked, AddressOf currentContextMenuClick
                    AddHandler ztc.currentContextMenuItemClicked, AddressOf currentContextMenuClick
                    RemoveHandler ztc.ClosingTask, AddressOf ClosingTaskInTaskDetails
                    AddHandler ztc.ClosingTask, AddressOf ClosingTaskInTaskDetails

                    ztc.Dock = DockStyle.Fill
                    ztc.TabDoc.EnableForm(False)

                    ztc.Enabled = False
                    '[Sebastian 13-11-2009] se fuerza que el tab que se visualize primero sea el de tareas
                    ztc.TabControl1.SelectedTab = ztc.TabDoc

                    ' agrega el vosir al contenedor..
                    DockTaskDetails.Controls.Add(ztc)

                    ztc.TaskResult.EditDate = Now()
                Else
                    ztc.TabDoc.EnableForm(False)
                    ztc.Enabled = False
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Selecciono el tab ")
                If UcDocumentsParent.IsDisposed = False AndAlso UcDocumentsParent.Disposing = False Then
                    If ztc.IsDisposed = False AndAlso ztc.Parent.Disposing = False Then
                        If UcDocumentsParent.TabPages.Contains(DirectCast(ztc.Parent, ZTaskContent)) Then
                            UcDocumentsParent.SelectTab(DirectCast(ztc.Parent, ZTaskContent))
                        End If
                    End If
                End If

                If MainFormTabControl.IsDisposed = False AndAlso MainFormTabControl.Disposing = False Then
                    If MainFormTabControl.TabPages.Contains(TabTaskDetails) Then
                        MainFormTabControl.SelectTab(TabTaskDetails)
                        RaiseEvent TabTaskChanged(TabPages.TabTaskDetails)
                    End If
                End If

                RemoveHandler UcDocumentsParent.TabIndexChanged, AddressOf DockPanelTasks_SelectContent
                AddHandler UcDocumentsParent.TabIndexChanged, AddressOf DockPanelTasks_SelectContent

                RaiseEvent OpenedTasksCountChanged(UcDocumentsParent.TabCount)

                'Vuelvo a ejecutar la logica de habilitacion/deshabilitacion de regla por que el paso anterior
                'me esta volviendo a mostrar una regla posiblemente oculta, en caso de corregir el error comentar la linea
                ztc.GetStatesOfTheButtonsRule()
            Catch ex As System.ComponentModel.Win32Exception
                ZClass.raiseerror(ex)
            Catch ex As System.Threading.ThreadAbortException
            Catch ex As Threading.SynchronizationLockException
            Catch ex As System.Threading.ThreadInterruptedException
            Catch ex As System.Threading.ThreadStartException
            Catch ex As System.Threading.ThreadStateException
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                Cursor = Cursors.Default
                taskbs = Nothing
                If ztc IsNot Nothing Then
                    ztc.Enabled = True
                    If ztc.TabDoc IsNot Nothing Then
                        ztc.TabDoc.EnableForm(True)
                    End If
                End If
            End Try
        End Sub

        Private Sub ClosingTaskInTaskDetails()
            If UcDocumentsParent IsNot Nothing Then
                RaiseEvent OpenedTasksCountChanged(UcDocumentsParent.TabCount)
            End If
        End Sub

        Private Sub ShowDocumentInTaskViewerLazy(ByRef ParentTaskId As Int64, ByVal Result As IResult)
            Cursor = Cursors.WaitCursor
            Dim ztc As UCTaskViewer = Nothing
            Dim taskbs As WFTaskBusiness
            Try
                taskbs = New WFTaskBusiness()
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargo el UCTaskViewer " & Now.ToString)
                RemoveHandler UcDocumentsParent.TabIndexChanged, AddressOf DockPanelTasks_SelectContent

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Busco si esta abierto el tab del UCTaskViewer " & Now.ToString)
                ztc = GetTaskViewer(ParentTaskId)

                'Si no existe la solapa la creo, sino la actualizo...
                If (IsNothing(ztc)) Then
                    Dim task As ITaskResult

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No estaba abierto el UCTaskViewer " & Now.ToString)
                    task = WFTaskBusiness.GetTaskByTaskIdAndDocTypeId(ParentTaskId, Result.DocTypeId, 0)

                    'Verifica la existencia de la tarea
                    If task Is Nothing Then
                        ShowResult(Result)
                        Exit Sub
                    End If

                    ' Carga el contenedor de tarea...
                    LoadTaskDetailsdContent(task)

                    ' instancia un visor de tarea...
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Instancio el UCTaskViewer " & Now.ToString)
                    ztc = New UCTaskViewer(task, CurrentUserId, Me)
                    RemoveHandler ztc.currentContextMenu.ItemClicked, AddressOf currentContextMenuClick
                    AddHandler ztc.currentContextMenu.ItemClicked, AddressOf currentContextMenuClick
                    RemoveHandler ztc.currentContextMenuItemClicked, AddressOf currentContextMenuClick
                    AddHandler ztc.currentContextMenuItemClicked, AddressOf currentContextMenuClick
                    RemoveHandler ztc.ClosingTask, AddressOf ClosingTaskInTaskDetails
                    AddHandler ztc.ClosingTask, AddressOf ClosingTaskInTaskDetails

                    ztc.Dock = DockStyle.Fill
                    ztc.TabDoc.EnableForm(False)

                    ztc.Enabled = False
                    '[Sebastian 13-11-2009] se fuerza que el tab que se visualize primero sea el de tareas
                    ztc.TabControl1.SelectedTab = ztc.TabDoc

                    ' agrega el vosir al contenedor..
                    DockTaskDetails.Controls.Add(ztc)

                    ztc.TaskResult.EditDate = Now()
                Else
                    ztc.TabDoc.EnableForm(False)
                    ztc.Enabled = False
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Selecciono el tab " & Now.ToString)
                If UcDocumentsParent.IsDisposed = False AndAlso UcDocumentsParent.Disposing = False Then
                    If ztc IsNot Nothing AndAlso ztc.IsDisposed = False AndAlso ztc.Parent.Disposing = False Then
                        If UcDocumentsParent.TabPages.Contains(DirectCast(ztc.Parent, ZTaskContent)) Then
                            UcDocumentsParent.SelectTab(DirectCast(ztc.Parent, ZTaskContent))
                        End If
                    End If
                End If

                If MainFormTabControl.IsDisposed = False AndAlso MainFormTabControl.Disposing = False Then
                    If MainFormTabControl.TabPages.Contains(TabTaskDetails) Then
                        MainFormTabControl.SelectTab(TabTaskDetails)
                        RaiseEvent TabTaskChanged(TabPages.TabTaskDetails)
                    End If
                End If

                RemoveHandler UcDocumentsParent.TabIndexChanged, AddressOf DockPanelTasks_SelectContent
                AddHandler UcDocumentsParent.TabIndexChanged, AddressOf DockPanelTasks_SelectContent

                'Ejecuto la carga y la ejecucion de las doenable
                'ztc.SetAsignedTo(ztc.TaskResult)

                'Vuelvo a ejecutar la logica de habilitacion/deshabilitacion de regla por que el paso anterior
                'me esta volviendo a mostrar una regla posiblemente oculta, en caso de corregir el error comentar la linea
                ztc.GetStatesOfTheButtonsRule()

                'Dim T As New Threading.Thread(AddressOf SelectStepAsync)
                'T.Start(stepId)


                Dim resultInserted As IResult = Results_Business.GetResult(Result.ID, Result.DocTypeId)
                ztc.ReloadAsociatedResult(resultInserted, True)
                RaiseEvent OpenedTasksCountChanged(UcDocumentsParent.TabCount)

            Catch ex As Exception
                ZClass.raiseerror(ex)
            Catch ex As System.Threading.ThreadAbortException
            Catch ex As Threading.SynchronizationLockException
            Catch ex As System.Threading.ThreadInterruptedException
            Catch ex As System.Threading.ThreadStartException
            Catch ex As System.Threading.ThreadStateException
            Finally
                Cursor = Cursors.Default
                If Not IsNothing(taskbs) Then
                    taskbs = Nothing
                End If
                If Not IsNothing(ztc) Then
                    ztc.Enabled = True
                    ztc.TabDoc.EnableForm(True)
                End If
            End Try
        End Sub

#Region "grilla"
        Public Event AsocInsertRelationedResult(ByVal result As Result, ByVal relationId As Int32)
        Public Event AsocCambiarNombre(ByRef Result As Result)
        Public Event AsocEliminar(ByVal Results() As Result)
        Public Event AsocExportarAExcel(ByRef Result As Result)
        Public Event TabTaskChanged(ByVal Tab As TabPages)
        Public Event OpenedTasksCountChanged(ByVal count As Int32)

        Friend Sub _ExportarAExcel(ByRef Result As Result)
            RaiseEvent AsocExportarAExcel(Result)
        End Sub

        Friend Sub _AddRelationedResult(ByVal result As Result, ByVal relationId As Int32)
            RaiseEvent AsocInsertRelationedResult(result, relationId)
        End Sub
        Friend Sub _CambiarNombre(ByRef Result As Result)
            RaiseEvent AsocCambiarNombre(Result)
        End Sub
#End Region


        Public Sub ShowInsertedResult(result As Result, openTask As String)
            lblBreadCrumb.Text = result.Name
            ControllerTask.ShowResultsGrid()
            ToolStripContainerTasks.TopToolStripPanel.Visible = False
            If Not String.IsNullOrEmpty(openTask) Then
                ControllerTask.tabResults.DontOpenTaskAfterAddToWF = openTask
            End If

            ControllerTask.tabResults.ShowResult(result)
            'ControllerTask.tabResults.ShowFormFullScreen()

        End Sub

        Private Function ShowTask(ByVal Task As ITaskResult, Optional ByVal toPrint As Boolean = False) As UCTaskViewer
            If Task IsNot Nothing Then
                Dim ztc As UCTaskViewer = Nothing
                Try
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargo el UCTaskViewer " & Now.ToString)
                    RemoveHandler UcDocumentsParent.TabIndexChanged, AddressOf DockPanelTasks_SelectContent

                    ' Toma un contenedor de tarea para el result seleccionado...
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Busco si esta abierto el tab del UCTaskViewer " & Now.ToString)
                    ztc = GetTaskViewer(Task.TaskId)

                    'Si no existe la solapa la creo, sino la actualizo...
                    If (IsNothing(ztc)) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No estaba abierto el UCTaskViewer " & Now.ToString)
                        ' Carga el contenedor de tarea...
                        '[Sebastian 04-06-2009] se agrego cast para salvar el warning
                        LoadTaskDetailsdContent(Task)
                        '  instancia un visor de tarea...
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Instancio el UCTaskViewer " & Now.ToString)

                        '(pablo)
                        'valido si la tarea a abrir contiene un excel
                        If Task.IsExcel Then
                            'valido si la tarea abierta ya se encuentra
                            'abierta en resultados, si es asi entonces cierro
                            'ese tab para poder abrirla en tareas
                            ZambaCore.HandleModule(ResultActions.CloseDocument, Task, Nothing, Nothing)
                        End If

                        ztc = New UCTaskViewer(Task, CurrentUserId, Me)
                        RemoveHandler ztc.currentContextMenu.ItemClicked, AddressOf currentContextMenuClick
                        AddHandler ztc.currentContextMenu.ItemClicked, AddressOf currentContextMenuClick
                        RemoveHandler ztc.currentContextMenuItemClicked, AddressOf currentContextMenuClick
                        AddHandler ztc.currentContextMenuItemClicked, AddressOf currentContextMenuClick
                        RemoveHandler ztc.ClosingTask, AddressOf ClosingTaskInTaskDetails
                        AddHandler ztc.ClosingTask, AddressOf ClosingTaskInTaskDetails

                        ztc.Enabled = False
                        ztc.Dock = DockStyle.Fill
                        ztc.TabDoc.EnableForm(False)

                        DockTaskDetails.Controls.Add(ztc)

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Selecciono el tab " & Now.ToString)
                        UcDocumentsParent.SelectTab(DirectCast(ztc.Parent, ZTaskContent))

                        ztc.TaskResult.EditDate = Now()
                    Else
                        ztc.TabDoc.EnableForm(False)
                        ztc.Enabled = False
                        UcDocumentsParent.SelectTab(DirectCast(ztc.Parent, ZTaskContent))
                    End If

                    '(pablo)
                    'valido que el tab 'tareas' se encuentre dentro de TbDiagrama
                    If OpenInFullScreen = False Then
                        MainFormTabControl.SelectTab(TabTaskDetails)
                        RaiseEvent TabTaskChanged(TabPages.TabTaskDetails)
                    Else
                        extVis2.Activate()
                    End If

                    RemoveHandler UcDocumentsParent.TabIndexChanged, AddressOf DockPanelTasks_SelectContent
                    AddHandler UcDocumentsParent.TabIndexChanged, AddressOf DockPanelTasks_SelectContent

                    RaiseEvent OpenedTasksCountChanged(UcDocumentsParent.TabCount)


                    'Ejecuto la carga y la ejecucion de las doenable
                    'ML no hace falta este llamado, ya que con el constructor del taskviewer ya se llega al setasignedto ztc.SetAsignedTo()
                    'Vuelvo a ejecutar la logica de habilitacion/deshabilitacion de regla por que el paso anterior
                    'me esta volviendo a mostrar una regla posiblemente oculta, en caso de corregir el error comentar la linea
                    ztc.GetStatesOfTheButtonsRule()
                    ztc.Viewer.IsMaximize = OpenInFullScreen
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    Return Nothing
                Finally
                    If Not IsNothing(ztc) Then
                        ztc.TabDoc.EnableForm(True)
                        ztc.Enabled = True
                    End If
                End Try
                If ztc IsNot Nothing AndAlso toPrint Then
                    Return ztc
                End If
                Return Nothing
            End If
        End Function

        Friend Sub ClearGrid()
            lblBreadCrumb.Text = String.Empty
            ControllerTask.tabResults.UCFusion2.GridView.DataSource = Nothing
        End Sub

        ''' <summary>
        ''' Método que sirve para viusalizar la tarea sobre la que se hizo doble click
        ''' </summary>
        ''' <param name="TaskId"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Marcelo]	13/03/2009	Created   Se recargan los forms de los docs asociados
        ''' </history>
        Friend Sub CloseTaskViewer(ByVal _TaskResult As TaskResult, ByVal refreshGrid As Boolean)
            Try
                If Disposing = False Then
                    If refreshGrid = True Then
                        ControllerTask.UCTaskGrid.UpdateTask(_TaskResult)
                    End If
                    If Boolean.Parse(UserPreferences.getValue("UpdateAsocWhenCloseTask", UPSections.UserPreferences, True)) = True Then
                        If UcDocumentsParent.TabPages.Count > 1 Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Refresca las tareas abiertas asociadas a la tarea a cerrar")

                            'Martin Invierto la busqueda de los asociados para actualizar, buscando primero los tabs abiertos y valido sobre cada uno si es asociado o no.
                            For Each ztc As ZTaskContent In UcDocumentsParent.TabPages
                                If Not ztc Is Nothing AndAlso Not ztc.TaskResult Is Nothing Then
                                    Dim tempResult As IResult = ztc.TaskResult
                                    If DocAsociatedBusiness.AreResultsAsociated(_TaskResult, tempResult) = True AndAlso _TaskResult.ID <> tempResult.ID Then

                                        'todo obtener todos los ids de los documentos asociados
                                        '   If Not IsNothing(ztc) Then
                                        refreshTask(ztc.TaskViewer, Nothing)
                                        'End If
                                    End If
                                    tempResult = Nothing
                                End If
                            Next
                        End If
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Método que sirve para visualizar la tarea sobre la que se hizo doble click o recargarla
        ''' </summary>
        ''' <param name="TaskId"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Marcelo]	13/03/2009	Created   Se recargan los forms de los docs asociados
        ''' </history>
        Public Sub ReloadTask(ByRef _TaskResult As TaskResult, ByVal refreshGrid As Boolean)
            If Not IsNothing(Me) AndAlso Disposing = False AndAlso Not IsNothing(_TaskResult) Then
                Try
                    SuspendLayout()
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizo la Tarea a Nivel Interfaz de Usuario")
                    'If refreshGrid = True Then
                    '    ControllerTask.UCTaskGrid.UpdateTask(_TaskResult)
                    'End If
                    Dim tv As TasksCtls.UCTaskViewer
                    tv = GetTaskViewer(_TaskResult.TaskId)
                    refreshTask(tv, _TaskResult)
                    'RefreshWFs()
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                Finally
                    ResumeLayout()
                End Try
            End If
        End Sub

        Private Sub AsocDocOpenTask(ByRef Result As Result)
            RaiseEvent DLoadIndexerEvent(Result)
        End Sub

        Dim DockTaskDetails As ZTaskContent
        Dim CantPdfs As Int32
        Private Sub LoadTaskDetailsdContent(ByRef task As ITaskResult)
            Try
                If task IsNot Nothing AndAlso UcDocumentsParent IsNot Nothing Then
                    'Creo una nueva solapa
                    DockTaskDetails = New ZTaskContent(task)
                    DockTaskDetails.Text = task.Name
                    UcDocumentsParent.TabPages.Add(DockTaskDetails)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        Private Function GetTaskViewer(ByVal TaskId As Int64) As UCTaskViewer
            Try
                If UcDocumentsParent IsNot Nothing Then
                    For Each ztc As ZTaskContent In UcDocumentsParent.TabPages
                        If ztc.TaskResult.TaskId = TaskId AndAlso Not ztc.TaskViewer Is Nothing Then
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se encontro el tab con TaskId:" & TaskId)
                            Return ztc.TaskViewer
                        End If
                    Next
                End If
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "No se encontro el tab con TaskId:" & TaskId)

                Return Nothing
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return Nothing
            End Try
        End Function

#End Region

#Region "TaskController"
        Public ControllerTask As Controller

        Private Sub LoadController()

            ControllerTask = New Controller(CurrentUserId)
            RemoveHandler ControllerTask.currentContextMenuItemClicked, AddressOf currentContextMenuClick
            AddHandler ControllerTask.currentContextMenuItemClicked, AddressOf currentContextMenuClick

            ControllerTask.Dock = DockStyle.Fill
            ToolStripContainerTasks.ContentPanel.Controls.Add(ControllerTask)

            RemoveHandler ControllerTask.UCTaskGrid.GridTasksSelected, AddressOf TaskSelected
            RemoveHandler ControllerTask.tabResults.TabInsertFormClosed, AddressOf TabInsertFormClosed
            RemoveHandler ControllerTask.UCTaskGrid.TaskDoubleClick, AddressOf ShowTask
            RemoveHandler ControllerTask.UCTaskGrid.NotAnyResultSelected, AddressOf ClearUserActions
            RemoveHandler ControllerTask.UCTaskGrid.NotAnyResultSelected, AddressOf LoadDynamicbuttonsOverGrid
            RemoveHandler ControllerTask.UCTaskGrid.visibleOrInvisibleButtonsRule, AddressOf visibleOrInvisibleButtonsRule
            RemoveHandler ControllerTask.tabResults.ShowTask, AddressOf ShowTaskViewer

            AddHandler ControllerTask.tabResults.TabInsertFormClosed, AddressOf TabInsertFormClosed
            AddHandler ControllerTask.UCTaskGrid.GridTasksSelected, AddressOf TaskSelected
            AddHandler ControllerTask.UCTaskGrid.TaskDoubleClick, AddressOf ShowTask
            AddHandler ControllerTask.UCTaskGrid.NotAnyResultSelected, AddressOf ClearUserActions
            AddHandler ControllerTask.UCTaskGrid.NotAnyResultSelected, AddressOf LoadDynamicbuttonsOverGrid
            AddHandler ControllerTask.UCTaskGrid.visibleOrInvisibleButtonsRule, AddressOf visibleOrInvisibleButtonsRule
            AddHandler ControllerTask.tabResults.ShowTask, AddressOf ShowTaskViewer
        End Sub
#Region "UCContextMenuClick"


        Private Sub currentContextMenuClick(Action As String, listResults As List(Of IResult), ContextMenuContainer As IMenuContextContainer)
            Try
                Dim mesg As String = "No puede seleccionar mas de un item para realizar esta accion"
                Select Case CStr(Action)
                    Case "GenerateLink"
                        If listResults.Count > 1 Then
                            MessageBox.Show(mesg, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            _GenerarLink(listResults(0), True)
                        End If
                    Case "GenerateLinkWeb"
                        If listResults.Count > 1 Then
                            MessageBox.Show(mesg, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            _GenerarLink(listResults(0), False)
                        End If
                    Case "Property"
                        If listResults.Count > 1 Then
                            MessageBox.Show(mesg, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            _ShowResultProperties(listResults(0))
                        End If
                    Case "Delete"
                        _Eliminar(listResults, ContextMenuContainer)
                    Case "ExportToPDF"
                        _ConvertToPdf(listResults)
                    Case "AddToWF"
                        _AdjuntarAWF(listResults)
                    Case "MoveCopyDoc"
                        _MoverCopiarDocumento(listResults)
                    Case "EditTIF"
                        If listResults.Count > 1 Then
                            MessageBox.Show(mesg, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            editarPag(listResults(0))
                        End If
                    Case "History"
                        If listResults.Count > 1 Then
                            MessageBox.Show(mesg, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            _Historial(listResults(0))
                        End If
                    Case "PrintIndexs"
                        If listResults.Count > 1 Then
                            MessageBox.Show(mesg, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            _Imprimir(listResults, Print.LoadAction.ShowPreview)
                        End If
                End Select

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub


#Region "EditarPag"
        Private Sub editarPag(ByRef R As Result)
            Cursor = Cursors.WaitCursor
            Try
                If IsNothing(iplugin) Then
                    iplugin = New Form2
                    iplugin.Show()
                    AddHandler iplugin.Save, AddressOf Save
                    AddHandler iplugin.CloseDocument, AddressOf Close
                    '    AddHandler iplugin.Update, AddressOf Update
                End If

                If Not IsNothing(R) Then
                    iplugin.initialize(R)
                    iplugin.play()
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Cursor = Cursors.Default
        End Sub
#End Region
#Region "Eliminar"
        Private Sub _Eliminar(ByVal Results As List(Of IResult), ContextMenuContainer As IMenuContextContainer)
            Try
                If Results.Count = 0 Or IsNothing(Results(0)) Then
                    MessageBox.Show("DEBE SELECCIONAR UN DOCUMENTO")
                    Exit Sub
                End If
                For Each r As Result In Results
                    If r.HasVersion = 1 AndAlso UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.DeleteVersions, r.DocType.ID) = False Then
                        MessageBox.Show("NO TIENE PERMISO PARA ELIMINAR UNO O MAS DE LOS DOCUMENTOS SELECCIONADOS")
                        Exit Sub
                    End If
                Next

                If MessageBox.Show("¿DESEA ELIMINAR LOS DOCUMENTOS SELECCIONADOS?", "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                    Dim DeletedResultsNames As New List(Of String)

                    For Each result As Result In Results
                        'MOD:1
                        If result.HasVersion = 1 Then
                            If result.RootDocumentId = 0 Then
                                'si el documento es root (no tiene padres) primero debe eliimnar sus versiones
                                'hijas y luego eliminar este - HasVersion Va a cambiar a 0
                                MessageBox.Show("No Puede eliminar un Documento Padre del sistema de versiones, Elimine sus versiones y luego esté", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                'si tiene versiones hijas no solo hay que eliminar el result
                                'tambien hay que actualizar los results hijos
                                'para que no queden huerfanos

                                DeletedResultsNames.Add(result.Name)
                                DeleteResult(result, False, False, True, True)
                                Results_Business.DeleteResultFromWorkflows(result.ID)

                            End If
                        Else
                            If result.HasVersion = 0 Then
                                DeleteResult(result, False, False, True, True)
                                Results_Business.DeleteResultFromWorkflows(result.ID)
                            Else
                                MessageBox.Show("No tiene permisos para eliminar documentos Versionados", "Zamba", MessageBoxButtons.OK)
                            End If
                        End If
                    Next
                    WFTreeControlForClient.RefreshWorkflow(-1, -1, False)
                    ContextMenuContainer.RefreshResults()

                    MessageBox.Show("Se han eliminado los siguientes elementos: " & String.Join(",", DeletedResultsNames), "Zamba", MessageBoxButtons.OK)



                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
                MessageBox.Show("OCURRIO UN ERROR AL ELIMINAR LOS DOCUMENTOS", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

        End Sub


        Public Sub DeleteResult(ByVal Result As Result, ByVal Ask As Boolean, ByVal conf As Boolean, ByVal Forced As Boolean, ByVal deletefile As Boolean)
            Try
                If Forced = False AndAlso UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.Delete, Result.DocTypeId) = False Then
                    MessageBox.Show("USTED NO TIENE PERMISO PARA ELIMINAR O MOVER ESTE DOCUMENTO", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If (Ask = True OrElse conf = True) Then
                    If conf = False Then
                        If MessageBox.Show("¿DESEA ELIMINAR EL DOCUMENTO SELECCIONADO?", "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
                            Exit Sub
                        End If
                    End If
                End If

                Dim DocumentId As Int64 = Result.ID
                Dim DocName As String = Result.Name
                Try
                    NotesBusiness.DeleteNotes(Result.ID)
                Catch ex As Exception
                    ZClass.raiseerror(ex)

                End Try
                Try
                    ControllerTask.tabResults.CloseDocumentViewerTab(Result)
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
                End Try
                Try
                    If Result.HasVersion = 1 Then

                        'if have child versions there must update childs
                        'y asignarle como documento padre al padre del eliminado
                        'and asign like father document the last document's father
                        'not to be an orphan and give you the version number corresponding
                        Results_Business.UpdateResultsVersionedDataWhenDelete(Result.DocTypeId, Result.ParentVerId, Result.ID, Result.RootDocumentId)
                    End If

                    'Diego : Obtengo el parentid de base porque en la coleccion de results
                    '           puede venir a desactualizado ( ya que se modifica en ejecucion)
                    Dim parentid As Int64
                    parentid = Results_Business.GetParentVersionId(Result.DocTypeId, Result.ID)

                    Results_Business.Delete(Result, deletefile)
                    If Result.ParentVerId > 0 AndAlso Results_Business.CountChildsVersions(Result.DocTypeId, Result.ParentVerId) = 0 Then
                        ' en caso de que se elimine un child y haya que actualizar el padre (HasVersion = 0)
                        Results_Business.UpdateLastResultVersioned(Result.DocTypeId, parentid)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                'borro de dock panels
                Try
                    UserBusiness.Rights.SaveAction(DocumentId, ObjectTypes.Documents, RightsType.Delete, DocName)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                If Ask = True And conf = False Then MessageBox.Show("EL DOCUMENTO SE ELIMINO EXITOSAMENTE", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region
#Region "GenerarLink"
        Delegate Sub DGenerateLink(ByRef Result As Result, ByVal TResult As Boolean)

        Private Sub _GenerarLink(ByRef Result As Result, ByVal TResult As Boolean)
            Invoke(New DGenerateLink(AddressOf GenerateLink), Result, TResult)
        End Sub
        ''' <summary>
        ''' '[Sebastian] 09-06-2009 se agrego cast para salver warning
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history> [Ezequiel] 27/01/09 Modified - Se agrego la opcion de mostrar link Web </history>
        Private Sub GenerateLink(ByRef Result As Result, ByVal TResult As Boolean)
            Dim text As String = IIf(TResult, Results_Business.GetLinkFromResult(Result), ZOptBusiness.GetValue("WebAccessDocIdPath") & "?docid=" & Result.ID & "&doctid=" & Result.DocTypeId).ToString
            Dim FrmTextbox As New FrmTextBox(text)
            '[Sebastian] 09-06-2009 se agrego cast para salver warning
            'Dim line As Int16 = CShort(1 + Int(text.Length *11.1 / 459.2))
            '32 = Title Height, 18 = line Height, 459.2 = line Width
            FrmTextbox.Size = New Size(CType(IIf(TResult, 459.2, 615), Integer), 100) '32 + line * 18)
            FrmTextbox.ShowDialog()
            FrmTextbox.Dispose()
            FrmTextbox = Nothing
            'MessageBox.Show("SE COPIO EL LINK GENERADO AL PORTAPAPELES DE WINDOWS", "ZAMBA")
        End Sub
#End Region
#Region "AdjuntarAWF"
        Private Sub _AdjuntarAWF(ByVal _Results As List(Of IResult))
            If _Results.Count = 0 Then
                MessageBox.Show("DEBE SELECCIONAR UN DOCUMENTO")
                Exit Sub
            End If
            Dim FrmDocWF As New FrmAddToWorkFlow(_Results)
            FrmDocWF.ShowDialog()
        End Sub
#End Region
#Region "ConvertToPDF"
        Friend Sub _ConvertToPdf(ByVal ResultArray As List(Of IResult))
            Dim Results As New Generic.List(Of Result)

            For Each CurrentResult As Result In ResultArray
                If CurrentResult.IsImage Then
                    Results.Add(CurrentResult)
                End If
            Next

            If Results.Count = 0 Then Exit Sub

            Dim frmSelectDirectory As New FolderBrowserDialog
            frmSelectDirectory.Description = "Carpeta donde guardar los PDFs"
            frmSelectDirectory.ShowDialog()

            Dim PdfFolderPath As String = frmSelectDirectory.SelectedPath

            If String.IsNullOrEmpty(PdfFolderPath) Then
                MessageBox.Show("DEBE SELECCIONAR UNA CARPETA DONDE GUARDAR LOS PDFS GENERADOS")
                Exit Sub
            End If

            For Each CurrentResult As Result In Results
                Results_Business.ConvertToPdfFile(CurrentResult, PdfFolderPath, CantPdfs)
            Next

            If CantPdfs = 1 Then
                MessageBox.Show("EL DOCUMENTOS SE EXPORTO A PDF EXITOSAMENTE")
            ElseIf CantPdfs > 1 Then
                MessageBox.Show("SE EXPORTARON " & CantPdfs & " DOCUMENTOS A PDF EXITOSAMENTE")
            End If

        End Sub
#End Region 'Esta el export to pdf, hay que ver si hacen lo mismo
#Region "Mover Documentos"
        Private Sub _MoverCopiarDocumento(ByRef listResults As List(Of IResult))
            Try
                For Each Result As IResult In listResults
                    Dim newresult As NewResult
                    If Not IsNothing(Result.FullPath) Then
                        newresult = New NewResult(Result.FullPath)
                    Else
                        newresult = New NewResult(Result.Doc_File)
                    End If
                    newresult.DocType = Result.DocType
                    newresult.Indexs = Result.Indexs

                    Dim UC As New FrmMove(newresult, Result)
                    RemoveHandler ClsMove.Eliminar, AddressOf DeleteResult
                    AddHandler ClsMove.Eliminar, AddressOf DeleteResult
                    UC.ShowDialog()
                    If UC.Moved Then
                        If Not IsNothing(ControllerTask.tabResults) Then
                            Try
                                If Not IsNothing(ControllerTask.tabResults.SelectedViewer) Then ControllerTask.tabResults.SelectedViewer.CloseDocument()
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try

                            WFTreeControlForClient.RefreshWorkflow(-2, -2, False)
                            'REFRESHUCTASKPANEL
                            'RefreshTasksPanel()
                        End If
                    End If

                Next
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region
#Region "Propiedades"
        Public Sub _ShowResultProperties(ByRef Result As Result)
            Dim DocInfo As New Zamba.Viewers.FrmDocumentDetails(Result)
            Try
                If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.FrmDocProperty, RightsType.View) = False Then
                    MessageBox.Show("Usted no tiene permiso para utilizar esta función", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                DocInfo = New Zamba.Viewers.FrmDocumentDetails(Result)
                DocInfo.ShowDialog()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                DocInfo.Dispose()
                DocInfo = Nothing
            End Try
        End Sub
#End Region
#Region "Historial"
        Friend Sub _Historial(ByRef Result As IResult)
            If IsNothing(Result) = False Then
                If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.FrmDocHistory, RightsType.View) = False Then
                    MessageBox.Show("Usted no tiene permiso para utilizar esta función", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim FrmActions As Zamba.AdminControls.UCActions = Nothing
                Try
                    FrmActions = New Zamba.AdminControls.UCActions(CurrentUserId)
                    FrmActions.ShowDocumentHistory(Result.ID)
                    FrmActions.ShowDialog()
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    MessageBox.Show("Ocurrio un error al mostrar el historial del documento " & ex.ToString, "Zamba - Historial de Documento", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    FrmActions.Dispose()
                    FrmActions = Nothing
                End Try
            End If
        End Sub
#End Region
#Region "Imprimir"
        Public Sub _Imprimir(ByVal results As List(Of IResult), ByVal loadAction As Print.LoadAction)
            Try
                'Dim r As Object = results

                Dim resultsToPrint As New List(Of IPrintable)
                For Each r As IResult In results
                    resultsToPrint.Add(r)
                Next

                'Using frmPrinter As New frmchooseprintmode(DirectCast(r, List(Of IPrintable)), loadAction)
                Using frmPrinter As New frmchooseprintmode(resultsToPrint, loadAction)

                    RemoveHandler frmPrinter.PrintVirtual, AddressOf PrintVirtualDocument
                    AddHandler frmPrinter.PrintVirtual, AddressOf PrintVirtualDocument

                    If frmPrinter.ShowDialog = DialogResult.OK Then
                        ' Se actualiza el U_TIME del usuario en UCM y se registra la acción (de que presiono el botón Imprimir) en la tabla USER_HST antes
                        ' de que aparezca el formulario de impresión
                        UserBusiness.Rights.SaveAction(CurrentUserId, ObjectTypes.Documents, RightsType.Print, "Se imprimio formulario")
                    End If

                    RemoveHandler frmPrinter.PrintVirtual, AddressOf PrintVirtualDocument

                End Using
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub PrintVirtualDocument(ByVal r As IResult)
            SyncLock _sync
                Try
                    ''Obtengo la tarea
                    'Dim Task As ITaskResult = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(r.ID, 0)
                    ''Abro o me paro en la tarea y obtengo el taskViewer
                    'Dim taskViewer As UCTaskViewer = ShowTask(Task, True)

                    'If taskViewer IsNot Nothing Then
                    '    taskViewer.TabDoc.PrintDocumentWB()
                    'End If

                    'Aca hacer lo del form de preview

                    Dim previewForm = New PreviewForm(r, PreviewType.PrintDocument)
                    previewForm.ShowDialog()
                    If previewForm.DialogResult = DialogResult.OK Then
                        r.Html = previewForm.frmbrowser.GetHtml
                    End If

                    Dim fileName As String = r.Name & "-temp" & (New Random).Next(1, 9999).ToString & ".html"
                    For Each invalidChar As Char In Path.GetInvalidFileNameChars
                        fileName = fileName.Replace(invalidChar, String.Empty)
                    Next
                    r.HtmlFile = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp").FullName & "\" & fileName

                    Try
                        If File.Exists(r.HtmlFile) Then
                            File.Delete(r.HtmlFile)
                        End If
                    Catch
                    End Try

                    Dim form As ZwebForm = FormBusiness.GetShowAndEditForms(r.DocType.ID)(0)
                    If File.Exists(form.Path.Replace(".html", ".mht")) Then
                        Try
                            Using write As New StreamWriter(r.HtmlFile)
                                write.AutoFlush = True
                                Dim reader As New StreamReader(form.Path)
                                Dim mhtstring As String = reader.ReadToEnd()
                                write.Write(mhtstring.Replace("<Zamba.Html>", r.Html))
                            End Using
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                        r.HtmlFile = r.HtmlFile
                    Else
                        Try
                            Using write As New StreamWriter(r.HtmlFile)
                                write.AutoFlush = True
                                write.Write(r.Html)
                            End Using
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    End If



                Catch ex As Exception
                    ZClass.raiseerror(ex)
                Finally
                    ControllerTask.UCTaskGrid.GridView.NewGrid.Enabled = True
                End Try
            End SyncLock
        End Sub
#End Region
#End Region


        Private Sub TabInsertFormClosed(tempId As Integer)

            WFTreeControlForClient.RemoveInsertNode(tempId)

        End Sub

        Friend Sub ShowController(ByRef wfstepId As Int64, ByVal refreshFilters As Boolean, ByVal showTaskListTab As Boolean, ByVal wfStateId As Int64, ShowGrid As Boolean, ByVal wfChanged As Boolean)

            Try
                Dim StepName As String = WFStepBusiness.GetStepNameById(wfstepId)
                lblBreadCrumb.Text = "Etapa: " & StepName

                ZTrace.WriteLineIf(ZTrace.IsInfo, "8 " & Now.ToString)

                'Los filtros son por grupo de usuarios o usuarios, aun no son por etapas, entonces son los mismos para todas las etapas
                'se agrega una opcion en el userconfig, que por defecto hara que se mantengan los filtros entre etapas.
                ControllerTask.UCTaskGrid.ShowTasksExtern(wfstepId, refreshFilters, wfStateId, wfChanged)
                If ShowGrid Then
                    ControllerTask.ShowTaskGrid()
                    ToolStripContainerTasks.TopToolStripPanel.Visible = True
                End If

                'Verifica si debe o no mostrar la solapa de la grilla de tareas.
                If showTaskListTab Then
                    MainFormTabControl.SelectedTab = Parent
                End If

                'Limpio la barra de acciones de usuario 
                ClearUserActions()

                LoadDynamicbuttonsOverGrid()
                WFTreeControlForClient.LoadDynamicButtons()

                ZTrace.WriteLineIf(ZTrace.IsInfo, "10 " & Now.ToString)

                RemoveHandler ControllerTask.UCTaskGrid.ShowComment, AddressOf ShowVersionComment
                AddHandler ControllerTask.UCTaskGrid.ShowComment, AddressOf ShowVersionComment

            Catch ex As InvalidOperationException
                ZClass.raiseerror(ex)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                'Se pone para ver si disminuye la memoria consumida cada vez que se cambia de etapa
                GC.Collect()
            End Try

        End Sub

        Private Sub LoadDynamicbuttonsOverGrid()
            'Se cargan los botones dinámicos
            Dim SelectedTaskResult As Generic.List(Of GridTaskResult) = ControllerTask.UCTaskGrid.SelectedTaskResultsExtern(False)

            If SelectedTaskResult IsNot Nothing AndAlso SelectedTaskResult.Count > 0 Then
                GenericRuleManager.LoadDynamicButtons(ToolBar1, 0, True, ButtonPlace.BarraTareas,
                                                  SelectedTaskResult(0).ID, ButtonType.Rule, WFTreeControlForClient.SelectedWfId)
            Else
                GenericRuleManager.LoadDynamicButtons(ToolBar1, 0, True, ButtonPlace.BarraTareas,
                                                  Nothing, ButtonType.Rule, WFTreeControlForClient.SelectedWfId)
            End If
        End Sub

        Friend Sub ShowController(ByRef Search As ISearch, ByVal query As String, ByVal queryCount As String)
            Try
                lblBreadCrumb.Text = Search.Name

                'Pongo la grilla de resultados por delante.
                ControllerTask.ShowResultsGrid()
                'Oculto la toolbar de tareas.
                ToolStripContainerTasks.TopToolStripPanel.Visible = False

                If ControllerTask.tabResults.UCFusion2 IsNot Nothing Then
                    'Limpia la grilla.
                    ControllerTask.tabResults.UCFusion2.FillResults(Nothing, Nothing)

                    ControllerTask.tabResults.UCFusion2.GridView.DisableAllFilters(False)

                    ControllerTask.tabResults.UCFusion2.ResetGrid()

                    'Vuelve a la primer pagina.
                    ControllerTask.tabResults.UCFusion2.LastPage = 0
                    'Obtiene los resultados con la consulta con su conteo
                    Dim dt As DataTable = Zamba.Core.Search.ModDocuments.SearchRows(query, queryCount, 0)

                    'Llena la grilla con los resultados obtenidos
                    ControllerTask.tabResults.UCFusion2.FillResults(dt, Search)
                    WFTreeControlForClient.RefreshSearchNode(dt, Search)
                End If
                MainFormTabControl.SelectedTab = Parent
                'Limpio la barra de acciones de usuario 
                ClearUserActions()
                LoadDynamicbuttonsOverGrid()
            Catch ex As InvalidOperationException
                ZClass.raiseerror(ex)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                'Se pone para ver si disminuye la memoria consumida cada vez que se cambia de etapa
                GC.Collect()
            End Try
        End Sub

        Friend Sub ShowController(ByVal Search As ISearch)
            Try
                lblBreadCrumb.Text = Search.Name
                'Los filtros son por grupo de usuarios o usuarios, aun no son por etapas, entonces son los mismos para todas las etapas
                'se agrega una opcion en el userconfig, que por defecto hara que se mantengan los filtros entre etapas.
                ControllerTask.ShowResultsGrid()

                ToolStripContainerTasks.TopToolStripPanel.Visible = False

                If ControllerTask.tabResults.UCFusion2 IsNot Nothing Then

                    ControllerTask.tabResults.UCFusion2.FillResults(Nothing, Search)
                    ControllerTask.tabResults.UCFusion2.LastPage = 0
                    ControllerTask.tabResults.UCFusion2.ShowTaskOfDT()
                    'ControllerTask.tabResults.ShowGridTab()

                End If

                MainFormTabControl.SelectedTab = Parent
                'Limpio la barra de acciones de usuario 
                ClearUserActions()
                LoadDynamicbuttonsOverGrid()

            Catch ex As InvalidOperationException
                ZClass.raiseerror(ex)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                'Se pone para ver si disminuye la memoria consumida cada vez que se cambia de etapa
                GC.Collect()
            End Try

        End Sub

        Friend Sub ShowController(ByVal IDOfInsertedForm As Integer)
            Dim form As ZwebForm = FormBusiness.GetForm(IDOfInsertedForm)
            lblBreadCrumb.Text = form.Name

            ControllerTask.tabResults.ShowTabOfInsertedForm(IDOfInsertedForm)

        End Sub
        Public Sub addTasksFromResults(ByVal data As DataTable, ByVal Search As ISearch, ByVal taskCount As Integer)
            If Search IsNot Nothing Then
                WFTreeControlForClient.ComeFromSearch = True
                If Not WFTreeControlForClient.SearchNodeExist(Search) Then
                    WFTreeControlForClient.AddSearchNode(Search, taskCount)
                    ControllerTask.ShowResultsGrid()
                    ToolStripContainerTasks.TopToolStripPanel.Visible = False
                    If ControllerTask.tabResults.UCFusion2 IsNot Nothing Then
                        ControllerTask.tabResults.UCFusion2.ResetGrid()
                        ControllerTask.tabResults.UCFusion2.LastPage = 0
                    End If
                    ControllerTask.tabResults.ShowFilesInFolder(data, If(Search IsNot Nothing, Search.Name, String.Empty), Search)
                ElseIf Not WFTreeControlForClient.IsSearchNodeSelected(Search.Name) Then
                    WFTreeControlForClient.SelectSearchNodeByName(Search.Name)
                    ControllerTask.tabResults.ShowFilesInFolder(data, If(Search IsNot Nothing, Search.Name, String.Empty), Search)
                    WFTreeControlForClient.RefreshSearchNode(data, Search)
                Else
                    WFTreeControlForClient.RefreshSearchNode(data, Search)
                End If
                WFTreeControlForClient.Focus()
                WFTreeControlForClient.ComeFromSearch = False
            End If
        End Sub

        ''' <summary>
        ''' Actualiza el control de la tarea
        ''' </summary>
        ''' <param name="ztc"></param>
        ''' <remarks></remarks>
        Private Sub refreshTask(ByRef TV As UCTaskViewer, ByVal TaskResult As ITaskResult)
            If TV IsNot Nothing Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando la tarea")
                Dim selectedIndex As Int32 = TV.TabControl1.SelectedIndex()
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Pagina seleccionada: " & selectedIndex.ToString())
                TV.ReLoadTask(selectedIndex, TaskResult)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Pagina seleccionada luego de la recarga: " & selectedIndex.ToString())

                'Ejecuto la carga y la ejecucion de las doenable
                DirectCast(TV.Parent, ZTaskContent).TaskResult = TV.TaskResult
                TV.IniciarTareaAlAbrir(TV.TaskResult)
                TV.SetAsignedTo()
                TV.SetAsignedAndSituationLabels(TaskResult)

                'Vuelvo a ejecutar la logica de habilitacion/deshabilitacion de regla por que el paso anterior
                'me esta volviendo a mostrar una regla posiblemente oculta, en caso de corregir el error comentar la linea
                TV.GetStatesOfTheButtonsRule()
                TV.TaskResult.EditDate = Now()
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Pagina seleccionada al finalizar: " & selectedIndex.ToString())
            End If
        End Sub

        ''' <summary>
        ''' Cuando se selecciona una tarea de la grilla
        ''' </summary>
        ''' <param name="GridtasksResults"></param>
        ''' <remarks></remarks>
        Private Sub TaskSelected(ByRef GridtasksResults As List(Of GridTaskResult))
            Try
                If GridtasksResults IsNot Nothing AndAlso GridtasksResults.Count > 0 Then
                    Dim Result As Result = Results_Business.GetResult(GridtasksResults(0).ID, GridtasksResults(0).doctypeid)
                    LoadResultRights(Result)
                    LoadUserAction(GridtasksResults)
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Sub


#End Region

#Region "Refresh"

        ''' <summary>
        ''' Método para refrescar los workflows
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    11/09/2008  Modified
        ''' </history>
        Public Sub RefreshWFs(ByVal StepID_from As Long, ByVal stepID_to As String)
            lblBreadCrumb.Text = String.Empty
            If Not isDisposed Then
                Try
                    ' Se refrescan los workflows y sus etapas
                    If WFTreeControlForClient Is Nothing OrElse WFTreeControlForClient.IsDisposed Then
                        LoadResults(False)
                    End If
                    WFTreeControlForClient.RefreshWorkflow(StepID_from, stepID_to, False)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Asigna los steps
        ''' </summary>
        ''' <param name="TaskId"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Tomas] 16/04/2009  Created
        Public Sub SetStep(ByVal TaskId As Int64)
            Try
                WFTreeControlForClient.SetLastStepId(TaskId)
                ' Se limpia la grilla
                ControllerTask.UCTaskGrid.OutLookGridClear()
                ZTrace.WriteLineIf(ZTrace.IsInfo, "12 " & Now.ToString)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub RefreshHandlers()

            RemoveHandler WFTaskBusiness.Distributed, AddressOf RemoveTask
            RemoveHandler WFTaskBusiness.refreshSteps, AddressOf refreshStepsAfterDistribute

            'RemoveHandler WFTaskBusiness.ChangedExpireDate, AddressOf RefreshExpireDate
            RemoveHandler WFTaskBusiness.AsignedAndExpireDate, AddressOf RefreshAsignedTo

            AddHandler WFTaskBusiness.Distributed, AddressOf RemoveTask
            AddHandler WFTaskBusiness.refreshSteps, AddressOf refreshStepsAfterDistribute

            AddHandler WFTaskBusiness.AsignedAndExpireDate, AddressOf RefreshAsignedTo

            'AddHandler WFTaskBusiness.ChangedExpireDate, AddressOf RefreshExpireDate

        End Sub
        Public Sub CloseOrRefreshTaskAfterDistribute(ByRef TaskResult As TaskResult)

            Try
                ControllerTask.ShowTaskGrid()
                ToolStripContainerTasks.TopToolStripPanel.Visible = True

                'Remueve la tarea de la grilla
                ControllerTask.UCTaskGrid.RemoveTaskExtern(TaskResult)

                Dim ztc As UCTaskViewer = GetTaskViewer(TaskResult.TaskId)
                If (Not IsNothing(ztc)) Then

                    Try
                        'Verifica si debe o no cerrar la tarea.
                        If Not ztc.chkCloseTaskAfterDistribute.Checked AndAlso
                           Zamba.Core.RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.Use, CInt(TaskResult.StepId)) Then
                            'Se refresca la tarea completa incluyendo formularios, estados, acciones de usuario, etc.
                            'ztc.RefreshTask(ztc.TaskResult)
                            ztc.RefreshTask(TaskResult)

                            If Not IsNothing(ztc) Then
                                ztc.TabDoc.Dispose()
                                ztc.ReLoadTask(TaskResult)

                                'Ejecuto la carga y la ejecucion de las doenable
                                ztc.SetAsignedTo()
                                ztc.SetAsignedAndSituationLabels(ztc.TaskResult)

                                'Vuelvo a ejecutar la logica de habilitacion/deshabilitacion de regla por que el paso anterior
                                'me esta volviendo a mostrar una regla posiblemente oculta, en caso de corregir el error comentar la linea
                                ztc.GetStatesOfTheButtonsRule()
                                ztc.TaskResult.EditDate = Now()
                            End If

                        Else
                            ztc.CloseTaskViewer(False)
                        End If

                    Catch ex As ObjectDisposedException
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Sub


        ''' <summary>
        ''' Quita la tarea de la grilla y actualiza o cierra el visualizador
        ''' </summary>
        ''' <param name="TaskResult"></param>
        ''' <remarks></remarks>
        Friend Sub RemoveTask(ByVal TaskResult As ITaskResult)
            Try
                If Not IsNothing(TaskResult) AndAlso ControllerTask IsNot Nothing AndAlso ControllerTask.UCTaskGrid IsNot Nothing Then
                    ControllerTask.ShowTaskGrid()
                    ToolStripContainerTasks.TopToolStripPanel.Visible = True

                    ControllerTask.UCTaskGrid.RemoveTaskExtern(DirectCast(TaskResult, TaskResult))

                    Dim ztc As UCTaskViewer = GetTaskViewer(TaskResult.TaskId)

                    If (Not IsNothing(ztc)) Then
                        Try
                            Dim users As Generic.List(Of Long) = UserGroupBusiness.GetUsersIds(ztc.TaskResult.AsignedToId, True)

                            'Verifica si debe o no cerrar la tarea.
                            If Not ztc.chkCloseTaskAfterDistribute.Checked AndAlso
                               (Membership.MembershipHelper.CurrentUser IsNot Nothing AndAlso (ztc.TaskResult.AsignedToId = Membership.MembershipHelper.CurrentUser.ID OrElse users.Contains(Membership.MembershipHelper.CurrentUser.ID))) AndAlso
                               Zamba.Core.RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.Use, CInt(TaskResult.StepId)) Then
                                'Comento el refresh de la tarea, para que solo actualice el nombre de la etapa - MF
                                'Se refresca la tarea completa incluyendo formularios, estados, acciones de usuario, etc.
                                'ztc.RefreshTask(ztc.TaskResult)
                                ztc.SetWFStep()
                                ztc.SetAsignedAndSituationLabels(TaskResult)
                            Else
                                ztc.CloseTaskViewer(False)
                            End If
                        Catch ex As ObjectDisposedException
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Metodo que permite cerrar una tarea sin eliminarla de la grilla.
        ''' </summary>
        ''' <param name="TaskResult"></param>
        ''' <remarks></remarks>
        Public Sub CloseTask(ByRef TaskResultID As Int64)
            Try
                Dim ztc As UCTaskViewer = GetTaskViewer(TaskResultID)

                If (Not IsNothing(ztc)) Then
                    If Not IsNothing(ztc.TaskResult) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cerrando tarea: " & ztc.TaskResult.Nombre)
                    End If
                    Try
                        ztc.CerrarTab()
                    Catch ex As ObjectDisposedException
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Sub


        ''' <summary>
        ''' Maximiza/Minimiza todas las tareas
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AdjustFullScreenTasks()
            Try
                For Each _zZTaskContent As ZTaskContent In UcDocumentsParent.TabPages
                    If Not _zZTaskContent.TaskViewer Is Nothing Then
                        _zZTaskContent.TaskViewer.Viewer.IsMaximize = OpenInFullScreen
                    End If
                Next
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Cierra todas las tareas
        ''' </summary>
        ''' <remarks></remarks>
        Public Function CloseTasks() As Boolean
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cerrando todas las tareas")
            For Each _zZTaskContent As ZTaskContent In UcDocumentsParent.TabPages
                If Not _zZTaskContent.TaskViewer Is Nothing Then
                    If _zZTaskContent.TaskViewer.CerrarTab() = False Then
                        Return False
                    End If
                    _zZTaskContent.Dispose()
                End If
            Next
            Return True
        End Function

        Public Function HasTasksOpened() As Boolean
            Dim ActiveTaskViewer As ZTaskContent
            If UcDocumentsParent.TabCount > 0 Then Return True
            Return False
        End Function

        Public Function CloseCurrentTask() As Boolean
            Try
                Dim ActiveTaskViewer As ZTaskContent
                If UcDocumentsParent.SelectedTab IsNot Nothing Then
                    ActiveTaskViewer = DirectCast(UcDocumentsParent.SelectedTab, ZTaskContent)
                    ActiveTaskViewer.TaskViewer.CerrarTab()
                    ActiveTaskViewer.Dispose()
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Método que llama a un método encargado de actualizar los nodos que muestran el nombre de la etapa y la cantidad de tareas
        ''' relacionados con el Distribuir 
        ''' </summary>
        ''' <param name="wfId"></param>
        ''' <param name="oldStepId"></param>
        ''' <param name="newStepId"></param>
        ''' <param name="distributedTasks"></param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    01/09/2008  Created 
        ''' </history>
        Private Sub refreshStepsAfterDistribute(ByVal oldStepID As Long, ByVal newStepID As Long)
            WFTreeControlForClient.RefreshWorkflow(oldStepID, newStepID, False)
        End Sub

        'Private Sub refreshInitialStepOfWF(ByRef TaskResult As TaskResult)

        '    ' Se agrega la tarea a la etapa del workflow actual
        '    WFTaskBusiness.AddTaskToHashTable(TaskResult, TaskResult.WfStep.ID)
        '    ' Actualizar nodo con cantidad de tareas

        'End Sub

        Private Sub RefreshAsignedTo(ByRef TaskResult As TaskResult)
            If TaskResult IsNot Nothing Then
                Try
                    'Actualizo Documents
                    ' Dim ztc As ZTaskContent = Me.GetZTaskContent(Result.id)
                    Dim ztc As UCTaskViewer = GetTaskViewer(TaskResult.TaskId)
                    If ztc IsNot Nothing Then

                        Dim selectedIndex As Int32 = ztc.TabControl1.SelectedIndex()
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Pagina seleccionada: " & selectedIndex.ToString())
                        ztc.ReLoadTask(selectedIndex, TaskResult)

                        ztc.IniciarTareaAlAbrir(TaskResult)

                        ztc.SetAsignedTo()
                        ztc.SetAsignedAndSituationLabels(ztc.TaskResult)
                    End If

                    If ControllerTask IsNot Nothing AndAlso ControllerTask.UCTaskGrid IsNot Nothing AndAlso ControllerTask.UCTaskGrid.GridType Then
                        'Actualizo Grid
                        ControllerTask.ShowTaskGrid()
                        ToolStripContainerTasks.TopToolStripPanel.Visible = True

                        Select Case ControllerTask.UCTaskGrid.GridType
                            Case UCTaskGrid.GridTypes.All
                                ControllerTask.UCTaskGrid.UpdateTaskItemExtern(TaskResult, 1)
                            Case UCTaskGrid.GridTypes.WorkFlow
                                If ControllerTask.UCTaskGrid.stepid = TaskResult.WorkId Then ControllerTask.UCTaskGrid.UpdateTaskItemExtern(TaskResult, 1)
                            Case UCTaskGrid.GridTypes.WFStep
                                If ControllerTask.UCTaskGrid.stepid = TaskResult.StepId Then ControllerTask.UCTaskGrid.UpdateTaskItemExtern(TaskResult, 1)
                        End Select
                    End If

                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        End Sub


#End Region
#End Region

#Region "Refresh"
        Public Sub UpdateTaskAsignedUser(ByVal taskid As Int64, ByVal UserAsignedName As String)
            ControllerTask.UCTaskGrid.UpdateTaskAsignedUserExtern(taskid, UserAsignedName)
        End Sub
#End Region


        ''' <summary>
        ''' Muestra la tarea
        ''' </summary>
        ''' <param name="Task">Tarea a mostrar</param>
        ''' <returns></returns>
        ''' <history>
        '''     [Tomas] 16/04/2009  Created 
        Public Sub OpenTask(ByVal Task As ITaskResult)
            'Selecciona la tarea en la grilla
            ControllerTask.UCTaskGrid.SelectTask(Task.TaskId)
            'Muestra la tarea
            ShowTask(Task)
        End Sub

        Private Sub InputIndexData(ByVal _result As Result, ByVal _indexs As ArrayList)
            Dim frmIndexImput As New Form()
        End Sub

        Private Sub UcDocumentsParent_ControlRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles UcDocumentsParent.ControlRemoved
            If UcDocumentsParent.TabPages.Count = 1 Then
                MainFormTabControl.SelectedTab = Parent

            End If
        End Sub

#Region "Toolbar"

        '''' <summary>
        '''' Carga la toolbar de tareas con las reglas
        '''' </summary>
        '''' <param name="_taskresults">Coleccion de taskResults utilizado para cargar reglas</param>
        '''' <remarks></remarks>
        '''' <history>Diego 27-06-2008 [Created]</history>
        'Sub LoadToolbar(ByRef _taskresults As List(Of ITaskResult))
        '    If IsNothing(_taskresults) Then Exit Sub
        '    taskresults.Clear()
        '    For Each r As ITaskResult In _taskresults
        '        If IsNothing(taskresults) Then taskresults = New List(Of ITaskResult)
        '        taskresults.Add(r)
        '    Next
        '    Me.LoadUserAction()
        'End Sub

        ''' <summary>
        ''' Evento Click de las reglas
        ''' </summary>
        ''' <param name="sender">Objeto emisor del evento</param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Diego]    27-06-2008 Created
        '''     [Gaston]   27-06-2008 Modified   Los results que se seleccionan estan definidos como TaskResults,
        '''                                      Es necesario pasarlos a tipo ITaskResults para utilizar el metodo
        '''                                      Execute
        '''     [Ezequiel] 02/09/2009 - Se remueve el evento que refresca ya que si se ejecutan varias tareas las cuales refrescan
        '''                             se estarian haciendo varios refresh. El evento se vuelve a agregar en el finally.
        '''</history>
        'Public Event ShowAddedTaskToWf(ByVal _task As Generic.List(Of ITaskResult))

        Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As ToolStripItemClickedEventArgs) Handles ToolBar1.ItemClicked
            'Verifica que no sea un botón genérico ya que estos poseen sus propios handlers
            If Not (TypeOf (e.ClickedItem) Is GenericRuleButton) Then
                Dim _tasks As New List(Of ITaskResult)
                Try
                    Dim tasksids As New List(Of Int64)
                    Dim stepid As Int64
                    For Each GridTaskResult As GridTaskResult In SelectedResult(True)
                        stepid = GridTaskResult.StepId
                        tasksids.Add(GridTaskResult.TaskId)
                    Next

                    _tasks = WFTaskBusiness.GetTasksByTaskIdsAndDocTypeIdAndStepId(tasksids, DirectCast(ControllerTask.UCTaskGrid.GridView.cmbDocType, System.Windows.Forms.ToolStripComboBox).ComboBox.SelectedValue, stepid)

                    If WFTaskBusiness.LockTasks(tasksids) Then
                        For Each task As ITaskResult In _tasks
                            If task.AsignedToId = 0 Then
                                'ASIGNO A USUARIO ACTUAL
                                WFTaskBusiness.Asign(task, Membership.MembershipHelper.CurrentUser.ID, Membership.MembershipHelper.CurrentUser.ID, False)
                                'INICIO LA TAREA
                                WFTaskBusiness.Iniciar(task)
                            End If
                        Next

                        'EJECUTO REGLAS 
                        Dim WFRB As New WFRulesBusiness
                        _tasks = WFRB.ExecutePrimaryRule(DirectCast(e.ClickedItem.Tag, WFRuleParent), _tasks, Nothing)
                    End If

                    If String.Compare(e.ClickedItem.Tag.GetType.Name, "DoDelete") = 0 Then
                        ToolBar1.Items.Clear()
                        ToolBar1.Update()
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    MessageBox.Show("Ocurrio un error en la ejecución de reglas de la tarea. Contactese con el administrador del sistema.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If Not IsNothing(_tasks) Then
                        Dim i As Int32
                        For i = 0 To _tasks.Count - 1
                            If Not IsNothing(_tasks(i)) Then
                                _tasks(i).Dispose()
                                _tasks(i) = Nothing
                            End If
                        Next
                        _tasks.Clear()
                        _tasks = Nothing
                    End If
                End Try
            End If
        End Sub

        Public Sub ShowProperGrid()
            ControllerTask.ShowResultsGrid()
            ToolStripContainerTasks.TopToolStripPanel.Visible = False
        End Sub

        Public Sub ShowResult(result As IResult)
            ControllerTask.tabResults.ShowProperResult(result)
        End Sub

        ''' <summary>
        ''' Muestra todas las reglas
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>Diego 27-06-2008 [Created]</history>
        Private Sub ShowUserActions()
            For Each B As ToolStripItem In ToolBar1.Items
                If TypeOf B Is System.Windows.Forms.ToolStripButton Then B.Visible = True
            Next
        End Sub

        ''' <summary>
        ''' Carga las reglas
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        '''Diego 27-06-2008 [Created]
        ''' [Ezequiel] 04/03/2009 Modified - Se agrego la opcion de que cargue la grilla de otros wf donde esta la entidad
        ''' [Sebastian] 15-07-09 MODIFIED - Se agrego permiso para cargar o no tareas segun si el usuario tiene o no permisos para 
        ''' ejecutar tareas de otros usuarios
        '''</history>
        Public Sub LoadUserAction(ByVal GridtasksResults As List(Of GridTaskResult))
            Try
                ClearUserActions()
                LoadDynamicbuttonsOverGrid()

                Dim userActionName As String = String.Empty
                Dim PredicateType As Predicate(Of GridTaskResult) = Nothing

                Dim AllowExecuteRulesAsignedToOtherUsers As Boolean = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.allowExecuteTasksAssignedToOtherUsers, CInt(GridtasksResults(0).StepId))

                Dim bolInvalidTasks As Boolean = False
                For Each task As GridTaskResult In GridtasksResults
                    Dim users As Generic.List(Of Long) = UserGroupBusiness.GetUsersIds(task.AsignedToId, True)

                    If (users.Contains(Membership.MembershipHelper.CurrentUser.ID) Or task.AsignedToId = Membership.MembershipHelper.CurrentUser.ID Or AllowExecuteRulesAsignedToOtherUsers = True Or task.AsignedToId = 0) = False Then
                        ControllerTask.UCTaskGrid.DeSelectTask(task.TaskId)
                        bolInvalidTasks = True
                    End If
                Next

                If bolInvalidTasks = True Then
                    MessageBox.Show("Usted seleccionó una tarea para la cual no tiene permiso de ejecutar reglas", "Zamba Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                Dim WFStep As IWFStep = Zamba.Core.WFStepBusiness.GetStepById(GridtasksResults(0).StepId)
                '                Dim Rules As DsRules = WFRulesBusiness.GetCompleteHashTableRulesByStep(GridtasksResults(0).StepId)
                For Each RuleRow As DsRules.WFRulesRow In WFStep.DSRules.WFRules.Rows
                    Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow.Id, True)

                    'Marcelo: Se comento la llamada a la habilitacion por BD ya que la misma no se utiliza
                    'Actualiza el estado de la regla
                    'Rule.Enable = WFRulesBusiness.GetRuleEstado(Rule.ID)
                    Rule.Enable = True
                    Dim RuleEnabled As Boolean
                    If GridtasksResults(0).UserRules.ContainsKey(Rule.ID) Then
                        '[Sebastian 04-06-2009] se agrego cast para salvar warning
                        RuleEnabled = DirectCast(GridtasksResults(0).UserRules(Rule.ID), Boolean)
                    Else
                        RuleEnabled = True
                    End If
                    If Rule.ParentType = TypesofRules.AccionUsuario AndAlso RuleEnabled = True AndAlso Rule.Enable = True Then
                        Dim UAB As New System.Windows.Forms.ToolStripButton
                        Dim ZIcon As New ZIconsList

                        UAB.Image = ZIcon.ZIconList.Images(WFRulesBusiness.GetRuleIconBasedOnClass(Rule.RuleClass, Rule.Name))
                        UAB.Tag = Rule

                        'Busca en la tabla si existe un nombre de acción de usuario para esa regla
                        Try
                            userActionName = WFBusiness.GetUserActionName(Rule.ID, Rule.WFStepId, Rule.Name, True).ToUpper
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                            userActionName = String.Empty
                        End Try

                        'Si el nombre no existe entonces le asigna el nombre de la regla
                        If String.IsNullOrEmpty(userActionName) Then
                            userActionName = Rule.Name.ToUpper
                        End If

                        'Asigna el nombre al botón. Si este es mayor que 20 lo corta y le agrega 3 puntos
                        UAB.ToolTipText = userActionName
                        If userActionName.Length > 20 Then
                            UAB.Text = userActionName.Substring(0, 20) & "..."
                        Else
                            UAB.Text = userActionName
                        End If
                        ToolBar1.Items.Add(UAB)
                    End If
                Next
                'Oculta/muestra reglas segun preferencias por cada result
                GetStatesOfTheButtonsRule(GridtasksResults)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        ''' <summary>
        ''' oculta y deshabilita las reglas de la toolbar
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        '''Diego 27-06-2008 [Created]
        ''' [Ezequiel] 04/03/2009 Modified - Se agrego la opcion de que borre la grilla de otros wf donde esta la entidad
        '''</history>
        Private Sub ClearUserActions()
            Try
                If ToolBar1.Items.Count > 1 Then
                    ToolBar1.Invalidate()
                    Dim Btn2Remove As New ArrayList
                    For Each B As ToolStripItem In ToolBar1.Items
                        If TypeOf B Is System.Windows.Forms.ToolStripButton Then
                            B.Visible = False
                            Btn2Remove.Add(B)
                        End If
                    Next
                    For i As Int32 = 0 To Btn2Remove.Count - 1
                        ToolBar1.Items.Remove(DirectCast(Btn2Remove(i), System.Windows.Forms.ToolStripButton))
                    Next

                    ToolBar1.Update()
                End If



            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Método que sirve para ver o ocultar las reglas 
        ''' </summary>
        ''' <param name="state"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]    24/07/2008  Created    
        ''' </history>
        Private Sub visibleOrInvisibleButtonsRule(ByRef state As Boolean)

            ToolBar1.Invalidate()

            For counter As Integer = 1 To ToolBar1.Items.Count - 1
                ToolBar1.Items(counter).Visible = state
            Next

            ToolBar1.Update()

        End Sub

        ''' <summary>
        ''' Método utilizado para habilitar/deshabilitar reglas segun coleccion de taskresults
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>Diego 27-06-2008 [Created]</history>
        Public Sub GetStatesOfTheButtonsRule(ByVal GridtasksResults As List(Of GridTaskResult))
            Try
                If GridtasksResults.Count = 0 Then Exit Sub
                Dim AllowExecuteRulesAsignedToOtherUsers As Boolean = RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.allowExecuteTasksAssignedToOtherUsers, CInt(GridtasksResults(0).StepId))

                Dim selectionvalue As RulePreferences = DirectCast(-1, RulePreferences)

                Dim Dt As DataTable = Nothing
                Dim Dt2 As DataTable = Nothing

                For Each _taskresult As GridTaskResult In GridtasksResults
                    Dim users As Generic.List(Of Long) = UserGroupBusiness.GetUsersIds(_taskresult.AsignedToId, True)

                    If users.Contains(Membership.MembershipHelper.CurrentUser.ID) Or _taskresult.AsignedToId = Membership.MembershipHelper.CurrentUser.ID Or AllowExecuteRulesAsignedToOtherUsers = True Or _taskresult.AsignedToId = 0 Then
                        Dt = Nothing
                        Dt2 = Nothing
                        selectionvalue = DirectCast(-1, RulePreferences)

                        'Recorre cada regla activa en el documento
                        For Each UAB As ToolStripButton In ToolBar1.Items
                            'Agregue esta linea para mejorar el tiempo del metodo
                            'Si el boton esta como no visible, no tiene sentido volver a preguntar por el mismo - MC
                            If UAB.Visible = True Then
                                Dim RuleId As Int64 = DirectCast(UAB.Tag, IRule).ID
                                'Obtiene el valor 
                                selectionvalue = WFBusiness.recoverItemSelectedThatCanBe_StateOrUserOrGroup(_taskresult.StepId, RuleId, False)
                                'Se Evalua el valor de la variable seleccion 
                                Select Case selectionvalue
                                    'Caso de trabajo con Estados
                                    Case RulePreferences.HabilitationSelectionState
                                        'Se Obtienen los ids de estados DESHABILITADOS
                                        Dt = WFRulesBusiness.GetRuleOption(_taskresult.StepId, RuleId, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeState, 0, True)
                                        'Por cada Id de estado se compara con el id de estado seleccionado, en cada de encontrar
                                        'Coincidencia, se deshabilita la regla
                                        If Not IsNothing(Dt) AndAlso Dt.Rows.Count > 0 Then
                                            For Each r As DataRow In Dt.Rows
                                                '[Sebastian 04-06-2009] se agrego parse y se comento la linea
                                                'para salcar warning
                                                'If r.Item(0) = _taskresult.StateId Then
                                                If Int64.Parse(r.Item("objvalue").ToString) = _taskresult.StateId Then
                                                    UAB.Visible = False
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                        'Caso de trabajo con Usuarios o Grupos
                                    Case RulePreferences.HabilitationSelectionUser
                                        Dim ruleEnabled As Boolean = False

                                        'Se Obtienen los ids de USUARIOS DESHABILITADOS
                                        Dt = WFRulesBusiness.GetRuleOption(_taskresult.StepId, RuleId, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeUser, 0, True)
                                        If Not IsNothing(Dt) AndAlso Dt.Rows.Count > 0 Then

                                            'Por cada Id de usuario se compara con el id de usuario logeado, en cada de encontrar
                                            'Coincidencia, se verifican los grupos
                                            For Each r As DataRow In Dt.Rows
                                                ruleEnabled = True
                                                If Int64.Parse(r.Item("Objvalue").ToString) = Membership.MembershipHelper.CurrentUser.ID Then
                                                    ruleEnabled = False
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                        If ruleEnabled = False Then
                                            ruleEnabled = True
                                            'Se obtienen los ids de grupo del usuario y que tienen permiso en la etapa
                                            Dt = WFStepBusiness.GetStepUserGroupsIdsAsDS(WFStepBusiness.GetStepIdByRuleId(RuleId, True), Membership.MembershipHelper.CurrentUser.ID)
                                            'Se Obtienen los ids de GRUPOS DESHABILITADOS
                                            Dt2 = WFRulesBusiness.GetRuleOption(_taskresult.StepId, RuleId, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeGroup, 0, True)
                                            If Not IsNothing(Dt) AndAlso Dt.Rows.Count > 0 AndAlso Not IsNothing(Dt2) Then

                                                'Por cada Grupo del usuario logeado y que tiene permiso la etapa, se fija si esta deshabilitado
                                                For Each rUser As DataRow In Dt.Rows
                                                    ruleEnabled = True
                                                    For Each r As DataRow In Dt2.Rows
                                                        If rUser.Item(0).ToString() = r.Item("Objvalue").ToString() Then
                                                            ruleEnabled = False
                                                            Exit For
                                                        End If
                                                    Next
                                                    'Si el grupo esta habilitado salgo asi queda habilitada la regla
                                                    If ruleEnabled = True Then
                                                        Exit For
                                                    End If
                                                Next
                                            End If
                                        End If

                                        If ruleEnabled = False Then
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "DesHabilitada")
                                            UAB.Visible = False
                                        Else
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Habilitada")
                                        End If

                                        'Caso de trabajo con Usuarios/Grupos y estados
                                    Case RulePreferences.HabilitationSelectionBoth
                                        'Se Obtienen los ids de USUARIOS Y ESTADOS DESHABILITADOS
                                        Dt = WFRulesBusiness.GetRuleOption(_taskresult.StepId, RuleId, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeStateAndUser, 0, True)
                                        'Por cada Id de usuario se comparan con el id de usuario logeado y los ids de etapa con el seleccionado en el combobox
                                        ', en cada de encontrar coincidencia, se deshabilita la regla
                                        If Not IsNothing(Dt) AndAlso Dt.Rows.Count > 0 Then
                                            For Each r As DataRow In Dt.Rows
                                                '[Sebastian 04-06-2009] se agrego parse para salvar warning
                                                If Int64.Parse(r.Item("ObjValue").ToString) = Membership.MembershipHelper.CurrentUser.ID AndAlso Int64.Parse(r.Item("ObjExtraData").ToString) = _taskresult.StateId Then
                                                    UAB.Visible = False
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                        'si no se deshabilito la regla por usuario y estado se intenta deshabilitar por grupo y estado
                                        If UAB.Visible Then
                                            'Se Obtienen los ids de GRUPOS Y ESTADOS DESHABILITADOS
                                            Dt2 = WFRulesBusiness.GetRuleOption(_taskresult.StepId, RuleId, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeStateAndGroup, 0, True)
                                            'Por cada Id de grupo se recorren sus usuarios, se comparan con el id de usuario logeado y los ids de etapa con el seleccionado en el combobox
                                            ', en cada de encontrar coincidencia, se deshabilita la regla
                                            If Not IsNothing(Dt2) AndAlso Dt2.Rows.Count > 0 Then
                                                For Each r As DataRow In Dt2.Rows
                                                    '[Sebastian 04-06-2009] se agrego parse para salvar warning
                                                    If Int64.Parse(r.Item("ObjExtraData").ToString) = _taskresult.StateId Then
                                                        '[Sebastian 04-06-2009] se agrego parse para salvar warning
                                                        For Each u As User In UserGroupBusiness.GetUsersByGroup(Int64.Parse(r.Item("ObjValue").ToString))
                                                            If u.ID = Membership.MembershipHelper.CurrentUser.ID Then
                                                                UAB.Visible = False
                                                                Exit For
                                                            End If
                                                        Next

                                                    End If
                                                    If UAB.Visible = False Then
                                                        Exit For
                                                    End If
                                                Next
                                            End If
                                        End If

                                End Select
                                'Aplica el permiso de permitir ejecutar reglas de tareas cuyos usuario son distintos de logeado
                                If Not AllowExecuteRulesAsignedToOtherUsers AndAlso Not users.Contains(Membership.MembershipHelper.CurrentUser.ID) AndAlso Not _taskresult.AsignedToId = Membership.MembershipHelper.CurrentUser.ID Then
                                    If _taskresult.AsignedToId <> 0 Then UAB.Visible = False
                                End If
                            End If
                        Next
                    Else
                        MessageBox.Show("Usted no tiene permiso para Ejecutar reglas sobre esta tarea", "Zamba Cliente", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    End If
                Next

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region


        Private Sub ShowVersionComment(ByVal result As Result)

            Dim collection As New System.Collections.Generic.List(Of String)
            Dim comment As String
            Dim commentdate As String

            comment = Results_Business.GetVersionComment(result.ID)
            commentdate = Results_Business.GetVersionCommentDate(result.ID)
            If String.IsNullOrEmpty(commentdate) Then
                commentdate = "No se registran datos de este documento"
                comment = String.Empty
            End If

            Dim PublishResult As New PublishableResult
            PublishResult = Results_Business.PublishableResult(result)
            Dim showversion As New frmPublishVersion(PublishResult, result, comment, commentdate)
            showversion.ShowDialog()
            UpdateIndexs(result)
        End Sub
        Private UCIndexViewer As Zamba.Viewers.UCIndexViewer
        Shared _sync As New Object
        Private Sub UpdateIndexs(ByRef Result As Result)
            UCIndexViewer.ShowIndexs(Result.ID, Result.DocTypeId, False)
        End Sub
#Region "Exportar a PDF"
        Private Sub ExportToPDF(ByRef Result As Result)
            Try
                Dim SFDlg As New SaveFileDialog
                Dim bResultado As Boolean
                SFDlg.CheckPathExists = False
                SFDlg.ValidateNames = True
                SFDlg.FileName = "Nuevo Documento"
                SFDlg.Filter = "PDF(*.pdf)|*.pdf"
                SFDlg.FilterIndex = 1
                SFDlg.ShowDialog(Me)
                Try
                    bResultado = Results_Business.exportarResultPDF(Result, SFDlg.FileName)
                Catch ex As Exception
                    bResultado = False
                End Try
                If bResultado Then
                    MessageBox.Show("Archivo exportado a PDF", "Zamba - Exportar a PDF")
                Else
                    MessageBox.Show("No se pudo convertir el archivo seleccionado a PDF", "Zamba - Exportar a PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#End Region

#Region "Utilizado por el Patron Observador"
        Dim iplugin As Form2 = Nothing
        Public Sub Save(ByRef r As Object)
            Dim path As String = DirectCast(r, Result).FullPath
            File.Copy(path, path, True)
        End Sub
        Public Sub Close(ByRef o As Object)
            Try
                If iplugin IsNot Nothing Then
                    iplugin.Close()
                    iplugin.Dispose()
                    iplugin = Nothing
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region

#Region "Tool Bar"
        Private Sub LoadResultRights(ByVal Result As Result)



            'ENVIAR POR EMAIL
            If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Documents, RightsType.EnviarPorMail) = True Then
                toolbar.btnAdjuntarEmail.Visible = True
                toolbar.ToolStripSeparator8.Visible = True
            Else
                toolbar.btnAdjuntarEmail.Visible = False
                toolbar.ToolStripSeparator8.Visible = False
            End If


            'IMPRIMIR
            If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Documents, RightsType.Print) = True Then
                toolbar.btnImprimirImagenesIndices.Visible = True
            Else
                toolbar.btnImprimirImagenesIndices.Visible = False
            End If

            If Result.IsPDF Then
                'todo: hay que ver porque con el boton de imprimir de zamba el PDF sale en blanco, desde windows no pasa.
                toolbar.btnImprimirImagenesIndices.Visible = False
            End If

        End Sub

        '''
        Private Sub LoadTaskBarButtons()


            'ENVIAR POR EMAIL
            If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Documents, RightsType.EnviarPorMail) = True Then
                toolbar.btnAdjuntarEmail.Visible = True
                toolbar.ToolStripSeparator8.Visible = True
            Else
                toolbar.btnAdjuntarEmail.Visible = False
                toolbar.ToolStripSeparator8.Visible = False
            End If


            'IMPRIMIR
            If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Documents, RightsType.Print) = True Then
                toolbar.btnImprimirImagenesIndices.Visible = True
            Else
                toolbar.btnImprimirImagenesIndices.Visible = False
            End If

            'Remueve los botones innecesarios
            toolbar.btnSaveAs.Visible = False
            'Me.toolbar.btnGotoWorkflow.Visible = False
            toolbar.btnAgregarUnaNuevaVersionDelDocuemto.Visible = False
            'Me.toolbar.btnAdjuntarMensaje.Visible = False
            toolbar.btnVerVersionesDelDocumento.Visible = False
            toolbar.btnChangePosition.Visible = False
            'Me.toolbar.btnVerDocumentosAsociados.Visible = False
            'Me.toolbar.btnVerForo.Visible = False
            toolbar.ToolStripSeparator7.Visible = False
            'Me.toolbar.ToolStripSeparator9.Visible = False
            toolbar.ToolStripSeparator10.Visible = False
            toolbar.ToolStripSeparator11.Visible = False
            toolbar.ToolStripSeparator12.Visible = False

        End Sub

        Private Sub toolbar_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles toolbar.ItemClicked
            Try
                Select Case CStr(e.ClickedItem.Tag)
                    Case "EMAIL"

                        Dim Result As List(Of IResult) = ControllerTask.UCTaskGrid.SelectedResultsListExtern()

                        If Result IsNot Nothing AndAlso Result.Count > 0 Then
                            ZClass.HandleModuleResultList(ResultActions.EnvioDeMail, Result, New Hashtable)
                        End If

                    Case "MENSAJE"
                        '[Sebastian] 30-06-2009 se comenta porque la funcionalidad de se elimino.
                        'Me.EnviarMessage(Me.ControllerTask.UCTaskGrid.SelectedResultsExtern)
                    Case "IMPRIMIR"
                        _Imprimir(ControllerTask.UCTaskGrid.SelectedResultsListExtern(), Print.LoadAction.ShowForm)
                    Case "PREVISUALIZAR"
                        _Imprimir(ControllerTask.UCTaskGrid.SelectedResultsListExtern(), Print.LoadAction.ShowPreview)
                    Case "VERSIONESDELDOCUMENTO"
                        '[Sebastian 04-06-2009] se agrego cast para salvar el warning
                        ShowVersionComment(DirectCast(ControllerTask.UCTaskGrid.SelectedResultsListExtern(0), Result))
                        'Case "GUARDARDOCUMENTOCOMO"
                        '    Try
                        '        Me.GuardarComo(Me.ControllerTask.UCTaskGrid.SelectedTask)
                        '    Catch ex As Exception
                        '        ZClass.raiseerror(ex)
                        '    End Try
                    Case "AGREGARACARPETA"
                        ''[Sebastian 04-06-2009] se agrego cast para salvar el warning
                        'Me.AgregarACarpeta(DirectCast(Me.ControllerTask.UCTaskGrid.SelectedResultsListExtern(0), Result))
                        Dim Result As Generic.List(Of IResult) = ControllerTask.UCTaskGrid.SelectedResultsListExtern()

                        If Result.Count > 0 Then
                            ZClass.HandleModule(ResultActions.InsertarDocumento, Result(0), New Hashtable)
                        End If
                    Case "ENVIARZIP"
                        Try
                            ControllerTask.tabResults.EnviarZip(ControllerTask.UCTaskGrid.SelectedResultsListExtern())
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Case "EXPORTPDF"
                        Try

                        Catch ex As Exception

                        End Try
                        Dim myListResult As List(Of IResult) = ControllerTask.UCTaskGrid.SelectedResultsListExtern()

                        For Each myResult As Result In myListResult
                            ExportToPDF(myResult)
                        Next
                    Case "SIZEUP"
                        ControllerTask.UCTaskGrid.SetFontSizeUp()
                    Case "SIZEDOWN"
                        ControllerTask.UCTaskGrid.SetFontSizeDown()
                End Select
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub


        Dim extVis2 As Zamba.Viewers.ExternalVisualizer

        Friend Sub ShowInFullScreen()
            If extVis2 Is Nothing Then
                extVis2 = New ExternalVisualizer(DirectCast(TabTaskDetails, TabPage))
            End If

            MainFormTabControl.SelectedTab = Parent

            RemoveHandler extVis2.FullScreenClosed, AddressOf CambiarDock
            AddHandler extVis2.FullScreenClosed, AddressOf CambiarDock

            OpenInFullScreen = True
            AdjustFullScreenTasks()
            extVis2.Show()
        End Sub

        ''' <summary>
        ''' [Sebastian] 16-06-2009 MODIFIED Se modifico para poder cerrar el formulario que muestra la tarea
        ''' (pablo) 11/10/2010 MODIFIED Se modifico el metodo para que se pueda abrir el tab de tareas completo
        ''' maximizada 
        ''' </summary>
        ''' <param name="Sender">el tab que se esta visualizando actualmente</param>
        ''' <param name="ClosedFromCross">Indica si el formulario es cerrado desde la cruz del formulario</param>
        ''' <remarks></remarks>
        Public Sub CambiarDock(ByVal Sender As TabPage, Optional ByVal ClosedFromCross As Boolean = False, Optional ByVal IsMaximize As Boolean = False)
            Try
                If Not ClosedFromCross Then
                    If IsMaximize Then
                        'Minimizar
                        CloseFullScreen(True)
                    Else
                        ShowInFullScreen()
                        'posiciona el tab en la solapa de tareas
                        SetSelectedTab(True)
                    End If
                Else
                    '(pablo) 10/11/2010
                    'posiciona el tab en la solapa de tareas
                    SetSelectedTab(False)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Public TaskIsMaximized As Boolean

        Public Sub CloseFullScreen(ByVal isMaximized As Boolean)
            If Not extVis2 Is Nothing Then
                extVis2.Close()
                extVis2.Dispose()
                If isMaximized Then
                    MainFormTabControl.SelectedTab = TabTaskDetails
                Else
                    MainFormTabControl.SelectedTab = Parent
                End If
            End If
            OpenInFullScreen = False
            AdjustFullScreenTasks()
            RaiseEvent TabTaskChanged(TabPages.TabTasks)

            TaskIsMaximized = False
            'De alguna manera poner isMaximize en ucdocumentviwer2

        End Sub

        Public Sub SelectTasksListTAB()
            MainFormTabControl.SelectedTab = Parent
        End Sub

        Dim treeHidden As Boolean = False

        Private Sub btnToggle_Click(sender As Object, e As EventArgs) Handles btnToggle.Click

            If treeHidden Then
                PosicionarSplitterWf()
                btnToggle.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_navigate_previous
                treeHidden = False
            Else
                HideSplitterWF()
                btnToggle.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_navigate_next1
                treeHidden = True
            End If
        End Sub

        Private Sub HideSplitterWF()
            Try
                RemoveHandler Splitter1.SplitterMoved, AddressOf Splitter1_SplitterMoved
                PanelResults.Width = 0
                AddHandler Splitter1.SplitterMoved, AddressOf Splitter1_SplitterMoved
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Public Sub SelectTasksTAB()
            MainFormTabControl.SelectedTab = TabTaskDetails
        End Sub

        '(pablo) 10/11/2010
        'evento que posiciona el tab en la solapa adecuada dependiendo 
        'del tab que se encuentre seleccionado
        Friend Sub SetSelectedTab(ByVal open As Boolean)
            If open Then
                MainFormTabControl.SelectedTab = Parent
                OpenInFullScreen = True
            Else
                MainFormTabControl.SelectedTab = TabTaskDetails
                OpenInFullScreen = False
            End If
        End Sub

        Public Sub AgregarACarpeta(ByRef Result As Result)
            Try
                Result.DocType.IsReindex = True
                RaiseEvent DLoadIndexerEvent(Result)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        Public Event DLoadIndexerEvent(ByRef Result As Result)
#End Region


        ''' <summary>
        ''' Cuando se selecciona la solapa, se carga el WF
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        'Private Sub tbDiagrama_Selected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlEventArgs) Handles MainFormTabControl.Selected
        '    showDiagram()
        'End Sub

        ''' <summary>
        ''' Muestra el diagrama en la solapa
        ''' </summary>
        ''' <remarks></remarks>
        'Private Sub showDiagram()
        '    If MainFormTabControl.SelectedTab.Name = TabGraphic.Name Then
        '        ShowWFGraphic(WFTreeControlForClient.getLastWfID())
        '    End If
        'End Sub

        Private Sub Splitter1_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles Splitter1.SplitterMoved
            Try
                UserPreferences.setValue("PaneldeWFAncho", PanelResults.Width, UPSections.WorkFlow)
                If PanelResults.Width > 0 Then
                    treeHidden = False
                    btnToggle.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_navigate_previous
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub



    End Class

End Namespace
