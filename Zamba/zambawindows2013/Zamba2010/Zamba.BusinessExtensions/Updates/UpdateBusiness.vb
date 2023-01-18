Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports Zamba.Data
Imports Zamba.Tools

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Updates
''' Class	 : Updates.Updater
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para manejar las actualizaciones de la version de Zamba en usuarios
''' </summary>
''' <remarks>
''' Actualizaciones Automaticas
''' </remarks>
''' <history>
''' 	[Hernan]	26/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class UpdateBusiness
    Inherits ZClass
    Public ServerVersion As String
    Public ServerPath As String

    Private Sub GetServerData()
        Try
            Dim ds As DataSet = UpdateFactory.GetServerData

            ServerVersion = ds.Tables(0).Rows(0).Item(0).ToString()
            ServerPath = ds.Tables(0).Rows(0).Item(1).ToString()
            ds.Dispose()
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la cantidad de usuarios que tienen la ultima versión de Zamba instalada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function UsuariosActualizadosCount() As Int32
        Return UpdateFactory.UsuariosActualizadosCount(ServerVersion)
    End Function

    Public Shared Function ExistsVersion(versionNumber As String) As Boolean
        Dim query As String = "select count(*) from VERREG where VER = '" & versionNumber & "'"
        If Integer.Parse(Servers.Server.Con.ExecuteScalar(CommandType.Text, query)) > 0 Then
            Return True
        End If
        Return False
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la cantidad de usuarios que todavia no actualizaron su versión de Zamba
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function UsuariosDesactualizadosCount() As Int32
        Return UpdateFactory.UsuariosDesactualizadosCount(ServerVersion)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve el detalle(listado) de los usuarios que estan actualizados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function Usuarios_Actualizados() As DataTable
        Return UpdateFactory.Usuarios_Actualizados(ServerVersion)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve el detalle(listado) de los usuarios que estan desactualizados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function Usuarios_Desactualizados() As DataTable
        Return UpdateFactory.Usuarios_Desactualizados(ServerVersion)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Modifica la versión que tiene el servidor, es decir pone una versión disponible
    ''' para actualizar
    ''' </summary>
    ''' <param name="newversion">Cadena con el numero de la version, ej: "1.6.7"</param>
    ''' <param name="path">Ruta donde se encuentra la versión</param>
    ''' <remarks>
    ''' La ruta debe ser una unidad de red donde los usuarios tengan acceso de lectura
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub ChangeServerVersion(ByVal newversion As String, ByVal path As String)
        If newversion.Trim <> String.Empty Then
            UpdateFactory.ChangeServerVersion(newversion, path)
        End If
        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(0, ObjectTypes.Version, RightsType.Edit, "Se actualizó la versión a la " & newversion & " ubicada en " & path)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Para forzar la actualización de un usuario especifico
    ''' </summary>
    ''' <param name="winuser">Nombre del usuario de windows que se desea actualizar</param>
    ''' <param name="serverversion"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub ForzarActualizar(ByVal winuser As String, ByVal serverversion As String)
        serverversion = GetOlderVersion(serverversion)
        UpdateFactory.ForzarActualizar(winuser, serverversion)
    End Sub


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Para forzar la actualización de una PC especifica
    ''' </summary>
    ''' <param name="mName">Nombre de la PC que se desea actualizar</param>
    ''' <param name="serverversion"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	15/01/2011	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub ForzarActualizarPorPC(ByVal mName As String, ByVal serverVersion As String)
        Try
            'Busca la version instalada
            Dim version As String = UpdaterFactory.GetMachineZambaVersion(mName)

            'Verifica si existe una version registrada
            If String.IsNullOrEmpty(version) OrElse IsDBNull(version) Then
                'Registra el puesto en zamba
                UpdaterBusiness.SetEstreg(serverVersion)

            Else
                'Actualiza la versión de zamba correspondiente al puesto
                UpdateFactory.ForzarActualizarPorPC(mName, serverVersion, Membership.MembershipHelper.CurrentUser.Name.ToString)
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Private Shared Function GetOlderVersion(ByVal version As String) As String
        Try
            If IsNumeric(version) Then
                version = (Int32.Parse(version) - 1).ToString()
            Else
                Dim aux As String = version.Substring(version.Length - 2, version.Length - 1)
                If IsNumeric(aux) Then aux = (Int32.Parse(version) - 1).ToString()
                version = version.Substring(0, version.Length - 2) & aux
            End If
            Return version
        Catch ex As Exception
        End Try
        Return version
    End Function
    Public Sub New()
        GetServerData()
    End Sub

    Public Overrides Sub Dispose()

    End Sub

    Public Shared Function GetVersionsTable() As DataTable
        Return UpdateFactory.GetVersionsTable()
    End Function

    Public Shared Function GetUsersVersionsTable() As DataTable
        Return UpdateFactory.GetUsersVersionsTable()
    End Function

    ''' <summary>
    ''' Agrega una nueva version de zamba
    ''' </summary>
    ''' <param name="versionNumber">Numero de version.</param>
    ''' <param name="sourcePath">Ruta donde busca el zip.</param>
    ''' <param name="destPath">Ruta donde se va a copiar el zip.</param>
    ''' <param name="fileName">Nombre del nuevo archivo.</param>
    ''' <param name="CopyFile">Si copia el zip en la carpeta o solo guarda el registro en base de datos.</param>
    ''' <remarks>
    ''' El ultimo parametro es porque cuando se descarga un archivo desde ftp ya lo copia en la carpeta,
    ''' y para reutilizar este metodo se añadio la opcion de que copie o no.
    ''' </remarks>
    Public Shared Sub AddNewZambaVersion(versionNumber As String, sourcePath As String, destPath As String, fileName As String, CopyFile As Boolean)
        Try
            Dim lastVersionId As Integer = GetLastVersionId()
            If CopyFile Then File.Copy(sourcePath, destPath & "\" & fileName, True)
            UpdateFactory.AddNewZambaVersion(lastVersionId + 1, versionNumber, destPath & "\" & fileName)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub RemoveZambaVersion(versions As List(Of VersionObject), removeFile As Boolean)
        Try
            Dim users As New DataTable
            For Each version As VersionObject In versions
                'Obtengo usuarios con esas versiones en la zuserconfig
                users.Merge(UpdateFactory.GetUsersIdByVersionNumber(UpdateFactory.GetVersionNumber(version.ID)))
                'Elimino versiones de la base
                UpdateFactory.RemoveZambaVersion(version.ID)
                If removeFile Then 'Elimino archivos
                    For Each fP As String In (From vs In versions Select vs.FilePath)
                        If File.Exists(fP) Then
                            File.Delete(fP)
                        End If
                    Next
                End If
            Next

            'Si habia usuarios con esas versiones
            If users.Rows.Count > 0 Then
                'Obtengo la version mas baja de las eliminadas.
                Dim lowerVersion As String = (From v In versions Order By v.ID Ascending).First().ID
                'Obtengo version anterior a la mas baja eliminada.
                Dim IdEarlierVersion As Integer = UpdateFactory.GetEarlierVersionId(lowerVersion)
                Dim NumberEarlierVersion As String = String.Empty
                Dim PathEarlierVersion As String = String.Empty

                If IdEarlierVersion > 0 Then
                    NumberEarlierVersion = UpdateFactory.GetVersionNumber(IdEarlierVersion)
                    PathEarlierVersion = UpdateFactory.GetVersionPath(IdEarlierVersion)
                    For Each usr As DataRow In users.Rows
                        UserPreferences.setValueForUsersId("LastVersion", NumberEarlierVersion, 1, Long.Parse(usr.Item(0)))
                        UserPreferences.setValueForUsersId("LastVersionPath", PathEarlierVersion, 1, Long.Parse(usr.Item(0)))
                    Next
                Else
                    For Each usr As DataRow In users.Rows
                        UserPreferences.RemoveValueByUsersID(usr(0), "LastVersion")
                        UserPreferences.RemoveValueByUsersID(usr(0), "LastVersionPath")
                    Next
                End If
            End If

        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub UpdateUsersVersion(versionNumber As String, versionPath As String, usersSelected As List(Of Long))
        Try
            For Each user As Long In usersSelected
                ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Actualizando usuario con id {0} a version numero {1}", user, versionNumber))
                UserPreferences.setValueForUsersId("LastVersion", versionNumber, 1, user)
                UserPreferences.setValueForUsersId("LastVersionPath", versionPath, 1, user)
            Next
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub UpdateUsersWithAutoUpdate()
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando actualizacion usuarios con actualizacion automatica")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Consultando usuarios con actualizacion auto activada")
            Dim usersDT As DataTable = UpdateFactory.GetUsersIDWithAutoUpdate()
            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Hay {0} usuarios con actualizacion automatica", usersDT.Rows.Count))

            If usersDT IsNot Nothing AndAlso usersDT.Rows.Count > 0 Then
                Dim usersList As New List(Of Long)
                For Each user As DataRow In usersDT.Rows
                    usersList.Add(user(0))
                Next
                Dim versionId = GetLastVersionId()
                Dim lastVersion As String = UpdateFactory.GetVersionNumber(versionId)
                Dim lastVersionPath As String = UpdateFactory.GetVersionPath(versionId)
                UpdateUsersVersion(lastVersion, lastVersionPath, usersList)
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Public Shared Function GetLastVersionId() As Integer
        Try
            Return UpdateFactory.GetLastVersionId()
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Function

    Public Shared Function GetLastVersionNumber() As Integer
        Try
            Return UpdateFactory.GetLastVersionNumberWithoutDots()
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Function

    'Public Shared Function GetVersionsListToDownLoadAtServer() As List(Of String)
    '    Try
    '        Dim Filelist As New List(Of String)
    '        Dim versionsToDownload As New List(Of String)

    '        Dim FtpUrl As String = Encryption.DecryptString(ZOptBusiness.GetValue("UpdateServerFtpUrl"))
    '        Dim FtpUserName As String = Encryption.DecryptString(ZOptBusiness.GetValue("UpdateServerFtpUserName"))
    '        Dim FtpPassword As String = Encryption.DecryptString(ZOptBusiness.GetValue("UpdateServerFtpPassword"))

    '        Dim pathToDownloadVersions As String = GetLastFolderPath()
    '        If String.IsNullOrEmpty(pathToDownloadVersions) Then
    '            pathToDownloadVersions = ZOptBusiness.GetValue("URLZambaRelease")
    '        End If

    '        If Not String.IsNullOrEmpty(pathToDownloadVersions) Then
    '            'Obtengo una lista con los archivos que hay en el servidor.
    '            Filelist = GetFTPFileList(FtpUrl, FtpUserName, FtpPassword)
    '            'De la lista de archivos obtengo un diccionario con el nombre del archivo como clave y el numero de version como valor
    '            Dim FileListDictionary As Dictionary(Of String, Integer) = DirListToDictionary(Filelist)
    '            'Otengo el ultimo numero de version que tengo actualmente en la VERREG.
    '            Dim lastLocalVersionNumber As Integer = GetLastVersionNumber()

    '            For Each version As KeyValuePair(Of String, Integer) In FileListDictionary
    '                If version.Value > lastLocalVersionNumber Then
    '                    versionsToDownload.Add(version.Key)
    '                End If
    '            Next
    '            Return versionsToDownload
    '        End If
    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try
    'End Function

    'Private Shared Sub DownloadZambaVersions(versionsToDownload As List(Of String))
    '    Try
    '        Dim FtpUrl As String = Encryption.DecryptString(ZOptBusiness.GetValue("UpdateServerFtpUrl"))
    '        Dim FtpUserName As String = Encryption.DecryptString(ZOptBusiness.GetValue("UpdateServerFtpUserName"))
    '        Dim FtpPassword As String = Encryption.DecryptString(ZOptBusiness.GetValue("UpdateServerFtpPassword"))
    '        Dim pathToDownloadVersions As String = GetLastFolderPath()
    '        If String.IsNullOrEmpty(pathToDownloadVersions) Then
    '            pathToDownloadVersions = ZOptBusiness.GetValue("URLZambaRelease")
    '        End If

    '        For Each version As String In versionsToDownload
    '            DownloadFTPFile(FtpUrl, version, FtpUserName, FtpPassword, pathToDownloadVersions)
    '            If Not File.Exists(pathToDownloadVersions & "\" & version) Then
    '                raiseerror(New Exception(String.Format("Error al bajar la nueva version {0}\{1} de {2}", pathToDownloadVersions, version, FtpUrl)))
    '                Exit For
    '            End If
    '            AddNewZambaVersion(GetNumberVersion(version), pathToDownloadVersions & "\" & version, pathToDownloadVersions, version, False)
    '        Next
    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try
    'End Sub

    ''' <summary>
    ''' Busca en el servidor si hay versiones mas actuales que la que hay en VERREG,
    ''' de ser asi las descarga.
    ''' </summary>
    ''' <returns>Retorna el numero de versiones descargadas o cero si no habia ninguna version mas nueva</returns>
    Public Shared Function CheckAndDownloadVersionsAtServer() As Integer
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Inicio verificacion nuevas versiones de Zamba")
            Dim Filelist As New List(Of String)
            Dim versionsToDownload As New List(Of String)

            Dim FtpUrl As String = Encryption.DecryptString(ZOptBusiness.GetValue("UpdateServerFtpUrl"))
            Dim FtpUserName As String = Encryption.DecryptString(ZOptBusiness.GetValue("UpdateServerFtpUserName"))
            Dim FtpPassword As String = Encryption.DecryptString(ZOptBusiness.GetValue("UpdateServerFtpPassword"))

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo directorio donde guardar el zip")

            Dim pathToDownloadVersions As String = ZOptBusiness.GetValue("URLZambaRelease")

            If pathToDownloadVersions.EndsWith("/") OrElse pathToDownloadVersions.EndsWith("\") Then
                pathToDownloadVersions = pathToDownloadVersions.Substring(0, pathToDownloadVersions.Length - 1)
            End If

            If String.IsNullOrEmpty(pathToDownloadVersions) OrElse Not Directory.Exists(pathToDownloadVersions) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se establecio variable URLZambaRelease en ZOPT o directorio con la direccion establecida ya no existe, se aborta la verificacion de versiones.")
                Return 0
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ruta de directorio obtenida: " & pathToDownloadVersions)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo archivos en el servidor")
            Filelist = GetFTPFileList(FtpUrl, FtpUserName, FtpPassword)
            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Se econtraron {0} archivo/s", Filelist.Count))
            'De la lista de archivos obtengo un diccionario con el nombre del archivo como clave y el numero de version como valor
            Dim FileListDictionary As Dictionary(Of String, Integer) = DirListToDictionary(Filelist)
            'Otengo el ultimo numero de version que tengo actualmente en la VERREG.
            Dim lastLocalVersionNumber As Integer = GetLastVersionNumber()
            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Ultima version en verreg {0}", lastLocalVersionNumber))

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando versiones a descargar")
            For Each version As KeyValuePair(Of String, Integer) In FileListDictionary
                If version.Value > lastLocalVersionNumber Then
                    versionsToDownload.Add(version.Key)
                End If
            Next

            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Versiones a descargar: {0}", versionsToDownload.Count))
            For Each version As String In versionsToDownload
                ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Descargando version {0}", version))
                DownloadFTPFile(FtpUrl, version, FtpUserName, FtpPassword, pathToDownloadVersions)
                ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Version {0} descargada", version))
                If Not File.Exists(pathToDownloadVersions & "\" & version) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Error al bajar la version {0} en la ruta {1}", version, pathToDownloadVersions))
                    raiseerror(New Exception(String.Format("Error al bajar la version {0} en la ruta {1}", version, pathToDownloadVersions)))
                    Exit For
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Agregando version {0} a Verreg", version))
                AddNewZambaVersion(GetNumberVersion(version), pathToDownloadVersions & "\" & version, pathToDownloadVersions, version, False)
            Next

            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Se descargaron y registraron {0} versiones", versionsToDownload.Count))
            Return versionsToDownload.Count
        Catch ex As Exception
            raiseerror(ex)
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Retorna la lista de archivos que hay en el servidor.
    ''' </summary>
    ''' <param name="FTPurl"> URL del servidor</param>
    ''' <param name="userName"> Usuario</param>
    ''' <param name="password"> Password</param>
    ''' <returns></returns>
    Private Shared Function GetFTPFileList(FTPurl As String, userName As String, password As String) As List(Of String)
        Try
            Dim Dirlist As New List(Of String)
            Dim request As FtpWebRequest = CType(WebRequest.Create(FTPurl), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.ListDirectory
            request.Credentials = New NetworkCredential(userName, password)
            Using response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
                Dim responseStream As Stream = response.GetResponseStream()
                Using reader As New StreamReader(responseStream)
                    Dim file As String
                    Do While reader.Peek <> -1
                        file = reader.ReadLine()
                        If file.StartsWith("zamba.", StringComparison.OrdinalIgnoreCase) AndAlso file.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) Then
                            Dirlist.Add(file)
                        End If
                    Loop
                End Using
            End Using
            Return Dirlist
        Catch ex As Exception
            If ex.Message.Contains("URI está vacío") Then
                raiseerror(New Exception(String.Format("El valor UpdateServerFtpUrl, UpdateServerFtpUserName o UpdateServerFtpPassword se encuentran vacios en ZOPT:  " & ex.ToString())))
            Else
                raiseerror(ex)
            End If
        End Try
    End Function

    Private Shared Sub DownloadFTPFile(FTPurl As String, FileName As String, Username As String, Password As String, DownloadPath As String)
        Try
            Using client As WebClient = New WebClient()
                client.Credentials = New NetworkCredential(Username, Password)
                Dim sourcePath = FTPurl & "\" & FileName
                Dim destPath = DownloadPath & "\" & FileName
                client.DownloadFile(sourcePath, destPath)
            End Using
        Catch ex As Win32Exception
            raiseerror(ex)

        Catch ex As Exception
            If ex.Message.Contains("URI está vacío") Then
                raiseerror(New Exception(String.Format("El valor UpdateServerFtpUrl, UpdateServerFtpUserName o UpdateServerFtpPassword se encuentran vacios en ZOPT:  " & ex.ToString())))
            Else
                raiseerror(ex)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene el ultima path utilizado al guardar una nueva version
    ''' </summary>
    ''' <returns>
    ''' La ruta donde se va a copiar el compilado
    ''' </returns>
    Public Shared Function GetLastFolderPath() As String
        Try
            Dim LastVersionPath As String = UpdateFactory.GetVersionPath(GetLastVersionId())
            If Not String.IsNullOrEmpty(LastVersionPath) Then
                If LastVersionPath.Contains("\") Then
                    Return LastVersionPath.Substring(0, LastVersionPath.LastIndexOf("\"))
                ElseIf LastVersionPath.Contains("/") Then
                    Return LastVersionPath.Substring(0, LastVersionPath.LastIndexOf("/"))
                End If
            End If
            Return String.Empty
        Catch ex As Exception
            raiseerror(ex)
            Return String.Empty
        End Try
    End Function

    Private Shared Function DirListToDictionary(dirlist As List(Of String)) As Dictionary(Of String, Integer)
        Try
            Dim DirListDictionary As New Dictionary(Of String, Integer)
            For Each versionFile As String In dirlist
                DirListDictionary.Add(versionFile, GetNumberVersion(versionFile).Replace(".", ""))
            Next
            Return DirListDictionary
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Function

    Private Shared Function GetNumberVersion(versionFile As String) As String
        Try
            While Not IsNumeric(versionFile.Substring(0, 1))
                versionFile = versionFile.Substring(versionFile.IndexOf(".") + 1)
                If IsNumeric(versionFile.Substring(0, 1)) Then
                    versionFile = versionFile.Remove(versionFile.LastIndexOf("."))
                End If
            End While
            Return versionFile
        Catch ex As Exception
            raiseerror(ex)
            Return String.Empty
        End Try
    End Function

End Class
