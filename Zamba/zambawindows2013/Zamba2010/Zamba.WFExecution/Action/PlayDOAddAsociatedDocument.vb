Imports Zamba.Data

Public Class PlayDOAddAsociatedDocument
    Private result As Result
    Private hs As Hashtable
    Private dicSpecificAttributed As Dictionary(Of Long, String) = Nothing
    Private myRule As IDoAddAsociatedDocument

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim haveSpecificAttributes As Boolean = myRule.HaveSpecificAttributes
        Dim newIndex As IIndex
        Dim parentIndex As IIndex
        Dim specificAttributeValue As String

        For Each CurrentResult As ITaskResult In results
            result = New Result()
            hs = New Hashtable()

            Try
                '[Sebastian 15-05-09] se agrego permiso para crear el asociado al entidad
                If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.Create, myRule.AsociatedDocType.ID) = False Then
                    ZTrace.WriteLineIf(ZTrace.IsWarning, "No se pudo crear un entidad asociado porque no posee los permisos requeridos.")
                Else
                    result.DocType = DirectCast(myRule.AsociatedDocType, Core.DocType)
                    result.DocTypeId = Result.DocTypeId
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando la entidad " & result.DocType.Name)

                    'Esta Propiedad se utiliza en el indexer para determinar si los atributos se cargan como readonly o no
                    result.DocType.IsReindex = True
                    result.DocType.Indexs.Clear()
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo schema de Entidad: " & result.DocTypeId)
                    result.DocType.Indexs.AddRange(IndexsBusiness.GetIndexsSchemaAsListOfDT(result.DocTypeId, true))
                    result.Indexs = result.DocType.Indexs


                    'Verifica la existencia de valores de atributos configurados desde la regla
                    If haveSpecificAttributes Then
                        dicSpecificAttributed = GetHashFromSpecificAttributes(myRule.SpecificAttrubutes, CurrentResult)
                    End If

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Iterando por Atributos, Cantidad: " & result.Indexs.Count)

                    For I As Int32 = 0 To result.Indexs.Count - 1
                        newIndex = result.Indexs(I)

                        'Verifica si tiene atributos especificos para completar
                        If haveSpecificAttributes AndAlso dicSpecificAttributed.ContainsKey(newIndex.ID) Then
                            'Obtiene el valor del atributo configurado en la regla, pisando el valor del padre
                            specificAttributeValue = dicSpecificAttributed(newIndex.ID)
                            newIndex.Data = specificAttributeValue
                            newIndex.DataTemp = specificAttributeValue
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "no tiene atributos especificos se busca si la tarea padre tiene un atributo en común, Cantidad: " & CurrentResult.Indexs.Count)
                            'Si no tiene atributos especificos se busca si la tarea padre tiene un atributo en común
                            For IR As Int32 = 0 To CurrentResult.Indexs.Count - 1
                                parentIndex = CurrentResult.Indexs(IR)
                                If newIndex.ID = parentIndex.ID Then
                                    newIndex.Data = parentIndex.Data
                                    newIndex.DataTemp = parentIndex.Data
                                    newIndex.dataDescription = parentIndex.dataDescription
                                    newIndex.dataDescriptionTemp = parentIndex.dataDescriptionTemp
                                    Exit For
                                End If
                            Next
                        End If
                    Next

                    result.ID = CoreData.GetNewID(IdTypes.DOCID)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El documento se configuró con éxito")
                    hs.Add("DontOpenTaskIfIsAsociatedToWF", myRule.DontOpenTaskIfIsAsociatedToWF)
                    hs.Add("ParentTaskId", CurrentResult.TaskId)
                    hs.Add("KeepAsignedDocId", result.ID)

                    If myRule.SelectionId = IDoAddAsociatedDocument.Selection.Ninguno Then
                        If myRule.OpenDefaultScreen Then
                            hs.Add("OpenOption", myRule.DefaultScreenId)
                        End If
                        Result.HandleModule(ResultActions.DoAddAsoc, result, DirectCast(myRule.AsociatedDocType, Core.DocType), hs)
                    ElseIf myRule.SelectionId = IDoAddAsociatedDocument.Selection.Template Then
                        myRule.AsociatedDocType.TemplateId = myRule.TemplateId
                        Result.HandleModule(ResultActions.InsertarTemplate, result, DirectCast(myRule.AsociatedDocType, Core.DocType), hs)
                    ElseIf myRule.SelectionId = IDoAddAsociatedDocument.Selection.Documento Then
                        myRule.AsociatedDocType.typeid = myRule.Typeid
                        Result.HandleModule(ResultActions.InsertarDocWithFormat, result, DirectCast(myRule.AsociatedDocType, Core.DocType), hs)
                    End If

                    If (Not result Is Nothing) Then

                        If VariablesInterReglas.ContainsKey("NuevoDocumento.Id") = False Then
                            VariablesInterReglas.Add("NuevoDocumento.Id", result.ID, False)
                        Else
                            VariablesInterReglas.Item("NuevoDocumento.Id") = result.ID
                        End If

                        If VariablesInterReglas.ContainsKey("NuevoDocumento.EntityId") = False Then
                            VariablesInterReglas.Add("NuevoDocumento.EntityId", result.DocTypeId, False)
                        Else
                            VariablesInterReglas.Item("NuevoDocumento.EntityId") = result.DocTypeId
                        End If

                    End If

                End If
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsError, "Error al configurar el documento")
                Throw ex
            Finally
                If Not IsNothing(hs) Then
                    hs.Clear()
                    hs = Nothing
                End If
                If Not IsNothing(result) Then
                    result.Dispose()
                    result = Nothing
                End If
            End Try
        Next

        newIndex = Nothing
        parentIndex = Nothing

        Return results
    End Function

    ''' <summary>
    ''' Obtiene la configuracion de los atributos, retornandolos en un diccionario
    ''' </summary>
    ''' <param name="configuration"></param>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetHashFromSpecificAttributes(ByVal configuration As String, ByVal taskResult As ITaskResult) As Dictionary(Of Long, String)
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
                    'Se obtienen los valores de la regla
                    value = value.Split("|")(0)
                    value = TextoInteligente.ReconocerCodigo(value, taskResult)
                    value = varInterReglas.ReconocerVariablesValuesSoloTexto(value)

                    'Se remueve un espacio final que proviene de textointeligente/variables
                    value = Trim(value)
                    If value <> String.Empty Then hash.Add(Int64.Parse(id), value)
                End If

                strIndex = strIndex.Remove(0, strIndex.Split("§")(0).Length)
                If strIndex.Length > 0 Then
                    strIndex = strIndex.Remove(0, 1)
                End If
            End While
        End If

        Return hash
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoAddAsociatedDocument)
        myRule = rule
    End Sub
End Class
