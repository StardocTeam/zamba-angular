Imports Zamba.Core
Imports Zamba.Core.WF.WF

Public Class PlayDoAddAsociatedForm
    Private _myRule As IDoAddAsociatedForm
    Private _htAttributesConfig As Dictionary(Of Long, String)

    Sub New(ByVal rule As IDoAddAsociatedForm)
        Me._myRule = rule
        Me._htAttributesConfig = New Dictionary(Of Long, String)
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim newTaskResults As List(Of ITaskResult)
        If Not _myRule.ContinueWithCurrentTasks Then
            newTaskResults = New List(Of ITaskResult)
        End If

        For Each currentResult As Result In results
         
            'Se obtiene el doctypeid para crear un NewResult
            Dim docTypeId As Int64 = FormBusiness.GetDocTypeId(_myRule.FormID)
            Trace.WriteLineIf(ZTrace.IsInfo, "DoctypeId obtenido = " & docTypeId)
            Dim newRes As NewResult = Results_Business.GetNewNewResult(docTypeId)

            If Not currentResult Is Nothing AndAlso currentResult.DocType Is Nothing Then
                Trace.WriteLineIf(ZTrace.IsInfo, "DocType del CurrentResult is Nothing")
                currentResult.DocType = DocTypesBusiness.GetDocType(docTypeId, True)
                Trace.WriteLineIf(ZTrace.IsInfo, "Se obtuvo y se asigno DocType al CurrentResult")
            End If

            'Se cargan los valores por defecto de todos los indices del newRes
            'Se cargan los valores por defecto de todos los indices del newRes
            Trace.WriteLineIf(ZTrace.IsInfo, "Se obtienen los valores por defecto de los indices del doctype")
            Dim indexesDefaultValues As Dictionary(Of Int64, String) = IndexsBusiness.GetIndexDefaultValues(newRes.DocType)
            Trace.WriteLineIf(ZTrace.IsInfo, "FOR Asignando los valores por defecto a los indices del newRes")

            Dim currIndex As IIndex
            For i As Int64 = 0 To newRes.Indexs.Count - 1
                currIndex = newRes.Indexs(i)
                If indexesDefaultValues.ContainsKey(currIndex.ID) Then
                    newRes.Indexs(i).Data = indexesDefaultValues(currIndex.ID)
                    newRes.Indexs(i).DataTemp = indexesDefaultValues(currIndex.ID)

                    'If newRes.Indexs(iDestino).DropDown = IndexAdditionalType.AutoSustitución Then
                    '    newRes.Indexs(iDestino).dataDescription = AutoSubstitutionBusiness.getDescription(newRes.Indexs(iDestino).Data, newRes.Indexs(iDestino).ID, False, newRes.Indexs(iDestino).Type)
                    'End If

                End If
            Next

            Trace.WriteLineIf(ZTrace.IsInfo, "Se asignaron todos los valores por defecto de los indices")

            Dim currIndexFrom As IIndex

            If _myRule.HaveSpecificAttributes() = True Then
                _htAttributesConfig = GetHashFromSpecificAttributes(_myRule.SpecificAttrubutes, results)
            End If

            'Verifica si debe completar los índices en común
            If _myRule.FillCommonAttributes Or _myRule.HaveSpecificAttributes() = True Then
                For iDestino As Int64 = 0 To newRes.Indexs.Count - 1
                    currIndex = newRes.Indexs(iDestino)

                    If (_htAttributesConfig.ContainsKey(currIndex.ID)) Then
                        newRes.Indexs(iDestino).Data = _htAttributesConfig(currIndex.ID)
                        newRes.Indexs(iDestino).DataTemp = _htAttributesConfig(currIndex.ID)
                    Else
                        If Not currentResult Is Nothing Then

                            For iOrigen As Int64 = 0 To currentResult.Indexs.Count - 1
                                currIndexFrom = currentResult.Indexs(iOrigen)

                                If currIndex.ID = currIndexFrom.ID Then
                                    If IndexsBusiness.getReferenceStatus(docTypeId, currIndex.ID) = False Then
                                        Trace.WriteLineIf(ZTrace.IsInfo, "Completando atributo " & currIndex.Name)
                                        'Si el valor del indice que trae CurrentResult es string vacio, deja el default seteado previamente
                                        If Not String.IsNullOrEmpty(currIndexFrom.Data) Then
                                            newRes.Indexs(iDestino).Data = currIndexFrom.Data
                                            newRes.Indexs(iDestino).DataTemp = currIndexFrom.DataTemp
                                        End If
                                        If newRes.Indexs(iDestino).DropDown = IndexAdditionalType.AutoSustitución OrElse newRes.Indexs(iDestino).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                            newRes.Indexs(iDestino).dataDescription = AutoSubstitutionBusiness.getDescription(newRes.Indexs(iDestino).Data, newRes.Indexs(iDestino).ID, False, newRes.Indexs(iDestino).Type)
                                        End If
                                    End If
                                    Exit For
                                End If
                            Next
                        End If

                        End If
                Next
            End If

            'Se abre la ventana con el formulario de insercion seleccionado
            Using frmInsertForm As Viewers.InsertFormDialog = New Viewers.InsertFormDialog(newRes, _myRule.FormID)
                'Título de la ventana
                frmInsertForm.Text = "Nuevo " & newRes.DocType.Name

                'Si el usuario acepta
                If frmInsertForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                    'Si se encuentra configurada la opcion para continuar la ejecucion con las tareas anteriores
                    If Not _myRule.ContinueWithCurrentTasks Then
                        'Se obtiene la tarea
                        Dim task As ITaskResult = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(newRes.ID, 0)
                        'Verifica si encontro algo
                        If Not IsNothing(task) Then
                            Trace.WriteLineIf(ZTrace.IsInfo, "Se adquiere la tarea del WF: " & task.WorkId)
                        Else
                            Trace.WriteLineIf(ZTrace.IsInfo, "La tarea no se encuentra en ningun WF, se agrega una tarea vacia en memoria")
                            task = New TaskResult()
                            task.ID = newRes.ID
                            task.Indexs = newRes.Indexs
                            task.DocType = newRes.DocType
                            task.DocTypeId = newRes.DocTypeId

                            task.Platter_Id = newRes.Platter_Id
                            task.Name = newRes.Name

                            'Verifica si debe abrir la tarea
                            If Not Me._myRule.DontOpenTaskAfterInsert Then
                                Dim wfId As Int64 = WFBusiness.GetWFAssociationByDocTypeId(task.DocTypeId)
                                If WFId = 0 Then
                                    ZambaCore.HandleModule(ResultActions.MostrarResult, newRes, Nothing)
                                End If
                            End If
                        End If
                        'Devuelve la coleccion de nuevos results
                        newTaskResults.Add(task)
                    End If
                Else
                    'Verifica si al cancelar debe tirar exception o cancelar la ejecucion sin errores
                    If Me._myRule.ThrowExceptionIfCancel Then
                        Trace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                        Throw New Exception("El usuario cancelo la ejecucion de la regla")
                    Else
                        results = Nothing
                        newTaskResults = Nothing
                        Exit For
                    End If
                End If
            End Using
        Next

        'Verifica con que debe continuar la ejecucion
        If _myRule.ContinueWithCurrentTasks Then
            Return results
        Else
            Return newTaskResults
        End If
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Private Function GetHashFromSpecificAttributes(ByVal configuration As String, ByVal results As List(Of ITaskResult)) As Dictionary(Of Long, String)
        Dim hash As New Dictionary(Of Long, String)
        Dim strIndex As String = configuration.Replace("//", "§")
        Dim value As String
        Dim id As Int64
        Dim strItem As String

        If Not String.IsNullOrEmpty(configuration) Then
            Dim varInterReglas As New VariablesInterReglas()

            'Separa cada item
            While Not String.IsNullOrEmpty(strIndex)
                'Obtengo el item (// separa por items y | separa por valor y no completar)

                strItem = strIndex.Split("§")(0)
                id = Int(strItem.Split("|")(0))
                value = strItem.Substring(strItem.IndexOf("|", StringComparison.Ordinal) + 1)

                'Si no esta configurado para heredarse se agrega al diccionario
                If value.Contains("[no_completar]") Then
                    value = value.Split("|")(0)

                    value = TextoInteligente.ReconocerCodigo(value, results(0))
                    value = WFRuleParent.ReconocerVariablesValuesSoloTexto(value)

                    hash.Add(Int64.Parse(id), value)
                End If

                strIndex = strIndex.Remove(0, strIndex.Split("§")(0).Length)
                If strIndex.Length > 0 Then
                    strIndex = strIndex.Remove(0, 1)
                End If
            End While
        End If

        Return hash
    End Function
End Class
