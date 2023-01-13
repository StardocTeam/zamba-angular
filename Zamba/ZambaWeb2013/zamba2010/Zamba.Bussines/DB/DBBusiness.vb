Imports Zamba.Data
Imports Zamba.Servers

''' <summary>
''' Esta clase se encarga de ejecutar consultas respecto a la base de datos
''' </summary>
''' <history>Marcelo 01/10/08 Created</history>
''' <remarks></remarks>
Public Class DBBusiness
    ''' <summary>
    ''' Return the columns names
    ''' </summary>
    ''' <param name="name">Nombre de la tabla</param>
    ''' <returns></returns>
    ''' <history>Marcelo 01/10/08 Created</history>
    ''' <remarks></remarks>
    Public Shared Function GetColumns(ByVal strServer As String, ByVal strDatabase As String, ByVal strUser As String, ByVal tableName As String) As DataSet
        Return DBFactory.GetColumns(strServer, strDatabase, strUser, tableName)
    End Function

    ''' <summary>
    ''' Devuelve una lista con todas las tablas y vistas de la base de datos
    ''' </summary>
    ''' <returns></returns>
    ''' <history>Marcelo 01/10/08 Created</history>
    ''' <history>Marcelo 18/12/08 Modified</history>
    ''' <remarks></remarks>
    Public Shared Function GetTablesAndViews(ByVal strServer As String, ByVal strDatabase As String, ByVal strUser As String) As DataSet
        Return DBFactory.GetTablesAndViews(strServer, strDatabase, strUser)
    End Function

    Public Sub InitializeDB()

        Dim UP As New UserPreferences

        Zamba.Servers.Server.ConInitialized = True
        Dim server As New Server(Zamba.Servers.Server.currentfile)
        server.InitializeConnection(UP.getValueForMachine("DateConfig", UPSections.UserPreferences, "CONVERT(DATETIME, '{0}-{1}-{2} 00:00:00', 102)"), UP.getValueForMachine("DateTimeConfig", UPSections.UserPreferences, "CONVERT(DATETIME, '{0}-{1}-{2} {3}:{4}:{5}', 102)"))
        server.MakeConnection()

        server = Nothing
        UP = Nothing
    End Sub

End Class