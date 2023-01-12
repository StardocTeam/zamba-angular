<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoForEach
    Inherits ZRuleControl

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
    Private Shadows Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCDoForEach))
        Me.btnAcept = New Zamba.AppBlock.ZButton()
        Me.lblValue = New Zamba.AppBlock.ZLabel()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.lbldescription = New Zamba.AppBlock.ZLabel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbHabilitation
        '
        Me.tbHabilitation.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbConfiguration
        '
        Me.tbConfiguration.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbAlerts
        '
        Me.tbAlerts.Margin = New System.Windows.Forms.Padding(4)
        Me.tbAlerts.Padding = New System.Windows.Forms.Padding(4)
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.lbldescription)
        Me.tbRule.Controls.Add(Me.txtValue)
        Me.tbRule.Controls.Add(Me.lblValue)
        Me.tbRule.Controls.Add(Me.btnAcept)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(740, 356)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(748, 385)
        '
        'btnAcept
        '
        Me.btnAcept.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAcept.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAcept.ForeColor = System.Drawing.Color.White
        Me.btnAcept.Location = New System.Drawing.Point(329, 95)
        Me.btnAcept.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAcept.Name = "btnAcept"
        Me.btnAcept.Size = New System.Drawing.Size(100, 28)
        Me.btnAcept.TabIndex = 0
        Me.btnAcept.Text = "Guardar"
        Me.btnAcept.UseVisualStyleBackColor = True
        '
        'lblValue
        '
        Me.lblValue.AutoSize = True
        Me.lblValue.BackColor = System.Drawing.Color.Transparent
        Me.lblValue.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblValue.Location = New System.Drawing.Point(31, 30)
        Me.lblValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(255, 16)
        Me.lblValue.TabIndex = 1
        Me.lblValue.Text = "Nombre de Variable Con la Cual Iterar"
        Me.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(35, 62)
        Me.txtValue.Margin = New System.Windows.Forms.Padding(4)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(393, 23)
        Me.txtValue.TabIndex = 2
        Me.txtValue.Tag = ""
        '
        'lbldescription
        '
        Me.lbldescription.BackColor = System.Drawing.Color.Transparent
        Me.lbldescription.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lbldescription.Location = New System.Drawing.Point(32, 160)
        Me.lbldescription.Name = "lbldescription"
        Me.lbldescription.Size = New System.Drawing.Size(664, 179)
        Me.lbldescription.TabIndex = 3
        Me.lbldescription.Text = resources.GetString("lbldescription.Text")
        Me.lbldescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCDoForEach
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCDoForEach"
        Me.Size = New System.Drawing.Size(748, 385)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAcept As ZButton
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents lblValue As ZLabel
    Friend WithEvents lbldescription As ZLabel
End Class
