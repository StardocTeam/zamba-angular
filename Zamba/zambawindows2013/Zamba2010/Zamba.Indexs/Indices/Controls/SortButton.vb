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
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                If Panel1 IsNot Nothing Then
                    Panel1.Dispose()
                    Panel1 = Nothing
                End If
                If Panel2 IsNot Nothing Then
                    Panel2.Dispose()
                    Panel2 = Nothing
                End If
                If Panel3 IsNot Nothing Then
                    Panel3.Dispose()
                    Panel3 = Nothing
                End If
                If Panel4 IsNot Nothing Then
                    Panel4.Dispose()
                    Panel4 = Nothing
                End If
                If PictureBox1 IsNot Nothing Then
                    PictureBox1.Dispose()
                    PictureBox1 = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        Catch
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents Panel2 As ZPanel
    Friend WithEvents Panel3 As ZPanel
    Friend WithEvents Panel4 As ZPanel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Panel1 = New ZPanel
        Panel2 = New ZPanel
        Panel3 = New ZPanel
        Panel4 = New ZPanel
        PictureBox1 = New System.Windows.Forms.PictureBox
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.BackColor = Color.FromArgb(214, 213, 217)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(26, 3)
        Panel1.TabIndex = 0
        '
        'Panel2
        '
        Panel2.BackColor = Color.FromArgb(214, 213, 217)
        Panel2.Dock = DockStyle.Left
        Panel2.Location = New Point(0, 3)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(3, 23)
        Panel2.TabIndex = 1
        '
        'Panel3
        '
        Panel3.BackColor = Color.FromArgb(214, 213, 217)
        Panel3.Dock = DockStyle.Right
        Panel3.Location = New Point(23, 3)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(3, 23)
        Panel3.TabIndex = 1
        '
        'Panel4
        '
        Panel4.BackColor = Color.FromArgb(214, 213, 217)
        Panel4.Dock = DockStyle.Bottom
        Panel4.Location = New Point(3, 21)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(20, 5)
        Panel4.TabIndex = 1
        '
        'PictureBox1
        '
        PictureBox1.BorderStyle = BorderStyle.FixedSingle
        PictureBox1.Dock = DockStyle.Fill
        PictureBox1.Location = New Point(3, 3)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(20, 18)
        PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 2
        PictureBox1.TabStop = False
        '
        'SortButton
        '
        MyBase.BackColor = Color.FromArgb(214, 213, 217)
        Controls.Add(PictureBox1)
        Controls.Add(Panel4)
        Controls.Add(Panel3)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Name = "SortButton"
        Size = New Size(26, 26)
        ResumeLayout(False)

    End Sub

#End Region

    Public Property Image() As Image
        Get
            Return PictureBox1.Image
        End Get
        Set(ByVal Value As Image)
            PictureBox1.Image = Value
        End Set
    End Property

    Public Event SortClick()

    Private Sub SortButton_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
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
