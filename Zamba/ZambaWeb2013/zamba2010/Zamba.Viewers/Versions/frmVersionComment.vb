Imports Zamba.AppBlock
Imports Zamba.Core
Imports System.Text

Public Class frmVersionComment
    Inherits ZForm

#Region " Código generado por el Diseñador de Windows Forms "

    

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents btnCancelar As System.windows.forms.button
    Friend WithEvents btnNuevaVersion As System.windows.forms.button
    Friend WithEvents txtComentario As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnUsuarios As System.Windows.Forms.Button
    Friend WithEvents chkNotificar As System.Windows.Forms.CheckBox
    Friend WithEvents gbNotificacion As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rdbAttachDocument As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAttachLink As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAttachNothing As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVersionComment))
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.btnNuevaVersion = New System.Windows.Forms.Button
        Me.txtComentario = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnUsuarios = New System.Windows.Forms.Button
        Me.chkNotificar = New System.Windows.Forms.CheckBox
        Me.gbNotificacion = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.rdbAttachDocument = New System.Windows.Forms.RadioButton
        Me.rdbAttachLink = New System.Windows.Forms.RadioButton
        Me.rdbAttachNothing = New System.Windows.Forms.RadioButton
        Me.gbNotificacion.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(203, 340)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(96, 25)
        Me.btnCancelar.TabIndex = 5
        Me.btnCancelar.Text = "Cancelar"
        '
        'btnNuevaVersion
        '
        Me.btnNuevaVersion.Location = New System.Drawing.Point(91, 340)
        Me.btnNuevaVersion.Name = "btnNuevaVersion"
        Me.btnNuevaVersion.Size = New System.Drawing.Size(96, 25)
        Me.btnNuevaVersion.TabIndex = 4
        Me.btnNuevaVersion.Text = "Nueva Version"
        '
        'txtComentario
        '
        Me.txtComentario.BackColor = System.Drawing.Color.White
        Me.txtComentario.Location = New System.Drawing.Point(16, 112)
        Me.txtComentario.Multiline = True
        Me.txtComentario.Name = "txtComentario"
        Me.txtComentario.Size = New System.Drawing.Size(352, 86)
        Me.txtComentario.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(16, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(352, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Comentario"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(16, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(352, 77)
        Me.Label1.TabIndex = 5
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUsuarios
        '
        Me.btnUsuarios.Location = New System.Drawing.Point(241, 40)
        Me.btnUsuarios.Name = "btnUsuarios"
        Me.btnUsuarios.Size = New System.Drawing.Size(96, 25)
        Me.btnUsuarios.TabIndex = 3
        Me.btnUsuarios.Text = "Usuarios"
        '
        'chkNotificar
        '
        Me.chkNotificar.AutoSize = True
        Me.chkNotificar.Location = New System.Drawing.Point(16, 212)
        Me.chkNotificar.Name = "chkNotificar"
        Me.chkNotificar.Size = New System.Drawing.Size(119, 17)
        Me.chkNotificar.TabIndex = 8
        Me.chkNotificar.Text = "Notificar a Usuarios"
        Me.chkNotificar.UseVisualStyleBackColor = True
        '
        'gbNotificacion
        '
        Me.gbNotificacion.Controls.Add(Me.Label3)
        Me.gbNotificacion.Controls.Add(Me.rdbAttachDocument)
        Me.gbNotificacion.Controls.Add(Me.rdbAttachLink)
        Me.gbNotificacion.Controls.Add(Me.btnUsuarios)
        Me.gbNotificacion.Controls.Add(Me.rdbAttachNothing)
        Me.gbNotificacion.Enabled = False
        Me.gbNotificacion.Location = New System.Drawing.Point(16, 235)
        Me.gbNotificacion.Name = "gbNotificacion"
        Me.gbNotificacion.Size = New System.Drawing.Size(352, 92)
        Me.gbNotificacion.TabIndex = 9
        Me.gbNotificacion.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Adjuntar:"
        '
        'rdbAttachDocument
        '
        Me.rdbAttachDocument.AutoSize = True
        Me.rdbAttachDocument.Location = New System.Drawing.Point(75, 60)
        Me.rdbAttachDocument.Name = "rdbAttachDocument"
        Me.rdbAttachDocument.Size = New System.Drawing.Size(79, 17)
        Me.rdbAttachDocument.TabIndex = 13
        Me.rdbAttachDocument.Text = "Documento"
        Me.rdbAttachDocument.UseVisualStyleBackColor = True
        '
        'rdbAttachLink
        '
        Me.rdbAttachLink.AutoSize = True
        Me.rdbAttachLink.Location = New System.Drawing.Point(75, 40)
        Me.rdbAttachLink.Name = "rdbAttachLink"
        Me.rdbAttachLink.Size = New System.Drawing.Size(43, 17)
        Me.rdbAttachLink.TabIndex = 12
        Me.rdbAttachLink.Text = "Link"
        Me.rdbAttachLink.UseVisualStyleBackColor = True
        '
        'rdbAttachNothing
        '
        Me.rdbAttachNothing.AutoSize = True
        Me.rdbAttachNothing.Checked = True
        Me.rdbAttachNothing.Location = New System.Drawing.Point(75, 21)
        Me.rdbAttachNothing.Name = "rdbAttachNothing"
        Me.rdbAttachNothing.Size = New System.Drawing.Size(64, 17)
        Me.rdbAttachNothing.TabIndex = 11
        Me.rdbAttachNothing.TabStop = True
        Me.rdbAttachNothing.Text = "Ninguno"
        Me.rdbAttachNothing.UseVisualStyleBackColor = True
        '
        'frmVersionComment
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(385, 379)
        Me.Controls.Add(Me.gbNotificacion)
        Me.Controls.Add(Me.chkNotificar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnNuevaVersion)
        Me.Controls.Add(Me.txtComentario)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVersionComment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Comentario de Version"
        Me.gbNotificacion.ResumeLayout(False)
        Me.gbNotificacion.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


#Region "Propiedades"
    Public ReadOnly Property Comment() As String
        Get
            Return Me.txtComentario.Text
        End Get
    End Property

    Private _Result As Result
    Public ReadOnly Property GetNewResult() As Result
        Get
            Return _Result
        End Get
    End Property

#End Region

    Dim ParentResult As Result
    Dim notifyByMail As Boolean = False
    Dim _tempPath As String

#Region "Constructores"
    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    Public Sub New(ByRef _ParentResult As Result)
        Me.New()
        notifyByMail = UserBusiness.Rights.GetUserRights(ObjectTypes.DocTypes, RightsType.NotifyOptions, _ParentResult.DocType.ID)
        rdbAttachDocument.Visible = notifyByMail
        ParentResult = _ParentResult
        Try
            Me.Label1.Text = _ParentResult.Name & ControlChars.NewLine & _ParentResult.OriginalName & ControlChars.NewLine & "Fecha de Creacion: " & _ParentResult.CreateDate & ControlChars.NewLine
            Me.Label1.Text &= "Fecha de Modificacion: " & _ParentResult.EditDate & ControlChars.NewLine
            'todo: mostrar mas info de versiones
        Catch
        End Try
    End Sub

    Public Sub New(ByRef _ParentResult As Result, ByVal tempPath As String)
        'Uso esta sobrecarga para version automatica, ya que se usa el path del archivo
        'Local en ves de la del servidor
        Me.New()
        notifyByMail = UserBusiness.Rights.GetUserRights(ObjectTypes.DocTypes, RightsType.NotifyOptions, _ParentResult.DocType.ID)
        rdbAttachDocument.Visible = notifyByMail
        ParentResult = _ParentResult
        _tempPath = tempPath
        Try
            Me.Label1.Text = _ParentResult.Name & ControlChars.NewLine & _ParentResult.OriginalName & ControlChars.NewLine & "Fecha de Creacion: " & _ParentResult.CreateDate & ControlChars.NewLine
            Me.Label1.Text &= "Fecha de Modificacion: " & _ParentResult.EditDate & ControlChars.NewLine
            'todo: mostrar mas info de versiones
        Catch
        End Try
    End Sub
#End Region

#Region "Eventos"
    Private Sub frmVersionComment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ChangeFormStyle()
    End Sub
    Private Sub btnNuevaVersion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevaVersion.Click
        Dim Sended As Boolean = False
        Try

            If String.IsNullOrEmpty(_tempPath) Then
                Me._Result = Results_Business.InsertNewVersionNoComment(ParentResult)
            Else
                Me._Result = Results_Business.InsertNewVersionNoComment(ParentResult, _tempPath)
            End If
            Results_Business.SaveVersionComment(Me._Result.ID, Me.Comment)
            Me.DialogResult = Windows.Forms.DialogResult.OK
            If Me.chkNotificar.Checked Then
                Select Case notifyByMail
                    Case True
                        Sended = SendNewVersionNotifyByMail(Me._Result.ID, Me._Result.DocType.ID, Me._Result.Name)
                    Case False
                        Sended = SendNewVersionNotifyByInternalMsj()
                End Select
                If Sended = False Then
                    MessageBox.Show("La nueva version se creo correctamente, Pero hubo un problema al notificar a usuario", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            raiseerror(ex)
            MessageBox.Show("No se ha podido Notificar la Nueva Versión.", "Zamba Software", MessageBoxButtons.OKCancel)
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End Try
        Me.Close()
    End Sub
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub
    Private Sub btnUsuarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsuarios.Click
        Dim newFrmSelectUsers As frmSelectUsers = New frmSelectUsers(GroupToNotifyTypes.NotifyOnly, ParentResult.ID, ParentResult.DocType.ID)
        newFrmSelectUsers.StartPosition = FormStartPosition.CenterParent
        newFrmSelectUsers.ShowDialog()
    End Sub
    Private Sub chkNotificar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNotificar.CheckedChanged
        ChangeFormStyle()

    End Sub
#End Region

#Region "Métodos"
    Private Function SendNewVersionNotifyByInternalMsj() As Boolean
        Try
            Dim UsersIds As Generic.List(Of Int64)
            Dim users As New Generic.List(Of User)
            Dim link As String

            'Obtiene todos los usuarios seleccionados y de grupos
            UsersIds = NotifyBusiness.GetAllUserIDsToNotify(GroupToNotifyTypes.NotifyOnly, _Result.ID)
            For Each userid As Int64 In UsersIds
                If Not IsNothing(UserBusiness.GetUserById(userid)) Then
                    users.Add(DirectCast(UserBusiness.GetUserById(userid), User))
                End If
            Next
            If users.Count > 0 Then
                Dim msgBody As New StringBuilder()
                msgBody.Append(Chr(13))
                msgBody.Append(Me.txtComentario.Text)
                msgBody.Append(Chr(13))
                msgBody.Append(Chr(13))
                Select Case rdbAttachNothing.Checked
                    Case True
                        InternalMessage.SendInternalMessage("Notificación de Nueva Version", Now.Date, msgBody.ToString, users, MessageType.MailCCO)
                        MessageBox.Show("Notificacion Enviada", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return True
                    Case False
                        Select Case rdbAttachLink.Checked
                            Case True
                                link = Results_Business.GetLinkFromResult(_Result)
                                msgBody.Append("Link de acceso a la nueva versión: ")
                                msgBody.Append(Chr(13))
                                msgBody.Append(link)
                                msgBody.Append(Chr(13))
                                InternalMessage.SendInternalMessage("Notificación de Nueva Version", Now.Date, msgBody.ToString, users, MessageType.MailCCO)
                                MessageBox.Show("Notificacion Enviada", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return True
                            Case False 'ADJUNTA ARCHIVO
                                'OK: No existe Adjuntos para msj Interno
                        End Select
                End Select

            Else
                MessageBox.Show("No hay usuarios para notificar", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Public Function SendNewVersionNotifyByMail(ByVal resultID As Int64, ByVal docTypeId As Int64, Optional ByVal resultName As String = "") As Boolean

        Try
            Dim reciepusers As New ArrayList
            Dim UsersIds As Generic.List(Of Int64)
            Dim mails As New Generic.List(Of String)
            Dim externalMails As ArrayList
            Dim sendmails As Boolean = True
            Dim link As String

            Dim msgBody As New StringBuilder()
            msgBody.Append(Chr(13))
            msgBody.Append(Me.txtComentario.Text)
            msgBody.Append(Chr(13))
            msgBody.Append(Chr(13))


            'valida que el usuario emisor del mail tenga configurado su mail
            If String.Compare(Zamba.Core.UserBusiness.CurrentUser.eMail.Mail, String.Empty) <> 0 Then

                'Obtiene todos los usuarios seleccionados y de grupos
                UsersIds = NotifyBusiness.GetAllUserIDsToNotify(GroupToNotifyTypes.NotifyOnly, ParentResult.ID)

                For Each userid As Int64 In UsersIds
                    reciepusers.Add(userid)
                Next

                ' obtiene los mails externos (escritos a mano)
                externalMails = NotifyBusiness.GetGroupExternalMails(GroupToNotifyTypes.NotifyOnly, ParentResult.ID)

                mails = NotifyBusiness.GetGroupToNotifyMails(reciepusers)

                Dim validMails As New Generic.List(Of String)

                For Each s As String In mails
                    If String.IsNullOrEmpty(s) = False Then
                        validMails.Add(s)
                    End If
                Next

                If validMails.Count > 0 Or externalMails.Count > 0 Then
                    'publica el documentos

                    ' Valida que se elimino algun registro por no tener un mail valido
                    If validMails.Count = mails.Count OrElse MessageBox.Show("Hay usuarios sin cuentas de mail definidas, ¿Desea continuar el envio?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                        For Each mail As String In externalMails
                            validMails.Add(mail)
                        Next
                        Select Case rdbAttachNothing.Checked
                            Case True
                                'If Not Message_Factory.SendMail(validMails, String.Empty, String.Empty, "Notificación de Nueva Version", msgBody.ToString(), False) Then
                                If Not MessagesBusiness.SendMail(validMails.ToString, String.Empty, String.Empty, "Notificación de Nueva Version", msgBody.ToString(), False, Nothing) Then
                                    MessageBox.Show("El mensaje no se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Return False
                                Else
                                    MessageBox.Show("El E-Mail se envio correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Return True
                                End If
                            Case False
                                Select Case rdbAttachLink.Checked
                                    Case True
                                        link = Results_Business.GetHtmlLinkFromResult(_Result.DocType.ID, _Result.ID, _Result.Name)
                                        msgBody.Append("Link de acceso a la nueva versión: ")
                                        msgBody.Append(Chr(13))
                                        msgBody.Append(link)
                                        msgBody.Append(Chr(13))
                                        'If Not Message_Factory.SendMail(validMails, String.Empty, String.Empty, "Notificación de Nueva Versión", msgBody.ToString(), True) Then
                                        If Not MessagesBusiness.SendMail(validMails.ToString, String.Empty, String.Empty, "Notificación de Nueva Version", msgBody.ToString(), True, Nothing) Then
                                            MessageBox.Show("El mensaje no se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Return False
                                        Else
                                            MessageBox.Show("El E-Mail se envio correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Return True
                                        End If
                                    Case False 'ADJUNTA ARCHIVO
                                        'Dim attachPath As String = Results_Business.GetFullName(_Result.ID, _Result.DocType.ID)
                                        'If Not Message_Factory.SendMail(validMails, String.Empty, String.Empty, "Notificación de Nueva Versión", msgBody.ToString(), False, _Result.FullPath) Then
                                        Dim ListAttach As New Generic.List(Of String)
                                        ListAttach.Add(_Result.FullPath)
                                        If Not MessagesBusiness.SendMail(validMails.ToString, String.Empty, String.Empty, "Notificación de Nueva Version", msgBody.ToString(), False, ListAttach) Then
                                            MessageBox.Show("El mensaje no se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Return False
                                        Else
                                            MessageBox.Show("El E-Mail se envio correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Return True
                                        End If
                                End Select
                        End Select
                    End If
                Else
                    MessageBox.Show("No hay usuarios para notificar", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            Else ' el usuario no tiene la cuenta de mail configurada
                MessageBox.Show("Debe configurar la cuenta de correo de su usuario", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function
    Private Sub ChangeFormStyle()
        Select Case Me.chkNotificar.Checked
            Case False
                Me.gbNotificacion.Enabled = False
                Me.gbNotificacion.Visible = False
                Dim drawCancel As New Drawing.Point(203, 250)
                Dim drawNewVersion As New Drawing.Point(91, 250)
                btnCancelar.Location = drawCancel
                btnNuevaVersion.Location = drawNewVersion
                Me.Height = 287
            Case True
                Me.gbNotificacion.Enabled = True
                Me.gbNotificacion.Visible = True
                Dim drawCancel As New Drawing.Point(203, 340)
                Dim drawNewVersion As New Drawing.Point(91, 340)
                btnCancelar.Location = drawCancel
                btnNuevaVersion.Location = drawNewVersion
                Me.Height = 379
        End Select
    End Sub
#End Region
End Class
