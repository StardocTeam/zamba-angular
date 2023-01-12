Imports System.Net
Imports System.Net.Mail

Module Module1

    Sub Main()
        Console.WriteLine("Test SMTP Autenticado")
        '        Console.Read()
        Dim mail As New MailMessage("andres.nagel@stardoc.com.ar", "andres.nagel@stardoc.com.ar")
        mail.Body = "Mensaje de Prueba."
        mail.Subject = "Mensaje de Prueba."

        EnviarMail(mail, "andres.nagel@stardoc.com.ar", "nagel", 25, "smtp.iplannetworks.net")

        Console.WriteLine("Test Finalizado")
        Console.Read()
    End Sub
    Public Sub EnviarMail(ByVal mail As Net.Mail.MailMessage, ByVal user As String, ByVal password As String, ByVal port As Integer, ByVal server As String)
        Dim cliente As New SmtpClient(server, port)
        cliente.Credentials = New NetworkCredential(user, password)
        cliente.DeliveryMethod = SmtpDeliveryMethod.Network
        cliente.Send(mail)
    End Sub
End Module
