Imports System.Collections.Generic

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
Public Class UpdateFactory

    Public Shared Function GetServerData() As DataSet
        Dim sql As String = "Select VER, path from verreg"
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Return ds
    End Function

    Public Shared Function GetUsersVersionsTable() As DataTable
        Dim query As String
        Dim data As DataSet

        If Server.isOracle Then

            query = "SELECT NVL(ZAU.C_Value, (SELECT C_VALUE FROM zuserconfig WHERE C_NAME LIKE 'AutoUpdate' AND C_USERID = 0)) AS ""ACTUALIZACION AUTOMATICA"", EstregAndLVAndLVP.* FROM (SELECT estregAndLastVersion.*, NVL(ZLVP.C_Value, (SELECT C_VALUE FROM zuserconfig	WHERE C_NAME LIKE 'LastVersionPath'	AND C_USERID = 0)) AS ""UBICACION"" FROM (SELECT estregAndUserId.*, NVL(ZLV.C_Value, (SELECT C_VALUE FROM zuserconfig WHERE C_NAME LIKE 'LastVersion' AND C_USERID = 0 )) AS ""VERSION A ACTUALIZAR"" FROM ( SELECT usrt.ID AS ""ID"", usrt.NAME AS USUARIO, NVL(e.M_NAME, '') AS MAQUINA, NVL(e.VER, '0') AS ""VERSION ACTUAL"", NVL(e.UPDATED, '') AS ""FECHA INSTALACION"" FROM ESTREG e RIGHT JOIN USRTABLE usrt ON e.W_USER = usrt.NAME) estregAndUserId LEFT JOIN ZUSERCONFIG ZLV ON estregAndUserId.""ID"" = ZLV.C_USERID AND ZLV.C_NAME = 'LastVersion') estregAndLastVersion LEFT JOIN ZUSERCONFIG ZLVP ON estregAndLastVersion.""ID"" = ZLVP.C_USERID AND ZLVP.C_NAME = 'LastVersionPath') EstregAndLVAndLVP LEFT JOIN ZUSERCONFIG ZAU ON EstregAndLVAndLVP.""ID"" = ZAU.C_USERID AND ZAU.C_NAME = 'AutoUpdate'"

        Else

            query = "SELECT ISNULL(ZAU.Value, (SELECT Value FROM zuserconfig WHERE NAME LIKE 'AutoUpdate' AND USERID = 0)) AS ""ACTUALIZACION AUTOMATICA"", EstregAndLVAndLVP.* FROM (SELECT estregAndLastVersion.*, ISNULL(ZLVP.Value, (SELECT Value FROM zuserconfig WHERE NAME LIKE 'LastVersionPath' AND USERID = 0)) AS ""UBICACION"" FROM (SELECT estregAndUserId.*, ISNULL(ZLV.Value, (SELECT Value FROM zuserconfig WHERE NAME LIKE 'LastVersion' AND USERID = 0)) AS ""VERSION A ACTUALIZAR"" FROM (SELECT usrt.ID AS ""ID"", usrt.NAME AS USUARIO, ISNULL(e.M_NAME, '') AS MAQUINA, ISNULL(e.VER, '0') AS ""VERSION ACTUAL"", ISNULL(e.UPDATED, '') AS ""FECHA INSTALACION"" FROM ESTREG e RIGHT JOIN USRTABLE usrt ON e.W_USER = usrt.NAME) estregAndUserId LEFT JOIN ZUSERCONFIG ZLV ON estregAndUserId.""ID"" = ZLV.USERID AND ZLV.NAME = 'LastVersion') estregAndLastVersion LEFT JOIN ZUSERCONFIG ZLVP ON estregAndLastVersion.""ID"" = ZLVP.USERID AND ZLVP.NAME = 'LastVersionPath') EstregAndLVAndLVP LEFT JOIN ZUSERCONFIG ZAU ON EstregAndLVAndLVP.""ID"" = ZAU.USERID AND ZAU.NAME = 'AutoUpdate'"

        End If

        data = Server.Con.ExecuteDataset(CommandType.Text, query)

        Return If(data IsNot Nothing, data.Tables(0), Nothing)
    End Function

    Public Shared Function GetVersionsTable() As DataTable
        Dim query As String = String.Empty
        Dim data As DataSet
        query = "Select ID, VER AS ""VERSION NUMERO"", PATH AS UBICACION, UPDATED AS ""FECHA DE CREACION"" from verreg ORDER BY ""VERSION NUMERO"" DESC"
        data = Server.Con.ExecuteDataset(CommandType.Text, query)
        Return If(data IsNot Nothing, data.Tables(0), Nothing)
    End Function

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
    Public Shared Function UsuariosActualizadosCount(ByVal ServerVersion As String) As Int32
        Dim sql As String
        sql = "Select count(1) from ESTREG where VER='" & ServerVersion & "'"
        Return Int32.Parse(Server.Con.ExecuteScalar(CommandType.Text, sql).ToString())
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
    Public Shared Function UsuariosDesactualizadosCount(ByVal ServerVersion As String) As Int32
        Dim sql As String = "Select count(1) from Zvw_ESTREG_100 where VERSION<'" & ServerVersion & "'"
        'sql = "Select count(1) from ESTREGVIEW where VERSION<'" & Me.ServerVersion & "'"
        Return System.Convert.ToInt32(Server.Con.ExecuteScalar(CommandType.Text, sql))
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
    Public Shared Function Usuarios_Actualizados(ByVal ServerVersion As String) As DataTable
        Dim ds As DataSet
        'Dim sql As String = "Select * from EstregView Where VERSION='" & Me.ServerVersion & "'"
        Dim sql As String = "Select * from Zvw_ESTREG_100 Where VERSION='" & ServerVersion & "'"
        ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Return ds.Tables(0)
    End Function

    Public Shared Sub RemoveZambaVersion(versionSelected As String)
        Dim query As String = String.Empty
        query = String.Format("DELETE FROM VERREG WHERE ID = {0}", versionSelected)
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Function GetVersionPath(versionID As String) As String
        Dim query As String = String.Empty
        query = String.Format("SELECT PATH FROM VERREG WHERE ID = {0}", versionID)
        Return Server.Con.ExecuteScalar(CommandType.Text, query)
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
    Public Shared Function Usuarios_Desactualizados(ByVal ServerVersion As String) As DataTable
        Dim ds As DataSet
        'Dim sql As String = "Select * from EstregView Where VERSION < '" & Me.ServerVersion & "'"
        Dim sql As String = "Select * from Zvw_ESTREG_100 Where VERSION < '" & ServerVersion & "'"
        ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Return ds.Tables(0)
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
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub ChangeServerVersion(ByVal newversion As String, ByVal path As String)
        Dim sql As String = "Update Verreg SET ver='" & newversion & "', Path='" & path.Trim & "'"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub

    Public Shared Function GetVersionNumber(IdVersion As Integer) As String
        Dim query As String = String.Empty
        query = String.Format("SELECT VER FROM VERREG WHERE ID = {0}", IdVersion)
        Return Server.Con.ExecuteScalar(CommandType.Text, query)
    End Function

    Public Shared Function GetUsersIdByVersionNumber(versionNumber As String) As DataTable
        Dim query As String = String.Empty
        If Server.isOracle Then
            query = String.Format("select c_UserId from ZUserConfig where c_name = 'LastVersion' and c_Value = '{0}'", versionNumber)
        Else
            query = String.Format("select UserId from ZUserConfig where name = 'LastVersion' and Value = '{0}'", versionNumber)
        End If
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

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
        Dim sql As String = "Update ESTREG set VER='" & serverversion & "' Where W_User='" & winuser & "'"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub

    Public Shared Function GetUsersVersionNumberByIds(stUsersSelected As String) As DataTable
        Dim query As String = String.Empty
        If Server.isOracle Then
            query = String.Format("select C_USERID, C_VALUE from ZUSERCONFIG where C_USERID in ({0}) and c_name = 'LastVersion' and c_section = 1;", stUsersSelected)
        Else
            query = String.Format("select USERID, VALUE from ZUSERCONFIG where USERID in ({0}) and name = 'LastVersion' and section = 1;", stUsersSelected)
        End If
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    Public Shared Function GetUsersVersionPathByIds(stUsersSelected As String) As DataTable
        Dim query As String = String.Empty
        If Server.isOracle Then
            query = String.Format("select C_USERID, C_VALUE from ZUSERCONFIG where C_USERID in ({0}) and c_name = 'LastVersionPath' and c_section = 1", stUsersSelected)
        Else
            query = String.Format("select USERID, VALUE from ZUSERCONFIG where USERID in ({0}) and name = 'LastVersionPath' and section = 1", stUsersSelected)
        End If
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    Public Shared Sub UpdateUsersVersionNumber(NewVersionNumber As String, usersToUpdateNumber As List(Of String))
        Dim query As String = String.Empty
        If Server.isOracle Then
            query = String.Format("UPDATE ZUSERCONFIG SET C_VALUE = {0},  WHERE  C_USERID in ({1}) AND C_NAME LIKE 'LastVersion'", NewVersionNumber, usersToUpdateNumber)
        Else
            query = String.Format("UPDATE ZUSERCONFIG SET VALUE = {0},  WHERE  C_USERID in ({1} AND NAME LIKE 'LastVersion')", NewVersionNumber, usersToUpdateNumber)
        End If
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Function GetLastVersionNumberWithoutDots() As Integer
        Dim query As String = String.Empty
        query = "SELECT max(replace(ver,'.','')) as ultimaVersion FROM VERREG"
        Return Server.Con.ExecuteScalar(CommandType.Text, query)
    End Function

    Public Shared Sub UpdateUsersVersionPath(NewPath As String, usersToUpdatePath As List(Of String))
        Dim query As String = String.Empty
        If Server.isOracle Then
            query = String.Format("UPDATE ZUSERCONFIG SET C_VALUE = {0},  WHERE  C_USERID in ({1}) AND C_NAME LIKE 'LastVersionPath'", NewPath, usersToUpdatePath)
        Else
            query = String.Format("UPDATE ZUSERCONFIG SET VALUE = {0},  WHERE  C_USERID in ({1}) AND NAME LIKE 'LastVersionPath'", NewPath, usersToUpdatePath)
        End If
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Sub InsertUsersVersionsPath(usersToInsertPath As List(Of String))
    End Sub

    Public Shared Function GetUsersIDWithAutoUpdate() As DataTable

        Dim query As String

        If Server.isOracle Then
            query = "select usr.id from usrtable usr left join zuserconfig zusc on usr.ID = zusc.C_USERID and zusc.C_NAME = 'AutoUpdate' where zusc.C_VALUE = 'True'"
        Else
            query = "select usr.id from usrtable usr left join zuserconfig zusc on usr.ID = zusc.USERID and zusc.NAME = 'AutoUpdate' where zusc.VALUE = 'True'"
        End If

        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    Public Shared Sub InsertUsersVersionsNumber(usersToInsertNumber As List(Of String))
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
    Public Shared Sub ForzarActualizarPorPC(ByVal mName As String, ByVal serverversion As String)
        Dim sql As String = "Update ESTREG set VER='" & serverversion & "' Where M_name='" & mName & "'"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub

    Public Shared Sub ForzarActualizarPorPC(ByVal mName As String, ByVal serverversion As String, ByVal user As String)
        Dim sql As String = "Update ESTREG set VER='" & serverversion & "', W_User='" & user & "' Where M_name='" & mName & "'"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub

    Public Shared Sub AddNewZambaVersion(ID As Integer, versionNumber As String, fileName As String)
        Dim query As String = String.Empty
        query = String.Format("INSERT INTO VERREG (ID, VER, PATH, UPDATED, SCRIPTFILE) VALUES({0}, '{1}', '{2}', {3}, '')",
                              ID,
                              versionNumber,
                              fileName,
                              If(Server.isOracle, "SYSDATE", "GETDATE()"))
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Function GetLastVersionId() As Integer
        Dim query As String = String.Empty
        query = "SELECT ID FROM VERREG WHERE ID = (SELECT MAX(ID) FROM VERREG)"
        Return Server.Con.ExecuteScalar(CommandType.Text, query)
    End Function

    Public Shared Function GetEarlierVersionId(Id As Integer) As Integer
        Dim query As String = String.Empty
        If Server.isOracle Then
            query = String.Format("SELECT * FROM (SELECT ID FROM VERREG WHERE ID < {0} ORDER BY ID DESC) WHERE ROWNUM = 1", Id)
        Else
            query = String.Format("SELECT TOP 1 ID FROM VERREG WHERE ID < {0} ORDER BY ID DESC", Id)
        End If
        Return Server.Con.ExecuteScalar(CommandType.Text, query)
    End Function

End Class
