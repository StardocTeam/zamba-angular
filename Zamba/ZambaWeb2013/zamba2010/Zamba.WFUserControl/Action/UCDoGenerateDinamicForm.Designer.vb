<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoGenerateDinamicForm
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
        Me.lstDocType = New System.Windows.Forms.ListBox
        Me.Label2 = New ZLabel
        Me.txtVariable = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.Label4 = New ZLabel
        Me.btnSave = New ZButton
        Me.Label5 = New ZLabel
        Me.txtFormName = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.Label6 = New ZLabel
        Me.cmbForms = New System.Windows.Forms.ComboBox
        Me.chkNoUsarFormDinamico = New System.Windows.Forms.CheckBox
        Me.Label7 = New ZLabel
        Me.txtColumnaDesc = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.Label7)
        Me.tbRule.Controls.Add(Me.txtColumnaDesc)
        Me.tbRule.Controls.Add(Me.chkNoUsarFormDinamico)
        Me.tbRule.Controls.Add(Me.cmbForms)
        Me.tbRule.Controls.Add(Me.Label6)
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.txtFormName)
        Me.tbRule.Controls.Add(Me.btnSave)
        Me.tbRule.Controls.Add(Me.Label4)
        Me.tbRule.Controls.Add(Me.txtVariable)
        Me.tbRule.Controls.Add(Me.Label2)
        Me.tbRule.Controls.Add(Me.lstDocType)
        '
        'lstDocType
        '
        Me.lstDocType.FormattingEnabled = True
        Me.lstDocType.HorizontalScrollbar = True
        Me.lstDocType.Location = New System.Drawing.Point(24, 39)
        Me.lstDocType.MultiColumn = True
        Me.lstDocType.Name = "lstDocType"
        Me.lstDocType.Size = New System.Drawing.Size(562, 212)
        Me.lstDocType.Sorted = True
        Me.lstDocType.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(151, 18)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Entidades"
        '
        'txtVariable
        '
        Me.txtVariable.Location = New System.Drawing.Point(24, 349)
        Me.txtVariable.Name = "txtVariable"
        Me.txtVariable.Size = New System.Drawing.Size(455, 21)
        Me.txtVariable.TabIndex = 2
        Me.txtVariable.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(21, 328)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 18)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Variable"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(532, 563)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Aceptar"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(21, 267)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(155, 18)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Nombre del Formulario"
        '
        'txtFormName
        '
        Me.txtFormName.Location = New System.Drawing.Point(24, 288)
        Me.txtFormName.Name = "txtFormName"
        Me.txtFormName.Size = New System.Drawing.Size(455, 21)
        Me.txtFormName.TabIndex = 5
        Me.txtFormName.Text = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(21, 448)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(279, 18)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Tipo de formulario para mostrar los datos"
        '
        'cmbForms
        '
        Me.cmbForms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbForms.Enabled = False
        Me.cmbForms.FormattingEnabled = True
        Me.cmbForms.Location = New System.Drawing.Point(24, 469)
        Me.cmbForms.Name = "cmbForms"
        Me.cmbForms.Size = New System.Drawing.Size(455, 21)
        Me.cmbForms.TabIndex = 8
        '
        'chkNoUsarFormDinamico
        '
        Me.chkNoUsarFormDinamico.AutoSize = True
        Me.chkNoUsarFormDinamico.Checked = True
        Me.chkNoUsarFormDinamico.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkNoUsarFormDinamico.Location = New System.Drawing.Point(24, 496)
        Me.chkNoUsarFormDinamico.Name = "chkNoUsarFormDinamico"
        Me.chkNoUsarFormDinamico.Size = New System.Drawing.Size(287, 17)
        Me.chkNoUsarFormDinamico.TabIndex = 9
        Me.chkNoUsarFormDinamico.Text = "No usar un formulario diferente para mostrar los datos"
        Me.chkNoUsarFormDinamico.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(21, 388)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(141, 18)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Columna Descripcion"
        '
        'txtColumnaDesc
        '
        Me.txtColumnaDesc.Location = New System.Drawing.Point(24, 409)
        Me.txtColumnaDesc.Name = "txtColumnaDesc"
        Me.txtColumnaDesc.Size = New System.Drawing.Size(455, 21)
        Me.txtColumnaDesc.TabIndex = 10
        Me.txtColumnaDesc.Text = ""
        '
        'UCDoGenerateDinamicForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoGenerateDinamicForm"
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstDocType As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents txtVariable As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents btnSave As ZButton
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents txtFormName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents cmbForms As System.Windows.Forms.ComboBox
    Friend WithEvents chkNoUsarFormDinamico As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As ZLabel
    Friend WithEvents txtColumnaDesc As Zamba.AppBlock.TextoInteligenteTextBox

End Class
