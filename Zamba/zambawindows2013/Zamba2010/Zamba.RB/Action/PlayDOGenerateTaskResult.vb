Imports Zamba.Core.WF.WF
Imports Zamba.Core
Imports Zamba.Data

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
        Me.MyRule = rule
        Me.dt = New DataTable()
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

        Try
      
            Me.indices = New SortedList()
            Me.indices.Clear()
            Me.indexValue = String.Empty
            Me.ruleindicesaux = Me.MyRule.indices.Replace("//", "�")

            'Obtengo todos los indices que tengo q llenar y la columna de donde va a salir el valor
            While Not String.IsNullOrEmpty(Me.ruleindicesaux)
                strItem = Me.ruleindicesaux.Split("�")(0)

                'Obtiene el id del idncie
                id = Int(strItem.Split("|")(0))

                'Obtiene el valor (sin el la marca para no autocompletar)
                value = strItem.Split("|")(1)

                If (id = 0) Then
                    'Si hay un indice 0 y tiene zvar, es la variable
                    If (value.Contains("zvar")) Then
                        indexValue = value
                    Else
                        indices.Add(Int64.Parse(id), value)
                    End If
                Else
                    indices.Add(Int64.Parse(id), value)
                End If

                    Me.ruleindicesaux = Me.ruleindicesaux.Remove(0, Me.ruleindicesaux.Split("�")(0).Length)
                    If Me.ruleindicesaux.Length > 0 Then
                        Me.ruleindicesaux = Me.ruleindicesaux.Remove(0, 1)
                    End If
            End While


            'Valido si selecciono la opcion de autocompletar los indices que tengan en comun
            If Me.MyRule.AutocompleteIndexsInCommon AndAlso results.Count > 0 Then
                Trace.WriteLineIf(ZTrace.IsInfo, "AutoCompleta Indices en Comun")

                If results(0).Indexs.Count = 0 Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "No hay indices para autocompletar, verifique que la tarea no se haya cerrado previamente")
                End If
                'Recorro los indices del primer result y me fijo cuales no fueron agregados a la coleccion de 
                'indices a completar (son los que se configuraron a mano desde la regla)
                For Each indice As IIndex In results(0).Indexs
                    If Not Me.indices.Contains(Int64.Parse(indice.ID)) Then
                        'si el atributo no esta en la coleccion entonces lo agrego y lo configuro
                        'para que sea resuelto por texto inteligente automaticamente
                        Trace.WriteLineIf(ZTrace.IsInfo, String.Format("Indice {0} se va a Auto Completar con {1}", indice.ID & ": " & indice.Name, "<<Tarea>>.<<Indice(" + indice.Name + ")>>"))
                        Me.indices.Add(Int64.Parse(indice.ID), "<<Tarea>>.<<Indice(" + indice.Name + ")>>")
                        Trace.WriteLineIf(ZTrace.IsInfo, "Indices configurados en la Regla: " & Me.indices.Count)
                    End If
                    Trace.WriteLineIf(ZTrace.IsInfo, String.Format("Indice {0} se va Hereda de la Tarea Padre", indice.ID & ": " & indice.Name))
                Next
            End If

            'Obtengo el dataset o datatable
            Me.valor = String.Empty
            Me.valor = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.indexValue, Nothing)

            Me.objeto = Nothing
            Me.objeto = WFRuleParent.ObtenerValorVariableObjeto(Me.valor)

            Me._filepath = Me.MyRule.FilePath

            'Obtengo la ruta del archivo fisico
            If String.IsNullOrEmpty(MyRule.FilePath) Then
                isVirtual = True
            Else
                isVirtual = False
                If results.Count > 0 Then
                    Me._filepath = TextoInteligente.ReconocerCodigo(Me._filepath, results(0))
                End If
                Me._filepath = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._filepath)
            End If

            'Dependiendo del tipo de objeto, es el tipo de creacion
            If (TypeOf (Me.objeto) Is DataSet) Then


                Me.dt = DirectCast(Me.objeto, DataSet).Tables(0)
                If Not IsNothing(Me.dt) Then
                    If results.Count > 0 Then
                        Me.r = results(0)
                    End If
                    '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                    If Not Me.MyRule.ContinueWithCurrentTasks Then
                        Dim newresults As List(Of ITaskResult) = Me.procesarTabla()
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
                            raiseerror(ex)
                        End Try
                        Return newresults
                    Else
                        Dim newresults As List(Of ITaskResult) = Me.procesarTabla()
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

            ElseIf (TypeOf (Me.objeto) Is DataTable) Then


                Me.dt = Me.objeto

                If Not IsNothing(Me.dt) Then
                    If results.Count > 0 Then
                        Me.r = results(0)
                    End If

                    '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                    If Not Me.MyRule.ContinueWithCurrentTasks Then
                        Dim newresults As List(Of ITaskResult) = Me.procesarTabla()
                        Return newresults
                    Else
                        Dim newresults As List(Of ITaskResult) = Me.procesarTabla()
                        Return results
                    End If
                Else
                    'Si no se obtuvo el objeto, lo devuelvo
                    Return results
                End If

            ElseIf (TypeOf (Me.objeto) Is String AndAlso Me.objeto <> String.Empty) Then


                Me.valor = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me.indexValue)
                Me.valores = New String() {Me.valor}
                'Return Me.createResult(0)
                If Not Me.MyRule.ContinueWithCurrentTasks Then
                    Return Me.createResult(0)
                Else
                    Me.createResult(0)
                    Return results
                End If

            ElseIf (TypeOf (WFRuleParent.ObtenerValorVariableObjeto(Me.valor)) Is Int32) Then


                Me.valor = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me.indexValue)
                Me.valores = New String() {Me.valor}
                '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                If Not Me.MyRule.ContinueWithCurrentTasks Then
                    Return Me.createResult(0)
                Else
                    Me.createResult(0)
                    Return results
                End If

            Else

                If results.Count > 0 Then
                    Me.r = results(0)
                End If
                '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                If Not Me.MyRule.ContinueWithCurrentTasks Then
                    Return Me.createResult(2)
                Else
                    Me.createResult(2)
                    Return results
                End If
                UserBusiness.Rights.SaveAction(Me.r.ID, Zamba.Core.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, Me.MyRule.Name)
                'Else
                '   Trace.WriteLineIf(ZTrace.IsInfo, "No se reconoci� ninguna variable, no se crearan tareas")
                '  Return results
                'End If
            End If

        Finally

            Me.indexValue = Nothing
            Me.ruleindicesaux = Nothing
            Me.strItem = Nothing
            Me.value = Nothing
            Me.valor = Nothing
            Me.valores = New String() {}
            Me.id = Nothing
            Me._filepath = Nothing
            Me.objeto = Nothing
            Me.indices = Nothing
            Me.dt = Nothing
            Me.r = Nothing
            Me.valoraux = Nothing
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
        If Me.indices.Count = 1 Then
            'Creo cada result con un solo indice con valor
            Dim valores(Me.dt.Rows.Count - 1) As String
            Dim i As Int32 = 0
            For Each row As DataRow In Me.dt.Rows
                valores(i) = row(0).ToString()

                i += 1
            Next

            Return Me.createResult(0)
        Else
            'Creo los result a partir de la tabla
            Return Me.createResult(1)
        End If
    End Function

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
        Select Case tipo
            'Case que crea las tareas apartir de los valores.
            Case 0
                For Each valor As String In Me.valores
                    Dim newRes As NewResult = Results_Business.GetNewNewResult(Me.MyRule.docTypeId)
                    newRes.File = Me._filepath

                    For Each indice As String In Me.indices.Keys
                        If Int64.Parse(indice) > 0 Then
                            For i As Int64 = 0 To newRes.Indexs.Count - 1
                                If Int64.Parse(indice) = newRes.Indexs(i).ID Then

                                    newRes.Indexs(i).Data = valor
                                    newRes.Indexs(i).DataTemp = valor
                                    If newRes.Indexs(i).DropDown = IndexAdditionalType.AutoSustituci�n Then

                                        newRes.Indexs(i).dataDescription = AutoSubstitutionBusiness.getDescription(valor, newRes.Indexs(i).ID, False, newRes.Indexs(i).Type)

                                    End If

                                    Exit For
                                End If
                            Next
                        End If
                    Next

                    Select Case (Results_Business.InsertDocument(newRes, False, False, False, False, isVirtual))
                        Case InsertResult.Insertado

                            If Me.MyRule.addCurrentwf = False Then
                                Dim arrayNewRes As ArrayList = New ArrayList()
                                arrayNewRes.Add(newRes)
                                Dim wfID As Int64 = WFBusiness.GetWorkflowIdByStepId(Me.MyRule.WFStepId)
                                arrayItaskResult.Add(WFTaskBusiness.AddNewResultsToWorkFLow(arrayNewRes, wfID, Not Me.MyRule.DontOpenTaskAfterInsert)(0))
                                Trace.WriteLineIf(ZTrace.IsInfo, "Agregado a WF con OpenTaskAfterInsert en " & Me.MyRule.DontOpenTaskAfterInsert)
                            Else
                                Trace.WriteLineIf(ZTrace.IsInfo, "No se adjunta al WF actual, debido a que esta activa la opcion de no adjuntar")
                            End If
                        Case InsertResult.ErrorIndicesIncompletos
                            Trace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de indices obligatorios")
                        Case InsertResult.ErrorIndicesInvalidos
                            Trace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar, hay indices con datos invalidos")
                    End Select

                Next

                'Case que crea los result a partir de la tabla
            Case 1
                Dim arrayNewRes As ArrayList = New ArrayList()

                For Each row As DataRow In Me.dt.Rows
                    'Obtengo un nuevo result
                    Dim newRes As NewResult = Results_Business.GetNewNewResult(Me.MyRule.docTypeId)
                    newRes.File = Me._filepath
                    'Por cada indice pedido obtengo los datos
                    For Each indice As String In Me.indices.Keys
                        If Int64.Parse(indice) <> 0 Then
                            For i As Int64 = 0 To newRes.Indexs.Count - 1
                                If Int64.Parse(indice) = newRes.Indexs(i).ID Then

                                    If Me.dt.Columns.Contains(Me.indices(Int64.Parse(indice))) Then
                                        Trace.WriteLineIf(ZTrace.IsInfo, "Asignando a: " & indice & " Asignando valor: " & row(Me.indices(Int64.Parse(indice))))
                                        newRes.Indexs(i).Data = row(Me.indices(Int64.Parse(indice))).ToString()
                                        newRes.Indexs(i).DataTemp = row(Me.indices(Int64.Parse(indice))).ToString()
                                    Else
                                        Me.valoraux = Me.indices(Int64.Parse(indice))
                                        Me.valoraux = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me.valoraux)
                                        If Not Me.r Is Nothing Then
                                            Me.valoraux = TextoInteligente.ReconocerCodigo(Me.valoraux, Me.r)
                                        End If
                                        Trace.WriteLineIf(ZTrace.IsInfo, "Asignando a: " & indice & " Asignando valor: " & Me.valoraux)
                                        newRes.Indexs(i).Data = Me.valoraux
                                        newRes.Indexs(i).DataTemp = Me.valoraux
                                    End If


                                    If newRes.Indexs(i).DropDown = IndexAdditionalType.AutoSustituci�n Then
                                        newRes.Indexs(i).dataDescription = AutoSubstitutionBusiness.getDescription(newRes.Indexs(i).Data, newRes.Indexs(i).ID, False, newRes.Indexs(i).Type)
                                        Trace.WriteLineIf(ZTrace.IsInfo, "Valor descripcion: " & newRes.Indexs(i).dataDescription)
                                    End If

                                    Exit For
                                End If
                            Next
                        End If
                    Next

                    'Inserto el nuevo result en zamba
                    Select Case Results_Business.InsertDocument(newRes, False, False, False, False, isvirtual)
                        Case InsertResult.Insertado
                            Trace.WriteLineIf(ZTrace.IsInfo, "Insertado en Zamba:" & newRes.ID)

                            If newRes.ID > 0 And Me.MyRule.addCurrentwf = False Then
                                'Agrego el result al wf
                                arrayNewRes.Add(newRes)
                            End If

                        Case InsertResult.ErrorIndicesIncompletos
                            Trace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de indices obligatorios")
                        Case InsertResult.ErrorIndicesInvalidos
                            Trace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar, hay indices con datos invalidos")
                    End Select

                Next
                '[Ezequiel] 03/12/09 - Movi de lugar la llamada al metodo que agrega al wf asi agrega todos los documentos creados de una para mas performance.
                If arrayNewRes.Count > 0 AndAlso Me.MyRule.addCurrentwf = False Then

                    Dim wfID As Int64 = WFBusiness.GetWorkflowIdByStepId(Me.MyRule.WFStepId)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Agregado a WF")
                    arrayItaskResult.AddRange(WFTaskBusiness.AddNewResultsToWorkFLow(arrayNewRes, wfID, Not Me.MyRule.DontOpenTaskAfterInsert))
                End If
                'Case que crea una sola tarea
            Case 2
                Dim newRes As NewResult = Results_Business.GetNewNewResult(Me.MyRule.docTypeId)
                newRes.File = Me._filepath
                Trace.WriteLineIf(ZTrace.IsVerbose, String.Format("Nueva Tarea con {0} Indices", newRes.Indexs.Count))
                Trace.WriteLineIf(ZTrace.IsVerbose, String.Format("Indices a Verificar configurados {0}", Me.indices.Count))
                For Each indice As Int64 In Me.indices.Keys
                    If Int64.Parse(indice) > 0 Then
                        For i As Int64 = 0 To newRes.Indexs.Count - 1
                            If Int64.Parse(indice) = newRes.Indexs(i).ID Then

                                'Si el indice no es referencial, le completo el valor
                                If IndexsBusiness.getReferenceStatus(MyRule.docTypeId, indice) = False Then
                                    Dim value As String
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Reconociendo Texto: " & Me.indices(indice))
                                    value = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.indices(indice), Me.r)
                                    Trace.WriteLineIf(ZTrace.IsVerbose, "Reconociendo Variables: " & value)
                                    value = WFRuleParent.ReconocerVariables(value)

                                    Trace.WriteLineIf(ZTrace.IsInfo, "Procesando indice: " & newRes.Indexs(i).name & " Asignando valor: " & value)
                                    newRes.Indexs(i).Data = value
                                    newRes.Indexs(i).DataTemp = value


                                    If newRes.Indexs(i).DropDown = IndexAdditionalType.AutoSustituci�n Then

                                        newRes.Indexs(i).dataDescription = AutoSubstitutionBusiness.getDescription(value, newRes.Indexs(i).ID, False, newRes.Indexs(i).Type)
                                        Trace.WriteLineIf(ZTrace.IsInfo, "Valor descripcion: " & newRes.Indexs(i).dataDescription)
                                    End If
                                End If
                                Exit For
                            End If
                        Next
                    End If
                Next

                'Inserto el nuevo result en zamba
                Select Case Results_Business.InsertDocument(newRes, False, False, False, False, isVirtual, False, False, Not MyRule.DontOpenTaskAfterInsert)
                    Case InsertResult.Insertado
                        Trace.WriteLineIf(ZTrace.IsInfo, "Insertado en Zamba:" & newRes.ID)

                        If newRes.ID > 0 Then
                            If Me.MyRule.addCurrentwf = False Then
                                'Agrego el result al wf
                                Dim arrayNewRes As ArrayList = New ArrayList()
                                arrayNewRes.Add(newRes)

                                Dim wfID As Int64


                                If Not Me.r Is Nothing Then
                                    wfID = r.WorkId
                                Else
                                    wfID = WFBusiness.GetWorkflowIdByStepId(Me.MyRule.WFStepId)
                                End If


                                Dim newITaskResultList As System.Collections.Generic.List(Of ITaskResult)
                                newITaskResultList = WFTaskBusiness.AddNewResultsToWorkFLow(arrayNewRes, wfID, Not Me.MyRule.DontOpenTaskAfterInsert)
                                If newITaskResultList Is Nothing Then
                                    Throw New Exception("No se inserto la nueva tarea en WF")
                                ElseIf newITaskResultList.Count = 0 Then
                                    Throw New Exception("No se inserto la nueva tarea en WF")
                                Else
                                    arrayItaskResult.Add(newITaskResultList(0))
                                    Trace.WriteLineIf(ZTrace.IsInfo, "Agregado a WF")
                                End If
                            Else
                                Trace.WriteLineIf(ZTrace.IsInfo, "No se adjunta al WF actual, debido a que esta activa la opcion de no adjuntar")

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
                                    Dim WFId As Int64 = WFBusiness.GetWFAssociationByDocTypeId(task.DocTypeId)
                                    If Me.MyRule.showDocument And WFId = 0 Then
                                      
                                        ZambaCore.HandleModule(ResultActions.MostrarResult, newRes, Nothing)
                                    End If

                                End If
                                arrayItaskResult.Add(task)
                            End If

                            Dim path As String = Me._filepath

                            If String.Compare(IO.Path.GetExtension(path).ToLower().Trim(), ".msg") = 0 Then

                                Try
                                    path = path.Substring(0, path.LastIndexOf(".")) & ".html"


                                    If IO.File.Exists(path) Then
                                        Dim destino As String = IO.Path.GetDirectoryName(newRes.FullPath) & "\" & IO.Path.GetFileName(newRes.FullPath)
                                        destino = destino.Substring(0, destino.LastIndexOf(".")) & ".html"
                                        IO.File.Copy(path, destino)
                                        Trace.WriteLineIf(ZTrace.IsInfo, "La copia se ha realizado con �xito. Ruta: " & destino)
                                    Else
                                        Trace.WriteLineIf(ZTrace.IsInfo, "No se encontr� el archivo: " & path)
                                    End If

                                Catch ex As Exception
                                    ZClass.raiseerror(ex)
                                End Try
                            End If

                        End If
                    Case InsertResult.ErrorIndicesIncompletos
                        Trace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de indices obligatorios")
                    Case InsertResult.ErrorIndicesInvalidos
                        Trace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de indices obligatorios")
                End Select
        End Select
        Trace.WriteLineIf(ZTrace.IsInfo, "Tarea generada con �xito!")
        Return arrayItaskResult
    End Function
    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class