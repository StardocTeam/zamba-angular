Imports System.IO

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim OFD As New OpenFileDialog
        OFD.ShowDialog()
        GenerarArchivo(OFD.FileName)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
    Public Sub GenerarArchivo(Archivo As String)
        Try

            Dim Texto As String
            Using sr As StreamReader = New StreamReader(Archivo)
                Texto = sr.ReadToEnd()
            End Using
            Dim SepararTexto() As String
            SepararTexto = Split(Texto, "style=")
            Dim PrimerLinea As Boolean = True
            Dim ClasesAcumuladas As String = String.Empty
            Dim ContadorClases As Int64
            Dim UnirTexto As String = String.Empty
            For Each parte As String In SepararTexto
                If PrimerLinea Then
                    UnirTexto = parte
                    PrimerLinea = False
                Else
                    Dim SepararLinea() As String
                    Dim UnirLinea As String
                    SepararLinea = Split(parte, """")
                    SepararLinea(0) = "class="
                    Dim NuevaClase As String
                    ContadorClases += 1
                    Dim NombreClase As String = Archivo.Replace(" ", "").Replace("-", "").Replace("_", "") + ContadorClases.ToString
                    NuevaClase = "." + NombreClase + "{" + SepararLinea(1) + "}" + vbCrLf
                    ClasesAcumuladas += NuevaClase
                    SepararLinea(1) = NombreClase
                    UnirLinea = Join(SepararLinea, """")
                    UnirTexto += UnirLinea
                End If
            Next
            Dim Estilos As String = "<style nonce=""c3RhcmRvYzIwMjE="">" + vbCrLf
            Estilos += ClasesAcumuladas + vbCrLf
            Estilos += "</style>"
            UnirTexto += vbCrLf + Estilos
            TextBox2.Text = UnirTexto
            Dim file As System.IO.StreamWriter
            file = My.Computer.FileSystem.OpenTextFileWriter(Archivo.Replace(".", "_css."), False)
            file.Write(UnirTexto)
            file.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub
End Class
