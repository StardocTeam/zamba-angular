Imports System.IO
Imports Microsoft.Office.Interop
'Imports Zamba.CoreControls
Imports System.data
Imports System.Collections
'Imports ZAMBA.Core
'Imports Zamba.ListControls
Imports System.Drawing.Printing
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Office
''' Class	 : Office.ExcelInterop
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para manejar objetos de Microsoft Office
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Marcelo]	27/11/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class OfficeInterop

    ''' <summary>
    ''' Ask for the path and save a document in PowerPoint
    ''' </summary>
    ''' <param name="OfficeFile">Word Document or PowerPoint Presentation to Save</param>
    '''<param name="Isword">If the document is word=true, if is PowerPoint=false</param>
    ''' <remarks></remarks>
    Public Shared Sub SaveAsOffice(ByVal OfficeFile As Object, ByVal isWord As Boolean)
        Dim DS As New SaveFileDialog()

        DS.CheckPathExists = True
        DS.ValidateNames = True
        DS.RestoreDirectory = True
        If isWord = True Then
            DS.Filter = "Microsoft Office Word|*.doc"
        Else
            DS.Filter = "Microsoft Office PowerPoint|*.ppt"
        End If
        If DS.ShowDialog() = DialogResult.OK Then

            Dim oPath As Object

            oPath = DS.FileName
            OfficeFile.SaveAs(oPath)

        End If
    End Sub

    ''' <summary>
    '''  Prints a Word Document
    ''' </summary>
    ''' <param name="OfficeFile">File to print</param>
    '''<param name="Isword">If the document is word=true, if is PowerPoint=false</param>
    ''' <remarks></remarks>
    Public Shared Sub ImprimirWord(ByVal path As String)
        Try
            wordPP(path)
            'Dim thrWord As New Threading.Thread(AddressOf wordPP)
            ' MessageBox.Show("Aguarde unos segundos mientras se carga el documento", "Vista Previa", MessageBoxButtons.OK)
            ' thrWord.Start(path)
        Catch ex As Threading.ThreadStartException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadStateException
        End Try
    End Sub
    Private Shared Sub wordPP(ByVal path As Object)
        Dim word As New Word.Application()

        Try
            'Open the document
            word.Documents.Add(path)

            ''Enable the print Preview bar only
            'For Each commandBar As Microsoft.Office.Core.CommandBar In word.CommandBars
            '    If commandBar.Name <> "Print Preview" Then
            '        commandBar.Enabled = False
            '    End If
            'Next

            'Event than handles the before Print of the document
            'RemoveHandler word.DocumentBeforePrint, AddressOf beforePrinted
            '    AddHandler word.DocumentBeforePrint, AddressOf beforePrinted


            'Show the document and the preview
            word.Visible = True

            '    word.ActiveDocument.PrintPreview()
            word.Dialogs(Microsoft.Office.Interop.Word.WdWordDialog.wdDialogFilePrint).Show()

            'Wait until the document is printed or closed
            '    While word.PrintPreview = True And word.Visible = True
            'Threading.Thread.Sleep(1000)
            '    End While
            '    RemoveHandler word.DocumentBeforePrint, AddressOf beforePrinted

            'Close the document
            word.Visible = False
            word.ActiveDocument.Close(False)
            word.Quit()
            word = Nothing

            GC.Collect()

        Catch ex As Exception
            word.ActiveDocument.Close(False)
            word.Quit()
            word = Nothing
        End Try
    End Sub

    'The document before being printed
    'Private Shared Sub beforePrinted(ByVal Doc As Word.Document, ByRef cancel As Boolean)
    '        cancel = True
    '    Dim printDialog1 As System.Windows.Forms.PrintDialog = New System.Windows.Forms.PrintDialog
    '    printDialog1.AllowSomePages = True
    '    If printDialog1.ShowDialog() = DialogResult.OK Then
    '        Doc.Application.ActivePrinter = printDialog1.PrinterSettings.PrinterName
    '        If (printDialog1.PrinterSettings.FromPage > 0 And printDialog1.PrinterSettings.ToPage > 0) Then
    '            Doc.PrintOut(0, 0, "3", "", printDialog1.PrinterSettings.FromPage.ToString(), printDialog1.PrinterSettings.ToPage.ToString())
    '        Else
    '            Doc.PrintOut(, , , , , , , printDialog1.PrinterSettings.Copies)
    '        End If
    '        Doc.Application.PrintPreview = False
    '    End If
    'End Sub

    ''' <summary>
    '''  Prints a PowerPoint Document
    ''' </summary>
    ''' <param name="OfficeFile">File to print</param>
    ''' <remarks></remarks>
    Public Shared Sub ImprimirPP(ByVal PPFile As Object)
        Dim PD As New PrintDialog
        PD.AllowSomePages = True
        If PD.ShowDialog = DialogResult.OK Then
            PPFile.PrintOut(0, , PD.PrinterSettings.PrinterName, PD.PrinterSettings.Copies, )
        End If
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Genera un codigo de barras y lo guarda en un documento de word
    ''' </summary>
    ''' <param name="Obj">Objeto del tipo wordDocument donde se va a insertar el barcode</param>
    ''' <param name="Value">Valor del barcode</param>
    ''' <history>
    ''' 	[Marcelo]	11/02/2010	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub BarcodeInWord(ByVal Word As Object, ByVal value As String, Optional ByVal save As Boolean = False, Optional ByVal Alignment As String = "Derecha", _
                                 Optional ByVal BarHeight As Int32 = 50)
        Dim Doc As Microsoft.Office.Interop.Word.Document = DirectCast(Word, Microsoft.Office.Interop.Word.Document)
        Dim strWord As String
        'Para cada indices numericos inserto el codigo de barra
        If Not String.IsNullOrEmpty(value) Then
            'valido si es numerico 
            If IsNumeric(value) = True Then
                If value.Length <= 10 Then
                    strWord = value
                    While strWord.Length <= 9
                        strWord = "0" & strWord
                    End While
                Else
                    strWord = value
                End If

                'Genera el barcode y lo guarda en un bitmap
                SaveBC(strWord, BarHeight)
                'Alinea el header
                Select Case Alignment
                    Case "Derecha"
                        Doc.Sections.Item(1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                    Case "Izquierda"
                        Doc.Sections.Item(1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    Case "Centro"
                        Doc.Sections.Item(1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
                End Select
                'Inserta la barcode en el documento
                Doc.InlineShapes.AddPicture(Application.StartupPath & "\TempBC.bmp", , True)
                If save = True Then
                    Doc.Save()
                End If
            End If
        End If
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Genera un codigo de barras y lo guarda de header de un documento de word
    ''' </summary>
    ''' <param name="Obj">Objeto del tipo wordDocument donde se va a insertar el barcode</param>
    ''' <param name="Value">Valor del barcode</param>
    ''' <history>
    ''' 	[Marcelo]	01/12/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub BarcodeWord(ByVal Word As Object, ByVal value As String, Optional ByVal save As Boolean = False, Optional ByVal Alignment As String = "Derecha", _
                                 Optional ByVal BarHeight As Int32 = 50)
        Dim Doc As Microsoft.Office.Interop.Word.Document = DirectCast(Word, Microsoft.Office.Interop.Word.Document)
        Dim strWord As String
        'Para cada indices numericos inserto el codigo de barra
        If Not String.IsNullOrEmpty(value) Then
            'valido si es numerico 
            If IsNumeric(value) = True Then
                If value.Length <= 10 Then
                    strWord = value
                    While strWord.Length <= 9
                        strWord = "0" & strWord
                    End While
                Else
                    strWord = value
                End If
                'Genera el barcode y lo guarda en un bitmap
                SaveBC(strWord, BarHeight)
                'Alinea el header
                Select Case Alignment
                    Case "Derecha"
                        Doc.Sections.Item(1).Headers.Item(Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range.ParagraphFormat.Alignment _
                       = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                    Case "Izquierda"
                        Doc.Sections.Item(1).Headers.Item(Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range.ParagraphFormat.Alignment _
                       = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                    Case "Centro"
                        Doc.Sections.Item(1).Headers.Item(Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range.ParagraphFormat.Alignment _
                         = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
                End Select
                'Inserta la barcode en el header del documento
                Doc.InlineShapes.AddPicture(Application.StartupPath & "\TempBC.bmp", , True, Doc.Sections.Item(1).Headers.Item(Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range)
                If save = True Then
                    Doc.Save()
                End If
            End If
        End If
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Genera un codigo de barras y lo pone de header de un nuevo documento de word
    ''' </summary>
    ''' <history>
    ''' 	[Marcelo]	01/12/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub NewBarcodeWord(ByVal Height As Object)
        Dim code As String = InputBox("Ingrese codigo numerico", "Word Barcode")
        Dim word As New Microsoft.Office.Interop.Word.Document
        BarcodeWord(word, code)
        word.Application.Visible = True
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Genera un codigo de barras y lo guarda en un Bitmap
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <history>
    ''' 	[Marcelo]	01/12/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SaveBC(ByVal Value As String, ByVal BarHeight As Int32)
        'Dim bc As New Zamba.Barcodes.BarCodeCtrl
        Dim bc As New PrintControl.PrintBarcodes
        Dim gr As Graphics
        Dim mr As Rectangle = New Rectangle()

        Dim bmp As New Bitmap(Value.Length * 21, 74)

        gr = Graphics.FromImage(bmp)
        Dim e As New System.Windows.Forms.PaintEventArgs(gr, mr)

        bc.BarCode = Value
        bc.headerText = ""
        bc.VertAlign = PrintControl.PrintBarcodes.AlignType.Left
        bc.leftMargin = 10
        bc.topMargin = 10
        bc.showFooter = True

        gr.Clear(Color.White)
        e = bc.PaintImage(e)

        bmp.Save(Application.StartupPath & "\TempBC.bmp", System.Drawing.Imaging.ImageFormat.Bmp)
        gr.Dispose()
    End Sub

    ''' <summary>
    ''' Reemplaza en un Word un texto por otro
    ''' </summary>
    ''' <param name="Word"></param>
    ''' <param name="text"></param>
    ''' <param name="replacement"></param>
    ''' <param name="Forward"></param>
    ''' <param name="Format"></param>
    ''' <param name="MatchCase"></param>
    ''' <param name="MatchWholeWord"></param>
    ''' <param name="MatchWildcards"></param>
    ''' <param name="MatchSoundsLike"></param>
    ''' <param name="MatchAllWordForms"></param>
    ''' <history>
    '''     Javier  25/10/2010  Created Pasado del Branch de Marsh
    '''</history>
    Public Shared Sub FindAndReplaceInWord(ByVal Word As Object, ByVal text As String, ByVal replacement As String, Optional ByVal Forward As Boolean = False, _
                                            Optional ByVal Format As Boolean = False, _
                                            Optional ByVal MatchCase As Boolean = False, Optional ByVal MatchWholeWord As Boolean = False, Optional ByVal MatchWildcards As Boolean = False, _
                                            Optional ByVal MatchSoundsLike As Boolean = False, Optional ByVal MatchAllWordForms As Boolean = False)

        Dim Doc As Microsoft.Office.Interop.Word.Document = DirectCast(Word, Microsoft.Office.Interop.Word.Document)

        'Doc.Content.Find.ClearFormatting()
        'Doc.Content.Find.Replacement.ClearFormatting()

        'Doc.Content.Find.Text = text
        'Doc.Content.Find.Replacement.Text = replacement
        'Doc.Content.Find.Forward = Forward
        'Doc.Content.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue
        'Doc.Content.Find.Format = Format
        'Doc.Content.Find.MatchCase = MatchCase
        'Doc.Content.Find.MatchWholeWord = MatchWholeWord
        'Doc.Content.Find.MatchWildcards = MatchWildcards
        'Doc.Content.Find.MatchSoundsLike = MatchSoundsLike
        'Doc.Content.Find.MatchAllWordForms = MatchAllWordForms

        Dim oText As Object = DirectCast(text, Object)
        Dim oReplacement As Object = DirectCast(replacement, Object)
        Dim oForward As Object = DirectCast(Forward, Object)
        Dim oWrap As Object = DirectCast(Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue, Object)
        Dim oFormat As Object = DirectCast(Format, Object)
        Dim oMatchCase As Object = DirectCast(MatchCase, Object)
        Dim oMatchWholeWord As Object = DirectCast(MatchWholeWord, Object)
        Dim oMatchWildcards As Object = DirectCast(MatchWildcards, Object)
        Dim oMatchSoundsLike As Object = DirectCast(MatchSoundsLike, Object)
        Dim oMatchAllWordForms As Object = DirectCast(MatchAllWordForms, Object)
        Dim owdReplaceAll As Object = DirectCast(Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll, Object)

        Doc.Content.Find.Execute(oText, oMatchCase, oMatchWholeWord, oMatchWildcards, oMatchSoundsLike, oMatchAllWordForms, oForward, oWrap, oFormat, oReplacement, owdReplaceAll, , , , )
        'Doc.Content.Find.Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)

        'Reemplazar en header y footer
        '(Sirve para todo el doc, pero se deja igualmente el replace de arriba qe ya está funcionando
        Dim rngStory As Word.Range
        Dim lngJunk As Long
        Dim oShp As Word.Shape

        lngJunk = Doc.Sections(1).Headers(1).Range.StoryType
        'Iterate through all story types in the current document
        For Each rngStory In Doc.StoryRanges
            'Iterate through all linked stories
            Do
                Try
                    rngStory.Find.Execute(oText, oMatchCase, oMatchWholeWord, oMatchWildcards, oMatchSoundsLike, oMatchAllWordForms, oForward, oWrap, oFormat, oReplacement, owdReplaceAll, , , , )
                    'On Error Resume Next
                    Select Case rngStory.StoryType
                        Case 6, 7, 8, 9, 10, 11
                            If rngStory.ShapeRange.Count > 0 Then
                                For Each oShp In rngStory.ShapeRange
                                    If oShp.TextFrame.HasText Then
                                        oShp.TextFrame.TextRange.Find.Execute(oText, oMatchCase, oMatchWholeWord, oMatchWildcards, oMatchSoundsLike, oMatchAllWordForms, oForward, oWrap, oFormat, oReplacement, owdReplaceAll, , , , )
                                    End If
                                Next
                            End If
                        Case Else
                            'Do Nothing
                    End Select
                    'On Error GoTo 0
                Catch ex As Exception
                End Try

                'Get next linked story (if any)
                rngStory = rngStory.NextStoryRange
            Loop Until rngStory Is Nothing
        Next

    End Sub

    ''' <summary>
    ''' Reemplaza en un word un texto por una tabla
    ''' </summary>
    ''' <param name="Word"></param>
    ''' <param name="text"></param>
    ''' <param name="tableinword"></param>
    ''' <param name="Forward"></param>
    ''' <param name="Format"></param>
    ''' <param name="MatchCase"></param>
    ''' <param name="MatchWholeWord"></param>
    ''' <param name="MatchWildcards"></param>
    ''' <param name="MatchSoundsLike"></param>
    ''' <param name="MatchAllWordForms"></param>
    ''' <history>
    '''     Javier  25/10/2010  Created Pasado del Branch de Marsh
    '''</history>
    Public Shared Sub FindAndReplaceInWord(ByVal Word As Object, ByVal text As String, ByVal tableinword As DataTable, Optional ByVal Forward As Boolean = False, _
                                            Optional ByVal Format As Boolean = False, _
                                            Optional ByVal MatchCase As Boolean = False, Optional ByVal MatchWholeWord As Boolean = False, Optional ByVal MatchWildcards As Boolean = False, _
                                            Optional ByVal MatchSoundsLike As Boolean = False, Optional ByVal MatchAllWordForms As Boolean = False)

        Dim Doc As Microsoft.Office.Interop.Word.Document = DirectCast(Word, Microsoft.Office.Interop.Word.Document)



        ' Número de columnas que tendrá la tabla.
        '
        Dim colsNumbers As Int32 = tableinword.Columns.Count

        ' Número de filas que tendrá la tabla, que será una
        ' fila más que las existentes en el objeto DataTable,
        ' porque la primera fila la utilizaremos para escribir
        ' el nombre de las columnas.
        ' 
        Dim rowsNumbers As Int32 = tableinword.Rows.Count + 1

        ' Definimos el área del documento de Word, donde crearemos
        ' la tabla. Al no existir todavía carácter alguno, tanto la
        ' posición de los caracteres inicial y final será cero.
        '
        Dim range As Word.Range = Doc.Range
        If range.Find.Execute(text) Then
            Dim table As Word.Table = Doc.Tables.Add(range, rowsNumbers, colsNumbers)

            ' Insertamos los encabezados de columna de la tabla, que
            ' se corresponderán con los nombres de los campos.
            '
            For col As Int32 = 1 To colsNumbers
                Dim cell As Word.Cell = table.Rows(1).Cells(col)
                cell.Range.Text = tableinword.Columns(col - 1).ColumnName
            Next

            ' Insertamos las filas en la tabla, comenzando por la
            ' fila número 2, ya que la primera fila está ocupada
            ' por el nombre de las columnas.
            '
            For row As Int32 = 2 To table.Rows.Count
                Dim c As Int32 = 0
                For Each cell As Word.Cell In table.Rows(row).Cells
                    ' Insertamos el valor de las celdas.
                    cell.Range.Text = tableinword.Rows(row - 2).Item(c).ToString
                    c += 1
                Next
            Next

            table.AutoFitBehavior(Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitContent)

            Dim oText As Object = DirectCast(text, Object)
            Dim oReplacement As Object = DirectCast(String.Empty, Object)
            Dim oForward As Object = DirectCast(Forward, Object)
            Dim oWrap As Object = DirectCast(Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue, Object)
            Dim oFormat As Object = DirectCast(Format, Object)
            Dim oMatchCase As Object = DirectCast(MatchCase, Object)
            Dim oMatchWholeWord As Object = DirectCast(MatchWholeWord, Object)
            Dim oMatchWildcards As Object = DirectCast(MatchWildcards, Object)
            Dim oMatchSoundsLike As Object = DirectCast(MatchSoundsLike, Object)
            Dim oMatchAllWordForms As Object = DirectCast(MatchAllWordForms, Object)
            Dim owdReplaceAll As Object = DirectCast(Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll, Object)

            Doc.Content.Find.Execute(oText, oMatchCase, oMatchWholeWord, oMatchWildcards, oMatchSoundsLike, oMatchAllWordForms, oForward, oWrap, oFormat, oReplacement, owdReplaceAll, , , , )
            'Doc.Content.Find.Execute(Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll)

            'Reemplazar en header y footer
            '(Sirve para todo el doc, pero se deja igualmente el replace de arriba qe ya está funcionando
            Dim rngStory As Word.Range
            Dim lngJunk As Long
            Dim oShp As Word.Shape

            lngJunk = Doc.Sections(1).Headers(1).Range.StoryType
            'Iterate through all story types in the current document
            For Each rngStory In Doc.StoryRanges
                'Iterate through all linked stories
                Do
                    Try
                        rngStory.Find.Execute(oText, oMatchCase, oMatchWholeWord, oMatchWildcards, oMatchSoundsLike, oMatchAllWordForms, oForward, oWrap, oFormat, oReplacement, owdReplaceAll, , , , )
                        'On Error Resume Next
                        Select Case rngStory.StoryType
                            Case 6, 7, 8, 9, 10, 11
                                If rngStory.ShapeRange.Count > 0 Then
                                    For Each oShp In rngStory.ShapeRange
                                        If oShp.TextFrame.HasText Then
                                            oShp.TextFrame.TextRange.Find.Execute(oText, oMatchCase, oMatchWholeWord, oMatchWildcards, oMatchSoundsLike, oMatchAllWordForms, oForward, oWrap, oFormat, oReplacement, owdReplaceAll, , , , )
                                        End If
                                    Next
                                End If
                            Case Else
                                'Do Nothing
                        End Select
                        'On Error GoTo 0
                    Catch ex As Exception
                    End Try

                    'Get next linked story (if any)
                    rngStory = rngStory.NextStoryRange
                Loop Until rngStory Is Nothing
            Next

        End If
    End Sub

    ''' <summary>
    '''     Coloca un código de barras en la parte superior de un word, con formato libre para poder moverse por el texto
    ''' </summary>
    ''' <param name="Word"></param>
    ''' <param name="value"></param>
    ''' <param name="save"></param>
    ''' <param name="Alignment"></param>
    ''' <param name="BarHeight"></param>
    ''' <param name="top"></param>
    ''' <param name="left"></param>
    ''' <history>
    '''     Javier  25/10/2010  Created Pasado del Branch de Marsh
    '''</history>
    Public Shared Sub BarcodeInWordTopImage(ByVal Word As Object, ByVal value As String, Optional ByVal save As Boolean = False, Optional ByVal Alignment As String = "Derecha", _
                                 Optional ByVal BarHeight As Int32 = 20, Optional ByVal top As Int64 = -300, Optional ByVal left As Int64 = -300)
        Dim Doc As Microsoft.Office.Interop.Word.Document = DirectCast(Word, Microsoft.Office.Interop.Word.Document)
        Dim strWord As String
        'Para cada indices numericos inserto el codigo de barra
        If Not String.IsNullOrEmpty(value) Then
            'valido si es numerico 
            If IsNumeric(value) = True Then
                If value.Length <= 10 Then
                    strWord = value
                    While strWord.Length <= 9
                        strWord = "0" & strWord
                    End While
                Else
                    strWord = value
                End If

                'Genera el barcode y lo guarda en un bitmap
                SaveBC(strWord, BarHeight)
                'Alinea el header




                'Select Case Alignment
                '    Case "Derecha"
                '        Doc.Sections.Item(1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight
                '    Case "Izquierda"
                '        Doc.Sections.Item(1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                '    Case "Centro"
                '        Doc.Sections.Item(1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
                'End Select
                'Inserta la barcode en el documento
                Doc.Shapes.AddPicture(Application.StartupPath & "\TempBC.bmp", , True)

                Dim cantidad As Integer = Doc.Shapes.Count
                Doc.Shapes(cantidad).WrapFormat.Type = Microsoft.Office.Interop.Word.WdWrapType.wdWrapNone

                Doc.Shapes(cantidad).RelativeVerticalPosition = Microsoft.Office.Interop.Word.WdRelativeVerticalPosition.wdRelativeVerticalPositionPage
                If top = -300 Then
                    Doc.Shapes(cantidad).Top = -7
                Else
                    Doc.Shapes(cantidad).Top = top
                End If

                'Obtiene ancho de la página para colocar el barcode al medio
                Dim tamanioPagina As Integer = Doc.PageSetup.PageWidth
                Doc.Shapes(cantidad).RelativeHorizontalPosition = Microsoft.Office.Interop.Word.WdRelativeHorizontalPosition.wdRelativeHorizontalPositionPage

                If left = -300 Then
                    Doc.Shapes(cantidad).Left = (tamanioPagina / 2) - (Doc.Shapes(cantidad).Width / 2)
                Else
                    Doc.Shapes(cantidad).Left = left
                End If

                If save = True Then
                    Doc.Save()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    '''     Guarda un documento office
    ''' </summary>
    ''' <param name="docobj"></param>
    ''' <history>
    '''     Javier  02/10/2010  Created 
    '''</history>
    Public Shared Sub SaveDoc(ByVal docobj As Object)
        Dim Doc As Microsoft.Office.Interop.Word.Document = DirectCast(docobj, Microsoft.Office.Interop.Word.Document)
        Doc.Save()
    End Sub
End Class
