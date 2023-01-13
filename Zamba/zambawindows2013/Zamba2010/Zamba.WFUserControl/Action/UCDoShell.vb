Public Class UCDOShell
    Inherits ZRuleControl
    Friend WithEvents btnExaminar As ZButton
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnSave As ZButton
    Friend WithEvents txtFullPath As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtParameters As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents chkUseProcess As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As ZLabel

    Private Shadows Sub InitializeComponent()
        Label2 = New ZLabel()
        btnExaminar = New ZButton()
        Label4 = New ZLabel()
        OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        btnSave = New ZButton()
        txtFullPath = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtParameters = New Zamba.AppBlock.TextoInteligenteTextBox()
        chkUseProcess = New System.Windows.Forms.CheckBox()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(chkUseProcess)
        tbRule.Controls.Add(txtParameters)
        tbRule.Controls.Add(txtFullPath)
        tbRule.Controls.Add(btnSave)
        tbRule.Controls.Add(Label4)
        tbRule.Controls.Add(btnExaminar)
        tbRule.Controls.Add(Label2)
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Dock = System.Windows.Forms.DockStyle.Top
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(3, 3)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(66, 16)
        Label2.TabIndex = 0
        Label2.Text = "Examinar"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'btnExaminar
        '
        btnExaminar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnExaminar.FlatStyle = FlatStyle.Flat
        btnExaminar.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        btnExaminar.ForeColor = System.Drawing.Color.White
        btnExaminar.Location = New System.Drawing.Point(495, 46)
        btnExaminar.Name = "btnExaminar"
        btnExaminar.Size = New System.Drawing.Size(88, 23)
        btnExaminar.TabIndex = 31
        btnExaminar.Text = "Examinar..."
        btnExaminar.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label4.Location = New System.Drawing.Point(9, 80)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(155, 16)
        Label4.TabIndex = 32
        Label4.Text = "Parametros (Opcional)"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        '
        'btnSave
        '
        btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        btnSave.ForeColor = System.Drawing.Color.White
        btnSave.Location = New System.Drawing.Point(410, 135)
        btnSave.Name = "btnSave"
        btnSave.Size = New System.Drawing.Size(87, 23)
        btnSave.TabIndex = 34
        btnSave.Text = "Guardar"
        btnSave.UseVisualStyleBackColor = True
        '
        'txtFullPath
        '
        txtFullPath.Dock = System.Windows.Forms.DockStyle.Top
        txtFullPath.Location = New System.Drawing.Point(3, 19)
        txtFullPath.Name = "txtFullPath"
        txtFullPath.Size = New System.Drawing.Size(610, 21)
        txtFullPath.TabIndex = 35
        txtFullPath.Text = ""
        '
        'txtParameters
        '
        txtParameters.Location = New System.Drawing.Point(170, 77)
        txtParameters.Name = "txtParameters"
        txtParameters.Size = New System.Drawing.Size(413, 21)
        txtParameters.TabIndex = 37
        txtParameters.Text = ""
        '
        'chkUseProcess
        '
        chkUseProcess.AutoSize = True
        chkUseProcess.BackColor = System.Drawing.Color.Transparent
        chkUseProcess.Location = New System.Drawing.Point(9, 116)
        chkUseProcess.Name = "chkUseProcess"
        chkUseProcess.Size = New System.Drawing.Size(179, 20)
        chkUseProcess.TabIndex = 38
        chkUseProcess.Text = "Ejecutar como proceso"
        chkUseProcess.UseVisualStyleBackColor = False
        '
        'UCDOShell
        '
        Name = "UCDOShell"
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub
    Private Shadows myrule As IDOSHELL

    Public Sub New(ByVal rule As IDOSHELL, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
        myrule = rule
        loadParameters()
    End Sub

    Private Sub loadParameters()
        If Not String.IsNullOrEmpty(myrule.Filepath) Then txtFullPath.Text = myrule.Filepath
        If Not String.IsNullOrEmpty(myrule.Parameter) Then txtParameters.Text = myrule.Parameter
        chkUseProcess.Checked = myrule.UseProcess
    End Sub
    Private Sub btnExaminar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExaminar.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            txtFullPath.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSave.Click

        Try
            myrule.Filepath = txtFullPath.Text
            myrule.Parameter = txtParameters.Text
            myrule.UseProcess = chkUseProcess.Checked
            WFRulesBusiness.UpdateParamItem(myrule.ID, 0, myrule.Filepath)
            WFRulesBusiness.UpdateParamItem(myrule.ID, 1, myrule.Parameter)
            WFRulesBusiness.UpdateParamItem(myrule.ID, 2, myrule.UseProcess)
            UserBusiness.Rights.SaveAction(myrule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & myrule.Name & "(" & myrule.ID & ")")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub


End Class