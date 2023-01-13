Imports System.Collections.Generic
Imports System.Text
Imports Zamba.Membership
Imports Zamba.Core
Imports Zamba.Servers
Imports Zamba.Tools

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

        ''Si llega un user id, obtengo el usuario.
        'If User Is Nothing AndAlso userID > 0 Then
        '    User = UserFactory.GetUserById(userID)
        'End If

        If Not IsNothing(User) Then
            If Not GetRight(User.ID, ObjectTypes.DocTypes, RightsType.ViewAssociateRightsByIndex, docTypeID) AndAlso _
                        GetRight(User.ID, ObjectTypes.DocTypes, RightsType.View, docTypeID) Then ' redo
                Return False
            Else
                If IsNothing(User.Groups) OrElse User.Groups.Count = 0 Then UserFactory.FillGroups(User)
                For Each g As IUserGroup In User.Groups
                    'Si el grupo no tiene el check y si tiene permisos de ver el documento
                    If Not GetRight(g.ID, ObjectTypes.DocTypes, RightsType.ViewAssociateRightsByIndex, docTypeID) AndAlso _
                        GetRight(g.ID, ObjectTypes.DocTypes, RightsType.View, docTypeID) Then
                        Return False
                    End If
                Next
                Return True
            End If
        Else
            Return True
        End If
    End Function
End Class
