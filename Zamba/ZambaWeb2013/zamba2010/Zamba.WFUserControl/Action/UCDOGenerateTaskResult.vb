Public Class UCDOGenerateTaskResult
    'Los controles de Reglas de Accion deben heredar de ZRuleControl
    Inherits ZRuleControl

    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents grdIndices As System.Windows.Forms.DataGridView
    Friend WithEvents txtVariable As TextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents chkAddToCurrentWF As System.Windows.Forms.CheckBox
    Friend WithEvents chkContinueWithCurrentTasks As System.Windows.Forms.CheckBox
    Friend WithEvents cmbDocTypes As ComboBox
    Friend WithEvents txtPath As TextBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents chkShowDocument As System.Windows.Forms.CheckBox
    Friend WithEvents RuleContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents chkAutocompleteIndexs As System.Windows.Forms.CheckBox
    Friend WithEvents Index_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nombre As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ValorIndice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents lstautomaticvariables As ListBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents Label6 As Label
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdself As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdmodal As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdnewwindow As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdnewtab As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnBuscar As ZButton
#Region " Código generado por el Diseñador de Windows Forms "

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.Label5 = New Zamba.AppBlock.ZLabel()
        Me.btnBuscar = New Zamba.AppBlock.ZButton()
        Me.cmbDocTypes = New System.Windows.Forms.ComboBox()
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.btnAceptar = New Zamba.AppBlock.ZButton()
        Me.grdIndices = New System.Windows.Forms.DataGridView()
        Me.Index_Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ValorIndice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.txtVariable = New System.Windows.Forms.TextBox()
        Me.chkAddToCurrentWF = New System.Windows.Forms.CheckBox()
        Me.chkContinueWithCurrentTasks = New System.Windows.Forms.CheckBox()
        Me.chkShowDocument = New System.Windows.Forms.CheckBox()
        Me.RuleContainer = New System.Windows.Forms.SplitContainer()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.lstautomaticvariables = New System.Windows.Forms.ListBox()
        Me.chkAutocompleteIndexs = New System.Windows.Forms.CheckBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdself = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdmodal = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdnewwindow = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdnewtab = New Telerik.WinControls.UI.RadRadioButton()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        CType(Me.grdIndices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RuleContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RuleContainer.Panel1.SuspendLayout()
        Me.RuleContainer.Panel2.SuspendLayout()
        Me.RuleContainer.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rdself, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmodal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdnewwindow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdnewtab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.RuleContainer)
        Me.tbRule.Size = New System.Drawing.Size(846, 562)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(854, 591)
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(24, 118)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(432, 23)
        Me.txtPath.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label5.FontSize = 9.75!
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(21, 99)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(122, 16)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Ruta del Archivo:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnBuscar
        '
        Me.btnBuscar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBuscar.ForeColor = System.Drawing.Color.White
        Me.btnBuscar.Location = New System.Drawing.Point(462, 118)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(47, 23)
        Me.btnBuscar.TabIndex = 17
        Me.btnBuscar.Text = "..."
        Me.btnBuscar.UseVisualStyleBackColor = False
        '
        'cmbDocTypes
        '
        Me.cmbDocTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDocTypes.FormattingEnabled = True
        Me.cmbDocTypes.Location = New System.Drawing.Point(203, 171)
        Me.cmbDocTypes.Name = "cmbDocTypes"
        Me.cmbDocTypes.Size = New System.Drawing.Size(253, 24)
        Me.cmbDocTypes.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label1.FontSize = 9.75!
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(21, 179)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Entidad:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ForeColor = System.Drawing.Color.White
        Me.btnAceptar.Location = New System.Drawing.Point(503, 170)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(82, 24)
        Me.btnAceptar.TabIndex = 3
        Me.btnAceptar.Text = "Guardar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'grdIndices
        '
        Me.grdIndices.AllowUserToAddRows = False
        Me.grdIndices.AllowUserToDeleteRows = False
        Me.grdIndices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdIndices.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Index_Id, Me.Nombre, Me.ValorIndice})
        Me.grdIndices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdIndices.Location = New System.Drawing.Point(0, 0)
        Me.grdIndices.Name = "grdIndices"
        Me.grdIndices.Size = New System.Drawing.Size(840, 331)
        Me.grdIndices.TabIndex = 10
        '
        'Index_Id
        '
        Me.Index_Id.DataPropertyName = "Index_Id"
        Me.Index_Id.HeaderText = "ID Atributo"
        Me.Index_Id.Name = "Index_Id"
        '
        'Nombre
        '
        Me.Nombre.DataPropertyName = "Index_Name"
        Me.Nombre.HeaderText = "Nombre Atributo"
        Me.Nombre.Name = "Nombre"
        Me.Nombre.ReadOnly = True
        '
        'ValorIndice
        '
        Me.ValorIndice.DataPropertyName = "ValorIndice"
        Me.ValorIndice.HeaderText = "Valor Atributo"
        Me.ValorIndice.Name = "ValorIndice"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label2.FontSize = 9.75!
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(21, 148)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(176, 16)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Variable Origen de Datos:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVariable
        '
        Me.txtVariable.Location = New System.Drawing.Point(203, 145)
        Me.txtVariable.Name = "txtVariable"
        Me.txtVariable.Size = New System.Drawing.Size(253, 23)
        Me.txtVariable.TabIndex = 12
        '
        'chkAddToCurrentWF
        '
        Me.chkAddToCurrentWF.AutoSize = True
        Me.chkAddToCurrentWF.BackColor = System.Drawing.Color.Transparent
        Me.chkAddToCurrentWF.Location = New System.Drawing.Point(369, 12)
        Me.chkAddToCurrentWF.Name = "chkAddToCurrentWF"
        Me.chkAddToCurrentWF.Size = New System.Drawing.Size(233, 20)
        Me.chkAddToCurrentWF.TabIndex = 13
        Me.chkAddToCurrentWF.Text = "No adjuntar al WorkFlow actual"
        Me.chkAddToCurrentWF.UseVisualStyleBackColor = False
        '
        'chkContinueWithCurrentTasks
        '
        Me.chkContinueWithCurrentTasks.AutoSize = True
        Me.chkContinueWithCurrentTasks.BackColor = System.Drawing.Color.Transparent
        Me.chkContinueWithCurrentTasks.Location = New System.Drawing.Point(24, 12)
        Me.chkContinueWithCurrentTasks.Name = "chkContinueWithCurrentTasks"
        Me.chkContinueWithCurrentTasks.Size = New System.Drawing.Size(339, 20)
        Me.chkContinueWithCurrentTasks.TabIndex = 14
        Me.chkContinueWithCurrentTasks.Text = "Continuar la ejecucion con las tareas originales"
        Me.chkContinueWithCurrentTasks.UseVisualStyleBackColor = False
        '
        'chkShowDocument
        '
        Me.chkShowDocument.AutoSize = True
        Me.chkShowDocument.Location = New System.Drawing.Point(24, 38)
        Me.chkShowDocument.Name = "chkShowDocument"
        Me.chkShowDocument.Size = New System.Drawing.Size(188, 20)
        Me.chkShowDocument.TabIndex = 18
        Me.chkShowDocument.Text = "Abrir la Tarea al Finalizar"
        Me.chkShowDocument.UseVisualStyleBackColor = True
        '
        'RuleContainer
        '
        Me.RuleContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RuleContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.RuleContainer.Location = New System.Drawing.Point(3, 3)
        Me.RuleContainer.Name = "RuleContainer"
        Me.RuleContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'RuleContainer.Panel1
        '
        Me.RuleContainer.Panel1.AutoScroll = True
        Me.RuleContainer.Panel1.BackColor = System.Drawing.Color.White
        Me.RuleContainer.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.RuleContainer.Panel1.Controls.Add(Me.Label6)
        Me.RuleContainer.Panel1.Controls.Add(Me.TextBox1)
        Me.RuleContainer.Panel1.Controls.Add(Me.Label3)
        Me.RuleContainer.Panel1.Controls.Add(Me.lstautomaticvariables)
        Me.RuleContainer.Panel1.Controls.Add(Me.chkAutocompleteIndexs)
        Me.RuleContainer.Panel1.Controls.Add(Me.chkShowDocument)
        Me.RuleContainer.Panel1.Controls.Add(Me.cmbDocTypes)
        Me.RuleContainer.Panel1.Controls.Add(Me.btnBuscar)
        Me.RuleContainer.Panel1.Controls.Add(Me.Label1)
        Me.RuleContainer.Panel1.Controls.Add(Me.txtPath)
        Me.RuleContainer.Panel1.Controls.Add(Me.btnAceptar)
        Me.RuleContainer.Panel1.Controls.Add(Me.Label5)
        Me.RuleContainer.Panel1.Controls.Add(Me.Label2)
        Me.RuleContainer.Panel1.Controls.Add(Me.chkContinueWithCurrentTasks)
        Me.RuleContainer.Panel1.Controls.Add(Me.txtVariable)
        Me.RuleContainer.Panel1.Controls.Add(Me.chkAddToCurrentWF)
        '
        'RuleContainer.Panel2
        '
        Me.RuleContainer.Panel2.Controls.Add(Me.grdIndices)
        Me.RuleContainer.Size = New System.Drawing.Size(840, 556)
        Me.RuleContainer.SplitterDistance = 221
        Me.RuleContainer.TabIndex = 19
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(613, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 16)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "WorkFlow ID"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(706, 10)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 23)
        Me.TextBox1.TabIndex = 29
        Me.TextBox1.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label3.FontSize = 9.75!
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(653, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(153, 16)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Variables Automaticas"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstautomaticvariables
        '
        Me.lstautomaticvariables.BackColor = System.Drawing.Color.White
        Me.lstautomaticvariables.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstautomaticvariables.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lstautomaticvariables.FormattingEnabled = True
        Me.lstautomaticvariables.ItemHeight = 16
        Me.lstautomaticvariables.Location = New System.Drawing.Point(607, 99)
        Me.lstautomaticvariables.Name = "lstautomaticvariables"
        Me.lstautomaticvariables.Size = New System.Drawing.Size(210, 112)
        Me.lstautomaticvariables.TabIndex = 27
        '
        'chkAutocompleteIndexs
        '
        Me.chkAutocompleteIndexs.AutoSize = True
        Me.chkAutocompleteIndexs.Location = New System.Drawing.Point(24, 73)
        Me.chkAutocompleteIndexs.Name = "chkAutocompleteIndexs"
        Me.chkAutocompleteIndexs.Size = New System.Drawing.Size(288, 20)
        Me.chkAutocompleteIndexs.TabIndex = 20
        Me.chkAutocompleteIndexs.Text = "Heredar los atributos de la tarea actual"
        Me.chkAutocompleteIndexs.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rdself)
        Me.RadGroupBox1.Controls.Add(Me.rdmodal)
        Me.RadGroupBox1.Controls.Add(Me.rdnewwindow)
        Me.RadGroupBox1.Controls.Add(Me.rdnewtab)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(218, 33)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(482, 34)
        Me.RadGroupBox1.TabIndex = 32
        '
        'rdself
        '
        Me.rdself.Location = New System.Drawing.Point(301, 6)
        Me.rdself.Name = "rdself"
        Me.rdself.Size = New System.Drawing.Size(98, 18)
        Me.rdself.TabIndex = 3
        Me.rdself.Text = "Misma Ventana"
        '
        'rdmodal
        '
        Me.rdmodal.Location = New System.Drawing.Point(202, 6)
        Me.rdmodal.Name = "rdmodal"
        Me.rdmodal.Size = New System.Drawing.Size(94, 18)
        Me.rdmodal.TabIndex = 2
        Me.rdmodal.Text = "Dialogo Modal"
        '
        'rdnewwindow
        '
        Me.rdnewwindow.Location = New System.Drawing.Point(100, 6)
        Me.rdnewwindow.Name = "rdnewwindow"
        Me.rdnewwindow.Size = New System.Drawing.Size(96, 18)
        Me.rdnewwindow.TabIndex = 1
        Me.rdnewwindow.Text = "Nueva Ventana"
        '
        'rdnewtab
        '
        Me.rdnewtab.Location = New System.Drawing.Point(5, 6)
        Me.rdnewtab.Name = "rdnewtab"
        Me.rdnewtab.Size = New System.Drawing.Size(89, 18)
        Me.rdnewtab.TabIndex = 0
        Me.rdnewtab.Text = "Nueva Solapa"
        '
        'UCDOGenerateTaskResult
        '
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.Name = "UCDOGenerateTaskResult"
        Me.Size = New System.Drawing.Size(854, 591)
        Me.Controls.SetChildIndex(Me.tbctrMain, 0)
        Me.tbRule.ResumeLayout(False)
        Me.tbctrMain.ResumeLayout(False)
        CType(Me.grdIndices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RuleContainer.Panel1.ResumeLayout(False)
        Me.RuleContainer.Panel1.PerformLayout()
        Me.RuleContainer.Panel2.ResumeLayout(False)
        CType(Me.RuleContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RuleContainer.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rdself, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmodal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdnewwindow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdnewtab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim indices As SortedList = New SortedList()

    'El New debe recibir la regla a configurar
    Public Sub New(ByRef CurrentRule As IDOGenerateTaskResult, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()

        chkAddToCurrentWF.Checked = CurrentRule.addCurrentwf
        chkContinueWithCurrentTasks.Checked = MyRule.ContinueWithCurrentTasks
        chkShowDocument.Checked = Not MyRule.DontOpenTaskAfterInsert
        chkAutocompleteIndexs.Checked = MyRule.AutocompleteIndexsInCommon
        TextBox1.Text = MyRule.SpecificWorkflowId
        txtPath.Text = CurrentRule.FilePath

        If MyRule.OpenMode = 0 Then
            rdnewtab.CheckState = CheckState.Checked
        ElseIf MyRule.OpenMode = 1 Then
            rdmodal.CheckState = CheckState.Checked
        ElseIf MyRule.OpenMode = 2 Then
            rdself.CheckState = CheckState.Checked
        ElseIf MyRule.OpenMode = 3 Then
            rdnewwindow.CheckState = CheckState.Checked
        Else
            rdnewtab.CheckState = CheckState.Checked
        End If


        AddAutomaticVariables()

    End Sub

    Public Shadows ReadOnly Property MyRule() As IDOGenerateTaskResult
        Get
            Return DirectCast(Rule, IDOGenerateTaskResult)
        End Get
    End Property

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Dim index As New System.Text.StringBuilder()
        Try
            If Not String.IsNullOrEmpty(cmbDocTypes.Text) Then
                MyRule.docTypeId = Int64.Parse(cmbDocTypes.SelectedValue.ToString())

                index.Append(0)
                index.Append("|")
                index.Append(txtVariable.Text)
                index.Append("//")

                Dim valor As Boolean
                Dim completar As Boolean

                For Each row As DataGridViewRow In grdIndices.Rows
                    'Se guardan las condiciones
                    valor = (Not IsNothing(row.Cells("valorIndice").Value) AndAlso Not IsDBNull(row.Cells("valorIndice").Value))
                    completar = row.Cells("DONOTCOMPLETE").Value

                    'Verifica que se deba completar algún dato del Atributo
                    If (completar And chkAutocompleteIndexs.Checked) Or (chkAutocompleteIndexs.Checked = False) Then

                        'Verifica si debe completar información del valor
                        If valor Then
                            index.Append(row.Cells("Index_Id").Value)
                            index.Append("|")
                            index.Append(row.Cells("valorIndice").Value)
                        Else
                            index.Append(row.Cells("Index_Id").Value)
                            index.Append("|")
                        End If

                        'Verifica si debe completar informacion de 
                        If completar Then
                            index.Append("|")
                            index.Append("[no_completar]")
                        End If

                        index.Append("//")
                    ElseIf (chkAutocompleteIndexs.Checked And completar = False) Then
                        If IsDBNull(row.Cells("valorIndice").Value) = False AndAlso String.IsNullOrEmpty(row.Cells("valorIndice").Value) = False Then
                            If MessageBox.Show("Se ha encontrado el valor manual " & Chr(34) & row.Cells("valorIndice").Value & Chr(34) & " para el indice " & row.Cells("Nombre").Value.trim() & ". ¿Desea quitar este valor manual?", "Atencion", MessageBoxButtons.YesNo) = DialogResult.No Then
                                row.Cells("DONOTCOMPLETE").Value = True

                                'Verifica si debe completar información del valor
                                If valor Then
                                    index.Append(row.Cells("Index_Id").Value)
                                    index.Append("|")
                                    index.Append(row.Cells("valorIndice").Value)
                                Else
                                    index.Append(row.Cells("Index_Id").Value)
                                    index.Append("|")
                                End If

                                'Verifica si debe completar informacion de 
                                index.Append("|")
                                index.Append("[no_completar]")

                                index.Append("//")
                            End If
                        End If
                    End If
                Next

                MyRule.indices = index.ToString()
                MyRule.addCurrentwf = chkAddToCurrentWF.Checked
                MyRule.ContinueWithCurrentTasks = chkContinueWithCurrentTasks.Checked
                MyRule.FilePath = txtPath.Text
                MyRule.DontOpenTaskAfterInsert = Not chkShowDocument.Checked
                MyRule.AutocompleteIndexsInCommon = chkAutocompleteIndexs.Checked
                MyRule.SpecificWorkflowId = TextBox1.Text

                If rdnewtab.CheckState = CheckState.Checked Then
                    MyRule.OpenMode = 0
                ElseIf rdnewwindow.CheckState = CheckState.Checked Then
                    MyRule.OpenMode = 3
                ElseIf rdmodal.CheckState = CheckState.Checked Then
                    MyRule.OpenMode = 1
                ElseIf rdself.CheckState = CheckState.Checked Then
                    MyRule.OpenMode = 2
                Else
                    MyRule.OpenMode = 0
                End If

                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, MyRule.docTypeId)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 1, MyRule.indices)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 2, MyRule.addCurrentwf)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 3, MyRule.ContinueWithCurrentTasks)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 4, MyRule.FilePath)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 5, True)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 6, MyRule.DontOpenTaskAfterInsert)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 7, MyRule.AutocompleteIndexsInCommon)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 8, MyRule.SpecificWorkflowId)
                    WFRulesBusiness.UpdateParamItem(Rule.ID, 9, MyRule.OpenMode)

                    UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")

                    AddAutomaticVariables()
                Else
                    MessageBox.Show("No se han encontrado entidades", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            index = Nothing
        End Try
    End Sub

    Private Sub UCDOGenerateTaskResult_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            'bolini = True
            cmbDocTypes.DisplayMember = "Doc_Type_Name"
            cmbDocTypes.ValueMember = "Doc_Type_Id"
            cmbDocTypes.DataSource = DocTypesBusiness.GetDocTypeNamesAndIds()

            If (MyRule.docTypeId <> 0) Then
                cmbDocTypes.SelectedValue = MyRule.docTypeId
            Else
                cmbDocTypes.SelectedIndex = 0
            End If

            Dim dt As DataTable = IndexsBusiness.GetIndexSchemaAsDataSet(cmbDocTypes.SelectedValue).Tables(0)
            dt.Columns.Remove(dt.Columns("Index_Type"))
            dt.Columns.Remove(dt.Columns("Index_Len"))
            dt.Columns.Remove(dt.Columns("DropDown"))
            dt.Columns.Remove(dt.Columns("IsReferenced"))
            dt.Columns.Remove(dt.Columns("Orden"))
            dt.Columns.Remove(dt.Columns("IndicePadre"))
            If dt.Columns.Contains("IndiceHijo") Then
                dt.Columns.Remove(dt.Columns("IndiceHijo"))
            End If
            If dt.Columns.Contains("DataTableName") Then
                dt.Columns.Remove(dt.Columns("DataTableName"))
            End If

            Dim col As New DataColumn("DONOTCOMPLETE")
            col.DataType = GetType(Boolean)
            col.DefaultValue = False
            col.Caption = "No Autocompletar"
            dt.Columns.Add(col)

            Dim strIndex As String = MyRule.indices.Replace("//", "§")
            Dim value As String
            Dim id As Int64
            Dim strItem As String

            dt.Columns.Add("ValorIndice")
            While Not String.IsNullOrEmpty(strIndex)
                'Obtengo el item (// separa por items y | separa por valor y no completar)

                strItem = strIndex.Split("§")(0)
                id = Int(strItem.Split("|")(0))
                value = strItem.Substring(strItem.IndexOf("|") + 1)

                indices.Add(Int64.Parse(id), value)

                strIndex = strIndex.Remove(0, strIndex.Split("§")(0).Length)
                If strIndex.Length > 0 Then
                    strIndex = strIndex.Remove(0, 1)
                End If
            End While

            If (indices.Count = 1) Then
                id = indices.GetKeyList(0)
                txtVariable.Text = indices(id)
            Else
                txtVariable.Text = indices(Int64.Parse(0))
                For Each row As DataRow In dt.Rows
                    If Not IsNothing(row("Index_Id")) Then
                        If (indices.Contains(Int64.Parse(row("Index_Id")))) Then

                            'Obtiene el valor y el check para no autocompletar (si es que existe)
                            value = indices(Int64.Parse(row("Index_Id")))

                            'Obtiene la posición del último "|" que separa la configuracion del check
                            id = value.LastIndexOf("|")

                            'Verifica que exista
                            If id > -1 Then
                                'Obtiene el autocompletado
                                strItem = value.Substring(id + 1)

                                'Verifica si debe dejar el valor que tiene y no autocompletarlo
                                If String.Compare(strItem, "[no_completar]") = 0 Then
                                    row("ValorIndice") = value.Substring(0, id)
                                    row("DONOTCOMPLETE") = True
                                Else
                                    row("ValorIndice") = value
                                    row("DONOTCOMPLETE") = False
                                End If
                            Else
                                'En caso de no encontrar un valor para no autocompletar
                                row("ValorIndice") = value
                                row("DONOTCOMPLETE") = False
                            End If

                        End If
                    End If
                Next
            End If

            grdIndices.DataSource = dt
            grdIndices.AutoResizeColumns()
            grdIndices.Columns("DONOTCOMPLETE").HeaderText = "No Autocompletar"
            chkAddToCurrentWF.Checked = DirectCast(Rule, Zamba.Core.IDOGenerateTaskResult).addCurrentwf
            chkShowDocument.Checked = Not DirectCast(Rule, Zamba.Core.IDOGenerateTaskResult).DontOpenTaskAfterInsert
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    'Dim bolini As Boolean = False
    Private Sub cmbDocTypes_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbDocTypes.SelectedIndexChanged

        Dim dt As DataTable = IndexsBusiness.GetIndexSchemaAsDataSet(cmbDocTypes.SelectedValue).Tables(0)
        dt.Columns.Add("ValorIndice")
        dt.Columns.Remove(dt.Columns("Index_Type"))
        dt.Columns.Remove(dt.Columns("Index_Len"))
        dt.Columns.Remove(dt.Columns("DropDown"))
        dt.Columns.Remove(dt.Columns("IsReferenced"))
        dt.Columns.Remove(dt.Columns("Orden"))
        dt.Columns.Remove(dt.Columns("IndicePadre"))
        If dt.Columns.Contains("IndiceHijo") Then
            dt.Columns.Remove(dt.Columns("IndiceHijo"))
        End If
        If dt.Columns.Contains("DataTableName") Then
            dt.Columns.Remove(dt.Columns("DataTableName"))
        End If

        Dim col As New DataColumn("DONOTCOMPLETE")
        col.DataType = GetType(Boolean)
        col.DefaultValue = False
        col.Caption = "No Autocompletar"
        dt.Columns.Add(col)

        grdIndices.DataSource = dt
        grdIndices.AutoResizeColumns()
        grdIndices.Columns("DONOTCOMPLETE").HeaderText = "No Autocompletar"
        indices.Clear()
        'txtIndex.Text = String.Empty
    End Sub

    Private Sub chkAutocompleteIndexs_CheckedChanged(sender As System.Object, e As EventArgs) Handles chkAutocompleteIndexs.CheckedChanged
        Try
            If chkAutocompleteIndexs.Checked Then
                For Each row As DataGridViewRow In grdIndices.Rows
                    If Not IsDBNull(row.Cells("valorIndice").Value) AndAlso Not String.IsNullOrEmpty(row.Cells("valorIndice").Value) Then
                        row.Cells("DONOTCOMPLETE").Value = True
                    Else
                        row.Cells("DONOTCOMPLETE").Value = False
                    End If
                Next
            Else
                For Each row As DataGridViewRow In grdIndices.Rows
                    row.Cells("DONOTCOMPLETE").Value = True
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub AddAutomaticVariables()
        Try
            lstautomaticvariables.Items.Clear()
            lstautomaticvariables.Items.Add("zvar(NuevaTarea.Id)")
            lstautomaticvariables.Items.Add("zvar(NuevaTarea.TaskId)")
            lstautomaticvariables.Items.Add("zvar(NuevaTarea.EntityId)")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub chkAddToCurrentWF_CheckedChanged(sender As Object, e As EventArgs) _
     Handles chkAddToCurrentWF.CheckedChanged
        If chkAddToCurrentWF.Checked Then
            Label6.Visible = True
            TextBox1.Visible = True
        Else
            Label6.Visible = False
            TextBox1.Visible = False
        End If


    End Sub

End Class