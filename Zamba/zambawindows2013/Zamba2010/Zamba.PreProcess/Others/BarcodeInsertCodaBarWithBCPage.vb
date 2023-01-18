Imports System.IO
Imports Zamba.Servers
Imports Zamba.Core
Imports System.Drawing
Imports ZXing
Imports Spire.Barcode
'Imports zamba.DocTypes.Factory

<Ipreprocess.PreProcessName("Insertar Código de Barras"), Ipreprocess.PreProcessHelp("Reemplazo la imagen correcta de caratula por la imagen por defecto de caratulas. Lee un archivo CajaYLote.xml, el cual esta ubicado en el directorio de la aplicación")>
Public Class BarcodeInsertCodaBarWithBCPage
    Inherits ZClass
    Implements Ipreprocess

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Reemplazo la imagen correcta de caratula por la imagen por defecto de caratulas. Lee un archivo CajaYLote.xml, el cual puede estar ubicado en C:\, C:\Monitoreo\ o en el directorio de la aplicación"
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
    Public Function process(ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As ArrayList Implements Ipreprocess.process
        Dim fs As FileInfo = Nothing
        Dim fx As FileInfo = Nothing
        Dim docTypeId As New ArrayList
        Dim docId As New ArrayList
        Dim result As New ArrayList
        Dim Reemplaza As Boolean
        Dim UserId As New ArrayList
        Dim IdCaratula As Integer
        Dim caratulaIndex As Int64
        Dim ErrorFile As Boolean

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando Proceso BarcodeInsertCodaBar")

        If Not IsNothing(Files(0)) Then

            If IsNothing(param) OrElse param.Count = 0 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha configurado el atributo que llevara el Id de caratula")
                raiseerror(New Exception("No se ha configurado el atributo que llevara el Id de caratula"))
                Return Nothing
            Else
                If Int64.TryParse(param(0), caratulaIndex) = False Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El Id de caratula no es un numero valido")
                    raiseerror(New Exception("El Id de caratula no es un numero valido"))
                    Return Nothing
                End If
            End If

            Dim TempDir As DirectoryInfo

            'Obtengo doctypeid y docid
            Try
                fs = New FileInfo(Files(0))
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo obtenido: " & fs.FullName.ToString())

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Moviendo a la carpeta Temp")
                fx = MovetoBarcodeFolderTemp(fs)

                If fx Is Nothing Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al mover archivo a la carpeta temporal")
                    Throw New Exception(String.Format("Error al mover archivo [{0}] a la carpeta temporal", fs.Name))
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo movido a carpeta " & fx.FullName.ToString())

                'creamos un nuevo document pdf 
                Dim completePdfDoc As New Spire.Pdf.PdfDocument(fx.FullName)
                Dim pages As New List(Of Spire.Pdf.PdfPageBase)
                Dim newPdfDoc As Spire.Pdf.PdfDocument
                Dim pagesWithCodabar As New List(Of Tuple(Of Int32, String))
                Dim index As Int32 = 0
                Dim Pdfs As New List(Of Spire.Pdf.PdfDocument)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Iterando sobre las paginas del PDF " & fx.FullName.ToString())
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de paginas: " & completePdfDoc.Pages.Count.ToString())

                'Se escanea las paginas, extrae las imagenes y verifica en que paginas hay codigos CodaBar
                For Each page As Spire.Pdf.PdfPageBase In completePdfDoc.Pages
                    pages.Add(page)
                    Dim images As Image() = page.ExtractImages()
                    If images IsNot Nothing AndAlso images.Length > 0 Then
                        For Each image As Image In images
                            Dim bcData As String() = BarcodeScanner.Scan(image, BarCodeType.Codabar)
                            If bcData IsNot Nothing AndAlso bcData.Length > 0 Then
                                For Each code As String In bcData
                                    If IsNumeric(code) AndAlso code.Length >= 5 AndAlso CodaBarExistsInZamba(code) Then
                                        pagesWithCodabar.Add(New Tuple(Of Int32, String)(index, code))
                                        Exit For
                                    End If
                                Next
                            End If
                        Next
                    End If
                    index = index + 1
                Next

                'Si el archivo no tiene codigos de barra del tipo CodaBar se deja en la carpeta temp.
                If Not pagesWithCodabar.Count > 0 Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("No se encontraron codigos del tipo CodaBar en el archivo [{0}], verificar documento.", fx.FullName))
                    Throw New Exception(String.Format("No se encontraron codigos del tipo CodaBar en el archivo [{0}], verificar documento.", fx.FullName))
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de paginas con CodaBar: " & pagesWithCodabar.Count.ToString())

                'Se crea carpeta temporal para poner los pdf archivos cortados
                TempDir = New DirectoryInfo(Path.Combine(fx.Directory.FullName, fx.Name.Substring(0, fx.Name.Length - 4)))
                CreateDir(TempDir)

                'Se genera cada PDF
                While pagesWithCodabar.Count > 0
                    newPdfDoc = New Spire.Pdf.PdfDocument()
                    Dim pagesCount = If(pagesWithCodabar.Count > 1, (pagesWithCodabar(1).Item1 - pagesWithCodabar(0).Item1), (pages.Count - pagesWithCodabar(0).Item1))
                    For Each p As Spire.Pdf.PdfPageBase In pages.GetRange(pagesWithCodabar(0).Item1, pagesCount)
                        newPdfDoc.InsertPage(completePdfDoc, p)
                    Next
                    newPdfDoc.DocumentInformation.Title = pagesWithCodabar(0).Item2
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Nuevo pdf creado: " & newPdfDoc.DocumentInformation.Title)
                    newPdfDoc.Tag = Path.Combine(TempDir.ToString(), pagesWithCodabar(0).Item2 & ".pdf")
                    newPdfDoc.SaveToFile(newPdfDoc.Tag.ToString())
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Pdf copiado en ruta: " & newPdfDoc.Tag.ToString())
                    Pdfs.Add(newPdfDoc)
                    pagesWithCodabar.RemoveAt(0)
                End While

                'Se procesa cada archivo pdf cortado.
                Dim pdfFile As FileInfo
                For Each pdf As Spire.Pdf.PdfDocument In Pdfs
                    Try
                        ErrorFile = False
                        result.Clear()
                        docId.Clear()
                        docTypeId.Clear()
                        UserId.Clear()

                        pdfFile = New FileInfo(pdf.Tag)
                        IdCaratula = pdf.DocumentInformation.Title

                        'Busco la caratula en la Base de datos
                        Dim strSelect As String

                        strSelect = "Select doc_id, doc_type_id, userid, scanned from zbarcode where id=" & IdCaratula
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Consulta ID caratula: " & strSelect)

                        'Me fijo cuantas replicas trajo
                        Dim ds As New DataSet
                        ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)

                        If ds Is Nothing OrElse ds.Tables(0) Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
                            'No encontro la caratula
                            Throw New Exception("No se encontro caratula en zbarcode con el ID: " & IdCaratula)
                        Else
                            'Por cada caratula o replica actualizo el result correspondiente
                            Dim i As Integer
                            For i = 0 To ds.Tables(0).Rows.Count - 1
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando result: " & ds.Tables(0).Rows(0).Item("doc_id").ToString())
                                If IsDBNull(ds.Tables(0).Rows(0).Item("scanned")) = False AndAlso (ds.Tables(0).Rows(0).Item("scanned") = "Si" OrElse ds.Tables(0).Rows(i).Item("scanned") = "SI" OrElse ds.Tables(0).Rows(i).Item("scanned") = "si") Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Reemplaza")
                                    docId.Add(ds.Tables(0).Rows(i).Item(0))
                                    docTypeId.Add(ds.Tables(0).Rows(i).Item(1))
                                    UserId.Add(ds.Tables(0).Rows(i).Item(2))
                                Else
                                    docId.Add(ds.Tables(0).Rows(i).Item(0))
                                    docTypeId.Add(ds.Tables(0).Rows(i).Item(1))
                                    UserId.Add(ds.Tables(0).Rows(i).Item(2))
                                End If
                            Next
                        End If

                        'Obtengo result
                        Dim j As Int32
                        Dim DocType As DocType = Nothing
                        Dim olddocTypeId As Int64
                        For i As Integer = 0 To docId.Count - 1
                            'Creo el Result por cada caratula o replica
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando result para: " & docId(i).ToString())
                            If CInt(docTypeId(i)) <> olddocTypeId Then
                                DocType = DocTypesBusiness.GetDocType(CInt(docTypeId(i)), True)
                            End If
                            Dim result1 As NewResult = Results_Business.GetNewNewResult(CInt(docId(i)), DocType)

                            For j = 0 To result1.Indexs.Count - 1
                                If result1.Indexs(j).ID = caratulaIndex Then result1.Indexs(j).Data = IdCaratula
                                RaiseInfos("Caratula: " & IdCaratula & "Numero: " & i + 1 & " Info Monitoreo")
                            Next

                            'actualizo las doc_i
                            Dim strupdate As String
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando doc_i: " & docTypeId(i).ToString() + ", caratula: " & IdCaratula.ToString())
                            strupdate = "Update doc_i" & docTypeId(i) & " set i" & caratulaIndex & "=" & IdCaratula & " Where doc_id=" & docId(i)
                            Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
                            result.Add(result1)
                        Next

                        'Muevo la caratula al volumen y reemplazo la caratula generica
                        Dim RB As New Results_Business
                        'le saco el atributo de solo lectura para poder reemplazarlo
                        For Each R As NewResult In result
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Moviendo caratula al volumen")
                            RB.LoadVolume(R)
                            R.File = pdfFile.FullName
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "NewResult.File = " & R.File.ToString())
                            RB.UpdateInsert(R, False, True, True, False, False, False)
                            RaiseInfos("Caratula " & IdCaratula & " INSERTADA en " & R.DocType.Name & " DOCID: " & R.ID, "Info Monitoreo")
                        Next
                        pdfFile.Attributes = FileAttributes.Normal

                        'Actualizo el estado de scanneado y la fecha,batch,box
                        If Server.isOracle Then
                            Dim parValues() As Object = {IdCaratula, 0, 0}
                            Server.Con.ExecuteNonQuery("UPDATE_SCANNEDBARCODE_PKG.Update_barcode", parValues)
                        Else
                            Dim parvalues() As Object = {IdCaratula, 0, 0}
                            Server.Con.ExecuteNonQuery("UPDATE_BARCODE", parvalues)
                        End If

                        'Obtengo la primer pagina del pdf
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando indexacion")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Tomando primer pagina para generar thumb")
                        Dim img As Image = pdf.SaveAsImage(0)
                        'Para cada replica genero el thumb con esa primer img e indexo los indices
                        For Each R As NewResult In result
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando thumb para Result: " & R.ID.ToString())
                            RB.GenerateThumbs(img, R.ID, R.DocTypeId)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Indexando indices para Result: " & R.ID.ToString())
                            RB.InsertSearchIndexData(R)
                            'Seteo el indexer state como Indices y thumbs. (si o si ese estado, si da error nada)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando estado de indexacion a: " & IndexedState.IndicesSolamenteIndexado.ToString())
                            Dim exist = Server.Con.ExecuteScalar(CommandType.Text, "select 1 from ZINDEXERSTATE where DOCID = " & R.ID) = 1
                            If exist Then
                                Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("UPDATE ZINDEXERSTATE SET STATE = {0} WHERE DOCID = {1}", Convert.ToInt32(IndexedState.IndicesSolamenteIndexado), R.ID))
                            Else
                                Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("INSERT INTO ZINDEXERSTATE (DOCTYPE, DOCID, ""DATE"", STATE) VALUES ({0}, {1}, {2}, {3})", R.DocTypeId, R.ID, If(Server.isOracle, "SYSDATE", "GETDATE()"), Convert.ToInt32(IndexedState.IndicesSolamenteIndexado)))
                            End If
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Indexacion completa.")
                        Next
                        RB = Nothing

                        'grabo la acción
                        For i As Integer = 0 To docTypeId.Count - 1
                            Try
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Grabando accion de usuario, datos: ")
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "ID de caratula: " & IdCaratula)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Modulo: " & ObjectTypes.ModuleMonitor.ToString())
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Tipo de permiso: " & RightsType.Create.ToString())
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Id de entidad: " & docTypeId(i))
                                UserBusiness.Rights.SaveAction(IdCaratula, ObjectTypes.ModuleMonitor, RightsType.Create, DocTypesBusiness.GetDocTypeName(docTypeId(i), True))
                            Catch ex As Exception
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al grabar accion de usuario, ver carpeta Exceptions.")
                                raiseerror(ex)
                            End Try
                        Next

                    Catch ex As Exception
                        ErrorFile = True
                        raiseerror(ex)
                        Exit For
                    End Try
                Next

            Catch ex As Exception
                ErrorFile = True
                raiseerror(ex)
            Finally
                If TempDir IsNot Nothing AndAlso Not String.IsNullOrEmpty(TempDir.FullName) Then
                    If Directory.Exists(TempDir.FullName) Then
                        Directory.Delete(TempDir.FullName, True)
                    End If
                End If
            End Try

            If ErrorFile Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("No se pudo procesar el archivo [{0}], verificar la carpeta Exceptions", New FileInfo(Files(0)).FullName))
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Pre Proceso terminado correctamente para archivo: " & fx.FullName)
                If File.Exists(fx.FullName) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Eliminando archivo: " & fx.FullName)
                    File.Delete(fx.FullName)
                End If
            End If

        Else
            raiseerror(New Exception("Files(0) esta en nothing"))
        End If

        Return Nothing

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
TryAgain:
            If Fi.Exists Then
                Dim Dir As IO.DirectoryInfo
                Dir = New IO.DirectoryInfo(Path.Combine(Fi.Directory.FullName, "Temp"))
                If Dir.Exists = False Then
                    Dir.Create()
                End If

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
                GoTo TryAgain
            Else
                Throw New Exception("Error al mover a temporal " & ex.ToString)
            End If
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
