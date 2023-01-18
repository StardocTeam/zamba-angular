Public Class UCDOEditTable
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
    Friend WithEvents txtVarSource As TextBox
    Friend Shadows WithEvents Label1 As ZLabel
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents txtVarDestiny As TextBox
    Friend WithEvents RdoMin As System.Windows.Forms.RadioButton
    Friend WithEvents RdoMax As System.Windows.Forms.RadioButton
    Friend WithEvents RdoFirst As System.Windows.Forms.RadioButton
    Friend WithEvents RdoLast As System.Windows.Forms.RadioButton
    Friend WithEvents RdoNone As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblKeyColum As ZLabel
    Friend WithEvents txtKeyColumn As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblFilterColumn As ZLabel
    Friend WithEvents txtEditColumn As Zamba.AppBlock.TextoInteligenteTextBox

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        BtnSave = New ZButton()
        txtVarSource = New TextBox()
        Label1 = New ZLabel()
        Label3 = New ZLabel()
        txtVarDestiny = New TextBox()
        RdoMin = New System.Windows.Forms.RadioButton()
        RdoMax = New System.Windows.Forms.RadioButton()
        RdoFirst = New System.Windows.Forms.RadioButton()
        RdoLast = New System.Windows.Forms.RadioButton()
        RdoNone = New System.Windows.Forms.RadioButton()
        GroupBox1 = New GroupBox()
        lblFilterColumn = New ZLabel()
        txtEditColumn = New Zamba.AppBlock.TextoInteligenteTextBox()
        lblKeyColum = New ZLabel()
        txtKeyColumn = New Zamba.AppBlock.TextoInteligenteTextBox()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(lblFilterColumn)
        tbRule.Controls.Add(txtEditColumn)
        tbRule.Controls.Add(GroupBox1)
        tbRule.Controls.Add(lblKeyColum)
        tbRule.Controls.Add(txtKeyColumn)
        tbRule.Controls.Add(Label3)
        tbRule.Controls.Add(txtVarDestiny)
        tbRule.Controls.Add(Label1)
        tbRule.Controls.Add(txtVarSource)
        tbRule.Controls.Add(BtnSave)
        tbRule.Size = New System.Drawing.Size(507, 578)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(515, 607)
        '
        'BtnSave
        '
        BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        BtnSave.FlatStyle = FlatStyle.Flat
        BtnSave.ForeColor = System.Drawing.Color.White
        BtnSave.Location = New System.Drawing.Point(367, 299)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New System.Drawing.Size(86, 33)
        BtnSave.TabIndex = 3
        BtnSave.Text = "Guardar"
        BtnSave.UseVisualStyleBackColor = False
        '
        'txtVarSource
        '
        txtVarSource.Location = New System.Drawing.Point(14, 29)
        txtVarSource.Name = "txtVarSource"
        txtVarSource.Size = New System.Drawing.Size(224, 23)
        txtVarSource.TabIndex = 17
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(11, 13)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(266, 16)
        Label1.TabIndex = 18
        Label1.Text = "Nombre de Variable de Origen de Datos"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(14, 283)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(296, 16)
        Label3.TabIndex = 21
        Label3.Text = "Nombre de Variable de Destino de los Datos"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtVarDestiny
        '
        txtVarDestiny.Location = New System.Drawing.Point(17, 299)
        txtVarDestiny.Name = "txtVarDestiny"
        txtVarDestiny.Size = New System.Drawing.Size(224, 23)
        txtVarDestiny.TabIndex = 20
        '
        'RdoMin
        '
        RdoMin.AutoSize = True
        RdoMin.Location = New System.Drawing.Point(11, 46)
        RdoMin.Name = "RdoMin"
        RdoMin.Size = New System.Drawing.Size(70, 20)
        RdoMin.TabIndex = 30
        RdoMin.Text = "Mínimo"
        RdoMin.UseVisualStyleBackColor = True
        '
        'RdoMax
        '
        RdoMax.AutoSize = True
        RdoMax.Checked = True
        RdoMax.Location = New System.Drawing.Point(11, 20)
        RdoMax.Name = "RdoMax"
        RdoMax.Size = New System.Drawing.Size(74, 20)
        RdoMax.TabIndex = 29
        RdoMax.TabStop = True
        RdoMax.Text = "Máximo"
        RdoMax.UseVisualStyleBackColor = True
        '
        'RdoFirst
        '
        RdoFirst.AutoSize = True
        RdoFirst.Location = New System.Drawing.Point(11, 69)
        RdoFirst.Name = "RdoFirst"
        RdoFirst.Size = New System.Drawing.Size(74, 20)
        RdoFirst.TabIndex = 32
        RdoFirst.Text = "Primero"
        RdoFirst.UseVisualStyleBackColor = True
        '
        'RdoLast
        '
        RdoLast.AutoSize = True
        RdoLast.Location = New System.Drawing.Point(11, 92)
        RdoLast.Name = "RdoLast"
        RdoLast.Size = New System.Drawing.Size(66, 20)
        RdoLast.TabIndex = 31
        RdoLast.Text = "Ultimo"
        RdoLast.UseVisualStyleBackColor = True
        '
        'RdoNone
        '
        RdoNone.AutoSize = True
        RdoNone.Location = New System.Drawing.Point(11, 115)
        RdoNone.Name = "RdoNone"
        RdoNone.Size = New System.Drawing.Size(142, 20)
        RdoNone.TabIndex = 33
        RdoNone.Text = "Sin Ordenamiento"
        RdoNone.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        GroupBox1.BackColor = System.Drawing.Color.Transparent
        GroupBox1.Controls.Add(RdoMin)
        GroupBox1.Controls.Add(RdoMax)
        GroupBox1.Controls.Add(RdoFirst)
        GroupBox1.Controls.Add(RdoLast)
        GroupBox1.Controls.Add(RdoNone)
        GroupBox1.Location = New System.Drawing.Point(17, 129)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(472, 142)
        GroupBox1.TabIndex = 32
        GroupBox1.TabStop = False
        GroupBox1.Text = "Tipo de Edicion"
        '
        'lblFilterColumn
        '
        lblFilterColumn.AutoSize = True
        lblFilterColumn.BackColor = System.Drawing.Color.Transparent
        lblFilterColumn.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblFilterColumn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblFilterColumn.Location = New System.Drawing.Point(18, 101)
        lblFilterColumn.Name = "lblFilterColumn"
        lblFilterColumn.Size = New System.Drawing.Size(120, 16)
        lblFilterColumn.TabIndex = 38
        lblFilterColumn.Text = "Columna Edicion:"
        lblFilterColumn.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtEditColumn
        '
        txtEditColumn.Location = New System.Drawing.Point(258, 93)
        txtEditColumn.MaxLength = 4000
        txtEditColumn.Name = "txtEditColumn"
        txtEditColumn.Size = New System.Drawing.Size(231, 21)
        txtEditColumn.TabIndex = 37
        txtEditColumn.Text = ""
        '
        'lblKeyColum
        '
        lblKeyColum.AutoSize = True
        lblKeyColum.BackColor = System.Drawing.Color.Transparent
        lblKeyColum.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblKeyColum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblKeyColum.Location = New System.Drawing.Point(18, 75)
        lblKeyColum.Name = "lblKeyColum"
        lblKeyColum.Size = New System.Drawing.Size(109, 16)
        lblKeyColum.TabIndex = 36
        lblKeyColum.Text = "Columna clave:"
        lblKeyColum.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtKeyColumn
        '
        txtKeyColumn.Location = New System.Drawing.Point(258, 67)
        txtKeyColumn.MaxLength = 4000
        txtKeyColumn.Name = "txtKeyColumn"
        txtKeyColumn.Size = New System.Drawing.Size(231, 21)
        txtKeyColumn.TabIndex = 35
        txtKeyColumn.Text = ""
        '
        'UCDOEditTable
        '
        Name = "UCDOEditTable"
        Size = New System.Drawing.Size(515, 607)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDOEditTable
    Public Sub New(ByRef DOEditTable As IDOEditTable, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DOEditTable, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = DOEditTable
        LoadRulesParams()
    End Sub
#End Region


    Private Sub UCDOSelect_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            LoadRulesParams()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub LoadRulesParams()
        Try
            txtVarSource.Text = CurrentRule.VarSource
            txtKeyColumn.Text = CurrentRule.KeyColumn
            txtEditColumn.Text = CurrentRule.EditColumn
            txtVarDestiny.Text = CurrentRule.VarDestiny

            Select Case CurrentRule.EditType
                Case 0
                    RdoMax.Checked = True
                Case 1
                    RdoMin.Checked = True
                Case 2
                    RdoLast.Checked = True
                Case 3
                    RdoFirst.Checked = True
                Case 4
                    RdoNone.Checked = True
                Case Else
                    RdoMax.Checked = True
            End Select

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            CurrentRule.VarSource = txtVarSource.Text
            CurrentRule.VarDestiny = txtVarDestiny.Text
            CurrentRule.KeyColumn = txtKeyColumn.Text
            CurrentRule.EditColumn = txtEditColumn.Text

            If RdoMax.Checked = True Then
                CurrentRule.EditType = 0
            ElseIf RdoMin.Checked = True Then
                CurrentRule.EditType = 1
            ElseIf RdoLast.Checked = True Then
                CurrentRule.EditType = 2
            ElseIf RdoFirst.Checked = True Then
                CurrentRule.EditType = 3
            ElseIf RdoNone.Checked = True Then
                CurrentRule.EditType = 4
            End If

            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.VarSource)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.KeyColumn)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.EditColumn)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.EditType)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.VarDestiny)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class