<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoCloseTask
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
        Me.lblTaskId = New Zamba.AppBlock.ZLabel()
        Me.txtTaskId = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.btnSaveValues = New Zamba.AppBlock.ZButton()
        Me.ZLabel1 = New Zamba.AppBlock.ZLabel()
        Me.rdcloseparentonly = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdcloseparenttoo = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdrefreshparent = New Telerik.WinControls.UI.RadRadioButton()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        CType(Me.rdcloseparentonly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdcloseparenttoo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rdrefreshparent, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.tbRule.Controls.Add(Me.RadGroupBox1)
        Me.tbRule.Controls.Add(Me.ZLabel1)
        Me.tbRule.Controls.Add(Me.lblTaskId)
        Me.tbRule.Controls.Add(Me.btnSaveValues)
        Me.tbRule.Controls.Add(Me.txtTaskId)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(591, 450)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(599, 479)
        '
        'lblTaskId
        '
        Me.lblTaskId.AutoSize = True
        Me.lblTaskId.BackColor = System.Drawing.Color.Transparent
        Me.lblTaskId.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaskId.FontSize = 9.75!
        Me.lblTaskId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblTaskId.Location = New System.Drawing.Point(20, 16)
        Me.lblTaskId.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTaskId.Name = "lblTaskId"
        Me.lblTaskId.Size = New System.Drawing.Size(198, 16)
        Me.lblTaskId.TabIndex = 0
        Me.lblTaskId.Text = "Id de tarea (TaskId) a cerrar"
        Me.lblTaskId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTaskId
        '
        Me.txtTaskId.Location = New System.Drawing.Point(24, 36)
        Me.txtTaskId.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtTaskId.MaxLength = 4000
        Me.txtTaskId.Name = "txtTaskId"
        Me.txtTaskId.Size = New System.Drawing.Size(448, 25)
        Me.txtTaskId.TabIndex = 0
        Me.txtTaskId.Text = ""
        '
        'btnSaveValues
        '
        Me.btnSaveValues.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSaveValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveValues.ForeColor = System.Drawing.Color.White
        Me.btnSaveValues.Location = New System.Drawing.Point(23, 296)
        Me.btnSaveValues.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSaveValues.Name = "btnSaveValues"
        Me.btnSaveValues.Size = New System.Drawing.Size(119, 28)
        Me.btnSaveValues.TabIndex = 1
        Me.btnSaveValues.Text = "Guardar"
        Me.btnSaveValues.UseVisualStyleBackColor = True
        '
        'ZLabel1
        '
        Me.ZLabel1.AutoSize = True
        Me.ZLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel1.FontSize = 9.75!
        Me.ZLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel1.Location = New System.Drawing.Point(20, 79)
        Me.ZLabel1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ZLabel1.Name = "ZLabel1"
        Me.ZLabel1.Size = New System.Drawing.Size(398, 16)
        Me.ZLabel1.TabIndex = 2
        Me.ZLabel1.Text = "Se puede dejar el campo vacio, para cerrar la Tarea actual"
        Me.ZLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rdcloseparentonly
        '
        Me.rdcloseparentonly.AutoSize = False
        Me.rdcloseparentonly.IsThreeState = True
        Me.rdcloseparentonly.Location = New System.Drawing.Point(5, 79)
        Me.rdcloseparentonly.Name = "rdcloseparentonly"
        Me.rdcloseparentonly.Size = New System.Drawing.Size(538, 39)
        Me.rdcloseparentonly.TabIndex = 6
        Me.rdcloseparentonly.Text = "Cerrar la Ventana Origen y no la Actual (Cierra unicamente la Tarea origen o la v" &
    "entana que abrio la tarea a cerrar)"
        Me.rdcloseparentonly.TextWrap = True
        '
        'rdcloseparenttoo
        '
        Me.rdcloseparenttoo.AutoScroll = True
        Me.rdcloseparenttoo.AutoSize = False
        Me.rdcloseparenttoo.IsThreeState = True
        Me.rdcloseparenttoo.Location = New System.Drawing.Point(5, 40)
        Me.rdcloseparenttoo.Name = "rdcloseparenttoo"
        Me.rdcloseparenttoo.Size = New System.Drawing.Size(500, 33)
        Me.rdcloseparenttoo.TabIndex = 7
        Me.rdcloseparenttoo.Text = "Cerrar la Ventana Origen (Cierra tambien la Tarea origen o la ventana que abrio l" &
    "a tarea a cerrar)"
        Me.rdcloseparenttoo.TextWrap = True
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rdrefreshparent)
        Me.RadGroupBox1.Controls.Add(Me.rdcloseparenttoo)
        Me.RadGroupBox1.Controls.Add(Me.rdcloseparentonly)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(22, 152)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(548, 123)
        Me.RadGroupBox1.TabIndex = 8
        '
        'rdrefreshparent
        '
        Me.rdrefreshparent.AutoScroll = True
        Me.rdrefreshparent.AutoSize = False
        Me.rdrefreshparent.IsThreeState = True
        Me.rdrefreshparent.Location = New System.Drawing.Point(5, 1)
        Me.rdrefreshparent.Name = "rdrefreshparent"
        Me.rdrefreshparent.Size = New System.Drawing.Size(500, 33)
        Me.rdrefreshparent.TabIndex = 8
        Me.rdrefreshparent.Text = "Actualizar Ventana Origen (Actualiza la Tarea origen o la ventana que abrio la ta" &
    "rea a cerrar)"
        Me.rdrefreshparent.TextWrap = True
        '
        'UCDoCloseTask
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCDoCloseTask"
        Me.Size = New System.Drawing.Size(599, 479)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        CType(Me.rdcloseparentonly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdcloseparenttoo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.rdrefreshparent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSaveValues As ZButton
    Friend WithEvents txtTaskId As New Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblTaskId As ZLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdcloseparenttoo As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdcloseparentonly As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents ZLabel1 As ZLabel
    Friend WithEvents rdrefreshparent As Telerik.WinControls.UI.RadRadioButton
End Class
