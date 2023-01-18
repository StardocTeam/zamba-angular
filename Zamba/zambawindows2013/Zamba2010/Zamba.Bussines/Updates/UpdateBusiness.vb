Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports Zamba.Data


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
    Friend Shared WithEvents bg As BackgroundWorker

    Private Sub GetServerData()
        Try
            Dim ds As DataSet = UpdateFactory.GetServerData

            Me.ServerVersion = ds.Tables(0).Rows(0).Item(0).ToString()
            Me.ServerPath = ds.Tables(0).Rows(0).Item(1).ToString()
            ds.Dispose()
        Catch ex As Exception
            ZClass.raiseerror(ex)
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
        Return UpdateFactory.UsuariosActualizadosCount(Me.ServerVersion)
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
        Return UpdateFactory.UsuariosDesactualizadosCount(Me.ServerVersion)
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
        Return UpdateFactory.Usuarios_Actualizados(Me.ServerVersion)
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
        Return UpdateFactory.Usuarios_Desactualizados(Me.ServerVersion)
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
            ZClass.raiseerror(ex)
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
        Me.GetServerData()
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
            Dim lastVersionId As Integer = UpdateFactory.GetLastVersionId()
            If CopyFile Then
                File.Copy(sourcePath, destPath & "\" & fileName, True)
            End If
            UpdateFactory.AddNewZambaVersion(lastVersionId + 1, versionNumber, destPath & "\" & fileName)
        Catch ex As Exception
            Throw ex
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
            Throw ex
        End Try
    End Sub

    Public Shared Sub OpenVersionLocation(version As VersionObject)
        Try
            Dim path As String = UpdateFactory.GetVersionPath(version.ID)
            If Not String.IsNullOrEmpty(path) AndAlso File.Exists(path) Then
                Dim folderPath = path.Substring(0, path.LastIndexOf("\"))
                Diagnostics.Process.Start(folderPath)
            Else
                MessageBox.Show("No su pudo mostrar ubicacion del archivo, verifique que exista.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub UpdateUsersVersion(versionNumber As String, versionPath As String, usersSelected As List(Of String))
        Try
            For Each user As String In usersSelected
                UserPreferences.setValueForUsersId("LastVersion", versionNumber, 1, Long.Parse(user))
                UserPreferences.setValueForUsersId("LastVersionPath", versionPath, 1, Long.Parse(user))
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function CheckLastVersionAtServer()
        Try
            Dim Filelist As New List(Of String)
            Dim versionsToDownload As New List(Of String)
            Dim FtpUrl As String = ZOptBusiness.GetValue("UpdateServerFtpUrl")
            Dim FtpUserName As String = ZOptBusiness.GetValue("UpdateServerFtpUserName")
            Dim FtpPassword As String = ZOptBusiness.GetValue("UpdateServerFtpPassword")
            Dim pathToDownloadVersions As String = GetLastFolderPath()

            If Not String.IsNullOrEmpty(pathToDownloadVersions) Then
                Filelist = GetFTPFileList(FtpUrl, FtpUserName, FtpPassword)
                Dim FileListDictionary As Dictionary(Of String, Integer) = DirListToDictionary(Filelist)
                Dim lastLocalVersionNumber As Integer = UpdateFactory.GetLastVersionNumber()

                For Each version As KeyValuePair(Of String, Integer) In FileListDictionary
                    If version.Value > lastLocalVersionNumber Then
                        versionsToDownload.Add(version.Key)
                    End If
                Next

                'Poner esto en un bgWorker
                For Each version As String In versionsToDownload
                    DownloadFTPFile(FtpUrl, version, FtpUserName, FtpPassword, pathToDownloadVersions)
                    If Not File.Exists(pathToDownloadVersions & "\" & version) Then
                        ZClass.raiseerror(New Exception(String.Format("Error al bajar la nueva version {0}\{1} de {2}", pathToDownloadVersions, version, FtpUrl)))
                        Exit For
                    End If
                    AddNewZambaVersion(GetNumberVersion(version), pathToDownloadVersions & "\" & version, pathToDownloadVersions, version, False)
                Next
                '

            End If

        Catch ex As Exception
            If ex.Message.Contains("URI está vacío") Then
                ZClass.raiseerror(New Exception(String.Format("El valor UpdateServerFtpUrl,UpdateServerFtpUserName o UpdateServerFtpPassword se encuentra vacio en ZOPT :  " & ex.ToString())))
            Else ZClass.raiseerror(ex)
            End If

        End Try
    End Function

    Private Shared Sub DownloadFTPFile(FTPurl As String, FileName As String, Username As String, Password As String, DownloadPath As String)
        Try
            Using client As WebClient = New WebClient()
                client.Credentials = New NetworkCredential(Username, Password)
                client.DownloadFile(FTPurl + "\" + FileName, DownloadPath + "\" + FileName)
            End Using
            'bg = New BackgroundWorker()
            'RemoveHandler bg.DoWork, AddressOf bg_DoWork
            'AddHandler bg.DoWork, AddressOf bg_DoWork
            'RemoveHandler bg.RunWorkerCompleted, AddressOf bg_RunWorkerCompleted
            'AddHandler bg.RunWorkerCompleted, AddressOf bg_RunWorkerCompleted

            'Dim pararms As Dictionary(Of String, String) = New Dictionary(Of String, String)
            'pararms.Add("FTPurl", FTPurl)
            'pararms.Add("FileName", FileName)
            'pararms.Add("Username", Username)
            'pararms.Add("Password", Password)
            'pararms.Add("DownloadPath", DownloadPath)

            'bg.RunWorkerAsync(pararms)


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Shared Sub bg_DoWork(sender As Object, e As DoWorkEventArgs)
    '    Dim params As Dictionary(Of String, String) = TryCast(e.Argument, Dictionary(Of String, String))
    '    Using client As WebClient = New WebClient()
    '        client.Credentials = New NetworkCredential(params("Username"), params("Password"))
    '        client.DownloadFile(params("FTPurl") + "\" + params("FileName"), params("DownloadPath") + "\" + params("FileName"))
    '    End Using
    'End Sub

    'Private Shared Sub bg_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)

    'End Sub

    Public Shared Function GetLastFolderPath() As String
        Dim LastVersionPath As String = UpdateFactory.GetVersionPath(UpdateFactory.GetLastVersionId())
        If Not String.IsNullOrEmpty(LastVersionPath) Then
            Return LastVersionPath.Substring(0, LastVersionPath.LastIndexOf("\"))
        Else
            Return String.Empty
        End If
    End Function

    Private Shared Function DirListToDictionary(dirlist As List(Of String)) As Dictionary(Of String, Integer)
        Dim DirListDictionary As New Dictionary(Of String, Integer)
        For Each versionFile As String In dirlist
            DirListDictionary.Add(versionFile, GetNumberVersion(versionFile).Replace(".", ""))
        Next
        Return DirListDictionary
    End Function

    Private Shared Function GetNumberVersion(versionFile As String) As String
        While Not IsNumeric(versionFile.Substring(0, 1))
            versionFile = versionFile.Substring(versionFile.IndexOf(".") + 1)
            If IsNumeric(versionFile.Substring(0, 1)) Then
                versionFile = versionFile.Remove(versionFile.LastIndexOf("."))
            End If
        End While
        Return versionFile
    End Function

    Private Shared Function GetFTPFileList(FTPurl As String, userName As String, password As String) As List(Of String)
        Dim Dirlist As New List(Of String)
        Dim request As FtpWebRequest = CType(WebRequest.Create(FTPurl), FtpWebRequest)
        request.Method = WebRequestMethods.Ftp.ListDirectory
        request.Credentials = New NetworkCredential(userName, password)
        Using response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Dim responseStream As Stream = response.GetResponseStream()
            Using reader As New StreamReader(responseStream)
                Do While reader.Peek <> -1
                    Dirlist.Add(reader.ReadLine)
                Loop
            End Using
        End Using
        Return Dirlist
    End Function

End Class
