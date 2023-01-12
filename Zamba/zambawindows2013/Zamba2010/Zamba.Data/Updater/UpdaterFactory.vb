Imports System.Text


'''<summary> Clase que agrupa los métodos usados para la actualización de Zamba</summary>
'''<history>[Alejandro]</history>
Public Class UpdaterFactory


    '''<summary>Obtiene, por Reflection, la versión del ensamblado pasado por parámetro</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetVersionByReflection(ByVal dllName As String) As String
        Dim dll As System.Reflection.Assembly = System.Reflection.Assembly.LoadFrom(dllName)
        Dim Version As String = dll.GetName().Version.ToString()
        Return Version
    End Function

    '''<history>[Santiago] 2012-06-12   Agregado Update de Last_Check
    '''</history>
    Public Shared Function GetVersionByEstreg() As String
        Dim Version As Object

        Version = Server.Con.ExecuteScalar(CommandType.Text, "select ver from estreg where m_name='" & Environment.MachineName & "'")

        ' [Santiago 2012-06-12]
        'UpdateLastVerified(Environment.MachineName)
        If Version Is Nothing Then
            Return String.Empty
        End If
        Return Version.ToString
    End Function

    Public Shared Sub UpdateLastVerified(strMachine As String, version As String)
        Dim dateFunction As String
        If Server.isOracle Then
            dateFunction = "sysdate"
        Else
            dateFunction = "getDate()"
        End If
        If Membership.MembershipHelper.CurrentUser Is Nothing Then
            Server.Con.ExecuteNonQuery(CommandType.Text, "update estreg set last_check=" & dateFunction & " , VER='" & version & "' where M_Name='" & strMachine & "'")
        Else
            Server.Con.ExecuteNonQuery(CommandType.Text, "update estreg set last_check=" & dateFunction & " , W_USER = '" & Membership.MembershipHelper.CurrentUser.Name & " , VER='" & version & "' where M_Name='" & strMachine & "'")
        End If

    End Sub

    '''<summary>Obtiene todas las características de la última versión (Version, Path donde se encuentra y Script File)</summary>
    '''<history>[Alejandro]
    '''         [Santiago]  2012-06-12  Agregado Update de Last_Check
    '''</history>
    Public Shared Sub GetLastestVersion(ByRef strLastestVersion As String, ByRef strUpdatePath As String, ByRef strCommand As String)

        Dim dsVersiones As New DataSet

        dsVersiones = Server.Con.ExecuteDataset(CommandType.Text, "select ver,path,scriptfile,id from verreg")

        ' [Santiago 2012-06-12]
        'UpdateLastVerified(Environment.MachineName)

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

    '''<summary>
    '''Obtiene el valor 'Version' de la tabla VERREG
    '''</summary>
    '''<history>[Alejandro]
    '''         [Marcelo]    Modified 18/09/2008
    '''         [Santiago]  2012-06-12  Agregado Update de Last_Check
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
    '''<history>[Alejandro]
    '''         [Santiago]  2012-06-12  Agregado 'Updated' a parámetros de Update
    '''</history>
    Public Shared Sub UpdateEstreg(ByVal strVersion As String, ByVal strMachineName As String, ByVal WinUser As String)
        Dim dateFunction As String
        If Server.isOracle Then
            dateFunction = "sysdate"
        Else
            dateFunction = "getDate()"
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, "Update Estreg set VER='" & strVersion & "',updated=" & dateFunction & " W_USER = '" & WinUser & "' where M_Name='" & strMachineName & "'")
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
        Return Server.Con.ExecuteDataset(CommandType.Text, "select ver,path,scriptfile,id from verreg")
    End Function

    '''<summary>Obtiene todos los IDs de la tabla ESTREG donde el nombre de máquina pasado
    '''por parámetro.</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function GetIdFromEstregWhereMName(ByVal strMachineName As String) As DataSet
        Return Server.Con.ExecuteDataset(CommandType.Text, "select id,ver from estreg where m_name='" & strMachineName & "'")
    End Function

    Public Shared Function GetMachineZambaVersion(ByVal machineName As String) As String
        If Server.isOracle = True Then
            Return Server.Con.ExecuteScalar(CommandType.Text, "select ver from estreg where m_name='" & machineName & "' and rownum = 1")
        Else
            Return Server.Con.ExecuteScalar(CommandType.Text, "select top 1 ver from estreg where m_name='" & machineName & "'")
        End If
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

    '''<summary>Obtiene el ID máximo de la Tabla ESTREG</summary>
    '''<history>[Alejandro]
    '''         [Marcelo]    Created 18/05/2012
    '''</history>
    Public Shared Function GetScriptsHistory() As DataSet
        If Server.isOracle Then
            Return Server.Con.ExecuteDataset(CommandType.Text, "select ID, c_name as Consulta,c_user as Usuario ,Resultado, Fecha from ZScripts")
        Else
            Return Server.Con.ExecuteDataset(CommandType.Text, "select ID, name as Consulta,[user] as Usuario ,Resultado, Fecha from ZScripts")
        End If

    End Function

    '''<summary>Obtiene si el id fue ejecutado</summary>
    '''<history>
    '''         [Marcelo]    Created 18/05/2012
    '''</history>
    Public Shared Function GetScriptIDExecuted(ByVal id As Int64) As Int64
        Return Server.Con.ExecuteScalar(CommandType.Text, "select count(1) from ZScripts where id =" & id & " and resultado='Correcto'")
    End Function

    ''' <summary>
    ''' Guarda en el historial la ejecucion del script
    ''' </summary>
    ''' <param name="id">ID del script</param>
    ''' <param name="name">Nombre del script</param>
    ''' <param name="username">Nombre del usuario que lo ejecuto</param>
    ''' <param name="Resultado">Resultado de la ejecucion</param>
    ''' <remarks></remarks>
    Public Shared Sub SaveScriptHistory(ByVal id As Int64, ByVal name As String, ByVal username As String, Resultado As String)

        Dim query As New StringBuilder

        If Server.isOracle Then
            query.Append("  BEGIN EXECUTE IMMEDIATE 'insert into ZSCripts(id,c_name,c_user,resultado,fecha) values (")
            query.Append(id)
            query.Append(",''")
            query.Append(name)
            query.Append("'',''")
            query.Append(username)
            query.Append("'',''")
            query.Append(Resultado)
            query.Append("'', sysdate)'; ")
            query.Append("EXCEPTION WHEN OTHERS THEN IF SQLCODE != -1 THEN RAISE; END IF; END;")
        Else
            query.Append("insert into ZSCripts(id,name,[user],resultado,fecha) values (" & id & ",'" & name & "','" & username & "','" & Resultado & "',getdate())")
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())
    End Sub
End Class