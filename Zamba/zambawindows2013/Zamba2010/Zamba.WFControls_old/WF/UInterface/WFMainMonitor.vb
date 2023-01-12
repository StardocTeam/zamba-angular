Imports Zamba.Data
Imports Zamba.Core
Imports ZAMBA.AppBlock
Imports Zamba.controls

Public Class WFMainMonitor
    Inherits ZForm

#Region " Código generado por el Diseñador de Windows Forms "
    Public Sub New()
        MyBase.New()
        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        _openMode = WFMain.OpenModes.Monitor
    End Sub

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

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents lstWorkflows As System.Windows.Forms.ListBox
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents lbDescripcion As ZLabel
    Friend WithEvents lbAyuda As ZLabel
    Friend WithEvents PanelButtons As System.Windows.Forms.Panel
    Friend WithEvents PanelListBox As System.Windows.Forms.Panel
    Friend WithEvents PanelLeft As System.Windows.Forms.Panel
    Friend WithEvents PanelRight As System.Windows.Forms.Panel
    Friend WithEvents PanelTop As System.Windows.Forms.Panel
    Friend WithEvents PanelBottom As System.Windows.Forms.Panel
    Friend WithEvents btnSalir As Zamba.AppBlock.ZButton
    Friend WithEvents lbTituloDescripcion As ZLabel
    Friend WithEvents lbTituloAyuda As ZLabel
    Friend WithEvents btMonitorear As Zamba.AppBlock.ZButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WFMainMonitor))
        Me.lstWorkflows = New System.Windows.Forms.ListBox
        Me.Panel1 = New Zamba.AppBlock.ZPanel
        Me.PanelListBox = New System.Windows.Forms.Panel
        Me.PanelButtons = New System.Windows.Forms.Panel
        Me.btMonitorear = New Zamba.AppBlock.ZButton
        Me.lbTituloAyuda = New ZLabel
        Me.lbTituloDescripcion = New ZLabel
        Me.btnSalir = New Zamba.AppBlock.ZButton
        Me.lbAyuda = New Zamba.AppBlock.ZLabel
        Me.lbDescripcion = New Zamba.AppBlock.ZLabel
        Me.PanelLeft = New System.Windows.Forms.Panel
        Me.PanelRight = New System.Windows.Forms.Panel
        Me.PanelTop = New System.Windows.Forms.Panel
        Me.PanelBottom = New System.Windows.Forms.Panel
        Me.Panel1.SuspendLayout()
        Me.PanelListBox.SuspendLayout()
        Me.PanelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstWorkflows
        '
        Me.lstWorkflows.BackColor = System.Drawing.Color.White
        Me.lstWorkflows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstWorkflows.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstWorkflows.ItemHeight = 16
        Me.lstWorkflows.Location = New System.Drawing.Point(0, 0)
        Me.lstWorkflows.Name = "lstWorkflows"
        Me.lstWorkflows.Size = New System.Drawing.Size(264, 372)
        Me.lstWorkflows.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PanelListBox)
        Me.Panel1.Controls.Add(Me.PanelButtons)
        Me.Panel1.Controls.Add(Me.PanelLeft)
        Me.Panel1.Controls.Add(Me.PanelRight)
        Me.Panel1.Controls.Add(Me.PanelTop)
        Me.Panel1.Controls.Add(Me.PanelBottom)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(460, 396)
        Me.Panel1.TabIndex = 3
        '
        'PanelListBox
        '
        Me.PanelListBox.BackColor = System.Drawing.Color.Transparent
        Me.PanelListBox.Controls.Add(Me.lstWorkflows)
        Me.PanelListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelListBox.Location = New System.Drawing.Point(5, 5)
        Me.PanelListBox.Name = "PanelListBox"
        Me.PanelListBox.Size = New System.Drawing.Size(264, 384)
        Me.PanelListBox.TabIndex = 12
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.Color.Transparent
        Me.PanelButtons.Controls.Add(Me.btMonitorear)
        Me.PanelButtons.Controls.Add(Me.lbTituloAyuda)
        Me.PanelButtons.Controls.Add(Me.lbTituloDescripcion)
        Me.PanelButtons.Controls.Add(Me.btnSalir)
        Me.PanelButtons.Controls.Add(Me.lbAyuda)
        Me.PanelButtons.Controls.Add(Me.lbDescripcion)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelButtons.Location = New System.Drawing.Point(269, 5)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(184, 384)
        Me.PanelButtons.TabIndex = 13
        '
        'btMonitorear
        '
        Me.btMonitorear.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btMonitorear.Location = New System.Drawing.Point(24, 314)
        Me.btMonitorear.Name = "btMonitorear"
        Me.btMonitorear.Size = New System.Drawing.Size(136, 26)
        Me.btMonitorear.TabIndex = 11
        Me.btMonitorear.Text = "Monitorear"
        '
        'lbTituloAyuda
        '
        Me.lbTituloAyuda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbTituloAyuda.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTituloAyuda.Location = New System.Drawing.Point(24, 235)
        Me.lbTituloAyuda.Name = "lbTituloAyuda"
        Me.lbTituloAyuda.Size = New System.Drawing.Size(136, 16)
        Me.lbTituloAyuda.TabIndex = 10
        Me.lbTituloAyuda.Text = "Ayuda"
        Me.lbTituloAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbTituloDescripcion
        '
        Me.lbTituloDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbTituloDescripcion.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTituloDescripcion.Location = New System.Drawing.Point(24, 163)
        Me.lbTituloDescripcion.Name = "lbTituloDescripcion"
        Me.lbTituloDescripcion.Size = New System.Drawing.Size(136, 16)
        Me.lbTituloDescripcion.TabIndex = 9
        Me.lbTituloDescripcion.Text = "Descripcion"
        Me.lbTituloDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSalir
        '
        Me.btnSalir.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSalir.Location = New System.Drawing.Point(24, 346)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(136, 26)
        Me.btnSalir.TabIndex = 7
        Me.btnSalir.Text = "Salir"
        '
        'lbAyuda
        '
        Me.lbAyuda.BackColor = System.Drawing.Color.Transparent
        Me.lbAyuda.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lbAyuda.ForeColor = System.Drawing.Color.FromArgb(76,76,76)
        Me.lbAyuda.Location = New System.Drawing.Point(32, 251)
        Me.lbAyuda.Name = "lbAyuda"
        Me.lbAyuda.Size = New System.Drawing.Size(128, 48)
        Me.lbAyuda.TabIndex = 6
        Me.lbAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbDescripcion
        '
        Me.lbDescripcion.BackColor = System.Drawing.Color.Transparent
        Me.lbDescripcion.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lbDescripcion.ForeColor = System.Drawing.Color.FromArgb(76,76,76)
        Me.lbDescripcion.Location = New System.Drawing.Point(32, 179)
        Me.lbDescripcion.Name = "lbDescripcion"
        Me.lbDescripcion.Size = New System.Drawing.Size(128, 48)
        Me.lbDescripcion.TabIndex = 5
        Me.lbDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PanelLeft
        '
        Me.PanelLeft.BackColor = System.Drawing.Color.Transparent
        Me.PanelLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelLeft.Location = New System.Drawing.Point(0, 5)
        Me.PanelLeft.Name = "PanelLeft"
        Me.PanelLeft.Size = New System.Drawing.Size(5, 384)
        Me.PanelLeft.TabIndex = 11
        '
        'PanelRight
        '
        Me.PanelRight.BackColor = System.Drawing.Color.Transparent
        Me.PanelRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelRight.Location = New System.Drawing.Point(453, 5)
        Me.PanelRight.Name = "PanelRight"
        Me.PanelRight.Size = New System.Drawing.Size(5, 384)
        Me.PanelRight.TabIndex = 10
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.Transparent
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(458, 5)
        Me.PanelTop.TabIndex = 9
        '
        'PanelBottom
        '
        Me.PanelBottom.BackColor = System.Drawing.Color.Transparent
        Me.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelBottom.Location = New System.Drawing.Point(0, 389)
        Me.PanelBottom.Name = "PanelBottom"
        Me.PanelBottom.Size = New System.Drawing.Size(458, 5)
        Me.PanelBottom.TabIndex = 8
        '
        'WFMainMonitor
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(464, 400)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WFMainMonitor"
        Me.Text = ""
        Me.Panel1.ResumeLayout(False)
        Me.PanelListBox.ResumeLayout(False)
        Me.PanelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private _openMode As WFMain.OpenModes
    Public Event ShowIde(ByVal workflow As Zamba.Core.WorkFlow, ByVal OpenMode As WFMain.OpenModes, RightType As RightsType)

    Dim DSWF As New List(Of WorkflowAdminDto)

#Region "Load"
    Private Sub WFMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = " Monitor \ Selección de WorkFlow"
        FillWF()
    End Sub
    Sub FillWF()
        Try
            DsWf = WFBusiness.GetWFsByUserRightMONITORING(Membership.MembershipHelper.CurrentUser.ID)
            lstWorkflows.DataSource = DSWF
            lstWorkflows.DisplayMember = "Name"
            lstWorkflows.ValueMember = "Work_ID"
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Eventos Botones"
    Private Sub btnMonitorear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btMonitorear.Click
        Try
            OpenWf()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ListBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstWorkflows.DoubleClick
        OpenWf()
    End Sub
    Private Sub OpenWf()
        Try
            If Me.lstWorkflows.SelectedIndex <> -1 Then
                Dim WFRow As WorkflowAdminDto = DSWF(Me.lstWorkflows.SelectedIndex)
                WF = WFFactory.GetWf(WFRow)
                RaiseEvent ShowIde(WF, _openMode, WFRow.Right)
                Me.Close()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Dim WF As Zamba.Core.WorkFlow
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstWorkflows.SelectedIndexChanged
        Try
            If Me.lstWorkflows.SelectedIndex <> -1 Then
                Dim WFRow As WorkflowAdminDto = DSWF(Me.lstWorkflows.SelectedIndex)
                WF = WFFactory.GetWf(WFRow)
                Me.lbDescripcion.Text = WF.Description
                Me.lbAyuda.Text = WF.Help
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
#End Region

End Class
