Imports Zamba.Core

Public Class NotifyPublish
    Inherits Zamba.AppBlock.ZForm

    Private _mailsPara As Generic.List(Of String)
    Private _link As String
    Private _path As String
    Private _users As Generic.List(Of User)
    Dim notifybymail As Boolean

    Public Sub New(ByVal link As String, ByVal mails As Generic.List(Of String), ByVal path As String)
        'Diego: Uso esta sobrecarga para hacer un envio de Mail
        InitializeComponent()
        _mailsPara = mails
        _link = link
        _path = path
    End Sub

    Public Sub New(ByVal link As String, ByVal users As Generic.List(Of User))
        'Diego: Uso esta sobrecarga para hacer un envio de Mensaje Interno
        InitializeComponent()
        _users = users
        _link = link
        GroupBox2.Visible = False
    End Sub

    Private Sub BtnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnAceptar.Click
        If IsNothing(_users) Then
            SendMail()
        Else
            SendInternalMessage()
        End If
    End Sub

    Private Sub SendInternalMessage()
        Dim body As New System.Text.StringBuilder()
        If RdbIncluirResumen.Checked Then
            body.Append(TxtBody.Text)
            body.AppendLine()
            body.Append("Acceso al archivo: ")
            body.Append(_link)
            InternalMessage.SendInternalMessage("Notificación de Publicación", Now.Date, body.ToString, _users, MessageType.MailCCO)
            MessageBox.Show("Mensaje interno Enviado con exito", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        Else ' no se incluye resumen
            body.Append("Acceso al archivo: ")
            body.Append(_link)
            InternalMessage.SendInternalMessage("Notificación de Publicación", Now.Date, String.Empty, _users, MessageType.MailCCO)
            MessageBox.Show("Mensaje interno Enviado con exito", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        End If
    End Sub

    Private Sub SendMail()
        Dim body As New System.Text.StringBuilder()
        Dim AttachList As New Generic.List(Of String)
        AttachList.Add(_path)

        If RdbIncluirResumen.Checked Then
            body.Append(TxtBody.Text)
            body.AppendLine()

            If RdbAgregarLink.Checked Then
                body.Append("Acceso al archivo: ")
                body.Append(_link)
                Try

                    MessagesBusiness.SendMail(_mailsPara.ToString, Nothing, Nothing, "Notificación de publicación", body.ToString(), True, Nothing)
                    MessageBox.Show("Envio realizado con exito", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                Catch ex As Exception
                    MessageBox.Show("No se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Close()
                End Try
              
          
            Else ' se adjunta el archivo

                Try
                    MessagesBusiness.SendMail(_mailsPara.ToString, Nothing, Nothing, "Notificación de publicación", body.ToString(), True, AttachList)
                    MessageBox.Show("Envio realizado con exito", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                Catch ex As Exception
                    MessageBox.Show("No se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Close()
                End Try
               
              

        End If
        Else ' no se incluye resumen
        If RdbAgregarLink.Checked Then
            body.Append("Acceso al archivo: ")
            body.Append(_link)

                Try
                    MessagesBusiness.SendMail(_mailsPara.ToString, String.Empty, String.Empty, "Notificación de publicación", body.ToString(), False)
                    MessageBox.Show("Envio realizado con exito", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                Catch ex As Exception
                    MessageBox.Show("No se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Close()
                End Try
          
          
        Else ' se adjunta el archivo
                Try
                    MessagesBusiness.SendMail(_mailsPara.ToString, Nothing, Nothing, "Notificación de publicación", String.Empty, False, AttachList)
                    MessageBox.Show("Envio realizado con exito", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                Catch ex As Exception
                    MessageBox.Show("No se pudo enviar correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Close()
                End Try

        End If
        End If
    End Sub

    Private Sub RdbNoIncluirResumen_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles RdbNoIncluirResumen.CheckedChanged
        TxtBody.Visible = False
        GroupBox2.Top = 53
        BtnAceptar.Top = 108
        Height = 172
    End Sub

    Private Sub RdbIncluirResumen_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles RdbIncluirResumen.CheckedChanged
        TxtBody.Visible = True
        GroupBox2.Top = 190
        BtnAceptar.Top = 245
        Height = 309
    End Sub
End Class