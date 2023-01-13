Imports Zamba.AppBlock
'Imports Zamba.Data
Imports Zamba.Core
Imports ZAMBA.Servers
Imports System.Windows.Forms
Imports ZAMBA.Tools


Public Class UCDeleteWz
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
    Friend WithEvents TP1 As ZTabsPage
    Friend WithEvents TP2 As ZTabsPage
    Friend WithEvents TP4 As ZTabsPage
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
    Friend WithEvents Grilla As System.Windows.Forms.DataGrid
    Friend WithEvents BtnEnd As Zamba.AppBlock.ZButton
    Friend WithEvents BtnTest As Zamba.AppBlock.ZButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New Zamba.AppBlock.ZBluePanel
        Me.BtnEnd = New Zamba.AppBlock.ZButton
        Me.BtnTest = New Zamba.AppBlock.ZButton
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
        Me.TP4 = New Zamba.AppBlock.ZTabsPage
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
        Me.Panel1.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel1.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel1.Controls.Add(Me.BtnEnd)
        Me.Panel1.Controls.Add(Me.BtnTest)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(336, 256)
        Me.Panel1.TabIndex = 0
        '
        'BtnEnd
        '
        Me.BtnEnd.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnEnd.Enabled = False
        Me.BtnEnd.Location = New System.Drawing.Point(184, 216)
        Me.BtnEnd.Name = "BtnEnd"
        Me.BtnEnd.Size = New System.Drawing.Size(88, 24)
        Me.BtnEnd.TabIndex = 2
        Me.BtnEnd.Text = "Finzalizar"
        '
        'BtnTest
        '
        Me.BtnTest.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnTest.Location = New System.Drawing.Point(88, 216)
        Me.BtnTest.Name = "BtnTest"
        Me.BtnTest.Size = New System.Drawing.Size(80, 24)
        Me.BtnTest.TabIndex = 1
        Me.BtnTest.Text = "TEST"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TP1)
        Me.TabControl1.Controls.Add(Me.TP2)
        Me.TabControl1.Controls.Add(Me.TP4)
        Me.TabControl1.Location = New System.Drawing.Point(8, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(320, 200)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.Color1 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TP1.Color2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
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
        Me.TP1.UseVisualStyleBackColor = True
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.White
        Me.TextBox4.Location = New System.Drawing.Point(112, 120)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.PasswordChar = Global.Microsoft.VisualBasic.ChrW(35)
        Me.TextBox4.Size = New System.Drawing.Size(176, 20)
        Me.TextBox4.TabIndex = 19
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.White
        Me.TextBox3.Location = New System.Drawing.Point(112, 96)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(176, 20)
        Me.TextBox3.TabIndex = 18
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.White
        Me.TextBox2.Location = New System.Drawing.Point(112, 72)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(176, 20)
        Me.TextBox2.TabIndex = 17
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(112, 48)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(176, 20)
        Me.TextBox1.TabIndex = 16
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
        Me.TP2.Color1 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TP2.Color2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TP2.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP2.IncludeBackground = True
        Me.TP2.Location = New System.Drawing.Point(4, 22)
        Me.TP2.Name = "TP2"
        Me.TP2.Size = New System.Drawing.Size(312, 174)
        Me.TP2.TabIndex = 1
        Me.TP2.Text = "Tabla"
        Me.TP2.UseVisualStyleBackColor = True
        '
        'TP4
        '
        Me.TP4.AutoScroll = True
        Me.TP4.Color1 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TP4.Color2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TP4.Controls.Add(Me.Grilla)
        Me.TP4.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP4.IncludeBackground = True
        Me.TP4.Location = New System.Drawing.Point(4, 22)
        Me.TP4.Name = "TP4"
        Me.TP4.Size = New System.Drawing.Size(312, 174)
        Me.TP4.TabIndex = 3
        Me.TP4.Text = "Claves"
        Me.TP4.UseVisualStyleBackColor = True
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
        'UCDeleteWz
        '
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UCDeleteWz"
        Me.Size = New System.Drawing.Size(336, 256)
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP1.PerformLayout()
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
    Private _servertype As Zamba.Servers.Server.DBTYPES
    Dim con1 As IConnection 'conexion a Zamba
    Dim con2 As IConnection 'conexion elegida por el usuario
    Dim chk As Windows.Forms.CheckBox
    Dim RB As Windows.Forms.RadioButton
    Public AllColumns As New ArrayList
    Public Claves As New Hashtable
    Private _tabla As String
    ' Public ColumnasInsertadas As New ArrayList
    Public Event TablaName(ByVal Tabla As String)
    Public key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Public iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Public ColumnasDevueltas As New Hashtable
    Public ValoresColumnasDevueltas As New Hashtable
    Public ValoresClaves As New Hashtable

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
    Public Property ServerType() As Zamba.Servers.Server.DBTYPES
        Get
            Return _servertype
        End Get
        Set(ByVal Value As Zamba.Servers.Server.DBTYPES)
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
#Region "Evento Publico"
    Public Shared Event query(ByVal sql As String)
    Public Event Mensaje(ByVal msg As String)
    Private Sub Grilla_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grilla.DoubleClick
        Me.Id = CInt(ds.Tables(0).Rows(Grilla.CurrentCell.RowNumber).Item(0))
        Me.LoadConsulta()
        RaiseEvent query(Me.DeleteString)
    End Sub
#End Region
#Region "Metodos Privados"
    ''' <summary>
    ''' [Sebastian 08-05-09] COMENT carga las consultas en el form querys cuando deseamos ejecutar una de ellas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadConsulta()
        Dim i As Int32
        Try
            Dim sql As String = "Select * from zqcolumns where id=" & Me.Id
            Dim ds As DataSet = con1.ExecuteDataset(CommandType.Text, sql)
            Me.Tabla = CType(ds.Tables(0).Rows(0).Item(1), String)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Claves.Add(ds.Tables(0).Rows(0).Item(1), ds.Tables(0).Rows(i).Item(2))
            Next
        Catch ex As Exception
            RaiseEvent Mensaje("No se pudo obtener las columnas a actualizar para la consulta seleccionada")
        End Try
    End Sub
    ''' <summary>
    ''' [Sebastian 08-05-09] COMENT agrega las columnas de la tabla seleccionada a la siguiente solapa
    ''' donde se van a definir los filtros de la consulta (where)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddtoWhere(ByVal sender As Object, ByVal e As EventArgs)
        'If sender.checked Then
        '    Claves.Add(sender.text)
        'Else
        '    Claves.Remove(sender.text)
        'End If
        Try
            If sender.checked = True Then
                If Me.Claves.ContainsKey(sender.tag) Then
                    Me.Claves(sender.tag) = sender.text
                Else
                    Me.Claves.Add(sender.tag, sender.text)
                End If
                Dim i As Integer
                Dim txtb As New Windows.Forms.TextBox
                txtb.SetBounds(Convert.ToInt32(sender.width) + 10, Convert.ToInt32(sender.location.y), 100, 25)
                txtb.Name = "txtClave" & i.ToString
                txtb.Text = ""
                txtb.Tag = sender.tag
                RemoveHandler txtb.TextChanged, AddressOf AddClaveValue
                AddHandler txtb.TextChanged, AddressOf AddClaveValue
                TabControl1.TabPages(2).Controls.Add(txtb)
            Else
                If Me.Claves.ContainsKey(sender.tag) Then
                    Me.Claves.Remove(sender.tag)
                End If
                ClearTxtWhere(sender)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' [Sebastian 08-05-09] gurda las claves que vamos a usar para el "where" de la consula
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddClaveValue(ByVal Sender As Object, ByVal e As EventArgs)
        Try
            If Me.Claves.ContainsKey(Sender.tag) Then
                Me.ValoresClaves(Sender.tag) = Sender.text
            Else
                Me.ValoresClaves.Add(Sender.tag, Sender.text)
            End If
        Catch ex As Exception
        End Try
    End Sub
    ''' <summary>
    ''' [Sebastian 08-05-09] COMENT blanque el "textbox" donde se define el valor que hace de filtro en 
    ''' la consulta en la parte del "where".
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <remarks></remarks>
    Private Sub ClearTxtWhere(ByVal Sender As Object)
        Try
            Dim i As Int32
            For i = 0 To Me.TabControl1.TabPages(2).Controls.Count - 1
                If Me.TabControl1.TabPages(2).Controls(i).Tag = Sender.tag And Me.TabControl1.TabPages(3).Controls(i).Location.X <> 0 Then
                    Me.TabControl1.TabPages(2).Controls(i).Visible = False
                    Me.TabControl1.TabPages(2).Controls.RemoveAt(i)
                    If Me.ValoresClaves.ContainsKey(Sender.tag) Then Me.ValoresClaves.Remove(Sender.tag)
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' [Sebastian 08-05-09] agrega un "textbox" al lado del nombre de la columna por la cual vamos a filtrar
    ''' para que podamos ingresar el valor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddColumn(ByVal sender As Object, ByVal e As EventArgs)

        Try
            If sender.checked = True Then
                'RaiseEvent ColumnDevuelta(sender.text)
                Claves.Add(sender.text, sender.tag)
            Else
                'RaiseEvent QuitarColumna(sender.text)
                Claves.Remove(sender.text)
            End If


            If sender.checked = True Then
                'If Me.ColumnasDevueltas.ContainsKey(sender.tag) Then
                '    Me.ColumnasDevueltas(sender.tag) = sender.text
                'Else
                '    Me.ColumnasDevueltas.Add(sender.tag, sender.text)
                'End If
                Dim i As Integer
                Dim txtb As New Windows.Forms.TextBox
                txtb.SetBounds(Convert.ToInt32(sender.width) + 10, Convert.ToInt32(sender.location.y), 100, 25)
                txtb.Name = "txtColumn" & i.ToString
                txtb.Text = ""
                txtb.Tag = sender.tag
                RemoveHandler txtb.TextChanged, AddressOf AddColumValue
                AddHandler txtb.TextChanged, AddressOf AddColumValue
                TabControl1.TabPages(2).Controls.Add(txtb)
            Else
                If Claves.Contains(sender.text) Then
                    Claves.Remove(sender.text)
                End If
                ClearTxt(sender)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub AddColumValue(ByVal Sender As Object, ByVal e As EventArgs)
        Try
            If Me.Claves.ContainsKey(Sender.text) Then
                Me.Claves(Sender.text) = Sender.text
            Else
                Me.Claves.Add(Sender.text, Sender.text)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ClearTxt(ByVal Sender As Object)
        Try
            Dim i As Int32
            For i = 0 To Me.TabControl1.TabPages(2).Controls.Count - 1
                If Me.TabControl1.TabPages(2).Controls(i).Tag = Sender.tag And Me.TabControl1.TabPages(2).Controls(i).Location.X <> 0 Then
                    Me.TabControl1.TabPages(2).Controls(i).Visible = False
                    Me.TabControl1.TabPages(2).Controls.RemoveAt(i)
                    ' If Me.ValoresColumnasDevueltas.ContainsKey(Sender.tag) Then Me.ValoresColumnasDevueltas.Remove(Sender.tag)
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub
    'Private Sub AddtoWhere(ByVal sender As Object, ByVal e As EventArgs)
    '    If sender.checked Then
    '        Claves.Add(sender.text)
    '        BtnEnd.Enabled = True
    '    Else
    '        Claves.Remove(sender.text)
    '    End If
    'End Sub
    ''' <summary>
    ''' [Sebastian 08-05-09] COMENT carga todas las tablas disponibles de la base datos para la configuracion que
    ''' le establecimos en la configuracion de conexion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarTablas()
        Dim initpoint As New System.Drawing.Point
        initpoint = Panel1.Location
        If Server.ServerType = Server.DBTYPES.OracleClient OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.Oracle Then
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
    Private Sub CargarColumnas(ByVal sender As Object, ByVal e As EventArgs)
        Dim initpoint As New System.Drawing.Point
        TabControl1.TabPages(2).Controls.Clear()
        If Convert.ToBoolean(sender.Checked) Then
            If Server.ServerType = Server.DBTYPES.OracleClient OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.Oracle Then
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
                        AllColumns.Add(ds.Tables(0).Columns(i).ColumnName.ToString)
                        RemoveHandler chk.CheckedChanged, AddressOf AddColumn
                        AddHandler chk.CheckedChanged, AddressOf AddColumn
                        TabControl1.TabPages(2).Controls.Add(chk)
                    Next
                Catch
                End Try
            Else
                Try
                    Dim sql As String = "sp_columns " & sender.text.ToString()
                    Dim ds As DataSet = con2.ExecuteDataset(CommandType.Text, sql)
                    Dim i As Int32
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        chk = New Windows.Forms.CheckBox
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
    Private Sub CargarClaves(ByVal columnas As ArrayList)
        Dim i As Int32
        Dim Iposition As New System.Drawing.Point(0, 20)
        Dim chk As Windows.Forms.CheckBox
        Me.TabControl1.TabPages(2).Controls.Clear()
        For i = 0 To columnas.Count - 1
            chk = New Windows.Forms.CheckBox
            chk.SetBounds(Iposition.X, Iposition.Y, 140, 25)
            chk.Name = i.ToString
            chk.Text = columnas(i).ToString
            chk.Tag = columnas(i)
            Me.TabControl1.TabPages(2).Controls.Add(chk)
            Iposition.Y += 30
            RemoveHandler chk.CheckedChanged, AddressOf AddtoWhere
            AddHandler chk.CheckedChanged, AddressOf AddtoWhere
        Next
    End Sub
    Private Sub LoadCombo()
        ' Dim i As Int16
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.MSSQLServer.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.MSSQLServer7Up.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.Oracle.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.Oracle9.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.OracleClient.ToString)
    End Sub
    Private Function Conect() As Boolean
        Dim ok As Boolean = True
        Try
            Dim server As New Server
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
    ''' <summary>
    ''' [Sebastian 08-05-09] COMENT guarda la conexion que definimos en la base de datos.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveConex()
        Try
            Me.Id = CoreBusiness.GetNewID(Zamba.Core.IdTypes.ZQUERY)
            Me.Nombre = InputBox("Ingrese un nombre para guardar la consulta").Trim
            If Nombre.Length > 50 Then Nombre = Nombre.Substring(0, 50)
            Dim sql As String = "Insert into ZQueryName values(" & Me.Id & ",'" & Me.Nombre & "','" & Me.ServerName & "','" & Me.DBUser & "','" & Encryption.EncryptString(Me.DBPassword, key, iv) & "'," & Me.ServerType & ",'" & Me.DBName & "'" & ",'Delete" & "')"
            con1.ExecuteNonQuery(CommandType.Text, sql)
            ' RaiseEvent Conexion(server)
            'server.dispose()
        Catch ex As Exception
            MessageBox.Show("No se pudo guardar la consulta." & ex.ToString)
        End Try
    End Sub
    Private Sub UCDelete_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.LoadCombo()
        Me.GetSavedQuerys()
    End Sub
    Private Sub GetSavedQuerys()
        Try
            Dim server As New Server
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
    ''' [Sebastian 08-05-09] COMENT guarda la consulta que definimos en la base de datos.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Save()
        Dim sql As String
        Dim i As Int32
        Dim replace As String
        Dim flagKeys As Boolean = False
        Try

            For i = 0 To Claves.Count - 1
                sql = "Insert into ZQColumns values(" & Me.Id.ToString() & ",'" & Tabla & "','" & " " & "','" & " " & "')"
                con1.ExecuteNonQuery(CommandType.Text, sql)
            Next

            For i = 0 To AllColumns.Count - 1
                If Claves.ContainsKey(AllColumns(i)) Then
                    replace = ValoresClaves(AllColumns(i)).ToString()
                    replace = replace.Replace(Chr(39), "''")
                    'sql = "Insert into ZQKeys values(" & Me.Id.ToString() & ",'" & Claves(AllColumns(i)).ToString() & "','" & replace & "','" & Tabla & "')"
                    sql = "Insert into ZQKeys(ID,Claves,Value) values(" & Me.Id & ",'" & Claves(AllColumns(i)).ToString() & "','" & replace & "')"
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                    flagKeys = True
                End If
            Next

        Catch ex As Exception
            'Save = False
            MessageBox.Show("Error al cargar las claves", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try


        'las columnas no se guardan en este proceso, solo las claves
        'For i = 0 To Claves.Count - 1
        '    sql = "Insert into ZQKeys values(" & Me.Id.ToString() & ",'" & Claves(i).ToString & "')"
        '    con1.ExecuteNonQuery(CommandType.Text, sql)
        'Next
        'Dim sqlDelete As String = "Insert into ZQueryName values(" & Me.Id & ",'" & Me.Nombre & "','" & Me.ServerName & "','" & Me.DBUser & "','" & Encryption.EncryptString(Me.DBPassword, key, iv) & "'," & Me.ServerType & ",'" & Me.DBName & "','Delete')"
        'con1.ExecuteNonQuery(CommandType.Text, sqlDelete)


    End Sub
#End Region
#Region "Metodos Publicos"
    ''' <summary>
    ''' [Sebastian 27-04-09] Create Este método ejecuta el delete en la tabla que se le indico
    ''' </summary>
    ''' <param name="idQuery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteDelete(ByVal idQuery As Integer) As Integer
        Dim strSelect As String
        Dim ds As New DataSet
        Dim QuerySelect As New System.Text.StringBuilder
        Dim i As Integer
        Dim ks As String = ""
        Dim cs As String = ""
        Try
            'Tabla
            QuerySelect.Append("DELETE  FROM ")
            strSelect = "SELECT * FROM ZQCOLUMNS WHERE ZQCOLUMNS.ID=" & idQuery
            ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            QuerySelect.Append(ds.Tables(0).Rows(0).Item("selecttable"))


            strSelect = "SELECT CLAVES,VALUE FROM ZQKEYS WHERE ID=" & idQuery
            ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            If ds.Tables(0).Rows.Count = 1 Then
                QuerySelect.Append(" WHERE " & ds.Tables(0).Rows(0).Item(0).ToString & " = '" & ds.Tables(0).Rows(0).Item(1).ToString & "'")
            End If
            If ds.Tables(0).Rows.Count > 1 Then
                QuerySelect.Append(" WHERE ")
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If i = (ds.Tables(0).Rows.Count - 1) Then
                        ks = ks + ds.Tables(0).Rows(i).Item(0).ToString + " = '" + ds.Tables(0).Rows(i).Item(1).ToString + "'"
                    Else
                        ks = ks + ds.Tables(0).Rows(i).Item(0).ToString + " = '" + ds.Tables(0).Rows(i).Item(1).ToString + "' AND "
                    End If
                Next
            End If
            QuerySelect.Append(" " & ks)
            Dim record As Int32 = Server.Con.ExecuteNonQuery(CommandType.Text, QuerySelect.ToString)
            Return record
            'If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            MessageBox.Show("¡Consulta Ejecutada!", "Consultas", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Return ds
            'End If


        Catch ex As Exception
            raiseerror(ex)
            MessageBox.Show("No se puedo ejecutar la consulta. Sentencia erronea  ", "Consultas", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'Return Nothing
    End Function
    Public Function DeleteString() As String
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append("Delete from ")
            sql.Append(Tabla)
            Dim i As Int32
            Try
                If Claves.Count > 0 Then
                    sql.Append(" Where ")
                    For i = 0 To Claves.Count - 1
                        sql.Append(Claves(i))
                        sql.Append("=")
                        sql.Append(",")
                    Next
                    If sql.ToString.EndsWith(",") Then sql.Replace(sql.ToString, sql.ToString.Substring(0, sql.Length - 1))
                End If
            Catch
                RaiseEvent Mensaje("Error al crear la consulta")
            End Try
            Return sql.ToString
        Finally
            sql = Nothing
        End Try
    End Function

#End Region
#Region "Botones"
    Private Sub btntest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTest.Click
        If Me.Conect Then
            MessageBox.Show("Conexión OK", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.SaveConex()
            Me.BtnEnd.Enabled = True
        Else
            MessageBox.Show("Error al conectar, verifique los datos. ", "Zamba Software - Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub BtnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEnd.Click
        Dim ok As Boolean = True
        Try
            Me.Save()
        Catch
            ok = False
        End Try
        If ok Then
            MessageBox.Show("Consulta guardada correctamente, ID= " & Me.Id & Chr(13) & "Columnas Claves: " & Claves.Count, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            RaiseEvent query(Me.DeleteString)
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
#End Region

    Private Sub TP3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
