Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDOGetTask
    Inherits ZRuleControl

#Region " Código generado por el Diseñador de Windows Forms "
#End Region

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

    <System.Diagnostics.DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCDOGetTask))
        Me.lblEnConstruccion = New Zamba.AppBlock.ZLabel()
        Me.lblDocType = New Zamba.AppBlock.ZLabel()
        Me.cmbDocTypes = New System.Windows.Forms.ComboBox()
        Me.zwpIndexsPanel = New Zamba.AppBlock.ZPanel()
        Me.txtVarTaskId = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label5 = New Zamba.AppBlock.ZLabel()
        Me.btnsaverule = New Zamba.AppBlock.ZButton()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.btnsaverule)
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.txtVarTaskId)
        Me.tbRule.Controls.Add(Me.lblEnConstruccion)
        Me.tbRule.Size = New System.Drawing.Size(541, 327)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(549, 356)
        '
        'lblEnConstruccion
        '
        Me.lblEnConstruccion.AutoSize = True
        Me.lblEnConstruccion.BackColor = System.Drawing.Color.Transparent
        Me.lblEnConstruccion.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnConstruccion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblEnConstruccion.Location = New System.Drawing.Point(16, 27)
        Me.lblEnConstruccion.Name = "lblEnConstruccion"
        Me.lblEnConstruccion.Size = New System.Drawing.Size(161, 16)
        Me.lblEnConstruccion.TabIndex = 1
        Me.lblEnConstruccion.Text = "Variable de ID de Tarea"
        Me.lblEnConstruccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDocType
        '
        Me.lblDocType.AutoSize = True
        Me.lblDocType.BackColor = System.Drawing.Color.Transparent
        Me.lblDocType.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblDocType.Location = New System.Drawing.Point(10, 32)
        Me.lblDocType.Name = "lblDocType"
        Me.lblDocType.Size = New System.Drawing.Size(99, 13)
        Me.lblDocType.TabIndex = 13
        Me.lblDocType.Text = "Entidad"
        Me.lblDocType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbDocTypes
        '
        Me.cmbDocTypes.FormattingEnabled = True
        Me.cmbDocTypes.Location = New System.Drawing.Point(127, 29)
        Me.cmbDocTypes.Name = "cmbDocTypes"
        Me.cmbDocTypes.Size = New System.Drawing.Size(245, 21)
        Me.cmbDocTypes.TabIndex = 14
        '
        'zwpIndexsPanel
        '
        Me.zwpIndexsPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.zwpIndexsPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.zwpIndexsPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.zwpIndexsPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.zwpIndexsPanel.Location = New System.Drawing.Point(2, 71)
        Me.zwpIndexsPanel.Name = "zwpIndexsPanel"
        Me.zwpIndexsPanel.Size = New System.Drawing.Size(388, 220)
        Me.zwpIndexsPanel.TabIndex = 15
        '
        'txtVarTaskId
        '
        Me.txtVarTaskId.Location = New System.Drawing.Point(19, 62)
        Me.txtVarTaskId.Name = "txtVarTaskId"
        Me.txtVarTaskId.Size = New System.Drawing.Size(482, 24)
        Me.txtVarTaskId.TabIndex = 2
        Me.txtVarTaskId.Text = "Ingrese la variable o texto inteligente con el id de tarea a obtener"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(16, 144)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(485, 73)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = resources.GetString("Label5.Text")
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnsaverule
        '
        Me.btnsaverule.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnsaverule.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsaverule.ForeColor = System.Drawing.Color.White
        Me.btnsaverule.Location = New System.Drawing.Point(395, 250)
        Me.btnsaverule.Name = "btnsaverule"
        Me.btnsaverule.Size = New System.Drawing.Size(81, 29)
        Me.btnsaverule.TabIndex = 4
        Me.btnsaverule.Text = "Guardar"
        Me.btnsaverule.UseVisualStyleBackColor = False
        '
        'UCDOGetTask
        '
        Me.Name = "UCDOGetTask"
        Me.Size = New System.Drawing.Size(549, 356)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblEnConstruccion As ZLabel

    Friend WithEvents cmbDocTypes As System.Windows.Forms.ComboBox
    Friend WithEvents lblDocType As ZLabel
    Friend WithEvents zwpIndexsPanel As ZPanel
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents txtVarTaskId As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnsaverule As ZButton

End Class
