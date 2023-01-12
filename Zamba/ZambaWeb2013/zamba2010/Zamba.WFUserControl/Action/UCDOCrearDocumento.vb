Public Class UCDOCrearDocumento
    Inherits ZRuleControl
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents txtPath As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblSecondaryValue As ZLabel
    Friend WithEvents txtText As TextBox
    Friend WithEvents ZButton1 As ZButton
    Friend WithEvents ZPanel1 As ZPanel
    Friend WithEvents Label2 As ZLabel

    ''' <summary>
    '''         
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myRule() As IDOCrearDocumento
        Get
            Return DirectCast(MyBase.Rule, IDOCrearDocumento)
        End Get
        Set(ByVal value As IDOCrearDocumento)
            MyBase.Rule = value
        End Set
    End Property

    Public Sub New(ByRef rule As IDOCrearDocumento, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
        myRule = rule

        txtText.Text = myRule.text
        txtPath.Text = myRule.path
    End Sub

    Private Shadows Sub InitializeComponent()
        Label2 = New ZLabel()
        txtText = New TextBox()
        lblSecondaryValue = New ZLabel()
        txtPath = New Zamba.AppBlock.TextoInteligenteTextBox()
        btnAceptar = New ZButton()
        ZButton1 = New ZButton()
        ZPanel1 = New ZPanel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        ZPanel1.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(txtText)
        tbRule.Controls.Add(Label2)
        tbRule.Controls.Add(btnAceptar)
        tbRule.Controls.Add(ZPanel1)
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Dock = System.Windows.Forms.DockStyle.Top
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(3, 88)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(119, 16)
        Label2.TabIndex = 48
        Label2.Text = "Texto a generar:"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtText
        '
        txtText.Dock = System.Windows.Forms.DockStyle.Top
        txtText.Location = New System.Drawing.Point(3, 104)
        txtText.Multiline = True
        txtText.Name = "txtText"
        txtText.Size = New System.Drawing.Size(610, 233)
        txtText.TabIndex = 47
        '
        'lblSecondaryValue
        '
        lblSecondaryValue.AutoSize = True
        lblSecondaryValue.BackColor = System.Drawing.Color.Transparent
        lblSecondaryValue.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblSecondaryValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblSecondaryValue.Location = New System.Drawing.Point(3, 10)
        lblSecondaryValue.Name = "lblSecondaryValue"
        lblSecondaryValue.Size = New System.Drawing.Size(186, 16)
        lblSecondaryValue.TabIndex = 46
        lblSecondaryValue.Text = "Ruta completa del archivo:"
        lblSecondaryValue.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtPath
        '
        txtPath.Location = New System.Drawing.Point(6, 29)
        txtPath.Name = "txtPath"
        txtPath.Size = New System.Drawing.Size(481, 27)
        txtPath.TabIndex = 44
        txtPath.Text = ""
        '
        'btnAceptar
        '
        btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAceptar.FlatStyle = FlatStyle.Flat
        btnAceptar.ForeColor = System.Drawing.Color.White
        btnAceptar.Location = New System.Drawing.Point(445, 354)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(95, 29)
        btnAceptar.TabIndex = 45
        btnAceptar.Text = "Guardar"
        btnAceptar.UseVisualStyleBackColor = False
        '
        'ZButton1
        '
        ZButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        ZButton1.FlatStyle = FlatStyle.Flat
        ZButton1.ForeColor = System.Drawing.Color.White
        ZButton1.Location = New System.Drawing.Point(493, 29)
        ZButton1.Name = "ZButton1"
        ZButton1.Size = New System.Drawing.Size(95, 29)
        ZButton1.TabIndex = 49
        ZButton1.Text = "Examinar"
        ZButton1.UseVisualStyleBackColor = False
        '
        'ZPanel1
        '
        ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel1.Controls.Add(ZButton1)
        ZPanel1.Controls.Add(lblSecondaryValue)
        ZPanel1.Controls.Add(txtPath)
        ZPanel1.Dock = System.Windows.Forms.DockStyle.Top
        ZPanel1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel1.Location = New System.Drawing.Point(3, 3)
        ZPanel1.Name = "ZPanel1"
        ZPanel1.Size = New System.Drawing.Size(610, 85)
        ZPanel1.TabIndex = 50
        '
        'UCDOCrearDocumento
        '
        Name = "UCDOCrearDocumento"
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ZPanel1.ResumeLayout(False)
        ZPanel1.PerformLayout()
        ResumeLayout(False)

    End Sub



    Private Sub btnAceptar_Click(sender As System.Object, e As EventArgs) Handles btnAceptar.Click
        myRule.text = txtText.Text
        myRule.path = txtPath.Text
        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, myRule.text)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 1, myRule.path)
    End Sub

    Private Sub ZButton1_Click(sender As Object, e As EventArgs) Handles ZButton1.Click
        Try
            Dim BrowserFileDialog As New FolderBrowserDialog
            If BrowserFileDialog.ShowDialog() = DialogResult.OK Then
                txtPath.Text = BrowserFileDialog.SelectedPath
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class