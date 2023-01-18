Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Collections
Imports System.Data

Public Class PrinterClass
    '//clone of Datagrid
    Dim PrintGrid As Grid

    '//printdocument for initial printer settings
    Private PrintDoc As PrintDocument

    '//defines whether the grid is ordered right to left
    Private bRightToLeft As Boolean

    '//Current Top
    Private CurrentY As Single = 0

    '//Current Left
    Private CurrentX As Single = 0

    '//CurrentRow to print
    Private CurrentRow As Integer = 0

    '//Page Counter
    Public PageCounter As Integer = 0

    '/// <summary>
    '/// Constructor Class
    '/// </summary>
    '/// <param name="pdocument"></param>
    '/// <param name="dgrid"></param>
    Public Sub New(ByVal pdocument As PrintDocument, ByVal dgrid As DataGrid)
        'MyBase.new()

        PrintGrid = New Grid(dgrid)
        PrintDoc = pdocument

        '//The grid columns are right to left
        bRightToLeft = dgrid.RightToLeft = RightToLeft.Yes

        '//init CurrentX and CurrentY
        CurrentY = pdocument.DefaultPageSettings.Margins.Top
        CurrentX = pdocument.DefaultPageSettings.Margins.Left


    End Sub

    Public Function Print(ByVal g As Graphics, ByRef currentX As Single, ByRef currentY As Single) As Boolean

        '//use predefined area
        currentX = currentX
        currentY = currentY

        PrintHeaders(g)

        Dim Morepages As Boolean = PrintDataGrid(g)

        currentY = currentY
        currentX = currentX

        Return Morepages


    End Function

    Public Function Print(ByVal g As Graphics) As Boolean

        CurrentX = PrintDoc.DefaultPageSettings.Margins.Left
        CurrentY = PrintDoc.DefaultPageSettings.Margins.Top
        PrintHeaders(g)
        Return PrintDataGrid(g)
    End Function

    '/// <summary>
    '/// Print the Grid Headers
    '/// </summary>
    '/// <param name="g"></param>
    Private Sub PrintHeaders(ByVal g As Graphics)

        Dim sf As StringFormat = New StringFormat

        '//if we want to print the grid right to left
        If (bRightToLeft) Then

            CurrentX = PrintDoc.DefaultPageSettings.PaperSize.Width - PrintDoc.DefaultPageSettings.Margins.Right
            sf.FormatFlags = StringFormatFlags.DirectionRightToLeft
        Else
            CurrentX = PrintDoc.DefaultPageSettings.Margins.Left
        End If
        Dim i As Integer
        For i = 0 To PrintGrid.Columns - 1

            '//set header alignment
            Select Case (CType(PrintGrid.Headers.GetValue(i), Header).Alignment)
                Case HorizontalAlignment.Left 'left 
                    sf.Alignment = StringAlignment.Near
                Case HorizontalAlignment.Center
                    sf.Alignment = StringAlignment.Center
                Case HorizontalAlignment.Right
                    sf.Alignment = StringAlignment.Far
            End Select


            '//advance X according to order
            If (bRightToLeft) Then


                '//draw the cell bounds (lines) and back color
                g.FillRectangle(New SolidBrush(PrintGrid.HeaderBackColor), CurrentX - PrintGrid.Headers(i).Width, CurrentY, PrintGrid.Headers(i).Width, PrintGrid.Headers(i).Height)
                g.DrawRectangle(New Pen(PrintGrid.LineColor), CurrentX - PrintGrid.Headers(i).Width, CurrentY, PrintGrid.Headers(i).Width, PrintGrid.Headers(i).Height)

                '//draw the cell text
                g.DrawString(PrintGrid.Headers(i).CText, PrintGrid.Headers(i).Font, New SolidBrush(PrintGrid.HeaderForeColor), New RectangleF(CurrentX - PrintGrid.Headers(i).Width, CurrentY, PrintGrid.Headers(i).Width, PrintGrid.Headers(i).Height), sf)
                '//next cell
                CurrentX -= PrintGrid.Headers(i).Width
            Else
                '//draw the cell bounds (lines) and back color
                g.FillRectangle(New SolidBrush(PrintGrid.HeaderBackColor), CurrentX, CurrentY, PrintGrid.Headers(i).Width, PrintGrid.Headers(i).Height)
                g.DrawRectangle(New Pen(PrintGrid.LineColor), CurrentX, CurrentY, PrintGrid.Headers(i).Width, PrintGrid.Headers(i).Height)


                '//draw the cell text
                g.DrawString(PrintGrid.Headers(i).CText, PrintGrid.Headers(i).Font, New SolidBrush(PrintGrid.HeaderForeColor), New RectangleF(CurrentX, CurrentY, PrintGrid.Headers(i).Width, PrintGrid.Headers(i).Height), sf)

                '//next cell
                CurrentX += PrintGrid.Headers(i).Width
            End If
        Next

        '//reset to beginning
        If (bRightToLeft) Then
            '//right align
            CurrentX = PrintDoc.DefaultPageSettings.PaperSize.Width - PrintDoc.DefaultPageSettings.Margins.Right
        Else
            '//left align
            CurrentX = PrintDoc.DefaultPageSettings.Margins.Left
        End If

        '//advance to next row
        CurrentY = CurrentY + CType(PrintGrid.Headers.GetValue(0), Header).Height

    End Sub



    Private Function PrintDataGrid(ByVal g As Graphics) As Boolean
        Dim sf As StringFormat = New StringFormat
        PageCounter = PageCounter + 1

        '//if we want to print the grid right to left
        If (bRightToLeft) Then
            CurrentX = PrintDoc.DefaultPageSettings.PaperSize.Width - PrintDoc.DefaultPageSettings.Margins.Right
            sf.FormatFlags = StringFormatFlags.DirectionRightToLeft
        Else
            CurrentX = PrintDoc.DefaultPageSettings.Margins.Left
        End If
        Dim i As Integer
        For i = CurrentRow To PrintGrid.Rows - 1
            Dim j As Integer
            For j = 0 To PrintGrid.Columns - 1

                '//set cell alignment
                Select Case (PrintGrid.Cell(i, j).Alignment)
                    '//left
                Case HorizontalAlignment.Left
                        sf.Alignment = StringAlignment.Near

                    Case HorizontalAlignment.Center
                        sf.Alignment = StringAlignment.Center


                        '//right
                    Case HorizontalAlignment.Right
                        sf.Alignment = StringAlignment.Far

                End Select

                '//advance X according to order
                If (bRightToLeft) Then
                    '//draw the cell bounds (lines) and back color
                    g.FillRectangle(New SolidBrush(PrintGrid.BackColor), CurrentX - PrintGrid.Cell(i, j).Width, CurrentY, PrintGrid.Cell(i, j).Width, PrintGrid.Cell(i, j).Height)
                    g.DrawRectangle(New Pen(PrintGrid.LineColor), CurrentX - PrintGrid.Cell(i, j).Width, CurrentY, PrintGrid.Cell(i, j).Width, PrintGrid.Cell(i, j).Height)

                    '//draw the cell text
                    g.DrawString(PrintGrid.Cell(i, j).CText, PrintGrid.Cell(i, j).Font, New SolidBrush(PrintGrid.ForeColor), New RectangleF(CurrentX - PrintGrid.Cell(i, j).Width, CurrentY, PrintGrid.Cell(i, j).Width, PrintGrid.Cell(i, j).Height), sf)

                    '//next cell
                    CurrentX -= PrintGrid.Cell(i, j).Width
                Else
                    '//draw the cell bounds (lines) and back color
                    g.FillRectangle(New SolidBrush(PrintGrid.BackColor), CurrentX, CurrentY, PrintGrid.Cell(i, j).Width, PrintGrid.Cell(i, j).Height)
                    g.DrawRectangle(New Pen(PrintGrid.LineColor), CurrentX, CurrentY, PrintGrid.Cell(i, j).Width, PrintGrid.Cell(i, j).Height)
                    '//draw the cell text
                    '//Draw text by alignment
                    g.DrawString(PrintGrid.Cell(i, j).CText, PrintGrid.Cell(i, j).Font, New SolidBrush(PrintGrid.ForeColor), New RectangleF(CurrentX, CurrentY, PrintGrid.Cell(i, j).Width, PrintGrid.Cell(i, j).Height), sf)

                    '//next cell
                    CurrentX += PrintGrid.Cell(i, j).Width
                End If


            Next

            '//reset to beginning
            If (bRightToLeft) Then
                '//right align
                CurrentX = PrintDoc.DefaultPageSettings.PaperSize.Width - PrintDoc.DefaultPageSettings.Margins.Right
            Else
                '//left align
                CurrentX = PrintDoc.DefaultPageSettings.Margins.Left
            End If

            '//advance to next row
            CurrentY += PrintGrid.Cell(i, 0).Height
            CurrentRow += 1
            '//if we are beyond the page margin (bottom) then we need another page,
            '//return true
            If (CurrentY > PrintDoc.DefaultPageSettings.PaperSize.Height - PrintDoc.DefaultPageSettings.Margins.Bottom) Then

                Return True
            End If
        Next
        Return False

    End Function
End Class
