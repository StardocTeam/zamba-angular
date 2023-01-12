Imports Zamba.Core
<Ipreprocess.PreProcessName("Dividir Mails"), Ipreprocess.PreProcessHelp("Divide el archivo de índices de Mails en MaestroPriv.txt en el mismo directorio y  los Mails Publicos en MaestroPublic.txt. Recibe como parametro el directorio del Maestro public.")> _
Public Class ippSplitMails
    Implements Ipreprocess

    Public Event LogError(ByVal ex As Exception)
#Region "Help"
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Divide el archivo de índices de Mails en MaestroPriv.txt en el mismo directorio y  los Mails Publicos en MaestroPublic.txt. Recibe como parametro el directorio del Maestro public."
    End Function
    Public Shared Function Help() As String
        Return "NOTA: Este preproceso divide el archivo Maestro obtenido " _
& "de la exportación de Lotus Notes y lo divide en dos  " _
 & "archivos, uno con los datos privados del usuario y otro con " _
  & "los datos públicos."

        Console.WriteLine("Sintaxis: ")
        Console.WriteLine("PPIndexMaster.exe [Directorio de Origen] [Directorio Destino]")
        '        WriteLine()
        WriteLine("Este preproceso divide el archivo enviado en dos archivos, denominados C:\MaestroPublic.txt y C:\MaestroPriv.txt, en los cuales se almacenará la")
        WriteLine("información pública y privada respectivamente.")
        WriteLine("Este preproceso se utiliza con el archivo Maestro.txt, el cual es generado en la exportación de Lotus Notes")
        '       ReadLine()

    End Function
#End Region
#Region "Variables Globales"
    Dim PublicCount As Int32
    Dim PrivateCount As Int32
    Dim DirPrivate As IO.DirectoryInfo
    Dim DirPublic As IO.DirectoryInfo
    Dim FiPublic As IO.FileInfo
    Dim FiPrivate As IO.FileInfo
#End Region

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Try

            Dim i As Int32
            For i = 0 To Files.Count - 1
                'fi = Files.Item(i)
                Dim fi As New IO.FileInfo(Files.Item(i))
                'fi = New IO.FileInfo(Files.Item(i))

                'Variables de Destino
                If fi.Exists Then
                    DirPrivate = New IO.DirectoryInfo(fi.Directory.FullName)
                    DirPublic = New IO.DirectoryInfo(param(0))
                    FiPublic = New IO.FileInfo(DirPublic.FullName & "\MaestroPublic.txt")
                    FiPrivate = New IO.FileInfo(DirPrivate.FullName & "\MaestroPriv.txt")
                    DivFile(fi)
                End If
            Next
            Dim Array As New ArrayList
            Array.Add(FiPrivate.FullName)
            Return Array
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Return Nothing
    End Function
    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Dim fi As IO.FileInfo
        File.Trim("'")
        fi = New IO.FileInfo(File)
        'Variables de Destino
        If fi.Exists Then
            DirPrivate = New IO.DirectoryInfo(fi.Directory.FullName)
            DirPublic = New IO.DirectoryInfo(fi.Directory.Parent.FullName)
            FiPublic = New IO.FileInfo(DirPublic.FullName & "\MaestroPublic.txt")
            FiPrivate = New IO.FileInfo(DirPrivate.FullName & "\MaestroPriv.txt")
            DivFile(fi)
        End If
        Return String.Empty
    End Function
    Private Sub DivFile(ByVal FiOrigen As IO.FileInfo)
        Try
            Dim WFiPrivate As New IO.StreamWriter(FiPrivate.FullName, True, System.Text.Encoding.Default)
            Dim WFiPublic As New IO.StreamWriter(FiPublic.FullName, True, System.Text.Encoding.Default)
            WFiPrivate.AutoFlush = True
            WFiPublic.AutoFlush = True
            Dim RFiOrigen As New IO.StreamReader(FiOrigen.Directory.FullName & "\maestro.txt", System.Text.Encoding.Default)
            'Dim RFiOrigen As New IO.StreamReader("\\arbuedf01\mails\retamof\maestro.txt", System.Text.Encoding.Default)
            Dim linea As String
            Dim Campos() As String
            Dim privado As Boolean = True
            Dim i As Int32
            RaiseEvent PreprocessMessage("Dividiendo Mails Publicos y Privados: " & FiOrigen.FullName)
            'Dim c As Int16 'para trace, borrarme despues del debuggueo 
            While RFiOrigen.Peek <> -1
                privado = True
                linea = RFiOrigen.ReadLine
                Campos = linea.Split("|")
                'Trace.WriteLineIf(ZTrace.IsInfo,"separando linea: " & c)
                'c += 1
                For i = 11 To Campos.Length - 1
                    If Campos(i).ToString.Trim <> String.Empty Then
                        privado = False
                        Exit For
                    End If
                Next
                If privado Then
                    WFiPrivate.WriteLine(linea)
                    PrivateCount += 1
                Else
                    WFiPublic.WriteLine(linea)
                    PublicCount += 1
                End If
            End While
            WFiPrivate.Close()
            WFiPublic.Close()
            RFiOrigen.Close()
            WFiPrivate = Nothing
            WFiPublic = Nothing
            RFiOrigen = Nothing
            GC.Collect()
            RaiseEvent PreprocessMessage("Mail Privados: " & PrivateCount)
            RaiseEvent PreprocessMessage("Mail Publicos: " & PublicCount)
        Catch ex As Exception
            RaiseEvent PreprocessMessage("Error Dividiendo Mails Publicos y Privados: " & FiOrigen.FullName & ". " & ex.ToString)
            Throw ex
        End Try
        Trace.Unindent()
    End Sub
#Region "XML"
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml
    End Sub
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implemetar
        Return String.Empty
    End Function
#End Region
#Region "Eventos"
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
#End Region

End Class
