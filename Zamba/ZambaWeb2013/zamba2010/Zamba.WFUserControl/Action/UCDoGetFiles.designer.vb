<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoGetFiles
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
        Me.BtnSaveFile = New Zamba.AppBlock.ZButton()
        Me.txtDirectory = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblDirectory = New Zamba.AppBlock.ZLabel()
        Me.ChkObtainAllFiles = New System.Windows.Forms.CheckBox()
        Me.btnInputExm = New Zamba.AppBlock.ZButton()
        Me.GbRule = New System.Windows.Forms.GroupBox()
        Me.lstExtension = New System.Windows.Forms.ListBox()
        Me.lvlVarName = New Zamba.AppBlock.ZLabel()
        Me.txtVarName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label4 = New Zamba.AppBlock.ZLabel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.GbRule.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbState.Size = New System.Drawing.Size(824, 781)
        '
        'tbHabilitation
        '
        Me.tbHabilitation.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbHabilitation.Size = New System.Drawing.Size(824, 781)
        '
        'tbConfiguration
        '
        Me.tbConfiguration.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbConfiguration.Size = New System.Drawing.Size(824, 781)
        '
        'tbAlerts
        '
        Me.tbAlerts.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbAlerts.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbAlerts.Size = New System.Drawing.Size(824, 781)
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.GbRule)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(824, 781)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(832, 810)
        '
        'BtnSaveFile
        '
        Me.BtnSaveFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnSaveFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveFile.ForeColor = System.Drawing.Color.White
        Me.BtnSaveFile.Location = New System.Drawing.Point(658, 598)
        Me.BtnSaveFile.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnSaveFile.Name = "BtnSaveFile"
        Me.BtnSaveFile.Size = New System.Drawing.Size(93, 33)
        Me.BtnSaveFile.TabIndex = 33
        Me.BtnSaveFile.Text = "Guardar"
        Me.BtnSaveFile.UseVisualStyleBackColor = False
        '
        'txtDirectory
        '
        Me.txtDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDirectory.Location = New System.Drawing.Point(136, 49)
        Me.txtDirectory.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtDirectory.Name = "txtDirectory"
        Me.txtDirectory.Size = New System.Drawing.Size(488, 61)
        Me.txtDirectory.TabIndex = 43
        Me.txtDirectory.Text = ""
        '
        'lblDirectory
        '
        Me.lblDirectory.AutoSize = True
        Me.lblDirectory.BackColor = System.Drawing.Color.Transparent
        Me.lblDirectory.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDirectory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblDirectory.Location = New System.Drawing.Point(9, 53)
        Me.lblDirectory.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDirectory.Name = "lblDirectory"
        Me.lblDirectory.Size = New System.Drawing.Size(77, 16)
        Me.lblDirectory.TabIndex = 42
        Me.lblDirectory.Text = "Directorio:"
        Me.lblDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ChkObtainAllFiles
        '
        Me.ChkObtainAllFiles.AutoSize = True
        Me.ChkObtainAllFiles.Location = New System.Drawing.Point(8, 562)
        Me.ChkObtainAllFiles.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ChkObtainAllFiles.Name = "ChkObtainAllFiles"
        Me.ChkObtainAllFiles.Size = New System.Drawing.Size(379, 20)
        Me.ChkObtainAllFiles.TabIndex = 55
        Me.ChkObtainAllFiles.Text = "Obtener los archivos de la ruta con sus subcarpetas."
        Me.ChkObtainAllFiles.UseVisualStyleBackColor = True
        '
        'btnInputExm
        '
        Me.btnInputExm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInputExm.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnInputExm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInputExm.ForeColor = System.Drawing.Color.White
        Me.btnInputExm.Location = New System.Drawing.Point(632, 66)
        Me.btnInputExm.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnInputExm.Name = "btnInputExm"
        Me.btnInputExm.Size = New System.Drawing.Size(101, 28)
        Me.btnInputExm.TabIndex = 43
        Me.btnInputExm.Text = "Examinar"
        Me.btnInputExm.UseVisualStyleBackColor = False
        '
        'GbRule
        '
        Me.GbRule.Controls.Add(Me.lstExtension)
        Me.GbRule.Controls.Add(Me.BtnSaveFile)
        Me.GbRule.Controls.Add(Me.lvlVarName)
        Me.GbRule.Controls.Add(Me.txtVarName)
        Me.GbRule.Controls.Add(Me.ChkObtainAllFiles)
        Me.GbRule.Controls.Add(Me.btnInputExm)
        Me.GbRule.Controls.Add(Me.lblDirectory)
        Me.GbRule.Controls.Add(Me.txtDirectory)
        Me.GbRule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GbRule.Location = New System.Drawing.Point(4, 4)
        Me.GbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GbRule.Name = "GbRule"
        Me.GbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GbRule.Size = New System.Drawing.Size(816, 773)
        Me.GbRule.TabIndex = 56
        Me.GbRule.TabStop = False
        '
        'lstExtension
        '
        Me.lstExtension.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstExtension.FormattingEnabled = True
        Me.lstExtension.ItemHeight = 16
        Me.lstExtension.Location = New System.Drawing.Point(8, 193)
        Me.lstExtension.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lstExtension.Name = "lstExtension"
        Me.lstExtension.Size = New System.Drawing.Size(791, 356)
        Me.lstExtension.TabIndex = 59
        '
        'lvlVarName
        '
        Me.lvlVarName.AutoSize = True
        Me.lvlVarName.BackColor = System.Drawing.Color.Transparent
        Me.lvlVarName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvlVarName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lvlVarName.Location = New System.Drawing.Point(8, 128)
        Me.lvlVarName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lvlVarName.Name = "lvlVarName"
        Me.lvlVarName.Size = New System.Drawing.Size(119, 16)
        Me.lvlVarName.TabIndex = 57
        Me.lvlVarName.Text = "Nombre Variable:"
        Me.lvlVarName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVarName
        '
        Me.txtVarName.Location = New System.Drawing.Point(135, 118)
        Me.txtVarName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtVarName.Name = "txtVarName"
        Me.txtVarName.Size = New System.Drawing.Size(263, 25)
        Me.txtVarName.TabIndex = 56
        Me.txtVarName.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(5, 138)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(199, 13)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "Elija extensiones de archivos a obtener:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCDoGetFiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCDoGetFiles"
        Me.Size = New System.Drawing.Size(832, 810)
        Me.tbRule.ResumeLayout(False)
        Me.tbctrMain.ResumeLayout(False)
        Me.GbRule.ResumeLayout(False)
        Me.GbRule.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnSaveFile As Zamba.AppBlock.ZButton
    Friend WithEvents txtDirectory As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblDirectory As ZLabel
    Friend WithEvents ChkObtainAllFiles As System.Windows.Forms.CheckBox
    Friend WithEvents btnInputExm As Zamba.AppBlock.ZButton
    Friend WithEvents GbRule As System.Windows.Forms.GroupBox
    Friend WithEvents lvlVarName As ZLabel
    Friend WithEvents txtVarName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents lstExtension As System.Windows.Forms.ListBox
End Class
