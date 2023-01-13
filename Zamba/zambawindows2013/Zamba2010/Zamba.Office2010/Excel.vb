'Imports Lyquidity.Controls
Imports Microsoft.Office.Interop
Imports System.Text
Imports System.Threading
Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms

'Imports Zamba.AppBlock
'Imports Zamba.Core
'Imports Zamba.CoreControls
'Imports Zamba.Servers

''' <summary>
''' Contiene los metodos para exportar a Excel
''' </summary>
''' <remarks></remarks>
Public Class ExcelInterop

    Public Shared ProcesoExcel As System.Diagnostics.Process
    Public Enum eEstilosHoja
        SinFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatNone
        Simple = Excel.XlRangeAutoFormat.xlRangeAutoFormatSimple
        Clasico1 = Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1
        Clasico2 = Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2
        Clasico3 = Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic3
        Contable1 = Excel.XlRangeAutoFormat.xlRangeAutoFormatAccounting1
        Contable2 = Excel.XlRangeAutoFormat.xlRangeAutoFormatAccounting2
        Contable3 = Excel.XlRangeAutoFormat.xlRangeAutoFormatAccounting3
        Contable4 = Excel.XlRangeAutoFormat.xlRangeAutoFormatAccounting4
        Coloreado1 = Excel.XlRangeAutoFormat.xlRangeAutoFormatColor1
        Coloreado2 = Excel.XlRangeAutoFormat.xlRangeAutoFormatColor2
        Coloreado3 = Excel.XlRangeAutoFormat.xlRangeAutoFormatColor3
        Estilo1 = Excel.XlRangeAutoFormat.xlRangeAutoFormatList1
        Estilo2 = Excel.XlRangeAutoFormat.xlRangeAutoFormatList2
        Estilo3 = Excel.XlRangeAutoFormat.xlRangeAutoFormatList3
        Efecto3D1 = Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects1
        Efecto3D2 = Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2
        TablaPivot = Excel.XlRangeAutoFormat.xlRangeAutoFormatClassicPivotTable
        FormatoLocal1 = Excel.XlRangeAutoFormat.xlRangeAutoFormatLocalFormat1
        FormatoLocal2 = Excel.XlRangeAutoFormat.xlRangeAutoFormatLocalFormat2
        FormatoLocal3 = Excel.XlRangeAutoFormat.xlRangeAutoFormatLocalFormat3
        FormatoLocal4 = Excel.XlRangeAutoFormat.xlRangeAutoFormatLocalFormat4
        PTNinguno = Excel.XlRangeAutoFormat.xlRangeAutoFormatPTNone
        Reporte1 = Excel.XlRangeAutoFormat.xlRangeAutoFormatReport1
        Reporte10 = Excel.XlRangeAutoFormat.xlRangeAutoFormatReport10
        Reporte2 = Excel.XlRangeAutoFormat.xlRangeAutoFormatReport2
        Reporte3 = Excel.XlRangeAutoFormat.xlRangeAutoFormatReport3
        Reporte4 = Excel.XlRangeAutoFormat.xlRangeAutoFormatReport4
        Reporte5 = Excel.XlRangeAutoFormat.xlRangeAutoFormatReport5
        Reporte6 = Excel.XlRangeAutoFormat.xlRangeAutoFormatReport6
        Reporte7 = Excel.XlRangeAutoFormat.xlRangeAutoFormatReport7
        Reporte8 = Excel.XlRangeAutoFormat.xlRangeAutoFormatReport8
        Reporte9 = Excel.XlRangeAutoFormat.xlRangeAutoFormatReport9
        Tabla1 = Excel.XlRangeAutoFormat.xlRangeAutoFormatTable1
        Tabla10 = Excel.XlRangeAutoFormat.xlRangeAutoFormatTable10
        Tabla2 = Excel.XlRangeAutoFormat.xlRangeAutoFormatTable2
        Tabla3 = Excel.XlRangeAutoFormat.xlRangeAutoFormatTable3
        Tabla4 = Excel.XlRangeAutoFormat.xlRangeAutoFormatTable4
        Tabla5 = Excel.XlRangeAutoFormat.xlRangeAutoFormatTable5
    End Enum


    Public Shared Sub DataTableToExcel(ByVal pDataTable As DataTable, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja)
        Dim sArchTemp As String = sArchivo & ".tmp"
        Dim bolOpen As Boolean

        FileOpen(1, sArchTemp, OpenMode.Output)
        bolOpen = True

        Dim sb As New StringBuilder
        Try
            Dim dc As DataColumn
            For Each dc In pDataTable.Columns
                sb.Append(dc.Caption)
                sb.Append(Microsoft.VisualBasic.ControlChars.Tab)
            Next
            PrintLine(1, sb.ToString())

            Dim i As Integer = 0
            Dim dr As DataRow
            For Each dr In pDataTable.Rows
                i = 0 : sb.Replace(sb.ToString, "")
                For Each dc In pDataTable.Columns
                    If Not IsDBNull(dr(i)) Then

                        sb.Append(DirectCast(dr(i).ToString(), String))
                        sb.Append(Microsoft.VisualBasic.ControlChars.Tab)
                    Else
                        sb.Append(Microsoft.VisualBasic.ControlChars.Tab)
                    End If
                    i += 1
                Next

                PrintLine(1, sb.ToString())
            Next
            FileClose(1)
            bolOpen = False
            TextToExcel(sArchTemp, sArchivo, eEstilo)
        Finally
            If bolOpen = True Then
                FileClose(1)
            End If
            sb = Nothing
        End Try
    End Sub
    Public Shared Sub DataSetToExcel(ByVal pDataSet As DataSet, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja)
        Dim sArchTemp As String = sArchivo & ".tmp"
        Dim bolOpen As Boolean

        FileOpen(1, sArchTemp, OpenMode.Output)
        bolOpen = True

        Dim sb As New StringBuilder
        Try
            For Each dc As DataColumn In pDataSet.Tables(0).Columns
                sb.Append(dc.Caption)
                sb.Append(Microsoft.VisualBasic.ControlChars.Tab)
            Next
            PrintLine(1, sb.ToString())

            Dim i As Integer = 0
            For Each dr As DataRow In pDataSet.Tables(0).Rows
                i = 0 : sb.Replace(sb.ToString, "")
                For Each dc As DataColumn In pDataSet.Tables(0).Columns
                    If Not IsDBNull(dr(i)) Then
                        sb.Append(DirectCast(dr(i).ToString(), String))
                        sb.Append(Microsoft.VisualBasic.ControlChars.Tab)
                    Else
                        sb.Append(Microsoft.VisualBasic.ControlChars.Tab)
                    End If
                    i += 1
                Next

                PrintLine(1, sb.ToString())
            Next
            FileClose(1)
            bolOpen = False
            TextToExcel(sArchTemp, sArchivo, eEstilo)
        Finally
            If bolOpen = True Then
                FileClose(1)
            End If
            sb = Nothing
        End Try
    End Sub

    'Exporta a excel con hipervínculo a los documentos del reporte.
    Public Shared Function DataTableDirectlyToExcelWithHL(ByVal pDatatable As DataTable, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja, ByVal dtPath As DataTable, Optional ByVal bolShow As Boolean = False, Optional ByVal ForceCulture As Boolean = False) As Boolean
        Dim apxls As New Excel.Application()
        Dim oldCI As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture

        If ForceCulture = True Then
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        End If

        Dim i As Int32 = 1
        Dim j As Int32 = 1
        apxls.Workbooks.Add()

        Try

            For Each dc As DataColumn In pDatatable.Columns
                apxls.Cells(1, i) = dc.Caption
                i += 1
            Next

            For Each dr As DataRow In pDatatable.Rows
                j += 1
                If Not j - 1 > pDatatable.Rows.Count Then
                    i = 0
                    For Each dc As DataColumn In pDatatable.Columns
                        If Not IsDBNull(dr(i)) Then
                            'Date format = ""dd/mm/yy" instead of "dd/mm/yy HH:MM:SS"(MC)
                            If dr(i).GetType().ToString() = "System.DateTime" Then
                                apxls.Cells(j, i + 1) = DirectCast(dr(i), System.DateTime).ToShortDateString()
                            Else
                                If i = 0 Then

                                    Dim strHyperLink As New StringBuilder()
                                    strHyperLink.Append("=Hyperlink(")
                                    strHyperLink.Append(Chr(34))
                                    strHyperLink.Append(dtPath.Rows(j - 2).Item(1).ToString())
                                    strHyperLink.Append(Chr(34))
                                    strHyperLink.Append(",")
                                    strHyperLink.Append(Chr(34))
                                    strHyperLink.Append(dr(i).ToString())
                                    strHyperLink.Append(Chr(34))
                                    strHyperLink.Append(")")

                                    apxls.Cells(j, i + 1) = strHyperLink.ToString()

                                Else
                                    apxls.Cells(j, i + 1) = dr(i).ToString()
                                End If

                            End If
                        End If
                        i += 1
                    Next
                End If
            Next

            'System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

            apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).AutoFormat(eEstilo)
            'Cells Backcolor White and Borders Black
            apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.White)
            apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black)
            'Title blue and bold
            apxls.Range(apxls.Cells(1, 1), apxls.Cells(1, i)).Font.Color = System.Drawing.ColorTranslator.ToOle(Color.FromArgb(0, 157, 224))
            apxls.Range(apxls.Cells(1, 1), apxls.Cells(1, i)).Font.Bold = True
            'Data on black
            apxls.Range(apxls.Cells(2, 1), apxls.Cells(j, i)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)
            'Autofit columns
            apxls.Columns.AutoFit()

            'Borro por las dudas, para que no pregunte Excel, el archivo destino (si existe)
            Try
                File.Delete(sArchivo)
            Catch ex As Exception
            End Try

            apxls.ActiveWorkbook.SaveAs(sArchivo, Excel.XlTextQualifier.xlTextQualifierNone - 1)
            apxls.ActiveWorkbook.Saved = True

            If bolShow = True Then
                apxls.Visible = True
            Else
                apxls.ActiveWorkbook.Close()
                apxls.Workbooks.Close()
                apxls.Quit()
                apxls = Nothing
            End If

            GC.Collect()
            Return True
        Catch ex As Exception
            apxls.Workbooks.Close()
            apxls.Quit()
            apxls = Nothing
            Return False
        Finally
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        End Try

    End Function

    'Public Shared Function DataTableDirectlyToExcel(ByVal pDatatable As DataTable, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja, Optional ByVal bolShow As Boolean = False, Optional ByVal ForceCulture As Boolean = False) As Boolean
    '    Dim apxls As New Excel.Application()
    '    Dim oldCI As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture

    '    If ForceCulture = True Then
    '        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
    '    End If

    '    Dim i As Int32 = 1
    '    Dim j As Int32 = 1
    '    apxls.Workbooks.Add()

    '    Try

    '        For Each dc As DataColumn In pDatatable.Columns
    '            apxls.Cells(1, i) = dc.Caption
    '            i += 1
    '        Next

    '        For Each dr As DataRow In pDatatable.Rows
    '            j += 1
    '            If Not j - 1 > pDatatable.Rows.Count Then
    '                i = 0
    '                For Each dc As DataColumn In pDatatable.Columns
    '                    If Not IsDBNull(dr(i)) Then
    '                        'Date format = ""dd/mm/yy" instead of "dd/mm/yy HH:MM:SS"(MC)
    '                        If dr(i).GetType().ToString() = "System.DateTime" Then
    '                            apxls.Cells(j, i + 1) = DirectCast(dr(i), System.DateTime).ToShortDateString()
    '                        Else
    '                            apxls.Cells(j, i + 1) = dr(i).ToString()
    '                        End If
    '                    End If
    '                    i += 1
    '                Next
    '            End If
    '        Next

    '        'System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

    '        apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).AutoFormat(eEstilo)
    '        'Cells Backcolor White and Borders Black
    '        apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.White)
    '        apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black)
    '        'Title blue and bold
    '        apxls.Range(apxls.Cells(1, 1), apxls.Cells(1, i)).Font.Color = System.Drawing.ColorTranslator.ToOle(Color.FromArgb(0,157,224))
    '        apxls.Range(apxls.Cells(1, 1), apxls.Cells(1, i)).Font.Bold = True
    '        'Data on black
    '        apxls.Range(apxls.Cells(2, 1), apxls.Cells(j, i)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)
    '        'Autofit columns
    '        apxls.Columns.AutoFit()

    '        'Borro por las dudas, para que no pregunte Excel, el archivo destino (si existe)
    '        Try
    '            File.Delete(sArchivo)
    '        Catch ex As Exception
    '        End Try

    '        apxls.ActiveWorkbook.SaveAs(sArchivo, Excel.XlTextQualifier.xlTextQualifierNone - 1)
    '        apxls.ActiveWorkbook.Saved = True

    '        If bolShow = True Then
    '            apxls.Visible = True
    '        Else
    '            apxls.ActiveWorkbook.Close()
    '            apxls.Workbooks.Close()
    '            apxls.Quit()
    '            apxls = Nothing
    '        End If

    '        GC.Collect()
    '        Return True
    '    Catch ex As Exception
    '        apxls.Workbooks.Close()
    '        apxls.Quit()
    '        apxls = Nothing
    '        Return False
    '    Finally
    '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
    '    End Try


    'End Function

    Public Shared Sub imprimirPlanilla(ByVal sArchivo As String, ByVal PreferenciasImpresion As Printing.PrinterSettings, Optional ByVal showPreview As Boolean = False)
        Dim Exc As Excel.Application = New Excel.Application
        Exc.Workbooks.Open(sArchivo)

        'imprimimos o vemos la vista previa
        If showPreview = False Then
            Dim Ws As Excel.Worksheet = Exc.ActiveWorkbook.ActiveSheet

            'Centramos horizontalmente la hoja para que quede mejor
            Ws.PageSetup.CenterHorizontally = True
            'Ajustamos el tamaño de la grilla para que entre en el ancho de la hoja
            Ws.PageSetup.FitToPagesWide = 1
            'Margenes
            Ws.PageSetup.LeftMargin = 1.0
            Ws.PageSetup.RightMargin = 2.5
            Ws.PageSetup.TopMargin = 1.0
            Ws.PageSetup.BottomMargin = 1.0
            'Ponemos la hoja apaisada o normal, segun corresponda
            If PreferenciasImpresion.LandscapeAngle = 90 Then
                Ws.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape
            Else
                Ws.PageSetup.Orientation = Excel.XlPageOrientation.xlPortrait
            End If
            Ws.PrintOut(, , PreferenciasImpresion.Copies, , Exc.ActivePrinter())

            Ws = Nothing
        Else
            Exc.Visible = True
            Exc.ActiveWorkbook.PrintPreview(True)
            Exc.Visible = False
        End If

        'cerramos el libro
        Exc.ActiveWorkbook.Close(False)
        'salimos del excel
        Exc.Quit()

        Exc = Nothing

        GC.Collect()
    End Sub
    'Public Shared Function abrirArchivoPlanilla(ByVal sArchivo As String)
    Public Shared Sub abrirArchivoPlanilla(ByVal sArchivo As String)
        Dim vCultura As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
        ProcesoExcel = New System.Diagnostics.Process
        ProcesoExcel.EnableRaisingEvents = False

        System.Diagnostics.Process.Start("Excel.exe", sArchivo)
        'ProcesoExcel.Start("Excel.exe", sArchivo)
        System.Threading.Thread.CurrentThread.CurrentCulture = vCultura
    End Sub

    'Public Overloads Shared Function UCResultGridAExcel(ByVal TreeListView As Zamba.ListControls.TreeListView, ByVal filePath As String, ByVal style As eEstilosHoja) As Boolean
    '    Try
    '        Dim pathCSV As String = filePath.Replace(".xls", ".csv")
    '        Dim texto As String = String.Empty
    '        Dim streamWriter As New StreamWriter(pathCSV)
    '        Dim indexList As New Hashtable

    '        For Each tch As Zamba.ListControls.ToggleColumnHeader In TreeListView.Columns
    '            texto &= tch.Text.Trim & ", "
    '        Next

    '        texto = texto.Remove(texto.Length - 2)
    '        streamWriter.WriteLine(texto)

    '        For Each node As TreeNode In TreeListView.Nodes
    '            Dim result As Result = DirectCast(DirectCast(node, Object).ZambaCore, Zamba.Core.Result)

    '            For Each CurrentIndex As Index In result.Indexs
    '                If Not indexList.ContainsKey(CurrentIndex.Name) Then
    '                    indexList.Add(CurrentIndex.Name, CurrentIndex)
    '                End If
    '            Next
    '        Next

    '        texto = String.Empty

    '        Dim index As Index
    '        Dim ColumnText As String = String.Empty
    '        For Each tch As Zamba.ListControls.ToggleColumnHeader In TreeListView.Columns
    '            ColumnText = tch.Text.Trim
    '            If indexList.ContainsKey(ColumnText) Then
    '                index = indexList.Item(ColumnText)
    '                texto &= index.Data() & ", "
    '            End If
    '        Next

    '        indexList.Clear()
    '        texto = texto.Remove(texto.Length - 2)

    '        streamWriter.WriteLine(texto)
    '        streamWriter.Close()

    '        TextToExcel(pathCSV, filePath, style)
    '        Return True

    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Function

    Public Overloads Shared Function UCResultGridAExcel(ByRef ZExtendedTreeView As Object, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja) As Boolean
        Dim sb As StringBuilder = New StringBuilder
        Dim sw As New StreamWriter(sArchivo & ".tmp")
        Dim i As Int32
        'Dim Indice As IIndex
        Dim DicIndices As New Hashtable
        'todo: Corregir exportacion a excel
        Dim item As Object 'Zamba.ListControls.TreeListNode
        Dim subitem As Object

        Try
            'Save the columns names
            For i = 0 To ZExtendedTreeView.Columns.Count - 1  'Columns.Count - 1
                Try
                    sb.Append(ZExtendedTreeView.Columns.Item(i).Caption)
                    sb.Append(Microsoft.VisualBasic.ControlChars.Tab)
                Catch ex As Exception
                    Throw New System.Exception(ex.Message)
                End Try
            Next

            sw.WriteLine(sb.ToString)

            'Foreach node save the data on the stringBuilder
            For Each item In ZExtendedTreeView.Nodes
                'Save the name of the document
                sb.Replace(sb.ToString, "")
                sb.Append(item.Text.Trim)
                sb.Append(Microsoft.VisualBasic.ControlChars.Tab)
                'Save the rest of the values
                For Each subitem In item.subitems
                    If Not IsDBNull(subitem.Text.Trim) Then
                        sb.Append(subitem.Text.Trim)
                        sb.Append(Microsoft.VisualBasic.ControlChars.Tab)
                    Else
                        sb.Append(Microsoft.VisualBasic.ControlChars.Tab)
                    End If
                Next
                sw.WriteLine(sb.ToString)
            Next
            sw.Close()
            'Llamo a la funcion que convierte el archivo generado a Excel®
            TextToExcelNew(sArchivo & ".tmp", sArchivo, eEstilo)
            Return True
        Finally
            sb.Remove(0, sb.Length)
            sb = Nothing
        End Try
    End Function
    Public Shared Sub DataTableDirectlyToPrint(ByVal pDataTable As DataTable, ByVal eEstilo As eEstilosHoja, ByVal PreferenciasImpresion As Printing.PrinterSettings)
        Dim apxls As New Excel.Application()
        Dim i As Int32 = 1
        Dim j As Int32 = 1
        apxls.Workbooks.Add()

        For Each dc As DataColumn In pDataTable.Columns
            apxls.Cells(1, i) = dc.Caption
            i += 1
        Next

        For Each dr As DataRow In pDataTable.Rows
            j += 1
            If Not j - 1 > pDataTable.Rows.Count Then
                i = 0
                For Each dc As DataColumn In pDataTable.Columns
                    If Not IsDBNull(dr(i)) Then
                        apxls.Cells(j, i + 1) = dr(i).ToString()
                    End If
                    i += 1
                Next
            End If
        Next

        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

        apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).AutoFormat(eEstilo)
        'Cells Backcolor White and Borders Black
        apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.White)
        apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black)
        'Title blue and bold
        apxls.Range(apxls.Cells(1, 1), apxls.Cells(1, i)).Font.Color = System.Drawing.ColorTranslator.ToOle(Color.FromArgb(0, 157, 224))
        apxls.Range(apxls.Cells(1, 1), apxls.Cells(1, i)).Font.Bold = True
        'Data on black
        apxls.Range(apxls.Cells(2, 1), apxls.Cells(j, i)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)
        'Autofit columns
        apxls.Columns.AutoFit()

        'Centramos horizontalmente la hoja para que quede mejor
        apxls.ActiveSheet.PageSetup.CenterHorizontally = True
        'Ajustamos el tamaño de la grilla para que entre en el ancho de la hoja
        apxls.ActiveSheet.PageSetup.FitToPagesWide = 1
        'Margenes
        apxls.ActiveSheet.PageSetup.LeftMargin = 1.0
        apxls.ActiveSheet.PageSetup.RightMargin = 2.5
        apxls.ActiveSheet.PageSetup.TopMargin = 1.0
        apxls.ActiveSheet.PageSetup.BottomMargin = 1.0
        'Ponemos la hoja apaisada o normal, segun corresponda
        If PreferenciasImpresion.LandscapeAngle = 90 Then
            apxls.ActiveSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape
        Else
            apxls.ActiveSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlPortrait
        End If
        'imprimimos en la impresora seleccionada las copias correspondientes
        apxls.ActiveSheet.PrintOut(, , PreferenciasImpresion.Copies, , apxls.ActivePrinter())


        'cerramos el libro
        'apxls.ActiveSheet.Close(False)
        'salimos del excel
        apxls.ActiveWorkbook.Close(False)
        apxls.Quit()
        apxls = Nothing

    End Sub
    ''' <summary>
    ''' Exporta los datos de un DataTable a un Excel
    ''' </summary>
    ''' <history>
    ''' [sebastian 29-12-2009] se comento el metodo para reemplarlo por el que se usa en office2003 ya que son igual a diferencia 
    '''de un if que se usar para forzar la cultura de la pc
    ''' </history>
    ''' <param name="pDatatable"></param>
    ''' <param name="sArchivo"></param>
    ''' <param name="eEstilo"></param>
    ''' <param name="bolShow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Shared Function DataTableDirectlyToExcel(ByVal pDatatable As DataTable, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja, Optional ByVal bolShow As Boolean = False) As Boolean
    '    Dim apxls As New Excel.Application()
    '    Dim i As Int32 = 1
    '    Dim j As Int32 = 1

    '    Dim oldCI As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
    '    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
    '    apxls.Workbooks.Add()

    '    Try

    '        For Each dc As DataColumn In pDatatable.Columns
    '            apxls.Cells(1, i) = dc.Caption
    '            i += 1
    '        Next

    '        For Each dr As DataRow In pDatatable.Rows
    '            j += 1
    '            If Not j - 1 > pDatatable.Rows.Count Then
    '                i = 0
    '                For Each dc As DataColumn In pDatatable.Columns
    '                    If Not IsDBNull(dr(i)) Then
    '                        'Date format = ""dd/mm/yy" instead of "dd/mm/yy HH:MM:SS"(MC)
    '                        If dr(i).GetType().ToString() = "System.DateTime" Then
    '                            apxls.Cells(j, i + 1) = DirectCast(dr(i), System.DateTime).ToShortDateString()
    '                        Else
    '                            apxls.Cells(j, i + 1) = dr(i).ToString()
    '                        End If
    '                    End If
    '                    i += 1
    '                Next
    '            End If
    '        Next

    '        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

    '        apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).AutoFormat(eEstilo)
    '        'Cells Backcolor White and Borders Black
    '        apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.White)
    '        apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black)
    '        'Title blue and bold
    '        apxls.Range(apxls.Cells(1, 1), apxls.Cells(1, i)).Font.Color = System.Drawing.ColorTranslator.ToOle(Color.FromArgb(0,157,224))
    '        apxls.Range(apxls.Cells(1, 1), apxls.Cells(1, i)).Font.Bold = True
    '        'Data on black
    '        apxls.Range(apxls.Cells(2, 1), apxls.Cells(j, i)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)
    '        'Autofit columns
    '        apxls.Columns.AutoFit()

    '        'Borro por las dudas, para que no pregunte Excel, el archivo destino (si existe)
    '        Try
    '            File.Delete(sArchivo)
    '        Catch ex As Exception
    '        End Try

    '        apxls.ActiveWorkbook.SaveAs(sArchivo, Excel.XlTextQualifier.xlTextQualifierNone - 1)
    '        apxls.ActiveWorkbook.Saved = True

    '        If bolShow = True Then
    '            apxls.Visible = True
    '        Else
    '            apxls.ActiveWorkbook.Close()
    '            apxls.Workbooks.Close()
    '            apxls.Quit()
    '            apxls = Nothing
    '        End If

    '        GC.Collect()
    '        Return True
    '    Catch ex As Exception
    '        apxls.Workbooks.Close()
    '        apxls.Quit()
    '        apxls = Nothing
    '        Return False

    '    Finally
    '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
    '    End Try
    '    System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
    'End Function
    Public Shared Function DataTableDirectlyToExcel(ByVal pDatatable As DataTable, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja, ByVal bolShow As Boolean, ByVal Culture As String) As Boolean
        Dim apxls As New Excel.Application()
        Dim oldCI As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(Culture)

        Dim i As Int32 = 1
        Dim j As Int32 = 1

        apxls.Workbooks.Add()
        'apxls.ActiveWorkbook.CheckCompatibility = False
        'apxls.DisplayAlerts = False
        'apxls.ActiveWorkbook.DoNotPromptForConvert = True
        Try

            For Each dc As DataColumn In pDatatable.Columns
                apxls.Cells(1, i) = dc.Caption
                i += 1
            Next

            For Each dr As DataRow In pDatatable.Rows
                j += 1
                If Not j - 1 > pDatatable.Rows.Count Then
                    i = 0
                    For Each dc As DataColumn In pDatatable.Columns
                        If Not IsDBNull(dr(i)) Then
                            'Date format = ""dd/mm/yy" instead of "dd/mm/yy HH:MM:SS"(MC)
                            If dr(i).GetType().ToString() = "System.DateTime" Then
                                apxls.Cells(j, i + 1) = DirectCast(dr(i), System.DateTime).ToString("yyyy-MM-dd")
                            Else
                                apxls.Cells(j, i + 1) = dr(i).ToString()
                            End If
                        End If
                        i += 1
                    Next
                End If
            Next

            'System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

            apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).AutoFormat(eEstilo)
            'Cells Backcolor White and Borders Black
            apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.White)
            apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black)
            'Title blue and bold
            apxls.Range(apxls.Cells(1, 1), apxls.Cells(1, i)).Font.Color = System.Drawing.ColorTranslator.ToOle(Color.FromArgb(0, 157, 224))
            apxls.Range(apxls.Cells(1, 1), apxls.Cells(1, i)).Font.Bold = True
            'Data on black
            apxls.Range(apxls.Cells(2, 1), apxls.Cells(j, i)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)
            'Autofit columns
            apxls.Columns.AutoFit()

            'Extesion XLSX por defecto, ya que si es 2007 la genera con ese formato
            Dim sArchivoPartido() As String = sArchivo.Split(".")
            Dim sExtVieja As String = sArchivoPartido(sArchivoPartido.Length - 1)
            Dim sExtNueva As String = "xlsx"

            If sArchivoPartido.Length() = 2 Then
                sArchivo = sArchivo.Replace(sExtVieja, sExtNueva)
            End If

            'Borro por las dudas, para que no pregunte Excel, el archivo destino (si existe)
            Try
                File.Delete(sArchivo)
            Catch ex As Exception
            End Try

            'apxls.ActiveWorkbook.SaveAs(sArchivo, Excel.XlTextQualifier.xlTextQualifierNone - 1)
            apxls.ActiveWorkbook.SaveAs(sArchivo)
            apxls.ActiveWorkbook.Saved = True

            If bolShow = True Then
                apxls.Visible = True
            Else
                apxls.ActiveWorkbook.Close()
                apxls.Workbooks.Close()
                apxls.Quit()
                apxls = Nothing
            End If

            GC.Collect()
            Return True
        Catch ex As Exception
            apxls.Workbooks.Close()
            apxls.Quit()
            apxls = Nothing
            Return False
        Finally
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        End Try


    End Function
    Public Shared Function DataTableDirectlyToExcel(ByVal addcol As Boolean, ByVal pDatatable As DataTable, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja, Optional ByVal bolShow As Boolean = False) As Boolean
        Dim apxls As New Excel.Application()
        Dim i As Int32 = 0
        Dim j As Int32 = 0
        apxls.Workbooks.Add()

        Try

            If addcol Then
                i = 1
                j = 1
                For Each dc As DataColumn In pDatatable.Columns
                    apxls.Cells(1, i) = dc.Caption
                    i += 1
                Next
            End If

            For Each dr As DataRow In pDatatable.Rows
                j += 1
                If Not j - 1 > pDatatable.Rows.Count Then
                    i = 0
                    For Each dc As DataColumn In pDatatable.Columns
                        If Not IsDBNull(dr(i)) Then
                            'Date format = ""dd/mm/yy" instead of "dd/mm/yy HH:MM:SS"(MC)
                            If dr(i).GetType().ToString() = "System.DateTime" Then
                                apxls.Cells(j, i + 1) = DirectCast(dr(i), System.DateTime).ToShortDateString()
                            Else
                                apxls.Cells(j, i + 1) = dr(i).ToString()
                            End If
                        End If
                        i += 1
                    Next
                End If
            Next

            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

            apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).AutoFormat(eEstilo)
            'Cells Backcolor White and Borders Black
            apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.White)
            apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black)
            'Title blue and bold
            If addcol Then
                apxls.Range(apxls.Cells(1, 1), apxls.Cells(1, i)).Font.Color = System.Drawing.ColorTranslator.ToOle(Color.FromArgb(0, 157, 224))
                apxls.Range(apxls.Cells(1, 1), apxls.Cells(1, i)).Font.Bold = True
                'Data on black
                apxls.Range(apxls.Cells(2, 1), apxls.Cells(j, i)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)
            End If
            'Autofit columns
            apxls.Columns.AutoFit()

            'Borro por las dudas, para que no pregunte Excel, el archivo destino (si existe)
            Try
                File.Delete(sArchivo)
            Catch ex As Exception
            End Try

            'apxls.ActiveWorkbook.SaveAs(sArchivo, tipe)
            apxls.ActiveWorkbook.SaveAs(sArchivo)
            apxls.ActiveWorkbook.Saved = True

            If bolShow = True Then
                apxls.Visible = True
            Else
                apxls.ActiveWorkbook.Close()
                apxls.Workbooks.Close()
                apxls.Quit()
                apxls = Nothing
            End If

            GC.Collect()
            Return True
        Catch ex As Exception
            apxls.Workbooks.Close()
            apxls.Quit()
            apxls = Nothing
            Return False
        End Try
    End Function
    '29/11/06 [Andres] , por alguna razon estaba comentado el codigo de este procedimiento que necesito
    'Public Overloads Shared Function UCResultGridAExcel(Byval RG As ExtendedListViews.TreeListView, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja) As Boolean
    '    '  Dim c As ColumnHeader
    '    Dim sb As String = ""
    '    Dim sw As New StreamWriter(sArchivo & ".tmp")
    '    Dim i As Int32
    '    Dim Indice As Index
    '    Dim DicIndices As New Hashtable
    '    Dim item As ExtendedListViews.ContainerListViewItem

    '    'guardamos las columnas
    '    For i = 0 To RG.Columns.Count - 1
    '        Try
    '            sb &= RG.Columns.Item(i).Text.Trim
    '            sb &= Microsoft.VisualBasic.ControlChars.Tab
    '        Catch ex As Exception
    '        End Try
    '    Next
    '    sw.WriteLine(sb)


    '    For Each item In RG.Items
    '        'Borro los elementos del diccionario de nombres de los indices
    '        DicIndices.Clear()
    '        'Agrego los indices del Result actual
    '        i = 0
    '        While i < DirectCast(DirectCast(item, ZXSearchNode).ZambaCore, Result).Indexs.Count
    '            Indice = DirectCast(DirectCast(item, ZXSearchNode).ZambaCore, Result).Indexs(i)
    '            DicIndices.Add(Indice.Name.Trim.ToUpper, Indice)
    '            i += 1
    '        End While 'While i < item.Result.Indexs.Count
    '        sb = ""

    '        'completo las 'columnas' con los indices del Result actual
    '        'For Each c In RG.ResultController.IndicesComunes
    '        For i = 0 To RG.Columns.Count - 1
    '            Try
    '                'Indice = DicIndices.Item(c.Text.Trim.ToUpper)
    '                Indice = DicIndices.Item(RG.Columns.Item(i).Text.Trim.ToUpper)
    '                If IsNothing(Indice) Then
    '                    If String.Compare(RG.Columns.Item(i).Text.Trim, "NOMBRE") = 0 Then
    '                        sb &= item.Text.Trim & Microsoft.VisualBasic.ControlChars.Tab
    '                    Else
    '                        'Si no contiene el indice dejo la 'columna' vacia
    '                        sb &= Microsoft.VisualBasic.ControlChars.Tab
    '                    End If
    '                Else
    '                    'pongo en la 'columna' el valor del indice
    '                    sb &= Indice.Data() & Microsoft.VisualBasic.ControlChars.Tab
    '                    'pongo el separador de columna
    '                    'sb &= Microsoft.VisualBasic.ControlChars.Tab
    '                End If
    '            Catch ex As Exception
    '            End Try
    '        Next 'For Each c In RG.ResultController.IndicesComunes

    '        'Grabo la fila en el archivo
    '        sw.WriteLine(sb)
    '    Next 'For Each item In RG.Items

    '    Try
    '        sw.Close()
    '    Catch ex As Exception
    '    End Try
    '    'Llamo a la funcion que convierte el archivo generado a Excel®
    '    TextToExcel(sArchivo & ".tmp", sArchivo, eEstilo)
    '    MessageBox.Show("LA EXPORTACION A EXCEL SE COMPLETO EXITOSAMENTE")
    '    Return True
    'End Function

    'Public Shared Sub TextToExcel(ByVal sourceFilePath As String, ByVal finishedFilePath As String, ByVal sheetStyle As eEstilosHoja)
    '    Dim excel As Excel.Application
    '    Try
    '        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
    '        excel = New Excel.Application
    '        excel.Workbooks.OpenText(sourceFilePath, , , , Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierNone, , True)
    '        Dim sheet As Excel.Worksheet = excel.ActiveWorkbook.ActiveSheet
    '        sheet.Range(sheet.Cells(1, 1), sheet.Cells(sheet.UsedRange.Rows.Count, sheet.UsedRange.Columns.Count)).AutoFormat(sheetStyle)
    '        If File.Exists(finishedFilePath) Then File.Delete(finishedFilePath)
    '        'excel.ActiveWorkbook.SaveAs(finishedFilePath, excel.XlTextQualifier.xlTextQualifierNone - 1)

    '        excel.SaveWorkspace(finishedFilePath)
    '        If File.Exists(sourceFilePath) Then File.Delete(sourceFilePath)
    '        excel.Quit()
    '        sheet = Nothing
    '        excel = Nothing
    '        MessageBox.Show("LA EXPORTACIÓN A EXCEL SE COMPLETO EXITOSAMENTE")
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    Finally
    '        If Not IsNothing(excel) Then
    '            excel.Quit()
    '            excel = Nothing
    '        End If
    '        GC.Collect()
    '    End Try
    'End Sub
    Public Shared Sub TextToExcel(ByVal sArchOrg As String, ByVal pFileName As String, ByVal eEstilo As eEstilosHoja)

        Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

        Dim ExcelApplication As Excel.Application = Nothing
        Try
            ExcelApplication = New Excel.Application

            ExcelApplication.Workbooks.OpenText(sArchOrg, , , , Excel.XlTextQualifier.xlTextQualifierNone, , True)

            Dim Wb As Excel.Workbook = ExcelApplication.ActiveWorkbook
            Dim Ws As Excel.Worksheet = Wb.ActiveSheet

            Ws.Range(Ws.Cells(1, 1), Ws.Cells(Ws.UsedRange.Rows.Count, Ws.UsedRange.Columns.Count)).AutoFormat(eEstilo)
            'Cells Backcolor White and Borders Black
            Ws.Range(Ws.Cells(1, 1), Ws.Cells(Ws.UsedRange.Rows.Count, Ws.UsedRange.Columns.Count)).Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.White)
            Ws.Range(Ws.Cells(1, 1), Ws.Cells(Ws.UsedRange.Rows.Count, Ws.UsedRange.Columns.Count)).Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black)
            'Title blue and bold
            Ws.Range(Ws.Cells(1, 1), Ws.Cells(1, Ws.UsedRange.Columns.Count)).Font.Color = System.Drawing.ColorTranslator.ToOle(Color.Blue) 'FromArgb(0,157,224)
            Ws.Range(Ws.Cells(1, 1), Ws.Cells(1, Ws.UsedRange.Columns.Count)).Font.Bold = True
            Ws.Range(Ws.Cells(1, 1), Ws.Cells(1, Ws.UsedRange.Columns.Count)).Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.Green)
            'Data on black
            Ws.Range(Ws.Cells(2, 1), Ws.Cells(Ws.UsedRange.Rows.Count, Ws.UsedRange.Columns.Count)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)
            'Autofit columns
            ExcelApplication.Columns.AutoFit()

            'Borro por las dudas, para que no pregunte Excel, el archivo destino (si existe)
            File.Delete(pFileName)

            ExcelApplication.ActiveWorkbook.SaveAs(pFileName, Excel.XlTextQualifier.xlTextQualifierNone - 1)

            ExcelApplication.Quit()

            Ws = Nothing
            Wb = Nothing
            ExcelApplication = Nothing

            File.Delete(sArchOrg)

        Catch ex As Exception
            If Not IsNothing(ExcelApplication) Then
                ExcelApplication.Quit()
                ExcelApplication = Nothing
            End If
        End Try
    End Sub
    ''' <summary>
    ''' Generates a Excel Document
    ''' </summary>
    ''' <param name="title">Title of the document</param>
    ''' <param name="title">Arraylist with strings</param>
    ''' <param name="body">Arraylist with values to by shown</param>
    ''' <param name="footer">Footer of the document</param>
    ''' <param name="sumPos">Position of the columns in the body to be sumed</param>
    ''' <param name="countPos">Position of the columns in the body to be counted</param>
    ''' <remarks></remarks>
    Public Shared Sub GenerateExcelDoc(ByVal title As String, ByVal header As ArrayList, ByVal body As ArrayList, ByVal footer As String, ByVal sumPos As ArrayList, ByVal countPos As ArrayList)
        Dim apxls As New Excel.Application()
        Try
            Dim i As Int32 = 2
            Dim j As Int32 = 1
            Dim iTotal As Int32 = 0
            Dim iCount As Int32 = 0
            Dim total(sumPos.Count) As Double
            Dim count(countPos.Count) As Int32

            apxls.Workbooks.Add()

            'Set the title
            If title <> "" Then
                apxls.Cells(i, j) = title
                apxls.Range(apxls.Cells(1, 1), apxls.Cells(i, j)).Font.Bold = True
                i += 2
            End If

            j = 2
            'Set the header
            For Each v1 As String In header
                If v1 <> "" Then
                    apxls.Cells(i, j) = v1
                End If
                j += 1
            Next
            apxls.Range(apxls.Cells(i, 1), apxls.Cells(i, j)).Font.Bold = True
            j = 2
            i += 2

            'Le agrego el cuerpo
            For Each v1 As ArrayList In body
                For Each v2 As String In v1
                    If v2 <> "" Then
                        apxls.Cells(i, j) = v2
                    End If
                    'If the sum is required do it
                    For Each pos As Int32 In sumPos
                        Try
                            If pos = j - 1 Then
                                total(iTotal) += Double.Parse(v2)
                                iTotal += 1
                                Exit For
                            End If
                        Catch
                        End Try
                    Next
                    'If the count is required do it
                    For Each pos As Int32 In countPos
                        Try
                            If pos = j - 1 Then
                                count(iCount) += 1
                                iCount += 1
                                Exit For
                            End If
                        Catch
                        End Try
                    Next
                    j += 1
                Next
                i += 1
                iTotal = 0
                iCount = 0
                j = 2
            Next

            j = 1
            i += 1
            'Write the Totals
            If total.Length > 0 Then
                apxls.Cells(i, 1) = "Totales"
                i += 1
                For Each tot As Int32 In sumPos
                    apxls.Cells(i, tot + 1) = total(j - 1)
                    j += 1
                Next
                apxls.Range(apxls.Cells(i - 1, 1), apxls.Cells(i, header.Count)).Font.Bold = True
                i += 1
                j = 1
            End If

            'Write the Count
            If count.Length > 0 Then
                apxls.Cells(i, 1) = "Recuento"
                i += 1
                For Each co As Int32 In countPos
                    apxls.Cells(i, co + 1) = count(j - 1)
                    j += 1
                Next
                apxls.Range(apxls.Cells(i - 1, 1), apxls.Cells(i, header.Count)).Font.Bold = True
                i += 1
                j = 1
            End If

            i += 2
            'Write the footer
            apxls.Cells(i, j) = footer
            apxls.Range(apxls.Cells(i, j), apxls.Cells(i, j)).Font.Bold = True

            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

            'Data on black
            apxls.Range(apxls.Cells(2, 1), apxls.Cells(i, 2)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)
            'Autofit columns
            apxls.Columns.AutoFit()

            'Show the excel
            apxls.Visible = True
        Catch ex As Exception
            apxls.Workbooks.Close()
            apxls.Quit()
            apxls = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Hace la copia de un archivo de excel a la direccion especificada por el usuario
    ''' </summary>
    ''' <param name="path">Path del archivo original</param>
    ''' <remarks></remarks>
    Public Shared Sub SaveAsExcel(ByVal path As String)
        Dim Exc As Excel.Application = Nothing
        Dim DS As New SaveFileDialog()

        DS.CheckPathExists = True
        DS.ValidateNames = True
        DS.RestoreDirectory = True
        DS.Filter = "Microsoft Office Excel|*.xls|Todos los Archivos|*.*"
        If DS.ShowDialog() = DialogResult.OK Then

            Exc = New Excel.Application

            Exc.Workbooks.Open(path)

            Exc.ActiveWorkbook.SaveAs(DS.FileName, Excel.XlTextQualifier.xlTextQualifierNone - 1)

            Exc.Quit()

            Exc = Nothing
        End If
    End Sub
    Public Shared Sub TextToExcelNew(ByVal sArchOrg As String, ByVal pFileName As String, ByVal eEstilo As eEstilosHoja)

        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

        Dim Exc As Excel.Application = Nothing
        Try
            Exc = New Excel.Application

            Exc.Workbooks.OpenText(sArchOrg, , , , Excel.XlTextQualifier.xlTextQualifierNone, , True)

            Dim Wb As Excel.Workbook = Exc.ActiveWorkbook
            Dim Ws As Excel.Worksheet = Wb.ActiveSheet

            Ws.Range(Ws.Cells(1, 1), Ws.Cells(Ws.UsedRange.Rows.Count, Ws.UsedRange.Columns.Count)).AutoFormat(eEstilo)
            'Cells Backcolor White and Borders Black
            Ws.Range(Ws.Cells(1, 1), Ws.Cells(Ws.UsedRange.Rows.Count, Ws.UsedRange.Columns.Count)).Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.White)
            Ws.Range(Ws.Cells(1, 1), Ws.Cells(Ws.UsedRange.Rows.Count, Ws.UsedRange.Columns.Count)).Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black)
            'Title blue and bold
            Ws.Range(Ws.Cells(1, 1), Ws.Cells(1, Ws.UsedRange.Columns.Count)).Font.Color = System.Drawing.ColorTranslator.ToOle(Color.FromArgb(0, 157, 224))
            Ws.Range(Ws.Cells(1, 1), Ws.Cells(1, Ws.UsedRange.Columns.Count)).Font.Bold = True
            'Data on black
            Ws.Range(Ws.Cells(2, 1), Ws.Cells(Ws.UsedRange.Rows.Count, Ws.UsedRange.Columns.Count)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)
            'Autofit columns
            Exc.Columns.AutoFit()

            'Borro por las dudas, para que no pregunte Excel, el archivo destino (si existe)
            Try
                File.Delete(pFileName)
            Catch ex As Exception
            End Try

            Exc.ActiveWorkbook.SaveAs(pFileName, Excel.XlTextQualifier.xlTextQualifierNone - 1)

            Exc.Quit()

            Ws = Nothing
            Wb = Nothing
            Exc = Nothing

            'Delete the temporal file
            Try
                File.Delete(sArchOrg)
            Catch ex As Exception
            End Try

            GC.Collect()

        Catch ex As Exception
            Exc.Quit()
            Exc = Nothing
        End Try
        'ProcesoExcel = New System.Diagnostics.Process
        'ProcesoExcel.EnableRaisingEvents = False
        'ProcesoExcel.Start("Excel.exe", pFileName)
        'System.Threading.Thread.CurrentThread.CurrentCulture = vCultura
    End Sub

    Public Shared Function setExcelGraphics(ByVal dt As System.Data.DataTable, ByVal graphtype As String, ByVal path As String, ByVal save As Boolean, ByVal bydate As Boolean) As Boolean
        return False
    End Function
End Class