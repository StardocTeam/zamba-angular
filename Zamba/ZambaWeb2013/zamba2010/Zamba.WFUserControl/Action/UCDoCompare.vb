''' <summary>
''' Esta regla compara un listado por un atributo de cada elemento y guarda en una variable los elementos que cumplan con ese valor
''' </summary>
''' <history>
''' Marcelo Created 14/09/2009
''' </history>
''' <remarks></remarks>
Public Class UCDoCompare
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
    Friend WithEvents chkUseDocAsoc As System.Windows.Forms.CheckBox
    Friend WithEvents lblVarDocAsoc As ZLabel
    Friend WithEvents cmbAsocDocTypes As ComboBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents txtIdDocAsoc As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents cmbComp As ComboBox
    Friend WithEvents txtValFiltro As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label7 As ZLabel
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents txtValComp As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents txtVar As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents txtVarName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label8 As ZLabel

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents BtnSave As ZButton

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        BtnSave = New ZButton
        chkUseDocAsoc = New System.Windows.Forms.CheckBox
        lblVarDocAsoc = New ZLabel
        cmbAsocDocTypes = New ComboBox
        Label2 = New ZLabel
        txtIdDocAsoc = New Zamba.AppBlock.TextoInteligenteTextBox
        txtVar = New Zamba.AppBlock.TextoInteligenteTextBox
        Label4 = New ZLabel
        txtValComp = New Zamba.AppBlock.TextoInteligenteTextBox
        Label5 = New ZLabel
        Label6 = New ZLabel
        txtValFiltro = New Zamba.AppBlock.TextoInteligenteTextBox
        Label7 = New ZLabel
        cmbComp = New ComboBox
        txtVarName = New Zamba.AppBlock.TextoInteligenteTextBox
        Label8 = New ZLabel
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(txtVarName)
        tbRule.Controls.Add(Label8)
        tbRule.Controls.Add(cmbComp)
        tbRule.Controls.Add(txtValFiltro)
        tbRule.Controls.Add(Label7)
        tbRule.Controls.Add(Label6)
        tbRule.Controls.Add(txtValComp)
        tbRule.Controls.Add(Label5)
        tbRule.Controls.Add(txtVar)
        tbRule.Controls.Add(Label4)
        tbRule.Controls.Add(txtIdDocAsoc)
        tbRule.Controls.Add(Label2)
        tbRule.Controls.Add(cmbAsocDocTypes)
        tbRule.Controls.Add(lblVarDocAsoc)
        tbRule.Controls.Add(chkUseDocAsoc)
        tbRule.Controls.Add(BtnSave)
        tbRule.Size = New System.Drawing.Size(481, 503)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(489, 529)
        '
        'BtnSave
        '
        BtnSave.DialogResult = System.Windows.Forms.DialogResult.None
        BtnSave.Location = New System.Drawing.Point(309, 460)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New System.Drawing.Size(72, 27)
        BtnSave.TabIndex = 3
        BtnSave.Text = "Guardar"
        '
        'chkUseDocAsoc
        '
        chkUseDocAsoc.AutoSize = True
        chkUseDocAsoc.Location = New System.Drawing.Point(10, 10)
        chkUseDocAsoc.Name = "chkUseDocAsoc"
        chkUseDocAsoc.Size = New System.Drawing.Size(159, 17)
        chkUseDocAsoc.TabIndex = 4
        chkUseDocAsoc.Text = "Utilizar documento asociado"
        chkUseDocAsoc.UseVisualStyleBackColor = True
        '
        'lblVarDocAsoc
        '
        lblVarDocAsoc.AutoSize = True
        lblVarDocAsoc.Location = New System.Drawing.Point(10, 40)
        lblVarDocAsoc.Name = "lblVarDocAsoc"
        lblVarDocAsoc.Size = New System.Drawing.Size(137, 13)
        lblVarDocAsoc.TabIndex = 7
        lblVarDocAsoc.Text = "Id del Documento Asociado"
        '
        'cmbAsocDocTypes
        '
        cmbAsocDocTypes.DropDownStyle = ComboBoxStyle.DropDownList
        cmbAsocDocTypes.FormattingEnabled = True
        cmbAsocDocTypes.Location = New System.Drawing.Point(222, 83)
        cmbAsocDocTypes.Name = "cmbAsocDocTypes"
        cmbAsocDocTypes.Size = New System.Drawing.Size(199, 21)
        cmbAsocDocTypes.TabIndex = 8
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(10, 86)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(147, 13)
        Label2.TabIndex = 9
        Label2.Text = "Tipo del Documento Asociado"
        '
        'txtIdDocAsoc
        '
        txtIdDocAsoc.Location = New System.Drawing.Point(222, 40)
        txtIdDocAsoc.Name = "txtIdDocAsoc"
        txtIdDocAsoc.Size = New System.Drawing.Size(199, 21)
        txtIdDocAsoc.TabIndex = 10
        txtIdDocAsoc.Text = ""
        '
        'txtVar
        '
        txtVar.Location = New System.Drawing.Point(222, 133)
        txtVar.Name = "txtVar"
        txtVar.Size = New System.Drawing.Size(199, 21)
        txtVar.TabIndex = 12
        txtVar.Text = ""
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(10, 133)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(45, 13)
        Label4.TabIndex = 11
        Label4.Text = "Variable"
        '
        'txtValComp
        '
        txtValComp.Location = New System.Drawing.Point(222, 172)
        txtValComp.Name = "txtValComp"
        txtValComp.Size = New System.Drawing.Size(199, 21)
        txtValComp.TabIndex = 14
        txtValComp.Text = ""
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(10, 172)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(105, 13)
        Label5.TabIndex = 13
        Label5.Text = "Atributo a Comparar"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(10, 211)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(66, 13)
        Label6.TabIndex = 15
        Label6.Text = "Comparador"
        '
        'txtValFiltro
        '
        txtValFiltro.Location = New System.Drawing.Point(222, 255)
        txtValFiltro.Name = "txtValFiltro"
        txtValFiltro.Size = New System.Drawing.Size(199, 21)
        txtValFiltro.TabIndex = 18
        txtValFiltro.Text = ""
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(10, 255)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(58, 13)
        Label7.TabIndex = 17
        Label7.Text = "Valor Filtro"
        '
        'cmbComp
        '
        cmbComp.DropDownStyle = ComboBoxStyle.DropDownList
        cmbComp.FormattingEnabled = True
        cmbComp.Items.AddRange(New Object() {"Igual", "Distinto", "Mayor", "Mayor Igual", "Menor", "Menor Igual", "Contiene", "Empieza", "Termina"})
        cmbComp.Location = New System.Drawing.Point(222, 211)
        cmbComp.Name = "cmbComp"
        cmbComp.Size = New System.Drawing.Size(199, 21)
        cmbComp.TabIndex = 19
        '
        'txtVarName
        '
        txtVarName.Location = New System.Drawing.Point(222, 296)
        txtVarName.Name = "txtVarName"
        txtVarName.Size = New System.Drawing.Size(199, 21)
        txtVarName.TabIndex = 21
        txtVarName.Text = ""
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(10, 296)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(136, 13)
        Label8.TabIndex = 20
        Label8.Text = "Nombre Variable Resultado"
        '
        'UCDoCompare
        '
        Name = "UCDoCompare"
        Size = New System.Drawing.Size(489, 529)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDoCompare
    Public Sub New(ByRef DoCompare As IDoCompare, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoCompare, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = DoCompare

        Try
            loadValues()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

    ''' <summary>
    ''' Carga los valores a la interfaz
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub loadValues()
        chkUseDocAsoc.Checked = CurrentRule.UseAsocDoc
        txtIdDocAsoc.Text = CurrentRule.IdAsoc
        'cargo todos los entidades 
        Dim ds As DataSet = DocTypesBusiness.GetAllDocTypes()

        If (ds.Tables.Count > 0) Then
            cmbAsocDocTypes.DataSource = ds.Tables(0)
            cmbAsocDocTypes.DisplayMember = "doc_type_name"
            cmbAsocDocTypes.ValueMember = "doc_type_id"
        End If

        If cmbAsocDocTypes.Items.Count > 0 Then
            cmbAsocDocTypes.SelectedValue = CurrentRule.idDocTypeAsoc
        End If

        txtVar.Text = CurrentRule.valueList
        txtValComp.Text = CurrentRule.valueComp
        cmbComp.Text = CurrentRule.Comp
        txtValFiltro.Text = CurrentRule.valueFilter
        txtVarName.Text = CurrentRule.variableName
    End Sub

    ''' <summary>
    ''' Guarda los valores en la Base de Datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            CurrentRule.UseAsocDoc = chkUseDocAsoc.Checked
            CurrentRule.IdAsoc = txtIdDocAsoc.Text
            If cmbAsocDocTypes.SelectedIndex >= 0 Then
                CurrentRule.idDocTypeAsoc = cmbAsocDocTypes.SelectedValue
            End If

            CurrentRule.valueList = txtVar.Text
            CurrentRule.valueComp = txtValComp.Text
            CurrentRule.Comp = cmbComp.Text
            CurrentRule.valueFilter = txtValFiltro.Text
            CurrentRule.variableName = txtVarName.Text

            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.UseAsocDoc, ObjectTypes.None)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.IdAsoc, ObjectTypes.None)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.idDocTypeAsoc, ObjectTypes.DocTypes)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.valueList, ObjectTypes.None)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.valueComp, ObjectTypes.None)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.Comp, ObjectTypes.None)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 6, CurrentRule.valueFilter, ObjectTypes.None)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 7, CurrentRule.variableName, ObjectTypes.None)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class