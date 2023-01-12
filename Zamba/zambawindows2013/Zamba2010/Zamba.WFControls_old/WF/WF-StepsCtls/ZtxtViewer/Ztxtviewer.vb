Public Class Ztxtviewer
    Inherits Zamba.AppBlock.ZControl

    Public IsReadOnly As Boolean
    Private File As String

#Region " C�digo generado por el Dise�ador de Windows Forms "

    Public Sub New(ByVal _File As String, ByVal _IsReadOnly As Boolean)
        MyBase.New()

        'El Dise�ador de Windows Forms requiere esta llamada.
        InitializeComponent()
        IsReadOnly = _IsReadOnly
        File = _File
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
    Friend WithEvents RTxt As System.Windows.Forms.RichTextBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Ztxtviewer))
        RTxt = New System.Windows.Forms.RichTextBox
        SuspendLayout()
        '
        'ZIconList
        '

        '
        'RTxt
        '
        RTxt.Dock = System.Windows.Forms.DockStyle.Fill
        RTxt.Location = New System.Drawing.Point(0, 0)
        RTxt.Name = "RTxt"
        RTxt.Size = New System.Drawing.Size(392, 280)
        RTxt.TabIndex = 0
        RTxt.Text = ""
        '
        'Ztxtviewer
        '
        Controls.Add(RTxt)
        Name = "Ztxtviewer"
        Size = New System.Drawing.Size(392, 280)
        ResumeLayout(False)

    End Sub

#End Region

    Private Sub Ztxtviewer_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        RTxt.ReadOnly = IsReadOnly
        Try
            RTxt.LoadFile(File, RichTextBoxStreamType.PlainText)
        Catch
        End Try
    End Sub
    Public Sub SAVE()
        RTxt.SaveFile(File)
    End Sub
End Class
