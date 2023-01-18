Imports ZAMBA.Servers
Imports Zamba.AppBlock

Public Class UCSentence
    Inherits System.Windows.Forms.UserControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
    Friend WithEvents Panel1 As ZBluePanel
    Friend WithEvents btnCancelar As Zamba.AppBlock.ZButton1
    Friend WithEvents BtnEnd As Zamba.AppBlock.ZButton1
    Friend WithEvents TP1 As ZTabsPage
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents LinkLabel6 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel5 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel4 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel3 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents btntest As Zamba.AppBlock.ZButton
    Friend WithEvents TabControl1 As ZTabs
    Friend WithEvents TP2 As ZTabsPage
    Friend WithEvents Panel2 As ZBluePanel
    Friend WithEvents Panel3 As ZBluePanel
    Friend WithEvents ZButton1 As Zamba.AppBlock.ZButton
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New Zamba.AppBlock.ZBluePanel
        Me.btnCancelar = New Zamba.AppBlock.ZButton1
        Me.BtnEnd = New Zamba.AppBlock.ZButton1
        Me.TabControl1 = New Zamba.AppBlock.ZTabs
        Me.TP1 = New Zamba.AppBlock.ZTabsPage
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.LinkLabel6 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel5 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel4 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.btntest = New Zamba.AppBlock.ZButton
        Me.TP2 = New Zamba.AppBlock.ZTabsPage
        Me.Panel3 = New Zamba.AppBlock.ZBluePanel
        Me.ComboBox2 = New System.Windows.Forms.ComboBox
        Me.ZButton1 = New Zamba.AppBlock.ZButton
        Me.Panel2 = New Zamba.AppBlock.ZBluePanel
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        Me.TP2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnCancelar)
        Me.Panel1.Controls.Add(Me.BtnEnd)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(336, 256)
        Me.Panel1.TabIndex = 1
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnCancelar.Location = New System.Drawing.Point(120, 224)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(80, 24)
        Me.btnCancelar.TabIndex = 3
        Me.btnCancelar.Text = "Cancelar"
        '
        'BtnEnd
        '
        Me.BtnEnd.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnEnd.Location = New System.Drawing.Point(208, 224)
        Me.BtnEnd.Name = "BtnEnd"
        Me.BtnEnd.Size = New System.Drawing.Size(104, 24)
        Me.BtnEnd.TabIndex = 2
        Me.BtnEnd.Text = "Guardar"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TP1)
        Me.TabControl1.Controls.Add(Me.TP2)
        Me.TabControl1.ItemSize = New System.Drawing.Size(56, 18)
        Me.TabControl1.Location = New System.Drawing.Point(8, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(320, 208)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP1.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP1.Controls.Add(Me.TextBox4)
        Me.TP1.Controls.Add(Me.TextBox3)
        Me.TP1.Controls.Add(Me.TextBox2)
        Me.TP1.Controls.Add(Me.TextBox1)
        Me.TP1.Controls.Add(Me.ComboBox1)
        Me.TP1.Controls.Add(Me.LinkLabel6)
        Me.TP1.Controls.Add(Me.LinkLabel5)
        Me.TP1.Controls.Add(Me.LinkLabel4)
        Me.TP1.Controls.Add(Me.LinkLabel3)
        Me.TP1.Controls.Add(Me.LinkLabel2)
        Me.TP1.Controls.Add(Me.btntest)
        Me.TP1.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP1.IncludeBackground = True
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Name = "TP1"
        Me.TP1.Size = New System.Drawing.Size(312, 182)
        Me.TP1.TabIndex = 0
        Me.TP1.Text = "Conexion"
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.White
        Me.TextBox4.Location = New System.Drawing.Point(112, 120)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.PasswordChar = Microsoft.VisualBasic.ChrW(35)
        Me.TextBox4.Size = New System.Drawing.Size(176, 20)
        Me.TextBox4.TabIndex = 19
        Me.TextBox4.Text = ""
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.White
        Me.TextBox3.Location = New System.Drawing.Point(112, 96)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(176, 20)
        Me.TextBox3.TabIndex = 18
        Me.TextBox3.Text = ""
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.White
        Me.TextBox2.Location = New System.Drawing.Point(112, 72)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(176, 20)
        Me.TextBox2.TabIndex = 17
        Me.TextBox2.Text = ""
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(112, 48)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(176, 20)
        Me.TextBox1.TabIndex = 16
        Me.TextBox1.Text = ""
        '
        'ComboBox1
        '
        Me.ComboBox1.BackColor = System.Drawing.Color.White
        Me.ComboBox1.ItemHeight = 13
        Me.ComboBox1.Location = New System.Drawing.Point(112, 24)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(176, 21)
        Me.ComboBox1.TabIndex = 15
        '
        'LinkLabel6
        '
        Me.LinkLabel6.BackColor = System.Drawing.Color.White
        Me.LinkLabel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LinkLabel6.Location = New System.Drawing.Point(16, 120)
        Me.LinkLabel6.Name = "LinkLabel6"
        Me.LinkLabel6.Size = New System.Drawing.Size(88, 16)
        Me.LinkLabel6.TabIndex = 14
        Me.LinkLabel6.TabStop = True
        Me.LinkLabel6.Text = "Password"
        Me.LinkLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LinkLabel5
        '
        Me.LinkLabel5.BackColor = System.Drawing.Color.White
        Me.LinkLabel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LinkLabel5.Location = New System.Drawing.Point(16, 96)
        Me.LinkLabel5.Name = "LinkLabel5"
        Me.LinkLabel5.Size = New System.Drawing.Size(88, 16)
        Me.LinkLabel5.TabIndex = 13
        Me.LinkLabel5.TabStop = True
        Me.LinkLabel5.Text = "User"
        Me.LinkLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LinkLabel4
        '
        Me.LinkLabel4.BackColor = System.Drawing.Color.White
        Me.LinkLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LinkLabel4.Location = New System.Drawing.Point(16, 72)
        Me.LinkLabel4.Name = "LinkLabel4"
        Me.LinkLabel4.Size = New System.Drawing.Size(88, 16)
        Me.LinkLabel4.TabIndex = 12
        Me.LinkLabel4.TabStop = True
        Me.LinkLabel4.Text = "DataBase"
        Me.LinkLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LinkLabel3
        '
        Me.LinkLabel3.BackColor = System.Drawing.Color.White
        Me.LinkLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LinkLabel3.Location = New System.Drawing.Point(16, 48)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(88, 16)
        Me.LinkLabel3.TabIndex = 11
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "Server Name"
        Me.LinkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LinkLabel2
        '
        Me.LinkLabel2.BackColor = System.Drawing.Color.White
        Me.LinkLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LinkLabel2.Location = New System.Drawing.Point(16, 24)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(88, 16)
        Me.LinkLabel2.TabIndex = 10
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "ServerType"
        Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btntest
        '
        Me.btntest.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btntest.Location = New System.Drawing.Point(200, 152)
        Me.btntest.Name = "btntest"
        Me.btntest.Size = New System.Drawing.Size(80, 24)
        Me.btntest.TabIndex = 1
        Me.btntest.Text = "Conectar"
        '
        'TP2
        '
        Me.TP2.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP2.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP2.Controls.Add(Me.Panel3)
        Me.TP2.Controls.Add(Me.Panel2)
        Me.TP2.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP2.IncludeBackground = True
        Me.TP2.Location = New System.Drawing.Point(4, 22)
        Me.TP2.Name = "TP2"
        Me.TP2.Size = New System.Drawing.Size(312, 182)
        Me.TP2.TabIndex = 1
        Me.TP2.Text = "Sentencia"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.ComboBox2)
        Me.Panel3.Controls.Add(Me.ZButton1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 144)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(312, 38)
        Me.Panel3.TabIndex = 1
        '
        'ComboBox2
        '
        Me.ComboBox2.BackColor = System.Drawing.Color.White
        Me.ComboBox2.Enabled = False
        Me.ComboBox2.Items.AddRange(New Object() {"Consulta de..", "Seleccion", "Manipulacion"})
        Me.ComboBox2.Location = New System.Drawing.Point(8, 8)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(112, 21)
        Me.ComboBox2.TabIndex = 3
        Me.ComboBox2.Text = "ComboBox2"
        '
        'ZButton1
        '
        Me.ZButton1.DialogResult = System.Windows.Forms.DialogResult.None
        Me.ZButton1.Location = New System.Drawing.Point(128, 8)
        Me.ZButton1.Name = "ZButton1"
        Me.ZButton1.Size = New System.Drawing.Size(176, 24)
        Me.ZButton1.TabIndex = 2
        Me.ZButton1.Text = "Comprobar Sentencia"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TextBox5)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(312, 144)
        Me.Panel2.TabIndex = 0
        '
        'TextBox5
        '
        Me.TextBox5.BackColor = System.Drawing.Color.White
        Me.TextBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox5.Location = New System.Drawing.Point(0, 0)
        Me.TextBox5.Multiline = True
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(312, 144)
        Me.TextBox5.TabIndex = 0
        Me.TextBox5.Text = ""
        '
        'UCSentence
        '
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UCSentence"
        Me.Size = New System.Drawing.Size(336, 256)
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Public Shared Sub ExecuteSentence(ByVal idSentence As Int32, ByVal Tipo As String)
        Try

            Dim strSelect As String = "SELECT value FROM zqsentence WHERE id=" & idSentence
            Dim strSentence As String = Server.Con.ExecuteScalar(CommandType.Text, strSelect).ToString()

            Select Case Tipo.Trim
                Case "Seleccion"
                    Dim ds As New DataSet
                    ds = Server.Con.ExecuteDataset(CommandType.Text, strSentence)
                    Dim formStc As New SentenceSelect(ds)
                    formStc.ShowDialog()
                    Exit Select
                Case "Manipulacion"
                    Dim i As Int32
                    i = Server.Con.ExecuteNonQuery(CommandType.Text, strSentence)
                    MessageBox.Show("Sentencia ejecutada corréctamente " & ControlChars.NewLine & "Registros afectados: " & i.ToString, "Ejecutar", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Select
            End Select
        Catch
        End Try
    End Sub



End Class
