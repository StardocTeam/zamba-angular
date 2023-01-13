Imports System.IO
Imports Zamba.Servers
Imports Zamba.Core
'Imports zamba.DocTypes.Factory

<Ipreprocess.PreProcessName("Insertar Código de Barras"), Ipreprocess.PreProcessHelp("Reemplazo la imagen correcta de caratula por la imagen por defecto de caratulas. Recibe como parametro el ID de indice de la caratula, en caso de no haber, poner cero ")>
Public Class ippBarcodeCodaBar
    Inherits ZClass
    Implements Ipreprocess

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Reemplazo la imagen correcta de caratula por la imagen por defecto de caratulas. "
    End Function
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implementar
        Return String.Empty
    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml
    End Sub

    Public Overrides Sub Dispose()

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
        Dim completePdfDocPages As Int64

        ZTrace.WriteLineIf(ZTrace.IsInfo, "process barcodeinsert")
        If Not IsNothing(Files(0)) Then

            If IsNothing(param) OrElse param.Count = 0 Then
                raiseerror(New Exception("No se ha configurado el atributo que llevara el Id de caratula"))
                Return Nothing
            Else
                If Int64.TryParse(param(0), caratulaIndex) = False Then
                    raiseerror(New Exception("El Id de caratula no es un numero valido"))
                    Return Nothing
                End If
            End If

            'Obtengo doctypeid y docid
            Try
                fs = New FileInfo(Files(0))
                fx = MovetoBarcodeFolderTemp(fs)
                completePdfDocPages = New Spire.Pdf.PdfDocument(fs.FullName).Pages.Count

                If IsNothing(fx) Then Return Nothing
                IdCaratula = fx.Name.Substring(0, fx.Name.Length - 4).Replace("A", String.Empty).ToString

                'Busco la caratula en la Base de datos
                Dim strSelect As String

                strSelect = "Select doc_id,doc_type_id,userid,scanned from zbarcode where id=" & IdCaratula
                ZTrace.WriteLineIf(ZTrace.IsInfo, "qry: " + strSelect)

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
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Reemplaza")
                            docId.Add(ds.Tables(0).Rows(i).Item(0))
                            docTypeId.Add(ds.Tables(0).Rows(i).Item(1))
                            UserId.Add(ds.Tables(0).Rows(i).Item(2))
                            ErrorFile = False
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
                raiseerror(ex)
                raiseerror(New Exception(fx.FullName & " Error obteniendo docid y doctypeid:" & ex.Message))
                RaiseNotifyErrors(fx.FullName & " Error obteniendo docid y doctypeid:" & ex.Message)
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
                Return Nothing
            End Try

            'Obtengo result
            Try
                Dim j As Int32
                Dim DocType As DocType = Nothing
                Dim olddocTypeId As Int64
                For i As Integer = 0 To docId.Count - 1
                    'Creo el Result por cada caratula o replica
                    If CInt(docTypeId(i)) <> olddocTypeId Then
                        DocType = DocTypesBusiness.GetDocType(CInt(docTypeId(i)), True)
                        olddocTypeId = docTypeId(i)
                    End If
                    Dim result1 As NewResult = Results_Business.GetNewNewResult(CInt(docId(i)), DocType)

                    For j = 0 To result1.Indexs.Count - 1
                        If result1.Indexs(j).ID = caratulaIndex Then result1.Indexs(j).Data = IdCaratula
                        RaiseInfos("Caratula: " & IdCaratula & "Numero: " & i + 1 & " Info Monitoreo")
                    Next

                    'actualizo las doc_i
                    Try
                        Dim strupdate As String
                        strupdate = String.Format("update doc_i{0} set i{1} = {2}, i{3} = {4} where doc_id = {5}", DocType.ID, caratulaIndex, IdCaratula, param(1), completePdfDocPages, docId(i))
                        'strupdate = "Update doc_i" & docTypeId(i) & " set i" & caratulaIndex & "=" & IdCaratula & ",i" & param(1) & "='" &  & "'  Where doc_id=" & docId(i)

                        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)

                    Catch ex As Exception
                        ErrorFile = True
                        Throw New Exception("Error actualizando Caratula con indice: " & caratulaIndex & " - Error: " & ex.ToString)
                        RaiseNotifyErrors("Error actualizando Caratula" & ex.ToString)
                    End Try
                    result.Add(result1)
                Next
            Catch ex As Exception
                ErrorFile = True
                ZTrace.WriteLineIf(ZTrace.IsInfo, fx.FullName & " Error obteniendo result:" & ex.ToString)
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
                    RB.UpdateInsert(R, False, True, True, False, False, False)
                    RaiseInfos("Caratula " & IdCaratula & " INSERTADA en " & R.DocType.Name & " DOCID: " & R.ID, "Info Monitoreo")
                Next
                RB = Nothing

                Try
                    If ErrorFile = False Then fx.Delete()
                Catch ex As IOException
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "no hay permiso en el volumen para borrar")
                End Try
            Catch ex As Exception
                'ZTrace.WriteLineIf(ZTrace.IsInfo,FM.FullFilename & " Error Moviendo doc_file-: " & ex.Message)
                ZTrace.WriteLineIf(ZTrace.IsInfo, fx.FullName & " Error Moviendo doc_file-: " & ex.Message)
                raiseerror(ex)
                Return Nothing
            End Try

            'Actualizo el estado de scanneado y la fecha,batch,box
            Try
                If Server.isOracle Then
                    ''Dim parNames() As String = {"caratulaid", "lote", "caja"}
                    'Dim parTypes() As Object = {10, 5, 10}
                    Dim parValues() As Object = {IdCaratula, 0, 0}

                    Server.Con.ExecuteNonQuery("UPDATE_SCANNEDBARCODE_PKG.Update_barcode", parValues)


                Else
                    ''Dim parNames() As String = {"@caratulaid", "@lote", "@caja"}
                    'Dim parTypes() As Object = {10, 5, 10}
                    Dim parvalues() As Object = {IdCaratula, 0, 0}

                    Server.Con.ExecuteNonQuery("UPDATE_BARCODE", parvalues)

                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, fx.FullName & " Error Actaulizando  ZBarcode doc_file:" & ex.Message)
                raiseerror(ex)
                Return Nothing
            End Try

            'grabo la acción
            Try
                For i As Integer = 0 To docTypeId.Count - 1
                    UserBusiness.Rights.SaveAction(IdCaratula, ObjectTypes.ModuleMonitor, RightsType.Create, DocTypesBusiness.GetDocTypeName(docTypeId(i), True))
                Next
            Catch ex As Exception
                'ZTrace.WriteLineIf(ZTrace.IsInfo,FM.FullFilename & " Error Actaulizando  ZBarcode doc_file:" & ex.Message)
                ZTrace.WriteLineIf(ZTrace.IsInfo, fx.FullName & " Error Actualizando  ZBarcode doc_file:" & ex.Message)
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
