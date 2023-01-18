

Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MailDocAsocConfig
    Inherits Zamba.AppBlock.ZForm


    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdbSelect = New System.Windows.Forms.RadioButton()
        Me.rdbAll = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtDocIdFilter = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.rdbFilterDocId = New System.Windows.Forms.RadioButton()
        Me.rdbSelAll = New System.Windows.Forms.RadioButton()
        Me.rdbManual = New System.Windows.Forms.RadioButton()
        Me.rdbFilter = New System.Windows.Forms.RadioButton()
        Me.rdbFirst = New System.Windows.Forms.RadioButton()
        Me.PanelIndexs = New System.Windows.Forms.Panel()
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.txtIndexValue = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.cbOperator = New System.Windows.Forms.ComboBox()
        Me.cbIndexs = New System.Windows.Forms.ComboBox()
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.btnCancel = New Zamba.AppBlock.ZButton()
        Me.btnSave = New Zamba.AppBlock.ZButton()
        Me.LstDocTypes = New System.Windows.Forms.ListBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.grpAssociatedName = New System.Windows.Forms.GroupBox()
        Me.chkKeepAssociatedDocName = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.PanelIndexs.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.grpAssociatedName.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.AutoSize = True
        Me.GroupBox1.Controls.Add(Me.rdbSelect)
        Me.GroupBox1.Controls.Add(Me.rdbAll)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 26)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.MaximumSize = New System.Drawing.Size(0, 62)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(251, 62)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Entidades"
        '
        'rdbSelect
        '
        Me.rdbSelect.AutoSize = True
        Me.rdbSelect.Location = New System.Drawing.Point(141, 25)
        Me.rdbSelect.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbSelect.Name = "rdbSelect"
        Me.rdbSelect.Size = New System.Drawing.Size(102, 20)
        Me.rdbSelect.TabIndex = 1
        Me.rdbSelect.Text = "Seleccionar"
        Me.rdbSelect.UseVisualStyleBackColor = True
        '
        'rdbAll
        '
        Me.rdbAll.AutoSize = True
        Me.rdbAll.Checked = True
        Me.rdbAll.Location = New System.Drawing.Point(9, 25)
        Me.rdbAll.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbAll.Name = "rdbAll"
        Me.rdbAll.Size = New System.Drawing.Size(65, 20)
        Me.rdbAll.TabIndex = 0
        Me.rdbAll.TabStop = True
        Me.rdbAll.Text = "Todos"
        Me.rdbAll.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.AutoSize = True
        Me.GroupBox2.Controls.Add(Me.txtDocIdFilter)
        Me.GroupBox2.Controls.Add(Me.rdbFilterDocId)
        Me.GroupBox2.Controls.Add(Me.rdbSelAll)
        Me.GroupBox2.Controls.Add(Me.rdbManual)
        Me.GroupBox2.Controls.Add(Me.rdbFilter)
        Me.GroupBox2.Controls.Add(Me.rdbFirst)
        Me.GroupBox2.Location = New System.Drawing.Point(17, 181)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(280, 143)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Seleccion"
        '
        'txtDocIdFilter
        '
        Me.txtDocIdFilter.Location = New System.Drawing.Point(100, 92)
        Me.txtDocIdFilter.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtDocIdFilter.Name = "txtDocIdFilter"
        Me.txtDocIdFilter.Size = New System.Drawing.Size(168, 25)
        Me.txtDocIdFilter.TabIndex = 5
        Me.txtDocIdFilter.Text = ""
        '
        'rdbFilterDocId
        '
        Me.rdbFilterDocId.AutoSize = True
        Me.rdbFilterDocId.Location = New System.Drawing.Point(11, 92)
        Me.rdbFilterDocId.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbFilterDocId.Name = "rdbFilterDocId"
        Me.rdbFilterDocId.Size = New System.Drawing.Size(89, 20)
        Me.rdbFilterDocId.TabIndex = 4
        Me.rdbFilterDocId.Text = "Filtrar ID:"
        Me.rdbFilterDocId.UseVisualStyleBackColor = True
        '
        'rdbSelAll
        '
        Me.rdbSelAll.AutoSize = True
        Me.rdbSelAll.Location = New System.Drawing.Point(168, 60)
        Me.rdbSelAll.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbSelAll.Name = "rdbSelAll"
        Me.rdbSelAll.Size = New System.Drawing.Size(65, 20)
        Me.rdbSelAll.TabIndex = 3
        Me.rdbSelAll.Text = "Todos"
        Me.rdbSelAll.UseVisualStyleBackColor = True
        '
        'rdbManual
        '
        Me.rdbManual.AutoSize = True
        Me.rdbManual.Location = New System.Drawing.Point(168, 31)
        Me.rdbManual.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbManual.Name = "rdbManual"
        Me.rdbManual.Size = New System.Drawing.Size(72, 20)
        Me.rdbManual.TabIndex = 2
        Me.rdbManual.Text = "Manual"
        Me.rdbManual.UseVisualStyleBackColor = True
        '
        'rdbFilter
        '
        Me.rdbFilter.AutoSize = True
        Me.rdbFilter.Location = New System.Drawing.Point(11, 60)
        Me.rdbFilter.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbFilter.Name = "rdbFilter"
        Me.rdbFilter.Size = New System.Drawing.Size(64, 20)
        Me.rdbFilter.TabIndex = 1
        Me.rdbFilter.Text = "Filtrar"
        Me.rdbFilter.UseVisualStyleBackColor = True
        '
        'rdbFirst
        '
        Me.rdbFirst.AutoSize = True
        Me.rdbFirst.Checked = True
        Me.rdbFirst.Location = New System.Drawing.Point(11, 31)
        Me.rdbFirst.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rdbFirst.Name = "rdbFirst"
        Me.rdbFirst.Size = New System.Drawing.Size(123, 20)
        Me.rdbFirst.TabIndex = 0
        Me.rdbFirst.TabStop = True
        Me.rdbFirst.Text = "Solo el primero"
        Me.rdbFirst.UseVisualStyleBackColor = True
        '
        'PanelIndexs
        '
        Me.PanelIndexs.Controls.Add(Me.Label3)
        Me.PanelIndexs.Controls.Add(Me.txtIndexValue)
        Me.PanelIndexs.Controls.Add(Me.Label2)
        Me.PanelIndexs.Controls.Add(Me.cbOperator)
        Me.PanelIndexs.Controls.Add(Me.cbIndexs)
        Me.PanelIndexs.Controls.Add(Me.Label1)
        Me.PanelIndexs.Location = New System.Drawing.Point(327, 181)
        Me.PanelIndexs.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PanelIndexs.Name = "PanelIndexs"
        Me.PanelIndexs.Size = New System.Drawing.Size(293, 143)
        Me.PanelIndexs.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.FontSize = 9.75!
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(7, 80)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Valor"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIndexValue
        '
        Me.txtIndexValue.Location = New System.Drawing.Point(101, 76)
        Me.txtIndexValue.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtIndexValue.Name = "txtIndexValue"
        Me.txtIndexValue.Size = New System.Drawing.Size(187, 62)
        Me.txtIndexValue.TabIndex = 4
        Me.txtIndexValue.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.FontSize = 9.75!
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(7, 46)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Operador"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbOperator
        '
        Me.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOperator.FormattingEnabled = True
        Me.cbOperator.Location = New System.Drawing.Point(101, 42)
        Me.cbOperator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cbOperator.Name = "cbOperator"
        Me.cbOperator.Size = New System.Drawing.Size(187, 24)
        Me.cbOperator.TabIndex = 2
        '
        'cbIndexs
        '
        Me.cbIndexs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbIndexs.FormattingEnabled = True
        Me.cbIndexs.Location = New System.Drawing.Point(101, 7)
        Me.cbIndexs.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cbIndexs.Name = "cbIndexs"
        Me.cbIndexs.Size = New System.Drawing.Size(187, 24)
        Me.cbIndexs.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.FontSize = 9.75!
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(7, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Atributo"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(327, 334)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 28)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancelar"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(197, 334)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 28)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Guardar"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'LstDocTypes
        '
        Me.LstDocTypes.FormattingEnabled = True
        Me.LstDocTypes.ItemHeight = 16
        Me.LstDocTypes.Location = New System.Drawing.Point(327, 25)
        Me.LstDocTypes.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LstDocTypes.Name = "LstDocTypes"
        Me.LstDocTypes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.LstDocTypes.Size = New System.Drawing.Size(292, 116)
        Me.LstDocTypes.TabIndex = 6
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.grpAssociatedName)
        Me.GroupBox3.Controls.Add(Me.GroupBox1)
        Me.GroupBox3.Controls.Add(Me.LstDocTypes)
        Me.GroupBox3.Controls.Add(Me.GroupBox2)
        Me.GroupBox3.Controls.Add(Me.PanelIndexs)
        Me.GroupBox3.Controls.Add(Me.btnCancel)
        Me.GroupBox3.Controls.Add(Me.btnSave)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(3, 2)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(646, 374)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        '
        'grpAssociatedName
        '
        Me.grpAssociatedName.Controls.Add(Me.chkKeepAssociatedDocName)
        Me.grpAssociatedName.Location = New System.Drawing.Point(17, 95)
        Me.grpAssociatedName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grpAssociatedName.Name = "grpAssociatedName"
        Me.grpAssociatedName.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grpAssociatedName.Size = New System.Drawing.Size(280, 75)
        Me.grpAssociatedName.TabIndex = 7
        Me.grpAssociatedName.TabStop = False
        Me.grpAssociatedName.Text = "Nombre del documento asociado"
        '
        'chkKeepAssociatedDocName
        '
        Me.chkKeepAssociatedDocName.AutoSize = True
        Me.chkKeepAssociatedDocName.Location = New System.Drawing.Point(9, 26)
        Me.chkKeepAssociatedDocName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkKeepAssociatedDocName.Name = "chkKeepAssociatedDocName"
        Me.chkKeepAssociatedDocName.Size = New System.Drawing.Size(258, 36)
        Me.chkKeepAssociatedDocName.TabIndex = 0
        Me.chkKeepAssociatedDocName.Text = "Mantener como nombre de adjunto" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "el nombre del archivo original."
        Me.chkKeepAssociatedDocName.UseVisualStyleBackColor = True
        '
        'MailDocAsocConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(652, 378)
        Me.Controls.Add(Me.GroupBox3)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "MailDocAsocConfig"
        Me.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Text = "Configuracion Mail Documentos Asociados"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.PanelIndexs.ResumeLayout(False)
        Me.PanelIndexs.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.grpAssociatedName.ResumeLayout(False)
        Me.grpAssociatedName.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbSelect As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAll As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbManual As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFilter As System.Windows.Forms.RadioButton
    Friend WithEvents rdbFirst As System.Windows.Forms.RadioButton
    Friend WithEvents PanelIndexs As System.Windows.Forms.Panel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents txtIndexValue As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents cbOperator As System.Windows.Forms.ComboBox
    Friend WithEvents cbIndexs As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents btnCancel As ZButton
    Friend WithEvents btnSave As ZButton
    Friend WithEvents LstDocTypes As System.Windows.Forms.ListBox
    Friend WithEvents rdbSelAll As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents grpAssociatedName As System.Windows.Forms.GroupBox
    Friend WithEvents chkKeepAssociatedDocName As System.Windows.Forms.CheckBox
    Friend WithEvents txtDocIdFilter As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents rdbFilterDocId As System.Windows.Forms.RadioButton
End Class
