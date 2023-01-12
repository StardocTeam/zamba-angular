Public Class PlayDoAutoName
    Private _myrule As IDoAutoName
    'Private _barcodeId As Int64
    Private _res As Result
    'Private atributos As SortedList
    'Private indexvalue As String
    'Private ruleindicesaux As String
    'Private strItem As String
    'Private value As String
    'Private id As Int32
    'Private top As String
    'Private left As String

    Public Sub New(ByVal rule As IDoAutoName)
        _myrule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim newResults As New System.Collections.Generic.List(Of Core.ITaskResult)

        Dim RB As New Results_Business
        Try
            If Not _myrule.updateMultiple Then
                For Each r As ITaskResult In results
                    If _myrule.Seleccion = "Seleccion Actual" Then
                        RB.UpdateAutoName(r)
                    Else
                        Dim docTypeId As String
                        Dim docId As String
                        Dim docIds As Object
                        Dim nombreColumna As String
                        Try
                            docTypeId = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myrule.variabledoctypeid)
                            nombreColumna = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myrule.nombreColumna)
                            docIds = WFRuleParent.ReconocerVariablesAsObject(_myrule.variabledocid)
                        Catch ex As Exception
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al obtener variables")
                            Throw ex
                        End Try

                        If results.Count > 0 Then
                            docTypeId = TextoInteligente.ReconocerCodigo(docTypeId, results(0))
                            nombreColumna = TextoInteligente.ReconocerCodigo(nombreColumna, results(0))

                            If TypeOf (docIds) Is DataSet = False Then
                                docId = TextoInteligente.ReconocerCodigo(docIds, results(0))
                            End If
                        End If

                        If TypeOf (docIds) Is DataSet Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Es dataset")
                            For Each dr As DataRow In DirectCast(docIds, DataSet).Tables(0).Rows
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "DocID: " & dr(nombreColumna.Trim()).ToString())
                                _res = Results_Business.GetResult(CType(dr(nombreColumna.Trim()), Long), CType(docTypeId, Long))

                                RB.UpdateAutoName(_res)
                            Next
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No es dataset")
                            _res = Results_Business.GetResult(CType(docId, Long), CType(docTypeId, Long))

                            RB.UpdateAutoName(_res)
                        End If
                    End If

                    'Dim filepath As String = Zamba.Core.TextoInteligente.ReconocerCodigo(_myrule.FilePath, r)
                    'filepath = WFRuleParent.ReconocerVariables(filepath)
                    'Dim TextoToSave As String = Zamba.Core.TextoInteligente.ReconocerCodigo(_myrule.TextToSave, r)
                    'TextoToSave = WFRuleParent.ReconocerVariables(TextoToSave)
                    'Dim file As New FileInfo(filepath)
                    ' rutaTemp = Me._myrule.FilePath 'EnvironmentUtil.GetTempDir("\\temp").ToString & "\\" & DateTime.Now.ToString("dd-MM-yy HH-mm-ss") & file.Name & Me._myrule.FileExtension
                    'Dim pathtemp As Object = rutaTemp
                    'Dim filePathnew As String = String.Empty
                    'filePathnew = rutaTemp.Trim() + "\" + Me._myrule.FileName + " " + DateTime.Now.ToString("dd-MM-yy HH-mm-ss") + Me._myrule.FileExtension 'rutaTemp"
                    'Using sw As StreamWriter = New StreamWriter(filePathnew)
                    'sw.Write(TextoToSave)
                    'End Using
                    'My.Computer.FileSystem.WriteAllText(filePathnew, TextoToSave, False)
                    'Seteamos una variable que guarda el path del documento local
                    'If VariablesInterReglas.ContainsKey(Me._myrule.VarFilePath) = Nothing Then
                    'VariablesInterReglas.Add(Me._myrule.VarFilePath, filePathnew)
                    'Else
                    'VariablesInterReglas.Item(Me._myrule.VarFilePath) = filePathnew
                    'End If
                    'stream = New System.IO.StreamWriter(rutaTemp)
                    'stream.Write(TextoToSave)
                    'stream.Close()

                Next
            Else
                Dim docTypeBusinessObj As New DocTypesBusiness()

                For Each strDocTypeId As String In _myrule.docTypeIds.Split(New Char() {","})
                    'Se instancia el result con el que se va a trabajar
                    '(a estos fines prácticos usar el mismo result y cambiarle algunos datos
                    'es performático)
                    Dim docTypeId As Int64 = Int64.Parse(strDocTypeId)
                    Dim TableName As String = "DOC_T" & docTypeId

                    Dim dtResults As DataTable = Results_Business.GetAutoNameResults(docTypeId, _myrule.days)
                    Dim docTypeObj As DocType = docTypeBusinessObj.GetDocType(docTypeId, True)

                    'Si el DocType tiene asociado results
                    If dtResults.Rows.Count > 0 Then

                        Dim Result As New Result
                        Dim IndexsBusines As New IndexsBusiness()
                        Result.DocType = docTypeObj

                        For Each dr As DataRow In dtResults.Rows
                            Try
                                Result.ID = CType(dr("Doc_Id"), Long)
                                'Result.Name = CStr(dr("Name")).Trim()

                                'Lógica de Datos: se le cargan los atributos al result,
                                Result.Indexs.Clear()
                                Result.Indexs.AddRange(IndexsBusines.GetIndexsSchemaAsListOfDT(Result.DocTypeId, True))
                                RB.CompleteIndexData(Result)
                                RB.UpdateAutoName(Result)

                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                            End Try
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            RB = Nothing
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class