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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.LnkCreateWinForm = New System.Windows.Forms.LinkLabel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LnkCreateWinForm
        '
        Me.LnkCreateWinForm.ActiveLinkColor = System.Drawing.Color.MidnightBlue
        Me.LnkCreateWinForm.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LnkCreateWinForm.LinkColor = System.Drawing.Color.Black
        Me.LnkCreateWinForm.Location = New System.Drawing.Point(16, 24)
        Me.LnkCreateWinForm.Name = "LnkCreateWinForm"
        Me.LnkCreateWinForm.Size = New System.Drawing.Size(200, 24)
        Me.LnkCreateWinForm.TabIndex = 0
        Me.LnkCreateWinForm.TabStop = True
        Me.LnkCreateWinForm.Text = "Crear Windows Form"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.LnkCreateWinForm)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(224, 392)
        Me.Panel1.TabIndex = 1
        '
        'UcOptions
        '
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UcOptions"
        Me.Size = New System.Drawing.Size(224, 392)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
