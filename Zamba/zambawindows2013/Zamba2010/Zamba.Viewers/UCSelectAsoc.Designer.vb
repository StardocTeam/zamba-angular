<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCSelectAsoc
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lstDocTypes = New System.Windows.Forms.ListBox
        Me.btnok = New ZButton
        Me.Label1 = New ZLabel
        Me.SuspendLayout()
        '
        'lstDocTypes
        '
        Me.lstDocTypes.FormattingEnabled = True
        Me.lstDocTypes.HorizontalScrollbar = True
        Me.lstDocTypes.Location = New System.Drawing.Point(25, 31)
        Me.lstDocTypes.Name = "lstDocTypes"
        Me.lstDocTypes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstDocTypes.Size = New System.Drawing.Size(272, 173)
        Me.lstDocTypes.TabIndex = 0
        '
        'btnok
        '
        Me.btnok.Location = New System.Drawing.Point(120, 210)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(75, 23)
        Me.btnok.TabIndex = 1
        Me.btnok.Text = "Aceptar"
        Me.btnok.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(274, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Selecciones los tipos de documento que desea adjuntar:"
        '
        'UCSelectAsoc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(325, 240)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.lstDocTypes)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UCSelectAsoc"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tipos de documento"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstDocTypes As System.Windows.Forms.ListBox
    Friend WithEvents btnok As ZButton
    Friend WithEvents Label1 As ZLabel
End Class
