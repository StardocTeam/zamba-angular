Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports Zamba.AppBlock
Imports Zamba.Core
Imports zamba.data

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
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Panel2 As zbluepanel
    Friend WithEvents Label1 As System.windows.forms.Label
    Friend WithEvents Panel1 As zbluepanel
    Friend WithEvents TxtText As System.Windows.Forms.TextBox
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents LblTitle As System.windows.forms.Label
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MnuSendToBack As System.Windows.Forms.MenuItem
    Friend WithEvents MnuBringToFront As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Note))
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
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
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
        Me.PictureBox1.Size = New System.Drawing.Size(248, 192)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem9, Me.MenuItem3, Me.MnuSendToBack, Me.MnuBringToFront})
        Me.ContextMenu1.RightToLeft = System.Windows.Forms.RightToLeft.No
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
        Me.MenuItem9.Text = "Eliminar nota"
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
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'Panel2
        '
        Me.Panel2.ContextMenu = Me.ContextMenu1
        Me.Panel2.Controls.Add(Me.LblTitle)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(248, 32)
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
        Me.LblTitle.Size = New System.Drawing.Size(248, 16)
        Me.LblTitle.TabIndex = 8
        Me.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.Label1.Size = New System.Drawing.Size(248, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DarkGray
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.ContextMenu = Me.ContextMenu1
        Me.Panel1.Controls.Add(Me.TxtText)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(248, 160)
        Me.Panel1.TabIndex = 6
        Me.Panel1.Visible = False
        '
        'TxtText
        '
        Me.TxtText.BackColor = System.Drawing.Color.AntiqueWhite
        Me.TxtText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtText.ContextMenu = Me.ContextMenu1
        Me.TxtText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtText.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.TxtText.Location = New System.Drawing.Point(0, 0)
        Me.TxtText.MaxLength = 255
        Me.TxtText.Multiline = True
        Me.TxtText.Name = "TxtText"
        Me.TxtText.Size = New System.Drawing.Size(246, 158)
        Me.TxtText.TabIndex = 5
        Me.TxtText.Text = ""
        '
        'Note
        '
        Me.AllowDrop = True
        Me.BackColor = System.Drawing.Color.DarkGray
        Me.ContextMenu = Me.ContextMenu1
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Name = "Note"
        Me.Size = New System.Drawing.Size(248, 192)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Private Sub Note_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.Mode = Mode.Small
        Me.Mode = Modes.Small
        '  Container_Form = sender
        'Me.PictureBox1.Anchor = AnchorStyles.Left
        'Me.PictureBox1.Dock = DockStyle.Fill
        'Me.PictureBox1.Width = Me.Width
        'Me.PictureBox1.Height = Me.Height
        'Me.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        'Me.PictureBox1.Image = Me.ImageList1.Images(1)
        '  oldX = Me.Location.X
        ' oldY = Me.Location.Y
        Me.LblTitle.Text = Me.title
        Me.Label1.Text = Me.NoteDate
        TxtText.Text = Me.Notetext
        Try
            Me.ToolTip1.SetToolTip(Me.PictureBox1, Me.Notetext.Trim)
        Catch ex As Exception
        End Try
        If UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.Notas, Zamba.Core.RightsType.Edit) Then
            Me.TxtText.Enabled = True
        Else
            Me.TxtText.Enabled = False
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
    Public Property TextColor() As Color
        Get
            Return Me.TxtText.ForeColor
        End Get
        Set(ByVal Value As Color)
            Me.TxtText.ForeColor = Value
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
    Public Property TextBackGroundColor() As Color
        Get
            Return Me.TxtText.BackColor
        End Get
        Set(ByVal Value As Color)
            Me.TxtText.BackColor = Value
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
        If IsMinimized Then
            Me.Height = 170
            Me.Width = 210
            Me.LblTitle.Visible = True
            Me.TxtText.Visible = True
            Me.Label1.Visible = True
            Me.Panel1.Visible = True
            Me.Panel2.Visible = True
            IsMinimized = False
            Me.PictureBox1.Visible = False
        Else
            Me.Height = 30
            Me.Width = 30
            Me.LblTitle.Visible = False
            Me.TxtText.Visible = False
            Me.Label1.Visible = False
            Me.Panel1.Visible = False
            Me.Panel2.Visible = False
            IsMinimized = True
            Me.PictureBox1.Visible = True
            'NotesFactory.Save(Me) 'Agregado para guardar al cerrar
        End If
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
    Private Sub TxtText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtText.TextChanged
        If Not IsLoading Then
            Me.Edited = True
        End If
        Me.Notetext = Me.TxtText.Text
        Me.Text = Me.TxtText.Text
    End Sub
    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        Mode = Modes.Small
    End Sub
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        Mode = Modes.Large
    End Sub
    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem9.Click
        If UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.Notas, Zamba.Core.RightsType.Delete) Then
            If MessageBox.Show("Esta seguro que desea eliminar la nota?", "Zamba Software - Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Visible = False
                NotesBusiness.Delete(Me.Id)
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
                    If NotePosition.X > ParentsLeft + Me.Parent.Width - 36 Then
                        NotePosition.X = ParentsLeft + Me.Parent.Width - 36
                    End If
                    'Top
                    If NotePosition.Y < ParentsTop + 50 Then '+ 106 Then
                        NotePosition.Y = ParentsTop + 50 '+ 106
                    End If
                    'Bottom
                    If NotePosition.Y > ParentsTop + Me.Parent.Height + 35 Then
                        NotePosition.Y = ParentsTop + Me.Parent.Height + 35
                    End If

                Case WindowsOfNote.Preview
                    'Left
                    If NotePosition.X < ParentsLeft + 25 Then
                        NotePosition.X = ParentsLeft + 25
                    End If
                    'Right
                    If NotePosition.X > ParentsLeft + Me.Parent.Width - 51 Then
                        NotePosition.X = ParentsLeft + Me.Parent.Width - 51
                    End If
                    'Top
                    If NotePosition.Y < ParentsTop + 51 Then
                        NotePosition.Y = ParentsTop + 51
                    End If
                    'Bottom
                    If NotePosition.Y > ParentsTop + Me.Parent.Height - 26 Then
                        NotePosition.Y = ParentsTop + Me.Parent.Height - 26
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
    Private Sub TxtText_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtText.Validated
        If Edited Then
            NotesBusiness.Save(Me.Notetext, Me.Location.X, Me.Location.Y, Me.Id)
            Try
                Me.ToolTip1.SetToolTip(Me.PictureBox1, Me.Notetext.Trim)
            Catch ex As Exception
            End Try
        End If
    End Sub
#End Region

#Region "BringToFront/SendToBack"
    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuSendToBack.Click
        Me.SendToBack()
    End Sub
    Private Sub MnuBringToFront_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuBringToFront.Click
        Me.BringToFront()
    End Sub
#End Region

End Class
