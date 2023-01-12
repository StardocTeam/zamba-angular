Imports Zamba.Core
Imports System.Text

Public Class frmGenerateNewVersion
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
    Friend WithEvents btnCancelar As ZButton
    Friend WithEvents btnNuevaVersion As ZButton
    Friend WithEvents txtComentario As TextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents btnUsuarios As ZButton
    Friend WithEvents chkNotificar As System.Windows.Forms.CheckBox
    Friend WithEvents gbNotificacion As GroupBox
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents rdbAttachDocument As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAttachLink As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAttachNothing As System.Windows.Forms.RadioButton
    Friend WithEvents lblDN As ZLabel
    Friend WithEvents lblVNum As ZLabel
    Friend WithEvents lblPVerNum As ZLabel
    Friend WithEvents lblDocName As ZLabel
    Friend WithEvents lblVersionNum As ZLabel
    Friend WithEvents lblParentVersionNum As ZLabel
    Friend WithEvents GroupBox1 As GroupBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(frmGenerateNewVersion))
        btnCancelar = New ZButton()
        btnNuevaVersion = New ZButton()
        txtComentario = New TextBox()
        Label2 = New ZLabel()
        btnUsuarios = New ZButton()
        chkNotificar = New System.Windows.Forms.CheckBox()
        gbNotificacion = New GroupBox()
        Label3 = New ZLabel()
        rdbAttachDocument = New System.Windows.Forms.RadioButton()
        rdbAttachLink = New System.Windows.Forms.RadioButton()
        rdbAttachNothing = New System.Windows.Forms.RadioButton()
        lblDN = New ZLabel()
        lblVNum = New ZLabel()
        lblPVerNum = New ZLabel()
        lblDocName = New ZLabel()
        lblVersionNum = New ZLabel()
        lblParentVersionNum = New ZLabel()
        GroupBox1 = New GroupBox()
        gbNotificacion.SuspendLayout()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        '
        'btnCancelar
        '
        btnCancelar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnCancelar.ForeColor = System.Drawing.Color.White
        btnCancelar.Location = New System.Drawing.Point(236, 411)
        btnCancelar.Name = "btnCancelar"
        btnCancelar.Size = New System.Drawing.Size(135, 28)
        btnCancelar.TabIndex = 5
        btnCancelar.Text = "Cancelar"
        btnCancelar.UseVisualStyleBackColor = False
        '
        'btnNuevaVersion
        '
        btnNuevaVersion.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnNuevaVersion.FlatStyle = FlatStyle.Flat
        btnNuevaVersion.ForeColor = System.Drawing.Color.White
        btnNuevaVersion.Location = New System.Drawing.Point(95, 411)
        btnNuevaVersion.Name = "btnNuevaVersion"
        btnNuevaVersion.Size = New System.Drawing.Size(135, 28)
        btnNuevaVersion.TabIndex = 4
        btnNuevaVersion.Text = "Nueva Version"
        btnNuevaVersion.UseVisualStyleBackColor = False
        '
        'txtComentario
        '
        txtComentario.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtComentario.BackColor = System.Drawing.Color.White
        txtComentario.Location = New System.Drawing.Point(15, 160)
        txtComentario.Multiline = True
        txtComentario.Name = "txtComentario"
        txtComentario.Size = New System.Drawing.Size(501, 98)
        txtComentario.TabIndex = 1
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(12, 138)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(105, 19)
        Label2.TabIndex = 6
        Label2.Text = "Comentario:"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'btnUsuarios
        '
        btnUsuarios.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnUsuarios.FlatStyle = FlatStyle.Flat
        btnUsuarios.ForeColor = System.Drawing.Color.White
        btnUsuarios.Location = New System.Drawing.Point(287, 42)
        btnUsuarios.Name = "btnUsuarios"
        btnUsuarios.Size = New System.Drawing.Size(135, 28)
        btnUsuarios.TabIndex = 3
        btnUsuarios.Text = "Usuarios"
        btnUsuarios.UseVisualStyleBackColor = False
        '
        'chkNotificar
        '
        chkNotificar.AutoSize = True
        chkNotificar.Location = New System.Drawing.Point(15, 273)
        chkNotificar.Name = "chkNotificar"
        chkNotificar.Size = New System.Drawing.Size(155, 20)
        chkNotificar.TabIndex = 8
        chkNotificar.Text = "Notificar a Usuarios"
        chkNotificar.UseVisualStyleBackColor = True
        '
        'gbNotificacion
        '
        gbNotificacion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        gbNotificacion.Controls.Add(Label3)
        gbNotificacion.Controls.Add(rdbAttachDocument)
        gbNotificacion.Controls.Add(rdbAttachLink)
        gbNotificacion.Controls.Add(btnUsuarios)
        gbNotificacion.Controls.Add(rdbAttachNothing)
        gbNotificacion.Enabled = False
        gbNotificacion.Location = New System.Drawing.Point(15, 299)
        gbNotificacion.Name = "gbNotificacion"
        gbNotificacion.Size = New System.Drawing.Size(501, 101)
        gbNotificacion.TabIndex = 9
        gbNotificacion.TabStop = False
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(17, 26)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(70, 16)
        Label3.TabIndex = 10
        Label3.Text = "Adjuntar:"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'rdbAttachDocument
        '
        rdbAttachDocument.AutoSize = True
        rdbAttachDocument.Location = New System.Drawing.Point(105, 69)
        rdbAttachDocument.Name = "rdbAttachDocument"
        rdbAttachDocument.Size = New System.Drawing.Size(100, 20)
        rdbAttachDocument.TabIndex = 13
        rdbAttachDocument.Text = "Documento"
        rdbAttachDocument.UseVisualStyleBackColor = True
        '
        'rdbAttachLink
        '
        rdbAttachLink.AutoSize = True
        rdbAttachLink.Location = New System.Drawing.Point(105, 46)
        rdbAttachLink.Name = "rdbAttachLink"
        rdbAttachLink.Size = New System.Drawing.Size(51, 20)
        rdbAttachLink.TabIndex = 12
        rdbAttachLink.Text = "Link"
        rdbAttachLink.UseVisualStyleBackColor = True
        '
        'rdbAttachNothing
        '
        rdbAttachNothing.AutoSize = True
        rdbAttachNothing.Checked = True
        rdbAttachNothing.Location = New System.Drawing.Point(105, 24)
        rdbAttachNothing.Name = "rdbAttachNothing"
        rdbAttachNothing.Size = New System.Drawing.Size(78, 20)
        rdbAttachNothing.TabIndex = 11
        rdbAttachNothing.TabStop = True
        rdbAttachNothing.Text = "Ninguno"
        rdbAttachNothing.UseVisualStyleBackColor = True
        '
        'lblDN
        '
        lblDN.AutoSize = True
        lblDN.BackColor = System.Drawing.Color.Transparent
        lblDN.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblDN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblDN.Location = New System.Drawing.Point(10, 27)
        lblDN.Name = "lblDN"
        lblDN.Size = New System.Drawing.Size(166, 16)
        lblDN.TabIndex = 10
        lblDN.Text = "Nombre del Documento:"
        lblDN.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblVNum
        '
        lblVNum.AutoSize = True
        lblVNum.BackColor = System.Drawing.Color.Transparent
        lblVNum.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblVNum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblVNum.Location = New System.Drawing.Point(10, 54)
        lblVNum.Name = "lblVNum"
        lblVNum.Size = New System.Drawing.Size(198, 16)
        lblVNum.TabIndex = 10
        lblVNum.Text = "Número de la Nueva Versión:"
        lblVNum.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblPVerNum
        '
        lblPVerNum.AutoSize = True
        lblPVerNum.BackColor = System.Drawing.Color.Transparent
        lblPVerNum.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblPVerNum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblPVerNum.Location = New System.Drawing.Point(10, 81)
        lblPVerNum.Name = "lblPVerNum"
        lblPVerNum.Size = New System.Drawing.Size(178, 16)
        lblPVerNum.TabIndex = 10
        lblPVerNum.Text = "Número de Version Padre:"
        lblPVerNum.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblDocName
        '
        lblDocName.AutoSize = True
        lblDocName.BackColor = System.Drawing.Color.Transparent
        lblDocName.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblDocName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblDocName.Location = New System.Drawing.Point(182, 27)
        lblDocName.Name = "lblDocName"
        lblDocName.Size = New System.Drawing.Size(69, 16)
        lblDocName.TabIndex = 10
        lblDocName.Text = "DocName"
        lblDocName.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblVersionNum
        '
        lblVersionNum.AutoSize = True
        lblVersionNum.BackColor = System.Drawing.Color.Transparent
        lblVersionNum.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblVersionNum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblVersionNum.Location = New System.Drawing.Point(214, 54)
        lblVersionNum.Name = "lblVersionNum"
        lblVersionNum.Size = New System.Drawing.Size(83, 16)
        lblVersionNum.TabIndex = 10
        lblVersionNum.Text = "VersionNum"
        lblVersionNum.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblParentVersionNum
        '
        lblParentVersionNum.AutoSize = True
        lblParentVersionNum.BackColor = System.Drawing.Color.Transparent
        lblParentVersionNum.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblParentVersionNum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblParentVersionNum.Location = New System.Drawing.Point(194, 81)
        lblParentVersionNum.Name = "lblParentVersionNum"
        lblParentVersionNum.Size = New System.Drawing.Size(100, 16)
        lblParentVersionNum.TabIndex = 10
        lblParentVersionNum.Text = "ParentVerNum"
        lblParentVersionNum.TextAlign = ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        GroupBox1.Controls.Add(lblDN)
        GroupBox1.Controls.Add(lblDocName)
        GroupBox1.Controls.Add(lblVNum)
        GroupBox1.Controls.Add(lblVersionNum)
        GroupBox1.Controls.Add(lblParentVersionNum)
        GroupBox1.Controls.Add(lblPVerNum)
        GroupBox1.Location = New System.Drawing.Point(15, 5)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(501, 121)
        GroupBox1.TabIndex = 11
        GroupBox1.TabStop = False
        GroupBox1.Text = "Datos Nueva Versión"
        '
        'frmGenerateNewVersion
        '
        AutoScaleBaseSize = New System.Drawing.Size(7, 16)
        BackColor = System.Drawing.Color.White
        ClientSize = New System.Drawing.Size(533, 453)
        Controls.Add(gbNotificacion)
        Controls.Add(chkNotificar)
        Controls.Add(btnCancelar)
        Controls.Add(btnNuevaVersion)
        Controls.Add(txtComentario)
        Controls.Add(Label2)
        Controls.Add(GroupBox1)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmGenerateNewVersion"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Text = "Nueva Version"
        gbNotificacion.ResumeLayout(False)
        gbNotificacion.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region

#Region "Atributos y Propiedades"

    Dim ParentResult As Result
    Dim notifyByMail As Boolean = False
    Dim _tempPath As String

    Public ReadOnly Property Comment() As String
        Get
            Return txtComentario.Text
        End Get
    End Property

    Private _Result As Result
    Public ReadOnly Property GetNewResult() As Result
        Get
            Return _Result
        End Get
    End Property

#End Region

#Region "Constructores"
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Public Sub New(ByRef _ParentResult As Result)
        Me.New()
        notifyByMail = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.NotifyOptions, _ParentResult.DocType.ID)
        rdbAttachDocument.Visible = notifyByMail
        ParentResult = _ParentResult

        Try
            lblDocName.Text = _ParentResult.Name & "(" & _ParentResult.OriginalName & ")"
            lblVersionNum.Text = Results_Business.GetNewVersionID(_ParentResult.RootDocumentId, _ParentResult.DocTypeId, _ParentResult.ID).ToString
            lblParentVersionNum.Text = _ParentResult.VersionNumber

            'Me.Label1.Text = _ParentResult.Name & ControlChars.NewLine & _ParentResult.OriginalName & ControlChars.NewLine & "Fecha de Creacion: " & _ParentResult.CreateDate & ControlChars.NewLine
            'Me.Label1.Text &= "Fecha de Modificacion: " & _ParentResult.EditDate & ControlChars.NewLine
            ''todo: mostrar mas info de versiones
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub New(ByRef _ParentResult As Result, ByVal tempPath As String)
        'Uso esta sobrecarga para version automatica, ya que se usa el path del archivo
        'Local en ves de la del servidor
        Me.New()
        notifyByMail = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.NotifyOptions, _ParentResult.DocType.ID)
        rdbAttachDocument.Visible = notifyByMail
        ParentResult = _ParentResult
        _tempPath = tempPath

        Try
            lblDocName.Text = _ParentResult.Name & "(" & _ParentResult.OriginalName & ")"
            lblVersionNum.Text = Results_Business.GetNewVersionID(_ParentResult.RootDocumentId, _ParentResult.DocTypeId, _ParentResult.ID).ToString
            lblParentVersionNum.Text = _ParentResult.VersionNumber

            'Me.Label1.Text = _ParentResult.Name & ControlChars.NewLine & _ParentResult.OriginalName & ControlChars.NewLine & "Fecha de Creacion: " & _ParentResult.CreateDate & ControlChars.NewLine
            'Me.Label1.Text &= "Fecha de Modificacion: " & _ParentResult.EditDate & ControlChars.NewLine
            ''todo: mostrar mas info de versiones
        Catch
        End Try
    End Sub

#End Region

#Region "Eventos"
    Private Sub frmVersionComment_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ChangeFormStyle()
    End Sub
    Private Sub btnNuevaVersion_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnNuevaVersion.Click

        Dim Sended As Boolean = False

        Try
            If String.IsNullOrEmpty(_tempPath) Then
                _Result = Results_Business.InsertNewVersionNoComment(ParentResult)
            Else
                _Result = Results_Business.InsertNewVersionNoComment(ParentResult, _tempPath)
            End If

            Results_Business.SaveVersionComment(_Result.ID, Comment)
            DialogResult = DialogResult.OK

            If chkNotificar.Checked Then
                Select Case notifyByMail
                    Case True
                        Sended = SendNewVersionNotifyByMail(_Result.ID, _Result.DocType.ID, _Result.Name)
                    Case False
                        Sended = SendNewVersionNotifyByInternalMsj()
                End Select

                If Sended = False Then
                    MessageBox.Show("La nueva version se creo correctamente, pero hubo un problema al notificar al usuario", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("No se ha podido Notificar la Nueva Versión.", "Zamba Software", MessageBoxButtons.OKCancel)
            DialogResult = DialogResult.Cancel
        End Try
        Close()

    End Sub
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub
    Private Sub btnUsuarios_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnUsuarios.Click
        Dim newFrmSelectUsers As frmSelectUsers = New frmSelectUsers(GroupToNotifyTypes.NotifyOnly, ParentResult.ID, ParentResult.DocType.ID)
        newFrmSelectUsers.StartPosition = FormStartPosition.CenterParent
        newFrmSelectUsers.ShowDialog()
    End Sub
    Private Sub chkNotificar_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkNotificar.CheckedChanged
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
                msgBody.Append(txtComentario.Text)
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
            msgBody.Append(txtComentario.Text)
            msgBody.Append(Chr(13))
            msgBody.Append(Chr(13))


            'valida que el usuario emisor del mail tenga configurado su mail
            If String.Compare(Membership.MembershipHelper.CurrentUser.eMail.Mail, String.Empty) <> 0 Then

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
                    If validMails.Count = mails.Count OrElse MessageBox.Show("Hay usuarios sin cuentas de mail definidas, ¿Desea continuar el envio?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                        For Each mail As String In externalMails
                            validMails.Add(mail)
                        Next
                        Select Case rdbAttachNothing.Checked
                            Case True
                                'If Not Message_Factory.SendMail(validMails, String.Empty, String.Empty, "Notificación de Nueva Version", msgBody.ToString(), False) Then
                                Try
                                    MessagesBusiness.SendMail(validMails.ToString, String.Empty, String.Empty, "Notificación de Nueva Version", msgBody.ToString(), False, Nothing)

                                    MessageBox.Show("El E-Mail se envio correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Return True
                                Catch ex As Exception
                                    MessageBox.Show("El mensaje no se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Return False

                                End Try


                            Case False
                                Select Case rdbAttachLink.Checked
                                    Case True
                                        link = Results_Business.GetHtmlLinkFromResult(_Result.DocType.ID, _Result.ID, _Result.Name)
                                        msgBody.Append("Link de acceso a la nueva versión: ")
                                        msgBody.Append(Chr(13))
                                        msgBody.Append(link)
                                        msgBody.Append(Chr(13))

                                        Try
                                            MessagesBusiness.SendMail(validMails.ToString, String.Empty, String.Empty, "Notificación de Nueva Version", msgBody.ToString(), True, Nothing)
                                            MessageBox.Show("El E-Mail se envio correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Return True
                                        Catch ex As Exception
                                            MessageBox.Show("El mensaje no se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Return False
                                        End Try


                                    Case False 'ADJUNTA ARCHIVO

                                        Dim ListAttach As New Generic.List(Of String)
                                        ListAttach.Add(_Result.FullPath)
                                        Try
                                            MessagesBusiness.SendMail(validMails.ToString, String.Empty, String.Empty, "Notificación de Nueva Version", msgBody.ToString(), False, ListAttach)
                                            MessageBox.Show("El E-Mail se envio correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Return True
                                        Catch ex As Exception
                                            MessageBox.Show("El mensaje no se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Return False
                                        End Try
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
        Select Case chkNotificar.Checked
            Case False
                gbNotificacion.Enabled = False
            Case True
                gbNotificacion.Enabled = True
        End Select
    End Sub
#End Region
End Class
