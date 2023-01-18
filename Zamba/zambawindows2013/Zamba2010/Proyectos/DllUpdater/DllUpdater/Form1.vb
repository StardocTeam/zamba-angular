Imports System.IO
Imports System.Net.Mail


Public Class Form1
    Private operationsMade As Generic.List(Of String)
    Private Notify As Boolean
    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.Height = 143
        operationsMade = New Generic.List(Of String)

        Shell("cmd.exe /c start \\www.stardocteam.com.ar/Sites/Stardoc/Development/", AppWinStyle.MinimizedNoFocus, True)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Notify = False
            operationsMade.Clear()
            ProgressBar1.Maximum = 0
            Dim path As String = "E:\Compilado"
            Try
                If Not Directory.Exists(path) Then
                    path = "D:\Compilado"
                End If
            Catch
                path = "D:\Compilado"
            End Try
            Try
                ProgressBar1.Maximum = Directory.GetFiles("\\www.stardocteam.com.ar\Sites\Stardoc\DLL Externas").Count
            Catch ex As Exception
                ProgressBar1.Maximum = Directory.GetFiles(Chr(34) & "\\www.stardocteam.com.ar\Sites\Stardoc\DLL Externas" & Chr(34)).Count
            End Try
            Dim localFile As IO.FileInfo = Nothing
            Dim remoteFile As IO.FileInfo = Nothing
            Dim filename As String = String.Empty
            For Each _File As String In Directory.GetFiles("\\www.stardocteam.com.ar\Sites\Stardoc\DLL Externas")
                Try
                    filename = _File.Remove(0, _File.LastIndexOf("\"))
                    operationsMade.Add("Procesando " & filename)
                    localFile = New IO.FileInfo(path & filename)
                    remoteFile = New IO.FileInfo(_File)
                    If localFile.Exists Then
                        'SE COMPARA POR LA FECHA DE CREACION DE AMBOS ARCHIVOS
                        If localFile.LastWriteTime < remoteFile.LastWriteTime Then
                            File.Copy(_File, path & filename, True)
                            operationsMade.Add("Procesado " & filename)
                        Else
                            If Not localFile.LastWriteTime = remoteFile.LastWriteTime Then
                                'LA VERSION LOCAL ES MAS ACTUALIZADA QUE LA DEL SERVIDOR
                                Dim frm As New frmFileUpdateOptions(filename)
                                frm.ShowDialog()

                                Select Case frm.DialogResult
                                    Case Windows.Forms.DialogResult.Yes
                                        'SOBREESCRIBE
                                        File.Copy(_File, path & filename, True)
                                        operationsMade.Add("Procesado " & filename)
                                    Case Windows.Forms.DialogResult.OK
                                        'REEMPLAZA ARCHIVO EN SERVIDOR
                                        localFile.CopyTo(_File, True)
                                        If Not Notify Then Notify = True
                                        operationsMade.Add("Reemplazando en Servidor  " & filename)

                                    Case Windows.Forms.DialogResult.Cancel
                                        'CANCELA
                                        operationsMade.Add("Cancelado " & filename)
                                End Select

                            Else
                                'EL ARCHIVO LOCAL ES IGUAL AL DEL SERVIDOR
                                operationsMade.Add("No es necesario actualizar " & filename)
                            End If
                        End If
                    Else
                        'NO EXISTE EL ARCHIVO EN LA PC
                        File.Copy(_File, path & filename, True)
                        operationsMade.Add("Procesado " & filename)
                    End If

                Catch
                    operationsMade.Add("Error Procesando " & filename)
                Finally
                    ProgressBar1.Value = ProgressBar1.Value + 1
                    'LIBERA EL ARCHIVO
                    localFile = Nothing
                    remoteFile = Nothing
                    GC.Collect()
                End Try
            Next
            If Notify Then
                NotifyServerUpdate()
            End If
            MessageBox.Show("Actualizacion finalizada")
        Catch ex As Exception
            MessageBox.Show(ex.ToString() & "Path: \\www.stardocteam.com.ar\Sites\Stardoc\DLL Externas")
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        ListBox1.Items.Clear()
        For Each Item As String In operationsMade
            ListBox1.Items.Add(Item)
        Next
        If Me.Height = 143 Then
            Me.Height = 395
        Else
            Me.Height = 143
        End If
    End Sub

    Private Sub NotifyServerUpdate()


        Dim EMsg As New System.Net.Mail.MailMessage
        EMsg.From = New MailAddress("diego.albarellos@stardoc.com.ar", Environment.MachineName)
        EMsg.To.Add("sistemas@stardoc.com.ar")
        EMsg.Subject = "Se actualizaron Dlls"
        EMsg.Body = "Se actualizaron dlls " & Environment.NewLine & "PCNAME = " & Environment.MachineName & " TIME = " & Date.Now
        EMsg.IsBodyHtml = True

        'send the message 

        Dim smtp As New SmtpClient("smtp.iplannetworks.net")
        smtp.Credentials = New Net.NetworkCredential("diego.albarellos@stardoc.com.ar", "albarellos")
        Try
            smtp.Send(EMsg)
        Catch ex As Exception
            MessageBox.Show("No se puedo enviar el mensaje", "Envío de mensajes", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ex.Data.Clear()
        Finally
            smtp = Nothing
            GC.Collect()
        End Try
    End Sub

End Class
