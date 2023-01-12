<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCDoExecuteScript
    Inherits Zamba.WFUserControl.ZRuleControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblTaskId = New Zamba.AppBlock.ZLabel()
        Me.btnSaveValues = New Zamba.AppBlock.ZButton()
        Me.RadRichTextEditor1 = New Telerik.WinControls.UI.RadRichTextEditor()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        CType(Me.RadRichTextEditor1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.tbRule.Controls.Add(Me.RadRichTextEditor1)
        Me.tbRule.Controls.Add(Me.lblTaskId)
        Me.tbRule.Controls.Add(Me.btnSaveValues)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(591, 450)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(599, 479)
        '
        'lblTaskId
        '
        Me.lblTaskId.AutoSize = True
        Me.lblTaskId.BackColor = System.Drawing.Color.Transparent
        Me.lblTaskId.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTaskId.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblTaskId.FontSize = 9.75!
        Me.lblTaskId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblTaskId.Location = New System.Drawing.Point(4, 4)
        Me.lblTaskId.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTaskId.Name = "lblTaskId"
        Me.lblTaskId.Size = New System.Drawing.Size(120, 16)
        Me.lblTaskId.TabIndex = 0
        Me.lblTaskId.Text = "Script a Ejecutar"
        Me.lblTaskId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSaveValues
        '
        Me.btnSaveValues.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSaveValues.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnSaveValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveValues.ForeColor = System.Drawing.Color.White
        Me.btnSaveValues.Location = New System.Drawing.Point(4, 418)
        Me.btnSaveValues.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSaveValues.Name = "btnSaveValues"
        Me.btnSaveValues.Size = New System.Drawing.Size(583, 28)
        Me.btnSaveValues.TabIndex = 1
        Me.btnSaveValues.Text = "Guardar"
        Me.btnSaveValues.UseVisualStyleBackColor = True
        '
        'RadRichTextEditor1
        '
        Me.RadRichTextEditor1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(156, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.RadRichTextEditor1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadRichTextEditor1.Location = New System.Drawing.Point(4, 20)
        Me.RadRichTextEditor1.Name = "RadRichTextEditor1"
        Me.RadRichTextEditor1.SelectionFill = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadRichTextEditor1.Size = New System.Drawing.Size(583, 398)
        Me.RadRichTextEditor1.TabIndex = 2
        '
        'UCDoExecuteScript
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCDoExecuteScript"
        Me.Size = New System.Drawing.Size(599, 479)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        CType(Me.RadRichTextEditor1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSaveValues As ZButton
    Friend WithEvents lblTaskId As ZLabel
    Friend WithEvents RadRichTextEditor1 As Telerik.WinControls.UI.RadRichTextEditor
End Class
