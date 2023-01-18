<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UcForumDetails
    Inherits System.Windows.Forms.UserControl

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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblExpireTime = New System.Windows.Forms.Label
        Me.lblMessage = New System.Windows.Forms.Label
        Me.txtExpireTime = New System.Windows.Forms.TextBox
        Me.txtCreationTime = New System.Windows.Forms.TextBox
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.txtMessageId = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Usuario creador:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Fecha de creación:"
        '
        'lblExpireTime
        '
        Me.lblExpireTime.AutoSize = True
        Me.lblExpireTime.Location = New System.Drawing.Point(13, 67)
        Me.lblExpireTime.Name = "lblExpireTime"
        Me.lblExpireTime.Size = New System.Drawing.Size(115, 13)
        Me.lblExpireTime.TabIndex = 2
        Me.lblExpireTime.Text = "Fecha de vencimiento:"
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Location = New System.Drawing.Point(13, 94)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(78, 13)
        Me.lblMessage.TabIndex = 3
        Me.lblMessage.Text = "Id del mensaje:"
        '
        'txtExpireTime
        '
        Me.txtExpireTime.Location = New System.Drawing.Point(132, 64)
        Me.txtExpireTime.Name = "txtExpireTime"
        Me.txtExpireTime.ReadOnly = True
        Me.txtExpireTime.Size = New System.Drawing.Size(229, 20)
        Me.txtExpireTime.TabIndex = 4
        '
        'txtCreationTime
        '
        Me.txtCreationTime.Location = New System.Drawing.Point(132, 38)
        Me.txtCreationTime.Name = "txtCreationTime"
        Me.txtCreationTime.ReadOnly = True
        Me.txtCreationTime.Size = New System.Drawing.Size(229, 20)
        Me.txtCreationTime.TabIndex = 5
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(132, 12)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.ReadOnly = True
        Me.txtUserName.Size = New System.Drawing.Size(229, 20)
        Me.txtUserName.TabIndex = 6
        '
        'txtMessageId
        '
        Me.txtMessageId.Location = New System.Drawing.Point(132, 91)
        Me.txtMessageId.Name = "txtMessageId"
        Me.txtMessageId.ReadOnly = True
        Me.txtMessageId.Size = New System.Drawing.Size(229, 20)
        Me.txtMessageId.TabIndex = 7
        '
        'UcForumDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtMessageId)
        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.txtCreationTime)
        Me.Controls.Add(Me.txtExpireTime)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.lblExpireTime)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "UcForumDetails"
        Me.Size = New System.Drawing.Size(417, 122)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblExpireTime As System.Windows.Forms.Label
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents txtExpireTime As System.Windows.Forms.TextBox
    Friend WithEvents txtCreationTime As System.Windows.Forms.TextBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents txtMessageId As System.Windows.Forms.TextBox

End Class
