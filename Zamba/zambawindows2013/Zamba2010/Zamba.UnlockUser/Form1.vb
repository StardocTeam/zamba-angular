Imports Zamba.Servers

Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtdbuser As System.Windows.Forms.TextBox
    Friend WithEvents txtpass As System.Windows.Forms.TextBox
    Friend WithEvents btnaceptar As System.Windows.Forms.Button
    Friend WithEvents txtnewpass As System.Windows.Forms.TextBox
    Friend WithEvents txtnewpass2 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CboUser As System.Windows.Forms.ComboBox
    Friend WithEvents btnhabilitar As System.Windows.Forms.Button
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtdbuser = New System.Windows.Forms.TextBox
        Me.txtpass = New System.Windows.Forms.TextBox
        Me.btnaceptar = New System.Windows.Forms.Button
        Me.CboUser = New System.Windows.Forms.ComboBox
        Me.txtnewpass = New System.Windows.Forms.TextBox
        Me.txtnewpass2 = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnhabilitar = New System.Windows.Forms.Button
        Me.pic = New System.Windows.Forms.PictureBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(24, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(128, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Usuario de Base de Datos"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(24, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 32)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Contraseña"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtdbuser
        '
        Me.txtdbuser.Location = New System.Drawing.Point(160, 88)
        Me.txtdbuser.Name = "txtdbuser"
        Me.txtdbuser.Size = New System.Drawing.Size(304, 20)
        Me.txtdbuser.TabIndex = 2
        Me.txtdbuser.Text = ""
        '
        'txtpass
        '
        Me.txtpass.Location = New System.Drawing.Point(160, 128)
        Me.txtpass.Name = "txtpass"
        Me.txtpass.PasswordChar = Microsoft.VisualBasic.ChrW(35)
        Me.txtpass.Size = New System.Drawing.Size(304, 20)
        Me.txtpass.TabIndex = 3
        Me.txtpass.Text = ""
        '
        'btnaceptar
        '
        Me.btnaceptar.BackColor = System.Drawing.Color.SteelBlue
        Me.btnaceptar.ForeColor = System.Drawing.Color.White
        Me.btnaceptar.Location = New System.Drawing.Point(320, 160)
        Me.btnaceptar.Name = "btnaceptar"
        Me.btnaceptar.Size = New System.Drawing.Size(144, 23)
        Me.btnaceptar.TabIndex = 4
        Me.btnaceptar.Text = "Aceptar"
        '
        'CboUser
        '
        Me.CboUser.Location = New System.Drawing.Point(168, 200)
        Me.CboUser.Name = "CboUser"
        Me.CboUser.Size = New System.Drawing.Size(296, 21)
        Me.CboUser.TabIndex = 5
        Me.CboUser.Visible = False
        '
        'txtnewpass
        '
        Me.txtnewpass.Location = New System.Drawing.Point(168, 232)
        Me.txtnewpass.MaxLength = 10
        Me.txtnewpass.Name = "txtnewpass"
        Me.txtnewpass.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtnewpass.Size = New System.Drawing.Size(296, 20)
        Me.txtnewpass.TabIndex = 6
        Me.txtnewpass.Text = ""
        Me.txtnewpass.Visible = False
        '
        'txtnewpass2
        '
        Me.txtnewpass2.Location = New System.Drawing.Point(168, 264)
        Me.txtnewpass2.MaxLength = 10
        Me.txtnewpass2.Name = "txtnewpass2"
        Me.txtnewpass2.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtnewpass2.Size = New System.Drawing.Size(296, 20)
        Me.txtnewpass2.TabIndex = 7
        Me.txtnewpass2.Text = ""
        Me.txtnewpass2.Visible = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(24, 200)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(128, 23)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Usuario"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label3.Visible = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Location = New System.Drawing.Point(24, 232)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(128, 23)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Nueva contraseña"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label4.Visible = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Location = New System.Drawing.Point(24, 264)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(128, 23)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Repetir contraseña"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label5.Visible = False
        '
        'btnhabilitar
        '
        Me.btnhabilitar.BackColor = System.Drawing.Color.SteelBlue
        Me.btnhabilitar.ForeColor = System.Drawing.Color.White
        Me.btnhabilitar.Location = New System.Drawing.Point(168, 296)
        Me.btnhabilitar.Name = "btnhabilitar"
        Me.btnhabilitar.Size = New System.Drawing.Size(64, 23)
        Me.btnhabilitar.TabIndex = 11
        Me.btnhabilitar.Text = "Habilitar"
        Me.btnhabilitar.Visible = False
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(16, 8)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(152, 56)
        Me.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pic.TabIndex = 12
        Me.pic.TabStop = False
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(200, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(216, 48)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Desbloqueo de Usuarios"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.SteelBlue
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(248, 296)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(216, 23)
        Me.Button1.TabIndex = 14
        Me.Button1.Text = "Poner como Administrador de Usuarios"
        Me.Button1.Visible = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.SteelBlue
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(248, 328)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(216, 23)
        Me.Button2.TabIndex = 15
        Me.Button2.Text = "Quitar como Administrador de Usuarios"
        Me.Button2.Visible = False
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(608, 366)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.btnhabilitar)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtnewpass2)
        Me.Controls.Add(Me.txtnewpass)
        Me.Controls.Add(Me.txtpass)
        Me.Controls.Add(Me.txtdbuser)
        Me.Controls.Add(Me.CboUser)
        Me.Controls.Add(Me.btnaceptar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.ForeColor = System.Drawing.Color.Black
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "Zamba - Desbloquear Usuario"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim acr As New Zamba.Tools.ApplicationConfig

    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click
        Try
            Me.MaKeconnection()
        Catch
        End Try

        Try
            If txtpass.Text = acr.PASSWORD Then
                Habilitar()
            Else
                MessageBox.Show("La clave no coincide")
            End If
        Catch
        End Try
        acr.dispose()
    End Sub
    Private Sub MaKeconnection()
        Try
            Dim server As New server
            server.MakeConnection()
            server.dispose()
        Catch
        End Try
    End Sub
    Private Sub Habilitar()
        Try
            For Each c As Control In Me.Controls
                c.Visible = True
            Next
            Me.LoadUsers()
        Catch
        End Try
    End Sub
    Private Sub LoadUsers()
        Try
            Dim sql As String = "Select ID, NAME from USRTABLE order by Name"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Me.CboUser.DataSource = ds.Tables(0)
            Me.CboUser.DisplayMember = "NAME"
            Me.CboUser.ValueMember = "ID"
            Me.CboUser.Refresh()
        Catch
        End Try
    End Sub

    Private Sub btnhabilitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhabilitar.Click
        Try
            If txtnewpass.Text.Trim = txtnewpass2.Text.Trim Then
                Dim unlock As New unlock
                unlock.unlockuser(Me.CboUser.SelectedValue, txtnewpass.Text.Trim)
                unlock.Dispose()
            End If
            MessageBox.Show("El usuario fue desbloqueado", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.txtdbuser.Text = acr.USER
        Catch ex As Exception
            MessageBox.Show("Copie primero el app.ini y vuelva a ejecutar la aplicación", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, "insert into usr_rights(GROUPID,OBJID,RType,ADITIONAL) values(" & Me.CboUser.SelectedValue & ",5,5,-1)")
            MessageBox.Show("Se habilito al usuario como administrador de Usuarios, recuerde deshabilitarlo por este medio, luego de solucionado el inconveniete", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, "delete usr_rights where GROUPID = " & Me.CboUser.SelectedValue & " and OBJID = 5 and RType = 5 and ADITIONAL = -1")
            MessageBox.Show("Se Deshabilito al usuario como administrador de Usuarios", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
        End Try
    End Sub
End Class
