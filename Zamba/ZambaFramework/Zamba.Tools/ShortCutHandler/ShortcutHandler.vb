Imports IWshRuntimeLibrary
Imports System.Windows.Forms
Imports System.IO
Imports System.Diagnostics

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
            'Dim shell As New WshShell()
            'Dim link As IWshShortcut = DirectCast(shell.CreateShortcut(fullName), IWshShortcut)
            'link.TargetPath = targetPath
            'link.Description = "Zamba Software"
            'link.WorkingDirectory = targetPath.Substring(0, targetPath.LastIndexOf("\")) & "\"
            'link.Save()
        Catch ex As Exception
            Throw ex
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
            Dir = New IO.DirectoryInfo(Membership.MembershipHelper.AppTempPath & "\Exceptions")
            If Dir.Exists = False Then
                Dir.Create()
            End If
        End Try
        Dim TraceDirectory As String = Dir.FullName & "\"
        If Not Directory.Exists(TraceDirectory) Then
            Directory.CreateDirectory(TraceDirectory)
        End If
        Dim Trace2 As New TextWriterTraceListener(TraceDirectory & "Trace Shortcut " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".txt")
        Trace.Listeners.Add(Trace2)
        Trace.AutoFlush = True

        Trace2.WriteLine("-----------------------------------------------------")
        Trace2.WriteLine("      Comienzo de la validación de Shortcuts")

        Dim Folder As DirectoryInfo

        Folder = New DirectoryInfo(folderFullName)

        Try
            If Not IsNothing(deleteShortcutNames) Then
                Trace2.WriteLine("")
                Trace2.WriteLine("Buscando Shortcuts para eliminar: ")
                For Each sName As String In deleteShortcutNames
                    If System.IO.File.Exists(folderFullName & sName & ".lnk") Then
                        Trace2.WriteLine("")
                        Trace2.Write("Shortcut encontrado [" & sName & ".lnk] ---> eliminando")
                        Try
                            Dim sht As New IO.FileInfo(folderFullName & sName & ".lnk")
                            sht.Delete()
                            sht = Nothing
                            Trace2.Write("| Eliminado con éxito.")
                        Catch
                            Try
                                Dim sht As New IO.FileInfo(folderFullName & sName & ".lnk")
                                sht.Attributes = FileAttributes.Normal
                                sht.Delete()
                                sht = Nothing
                                Trace2.Write("| Eliminado con éxito.")
                            Catch
                                Try
                                    System.IO.File.Delete(folderFullName & sName & ".lnk")
                                    Trace2.Write("| Eliminado con éxito.")
                                Catch ex As Exception
                                    Trace2.Write("| No se ha podido eliminar (error: " & ex.ToString() & ")")
                                End Try
                            End Try
                        End Try
                    End If
                Next
            Else

            End If
        Catch ex As Exception
            Trace2.WriteLine("")
            Trace2.WriteLine("Error No Controlado : " & ex.ToString())
            Trace2.WriteLine("")
        End Try

        'Se crea esta colección para borrar los shortCut necesarios después de recorrer
        'la lista de búsqueda y que no de error al modificarla en ejecucuión [Alejandro]
        Dim filesToDelete As New Generic.List(Of FileInfo)

        Try
            Trace2.WriteLine("")
            Trace2.WriteLine("")
            Trace2.WriteLine("Buscando Shortcuts con coincidencias:")
            For Each file As FileInfo In Folder.GetFiles("*.lnk")
                Trace2.WriteLine("")
                Trace2.Write("Validando Shortcut [" & file.Name & "] : ")
                If ExistsShortcut(file.FullName.Trim, targetPath) Then
                    Trace2.Write("Coincidencia de Destino (" & targetPath & ").")
                    If Not filesToDelete.Contains(file) Then
                        filesToDelete.Add(file)
                        Trace2.Write(" Añadido para eliminar.")
                    End If
                ElseIf Not IsNothing(deleteShortcutTargets) Then
                    Dim Shortcut As New ShellShortcut(file.FullName)
                    If deleteShortcutTargets.Contains(Shortcut.Path) Then
                        Trace2.Write("Coincidencia de Destino (" & Shortcut.Path & ").")
                        If Not filesToDelete.Contains(file) Then
                            filesToDelete.Add(file)
                            Trace2.Write(" Añadido para eliminar.")
                        End If
                    Else
                        Trace2.Write(" Sin coincidencias.")
                    End If
                Else
                    Trace2.Write(" Sin coincidencias.")
                End If
            Next
        Catch ex As Exception
            Trace2.WriteLine("")
            Trace2.WriteLine("Error No Controlado : " & ex.ToString())
            Trace2.WriteLine("")
        End Try

        Trace2.WriteLine("")

        Try
            'Se recorre de esta forma la colección para que al recorrer la lista no de
            'error de modificación de colección en ejecución [Alejandro]
            Dim filesCount As Int16 = Convert.ToInt16(filesToDelete.Count - 1)
            Trace2.WriteLine("")
            Trace2.WriteLine(filesCount.ToString() & " Shortcut(s) para eliminar")
            Trace2.WriteLine("")
            Dim i As Int16
            For i = 0 To filesCount
                Trace2.WriteLine("")
                Trace2.Write("Shortcut: " & filesToDelete(i).FullName)
                Try
                    filesToDelete(i).Delete()
                    filesToDelete(i) = Nothing
                    Trace2.Write(" --> eliminado.")
                Catch
                    Try
                        filesToDelete(i).Attributes = FileAttributes.Normal
                        filesToDelete(i).Delete()
                        filesToDelete(i) = Nothing
                        Trace2.Write(" --> eliminado.")
                    Catch ex As Exception
                        Trace2.Write(" --> no ha podido ser eliminado (error: " & ex.ToString() & ".")
                    End Try
                End Try
            Next
        Catch ex As Exception
            Trace2.WriteLine("")
            Trace2.WriteLine("Error No Controlado : " & ex.ToString())
            Trace2.WriteLine("")
        End Try

        Trace2.WriteLine("")
        Trace2.WriteLine("")
        Trace2.WriteLine("Creando acceso directo:")

        Dim FullName As New System.Text.StringBuilder()
        Try
            FullName.Append(folderFullName)
            FullName.Append(shortcutName)
            FullName.Append(".lnk") 'le pongo la extensión .lnk
            CreateLink(FullName.ToString, targetPath)
            Trace2.WriteLine("[" & FullName.ToString() & "|" & targetPath & "] Shortcut creado con éxito")
        Catch ex As Exception
            Trace2.WriteLine("El Shortcut no ha podido ser creado (error: " & ex.ToString() & ")")
        End Try
        FullName = Nothing

        Trace2.WriteLine("")
        Trace2.WriteLine("")
        Trace2.WriteLine("-----------------------------------------------------")
        Trace2.WriteLine("")
        Trace2.Close()

    End Sub

End Class
