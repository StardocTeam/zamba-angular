<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoWait
    Inherits Zamba.WFUserControl.ZRuleControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.txtWaitTime = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.ZButton = New Zamba.AppBlock.ZButton
        Me.Label5 = New ZLabel
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.txtWaitTime)
        Me.tbRule.Controls.Add(Me.ZButton)
        '
        'txtWaitTime
        '
        Me.txtWaitTime.Location = New System.Drawing.Point(175, 27)
        Me.txtWaitTime.Name = "txtWaitTime"
        Me.txtWaitTime.Size = New System.Drawing.Size(147, 21)
        Me.txtWaitTime.TabIndex = 16
        '
        'ZButton
        '
        Me.ZButton.DialogResult = System.Windows.Forms.DialogResult.None
        Me.ZButton.Location = New System.Drawing.Point(328, 27)
        Me.ZButton.Name = "ZButton"
        Me.ZButton.Size = New System.Drawing.Size(68, 21)
        Me.ZButton.TabIndex = 14
        Me.ZButton.Text = "Guardar"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(6, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(163, 13)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Cantidad de segundos a esperar"
        '
        'UCDoWait
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoWait"
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtWaitTime As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents ZButton As Zamba.AppBlock.ZButton
    Friend WithEvents Label5 As ZLabel

End Class
