Public Class PrintingComponents
    Public Shared Sub PrintConfig()
        Try
            Dim PrintDialog1 As New PrintDialog
            PrintDialog1.UseEXDialog = True
            PrintDialog1.PrinterSettings = ZPrinting.PrintConfig
            PrintDialog1.ShowDialog()
            ZPrinting.PrintConfig = PrintDialog1.PrinterSettings
            PrintDialog1.Dispose()
            PrintDialog1 = Nothing
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Sub PageSetup()
        Dim ps As New PageSetupDialog
        Try
            ps.PrinterSettings = ZPrinting.PrintConfig
            ps.PageSettings = ZPrinting.PrintConfig.DefaultPageSettings
            ps.PageSettings.Margins = System.Drawing.Printing.PrinterUnitConvert.Convert(ps.PageSettings.Margins, System.Drawing.Printing.PrinterUnit.ThousandthsOfAnInch, System.Drawing.Printing.PrinterUnit.HundredthsOfAMillimeter)
            If ps.ShowDialog() = DialogResult.OK Then
                ZPrinting.SavePrintConfig(ps.PrinterSettings)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
