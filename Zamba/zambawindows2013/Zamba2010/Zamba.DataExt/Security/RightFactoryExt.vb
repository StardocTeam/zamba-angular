Imports Zamba.Core

Public Class RightFactoryExt
    Inherits RightFactory

    ''' <summary>
    ''' Devuelve si el usuario posee el check habilitado de los Permisos de Asociados.
    ''' Obtiene el usuario a traves de su objecto IUser o a traves de el ID y el ID del docType
    ''' en donde va a buscar.
    ''' </summary>
    ''' <param name="User"></param>
    ''' <param name="docTypeID"></param>
    ''' <param name="userID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSpecificAttributeRight(ByVal User As IUser, ByVal docTypeID As Long) As Boolean
        If Not IsNothing(User) Then
            If GetRight(User.ID, ObjectTypes.DocTypes, RightsType.ViewAssociateRightsByIndex, docTypeID) Then ' redo
                Return True
            End If
            If IsNothing(User.Groups) OrElse User.Groups.Count = 0 Then UserFactory.FillGroups(User)
            For Each g As IUserGroup In User.Groups
                'Si el grupo no tiene el check
                If GetRight(g.ID, ObjectTypes.DocTypes, RightsType.ViewAssociateRightsByIndex, docTypeID) Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function
End Class
