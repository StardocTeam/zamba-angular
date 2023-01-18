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
    Private myRule As IDoCompare
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim NewSortedList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Trace.WriteLineIf(ZTrace.IsInfo, "Cantidad de results a ejecutar: " & results.Count)
            Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo valores a comparar...")
            Dim value As Object = WFRuleParent.ReconocerVariablesAsObject(myRule.valueList)

            If Not IsNothing(value) Then
                Dim newList As List(Of Object) = New List(Of Object)
                Trace.WriteLineIf(ZTrace.IsInfo, "Valor de la variable obtenida: " & value.ToString())
                'Si no hay tareas, la ejecuto una sola vez, sino, una vez por cada tarea
                If results.Count = 0 Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "Comparando...")
                    newList = compareValue(value, myRule)
                Else
                    For Each r As TaskResult In results
                        Trace.WriteLineIf(ZTrace.IsInfo, "Utilizar documentos asociados...")
                        If myRule.UseAsocDoc = False Then
                            Trace.WriteLineIf(ZTrace.IsInfo, " Falso.")
                            Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo valores para " & r.Name & ", Id " & r.ID)
                            If TypeOf value Is String Then
                                value = Zamba.Core.TextoInteligente.ReconocerCodigoAsObject(myRule.valueList, r)
                            End If
                        Else
                            Trace.WriteLineIf(ZTrace.IsInfo, " Verdadero.")
                            Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo documentos asociados...")
                            Dim Asociated As ArrayList = DocAsociatedBusiness.getAsociatedResultsFromResult(r, Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)))

                            Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo valores para " & r.Name & ", Id " & r.ID)
                            Trace.WriteLineIf(ZTrace.IsInfo, "Cantidad de documentos asociados encontrados: " & Asociated.Count)
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
                            Trace.WriteLineIf(ZTrace.IsInfo, "Valor de la variable obtenida: " & value.ToString())
                            newList = compareValue(value, myRule)
                        Else
                            Trace.WriteLineIf(ZTrace.IsInfo, "Los valores a comparar se encuentran nulos. La comparación no será realizada.")
                        End If
                        NewSortedList.Add(r)
                    Next
                End If

                Trace.WriteLineIf(ZTrace.IsInfo, "Cantidad de valores a guardar: " & newList.Count)
                Trace.WriteLineIf(ZTrace.IsInfo, "Guardando valores...")
                If String.IsNullOrEmpty(myRule.variableName) = False Then
                    If VariablesInterReglas.ContainsKey(myRule.variableName) = False Then
                        VariablesInterReglas.Add(myRule.variableName, newList, False)
                    Else
                        VariablesInterReglas.Item(myRule.variableName) = newList
                    End If
                End If
                Trace.WriteLineIf(ZTrace.IsInfo, "El proceso de comparación se ha ejecutado con éxito.")
            Else
                Trace.WriteLineIf(ZTrace.IsInfo, "La variable se encuentra vacía.")
            End If
        Finally

        End Try

        Return NewSortedList
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

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
                    Trace.WriteLineIf(ZTrace.IsInfo, "Comparación válida")
                    newList.Add(item)
                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "Comparación inválida")
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
        Trace.WriteLineIf(ZTrace.IsInfo, "Comparacion: " & value.ToString() & " " & comp & " " & valueFilter)
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

    Public Sub New(ByVal rule As IDoCompare)
        Me.myRule = rule
    End Sub
End Class