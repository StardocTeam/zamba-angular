Imports Zamba.Servers
Imports System.Windows.Forms
Imports ZAMBA.Tools
Imports Zamba.AppBlock
'Imports Zamba.Data
Imports Zamba.Core

Public Class UCTable
    Inherits System.Windows.Forms.UserControl
#Region "Variables Globales"
    Dim chk As CheckBox
    Dim RB As RadioButton
    'Dim con1 As IConnection 'conexion a Zamba
    Dim con2 As IConnection 'conexion elegida por el usuario
    'Dim tabla As String
    Private _servername, _conexname, _dbpassword, _usuario, _db As String
    Private _id As Int16
    ' Private _nombre As String
    Private _servertype As ZAMBA.Servers.Server.DBTYPES
    Public AllColumns As New ArrayList
    Public key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Public iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
#End Region
#Region "Eventos"
    Public Event TablaName(ByVal Tabla As String)
    Public Event ColumnDevuelta(ByVal Columna As String)
    Public Event ClaveUnica(ByVal clave As String)
    Public Event habilitar()
    Public Shared Event Conexion(ByVal conex As ZAMBA.Servers.Server)
    Public Event LastID(ByVal id As Int32)
    Public Shared Event SaveCon()
    Public Shared Event QuitarColumna(ByVal columna As String)
#End Region
#Region "Propiedades"
    Public Property ServerName() As String
        Get
            Return _servername
        End Get
        Set(ByVal Value As String)
            _servername = Value
        End Set
    End Property
    Public Property ServerType() As String
        Get
            Return _servertype
        End Get
        Set(ByVal Value As String)
            _servertype = Value
        End Set
    End Property
    Public Property DBName() As String
        Get
            Return _db
        End Get
        Set(ByVal Value As String)
            _db = Value
        End Set
    End Property
    Public Property DBPassword() As String
        Get
            Return _dbpassword
        End Get
        Set(ByVal Value As String)
            _dbpassword = Value
        End Set
    End Property
    Public Property DBUser() As String
        Get
            Return _usuario
        End Get
        Set(ByVal Value As String)
            _usuario = Value
        End Set
    End Property
    Public Property Id() As Int32
        Get
            Return _id
        End Get
        Set(ByVal Value As Int32)
            _id = Value
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
#End Region
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
    Friend WithEvents TC As ztabs
    Friend WithEvents TabPage1 As ztabspage
    Friend WithEvents TabPage2 As ztabspage
    Friend WithEvents Panel1 As ZBluePanel
    Friend WithEvents Panel2 As ZBluePanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As ZTabsPage
    Friend WithEvents TBC As ZTabs
    Friend WithEvents TabCreate As ZTabsPage
    Friend WithEvents BtnCancell As Zamba.AppBlock.ZButton 'System.Windows.Forms.Button
    Friend WithEvents btnTest As Zamba.AppBlock.ZButton
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TC = New Zamba.AppBlock.ZTabs
        Me.TabPage3 = New Zamba.AppBlock.ZTabsPage
        Me.TBC = New Zamba.AppBlock.ZTabs
        Me.TabCreate = New Zamba.AppBlock.ZTabsPage
        Me.BtnCancell = New Zamba.AppBlock.ZButton
        Me.btnTest = New Zamba.AppBlock.ZButton
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
        Me.TabPage1 = New Zamba.AppBlock.ZTabsPage
        Me.Panel1 = New Zamba.AppBlock.ZBluePanel
        Me.TabPage2 = New Zamba.AppBlock.ZTabsPage
        Me.Panel2 = New Zamba.AppBlock.ZBluePanel
        Me.Label1 = New System.Windows.Forms.Label
        Me.TC.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TBC.SuspendLayout()
        Me.TabCreate.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TC
        '
        Me.TC.Controls.Add(Me.TabPage3)
        Me.TC.Controls.Add(Me.TabPage1)
        Me.TC.Controls.Add(Me.TabPage2)
        Me.TC.Location = New System.Drawing.Point(8, 8)
        Me.TC.Name = "TC"
        Me.TC.SelectedIndex = 0
        Me.TC.Size = New System.Drawing.Size(288, 208)
        Me.TC.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabPage3.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabPage3.Controls.Add(Me.TBC)
        Me.TabPage3.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TabPage3.IncludeBackground = True
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(280, 182)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Conectar"
        '
        'TBC
        '
        Me.TBC.Controls.Add(Me.TabCreate)
        Me.TBC.Location = New System.Drawing.Point(-36, -17)
        Me.TBC.Name = "TBC"
        Me.TBC.SelectedIndex = 0
        Me.TBC.Size = New System.Drawing.Size(320, 216)
        Me.TBC.TabIndex = 1
        '
        'TabCreate
        '
        Me.TabCreate.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabCreate.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabCreate.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabCreate.Controls.Add(Me.BtnCancell)
        Me.TabCreate.Controls.Add(Me.btnTest)
        Me.TabCreate.Controls.Add(Me.TextBox4)
        Me.TabCreate.Controls.Add(Me.TextBox3)
        Me.TabCreate.Controls.Add(Me.TextBox2)
        Me.TabCreate.Controls.Add(Me.TextBox1)
        Me.TabCreate.Controls.Add(Me.ComboBox1)
        Me.TabCreate.Controls.Add(Me.LinkLabel6)
        Me.TabCreate.Controls.Add(Me.LinkLabel5)
        Me.TabCreate.Controls.Add(Me.LinkLabel4)
        Me.TabCreate.Controls.Add(Me.LinkLabel3)
        Me.TabCreate.Controls.Add(Me.LinkLabel2)
        Me.TabCreate.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TabCreate.IncludeBackground = True
        Me.TabCreate.Location = New System.Drawing.Point(4, 22)
        Me.TabCreate.Name = "TabCreate"
        Me.TabCreate.Size = New System.Drawing.Size(312, 190)
        Me.TabCreate.TabIndex = 2
        Me.TabCreate.Text = "Conectar"
        '
        'BtnCancell
        '
        Me.BtnCancell.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnCancell.Location = New System.Drawing.Point(144, 152)
        Me.BtnCancell.Name = "BtnCancell"
        Me.BtnCancell.Size = New System.Drawing.Size(80, 24)
        Me.BtnCancell.TabIndex = 11
        Me.BtnCancell.Text = "Cancelar"
        '
        'btnTest
        '
        Me.btnTest.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnTest.Location = New System.Drawing.Point(40, 152)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(72, 24)
        Me.btnTest.TabIndex = 10
        Me.btnTest.Text = "Probar"
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.White
        Me.TextBox4.Location = New System.Drawing.Point(128, 112)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.PasswordChar = Microsoft.VisualBasic.ChrW(35)
        Me.TextBox4.Size = New System.Drawing.Size(176, 20)
        Me.TextBox4.TabIndex = 9
        Me.TextBox4.Text = ""
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.White
        Me.TextBox3.Location = New System.Drawing.Point(128, 88)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(176, 20)
        Me.TextBox3.TabIndex = 8
        Me.TextBox3.Text = ""
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.White
        Me.TextBox2.Location = New System.Drawing.Point(128, 64)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(176, 20)
        Me.TextBox2.TabIndex = 7
        Me.TextBox2.Text = ""
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(128, 40)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(176, 20)
        Me.TextBox1.TabIndex = 6
        Me.TextBox1.Text = ""
        '
        'ComboBox1
        '
        Me.ComboBox1.BackColor = System.Drawing.Color.White
        Me.ComboBox1.Location = New System.Drawing.Point(128, 16)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(176, 21)
        Me.ComboBox1.TabIndex = 5
        '
        'LinkLabel6
        '
        Me.LinkLabel6.BackColor = System.Drawing.Color.White
        Me.LinkLabel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LinkLabel6.Location = New System.Drawing.Point(32, 112)
        Me.LinkLabel6.Name = "LinkLabel6"
        Me.LinkLabel6.Size = New System.Drawing.Size(88, 16)
        Me.LinkLabel6.TabIndex = 4
        Me.LinkLabel6.TabStop = True
        Me.LinkLabel6.Text = "Password"
        Me.LinkLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LinkLabel5
        '
        Me.LinkLabel5.BackColor = System.Drawing.Color.White
        Me.LinkLabel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LinkLabel5.Location = New System.Drawing.Point(32, 88)
        Me.LinkLabel5.Name = "LinkLabel5"
        Me.LinkLabel5.Size = New System.Drawing.Size(88, 16)
        Me.LinkLabel5.TabIndex = 3
        Me.LinkLabel5.TabStop = True
        Me.LinkLabel5.Text = "User"
        Me.LinkLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LinkLabel4
        '
        Me.LinkLabel4.BackColor = System.Drawing.Color.White
        Me.LinkLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LinkLabel4.Location = New System.Drawing.Point(32, 64)
        Me.LinkLabel4.Name = "LinkLabel4"
        Me.LinkLabel4.Size = New System.Drawing.Size(88, 16)
        Me.LinkLabel4.TabIndex = 2
        Me.LinkLabel4.TabStop = True
        Me.LinkLabel4.Text = "DataBase"
        Me.LinkLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LinkLabel3
        '
        Me.LinkLabel3.BackColor = System.Drawing.Color.White
        Me.LinkLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LinkLabel3.Location = New System.Drawing.Point(32, 40)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(88, 16)
        Me.LinkLabel3.TabIndex = 1
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "Server Name"
        Me.LinkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LinkLabel2
        '
        Me.LinkLabel2.BackColor = System.Drawing.Color.White
        Me.LinkLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LinkLabel2.Location = New System.Drawing.Point(32, 16)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(88, 16)
        Me.LinkLabel2.TabIndex = 0
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "ServerType"
        Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabPage1.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabPage1.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TabPage1.IncludeBackground = True
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(280, 182)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Tablas"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(210, Byte), CType(255, Byte))
        Me.Panel1.Location = New System.Drawing.Point(8, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(232, 168)
        Me.Panel1.TabIndex = 2
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage2.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabPage2.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabPage2.Controls.Add(Me.Panel2)
        Me.TabPage2.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TabPage2.IncludeBackground = True
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(280, 182)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Columnas"
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Location = New System.Drawing.Point(8, 8)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(232, 184)
        Me.Panel2.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(184, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Seleccione una tabla primero"
        '
        'UCTable
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(215, Byte), CType(228, Byte), CType(247, Byte))
        Me.Controls.Add(Me.TC)
        Me.Name = "UCTable"
        Me.Size = New System.Drawing.Size(296, 256)
        Me.TC.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TBC.ResumeLayout(False)
        Me.TabCreate.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub UCTable_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Dim dscon As New DsConfig
        ' dscon.ReadXml(".\zdbconfig.xml")
        ' Dim server As New server
        ' server.MakeConnection()
        '  con1 = server.Con
        '  server.dispose()
        '  server = New server
        '   server.MakeConnection(dscon.DsConfig.Rows(0).Item("DBType"), dscon.DsConfig.Rows(0).Item("Server"), dscon.DsConfig.Rows(0).Item("DataBase"), dscon.DsConfig.Rows(0).Item("User"), ZAMBA.Crypto.Encryption.DecryptString(dscon.DsConfig.Rows(0).Item("Password"), key, iv))
        '   con2 = server.Con
        'CargarTablas()
        'Dim i As Int16
        ComboBox1.Items.AddRange(Zamba.Core.ServersBusiness.GetServerTypes.ToArray)
        'ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.MSSQLServer.ToString)
        'ComboBox1.Items.Add(ZAMBA.Servers.Server.DBTYPES.MSSQLServer7Up.ToString)
        'ComboBox1.Items.Add(ZAMBA.Servers.Server.DBTYPES.Oracle.ToString)
        'ComboBox1.Items.Add(ZAMBA.Servers.Server.DBTYPES.Oracle9.ToString)
        'ComboBox1.Items.Add(ZAMBA.Servers.Server.DBTYPES.OracleClient.ToString)
        Me.Id = Zamba.Core.CoreBusiness.GetNewID(Zamba.Core.IdTypes.ZQUERY)
        RaiseEvent LastID(Me.Id)
    End Sub
    Private Sub AddColumn(ByVal sender As Object, ByVal e As EventArgs)
        If sender.checked = True Then
            RaiseEvent ColumnDevuelta(sender.text)
            RaiseEvent habilitar()
        Else
            RaiseEvent QuitarColumna(sender.text)
        End If
    End Sub
    Private Sub CargarTablas()
        Dim initpoint As New System.Drawing.Point
        initpoint = Panel1.Location
        If Server.ServerType = Server.DBTYPES.OracleClient OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.Oracle Then
            Dim sql As String = "Select Object_Name from user_objects where object_type='VIEW' or OBJECT_TYPE='TABLE' order by object_name"
            Dim ds As DataSet = con2.ExecuteDataset(CommandType.Text, sql)

            Dim i As Int32
            For i = 0 To ds.Tables(0).Rows.Count - 1
                RB = New RadioButton
                RemoveHandler RB.CheckedChanged, AddressOf CargarColumnas
                RemoveHandler RB.CheckedChanged, AddressOf NombreTabla
                AddHandler RB.CheckedChanged, AddressOf CargarColumnas
                AddHandler RB.CheckedChanged, AddressOf NombreTabla
                RB.SetBounds(initpoint.X, initpoint.Y, 140, 25)
                initpoint.Y += 30
                RB.Name = i.ToString
                RB.Text = CType(ds.Tables(0).Rows(i).Item(0), String)
                Panel1.Controls.Add(RB)
            Next
            'Cargo los sinonimos de Oracle
            Try
                sql = "Select synonym_name from user_synonyms"
                ds = con2.ExecuteDataset(CommandType.Text, sql)
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    RB = New RadioButton
                    RemoveHandler RB.CheckedChanged, AddressOf CargarColumnas
                    RemoveHandler RB.CheckedChanged, AddressOf NombreTabla
                    AddHandler RB.CheckedChanged, AddressOf CargarColumnas
                    AddHandler RB.CheckedChanged, AddressOf NombreTabla
                    RB.SetBounds(initpoint.X, initpoint.Y, 140, 25)
                    initpoint.Y += 30
                    RB.Name = i.ToString
                    RB.Text = CType(ds.Tables(0).Rows(i).Item(0), String)
                    Panel1.Controls.Add(RB)
                Next
            Catch
            End Try
        Else
            Try
                Dim sql As String = "select name from sysobjects where xtype='U' or xtype='V' order by name"
                Dim ds As DataSet = con2.ExecuteDataset(CommandType.Text, sql)
                Dim i As Int32
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    RB = New RadioButton
                    RemoveHandler RB.CheckedChanged, AddressOf CargarColumnas
                    RemoveHandler RB.CheckedChanged, AddressOf NombreTabla
                    AddHandler RB.CheckedChanged, AddressOf CargarColumnas
                    AddHandler RB.CheckedChanged, AddressOf NombreTabla
                    RB.SetBounds(initpoint.X, initpoint.Y, 140, 25)
                    initpoint.Y += 30
                    RB.Name = i.ToString
                    RB.Text = CType(ds.Tables(0).Rows(i).Item(0), String)
                    Panel1.Controls.Add(RB)
                Next
            Catch
            End Try
        End If
    End Sub
    Private Sub NombreTabla(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent TablaName(sender.Text)
    End Sub
    Private Sub CargarColumnas(ByVal sender As Object, ByVal e As EventArgs)
        Dim initpoint As New System.Drawing.Point
        Panel2.Controls.Clear()
        If sender.Checked Then
            If Server.ServerType = Server.DBTYPES.OracleClient OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.Oracle Then
                Try
                    Dim sql As String
                    sql = "select * from " & sender.text
                    Dim ds As DataSet = con2.ExecuteDataset(CommandType.Text, sql)
                    Dim i As Int32
                    For i = 0 To ds.Tables(0).Columns.Count - 1
                        chk = New CheckBox
                        chk.SetBounds(initpoint.X, initpoint.Y, 140, 25)
                        initpoint.Y += 30
                        chk.Name = i.ToString
                        chk.Text = Ds.Tables(0).Columns(i).ColumnName.ToString
                        AllColumns.Add(Ds.Tables(0).Columns(i).ColumnName.ToString)
                        RemoveHandler chk.CheckedChanged, AddressOf AddColumn
                        AddHandler chk.CheckedChanged, AddressOf AddColumn
                        Panel2.Controls.Add(chk)
                    Next
                Catch
                End Try
            Else
                Try
                    Dim sql As String = "sp_columns " & sender.text
                    Dim ds As DataSet = con2.ExecuteDataset(CommandType.Text, sql)
                    Dim i As Int32
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        chk = New CheckBox
                        chk.SetBounds(initpoint.X, initpoint.Y, 140, 25)
                        initpoint.Y += 30
                        chk.Name = i.ToString
                        chk.Text = CType(ds.Tables(0).Rows(i).Item(3), String)
                        AllColumns.Add(ds.Tables(0).Rows(i).item(3))
                        RemoveHandler chk.CheckedChanged, AddressOf AddColumn
                        AddHandler chk.CheckedChanged, AddressOf AddColumn
                        Panel2.Controls.Add(chk)
                    Next
                Catch
                End Try
            End If
        End If
    End Sub
    Private Function Conect() As Boolean
        Dim ok As Boolean = True
        Try
            Dim server As New server
            server.MakeConnection()
            'con1 = server.Con
            server.dispose()
            server.MakeConnection(Me.ServerType, Me.ServerName, Me.DBName, Me.DBUser, Me.DBPassword)
            con2 = server.Con
            Me.CargarTablas()
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
    Private Sub SaveConex()
        Try
            Dim s As New Server
            s.MakeConnection()
            Me.Nombre = InputBox("Ingrese un nombre para guardar la consulta").Trim
            If Nombre.Length > 50 Then Nombre = Nombre.Substring(0, 50)
            Dim sql As String = "insert into ZQueryName values(" & Me.Id & ",'" & Me.Nombre & "','" & Me.ServerName & "','" & Me.DBUser & "','" & Encryption.EncryptString(Me.DBPassword, key, iv) & "'," & Me.ServerType & ",'" & Me.DBName & "')"
            server.Con.ExecuteNonQuery(CommandType.Text, sql)
            RaiseEvent Conexion(s)
            s.dispose()
        Catch ex As Exception
            MessageBox.Show("No se pudo guardar la consulta." & ex.ToString)
        End Try
    End Sub
    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        If Me.Conect Then
            MessageBox.Show("Conexión OK", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.SaveConex()
        Else
            MessageBox.Show("Error al conectar, verifique los datos. ", "Zamba Software - Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
#Region "Validar"
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text = "MSSQLServer7Up" Then Me.ServerType = Server.DBTYPES.MSSQLServer7Up
        If ComboBox1.Text = "MSSQLServer" Then Me.ServerType = Server.DBTYPES.MSSQLServer
        If ComboBox1.Text = "Oracle" Then Me.ServerType = Server.DBTYPES.Oracle
        If ComboBox1.Text = "Oracle9" Then Me.ServerType = Server.DBTYPES.Oracle9
        If ComboBox1.Text = "OracleClient" Then Me.ServerType = Server.DBTYPES.OracleClient
        If ComboBox1.Text = "SyBase" Then Me.ServerType = Server.DBTYPES.SyBase
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

    Private Sub BtnCancell_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancell.Click

    End Sub
End Class
