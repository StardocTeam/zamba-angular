Public Class UCDOShowEditTable
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
    Friend WithEvents lblQuery As System.Windows.Forms.LinkLabel
    Friend WithEvents cboquery As ComboBox
    Friend WithEvents txtVarSource As TextBox
    Friend Shadows WithEvents Label1 As ZLabel
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents txtVarDestiny As TextBox
    Friend WithEvents ChkMultipleSelection As System.Windows.Forms.CheckBox
    Friend WithEvents RdoAllColumns As System.Windows.Forms.RadioButton
    Friend WithEvents RdoEspecificColumns As System.Windows.Forms.RadioButton
    Friend WithEvents TxtSaveColumns As TextBox
    Friend WithEvents TxtSourceColumns As TextBox
    Friend WithEvents RdoSourceColumns As System.Windows.Forms.RadioButton
    Friend WithEvents RdoSourceAllColumns As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lblSourceColumns As ZLabel
    Friend WithEvents lblSaveColumns As ZLabel
    Friend WithEvents grpEspecificColumns As GroupBox
    Friend WithEvents grpSourceColumns As GroupBox
    Friend WithEvents grpAditionalCfg As GroupBox
    Friend WithEvents chkShowCheckColumn As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowDataOnly As System.Windows.Forms.CheckBox
    Friend WithEvents lblCheckedItems As ZLabel
    Friend WithEvents txtCheckedItems As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblCheckItemsColumn As ZLabel
    Friend WithEvents txtCheckItemsColumn As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents txtEditColumns As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnTest As ZButton

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        BtnSave = New ZButton()
        lblQuery = New System.Windows.Forms.LinkLabel()
        cboquery = New ComboBox()
        btnTest = New ZButton()
        txtVarSource = New TextBox()
        Label1 = New ZLabel()
        Label3 = New ZLabel()
        txtVarDestiny = New TextBox()
        ChkMultipleSelection = New System.Windows.Forms.CheckBox()
        RdoAllColumns = New System.Windows.Forms.RadioButton()
        RdoEspecificColumns = New System.Windows.Forms.RadioButton()
        TxtSaveColumns = New TextBox()
        TxtSourceColumns = New TextBox()
        RdoSourceColumns = New System.Windows.Forms.RadioButton()
        RdoSourceAllColumns = New System.Windows.Forms.RadioButton()
        GroupBox1 = New GroupBox()
        txtEditColumns = New Zamba.AppBlock.TextoInteligenteTextBox()
        grpSourceColumns = New GroupBox()
        lblSourceColumns = New ZLabel()
        GroupBox2 = New GroupBox()
        grpEspecificColumns = New GroupBox()
        lblSaveColumns = New ZLabel()
        grpAditionalCfg = New GroupBox()
        lblCheckItemsColumn = New ZLabel()
        txtCheckItemsColumn = New Zamba.AppBlock.TextoInteligenteTextBox()
        lblCheckedItems = New ZLabel()
        txtCheckedItems = New Zamba.AppBlock.TextoInteligenteTextBox()
        chkShowDataOnly = New System.Windows.Forms.CheckBox()
        chkShowCheckColumn = New System.Windows.Forms.CheckBox()
        Label4 = New ZLabel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        GroupBox1.SuspendLayout()
        grpSourceColumns.SuspendLayout()
        GroupBox2.SuspendLayout()
        grpEspecificColumns.SuspendLayout()
        grpAditionalCfg.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(grpAditionalCfg)
        tbRule.Controls.Add(TxtSourceColumns)
        tbRule.Controls.Add(GroupBox2)
        tbRule.Controls.Add(GroupBox1)
        tbRule.Controls.Add(Label3)
        tbRule.Controls.Add(txtVarDestiny)
        tbRule.Controls.Add(Label1)
        tbRule.Controls.Add(txtVarSource)
        tbRule.Controls.Add(btnTest)
        tbRule.Controls.Add(cboquery)
        tbRule.Controls.Add(lblQuery)
        tbRule.Controls.Add(BtnSave)
        tbRule.Size = New System.Drawing.Size(604, 578)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(612, 607)
        '
        'BtnSave
        '
        BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        BtnSave.FlatStyle = FlatStyle.Flat
        BtnSave.ForeColor = System.Drawing.Color.White
        BtnSave.Location = New System.Drawing.Point(463, 510)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New System.Drawing.Size(94, 30)
        BtnSave.TabIndex = 3
        BtnSave.Text = "Guardar"
        BtnSave.UseVisualStyleBackColor = False
        '
        'lblQuery
        '
        lblQuery.AutoSize = True
        lblQuery.BackColor = System.Drawing.Color.Transparent
        lblQuery.Enabled = False
        lblQuery.Location = New System.Drawing.Point(14, 24)
        lblQuery.Name = "lblQuery"
        lblQuery.Size = New System.Drawing.Size(65, 16)
        lblQuery.TabIndex = 6
        lblQuery.TabStop = True
        lblQuery.Text = "Consulta"
        '
        'cboquery
        '
        cboquery.Enabled = False
        cboquery.FormattingEnabled = True
        cboquery.Location = New System.Drawing.Point(86, 21)
        cboquery.Name = "cboquery"
        cboquery.Size = New System.Drawing.Size(370, 24)
        cboquery.TabIndex = 11
        '
        'btnTest
        '
        btnTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnTest.Enabled = False
        btnTest.FlatStyle = FlatStyle.Flat
        btnTest.ForeColor = System.Drawing.Color.White
        btnTest.Location = New System.Drawing.Point(473, 20)
        btnTest.Name = "btnTest"
        btnTest.Size = New System.Drawing.Size(90, 25)
        btnTest.TabIndex = 16
        btnTest.Text = "Test"
        btnTest.UseVisualStyleBackColor = False
        '
        'txtVarSource
        '
        txtVarSource.Location = New System.Drawing.Point(17, 69)
        txtVarSource.Name = "txtVarSource"
        txtVarSource.Size = New System.Drawing.Size(439, 23)
        txtVarSource.TabIndex = 17
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(14, 53)
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
        Label3.Location = New System.Drawing.Point(14, 494)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(296, 16)
        Label3.TabIndex = 21
        Label3.Text = "Nombre de Variable de Destino de los Datos"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtVarDestiny
        '
        txtVarDestiny.Location = New System.Drawing.Point(17, 510)
        txtVarDestiny.Name = "txtVarDestiny"
        txtVarDestiny.Size = New System.Drawing.Size(224, 23)
        txtVarDestiny.TabIndex = 20
        '
        'ChkMultipleSelection
        '
        ChkMultipleSelection.AutoSize = True
        ChkMultipleSelection.Location = New System.Drawing.Point(11, 20)
        ChkMultipleSelection.Name = "ChkMultipleSelection"
        ChkMultipleSelection.Size = New System.Drawing.Size(145, 20)
        ChkMultipleSelection.TabIndex = 25
        ChkMultipleSelection.Text = "Selección multiple"
        ChkMultipleSelection.UseVisualStyleBackColor = True
        '
        'RdoAllColumns
        '
        RdoAllColumns.AutoSize = True
        RdoAllColumns.Location = New System.Drawing.Point(11, 20)
        RdoAllColumns.Name = "RdoAllColumns"
        RdoAllColumns.Size = New System.Drawing.Size(155, 20)
        RdoAllColumns.TabIndex = 26
        RdoAllColumns.TabStop = True
        RdoAllColumns.Text = "Todas las Columnas"
        RdoAllColumns.UseVisualStyleBackColor = True
        '
        'RdoEspecificColumns
        '
        RdoEspecificColumns.AutoSize = True
        RdoEspecificColumns.Location = New System.Drawing.Point(11, 47)
        RdoEspecificColumns.Name = "RdoEspecificColumns"
        RdoEspecificColumns.Size = New System.Drawing.Size(166, 20)
        RdoEspecificColumns.TabIndex = 27
        RdoEspecificColumns.TabStop = True
        RdoEspecificColumns.Text = "Columnas especificas"
        RdoEspecificColumns.UseVisualStyleBackColor = True
        '
        'TxtSaveColumns
        '
        TxtSaveColumns.Location = New System.Drawing.Point(371, 10)
        TxtSaveColumns.Name = "TxtSaveColumns"
        TxtSaveColumns.Size = New System.Drawing.Size(129, 23)
        TxtSaveColumns.TabIndex = 28
        '
        'TxtSourceColumns
        '
        TxtSourceColumns.Location = New System.Drawing.Point(401, 156)
        TxtSourceColumns.Name = "TxtSourceColumns"
        TxtSourceColumns.Size = New System.Drawing.Size(129, 23)
        TxtSourceColumns.TabIndex = 31
        '
        'RdoSourceColumns
        '
        RdoSourceColumns.AutoSize = True
        RdoSourceColumns.Location = New System.Drawing.Point(11, 46)
        RdoSourceColumns.Name = "RdoSourceColumns"
        RdoSourceColumns.Size = New System.Drawing.Size(166, 20)
        RdoSourceColumns.TabIndex = 30
        RdoSourceColumns.Text = "Columnas especificas"
        RdoSourceColumns.UseVisualStyleBackColor = True
        '
        'RdoSourceAllColumns
        '
        RdoSourceAllColumns.AutoSize = True
        RdoSourceAllColumns.Checked = True
        RdoSourceAllColumns.Location = New System.Drawing.Point(11, 20)
        RdoSourceAllColumns.Name = "RdoSourceAllColumns"
        RdoSourceAllColumns.Size = New System.Drawing.Size(155, 20)
        RdoSourceAllColumns.TabIndex = 29
        RdoSourceAllColumns.TabStop = True
        RdoSourceAllColumns.Text = "Todas las Columnas"
        RdoSourceAllColumns.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        GroupBox1.BackColor = System.Drawing.Color.Transparent
        GroupBox1.Controls.Add(txtEditColumns)
        GroupBox1.Controls.Add(grpSourceColumns)
        GroupBox1.Controls.Add(RdoSourceColumns)
        GroupBox1.Controls.Add(RdoSourceAllColumns)
        GroupBox1.Location = New System.Drawing.Point(6, 96)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(557, 126)
        GroupBox1.TabIndex = 32
        GroupBox1.TabStop = False
        GroupBox1.Text = "Columnas a Mostrar"
        GroupBox1.Controls.SetChildIndex(RdoSourceAllColumns, 0)
        GroupBox1.Controls.SetChildIndex(RdoSourceColumns, 0)
        GroupBox1.Controls.SetChildIndex(grpSourceColumns, 0)
        GroupBox1.Controls.SetChildIndex(txtEditColumns, 0)
        '
        'txtEditColumns
        '
        txtEditColumns.Location = New System.Drawing.Point(261, 97)
        txtEditColumns.MaxLength = 4000
        txtEditColumns.Name = "txtEditColumns"
        txtEditColumns.Size = New System.Drawing.Size(263, 21)
        txtEditColumns.TabIndex = 39
        txtEditColumns.Text = ""
        '
        'grpSourceColumns
        '
        grpSourceColumns.Controls.Add(lblSourceColumns)
        grpSourceColumns.Location = New System.Drawing.Point(27, 46)
        grpSourceColumns.Name = "grpSourceColumns"
        grpSourceColumns.Size = New System.Drawing.Size(524, 45)
        grpSourceColumns.TabIndex = 35
        grpSourceColumns.TabStop = False
        grpSourceColumns.Text = "Columnas especificas"
        '
        'lblSourceColumns
        '
        lblSourceColumns.AutoSize = True
        lblSourceColumns.BackColor = System.Drawing.Color.Transparent
        lblSourceColumns.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblSourceColumns.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblSourceColumns.Location = New System.Drawing.Point(6, 17)
        lblSourceColumns.Name = "lblSourceColumns"
        lblSourceColumns.Size = New System.Drawing.Size(356, 16)
        lblSourceColumns.TabIndex = 34
        lblSourceColumns.Text = "Ingresar el numero de columna separados por comas"
        lblSourceColumns.TextAlign = ContentAlignment.MiddleLeft
        '
        'GroupBox2
        '
        GroupBox2.BackColor = System.Drawing.Color.Transparent
        GroupBox2.Controls.Add(grpEspecificColumns)
        GroupBox2.Controls.Add(RdoEspecificColumns)
        GroupBox2.Controls.Add(RdoAllColumns)
        GroupBox2.Location = New System.Drawing.Point(3, 228)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New System.Drawing.Size(560, 100)
        GroupBox2.TabIndex = 33
        GroupBox2.TabStop = False
        GroupBox2.Text = "Columnas a Guardar"
        '
        'grpEspecificColumns
        '
        grpEspecificColumns.Controls.Add(lblSaveColumns)
        grpEspecificColumns.Controls.Add(TxtSaveColumns)
        grpEspecificColumns.Location = New System.Drawing.Point(27, 47)
        grpEspecificColumns.Name = "grpEspecificColumns"
        grpEspecificColumns.Size = New System.Drawing.Size(527, 45)
        grpEspecificColumns.TabIndex = 36
        grpEspecificColumns.TabStop = False
        grpEspecificColumns.Text = "Columnas especificas"
        '
        'lblSaveColumns
        '
        lblSaveColumns.AutoSize = True
        lblSaveColumns.BackColor = System.Drawing.Color.Transparent
        lblSaveColumns.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblSaveColumns.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblSaveColumns.Location = New System.Drawing.Point(6, 17)
        lblSaveColumns.Name = "lblSaveColumns"
        lblSaveColumns.Size = New System.Drawing.Size(356, 16)
        lblSaveColumns.TabIndex = 35
        lblSaveColumns.Text = "Ingresar el numero de columna separados por comas"
        lblSaveColumns.TextAlign = ContentAlignment.MiddleLeft
        '
        'grpAditionalCfg
        '
        grpAditionalCfg.BackColor = System.Drawing.Color.Transparent
        grpAditionalCfg.Controls.Add(lblCheckItemsColumn)
        grpAditionalCfg.Controls.Add(txtCheckItemsColumn)
        grpAditionalCfg.Controls.Add(lblCheckedItems)
        grpAditionalCfg.Controls.Add(txtCheckedItems)
        grpAditionalCfg.Controls.Add(chkShowDataOnly)
        grpAditionalCfg.Controls.Add(chkShowCheckColumn)
        grpAditionalCfg.Controls.Add(ChkMultipleSelection)
        grpAditionalCfg.Location = New System.Drawing.Point(3, 334)
        grpAditionalCfg.Name = "grpAditionalCfg"
        grpAditionalCfg.Size = New System.Drawing.Size(560, 147)
        grpAditionalCfg.TabIndex = 34
        grpAditionalCfg.TabStop = False
        grpAditionalCfg.Text = "Configuración adicional"
        '
        'lblCheckItemsColumn
        '
        lblCheckItemsColumn.AutoSize = True
        lblCheckItemsColumn.BackColor = System.Drawing.Color.Transparent
        lblCheckItemsColumn.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblCheckItemsColumn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblCheckItemsColumn.Location = New System.Drawing.Point(8, 121)
        lblCheckItemsColumn.Name = "lblCheckItemsColumn"
        lblCheckItemsColumn.Size = New System.Drawing.Size(324, 16)
        lblCheckItemsColumn.TabIndex = 38
        lblCheckItemsColumn.Text = "Numero de columna por el cual se va comparar:"
        lblCheckItemsColumn.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtCheckItemsColumn
        '
        txtCheckItemsColumn.Location = New System.Drawing.Point(329, 118)
        txtCheckItemsColumn.MaxLength = 4000
        txtCheckItemsColumn.Name = "txtCheckItemsColumn"
        txtCheckItemsColumn.Size = New System.Drawing.Size(225, 21)
        txtCheckItemsColumn.TabIndex = 37
        txtCheckItemsColumn.Text = ""
        '
        'lblCheckedItems
        '
        lblCheckedItems.AutoSize = True
        lblCheckedItems.BackColor = System.Drawing.Color.Transparent
        lblCheckedItems.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblCheckedItems.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblCheckedItems.Location = New System.Drawing.Point(8, 95)
        lblCheckedItems.Name = "lblCheckedItems"
        lblCheckedItems.Size = New System.Drawing.Size(122, 16)
        lblCheckedItems.TabIndex = 36
        lblCheckedItems.Text = "Seleccion previa:"
        lblCheckedItems.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtCheckedItems
        '
        txtCheckedItems.Location = New System.Drawing.Point(136, 91)
        txtCheckedItems.MaxLength = 4000
        txtCheckedItems.Name = "txtCheckedItems"
        txtCheckedItems.Size = New System.Drawing.Size(418, 21)
        txtCheckedItems.TabIndex = 35
        txtCheckedItems.Text = ""
        '
        'chkShowDataOnly
        '
        chkShowDataOnly.AutoSize = True
        chkShowDataOnly.Location = New System.Drawing.Point(11, 44)
        chkShowDataOnly.Name = "chkShowDataOnly"
        chkShowDataOnly.Size = New System.Drawing.Size(223, 20)
        chkShowDataOnly.TabIndex = 27
        chkShowDataOnly.Text = "Mostrar unicamente los datos"
        chkShowDataOnly.UseVisualStyleBackColor = True
        '
        'chkShowCheckColumn
        '
        chkShowCheckColumn.AutoSize = True
        chkShowCheckColumn.Location = New System.Drawing.Point(11, 67)
        chkShowCheckColumn.Name = "chkShowCheckColumn"
        chkShowCheckColumn.Size = New System.Drawing.Size(307, 20)
        chkShowCheckColumn.TabIndex = 26
        chkShowCheckColumn.Text = "Agregar columna de selección de registros"
        chkShowCheckColumn.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label4.Location = New System.Drawing.Point(6, 100)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(249, 13)
        Label4.TabIndex = 40
        Label4.Text = "Nombres de Columnas Edicion separadas por coma"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        '
        'UCDOShowEditTable
        '
        Name = "UCDOShowEditTable"
        Size = New System.Drawing.Size(612, 607)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        grpSourceColumns.ResumeLayout(False)
        grpSourceColumns.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        grpEspecificColumns.ResumeLayout(False)
        grpEspecificColumns.PerformLayout()
        grpAditionalCfg.ResumeLayout(False)
        grpAditionalCfg.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDOShowEditTable
    Public Sub New(ByRef DOShowEditTable As IDOShowEditTable, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DOShowEditTable, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = DOShowEditTable
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
            cboquery.Text = CurrentRule.SQLSelectId
            txtVarSource.Text = CurrentRule.VarSource
            If String.IsNullOrEmpty(CurrentRule.ShowColumns) Then
                RdoSourceAllColumns.Checked = True
                RdoSourceColumns.Checked = False
                TxtSourceColumns.Text = String.Empty
                TxtSourceColumns.Enabled = False
                lblSourceColumns.Enabled = False
            Else
                RdoSourceAllColumns.Checked = False
                RdoSourceColumns.Checked = True
                TxtSourceColumns.Text = CurrentRule.ShowColumns
                TxtSourceColumns.Enabled = True
                lblSourceColumns.Enabled = True
            End If

            ChkMultipleSelection.Checked = CurrentRule.SelectMultiRow
            If String.IsNullOrEmpty(CurrentRule.GetSelectedCols) Then
                RdoAllColumns.Checked = True
                RdoEspecificColumns.Checked = False
                TxtSaveColumns.Text = String.Empty
                TxtSaveColumns.Enabled = False
                lblSaveColumns.Enabled = False
            Else
                RdoAllColumns.Checked = False
                RdoEspecificColumns.Checked = True
                TxtSaveColumns.Text = CurrentRule.GetSelectedCols
                TxtSaveColumns.Enabled = True
                lblSaveColumns.Enabled = True
            End If
            txtVarDestiny.Text = CurrentRule.VarDestiny
            chkShowCheckColumn.Checked = CurrentRule.ShowCheckColumn
            chkShowDataOnly.Checked = CurrentRule.ShowDataOnly
            txtCheckedItems.Text = CurrentRule.CheckedItems
            txtCheckItemsColumn.Text = CurrentRule.CheckedItemsColumn
            txtEditColumns.Text = CurrentRule.EditColumns
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub btnTest_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTest.Click
        Try
            'ToDo: Falta implementación
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            If String.IsNullOrEmpty(txtCheckedItems.Text) = False And String.IsNullOrEmpty(txtCheckItemsColumn.Text) Then
                MessageBox.Show("Debe completar la columna por la cual se compararan los items seleccionados", "Atencion", MessageBoxButtons.OK)
            Else
                If String.IsNullOrEmpty(cboquery.Text) Then
                    CurrentRule.SQLSelectId = 0
                Else
                    CurrentRule.SQLSelectId = cboquery.Text
                End If

                CurrentRule.VarSource = txtVarSource.Text

                If RdoSourceAllColumns.Checked Then
                    CurrentRule.ShowColumns = String.Empty
                Else
                    CurrentRule.ShowColumns = TxtSourceColumns.Text
                End If

                If RdoAllColumns.Checked Then
                    CurrentRule.GetSelectedCols = String.Empty
                Else
                    CurrentRule.GetSelectedCols = TxtSaveColumns.Text
                End If

                CurrentRule.SelectMultiRow = ChkMultipleSelection.Checked
                CurrentRule.ShowCheckColumn = chkShowCheckColumn.Checked
                CurrentRule.VarDestiny = txtVarDestiny.Text
                CurrentRule.ShowDataOnly = chkShowDataOnly.Checked
                CurrentRule.CheckedItems = txtCheckedItems.Text
                CurrentRule.CheckedItemsColumn = txtCheckItemsColumn.Text
                CurrentRule.EditColumns = txtEditColumns.Text

                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.SQLSelectId)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.VarSource)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.ShowColumns)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.SelectMultiRow)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.GetSelectedCols)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.VarDestiny)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 6, CurrentRule.ShowCheckColumn)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 7, CurrentRule.ShowDataOnly)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 8, CurrentRule.CheckedItems)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 9, CurrentRule.CheckedItemsColumn)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 10, CurrentRule.EditColumns)
                UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub RdoSourceColumns_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles RdoSourceColumns.CheckedChanged
        lblSourceColumns.Enabled = RdoSourceColumns.Checked
        TxtSourceColumns.Enabled = RdoSourceColumns.Checked
    End Sub

    Private Sub RdoEspecificColumns_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles RdoEspecificColumns.CheckedChanged
        lblSaveColumns.Enabled = RdoEspecificColumns.Checked
        TxtSaveColumns.Enabled = RdoEspecificColumns.Checked
    End Sub

    Private Sub chkShowCheckColumn_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkShowCheckColumn.CheckedChanged
        txtCheckedItems.Visible = chkShowCheckColumn.Checked
        lblCheckedItems.Visible = chkShowCheckColumn.Checked
        txtCheckItemsColumn.Visible = chkShowCheckColumn.Checked
        lblCheckItemsColumn.Visible = chkShowCheckColumn.Checked
    End Sub
End Class
