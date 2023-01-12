Public Class Control0
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
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents PanelLine As System.Windows.Forms.Panel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Control0))
        Panel1 = New ZPanel
        PictureBox1 = New System.Windows.Forms.PictureBox
        Label3 = New ZLabel
        PanelLine = New System.Windows.Forms.Panel
        Label2 = New ZLabel
        Label1 = New ZLabel
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'ZIconList
        '

        '
        'Panel1
        '
        Panel1.Controls.Add(PictureBox1)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(PanelLine)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(550, 393)
        Panel1.TabIndex = 1
        '
        'PictureBox1
        '
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        PictureBox1.Location = New System.Drawing.Point(40, 40)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New System.Drawing.Size(152, 312)
        PictureBox1.TabIndex = 5
        PictureBox1.TabStop = False
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label3.Location = New System.Drawing.Point(216, 208)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(304, 64)
        Label3.TabIndex = 4
        Label3.Text = "  Este asistente lo guiara paso por paso de una forma guiada al ingreso de las ca" & _
        "racteristicas principales."
        '
        'PanelLine
        '
        PanelLine.BackColor = System.Drawing.Color.Black
        PanelLine.Location = New System.Drawing.Point(216, 136)
        PanelLine.Name = "PanelLine"
        PanelLine.Size = New System.Drawing.Size(296, 1)
        PanelLine.TabIndex = 3
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label2.Location = New System.Drawing.Point(216, 152)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(304, 48)
        Label2.TabIndex = 1
        Label2.Text = "  Realice de una forma mas practica la creacion de su nuevo circuito de trabajo."
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 15.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label1.Location = New System.Drawing.Point(216, 48)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(304, 80)
        Label1.TabIndex = 0
        Label1.Text = "Asistente para la creacion de su nuevo Workflow"
        '
        'Control0
        '
        Controls.Add(Panel1)
        Name = "Control0"
        Size = New System.Drawing.Size(550, 393)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region


End Class
