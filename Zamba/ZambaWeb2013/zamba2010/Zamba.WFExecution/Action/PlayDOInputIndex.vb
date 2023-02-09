
''' <summary>
''' Pregunta por un indice y guardar el valor ingresado
''' </summary>
''' <history>Marcelo Modified 02/12/08</history>
''' <remarks></remarks>
Public Class PlayDOInputIndex
    'Private m_Name As String

    Private _myRule As IDOInputIndex
    Private lstResults As System.Collections.Generic.List(Of Core.ITaskResult)
    Private Valor As String
    Private lstIndexsIds As List(Of Int64)
    'Private formIndex As FrmInputText

    Sub New(ByVal rule As IDOInputIndex)
        Me._myRule = rule
    End Sub

    ''' <summary>
    ''' Pregunta por un indice y guarda el valor en la tabla
    ''' </summary>
    ''' <param name="results">Resulta a ejecutar</param>
    ''' <param name="myrule">Parametros de la regla</param>
    ''' <history>Marcelo Modified 02/12/08</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Me.lstResults = New System.Collections.Generic.List(Of Core.ITaskResult)
        Me.Valor = ""
        Me.lstIndexsIds = New List(Of Int64)(1)

        Try
            If validarIndice(results) Then
                'Comente esta linea porque no se utilizaba el nombre obtenido
                'm_Name = Core.IndexsBusiness.GetIndexName(myrule.Index)

                'Pregunto por el valor del indice
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Mostrando el formulario.")
                'Me.formIndex = New FrmInputText("Ingresar " & IndexsBusiness.GetIndexNameById(_myRule.Index))
                'formIndex.ShowDialog()
                'If formIndex.DialogResult =DialogResult.OK Then
                '    Valor = formIndex.txtCustomItem.Text
                '    formIndex.Close()
                '    ZTrace.WriteLineIf(ZTrace.IsInfo, "El valor obtenido es: " & Valor)

                '    'Si hay valor
                '    'If Valor.Length > 0 Then
                '    Dim Results_Business As New Results_Business

                '    For Each r As Core.TaskResult In results
                '        Dim dtModifiedIndex As New DataTable
                '        dtModifiedIndex.Columns.Add("ID", GetType(Int64))
                '        dtModifiedIndex.Columns.Add("OldValue", GetType(String))
                '        dtModifiedIndex.Columns.Add("NewValue", GetType(String))
                '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)

                '        For Each i As Core.Index In r.Indexs
                '            If i.ID = Me._myRule.Index Then
                '                Dim row As DataRow = dtModifiedIndex.NewRow()
                '                row("ID") = i.ID
                '                row("OldValue") = i.Data
                '                row("NewValue") = Valor
                '                dtModifiedIndex.Rows.Add(row)

                '                lstIndexsIds.Add(i.ID)
                '                i.Data = Valor
                '                i.DataTemp = Valor

                '                Results_Business.SaveModifiedIndexData(DirectCast(r, Zamba.Core.Result), True, False, lstIndexsIds, dtModifiedIndex)
                '                lstResults.Add(r)
                '                Exit For
                '            End If
                '        Next
                '        UserBusiness.SaveAction(r.ID, Zamba.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, Me._myRule.MaskName)
                '    Next
                '    'Else
                '    '    Throw New Exception("No ha ingresado ningun valor")
                '    'End If

                '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificaciones realizadas con éxito.")
                'Else
                '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha cancelado la ejecucion.")

                '    If Me._myRule.ThrowExceptionIfCancel Then
                '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                '        Throw New Exception("El usuario cancelo la ejecucion de la regla")
                '    Else
                '        Return Nothing
                '    End If
                'End If
            End If
        Finally
            lstIndexsIds = Nothing

            Me.Valor = Nothing
            'Me.formIndex = Nothing
        End Try

        Return lstResults
    End Function
    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        If validarIndice(results) Then
            'Pregunto por el valor del indice
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Mostrando el formulario.")
            Params.Add("IndexName", New IndexsBusiness().GetIndexNameById(_myRule.Index))
            Params.Add("IndexId", _myRule.Index)
        End If

        Return results
    End Function

    Public Function PlayWebSecondExecution(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Me.lstResults = New System.Collections.Generic.List(Of Core.ITaskResult)
        Me.Valor = ""
        Me.lstIndexsIds = New List(Of Int64)(1)

        Try
            Valor = Params("Valor")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El valor obtenido es: " & Valor)

            'Si hay valor
            If Valor.Length > 0 Then
                For Each r As Core.TaskResult In results
                    Dim dtModifiedIndex As New DataTable
                    dtModifiedIndex.Columns.Add("ID", GetType(Int64))
                    dtModifiedIndex.Columns.Add("OldValue", GetType(String))
                    dtModifiedIndex.Columns.Add("NewValue", GetType(String))
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                    For Each i As Core.Index In r.Indexs
                        If i.ID = Me._myRule.Index Then
                            Dim row As DataRow = dtModifiedIndex.NewRow()
                            row("ID") = i.ID
                            row("OldValue") = i.Data
                            row("NewValue") = Valor
                            dtModifiedIndex.Rows.Add(row)

                            lstIndexsIds.Add(i.ID)
                            i.Data = Valor
                            i.DataTemp = Valor
                            Dim Results_Business As New Results_Business()
                            Results_Business.SaveModifiedIndexData(DirectCast(r, Zamba.Core.Result), True, False, lstIndexsIds, dtModifiedIndex)
                            lstResults.Add(r)
                            Exit For
                        End If
                    Next
                    Dim RightsBusiness As New RightsBusiness()
                    RightsBusiness.SaveAction(r.ID, Zamba.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, Me._myRule.Name)
                Next
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificaciones realizadas con éxito.")
            Return lstResults
        Finally
            lstIndexsIds = Nothing

            Me.Valor = Nothing
            'Me.formIndex = Nothing
            Params.Clear()
        End Try
    End Function
    'Esta funcion devuelve un valor true si el indice a modificar existe en el entidad
    Private Function validarIndice(ByRef Results As System.Collections.Generic.List(Of Core.ITaskResult)) As Boolean
        For Each r As Core.TaskResult In Results
            For Each i As Core.Index In r.Indexs
                If i.ID = Me._myRule.Index Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function
End Class
