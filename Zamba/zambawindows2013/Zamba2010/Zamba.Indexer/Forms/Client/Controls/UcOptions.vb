Public Class UcOptions
    Inherits Zamba.AppBlock.ZControl

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
    Friend WithEvents LnkCreateWinForm As System.Windows.Forms.LinkLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        LnkCreateWinForm = New System.Windows.Forms.LinkLabel
        Panel1 = New System.Windows.Forms.Panel
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'LnkCreateWinForm
        '
        LnkCreateWinForm.ActiveLinkColor = System.Drawing.Color.MidnightBlue
        LnkCreateWinForm.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        LnkCreateWinForm.LinkColor = System.Drawing.Color.Black
        LnkCreateWinForm.Location = New System.Drawing.Point(16, 24)
        LnkCreateWinForm.Name = "LnkCreateWinForm"
        LnkCreateWinForm.Size = New System.Drawing.Size(200, 24)
        LnkCreateWinForm.TabIndex = 0
        LnkCreateWinForm.TabStop = True
        LnkCreateWinForm.Text = "Crear Windows Form"
        '
        'Panel1
        '
        Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel1.Controls.Add(LnkCreateWinForm)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(224, 392)
        Panel1.TabIndex = 1
        '
        'UcOptions
        '
        BackColor = System.Drawing.Color.White
        Controls.Add(Panel1)
        Name = "UcOptions"
        Size = New System.Drawing.Size(224, 392)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

End Class
