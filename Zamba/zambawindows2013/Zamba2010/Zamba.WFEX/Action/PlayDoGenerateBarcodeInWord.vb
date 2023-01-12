Imports Zamba.Core
Imports Zamba.Data
Imports System.IO
Imports Zamba.Tools
Imports Zamba.Core.WF.WF

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
    Public Function Play(ByVal results As List(Of Core.ITaskResult)) As List(Of ITaskResult)
        Dim newResults As New List(Of ITaskResult)
        Dim _res As NewResult
        Dim _barcodeId, auxint64 As Int64
        Dim path, texto, top, left, extension As String

        Dim wi As Zamba.Office.WordInterop = Nothing
        Dim wordapp As Office.WordApplication = Nothing
        Dim file As FileInfo = Nothing
        Dim wordobj, source As Object
        Dim cambiosZvar As Hashtable = Nothing
        Dim cambiosTextInt As Hashtable = Nothing

        Try
            Trace.WriteLineIf(ZTrace.IsInfo, "Instanciando la aplicación Word")
            wi = New Zamba.Office.WordInterop()
            wordapp = New Office.WordApplication()

            For Each r As ITaskResult In results
                _res = New NewResult()
                _res.Parent = DocTypesBusiness.GetDocType(_myrule.docTypeId, True)
                _res.DocumentalId = 0
                _res.Indexs = ZCore.FilterIndex(_myrule.docTypeId, False)
                _res.ID = ToolsBusiness.GetNewID(IdTypes.DOCID)

                Results_Business.LoadVolume(_res)
                Trace.WriteLineIf(ZTrace.IsInfo, "ID Documento: " & _res.ID)

                source = TextoInteligente.ReconocerCodigo(_myrule.FilePath, r)
                source = WFRuleParent.ReconocerVariablesAsObject(source)

                If TypeOf source Is Byte() Then
                    extension = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myrule.DocPathVar)
                    If Not extension.StartsWith(".") Then extension = "." & extension
                    path = EnvironmentUtil.GetTempDir("\IndexerTemp").ToString & "\" & DateTime.Now.ToString("dd-MM-yy HH-mm-ss") & extension
                    FileEncode.Decode(path, DirectCast(source, Byte()))
                Else
                    path = source.ToString
                    Trace.WriteLineIf(ZTrace.IsInfo, "Ruta del archivo: " & path)
                    file = New FileInfo(path)
                    path = EnvironmentUtil.GetTempDir("\IndexerTemp").ToString & "\" & DateTime.Now.ToString("dd-MM-yy HH-mm-ss") & file.Name
                    file.CopyTo(path, True)
                    file = Nothing
                End If

                Trace.WriteLineIf(ZTrace.IsInfo, "Abriendo archivo de word")
                wordobj = wordapp.OpenDocument(path)

                Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo texto del word")
                texto = wi.GetAllText(wordobj)
                Trace.WriteLineIf(ZTrace.IsInfo, "Texto obtenido: " & texto)

                'Se reconoce variables
                cambiosZvar = WFRuleParent.ReconocerVariablesValuesSoloTextoAsHashTB(texto)
                cambiosTextInt = TextoInteligente.ReconocerCodigoAsHashTB(texto, r)

                'Se reemplaza en word las coincidencias de Texto inteligente
                For Each i As String In cambiosTextInt.Keys
                    Zamba.Office.OfficeInterop.FindAndReplaceInWord(wordobj, i, cambiosTextInt.Item(i))
                Next

                'Se reemplaza en word las coincidencias de Zvar
                For Each i As String In cambiosZvar.Keys
                    Zamba.Office.OfficeInterop.FindAndReplaceInWord(wordobj, i, cambiosZvar.Item(i))
                Next

                'Si se configuro la regla para que se inserte código de barras
                If Me._myrule.InsertBarcode Then
                    'Se obtiene la distancia desde arriba
                    top = TextoInteligente.ReconocerCodigo(Me._myrule.Top, r)
                    top = WFRuleParent.ReconocerVariables(top)
                    If Not Int64.TryParse(top, auxint64) Then
                        top = "0"
                    End If

                    'Se obtiene la distancia desde la izquierda
                    left = TextoInteligente.ReconocerCodigo(Me._myrule.Left, r)
                    left = WFRuleParent.ReconocerVariables(left)
                    If Not Int64.TryParse(left, auxint64) Then
                        left = "0"
                    End If

                    _barcodeId = ToolsBusiness.GetNewID(IdTypes.Caratulas)
                    Trace.WriteLineIf(ZTrace.IsInfo, "ID Caratula: " & _barcodeId)

                    'Generando word
                    Zamba.Office.OfficeInterop.BarcodeInWordTopImage(wordobj, _barcodeId.ToString(), False, "Centro", 20, Int64.Parse(top), Int64.Parse(left))
                    Zamba.Office.OfficeInterop.SaveDoc(wordobj)

                    'Verifica si debe insertarlo en Zamba
                    If Not Me._myrule.WithoutInsert Then
                        'Inserción del documento
                        Trace.WriteLineIf(ZTrace.IsInfo, "Insertando caratula")
                        If BarcodesBusiness.Insert(_res, Int32.Parse(_res.Parent.ID.ToString()), Int32.Parse(Membership.MembershipHelper.CurrentUser.ID.ToString()), _barcodeId, True) Then
                            UserBusiness.Rights.SaveAction(_res.ID, ObjectTypes.ModuleBarCode, RightsType.Create, "usuario imprimio caratula")
                        Else
                            MsgBox("No se pudo insertar el código de barras", MsgBoxStyle.OkOnly, "Error en inserción")
                        End If
                    Else
                        Trace.WriteLineIf(ZTrace.IsInfo, "El documento no fué insertado por la configuración de la regla")
                    End If

                    wordapp.Visible = True
                    wi.Activate(wordobj)

                    'Imprime automáticamente la carátula
                    If Me._myrule.AutoPrint Then
                        wi.PrintWithWord(wordobj, wordapp)
                    End If

                Else
                    'Generando word
                    _res.File = path
                    _res.Disk_Group_Id = 0
                    Zamba.Office.OfficeInterop.SaveDoc(wordobj)

                    'Si la funcion de autoprint esta activa y el volumen es de 
                    'tipo base de datos entonces lo imprime antes de insertar
                    If _res.Volume.Type = VolumeTypes.DataBase Then
                        'Autoimpresion
                        If Me._myrule.AutoPrint Then wi.PrintWithWord(wordobj, wordapp)

                        'Liberacion de memoria antes de insertar
                        If wordapp IsNot Nothing Then
                            wordapp.Dispose()
                            wordapp = Nothing
                        End If
                    End If

                    If Me._myrule.WithoutInsert Then
                        Trace.WriteLineIf(ZTrace.IsInfo, "El documento no fué insertado por la configuración de la regla")
                    Else
                        'Inserta el documento
                        Trace.WriteLineIf(ZTrace.IsInfo, "Insertando documentacion")
                        Select Case (Results_Business.InsertDocument(_res, False, False, False, False, False))
                            Case InsertResult.Insertado
                                Trace.WriteLineIf(ZTrace.IsInfo, "Insertado en Zamba ")
                            Case InsertResult.ErrorIndicesIncompletos
                                Trace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de atributos obligatorios")
                            Case InsertResult.ErrorIndicesInvalidos
                                Trace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar, hay atributos con datos invalidos")
                        End Select
                    End If

                    'Si la funcion de autoprint esta activa y el volumen no es de tipo base de  
                    'datos entonces lo imprime despues de insertar (como venia funcionando)
                    If _res.Volume.Type <> VolumeTypes.DataBase AndAlso Me._myrule.AutoPrint Then
                        wi.PrintWithWord(wordobj, wordapp)
                    End If
                End If

                If Not Me._myrule.ContinueWithCurrentTasks Then
                    Dim task As ITaskResult = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(_res.ID, 0)
                    If Not IsNothing(task) Then
                        Trace.WriteLineIf(ZTrace.IsInfo, "Se adquiere la tarea del WF: " & task.WorkId)
                    Else
                        Trace.WriteLineIf(ZTrace.IsInfo, "La tarea no se encuentra en ningun WF, se agrega una tarea vacia")
                        task = New TaskResult()
                        task.ID = _res.ID
                        task.Indexs = _res.Indexs
                        task.DocType = _res.DocType
                        task.DocTypeId = _res.DocTypeId
                 
                        task.File = _res.File
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
            If cambiosZvar IsNot Nothing Then
                cambiosZvar.Clear()
                cambiosZvar = Nothing
            End If
            If cambiosTextInt IsNot Nothing Then
                cambiosTextInt.Clear()
                cambiosTextInt = Nothing
            End If
            If Not Me._myrule.InsertBarcode Then
                If wordapp IsNot Nothing Then
                    wordapp.Dispose()
                    wordapp = Nothing
                End If
                wordobj = Nothing
                If wi IsNot Nothing Then wi = Nothing
                GC.Collect()
            End If
        End Try

        If Me._myrule.SaveDocPathVar Then
            Dim docPath As String = Me._myrule.DocPathVar
            If docPath.StartsWith("zvar(") AndAlso docPath.EndsWith(")") Then
                docPath = docPath.Replace("zvar(", String.Empty)
                docPath = docPath.Remove(docPath.Length - 1, 1)
            End If

            Trace.WriteLineIf(ZTrace.IsInfo, "Guardando ruta del word: " & path)

            'Seteamos una variable que guarda el path del documento local
            If VariablesInterReglas.ContainsKey(docPath) Then
                VariablesInterReglas.Item(docPath) = path
            Else
                VariablesInterReglas.Add(docPath, path, False)
            End If
        End If

        'Si se configuró la regla para que continue con las tareas actuales
        If Me._myrule.ContinueWithCurrentTasks Then
            Return results
        Else
            Return newResults
        End If

    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
