Imports ZAMBA.Servers
'Imports Zamba.Data
Imports Zamba.Core

Imports ZAMBA.Tools
Imports ZAMBA.AppBlock
Imports System.Windows.Forms
Public Class UCInsert
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
    Friend WithEvents TP4 As ZTabPage
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New Zamba.AppBlock.ZBluePanel
        Me.btnfin = New Zamba.AppBlock.ZButton
        Me.Test = New Zamba.AppBlock.ZButton
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
        Me.TP3 = New Zamba.AppBlock.ZTabPage
        Me.TP4 = New Zamba.AppBlock.ZTabPage
        Me.Grilla = New System.Windows.Forms.DataGrid
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        Me.TP4.SuspendLayout()
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
        Me.TP3.Text = "Columnas a Insertar"
        '
        'TP4
        '
        Me.TP4.AutoScroll = True
        Me.TP4.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP4.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TP4.Controls.Add(Me.Grilla)
        Me.TP4.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP4.IncludeBackground = True
        Me.TP4.Location = New System.Drawing.Point(4, 22)
        Me.TP4.Name = "TP4"
        Me.TP4.Size = New System.Drawing.Size(312, 174)
        Me.TP4.TabIndex = 3
        Me.TP4.Text = "Consultas Guardadas"
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
        'UCInsert
        '
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UCInsert"
        Me.Size = New System.Drawing.Size(336, 256)
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP4.ResumeLayout(False)
        CType(Me.Grilla, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
#Region "Variables Globales"
    Private _servername, _conexname, _dbpassword, _usuario, _db As String
    Private _id As Int16
    Dim ds As DataSet
    ' Private _nombre As String
    Private _servertype As ZAMBA.Servers.Server.DBTYPES
    Dim con1 As IConnection 'conexion a Zamba
    Dim con2 As IConnection 'conexion elegida por el usuario
    Dim chk As Windows.Forms.CheckBox
    Dim RB As Windows.Forms.RadioButton
    Public AllColumns As New ArrayList
    Public Claves As New Hashtable
    Private _tabla As String
    Public ColumnasInsertadas As New ArrayList
    Public Event TablaName(ByVal Tabla As String)
    Public key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Public iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
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
    Public Property ServerType() As ZAMBA.Servers.Server.DBTYPES
        Get
            Return _servertype
        End Get
        Set(ByVal Value As ZAMBA.Servers.Server.DBTYPES)
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
#Region "New"
    Public Sub New(ByVal _id As Int32)
        Me.Id = _id
        Dim server As New Server(Zamba.Servers.UcSelectConnection.currentfile)
        server.InitializeConnection(Zamba.Core.UserPreferences.getValue("DateConfig", Sections.UserPreferences, "CONVERT(DATETIME, '{0}-{1}-{2} 00:00:00', 102)"), Zamba.Core.UserPreferences.getValue("DateTimeConfig", Sections.UserPreferences, "CONVERT(DATETIME, '{0}-{1}-{2} {3}:{4}:{5}', 102)"))
        server.MakeConnection()

        con1 = server.Con
        LoadConsulta()
    End Sub
#End Region
#Region "Metodos Privados"
    ''' <summary>
    ''' [Sebastian 19-05-09] carga las consultas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadConsulta()
        Dim i As Int32
        Try
            Dim sql As String = "Select * from zqcolumns where id=" & Me.Id
            Dim ds As DataSet = con1.ExecuteDataset(CommandType.Text, sql)
            Me.Tabla = CType(ds.Tables(0).Rows(0).Item(1), String)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                ColumnasInsertadas.Add(ds.Tables(0).Rows(i).Item(2))
            Next
        Catch ex As Exception
            RaiseEvent Mensaje("No se pudo obtener las columnas a actualizar para la consulta seleccionada")
        End Try
    End Sub
    Private Sub AddColumn(ByVal sender As Object, ByVal e As EventArgs)
        If sender.checked = True Then
            'RaiseEvent ColumnDevuelta(sender.text)
            ColumnasInsertadas.Add(sender.text)
        Else
            ColumnasInsertadas.Remove(sender.text)
            'RaiseEvent QuitarColumna(sender.text)
        End If
    End Sub
    ''' <summary>
    ''' [Sebastian 19-05-09] COMENTED agrega una condicion a la consulta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
                TabControl1.TabPages(2).Controls.Add(txtb)
            Else
                If Me.Claves.ContainsKey(sender.tag) Then
                    Me.Claves.Remove(sender.tag)
                End If
                ClearTxt(sender)
            End If
        Catch ex As Exception

        End Try

    End Sub
    ''' <summary>
    ''' [Sebastian 19-05-09] COMENTED limpia los text box que fueron creados dinamicamente
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <remarks> </remarks>
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
    ''' <summary>
    ''' [Sebastian 19-05-09] COMENTED carga las tablas de la base de datos a la cual estamos conectados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarTablas()
        Dim initpoint As New System.Drawing.Point
        initpoint = Panel1.Location
        If Server.IsOracle Then
            Dim sql As String = "Select Object_Name from user_objects where object_type='VIEW' or OBJECT_TYPE='TABLE' order by object_name"
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
    ''' <summary>
    ''' [Sebastian 19-05-09] COMENTED guarda el valor que ingresamos para la condicion
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
    ''' <summary>
    ''' [Sebastian 19-05-09] COMENTED agrega las columnas de la tabla seleccionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CargarColumnas(ByVal sender As Object, ByVal e As EventArgs)
        Dim initpoint As New System.Drawing.Point
        TabControl1.TabPages(2).Controls.Clear()
        If Convert.ToBoolean(sender.Checked) Then
            If Server.IsOracle Then
                Try
                    Dim sql As String
                    sql = "select * from " & sender.text.ToString()
                    Dim ds As DataSet = con2.ExecuteDataset(CommandType.Text, sql)
                    Dim i As Int32
                    For i = 0 To ds.Tables(0).Columns.Count - 1
                        chk = New Windows.Forms.CheckBox
                        chk.SetBounds(initpoint.X, initpoint.Y, 140, 25)
                        initpoint.Y += 30
                        chk.Name = i.ToString
                        chk.Text = ds.Tables(0).Columns(i).ColumnName.ToString
                        chk.Tag = ds.Tables(0).Columns(i).ColumnName.ToString
                        AllColumns.Add(ds.Tables(0).Columns(i).ColumnName.ToString)
                        RemoveHandler chk.CheckedChanged, AddressOf AddColumn
                        AddHandler chk.CheckedChanged, AddressOf AddColumn
                        RemoveHandler chk.CheckedChanged, AddressOf AddtoWhere
                        AddHandler chk.CheckedChanged, AddressOf AddtoWhere
                        TabControl1.TabPages(2).Controls.Add(chk)
                    Next
                Catch
                End Try
            Else
                Try
                    Dim sql As String = "sp_columns " & sender.text.ToString
                    Dim ds As DataSet = con2.ExecuteDataset(CommandType.Text, sql)
                    Dim i As Int32
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        chk = New Windows.Forms.CheckBox
                        chk.SetBounds(initpoint.X, initpoint.Y, 140, 25)
                        initpoint.Y += 30
                        chk.Name = i.ToString
                        chk.Text = CType(ds.Tables(0).Rows(i).Item(3), String)
                        chk.Tag = CType(ds.Tables(0).Rows(i).Item(3), String)
                        AllColumns.Add(ds.Tables(0).Rows(i).Item(3))
                        RemoveHandler chk.CheckedChanged, AddressOf AddColumn
                        AddHandler chk.CheckedChanged, AddressOf AddColumn
                        RemoveHandler chk.CheckedChanged, AddressOf AddtoWhere
                        AddHandler chk.CheckedChanged, AddressOf AddtoWhere
                        TabControl1.TabPages(2).Controls.Add(chk)
                    Next
                Catch
                End Try
            End If
        End If
        CargarClaves(AllColumns)
    End Sub
    Private Sub LoadCombo()
        'Dim i As Int16
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.MSSQLServer.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.MSSQLServer7Up.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.Oracle.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.Oracle9.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.OracleClient.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.MSSQLExpress.ToString)
    End Sub
    Private Sub CargarClaves(ByVal Columns As ArrayList)
        Try
            Dim i As Int16
            Dim initpoint As New System.Drawing.Point
            'TabControl1.TabPages(2).Controls.Clear()
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
                TabControl1.TabPages(2).Controls.Add(chk)
            Next
            TabControl1.TabPages(2).Controls.Add(chk)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Function Conect() As Boolean
        Dim ok As Boolean = True
        Try
            Dim server As New Server(Zamba.Servers.UcSelectConnection.currentfile)
            server.InitializeConnection(Zamba.Core.UserPreferences.getValue("DateConfig", Sections.UserPreferences, "CONVERT(DATETIME, '{0}-{1}-{2} 00:00:00', 102)"), Zamba.Core.UserPreferences.getValue("DateTimeConfig", Sections.UserPreferences, "CONVERT(DATETIME, '{0}-{1}-{2} {3}:{4}:{5}', 102)"))
            server.MakeConnection()

            con1 = server.Con
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
            'Dim server As New server
            'server.MakeConnection()
            Me.Id = CoreBusiness.GetNewID(Zamba.Core.IdTypes.ZQUERY)
            Me.Nombre = InputBox("Ingrese un nombre para guardar la consulta").Trim
            If Nombre.Length > 50 Then Nombre = Nombre.Substring(0, 50)
            Dim sql As String = "Insert into ZQueryName values(" & Me.Id & ",'" & Me.Nombre & "','" & Me.ServerName & "','" & Me.DBUser & "','" & Encryption.EncryptString(Me.DBPassword, key, iv) & "'," & Me.ServerType & ",'" & Me.DBName & "','Insert')"
            con1.ExecuteNonQuery(CommandType.Text, sql)
            ' RaiseEvent Conexion(server)
            'server.dispose()
        Catch ex As Exception
            MessageBox.Show("No se pudo guardar la consulta." & ex.ToString)
        End Try
    End Sub
    Private Sub UCInsert_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.LoadCombo()
        Me.GetSavedQuerys()
    End Sub
    Private Sub GetSavedQuerys()
        Try
            Dim server As New Server(Zamba.Servers.UcSelectConnection.currentfile)
            server.InitializeConnection(Zamba.Core.UserPreferences.getValue("DateConfig", Sections.UserPreferences, "CONVERT(DATETIME, '{0}-{1}-{2} 00:00:00', 102)"), Zamba.Core.UserPreferences.getValue("DateTimeConfig", Sections.UserPreferences, "CONVERT(DATETIME, '{0}-{1}-{2} {3}:{4}:{5}', 102)"))
            server.MakeConnection()

            con1 = server.Con
            Dim sql As String = "select * from zqueryname"
            ds = con1.ExecuteDataset(CommandType.Text, sql)
            Grilla.DataSource = ds.Tables(0)
            Grilla.Refresh()
        Catch
        End Try
    End Sub
    ''' <summary>
    ''' [sebastian 24-04-09] MODIFIED se agrego try y el insert del nombre de la consulta  y tipo 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Save()
        Dim sql As String
        Dim i As Int32
        Try


            For i = 0 To ColumnasInsertadas.Count - 1
                sql = "Insert into ZQColumns values(" & Me.Id.ToString() & ",'" & Tabla & "','" & ColumnasInsertadas(i).ToString() & "','" & ColumnasInsertadas(i).ToString() & "')"
                con1.ExecuteNonQuery(CommandType.Text, sql)
            Next
            For Each clave As String In Claves.Keys
                sql = "Insert into ZQKeys values(" & Me.Id.ToString() & ",'" & clave.ToString() & "','" & Claves(clave).ToString() & "','' " & ")"
                con1.ExecuteNonQuery(CommandType.Text, sql)
            Next

            'Dim sqlInsert As String = "Insert into ZQueryName values(" & Me.Id & ",'" & Me.Nombre & "','" & Me.ServerName & "','" & Me.DBUser & "','" & Encryption.EncryptString(Me.DBPassword, key, iv) & "'," & Me.ServerType & ",'" & Me.DBName & "','Insert')"
            'con1.ExecuteNonQuery(CommandType.Text, sqlInsert)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Function InsertString(ByVal columnasinsertadas As ArrayList) As String
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append("Insert into ")
            sql.Append(Tabla)
            sql.Append(" (")

            Dim i As Int32
            For i = 0 To columnasinsertadas.Count - 1
                sql.Append(columnasinsertadas(i))
                sql.Append(",")
            Next
            If sql.ToString.EndsWith(",") Then sql.Replace(sql.ToString, sql.ToString.Substring(0, sql.Length - 1))
            sql.Append(") values(")
            Return sql.ToString
        Finally
            sql = Nothing
        End Try
    End Function
#End Region
#Region "Evento Publico"
    Public Shared Event query(ByVal sql As String)
    Public Event Mensaje(ByVal msg As String)
    Private Sub Grilla_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grilla.DoubleClick
        Me.Id = CInt(ds.Tables(0).Rows(Grilla.CurrentCell.RowNumber).Item(0))
        Me.LoadConsulta()
        RaiseEvent query(Me.InsertString(ColumnasInsertadas))
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
            RaiseEvent query(Me.InsertString(ColumnasInsertadas))
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

    Public Shared Function ExecuteInsert(ByVal idQuery As Integer) As Int32
        Dim strSelect As String
        Dim ds As New DataSet
        Dim dsCurrentTable As New DataSet
        Dim QuerySelect As New System.Text.StringBuilder
        Dim i As Integer
        Dim ks As String = ""
        Dim cs As String = ""
        Dim QueryTable As String

        Try
            'Tabla

            QuerySelect.Append("INSERT INTO ")
            strSelect = "SELECT * FROM ZQCOLUMNS WHERE ZQCOLUMNS.ID=" & idQuery & "ORDER BY SELECTCOLUMNS"
            ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            QuerySelect.Append(ds.Tables(0).Rows(0).Item("selecttable"))
            QuerySelect.Append(" (")
            For Each row As DataRow In ds.Tables(0).Rows
                QuerySelect.Append(row("SelectColumns").ToString & ",")
            Next
            QuerySelect.Replace(QuerySelect.ToString, QuerySelect.ToString.Remove(QuerySelect.ToString.LastIndexOf(","), 1))
            QuerySelect.Append(")")
            QuerySelect.Append(" values(")

            QueryTable = "select syscolumns.name from sysobjects,syscolumns where sysobjects.id=syscolumns.id and sysobjects.name='" & ds.Tables(0).Rows(0).Item("selecttable") & "'"
            dsCurrentTable = Server.Con.ExecuteDataset(CommandType.Text, QueryTable)
            
            'valores
            strSelect = "SELECT CLAVES,VALUE FROM ZQKEYS WHERE ID=" & idQuery & " ORDER BY CLAVES"
            ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)


            For Each row As DataRow In dsCurrentTable.Tables(0).Rows
                For Each ValueToInsert As DataRow In ds.Tables(0).Rows
                    If String.Compare(row.Item(0).ToString, ValueToInsert.Item(0).ToString) = 0 Then
                        QuerySelect.Append("'" & ValueToInsert.Item(1).ToString & "',")

                    End If
                Next
            Next
            QuerySelect.Append(")")


            QuerySelect.Replace(QuerySelect.ToString, QuerySelect.ToString.Remove(QuerySelect.ToString.LastIndexOf(","), 1))
            Dim record As Int32 = Server.Con.ExecuteNonQuery(CommandType.Text, QuerySelect.ToString)
            MessageBox.Show("¡Consulta Ejecutada!", "Consultas", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return record

        Catch ex As Exception
            raiseerror(ex)
            MessageBox.Show("No se puedo ejecutar la consulta. Sentencia erronea  ", "Consultas", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Function
#End Region
End Class
