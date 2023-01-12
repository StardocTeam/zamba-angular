<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoDecodeFile
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
        Me.lblReadFile = New Zamba.AppBlock.ZLabel()
        Me.txtBinary = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.txtPath = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label5 = New Zamba.AppBlock.ZLabel()
        Me.txtFileName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label6 = New Zamba.AppBlock.ZLabel()
        Me.txtVar = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.btnguardar = New Zamba.AppBlock.ZButton()
        Me.txtstart = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label7 = New Zamba.AppBlock.ZLabel()
        Me.txtend = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label8 = New Zamba.AppBlock.ZLabel()
        Me.txtext = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label9 = New Zamba.AppBlock.ZLabel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
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
        Me.tbRule.Controls.Add(Me.txtext)
        Me.tbRule.Controls.Add(Me.Label9)
        Me.tbRule.Controls.Add(Me.txtend)
        Me.tbRule.Controls.Add(Me.Label8)
        Me.tbRule.Controls.Add(Me.txtstart)
        Me.tbRule.Controls.Add(Me.Label7)
        Me.tbRule.Controls.Add(Me.btnguardar)
        Me.tbRule.Controls.Add(Me.txtVar)
        Me.tbRule.Controls.Add(Me.Label6)
        Me.tbRule.Controls.Add(Me.txtFileName)
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.txtPath)
        Me.tbRule.Controls.Add(Me.Label3)
        Me.tbRule.Controls.Add(Me.txtBinary)
        Me.tbRule.Controls.Add(Me.lblReadFile)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(824, 781)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(832, 810)
        '
        'lblReadFile
        '
        Me.lblReadFile.AutoSize = True
        Me.lblReadFile.BackColor = System.Drawing.Color.Transparent
        Me.lblReadFile.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReadFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblReadFile.Location = New System.Drawing.Point(31, 34)
        Me.lblReadFile.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblReadFile.Name = "lblReadFile"
        Me.lblReadFile.Size = New System.Drawing.Size(99, 16)
        Me.lblReadFile.TabIndex = 28
        Me.lblReadFile.Text = "Texto binario:"
        Me.lblReadFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBinary
        '
        Me.txtBinary.Location = New System.Drawing.Point(155, 31)
        Me.txtBinary.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtBinary.MaxLength = 4000
        Me.txtBinary.Name = "txtBinary"
        Me.txtBinary.Size = New System.Drawing.Size(563, 25)
        Me.txtBinary.TabIndex = 29
        Me.txtBinary.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(31, 167)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 16)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Path destino:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(155, 164)
        Me.txtPath.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtPath.MaxLength = 4000
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(564, 25)
        Me.txtPath.TabIndex = 31
        Me.txtPath.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(31, 213)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(117, 16)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Nombre Archivo:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(155, 209)
        Me.txtFileName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtFileName.MaxLength = 4000
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(564, 25)
        Me.txtFileName.TabIndex = 33
        Me.txtFileName.Text = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(31, 292)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(177, 16)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "Guardar path en variable:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVar
        '
        Me.txtVar.Location = New System.Drawing.Point(213, 288)
        Me.txtVar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtVar.MaxLength = 4000
        Me.txtVar.Name = "txtVar"
        Me.txtVar.Size = New System.Drawing.Size(505, 25)
        Me.txtVar.TabIndex = 35
        Me.txtVar.Text = ""
        '
        'btnguardar
        '
        Me.btnguardar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnguardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnguardar.ForeColor = System.Drawing.Color.White
        Me.btnguardar.Location = New System.Drawing.Point(573, 336)
        Me.btnguardar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnguardar.Name = "btnguardar"
        Me.btnguardar.Size = New System.Drawing.Size(101, 28)
        Me.btnguardar.TabIndex = 45
        Me.btnguardar.Text = "Guardar"
        Me.btnguardar.UseVisualStyleBackColor = False
        '
        'txtstart
        '
        Me.txtstart.Location = New System.Drawing.Point(155, 75)
        Me.txtstart.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtstart.MaxLength = 4000
        Me.txtstart.Name = "txtstart"
        Me.txtstart.Size = New System.Drawing.Size(563, 25)
        Me.txtstart.TabIndex = 47
        Me.txtstart.Text = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(31, 79)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(95, 16)
        Me.Label7.TabIndex = 46
        Me.Label7.Text = "Texto desde:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtend
        '
        Me.txtend.Location = New System.Drawing.Point(155, 119)
        Me.txtend.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtend.MaxLength = 4000
        Me.txtend.Name = "txtend"
        Me.txtend.Size = New System.Drawing.Size(563, 25)
        Me.txtend.TabIndex = 49
        Me.txtend.Text = ""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(31, 123)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(93, 16)
        Me.Label8.TabIndex = 48
        Me.Label8.Text = "Texto hasta:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtext
        '
        Me.txtext.Location = New System.Drawing.Point(155, 247)
        Me.txtext.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtext.MaxLength = 4000
        Me.txtext.Name = "txtext"
        Me.txtext.Size = New System.Drawing.Size(563, 25)
        Me.txtext.TabIndex = 51
        Me.txtext.Text = ""
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(31, 251)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(77, 16)
        Me.Label9.TabIndex = 50
        Me.Label9.Text = "Extension:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCDoDecodeFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCDoDecodeFile"
        Me.Size = New System.Drawing.Size(832, 810)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblReadFile As ZLabel
    Friend WithEvents txtBinary As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents txtPath As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents txtVar As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents txtFileName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnguardar As Zamba.AppBlock.ZButton
    Friend WithEvents txtend As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label8 As ZLabel
    Friend WithEvents txtstart As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label7 As ZLabel
    Friend WithEvents txtext As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label9 As ZLabel

End Class
