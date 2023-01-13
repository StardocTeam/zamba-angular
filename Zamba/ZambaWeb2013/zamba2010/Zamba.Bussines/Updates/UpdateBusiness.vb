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

    Private Sub GetServerData()
        Try
            Dim ds As DataSet = updatefactory.getserverdata

            Me.ServerVersion = ds.Tables(0).Rows(0).Item(0).ToString()
            Me.ServerPath = ds.Tables(0).Rows(0).Item(1).ToString()
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
        Return updatefactory.UsuariosActualizadosCount(Me.ServerVersion)
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
        Return updatefactory.UsuariosDesactualizadosCount(Me.ServerVersion)
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
        Return updatefactory.Usuarios_Actualizados(Me.ServerVersion)
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
        Return updatefactory.Usuarios_Desactualizados(Me.ServerVersion)
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
        updatefactory.ForzarActualizar(winuser, serverversion)
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
        UpdateFactory.ForzarActualizarPorPC(mName, serverversion)
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
End Class
