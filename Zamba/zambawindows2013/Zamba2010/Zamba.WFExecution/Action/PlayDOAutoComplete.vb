'Imports Zamba.Barcode
Public Class PlayDOAutoComplete

    Private _myRule As IDOAutoComplete
    Private AC As AutocompleteBCBusiness
    Private docs As System.Collections.Generic.List(Of Core.ITaskResult)
    Private indexKeys As ArrayList
    Private indexsDataTemp As ArrayList

    Sub New(ByVal rule As IDOAutoComplete)
        _myRule = rule
    End Sub


    ''' <summary>
    ''' Play de la regla autocomplete
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myRule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    05/11/2008  Modified    Autocompletar con más claves (además de una que era como estaba originalmente)
    ''' </history>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)


        AC = Nothing
        docs = New System.Collections.Generic.List(Of Core.ITaskResult)

        Try
            For Each r As Core.TaskResult In results

                'Obtiene el campo IndexKey relacionado con AutoComplete del primer
                'documento. Es es porque deberían ser todos del mismo tipo.
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo atributos para autocompletar del documento: " & r.Name & " (Id: " & r.ID.ToString & ")")
                indexKeys = AutoCompleteBarcode_FactoryBusiness.getIndexKeys(r.DocType.ID)

                'Si ocurre un error en este punto, es porque index es Nothing que quiere decir que el documento no tiene atributos para autocompletado
                If Not (IsNothing(indexKeys)) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encontraron atributos para autocompletar.")
                    indexsDataTemp = New ArrayList

                    'Obtiene una instancia del Objeto AutoComplete
                    AC = AutoCompleteBarcode_FactoryBusiness.GetComplete(Int32.Parse(r.DocType.ID), DirectCast(indexKeys(0), Index).ID)

                    If Not (IsNothing(AC)) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo los datos para autocompletar en los atributos...")
                        For Each ind As Index In indexKeys
                            ind.DataTemp = AutocompleteBCBusiness.findIn(r.Indexs, ind).Data
                            'indexsDataTemp.Add(ind.DataTemp)
                            indexsDataTemp.Add(ind)
                        Next

                        'Obtiene los datos del documento
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Autocompletando atributos...")
                        If Not (IsNothing(AC.Complete(r, indexsDataTemp, Nothing, True))) Then
                            'Agrega el documento a la coleccion
                            docs.Add(r)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando las modificaciones...")
                            Dim rstBuss As New Results_Business()
                            rstBuss.SaveModifiedIndexData(DirectCast(r, Core.Result), True, False)
                            rstBuss = Nothing
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificaciones realizadas con éxito.")
                        End If

                        AC.Dispose()
                        AC = Nothing
                    End If
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontraron atributos para autocompletar.")
                End If
                UserBusiness.Rights.SaveAction(r.ID, ObjectTypes.WFTask, RightsType.ExecuteRule, _myRule.Name)
            Next

            Return (docs)
        Finally

            AC = Nothing
            docs = Nothing
            indexKeys = Nothing
            indexsDataTemp = Nothing
        End Try
    End Function

  
    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
