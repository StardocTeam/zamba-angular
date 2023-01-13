Imports Zamba.AppBlock
Imports Zamba.Core.ProcessHistories
Imports Zamba.Servers
'Imports ZAMBA.Controls
Imports System.Text
Imports System.IO

Public Class ProcessEngine
    Inherits ZClass

    Public Sub New()
        MyBase.new()
    End Sub
    Public Overrides Sub Dispose()

    End Sub


    Private Process As Process
    Private Document As NewResult

    Private TempVolFiles As Int32
    Public FlagGoOn As Boolean = True
    Dim ActualLine As Int32

    Enum CounterUpdates
        LineCounted
        LineImported
        LineSkiped
        ErrorLineImported
        ErrorLineFailed
        ErrolineFound
    End Enum

#Region "InitialProcess"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Process"></param>
    ''' <history>
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' <remarks></remarks>
    Public Sub InitialProcess(ByRef Process As Process)
        Me.Process = Process
        Try
            StartProcess()

            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(Process.ID, ObjectTypes.ModuleImport, RightsType.Execute, "Ejecutando el proceso: " & Process.Name & "(" & Process.ID & ")")
            ProcessFinalized()
        Catch ex As System.ExecutionEngineException
            raiseerror(ex)
        Catch ex As System.NullReferenceException
            raiseerror(ex)
        Catch ex As Threading.ThreadAbortException
            raiseerror(ex)
        Catch ex As Threading.ThreadInterruptedException
            raiseerror(ex)
        Catch ex As Threading.ThreadStateException
            raiseerror(ex)
        Catch ex As Threading.SynchronizationLockException
            raiseerror(ex)
        Catch ex As Exception
            raiseerror(ex)
            RaiseEvent LogErrorMsg(ex.ToString)
        End Try
    End Sub
#End Region


#Region "ProcessEvents"
    Public Event LogMsg(ByVal Msg As String)
    Public Event LogErrorMsg(ByVal Msg As String)
    Public Event LogCounterChanged(ByVal CounterType As CounterTypes, ByVal Count As Int32)
    Public Event LogProcessFinalized(ByVal Msg As String)
    Private Sub EventOCurred(ByVal Msg As String)
        Try
            WriteLog(Msg)
            Process.History.LogList.Add(Msg)
            '       Advice.LogEvents(Msg)
            RaiseEvent LogMsg(Msg)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ErrorOcurred(ByVal msg As String)
        Try
            WriteLog(msg)
            Process.History.ErrorList.Add(msg)
            ' Advice.LogEvents(msg)
            RaiseEvent LogErrorMsg(msg)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    'Private Sub CounterChanged(ByVal CounterType As ProcessHistories.CounterTypes, ByVal Count As Int32)
    '    '    Advice.CounterChanged(CounterType, Count)
    '    RaiseEvent LogCounterChanged(CounterType, Count)
    'End Sub
    Private Sub ProcessFinalized()
        Try
            RaiseEvent LogProcessFinalized("Proceso Finalizado")
        Catch ex As Threading.ThreadAbortException
            raiseerror(ex)
        Catch ex As Threading.SynchronizationLockException
            raiseerror(ex)
        Catch ex As Threading.ThreadInterruptedException
            raiseerror(ex)
        Catch ex As Threading.ThreadStateException
            raiseerror(ex)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub
    ' Private LinesCount As Int32
    'Private Increment As Decimal
    'Private IncrementCount As Integer
    Private Sub WriteLog(ByVal Msg As String)
        Try
            Dim LogWriter As New StreamWriter(Process.History.LOGFILE, True, Text.Encoding.Default)
            Try
                LogWriter.AutoFlush = True
                LogWriter.WriteLine(Msg)
            Catch ex As Exception
                zamba.core.zclass.raiseerror(ex)
            Finally
                LogWriter.Close()
                LogWriter = Nothing
                GC.Collect()
            End Try
        Catch
        End Try
    End Sub
#End Region

#Region "Startprocess"
    Private Sub StartProcess()
        Try
            EventOCurred("Proceso: " & Process.Name)

            'Instancio si es necesario el historial
            If InstanceHistory() = False Then Exit Sub
            EventOCurred("Comienzo de Proceso: " & Process.History.Process_Date.ToString("dd-MM-yyyy HH-mm-ss"))
            Try
                EventOCurred("Solicitando Archivo de Atributos")
                GetFileToProcess()
            Catch ex As Exception
                raiseerror(ex)
                ErrorOcurred(ex.ToString)
                Exit Sub
            End Try
            'Hago BackUp de los archivos si corresponde
            Try
                BackUpFiles()
            Catch ex As Exception
                If MessageBox.Show("Ha ocurrido una excepcion en el preproceso. Desea tratar de reintentar el proceso?", "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Try
                        BackUpFiles()
                    Catch ex2 As Exception
                        raiseerror(ex)
                        MessageBox.Show("Error al ejecutar el proceso. Proceso finalizado sin ejecutar." & ControlChars.NewLine & ex.ToString)
                        ErrorOcurred(ex.ToString)
                        Exit Sub
                    End Try
                Else
                    MessageBox.Show(ex.ToString)
                    ErrorOcurred(ex.ToString)
                    Exit Sub
                End If
            End Try
            'creo el temporal para trabajar
            Try
                CreateTempFile()
            Catch ex As Exception
                ErrorOcurred(ex.ToString)
                Exit Sub
            End Try
            'Chequeo que el archivo no se haya ejecutado anteriormente
            If CheckFileHash() = False Then Exit Sub
            'Creo el Documento
            Try
                CreateDocument()
            Catch ex As Exception
                ErrorOcurred(ex.ToString)
                Exit Sub
            End Try
            'Obtengo los Datos de los atributos del  Proceso
            If GetprocessIndexData() = False Then Exit Sub

            'Elimino si corresponde el archivo de origen
            Try
                DeleteOriginalIndexFile()
            Catch ex As Exception
                ErrorOcurred(ex.ToString)
            End Try

            'corro el preproceso
            Dim Files As New ArrayList
            Try
                Files = PreProcess

                'Dim i As Int32
                'For i = 0 To Files.Count - 1
                '    ZTrace.WriteLineIf(ZTrace.IsInfo,Files.Item(i))
                'Next
            Catch ex As Exception
                ErrorOcurred(ex.ToString)
                Exit Sub
            End Try

            'corro el proceso final
            Try
                FinalProcess(Files)
            Catch ex As Exception
                ErrorOcurred(ex.ToString)
                Exit Sub
            End Try

            'corro el postproceso
            Try
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando PostProcesos")
                PostProcessEjecutados = Process_Business.PostProcess(Files, Process.ID)
            Catch ex As Exception
                ErrorOcurred(ex.ToString)
            End Try
        Finally
            Trace.Listeners.Clear()
            Trace.Flush()
        End Try
        FinalMessages()
    End Sub
    Dim PostProcessEjecutados As New ArrayList
    'Dim OldHistoryIndex As Int32
    Private Function InstanceHistory() As Boolean
        Try
            Select Case Process.type
                Case ProcessTypes.Common
                    Process.History.Histories.Tables(0).Rows.Add(ProcessHistories.NewHistory(Process))
                    Process.History.Histories.AcceptChanges()
                    Process.History.HistoryIndex = Process.History.Histories.Tables(0).Rows.Count - 1
                    ProcessHistories.SaveHistory(Process.History)
                Case ProcessTypes.ProcessErrors
                    ' Me.OldHistoryIndex = Process.History.HistoryIndex
                    ' Process.History.Histories.ProcessHistory.Rows.Add(ProcessHistories.NewHistory(Process))
                    ' Process.History.Histories.AcceptChanges()
                    '   Process.History.HistoryIndex = Process.History.Histories.ProcessHistory.Count - 1
                    '  Process.History.Path = Process.History.Histories.ProcessHistory(OldHistoryIndex).Path.Substring(0, Process.History.Histories.ProcessHistory(OldHistoryIndex).Path.LastIndexOf(".")) & " ERROR.TXT"
                    '  ProcessHistories.SaveHistory(Process.History)
                Case ProcessTypes.Test
                    Process.History.Histories.Tables(0).Rows.Add(ProcessHistories.NewHistory(Process))
                    Process.History.Histories.AcceptChanges()
                    Process.History.HistoryIndex = Process.History.Histories.Tables(0).Rows.Count - 1
                    ProcessHistories.SaveHistory(Process.History)
                Case ProcessTypes.ContinueProcess
                    '  Process.History.Histories.ProcessHistory.Rows.Add(ProcessHistories.NewHistory(Process))
                    '    Process.History.Histories.AcceptChanges()
                    '      Process.History.HistoryIndex = Process.History.Histories.ProcessHistory.Count - 1
                    '     ProcessHistories.SaveHistory(Process.History)
            End Select
            Return True
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
            ' Throw New Exception("Error al instanciar el historial " & ex.ToString)
            Return True
        End Try
    End Function
    Private Sub GetFileToProcess()
        Select Case Process.type
            Case ProcessTypes.Common
                If Process.FlagSourceVariable = True Then
                    Try
                        Process.History.Path = OpenFileDialog()
                    Catch ex As Exception
                        Process.History.Result = Zamba.Core.Results.Erroneo
                        Throw ex
                    End Try
                Else
                    Process.History.Path = Process.Path
                End If
                CheckFiles(Process.History.Path)
                EventOCurred("Archivo de Atributos: " & Process.History.Path)
            Case ProcessTypes.ProcessErrors
                CheckFiles(Process.History.ERRORFILE)
                EventOCurred("Archivo de Atributos: " & Process.History.ERRORFILE)
            Case ProcessTypes.ContinueProcess
                CheckFiles(Process.History.TEMPFILE)
                EventOCurred("Archivo de Atributos: " & Process.History.TEMPFILE)
            Case ProcessTypes.Test
                If Process.FlagSourceVariable = True Then
                    Try
                        Process.History.Path = OpenFileDialog()
                    Catch ex As Exception
                        Process.History.Result = Zamba.Core.Results.Erroneo
                        Throw ex
                    End Try
                Else
                    Process.History.Path = Process.Path
                End If
                CheckFiles(Process.History.Path)
                EventOCurred("Archivo de Atributos: " & Process.History.Path)
        End Select
    End Sub
    Private Shared Function OpenFileDialog() As String
        Dim FileDialog As New System.Windows.Forms.OpenFileDialog
        Try
            'TODO Falta restringir las extensiones y poner el directorio default el del proceso
            FileDialog.ShowDialog()
            If FileDialog.FileName = "" Or IsNothing(FileDialog.FileName) Then
                Throw New Exception("El archivo de Atributos no es correcto.")
            Else
                If New FileInfo(FileDialog.FileName.Trim).Exists Then
                    Return FileDialog.FileName.Trim
                Else
                    Throw New Exception("El archivo de Atributos no es correcto: " & FileDialog.FileName)
                End If
            End If
        Finally
            FileDialog.Dispose()
            FileDialog = Nothing
        End Try
    End Function
    Private Shared Sub CheckFiles(ByVal File As String)
        If New FileInfo(File).Exists = False Then Throw New Exception("El Archivo especificado no existe: " & File)
    End Sub
    Private Sub BackUpFiles()
        If Process.type <> ProcessTypes.ProcessErrors AndAlso Process.type <> ProcessTypes.ContinueProcess Then
            Try
                Dim IPPREPROCESS As New PreProcessFactory
                RemoveHandler IPPREPROCESS.PreProcessMessage, AddressOf EventOCurred
                RemoveHandler IPPREPROCESS.PreprocessError, AddressOf ErrorOcurred
                AddHandler IPPREPROCESS.PreProcessMessage, AddressOf EventOCurred
                AddHandler IPPREPROCESS.PreprocessError, AddressOf ErrorOcurred
                IPPREPROCESS.preprocessFile("BackUpFiles", Process.History.Path, Process.BackUpPath & "," & Process.Name & "," & Process.History.Process_Date.ToString("dd-MM-yyyy hh-mm-ss"))
            Catch ex As Exception
                Process.History.Result = Zamba.Core.Results.Erroneo
                ErrorOcurred("Error al realizar el backup: " & ex.ToString)
                Throw New Exception("Ocurrio un error al ejecutar los preprocesos " & ex.ToString)
            End Try
        End If
    End Sub
    Private Sub CreateTempFile()
        Try
            Select Case Process.type
                Case ProcessTypes.Common
                    Dim FiOriginal As New FileInfo(Process.History.Path)
                    FiOriginal.CopyTo(Process.History.TEMPFILE, True)
                Case ProcessTypes.Test
                    Dim FiOriginal As New FileInfo(Process.History.Path)
                    FiOriginal.CopyTo(Process.History.TEMPFILE, True)
            End Select
        Catch ex As Exception
            Process.History.Result = Zamba.Core.Results.Erroneo
            Throw New Exception("No se pudo crear el archivo temporal de atributos" & Process.History.TEMPFILE)
        End Try
    End Sub
    Private Function CheckFileHash() As Boolean
        If Process.AskConfirmations = True AndAlso ProcessHistories.ProcessIsReady(Process) = True Then
            If MessageBox.Show("Este proceso ha sido ejecutado recientemente, ¿quiere ejecutarlo de todos modos?", "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Information) <> DialogResult.Yes Then
                ErrorOcurred("El proceso ya ha sido ejecutado con anterioridad")
                Process.History.Result = Zamba.Core.Results.Cancelado_EjecutadoAnteriormente
                Return False
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function
    Private _check As Boolean = True

    Public Property Check() As Boolean
        Get
            Return _check
        End Get
        Set(ByVal Value As Boolean)
            _check = Value
        End Set
    End Property
    Private Function CreateDocument() As Boolean
        Try
            Dim RB As New Results_Business

            If IsNothing(Document) Then Document = RB.GetNewNewResult(Process.DocType)
            Document.Parent = Process.DocType
            ' Document.DocTypeName = Document.Parent.Name
            'Document.DocTypeId = Document.Parent.Id
            EventOCurred("Asignando Entidad: " & Document.DocType.Name)
            'para que sirve?
            ' Document.IndexText = DocType.GetIndexText(Me.Process.DocTypeId)
            Results_Business.LoadIndexs(Document)
            ' Document.FlagCopyVerify = Me.Process.Verify
        Catch ex As Exception
            Process.History.Result = Zamba.Core.Results.Erroneo
            Throw New Exception("Error al crear el Documento Base del proceso " & ex.ToString)
        End Try
    End Function
    Private Function GetprocessIndexData() As Boolean
        Try
            Process.DsProcessIndex = ProcessFactory.GetProcessIndexData(Process.ID)
            Return True
        Catch ex As Exception
            ErrorOcurred("Error obteniendo los datos del proceso: " & ex.ToString)
            Process.History.Result = Zamba.Core.Results.Erroneo
            Return False
        End Try
    End Function
    Private Function DeleteOriginalIndexFile() As Boolean
        Try
            If Process.type = ProcessTypes.Common AndAlso Process.FlagDelSourceFile = True Then
                GC.Collect()
                Dim Fa As New FileInfo(Process.History.Path)
                Fa.Delete()
            End If
        Catch ex As Exception
            Throw New Exception("Ocurrio un error al eliminar el archivo de Atributos original: " & ex.ToString)
        End Try
    End Function

#End Region
#Region "PreProcess"
    Private Function PreProcess() As ArrayList
        Dim FileList As New ArrayList
        Select Case Process.type
            Case ProcessTypes.Common
                FileList.Add(Process.History.TEMPFILE)
                Try
                    Dim PreProcessArray As ArrayList = PreProcessFactory.getPreprocess(Process.ID, True)
                    If PreProcessArray.Count > 0 Then
                        Dim IPPREPROCESS As New PreProcessFactory
                        RemoveHandler IPPREPROCESS.PreprocessError, AddressOf ErrorOcurred
                        RemoveHandler IPPREPROCESS.PreProcessMessage, AddressOf EventOCurred
                        AddHandler IPPREPROCESS.PreprocessError, AddressOf ErrorOcurred
                        AddHandler IPPREPROCESS.PreProcessMessage, AddressOf EventOCurred
                        FileList = IPPREPROCESS.Preprocess(PreProcessArray, FileList, , False)
                    End If
                Catch ex As Exception
                    Process.History.Result = Zamba.Core.Results.Erroneo
                    Throw New Exception("Ocurrio un error al ejecutar los preprocesos" & ex.ToString)
                End Try
            Case ProcessTypes.ContinueProcess
                FileList.Add(Process.History.TEMPFILE)
            Case ProcessTypes.ProcessErrors
                FileList.Add(Process.History.ERRORFILE)
            Case ProcessTypes.Test
                FileList.Add(Process.History.TEMPFILE)
                Try
                    Dim PreProcessArray As ArrayList = PreProcessFactory.getPreprocess(Process.ID, True)
                    If PreProcessArray.Count > 0 Then
                        Dim IPPREPROCESS As New PreProcessFactory
                        RemoveHandler IPPreProcess.PreprocessError, AddressOf ErrorOcurred
                        RemoveHandler IPPreProcess.PreProcessMessage, AddressOf EventOCurred
                        AddHandler IPPreProcess.PreprocessError, AddressOf ErrorOcurred
                        AddHandler IPPreProcess.PreProcessMessage, AddressOf EventOCurred
                        FileList = IPPREPROCESS.Preprocess(PreProcessArray, FileList, , True)
                    End If
                Catch ex As Exception
                    Process.History.Result = Zamba.Core.Results.Erroneo
                    Throw New Exception("Ocurrio un error al ejecutar los preprocesos" & ex.tostring)
                End Try
        End Select
        Return FileList
    End Function

#End Region
    '#Region "PostProcess"
    '    Private Sub PostProcess(ByVal Files As ArrayList)
    '        If IO.File.Exists(".\postprocess.xml") Then
    '            Dim ds As New DsPostProcess
    '            Try
    '                ds.ReadXml(".\postprocess.xml")
    '            Catch ex As Exception
    '                ds = New DsPostProcess
    '            End Try
    '            Dim i As Int32
    '            For i = 0 To ds.Tables(0).Rows.Count - 1
    '                Try
    '                    If CInt(ds.Tables(0).Rows(i).Item(1)) = Me.Process.ID Then
    '                        For Each s As String In Files
    '                            Shell(ds.Tables(0).Rows(i).Item(0) & " " & s, AppWinStyle.NormalFocus, False)
    '                            Me.PostProcessEjecutados.Add(ds.Tables(0).Rows(i).Item(0))
    '                        Next
    '                    End If
    '                Catch ex As Exception
    '                    Throw New Exception("Fallo el postproceso " & ds.Tables(0).Rows(i).Item(0) & " " & ex.ToString)
    '                End Try
    '            Next
    '            ds.WriteXml(".\postprocess.xml")
    '        End If
    '    End Sub

    '#End Region
#Region "FinalProcess"
    Dim Fi As FileInfo
    Private Function FinalProcess(ByVal PreProcessedFilesList As ArrayList) As Boolean
        'Dim XLine As String
        Dim FileCounter As Int32
        '13/12 FEDE: aca salta la excepcion -->>
        Try
            If Not IsNothing(PreProcessedFilesList) Then
                For FileCounter = 0 To PreProcessedFilesList.Count - 1
                    Fi = New FileInfo(PreProcessedFilesList(FileCounter))
                    'Abro el archivo temporal
                    OpenFiletoRead()
                    'Abro el archivo de errores
                    '    If OpenFileToWrite() = False Then Return False
                    'Cuento las filas
                    CountFilesToProcess()
                    'Informo las filas y pregunto para comenzar
                    If InformFilesToProcess() = False Then Return False
                    'Abro el/los  archivo/s segun el tipo de proceso
                    ProcessFiles()
                Next
            End If
            DefineResult()
            ResultActions()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
            MessageBox.Show(ex.ToString)
        End Try
        Return True
    End Function
    Private Function OpenFiletoRead() As Boolean
        Try
            FileReader = New System.IO.StreamReader(Fi.FullName, System.Text.Encoding.Default)
        Catch ex As Exception
            Process.History.Result = Zamba.Core.Results.Erroneo
            Throw New Exception("No se pudo abrir el archivo temporal de atributos en: " & Fi.FullName & " " & ex.ToString)
        End Try
    End Function

    Private Sub CountFilesToProcess()
        Try
            EventOCurred("Realizando el conteo de filas a procesar")
            Do While FileReader.Peek <> -1 AndAlso CancelCheck() = True
                Dim x As String
                x = FileReader.ReadLine()
                If x.Trim <> "" Then
                    UpdateCounters(CounterUpdates.LineCounted)
                End If
            Loop
            EventOCurred("Filas a procesar: " & Process.History.TotalFiles - Process.History.ProcesedFiles)
        Catch ex As Exception
            Process.History.Result = Zamba.Core.Results.Erroneo
            Throw New Exception("Error al realizar el conteo de filas " & ex.ToString)
        Finally
            Try
                CloseFile()
            Catch ex As Exception
                zamba.core.zclass.raiseerror(ex)
            End Try
        End Try
    End Sub
    Private Function InformFilesToProcess() As Boolean
        If Process.UserId <> 9999 AndAlso Process.AskConfirmations Then
            ' Dim Msg As System.Windows.Forms.MessageBox
            If Process.type = ProcessTypes.ContinueProcess Then
                If Process.History.TotalFiles > 0 Then
                    If System.Windows.Forms.MessageBox.Show("Se detectaron " & Process.History.TotalFiles & " Filas, Importadas: " & Process.History.ProcesedFiles & ", Con Errores: " & Process.History.ErrorFiles & ", Salteadas: " & Process.History.SkipedFiles & ". Desea Reprocesar?", "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) = DialogResult.No Then
                        If Process.History.ErrorFiles > 0 Then
                            ErrorOcurred("Proceso Cancelado")
                            Process.History.Result = Zamba.Core.Results.Con_Errores
                            Return False
                        Else
                            ErrorOcurred("Proceso sin ejecutar")
                            Process.History.Result = Zamba.Core.Results.Sin_procesar
                            Return False
                        End If
                    End If
                Else
                    System.Windows.Forms.MessageBox.Show("No se detectaron Filas para procesar", "Zamba Software - Importacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
                    ErrorOcurred("Proceso No Procesado")
                    Process.History.Result = Zamba.Core.Results.Sin_procesar
                    Return False
                End If
                Return True
            Else
                If Process.History.TotalFiles > 0 Then
                    If System.Windows.Forms.MessageBox.Show("Se detectaron " & Process.History.TotalFiles & " filas, Desea procesar?", "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) = DialogResult.No Then
                        ErrorOcurred("Proceso Cancelado")
                        Process.History.Result = Zamba.Core.Results.Cancelado
                        Return False
                    Else
                        Return True
                    End If
                Else
                    System.Windows.Forms.MessageBox.Show("No se detectaron Filas para procesar", "Zamba Software - Importacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
                    ErrorOcurred("Proceso No Procesado")
                    Process.History.Result = Zamba.Core.Results.Sin_procesar
                    Return False
                End If
            End If
        Else
            Return True
        End If
    End Function

    Private Sub ProcessFiles()
        Try
            Select Case Process.type
                Case ProcessTypes.Common
                    FileReader = New System.IO.StreamReader(Fi.FullName, System.Text.Encoding.Default)
                    ImportFiles()
                Case ProcessTypes.ContinueProcess
                    FileReader = New System.IO.StreamReader(Fi.FullName, System.Text.Encoding.Default)
                    SkipFiles(Process.History.ProcesedFiles + Process.History.ErrorFiles)
                    ImportFiles()
                Case ProcessTypes.ProcessErrors
                    'Importo los errores
                    FileReader = New System.IO.StreamReader(Fi.FullName, System.Text.Encoding.Default)
                    ImportFiles()
                Case ProcessTypes.Test
                    FileReader = New System.IO.StreamReader(Fi.FullName, System.Text.Encoding.Default)
                    TestFiles()
            End Select
        Catch ex As Exception
            ErrorOcurred("Error al abrir el archivo de atributos")
            Process.History.Result = Zamba.Core.Results.Erroneo
        Finally
            CloseFile()
        End Try
    End Sub

    Private Function ImportFiles()
        Dim Xline As String
        Dim str As StringBuilder = New StringBuilder
        Do While FileReader.Peek <> -1 AndAlso CancelCheck()
            Try
                Xline = FileReader.ReadLine
                If Xline.Trim <> "" Then
                    EventOCurred("Leyendo Fila N: " & ActualLine & " Detalle: " & Xline)
                    Import(Xline)
                    UpdateCounters(CounterUpdates.LineImported)

                    SafeSave()
                End If
            Catch ex As Exception
                ErrorOcurred("ERROR LINEA N: " & ActualLine & " : " & Xline & " Err: " & ex.ToString)
                If Process.type <> ProcessTypes.ProcessErrors Then
                    WriteError(Xline)
                    UpdateCounters(CounterUpdates.ErrolineFound)
                Else
                    str.AppendLine(Xline)
                    UpdateCounters(CounterUpdates.ErrorLineFailed)
                End If
                Process.History.Result = Zamba.Core.Results.Con_Errores
                SafeSave()
            End Try
        Loop
        If ProcessTypes.ProcessErrors = Process.type Then
            FileReader.Close()
            FileReader = Nothing
            Try
                File.Delete(Process.History.ERRORFILE)
            Catch
                Dim info As New FileInfo(Process.History.ERRORFILE)
                info.Attributes = FileAttributes.Normal
                info.Delete()
                info = Nothing
            End Try
            For Each line As String In str.ToString().Split(Environment.NewLine)
                If line.Trim() <> String.Empty Then
                    WriteError(line)
                End If
            Next
        End If
    End Function

    Dim FilesCount As Int32 = 0
    Dim Hash As SortedList = New SortedList


    Private Sub Import(ByVal x As String, Optional ByVal Test As Boolean = False)

        'separo los atributos
        'x = Replace(x, "'", "''")
        x = x.Replace(ControlChars.NewLine, " ")
        x = x.Replace(Chr(34), "")
        x = x.Replace("'", " ")

        FilesCount = 0
        Dim xsplit = x.Split(Process.Caracter.Trim)

        'Declaraciones Internas
        Dim ProcessIndexCounter As Integer
        Dim Ruta As String
        Dim Extension As String
        Dim NewFileName As String
        Dim MultipleFiles As New ArrayList

        'reseteo el valor de los datos de los atributos
        Dim DocumentIndexCounter As Int32
        For DocumentIndexCounter = 0 To Document.Indexs.Count - 1
            Document.Indexs(DocumentIndexCounter).data = ""
        Next

        'chequeo el tipo y dato del indice
        For ProcessIndexCounter = 0 To Process.DsProcessIndex.Tables("IP_INDEX").Rows.Count - 1
            'TODO Falta ver si la columna es string no hacer la conversion y el trim que se hagga cuando se inserta el dato
            Dim IndexId As String = Process.DsProcessIndex.Tables("IP_INDEX").Rows(ProcessIndexCounter).Item("INDEX_ID")

            Select Case IndexId.ToUpper
                Case "RUTA"
                    Try
                        Ruta = xsplit(ProcessIndexCounter)
                    Catch ex As Exception
                        ErrorOcurred("Documento: " & ActualLine & " Fila " & x & " Atributo " & IndexId)
                        Process.History.Result = Zamba.Core.Results.Con_Errores
                        Throw ex
                    End Try
                Case "RUTACOMPLETA"
                    If Process.FlagMultipleFiles = True Then
                        MultipleFiles.Clear()
                        Try
                            Dim FilesSplit() = xsplit(ProcessIndexCounter).split(Process.MultipleCaracter.Trim)
                            FilesCount += FilesSplit.Length
                            Document.File = xsplit(ProcessIndexCounter)
                            MultipleFiles.AddRange(FilesSplit)
                        Catch ex As Exception
                            ErrorOcurred("Documento: " & ActualLine & " Fila " & x & " Atributo " & IndexId)
                            Process.History.Result = Zamba.Core.Results.Con_Errores
                            Throw ex
                        End Try
                    Else
                        Try
                            Document.File = xsplit(ProcessIndexCounter)
                        Catch ex As Exception
                            ErrorOcurred("Documento: " & ActualLine & " Fila " & x & " Atributo " & IndexId)
                            Process.History.Result = Zamba.Core.Results.Con_Errores
                            Throw ex
                        End Try
                    End If
                Case "NOMBREARCHIVO"
                    Try
                        NewFileName = xsplit(ProcessIndexCounter)
                    Catch ex As Exception
                        ErrorOcurred("Documento: " & ActualLine & " Fila " & x & " Atributo " & IndexId)
                        Process.History.Result = Zamba.Core.Results.Con_Errores
                        Throw ex
                    End Try
                Case "EXTENSION"
                    Try
                        Extension = xsplit(ProcessIndexCounter)
                    Catch ex As Exception
                        ErrorOcurred("Documento: " & ActualLine & " Fila " & x & " Atributo " & IndexId)
                        Process.History.Result = Zamba.Core.Results.Con_Errores
                        Throw ex
                    End Try
                Case "SININDEXAR"

                Case Else
                    Dim Arrayindex As Int32 = 0
                    Try
                        Dim OIndexId As Object = CInt(IndexId)
                        ' Dim ArrayIndex As Integer = Document.IndexIdLst.IndexOf(OIndexId)
                        ' If ArrayIndex < 0 Then Throw New Exception("Atributo no coincide")
                        For i As Int32 = 0 To Document.Indexs.Count - 1
                            If Document.Indexs(i).ID = OIndexId Then
                                Arrayindex = i
                            End If
                        Next
                        Dim Data As String = Trim(xsplit(ProcessIndexCounter))
                        Data = Replace(Data, "'", " ")
                        If IsNothing(Data) Then Data = ""

                        If Data.Trim = "" Then
                            If Process.FlagAceptBlankData = True Then
                                Select Case Document.Indexs(Arrayindex).Type
                                    Case 1, 2, 3, 6
                                        Data = 0
                                    Case 4, 5
                                        Data = String.Empty
                                    Case Else
                                        Data = ""
                                End Select
                                Document.Indexs(Arrayindex).Data = Data
                                Document.Indexs(Arrayindex).DataTemp = Data
                            Else
                                Data = ""
                                ErrorOcurred("Documento: " & ActualLine & " Fila " & x & " Atributo " & IndexId)
                                ErrorOcurred("Documento: " & ActualLine & " No se aceptan Atributos en Blanco")
                                Process.History.Result = Zamba.Core.Results.Con_Errores
                                Throw New Exception
                            End If
                        Else
                            'TODO Falta Condicionales especificos a abstraer
                            'validacion de atributos 
                            'TODO Falta mandar a la validacion el len
                            Dim IndexValidated As String = Index.ValidateIndexTypeData(Data,
                            DirectCast(Document.Indexs(Arrayindex), Index).Type)

                            If IsNothing(IndexValidated) OrElse IndexValidated = "" Then
                                Document.Indexs(Arrayindex).Data = Data.Trim
                                Document.Indexs(Arrayindex).DataTemp = Data.Trim
                            Else
                                Data = ""
                                ErrorOcurred("Documento: " & ActualLine & " Fila " & x)
                                ErrorOcurred("Documento: " & ActualLine & " Error de Atributo: " & IndexValidated & " Atributo: " & IndexId)
                                Process.History.Result = Zamba.Core.Results.Con_Errores
                                Throw New Exception
                            End If
                        End If
                    Catch ex As Exception
                        ErrorOcurred("ERROR: " & ActualLine & " Fila " & x)
                        Process.History.Result = Zamba.Core.Results.Con_Errores
                        Dim ex2 As Exception = New Exception(ex.Message & " Cantidad de Atributos: " & Document.Indexs.Count & " Posicion del indice: " & Arrayindex, ex)
                        raiseerror(ex2)
                        Throw ex2
                    End Try
            End Select
        Next
        EventOCurred("Copiando Documento: " & ActualLine)
        Try
            If Document.File = "" Then
                Document.File = Ruta & NewFileName & Extension
            End If
        Catch ex As Exception
            ErrorOcurred("Error al armar el Path del documento: " & ActualLine & " Fila " & x)
            Process.History.Result = Zamba.Core.Results.Con_Errores
            Throw Ex
        End Try
        Document.CreateDate = Process.History.Process_Date
        'Creo el documento
        Try


            If Process.FlagMultipleFiles = True Then
                Dim AlmostOneInserted As Boolean = False
                Dim i As Int32
                For i = 0 To MultipleFiles.Count - 1
                    Document.File = MultipleFiles(i)
                    If Document.File.Trim <> "" Then
                        EventOCurred("Verificando la existencia del archivo " & ActualLine)
                        If New FileInfo(Document.File).Exists = False Then
                            Process.History.Result = Zamba.Core.Results.Con_Errores
                            Throw New Exception("El Archivo a importar no éxiste en la ubicación especificada: " & Document.File)
                        End If
                    End If
                Next

                For i = 0 To MultipleFiles.Count - 1
                    Document.File = MultipleFiles(i)
                    If Document.File.Trim <> "" Then
                        Try
                            Dim Result As InsertResult
                            If Process.type <> ProcessTypes.Test Then
                                Document.ID = 0
                                Result = Results_Business.InsertDocument(Document, False, False, Process.Replace, Process.AskConfirmations)
                                Hash.Add(Document.ID, x)
                            Else
                                Result = InsertResult.Insertado
                            End If
                            Select Case Result
                                Case InsertResult.ErrorCopia
                                    Throw New Exception("Error al copiar el archivo")
                                Case InsertResult.ErrorInsertar
                                    Throw New Exception("Error al Insertar")
                                Case InsertResult.ErrorReemplazar
                                    Throw New Exception("Error al Reemplazar")
                                Case InsertResult.Insertado
                                    DelOriginalDocumentFile()
                                Case InsertResult.InsertadoNuevoVolumen
                                    DelOriginalDocumentFile()
                                Case InsertResult.InsertadoTempVolumen
                                    DelOriginalDocumentFile()
                                Case InsertResult.NoInsertado
                                    Throw New Exception("Error al Insertar")
                                Case InsertResult.NoRemplazado
                                    Throw New Exception("No Reemplazado")
                                Case InsertResult.Remplazado
                                    DelOriginalDocumentFile()
                                Case InsertResult.RemplazadoTodos
                                    Process.Replace = True
                                    DelOriginalDocumentFile()
                                Case InsertResult.NoRemplazado
                                    Process.Replace = False
                                    Process.AskConfirmations = False
                                    Throw New Exception("No Reemplazado")
                            End Select
                            AlmostOneInserted = True
                        Catch ex As VolFullException
                            ErrorOcurred("El Volumen de almacenamiento esta lleno: " & ActualLine & " Fila " & x)
                            Process.History.Result = Zamba.Core.Results.Con_Errores
                            Throw ex
                        Catch ex As Exception
                            ErrorOcurred(ex.ToString)
                            Process.History.Result = Zamba.Core.Results.Con_Errores
                            Throw ex
                        End Try
                        If Document.Volume.ID < 0 Then
                            ErrorOcurred("Almacenando en Volumen Temporal documento N: " & ActualLine)
                            EventOCurred("Insertando SubDocumento: " & i + 1 & " en VOLUMEN TEMPORAL, Documento Numero: " & ActualLine & " Nombre: " & Document.Name)
                            TempVolFiles += 1
                        Else
                            EventOCurred("Insertando SubDocumento: " & i + 1 & " de Documento Numero: " & ActualLine & " Nombre: " & Document.Name)
                        End If
                    End If
                Next
                If AlmostOneInserted Then
                    EventOCurred("Documento  Importado N: " & ActualLine)
                    AlmostOneInserted = False
                Else
                    Dim ex As New Exception("No se encontro ningun archivo para importar, verifique los atributos")
                    ErrorOcurred(ex.ToString)
                    Process.History.Result = Zamba.Core.Results.Con_Errores
                    Throw ex
                End If
            Else
                Try
                    Dim isVirtual As Boolean
                    EventOCurred("Guardando Documento: " & ActualLine)
                    If String.IsNullOrEmpty(Document.File) Then
                        isVirtual = True
                        EventOCurred("El documento no tiene ruta")
                    Else
                        isVirtual = False
                        If File.Exists(Document.File) = False Then
                            Throw New Exception("El Archivo a importar no existe en la ubicacion especificada: " & Document.File)
                        End If
                    End If
                    Dim Result As InsertResult
                    If Process.type <> ProcessTypes.Test Then
                        Document.ID = 0
                        Document.Disk_Group_Id = 0
                        Result = Results_Business.InsertDocument(Document, False, False, Process.Replace, Process.AskConfirmations, isVirtual)
                        If Document.ID <> 0 Then Hash.Add(Document.ID, x)
                        '    results_factory.SaveIndexData(Document, False)
                    Else
                        Result = InsertResult.Insertado
                    End If
                    Select Case Result
                        Case InsertResult.ErrorCopia
                            Throw New Exception("Error al copiar el archivo")
                        Case InsertResult.ErrorInsertar
                            Throw New Exception("Error al Insertar")
                        Case InsertResult.ErrorReemplazar
                            Throw New Exception("Error al Reemplazar")
                        Case InsertResult.Insertado
                            DelOriginalDocumentFile()
                        Case InsertResult.InsertadoNuevoVolumen
                            DelOriginalDocumentFile()
                        Case InsertResult.InsertadoTempVolumen
                            DelOriginalDocumentFile()
                        Case InsertResult.NoInsertado
                            Throw New Exception("Error al Insertar")
                        Case InsertResult.NoRemplazado
                            Throw New Exception("No Reemplazado")
                        Case InsertResult.Remplazado
                            DelOriginalDocumentFile()
                        Case InsertResult.RemplazadoTodos
                            Process.Replace = True
                            DelOriginalDocumentFile()
                        Case InsertResult.ErrorIndicesIncompletos
                            Throw New Exception("Error faltan atributos obligatorios")
                        Case InsertResult.ErrorIndicesInvalidos
                            Throw New Exception("Error atributos con datos invalidos")
                    End Select
                Catch ex As Exception
                    ErrorOcurred(ex.ToString)
                    Process.History.Result = Zamba.Core.Results.Con_Errores
                    Throw ex
                End Try

                If Document.Volume.ID < 0 Then
                    ErrorOcurred("Almacenando en Volumen Temporal documento N: " & ActualLine)
                    EventOCurred("Insertando en VOLUMEN TEMPORAL, Documento Numero: " & ActualLine & " Nombre: " & Document.Name)
                    TempVolFiles += +1
                Else
                    EventOCurred("Insertando Documento Numero: " & ActualLine & " Nombre: " & Document.Name)
                End If
                EventOCurred("Documento Importado N:" & ActualLine)
            End If
        Catch ex As Exception
            ErrorOcurred(ex.tostring)
            Throw ex
        End Try

        'If Process.History.Result = Results.Con_Errores Or Process.History.Result = Results.Erroneo Then
        '    Dim files As New ArrayList
        '    files.Add(Process.History.ERRORFILE)
        '    files.Add(Process.History.LOGFILE)
        '    Try
        '        Zamba.ZSMTP.SendMail("soporte@stardoc.com.ar", "El siguiente proceso de importación finalizo con errores.", "Errores de Importación", files)
        '    Catch
        '    End Try
        'End If
        If Process.CheckBatch = True Then
            RaiseEvent LogMsg("Comenzando con la verificación")
            CheckAllInsertedDocuments(Document.DocType.Id)
            RaiseEvent LogMsg("Verificación finalizada")
        End If
    End Sub
    Private Sub CheckAllInsertedDocuments(ByVal docTypeId As Int64)
        'TODO: pasar a Stored Procedure
        Dim sql As Text.StringBuilder = New Text.StringBuilder
        Dim i As Int32 = 0
        Dim c As Int32 = 0
        Dim docid As Int64 = 0
        Try
            For i = 0 To Hash.Count - 1
                docid = Hash.GetKey(i)
                sql.Append("Select count(1) from doc")
                sql.Append(doctypeid)
                sql.Append(" Where DOC_ID=")
                sql.Append(docid)
                c = Server.Con.ExecuteScalar(CommandType.Text, sql.ToString)
                sql = sql.Remove(0, sql.Length)
                If c <> 1 Then
                    Import(Hash(docid))
                End If
                Hash.Remove(docid)
            Next
        Catch ex As Exception
        End Try
    End Sub
    Private Sub DelOriginalDocumentFile()
        Try
            If Process.type <> ProcessTypes.Test Then
                If Process.Move = True Then
                    Dim Fi As New FileInfo(Document.OriginalName)
                    Fi.Delete()
                End If
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Function WriteError(ByVal Err As String) As Boolean
        'If Process.type <> ProcessTypes.ProcessErrors Then
        Dim ErrorsWriter As New System.IO.StreamWriter(Process.History.ERRORFILE, True, System.Text.Encoding.Default)
        Try
            ErrorsWriter.AutoFlush = True
            ErrorsWriter.WriteLine(Err)
        Finally
            ErrorsWriter.Close()
            ErrorsWriter = Nothing
        End Try
        'End If
    End Function
    Private Shared Function DeleteError(ByVal Err As String, ByVal LineCount As Int32) As Boolean
    End Function

    Private Function SkipFiles(ByVal Count As Int32)
        Dim Xline As String
        Do While FileReader.Peek <> -1 AndAlso Count <= ActualLine AndAlso CancelCheck()
            Try
                Xline = FileReader.ReadLine
                EventOCurred("Salteando Fila N: " & ActualLine & " Detalle: " & Xline)
                UpdateCounters(CounterUpdates.ErrorLineImported)
                SafeSave()
            Catch ex As Exception
                ErrorOcurred("Linea omitida: " & ActualLine & " : " & Xline & " Err: " & ex.ToString)
                UpdateCounters(CounterUpdates.ErrorLineFailed)
                Process.History.Result = Zamba.Core.Results.Con_Errores
                SafeSave()
            End Try
        Loop
    End Function
    Private Function TestFiles()
        Dim Xline As String
        Do While FileReader.Peek <> -1 AndAlso CancelCheck()
            Try
                Xline = FileReader.ReadLine
                If Xline.Trim <> "" Then
                    EventOCurred("Testeando Fila N: " & ActualLine & " Detalle: " & Xline)
                    Import(Xline)
                    UpdateCounters(CounterUpdates.LineImported)
                    SafeSave()
                End If
            Catch ex As Exception
                ErrorOcurred("Linea omitida: " & ActualLine & " : " & Xline & " Err: " & ex.ToString)
                WriteError(Xline)
                UpdateCounters(CounterUpdates.ErrolineFound)
                Process.History.Result = Zamba.Core.Results.Con_Errores
                SafeSave()
            End Try
        Loop
    End Function

    Private Sub CloseFile()
        Try
            EventOCurred("Cerrando el archivo de atributos")
            If Not IsNothing(FileReader) Then
                FileReader.Close()
                FileReader = Nothing
            End If
            GC.Collect()
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Private Sub DefineResult()
        If Process.History.ErrorFiles > 0 Then
            'Con Errores
            If FlagGoOn = True Then
                'Sin Cancelacion
                Process.History.Result = Zamba.Core.Results.Con_Errores
            Else
                'Con Cancelacion
                Process.History.Result = Zamba.Core.Results.Cancelado_y_Errores
            End If
        Else
            'Sin Errores
            If FlagGoOn = True Then
                'Sin Cancelacion
                If Process.History.ProcesedFiles > 0 Then
                    Process.History.Result = Zamba.Core.Results.Correctamente
                Else
                    Process.History.Result = Zamba.Core.Results.Sin_procesar
                End If
            Else
                'Con Cancelacion
                Process.History.Result = Zamba.Core.Results.Cancelado
            End If
        End If
        EventOCurred("Proceso Terminado : " & Process.History.Result.ToString)
    End Sub
    Private Sub ResultActions()
        'Armo los mensajes de finalizacion
        EventOCurred("Proceso Finalizado: " & Now)

        Select Case Process.History.Result
            Case Zamba.Core.Results.Cancelado
            Case Zamba.Core.Results.Cancelado_EjecutadoAnteriormente
            Case Zamba.Core.Results.Cancelado_y_Errores
            Case Zamba.Core.Results.Con_Errores
            Case Zamba.Core.Results.Correctamente
                ''Solo si es correcto el procesamiento elimino el archivo de origen de atributos, si esta fuera la opcion 
                'If Me.Process.FlagDelSourceFile = True Then
                '    Try
                '        EventOCurred("Eliminando Archivo Original de Atributos")
                '        GC.Collect()
                '        Dim Fa As New IO.FileInfo(Process.History.Path)
                '        Fa.Delete()
                '    Catch ex As Exception
                '        ErrorOcurred(ex.tostring)
                '        ErrorOcurred("No se pudo Eliminar el archivo de atributos: " & ex.tostring)
                '    End Try
                'End If
            Case Zamba.Core.Results.Erroneo
            Case Zamba.Core.Results.Sin_procesar
        End Select
    End Sub
    Private Sub FinalMessages()
        Try
            EventOCurred("Proceso: " & Process.Name & " finalizado: " & Process.History.Result.ToString)
            EventOCurred("Total de Documentos: " & Process.History.TotalFiles)
            EventOCurred("Documentos Importados: " & Process.History.ProcesedFiles)
            EventOCurred("Documentos con Error: " & Process.History.ErrorFiles)
            EventOCurred("Documentos Salteados: " & Process.History.SkipedFiles)
            EventOCurred("Archivos almacenados en VOLUMEN TEMPORAL: " & TempVolFiles)
            If PostProcessEjecutados.Count > 0 Then
                Dim I As Int32
                For I = 0 To PostProcessEjecutados.Count - 1
                    EventOCurred("Post Proceso Ejecutado. Nombre: " & PostProcessEjecutados(I))
                Next
            End If
            'Guardo el historial del proceso
            ProcessHistories.UpdateHistory(Process.History)
            Informar()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

#End Region
#Region "Files y Streams"
    Dim FileReader As IO.StreamReader
#End Region

#Region "Utilities"
    'Private Sub ProcessCanceled()
    '    '     RaiseEvent eEventOcurred("Proceso Cancelado")
    '    Me.FlagGoOn = False
    'End Sub
    Private Sub UpdateCounters(ByVal UpdateType As CounterUpdates, Optional ByVal Line As Int64 = 0)
        Select Case UpdateType
            Case CounterUpdates.LineCounted
                If Process.type <> ProcessTypes.ProcessErrors Then
                    Process.History.TotalFiles += 1
                End If
                RaiseEvent LogCounterChanged(CounterTypes.Counted, Line)
            Case CounterUpdates.LineImported
                Process.History.ProcesedFiles += 1
                ActualLine += +1
                RaiseEvent LogCounterChanged(CounterTypes.Imported, Line)
            Case CounterUpdates.LineSkiped
                '                Process.History.ProcesedFiles += 1
                '       Process.History.SkipedFiles += 1
                ActualLine += +1
            Case CounterUpdates.ErrolineFound
                Process.History.ErrorFiles += 1
                '     Process.History.ProcesedFiles += +1
                '                ErrorOcurredline(ActualLine)
                ActualLine += +1
                RaiseEvent LogCounterChanged(CounterTypes.ErrorFound, Line)
            Case CounterUpdates.ErrorLineImported
                Process.History.ErrorFiles += -1
                Process.History.ProcesedFiles += 1
                '              Process.History.DelErrorLine(Line)
                ActualLine += +1
                RaiseEvent LogCounterChanged(CounterTypes.ErrorImported, Line)
            Case CounterUpdates.ErrorLineFailed
                '            Process.History.ProcesedFiles += +1
                ActualLine += 1
        End Select
    End Sub
    Private Sub SafeSave()
        Try
            'Guardo el historial del proceso por Falla Tecnica o de Hardware
            If Process.History.ErrorFiles > 0 Then
                Process.History.Result = Results.Cancelado_y_Errores
            Else
                Process.History.Result = Results.Cancelado
            End If
            ProcessHistories.UpdateHistory(Process.History)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Function CancelCheck() As Boolean
        'Verifico la cancelacion
        If FlagGoOn = False Then
            If MessageBox.Show("Esta seguro que desea cancelar el proceso de importación?", "Cancelacion Proceso de Importacion", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                EventOCurred("Se cancelo el proceso")
                Return False
            Else
                FlagGoOn = True
                Return True
            End If
        Else
            Return True
        End If
    End Function
#End Region

    Public Shared Function preProcessThisFile(ByVal proId As Int32, ByVal filein As String) As String
        '  Dim process As process = ProcessFactory.GetProcess(proId)
        Dim pre As ArrayList = PreProcessFactory.getPreprocess(proId, True)
        Dim files As New ArrayList
        files.Add(filein)
        Dim ipp As New PreProcessFactory
        Dim result As ArrayList = ipp.Preprocess(pre, files)
        If result.Count > 0 Then
            Return result(0)
        End If
        Return Nothing
    End Function
    Private Sub Informar()
        Try
            Dim ds As New DataSet
            Dim opcion As Zamba.Core.Results
            If File.Exists(".\Notify.xml") Then
                ds.ReadXml(".\Notify.xml")
                If ds.Tables(0).Rows(0).Item(0) = "8" Then
                    '8, No se envia mails
                Else
                    opcion = CInt(ds.Tables(0).Rows(0).Item(0))

                    If opcion = "9" Then
                        Dim files As New List(Of String)
                        files.Add(Process.History.ERRORFILE)
                        files.Add(Process.History.LOGFILE)
                        MessagesBusiness.SendMail("soporte@stardoc.com.ar", String.Empty, String.Empty, "El siguiente proceso de importación finalizo con errores.", "Resultados de Importación", True, files, Nothing)
                    Else
                        Process.History.Result = opcion
                        Dim files As New List(Of String)
                        files.Add(Process.History.ERRORFILE)
                        files.Add(Process.History.LOGFILE)
                        MessagesBusiness.SendMail("soporte@stardoc.com.ar", String.Empty, String.Empty, "El siguiente proceso de importación finalizo con errores.", "Resultados de Importación", True, files, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub



End Class