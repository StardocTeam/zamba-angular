Imports Zamba.Tools
Imports Microsoft.Win32
Imports Zamba.AppBlock


Public Class DBConfig
    Inherits ZForm


    Private Sub InitializeSystem()
        Zamba.Servers.Server.ConInitialized = True
        Dim server As New Server(Server.currentfile)
        server.dispose()
    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        Catch
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label3 As ZSubTitleLabel
    Friend WithEvents Btn_Aceptar As ZButton
    Friend WithEvents Label2 As ZSubTitleLabel
    Friend WithEvents Label1 As ZSubTitleLabel
    Friend WithEvents TxtDB As TextBox
    Friend WithEvents Label4 As ZSubTitleLabel
    Friend WithEvents Label5 As ZSubTitleLabel
    Friend WithEvents TxtPassword As TextBox
    Friend WithEvents txtUser As TextBox
    Friend WithEvents Label6 As ZSubTitleLabel
    Friend WithEvents CboServer As ComboBox
    Friend WithEvents CboType As ComboBox
    Friend WithEvents BtnTest As ZButton
    Friend WithEvents chkAuthentication As System.Windows.Forms.CheckBox
    Friend WithEvents UcSelectConnection1 As Zamba.Servers.UcSelectConnection
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DBConfig))
        Label3 = New Zamba.AppBlock.ZSubTitleLabel()
        Btn_Aceptar = New ZButton()
        Label2 = New Zamba.AppBlock.ZSubTitleLabel()
        Label1 = New Zamba.AppBlock.ZSubTitleLabel()
        TxtDB = New TextBox()
        Label4 = New Zamba.AppBlock.ZSubTitleLabel()
        Label5 = New Zamba.AppBlock.ZSubTitleLabel()
        TxtPassword = New TextBox()
        txtUser = New TextBox()
        Label6 = New Zamba.AppBlock.ZSubTitleLabel()
        CboServer = New ComboBox()
        CboType = New ComboBox()
        BtnTest = New ZButton()
        PictureBox1 = New System.Windows.Forms.PictureBox()
        chkAuthentication = New System.Windows.Forms.CheckBox()
        UcSelectConnection1 = New Zamba.Servers.UcSelectConnection()
        CType(PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label3.Font = New Font("Arial", 8.75!)
        Label3.ForeColor = System.Drawing.Color.White
        Label3.Location = New System.Drawing.Point(299, 29)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(345, 35)
        Label3.TabIndex = 21
        Label3.Text = "Selección de Base de Datos"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        '
        'Btn_Aceptar
        '
        Btn_Aceptar.DialogResult = System.Windows.Forms.DialogResult.None
        Btn_Aceptar.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Btn_Aceptar.Location = New System.Drawing.Point(411, 349)
        Btn_Aceptar.Name = "Btn_Aceptar"
        Btn_Aceptar.Size = New System.Drawing.Size(233, 26)
        Btn_Aceptar.TabIndex = 6
        Btn_Aceptar.Text = "Guardar"
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label2.Font = New Font("Arial", 8.75!)
        Label2.ForeColor = System.Drawing.Color.White
        Label2.ImageAlign = ContentAlignment.MiddleLeft
        Label2.Location = New System.Drawing.Point(299, 159)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(104, 23)
        Label2.TabIndex = 17
        Label2.Text = "Base de Datos"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label1.Font = New Font("Arial", 8.75!)
        Label1.ForeColor = System.Drawing.Color.White
        Label1.ImageAlign = ContentAlignment.MiddleLeft
        Label1.Location = New System.Drawing.Point(299, 124)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(104, 26)
        Label1.TabIndex = 16
        Label1.Text = "Servidor"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        '
        'TxtDB
        '
        TxtDB.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        TxtDB.Location = New System.Drawing.Point(411, 159)
        TxtDB.Name = "TxtDB"
        TxtDB.Size = New System.Drawing.Size(233, 23)
        TxtDB.TabIndex = 2
        '
        'Label4
        '
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label4.Font = New Font("Arial", 8.75!)
        Label4.ForeColor = System.Drawing.Color.White
        Label4.ImageAlign = ContentAlignment.MiddleLeft
        Label4.Location = New System.Drawing.Point(299, 228)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(104, 25)
        Label4.TabIndex = 25
        Label4.Text = "Clave"
        Label4.TextAlign = ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label5.Font = New Font("Arial", 8.75!)
        Label5.ForeColor = System.Drawing.Color.White
        Label5.ImageAlign = ContentAlignment.MiddleLeft
        Label5.Location = New System.Drawing.Point(299, 193)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(104, 26)
        Label5.TabIndex = 24
        Label5.Text = "Usuario"
        Label5.TextAlign = ContentAlignment.MiddleCenter
        '
        'TxtPassword
        '
        TxtPassword.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        TxtPassword.Location = New System.Drawing.Point(411, 228)
        TxtPassword.Name = "TxtPassword"
        TxtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        TxtPassword.Size = New System.Drawing.Size(233, 23)
        TxtPassword.TabIndex = 4
        '
        'txtUser
        '
        txtUser.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtUser.Location = New System.Drawing.Point(411, 193)
        txtUser.Name = "txtUser"
        txtUser.Size = New System.Drawing.Size(233, 23)
        txtUser.TabIndex = 3
        '
        'Label6
        '
        Label6.BackColor = System.Drawing.Color.Transparent
        Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label6.Font = New Font("Arial", 8.75!)
        Label6.ForeColor = System.Drawing.Color.White
        Label6.ImageAlign = ContentAlignment.MiddleLeft
        Label6.Location = New System.Drawing.Point(299, 90)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(104, 23)
        Label6.TabIndex = 26
        Label6.Text = "Tipo"
        Label6.TextAlign = ContentAlignment.MiddleCenter
        '
        'CboServer
        '
        CboServer.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        CboServer.Location = New System.Drawing.Point(411, 124)
        CboServer.Name = "CboServer"
        CboServer.Size = New System.Drawing.Size(233, 24)
        CboServer.TabIndex = 1
        '
        'CboType
        '
        CboType.DropDownStyle = ComboBoxStyle.DropDownList
        CboType.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        CboType.Location = New System.Drawing.Point(411, 90)
        CboType.Name = "CboType"
        CboType.Size = New System.Drawing.Size(233, 24)
        CboType.TabIndex = 0
        '
        'BtnTest
        '
        BtnTest.DialogResult = System.Windows.Forms.DialogResult.None
        BtnTest.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        BtnTest.Location = New System.Drawing.Point(411, 306)
        BtnTest.Name = "BtnTest"
        BtnTest.Size = New System.Drawing.Size(233, 26)
        BtnTest.TabIndex = 5
        BtnTest.Text = "Testear Conexión"
        '
        'PictureBox1
        '
        PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        PictureBox1.Location = New System.Drawing.Point(16, 17)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New System.Drawing.Size(168, 87)
        PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 30
        PictureBox1.TabStop = False
        '
        'chkAuthentication
        '
        chkAuthentication.AutoSize = True
        chkAuthentication.ForeColor = System.Drawing.Color.White
        chkAuthentication.Location = New System.Drawing.Point(411, 268)
        chkAuthentication.Name = "chkAuthentication"
        chkAuthentication.Size = New System.Drawing.Size(187, 17)
        chkAuthentication.TabIndex = 31
        chkAuthentication.Text = "Utilizar Autenticación de Windows"
        chkAuthentication.UseVisualStyleBackColor = True
        '
        'UcSelectConnection1
        '
        UcSelectConnection1.Location = New System.Drawing.Point(16, 306)
        UcSelectConnection1.Name = "UcSelectConnection1"
        UcSelectConnection1.Size = New System.Drawing.Size(387, 69)
        UcSelectConnection1.TabIndex = 32
        '
        'DBConfig
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        BackColor = System.Drawing.Color.Navy
        ClientSize = New System.Drawing.Size(682, 426)
        Controls.Add(UcSelectConnection1)
        Controls.Add(chkAuthentication)
        Controls.Add(PictureBox1)
        Controls.Add(BtnTest)
        Controls.Add(CboType)
        Controls.Add(CboServer)
        Controls.Add(Label6)
        Controls.Add(Label4)
        Controls.Add(Label5)
        Controls.Add(TxtPassword)
        Controls.Add(txtUser)
        Controls.Add(TxtDB)
        Controls.Add(Label3)
        Controls.Add(Btn_Aceptar)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Name = "DBConfig"
        Text = "Zamba Software - Configuración de Base de Datos"
        CType(PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region


    ReadOnly Property AppConfig() As ApplicationConfig
        Get
            Return Server.AppConfig
        End Get
    End Property


    Private Sub DBConfig_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

        AddHandler UcSelectConnection1.ConnectionChanged, AddressOf ConnectionChanged

        Btn_Aceptar.Enabled = False


        CboType.Items.Add(DBTYPES.MSSQLServer7Up)
        CboType.Items.Add(DBTYPES.MSSQLServer)
        CboType.Items.Add(DBTYPES.Oracle)
        CboType.Items.Add(DBTYPES.Oracle9)
        CboType.Items.Add(DBTYPES.SyBase)
        CboType.Items.Add(DBTYPES.OracleClient)
        CboType.Items.Add(DBTYPES.ODBC)
        CboType.Items.Add(DBTYPES.MSSQLExpress)
        CboType.Items.Add(DBTYPES.OracleDirect)
        CboType.Items.Add(DBTYPES.OracleManaged)
        CboType.Items.Add(DBTYPES.OracleODP)



        Try
            Try
                GetActualConfig()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Catch ex As Exception
            MessageBox.Show("No existe una configuración, por favor ingrese los datos y presione Guardar Configuración", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica los campos para establecer la conexion a la base de datos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function CheckFields() As Boolean
        Dim Flag As Boolean = True
        Try
            If chkAuthentication.Checked Then
                If CboServer.Text.Trim = "" Then
                    MessageBox.Show("Ingrese el nombre del Servidor de Datos")
                    Flag = False
                End If
                If TxtDB.Text.Trim = "" Then
                    MessageBox.Show("Ingrese el nombre de la base de datos")
                    Flag = False
                End If
            Else
                If TxtDB.Text.Trim = "" Then
                    MessageBox.Show("Ingrese el nombre de la base de datos")
                    Flag = False
                End If
                If txtUser.Text.Trim = "" Then
                    MessageBox.Show("Ingrese el nombre de usuario")
                    Flag = False
                End If
                If TxtPassword.Text.Trim = "" Then
                    MessageBox.Show("Ingrese la contraseña ")
                    Flag = False
                End If

                If CboType.Text.Trim = "" Then
                    MessageBox.Show("Ingrese el tipo de Servidor")
                    Flag = False
                End If
                If CboServer.Text.Trim = "" Then
                    MessageBox.Show("Ingrese el nombre del Servidor de Datos")
                    Flag = False
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error al chequear la configuración. " & ex.ToString, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
        If Flag = False Then
            Return False
        Else
            Return True
        End If
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Realiza un intento de conexión
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub Test()
        If CheckFields() = True Then
            Try
                Dim TempAppConfig As New Zamba.Tools.ApplicationConfig(Nothing)

                TempAppConfig.SERVER = CboServer.Text
                TempAppConfig.WIN_AUTHENTICATION = chkAuthentication.Checked
                TempAppConfig.SERVERTYPE = CboType.SelectedIndex
                TempAppConfig.DB = TxtDB.Text
                TempAppConfig.USER = txtUser.Text
                TempAppConfig.PASSWORD = TxtPassword.Text

                Dim server As New Server(Server.currentfile)
                server.MakeConnection(TempAppConfig.SERVERTYPE, TempAppConfig.SERVER, TempAppConfig.DB, TempAppConfig.USER, TempAppConfig.PASSWORD)
                If Server.isSQLServer Then
                    Dim d As Integer = Server.Con.ExecuteScalar(CommandType.Text, "select count(1) from sysobjects")
                Else
                    Dim d As Integer = Server.Con.ExecuteScalar(CommandType.Text, "select count(1) from user_objects")
                End If
                server.dispose()

                MessageBox.Show("La conexión ha sido correcta")
                Btn_Aceptar.Enabled = True
            Catch ex As Exception
                Btn_Aceptar.Enabled = False
                MessageBox.Show("La conexión ha producido un error: " & ex.ToString, "Zamba - Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            Btn_Aceptar.Enabled = False
        End If
    End Sub


    Private Shared Sub UpdateRegistry()
        Dim val As String = Application.StartupPath & "\com"
        Dim key As String = "Software\\" & "Westbrook Technologies\\Fortis\\InstallInfo"

        Dim reg As Microsoft.Win32.RegistryKey = Registry.CurrentUser.CreateSubKey(key)
        reg.SetValue("path", val)

        'Microsoft.Win32.Registry.CurrentUser.SetValue(, Application.StartupPath.Replace("\","\\" & "\\com\")
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Registra la instalación de Zamba en la base de datos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function RegistrarEstacion() As Int32
        Dim ver As String = ReflectionLibrary.GetVersion("ZConfigCtls.exe")
        ver = ver.Replace(".", "")
        Try
            ver = ver.Substring(0, 4)
        Catch ex As ArgumentOutOfRangeException
        Catch ex As IndexOutOfRangeException
        Catch ex As Exception
        End Try

        'Dim parValues() = {Environment.UserName, Environment.MachineName, ver}
        Dim Ds As DataSet
        Try
            If Server.isOracle Then

                ''Dim parNames() As String = {"WinUser", "Pc", "Ver", "io_cursor"}
                'Dim parTypes() As Object = {7, 7, 7, 5}
                Dim parValuesOra() As Object = {Environment.UserName, Environment.MachineName, ver, 2}
                'Ds = Server.Con(True).ExecuteDataset("SELECT_ESTREG_PKG.SELECT_ESTREG",  parValuesOra)
                Ds = Server.Con(True).ExecuteDataset("zsp_security_100.InsertStation", parValuesOra)
                Return Ds.Tables(0).Rows(0).Item(0)
            Else
                Dim parValuesSql() As String = {Environment.UserName, Environment.MachineName, ver}
                'Ds = Server.Con(True).ExecuteDataset("SELECT_ESTREG", parValuesSql)
                Ds = Server.Con(True).ExecuteDataset("zsp_security_100_InsertStation", parValuesSql)
                Return Ds.Tables(0).Rows(0).Item(0)
            End If
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error al registrar la estación " & ex.ToString, "ZAMBA SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function



    Private Sub SetConfig(file As String)
        Dim flag As Boolean = CheckFields()
        If flag = True Then
            Try
                AppConfig.SERVER = CboServer.Text
                AppConfig.WIN_AUTHENTICATION = chkAuthentication.Checked
                AppConfig.SERVERTYPE = CboType.SelectedIndex
                AppConfig.DB = TxtDB.Text
                AppConfig.USER = txtUser.Text
                AppConfig.PASSWORD = TxtPassword.Text

                AppConfig.Write(file)
            Catch ex As Exception
                MessageBox.Show("No se pudo escribir en el archivo de configuración", "Zamba Error N 103", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Debe llenar todos los campos", "ZAMBA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Lee el archivo App.ini y completa los valores de la ultima conexion configurada
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub GetActualConfig()
        Try
            CboServer.Text = CStr(AppConfig.SERVER)
            CboType.SelectedIndex = CStr(AppConfig.SERVERTYPE)
            TxtDB.Text = AppConfig.DB
            txtUser.Text = AppConfig.USER
            TxtPassword.Text = AppConfig.PASSWORD
            chkAuthentication.Checked = AppConfig.WIN_AUTHENTICATION
            BtnTest.Enabled = True
            Btn_Aceptar.Enabled = False
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub




    Private Sub BtnTest_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnTest.Click
        Try
            Test()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub Btn_Aceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Btn_Aceptar.Click

        Try
            Dim FB As New SaveFileDialog()
            FB.AddExtension = True
            FB.CheckPathExists = True
            FB.FileName = currentFile
            FB.InitialDirectory =
                FB.OverwritePrompt = False
            FB.ShowDialog()
            SetConfig(FB.FileName)

            MessageBox.Show("Configuración guardada correctamente", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error al guardar la configuración " & ex.ToString, "Zamba SoftWare", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub chkAuthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkAuthentication.CheckedChanged
        If chkAuthentication.Checked Then
            TxtPassword.Enabled = False
            txtUser.Enabled = False
        Else
            TxtPassword.Enabled = True
            txtUser.Enabled = True
        End If

    End Sub

    Private currentFile As String
    Private Sub ConnectionChanged(File As String)
        currentFile = File
        'Cache.CacheBusiness.ClearAllCache()
        Server.AppConfig.Read(File)
        'Zamba.Core.DBBusiness.InitializeDB()
        GetActualConfig()
    End Sub

End Class
