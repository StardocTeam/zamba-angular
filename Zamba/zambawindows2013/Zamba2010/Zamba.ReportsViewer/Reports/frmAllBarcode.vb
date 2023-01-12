Imports Zamba.Servers
Imports ZAMBA.core
Public Class frmAllBarcode
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
    Friend WithEvents btnaceptar As System.Windows.Forms.Button
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkAllUsers As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents chkAllCreateDate As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboUsers As System.Windows.Forms.ComboBox
    Friend WithEvents chkAllScannedDate As System.Windows.Forms.CheckBox
    Friend WithEvents cboFechaHastaCreate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboFechaDesdeCreate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboFechaHastaScanned As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboFechaDesdeScanned As System.Windows.Forms.DateTimePicker
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnaceptar = New System.Windows.Forms.Button
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.RadioButton3 = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkAllUsers = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cboUsers = New System.Windows.Forms.ComboBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboFechaHastaCreate = New System.Windows.Forms.DateTimePicker
        Me.cboFechaDesdeCreate = New System.Windows.Forms.DateTimePicker
        Me.chkAllCreateDate = New System.Windows.Forms.CheckBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboFechaHastaScanned = New System.Windows.Forms.DateTimePicker
        Me.cboFechaDesdeScanned = New System.Windows.Forms.DateTimePicker
        Me.chkAllScannedDate = New System.Windows.Forms.CheckBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnaceptar
        '
        Me.btnaceptar.Location = New System.Drawing.Point(328, 416)
        Me.btnaceptar.Name = "btnaceptar"
        Me.btnaceptar.Size = New System.Drawing.Size(88, 23)
        Me.btnaceptar.TabIndex = 7
        Me.btnaceptar.Text = "Aceptar"
        '
        'RadioButton1
        '
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(8, 24)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(184, 24)
        Me.RadioButton1.TabIndex = 8
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "No Digitalizadas + Digitalizadas"
        '
        'RadioButton2
        '
        Me.RadioButton2.Location = New System.Drawing.Point(8, 48)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(184, 24)
        Me.RadioButton2.TabIndex = 9
        Me.RadioButton2.Text = "No Digitalizadas"
        '
        'RadioButton3
        '
        Me.RadioButton3.Location = New System.Drawing.Point(8, 72)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(184, 24)
        Me.RadioButton3.TabIndex = 10
        Me.RadioButton3.Text = "Digitalizadas"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButton3)
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Location = New System.Drawing.Point(24, 16)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 104)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Caratulas"
        '
        'chkAllUsers
        '
        Me.chkAllUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkAllUsers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllUsers.Location = New System.Drawing.Point(8, 24)
        Me.chkAllUsers.Name = "chkAllUsers"
        Me.chkAllUsers.TabIndex = 12
        Me.chkAllUsers.Text = "Todos"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboUsers)
        Me.GroupBox2.Controls.Add(Me.chkAllUsers)
        Me.GroupBox2.Location = New System.Drawing.Point(232, 16)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(208, 104)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Usuario"
        '
        'cboUsers
        '
        Me.cboUsers.Location = New System.Drawing.Point(8, 56)
        Me.cboUsers.Name = "cboUsers"
        Me.cboUsers.Size = New System.Drawing.Size(192, 21)
        Me.cboUsers.TabIndex = 13
        Me.cboUsers.Text = "ComboBox1"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.cboFechaHastaCreate)
        Me.GroupBox3.Controls.Add(Me.cboFechaDesdeCreate)
        Me.GroupBox3.Controls.Add(Me.chkAllCreateDate)
        Me.GroupBox3.Location = New System.Drawing.Point(24, 128)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(416, 128)
        Me.GroupBox3.TabIndex = 14
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Fecha de Creacion"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(24, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 23)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Fecha hasta"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(24, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 23)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Fecha desde"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboFechaHastaCreate
        '
        Me.cboFechaHastaCreate.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.cboFechaHastaCreate.Location = New System.Drawing.Point(128, 88)
        Me.cboFechaHastaCreate.Name = "cboFechaHastaCreate"
        Me.cboFechaHastaCreate.TabIndex = 15
        '
        'cboFechaDesdeCreate
        '
        Me.cboFechaDesdeCreate.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.cboFechaDesdeCreate.Location = New System.Drawing.Point(128, 56)
        Me.cboFechaDesdeCreate.Name = "cboFechaDesdeCreate"
        Me.cboFechaDesdeCreate.TabIndex = 14
        '
        'chkAllCreateDate
        '
        Me.chkAllCreateDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkAllCreateDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllCreateDate.Location = New System.Drawing.Point(24, 24)
        Me.chkAllCreateDate.Name = "chkAllCreateDate"
        Me.chkAllCreateDate.TabIndex = 13
        Me.chkAllCreateDate.Text = "Todos"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.cboFechaHastaScanned)
        Me.GroupBox4.Controls.Add(Me.cboFechaDesdeScanned)
        Me.GroupBox4.Controls.Add(Me.chkAllScannedDate)
        Me.GroupBox4.Location = New System.Drawing.Point(24, 264)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(416, 128)
        Me.GroupBox4.TabIndex = 15
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Fecha de Digitalizacion"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(24, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 23)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Fecha hasta"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Location = New System.Drawing.Point(24, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 23)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Fecha desde"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboFechaHastaScanned
        '
        Me.cboFechaHastaScanned.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.cboFechaHastaScanned.Location = New System.Drawing.Point(128, 88)
        Me.cboFechaHastaScanned.Name = "cboFechaHastaScanned"
        Me.cboFechaHastaScanned.TabIndex = 19
        '
        'cboFechaDesdeScanned
        '
        Me.cboFechaDesdeScanned.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.cboFechaDesdeScanned.Location = New System.Drawing.Point(128, 56)
        Me.cboFechaDesdeScanned.Name = "cboFechaDesdeScanned"
        Me.cboFechaDesdeScanned.TabIndex = 18
        '
        'chkAllScannedDate
        '
        Me.chkAllScannedDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkAllScannedDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllScannedDate.Location = New System.Drawing.Point(24, 24)
        Me.chkAllScannedDate.Name = "chkAllScannedDate"
        Me.chkAllScannedDate.TabIndex = 14
        Me.chkAllScannedDate.Text = "Todos"
        '
        'frmAllBarcode
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(464, 453)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnaceptar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmAllBarcode"
        Me.Text = "Busqueda Avanzada"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event Opt(ByVal opt As Int16, ByVal UserId As Int32, ByVal fechaDesdeCreate As Date, ByVal fechaHastaCreate As Date, ByVal fechaDesdeScanned As Date, ByVal fechaHastaScanned As Date)
    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click
        Dim OptionCar As Int16
        Dim UserId As Int32
        Dim fechaDesdeCreate As Date
        Dim fechaHastaCreate As Date
        Dim fechaDesdeScanned As Date
        Dim fechaHastaScanned As Date
        Try
            If MyClass.RadioButton1.Checked = True Then
                OptionCar = 1
            ElseIf MyClass.RadioButton2.Checked = True Then
                OptionCar = 2
            ElseIf MyClass.RadioButton3.Checked = True Then
                OptionCar = 3
            End If
            If MyClass.chkAllUsers.Checked = True Then
                UserId = -1
            Else
                UserId = MyClass.cboUsers.SelectedValue
            End If
            If MyClass.chkAllCreateDate.Checked = True Then
                fechaDesdeCreate = "1/1/1900"
                fechaHastaCreate = "1/1/2099"
            Else
                fechaDesdeCreate = MyClass.cboFechaDesdeCreate.Text
                fechaHastaCreate = MyClass.cboFechaHastaCreate.Text
            End If
            If MyClass.chkAllScannedDate.Checked = True Then
                fechaDesdeScanned = "1/1/1900"
                fechaHastaScanned = "1/1/2099"
            Else
                fechaDesdeScanned = MyClass.cboFechaDesdeScanned.Text
                fechaHastaScanned = MyClass.cboFechaHastaScanned.Text
            End If

            RaiseEvent Opt(OptionCar, UserId, fechaDesdeCreate, fechaHastaCreate, fechaDesdeScanned, fechaHastaScanned)

        Catch ex As Exception
        End Try

        Me.Close()
    End Sub

#Region "Checked Change"
    Private Sub chkAllUsers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllUsers.CheckedChanged
        If MyClass.chkAllUsers.Checked = True Then
            Me.cboUsers.Enabled = False
        Else
            Me.cboUsers.Enabled = True
        End If
    End Sub
    Private Sub chkAllCreateDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllCreateDate.CheckedChanged
        If MyClass.chkAllCreateDate.Checked = True Then
            Me.cboFechaDesdeCreate.Enabled = False
            Me.cboFechaHastaCreate.Enabled = False
            Me.Label1.Enabled = False
            Me.Label2.Enabled = False
        Else
            Me.cboFechaDesdeCreate.Enabled = True
            Me.cboFechaHastaCreate.Enabled = True
            Me.Label1.Enabled = True
            Me.Label2.Enabled = True
        End If
    End Sub
    Private Sub chkAllScannedDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllScannedDate.CheckedChanged
        If MyClass.chkAllScannedDate.Checked = True Then
            Me.cboFechaDesdeScanned.Enabled = False
            Me.cboFechaHastaScanned.Enabled = False
            Me.Label3.Enabled = False
            Me.Label4.Enabled = False
        Else
            Me.cboFechaDesdeScanned.Enabled = True
            Me.cboFechaHastaScanned.Enabled = True
            Me.Label3.Enabled = True
            Me.Label4.Enabled = True
        End If
    End Sub
    Private Sub RadioButton2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            MyClass.GroupBox4.Enabled = False
        Else
            MyClass.GroupBox4.Enabled = True
        End If
    End Sub
#End Region

    Private Sub frmAllBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyClass.chkAllUsers.Checked = True
        MyClass.chkAllCreateDate.Checked = True
        MyClass.chkAllScannedDate.Checked = True
        LoadUsers()
    End Sub

    Private Sub LoadUsers()
        Try
            ' Dim sql As String = "select ID,NAME from usrtable order by name"
            ' Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Dim ds As DataSet = GetUsersDatasetOrderbyName
            MyClass.cboUsers.DataSource = ds.Tables(0)
            MyClass.cboUsers.DisplayMember = "NAME"
            MyClass.cboUsers.ValueMember = "ID"
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function GetUsersDatasetOrderbyName() As DataSet
        Dim ds As New DataSet
        Try
            Dim sql As String = "Select * from usrtable order by NAME"
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Catch
        End Try
        Return ds
    End Function

End Class
