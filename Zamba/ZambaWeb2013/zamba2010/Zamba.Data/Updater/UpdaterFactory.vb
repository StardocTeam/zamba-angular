Imports Zamba.Servers


'''<summary> Clase que agrupa los métodos usados para la actualización de Zamba</summary>
'''<history>[Alejandro]</history>
Public Class UpdaterFactory


    '''<summary>Obtiene, por Reflection, la versión del ensamblado pasado por parámetro</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetVersionByReflection(ByVal dllName As String) As String
        Dim dll As System.Reflection.Assembly = System.Reflection.Assembly.LoadFile(dllName)
        Dim Version As String = dll.GetName().Version.ToString()
        Return Version
    End Function

    '''<summary>Obtiene todas las características de la última versión (Version, Path donde se encuentra y Script File)</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub GetLastestVersion(ByRef strLastestVersion As String, ByRef strUpdatePath As String, ByRef strCommand As String)

        Dim dsVersiones As New DataSet

        dsVersiones = Server.Con.ExecuteDataset(CommandType.Text, "select ver,path,scriptfile,id from verreg")

        If Not IsDBNull(dsVersiones) AndAlso Not IsNothing(dsVersiones.Tables(0)) AndAlso dsVersiones.Tables(0).Rows.Count > 0 Then

            If Not IsDBNull(dsVersiones.Tables(0).Rows(0).Item(0)) Then
                strLastestVersion = dsVersiones.Tables(0).Rows(0).Item(0).ToString()
            End If

            If Not IsDBNull(dsVersiones.Tables(0).Rows(0).Item(1)) Then
                strUpdatePath = dsVersiones.Tables(0).Rows(0).Item(1).ToString()
            End If

            If Not IsDBNull(dsVersiones.Tables(0).Rows(0).Item(2)) Then
                strCommand = dsVersiones.Tables(0).Rows(0).Item(2).ToString()
            End If

            dsVersiones.Dispose()

        End If

    End Sub

    ''''<summary>Toma el valor de versión del Cliente por Reflection y el valor de la última
    ''''versión del Cliente de la tabla VERREG, luego las compara:
    ''''TRUE: La versión de Cliente es la última
    ''''FALSE: La versión del Cliente es menor a la última o alguna de las dos versiones es 'Desconocida' [Alejandro]</summary>
    ''''<history>[Alejandro]</history>
    'Public Shared Function IsLastestVersion() As Boolean
    '    Dim lastestVersion As String
    '    Dim userVersion As String
    '    lastestVersion = GetLastestVersion()
    '    userVersion = GetVersionByReflection("Cliente.exe")
    '    userVersion = userVersion.Replace(".", "")
    '    Try
    '        Try
    '            userVersion = userVersion.Substring(0, 4)
    '        Catch ex As ArgumentOutOfRangeException
    '        Catch ex As IndexOutOfRangeException
    '        Catch ex As Exception
    '        End Try
    '    Catch ex As Exception
    '        userVersion = "Desconocida"
    '    End Try
    '    If userVersion = "Desconocida" Then
    '        Return False
    '    ElseIf lastestVersion = "Desconocida" Then
    '        Return False
    '    ElseIf Convert.ToInt32(userVersion) < Convert.ToInt32(lastestVersion) Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function


    '''<summary>
    '''Obtiene el valor 'Version' de la tabla VERREG
    '''</summary>
    '''<history>[Alejandro]
    '''         [Marcelo]    Modified 18/09/2008
    '''</history>
    Public Shared Function GetLastestVersionFromVerreg() As String
        Dim reg As String = Server.Con.ExecuteScalar(CommandType.Text, "select ver from verreg")
        If String.IsNullOrEmpty(reg) Then
            Return "0"
        Else
            Return reg
        End If
    End Function

    '''<summary>Actualiza la tabla ESTREG con los valores de Version y Nombre de Máquina pasados por parámetro.</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub UpdateEstreg(ByVal strVersion As String, ByVal strMachineName As String)
        Server.Con.ExecuteNonQuery(CommandType.Text, "Update Estreg set VER='" & strVersion & "' where M_Name='" & strMachineName & "'")
    End Sub

    '''<summary>Inserta un nuevo registro en la tabla ESTREG con los valores de ID, Nombre de Máquina,
    '''Nombre de Usuario y Versión pasados por parámetro</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub SetEstreg(ByVal strLastId As String, ByVal strMachineName As String, ByVal strUserName As String, ByVal srtVersion As String)

        Dim strinsert As String = String.Empty

        If Server.isOracle Then
            strinsert = "insert into estreg values(" & strLastId & ",'" & strMachineName & "','" & strUserName & "','" & srtVersion & "',sysdate,sysdate)"
        Else
            strinsert = "insert into estreg values(" & strLastId & ",'" & strMachineName & "','" & strUserName & "','" & srtVersion & "',GetDate(),GetDate())"
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    End Sub

    '''<summary>Obtiene todos los registros de la tabla VERREG</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetVerreg() As DataSet
        Dim Ds As New DataSet()
        Ds = Server.Con.ExecuteDataset(CommandType.Text, "select ver,path,scriptfile,id from verreg")
        Return Ds
    End Function

    '''<summary>Obtiene todos los IDs de la tabla ESTREG donde el nombre de máquina pasado
    '''por parámetro.</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetIdFromEstregWhereMName(ByVal strMachineName As String) As DataSet
        Dim ds As New DataSet()
        Dim strselect As String = "select id,ver from estreg where m_name='" & strMachineName & "'"
        ds = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Return ds
    End Function

    '''<summary>Borra todos los registros de la tabla ESTREG donde el Nombre de Máquina
    '''pasado por parámetro.</summary>
    '''<history>[Alejandro]</history>
    Public Shared Sub DeleteFromEstregWhereMName(ByVal strMachineName As String)
        Server.Con.ExecuteNonQuery(CommandType.Text, "delete estreg where m_name='" & strMachineName & "'")
    End Sub

    '''<summary>Obtiene el ID máximo de la Tabla ESTREG</summary>
    '''<history>[Alejandro]
    '''         [Marcelo]    Modified 18/09/2008
    '''</history>
    Public Shared Function GetMaxIdFromEstreg() As Int64
        Dim reg As Object = Server.Con.ExecuteScalar(CommandType.Text, "SELECT MAX(ID) FROM estreg")

        If IsDBNull(reg) Then
            Return 0
        Else
            Return Convert.ToInt64(reg.ToString())
        End If
    End Function
End Class