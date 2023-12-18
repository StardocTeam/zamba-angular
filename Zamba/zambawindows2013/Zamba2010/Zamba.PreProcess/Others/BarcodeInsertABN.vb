Imports System.io
Imports Zamba.Servers
Imports Zamba.Core
'Imports zamba.DocTypes.Factory

<Ipreprocess.PreProcessName("Insertar C�digo de Barras para ABN"), Ipreprocess.PreProcessHelp("Reemplazo la imagen correcta de caratula por la imagen por defecto de caratulas. Lee un archivo CajaYLote.xml, el cual esta ubicado en el directorio de la aplicaci�n")> _
Public Class ippBarcodeInsertABN
    Inherits ZClass
    Implements Ipreprocess

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Reemplazo la imagen correcta de caratula por la imagen por defecto de caratulas. Lee un archivo CajaYLote.xml, el cual puede estar ubicado en C:\, C:\Monitoreo\ o en el directorio de la aplicaci�n"
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
            raiseerror(ex)
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
                Dim Fa As New FileInfo(dir.FullName & "\" & Fi.Name)
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
                Dim Fa As New FileInfo(dir.FullName & "\" & Fi.Name)
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
        Dim caja As Int64
        Dim lote As String = String.Empty
        Dim Reemplaza As Boolean
        Dim UserId As New ArrayList
        Dim IdCaratula As Integer
        Dim ErrorFile As Boolean
        'Carga en las variables pasadas los valores ingresos para caja y lote
        ReadBoxAndBatch(caja, lote, Reemplaza)
        GC.Collect()

        If Not IsNothing(Files(0)) Then
            'Obtengo doctypeid y docid
            Try
                fs = New FileInfo(Files(0))
                fx = Movetotemp(fs)
                If IsNothing(fx) Then Return Nothing
                IdCaratula = fx.Name.Substring(0, fx.Name.Length - 4)

                'Busco la caratula en la Base de datos
                Dim strSelect As String
                strSelect = "Select doc_id,doc_type_id,userid,scanned from zbarcode where id=" & IdCaratula

                'Me fijo cuantas replicas trajo
                Dim ds As New DataSet
                ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)

                If ds.Tables(0).Rows.Count = 0 Then
                    'No encontro la caratula
                    Throw New Exception("No se encontro caratulaid en zbarcode de:" & IdCaratula)
                    RaiseNotifyErrors("No se encontro caratulaid en zbarcode de:" & IdCaratula)
                Else
                    'Por cada caratula o replica actualizo el result correspondiente
                    Dim i As Integer
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        If IsDBNull(ds.Tables(0).Rows(0).Item("scanned")) = False AndAlso (ds.Tables(0).Rows(0).Item("scanned") = "Si" OrElse ds.Tables(0).Rows(i).Item("scanned") = "SI" OrElse ds.Tables(0).Rows(i).Item("scanned") = "si") Then
                            If Reemplaza Then
                                docId.Add(ds.Tables(0).Rows(i).Item(0))
                                docTypeId.Add(ds.Tables(0).Rows(i).Item(1))
                                UserId.Add(ds.Tables(0).Rows(i).Item(2))
                                ErrorFile = False
                            Else
                                Try
                                    Dim sw As New StreamWriter(fx.Directory.FullName & "\" & fx.Name.Substring(0, fx.Name.Length - 4) & " NO REEMPLAZADA.log", True)
                                    ErrorFile = True
                                    sw.WriteLine("Imagen ya scaneada, si se quiere reemplazar marcar Xml Caja y Lote para reemplazo")
                                    sw.Flush()
                                    sw.Close()
                                    sw = Nothing
                                    RaiseInfos("Imagen ya scaneada, si se quiere reemplazar marcar Xml Caja y Lote para reemplazo" & IdCaratula, "Error Monitoreo")
                                Catch ex As Exception
                                    raiseerror(New Exception("Error escribiendo archivo de logerror de reemplazo " & ex.ToString))
                                    Return Nothing
                                End Try
                            End If
                        Else
                            docId.Add(ds.Tables(0).Rows(i).Item(0))
                            docTypeId.Add(ds.Tables(0).Rows(i).Item(1))
                            UserId.Add(ds.Tables(0).Rows(i).Item(2))
                            ErrorFile = False
                        End If
                    Next
                End If
            Catch ex As Exception
                ErrorFile = True
                raiseerror(New Exception(fx.FullName & " Error obteniendo docid y doctypeid:" & ex.Message))
                RaiseNotifyErrors(fx.FullName & " Error obteniendo docid y doctypeid:" & ex.Message)
                Return Nothing
            End Try

            '  ZCore.LoadCore()
            'Obtengo result
            Try
                Dim j As Int32
                Dim DocType As DocType = Nothing
                Dim oldDoctypeid As Int32
                For i As Integer = 0 To docId.Count - 1
                    'Creo el Result por cada caratula o replica
                    If CInt(docTypeId(i)) <> oldDoctypeid Then
                        DocType = DocTypesBusiness.GetDocType(CInt(docTypeId(i)))
                    End If
                    Dim result1 As NewResult = Results_Business.GetNewNewResult(CInt(docId(i)), DocType)

                    For j = 0 To result1.Indexs.Count - 1
                        If result1.Indexs(j).id = 10586 Then result1.Indexs(j).data = caja
                        If result1.Indexs(j).id = 10588 Then result1.Indexs(j).data = lote
                        If result1.Indexs(j).id = 10589 Then result1.Indexs(j).data = IdCaratula
                        RaiseInfos("Caratula: " & IdCaratula & "Numero: " & i + 1 & " Caja: " & caja & " Lote: " & lote & "Info Monitoreo")
                    Next

                    'actualizo las doc_i
                    Try
                        Dim strupdate As String
                        If caja = 0 Then
                            strupdate = "Update doc_i" & docTypeId(i) & " set i10588='" & lote & "',i10589=" & IdCaratula & " Where doc_id=" & docId(i)
                        Else
                            strupdate = "Update doc_i" & docTypeId(i) & " set i10586=" & caja & " , i10588='" & lote & "',i10589=" & IdCaratula & " Where doc_id=" & docId(i)
                        End If
                        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
                    Catch ex As Exception
                        ErrorFile = True
                        Throw New Exception("Error actualizando Caja y Lote" & ex.ToString)
                        RaiseNotifyErrors("Error actualizando Caja y Lote" & ex.ToString)
                    End Try
                    result.Add(result1)
                Next
            Catch ex As Exception
                ErrorFile = True
                Trace.WriteLineIf(ZTrace.IsInfo, fx.FullName & " Error obteniendo result:" & ex.ToString)
                raiseerror(ex)
                RaiseNotifyErrors(fx.FullName & " Error obteniendo result:" & ex.ToString)
                Return Nothing
            End Try


            'Muevo la caratula al volumen y reemplazo la caratula generica
            Try
                Dim RB As New Results_Business

                'le saco el atrubuto de solo lectura para poder reemplazarlo
                For Each R As NewResult In result
                    RB.LoadVolume(R)
                    R.File = fx.FullName
                    RB.UpdateInsert(R, False, True, True, False, False)
                    RaiseInfos("Caratula " & IdCaratula & " INSERTADA en " & R.DocType.Name & " DOCID: " & R.ID, "Info Monitoreo")
                Next
                RB = Nothing
                fx.Attributes = FileAttributes.Normal
                Try
                    If ErrorFile = False Then fx.Delete()
                Catch ex As IOException
                    Trace.WriteLineIf(ZTrace.IsInfo, "no hay permiso en el volumen para borrar")
                End Try
            Catch ex As Exception
                'Trace.WriteLineIf(ZTrace.IsInfo,FM.FullFilename & " Error Moviendo doc_file-: " & ex.Message)
                Trace.WriteLineIf(ZTrace.IsInfo, fx.FullName & " Error Moviendo doc_file-: " & ex.Message)
                raiseerror(ex)
                Return Nothing
            End Try

            'Actualizo el estado de scanneado y la fecha,batch,box
            Try
                If Server.isOracle Then
                    ''Dim parNames() As String = {"caratulaid", "lote", "caja"}
                    'Dim parTypes() As Object = {10, 5, 10}
                    Dim parValues() As Object = {IdCaratula, lote, caja}

                    Server.Con.ExecuteNonQuery("UPDATE_SCANNEDBARCODE_PKG.Update_barcode", parValues)


                Else
                    ''Dim parNames() As String = {"@caratulaid", "@lote", "@caja"}
                    'Dim parTypes() As Object = {10, 5, 10}
                    Dim parvalues() As Object = {IdCaratula, lote, caja}
                    Server.Con.ExecuteNonQuery("UPDATE_BARCODE", parvalues)
                End If
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, fx.FullName & " Error Actaulizando  ZBarcode doc_file:" & ex.Message)
                raiseerror(ex)
                Return Nothing
            End Try

            'grabo la acci�n
            Try
                For i As Integer = 0 To docTypeId.Count - 1
                    UserBusiness.Rights.SaveAction(IdCaratula, ObjectTypes.ModuleMonitor, RightsType.Create, DocTypesBusiness.GetDocTypeName(docTypeId(i), True), UserId(i))
                Next
            Catch ex As Exception
                'Trace.WriteLineIf(ZTrace.IsInfo,FM.FullFilename & " Error Actaulizando  ZBarcode doc_file:" & ex.Message)
                Trace.WriteLineIf(ZTrace.IsInfo, fx.FullName & " Error Actaulizando  ZBarcode doc_file:" & ex.Message)
                raiseerror(ex)
                Return Nothing
            End Try
        Else
            raiseerror(New Exception("Files(0) esta en nothing"))
        End If
        Return Nothing
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