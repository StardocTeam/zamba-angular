Imports Zamba.Core
Imports ZAMBA.AppBlock
'Imports Zamba.Barcodes
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Drawing


Public Class Barcode_Motor
    Inherits ZClass

    Public Overrides Sub Dispose()
    End Sub



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Imprime en la caratula un codigo de barras
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="Value"></param>
    ''' <param name="x"></param>
    ''' <param name="y"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function Print(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal Value As String, ByVal x As Integer, ByVal y As Integer) As Graphics
        Dim bc As New PrintControl.PrintBarcodes
        bc.BarCode = Value
        bc.HeaderText = ""
        bc.VertAlign = PrintControl.PrintBarcodes.AlignType.Left
        bc.LeftMargin = x
        bc.TopMargin = y
        e = bc.PrintImage(e)
        Return e.Graphics
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Agrega ceros al value del barcode 
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function convertCodeBar(ByVal data As Integer) As String
        Dim s As New System.Text.StringBuilder
        Try
            s.Append(data)
            If s.Length <= 9 Then
                s.Insert(0, "0", 9 - s.Length)
            End If
            Return s.ToString
        Finally
            s = Nothing
        End Try
    End Function
End Class
