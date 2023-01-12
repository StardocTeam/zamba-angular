
Imports System
Imports Zamba.Core
Imports Zamba.Data
Imports System.Collections
Imports System.Collections.Generic



Public Class NotifyBusiness

    Public Shared Function GetMails(ByVal typeGroupToNotify As GroupToNotifyTypes, ByVal idGroupToNotify As Int64, ByRef mailsToNotify As List(Of String)) As Boolean

        Dim UsersIds As Generic.List(Of Int64)
        Dim flagAllUsersWithMail As Boolean = True
        Dim reciepusers As New ArrayList
        Dim externalMails As ArrayList

        If IsNothing(mailsToNotify) Then
            mailsToNotify = New Generic.List(Of String)
        End If

        Try
            'Obtiene todos los usuarios seleccionados y de grupos
            UsersIds = GetAllUserIDsToNotify(typeGroupToNotify, idGroupToNotify)
            For Each userid As Int64 In UsersIds
                reciepusers.Add(userid)
            Next
            ' obtiene los mails externos (escritos a mano)
            externalMails = GetGroupExternalMails(typeGroupToNotify, idGroupToNotify)

            If reciepusers.Count > 0 Or externalMails.Count > 0 Then
                ' Obtiene los mails de los Usuarios
                mailsToNotify = GetGroupToNotifyMails(reciepusers)
                Dim sNothing As String = Nothing
                Dim auxLst As New Generic.List(Of String)

                For Each s As String In mailsToNotify
                    auxLst.Add(s)
                Next
                'quita los registros que vienen en Nothing
                For Each s As String In auxLst
                    If String.IsNullOrEmpty(s) Then
                        mailsToNotify.Remove(s)
                    End If
                Next

                If auxLst.Count <> mailsToNotify.Count Then
                    flagAllUsersWithMail = False
                End If

                For Each mail As String In externalMails
                    mailsToNotify.Add(mail)
                Next
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            flagAllUsersWithMail = False
        End Try

        Return flagAllUsersWithMail

    End Function
    Public Shared Function GetMails(ByVal typeGroupToNotify As GroupToNotifyTypes, ByVal docIds As Generic.List(Of Int64), ByRef mailsToNotify As List(Of String)) As Boolean

        Dim UsersIds As Generic.List(Of Int64)
        Dim flagAllUsersWithMail As Boolean = True
        Dim reciepusers As New ArrayList
        Dim externalMails As ArrayList

        If IsNothing(mailsToNotify) Then
            mailsToNotify = New Generic.List(Of String)
        End If

        Try
            'Obtiene todos los usuarios seleccionados y de grupos
            UsersIds = GetAllUserIDsToNotify(typeGroupToNotify, docIds)
            For Each userid As Int64 In UsersIds
                reciepusers.Add(userid)
            Next
            ' obtiene los mails externos (escritos a mano)
            externalMails = GetGroupExternalMails(typeGroupToNotify, docIds)

            If reciepusers.Count > 0 Or externalMails.Count > 0 Then
                ' Obtiene los mails de los Usuarios
                mailsToNotify = GetGroupToNotifyMails(reciepusers)
                Dim sNothing As String = Nothing
                Dim auxLst As New Generic.List(Of String)

                For Each s As String In mailsToNotify
                    auxLst.Add(s)
                Next
                'quita los registros que vienen en Nothing
                For Each s As String In auxLst
                    If String.IsNullOrEmpty(s) Then
                        mailsToNotify.Remove(s)
                    End If
                Next

                If auxLst.Count <> mailsToNotify.Count Then
                    flagAllUsersWithMail = False
                End If

                For Each mail As String In externalMails
                    mailsToNotify.Add(mail)
                Next
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            flagAllUsersWithMail = False
        End Try

        Return flagAllUsersWithMail

    End Function
    Public Shared Function GetGroupExternalMails(ByVal typeid As GroupToNotifyTypes, ByVal Ids As Generic.List(Of Int64)) As ArrayList
        Dim users As ArrayList
        users = NotifyFactory.GetGroupExternalMails(typeid, Ids)
        Return users
    End Function

    'Public Shared Sub NotifyGroup(ByVal typeGroupToNotify As GroupToNotifyTypes, ByVal idGroupToNotify As Int64 _
    ', ByVal SendAsHtml As Boolean, Optional ByVal asuntoMail As String = "", Optional ByVal bodyMail As String = "" _
    ', Optional ByVal attachPath As String = "", Optional ByVal ccMail As String = "", Optional ByVal ccoMail As String = "")

    '    Dim sendmails As Boolean = True


    '' se elimino algun registro por no tener un mail
    '    If MessageBox.Show("Hay usuarios sin cuentas de mail definidas, ¿Desea continuar el envio?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
    '        For Each mail As String In externalMails
    '            mails.Add(mail)
    '        Next
    '        If mails.Count > 0 Then
    '            If Not String.IsNullOrEmpty(attachPath) Then
    '                If Message_Factory.SendMail(mails, ccMail, ccoMail, asuntoMail, bodyMail, SendAsHtml, attachPath) Then
    '                    MessageBox.Show("Mensaje enviado con exito", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Else
    '                    MessageBox.Show("El mensaje no se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                End If
    '            Else
    '                If Message_Factory.SendMail(mails, ccMail, ccoMail, asuntoMail, bodyMail, SendAsHtml) Then
    '                    MessageBox.Show("Mensaje enviado con exito", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Else
    '                    MessageBox.Show("El mensaje no se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                End If
    '            End If
    '        Else
    '            MessageBox.Show("No hay usuarios para notificar", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End If
    '    End If
    '            Else ' todos los registros de la lista son validos
    '    For Each mail As String In externalMails
    '        mails.Add(mail)
    '    Next
    '    If Not String.IsNullOrEmpty(attachPath) Then
    '        If Message_Factory.SendMail(mails, ccMail, ccoMail, asuntoMail, bodyMail, SendAsHtml, attachPath) Then
    '            MessageBox.Show("Mensaje enviado con exito", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Else
    '            MessageBox.Show("El mensaje no se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        End If
    '    Else
    '        If Message_Factory.SendMail(mails, ccMail, ccoMail, asuntoMail, bodyMail, SendAsHtml) Then
    '            MessageBox.Show("Mensaje enviado con exito", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Else
    '            MessageBox.Show("El mensaje no se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        End If
    '    End If
    '            End If
    '        Else ' no hay seleccionados usuario receptores del mail
    '    MessageBox.Show("Seleccione usuarios a notificar", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        End If
    '        Else ' el usuario no tiene la cuenta de mail configurada
    '    MessageBox.Show("Debe configurar la cuenta de correo de su usuario", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        End If
    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try

    'End Sub

    ' Public Shared Sub NotifyGroup(ByVal typeGroupToNotify As GroupToNotifyTypes, ByVal idGroupToNotify As Int64 _
    ', ByVal SendAsHtml As Boolean, Optional ByVal asuntoMail As String = "", Optional ByVal bodyMail As String = "" _
    ', Optional ByVal attachPath As String = "", Optional ByVal ccMail As String = "", Optional ByVal ccoMail As String = "")

    '     Dim reciepusers As New ArrayList
    '     Dim UsersIds As Generic.List(Of Int64)
    '     Dim mails As New Generic.List(Of String)
    '     Dim externalMails As ArrayList
    '     Dim sendmails As Boolean = True

    '     Try
    '         'valida que el usuario emisor del mail tenga configurado su mail
    '         If String.Compare(Membership.MembershipHelper.CurrentUser.eMail.Mail, String.Empty) <> 0 Then
    '             'Obtiene todos los usuarios seleccionados y de grupos
    '             UsersIds = GetAllUserIDsToNotify(typeGroupToNotify, idGroupToNotify)
    '             For Each userid As Int64 In UsersIds
    '                 reciepusers.Add(userid)
    '             Next
    '             ' obtiene los mails externos (escritos a mano)
    '             externalMails = GetGroupExternalMails(typeGroupToNotify, idGroupToNotify)

    '             If reciepusers.Count > 0 Or externalMails.Count > 0 Then
    '                 ' Obtiene los mails de los Usuarios
    '                 mails = GetGroupToNotifyMails(reciepusers)
    '                 Dim sNothing As String = Nothing
    '                 Dim auxLst As New Generic.List(Of String)

    '                 For Each s As String In mails
    '                     auxLst.Add(s)
    '                 Next
    '                 'quita los registros que vienen en Nothing
    '                 For Each s As String In auxLst
    '                     If String.IsNullOrEmpty(s) Then
    '                         mails.Remove(s)
    '                     End If
    '                 Next

    '                 If auxLst.Count <> mails.Count Then
    '                     ' se elimino algun registro por no tener un mail
    '                     If MessageBox.Show("Hay usuarios sin cuentas de mail definidas, ¿Desea continuar el envio?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
    '                         For Each mail As String In externalMails
    '                             mails.Add(mail)
    '                         Next
    '                         If mails.Count > 0 Then
    '                             If Not String.IsNullOrEmpty(attachPath) Then
    '                                 If Message_Factory.SendMail(mails, ccMail, ccoMail, asuntoMail, bodyMail, SendAsHtml, attachPath) Then
    '                                     MessageBox.Show("Mensaje enviado con exito", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                                 Else
    '                                     MessageBox.Show("El mensaje no se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                                 End If
    '                             Else
    '                                 If Message_Factory.SendMail(mails, ccMail, ccoMail, asuntoMail, bodyMail, SendAsHtml) Then
    '                                     MessageBox.Show("Mensaje enviado con exito", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                                 Else
    '                                     MessageBox.Show("El mensaje no se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                                 End If
    '                             End If
    '                         Else
    '                             MessageBox.Show("No hay usuarios para notificar", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                         End If
    '                     End If
    '                 Else ' todos los registros de la lista son validos
    '                     For Each mail As String In externalMails
    '                         mails.Add(mail)
    '                     Next
    '                     If Not String.IsNullOrEmpty(attachPath) Then
    '                         If Message_Factory.SendMail(mails, ccMail, ccoMail, asuntoMail, bodyMail, SendAsHtml, attachPath) Then
    '                             MessageBox.Show("Mensaje enviado con exito", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                         Else
    '                             MessageBox.Show("El mensaje no se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                         End If
    '                     Else
    '                         If Message_Factory.SendMail(mails, ccMail, ccoMail, asuntoMail, bodyMail, SendAsHtml) Then
    '                             MessageBox.Show("Mensaje enviado con exito", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                         Else
    '                             MessageBox.Show("El mensaje no se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                         End If
    '                     End If
    '                 End If
    '             Else ' no hay seleccionados usuario receptores del mail
    '                 MessageBox.Show("Seleccione usuarios a notificar", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '             End If
    '         Else ' el usuario no tiene la cuenta de mail configurada
    '             MessageBox.Show("Debe configurar la cuenta de correo de su usuario", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '         End If
    '     Catch ex As Exception
    '         ZClass.raiseerror(ex)
    '     End Try

    ' End Sub

    Public Shared Function GetGroupToNotifyMails(ByVal users As ArrayList) As Generic.List(Of String)
        Dim mails As New Generic.List(Of String)
        For Each id As Int64 In users
            mails.Add(NotifyFactory.GetMailToNotify(id))
        Next
        Return mails
    End Function

    Public Shared Function GetUserMail(ByVal parentID As Int32) As String
        Return NotifyFactory.GetMailToNotify(parentID)
    End Function
    Public Shared Function getAllSelectedUsers(ByVal typeGroupToNotify As GroupToNotifyTypes, ByVal docIds As Generic.List(Of Int64)) As Generic.List(Of Int64)
        Dim UsersIds As Generic.List(Of Int64)
        UsersIds = GetAllUserIDsToSave(typeGroupToNotify, docIds)
        Return UsersIds
    End Function

    Public Shared Function GetGroupToNotifyMails(ByVal userIDs As List(Of Int64)) As Generic.List(Of String)
        Dim mails As New Generic.List(Of String)
        For Each id As Int32 In userIDs
            mails.Add(NotifyFactory.GetMailToNotify(id))
        Next
        Return mails
    End Function

    Public Shared Function GetGroupToNotifyAllUsers(ByVal typeid As GroupToNotifyTypes, ByVal Id As Int64) As List(Of Int64)

        Dim lstGroupIDs As New List(Of String)
        Dim lstUserIDs As New List(Of String)
        Dim ds As New DataSet
        Dim lstResultUsers As New List(Of Int64)

        Try
            ds = NotifyFactory.GetUserToNotify(typeid, Id)
            If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In ds.Tables(0).Rows
                    If String.IsNullOrEmpty(r("UserId").ToString()) Then
                        If Not String.IsNullOrEmpty(r("GroupId").ToString()) Then
                            lstGroupIDs.Add(r("GroupId").ToString())
                        End If
                    Else
                        lstUserIDs.Add(r("UserId"))
                    End If
                Next
            End If
            For Each groupID As String In lstGroupIDs
                Dim tmpUserList As ArrayList
                tmpUserList = UserBusiness.GetGroupUsers(groupID)
                For Each tmpUserID As String In tmpUserList
                    lstUserIDs.Add(tmpUserID)
                Next
            Next

            For Each userID As String In lstUserIDs
                lstResultUsers.Add(Convert.ToInt64(userID))
            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return lstResultUsers

    End Function

    Public Shared Function GetGroupToNotifyUsers(ByVal typeid As GroupToNotifyTypes, ByVal Id As Int64) As List(Of Int64)

        Dim lstUserIDs As New List(Of Int64)
        Dim ds As New DataSet

        Try
            ds = NotifyFactory.GetUserToNotify(typeid, Id)
            If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In ds.Tables(0).Rows
                    If Not String.IsNullOrEmpty(r("UserId").ToString) AndAlso Int64.Parse(r("UserId").ToString) > 0 Then
                        lstUserIDs.Add(Convert.ToInt64(r("UserId")))
                    End If
                Next
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return lstUserIDs

    End Function
    Public Shared Function GetGroupToNotifyUsers(ByVal typeid As GroupToNotifyTypes, ByVal Ids As Generic.List(Of Int64)) As List(Of Int64)

        Dim lstUserIDs As New List(Of Int64)
        Dim ds As New DataSet

        Try
            ds = NotifyFactory.GetUserToNotify(typeid, Ids)
            If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In ds.Tables(0).Rows
                    If Not String.IsNullOrEmpty(r("UserId").ToString) AndAlso Int64.Parse(r("UserId").ToString) > 0 Then
                        lstUserIDs.Add(Convert.ToInt64(r("UserId")))
                    End If
                Next
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return lstUserIDs

    End Function

    Public Shared Function GetGroupToNotifyUsersAsArrayList(ByVal typeid As GroupToNotifyTypes, ByVal Id As Int64) As ArrayList

        Dim lstUserIDs As New ArrayList()
        Dim ds As New DataSet

        Try
            ds = NotifyFactory.GetUserToNotify(typeid, Id)
            If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In ds.Tables(0).Rows
                    If Not String.IsNullOrEmpty(r("UserId").ToString()) Then
                        lstUserIDs.Add(Convert.ToInt64(r("UserId")))
                    End If
                Next
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return lstUserIDs

    End Function

    Public Shared Function GetGroupToNotifyUserGroups(ByVal typeId As GroupToNotifyTypes, ByVal groupId As Int64) As List(Of Int64)
        Dim tmpUserGroups As New Generic.List(Of Int64)
        Dim ds As New DataSet
        Try
            ds = NotifyFactory.GetUserGroupToNotify(typeId, groupId)
            If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In ds.Tables(0).Rows
                    If Not String.IsNullOrEmpty(r("GroupId").ToString) AndAlso Int64.Parse(r("GroupId").ToString) > 0 Then
                        tmpUserGroups.Add(Convert.ToInt64(r("GroupId")))
                    End If
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return tmpUserGroups
    End Function
    Public Shared Function GetGroupToNotifyUserGroups(ByVal typeId As GroupToNotifyTypes, ByVal docIds As Generic.List(Of Int64)) As List(Of Int64)
        Dim tmpUserGroups As New Generic.List(Of Int64)
        Dim ds As New DataSet
        Try
            ds = NotifyFactory.GetUserGroupToNotify(typeId, docIds)
            If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In ds.Tables(0).Rows
                    If Not String.IsNullOrEmpty(r("GroupId").ToString) AndAlso Int64.Parse(r("GroupId").ToString) > 0 Then
                        tmpUserGroups.Add(Convert.ToInt64(r("GroupId")))
                    End If
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return tmpUserGroups
    End Function
    Public Shared Function GetAllUserIDsToNotify(ByVal typeID As GroupToNotifyTypes, ByVal groupID As Int64) As List(Of Int64)
        Dim tmpUserIDs1 As Generic.List(Of Int64) = GetGroupToNotifyUsers(typeID, groupID)
        Dim tmpGroupIDs As Generic.List(Of Int64) = GetGroupToNotifyUserGroups(typeID, groupID)
        Dim tmpUserIDs2 As New Generic.List(Of Int64)
        For Each gID As Int64 In tmpGroupIDs
            For Each uID As Int64 In UserGroupBusiness.GetUsersIds(gID, True)
                If Not tmpUserIDs2.Contains(uID) Then
                    tmpUserIDs2.Add(uID)
                End If
            Next
        Next
        For Each userId As Int64 In tmpUserIDs1
            If Not tmpUserIDs2.Contains(userId) Then
                tmpUserIDs2.Add(userId)
            End If
        Next
        Return tmpUserIDs2
    End Function
    Public Shared Function GetAllUserIDsToNotify(ByVal typeID As GroupToNotifyTypes, ByVal docIds As Generic.List(Of Int64)) As List(Of Int64)
        Dim tmpUserIDs1 As Generic.List(Of Int64) = GetGroupToNotifyUsers(typeID, docIds)
        Dim tmpGroupIDs As Generic.List(Of Int64) = GetGroupToNotifyUserGroups(typeID, docIds)
        Dim tmpUserIDs2 As New Generic.List(Of Int64)
        For Each gID As Int64 In tmpGroupIDs
            For Each uID As Int64 In UserGroupBusiness.GetUsersIds(gID, False)
                If Not tmpUserIDs2.Contains(uID) Then
                    tmpUserIDs2.Add(uID)
                End If
            Next
        Next
        For Each userId As Int64 In tmpUserIDs1
            If Not tmpUserIDs2.Contains(userId) Then
                tmpUserIDs2.Add(userId)
            End If
        Next
        Return tmpUserIDs2
    End Function
    Public Shared Function GetAllData(ByVal doc_id As Int64) As DataSet
        Return NotifyFactory.GetAllData(doc_id)
    End Function

    Public Shared Sub SaveAllData(ByVal doc_id As Int64, ByVal typeid As Int32, ByVal userid As Int64, ByVal extradata As String, ByVal groupid As Int64)
        NotifyFactory.SaveAllData(doc_id, typeid, userid, extradata, groupid)
    End Sub
    Public Shared Function GetGroupToNotifyGroups(ByVal typeid As GroupToNotifyTypes, ByVal Ids As Generic.List(Of Int64)) As List(Of Int64)
        Dim lstGroupIDs As New List(Of Int64)
        Dim ds As New DataSet

        Try
            ds = NotifyFactory.GetUserGroupToNotify(typeid, Ids)
            If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In ds.Tables(0).Rows
                    If Not String.IsNullOrEmpty(r("GroupId").ToString()) Then
                        lstGroupIDs.Add(Convert.ToInt64(r("GroupId")))
                    End If
                Next
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return lstGroupIDs

    End Function
    Public Shared Function GetGroupToNotifyGroups(ByVal typeid As GroupToNotifyTypes, ByVal Id As Int64) As List(Of Int64)
        Dim lstGroupIDs As New List(Of Int64)
        Dim ds As New DataSet

        Try
            ds = NotifyFactory.GetUserGroupToNotify(typeid, Id)
            If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In ds.Tables(0).Rows
                    If Not String.IsNullOrEmpty(r("GroupId").ToString()) Then
                        lstGroupIDs.Add(Convert.ToInt64(r("GroupId")))
                    End If
                Next
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return lstGroupIDs

    End Function


    Public Shared Function GetGroupExternalMails(ByVal typeid As GroupToNotifyTypes, ByVal Id As Int64) As ArrayList
        Dim users As ArrayList
        users = NotifyFactory.GetGroupExternalMails(typeid, Id)
        Return users
    End Function

    Public Shared Function ValidateGroupToNotifyExsist(ByVal _groupId As Int64) As Boolean
        Return NotifyFactory.ValidateGroupToNotifyExist(_groupId)
    End Function

    Public Shared Sub SetNewUserToNotify(ByVal typeid As GroupToNotifyTypes, ByVal _groupId As Int64, ByVal _userId As Int64)
        NotifyFactory.SetNewUserToNotify(typeid, _groupId, _userId)
    End Sub

    Public Shared Sub SetNewUserGroupToNotify(ByVal typeid As GroupToNotifyTypes, ByVal _groupId As Int64, ByVal _userGroupId As Int64)
        NotifyFactory.SetNewUserGroupToNotify(typeid, _groupId, _userGroupId)
    End Sub

    Public Shared Sub SetNewMailToNotify(ByVal typeid As GroupToNotifyTypes, ByVal _groupId As Int64, ByVal _mail As String)
        NotifyFactory.SetNewMailToNotify(typeid, _groupId, _mail)
    End Sub

    Public Shared Sub SetNewUserToNotify(ByVal typeid As GroupToNotifyTypes, ByVal _groupId As Int64, ByVal _userId As Int64())
        For Each i As Int64 In _userId
            NotifyFactory.SetNewUserToNotify(typeid, _groupId, i)
        Next
    End Sub

    Public Shared Sub DeleteUserToNotify(ByVal _groupId As Int64, ByVal _userId As Int64)
        Try
            NotifyFactory.DeleteUserToNotify(_groupId, _userId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub DeleteMailToNotify(ByVal _groupId As Int64, ByVal _mail As String)
        Try
            NotifyFactory.DeleteMailToNotify(_groupId, _mail)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub DeleteNotify(ByVal documentID As Int64, ByVal typeNotify As GroupToNotifyTypes)
        NotifyFactory.DeleteNotify(documentID, typeNotify)
    End Sub

    Public Shared Sub DeleteUserGroupToNotify(ByVal _groupId As Int64, ByVal _userGroupId As Int64)
        Try
            NotifyFactory.DeleteUserGroupToNotify(_groupId, _userGroupId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo | Valida si el usuario ya pertenece al Grupo de Notificacion indicado. Si ya pertenece devuelve True. 
    ''' </summary>
    Public Shared Function ValidateUserInGroupToNotify(ByVal _groupId As Int64, ByVal _userId As Int64) As Boolean
        Return NotifyFactory.ValidateUserInGroupToNotify(_groupId, _userId)
    End Function

    Public Shared Function GetAllUserIDsToSave(ByVal typeID As GroupToNotifyTypes, ByVal docIds As Generic.List(Of Int64)) As List(Of Int64)
        Dim tmpUserIDs1 As Generic.List(Of Int64) = GetGroupToNotifyUsers(typeID, docIds)
        Dim tmpGroupIDs As Generic.List(Of Int64) = GetGroupToNotifyUserGroups(typeID, docIds)
        Dim tmpUserIDs2 As New Generic.List(Of Int64)
        For Each gID As Int64 In tmpGroupIDs
            tmpUserIDs2.Add(gID)
        Next
        For Each userId As Int64 In tmpUserIDs1
            If Not tmpUserIDs2.Contains(userId) Then
                tmpUserIDs2.Add(userId)
            End If
        Next
        Return tmpUserIDs2
    End Function
End Class
