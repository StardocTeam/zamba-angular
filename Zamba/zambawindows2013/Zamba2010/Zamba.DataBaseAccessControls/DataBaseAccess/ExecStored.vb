Imports Zamba.Servers
'Imports ZAMBA.Crypto
Imports Zamba.Tools
Imports Zamba.AppBlock

Imports System.Windows.Forms
Imports Zamba.Core

Public Class ExecStored
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
    Friend WithEvents TabControl1 As ZTabs
    Friend WithEvents TP1 As ZTabPage
    Friend WithEvents TP2 As ZTabPage
    Friend WithEvents TP3 As ZTabPage
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
    Friend WithEvents TP4 As ZTabPage
    Friend WithEvents btn1 As Zamba.AppBlock.ZButton
    Friend WithEvents btn2 As Zamba.AppBlock.ZButton
    Friend WithEvents Panel2 As ZBluePanel
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As Zamba.AppBlock.ZButton
    Friend WithEvents Lista As System.Windows.Forms.ListView
    Friend WithEvents Button2 As Zamba.AppBlock.ZButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New Zamba.AppBlock.ZBluePanel
        Me.btn2 = New Zamba.AppBlock.ZButton
        Me.btn1 = New Zamba.AppBlock.ZButton
        Me.TabControl1 = New Zamba.AppBlock.ZTabs
        Me.TP1 = New Zamba.AppBlock.ZTabPage
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
        Me.TP2 = New Zamba.AppBlock.ZTabPage
        Me.Panel2 = New Zamba.AppBlock.ZBluePanel
        Me.TP4 = New Zamba.AppBlock.ZTabPage
        Me.Button2 = New Zamba.AppBlock.ZButton
        Me.Lista = New System.Windows.Forms.ListView
        Me.Button1 = New Zamba.AppBlock.ZButton
        Me.ComboBox2 = New System.Windows.Forms.ComboBox
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.TP3 = New Zamba.AppBlock.ZTabPage
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        Me.TP2.SuspendLayout()
        Me.TP4.SuspendLayout()
        Me.TP3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btn2)
        Me.Panel1.Controls.Add(Me.btn1)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(384, 256)
        Me.Panel1.TabIndex = 0
        '
        'btn2
        '
        Me.btn2.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btn2.Location = New System.Drawing.Point(200, 216)
        Me.btn2.Name = "btn2"
        Me.btn2.Size = New System.Drawing.Size(88, 24)
        Me.btn2.TabIndex = 2
        Me.btn2.Text = "Finalizar"
        '
        'btn1
        '
        Me.btn1.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btn1.Location = New System.Drawing.Point(72, 216)
        Me.btn1.Name = "btn1"
        Me.btn1.Size = New System.Drawing.Size(88, 24)
        Me.btn1.TabIndex = 1
        Me.btn1.Text = "Test"
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.TP1)
        Me.TabControl1.Controls.Add(Me.TP2)
        Me.TabControl1.Controls.Add(Me.TP4)
        Me.TabControl1.Controls.Add(Me.TP3)
        Me.TabControl1.Location = New System.Drawing.Point(8, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(368, 200)
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
        Me.TP1.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP1.IncludeBackground = True
        Me.TP1.Location = New System.Drawing.Point(4, 25)
        Me.TP1.Name = "TP1"
        Me.TP1.Size = New System.Drawing.Size(360, 171)
        Me.TP1.TabIndex = 0
        Me.TP1.Text = "Conexión"
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
        'TP2
        '
        Me.TP2.AutoScroll = True
        Me.TP2.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP2.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP2.Controls.Add(Me.Panel2)
        Me.TP2.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP2.IncludeBackground = True
        Me.TP2.Location = New System.Drawing.Point(4, 25)
        Me.TP2.Name = "TP2"
        Me.TP2.Size = New System.Drawing.Size(360, 171)
        Me.TP2.TabIndex = 1
        Me.TP2.Text = "Procedimietos Almacenados"
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.Location = New System.Drawing.Point(8, 8)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(344, 160)
        Me.Panel2.TabIndex = 0
        '
        'TP4
        '
        Me.TP4.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP4.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP4.Controls.Add(Me.Button2)
        Me.TP4.Controls.Add(Me.Lista)
        Me.TP4.Controls.Add(Me.Button1)
        Me.TP4.Controls.Add(Me.ComboBox2)
        Me.TP4.Controls.Add(Me.TextBox5)
        Me.TP4.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP4.IncludeBackground = True
        Me.TP4.Location = New System.Drawing.Point(4, 25)
        Me.TP4.Name = "TP4"
        Me.TP4.Size = New System.Drawing.Size(360, 171)
        Me.TP4.TabIndex = 4
        Me.TP4.Text = "Parametros"
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.None
        Me.Button2.Location = New System.Drawing.Point(240, 144)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(104, 24)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Remover"
        Me.Button2.Visible = False
        '
        'Lista
        '
        Me.Lista.BackColor = System.Drawing.Color.White
        Me.Lista.Location = New System.Drawing.Point(24, 96)
        Me.Lista.Name = "Lista"
        Me.Lista.Size = New System.Drawing.Size(320, 48)
        Me.Lista.TabIndex = 3
        Me.Lista.View = System.Windows.Forms.View.List
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.None
        Me.Button1.Location = New System.Drawing.Point(48, 64)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(88, 24)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Agregar"
        '
        'ComboBox2
        '
        Me.ComboBox2.BackColor = System.Drawing.Color.White
        Me.ComboBox2.Items.AddRange(New Object() {"Int", "Boolean", "varchar", "decimal", "cursor", "Date"})
        Me.ComboBox2.Location = New System.Drawing.Point(184, 32)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(152, 21)
        Me.ComboBox2.TabIndex = 1
        '
        'TextBox5
        '
        Me.TextBox5.BackColor = System.Drawing.Color.White
        Me.TextBox5.Location = New System.Drawing.Point(16, 32)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(112, 20)
        Me.TextBox5.TabIndex = 0
        Me.TextBox5.Text = ""
        '
        'TP3
        '
        Me.TP3.AutoScroll = True
        Me.TP3.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP3.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP3.Controls.Add(Me.RadioButton2)
        Me.TP3.Controls.Add(Me.RadioButton1)
        Me.TP3.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP3.IncludeBackground = True
        Me.TP3.Location = New System.Drawing.Point(4, 25)
        Me.TP3.Name = "TP3"
        Me.TP3.Size = New System.Drawing.Size(360, 171)
        Me.TP3.TabIndex = 2
        Me.TP3.Text = "Tipo de Resultado"
        '
        'RadioButton2
        '
        Me.RadioButton2.Location = New System.Drawing.Point(64, 80)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(184, 24)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.Text = "Sin Retorno de Datos"
        '
        'RadioButton1
        '
        Me.RadioButton1.Location = New System.Drawing.Point(64, 56)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(184, 16)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.Text = "Con Retorno de Datos"
        '
        'ExecStored
        '
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ExecStored"
        Me.Size = New System.Drawing.Size(384, 256)
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP2.ResumeLayout(False)
        Me.TP4.ResumeLayout(False)
        Me.TP3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
#Region "Variables Globales"
    Private _servername, _conexname, _dbpassword, _usuario, _db As String
    Private _id As Int16
    'Dim ds As DataSet
    'Private _nombre As String
    Private _servertype As Zamba.Servers.Server.DBTYPES
    'Dim con1 As IConnection 'conexion a Zamba
    'Dim con2 As IConnection 'conexion elegida por el usuario
    '  Dim chk As Windows.Forms.CheckBox
    Dim RB As Windows.Forms.RadioButton
    'Public AllColumns As New ArrayList
    'Public Claves As New ArrayList
    Private _stored As String
    Dim parnames As New ArrayList
    Dim partypes As New ArrayList
    Public ColumnasInsertadas As New ArrayList
    Public Event TablaName(ByVal Tabla As String)
    Public key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Public iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
#End Region
#Region "Validar"
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

        If ComboBox1.Text = "MSSQLServer7Up" Then Me.ServerType = Server.DBTYPES.MSSQLServer7Up
        If ComboBox1.Text = "MSSQLServer" Then Me.ServerType = Server.DBTYPES.MSSQLServer
        If ComboBox1.Text = "Oracle" Then Me.ServerType = Server.DBTYPES.Oracle
        If ComboBox1.Text = "Oracle9" Then Me.ServerType = Server.DBTYPES.Oracle9
        If ComboBox1.Text = "OracleClient" Then Me.ServerType = Server.DBTYPES.OracleClient
        If ComboBox1.Text = "SyBase" Then Me.ServerType = Server.DBTYPES.SyBase
        If ComboBox1.Text = "MSSQLExpress" Then Me.ServerType = Server.DBTYPES.MSSQLExpress

    End Sub
    Private Sub TextBox1_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Validated
        Me.ServerName = TextBox1.Text
    End Sub
    Private Sub TextBox2_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.Validated
        Me.DBName = TextBox2.Text
    End Sub
    Private Sub TextBox3_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.Validated
        Me.DBUser = TextBox3.Text
    End Sub
    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        Me.DBPassword = TextBox4.Text
    End Sub
#End Region
#Region "Propiedades"
    Public Property ServerName() As String
        Get
            Return _servername
        End Get
        Set(ByVal Value As String)
            _servername = Value
            RaiseEvent sname(Value)
        End Set
    End Property
    Public Property ServerType() As Zamba.Servers.Server.DBTYPES
        Get
            Return _servertype
        End Get
        Set(ByVal Value As Zamba.Servers.Server.DBTYPES)
            _servertype = Value
            RaiseEvent stype(Value)
        End Set
    End Property
    Public Property DBName() As String
        Get
            Return _db
        End Get
        Set(ByVal Value As String)
            _db = Value
            RaiseEvent database(Value)
        End Set
    End Property
    Public Property DBPassword() As String
        Get
            Return _dbpassword
        End Get
        Set(ByVal Value As String)
            _dbpassword = Value
            RaiseEvent password(Value)
        End Set
    End Property
    Public Property DBUser() As String
        Get
            Return _usuario
        End Get
        Set(ByVal Value As String)
            _usuario = Value
            RaiseEvent usuario(Value)
        End Set
    End Property
    Public Property Id() As Int32
        Get
            Return _id
        End Get
        Set(ByVal Value As Int32)
            _id = Convert.ToInt16(Value)
        End Set
    End Property
    Public Property Nombre() As String
        Get
            Return _conexname
        End Get
        Set(ByVal Value As String)
            _conexname = Value
        End Set
    End Property
    Public Property Stored() As String
        Get
            Return _stored
        End Get
        Set(ByVal Value As String)
            _stored = Value
        End Set
    End Property
#End Region
#Region "Evento Publico"
    Public Shared Event query(ByVal sql As String)
    Public Shared Event stype(ByVal server As Zamba.Servers.Server.DBTYPES)
    Public Shared Event sname(ByVal name As String)
    Public Shared Event database(ByVal nombre As String)
    Public Shared Event password(ByVal clave As String)
    Public Shared Event usuario(ByVal username As String)
    Public Event Mensaje(ByVal msg As String)

#End Region
#Region "Metodos Privados"

    Private Function Conect() As Boolean
        Dim ok As Boolean = True
        Try
            Dim server As New Server(Zamba.Servers.UcSelectConnection.currentfile)
            server.InitializeConnection(Zamba.Core.UserPreferences.getValue("DateConfig", Sections.UserPreferences, "CONVERT(DATETIME, '{0}-{1}-{2} 00:00:00', 102)"), Zamba.Core.UserPreferences.getValue("DateTimeConfig", Sections.UserPreferences, "CONVERT(DATETIME, '{0}-{1}-{2} {3}:{4}:{5}', 102)"))
            server.MakeConnection(Me.ServerType, Me.ServerName, Me.DBName, Me.DBUser, Me.DBPassword)
            server.dispose()
        Catch ex As Exception
            ok = False
            MessageBox.Show("No se pudo conectar con la Base de datos. " & ex.ToString, "Zamba", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try
        If ok Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region
    Private Sub btn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click
        If Me.Conect Then
            Me.CargarStored()
        End If
    End Sub
    Private Sub CargarStored()
        Dim sql As String
        Dim ds As DataSet = Nothing
        Select Case Me.ServerType
            Case Server.isSQLServer, Server.DBTYPES.MSSQLExpress, Server.DBTYPES.MSSQLServer7Up
                Try
                    sql = "Select Name from sysobjects where xtype='P' order by Name"
                    ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
                Catch
                End Try
            Case Server.DBTYPES.Oracle, Server.DBTYPES.Oracle9, Server.DBTYPES.OracleClient
                sql = "Select Object_Name from user_objects where object_type = 'PACKAGE' or object_type = 'PACKAGE BODY' order by Object_Name"
                ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Case "SyBase"
                'TODO Falta
        End Select
        Dim i As Int32
        Dim initpoint As New System.Drawing.Point
        initpoint = Panel1.Location

        For i = 0 To ds.Tables(0).Rows.Count - 1
            RB = New Windows.Forms.RadioButton
            RemoveHandler RB.CheckedChanged, AddressOf StoredName
            AddHandler RB.CheckedChanged, AddressOf StoredName
            RB.SetBounds(initpoint.X, initpoint.Y, 140, 25)
            initpoint.Y += 30
            RB.Name = i.ToString
            RB.Text = CType(ds.Tables(0).Rows(i).Item(0), String)
            RB.Width = Panel2.Height - 5
            Panel2.Controls.Add(RB)
            Panel2.AutoScroll = True
        Next
    End Sub
    Private Sub StoredName(ByVal sender As Object, ByVal e As EventArgs)
        Me.Stored = sender.text.ToString()
    End Sub
    Dim QueryType As String
    Public Event Retorno(ByVal sqltype As String)
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            QueryType = "ConRetorno"
        Else
            QueryType = "SinRetorno"
        End If
        RaiseEvent Retorno(QueryType)
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If TextBox5.Text.Trim <> "" AndAlso ComboBox2.Text.Trim <> "" Then
                parnames.Add(TextBox5.Text.Trim)
                Select Case ComboBox2.Text.Trim.ToUpper
                    Case "INT"
                        partypes.Add(13)
                    Case "CURSOR"
                        partypes.Add(5)
                    Case "VARCHAR"
                        partypes.Add(3)
                    Case "DATE"
                        partypes.Add(7)
                    Case "DECIMAL"
                        partypes.Add(17)
                End Select
                Lista.Items.Add(TextBox5.Text.Trim & " (" & ComboBox2.Text.Trim & ")")
            End If
        Catch
        End Try
    End Sub
    Private Sub Lista_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lista.SelectedIndexChanged
        If Lista.SelectedItems.Count > 0 Then
            Button2.Visible = True
        End If
    End Sub
    Public Shared Event EjecutarStored(ByVal StoredName As String, ByVal partypes As ArrayList, ByVal parnames As ArrayList, ByVal Tipo As String)
    Private Sub btn2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2.Click
        RaiseEvent EjecutarStored(Me.Stored, partypes, parnames, QueryType)
        Me.Visible = False
        Me.Dispose()
    End Sub
    Private Sub LoadCombo()
        ' Dim i As Int16

        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.MSSQLServer.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.MSSQLServer7Up.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.Oracle.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.Oracle9.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.OracleClient.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.MSSQLExpress.ToString)
    End Sub
    Private Sub ExecStored_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadCombo()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'boton remove
        Try
            partypes.Remove(Lista.SelectedIndices.Item(0))
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub
End Class
