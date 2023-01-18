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
        Me.clbReglasActivas = New System.Windows.Forms.CheckedListBox
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.Label3 = New ZLabel
        Me.btnAplicar = New ZButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.ChkEjecutarConTabs = New System.Windows.Forms.CheckBox
        Me.chkDesactivarTareaActual = New System.Windows.Forms.CheckBox
        Me.rdbActivarReglas = New System.Windows.Forms.RadioButton
        Me.rdbDesactivarReglas = New System.Windows.Forms.RadioButton
        Me.lblSeleccion = New ZLabel
        Me.rdbTodoElWorkFlow = New System.Windows.Forms.RadioButton
        Me.rdbEtapaActual = New System.Windows.Forms.RadioButton
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.GroupBox4)
        Me.tbRule.Controls.Add(Me.btnAplicar)
        Me.tbRule.Controls.Add(Me.GroupBox3)
        Me.tbRule.Controls.Add(Me.TableLayoutPanel3)
        Me.tbRule.Size = New System.Drawing.Size(648, 377)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(656, 403)
        '
        'clbReglasActivas
        '
        Me.clbReglasActivas.BackColor = System.Drawing.SystemColors.Window
        Me.clbReglasActivas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbReglasActivas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbReglasActivas.FormattingEnabled = True
        Me.clbReglasActivas.Location = New System.Drawing.Point(3, 50)
        Me.clbReglasActivas.Name = "clbReglasActivas"
        Me.clbReglasActivas.Size = New System.Drawing.Size(390, 306)
        Me.clbReglasActivas.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.clbReglasActivas, 0, 2)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(4, 6)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 321.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(396, 362)
        Me.TableLayoutPanel3.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(287, 28)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Activa o Desactiva Reglas"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAplicar
        '
        Me.btnAplicar.Location = New System.Drawing.Point(583, 342)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.Size = New System.Drawing.Size(56, 26)
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
        Me.GroupBox3.Location = New System.Drawing.Point(406, 155)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(233, 147)
        Me.GroupBox3.TabIndex = 15
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Configuración"
        '
        'ChkEjecutarConTabs
        '
        Me.ChkEjecutarConTabs.AutoSize = True
        Me.ChkEjecutarConTabs.BackColor = System.Drawing.Color.Transparent
        Me.ChkEjecutarConTabs.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkEjecutarConTabs.Location = New System.Drawing.Point(12, 115)
        Me.ChkEjecutarConTabs.Name = "ChkEjecutarConTabs"
        Me.ChkEjecutarConTabs.Size = New System.Drawing.Size(215, 17)
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
        Me.chkDesactivarTareaActual.Location = New System.Drawing.Point(12, 92)
        Me.chkDesactivarTareaActual.Name = "chkDesactivarTareaActual"
        Me.chkDesactivarTareaActual.Size = New System.Drawing.Size(157, 17)
        Me.chkDesactivarTareaActual.TabIndex = 5
        Me.chkDesactivarTareaActual.Text = "Aplica solo a la tarea actual"
        Me.chkDesactivarTareaActual.UseVisualStyleBackColor = False
        '
        'rdbActivarReglas
        '
        Me.rdbActivarReglas.AutoSize = True
        Me.rdbActivarReglas.BackColor = System.Drawing.Color.Transparent
        Me.rdbActivarReglas.Location = New System.Drawing.Point(12, 40)
        Me.rdbActivarReglas.Name = "rdbActivarReglas"
        Me.rdbActivarReglas.Size = New System.Drawing.Size(94, 17)
        Me.rdbActivarReglas.TabIndex = 7
        Me.rdbActivarReglas.TabStop = True
        Me.rdbActivarReglas.Text = "Activar Reglas"
        Me.rdbActivarReglas.UseVisualStyleBackColor = False
        '
        'rdbDesactivarReglas
        '
        Me.rdbDesactivarReglas.AutoSize = True
        Me.rdbDesactivarReglas.BackColor = System.Drawing.Color.Transparent
        Me.rdbDesactivarReglas.Location = New System.Drawing.Point(12, 60)
        Me.rdbDesactivarReglas.Name = "rdbDesactivarReglas"
        Me.rdbDesactivarReglas.Size = New System.Drawing.Size(111, 17)
        Me.rdbDesactivarReglas.TabIndex = 8
        Me.rdbDesactivarReglas.TabStop = True
        Me.rdbDesactivarReglas.Text = "Desactivar Reglas"
        Me.rdbDesactivarReglas.UseVisualStyleBackColor = False
        '
        'lblSeleccion
        '
        Me.lblSeleccion.AutoSize = True
        Me.lblSeleccion.BackColor = System.Drawing.Color.Transparent
        Me.lblSeleccion.Location = New System.Drawing.Point(11, 24)
        Me.lblSeleccion.Name = "lblSeleccion"
        Me.lblSeleccion.Size = New System.Drawing.Size(55, 13)
        Me.lblSeleccion.TabIndex = 11
        Me.lblSeleccion.Text = "Selección:"
        '
        'rdbTodoElWorkFlow
        '
        Me.rdbTodoElWorkFlow.AutoSize = True
        Me.rdbTodoElWorkFlow.BackColor = System.Drawing.Color.Transparent
        Me.rdbTodoElWorkFlow.Location = New System.Drawing.Point(13, 44)
        Me.rdbTodoElWorkFlow.Name = "rdbTodoElWorkFlow"
        Me.rdbTodoElWorkFlow.Size = New System.Drawing.Size(158, 17)
        Me.rdbTodoElWorkFlow.TabIndex = 15
        Me.rdbTodoElWorkFlow.TabStop = True
        Me.rdbTodoElWorkFlow.Text = "Reglas de todo el WorkFlow"
        Me.rdbTodoElWorkFlow.UseVisualStyleBackColor = False
        '
        'rdbEtapaActual
        '
        Me.rdbEtapaActual.AutoSize = True
        Me.rdbEtapaActual.BackColor = System.Drawing.Color.Transparent
        Me.rdbEtapaActual.Location = New System.Drawing.Point(13, 21)
        Me.rdbEtapaActual.Name = "rdbEtapaActual"
        Me.rdbEtapaActual.Size = New System.Drawing.Size(147, 17)
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
        Me.GroupBox4.Location = New System.Drawing.Point(406, 76)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(184, 69)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Visualización"
        '
        'UcDoEnableRule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UcDoEnableRule"
        Me.Size = New System.Drawing.Size(656, 403)
        Me.tbRule.ResumeLayout(False)
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

End Class