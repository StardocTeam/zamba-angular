Public Class PlayDoReadExcel

    Private _myrule As IDoReadExcel
    Private _exceldata As String
    Private _excelFile As String


    Sub New(ByVal rule As IDoReadExcel)
        Me._myrule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)

        For Each r As ITaskResult In results

            Me._excelFile = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myrule.ExcelFile, r)
            Me._excelFile = WFRuleParent.ReconocerVariables(Me._excelFile).TrimEnd
            Me._exceldata = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myrule.ExcelData, r)
            Me._exceldata = WFRuleParent.ReconocerVariables(Me._exceldata).TrimEnd

            Dim hs As Hashtable = Zamba.Office.ExcelInterop.ReadExcel(Me._excelFile, Me._myrule.ExcelData)

            For Each item As String In hs.Keys
                TextoInteligente.AsignItemFromSmartText(item.Trim, r, hs.Item(item).ToString)
                Trace.WriteLineIf((ZTrace.IsVerbose) AndAlso (Not IsNothing(hs.Item(item).ToString)), "Resultado Escalar: " & hs.Item(item).ToString)

                Trace.WriteLineIf(ZTrace.IsVerbose, "Guardando en variable: " & item.Trim)

                If WFRuleParent.VariablesInterReglas.ContainsKey(item.Trim) = False Then
                    WFRuleParent.VariablesInterReglas.Add(item.Trim, hs.Item(item).ToString)
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Variable Creada")
                Else
                    WFRuleParent.VariablesInterReglas(item.Trim) = hs.Item(item).ToString
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Variable Guardada")
                End If
            Next

        Next


        Return results
    End Function
    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
