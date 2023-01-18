Imports Zamba.Tools
Imports Zamba.Core
'Imports Zamba.Impersonate

Public Class FrmImpersonateCfg
    Private Shared key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Private Shared iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        If String.IsNullOrEmpty(txtDom.Text) AndAlso String.IsNullOrEmpty(txtUsr.Text) AndAlso String.IsNullOrEmpty(txtPass.Text) Then
            MessageBox.Show("Se deben completar todos los campos")
            Exit Sub
        End If

        Dim zImper As New ZImpersonalize
        Try
            If zImper.impersonateValidUser(txtUsr.Text, txtDom.Text, txtPass.Text) Then
                MessageBox.Show("Prueba correcta")
                zImper.undoImpersonation()
            Else
                MessageBox.Show("Fallo la Prueba")
            End If

            SaveValues()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.ToString())
        Finally
            zImper = Nothing
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim zImper As New ZImpersonalize
        Try
            If String.IsNullOrEmpty(txtDom.Text) AndAlso String.IsNullOrEmpty(txtUsr.Text) AndAlso String.IsNullOrEmpty(txtPass.Text) Then
                MessageBox.Show("Se deben completar todos los campos")
                Exit Sub
            End If

            If zImper.impersonateValidUser(txtUsr.Text, txtDom.Text, txtPass.Text) Then
                zImper.undoImpersonation()

                SaveValues()

                MessageBox.Show("Configuracion guardada")

                Me.Close()
            Else
                MessageBox.Show("Fallo la configuracion")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.ToString())
        Finally
            zImper = Nothing
        End Try
    End Sub

    Private Sub SaveValues()
        ZOptBusiness.InsertUpdateValue("UpdaterCredentials_usr", txtUsr.Text)
        ZOptBusiness.InsertUpdateValue("UpdaterCredentials_pss", Encryption.EncryptString(txtPass.Text, key, iv))
        ZOptBusiness.InsertUpdateValue("UpdaterCredentials_dom", txtDom.Text)
        'ZOptBusiness.InsertUpdateValue("UpdaterCredentials_typ", cboSessionType.SelectedItem)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            txtUsr.Text = ZOptBusiness.GetValue("UpdaterCredentials_usr")
            txtDom.Text = ZOptBusiness.GetValue("UpdaterCredentials_dom")
            txtPass.Text = Encryption.DecryptString(ZOptBusiness.GetValue("UpdaterCredentials_pss"), key, iv)

            'cboSessionType.DataSource = [Enum].GetNames(GetType(ZImpersonalizeAdvance.LogonType))
            'Dim sessionValue As String = ZOptBusiness.GetValue("UpdaterCredentials_typ")
            'If String.IsNullOrEmpty(sessionValue) Then
            '    cboSessionType.SelectedItem = ZImpersonalizeAdvance.LogonType.LOGON32_LOGON_INTERACTIVE.ToString
            'Else
            '    cboSessionType.SelectedItem = sessionValue
            'End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ha ocurrido un error al cargar los valores guardados", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If String.IsNullOrEmpty(txtUsr.Text) Then txtUsr.Text = Environment.UserName
            If String.IsNullOrEmpty(txtDom.Text) Then txtDom.Text = Environment.UserDomainName

        End Try
    End Sub
End Class
