Imports ZAMBA.Servers
Imports Zamba.Core
<Ipreprocess.PreProcessName("Actualizar Control"), Ipreprocess.PreProcessHelp("Preproceso para realizar actualizaciones en la base de datos")> _
Public Class ippUpdateControl
    Implements Ipreprocess
#Region "HELP"
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Preproceso para realizar actualizaciones en la base de datos"
    End Function
#End Region
#Region "XML"
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml
    End Sub
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implementar
        Return String.Empty
    End Function
#End Region
#Region "Eventos"
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
#End Region
    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int16
        For i = 0 To Files.Count - 1
            processFile(Files(i), param(i))
        Next
        Return Files
    End Function
    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile

        Dim claves As New Hashtable()
        Dim columnasDevueltas As New Hashtable()
        Dim id As Int32
        Dim tabla As String = String.Empty




        Dim parametros() As String = param.Split(",")
        Dim sr As New IO.StreamReader(File)
        Dim linea As String

        'Dim x As New CUpdate(parametros(0))
        'se cambió por:
        DataBaseAccessBusiness.UCUpdate.LoadConsultas(claves, columnasDevueltas, tabla, id)


        Dim campos() As String
        '  Dim car As Char
        Dim valores As New ArrayList
        Dim where As New ArrayList
        Dim i As Int16
        While sr.Peek <> -1
            linea = sr.ReadLine
            campos = linea.Split("|"c)
            For i = 1 To claves.Count
                'los primeros x parametros son para los valores
                valores.Add(campos(CInt(parametros(i))))
            Next
            For i = claves.Count + 1 To parametros.Length - 1
                'los y restantes son para el where
                where.Add(campos(parametros(i)))
            Next
            'Prueba para importar
            'Dim str As String = x.UpdateString(valores, where)
            'se cambió por:
            Dim str As String = DataBaseAccessBusiness.UCUpdate.UpdateString(valores, where, claves, columnasDevueltas, tabla, id)
            Server.Con.ExecuteNonQuery(CommandType.Text, str)
            valores.Clear()
            where.Clear()
        End While
        sr.Close()
        Return String.Empty
    End Function
End Class
