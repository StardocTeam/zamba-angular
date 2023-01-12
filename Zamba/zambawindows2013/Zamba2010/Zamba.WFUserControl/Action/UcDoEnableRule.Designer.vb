Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UcDoEnableRule
    Inherits ZRuleControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        ' MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Overloads Sub InitializeComponent()
        Me.clbReglasActivas = New System.Windows.Forms.CheckedListBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.btnAplicar = New Zamba.AppBlock.ZButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ChkEjecutarConTabs = New System.Windows.Forms.CheckBox()
        Me.chkDesactivarTareaActual = New System.Windows.Forms.CheckBox()
        Me.rdbActivarReglas = New System.Windows.Forms.RadioButton()
        Me.rdbDesactivarReglas = New System.Windows.Forms.RadioButton()
        Me.lblSeleccion = New Zamba.AppBlock.ZLabel()
        Me.rdbTodoElWorkFlow = New System.Windows.Forms.RadioButton()
        Me.rdbEtapaActual = New System.Windows.Forms.RadioButton()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
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
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.TextBox1)
        Me.tbRule.Controls.Add(Me.GroupBox4)
        Me.tbRule.Controls.Add(Me.btnAplicar)
        Me.tbRule.Controls.Add(Me.GroupBox3)
        Me.tbRule.Controls.Add(Me.TableLayoutPanel3)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(867, 467)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(875, 496)
        '
        'clbReglasActivas
        '
        Me.clbReglasActivas.BackColor = System.Drawing.SystemColors.Window
        Me.clbReglasActivas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbReglasActivas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbReglasActivas.FormattingEnabled = True
        Me.clbReglasActivas.Location = New System.Drawing.Point(4, 61)
        Me.clbReglasActivas.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.clbReglasActivas.Name = "clbReglasActivas"
        Me.clbReglasActivas.Size = New System.Drawing.Size(520, 387)
        Me.clbReglasActivas.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.clbReglasActivas, 0, 2)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(5, 66)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 395.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 138.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(528, 387)
        Me.TableLayoutPanel3.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.FontSize = 8.25!
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(287, 28)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Activa o Desactiva Reglas"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAplicar
        '
        Me.btnAplicar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAplicar.ForeColor = System.Drawing.Color.White
        Me.btnAplicar.Location = New System.Drawing.Point(777, 421)
        Me.btnAplicar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.Size = New System.Drawing.Size(75, 32)
        Me.btnAplicar.TabIndex = 3
        Me.btnAplicar.Text = "Guardar"
        Me.btnAplicar.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.ChkEjecutarConTabs)
        Me.GroupBox3.Controls.Add(Me.chkDesactivarTareaActual)
        Me.GroupBox3.Controls.Add(Me.rdbActivarReglas)
        Me.GroupBox3.Controls.Add(Me.rdbDesactivarReglas)
        Me.GroupBox3.Controls.Add(Me.lblSeleccion)
        Me.GroupBox3.Location = New System.Drawing.Point(541, 191)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(311, 181)
        Me.GroupBox3.TabIndex = 15
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Configuración"
        '
        'ChkEjecutarConTabs
        '
        Me.ChkEjecutarConTabs.AutoSize = True
        Me.ChkEjecutarConTabs.BackColor = System.Drawing.Color.Transparent
        Me.ChkEjecutarConTabs.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkEjecutarConTabs.Location = New System.Drawing.Point(16, 142)
        Me.ChkEjecutarConTabs.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ChkEjecutarConTabs.Name = "ChkEjecutarConTabs"
        Me.ChkEjecutarConTabs.Size = New System.Drawing.Size(291, 20)
        Me.ChkEjecutarConTabs.TabIndex = 12
        Me.ChkEjecutarConTabs.Text = "Ejecutar con la configuracion de Estado"
        Me.ChkEjecutarConTabs.UseVisualStyleBackColor = False
        '
        'chkDesactivarTareaActual
        '
        Me.chkDesactivarTareaActual.AutoSize = True
        Me.chkDesactivarTareaActual.BackColor = System.Drawing.Color.Transparent
        Me.chkDesactivarTareaActual.Checked = True
        Me.chkDesactivarTareaActual.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDesactivarTareaActual.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkDesactivarTareaActual.Location = New System.Drawing.Point(16, 113)
        Me.chkDesactivarTareaActual.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkDesactivarTareaActual.Name = "chkDesactivarTareaActual"
        Me.chkDesactivarTareaActual.Size = New System.Drawing.Size(212, 20)
        Me.chkDesactivarTareaActual.TabIndex = 5
        Me.chkDesactivarTareaActual.Text = "Aplica solo a la tarea actual"
        Me.chkDesactivarTareaActual.UseVisualStyleBackColor = False
        '
        'rdbActivarReglas
        '
        Me.rdbActivarReglas.AutoSize = True
        Me.rdbActivarReglas.BackColor = System.Drawing.Color.Transparent
        Me.rdbActivarReglas.Location = New System.Drawing.Point(16, 49)
        Me.rdbActivarReglas.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbActivarReglas.Name = "rdbActivarReglas"
        Me.rdbActivarReglas.Size = New System.Drawing.Size(120, 20)
        Me.rdbActivarReglas.TabIndex = 7
        Me.rdbActivarReglas.TabStop = True
        Me.rdbActivarReglas.Text = "Activar Reglas"
        Me.rdbActivarReglas.UseVisualStyleBackColor = False
        '
        'rdbDesactivarReglas
        '
        Me.rdbDesactivarReglas.AutoSize = True
        Me.rdbDesactivarReglas.BackColor = System.Drawing.Color.Transparent
        Me.rdbDesactivarReglas.Location = New System.Drawing.Point(16, 74)
        Me.rdbDesactivarReglas.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbDesactivarReglas.Name = "rdbDesactivarReglas"
        Me.rdbDesactivarReglas.Size = New System.Drawing.Size(143, 20)
        Me.rdbDesactivarReglas.TabIndex = 8
        Me.rdbDesactivarReglas.TabStop = True
        Me.rdbDesactivarReglas.Text = "Desactivar Reglas"
        Me.rdbDesactivarReglas.UseVisualStyleBackColor = False
        '
        'lblSeleccion
        '
        Me.lblSeleccion.AutoSize = True
        Me.lblSeleccion.BackColor = System.Drawing.Color.Transparent
        Me.lblSeleccion.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeleccion.FontSize = 9.75!
        Me.lblSeleccion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblSeleccion.Location = New System.Drawing.Point(15, 30)
        Me.lblSeleccion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSeleccion.Name = "lblSeleccion"
        Me.lblSeleccion.Size = New System.Drawing.Size(77, 16)
        Me.lblSeleccion.TabIndex = 11
        Me.lblSeleccion.Text = "Selección:"
        Me.lblSeleccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rdbTodoElWorkFlow
        '
        Me.rdbTodoElWorkFlow.AutoSize = True
        Me.rdbTodoElWorkFlow.BackColor = System.Drawing.Color.Transparent
        Me.rdbTodoElWorkFlow.Location = New System.Drawing.Point(17, 54)
        Me.rdbTodoElWorkFlow.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbTodoElWorkFlow.Name = "rdbTodoElWorkFlow"
        Me.rdbTodoElWorkFlow.Size = New System.Drawing.Size(208, 20)
        Me.rdbTodoElWorkFlow.TabIndex = 15
        Me.rdbTodoElWorkFlow.TabStop = True
        Me.rdbTodoElWorkFlow.Text = "Reglas de todo el WorkFlow"
        Me.rdbTodoElWorkFlow.UseVisualStyleBackColor = False
        '
        'rdbEtapaActual
        '
        Me.rdbEtapaActual.AutoSize = True
        Me.rdbEtapaActual.BackColor = System.Drawing.Color.Transparent
        Me.rdbEtapaActual.Location = New System.Drawing.Point(17, 26)
        Me.rdbEtapaActual.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbEtapaActual.Name = "rdbEtapaActual"
        Me.rdbEtapaActual.Size = New System.Drawing.Size(195, 20)
        Me.rdbEtapaActual.TabIndex = 14
        Me.rdbEtapaActual.TabStop = True
        Me.rdbEtapaActual.Text = "Reglas de la Etapa Actual"
        Me.rdbEtapaActual.UseVisualStyleBackColor = False
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.rdbTodoElWorkFlow)
        Me.GroupBox4.Controls.Add(Me.rdbEtapaActual)
        Me.GroupBox4.Location = New System.Drawing.Point(541, 94)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox4.Size = New System.Drawing.Size(245, 85)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Visualización"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(9, 35)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(839, 23)
        Me.TextBox1.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 16)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Ids de Reglas"
        '
        'UcDoEnableRule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UcDoEnableRule"
        Me.Size = New System.Drawing.Size(875, 496)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    'Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents clbReglasActivas As System.Windows.Forms.CheckedListBox
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnAplicar As ZButton
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkDesactivarTareaActual As System.Windows.Forms.CheckBox
    Friend WithEvents rdbActivarReglas As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDesactivarReglas As System.Windows.Forms.RadioButton
    Friend WithEvents lblSeleccion As ZLabel
    Friend WithEvents rdbTodoElWorkFlow As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEtapaActual As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents ChkEjecutarConTabs As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox1 As TextBox
End Class