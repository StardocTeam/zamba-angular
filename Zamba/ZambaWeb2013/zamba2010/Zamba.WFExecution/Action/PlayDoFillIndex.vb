Imports System.Text

Public Class PlayDoFillIndex

    Private _myRule As IDoFillIndex
    Private _index As Core.Index
    Private indexs As List(Of Int64)
    Private Parameters() As String
    Private Indice As Core.Index
    Private NuevoValor As String

    Sub New(ByVal rule As IDoFillIndex)
        Me._myRule = rule
    End Sub

    ''' <summary>
    ''' Ejecución de la regla DoFillIndex
    ''' </summary>
    ''' <param name="results" ></param>
    ''' <param name="myRule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''         [Gaston] 06/10/2008         Modified    
    ''' [Sebastian] 15-05-09 Modified Evitar que se modifique un indice cargado
    ''' </history>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim UB As New UserBusiness

        Dim id As Integer = Me._myRule.ID

        Try
            'Me._index = Me._myRule.Index
            Me.indexs = New List(Of Int64)(1)
            Me.indexs.Clear()
            Dim dtModifiedIndex As New DataTable
            dtModifiedIndex.Columns.Add("ID", GetType(Int64))
            dtModifiedIndex.Columns.Add("OldValue", GetType(String))
            dtModifiedIndex.Columns.Add("NewValue", GetType(String))
            'Me.indexs.Add(Me._index.ID)

            For Each taskResult As Core.TaskResult In results
                dtModifiedIndex.Rows.Clear()
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & taskResult.Name)

                Dim sbIndexHistory As New StringBuilder()
                sbIndexHistory.Append("Modificaciones realizadas en '" & taskResult.Name & "': ")

                For Each I As Index In taskResult.Indexs
                    I.DataTemp = I.Data
                Next

                Dim oldDataValue As String = String.Empty
                Dim oldDescriptionValue As String = String.Empty
                Dim changes As Boolean = False


                For I As Int32 = 0 To Me._myRule.IndexId.Split("|").Length - 1
                    Indice = taskResult.GetIndexById(CType(Me._myRule.IndexId.Split("|")(I), System.Int32))

                    NuevoValor = Nothing

                    If Not IsNothing(Indice) Then
                        If IsNothing(Me._myRule.OverWriteIndex.Split("|")(I)) OrElse
                                            Me._myRule.OverWriteIndex.Split("|")(I).Equals(String.Empty) Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Alguno de los parametros esta vacio")
                        Else
                            Me.indexs.Add(Indice.ID)

                            oldDataValue = Indice.Data
                            oldDescriptionValue = Indice.dataDescription


                            Dim PrimaryValue As String = Me._myRule.PrimaryValue.Split("|")(I)
                            Dim SecondaryValue As String = Me._myRule.SecondaryValue.Split("|")(I)
                            Dim BoolValue As String = CBool(Me._myRule.OverWriteIndex.Split("|")(I))
                            ReplaceIndexData(Indice, PrimaryValue, SecondaryValue, BoolValue, taskResult, dtModifiedIndex)

                            If (oldDataValue Is Nothing) Then
                                oldDataValue = String.Empty
                            End If

                            If (taskResult.ID <> 0) Then
                                If (oldDataValue IsNot Nothing AndAlso String.CompareOrdinal(oldDataValue.Trim(), Indice.Data.Trim()) <> 0) Then
                                    changes = True
                                    '//Si existen cambios se guardan para el historial
                                    If (String.IsNullOrEmpty(oldDescriptionValue)) Then
                                        sbIndexHistory.Append("índice '" & Indice.Name & "' de '" & oldDataValue.Trim() & "' a '" & Indice.Data.Trim() & "', ")
                                    Else
                                        sbIndexHistory.Append("índice '" & Indice.Name & "' de '" & oldDescriptionValue.Trim() & "' a '" & Indice.dataDescription.Trim() & "', ")
                                    End If
                                End If
                            End If

                        End If
                    End If
                Next

                If indexs.Count > 0 Then
                    Dim rstBuss As New Results_Business()
                    rstBuss.SaveModifiedIndexData(DirectCast(taskResult, Core.Result), True, False, Me.indexs, dtModifiedIndex)
                    rstBuss = Nothing
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha modificado ningun atributo")
                End If

                '                //Guarda el historial
                sbIndexHistory = sbIndexHistory.Remove(sbIndexHistory.Length - 2, 2)
                If (changes) Then
                    UB.SaveAction(taskResult.ID, ObjectTypes.Documents, RightsType.ReIndex, sbIndexHistory.ToString(), 0)
                End If

                UB.SaveAction(taskResult.ID, Zamba.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, Me._myRule.Name)
            Next
        Finally
            UB = Nothing
            Me._index = Nothing
            Me.indexs = Nothing
            Indice = Nothing
            NuevoValor = Nothing
        End Try

        Return (results)

    End Function

    Public Sub ReplaceIndexData(ByRef Indice As Zamba.Core.Index, ByVal PrincipalData As String, ByVal SecondaryData As String, ByVal OverwriteIndex As Boolean, ByRef TaskResult As ITaskResult, ByVal dtModifiedIndex As DataTable)

        ' Si el valor principal no está vacío
        If Not (String.IsNullOrEmpty(PrincipalData)) Then
            NuevoValor = WFRulesBusiness.ResolveText(PrincipalData, TaskResult)
        ElseIf Not (String.IsNullOrEmpty(SecondaryData)) Then
            NuevoValor = WFRulesBusiness.ResolveText(SecondaryData, TaskResult)
        End If

        If Not String.IsNullOrEmpty(NuevoValor) Then
            Dim WFRB As New WFRulesBusiness
            WFRB.ReconocerRuleValueFunctions(NuevoValor)
            WFRB = Nothing
        Else
            NuevoValor = String.Empty
        End If

        If OverwriteIndex = False Then
            Dim row As DataRow = dtModifiedIndex.NewRow()
            row("ID") = Indice.ID
            row("OldValue") = Indice.Data
            row("NewValue") = NuevoValor
            dtModifiedIndex.Rows.Add(row)

            Indice.Data = NuevoValor
            Indice.DataTemp = NuevoValor
            If Indice.DropDown = IndexAdditionalType.AutoSustitución OrElse Indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                Dim ASB As New AutoSubstitutionBusiness
                Indice.dataDescription = ASB.getDescription(Indice.Data, Indice.ID)
                Indice.dataDescriptionTemp = ASB.getDescription(Indice.Data, Indice.ID)
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Completando atributos...")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Atributos completados con éxito!")
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El índice tiene un valor y no será sobreescrito.")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor: " & Indice.Data)
        End If
    End Sub

End Class
