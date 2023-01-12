Imports Zamba.Core.WF.WF
Imports Zamba.Core
'Imports Zamba.WFBusiness

Public Class WFMonitorIDE
    Inherits ZForm

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
    Friend WithEvents PanelMain As ZPanel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents PanelBottom As System.Windows.Forms.Panel
    Friend WithEvents PanelTop As System.Windows.Forms.Panel
    Friend WithEvents PanelRight As System.Windows.Forms.Panel
    Friend WithEvents PanelLeft As System.Windows.Forms.Panel
    Friend WithEvents PanelCircuit As System.Windows.Forms.Panel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        PanelMain = New ZPanel
        Splitter1 = New System.Windows.Forms.Splitter
        PanelCircuit = New System.Windows.Forms.Panel
        PanelBottom = New System.Windows.Forms.Panel
        PanelTop = New System.Windows.Forms.Panel
        PanelRight = New System.Windows.Forms.Panel
        PanelLeft = New System.Windows.Forms.Panel
        SuspendLayout()
        '
        'PanelMain
        '
        PanelMain.BackColor = System.Drawing.Color.White
        PanelMain.Dock = System.Windows.Forms.DockStyle.Fill
        PanelMain.Location = New System.Drawing.Point(316, 5)
        PanelMain.Name = "PanelMain"
        PanelMain.Size = New System.Drawing.Size(351, 456)
        PanelMain.TabIndex = 46
        '
        'Splitter1
        '
        Splitter1.BackColor = System.Drawing.Color.GhostWhite
        Splitter1.Location = New System.Drawing.Point(312, 5)
        Splitter1.Name = "Splitter1"
        Splitter1.Size = New System.Drawing.Size(4, 456)
        Splitter1.TabIndex = 45
        Splitter1.TabStop = False
        '
        'PanelCircuit
        '
        PanelCircuit.BackColor = System.Drawing.Color.White
        PanelCircuit.Dock = System.Windows.Forms.DockStyle.Left
        PanelCircuit.Location = New System.Drawing.Point(5, 5)
        PanelCircuit.Name = "PanelCircuit"
        PanelCircuit.Size = New System.Drawing.Size(307, 456)
        PanelCircuit.TabIndex = 44
        '
        'PanelBottom
        '
        PanelBottom.BackColor = System.Drawing.Color.GhostWhite
        PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        PanelBottom.Location = New System.Drawing.Point(5, 461)
        PanelBottom.Name = "PanelBottom"
        PanelBottom.Size = New System.Drawing.Size(662, 3)
        PanelBottom.TabIndex = 41
        '
        'PanelTop
        '
        PanelTop.BackColor = System.Drawing.Color.GhostWhite
        PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        PanelTop.Location = New System.Drawing.Point(5, 2)
        PanelTop.Name = "PanelTop"
        PanelTop.Size = New System.Drawing.Size(662, 3)
        PanelTop.TabIndex = 43
        '
        'PanelRight
        '
        PanelRight.BackColor = System.Drawing.Color.GhostWhite
        PanelRight.Dock = System.Windows.Forms.DockStyle.Right
        PanelRight.Location = New System.Drawing.Point(667, 2)
        PanelRight.Name = "PanelRight"
        PanelRight.Size = New System.Drawing.Size(3, 462)
        PanelRight.TabIndex = 42
        '
        'PanelLeft
        '
        PanelLeft.BackColor = System.Drawing.Color.GhostWhite
        PanelLeft.Dock = System.Windows.Forms.DockStyle.Left
        PanelLeft.Location = New System.Drawing.Point(2, 2)
        PanelLeft.Name = "PanelLeft"
        PanelLeft.Size = New System.Drawing.Size(3, 462)
        PanelLeft.TabIndex = 40
        '
        'WFMonitorIDE
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        ClientSize = New System.Drawing.Size(672, 466)
        Controls.Add(PanelMain)
        Controls.Add(Splitter1)
        Controls.Add(PanelCircuit)
        Controls.Add(PanelBottom)
        Controls.Add(PanelTop)
        Controls.Add(PanelRight)
        Controls.Add(PanelLeft)
        DockPadding.All = 2
        Name = "WFMonitorIDE"
        WindowState = System.Windows.Forms.FormWindowState.Maximized
        ResumeLayout(False)

    End Sub

#End Region

    Dim WF As Core.WorkFlow
    Dim WithEvents WFPanelMonitor As WFPanelMonitor
    Dim WithEvents ControllerMonitor As ControllerMonitor
    '  Dim WFTransitions As WFTransitions
    Dim IL As Zamba.AppBlock.ZIconsList

    Dim CurrentUserId As Int64
    Public Sub New(ByVal WF As Core.WorkFlow, ByVal CurrentUserId As Int64)
        MyBase.New()
        InitializeComponent()

        Me.CurrentUserId = CurrentUserId


        Try
            IL = New Zamba.AppBlock.ZIconsList
            Me.WF = WF
            WFBusiness.GetFullMonitorWF(WF)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Dim WFShapeCircuit As Zamba.WFShapes.Controls.MainForm
    Private Sub LoadGraphicTab(ByVal wf As Zamba.Core.WorkFlow)
        Try
            If IsNothing(WFShapeCircuit) AndAlso Not IsNothing(wf) Then
                WFShapeCircuit = New Zamba.WFShapes.Controls.MainForm(wf, False)
                WFShapeCircuit.Dock = DockStyle.Fill
                PanelMain.Controls.Add(WFShapeCircuit)
            Else
                'Manda Error de resolución de sobrecarga porque ninguna de las funciones 'cargarWorkFlow' a las que se tiene acceso acepta este número de argumentos.	C:\ZambaSoftware2005\GroupCommonControls\ZCtrls\WF\UInterface\Client\UCPanels.vb	
                WFShapeCircuit.cargarWorkFlow(wf, False)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "Refresh"
    Private _timer As Threading.Timer
    Dim CallBack As New Threading.TimerCallback(AddressOf RefreshWF)
    Dim State As Object
    Private Sub InitializeTimer()
        RefreshHandlers()
        _timer = New Threading.Timer(CallBack, State, Total * 1000, Total * 1000)
    End Sub

    Dim SecondsRemaining As Int32
    Dim WithEvents T2 As New Timer


    Private Sub RefreshHandlers()
        'RemoveHandler WFTaskBusiness.AddedTask, AddressOf AddTask
        'RemoveHandler WFTaskBusiness.RemovedTask, AddressOf ExtractTask
        RemoveHandler WFTaskBusiness.Distributed, AddressOf RefreshStep
        RemoveHandler WFTaskBusiness.AsignedAndExpireDate, AddressOf RefreshAsignedTo

        'RemoveHandler WFTaskBusiness.ChangedExpireDate, AddressOf RefreshExpireDate
        RemoveHandler ControllerMonitor.UnassignedTask, AddressOf RefreshGridAfterEdit

        'AddHandler WFTaskBusiness.AddedTask, AddressOf AddTask
        'AddHandler WFTaskBusiness.RemovedTask, AddressOf ExtractTask
        AddHandler WFTaskBusiness.Distributed, AddressOf RefreshStep
        AddHandler WFTaskBusiness.AsignedAndExpireDate, AddressOf RefreshAsignedTo

        'AddHandler WFTaskBusiness.ChangedExpireDate, AddressOf RefreshExpireDate
        AddHandler ControllerMonitor.UnassignedTask, AddressOf RefreshGridAfterEdit
    End Sub
    Private Sub RefreshGridAfterEdit()
        Dim BaseWFNode As BaseWFNode = DirectCast(WFPanelMonitor.TreeView1.SelectedNode, BaseWFNode)
        Dim EditedStep As WFStep = DirectCast(BaseWFNode, MonitorStepNode).WFStep
        EditStep(EditedStep)
    End Sub
    Private Sub RefreshWF(ByVal state As Object)
        Try
            RefreshWF()
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadStateException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub RefreshWF(ByVal sender As Object, ByVal e As EventArgs)
        Try
            RefreshWF()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub RefreshWF()
        Try
            If Not Disposing Then
                'todo: cargar todo el wf nuevamente
                '  WFBusiness.Refresh(WF)
                LoadMonitorIDE()
                ControllerMonitor.DisposeControls()
                SecondsRemaining = Total
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub AddTask(ByRef Result As TaskResult)
        Try
            'From TreeView
            WFTaskBusiness.UpdateNodesTasksCount(DirectCast(WFPanelMonitor.TreeView1.Nodes(0), WFNode))

            'From Grid
            ControllerMonitor.UCTaskGrid.AddTaskExtern(Result)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ExtractTask(ByRef Result As TaskResult)
        Try
            'From TreeView
            WFTaskBusiness.UpdateNodesTasksCount(DirectCast(WFPanelMonitor.TreeView1.Nodes(0), WFNode))

            'From Grid
            ControllerMonitor.UCTaskGrid.RemoveTaskExtern(Result)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub RefreshStep(ByVal Result As TaskResult)

        Try
            RemoveHandler WFTaskBusiness.Distributed, AddressOf RefreshStep
            'From Grid
            ControllerMonitor.UCTaskGrid.RemoveTaskExtern(Result)

            'From TreeView
            WFBusiness.GetFullMonitorWF(WF)
            WFPanelMonitor.LoadWf(WF, IL)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            AddHandler WFTaskBusiness.Distributed, AddressOf RefreshStep
        End Try

    End Sub

    Private Sub RefreshAsignedTo(ByRef Result As TaskResult)
        Try
            'Actualizo Grid
            ControllerMonitor.UCTaskGrid.UpdateTaskItemExtern(Result, 1)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub RefreshTaskState(ByRef Result As TaskResult)
        Try
            'Actualizo Grid
            ControllerMonitor.UCTaskGrid.UpdateTaskItemExtern(Result, 3)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub RefreshState(ByRef Result As TaskResult)
        Try
            'Actualizo Grid
            ControllerMonitor.UCTaskGrid.UpdateTaskItemExtern(Result, 4)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub RefreshExpireDate(ByRef Result As TaskResult)
        Try
            'Actualizo Grid
            ControllerMonitor.UCTaskGrid.UpdateTaskItemExtern(Result, 5)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Load"
    Delegate Sub LM()
    Private Sub LoadMonitorIDE()
        'Estas validaciones se pusieron para tratar de evitar el error:
        'Invoke or BeginInvoke cannot be called on a control until the window handle has been created.
        Try
            If Not IsNothing(Me) Then
                Dim D1 As New LM(AddressOf DLoadMonitorIDE)
                If Not IsNothing(D1) Then
                    Invoke(D1)
                End If
            End If
        Catch ex As InvalidOperationException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub DLoadMonitorIDE()
        Try
            'TreeView
            If IsNothing(WFPanelMonitor) Then
                WFPanelMonitor = New WFPanelMonitor(WF, IL)
                WFPanelMonitor.Dock = DockStyle.Fill
                PanelCircuit.Controls.Add(WFPanelMonitor)
                RemoveHandler WFPanelMonitor.WFSelected, AddressOf EditWF
                RemoveHandler WFPanelMonitor.StepSelected, AddressOf EditStep
                RemoveHandler WFPanelMonitor.IBUpdate.Click, AddressOf RefreshWF
                AddHandler WFPanelMonitor.WFSelected, AddressOf EditWF
                AddHandler WFPanelMonitor.StepSelected, AddressOf EditStep
                AddHandler WFPanelMonitor.IBUpdate.Click, AddressOf RefreshWF
            Else
                WFPanelMonitor.LoadWf(WF, IL)
            End If

            'Lista de Tareas
            If IsNothing(ControllerMonitor) Then
                ControllerMonitor = New ControllerMonitor(DirectCast(WFPanelMonitor.TreeView1.Nodes(0), WFNode), CurrentUserId)
                ControllerMonitor.Dock = DockStyle.Fill
                PanelMain.Controls.Add(ControllerMonitor)
                ControllerMonitor.ControllerRules.TreeView1.ImageList = IL.ZIconList
                'ControllerMonitor.UCTaskGrid.MultiSelect = True
                RemoveHandler ControllerMonitor.RefreshWF, AddressOf RefreshWF
                AddHandler ControllerMonitor.RefreshWF, AddressOf RefreshWF
                RemoveHandler ControllerMonitor.SetTotal, AddressOf SetTotal
                AddHandler ControllerMonitor.SetTotal, AddressOf SetTotal
            Else
                ControllerMonitor.LoadWfNode(DirectCast(WFPanelMonitor.TreeView1.Nodes(0), WFNode))
            End If

            'Transiciones
            LoadGraphicTab(WF)
            'If IsNothing(WFTransitions) Then
            '    WFTransitions = New WFTransitions
            '    WFTransitions.Dock = DockStyle.Fill
            '    Me.PanelMain.Controls.Add(WFTransitions)
            '    WFTransitions.LoadWF(WF)
            '    WFTransitions.lnkNewStep.Visible = False
            '    WFTransitions.PanelTop.Color1 = System.Drawing.Color.GhostWhite
            '    WFTransitions.PanelTop.Color2 = System.Drawing.Color.FromArgb(CType(198, Byte), CType(222, Byte), CType(247, Byte))
            'Else
            '    WFTransitions.LoadWF(WF)
            'End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub WFMonitorIDE_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        SetTotal(60)

        LoadMonitorIDE()
        InitializeTimer()

        'Ajusto PanelCircuit a 30%
        PanelCircuit.Width = CInt(((30 * Width) / 100))
    End Sub
#End Region

#Region "Steps"
    Private Sub EditStep(ByRef wfstep As WFStep)
        Try
            ControllerMonitor.BringToFront()
            ControllerMonitor.UCTaskGrid.ShowTasksExtern(wfstep.ID, True, 0, False)
            ControllerMonitor.ControllerRules.ShowRules(wfstep)
            ControllerMonitor.ControllerViewer.PicBox.Visible = False
            ControllerMonitor.ControllerViewer.LabelError.Visible = False
            ControllerMonitor.BringToFront()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "WF"
    Private Sub EditWF(ByVal WF As Core.WorkFlow)
        Try
            WFShapeCircuit.BringToFront()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region


    Dim Total As Int32

    Private Sub SetTotal(ByVal Total As Int32)
        T2.Interval = 5000
        T2.Enabled = False
        Me.Total = Total
        SecondsRemaining = Me.Total
        T2Tick()
        T2.Enabled = True
    End Sub

    Private Sub T2Tick()
        If IsNothing(ControllerMonitor) Then Exit Sub
        SecondsRemaining -= 5
        If SecondsRemaining = 0 Then SecondsRemaining = Total
        ControllerMonitor.lblActualizando.Text = "Actualizando en " & SecondsRemaining & " seg."
        ControllerMonitor.lblActualizando.Refresh()
    End Sub

    Private Sub T2_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles T2.Tick
        T2Tick()
    End Sub
End Class
