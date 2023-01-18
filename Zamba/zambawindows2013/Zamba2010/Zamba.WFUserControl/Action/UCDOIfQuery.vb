Public Class UCDOIfQuery
    Inherits ZRuleControl


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
    Friend WithEvents txtScriptSql As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents BtnSave As ZButton
    Friend WithEvents Label2 As ZLabel
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents txtIfValue As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents txtSql2 As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents txtIfSql As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents cmbOper As ComboBox
    Friend WithEvents optNonQuery As System.Windows.Forms.RadioButton
    Friend WithEvents optEscalar As System.Windows.Forms.RadioButton
    Friend WithEvents optDataset As System.Windows.Forms.RadioButton
    Friend WithEvents ZPanel1 As ZPanel
    Friend WithEvents txtNombreConsulta As Zamba.AppBlock.TextoInteligenteTextBox

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        txtScriptSql = New Zamba.AppBlock.TextoInteligenteTextBox()
        BtnSave = New ZButton()
        Label2 = New ZLabel()
        Label3 = New ZLabel()
        txtNombreConsulta = New Zamba.AppBlock.TextoInteligenteTextBox()
        Label4 = New ZLabel()
        txtIfSql = New Zamba.AppBlock.TextoInteligenteTextBox()
        Label5 = New ZLabel()
        txtSql2 = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtIfValue = New Zamba.AppBlock.TextoInteligenteTextBox()
        Label6 = New ZLabel()
        cmbOper = New ComboBox()
        optNonQuery = New System.Windows.Forms.RadioButton()
        optEscalar = New System.Windows.Forms.RadioButton()
        optDataset = New System.Windows.Forms.RadioButton()
        ZPanel1 = New ZPanel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        ZPanel1.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(txtSql2)
        tbRule.Controls.Add(Label5)
        tbRule.Controls.Add(txtScriptSql)
        tbRule.Controls.Add(Label2)
        tbRule.Controls.Add(ZPanel1)
        tbRule.Controls.Add(txtIfSql)
        tbRule.Controls.Add(optDataset)
        tbRule.Controls.Add(optEscalar)
        tbRule.Controls.Add(optNonQuery)
        tbRule.Controls.Add(Label4)
        tbRule.Controls.Add(txtNombreConsulta)
        tbRule.Controls.Add(Label3)
        tbRule.Controls.Add(BtnSave)
        tbRule.Size = New System.Drawing.Size(547, 525)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(555, 554)
        '
        'txtScriptSql
        '
        txtScriptSql.Dock = System.Windows.Forms.DockStyle.Top
        txtScriptSql.Location = New System.Drawing.Point(3, 177)
        txtScriptSql.MaxLength = 4000
        txtScriptSql.Name = "txtScriptSql"
        txtScriptSql.Size = New System.Drawing.Size(541, 107)
        txtScriptSql.TabIndex = 0
        txtScriptSql.Text = ""
        '
        'BtnSave
        '
        BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        BtnSave.FlatStyle = FlatStyle.Flat
        BtnSave.ForeColor = System.Drawing.Color.White
        BtnSave.Location = New System.Drawing.Point(421, 492)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New System.Drawing.Size(91, 27)
        BtnSave.TabIndex = 3
        BtnSave.Text = "Guardar"
        BtnSave.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Dock = System.Windows.Forms.DockStyle.Top
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(3, 161)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(289, 16)
        Label2.TabIndex = 19
        Label2.Text = "Script a ejecutar si se cumple la condicion"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(301, 425)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(166, 16)
        Label3.TabIndex = 21
        Label3.Text = "Nombre de la consulta :"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtNombreConsulta
        '
        txtNombreConsulta.BackColor = System.Drawing.Color.White
        txtNombreConsulta.Location = New System.Drawing.Point(304, 452)
        txtNombreConsulta.MaxLength = 4000
        txtNombreConsulta.Name = "txtNombreConsulta"
        txtNombreConsulta.Size = New System.Drawing.Size(227, 21)
        txtNombreConsulta.TabIndex = 24
        txtNombreConsulta.Text = ""
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Dock = System.Windows.Forms.DockStyle.Top
        Label4.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label4.Location = New System.Drawing.Point(3, 3)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(110, 16)
        Label4.TabIndex = 26
        Label4.Text = "Script de sqlif :"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtIfSql
        '
        txtIfSql.Dock = System.Windows.Forms.DockStyle.Top
        txtIfSql.Location = New System.Drawing.Point(3, 19)
        txtIfSql.MaxLength = 4000
        txtIfSql.Name = "txtIfSql"
        txtIfSql.Size = New System.Drawing.Size(541, 97)
        txtIfSql.TabIndex = 25
        txtIfSql.Text = ""
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Dock = System.Windows.Forms.DockStyle.Top
        Label5.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label5.Location = New System.Drawing.Point(3, 284)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(313, 16)
        Label5.TabIndex = 28
        Label5.Text = "Script a ejecutar si NO se cumple la condicion"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtSql2
        '
        txtSql2.Dock = System.Windows.Forms.DockStyle.Top
        txtSql2.Location = New System.Drawing.Point(3, 300)
        txtSql2.MaxLength = 4000
        txtSql2.Name = "txtSql2"
        txtSql2.Size = New System.Drawing.Size(541, 107)
        txtSql2.TabIndex = 27
        txtSql2.Text = ""
        '
        'txtIfValue
        '
        txtIfValue.Location = New System.Drawing.Point(210, 3)
        txtIfValue.MaxLength = 4000
        txtIfValue.Name = "txtIfValue"
        txtIfValue.Size = New System.Drawing.Size(230, 21)
        txtIfValue.TabIndex = 29
        txtIfValue.Text = ""
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.BackColor = System.Drawing.Color.Transparent
        Label6.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label6.Location = New System.Drawing.Point(3, 6)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(72, 16)
        Label6.TabIndex = 30
        Label6.Text = "Resultado"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        '
        'cmbOper
        '
        cmbOper.DropDownStyle = ComboBoxStyle.DropDownList
        cmbOper.Location = New System.Drawing.Point(101, 3)
        cmbOper.Name = "cmbOper"
        cmbOper.Size = New System.Drawing.Size(92, 24)
        cmbOper.TabIndex = 31
        '
        'optNonQuery
        '
        optNonQuery.AutoSize = True
        optNonQuery.BackColor = System.Drawing.Color.Transparent
        optNonQuery.Location = New System.Drawing.Point(14, 430)
        optNonQuery.Name = "optNonQuery"
        optNonQuery.Size = New System.Drawing.Size(157, 20)
        optNonQuery.TabIndex = 32
        optNonQuery.Text = "No Devolver valores"
        optNonQuery.UseVisualStyleBackColor = False
        '
        'optEscalar
        '
        optEscalar.AutoSize = True
        optEscalar.BackColor = System.Drawing.Color.Transparent
        optEscalar.Location = New System.Drawing.Point(14, 453)
        optEscalar.Name = "optEscalar"
        optEscalar.Size = New System.Drawing.Size(172, 20)
        optEscalar.TabIndex = 33
        optEscalar.Text = "Devolver un solo valor"
        optEscalar.UseVisualStyleBackColor = False
        '
        'optDataset
        '
        optDataset.AutoSize = True
        optDataset.BackColor = System.Drawing.Color.Transparent
        optDataset.Checked = True
        optDataset.Location = New System.Drawing.Point(14, 474)
        optDataset.Name = "optDataset"
        optDataset.Size = New System.Drawing.Size(240, 20)
        optDataset.TabIndex = 34
        optDataset.TabStop = True
        optDataset.Text = "Devolver un conjunto de valores"
        optDataset.UseVisualStyleBackColor = False
        '
        'ZPanel1
        '
        ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel1.Controls.Add(Label6)
        ZPanel1.Controls.Add(cmbOper)
        ZPanel1.Controls.Add(txtIfValue)
        ZPanel1.Dock = System.Windows.Forms.DockStyle.Top
        ZPanel1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel1.Location = New System.Drawing.Point(3, 116)
        ZPanel1.Name = "ZPanel1"
        ZPanel1.Size = New System.Drawing.Size(541, 45)
        ZPanel1.TabIndex = 35
        '
        'UCDOIfQuery
        '
        Name = "UCDOIfQuery"
        Size = New System.Drawing.Size(555, 554)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ZPanel1.ResumeLayout(False)
        ZPanel1.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDOIfQuery
    Public Sub New(ByRef DOIfQuery As IDOIfQuery, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DOIfQuery, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = DOIfQuery

        Try
            LoadRulesParams()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
#End Region

#Region "Propietary"



#Region "Atributos y Documentos"
    Private Sub LoadRulesParams()
        Try
            txtScriptSql.Text = CurrentRule.SQL
            txtScriptSql.ModificarColores()
            txtSql2.Text = CurrentRule.SQL2
            txtSql2.ModificarColores()
            txtIfValue.Text = CurrentRule.IFValue
            txtIfValue.ModificarColores()
            txtIfSql.Text = CurrentRule.IFSQL
            txtIfSql.ModificarColores()
            txtNombreConsulta.Text = CurrentRule.HashTable
            Dim enumtype As Type = GetType(Comparadores)
            For Each x As Comparadores In [Enum].GetValues(enumtype)
                cmbOper.Items.Add(x.ToString)
            Next
            cmbOper.SelectedItem = cmbOper.Items(cmbOper.Items.IndexOf(CurrentRule.CompType.ToString))
            If Not IsNothing(CurrentRule.ExecuteType) Then
                If CurrentRule.ExecuteType.ToString = "ESCALAR" Then
                    optEscalar.Checked = True
                ElseIf CurrentRule.ExecuteType.ToString = "NONQUERY" Then
                    optNonQuery.Checked = True
                Else
                    optDataset.Checked = True
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#End Region

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            Dim Ht As String
            Try
                CurrentRule.SQL = txtScriptSql.Text.Replace("''", "'")
                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.SQL)

                CurrentRule.SQL2 = txtSql2.Text.Replace("''", "'")
                WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.SQL2)

                CurrentRule.IFSQL = txtIfSql.Text.Replace("''", "'")
                WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.IFSQL)

                CurrentRule.IFValue = txtIfValue.Text.Replace("''", "'")
                WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.IFValue)



                Ht = txtNombreConsulta.Text
                If Ht = "" Then
                    MessageBox.Show("Debe ingresar un nombre al conjunto de resultados", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                CurrentRule.HashTable = Ht.Trim
                WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.HashTable.Trim)
                CurrentRule.CompType = [Enum].Parse(GetType(Comparadores), cmbOper.SelectedItem.ToString)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.CompType)
                If optEscalar.Checked = True Then
                    CurrentRule.ExecuteType = "ESCALAR"
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 6, "ESCALAR")
                ElseIf optDataset.Checked Then
                    CurrentRule.ExecuteType = "DATASET"
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 6, "DATASET")
                Else
                    CurrentRule.ExecuteType = "NONQUERY"
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 6, "NONQUERY")
                End If
                UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("ERROR DE CONEXION " & ex.ToString, "Zamba - Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
