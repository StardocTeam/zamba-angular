Imports System.IO
Imports Zamba.Tools
Imports Zamba.Core.WF.WF

Public Class PlayDoGenerateBarcodeInWord

    Private _myrule As IDoGenerateBarcodeInWord
    Private _barcodeId As Int64
    Private _res As NewResult
    Private indices As SortedList
    Private indexvalue As String
    Private ruleindicesaux As String
    Private strItem As String
    Private value As String
    Private id As Int32
    Private top As String
    Private left As String

    ''' <summary>
    ''' Constructor de la ejecución de la regla PlayDoGenerateBarcodeInWord
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <history>
    '''     Javier 28/10/2010   Created
    ''' </history>
    Public Sub New(ByVal rule As IDoGenerateBarcodeInWord)
        _myrule = rule
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
        indices = New SortedList()
        indices.Clear()
        indexvalue = String.Empty
        ruleindicesaux = _myrule.Indices
        'Obtengo todos los indices que tengo q llenar y la columna de donde va a salir el valor
        While Not String.IsNullOrEmpty(ruleindicesaux)
            'Obtengo el item (// separa por items y | separa por valor)
            strItem = ruleindicesaux.Split("//")(0)
            id = Int(strItem.Split("|")(0))
            value = strItem.Split("|")(1)

            If String.IsNullOrEmpty(indexvalue) Then
                indexvalue = value
            End If

            indices.Add(Int64.Parse(id), value)

            ruleindicesaux = ruleindicesaux.Remove(0, ruleindicesaux.Split("//")(0).Length)
            If ruleindicesaux.Length > 0 Then
                ruleindicesaux = ruleindicesaux.Remove(0, 2)
            End If
        End While

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
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Instanciando la aplicación Word")
            wi = New Zamba.Office.WordInterop()
            wordapp = New Office.WordApplication()
            Dim RB As New Results_Business

            For Each r As ITaskResult In results
                _res = New NewResult()
                _res.Parent = DocTypesBusiness.GetDocType(_myrule.docTypeId, True)

                _res.Indexs = ZCore.FilterIndex(_myrule.docTypeId, False)
                _res.ID = ToolsBusiness.GetNewID(IdTypes.DOCID)

                RB.LoadVolume(_res)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "ID Documento: " & _res.ID)


                'Valido si selecciono la opcion de autocompletar los indices que tengan en comun
                If results.Count > 0 Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "AutoCompleta Indices en Comun")

                    If results(0).Indexs.Count = 0 Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No hay indices para autocompletar, verifique que la tarea no se haya cerrado previamente")
                    End If
                    'Recorro los indices del primer result y me fijo cuales no fueron agregados a la coleccion de 
                    'indices a completar (son los que se configuraron a mano desde la regla)
                    For Each indice As IIndex In results(0).Indexs
                        If Not indices.Contains(Int64.Parse(indice.ID)) Then
                            'si el atributo no esta en la coleccion entonces lo agrego y lo configuro
                            'para que sea resuelto por texto inteligente automaticamente
                            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Indice {0} se va a Auto Completar con {1}", indice.ID & ": " & indice.Name, "<<Tarea>>.<<Indice(" + indice.Name + ")>>"))
                            indices.Add(Int64.Parse(indice.ID), "<<Tarea>>.<<Indice(" + indice.Name + ")>>")
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Indices configurados en la Regla: " & indices.Count)
                        End If
                        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Indice {0} se va Hereda de la Tarea Padre", indice.ID & ": " & indice.Name))
                    Next
                End If

                For Each indice As Int64 In indices.Keys
                    If Int64.Parse(indice) > 0 Then
                        For i As Int64 = 0 To _res.Indexs.Count - 1
                            If Int64.Parse(indice) = _res.Indexs(i).ID Then

                                'Si el indice no es referencial, le completo el valor
                                If IndexsBusiness.getReferenceStatus(_myrule.docTypeId, indice) = False Then
                                    Dim value As String
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Reconociendo Texto: " & indices(indice))
                                    value = TextoInteligente.ReconocerCodigo(indices(indice), r)
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Reconociendo Variables: " & value)
                                    value = WFRuleParent.ReconocerVariables(value)

                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Procesando indice: " & _res.Indexs(i).Name & " Asignando valor: " & value)
                                    _res.Indexs(i).Data = value
                                    _res.Indexs(i).DataTemp = value

                                    If _res.Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse _res.Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        _res.Indexs(i).dataDescription = AutoSubstitutionBusiness.getDescription(value, _res.Indexs(i).ID, False, _res.Indexs(i).Type)
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor descripcion: " & _res.Indexs(i).dataDescription)
                                    End If
                                End If
                                Exit For
                            End If
                        Next
                    End If
                Next

                source = TextoInteligente.ReconocerCodigo(_myrule.FilePath, r)
                source = WFRuleParent.ReconocerVariablesAsObject(source)

                If TypeOf source Is Byte() Then
                    extension = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myrule.DocPathVar)
                    If Not extension.StartsWith(".") Then extension = "." & extension
                    path = EnvironmentUtil.GetTempDir("\IndexerTemp").ToString & "\" & DateTime.Now.ToString("dd-MM-yy HH-mm-ss") & extension
                    FileEncode.Decode(path, DirectCast(source, Byte()))
                Else
                    path = source.ToString
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ruta del archivo: " & path)
                    file = New FileInfo(path)
                    path = EnvironmentUtil.GetTempDir("\IndexerTemp").ToString & "\" & DateTime.Now.ToString("dd-MM-yy HH-mm-ss") & file.Name
                    file.CopyTo(path, True)
                    file = Nothing
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Abriendo archivo de word")
                wordobj = wordapp.OpenDocument(path)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo texto del word")
                texto = wi.GetAllText(wordobj)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto obtenido: " & texto)

                'Se reconoce variables
                cambiosZvar = WFRuleParent.ReconocerVariablesValuesSoloTextoAsHashTB(texto)
                cambiosTextInt = TextoInteligente.ReconocerCodigoAsHashTB(texto, r, _res)

                Office.OfficeInterop.FindAndReplaceInWord(wordobj, "<<Tarea>>.<<NombreYApellido>>", Membership.MembershipHelper.CurrentUser.Nombres & " " & Membership.MembershipHelper.CurrentUser.Apellidos)
                Office.OfficeInterop.FindAndReplaceInWord(wordobj, "<<Tarea>>.<<Telefono>>", Membership.MembershipHelper.CurrentUser.telefono)
                Office.OfficeInterop.FindAndReplaceInWord(wordobj, "<<Tarea>>.<<Mail>>", Membership.MembershipHelper.CurrentUser.eMail.Mail)
                ' Si hay tablas, las reemplazo
                TextoInteligente.ReconocerCodigoInWord(wordobj, r, _res, indices)

                'Se reemplaza en word las coincidencias de Texto inteligente
                For Each i As String In cambiosTextInt.Keys
                    Zamba.Office.OfficeInterop.FindAndReplaceInWord(wordobj, i, cambiosTextInt.Item(i))
                Next

                'Se reemplaza en word las coincidencias de Zvar
                For Each i As String In cambiosZvar.Keys
                    Zamba.Office.OfficeInterop.FindAndReplaceInWord(wordobj, i, cambiosZvar.Item(i))
                Next

                'Si se configuro la regla para que se inserte código de barras
                If _myrule.InsertBarcode Then
                    'Se obtiene la distancia desde arriba
                    top = TextoInteligente.ReconocerCodigo(_myrule.Top, r)
                    top = WFRuleParent.ReconocerVariables(top)
                    If Not Int64.TryParse(top, auxint64) Then
                        top = "0"
                    End If

                    'Se obtiene la distancia desde la izquierda
                    left = TextoInteligente.ReconocerCodigo(_myrule.Left, r)
                    left = WFRuleParent.ReconocerVariables(left)
                    If Not Int64.TryParse(left, auxint64) Then
                        left = "0"
                    End If

                    _barcodeId = ToolsBusiness.GetNewID(IdTypes.Caratulas)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "ID Caratula: " & _barcodeId)

                    If VariablesInterReglas.ContainsKey("Barcode") Then
                        VariablesInterReglas.Item("Barcode") = _barcodeId
                    Else
                        VariablesInterReglas.Add("Barcode", _barcodeId, False)
                    End If

                    'Generando word
                    Zamba.Office.OfficeInterop.BarcodeInWordTopImage(wordobj, _barcodeId.ToString(), False, "Centro", 20, Int64.Parse(top), Int64.Parse(left))
                    Zamba.Office.OfficeInterop.SaveDoc(wordobj)

                    'Verifica si debe insertarlo en Zamba
                    If Not _myrule.WithoutInsert Then
                        'Inserción del documento
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertando caratula")
                        If BarcodesBusiness.Insert(_res, _res.Parent.ID, CInt(Membership.MembershipHelper.CurrentUser.ID), CInt(_barcodeId), True) Then
                            UserBusiness.Rights.SaveAction(_res.ID, ObjectTypes.ModuleBarCode, RightsType.Create, "usuario imprimio caratula")
                        Else
                            MsgBox("No se pudo insertar el código de barras", MsgBoxStyle.OkOnly, "Error en inserción")
                        End If
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El documento no fué insertado por la configuración de la regla")
                    End If

                    wordapp.Visible = True
                    wi.Activate(wordobj)

                    'Imprime automáticamente la carátula
                    If _myrule.AutoPrint Then
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
                        If _myrule.AutoPrint Then wi.PrintWithWord(wordobj, wordapp)

                        'Liberacion de memoria antes de insertar
                        If wordapp IsNot Nothing Then
                            wordapp.Dispose()
                            wordapp = Nothing
                        End If
                    End If

                    If _myrule.WithoutInsert Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El documento no fué insertado por la configuración de la regla")
                    Else
                        'Inserta el documento
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertando documentacion")
                        Select Case (Results_Business.InsertDocument(_res, False, False, False, False, False))
                            Case InsertResult.Insertado
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertado en Zamba ")
                            Case InsertResult.ErrorIndicesIncompletos
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de atributos obligatorios")
                            Case InsertResult.ErrorIndicesInvalidos
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar, hay atributos con datos invalidos")
                        End Select
                    End If

                    'Si la funcion de autoprint esta activa y el volumen no es de tipo base de  
                    'datos entonces lo imprime despues de insertar (como venia funcionando)
                    If _res.Volume.Type <> VolumeTypes.DataBase AndAlso _myrule.AutoPrint Then
                        wi.PrintWithWord(wordobj, wordapp)
                    End If
                End If

                If Not _myrule.ContinueWithCurrentTasks Then
                    Dim task As ITaskResult = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(_res.ID, 0)
                    If Not IsNothing(task) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se adquiere la tarea del WF: " & task.WorkId)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "La tarea no se encuentra en ningun WF, se agrega una tarea vacia")
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
            If Not _myrule.InsertBarcode Then
                If wordapp IsNot Nothing Then
                    wordapp.Dispose()
                    wordapp = Nothing
                End If
                wordobj = Nothing
                If wi IsNot Nothing Then wi = Nothing
                GC.Collect()
            End If
        End Try

        If _myrule.SaveDocPathVar Then
            Dim docPath As String = _myrule.DocPathVar
            If docPath.StartsWith("zvar(") AndAlso docPath.EndsWith(")") Then
                docPath = docPath.Replace("zvar(", String.Empty)
                docPath = docPath.Remove(docPath.Length - 1, 1)
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando ruta del word: " & path)

            'Seteamos una variable que guarda el path del documento local
            If VariablesInterReglas.ContainsKey(docPath) Then
                VariablesInterReglas.Item(docPath) = path
            Else
                VariablesInterReglas.Add(docPath, path, False)
            End If
        End If

        'Si se configuró la regla para que continue con las tareas actuales
        If _myrule.ContinueWithCurrentTasks Then
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
