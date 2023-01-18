Public Class UCWFNote
    Inherits Zamba.AppBlock.ZControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl1 overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ContextMenu1 As ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents Panel2 As System.windows.forms.Panel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TxtText As System.Windows.Forms.Panel
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents LblTitle As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(UCWFNote))
        PictureBox1 = New System.Windows.Forms.PictureBox
        ContextMenu1 = New ContextMenu
        MenuItem1 = New System.Windows.Forms.MenuItem
        MenuItem2 = New System.Windows.Forms.MenuItem
        MenuItem3 = New System.Windows.Forms.MenuItem
        MenuItem5 = New System.Windows.Forms.MenuItem
        MenuItem6 = New System.Windows.Forms.MenuItem
        MenuItem7 = New System.Windows.Forms.MenuItem
        MenuItem8 = New System.Windows.Forms.MenuItem
        MenuItem9 = New System.Windows.Forms.MenuItem
        Panel2 = New System.Windows.Forms.Panel
        LblTitle = New ZLabel
        Label1 = New ZLabel
        Panel1 = New System.Windows.Forms.Panel
        TxtText = New System.Windows.Forms.Panel
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'PictureBox1
        '
        PictureBox1.AccessibleDescription = resources.GetString("PictureBox1.AccessibleDescription")
        PictureBox1.AccessibleName = resources.GetString("PictureBox1.AccessibleName")
        PictureBox1.Anchor = CType(resources.GetObject("PictureBox1.Anchor"), System.Windows.Forms.AnchorStyles)
        PictureBox1.BackColor = System.Drawing.Color.SlateGray
        PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        PictureBox1.ContextMenu = ContextMenu1
        PictureBox1.Dock = CType(resources.GetObject("PictureBox1.Dock"), System.Windows.Forms.DockStyle)
        PictureBox1.Enabled = CType(resources.GetObject("PictureBox1.Enabled"), Boolean)
        PictureBox1.Font = CType(resources.GetObject("PictureBox1.Font"), Font)
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        PictureBox1.ImeMode = CType(resources.GetObject("PictureBox1.ImeMode"), System.Windows.Forms.ImeMode)
        PictureBox1.Location = CType(resources.GetObject("PictureBox1.Location"), System.Drawing.Point)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.RightToLeft = CType(resources.GetObject("PictureBox1.RightToLeft"), System.Windows.Forms.RightToLeft)
        PictureBox1.Size = CType(resources.GetObject("PictureBox1.Size"), System.Drawing.Size)
        PictureBox1.SizeMode = CType(resources.GetObject("PictureBox1.SizeMode"), System.Windows.Forms.PictureBoxSizeMode)
        PictureBox1.TabIndex = CInt(resources.GetObject("PictureBox1.TabIndex"))
        PictureBox1.TabStop = False
        PictureBox1.Text = resources.GetString("PictureBox1.Text")
        PictureBox1.Visible = CType(resources.GetObject("PictureBox1.Visible"), Boolean)
        '
        'ContextMenu1
        '
        ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {MenuItem1, MenuItem2, MenuItem3, MenuItem9})
        ContextMenu1.RightToLeft = CType(resources.GetObject("ContextMenu1.RightToLeft"), System.Windows.Forms.RightToLeft)
        '
        'MenuItem1
        '
        MenuItem1.Enabled = CType(resources.GetObject("MenuItem1.Enabled"), Boolean)
        MenuItem1.Index = 0
        MenuItem1.Shortcut = CType(resources.GetObject("MenuItem1.Shortcut"), System.Windows.Forms.Shortcut)
        MenuItem1.ShowShortcut = CType(resources.GetObject("MenuItem1.ShowShortcut"), Boolean)
        MenuItem1.Text = resources.GetString("MenuItem1.Text")
        MenuItem1.Visible = CType(resources.GetObject("MenuItem1.Visible"), Boolean)
        '
        'MenuItem2
        '
        MenuItem2.Enabled = CType(resources.GetObject("MenuItem2.Enabled"), Boolean)
        MenuItem2.Index = 1
        MenuItem2.Shortcut = CType(resources.GetObject("MenuItem2.Shortcut"), System.Windows.Forms.Shortcut)
        MenuItem2.ShowShortcut = CType(resources.GetObject("MenuItem2.ShowShortcut"), Boolean)
        MenuItem2.Text = resources.GetString("MenuItem2.Text")
        MenuItem2.Visible = CType(resources.GetObject("MenuItem2.Visible"), Boolean)
        '
        'MenuItem3
        '
        MenuItem3.Enabled = CType(resources.GetObject("MenuItem3.Enabled"), Boolean)
        MenuItem3.Index = 2
        MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {MenuItem5, MenuItem6, MenuItem7, MenuItem8})
        MenuItem3.Shortcut = CType(resources.GetObject("MenuItem3.Shortcut"), System.Windows.Forms.Shortcut)
        MenuItem3.ShowShortcut = CType(resources.GetObject("MenuItem3.ShowShortcut"), Boolean)
        MenuItem3.Text = resources.GetString("MenuItem3.Text")
        MenuItem3.Visible = CType(resources.GetObject("MenuItem3.Visible"), Boolean)
        '
        'MenuItem5
        '
        MenuItem5.Enabled = CType(resources.GetObject("MenuItem5.Enabled"), Boolean)
        MenuItem5.Index = 0
        MenuItem5.Shortcut = CType(resources.GetObject("MenuItem5.Shortcut"), System.Windows.Forms.Shortcut)
        MenuItem5.ShowShortcut = CType(resources.GetObject("MenuItem5.ShowShortcut"), Boolean)
        MenuItem5.Text = resources.GetString("MenuItem5.Text")
        MenuItem5.Visible = CType(resources.GetObject("MenuItem5.Visible"), Boolean)
        '
        'MenuItem6
        '
        MenuItem6.Enabled = CType(resources.GetObject("MenuItem6.Enabled"), Boolean)
        MenuItem6.Index = 1
        MenuItem6.Shortcut = CType(resources.GetObject("MenuItem6.Shortcut"), System.Windows.Forms.Shortcut)
        MenuItem6.ShowShortcut = CType(resources.GetObject("MenuItem6.ShowShortcut"), Boolean)
        MenuItem6.Text = resources.GetString("MenuItem6.Text")
        MenuItem6.Visible = CType(resources.GetObject("MenuItem6.Visible"), Boolean)
        '
        'MenuItem7
        '
        MenuItem7.Enabled = CType(resources.GetObject("MenuItem7.Enabled"), Boolean)
        MenuItem7.Index = 2
        MenuItem7.Shortcut = CType(resources.GetObject("MenuItem7.Shortcut"), System.Windows.Forms.Shortcut)
        MenuItem7.ShowShortcut = CType(resources.GetObject("MenuItem7.ShowShortcut"), Boolean)
        MenuItem7.Text = resources.GetString("MenuItem7.Text")
        MenuItem7.Visible = CType(resources.GetObject("MenuItem7.Visible"), Boolean)
        '
        'MenuItem8
        '
        MenuItem8.Enabled = CType(resources.GetObject("MenuItem8.Enabled"), Boolean)
        MenuItem8.Index = 3
        MenuItem8.Shortcut = CType(resources.GetObject("MenuItem8.Shortcut"), System.Windows.Forms.Shortcut)
        MenuItem8.ShowShortcut = CType(resources.GetObject("MenuItem8.ShowShortcut"), Boolean)
        MenuItem8.Text = resources.GetString("MenuItem8.Text")
        MenuItem8.Visible = CType(resources.GetObject("MenuItem8.Visible"), Boolean)
        '
        'MenuItem9
        '
        MenuItem9.Enabled = CType(resources.GetObject("MenuItem9.Enabled"), Boolean)
        MenuItem9.Index = 3
        MenuItem9.Shortcut = CType(resources.GetObject("MenuItem9.Shortcut"), System.Windows.Forms.Shortcut)
        MenuItem9.ShowShortcut = CType(resources.GetObject("MenuItem9.ShowShortcut"), Boolean)
        MenuItem9.Text = resources.GetString("MenuItem9.Text")
        MenuItem9.Visible = CType(resources.GetObject("MenuItem9.Visible"), Boolean)
        '
        'Panel2
        '
        Panel2.AccessibleDescription = resources.GetString("Panel2.AccessibleDescription")
        Panel2.AccessibleName = resources.GetString("Panel2.AccessibleName")
        Panel2.Anchor = CType(resources.GetObject("Panel2.Anchor"), System.Windows.Forms.AnchorStyles)
        Panel2.AutoScroll = CType(resources.GetObject("Panel2.AutoScroll"), Boolean)
        Panel2.AutoScrollMargin = CType(resources.GetObject("Panel2.AutoScrollMargin"), System.Drawing.Size)
        Panel2.AutoScrollMinSize = CType(resources.GetObject("Panel2.AutoScrollMinSize"), System.Drawing.Size)
        Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Panel2.ContextMenu = ContextMenu1
        Panel2.Controls.Add(LblTitle)
        Panel2.Controls.Add(Label1)
        Panel2.Dock = CType(resources.GetObject("Panel2.Dock"), System.Windows.Forms.DockStyle)
        Panel2.Enabled = CType(resources.GetObject("Panel2.Enabled"), Boolean)
        Panel2.Font = CType(resources.GetObject("Panel2.Font"), Font)
        Panel2.ImeMode = CType(resources.GetObject("Panel2.ImeMode"), System.Windows.Forms.ImeMode)
        Panel2.Location = CType(resources.GetObject("Panel2.Location"), System.Drawing.Point)
        Panel2.Name = "Panel2"
        Panel2.RightToLeft = CType(resources.GetObject("Panel2.RightToLeft"), System.Windows.Forms.RightToLeft)
        Panel2.Size = CType(resources.GetObject("Panel2.Size"), System.Drawing.Size)
        Panel2.TabIndex = CInt(resources.GetObject("Panel2.TabIndex"))
        Panel2.Text = resources.GetString("Panel2.Text")
        Panel2.Visible = CType(resources.GetObject("Panel2.Visible"), Boolean)
        '
        'LblTitle
        '
        LblTitle.AccessibleDescription = resources.GetString("LblTitle.AccessibleDescription")
        LblTitle.AccessibleName = resources.GetString("LblTitle.AccessibleName")
        LblTitle.Anchor = CType(resources.GetObject("LblTitle.Anchor"), System.Windows.Forms.AnchorStyles)
        LblTitle.AutoSize = CType(resources.GetObject("LblTitle.AutoSize"), Boolean)
        LblTitle.BackColor = System.Drawing.SystemColors.Menu
        LblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        LblTitle.ContextMenu = ContextMenu1
        LblTitle.Dock = CType(resources.GetObject("LblTitle.Dock"), System.Windows.Forms.DockStyle)
        LblTitle.Enabled = CType(resources.GetObject("LblTitle.Enabled"), Boolean)
        LblTitle.Font = CType(resources.GetObject("LblTitle.Font"), Font)
        LblTitle.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        LblTitle.Image = CType(resources.GetObject("LblTitle.Image"), System.Drawing.Image)
        LblTitle.ImageAlign = CType(resources.GetObject("LblTitle.ImageAlign"), System.Drawing.ContentAlignment)
        LblTitle.ImageIndex = CInt(resources.GetObject("LblTitle.ImageIndex"))
        LblTitle.ImeMode = CType(resources.GetObject("LblTitle.ImeMode"), System.Windows.Forms.ImeMode)
        LblTitle.Location = CType(resources.GetObject("LblTitle.Location"), System.Drawing.Point)
        LblTitle.Name = "LblTitle"
        LblTitle.RightToLeft = CType(resources.GetObject("LblTitle.RightToLeft"), System.Windows.Forms.RightToLeft)
        LblTitle.Size = CType(resources.GetObject("LblTitle.Size"), System.Drawing.Size)
        LblTitle.TabIndex = CInt(resources.GetObject("LblTitle.TabIndex"))
        LblTitle.Text = resources.GetString("LblTitle.Text")
        LblTitle.TextAlign = CType(resources.GetObject("LblTitle.TextAlign"), System.Drawing.ContentAlignment)
        LblTitle.Visible = CType(resources.GetObject("LblTitle.Visible"), Boolean)
        '
        'Label1
        '
        Label1.AccessibleDescription = resources.GetString("Label1.AccessibleDescription")
        Label1.AccessibleName = resources.GetString("Label1.AccessibleName")
        Label1.Anchor = CType(resources.GetObject("Label1.Anchor"), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = CType(resources.GetObject("Label1.AutoSize"), Boolean)
        Label1.BackColor = System.Drawing.SystemColors.Menu
        Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label1.ContextMenu = ContextMenu1
        Label1.Dock = CType(resources.GetObject("Label1.Dock"), System.Windows.Forms.DockStyle)
        Label1.Enabled = CType(resources.GetObject("Label1.Enabled"), Boolean)
        Label1.Font = CType(resources.GetObject("Label1.Font"), Font)
        Label1.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label1.Image = CType(resources.GetObject("Label1.Image"), System.Drawing.Image)
        Label1.ImageAlign = CType(resources.GetObject("Label1.ImageAlign"), System.Drawing.ContentAlignment)
        Label1.ImageIndex = CInt(resources.GetObject("Label1.ImageIndex"))
        Label1.ImeMode = CType(resources.GetObject("Label1.ImeMode"), System.Windows.Forms.ImeMode)
        Label1.Location = CType(resources.GetObject("Label1.Location"), System.Drawing.Point)
        Label1.Name = "Label1"
        Label1.RightToLeft = CType(resources.GetObject("Label1.RightToLeft"), System.Windows.Forms.RightToLeft)
        Label1.Size = CType(resources.GetObject("Label1.Size"), System.Drawing.Size)
        Label1.TabIndex = CInt(resources.GetObject("Label1.TabIndex"))
        Label1.Text = resources.GetString("Label1.Text")
        Label1.TextAlign = CType(resources.GetObject("Label1.TextAlign"), System.Drawing.ContentAlignment)
        Label1.Visible = CType(resources.GetObject("Label1.Visible"), Boolean)
        '
        'Panel1
        '
        Panel1.AccessibleDescription = resources.GetString("Panel1.AccessibleDescription")
        Panel1.AccessibleName = resources.GetString("Panel1.AccessibleName")
        Panel1.Anchor = CType(resources.GetObject("Panel1.Anchor"), System.Windows.Forms.AnchorStyles)
        Panel1.AutoScroll = CType(resources.GetObject("Panel1.AutoScroll"), Boolean)
        Panel1.AutoScrollMargin = CType(resources.GetObject("Panel1.AutoScrollMargin"), System.Drawing.Size)
        Panel1.AutoScrollMinSize = CType(resources.GetObject("Panel1.AutoScrollMinSize"), System.Drawing.Size)
        Panel1.BackColor = System.Drawing.Color.DarkGray
        Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel1.ContextMenu = ContextMenu1
        Panel1.Controls.Add(TxtText)
        Panel1.Dock = CType(resources.GetObject("Panel1.Dock"), System.Windows.Forms.DockStyle)
        Panel1.Enabled = CType(resources.GetObject("Panel1.Enabled"), Boolean)
        Panel1.Font = CType(resources.GetObject("Panel1.Font"), Font)
        Panel1.ImeMode = CType(resources.GetObject("Panel1.ImeMode"), System.Windows.Forms.ImeMode)
        Panel1.Location = CType(resources.GetObject("Panel1.Location"), System.Drawing.Point)
        Panel1.Name = "Panel1"
        Panel1.RightToLeft = CType(resources.GetObject("Panel1.RightToLeft"), System.Windows.Forms.RightToLeft)
        Panel1.Size = CType(resources.GetObject("Panel1.Size"), System.Drawing.Size)
        Panel1.TabIndex = CInt(resources.GetObject("Panel1.TabIndex"))
        Panel1.Text = resources.GetString("Panel1.Text")
        Panel1.Visible = CType(resources.GetObject("Panel1.Visible"), Boolean)
        '
        'TxtText
        '
        TxtText.AccessibleDescription = resources.GetString("TxtText.AccessibleDescription")
        TxtText.AccessibleName = resources.GetString("TxtText.AccessibleName")
        TxtText.Anchor = CType(resources.GetObject("TxtText.Anchor"), System.Windows.Forms.AnchorStyles)
        TxtText.BackColor = System.Drawing.Color.AntiqueWhite
        TxtText.BackgroundImage = CType(resources.GetObject("TxtText.BackgroundImage"), System.Drawing.Image)
        TxtText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        TxtText.ContextMenu = ContextMenu1
        TxtText.Dock = CType(resources.GetObject("TxtText.Dock"), System.Windows.Forms.DockStyle)
        TxtText.Enabled = CType(resources.GetObject("TxtText.Enabled"), Boolean)
        TxtText.Font = CType(resources.GetObject("TxtText.Font"), Font)
        TxtText.ImeMode = CType(resources.GetObject("TxtText.ImeMode"), System.Windows.Forms.ImeMode)
        TxtText.Location = CType(resources.GetObject("TxtText.Location"), System.Drawing.Point)
        TxtText.Name = "TxtText"
        TxtText.RightToLeft = CType(resources.GetObject("TxtText.RightToLeft"), System.Windows.Forms.RightToLeft)
        TxtText.Size = CType(resources.GetObject("TxtText.Size"), System.Drawing.Size)
        TxtText.TabIndex = CInt(resources.GetObject("TxtText.TabIndex"))
        TxtText.Text = resources.GetString("TxtText.Text")
        TxtText.Visible = CType(resources.GetObject("TxtText.Visible"), Boolean)
        '
        'Note
        '
        AccessibleDescription = resources.GetString("$this.AccessibleDescription")
        AccessibleName = resources.GetString("$this.AccessibleName")
        AllowDrop = True
        AutoScroll = CType(resources.GetObject("$this.AutoScroll"), Boolean)
        AutoScrollMargin = CType(resources.GetObject("$this.AutoScrollMargin"), System.Drawing.Size)
        AutoScrollMinSize = CType(resources.GetObject("$this.AutoScrollMinSize"), System.Drawing.Size)
        BackColor = System.Drawing.Color.DarkGray
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        ContextMenu = ContextMenu1
        Controls.Add(Panel1)
        Controls.Add(Panel2)
        Controls.Add(PictureBox1)
        Enabled = CType(resources.GetObject("$this.Enabled"), Boolean)
        Font = CType(resources.GetObject("$this.Font"), Font)
        ImeMode = CType(resources.GetObject("$this.ImeMode"), System.Windows.Forms.ImeMode)
        Location = CType(resources.GetObject("$this.Location"), System.Drawing.Point)
        Name = "UCWFNote"
        RightToLeft = CType(resources.GetObject("$this.RightToLeft"), System.Windows.Forms.RightToLeft)
        Size = CType(resources.GetObject("$this.Size"), System.Drawing.Size)
        Panel2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region
    'Dim _title As String
    'TODO Falta hacer validacion interna de las fechas
    ' Dim _Container_Form As Object
    Dim _LocationX As Integer
    Dim _LocationY As Integer
    Dim _OriginalHeight As Integer
    Dim _OriginalWidth As Integer
    Dim _oldX As Integer
    Dim _oldY As Integer
    Public Property OriginalX() As Int32
        Get
            Return _oldX
        End Get
        Set(ByVal Value As Int32)
            _oldX = Value
        End Set
    End Property
    Public Property OriginalY() As Int32
        Get
            Return _oldY
        End Get
        Set(ByVal Value As Int32)
            _oldY = Value
        End Set
    End Property
    Private _edited As Boolean
    Public Property Edited() As Boolean
        Get
            Return _edited
        End Get
        Set(ByVal Value As Boolean)
            _edited = Value
        End Set
    End Property
    Private _flagloading As Boolean = True
    Public Property IsLoading() As Boolean
        Get
            Return _flagloading
        End Get
        Set(ByVal Value As Boolean)
            _flagloading = Value
        End Set
    End Property
    Public Property ActualX() As Integer
        Get
            Return _LocationX
        End Get
        Set(ByVal Value As Integer)
            _LocationX = Value
        End Set
    End Property
    Public Property ActualY() As Integer
        Get
            Return _LocationY
        End Get
        Set(ByVal Value As Integer)
            _LocationY = Value
        End Set
    End Property
    Public Property OriginalHeight() As Integer
        Get
            Return _OriginalHeight
        End Get
        Set(ByVal Value As Integer)
            _OriginalHeight = Value
        End Set
    End Property
    Public Property OriginalWidth() As Integer
        Get
            Return _OriginalWidth
        End Get
        Set(ByVal Value As Integer)
            _OriginalWidth = Value
        End Set
    End Property

    Public Property Title() As String
        Get
            Return LblTitle.Text.Trim
        End Get
        Set(ByVal Value As String)
            LblTitle.Text = Value.Trim
        End Set
    End Property
    Private Sub Note_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        ' _Container_Form = sender
        _oldX = Location.X
        _oldY = Location.Y
        ResizeNote()
    End Sub
    Private Property IsMinimized() As Boolean
        Get
            Return minimized1
        End Get
        Set(ByVal Value As Boolean)
            minimized1 = Value
        End Set
    End Property
    Dim minimized1 As Boolean
    Private Sub ResizeNote()
        If IsMinimized = True Then
            Height = 230
            Width = 540
            LblTitle.Visible = True
            TxtText.Visible = True
            Label1.Visible = True
            Panel1.Visible = True
            Panel2.Visible = True
            IsMinimized = False
            PictureBox1.Visible = False
        Else
            Height = 40
            Width = 40
            LblTitle.Visible = False
            TxtText.Visible = False
            Label1.Visible = False
            Panel1.Visible = False
            Panel2.Visible = False
            IsMinimized = True
            PictureBox1.Visible = True
        End If
    End Sub

    Enum Modes
        Small
        Large
    End Enum

    Dim _mode As Modes = Modes.Small
    Public Property Mode() As Modes
        Get
            Return _mode
        End Get
        Set(ByVal Value As Modes)
            _mode = Value
            If Value = Modes.Large Then
                IsMinimized = True
                ResizeNote()
            Else
                IsMinimized = False
                ResizeNote()
            End If
        End Set
    End Property

    Private Sub LblTitle_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles LblTitle.DoubleClick
        ResizeNote()
    End Sub
    Private Sub PictureBox1_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles PictureBox1.DoubleClick
        ResizeNote()
    End Sub
    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PictureBox1.Click
        ResizeNote()
    End Sub
    Private Sub TxtText_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles TxtText.DoubleClick
        ResizeNote()
    End Sub
    Private Sub Note_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.DoubleClick
        ResizeNote()
    End Sub
    Private Sub Note_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Click
        ResizeNote()
    End Sub
    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem1.Click
        Mode = Modes.Small
    End Sub
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem2.Click
        Mode = Modes.Large
    End Sub
    Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem3.Click
        DoDragDrop(Me, DragDropEffects.Move)
    End Sub
    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem5.Click
        Left = Left + 15
    End Sub
    Private Sub MenuItem6_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem6.Click
        Left = Left - 15
    End Sub
    Private Sub MenuItem7_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem7.Click
        Top = Top - 15
    End Sub
    Private Sub MenuItem8_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem8.Click
        Top = Top + 15
    End Sub
    Private Sub LblTitle_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles LblTitle.Click
        ResizeNote()
    End Sub
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Label1.Click
        ResizeNote()
    End Sub
    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem9.Click
        Visible = False
        '     NotesFactory.Delete(Me)
    End Sub

    Dim dragging As Boolean

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = MouseButtons.Left Then
            dragging = True
        End If
    End Sub
    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If dragging Then
            Visible = True
            'move control to new position
            Dim MPosition As New Point
            MPosition.Offset(ActualX, ActualY)

            MPosition.X = CInt(Cursor.Position.X)
            MPosition.Y = CInt(Cursor.Position.Y)
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'esto anda bien''''''''''''''''''''''''''
            If MPosition.X < ParentForm.Left Then
                MPosition.X = ParentForm.Left
            End If
            If MPosition.X > ParentForm.Left + ActualX Then
                MPosition.X = ParentForm.Left + ActualX
            End If
            ''''''''''''''''''''''''''''''''''''''''

            'este esto bien'''''''''''''''''''''''
            If MPosition.Y < ParentForm.Top Then
                MPosition.Y = ParentForm.Top
            End If
            If MPosition.Y > (ParentForm.Top + ActualY) Then
                MPosition.Y = ParentForm.Top + ActualY
            End If
            Refresh()
            '''''''''''''''''''''''''''''''''''''''
            'Me.ToolTip1.SetToolTip(Me.PictureBox1, MPosition.X & "," & MPosition.Y)
            Location = ParentForm.PointToClient(MPosition)
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'ensure control cannot leave container
            Refresh()
        End If
    End Sub
    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        If dragging Then
            'end the dragging
            dragging = False
            '   NotesFactory.Save(Me)
        End If
    End Sub
    Private Sub Note_LocationChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.LocationChanged
        If IsLoading = False Then Edited = True
    End Sub

    Private Sub TxtText_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles TxtText.Validated
        '   If Edited = True Then NotesFactory.Save(Me)
    End Sub
End Class
