Imports Zamba.AppBlock

Public Class SortButton
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
    Friend WithEvents Panel1 As ZWhitePanel
    Friend WithEvents Panel2 As ZWhitePanel
    Friend WithEvents Panel3 As ZWhitePanel
    Friend WithEvents Panel4 As ZWhitePanel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New ZWhitePanel
        Me.Panel2 = New ZWhitePanel
        Me.Panel3 = New ZWhitePanel
        Me.Panel4 = New ZWhitePanel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(214, Byte), CType(213, Byte), CType(217, Byte))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(26, 3)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(214, Byte), CType(213, Byte), CType(217, Byte))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(3, 23)
        Me.Panel2.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(214, Byte), CType(213, Byte), CType(217, Byte))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(23, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(3, 23)
        Me.Panel3.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(214, Byte), CType(213, Byte), CType(217, Byte))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(3, 21)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(20, 5)
        Me.Panel4.TabIndex = 1
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(20, 18)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'SortButton
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(214, Byte), CType(213, Byte), CType(217, Byte))
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "SortButton"
        Me.Size = New System.Drawing.Size(26, 26)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property Image() As Image
        Get
            Return Me.PictureBox1.Image
        End Get
        Set(ByVal Value As Image)
            Me.PictureBox1.Image = Value
        End Set
    End Property

    Public Event SortClick()

    Private Sub SortButton_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RemoveHandler Panel1.Click, AddressOf Clicks
        RemoveHandler Panel2.Click, AddressOf Clicks
        RemoveHandler Panel3.Click, AddressOf Clicks
        RemoveHandler Panel4.Click, AddressOf Clicks
        RemoveHandler PictureBox1.Click, AddressOf Clicks
        AddHandler Panel1.Click, AddressOf Clicks
        AddHandler Panel2.Click, AddressOf Clicks
        AddHandler Panel3.Click, AddressOf Clicks
        AddHandler Panel4.Click, AddressOf Clicks
        AddHandler PictureBox1.Click, AddressOf Clicks
    End Sub
    Private Sub Clicks(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SortClick()
    End Sub
End Class
