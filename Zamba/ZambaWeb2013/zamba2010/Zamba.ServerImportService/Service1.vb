Imports System.ServiceProcess
Imports System.Windows.Forms
'Imports System.Drawing
Imports System.Diagnostics
Imports System
Public Class Service1
    Inherits System.ServiceProcess.ServiceBase

#Region " Código generado por el Diseñador de componentes "

    Public Sub New()
        MyBase.New()
        Try

            ' El Diseñador de componentes requiere esta llamada.
            InitializeComponent()

            ' Agregar cualquier inicialización después de la llamada a InitializeComponent()
            ServerImportClass.Methods.AddTrace(Application.StartupPath)

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

    'UserService reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' El punto de entrada principal para el proceso
    <MTAThread()> _
    Shared Sub Main()
        Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        ' Se puede ejecutar en el mismo proceso más de un servicio NT. Para agregar
        ' otro servicio a este proceso, cambie la siguiente línea a fin de
        ' crear otro objeto de servicio. Por ejemplo,
        '
        '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
        '
        ServicesToRun = New System.ServiceProcess.ServiceBase() {New Service1}

        System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    End Sub

    'Requerido por el Diseñador de componentes
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de componentes requiere el siguiente procedimiento
    'Se puede modificar utilizando el Diseñador de componentes. No lo modifique
    ' con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        Me.ServiceName = "Service1"
    End Sub

#End Region

    Dim Threading As String
    Dim periodo As Int64
    Dim NewId As Int32
    Public Shared remObj As Runtime.Remoting.WellKnownServiceTypeEntry

    'Private Sub LoadTrace()
    '    ServerImportClass.Methods.AddTrace(Application.StartupPath)
    '    ServerImportClass.Methods.InstanciarUsuario()
    'End Sub
    Private Sub LogsError(ByVal ex As Exception)
        Zamba.AppBlock.ZException.Log(ex)
    End Sub


    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Agregar código aquí para iniciar el servicio. Este método debería poner en movimiento
        ' los elementos para que el servicio pueda funcionar.
        Try
            'Me.LoadTrace()

            Dim puerto As Int32
            Dim Tipo As String = String.Empty

            ServerImportClass.Methods.WritePort()

            ServerImportClass.Methods.ReadPort(puerto.ToString, Tipo.ToString, Threading, periodo)

            ServerImportClass.Methods.AddToServers(Environment.MachineName, ServerImportClass.Methods.GetIp, puerto, "ZServEngine", "IZRemoting", Tipo, NewId)

            If Environment.GetCommandLineArgs.Length > 1 Then
                ServerImportClass.Methods.SetEngine()
            Else
                ServerImportClass.Methods.SetChannel(puerto.ToString, Tipo, Threading, periodo.ToString, remObj, Application.StartupPath)
            End If

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Application.Exit()
        End Try
    End Sub

    Protected Overrides Sub OnStop()
        ' Agregar código aquí para realizar cualquier anulación necesaria para detener el servicio.
        ServerImportClass.Methods.RemoveFromServers(NewId)
        Application.Exit()
    End Sub

End Class
