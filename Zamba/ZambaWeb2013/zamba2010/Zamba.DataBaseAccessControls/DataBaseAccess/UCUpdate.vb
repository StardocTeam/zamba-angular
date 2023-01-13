Imports ZAMBA.Servers
Imports Zamba.AppBlock
'Imports Zamba.Data
Imports Zamba.Core

Imports ZAMBA.Tools
Imports System.Windows.Forms
Public Class UCUpdate
    Inherits ZControl

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
    Friend WithEvents BtnEnd As Zamba.AppBlock.ZButton1
    Friend WithEvents btntest As Zamba.AppBlock.ZButton
    Friend WithEvents btnCancelar As Zamba.AppBlock.ZButton1
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
        Me.TP3 = New Zamba.AppBlock.ZTabsPage
        Me.TP2 = New Zamba.AppBlock.ZTabsPage
        Me.TP4 = New Zamba.AppBlock.ZTabsPage
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel1.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel1.Controls.Add(Me.btnCancelar)
        Me.Panel1.Controls.Add(Me.BtnEnd)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(336, 256)
        Me.Panel1.TabIndex = 0
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
        Me.TabControl1.Controls.Add(Me.TP3)
        Me.TabControl1.Controls.Add(Me.TP2)
        Me.TabControl1.Controls.Add(Me.TP4)
        Me.TabControl1.Location = New System.Drawing.Point(8, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(320, 208)
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
        Me.TP1.Controls.Add(Me.btntest)
        Me.TP1.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP1.IncludeBackground = True
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Name = "TP1"
        Me.TP1.Size = New System.Drawing.Size(312, 182)
        Me.TP1.TabIndex = 0
        Me.TP1.Text = "Conexión"
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.White
        Me.TextBox4.Location = New System.Drawing.Point(112, 120)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.PasswordChar = Global.Microsoft.VisualBasic.ChrW(35)
        Me.TextBox4.Size = New System.Drawing.Size(176, 21)
        Me.TextBox4.TabIndex = 19
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.White
        Me.TextBox3.Location = New System.Drawing.Point(112, 96)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(176, 21)
        Me.TextBox3.TabIndex = 18
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.White
        Me.TextBox2.Location = New System.Drawing.Point(112, 72)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(176, 21)
        Me.TextBox2.TabIndex = 17
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(112, 48)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(176, 21)
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
        'btntest
        '
        Me.btntest.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btntest.Location = New System.Drawing.Point(200, 152)
        Me.btntest.Name = "btntest"
        Me.btntest.Size = New System.Drawing.Size(80, 24)
        Me.btntest.TabIndex = 1
        Me.btntest.Text = "Conectar"
        '
        'TP3
        '
        Me.TP3.AutoScroll = True
        Me.TP3.Color1 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TP3.Color2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TP3.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP3.IncludeBackground = True
        Me.TP3.Location = New System.Drawing.Point(4, 22)
        Me.TP3.Name = "TP3"
        Me.TP3.Size = New System.Drawing.Size(312, 182)
        Me.TP3.TabIndex = 2
        Me.TP3.Text = "Tabla a actualizar"
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
        Me.TP2.Size = New System.Drawing.Size(312, 182)
        Me.TP2.TabIndex = 1
        Me.TP2.Text = "Columnas a actualizar"
        '
        'TP4
        '
        Me.TP4.AutoScroll = True
        Me.TP4.Color1 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TP4.Color2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TP4.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TP4.IncludeBackground = True
        Me.TP4.Location = New System.Drawing.Point(4, 22)
        Me.TP4.Name = "TP4"
        Me.TP4.Size = New System.Drawing.Size(312, 182)
        Me.TP4.TabIndex = 3
        Me.TP4.Text = "Claves"
        '
        'UCUpdate
        '
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UCUpdate"
        Me.Size = New System.Drawing.Size(336, 256)
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Variables Globales"
    Private _servername, _conexname, _dbpassword, _usuario, _db As String
    Private _id As Int16
    ' Private _nombre As String
    Private _servertype As Zamba.Servers.Server.DBTYPES
    Dim con1 As IConnection 'conexion a Zamba
    Dim con2 As IConnection 'conexion elegida por el usuario
    Dim chk As Windows.Forms.CheckBox
    Dim RB As Windows.Forms.RadioButton
    Public AllColumns As New ArrayList
    Public Claves As New Hashtable
    Public ValoresClaves As New Hashtable
    Private _tabla As String
    Public ColumnasDevueltas As New Hashtable
    Public ValoresColumnasDevueltas As New Hashtable
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

    Public Event Mensaje(ByVal msg As String)

    Public Sub New(ByVal _id As Int32)
        Me.Id = _id
        Dim server As New server
        server.MakeConnection()
        con1 = server.Con
        LoadConsultas()


    End Sub

    'Preparando para comentar...
    Private Sub LoadConsultas()
        Try
            'Dim sql As String = "Select Claves from zqkeys where id=" & Me.Id
            'Dim ds As DataSet = con1.ExecuteDataset(CommandType.Text, sql)
            'For i = 0 To ds.Tables(0).Rows.Count - 1
            '    Claves.Add(ds.Tables(0).Rows(i).Item(0), ds.Tables(0).Rows(i).Item(0))
            'Next
            'se cambió por:
            DataBaseAccessBusiness.UCUpdate.GetClavesFromZqkeys(Me.Claves, Me.ColumnasDevueltas, Me.Tabla, Me.Id)
        Catch ex As Exception
            RaiseEvent Mensaje("No se pudo obtener las claves para la consulta seleccionada")
        End Try
        Try
            'Dim sql As String = "Select * from zqcolumns where id=" & Me.Id
            'Dim ds As DataSet = con1.ExecuteDataset(CommandType.Text, sql)
            'Me.Tabla = CType(ds.Tables(0).Rows(0).Item(1), String)
            'For i = 0 To ds.Tables(0).Rows.Count - 1
            '    ColumnasDevueltas.Add(ds.Tables(0).Rows(i).Item(2), ds.Tables(0).Rows(i).Item(2))
            'Next
            'se cambió por:
            DataBaseAccessBusiness.UCUpdate.GetAllFromZqcolumns(Me.Claves, Me.ColumnasDevueltas, Me.Tabla, Me.Id)
        Catch ex As Exception
            RaiseEvent Mensaje("No se pudo obtener las columnas a actualizar para la consulta seleccionada")
        End Try
    End Sub


    'Método Obsoleto, utilizar su equivalente en 
    'DataBaseAccesBusiness
    'Public Function UpdateString(ByVal Where As Hashtable, ByVal valoresNuevos As Hashtable) As String
    '    Dim sql As New System.Text.StringBuilder
    '    Try
    '        sql.Append("Update ")
    '        sql.Append(Me.Tabla)
    '        sql.Append(" set ")

    '        If Me.Id <> 0 And valoresNuevos.Count = ColumnasDevueltas.Count Then
    '            Dim i As Int32
    '            Try
    '                For i = 0 To ColumnasDevueltas.Count - 1
    '                    sql.Append(ColumnasDevueltas(i))
    '                    sql.Append("=")
    '                    If IsNumeric(valoresNuevos(i)) Then
    '                        sql.Append(valoresNuevos(i))
    '                        sql.Append(",")
    '                    Else
    '                        sql.Append("'")
    '                        sql.Append(valoresNuevos(i))
    '                        sql.Append("',")
    '                    End If
    '                Next
    '                If sql.ToString.EndsWith(",") Then sql.Replace(sql.ToString, sql.ToString.Substring(0, sql.Length - 1))
    '                sql.Append(" Where ")
    '                For i = 0 To Claves.Count - 1
    '                    sql.Append(Claves(i))
    '                    sql.Append("=")
    '                    If IsNumeric(Where(i)) Then
    '                        sql.Append(Where(i))
    '                    Else
    '                        sql.Append("'")
    '                        sql.Append(Where(i))
    '                        sql.Append("'")

    '                    End If
    '                Next
    '            Catch ex As Exception
    '                MessageBox.Show(ex.ToString)
    '            End Try
    '        Else
    '            MessageBox.Show("La cantidad de parámetros no coincide con los necesarios para la consulta " & Me.Id, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End If
    '    Return sql.ToString
    '    Finally
    '        sql = Nothing
    '    End Try
    'End Function

    'Método Obsoleto, utilizar su equivalente en 
    'DataBaseAccesBusiness
    'Public Function UpdateString(ByVal Where As ArrayList, ByVal valoresNuevos As ArrayList) As String
    '    Dim sql As New System.Text.StringBuilder
    '    Try
    '        sql.Append("Update ")
    '        sql.Append(Me.Tabla)
    '        sql.Append(" set ")
    '        If Me.Id <> 0 And valoresNuevos.Count = ColumnasDevueltas.Count Then
    '            Dim i As Int32
    '            Try
    '                For i = 0 To ColumnasDevueltas.Count - 1
    '                    sql.Append(ColumnasDevueltas(i).ToString)
    '                    sql.Append("=")
    '                    If IsNumeric(valoresNuevos(i)) Then
    '                        sql.Append(valoresNuevos(i).ToString)
    '                        sql.Append(",")
    '                    Else
    '                        sql.Append("'")
    '                        sql.Append(valoresNuevos(i).ToString)
    '                        sql.Append("',")
    '                    End If
    '                Next
    '                'revisar esta linea, porque se cambio con el stringbuilder
    '                If sql.ToString.EndsWith(",") Then sql.ToString.TrimEnd(Char.Parse(",")) ' = sql.ToString.Substring(0, sql.ToString.Length - 1)
    '                sql.Append(" Where ")
    '                For i = 0 To Claves.Count - 1
    '                    sql.Append(Claves(i).ToString())
    '                    sql.Append("=")
    '                    If IsNumeric(Where(i)) Then
    '                        sql.Append(Where(i))
    '                    Else
    '                        sql.Append("'")
    '                        sql.Append(Where(i).ToString())
    '                        sql.Append("'")
    '                    End If
    '                Next
    '            Catch ex As Exception
    '                MessageBox.Show(ex.ToString)
    '            End Try
    '        Else
    '            MessageBox.Show("La cantidad de parámetros no coincide con los necesarios para la consulta " & Me.Id, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End If
    '    Return sql.ToString
    '    Finally
    '        sql = Nothing
    '    End Try
    'End Function

    Public Shared Sub ExecuteUpdate(ByVal idQuery As Integer)
        Dim strSelect As String
        Dim ds As New DataSet
        Dim strUpdate As String
        Dim i As Integer
        Dim ks As String = ""
        Dim cs As String = ""
        Dim cant As Int32 = 0
        Try
            'Tabla
            strSelect = "SELECT ZQCOLUMNS.SELECTTABLE FROM ZQCOLUMNS WHERE ZQCOLUMNS.ID=" & idQuery
            ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            strUpdate = "UPDATE " & ds.Tables(0).Rows(0).Item(0).ToString

            'Columnas a actualizar
            strSelect = "SELECT SELECTCOLUMNS,VALUE FROM ZQCOLUMNS WHERE ID=" & idQuery
            ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            If ds.Tables(0).Rows.Count = 1 Then
                strUpdate = strUpdate & " SET " & ds.Tables(0).Rows(0).Item(0).ToString & " = '" & ds.Tables(0).Rows(0).Item(1).ToString & "'"
            End If
            If ds.Tables(0).Rows.Count > 1 Then
                strUpdate = strUpdate & " SET "
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If i = (ds.Tables(0).Rows.Count - 1) Then
                        cs += ds.Tables(0).Rows(i).Item(0).ToString + " = '" + ds.Tables(0).Rows(i).Item(1).ToString + "'"
                    Else
                        cs += ds.Tables(0).Rows(i).Item(0).ToString + " = '" + ds.Tables(0).Rows(i).Item(1).ToString + "', "
                    End If
                Next
                strUpdate += cs
            End If

            'claves
            strSelect = "SELECT CLAVES,VALUE FROM ZQKEYS WHERE ID=" & idQuery
            ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            If ds.Tables(0).Rows.Count = 1 Then
                strUpdate = strUpdate & " WHERE " & ds.Tables(0).Rows(0).Item(0).ToString & " = '" & ds.Tables(0).Rows(0).Item(1).ToString & "'"
            End If
            If ds.Tables(0).Rows.Count > 1 Then
                strUpdate = strUpdate & " WHERE "
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If i = (ds.Tables(0).Rows.Count - 1) Then
                        ks = ks + ds.Tables(0).Rows(i).Item(0).ToString + " = '" + ds.Tables(0).Rows(i).Item(1).ToString + "'"
                    Else
                        ks = ks + ds.Tables(0).Rows(i).Item(0).ToString + " = '" + ds.Tables(0).Rows(i).Item(1).ToString + "' AND "
                    End If
                Next
            End If
            strUpdate += ks
            cant = Convert.ToInt32(Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate) / 2)
            'cant = Convert.ToInt32(cant / 2)
            MessageBox.Show("Actualización ejecutada con exito!" & Chr(10) & "Registros afectados: " & cant.ToString, "Consultas", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            raiseerror(ex)
            MessageBox.Show("No se puedo ejecutar la consulta. Sentencia erronea  ", "Consultas", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Eventos"

    Private Sub UCUpdate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Id = CoreBusiness.GetNewID(Zamba.Core.IdTypes.ZQUERY)
        Me.LoadCombo()
        Me.TextBox1.Text = "yoda"
        Me.TextBox2.Text = "zamba159tst"
        Me.TextBox3.Text = "sa"
        Me.TextBox4.Text = "doc"
    End Sub

    Private Sub btntest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntest.Click
        If Me.Conect Then
            MessageBox.Show("Conexión OK", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Error al conectar, verifique los datos. ", "Zamba Software - Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Public Shared Event query(ByVal sql As String)

    Private Sub BtnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEnd.Click
        If Me.Save = True Then
            If Me.SaveConex = True Then
                MessageBox.Show("Consulta guardada correctamente, ID= " & Me.Id & "   " & Chr(13) & "Columnas a Actualizar: " & ColumnasDevueltas.Count & Chr(13) & "Columnas Claves: " & Claves.Count, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim mensajeError As String = String.Empty
                RaiseEvent query(DataBaseAccessBusiness.UCUpdate.UpdateString(Claves, ColumnasDevueltas, Me.Claves, Me.ColumnasDevueltas, Me.Tabla, Me.Id))
                If Not mensajeError = String.Empty Then
                    MessageBox.Show(mensajeError, "Zamba Software", MessageBoxButtons.OKCancel)
                End If
            End If
        Else
            MessageBox.Show("No se pudo guardar la consuta", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Try
            Me.Visible = False
            Me.ParentForm.Visible = False
            Me.Dispose()
            If Not IsNothing(Me.ParentForm) Then
                Me.ParentForm.Dispose()
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Columnas"

    Private Sub CargarColumnas(ByVal sender As Object, ByVal e As EventArgs)
        Dim initpoint As New System.Drawing.Point(0, 20)
        TabControl1.TabPages(2).Controls.Clear()
        If Convert.ToBoolean(sender.Checked) Then
            Try
                Dim sql As String
                sql = "select * from " & sender.text.ToString
                Dim ds As DataSet = con2.ExecuteDataset(CommandType.Text, sql)
                Dim i As Int32
                AllColumns.Clear()
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
                    TabControl1.TabPages(2).Controls.Add(chk)
                Next
            Catch
            End Try
        End If
        CargarClaves(AllColumns)
    End Sub

    Private Sub AddColumn(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If sender.checked = True Then
                If Me.ColumnasDevueltas.ContainsKey(sender.tag) Then
                    Me.ColumnasDevueltas(sender.tag) = sender.text
                Else
                    Me.ColumnasDevueltas.Add(sender.tag, sender.text)
                End If
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
                If Me.ColumnasDevueltas.ContainsKey(sender.tag) Then
                    Me.ColumnasDevueltas.Remove(sender.tag)
                End If
                ClearTxt(sender)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub AddColumValue(ByVal Sender As Object, ByVal e As EventArgs)
        Try
            If Me.ColumnasDevueltas.ContainsKey(Sender.tag) Then
                Me.ValoresColumnasDevueltas(Sender.tag) = Sender.text
            Else
                Me.ValoresColumnasDevueltas.Add(Sender.tag, Sender.text)
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ClearTxt(ByVal Sender As Object)
        Try
            Dim i As Int32
            For i = 0 To Me.TabControl1.TabPages(2).Controls.Count - 1
                If Me.TabControl1.TabPages(2).Controls(i).Tag = Sender.tag And Me.TabControl1.TabPages(2).Controls(i).Location.X <> 0 Then
                    Me.TabControl1.TabPages(2).Controls(i).Visible = False
                    Me.TabControl1.TabPages(2).Controls.RemoveAt(i)
                    If Me.ValoresColumnasDevueltas.ContainsKey(Sender.tag) Then Me.ValoresColumnasDevueltas.Remove(Sender.tag)
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Claves"

    Private Sub CargarClaves(ByVal columnas As ArrayList)
        Dim i As Int32
        Dim Iposition As New System.Drawing.Point(0, 20)
        Dim chk As Windows.Forms.CheckBox
        Me.TabControl1.TabPages(3).Controls.Clear()
        For i = 0 To columnas.Count - 1
            chk = New Windows.Forms.CheckBox
            chk.SetBounds(Iposition.X, Iposition.Y, 140, 25)
            chk.Name = i.ToString
            chk.Text = columnas(i).ToString
            chk.Tag = columnas(i)
            Me.TabControl1.TabPages(3).Controls.Add(chk)
            Iposition.Y += 30
            RemoveHandler chk.CheckedChanged, AddressOf AddtoWhere
            AddHandler chk.CheckedChanged, AddressOf AddtoWhere
        Next
    End Sub

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
                TabControl1.TabPages(3).Controls.Add(txtb)
            Else
                If Me.Claves.ContainsKey(sender.tag) Then
                    Me.Claves.Remove(sender.tag)
                End If
                ClearTxtWhere(sender)
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

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

    Private Sub ClearTxtWhere(ByVal Sender As Object)
        Try
            Dim i As Int32
            For i = 0 To Me.TabControl1.TabPages(3).Controls.Count - 1
                If Me.TabControl1.TabPages(3).Controls(i).Tag = Sender.tag And Me.TabControl1.TabPages(3).Controls(i).Location.X <> 0 Then
                    Me.TabControl1.TabPages(3).Controls(i).Visible = False
                    Me.TabControl1.TabPages(3).Controls.RemoveAt(i)
                    If Me.ValoresClaves.ContainsKey(Sender.tag) Then Me.ValoresClaves.Remove(Sender.tag)
                End If
            Next
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Tablas"
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
                RB.SetBounds(initpoint.X, initpoint.Y, 800, 25)
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
        Me.Tabla = sender.text.ToString
    End Sub
#End Region

#Region "Servidor"

    Private Function Conect() As Boolean
        Dim ok As Boolean = True
        Try
            Dim server As New server
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

    Private Sub LoadCombo()
        'Dim i As Int16
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.MSSQLServer.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.MSSQLServer7Up.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.Oracle.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.Oracle9.ToString)
        ComboBox1.Items.Add(Zamba.Servers.Server.DBTYPES.OracleClient.ToString)
    End Sub

    Private Function SaveConex() As Boolean
        Try
            Me.Nombre = InputBox("Ingrese un nombre para guardar la consulta").Trim
            If Nombre.Length > 50 Then Nombre = Nombre.Substring(0, 50)
            Dim sql As String = "Insert into ZQueryName values(" & Me.Id & ",'" & Me.Nombre & "','" & Me.ServerName & "','" & Me.DBUser & "','" & Encryption.EncryptString(Me.DBPassword, key, iv) & "'," & Me.ServerType & ",'" & Me.DBName & "','Update')"
            con1.ExecuteNonQuery(CommandType.Text, sql)
            SaveConex = True
        Catch ex As Exception
            MessageBox.Show("No se pudo guardar la consulta." & ex.ToString)
            SaveConex = False
        End Try
    End Function

#End Region

#Region "Validar Servidor"
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

    Private Function Save() As Boolean
        Dim sql As String
        Dim i As Int32
        Dim replace As String
        Dim flagColumns As Boolean = False
        Dim flagKeys As Boolean = False
        Try
            For i = 0 To AllColumns.Count - 1
                If ColumnasDevueltas.ContainsKey(AllColumns(i)) Then
                    replace = ValoresColumnasDevueltas(AllColumns(i)).ToString()
                    replace = replace.Replace(Chr(39), "''")
                    sql = "Insert into ZQColumns values(" & Me.Id.ToString() & ",'" & Tabla & "','" & ColumnasDevueltas(AllColumns(i)).ToString() & "','" & replace & "')"
                    con1.ExecuteNonQuery(CommandType.Text, sql)
                    flagColumns = True
                End If
            Next
        Catch ex As Exception
            flagColumns = False
        End Try

        If flagColumns = True Then
            Try
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
                Save = False
                MessageBox.Show("Error al cargar las claves", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End Try
            If flagKeys = False Then
                Save = False
                MessageBox.Show("Error al cargar las claves", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If
        Else
            MessageBox.Show("Error al cargar las columnas", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Save = False
            Exit Function
        End If
        Save = True
    End Function

End Class
