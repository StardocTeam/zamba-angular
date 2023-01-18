Imports System.IO
Imports Zamba.Servers.Server
Imports Zamba.Core
Imports Zamba.Servers

'Imports zamba.DocTypes.Factory

<Ipreprocess.PreProcessName("Insertar Código de Barras"), Ipreprocess.PreProcessHelp("Reemplazo la imagen correcta de caratula por la imagen por defecto de caratulas. Lee un archivo CajaYLote.xml, el cual puede estar ubicado en C:\, C:\Monitoreo\ o en el directorio de la aplicación")> _
Public Class ippBarcodeInsert
    Inherits ZClass
    Implements Ipreprocess

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Reemplazo la imagen correcta de caratula por la imagen por defecto de caratulas. Lee un archivo CajaYLote.xml, el cual esta ubicado en el directorio de la aplicación"
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
            Throw New Exception("Error leyendo Caja Y Lote " & ex.ToString)
        Finally
            Try
                ds.Dispose()
                ds = Nothing
                GC.Collect()
            Catch
            End Try
        End Try
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

        Dim fs As FileInfo
        Dim fx As FileInfo = Nothing
        Dim caja As Int64
        Dim lote As String = String.Empty
        Dim Reemplaza As Boolean
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
                Dim strSelect As String = String.Empty
                strSelect = "Select doc_id,doc_type_id,userid,scanned from zbarcode where id=" & IdCaratula

                'Me fijo cuantas replicas trajo
                Dim ds As New DataSet
                ds = Con.ExecuteDataset(CommandType.Text, strSelect)
                If ds.Tables(0).Rows.Count = 0 Then
                    'No encontro la caratula
                    Throw New Exception("No se encontro caratulaid en zbarcode de:" & IdCaratula)
                    RaiseInfos("No se encontro caratulaid en zbarcode de:" & IdCaratula, "Error Monitoreo")
                Else
                    'Por cada caratula o replica actualizo el result correspondiente
                    Dim FirstDocument As Boolean = True
                    Dim i As Integer
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        RaiseInfos("Caratula Y Replicas " & ds.Tables(0).Rows.Count & " Caratula: " & IdCaratula, "Info Monitoreo")

                        Dim DocTypeId, UserId As Int32
                        Dim DocId As Int64

                        'verifico si ya fue scaneada o esta puesto para reemplazo
                        If IsDBNull(ds.Tables(0).Rows(i).Item("scanned")) = False AndAlso (ds.Tables(0).Rows(i).Item("scanned") = "Si" OrElse ds.Tables(0).Rows(i).Item("scanned") = "SI" OrElse ds.Tables(0).Rows(i).Item("scanned") = "si") AndAlso Reemplaza = False Then
                            Try
                                ErrorFile = True
                                Dim sw As New StreamWriter(fx.Directory.FullName & "\NO REEMPLAZADA" & fx.Name.Substring(0, fx.Name.Length - 4) & ".log", True)
                                sw.WriteLine("Imagen ya scaneada, si se quiere reemplazar marcar Xml Caja y Lote para reemplazo")
                                sw.Flush()
                                sw.Close()
                                sw = Nothing
                                RaiseInfos("Imagen ya scaneada, si se quiere reemplazar marcar Xml Caja y Lote para reemplazo" & IdCaratula, "Error Monitoreo")
                            Catch ex As Exception
                                Throw New Exception("Error escribiendo archivo de logerror de reemplazo " & ex.ToString)
                            End Try
                        Else
                            DocId = ds.Tables(0).Rows(i).Item(0)
                            DocTypeId = ds.Tables(0).Rows(i).Item(1)
                            UserId = ds.Tables(0).Rows(i).Item(2)
                            ErrorFile = False
                        End If

                        'Creo el Result por cada caratula o replica
                        Dim R As NewResult = Results_Business.GetNewNewResult(DocId, DocTypesBusiness.GetDocType(DocTypeId))
                        Dim j As Int16
                        For j = 0 To R.Indexs.Count - 1
                            If R.Indexs(j).id = 25 Then R.Indexs(j).data = caja
                            If R.Indexs(j).id = 26 Then R.Indexs(j).data = lote
                            If R.Indexs(j).id = 386 Then R.Indexs(j).data = IdCaratula
                        Next
                        RaiseInfos("Caja: " & caja & " Lote: " & lote & " Caratula: " & IdCaratula & "Numero: " & i + 1, "Info Monitoreo")
                        'actualizo las doc_i con la caja y lote
                        Try
                            Dim strupdate As String = "Update doc_i" & DocTypeId & " set i25=" & caja & " , i26='" & lote & "',i386=" & IdCaratula & " Where doc_id=" & DocId
                            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
                        Catch ex As Exception
                            ErrorFile = True
                            Throw New Exception("Error actualizando Caja y Lote" & ex.ToString)
                        End Try


                        'Actualizo el doc_file en la Doc_T
                        Dim RB As Results_Business

                        Try
                            If FirstDocument Then
                                FirstDocument = False
                                RB.LoadVolume(R)
                                R.File = fx.FullName
                                RB.UpdateMonitorInsert(R, True)
                                RaiseInfos("Caratula Insertada en " & R.DocType.Name & " DOCID: " & R.ID & " Caratula: " & IdCaratula, "Info Monitoreo")
                            End If
                        Catch ex As Exception
                            raiseerror(ex)
                        End Try

                        RB = Nothing


                        'grabo la acción
                        UserBusiness.Rights.SaveAction(IdCaratula, ObjectTypes.ModuleMonitor, RightsType.Create, R.DocType.Name)
                        Try
                            'Actualizo el estado de scanneado y la fecha,batch,box
                            If Server.isOracle Then

                                Dim parValues() As Object = {IdCaratula, lote, caja}
                                Con.ExecuteNonQuery("UPDATE_SCANNEDBARCODE_BOX_LOT_PKG.Update_barcode",
                                 parValues)
                            Else
                                Dim parvalues() As Object = {IdCaratula, lote, caja}
                                Con.ExecuteNonQuery("UPDATE_BARCODE_BOX_LOT", parvalues)
                            End If
                        Catch ex As Exception
                            raiseerror(ex)
                        End Try
                    Next

                    'Termine de recorrer la caratula y sus replicas

                    'Borro el archivo origen
                    Try
                        fx.Attributes = FileAttributes.Normal
                        If ErrorFile = False Then fx.Delete()
                    Catch ex As IOException
                        raiseerror(ex)
                    End Try

                    'Actualizo el estado de scanneado y la fecha,batch,box
                    Try
                        If Server.isOracle Then

                            ''Dim parNames() As String = {"caratulaid", "lote", "caja"}
                            'Dim parTypes() As Object = {10, 5, 10}
                            Dim parValues() As Object = {IdCaratula, lote, caja}
                            Con.ExecuteNonQuery("UPDATE_SCANNEDBARCODE_PKG.Update_barcode",
                             parValues)
                        Else

                            ''Dim parNames() As String = {"@caratulaid", "@lote", "@caja"}
                            'Dim parTypes() As Object = {10, 5, 10}
                            Dim parvalues() As Object = {IdCaratula, lote, caja}
                            Con.ExecuteNonQuery("UPDATE_BARCODE", parvalues)
                        End If
                    Catch ex As Exception
                        raiseerror(ex)
                    End Try

                End If
            Catch ex As Exception
                ErrorFile = True
                raiseerror(New Exception(fx.FullName & " Error obteniendo docid y doctypeid:" & ex.Message))
                RaiseInfos(fx.FullName & " Error obteniendo docid y doctypeid:" & ex.Message, "Error Monitoreo")
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
    Public Overrides Sub Dispose()
    End Sub
End Class
