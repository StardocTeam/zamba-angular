Imports Zamba.Core
Imports Zamba.Data
'Imports zamba.DocTypes.Factory

<Ipreprocess.PreProcessName("Agregar Aguas"), Ipreprocess.PreProcessHelp("Monitoreo para Aguas Argentinas S.A., hecho para insertar documentos de tipo 'FACTURAS' y 'OP', que tengan en el nombre del archivo el codigo de LOTE y el número de FACTURA, separados por '-'. Parametros: TipoDocumento , LOTE , DOCUMENTO . Ver:1.1, 5/12/2005")> _
Public Class ippAguasInsert
    'Interfaces
    Implements Ipreprocess
    'Atributos
    Dim processed As Int32
    Dim Errors As Int32
    Dim DocTypeName As String = String.Empty
    '    Dim docTypeId As Int32 = -1
    Dim docType As DocType

    Private Index1Name As String = ""
    Private Index2Name As String = ""
    'constantes
    Private Const DTNAME As Int32 = 0
    Private Const INDLOTE As Int32 = 1
    Private Const INDDOC As Int32 = 2
    Private Const SEPDIR As String = "\"
    Private Const SEPUNIDAD As String = ":"
    Private Const SEPPARAMARCH As String = "-"
    Private Const SEPPARAM As String = ","
    Private Const CANTPARAMARCH As Int32 = 2
    Private Const CANTPARAM As Int32 = 3
    Private Const PARAMLOTE As String = "LOTE"
    'Implementacion de interfaces
    '----------------------------------
#Region "XML"
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:implementar
        Return String.Empty
    End Function
#End Region

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Monitoreo para Aguas Argentinas S.A., hecho para insertar documentos de tipo 'FACTURAS' y 'OP', que tengan en el nombre del archivo el codigo de LOTE y el número de FACTURA, separados por '-'. Parametros: TipoDocumento , LOTE , DOCUMENTO . Ver:1.1, 5/12/2005"
    End Function
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Function process(ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As ArrayList Implements Ipreprocess.process

        Trace.WriteLineIf(ZTrace.IsInfo, Files(0))
        If Not IsNothing(Files(0)) Then

            ' Dim fi As New IO.FileInfo(FM.FullFilename)

            'Obtengo los parametros
            Try
                If Not getParametros(param) Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "Error: Cantidad de parametros incorrecta,  cantidad esperada: " & CANTPARAM.ToString)
                    Errors += 1
                    RaiseEvent PreprocessError("Error: Cantidad de parametros incorrecta, cantidad esperada: " & CANTPARAM.ToString)
                    Return Nothing
                End If
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error Obteniendo Parametros " & ex.ToString)
                Return Nothing
            End Try

            'Obtengo el DTID
            Try
                docType = DocTypesBusiness.GetDocType(DocTypeName)
                Trace.WriteLineIf(ZTrace.IsInfo, "DocTypeID=" & docType.ID)
                If docType.ID = 0 Then
                    Zamba.Core.ZClass.raiseerror(New Exception("No existe el tipo de documento " & DocTypeName))
                    Return Nothing
                End If
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error asignado tipo de documento " & ex.ToString)
                Zamba.Core.ZClass.raiseerror(ex)
                Return Nothing
            End Try

            'Creo el documento a insertar
            Dim Document As NewResult
            Dim RB As Results_Business
            Trace.WriteLineIf(ZTrace.IsInfo, "Creo el documento")
            Trace.WriteLineIf(ZTrace.IsInfo, "Document=Results_Factory.GetNewNewResult(Me.docTypeId)")

            Document = RB.GetNewNewResult(docType)
            '  Document.DocTypeName = Me.DocTypeName
            Trace.WriteLineIf(ZTrace.IsInfo, "Document.DocTypeName=" & DocTypeName)
            RB = Nothing



            Dim Fin As IO.FileInfo
            '  Dim x As Int32

            Try
                processed += 1

                Dim index1Position As Int32
                Dim index2Position As Int32
                'Borro el contenido de los indices del documento
                borrarIndicesDocumento(Document)

                'Obtengo la posicion de los indices
                index1Position = posIndicePorNombre(Index1Name, Document)
                Trace.WriteLineIf(ZTrace.IsInfo, "Index1Position=" & index1Position)
                index2Position = posIndicePorNombre(Index2Name, Document)
                Trace.WriteLineIf(ZTrace.IsInfo, "Index2Position=" & index2Position)

                'Si no encuentra un indice termino la funcion
                If index1Position < 0 Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "El indice (1) ingresado no corresponde al tipo de documento")
                    Zamba.Core.ZClass.raiseerror(New Exception("El indice ingresado (1) no corresponde al tipo de documento"))
                    Return Nothing
                End If

                If index2Position < 0 Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "El indice (2) ingresado no corresponde al tipo de documento")
                    Zamba.Core.ZClass.raiseerror(New Exception("El indice ingresado (2) no corresponde al tipo de documento"))
                    Return Nothing
                End If

                'Creo el documento
                Document.File = Files(0)
                Trace.WriteLineIf(ZTrace.IsInfo, "Document.File=" & Files(0))

                'Para obtener el nombre del archivo
                Fin = New IO.FileInfo(Files(0))

                'obtengo los parametros del nombre del archivo, que estan separados por -
                Dim str(), sArch As String
                'Quito la extension del archivo
                sArch = Fin.Name.Split(".")(0) 'Fin.Name.Split(SEPPARAMARCH)
                'Obtengo los parametros del nombre del archivo
                str = sArch.Split(SEPPARAMARCH)

                'Verifico si la cant de parametros en el nombre del archivo es correcta
                If str.Length <> CANTPARAMARCH Then
                    'error en cant de parametros en el nombre del archivo
                    Trace.WriteLineIf(ZTrace.IsInfo, "Error: Cantidad de parametros en el nombre del archivo incorrecta, cantidad encontrada: " & str.Length.ToString & ", cantidad esperada: " & CANTPARAMARCH.ToString)
                    Errors += 1
                    RaiseEvent PreprocessError("Error: Cantidad de parametros en el nombre del archivo incorrecta, cantidad encontrada: " & str.Length.ToString & ", cantidad esperada: " & CANTPARAMARCH.ToString)
                    Return Nothing
                End If

                'Meto donde corresponde los valores de los parametros
                ' Dim sParams() As String
                ' sParams = param.ToArray
                'If param(1).Trim.ToUpper = PARAMLOTE Then
                If String.Compare(param(1), PARAMLOTE, True) = 0 Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "Indice donde se va a poner")
                    Trace.WriteLineIf(ZTrace.IsInfo, Document.Indexs(index1Position).Name)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Valor que se va a poner")
                    Trace.WriteLineIf(ZTrace.IsInfo, str(0).Trim)

                    Document.Indexs(index1Position).data = str(0).Trim
                    Document.Indexs(index1Position).datatemp = str(0).Trim

                    Trace.WriteLineIf(ZTrace.IsInfo, "Indice donde se va a poner")
                    Trace.WriteLineIf(ZTrace.IsInfo, Document.Indexs(index2Position).Name)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Valor que se va a poner")
                    Trace.WriteLineIf(ZTrace.IsInfo, str(1).Trim)

                    Document.Indexs(index2Position).data = CInt(str(1).Trim)
                    Document.Indexs(index2Position).datatemp = CInt(str(1).Trim)
                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "Indice donde se va a poner")
                    Trace.WriteLineIf(ZTrace.IsInfo, Document.Indexs(index1Position).Name)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Valor que se va a poner")
                    Trace.WriteLineIf(ZTrace.IsInfo, str(0).Trim)

                    Document.Indexs(index1Position).data = str(1).Trim
                    Document.Indexs(index1Position).datatemp = str(1).Trim

                    Trace.WriteLineIf(ZTrace.IsInfo, "Indice donde se va a poner")
                    Trace.WriteLineIf(ZTrace.IsInfo, Document.Indexs(index2Position).Name)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Valor que se va a poner")
                    Trace.WriteLineIf(ZTrace.IsInfo, str(1).Trim)

                    Document.Indexs(index2Position).data = CInt(str(1).Trim)
                    Document.Indexs(index2Position).datatemp = CInt(str(1).Trim)
                End If

                'Inserto el documento
                ' Dim iStationId As Int32
                Document.FolderId = CoreData.GetNewID(IdTypes.FOLDERSID)

                Results_Business.InsertDocument(Document, True, False, False, False)
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsInfo, "Error " & Errors & " " & ex.ToString)
                Errors += 1
                RaiseEvent PreprocessError(ex.ToString)
            End Try
        End If
        Return Nothing
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        'TODO:Implementar
        Return String.Empty
    End Function
    'Funciones
    '------------------
    Private Shared Function posIndicePorNombre(ByVal IndNombre As String, ByVal Document As NewResult) As Int32
        Dim i, iPos As Int32
        iPos = -1
        i = 0
        If Not IsNothing(Document) Then
            'Limpio el objeto document y busco la pos del indice 
            'For i = 0 To Document.Indexs.Count - 1
            'Document.Indexs(i).data = ""
            Trace.WriteLineIf(ZTrace.IsInfo, "posIndicePorNombre")
            Trace.WriteLineIf(ZTrace.IsInfo, "Indices: " & Document.Indexs.Count - 1)
            While i < Document.Indexs.Count AndAlso iPos = -1
                If Document.Indexs(i).name.trim.toupper = IndNombre.ToUpper Then
                    iPos = i
                    Trace.WriteLineIf(ZTrace.IsInfo, "Posicion: " & iPos)
                End If
                'Next
                i += 1
            End While
        End If
        Trace.WriteLineIf(ZTrace.IsInfo, "devuelve la posicion: " & iPos)
        Return iPos
    End Function
    Private Shared Sub borrarIndicesDocumento(ByRef Document As NewResult)
        Dim i As Int32
        For i = 0 To Document.Indexs.Count - 1
            Try
                Document.Indexs(i).data = ""
                Document.Indexs(i).datatemp = ""
            Catch
            End Try
        Next
    End Sub
    'Parametros DOCTYPENAME,LOTE,DOCUMENTO
    Private Function getParametros(ByVal param As ArrayList) As Boolean
        ' Dim params() As String = param.ToArray
        Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo los parametros, Cantidad: " & param.Count)
        If param.Count = CANTPARAM Then
            DocTypeName = param(0).Trim 'params(DTNAME)
            Trace.WriteLineIf(ZTrace.IsInfo, "Me.DoctypeName=" & param(0).Trim)

            Index1Name = param(1).Trim 'params(INDLOTE)
            Trace.WriteLineIf(ZTrace.IsInfo, "Me.Index1Name=" & param(1).Trim)

            Index2Name = param(2).Trim 'params(INDDOC)
            Trace.WriteLineIf(ZTrace.IsInfo, "Me.Index2Name=" & param(2).Trim)

            Return True
        Else
            Trace.WriteLineIf(ZTrace.IsInfo, "La cantidad de parametros no coincide")
            Return False
        End If
    End Function
End Class
