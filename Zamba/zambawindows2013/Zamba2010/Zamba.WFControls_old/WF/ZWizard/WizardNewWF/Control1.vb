Public Class Control1
    Inherits zcontrol

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
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents txtName As TextBox
    Friend WithEvents txtHelp As TextBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Control1))
        Panel1 = New ZPanel
        txtHelp = New TextBox
        Label4 = New ZLabel
        txtDescription = New TextBox
        Label3 = New ZLabel
        txtName = New TextBox
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
        Panel1.Controls.Add(txtHelp)
        Panel1.Controls.Add(Label4)
        Panel1.Controls.Add(txtDescription)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(txtName)
        Panel1.Controls.Add(PanelLine)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(550, 393)
        Panel1.TabIndex = 0
        '
        'txtHelp
        '
        txtHelp.BackColor = System.Drawing.Color.White
        txtHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtHelp.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtHelp.Location = New System.Drawing.Point(40, 280)
        txtHelp.MaxLength = 100
        txtHelp.Name = "txtHelp"
        txtHelp.Size = New System.Drawing.Size(448, 23)
        txtHelp.TabIndex = 11
        txtHelp.Text = ""
        '
        'Label4
        '
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label4.Location = New System.Drawing.Point(40, 256)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(128, 23)
        Label4.TabIndex = 10
        Label4.Text = "Y su ayuda"
        Label4.TextAlign = ContentAlignment.MiddleCenter
        '
        'txtDescription
        '
        txtDescription.BackColor = System.Drawing.Color.White
        txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtDescription.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtDescription.Location = New System.Drawing.Point(40, 224)
        txtDescription.MaxLength = 100
        txtDescription.Name = "txtDescription"
        txtDescription.Size = New System.Drawing.Size(448, 23)
        txtDescription.TabIndex = 1
        txtDescription.Text = ""
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label3.Location = New System.Drawing.Point(40, 200)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(184, 23)
        Label3.TabIndex = 8
        Label3.Text = "Ingrese su descripcion"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        '
        'txtName
        '
        txtName.BackColor = System.Drawing.Color.White
        txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtName.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        txtName.Location = New System.Drawing.Point(40, 168)
        txtName.MaxLength = 50
        txtName.Name = "txtName"
        txtName.Size = New System.Drawing.Size(448, 23)
        txtName.TabIndex = 0
        txtName.Text = "Mi WorkFlow 1"
        '
        'PanelLine
        '
        PanelLine.BackColor = System.Drawing.Color.Black
        PanelLine.Location = New System.Drawing.Point(24, 112)
        PanelLine.Name = "PanelLine"
        PanelLine.Size = New System.Drawing.Size(496, 1)
        PanelLine.TabIndex = 2
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label2.Location = New System.Drawing.Point(40, 144)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(320, 23)
        Label2.TabIndex = 6
        Label2.Text = "Ingrese el nombre que identificara a su WorkFLow"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 15.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(24, 32)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(504, 72)
        Label1.TabIndex = 0
        Label1.Text = "Determine las caracteristicas basicas del WorkFLow"
        '
        'Control1
        '
        Controls.Add(Panel1)
        Name = "Control1"
        Size = New System.Drawing.Size(550, 393)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Public ReadOnly Property Nombre() As String
        Get
            Return txtName.Text
        End Get
    End Property
    Public ReadOnly Property Descripcion() As String
        Get
            Return txtDescription.Text
        End Get
    End Property
    Public ReadOnly Property Ayuda() As String
        Get
            Return txtHelp.Text
        End Get
    End Property

End Class
