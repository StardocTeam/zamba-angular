Imports zamba.Core
Public Class PlayIfDocumentType

    Private _myRule As IIfDocumentType

    Sub New(ByVal rule As IIfDocumentType)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFRB As New WFRulesBusiness
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
        If (Me._myRule.ChildRulesIds Is Nothing OrElse Me._myRule.ChildRulesIds.Count = 0) Then
            Me._myRule.ChildRulesIds = WFRB.GetChildRulesIds(Me._myRule.ID)
        End If

        If Me._myRule.ChildRulesIds.Count = 2 Then
            For Each childruleId As Int64 In Me._myRule.ChildRulesIds
                Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                R.ParentRule = _myRule
                R.IsAsync = _myRule.IsAsync
                If R.GetType().ToString().Contains("IfBranch") Then
                    Return results
                End If
            Next
        End If


        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ingreso al PLAY de IfDocumentType")
        Select Case Me._myRule.Comp
            Case Comparators.Equal
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparo por =")
                For Each r As TaskResult In results
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                    If r.DocType.ID = Me._myRule.DocTypeId Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El tipo de Documento ingresado es el mismo al comparado.")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Doc_Type_ID = " & Me._myRule.DocTypeId)
                        NewList.Add(r)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El tipo de Documento ingresado es diferente al comparado.")
                    End If
                Next
            Case Comparators.Different
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparo por Distinto")
                For Each r As TaskResult In results
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                    If r.DocType.ID <> Me._myRule.DocTypeId Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El tipo de Documento ingresado es distinto al comparado.")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Doc_Type_ID = " & Me._myRule.DocTypeId)
                        NewList.Add(r)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El tipo de Documento ingresado es diferente al comparado.")
                    End If
                Next
        End Select


        Return NewList
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ingreso al PLAY de IfDocumentType")
        Select Case Me._myRule.Comp
            Case Comparators.Equal
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparo por =")
                For Each r As TaskResult In results
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                    If (r.DocType.ID = Me._myRule.DocTypeId) = ifType Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El tipo de Documento ingresado es el mismo al comparado.")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Doc_Type_ID = " & Me._myRule.DocTypeId)
                        NewList.Add(r)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El tipo de Documento ingresado es diferente al comparado.")
                    End If
                Next
            Case Comparators.Different
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparo por Distinto")
                For Each r As TaskResult In results
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                    If (r.DocType.ID <> Me._myRule.DocTypeId) = ifType Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El tipo de Documento ingresado es distinto al comparado.")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Doc_Type_ID = " & Me._myRule.DocTypeId)
                        NewList.Add(r)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El tipo de Documento ingresado es diferente al comparado.")
                    End If
                Next
        End Select

        Return NewList
    End Function
End Class
