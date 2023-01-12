Imports Zamba.Core
<Ipreprocess.PreProcessName("Backup de Mails"), Ipreprocess.PreProcessHelp("Con Tres parametros separados por comas sin espacios, 1-Path Destino,2-Nombre del Proceso,3-Fecha del proceso. Realiza el BackUp del archivo de indices y sus archivos")> _
Public Class ippBackupMails
    Implements IDisposable
    Implements Ipreprocess
#Region "Esta clase hace..."
    ' clase copiada de BackupFiles .... para usar con los mails .. 
    'por ahora es igual a BackupFiles.vb
#End Region
    Private Sub BackupFiles(ByVal path As String, ByVal DestNameDate As String)
        'Realiza un backup de todos los archivos existentes antes de correr el proceso de importación
        Dim FoOrigen As New IO.DirectoryInfo(path)
        Dim PathDestino As String = DestNameDate.Split(",")(0) & "\Process " & DestNameDate.Split(",")(1) & " " & DestNameDate.Split(",")(2)
        Dim foDestino As New IO.DirectoryInfo(PathDestino)
        If Not foDestino.Exists Then
            foDestino.Create()
        End If
        Dim files() As IO.FileInfo = FoOrigen.GetFiles
        Dim file As IO.FileInfo

        Dim Count As Int32 = files.Length
        RaiseEvent PreprocessMessage("BackUp: Directorio Origen: " & FoOrigen.FullName)
        RaiseEvent PreprocessMessage("BackUp: Directorio Destino: " & foDestino.FullName)
        RaiseEvent PreprocessMessage("BackUp: Filas a copiar: " & Count)
        For Each file In files
            Count += -1
            file.CopyTo(foDestino.FullName + "\" + file.Name, True)
            If Count Mod 30 = 0 Then
                RaiseEvent PreprocessMessage("Filas restantes a copiar: " & Count)
            End If
        Next
        RaiseEvent PreprocessMessage("BackUp: Filas copiadas.")
    End Sub
    'Private Sub BackupFilesAndFolders(ByVal pathorigen As String, ByVal pathdestino As String)
    '    Dim FoOrigen As New IO.DirectoryInfo(pathorigen)
    '    Dim foDestino As New IO.DirectoryInfo(pathdestino)
    '    'Dim nuevopath As String = path & "\Backup " & Now.Date.ToString
    '    If Not foDestino.Exists Then
    '        foDestino.Create()
    '    End If
    '    Dim files() As IO.FileInfo = FoOrigen.GetFiles
    '    Dim file As IO.FileInfo

    '    For Each file In files
    '        file.CopyTo(foDestino.FullName + "\" + file.Name)
    '        file.Directory.GetDirectories.Copy(FoOrigen.GetDirectories, foDestino.GetDirectories, FoOrigen.GetDirectories.Length)
    '    Next
    'End Sub

    Public Sub Dispose() Implements System.IDisposable.Dispose

    End Sub

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return ""
    End Function

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int32
        For i = 0 To Files.Count - 1
            processFile(Files(i), param(0))
        Next
        Return Files
    End Function

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
        Return "Con Tres parametros separados por comas sin espacios, 1-Path Destino,2-Nombre del Proceso,3-Fecha del proceso. Realiza el BackUp del archivo de indices y sus archivos"
    End Function

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
End Class
