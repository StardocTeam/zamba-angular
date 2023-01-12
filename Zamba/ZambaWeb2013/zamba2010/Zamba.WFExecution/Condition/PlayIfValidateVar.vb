Imports Zamba.Core

Public Class PlayIfValidateVar
    Private _myRule As IIfValidateVar
    Private zValue As String
    Private zvar As String
    Private ds As DataSet
    Private rulevalue As String
    Private dt As DataTable
    Private dstest As DataSet

    Sub New(ByVal rule As IIfValidateVar)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFRB As New WFRulesBusiness
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
        If (Me._myRule.ChildRulesIds Is Nothing OrElse Me._myRule.ChildRulesIds.Count = 0) Then
            Me._myRule.ChildRulesIds = WFRB.GetChildRulesIds(Me._myRule.ID, Me._myRule.RuleClass, results)
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

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)()
        Try
            For Each R As TaskResult In results
                If ValidateVar(R) = 0 Then
                    NewList.Add(R)
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Me.zValue = Nothing
            Me.zvar = Nothing
            Me.ds = Nothing
        End Try
        Return NewList
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="myrule"></param>
    ''' <param name="r"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateVar(ByVal r As Result) As Boolean

        Me.zValue = String.Empty
        Me.zvar = Me._myRule.TxtVar

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable original: " & Me.zvar)
        Me.zvar = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me.zvar)
        Me.zvar = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.zvar, r)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable pasada por textointeligente: " & Me.zvar)
        Me.zvar = Me.zvar.Trim()

        If Me._myRule.TxtVar.IndexOf("zvar(") <> -1 Then
            Me.zvar = Me.zvar.Replace("zvar(", String.Empty).Trim()
            If (Me.zvar.Contains("(")) Then
                Me.zvar = Me.zvar.Remove(zvar.Length - 1)
            Else
                Me.zvar = Me.zvar.Replace(")", String.Empty).Trim()
            End If
        ElseIf String.IsNullOrEmpty(Me.zvar) Then
            Me.zvar = Me._myRule.TxtVar
        End If

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable final: " & Me.zvar)

        If VariablesInterReglas.ContainsKey(Me.zvar) Then
            If (TypeOf (VariablesInterReglas.Item(Me.zvar)) Is DataSet) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable DataSet")
                Me.ds = VariablesInterReglas.Item(Me.zvar)
                If Not IsNothing(Me.ds) AndAlso Not IsDBNull(Me.ds) AndAlso Me.ds.Tables.Count > 0 AndAlso Me.ds.Tables(0).Rows.Count > 0 AndAlso Not IsDBNull(Me.ds.Tables(0).Rows(0)) Then
                    Me.zValue = Me.ds.Tables(0).Rows(0)(1).ToString()
                Else
                    'Return 0
                    Me.zValue = String.Empty
                End If
            ElseIf (TypeOf (VariablesInterReglas.Item(Me.zvar)) Is DataRow) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable DataRow")
                Dim dr As DataRow = VariablesInterReglas.Item(Me.zvar)
                If Not IsDBNull(dr) Then
                    Me.zValue = dr(0).ToString()
                Else
                    'Return 0
                    Me.zValue = String.Empty
                End If
            ElseIf (VariablesInterReglas.Item(Me.zvar)) Is Nothing OrElse IsDBNull(VariablesInterReglas.Item(Me.zvar)) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable Vacia")
                Me.zValue = String.Empty
            ElseIf (TypeOf (VariablesInterReglas.Item(Me.zvar)) Is DataTable) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable Datatable")
                Me.dt = VariablesInterReglas.Item(Me.zvar)

                If Not IsNothing(Me.dt) AndAlso Not IsDBNull(Me.dt) AndAlso Me.dt.Rows.Count > 0 AndAlso Not IsDBNull(Me.dt.Rows(0)) Then
                    Me.zValue = Me.dt.Rows(0)(0).ToString() '.Tables(0).Rows(0)(1).ToString()
                Else
                    'Return 0
                    Me.zValue = String.Empty
                End If
            ElseIf (TypeOf (VariablesInterReglas.Item(Me.zvar)) Is Byte()) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable Byte")
                Me.zValue = "Byte"
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable de otro tipo")
                Me.zValue = VariablesInterReglas.Item(Me.zvar)
            End If
        ElseIf Me._myRule.TxtVar.IndexOf("<<") <> -1 Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "La variable es Texto Inteligente")
            Me.zValue = Me.zvar
        Else

            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha encontrado la variable")
            Me.zValue = zvar
        End If

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable Obtenida: " & Me.zValue)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Operador: " & Me._myRule.Operador.ToString())
        Me.rulevalue = String.Empty
        Dim VarInterReglas As New VariablesInterReglas()
        Me.rulevalue = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._myRule.TxtValue)
        VarInterReglas = Nothing

        Me.rulevalue = TextoInteligente.ReconocerCodigo(Me.rulevalue, r)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable a comparar Obtenida: " & Me.rulevalue)
        If Not String.IsNullOrEmpty(Me.rulevalue) Then
            Me.rulevalue = Me.rulevalue.Trim
        End If

        If String.IsNullOrEmpty(Me.rulevalue) Then
            Me.rulevalue = Me._myRule.TxtValue
        End If
        Return ToolsBusiness.ValidateComp(Me.zValue, Me.rulevalue, Me._myRule.Operador, Me._myRule.CaseInsensitive)
    End Function

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)()
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando validación por: " & ifType.ToString())
            For Each R As TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & R.Name)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Realizando validación...")
                If ValidateVar(R) = ifType Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Verdadero")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando result a la lista")
                    NewList.Add(R)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: Falso")
                End If
            Next
        Finally
            Me.zValue = String.Empty
            Me.zvar = String.Empty
            Me.ds = Nothing
        End Try
        Return NewList
    End Function
End Class