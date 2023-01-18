Imports System.Threading
Imports System.Net
Imports Zamba.Imports
Imports System.Runtime.InteropServices
Imports ZAMBA.Servers
Imports ZAMBA.Core
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
'Imports System.Runtime.Remoting.Channels.Tcp
Imports Zamba.AppBlock

'Imports Zamba.Data
Public Class ZServImp
    Inherits ZForm

    Private Cicon As System.Windows.Forms.NotifyIcon

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
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    '  Friend WithEvents mnuPendientes As System.Windows.Forms.MenuItem
    '  Friend WithEvents MnuImportados As System.Windows.Forms.MenuItem
    Friend WithEvents Icons As System.Windows.Forms.ImageList
    '  Friend WithEvents mnuerrores As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZServImp))
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.Icons = New System.Windows.Forms.ImageList(Me.components)
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.MenuItem6 = New System.Windows.Forms.MenuItem
        Me.MenuItem7 = New System.Windows.Forms.MenuItem
        Me.SuspendLayout()
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem3, Me.MenuItem4, Me.MenuItem5, Me.MenuItem6, Me.MenuItem7})
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = -1
        Me.MenuItem2.Text = "-"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = -1
        Me.MenuItem1.Text = "Salir"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.White
        Me.LinkLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LinkLabel1.Location = New System.Drawing.Point(0, 0)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(344, 60)
        Me.LinkLabel1.TabIndex = 0
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Iniciando Servicio"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Icons
        '
        Me.Icons.ImageStream = CType(resources.GetObject("Icons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Icons.TransparentColor = System.Drawing.Color.Transparent
        Me.Icons.Images.SetKeyName(0, "")
        Me.Icons.Images.SetKeyName(1, "")
        Me.Icons.Images.SetKeyName(2, "")
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 0
        Me.MenuItem3.Text = "Estado"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 1
        Me.MenuItem4.Text = "Detener Servicio"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 2
        Me.MenuItem5.Text = "Importar los Pendientes"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 3
        Me.MenuItem6.Text = "Quitar todos los servidores"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 4
        Me.MenuItem7.Text = "Ver Servidores Activos"
        '
        'ZServImp
        '
        Me.ClientSize = New System.Drawing.Size(344, 58)
        Me.Controls.Add(Me.LinkLabel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ZServImp"
        Me.Opacity = 0.7
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zamba Software Servicio Importacion de Mails"
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "New"
    Public Sub New()
        MyBase.New()
        Try
            InitializeComponent()

            ServerImportClass.methods.addtrace(Application.StartupPath)

            '            RemoveHandler Zamba.AppBlock.ZForm.LogError, AddressOf LogEx
            '   RemoveHandler Zamba.Core.ZClass.LogError, AddressOf LogEx
            '           AddHandler Zamba.AppBlock.ZForm.LogError, AddressOf LogEx
            '   AddHandler Zamba.Core.ZClass.LogError, AddressOf LogEx
            ServerImportClass.Methods.InstanciarUsuario()
        Catch ex As ArgumentNullException
        Catch ex As ArgumentOutOfRangeException
        Catch ex As ArgumentException
        Catch ex As Exception
        End Try
    End Sub
#End Region

    'Private Shared Sub InstanciarUsuario()
    '    Trace.WriteLine("Obteniendo los permisos de usuario")
    '    Zamba.Core.RightComponent.ValidateLogIn(UserPreferences.UserId)
    '    Trace.WriteLine("Los permisos se obtuvieron correctamente")
    'End Sub

#Region "LOG"
    Private Sub LogEx(ByVal ex As Exception)
        Zamba.AppBlock.ZException.Log(ex)
    End Sub
#End Region

#Region "Systry"
    Dim importados, erroneos, Pendientes As Int64
    Dim TCB As New Threading.TimerCallback(AddressOf HideForm)
    Dim Timer1 As Threading.Timer
    Dim state As Object

    Delegate Sub DHideForm(ByVal state As Object)

    Private Sub HideForm(ByVal State As Object)
        Dim D1 As New DHideForm(AddressOf HidesForm)
        Me.Invoke(D1, New Object() {State})
    End Sub
    Private Sub HidesForm(ByVal state As Object)
        Try
            Me.Visible = False
            Timer1.Change(10000, 10000)
            Timer1.Dispose()
            Timer1 = Nothing
        Catch ex As NullReferenceException
        Catch ex As ThreadAbortException
        Catch ex As ThreadInterruptedException
        Catch ex As ThreadStateException
        Catch ex As OutOfMemoryException
        End Try
    End Sub
    'Private Sub UpdateStatus(ByVal Pendientes As Int64, ByVal Importados As Int64, ByVal Erroneos As Int64, ByVal Obs As Hashtable)
    '    Try
    '        Me.Pendientes = Pendientes
    '        Me.importados = Importados
    '        Me.erroneos += Erroneos
    '        Dim Usuario As String = Obs("USUARIO")
    '        Dim PC As String = Obs("PC")
    '        'todo: mostrar info en panel de control

    '    Catch ex As Exception
    '        Trace.WriteLine("Error en UpdateStatus: " & ex.ToString)
    '    End Try
    'End Sub

    Dim cb As New Threading.TimerCallback(AddressOf CheckErrors)
    Private Sub CheckErrors(ByVal state As Object)
        Try
            Dim sql As String = "Select * from ZExportErrors"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Dim i As Int32 = 0
            Dim ZServEngine As New ServerImportClass.ZServEngine()
            For i = 0 To ds.Tables(0).Rows.Count - 1
                If Not IsDBNull(ds.Tables(0).Rows(i).Item(3)) Then
                    ZServEngine.Run("INSERTARMAIL", ds.Tables(0).Rows(i).Item(3).ToString(), Nothing)
                End If
            Next
            ds.Dispose()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Dim cbN As New Threading.TimerCallback(AddressOf CheckN)
    Private Sub CheckN(ByVal state As Object)
        Try
            Dim sql As String = "Select line from ZExportcontrol where insertado = 'N'"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Dim i As Int32 = 0
            Dim ZServEngine As New ServerImportClass.ZServEngine()
            For i = 0 To ds.Tables(0).Rows.Count - 1
                If Not IsDBNull(ds.Tables(0).Rows(i).Item(0)) Then
                    ZServEngine.Run("INSERTARMAIL", ds.Tables(0).Rows(i).Item(0).ToString(), Nothing)
                End If
            Next
            ds.Dispose()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Dim Threading As String
    Dim periodo As Int64

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cicon = New NotifyIcon
        Me.Cicon.Icon = Me.Icon
        Me.Cicon.Text = "Zamba Software - Servicio de Importacion de Mails"
        RemoveHandler Cicon.DoubleClick, AddressOf NotifyIconOnClick
        AddHandler Cicon.DoubleClick, AddressOf NotifyIconOnClick
        'RemoveHandler ZServEngine.Status, AddressOf UpdateStatus
        'AddHandler ZServEngine.Status, AddressOf UpdateStatus
        ' Me.MnuImportados.Text = "Importados: " & Me.importados
        ' Me.mnuPendientes.Text = "Pendientes: " & Me.Pendientes
        ' Me.mnuerrores.Text = "Errores: " & Me.erroneos
        Me.Cicon.ContextMenu = Me.ContextMenu1
        Me.Cicon.Visible = True
        Timer1 = New Threading.Timer(TCB, state, 2000, 2000)

        Dim puerto As Int32
        Dim Tipo As String

        ServerImportClass.Methods.WritePort()

        ServerImportClass.Methods.ReadPort(puerto.ToString(), Tipo, Threading, periodo)

        ServerImportClass.Methods.AddToServers(Environment.MachineName, ServerImportClass.Methods.GetIp, puerto, "ZServEngine", "IZRemoting", Tipo, NewId)

        If Environment.GetCommandLineArgs.Length > 1 Then
            ServerImportClass.Methods.SetEngine()
            'Trace.WriteLine("Ingreso con mas de un parametro")
            'Dim ZServEngine As New ZServEngine
            'ZServEngine.Run1() 'SI RECIBE UN PARAMETRO QUE INGRESE LA LINEA
        Else
            ServerImportClass.Methods.SetChannel(puerto.ToString(), Tipo, Threading, periodo.ToString(), remObj, Application.StartupPath)
            'Trace.WriteLine("Ingreso con 0 parametro")

            'If Tipo.ToString.ToUpper = "TCP" Then
            '    Dim channel As New TcpServerChannel(puerto)
            '    Try
            '        ChannelServices.RegisterChannel(channel)
            '    Catch
            '    End Try
            'End If
            'If Tipo.ToString.ToUpper = "HTTP" Then
            '    Dim channel As New Http.HttpServerChannel(puerto)
            '    Try
            '        ChannelServices.RegisterChannel(channel)
            '    Catch
            '    End Try
            'End If
            'If Me.Threading = "Y" AndAlso Me.periodo > 0 Then
            '    Dim ZSrv As New ZEngineService(Me.periodo)
            'Else
            '    remObj = New WellKnownServiceTypeEntry(GetType(ZServEngine), "ZServEngine", WellKnownObjectMode.Singleton)
            '    RemotingConfiguration.RegisterWellKnownServiceType(remObj)
            '    Trace.WriteLine("Servidor Registrado en: TCP, Puerto " & puerto)
            '    Trace.WriteLine(remObj.ToString)
            'End If
        End If
        Try
            Dim t As New Threading.Timer(cb, Nothing, 0, Integer.Parse(DateDiff(DateInterval.Second, Now, Date.Parse(Now.Day & "/" & Now.Month & "/" & Now.Year & " 05:59:00 PM")) * 1000))
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Try
            Dim t1 As New Threading.Timer(cbN, Nothing, 600000, 600000)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Dim NewId As Int32

    'Private Shared Function GetIp() As String
    '    Try
    '        Trace.WriteLine("IP:")
    '        Dim Host As String = Dns.GetHostName
    '        Dim IPs As IPHostEntry = Dns.GetHostEntry(Host)
    '        Dim Direcciones As IPAddress() = IPs.AddressList
    '        Trace.WriteLine(Direcciones(0).ToString())
    '        Return Direcciones(0).ToString()
    '    Catch ex As Exception
    '        Return "0.0.0.0"
    '    End Try
    'End Function


    'Private Sub AddToServers(ByVal NombreServidor As String, ByVal IP As String, ByVal Puerto As Int32, ByVal Objeto As String, ByVal Interfaz As String, ByVal Descripcion As String)
    '    Try
    '        'todo que se fije si ya existe y lo actualice
    '        NewId = Zamba.Data.CoreData.GetNewID(ZClass.IdTypes.ZServidores)
    '        Dim sql As String = "Insert into ZServidores (Id,NombreServidor,IP,Puerto,Objeto,Interfaz,Descripcion)values(" & NewId & ",'" & NombreServidor & "','" & IP & "','" & Puerto & "',' Objeto ','" & Interfaz & "','" & Descripcion & "')"
    '        Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub
    'Private Sub RemoveFromServers()
    '    Try
    '        Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, "Delete ZServidores Where Id =" & NewId)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub
    Private Sub RemoveAllServers()
        Try
            Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, "Delete ZServidores")
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub NotifyIconOnClick(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
#End Region

#Region "Salir"
    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        Try
            Me.Close()
        Catch
        End Try
    End Sub
#End Region

    Public Shared remObj As WellKnownServiceTypeEntry

    Private Sub ZServImp_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        ServerImportClass.Methods.RemoveFromServers(NewId)
    End Sub

    Private Sub MenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem6.Click
        Try
            Me.RemoveAllServers()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
        Try
            Me.Close()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem5.Click
        Try
            CheckN(Nothing)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub MenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem7.Click
        Try
            ShowAllServers()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ShowAllServers()
        Try
            Dim strselect As String
            strselect = "Select nombreservidor,puerto from ZServidores"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
            For Each r As DataRow In ds.Tables(0).Rows
                Console.WriteLine("PC: " & r.Item(0) & " Puerto: " & r.Item(1))
            Next
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

End Class
