Imports Zamba.Core.WF.WF
Imports Zamba.Controls.WF.TasksCtls
Imports Zamba.Core

Public Class ControllerMonitor
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
    Friend WithEvents Splitter5 As System.Windows.Forms.Splitter
    Friend WithEvents PanelGrid As System.Windows.Forms.Panel
    Friend WithEvents PanelViewer As System.Windows.Forms.Panel
    Friend WithEvents InertButton1 As Button
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents lnkQuitar As System.Windows.Forms.LinkLabel
    Friend WithEvents LnkAsignar As System.Windows.Forms.LinkLabel
    Friend WithEvents LnkDesasignar As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkHistorial As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkRenoveExpireDate As System.Windows.Forms.LinkLabel
    Friend WithEvents btnShowDocument As ZButton
    Friend WithEvents btnShowRules As ZButton
    Friend WithEvents PanelButtons As ZPanel
    '  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents lnkDistribuir As System.Windows.Forms.LinkLabel
    Friend WithEvents lblActualizando As ZLabel
    Friend WithEvents txtSegundos As TextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents btnOK As ZButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New ComponentModel.Container
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(ControllerMonitor))
        PanelGrid = New System.Windows.Forms.Panel
        Splitter5 = New System.Windows.Forms.Splitter
        PanelViewer = New System.Windows.Forms.Panel
        Label1 = New ZLabel
        PanelButtons = New ZPanel
        lnkRenoveExpireDate = New System.Windows.Forms.LinkLabel
        lnkDistribuir = New System.Windows.Forms.LinkLabel
        lnkHistorial = New System.Windows.Forms.LinkLabel
        lnkQuitar = New System.Windows.Forms.LinkLabel
        LnkAsignar = New System.Windows.Forms.LinkLabel
        LnkDesasignar = New System.Windows.Forms.LinkLabel
        btnShowDocument = New ZButton
        btnShowRules = New ZButton
        '  Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        lblActualizando = New ZLabel
        txtSegundos = New TextBox
        Label2 = New ZLabel
        Label3 = New ZLabel
        btnOK = New ZButton
        PanelButtons.SuspendLayout()
        SuspendLayout()
        '
        'PanelGrid
        '
        PanelGrid.BackColor = System.Drawing.Color.White
        PanelGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelGrid.Dock = System.Windows.Forms.DockStyle.Top
        PanelGrid.Location = New System.Drawing.Point(0, 104)
        PanelGrid.Name = "PanelGrid"
        PanelGrid.Size = New System.Drawing.Size(784, 204)
        PanelGrid.TabIndex = 5
        '
        'Splitter5
        '
        Splitter5.BackColor = System.Drawing.Color.GhostWhite
        Splitter5.Dock = System.Windows.Forms.DockStyle.Top
        Splitter5.Location = New System.Drawing.Point(0, 308)
        Splitter5.Name = "Splitter5"
        Splitter5.Size = New System.Drawing.Size(784, 4)
        Splitter5.TabIndex = 6
        Splitter5.TabStop = False
        '
        'PanelViewer
        '
        PanelViewer.AutoScroll = True
        PanelViewer.BackColor = System.Drawing.Color.White
        PanelViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelViewer.Dock = System.Windows.Forms.DockStyle.Fill
        PanelViewer.Location = New System.Drawing.Point(0, 312)
        PanelViewer.Name = "PanelViewer"
        PanelViewer.Size = New System.Drawing.Size(784, 152)
        PanelViewer.TabIndex = 7
        '
        'Label1
        '
        Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label1.Dock = System.Windows.Forms.DockStyle.Top
        Label1.Font = New Font("Verdana", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label1.Location = New System.Drawing.Point(0, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(784, 32)
        Label1.TabIndex = 8
        Label1.Text = "  Monitoreo"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'PanelButtons
        '
        PanelButtons.BackColor = System.Drawing.Color.White
        PanelButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelButtons.Controls.Add(lnkRenoveExpireDate)
        PanelButtons.Controls.Add(lnkDistribuir)
        PanelButtons.Controls.Add(lnkHistorial)
        PanelButtons.Controls.Add(lnkQuitar)
        PanelButtons.Controls.Add(LnkAsignar)
        PanelButtons.Controls.Add(LnkDesasignar)
        PanelButtons.Dock = System.Windows.Forms.DockStyle.Top
        PanelButtons.Location = New System.Drawing.Point(0, 32)
        PanelButtons.Name = "PanelButtons"
        PanelButtons.Size = New System.Drawing.Size(784, 72)
        PanelButtons.TabIndex = 9
        '
        'lnkRenoveExpireDate
        '
        lnkRenoveExpireDate.ActiveLinkColor = System.Drawing.Color.SlateGray
        lnkRenoveExpireDate.BackColor = System.Drawing.Color.Transparent
        lnkRenoveExpireDate.Cursor = System.Windows.Forms.Cursors.Hand
        lnkRenoveExpireDate.DisabledLinkColor = System.Drawing.Color.LightSteelBlue
        lnkRenoveExpireDate.Font = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lnkRenoveExpireDate.ForeColor = System.Drawing.Color.MidnightBlue
        lnkRenoveExpireDate.Image = CType(resources.GetObject("lnkRenoveExpireDate.Image"), System.Drawing.Image)
        lnkRenoveExpireDate.ImageAlign = ContentAlignment.TopCenter
        lnkRenoveExpireDate.LinkBehavior = LinkBehavior.NeverUnderline
        lnkRenoveExpireDate.LinkColor = System.Drawing.Color.MidnightBlue
        lnkRenoveExpireDate.Location = New System.Drawing.Point(288, 4)
        lnkRenoveExpireDate.Name = "lnkRenoveExpireDate"
        lnkRenoveExpireDate.Size = New System.Drawing.Size(68, 60)
        lnkRenoveExpireDate.TabIndex = 60
        lnkRenoveExpireDate.TabStop = True
        lnkRenoveExpireDate.Text = "Renovar Vencimiento"
        lnkRenoveExpireDate.TextAlign = ContentAlignment.BottomCenter
        '
        'lnkDistribuir
        '
        lnkDistribuir.ActiveLinkColor = System.Drawing.Color.SlateGray
        lnkDistribuir.BackColor = System.Drawing.Color.Transparent
        lnkDistribuir.Cursor = System.Windows.Forms.Cursors.Hand
        lnkDistribuir.DisabledLinkColor = System.Drawing.Color.LightSteelBlue
        lnkDistribuir.Font = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lnkDistribuir.ForeColor = System.Drawing.Color.MidnightBlue
        lnkDistribuir.Image = CType(resources.GetObject("lnkDistribuir.Image"), System.Drawing.Image)
        lnkDistribuir.ImageAlign = ContentAlignment.TopCenter
        lnkDistribuir.LinkBehavior = LinkBehavior.NeverUnderline
        lnkDistribuir.LinkColor = System.Drawing.Color.MidnightBlue
        lnkDistribuir.Location = New System.Drawing.Point(220, 8)
        lnkDistribuir.Name = "lnkDistribuir"
        lnkDistribuir.Size = New System.Drawing.Size(52, 52)
        lnkDistribuir.TabIndex = 59
        lnkDistribuir.TabStop = True
        lnkDistribuir.Text = "Distribuir"
        lnkDistribuir.TextAlign = ContentAlignment.BottomCenter
        '
        'lnkHistorial
        '
        lnkHistorial.ActiveLinkColor = System.Drawing.Color.SlateGray
        lnkHistorial.BackColor = System.Drawing.Color.Transparent
        lnkHistorial.Cursor = System.Windows.Forms.Cursors.Hand
        lnkHistorial.DisabledLinkColor = System.Drawing.Color.LightSteelBlue
        lnkHistorial.Font = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lnkHistorial.ForeColor = System.Drawing.Color.MidnightBlue
        lnkHistorial.Image = CType(resources.GetObject("lnkHistorial.Image"), System.Drawing.Image)
        lnkHistorial.ImageAlign = ContentAlignment.TopCenter
        lnkHistorial.LinkBehavior = LinkBehavior.NeverUnderline
        lnkHistorial.LinkColor = System.Drawing.Color.MidnightBlue
        lnkHistorial.Location = New System.Drawing.Point(368, 8)
        lnkHistorial.Name = "lnkHistorial"
        lnkHistorial.Size = New System.Drawing.Size(52, 52)
        lnkHistorial.TabIndex = 58
        lnkHistorial.TabStop = True
        lnkHistorial.Text = "Historial"
        lnkHistorial.TextAlign = ContentAlignment.BottomCenter
        '
        'lnkQuitar
        '
        lnkQuitar.ActiveLinkColor = System.Drawing.Color.SlateGray
        lnkQuitar.BackColor = System.Drawing.Color.Transparent
        lnkQuitar.Cursor = System.Windows.Forms.Cursors.Hand
        lnkQuitar.DisabledLinkColor = System.Drawing.Color.LightSteelBlue
        lnkQuitar.Font = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lnkQuitar.ForeColor = System.Drawing.Color.MidnightBlue
        lnkQuitar.Image = CType(resources.GetObject("lnkQuitar.Image"), System.Drawing.Image)
        lnkQuitar.ImageAlign = ContentAlignment.TopCenter
        lnkQuitar.LinkBehavior = LinkBehavior.NeverUnderline
        lnkQuitar.LinkColor = System.Drawing.Color.MidnightBlue
        lnkQuitar.Location = New System.Drawing.Point(156, 16)
        lnkQuitar.Name = "lnkQuitar"
        lnkQuitar.Size = New System.Drawing.Size(52, 44)
        lnkQuitar.TabIndex = 55
        lnkQuitar.TabStop = True
        lnkQuitar.Text = "Quitar"
        lnkQuitar.TextAlign = ContentAlignment.BottomCenter
        '
        'LnkAsignar
        '
        LnkAsignar.ActiveLinkColor = System.Drawing.Color.SlateGray
        LnkAsignar.BackColor = System.Drawing.Color.Transparent
        LnkAsignar.Cursor = System.Windows.Forms.Cursors.Hand
        LnkAsignar.DisabledLinkColor = System.Drawing.Color.LightSteelBlue
        LnkAsignar.Font = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        LnkAsignar.ForeColor = System.Drawing.Color.MidnightBlue
        LnkAsignar.Image = CType(resources.GetObject("LnkAsignar.Image"), System.Drawing.Image)
        LnkAsignar.ImageAlign = ContentAlignment.TopCenter
        LnkAsignar.LinkBehavior = LinkBehavior.NeverUnderline
        LnkAsignar.LinkColor = System.Drawing.Color.MidnightBlue
        LnkAsignar.Location = New System.Drawing.Point(20, 12)
        LnkAsignar.Name = "LnkAsignar"
        LnkAsignar.Size = New System.Drawing.Size(52, 48)
        LnkAsignar.TabIndex = 56
        LnkAsignar.TabStop = True
        LnkAsignar.Text = "Asignar"
        LnkAsignar.TextAlign = ContentAlignment.BottomCenter
        '
        'LnkDesasignar
        '
        LnkDesasignar.ActiveLinkColor = System.Drawing.Color.SlateGray
        LnkDesasignar.BackColor = System.Drawing.Color.Transparent
        LnkDesasignar.Cursor = System.Windows.Forms.Cursors.Hand
        LnkDesasignar.DisabledLinkColor = System.Drawing.Color.LightSteelBlue
        LnkDesasignar.Font = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        LnkDesasignar.ForeColor = System.Drawing.Color.MidnightBlue
        LnkDesasignar.Image = CType(resources.GetObject("LnkDesasignar.Image"), System.Drawing.Image)
        LnkDesasignar.ImageAlign = ContentAlignment.TopCenter
        LnkDesasignar.LinkBehavior = LinkBehavior.NeverUnderline
        LnkDesasignar.LinkColor = System.Drawing.Color.MidnightBlue
        LnkDesasignar.Location = New System.Drawing.Point(84, 12)
        LnkDesasignar.Name = "LnkDesasignar"
        LnkDesasignar.Size = New System.Drawing.Size(64, 48)
        LnkDesasignar.TabIndex = 57
        LnkDesasignar.TabStop = True
        LnkDesasignar.Text = "Desasignar"
        LnkDesasignar.TextAlign = ContentAlignment.BottomCenter
        '
        'btnShowDocument
        '
        btnShowDocument.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnShowDocument.BackColor = System.Drawing.Color.LightSlateGray
        btnShowDocument.Cursor = System.Windows.Forms.Cursors.Hand
        btnShowDocument.FlatStyle = FlatStyle.Popup
        btnShowDocument.Font = New Font("Verdana", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        btnShowDocument.ForeColor = System.Drawing.Color.White
        btnShowDocument.Location = New System.Drawing.Point(572, 4)
        btnShowDocument.Name = "btnShowDocument"
        btnShowDocument.Size = New System.Drawing.Size(88, 24)
        btnShowDocument.TabIndex = 10
        btnShowDocument.Text = "Documento"
        btnShowDocument.UseVisualStyleBackColor = False
        '
        'btnShowRules
        '
        btnShowRules.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnShowRules.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        btnShowRules.Cursor = System.Windows.Forms.Cursors.Hand
        btnShowRules.FlatStyle = FlatStyle.Popup
        btnShowRules.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnShowRules.Location = New System.Drawing.Point(668, 4)
        btnShowRules.Name = "btnShowRules"
        btnShowRules.Size = New System.Drawing.Size(84, 23)
        btnShowRules.TabIndex = 11
        btnShowRules.Text = "Reglas"
        btnShowRules.UseVisualStyleBackColor = False
        '
        'ImageList1
        '
        '   Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        '   Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '   Me.ImageList1.Images.SetKeyName(0, "")
        '   Me.ImageList1.Images.SetKeyName(1, "")
        ''   Me.ImageList1.Images.SetKeyName(2, "")
        '  Me.ImageList1.Images.SetKeyName(3, "")
        '
        'lblActualizando
        '
        lblActualizando.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(254, Byte), Integer))
        lblActualizando.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lblActualizando.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblActualizando.Location = New System.Drawing.Point(132, 6)
        lblActualizando.Name = "lblActualizando"
        lblActualizando.Size = New System.Drawing.Size(152, 20)
        lblActualizando.TabIndex = 12
        lblActualizando.Text = "Actualizando en 60 seg."
        '
        'txtSegundos
        '
        txtSegundos.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtSegundos.Location = New System.Drawing.Point(398, 6)
        txtSegundos.Name = "txtSegundos"
        txtSegundos.Size = New System.Drawing.Size(32, 22)
        txtSegundos.TabIndex = 13
        txtSegundos.Text = "60"
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(254, Byte), Integer))
        Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label2.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(296, 6)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(100, 20)
        Label2.TabIndex = 14
        Label2.Text = "Actualizar cada:"
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(254, Byte), Integer))
        Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label3.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(431, 6)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(35, 20)
        Label3.TabIndex = 15
        Label3.Text = "seg"
        '
        'btnOK
        '
        btnOK.FlatStyle = FlatStyle.Flat
        btnOK.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnOK.Location = New System.Drawing.Point(470, 6)
        btnOK.Name = "btnOK"
        btnOK.Size = New System.Drawing.Size(32, 20)
        btnOK.TabIndex = 16
        btnOK.Text = "OK"
        '
        'ControllerMonitor
        '
        BackColor = System.Drawing.Color.LightSteelBlue
        Controls.Add(btnOK)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(txtSegundos)
        Controls.Add(lblActualizando)
        Controls.Add(btnShowRules)
        Controls.Add(btnShowDocument)
        Controls.Add(PanelViewer)
        Controls.Add(Splitter5)
        Controls.Add(PanelGrid)
        Controls.Add(PanelButtons)
        Controls.Add(Label1)
        Name = "ControllerMonitor"
        Size = New System.Drawing.Size(784, 464)
        PanelButtons.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region

    Dim WFNode As WFNode
    Dim CurrentUserId As Int64

    Public Sub New(ByVal WFNode As WFNode, ByVal CurrentUserId As Int64)
        MyBase.New()
        InitializeComponent()
        Me.CurrentUserId = CurrentUserId
        LoadWfNode(WFNode)
    End Sub
    Public Sub New(ByVal CurrentUserId As Int64)
        MyBase.New()
        InitializeComponent()
        Me.CurrentUserId = CurrentUserId
    End Sub
    Public Sub LoadWfNode(ByVal WFNode As WFNode)
        Me.WFNode = WFNode
        LoadGrid()
        LoadRules()
        LoadViewer()
    End Sub


#Region "DisposeControlsBecauseRefreshing"
    Public Sub DisposeControls()
        Try
            If IsNothing(UCDistribuir) = False Then UCDistribuir.Dispose()
            If IsNothing(UCExpire) = False Then UCExpire.Dispose()
            If IsNothing(ucAssign) = False Then ucAssign.Dispose()
            If IsNothing(ucRemove) = False Then ucRemove.Dispose()
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Grid"
    Friend WithEvents UCTaskGrid As UCTaskGrid
    Private Sub LoadGrid()
        Try
            UCTaskGrid = New UCTaskGrid(CurrentUserId)
            UCTaskGrid.Dock = DockStyle.Fill
            PanelGrid.Controls.Add(UCTaskGrid)
            UCTaskGrid.BringToFront()

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Rules"
    Friend ControllerRules As ControllerRules
    Private Sub LoadRules()
        Try
            ControllerRules = New ControllerRules
            ControllerRules.Dock = DockStyle.Fill
            PanelViewer.Controls.Add(ControllerRules)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Viewer"
    Friend ControllerViewer As ControllerViewer
    Private Sub LoadViewer()
        Try
            ControllerViewer = New ControllerViewer
            ControllerViewer.Dock = DockStyle.Fill
            PanelViewer.Controls.Add(ControllerViewer)
            ControllerViewer.BringToFront()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ShowResult(ByRef TaskResult As ITaskResult)
        Try
            ControllerViewer.TaskResult = TaskResult
            ControllerViewer.ShowImage()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Show Document/Rules"
    Private Sub btnShowDocument_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnShowDocument.Click
        Try
            SetDesignButtons(btnShowDocument, btnShowRules)
            ControllerViewer.BringToFront()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnShowRules_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnShowRules.Click
        Try
            SetDesignButtons(btnShowRules, btnShowDocument)
            ControllerRules.BringToFront()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Shared Sub SetDesignButtons(ByVal SelectButton As Button, ByVal UnSelectButton As Button)
        SelectButton.BackColor = System.Drawing.Color.LightSlateGray
        SelectButton.Font = New Font("Verdana", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        SelectButton.ForeColor = System.Drawing.Color.White
        UnSelectButton.BackColor = System.Drawing.Color.FromArgb(CType(198, Byte), CType(222, Byte), CType(247, Byte))
        UnSelectButton.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        UnSelectButton.ForeColor = Color.FromArgb(76, 76, 76)
    End Sub
#End Region

#Region "Actions"

#Region "Asignar"
    Friend WithEvents ucAssign As UCAsignar
    Private Sub LnkAsignar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LnkAsignar.Click
        Try
            Dim selTaskResExtern As List(Of GridTaskResult) = UCTaskGrid.SelectedTaskResultsExtern(True)

            If selTaskResExtern IsNot Nothing AndAlso selTaskResExtern.Count > 0 Then
                Dim TaskResult As ITaskResult '= WFTaskBusiness.GetTask(selTaskResExtern(0).TaskId, selTaskResExtern(0).doctypeid, selTaskResExtern(0).StepId)

                ucAssign = New UCAsignar(DirectCast(TaskResult, TaskResult), UCAsignar.AsignTypes.Asignar)
                Dim ucAssignLocation As New Point
                ucAssignLocation.X = LnkAsignar.Location.X
                ucAssignLocation.Y = LnkAsignar.Location.Y + LnkAsignar.Height + 35
                ucAssign.Location = ucAssignLocation
                Controls.Add(ucAssign)
                ucAssign.BringToFront()
                ucAssign.Focus()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Desasignar"
    Public Shared Event UnassignedTask()
    Private Sub LnkDesasignar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LnkDesasignar.Click

        Dim selTaskResExtern As List(Of GridTaskResult)

        Try
            selTaskResExtern = UCTaskGrid.SelectedTaskResultsExtern(True)

            If selTaskResExtern IsNot Nothing Then
                For Each task As GridTaskResult In selTaskResExtern
                    'Dim task As TaskResult = DirectCast(lvItem, ListViewItemTask).Result
                    If task.TaskState <> Zamba.Core.TaskStates.Desasignada Then
                        ' WFTaskBusiness.UnAssign(WFTaskBusiness.GetTask(task.TaskId, task.doctypeid, task.StepId), UserBusiness.Rights.CurrentUser)
                        RaiseEvent UnassignedTask()
                    End If
                Next
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            selTaskResExtern = Nothing
        End Try

    End Sub
#End Region

#Region "Quitar"
    Dim WithEvents ucRemove As UcRemove
    Private Sub lnkQuitar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkQuitar.Click
        Try
            If UCTaskGrid.SelectedTaskResultsExtern(True).Count > 0 Then
                ucRemove = New UcRemove
                Dim p As New Point
                p.X = lnkQuitar.Location.X
                p.Y = lnkQuitar.Location.Y + lnkQuitar.Height + 35
                ucRemove.Location = p
                Controls.Add(ucRemove)
                ucRemove.BringToFront()
                ucRemove.Focus()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub DeleteDocument() Handles ucRemove.DeleteAll
        Try
            For Each t As GridTaskResult In UCTaskGrid.SelectedTaskResultsExtern(True)
                If t.TaskState = Zamba.Core.TaskStates.Desasignada Then
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region


#Region "Distribuir"
    Friend WithEvents UCDistribuir As UCDistribuir


    Private Sub RaiseRefreshWf()
        RaiseEvent RefreshWF()
    End Sub

    Public Event RefreshWF()
    Public Event SetTotal(ByVal Total As Int32)

    Private Sub UCDistribuir_Derivate(ByRef Result As TaskResult, ByVal NewWFStep As WFStep) Handles UCDistribuir.Derivate
        Try
            'For Each t As GridTaskResult In Me.UCTaskGrid.SelectedResults(True)
            '    'Dim t As TaskResult = DirectCast(lvi, ListViewItemTask).Result
            If Result.TaskState = Zamba.Core.TaskStates.Desasignada Then
                'Derivo
                WFTaskBusiness.Distribute(New List(Of ITaskResult)(Result), NewWFStep.ID, CurrentUserId)
            ElseIf UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.allowExecuteTasksAssignedToOtherUsers, CType(Result.StepId, Int32)) Then
                WFTaskBusiness.Distribute(New List(Of ITaskResult)(Result), NewWFStep.ID, CurrentUserId)
            End If

            'Next

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Historial"
    Dim WithEvents UCHistory As UCTaskHistory
    Dim F As ZForm
    Private Sub lnkHistorial_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkHistorial.Click
        Try
            If UCTaskGrid.SelectedTaskResultsExtern(True).Count > 0 Then
                Dim ResultId As Integer = CInt(UCTaskGrid.SelectedTaskResultsExtern(True)(0).ID)
                Dim doctypeid As Integer = CInt(UCTaskGrid.SelectedTaskResultsExtern(True)(0).doctypeid)
                UCHistory = New UCTaskHistory(ResultId, CurrentUserId, False, doctypeid)
                UCHistory.Dock = DockStyle.Fill
                F = New ZForm
                F.Name = "Historial"
                F.FormBorderStyle = FormBorderStyle.Sizable
                F.Width = UCHistory.Width + 10
                F.Height = UCHistory.Height + 45
                F.StartPosition = FormStartPosition.CenterScreen
                F.ShowInTaskbar = False
                F.Controls.Add(UCHistory)
                F.ShowDialog()
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Renovar Vencimiento"
    Friend WithEvents UCExpire As UCExpiredDate

    Private Sub UCExpire_ChangeExpireDate(ByRef Result As TaskResult, ByVal NewDate As DateTime) Handles UCExpire.ChangeExpireDate
        Try
            'For Each task As GridTaskResult In Me.UCTaskGrid.SelectedResults(True)
            '    'CambioVencimiento
            WFTaskBusiness.ChangeExpireDate(Result, NewDate)
            'Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#End Region

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnOK.Click
        If IsNumeric(txtSegundos.Text) AndAlso CInt(txtSegundos.Text) >= 10 Then
            RaiseEvent SetTotal(CInt(txtSegundos.Text))
        Else
            MessageBox.Show("DEBE COMPLETAR UNA CANTIDAD DE SEGUNDOS VALIDA Y SUPERIOR O IGUAL A 10 SEGUNDOS")
            txtSegundos.Text = "60"
        End If
    End Sub
End Class
