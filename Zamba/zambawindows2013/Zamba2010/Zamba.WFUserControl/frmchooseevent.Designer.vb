Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmchooseevent
    Inherits Zamba.AppBlock.ZForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lstEvents = New System.Windows.Forms.ListBox()
        Me.btnAceptar = New ZButton()
        Me.SuspendLayout()
        '
        'lstEvents
        '
        Me.lstEvents.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstEvents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstEvents.FormattingEnabled = True
        Me.lstEvents.ItemHeight = 14
        Me.lstEvents.Location = New System.Drawing.Point(2, 2)
        Me.lstEvents.Name = "lstEvents"
        Me.lstEvents.Size = New System.Drawing.Size(244, 163)
        Me.lstEvents.TabIndex = 0
        '
        'btnAceptar
        '
        Me.btnAceptar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.Location = New System.Drawing.Point(2, 165)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(244, 30)
        Me.btnAceptar.TabIndex = 1
        Me.btnAceptar.Text = "Aceptar"
        '
        'frmchooseevent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(248, 197)
        Me.ControlBox = False
        Me.Controls.Add(Me.lstEvents)
        Me.Controls.Add(Me.btnAceptar)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmchooseevent"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Seleccione un tipo de evento"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstEvents As System.Windows.Forms.ListBox
    Friend WithEvents btnAceptar As ZButton
End Class
