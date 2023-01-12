Public Class UCDoInsertSLST
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
    Friend WithEvents BtnSave As ZButton
    Friend Shadows WithEvents lblDescription As ZLabel
    Friend WithEvents lblID As ZLabel
    Friend WithEvents txtIDSLST As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtID As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblID_table As ZLabel
    Friend WithEvents txtDescription As Zamba.AppBlock.TextoInteligenteTextBox

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        BtnSave = New ZButton
        lblDescription = New ZLabel
        txtDescription = New Zamba.AppBlock.TextoInteligenteTextBox
        txtIDSLST = New Zamba.AppBlock.TextoInteligenteTextBox
        lblID = New ZLabel
        GroupBox1 = New GroupBox
        lblID_table = New ZLabel
        txtID = New Zamba.AppBlock.TextoInteligenteTextBox
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(GroupBox1)
        tbRule.Controls.Add(BtnSave)
        tbRule.Size = New System.Drawing.Size(468, 528)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(476, 554)
        '
        'BtnSave
        '
        BtnSave.DialogResult = System.Windows.Forms.DialogResult.None
        BtnSave.Location = New System.Drawing.Point(360, 316)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New System.Drawing.Size(67, 27)
        BtnSave.TabIndex = 3
        BtnSave.Text = "Guardar"
        '
        'lblDescription
        '
        lblDescription.AutoSize = True
        lblDescription.BackColor = System.Drawing.Color.Transparent
        lblDescription.Location = New System.Drawing.Point(26, 144)
        lblDescription.Name = "lblDescription"
        lblDescription.Size = New System.Drawing.Size(65, 13)
        lblDescription.TabIndex = 21
        lblDescription.Text = "Descripción:"
        '
        'txtDescription
        '
        txtDescription.Location = New System.Drawing.Point(127, 136)
        txtDescription.MaxLength = 4000
        txtDescription.Name = "txtDescription"
        txtDescription.Size = New System.Drawing.Size(224, 21)
        txtDescription.TabIndex = 24
        txtDescription.Text = ""
        '
        'txtIDSLST
        '
        txtIDSLST.Location = New System.Drawing.Point(127, 39)
        txtIDSLST.MaxLength = 4000
        txtIDSLST.Name = "txtIDSLST"
        txtIDSLST.Size = New System.Drawing.Size(224, 21)
        txtIDSLST.TabIndex = 29
        txtIDSLST.Text = ""
        '
        'lblID
        '
        lblID.AutoSize = True
        lblID.BackColor = System.Drawing.Color.Transparent
        lblID.Location = New System.Drawing.Point(26, 94)
        lblID.Name = "lblID"
        lblID.Size = New System.Drawing.Size(44, 13)
        lblID.TabIndex = 30
        lblID.Text = "Código:"
        '
        'GroupBox1
        '
        GroupBox1.Controls.Add(lblID_table)
        GroupBox1.Controls.Add(txtID)
        GroupBox1.Controls.Add(lblID)
        GroupBox1.Controls.Add(txtIDSLST)
        GroupBox1.Controls.Add(txtDescription)
        GroupBox1.Controls.Add(lblDescription)
        GroupBox1.Location = New System.Drawing.Point(45, 106)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(382, 187)
        GroupBox1.TabIndex = 31
        GroupBox1.TabStop = False
        '
        'lblID_table
        '
        lblID_table.AutoSize = True
        lblID_table.Location = New System.Drawing.Point(26, 47)
        lblID_table.Name = "lblID_table"
        lblID_table.Size = New System.Drawing.Size(51, 13)
        lblID_table.TabIndex = 32
        lblID_table.Text = "ID Tabla:"
        '
        'txtID
        '
        txtID.Location = New System.Drawing.Point(127, 86)
        txtID.Name = "txtID"
        txtID.Size = New System.Drawing.Size(224, 21)
        txtID.TabIndex = 32
        txtID.Text = ""
        '
        'UCDoInsertSLST
        '
        Name = "UCDoInsertSLST"
        Size = New System.Drawing.Size(476, 554)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDoInsertSLST
    Public Sub New(ByRef DoInsertSLST As IDoInsertSLST, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoInsertSLST, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = DoInsertSLST

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
            txtIDSLST.Text = CurrentRule.IDSLST
            txtID.Text = CurrentRule.Code
            txtDescription.Text = CurrentRule.Description


        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#End Region

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            CurrentRule.IDSLST = txtIDSLST.Text
            CurrentRule.Code = txtID.Text
            CurrentRule.Description = txtDescription.Text

            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.IDSLST)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.Code)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.Description)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
