Imports Zamba.Core.WF.WF

''' <summary>
''' Genera las tareas a partir de un dataset
''' </summary>
''' <history>Marcelo modified 07/11/08</history>
''' <remarks></remarks>
Public Class PlayDOGenerateTaskResult
    Private MyRule As IDOGenerateTaskResult
    Private indexValue As String
    Private ruleindicesaux As String
    Private strItem As String
    Private value As String
    Private valor As String
    Private valores() As String
    Private id As Int32
    Private objeto As Object
    Private indices As SortedList
    Private dt As DataTable
    Private r As TaskResult
    Private _filepath As String
    Private valoraux As String
    Private isVirtual As Boolean

    Public Sub New(ByVal rule As IDOGenerateTaskResult)
        MyRule = rule
        dt = New DataTable()
    End Sub

    ''' <summary>
    ''' Ejecucion de la regla
    ''' </summary>
    ''' <param name="results">results a ejecutar</param>
    ''' <param name="rule">regla</param>
    ''' <history>   Marcelo Modified    12/01/2010</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim numberIndexValue As Integer
        Try
            If results.Count > 0 AndAlso results(0) IsNot Nothing Then
                ParentTaskId = results(0).TaskId
            End If
            indices = New SortedList()
            indices.Clear()
            indexValue = String.Empty
            ruleindicesaux = MyRule.indices.Replace("//", "§")

            'Obtengo todos los indices que tengo q llenar y la columna de donde va a salir el valor
            While Not String.IsNullOrEmpty(ruleindicesaux)
                strItem = ruleindicesaux.Split("§")(0)
                ' numberIndexValue = strItem.IndexOf("|")
                'Obtiene el id del idncie
                id = Int(strItem.Split("|")(0))
                value = strItem.Split("|")(1)
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Id Indice: " & id)
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Valor manual: " & value)
                Dim autocompletevalue As String = String.Empty
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Verifica si tiene que Autocompletar: " & strItem)
                If strItem.Split("|").Length > 2 Then
                    'Obtiene el valor (sin el la marca para no autocompletar)
                    autocompletevalue = strItem.Split("|")(2)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Autocompletar: " & autocompletevalue)
                End If

                If (id = 0) Then
                    'Si hay un indice 0 y tiene zvar, es la variable
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Variable de Archivo: " & value)
                    If (value.Contains("zvar")) Then
                        indexValue = value
                    Else
                        indices.Add(Int64.Parse(id), value)
                    End If
                ElseIf value.Length > 0 OrElse autocompletevalue.Length > 0 Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Asignacion de Valor manual: " & value)
                    value = TextoInteligente.ReconocerCodigo(value, results(0))
                    value = WFRuleParent.ReconocerVariablesValuesSoloTexto(value)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Valor manual final: " & value)
                    indices.Add(Int64.Parse(id), value)
                End If

                ruleindicesaux = ruleindicesaux.Remove(0, ruleindicesaux.Split("§")(0).Length)
                If ruleindicesaux.Length > 0 Then
                    ruleindicesaux = ruleindicesaux.Remove(0, 1)
                End If
            End While


            'Valido si selecciono la opcion de autocompletar los indices que tengan en comun
            If MyRule.AutocompleteIndexsInCommon AndAlso results.Count > 0 AndAlso results(0) IsNot Nothing Then
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
                        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Indice {0} - {1}: se Autocompleta con <<Tarea>>.<<Indice({1})>>", indice.ID, indice.Name))
                        indices.Add(Int64.Parse(indice.ID), "<<Tarea>>.<<Indice(" + indice.Name + ")>>")
                    End If
                Next
            End If

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Asignacion de Archivo: " & MyRule.FilePath)

            _filepath = MyRule.FilePath
            'Obtengo la ruta del archivo fisico
            If String.IsNullOrEmpty(MyRule.FilePath) Then
                isVirtual = True
            Else
                isVirtual = False
                If results.Count > 0 AndAlso results(0) IsNot Nothing Then
                    _filepath = TextoInteligente.ReconocerCodigo(_filepath, results(0))
                End If
                _filepath = WFRuleParent.ReconocerVariablesValuesSoloTexto(_filepath)
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Archivo adjunto: " & _filepath)
            End If

            'Obtengo el dataset o datatable
            If Not String.IsNullOrEmpty(indexValue) AndAlso indexValue.Length > 0 Then
                valor = String.Empty
                valor = TextoInteligente.ReconocerCodigo(indexValue, Nothing)
                objeto = Nothing
                objeto = WFRuleParent.ObtenerValorVariableObjeto(valor)

                'Dependiendo del tipo de objeto, es el tipo de creacion
                If (TypeOf (objeto) Is DataSet) Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Valor Texto Inteligente: " & valor)

                    dt = DirectCast(objeto, DataSet).Tables(0)
                    If Not IsNothing(dt) Then
                        If results.Count > 0 AndAlso results(0) IsNot Nothing Then
                            r = results(0)
                        End If
                        '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                        If Not MyRule.ContinueWithCurrentTasks Then
                            Dim newresults As List(Of ITaskResult) = procesarTabla()
                            Try

                                If (Not newresults Is Nothing AndAlso newresults.Count = 1 AndAlso Not newresults(0) Is Nothing) Then

                                    If VariablesInterReglas.ContainsKey("NuevaTarea.Id") = False Then
                                        VariablesInterReglas.Add("NuevaTarea.Id", newresults(0).ID, False)
                                    Else
                                        VariablesInterReglas.Item("NuevaTarea.Id") = newresults(0).ID
                                    End If
                                    If VariablesInterReglas.ContainsKey("NuevaTarea.TaskId") = False Then
                                        VariablesInterReglas.Add("NuevaTarea.TaskId", newresults(0).TaskId, False)
                                    Else
                                        VariablesInterReglas.Item("NuevaTarea.TaskId") = newresults(0).TaskId
                                    End If
                                    If VariablesInterReglas.ContainsKey("NuevaTarea.EntityId") = False Then
                                        VariablesInterReglas.Add("NuevaTarea.EntityId", newresults(0).DocTypeId, False)
                                    Else
                                        VariablesInterReglas.Item("NuevaTarea.EntityId") = newresults(0).DocTypeId
                                    End If

                                End If
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try
                            Return newresults
                        Else
                            Dim newresults As List(Of ITaskResult) = procesarTabla()
                            If (Not newresults Is Nothing AndAlso newresults.Count = 1 AndAlso Not newresults(0) Is Nothing) Then

                                If VariablesInterReglas.ContainsKey("NuevaTarea.Id") = False Then
                                    VariablesInterReglas.Add("NuevaTarea.Id", newresults(0).ID, False)
                                Else
                                    VariablesInterReglas.Item("NuevaTarea.Id") = newresults(0).ID
                                End If
                                If VariablesInterReglas.ContainsKey("NuevaTarea.TaskId") = False Then
                                    VariablesInterReglas.Add("NuevaTarea.TaskId", newresults(0).TaskId, False)
                                Else
                                    VariablesInterReglas.Item("NuevaTarea.TaskId") = newresults(0).TaskId
                                End If
                                If VariablesInterReglas.ContainsKey("NuevaTarea.EntityId") = False Then
                                    VariablesInterReglas.Add("NuevaTarea.EntityId", newresults(0).DocTypeId, False)
                                Else
                                    VariablesInterReglas.Item("NuevaTarea.EntityId") = newresults(0).DocTypeId
                                End If

                            End If
                            Return results
                        End If
                    Else
                        'Si no se obtuvo el objeto, lo devuelvo
                        Return results
                    End If

                ElseIf (TypeOf (objeto) Is DataTable) Then


                    dt = objeto

                    If Not IsNothing(dt) Then
                        If results.Count > 0 AndAlso results(0) IsNot Nothing Then
                            r = results(0)
                        End If

                        '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                        If Not MyRule.ContinueWithCurrentTasks Then
                            Dim newresults As List(Of ITaskResult) = procesarTabla()
                            Return newresults
                        Else
                            Dim newresults As List(Of ITaskResult) = procesarTabla()
                            Return results
                        End If
                    Else
                        'Si no se obtuvo el objeto, lo devuelvo
                        Return results
                    End If

                ElseIf (TypeOf (objeto) Is String AndAlso objeto <> String.Empty) Then


                    valor = WFRuleParent.ReconocerVariablesValuesSoloTexto(indexValue)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Valor Variable: " & valor)

                    valores = New String() {valor}
                    'Return Me.createResult(0)
                    If Not MyRule.ContinueWithCurrentTasks Then
                        Return createResult(0)
                    Else
                        createResult(0)
                        Return results
                    End If

                ElseIf (TypeOf (WFRuleParent.ObtenerValorVariableObjeto(valor)) Is Int32) Then


                    valor = WFRuleParent.ReconocerVariablesValuesSoloTexto(indexValue)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Valor Variable: " & valor)

                    valores = New String() {valor}
                    '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                    If Not MyRule.ContinueWithCurrentTasks Then
                        Return createResult(0)
                    Else
                        createResult(0)
                        Return results
                    End If

                Else

                    If results.Count > 0 AndAlso results(0) IsNot Nothing Then
                        r = results(0)
                    End If
                    '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                    If Not MyRule.ContinueWithCurrentTasks Then
                        Return createResult(2)
                    Else
                        createResult(2)
                        Return results
                    End If
                    UserBusiness.Rights.SaveAction(r.ID, ObjectTypes.WFTask, RightsType.ExecuteRule, MyRule.Name)
                End If
            Else
                If results.Count > 0 AndAlso results(0) IsNot Nothing Then
                    r = results(0)
                End If
                If Not MyRule.ContinueWithCurrentTasks Then
                    Return createResult(2)
                Else
                    createResult(2)
                    Return results
                End If
                UserBusiness.Rights.SaveAction(r.ID, ObjectTypes.WFTask, RightsType.ExecuteRule, MyRule.Name)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Throw ex
        Finally

            indexValue = Nothing
            ruleindicesaux = Nothing
            strItem = Nothing
            value = Nothing
            valor = Nothing
            valores = New String() {}
            id = Nothing
            _filepath = Nothing
            objeto = Nothing
            indices = Nothing
            dt = Nothing
            r = Nothing
            valoraux = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Crea la tabla a partir de los results
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <param name="indices"></param>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function procesarTabla() As System.Collections.Generic.List(Of Core.ITaskResult)
        'Lo hago de la manera que estaba antes, para no romper la regla
        If indices.Count = 1 Then
            'Creo cada result con un solo indice con valor
            Dim valores(dt.Rows.Count - 1) As String
            Dim i As Int32 = 0
            For Each row As DataRow In dt.Rows
                valores(i) = row(0).ToString()

                i += 1
            Next

            Return createResult(0)
        Else
            'Creo los result a partir de la tabla
            Return createResult(1)
        End If
    End Function
    Private ParentTaskId As Int64
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="valores"></param>
    ''' <param name="rule"></param>
    ''' <param name="indices"></param>
    ''' <returns></returns>
    ''' <history>Marcelo modified 07/11/08</history>
    ''' <remarks></remarks>
    Private Function createResult(ByVal tipo As Int16) As System.Collections.Generic.List(Of Core.ITaskResult)

        'Crea un nuevo result y lo inserta en Zamba
        Dim arrayItaskResult As New Generic.List(Of Core.ITaskResult)()
        Dim WFTB As New WFTaskBusiness
        Select Case tipo
            'Case que crea las tareas apartir de los valores.
            Case 0
                For Each valor As String In valores
                    Dim newRes As NewResult = Results_Business.GetNewNewResult(MyRule.docTypeId)
                    newRes.File = _filepath

                    For Each indice As String In indices.Keys
                        If Int64.Parse(indice) > 0 Then
                            For i As Int64 = 0 To newRes.Indexs.Count - 1
                                If Int64.Parse(indice) = newRes.Indexs(i).ID Then

                                    newRes.Indexs(i).Data = valor
                                    newRes.Indexs(i).DataTemp = newRes.Indexs(i).Data
                                    If newRes.Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse newRes.Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then

                                        newRes.Indexs(i).dataDescription = AutoSubstitutionBusiness.getDescription(valor, newRes.Indexs(i).ID, False, newRes.Indexs(i).Type)
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor descripcion Indice: " & newRes.Indexs(i).dataDescription)

                                    End If

                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se asigna a {0} - {1}: {2}", newRes.Indexs(i).ID, newRes.Indexs(i).Name, valor))

                                    Exit For
                                End If
                            Next
                        End If
                    Next

                    Select Case (Results_Business.InsertDocument(newRes, False, False, False, False, isVirtual))
                        Case InsertResult.Insertado

                            If MyRule.addCurrentwf = False Then
                                Dim arrayNewRes As ArrayList = New ArrayList()
                                arrayNewRes.Add(newRes)
                                Dim wfID As Int64 = WFBusiness.GetWorkflowIdByStepId(MyRule.WFStepId)
                                arrayItaskResult.Add(WFTaskBusiness.AddNewResultsToWorkFLow(arrayNewRes, wfID, Not MyRule.DontOpenTaskAfterInsert)(0))
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregado a WF con OpenTaskAfterInsert en " & MyRule.DontOpenTaskAfterInsert)
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se adjunta al WF actual, debido a que esta activa la opcion de no adjuntar")
                                Dim task As ITaskResult = WFTB.GetTaskByDocId(newRes.ID)
                                If Not IsNothing(task) Then
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se adquiere la tarea del WF: " & task.WorkId)
                                Else
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "La tarea no se encuentra en ningun WF, se agrega una tarea vacia")
                                    task = New TaskResult()
                                    task.ID = newRes.ID
                                    task.Indexs = newRes.Indexs
                                    task.DocType = newRes.DocType
                                    task.DocTypeId = newRes.DocTypeId

                                End If
                                arrayItaskResult.Add(task)
                            End If
                        Case InsertResult.ErrorIndicesIncompletos
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de indices obligatorios")
                        Case InsertResult.ErrorIndicesInvalidos
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar, hay indices con datos invalidos")
                    End Select

                Next

                'Case que crea los result a partir de la tabla
            Case 1
                Dim arrayNewRes As ArrayList = New ArrayList()

                For Each row As DataRow In dt.Rows
                    'Obtengo un nuevo result
                    Dim newRes As NewResult = Results_Business.GetNewNewResult(MyRule.docTypeId)
                    newRes.File = _filepath
                    'Por cada indice pedido obtengo los datos
                    For Each indice As String In indices.Keys
                        If Int64.Parse(indice) <> 0 Then
                            For i As Int64 = 0 To newRes.Indexs.Count - 1
                                If Int64.Parse(indice) = newRes.Indexs(i).ID Then

                                    If dt.Columns.Contains(indices(Int64.Parse(indice))) Then
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando a: " & indice & " Asignando valor: " & row(indices(Int64.Parse(indice))))
                                        newRes.Indexs(i).Data = row(indices(Int64.Parse(indice))).ToString()
                                        newRes.Indexs(i).DataTemp = newRes.Indexs(i).Data
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se asigna a {0} - {1}: {2}", newRes.Indexs(i).ID, newRes.Indexs(i).Name, row(indices(Int64.Parse(indice))).ToString()))

                                    Else
                                        valoraux = indices(Int64.Parse(indice))
                                        valoraux = WFRuleParent.ReconocerVariablesValuesSoloTexto(valoraux)
                                        If Not r Is Nothing Then
                                            valoraux = TextoInteligente.ReconocerCodigo(valoraux, r)
                                        End If
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando a: " & indice & " Asignando valor: " & valoraux)
                                        newRes.Indexs(i).Data = valoraux
                                        newRes.Indexs(i).DataTemp = newRes.Indexs(i).Data
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se asigna a {0} - {1}: {2}", newRes.Indexs(i).ID, newRes.Indexs(i).Name, valoraux))

                                    End If


                                    If newRes.Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse newRes.Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        newRes.Indexs(i).dataDescription = AutoSubstitutionBusiness.getDescription(newRes.Indexs(i).Data, newRes.Indexs(i).ID, False, newRes.Indexs(i).Type)
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor descripcion Indice: " & newRes.Indexs(i).dataDescription)
                                    End If

                                    Exit For
                                End If
                            Next
                        End If
                    Next

                    'Inserto el nuevo result en zamba
                    Select Case Results_Business.InsertDocument(newRes, False, False, False, False, isVirtual)
                        Case InsertResult.Insertado
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertado en Zamba:" & newRes.ID)

                            If newRes.ID > 0 And MyRule.addCurrentwf = False Then
                                'Agrego el result al wf
                                arrayNewRes.Add(newRes)
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "No se adjunta al WF actual, debido a que esta activa la opcion de no adjuntar")

                                Dim task As ITaskResult = WFTB.GetTaskByDocId(newRes.ID)
                                If Not IsNothing(task) Then
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se adquiere la tarea del WF: " & task.WorkId)
                                Else
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "La tarea no se encuentra en ningun WF, se agrega una tarea vacia")
                                    task = New TaskResult()
                                    task.ID = newRes.ID
                                    task.Indexs = newRes.Indexs
                                    task.DocType = newRes.DocType
                                    task.DocTypeId = newRes.DocTypeId

                                End If
                                arrayItaskResult.Add(task)
                            End If

                        Case InsertResult.ErrorIndicesIncompletos
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de indices obligatorios")
                        Case InsertResult.ErrorIndicesInvalidos
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar, hay indices con datos invalidos")
                    End Select
                Next
                '[Ezequiel] 03/12/09 - Movi de lugar la llamada al metodo que agrega al wf asi agrega todos los documentos creados de una para mas performance.
                If arrayNewRes.Count > 0 AndAlso MyRule.addCurrentwf = False Then

                    Dim wfID As Int64 = WFBusiness.GetWorkflowIdByStepId(MyRule.WFStepId)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregado a WF")
                    arrayItaskResult.AddRange(WFTaskBusiness.AddNewResultsToWorkFLow(arrayNewRes, wfID, Not MyRule.DontOpenTaskAfterInsert))
                End If
                'Case que crea una sola tarea
            Case 2
                Dim newRes As NewResult = Results_Business.GetNewNewResult(MyRule.docTypeId)
                newRes.File = _filepath
                ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Nueva Tarea con {0} Indices", newRes.Indexs.Count))
                ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Indices a Verificar configurados {0}", indices.Count))
                For Each indice As Int64 In indices.Keys
                    If Int64.Parse(indice) > 0 Then
                        For i As Int64 = 0 To newRes.Indexs.Count - 1
                            If Int64.Parse(indice) = newRes.Indexs(i).ID Then

                                'Si el indice no es referencial, le completo el valor
                                If IndexsBusiness.getReferenceStatus(MyRule.docTypeId, indice) = False Then
                                    Dim value As String
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Reconociendo Texto: " & indices(indice))
                                    value = TextoInteligente.ReconocerCodigo(indices(indice), r)
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Reconociendo Variables: " & value)
                                    value = WFRuleParent.ReconocerVariables(value)

                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Procesando indice: " & newRes.Indexs(i).Name & " Asignando valor: " & value)
                                    newRes.Indexs(i).Data = value
                                    newRes.Indexs(i).DataTemp = newRes.Indexs(i).Data
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se asigna a {0} - {1}: {2}", newRes.Indexs(i).ID, newRes.Indexs(i).Name, value))


                                    If newRes.Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse newRes.Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then

                                        newRes.Indexs(i).dataDescription = AutoSubstitutionBusiness.getDescription(value, newRes.Indexs(i).ID, False, newRes.Indexs(i).Type)
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor descripcion: " & newRes.Indexs(i).dataDescription)
                                    End If
                                End If
                                Exit For
                            End If
                        Next
                    End If
                Next

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "MyRule.DontOpenTaskAfterInsert: " & MyRule.DontOpenTaskAfterInsert.ToString())
                'Inserto el nuevo result en zamba
                Select Case Results_Business.InsertDocument(newRes, False, False, False, False, isVirtual, False, False, True, Not MyRule.DontOpenTaskAfterInsert, Not MyRule.DontOpenTaskAfterInsert, ParentTaskId)
                    Case InsertResult.Insertado
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertado en Zamba:" & newRes.ID)

                        If newRes.ID > 0 Then
                            If MyRule.addCurrentwf = False Then
                                'Agrego el result al wf
                                Dim arrayNewRes As ArrayList = New ArrayList()
                                arrayNewRes.Add(newRes)

                                Dim wfID As Int64


                                If Not r Is Nothing Then
                                    wfID = r.WorkId
                                Else
                                    wfID = WFBusiness.GetWorkflowIdByStepId(MyRule.WFStepId)
                                End If


                                Dim newITaskResultList As System.Collections.Generic.List(Of ITaskResult)
                                newITaskResultList = WFTaskBusiness.AddNewResultsToWorkFLow(arrayNewRes, wfID, Not MyRule.DontOpenTaskAfterInsert)
                                If newITaskResultList Is Nothing Then
                                    Throw New Exception("No se inserto la nueva tarea en WF")
                                ElseIf newITaskResultList.Count = 0 Then
                                    Throw New Exception("No se inserto la nueva tarea en WF")
                                Else
                                    arrayItaskResult.Add(newITaskResultList(0))
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregado a WF")
                                End If
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se adjunta al WF actual, debido a que esta activa la opcion de no adjuntar")

                                Dim task As ITaskResult = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(newRes.ID, 0)
                                If IsNothing(task) Then
                                    task = New TaskResult()
                                    task.ID = newRes.ID
                                    task.Indexs = newRes.Indexs
                                    task.DocType = newRes.DocType
                                    task.DocTypeId = newRes.DocTypeId
                                    task.Platter_Id = newRes.Platter_Id

                                    '(Pablo) valido si la nueva tarea generada debe visualizarse 
                                    '        o no dependiendo del check seteado desde el administrador
                                    ' Dim WFId As Int64 = WFBusiness.GetWFAssociationByDocTypeId(task.DocTypeId)
                                    If Not MyRule.DontOpenTaskAfterInsert Then 'And WFId = 0 Then
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se abrira la Tarea")
                                        ZambaCore.HandleModule(ResultActions.MostrarResult, newRes, Nothing)
                                    End If

                                End If
                                arrayItaskResult.Add(task)
                            End If

                            Dim path As String = _filepath

                            If String.Compare(IO.Path.GetExtension(path).ToLower().Trim(), ".msg") = 0 Then

                                Try
                                    path = path.Substring(0, path.LastIndexOf(".")) & ".html"


                                    If IO.File.Exists(path) Then
                                        Dim destino As String = IO.Path.GetDirectoryName(newRes.FullPath) & "\" & IO.Path.GetFileName(newRes.FullPath)
                                        destino = destino.Substring(0, destino.LastIndexOf(".")) & ".html"
                                        IO.File.Copy(path, destino)
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "La copia se ha realizado con éxito. Ruta: " & destino)
                                    Else
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontró el archivo: " & path)
                                    End If

                                Catch ex As Exception
                                    ZClass.raiseerror(ex)
                                End Try
                            End If

                        End If
                    Case InsertResult.ErrorIndicesIncompletos
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de indices obligatorios")
                    Case InsertResult.ErrorIndicesInvalidos
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de indices obligatorios")
                End Select
        End Select
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Tarea generada con éxito!")
        Try
            If (Not arrayItaskResult Is Nothing AndAlso arrayItaskResult.Count = 1 AndAlso Not arrayItaskResult(0) Is Nothing) Then

                If VariablesInterReglas.ContainsKey("NuevaTarea.Id") = False Then
                    VariablesInterReglas.Add("NuevaTarea.Id", arrayItaskResult(0).ID, False)
                Else
                    VariablesInterReglas.Item("NuevaTarea.Id") = arrayItaskResult(0).ID
                End If
                If VariablesInterReglas.ContainsKey("NuevaTarea.TaskId") = False Then
                    VariablesInterReglas.Add("NuevaTarea.TaskId", arrayItaskResult(0).TaskId, False)
                Else
                    VariablesInterReglas.Item("NuevaTarea.TaskId") = arrayItaskResult(0).TaskId
                End If
                If VariablesInterReglas.ContainsKey("NuevaTarea.EntityId") = False Then
                    VariablesInterReglas.Add("NuevaTarea.EntityId", arrayItaskResult(0).DocTypeId, False)
                Else
                    VariablesInterReglas.Item("NuevaTarea.EntityId") = arrayItaskResult(0).DocTypeId
                End If

            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        WFTB = Nothing
        Return arrayItaskResult
    End Function
    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class