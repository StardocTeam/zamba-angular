Imports Zamba.AppBlock
Public Class Form1
    Inherits ZForm

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
    Friend WithEvents Button1 As Zamba.AppBlock.ZButton
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.Button1 = New Zamba.AppBlock.ZButton
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.None
        Me.Button1.Location = New System.Drawing.Point(72, 80)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(136, 24)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Aceptar"
        '
        'ComboBox1
        '
        Me.ComboBox1.Items.AddRange(New Object() {"Todos los Documentos", "Documentos Sin Mails", "Solo Mails", "Usuarios Conectados", "Usuarios Inactivos", "Documentos Eliminados", "Documentos Mas Consultados", "Documentos Impresos ", "Documentos Eliminados ", "Usuarios Con Zamba", "Documentos En Workflows", "Documentos con Indices", "Historial de Usuario", "Historial de Documento", "Incremento Mensual de Documentos", "Incremento Mensual de Documentos sin Mails", "Permisos por Usuario", "Volumenes", "Historial de Acciones", "Exportados", "Caratulas Ingresadas"})
        Me.ComboBox1.Location = New System.Drawing.Point(8, 48)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(304, 21)
        Me.ComboBox1.TabIndex = 1
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(8, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(96, 32)
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(328, 118)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Button1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "Módulo de Reportes - Zamba Software"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SelectReport()
    End Sub
    Private Sub SelectReport()
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case ComboBox1.Text.Trim
                Case "Todos los Documentos"
                    'Shell(".\zreports.exe 7", AppWinStyle.NormalFocus, False)
                    '                    Shell(Application.StartupPath & "\zreports.exe 7", AppWinStyle.MaximizedFocus, True)
                    'Dim FrmRpt As Zamba.Reports.FrmReport
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.Documentos)
                    FrmRpt.Show()
                Case "Usuarios Conectados"
                    'Shell(".\zreports.exe 4", AppWinStyle.NormalFocus, False)
                    '                   Shell(Application.StartupPath & "\zreports.exe 4", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.UsuariosActivos)
                    FrmRpt.Show()
                Case "Solo Mails"
                    'Shell(".\zreports.exe 16", AppWinStyle.NormalFocus, False)
                    '                    Shell(Application.StartupPath & "\zreports.exe 16", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.SoloMails)
                    FrmRpt.Show()
                Case "Usuarios Inactivos"
                    'Shell(".\zreports.exe 8", AppWinStyle.NormalFocus, False)
                    '               Shell(Application.StartupPath & "\zreports.exe 8", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.InactiveUsers)
                    FrmRpt.Show()
                Case "Documentos Eliminados"
                    'Shell(".\zreports.exe 11", AppWinStyle.NormalFocus, False)
                    ' Shell(Application.StartupPath & "\zreports.exe 11", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.DocumentosEliminados)
                    FrmRpt.Show()
                Case "Documentos Mas Consultados"
                    'Shell(".\zreports.exe 9", AppWinStyle.NormalFocus, False)
                    '      Shell(Application.StartupPath & "\zreports.exe 9", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.DocumentosMasConsultados)
                    FrmRpt.Show()
                Case "Documentos Impresos"
                    'Shell(".\zreports.exe 10", AppWinStyle.NormalFocus, False)
                    '      Shell(Application.StartupPath & "\zreports.exe 10", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.DocumentosImpresos)
                    FrmRpt.Show()
                Case "Usuarios Con Zamba"
                    'Shell(".\zreports.exe 14", AppWinStyle.NormalFocus, False)
                    '     Shell(Application.StartupPath & "\zreports.exe 14", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.UsuariosConZamba)
                    FrmRpt.Show()
                Case "Documentos Sin Mails"
                    '    Shell(Application.StartupPath & "\zreports.exe 17", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.DocumentosSinMails)
                    FrmRpt.Show()

                Case "Documentos con Indices"
                    '     Shell(Application.StartupPath & "\zreports.exe 15", AppWinStyle.MaximizedFocus, True)

                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.DocumentosConIndices)
                    FrmRpt.Show()
                Case "Historial de Usuario"
                    '     Shell(Application.StartupPath & "\zreports.exe 19", AppWinStyle.MaximizedFocus, True)

                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.HistorialUsuario)
                    FrmRpt.Show()
                Case "Historial de Documento"
                    '      Shell(Application.StartupPath & "\zreports.exe 20", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.HistorialDocumento)
                    FrmRpt.Show()

                Case "Incremento Mensual de Documentos"
                    '     Shell(Application.StartupPath & "\zreports.exe 21", AppWinStyle.MaximizedFocus, True)

                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.DocumentosPorFechas)
                    FrmRpt.Show()
                Case "Incremento Mensual de Documentos sin Mails"
                    '      Shell(Application.StartupPath & "\zreports.exe 22", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.DocumentosPorFechasSinMails)
                    FrmRpt.Show()
                Case "Permisos por Usuario"
                    '    Shell(Application.StartupPath & "\zreports.exe 23", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.PermisosPorUsuarios)
                    FrmRpt.Show()

                Case "Volumenes"
                    '         Shell(Application.StartupPath & "\zreports.exe 31", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.Volumenes)
                    FrmRpt.Show()
                Case "Documentos En Workflows"
                    MessageBox.Show("Usted no cuenta con el módulo de Workflow", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Case "Historial de Acciones"
                    '      Shell(Application.StartupPath & "\zreports.exe 32", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.HistorialDeAcciones)
                    FrmRpt.Show()

                Case "Exportados"
                    '     Shell(Application.StartupPath & "\zreports.exe 2", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.Exportar) '.Exportados)
                    FrmRpt.Show()

                Case "Caratulas Ingresadas"
                    '        Shell(Application.StartupPath & "\zreports.exe 33", AppWinStyle.MaximizedFocus, True)
                    Dim FrmRpt As Zamba.ReportsViewer.FrmReport
                    FrmRpt = Zamba.ReportsViewer.ZReports.generarReporte(Zamba.ReportsViewer.ZReports.ReportType.CaratulasIngresadas)
                    FrmRpt.Show()
            End Select
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
End Class
