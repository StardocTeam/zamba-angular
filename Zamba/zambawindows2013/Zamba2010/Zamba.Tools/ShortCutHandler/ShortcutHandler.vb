Imports System.Collections.Generic
Imports System.IO
Imports IWshRuntimeLibrary

''' <summary>
''' Esta clase se encarga de administrar los accesos directos a Zamba
''' </summary>
''' <remarks></remarks>
Public Class ShortcutHandler
    ''' <summary>
    ''' Crea un acceso directo 
    ''' </summary>
    ''' <param name="fullName">La ruta COMPLETA del acceso directo</param>
    ''' <param name="targetPath">La ruta COMPLETA del archivo al que apunta</param>
    ''' <remarks> 
    ''' FullName
    ''' Ej: C:\Documents and Settings\Legnani\Escritorio\Cliente.lnk OK
    '''     C:\Documents and Settings\Legnani\Escritorio\            MAL
    ''' </remarks>
    Public Shared Sub CreateLink(ByVal fullName As String, ByVal targetPath As String)
        Try
            Dim shell As New WshShell()
            Dim link As IWshShortcut = DirectCast(shell.CreateShortcut(fullName), IWshShortcut)
            link.TargetPath = targetPath
            link.Description = "Zamba Cliente"
            link.WorkingDirectory = targetPath.Substring(0, targetPath.LastIndexOf("\")) & "\"
            link.Save()
        Catch ex As Exception
        End Try
    End Sub
    ''' <summary>
    ''' Valida si un Acceso Directo apunta a un archivo específico
    ''' </summary>
    ''' <param name="fullName">La ruta COMPLETA del acceso directo</param>
    ''' <param name="targetPath">La ruta COMPLETA del archivo a que apunta</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' FullName
    ''' Ej: C:\Documents and Settings\Legnani\Escritorio\Cliente.lnk OK
    '''     C:\Documents and Settings\Legnani\Escritorio\            MAL
    ''' </remarks>
    Public Shared Function ExistsShortcut(ByVal fullName As String, ByVal targetPath As String) As Boolean
        Try
            Dim Shortcut As New ShellShortcut(fullName)
            Return Shortcut.Path = targetPath
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub DeleteShorcuts(shorcuts As IEnumerable(Of String), ByRef shorutsNonDeleted As List(Of String))
        If shorcuts IsNot Nothing Then
            For Each shorcut As String In shorcuts
                Dim shorcutPath As String = String.Concat(Path.Combine(My.Computer.FileSystem.SpecialDirectories.Desktop, shorcut), ".lnk")
                If IO.File.Exists(shorcutPath) Then
                    Try
                        IO.File.Delete(shorcutPath)
                    Catch ex As Exception
                        shorutsNonDeleted.Add(String.Format("No se pudo eliminar: {0}, Error: \n{1}", shorcutPath, ex.Message))
                    End Try
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Valida si existe un acceso directo a un archivo en una carpeta. Si no existe lo crea.
    ''' </summary>
    ''' <param name="folderFullName">La ruta COMPLETA de la carpeta</param>
    ''' <param name="targetPath">La ruta COMPLETA del archivo al que se apunta</param>
    ''' <remarks></remarks>
    Public Shared Sub ValidateShortcutInFolder(ByVal folderFullName As String, ByVal targetPath As String)
        Dim Folder As New DirectoryInfo(folderFullName)
        Dim FlagExists As Boolean
        For Each file As FileInfo In Folder.GetFiles("*.lnk")
            If ExistsShortcut(file.FullName, targetPath) Then
                FlagExists = True
                Exit For
            End If
        Next

        If FlagExists Then
            Exit Sub
        Else
            Dim FileInfo As New FileInfo(targetPath)
            Dim FullName As New System.Text.StringBuilder()
            FullName.Append(folderFullName)
            FullName.Append("\")
            FullName.Append(FileInfo.Name)
            FullName.Remove(FullName.Length - 4, 4) 'le saco la extension del archivo al que apunta 
            FullName.Append(".lnk") 'le pongo la extensión .lnk
            CreateLink(FullName.ToString, targetPath)
            FileInfo = Nothing
            FullName = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Valida si existe un acceso directo a un archivo en una carpeta. Si no existe lo crea.
    ''' </summary>
    ''' <param name="folderFullName">La ruta COMPLETA de la carpeta</param>
    ''' <param name="targetPath">La ruta COMPLETA del archivo al que se apunta</param>
    ''' <param name="shortcutName">El nombre del acceso directo</param>
    ''' <remarks></remarks>
    Public Shared Sub ValidateShortcutInFolder(ByVal folderFullName As String, ByVal targetPath As String, ByVal shortcutName As String, Optional ByVal deleteShortcutNames As Generic.List(Of String) = Nothing, Optional ByVal deleteShortcutTargets As Generic.List(Of String) = Nothing)

        Dim Dir As IO.DirectoryInfo
        Try
            Dir = New IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\Exceptions")
            If Dir.Exists = False Then
                Dir.Create()
            End If
        Catch
            Dir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\Exceptions")
            If Dir.Exists = False Then
                Dir.Create()
            End If
        End Try
        Dim TraceDirectory As String = Dir.FullName & "\"
        If Not Directory.Exists(TraceDirectory) Then
            Directory.CreateDirectory(TraceDirectory)
        End If

        Dim Folder As DirectoryInfo

        Folder = New DirectoryInfo(folderFullName)

        Try
            If Not IsNothing(deleteShortcutNames) Then
                For Each sName As String In deleteShortcutNames
                    If IO.File.Exists(folderFullName & sName & ".lnk") Then
                        Try
                            Dim sht As New FileInfo(folderFullName & sName & ".lnk")
                            sht.Delete()
                            sht = Nothing
                        Catch
                            Try
                                Dim sht As New FileInfo(folderFullName & sName & ".lnk")
                                sht.Attributes = FileAttributes.Normal
                                sht.Delete()
                                sht = Nothing
                            Catch
                                Try
                                    IO.File.Delete(folderFullName & sName & ".lnk")
                                Catch ex As Exception
                                End Try
                            End Try
                        End Try
                    End If
                Next
            Else

            End If
        Catch ex As Exception
        End Try

        'Se crea esta colección para borrar los shortCut necesarios después de recorrer
        'la lista de búsqueda y que no de error al modificarla en ejecucuión [Alejandro]
        Dim filesToDelete As New Generic.List(Of FileInfo)

        Try
            For Each file As FileInfo In Folder.GetFiles("*.lnk")
                If ExistsShortcut(file.FullName.Trim, targetPath) Then
                    If Not filesToDelete.Contains(file) Then
                        filesToDelete.Add(file)
                    End If
                ElseIf Not IsNothing(deleteShortcutTargets) Then
                    Dim Shortcut As New ShellShortcut(file.FullName)
                    If deleteShortcutTargets.Contains(Shortcut.Path) Then
                        If Not filesToDelete.Contains(file) Then
                            filesToDelete.Add(file)
                        End If
                    Else
                    End If
                Else
                End If
            Next
        Catch ex As Exception
        End Try


        Try
            'Se recorre de esta forma la colección para que al recorrer la lista no de
            'error de modificación de colección en ejecución [Alejandro]
            Dim filesCount As Int16 = Convert.ToInt16(filesToDelete.Count - 1)
            Dim i As Int16
            For i = 0 To filesCount
                Try
                    filesToDelete(i).Delete()
                    filesToDelete(i) = Nothing
                Catch
                    Try
                        filesToDelete(i).Attributes = FileAttributes.Normal
                        filesToDelete(i).Delete()
                        filesToDelete(i) = Nothing
                    Catch ex As Exception
                    End Try
                End Try
            Next
        Catch ex As Exception
        End Try


        Dim FullName As New System.Text.StringBuilder()
        Try
            FullName.Append(folderFullName)
            FullName.Append(shortcutName)
            FullName.Append(".lnk") 'le pongo la extensión .lnk
            CreateLink(FullName.ToString, targetPath)
        Catch ex As Exception
        End Try
        FullName = Nothing


    End Sub

End Class
