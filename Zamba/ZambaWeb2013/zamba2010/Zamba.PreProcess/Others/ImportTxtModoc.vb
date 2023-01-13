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


        Dim docTypeId As Int64
        Dim docId As New ArrayList
        Dim result As New ArrayList
        Dim Reemplaza As Boolean
        Dim UserId As New ArrayList
        Dim IdCaratula As Integer
        Dim caratulaIndex As Int64
        Dim ErrorFile As Boolean
        Dim Nombrelote As Integer
        Dim cantpage As Integer

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando Proceso BarcodeInsertCodaBar")


        'EntityId = 222
        '    Var familia = 0
        '        ippReplaceSeparator = "|"

        'Data read from line:
        '--------------------
        'Número de Guía(0)  139614
        'Número de Despacho(1)  139548
        'Tipo de Despacho(2) 139588
        'Aduana(3) 139550
        'Fecha de Despacho(4) 139551
        'Código de SIGEA(5) 
        'DIGI-ENDO(6) 139589
        'IMPO_EXPO Nombre(7) 139586
        'IMPO-EXPO CUIT(8) 139584
        'Despachante Nombre(9) 139579
        'Despachante CUIT(10) 139600
        'Número Interno(11) 33705
        'Documento de Transporte(12) 139582
        'Monto FOB(13) 139559
        'Números de Facturas(14)  139583
        'Origen(15) 139587
        'Familia(16) 139602
        '--------------------

        Dim ippReplaceSeparator = "|"
        Dim indexsToModify As New List(Of String)

        Dim indexs As Dictionary(Of String, String)
        'Indices para modificar ordenados

        indexsToModify.Add("139614")
        indexsToModify.Add("139594")
        indexsToModify.Add("139578")
        indexsToModify.Add("139590")
        indexsToModify.Add("139639")
        indexsToModify.Add("139640")
        indexsToModify.Add("139641")
        indexsToModify.Add("139642")
        indexsToModify.Add("139643")
        indexsToModify.Add("139644")
        indexsToModify.Add("139609")
        'indexsToModify.Add("139600")
        'indexsToModify.Add("33705")
        'indexsToModify.Add("139582")
        'indexsToModify.Add("139559")
        'indexsToModify.Add("139583")
        'indexsToModify.Add("139587")
        'indexsToModify.Add("139602")


        '-------------------------

        If Not IsNothing(Files(0)) Then
            Dim file = New FileInfo(Files(0))
            Dim basepath As String = file.Directory.FullName
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


            If Files(0).ToString().EndsWith("TXT") Then
                Dim fs As FileInfo = Nothing
                Dim fx As FileInfo = Nothing

                'Backup del TXT
                fs = New FileInfo(Files(0))
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo obtenido: " & fs.FullName.ToString())

                Try
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Moviendo a la carpeta Temp")
                    fx = MovetoBarcodeFolderTemp(fs)
                Catch ex As Exception
                    ' ZClass.raiseerror(ex)
                    Exit Function
                End Try

                If fx Is Nothing Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al mover archivo a la carpeta temporal")
                    Throw New Exception(String.Format("Error al mover archivo [{0}] a la carpeta temporal", fs.Name))
                End If

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Archivo movido a carpeta " & fx.FullName.ToString())

                Try
                    'Realiza un backup de todos los archivos existentes antes de correr el proceso de importación
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Realizando BackUp del archivo")
                    Dim PathDestino As String = Path.Combine(fx.Directory.Parent.FullName, "BackUp")
                    Dim foDestino As New IO.DirectoryInfo(PathDestino)
                    If Not foDestino.Exists Then
                        foDestino.Create()
                    End If
                    fx.CopyTo(foDestino.FullName + "\" + fx.Name, True)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                '--------------------------------------------------------END BACKUP Y MOVE TXT------------------------------------

                Dim reader As New StreamReader(fs.FullName, Encoding.Default)

                Dim line As String = Nothing

                Dim lines As Integer = 0

                Dim indexBusiness As New IndexsBusiness()
                While (reader.Peek() <> -1)
                    line = reader.ReadLine()
                    Dim parts = line.Split(ippReplaceSeparator)
                    Dim listOfData As New List(Of String)
                    listOfData.AddRange(parts)
                    indexs = chargeDictionary(indexsToModify, listOfData)
                    Dim PageNumbre As String = listOfData(listOfData.Count - 1)
                    Dim currentFile As String = basepath & "\" & fs.Name.Replace(".TXT", "") & "_" & PageNumbre & ".pdf"

                    '---------Trabajar con el archivoque corresponde a cada linea del TXT, lo movemos al temp, lo backup y luego lo insertamos


                    Dim fscurrentFile As FileInfo = Nothing
                    Dim fxcurrentFile As FileInfo = Nothing


                    Dim TempDir As DirectoryInfo
                    Dim ErrorTempDir As DirectoryInfo
                    Dim ErrorException As Exception


                    Try
                        '-------------------------------------------MOVE PDF FILE OF EACH LINE TO TEMP AND BACKUP------------------------
                        fscurrentFile = New FileInfo(currentFile)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo obtenido: " & fscurrentFile.FullName.ToString())

                        Try
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Moviendo a la carpeta Temp")
                            fxcurrentFile = MovetoBarcodeFolderTemp(fscurrentFile)
                        Catch ex As Exception
                            ' ZClass.raiseerror(ex)
                            Exit Function
                        End Try

                        If fxcurrentFile Is Nothing Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al mover archivo a la carpeta temporal")
                            Throw New Exception(String.Format("Error al mover archivo [{0}] a la carpeta temporal", fs.Name))
                        End If

                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Archivo movido a carpeta " & fxcurrentFile.FullName.ToString())

                        Try
                            'Realiza un backup de todos los archivos existentes antes de correr el proceso de importación
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Realizando BackUp del archivo")
                            Dim PathDestino As String = Path.Combine(fxcurrentFile.Directory.Parent.FullName, "BackUp")
                            Dim foDestino As New IO.DirectoryInfo(PathDestino)
                            If Not foDestino.Exists Then
                                foDestino.Create()
                            End If
                            fx.CopyTo(foDestino.FullName + "\" + fx.Name, True)
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Catch ex As Exception
                        ErrorFile = True
                        raiseerror(ex)
                        ErrorException = ex
                    End Try

                    '-------------------------------------------END MOVE PDF FILE OF EACH LINE TO TEMP AND BACKUP------------------------


                    Dim results_Business As New Results_Business
                    Dim newresult As NewResult = Results_Business.GetNewNewResult(docTypeId)
                    newresult.File = fscurrentFile.FullName

                    Dim emptyIndexs = indexBusiness.GetIndexsSchemaAsListOfDT(docTypeId, True)
                    For Each I As Index In emptyIndexs
                        If indexs.ContainsKey(I.ID.ToString) Then
                            I.Data = indexs(I.ID.ToString)
                            I.DataTemp = indexs(I.ID.ToString)
                        End If
                    Next
                    newresult.Indexs = emptyIndexs
                    Results_Business.InsertDocument(newresult, False, False, False, False, False, False, False, False, False, False, 0, True)

                End While
                reader.Close()
                reader.Dispose()
            End If
        End If
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
        Try
            If Fi.Exists Then
                Dim Dir As IO.DirectoryInfo
                Dir = New IO.DirectoryInfo(Path.Combine(Fi.Directory.FullName, "Temp"))
                If Dir.Exists = False Then
                    Dir.Create()
                End If

                Dim Fa As New FileInfo(Dir.FullName & "\" & Fi.Name)
                If Fa.Exists Then
                    Throw New Exception("Error al mover a temporal, ya existe el archivo en el temporal")
                End If
                Fi.MoveTo(Fa.FullName)
                Return Fa
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw New Exception("Error al mover a temporal " & ex.ToString)
        End Try
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

End Class
