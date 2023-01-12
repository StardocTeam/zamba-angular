Imports System.IO
Imports System.Threading
Imports Microsoft.Win32
Imports System.Collections.Generic
Imports System.Security.Cryptography
'Imports Zamba.Impersonate


''' -----------------------------------------------------------------------------
''' Project	 : Zamba.UpdateNgen
''' Class	 : Updater
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase que Actualiza los archivos de Zamba
''' </summary>
''' <history>
'''     [Alejandro]    03/10/2007 Created
'''     [Marcelo]    14/04/2008 Modified
'''     [Marcelo]    23/03/2009 Modified
''' </history>
''' -----------------------------------------------------------------------------

Public Class Updater
    Inherits System.Windows.Forms.Form

#Region "Atributos"
    Private PathOrigen As String
    Private PathDestino As String
    Private PathBackToExecute As String
    Private FlagIsZambaInstalled As Boolean = False
    Private Usr As String
    Private Pass As String
    Private Domain As String
    Private DecodePass As String
    Private ExecuteClientAfterUpdate As String
    Private FlagUserCredentials As Boolean
    'Private LogonType As ZImpersonalizeAdvance.LogonType
    Friend WithEvents pbProgresoUpdate As System.Windows.Forms.ProgressBar
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Private flagCorrectUpdate As Boolean = False
    Private Shared key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Private Shared iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
#End Region

#Region "Constructor"
    Public Sub New()

        Me.InitializeComponent()
        Try
            Dim exceptionsPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\Exceptions"
            If Not IO.Directory.Exists(exceptionsPath) Then IO.Directory.CreateDirectory(exceptionsPath)
            Trace.Listeners.Add(New TextWriterTraceListener(exceptionsPath & "\Trace UpdateNGen " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".txt"))
            Trace.AutoFlush = True
            Trace.WriteLine("Corriendo actualizacion NGen version:" & Application.ProductVersion)
        Catch
        End Try
    End Sub

    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Updater))
        Me.pbProgresoUpdate = New System.Windows.Forms.ProgressBar()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'pbProgresoUpdate
        '
        Me.pbProgresoUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.pbProgresoUpdate.ForeColor = System.Drawing.Color.LightSteelBlue
        Me.pbProgresoUpdate.Location = New System.Drawing.Point(12, 45)
        Me.pbProgresoUpdate.Name = "pbProgresoUpdate"
        Me.pbProgresoUpdate.Size = New System.Drawing.Size(388, 26)
        Me.pbProgresoUpdate.Step = 1
        Me.pbProgresoUpdate.TabIndex = 0
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Location = New System.Drawing.Point(9, 20)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(191, 13)
        Me.lblMessage.TabIndex = 1
        Me.lblMessage.Text = "Actualizando Zamba. Por favor espere."
        '
        'Updater
        '
        Me.ClientSize = New System.Drawing.Size(418, 98)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.pbProgresoUpdate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Updater"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zamba - Actualización Automática"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Eventos"
    ''' <summary>
    ''' Actualiza los archivos, recibe como parametro el path de origen, 
    '''el de destino, el del ejecutable y si zamba se encuentra instalado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Updater_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Trace.WriteLine("Iniciando update")
        Trace.WriteLine("-----------------------------------------")
        Trace.WriteLine("Usuario Windows: " & Environment.UserName)
        Trace.WriteLine("Dominio: " & Environment.UserDomainName)
        Trace.WriteLine("Puesto: " & Environment.MachineName)
        Trace.WriteLine("SO: " & Environment.OSVersion.VersionString)
        Trace.WriteLine("Ruta: " & Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))

        'Si no tiene argumentos iniciales se cierra
        If Not Environment.GetCommandLineArgs.Length > 1 Then
            Trace.WriteLine("Comienzo actualizacion por nro de version")

            Try
                Me.StartActions()
            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try

            Me.Close()
            Me.Dispose()
            Me.Finalize()
        Else
            Trace.WriteLine("Cerrando Procesos Activos de Zamba.")
            CerrarProcesosZamba()

            Trace.WriteLine(" ")
            Trace.WriteLine("---------------------------------------------")
            Trace.WriteLine("Comienza proceso de lectura de comandos.")

            Dim commandBuilder As New System.Text.StringBuilder()

            For Each s As String In Environment.GetCommandLineArgs()
                commandBuilder.Append(s)
                commandBuilder.Append(" ")
            Next
            commandBuilder = commandBuilder.Remove(commandBuilder.Length - 1, 1)

            Try
                Actualizar(commandBuilder.ToString())
            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try
            commandBuilder = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de la actualizacion a partir de los parametros
    ''' </summary>
    ''' <param name="arguments"></param>
    ''' <remarks></remarks>
    Private Sub Actualizar(ByVal arguments As String)
        Trace.WriteLine("Linea de comandos encontrada:")
        Trace.WriteLine(arguments)

        'Obtengo los valores 
        Dim CommandArgs As String() = arguments.Split(Char.Parse("+"))
        Trace.WriteLine("Cantidad de parametros Obtenidos : " & CommandArgs.Length)
        Me.PathOrigen = CommandArgs(1)
        Trace.WriteLine("PathOrigen: " & PathOrigen)
        Me.PathDestino = CommandArgs(2)
        Trace.WriteLine("PathDestino: " & PathDestino)
        Me.PathBackToExecute = CommandArgs(3)
        Trace.WriteLine("PathBackToExecute:" & PathBackToExecute)
        Me.FlagIsZambaInstalled = Boolean.Parse(CommandArgs(4))
        Trace.WriteLine("FlagIsZambaInstalled:" & FlagIsZambaInstalled)

        Try
            Me.Usr = CommandArgs(5).Trim()
            Trace.WriteLine("Usr: " & Usr)
            Me.Pass = CommandArgs(6).Trim()
            Trace.WriteLine("Pass: " & Pass)
            If Not String.IsNullOrEmpty(Pass) Then
                Me.DecodePass = DecryptString(Pass.Replace("§", "+"), key, iv)
            End If
            Me.Domain = CommandArgs(7).Trim()
            Trace.WriteLine("Domain: " & Domain)

            'If CommandArgs.Length >= 10 Then
            '    Me.LogonType = [Enum].Parse(GetType(ZImpersonalizeAdvance.LogonType), CommandArgs(9).Trim())
            '    Trace.WriteLine("Tipo de LOGON:" & Me.LogonType.ToString)
            'Else
            '    Me.LogonType = ZImpersonalizeAdvance.LogonType.LOGON32_LOGON_INTERACTIVE
            '    Trace.WriteLine("No se encontró el parámetro que define el tipo de logon." & vbCrLf & "Tipo de LOGON (defecto): 2")
            'End If
        Catch ex As Exception
            Trace.WriteLine(ex.Message)
        End Try

        Try
            Me.ExecuteClientAfterUpdate = CommandArgs(8)
            Trace.WriteLine("ExecuteClientAfterUpdate: " & ExecuteClientAfterUpdate)
            If String.IsNullOrEmpty(Me.ExecuteClientAfterUpdate) Then
                Me.ExecuteClientAfterUpdate = "true"
                Trace.WriteLine("El valor de ExecuteClientAfterUpdate no se encontraba configurado. Por defecto será true.")
            End If
        Catch ex As Exception
            Me.ExecuteClientAfterUpdate = "true"
            Trace.WriteLine("Ha ocurrido un error al recuperar el valor del parámetro ExecuteClientAfterUpdate. Por defecto será true.")
            Trace.WriteLine(ex.Message)
        End Try

        If Me.FlagIsZambaInstalled Then
            Me.Visible = False
            Aceptar_Click(arguments)
        Else
            Me.Visible = True
            Aceptar_Click(arguments)
        End If
    End Sub

    ''' <summary>
    ''' Metodo q actualiza los archivos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Aceptar_Click(ByVal arguments As String)
        Trace.WriteLine("---------------------------------------------")
        Trace.WriteLine("Comienza proceso de actualización.")

        If arguments <> String.Empty Then
            Dim flagILMFilesDeleted As Boolean = True
            Do
                Try
                    Me.DeleteFilesILM()
                    flagILMFilesDeleted = True
                Catch ex As Exception
                    Trace.WriteLine("Ha ocurrido un error:")
                    Trace.WriteLine(ex.Message)
                    flagILMFilesDeleted = True
                End Try

            Loop While Not flagILMFilesDeleted _
            AndAlso MessageBox.Show("Los archivos: " & Chr(13) & "ILM.Installed.dat" & Chr(13) & "ILM.Installed.dat" _
                                    & Chr(13) & "No han podido ser borrados. Presione OK para intentar de nuevo y Cancel para continuar con la Actualización.",
                                    "Zamba Software", MessageBoxButtons.OKCancel) = DialogResult.OK

            Try
                If Not Me.FlagIsZambaInstalled Then
                    Me.Actualizar(Me.PathOrigen, Me.PathDestino)
                    Me.pbProgresoUpdate.Step = Me.pbProgresoUpdate.Maximum
                    Me.flagCorrectUpdate = True
                End If
            Catch ex As Exception
                Trace.WriteLine(ex.Message)
                Me.pbProgresoUpdate.Step = Me.pbProgresoUpdate.Maximum
                Me.flagCorrectUpdate = False
                MessageBox.Show("Ha ocurrido un error en la Actualización." & vbCrLf & _
                                "Reinicie Zamba y si el problema persiste contacte a su Departamento de Sistemas.", _
                                "Actualización de Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            Dim frmInicial As Inicial = Nothing
            Try
                frmInicial = New Inicial(Usr, Pass, Domain, DecodePass)
                If frmInicial.ShowDialog() = DialogResult.OK Then
                    Me.flagCorrectUpdate = True
                Else
                    Throw frmInicial.Exception
                End If
            Catch ex As Exception
                Trace.WriteLine(ex.Message)
                Me.pbProgresoUpdate.Step = Me.pbProgresoUpdate.Maximum
                Me.flagCorrectUpdate = False
                MessageBox.Show("Ha ocurrido un error en las tareas de actualización secundarias de Zamba." & vbCrLf & _
                                "Es posible que algunos componentes que se comunican con Zamba no funcionen correctamente." & vbCrLf & _
                                "Se recomienda reiniciar Zamba y, en caso de que el problema persista, contactar a su Departamento de Sistemas.", _
                                "Actualización de Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If frmInicial IsNot Nothing Then
                    frmInicial.Dispose()
                    frmInicial = Nothing
                End If
            End Try

            Dim sr As IO.StreamReader = Nothing
            Dim programList As List(Of String) = Nothing

            Try
                'Permite ejecutar un exe al terminar la actualizacion de Zamba. el nombre del exe debe guardarse en initialexecute.dat
                Trace.WriteLine("Verificando archivos de ejecucion: " & Application.StartupPath)
                If IO.File.Exists(Application.StartupPath & "\initialexecute.dat") Then
                    sr = New IO.StreamReader(Application.StartupPath & "\initialexecute.dat")
                    programList = New List(Of String)
                    Dim flag As Boolean = False
                    Trace.WriteLine("Cargando lista de ejecución")
                    While Not sr.EndOfStream
                        Dim program As String = sr.ReadLine
                        programList.Add(program)
                        Trace.WriteLine("Comandos obtenidos: " & program)
                    End While

                    sr.Close()
                    Trace.WriteLine("Lista de ejecución terminada")
                    Trace.WriteLine("Cantidad de lineas a ejecutar: " & programList.Count.ToString())
                    If programList.Count > 0 Then
                        Trace.WriteLine("Ejecutando...")
                        Dim index As Integer = 0

                        If programList(index).Trim <> String.Empty Then

                            Dim dllAndParameters As String = String.Empty

                            Select Case programList(index).ToUpper
                                Case "UPDATENGEN"
                                    dllAndParameters = Application.StartupPath & "\UpToDate\Zamba.UpdateNGen.exe +" & Me.PathOrigen & " +" & Application.StartupPath & " +" & Application.StartupPath & "\Cliente.exe +false"
                                    Trace.WriteLine(" " & index.ToString() & " --> " & dllAndParameters)
                                    Shell(dllAndParameters, AppWinStyle.NormalFocus, False)
                                Case "UPDATE.EXPORTAOUTLOOK"
                                    dllAndParameters = Path.GetDirectoryName(Application.StartupPath) & "\exportaoutlook\uptodate\Zamba.Update.ExportaOutlook.exe "
                                    Trace.WriteLine(" " & index.ToString() & " --> " & dllAndParameters)
                                    Shell(dllAndParameters, AppWinStyle.NormalFocus, False)
                                Case "UPDATECLIENTNGEN"
                                    dllAndParameters = Application.StartupPath & "\Zamba.UpdateClientNGen.exe"
                                    Trace.WriteLine(" " & index.ToString() & " --> " & dllAndParameters)
                                    Shell(dllAndParameters, AppWinStyle.NormalFocus, False)
                                Case Else
                                    Trace.WriteLine(" " & index.ToString() & " --> " & programList(index).Trim())
                                    Shell(programList(index).Trim, AppWinStyle.Hide, False)
                            End Select
                        End If
                    End If
                End If
            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            Finally
                If programList IsNot Nothing Then
                    programList.Clear()
                    programList = Nothing
                End If
                Try
                    If sr IsNot Nothing Then
                        sr.Close()
                        sr.Dispose()
                        sr = Nothing
                    End If
                Catch
                End Try
            End Try

            Try
                If Not Me.FlagIsZambaInstalled Then
                    Trace.WriteLine("Not ZanbaIsInstalled. ExecuteClientAfterUpdate = " & ExecuteClientAfterUpdate)
                    If Not String.IsNullOrEmpty(ExecuteClientAfterUpdate) AndAlso ExecuteClientAfterUpdate.Trim.ToLower = "true" Then
                        Trace.WriteLine("Ejecutando el cliente nuevamente")
                        Me.BackToExecute(Me.PathBackToExecute.Replace("UpToDate", String.Empty), Me.flagCorrectUpdate)
                    Else
                        MessageBox.Show("Se ha configurado la actualización de Zamba para no abrir el cliente al finalizar el proceso." & vbCrLf & _
                                        "Para que la actualización del sistema finalice correctamente, es necesario que el cliente se abra desde el actualizador." & vbCrLf & _
                                        "Comuníquese con su área de Soporte Técnico para mayor información.", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Trace.WriteLine("# El cliente de zamba no será ejecutado. Esto no permitirá una correcta actualización del mismo, ya que para esto es necesario del parámetro CorrectUpdate.")
                        Trace.WriteLine("# Para solucionar este problema se debe configurar la variable ExecuteClientAfterUpdate de ZOPT en True.")
                    End If
                End If
            Catch ex As Exception
                Trace.WriteLine(ex.Message)
                MessageBox.Show("Error al abrir nuevamente Zamba", "Error en la actualización", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            Trace.WriteLine("Fin de la actualización. Cerrando componentes.")
        Else
            Trace.WriteLine("Actualización sin argumentos.")
            Dim pbStep As Int16
            Me.pbProgresoUpdate.Maximum = 4000
            Me.pbProgresoUpdate.Step = 0
            For pbStep = 0 To Convert.ToInt16(Me.pbProgresoUpdate.Maximum)
                Me.pbProgresoUpdate.PerformStep()
                Thread.Sleep(1)
            Next
            Me.pbProgresoUpdate.Step = Me.pbProgresoUpdate.Maximum
        End If

        Me.Close()
        Me.Dispose()
        Me.Finalize()
    End Sub

#End Region

#Region "Métodos"

    Private Sub Actualizar(ByVal strPathOrigen As String, ByVal strPathdestino As String)
        Dim success As Boolean = False

        If String.IsNullOrEmpty(Usr) OrElse String.IsNullOrEmpty(Pass) OrElse String.IsNullOrEmpty(Domain) Then
            Trace.WriteLine("Credenciales no encontradas, verifique que se hayan seteado usuario, password y dominio. Se procede a actualizar sin credenciales.")
            DoUpdate(strPathOrigen, strPathdestino)
        Else
            Trace.WriteLine("Usuario para actualizacion encontrado: " & Usr)

            If Not String.IsNullOrEmpty(DecodePass) Then
                Dim zImper As New ZImpersonalize

                Try
                    Trace.WriteLine("Impersonalizando updater")
                    success = zImper.impersonateValidUser(Usr, Domain, DecodePass)
                    If success Then
                        Trace.WriteLine("Updater impersonalizado")
                    Else
                        Trace.WriteLine("No se ha podido impesonalizar")
                    End If

                    DoUpdate(strPathOrigen, strPathdestino)
                Catch ex As Exception
                    Trace.WriteLine(ex.ToString)
                Finally
                    Trace.WriteLine("Desimpesonalizando updater")
                    Try
                        If success Then
                            Trace.WriteLine("Desimpesonalizando Actualizar")
                            Trace.WriteLine("Antes de desloguear: " & Environment.UserName)
                            zImper.undoImpersonation()
                            Trace.WriteLine("Despues de desloguear: " & Environment.UserName)
                            Trace.WriteLine("Actualizar desimpersonalizado")
                        End If
                    Catch ex As Exception
                        Trace.WriteLine(ex.Message)
                    End Try
                    zImper = Nothing
                End Try
            Else
                Trace.WriteLine("Password no decodeada")
            End If
        End If
    End Sub

    Private Sub DoUpdate(ByVal params As Object())
        DoUpdate(params(0), params(1))
    End Sub

    Private Sub DoUpdate(ByVal strPathOrigen As String, ByVal strPathdestino As String)
        Trace.WriteLine("*Comienzo de actualización:")
        Dim DirOrigen As New IO.DirectoryInfo(strPathOrigen)
        Trace.WriteLine("Ruta origen: " & strPathOrigen)
        Dim DirDestino As New IO.DirectoryInfo(strPathdestino)
        Trace.WriteLine("Ruta destino: " & strPathdestino)
        Try
           
            Me.pbProgresoUpdate.Maximum = Directory.GetFiles(DirOrigen.FullName, "*.*", SearchOption.AllDirectories).Length
            Dim proc As System.Diagnostics.Process

            'Se obtienen todos los ejecutables de Zamba que posiblemente puedan estar corriendo y se cierran.
            If strPathdestino.Contains("UpToDate") Then strPathdestino = strPathdestino.Replace("UpToDate", String.Empty)
            For Each exePath As String In IO.Directory.GetFiles(strPathdestino.Trim, "*.exe")
                If Not exePath.EndsWith("Zamba.UpdateNGen.exe") Then
                    For Each proc In System.Diagnostics.Process.GetProcessesByName(IO.Path.GetFileNameWithoutExtension(exePath))
                        Try
                            Trace.WriteLine("Cerrando " & exePath)
                            proc.Kill()
                        Catch ex As Exception
                            If Not proc.HasExited Then
                                If proc.WaitForExit(1000) = False Then
                                    Try
                                        Trace.WriteLine("Reintentando cerrar el proceso anterior...")
                                        proc.Kill()
                                    Catch ex2 As Exception
                                        Trace.WriteLine("El proceso no ha podido cerrarse.")
                                    End Try
                                End If
                            End If
                        End Try
                    Next
                End If
            Next
        Catch ex As Exception
            Trace.WriteLine(ex.Message)
        End Try

        CopySubDirAndFiles(DirOrigen, DirDestino)
    End Sub
    Sub CerrarProcesosZamba()

        Try
            Dim proc As System.Diagnostics.Process
            Dim strPathdestino As String = Application.StartupPath.ToString
            'Se obtienen todos los ejecutables de Zamba que posiblemente puedan estar corriendo y se cierran.
            For Each exePath As String In IO.Directory.GetFiles(strPathdestino.Trim, "*.exe")
                If Not exePath.EndsWith("Zamba.UpdateNGen.exe") Then
                    For Each proc In System.Diagnostics.Process.GetProcessesByName(IO.Path.GetFileNameWithoutExtension(exePath))

                        Try
                            Trace.WriteLine("Cerrando " & exePath)
                            Trace.WriteLine("Usuario actual con cual se cierra el proceso: " & Environment.UserName)
                            proc.Kill()
                            Trace.WriteLine("Cerrado Correctamente: " & exePath)
                        Catch ex As Exception
                            If Not proc.HasExited Then
                                If proc.WaitForExit(1000) = False Then
                                    Try
                                        Trace.WriteLine("Reintentando cerrar el proceso anterior...")
                                        proc.Kill()
                                    Catch ex2 As Exception
                                        Trace.WriteLine("El proceso no ha podido cerrarse.")
                                    End Try
                                End If
                            End If
                        End Try
                    Next
                End If
            Next

            If strPathdestino.Contains("\UpToDate") Then
                strPathdestino = Application.StartupPath.ToString.Replace("\UpToDate", " ")
            End If

            For Each exePath As String In IO.Directory.GetFiles(strPathdestino.Trim, "*.exe")
                If Not exePath.EndsWith("Zamba.UpdateNGen.exe") Then
                    For Each proc In System.Diagnostics.Process.GetProcessesByName(IO.Path.GetFileNameWithoutExtension(exePath))

                        Try
                            Trace.WriteLine("Cerrando " & exePath)
                            Trace.WriteLine("Usuario actual con cual se cierra el proceso: " & Environment.UserName)
                            proc.Kill()
                            Trace.WriteLine("Cerrado Correctamente: " & exePath)
                        Catch ex As Exception
                            If Not proc.HasExited Then
                                If proc.WaitForExit(1000) = False Then
                                    Try
                                        Trace.WriteLine("Reintentando cerrar el proceso anterior...")
                                        proc.Kill()
                                    Catch ex2 As Exception
                                        Trace.WriteLine("El proceso no ha podido cerrarse.")
                                    End Try
                                End If
                            End If
                        End Try
                    Next
                End If
            Next
        Catch ex As Exception
            Trace.WriteLine(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Copia los archivos del servidor a la pc del usuario
    ''' </summary>
    ''' <param name="DirOrigen"></param>
    ''' <param name="DirDestino"></param>
    ''' <history>Marcelo modified 23/03/09</history>
    ''' <remarks></remarks>
    Private Sub CopySubDirAndFiles(ByVal DirOrigen As IO.DirectoryInfo, ByVal DirDestino As IO.DirectoryInfo)
        If DirDestino.Exists = False Then
            Trace.WriteLine("Intentando crear directorio: " & DirDestino.FullName)
            DirDestino.Create()
        End If
        Trace.WriteLine("Obteniendo cantidad de archivos en origen")
        Dim archivos() As IO.FileInfo = DirOrigen.GetFiles
        Trace.WriteLine("Cantidad de archivos: " & archivos.Length)
        Dim i As Int32
        Dim QArchivos As Int32 = archivos.Length
        For i = 0 To QArchivos - 1
            If LCase(archivos(i).Name) <> "app.ini" AndAlso String.Compare(LCase(archivos(i).Name), "execute.dat") <> 0 Then   'Valido que no copie el App.ini
                Try
                    If (Not archivos(i).Name.ToLower().Contains("updatengen")) Then
                        Trace.WriteLine("Copiando archivo: " & archivos(i).Name)
                        archivos(i).CopyTo(DirDestino.FullName & "\" & archivos(i).Name, True)
                        Me.pbProgresoUpdate.PerformStep()
                    End If
                Catch ex As Exception
                    Dim fullname As String = archivos(i).FullName
                    Dim name As String = archivos(i).Name
                    Try
                        Trace.WriteLine("Verificando nombre de archivo")
                        If archivos(i).Name.ToUpper = "CLIENTE" Or archivos(i).Name.ToUpper = "CLIENTE.EXE" Then
                            Trace.WriteLine("El archivo es el cliente, cerrando procesos")
                            CloseProcesses()
                            GC.Collect()

                            Thread.Sleep(5000)

                            Try
                                Trace.WriteLine("Cambiando atributo: " & archivos(i).Name)
                                Dim fi As New IO.FileInfo(DirDestino.FullName & "\" & archivos(i).Name)
                                fi.Attributes = IO.FileAttributes.Normal
                                fi = Nothing

                                Trace.WriteLine("Borrando el cliente")
                                System.IO.File.Delete(DirDestino.FullName & "\" & name)
                            Catch ex3 As Exception
                                Trace.WriteLine("El archivo (" & fullname & ") no pudo cambiarse el atributo o borrarse. Error original: " & ex.Message)

                            End Try

                            Trace.WriteLine("Copiando archivo: " & DirDestino.FullName & "\" & name)
                            archivos(i).CopyTo(DirDestino.FullName & "\" & name, True)
                        Else
                            archivos(i).CopyTo(DirDestino.FullName & "\" & "failed" & archivos(i).Name, True)
                            Trace.WriteLine("El archivo (" & archivos(i).Name & ") no pudo ser copiado [se genera un failed]. Error: " & ex.Message)
                        End If
                        Me.pbProgresoUpdate.PerformStep()
                    Catch ex2 As Exception
                        Trace.WriteLine("El archivo (" & fullname & ") no pudo ser copiado. Error original: " & ex.Message)
                        Trace.WriteLine("El archivo (" & fullname & ") no pudo ser copiado. Error verificacion: " & ex2.ToString())
                        Me.pbProgresoUpdate.PerformStep()
                    End Try
                End Try
                Application.DoEvents()
            End If
        Next
        Try
            Dim SubDir() As IO.DirectoryInfo = DirOrigen.GetDirectories()
            For i = 0 To SubDir.Length - 1
                Dim SubDirDestino As New IO.DirectoryInfo(DirDestino.FullName & "\" & SubDir(i).Name)
                Trace.WriteLine("Copiando archivos de directorio: " & SubDirDestino.ToString())
                CopySubDirAndFiles(SubDir(i), SubDirDestino)
            Next
        Catch
        End Try
    End Sub

    Private Sub BackToExecute(ByVal strPathToExecute As String, ByVal blnCorrectUpdate As Boolean)
        If blnCorrectUpdate Then
            Trace.WriteLine("Ejecutando (" & strPathToExecute & ") con parámetro 'CorrectUpdate'")
            Shell(strPathToExecute & " CorrectUpdate", AppWinStyle.NormalFocus)
        Else
            Trace.WriteLine("Ejecutando (" & strPathToExecute & ") con parámetro 'WrongUpdate'")
            Shell(strPathToExecute & " WrongUpdate", AppWinStyle.NormalFocus)
        End If
    End Sub

    Private Sub DeleteFilesILM()
        Trace.WriteLine("*Borrado de archivos ILM:")
        Trace.WriteLine("ILM.Installed.dat")
        Dim success As Boolean = False

        If File.Exists(Me.PathDestino & "\ILM.Installed.dat") Then
            Trace.WriteLine("El archivo ILM.Installed.dat existe en la ruta:")
            Trace.WriteLine(Me.PathDestino & "\ILM.Installed.dat")

            'Verifico si Las credenciales fueron encontradas
            If String.IsNullOrEmpty(usr) OrElse String.IsNullOrEmpty(pass) OrElse String.IsNullOrEmpty(domain) Then
                Trace.WriteLine("Credenciales no encontradas, verifique que se hayan seteado usuario, password y dominio. Se intentará borrar sin credenciales.")
                DeleteFiles(New Object() {Me.PathDestino & "\ILM.Installed.dat"})
            Else
                'Si fueron encontradas Impersonalizo y sigo con el proceso
                If Not String.IsNullOrEmpty(decodePass) Then
                    Dim zImper As New ZImpersonalize
                    Try
                        Trace.WriteLine("Impersonalizando DeleteFilesILM ILM.Installed.dat")
                        success = zImper.impersonateValidUser(Usr, Domain, DecodePass)
                        If success Then
                            Trace.WriteLine("DeleteFilesILM ILM.Installed.dat impersonalizado")
                            Try
                                File.Delete(Me.PathDestino & "\ILM.Installed.dat")
                            Catch ex As Exception
                                Trace.WriteLine(ex.Message)
                                Trace.WriteLine("El archivo '" & Me.PathDestino & "\ILM.Installed.dat No ha sido borrado.")
                            End Try
                            Trace.WriteLine("El archivo '" & Me.PathDestino & "\ILM.Installed.dat ha sido borrado correctamente.")
                        Else
                            Trace.WriteLine("No se ha podido impesonalizar")
                            File.Delete(Me.PathDestino & "\ILM.Installed.dat")
                        End If
                    Catch ex As Exception

                        Trace.WriteLine(ex.Message)
                    Finally
                        Try
                            If success Then
                                Trace.WriteLine("Desimpesonalizando DeleteFilesILM ILM.Installed.dat")
                                Trace.WriteLine("Antes de desloguear: " & Environment.UserName)
                                zImper.undoImpersonation()
                                Trace.WriteLine("Despues de desloguear: " & Environment.UserName)
                                Trace.WriteLine("DeleteFilesILM ILM.Installed.dat desimpersonalizado")
                            End If

                        Catch ex As Exception
                            Trace.WriteLine(ex.Message)
                        End Try

                        zImper = Nothing
                    End Try

                End If
            End If
        Else
            Trace.WriteLine("El archivo ILM.Installed.dat no existe en la ruta:")
            Trace.WriteLine(Me.PathDestino & "\ILM.Installed.dat")
            Dim newPath As String = Me.PathDestino.Remove(Me.PathDestino.LastIndexOf("\"))
            If File.Exists(newPath & "\ILM.Installed.dat") Then

                Trace.WriteLine("El archivo ILM.Installed.dat existe en la ruta:")
                Trace.WriteLine(newPath & "\ILM.Installed.dat")

                'Verifico si Las credenciales fueron encontradas
                If String.IsNullOrEmpty(usr) OrElse String.IsNullOrEmpty(pass) OrElse String.IsNullOrEmpty(domain) Then
                    Trace.WriteLine("Credenciales no encontradas, verifique que se hayan seteado usuario, password y dominio. Se intentará borrar sin credenciales.")
                    DeleteFiles(New Object() {newPath & "\ILM.Installed.dat"})
                Else
                    'Si fueron encontradas Impersonalizo y sigo con el proceso
                    If Not String.IsNullOrEmpty(decodePass) Then
                        Dim zImper As New ZImpersonalize
                        Try
                            Trace.WriteLine("Impersonalizando DeleteFilesILM ILM.Installed.dat")
                            success = zImper.impersonateValidUser(Usr, Domain, DecodePass)
                            If success Then
                                Trace.WriteLine("DeleteFilesILM ILM.Installed.dat impersonalizado")
                                Try
                                    File.Delete(newPath & "\ILM.Installed.dat")
                                Catch ex As Exception
                                    Trace.WriteLine(ex.Message)
                                    Trace.WriteLine("El archivo '" & newPath & "\ILM.Installed.dat No ha sido borrado.")
                                End Try
                                Trace.WriteLine("El archivo '" & newPath & "\ILM.Installed.dat ha sido borrado correctamente.")
                            Else
                                Trace.WriteLine("No se ha podido impesonalizar")
                                File.Delete(newPath & "\ILM.Installed.dat")
                            End If
                        Catch ex As Exception
                            Trace.WriteLine(ex.Message)
                        Finally
                            Try
                                If success Then
                                    Trace.WriteLine("Desimpesonalizando DeleteFilesILM ILM.Installed.dat")
                                    Trace.WriteLine("Antes de desloguear: " & Environment.UserName)
                                    zImper.undoImpersonation()
                                    Trace.WriteLine("Despues de desloguear: " & Environment.UserName)
                                    Trace.WriteLine("DeleteFilesILM ILM.Installed.dat desimpersonalizado")
                                End If
                            Catch ex As Exception
                                Trace.WriteLine(ex.Message)
                            End Try
                            zImper = Nothing
                        End Try
                    End If
                End If
            Else
                Trace.WriteLine("El archivo ILM.Installed.dat no existe en la ruta:")
                Trace.WriteLine(newPath & "\ILM.Installed.dat")
            End If

        End If

        Trace.WriteLine("ILM.Install.dat")

        If File.Exists(Me.PathDestino & "\ILM.Install.dat") Then
            Trace.WriteLine("El archivo ILM.Install.dat existe en la ruta:")
            Trace.WriteLine(Me.PathDestino & "\ILM.Install.dat")

            'Verifico si Las credenciales fueron encontradas
            If String.IsNullOrEmpty(Usr) OrElse String.IsNullOrEmpty(Pass) OrElse String.IsNullOrEmpty(Domain) Then
                Trace.WriteLine("Credenciales no encontradas, verifique que se hayan seteado usuario, password y dominio. Se intentará borrar sin credenciales.")
                DeleteFiles(New Object() {Me.PathDestino & "\ILM.Install.dat"})
            Else
                If Not String.IsNullOrEmpty(DecodePass) Then
                    Dim zImper As New ZImpersonalize
                    Try

                        Trace.WriteLine("Impersonalizando DeleteFilesILM ILM.Install.dat")
                        success = zImper.impersonateValidUser(Usr, Domain, DecodePass)
                        If success Then
                            Trace.WriteLine("DeleteFilesILM ILM.Install.dat impersonalizado")
                            Try
                                File.Delete(Me.PathDestino & "\ILM.Install.dat")
                            Catch ex As Exception
                                Trace.WriteLine(ex.Message)
                                Trace.WriteLine("El archivo '" & Me.PathDestino & "\ILM.Install.dat No ha sido borrado.")
                            End Try
                            Trace.WriteLine("El archivo '" & Me.PathDestino & "\ILM.Install.dat ha sido borrado correctamente.")
                        Else
                            Trace.WriteLine("No se ha podido impesonalizar")
                        End If
                    Catch ex As Exception
                        Trace.WriteLine(ex.Message)
                    Finally
                        Try
                            If success Then
                                Trace.WriteLine("Desimpesonalizando DeleteFilesILM ILM.Install.dat")
                                Trace.WriteLine("Antes de desloguear: " & Environment.UserName)
                                zImper.undoImpersonation()
                                Trace.WriteLine("Despues de desloguear: " & Environment.UserName)
                                Trace.WriteLine("DeleteFilesILM ILM.Install.dat desimpersonalizado")
                            End If
                        Catch ex As Exception
                            Trace.WriteLine(ex.Message)
                        End Try
                        zImper = Nothing
                    End Try
                End If
            End If

        Else
            Trace.WriteLine("El archivo ILM.Install.dat no existe en la ruta:")
            Trace.WriteLine(Me.PathDestino & "\ILM.Install.dat")
            Dim newPath As String = Me.PathDestino.Remove(Me.PathDestino.LastIndexOf("\"))
            If File.Exists(newPath & "\ILM.Install.dat") Then
                Trace.WriteLine("El archivo ILM.Install.dat existe en la ruta:")
                Trace.WriteLine(newPath & "\ILM.Install.dat")

                'Verifico si Las credenciales fueron encontradas
                If String.IsNullOrEmpty(Usr) OrElse String.IsNullOrEmpty(Pass) OrElse String.IsNullOrEmpty(Domain) Then
                    Trace.WriteLine("Credenciales no encontradas, verifique que se hayan seteado usuario, password y dominio. Se intentará borrar sin credenciales.")
                    DeleteFiles(New Object() {newPath & "\ILM.Install.dat"})
                Else
                    If Not String.IsNullOrEmpty(DecodePass) Then
                        Dim zImper As New ZImpersonalize
                        Try
                            Trace.WriteLine("Impersonalizando DeleteFilesILM ILM.Install.dat")
                            success = zImper.impersonateValidUser(Usr, Domain, DecodePass)
                            If success Then
                                Trace.WriteLine("DeleteFilesILM ILM.Install.dat impersonalizado")
                                Try
                                    File.Delete(newPath & "\ILM.Install.dat")
                                Catch ex As Exception
                                    Trace.WriteLine(ex.Message)
                                    Trace.WriteLine("El archivo '" & newPath & "\ILM.Install.dat No ha sido borrado.")
                                End Try
                                Trace.WriteLine("El archivo '" & newPath & "\ILM.Install.dat ha sido borrado correctamente.")
                            Else
                                Trace.WriteLine("No se ha podido impesonalizar")
                            End If
                        Catch ex As Exception
                            Trace.WriteLine(ex.Message)
                        Finally
                            Try
                                If success Then
                                    Trace.WriteLine("Desimpesonalizando DeleteFilesILM ILM.Install.dat")
                                    Trace.WriteLine("Antes de desloguear: " & Environment.UserName)
                                    zImper.undoImpersonation()
                                    Trace.WriteLine("Despues de desloguear: " & Environment.UserName)
                                    Trace.WriteLine("DeleteFilesILM ILM.Install.dat desimpersonalizado")
                                End If
                            Catch ex As Exception
                                Trace.WriteLine(ex.Message)
                            End Try
                            zImper = Nothing
                        End Try
                    End If
                End If
            Else
                Trace.WriteLine("El archivo ILM.Install.dat no existe en la ruta:")
                Trace.WriteLine(newPath & "\ILM.Install.dat")
            End If
        End If
    End Sub

    Private Sub DeleteFiles(ByVal params As Object())
        Try
            File.Delete(params(0))
            Trace.WriteLine("El archivo '" & params(0) & "' ha sido borrado correctamente.")
        Catch ex As Exception
            Trace.WriteLine(ex.Message)
            Trace.WriteLine("El archivo '" & params(0) & "' No ha sido borrado.")
        End Try
    End Sub

    Public Shared Function DecryptString(ByVal sOut As String, ByVal sKey As Byte(), ByVal IV As Byte()) As String
        'Servicio de encriptación
        Dim DES As New System.Security.Cryptography.RijndaelManaged
        'Verifico la clave y el vector

        If sKey.GetUpperBound(0) <> 15 Or IV.GetUpperBound(0) <> 15 Then
            Throw New ArgumentOutOfRangeException("Iv o Key")
        End If

        'Variable a donde se pone el resultado
        Dim Result As String

        DES.Mode = CipherMode.ECB
        Dim DESDecrypt As ICryptoTransform
        ' creo el  decryptor.
        DESDecrypt = DES.CreateDecryptor(sKey, IV)
        Try
            Dim Buffer As Byte() = Convert.FromBase64String(sOut)
            ' Transform and return the string.
            Result = (System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length)))
        Catch ex As Exception
            Result = String.Empty
        End Try

        Return Result
    End Function


#Region "WI 8801 - Validacion independiente"
    ''' <summary>
    ''' Se fuerza la actualizacion del compilado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StartActions()
        Try
            Dim updatePath As String = String.Empty

            'Borra los archivos fallidos de actualizaciones anteriores
            Try

                If String.IsNullOrEmpty(usr) OrElse String.IsNullOrEmpty(pass) OrElse String.IsNullOrEmpty(domain) Then
                    Trace.WriteLine("Credenciales no encontradas, verifique que se hayan seteado usuario, password y dominio")
                    Me.DeleteFailedFiles(Application.StartupPath)
                Else
                    Trace.WriteLine("Usuario para actualizacion encontrado: " & usr)
                    Dim success As Boolean = False
                    If Not String.IsNullOrEmpty(DecodePass) Then
                        Dim zImper As New ZImpersonalize
                        Try
                            Trace.WriteLine("Impersonalizando updater")
                            success = zImper.impersonateValidUser(Usr, Domain, DecodePass)
                            If success Then
                                Trace.WriteLine("Updater impersonalizado")
                            Else
                                Trace.WriteLine("No se ha podido impesonalizar")
                            End If

                            Me.DeleteFailedFiles(Application.StartupPath)
                        Catch ex As Exception
                            Trace.WriteLine(ex.Message)
                        Finally
                            Trace.WriteLine("Desimpesonalizando updater")
                            Try
                                Trace.WriteLine("Antes de desloguear" & Environment.UserName)
                                zImper.undoImpersonation()
                                Trace.WriteLine("Despues de desloguear" & Environment.UserName)
                            Catch ex As Exception
                                Trace.WriteLine(ex.Message)
                            End Try
                            Trace.WriteLine("Updater desimpersonalizado")
                            zImper = Nothing
                        End Try
                    Else
                        Trace.WriteLine("Password no decodeada")
                    End If
                End If
            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try

            Try
                Me.UpdateFiles(updatePath)
            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try
        Catch ex As Threading.ThreadAbortException
            MessageBox.Show("ERROR 2 no controlado: " & ex.ToString)
        Catch ex As Threading.ThreadInterruptedException
            MessageBox.Show("ERROR 3 no controlado: " & ex.ToString)
        Catch ex As Threading.ThreadStateException
            MessageBox.Show("ERROR 4 no controlado: " & ex.ToString)
        Catch ex As Threading.SynchronizationLockException
            MessageBox.Show("ERROR 5 no controlado: " & ex.ToString)
        Catch ex As Exception
            MessageBox.Show("ERROR 1 no controlado: " & ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Actualiza los archivos
    ''' </summary>
    ''' <param name="Updatepath"></param>
    ''' <remarks></remarks>
    Private Sub UpdateFiles(ByVal Updatepath As String)
        If Not Directory.Exists(Updatepath) Then
            MessageBox.Show("No se puede conectar con el servidor. Path de origen inexistente. Imposible actualizar", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Actualizar(" +" & Updatepath & " +" & Application.StartupPath.Replace("UpToDate", String.Empty) & " +" & Application.StartupPath & "\Cliente.exe +false")
        End If
    End Sub

    Private Sub DeleteFailedFiles(ByVal params As Object())
        DeleteFailedFiles(params(0))
    End Sub

    ''' <summary>
    ''' Borra los archivos fallidos de actualizaciones anteriores
    ''' </summary>
    ''' <param name="path"></param>
    ''' <remarks></remarks>
    Private Sub DeleteFailedFiles(ByVal path As String)
        Trace.WriteLine("Borrando los archivos fallidos de actualizaciones anteriores")
        Dim dir As New IO.DirectoryInfo(path)
        Dim files() As IO.FileInfo = dir.GetFiles("failed*.*")
        Dim file As IO.FileInfo
        For Each file In files
            Try
                file.Attributes = FileAttributes.Normal
                file.Delete()
            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try
        Next
        Dim directories() As IO.DirectoryInfo = dir.GetDirectories
        Dim dire As IO.DirectoryInfo
        For Each dire In directories
            DeleteFailedFiles(dire.FullName)
        Next
    End Sub
#End Region
#End Region

End Class

