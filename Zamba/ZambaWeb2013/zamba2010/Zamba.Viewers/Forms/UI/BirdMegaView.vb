Public Class BirdMegaView
    Inherits Zamba.AppBlock.ZForm
    Dim NewImage As Image
    Dim DiferenciaTop As Int32
    Dim DiferenciaLeft As Int32
    Dim PrevioTop As Int32
    Dim PrevioLeft As Int32

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByVal NewImage As Image)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Me.NewImage = NewImage
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
    Friend WithEvents PictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents Lupa As System.Windows.Forms.PictureBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Lupa = New System.Windows.Forms.PictureBox
        PictureBox = New System.Windows.Forms.PictureBox
        SuspendLayout()
        '
        'Lupa
        '
        Lupa.BackColor = System.Drawing.Color.Transparent
        Lupa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Lupa.Location = New System.Drawing.Point(11, 14)
        Lupa.Name = "Lupa"
        Lupa.Size = New System.Drawing.Size(28, 28)
        Lupa.TabIndex = 2
        Lupa.TabStop = False
        '
        'PictureBox
        '
        PictureBox.Location = New System.Drawing.Point(151, 49)
        PictureBox.Name = "PictureBox"
        PictureBox.Size = New System.Drawing.Size(122, 158)
        PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        PictureBox.TabIndex = 3
        PictureBox.TabStop = False
        PictureBox.Visible = False
        '
        'BirdMegaView
        '
        AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        ClientSize = New System.Drawing.Size(122, 158)
        Controls.Add(Lupa)
        Controls.Add(PictureBox)
        Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Name = "BirdMegaView"
        ShowInTaskbar = False
        StartPosition = System.Windows.Forms.FormStartPosition.Manual
        TopMost = True
        ResumeLayout(False)

    End Sub

#End Region

    Private Sub BirdMegaView_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try

            Lupa.Top = 0
            Lupa.Left = 0

            PictureBox.Image = NewImage

            'Dim g As Graphics = Graphics.FromImage(NewImage)
            'Dim rect As New Rectangle(0, 0, Me.Width, Me.Height)
            'g.DrawImage(NewImage, 0, 0, rect, GraphicsUnit.Pixel)
            Dim bit As Bitmap = New Bitmap(PictureBox.Image, PictureBox.Width, PictureBox.Height)
            BackgroundImage = bit

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    'Public c As Cursors

    Dim Moviendo As Boolean

    Private Sub Lupa_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Lupa.MouseDown
        Moviendo = True
    End Sub

    Private Sub Lupa_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Lupa.MouseUp
        Moviendo = False
    End Sub

    Public Event MeMovi(ByVal DTop As Int32, ByVal DLeft As Int32)
    Public Event MeCerre()


    Private Sub Lupa_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Lupa.MouseMove
        If Moviendo = True Then

            PrevioTop = Lupa.Top
            PrevioLeft = Lupa.Left


            Dim NuevoTop As Int32 = Cursor.Position.Y - Top - 32
            Dim NuevoLeft As Int32 = Cursor.Position.X - Left - 16

            If NuevoTop < 0 Then NuevoTop = 0
            If NuevoTop > Height - Lupa.Height - 24 Then NuevoTop = Height - Lupa.Height - 24
            If NuevoLeft < 0 Then NuevoLeft = 0
            If NuevoLeft > Width - Lupa.Width - 6 Then NuevoLeft = Width - Lupa.Width - 6

            Lupa.Top = NuevoTop
            Lupa.Left = NuevoLeft

            DiferenciaTop += Lupa.Top - PrevioTop
            DiferenciaLeft += Lupa.Left - PrevioLeft

            RaiseEvent MeMovi(DiferenciaTop, DiferenciaLeft)
            RaiseEvent FuiMovido(NuevoTop, NuevoLeft)
            DiferenciaTop = 0
            DiferenciaLeft = 0

        End If
        Cursor = Cursors.Hand
    End Sub

    Dim IsIn As Boolean
    Private Sub Lupa_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles Lupa.MouseEnter
        IsIn = True
    End Sub

    Private Sub Lupa_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Lupa.MouseLeave
        IsIn = False
    End Sub

    Private Sub BirdMegaView_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If IsIn = False Then Cursor = Cursors.Default
    End Sub

    Private Sub Lupa_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Lupa.Click

    End Sub

    Private Sub Lupa_Move(ByVal sender As Object, ByVal e As EventArgs) Handles Lupa.Move

    End Sub

    Private Sub BirdMegaView_Closed(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Closed
        RaiseEvent MeCerre()
    End Sub

    Public Event FuiMovido(ByVal Top As Int32, ByVal Left As Int32)


    Private Sub BirdMegaView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Click
        PrevioTop = Lupa.Top
        PrevioLeft = Lupa.Left

        Dim NuevoTop As Int32 = Cursor.Position.Y - Top - 32
        Dim NuevoLeft As Int32 = Cursor.Position.X - Left - 16

        If NuevoTop < 0 Then NuevoTop = 0
        If NuevoTop > Height - Lupa.Height - 24 Then NuevoTop = Height - Lupa.Height - 24
        If NuevoLeft < 0 Then NuevoLeft = 0
        If NuevoLeft > Width - Lupa.Width - 6 Then NuevoLeft = Width - Lupa.Width - 6

        Lupa.Top = NuevoTop
        Lupa.Left = NuevoLeft

        DiferenciaTop += Lupa.Top - PrevioTop
        DiferenciaLeft += Lupa.Left - PrevioLeft

        RaiseEvent MeMovi(DiferenciaTop, DiferenciaLeft)
        RaiseEvent FuiMovido(NuevoTop, NuevoLeft)

        DiferenciaTop = 0
        DiferenciaLeft = 0
    End Sub

    Public Sub ChangeLeft(ByVal Left As Int32)
        PrevioTop = Lupa.Top
        PrevioLeft = Lupa.Left

        Dim NuevoTop As Int32 = Lupa.Top
        Dim NuevoLeft As Int32 = Left

        If NuevoTop < 0 Then NuevoTop = 0
        If NuevoTop > Height - Lupa.Height - 24 Then NuevoTop = Height - Lupa.Height - 24
        If NuevoLeft < 0 Then NuevoLeft = 0
        If NuevoLeft > Width - Lupa.Width - 6 Then NuevoLeft = Width - Lupa.Width - 6

        Lupa.Top = NuevoTop
        Lupa.Left = NuevoLeft

        DiferenciaTop += Lupa.Top - PrevioTop
        DiferenciaLeft += Lupa.Left - PrevioLeft

        RaiseEvent MeMovi(DiferenciaTop, DiferenciaLeft)
        RaiseEvent FuiMovido(NuevoTop, NuevoLeft)

        DiferenciaTop = 0
        DiferenciaLeft = 0
    End Sub
    Public Sub ChangeTop(ByVal Top As Int32)
        PrevioTop = Lupa.Top
        PrevioLeft = Lupa.Left

        Dim NuevoTop As Int32 = Top
        Dim NuevoLeft As Int32 = Lupa.Left

        If NuevoTop < 0 Then NuevoTop = 0
        If NuevoTop > Height - Lupa.Height - 24 Then NuevoTop = Height - Lupa.Height - 24
        If NuevoLeft < 0 Then NuevoLeft = 0
        If NuevoLeft > Width - Lupa.Width - 6 Then NuevoLeft = Width - Lupa.Width - 6

        Lupa.Top = NuevoTop
        Lupa.Left = NuevoLeft

        DiferenciaTop += Lupa.Top - PrevioTop
        DiferenciaLeft += Lupa.Left - PrevioLeft

        RaiseEvent MeMovi(DiferenciaTop, DiferenciaLeft)
        RaiseEvent FuiMovido(NuevoTop, NuevoLeft)

        DiferenciaTop = 0
        DiferenciaLeft = 0
    End Sub
End Class
