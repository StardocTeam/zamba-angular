Public Class UCDoExecuteExplorer
    Inherits ZRuleControl

    Private Event OpenMissedRule(ByVal workflowId As Int64, ByVal ruleId As Int64)
    Private Delegate Sub ChangeCursorDelegate(ByVal cur As Cursor)

    Private Sub ChangeCursor(ByVal cur As Cursor)
        Try
            Cursor = cur
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


#Region " Código generado por el Diseñador de Windows Forms "

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents BtnSave As ZButton
    Friend WithEvents txtDSName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend Shadows WithEvents lblRoute As ZLabel
    Friend WithEvents chkWaitToCloseBrowser As System.Windows.Forms.CheckBox
    Friend WithEvents txtWidth As TextBox
    Friend WithEvents txtHeight As TextBox
    Friend WithEvents lblWidth As ZLabel
    Friend WithEvents lblHeight As ZLabel
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents lblpercent As ZLabel
    Friend WithEvents TxtVar As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents TxtValue As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents lblvalor As ZLabel
    Friend WithEvents lblOperator As ZLabel
    Friend WithEvents lblvar As ZLabel
    Friend WithEvents TxtOperator As ComboBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents CBOEvaluateRule As ComboBox
    Friend WithEvents chkContinueWithRule As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents CBORules As ComboBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents chkHabilitar As System.Windows.Forms.CheckBox
    Friend WithEvents lblrequiredfield As ZLabel
    Friend WithEvents GbVisualizations As GroupBox
    Friend WithEvents rbHorizontalVisualization As System.Windows.Forms.RadioButton
    Friend WithEvents rbVerticalVisualization As System.Windows.Forms.RadioButton
    Friend WithEvents chkCondition As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents CBOElse As ComboBox
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents chkOpenNewWindowBrowser As System.Windows.Forms.CheckBox
    Friend WithEvents btnEvaluateGoRule As ZButton
    Friend WithEvents btnElseGoRule As ZButton
    Friend WithEvents btnRulesGoRule As ZButton
    Friend WithEvents txtRoute As Zamba.AppBlock.TextoInteligenteTextBox

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        BtnSave = New ZButton()
        lblRoute = New ZLabel()
        txtRoute = New Zamba.AppBlock.TextoInteligenteTextBox()
        chkWaitToCloseBrowser = New System.Windows.Forms.CheckBox()
        lblHeight = New ZLabel()
        lblWidth = New ZLabel()
        txtHeight = New TextBox()
        txtWidth = New TextBox()
        lblpercent = New ZLabel()
        Label4 = New ZLabel()
        TxtVar = New Zamba.AppBlock.TextoInteligenteTextBox()
        TxtValue = New Zamba.AppBlock.TextoInteligenteTextBox()
        Label1 = New ZLabel()
        Label2 = New ZLabel()
        GroupBox1 = New GroupBox()
        btnRulesGoRule = New ZButton()
        CBORules = New ComboBox()
        chkContinueWithRule = New System.Windows.Forms.CheckBox()
        GroupBox2 = New GroupBox()
        btnEvaluateGoRule = New ZButton()
        CBOEvaluateRule = New ComboBox()
        GroupBox3 = New GroupBox()
        lblvalor = New ZLabel()
        lblOperator = New ZLabel()
        lblvar = New ZLabel()
        TxtOperator = New ComboBox()
        GroupBox4 = New GroupBox()
        GroupBox6 = New GroupBox()
        btnElseGoRule = New ZButton()
        CBOElse = New ComboBox()
        chkCondition = New System.Windows.Forms.CheckBox()
        chkHabilitar = New System.Windows.Forms.CheckBox()
        GroupBox5 = New GroupBox()
        lblrequiredfield = New ZLabel()
        rbVerticalVisualization = New System.Windows.Forms.RadioButton()
        rbHorizontalVisualization = New System.Windows.Forms.RadioButton()
        GbVisualizations = New GroupBox()
        Label5 = New ZLabel()
        Label6 = New ZLabel()
        chkOpenNewWindowBrowser = New System.Windows.Forms.CheckBox()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        GroupBox3.SuspendLayout()
        GroupBox4.SuspendLayout()
        GroupBox6.SuspendLayout()
        GroupBox5.SuspendLayout()
        GbVisualizations.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(chkOpenNewWindowBrowser)
        tbRule.Controls.Add(Label6)
        tbRule.Controls.Add(Label5)
        tbRule.Controls.Add(GbVisualizations)
        tbRule.Controls.Add(lblrequiredfield)
        tbRule.Controls.Add(chkHabilitar)
        tbRule.Controls.Add(GroupBox5)
        tbRule.Controls.Add(GroupBox4)
        tbRule.Controls.Add(Label4)
        tbRule.Controls.Add(lblpercent)
        tbRule.Controls.Add(txtWidth)
        tbRule.Controls.Add(txtHeight)
        tbRule.Controls.Add(lblWidth)
        tbRule.Controls.Add(lblHeight)
        tbRule.Controls.Add(chkWaitToCloseBrowser)
        tbRule.Controls.Add(lblRoute)
        tbRule.Controls.Add(txtRoute)
        tbRule.Controls.Add(BtnSave)
        tbRule.Size = New System.Drawing.Size(727, 647)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(735, 676)
        '
        'BtnSave
        '
        BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        BtnSave.FlatStyle = FlatStyle.Flat
        BtnSave.ForeColor = System.Drawing.Color.White
        BtnSave.Location = New System.Drawing.Point(388, 608)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New System.Drawing.Size(93, 28)
        BtnSave.TabIndex = 3
        BtnSave.Text = "Guardar"
        BtnSave.UseVisualStyleBackColor = False
        '
        'lblRoute
        '
        lblRoute.AutoSize = True
        lblRoute.BackColor = System.Drawing.Color.Transparent
        lblRoute.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblRoute.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblRoute.Location = New System.Drawing.Point(3, 9)
        lblRoute.Name = "lblRoute"
        lblRoute.Size = New System.Drawing.Size(147, 16)
        lblRoute.TabIndex = 21
        lblRoute.Text = "Dirigir la ruta hacia:*"
        lblRoute.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtRoute
        '
        txtRoute.Location = New System.Drawing.Point(127, 6)
        txtRoute.Name = "txtRoute"
        txtRoute.Size = New System.Drawing.Size(477, 43)
        txtRoute.TabIndex = 20
        txtRoute.Text = ""
        '
        'chkWaitToCloseBrowser
        '
        chkWaitToCloseBrowser.AutoSize = True
        chkWaitToCloseBrowser.Location = New System.Drawing.Point(285, 127)
        chkWaitToCloseBrowser.Name = "chkWaitToCloseBrowser"
        chkWaitToCloseBrowser.Size = New System.Drawing.Size(289, 20)
        chkWaitToCloseBrowser.TabIndex = 22
        chkWaitToCloseBrowser.Text = "Ejecutar la regla en una nueva ventana"
        chkWaitToCloseBrowser.UseVisualStyleBackColor = True
        '
        'lblHeight
        '
        lblHeight.AutoSize = True
        lblHeight.BackColor = System.Drawing.Color.Transparent
        lblHeight.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblHeight.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblHeight.Location = New System.Drawing.Point(45, 115)
        lblHeight.Name = "lblHeight"
        lblHeight.Size = New System.Drawing.Size(147, 16)
        lblHeight.TabIndex = 25
        lblHeight.Text = "Alto predeterminado:"
        lblHeight.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblWidth
        '
        lblWidth.AutoSize = True
        lblWidth.BackColor = System.Drawing.Color.Transparent
        lblWidth.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblWidth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblWidth.Location = New System.Drawing.Point(42, 77)
        lblWidth.Name = "lblWidth"
        lblWidth.Size = New System.Drawing.Size(162, 16)
        lblWidth.TabIndex = 26
        lblWidth.Text = "Ancho predeterminado:"
        lblWidth.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtHeight
        '
        txtHeight.Location = New System.Drawing.Point(194, 112)
        txtHeight.Name = "txtHeight"
        txtHeight.Size = New System.Drawing.Size(60, 23)
        txtHeight.TabIndex = 27
        '
        'txtWidth
        '
        txtWidth.Location = New System.Drawing.Point(194, 70)
        txtWidth.Name = "txtWidth"
        txtWidth.Size = New System.Drawing.Size(60, 23)
        txtWidth.TabIndex = 28
        '
        'lblpercent
        '
        lblpercent.AutoSize = True
        lblpercent.BackColor = System.Drawing.Color.Transparent
        lblpercent.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblpercent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblpercent.Location = New System.Drawing.Point(257, 116)
        lblpercent.Name = "lblpercent"
        lblpercent.Size = New System.Drawing.Size(21, 16)
        lblpercent.TabIndex = 29
        lblpercent.Text = "%"
        lblpercent.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label4.Location = New System.Drawing.Point(257, 74)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(21, 16)
        Label4.TabIndex = 30
        Label4.Text = "%"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        '
        'TxtVar
        '
        TxtVar.Location = New System.Drawing.Point(15, 33)
        TxtVar.Name = "TxtVar"
        TxtVar.Size = New System.Drawing.Size(172, 21)
        TxtVar.TabIndex = 3
        TxtVar.Text = ""
        '
        'TxtValue
        '
        TxtValue.Location = New System.Drawing.Point(352, 33)
        TxtValue.Name = "TxtValue"
        TxtValue.Size = New System.Drawing.Size(114, 21)
        TxtValue.TabIndex = 5
        TxtValue.Text = ""
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(6, 17)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(53, 13)
        Label1.TabIndex = 0
        Label1.Text = "Variable"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Label2.AutoEllipsis = True
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(184, 17)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(60, 13)
        Label2.TabIndex = 1
        Label2.Text = "Operador"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        GroupBox1.Controls.Add(btnRulesGoRule)
        GroupBox1.Controls.Add(CBORules)
        GroupBox1.Location = New System.Drawing.Point(39, 44)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(432, 85)
        GroupBox1.TabIndex = 23
        GroupBox1.TabStop = False
        GroupBox1.Text = "Elegir Regla"
        '
        'btnRulesGoRule
        '
        btnRulesGoRule.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnRulesGoRule.Enabled = False
        btnRulesGoRule.FlatStyle = FlatStyle.Flat
        btnRulesGoRule.ForeColor = System.Drawing.Color.White
        btnRulesGoRule.Location = New System.Drawing.Point(20, 47)
        btnRulesGoRule.Name = "btnRulesGoRule"
        btnRulesGoRule.Size = New System.Drawing.Size(194, 32)
        btnRulesGoRule.TabIndex = 1
        btnRulesGoRule.Text = "Ir a la regla de destino"
        btnRulesGoRule.UseVisualStyleBackColor = True
        '
        'CBORules
        '
        CBORules.FormattingEnabled = True
        CBORules.Location = New System.Drawing.Point(18, 20)
        CBORules.Name = "CBORules"
        CBORules.Size = New System.Drawing.Size(397, 24)
        CBORules.TabIndex = 0
        '
        'chkContinueWithRule
        '
        chkContinueWithRule.AutoSize = True
        chkContinueWithRule.Location = New System.Drawing.Point(36, 18)
        chkContinueWithRule.Name = "chkContinueWithRule"
        chkContinueWithRule.Size = New System.Drawing.Size(174, 20)
        chkContinueWithRule.TabIndex = 25
        chkContinueWithRule.Text = "Continuar la ejecución"
        chkContinueWithRule.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        GroupBox2.Controls.Add(btnEvaluateGoRule)
        GroupBox2.Controls.Add(CBOEvaluateRule)
        GroupBox2.Location = New System.Drawing.Point(41, 5)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New System.Drawing.Size(417, 85)
        GroupBox2.TabIndex = 31
        GroupBox2.TabStop = False
        GroupBox2.Text = "Elegir Regla"
        '
        'btnEvaluateGoRule
        '
        btnEvaluateGoRule.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnEvaluateGoRule.Enabled = False
        btnEvaluateGoRule.FlatStyle = FlatStyle.Flat
        btnEvaluateGoRule.ForeColor = System.Drawing.Color.White
        btnEvaluateGoRule.Location = New System.Drawing.Point(18, 50)
        btnEvaluateGoRule.Name = "btnEvaluateGoRule"
        btnEvaluateGoRule.Size = New System.Drawing.Size(194, 30)
        btnEvaluateGoRule.TabIndex = 1
        btnEvaluateGoRule.Text = "Ir a la regla de destino"
        btnEvaluateGoRule.UseVisualStyleBackColor = True
        '
        'CBOEvaluateRule
        '
        CBOEvaluateRule.FormattingEnabled = True
        CBOEvaluateRule.Location = New System.Drawing.Point(18, 20)
        CBOEvaluateRule.Name = "CBOEvaluateRule"
        CBOEvaluateRule.Size = New System.Drawing.Size(331, 24)
        CBOEvaluateRule.TabIndex = 0
        '
        'GroupBox3
        '
        GroupBox3.BackColor = System.Drawing.Color.Transparent
        GroupBox3.Controls.Add(lblvalor)
        GroupBox3.Controls.Add(lblOperator)
        GroupBox3.Controls.Add(lblvar)
        GroupBox3.Controls.Add(TxtOperator)
        GroupBox3.Controls.Add(TxtVar)
        GroupBox3.Controls.Add(TxtValue)
        GroupBox3.Location = New System.Drawing.Point(41, 96)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New System.Drawing.Size(478, 73)
        GroupBox3.TabIndex = 32
        GroupBox3.TabStop = False
        '
        'lblvalor
        '
        lblvalor.AutoSize = True
        lblvalor.BackColor = System.Drawing.Color.Transparent
        lblvalor.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblvalor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblvalor.Location = New System.Drawing.Point(393, 17)
        lblvalor.Name = "lblvalor"
        lblvalor.Size = New System.Drawing.Size(40, 16)
        lblvalor.TabIndex = 36
        lblvalor.Text = "Valor"
        lblvalor.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblOperator
        '
        lblOperator.AutoSize = True
        lblOperator.BackColor = System.Drawing.Color.Transparent
        lblOperator.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblOperator.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblOperator.Location = New System.Drawing.Point(243, 17)
        lblOperator.Name = "lblOperator"
        lblOperator.Size = New System.Drawing.Size(68, 16)
        lblOperator.TabIndex = 35
        lblOperator.Text = "Operador"
        lblOperator.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblvar
        '
        lblvar.AutoSize = True
        lblvar.BackColor = System.Drawing.Color.Transparent
        lblvar.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblvar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblvar.Location = New System.Drawing.Point(75, 17)
        lblvar.Name = "lblvar"
        lblvar.Size = New System.Drawing.Size(59, 16)
        lblvar.TabIndex = 34
        lblvar.Text = "Variable"
        lblvar.TextAlign = ContentAlignment.MiddleLeft
        '
        'TxtOperator
        '
        TxtOperator.Location = New System.Drawing.Point(221, 33)
        TxtOperator.Name = "TxtOperator"
        TxtOperator.Size = New System.Drawing.Size(107, 24)
        TxtOperator.TabIndex = 6
        '
        'GroupBox4
        '
        GroupBox4.Controls.Add(GroupBox6)
        GroupBox4.Controls.Add(chkCondition)
        GroupBox4.Controls.Add(GroupBox3)
        GroupBox4.Controls.Add(GroupBox2)
        GroupBox4.Location = New System.Drawing.Point(9, 175)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New System.Drawing.Size(595, 289)
        GroupBox4.TabIndex = 33
        GroupBox4.TabStop = False
        '
        'GroupBox6
        '
        GroupBox6.Controls.Add(btnElseGoRule)
        GroupBox6.Controls.Add(CBOElse)
        GroupBox6.Location = New System.Drawing.Point(39, 200)
        GroupBox6.Name = "GroupBox6"
        GroupBox6.Size = New System.Drawing.Size(418, 85)
        GroupBox6.TabIndex = 42
        GroupBox6.TabStop = False
        GroupBox6.Text = "Elegir Regla"
        '
        'btnElseGoRule
        '
        btnElseGoRule.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnElseGoRule.Enabled = False
        btnElseGoRule.FlatStyle = FlatStyle.Flat
        btnElseGoRule.ForeColor = System.Drawing.Color.White
        btnElseGoRule.Location = New System.Drawing.Point(19, 49)
        btnElseGoRule.Name = "btnElseGoRule"
        btnElseGoRule.Size = New System.Drawing.Size(195, 30)
        btnElseGoRule.TabIndex = 42
        btnElseGoRule.Text = "Ir a la regla de destino"
        btnElseGoRule.UseVisualStyleBackColor = True
        '
        'CBOElse
        '
        CBOElse.FormattingEnabled = True
        CBOElse.Location = New System.Drawing.Point(18, 22)
        CBOElse.Name = "CBOElse"
        CBOElse.Size = New System.Drawing.Size(388, 24)
        CBOElse.TabIndex = 41
        '
        'chkCondition
        '
        chkCondition.AutoSize = True
        chkCondition.Location = New System.Drawing.Point(36, 174)
        chkCondition.Name = "chkCondition"
        chkCondition.Size = New System.Drawing.Size(280, 20)
        chkCondition.TabIndex = 40
        chkCondition.Text = "Si la condicion no se cumple ejecutar:"
        chkCondition.UseVisualStyleBackColor = True
        '
        'chkHabilitar
        '
        chkHabilitar.AutoSize = True
        chkHabilitar.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        chkHabilitar.Location = New System.Drawing.Point(9, 149)
        chkHabilitar.Name = "chkHabilitar"
        chkHabilitar.Size = New System.Drawing.Size(80, 20)
        chkHabilitar.TabIndex = 33
        chkHabilitar.Text = "Habilitar"
        chkHabilitar.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        GroupBox5.Controls.Add(chkContinueWithRule)
        GroupBox5.Controls.Add(GroupBox1)
        GroupBox5.Location = New System.Drawing.Point(9, 463)
        GroupBox5.Name = "GroupBox5"
        GroupBox5.Size = New System.Drawing.Size(595, 145)
        GroupBox5.TabIndex = 34
        GroupBox5.TabStop = False
        '
        'lblrequiredfield
        '
        lblrequiredfield.AutoSize = True
        lblrequiredfield.BackColor = System.Drawing.Color.Transparent
        lblrequiredfield.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblrequiredfield.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblrequiredfield.Location = New System.Drawing.Point(12, 611)
        lblrequiredfield.Name = "lblrequiredfield"
        lblrequiredfield.Size = New System.Drawing.Size(146, 16)
        lblrequiredfield.TabIndex = 35
        lblrequiredfield.Text = "* Campos requeridos"
        lblrequiredfield.TextAlign = ContentAlignment.MiddleLeft
        '
        'rbVerticalVisualization
        '
        rbVerticalVisualization.Checked = True
        rbVerticalVisualization.Location = New System.Drawing.Point(103, 15)
        rbVerticalVisualization.Name = "rbVerticalVisualization"
        rbVerticalVisualization.Size = New System.Drawing.Size(140, 17)
        rbVerticalVisualization.TabIndex = 36
        rbVerticalVisualization.TabStop = True
        rbVerticalVisualization.Text = "Visualización Horizontal"
        rbVerticalVisualization.UseVisualStyleBackColor = True
        '
        'rbHorizontalVisualization
        '
        rbHorizontalVisualization.AutoSize = True
        rbHorizontalVisualization.Location = New System.Drawing.Point(103, 37)
        rbHorizontalVisualization.Name = "rbHorizontalVisualization"
        rbHorizontalVisualization.Size = New System.Drawing.Size(163, 20)
        rbHorizontalVisualization.TabIndex = 37
        rbHorizontalVisualization.Text = "Visualización Vertical"
        rbHorizontalVisualization.UseVisualStyleBackColor = True
        '
        'GbVisualizations
        '
        GbVisualizations.Controls.Add(rbHorizontalVisualization)
        GbVisualizations.Controls.Add(rbVerticalVisualization)
        GbVisualizations.Location = New System.Drawing.Point(285, 55)
        GbVisualizations.Name = "GbVisualizations"
        GbVisualizations.Size = New System.Drawing.Size(319, 66)
        GbVisualizations.TabIndex = 38
        GbVisualizations.TabStop = False
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label5.Location = New System.Drawing.Point(12, 96)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(173, 16)
        Label5.TabIndex = 39
        Label5.Text = "Configuración Horizontal:"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.BackColor = System.Drawing.Color.Transparent
        Label6.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label6.Location = New System.Drawing.Point(12, 56)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(157, 16)
        Label6.TabIndex = 40
        Label6.Text = "Configuracion Vertical:"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        '
        'chkOpenNewWindowBrowser
        '
        chkOpenNewWindowBrowser.AutoSize = True
        chkOpenNewWindowBrowser.Location = New System.Drawing.Point(285, 151)
        chkOpenNewWindowBrowser.Name = "chkOpenNewWindowBrowser"
        chkOpenNewWindowBrowser.Size = New System.Drawing.Size(404, 20)
        chkOpenNewWindowBrowser.TabIndex = 41
        chkOpenNewWindowBrowser.Text = "Abrir como una segunda instancia en una nueva ventana"
        chkOpenNewWindowBrowser.UseVisualStyleBackColor = True
        '
        'UCDoExecuteExplorer
        '
        Name = "UCDoExecuteExplorer"
        Size = New System.Drawing.Size(735, 676)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox2.ResumeLayout(False)
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        GroupBox4.ResumeLayout(False)
        GroupBox4.PerformLayout()
        GroupBox6.ResumeLayout(False)
        GroupBox5.ResumeLayout(False)
        GroupBox5.PerformLayout()
        GbVisualizations.ResumeLayout(False)
        GbVisualizations.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDoExecuteExplorer

    Public Sub New(ByRef DoExecuteExplorer As IDoExecuteExplorer, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoExecuteExplorer, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = DoExecuteExplorer
        Try
            RemoveHandler OpenMissedRule, AddressOf _wfPanelCircuit.OpenMissedRule
            AddHandler OpenMissedRule, AddressOf _wfPanelCircuit.OpenMissedRule
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub



#End Region
    Public Shadows ReadOnly Property MyRule() As IDoExecuteExplorer
        Get
            Return DirectCast(Rule, IDoExecuteExplorer)
        End Get
    End Property

    Private Shared sComparadores() As String = {"Igual", "Distinto", "Menor", "Mayor", "IgualMenor", "IgualMayor"}
    Private Sub UCDoExecuteExplorer_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            txtRoute.Text = CurrentRule.Route
            chkWaitToCloseBrowser.Checked = CurrentRule.BrowserStatus
            chkOpenNewWindowBrowser.Checked = CurrentRule.OpenNewWindowBrowser
            If CurrentRule.Height.ToString <> String.Empty Then
                txtHeight.Text = CurrentRule.Height
            Else
                '% alto por defecto
                txtHeight.Text = 75
            End If
            If CurrentRule.Width.ToString <> String.Empty Then
                txtWidth.Text = CurrentRule.Width
            Else
                '% ancho por defecto
                txtWidth.Text = 25
            End If

            If Not IsNothing(sComparadores) Then
                TxtOperator.Items.AddRange(sComparadores)
            End If

            Select Case CurrentRule.Operador
                Case Comparadores.Distinto
                    TxtOperator.SelectedItem = Comparadores.Distinto.ToString
                Case Comparadores.Igual
                    TxtOperator.SelectedItem = Comparadores.Igual.ToString
                Case Comparadores.IgualMayor
                    TxtOperator.SelectedItem = Comparadores.IgualMayor.ToString
                Case Comparadores.IgualMenor
                    TxtOperator.SelectedItem = Comparadores.IgualMenor.ToString
                Case Comparadores.Mayor
                    TxtOperator.SelectedItem = Comparadores.Mayor.ToString
                Case Comparadores.Menor
                    TxtOperator.SelectedItem = Comparadores.Menor.ToString
            End Select

            chkContinueWithRule.Checked = CurrentRule.ContinueWithRule


            rbHorizontalVisualization.Checked = CurrentRule.HorizontalVisualization
            rbVerticalVisualization.Checked = Not CurrentRule.HorizontalVisualization
            'habilito o deshabilito el groupbox
            If CurrentRule.Habilitar Then
                chkHabilitar.Checked = CurrentRule.Habilitar
                CBOEvaluateRule.Enabled = CurrentRule.Habilitar
                CBOElse.Enabled = CurrentRule.Habilitar
                TxtVar.Enabled = CurrentRule.Habilitar
                TxtValue.Enabled = CurrentRule.Habilitar
                TxtOperator.Enabled = CurrentRule.Habilitar
                chkCondition.Checked = CurrentRule.HabilitarMensaje
            Else
                chkHabilitar.Checked = False
                CBOEvaluateRule.Enabled = False
                CBOElse.Enabled = False
                TxtVar.Enabled = False
                TxtValue.Enabled = False
                TxtOperator.Enabled = False
            End If

            CBORules.Enabled = CurrentRule.ContinueWithRule
            If CurrentRule.ExecuteElseID = -1 Or CurrentRule.ExecuteElseID = 0 Then
                chkCondition.Checked = False
                CBOElse.Enabled = False
            Else
                chkCondition.Checked = True
                CBOElse.Enabled = True
            End If

            'CBOEvaluateRule
            Dim dt As DataTable = WFRulesBusiness.GetDoExecuteRules().Tables(0)
            CBORules.DataSource = dt
            CBORules.DisplayMember = dt.Columns(0).ColumnName '  "NAME"
            CBORules.ValueMember = dt.Columns(1).ColumnName    '"ID"
            If CBORules.Enabled Then
                CBORules.SelectedValue = Int32.Parse(CurrentRule.RuleID)
            Else
                CBORules.SelectedValue = -1
            End If
            'CBOEvaluateRule
            Dim dtEvaluateRule As DataTable = WFRulesBusiness.GetDoExecuteRules().Tables(0)
            CBOEvaluateRule.DataSource = dtEvaluateRule
            CBOEvaluateRule.DisplayMember = dtEvaluateRule.Columns(0).ColumnName '  "NAME"
            CBOEvaluateRule.ValueMember = dtEvaluateRule.Columns(1).ColumnName    '"ID"
            If CBOEvaluateRule.Enabled Then
                CBOEvaluateRule.SelectedValue = Int32.Parse(CurrentRule.EvaluateRuleID)
            Else
                CBOEvaluateRule.SelectedValue = -1
            End If
            'CBOElse
            Dim dtExecuteElse As DataTable = WFRulesBusiness.GetDoExecuteRules().Tables(0)
            CBOElse.DataSource = dtExecuteElse
            CBOElse.DisplayMember = dtExecuteElse.Columns(0).ColumnName '  "NAME"
            CBOElse.ValueMember = dtExecuteElse.Columns(1).ColumnName    '"ID"
            If CBOElse.Enabled Then
                CBOElse.SelectedValue = Int32.Parse(CurrentRule.ExecuteElseID)
            Else
                CBOElse.SelectedValue = -1
            End If

            TxtVar.Text = CurrentRule.Variable
            TxtValue.Text = CurrentRule.Valor

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            If String.IsNullOrEmpty(txtRoute.Text) Then
                MessageBox.Show("Debe completar los campos requeridos", "Atencion", MessageBoxButtons.OK)
                Exit Sub
            Else
                CurrentRule.Route = txtRoute.Text.Trim
                CurrentRule.BrowserStatus = chkWaitToCloseBrowser.Checked
                CurrentRule.HorizontalVisualization = rbHorizontalVisualization.Checked
                CurrentRule.OpenNewWindowBrowser = chkOpenNewWindowBrowser.Checked
                If CBORules.Enabled And Not IsNothing(CBORules.SelectedItem) Then
                    CurrentRule.RuleID = Int32.Parse(CBORules.SelectedValue)
                Else
                    CurrentRule.RuleID = -1
                End If

                If CBOEvaluateRule.Enabled And Not IsNothing(CBOEvaluateRule.SelectedItem) Then
                    CurrentRule.EvaluateRuleID = Int32.Parse(CBOEvaluateRule.SelectedValue)
                Else
                    CurrentRule.EvaluateRuleID = -1
                End If

                If CBOElse.Enabled And Not IsNothing(CBOElse.SelectedItem) Then
                    CurrentRule.ExecuteElseID = Int32.Parse(CBOElse.SelectedValue)
                Else
                    CurrentRule.ExecuteElseID = -1
                End If

                If chkHabilitar.Checked = True Then
                    CurrentRule.Variable = TxtVar.Text
                    CurrentRule.Valor = TxtValue.Text
                    CurrentRule.Habilitar = chkHabilitar.Checked
                    CurrentRule.HabilitarMensaje = chkCondition.Checked
                    Select Case TxtOperator.SelectedIndex
                        Case 0
                            CurrentRule.Operador = Comparadores.Igual
                        Case 1
                            CurrentRule.Operador = Comparadores.Distinto
                        Case 2
                            CurrentRule.Operador = Comparadores.Menor
                        Case 3
                            CurrentRule.Operador = Comparadores.Mayor
                        Case 4
                            CurrentRule.Operador = Comparadores.IgualMenor
                        Case 5
                            CurrentRule.Operador = Comparadores.IgualMayor
                    End Select
                Else
                    CBOEvaluateRule.Enabled = False
                    TxtVar.Enabled = False
                    TxtValue.Enabled = False
                    TxtOperator.Enabled = False
                End If

                If Not String.IsNullOrEmpty(txtHeight.Text) And IsNumeric(txtHeight.Text) And Not txtHeight.Text = "0" Then
                    CurrentRule.Height = txtHeight.Text
                Else
                    txtHeight.Text = 25
                    CurrentRule.Height = 25
                End If

                If Not String.IsNullOrEmpty(txtWidth.Text) And IsNumeric(txtWidth.Text) And Not txtWidth.Text = "0" Then
                    CurrentRule.Width = txtWidth.Text
                Else
                    txtWidth.Text = 25
                    CurrentRule.Width = 25
                End If

                CurrentRule.ContinueWithRule = chkContinueWithRule.Checked
                CurrentRule.Habilitar = chkHabilitar.Checked
            End If

            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Route)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.BrowserStatus)

            If Not IsNothing(CBORules.SelectedValue) Or CurrentRule.RuleID = -1 Then
                WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.RuleID)
            End If
            If Not IsNothing(CBOEvaluateRule.SelectedValue) Or CurrentRule.EvaluateRuleID = -1 Then
                WFRulesBusiness.UpdateParamItem(Rule.ID, 10, CurrentRule.EvaluateRuleID)
            End If
            If Not IsNothing(CBOElse.SelectedValue) Or CurrentRule.ExecuteElseID = -1 Then
                WFRulesBusiness.UpdateParamItem(Rule.ID, 12, CurrentRule.ExecuteElseID)
            End If

            WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.Height)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.Width)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.ContinueWithRule)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 6, CurrentRule.Operador)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 7, CurrentRule.Variable)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 8, CurrentRule.Valor)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 9, CurrentRule.Habilitar)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 11, CurrentRule.HabilitarMensaje)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 13, CurrentRule.HorizontalVisualization)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 14, CurrentRule.OpenNewWindowBrowser)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
            CurrentRule.Name = "Abrir Explorador "
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub chkContinueWithRule_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkContinueWithRule.CheckedChanged
        Dim response As System.Windows.Forms.DialogResult

        If chkContinueWithRule.Checked = False Then
            If CBORules.SelectedIndex <> -1 Then
                response = System.Windows.Forms.MessageBox.Show("Desea deshabilitar el combobox?. Perderá todos los cambios.", "ATENCION", MessageBoxButtons.YesNo)
                If response = DialogResult.Yes Then
                    chkContinueWithRule.Text = "No continuar la ejecución"
                    CBOElse.Enabled = False
                    btnRulesGoRule.Enabled = False
                    CBORules.Text = String.Empty
                    CBORules.SelectedIndex = -1
                Else
                    chkContinueWithRule.Checked = True
                End If
            Else
                chkContinueWithRule.Checked = False
                CBORules.Enabled = False
                btnRulesGoRule.Enabled = False
                chkContinueWithRule.Text = "No continuar la ejecución"
            End If
        Else
            CBORules.Enabled = True
            btnRulesGoRule.Enabled = True
            chkContinueWithRule.Text = "Continuar la ejecución"
        End If
    End Sub
#Region "Habilitacion"
    Private Sub chkHabilitar_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkHabilitar.CheckedChanged
        Dim response As System.Windows.Forms.DialogResult
        If chkHabilitar.Checked = False Then
            If CBOEvaluateRule.SelectedValue <> 0 Or TxtVar.Text <> String.Empty Or TxtValue.Text <> String.Empty Then
                response = System.Windows.Forms.MessageBox.Show("Desea deshabilitar realmente la seccion?. Perderá todos los cambios.", "ATENCION", MessageBoxButtons.YesNo)
                If response = DialogResult.Yes Then
                    chkHabilitar.Text = "Habilitar"
                    TxtValue.Text = String.Empty
                    TxtVar.Text = String.Empty
                    TxtOperator.Text = String.Empty
                    CBOEvaluateRule.Text = String.Empty
                    chkHabilitar.Checked = False
                    CBOEvaluateRule.SelectedIndex = -1
                    CurrentRule.EvaluateRuleID = -1
                    CurrentRule.Valor = String.Empty
                    CurrentRule.Variable = String.Empty
                    CurrentRule.Operador = -1
                    CBOEvaluateRule.Enabled = False
                    btnEvaluateGoRule.Enabled = False
                    TxtVar.Enabled = False
                    TxtValue.Enabled = False
                    TxtOperator.Enabled = False
                Else
                    chkHabilitar.Checked = True
                End If
            Else
                'Me.chkHabilitar.Checked = Me.CurrentRule.Habilitar
                chkHabilitar.Text = "Habilitar"
                CBOEvaluateRule.Enabled = False
                btnEvaluateGoRule.Enabled = False
                TxtVar.Enabled = False
                TxtValue.Enabled = False
                TxtOperator.Enabled = False
            End If
        Else
            chkHabilitar.Text = "Deshabilitar"
            CBOEvaluateRule.Enabled = True
            btnEvaluateGoRule.Enabled = True
            TxtVar.Enabled = True
            TxtValue.Enabled = True
            TxtOperator.Enabled = True
        End If
    End Sub

#End Region

    Private Sub CBOEvaluateRule_TextUpdate(ByVal sender As System.Object, ByVal e As EventArgs) Handles CBOEvaluateRule.TextUpdate
        If DirectCast(sender, ComboBox).SelectedText = String.Empty And DirectCast(sender, ComboBox).Text = String.Empty Then
            CurrentRule.EvaluateRuleID = -1
        End If
    End Sub
    Private Sub CBOElse_TextUpdate(ByVal sender As System.Object, ByVal e As EventArgs) Handles CBOElse.TextUpdate
        If DirectCast(sender, ComboBox).SelectedText = String.Empty And DirectCast(sender, ComboBox).Text = String.Empty Then
            CurrentRule.ExecuteElseID = -1
        End If
    End Sub
    Private Sub CBORules_TextUpdate(ByVal sender As System.Object, ByVal e As EventArgs) Handles CBORules.TextUpdate
        If DirectCast(sender, ComboBox).SelectedText = String.Empty And DirectCast(sender, ComboBox).Text = String.Empty Then
            CurrentRule.RuleID = -1
        End If
    End Sub

    Private Sub chkCondition_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkCondition.CheckedChanged
        Dim response As System.Windows.Forms.DialogResult
        If chkCondition.Checked = False Then
            response = System.Windows.Forms.MessageBox.Show("Desea deshabilitar el combobox?. Perderá todos los cambios.", "ATENCION", MessageBoxButtons.YesNo)
            If response = DialogResult.Yes Then
                CBOElse.Enabled = False
                btnElseGoRule.Enabled = False
                CBOElse.Text = String.Empty
                CurrentRule.ExecuteElseID = -1
                chkCondition.Checked = False
            End If
        Else
            CBOElse.Enabled = True
            btnElseGoRule.Enabled = True
            chkCondition.Checked = True
        End If
    End Sub

    Private Sub btnEvaluateGoRule_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEvaluateGoRule.Click
        If Not IsNothing(CBOEvaluateRule.SelectedValue) Then
            Dim wfbe As New WFBusinessExt
            Try
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.WaitCursor})
                Dim ruleId As Int64 = Int64.Parse(CBOEvaluateRule.SelectedValue)
                RaiseEvent OpenMissedRule(wfbe.GetWorkflowIdByRule(ruleId), ruleId)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                wfbe = Nothing
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.Default})
            End Try
        End If
    End Sub

    Private Sub btnElseGoRule_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnElseGoRule.Click
        If Not IsNothing(CBOElse.SelectedValue) Then
            Dim wfbe As New WFBusinessExt
            Try
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.WaitCursor})
                Dim ruleId As Int64 = Int64.Parse(CBOElse.SelectedValue)
                RaiseEvent OpenMissedRule(wfbe.GetWorkflowIdByRule(ruleId), ruleId)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                wfbe = Nothing
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.Default})
            End Try
        End If
    End Sub

    Private Sub btnRulesGoRule_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnRulesGoRule.Click
        If Not IsNothing(CBORules.SelectedValue) Then
            Dim wfbe As New WFBusinessExt
            Try
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.WaitCursor})
                Dim ruleId As Int64 = Int64.Parse(CBORules.SelectedValue)
                RaiseEvent OpenMissedRule(wfbe.GetWorkflowIdByRule(ruleId), ruleId)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                wfbe = Nothing
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.Default})
            End Try
        End If
    End Sub
End Class
