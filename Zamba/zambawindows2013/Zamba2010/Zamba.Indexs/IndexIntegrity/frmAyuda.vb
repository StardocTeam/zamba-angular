Public Class frmAyuda
    Inherits Zamba.appblock.zform

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

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
    Friend WithEvents ZPanel As ZPanel
    Friend WithEvents TextBox1 As TextBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        ZPanel = New ZPanel
        TextBox1 = New TextBox
        ZPanel.SuspendLayout()
        SuspendLayout()
        '
        'ZPanel
        '
        ZPanel.Controls.Add(TextBox1)
        ZPanel.Dock = DockStyle.Fill
        ZPanel.Location = New Point(0, 0)
        ZPanel.Name = "ZPanel"
        ZPanel.Size = New Size(336, 133)
        ZPanel.TabIndex = 0
        '
        'TextBox1
        '
        TextBox1.AcceptsReturn = True
        TextBox1.AcceptsTab = True
        TextBox1.Location = New Point(8, 8)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.Size = New Size(320, 120)
        TextBox1.TabIndex = 0
        TextBox1.Text = "Esta herramienta sirve para detectar atributos erroneos en las tablas DOC_I" &
        ", así como también las filas de relaciones entre atributos y DOC TYPES que ya no e" &
        "xisten más, que no se borraron oportunamente."
        '
        'frmAyuda
        '
        AutoScaleBaseSize = New Size(5, 13)
        ClientSize = New Size(336, 133)
        Controls.Add(ZPanel)
        MaximizeBox = False
        Name = "frmAyuda"
        Text = "Zamba - Comprobar integridad de atributos - Ayuda"
        ZPanel.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

End Class
