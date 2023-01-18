Imports System.Windows.Forms
Imports zamba.Grid

Public Class PlayDoGenerateBarcode
    Private filePath As String
    Private barcode As String
    Private BarHeight As String
    Private Height As Int64
    Private _myRule As IDOGenerateBarcode

    Public Sub New(ByVal rule As IDoGenerateBarcode)
        _myRule = rule
    End Sub

    ''' <summary>
    '''    
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myRule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Marcelo] 01-07-10 Created Create a Barcode
    ''' </history>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            If results.Count > 0 Then
                Me.filePath = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.filePath, results(0))
                Me.barcode = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Barcode, results(0))
                Me.BarHeight = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Height, results(0))
            End If
            Me.filePath = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me.filePath).Trim()
            Me.barcode = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me.barcode).Trim()
            Me.BarHeight = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me.BarHeight).Trim()
            If String.IsNullOrEmpty(barcode) = False And String.IsNullOrEmpty(barheight) = False And String.IsNullOrEmpty(filePath) = False Then
                If Int64.TryParse(BarHeight, Height) Then
                    BarcodesBussines.SaveBarcodeToFile(barcode, BarHeight, filePath.Split(".")(0), _myRule.FileExtension.ToLower())
                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "No se ha definido la altura del codigo de barras")
                End If
            Else
                Trace.WriteLineIf(ZTrace.IsInfo, "Alguno de los parametros de la regla no estan definidos o no tienen valor")
            End If
        Finally
            Trace.WriteLineIf(ZTrace.IsVerbose, "Liberando recursos.")
            Me.barcode = Nothing
            Me.filePath = Nothing
        End Try

        Return results
    End Function

    
End Class