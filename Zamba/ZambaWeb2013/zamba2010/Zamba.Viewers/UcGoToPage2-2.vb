Public Class UcGoToPage2_2
    '    Inherits System.Windows.Forms.UserControl
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
    Friend WithEvents BtnGoPage As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.BtnGoPage = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'BtnGoPage
        '
        Me.BtnGoPage.BackColor = System.Drawing.Color.LightSteelBlue
        Me.BtnGoPage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGoPage.Location = New System.Drawing.Point(2, 8)
        Me.BtnGoPage.Name = "BtnGoPage"
        Me.BtnGoPage.Size = New System.Drawing.Size(33, 22)
        Me.BtnGoPage.TabIndex = 3
        Me.BtnGoPage.Text = "Ir"
        '
        'UcGoToPage2_2
        '
        Me.BackColor = System.Drawing.Color.Lavender
        Me.Controls.Add(Me.BtnGoPage)
        Me.Name = "UcGoToPage2_2"
        Me.Size = New System.Drawing.Size(39, 38)
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
