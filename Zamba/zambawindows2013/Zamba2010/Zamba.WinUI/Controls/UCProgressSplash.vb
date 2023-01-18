Public Class UCProgressSplash
    Inherits System.Windows.Forms.UserControl

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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(UCProgressSplash))
        Panel1 = New System.Windows.Forms.Panel
        PictureBox1 = New System.Windows.Forms.PictureBox
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.BackColor = System.Drawing.Color.Transparent
        Panel1.Controls.Add(PictureBox1)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(345, 172)
        Panel1.TabIndex = 2
        '
        'PictureBox1
        '
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        PictureBox1.Location = New System.Drawing.Point(-3, 1)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New System.Drawing.Size(350, 170)
        PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        PictureBox1.TabIndex = 1
        PictureBox1.TabStop = False
        '
        'UCProgressSplash
        '
        BackColor = System.Drawing.Color.LightSkyBlue
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Controls.Add(Panel1)
        Name = "UCProgressSplash"
        Size = New System.Drawing.Size(345, 172)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region


End Class
