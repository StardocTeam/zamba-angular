Imports Zamba.Servers

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
        Dim sql As String = "Update ESTREG set VER=" & serverversion & " Where W_User='" & winuser & "'"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
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
        Dim sql As String = "Update ESTREG set VER=" & serverversion & " Where M_name='" & mName & "'"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub
End Class
