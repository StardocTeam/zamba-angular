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
    Friend WithEvents ZBluePanel1 As Zamba.AppBlock.ZBluePanel
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ZBluePanel1 = New Zamba.AppBlock.ZBluePanel
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.ZBluePanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ZBluePanel1
        '
        Me.ZBluePanel1.Controls.Add(Me.TextBox1)
        Me.ZBluePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ZBluePanel1.Location = New System.Drawing.Point(0, 0)
        Me.ZBluePanel1.Name = "ZBluePanel1"
        Me.ZBluePanel1.Size = New System.Drawing.Size(336, 133)
        Me.ZBluePanel1.TabIndex = 0
        '
        'TextBox1
        '
        Me.TextBox1.AcceptsReturn = True
        Me.TextBox1.AcceptsTab = True
        Me.TextBox1.Location = New System.Drawing.Point(8, 8)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(320, 120)
        Me.TextBox1.TabIndex = 0
        Me.TextBox1.Text = "Esta herramienta sirve para detectar indices erroneos en las tablas DOC_I y DOC_D" & _
        ", así como también las filas de relaciones entre indices y DOC TYPES que ya no e" & _
        "xisten más, que no se borraron oportunamente."
        '
        'frmAyuda
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(336, 133)
        Me.Controls.Add(Me.ZBluePanel1)
        Me.MaximizeBox = False
        Me.Name = "frmAyuda"
        Me.Text = "Zamba - Comprobar integridad de atributos - Ayuda"
        Me.ZBluePanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
