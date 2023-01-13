<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTransaction
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTransaction))
        Me.btnStart = New System.Windows.Forms.Button()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.btnSearchFile = New System.Windows.Forms.Button()
        Me.txtMessages = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(155, 53)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(143, 39)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Comenzar transacción"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'txtFile
        '
        Me.txtFile.Location = New System.Drawing.Point(12, 13)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.Size = New System.Drawing.Size(392, 20)
        Me.txtFile.TabIndex = 1
        '
        'btnSearchFile
        '
        Me.btnSearchFile.Location = New System.Drawing.Point(410, 11)
        Me.btnSearchFile.Name = "btnSearchFile"
        Me.btnSearchFile.Size = New System.Drawing.Size(30, 23)
        Me.btnSearchFile.TabIndex = 2
        Me.btnSearchFile.Text = "..."
        Me.btnSearchFile.UseVisualStyleBackColor = True
        '
        'txtMessages
        '
        Me.txtMessages.Location = New System.Drawing.Point(12, 107)
        Me.txtMessages.Multiline = True
        Me.txtMessages.Name = "txtMessages"
        Me.txtMessages.Size = New System.Drawing.Size(428, 306)
        Me.txtMessages.TabIndex = 3
        '
        'FrmTransaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 425)
        Me.Controls.Add(Me.txtMessages)
        Me.Controls.Add(Me.btnSearchFile)
        Me.Controls.Add(Me.txtFile)
        Me.Controls.Add(Me.btnStart)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmTransaction"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zamba.Transactions"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchFile As System.Windows.Forms.Button
    Friend WithEvents txtMessages As System.Windows.Forms.TextBox

End Class
