Imports Zamba.Data
Imports System.Collections.Generic
Imports Zamba.Servers.Server
Imports Zamba.Core

Public Class ServersFactory

    Public Shared Function GetServerTypes() As List(Of String)
        Dim ServerTypes As New List(Of String)

        'ServerTypes.AddRange([Enum].GetValues(GetType(Server.DBTYPES)))
        For Each item As Object In DirectCast([Enum].GetValues(DirectCast(Server.ServerType.GetType, Type)), Array)
            ServerTypes.Add(item.ToString.Trim)
        Next
        Return ServerTypes
    End Function

    Public Shared Function ConvertDate(ByVal pDate As String) As String
        Return Server.Con.ConvertDate(pDate)
    End Function

    Public Shared Function GetServerType() As Int32
        Return Convert.ToInt32(Servers.Server.ServerType)
    End Function

    ''' <summary>
    ''' Devuelve una conexion a la base de datos
    ''' </summary>
    ''' <param name="servertype"></param>
    ''' <param name="dbname"></param>
    ''' <param name="dbpassword"></param>
    ''' <param name="dbuser"></param>
    ''' <param name="servidor"></param>
    ''' <param name="_commandType"></param>
    ''' <param name="commandText"></param>
    ''' <history>Marcelo Created 09/12/08</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetNewConnection(ByVal servertype As DBTypes, ByVal dbname As String, ByVal dbpassword As String, ByVal dbuser As String, ByVal servidor As String) As IConnection
        Return Server.Con(servertype, servidor, dbname, dbuser, dbpassword)
    End Function

    Public Shared Function BuildExecuteScalar(ByVal _commandType As CommandType, ByVal commandText As String) As Object
        Return Server.Con.ExecuteScalar(_commandType, commandText)
    End Function

    Public Shared Function BuildExecuteScalar(ByVal servertype As DBTypes, ByVal dbname As String, ByVal dbpassword As String, ByVal dbuser As String, ByVal servidor As String, ByVal _commandType As CommandType, ByVal commandText As String) As Object
        Return Server.Con(servertype, servidor, dbname, dbuser, dbpassword).ExecuteScalar(_commandType, commandText)
    End Function

    Public Shared Function BuildExecuteDataSet(ByVal _commandType As CommandType, ByVal commandText As String) As DataSet
        Return Server.Con.ExecuteDataset(_commandType, commandText)
    End Function

    Public Shared Function BuildExecuteDataSet(ByVal servertype As DBTypes, ByVal dbname As String, ByVal dbpassword As String, ByVal dbuser As String, ByVal servidor As String, ByVal _commandType As CommandType, ByVal commandText As String) As DataSet
        Return Server.Con(servertype, servidor, dbname, dbuser, dbpassword).ExecuteDataset(_commandType, commandText)
    End Function

    Public Shared Sub BuildExecuteNonQuery(ByVal _commandType As CommandType, ByVal commandText As String)
        Server.Con.ExecuteNonQuery(_commandType, commandText)
    End Sub

    Public Shared Sub BuildExecuteNonQuery(ByVal servertype As DBTypes, ByVal dbname As String, ByVal dbpassword As String, ByVal dbuser As String, ByVal servidor As String, ByVal _commandType As CommandType, ByVal commandText As String)
        Server.Con(servertype, servidor, dbname, dbuser, dbpassword).ExecuteNonQuery(_commandType, commandText)
    End Sub

    Public Shared Function IsConnectionValid(ByVal intServerType As Int32, ByVal serverName As String, ByVal dataBase As String, ByVal conUser As String, ByVal conPass As String) As Int32

        Dim con As IConnection

        con = Server.Con(DirectCast(intServerType, DBTypes), serverName, dataBase, conUser, conPass)

        Dim d As Integer = 0
        If Server.ServerType = DBTypes.MSSQLServer Or Server.ServerType = DBTypes.MSSQLServer7Up Then
            d = Zamba.Servers.Server.Con.ExecuteScalar(CommandType.Text, "select count(1) from sysobjects")
        Else
            d = Zamba.Servers.Server.Con.ExecuteScalar(CommandType.Text, "select count(1) from user_objects")
        End If

        Return d

    End Function

    Public Shared Event ConnectionTerminated()
    Public Shared Event SessionTimeOut()

    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Shared WithEvents server As Zamba.Servers.Server

    Private Shared Sub Event_ConnectionTerminated() Handles server.ConnectionTerminated
        RaiseEvent ConnectionTerminated()
    End Sub
    Private Shared Sub Event_SessionTimeOut() Handles server.SessionTimeOut
        RaiseEvent SessionTimeOut()
    End Sub

End Class
