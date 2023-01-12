Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports System.IO

''' <summary>
''' Clase que compara 2 excel. Se creo en una urgencia para la migracion de HDI.
''' </summary>
''' <history> Marcelo Created 20/05/2012</history>
''' <remarks></remarks>
Public Class ExcelCompare

    ''' <summary>
    ''' Compara 2 excel y marca las diferencias entre ambos
    ''' </summary>
    ''' <param name="rutaArchivo1"></param>
    ''' <param name="rutaArchivo2"></param>
    ''' <remarks></remarks>
    Private Sub CompararExcels(ByVal rutaArchivo1 As String, ByVal rutaArchivo2 As String)
        Dim Exc1 As Excel.Application = New Excel.Application

        Try
            Try
                Dim sw As New StreamWriter(System.Windows.Forms.Application.StartupPath + "\\ValoresDefecto.txt")
                sw.AutoFlush = True
                sw.WriteLine(txtExcel1.Text)
                sw.WriteLine(txtExcel2.Text)
                sw.Close()
                sw.Dispose()
                sw = Nothing
            Catch ex As Exception
                Trace.WriteLine("Error en valores por defecto")
                Trace.WriteLine(ex.ToString())
            End Try

            Exc1.Workbooks.Open(rutaArchivo1)
            Exc1.Workbooks.Open(rutaArchivo2)

            Dim ws1 As Worksheet = Exc1.Workbooks(1).Worksheets(1)
            Dim ws2 As Worksheet = Exc1.Workbooks(2).Worksheets(1)

            Dim countFin As Int64 = 0
            For j As Int32 = 1 To ws1.Columns.Count
                If countFin < 2 Then
                    Dim count As Int64 = 0
                    For i As Int32 = 1 To ws1.Rows.Count
                        If count < 2 Then
                            If ws1.Cells(i, j).value Is Nothing Or ws2.Cells(i, j).value Is Nothing Then
                                count = count + 1
                            Else
                                If ws1.Cells(i, j).value.ToString() <> ws2.Cells(i, j).value.ToString() Then
                                    If (ws1.Cells(i, j).value.ToString() = "0" And ws2.Cells(i, j).value.ToString() = "False") Or (ws1.Cells(i, j).value.ToString() = "1" And ws2.Cells(i, j).value.ToString() = "True") Or (ws1.Cells(i, j).value.ToString() = "False" And ws2.Cells(i, j).value.ToString() = "0") Or (ws1.Cells(i, j).value.ToString() = "True" And ws2.Cells(i, j).value.ToString() = "1") Then
                                        ws1.Cells(i, j).Interior.Color = Color.White
                                        ws1.Cells(i, j).Font.Color = Color.Black
                                    Else
                                        ws1.Cells(i, j).Interior.Color = Color.LightCyan
                                        ws1.Cells(i, j).Font.Color = Color.Red
                                    End If
                                Else
                                    ws1.Cells(i, j).Interior.Color = Color.White
                                    ws1.Cells(i, j).Font.Color = Color.Black
                                End If
                            End If
                        Else
                            If i = 3 Then
                                countFin = countFin + 1
                            Else
                                countFin = 0
                            End If
                            Exit For
                        End If
                    Next
                Else
                    Exit For
                End If
            Next

            Exc1.Workbooks(1).Save()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

            Exc1.Workbooks.Close()
            Exc1.Quit()
            Exc1 = Nothing

            MessageBox.Show("Proceso finalizado")
            Me.Close()
    End Sub


    ''' <summary>
    ''' Ejecuta la comparacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCompare_Click(sender As System.Object, e As System.EventArgs) Handles btnCompare.Click
        CompararExcels(txtExcel1.Text, txtExcel2.Text)
    End Sub

    ''' <summary>
    ''' Cuando carga el formulario, completa los txt con los valores anteriores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExcelCompare_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Dim sw As New StreamReader(System.Windows.Forms.Application.StartupPath + "\\ValoresDefecto.txt")
            txtExcel1.Text = sw.ReadLine()
            txtExcel2.Text = sw.ReadLine()
            sw.Close()
            sw.Dispose()
            sw = Nothing
        Catch ex As Exception
            Trace.WriteLine("Error en valores por defecto")
            Trace.WriteLine(ex.ToString())
        End Try
    End Sub

    ''' <summary>
    ''' Permite buscar la ruta de un excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExcel1_Click(sender As System.Object, e As System.EventArgs) Handles btnExcel1.Click
        Dim fileDialog As New OpenFileDialog()
        If fileDialog.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            txtExcel1.Text = fileDialog.FileName
        End If
        fileDialog.Dispose()
        fileDialog = Nothing
    End Sub

    ''' <summary>
    ''' Permite buscar la ruta de un excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExcel2_Click(sender As System.Object, e As System.EventArgs) Handles btnExcel2.Click
        Dim fileDialog As New OpenFileDialog()
        If fileDialog.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            txtExcel2.Text = fileDialog.FileName
        End If
        fileDialog.Dispose()
        fileDialog = Nothing
    End Sub
End Class