Imports Zamba.Data
Imports Zamba.Core

Public Class UserComponent

#Region "Funciones Publicas"


#Region "Encriptación"
    Private Shared key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Private Shared iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
#End Region


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Borra un Usuario
    ''' </summary>
    ''' <param name="usr"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub Delete(ByVal usr As iuser)
        UserComponentFactory.Delete(usr)
    End Sub
    
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene el nombre de usuario en base al ID
    ''' </summary>
    ''' <param name="UserId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetUserNamebyId(ByVal UserId As Int64) As String
        Return UserComponentFactory.GetUserNamebyId(UserId)
    End Function
#End Region



    Public Shared Sub ClearHashTables()
        SyncLock (Cache.UsersAndGroups.hsUserTable)
            Cache.UsersAndGroups.hsUserTable = New SynchronizedHashtable()
        End SyncLock
    End Sub

End Class

