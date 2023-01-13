Imports Zamba.Data
Imports ILM.LotusLibrary
Imports Zamba.Servers

'''<summary> Clase que agrupa los métodos usados para la actualización de Zamba</summary>
'''<history>[Alejandro]</history>
Public Class UpdaterBusiness

    '''<summary> Permite ejecutar un exe al terminar la actualizacion de Zamba. 
    '''el nombre del exe debe guardarse en initialexecute.dat</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub FinalizarUpdate()
        Try
            If IO.File.Exists(System.Windows.Forms.Application.StartupPath & "\initialexecute.dat") Then
                Dim sr As New IO.StreamReader(System.Windows.Forms.Application.StartupPath & "\initialexecute.dat")
                Dim program As String = sr.ReadLine
                sr.Close()
                If program.Trim <> String.Empty Then
                    Shell(program.Trim, AppWinStyle.Hide, False)
                End If
            End If
        Catch
        End Try
    End Sub

    '''<summary>Obtiene el valor 'Version' de la tabla VERREG</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetLastestVersion() As String
        Dim lastestVersion As String
        Try
            lastestVersion = UpdaterFactory.GetLastestVersionFromVerreg()
            If String.IsNullOrEmpty(lastestVersion) Then
                Return "Desconocida"
            Else
                Return lastestVersion
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return String.Empty
        End Try
    End Function

    '''<summary>Toma el valor de versión del Cliente por Reflection y el valor de la última
    '''versión del Cliente de la tabla VERREG, luego las compara:
    '''TRUE: La versión de Cliente es la última
    '''FALSE: La versión del Cliente es menor a la última o alguna de las dos versiones es 'Desconocida' [Alejandro]</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function IsLastestVersion() As Boolean
        Dim lastestVersion As String
        Dim userVersion As String
        Try
            Dim CurrentUser As IUser = Membership.MembershipHelper.CurrentUser
            If CurrentUser IsNot Nothing Then
                lastestVersion = GetLastestVersion()
                userVersion = GetVersionByEstreg(CurrentUser.Name)
                userVersion = userVersion.Replace(".", "")
                lastestVersion = lastestVersion.Replace(".", "")
            End If

            If userVersion Is Nothing OrElse lastestVersion Is Nothing Then
                Return True
            ElseIf userVersion = "Desconocida" Then
                Return False
            ElseIf lastestVersion = "Desconocida" Then
                Return False
            ElseIf userVersion.Length = 0 OrElse lastestVersion.Length = 0 Then
                Return False
            ElseIf Convert.ToInt32(userVersion) < Convert.ToInt32(lastestVersion) Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return True
        End Try
    End Function



    ''' <summary>
    ''' Verifica si es la primera vez que Zamba se ejecuta en el puesto de usuario
    ''' </summary>
    ''' <returns>True si el puesto es nuevo</returns>
    ''' <remarks>Lo que hace es buscar el puesto de usuario en la tabla que guarda los puestos instalados. Si no existe devuelve True.</remarks>
    Public Shared Function IsNewHost(version As String) As Boolean
        If String.Compare(GetVersionByEstreg(version), "Desconocida") = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    '''<summary>Obtiene, por Reflection, la versión del ensamblado pasado por parámetro</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetVersion(ByVal dllName As String) As String
        Return UpdaterFactory.GetVersionByReflection(dllName)
        Dim lastestVersion As String
        'lastestVersion = UpdaterFactory.GetVersion()
        If String.IsNullOrEmpty(lastestVersion) Then
            Return "Desconocida"
        Else
            Return lastestVersion
        End If
    End Function
    Public Shared Function GetVersion() As String
        'Return UpdaterFactory.GetVersionByEstreg()
        Dim lastestVersion As String = UpdaterFactory.GetVersionByEstreg()

        If String.IsNullOrEmpty(lastestVersion) Then
            Return "Desconocida"
        Else
            Return lastestVersion
        End If
    End Function

    '''<summary>Obtiene todas las características de la última versión (Version, Path donde se encuentra y Script File)</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub GetLastestVersion(ByRef strLastestVersion As String, ByRef strUpdatePath As String, ByRef strCommand As String)
        Try
            UpdaterFactory.GetLastestVersion(strLastestVersion, strUpdatePath, strCommand)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    '''<summary>Actualiza la tabla ESTREG con los valores de Version y Nombre de Máquina pasados por parámetro.</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub UpdateEstreg(ByVal strVersion As String, ByVal strMachineName As String, ByVal WinUser As String)
        Try
            UpdaterFactory.UpdateEstreg(strVersion, strMachineName, WinUser)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    '''<summary>Actualiza la tabla ESTREG con el valor de Version pasado por parámetro y el valor de
    '''máquina obtenido por Reflection (environment - current user)</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub UpdateEstreg(ByVal strVersion As String, ByVal WinUser As String)
        Try
            UpdaterFactory.UpdateEstreg(strVersion, Environment.MachineName, WinUser)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub SetEstreg(ByVal serverVersion As String)
        Dim maxID As Int64 = GetMaxIdFromEstreg() + 1
        ZTrace.WriteLineIf(ZTrace.IsInfo, "# Registrando en estreg el puesto:")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "- ID: " & maxID.ToString)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "- Puesto: " & Environment.MachineName)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "- Usuario de windows: " & Environment.UserName)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "- Versión de Zamba: " & serverVersion)
        UpdaterFactory.SetEstreg(maxID.ToString(), Environment.MachineName, Environment.UserName, serverVersion)
    End Sub

    '''<summary>Obtiene todos los registros de la tabla VERREG</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetVerreg() As DataSet
        Return UpdaterFactory.GetVerreg()
    End Function

    '''<summary>Obtiene todos los IDs de la tabla ESTREG donde el nombre de máquina pasado
    '''por parámetro.</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetIdFromEstregWhereMName(ByVal strMachineName As String) As DataSet
        Return UpdaterFactory.GetIdFromEstregWhereMName(strMachineName)
    End Function

    '''<summary>Obtiene todos los IDs de la tabla ESTREG para el Nombre de Máquina obtenido
    ''' por Reflection (Environmet - Current User)</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetIdFromEstregWhereMName() As DataSet
        Return UpdaterFactory.GetIdFromEstregWhereMName(Environment.MachineName)
    End Function

    ''' <summary>
    ''' Obtiene la versión de Zamba del puesto actual desde base de datos
    ''' </summary>
    ''' <returns>Devuelve un numero en caso de encontrar un resultado. Caso contrario devuelve la palabra 'Desconocida'</returns>
    ''' <remarks></remarks>
    Public Shared Function GetVersionByEstreg(version As String) As String
        Return GetVersionByEstreg(Environment.MachineName, version)
    End Function

    '''<summary>Obtiene la versión de la tabla ESTREG donde el nombre de máquina pasado
    '''por parámetro.</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetVersionByEstreg(ByVal strMachineName As String, ByVal version As String) As String
        Dim sVersion As String

        Try
            sVersion = UpdaterFactory.GetMachineZambaVersion(strMachineName)

            'Verifica si el puesto se encontraba registrado
            If IsDBNull(sVersion) OrElse String.IsNullOrEmpty(sVersion) Then
                'Marca al puesto como desactualizado ya que es posible que se encuentre  
                'ejecutando una versión anterior a la que se encuentra en el servidor
                sVersion = "Desconocida"
            Else
                'Actualiza la fecha de verificación de versión
                UpdaterFactory.UpdateLastVerified(strMachineName, version)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return sVersion
    End Function

    '''<summary>Borra todos los registros de la tabla ESTREG donde el Nombre de Máquina
    '''pasado por parámetro.</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub DeleteFromEstregWhereMName(ByVal strMachineName As String)
        UpdaterFactory.DeleteFromEstregWhereMName(strMachineName)
    End Sub

    '''<summary>Borra todos los registros de la tabla ESTREG donde el Nombre de Máquina
    '''obtenido por Reflection (Environment - Current User).</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub DeleteFromEstregWhereMName()
        UpdaterFactory.DeleteFromEstregWhereMName(Environment.MachineName)
    End Sub

    '''<summary>Obtiene el ID máximo de la Tabla ESTREG</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetMaxIdFromEstreg() As Int64
        Return UpdaterFactory.GetMaxIdFromEstreg()
    End Function

#Region "Lotus"

    '''<summary> Abre el Lotus y realiza las actualizaciones</summary>
    '''<history> Hernan </history>
    Public Shared Sub LotusUpdate(DateConfig As String, DateTimeConfig As String)
        Try
            If Install.Instalar(DateConfig, DateTimeConfig) = False Then
                Install.ReInstalar()
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        End Try
    End Sub

#End Region


    ''' <summary>
    ''' Obtiene el historial de todos los scripts ejecutados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetScriptsHistory() As DataTable
        Dim ds As DataSet = UpdaterFactory.GetScriptsHistory()

        Return ds.Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene si el id fue ejecutado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetScriptIDExecuted(ByVal id As Int64) As Boolean
        If UpdaterFactory.GetScriptIDExecuted(id) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Guarda en el historial la ejecucion de un script
    ''' </summary>
    ''' <param name="id">ID del script</param>
    ''' <param name="name">Nombre del script</param>
    ''' <param name="Resultado">Resultado del script</param>
    ''' <remarks></remarks>
    Public Shared Sub SaveScriptHistory(ByVal id As Int64, ByVal name As String, Resultado As String)
        UpdaterFactory.SaveScriptHistory(id, name, Membership.MembershipHelper.CurrentUser.Name, Resultado)
    End Sub

    ''' <summary>
    ''' Ejecuta la consulta
    ''' </summary>
    ''' <param name="query"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteQuery(con As IConnection, ByVal query As String) As Boolean
        Dim Transact As New Transaction(con)
        Try
            query = query.Replace(vbCrLf, " ")
            query = query.Replace(" GO ", "ð")
            For Each s As String In query.Split("ð")
                If String.IsNullOrEmpty(s.Trim) = False Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Consulta a ejecutar: " & s)
                    'Transact.Con.ExecuteNonQuery(Transact.Transaction, CommandType.Text, s)
                End If
            Next
        Catch ex As Exception
            Transact.Rollback()
            Throw ex
        End Try
        Transact.Commit()
    End Function
End Class
