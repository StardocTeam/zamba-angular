'Imports ZAMBA.Servers
Imports Zamba.ReportsCore
Imports Zamba.Core
Public Class FrmReport
    Inherits Zamba.appblock.ZForm
    Public Report As Zamba.ReportsViewer.ZReports.ReportType
    'Private validarParam As New csValidarParametros
    'Public sRuta As String
    'Public FormatoExportacion As CrystalDecisions.Shared.ExportFormatType


#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
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
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MnuSendMail As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReport))
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MnuSendMail = New System.Windows.Forms.MenuItem
        Me.SuspendLayout()
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.AutoScroll = True
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(2, 2)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.ReportSource = Nothing
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(800, 553)
        Me.CrystalReportViewer1.TabIndex = 0
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MnuSendMail})
        '
        'MnuSendMail
        '
        Me.MnuSendMail.Index = 0
        Me.MnuSendMail.Text = "Enviar por mail"
        '
        'FrmReport
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(804, 557)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "FrmReport"
        Me.Text = "Zamba SoftWare - Informes "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

#End Region

    '#Region "Tipo Enumerado"
    'Public Enum ReportType
    '        Exportados = 1
    '        Exportar = 2
    '        PublicosAExportar = 3
    '        UsuariosActivos = 4
    '        Permisos = 5
    '        Procesos = 6
    '        Documentos = 7
    '        InactiveUsers = 8
    '        DocumentosMasConsultados = 9
    '        DocumentosImpresos = 10
    '        DocumentosEliminados = 11
    '        DocumentosEnviados = 12
    '        DocumentosSinExtension = 13
    '        UsuariosConZamba = 14
    '        DocumentosConIndices = 15
    '        SoloMails = 16
    '        DocumentosSinMails = 17
    '        MailsPublicosPorUsuario = 18
    '        HistorialUsuario = 19
    '        HistorialDocumento = 20
    '        DocumentosPorFechas = 21
    '        DocumentosPorFechasSinMails = 22
    '        PermisosPorUsuarios = 23
    '        DocumentosImpresosPorFechas = 24
    '        LoginsFallidos = 25
    '        SacannedBarcodeByDate = 26
    '        SacannedBarcodeByBatche = 27
    '        AllBarcodes = 28
    '        UsuariosBloqueados = 29
    '        DocumentosEnVolumenes = 30
    '        Volumenes = 31
    '        HistorialDeAcciones = 32
    '        CaratulasIngresadas = 33
    'End Enum
    '#End Region
#Region "Procedimientos Auxiliares"
    Private Shared Sub CreateFolder()
        Try
            Dim Folder As New IO.DirectoryInfo(Application.StartupPath & "\Informes")
            If Folder.Exists = False Then
                Folder.Create()
            End If
        Catch ex As Exception
            MessageBox.Show("Imposible crear el directorio para informes. Cree un directorio: " & Application.StartupPath & "\Informes", "Zamba Reportes", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    Private Shared Sub Conectar()
        Try
            If Not IO.File.Exists(".\app.ini") Then
                Dim dire As New IO.DirectoryInfo(Application.StartupPath)
                IO.File.Copy(dire.Parent.FullName & "\app.ini", Application.StartupPath & "\app.ini", True)
                'En desuso
                'If IO.File.Exists(dire.Parent.FullName & "\smtpConfig.xml") Then
                '    IO.File.Copy(dire.Parent.FullName & "\smtpConfig.xml", Application.StartupPath & "\smtpConfig.xml", True)
                'End If
            End If
            'Dim server As New server
            'server.MakeConnection()
            'server.dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
        End Try
    End Sub

#End Region

    Dim Fdesde, fhasta As Date
    Private Sub Fechas(ByVal desde As Date, ByVal hasta As Date)
        Me.Fdesde = desde
        Me.fhasta = hasta
    End Sub
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ok As Boolean = True

        '---Maxi---
        'If validarParam.ValidarParametros() <> 0 Then

        '    MessageBox.Show("Parametros Incorrectos", "Error", MessageBoxButtons.OK)
        '    Me.Close()

        'End If
        'Report = Environment.GetCommandLineArgs(1)
        'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
        '    sRuta = Environment.GetCommandLineArgs(2)
        '    TipoFormato = Environment.GetCommandLineArgs(3)
        '    'MessageBox.Show("REPORTE AUTOMATIZADO")
        'End If
        Try
            Conectar()
        Catch EX As Exception
            ok = False
            MessageBox.Show("ERROR en Conectar()", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MnuSendMail.Enabled = False
        End Try
        If ok Then CargarReporte()


    End Sub
    Private Sub CargarReporte()
        Try
            CreateFolder()
        Catch
        End Try
        Try
            Select Case Report
                Case Zamba.ReportsViewer.ZReports.ReportType.Exportar
                    Dim DsMails As New DsMails
                    Dim Dlg As New FolderBrowserDialog
                    Dlg.ShowNewFolderButton = False
                    Dim Result As DialogResult = Dlg.ShowDialog()
                    If Result = Windows.Forms.DialogResult.OK Then
                        Dim Path As String = Dlg.SelectedPath
                        Dim RptMails As New RptMails(Path)
                        DsMails = RptMails.MailsToImport
                        Dim Cr As New RptMailsToImport
                        Cr.SetDataSource(DirectCast(DsMails.DsMails, System.Data.DataTable))
                        Me.CrystalReportViewer1.ReportSource = Cr
                        'Cr.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "inf15112005.html")
                    End If

                Case Zamba.ReportsViewer.ZReports.ReportType.Exportados
                    'Dim Mails As New RptMails
                    Dim datos As DsMailsCount
                    datos = RptMails.MailsImportados
                    Dim rpt As New RptMailsImportados
                    rpt.SetDataSource(datos)
                    Me.CrystalReportViewer1.ReportSource = rpt
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    '    'MessageBox.Show("REPORTE EXPORTADO")
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.PublicosAExportar
                    Dim Mails As DsMails
                    Dim Dlg As New FolderBrowserDialog
                    Dlg.ShowNewFolderButton = False
                    Dim Result As DialogResult = Dlg.ShowDialog()
                    If Result = System.Windows.Forms.DialogResult.OK Then
                        Dim Path As String = Dlg.SelectedPath
                        Dim RptMails As New RptMails(Path)
                        Mails = RptMails.MailsPublicToImport
                        Dim Cr As New RptMailsPublicToImport
                        Cr.SetDataSource(DirectCast(Mails.DsMails, System.Data.DataTable))
                        Me.CrystalReportViewer1.ReportSource = Cr
                        ' Cr.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\MailsPublicosAExportar.pdf")
                    End If

                Case Zamba.ReportsViewer.ZReports.ReportType.UsuariosActivos
                    Dim dsactusers As New DsActiveUsers
                    'Dim au As New ActiveUsers
                    dsactusers = ActiveUsers.Get_OnLine_Users()
                    Dim Rpt As New RptOnlineUsers
                    Rpt.SetDataSource(dsactusers)
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.Permisos
                    Dim Permisos As New DsPermisos
                    'Dim UR As New UsersRights
                    Dim Rpt As New RPTPermisos
                    Permisos = UsersRights.VerPermisos
                    Rpt.SetDataSource(DirectCast(Permisos.DsPermisos, System.Data.DataTable))
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.PermisosPorUsuarios
                    Dim Permisos As New DsPermisos
                    'Dim Ur As New UsersRights
                    Dim Rpt As New RptPermisosPorUsuario
                    Permisos = UsersRights.PermisosPorUsuario
                    Rpt.SetDataSource(DirectCast(Permisos.DsPermisos, System.Data.DataTable))
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    '  Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\PermisosPorUsuarios.pdf")
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.Procesos
                    Dim Dlg As New FolderBrowserDialog
                    Dlg.ShowNewFolderButton = False
                    Dim Result As DialogResult = Dlg.ShowDialog()
                    If Result = System.Windows.Forms.DialogResult.OK Then
                        Dim Path As String = Dlg.SelectedPath
                        'Dim Procesos As New DsRptProcess
                        Dim process As New RptProceso(Path)
                        Dim rpt As New RptProcess
                        'Procesos = process.Datos
                        rpt.SetDataSource(process.Datos)
                        Me.CrystalReportViewer1.ReportSource = rpt
                        'Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\Procesos.pdf")
                    End If

                Case Zamba.ReportsViewer.ZReports.ReportType.Documentos
                    Dim Rpt As New RptDocumentos
                    Dim dsdocs As New DsDocumentsType
                    Dim datos As New DocCount
                    dsdocs = datos.Datos()
                    Rpt.SetDataSource(dsdocs)
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    '     Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\Documentos.pdf")
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.DocumentosSinMails
                    Dim Rpt As New RptDocumentos
                    Dim dsdocs As New DsDocumentsType
                    Dim datos As New DocCount
                    dsdocs = datos.DatosSinMails
                    Rpt.SetDataSource(dsdocs)
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    '    Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\DocumentosSinMails.pdf")
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.SoloMails
                    Dim rpt As New RptDocumentos
                    Dim dsdocs As New DsDocumentsType
                    Dim datos As New DocCount
                    dsdocs = datos.Datos(True)
                    rpt.SetDataSource(dsdocs)
                    Me.CrystalReportViewer1.ReportSource = rpt
                    '   Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\SoloMails.pdf")
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.DocumentosSinExtension
                    'sin extension. No se usa.
                    Dim Rpt As New RptDocumentos
                    Dim dsdocs As New DsDocumentsType
                    Dim datos As New DocCount
                    dsdocs = datos.DocSinExtension
                    Rpt.SetDataSource(dsdocs)
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    '  Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\DocSinExtension.pdf")
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.InactiveUsers
                    Dim Rpt As New RptInactive
                    Dim ds As New DsInactiveUsers
                    'Dim InUsers As New ClsEstadisticas
                    ds = ClsEstadisticas.InactiveUsers
                    Rpt.SetDataSource(ds)
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    '  Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\InactiveUsers.pdf")
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.DocumentosMasConsultados
                    Dim Rpt As New RptDocMasCons
                    Dim ds As New dsBestDocuments
                    'Dim Docs As New ClsEstadisticas
                    ds = ClsEstadisticas.DocumentosMasConsultados(25)
                    Rpt.SetDataSource(ds)
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    '  Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\DocumentosMasConsultados.pdf")
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.DocumentosImpresos
                    Dim Rpt As New RptImpresos
                    Dim ds As New DsUsersPrint
                    'Dim printdocs As New ClsEstadisticas
                    ds = ClsEstadisticas.UsersPrinting()
                    Rpt.SetDataSource(ds)
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    ' Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\DocumentosImpresos.pdf")
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.DocumentosImpresosPorFechas
                    Dim Rpt As New RptImpresos
                    Dim ds As New DsUsersPrint
                    'Dim printdocs As New ClsEstadisticas
                    Dim frm As New FrmDates
                    RemoveHandler frm.Fechas, AddressOf Me.Fechas
                    AddHandler frm.Fechas, AddressOf Me.Fechas
                    frm.ShowDialog()
                    ds = ClsEstadisticas.UsersPrinting(Me.Fdesde, fhasta)
                    Rpt.SetDataSource(ds)
                    Me.CrystalReportViewer1.ReportSource = Rpt

                Case Zamba.ReportsViewer.ZReports.ReportType.DocumentosEliminados
                    Dim Rpt As New RptDocDeleted
                    Dim ds As New DsDocDeleted
                    'Dim doc As New ClsEstadisticas
                    ds = ClsEstadisticas.DocumentosEliminados()
                    Rpt.SetDataSource(ds)
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    '   Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\DocumentosEliminados.pdf")
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.DocumentosEnviados
                    Dim Rpt As New RptDocSend
                    Dim ds As New DsDocDeleted
                    'Dim doc As New ClsEstadisticas
                    ds = ClsEstadisticas.DocEnviados
                    Rpt.SetDataSource(ds)
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    '   Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\DocumentosEnviados.pdf")
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.UsuariosConZamba
                    Dim rpt As New RptInstalled
                    Dim ds As New DsInstalled
                    'Dim doc As New ClsEstadisticas
                    ds = ClsEstadisticas.Instalaciones
                    rpt.SetDataSource(ds)
                    Me.CrystalReportViewer1.ReportSource = rpt
                    '  Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\PCsInstaladas.pdf")
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.DocumentosConIndices
                    Dim rpt As New RptDocIndex
                    'Dim doc As New Document
                    rpt.SetDataSource(Document.DocumentsIndexs)
                    Me.CrystalReportViewer1.ReportSource = rpt
                    '    Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\MailspublicosporUsuario.pdf")
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.MailsPublicosPorUsuario
                    Dim rpt As New RptMailsPub
                    Dim ds As New DsMailspublicos
                    ' Dim doc As New RptMails
                    ds = RptMails.MailsPublicosporUsuarios
                    rpt.SetDataSource(ds)
                    Me.CrystalReportViewer1.ReportSource = rpt
                    '  Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\MailspublicosporUsuario.pdf")
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.HistorialUsuario
                    Dim rpt As New RptUserHistory
                    'Dim history As New ClsHistory
                    Dim frm As New FrmUsers
                    RemoveHandler frm.Usuario, AddressOf usuario
                    AddHandler frm.Usuario, AddressOf usuario
                    frm.ShowDialog()
                    rpt.SetDataSource(ClsHistory.HistorialUsuario(Userid))
                    Me.CrystalReportViewer1.ReportSource = rpt
                    '   Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\HistorialUsuarios.pdf")

                Case Zamba.ReportsViewer.ZReports.ReportType.HistorialDocumento
                    Dim rpt As New RptDocHistory
                    'Dim history As New ClsHistory
                    Dim frm As New FrmDocHistory
                    RemoveHandler frm.DocNro, AddressOf Me.DocHistoryNro
                    AddHandler frm.DocNro, AddressOf Me.DocHistoryNro
                    frm.ShowDialog()
                    rpt.SetDataSource(ClsHistory.GetDocumentActions(DocNro))
                    Me.CrystalReportViewer1.ReportSource = rpt
                    '  Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\HistorialDocumento.pdf")

                Case Zamba.ReportsViewer.ZReports.ReportType.DocumentosPorFechas
                    Dim frm As New FrmDates
                    RemoveHandler frm.Fechas, AddressOf Fechas
                    AddHandler frm.Fechas, AddressOf Fechas
                    frm.ShowDialog()
                    Dim Rpt As New RptDocByDate
                    Dim ds As New DsDocumentsByDate
                    Dim doc As New DocCount
                    ds = doc.DocumentosPorFechas(Me.Fdesde, fhasta)
                    Rpt.SetDataSource(ds)
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    '  Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\DocumentosPorFechas.pdf")

                Case Zamba.ReportsViewer.ZReports.ReportType.DocumentosPorFechasSinMails
                    Dim frm As New FrmDates
                    RemoveHandler frm.Fechas, AddressOf Fechas
                    AddHandler frm.Fechas, AddressOf Fechas
                    frm.ShowDialog()
                    Dim Rpt As New RptDocByDate
                    Dim ds As New DsDocumentsByDate
                    Dim doc As New DocCount
                    ds = doc.DocumentosPorFechasSinMails(Me.Fdesde, fhasta)
                    Rpt.SetDataSource(ds)
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    '     Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\DocumentosPorFechasSinMails.pdf")

                Case Zamba.ReportsViewer.ZReports.ReportType.LoginsFallidos
                    Dim Rpt As New rptLogins
                    'Dim datos As New ClsEstadisticas
                    Dim Ds As DataSet
                    Ds = ClsEstadisticas.LoginFailed
                    If Not IsNothing(Ds) Then
                        Rpt.SetDataSource(Ds)
                        Me.CrystalReportViewer1.ReportSource = Rpt
                        '  Rpt.ExportToDisk(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat, "C:\Informes\IntentosFallidosdeLogin.pdf")
                        'Exporta a disco si corresponde
                        'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                        '    Rpt.ExportToDisk(TipoFormato, sRuta)
                        'End If
                    Else
                    End If


                Case Zamba.ReportsViewer.ZReports.ReportType.SacannedBarcodeByDate
                    Dim Rpt As New RptCaratulas
                    Dim datos As New ClsHistory
                    Rpt.SetDataSource(datos.ScannedBarcodeByDate)
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.SacannedBarcodeByBatche
                    Dim Rpt As New RptCaratulas
                    Dim datos As New ClsHistory
                    Rpt.SetDataSource(datos.ScannedBarcodeByBatch)
                    Me.CrystalReportViewer1.ReportSource = Rpt
                    'Exporta a disco si corresponde
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.AllBarcodes
                    frmAllBarcode = New frmAllBarcode
                    frmAllBarcode.StartPosition = FormStartPosition.CenterScreen
                    frmAllBarcode.ShowDialog()
                    Dim Rpt As New RptCaratulas
                    'Dim datos As New ClsHistory
                    'NoDigitalizadas + Digitalizadas 
                    If opt = 1 Then
                        Rpt.SetDataSource(ClsHistory.BarcodeAll(opt, User, fechaDesdeCreate, fechaHastaCreate, fechaDesdeScanned, fechaHastaScanned))
                        Me.CrystalReportViewer1.ReportSource = Rpt
                        'NoDigitalizadas
                    ElseIf opt = 2 Then
                        Rpt.SetDataSource(ClsHistory.BarcodeNoDigitalizados(opt, User, fechaDesdeCreate, fechaHastaCreate))
                        Me.CrystalReportViewer1.ReportSource = Rpt
                        'Digitalizadas 
                    ElseIf opt = 3 Then
                        Rpt.SetDataSource(ClsHistory.BarcodeDigitalizados(opt, User, fechaDesdeCreate, fechaHastaCreate, fechaDesdeScanned, fechaHastaScanned))
                        Me.CrystalReportViewer1.ReportSource = Rpt
                    End If

                Case Zamba.ReportsViewer.ZReports.ReportType.UsuariosBloqueados
                    Dim rpt As New rptLockUser
                    'Dim users As New ActiveUsers
                    rpt.SetDataSource(ActiveUsers.LockedUser)
                    Me.CrystalReportViewer1.ReportSource = rpt
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.DocumentosEnVolumenes
                    'Dim docs As New ClsEstadisticas
                    Dim frm As New FrmDates
                    RemoveHandler frm.Fechas, AddressOf Fechas
                    AddHandler frm.Fechas, AddressOf Fechas
                    frm.ShowDialog()
                    Me.Cursor = Cursors.WaitCursor
                    Dim ds As DataSet = ClsEstadisticas.VolDocs(Me.Fdesde, Me.fhasta)
                    Dim rpt As New rptDocDiarios
                    rpt.SetDataSource(ds)
                    Me.CrystalReportViewer1.ReportSource = rpt
                    Me.Cursor = Cursors.Default
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If

                Case Zamba.ReportsViewer.ZReports.ReportType.Volumenes
                    'Dim vols As New Volumenes
                    Me.Cursor = Cursors.WaitCursor
                    Dim rpt As New rptvolumenes
                    rpt.SetDataSource(Volumenes.GetVolumenes)
                    Me.CrystalReportViewer1.ReportSource = rpt
                    Me.Cursor = Cursors.Default
                    ' vols.Dispose()
                    'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
                    '    Rpt.ExportToDisk(TipoFormato, sRuta)
                    'End If
                Case Zamba.ReportsViewer.ZReports.ReportType.HistorialDeAcciones
                    Try
                        Dim Hist As New HistorialDeAcciones
                        Me.Cursor = Cursors.WaitCursor
                        Dim rpt As New rptHistorialAcciones
                        Dim ds As DsHistorialAcciones
                        ds = Hist.CargarDataSet()
                        rpt.SetDataSource(ds)
                        Me.CrystalReportViewer1.ReportSource = rpt
                        Me.Cursor = Cursors.Default
                        Hist.Dispose()
                    Catch ex As Exception
                        Zamba.AppBlock.ZException.Log(ex, False)
                        MessageBox.Show("ERROR CARGANDO CRPTS Historial De Acciones:  " & ex.ToString)
                    End Try

                Case Zamba.ReportsViewer.ZReports.ReportType.CaratulasIngresadas
                    Dim Cs As New csCaratulasIngresadas
                    Dim DsT As dsCaratulasIngresadas
                    DsT = Cs.cargarDataSet
                    If Cs.UsrId > 0 Then
                        Dim rpt As New rptCaratulasIngresadasPorUsuario
                        rpt.SetDataSource(DsT)
                        Me.CrystalReportViewer1.ReportSource = rpt
                    Else
                        Dim rpt As New rptCaratulasIngresadas
                        rpt.SetDataSource(DsT)
                        Me.CrystalReportViewer1.ReportSource = rpt
                    End If

            End Select
            'Finaliza el programa si fue lanzado por el ZScheduler
            'If validarParam.iMaxParametros = Environment.GetCommandLineArgs.Length Then
            '    'MessageBox.Show("ZREPORTS CERRADO")
            '    'CrystalReportViewer1.PrintReport()
            '    Me.Close()
            'End If
        Catch exc As CrystalDecisions.CrystalReports.Engine.EngineException
            Zamba.AppBlock.ZException.Log(exc)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

#Region "AllBarcode"
    Dim WithEvents frmAllBarcode As frmAllBarcode
    Dim opt As Int16
    Dim User As Int32
    Dim fechaDesdeCreate As Date
    Dim fechaHastaCreate As Date
    Dim fechaDesdeScanned As Date
    Dim fechaHastaScanned As Date
    Private Sub frmAllBarcode_Opt(ByVal opt As Int16, ByVal UserId As Int32, ByVal fechaDesdeCreate As Date, ByVal fechaHastaCreate As Date, ByVal fechaDesdeScanned As Date, ByVal fechaHastaScanned As Date) Handles frmAllBarcode.Opt
        Try
            MyClass.opt = opt
            MyClass.User = UserId
            MyClass.fechaDesdeCreate = fechaDesdeCreate
            MyClass.fechaHastaCreate = fechaHastaCreate
            MyClass.fechaDesdeScanned = fechaDesdeScanned
            MyClass.fechaHastaScanned = fechaHastaScanned
        Catch ex As Exception
        End Try
    End Sub
#End Region

    Dim Userid As Int32
    Dim DocNro As Int32
    Private Sub DocHistoryNro(ByVal id As Int64)
        DocNro = id
    End Sub
    Private Sub usuario(ByVal usr As Int32)
        Userid = usr
    End Sub
#Region "Envio de Mail"
    Private Sub MnuSendMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuSendMail.Click
        Try
            Dim mail As String = InputBox("Ingrese la dirección de correo: ", "Zamba Software - Mail")
            If mail = Nothing OrElse mail = "" Then Exit Sub
            SendReportByMail(Report, mail)
        Catch
            MessageBox.Show("No se puede enviar el informe por mail. Asegurese que tiene instalado el Cliente SMTP", "Zamba Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Shared Sub SendReportByMail(ByVal Report As Zamba.ReportsViewer.ZReports.ReportType, ByVal address As String)
        Dim attachs As New ArrayList
        Dim File As IO.FileInfo
        Select Case Report
            Case Zamba.ReportsViewer.ZReports.ReportType.Documentos
                File = New IO.FileInfo(Application.StartupPath & "\Informes\Documentos.pdf")
            Case Zamba.ReportsViewer.ZReports.ReportType.Exportados
                File = New IO.FileInfo(Application.StartupPath & "\Informes\Exportados.pdf")
            Case Zamba.ReportsViewer.ZReports.ReportType.Exportar
                File = New IO.FileInfo(Application.StartupPath & "\Informes\MailsaExportar.pdf")
            Case Zamba.ReportsViewer.ZReports.ReportType.Permisos
                File = New IO.FileInfo(Application.StartupPath & "\Informes\Permisos.pdf")
            Case Zamba.ReportsViewer.ZReports.ReportType.Procesos
                File = New IO.FileInfo(Application.StartupPath & "\Informes\Procesos")
            Case Zamba.ReportsViewer.ZReports.ReportType.PublicosAExportar
                File = New IO.FileInfo(Application.StartupPath & "\Informes\PublicosaExportar.pdf")
            Case Zamba.ReportsViewer.ZReports.ReportType.UsuariosActivos
                File = New IO.FileInfo(Application.StartupPath & "\Informes\OnlineUsers.pdf")
        End Select
        If Not IsNothing(File) AndAlso File.Exists = True Then
            attachs.Add(File.FullName)
            Try
                Zamba.Tools.ZSMTP.SendMail(address, "Informe Zamba Software", "Informe de Zamba, Fecha: " & Now.ToString, attachs)
            Catch
            End Try
        End If
    End Sub
#End Region

End Class
