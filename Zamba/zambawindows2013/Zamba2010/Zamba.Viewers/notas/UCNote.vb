Imports Zamba.Core

Public Class Note
    Inherits basenote

#Region " Windows Form Designer generated code "

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
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Panel2 As ZPanel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents TxtText As TextBox
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents LblTitle As ZLabel
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MnuSendToBack As System.Windows.Forms.MenuItem
    Friend WithEvents MnuBringToFront As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Note))
        PictureBox1 = New System.Windows.Forms.PictureBox
        ContextMenu1 = New ContextMenu
        MenuItem1 = New System.Windows.Forms.MenuItem
        MenuItem2 = New System.Windows.Forms.MenuItem
        MenuItem9 = New System.Windows.Forms.MenuItem
        MenuItem3 = New System.Windows.Forms.MenuItem
        MnuSendToBack = New System.Windows.Forms.MenuItem
        MnuBringToFront = New System.Windows.Forms.MenuItem
        ImageList1 = New ImageList(components)
        Panel2 = New ZPanel
        LblTitle = New ZLabel
        Label1 = New ZLabel
        Panel1 = New ZPanel
        TxtText = New TextBox
        ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'PictureBox1
        '
        PictureBox1.BackColor = System.Drawing.Color.LightSteelBlue
        PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        PictureBox1.ContextMenu = ContextMenu1
        PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        PictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        PictureBox1.Location = New System.Drawing.Point(0, 0)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New System.Drawing.Size(248, 192)
        PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        '
        'ContextMenu1
        '
        ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {MenuItem1, MenuItem2, MenuItem9, MenuItem3, MnuSendToBack, MnuBringToFront})
        ContextMenu1.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        'MenuItem1
        '
        MenuItem1.Index = 0
        MenuItem1.Shortcut = System.Windows.Forms.Shortcut.F4
        MenuItem1.Text = "Minimizar"
        '
        'MenuItem2
        '
        MenuItem2.Index = 1
        MenuItem2.Text = "Maximizar"
        '
        'MenuItem9
        '
        MenuItem9.Index = 2
        MenuItem9.Text = "Eliminar nota"
        '
        'MenuItem3
        '
        MenuItem3.Index = 3
        MenuItem3.Text = "-"
        '
        'MnuSendToBack
        '
        MnuSendToBack.Index = 4
        MnuSendToBack.Text = "Enviar al Fondo"
        '
        'MnuBringToFront
        '
        MnuBringToFront.Index = 5
        MnuBringToFront.Text = "Traer al Frente"
        '
        'ImageList1
        '
        ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'Panel2
        '
        Panel2.ContextMenu = ContextMenu1
        Panel2.Controls.Add(LblTitle)
        Panel2.Controls.Add(Label1)
        Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Panel2.Location = New System.Drawing.Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New System.Drawing.Size(248, 32)
        Panel2.TabIndex = 7
        Panel2.Visible = False
        '
        'LblTitle
        '
        LblTitle.BackColor = System.Drawing.SystemColors.Menu
        LblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        LblTitle.ContextMenu = ContextMenu1
        LblTitle.Cursor = System.Windows.Forms.Cursors.Hand
        LblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        LblTitle.Font = New Font("Tahoma", 6.75!)
        LblTitle.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        LblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl
        LblTitle.Location = New System.Drawing.Point(0, 16)
        LblTitle.Name = "LblTitle"
        LblTitle.Size = New System.Drawing.Size(248, 16)
        LblTitle.TabIndex = 8
        LblTitle.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.SystemColors.Menu
        Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label1.ContextMenu = ContextMenu1
        Label1.Cursor = System.Windows.Forms.Cursors.Hand
        Label1.Dock = System.Windows.Forms.DockStyle.Top
        Label1.Font = New Font("Tahoma", 6.75!)
        Label1.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label1.Location = New System.Drawing.Point(0, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(248, 16)
        Label1.TabIndex = 0
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Panel1.BackColor = System.Drawing.Color.DarkGray
        Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel1.ContextMenu = ContextMenu1
        Panel1.Controls.Add(TxtText)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 32)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(248, 160)
        Panel1.TabIndex = 6
        Panel1.Visible = False
        '
        'TxtText
        '
        TxtText.BackColor = System.Drawing.Color.AntiqueWhite
        TxtText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        TxtText.ContextMenu = ContextMenu1
        TxtText.Dock = System.Windows.Forms.DockStyle.Fill
        TxtText.Font = New Font("Tahoma", 9.75!)
        TxtText.Location = New System.Drawing.Point(0, 0)
        TxtText.MaxLength = 255
        TxtText.Multiline = True
        TxtText.Name = "TxtText"
        TxtText.Size = New System.Drawing.Size(246, 158)
        TxtText.TabIndex = 5
        TxtText.Text = ""
        '
        'Note
        '
        AllowDrop = True
        BackColor = System.Drawing.Color.DarkGray
        ContextMenu = ContextMenu1
        Controls.Add(Panel1)
        Controls.Add(Panel2)
        Controls.Add(PictureBox1)
        Font = New Font("Tahoma", 8.25!, FontStyle.Bold)
        Name = "Note"
        Size = New System.Drawing.Size(248, 192)
        Panel2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Private Sub Note_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        'Me.Mode = Mode.Small
        Mode = Modes.Small
        '  Container_Form = sender
        'Me.PictureBox1.Anchor = AnchorStyles.Left
        'Me.PictureBox1.Dock = DockStyle.Fill
        'Me.PictureBox1.Width = Me.Width
        'Me.PictureBox1.Height = Me.Height
        'Me.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        'Me.PictureBox1.Image = Me.ImageList1.Images(1)
        '  oldX = Me.Location.X
        ' oldY = Me.Location.Y
        LblTitle.Text = title
        Label1.Text = NoteDate
        TxtText.Text = Notetext
        Try
            ToolTip1.SetToolTip(PictureBox1, Notetext.Trim)
        Catch ex As Exception
        End Try
        If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Notas, RightsType.Edit) Then
            TxtText.Enabled = True
        Else
            TxtText.Enabled = False
        End If
    End Sub

#Region "Public Properties"
    Public Property TitleColor() As Color
        Get
            Return LblTitle.ForeColor
        End Get
        Set(ByVal Value As Color)
            LblTitle.ForeColor = Value
        End Set
    End Property
    Public Property TextColor() As Color
        Get
            Return TxtText.ForeColor
        End Get
        Set(ByVal Value As Color)
            TxtText.ForeColor = Value
        End Set
    End Property
    Public Property TitleBackGroundColor() As Color
        Get
            Return LblTitle.BackColor
        End Get
        Set(ByVal Value As Color)
            LblTitle.BackColor = Value
        End Set
    End Property
    Public Property TextBackGroundColor() As Color
        Get
            Return TxtText.BackColor
        End Get
        Set(ByVal Value As Color)
            TxtText.BackColor = Value
        End Set
    End Property
    Private _Mode As Modes
    Public Property Mode() As Modes
        Get
            Return _Mode
        End Get
        Set(ByVal Value As Modes)
            _Mode = Value
            If Value = Modes.Large Then
                IsMinimized = True
                ResizeNote()
            Else
                IsMinimized = False
                ResizeNote()
            End If
        End Set
    End Property
#End Region

#Region "ReziseNote"
    Dim minimized1 As Boolean
    Private Property IsMinimized() As Boolean
        Get
            Return minimized1
        End Get
        Set(ByVal Value As Boolean)
            minimized1 = Value
        End Set
    End Property
    Private Sub ResizeNote()
        If IsMinimized Then
            Height = 170
            Width = 210
            LblTitle.Visible = True
            TxtText.Visible = True
            Label1.Visible = True
            Panel1.Visible = True
            Panel2.Visible = True
            IsMinimized = False
            PictureBox1.Visible = False
        Else
            Height = 30
            Width = 30
            LblTitle.Visible = False
            TxtText.Visible = False
            Label1.Visible = False
            Panel1.Visible = False
            Panel2.Visible = False
            IsMinimized = True
            PictureBox1.Visible = True
            'NotesFactory.Save(Me) 'Agregado para guardar al cerrar
        End If
    End Sub
    Private Sub Label1_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles Label1.DoubleClick
        ResizeNote()
    End Sub
    Private Sub LblTitle_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles LblTitle.DoubleClick
        ResizeNote()
    End Sub
    Private Sub PictureBox1_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles PictureBox1.DoubleClick
        ResizeNote()
    End Sub
#End Region

#Region "ContextMenu"
    Private Sub TxtText_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TxtText.TextChanged
        If Not IsLoading Then
            Edited = True
        End If
        Notetext = TxtText.Text
        Text = TxtText.Text
    End Sub
    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem1.Click
        Mode = Modes.Small
    End Sub
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem2.Click
        Mode = Modes.Large
    End Sub
    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem9.Click
        If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Notas, RightsType.Delete) Then
            If MessageBox.Show("Esta seguro que desea eliminar la nota?", "Zamba Software - Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Visible = False
                NotesBusiness.Delete(Id)
            End If
        End If
    End Sub
#End Region

#Region "Drag"
    Dim dragging As Boolean
    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = MouseButtons.Left Then
            dragging = True
        End If
    End Sub
    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        If dragging Then
            dragging = False
            NotesBusiness.Save(Notetext, Location.X, Location.Y, Id)
        End If
    End Sub

    Private Enum WindowsOfNote
        DockDocument
        Preview
    End Enum
    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If dragging Then

            Dim NotePosition As New Drawing.Point
            NotePosition.X = Cursor.Position.X
            NotePosition.Y = Cursor.Position.Y


            Dim ParentsLeft As Int32
            Dim ParentsTop As Int32
            Dim WindowsOfNote As WindowsOfNote = WindowsOfNote.DockDocument

            'Sumo Left y Top de los controles padres.
            Try
                ParentsLeft = _
                    Parent.Left _
                    + Parent.Parent.Left _
                    + Parent.Parent.Parent.Left _
                    + Parent.Parent.Parent.Parent.Left
                '5
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Left()
                Else
                    WindowsOfNote = WindowsOfNote.Preview
                    Exit Try
                End If
                '6
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '7
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
                Else
                    Exit Try
                End If
                '8
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '9
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '10
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '11
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '12
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '13
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '14
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                End If
                '15
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '16
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
                Else
                    Exit Try
                End If
                '17
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
                Else
                    Exit Try
                End If
                '18
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
                Else
                    Exit Try
                End If
                '19
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
                Else
                    Exit Try
                End If
            Catch
            End Try

            '---------------
            Try
                ParentsTop = _
                Parent.Top _
                + Parent.Parent.Top _
                + Parent.Parent.Parent.Top _
                + Parent.Parent.Parent.Parent.Top

                '5
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Top()
                Else
                    Exit Try
                End If
                '6
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '7
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '8
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '9
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '10
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '11
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '12
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '13
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If

                '14
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '15
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '16
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top()
                Else
                    Exit Try
                End If
                '17
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top()
                Else
                    Exit Try
                End If
                '18
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top()
                Else
                    Exit Try
                End If
                '19
                If Not IsNothing(Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top()
                Else
                    Exit Try
                End If
            Catch
            End Try

            Select Case WindowsOfNote
                Case WindowsOfNote.DockDocument
                    'Left
                    If NotePosition.X < ParentsLeft + 10 Then '+ 35 Then
                        NotePosition.X = ParentsLeft + 10 '+ 35
                    End If
                    'Right
                    If NotePosition.X > ParentsLeft + Parent.Width - 36 Then
                        NotePosition.X = ParentsLeft + Parent.Width - 36
                    End If
                    'Top
                    If NotePosition.Y < ParentsTop + 50 Then '+ 106 Then
                        NotePosition.Y = ParentsTop + 50 '+ 106
                    End If
                    'Bottom
                    If NotePosition.Y > ParentsTop + Parent.Height + 35 Then
                        NotePosition.Y = ParentsTop + Parent.Height + 35
                    End If

                Case WindowsOfNote.Preview
                    'Left
                    If NotePosition.X < ParentsLeft + 25 Then
                        NotePosition.X = ParentsLeft + 25
                    End If
                    'Right
                    If NotePosition.X > ParentsLeft + Parent.Width - 51 Then
                        NotePosition.X = ParentsLeft + Parent.Width - 51
                    End If
                    'Top
                    If NotePosition.Y < ParentsTop + 51 Then
                        NotePosition.Y = ParentsTop + 51
                    End If
                    'Bottom
                    If NotePosition.Y > ParentsTop + Parent.Height - 26 Then
                        NotePosition.Y = ParentsTop + Parent.Height - 26
                    End If


            End Select



            Location = Parent.PointToClient(NotePosition)

            'borders adjust 
            '            -       
            '            +
            '  -  +              -  +
            '            -
            '            +
            '

        End If
    End Sub


    'Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
    '    If dragging Then

    '        Dim NotePosition As New Drawing.Point
    '        NotePosition.X = Cursor.Position.X
    '        NotePosition.Y = Cursor.Position.Y


    '        Dim ParentsLeft As Int32
    '        Dim ParentsTop As Int32
    '        Dim WindowsOfNote As WindowsOfNote = WindowsOfNote.DockDocument

    '        'Sumo Left y Top de los controles padres.
    '        Try
    '            ParentsLeft = _
    '                Me.Parent.Left _
    '                + Me.Parent.Parent.Left _
    '                + Me.Parent.Parent.Parent.Left _
    '                + Me.Parent.Parent.Parent.Parent.Left
    '            '5
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Left()
    '            Else
    '                Exit Try
    '            End If
    '            '6
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Left
    '            Else
    '                Exit Try
    '            End If
    '            '7
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
    '            Else
    '                Exit Try
    '            End If
    '            '8
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
    '            Else
    '                Exit Try
    '            End If
    '            '9
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
    '            Else
    '                WindowsOfNote = WindowsOfNote.DockFloat
    '                Exit Try
    '            End If
    '            '10
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
    '            Else
    '                Exit Try
    '            End If
    '            '11
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
    '            Else
    '                Exit Try
    '            End If
    '            '12
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
    '            Else
    '                Exit Try
    '            End If
    '            '13
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
    '            Else
    '                Exit Try
    '            End If
    '            '14
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
    '            End If
    '            '15
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
    '            Else
    '                Exit Try
    '            End If
    '            '16
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
    '            Else
    '                Exit Try
    '            End If
    '            '17
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
    '            Else
    '                Exit Try
    '            End If
    '            '18
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
    '            Else
    '                Exit Try
    '            End If
    '            '19
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
    '            Else
    '                Exit Try
    '            End If
    '        Catch
    '        End Try

    '        '---------------
    '        Try
    '            ParentsTop = _
    '            Me.Parent.Top _
    '            + Me.Parent.Parent.Top _
    '            + Me.Parent.Parent.Parent.Top _
    '            + Me.Parent.Parent.Parent.Parent.Top

    '            '5
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Top()
    '            Else
    '                Exit Try
    '            End If
    '            '6
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Top
    '            Else
    '                Exit Try
    '            End If
    '            '7
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
    '            Else
    '                Exit Try
    '            End If
    '            '8
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
    '            Else
    '                Exit Try
    '            End If
    '            '9
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
    '            Else
    '                Exit Try
    '            End If
    '            '10
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
    '            Else
    '                Exit Try
    '            End If
    '            '11
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
    '            Else
    '                Exit Try
    '            End If
    '            '12
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
    '            Else
    '                Exit Try
    '            End If
    '            '13
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
    '            Else
    '                Exit Try
    '            End If

    '            '14
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
    '            Else
    '                Exit Try
    '            End If
    '            '15
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
    '            Else
    '                Exit Try
    '            End If
    '            '16
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top()
    '            Else
    '                Exit Try
    '            End If
    '            '17
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top()
    '            Else
    '                Exit Try
    '            End If
    '            '18
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top()
    '            Else
    '                Exit Try
    '            End If
    '            '19
    '            If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
    '                ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top()
    '            Else
    '                Exit Try
    '            End If
    '        Catch
    '        End Try

    '        Select Case WindowsOfNote
    '            Case WindowsOfNote.DockDocument
    '                'Left
    '                If NotePosition.X < ParentsLeft + 35 Then
    '                    NotePosition.X = ParentsLeft + 35
    '                End If

    '                'Right
    '                If NotePosition.X > ParentsLeft + Me.Parent.Width - 40 Then
    '                    NotePosition.X = ParentsLeft + Me.Parent.Width - 40
    '                End If

    '                'Top
    '                If NotePosition.Y < ParentsTop + 106 Then
    '                    NotePosition.Y = ParentsTop + 106
    '                End If

    '                'Bottom
    '                If NotePosition.Y > ParentsTop + Me.Parent.Height + 32 Then
    '                    NotePosition.Y = ParentsTop + Me.Parent.Height + 32
    '                End If

    '            Case WindowsOfNote.DockFloat
    '                'Left
    '                If NotePosition.X < ParentsLeft + 29 Then
    '                    NotePosition.X = ParentsLeft + 29
    '                End If

    '                'Right
    '                If NotePosition.X > ParentsLeft + Me.Parent.Width - 46 Then
    '                    NotePosition.X = ParentsLeft + Me.Parent.Width - 46
    '                End If

    '                'Top
    '                If NotePosition.Y < ParentsTop + 46 Then
    '                    NotePosition.Y = ParentsTop + 46
    '                End If

    '                'Bottom
    '                If NotePosition.Y > ParentsTop + Me.Parent.Height - 29 Then
    '                    NotePosition.Y = ParentsTop + Me.Parent.Height - 29
    '                End If
    '        End Select

    '        Me.Location = Me.Parent.PointToClient(NotePosition)

    '        'borders adjust 
    '        '            -       
    '        '            +
    '        '  -  +              -  +
    '        '            -
    '        '            +
    '        '

    '    End If
#End Region

#Region "Txt"
    Private Sub TxtText_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles TxtText.Validated
        If Edited Then
            NotesBusiness.Save(Notetext, Location.X, Location.Y, Id)
            Try
                ToolTip1.SetToolTip(PictureBox1, Notetext.Trim)
            Catch ex As Exception
            End Try
        End If
    End Sub
#End Region

#Region "BringToFront/SendToBack"
    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MnuSendToBack.Click
        SendToBack()
    End Sub
    Private Sub MnuBringToFront_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MnuBringToFront.Click
        BringToFront()
    End Sub
#End Region

End Class
