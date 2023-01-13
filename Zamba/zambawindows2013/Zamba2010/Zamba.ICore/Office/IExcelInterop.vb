Public Enum eEstilosHoja
    SinFormato = -4142 'Excel.XlRangeAutoFormat.xlRangeAutoFormatNone
    Simple = -4154 'Excel.XlRangeAutoFormat.xlRangeAutoFormatSimple
    Clasico1 = 1 'Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1
    Clasico2 = 2 'Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2
    Clasico3 = 3 'Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic3
    Contable1 = 4 'Excel.XlRangeAutoFormat.xlRangeAutoFormatAccounting1
    Contable2 = 5 'Excel.XlRangeAutoFormat.xlRangeAutoFormatAccounting2
    Contable3 = 6 'Excel.XlRangeAutoFormat.xlRangeAutoFormatAccounting3
    Contable4 = 17 'Excel.XlRangeAutoFormat.xlRangeAutoFormatAccounting4
    Coloreado1 = 7 'Excel.XlRangeAutoFormat.xlRangeAutoFormatColor1
    Coloreado2 = 8 'Excel.XlRangeAutoFormat.xlRangeAutoFormatColor2
    Coloreado3 = 9 'Excel.XlRangeAutoFormat.xlRangeAutoFormatColor3
    Estilo1 = 10 'Excel.XlRangeAutoFormat.xlRangeAutoFormatList1
    Estilo2 = 11 'Excel.XlRangeAutoFormat.xlRangeAutoFormatList2
    Estilo3 = 12 'Excel.XlRangeAutoFormat.xlRangeAutoFormatList3
    Efecto3D1 = 13 'Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects1
    Efecto3D2 = 14 'Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2
    TablaPivot = 31 'Excel.XlRangeAutoFormat.xlRangeAutoFormatClassicPivotTable
    FormatoLocal1 = 15 'Excel.XlRangeAutoFormat.xlRangeAutoFormatLocalFormat1
    FormatoLocal2 = 16 'Excel.XlRangeAutoFormat.xlRangeAutoFormatLocalFormat2
    FormatoLocal3 = 19 'Excel.XlRangeAutoFormat.xlRangeAutoFormatLocalFormat3
    FormatoLocal4 = 20 'Excel.XlRangeAutoFormat.xlRangeAutoFormatLocalFormat4
    PTNinguno = 42 'Excel.XlRangeAutoFormat.xlRangeAutoFormatPTNone
    Reporte1 = 21 'Excel.XlRangeAutoFormat.xlRangeAutoFormatReport1
    Reporte10 = 30 'Excel.XlRangeAutoFormat.xlRangeAutoFormatReport10
    Reporte2 = 22 'Excel.XlRangeAutoFormat.xlRangeAutoFormatReport2
    Reporte3 = 23 'Excel.XlRangeAutoFormat.xlRangeAutoFormatReport3
    Reporte4 = 24 'Excel.XlRangeAutoFormat.xlRangeAutoFormatReport4
    Reporte5 = 25 'Excel.XlRangeAutoFormat.xlRangeAutoFormatReport5
    Reporte6 = 26 'Excel.XlRangeAutoFormat.xlRangeAutoFormatReport6
    Reporte7 = 27 'Excel.XlRangeAutoFormat.xlRangeAutoFormatReport7
    Reporte8 = 28 'Excel.XlRangeAutoFormat.xlRangeAutoFormatReport8
    Reporte9 = 29 'Excel.XlRangeAutoFormat.xlRangeAutoFormatReport9
    Tabla1 = 32 'Excel.XlRangeAutoFormat.xlRangeAutoFormatTable1
    Tabla10 = 41 'Excel.XlRangeAutoFormat.xlRangeAutoFormatTable10
    Tabla2 = 33 'Excel.XlRangeAutoFormat.xlRangeAutoFormatTable2
    Tabla3 = 34 'Excel.XlRangeAutoFormat.xlRangeAutoFormatTable3
    Tabla4 = 35 'Excel.XlRangeAutoFormat.xlRangeAutoFormatTable4
    Tabla5 = 36 'Excel.XlRangeAutoFormat.xlRangeAutoFormatTable5
End Enum

Public Interface IExcelInterop

    Property ProcesoExcel As Process

    Sub DataTableToExcel(ByVal pDataTable As DataTable, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja)
    Sub DataSetToExcel(ByVal pDataSet As DataSet, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja)
    Function DataTableDirectlyToExcelWithHL(ByVal pDatatable As DataTable, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja, ByVal dtPath As DataTable, Optional ByVal bolShow As Boolean = False, Optional ByVal ForceCulture As Boolean = False) As Boolean
    Sub imprimirPlanilla(ByVal sArchivo As String, ByVal PreferenciasImpresion As Printing.PrinterSettings, Optional ByVal showPreview As Boolean = False)
    Sub abrirArchivoPlanilla(ByVal sArchivo As String)
    Overloads Function UCResultGridAExcel(ByRef ZExtendedTreeView As Object, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja) As Boolean
    Sub DataTableDirectlyToPrint(ByVal pDataTable As DataTable, ByVal eEstilo As eEstilosHoja, ByVal PreferenciasImpresion As Printing.PrinterSettings)

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
    'Public  Function DataTableDirectlyToExcel(ByVal pDatatable As DataTable, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja, Optional ByVal bolShow As Boolean = False) As Boolean
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
    '        apxls.Range(apxls.Cells(1, 1), apxls.Cells(1, i)).Font.Color = System.Drawing.ColorTranslator.ToOle(Color.DodgerBlue)
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
    Function DataTableDirectlyToExcel(ByVal pDatatable As DataTable, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja, Optional ByVal bolShow As Boolean = False, Optional ByVal ForceCulture As Boolean = False) As Boolean

    Function DataTableDirectlyToExcel(ByVal addcol As Boolean, ByVal pDatatable As DataTable, ByVal sArchivo As String, ByVal eEstilo As eEstilosHoja, Optional ByVal bolShow As Boolean = False, Optional ByVal tipe As Int16 = -4143) As Boolean
    Sub TextToExcel(ByVal sArchOrg As String, ByVal pFileName As String, ByVal eEstilo As eEstilosHoja)

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
    Sub GenerateExcelDoc(ByVal title As String, ByVal header As ArrayList, ByVal body As ArrayList, ByVal footer As String, ByVal sumPos As ArrayList, ByVal countPos As ArrayList)

    ''' <summary>
    ''' Hace la copia de un archivo de excel a la direccion especificada por el usuario
    ''' </summary>
    ''' <param name="path">Path del archivo original</param>
    ''' <remarks></remarks>
    Sub SaveAsExcel(ByVal path As String)

    Sub TextToExcelNew(ByVal sArchOrg As String, ByVal pFileName As String, ByVal eEstilo As eEstilosHoja)
    Function setExcelGraphics(ByVal dt As System.Data.DataTable, ByVal graphtype As String, ByVal path As String, ByVal save As Boolean, ByVal bydate As Boolean) As Boolean
End Interface

