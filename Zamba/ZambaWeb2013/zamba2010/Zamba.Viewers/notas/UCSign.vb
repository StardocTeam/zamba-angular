Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports Zamba.AppBlock
Imports Zamba.Core
Imports zamba.data
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
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Panel2 As zbluepanel
    Friend WithEvents Label1 As System.windows.forms.Label
    Friend WithEvents Panel1 As zbluepanel
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents LblTitle As System.windows.forms.Label
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MnuSendToBack As System.Windows.Forms.MenuItem
    Friend WithEvents MnuBringToFront As System.Windows.Forms.MenuItem
    Friend WithEvents TxtText As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxSign As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Sign))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItem9 = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.MnuSendToBack = New System.Windows.Forms.MenuItem
        Me.MnuBringToFront = New System.Windows.Forms.MenuItem
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel2 = New Zamba.AppBlock.ZBluePanel
        Me.LblTitle = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1 = New Zamba.AppBlock.ZBluePanel
        Me.TxtText = New System.Windows.Forms.TextBox
        Me.PictureBoxSign = New System.Windows.Forms.PictureBox
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBoxSign, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox1.ContextMenu = Me.ContextMenu1
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(204, 160)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem9, Me.MenuItem3, Me.MnuSendToBack, Me.MnuBringToFront})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Shortcut = System.Windows.Forms.Shortcut.F4
        Me.MenuItem1.Text = "Minimizar"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.Text = "Maximizar"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 2
        Me.MenuItem9.Text = "Eliminar firma"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 3
        Me.MenuItem3.Text = "-"
        '
        'MnuSendToBack
        '
        Me.MnuSendToBack.Index = 4
        Me.MnuSendToBack.Text = "Enviar al Fondo"
        '
        'MnuBringToFront
        '
        Me.MnuBringToFront.Index = 5
        Me.MnuBringToFront.Text = "Traer al Frente"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        '
        'Panel2
        '
        Me.Panel2.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel2.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel2.ContextMenu = Me.ContextMenu1
        Me.Panel2.Controls.Add(Me.LblTitle)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(204, 32)
        Me.Panel2.TabIndex = 7
        Me.Panel2.Visible = False
        '
        'LblTitle
        '
        Me.LblTitle.BackColor = System.Drawing.SystemColors.Menu
        Me.LblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblTitle.ContextMenu = Me.ContextMenu1
        Me.LblTitle.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblTitle.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.LblTitle.ForeColor = System.Drawing.Color.Black
        Me.LblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LblTitle.Location = New System.Drawing.Point(0, 16)
        Me.LblTitle.Name = "LblTitle"
        Me.LblTitle.Size = New System.Drawing.Size(204, 16)
        Me.LblTitle.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Menu
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.ContextMenu = Me.ContextMenu1
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(204, 16)
        Me.Label1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DarkGray
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel1.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel1.ContextMenu = Me.ContextMenu1
        Me.Panel1.Controls.Add(Me.TxtText)
        Me.Panel1.Controls.Add(Me.PictureBoxSign)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(204, 128)
        Me.Panel1.TabIndex = 6
        Me.Panel1.Visible = False
        '
        'TxtText
        '
        Me.TxtText.BackColor = System.Drawing.Color.LightSkyBlue
        Me.TxtText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtText.ContextMenu = Me.ContextMenu1
        Me.TxtText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtText.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.TxtText.Location = New System.Drawing.Point(0, 0)
        Me.TxtText.MaxLength = 255
        Me.TxtText.Multiline = True
        Me.TxtText.Name = "TxtText"
        Me.TxtText.Size = New System.Drawing.Size(202, 126)
        Me.TxtText.TabIndex = 33
        '
        'PictureBoxSign
        '
        Me.PictureBoxSign.BackColor = System.Drawing.Color.LightSteelBlue
        Me.PictureBoxSign.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxSign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBoxSign.Location = New System.Drawing.Point(0, 0)
        Me.PictureBoxSign.Name = "PictureBoxSign"
        Me.PictureBoxSign.Size = New System.Drawing.Size(202, 126)
        Me.PictureBoxSign.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxSign.TabIndex = 32
        Me.PictureBoxSign.TabStop = False
        '
        'Sign
        '
        Me.AllowDrop = True
        Me.BackColor = System.Drawing.Color.DarkGray
        Me.ContextMenu = Me.ContextMenu1
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Name = "Sign"
        Me.Size = New System.Drawing.Size(204, 160)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBoxSign, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Note_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Mode = Modes.Small
            Me.LblTitle.Text = Me.title
            Me.Label1.Text = Me.NoteDate
            Me.TxtText.Text = Me.Notetext
            LoadPic()
            If UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.Notas, Zamba.Core.RightsType.Edit) Then
                Me.TxtText.Enabled = True
            Else
                Me.TxtText.Enabled = False
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
            Me.PictureBox1.Image = Img
        Else
            'Me.PictureBoxSign.Image = Nothing
            Me.PictureBox1.Image = Nothing
        End If
    End Sub

#Region "Public Properties"
    Public Property TitleColor() As Color
        Get
            Return Me.LblTitle.ForeColor
        End Get
        Set(ByVal Value As Color)
            Me.LblTitle.ForeColor = Value
        End Set
    End Property
    Public Property TitleBackGroundColor() As Color
        Get
            Return Me.LblTitle.BackColor
        End Get
        Set(ByVal Value As Color)
            Me.LblTitle.BackColor = Value
        End Set
    End Property

    Public Property Mode() As Modes
        Get
            Return noteMode1
        End Get
        Set(ByVal Value As Modes)
            noteMode1 = Value
            If Value = Modes.Large Then
                Me.IsMinimized = True
                Me.ResizeNote()
            Else
                Me.IsMinimized = False
                Me.ResizeNote()
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
                Me.Size = s

                Me.LblTitle.Visible = True
                Me.Label1.Visible = True
                Me.Panel1.Visible = True
                Me.Panel2.Visible = True
                IsMinimized = False
                Me.PictureBox1.Visible = False
            Else
                'Me.Height = 35
                'Me.Width = 35
                Me.Height = 70
                Me.Width = 90
                Me.LblTitle.Visible = False
                Me.Label1.Visible = False
                Me.Panel1.Visible = False
                Me.Panel2.Visible = False
                IsMinimized = True
                Me.PictureBox1.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Label1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label1.DoubleClick
        ResizeNote()
    End Sub
    Private Sub LblTitle_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblTitle.DoubleClick
        ResizeNote()
    End Sub
    Private Sub PictureBox1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        ResizeNote()
    End Sub
#End Region

#Region "ContextMenu"
    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        Mode = Modes.Small
    End Sub
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        Mode = Modes.Large
    End Sub
    Public Event DestroySign(ByVal sign As Sign)
    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem9.Click
        If DirectCast(UserBusiness.CurrentUser, IUser).ID = Me.UserID Then
            Dim pw As New FrmAskPassword(DirectCast(UserBusiness.CurrentUser, IUser).Password)
            If pw.ShowDialog() = DialogResult.OK Then
                Me.Visible = False
                NotesBusiness.Delete(Me.Id)
                RaiseEvent DestroySign(Me)
            End If
        End If
        Me.Dispose()
    End Sub
#End Region

#Region "Drag"
    Dim dragging As Boolean
    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            dragging = True
        End If
    End Sub
    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        If dragging Then
            dragging = False
            NotesBusiness.Save(Me.Notetext, Me.Location.X, Me.Location.Y, Me.Id)
        End If
    End Sub
    Private Enum WindowsOfNote
        DockDocument
        Preview
    End Enum
    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If dragging Then

            Dim NotePosition As New Drawing.Point
            NotePosition.X = Windows.Forms.Cursor.Position.X
            NotePosition.Y = Windows.Forms.Cursor.Position.Y


            Dim ParentsLeft As Int32
            Dim ParentsTop As Int32
            Dim WindowsOfNote As WindowsOfNote = WindowsOfNote.DockDocument

            'Sumo Left y Top de los controles padres.
            Try
                ParentsLeft = _
                    Me.Parent.Left _
                    + Me.Parent.Parent.Left _
                    + Me.Parent.Parent.Parent.Left _
                    + Me.Parent.Parent.Parent.Parent.Left
                '5
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Left()
                Else
                    WindowsOfNote = WindowsOfNote.Preview
                    Exit Try
                End If
                '6
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '7
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
                Else
                    Exit Try
                End If
                '8
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '9
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '10
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '11
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '12
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '13
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '14
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                End If
                '15
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left
                Else
                    Exit Try
                End If
                '16
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
                Else
                    Exit Try
                End If
                '17
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
                Else
                    Exit Try
                End If
                '18
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
                Else
                    Exit Try
                End If
                '19
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsLeft += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Left()
                Else
                    Exit Try
                End If
            Catch
            End Try

            '---------------
            Try
                ParentsTop = _
                Me.Parent.Top _
                + Me.Parent.Parent.Top _
                + Me.Parent.Parent.Parent.Top _
                + Me.Parent.Parent.Parent.Parent.Top

                '5
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Top()
                Else
                    Exit Try
                End If
                '6
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '7
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '8
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '9
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '10
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '11
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '12
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '13
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If

                '14
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '15
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top
                Else
                    Exit Try
                End If
                '16
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top()
                Else
                    Exit Try
                End If
                '17
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top()
                Else
                    Exit Try
                End If
                '18
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top()
                Else
                    Exit Try
                End If
                '19
                If Not IsNothing(Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent) Then
                    ParentsTop += Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Top()
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
                    If NotePosition.X > ParentsLeft + Me.Parent.Width - 41 Then
                        NotePosition.X = ParentsLeft + Me.Parent.Width - 41
                    End If
                    'Top
                    If NotePosition.Y < ParentsTop + 50 Then '+ 106 Then
                        NotePosition.Y = ParentsTop + 50 '+ 106
                    End If
                    'Bottom
                    If NotePosition.Y > ParentsTop + Me.Parent.Height + 30 Then
                        NotePosition.Y = ParentsTop + Me.Parent.Height + 30
                    End If

                Case WindowsOfNote.Preview

                    'Left
                    If NotePosition.X < ParentsLeft + 25 Then
                        NotePosition.X = ParentsLeft + 25
                    End If
                    'Right
                    If NotePosition.X > ParentsLeft + Me.Parent.Width - 56 Then
                        NotePosition.X = ParentsLeft + Me.Parent.Width - 56
                    End If
                    'Top
                    If NotePosition.Y < ParentsTop + 51 Then
                        NotePosition.Y = ParentsTop + 51
                    End If
                    'Bottom
                    If NotePosition.Y > ParentsTop + Me.Parent.Height - 31 Then
                        NotePosition.Y = ParentsTop + Me.Parent.Height - 31
                    End If

            End Select



            Me.Location = Me.Parent.PointToClient(NotePosition)

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
    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuSendToBack.Click
        Me.SendToBack()
    End Sub
    Private Sub MnuBringToFront_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuBringToFront.Click
        Me.BringToFront()
    End Sub
#End Region

    Private Sub TxtText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtText.TextChanged
        Me.Notetext = Me.TxtText.Text
    End Sub

    Private Sub TxtText_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtText.Validated
        NotesBusiness.Save(Me.Notetext, Me.Location.X, Me.Location.Y, Me.Id)
    End Sub
End Class
