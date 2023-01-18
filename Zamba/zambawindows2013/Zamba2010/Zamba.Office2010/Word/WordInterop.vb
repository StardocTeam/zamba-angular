Imports Microsoft.Office.Interop
Imports Zamba.Core

Public Class WordInterop

    Private vFalseValue As Object = False
    Private vTrueValue As Object = True
    Private vMissing As Object = Type.Missing
    Private vFileName As Object

    Public Sub Activate(ByVal word As Object)
        Dim WordDoc As Microsoft.Office.Interop.Word.Document = DirectCast(word, Microsoft.Office.Interop.Word.Document)
        WordDoc.Activate()
    End Sub

    Public Function Print(ByVal Doc As String, ByVal PrinterName As String) As Boolean
        Return doPrint(Doc, PrinterName)
    End Function

    Public Function PrintToDefaultPrinter(ByVal Doc As String) As Boolean
        Return doPrint(Doc, String.Empty)
    End Function

    Private Function doPrint(ByVal Doc As String, Optional ByVal PrinterName As String = "") As Boolean

        Dim Wordapp As Word.Application = Nothing
        Dim WordDoc As Word.Document = Nothing

        Dim UserPrinter As String = String.Empty
        Dim Resul As Boolean = False

        Try

            ZTrace.WriteLineIf(ZTrace.IsError, "WordInterop: ejecutando doPrint para el documento: " & Doc)
            ZTrace.WriteLineIf(ZTrace.IsError, "WordInterop: Instanciando objetos doc y word")

            Wordapp = New Word.Application()
            WordDoc = New Word.Document()

            vFileName = Doc.Trim()

            'Si se especifica una impresora guardar la actual, esta asignacion cambia la default del SO
            If Not String.IsNullOrEmpty(PrinterName) Then
                ZTrace.WriteLineIf(ZTrace.IsError, "WordInterop: Estableciendo impresora: " & PrinterName.Trim)
                UserPrinter = Wordapp.ActivePrinter
                Wordapp.ActivePrinter = PrinterName.Trim
                System.Threading.Thread.Sleep(1000)
            End If

            ZTrace.WriteLineIf(ZTrace.IsError, "WordInterop: Abriendo .doc")
            WordDoc = Wordapp.Documents.Open(vFileName, vFalseValue, vTrueValue)

            Wordapp.Visible = False
            WordDoc.Activate()

            ZTrace.WriteLineIf(ZTrace.IsError, "WordInterop: Imprimiendo .doc")
            WordDoc.PrintOut(vFalseValue)

            'Si se cambio la impresora recuperar la del usuario
            If Not String.IsNullOrEmpty(UserPrinter) Then
                ZTrace.WriteLineIf(ZTrace.IsError, "WordInterop: Recuperando impresora: " & UserPrinter.Trim)
                Wordapp.ActivePrinter = UserPrinter
                System.Threading.Thread.Sleep(500)
            End If

            Resul = True

        Catch ex As Exception

            ZTrace.WriteLineIf(ZTrace.IsError, "WordInterop Exception: " & ex.ToString())

        Finally

            ZTrace.WriteLineIf(ZTrace.IsError, "WordInterop: Cerrando doc y word")
            WordDoc.Close(vFalseValue)
            Wordapp.Quit(vFalseValue)

            WordDoc = Nothing
            Wordapp = Nothing

            GC.Collect()

        End Try

        Return Resul

    End Function


    ''' <summary>
    '''     Imprime un documento de word a traves de la API de Microsoft y con el worddocument por referencia
    ''' </summary>
    ''' <param name="Doc"></param>
    ''' <param name="app"></param>
    ''' <param name="PrinterName"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  28/10/2010  Created
    '''</history>
    Public Function PrintWithWord(ByVal Doc As Object, ByVal app As Object, Optional ByVal PrinterName As String = "") As Boolean
        ZTrace.WriteLineIf(ZTrace.IsError, "WordInterop: PrintWithWord")
        Dim WordDoc As Microsoft.Office.Interop.Word.Document = DirectCast(Doc, Microsoft.Office.Interop.Word.Document)
        Dim Wordapp As Microsoft.Office.Interop.Word.Application = DirectCast(app, Microsoft.Office.Interop.Word.Application)

        Dim UserPrinter As String = String.Empty
        Dim Resul As Boolean = False

        Try


            'Si se especifica una impresora guardar la actual, esta asignacion cambia la default del SO
            If Not String.IsNullOrEmpty(PrinterName) Then
                ZTrace.WriteLineIf(ZTrace.IsError, "WordInterop: Estableciendo impresora: " & PrinterName.Trim)
                UserPrinter = Wordapp.ActivePrinter
                Wordapp.ActivePrinter = PrinterName.Trim
                System.Threading.Thread.Sleep(500)
            End If

            ZTrace.WriteLineIf(ZTrace.IsError, "WordInterop: Imprimiendo .doc")
            WordDoc.PrintOut(vFalseValue)

            'Si se cambio la impresora recuperar la del usuario
            If Not String.IsNullOrEmpty(UserPrinter) Then
                ZTrace.WriteLineIf(ZTrace.IsError, "WordInterop: Recuperando impresora: " & UserPrinter.Trim)
                Wordapp.ActivePrinter = UserPrinter
                System.Threading.Thread.Sleep(500)
            End If

            Resul = True

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, "WordInterop Exception: " & ex.ToString())
        End Try

        Return Resul
    End Function

    ''' <summary>
    '''     Devuelve todo el texto de un documento de Word
    ''' </summary>
    ''' <param name="worddoc"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  02/11/2010  Created
    '''</history>
    Public Function GetAllText(ByVal docobj As Object) As String
        Dim texto As String = String.Empty
        Dim worddoc As Microsoft.Office.Interop.Word.Document = DirectCast(docobj, Microsoft.Office.Interop.Word.Document)
        If worddoc.Sections.Count > 0 Then
            For i As Integer = 1 To worddoc.Sections.Count
                texto = texto + worddoc.Sections(i).Range.Text
            Next
        End If
        Return texto
    End Function
End Class

