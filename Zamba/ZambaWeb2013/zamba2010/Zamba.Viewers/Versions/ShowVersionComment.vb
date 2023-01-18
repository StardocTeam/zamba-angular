Imports Zamba.Core

Public Class frmShowVersionComment
    Inherits Zamba.AppBlock.ZForm

    Private UCIndexViewer As Viewers.UCIndexViewer
    Private IndexsIds As New ArrayList
    Private _Publishresult As PublishableResult
    Private _result As Result
    Dim notifybymail As Boolean

#Region " Constructor "
    Public Sub New(ByRef Publishresult As PublishableResult, ByVal result As Result, ByRef comment As String, ByRef fecha As String)
        InitializeComponent()
        _Publishresult = Publishresult
        _result = result
        Label2.Text = fecha
        lblCreador.Text = "Usuario Creador: " & Results_Business.GetCreatorUser(result.ID)
        TextBox1.Text = comment
        IndexsIds.AddRange(IndexsBusiness.GetIndexsIdsByDocTypeId(result.DocType.ID))
        UCIndexViewer = New Viewers.UCIndexViewer(False)
        UCIndexViewer.Dock = DockStyle.Fill
        Me.PanelIndex.Controls.Add(UCIndexViewer)
        UCIndexViewer.ShowEspecifiedIndexs(DirectCast(result, Result), IndexsIds)

        ' rights
        Dim Rights As Boolean = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.PublishVersions, result.DocType.ID)
        BtnAddUsers.Visible = Rights
        BtnNotify.Visible = Rights
        BntPublish.Visible = Rights
        notifybymail = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.NotifyOptions, _result.DocType.ID)

    End Sub
#End Region

    Private Sub PanelIndex_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    'Private Function GetIndexs(ByVal DoctypeId As Long) As ArrayList
    '    Return IndexsBusiness.getIndexsByDocTypeId(DoctypeId)
    'End Function

    Private Sub BntPublish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BntPublish.Click
        Try
            Results_Business.SavePublish(_result, _Publishresult)
            Me.Close()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub frmShowVersionComment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BtnNotify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNotify.Click
        If notifybymail = True Then
            PublishAndSendMail()
        Else
            PublishAndSendInternalMessage()
        End If

    End Sub

    Private Sub PublishAndSendMail()
        Try
            Dim reciepusers As New ArrayList
            Dim UsersIds As Generic.List(Of Int64)
            Dim mails As New Generic.List(Of String)
            Dim externalMails As ArrayList
            Dim sendmails As Boolean = True
            Dim link As String

            'valida que el usuario emisor del mail tenga configurado su mail
            If String.Compare(Zamba.Core.UserBusiness.CurrentUser.eMail.Mail, String.Empty) <> 0 Then

                'Obtiene todos los usuarios seleccionados y de grupos
                UsersIds = NotifyBusiness.GetAllUserIDsToNotify(GroupToNotifyTypes.Publish, _result.ID)

                For Each userid As Int64 In UsersIds
                    reciepusers.Add(userid)
                Next

                ' obtiene los mails externos (escritos a mano)
                externalMails = NotifyBusiness.GetGroupExternalMails(GroupToNotifyTypes.Publish, _result.ID)

                mails = NotifyBusiness.GetGroupToNotifyMails(reciepusers)

                Dim validMails As New Generic.List(Of String)

                For Each s As String In mails
                    If String.IsNullOrEmpty(s) = False Then
                        validMails.Add(s)
                    End If
                Next

                If validMails.Count > 0 Or externalMails.Count > 0 Then
                    'publica el documentos
                    Results_Business.SavePublish(_result, _Publishresult)
                    ' Valida que se elimino algun registro por no tener un mail valido
                    If validMails.Count = mails.Count OrElse MessageBox.Show("Hay usuarios sin cuentas de mail definidas, ¿Desea continuar el envio?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                        For Each mail As String In externalMails
                            validMails.Add(mail)
                        Next
                        link = Results_Business.GetHtmlLinkFromResult(_result.DocType.ID, _result.ID, _result.Name)
                        Dim frm As New NotifyPublish(link, validMails, _result.FullPath)
                        frm.ShowDialog()
                        Me.Close()
                    End If
                Else
                    MessageBox.Show("No hay usuarios para notificar", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else ' el usuario no tiene la cuenta de mail configurada
                MessageBox.Show("Debe configurar la cuenta de correo de su usuario", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
 
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub PublishAndSendInternalMessage()
        Try
            Dim UsersIds As Generic.List(Of Int64)
            Dim users As New Generic.List(Of User)
            Dim link As String

            'Obtiene todos los usuarios seleccionados y de grupos
            UsersIds = NotifyBusiness.GetAllUserIDsToNotify(GroupToNotifyTypes.Publish, _result.ID)
            For Each userid As Int64 In UsersIds
                If Not IsNothing(UserBusiness.GetUserById(userid)) Then
                    users.Add(DirectCast(UserBusiness.GetUserById(userid), User))
                End If
            Next
            If users.Count > 0 Then
                'publica el documentos
                Results_Business.SavePublish(_result, _Publishresult)

                link = Results_Business.GetLinkFromResult(_result)
                Dim frm As New NotifyPublish(link, users)
                frm.ShowDialog()
                Me.Close()
            Else
                MessageBox.Show("No hay usuarios para notificar", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnAddUsers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddUsers.Click
        Dim frm As New frmSelectUsers(GroupToNotifyTypes.Publish, _result.ID, _result.DocType.ID)
        frm.ShowDialog()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim frm As New frmVersionedDetails(_result)
        frm.ShowDialog()
    End Sub
End Class