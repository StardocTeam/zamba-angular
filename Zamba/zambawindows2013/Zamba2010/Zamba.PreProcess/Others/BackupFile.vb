Imports Zamba.Core
<Ipreprocess.PreProcessName("Backup del Archivo"), Ipreprocess.PreProcessHelp("Realiza un backup del archivo en la carpeta enviada como parámetro, en caso contrario la guarda en la carpeta de backup default")> _
Public Class ippBackupFile
    Implements Ipreprocess

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage


    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Realiza un backup del archivo en la carpeta enviada como parámetro, en caso contrario la guarda en la carpeta de backup default"
    End Function

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implentar
        Return String.Empty
    End Function


    Private Const MAXIMODEINTENTOS As Integer = 1000


    Private BackUpFolder As String
    Private Sub SetFolder(ByRef fm As ArrayList, ByVal param As ArrayList)
        If param Is Nothing OrElse param(0).Trim = "" Then
            BackUpFolder = FM(1)
        Else
            BackUpFolder = param(0)
        End If
        If Not IO.Directory.Exists(BackUpFolder) Then
            IO.Directory.CreateDirectory(BackUpFolder)
        End If
        If BackUpFolder.LastIndexOf("\") <> BackUpFolder.Length - 1 AndAlso BackUpFolder.LastIndexOf("\") Then
            'FM(1) = Me.BackUpFolder & "\"
            FM.Add(BackUpFolder & "\")
        Else
            FM.Add(BackUpFolder)
        End If

    End Sub
    Private Sub SetFolder(ByVal Origen As String, ByVal param As String)
        BackUpFolder = param
        If Not IO.Directory.Exists(BackUpFolder) Then
            IO.Directory.CreateDirectory(BackUpFolder)
        End If
        If BackUpFolder.LastIndexOf("\") <> BackUpFolder.Length - 1 AndAlso BackUpFolder.LastIndexOf("\") Then
            BackUpFolder = BackUpFolder & "\"
        End If
    End Sub

    ''' <summary>
    ''' Se copia el archivo
    ''' </summary>
    ''' <param name="Files"></param>
    ''' <param name="param"></param>
    ''' <param name="xml"></param>
    ''' <param name="Test"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function process(ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As ArrayList Implements Ipreprocess.process
        If Files(0) <> Nothing Then
            Dim targetFile As String
            SetFolder(Files, param)
            Dim fi As New IO.FileInfo(Files(0))
            targetFile = Files(1) & fi.Name


            Try
                Trace.WriteLineIf(ZTrace.IsInfo, "Copiando archivo")
                Trace.WriteLineIf(ZTrace.IsInfo, "Original: " & Files(0))
                Trace.WriteLineIf(ZTrace.IsInfo, "Destino: " & targetFile)
                If IO.File.Exists(targetFile) = True Then
                    TryToCopy(Files(0), Files(1))
                Else
                    IO.File.Copy(Files(0), targetFile)
                End If
                Trace.WriteLineIf(ZTrace.IsInfo, "Archivo copiado")
            Catch
                Try
                    TryToCopy(Files(0), Files(1))
                    Trace.WriteLineIf(ZTrace.IsInfo, "Archivo copiado")
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    Return Nothing
                End Try
            End Try
        End If
        Return Files
    End Function

    ''' <summary>
    ''' Si no se puede copiar el archivo, lo renombra
    ''' </summary>
    ''' <param name="origen"></param>
    ''' <param name="PathDestino"></param>
    ''' <remarks></remarks>
    Private Shared Sub TryToCopy(ByVal origen As String, ByVal PathDestino As String)
        Dim i As Integer = 1
        Dim flag As Boolean = True

        While flag
            Try
                Dim fi As New IO.FileInfo(origen)
                Dim dest As String
                If fi.Name.IndexOf(".") = -1 Then
                    dest = PathDestino & fi.Name & i.ToString("d3") & "_" & fi.Extension
                Else
                    dest = PathDestino & fi.Name.Substring(0, fi.Name.LastIndexOf(".")) & "_" & i.ToString("d3") & fi.Extension
                End If
                i += 1

                Trace.WriteLineIf(ZTrace.IsInfo, "Copiando archivo")
                Trace.WriteLineIf(ZTrace.IsInfo, "Original: " & origen)
                Trace.WriteLineIf(ZTrace.IsInfo, "Destino: " & dest)
                IO.File.Copy(origen, dest)
                flag = False
            Catch ex As IO.IOException
                flag = True
            End Try
        End While
    End Sub
    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        If File <> "" Then
            Dim targetFile As String
            SetFolder(File, param)
            Dim fi As New IO.FileInfo(File)
            targetFile = BackUpFolder & fi.Name

            Try
                IO.File.Copy(File, targetFile)
            Catch ex As Exception
                TryToCopy(File, BackUpFolder)
            End Try
        End If
        Return String.Empty
    End Function

    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml
    End Sub

    Public Shared ReadOnly Property Id() As Integer
        Get
            Return ProcessFactory.GetProcessIDByName(Name())
        End Get
    End Property

    Public Shared ReadOnly Property Name() As String
        Get
            Return "Back Up de Archivo"
        End Get
    End Property
End Class
