Public Class UCDoImport
    Inherits ZRuleControl

    ''Friend WithEvents lblEnConstruccion As ZLabel
    Friend WithEvents lblTextToReplace As ZLabel
    Friend WithEvents txtText As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtVariable As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents txtHasta As TextBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents txtDesde As TextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents btnAdd As ZButton
    Friend WithEvents lstReplaceFields As ListBox
    Friend WithEvents ZButton As ZButton
    Friend WithEvents btnRemove As ZButton
    Friend WithEvents lblReadFile As ZLabel
    Private _currentRule As IDoImport
    Public Sub New(ByRef rule As IDoImport, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
        _currentRule = rule
        txtText.Text = _currentRule.TextToParse
        For Each field As String In _currentRule.ListToParse.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)
            lstReplaceFields.Items.Add(field)
        Next
    End Sub

    Private Shadows Sub InitializeComponent()
        lblReadFile = New ZLabel()
        txtText = New Zamba.AppBlock.TextoInteligenteTextBox()
        lblTextToReplace = New ZLabel()
        Label2 = New ZLabel()
        txtDesde = New TextBox()
        Label4 = New ZLabel()
        txtHasta = New TextBox()
        Label5 = New ZLabel()
        txtVariable = New Zamba.AppBlock.TextoInteligenteTextBox()
        btnAdd = New ZButton()
        lstReplaceFields = New ListBox()
        ZButton = New ZButton()
        btnRemove = New ZButton()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(btnRemove)
        tbRule.Controls.Add(ZButton)
        tbRule.Controls.Add(lstReplaceFields)
        tbRule.Controls.Add(btnAdd)
        tbRule.Controls.Add(txtVariable)
        tbRule.Controls.Add(Label5)
        tbRule.Controls.Add(txtHasta)
        tbRule.Controls.Add(Label4)
        tbRule.Controls.Add(txtDesde)
        tbRule.Controls.Add(Label2)
        tbRule.Controls.Add(lblReadFile)
        tbRule.Controls.Add(txtText)
        tbRule.Controls.Add(lblTextToReplace)
        '
        'lblReadFile
        '
        lblReadFile.AutoSize = True
        lblReadFile.BackColor = System.Drawing.Color.Transparent
        lblReadFile.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblReadFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblReadFile.Location = New System.Drawing.Point(20, 29)
        lblReadFile.Name = "lblReadFile"
        lblReadFile.Size = New System.Drawing.Size(126, 16)
        lblReadFile.TabIndex = 40
        lblReadFile.Text = "Variable a utilizar:"
        lblReadFile.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtText
        '
        txtText.Location = New System.Drawing.Point(166, 29)
        txtText.MaxLength = 4000
        txtText.Name = "txtText"
        txtText.Size = New System.Drawing.Size(415, 21)
        txtText.TabIndex = 39
        txtText.Text = ""
        '
        'lblTextToReplace
        '
        lblTextToReplace.AutoSize = True
        lblTextToReplace.BackColor = System.Drawing.Color.Transparent
        lblTextToReplace.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblTextToReplace.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblTextToReplace.Location = New System.Drawing.Point(20, 162)
        lblTextToReplace.Name = "lblTextToReplace"
        lblTextToReplace.Size = New System.Drawing.Size(140, 16)
        lblTextToReplace.TabIndex = 38
        lblTextToReplace.Text = "Texto a reemplazar:"
        lblTextToReplace.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(20, 84)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(54, 16)
        Label2.TabIndex = 41
        Label2.Text = "Desde:"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtDesde
        '
        txtDesde.Location = New System.Drawing.Point(23, 103)
        txtDesde.Name = "txtDesde"
        txtDesde.Size = New System.Drawing.Size(87, 23)
        txtDesde.TabIndex = 42
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label4.Location = New System.Drawing.Point(127, 82)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(52, 16)
        Label4.TabIndex = 43
        Label4.Text = "Hasta:"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtHasta
        '
        txtHasta.Location = New System.Drawing.Point(130, 103)
        txtHasta.Name = "txtHasta"
        txtHasta.Size = New System.Drawing.Size(81, 23)
        txtHasta.TabIndex = 44
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label5.Location = New System.Drawing.Point(232, 81)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(86, 16)
        Label5.TabIndex = 45
        Label5.Text = "Guardar en:"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtVariable
        '
        txtVariable.Location = New System.Drawing.Point(235, 105)
        txtVariable.MaxLength = 4000
        txtVariable.Name = "txtVariable"
        txtVariable.Size = New System.Drawing.Size(271, 21)
        txtVariable.TabIndex = 46
        txtVariable.Text = ""
        '
        'btnAdd
        '
        btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.ForeColor = System.Drawing.Color.White
        btnAdd.Location = New System.Drawing.Point(512, 103)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New System.Drawing.Size(85, 35)
        btnAdd.TabIndex = 47
        btnAdd.Text = "Agregar"
        btnAdd.UseVisualStyleBackColor = False
        '
        'lstReplaceFields
        '
        lstReplaceFields.FormattingEnabled = True
        lstReplaceFields.HorizontalScrollbar = True
        lstReplaceFields.ItemHeight = 16
        lstReplaceFields.Location = New System.Drawing.Point(23, 181)
        lstReplaceFields.Name = "lstReplaceFields"
        lstReplaceFields.Size = New System.Drawing.Size(483, 164)
        lstReplaceFields.TabIndex = 48
        '
        'ZButton
        '
        ZButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        ZButton.FlatStyle = FlatStyle.Flat
        ZButton.ForeColor = System.Drawing.Color.White
        ZButton.Location = New System.Drawing.Point(451, 365)
        ZButton.Name = "ZButton"
        ZButton.Size = New System.Drawing.Size(86, 29)
        ZButton.TabIndex = 49
        ZButton.Text = "Guardar"
        ZButton.UseVisualStyleBackColor = False
        '
        'btnRemove
        '
        btnRemove.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnRemove.FlatStyle = FlatStyle.Flat
        btnRemove.ForeColor = System.Drawing.Color.White
        btnRemove.Location = New System.Drawing.Point(512, 204)
        btnRemove.Name = "btnRemove"
        btnRemove.Size = New System.Drawing.Size(85, 32)
        btnRemove.TabIndex = 50
        btnRemove.Text = "Eliminar"
        btnRemove.UseVisualStyleBackColor = False
        '
        'UCDoImport
        '
        Name = "UCDoImport"
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            If Not String.IsNullOrEmpty(txtDesde.Text) AndAlso Not String.IsNullOrEmpty(txtHasta.Text) AndAlso Not String.IsNullOrEmpty(txtVariable.Text) Then

                lstReplaceFields.Items.Add(txtDesde.Text & "," & txtHasta.Text & "¶" & txtVariable.Text)
            Else
                MessageBox.Show("Faltan completar campos.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnRemove.Click
        Try
            If Not lstReplaceFields.SelectedItem Is Nothing Then
                lstReplaceFields.Items.Remove(lstReplaceFields.SelectedItem)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ZButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ZButton.Click
        Try
            _currentRule.TextToParse = txtText.Text
            _currentRule.ListToParse = ""
            For Each field As String In lstReplaceFields.Items
                If Not String.IsNullOrEmpty(field) Then _currentRule.ListToParse += field & "§"
            Next
            If _currentRule.ListToParse.Length = 0 OrElse lstReplaceFields.Items.Count = 0 Then
                _currentRule.ListToParse = ""
            Else
                _currentRule.ListToParse = _currentRule.ListToParse.Substring(0, _currentRule.ListToParse.Length - 1)
            End If
            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, _currentRule.TextToParse)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, _currentRule.ListToParse)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class


