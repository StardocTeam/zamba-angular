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
    Friend WithEvents BtnGoPage As ZButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        BtnGoPage = New ZButton
        SuspendLayout()
        '
        'BtnGoPage
        '
        BtnGoPage.BackColor = System.Drawing.Color.LightSteelBlue
        BtnGoPage.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily()
        BtnGoPage.Location = New System.Drawing.Point(2, 8)
        BtnGoPage.Name = "BtnGoPage"
        BtnGoPage.Size = New System.Drawing.Size(33, 22)
        BtnGoPage.TabIndex = 3
        BtnGoPage.Text = "Ir"
        '
        'UcGoToPage2_2
        '
        BackColor = System.Drawing.Color.Lavender
        Controls.Add(BtnGoPage)
        Name = "UcGoToPage2_2"
        Size = New System.Drawing.Size(39, 38)
        ResumeLayout(False)

    End Sub

#End Region

End Class
