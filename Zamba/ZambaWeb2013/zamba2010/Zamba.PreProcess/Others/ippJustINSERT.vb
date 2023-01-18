Imports ZAMBA.Core
Imports Zamba.Data
'Imports zamba.DocTypes.Factory

<Ipreprocess.PreProcessName("Insertar Documento"), Ipreprocess.PreProcessHelp("Inserta el documento monitoreado. Recibe como parámetro el nombre del tipo de documento, el nombre del índice asociado al nombre del archivo y un parámetro opcional (UPPER,LOWER) que especifica si los datos se insertan en mayúscula o minúscula, si el nombre del indice se omite, no se cargara ningun indice")> _
Public Class ippJustINSERT
    Inherits ZClass
    Implements Ipreprocess

    Public Overrides Sub Dispose()

    End Sub
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Inserta el documento monitoreado. Recibe como parámetro el nombre del tipo de documento, el nombre del índice asociado al nombre del archivo y un parámetro opcional (UPPER,LOWER) que especifica si los datos se insertan en mayúscula o minúscula, si el nombre del indice se omite, no se cargara ningun indice"
    End Function

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implementar
        Return String.Empty
    End Function

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Dim processed As Int32
    Dim Errors As Int32
    Dim Doctypename As String = "DocType"
    'Dim docTypeId As Int32 = -1
    Dim DocType As DocType

    'Dim UpperOrLower As String = ""
    Dim IndexName As String = "Index"


    Private Sub GetParameters(ByVal param As ArrayList)
        'Dim params As String() = param.Split(",")
        'Dim params As String() = param.ToArray
        ' Dim i As Integer
        Doctypename = param(0)
        IndexName = param(1)
        Try
            ' Me.UpperOrLower = param(2)
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsInfo, "Error Obteniendo Parametros " & ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Procesa la inserción de documentos desde un preproceso
    ''' </summary>
    ''' <param name="Files"></param>
    ''' <param name="param"></param>
    ''' <param name="xml"></param>
    ''' <param name="Test"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Tomás] 15/09/2009 Modified - Se corrige un error en la inserción con el tema de la propiedad .datatemp
    '''                               También se optimizan comparaciones de strings.
    ''' </history>
    Public Function process(ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As ArrayList Implements Ipreprocess.process
        'Trace.WriteLineIf(ZTrace.IsInfo,FM.FullFilename)
        Trace.WriteLineIf(ZTrace.IsInfo, Files(0))
        'If Not IsNothing(FM.FullFilename) Then
        If Not IsNothing(Files(0)) Then
            'Dim fi As New IO.FileInfo(FM.FullFilename)
            Dim fi As New IO.FileInfo(Files(0))

            Try
                GetParameters(param)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error Obteniendo Parametros " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                Return Nothing
            End Try

            Try
                DocType = DocTypesFactory.GetDocType(Doctypename)
                If DocType.ID = 0 Then
                    zamba.core.zclass.raiseerror(New Exception("No existe el tipo de documento " & Doctypename))
                    Return Nothing
                End If
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error asignado tipo de documento " & ex.ToString)
                zamba.core.zclass.raiseerror(ex)
                Return Nothing
            End Try


            Dim Document As NewResult
            Dim RB As New Results_Business
            Document = RB.GetNewNewResult(DocType)
            RB = Nothing
            '  Document.DocTypeName = Me.Doctypename

            Dim Fin As IO.FileInfo
            ' Dim x As Int32
            Try
                processed += 1

                Dim indexPosition As Int32 = -1

                Dim i As Integer
                'Limpio el objeto document y busco la pos del indice 
                For i = 0 To Document.Indexs.Count - 1
                    Document.Indexs(i).data = ""
                    If String.Compare(Document.Indexs(i).name.toupper, IndexName.ToUpper) = 0 Then
                        indexPosition = i
                        Exit For
                    End If
                Next
                If indexPosition = -1 AndAlso Not String.IsNullOrEmpty(IndexName) Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "El indice ingresado no corresponde al tipo de documento")
                    raiseerror(New Exception("El indice ingresado no corresponde al tipo de documento"))
                    Return Nothing
                End If

                'Document.File = FM.FullFilename
                Document.File = Files(0)

                'Fin = New IO.FileInfo(FM.FullFilename)
                Fin = New IO.FileInfo(Files(0))

                'TODO Falta decir cual es el la posicion del indice

                '[Tomas - 15/09/2009]   Se agrega la asignacion a la propiedad .dataTemp ya 
                '                       en la inserción se toma ese valor y los valores de 
                '                       índices nunca eran insertados. 
                Dim tempData As String = Fin.Name.Substring(0, fi.Name.LastIndexOf("."))
                If Not IsNothing(param) AndAlso Not String.IsNullOrEmpty(IndexName) Then
                    If param.IndexOf("UPPER") <> -1 Then
                        Document.Indexs(indexPosition).data = tempData.ToUpper
                        Document.Indexs(indexPosition).dataTemp = tempData.ToUpper
                    Else
                        If param.IndexOf("LOWER") <> -1 Then
                            Document.Indexs(indexPosition).data = tempData.ToLower
                            Document.Indexs(indexPosition).dataTemp = tempData.ToLower
                        Else
                            Document.Indexs(indexPosition).data = tempData
                            Document.Indexs(indexPosition).dataTemp = tempData
                        End If
                    End If
                ElseIf Not String.IsNullOrEmpty(IndexName) Then
                    Document.Indexs(indexPosition).data = tempData
                    Document.Indexs(indexPosition).dataTemp = tempData
                End If
                tempData = Nothing
                Document.FolderId = CoreData.GetNewID(IdTypes.FOLDERSID)
                Results_Business.InsertDocument(Document, True, False, True, False)

                Dim caja As Int64
                Dim lote As String = String.Empty
                Dim Reemplaza As Boolean
                Dim IndexId As Int64
                Dim IndexValue As Int32

                'Carga en las variables pasadas los valores ingresos para caja y lote
                ReadBoxAndBatch(caja, lote, Reemplaza, IndexId, IndexValue)
                GC.Collect()

                'actualizo las doc_i con la caja y lote
                Try
                    RaiseInfos("Caja: " & caja & " Lote: " & lote & "Indice: " & IndexId & " docid: " & Document.ID, "Info Monitoreo")
                    Dim strupdate As String = String.Format("Update doc_i" & DocType.ID & " set i25=" & caja & " , i26='" & lote & "', i255= '" & IndexId & "' Where doc_id=" & Document.ID, IndexId, IndexValue)
                    Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
                Catch ex As Exception
                    Throw New Exception("Error actualizando Caja y Lote" & ex.ToString)
                End Try



            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error " & Errors.ToString & " " & ex.ToString)
                Errors += 1
                RaiseEvent PreprocessError(ex.ToString)
            End Try
        End If
        Return Nothing
    End Function

    Private Shared Sub ReadBoxAndBatch(ByRef caja As Int64, ByRef lote As String, ByRef Reemplaza As Boolean, ByRef IndexId As Int64, ByRef IndexValue As String)
        Dim ds As DsCajaLote = Nothing
        Try
            ds = New DsCajaLote
            If IO.File.Exists(Application.StartupPath & "\CajayLote.xml") Then
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


    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        'TODO:Implementar
        Return String.Empty
    End Function

    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml
    End Sub


    Public Shared ReadOnly Property Id() As Integer
        Get
            Return ProcessFactory.GetProcessIDByName(Name())
        End Get
    End Property

    Public Shared ReadOnly Property Name() As String
        Get
            Return "Just Insert"
        End Get
    End Property
End Class
