Imports System.Windows.Forms

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.WFExecution
''' Class	 : PlayDoFillIndexComplete
''' -----------------------------------------------------------------------------
''' <summary>
''' Play de la regla DoFillIndexComplete
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
'''     [Gaston]	15/07/2008	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class PlayDoFillIndexComplete

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IDoFillIndexComplete) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim UB As New UserBusiness
        Try

            Dim _index As Core.Index = myrule.Index
            Dim value As String = String.Empty
            Dim Results_Business As New Results_Business

            For Each taskResult As Core.TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & taskResult.Name)
                For Each I As Index In taskResult.Indexs
                    I.DataTemp = I.Data
                Next

                Dim Indice As Core.Index = taskResult.GetIndexById(_index.ID)

                If Not (IsNothing(Indice)) Then

                    '================================================================
                    ' Primero, se ejecuta lo que contenía la regla PlayDoFillIndex
                    '================================================================
                    If (String.Compare(myrule.TEXTODEFAULT, "VALOR POR DEFECTO") = 0) Then

                        Dim NuevoValor As String
                        NuevoValor = Zamba.Core.TextoInteligente.ReconocerCodigo(_index.Data, taskResult)
                        Dim VarInterReglas As New VariablesInterReglas()
                        NuevoValor = VarInterReglas.ReconocerVariablesValuesSoloTexto(NuevoValor)
                        VarInterReglas = Nothing
                        Indice.Data = NuevoValor
                        Indice.DataTemp = NuevoValor
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Completando atributos...")
                        Results_Business.SaveModifiedIndexData(DirectCast(taskResult, Core.Result), True, False, Nothing, Nothing)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Atributos completados con éxito!")
                    Else

                        '=======================================================================
                        ' Segundo, se ejecuta lo que contenía la regla PlayDoFillIndexDefault
                        '=======================================================================
                        If ((String.Compare(myrule.TEXTODEFAULT, "FECHA Y HORA ACTUAL") = 0) Or _
                            (String.Compare(myrule.TEXTODEFAULT, "USUARIO ACTUAL") = 0) Or _
                            (String.Compare(myrule.TEXTODEFAULT, "USUARIO WINDOWS") = 0) Or _
                            (String.Compare(myrule.TEXTODEFAULT, "NOMBRE DE PC") = 0)) Then

                            If String.Compare(myrule.TEXTODEFAULT, "FECHA Y HORA ACTUAL") = 0 Then
                                value = Now
                            ElseIf String.Compare(myrule.TEXTODEFAULT, "USUARIO ACTUAL") = 0 Then
                                value = Zamba.Core.Users.User.GetUserName
                            ElseIf String.Compare(myrule.TEXTODEFAULT, "USUARIO WINDOWS") = 0 Then
                                value = Environment.UserName
                            ElseIf String.Compare(myrule.TEXTODEFAULT, "NOMBRE DE PC") = 0 Then
                                value = Environment.MachineName
                            End If

                            _index.Data = value

                            Indice.Data = _index.Data
                            Indice.DataTemp = _index.Data
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Completando atributos...")
                            Results_Business.SaveModifiedIndexData(DirectCast(taskResult, Result), True, False, Nothing, Nothing)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Atributos completados con éxito!")

                        End If

                    End If

                End If
                UB.SaveAction(taskResult.ID, Zamba.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, myrule.Name)
            Next

            '=================================================================
            ' Tercero, se ejecuta lo que contenía la regla PlayDoInputIndex
            '=================================================================
            If (String.Compare(myrule.TEXTODEFAULT, "PREGUNTAR POR VALOR") = 0) Then

                Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
                Dim Valor As String = ""

                If (validarIndice(results, myrule.Index.ID)) Then
                    'Dim formIndex As FrmInputText = New FrmInputText()
                    'formIndex.ShowDialog()
                    'Valor = formIndex.txtCustomItem.Text 'InputBox("Ingrese un valor para el indice " & m_Name, "Modificar Indice " & m_Name)
                    'formIndex.Close()

                    If (Valor.Length = 0) Then
                        MessageBox.Show("No se modifico el indice", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Completando atributos...")

                        For Each r As Core.TaskResult In results
                            For Each i As Core.Index In r.Indexs

                                If (i.ID = myrule.Index.ID) Then
                                    i.Data = Valor
                                    i.DataTemp = Valor
                                    Results_Business.SaveModifiedIndexData(DirectCast(r, Zamba.Core.Result), True, False, Nothing, Nothing)
                                    NewList.Add(r)
                                    Exit For
                                End If
                            Next
                        Next
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Atributos completados con éxito!")

                    End If
                End If

                Return NewList

            End If
        Finally

        End Try

        Return results

    End Function

    'Esta funcion devuelve un valor true si el indice a modificar existe en el entidad
    Private Function validarIndice(ByRef Results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal indexId As Int32) As Boolean
        For Each r As Core.TaskResult In Results
            For Each i As Core.Index In r.Indexs
                If i.ID = indexId Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function

End Class