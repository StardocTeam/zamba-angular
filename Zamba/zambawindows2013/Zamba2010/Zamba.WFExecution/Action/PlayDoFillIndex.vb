Public Class PlayDoFillIndex
    Private _myRule As IDoFillIndex
    Private indexs As List(Of Int64)
    Private Parameters() As String
    Private NuevoValor As String
    Private indexID As Int64

    Sub New(ByVal rule As IDoFillIndex)
        _myRule = rule
    End Sub

    ''' <summary>
    ''' Ejecución de la regla DoFillIndex
    ''' </summary>
    ''' <param name="results" ></param>
    ''' <param name="myRule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	06/10/2008	Modified    
    ''' [Sebastian] 15-05-09 Modified Evitar que se modifique un indice cargado
    ''' </history>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            indexs = New List(Of Int64)()

            For Each taskResult As Core.TaskResult In results

                For I As Int32 = 0 To _myRule.IndexId.Split("|").Length - 1
                    indexID = CType(_myRule.IndexId.Split("|")(I), System.Int64)

                    NuevoValor = Nothing
                    If indexID > 0 Then
                        If IsNothing(_myRule.OverWriteIndex.Split("|")(I)) OrElse
                                        _myRule.OverWriteIndex.Split("|")(I).Equals(String.Empty) Then

                        Else
                            For x As Int64 = 0 To taskResult.Indexs.Count - 1
                                If indexID = taskResult.Indexs(x).ID Then
                                    indexs.Add(indexID)
                                    ReplaceIndexData(taskResult.Indexs(x), _myRule.PrimaryValue.Split("|")(I), _myRule.SecondaryValue.Split("|")(I), CBool(_myRule.OverWriteIndex.Split("|")(I)), taskResult)
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                Next



                If indexs.Count > 0 Then
                    Dim rstBuss As New Results_Business()
                    rstBuss.SaveModifiedIndexData(DirectCast(taskResult, Core.Result), True, False, indexs)
                    rstBuss = Nothing

                    UserBusiness.Rights.SaveAction(taskResult.ID, ObjectTypes.WFTask, RightsType.ExecuteRule, _myRule.Name)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha modificado ningun atributo")
                End If
            Next
        Finally
            Parameters = Nothing
            If Not IsNothing(indexs) Then
                indexs.Clear()
                indexs = Nothing
            End If
            NuevoValor = Nothing
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub ReplaceIndexData(ByRef Indice As Zamba.Core.Index, ByVal PrincipalData As String, ByVal SecondaryData As String, ByVal OverwriteIndex As Boolean, ByRef TaskResult As ITaskResult)

        ' Si el valor principal no está vacío
        If Not (String.IsNullOrEmpty(PrincipalData)) Then
            NuevoValor = TextoInteligente.ReconocerCodigo(PrincipalData, TaskResult)
        ElseIf Not (String.IsNullOrEmpty(SecondaryData)) Then
            NuevoValor = TextoInteligente.ReconocerCodigo(SecondaryData, TaskResult)
        End If

        If Not (String.IsNullOrEmpty(NuevoValor)) Then
            NuevoValor = WFRuleParent.ReconocerVariablesValuesSoloTexto(NuevoValor)
            WFRulesBusiness.ReconocerRuleValueFunctions(NuevoValor)
        Else
            NuevoValor = String.Empty
        End If

        If OverwriteIndex = False Then
            Indice.Data = NuevoValor
            Indice.DataTemp = NuevoValor
            If Indice.DropDown = IndexAdditionalType.AutoSustitución OrElse Indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                Indice.dataDescription = AutoSubstitutionBusiness.getDescription(Indice.Data, Indice.ID, False, Indice.Type, False)
                Indice.dataDescriptionTemp = Indice.dataDescription
                If Indice.dataDescription.Length = 0 Then
                    Dim codigo As String = AutoSubstitutionBusiness.getCodigo(NuevoValor, Indice.ID, False, Indice.Type, False)
                    If codigo.Length > 0 Then
                        Indice.Data = codigo
                        Indice.DataTemp = codigo
                        Indice.dataDescription = NuevoValor
                        Indice.dataDescriptionTemp = NuevoValor
                    End If
                End If
            End If
        End If
    End Sub
End Class