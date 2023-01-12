Imports Zamba.Core.WF.WF
Imports Zamba.Core
Imports Zamba.Data
Imports System.IO

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
    Private SpecificWorkflowId As Int64

    Dim WFTB As New WFTaskBusiness
    Dim UB As New UserBusiness
    Dim ASB As New AutoSubstitutionBusiness

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
        Dim VarInterReglas As New VariablesInterReglas()
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "DoGenerateTaskResult")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Indices: " & Me.MyRule.indices)
            Me.indices = New SortedList()
            Me.indices.Clear()
            Me.indexValue = String.Empty
            Me.ruleindicesaux = Me.MyRule.indices.Replace("//", "§")

            'Obtengo todos los indices que tengo q llenar y la columna de donde va a salir el valor
            While Not String.IsNullOrEmpty(Me.ruleindicesaux)
                strItem = Me.ruleindicesaux.Split("§")(0)

                'Obtiene el id del idncie
                id = Int(strItem.Split("|")(0))
                'Obtiene el valor (sin el la marca para no autocompletar)
                value = strItem.Split("|")(1)

                ' IM - Copie este fragmento del metodo PlayWeb
                If (id = 0) Then
                    'Si hay un indice 0 y tiene zvar, es la variable
                    If (value.Contains("zvar")) Then
                        indexValue = value
                    Else
                        If indices.ContainsKey(Int64.Parse(id)) = False Then indices.Add(Int64.Parse(id), value)
                    End If
                Else
                    If indices.ContainsKey(Int64.Parse(id)) = False Then indices.Add(Int64.Parse(id), value)
                End If

                Me.ruleindicesaux = Me.ruleindicesaux.Remove(0, Me.ruleindicesaux.Split("§")(0).Length)
                If Me.ruleindicesaux.Length > 0 Then
                    Me.ruleindicesaux = Me.ruleindicesaux.Remove(0, 1)
                End If
            End While


            'Valido si selecciono la opcion de autocompletar los indices que tengan en comun
            If Me.MyRule.AutocompleteIndexsInCommon AndAlso results.Count > 0 Then

                'Recorro los indices del primer result y me fijo cuales no fueron agregados a la coleccion de 
                'indices a completar (son los que se configuraron a mano desde la regla)
                For Each indice As IIndex In results(0).Indexs

                    If Not Me.indices.Contains(Int64.Parse(indice.ID)) Then
                        'si el indice no esta en la coleccion entonces lo agrego y lo configuro
                        'para que sea resuelto por texto inteligente automaticamente
                        Me.indices.Add(Int64.Parse(indice.ID), "<<Tarea>>.<<Indice(" + indice.Name + ")>>")
                    End If

                Next

            End If

            'Obtengo el dataset o datatable
            Me.valor = String.Empty
            Me.valor = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.indexValue, Nothing)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor: " & Me.valor)
            Me.objeto = Nothing
            Me.objeto = WFRuleParent.ObtenerValorVariableObjeto(Me.valor)

            Me._filepath = Me.MyRule.FilePath

            'Obtengo la ruta del archivo fisico
            If String.IsNullOrEmpty(MyRule.FilePath) Then
                isVirtual = True
            Else
                isVirtual = False
                Try
                    If results.Count > 0 Then
                        Me._filepath = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._filepath)
                        Me._filepath = TextoInteligente.ReconocerCodigo(Me._filepath, results(0))
                    End If
                    Me._filepath = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._filepath)

                Catch ex As Exception
                    If (ex.ToString.Contains("Error")) Then
                        ZTrace.WriteLineIf(ZTrace.IsError, "No existe el archivo")
                        Throw New Exception("No existe el archivo")
                    Else
                        Throw
                    End If
                End Try

                ' Si el archivo no existe en la ruta lanzo exception
                If Not File.Exists(Me._filepath) Then
                    ZTrace.WriteLineIf(ZTrace.IsError, "No existe el archivo en la ruta: " & Me._filepath)
                    Throw New Exception("No se pudo realizar la acción solicitada por no encontrarse el archivo requerido")
                End If

            End If


            'Dependiendo del tipo de objeto, es el tipo de creacion
            If (TypeOf (Me.objeto) Is DataSet) Then

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Es Dataset")
                Me.dt = DirectCast(Me.objeto, DataSet).Tables(0)
                If Not IsNothing(Me.dt) Then
                    If results.Count > 0 Then
                        Me.r = results(0)
                    End If
                    '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                    If Not Me.MyRule.ContinueWithCurrentTasks Then
                        Return Me.procesarTabla()
                    Else
                        Me.procesarTabla()
                        Return results
                    End If
                Else
                    'Si no se obtuvo el objeto, lo devuelvo
                    Return results
                End If

            ElseIf (TypeOf (Me.objeto) Is DataTable) Then

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Es Datatable")
                Me.dt = Me.objeto

                If Not IsNothing(Me.dt) Then
                    If results.Count > 0 Then
                        Me.r = results(0)
                    End If

                    '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                    If Not Me.MyRule.ContinueWithCurrentTasks Then
                        Return Me.procesarTabla()
                    Else
                        Me.procesarTabla()
                        Return results
                    End If
                Else
                    'Si no se obtuvo el objeto, lo devuelvo
                    Return results
                End If

            ElseIf (TypeOf (Me.objeto) Is String AndAlso Me.objeto.ToString.Length > 0) Then

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Es String")
                Me.valor = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me.indexValue)
                Me.valores = New String() {Me.valor}
                Return Me.createResult(0)

            ElseIf (TypeOf (WFRuleParent.ObtenerValorVariableObjeto(Me.valor)) Is Int32) Then

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Es Int32")
                Me.valor = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me.indexValue)
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
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se reconoció ninguna variable, se creara una sola tarea")
                    Me.r = results(0)
                    '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                    If Not Me.MyRule.ContinueWithCurrentTasks Then
                        Return Me.createResult(2)
                    Else
                        Me.createResult(2)
                        Return results
                    End If
                    UB.SaveAction(Me.r.ID, Zamba.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, Me.MyRule.Name)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se reconoció ninguna variable, no se crearan tareas")
                    Return results
                End If
            End If

        Finally
            VarInterReglas = Nothing
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
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valores " & i & " " & valores(i))
                i += 1
            Next
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de valores: " & Me.dt.Rows.Count)
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
        Dim VarInterReglas As New VariablesInterReglas()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando tarea...")
        'Crea un nuevo result y lo inserta en Zamba
        Dim ResultBusiness As New Results_Business
        Dim WFB As New WFBusiness

        Dim arrayItaskResult As New Generic.List(Of Core.ITaskResult)()
        Select Case tipo
            'Case que crea las tareas apartir de los valores.
            Case 0
                For Each valor As String In Me.valores
                    Dim newRes As NewResult = ResultBusiness.GetNewNewResult(Me.MyRule.docTypeId)
                    newRes.File = Me._filepath
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "New Result")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor indice: " & valor)
                    For Each indice As String In Me.indices.Keys
                        For Each index As Index In newRes.Indexs
                            If index.ID = Int64.Parse(indice) Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando valor: " & valor)
                                index.Data = valor
                                index.DataTemp = valor
                                If index.DropDown = IndexAdditionalType.AutoSustitución OrElse index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Indice de Sustitucion - Intentando recuperar descripcion")
                                    index.dataDescription = ASB.getDescription(valor, index.ID)
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor descripcion: " & index.dataDescription)
                                End If
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor asignado")
                                Exit For
                            End If
                        Next
                    Next

                    Select Case (ResultBusiness.Insert(newRes, False, False, False, False, isVirtual))
                        Case InsertResult.Insertado
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertado en Zamba " & valor)
                            If Me.MyRule.addCurrentwf = False Then
                                Dim arrayNewRes As ArrayList = New ArrayList()
                                arrayNewRes.Add(newRes)
                                Dim wfID As Int64 = WFB.GetWorkflowIdByStepId(Me.MyRule.WFStepId)
                                arrayItaskResult.Add(WFTB.AddNewResultsToWorkFLow(arrayNewRes, wfID, Not Me.MyRule.DontOpenTaskAfterInsert)(0))
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregado a WF con OpenTaskAfterInsert en " & Me.MyRule.DontOpenTaskAfterInsert)
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se adjunta al WF actual, debido a que esta activa la opcion de no adjuntar")
                            End If
                        Case InsertResult.ErrorIndicesIncompletos
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de atributos obligatorios")
                        Case InsertResult.ErrorIndicesInvalidos
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar, hay indices con datos invalidos")
                    End Select

                Next
                'Case que crea los result a partir de la tabla
            Case 1

                Dim arrayNewRes As ArrayList = New ArrayList()

                For Each row As DataRow In Me.dt.Rows
                    'Obtengo un nuevo result
                    Dim newRes As NewResult = ResultBusiness.GetNewNewResult(Me.MyRule.docTypeId)
                    newRes.File = Me._filepath
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "New Result")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando Indices:")
                    'Por cada indice pedido obtengo los datos
                    For Each indice As String In Me.indices.Keys
                        If Int64.Parse(indice) <> 0 Then
                            For Each resIndice As Index In newRes.Indexs
                                If resIndice.ID = Int64.Parse(indice) Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando a: " & indice)

                                    If Me.dt.Columns.Contains(Me.indices(Int64.Parse(indice))) Then
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando valor: " & row(Me.indices(Int64.Parse(indice))))
                                        resIndice.Data = row(Me.indices(Int64.Parse(indice))).ToString()
                                        resIndice.DataTemp = row(Me.indices(Int64.Parse(indice))).ToString()
                                    Else
                                        Me.valoraux = Me.indices(Int64.Parse(indice))
                                        Me.valoraux = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me.valoraux)
                                        If Not Me.r Is Nothing Then
                                            Me.valoraux = TextoInteligente.ReconocerCodigo(Me.valoraux, Me.r)
                                        End If
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando valor: " & Me.valoraux)
                                        resIndice.Data = Me.valoraux
                                        resIndice.DataTemp = Me.valoraux
                                    End If

                                    If resIndice.DropDown = IndexAdditionalType.AutoSustitución OrElse resIndice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Indice de Sustitucion- Intentando recuperar descripcion")
                                        resIndice.dataDescription = ASB.getDescription(resIndice.Data, resIndice.ID)
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor descripcion: " & resIndice.dataDescription)
                                    End If
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor asignado")
                                    Exit For
                                End If
                            Next
                        End If
                    Next


                    'Inserto el nuevo result en zamba
                    Select Case ResultBusiness.Insert(newRes, False, False, False, False, isVirtual)
                        Case InsertResult.Insertado
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertado en Zamba:" & newRes.ID)

                            If newRes.ID > 0 And Me.MyRule.addCurrentwf = False Then
                                'Agrego el result al wf
                                arrayNewRes.Add(newRes)
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se adjunta al WF actual, debido a que esta activa la opcion de no adjuntar")
                            End If
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº")
                        Case InsertResult.ErrorIndicesIncompletos
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de atributos obligatorios")
                        Case InsertResult.ErrorIndicesInvalidos
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar, hay indices con datos invalidos")
                    End Select

                Next
                '[Ezequiel] 03/12/09 - Movi de lugar la llamada al metodo que agrega al wf asi agrega todos los documentos creados de una para mas performance.
                If arrayNewRes.Count > 0 AndAlso Me.MyRule.addCurrentwf = False Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando a WF")
                    Dim wfID As Int64 = WFB.GetWorkflowIdByStepId(Me.MyRule.WFStepId)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregado a WF")
                    arrayItaskResult.AddRange(WFTB.AddNewResultsToWorkFLow(arrayNewRes, wfID, Not Me.MyRule.DontOpenTaskAfterInsert))
                End If
                'Case que crea una sola tarea
            Case 2
                Dim newRes As NewResult = ResultBusiness.GetNewNewResult(Me.MyRule.docTypeId)
                newRes.File = Me._filepath
                ZTrace.WriteLineIf(ZTrace.IsInfo, "New Result")
                For Each indice As Int64 In Me.indices.Keys
                    For Each index As Index In newRes.Indexs
                        If index.ID = indice Then
                            Dim value As String
                            value = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.indices(indice), Me.r)
                            value = VarInterReglas.ReconocerVariables(value)

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando valor: " & value)
                            index.Data = value
                            index.DataTemp = value
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor asignado")
                            Exit For
                        End If
                    Next
                Next


                'Inserto el nuevo result en zamba
                Select Case ResultBusiness.Insert(newRes, False, False, False, False, isVirtual, False, False, Not MyRule.DontOpenTaskAfterInsert)
                    Case InsertResult.Insertado
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertado en Zamba:" & newRes.ID)

                        If newRes.ID > 0 Then
                            If Me.MyRule.addCurrentwf = False Then
                                'Agrego el result al wf
                                Dim arrayNewRes As ArrayList = New ArrayList()
                                arrayNewRes.Add(newRes)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando a WF")
                                Dim wfID As Int64
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtengo el WFID")

                                If Not Me.r Is Nothing Then
                                    wfID = r.WorkId
                                Else
                                    wfID = WFB.GetWorkflowIdByStepId(Me.MyRule.WFStepId)
                                End If

                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtengo la lista de ITaskResults insertando en WF")
                                Dim newITaskResultList As System.Collections.Generic.List(Of ITaskResult)
                                newITaskResultList = WFTB.AddNewResultsToWorkFLow(arrayNewRes, wfID, Not Me.MyRule.DontOpenTaskAfterInsert)
                                If newITaskResultList Is Nothing Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La lista de Itaskresults es nothing")
                                    Throw New Exception("No se inserto la nueva tarea en WF")
                                ElseIf newITaskResultList.Count = 0 Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La lista de Itaskresults esta vacia")
                                    Throw New Exception("No se inserto la nueva tarea en WF")
                                Else
                                    arrayItaskResult.Add(newITaskResultList(0))
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregado a WF")
                                End If
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se adjunta al WF actual, debido a que esta activa la opcion de no adjuntar")

                                Dim task As ITaskResult = WFTB.GetTaskByDocIdAndWorkFlowId(newRes.ID, 0)
                                If Not IsNothing(task) Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se adquiere la tarea del WF: " & task.WorkId)
                                Else
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La tarea no se encuentra en ningun WF, se agrega una tarea vacia")
                                    task = New TaskResult()
                                    task.ID = newRes.ID
                                    task.Indexs = newRes.Indexs
                                    task.DocType = newRes.DocType
                                    task.DocTypeId = newRes.DocTypeId

                                End If
                                arrayItaskResult.Add(task)
                            End If

                            Dim path As String = Me._filepath

                            If String.Compare(IO.Path.GetExtension(path).ToLower().Trim(), ".msg") = 0 Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo es de extensión MSG.")
                                Try
                                    path = path.Substring(0, path.LastIndexOf(".")) & ".html"

                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia del archivo: " & path)
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

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº")
                        End If
                    Case InsertResult.ErrorIndicesIncompletos
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de atributos obligatorios")
                    Case InsertResult.ErrorIndicesInvalidos
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de atributos obligatorios")
                End Select
        End Select
        VarInterReglas = Nothing
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Tarea generada con éxito!")
        ResultBusiness = Nothing
        WFB = Nothing

        Return arrayItaskResult
    End Function

    ''' <summary>
    ''' Ejecucion de la regla
    ''' </summary>
    ''' <param name="results">results a ejecutar</param>
    ''' <history>   Marcelo Modified    12/01/2010</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim VarInterReglas As New VariablesInterReglas()
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "DoGenerateTaskResult")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Indices: " & Me.MyRule.indices)
            Me.indices = New SortedList()
            Me.indices.Clear()
            Me.indexValue = String.Empty
            Me.ruleindicesaux = Me.MyRule.indices.Replace("//", "§")

            'Obtengo todos los indices que tengo q llenar y la columna de donde va a salir el valor
            While Not String.IsNullOrEmpty(Me.ruleindicesaux)
                strItem = Me.ruleindicesaux.Split("§")(0)

                'Obtiene el id del idncie
                id = Int(strItem.Split("|")(0))
                'Obtiene el valor (sin el la marca para no autocompletar)
                value = strItem.Split("|")(1)

                If (id = 0) Then
                    'Si hay un indice 0 y tiene zvar, es la variable
                    If (value.Contains("zvar")) Then
                        indexValue = value
                    Else
                        If indices.ContainsKey(Int64.Parse(id)) = False Then indices.Add(Int64.Parse(id), value)
                    End If
                Else
                    If indices.ContainsKey(Int64.Parse(id)) = False Then indices.Add(Int64.Parse(id), value)
                End If

                Me.ruleindicesaux = Me.ruleindicesaux.Remove(0, Me.ruleindicesaux.Split("§")(0).Length)
                If Me.ruleindicesaux.Length > 0 Then
                    Me.ruleindicesaux = Me.ruleindicesaux.Remove(0, 1)
                End If
            End While


            'Valido si selecciono la opcion de autocompletar los indices que tengan en comun
            If Me.MyRule.AutocompleteIndexsInCommon AndAlso results.Count > 0 Then

                'Recorro los indices del primer result y me fijo cuales no fueron agregados a la coleccion de 
                'indices a completar (son los que se configuraron a mano desde la regla)
                For Each indice As IIndex In results(0).Indexs
                    If Not Me.indices.Contains(Int64.Parse(indice.ID)) Then
                        'si el indice no esta en la coleccion entonces lo agrego y lo configuro
                        'para que sea resuelto por texto inteligente automaticamente
                        Me.indices.Add(Int64.Parse(indice.ID), "<<Tarea>>.<<Indice(" + indice.Name + ")>>")
                    End If
                Next
            End If

            'Obtengo el dataset o datatable
            Me.valor = String.Empty
            Me.valor = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.indexValue, Nothing)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor: " & Me.valor)
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
                Me._filepath = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._filepath)

                ' Si el archivo no existe en la ruta lanzo exception
                If Not File.Exists(Me._filepath) Then
                    ZTrace.WriteLineIf(ZTrace.IsError, "No existe el archivo en la ruta: " & Me._filepath)
                    Throw New Exception("No se pudo realizar la acción solicitada por no encontrarse el archivo requerido ")
                End If

            End If

            'Dependiendo del tipo de objeto, es el tipo de creacion
            If (TypeOf (Me.objeto) Is DataSet) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Es Dataset")
                Me.dt = DirectCast(Me.objeto, DataSet).Tables(0)
                If Not IsNothing(Me.dt) Then
                    If results.Count > 0 Then
                        Me.r = results(0)
                    End If
                    '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                    If Not Me.MyRule.ContinueWithCurrentTasks Then
                        Return Me.procesarTablaWeb(Params)
                    Else
                        Me.procesarTablaWeb(Params)
                        Return results
                    End If
                Else
                    'Si no se obtuvo el objeto, lo devuelvo
                    Return results
                End If
            ElseIf (TypeOf (Me.objeto) Is DataTable) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Es Datatable")
                Me.dt = Me.objeto

                If Not IsNothing(Me.dt) Then
                    If results.Count > 0 Then
                        Me.r = results(0)
                    End If

                    '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                    If Not Me.MyRule.ContinueWithCurrentTasks Then
                        Return Me.procesarTablaWeb(Params)
                    Else
                        Me.procesarTablaWeb(Params)
                        Return results
                    End If
                Else
                    'Si no se obtuvo el objeto, lo devuelvo
                    Return results
                End If
            ElseIf (TypeOf (Me.objeto) Is String AndAlso Me.objeto.ToString.Length > 0) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Es String")
                Me.valor = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me.indexValue)
                Me.valores = New String() {Me.valor}
                Return Me.createWebResult(0, Params)
            ElseIf (TypeOf (WFRuleParent.ObtenerValorVariableObjeto(Me.valor)) Is Int32) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Es Int32")
                Me.valor = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me.indexValue)
                Me.valores = New String() {Me.valor}
                '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                If Not Me.MyRule.ContinueWithCurrentTasks Then
                    Return Me.createWebResult(0, Params)
                Else
                    Me.createWebResult(0, Params)
                    Return results
                End If
            Else
                'If results.Count > 0 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se reconoció ninguna variable, se creara un solo documento")
                If results.Count > 0 Then
                    Me.r = results(0)
                End If
                '[Ezequiel] - 23/11/09 - Valido si se tiene que continuar la ejecucion con los nuevos resultados
                If Not Me.MyRule.ContinueWithCurrentTasks Then
                    Return Me.createWebResult(2, Params)
                Else
                    Me.createWebResult(2, Params)


                    Return results
                End If
                UB.SaveAction(Me.r.ID, Zamba.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, Me.MyRule.Name)
                'Else
                'ZTrace.WriteLineIf(ZTrace.IsInfo, "No se reconoció ninguna variable, no se crearan tareas")
                'Return results
                'End If
            End If




        Finally
            VarInterReglas = Nothing
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
    ''' 
    ''' </summary>
    ''' <param name="valores"></param>
    ''' <param name="rule"></param>
    ''' <param name="indices"></param>
    ''' <returns></returns>
    ''' <history>Marcelo modified 07/11/08</history>
    ''' <remarks></remarks>
    Private Function createWebResult(ByVal tipo As Int16, ByRef Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando tarea...")
        Dim VarInterReglas As New VariablesInterReglas()
        Dim ResultBusiness As New Results_Business
        Dim WFB As New WFBusiness


        'Crea un nuevo result y lo inserta en Zamba
        Dim arrayItaskResult As New Generic.List(Of Core.ITaskResult)()
        Select Case tipo
            'Case que crea las tareas apartir de los valores.
            Case 0
                For Each valor As String In Me.valores
                    Dim newRes As NewResult = ResultBusiness.GetNewNewResult(Me.MyRule.docTypeId)
                    newRes.File = Me._filepath
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "New Result")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor indice: " & valor)
                    For Each indice As String In Me.indices.Keys
                        For Each index As Index In newRes.Indexs
                            If index.ID = Int64.Parse(indice) Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando valor: " & valor)
                                index.Data = valor
                                index.DataTemp = valor
                                If index.DropDown = IndexAdditionalType.AutoSustitución OrElse index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Indice de Sustitucion - Intentando recuperar descripcion")
                                    'index.dataDescription = ASB.getDescription(valor, index.ID, False, index.Type)
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor descripcion: " & index.dataDescription)
                                End If
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor asignado")
                                Exit For
                            End If
                        Next
                    Next

                    Select Case (ResultBusiness.Insert(newRes, False, False, False, False, isVirtual))
                        Case InsertResult.Insertado
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertado en Zamba " & valor)

                            If VariablesInterReglas.ContainsKey("GeneratedDocId") Then
                                VariablesInterReglas.Item("GeneratedDocId") = newRes.ID
                            Else
                                VariablesInterReglas.Add("GeneratedDocId", newRes.ID)
                            End If
                            'If VariablesInterReglas.ContainsKey("accion") Then
                            '    VariablesInterReglas.Item("accion") = newRes.ID
                            'Else
                            '    VariablesInterReglas.Add("accion", newRes.ID)
                            'End If

                            If Params.Contains("ResultID") = False Then
                                Params.Add("ResultID", newRes.ID)
                                Params.Add("DocTypeId", newRes.DocTypeId)
                                Params.Add("DocName", newRes.Name)
                                ' step id y taskid
                            End If
                            Params.Add("OpenTaskAfterInsert", Not Me.MyRule.DontOpenTaskAfterInsert)
                            Params.Add("OpenMode", MyRule.OpenMode)

                            If Me.MyRule.addCurrentwf = False Then
                                Dim arrayNewRes As ArrayList = New ArrayList()
                                arrayNewRes.Add(newRes)
                                Dim wfID As Int64 = WFB.GetWorkflowIdByStepId(Me.MyRule.WFStepId)

                                arrayItaskResult.Add(WFTB.AddNewResultsToWorkFLow(arrayNewRes, wfID, Not Me.MyRule.DontOpenTaskAfterInsert)(0))
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregado a WF con OpenTaskAfterInsert en " & Me.MyRule.DontOpenTaskAfterInsert)
                            ElseIf (MyRule.SpecificWorkflowId > 0) Then
                                Dim arrayNewRes As ArrayList = New ArrayList()
                                arrayNewRes.Add(newRes)
                                arrayItaskResult.Add(WFTB.AddNewResultsToWorkFLow(arrayNewRes, MyRule.SpecificWorkflowId, Not Me.MyRule.DontOpenTaskAfterInsert)(0))

                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se adjunta al WF actual, debido a que esta activa la opcion de no adjuntar")

                            End If
                        Case InsertResult.ErrorIndicesIncompletos
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de atributos obligatorios")
                            Params.Add("Error", "No se pudo insertar por falta de atributos obligatorios")

                        Case InsertResult.ErrorIndicesInvalidos
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar, hay indices con datos invalidos")
                            Params.Add("Error", "No se pudo insertar, hay indices con datos invalidos")
                    End Select

                Next
                'Case que crea los result a partir de la tabla
            Case 1
                Dim arrayNewRes As ArrayList = New ArrayList()

                For Each row As DataRow In Me.dt.Rows
                    'Obtengo un nuevo result
                    Dim newRes As NewResult = ResultBusiness.GetNewNewResult(Me.MyRule.docTypeId)
                    newRes.File = Me._filepath
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "New Result")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando Indices:")
                    'Por cada indice pedido obtengo los datos
                    For Each indice As String In Me.indices.Keys
                        If Int64.Parse(indice) <> 0 Then
                            For Each resIndice As Index In newRes.Indexs
                                If resIndice.ID = Int64.Parse(indice) Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando a: " & indice)

                                    If Me.dt.Columns.Contains(Me.indices(Int64.Parse(indice))) Then
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando valor: " & row(Me.indices(Int64.Parse(indice))))
                                        resIndice.Data = row(Me.indices(Int64.Parse(indice))).ToString()
                                        resIndice.DataTemp = row(Me.indices(Int64.Parse(indice))).ToString()
                                    Else
                                        Me.valoraux = Me.indices(Int64.Parse(indice))
                                        Me.valoraux = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me.valoraux)
                                        If Not Me.r Is Nothing Then
                                            Me.valoraux = TextoInteligente.ReconocerCodigo(Me.valoraux, Me.r)
                                        End If
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando valor: " & Me.valoraux)
                                        resIndice.Data = Me.valoraux
                                        resIndice.DataTemp = Me.valoraux
                                    End If

                                    If resIndice.DropDown = IndexAdditionalType.AutoSustitución OrElse resIndice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Indice de Sustitucion- Intentando recuperar descripcion")
                                        resIndice.dataDescription = ASB.getDescription(resIndice.Data, resIndice.ID)
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor descripcion: " & resIndice.dataDescription)
                                    End If
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor asignado")
                                    Exit For
                                End If
                            Next
                        End If
                    Next

                    'Inserto el nuevo result en zamba
                    Select Case ResultBusiness.Insert(newRes, False, False, False, False, isVirtual)
                        Case InsertResult.Insertado
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertado en Zamba:" & newRes.ID)

                            If VariablesInterReglas.ContainsKey("GeneratedDocId") Then
                                VariablesInterReglas.Item("GeneratedDocId") = newRes.ID
                            Else
                                VariablesInterReglas.Add("GeneratedDocId", newRes.ID)
                            End If

                            If Params.Contains("ResultID") = False Then
                                Params.Add("ResultID", newRes.ID)
                                Params.Add("DocTypeId", newRes.DocTypeId)
                                Params.Add("DocName", newRes.Name)
                            End If
                            Params.Add("OpenTaskAfterInsert", Not Me.MyRule.DontOpenTaskAfterInsert)
                            Params.Add("OpenMode", MyRule.OpenMode)

                            If newRes.ID > 0 And Me.MyRule.addCurrentwf = False Then
                                'Agrego el result al wf
                                arrayNewRes.Add(newRes)
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se adjunta al WF actual, debido a que esta activa la opcion de no adjuntar")
                            End If
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº")
                        Case InsertResult.ErrorIndicesIncompletos
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de atributos obligatorios")
                            Params.Add("Error", "No se pudo insertar por falta de atributos obligatorios")
                        Case InsertResult.ErrorIndicesInvalidos
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar, hay indices con datos invalidos")
                            Params.Add("Error", "No se pudo insertar, hay indices con datos invalidos")
                    End Select
                Next
                '[Ezequiel] 03/12/09 - Movi de lugar la llamada al metodo que agrega al wf asi agrega todos los documentos creados de una para mas performance.
                If arrayNewRes.Count > 0 AndAlso Me.MyRule.addCurrentwf = False Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando a WF")
                    Dim wfID As Int64 = WFB.GetWorkflowIdByStepId(Me.MyRule.WFStepId)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregado a WF")
                    arrayItaskResult.AddRange(WFTB.AddNewResultsToWorkFLow(arrayNewRes, wfID, Not Me.MyRule.DontOpenTaskAfterInsert))
                End If
                'Case que crea una sola tarea
            Case 2
                Dim newRes As NewResult = ResultBusiness.GetNewNewResult(Me.MyRule.docTypeId)
                newRes.File = Me._filepath
                ZTrace.WriteLineIf(ZTrace.IsInfo, "New Result")
                For Each indice As Int64 In Me.indices.Keys
                    For Each index As Index In newRes.Indexs
                        If index.ID = indice Then
                            Dim value As String
                            value = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.indices(indice), Me.r)
                            value = VarInterReglas.ReconocerVariables(value)

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando valor: " & value)
                            index.Data = value
                            index.DataTemp = value
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor asignado")
                            Exit For
                        End If
                    Next
                Next


                'Inserto el nuevo result en zamba
                Dim ExecuteEntryRules As Boolean = MyRule.DontOpenTaskAfterInsert
                Select Case ResultBusiness.Insert(newRes, False, False, False, False, isVirtual, False, False, ExecuteEntryRules)
                    Case InsertResult.Insertado
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertado en Zamba:" & newRes.ID)

                        If VariablesInterReglas.ContainsKey("GeneratedDocId") Then
                            VariablesInterReglas.Item("GeneratedDocId") = newRes.ID
                        Else
                            VariablesInterReglas.Add("GeneratedDocId", newRes.ID)
                        End If




                        If Params.Contains("ResultID") = False Then
                            Params.Add("ResultID", newRes.ID)
                            Params.Add("DocTypeId", newRes.DocTypeId)
                            Params.Add("DocName", newRes.Name)
                        End If
                        Params.Add("OpenTaskAfterInsert", Not Me.MyRule.DontOpenTaskAfterInsert)
                        Params.Add("OpenMode", MyRule.OpenMode)
                        If newRes.ID > 0 Then
                            If Me.MyRule.addCurrentwf = False Then
                                'Agrego el result al wf
                                Dim arrayNewRes As ArrayList = New ArrayList()
                                arrayNewRes.Add(newRes)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando a WF")
                                Dim wfID As Int64
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtengo el WFID")

                                If Not Me.r Is Nothing AndAlso r.WorkId > 0 Then
                                    wfID = r.WorkId
                                Else
                                    wfID = WFB.GetWorkflowIdByStepId(Me.MyRule.WFStepId)
                                End If



                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtengo la lista de ITaskResults insertando en WF")
                                Dim newITaskResultList As System.Collections.Generic.List(Of ITaskResult)

                                'marcos 
                                'ver si se le puede pasar el wfId del nuevo textbox de la regla generatetask results
                                If wfID = 0 Then

                                    newITaskResultList = WFTB.AddNewResultsToWorkFLow(arrayNewRes, Me.MyRule.SpecificWorkflowId, Not Me.MyRule.DontOpenTaskAfterInsert)

                                Else
                                    newITaskResultList = WFTB.AddNewResultsToWorkFLow(arrayNewRes, wfID, Not Me.MyRule.DontOpenTaskAfterInsert)

                                End If


                                If newITaskResultList Is Nothing Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La lista de Itaskresults es nothing")
                                    Throw New Exception("No se inserto la nueva tarea en WF")
                                ElseIf newITaskResultList.Count = 0 Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La lista de Itaskresults esta vacia")
                                    Throw New Exception("No se inserto la nueva tarea en WF")
                                Else
                                    arrayItaskResult.Add(newITaskResultList(0))
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregado a WF")
                                End If
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se adjunta al WF actual, debido a que esta activa la opcion de no adjuntar")

                                Dim task As ITaskResult = WFTB.GetTaskByDocIdAndWorkFlowId(newRes.ID, 0)
                                If Not IsNothing(task) Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se adquiere la tarea del WF: " & task.WorkId)
                                Else
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La tarea no se encuentra en ningun WF, se agrega una tarea vacia en memoria")
                                    task = New TaskResult()
                                    task.ID = newRes.ID
                                    task.Indexs = newRes.Indexs
                                    task.DocType = newRes.DocType
                                    task.DocTypeId = newRes.DocTypeId

                                End If
                                arrayItaskResult.Add(task)
                            End If

                            Dim path As String = Me._filepath

                            If String.Compare(IO.Path.GetExtension(path).ToLower().Trim(), ".msg") = 0 Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo es de extensión MSG.")
                                Try
                                    path = path.Substring(0, path.LastIndexOf(".")) & ".html"

                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia del archivo: " & path)
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

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº")
                        End If
                    Case InsertResult.ErrorIndicesIncompletos
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar por falta de atributos obligatorios")
                        Params.Add("Error", "No se pudo insertar por falta de atributos obligatorios")

                    Case InsertResult.ErrorIndicesInvalidos
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo insertar, hay indices con datos invalidos")
                        Params.Add("Error", "No se pudo insertar, hay indices con datos invalidos")
                End Select
        End Select
        VarInterReglas = Nothing
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Tarea generada con éxito!")
        Params.Add("msg", "Tarea generada con éxito!")
        ResultBusiness = Nothing
        WFB = Nothing

        Try

            If (Not arrayItaskResult Is Nothing AndAlso arrayItaskResult.Count = 1 AndAlso Not arrayItaskResult(0) Is Nothing) Then

                If VariablesInterReglas.ContainsKey("NuevaTarea.Id") = False Then
                    VariablesInterReglas.Add("NuevaTarea.Id", arrayItaskResult(0).ID)
                Else
                    VariablesInterReglas.Item("NuevaTarea.Id") = arrayItaskResult(0).ID
                End If
                If VariablesInterReglas.ContainsKey("NuevaTarea.TaskId") = False Then
                    VariablesInterReglas.Add("NuevaTarea.TaskId", arrayItaskResult(0).TaskId)
                Else
                    VariablesInterReglas.Item("NuevaTarea.TaskId") = arrayItaskResult(0).TaskId
                End If
                If VariablesInterReglas.ContainsKey("NuevaTarea.EntityId") = False Then
                    VariablesInterReglas.Add("NuevaTarea.EntityId", arrayItaskResult(0).DocTypeId)
                Else
                    VariablesInterReglas.Item("NuevaTarea.EntityId") = arrayItaskResult(0).DocTypeId
                End If

            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return arrayItaskResult
    End Function

    ''' <summary>
    ''' Crea la tabla a partir de los results
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <param name="indices"></param>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function procesarTablaWeb(ByRef Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        'Lo hago de la manera que estaba antes, para no romper la regla
        If Me.indices.Count = 1 Then
            'Creo cada result con un solo indice con valor
            Dim valores(Me.dt.Rows.Count - 1) As String
            Dim i As Int32 = 0
            For Each row As DataRow In Me.dt.Rows
                valores(i) = row(0).ToString()
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valores " & i & " " & valores(i))
                i += 1
            Next
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de valores: " & Me.dt.Rows.Count)
            Return Me.createWebResult(0, Params)
        Else
            'Creo los result a partir de la tabla
            Return Me.createWebResult(1, Params)
        End If
    End Function
End Class