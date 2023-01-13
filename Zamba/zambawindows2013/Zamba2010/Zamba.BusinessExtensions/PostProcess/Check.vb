Public Class Check
    Implements IPPostProcess

#Region "XML"
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements IPPostProcess.GetXml
        Return Nothing
    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements IPPostProcess.SetXml

    End Sub
#End Region

#Region "Eventos"
    Public Event PreprocessError(ByVal Errormsg As String) Implements IPPostProcess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements IPPostProcess.PreprocessMessage
#End Region

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements IPPostProcess.process
        
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements IPPostProcess.processFile
        Dim sr As IO.StreamReader = New IO.StreamReader(File)
        Dim linea As String = ""
        Dim Ruta As String = 0
        Dim cantArchivos As Int32 = 0

        While sr.Peek <> -1
            linea = sr.ReadLine
            Ruta = linea.Split("|")(Ruta)
            cantArchivos += Ruta.Split(",").Length
        End While
    End Function

    Public Function GetHelp() As String Implements IPPostProcess.GetHelp
        Return String.Empty
    End Function
End Class
