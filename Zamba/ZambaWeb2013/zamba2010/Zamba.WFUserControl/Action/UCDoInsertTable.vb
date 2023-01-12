Public Class UCDoInsertTable
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
    Friend WithEvents txtDSName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend Shadows WithEvents lblName As ZLabel
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents txtTable As Zamba.AppBlock.TextoInteligenteTextBox

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        BtnSave = New ZButton()
        txtDSName = New Zamba.AppBlock.TextoInteligenteTextBox()
        lblName = New ZLabel()
        Label3 = New ZLabel()
        txtTable = New Zamba.AppBlock.TextoInteligenteTextBox()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(Label3)
        tbRule.Controls.Add(txtTable)
        tbRule.Controls.Add(txtDSName)
        tbRule.Controls.Add(lblName)
        tbRule.Controls.Add(BtnSave)
        tbRule.Size = New System.Drawing.Size(507, 540)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(515, 569)
        '
        'BtnSave
        '
        BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        BtnSave.FlatStyle = FlatStyle.Flat
        BtnSave.ForeColor = System.Drawing.Color.White
        BtnSave.Location = New System.Drawing.Point(307, 227)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New System.Drawing.Size(87, 29)
        BtnSave.TabIndex = 3
        BtnSave.Text = "Guardar"
        BtnSave.UseVisualStyleBackColor = False
        '
        'txtDSName
        '
        txtDSName.Location = New System.Drawing.Point(91, 105)
        txtDSName.Name = "txtDSName"
        txtDSName.Size = New System.Drawing.Size(342, 21)
        txtDSName.TabIndex = 17
        txtDSName.Text = ""
        '
        'lblName
        '
        lblName.AutoSize = True
        lblName.BackColor = System.Drawing.Color.Transparent
        lblName.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblName.Location = New System.Drawing.Point(22, 110)
        lblName.Name = "lblName"
        lblName.Size = New System.Drawing.Size(63, 16)
        lblName.TabIndex = 18
        lblName.Text = "Nombre:"
        lblName.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(22, 175)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(48, 16)
        Label3.TabIndex = 21
        Label3.Text = "Tabla:"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtTable
        '
        txtTable.Location = New System.Drawing.Point(91, 172)
        txtTable.Name = "txtTable"
        txtTable.Size = New System.Drawing.Size(342, 21)
        txtTable.TabIndex = 20
        txtTable.Text = ""
        '
        'UCDoInsertTable
        '
        Name = "UCDoInsertTable"
        Size = New System.Drawing.Size(515, 569)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDoInsertTable
    Public Sub New(ByRef DoInsertTable As IDoInsertTable, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoInsertTable, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = DoInsertTable
    End Sub
#End Region


    Private Sub UCDoInsertTable_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            txtDSName.Text = CurrentRule.DataSet
            txtTable.Text = CurrentRule.Table

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            If String.IsNullOrEmpty(txtDSName.Text) Or String.IsNullOrEmpty(txtTable.Text) Then
                MessageBox.Show("Debe completar los campos seleccionados", "Atencion", MessageBoxButtons.OK)
            Else
                CurrentRule.DataSet = txtDSName.Text
                CurrentRule.Table = txtTable.Text
            End If

            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.DataSet)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.Table)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
