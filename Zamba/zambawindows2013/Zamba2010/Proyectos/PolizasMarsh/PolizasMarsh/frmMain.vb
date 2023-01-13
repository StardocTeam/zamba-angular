Imports System.IO
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.Office.Interop.Excel
Imports System.Threading.Thread
Imports System.Globalization
Imports Zamba.Servers

Public Class frmMain

    Private docTypeId As Int64 = 0
    Private indexId As Int64 = 0
    Private excelColumn As String = String.Empty
    Private excelPath As String = String.Empty
    Private folderPath As String = String.Empty
    Private configPath As String
    Private listFileNotFound As New List(Of String)
    Private listDocNotFound As New List(Of String)
    Private listMultipleDocs As New List(Of String)
    Private query As String
    Private tempPath As System.Data.DataTable
    Private filePath As String
    Private processed As Int32 = 0

    Private ExcelApp As Application = Nothing
    Private ExcelDoc As Workbook = Nothing
    Private ExcelBooks As Workbooks = Nothing
    Private ExcelSheet As Worksheet = Nothing
    Private ExcelRange As Range = Nothing


    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = 140

        Try
            Dim executionPath As String = My.Application.Info.DirectoryPath & "\Ejecucion.txt"

            'Si el archivo existe se borra el contenido
            If File.Exists(executionPath) Then
                Using sw As New StreamWriter(executionPath, False)
                    sw.Write(String.Empty)
                    sw.Close()
                End Using
            End If

            'Se agrega un log para guardar la ejecución del aplicativo
            Trace.Listeners.Add(New TextWriterTraceListener(executionPath))
            Trace.AutoFlush = True
        Catch
        End Try

        Trace.WriteLine("Inicio de ejecución: " & DateTime.Now.ToString())

        'Ruta del archivo que contiene la configuración
        configPath = My.Application.Info.DirectoryPath & "\config.ini"

        Try
            Trace.WriteLine("Verificando la existencia del archivo de configuración: " & configPath)
            If File.Exists(configPath) Then
                Trace.WriteLine("EXISTE. Obteniendo la configuración...")
                Dim configData As String()
                Dim separator As Char() = {Char.Parse("=")}
                Using config As New StreamReader(configPath, System.Text.Encoding.UTF8)
                    While (Not config.EndOfStream)
                        Try
                            'Se obtiene la clave de la opcion y su valor
                            configData = config.ReadLine().Split(separator, System.StringSplitOptions.RemoveEmptyEntries)
                            If configData.Length = 2 Then
                                Trace.WriteLine("Cargando " & configData(0))
                                'Se guarda en variables lo obtenido
                                Select Case configData(0)
                                    Case "docTypeId"
                                        docTypeId = Int64.Parse(configData(1))
                                        Trace.WriteLine("docTypeId CARGADO: " & docTypeId.ToString)
                                    Case "indexId"
                                        indexId = Int64.Parse(configData(1))
                                        Trace.WriteLine("indexId CARGADO: " & indexId.ToString)
                                    Case "excelColumn"
                                        excelColumn = configData(1)
                                        Trace.WriteLine("excelColumn CARGADO: " & excelColumn)
                                    Case "excelPath"
                                        txtExcelPath.Text = configData(1)
                                        Trace.WriteLine("excelPath CARGADO: " & txtExcelPath.Text)
                                    Case "folderPath"
                                        txtFolderPath.Text = configData(1)
                                        Trace.WriteLine("folderPath CARGADO: " & txtFolderPath.Text)
                                End Select
                            ElseIf configData.Length = 1 Then
                                Trace.WriteLine("La opción " & configData(0) & " todavía no tiene valor.")
                            End If
                        Catch ex As Exception
                            Trace.WriteLine("ERROR")
                            Trace.WriteLine(ex.Message)
                        End Try
                    End While
                    config.Close()
                End Using
            Else
                Trace.WriteLine("NO EXISTE")
            End If
        Catch ex As Exception
            Trace.WriteLine("ERROR")
            Trace.WriteLine(ex.Message)
            MessageBox.Show("Error al cargar la configuración del usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnGetExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetExcel.Click
        LoadExcelFile()
    End Sub
    Private Sub btnGetFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFolder.Click
        LoadFolderPath()
    End Sub
    Private Sub btnOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOptions.Click
        ShowOptions()
    End Sub
    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click

        'Valida que los parámetros de configuración se encuentren completos
        If docTypeId = 0 OrElse indexId = 0 OrElse String.IsNullOrEmpty(excelColumn) Then
            MessageBox.Show("Verifique que el tipo de documento, el índice y el nombre de columna del excel se encuentren completos", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ShowOptions()
            Exit Sub
        ElseIf String.IsNullOrEmpty(txtExcelPath.Text) Then
            MessageBox.Show("Debe seleccionar un documento excel para continuar", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LoadExcelFile()
            Exit Sub
        ElseIf String.IsNullOrEmpty(txtFolderPath.Text) Then
            MessageBox.Show("Debe seleccionar una carpeta destino de exportación para continuar", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LoadFolderPath()
            Exit Sub
        ElseIf Not File.Exists(txtExcelPath.Text) Then
            MessageBox.Show("El documento excel no existe", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LoadExcelFile()
            Exit Sub
        ElseIf Not Directory.Exists(txtFolderPath.Text) Then
            MessageBox.Show("La carpeta destino no existe", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LoadFolderPath()
            Exit Sub
        End If

        'Ahora que la información es válida se guarda en el archivo de configuración
        SaveConfigFile()

        'Se deshabilitan los controles
        Me.Height = 200
        btnExit.Enabled = False
        btnGetExcel.Enabled = False
        btnGetFolder.Enabled = False
        btnOptions.Enabled = False
        btnStart.Enabled = False
        System.Windows.Forms.Application.DoEvents()

        'Se guardan los valores en variables para que se puedan acceder desde los hilos
        excelPath = txtExcelPath.Text
        folderPath = txtFolderPath.Text

        'Comienza el procesamiento del excel
        Worker.RunWorkerAsync(New Object)
    End Sub

    Private Sub Worker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        Trace.WriteLine(vbCrLf & "===========================================================================================" & vbCrLf)
        Trace.WriteLine("Comienza el procesamiento del EXCEL. Los parámetros configurados son los siguientes:" & vbCrLf)
        Trace.WriteLine("docTypeId=" & docTypeId.ToString)
        Trace.WriteLine("indexId=" & indexId.ToString)
        Trace.WriteLine("excelColumn=" & excelColumn)
        Trace.WriteLine("excelPath=" & txtExcelPath.Text)
        Trace.WriteLine("folderPath=" & txtFolderPath.Text)
        Trace.WriteLine(vbCrLf & "===========================================================================================" & vbCrLf)

        Try
            'Se obtiene la aplicación
            ExcelApp = New Application()
            ExcelApp.Visible = False

            'Se obtienen los workbooks
            ExcelBooks = ExcelApp.Workbooks

            'Se obtiene el excel
            Try
                ExcelDoc = ExcelBooks.Open(txtExcelPath.Text)
            Catch ex As Exception
                CurrentThread.CurrentCulture = New CultureInfo("en-US")
                ExcelDoc = ExcelBooks.Open(excelPath)
            End Try

            If ExcelDoc IsNot Nothing Then
                'Se obtiene la columna que contiene el índice clave
                ExcelSheet = ExcelDoc.Sheets(1)
                ExcelRange = ExcelSheet.UsedRange
                Dim colI As Int32
                For colI = 1 To ExcelRange.Columns.Count
                    If String.Compare(ExcelRange.Value2(1, colI).ToString(), excelColumn, True) = 0 Then
                        Trace.WriteLine("La columna a encontrar se encontraba en la posición " & colI.ToString())
                        Exit For
                    End If
                Next

                'Verifica haber encontrado la columna
                If colI <= ExcelRange.Columns.Count Then

                    'Se recorren las filas obteniendo el valor de cada celda
                    Dim rowI As Int32
                    For rowI = 2 To ExcelRange.Rows.Count
                        ProcessValue(ExcelRange.Value2(rowI, colI))
                    Next

                Else
                    MessageBox.Show("No se pudo encontrar la columna especificada")
                End If
            End If

        Catch ex As Exception
            Trace.WriteLine("ERROR")
            Trace.WriteLine(ex.Message)
            MessageBox.Show("Error en el procesamiento del excel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DisposeExcelResources()
        End Try
    End Sub
    Private Sub Worker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles Worker.ProgressChanged
        Select Case e.ProgressPercentage
            Case 1 'Procesado correctamente
                lblOk.Text = Int32.Parse(lblOk.Text) + 1
            Case 2 'Falta el archivo en el volumen
                lblFileNotFound.Text = Int32.Parse(lblFileNotFound.Text) + 1
                listFileNotFound.Add(e.UserState)
            Case 3 'No se encontraron documentos con la clave de búsqueda
                lblDocNotFound.Text = Int32.Parse(lblDocNotFound.Text) + 1
                listDocNotFound.Add(e.UserState)
            Case 4 'Valores mal escritos en excel
                lblBadNumbers.Text = Int32.Parse(lblBadNumbers.Text) + 1
        End Select
    End Sub
    Private Sub Worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        Trace.WriteLine(vbCrLf & "Fin del procesamiento del excel" & vbCrLf)
        Dim traceFileName As String = My.Application.Info.DirectoryPath & "\Resultados " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".txt"

        Try
            'Se agrega un segundo log, pero este será unicamente de resultados y será mostrado al usuario
            Trace.Listeners.Add(New TextWriterTraceListener(traceFileName))
            Trace.AutoFlush = True
        Catch
        End Try

        'Se escribe el reporte
        Trace.WriteLine("===========================================================================================")
        Trace.WriteLine("Resultados obtenidos del procesamiento del excel: " & txtExcelPath.Text)
        Trace.WriteLine("===========================================================================================")
        Trace.WriteLine(vbCrLf & "Archivos totales copiados: " & lblOk.Text)
        Trace.WriteLine(vbCrLf & "Archivos físicos no encontrados en volumen: " & lblFileNotFound.Text)
        If listFileNotFound.Count > 0 Then
            For Each doc As String In listFileNotFound
                Trace.WriteLine(vbTab & doc)
            Next
            Trace.WriteLine(String.Empty)
        End If
        Trace.WriteLine(vbCrLf & "Documentos en Zamba no encontrados: " & lblDocNotFound.Text)
        If listDocNotFound.Count > 0 Then
            For Each doc As String In listDocNotFound
                Trace.WriteLine(vbTab & doc)
            Next
            Trace.WriteLine(String.Empty)
        End If
        Trace.WriteLine(vbCrLf & "Documentos múltiples encontrados: " & listMultipleDocs.Count)
        If listMultipleDocs.Count > 0 Then
            For Each doc As String In listMultipleDocs
                Trace.WriteLine(vbTab & doc)
            Next
            Trace.WriteLine(String.Empty)
        End If
        Trace.WriteLine(vbCrLf & "Valores mal escritos en el documento excel: " & lblBadNumbers.Text)

        'Cierra el log de resultados y lo abre
        Trace.Close()
        If File.Exists(traceFileName) Then
            Try
                Process.Start(traceFileName)
            Catch ex As Exception
            End Try
        End If

        btnExit.Enabled = True
    End Sub

    ''' <summary>
    ''' Muestra la ventana de opciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowOptions()
        Dim form As frmConfig = Nothing
        Try
            form = New frmConfig(docTypeId, indexId, excelColumn)
            If form.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                'Se actualiza el valor de las variables
                docTypeId = form.cmbDocType.SelectedValue
                indexId = form.cmbIndex.SelectedValue
                excelColumn = form.txtExcelColumn.Text
                'Se guardan las opciones actualizadas
                SaveConfigFile()
            End If
        Catch ex As Exception
            Trace.WriteLine("ERROR")
            Trace.WriteLine(ex.Message)
            MessageBox.Show("Error en las opciones del programa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If form IsNot Nothing Then
                form.Dispose()
                form = Nothing
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Abre un cuadro de selección de archivo para adjuntar un excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadExcelFile()
        Try
            Using fileDlg As New OpenFileDialog
                fileDlg.AddExtension = True
                fileDlg.CheckFileExists = True
                fileDlg.CheckPathExists = True
                fileDlg.DefaultExt = ".xls"
                fileDlg.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
                fileDlg.Multiselect = False
                fileDlg.RestoreDirectory = False
                fileDlg.ShowHelp = False
                fileDlg.Title = "Seleccione un documento Excel"

                If fileDlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    txtExcelPath.Text = fileDlg.FileName
                End If
            End Using
        Catch ex As Exception
            Trace.WriteLine("ERROR")
            Trace.WriteLine(ex.Message)
            MessageBox.Show("Error al cargar la ubicación del excel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub LoadFolderPath()
        Try
            Using folderDlg As New FolderBrowserDialog
                folderDlg.Description = "Seleccione la carpeta destino de las pólizas exportadas"
                folderDlg.RootFolder = Environment.SpecialFolder.Desktop
                folderDlg.ShowNewFolderButton = True

                If folderDlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    txtFolderPath.Text = folderDlg.SelectedPath
                End If
            End Using
        Catch ex As Exception
            Trace.WriteLine("ERROR")
            Trace.WriteLine(ex.Message)
            MessageBox.Show("Error al cargar la ubicación del excel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub SaveConfigFile()
        Try
            Using config As New StreamWriter(configPath, False, System.Text.Encoding.UTF8)
                Dim sb As New StringBuilder()
                sb.Append("docTypeId=")
                sb.AppendLine(docTypeId)
                sb.Append("indexId=")
                sb.AppendLine(indexId)
                sb.Append("excelColumn=")
                sb.AppendLine(excelColumn)
                sb.Append("excelPath=")
                sb.AppendLine(txtExcelPath.Text)
                sb.Append("folderPath=")
                sb.Append(txtFolderPath.Text)

                config.Write(sb.ToString)
                sb = Nothing
                config.Flush()
                config.Close()
            End Using
        Catch ex As Exception
            Trace.WriteLine("ERROR")
            Trace.WriteLine(ex.Message)
            MessageBox.Show("Error al guardar las opciones del usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ProcessValue(ByVal indexValue As Object)
        If indexValue IsNot Nothing AndAlso IsNumeric(indexValue) Then
            'Se obtiene la ruta del documento a buscar
            If Server.isOracle Then
                query = "select dv.disk_vol_path || '\' || " & docTypeId.ToString & " || '\' || cast(dt.offset as varchar2(30)) || '\' || dt.doc_file " & _
                "from disk_volume dv inner join doc_t" & docTypeId.ToString & " dt on dv.disk_vol_id=dt.vol_id " & _
                "inner join doc_i" & docTypeId.ToString & " di on dt.doc_id=di.doc_id where di.i" & indexId.ToString & "=" & indexValue
            Else
                query = "select dv.disk_vol_path + '\" & docTypeId.ToString & "\' + cast(dt.offset as varchar) + '\' + dt.doc_file " & _
                "from disk_volume dv inner join doc_t" & docTypeId.ToString & " dt on dv.disk_vol_id=dt.vol_id " & _
                "inner join doc_i" & docTypeId.ToString & " di on dt.doc_id=di.doc_id where di.i" & indexId.ToString & "=" & indexValue
            End If

            'Se obtiene la ubicación del archivo
            tempPath = Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)

            'Verifica que existan archivos a copiar
            If tempPath.Rows.Count > 0 Then

                'Se copia el archivo solamente si es único o si es multiple y no se copio anteriormente
                If tempPath.Rows.Count = 1 OrElse Not listMultipleDocs.Contains(indexValue) Then
                    Dim filePath As String

                    For i As Int32 = 0 To tempPath.Rows.Count - 1
                        filePath = tempPath.Rows(i).Item(0).ToString.Trim

                        If File.Exists(filePath) Then
                            'Se realiza la copia del documento
                            File.Copy(filePath, GetUniqueFileName(folderPath, indexValue.ToString, Path.GetExtension(filePath.ToString)))
                            Worker.ReportProgress(1)
                        Else
                            'La ruta encontrada no existe
                            Worker.ReportProgress(2, indexValue)
                        End If
                    Next

                    If tempPath.Rows.Count > 1 Then
                        listMultipleDocs.Add(indexValue)
                    End If
                End If

            Else
                'No se encontró el registro en Zamba
                Worker.ReportProgress(3, indexValue)
            End If
        Else
            'El valor del excel se encontraba erroneo
            Worker.ReportProgress(4)
        End If
    End Sub
    Private Sub DisposeExcelResources()
        Try
            ExcelRange = Nothing
            ExcelSheet = Nothing

            'Se libera el documento
            If ExcelDoc IsNot Nothing Then
                Try
                    ExcelDoc.Close()
                Catch
                End Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelDoc)
                ExcelDoc = Nothing
            End If

            'Se libera el temporal
            If ExcelBooks IsNot Nothing Then
                Try
                    ExcelBooks.Close()
                Catch
                End Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelBooks)
                ExcelBooks = Nothing
            End If

            'Si no queda ningun workbook abierto cierro tambien el excel
            If Not IsNothing(ExcelApp) Then
                ExcelApp.Quit()
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp)
            End If
        Catch ex As Exception
            Trace.WriteLine("ERROR")
            Trace.WriteLine(ex.Message)
        End Try
    End Sub
    Public Shared Function GetUniqueFileName(ByVal filePath As String, ByVal fileName As String, ByVal fileExtension As String) As String
        Dim identifier As String = String.Empty
        Dim cont As Int16 = 0

        'Valida que la extensión contenga el punto
        If Not fileExtension.StartsWith(".") AndAlso Not fileName.EndsWith(".") Then
            fileExtension.Insert(0, ".")
        End If

        'Valida que entre el path y el nombre del archivo exista una barra
        If Not filePath.EndsWith("\") AndAlso Not fileName.StartsWith("\") Then
            filePath &= "\"
        End If

        'Valida que el archivo no exista en el directorio. 
        'En caso de existir le agrega un contador entre paréntesis.
        While IO.File.Exists(filePath & fileName & identifier & fileExtension)
            identifier = "(" & cont.ToString & ")"
            cont += 1
        End While

        fileName = fileName & identifier
        Return (filePath & fileName & fileExtension)
    End Function

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        System.Windows.Forms.Application.Exit()
    End Sub
End Class
