<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCDOApproveMatrix
    Inherits ZRuleControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Overloads Sub InitializeComponent()
        Me.CBOIndexs1 = New System.Windows.Forms.ComboBox()
        Me.CBOIndexs2 = New System.Windows.Forms.ComboBox()
        Me.CBOIndexs3 = New System.Windows.Forms.ComboBox()
        Me.CBOLevel = New System.Windows.Forms.ComboBox()
        Me.CBOAmount = New System.Windows.Forms.ComboBox()
        Me.CBOSecuence = New System.Windows.Forms.ComboBox()
        Me.CBOEntities = New System.Windows.Forms.ComboBox()
        Me.LabelIndex1 = New Zamba.AppBlock.ZLabel()
        Me.LabelIndex2 = New Zamba.AppBlock.ZLabel()
        Me.LabelIndex3 = New Zamba.AppBlock.ZLabel()
        Me.LabelAmount = New Zamba.AppBlock.ZLabel()
        Me.LabelLevel = New Zamba.AppBlock.ZLabel()
        Me.LabelSecuence = New Zamba.AppBlock.ZLabel()
        Me.LabelEntity = New Zamba.AppBlock.ZLabel()
        Me.BTNADD = New Zamba.AppBlock.ZButton()
        Me.ZLabelVariable1 = New Zamba.AppBlock.ZLabel()
        Me.TextoInteligenteVariable1 = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.ZLabelVariable2 = New Zamba.AppBlock.ZLabel()
        Me.TextoInteligenteVariable2 = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.ZLabelVariable3 = New Zamba.AppBlock.ZLabel()
        Me.TextoInteligenteVariable3 = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.ZLabel1 = New Zamba.AppBlock.ZLabel()
        Me.TextoInteligenteApprover = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.TextoInteligenteSecuence = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.ZLabel2 = New Zamba.AppBlock.ZLabel()
        Me.TextoInteligenteLevel = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.ZLabel3 = New Zamba.AppBlock.ZLabel()
        Me.ZLabel4 = New Zamba.AppBlock.ZLabel()
        Me.CBORegisterEntity = New System.Windows.Forms.ComboBox()
        Me.ZLabel5 = New Zamba.AppBlock.ZLabel()
        Me.CBORegistryIdIndex = New System.Windows.Forms.ComboBox()
        Me.ZLabel6 = New Zamba.AppBlock.ZLabel()
        Me.TextoInteligenteActions = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.ZLabel7 = New Zamba.AppBlock.ZLabel()
        Me.CBOActionIndex = New System.Windows.Forms.ComboBox()
        Me.ZLabel8 = New Zamba.AppBlock.ZLabel()
        Me.CBOApprover = New System.Windows.Forms.ComboBox()
        Me.TextoInteligenteIsApproverVariable = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.ZLabel9 = New Zamba.AppBlock.ZLabel()
        Me.TextoInteligenteApproversListVariable = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.ZLabel10 = New Zamba.AppBlock.ZLabel()
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
        Me.tbRule.Controls.Add(Me.TextoInteligenteApproversListVariable)
        Me.tbRule.Controls.Add(Me.ZLabel10)
        Me.tbRule.Controls.Add(Me.TextoInteligenteIsApproverVariable)
        Me.tbRule.Controls.Add(Me.ZLabel9)
        Me.tbRule.Controls.Add(Me.ZLabel8)
        Me.tbRule.Controls.Add(Me.CBOApprover)
        Me.tbRule.Controls.Add(Me.ZLabel7)
        Me.tbRule.Controls.Add(Me.CBOActionIndex)
        Me.tbRule.Controls.Add(Me.TextoInteligenteActions)
        Me.tbRule.Controls.Add(Me.ZLabel6)
        Me.tbRule.Controls.Add(Me.ZLabel5)
        Me.tbRule.Controls.Add(Me.CBORegistryIdIndex)
        Me.tbRule.Controls.Add(Me.ZLabel4)
        Me.tbRule.Controls.Add(Me.CBORegisterEntity)
        Me.tbRule.Controls.Add(Me.TextoInteligenteLevel)
        Me.tbRule.Controls.Add(Me.ZLabel3)
        Me.tbRule.Controls.Add(Me.TextoInteligenteSecuence)
        Me.tbRule.Controls.Add(Me.ZLabel2)
        Me.tbRule.Controls.Add(Me.TextoInteligenteApprover)
        Me.tbRule.Controls.Add(Me.ZLabel1)
        Me.tbRule.Controls.Add(Me.TextoInteligenteVariable1)
        Me.tbRule.Controls.Add(Me.ZLabelVariable1)
        Me.tbRule.Controls.Add(Me.TextoInteligenteVariable2)
        Me.tbRule.Controls.Add(Me.ZLabelVariable2)
        Me.tbRule.Controls.Add(Me.TextoInteligenteVariable3)
        Me.tbRule.Controls.Add(Me.ZLabelVariable3)
        Me.tbRule.Controls.Add(Me.BTNADD)
        Me.tbRule.Controls.Add(Me.LabelIndex1)
        Me.tbRule.Controls.Add(Me.LabelAmount)
        Me.tbRule.Controls.Add(Me.LabelIndex2)
        Me.tbRule.Controls.Add(Me.LabelIndex3)
        Me.tbRule.Controls.Add(Me.LabelLevel)
        Me.tbRule.Controls.Add(Me.LabelSecuence)
        Me.tbRule.Controls.Add(Me.CBOIndexs1)
        Me.tbRule.Controls.Add(Me.CBOIndexs2)
        Me.tbRule.Controls.Add(Me.CBOIndexs3)
        Me.tbRule.Controls.Add(Me.CBOLevel)
        Me.tbRule.Controls.Add(Me.CBOAmount)
        Me.tbRule.Controls.Add(Me.CBOSecuence)
        Me.tbRule.Controls.Add(Me.LabelEntity)
        Me.tbRule.Controls.Add(Me.CBOEntities)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(964, 522)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(972, 551)
        '
        'CBOIndexs1
        '
        Me.CBOIndexs1.FormattingEnabled = True
        Me.CBOIndexs1.Location = New System.Drawing.Point(239, 299)
        Me.CBOIndexs1.Margin = New System.Windows.Forms.Padding(4)
        Me.CBOIndexs1.Name = "CBOIndexs1"
        Me.CBOIndexs1.Size = New System.Drawing.Size(282, 24)
        Me.CBOIndexs1.TabIndex = 0
        '
        'CBOIndexs2
        '
        Me.CBOIndexs2.FormattingEnabled = True
        Me.CBOIndexs2.Location = New System.Drawing.Point(239, 367)
        Me.CBOIndexs2.Margin = New System.Windows.Forms.Padding(4)
        Me.CBOIndexs2.Name = "CBOIndexs2"
        Me.CBOIndexs2.Size = New System.Drawing.Size(282, 24)
        Me.CBOIndexs2.TabIndex = 0
        '
        'CBOIndexs3
        '
        Me.CBOIndexs3.FormattingEnabled = True
        Me.CBOIndexs3.Location = New System.Drawing.Point(239, 435)
        Me.CBOIndexs3.Margin = New System.Windows.Forms.Padding(4)
        Me.CBOIndexs3.Name = "CBOIndexs3"
        Me.CBOIndexs3.Size = New System.Drawing.Size(282, 24)
        Me.CBOIndexs3.TabIndex = 0
        '
        'CBOLevel
        '
        Me.CBOLevel.FormattingEnabled = True
        Me.CBOLevel.Location = New System.Drawing.Point(155, 146)
        Me.CBOLevel.Margin = New System.Windows.Forms.Padding(4)
        Me.CBOLevel.Name = "CBOLevel"
        Me.CBOLevel.Size = New System.Drawing.Size(282, 24)
        Me.CBOLevel.TabIndex = 0
        '
        'CBOAmount
        '
        Me.CBOAmount.FormattingEnabled = True
        Me.CBOAmount.Location = New System.Drawing.Point(155, 82)
        Me.CBOAmount.Margin = New System.Windows.Forms.Padding(4)
        Me.CBOAmount.Name = "CBOAmount"
        Me.CBOAmount.Size = New System.Drawing.Size(282, 24)
        Me.CBOAmount.TabIndex = 0
        '
        'CBOSecuence
        '
        Me.CBOSecuence.FormattingEnabled = True
        Me.CBOSecuence.Location = New System.Drawing.Point(155, 114)
        Me.CBOSecuence.Margin = New System.Windows.Forms.Padding(4)
        Me.CBOSecuence.Name = "CBOSecuence"
        Me.CBOSecuence.Size = New System.Drawing.Size(282, 24)
        Me.CBOSecuence.TabIndex = 0
        '
        'CBOEntities
        '
        Me.CBOEntities.FormattingEnabled = True
        Me.CBOEntities.Location = New System.Drawing.Point(155, 12)
        Me.CBOEntities.Margin = New System.Windows.Forms.Padding(4)
        Me.CBOEntities.Name = "CBOEntities"
        Me.CBOEntities.Size = New System.Drawing.Size(282, 24)
        Me.CBOEntities.TabIndex = 0
        '
        'LabelIndex1
        '
        Me.LabelIndex1.AutoSize = True
        Me.LabelIndex1.BackColor = System.Drawing.Color.Transparent
        Me.LabelIndex1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LabelIndex1.FontSize = 9.75!
        Me.LabelIndex1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.LabelIndex1.Location = New System.Drawing.Point(4, 302)
        Me.LabelIndex1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelIndex1.Name = "LabelIndex1"
        Me.LabelIndex1.Size = New System.Drawing.Size(139, 16)
        Me.LabelIndex1.TabIndex = 4
        Me.LabelIndex1.Text = "Atributo de Salida 1"
        Me.LabelIndex1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelIndex2
        '
        Me.LabelIndex2.AutoSize = True
        Me.LabelIndex2.BackColor = System.Drawing.Color.Transparent
        Me.LabelIndex2.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LabelIndex2.FontSize = 9.75!
        Me.LabelIndex2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.LabelIndex2.Location = New System.Drawing.Point(7, 370)
        Me.LabelIndex2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelIndex2.Name = "LabelIndex2"
        Me.LabelIndex2.Size = New System.Drawing.Size(139, 16)
        Me.LabelIndex2.TabIndex = 4
        Me.LabelIndex2.Text = "Atributo de Salida 2"
        Me.LabelIndex2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelIndex3
        '
        Me.LabelIndex3.AutoSize = True
        Me.LabelIndex3.BackColor = System.Drawing.Color.Transparent
        Me.LabelIndex3.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LabelIndex3.FontSize = 9.75!
        Me.LabelIndex3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.LabelIndex3.Location = New System.Drawing.Point(7, 438)
        Me.LabelIndex3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelIndex3.Name = "LabelIndex3"
        Me.LabelIndex3.Size = New System.Drawing.Size(139, 16)
        Me.LabelIndex3.TabIndex = 4
        Me.LabelIndex3.Text = "Atributo de Salida 3"
        Me.LabelIndex3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelAmount
        '
        Me.LabelAmount.AutoSize = True
        Me.LabelAmount.BackColor = System.Drawing.Color.Transparent
        Me.LabelAmount.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LabelAmount.FontSize = 9.75!
        Me.LabelAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.LabelAmount.Location = New System.Drawing.Point(9, 87)
        Me.LabelAmount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelAmount.Name = "LabelAmount"
        Me.LabelAmount.Size = New System.Drawing.Size(49, 16)
        Me.LabelAmount.TabIndex = 4
        Me.LabelAmount.Text = "Monto"
        Me.LabelAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelLevel
        '
        Me.LabelLevel.AutoSize = True
        Me.LabelLevel.BackColor = System.Drawing.Color.Transparent
        Me.LabelLevel.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LabelLevel.FontSize = 9.75!
        Me.LabelLevel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.LabelLevel.Location = New System.Drawing.Point(9, 154)
        Me.LabelLevel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelLevel.Name = "LabelLevel"
        Me.LabelLevel.Size = New System.Drawing.Size(39, 16)
        Me.LabelLevel.TabIndex = 4
        Me.LabelLevel.Text = "Nivel"
        Me.LabelLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelSecuence
        '
        Me.LabelSecuence.AutoSize = True
        Me.LabelSecuence.BackColor = System.Drawing.Color.Transparent
        Me.LabelSecuence.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LabelSecuence.FontSize = 9.75!
        Me.LabelSecuence.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.LabelSecuence.Location = New System.Drawing.Point(9, 122)
        Me.LabelSecuence.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSecuence.Name = "LabelSecuence"
        Me.LabelSecuence.Size = New System.Drawing.Size(76, 16)
        Me.LabelSecuence.TabIndex = 4
        Me.LabelSecuence.Text = "Secuencia"
        Me.LabelSecuence.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelEntity
        '
        Me.LabelEntity.AutoSize = True
        Me.LabelEntity.BackColor = System.Drawing.Color.Transparent
        Me.LabelEntity.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LabelEntity.FontSize = 9.75!
        Me.LabelEntity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.LabelEntity.Location = New System.Drawing.Point(8, 15)
        Me.LabelEntity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelEntity.Name = "LabelEntity"
        Me.LabelEntity.Size = New System.Drawing.Size(121, 16)
        Me.LabelEntity.TabIndex = 4
        Me.LabelEntity.Text = "Matriz Secuencia"
        Me.LabelEntity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BTNADD
        '
        Me.BTNADD.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTNADD.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTNADD.ForeColor = System.Drawing.Color.White
        Me.BTNADD.Location = New System.Drawing.Point(569, 299)
        Me.BTNADD.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNADD.Name = "BTNADD"
        Me.BTNADD.Size = New System.Drawing.Size(147, 28)
        Me.BTNADD.TabIndex = 5
        Me.BTNADD.Text = "Guardar"
        Me.BTNADD.UseVisualStyleBackColor = False
        '
        'ZLabelVariable1
        '
        Me.ZLabelVariable1.BackColor = System.Drawing.Color.Transparent
        Me.ZLabelVariable1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabelVariable1.FontSize = 9.75!
        Me.ZLabelVariable1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabelVariable1.Location = New System.Drawing.Point(4, 318)
        Me.ZLabelVariable1.Name = "ZLabelVariable1"
        Me.ZLabelVariable1.Size = New System.Drawing.Size(214, 47)
        Me.ZLabelVariable1.TabIndex = 6
        Me.ZLabelVariable1.Text = "Variable a Asignar  Salida 1"
        Me.ZLabelVariable1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextoInteligenteVariable1
        '
        Me.TextoInteligenteVariable1.Location = New System.Drawing.Point(239, 330)
        Me.TextoInteligenteVariable1.Name = "TextoInteligenteVariable1"
        Me.TextoInteligenteVariable1.Size = New System.Drawing.Size(282, 30)
        Me.TextoInteligenteVariable1.TabIndex = 7
        Me.TextoInteligenteVariable1.Text = ""
        '
        'ZLabelVariable2
        '
        Me.ZLabelVariable2.BackColor = System.Drawing.Color.Transparent
        Me.ZLabelVariable2.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabelVariable2.FontSize = 9.75!
        Me.ZLabelVariable2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabelVariable2.Location = New System.Drawing.Point(4, 386)
        Me.ZLabelVariable2.Name = "ZLabelVariable2"
        Me.ZLabelVariable2.Size = New System.Drawing.Size(214, 49)
        Me.ZLabelVariable2.TabIndex = 6
        Me.ZLabelVariable2.Text = "Variable a Asignar Salida 2"
        Me.ZLabelVariable2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextoInteligenteVariable2
        '
        Me.TextoInteligenteVariable2.Location = New System.Drawing.Point(239, 398)
        Me.TextoInteligenteVariable2.Name = "TextoInteligenteVariable2"
        Me.TextoInteligenteVariable2.Size = New System.Drawing.Size(282, 30)
        Me.TextoInteligenteVariable2.TabIndex = 7
        Me.TextoInteligenteVariable2.Text = ""
        '
        'ZLabelVariable3
        '
        Me.ZLabelVariable3.BackColor = System.Drawing.Color.Transparent
        Me.ZLabelVariable3.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabelVariable3.FontSize = 9.75!
        Me.ZLabelVariable3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabelVariable3.Location = New System.Drawing.Point(7, 453)
        Me.ZLabelVariable3.Name = "ZLabelVariable3"
        Me.ZLabelVariable3.Size = New System.Drawing.Size(214, 49)
        Me.ZLabelVariable3.TabIndex = 6
        Me.ZLabelVariable3.Text = "Variable a Asignar Salida 3"
        Me.ZLabelVariable3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextoInteligenteVariable3
        '
        Me.TextoInteligenteVariable3.Location = New System.Drawing.Point(239, 466)
        Me.TextoInteligenteVariable3.Name = "TextoInteligenteVariable3"
        Me.TextoInteligenteVariable3.Size = New System.Drawing.Size(282, 30)
        Me.TextoInteligenteVariable3.TabIndex = 7
        Me.TextoInteligenteVariable3.Text = ""
        '
        'ZLabel1
        '
        Me.ZLabel1.AutoSize = True
        Me.ZLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel1.FontSize = 9.75!
        Me.ZLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel1.Location = New System.Drawing.Point(9, 191)
        Me.ZLabel1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ZLabel1.Name = "ZLabel1"
        Me.ZLabel1.Size = New System.Drawing.Size(131, 16)
        Me.ZLabel1.TabIndex = 8
        Me.ZLabel1.Text = "Aprobador Variable"
        Me.ZLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextoInteligenteApprover
        '
        Me.TextoInteligenteApprover.Location = New System.Drawing.Point(155, 182)
        Me.TextoInteligenteApprover.Name = "TextoInteligenteApprover"
        Me.TextoInteligenteApprover.Size = New System.Drawing.Size(282, 30)
        Me.TextoInteligenteApprover.TabIndex = 9
        Me.TextoInteligenteApprover.Text = ""
        '
        'TextoInteligenteSecuence
        '
        Me.TextoInteligenteSecuence.Location = New System.Drawing.Point(155, 218)
        Me.TextoInteligenteSecuence.Name = "TextoInteligenteSecuence"
        Me.TextoInteligenteSecuence.Size = New System.Drawing.Size(282, 30)
        Me.TextoInteligenteSecuence.TabIndex = 11
        Me.TextoInteligenteSecuence.Text = ""
        '
        'ZLabel2
        '
        Me.ZLabel2.AutoSize = True
        Me.ZLabel2.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel2.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel2.FontSize = 9.75!
        Me.ZLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel2.Location = New System.Drawing.Point(9, 227)
        Me.ZLabel2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ZLabel2.Name = "ZLabel2"
        Me.ZLabel2.Size = New System.Drawing.Size(132, 16)
        Me.ZLabel2.TabIndex = 10
        Me.ZLabel2.Text = "Secuencia Variable"
        Me.ZLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextoInteligenteLevel
        '
        Me.TextoInteligenteLevel.Location = New System.Drawing.Point(155, 255)
        Me.TextoInteligenteLevel.Name = "TextoInteligenteLevel"
        Me.TextoInteligenteLevel.Size = New System.Drawing.Size(282, 30)
        Me.TextoInteligenteLevel.TabIndex = 13
        Me.TextoInteligenteLevel.Text = ""
        '
        'ZLabel3
        '
        Me.ZLabel3.AutoSize = True
        Me.ZLabel3.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel3.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel3.FontSize = 9.75!
        Me.ZLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel3.Location = New System.Drawing.Point(9, 264)
        Me.ZLabel3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ZLabel3.Name = "ZLabel3"
        Me.ZLabel3.Size = New System.Drawing.Size(95, 16)
        Me.ZLabel3.TabIndex = 12
        Me.ZLabel3.Text = "Nivel Variable"
        Me.ZLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ZLabel4
        '
        Me.ZLabel4.AutoSize = True
        Me.ZLabel4.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel4.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel4.FontSize = 9.75!
        Me.ZLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel4.Location = New System.Drawing.Point(463, 15)
        Me.ZLabel4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ZLabel4.Name = "ZLabel4"
        Me.ZLabel4.Size = New System.Drawing.Size(125, 16)
        Me.ZLabel4.TabIndex = 15
        Me.ZLabel4.Text = "Registro Acciones"
        Me.ZLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CBORegisterEntity
        '
        Me.CBORegisterEntity.FormattingEnabled = True
        Me.CBORegisterEntity.Location = New System.Drawing.Point(644, 12)
        Me.CBORegisterEntity.Margin = New System.Windows.Forms.Padding(4)
        Me.CBORegisterEntity.Name = "CBORegisterEntity"
        Me.CBORegisterEntity.Size = New System.Drawing.Size(282, 24)
        Me.CBORegisterEntity.TabIndex = 14
        '
        'ZLabel5
        '
        Me.ZLabel5.AutoSize = True
        Me.ZLabel5.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel5.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel5.FontSize = 9.75!
        Me.ZLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel5.Location = New System.Drawing.Point(464, 53)
        Me.ZLabel5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ZLabel5.Name = "ZLabel5"
        Me.ZLabel5.Size = New System.Drawing.Size(100, 16)
        Me.ZLabel5.TabIndex = 17
        Me.ZLabel5.Text = "Id de Registro"
        Me.ZLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CBORegistryIdIndex
        '
        Me.CBORegistryIdIndex.FormattingEnabled = True
        Me.CBORegistryIdIndex.Location = New System.Drawing.Point(644, 48)
        Me.CBORegistryIdIndex.Margin = New System.Windows.Forms.Padding(4)
        Me.CBORegistryIdIndex.Name = "CBORegistryIdIndex"
        Me.CBORegistryIdIndex.Size = New System.Drawing.Size(282, 24)
        Me.CBORegistryIdIndex.TabIndex = 16
        '
        'ZLabel6
        '
        Me.ZLabel6.AutoSize = True
        Me.ZLabel6.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel6.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel6.FontSize = 9.75!
        Me.ZLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel6.Location = New System.Drawing.Point(464, 126)
        Me.ZLabel6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ZLabel6.Name = "ZLabel6"
        Me.ZLabel6.Size = New System.Drawing.Size(166, 16)
        Me.ZLabel6.TabIndex = 19
        Me.ZLabel6.Text = "Acciones de Aprobacion"
        Me.ZLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextoInteligenteActions
        '
        Me.TextoInteligenteActions.BackColor = System.Drawing.Color.White
        Me.TextoInteligenteActions.Location = New System.Drawing.Point(644, 120)
        Me.TextoInteligenteActions.Name = "TextoInteligenteActions"
        Me.TextoInteligenteActions.Size = New System.Drawing.Size(282, 30)
        Me.TextoInteligenteActions.TabIndex = 20
        Me.TextoInteligenteActions.Text = ""
        '
        'ZLabel7
        '
        Me.ZLabel7.AutoSize = True
        Me.ZLabel7.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel7.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel7.FontSize = 9.75!
        Me.ZLabel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel7.Location = New System.Drawing.Point(464, 85)
        Me.ZLabel7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ZLabel7.Name = "ZLabel7"
        Me.ZLabel7.Size = New System.Drawing.Size(52, 16)
        Me.ZLabel7.TabIndex = 22
        Me.ZLabel7.Text = "Accion"
        Me.ZLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CBOActionIndex
        '
        Me.CBOActionIndex.FormattingEnabled = True
        Me.CBOActionIndex.Location = New System.Drawing.Point(644, 80)
        Me.CBOActionIndex.Margin = New System.Windows.Forms.Padding(4)
        Me.CBOActionIndex.Name = "CBOActionIndex"
        Me.CBOActionIndex.Size = New System.Drawing.Size(282, 24)
        Me.CBOActionIndex.TabIndex = 21
        '
        'ZLabel8
        '
        Me.ZLabel8.AutoSize = True
        Me.ZLabel8.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel8.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel8.FontSize = 9.75!
        Me.ZLabel8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel8.Location = New System.Drawing.Point(9, 50)
        Me.ZLabel8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ZLabel8.Name = "ZLabel8"
        Me.ZLabel8.Size = New System.Drawing.Size(75, 16)
        Me.ZLabel8.TabIndex = 24
        Me.ZLabel8.Text = "Aprobador"
        Me.ZLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CBOApprover
        '
        Me.CBOApprover.FormattingEnabled = True
        Me.CBOApprover.Location = New System.Drawing.Point(155, 45)
        Me.CBOApprover.Margin = New System.Windows.Forms.Padding(4)
        Me.CBOApprover.Name = "CBOApprover"
        Me.CBOApprover.Size = New System.Drawing.Size(282, 24)
        Me.CBOApprover.TabIndex = 23
        '
        'TextoInteligenteIsApproverVariable
        '
        Me.TextoInteligenteIsApproverVariable.Location = New System.Drawing.Point(644, 172)
        Me.TextoInteligenteIsApproverVariable.Name = "TextoInteligenteIsApproverVariable"
        Me.TextoInteligenteIsApproverVariable.Size = New System.Drawing.Size(282, 30)
        Me.TextoInteligenteIsApproverVariable.TabIndex = 26
        Me.TextoInteligenteIsApproverVariable.Text = ""
        '
        'ZLabel9
        '
        Me.ZLabel9.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel9.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel9.FontSize = 9.75!
        Me.ZLabel9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel9.Location = New System.Drawing.Point(464, 163)
        Me.ZLabel9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ZLabel9.Name = "ZLabel9"
        Me.ZLabel9.Size = New System.Drawing.Size(173, 49)
        Me.ZLabel9.TabIndex = 25
        Me.ZLabel9.Text = "Usuario Actual Es Aprobador Variable (True,False)"
        Me.ZLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextoInteligenteApproversListVariable
        '
        Me.TextoInteligenteApproversListVariable.Location = New System.Drawing.Point(644, 218)
        Me.TextoInteligenteApproversListVariable.Name = "TextoInteligenteApproversListVariable"
        Me.TextoInteligenteApproversListVariable.Size = New System.Drawing.Size(282, 30)
        Me.TextoInteligenteApproversListVariable.TabIndex = 28
        Me.TextoInteligenteApproversListVariable.Text = ""
        '
        'ZLabel10
        '
        Me.ZLabel10.AutoSize = True
        Me.ZLabel10.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel10.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel10.FontSize = 9.75!
        Me.ZLabel10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel10.Location = New System.Drawing.Point(464, 224)
        Me.ZLabel10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ZLabel10.Name = "ZLabel10"
        Me.ZLabel10.Size = New System.Drawing.Size(182, 16)
        Me.ZLabel10.TabIndex = 27
        Me.ZLabel10.Text = "Lista Aprobadores Variable"
        Me.ZLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCDOApproveMatrix
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCDOApproveMatrix"
        Me.Size = New System.Drawing.Size(972, 551)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CBOIndexs1 As System.Windows.Forms.ComboBox
    Friend WithEvents LabelIndex1 As ZLabel

    Friend WithEvents CBOIndexs2 As System.Windows.Forms.ComboBox
    Friend WithEvents LabelIndex2 As ZLabel

    Friend WithEvents CBOIndexs3 As System.Windows.Forms.ComboBox
    Friend WithEvents LabelIndex3 As ZLabel

    Friend WithEvents CBOLevel As System.Windows.Forms.ComboBox
    Friend WithEvents LabelLevel As ZLabel

    Friend WithEvents CBOAmount As System.Windows.Forms.ComboBox
    Friend WithEvents LabelAmount As ZLabel

    Friend WithEvents CBOSecuence As System.Windows.Forms.ComboBox
    Friend WithEvents LabelSecuence As ZLabel

    Friend WithEvents CBOEntities As System.Windows.Forms.ComboBox
    Friend WithEvents LabelEntity As ZLabel

    Friend WithEvents BTNADD As Zamba.AppBlock.ZButton 'ZButton

    Friend WithEvents TextoInteligenteVariable1 As TextoInteligenteTextBox
    Friend WithEvents TextoInteligenteVariable2 As TextoInteligenteTextBox
    Friend WithEvents TextoInteligenteVariable3 As TextoInteligenteTextBox
    Friend WithEvents ZLabelVariable1 As ZLabel
    Friend WithEvents ZLabelVariable2 As ZLabel
    Friend WithEvents ZLabelVariable3 As ZLabel
    Friend WithEvents ZLabel4 As ZLabel
    Friend WithEvents CBORegisterEntity As ComboBox
    Friend WithEvents TextoInteligenteLevel As TextoInteligenteTextBox
    Friend WithEvents ZLabel3 As ZLabel
    Friend WithEvents TextoInteligenteSecuence As TextoInteligenteTextBox
    Friend WithEvents ZLabel2 As ZLabel
    Friend WithEvents TextoInteligenteApprover As TextoInteligenteTextBox
    Friend WithEvents ZLabel1 As ZLabel
    Friend WithEvents ZLabel6 As ZLabel
    Friend WithEvents ZLabel5 As ZLabel
    Friend WithEvents CBORegistryIdIndex As ComboBox
    Friend WithEvents TextoInteligenteActions As TextoInteligenteTextBox
    Friend WithEvents ZLabel7 As ZLabel
    Friend WithEvents CBOActionIndex As ComboBox
    Friend WithEvents ZLabel8 As ZLabel
    Friend WithEvents CBOApprover As ComboBox
    Friend WithEvents TextoInteligenteApproversListVariable As TextoInteligenteTextBox
    Friend WithEvents ZLabel10 As ZLabel
    Friend WithEvents TextoInteligenteIsApproverVariable As TextoInteligenteTextBox
    Friend WithEvents ZLabel9 As ZLabel
End Class
