<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGeneralRules
    Inherits Zamba.AppBlock.ZForm

    'Form overrides dispose to clean up the component list.
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
        Me.lblRule = New ZLabel()
        Me.lblPlace = New ZLabel()
        Me.cboPlace = New System.Windows.Forms.ComboBox()
        Me.btnOK = New Zamba.AppBlock.ZButton()
        Me.btnCancel = New Zamba.AppBlock.ZButton()
        Me.cboRules = New System.Windows.Forms.ComboBox()
        Me.lblOrder = New ZLabel()
        Me.txtOrder = New System.Windows.Forms.TextBox()
        Me.txtCaption = New System.Windows.Forms.TextBox()
        Me.lblCaption = New ZLabel()
        Me.chkForOneWf = New System.Windows.Forms.CheckBox()
        Me.cmbSelectIcon = New System.Windows.Forms.ComboBox()
        Me.lblSelectIcon = New ZLabel()
        Me.txtCssClass = New System.Windows.Forms.TextBox()
        Me.ZLabel1 = New ZLabel()
        Me.txtGroupName = New System.Windows.Forms.TextBox()
        Me.ZLabel2 = New ZLabel()
        Me.txtGroupClass = New System.Windows.Forms.TextBox()
        Me.ZLabel3 = New ZLabel()
        Me.txtParams = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblParams = New ZLabel()
        Me.chkNeedRights = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'lblRule
        '
        Me.lblRule.AutoSize = True
        Me.lblRule.BackColor = System.Drawing.Color.Transparent
        Me.lblRule.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRule.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.lblRule.Location = New System.Drawing.Point(12, 14)
        Me.lblRule.Name = "lblRule"
        Me.lblRule.Size = New System.Drawing.Size(36, 14)
        Me.lblRule.TabIndex = 0
        Me.lblRule.Text = "Regla"
        Me.lblRule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPlace
        '
        Me.lblPlace.AutoSize = True
        Me.lblPlace.BackColor = System.Drawing.Color.Transparent
        Me.lblPlace.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlace.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.lblPlace.Location = New System.Drawing.Point(12, 56)
        Me.lblPlace.Name = "lblPlace"
        Me.lblPlace.Size = New System.Drawing.Size(58, 14)
        Me.lblPlace.TabIndex = 2
        Me.lblPlace.Text = "Ubicación"
        Me.lblPlace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboPlace
        '
        Me.cboPlace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPlace.FormattingEnabled = True
        Me.cboPlace.Location = New System.Drawing.Point(15, 73)
        Me.cboPlace.Name = "cboPlace"
        Me.cboPlace.Size = New System.Drawing.Size(273, 22)
        Me.cboPlace.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(294, 277)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(148, 30)
        Me.btnOK.TabIndex = 11
        Me.btnOK.Text = "Aceptar"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(448, 277)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(148, 30)
        Me.btnCancel.TabIndex = 12
        Me.btnCancel.Text = "Cancelar"
        '
        'cboRules
        '
        Me.cboRules.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboRules.FormattingEnabled = True
        Me.cboRules.Location = New System.Drawing.Point(15, 31)
        Me.cboRules.Name = "cboRules"
        Me.cboRules.Size = New System.Drawing.Size(581, 22)
        Me.cboRules.TabIndex = 0
        '
        'lblOrder
        '
        Me.lblOrder.AutoSize = True
        Me.lblOrder.BackColor = System.Drawing.Color.Transparent
        Me.lblOrder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrder.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.lblOrder.Location = New System.Drawing.Point(13, 280)
        Me.lblOrder.Name = "lblOrder"
        Me.lblOrder.Size = New System.Drawing.Size(41, 14)
        Me.lblOrder.TabIndex = 10
        Me.lblOrder.Text = "Orden"
        Me.lblOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblOrder.Visible = False
        '
        'txtOrder
        '
        Me.txtOrder.Enabled = False
        Me.txtOrder.Location = New System.Drawing.Point(60, 277)
        Me.txtOrder.Name = "txtOrder"
        Me.txtOrder.Size = New System.Drawing.Size(51, 22)
        Me.txtOrder.TabIndex = 9
        Me.txtOrder.Visible = False
        '
        'txtCaption
        '
        Me.txtCaption.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCaption.Location = New System.Drawing.Point(291, 73)
        Me.txtCaption.Name = "txtCaption"
        Me.txtCaption.Size = New System.Drawing.Size(305, 22)
        Me.txtCaption.TabIndex = 2
        '
        'lblCaption
        '
        Me.lblCaption.AutoSize = True
        Me.lblCaption.BackColor = System.Drawing.Color.Transparent
        Me.lblCaption.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCaption.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.lblCaption.Location = New System.Drawing.Point(288, 56)
        Me.lblCaption.Name = "lblCaption"
        Me.lblCaption.Size = New System.Drawing.Size(308, 14)
        Me.lblCaption.TabIndex = 12
        Me.lblCaption.Text = "Leyenda del botón (por defecto el nombre de la regla)"
        Me.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkForOneWf
        '
        Me.chkForOneWf.AutoSize = True
        Me.chkForOneWf.Enabled = False
        Me.chkForOneWf.Location = New System.Drawing.Point(15, 104)
        Me.chkForOneWf.Name = "chkForOneWf"
        Me.chkForOneWf.Size = New System.Drawing.Size(238, 18)
        Me.chkForOneWf.TabIndex = 3
        Me.chkForOneWf.Text = "Mostrar solo en el Workflow de la regla"
        Me.chkForOneWf.UseVisualStyleBackColor = True
        '
        'cmbSelectIcon
        '
        Me.cmbSelectIcon.FormattingEnabled = True
        Me.cmbSelectIcon.Location = New System.Drawing.Point(542, 102)
        Me.cmbSelectIcon.Name = "cmbSelectIcon"
        Me.cmbSelectIcon.Size = New System.Drawing.Size(54, 22)
        Me.cmbSelectIcon.TabIndex = 5
        '
        'lblSelectIcon
        '
        Me.lblSelectIcon.AutoSize = True
        Me.lblSelectIcon.Location = New System.Drawing.Point(498, 105)
        Me.lblSelectIcon.Name = "lblSelectIcon"
        Me.lblSelectIcon.Size = New System.Drawing.Size(38, 14)
        Me.lblSelectIcon.TabIndex = 16
        Me.lblSelectIcon.Text = "Icono"
        '
        'txtCssClass
        '
        Me.txtCssClass.Enabled = False
        Me.txtCssClass.Location = New System.Drawing.Point(15, 165)
        Me.txtCssClass.Name = "txtCssClass"
        Me.txtCssClass.Size = New System.Drawing.Size(273, 22)
        Me.txtCssClass.TabIndex = 6
        '
        'ZLabel1
        '
        Me.ZLabel1.AutoSize = True
        Me.ZLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZLabel1.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.ZLabel1.Location = New System.Drawing.Point(12, 148)
        Me.ZLabel1.Name = "ZLabel1"
        Me.ZLabel1.Size = New System.Drawing.Size(147, 14)
        Me.ZLabel1.TabIndex = 18
        Me.ZLabel1.Text = "Clase CSS(Solo para web)"
        Me.ZLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtGroupName
        '

        Me.txtGroupName.Location = New System.Drawing.Point(15, 207)
        Me.txtGroupName.Name = "txtGroupName"
        Me.txtGroupName.Size = New System.Drawing.Size(273, 22)
        Me.txtGroupName.TabIndex = 7
        '
        'ZLabel2
        '
        Me.ZLabel2.AutoSize = True
        Me.ZLabel2.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZLabel2.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.ZLabel2.Location = New System.Drawing.Point(12, 190)
        Me.ZLabel2.Name = "ZLabel2"
        Me.ZLabel2.Size = New System.Drawing.Size(192, 14)
        Me.ZLabel2.TabIndex = 20
        Me.ZLabel2.Text = "Nombre de grupo"
        Me.ZLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtGroupClass
        '
        Me.txtGroupClass.Enabled = False
        Me.txtGroupClass.Location = New System.Drawing.Point(15, 249)
        Me.txtGroupClass.Name = "txtGroupClass"
        Me.txtGroupClass.Size = New System.Drawing.Size(273, 22)
        Me.txtGroupClass.TabIndex = 8
        '
        'ZLabel3
        '
        Me.ZLabel3.AutoSize = True
        Me.ZLabel3.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZLabel3.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.ZLabel3.Location = New System.Drawing.Point(12, 232)
        Me.ZLabel3.Name = "ZLabel3"
        Me.ZLabel3.Size = New System.Drawing.Size(176, 14)
        Me.ZLabel3.TabIndex = 22
        Me.ZLabel3.Text = "Clase de grupo(Solo para web)"
        Me.ZLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtParams
        '
        Me.txtParams.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtParams.Location = New System.Drawing.Point(294, 165)
        Me.txtParams.Name = "txtParams"
        Me.txtParams.Size = New System.Drawing.Size(302, 106)
        Me.txtParams.TabIndex = 10
        Me.txtParams.Text = ""
        '
        'lblParams
        '
        Me.lblParams.AutoSize = True
        Me.lblParams.BackColor = System.Drawing.Color.Transparent
        Me.lblParams.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblParams.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.lblParams.Location = New System.Drawing.Point(291, 148)
        Me.lblParams.Name = "lblParams"
        Me.lblParams.Size = New System.Drawing.Size(128, 14)
        Me.lblParams.TabIndex = 24
        Me.lblParams.Text = "Parámetros opcionales"
        Me.lblParams.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkNeedRights
        '
        Me.chkNeedRights.AutoSize = True
        Me.chkNeedRights.Checked = True
        Me.chkNeedRights.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkNeedRights.Location = New System.Drawing.Point(291, 104)
        Me.chkNeedRights.Name = "chkNeedRights"
        Me.chkNeedRights.Size = New System.Drawing.Size(180, 18)
        Me.chkNeedRights.TabIndex = 4
        Me.chkNeedRights.Text = "Verificar permisos de usuario"
        Me.chkNeedRights.UseVisualStyleBackColor = True
        '
        'frmGeneralRules
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(614, 314)
        Me.Controls.Add(Me.chkNeedRights)
        Me.Controls.Add(Me.lblParams)
        Me.Controls.Add(Me.txtParams)
        Me.Controls.Add(Me.ZLabel3)
        Me.Controls.Add(Me.txtGroupClass)
        Me.Controls.Add(Me.txtGroupName)
        Me.Controls.Add(Me.ZLabel2)
        Me.Controls.Add(Me.txtCssClass)
        Me.Controls.Add(Me.ZLabel1)
        Me.Controls.Add(Me.lblSelectIcon)
        Me.Controls.Add(Me.cmbSelectIcon)
        Me.Controls.Add(Me.chkForOneWf)
        Me.Controls.Add(Me.txtCaption)
        Me.Controls.Add(Me.lblCaption)
        Me.Controls.Add(Me.txtOrder)
        Me.Controls.Add(Me.lblOrder)
        Me.Controls.Add(Me.cboRules)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.cboPlace)
        Me.Controls.Add(Me.lblPlace)
        Me.Controls.Add(Me.lblRule)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(407, 224)
        Me.Name = "frmGeneralRules"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " Configuración de reglas generales"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblRule As ZLabel
    Friend WithEvents lblPlace As ZLabel
    Friend WithEvents cboPlace As System.Windows.Forms.ComboBox
    Friend WithEvents btnOK As Zamba.AppBlock.ZButton
    Friend WithEvents btnCancel As Zamba.AppBlock.ZButton
    Friend WithEvents cboRules As System.Windows.Forms.ComboBox
    Friend WithEvents lblOrder As ZLabel
    Friend WithEvents txtOrder As System.Windows.Forms.TextBox
    Friend WithEvents txtCaption As System.Windows.Forms.TextBox
    Friend WithEvents lblCaption As ZLabel
    Friend WithEvents chkForOneWf As System.Windows.Forms.CheckBox
    Friend WithEvents cmbSelectIcon As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelectIcon As ZLabel
    Friend WithEvents txtCssClass As System.Windows.Forms.TextBox
    Friend WithEvents ZLabel1 As ZLabel
    Friend WithEvents txtGroupName As System.Windows.Forms.TextBox
    Friend WithEvents ZLabel2 As ZLabel
    Friend WithEvents txtGroupClass As System.Windows.Forms.TextBox
    Friend WithEvents ZLabel3 As ZLabel
    Friend WithEvents txtParams As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblParams As ZLabel
    Friend WithEvents chkNeedRights As System.Windows.Forms.CheckBox
End Class
