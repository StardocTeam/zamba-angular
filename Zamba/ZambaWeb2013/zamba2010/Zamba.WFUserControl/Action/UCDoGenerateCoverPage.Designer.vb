<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCDoGenerateCoverPage
    Inherits ZRuleControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Overloads Sub InitializeComponent()
        Me.cboTiposDeDocumento = New System.Windows.Forms.ComboBox()
        Me.chkprintindexs = New System.Windows.Forms.CheckBox()
        Me.txtnote = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.BtnSave = New Zamba.AppBlock.ZButton()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.Label4 = New Zamba.AppBlock.ZLabel()
        Me.Label5 = New Zamba.AppBlock.ZLabel()
        Me.chkSetPrinter = New System.Windows.Forms.CheckBox()
        Me.Label6 = New Zamba.AppBlock.ZLabel()
        Me.txtCopies = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.chkContinueWithGeneratedDocument = New System.Windows.Forms.CheckBox()
        Me.chkDontOpenTaskAfterInsert = New System.Windows.Forms.CheckBox()
        Me.chkUseTemplate = New System.Windows.Forms.CheckBox()
        Me.txtTemplatePath = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblTemplatePath = New Zamba.AppBlock.ZLabel()
        Me.btnBrowse = New Zamba.AppBlock.ZButton()
        Me.chkUseCurrentTask = New System.Windows.Forms.CheckBox()
        Me.ZLabel1 = New Zamba.AppBlock.ZLabel()
        Me.txtCopiesCount = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.ZLabel2 = New Zamba.AppBlock.ZLabel()
        Me.txtTemplateHeigth = New System.Windows.Forms.TextBox()
        Me.txtTemplateWidth = New System.Windows.Forms.TextBox()
        Me.lblTemplateWidth = New Zamba.AppBlock.ZLabel()
        Me.lblTemplateHeigth = New Zamba.AppBlock.ZLabel()
        Me.lblPageSize = New Zamba.AppBlock.ZLabel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbHabilitation
        '
        Me.tbHabilitation.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbConfiguration
        '
        Me.tbConfiguration.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbAlerts
        '
        Me.tbAlerts.Margin = New System.Windows.Forms.Padding(4)
        Me.tbAlerts.Padding = New System.Windows.Forms.Padding(4)
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.lblPageSize)
        Me.tbRule.Controls.Add(Me.lblTemplateHeigth)
        Me.tbRule.Controls.Add(Me.lblTemplateWidth)
        Me.tbRule.Controls.Add(Me.txtTemplateWidth)
        Me.tbRule.Controls.Add(Me.txtTemplateHeigth)
        Me.tbRule.Controls.Add(Me.ZLabel2)
        Me.tbRule.Controls.Add(Me.txtCopiesCount)
        Me.tbRule.Controls.Add(Me.ZLabel1)
        Me.tbRule.Controls.Add(Me.chkUseCurrentTask)
        Me.tbRule.Controls.Add(Me.btnBrowse)
        Me.tbRule.Controls.Add(Me.lblTemplatePath)
        Me.tbRule.Controls.Add(Me.txtTemplatePath)
        Me.tbRule.Controls.Add(Me.chkUseTemplate)
        Me.tbRule.Controls.Add(Me.chkDontOpenTaskAfterInsert)
        Me.tbRule.Controls.Add(Me.chkContinueWithGeneratedDocument)
        Me.tbRule.Controls.Add(Me.txtCopies)
        Me.tbRule.Controls.Add(Me.Label6)
        Me.tbRule.Controls.Add(Me.chkSetPrinter)
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.BtnSave)
        Me.tbRule.Controls.Add(Me.txtnote)
        Me.tbRule.Controls.Add(Me.chkprintindexs)
        Me.tbRule.Controls.Add(Me.cboTiposDeDocumento)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(824, 781)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(832, 810)
        '
        'cboTiposDeDocumento
        '
        Me.cboTiposDeDocumento.FormattingEnabled = True
        Me.cboTiposDeDocumento.Location = New System.Drawing.Point(55, 35)
        Me.cboTiposDeDocumento.Margin = New System.Windows.Forms.Padding(4)
        Me.cboTiposDeDocumento.Name = "cboTiposDeDocumento"
        Me.cboTiposDeDocumento.Size = New System.Drawing.Size(424, 24)
        Me.cboTiposDeDocumento.TabIndex = 0
        '
        'chkprintindexs
        '
        Me.chkprintindexs.AutoSize = True
        Me.chkprintindexs.Location = New System.Drawing.Point(55, 82)
        Me.chkprintindexs.Margin = New System.Windows.Forms.Padding(4)
        Me.chkprintindexs.Name = "chkprintindexs"
        Me.chkprintindexs.Size = New System.Drawing.Size(424, 20)
        Me.chkprintindexs.TabIndex = 1
        Me.chkprintindexs.Text = "Imprimir atributos en blanco y códigos de barra de atributos"
        Me.chkprintindexs.UseVisualStyleBackColor = True
        '
        'txtnote
        '
        Me.txtnote.Location = New System.Drawing.Point(55, 424)
        Me.txtnote.Margin = New System.Windows.Forms.Padding(4)
        Me.txtnote.Name = "txtnote"
        Me.txtnote.Size = New System.Drawing.Size(588, 60)
        Me.txtnote.TabIndex = 2
        Me.txtnote.Text = ""
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.ForeColor = System.Drawing.Color.White
        Me.BtnSave.Location = New System.Drawing.Point(503, 497)
        Me.BtnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(140, 28)
        Me.BtnSave.TabIndex = 3
        Me.BtnSave.Text = "Guardar"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label2.FontSize = 9.75!
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(44, 32)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(533, 55)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Seleccione la entidad para generar la carátula, los atributos del nuevo documento" &
    " se heredaran de la tarea actual, asignando solamente los atributos en común par" &
    "a ambos entidades."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label4.FontSize = 9.75!
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(52, 133)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 16)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Entidad"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label5.FontSize = 9.75!
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(52, 397)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 16)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Notas:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkSetPrinter
        '
        Me.chkSetPrinter.AutoSize = True
        Me.chkSetPrinter.Location = New System.Drawing.Point(55, 110)
        Me.chkSetPrinter.Margin = New System.Windows.Forms.Padding(4)
        Me.chkSetPrinter.Name = "chkSetPrinter"
        Me.chkSetPrinter.Size = New System.Drawing.Size(184, 20)
        Me.chkSetPrinter.TabIndex = 7
        Me.chkSetPrinter.Text = "Permitir elegir impresora"
        Me.chkSetPrinter.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label6.FontSize = 9.75!
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(52, 367)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(139, 16)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Numero de replicas:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCopies
        '
        Me.txtCopies.Location = New System.Drawing.Point(199, 364)
        Me.txtCopies.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCopies.Name = "txtCopies"
        Me.txtCopies.Size = New System.Drawing.Size(51, 23)
        Me.txtCopies.TabIndex = 9
        Me.txtCopies.Text = ""
        '
        'chkContinueWithGeneratedDocument
        '
        Me.chkContinueWithGeneratedDocument.AutoSize = True
        Me.chkContinueWithGeneratedDocument.Location = New System.Drawing.Point(55, 138)
        Me.chkContinueWithGeneratedDocument.Margin = New System.Windows.Forms.Padding(4)
        Me.chkContinueWithGeneratedDocument.Name = "chkContinueWithGeneratedDocument"
        Me.chkContinueWithGeneratedDocument.Size = New System.Drawing.Size(325, 20)
        Me.chkContinueWithGeneratedDocument.TabIndex = 10
        Me.chkContinueWithGeneratedDocument.Text = "Continuar la ejecución con la tarea generada"
        Me.chkContinueWithGeneratedDocument.UseVisualStyleBackColor = True
        '
        'chkDontOpenTaskAfterInsert
        '
        Me.chkDontOpenTaskAfterInsert.AutoSize = True
        Me.chkDontOpenTaskAfterInsert.Location = New System.Drawing.Point(55, 166)
        Me.chkDontOpenTaskAfterInsert.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDontOpenTaskAfterInsert.Name = "chkDontOpenTaskAfterInsert"
        Me.chkDontOpenTaskAfterInsert.Size = New System.Drawing.Size(480, 20)
        Me.chkDontOpenTaskAfterInsert.TabIndex = 20
        Me.chkDontOpenTaskAfterInsert.Text = "No intentar abrir la tarea al finalizar la insercion del código de barras"
        Me.chkDontOpenTaskAfterInsert.UseVisualStyleBackColor = True
        '
        'chkUseTemplate
        '
        Me.chkUseTemplate.AutoSize = True
        Me.chkUseTemplate.Location = New System.Drawing.Point(55, 212)
        Me.chkUseTemplate.Margin = New System.Windows.Forms.Padding(4)
        Me.chkUseTemplate.Name = "chkUseTemplate"
        Me.chkUseTemplate.Size = New System.Drawing.Size(111, 20)
        Me.chkUseTemplate.TabIndex = 21
        Me.chkUseTemplate.Text = "Usar plantilla"
        Me.chkUseTemplate.UseVisualStyleBackColor = True
        '
        'txtTemplatePath
        '
        Me.txtTemplatePath.Location = New System.Drawing.Point(158, 240)
        Me.txtTemplatePath.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTemplatePath.Name = "txtTemplatePath"
        Me.txtTemplatePath.Size = New System.Drawing.Size(377, 23)
        Me.txtTemplatePath.TabIndex = 22
        Me.txtTemplatePath.Text = ""
        '
        'lblTemplatePath
        '
        Me.lblTemplatePath.AutoSize = True
        Me.lblTemplatePath.BackColor = System.Drawing.Color.Transparent
        Me.lblTemplatePath.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblTemplatePath.FontSize = 9.75!
        Me.lblTemplatePath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblTemplatePath.Location = New System.Drawing.Point(52, 243)
        Me.lblTemplatePath.Name = "lblTemplatePath"
        Me.lblTemplatePath.Size = New System.Drawing.Size(99, 16)
        Me.lblTemplatePath.TabIndex = 23
        Me.lblTemplatePath.Text = "Ruta plantilla:"
        Me.lblTemplatePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Verdana", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.ForeColor = System.Drawing.Color.White
        Me.btnBrowse.Location = New System.Drawing.Point(543, 240)
        Me.btnBrowse.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(100, 23)
        Me.btnBrowse.TabIndex = 24
        Me.btnBrowse.Text = "Buscar"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'chkUseCurrentTask
        '
        Me.chkUseCurrentTask.AutoSize = True
        Me.chkUseCurrentTask.Location = New System.Drawing.Point(499, 37)
        Me.chkUseCurrentTask.Name = "chkUseCurrentTask"
        Me.chkUseCurrentTask.Size = New System.Drawing.Size(144, 20)
        Me.chkUseCurrentTask.TabIndex = 25
        Me.chkUseCurrentTask.Text = "Usar Tarea Actual"
        Me.chkUseCurrentTask.UseVisualStyleBackColor = True
        '
        'ZLabel1
        '
        Me.ZLabel1.AutoSize = True
        Me.ZLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel1.FontSize = 9.75!
        Me.ZLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel1.Location = New System.Drawing.Point(292, 367)
        Me.ZLabel1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ZLabel1.Name = "ZLabel1"
        Me.ZLabel1.Size = New System.Drawing.Size(167, 16)
        Me.ZLabel1.TabIndex = 26
        Me.ZLabel1.Text = "Numero de Impresiones:"
        Me.ZLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCopiesCount
        '
        Me.txtCopiesCount.Location = New System.Drawing.Point(466, 364)
        Me.txtCopiesCount.Multiline = False
        Me.txtCopiesCount.Name = "txtCopiesCount"
        Me.txtCopiesCount.Size = New System.Drawing.Size(177, 23)
        Me.txtCopiesCount.TabIndex = 63
        Me.txtCopiesCount.Text = ""
        '
        'ZLabel2
        '
        Me.ZLabel2.AutoSize = True
        Me.ZLabel2.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel2.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel2.FontSize = 9.75!
        Me.ZLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel2.Location = New System.Drawing.Point(52, 15)
        Me.ZLabel2.Name = "ZLabel2"
        Me.ZLabel2.Size = New System.Drawing.Size(144, 16)
        Me.ZLabel2.TabIndex = 64
        Me.ZLabel2.Text = "Seleccionar Entidad:"
        Me.ZLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTemplateHeigth
        '
        Me.txtTemplateHeigth.Enabled = False
        Me.txtTemplateHeigth.Location = New System.Drawing.Point(243, 305)
        Me.txtTemplateHeigth.Name = "txtTemplateHeigth"
        Me.txtTemplateHeigth.Size = New System.Drawing.Size(52, 23)
        Me.txtTemplateHeigth.TabIndex = 65
        '
        'txtTemplateWidth
        '
        Me.txtTemplateWidth.Enabled = False
        Me.txtTemplateWidth.Location = New System.Drawing.Point(114, 305)
        Me.txtTemplateWidth.Name = "txtTemplateWidth"
        Me.txtTemplateWidth.Size = New System.Drawing.Size(52, 23)
        Me.txtTemplateWidth.TabIndex = 65
        '
        'lblTemplateWidth
        '
        Me.lblTemplateWidth.AutoSize = True
        Me.lblTemplateWidth.BackColor = System.Drawing.Color.Transparent
        Me.lblTemplateWidth.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblTemplateWidth.FontSize = 9.75!
        Me.lblTemplateWidth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblTemplateWidth.Location = New System.Drawing.Point(52, 308)
        Me.lblTemplateWidth.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTemplateWidth.Name = "lblTemplateWidth"
        Me.lblTemplateWidth.Size = New System.Drawing.Size(55, 16)
        Me.lblTemplateWidth.TabIndex = 66
        Me.lblTemplateWidth.Text = "Ancho:"
        Me.lblTemplateWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTemplateHeigth
        '
        Me.lblTemplateHeigth.AutoSize = True
        Me.lblTemplateHeigth.BackColor = System.Drawing.Color.Transparent
        Me.lblTemplateHeigth.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblTemplateHeigth.FontSize = 9.75!
        Me.lblTemplateHeigth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblTemplateHeigth.Location = New System.Drawing.Point(196, 308)
        Me.lblTemplateHeigth.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTemplateHeigth.Name = "lblTemplateHeigth"
        Me.lblTemplateHeigth.Size = New System.Drawing.Size(40, 16)
        Me.lblTemplateHeigth.TabIndex = 67
        Me.lblTemplateHeigth.Text = "Alto:"
        Me.lblTemplateHeigth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPageSize
        '
        Me.lblPageSize.AutoSize = True
        Me.lblPageSize.BackColor = System.Drawing.Color.Transparent
        Me.lblPageSize.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPageSize.FontSize = 9.75!
        Me.lblPageSize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblPageSize.Location = New System.Drawing.Point(52, 282)
        Me.lblPageSize.Name = "lblPageSize"
        Me.lblPageSize.Size = New System.Drawing.Size(216, 16)
        Me.lblPageSize.TabIndex = 68
        Me.lblPageSize.Text = "Tamaño de la hoja (milimetros):"
        Me.lblPageSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCDoGenerateCoverPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCDoGenerateCoverPage"
        Me.Size = New System.Drawing.Size(832, 810)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cboTiposDeDocumento As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents BtnSave As ZButton
    Friend WithEvents txtnote As TextoInteligenteTextBox
    Friend WithEvents chkprintindexs As System.Windows.Forms.CheckBox
    Friend WithEvents txtCopies As TextoInteligenteTextBox
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents chkSetPrinter As System.Windows.Forms.CheckBox
    Friend WithEvents chkContinueWithGeneratedDocument As System.Windows.Forms.CheckBox
    Friend WithEvents chkDontOpenTaskAfterInsert As System.Windows.Forms.CheckBox
    Friend WithEvents lblTemplatePath As ZLabel
    Friend WithEvents txtTemplatePath As TextoInteligenteTextBox
    Friend WithEvents chkUseTemplate As CheckBox
    Friend WithEvents btnBrowse As ZButton
    Friend WithEvents chkUseCurrentTask As CheckBox
    Friend WithEvents ZLabel1 As ZLabel
    Friend WithEvents txtCopiesCount As TextoInteligenteTextBox
    Friend WithEvents ZLabel2 As ZLabel
    Friend WithEvents txtTemplateWidth As TextBox
    Friend WithEvents txtTemplateHeigth As TextBox
    Friend WithEvents lblTemplateHeigth As ZLabel
    Friend WithEvents lblTemplateWidth As ZLabel
    Friend WithEvents lblPageSize As ZLabel
End Class
