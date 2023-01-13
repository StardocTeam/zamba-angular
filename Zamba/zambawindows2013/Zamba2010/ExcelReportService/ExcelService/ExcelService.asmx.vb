Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web
Imports Microsoft.Office.Interop.Excel
Imports System.Data
Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Drawing

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<ToolboxItem(False)> _
Public Class ExcelService
    Inherits System.Web.Services.WebService


    <WebMethod()> _
      Public Function setExcelGraphics(ByVal ds As System.Data.DataSet, ByVal graphtype As String, ByVal path As String, ByVal save As Boolean, ByVal bydate As Boolean) As Boolean
        If Not IsNothing(ds) And String.Compare(path, String.Empty) <> 0 And String.Compare(graphtype, String.Empty) <> 0 Then
            Dim oldCI As System.Globalization.CultureInfo = _
                                System.Threading.Thread.CurrentThread.CurrentCulture

            Dim track As StringBuilder = New StringBuilder()
            track.AppendLine("Ingresando a generar gráfico")
            track.AppendLine("Path: " & path)
            Dim app As Application = New Application
            Try
                app.UserControl = True
                System.Threading.Thread.CurrentThread.CurrentCulture = _
                    New System.Globalization.CultureInfo("en-US")

                app.Workbooks.Add()
                Dim Wb As Workbook = app.ActiveWorkbook
                Wb.Sheets.Add()
                Dim Ws As Worksheet = Wb.ActiveSheet
                Dim range As Range
                Dim chartExcel As ChartObject
                Dim axi As Axes

                Dim i As Int32 = 1
                Dim j As Int32 = 1

                Dim colCount As Int32 = ds.Tables(0).Columns.Count

                'Salvo los encabezados
                track.AppendLine("Salvando encabezados")
                track.AppendLine("Cantidad de Columnas: " & ds.Tables(0).Columns.Count.ToString())
                For Each column As DataColumn In ds.Tables(0).Columns
                    'If j = 1 And bydate = True Then
                    '    Ws.Cells(i, colCount) = column.ColumnName
                    'ElseIf j = colCount And bydate = True Then
                    '    Ws.Cells(i, 1) = column.ColumnName
                    'Else
                    Ws.Cells(i, j) = column.ColumnName
                    'End If
                    j += 1
                Next

                i = 2
                'Salvo los datos de la grilla
                track.AppendLine("Salvando datos")
                track.AppendLine("Cantidad de Rows: " & ds.Tables(0).Rows.Count.ToString())
                For Each Row As DataRow In ds.Tables(0).Rows
                    j = 1
                    For Each cell As Object In Row.ItemArray()
                        'If j = 1 And bydate = True Then
                        '    Ws.Cells(i, colCount) = cell.ToString().Replace(",", ".")
                        'ElseIf j = colCount And bydate = True Then
                        '    Ws.Cells(i, 1) = cell.ToString().Replace(",", ".")
                        'Else
                        Ws.Cells(i, j) = cell.ToString().Replace(",", ".")
                        'End If
                        j += 1
                    Next
                    i += 1
                Next

                track.AppendLine("Save=" & save.ToString())
                If save = False Then
                    track.AppendLine("Generando gráfico")
                    chartExcel = Ws.ChartObjects.Add(0, 0, 600, 400)

                    'Tipo de gráfico
                    If String.Compare(graphtype.ToUpper(), "LINEAS") = 0 Then
                        chartExcel.Chart.ChartType = XlChartType.xlLine
                    ElseIf String.Compare(graphtype.ToUpper(), "AREA") = 0 Then
                        chartExcel.Chart.ChartType = XlChartType.xlArea
                    ElseIf String.Compare(graphtype.ToUpper(), "COLUMNAS") = 0 Then
                        chartExcel.Chart.ChartType = XlChartType.xlColumnClustered
                    ElseIf String.Compare(graphtype.ToUpper(), "BARRAS") = 0 Then
                        chartExcel.Chart.ChartType = XlChartType.xl3DBarStacked
                    End If

                    'rango del gráfico
                    track.AppendLine("Asginando rango")
                    range = Ws.Range(Ws.Cells(1, 1), Ws.Cells(i - 1, j - 1))
                    'Hacer por columnas
                    chartExcel.Chart.SetSourceData(range, XlRowCol.xlColumns)

                    'No muestra los comentarios del gráfico
                    chartExcel.Chart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowNone)
                    chartExcel.Chart.Legend.Font.Bold = True
                    axi = chartExcel.Chart.Axes(, XlAxisGroup.xlPrimary)
                    axi.Item(XlAxisType.xlCategory).TickLabels.Font.Bold = True
                    axi.Item(XlAxisType.xlCategory).AxisBetweenCategories = True
                    axi.Item(XlAxisType.xlCategory).CategoryType = XlCategoryType.xlCategoryScale

                    'axi.Item(XlAxisType.xlValue).AxisTitle.Orientation = XlOrientation.xlVertical

                    track.AppendLine("Path = " & path)
                    If System.IO.File.Exists(path) Then
                        '[sebastian 03/12/2008] seteo la propiedad del archivo a normal para poder borrarlo
                        'sino caso contrario procue una exception
                        FileSystem.SetAttr(path, FileAttribute.Normal)
                        System.IO.File.Delete(path)
                    End If

                    track.AppendLine("Exportando")
                    chartExcel.Chart.Export(path, "JPG")
                Else
                    Wb.SaveAs(path)
                End If
                Wb.Saved = True

                track.AppendLine("Cerrando workbook")
                Wb.Close()

                Return True
            Catch ex As Exception
                track.AppendLine(ex.ToString())
                Return False
            Finally
                writeLog(track.ToString(), False)
                track = Nothing
                If Not IsNothing(app.ActiveWorkbook) Then
                    app.ActiveWorkbook.Close(False)
                End If
                app.Workbooks.Close()
                app.Quit()
                app = Nothing
                GC.Collect()
                Try
                    Dim xlApp As Application = Nothing
                    Try
                        xlApp = CType(GetObject(, "Excel.Application"), Application)
                    Catch
                    End Try
                    If Not IsNothing(xlApp) Then
                        If xlApp.Workbooks.Count = 0 Then
                            xlApp.Quit()
                        End If
                    End If
                    GC.Collect()
                    Dim Proc() As System.Diagnostics.Process
                    Proc = System.Diagnostics.Process.GetProcessesByName("EXCEL")
                    For Each mc As System.Diagnostics.Process In Proc
                        mc.Kill()
                    Next
                Catch ex As Exception
                End Try
                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
            End Try
        Else
            Return False
        End If
    End Function

    <WebMethod()> _
      Public Function setExcelGraphics2(ByVal ds As System.Data.DataSet, ByVal graphtype As String, ByVal path As String, ByVal save As Boolean) As String
        If Not IsNothing(ds) And String.Compare(path, String.Empty) <> 0 And String.Compare(graphtype, String.Empty) <> 0 Then
            Dim app As Application = New Application
            Dim oldCI As System.Globalization.CultureInfo = _
                    System.Threading.Thread.CurrentThread.CurrentCulture
            Try
                app.UserControl = True
                System.Threading.Thread.CurrentThread.CurrentCulture = _
                    New System.Globalization.CultureInfo("en-US")

                app.Workbooks.Add()
                Dim Wb As Workbook = app.ActiveWorkbook
                Wb.Sheets.Add()
                Dim Ws As Worksheet = Wb.ActiveSheet
                Dim range As Range
                Dim chartExcel As Chart = New Chart()
                Dim axi As Axes

                Dim i As Int32 = 1
                Dim j As Int32 = 1

                'Salvo los encabezados
                For Each column As DataColumn In ds.Tables(0).Columns
                    Ws.Cells(i, j) = column.ColumnName
                    j += 1
                Next

                i = 2
                'Salvo los datos de la grilla
                For Each Row As DataRow In ds.Tables(0).Rows
                    j = 1
                    For Each cell As Object In Row.ItemArray()
                        Ws.Cells(i, j) = cell.ToString().Replace(",", ".")
                        j += 1
                    Next
                    i += 1
                Next

                If save = False Then
                    chartExcel = Ws.ChartObjects.Add(0, 0, 600, 400)

                    'Tipo de gráfico
                    If String.Compare(graphtype.ToUpper(), "LINEAS") = 0 Then
                        chartExcel.Chart.ChartType = XlChartType.xlLine
                    ElseIf String.Compare(graphtype.ToUpper(), "AREA") = 0 Then
                        chartExcel.Chart.ChartType = XlChartType.xlArea
                    ElseIf String.Compare(graphtype.ToUpper(), "COLUMNAS") = 0 Then
                        chartExcel.Chart.ChartType = XlChartType.xlColumnClustered
                    ElseIf String.Compare(graphtype.ToUpper(), "BARRAS") = 0 Then
                        chartExcel.Chart.ChartType = XlChartType.xl3DBarStacked
                    End If

                    'rango del gráfico
                    range = Ws.Range(Ws.Cells(1, 1), Ws.Cells(i - 1, j - 2))
                    'Hacer por columnas
                    chartExcel.Chart.SetSourceData(range, XlRowCol.xlColumns)

                    'No muestra los comentarios del gráfico
                    chartExcel.Chart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowNone)
                    chartExcel.Chart.Legend.Font.Bold = True
                    axi = chartExcel.Chart.Axes(, XlAxisGroup.xlPrimary)
                    axi.Item(XlAxisType.xlCategory).TickLabels.Font.Bold = True
                    axi.Item(XlAxisType.xlCategory).AxisBetweenCategories = True
                    axi.Item(XlAxisType.xlCategory).CategoryType = XlCategoryType.xlCategoryScale

                    If System.IO.File.Exists(path) Then
                        System.IO.File.Delete(path)
                    End If

                    chartExcel.Chart.Export(path, "JPG")
                Else
                    Wb.SaveAs(path)
                End If
                Wb.Saved = True

                Wb.Close()

                Return path
            Catch ex As Exception
                Dim sr As IO.StreamWriter = New IO.StreamWriter(path)
                sr.Close()
                sr.Dispose()
                sr = Nothing
                Return ex.ToString()
            Finally
                If Not IsNothing(app.ActiveWorkbook) Then
                    app.ActiveWorkbook.Close(False)
                End If
                app.Workbooks.Close()
                app.Quit()
                app = Nothing
                GC.Collect()
                Try
                    Dim xlApp As Application = Nothing
                    Try
                        xlApp = CType(GetObject(, "Excel.Application"), Application)
                    Catch
                    End Try
                    If Not IsNothing(xlApp) Then
                        If xlApp.Workbooks.Count = 0 Then
                            xlApp.Quit()
                        End If
                    End If
                    GC.Collect()
                    Dim Proc() As System.Diagnostics.Process
                    Proc = System.Diagnostics.Process.GetProcessesByName("EXCEL")
                    For Each mc As System.Diagnostics.Process In Proc
                        mc.Kill()
                    Next
                Catch ex As Exception
                End Try
                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
            End Try
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Método que sirve para generar un reporte excel, y en base a ese reporte generar un gráfico en base al contenido del dataset
    ''' </summary>
    ''' <param name="ds">Dataset con los datos que se deben mostrar en el gráfico</param>
    ''' <param name="graphtype">Tipo de gráfico que se va a visualizar</param>
    ''' <param name="path">Camino de la imagen que muestra el gráfico</param>
    ''' <param name="save">Bandera que indica si se va a guardar o no el worksheet????</param>
    ''' <param name="bydate">Bandera que indica si hay una fecha o si es un dato????</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]  28/11/2008  Created    Código tomado del método setExcelGraphics original y adaptado para colocar nombres en los gráficos en
    '''                                      vez de números (sólo cuando corresponda). Ejemplo: el código original colocaba números en orden 
    '''                                      secuencial en el gráfico, en vez de los nombres de los usuarios. Este método lo corrige
    ''' </history>
    <WebMethod()> _
  Public Function setExcelGraphics3(ByVal ds As System.Data.DataSet, ByVal graphtype As String, ByVal path As String, ByVal save As Boolean, ByVal bydate As Boolean) As Boolean

        If Not IsNothing(ds) And String.Compare(path, String.Empty) <> 0 And String.Compare(graphtype, String.Empty) <> 0 Then

            Dim oldCI As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
            Dim track As StringBuilder = New StringBuilder()
            track.AppendLine("Ingresando a generar gráfico")
            track.AppendLine("Path: " & path)
            Dim app As Application = New Application

            Try

                app.UserControl = True
                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
                app.Workbooks.Add()
                Dim Wb As Workbook = app.ActiveWorkbook
                Wb.Sheets.Add()
                Dim Ws As Worksheet = Wb.ActiveSheet
                Dim range As Range
                Dim chartExcel As ChartObject
                Dim axi As Axes

                'Salvo los datos de la grilla
                track.AppendLine("Salvando datos")
                track.AppendLine("Cantidad de Rows: " & ds.Tables(0).Rows.Count.ToString())

                Dim r As Integer = 1
                Dim c As Integer = 1

                For Each row As DataRow In ds.Tables(0).Rows

                    For Each cell As Object In row.ItemArray()

                        If (bydate = True) Then
                            Ws.Cells(r, c) = cell.ToString().Replace(",", ".")
                        Else
                            Ws.Cells(r, c) = cell.ToString().Replace(",", ".")
                        End If

                        c += 1

                    Next

                    r += 1
                    c = 1

                Next

                track.AppendLine("Save=" & save.ToString())

                If (save = False) Then

                    track.AppendLine("Generando gráfico")
                    chartExcel = Ws.ChartObjects.Add(0, 0, 600, 400)

                    'Tipo de gráfico
                    If String.Compare(graphtype.ToUpper(), "LINEAS") = 0 Then
                        chartExcel.Chart.ChartType = XlChartType.xlLine
                    ElseIf String.Compare(graphtype.ToUpper(), "AREA") = 0 Then
                        chartExcel.Chart.ChartType = XlChartType.xlArea
                    ElseIf String.Compare(graphtype.ToUpper(), "COLUMNAS") = 0 Then
                        chartExcel.Chart.ChartType = XlChartType.xlColumnClustered
                    ElseIf String.Compare(graphtype.ToUpper(), "BARRAS") = 0 Then
                        chartExcel.Chart.ChartType = XlChartType.xl3DBarStacked
                    End If

                    'rango del gráfico
                    range = Ws.Range(Ws.Cells(1, 1), Ws.Cells(r - 1, ds.Tables(0).Columns.Count))
                    'Hacer por columnas
                    chartExcel.Chart.SetSourceData(range, XlRowCol.xlColumns)

                    ''No muestra los comentarios del gráfico
                    'chartExcel.Chart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowNone)
                    'chartExcel.Chart.Legend.Font.Bold = True
                    'axi = chartExcel.Chart.Axes(, XlAxisGroup.xlPrimary)
                    'axi.Item(XlAxisType.xlCategory).TickLabels.Font.Bold = True
                    'axi.Item(XlAxisType.xlCategory).AxisBetweenCategories = True
                    'axi.Item(XlAxisType.xlCategory).CategoryType = XlCategoryType.xlCategoryScale

                    ' Elimina la o las leyendas que puede haber en el gráfico
                    chartExcel.Chart.Legend.Clear()

                    track.AppendLine("Path = " & path)

                    If System.IO.File.Exists(path) Then
                        System.IO.File.Delete(path)
                    End If

                    track.AppendLine("Exportando")
                    chartExcel.Chart.Export(path, "JPG")

                Else
                    Wb.SaveAs(path)
                End If

                Wb.Saved = True

                track.AppendLine("Cerrando workbook")
                Wb.Close()

                Return (True)

            Catch ex As Exception
                track.AppendLine(ex.ToString())
                Return (False)
            Finally

                writeLog(track.ToString(), False)
                track = Nothing

                If Not (IsNothing(app.ActiveWorkbook)) Then
                    app.ActiveWorkbook.Close(False)
                End If

                app.Workbooks.Close()
                app.Quit()
                app = Nothing
                GC.Collect()

                Try

                    Dim xlApp As Application = Nothing

                    Try
                        xlApp = CType(GetObject(, "Excel.Application"), Application)
                    Catch
                    End Try

                    If Not (IsNothing(xlApp)) Then
                        If xlApp.Workbooks.Count = 0 Then
                            xlApp.Quit()
                        End If
                    End If

                    GC.Collect()

                    Dim Proc() As System.Diagnostics.Process
                    Proc = System.Diagnostics.Process.GetProcessesByName("EXCEL")

                    For Each mc As System.Diagnostics.Process In Proc
                        mc.Kill()
                    Next

                Catch ex As Exception
                End Try

                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI

            End Try
        Else
            Return False
        End If

    End Function
    ''' <summary>
    ''' Exporta los datos de un DataTable a un Excel
    ''' </summary>
    ''' <param name="pDatatable"></param>
    ''' <param name="sArchivo"></param>
    ''' <param name="bolShow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <History>[Ezequiel] Created  29/01/2009 - Se copio codigo del proyecto office y se modifico para que funcione en web</History>

    <WebMethod()> _
    Public Function DataTableDirectlyToExcel(ByVal pDatatable As System.Data.DataTable, ByVal sArchivo As String) As Boolean
        Dim apxls As New Excel.Application()
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
                                apxls.Cells(j, i + 1) = dr(i).ToString()
                            End If
                        End If
                        i += 1
                    Next
                End If
            Next

            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

            'Cells Backcolor White and Borders Black
            apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.White)
            apxls.Range(apxls.Cells(1, 1), apxls.Cells(j, i)).Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black)
            'Title blue and bold
            apxls.Range(apxls.Cells(1, 1), apxls.Cells(1, i)).Font.Color = System.Drawing.ColorTranslator.ToOle(Color.DodgerBlue)
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
            apxls.ActiveWorkbook.SaveAs(sArchivo)

            apxls.ActiveWorkbook.Saved = True

            apxls.ActiveWorkbook.Close()
            apxls.Workbooks.Close()
            apxls.Quit()
            apxls = Nothing
            GC.Collect()
            Return True
        Catch ex As Exception
            apxls.Workbooks.Close()
            apxls.Quit()
            apxls = Nothing
            Return False
        End Try
    End Function

    Private Sub writeLog(ByVal message As String, ByVal bolEx As Boolean)
        Try
            Dim path As String = System.Web.Configuration.WebConfigurationManager.AppSettings("ExportPath")
            If (path.EndsWith("\\") = False) Then
                path += "\\"
            End If
            path += "Trace.txt"
            Dim writer As System.IO.StreamWriter = New System.IO.StreamWriter(path)
            writer.Write(message)
            writer.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class