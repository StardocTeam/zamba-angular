Imports ZAMBA.Core

Public Class WFServiceIDE
    Inherits ZForm

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

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents Panel4 As ZPanel
    Friend WithEvents Panel5 As ZPanel
    Friend WithEvents Splitter3 As zsplitter
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents Panel6 As ZPanel
    Friend WithEvents Splitter5 As zsplitter
    Friend WithEvents Splitter6 As zsplitter
    Friend WithEvents Panel7 As ZPanel
    Friend WithEvents Splitter1 As zsplitter
    Friend WithEvents Splitter4 As zsplitter
    Friend WithEvents BtnInterval As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnstart As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnstop As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnStartrefresh As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnstartschedule As System.Windows.Forms.ToolBarButton
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblstatus As ZLabel
    Friend WithEvents lblwfsrunning As ZLabel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents Lbliniciado As ZLabel
    Friend WithEvents Lblreiniciado As ZLabel
    Friend WithEvents Lblactualizacion As ZLabel
    Friend WithEvents lbldetenido As ZLabel
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents lstServices As ListBox
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New ComponentModel.Container
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(WFServiceIDE))
        Panel1 = New ZPanel
        Label5 = New ZLabel
        lblwfsrunning = New ZLabel
        lblstatus = New ZLabel
        PictureBox1 = New System.Windows.Forms.PictureBox
        ToolBar1 = New System.Windows.Forms.ToolBar
        BtnInterval = New System.Windows.Forms.ToolBarButton
        btnstart = New System.Windows.Forms.ToolBarButton
        btnstop = New System.Windows.Forms.ToolBarButton
        btnStartrefresh = New System.Windows.Forms.ToolBarButton
        btnstartschedule = New System.Windows.Forms.ToolBarButton
        ImageList1 = New ImageList(components)
        Panel4 = New ZPanel
        Panel5 = New ZPanel
        lstServices = New ListBox
        ListView1 = New System.Windows.Forms.ListView
        ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Splitter3 = New ZSplitter
        Panel6 = New ZPanel
        lbldetenido = New ZLabel
        Lblactualizacion = New ZLabel
        Lblreiniciado = New ZLabel
        Lbliniciado = New ZLabel
        Label4 = New ZLabel
        Label1 = New ZLabel
        Label2 = New ZLabel
        Label3 = New ZLabel
        Splitter5 = New ZSplitter
        Splitter6 = New ZSplitter
        Panel7 = New ZPanel
        Splitter1 = New ZSplitter
        Splitter4 = New ZSplitter
        Panel1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel5.SuspendLayout()
        Panel6.SuspendLayout()
        Panel7.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.AllowDrop = True
        Panel1.AutoScroll = True


        Panel1.Controls.Add(Label5)
        Panel1.Controls.Add(lblwfsrunning)
        Panel1.Controls.Add(lblstatus)
        Panel1.Controls.Add(PictureBox1)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(2, 64)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(559, 130)
        Panel1.TabIndex = 0
        '
        'Label5
        '
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Font = New Font("Lucida Console", 15.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label5.ForeColor = System.Drawing.Color.SlateGray
        Label5.Location = New System.Drawing.Point(368, 8)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(176, 32)
        Label5.TabIndex = 3
        Label5.Text = "00:00"
        Label5.TextAlign = ContentAlignment.MiddleCenter
        '
        'lblwfsrunning
        '
        lblwfsrunning.BackColor = System.Drawing.Color.Transparent
        lblwfsrunning.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        lblwfsrunning.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        lblwfsrunning.Location = New System.Drawing.Point(72, 72)
        lblwfsrunning.Name = "lblwfsrunning"
        lblwfsrunning.Size = New System.Drawing.Size(272, 24)
        lblwfsrunning.TabIndex = 2
        lblwfsrunning.Text = "WorkFlows en Ejecucion: 0"
        lblwfsrunning.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblstatus
        '
        lblstatus.BackColor = System.Drawing.Color.Transparent
        lblstatus.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        lblstatus.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        lblstatus.Location = New System.Drawing.Point(72, 32)
        lblstatus.Name = "lblstatus"
        lblstatus.Size = New System.Drawing.Size(216, 24)
        lblstatus.TabIndex = 1
        lblstatus.Text = "Servicio detenido"
        lblstatus.TextAlign = ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        PictureBox1.BackColor = System.Drawing.Color.Transparent
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        PictureBox1.Location = New System.Drawing.Point(8, 32)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New System.Drawing.Size(48, 64)
        PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        '
        'ToolBar1
        '
        ToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {BtnInterval, btnstart, btnstop, btnStartrefresh, btnstartschedule})
        ToolBar1.ButtonSize = New System.Drawing.Size(110, 50)
        ToolBar1.Divider = False
        ToolBar1.DropDownArrows = True
        ToolBar1.ImageList = ImageList1
        ToolBar1.Location = New System.Drawing.Point(2, 2)
        ToolBar1.Name = "ToolBar1"
        ToolBar1.ShowToolTips = True
        ToolBar1.Size = New System.Drawing.Size(916, 56)
        ToolBar1.TabIndex = 4
        '
        'BtnInterval
        '
        BtnInterval.ImageIndex = 3
        BtnInterval.Name = "BtnInterval"
        BtnInterval.Tag = "interval"
        BtnInterval.Text = "Intervalo"
        BtnInterval.ToolTipText = "Asignar el intervalo de actualizacion"
        BtnInterval.Visible = False
        '
        'btnstart
        '
        btnstart.ImageIndex = 5
        btnstart.Name = "btnstart"
        btnstart.Tag = "start"
        btnstart.Text = "Iniciar Servicio"
        btnstart.ToolTipText = "Iniciar Servicio"
        '
        'btnstop
        '
        btnstop.ImageIndex = 6
        btnstop.Name = "btnstop"
        btnstop.Tag = "stop"
        btnstop.Text = "Detener Servicio"
        btnstop.ToolTipText = "Detener Servicio"
        '
        'btnStartrefresh
        '
        btnStartrefresh.ImageIndex = 1
        btnStartrefresh.Name = "btnStartrefresh"
        btnStartrefresh.Tag = "startrefresh"
        btnStartrefresh.Text = "Iniciar actualizacion"
        btnStartrefresh.ToolTipText = "Iniciar actualizacion"
        btnStartrefresh.Visible = False
        '
        'btnstartschedule
        '
        btnstartschedule.ImageIndex = 4
        btnstartschedule.Name = "btnstartschedule"
        btnstartschedule.Tag = "startschedule"
        btnstartschedule.Text = "Iniciar planificacion"
        btnstartschedule.ToolTipText = "Iniciar planificacion"
        btnstartschedule.Visible = False
        '
        'ImageList1
        '
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        ImageList1.TransparentColor = System.Drawing.Color.Transparent
        ImageList1.Images.SetKeyName(0, "")
        ImageList1.Images.SetKeyName(1, "")
        ImageList1.Images.SetKeyName(2, "")
        ImageList1.Images.SetKeyName(3, "")
        ImageList1.Images.SetKeyName(4, "")
        ImageList1.Images.SetKeyName(5, "")
        ImageList1.Images.SetKeyName(6, "")
        ImageList1.Images.SetKeyName(7, "")
        ImageList1.Images.SetKeyName(8, "")
        '
        'Panel4
        '


        Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Panel4.Location = New System.Drawing.Point(2, 520)
        Panel4.Name = "Panel4"
        Panel4.Size = New System.Drawing.Size(916, 52)
        Panel4.TabIndex = 3
        '
        'Panel5
        '
        Panel5.Controls.Add(lstServices)
        Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Panel5.Location = New System.Drawing.Point(2, 390)
        Panel5.Name = "Panel5"
        Panel5.Size = New System.Drawing.Size(559, 125)
        Panel5.TabIndex = 4
        '
        'lstServices
        '
        lstServices.Dock = System.Windows.Forms.DockStyle.Fill
        lstServices.FormattingEnabled = True
        lstServices.Location = New System.Drawing.Point(0, 0)
        lstServices.Name = "lstServices"
        lstServices.Size = New System.Drawing.Size(559, 121)
        lstServices.TabIndex = 15
        '
        'ListView1
        '
        ListView1.BackColor = System.Drawing.Color.White
        ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader1})
        ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        ListView1.FullRowSelect = True
        ListView1.HideSelection = False
        ListView1.Location = New System.Drawing.Point(0, 0)
        ListView1.Name = "ListView1"
        ListView1.Size = New System.Drawing.Size(352, 451)
        ListView1.TabIndex = 0
        ListView1.UseCompatibleStateImageBehavior = False
        ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        ColumnHeader1.Text = "Detalle"
        ColumnHeader1.Width = 350
        '
        'Splitter3
        '
        Splitter3.BackColor = System.Drawing.Color.SteelBlue
        Splitter3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Splitter3.Dock = System.Windows.Forms.DockStyle.Bottom
        Splitter3.Location = New System.Drawing.Point(2, 384)
        Splitter3.Name = "Splitter3"
        Splitter3.Size = New System.Drawing.Size(559, 6)
        Splitter3.TabIndex = 7
        Splitter3.TabStop = False
        '
        'Panel6
        '
        Panel6.Controls.Add(lbldetenido)
        Panel6.Controls.Add(Lblactualizacion)
        Panel6.Controls.Add(Lblreiniciado)
        Panel6.Controls.Add(Lbliniciado)
        Panel6.Controls.Add(Label4)
        Panel6.Controls.Add(Label1)
        Panel6.Controls.Add(Label2)
        Panel6.Controls.Add(Label3)
        Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Panel6.Location = New System.Drawing.Point(2, 200)
        Panel6.Name = "Panel6"
        Panel6.Size = New System.Drawing.Size(559, 184)
        Panel6.TabIndex = 9
        '
        'lbldetenido
        '
        lbldetenido.BackColor = System.Drawing.Color.Transparent
        lbldetenido.Location = New System.Drawing.Point(176, 48)
        lbldetenido.Name = "lbldetenido"
        lbldetenido.Size = New System.Drawing.Size(344, 23)
        lbldetenido.TabIndex = 10
        lbldetenido.TextAlign = ContentAlignment.MiddleLeft
        '
        'Lblactualizacion
        '
        Lblactualizacion.BackColor = System.Drawing.Color.Transparent
        Lblactualizacion.Location = New System.Drawing.Point(176, 112)
        Lblactualizacion.Name = "Lblactualizacion"
        Lblactualizacion.Size = New System.Drawing.Size(344, 23)
        Lblactualizacion.TabIndex = 9
        Lblactualizacion.TextAlign = ContentAlignment.MiddleLeft
        '
        'Lblreiniciado
        '
        Lblreiniciado.BackColor = System.Drawing.Color.Transparent
        Lblreiniciado.Location = New System.Drawing.Point(176, 75)
        Lblreiniciado.Name = "Lblreiniciado"
        Lblreiniciado.Size = New System.Drawing.Size(344, 23)
        Lblreiniciado.TabIndex = 8
        Lblreiniciado.TextAlign = ContentAlignment.MiddleLeft
        '
        'Lbliniciado
        '
        Lbliniciado.BackColor = System.Drawing.Color.Transparent
        Lbliniciado.Location = New System.Drawing.Point(176, 16)
        Lbliniciado.Name = "Lbliniciado"
        Lbliniciado.Size = New System.Drawing.Size(344, 23)
        Lbliniciado.TabIndex = 7
        Lbliniciado.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label4.Location = New System.Drawing.Point(16, 112)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(152, 24)
        Label4.TabIndex = 6
        Label4.Text = "Ultima Actualizacion:"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label1.Location = New System.Drawing.Point(16, 16)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(152, 24)
        Label1.TabIndex = 3
        Label1.Text = "Servicio iniciado:"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label2.Location = New System.Drawing.Point(16, 48)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(152, 24)
        Label2.TabIndex = 4
        Label2.Text = "Servicio Detenido:"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label3.Location = New System.Drawing.Point(16, 80)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(152, 24)
        Label3.TabIndex = 5
        Label3.Text = "Servicio Reiniciado:"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'Splitter5
        '
        Splitter5.BackColor = System.Drawing.Color.SteelBlue
        Splitter5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Splitter5.Dock = System.Windows.Forms.DockStyle.Bottom
        Splitter5.Location = New System.Drawing.Point(2, 515)
        Splitter5.Name = "Splitter5"
        Splitter5.Size = New System.Drawing.Size(916, 5)
        Splitter5.TabIndex = 10
        Splitter5.TabStop = False
        '
        'Splitter6
        '
        Splitter6.BackColor = System.Drawing.Color.SteelBlue
        Splitter6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Splitter6.Dock = System.Windows.Forms.DockStyle.Bottom
        Splitter6.Location = New System.Drawing.Point(2, 194)
        Splitter6.Name = "Splitter6"
        Splitter6.Size = New System.Drawing.Size(559, 6)
        Splitter6.TabIndex = 11
        Splitter6.TabStop = False
        '
        'Panel7
        '
        Panel7.Controls.Add(ListView1)
        Panel7.Dock = System.Windows.Forms.DockStyle.Right
        Panel7.Location = New System.Drawing.Point(566, 64)
        Panel7.Name = "Panel7"
        Panel7.Size = New System.Drawing.Size(352, 451)
        Panel7.TabIndex = 12
        '
        'Splitter1
        '
        Splitter1.BackColor = System.Drawing.Color.SteelBlue
        Splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Splitter1.Dock = System.Windows.Forms.DockStyle.Right
        Splitter1.Location = New System.Drawing.Point(561, 64)
        Splitter1.Name = "Splitter1"
        Splitter1.Size = New System.Drawing.Size(5, 451)
        Splitter1.TabIndex = 13
        Splitter1.TabStop = False
        '
        'Splitter4
        '
        Splitter4.BackColor = System.Drawing.Color.SteelBlue
        Splitter4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Splitter4.Dock = System.Windows.Forms.DockStyle.Top
        Splitter4.Location = New System.Drawing.Point(2, 58)
        Splitter4.Name = "Splitter4"
        Splitter4.Size = New System.Drawing.Size(916, 6)
        Splitter4.TabIndex = 14
        Splitter4.TabStop = False
        '
        'WFServiceIDE
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        ClientSize = New System.Drawing.Size(920, 574)
        Controls.Add(Panel1)
        Controls.Add(Splitter6)
        Controls.Add(Panel6)
        Controls.Add(Splitter3)
        Controls.Add(Panel5)
        Controls.Add(Splitter1)
        Controls.Add(Panel7)
        Controls.Add(Splitter5)
        Controls.Add(Panel4)
        Controls.Add(Splitter4)
        Controls.Add(ToolBar1)
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Name = "WFServiceIDE"
        Text = "Zamba - Servicio de Workflow"
        WindowState = System.Windows.Forms.FormWindowState.Maximized
        Panel1.ResumeLayout(False)
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel5.ResumeLayout(False)
        Panel6.ResumeLayout(False)
        Panel7.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region

    'Public WfServiceBusiness As WFServiceBusiness
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="WFIds"></param>
    ''' <history>
    '''     [Marcelo]   01/10/2009  Modified     Rules Load Event
    ''' </history>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        RemoveHandler ZBaseCore.ForceLoad, AddressOf LazyLoadBusiness.LoadInstance
        AddHandler ZBaseCore.ForceLoad, AddressOf LazyLoadBusiness.LoadInstance
        'Marcelo: this event loads the rules preferences
        RemoveHandler ZBaseCore.loadRulePreference, AddressOf WFRulesBusiness.FillRulePreference
        AddHandler ZBaseCore.loadRulePreference, AddressOf WFRulesBusiness.FillRulePreference

        lstServices.DataSource = ServiceBusiness.GetServices(ServiceTypes.Workflow)
        lstServices.DisplayMember = "ServiceName"
    End Sub


#Region "FillIde"

    Private Sub WFServiceIDE_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        StopAllService()
    End Sub

    Private Sub WFIDE_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            ShowServiceStatusPicture()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            StartClock()
        Catch ex As Exception
        End Try
    End Sub

#End Region

    Public Event stopTimeOut()
    Public Event activateTimeOut()

    Dim Clock As Threading.Timer
    Dim CB As New Threading.TimerCallback(AddressOf Tick)
    Dim state As Object

    Private Sub StartClock()
        Try
            Clock = New Threading.Timer(CB, state, 0, 60000)
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStateException
        Catch ex As Exception
        End Try
    End Sub
    'Dim Count As Int16
    ''' <summary>
    ''' [sebastian 29-04-09] se agrego trace para mostrar la hora cada cuanto se ejecuta el servicio.
    ''' </summary>
    ''' <param name="State"></param>
    ''' <remarks></remarks>
    Private Sub Tick(ByVal State As Object)
        Try
            Label5.Text = Now.ToLongTimeString
            ShowServiceStatusPicture()
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Comienzo del tick para el servicio: " & DateTime.Now)
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStateException
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ShowServiceStatusPicture()
        If Not IsNothing(service) Then
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            If service.WFServiceBusiness.Running = True Then
                PictureBox1.Image = ImageList1.Images(7)
                lblstatus.Text = "Servicio ejecutandose"
            Else
                PictureBox1.Image = ImageList1.Images(8)
                lblstatus.Text = "Servicio detenido"
            End If
            lblwfsrunning.Text = "WorkFlows en Ejecucion: " & service.WFServiceBusiness.WfServ.WFIds.Count
            Lbliniciado.Text = service.WFServiceBusiness.WfServ.DateStarted
            lbldetenido.Text = service.WFServiceBusiness.WfServ.DateStoped
            Lblreiniciado.Text = service.WFServiceBusiness.WfServ.DateReStarted
            Lblactualizacion.Text = service.WFServiceBusiness.WfServ.DateLastRefresh.ToString
        End If
    End Sub
    Delegate Sub DShowMsg(ByVal Msg As String)
    Private Sub ShowMsg(ByVal Msg As String)
        If ListView1.InvokeRequired Then
            Dim D1 As New DShowMsg(AddressOf ShowMsg)
            Dim objs() As Object = {Msg}
            Invoke(D1, objs)
        Else
            If ListView1.Items.Count > 500 Then
                ListView1.SuspendLayout()
                ListView1.Items.RemoveAt(0)
                ListView1.Items.Add(Now.ToShortTimeString & ": " & Msg)
                ListView1.ResumeLayout()
            Else
                ListView1.Items.Add(Now.ToShortTimeString & ": " & Msg)
            End If
            ListView1.Items(ListView1.Items.Count - 1).EnsureVisible()
        End If
    End Sub
#Region "Intervalo"
    'Private Sub ShowIntervalForm()
    '    Try
    '        Dim ucInterval As New UCInterval(WfServiceBusiness.WfServ.RefreshRate)
    '        RemoveHandler ucInterval.Cancel, AddressOf IntervalCancel
    '        RemoveHandler ucInterval.Save, AddressOf IntervalSave
    '        AddHandler ucInterval.Cancel, AddressOf IntervalCancel
    '        AddHandler ucInterval.Save, AddressOf IntervalSave
    '        Me.Panel5.Controls.Clear()
    '        Me.Panel5.Controls.Add(ucInterval)
    '    Catch ex As Exception
    '        Zamba.Core.ZClass.raiseerror(ex)
    '    End Try
    'End Sub
    Private Sub IntervalCancel()
        Panel5.Controls.Clear()
    End Sub
    'Private Sub IntervalSave(ByVal Interval As Int32)
    '    WfServiceBusiness.RefreshRateChanged(Interval)
    'End Sub
#End Region

#Region "Service Actions"
    Dim service As Service.Service
    ''' <summary>
    ''' Método encargado de iniciar el servicio
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	27/08/2008	Modified
    ''' </history>
    Sub StartAllService()
        If lstServices.SelectedIndex >= 0 Then
            ' Cuando se inicia el servicio de monitoreo se frena el temporizador encargado de verificar si el programa está inactivo
            ' (y que pasado tal tiempo hace que aparezca la pantalla de login)
            RaiseEvent stopTimeOut()

            service = New Service.Service(True)

            If service.LoadService(DirectCast(lstServices.SelectedItem, ServiceObj).ServiceID) = True Then
                AddHandler service.WFServiceBusiness.ShowMsg, AddressOf ShowMsg
                If service.StartService() = False Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha iniciado el servicio")
                Else
                    ShowServiceStatusPicture()
                End If
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha cargado el servicio")
            End If

            'WfServiceBusiness.StartService(ClientType.Service, True, True)
        Else
            MessageBox.Show("Debe seleccionar un servicio de la lista", "Atencion", MessageBoxButtons.OK)
        End If
    End Sub

    ''' <summary>
    ''' Método encargado de pausar el servicio
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	27/08/2008	Modified
    ''' </history>
    Sub StopAllService()
        If Not IsNothing(service) Then
            ' Cuando se pausa el servicio se inicia el temporizador encargado de verificar si el programa está inactivo
            ' (y que pasado tal tiempo hace que aparezca la pantalla de login)
            RaiseEvent activateTimeOut()
            service.ForceStopService()
            ShowServiceStatusPicture()
        End If
    End Sub

    'Sub StartRefresh()
    '    WfServiceBusiness.CheckA(WfServiceBusiness)
    '    ShowServiceStatusPicture()
    'End Sub
    'Sub StartSchedule()
    '    WfServiceBusiness.CheckP(WfServiceBusiness)
    '    ShowServiceStatusPicture()
    'End Sub
#End Region

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case CStr(e.Button.Tag)
            Case "interval"
                'Me.ShowIntervalForm()
            Case "start"
                StartAllService()
            Case "stop"
                StopAllService()
            Case "startrefresh"
                'Me.StartRefresh()
            Case "startschedule"
                'Me.StartSchedule()
        End Select
    End Sub
End Class
