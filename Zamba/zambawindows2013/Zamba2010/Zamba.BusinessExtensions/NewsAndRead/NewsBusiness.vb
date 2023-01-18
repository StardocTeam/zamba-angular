Imports Zamba.Data

Public Class NewsBusiness
    ''' <summary>
    ''' Guarda la notificacion
    ''' </summary>
    ''' <param name="NewsID"></param>
    ''' <param name="DocID"></param>
    ''' <param name="DocTypeID"></param>
    ''' <param name="comment"></param>
    ''' <remarks></remarks>
    Public Sub SaveNews(ByVal NewsID As Int64, ByVal DocID As Int64, ByVal DocTypeID As Int64, ByVal comment As String)
        Dim _NewsFactory As New Newsfactory()
        _NewsFactory.SaveNews(NewsID, DocID, DocTypeID, comment)
        _NewsFactory = Nothing
    End Sub

    ''' <summary>
    ''' Marca la novedad como leida
    ''' </summary>
    ''' <param name="currentUser">Usuario</param>
    ''' <param name="DocTypeID">ID de la entidad</param>
    ''' <param name="resultID">Doc ID</param>
    ''' <remarks></remarks>
    Public Sub SetRead(ByVal currentUser As IUser, ByVal DocTypeID As Int64, ByVal docID As Int64)
        Dim users As String = currentUser.ID & ","
        Dim _newsFactory As New Newsfactory()

        For Each group As UserGroup In currentUser.Groups
            users &= group.ID & ","
        Next

        users = users.Remove(users.Length - 1)
        _newsFactory.SetNewsRead(users, DocTypeID, docID)

        If RightsBusiness.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.FlagAsRead, DocTypeID) = True Then
            _newsFactory.SetDocRead(currentUser.ID, DocTypeID, docID)
        End If

        _newsFactory = Nothing
    End Sub
End Class
