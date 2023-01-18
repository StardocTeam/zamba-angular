Imports ZAMBA.AppBlock
Imports ZAMBA.Servers
'Imports Zamba.Data
Imports Zamba.Core
Imports ZAMBA.Tools
Imports System.Windows.Forms


Public Class UCSelect
    Inherits System.Windows.Forms.UserControl

    Public key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Public iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

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
    Friend WithEvents TP1 As ZTabsPage
    Friend WithEvents TP2 As ZTabsPage
    Friend WithEvents TP3 As ZTabsPage
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
    Friend WithEvents BtnEnd As Zamba.AppBlock.ZButton
    Friend WithEvents btntest As Zamba.AppBlock.ZButton
    Friend WithEvents Test As Zamba.AppBlock.ZButton
    Friend WithEvents btnfin As Zamba.AppBlock.ZButton
    Friend WithEvents Grilla As System.Windows.Forms.DataGrid
    Friend WithEvents TP5 As ZTabsPage
    Friend WithEvents TP4 As ZTabsPage
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New Zamba.AppBlock.ZBluePanel
        Me.btnfin = New Zamba.AppBlock.ZButton
        Me.Test = New Zamba.AppBlock.ZButton
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
        Me.TP2 = New Zamba.AppBlock.ZTabsPage
        Me.TP3 = New Zamba.AppBlock.ZTabsPage
        Me.TP4 = New Zamba.AppBlock.ZTabsPage
        Me.TP5 = New Zamba.AppBlock.ZTabsPage
        Me.Grilla = New System.Windows.Forms.DataGrid
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        Me.TP5.SuspendLayout()
        CType(Me.Grilla, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnfin)
        Me.Panel1.Controls.Add(Me.Test)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(336, 256)
        Me.Panel1.TabIndex = 0
        '
        'btnfin
        '
        Me.btnfin.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnfin.Enabled = False
        Me.btnfin.Location = New System.Drawing.Point(176, 216)
        Me.btnfin.Name = "btnfin"
        Me.btnfin.Size = New System.Drawing.Size(80, 24)
        Me.btnfin.TabIndex = 2
        Me.btnfin.Text = "Finalizar"
        '
        'Test
        '
        Me.Test.DialogResult = System.Windows.Forms.DialogResult.None
        Me.Test.Location = New System.Drawing.Point(40, 216)
        Me.Test.Name = "Test"
        Me.Test.Size = New System.Drawing.Size(80, 24)
        Me.Test.TabIndex = 1
        Me.Test.Text = "Test"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TP1)
        Me.TabControl1.Controls.Add(Me.TP2)
        Me.TabControl1.Controls.Add(Me.TP3)
        Me.TabControl1.Controls.Add(Me.TP4)
        Me.TabControl1.Controls.Add(Me.TP5)
        Me.TabControl1.Location = New System.Drawing.Point(8, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(320, 200)
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
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Name = "TP1"
        Me.TP1.Size = New System.Drawing.Size(312, 174)
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
        Me.TP2.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP2.IncludeBackground = True
        Me.TP2.Location = New System.Drawing.Point(4, 22)
        Me.TP2.Name = "TP2"
        Me.TP2.Size = New System.Drawing.Size(312, 174)
        Me.TP2.TabIndex = 1
        Me.TP2.Text = "Tabla"
        '
        'TP3
        '
        Me.TP3.AutoScroll = True
        Me.TP3.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP3.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP3.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP3.IncludeBackground = True
        Me.TP3.Location = New System.Drawing.Point(4, 22)
        Me.TP3.Name = "TP3"
        Me.TP3.Size = New System.Drawing.Size(312, 174)
        Me.TP3.TabIndex = 2
        Me.TP3.Text = "Columnas"
        '
        'TP4
        '
        Me.TP4.AutoScroll = True
        Me.TP4.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP4.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP4.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP4.IncludeBackground = True
        Me.TP4.Location = New System.Drawing.Point(4, 22)
        Me.TP4.Name = "TP4"
        Me.TP4.Size = New System.Drawing.Size(312, 174)
        Me.TP4.TabIndex = 4
        Me.TP4.Text = "Where"
        '
        'TP5
        '
        Me.TP5.AutoScroll = True
        Me.TP5.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP5.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP5.Controls.Add(Me.Grilla)
        Me.TP5.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP5.IncludeBackground = True
        Me.TP5.Location = New System.Drawing.Point(4, 22)
        Me.TP5.Name = "TP5"
        Me.TP5.Size = New System.Drawing.Size(312, 174)
        Me.TP5.TabIndex = 3
        Me.TP5.Text = "Consultas Guardadas"
        '
        'Grilla
        '
        Me.Grilla.DataMember = ""
        Me.Grilla.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.Grilla.Location = New System.Drawing.Point(8, 8)
        Me.Grilla.Name = "Grilla"
        Me.Grilla.Size = New System.Drawing.Size(296, 152)
        Me.Grilla.TabIndex = 0
        '
        'UCSelect
        '
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UCSelect"
        Me.Size = New System.Drawing.Size(336, 256)
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP5.ResumeLayout(False)
        CType(Me.Grilla, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
#Region "Variables Globales"
    Private _servername, _conexname, _dbpassword, _usuario, _db As String
    Private _id As Int16
    Dim ds As DataSet
    'Private _nombre As String
    Private _servertype As String
    '    Dim con1 As IConnection 'conexion a Zamba
    '   Dim con2 As IConnection 'conexion elegida por el usuario
    Dim chk As Windows.Forms.CheckBox
    Dim RB As Windows.Forms.RadioButton
    Public AllColumns As New ArrayList
    Public Claves As New Hashtable
    Private _tabla As String
    Public ColumnasInsertadas As New ArrayList
    Public Event TablaName(ByVal Tabla As String)
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
    Public Property ServerType() As String
        Get
            Return _servertype
        End Get
        Set(ByVal Value As String)
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
    Public Property Tabla() As String
        Get
            Return _tabla
        End Get
        Set(ByVal Value As String)
            _tabla = Value
        End Set
    End Property
#End Region
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
#Region "New"
    Public Sub New(ByVal _id As Int32)
        Me.New()
        Me.Id = _id
        LoadConsulta()
    End Sub
#End Region
#Region "Metodos Privados"
   
    Private Sub AddColumn(ByVal sender As Object, ByVal e As EventArgs)
        
        If sender.checked = True Then
            'RaiseEvent ColumnDevuelta(sender.text)
            ColumnasInsertadas.Add(sender.text)
        Else
            ColumnasInsertadas.Remove(sender.text)
            'RaiseEvent QuitarColumna(sender.text)
        End If
    End Sub
    Private Sub ClearTxt(ByVal Sender As Object)
        Try
            Dim i As Int32
            For i = 0 To Me.TabControl1.TabPages(2).Controls.Count - 1
                If Me.TabControl1.TabPages(2).Controls(i).Tag = Sender.tag And Me.TabControl1.TabPages(2).Controls(i).Location.X <> 0 Then
                    Me.TabControl1.TabPages(2).Controls(i).Visible = False
                    Me.TabControl1.TabPages(2).Controls.RemoveAt(i)
                    If Me.ColumnasInsertadas.Contains(Sender.tag) Then Me.ColumnasInsertadas.Remove(Sender.tag)
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub
    Private Sub AddColumValue(ByVal Sender As Object, ByVal e As EventArgs)
        Try
            If Me.Claves.ContainsKey(Sender.tag) Then
                Me.Claves(Sender.tag) = Sender.text
            Else
                Me.Claves.Add(Sender.tag, Sender.text)
            End If
            'If Me.ColumnasInsertadas.Contains(Sender.tag) Then
            '    Me.ColumnasInsertadas(Sender.tag) = Sender.text
            'Else
            '    Me.ColumnasInsertadas.Add(Sender.text)
            'End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub AddtoWhere(ByVal sender As Object, ByVal e As EventArgs)


        Try
            If sender.checked = True Then
                If Me.ColumnasInsertadas.Count > 0 AndAlso Me.Claves.ContainsKey(sender.tag) Then
                    Me.Claves(sender.tag) = sender.text
                Else
                    Me.Claves.Add(sender.tag, sender.text)
                End If
                Dim i As Integer
                Dim txtb As New Windows.Forms.TextBox
                txtb.SetBounds(Convert.ToInt32(sender.width) + 10, Convert.ToInt32(sender.location.y), 100, 25)
                txtb.Name = "txtColumn" & i.ToString
                txtb.Text = ""
                txtb.Tag = sender.text
                RemoveHandler txtb.TextChanged, AddressOf AddColumValue
                AddHandler txtb.TextChanged, AddressOf AddColumValue
                TabControl1.TabPages(3).Controls.Add(txtb)
            Else
                If Me.Claves.ContainsKey(sender.tag) Then
                    Me.Claves.Remove(sender.tag)
                End If
                ClearTxt(sender)
            End If
        Catch ex As Exception
        End Try


        'If Convert.ToBoolean(sender.checked) Then
        '    Claves.Add(sender.text)
        '    Try
        '        BtnEnd.Enabled = True
        '    Catch ex As Exception
        '        btnfin.Enabled = True
        '    End Try
        'Else
        '    Claves.Remove(sender.text)
        'End If
    End Sub
    Private Sub CargarTablas()
        Dim initpoint As New System.Drawing.Point
        initpoint = Panel1.Location
        If Server.ServerType = Server.DBTYPES.OracleClient Or Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.Oracle Then
            Dim sql As String = "Select Object_Name from user_objects where object_type='VIEW' or OBJECT_TYPE='TABLE' order by object_name"
            Dim ds As DataSet = con2.ExecuteDataset(CommandType.Text, sql)
            Dim i As Int32
            For i = 0 To ds.Tables(0).Rows.Count - 1
                RB = New Windows.Forms.RadioButton
                RB.BackColor = Color.Transparent
                RemoveHandler RB.CheckedChanged, AddressOf CargarColumnas
                RemoveHandler RB.CheckedChanged, AddressOf NombreTabla
                AddHandler RB.CheckedChanged, AddressOf CargarColumnas
                AddHandler RB.CheckedChanged, AddressOf NombreTabla
                RB.SetBounds(initpoint.X, initpoint.Y, 140, 25)
                initpoint.Y += 30
                RB.Name = i.ToString
                RB.Text = CType(ds.Tables(0).Rows(i).Item(0), String)
                TabControl1.TabPages(1).Controls.Add(RB)
            Next
            'Cargo los sinonimos
            Try
                sql = "Select synonym_name from user_synonyms"
                ds = Con2.ExecuteDataset(CommandType.Text, sql)
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    RB = New RadioButton
                    RB.BackColor = Color.Transparent
                    RemoveHandler RB.CheckedChanged, AddressOf CargarColumnas
                    RemoveHandler RB.CheckedChanged, AddressOf NombreTabla
                    AddHandler RB.CheckedChanged, AddressOf CargarColumnas
                    AddHandler RB.CheckedChanged, AddressOf NombreTabla
                    RB.SetBounds(initpoint.X, initpoint.Y, 140, 25)
                    initpoint.Y += 30
                    RB.Text = CType(ds.Tables(0).Rows(i).Item(0), String)
                    TabControl1.TabPages(1).Controls.Add(RB)
                Next
            Catch ex As Exception
            End Try
        Else
            Try
                Dim sql As String = "select name from sysobjects where xtype='U' or xtype='V' order by name"
                Dim ds As DataSet = con2.ExecuteDataset(CommandType.Text, sql)
                Dim i As Int32
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    RB = New Windows.Forms.RadioButton
                    RemoveHandler RB.CheckedChanged, AddressOf CargarColumnas
                    RemoveHandler RB.CheckedChanged, AddressOf NombreTabla
                    AddHandler RB.CheckedChanged, AddressOf CargarColumnas
                    AddHandler RB.CheckedChanged, AddressOf NombreTabla
                    RB.SetBounds(initpoint.X, initpoint.Y, 140, 25)
                    initpoint.Y += 30
                    RB.Name = i.ToString
                    RB.Text = CType(ds.Tables(0).Rows(i).Item(0), String)
                    TabControl1.TabPages(1).Controls.Add(RB)
                Next
            Catch
            End Try
        End If
    End Sub
    Private Sub NombreTabla(ByVal sender As Object, ByVal e As EventArgs)
        'RaiseEvent TablaName(sender.Text)
        Me.Tabla = sender.text.ToString()
    End Sub
    Private Sub CargarColumnas(ByVal sender As Object, ByVal e As EventArgs)
        Dim initpoint As New System.Drawing.Point
        TabControl1.TabPages(2).Controls.Clear()
        AllColumns.Clear()
        If Convert.ToBoolean(sender.Checked) Then
            If Server.ServerType = Server.DBTYPES.OracleClient Or Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.Oracle Then
                Try
                    Dim sql As String
                    sql = "select * from " & sender.text.ToString()
                    Dim ds As DataSet = Con2.ExecuteDataset(CommandType.Text, sql)
                    Dim i As Int32
                    For i = 0 To ds.Tables(0).Columns.Count - 1
                        chk = New Windows.Forms.CheckBox
                        chk.BackColor = Color.Transparent
                        chk.SetBounds(initpoint.X, initpoint.Y, 140, 25)
                        initpoint.Y += 30
                        chk.Name = i.ToString
                        chk.Text = ds.Tables(0).Columns(i).ColumnName.ToString
                        AllColumns.Add(ds.Tables(0).Columns(i).ColumnName.ToString)
                        RemoveHandler chk.CheckedChanged, AddressOf AddColumn
                        AddHandler chk.CheckedChanged, AddressOf AddColumn
                        TabControl1.TabPages(2).Controls.Add(chk)
                    Next
                Catch
                End Try
            Else
                Try
                    Dim sql As String = "sp_columns " & sender.text
                    Dim ds As DataSet = Con2.ExecuteDataset(CommandType.Text, sql)
                    Dim i As Int32
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        chk = New Windows.Forms.CheckBox
                        chk.BackColor = Color.Transparent
                        chk.SetBounds(initpoint.X, initpoint.Y, 140, 25)
                        initpoint.Y += 30
                        chk.Name = i.ToString
                        chk.Text = CType(ds.Tables(0).Rows(i).Item(3), String)
                        AllColumns.Add(ds.Tables(0).Rows(i).Item(3))
                        RemoveHandler chk.CheckedChanged, AddressOf AddColumn
                        AddHandler chk.CheckedChanged, AddressOf AddColumn
                        TabControl1.TabPages(2).Controls.Add(chk)
                    Next
                Catch
                End Try
            End If
        End If
        CargarClaves(AllColumns)
    End Sub
    Private Sub CargarClaves(ByVal Columns As ArrayList)
        Try
            Dim i As Int16
            Dim initpoint As New System.Drawing.Point
            TabControl1.TabPages(3).Controls.Clear()
            For i = 0 To Columns.Count - 1
                chk = New Windows.Forms.CheckBox
                chk.BackColor = Color.Transparent
                chk.SetBounds(initpoint.X, initpoint.Y, 140, 25)
                initpoint.Y += 30
                chk.Name = i.ToString
                chk.Text = Columns(i).ToString  'CType(ds.Tables(0).Rows(i).Item(3), String)
                chk.Tag = Columns(i)
                'AllColumns.Add(ds.Tables(0).Rows(i).Item(3))
                RemoveHandler chk.CheckedChanged, AddressOf AddtoWhere
                AddHandler chk.CheckedChanged, AddressOf AddtoWhere
                TabControl1.TabPages(3).Controls.Add(chk)
            Next
            TabControl1.TabPages(2).Controls.Add(chk)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub LoadCombo()
        'Dim i As Int16
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.MSSQLServer.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.MSSQLServer7Up.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.Oracle.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.Oracle9.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.OracleClient.ToString)
    End Sub
    Dim Con2 As IConnection
    Private Function Conect() As Boolean
        Dim ok As Boolean = True
        Try
            Dim s As New Server
            s.MakeConnection()
            s.dispose()
            Con2 = Server.Con(Me.ServerType, Me.ServerName, Me.DBName, Me.DBUser, Me.DBPassword)
            Me.CargarTablas()
            ' s.dispose()
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
            Me.Id = CoreBusiness.GetNewID(Zamba.Core.IdTypes.ZQUERY)
            Me.Nombre = InputBox("Ingrese un nombre para guardar la consulta").Trim
            If Nombre.Length > 50 Then Nombre = Nombre.Substring(0, 50)
            Dim sql As String = "Insert into ZQueryName(Id,Nombre,ServerName,Usuario,Clave,ServerType,db) values(" & Me.Id & ",'" & Me.Nombre & "','" & Me.ServerName & "','" & Me.DBUser & "','" & Encryption.EncryptString(Me.DBPassword, key, iv) & "'," & Me.ServerType & ",'" & Me.DBName & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch ex As Exception
            MessageBox.Show("No se pudo guardar la consulta." & ex.ToString)
        End Try
    End Sub
    Private Sub UCInsert_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim server As New server
        'server.MakeConnection()
        'server.dispose()
        LoadConsulta()
        Me.LoadCombo()
        Me.GetSavedQuerys()
    End Sub
    Private Sub GetSavedQuerys()
        Try
            'Dim server As New server
            'server.MakeConnection()
            'server.dispose()
            Dim sql As String = "select * from zqueryname"
            ds = server.Con.ExecuteDataset(CommandType.Text, sql)
            Grilla.DataSource = ds.Tables(0)
            Grilla.Refresh()
        Catch
        End Try
    End Sub
    Private Sub Save()
        Dim sql As String
        Dim i As Int32
        Try

        
            For i = 0 To ColumnasInsertadas.Count - 1
                sql = "Insert into ZQColumns(ID,SELECTTABLE,SELECTCOLUMNS)values(" & Me.Id & ",'" & Tabla & "','" & ColumnasInsertadas(i) & "')"
                Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            Next
            For Each Clave As String In Claves.Keys
                sql = "Insert into ZQKeys(ID,Claves, Value) values(" & Me.Id & ",'" & Clave.ToString.Trim & "','" & Claves(Clave).ToString.Trim & "')"
                Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            Next
            Dim UpQueryType As String = "Insert into ZQueryName values(" & Me.Id & ",'" & Me.Nombre & "','" & Me.ServerName & "','" & Me.DBUser & "','" & Encryption.EncryptString(Me.DBPassword, key, iv) & "'," & Me.ServerType & ",'" & Me.DBName & "','Select')"
            Server.Con.ExecuteNonQuery(CommandType.Text, UpQueryType)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region
#Region "Evento Publico"
    Public Shared Event query(ByVal sql As String)
    Public Shared Event stype(ByVal server As Zamba.Servers.Server.DBTYPES)
    Public Shared Event sname(ByVal name As String)
    Public Shared Event database(ByVal nombre As String)
    Public Shared Event password(ByVal clave As String)
    Public Shared Event usuario(ByVal username As String)
    Public Event Mensaje(ByVal msg As String)
    Private Sub Grilla_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grilla.DoubleClick
        Me.Id = CInt(ds.Tables(0).Rows(Grilla.CurrentCell.RowNumber).Item(0))
        Me.LoadConsulta()
        '[Sebastian 19-05-09] se realizo este for each para poder pasarle a make select el parametro sin 
        'modificar el metodo.
        'todo: optimizar esto para no tener que hacer uso de for each.
        Dim ClavesWhere As New ArrayList
        For Each a As String In Claves.Keys
            ClavesWhere.Add(a)
        Next
        RaiseEvent query(DataBaseAccessBusiness.MakeSelect(Me.Id, ClavesWhere))
    End Sub
#End Region
#Region "Botones"
    Private Sub Btnfin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfin.Click
        Dim ok As Boolean = True
        Try
            Me.Save()
        Catch
            ok = False
        End Try
        If ok Then
            MessageBox.Show("Consulta guardada correctamente, ID= " & Me.Id & Chr(13) & "Columnas a Actualizar: " & ColumnasInsertadas.Count & Chr(13) & "Columnas Claves: " & Claves.Count, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '[Sebastian 19-05-09] se realizo este for each para poder pasarle a make select el parametro sin 
            'modificar el metodo.
            'todo: optimizar esto para no tener que hacer uso de for each
            Dim ClavesWhere As New ArrayList
            For Each a As String In Claves.Keys
                ClavesWhere.Add(a)
            Next
            RaiseEvent query(DataBaseAccessBusiness.MakeSelect(Me.Id, ClavesWhere))
            Try
                Me.Visible = False
                Me.ParentForm.Visible = False
                Me.Dispose()
                Me.ParentForm.Dispose()
            Catch
            End Try
        Else
            MessageBox.Show("NO se pudo guardar la consuta", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Test.Click
        If Me.Conect Then
            MessageBox.Show("Conexión OK", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.SaveConex()
            Me.btnfin.Enabled = True
        Else
            MessageBox.Show("Error al conectar, verifique los datos. ", "Zamba Software - Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
#End Region
#Region "Metodos Publicos"
    Public Sub LoadConsulta()
        Dim i As Int32
        Try
            Dim sql As String = "Select * from zqcolumns where id=" & Me.Id
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.Tabla = CType(ds.Tables(0).Rows(0).Item(1), String)
            End If
            For i = 0 To ds.Tables(0).Rows.Count - 1
                '  ColumnasInsertadas.Add(ds.Tables(0).Rows(i).Item(2))
            Next
            'Dim Conexion As New ArrayList
            sql = "Select SERVERNAME,USUARIO, CLAVE, SERVERTYPE,DB from zqueryname Where id=" & Me.Id
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.ServerName = ds.Tables(0).Rows(0).Item(0).ToString()
                Me.DBUser = ds.Tables(0).Rows(0).Item(1).ToString()
                Me.DBPassword = Tools.Encryption.DecryptString(ds.Tables(0).Rows(0).Item(2).ToString(), Me.key, Me.iv)
                Me.ServerType = DirectCast(Convert.ToInt32(ds.Tables(0).Rows(0).Item(3)), DbType)
                Me.DBName = ds.Tables(0).Rows(0).Item(4).ToString()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            RaiseEvent Mensaje("No se pudo obtener las columnas a actualizar para la consulta seleccionada")
            'Finally
            '    ds = Nothing
            '    ds.Dispose()
        End Try
    End Sub
    Public Shared Function ExecuteSelect(ByVal idQuery As Integer) As DataSet
        Dim strSelect As String
        Dim ds As New DataSet
        Dim QuerySelect As New System.Text.StringBuilder
        Dim i As Integer
        Dim ks As String = ""
        Dim cs As String = ""
        Try
            'Tabla
            strSelect = "SELECT * FROM ZQCOLUMNS WHERE ZQCOLUMNS.ID=" & idQuery
            ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            QuerySelect.Append("SELECT ")

            For Each row As DataRow In ds.Tables(0).Rows
                QuerySelect.Append(row("SELECTCOLUMNS") & ",")
            Next
            QuerySelect.Replace(QuerySelect.ToString, QuerySelect.ToString.Remove(QuerySelect.ToString.LastIndexOf(",")))
            QuerySelect.Append(" from " & ds.Tables(0).Rows(0).Item("SELECTTABLE").ToString)

            'condiciones para el where
            strSelect = "SELECT CLAVES,VALUE FROM ZQKEYS WHERE ID=" & idQuery
            ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            If ds.Tables(0).Rows.Count = 1 Then
                QuerySelect.Append(" WHERE " & ds.Tables(0).Rows(0).Item(0).ToString & " = '" & ds.Tables(0).Rows(0).Item(1).ToString & "'")
            End If
            If ds.Tables(0).Rows.Count > 1 Then
                QuerySelect.Append(" WHERE ")
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If i = (ds.Tables(0).Rows.Count - 1) Then
                        ks = ks + ds.Tables(0).Rows(i).ItemArray(0).ToString + " = '" + ds.Tables(0).Rows(i).ItemArray(1).ToString + "'"
                    Else
                        ks = ks + ds.Tables(0).Rows(i).ItemArray(0).ToString + " = '" + ds.Tables(0).Rows(i).ItemArray(1).ToString + "' AND "
                    End If
                Next
                QuerySelect.Append(ks)
            End If

            
            ds = Server.Con.ExecuteDataset(CommandType.Text, QuerySelect.ToString)
            If ds.Tables(0).Rows.Count > 0 Then
                MessageBox.Show("¡Consulta Ejecutada!", "Consultas", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return ds
            End If


        Catch ex As Exception
            raiseerror(ex)
            MessageBox.Show("No se puedo ejecutar la consulta. Sentencia erronea  ", "Consultas", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return ds
    End Function
#End Region
End Class
