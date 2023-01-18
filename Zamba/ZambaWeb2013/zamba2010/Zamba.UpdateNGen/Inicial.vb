Imports Microsoft.Win32
Imports System.Diagnostics.Trace
Imports Zamba.Impersonate

''' -----------------------------------------------------------------------------
''' Project	 : Zamba Cliente
''' Class	 : Inicial
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Formulario que muestra en pantalla el progreso de la actualización
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Delegate Sub DUpdateGuiInfo(ByVal info As String)

Public Class Inicial
    Inherits System.Windows.forms.Form

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
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Inicial))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(333, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(320, 45)
        Me.Label1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(333, 156)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(320, 82)
        Me.Label2.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Location = New System.Drawing.Point(0, 37)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(672, 327)
        Me.Panel1.TabIndex = 66
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(64, 168)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(226, 19)
        Me.Label3.TabIndex = 63
        Me.Label3.Text = "Software de Gestión Documental"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox2
        '
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(70, 193)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(216, 70)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 61
        Me.PictureBox2.TabStop = False
        '
        'Inicial
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(128, Byte))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(666, 401)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DockPadding.All = 1
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Inicial"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " ZAMBA SOFTWARE"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Atributos"
    Dim P() As Process
    Private Usr, Pass, Domain, DecodePass, tempTrace As String
    Private _exception As Exception
    'Private LogonType As ZImpersonalizeAdvance.LogonType
    Private dUpdateGui As New DUpdateGuiInfo(AddressOf UpdateGuiInfo)
    Private Shared key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Private Shared iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Public Property Exception() As Exception
        Get
            Return _exception
        End Get
        Set(ByVal value As Exception)
            _exception = value
        End Set
    End Property

#End Region

#Region "Ctor & GUI Methods"
    Public Sub New(ByVal Usr As String, _
                   ByVal Pass As String, _
                   ByVal Domain As String, _
                   ByVal DecodePass As String)
        'ByVal logonType As ZImpersonalizeAdvance.LogonType)
        MyBase.New()
        Me.Usr = Usr
        Me.Pass = Pass
        Me.Domain = Domain
        Me.DecodePass = DecodePass
        'Me.LogonType = LogonType
        InitializeComponent()
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Label1.Text = "Verificando componentes"
        Me.Show()
        Dim t As New Threading.Thread(New Threading.ThreadStart(AddressOf CheckComponents))
        t.Start()
    End Sub
    Private Sub UpdateGuiInfo(ByVal info As String)
        tempTrace = "Verificando: " & info
        Trace.WriteLine(tempTrace)
        Me.Label2.Text = tempTrace
    End Sub
#End Region

    Private Sub WaitForNGen()
        P = Process.GetProcessesByName("NGEN")
        While P.Length > 0
            Threading.Thread.CurrentThread.Sleep(300)
            P = Process.GetProcessesByName("NGEN")
        End While
    End Sub

    Private Sub CheckComponents()
        Dim Dir As String
        Dim i As Int32
        Dim directory As IO.DirectoryInfo
        Dim File() As IO.FileInfo
        Dim tempTrace As String

        Try
            Dir = System.Web.HttpRuntime.ClrInstallDirectory
            directory = New IO.DirectoryInfo(Application.StartupPath & "\")

            Trace.WriteLine("Ejecutando ngen en las bibliotecas de vinculos dinámicos.")
            Try
                File = directory.GetFiles("*.dll")

                For i = 0 To File.Length - 1
                    If File(i).Name.ToUpper.StartsWith("ZAMBA") Then
                        Try
                            Invoke(dUpdateGui, New Object() {File(i).Name})
                            Shell(Chr(34) & Dir & "\NGEN.EXE" & Chr(34) & " " & Chr(34) & File(i).FullName & Chr(34), AppWinStyle.Hide, False, 120000)
                            WaitForNGen()
                        Catch ex As Exception
                            Trace.WriteLine(ex.ToString)
                        End Try
                    End If
                Next
            Catch ex As Exception
                Trace.WriteLine(ex.ToString)
            End Try

            Trace.WriteLine("Ejecutando Ngen sobre los ejecutables.")
            File = directory.GetFiles("*.exe")
            For i = 0 To File.Length - 1
                If File(i).Name.ToUpper.StartsWith("ZAMBA") Then
                    Try
                        Invoke(dUpdateGui, New Object() {File(i).Name})
                        Shell(Chr(34) & Dir & "\NGEN.EXE" & Chr(34) & " " & Chr(34) & File(i).FullName & Chr(34), AppWinStyle.Hide, False, 120000)
                        WaitForNGen()
                    Catch ex As Exception
                        Trace.WriteLine(ex.ToString)
                    End Try
                End If
            Next

            Try
                Invoke(dUpdateGui, New Object() {"Cliente.exe"})
                Shell(Chr(34) & Dir & "\NGEN.EXE" & Chr(34) & " " & Chr(34) & "Cliente.exe" & Chr(34), AppWinStyle.Hide, True, 120000)
                WaitForNGen()
            Catch ex As Exception
                Trace.WriteLine(ex.ToString)
            End Try

            Try
                Invoke(dUpdateGui, New Object() {"Registro de Zamba"})
                If String.IsNullOrEmpty(Usr) OrElse String.IsNullOrEmpty(Pass) OrElse String.IsNullOrEmpty(Domain) Then
                    Trace.WriteLine("Credenciales no encontradas, verifique que se hayan seteado usuario, password y dominio")
                    Trace.WriteLine("# Intentando modificar el registro de Windows para registrar Zamba en el puesto.")
                    Register.RegisterZamba()
                    Trace.WriteLine("Cambios aplicados con éxito!")

                Else
                    Trace.WriteLine("Usuario para actualizacion encontrado: " & Usr)

                    If Not String.IsNullOrEmpty(DecodePass) Then
                        Dim success As Boolean = False
                        Dim zImper As New ZImpersonalize

                        Try
                            Trace.WriteLine("Impersonalizando updater")
                            success = zImper.impersonateValidUser(Usr, Domain, DecodePass)
                            If success Then
                                Trace.WriteLine("Updater impersonalizado")
                            Else
                                Trace.WriteLine("No se ha podido impesonalizar")
                            End If

                            Trace.WriteLine("Registrando Zamba en sistema Windows.")
                            Register.RegisterZamba()
                            Trace.WriteLine("Cambios aplicados con éxito!")
                        Finally
                            Try
                                If success Then
                                    Trace.WriteLine("Desimpesonalizando Updater")
                                    Trace.WriteLine("Antes de desloguear: " & Environment.UserName)
                                    zImper.undoImpersonation()
                                    Trace.WriteLine("Despues de desloguear: " & Environment.UserName)
                                    Trace.WriteLine("Updater desimpersonalizado")
                                End If
                            Catch ex As Exception
                                Trace.WriteLine(ex.ToString())
                            End Try
                            zImper = Nothing
                        End Try
                    Else
                        Trace.WriteLine("Password no decodeada")
                    End If
                End If
            Catch ex As Exception
                Trace.WriteLine(ex.ToString)
                'If MessageBox.Show("Ha ocurrido un error al registrar Zamba en Windows." & vbCrLf & _
                '                   "Puede continuar con la instalación de Zamba, pero es posible que ocurran errores al abrir documentación de Zamba por fuera del mismo." & vbCrLf & vbCrLf & _
                '                   "Presione ACEPTAR para omitir el error y continuar con la instalación.", "Error al registrar Zamba en Windows", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) _
                '               <> Windows.Forms.DialogResult.OK Then
                '    Throw ex
                'End If
            End Try

            Try
                Invoke(dUpdateGui, New Object() {"Registro de powerweb.ocx"})
                If String.IsNullOrEmpty(Usr) OrElse String.IsNullOrEmpty(Pass) OrElse String.IsNullOrEmpty(Domain) Then
                    Trace.WriteLine("Credenciales no encontradas, verifique que se hayan seteado usuario, password y dominio")
                    Trace.WriteLine("# Intentando registrar las librerias de modificación de imágenes.")
                    Register.RegistrarOcx()
                    Trace.WriteLine("Cambios aplicados con éxito!")
                Else
                    Trace.WriteLine("Usuario para actualizacion encontrado: " & Usr)

                    If Not String.IsNullOrEmpty(DecodePass) Then
                        Dim success As Boolean = False
                        Dim zImper As New ZImpersonalize

                        Try
                            Trace.WriteLine("Impersonalizando updater")
                            success = zImper.impersonateValidUser(Usr, Domain, DecodePass)
                            If success Then
                                Trace.WriteLine("Updater impersonalizado")
                            Else
                                Trace.WriteLine("No se ha podido impesonalizar")
                            End If

                            Trace.WriteLine("Registrando librerias de procesamiento de imágenes.")
                            Register.RegistrarOcx()
                            Trace.WriteLine("Cambios aplicados con éxito!")
                        Catch ex As Exception
                            Trace.WriteLine(ex.ToString)
                        Finally
                            Try
                                If success Then
                                    Trace.WriteLine("Desimpersonalizando Updater")
                                    Trace.WriteLine("Antes de desloguear: " & Environment.UserName)
                                    zImper.undoImpersonation()
                                    Trace.WriteLine("Despues de desloguear: " & Environment.UserName)
                                    Trace.WriteLine("Updater desimpersonalizado")
                                End If
                            Catch ex As Exception
                                Trace.WriteLine(ex.ToString())
                            End Try
                            zImper = Nothing
                        End Try
                    Else
                        Trace.WriteLine("Password no decodeada")
                    End If
                End If
            Catch ex As Exception
                Trace.WriteLine(ex.ToString)
                'If MessageBox.Show("Ha ocurrido un error al registrar las librerias de procesamiento de imágenes." & vbCrLf & _
                '                   "Puede continuar con la instalación de Zamba, pero es posible que ocurran errores al utilizar dichos componentes." & vbCrLf & vbCrLf & _
                '                   "Presione ACEPTAR para omitir el error y continuar con la instalación.", "Error al registrar powerweb.ocx", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) _
                '               <> Windows.Forms.DialogResult.OK Then
                '    Throw ex
                'End If
            End Try

            Try
                Invoke(dUpdateGui, New Object() {"Registro de Wiaaut.dll"})
                Trace.WriteLine("Registrando librerías Kodak.")
                Register.RegistrarWia()
                Trace.WriteLine("Cambios aplicados con éxito!")
            Catch ex As Exception
                Trace.WriteLine(ex.ToString)
                'If MessageBox.Show("Ha ocurrido un error al registrar las librerias de Kodak." & vbCrLf & _
                '                   "Puede continuar con la instalación de Zamba, pero es posible que ocurran errores al utilizar dichos componentes." & vbCrLf & vbCrLf & _
                '                  "Presione ACEPTAR para omitir el error y continuar con la instalación.", "Error al registrar Wiaaut.dll", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) _
                '               <> Windows.Forms.DialogResult.OK Then
                '    Throw ex
                'End If
            End Try

            'Se marca el proceso con resultado correcto
            Me.DialogResult = DialogResult.OK

        Catch ex As Exception
            Me.Exception = ex

            'Se marca el proceso con resultado incorrecto
            Me.DialogResult = DialogResult.Abort
        Finally
            directory = Nothing
        End Try
    End Sub

End Class
