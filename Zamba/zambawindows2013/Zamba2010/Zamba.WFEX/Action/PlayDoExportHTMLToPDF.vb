Imports Zamba.Core

Public Class PlayDoExportHTMLToPDF
    Private _myRule As IDoExportHTMLToPDF

    Public Sub New(ByVal rule As IDoExportHTMLToPDF)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Windows.Forms.MessageBox.Show("REGLA NO IMPLEMENTADA EN EL CLIENTE DE ESCRITORIO DE WINDOWS.")

        Return (results)
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class