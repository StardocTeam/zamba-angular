Imports Zamba.Servers
Imports System.Windows.Forms
Imports Zamba.AppBlock
Imports zamba.core
Public Class frmUSrFRecha
    Inherits System.Windows.Forms.Form

    Private MePuedoCerrar As Boolean = True
    Public Event Aceptar(ByVal FechaDesde As Date, ByVal FechaHasta As Date, ByVal UsrId As Int32)
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
    Friend WithEvents ZBluePanel1 As Zamba.AppBlock.ZBluePanel
    Friend WithEvents ZButton1 As Zamba.AppBlock.ZButton
    Friend WithEvents ZBluePanel2 As Zamba.AppBlock.ZBluePanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents FechaHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents FechaDesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents Cbousers As System.Windows.Forms.ComboBox
    Friend WithEvents cboxFiltrarUsuario As System.Windows.Forms.CheckBox
    Friend WithEvents ZBPUsuario As Zamba.AppBlock.ZBluePanel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ZBluePanel1 = New Zamba.AppBlock.ZBluePanel
        Me.ZBPUsuario = New Zamba.AppBlock.ZBluePanel
        Me.Cbousers = New System.Windows.Forms.ComboBox
        Me.ZBluePanel2 = New Zamba.AppBlock.ZBluePanel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.FechaHasta = New System.Windows.Forms.DateTimePicker
        Me.FechaDesde = New System.Windows.Forms.DateTimePicker
        Me.ZButton1 = New Zamba.AppBlock.ZButton
        Me.cboxFiltrarUsuario = New System.Windows.Forms.CheckBox
        Me.ZBluePanel1.SuspendLayout()
        Me.ZBPUsuario.SuspendLayout()
        Me.ZBluePanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ZBluePanel1
        '
        Me.ZBluePanel1.Controls.Add(Me.cboxFiltrarUsuario)
        Me.ZBluePanel1.Controls.Add(Me.ZBPUsuario)
        Me.ZBluePanel1.Controls.Add(Me.ZBluePanel2)
        Me.ZBluePanel1.Controls.Add(Me.ZButton1)
        Me.ZBluePanel1.Location = New System.Drawing.Point(0, 0)
        Me.ZBluePanel1.Name = "ZBluePanel1"
        Me.ZBluePanel1.Size = New System.Drawing.Size(360, 216)
        Me.ZBluePanel1.TabIndex = 0
        '
        'ZBPUsuario
        '
        Me.ZBPUsuario.Controls.Add(Me.Cbousers)
        Me.ZBPUsuario.Enabled = False
        Me.ZBPUsuario.Location = New System.Drawing.Point(16, 128)
        Me.ZBPUsuario.Name = "ZBPUsuario"
        Me.ZBPUsuario.Size = New System.Drawing.Size(328, 48)
        Me.ZBPUsuario.TabIndex = 2
        '
        'Cbousers
        '
        Me.Cbousers.BackColor = System.Drawing.Color.White
        Me.Cbousers.Location = New System.Drawing.Point(8, 16)
        Me.Cbousers.Name = "Cbousers"
        Me.Cbousers.Size = New System.Drawing.Size(312, 21)
        Me.Cbousers.TabIndex = 1
        '
        'ZBluePanel2
        '
        Me.ZBluePanel2.Controls.Add(Me.Label2)
        Me.ZBluePanel2.Controls.Add(Me.Label1)
        Me.ZBluePanel2.Controls.Add(Me.FechaHasta)
        Me.ZBluePanel2.Controls.Add(Me.FechaDesde)
        Me.ZBluePanel2.Location = New System.Drawing.Point(16, 16)
        Me.ZBluePanel2.Name = "ZBluePanel2"
        Me.ZBluePanel2.Size = New System.Drawing.Size(328, 72)
        Me.ZBluePanel2.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(8, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 23)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Fecha hasta"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 23)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Fecha desde"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FechaHasta
        '
        Me.FechaHasta.CalendarMonthBackground = System.Drawing.Color.White
        Me.FechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.FechaHasta.Location = New System.Drawing.Point(120, 40)
        Me.FechaHasta.Name = "FechaHasta"
        Me.FechaHasta.TabIndex = 5
        '
        'FechaDesde
        '
        Me.FechaDesde.CalendarMonthBackground = System.Drawing.Color.White
        Me.FechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.FechaDesde.Location = New System.Drawing.Point(120, 8)
        Me.FechaDesde.Name = "FechaDesde"
        Me.FechaDesde.TabIndex = 4
        '
        'ZButton1
        '
        Me.ZButton1.DialogResult = System.Windows.Forms.DialogResult.None
        Me.ZButton1.Location = New System.Drawing.Point(240, 184)
        Me.ZButton1.Name = "ZButton1"
        Me.ZButton1.Size = New System.Drawing.Size(104, 24)
        Me.ZButton1.TabIndex = 0
        Me.ZButton1.Text = "Aceptar"
        '
        'cboxFiltrarUsuario
        '
        Me.cboxFiltrarUsuario.BackColor = System.Drawing.Color.Transparent
        Me.cboxFiltrarUsuario.Location = New System.Drawing.Point(16, 104)
        Me.cboxFiltrarUsuario.Name = "cboxFiltrarUsuario"
        Me.cboxFiltrarUsuario.Size = New System.Drawing.Size(144, 16)
        Me.cboxFiltrarUsuario.TabIndex = 3
        Me.cboxFiltrarUsuario.Text = "Discriminar por usuario"
        '
        'frmUSrFRecha
        '
        Me.ClientSize = New System.Drawing.Size(360, 214)
        Me.Controls.Add(Me.ZBluePanel1)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(368, 248)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(368, 248)
        Me.Name = "frmUSrFRecha"
        Me.Text = "frmUSrFRecha"
        Me.ZBluePanel1.ResumeLayout(False)
        Me.ZBPUsuario.ResumeLayout(False)
        Me.ZBluePanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmUSrFRecha_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim ds As DataSet = GetUsersDatasetOrderbyName
            Cbousers.DataSource = ds.Tables(0)
            Cbousers.DisplayMember = "NAME"
            Cbousers.ValueMember = "ID"
        Catch ex As Exception
            ZException.Log(ex, False)
            MessageBox.Show("Error al cargar la lista de usuarios", "Error en usuarios")
            MePuedoCerrar = False
            Me.Close()
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

    Private Sub frmUSrFRecha_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

    End Sub

    Private Sub frmUSrFRecha_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        e.Cancel() = MePuedoCerrar
    End Sub

    Private Sub ZButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZButton1.Click
        If cboxFiltrarUsuario.Checked = True Then
            RaiseEvent Aceptar(FechaDesde.Text, FechaHasta.Text, Cbousers.SelectedValue)
        Else
            RaiseEvent Aceptar(FechaDesde.Text, FechaHasta.Text, 0)
        End If
        MePuedoCerrar = False
        Me.Close()
    End Sub

    Private Sub ZBluePanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles ZBluePanel1.Paint

    End Sub

    Private Sub cboxFiltrarUsuario_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboxFiltrarUsuario.CheckedChanged
        If cboxFiltrarUsuario.Checked = True Then
            ZBPUsuario.Enabled = True
        Else
            ZBPUsuario.Enabled = False
        End If
    End Sub
End Class
