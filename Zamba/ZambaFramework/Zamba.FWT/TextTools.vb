Namespace Tools
    Public Class TextTools
        Public Shared Function ReemplazarAcentos(texto As String) As String
            texto = texto.Replace("á", "a")
            texto = texto.Replace("é", "e")
            texto = texto.Replace("í", "i")
            texto = texto.Replace("ó", "o")
            texto = texto.Replace("ú", "u")
            texto = texto.Replace("Á", "A")
            texto = texto.Replace("É", "E")
            texto = texto.Replace("Í", "I")
            texto = texto.Replace("Ó", "O")
            texto = texto.Replace("Ú", "U")

            Return texto
        End Function
    End Class
End Namespace