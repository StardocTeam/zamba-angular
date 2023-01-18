Imports Zamba.Core.DocTypes.DocAsociated
Imports Zamba.Core

''' <summary>
''' Esta regla compara un listado por un atributo de cada elemento y guarda en una variable los elementos que cumplan con ese valor
''' </summary>
''' <history>
''' Marcelo Created 14/09/2009
''' </history>
''' <remarks></remarks>
Public Class PlayDoCompare


    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDoCompare) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim UP As New UserPreferences
        Dim NewSortedList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de results a ejecutar: " & results.Count)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo valores a comparar...")
            Dim VarInterReglas As New VariablesInterReglas()
            Dim value As Object = VarInterReglas.ReconocerVariablesAsObject(myRule.valueList)
            VarInterReglas = Nothing

            If Not IsNothing(value) Then
                Dim newList As List(Of Object) = New List(Of Object)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor de la variable obtenida: " & value.ToString())
                'Si no hay tareas, la ejecuto una sola vez, sino, una vez por cada tarea
                If results.Count = 0 Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparando...")
                    newList = compareValue(value, myRule)
                Else
                    For Each r As TaskResult In results
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Utilizar documentos asociados...")
                        If myRule.UseAsocDoc = False Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, " Falso.")
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo valores para " & r.Name & ", Id " & r.ID)
                            If TypeOf value Is String Then
                                value = Zamba.Core.TextoInteligente.ReconocerCodigoAsObject(myRule.valueList, r)
                            End If
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, " Verdadero.")
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo documentos asociados...")
                            Dim DAB As New DocTypes.DocAsociated.DocAsociatedBusiness
                            Dim Asociated As ArrayList = DAB.getAsociatedResultsFromResult(r, Int32.Parse(UP.getValue("CantidadFilas", UPSections.UserPreferences, 100, Zamba.Membership.MembershipHelper.CurrentUser.ID)), Membership.MembershipHelper.CurrentUser.ID)
                            DAB = Nothing
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo valores para " & r.Name & ", Id " & r.ID)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de documentos asociados encontrados: " & Asociated.Count)
                            If Asociated.Count > 0 Then
                                If myRule.idDocTypeAsoc = 0 Then
                                    If TypeOf value Is String Then
                                        value = Zamba.Core.TextoInteligente.ReconocerCodigoAsObject(myRule.valueList, Asociated(0))
                                    End If
                                Else
                                    For Each asoc As Result In Asociated
                                        If asoc.DocTypeId = myRule.idDocTypeAsoc Then
                                            value = Zamba.Core.TextoInteligente.ReconocerCodigoAsObject(myRule.valueList, asoc)
                                            Exit For
                                        End If
                                    Next
                                End If
                            Else
                                value = Nothing
                            End If
                        End If

                        If Not IsNothing(value) Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor de la variable obtenida: " & value.ToString())
                            newList = compareValue(value, myRule)
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Los valores a comparar se encuentran nulos. La comparación no será realizada.")
                        End If
                        NewSortedList.Add(r)
                    Next
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de valores a guardar: " & newList.Count)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando valores...")
                If String.IsNullOrEmpty(myRule.variableName) = False Then
                    If VariablesInterReglas.ContainsKey(myRule.variableName) = False Then
                        VariablesInterReglas.Add(myRule.variableName, newList)
                    Else
                        VariablesInterReglas.Item(myRule.variableName) = newList
                    End If
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El proceso de comparación se ha ejecutado con éxito.")
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "La variable se encuentra vacía.")
            End If
        Finally
            UP = Nothing
        End Try

        Return NewSortedList
    End Function

    ''' <summary>
    ''' Compara el valor y devuelve los items que cumplan la condicion
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function compareValue(ByVal value As Object, ByVal myrule As IDoCompare) As List(Of Object)
        Dim newList As New List(Of Object)()
        If Not IsNothing(value) Then
            For Each item As Object In value
                Dim valor As Object = item.GetType().GetProperty(myrule.valueComp)
                valor = valor.GetValue(item, Nothing)

                If compare(valor, myrule.Comp, myrule.valueFilter) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparación válida")
                    newList.Add(item)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparación inválida")
                End If
            Next
        End If

        Return newList
    End Function

    ''' <summary>
    ''' Compara los valores y devuelve true si se cumplen
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="comp"></param>
    ''' <param name="myrule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function compare(ByVal value As String, ByVal comp As String, ByVal valueFilter As String)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparacion: " & value.ToString() & " " & comp & " " & valueFilter)
        Select Case comp.ToLower()
            Case "igual"
                If String.Compare(value, valueFilter) = 0 Then
                    Return True
                Else
                    Return False
                End If
            Case "distinto"
                If String.Compare(value, valueFilter) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case "mayor"
                If IsNumeric(value) AndAlso IsNumeric(valueFilter) Then
                    Return Double.Parse(value) > Double.Parse(valueFilter)
                Else
                    Return value > valueFilter
                End If
            Case "mayor igual"
                If IsNumeric(value) AndAlso IsNumeric(valueFilter) Then
                    Return Double.Parse(value) >= Double.Parse(valueFilter)
                Else
                    Return value >= valueFilter
                End If
            Case "menor"
                If IsNumeric(value) AndAlso IsNumeric(valueFilter) Then
                    Return Double.Parse(value) < Double.Parse(valueFilter)
                Else
                    Return value < valueFilter
                End If
            Case "menor igual"
                If IsNumeric(value) AndAlso IsNumeric(valueFilter) Then
                    Return Double.Parse(value) <= Double.Parse(valueFilter)
                Else
                    Return value <= valueFilter
                End If
            Case "contiene"
                Return value.Contains(valueFilter)
            Case "empieza"
                Return value.StartsWith(valueFilter)
            Case "termina"
                Return Not value.EndsWith(valueFilter)
            Case Else
                Return False
        End Select
    End Function
End Class