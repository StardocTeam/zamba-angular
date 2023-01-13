Imports Zamba.Core
<Ipreprocess.PreProcessName("Realizar Backup"), Ipreprocess.PreProcessHelp("Con Tres parametros separados por comas sin espacios, 1-Path Destino,2-Nombre del Proceso,3-Fecha del proceso. Realiza el BackUp del archivo de atributos y sus archivos")> _
Public Class ippBackupFiles
    Implements IDisposable
    Implements Ipreprocess

    Private Sub BackupFiles(ByVal path As String, ByVal DestNameDate As String)
        'Realiza un backup de todos los archivos existentes antes de correr el proceso de importación
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Realizando BackUp del archivo")
        Dim FoOrigen As New IO.DirectoryInfo(path)
        Dim PathDestino As String = DestNameDate.Split(",")(0) & "\Process " & DestNameDate.Split(",")(1) & " " & DestNameDate.Split(",")(2)
        Dim foDestino As New IO.DirectoryInfo(PathDestino)
        If Not foDestino.Exists Then
            foDestino.Create()
        End If
        'Dim con As Int32 = 0
        Dim files() As IO.FileInfo = FoOrigen.GetFiles
        Dim file As IO.FileInfo

        Dim Count As Int32 = files.Length
        RaiseEvent PreprocessMessage("BackUp: Directorio Origen: " & FoOrigen.FullName)
        RaiseEvent PreprocessMessage("BackUp: Directorio Destino: " & foDestino.FullName)
        RaiseEvent PreprocessMessage("BackUp: Archivos a copiar: " & Count)
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "BackUp: Directorio Origen: " & FoOrigen.FullName)
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "BackUp: Directorio Destino: " & foDestino.FullName)
        For Each file In files
            Count += -1
            ZTrace.WriteLineIf(ZTrace.IsInfo, "BackUp: " & file.Name)
            Try
                file.CopyTo(foDestino.FullName + "\" + file.Name, True)
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsError, "No se pudo realizar Backup")
                If (ex.Message.Contains("The process cannot access the file") = False) Then
                    ZClass.raiseerror(ex)
                End If
            End Try
            If Count Mod 30 = 0 Then
                RaiseEvent PreprocessMessage("Archivos restantes a copiar: " & Count)
            End If
        Next
        RaiseEvent PreprocessMessage("BackUp: Archivos copiados.")
    End Sub
    Public Sub Dispose() Implements System.IDisposable.Dispose
    End Sub

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return xml
    End Function

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int32
        Dim sbParam As New System.Text.StringBuilder

        For i = 0 To param.Count - 1
            sbParam.Append(param(i))
            sbParam.Append(",")
        Next

        For i = 0 To Files.Count - 1
            Try
                processFile(Files(i), sbParam.ToString)
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Files(i) = Nothing
            End Try
        Next
        Return Files
    End Function
    ''' <summary>
    ''' Copia todos los archivos del directorio de origen a un backup
    ''' </summary>
    ''' <param name="File"></param>
    ''' <param name="param"></param>
    ''' <param name="xml"></param>
    ''' <param name="Test"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Dim fileinf As New IO.FileInfo(File)
        RaiseEvent PreprocessMessage("Realizando Backup del archivo " & File)
        BackupFiles(fileinf.Directory.FullName, param)
        RaiseEvent PreprocessMessage("Backup finalizado")
        Return File
    End Function

    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml
    End Sub

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Con Tres parametros separados por comas sin espacios, 1-Path Destino,2-Nombre del Proceso,3-Fecha del proceso. Realiza el BackUp del archivo de atributos y sus archivos"
    End Function

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
End Class