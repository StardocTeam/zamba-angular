Public Class ZReports

    Public Enum ReportType
        Exportados = 1
        Exportar = 2
        PublicosAExportar = 3
        UsuariosActivos = 4
        Permisos = 5
        Procesos = 6
        Documentos = 7
        InactiveUsers = 8
        DocumentosMasConsultados = 9
        DocumentosImpresos = 10
        DocumentosEliminados = 11
        DocumentosEnviados = 12
        DocumentosSinExtension = 13
        UsuariosConZamba = 14
        DocumentosConIndices = 15
        SoloMails = 16
        DocumentosSinMails = 17
        MailsPublicosPorUsuario = 18
        HistorialUsuario = 19
        HistorialDocumento = 20
        DocumentosPorFechas = 21
        DocumentosPorFechasSinMails = 22
        PermisosPorUsuarios = 23
        DocumentosImpresosPorFechas = 24
        LoginsFallidos = 25
        SacannedBarcodeByDate = 26
        SacannedBarcodeByBatche = 27
        AllBarcodes = 28
        UsuariosBloqueados = 29
        DocumentosEnVolumenes = 30
        Volumenes = 31
        HistorialDeAcciones = 32
        CaratulasIngresadas = 33
    End Enum
    Public Shared Function generarReporte(ByVal eRpt As ReportType) As FrmReport
        Dim FrmRpt As FrmReport
        Try
            FrmRpt = New FrmReport
            FrmRpt.Report = eRpt
            Return FrmRpt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function exportarReporte(ByVal Form As FrmReport, ByVal eTipo As CrystalDecisions.Shared.ExportFormatType, ByVal sRuta As String) As Boolean
        Try
            Dim Rpt As CrystalDecisions.CrystalReports.Engine.ReportClass
            Rpt = Form.CrystalReportViewer1.ReportSource
            Rpt.ExportToDisk(eTipo, sRuta)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
