
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
    Private formIndex As FrmInputText

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
        Me.Valor = String.Empty
        Me.lstIndexsIds = New List(Of Int64)(1)

        Try
            If validarIndice(results) Then
                'Comente esta linea porque no se utilizaba el nombre obtenido
                'm_Name = Core.IndexsBusiness.GetIndexName(myrule.Index)

                'Pregunto por el valor del indice

                Me.formIndex = New FrmInputText("Ingresar " & IndexsBusiness.GetIndexNameById(_myRule.Index))
                formIndex.ShowDialog()
                If formIndex.DialogResult = Windows.Forms.DialogResult.OK Then
                    Valor = formIndex.txtCustomItem.Text
                    formIndex.Close()


                    'Si hay valor
                    'If Valor.Length > 0 Then
                    For Each r As Core.TaskResult In results
                        For Each i As Core.Index In r.Indexs
                            If i.ID = Me._myRule.Index Then
                                lstIndexsIds.Add(i.ID)
                                i.Data = Valor
                                i.DataTemp = Valor
                                Dim rstBuss As New Results_Business()
                                rstBuss.SaveModifiedIndexData(DirectCast(r, Zamba.Core.Result), True, False, lstIndexsIds)
                                rstBuss = Nothing
                                lstResults.Add(r)
                                Exit For
                            End If
                        Next
                        UserBusiness.Rights.SaveAction(r.ID, Zamba.Core.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, Me._myRule.Name)
                    Next
                    'Else
                    '    Throw New Exception("No ha ingresado ningun valor")
                    'End If

                Else

                    If Me._myRule.ThrowExceptionIfCancel Then
                        Trace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                        Throw New Exception("El usuario cancelo la ejecucion de la regla")
                    Else
                        Return Nothing
                    End If
                End If
            End If
        Finally
            If lstIndexsIds IsNot Nothing Then
                lstIndexsIds.Clear()
                lstIndexsIds = Nothing
            End If
            lstIndexsIds = Nothing
            Me.Valor = Nothing
            If formIndex IsNot Nothing Then
                formIndex.Dispose()
                Me.formIndex = Nothing
            End If
        End Try

        Return lstResults
    End Function
    'Esta funcion devuelve un valor true si el atributo a modificar existe en la entidad
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

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
