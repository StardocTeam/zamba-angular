<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InputBoxDoAsk
    Inherits Zamba.AppBlock.ZForm

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
        Me.txtinput = New System.Windows.Forms.TextBox
        Me.lblmensaje = New System.Windows.Forms.Label
        Me.Accept = New System.Windows.Forms.Button
        Me.lblmsj = New System.Windows.Forms.Label
        Me.btncancel = New System.Windows.Forms.Button
        Me.lstValue = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'txtinput
        '
        Me.txtinput.BackColor = System.Drawing.Color.White
        Me.txtinput.Location = New System.Drawing.Point(15, 50)
        Me.txtinput.Multiline = True
        Me.txtinput.Name = "txtinput"
        Me.txtinput.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtinput.Size = New System.Drawing.Size(429, 62)
        Me.txtinput.TabIndex = 2
        '
        'lblmensaje
        '
        Me.lblmensaje.Location = New System.Drawing.Point(12, 9)
        Me.lblmensaje.Name = "lblmensaje"
        Me.lblmensaje.Size = New System.Drawing.Size(432, 38)
        Me.lblmensaje.TabIndex = 3
        '
        'Accept
        '
        Me.Accept.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Accept.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Accept.Location = New System.Drawing.Point(43, 129)
        Me.Accept.Name = "Accept"
        Me.Accept.Size = New System.Drawing.Size(90, 23)
        Me.Accept.TabIndex = 4
        Me.Accept.Text = "Aceptar"
        Me.Accept.UseVisualStyleBackColor = True
        '
        'lblmsj
        '
        Me.lblmsj.AutoSize = True
        Me.lblmsj.ForeColor = System.Drawing.Color.Black
        Me.lblmsj.Location = New System.Drawing.Point(31, 23)
        Me.lblmsj.Name = "lblmsj"
        Me.lblmsj.Size = New System.Drawing.Size(0, 13)
        Me.lblmsj.TabIndex = 6
        '
        'btncancel
        '
        Me.btncancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btncancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancel.Location = New System.Drawing.Point(312, 129)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(90, 23)
        Me.btncancel.TabIndex = 7
        Me.btncancel.Text = "Cancelar"
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'lstValue
        '
        Me.lstValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstValue.FormattingEnabled = True
        Me.lstValue.Location = New System.Drawing.Point(15, 50)
        Me.lstValue.Name = "lstValue"
        Me.lstValue.Size = New System.Drawing.Size(429, 67)
        Me.lstValue.TabIndex = 8
        Me.lstValue.Visible = False
        '
        'InputBoxDoAsk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(461, 168)
        Me.Controls.Add(Me.lstValue)
        Me.Controls.Add(Me.btncancel)
        Me.Controls.Add(Me.lblmsj)
        Me.Controls.Add(Me.Accept)
        Me.Controls.Add(Me.lblmensaje)
        Me.Controls.Add(Me.txtinput)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InputBoxDoAsk"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ingreso de datos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtinput As System.Windows.Forms.TextBox
    Friend WithEvents lblmensaje As System.Windows.Forms.Label
    Friend WithEvents Accept As System.Windows.Forms.Button
    Friend WithEvents lblmsj As System.Windows.Forms.Label
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents lstValue As System.Windows.Forms.ListBox
End Class
