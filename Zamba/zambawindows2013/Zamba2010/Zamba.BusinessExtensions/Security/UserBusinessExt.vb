Imports Zamba.Data

Partial Public Class UserBusinessExt
    Inherits UserBusiness


    ''' <summary>
    ''' Método que verifica si el usuario sigue o no en la tabla UCM
    ''' </summary>
    ''' <param name="User"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Martin]	18/06/2012	Created
    ''' [German] 16/07/2012 Se corrige funcionalidad multisession.Ahora un usuario puede
    '''                     ingresar al cliente si dejo una instancia anterior abierta.
    ''' </history>
    Public Function ValidatePCName(ByVal User As IUser) As Boolean
        If UserPreferences.getValue("MultiSession", UPSections.UserPreferences, True) = False Then
            Dim _Ucmfactoryext As New UCMFactoryExt()
            Try
                Return _Ucmfactoryext.verifyIfPCNameInUCMMoreThanOnce(User.ID)
            Finally
                _Ucmfactoryext = Nothing
            End Try
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Obtiene todos los usuarios de uno o mas grupos
    ''' </summary>
    ''' <returns>ArrayList de objetos IUser. Solo se completa el ID y NAME del usuario.</returns>
    ''' <remarks>Para obtener todos los usuarios completos crear otro método</remarks>
    Public Function GetUserIdAndNameByGroupIds(ByVal groupIds As ArrayList) As ArrayList
        Dim userFactory As New UserFactoryExt
        Return userFactory.GetUserIdAndNameByGroupIds(groupIds)
    End Function

    Public Function GetUserAndGroupsMailsByUserOrGroupId(ByVal Ids As List(Of Int64)) As String
        Dim userFactory As New UserFactoryExt
        Return userFactory.GetUserAndGroupsMailsByUserOrGroupId(Ids)
    End Function

    Public Function GetUserAndGroupsMailsByUserOrGroupId(ByVal Id As Int64) As String
        Dim userFactory As New UserFactoryExt
        Dim Ids As New List(Of Int64)
        Ids.Add(Id)
        Return userFactory.GetUserAndGroupsMailsByUserOrGroupId(Ids)
    End Function

End Class
