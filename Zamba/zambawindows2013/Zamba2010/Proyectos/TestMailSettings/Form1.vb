Imports System.Net.Mail
Imports System.Net

Public Class FrmTestMailSettings



    Private Sub BtnTest_Click(sender As System.Object, e As System.EventArgs) Handles btnTest.Click

        Dim msg As New System.Net.Mail.MailMessage()
        msg.From = New MailAddress(txtMail.Text)
        msg.Body = "Este es un mensaje para notificar que el mail a sido configurado con exito."
        msg.Subject = "Prueba sobre la configuración de mail"
        msg.To.Add(txtMail.Text)
        msg.IsBodyHtml = False
        Try
            'Instanciamos el objeto smtp con el cual vamos a realizar el envio de mail
            Dim SMTP As New SmtpClient()
            'Cargamos el servidor y el puerto a utilizar para poder realizar el envio
            SMTP.Host = txtHost.Text
            SMTP.Port = txtPort.Text
            'Seteamos las opciones de credenciales y de Ssl
            SMTP.UseDefaultCredentials = chkUseDefaultCredentials.Checked
            SMTP.EnableSsl = chkEnableSsl.Checked
            If Not chkUseDefaultCredentials.Checked AndAlso
((Not String.IsNullOrEmpty(txtUsr.Text.Trim()) AndAlso Not String.IsNullOrEmpty(txtPass.Text.Trim())) OrElse Not chkBlankCredentials.Checked) Then
                SMTP.Credentials = New NetworkCredential(txtUsr.Text, txtPass.Text)
            End If

            SMTP.Send(msg)
            MessageBox.Show("mensaje enviado con exito, verifique el correo del mail ingresado", "alerta", MessageBoxButtons.OK)
            'Liberamos la memoria
            SMTP = Nothing
        Catch ex As System.Net.Sockets.SocketException
            MessageBox.Show(ex.ToString)
        Catch ex As Exception
            MessageBox.Show("Error al realizar la prueba, verifique su configuración", "alerta", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MessageBox.Show(ex.ToString)
        End Try
     
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


End Class
