Imports Zamba.Membership
Imports Zamba.Core.Search
Imports Zamba.Controls
Imports Zamba.AdminControls
Imports Zamba.Core
Imports Microsoft.Win32
Imports System.Threading
Imports System.Reflection
Imports System.IO
Imports Zamba.ZTimers
Imports System.Data
Imports Zamba.PreLoad
Imports Zamba.Debugger
Imports Zamba.WFActivity.Regular
Imports System.Collections.Generic
Imports Zamba.Tools
Imports System.ComponentModel
Imports Zamba.QuickSearch.frmQuickSearch
Imports Zamba.QuickSearch
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Zamba.Framework
Imports System.Net

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Cliente
''' Class	 : Client.MainForm
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Formulario Principal del módulo Zamba Cliente
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' 	[Marcelo]	14/04/2008	Modified
'''     [Gaston]    Aprox. 05/05/2008  Inicio de Modificación
''' </history>
''' -----------------------------------------------------------------------------
Public Class MainForm
    Inherits ZForm
    Implements IContainer

    Dim FlagDoClose As Boolean = True
    Dim HashMensajes As New Hashtable
    Dim cerrando As Boolean
    Dim FirstTimeLoaded As Boolean = True
    Dim DynamicButtonsLoaded As Boolean
    Dim CTCB As New Threading.TimerCallback(AddressOf TimeOut)
    Private TimeOutFlag As Boolean
    Dim ConnectionTimer As ZTimer

    Friend WithEvents DocumentoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    Friend WithEvents WordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PowerpointToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    Friend WithEvents ConfigurarPáginaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfigurarImpresoraToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsuarioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents USUARIOToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HistorialToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CambioDeClaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PreferenciasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HerramientasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpcionesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EstaciónToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator

    Friend WithEvents GenerarBarcodeEnWordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PlantillasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AyudaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ManualDeUsuarioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReleaseNotesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SobreZambaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolbarZamba As ZToolBar
    Friend WithEvents BtnSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnResults As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnInsert As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnCaratulas As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnTareas As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnCerrar As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnActiveTaks As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnOptions As ToolStripDropDownButton
    Friend WithEvents DepuradorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnInformes As System.Windows.Forms.ToolStripButton
    Friend WithEvents cleanCacheMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RegistrarZambaEnWindowsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportError As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblpreloadstatus As ToolStripButton
    Friend WithEvents RegistrarBrowserToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnclosezamba As ToolStripButton
    Friend WithEvents BtnMaximize As ToolStripButton
    Friend WithEvents BtnMinimize As ToolStripButton
    Friend WithEvents lblinformation As ToolStripButton
    Private WithEvents radAutoCompleteBox As RadAutoCompleteBox
    Friend WithEvents btnStart As ToolStripSplitButton
    Friend WithEvents ActualizarToolStripMenuItem As ToolStripMenuItem
    Dim State As Object


#Region " Windows Form Designer generated code "

    <STAThread()>
    Shared Sub Main()
        Try
            'Atrapa toda exception generada en este hilo no controlada
            AddHandler Application.ThreadException, AddressOf ZException.ThreadExceptionHandler

            Dim frmMainForm As New MainForm
            Application.Run(frmMainForm)

        Catch ex As TargetInvocationException
        Catch ex As ObjectDisposedException
        Catch ex As NullReferenceException
            MessageBox.Show(ex.ToString, "Error - Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As UnauthorizedAccessException
            MessageBox.Show(ex.ToString, "Error - Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As OutOfMemoryException
            MessageBox.Show(ex.ToString, "Error - Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As StackOverflowException
            MessageBox.Show(ex.ToString, "Error - Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As AppDomainUnloadedException
            MessageBox.Show(ex.ToString, "Error - Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As ApplicationException
            MessageBox.Show(ex.ToString, "Error - Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As ArgumentException
            MessageBox.Show(ex.ToString, "Error - Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As ArithmeticException
            MessageBox.Show(ex.ToString, "Error - Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As BadImageFormatException
            MessageBox.Show(ex.ToString, "Error - Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As InvalidOperationException
            MessageBox.Show(ex.ToString, "Error - Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As InvalidProgramException
            MessageBox.Show(ex.ToString, "Error - Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As System.ComponentModel.Win32Exception
            MessageBox.Show(ex.ToString, "Error - Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As SystemException
            MessageBox.Show(ex.ToString, "Error - Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error - Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared NetworkConnection As NetworkConnection



    Public Sub New()
        MyBase.New()
        Dim go As Boolean = True
        Dim count As Int64 = 0

        'While go
        '    count = count + 1
        '    Threading.Thread.Sleep(count * 1000)
        'End While

        Dim status As String

        If DBBusiness.InitializeSystem(ObjectTypes.Cliente, New RulesInstance().GetWFActivityRegularAssembly(), False, status, New ErrorReportBusiness) = False Then
        End If

        Try
            If (Boolean.Parse(UserPreferences.getValueForMachine("UseMasterCredentials", UPSections.UserPreferences, False).ToString())) Then

                Dim MasterCredentials As New NetworkCredential()
                MasterCredentials.Domain = ZOptBusiness.GetValueOrDefault("Domain", "pseguros.com")
                MasterCredentials.UserName = ZOptBusiness.GetValueOrDefault("DomainUserName", "stardocservice")
                MasterCredentials.Password = ZOptBusiness.GetValueOrDefault("DomainPasswordEncrypted", "sodio2017_nzt")
                Dim ServerShare As String = ZOptBusiness.GetValueOrDefault("DomainServerShare", "\\svrimage\zamba")
                NetworkConnection = New NetworkConnection(ServerShare, MasterCredentials)
            End If
        Catch ex As Win32Exception
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        If Not cerrando Then
            Try
                CheckInitialArguments()
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error: " & ex.ToString)
            End Try
            If Not cerrando Then
                InitializeComponent()
                lblinformation.Text &= status
                Me.MaximizeBox = True
                Me.MinimizeBox = False
                Me.ControlBox = False

            End If
            ThemeResolutionService.ApplicationThemeName = "TelerikMetroBlue"
        Else
            Try
                Close()
            Catch ex As Exception
            Finally
                Application.Exit()
            End Try
        End If


    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Disposing del Main Form at: | " & Date.Now.ToString)
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        Catch ex As Exception
        End Try

    End Sub




    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MenuItem68 As System.Windows.Forms.MenuItem
    Friend WithEvents NotifyIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents old_MenuItem36 As System.Windows.Forms.MenuItem
    Friend WithEvents old_MenuItem37 As System.Windows.Forms.MenuItem
    Friend WithEvents old_MenuItem75 As System.Windows.Forms.MenuItem
    Friend WithEvents old_MenuItem76 As System.Windows.Forms.MenuItem
    Friend WithEvents old_MenuItem77 As System.Windows.Forms.MenuItem
    Friend WithEvents old_MenuItem42 As System.Windows.Forms.MenuItem
    Friend WithEvents old_MenuItem43 As System.Windows.Forms.MenuItem
    Friend WithEvents old_ContextMenu2 As ContextMenu
    Friend WithEvents old_MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents old_MenuItem16 As System.Windows.Forms.MenuItem

    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.old_MenuItem36 = New System.Windows.Forms.MenuItem()
        Me.old_MenuItem37 = New System.Windows.Forms.MenuItem()
        Me.MenuItem68 = New System.Windows.Forms.MenuItem()
        Me.old_MenuItem75 = New System.Windows.Forms.MenuItem()
        Me.old_MenuItem76 = New System.Windows.Forms.MenuItem()
        Me.old_MenuItem77 = New System.Windows.Forms.MenuItem()
        Me.old_MenuItem42 = New System.Windows.Forms.MenuItem()
        Me.old_MenuItem43 = New System.Windows.Forms.MenuItem()
        Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.old_ContextMenu2 = New System.Windows.Forms.ContextMenu()
        Me.old_MenuItem11 = New System.Windows.Forms.MenuItem()
        Me.old_MenuItem16 = New System.Windows.Forms.MenuItem()
        Me.DocumentoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfigurarPáginaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfigurarImpresoraToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PowerpointToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VersionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsuarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.USUARIOToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.HistorialToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CambioDeClaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.PreferenciasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HerramientasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpcionesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EstaciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.GenerarBarcodeEnWordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.PlantillasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cleanCacheMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DepuradorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.RegistrarZambaEnWindowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RegistrarBrowserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AyudaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManualDeUsuarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReleaseNotesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuReportError = New System.Windows.Forms.ToolStripMenuItem()
        Me.SobreZambaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolbarZamba = New Zamba.AppBlock.ZToolBar()
        Me.btnStart = New System.Windows.Forms.ToolStripSplitButton()
        Me.ActualizarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BtnSearch = New System.Windows.Forms.ToolStripButton()
        Me.BtnInsert = New System.Windows.Forms.ToolStripButton()
        Me.BtnCaratulas = New System.Windows.Forms.ToolStripButton()
        Me.btnInformes = New System.Windows.Forms.ToolStripButton()
        Me.BtnTareas = New System.Windows.Forms.ToolStripButton()
        Me.BtnActiveTaks = New System.Windows.Forms.ToolStripButton()
        Me.BtnCerrar = New System.Windows.Forms.ToolStripButton()
        Me.BtnOptions = New System.Windows.Forms.ToolStripDropDownButton()
        Me.lblpreloadstatus = New System.Windows.Forms.ToolStripButton()
        Me.btnclosezamba = New System.Windows.Forms.ToolStripButton()
        Me.BtnMaximize = New System.Windows.Forms.ToolStripButton()
        Me.BtnMinimize = New System.Windows.Forms.ToolStripButton()
        Me.lblinformation = New System.Windows.Forms.ToolStripButton()
        Me.radAutoCompleteBox = New Telerik.WinControls.UI.RadAutoCompleteBox()
        Me.BtnResults = New System.Windows.Forms.ToolStripButton()
        Me.ToolbarZamba.SuspendLayout()
        CType(Me.radAutoCompleteBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'old_MenuItem36
        '
        Me.old_MenuItem36.Enabled = False
        Me.old_MenuItem36.Index = -1
        Me.old_MenuItem36.Text = "Cliente"
        '
        'old_MenuItem37
        '
        Me.old_MenuItem37.Enabled = False
        Me.old_MenuItem37.Index = -1
        Me.old_MenuItem37.Text = "Visualizador"
        '
        'MenuItem68
        '
        Me.MenuItem68.Index = -1
        Me.MenuItem68.Text = "-"
        '
        'old_MenuItem75
        '
        Me.old_MenuItem75.Index = -1
        Me.old_MenuItem75.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.old_MenuItem76, Me.old_MenuItem77})
        Me.old_MenuItem75.Text = "Informe de PDFS"
        '
        'old_MenuItem76
        '
        Me.old_MenuItem76.Index = 0
        Me.old_MenuItem76.Text = "Insertados"
        '
        'old_MenuItem77
        '
        Me.old_MenuItem77.Index = 1
        Me.old_MenuItem77.Text = "No Insertados"
        '
        'old_MenuItem42
        '
        Me.old_MenuItem42.Index = -1
        Me.old_MenuItem42.Text = "Vertical"
        '
        'old_MenuItem43
        '
        Me.old_MenuItem43.Index = -1
        Me.old_MenuItem43.Text = "Horizontal"
        '
        'NotifyIcon
        '
        Me.NotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon.ContextMenu = Me.old_ContextMenu2
        Me.NotifyIcon.Icon = CType(resources.GetObject("NotifyIcon.Icon"), System.Drawing.Icon)
        Me.NotifyIcon.Text = "Zamba"
        '
        'old_ContextMenu2
        '
        Me.old_ContextMenu2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.old_MenuItem11, Me.old_MenuItem16})
        '
        'old_MenuItem11
        '
        Me.old_MenuItem11.Index = 0
        Me.old_MenuItem11.Text = "Maximizar Zamba"
        '
        'old_MenuItem16
        '
        Me.old_MenuItem16.Index = 1
        Me.old_MenuItem16.Text = "Salir de Zamba"
        '
        'DocumentoToolStripMenuItem
        '
        Me.DocumentoToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.DocumentoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfigurarPáginaToolStripMenuItem, Me.ConfigurarImpresoraToolStripMenuItem})
        Me.DocumentoToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.DocumentoToolStripMenuItem.Name = "DocumentoToolStripMenuItem"
        Me.DocumentoToolStripMenuItem.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.DocumentoToolStripMenuItem.Size = New System.Drawing.Size(166, 20)
        Me.DocumentoToolStripMenuItem.Text = "Impresora"
        '
        'ConfigurarPáginaToolStripMenuItem
        '
        Me.ConfigurarPáginaToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.ConfigurarPáginaToolStripMenuItem.Image = Global.Zamba.Client.My.Resources.Resources.configurarpagina
        Me.ConfigurarPáginaToolStripMenuItem.Name = "ConfigurarPáginaToolStripMenuItem"
        Me.ConfigurarPáginaToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.ConfigurarPáginaToolStripMenuItem.Text = "Configurar Página"
        '
        'ConfigurarImpresoraToolStripMenuItem
        '
        Me.ConfigurarImpresoraToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.ConfigurarImpresoraToolStripMenuItem.Image = Global.Zamba.Client.My.Resources.Resources.configurarimpresora
        Me.ConfigurarImpresoraToolStripMenuItem.Name = "ConfigurarImpresoraToolStripMenuItem"
        Me.ConfigurarImpresoraToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.ConfigurarImpresoraToolStripMenuItem.Text = "Configurar Impresora"
        '
        'WordToolStripMenuItem
        '
        Me.WordToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.WordToolStripMenuItem.Name = "WordToolStripMenuItem"
        Me.WordToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.WordToolStripMenuItem.Text = "Word"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'PowerpointToolStripMenuItem
        '
        Me.PowerpointToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.PowerpointToolStripMenuItem.Name = "PowerpointToolStripMenuItem"
        Me.PowerpointToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.PowerpointToolStripMenuItem.Text = "Powerpoint"
        '
        'VersionToolStripMenuItem
        '
        Me.VersionToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.VersionToolStripMenuItem.Name = "VersionToolStripMenuItem"
        Me.VersionToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.VersionToolStripMenuItem.Text = "Version"
        '
        'UsuarioToolStripMenuItem
        '
        Me.UsuarioToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.UsuarioToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.USUARIOToolStripMenuItem1, Me.ToolStripSeparator2, Me.HistorialToolStripMenuItem, Me.CambioDeClaveToolStripMenuItem, Me.ToolStripSeparator3, Me.PreferenciasToolStripMenuItem})
        Me.UsuarioToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.UsuarioToolStripMenuItem.Name = "UsuarioToolStripMenuItem"
        Me.UsuarioToolStripMenuItem.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.UsuarioToolStripMenuItem.Size = New System.Drawing.Size(166, 20)
        Me.UsuarioToolStripMenuItem.Text = "Usuario"
        '
        'USUARIOToolStripMenuItem1
        '
        Me.USUARIOToolStripMenuItem1.BackColor = System.Drawing.Color.White
        Me.USUARIOToolStripMenuItem1.Name = "USUARIOToolStripMenuItem1"
        Me.USUARIOToolStripMenuItem1.Size = New System.Drawing.Size(185, 22)
        Me.USUARIOToolStripMenuItem1.Text = "USUARIO"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.BackColor = System.Drawing.Color.White
        Me.ToolStripSeparator2.ForeColor = System.Drawing.Color.White
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(182, 6)
        '
        'HistorialToolStripMenuItem
        '
        Me.HistorialToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.HistorialToolStripMenuItem.Image = Global.Zamba.Client.My.Resources.Resources.historial
        Me.HistorialToolStripMenuItem.Name = "HistorialToolStripMenuItem"
        Me.HistorialToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.HistorialToolStripMenuItem.Text = "Historial"
        '
        'CambioDeClaveToolStripMenuItem
        '
        Me.CambioDeClaveToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.CambioDeClaveToolStripMenuItem.Image = Global.Zamba.Client.My.Resources.Resources.cambiodeclave
        Me.CambioDeClaveToolStripMenuItem.Name = "CambioDeClaveToolStripMenuItem"
        Me.CambioDeClaveToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.CambioDeClaveToolStripMenuItem.Text = "Cambio de Clave"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.BackColor = System.Drawing.Color.White
        Me.ToolStripSeparator3.ForeColor = System.Drawing.Color.White
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(182, 6)
        '
        'PreferenciasToolStripMenuItem
        '
        Me.PreferenciasToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.PreferenciasToolStripMenuItem.Image = Global.Zamba.Client.My.Resources.Resources.preferencias
        Me.PreferenciasToolStripMenuItem.Name = "PreferenciasToolStripMenuItem"
        Me.PreferenciasToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.PreferenciasToolStripMenuItem.Text = "Preferencias"
        '
        'HerramientasToolStripMenuItem
        '
        Me.HerramientasToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.HerramientasToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpcionesToolStripMenuItem, Me.ToolStripSeparator4, Me.GenerarBarcodeEnWordToolStripMenuItem, Me.ToolStripSeparator5, Me.PlantillasToolStripMenuItem, Me.cleanCacheMenuItem, Me.DepuradorToolStripMenuItem, Me.ToolStripSeparator11, Me.RegistrarZambaEnWindowsToolStripMenuItem, Me.RegistrarBrowserToolStripMenuItem})
        Me.HerramientasToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.HerramientasToolStripMenuItem.Name = "HerramientasToolStripMenuItem"
        Me.HerramientasToolStripMenuItem.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.HerramientasToolStripMenuItem.Size = New System.Drawing.Size(166, 20)
        Me.HerramientasToolStripMenuItem.Text = "Herramientas"
        '
        'OpcionesToolStripMenuItem
        '
        Me.OpcionesToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.OpcionesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EstaciónToolStripMenuItem})
        Me.OpcionesToolStripMenuItem.Name = "OpcionesToolStripMenuItem"
        Me.OpcionesToolStripMenuItem.Size = New System.Drawing.Size(267, 22)
        Me.OpcionesToolStripMenuItem.Text = "Opciones"
        '
        'EstaciónToolStripMenuItem
        '
        Me.EstaciónToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.EstaciónToolStripMenuItem.Name = "EstaciónToolStripMenuItem"
        Me.EstaciónToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.EstaciónToolStripMenuItem.Text = "Estación"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.BackColor = System.Drawing.Color.White
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(264, 6)
        '
        'GenerarBarcodeEnWordToolStripMenuItem
        '
        Me.GenerarBarcodeEnWordToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.GenerarBarcodeEnWordToolStripMenuItem.Name = "GenerarBarcodeEnWordToolStripMenuItem"
        Me.GenerarBarcodeEnWordToolStripMenuItem.Size = New System.Drawing.Size(267, 22)
        Me.GenerarBarcodeEnWordToolStripMenuItem.Text = "Generar Barcode en Word"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.BackColor = System.Drawing.Color.White
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(264, 6)
        '
        'PlantillasToolStripMenuItem
        '
        Me.PlantillasToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.PlantillasToolStripMenuItem.Name = "PlantillasToolStripMenuItem"
        Me.PlantillasToolStripMenuItem.Size = New System.Drawing.Size(267, 22)
        Me.PlantillasToolStripMenuItem.Text = "Plantillas"
        '
        'cleanCacheMenuItem
        '
        Me.cleanCacheMenuItem.BackColor = System.Drawing.Color.White
        Me.cleanCacheMenuItem.Name = "cleanCacheMenuItem"
        Me.cleanCacheMenuItem.Size = New System.Drawing.Size(267, 22)
        Me.cleanCacheMenuItem.Text = "Limpiar Cache"
        '
        'DepuradorToolStripMenuItem
        '
        Me.DepuradorToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.DepuradorToolStripMenuItem.Name = "DepuradorToolStripMenuItem"
        Me.DepuradorToolStripMenuItem.Size = New System.Drawing.Size(267, 22)
        Me.DepuradorToolStripMenuItem.Text = "Depurador"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.BackColor = System.Drawing.Color.White
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(264, 6)
        '
        'RegistrarZambaEnWindowsToolStripMenuItem
        '
        Me.RegistrarZambaEnWindowsToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.RegistrarZambaEnWindowsToolStripMenuItem.Name = "RegistrarZambaEnWindowsToolStripMenuItem"
        Me.RegistrarZambaEnWindowsToolStripMenuItem.Size = New System.Drawing.Size(267, 22)
        Me.RegistrarZambaEnWindowsToolStripMenuItem.Text = "Registrar Zamba en Windows"
        '
        'RegistrarBrowserToolStripMenuItem
        '
        Me.RegistrarBrowserToolStripMenuItem.Name = "RegistrarBrowserToolStripMenuItem"
        Me.RegistrarBrowserToolStripMenuItem.Size = New System.Drawing.Size(267, 22)
        Me.RegistrarBrowserToolStripMenuItem.Text = "Registrar Browser"
        '
        'AyudaToolStripMenuItem
        '
        Me.AyudaToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.AyudaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ManualDeUsuarioToolStripMenuItem, Me.ReleaseNotesToolStripMenuItem, Me.ToolStripSeparator6, Me.mnuReportError, Me.SobreZambaToolStripMenuItem})
        Me.AyudaToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.AyudaToolStripMenuItem.Name = "AyudaToolStripMenuItem"
        Me.AyudaToolStripMenuItem.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.AyudaToolStripMenuItem.Size = New System.Drawing.Size(166, 20)
        Me.AyudaToolStripMenuItem.Text = "Ayuda"
        '
        'ManualDeUsuarioToolStripMenuItem
        '
        Me.ManualDeUsuarioToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.ManualDeUsuarioToolStripMenuItem.Image = Global.Zamba.Client.My.Resources.Resources.manual
        Me.ManualDeUsuarioToolStripMenuItem.Name = "ManualDeUsuarioToolStripMenuItem"
        Me.ManualDeUsuarioToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.ManualDeUsuarioToolStripMenuItem.Text = "Manual de Usuario"
        '
        'ReleaseNotesToolStripMenuItem
        '
        Me.ReleaseNotesToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.ReleaseNotesToolStripMenuItem.Image = Global.Zamba.Client.My.Resources.Resources.manual
        Me.ReleaseNotesToolStripMenuItem.Name = "ReleaseNotesToolStripMenuItem"
        Me.ReleaseNotesToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.ReleaseNotesToolStripMenuItem.Text = "Release Notes"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.BackColor = System.Drawing.Color.White
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(193, 6)
        '
        'mnuReportError
        '
        Me.mnuReportError.BackColor = System.Drawing.Color.White
        Me.mnuReportError.Image = Global.Zamba.Client.My.Resources.Resources.bug
        Me.mnuReportError.Name = "mnuReportError"
        Me.mnuReportError.Size = New System.Drawing.Size(196, 22)
        Me.mnuReportError.Text = "Reportar Error"
        '
        'SobreZambaToolStripMenuItem
        '
        Me.SobreZambaToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.SobreZambaToolStripMenuItem.Image = Global.Zamba.Client.My.Resources.Resources.sobrezamba
        Me.SobreZambaToolStripMenuItem.Name = "SobreZambaToolStripMenuItem"
        Me.SobreZambaToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.SobreZambaToolStripMenuItem.Text = "Sobre Zamba"
        '
        'ToolbarZamba
        '
        Me.ToolbarZamba.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ToolbarZamba.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolbarZamba.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolbarZamba.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.ToolbarZamba.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnStart, Me.BtnSearch, Me.BtnInsert, Me.BtnCaratulas, Me.btnInformes, Me.BtnTareas, Me.BtnActiveTaks, Me.BtnCerrar, Me.BtnOptions, Me.lblpreloadstatus, Me.btnclosezamba, Me.BtnMaximize, Me.BtnMinimize, Me.lblinformation})
        Me.ToolbarZamba.Location = New System.Drawing.Point(5, 5)
        Me.ToolbarZamba.Name = "ToolbarZamba"
        Me.ToolbarZamba.Size = New System.Drawing.Size(1360, 38)
        Me.ToolbarZamba.Stretch = True
        Me.ToolbarZamba.TabIndex = 1
        '
        'btnStart
        '
        Me.btnStart.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ActualizarToolStripMenuItem})
        Me.btnStart.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.btnStart.ForeColor = System.Drawing.Color.White
        Me.btnStart.Image = Global.Zamba.Client.My.Resources.Resources.appbar_home1
        Me.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(95, 35)
        Me.btnStart.Tag = "INICIO"
        Me.btnStart.Text = "INICIO"
        '
        'ActualizarToolStripMenuItem
        '
        Me.ActualizarToolStripMenuItem.Name = "ActualizarToolStripMenuItem"
        Me.ActualizarToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.ActualizarToolStripMenuItem.Text = "Actualizar"
        '
        'BtnSearch
        '
        Me.BtnSearch.CheckOnClick = True
        Me.BtnSearch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSearch.ForeColor = System.Drawing.Color.White
        Me.BtnSearch.Image = Global.Zamba.Client.My.Resources.Resources.appbar_magnify
        Me.BtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnSearch.Margin = New System.Windows.Forms.Padding(3)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(92, 32)
        Me.BtnSearch.Tag = "BUSCAR"
        Me.BtnSearch.Text = "BUSCAR"
        Me.BtnSearch.ToolTipText = "BUSCAR DOCUMENTOS"
        '
        'BtnInsert
        '
        Me.BtnInsert.CheckOnClick = True
        Me.BtnInsert.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.BtnInsert.ForeColor = System.Drawing.Color.White
        Me.BtnInsert.Image = Global.Zamba.Client.My.Resources.Resources.appbar1
        Me.BtnInsert.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnInsert.Margin = New System.Windows.Forms.Padding(3)
        Me.BtnInsert.Name = "BtnInsert"
        Me.BtnInsert.Size = New System.Drawing.Size(104, 32)
        Me.BtnInsert.Tag = "INSERTAR"
        Me.BtnInsert.Text = "INSERTAR"
        Me.BtnInsert.ToolTipText = "INSERTAR DOCUMENTOS"
        '
        'BtnCaratulas
        '
        Me.BtnCaratulas.CheckOnClick = True
        Me.BtnCaratulas.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.BtnCaratulas.ForeColor = System.Drawing.Color.White
        Me.BtnCaratulas.Image = Global.Zamba.Client.My.Resources.Resources.appbar2
        Me.BtnCaratulas.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnCaratulas.Margin = New System.Windows.Forms.Padding(3)
        Me.BtnCaratulas.Name = "BtnCaratulas"
        Me.BtnCaratulas.Size = New System.Drawing.Size(108, 32)
        Me.BtnCaratulas.Tag = "CARATULA"
        Me.BtnCaratulas.Text = "CARATULA"
        Me.BtnCaratulas.ToolTipText = "GENERAR CARATULAS DE CODIGO DE BARRA"
        '
        'btnInformes
        '
        Me.btnInformes.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.btnInformes.ForeColor = System.Drawing.Color.White
        Me.btnInformes.Image = Global.Zamba.Client.My.Resources.Resources.appbar_graph_line1
        Me.btnInformes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnInformes.Margin = New System.Windows.Forms.Padding(3)
        Me.btnInformes.Name = "btnInformes"
        Me.btnInformes.Size = New System.Drawing.Size(108, 32)
        Me.btnInformes.Tag = "INFORMES"
        Me.btnInformes.Text = "INFORMES"
        Me.btnInformes.ToolTipText = "VER LISTADO INFORMES"
        '
        'BtnTareas
        '
        Me.BtnTareas.BackColor = System.Drawing.Color.Transparent
        Me.BtnTareas.CheckOnClick = True
        Me.BtnTareas.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.BtnTareas.ForeColor = System.Drawing.Color.White
        Me.BtnTareas.Image = Global.Zamba.Client.My.Resources.Resources.appbar_list1
        Me.BtnTareas.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnTareas.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnTareas.Name = "BtnTareas"
        Me.BtnTareas.Size = New System.Drawing.Size(91, 38)
        Me.BtnTareas.Tag = "TAREAS"
        Me.BtnTareas.Text = "TAREAS"
        Me.BtnTareas.ToolTipText = "VER TAREAS PENDIENTES"
        '
        'BtnActiveTaks
        '
        Me.BtnActiveTaks.BackColor = System.Drawing.Color.Transparent
        Me.BtnActiveTaks.CheckOnClick = True
        Me.BtnActiveTaks.Enabled = False
        Me.BtnActiveTaks.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.BtnActiveTaks.ForeColor = System.Drawing.Color.White
        Me.BtnActiveTaks.Image = Global.Zamba.Client.My.Resources.Resources.appbar_clipboard_paper1
        Me.BtnActiveTaks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnActiveTaks.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnActiveTaks.Name = "BtnActiveTaks"
        Me.BtnActiveTaks.Size = New System.Drawing.Size(155, 35)
        Me.BtnActiveTaks.Tag = "TAREAS ACTIVAS"
        Me.BtnActiveTaks.Text = "TAREAS ACTIVAS"
        Me.BtnActiveTaks.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnActiveTaks.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
        '
        'BtnCerrar
        '
        Me.BtnCerrar.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.BtnCerrar.ForeColor = System.Drawing.Color.White
        Me.BtnCerrar.Image = Global.Zamba.Client.My.Resources.Resources.appbar3
        Me.BtnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnCerrar.Margin = New System.Windows.Forms.Padding(3)
        Me.BtnCerrar.Name = "BtnCerrar"
        Me.BtnCerrar.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        Me.BtnCerrar.Size = New System.Drawing.Size(90, 32)
        Me.BtnCerrar.Tag = "CERRAR"
        Me.BtnCerrar.Text = "CERRAR"
        Me.BtnCerrar.ToolTipText = "CERRAR VENTANA"
        Me.BtnCerrar.Visible = False
        '
        'BtnOptions
        '
        Me.BtnOptions.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DocumentoToolStripMenuItem, Me.UsuarioToolStripMenuItem, Me.HerramientasToolStripMenuItem, Me.AyudaToolStripMenuItem})
        Me.BtnOptions.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.BtnOptions.ForeColor = System.Drawing.Color.White
        Me.BtnOptions.Image = Global.Zamba.Client.My.Resources.Resources.appbar4
        Me.BtnOptions.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnOptions.Name = "BtnOptions"
        Me.BtnOptions.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        Me.BtnOptions.Size = New System.Drawing.Size(41, 35)
        '
        'lblpreloadstatus
        '
        Me.lblpreloadstatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.lblpreloadstatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.lblpreloadstatus.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.lblpreloadstatus.Name = "lblpreloadstatus"
        Me.lblpreloadstatus.Size = New System.Drawing.Size(23, 35)
        '
        'btnclosezamba
        '
        Me.btnclosezamba.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnclosezamba.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnclosezamba.ForeColor = System.Drawing.Color.White
        Me.btnclosezamba.Image = Global.Zamba.Client.My.Resources.Resources.appbar5
        Me.btnclosezamba.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnclosezamba.Name = "btnclosezamba"
        Me.btnclosezamba.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        Me.btnclosezamba.Size = New System.Drawing.Size(32, 35)
        Me.btnclosezamba.Text = "Cerrar"
        '
        'BtnMaximize
        '
        Me.BtnMaximize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.BtnMaximize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BtnMaximize.Image = Global.Zamba.Client.My.Resources.Resources.appbar_window_restore
        Me.BtnMaximize.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnMaximize.Name = "BtnMaximize"
        Me.BtnMaximize.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        Me.BtnMaximize.Size = New System.Drawing.Size(32, 35)
        Me.BtnMaximize.Text = "Maximizar"
        '
        'BtnMinimize
        '
        Me.BtnMinimize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.BtnMinimize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BtnMinimize.Image = Global.Zamba.Client.My.Resources.Resources.appbar_minus
        Me.BtnMinimize.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnMinimize.Name = "BtnMinimize"
        Me.BtnMinimize.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        Me.BtnMinimize.Size = New System.Drawing.Size(32, 35)
        Me.BtnMinimize.Text = "Minimizar"
        '
        'lblinformation
        '
        Me.lblinformation.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.lblinformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblinformation.ForeColor = System.Drawing.Color.White
        Me.lblinformation.Image = CType(resources.GetObject("lblinformation.Image"), System.Drawing.Image)
        Me.lblinformation.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.lblinformation.Name = "lblinformation"
        Me.lblinformation.Size = New System.Drawing.Size(23, 35)
        '
        'radAutoCompleteBox
        '
        Me.radAutoCompleteBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.radAutoCompleteBox.Location = New System.Drawing.Point(0, 0)
        Me.radAutoCompleteBox.Name = "radAutoCompleteBox"
        '
        '
        '
        Me.radAutoCompleteBox.RootElement.ControlBounds = New System.Drawing.Rectangle(0, 0, 164, 26)
        Me.radAutoCompleteBox.Size = New System.Drawing.Size(200, 38)
        Me.radAutoCompleteBox.TabIndex = 0
        '
        'BtnResults
        '
        Me.BtnResults.Font = New System.Drawing.Font("Verdana", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnResults.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.BtnResults.Image = Global.Zamba.Client.My.Resources.Resources.appbar_column_one
        Me.BtnResults.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnResults.Margin = New System.Windows.Forms.Padding(3)
        Me.BtnResults.Name = "BtnResults"
        Me.BtnResults.Size = New System.Drawing.Size(103, 26)
        Me.BtnResults.Tag = "RESULTADOS"
        Me.BtnResults.Text = "RESULTADOS"
        Me.BtnResults.ToolTipText = "Ver Resultados"
        '
        'MainForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 16)
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.Zamba.Client.My.Resources.Resources.zamba_hor_ext_color
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(1370, 425)
        Me.Controls.Add(Me.ToolbarZamba)
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.DoubleBuffered = True
        Me.ForeColor = System.Drawing.Color.Black
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "MainForm"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ""
        Me.TransparencyKey = System.Drawing.Color.Magenta
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolbarZamba.ResumeLayout(False)
        Me.ToolbarZamba.PerformLayout()
        CType(Me.radAutoCompleteBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

#Region "Initial Arguments"

    Public Shared line As String
    Private Argument As String
    Private initialArgumentsChecked As Boolean = False


    Private Sub CheckInitialArguments()
        'Verifica que el método sea ejecutado una única vez al inicio de sesión de Zamba
        If Not initialArgumentsChecked Then
            initialArgumentsChecked = True

            Dim frmWait As WaitForm = Nothing
            Try
                Dim version As String = UpdaterBusiness.GetVersion(System.Windows.Forms.Application.StartupPath & "\Zamba.Cliente.exe").Replace(".", String.Empty)
                If UpdaterBusiness.IsNewHost(version) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "# Nueva instalación detectada.")

                    UpdaterBusiness.SetEstreg(version)

                ElseIf UpdaterBusiness.IsLastestVersion() Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "# Zamba se encuentra actualizado.")
                Else
                    If Not String.IsNullOrEmpty(line) AndAlso
                        (String.Compare(line.ToLower, "correctupdate") = 0 OrElse
                        String.Compare(line.ToLower, "wrongupdate") = 0) Then

                        If String.Compare(line.ToLower, "correctupdate") = 0 Then
                            FinishInstallationProcess()
                            Exit Sub
                        Else
                            If MessageBox.Show("Ha ocurrido un error en la actualización de Zamba, ¿deséa intentar nuevamente?" & vbCrLf &
                                               "Si presiona ACEPTAR el proceso de actualización comenzará." & vbCrLf &
                                               "Si presiona CANCELAR podrá continuar trabajando con la versión desactualizada.",
                                   "ZAMBA SOFTWARE", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation,
                                   MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, False) _
                               = DialogResult.Cancel Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "# Ha ocurrido un error al actualizar Zamba. El usuario seleccionó la opción para continuar trabajando con la versión anterior.")
                                Exit Sub
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "# Ha ocurrido un error al actualizar Zamba. El usuario seleccionó la opción para volver a intentarlo.")
                            End If
                        End If
                    Else
                        If MessageBox.Show("Existe una nueva version de Zamba, desea actualizar ahora?",
                                           "Actualizacion de Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                           MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, False) _
                                       = DialogResult.No Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "# Zamba requiere de una actualización pero el usuario seleccionó la opción para continuar trabajando con la versión anterior.")
                            Exit Sub
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "# Zamba comenzará con el proceso de actualización.")
                        End If
                    End If

                    frmWait = New WaitForm
                    frmWait.Show()
                    Application.DoEvents()

                    FlagDoClose = True
                    cerrando = True
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Flag cerrando y FlagDoClose en True")

                    Dim newFrmInicial As New Inicial2()
                    newFrmInicial.StartActions()
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
            Finally
                If frmWait IsNot Nothing Then
                    frmWait.Close()
                    frmWait.Dispose()
                End If
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Termina de hacer las últimas actualizaciones que Zamba necesita para funcionar correctamente
    ''' </summary>
    ''' <remarks>Se ejecuta por unica vez al finalizar una actualización correcta de Zamba</remarks>
    Private Sub FinishInstallationProcess()
        'Actualiza la versión de zamba del puesto
        Dim serverVersion As String = UpdaterBusiness.GetLastestVersion()
        UpdateBusiness.ForzarActualizarPorPC(Environment.MachineName, serverVersion)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "# ZAMBA se ha actualizado correctamente a la versión " & serverVersion)
        MessageBox.Show("ZAMBA se ha actualizado correctamente a la versión " & serverVersion,
                        "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

#End Region

#Region "Private Fields"
    Public UserValidatedFlag As Boolean
    Private Login As Login
    Private frmError As FrmErrorReporting = Nothing
    Public flaglegal As Boolean
#End Region

#Region "Document"
    Private TotalDocuments As Int64
    Private Sub TotalCounts(ByVal Count As Int64, ByVal DocTypeCounts As Int32)
        Try
            SyncLock (Me)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Recibo el Conteo de  Documentos, ModDoucments.SumTotalCount linea 664 |" & Now.ToString)
                TotalDocuments += Count
            End SyncLock
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStateException
        Catch ex As Exception
            'Catch
        End Try
    End Sub
#End Region

#Region "Mail"
    'Private Sub CheckMessages()
    '    Try
    '        Dim messages As Integer = MessagesBusiness.CheckMessages
    '        If messages > 0 Then
    '            If messages = 1 Then
    '                Me.ShowInfo("TIENE 1 MENSAJE NUEVO EN SU BANDEJA DE MENSAJES", "", Enums.TMsg.NO, Enums.Tinterfaz.Both)
    '            Else
    '                Me.ShowInfo("TIENE " & messages & " MENSAJES NUEVOS EN SU BANDEJA DE MENSAJES", "", Enums.TMsg.NO, Enums.Tinterfaz.Both)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try
    'End Sub

#End Region

#Region "Help"
    Private Sub ManualDelUsuario()
        Try
            ShowUserHelp()
        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ReleaseNotes()
        Try
            ShowUserHelpRelease()
        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ShowUserHelpRelease()
        Dim help As New frmHelp(frmHelp.HelpMode.ReleaseNotes)
        help.ShowDialog()
    End Sub
    Private Sub ShowUserHelp()
        Dim help As New frmHelp(frmHelp.HelpMode.UserHelp)
        help.ShowDialog()
    End Sub
#End Region

#Region "Station Property"
    Private Sub Estacion()
        Try
            If RightsBusiness.GetUserRights(ObjectTypes.FrmEstation, RightsType.View) = True Then
                Dim FrmEstation As New FrmEstation
                FrmEstation.ShowDialog()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Change Password"
    Private Sub CambioDeClave()
        Try
            If IsNothing(FrmView) = True Then LoadViewer()
            FrmView.CambioDeClave()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Remove Connection & Close & Dispose & Finalize"
    Private Sub closeall()
        Dim i As Integer
        Dim arrayForms As Array = MdiChildren()
        For i = 0 To MdiChildren.Length - 1
            Dim aForm As Form = DirectCast(arrayForms(i), Form)
            aForm.Close()
        Next
    End Sub

    Private Shared Sub OK()
        SendKeys.Send("Enter")
    End Sub

#Region "Salir"
    Dim FlagClose As Boolean

    ''' <summary>
    ''' Evento que se ejecuta antes de cerrar la aplicación cliente
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	20/02/2009	Modified    Los valores header y footer vuelven a sus valores originales
    '''     [Tomas]     23/02/2009  Modified    Se remueve la modificación del registro ya que pasa a ser controlada
    '''                                         por el método Print de la clase FormBrowser.
    '''     [Tomas]     12/03/09    Modified    A partir de nuevas especificaciones se comprueba la configuración
    '''                                         del usuario y los valores de header y footer toman sus valores por defecto.
    ''' </history>
    Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            'Este metodo solo se debe invocar cunado se presiona la X de cerrar un documento de office con hook en Zamba.
            If FrmView.CloseActiveTaskTab() Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If
        Catch ex As ThreadAbortException
        Catch ex As Exception
            e.Cancel = False
        End Try
    End Sub

#End Region
    Private Sub CleanExceptions()
        Try
            Users.Actions.CleanExceptions()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Menues"
    Private Sub ConfigurarPagina()
        Try
            If IsNothing(FrmView) = True Then LoadViewer()
            FrmView.ConfigurarPagina()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub NuevoWord()
        Try
            Insert("NuevoWord")
        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub NuevoExcel()
        Try
            Insert("NuevoExcel")
        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub NuevoPowerpoint()
        Try
            Insert("NuevoPowerpoint")
        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub PowerPoint_app()
        Try
            If RightsBusiness.GetUserRights(ObjectTypes.ModuleElectronicDoc, RightsType.Use) = False Then
                MessageBox.Show("Usted no tiene permiso para utilizar Documentos Electronicos", "Zamba Software - Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Exit Sub
            End If

            Dim Fi As New FileInfo(Application.StartupPath + "\new.ppt")
            Dim Fa As FileInfo

            If Fi.Exists Then
                Try
                    Try
                        Fa = New FileInfo(Application.StartupPath & "\new.ppt")
                        Fi.CopyTo(Fa.FullName, True)
                    Catch
                        Dim i As Int32
                        For i = 1 To 10
                            Try
                                Fa = New FileInfo(Application.StartupPath & "\new" & i & ".ppt")
                                Fi.CopyTo(Fa.FullName, True)
                                Exit For
                            Catch
                            End Try
                        Next
                    End Try
                    Insert(Fa.FullName)
                Finally
                    Try
                        Fa.Delete()
                    Catch
                    End Try
                End Try
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ConfigurarImpresora()
        Try
            If IsNothing(FrmView) = True Then LoadViewer()
            FrmView.ConfigurarImpresora()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub



#End Region

#Region "Inicializacion del MDI"

    Private Sub CloseApplication()
        Application.Exit()
    End Sub

    ''' <summary>
    ''' Método utilizado para validar el usuario y la contraseña. En caso de que sea válido, se lo deja entrar y se inicia el connectionTimer
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    13/05/2008  Modified
    '''     [Gaston]    04/06/2009  Modified    La propiedad UserType se coloca en True indicando que el type en UCM va a ser 0
    '''                                         (Sólo válido para licencia documental)(Nota: La propiedad se coloco adentro del constructor)
    '''     [Gaston]    16/06/2009  Modified    "True" para el constructor del login indicando que es un cliente el que desea ingresar a Zamba
    ''' </history>
    Private Sub ValidarUsuarioYContrasenya(ByVal ReLogin As Boolean)
        Try
            If Not IsNothing(NotifyIcon) Then
                NotifyIcon.Visible = False
            End If
        Catch
        End Try

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando la validación de usuario")

        If (Users.User.IsNothingUser) Then

            Login = New Login(True, ReLogin, False, ObjectTypes.Cliente,,,, New RulesInstance().GetWFActivityRegularAssembly)

            If IsNothing(Membership.MembershipHelper.CurrentUser) Then
                If Not (UserBusiness.IsWUPreferenceAndExistUser()) Then

                    Try
                        Visible = False
                        If Login.IsDisposed = False Then Login.ShowDialog()

                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                        MessageBox.Show("Ocurrio un Error en el Sistema, al mostrar el dialogo de Usuario " & ex.ToString, "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Validación de usuario finalizada con errores at: | " & Date.Now.ToString)
                        Application.Exit()
                    Finally
                        Visible = True
                    End Try
                    Try
                        Login.Focus()
                    Catch
                    End Try

                End If

                If (Login.DialogResult = DialogResult.OK) Then

                    'Valido que los valores del app.ini coincide con los guardados en la base
                    If (UserBusiness.ValidateDataBase()) Then
                        Dim _UserBusinessExt As New UserBusinessExt()
                        Try
                            If (_UserBusinessExt.ValidatePCName(Membership.MembershipHelper.CurrentUser)) Then
                                UserValidatedFlag = True
                                ToolbarZamba.Enabled = True


                                ' Cuando vuelve a aparecer el formulario de login tras haber expirado el time_out, el formulario principal del cliente se
                                ' deshabilita. Por lo tanto, si el cliente vuelve a loguearse, y es válido, el formulario principal debe volver a habilitarse
                                If (Enabled = False) Then
                                    Enabled = True
                                End If
                            Else
                                Try
                                    Ucm.RemoveConnection()
                                Catch ex As Exception
                                    ZClass.raiseerror(ex)
                                End Try
                                MessageBox.Show("El usuario ya tiene una sesión iniciada", "Atencion")
                                MembershipHelper.SetCurrentUser(Nothing)
                                ValidarUsuarioYContrasenya(ReLogin)
                            End If
                        Finally
                            _UserBusinessExt = Nothing
                        End Try
                    Else
                        Try
                            Ucm.RemoveConnection()
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try





                        Dim contacto As String = ZOptBusiness.GetValue("ContactoValidacion")
                        Dim contactoMail As String = ZOptBusiness.GetValue("MailContactoValidacion")

                        If String.IsNullOrEmpty(contacto) Then
                            MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con Stardoc Argentina S.A.", "Atencion")
                        ElseIf String.IsNullOrEmpty(contactoMail) Then
                            MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con " & contacto, "Atencion")
                        Else
                            MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con " & contacto & " al correo electronico " & contactoMail, "Atencion")
                        End If
                    End If
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Validación de usuario Finalizada at: | " & Date.Now.ToString)

            Else
                If (UserBusiness.ValidateDataBase()) Then
                    Dim _UserBusinessExt As New UserBusinessExt()
                    Try
                        If (_UserBusinessExt.ValidatePCName(Membership.MembershipHelper.CurrentUser)) Then
                            UserValidatedFlag = True
                            ToolbarZamba.Enabled = True


                            ' Cuando vuelve a aparecer el formulario de login tras haber expirado el time_out, el formulario principal del cliente se
                            ' deshabilita. Por lo tanto, si el cliente vuelve a loguearse, y es válido, el formulario principal debe volver a habilitarse
                            If (Enabled = False) Then
                                Enabled = True
                            End If
                        Else
                            Try
                                Ucm.RemoveConnection()
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try
                            MessageBox.Show("El usuario ya tiene una sesión iniciada", "Atencion")
                            MembershipHelper.SetCurrentUser(Nothing)
                            ValidarUsuarioYContrasenya(ReLogin)
                        End If
                    Finally
                        _UserBusinessExt = Nothing
                    End Try
                Else
                    Try
                        Ucm.RemoveConnection()
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try




                    Dim contacto As String = ZOptBusiness.GetValue("ContactoValidacion")
                    Dim contactoMail As String = ZOptBusiness.GetValue("MailContactoValidacion")

                    If String.IsNullOrEmpty(contacto) Then
                        MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con Stardoc Argentina S.A.", "Atencion")
                    ElseIf String.IsNullOrEmpty(contactoMail) Then
                        MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con " & contacto, "Atencion")
                    Else
                        MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con " & contacto & " al correo electronico " & contactoMail, "Atencion")
                    End If

                End If

            End If
        Else
            If (UserBusiness.ValidateDataBase()) Then
                Dim _UserBusinessExt As New UserBusinessExt()
                Try
                    If Membership.MembershipHelper.CurrentUser.ConnectionId = 0 Then
                        Try
                            Dim WinUser, WinPc As String
                            WinUser = Environment.UserName
                            WinPc = Environment.MachineName
                            Dim connectionId As Integer = Ucm.NewConnection(UserBusiness.Rights.CurrentUser.ID, WinUser, WinPc, Int16.Parse(UserPreferences.getValue("TimeOut", UPSections.UserPreferences, 30)), 0, False)
                            If (connectionId <= 0) Then
                                MessageBox.Show("Se alcanzo el maximo de licencias disponibles", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Application.Exit()
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                            MessageBox.Show("Ocurrio un error inesperado. Contáctese con su administrador o reinicie la aplicación", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            UserBusiness.Rights.CurrentUser.ConnectionId = 0
                            Application.Exit()
                        End Try
                        UserPreferences.setValueForMachine("UserId", UserBusiness.Rights.CurrentUser.ID.ToString(), UPSections.UserPreferences)
                    End If


                    If (_UserBusinessExt.ValidatePCName(Membership.MembershipHelper.CurrentUser)) Then
                        UserValidatedFlag = True
                        ToolbarZamba.Enabled = True


                        ' Cuando vuelve a aparecer el formulario de login tras haber expirado el time_out, el formulario principal del cliente se
                        ' deshabilita. Por lo tanto, si el cliente vuelve a loguearse, y es válido, el formulario principal debe volver a habilitarse
                        If (Enabled = False) Then
                            Enabled = True
                        End If
                    Else
                        Try
                            Ucm.RemoveConnection()
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                        MessageBox.Show("El usuario ya tiene una sesión iniciada", "Atencion")
                        MembershipHelper.SetCurrentUser(Nothing)
                        ValidarUsuarioYContrasenya(ReLogin)
                    End If
                Finally
                    _UserBusinessExt = Nothing
                End Try
            Else
                Try
                    Ucm.RemoveConnection()
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try




                Dim contacto As String = ZOptBusiness.GetValue("ContactoValidacion")
                Dim contactoMail As String = ZOptBusiness.GetValue("MailContactoValidacion")

                If String.IsNullOrEmpty(contacto) Then
                    MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con Stardoc Argentina S.A.", "Atencion")
                ElseIf String.IsNullOrEmpty(contactoMail) Then
                    MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con " & contacto, "Atencion")
                Else
                    MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con " & contacto & " al correo electronico " & contactoMail, "Atencion")
                End If
            End If

        End If

    End Sub

    Private Sub SetModulesRights()
        Try
            BtnCaratulas.Enabled = RightsBusiness.GetUserRights(ObjectTypes.ModuleBarCode, RightsType.Use)

            ConfigurarPáginaToolStripMenuItem.Enabled = RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.Print)
            ConfigurarImpresoraToolStripMenuItem.Enabled = RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.Print)
            PreferenciasToolStripMenuItem.Enabled = RightsBusiness.GetUserRights(ObjectTypes.PreferenciasDeUsuario, RightsType.Configurar)

            EstaciónToolStripMenuItem.Enabled = RightsBusiness.GetUserRights(ObjectTypes.FrmEstation, RightsType.View)
            HistorialToolStripMenuItem.Enabled = RightsBusiness.GetUserRights(ObjectTypes.HistorialDeUsuario, RightsType.View)

            'TODO: Permiso a preferencias...
            If Not RightsBusiness.GetUserRights(ObjectTypes.PreferenciasDeUsuario, RightsType.Configurar) Then
                PreferenciasToolStripMenuItem.Visible = False
            End If

            'TODO: Permiso ver WorkFlow(Tareas)...
            If Not RightsBusiness.GetUserRights(ObjectTypes.ModuleWorkFlow, RightsType.Use) Then
                BtnActiveTaks.Enabled = False
            End If

            If Not RightsBusiness.GetUserRights(ObjectTypes.ModuleReports, RightsType.Use) AndAlso Not RightsBusiness.GetUserRights(ObjectTypes.ModuleReports, RightsType.Use) Then
                btnInformes.Enabled = False
            Else
                btnInformes.Enabled = True
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub AddHandlers()
        RemoveHandler ModDocuments.ShowResults, AddressOf InvokeShowResults
        RemoveHandler ModDocuments.TotalCounts, AddressOf TotalCounts
        RemoveHandler ModDocuments.DeboMaximizar, AddressOf DeboMaximizar
        AddHandler ModDocuments.ShowResults, AddressOf InvokeShowResults
        AddHandler ModDocuments.TotalCounts, AddressOf TotalCounts
        AddHandler ModDocuments.DeboMaximizar, AddressOf DeboMaximizar

        Try
            RemoveHandler ProxyServer.ValidateLogin, AddressOf ValidateLogin
            RemoveHandler ProxyServer.ShowTask, AddressOf ShowTask
            RemoveHandler ProxyServer.ShowResult, AddressOf ShowResult
            AddHandler ProxyServer.ValidateLogin, AddressOf ValidateLogin
            AddHandler ProxyServer.ShowTask, AddressOf ShowTask
            AddHandler ProxyServer.ShowResult, AddressOf ShowResult
            'AddHandler ProxyServer.DoSearch, AddressOf DoSearch
        Catch ex As Exception
        End Try
        Try
            RemoveHandler ProxyServer.InsertFile, AddressOf Insert
            AddHandler ProxyServer.InsertFile, AddressOf Insert
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ShowResult(Result As IResult)
        Try
            If Result Is Nothing Then
                MessageBox.Show("No se puede tener acceso al documento solicitado." & vbCrLf &
                       "Es posible que no tenga permisos de visualización o que el mismo haya sido eliminado.",
                       "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                If FrmView Is Nothing Then
                    If Me IsNot Nothing AndAlso Not Me.IsDisposed AndAlso FrmView Is Nothing Then
                        LoadViewer()
                    End If

                    If FrmView Is Nothing Then
                        Throw New Exception("Error al cargar el control necesario para visualizar la tarea.")
                    End If
                End If

                FrmView.ShowProperResult(Result)
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub DeboMaximizar()
        NotifyIcon.Visible = False
        MaximizeForm()
    End Sub

    Private Sub ValidateLogin()
        MaximizeForm()
        NotifyIcon.Visible = False
    End Sub

    Private Sub ShowTask(ByVal taskId As Int64, ByVal stepId As Int64, ByVal docTypeId As Int64)
        Try
            If taskId = 0 AndAlso stepId = 0 AndAlso docTypeId = 0 Then
                MessageBox.Show("No se puede tener acceso al documento solicitado." & vbCrLf &
                       "Es posible que no tenga permisos de visualización o que el mismo haya sido eliminado.",
                       "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                If FrmView Is Nothing Then
                    LoadViewer()
                    If FrmView Is Nothing Then
                        Throw New Exception("Error al cargar el control necesario para visualizar la tarea.")
                    End If
                End If

                FrmView.ShowTask(taskId, stepId, docTypeId)
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método que remueve y vuelve a accionar los eventos, así cuando estos se ejecuten llamen al método correspondiente ubicado en AddressOf
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	16/05/2008	Modified
    '''     [Gaston]    04/06/2009  Modified    Agregado del closeUserSession
    '''     [Javier]    23/07/2010  Modified    Manejador del refreshtimeout para WFRulesBusiness
    ''' </history>
    Private Sub AddExceptionsHandlers()
        RemoveHandler Zamba.AppBlock.ZForm.RefreshTimeOut, AddressOf UpdateTimeOut
        RemoveHandler Zamba.AppBlock.ZControl.RefreshTimeOut, AddressOf UpdateTimeOut
        RemoveHandler UserBusiness.Rights.RefreshTimeOut, AddressOf UpdateTimeOut
        RemoveHandler UserBusiness.Rights.SessionTimeOut, AddressOf SessionTimeOut

        RemoveHandler UserBusiness.Actions.RefreshTimeOut, AddressOf UpdateTimeOut
        RemoveHandler UserBusiness.Actions.SessionTimeOut, AddressOf SessionTimeOut
        'RemoveHandler UserBusiness.Actions.closeUserSession, AddressOf closeUserSession
        RemoveHandler Zamba.Core.WFRulesBusiness.RefreshTimeOut, AddressOf UpdateTimeOut
        RemoveHandler Zamba.AppBlock.ZForm.ShowInfo, AddressOf ShowInfo
        RemoveHandler Zamba.AppBlock.ZControl.ShowInfo, AddressOf ShowInfo
        RemoveHandler Zamba.AppBlock.ZForm.ShowError, AddressOf ShowError
        RemoveHandler Zamba.AppBlock.ZControl.ShowError, AddressOf ShowError
        RemoveHandler ZClass.ShowError, AddressOf ShowError
        RemoveHandler ZClass.ShowWarning, AddressOf ShowWarning
        RemoveHandler ZClass.ShowInfo, AddressOf ShowInfo
        RemoveHandler Zamba.AppBlock.ZForm.ShowWarning, AddressOf ShowWarning
        RemoveHandler Zamba.AppBlock.ZControl.ShowWarning, AddressOf ShowWarning
        AddHandler Zamba.AppBlock.ZForm.RefreshTimeOut, AddressOf UpdateTimeOut
        AddHandler Zamba.AppBlock.ZControl.RefreshTimeOut, AddressOf UpdateTimeOut
        AddHandler UserBusiness.Rights.RefreshTimeOut, AddressOf UpdateTimeOut
        AddHandler UserBusiness.Rights.SessionTimeOut, AddressOf SessionTimeOut

        AddHandler UserBusiness.Actions.RefreshTimeOut, AddressOf UpdateTimeOut
        AddHandler UserBusiness.Actions.SessionTimeOut, AddressOf SessionTimeOut
        'AddHandler UserBusiness.Actions.closeUserSession, AddressOf closeUserSession
        AddHandler Zamba.Core.WFRulesBusiness.RefreshTimeOut, AddressOf UpdateTimeOut
        AddHandler Zamba.AppBlock.ZForm.ShowInfo, AddressOf ShowInfo
        AddHandler Zamba.AppBlock.ZControl.ShowInfo, AddressOf ShowInfo
        AddHandler Zamba.AppBlock.ZForm.ShowError, AddressOf ShowError
        AddHandler Zamba.AppBlock.ZControl.ShowError, AddressOf ShowError
        AddHandler Zamba.AppBlock.ZForm.ShowWarning, AddressOf ShowWarning
        AddHandler Zamba.AppBlock.ZControl.ShowWarning, AddressOf ShowWarning
        AddHandler ZClass.ShowError, AddressOf ShowError
        AddHandler ZClass.ShowWarning, AddressOf ShowWarning
        AddHandler ZClass.ShowInfo, AddressOf ShowInfo
    End Sub


    ''' <summary> Delete Exception Logs > 30 Days. </summary>
    ''' <remarks></remarks>
    Private Sub BorrarException()
        Dim File As FileInfo = Nothing
        Dim RutaException As String = GetTempDir("\Exceptions").FullName


        If Directory.Exists(RutaException) = True Then

            For Each Path As String In Directory.GetFiles(RutaException)
                Try
                    File = New FileInfo(Path)

                    If Date.Now.Subtract(File.LastWriteTime).Days >= 30 Then
                        File.Delete()
                    End If

                Catch ex As Exception

                End Try
            Next
        End If
    End Sub
    Public Sub TareasMantenimientoInicialesdeZamba()
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Version de Zamba: " & Application.ProductVersion)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Borrando archivos temporales en thread - " & Now.ToString())
            Dim T1 As New Threading.Thread(AddressOf clearFiles)
            T1.Name = "Files"
            T1.Start()
        Catch ex As Threading.SynchronizationLockException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadAbortException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadInterruptedException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadStateException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Limpiar archivos al iniciar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clearFiles()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Borrando archivos Temporales - " & Now.ToString())
        DeleteTemps()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Borrando archivos Exception - " & Now.ToString())
        BorrarException()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Finalizacion de borrado de archivos - " & Now.ToString())
    End Sub

    ''' <summary>
    ''' Evento Load del Formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier  10/01/2011  Modified    Se agrega nombre de usuario al título de la ventana
    ''' </history>
    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

        'If Not String.IsNullOrEmpty(line) Then

        '    Dim PS As New ProxyServer
        '    Dim Datos As New Hashtable
        '    Datos.Add("USUARIO", Environment.UserName)
        '    Datos.Add("PC", Environment.MachineName)
        '    Argument = UCase(line)
        '    line = Nothing
        '    LoadMainform1()
        '    Dim Action As String
        '    PS.Run(Action, Argument, Datos)
        '    If Action = "cerrando" Then
        '        Close()
        '        Application.Exit()
        '        Exit Sub
        '    End If
        '    'Argument = Nothing
        '    TareasMantenimientoInicialesdeZamba()
        '    'Me.LoadMainform1()
        '    LoadMainform2(False)
        'Else

        If Not String.IsNullOrEmpty(line) Then

            If line.ToUpper().Contains("USERID=") And line.ToUpper().Contains("ZAMBA:\\") Then
                Dim usrWithId As String = line.Substring(line.IndexOf("USERID"), line.Length - line.IndexOf("USERID"))
                ' line = line.Remove(line.IndexOf("USERID"), line.Length - line.IndexOf("USERID"))

                Dim userId = Int64.Parse(usrWithId.Split("=")(1))

                If Membership.MembershipHelper.CurrentUser Is Nothing Then
                    If Not IsNothing(UserBusiness.Rights.ValidateLogIn(userId, ClientType.Desktop)) Then
                        'Dim timeout As Integer = UserPreferences.getValue("TimeOut", Sections.UserPreferences, "20")
                        'Dim winusername As String = UserGroupBusiness.GetUserorGroupNamebyId(userId)
                        'UcmServices.Login(timeout, "Cliente", userId, winusername, Environment.MachineName, 0, ServiceTypes.Report)
                        Me.UserValidatedFlag = True
                    End If
                End If
            End If
        End If

        If cerrando = False Then
            TareasMantenimientoInicialesdeZamba()
            LoadMainform1()

            If Not String.IsNullOrEmpty(line) Then
                If Not line.ToUpper().Contains("USERID=") Then

                    If ProxyServer.CheckForLegalNotice() = "cerrando" Then
                        Close()
                        Application.Exit()
                        Exit Sub
                    End If
                End If
            ElseIf String.IsNullOrEmpty(line) Then
                Try

                    If ProxyServer.CheckForLegalNotice() = "cerrando" Then
                        Close()
                        Application.Exit()
                        Exit Sub
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If


            LoadMainform2(False)
            Else
                If FlagDoClose = True Then
                Close()
                Application.Exit()
                Exit Sub
            Else
                Maximize()
            End If
        End If

        Try
            lblinformation.Text = "Zamba (" & Application.ProductVersion & ")"

            Dim Name As String = ZOptBusiness.GetValue("ClientTitle")
            If Not String.IsNullOrEmpty(Name) Then
                lblinformation.Text = lblinformation.Text & Name
            End If

            If IsNothing(Membership.MembershipHelper.CurrentUser) = False Then
                lblinformation.Text = lblinformation.Text & " - [" & Membership.MembershipHelper.CurrentUser.Name & "]"
            End If
            Me.Text = "Zamba Cliente"
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Gets directory to save data
    ''' </summary>
    ''' <param name="dire"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Ezequiel] 15/05/09 Created.
    ''' </history>
    Public Shared Function GetTempDir(ByVal dire As String) As IO.DirectoryInfo
        Dim Dir As IO.DirectoryInfo
        Try
            Dir = New IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software" & dire)
            If Dir.Exists = False Then
                Dir.Create()
            End If
        Catch
            Dir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & dire)
            If Dir.Exists = False Then
                Dir.Create()
            End If
        End Try
        Return Dir
    End Function
    ''' <summary>
    ''' Metodo que elimina los archivos temporales
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 21/04/09 Created
    '''           [Marcelo]  26/05/09 Modified </history>
    Private Sub DeleteTemps()

        Dim File As FileInfo = Nothing
        Dim path As String = String.Empty
        Try
            path = GetTempDir("\OfficeTemp").FullName
            If Directory.Exists(path) = True Then
                For Each filePath As String In Directory.GetFiles(path)
                    Try
                        File = New FileInfo(filePath)
                        File.Delete()
                    Catch ex As Exception
                    End Try
                Next
            End If
        Catch ex As Exception
        End Try
        Try
            path = GetTempDir("\IndexerTemp").FullName
            If Directory.Exists(path) = True Then
                For Each filePath As String In Directory.GetFiles(path)
                    Try
                        File = New FileInfo(filePath)
                        File.Delete()
                    Catch ex As Exception
                    End Try
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private LoadMainform1Done As Boolean
    Public Sub LoadMainform1()

        If cerrando = False Then
            If LoadMainform1Done = False Then
                Try
                    AddExceptionsHandlers()
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                Try
                    AddHandlers()
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                'Try
                '    'QuickSearch
                '    Dim enableQuickSearch As Boolean
                '    If Boolean.TryParse(UserPreferences.getValue("EnableQuickSearch", UPSections.Search, False), enableQuickSearch) AndAlso enableQuickSearch Then
                '        QuickSearchInit()
                '    End If
                'Catch ex As Exception
                '    ZClass.raiseerror(ex)
                'End Try

                'Try
                '    Dim CreateShortCutToZamba As Boolean
                '    If Boolean.TryParse(UserPreferences.getValue("CreateShortCutToZamba", UPSections.Search, False), CreateShortCutToZamba) AndAlso CreateShortCutToZamba Then
                '        ShortcutHandler.CreateLink(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), "Zamba.lnk"), Path.Combine(Application.StartupPath, "cliente.exe"))
                '    End If
                'Catch ex As Exception
                '    ' //  ZClass.raiseerror(ex)
                'End Try

                Try
                    'MARSH
                    'LoadAppIniFromServer()
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                LoadMainform1Done = True
            End If
        Else
            Application.Exit()
        End If
    End Sub

    Private Sub LoadAppIniFromServer()
        Try
            Dim appPathUp As String = GetLastAppServerPathFormConfigFile()
            ZTrace.WriteLineIf(ZTrace.IsInfo, Convert.ToString("appPathUp: ") & appPathUp)

            If appPathUp <> String.Empty AndAlso appPathUp.Contains("\") Then
                If (Directory.Exists(appPathUp)) Then
                    Dim pathsInisServer = Directory.GetFiles(appPathUp)
                    'Agarro el app.ini de la ruta

                    For Each currentFile As String In pathsInisServer
                        ZTrace.WriteLineIf(ZTrace.IsInfo, Convert.ToString("currentFile: ") & currentFile)
                        Dim appIniServer As New FileInfo(currentFile)
                        appIniServer.CopyTo(Path.Combine(Membership.MembershipHelper.AppConfigPath, appIniServer.Name), True)
                    Next
                End If
            End If

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, "Error: " + ex.ToString())
        End Try
    End Sub

    Public Shared Function GetLastAppServerPathFormConfigFile() As String
        Try
            Dim File As New FileInfo(Path.Combine(Membership.MembershipHelper.AppConfigPath, "AppIniServerPath.ini"))
            If (File.Exists) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "AppIniServerPath.ini: Existe")
                Dim sr As New StreamReader(File.FullName)
                Dim LastAppServerPath As String = sr.ReadToEnd()
                sr.Close()
                sr.Dispose()
                Return LastAppServerPath
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "AppIniServerPath.ini: NO Existe")
                Dim sr As New StreamWriter(File.FullName)
                'MARSH
                Dim LastAppServerPath As String = "\\arbue11as06v\zamba\Volumenes\appini"
                sr.WriteLine(LastAppServerPath)
                sr.Flush()
                sr.Close()
                sr.Dispose()
                ZTrace.WriteLineIf(ZTrace.IsInfo, Convert.ToString("Se creo AppIniServerPath.ini con: ") & LastAppServerPath)

                Return LastAppServerPath
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, "Error: " + ex.ToString())
            Return String.Empty
        End Try
    End Function

    Dim FlagFirstTime As Boolean = True

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <history> Marcelo Modified 07/08/09 Se modifico la mandera de setear el intervalo de la bandeja de entrada</history>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomás] 18/08/2009  Modified    Se corrige el error que se visualizaba la pantalla de búsquedas vacía cuando 
    '''                                     se realizaba una actualización con UpdateNGen.
    '''     [Tomás] 19/08/2009  Modified    Se iguala a nothing la variable 'line' para que al salir de la aplicacion no procese dichos datos
    '''' </history>
    Public Sub LoadMainform2(ByVal ReLogin As Boolean)
        Try
            If UserValidatedFlag = False Then
                Try
                    ValidarUsuarioYContrasenya(ReLogin)
                Catch ex As Exception
                    ''ZClass.raiseerror(ex)
                    Application.Exit()
                End Try
            End If

            If UserValidatedFlag Then
                Try

                    If Not DynamicButtonsLoaded Then
                        GenericRuleManager.LoadDynamicButtons(ToolbarZamba, ToolbarZamba.Items.IndexOf(BtnActiveTaks) + 1, True, ButtonPlace.BarraPrincipal, Nothing)
                        DynamicButtonsLoaded = True
                    End If
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "SetModulesRights")
                    SetModulesRights()
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Llamo al SearchMails")
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    Application.Exit()
                End Try

                'Try
                '    If (Argument IsNot Nothing OrElse Argument = String.Empty) OrElse (Argument.ToUpper().Contains("USERID=") AndAlso Not Argument.ToUpper().Contains("ZAMBA:\\")) Then
                '        If (Users.User.IsWfLic) = True Then
                '            If ReLogin = False Then OpenTasks(True)
                '        ElseIf FlagFirstTime = True Then

                '            If (Not IsNothing(FrmView)) Then
                '                FrmView.verifyIsTasksPanelIsSelected()
                '            End If

                '            FlagFirstTime = False
                '            If Me IsNot Nothing AndAlso Not Me.IsDisposed Then
                '                LoadViewer()
                '            End If


                '            If String.IsNullOrEmpty(Argument) Then

                '                If IsNothing(line) AndAlso FrmView IsNot Nothing Then
                '                    FrmView.SelectTab(TabPages.TabSearch)
                '                Else
                '                    If String.Compare(line, "CorrectUpdate") = 0 OrElse String.Compare(line, "WrongUpdate") = 0 Then
                '                        FrmView.SelectTab(TabPages.TabSearch)
                '                    End If
                '                End If
                '            End If

                '        Else
                '            If (Not IsNothing(FrmView)) Then
                '                FrmView.verifyIsTasksPanelIsSelected()
                '            End If
                '        End If
                '    End If

                'Catch ex As Exception
                '    ZClass.raiseerror(ex)
                'End Try

                'Argument = Nothing

                Try
                    USUARIOToolStripMenuItem1.Text = Users.User.GetUserName
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Else
                Application.Exit()
            End If

            If Me IsNot Nothing AndAlso Not Me.IsDisposed Then
                Dim WP As New BackgroundWorker()
                AddHandler WP.DoWork, AddressOf MessagesBusiness.SearchMailAndSaveAsync
                WP.RunWorkerAsync(Nothing)
                'Maximize()
            End If


            If FirstTimeLoaded Then
                FirstTimeLoaded = False

                ZCore.LoadCore()
                Dim PreLoad = New Zamba.PreLoad.PreLoadEngine()
                RemoveHandler PreLoadEngine.ChangeTextEvent, AddressOf UpdateStatusLabel
                RemoveHandler PreLoadEngine.CloseDialogEvent, AddressOf CleanStatusLabel
                AddHandler PreLoadEngine.ChangeTextEvent, AddressOf UpdateStatusLabel
                AddHandler PreLoadEngine.CloseDialogEvent, AddressOf CleanStatusLabel
                PreLoad.PreLoadObjects

            End If


            Try
                If Not String.IsNullOrEmpty(line) Then

                    Dim PS As New ProxyServer
                    Dim Datos As New Hashtable
                    Datos.Add("USUARIO", Environment.UserName)
                    Datos.Add("PC", Environment.MachineName)
                    Argument = UCase(line)
                    line = Nothing
                    TimeOutFlag = False

                    Dim Action As String
                    PS.Run(Action, Argument, Datos)
                Else
                    If Me IsNot Nothing AndAlso Not Me.IsDisposed AndAlso FrmView Is Nothing Then
                        LoadViewer()
                    End If
                    If FrmView IsNot Nothing Then
                        Dim MenuDefaultEnCliente As String = UserPreferences.getValue("MenuDefaultEnCliente", UPSections.UserPreferences, "Tareas")
                        Select Case MenuDefaultEnCliente
                            Case "Busqueda"
                                FrmView.SelectTab(TabPages.TabSearch)
                            Case "Tareas"
                                FrmView.SelectTab(TabPages.TabTasks)
                            Case "Insertar"
                                FrmView.SelectTab(TabPages.TabIndexer)
                            Case "Caratula"
                                FrmView.SelectTab(TabPages.TabBarcode)
                        End Select
                    End If
                End If

                TimeOutFlag = False

                InitializeConnectionTimer()

                'RemoveHandler UserBusiness.Rights.closeUserSession, AddressOf closeUserSession
                'AddHandler UserBusiness.Rights.closeUserSession, AddressOf closeUserSession

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub QuickSearchInit()
        Try
            Dim QS As New frmQuickSearch(SearchModes.Desktop, Me)
            QS.StartListener()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CleanStatusLabel()

        Try
            Invoke(New UpdateStatusText(AddressOf ChangeText), String.Empty)
        Catch ex As System.InvalidOperationException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Delegate Sub UpdateStatusText(text As String)
    Private Sub UpdateStatusLabel(text As String)
        Try
            Invoke(New UpdateStatusText(AddressOf ChangeText), text)
        Catch ex As System.InvalidOperationException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ChangeText(Text As Object)
        lblpreloadstatus.ToolTipText = Text
        If Text = String.Empty Then
            lblpreloadstatus.Visible = False
        Else
            lblpreloadstatus.Image = Global.Zamba.Client.My.Resources.Resources.loading
            lblpreloadstatus.Visible = True
        End If
    End Sub
#End Region

#Region "Search"
    Private WithEvents FrmView As MainPanel
    Delegate Sub DLoadViewer()

    Private Sub EventoMenu(ByVal Nombre As String)
        Select Case Nombre
            Case "CONFIGURARIMPRESORA"
                ConfigurarImpresora()
            Case "CONFIGURARPAGINA"
                ConfigurarPagina()
            Case "NUEVOWORD"
                NuevoWord()
            Case "NUEVOEXCEL"
                NuevoExcel()
            Case "NUEVOPORWERPOINT"
                NuevoPowerpoint()
            Case "NUEVAVERSION"
            Case "HISTORIAL"
                ShowUserHistory()
            Case "CAMBIODECLAVE"
                CambioDeClave()
            Case "PREFERENCIAS"
                Preferencias()
            Case "ESTACION"
            Case "EJECUTARBATCHES"
            Case "EXPORTARAHTML"
            Case "VEREXPORTACIONES"
            Case "PLANTILLAS"
                VerPlantillas()
            Case "SOBREZAMBA"
                Open_About()
            Case "MANUALDELUSUARIO"
                ManualDelUsuario()
        End Select
    End Sub

    Private Sub LoadViewer()
        Try
            If FrmView Is Nothing AndAlso Membership.MembershipHelper.CurrentUser IsNot Nothing Then
                loadTabButtonsDictionary()
                btnStart.Visible = UserPreferences.getValue("ShowHomeTabinDesktop", UPSections.UserPreferences, True)
                FrmView = New MainPanel(Membership.MembershipHelper.CurrentUser.ID)
                FrmView.MdiParent = Me
                FrmView.WindowState = FormWindowState.Normal

                RemoveHandler MainPanel.FilesDragged, AddressOf AllFilesDragged
                RemoveHandler FrmView._EventoMenu, AddressOf EventoMenu
                RemoveHandler FrmView.TabChanged, AddressOf ActiveButton
                AddHandler FrmView._EventoMenu, AddressOf EventoMenu
                AddHandler MainPanel.FilesDragged, AddressOf AllFilesDragged
                AddHandler FrmView.TabChanged, AddressOf ActiveButton

                RemoveHandler FrmView._tasksOpenedCountChanged, AddressOf ChangeTasksDetailsBtnState
                AddHandler FrmView._tasksOpenedCountChanged, AddressOf ChangeTasksDetailsBtnState

                FrmView.Dock = DockStyle.Fill
                Maximize()
                FrmView.Show()
                FrmView.WindowState = FormWindowState.Normal
                ActivateMdiChild(FrmView)

                '  SetupAutoComplete()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ChangeTasksDetailsBtnState(count As Integer)
        Try
            If count > 0 Then
                BtnActiveTaks.Enabled = True
            Else
                BtnActiveTaks.Enabled = False
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ShowTab(ByVal Tab As TabPages)
        LoadViewer()
        FrmView.SelectTab(Tab)
    End Sub

    Private Sub ShowResults(ByRef Results As DataTable, ByVal SearchName As String, ByVal Search As ISearch, ByVal fromCommandLineArgs As Boolean)
        Try
            LoadViewer()
            FrmView.ShowWFTaksbyResults(Results, Search, Results.MinimumCapacity)
            FrmView.BringToFront()
            'If Results.Rows.Count = 0 Then
            '    If fromCommandLineArgs Then
            '        'Se modifica a MessageBox, ya que a veces al demorar la carga de los controles de Zamba, 
            '        'la notificación se pierde y el usuario no se entera del mensaje.
            '        MessageBox.Show("No se puede tener acceso al documento solicitado." & vbCrLf &
            '                 "Es posible que no tenga permisos de visualización o que el mismo haya sido eliminado.",
            '                 "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Else
            '        ShowInfo("NO SE ENCONTRARON RESULTADOS", String.Empty)
            '    End If
            'Else
            '    LoadViewer()
            '    If Results.Rows.Count = 1 Then
            '        ShowInfo("SE ENCONTRO 1 RESULTADO", String.Empty)
            '    Else
            '        ShowInfo("SE ENCONTRARON " & Results.MinimumCapacity & " RESULTADOS", String.Empty)
            '    End If

            '    FrmView.ShowWFTaksbyResults(Results, Search, Results.MinimumCapacity)

            '    FrmView.BringToFront()

            'End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Delegate Sub CallShowResult(ByRef Results As DataTable, ByVal SearchName As String, ByVal Search As ISearch, ByVal fromCommandLineArgs As Boolean)
    Public Sub InvokeShowResults(ByVal Results As DataTable, ByVal SearchName As String, ByVal Search As ISearch, Optional ByVal fromCommandLineArgs As Boolean = False)
        Try
            If Results.Rows.Count > 0 Then
                Invoke(New CallShowResult(AddressOf ShowResults), New Object() {Results, SearchName, Search, fromCommandLineArgs})
            Else

            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "WorkFlow"

    ''' <summary>
    ''' Método que sirve para iniciar la sección de Tareas
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    11/06/2009  Modified     En caso de tener licencia documental se realiza un cambio de licencia a Workflow
    '''     [Gaston]    12/06/2009  Modified     Validación de usuario y WFLic
    '''     [Marcelo]   01/10/2009  Modified     Rules Preferences Load Event
    ''' </history>
    Private Sub OpenTasks(ByVal ShowList As Boolean)
        Try
            If IsNothing(FrmView) Then LoadViewer()
            FrmView.Showtasks(ShowList)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub



#End Region

#Region "Metodos Privados"

    ''' <summary>
    ''' Método utilizado para inicializar el ConnectionTimer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeConnectionTimer()
        If (ConnectionTimer Is Nothing) Then

            Dim duetime As Int32 = Int32.Parse(UserPreferences.getValue("TimeOut", UPSections.UserPreferences, 30)) * 60000
            Dim period As Int32 = Int32.Parse(UserPreferences.getValue("TimeOut", UPSections.UserPreferences, 30)) * 60000
            If (duetime >= 30 AndAlso period >= 30) Then
                ConnectionTimer = New ZTimer(CTCB, State, duetime, period, Int32.Parse(UserPreferences.getValue("TimeStartT", UPSections.UserPreferences, "0")), Int32.Parse(UserPreferences.getValue("TimeEndT", UPSections.UserPreferences, "24")))
            Else
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "El TimeOut esta configurado para menos de 30 minutos.")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método utilizado para actualizar el ConnectionTimer
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    09/05/2008  Modified
    ''' </history>
    Private Sub UpdateTimeOut()
        Try
            If (ConnectionTimer IsNot Nothing) Then
                ConnectionTimer.Change(Int32.Parse(UserPreferences.getValue("TimeOut", UPSections.UserPreferences, 30)) * 60000, Int32.Parse(UserPreferences.getValue("TimeOut", UPSections.UserPreferences, 30)) * 60000)
                If TimeOutFlag = True Then
                    SessionTimeOut()
                End If
            End If
        Catch ex As Threading.SynchronizationLockException
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.Message + " " + ex.StackTrace)
        Catch ex As Threading.ThreadAbortException
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.Message + " " + ex.StackTrace)
        Catch ex As Threading.ThreadInterruptedException
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.Message + " " + ex.StackTrace)
        Catch ex As Threading.ThreadStateException
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.Message + " " + ex.StackTrace)
        Catch ex As ObjectDisposedException
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.Message + " " + ex.StackTrace)
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.Message + " " + ex.StackTrace)
        End Try
    End Sub

    ''' <summary>
    ''' Método dependiendo de la configuración de usuario devuelve los valores de header y footer como estaban.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Tomas 11/03/09]    Created
    ''' </history>
    Private Sub GetBackFooterAndHeaderKeys()
        ' Obtiene la configuración del usuario
        Dim valor As Object = ZOptBusiness.GetValue("ShowMsgBox" + Environment.MachineName.ToString())
        Dim modificarRegistro As Object = ZOptBusiness.GetValue("ModifyRegistry" + Environment.MachineName.ToString())

        If valor IsNot Nothing AndAlso modificarRegistro IsNot Nothing Then
            ' En caso de que el usuario decidiera volver a mostrar el mensaje de confirmación y que haya
            ' decidido modificar el header y footer, estas 2 últimas vuelven a tomar sus valores originales.
            If valor = 1 And modificarRegistro = 1 Then
                ' Obtengo el directorio de las llaves de header y footer
                Dim oKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Internet Explorer\PageSetup", True)

                If Not (IsNothing(oKey)) Then
                    ' Los valores header y footer vuelven a sus valores por defecto 
                    oKey.SetValue("header", "&w&bPage &p of &P")
                    oKey.SetValue("footer", "&u&b&d")
                End If

                oKey.Close()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método utilizado para colocar el flag TimeOutFlag en True, indicando que el time_out del usuario ya expiro
    ''' </summary>
    ''' <param name="State"></param>
    ''' <remarks></remarks>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId:="State")> Private Sub TimeOut(ByVal State As Object)
        TimeOutFlag = True
        Access.Server.RemoveConnection()
    End Sub

    Dim sync2 As Object = New Object()
    Dim sync1 As Object = New Object()
    Dim sync As Object = New Object()

    ''' <summary>
    ''' Método utilizado para quitar al cliente (cuyo time_out expiro) de la tabla UCM 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    12/05/2008  Modified
    ''' </history>
    Private Sub SessionTimeOut()
        'If TimeOutFlag Then
        SyncLock sync1
            If TimeOutFlag Then
                ' El flag que indica que el time_out expiro se coloca en false
                TimeOutFlag = False

                ' Se detiene el Thread ConnectionTimer y se liberan los recursos
                If ConnectionTimer IsNot Nothing Then
                    ConnectionTimer.Dispose()
                    ConnectionTimer = Nothing
                End If

                Try
                    ' Si licencias es 1 y hay un cliente en UCM no remover
                    Access.Server.RemoveConnection()
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                ReLogin()
            End If
        End SyncLock
        'End If
    End Sub

    ''' <summary>
    ''' Método que sirve para cerrar la sesión del usuario si éste no se encuentra más en la tabla UCM
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    04/06/2009  Created 
    ''' </history>
    Private Sub closeUserSession()
        If TimeOutFlag = False Then
            TimeOutFlag = True
            SessionTimeOut()
        End If
    End Sub

    ''' <summary>
    ''' Método utilizado para limpiar la instancia de usuario actual y mostrar un nuevo formulario de login
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReLogin()
        If Not IsNothing(Membership.MembershipHelper.CurrentUser) Then
            SyncLock sync2
                If Not IsNothing(Membership.MembershipHelper.CurrentUser) Then
                    MembershipHelper.SetCurrentUser(Nothing)
                    UserValidatedFlag = False
                    LoadMainform2(True)
                End If
            End SyncLock
        End If
    End Sub

#End Region

#Region "Public Methods"

    Public Shared Sub Open_About()
        Try
            Dim About As New About
            About.ShowDialog()
            About.Dispose()
            About = Nothing
        Catch ex As Exception
            ' zclass.raiseerror(ex)
        End Try
    End Sub
    'Public Sub Open_Batch(ByVal Batch As ZBatch)
    '    'TODO: Ver permisos de batches
    '    Try
    '        If IsNothing(Batch) = False AndAlso Batch.Results.Count > 0 Then
    '            Insert(Batch.Results)
    '        End If
    '    Catch ex As Exception
    '        zclass.raiseerror(ex)
    '    End Try
    'End Sub

#End Region

    'Flag than indicates if the client is reloaded
    Public Shared FlagInitialize As Boolean
    Private Sub MainForm_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Activated
        Try
            If TimeOutFlag Then
                SessionTimeOut()
            End If
            If FlagInitialize Then
                'Este flag DEBE ser el primero en ser ejecutado ya que al iniciar sesión en Zamba este evento es llamado 2 veces
                'y por alguna razón se ejecuta como 2 hilos separados, lo que produce errores en el MaximizeForm.
                FlagInitialize = False

                'Maximize and search the result
                CheckInitialArguments()
                MaximizeForm()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


#Region "Forms & Templates"
    Private Sub VerPlantillas()
        Try
            If IsNothing(FrmView) = True Then
                LoadViewer()
            End If

            FrmView.ShowTempletasAdmin()

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region
#Region "Insertar"

    Private Sub Insert(Optional ByVal NewDocument As String = "")
        'If RightsBusiness.GetUserRights(ObjectTypes.ModuleInsert, RightsType.Use) = False Then
        '    MessageBox.Show("Ud No tiene permiso para utilizar este módulo", "Zamba Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If
        Try
            If IsNothing(FrmView) = True Then LoadViewer()
            FrmView.Insert(NewDocument)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

#Region "Usuarios - Preferencias e Historial"
    Private Sub Preferencias()
        Try
            Dim Userpref As New frmUserPreferences
            Userpref.ShowDialog()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ShowUserHistory()
        Try
            If IsNothing(FrmView) = True Then LoadViewer()
            FrmView.ShowUserHistory()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region


#Region "Codigos de barra"

    Private Sub showBarCode()

        Try
            Try
                If IsNothing(FrmView) = True Then LoadViewer()
                FrmView.ShowCoverPage()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "LoadICons"
    'Private Sub LoadIcons()
    '    Try
    '        DocTypeIcons.LoadIcons(Me.IconList)
    '    Catch ex As Exception
    '        zclass.raiseerror(ex)
    '    End Try
    'End Sub
#End Region

#Region "Viewer Manejo de Menues"

    'CUANDO EL FORMULARIO SE CERRO ME FIJO SI QUEDA ALGUN VIEWER Y SINO DESHABILITO EL MENU DE ENVIAR A
    Private Sub FrmViewer_Closed(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dispose(True)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "notifyIcon"
    Private Shadows Sub ShowInfo(ByVal msg As String, ByVal Title As String, ByVal TMsg As Zamba.AppBlock.Enums.TMsg, ByVal Tinterfaz As Zamba.AppBlock.Enums.Tinterfaz)
        Try
            AddIcon(Me)
            INotification(msg, Title)
            startNotifyTimer()
        Catch ex As System.ObjectDisposedException
        Catch ex As Exception
        End Try
    End Sub
    Private Shadows Sub ShowInfo(ByVal msg As String, ByVal title As String)
        Try
            AddIcon(Me)
            INotification(msg, title)
            startNotifyTimer()
        Catch ex As System.ObjectDisposedException
        Catch ex As Exception
        End Try
    End Sub

    Private Shadows Sub ShowError(ByVal msg As String)
        Try
            AddIcon(Me)
            ENotification(msg)
            startNotifyTimer()
        Catch ex As System.ObjectDisposedException
        Catch ex As Exception
        End Try
    End Sub

    Private Shadows Sub ShowWarning(ByVal msg As String)
        Try
            AddIcon(Me)
            WNotification(msg)
            startNotifyTimer()
        Catch ex As System.ObjectDisposedException
        Catch ex As Exception
        End Try
    End Sub

    Private TNotify As Threading.Timer
    Private Sub startNotifyTimer()
        Try
            TNotify = New Threading.Timer(New Threading.TimerCallback(AddressOf CloseNotify), State, 5000, 5000)
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStateException
        Catch ex As Exception
        End Try
    End Sub

    Delegate Sub dremoveicon(ByRef Form As Form)
    Private Sub CloseNotify(ByVal state As Object)
        Try
            Invoke(New dremoveicon(AddressOf RemoveIcon), New Object() {Me})
            TNotify.Dispose()
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStateException
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Minimize To Try"


    Private Sub notifyIcon1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NotifyIcon.Click
        MaximizeForm()
        NotifyIcon.Visible = False

    End Sub
    Private Sub MaximizeForm()
        Try
            If IsDisposed = False Then
                Show()
                ShowInTaskbar = True

                If UserValidatedFlag Then
                    If Not String.IsNullOrEmpty(line) AndAlso line.Trim.Length > 0 Then
                        Dim PS As New ProxyServer
                        Dim Datos As New Hashtable
                        Datos.Add("USUARIO", Environment.UserName)
                        Datos.Add("PC", Environment.MachineName)
                        Argument = line.Trim
                        line = Nothing
                        PS.Run(String.Empty, Argument, Datos)
                    End If

                End If
                Maximize()
            End If
        Catch ex As ObjectDisposedException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region
#Region "Drag & Drop Files"

    Private Sub AllFilesDragged(ByVal Files() As String)
        If RightsBusiness.GetUserRights(ObjectTypes.ModuleElectronicDoc, RightsType.Use) = False Then
            MessageBox.Show("Usted no tiene permiso para utilizar Documentos Electrónicos", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim ArrayFiles As New ArrayList

        For Each Item As String In Files
            ArrayFiles.Add(Item)
        Next
        Try
            'Do Until IsNothing(NewIndexer) = False
            Insert()
            'Loop
            '            If IsNothing(NewIndexer) = False Then NewIndexer.AddFiles(ArrayFiles)
        Catch ex As Exception
        End Try
    End Sub

#End Region
    Private Sub MenuItem11_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles old_MenuItem11.Click
        MaximizeForm()
    End Sub
    Private Sub MenuItem16_Click_1(ByVal sender As System.Object, ByVal e As EventArgs) Handles old_MenuItem16.Click
        Try
            MustCloseZamba = True

            closeZamba()
        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub MainForm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Click
        Try
            If Users.User.IsNothingUser = True Then
                Try
                    If IsNothing(Login) = False Then Login.Focus()
                Catch
                End Try
            End If
        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub MainForm_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.GotFocus
        Try
            If Users.User.IsNothingUser AndAlso Login IsNot Nothing Then
                Login.Focus()
            End If
        Catch
        End Try
    End Sub

    Private Shared Sub SetLotusNotesDefault()
        Access.utilities.SetLotusNotesDefault()
    End Sub

    Private Sub MenuButtonItem1_Activate_1(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            If IsNothing(FrmView) = True Then LoadViewer()
            FrmView.CreateWordWBarcode()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub



#Region "Menubar"

    Private Sub WordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles WordToolStripMenuItem.Click
        NuevoWord()
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExcelToolStripMenuItem.Click
        NuevoExcel()
    End Sub

    Private Sub PowerpointToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PowerpointToolStripMenuItem.Click
        NuevoPowerpoint()
    End Sub

    Private Sub InsertarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            Insert()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CarátulaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            showBarCode()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ConfigurarPáginaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ConfigurarPáginaToolStripMenuItem.Click
        ConfigurarPagina()
    End Sub

    Private Sub ConfigurarImpresoraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ConfigurarImpresoraToolStripMenuItem.Click
        ConfigurarImpresora()
    End Sub

    Private Sub CerrarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            closeall()
        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub HistorialToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles HistorialToolStripMenuItem.Click
        Try
            ShowUserHistory()
        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CambioDeClaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CambioDeClaveToolStripMenuItem.Click
        CambioDeClave()
    End Sub

    Private Sub PreferenciasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PreferenciasToolStripMenuItem.Click
        Preferencias()
    End Sub

    Private Sub EstaciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EstaciónToolStripMenuItem.Click
        Estacion()
    End Sub

    Private Sub GenerarBarcodeEnWordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles GenerarBarcodeEnWordToolStripMenuItem.Click
        Try
            If IsNothing(FrmView) = True Then LoadViewer()
            FrmView.CreateWordWBarcode()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CascadaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            LayoutMdi(MdiLayout.Cascade)
        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub VerticalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            LayoutMdi(MdiLayout.TileVertical)
        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub HorizontalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            LayoutMdi(MdiLayout.TileHorizontal)
        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ManualDeUsuarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ManualDeUsuarioToolStripMenuItem.Click
        ManualDelUsuario()
    End Sub

    Private Sub ReleaseNotesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReleaseNotesToolStripMenuItem.Click
        ReleaseNotes()
    End Sub

    Private Sub SobreZambaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SobreZambaToolStripMenuItem.Click
        Open_About()
    End Sub

    Private Sub BúsquedaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            LoadViewer()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub PlantillasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PlantillasToolStripMenuItem.Click
        VerPlantillas()
    End Sub

#End Region

    Private Shared ReadOnly tabButtons As New Dictionary(Of Int32, String)

    Private Sub loadTabButtonsDictionary()
        tabButtons.Add(TabPages.TabStart, btnStart.Name)
        tabButtons.Add(TabPages.TabSearch, BtnSearch.Name)
        tabButtons.Add(TabPages.TabIndexer, BtnInsert.Name)
        tabButtons.Add(TabPages.TabBarcode, BtnCaratulas.Name)
        tabButtons.Add(TabPages.TabTasks, BtnTareas.Name)
        tabButtons.Add(TabPages.TabTaskDetails, BtnActiveTaks.Name)
    End Sub

    Private Sub ActiveButton(button As Int32)
        Try
            For Each btn As ToolStripItem In ToolbarZamba.Items
                If TypeOf btn Is ToolStripButton Then
                    DirectCast(btn, ToolStripButton).Checked = False
                    DirectCast(btn, ToolStripButton).CheckState = CheckState.Unchecked
                    DirectCast(btn, ToolStripButton).BackColor = Color.Transparent

                End If
            Next

            If tabButtons.ContainsKey(button) Then
                If TypeOf ToolbarZamba.Items.Item(tabButtons(button)) Is ToolStripButton Then
                    Dim btn As ToolStripButton = DirectCast(ToolbarZamba.Items.Item(tabButtons(button)), ToolStripButton)
                    btn.Checked = False
                    btn.CheckState = CheckState.Unchecked
                    btn.BackColor = Color.LightSkyBlue
                    btn.Select()
                End If
            End If
            Activate()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub ToolBarButtons_Click(ByVal sender As System.Object, ByVal e As EventArgs) _
            Handles BtnSearch.Click, BtnCaratulas.Click, BtnInsert.Click, BtnResults.Click, BtnCerrar.Click, BtnTareas.Click, btnStart.Click, BtnActiveTaks.Click

        Try
            Dim buttonTag As String = DirectCast(sender, System.Windows.Forms.ToolStripItem).Tag


            Select Case CStr(buttonTag).ToUpper

                Case "BUSCAR"
                    Try
                        ShowTab(TabPages.TabSearch)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Case "INSERTAR"
                    Try
                        Insert()
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Case "RESULTADOS"
                    Try
                        ShowTab(TabPages.TabResults)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Case "CARATULA"
                    Try
                        showBarCode()
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                Case "TAREAS"
                    Try
                        OpenTasks(True)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Case "CERRAR"
                    Try
                        closeZamba()
                        ActiveButton(-1)
                    Catch ex As ThreadAbortException

                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
            End Select
        Catch ex As ThreadAbortException

        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Function closeZamba() As Boolean
        Try

            If FrmView IsNot Nothing Then
                If FrmView.HayTareasAbiertas() OrElse FrmView.HayDocumentosAbiertos() Then

                    If FrmView.CanCloseZamba Then
                        FlagClose = True
                    Else
                        FlagClose = False
                        Return FlagClose
                    End If

                Else
                    If cerrando OrElse (Convert.ToBoolean(UserPreferences.getValue("HideZambaInTray", UPSections.UserPreferences, True)) = False AndAlso MessageBox.Show("   ¿Desea cerrar ZAMBA?   ", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No) Then
                        FlagClose = False
                        Return FlagClose
                    Else
                        FlagClose = True
                    End If
                End If
            End If

            If Convert.ToBoolean(UserPreferences.getValue("HideZambaInTray", UPSections.UserPreferences, True)) = True AndAlso FlagClose = True AndAlso MustCloseZamba = False Then
                If Not IsNothing(NotifyIcon) Then NotifyIcon.Visible = True
                Hide()
                FlagClose = False
                Return FlagClose
            Else
                If Not IsNothing(NotifyIcon) Then
                    NotifyIcon.Visible = False
                End If

                Try
                    ' Dependiendo de la configuración de usuario devuelve los valores de header y footer como estaban.
                    GetBackFooterAndHeaderKeys()
                Catch
                End Try

                Try
                    Access.Server.RemoveConnection()
                    'Ucm.RemoveConnection(Users.User.ConnectionId)
                    If Users.User.IsWfLic = True Then Access.Server.RemoveWFConnections() 'Ucm.RemoveWFConnections(Users.User.ConnectionId)

                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                Try
                    Ucm.RemoveConnection()
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                Try
                    CleanExceptions()
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                Try
                    'Permite ejecutar un exe al cerrar Zamba. el nombre del exe debe guardarse en execute.dat
                    If File.Exists(".\execute.dat") Then
                        Dim sr As New IO.StreamReader(".\execute.dat")
                        Dim program As String = sr.ReadLine
                        sr.Close()
                        If program.Trim <> String.Empty Then
                            Shell(program.Trim, AppWinStyle.Hide, False)
                        End If
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                Application.Exit()

                Return FlagClose

            End If
        Catch es As InvalidOperationException
            Application.Exit()
            Return True
        Catch ex As Exception
            Application.Exit()
            Return True
        End Try
    End Function
    ''' <summary>
    ''' Se fija si puede cerrar zamba.
    ''' Si hay documentos o tareas abiertas, no se cierra, va cerrando la activa hasta que no quede nada abierto.
    ''' </summary>
    ''' <returns>Retorna True si ya se puede cerrar zamba, False si hay cosas por cerrar.</returns>
    Private Function closeActiveTab() As Boolean
        Try

            If FrmView IsNot Nothing Then
                If FrmView.CanCloseZamba Then
                    FlagClose = False
                    Return FlagClose
                End If
            End If

            If Convert.ToBoolean(UserPreferences.getValue("HideZambaInTray", UPSections.UserPreferences, True)) = True AndAlso FlagClose = False Then
                If Not IsNothing(NotifyIcon) Then NotifyIcon.Visible = True
                Hide()
                FlagClose = False
                Return FlagClose
            Else
                If Not IsNothing(NotifyIcon) Then
                    NotifyIcon.Visible = False
                End If
                If cerrando OrElse MessageBox.Show("   ¿Desea cerrar ZAMBA?   ", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    Try
                        ' Dependiendo de la configuración de usuario devuelve los valores de header y footer como estaban.
                        GetBackFooterAndHeaderKeys()
                    Catch
                    End Try

                    Try
                        Access.Server.RemoveConnection()
                        'Ucm.RemoveConnection(Users.User.ConnectionId)
                        If Users.User.IsWfLic = True Then Access.Server.RemoveWFConnections() 'Ucm.RemoveWFConnections(Users.User.ConnectionId)

                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    Try
                        Ucm.RemoveConnection()
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    Try
                        CleanExceptions()
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    Try
                        'Permite ejecutar un exe al cerrar Zamba. el nombre del exe debe guardarse en execute.dat
                        If File.Exists(".\execute.dat") Then
                            Dim sr As New IO.StreamReader(".\execute.dat")
                            Dim program As String = sr.ReadLine
                            sr.Close()
                            If program.Trim <> String.Empty Then
                                Shell(program.Trim, AppWinStyle.Hide, False)
                            End If
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    If MessageBoxButtons.YesNo Then
                        Application.Exit()
                    Else
                        Activate()
                    End If
                    FlagClose = True
                    Return FlagClose
                Else
                    FlagClose = False
                    Return FlagClose
                End If
            End If
        Catch es As InvalidOperationException
        Catch ex As Exception
        End Try
    End Function


    Private Sub BtnActiveTaks_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnActiveTaks.Click
        OpenTasks(False)
    End Sub

    Private Sub DepuradorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DepuradorToolStripMenuItem.Click
        Try
            StartDebugger()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub StartDebugger()
        'SyncLock Debugger
        If Debugger Is Nothing Then
            Dim th As Thread = New Thread(AddressOf LoadDebugger)
            th.SetApartmentState(ApartmentState.STA)
            th.Start()
        End If
        'End SyncLock
    End Sub

    Dim Debugger As DebuggerForm

    Private Sub LoadDebugger()
        Try
            'If Debugger Is Nothing Then
            Debugger = New DebuggerForm
            RemoveHandler ZClass.eHandleModuleWithDoctype, AddressOf ExecHandleModuleWithDoctype
            RemoveHandler WFRuleParent.RuleToExecute, AddressOf ExecRuleToExecute
            RemoveHandler WFRuleParent.RuleExecuted, AddressOf ExecRuleExecuted
            RemoveHandler WFRuleParent.RuleExecutedError, AddressOf ExecRuleExecutedError
            AddHandler ZClass.eHandleModuleWithDoctype, AddressOf ExecHandleModuleWithDoctype
            AddHandler WFRuleParent.RuleToExecute, AddressOf ExecRuleToExecute
            AddHandler WFRuleParent.RuleExecuted, AddressOf ExecRuleExecuted
            AddHandler WFRuleParent.RuleExecutedError, AddressOf ExecRuleExecutedError
            'End If
            Application.Run(Debugger)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Debugger = Nothing
        End Try
    End Sub

    Private Sub ExecHandleModuleWithDoctype(resultActionType As ResultActions, ByRef currentResult As ZambaCore, docType As ZambaCore, Params As Hashtable)
        Try
            If Debugger IsNot Nothing Then
                Debugger.HandleModuleWithDoctype(resultActionType, currentResult, docType, Params)
            End If
        Catch ex As InvalidOperationException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ExecRuleToExecute(ByVal Rule As WFRuleParent, ByVal Tasks As List(Of ITaskResult))
        Try
            If Debugger IsNot Nothing Then
                Debugger.RuleToExecute(Rule, Tasks)
            End If
        Catch ex As InvalidOperationException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ExecRuleExecuted(ByVal Rule As WFRuleParent, ByVal Tasks As List(Of ITaskResult))
        Try
            If Debugger IsNot Nothing Then
                Debugger.RuleExecuted(Rule, Tasks)
            End If
        Catch ex As InvalidOperationException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ExecRuleExecutedError(ByVal Rule As WFRuleParent, ByVal Tasks As List(Of ITaskResult), ByVal excep As Exception, ByRef errorbreakpoint As Boolean)
        Try
            If Debugger IsNot Nothing Then
                Debugger.RuleExecutedError(Rule, Tasks, excep, errorbreakpoint)
            End If
        Catch ex As InvalidOperationException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub MnuInformes_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnInformes.Click
        Try
            Dim path As String = Application.StartupPath & "\Zamba.ReportsBuilder.exe USERID=" & MembershipHelper.CurrentUser.ID
            Shell(path, AppWinStyle.NormalFocus)
            btnInformes.Select()

        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ocurrió un error al abrir la configuración del generador de reportes", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


    ''' <summary>
    ''' Limpia la cache
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cleanCacheMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cleanCacheMenuItem.Click
        Try
            Cache.CacheBusiness.ClearAllCache()
            Zamba.Core.ZCore.Cleardata()

            MessageBox.Show("Cache limpiado correctamente")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub RegistrarZambaEnWindowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles RegistrarZambaEnWindowsToolStripMenuItem.Click
        Try
            Dim CreateShortCutToZamba As Boolean
            If Boolean.TryParse(UserPreferences.getValue("RegisterZamba", UPSections.Search, False), CreateShortCutToZamba) AndAlso CreateShortCutToZamba Then
                Dim Reg As New Register
                If Reg.CrearRegZamba() AndAlso Reg.regReportsBuilder() Then
                    MessageBox.Show("Registracion correcta", "Registro de Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("No se pudo registrar Zamba", "Registro de Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End If
                Reg = Nothing
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Muestra la ventana de reporte de error
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReportarErrorToolStripMenuItem_Click(sender As System.Object, e As EventArgs) Handles mnuReportError.Click
        Try
            If frmError Is Nothing OrElse frmError.IsDisposed Then
                frmError = New FrmErrorReporting
            End If
            frmError.Show()
            frmError.Activate()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub



    Private Class MyRenderer
        Inherits ToolStripProfessionalRenderer

        Public Sub New()
            RoundedEdges = False
        End Sub

        Protected Overrides Sub OnRenderButtonBackground(e As ToolStripItemRenderEventArgs)
            Dim btn As ToolStripButton = CType(e.Item, ToolStripButton)
            If btn IsNot Nothing AndAlso btn.CheckOnClick AndAlso btn.Checked Then
                Dim bounds As Rectangle = New Rectangle(Point.Empty, e.Item.Size)
                'e.Graphics.FillRectangle(Brushes.LightBlue, bounds)
                Dim myBrush As New SolidBrush(Color.FromArgb(194, 224, 255))
                e.Graphics.FillRectangle(myBrush, bounds)
            Else
                MyBase.OnRenderButtonBackground(e)
            End If
        End Sub


    End Class

    Private Sub RegistrarBrowserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegistrarBrowserToolStripMenuItem.Click
        'Dim wbe = New WBEmulator()
        'If wbe.IsBrowserEmulationSet() = False Then
        '    wbe.SetBrowserEmulationVersion()
        'End If

        Dim IEFC As New IEFeatureControl
        IEFC.SetBrowserFeatureControl()
    End Sub

    Private Sub BtnMaximize_Click(sender As Object, e As EventArgs) Handles BtnMaximize.Click
        SwitchScreenState()
    End Sub

    Private Sub Normal()
        SetBounds(Screen.FromHandle(Handle).WorkingArea.Left, Screen.FromHandle(Handle).WorkingArea.Top, Screen.FromHandle(Handle).WorkingArea.Width - 25, Screen.FromHandle(Handle).PrimaryScreen.WorkingArea.Height - 25)
        WindowState = FormWindowState.Normal
        BtnMaximize.ToolTipText = "Maximizar"
        BtnMaximize.Image = Global.Zamba.Client.My.Resources.Resources.appbar_app
    End Sub
    Private Sub Maximize()
        ClientSize = Screen.FromHandle(Handle).WorkingArea.Size
        MaximizedBounds = Screen.FromHandle(Handle).WorkingArea
        Left = 0
        Top = 0
        BtnMaximize.Image = Global.Zamba.Client.My.Resources.Resources.appbar_window_restore
        BtnMaximize.ToolTipText = "Normal"
        WindowState = FormWindowState.Maximized
        Application.DoEvents()
    End Sub
    Private Sub SwitchScreenState()
        If Me.WindowState = FormWindowState.Maximized Then
            Normal()
        Else
            Maximize()
        End If
    End Sub

    Private Sub BtnMinimize_Click(sender As Object, e As EventArgs) Handles BtnMinimize.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnclosezamba_Click(sender As Object, e As EventArgs) Handles btnclosezamba.Click
        closeZamba()
    End Sub

    'Dim posX As Integer
    'Dim posY As Integer
    'Dim drag As Boolean

    'Private Sub ToolbarZamba_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ToolbarZamba.MouseDoubleClick
    '    If e.Button = MouseButtons.Left Then
    '        For Each btn As ToolStripItem In ToolbarZamba.Items
    '            If btn.Pressed Then
    '                Return
    '            End If
    '        Next
    '        If WindowState = FormWindowState.Maximized Then
    '            Normal()
    '        Else
    '            Maximize()
    '        End If
    '    End If
    'End Sub

    'Private Sub ToolbarZamba_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ToolbarZamba.MouseDown
    '    If e.Button = MouseButtons.Left Then
    '        drag = True
    '        posX = Cursor.Position.X - Left
    '        posY = Cursor.Position.Y - Top
    '    End If
    'End Sub

    'Private Sub ToolbarZamba_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ToolbarZamba.MouseUp
    '    drag = False
    'End Sub

    'Private Sub ToolbarZamba_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ToolbarZamba.MouseMove
    '    If drag Then
    '        Top = Cursor.Position.Y - posY
    '        Left = Cursor.Position.X - posX
    '    End If
    '    Cursor = Cursors.Default
    'End Sub

    Private Const SnapDist As Integer = 100

    Private Function DoSnap(ByVal pos As Integer, ByVal edge As Integer) As Boolean
        Dim delta As Integer = pos - edge
        Return delta > 0 AndAlso delta <= SnapDist
    End Function

    Protected Overrides Sub OnResizeEnd(ByVal e As EventArgs)
        MyBase.OnResizeEnd(e)
        Dim scn As Screen = Screen.FromPoint(Me.Location)
        If DoSnap(Me.Left, scn.WorkingArea.Left) Then Me.Left = scn.WorkingArea.Left
        If DoSnap(Me.Top, scn.WorkingArea.Top) Then Me.Top = scn.WorkingArea.Top
        If DoSnap(scn.WorkingArea.Right, Me.Right) Then Me.Left = scn.WorkingArea.Right - Me.Width
        If DoSnap(scn.WorkingArea.Bottom, Me.Bottom) Then Me.Top = scn.WorkingArea.Bottom - Me.Height
    End Sub


    Public Sub Add(component As IComponent) Implements IContainer.Add
    End Sub

    Public Sub Add(component As IComponent, name As String) Implements IContainer.Add
    End Sub

    Public Sub Remove(component As IComponent) Implements IContainer.Remove
    End Sub

    Public Property MustCloseZamba As Boolean

    Private ReadOnly Property IContainer_Components As ComponentCollection Implements IContainer.Components
        Get
            Return Nothing
        End Get
    End Property

    Dim WP As BackgroundWorker

    Private Sub SetupAutoComplete()
        ' AddHandler radAutoCompleteBox.Items.CollectionChanged, AddressOf OnItemsCollectionChanged
        radAutoCompleteBox.AutoCompleteDisplayMember = "Word"
        radAutoCompleteBox.AutoCompleteValueMember = "Word"
        'AddHandler radAutoCompleteBox.ListElement.VisualItemFormatting, AddressOf OnListElementVisualItemFormatting
        radAutoCompleteBox.DropDownMaxSize = New Size(200, 0)
        Controls.Add(radAutoCompleteBox)
        radAutoCompleteBox.Location = New Point(25, 25)
        radAutoCompleteBox.BringToFront()
        '        Me.FillList(radAutoCompleteBox.Items)
    End Sub
    'Private Sub OnListElementVisualItemFormatting(ByVal sender As Object, ByVal e As VisualItemFormattingEventArgs)
    '    Dim dataItem As RadListDataItem = e.VisualItem.Data
    '    e.VisualItem.Text = String.Format("{0}", dataItem.Text)
    'End Sub


    Private Sub radAutoCompleteBox_TextChanged(sender As Object, e As KeyPressEventArgs) Handles radAutoCompleteBox.KeyPress
        Try
            Dim caretIndex As Int32 = radAutoCompleteBox.TextBoxElement.CaretIndex
            Dim nonTokenizedBlock = radAutoCompleteBox.TextBoxElement.ViewElement.Children.OfType(Of TextBlockElement)().LastOrDefault(Function(x) caretIndex >= x.Offset AndAlso caretIndex <= x.Offset + x.Length)

            If (nonTokenizedBlock Is Nothing) Then
                Return
            End If

            Dim value As String = nonTokenizedBlock.Text + e.KeyChar.ToString()
            Dim ds As DataSet
            ' Dim ss As New GlobalSearch.SearchSuggestions()
            '  ds = ss.getData(value)
            ' radAutoCompleteBox.AutoCompleteDataSource.Clear()
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                radAutoCompleteBox.AutoCompleteDataSource = New BindingSource(ds.Tables(0), String.Empty)
            End If


        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub DoGlobalSearch(sender As Object, e As DoWorkEventArgs)
        Dim ds As DataSet
        '  Dim ss As New GlobalSearch.SearchSuggestions()
        '  ds = ss.getData(e.Argument.ToString())
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            e.Result = ds.Tables(0)
        End If
    End Sub

    Private Sub DoGlobalSearchCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        Try
            radAutoCompleteBox.AutoCompleteDataSource = New BindingSource(e.Result, String.Empty)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnStart_ButtonClick(sender As Object, e As EventArgs) Handles btnStart.ButtonClick
        Try
            ShowTab(TabPages.TabStart)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ActualizarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizarToolStripMenuItem.Click
        Try
            FrmView.SelectTab(TabPages.TabStart)
            FrmView.RefreshTab(TabPages.TabStart)
        Catch ex As Exception
            ZClass.raiseerror(ex)

        End Try
    End Sub


End Class