Imports Zamba.Servers

Public Class DbToolsFactoryExt
    Dim dbowner As String
    ''' <summary>
    ''' Gets the database schema
    ''' </summary>
    ''' <returns>Database schema</returns>
    ''' <remarks>Calls Server.dbOwner property</remarks>
    Public Function GetDataBaseSchema()
        If String.IsNullOrEmpty(dbowner) Then
            If String.IsNullOrEmpty(ZOptFactory.GetValue("DBOwner")) Then
                If Server.dbOwner Is Nothing Then
                    Dim auxInitialization As Server.DBTYPES = Server.ServerType
                End If
                dbowner = Server.dbOwner
            Else
                dbowner = ZOptFactory.GetValue("DBOwner")
            End If
        End If
        Return dbowner
    End Function
End Class
