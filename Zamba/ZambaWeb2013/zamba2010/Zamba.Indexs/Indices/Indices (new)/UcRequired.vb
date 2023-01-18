Public Class UcRequired
    Inherits Zamba.AppBlock.ZBlueControl

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
    Friend WithEvents GB As System.Windows.Forms.GroupBox
    Friend WithEvents rbobligatorio As System.Windows.Forms.RadioButton
    Friend WithEvents RbNoObligatorio As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbNoObligatorioNotes As System.Windows.Forms.RadioButton
    Friend WithEvents RbObligatorioNotes As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GB = New System.Windows.Forms.GroupBox
        Me.rbobligatorio = New System.Windows.Forms.RadioButton
        Me.RbNoObligatorio = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rbNoObligatorioNotes = New System.Windows.Forms.RadioButton
        Me.RbObligatorioNotes = New System.Windows.Forms.RadioButton
        Me.GB.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GB
        '
        Me.GB.BackColor = System.Drawing.Color.Transparent
        Me.GB.Controls.Add(Me.RbNoObligatorio)
        Me.GB.Controls.Add(Me.rbobligatorio)
        Me.GB.Location = New System.Drawing.Point(16, 16)
        Me.GB.Name = "GB"
        Me.GB.Size = New System.Drawing.Size(360, 72)
        Me.GB.TabIndex = 0
        Me.GB.TabStop = False
        Me.GB.Text = "Requerido Zamba"
        '
        'rbobligatorio
        '
        Me.rbobligatorio.Location = New System.Drawing.Point(104, 16)
        Me.rbobligatorio.Name = "rbobligatorio"
        Me.rbobligatorio.Size = New System.Drawing.Size(176, 24)
        Me.rbobligatorio.TabIndex = 0
        Me.rbobligatorio.Text = "Obligatorio"
        '
        'RbNoObligatorio
        '
        Me.RbNoObligatorio.Checked = True
        Me.RbNoObligatorio.Location = New System.Drawing.Point(104, 40)
        Me.RbNoObligatorio.Name = "RbNoObligatorio"
        Me.RbNoObligatorio.Size = New System.Drawing.Size(176, 24)
        Me.RbNoObligatorio.TabIndex = 1
        Me.RbNoObligatorio.TabStop = True
        Me.RbNoObligatorio.Text = "No Obligatorio"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.rbNoObligatorioNotes)
        Me.GroupBox1.Controls.Add(Me.RbObligatorioNotes)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 104)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(360, 72)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Requerido Lotus Notes"
        '
        'rbNoObligatorioNotes
        '
        Me.rbNoObligatorioNotes.Checked = True
        Me.rbNoObligatorioNotes.Location = New System.Drawing.Point(104, 40)
        Me.rbNoObligatorioNotes.Name = "rbNoObligatorioNotes"
        Me.rbNoObligatorioNotes.Size = New System.Drawing.Size(176, 24)
        Me.rbNoObligatorioNotes.TabIndex = 1
        Me.rbNoObligatorioNotes.TabStop = True
        Me.rbNoObligatorioNotes.Text = "No Obligatorio"
        '
        'RbObligatorioNotes
        '
        Me.RbObligatorioNotes.Location = New System.Drawing.Point(104, 16)
        Me.RbObligatorioNotes.Name = "RbObligatorioNotes"
        Me.RbObligatorioNotes.Size = New System.Drawing.Size(176, 24)
        Me.RbObligatorioNotes.TabIndex = 0
        Me.RbObligatorioNotes.Text = "Obligatorio"
        '
        'UcRequired
        '
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GB)
        Me.Name = "UcRequired"
        Me.Size = New System.Drawing.Size(408, 192)
        Me.GB.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

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

    Private Sub RbObligatorioNotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbObligatorioNotes.CheckedChanged
        MustCompleteLotus = Not MustCompleteLotus
        RaiseEvent MustCompleteNotes(MustCompleteLotus)
    End Sub

    Private Sub rbobligatorio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbobligatorio.CheckedChanged
        MustComplete = Not MustComplete
        RaiseEvent MustCompleteNotes(MustComplete)
    End Sub

End Class
