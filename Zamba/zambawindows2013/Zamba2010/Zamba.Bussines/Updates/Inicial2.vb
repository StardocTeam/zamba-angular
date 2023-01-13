Imports Zamba.Core
Imports Zamba.AppBlock
Imports System.IO
Imports System.Data
''' -----------------------------------------------------------------------------
''' Project	 : Zamba Cliente
''' Class	 : Inicial
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase que muestra en pantalla el proceso previo a la actualización
''' sin embargo, la actualización se realiza por atrás y sin conocimiento del
''' usuario.
''' </summary>
''' <history>
''' 	[Hernan]	29/05/2006	Created
'''     [Alejandro]    03/10/2007 Modified
'''     [Marcelo]    14/04/2008 Modified
''' </history>
''' -----------------------------------------------------------------------------

Public Class Inicial2
    '1   - Voy a la base y busco la última versión.
    '1.1 - Por Reflection saco la versión en uso.
    '1.2 - Cualquier error obteniendo las versiones deja los campos vacíos
    '2   - Borro los archivos 'failed' dentro del directorio de Zamba.
    '3   - Hago un Shell con el ejecutable que actualiza Zamba.
    '4   - Updatea Lotus.
    '5   - Finaliza este Formulario y la Aplicación(Zamba.Cliente)

#Region "Atributos"
    Dim Command As String
    Dim CurrentVersion As String
    Dim Updatepath As String
    Dim MyVersion As String
    Dim _Progress As Int32
    Private Shared key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Private Shared iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

    'Dim progress2 As New SmoothProgress
#End Region

#Region "Constructor"

    Public Sub New()

        MyBase.New()

    End Sub

#End Region

#Region "Métodos"

    '''<summary>Inicia toda la lógica</summary>
    Public Sub StartActions()

        'Solo necesito el valor del Path de Actualización.
        UpdaterBusiness.GetLastestVersion(String.Empty, Me.Updatepath, String.Empty)

        'Seteo las versiones.
        Me.GetVersions()

        Try
            Me.DeleteFailedFiles(Application.StartupPath)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Me.UpdateFiles()

    End Sub

    ''' <summary>
    ''' Hace un Shell del .exe que actualiza el registro de Zamba
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateFiles()

        If Not Directory.Exists(Me.Updatepath) Then
            MessageBox.Show("No se puede conectar con el servidor. Path de origen inexistente. Imposible actualizar", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw New Exception("No se puede conectar con el servidor. Path de origen inexistente. Imposible actualizar.")
        Else
            Dim usr As String = ZOptBusiness.GetValue("UpdaterCredentials_usr")
            Dim pass As String = ZOptBusiness.GetValue("UpdaterCredentials_pss")
            Dim domain As String = ZOptBusiness.GetValue("UpdaterCredentials_dom")
            If Not String.IsNullOrEmpty(pass) Then
                pass = pass.Replace("+", "§")
            End If

            Dim ExecuteClientAfterUpdate As String = "False"
            If ZOptBusiness.GetValue("ExecuteClientAfterUpdate") = "True" Then
                ExecuteClientAfterUpdate = "True"
            End If

            'Se realiza el Shell Al UpdateClientNGen con los parametros necesarios para que actualice al updateNGen . Una vez actualizado el updateNGen  se le hace un shell al mismo para que actualice al cliente.
            Dim PathOrigen As String = Me.Updatepath
            Dim PathDestino As String = Application.StartupPath
            Dim PathBackToExecute As String = Application.StartupPath & "\Zamba.Cliente.exe"
            Dim FlagIsZambaInstalled As String = "False"
            Dim installWithAdminRights As String = ZOptBusiness.GetValue("InstallZambaWithAdminRights")
            Dim dll As String = Application.StartupPath & "\UpToDate\Zamba.UpdateClientNGen.exe "
            Dim params As String = "+" & PathOrigen & "+" & PathDestino & "+" & PathBackToExecute & "+" & FlagIsZambaInstalled & "+" & usr & "+" & pass & "+" & domain & "+" & ExecuteClientAfterUpdate & "+" & installWithAdminRights '& "+" & logonType

            ZTrace.WriteLineIf(ZTrace.IsInfo, "# Parametros enviados desde Zamba.Cliente.exe a Zamba.UpdateClientNGen.exe")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "PathOrigen: " & PathOrigen)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "PathDestino: " & PathDestino)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "PathBackToExecute: " & PathBackToExecute)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "FlagIsZambaInstaled: " & FlagIsZambaInstalled)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Usr: " & usr)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "pass: " & pass)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "domain: " & domain)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "ExecuteClientAfterUpdate: " & ExecuteClientAfterUpdate)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "InstallZambaWithAdminRights: " & installWithAdminRights)

            If installWithAdminRights Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "# Ejecutando UpdateClientNGen.exe mediante Process.Start")
                Dim p As New Diagnostics.Process()
                Dim processStartInfo As New Diagnostics.ProcessStartInfo
                processStartInfo.FileName = dll
                processStartInfo.Arguments = params
                processStartInfo.Verb = "runas"
                processStartInfo.WindowStyle = ProcessWindowStyle.Normal
                processStartInfo.UseShellExecute = True
                p.Start(processStartInfo)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "# Ejecutando UpdateClientNGen.exe mediante Shell")
                Shell(dll & params, AppWinStyle.NormalFocus, False)
            End If
        End If
    End Sub

    '''<summary> Borra los archivos 'failed' en el directorio de Zamba </summary>
    Private Sub DeleteFailedFiles(ByVal path As String)
        Dim dir As New IO.DirectoryInfo(path)
        Dim files() As IO.FileInfo = dir.GetFiles("failed*.*")
        Dim file As IO.FileInfo
        For Each file In files
            Try
                file.Attributes = FileAttributes.Normal
                file.Delete()
            Catch ex As Exception
                ZAMBA.Core.ZClass.raiseerror(ex)
            End Try
        Next
        Dim directories() As IO.DirectoryInfo = dir.GetDirectories
        Dim dire As IO.DirectoryInfo
        For Each dire In directories
            DeleteFailedFiles(dire.FullName)
        Next
    End Sub

    Private Sub UpdateTheUpdater()
        Try
            Dim destPath As String = Application.StartupPath & "\UpToDate\"

            Dim origPath As String = Me.Updatepath & "\UpToDate\"

            For Each fileToCopy As String In Directory.GetFiles(origPath)
                File.Copy(fileToCopy, destPath & Path.GetFileName(fileToCopy), True)
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    '''<summary> Setea las variables globales de versiones </summary>
    Private Sub GetVersions()
        'Se asigna los valores de las diferentes versiones; puede asignarse el 
        'valor "<nulo>" ya que es solo para fines visuales
        Try
            Me.MyVersion = UpdaterBusiness.GetVersion()
        Catch ex As Exception
            Me.MyVersion = String.Empty
            ZClass.raiseerror(ex)
        End Try

        Try
            Me.CurrentVersion = UpdaterBusiness.GetLastestVersion()
        Catch ex As Exception
            Me.CurrentVersion = String.Empty
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

End Class
