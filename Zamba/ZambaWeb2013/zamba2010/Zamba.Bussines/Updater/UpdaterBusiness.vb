Imports Zamba.Data
Imports ILM.LotusLibrary

'''<summary> Clase que agrupa los métodos usados para la actualización de Zamba</summary>
'''<history>[Alejandro]</history>
Public Class UpdaterBusiness

    '''<summary> Permite ejecutar un exe al terminar la actualizacion de Zamba. 
    '''el nombre del exe debe guardarse en initialexecute.dat</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub FinalizarUpdate()
        Try
            If IO.File.Exists(Membership.MembershipHelper.AppConfigPath & "\initialexecute.dat") Then
                Dim sr As New IO.StreamReader(Membership.MembershipHelper.AppConfigPath & "\initialexecute.dat")
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
            lastestVersion = UpdaterFactory.GetLastestVersionFromVerreg()
        If String.IsNullOrEmpty(lastestVersion) Then
            Return "Desconocida"
        Else
            Return lastestVersion
        End If
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
            lastestVersion = GetLastestVersion()
            userVersion = GetVersionByEstreg()

            If userVersion = "Desconocida" Then
                Return False
            ElseIf lastestVersion = "Desconocida" Then
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

    '''<summary>Obtiene, por Reflection, la versión del ensamblado pasado por parámetro</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetVersion(ByVal dllName As String) As String
        Return UpdaterFactory.GetVersionByReflection(dllName)
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
    Public Shared Sub UpdateEstreg(ByVal strVersion As String, ByVal strMachineName As String)
        Try
            UpdaterFactory.UpdateEstreg(strVersion, strMachineName)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    '''<summary>Actualiza la tabla ESTREG con el valor de Version pasado por parámetro y el valor de
    '''máquina obtenido por Reflection (environment - current user)</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub UpdateEstreg(ByVal strVersion As String)
        Try
            UpdaterFactory.UpdateEstreg(strVersion, Environment.MachineName)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    '''<summary>Inserta un nuevo registro en la tabla ESTREG con los valores de ID, Nombre de Máquina,
    '''Nombre de Usuario y Versión pasados por parámetro</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub SetEstreg(ByVal strLastId As String, ByVal strMachineName As String, ByVal strUserName As String, ByVal strVersion As String)
        Try
            UpdaterFactory.SetEstreg(strLastId, strMachineName, strUserName, strVersion)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    '''<summary>Inserta un nuevo registro en la tabla ESTREG con los valores de ID y Versión
    '''pasados por parámetro. Los valores de Nombre de Máquina y Nombre de Usuario los obtiene
    ''' por Reflection (environment - current user)</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub SetEstreg(ByVal strLastId As String, ByVal strVersion As String)
        Try
            UpdaterFactory.SetEstreg(strLastId, Environment.MachineName, Environment.UserName, strVersion)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    '''<summary>Inserta un nuevo registro en la tabla ESTREG. Los valores de Nombre de Máquina, 
    ''' Nombre de Usuario y Versión los obtiene por Reflection (environment - current user)
    '''</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub SetEstreg()
        Dim strLastId As String
        Dim strVersion As String
        Dim maxID As Int64 = GetMaxIdFromEstreg() + 1
        strLastId = maxID.ToString()
        strVersion = GetVersion("Cliente.exe")
        strVersion = strVersion.Replace(".", "")
        UpdaterFactory.SetEstreg(strLastId, Environment.MachineName, Environment.UserName, strVersion)
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

    '''<summary>Obtiene la versión de la tabla ESTREG para el Nombre de Máquina obtenido
    ''' por Reflection (Environmet - Current User)</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetVersionByEstreg() As String
        Dim sVersion As String = String.Empty
        Dim ds As New DataSet()
            ds = UpdaterFactory.GetIdFromEstregWhereMName(Environment.MachineName)

        If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) Then
            If ds.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In ds.Tables(0).Rows
                    sVersion = r("ver").ToString()
                Next
            Else
                SetEstreg()
                sVersion = GetVersionByEstreg()
           End If
        End If
        Return sVersion
    End Function

    '''<summary>Obtiene la versión de la tabla ESTREG donde el nombre de máquina pasado
    '''por parámetro.</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetVersionByEstreg(ByVal strMachineName As String) As String
        Dim sVersion As String = "Desconocida"
        Dim ds As New DataSet()
        Try
            ds = UpdaterFactory.GetIdFromEstregWhereMName(strMachineName)

            If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In ds.Tables(0).Rows
                    sVersion = r("ver").ToString()
                Next
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









End Class
