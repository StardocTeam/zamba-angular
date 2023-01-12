Public Class UcRequired
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
    Friend WithEvents GB As GroupBox
    Friend WithEvents rbobligatorio As System.Windows.Forms.RadioButton
    Friend WithEvents RbNoObligatorio As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbNoObligatorioNotes As System.Windows.Forms.RadioButton
    Friend WithEvents RbObligatorioNotes As System.Windows.Forms.RadioButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        GB = New GroupBox
        rbobligatorio = New System.Windows.Forms.RadioButton
        RbNoObligatorio = New System.Windows.Forms.RadioButton
        GroupBox1 = New GroupBox
        rbNoObligatorioNotes = New System.Windows.Forms.RadioButton
        RbObligatorioNotes = New System.Windows.Forms.RadioButton
        GB.SuspendLayout()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        '
        'GB
        '
        GB.BackColor = Color.Transparent
        GB.Controls.Add(RbNoObligatorio)
        GB.Controls.Add(rbobligatorio)
        GB.Location = New Point(16, 16)
        GB.Name = "GB"
        GB.Size = New Size(360, 72)
        GB.TabIndex = 0
        GB.TabStop = False
        GB.Text = "Requerido Zamba"
        '
        'rbobligatorio
        '
        rbobligatorio.Location = New Point(104, 16)
        rbobligatorio.Name = "rbobligatorio"
        rbobligatorio.Size = New Size(176, 24)
        rbobligatorio.TabIndex = 0
        rbobligatorio.Text = "Obligatorio"
        '
        'RbNoObligatorio
        '
        RbNoObligatorio.Checked = True
        RbNoObligatorio.Location = New Point(104, 40)
        RbNoObligatorio.Name = "RbNoObligatorio"
        RbNoObligatorio.Size = New Size(176, 24)
        RbNoObligatorio.TabIndex = 1
        RbNoObligatorio.TabStop = True
        RbNoObligatorio.Text = "No Obligatorio"
        '
        'GroupBox1
        '
        GroupBox1.BackColor = Color.Transparent
        GroupBox1.Controls.Add(rbNoObligatorioNotes)
        GroupBox1.Controls.Add(RbObligatorioNotes)
        GroupBox1.Location = New Point(16, 104)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(360, 72)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        GroupBox1.Text = "Requerido Lotus Notes"
        '
        'rbNoObligatorioNotes
        '
        rbNoObligatorioNotes.Checked = True
        rbNoObligatorioNotes.Location = New Point(104, 40)
        rbNoObligatorioNotes.Name = "rbNoObligatorioNotes"
        rbNoObligatorioNotes.Size = New Size(176, 24)
        rbNoObligatorioNotes.TabIndex = 1
        rbNoObligatorioNotes.TabStop = True
        rbNoObligatorioNotes.Text = "No Obligatorio"
        '
        'RbObligatorioNotes
        '
        RbObligatorioNotes.Location = New Point(104, 16)
        RbObligatorioNotes.Name = "RbObligatorioNotes"
        RbObligatorioNotes.Size = New Size(176, 24)
        RbObligatorioNotes.TabIndex = 0
        RbObligatorioNotes.Text = "Obligatorio"
        '
        'UcRequired
        '
        Controls.Add(GroupBox1)
        Controls.Add(GB)
        Name = "UcRequired"
        Size = New Size(408, 192)
        GB.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

#Region "Variables"
    Private MustComplete As Boolean = False
    Private MustCompleteLotus As Boolean = False
#End Region
#Region "Eventos"
    Public Event MustCompleteZamba(ByVal Valor As Boolean)
    Public Event MustCompleteNotes(ByVal Valor As Boolean)
#End Region

    Private Sub RbObligatorioNotes_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles RbObligatorioNotes.CheckedChanged
        MustCompleteLotus = Not MustCompleteLotus
        RaiseEvent MustCompleteNotes(MustCompleteLotus)
    End Sub

    Private Sub rbobligatorio_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbobligatorio.CheckedChanged
        MustComplete = Not MustComplete
        RaiseEvent MustCompleteNotes(MustComplete)
    End Sub

End Class
