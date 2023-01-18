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
        Return results
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
        Me.zvar = String.Empty

        If Me._myRule.TxtVar.IndexOf("zvar(") <> -1 Then
            Me.zvar = Me._myRule.TxtVar.Replace("zvar(", String.Empty)
            Me.zvar = Me.zvar.Replace(")", String.Empty).Trim()
        Else
            Me.zvar = Me._myRule.TxtVar
        End If

        Me.zvar = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.zvar, r)
        Trace.WriteLineIf(ZTrace.IsInfo, "Variable original: " & Me.zvar & " Variable pasada por textointeligente: " & Me.zvar)
        Me.zvar = Me.zvar.Trim()

        If VariablesInterReglas.ContainsKey(Me.zvar) Then
            If (TypeOf (VariablesInterReglas.Item(Me.zvar)) Is DataSet) Then
                Trace.WriteLineIf(ZTrace.IsInfo, "Variable DataSet")
                Me.ds = VariablesInterReglas.Item(Me.zvar)

                If IsNothing(Me.ds) Then
                    Me.zValue = String.Empty
                ElseIf IsDBNull(Me.ds) Then
                    Me.zValue = String.Empty
                ElseIf Me.ds.Tables.Count <= 0 Then
                    Me.zValue = String.Empty
                ElseIf Me.ds.Tables(0).Rows.Count = 1 AndAlso Me.ds.Tables(0).Columns.Count > 1 Then
                    Me.zValue = Me.ds.Tables(0).Rows(0)(1).ToString()
                ElseIf Me.ds.Tables(0).Rows.Count > 0 AndAlso Not IsDBNull(Me.ds.Tables(0).Rows(0)) Then
                    Me.zValue = "Dataset Con Valores"
                Else
                    Me.zValue = String.Empty
                End If
            ElseIf (VariablesInterReglas.Item(Me.zvar)) Is Nothing OrElse IsDBNull(VariablesInterReglas.Item(Me.zvar)) Then
                Trace.WriteLineIf(ZTrace.IsInfo, "Variable Vacia")
                Me.zValue = String.Empty
            ElseIf (TypeOf (VariablesInterReglas.Item(Me.zvar)) Is DataTable) Then
                Trace.WriteLineIf(ZTrace.IsInfo, "Variable Datatable")
                Me.dt = VariablesInterReglas.Item(Me.zvar)

                If Not IsNothing(Me.dt) AndAlso Not IsDBNull(Me.dt) AndAlso Me.dt.Rows.Count > 0 AndAlso Not IsDBNull(Me.dt.Rows(0)) Then
                    Me.zValue = Me.dt.Rows(0)(0).ToString() '.Tables(0).Rows(0)(1).ToString()
                Else
                    'Return 0
                    Me.zValue = String.Empty
                End If
            Else
                Trace.WriteLineIf(ZTrace.IsInfo, "Variable de otro tipo")
                Me.zValue = VariablesInterReglas.Item(Me.zvar)
            End If



        Else
            Trace.WriteLineIf(ZTrace.IsInfo, "No se ha encontrado la variable")
            Me.zValue = Me.zvar
        End If

        Me.rulevalue = String.Empty
        Me.rulevalue = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.TxtValue)

        Me.rulevalue = TextoInteligente.ReconocerCodigo(Me.rulevalue, r)

        If Not String.IsNullOrEmpty(Me.rulevalue) Then
            Me.rulevalue = Me.rulevalue.Trim()
        End If
        Trace.WriteLineIf(ZTrace.IsInfo, "Variable Obtenida: " & Me.zValue & " Operador: " & Me._myRule.Operador.ToString() & " Variable a comparar: " & Chr(34) & Me.rulevalue & Chr(34))

        If String.IsNullOrEmpty(Me.rulevalue) Then
            Me.rulevalue = Me._myRule.TxtValue
        End If
        Return ToolsBusiness.ValidateComp(Me.zValue, Me.rulevalue, Me._myRule.Operador, Me._myRule.CaseInsensitive)
    End Function

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)()
        Try

            For Each R As TaskResult In results
                If ValidateVar(R) = ifType Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "Resultado: Verdadero, validación por: " & ifType.ToString())
                    NewList.Add(R)
                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "Resultado: Falso, validación por: " & ifType.ToString())
                End If
            Next
        Finally
            Me.zValue = String.Empty
            Me.zvar = String.Empty
            Me.ds = Nothing
        End Try
        Return NewList
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)


        Return WFRuleParent.ReconocerZvar(Me._myRule.TxtValue)

    End Function
End Class