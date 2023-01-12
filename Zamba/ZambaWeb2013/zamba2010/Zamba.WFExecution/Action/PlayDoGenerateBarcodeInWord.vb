Imports System.IO
Imports Zamba.Core.WF.WF
Imports Zamba.FileTools

Public Class PlayDoGenerateBarcodeInWord

    Private _myrule As IDoGenerateBarcodeInWord
    ''' <summary>
    ''' Constructor de la ejecución de la regla PlayDoGenerateBarcodeInWord
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <history>
    '''     Javier 28/10/2010   Created
    ''' </history>
    Public Sub New(ByVal rule As IDoGenerateBarcodeInWord)
        Me._myrule = rule
    End Sub

    ''' <summary>
    ''' La regla genera una carátula en word, la muestra en pantalla y dependiendo de la configuracion, pega el código de barras, imprime automaticamente, etc
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier 28/10/2010   Created
    '''     Javier 02/12/2010   Modified     reconocervariables and reconocertextointeligente added in template path
    ''' </history>
    ''' 
    Public Sub ConstruirDirectorioSiNoExiste(Path As String)
        Dim SubCarpetas() As String
        SubCarpetas = Path.Split("\")
        Dim CreandoRuta As String = SubCarpetas(0)
        For i As Int32 = 1 To SubCarpetas.Length - 1
            CreandoRuta += "\" + SubCarpetas(i)
            If (Not Directory.Exists(CreandoRuta)) Then
                Directory.CreateDirectory(CreandoRuta)
            End If
        Next i
    End Sub
    Public Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
        Dim newResults As New List(Of ITaskResult)
        Dim _res As NewResult
        Dim _barcodeId, auxint64 As Int64
        Dim path, texto, top, left, extension As String

        Dim file As FileInfo = Nothing
        Dim source As Object
        Dim cambiosZvar As Hashtable = Nothing
        Dim cambiosTextInt As Hashtable = Nothing
        Dim VarInterReglas As New VariablesInterReglas()
        Dim spireOffice As Zamba.FileTools.SpireTools = Nothing

        Dim DTB As New DocTypesBusiness
        Dim RB As New Results_Business
        Dim WFTB As New WFTaskBusiness
        Try
            spireOffice = New Zamba.FileTools.SpireTools
            For Each r As ITaskResult In results
                _res = New NewResult()
                _res.Parent = DTB.GetDocType(_myrule.docTypeId)
                _res.Indexs = ZCore.GetInstance().FilterIndex(_myrule.docTypeId)
                _res.ID = ToolsBusiness.GetNewID(IdTypes.DOCID)

                RB.LoadVolume(_res)
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "ID Documento: " & _res.ID)

                source = TextoInteligente.ReconocerCodigo(_myrule.FilePath, r)
                source = VarInterReglas.ReconocerVariablesAsObject(source)

                Dim random As New Random(Now.Millisecond) 'Dejar la instancia dentro del foreach
                ConstruirDirectorioSiNoExiste(Membership.MembershipHelper.AppTempPath & "\temp\")
                If TypeOf source Is Byte() Then
                    extension = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._myrule.DocPathVar)
                    If Not extension.StartsWith(".") Then extension = "." & extension
                    path = Membership.MembershipHelper.AppTempPath & "\temp\" & random.Next(1000000, 9999999).ToString & extension

                    FileEncode.Decode(path, DirectCast(source, Byte()))
                Else
                    path = source.ToString
                    file = New FileInfo(path)
                    path = Membership.MembershipHelper.AppTempPath & "\temp\" & random.Next(1000000, 9999999).ToString & file.Name
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ruta del archivo: " & path)
                    file.CopyTo(path, True)
                    file = Nothing
                End If
                random = Nothing

                'Se reconoce variables
                texto = spireOffice.GetText(path)
                cambiosZvar = VarInterReglas.ReconocerVariablesValuesSoloTextoAsHashTB(texto)
                cambiosTextInt = TextoInteligente.ReconocerCodigoAsHashTB(texto, r)

                'Se reemplaza el contenido del word
                spireOffice.ReplaceInWord(path, cambiosZvar, cambiosTextInt)

                'Si se configuro la regla para que se inserte código de barras
                If Not Me._myrule.InsertBarcode Then
                    'Insertar documento normalmente
                    _res.File = path
                    _res.Disk_Group_Id = 0

                    If Me._myrule.WithoutInsert Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El documento no   fué insertado por la configuración de la regla")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Insertando documentacion")
                        Select Case (RB.Insert(_res, False, False, False, False, False))
                            Case InsertResult.Insertado
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertado en Zamba ")
                            Case InsertResult.ErrorIndicesIncompletos
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de atributos obligatorios")
                            Case InsertResult.ErrorIndicesInvalidos
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar, hay indices con datos invalidos")
                        End Select
                    End If
                Else
                    If Me._myrule.WithoutInsert Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El documento no   fué insertado por la configuración de la regla")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Insertando documentacion")
                        Select Case (RB.Insert(_res, False, False, False, False, False))
                            Case InsertResult.Insertado
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertado en Zamba ")
                            Case InsertResult.ErrorIndicesIncompletos
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de atributos obligatorios")
                            Case InsertResult.ErrorIndicesInvalidos
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar, hay indices con datos invalidos")
                        End Select
                    End If
                End If
                If Not Me._myrule.ContinueWithCurrentTasks Then
                    Dim task As ITaskResult = WFTB.GetTaskByDocIdAndWorkFlowId(_res.ID, 0)
                    If Not IsNothing(task) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se adquiere la tarea del WF: " & task.WorkId)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "La tarea no se encuentra en ningun WF, se agrega una tarea vacia")
                        task = New TaskResult()
                        task.ID = _res.ID
                        task.Indexs = _res.Indexs
                        task.DocType = _res.DocType
                        task.DocTypeId = _res.DocTypeId
                        task.OffSet = _res.OffSet
                        If Not String.IsNullOrEmpty(_res.File) Then
                            task.File = _res.File
                        End If
                        task.Disk_Group_Id = _res.Disk_Group_Id
                        task.DISK_VOL_PATH = _res.DISK_VOL_PATH
                        task.Doc_File = _res.Doc_File
                    End If
                    newResults.Add(task)
                End If
            Next

        Finally
            'Liberacion de memoria y procesos de word en caso de ser necesario
            file = Nothing
            VarInterReglas = Nothing
            spireOffice = Nothing
            If cambiosZvar IsNot Nothing Then
                cambiosZvar.Clear()
                cambiosZvar = Nothing
            End If
            If cambiosTextInt IsNot Nothing Then
                cambiosTextInt.Clear()
                cambiosTextInt = Nothing
            End If
        End Try

        If Me._myrule.SaveDocPathVar Then
            Dim docPath As String = Me._myrule.DocPathVar
            If docPath.StartsWith("zvar(") AndAlso docPath.EndsWith(")") Then
                docPath = docPath.Replace("zvar(", String.Empty)
                docPath = docPath.Remove(docPath.Length - 1, 1)
            End If

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando ruta del word: " & path)

            'Seteamos una variable que guarda el path del documento local
            If VariablesInterReglas.ContainsKey(docPath) Then
                VariablesInterReglas.Item(docPath) = path
            Else
                VariablesInterReglas.Add(docPath, path)
            End If
        End If

        'Si se configuró la regla para que continue con las tareas actuales
        If Me._myrule.ContinueWithCurrentTasks Then
            Return results
        Else
            Return newResults
        End If

        DTB = Nothing
        RB = Nothing
        WFTB = Nothing
    End Function

End Class