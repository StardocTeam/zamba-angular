<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoShowForm
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
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.cmbForms = New System.Windows.Forms.ComboBox()
        Me.btnAceptar = New Zamba.AppBlock.ZButton()
        Me.chkAssociatedDocDataShow = New System.Windows.Forms.CheckBox()
        Me.lblVarDoc_id = New Zamba.AppBlock.ZLabel()
        Me.txtVarDoc_id = New System.Windows.Forms.TextBox()
        Me.chkShowDialogMaximized = New System.Windows.Forms.CheckBox()
        Me.chkViewOriginal = New System.Windows.Forms.CheckBox()
        Me.chkCerrarConCruz = New System.Windows.Forms.CheckBox()
        Me.chkCloseFormAfterRuleExecution = New System.Windows.Forms.CheckBox()
        Me.chkViewAsoc = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
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
        Me.tbRule.Controls.Add(Me.CheckBox1)
        Me.tbRule.Controls.Add(Me.chkViewAsoc)
        Me.tbRule.Controls.Add(Me.chkCloseFormAfterRuleExecution)
        Me.tbRule.Controls.Add(Me.chkCerrarConCruz)
        Me.tbRule.Controls.Add(Me.chkViewOriginal)
        Me.tbRule.Controls.Add(Me.chkShowDialogMaximized)
        Me.tbRule.Controls.Add(Me.chkAssociatedDocDataShow)
        Me.tbRule.Controls.Add(Me.btnAceptar)
        Me.tbRule.Controls.Add(Me.Label1)
        Me.tbRule.Controls.Add(Me.cmbForms)
        Me.tbRule.Controls.Add(Me.lblVarDoc_id)
        Me.tbRule.Controls.Add(Me.txtVarDoc_id)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(697, 398)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(705, 427)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.FontSize = 9.75!
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(53, 52)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Tipo de Formulario"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbForms
        '
        Me.cmbForms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbForms.FormattingEnabled = True
        Me.cmbForms.Location = New System.Drawing.Point(190, 52)
        Me.cmbForms.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbForms.Name = "cmbForms"
        Me.cmbForms.Size = New System.Drawing.Size(385, 24)
        Me.cmbForms.TabIndex = 0
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ForeColor = System.Drawing.Color.White
        Me.btnAceptar.Location = New System.Drawing.Point(450, 351)
        Me.btnAceptar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(125, 28)
        Me.btnAceptar.TabIndex = 2
        Me.btnAceptar.Text = "Guardar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'chkAssociatedDocDataShow
        '
        Me.chkAssociatedDocDataShow.AutoSize = True
        Me.chkAssociatedDocDataShow.Location = New System.Drawing.Point(57, 154)
        Me.chkAssociatedDocDataShow.Margin = New System.Windows.Forms.Padding(4)
        Me.chkAssociatedDocDataShow.Name = "chkAssociatedDocDataShow"
        Me.chkAssociatedDocDataShow.Size = New System.Drawing.Size(281, 20)
        Me.chkAssociatedDocDataShow.TabIndex = 3
        Me.chkAssociatedDocDataShow.Text = "Mostrar datos de documento asociado"
        Me.chkAssociatedDocDataShow.UseVisualStyleBackColor = True
        '
        'lblVarDoc_id
        '
        Me.lblVarDoc_id.AutoSize = True
        Me.lblVarDoc_id.BackColor = System.Drawing.Color.Transparent
        Me.lblVarDoc_id.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVarDoc_id.FontSize = 9.75!
        Me.lblVarDoc_id.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblVarDoc_id.Location = New System.Drawing.Point(53, 101)
        Me.lblVarDoc_id.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVarDoc_id.Name = "lblVarDoc_id"
        Me.lblVarDoc_id.Size = New System.Drawing.Size(107, 16)
        Me.lblVarDoc_id.TabIndex = 6
        Me.lblVarDoc_id.Text = "Variable doc_id"
        Me.lblVarDoc_id.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVarDoc_id
        '
        Me.txtVarDoc_id.Location = New System.Drawing.Point(190, 97)
        Me.txtVarDoc_id.Margin = New System.Windows.Forms.Padding(4)
        Me.txtVarDoc_id.Name = "txtVarDoc_id"
        Me.txtVarDoc_id.Size = New System.Drawing.Size(385, 23)
        Me.txtVarDoc_id.TabIndex = 5
        '
        'chkShowDialogMaximized
        '
        Me.chkShowDialogMaximized.AutoSize = True
        Me.chkShowDialogMaximized.Location = New System.Drawing.Point(57, 182)
        Me.chkShowDialogMaximized.Margin = New System.Windows.Forms.Padding(4)
        Me.chkShowDialogMaximized.Name = "chkShowDialogMaximized"
        Me.chkShowDialogMaximized.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShowDialogMaximized.Size = New System.Drawing.Size(219, 20)
        Me.chkShowDialogMaximized.TabIndex = 7
        Me.chkShowDialogMaximized.Text = "Mostrar dialogo sin maximizar"
        Me.chkShowDialogMaximized.UseVisualStyleBackColor = True
        '
        'chkViewOriginal
        '
        Me.chkViewOriginal.AutoSize = True
        Me.chkViewOriginal.Location = New System.Drawing.Point(57, 210)
        Me.chkViewOriginal.Margin = New System.Windows.Forms.Padding(4)
        Me.chkViewOriginal.Name = "chkViewOriginal"
        Me.chkViewOriginal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkViewOriginal.Size = New System.Drawing.Size(177, 20)
        Me.chkViewOriginal.TabIndex = 8
        Me.chkViewOriginal.Text = "Ver documento original"
        Me.chkViewOriginal.UseVisualStyleBackColor = True
        '
        'chkCerrarConCruz
        '
        Me.chkCerrarConCruz.AutoSize = True
        Me.chkCerrarConCruz.Location = New System.Drawing.Point(57, 267)
        Me.chkCerrarConCruz.Margin = New System.Windows.Forms.Padding(4)
        Me.chkCerrarConCruz.Name = "chkCerrarConCruz"
        Me.chkCerrarConCruz.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCerrarConCruz.Size = New System.Drawing.Size(335, 20)
        Me.chkCerrarConCruz.TabIndex = 9
        Me.chkCerrarConCruz.Text = "Mostrar minimizar, restaurar y cerrar formulario"
        Me.chkCerrarConCruz.UseVisualStyleBackColor = True
        '
        'chkCloseFormAfterRuleExecution
        '
        Me.chkCloseFormAfterRuleExecution.AutoSize = True
        Me.chkCloseFormAfterRuleExecution.Location = New System.Drawing.Point(57, 295)
        Me.chkCloseFormAfterRuleExecution.Margin = New System.Windows.Forms.Padding(4)
        Me.chkCloseFormAfterRuleExecution.Name = "chkCloseFormAfterRuleExecution"
        Me.chkCloseFormAfterRuleExecution.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCloseFormAfterRuleExecution.Size = New System.Drawing.Size(470, 20)
        Me.chkCloseFormAfterRuleExecution.TabIndex = 10
        Me.chkCloseFormAfterRuleExecution.Text = "Cerrar la ventana luego de ejecutar cualquier regla en el formulario"
        Me.chkCloseFormAfterRuleExecution.UseVisualStyleBackColor = True
        '
        'chkViewAsoc
        '
        Me.chkViewAsoc.AutoSize = True
        Me.chkViewAsoc.Location = New System.Drawing.Point(57, 239)
        Me.chkViewAsoc.Margin = New System.Windows.Forms.Padding(4)
        Me.chkViewAsoc.Name = "chkViewAsoc"
        Me.chkViewAsoc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkViewAsoc.Size = New System.Drawing.Size(160, 20)
        Me.chkViewAsoc.TabIndex = 11
        Me.chkViewAsoc.Text = "Consultar Asociados"
        Me.chkViewAsoc.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(57, 323)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CheckBox1.Size = New System.Drawing.Size(363, 20)
        Me.CheckBox1.TabIndex = 12
        Me.CheckBox1.Text = "Refrescar la Tarea Abierta, cambiando el formulario"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'UCDoShowForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCDoShowForm"
        Me.Size = New System.Drawing.Size(705, 427)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbForms As System.Windows.Forms.ComboBox
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents chkAssociatedDocDataShow As System.Windows.Forms.CheckBox
    Friend WithEvents lblVarDoc_id As ZLabel
    Friend WithEvents txtVarDoc_id As System.Windows.Forms.TextBox
    Friend WithEvents chkShowDialogMaximized As System.Windows.Forms.CheckBox
    Friend WithEvents chkViewOriginal As System.Windows.Forms.CheckBox
    Friend WithEvents chkCerrarConCruz As System.Windows.Forms.CheckBox
    Friend WithEvents chkCloseFormAfterRuleExecution As System.Windows.Forms.CheckBox
    Friend WithEvents chkViewAsoc As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As CheckBox
End Class
