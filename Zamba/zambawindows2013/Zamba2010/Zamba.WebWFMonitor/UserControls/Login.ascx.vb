Imports Zamba.Data
Imports Zamba.Core

Partial Class Login
    Inherits System.Web.UI.UserControl

    Public Event Logueado()
    Dim Count As Integer
    Dim User As String
    Dim Pass As String
    Dim WF As Boolean
    Dim MyUser As User

    Private Sub Login_Fail(ByVal Usuario As String)
        Count += 1
        If Count = 3 Then
            If Zamba.Servers.Server.Con.ExecuteScalar(System.Data.CommandType.Text, "Select lockuser from ZSecOption") = "TRUE" Then
                SecurityOptions.LockUserPassword(Me.ID, Usuario)
            End If
            Zamba.Users.Factory.RightFactory.SaveAction(GetUserID(Usuario), IUser.ObjectTypes.Users, IUser.RightsType.InicioFallidoDeSesion, "PC:" & Environment.MachineName & ",Usuario_Windows:" & Environment.UserName, GetUserID(Usuario))
            Response.Redirect("about:blank")
        End If
    End Sub


    Private Shared Function GetUserID(ByVal name As String) As Int32
        Try
            Dim sql As String = "Select id from usrtable where name='" & name & "'"
            Return Zamba.Servers.Server.Con.ExecuteScalar(System.Data.CommandType.Text, sql)
        Catch
            Return 0
        End Try
	End Function


    Private Sub Login_Ok(ByVal Usuario As String)
        Try

            Dim WinUser, WinPc As String
            Try
                WinUser = Environment.UserName
            Catch ex As Exception
                RaiseError(ex)
                WinUser = "Desconocido"
            End Try
            Try
                WinPc = Environment.MachineName
            Catch ex As Exception
                RaiseError(ex)
                WinPc = "Desconocida"
            End Try
            'Validar licencia para WorkFlow, parametro opcional WF
            Me.WF = Me.RememberMe.Checked
            If Me.WF = True Then
                If Zamba.Users.Factory.RightFactory.GetUserRights(IUser.ObjectTypes.ModuleWorkFlow, IUser.RightsType.Use) = False Then
                    'No tiene permiso para acceder a WF
                    WF = False
                End If
            End If
            Zamba.Users.Factory.RightFactory.CurrentUser.WFLic = WF
            Zamba.Users.Factory.RightFactory.CurrentUser.ConnectionId = Ucm.NewConnection(Zamba.Users.Factory.RightFactory.CurrentUser.ID, WinUser, WinPc, UserPreferences.TimeOut, WF)
            Ucm.ConectionTime = Now.ToString("hh:mm:ss")
        Catch ex As Exception
            RaiseError(ex)
            Me.FailureText.Text = "Maximo de Licencias conectadas, contáctese con su proveedor para adquirir nuevas licencias."
            Zamba.Users.Factory.RightFactory.CurrentUser.ConnectionId = 0
        End Try
        Try
            UserPreferences.UserId = Zamba.Users.Factory.RightFactory.CurrentUser.ID
        Catch
        End Try
        If Zamba.Users.Factory.RightFactory.CurrentUser.ConnectionId <> 0 Then
            Session("Usuario") = Usuario
            Dim L As Generic.List(Of Usuario) = Application("Usuarios")
            If IsNothing(L) Then L = New Generic.List(Of Usuario)
            L.Add(New Usuario(Usuario))
            Application("Usuarios") = L

            RaiseEvent Logueado()
        Else
            Me.FailureText.Text = "Límite de Conexiones"
            Login_Fail(Usuario)
        End If

    End Sub



    Private Sub Validar(ByVal Usuario As String, ByVal Password As String)
        Try

            Zamba.Users.Factory.RightFactory.ValidateLogIn(Usuario, Password)

            If Zamba.Users.Factory.RightFactory.CurrentUser Is Nothing Then
                Login_Fail(Usuario)
            End If

            Me.MyUser = Zamba.Users.Factory.RightFactory.CurrentUser

            If Zamba.Users.Factory.RightFactory.CurrentUser.ID <> -1 AndAlso Zamba.Users.Factory.RightFactory.CurrentUser.ID <> 0 Then
                If SecurityOptions.ClaveVencida(Zamba.Users.Factory.RightFactory.CurrentUser.ID) = False Then
                    Login_Ok(Usuario)
                Else
                    Me.Panel1.Visible = False
                    Me.Panel2.Visible = True
                End If
            Else
                If Zamba.Users.Factory.RightFactory.CurrentUser.ID = -1 Then
                    Me.FailureText.Text = "El Usuario se encuentra bloqueado"
                Else
                    Me.FailureText.Text = "El Usuario o la clave ingresadas son incorréctos"
                End If
                Login_Fail(Usuario)
            End If


        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            Me.FailureText.Text = ex.Message
            Login_Fail(Usuario)
        End Try

    End Sub



    Public ReadOnly Property Usuario() As String
        Get
            Return Me.UserName.Text
        End Get
    End Property



    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginButton.Click
        If Me.UserName.Text = "" OrElse Me.Password.Text = "" Then Exit Sub
        Validar(Me.UserName.Text, Me.Password.Text)
	End Sub



    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        If Me.TextBox2.Text = Me.TextBox1.Text Then

            If SecurityOptions.IsValidPassword(Me.MyUser.Id, Usuario.Trim, Me.TextBox1.Text.Trim, True) Then
                Me.MyUser.Password = Me.TextBox1.Text.Trim
                Try
                    Zamba.Users.Factory.UserFactory.Update(Me.MyUser)
                    'Esta dentro del user.Update Zamba.Security.ZSecurityOptions.SecurityOptions.SavePassword(Me._usr.id, Me.Login_Password.Text)
                    'MessageBox.Show("La clave se cambió correctamente", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'RaiseEvent ChangePassword(True)
                    Me.Panel2.Visible = False
                    Me.Panel1.Visible = True
                Catch ex As Exception
                    RaiseError(ex)
                    Login_Fail(Usuario)
                    'MessageBox.Show("Ocurrio un error al intentar cambiar la clave. " & ex.ToString, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            End If
        End If

    End Sub


End Class
