Imports System.IO
Imports Zamba.Servers
Imports Zamba.Core
Imports System.Drawing
Imports ZXing
Imports Spire.Barcode
Imports System.Text
'Imports zamba.DocTypes.Factory

<Ipreprocess.PreProcessName("Insertar Código de Barras"), Ipreprocess.PreProcessHelp("Reemplazo la imagen correcta de caratula por la imagen por defecto de caratulas. Lee un archivo CajaYLote.xml, el cual esta ubicado en el directorio de la aplicación")>
Public Class ippImportTxtModoc
    Inherits ZClass
    Implements Ipreprocess

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        'Return "Reemplazo la imagen correcta de caratula por la imagen por defecto de caratulas. Lee un archivo CajaYLote.xml, el cual puede estar ubicado en C:\, C:\Monitoreo\ o en el directorio de la aplicación"
        Return "Reemplazo la imagen correcta de caratula por la imagen por defecto de caratulas, Configurando con Numero de id de caratula , id de Nombre de lote y id cantidad de paginas."
    End Function
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implementar
        Return String.Empty
    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml
    End Sub
    Private Shared Sub ReadBoxAndBatch(ByRef caja As Int64, ByRef lote As String, ByRef Reemplaza As Boolean)
        Dim ds As DsCajaLote = Nothing
        Try
            ds = New DsCajaLote
            If File.Exists(Application.StartupPath & "\CajayLote.xml") Then
                ds.ReadXml(Application.StartupPath & "\CajayLote.xml")
                caja = ds.Tables(0).Rows(0).Item(0)
                lote = ds.Tables(0).Rows(0).Item(1)
                Reemplaza = ds.Tables(0).Rows(0).Item("REEMPLAZA")
            Else
                Dim row As DsCajaLote.dsCajaLoteRow = ds.dsCajaLote.NewdsCajaLoteRow
                row.NroCaja = 0
                row.lote = "completar"
                row.REEMPLAZA = False
                ds.Tables(0).Rows.Add(row)
                ds.AcceptChanges()
                ds.WriteXml(Application.StartupPath & "\CajayLote.xml")
                MessageBox.Show("Complete el Nro de Caja y Lote")
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            RaiseNotifyErrors("Error leyendo Caja Y Lote " & ex.ToString)
            Throw New Exception("Error leyendo Caja Y Lote " & ex.ToString)
        Finally
            Try
                If IsNothing(ds) = False Then ds.Dispose()
                ds = Nothing
                GC.Collect()
            Catch
            End Try
        End Try
    End Sub
    Public Overrides Sub Dispose()

    End Sub
    Private Function Movetotemp(ByVal Fi As FileInfo) As FileInfo
        Try
            If Fi.Exists Then
                Dim Dir As IO.DirectoryInfo
                Try
                    Dir = New IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\temp")
                    If Dir.Exists = False Then
                        Dir.Create()
                    End If
                Catch
                    Dir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\temp")
                    If Dir.Exists = False Then
                        Dir.Create()
                    End If
                End Try
                Dim Fa As New FileInfo(Dir.FullName & "\" & Fi.Name)
                If Fa.Exists Then
                    GC.Collect(GC.MaxGeneration)
                    Fa.Delete()
                End If
                Fi.MoveTo(Fa.FullName)
                Return Fa
            Else
                Return Nothing
            End If
        Catch ex As Exception
            If ex.Message.IndexOf("obtener acceso") <> -1 Then
                GC.Collect(GC.MaxGeneration)
                Threading.Thread.CurrentThread.Sleep(10000)
                Dim Dir As IO.DirectoryInfo
                Try
                    Dir = New IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\temp")
                    If Dir.Exists = False Then
                        Dir.Create()
                    End If
                Catch
                    Dir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\temp")
                    If Dir.Exists = False Then
                        Dir.Create()
                    End If
                End Try
                Dim Fa As New FileInfo(Dir.FullName & "\" & Fi.Name)
                If Fa.Exists Then
                    Fa.Delete()
                End If
                Fi.MoveTo(Fa.FullName)
                Return Fa
            ElseIf ex.Message.IndexOf("encontrar") <> -1 Then
                Dim dir As New IO.DirectoryInfo(Fi.Directory.FullName & "\Temp")
                Dim Fa As New FileInfo(dir.FullName & "\" & Fi.Name)
                If Fa.Exists = True Then
                    Return Fa
                Else
                    Return Nothing
                End If
            Else
                Throw New Exception("Error al mover a temporal " & ex.ToString)
            End If
        End Try
    End Function

    Public Function chargeDictionary(listOfkey As List(Of String), listOfData As List(Of String)) As Dictionary(Of String, String)
        Dim i As Integer
        Dim indexs As New Dictionary(Of String, String)
        For i = 0 To listOfkey.Count - 1
            indexs.Add(listOfkey(i), listOfData(i))
        Next
        Return indexs
    End Function

    Public Function process(ByVal Files As ArrayList,
                            Optional ByVal param As ArrayList = Nothing,
                            Optional ByVal xml As String = Nothing,
                            Optional ByVal Test As Boolean = False) As ArrayList Implements Ipreprocess.process

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando Proceso ImportTxtModoc")

        Dim docTypeId As Int64
        Dim DoneFolder As DirectoryInfo
        Dim ErrorFolder As DirectoryInfo
        Dim BaseFolder As String
        Dim file As FileInfo = New FileInfo(Files(0))

        Dim txtExten = ".TXT"
        Dim doneFolderName = "Inserted"
        Dim errorFolderName = "Error"
        Dim despachoName As String

        If file IsNot Nothing AndAlso file.FullName.EndsWith(txtExten) Then 'Si es txt

            despachoName = file.Name.Replace(txtExten, "")

            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Archivo a procesar {0}", file.FullName))

            BaseFolder = file.DirectoryName

            If IsNothing(param) OrElse param.Count = 0 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha configurado el atributo que llevara el Id de entidad")
                raiseerror(New Exception("No se ha configurado el atributo que llevara el Id de entidad"))
                Return Nothing
            Else
                If Int64.TryParse(param(0), docTypeId) = False Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El Id de entidad no es un numero valido")
                    raiseerror(New Exception("El Id de entidad no es un numero valido"))
                    Return Nothing
                End If
            End If

            'obtener las lineas del txt (nombres de los archivos), tambien el completo, con las propiedades
            Dim pdfFilesAndProps As Dictionary(Of FileInfo, FileProps) = GetFilesFromTXT(file)
            Dim pdfFiles As List(Of FileInfo) = pdfFilesAndProps.Keys.ToList()

            If FilesExists(pdfFiles) AndAlso FilesAvailables(pdfFiles) Then 'Si estan todos los archivos y no estan tomados

                'Carpeta con nombre del despacho 
                Dim despachoFolder As New DirectoryInfo(Path.Combine(file.Directory.FullName, despachoName))
                'Pasar todos los archivos a esa carpeta 
                pdfFiles.Add(file)
                MoveFilesToFolder(pdfFiles, despachoFolder) 'Los mueve a la carpeta, si no puede revierte y los deja todos donde estaban

                'Proceso todos los archivos
                Dim _error As Exception = ProcessFiles(pdfFilesAndProps, docTypeId)

                If _error Is Nothing Then 'No hubo excepcion, se procesó todo bien
                    'pasar carpeta completa a carpeta Ok
                    DoneFolder = New DirectoryInfo(Path.Combine(BaseFolder, doneFolderName))
                    If Not DoneFolder.Exists Then
                        DoneFolder.Create()
                    End If
                    despachoFolder.MoveTo(Path.Combine(DoneFolder.FullName, despachoName))
                Else
                    'guardar exception en la carpeta del despacho
                    AddErrorFileToFolder(_error, despachoFolder.FullName)
                    'pasar carpeta completa a carpeta Error
                    ErrorFolder = New DirectoryInfo(Path.Combine(BaseFolder, errorFolderName))
                    If Not ErrorFolder.Exists Then
                        ErrorFolder.Create()
                    End If
                    despachoFolder.MoveTo(Path.Combine(ErrorFolder.FullName, despachoName))
                End If

            Else
                'ZTrace.WriteLineIf(ZTrace.IsError, String.Format("No existen todos los archivos correspondiente al txt {0} o estan tomados por otro proceso, verificar en Trace", file.Name))
                raiseerror(New Exception(String.Format("No existen o no estan disponibles todos los archivos correspondiente al txt {0}, verificar en Trace", file.Name)))
            End If

        End If

    End Function

    Private Sub AddErrorFileToFolder(_error As Exception, folder As String)
        Try
            Dim sw As New StreamWriter(Path.Combine(folder, "Errors.txt"))
            sw.AutoFlush = True
            sw.WriteLine(_error.Message)
            sw.WriteLine("Error:")
            sw.WriteLine(_error.InnerException.Message)
            sw.WriteLine("StackTrace:")
            sw.WriteLine(_error.InnerException.StackTrace)
            sw.Close()
            sw.Dispose()
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, String.Format("No se pudo adjuntar archivo de error a la carpeta ", folder))
            raiseerror(New Exception(String.Format("No se pudo adjuntar archivo de error a la carpeta ", folder)))
        End Try
    End Sub

    Private Function ProcessFiles(pdfFiles As Dictionary(Of FileInfo, FileProps), docTypeId As Int64) As Exception

        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Comenzando la insercion de los archivos"))

        Dim _error As Exception = Nothing

        Dim _file As FileInfo
        Dim _props As FileProps
        Dim indexBusiness As New IndexsBusiness()

        For Each pdf As KeyValuePair(Of FileInfo, FileProps) In pdfFiles

            _file = pdf.Key
            _props = pdf.Value

            Try

                ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Insertando archivo {0}", _file.Name))

                If _file.ToString().Contains("_") Then 'Pdfs de paginas

                    Dim query As String = String.Format("select doc_id from doc_i139089 where i139548 = '{0}' and i139609 = {1} and i139603 = '{2}'",
                                                           _props.NroDespachoIndex,
                                                           _props.PageNumberIndex,
                                                           _props.CodigoIndex)

                    Dim rdocid As Object = Server.Con.ExecuteScalar(CommandType.Text, query)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Doc_id {0} obtenido para el archivo {1}", If(rdocid, 0), _file.Name))

                    If (IsDBNull(rdocid) OrElse rdocid Is Nothing OrElse rdocid.ToString() = "0") Then

                        Dim results_Business As New Results_Business
                        Dim newresult As NewResult = Results_Business.GetNewNewResult(docTypeId)
                        newresult.File = _file.FullName

                        Dim emptyIndexs = indexBusiness.GetIndexsSchemaAsListOfDT(docTypeId, True)
                        For Each I As Index In emptyIndexs
                            If _props.indexs.ContainsKey(I.ID.ToString) Then
                                I.Data = _props.indexs(I.ID.ToString)
                                I.DataTemp = _props.indexs(I.ID.ToString)
                            End If
                        Next
                        newresult.Indexs = emptyIndexs
                        Results_Business.InsertDocument(newresult, False, False, False, False, False, False, False, False, False, False, 0, True)

                    Else
                        Dim r As IResult = Results_Business.GetResult(rdocid, 139072)
                        Dim RB As New Results_Business
                        RB.ReplaceDocument(r, _file.FullName, False)
                        For Each I As Index In r.Indexs
                            If _props.indexs.ContainsKey(I.ID.ToString) Then
                                I.Data = _props.indexs(I.ID.ToString)
                                I.DataTemp = _props.indexs(I.ID.ToString)
                            End If
                        Next

                        RB.SaveModifiedIndexData(r, True, False)
                    End If

                Else 'Pdf completo
                    Dim NroDespacho As String = _file.Name.Replace(".pdf", "").Split("-")(0)
                    Dim Codigo As String = _file.Name.Replace(".pdf", "").Split("-")(1)
                    Dim query As String = String.Format("select doc_id from  doc_i139072  where i139548 = '{0}' and i139603 = '{1}'", NroDespacho, Codigo)

                    Dim RdocId As String = Server.Con().ExecuteScalar(CommandType.Text, query)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Doc_id {0} obtenido para el archivo {1}", If(RdocId, 0), _file.Name))

                    Dim r As IResult = Results_Business.GetResult(RdocId, 139072)
                    Dim RB As New Results_Business
                    RB.ReplaceDocument(r, _file.FullName, False)
                End If
            Catch ex As Exception
                Dim msg = String.Format("Error insertando el archivo {0}", _file.Name)
                ZTrace.WriteLineIf(ZTrace.IsError, msg)
                Dim _exception = New Exception(String.Format("Metodo ProcessFiles, {0}", msg), ex)
                raiseerror(ex)
                _error = _exception
                Exit For
            End Try
        Next

        Return _error
    End Function

    Private Sub MoveFilesToFolder(files As List(Of FileInfo), Folder As DirectoryInfo, Optional rollback As Boolean = False)

        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Moviendo archivos a carpeta {0}", Folder.FullName))

        Dim maxTries = 3
        Dim tries = 0
        Dim PassedFiles As New List(Of FileInfo)

        For Each file As FileInfo In files

            If file IsNot Nothing Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Moviendo el archivo: {0}", file.FullName))
                If Not Folder.Exists Then
                    Folder.Create()
                End If
                tries = 0
                While (tries < maxTries)
                    Try
                        file.MoveTo(Path.Combine(Folder.FullName, file.Name))
                        PassedFiles.Add(file)
                        Continue For
                    Catch ex As Exception
                        tries += 1
                        Threading.Thread.CurrentThread.Sleep(10000)
                    End Try
                End While
                If tries = maxTries Then
                    If Not rollback Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Error al mover el archivo {0} a la carpeta {1}. Se devuelven a la carpeta padre {2}", file.Name, Folder.FullName, Folder.Parent.FullName))
                        Dim folderToDelete As DirectoryInfo = Folder
                        MoveFilesToFolder(PassedFiles, Folder.Parent, True)
                        folderToDelete.Refresh()
                        If folderToDelete.Exists Then
                            folderToDelete.Delete()
                        End If
                    End If
                    Throw New Exception(String.Format("Error al mover el archivo {0} a la carpeta {1}.", file.Name, Folder.FullName))
                End If
            End If
        Next

    End Sub

    Private Function FilesExists(pdfFiles As List(Of FileInfo)) As Boolean
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia fisica de los archivos del TXT en la carpeta")
        Dim exists As Boolean = True
        For Each file As FileInfo In pdfFiles
            If Not file.Exists Then
                ZTrace.WriteLineIf(ZTrace.IsError, String.Format("El archivo {0} no existe o aun no esta disponible", file.Name))
                'raiseerror(New Exception(String.Format("El archivo {0} no existe o aun no esta disponible", file.Name)))
                Return False
            End If
        Next
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Existen todos los archivos")
        Return exists
    End Function

    Private Function FilesAvailables(pdfFiles As List(Of FileInfo)) As Boolean
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando que los archivos no esten ocupados por otro proceso")
        Dim available As Boolean = True
        Dim _fileStream As FileStream
        For Each file As FileInfo In pdfFiles
            Try
                _fileStream = New FileStream(file.FullName, FileMode.Open, FileAccess.ReadWrite)
                _fileStream.Close()
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsError, String.Format("El archivo {0} no esta disponible, puede estar tomado por otro proceso", file.Name))
                'raiseerror(New Exception(String.Format("El archivo {0} no esta disponible, puede estar tomado por otro proceso", file.Name)))
                Return False
            End Try
        Next
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Todos los archivos disponibles")
        Return available
    End Function

    Private Function GetFilesFromTXT(file As FileInfo) As Dictionary(Of FileInfo, FileProps)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Comenzando extraccion de las lineas del archivo TXT")

        Dim filesAndProps As New Dictionary(Of FileInfo, FileProps)
        Dim basepath As String = file.Directory.FullName
        Dim line As String
        Dim lines As Integer
        Dim ippReplaceSeparator = "|"
        Dim reader As StreamReader
        Dim Indexs As Dictionary(Of String, String)
        Dim indexsToModify As List(Of String)
        Dim indexBusiness As IndexsBusiness

        Dim completePdfcod As String

        Dim _file As FileInfo
        Dim _props As FileProps

        Dim PageNumberIndex As Integer
        Dim FamilyIndex As Integer
        Dim NroDespachoIndex As Integer
        Dim CodigoIndex As Integer

        Try
            indexsToModify = New List(Of String)
            indexsToModify.AddRange(UserPreferences.getValue("MonitorModocIndexs", UPSections.UserPreferences, "139614,139548,139590,139608,139640,139641,139642,139643,139644,139609,139603").Split(","))

            PageNumberIndex = Integer.Parse(UserPreferences.getValue("MonitorModocPageNumIndex", UPSections.UserPreferences, 2))
            FamilyIndex = Integer.Parse(UserPreferences.getValue("MonitorModocFamilyIndex", UPSections.UserPreferences, 9))
            NroDespachoIndex = Integer.Parse(UserPreferences.getValue("MonitorModocNroDespachoIndex", UPSections.UserPreferences, 10))
            CodigoIndex = Integer.Parse(UserPreferences.getValue("MonitorModocCodigoDespachoIndex", UPSections.UserPreferences, 1))

            reader = New StreamReader(file.FullName, Encoding.Default)
            indexBusiness = New IndexsBusiness()

            Dim parts As String()
            Dim listOfData As List(Of String)
            Dim PageNumber As String
            Dim Family As String
            Dim NroDespacho As String
            Dim codigo As String

            While (reader.Peek() <> -1)

                line = reader.ReadLine()
                parts = line.Split(ippReplaceSeparator)
                listOfData = New List(Of String)
                listOfData.AddRange(parts)
                Indexs = chargeDictionary(indexsToModify, listOfData)
                PageNumber = listOfData(listOfData.Count - PageNumberIndex)
                Family = listOfData(listOfData.Count - FamilyIndex)
                NroDespacho = listOfData(listOfData.Count - NroDespachoIndex)
                codigo = listOfData(listOfData.Count - CodigoIndex)

                completePdfcod = codigo

                _file = New FileInfo(Path.Combine(basepath, file.Name.Replace(".TXT", "") & "-" & Family & "_" & PageNumber & ".pdf"))
                _props = New FileProps With {.PageNumberIndex = PageNumber, .FamilyIndex = Family, .NroDespachoIndex = NroDespacho, .CodigoIndex = codigo, .indexs = Indexs}

                filesAndProps.Add(_file, _props)

            End While

            'Agregar tambien el pdf completo
            _file = New FileInfo(Path.Combine(basepath, file.Name.Replace(".TXT", "") & "-" & completePdfcod & ".pdf"))
            _props = New FileProps()
            filesAndProps.Add(_file, _props)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Extraccion completada con exito")
            Return filesAndProps

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, "Error extrayendo los archivos del TXT")
            raiseerror(ex)
            Throw
        Finally
            If reader IsNot Nothing Then
                reader.Close()
                reader.Dispose()
            End If
        End Try
    End Function

    Private Function CodaBarExistsInZamba(ByVal IdCaratula As Int64) As Boolean
        Return (Server.Con.ExecuteScalar(CommandType.Text, String.Format("Select (1) from zbarcode where id = {0}", IdCaratula)) = 1)
    End Function

    ''' <summary>
    ''' Genera carpeta
    ''' </summary>
    ''' <param name="Directory"></param>
    Private Sub CreateDir(Directory As DirectoryInfo)
        Try
TryAgain:
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando carpeta: " & Directory.FullName)
            If Directory IsNot Nothing AndAlso Not Directory.Exists Then
                Directory.Create()
            End If
        Catch ex As Exception
            If ex.Message.IndexOf("obtener acceso") <> -1 Then
                GC.Collect(GC.MaxGeneration)
                Threading.Thread.CurrentThread.Sleep(10000)
                GoTo TryAgain
            Else
                ZTrace.WriteLineIf(ZTrace.IsError, "Error al crear directorio " & ex.ToString)
                Throw New Exception("Error al crear directorio " & ex.ToString)
            End If
        End Try
    End Sub

    Private Function MovetoBarcodeFolderTemp(ByVal Fi As FileInfo) As FileInfo
        Dim maxTries = 3
        Dim tries = 0

        While (tries < maxTries)
            Try
                If Fi.Exists Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Intento de mover archivo a temporal num: {0}", tries + 1))
                    Dim Dir As IO.DirectoryInfo
                    Dir = New IO.DirectoryInfo(Path.Combine(Fi.Directory.FullName, "Temp"))
                    If Dir.Exists = False Then
                        Dir.Create()
                    End If

                    Dim Fa As New FileInfo(Dir.FullName & "\" & Fi.Name)
                    If Fa.Exists Then
                        Fa.Delete()
                    End If
                    Fi.MoveTo(Fa.FullName)
                    Return Fa
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                tries += 1
                ZClass.raiseerror(ex)
                Threading.Thread.CurrentThread.Sleep(10000)
                'Throw New Exception("Error al mover a temporal " & ex.ToString)
            End Try
        End While
        Throw New Exception(String.Format("Error moviendo el archivo {0} a la carpeta temporal.", Fi.FullName))
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        'TODO:Implementar
        Return String.Empty
    End Function

    Public Shared ReadOnly Property Id() As Integer
        Get
            Return ProcessFactory.GetProcessIDByName(Name())
        End Get
    End Property

    Public Shared ReadOnly Property Name() As String
        Get
            Return "Insertar Codigo de Barras"
        End Get
    End Property


    Class FileProps
        Public PageNumberIndex As String
        Public FamilyIndex As String
        Public NroDespachoIndex As String
        Public CodigoIndex As String
        Public indexs As Dictionary(Of String, String)
    End Class


End Class
