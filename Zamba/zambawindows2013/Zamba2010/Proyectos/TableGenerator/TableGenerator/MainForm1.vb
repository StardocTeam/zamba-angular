Imports System.Windows.Forms
Public Class MainForm

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AxWebBrowser1.Navigate(Application.StartupPath + "\test.html")

    End Sub


    ''' <summary>
    '''Carga el contenido de un DataTabla en una tabla Html del documento
    ''' </summary>
    ''' <param name="tableId"></param>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub LoadTable(ByVal tableId As String, ByVal dt As DataTable)
        Dim Table As HtmlElement = AxWebBrowser1.Document.GetElementById(tableId)
        If (Not IsNothing(Table)) Then
            LoadTableHeader(Table, dt.Columns)
            LoadTableBody(Table, dt.Rows)
        End If
    End Sub
    ''' <summary>
    ''' Carga las columnas header de un DataTable en el una tabla Html del documento
    ''' </summary>
    ''' <param name="table"></param>
    ''' <param name="dcs"></param>
    ''' <remarks></remarks>
    Private Sub LoadTableHeader(ByRef table As HtmlElement, ByVal dcs As DataColumnCollection)
        Dim HeaderRow As HtmlElement = AxWebBrowser1.Document.CreateElement("tr")

        Dim HeaderColumn As HtmlElement = Nothing
        For Each Column As DataColumn In dcs
            HeaderColumn = AxWebBrowser1.Document.CreateElement("th")
            HeaderColumn.InnerHtml = Column.ColumnName

            HeaderRow.AppendChild(HeaderColumn)
        Next
        table.AppendChild(HeaderRow)
    End Sub
    ''' <summary>
    ''' Carga el contenido de un DataTabla en el body de una tabla Html del documento
    ''' </summary>
    ''' <param name="table"></param>
    ''' <param name="drs"></param>
    ''' <remarks></remarks>
    Private Sub LoadTableBody(ByRef table As HtmlElement, ByVal drs As DataRowCollection)
        Dim CurrentRow As HtmlElement = Nothing
        Dim CurrentCell As HtmlElement = Nothing
        For Each dr As DataRow In drs
            CurrentRow = AxWebBrowser1.Document.CreateElement("tr")


            For Each CellValue As Object In dr.ItemArray
                CurrentCell = AxWebBrowser1.Document.CreateElement("td")
                CurrentCell.InnerHtml = CellValue.ToString()

                CurrentRow.AppendChild(CurrentCell)
            Next
            table.AppendChild(CurrentRow)
        Next
    End Sub


    Private Sub AxWebBrowser1_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles AxWebBrowser1.DocumentCompleted

        Dim dt As New DataTable()

        For i As Int32 = 0 To 5
            dt.Columns.Add(New DataColumn("Columna Nº " + i.ToString()))
        Next

        Dim CurrentRow As DataRow = Nothing
        For j As Int32 = 0 To 5
            CurrentRow = dt.NewRow()

            For k As Int32 = 0 To 5
                CurrentRow(k) = ((j * 5) + k).ToString()
            Next

            dt.Rows.Add(CurrentRow)
        Next

        LoadTable("mainTable", dt)

        tbHtml.Text = AxWebBrowser1.Document.Body.OuterHtml
    End Sub
End Class