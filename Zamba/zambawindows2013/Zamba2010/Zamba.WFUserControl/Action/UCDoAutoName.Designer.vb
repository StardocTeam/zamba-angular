<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoAutoName
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
    Private Overloads Sub InitializeComponent()
        Me.lblSecondaryValue = New ZLabel()
        Me.txtVariableDocId = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.btnAceptar = New ZButton()
        Me.Label2 = New ZLabel()
        Me.RdbUsarActual = New System.Windows.Forms.RadioButton()
        Me.RdbEspecificarId = New System.Windows.Forms.RadioButton()
        Me.Label5 = New ZLabel()
        Me.TxtVariableDocTypeId = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label6 = New ZLabel()
        Me.TxtVariableNombreDeLaColumna = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.pnlIndividual = New System.Windows.Forms.Panel()
        Me.lblUpdateMode = New ZLabel()
        Me.rdoIndividual = New System.Windows.Forms.RadioButton()
        Me.rdoMultiple = New System.Windows.Forms.RadioButton()
        Me.pnlMultiple = New System.Windows.Forms.Panel()
        Me.Label3 = New ZLabel()
        Me.lblSelected = New ZLabel()
        Me.lblDocTypes = New ZLabel()
        Me.chkDocTypeList = New System.Windows.Forms.CheckedListBox()
        Me.txtDays = New System.Windows.Forms.TextBox()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.pnlIndividual.SuspendLayout()
        Me.pnlMultiple.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Size = New System.Drawing.Size(451, 399)
        '
        'tbHabilitation
        '
        Me.tbHabilitation.Size = New System.Drawing.Size(451, 399)
        '
        'tbConfiguration
        '
        Me.tbConfiguration.Size = New System.Drawing.Size(451, 399)
        '
        'tbAlerts
        '
        Me.tbAlerts.Size = New System.Drawing.Size(451, 399)
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.pnlMultiple)
        Me.tbRule.Controls.Add(Me.rdoMultiple)
        Me.tbRule.Controls.Add(Me.rdoIndividual)
        Me.tbRule.Controls.Add(Me.lblUpdateMode)
        Me.tbRule.Controls.Add(Me.pnlIndividual)
        Me.tbRule.Controls.Add(Me.btnAceptar)
        Me.tbRule.Size = New System.Drawing.Size(451, 399)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(459, 425)
        '
        'lblSecondaryValue
        '
        Me.lblSecondaryValue.AutoSize = True
        Me.lblSecondaryValue.Location = New System.Drawing.Point(3, 45)
        Me.lblSecondaryValue.Name = "lblSecondaryValue"
        Me.lblSecondaryValue.Size = New System.Drawing.Size(79, 13)
        Me.lblSecondaryValue.TabIndex = 41
        Me.lblSecondaryValue.Text = "Variable Doc Id"
        '
        'txtVariableDocId
        '
        Me.txtVariableDocId.Location = New System.Drawing.Point(127, 42)
        Me.txtVariableDocId.Name = "txtVariableDocId"
        Me.txtVariableDocId.Size = New System.Drawing.Size(298, 21)
        Me.txtVariableDocId.TabIndex = 39
        Me.txtVariableDocId.Text = ""
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(134, 213)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(78, 23)
        Me.btnAceptar.TabIndex = 40
        Me.btnAceptar.Text = "Aceptar"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(116, 13)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "Seleccione una opción:"
        '
        'RdbUsarActual
        '
        Me.RdbUsarActual.AutoSize = True
        Me.RdbUsarActual.Location = New System.Drawing.Point(127, 7)
        Me.RdbUsarActual.Name = "RdbUsarActual"
        Me.RdbUsarActual.Size = New System.Drawing.Size(80, 17)
        Me.RdbUsarActual.TabIndex = 44
        Me.RdbUsarActual.TabStop = True
        Me.RdbUsarActual.Text = "Usar Actual"
        Me.RdbUsarActual.UseVisualStyleBackColor = True
        '
        'RdbEspecificarId
        '
        Me.RdbEspecificarId.AutoSize = True
        Me.RdbEspecificarId.Location = New System.Drawing.Point(228, 7)
        Me.RdbEspecificarId.Name = "RdbEspecificarId"
        Me.RdbEspecificarId.Size = New System.Drawing.Size(89, 17)
        Me.RdbEspecificarId.TabIndex = 45
        Me.RdbEspecificarId.TabStop = True
        Me.RdbEspecificarId.Text = "Especificar Id"
        Me.RdbEspecificarId.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 82)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(110, 13)
        Me.Label5.TabIndex = 47
        Me.Label5.Text = "Variable Doc Type Id:"
        '
        'TxtVariableDocTypeId
        '
        Me.TxtVariableDocTypeId.Location = New System.Drawing.Point(127, 82)
        Me.TxtVariableDocTypeId.Name = "TxtVariableDocTypeId"
        Me.TxtVariableDocTypeId.Size = New System.Drawing.Size(298, 21)
        Me.TxtVariableDocTypeId.TabIndex = 46
        Me.TxtVariableDocTypeId.Text = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(1, 124)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(118, 13)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "Nombre de la Columna:"
        '
        'TxtVariableNombreDeLaColumna
        '
        Me.TxtVariableNombreDeLaColumna.Location = New System.Drawing.Point(127, 121)
        Me.TxtVariableNombreDeLaColumna.Name = "TxtVariableNombreDeLaColumna"
        Me.TxtVariableNombreDeLaColumna.Size = New System.Drawing.Size(298, 21)
        Me.TxtVariableNombreDeLaColumna.TabIndex = 49
        Me.TxtVariableNombreDeLaColumna.Text = ""
        '
        'pnlIndividual
        '
        Me.pnlIndividual.BackColor = System.Drawing.Color.Transparent
        Me.pnlIndividual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlIndividual.Controls.Add(Me.Label2)
        Me.pnlIndividual.Controls.Add(Me.TxtVariableNombreDeLaColumna)
        Me.pnlIndividual.Controls.Add(Me.txtVariableDocId)
        Me.pnlIndividual.Controls.Add(Me.Label6)
        Me.pnlIndividual.Controls.Add(Me.lblSecondaryValue)
        Me.pnlIndividual.Controls.Add(Me.Label5)
        Me.pnlIndividual.Controls.Add(Me.RdbUsarActual)
        Me.pnlIndividual.Controls.Add(Me.TxtVariableDocTypeId)
        Me.pnlIndividual.Controls.Add(Me.RdbEspecificarId)
        Me.pnlIndividual.Location = New System.Drawing.Point(6, 54)
        Me.pnlIndividual.Name = "pnlIndividual"
        Me.pnlIndividual.Size = New System.Drawing.Size(439, 153)
        Me.pnlIndividual.TabIndex = 50
        '
        'lblUpdateMode
        '
        Me.lblUpdateMode.AutoSize = True
        Me.lblUpdateMode.Location = New System.Drawing.Point(12, 21)
        Me.lblUpdateMode.Name = "lblUpdateMode"
        Me.lblUpdateMode.Size = New System.Drawing.Size(116, 13)
        Me.lblUpdateMode.TabIndex = 51
        Me.lblUpdateMode.Text = "Modo de actualización:"
        '
        'rdoIndividual
        '
        Me.rdoIndividual.AutoSize = True
        Me.rdoIndividual.Location = New System.Drawing.Point(134, 19)
        Me.rdoIndividual.Name = "rdoIndividual"
        Me.rdoIndividual.Size = New System.Drawing.Size(71, 17)
        Me.rdoIndividual.TabIndex = 52
        Me.rdoIndividual.TabStop = True
        Me.rdoIndividual.Text = "Individual"
        Me.rdoIndividual.UseVisualStyleBackColor = True
        '
        'rdoMultiple
        '
        Me.rdoMultiple.AutoSize = True
        Me.rdoMultiple.Location = New System.Drawing.Point(235, 19)
        Me.rdoMultiple.Name = "rdoMultiple"
        Me.rdoMultiple.Size = New System.Drawing.Size(61, 17)
        Me.rdoMultiple.TabIndex = 53
        Me.rdoMultiple.TabStop = True
        Me.rdoMultiple.Text = "Multiple"
        Me.rdoMultiple.UseVisualStyleBackColor = True
        '
        'pnlMultiple
        '
        Me.pnlMultiple.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMultiple.Controls.Add(Me.txtDays)
        Me.pnlMultiple.Controls.Add(Me.Label3)
        Me.pnlMultiple.Controls.Add(Me.lblSelected)
        Me.pnlMultiple.Controls.Add(Me.lblDocTypes)
        Me.pnlMultiple.Controls.Add(Me.chkDocTypeList)
        Me.pnlMultiple.Location = New System.Drawing.Point(6, 242)
        Me.pnlMultiple.Name = "pnlMultiple"
        Me.pnlMultiple.Size = New System.Drawing.Size(439, 153)
        Me.pnlMultiple.TabIndex = 54
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 131)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(178, 13)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "Dias desde modificacion (0 sin filtro)"
        '
        'lblSelected
        '
        Me.lblSelected.AutoSize = True
        Me.lblSelected.Location = New System.Drawing.Point(6, 112)
        Me.lblSelected.Name = "lblSelected"
        Me.lblSelected.Size = New System.Drawing.Size(78, 13)
        Me.lblSelected.TabIndex = 2
        Me.lblSelected.Text = "Seleccionados:"
        '
        'lblDocTypes
        '
        Me.lblDocTypes.AutoSize = True
        Me.lblDocTypes.Location = New System.Drawing.Point(6, 9)
        Me.lblDocTypes.Name = "lblDocTypes"
        Me.lblDocTypes.Size = New System.Drawing.Size(117, 26)
        Me.lblDocTypes.TabIndex = 1
        Me.lblDocTypes.Text = "Seleccione los tipos de " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "documento deseados"
        '
        'chkDocTypeList
        '
        Me.chkDocTypeList.FormattingEnabled = True
        Me.chkDocTypeList.Location = New System.Drawing.Point(127, 9)
        Me.chkDocTypeList.Name = "chkDocTypeList"
        Me.chkDocTypeList.Size = New System.Drawing.Size(298, 116)
        Me.chkDocTypeList.TabIndex = 0
        '
        'txtDays
        '
        Me.txtDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays.Location = New System.Drawing.Point(325, 128)
        Me.txtDays.Name = "txtDays"
        Me.txtDays.Size = New System.Drawing.Size(100, 21)
        Me.txtDays.TabIndex = 51
        Me.txtDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'UCDoAutoName
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoAutoName"
        Me.Size = New System.Drawing.Size(459, 425)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.pnlIndividual.ResumeLayout(False)
        Me.pnlIndividual.PerformLayout()
        Me.pnlMultiple.ResumeLayout(False)
        Me.pnlMultiple.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents lblSecondaryValue As ZLabel
    Friend WithEvents txtVariableDocId As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents RdbEspecificarId As System.Windows.Forms.RadioButton
    Friend WithEvents RdbUsarActual As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents TxtVariableDocTypeId As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents TxtVariableNombreDeLaColumna As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents pnlIndividual As System.Windows.Forms.Panel
    Friend WithEvents rdoMultiple As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIndividual As System.Windows.Forms.RadioButton
    Friend WithEvents lblUpdateMode As ZLabel
    Friend WithEvents pnlMultiple As System.Windows.Forms.Panel
    Friend WithEvents chkDocTypeList As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblDocTypes As ZLabel
    Friend WithEvents lblSelected As ZLabel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents txtDays As System.Windows.Forms.TextBox
End Class