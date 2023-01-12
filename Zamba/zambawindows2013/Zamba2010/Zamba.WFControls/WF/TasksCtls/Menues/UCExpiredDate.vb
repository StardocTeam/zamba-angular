Imports ZAMBA.Core
Public Class UCExpiredDate
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
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents BtnChangeExpireDate As ZButton
    Friend WithEvents lblExpiredate As ZLabel
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Panel1 = New ZPanel
        BtnChangeExpireDate = New ZButton
        Label1 = New ZLabel
        lblExpiredate = New ZLabel
        DateTimePicker2 = New System.Windows.Forms.DateTimePicker
        DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.BackColor = System.Drawing.SystemColors.Control
        Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel1.Controls.Add(lblExpiredate)
        Panel1.Controls.Add(DateTimePicker2)
        Panel1.Controls.Add(DateTimePicker1)
        Panel1.Controls.Add(BtnChangeExpireDate)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(244, 128)
        Panel1.TabIndex = 1
        '
        'BtnChangeExpireDate
        '
        BtnChangeExpireDate.BackColor = System.Drawing.Color.Transparent
        BtnChangeExpireDate.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        BtnChangeExpireDate.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        BtnChangeExpireDate.Location = New System.Drawing.Point(48, 84)
        BtnChangeExpireDate.Name = "BtnChangeExpireDate"
        BtnChangeExpireDate.Size = New System.Drawing.Size(144, 24)
        BtnChangeExpireDate.TabIndex = 20
        BtnChangeExpireDate.Text = "Renovar "
        '
        'Label1
        '
        Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Cursor = System.Windows.Forms.Cursors.Hand
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(224, 4)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(15, 19)
        Label1.TabIndex = 0
        Label1.Text = "X"
        '
        'lblExpiredate
        '
        lblExpiredate.BackColor = System.Drawing.Color.Transparent
        lblExpiredate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lblExpiredate.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblExpiredate.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        lblExpiredate.Location = New System.Drawing.Point(16, 28)
        lblExpiredate.Name = "lblExpiredate"
        lblExpiredate.Size = New System.Drawing.Size(180, 20)
        lblExpiredate.TabIndex = 36
        lblExpiredate.TextAlign = ContentAlignment.MiddleLeft
        '
        'DateTimePicker2
        '
        DateTimePicker2.CalendarMonthBackground = System.Drawing.Color.SkyBlue
        DateTimePicker2.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time
        DateTimePicker2.Location = New System.Drawing.Point(16, 48)
        DateTimePicker2.Name = "DateTimePicker2"
        DateTimePicker2.ShowUpDown = True
        DateTimePicker2.Size = New System.Drawing.Size(104, 22)
        DateTimePicker2.TabIndex = 35
        DateTimePicker2.Value = New Date(2005, 10, 28, 0, 0, 0, 0)
        '
        'DateTimePicker1
        '
        DateTimePicker1.CalendarMonthBackground = System.Drawing.Color.SkyBlue
        DateTimePicker1.CalendarTitleBackColor = System.Drawing.Color.Gainsboro
        DateTimePicker1.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        DateTimePicker1.Location = New System.Drawing.Point(196, 28)
        DateTimePicker1.Name = "DateTimePicker1"
        DateTimePicker1.Size = New System.Drawing.Size(24, 22)
        DateTimePicker1.TabIndex = 34
        '
        'UCExpiredDate
        '
        Controls.Add(Panel1)
        Name = "UCExpiredDate"
        Size = New System.Drawing.Size(244, 128)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Dim Result As TaskResult
    Sub New(ByRef Result As TaskResult)
        MyBase.New()
        InitializeComponent()
        Try
            Me.Result = Result
            LoadUCExpiredDate()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

#Region "Load"
    Private Sub LoadUCExpiredDate()
        Try
            If Not Result.ExpireDate = #12:00:00 AM# Then
                DateTimePicker1.Value = Result.ExpireDate
                lblExpiredate.Text = DateTimePicker1.Value.Date.ToLongDateString
                DateTimePicker2.Value = Result.ExpireDate
            End If
            RemoveHandler DateTimePicker1.ValueChanged, AddressOf DateTimePicker1_ValueChanged
            AddHandler DateTimePicker1.ValueChanged, AddressOf DateTimePicker1_ValueChanged
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Class UserItem
        Inherits ListViewItem
        Public User As iuser
        Sub New(ByVal User As iuser)
            MyBase.new()
            Me.User = User
            Text = User.Name
        End Sub
    End Class
#End Region

#Region "Eventos"
    Private Sub DateTimePicker1_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
        lblExpiredate.Text = DateTimePicker1.Value.ToLongDateString
    End Sub
    Public Event ChangeExpireDate(ByRef Result As TaskResult, ByVal NewDate As DateTime)
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnChangeExpireDate.Click
        Try
            Dim NewDate As DateTime = New DateTime(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, DateTimePicker1.Value.Day, DateTimePicker2.Value.Hour, DateTimePicker2.Value.Minute, DateTimePicker2.Value.Second)
            If NewDate <> Result.ExpireDate Then
                RaiseEvent ChangeExpireDate(Result, NewDate)
            End If
            Dispose()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Close"
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Label1.Click
        Try
            Dispose()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub UcDerivar_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Leave
        Try
            Dispose()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region

End Class

