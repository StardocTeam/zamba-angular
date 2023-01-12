'Imports Zamba.Barcode
Public Class PlayDOAutoComplete

    Private _myRule As IDOAutoComplete
    Private AC As AutocompleteBCBusiness
    Private docs As System.Collections.Generic.List(Of Core.ITaskResult)
    Private indexKeys As ArrayList
    Private indexsDataTemp As ArrayList

    Sub New(ByVal rule As IDOAutoComplete)
        Me._myRule = rule
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


        Me.AC = Nothing
        Me.docs = New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim UB As New UserBusiness

        Try
            For Each r As Core.TaskResult In results

                'Obtiene el campo IndexKey relacionado con AutoComplete del primer
                'documento. Es es porque deberían ser todos del mismo tipo.
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo atributos para autocompletar")
                ZTrace.WriteLineIf(ZTrace.IsInfo, " del documento: " & r.Name & " (Id: " & r.ID.ToString & ")")
                Me.indexKeys = AutoCompleteBarcode_FactoryBusiness.getIndexKeys(r.DocType.ID)

                'Si ocurre un error en este punto, es porque index es Nothing que quiere decir que el documento no tiene indices para autocompletado
                If Not (IsNothing(indexKeys)) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encontraron atributos para autocompletar.")
                    Me.indexsDataTemp = New ArrayList

                    'Obtiene una instancia del Objeto AutoComplete
                    AC = AutoCompleteBarcode_FactoryBusiness.GetComplete(Int32.Parse(r.DocType.ID), DirectCast(indexKeys(0), Index).ID)

                    If Not (IsNothing(AC)) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo los datos para autocompletar en los atributos...")
                        For Each ind As Index In indexKeys
                            ind.DataTemp = Me.findIn(r.Indexs, ind).Data
                            'indexsDataTemp.Add(ind.DataTemp)
                            indexsDataTemp.Add(ind)
                        Next

                        'Obtiene los datos del documento
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Autocompletando atributos...")
                        If Not (IsNothing(AC.Complete(r, indexsDataTemp, Nothing))) Then
                            'Agrega el documento a la coleccion
                            docs.Add(r)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando las modificaciones...")
                            Dim Results_Business As New Results_Business
                            Results_Business.SaveModifiedIndexData(DirectCast(r, Core.Result), True, False, Nothing, Nothing)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificaciones realizadas con éxito.")
                        End If

                        AC.Dispose()
                        AC = Nothing
                    End If
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontraron atributos para autocompletar.")
                End If
                UB.SaveAction(r.ID, Zamba.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, Me._myRule.Name)
            Next

            Return (docs)
        Finally
            UB = Nothing
            Me.AC = Nothing
            Me.docs = Nothing
            Me.indexKeys = Nothing
            Me.indexsDataTemp = Nothing
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Busca un objeto index dentro de un ArrayList
    ''' </summary>
    ''' <param name="Indexs">ArrayList donde se va a buscar</param>
    ''' <param name="pIndex">Indice a buscar</param>
    ''' <returns>Un index</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	07/06/2006	Creado
    '''     [Hernan] 11/10/2006 Modificado
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function findIn(ByVal Indexs As List(Of IIndex), ByVal pIndex As Core.Index) As Core.Index
        Dim i As Int16 = 0
        For i = 0 To Indexs.Count - 1
            If Indexs(i).ID = pIndex.ID Then
                pIndex.Data = Indexs(i).Data
                pIndex.DataTemp = Indexs(i).DataTemp
                Return pIndex
            End If
        Next
        Return Nothing
    End Function
End Class
