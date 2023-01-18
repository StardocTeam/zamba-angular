Imports Zamba.Core
'Imports zamba.security
Imports Zamba.AdminControls



Public Class Sign
    Inherits BaseNote

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
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Panel2 As ZPanel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents LblTitle As ZLabel
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MnuSendToBack As System.Windows.Forms.MenuItem
    Friend WithEvents MnuBringToFront As System.Windows.Forms.MenuItem
    Friend WithEvents TxtText As TextBox
    Friend WithEvents PictureBoxSign As System.Windows.Forms.PictureBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Sign))
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
        PictureBoxSign = New System.Windows.Forms.PictureBox
        CType(PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        CType(PictureBoxSign, System.ComponentModel.ISupportInitialize).BeginInit()
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
        PictureBox1.Size = New System.Drawing.Size(204, 160)
        PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        '
        'ContextMenu1
        '
        ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {MenuItem1, MenuItem2, MenuItem9, MenuItem3, MnuSendToBack, MnuBringToFront})
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
        MenuItem9.Text = "Eliminar firma"
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
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        ImageList1.TransparentColor = System.Drawing.Color.Transparent
        ImageList1.Images.SetKeyName(0, "")
        ImageList1.Images.SetKeyName(1, "")
        '
        'Panel2
        '


        Panel2.ContextMenu = ContextMenu1
        Panel2.Controls.Add(LblTitle)
        Panel2.Controls.Add(Label1)
        Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Panel2.Location = New System.Drawing.Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New System.Drawing.Size(204, 32)
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
        LblTitle.Size = New System.Drawing.Size(204, 16)
        LblTitle.TabIndex = 8
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
        Label1.Size = New System.Drawing.Size(204, 16)
        Label1.TabIndex = 0
        '
        'Panel1
        '
        Panel1.BackColor = System.Drawing.Color.DarkGray
        Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle


        Panel1.ContextMenu = ContextMenu1
        Panel1.Controls.Add(TxtText)
        Panel1.Controls.Add(PictureBoxSign)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 32)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(204, 128)
        Panel1.TabIndex = 6
        Panel1.Visible = False
        '
        'TxtText
        '
        TxtText.BackColor = System.Drawing.Color.LightSkyBlue
        TxtText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        TxtText.ContextMenu = ContextMenu1
        TxtText.Dock = System.Windows.Forms.DockStyle.Fill
        TxtText.Font = New Font("Tahoma", 9.75!)
        TxtText.Location = New System.Drawing.Point(0, 0)
        TxtText.MaxLength = 255
        TxtText.Multiline = True
        TxtText.Name = "TxtText"
        TxtText.Size = New System.Drawing.Size(202, 126)
        TxtText.TabIndex = 33
        '
        'PictureBoxSign
        '
        PictureBoxSign.BackColor = System.Drawing.Color.LightSteelBlue
        PictureBoxSign.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        PictureBoxSign.Dock = System.Windows.Forms.DockStyle.Fill
        PictureBoxSign.Location = New System.Drawing.Point(0, 0)
        PictureBoxSign.Name = "PictureBoxSign"
        PictureBoxSign.Size = New System.Drawing.Size(202, 126)
        PictureBoxSign.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        PictureBoxSign.TabIndex = 32
        PictureBoxSign.TabStop = False
        '
        'Sign
        '
        AllowDrop = True
        BackColor = System.Drawing.Color.DarkGray
        ContextMenu = ContextMenu1
        Controls.Add(Panel1)
        Controls.Add(Panel2)
        Controls.Add(PictureBox1)
        Font = New Font("Tahoma", 8.25!, FontStyle.Bold)
        Name = "Sign"
        Size = New System.Drawing.Size(204, 160)
        CType(PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(PictureBoxSign, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

#End Region

    Private Sub Note_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            Mode = Modes.Small
            LblTitle.Text = title
            Label1.Text = NoteDate
            TxtText.Text = Notetext
            LoadPic()
            If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Notas, RightsType.Edit) Then
                TxtText.Enabled = True
            Else
                TxtText.Enabled = False
            End If
        Catch
        End Try
    End Sub
    Dim noteMode1 As Modes
    Dim Img As Image
    Private Sub LoadPic()
        If IO.File.Exists(SignPath) Then
            Img = Image.FromFile(SignPath)
            'Me.PictureBoxSign.Image = Img
            PictureBox1.Image = Img
        Else
            'Me.PictureBoxSign.Image = Nothing
            PictureBox1.Image = Nothing
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
    Public Property TitleBackGroundColor() As Color
        Get
            Return LblTitle.BackColor
        End Get
        Set(ByVal Value As Color)
            LblTitle.BackColor = Value
        End Set
    End Property

    Public Property Mode() As Modes
        Get
            Return noteMode1
        End Get
        Set(ByVal Value As Modes)
            noteMode1 = Value
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
        Try
            If IsMinimized = True Then

                Dim s As New Size
                s.Width = Img.Width '(30 * Img.Width) / Img.HorizontalResolution
                s.Height = Img.Height + 32 '(30 * Img.Height + 32) / Img.VerticalResolution
                Size = s

                LblTitle.Visible = True
                Label1.Visible = True
                Panel1.Visible = True
                Panel2.Visible = True
                IsMinimized = False
                PictureBox1.Visible = False
            Else
                'Me.Height = 35
                'Me.Width = 35
                Height = 70
                Width = 90
                LblTitle.Visible = False
                Label1.Visible = False
                Panel1.Visible = False
                Panel2.Visible = False
                IsMinimized = True
                PictureBox1.Visible = True
            End If
        Catch ex As Exception

        End Try
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
    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem1.Click
        Mode = Modes.Small
    End Sub
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem2.Click
        Mode = Modes.Large
    End Sub
    Public Event DestroySign(ByVal sign As Sign)
    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem9.Click
        If DirectCast(Membership.MembershipHelper.CurrentUser, IUser).ID = UserID Then
            Dim pw As New FrmAskPassword(DirectCast(Membership.MembershipHelper.CurrentUser, IUser).Password)
            If pw.ShowDialog() = DialogResult.OK Then
                Visible = False
                NotesBusiness.Delete(Id)
                RaiseEvent DestroySign(Me)
            End If
        End If
        Dispose()
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
                    If NotePosition.X > ParentsLeft + Parent.Width - 41 Then
                        NotePosition.X = ParentsLeft + Parent.Width - 41
                    End If
                    'Top
                    If NotePosition.Y < ParentsTop + 50 Then '+ 106 Then
                        NotePosition.Y = ParentsTop + 50 '+ 106
                    End If
                    'Bottom
                    If NotePosition.Y > ParentsTop + Parent.Height + 30 Then
                        NotePosition.Y = ParentsTop + Parent.Height + 30
                    End If

                Case WindowsOfNote.Preview

                    'Left
                    If NotePosition.X < ParentsLeft + 25 Then
                        NotePosition.X = ParentsLeft + 25
                    End If
                    'Right
                    If NotePosition.X > ParentsLeft + Parent.Width - 56 Then
                        NotePosition.X = ParentsLeft + Parent.Width - 56
                    End If
                    'Top
                    If NotePosition.Y < ParentsTop + 51 Then
                        NotePosition.Y = ParentsTop + 51
                    End If
                    'Bottom
                    If NotePosition.Y > ParentsTop + Parent.Height - 31 Then
                        NotePosition.Y = ParentsTop + Parent.Height - 31
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

    'Private Enum WindowsOfNote
    '    DockDocument
    '    DockFloat
    'End Enum
    'Para ventana float
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
    '                If NotePosition.X > ParentsLeft + Me.Parent.Width - 41 Then
    '                    NotePosition.X = ParentsLeft + Me.Parent.Width - 41
    '                End If

    '                'Top
    '                If NotePosition.Y < ParentsTop + 106 Then
    '                    NotePosition.Y = ParentsTop + 106
    '                End If

    '                'Bottom
    '                If NotePosition.Y > ParentsTop + Me.Parent.Height + 31 Then
    '                    NotePosition.Y = ParentsTop + Me.Parent.Height + 31
    '                End If

    '            Case WindowsOfNote.DockFloat
    '                'Left
    '                If NotePosition.X < ParentsLeft + 29 Then
    '                    NotePosition.X = ParentsLeft + 29
    '                End If

    '                'Right
    '                If NotePosition.X > ParentsLeft + Me.Parent.Width - 48 Then
    '                    NotePosition.X = ParentsLeft + Me.Parent.Width - 48
    '                End If

    '                'Top
    '                If NotePosition.Y < ParentsTop + 46 Then
    '                    NotePosition.Y = ParentsTop + 46
    '                End If

    '                'Bottom
    '                If NotePosition.Y > ParentsTop + Me.Parent.Height - 31 Then
    '                    NotePosition.Y = ParentsTop + Me.Parent.Height - 31
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
    'End Sub
#End Region

#Region "BringToFront/SendToBack"
    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MnuSendToBack.Click
        SendToBack()
    End Sub
    Private Sub MnuBringToFront_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MnuBringToFront.Click
        BringToFront()
    End Sub
#End Region

    Private Sub TxtText_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtText.TextChanged
        Notetext = TxtText.Text
    End Sub

    Private Sub TxtText_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles TxtText.Validated
        NotesBusiness.Save(Notetext, Location.X, Location.Y, Id)
    End Sub
End Class
