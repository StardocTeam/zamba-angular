Public Class FrmPreviewGold2
    Inherits Zamba.appblock.zform

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.


    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(FrmPreviewGold2))
        PictureBox1 = New System.Windows.Forms.PictureBox
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'PictureBox1
        '
        PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        PictureBox1.Location = New System.Drawing.Point(2, 2)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New System.Drawing.Size(288, 262)
        PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        '
        'FrmPreviewGold2
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        BackColor = System.Drawing.Color.LightSteelBlue
        ClientSize = New System.Drawing.Size(292, 266)
        Controls.Add(PictureBox1)
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Name = "FrmPreviewGold2"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

#End Region

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)

        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub


End Class
