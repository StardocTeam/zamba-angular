Imports System
Imports System.Drawing
Imports System.Collections
Imports System.Data
Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Printing

Public Class Grid
    Dim fontGridFont As Font
    Dim fontGridHeadersFont As Font

    Public HeaderBackColor As Color
    Public HeaderForeColor As Color
    Public LineColor As Color
    Public ForeColor As Color
    Public BackColor As Color

    Dim row As Integer
    Dim column As Integer

    Public Cells(,) As Cell

    Public Headers() As Header

    Public ReadOnly Property Header(ByVal Column As Integer)
        Get
            Return Headers(Column)
        End Get
    End Property

    '/// <summary>
    '/// The Font of the text in the cells
    '/// </summary>
    Public Property Font() As Font
        Get
            Return fontGridFont
        End Get
        Set(ByVal Value As Font)
            fontGridFont = Value
        End Set
    End Property


    '/// <summary>
    '/// The Font of the text in the header cells
    '/// </summary>
    Public Property HeaderFont() As Font
        Get
            Return fontGridHeadersFont
        End Get
        Set(ByVal Value As Font)
            fontGridHeadersFont = Value
        End Set
    End Property


    Public ReadOnly Property Rows() As Integer
        Get
            Return row
        End Get
    End Property


    Public ReadOnly Property Columns() As Integer
        Get
            Return column
        End Get
    End Property


    '/// <summary>
    '/// Gets or Sets a Cell
    '/// </summary>
    ' Public Cell Me(int Property ColumnNumber)() As RowNumber,int

    Public Property Cell(ByVal RowNumber As Integer, ByVal ColumnNumber As Integer) As Cell
        Get
            '//check to see if the cell exists
            If (RowNumber >= 0 And ColumnNumber >= 0 And RowNumber <= Cells.GetUpperBound(0) And ColumnNumber <= Cells.GetUpperBound(1)) Then

                '//return found cell
                Return Cells(RowNumber, ColumnNumber)
            Else
                '//error - no cell found
                Return Nothing
                '//throw new NoCellException
            End If
        End Get
        Set(ByVal Value As Cell)
            '//Check the number of Cell to exist
            If (RowNumber >= 0 And ColumnNumber >= 0 And RowNumber <= Cells.GetUpperBound(0) And ColumnNumber <= Cells.GetUpperBound(1)) Then
                '				
                '//set value
                Cells(RowNumber, ColumnNumber) = Value

            Else

                '//throw new NoCellException
            End If
        End Set
    End Property

    '/// <summary>
    '/// Set a new value for a cell
    '/// </summary>
    Public WriteOnly Property Cell(ByVal cell1 As Cell) As Cell
        Set(ByVal Value As Cell)
            Cells(cell1.RowNumber, cell1.ColumnNumber) = Value
        End Set
    End Property


    Public Sub New(ByVal TheGrid As DataGrid)
        Try
            '//get the Data in the grid
            Dim TableGrid As DataTable = Nothing

            If (TheGrid.DataSource.GetType() Is GetType(DataView)) Then
                Dim ViewGrid As DataView = CType(TheGrid.DataSource, DataView)
                TableGrid = ViewGrid.Table
            Else
                TableGrid = CType(TheGrid.DataSource, DataTable)
            End If
            '//set number of rows
            row = TableGrid.Rows.Count

            '//set number of columns
            '//first check if the grid has tablestyle and columnstyle

            '/'/check for table styles
            If (TheGrid.TableStyles.Count = 0) Then

                '//create table style and column style
                CreateColumnStyles(TheGrid, TableGrid)

            Else

                '//create column styles if there are none
                If (TheGrid.TableStyles(TableGrid.TableName).GridColumnStyles.Count = 0) Then
                    CreateColumnStyles(TheGrid, TableGrid)
                End If
            End If
            '//set number of columns
            column = TheGrid.TableStyles(TableGrid.TableName).GridColumnStyles.Count

            Cells = New Cell(Rows, Columns) {}
            'Copy Cells
            Dim i As Integer
            For i = 0 To Rows - 1
                Dim j As Integer
                For j = 0 To Columns - 1
                    Cells(i, j) = New Cell(i, j, TheGrid.Font, TheGrid.TableStyles(TableGrid.TableName).GridColumnStyles(j).Alignment, New RectangleF(0, 0, TheGrid.GetCellBounds(i, j).Width, TheGrid.GetCellBounds(i, j).Height), TheGrid(i, j).ToString())
                Next
            Next
            'init number of columns to headers
            Headers = New Header(column) {}
            SetHeaders(TheGrid, TableGrid)

            '//define grid colors
            SetColors(TheGrid)

        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try
    End Sub

    Private Sub CreateColumnStyles(ByVal TheGrid As DataGrid, ByVal TableGrid As DataTable)

        '// Define new table style.
        Dim tableStyle As DataGridTableStyle = New DataGridTableStyle

        Dim g As Graphics = TheGrid.CreateGraphics()

        Try


            '// Clear any existing table styles.
            TheGrid.TableStyles.Clear()

            '// Use mapping name that is defined in the data source.
            tableStyle.MappingName = TableGrid.TableName

            '// Now create the column styles within the table style.
            Dim columnStyle As DataGridTextBoxColumn

            Dim iCurrCol As Integer
            For iCurrCol = 0 To TableGrid.Columns.Count - 1
                Dim dataColumn As DataColumn = TableGrid.Columns(iCurrCol)

                columnStyle = New DataGridTextBoxColumn

                columnStyle.HeaderText = dataColumn.ColumnName
                columnStyle.MappingName = dataColumn.ColumnName


                columnStyle.TextBox.Width = TheGrid.GetCellBounds(0, iCurrCol).Width

                columnStyle.TextBox.Height = CType(g.MeasureString(columnStyle.HeaderText, TheGrid.HeaderFont).Height, Integer) + 10


                '// Add the new column style to the table style.
                tableStyle.GridColumnStyles.Add(columnStyle)
            Next


            '// Add the new table style to the data grid.

            TheGrid.TableStyles.Add(tableStyle)

        Catch e As Exception

            Console.WriteLine(e.Message)

        Finally

            g.Dispose()
        End Try

    End Sub
    Private Sub SetHeaders(ByVal TheGrid As DataGrid, ByVal TableGrid As DataTable)

        Try

            '//Check if there are styles
            If (TheGrid.TableStyles.Count > 0) Then

                If (TheGrid.TableStyles(TableGrid.TableName).GridColumnStyles.Count > 0) Then
                    Dim i As Integer
                    For i = 0 To Headers.GetUpperBound(0) - 1

                        '//Known bug - when there are no rows headers are not displayed properly
                        Dim columnStyle As DataGridTextBoxColumn = CType(TheGrid.TableStyles(TableGrid.TableName).GridColumnStyles(i), DataGridTextBoxColumn)

                        If (Cells.GetUpperBound(0) > 0) Then

                            'Headers(i) = New Header(i, TheGrid.HeaderFont, columnStyle.Alignment, New Rectangle(Cells(0, i).Location.X, columnStyle.TextBox.Bounds.Y, Cells(0, i).Location.Width, CType(TheGrid.TableStyles(TableGrid.TableName).GridColumnStyles(0), DataGridTextBoxColumn), columnStyle.HeaderText)
                            Headers(i) = New Header(i, TheGrid.HeaderFont, columnStyle.Alignment, New RectangleF(Cells(0, i).Location.X, columnStyle.TextBox.Bounds.Y, Cells(0, i).Location.Width, CType(TheGrid.TableStyles(TableGrid.TableName).GridColumnStyles(0), DataGridTextBoxColumn).TextBox.Height), columnStyle.HeaderText)
                        Else

                            Headers(i) = New Header(i, TheGrid.HeaderFont, columnStyle.Alignment, New RectangleF(columnStyle.TextBox.Bounds.X, columnStyle.TextBox.Bounds.Y, columnStyle.TextBox.Bounds.Width, columnStyle.TextBox.Bounds.Height), columnStyle.HeaderText)
                        End If
                    Next

                End If
            End If

        Catch ex As Exception

            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SetColors(ByVal TheGrid As DataGrid)

        HeaderBackColor = TheGrid.HeaderBackColor
        HeaderForeColor = TheGrid.HeaderForeColor
        LineColor = TheGrid.GridLineColor
        ForeColor = TheGrid.ForeColor
        BackColor = TheGrid.BackColor
    End Sub
End Class
