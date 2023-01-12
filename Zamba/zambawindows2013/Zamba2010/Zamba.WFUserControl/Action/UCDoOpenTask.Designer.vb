<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoOpenTask
    Inherits ZRuleControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Overloads Sub InitializeComponent()
        Me.ChkUseCurrentTask = New System.Windows.Forms.CheckBox()
        Me.txtTaskID = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblTaskID = New Zamba.AppBlock.ZLabel()
        Me.txtDocID = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblDocID = New Zamba.AppBlock.ZLabel()
        Me.Btn_Save = New Zamba.AppBlock.ZButton()
        Me.lblChooseTextBox = New Zamba.AppBlock.ZLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdnewtab = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdnewwindow = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdmodal = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdself = New Telerik.WinControls.UI.RadRadioButton()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rdnewtab, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdnewwindow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmodal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdself, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.tbRule.Controls.Add(Me.RadGroupBox1)
        Me.tbRule.Controls.Add(Me.lblChooseTextBox)
        Me.tbRule.Controls.Add(Me.Btn_Save)
        Me.tbRule.Controls.Add(Me.lblDocID)
        Me.tbRule.Controls.Add(Me.txtDocID)
        Me.tbRule.Controls.Add(Me.lblTaskID)
        Me.tbRule.Controls.Add(Me.txtTaskID)
        Me.tbRule.Controls.Add(Me.ChkUseCurrentTask)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(592, 439)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(600, 468)
        '
        'ChkUseCurrentTask
        '
        Me.ChkUseCurrentTask.AutoSize = True
        Me.ChkUseCurrentTask.Location = New System.Drawing.Point(24, 192)
        Me.ChkUseCurrentTask.Margin = New System.Windows.Forms.Padding(4)
        Me.ChkUseCurrentTask.Name = "ChkUseCurrentTask"
        Me.ChkUseCurrentTask.Size = New System.Drawing.Size(157, 20)
        Me.ChkUseCurrentTask.TabIndex = 3
        Me.ChkUseCurrentTask.Text = "Utilizar tarea actual"
        Me.ChkUseCurrentTask.UseVisualStyleBackColor = True
        '
        'txtTaskID
        '
        Me.txtTaskID.Location = New System.Drawing.Point(90, 91)
        Me.txtTaskID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTaskID.Name = "txtTaskID"
        Me.txtTaskID.Size = New System.Drawing.Size(481, 25)
        Me.txtTaskID.TabIndex = 4
        Me.txtTaskID.Text = ""
        '
        'lblTaskID
        '
        Me.lblTaskID.AutoSize = True
        Me.lblTaskID.BackColor = System.Drawing.Color.Transparent
        Me.lblTaskID.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblTaskID.FontSize = 9.75!
        Me.lblTaskID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblTaskID.Location = New System.Drawing.Point(20, 100)
        Me.lblTaskID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTaskID.Name = "lblTaskID"
        Me.lblTaskID.Size = New System.Drawing.Size(62, 16)
        Me.lblTaskID.TabIndex = 5
        Me.lblTaskID.Text = "Task ID:"
        Me.lblTaskID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDocID
        '
        Me.txtDocID.Location = New System.Drawing.Point(90, 146)
        Me.txtDocID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDocID.Name = "txtDocID"
        Me.txtDocID.Size = New System.Drawing.Size(481, 25)
        Me.txtDocID.TabIndex = 6
        Me.txtDocID.Text = ""
        '
        'lblDocID
        '
        Me.lblDocID.AutoSize = True
        Me.lblDocID.BackColor = System.Drawing.Color.Transparent
        Me.lblDocID.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblDocID.FontSize = 9.75!
        Me.lblDocID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblDocID.Location = New System.Drawing.Point(20, 155)
        Me.lblDocID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDocID.Name = "lblDocID"
        Me.lblDocID.Size = New System.Drawing.Size(58, 16)
        Me.lblDocID.TabIndex = 7
        Me.lblDocID.Text = "Doc ID:"
        Me.lblDocID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Btn_Save
        '
        Me.Btn_Save.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Save.ForeColor = System.Drawing.Color.White
        Me.Btn_Save.Location = New System.Drawing.Point(23, 371)
        Me.Btn_Save.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Save.Name = "Btn_Save"
        Me.Btn_Save.Size = New System.Drawing.Size(129, 37)
        Me.Btn_Save.TabIndex = 8
        Me.Btn_Save.Text = "Guardar"
        Me.Btn_Save.UseVisualStyleBackColor = True
        '
        'lblChooseTextBox
        '
        Me.lblChooseTextBox.AutoSize = True
        Me.lblChooseTextBox.BackColor = System.Drawing.Color.Transparent
        Me.lblChooseTextBox.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblChooseTextBox.FontSize = 9.75!
        Me.lblChooseTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblChooseTextBox.Location = New System.Drawing.Point(129, 52)
        Me.lblChooseTextBox.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblChooseTextBox.Name = "lblChooseTextBox"
        Me.lblChooseTextBox.Size = New System.Drawing.Size(178, 16)
        Me.lblChooseTextBox.TabIndex = 9
        Me.lblChooseTextBox.Text = "Ingrese Task ID o Doc ID:"
        Me.lblChooseTextBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rdself)
        Me.RadGroupBox1.Controls.Add(Me.rdmodal)
        Me.RadGroupBox1.Controls.Add(Me.rdnewwindow)
        Me.RadGroupBox1.Controls.Add(Me.rdnewtab)
        Me.RadGroupBox1.HeaderText = "Seleccione tipo de apertura"
        Me.RadGroupBox1.Location = New System.Drawing.Point(23, 220)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(195, 144)
        Me.RadGroupBox1.TabIndex = 10
        Me.RadGroupBox1.Text = "Seleccione tipo de apertura"
        '
        'rdnewtab
        '
        Me.rdnewtab.Location = New System.Drawing.Point(18, 33)
        Me.rdnewtab.Name = "rdnewtab"
        Me.rdnewtab.Size = New System.Drawing.Size(89, 18)
        Me.rdnewtab.TabIndex = 0
        Me.rdnewtab.Text = "Nueva Solapa"
        '
        'rdnewwindow
        '
        Me.rdnewwindow.Location = New System.Drawing.Point(18, 58)
        Me.rdnewwindow.Name = "rdnewwindow"
        Me.rdnewwindow.Size = New System.Drawing.Size(96, 18)
        Me.rdnewwindow.TabIndex = 1
        Me.rdnewwindow.Text = "Nueva Ventana"
        '
        'rdmodal
        '
        Me.rdmodal.Location = New System.Drawing.Point(18, 83)
        Me.rdmodal.Name = "rdmodal"
        Me.rdmodal.Size = New System.Drawing.Size(94, 18)
        Me.rdmodal.TabIndex = 2
        Me.rdmodal.Text = "Dialogo Modal"
        '
        'rdself
        '
        Me.rdself.Location = New System.Drawing.Point(18, 108)
        Me.rdself.Name = "rdself"
        Me.rdself.Size = New System.Drawing.Size(98, 18)
        Me.rdself.TabIndex = 3
        Me.rdself.Text = "Misma Ventana"
        '
        'UCDoOpenTask
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCDoOpenTask"
        Me.Size = New System.Drawing.Size(600, 468)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rdnewtab, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdnewwindow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmodal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdself, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ChkUseCurrentTask As System.Windows.Forms.CheckBox
    Friend WithEvents lblTaskID As ZLabel
    Friend WithEvents txtTaskID As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblDocID As ZLabel
    Friend WithEvents txtDocID As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Btn_Save As ZButton
    Friend WithEvents lblChooseTextBox As ZLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdself As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdmodal As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdnewwindow As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdnewtab As Telerik.WinControls.UI.RadRadioButton
End Class