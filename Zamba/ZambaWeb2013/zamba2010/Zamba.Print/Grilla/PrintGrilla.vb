'Imports Microsoft.Office.Interop
Public Class PrintGrilla
    ''' <summary>
    '''  Imprime con formato de excel el contenido de un datasource.
    ''' </summary>
    ''' <param name="pDataSet">dataSet con los datos</param>
    ''' <param name="fileName">el nombre que va a tener el archivo temporal</param>
    ''' <remarks></remarks>
    Public Shared Sub ImprimirDirectlyToExcel(ByVal pDataTable As DataTable, ByVal fileName As String)
        Dim sArchivo As String
        Try
            Dim PD As New PrintDialog
            Dim Doc As New Printing.PrintDocument
            sArchivo = System.Environment.CurrentDirectory & "\" & fileName & ".xls"
            PD.Document = Doc
            If PD.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Zamba.Office.ExcelInterop.DataTableDirectlyToExcel(pDataTable, sArchivo, Zamba.Office.ExcelInterop.eEstilosHoja.Reporte2, False)
                Zamba.Office.ExcelInterop.imprimirPlanilla(sArchivo, PD.PrinterSettings)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo imprimir en Excel, excepción: " & ex.ToString.Substring(20), "Zamba - Exportar Grilla a Excel®")
            Try
                Zamba.Office.ExcelInterop.ProcesoExcel.Kill()
            Catch
            End Try
            Exit Sub
        End Try
        'Borro el archivo temporal
        Try
            Dim Arch As New System.IO.FileInfo(sArchivo)
            Arch.Delete()
        Catch sex As Exception
        End Try
    End Sub
    ''' <summary>
    '''  Imprime con formato de excel el contenido de un datasource.
    ''' </summary>
    ''' <param name="pDataSet">dataSet con los datos</param>
    ''' <remarks></remarks>
    Public Shared Sub ImprimirAExcel(ByVal pDataTable As DataTable)
        'Dim sArchivo As String
        Try
            Dim PD As New PrintDialog
            Dim Doc As New Printing.PrintDocument
            PD.Document = Doc
            If PD.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Zamba.Office.ExcelInterop.DataTableDirectlyToPrint(pDataTable, Zamba.Office.ExcelInterop.eEstilosHoja.Reporte2, PD.PrinterSettings)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo imprimir en Excel, excepción: " & ex.ToString.Substring(20), "Zamba - Exportar Grilla a Excel®")
            Try
                Zamba.Office.ExcelInterop.ProcesoExcel.Kill()
            Catch
            End Try
            Exit Sub
        End Try
    End Sub
    ''' <summary>
    '''  Imprime con formato de excel el contenido de un datasource.
    ''' </summary>
    ''' <param name="pDataTable">los datos de la tabla</param>
    ''' <param name="fileName">el nombre que va a tener el archivo temporal</param>
    ''' <remarks></remarks>
    Public Shared Sub Imprimir(ByVal pDataTable As DataTable, ByVal fileName As String)
        Dim sArchivo As String
        Try
            Dim PD As New PrintDialog
            Dim Doc As New Printing.PrintDocument
            sArchivo = System.Environment.CurrentDirectory & "\" & fileName & ".xls"
            PD.Document = Doc
            If PD.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Zamba.Office.ExcelInterop.DataTableToExcel(pDataTable, sArchivo, Zamba.Office.ExcelInterop.eEstilosHoja.Reporte2)
                Zamba.Office.ExcelInterop.imprimirPlanilla(sArchivo, PD.PrinterSettings)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo imprimir en Excel, excepción: " & ex.ToString.Substring(20), "Zamba - Exportar Grilla a Excel®")
            Try
                Zamba.Office.ExcelInterop.ProcesoExcel.Kill()
            Catch
            End Try
            Exit Sub
        End Try
        'Borro el archivo temporal
        Try
            Dim Arch As New System.IO.FileInfo(sArchivo)
            Arch.Delete()
        Catch sex As Exception
        End Try
    End Sub

    '''' <summary>
    ''''  Imprime con formato de excel el contenido de un treeListView.
    '''' </summary>
    '''' <param name="pDataTree">el arbol que contiene los datos</param>
    '''' <param name="fileName">el nombre que va a tener el archivo temporal</param>
    '''' <remarks></remarks>
    'Public Shared Sub Imprimir(ByVal pDataTree As Zamba.ListControls.TreeListView, ByVal fileName As String)
    '    Dim sArchivo As String
    '    Try
    '        Dim PD As New PrintDialog
    '        Dim Doc As New Printing.PrintDocument
    '        sArchivo = System.Environment.CurrentDirectory & "\" & fileName & ".xls"
    '        PD.Document = Doc
    '        If PD.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            pDataTree.ColumnSortColor = Color.Black
    '            Zamba.Office.ExcelInterop.UCResultGridAExcel(pDataTree, sArchivo, Zamba.Office.ExcelInterop.eEstilosHoja.Reporte2)
    '            Zamba.Office.ExcelInterop.imprimirPlanilla(sArchivo, PD.PrinterSettings)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("No se pudo imprimir en Excel, excepción: " & ex.ToString.Substring(20), "Zamba - Exportar Grilla a Excel®")
    '        Try
    '            Zamba.Office.ExcelInterop.ProcesoExcel.Kill()
    '        Catch
    '        End Try
    '        Exit Sub
    '    End Try
    '    'Borro el archivo temporal
    '    Try
    '        Dim Arch As New System.IO.FileInfo(sArchivo)
    '        Arch.Delete()
    '    Catch sex As Exception
    '    End Try
    'End Sub


    ''' <summary>
    '''  Imprime un archivo de excel
    ''' </summary>
    ''' <param name="fileName">path donde esta el archivo</param>
    ''' <remarks></remarks>
    Public Shared Sub ImprimirExcel(ByVal path As String, Optional ByVal showPreview As Boolean = False)
        Try
            If showPreview = False Then
                Dim PD As New PrintDialog
                If PD.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    Zamba.Office.ExcelInterop.imprimirPlanilla(path, PD.PrinterSettings, showPreview)
                End If
            Else
                Zamba.Office.ExcelInterop.imprimirPlanilla(path, Nothing, showPreview)
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudo imprimir en Excel, excepción: " & ex.ToString.Substring(20), "Zamba - Exportar Grilla a Excel®")
            Try
                Zamba.Office.ExcelInterop.ProcesoExcel.Kill()
            Catch
            End Try
        End Try
    End Sub

    ''' <summary>
    '''  Prints a Office Document
    ''' </summary>
    ''' <param name="OfficeFile">File to print</param>
    '''<param name="Isword">If the document is word=true, if is PowerPoint=false</param>
    ''' <remarks></remarks>
    Public Shared Sub ImprimirWord(ByVal FilePath As String)
        Try
            Zamba.Office.OfficeInterop.ImprimirWord(FilePath)
        Catch ex As Exception
            MessageBox.Show("No se pudo imprimir en Office, excepción: " & ex.ToString.Substring(20))
        End Try
    End Sub

    ''' <summary>
    '''  Prints a Office Document
    ''' </summary>
    ''' <param name="OfficeFile">File to print</param>
    '''<param name="Isword">If the document is word=true, if is PowerPoint=false</param>
    ''' <remarks></remarks>
    Public Shared Sub ImprimirPP(ByVal OfficeFile As Object)
        Try
            Zamba.Office.OfficeInterop.ImprimirPP(OfficeFile)
        Catch ex As Exception
            MessageBox.Show("No se pudo imprimir en Office, excepción: " & ex.ToString.Substring(20))
        End Try
    End Sub
End Class
