Public Class UcGoToPage2_2
    '    Inherits System.Windows.Forms.UserControl
    Inherits Zamba.AppBlock.ZControl

#Region " C�digo generado por el Dise�ador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Dise�ador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicializaci�n despu�s de la llamada a InitializeComponent()

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

    'Requerido por el Dise�ador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Dise�ador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Dise�ador de Windows Forms. 
    'No lo modifique con el editor de c�digo.
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
