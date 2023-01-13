Imports Microsoft.Office.Interop

Public Class WordInterop

    Private vFalseValue As Object = False
    Private vTrueValue As Object = True
    Private vMissing As Object = Type.Missing
    Private vFileName As Object

    Public Function Print(ByVal Doc As String, ByVal PrinterName As String) As Boolean
        Return doPrint(Doc, PrinterName)
    End Function

    Public Function PrintToDefaultPrinter(ByVal Doc As String) As Boolean
        Return doPrint(Doc, "")
    End Function

    Private Function doPrint(ByVal Doc As String, Optional ByVal PrinterName As String = "") As Boolean

        Dim Wordapp As Word.Application = Nothing
        Dim WordDoc As Word.Document = Nothing

        Dim UserPrinter As String = String.Empty
        Dim Resul As Boolean = False

        Try

            Trace.WriteLine("WordInterop: ejecutando doPrint para el documento: " & Doc)
            Trace.WriteLine("WordInterop: Instanciando objetos doc y word")

            Wordapp = New Word.Application()
            WordDoc = New Word.Document()

            vFileName = Doc.Trim()

            'Si se especifica una impresora guardar la actual, esta asignacion cambia la default del SO
            If Not String.IsNullOrEmpty(PrinterName) Then
                Trace.WriteLine("WordInterop: Estableciendo impresora: " & PrinterName.Trim)
                UserPrinter = Wordapp.ActivePrinter
                Wordapp.ActivePrinter = PrinterName.Trim
                System.Threading.Thread.Sleep(1000)
            End If

            Trace.WriteLine("WordInterop: Abriendo .doc")
            WordDoc = Wordapp.Documents.Open(vFileName, vFalseValue, vTrueValue)

            Wordapp.Visible = False
            WordDoc.Activate()

            Trace.WriteLine("WordInterop: Imprimiendo .doc")
            WordDoc.PrintOut(vFalseValue)

            'Si se cambio la impresora recuperar la del usuario
            If Not String.IsNullOrEmpty(UserPrinter) Then
                Trace.WriteLine("WordInterop: Recuperando impresora: " & UserPrinter.Trim)
                Wordapp.ActivePrinter = UserPrinter
                System.Threading.Thread.Sleep(500)
            End If

            Resul = True

        Catch ex As Exception

            Trace.WriteLine("WordInterop Exception: " & ex.ToString())

        Finally

            Trace.WriteLine("WordInterop: Cerrando doc y word")
            WordDoc.Close(vFalseValue)
            Wordapp.Quit(vFalseValue)

            WordDoc = Nothing
            Wordapp = Nothing

            GC.Collect()

        End Try

        Return Resul

    End Function

End Class

