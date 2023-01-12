Public Class PlayDoWriteOnExcel

    Private _myrule As IDoWriteOnExcel
    Private _exceldata As String
    Private _excelFile As String


    Sub New(ByVal rule As IDoWriteOnExcel)
        Me._myrule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)

        For Each r As ITaskResult In results

            Me._excelFile = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myrule.ExcelFile, r)
            Me._excelFile = WFRuleParent.ReconocerVariables(Me._excelFile).TrimEnd
            Me._exceldata = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myrule.ExcelData, r)
            Me._exceldata = WFRuleParent.ReconocerVariables(Me._exceldata).TrimEnd

            Zamba.Office.ExcelInterop.WriteOnExcel(Me._excelFile, Me._exceldata)
        Next


        Return results
    End Function
    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
