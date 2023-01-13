<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInputBox
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
        Me.btnAcept = New System.Windows.Forms.Button()
        Me.txtUserText = New System.Windows.Forms.TextBox()
        Me.lblText = New System.Windows.Forms.Label()
        Me.lblChracters = New System.Windows.Forms.Label()
        Me.lblMax20 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnAcept
        '
        Me.btnAcept.Location = New System.Drawing.Point(371, 65)
        Me.btnAcept.Name = "btnAcept"
        Me.btnAcept.Size = New System.Drawing.Size(87, 27)
        Me.btnAcept.TabIndex = 1
        Me.btnAcept.Text = "Aceptar"
        Me.btnAcept.UseVisualStyleBackColor = True
        '
        'txtUserText
        '
        Me.txtUserText.Location = New System.Drawing.Point(12, 32)
        Me.txtUserText.Name = "txtUserText"
        Me.txtUserText.Size = New System.Drawing.Size(446, 21)
        Me.txtUserText.TabIndex = 0
        '
        'lblText
        '
        Me.lblText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblText.Location = New System.Drawing.Point(12, 9)
        Me.lblText.Name = "lblText"
        Me.lblText.Size = New System.Drawing.Size(446, 20)
        Me.lblText.TabIndex = 2
        '
        'lblChracters
        '
        Me.lblChracters.AutoSize = True
        Me.lblChracters.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChracters.Location = New System.Drawing.Point(9, 77)
        Me.lblChracters.Name = "lblChracters"
        Me.lblChracters.Size = New System.Drawing.Size(149, 15)
        Me.lblChracters.TabIndex = 3
        Me.lblChracters.Text = "maximo de caracteres"
        '
        'lblMax20
        '
        Me.lblMax20.AutoSize = True
        Me.lblMax20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMax20.ForeColor = System.Drawing.Color.Red
        Me.lblMax20.Location = New System.Drawing.Point(9, 64)
        Me.lblMax20.Name = "lblMax20"
        Me.lblMax20.Size = New System.Drawing.Size(129, 13)
        Me.lblMax20.TabIndex = 4
        Me.lblMax20.Text = "mas de 20 caracteres"
        '
        'frmInputBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(470, 104)
        Me.Controls.Add(Me.lblMax20)
        Me.Controls.Add(Me.lblChracters)
        Me.Controls.Add(Me.lblText)
        Me.Controls.Add(Me.txtUserText)
        Me.Controls.Add(Me.btnAcept)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInputBox"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnAcept As System.Windows.Forms.Button
    Friend WithEvents txtUserText As System.Windows.Forms.TextBox
    Friend WithEvents lblText As System.Windows.Forms.Label
    Friend WithEvents lblChracters As System.Windows.Forms.Label
    Friend WithEvents lblMax20 As System.Windows.Forms.Label
End Class
