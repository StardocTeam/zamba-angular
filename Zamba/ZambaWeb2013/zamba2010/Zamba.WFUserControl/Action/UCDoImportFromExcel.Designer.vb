<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoImportFromExcel
    Inherits Zamba.WFUserControl.ZRuleControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.txtFilePath = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.btnBrowse = New Zamba.AppBlock.ZButton()
        Me.txtVersion = New Zamba.AppBlock.ZLabel()
        Me.cmbVersion = New System.Windows.Forms.ComboBox()
        Me.BtnSave = New Zamba.AppBlock.ZButton()
        Me.btnPreview = New Zamba.AppBlock.ZButton()
        Me.lblSheetName = New Zamba.AppBlock.ZLabel()
        Me.txtsheetName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.grdTableCols = New System.Windows.Forms.DataGridView()
        Me.Label4 = New Zamba.AppBlock.ZLabel()
        Me.txtVarName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtDirPath = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtSaveFileName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label5 = New Zamba.AppBlock.ZLabel()
        Me.Label6 = New Zamba.AppBlock.ZLabel()
        Me.chkSaveCopyAs = New System.Windows.Forms.CheckBox()
        Me.btnBrowse2 = New Zamba.AppBlock.ZButton()
        Me.chkUseSpire = New System.Windows.Forms.CheckBox()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        CType(Me.grdTableCols, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbState.Size = New System.Drawing.Size(824, 781)
        '
        'tbHabilitation
        '
        Me.tbHabilitation.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbHabilitation.Size = New System.Drawing.Size(824, 781)
        '
        'tbConfiguration
        '
        Me.tbConfiguration.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbConfiguration.Size = New System.Drawing.Size(824, 781)
        '
        'tbAlerts
        '
        Me.tbAlerts.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbAlerts.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbAlerts.Size = New System.Drawing.Size(824, 781)
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.chkUseSpire)
        Me.tbRule.Controls.Add(Me.btnBrowse2)
        Me.tbRule.Controls.Add(Me.chkSaveCopyAs)
        Me.tbRule.Controls.Add(Me.Label6)
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.txtSaveFileName)
        Me.tbRule.Controls.Add(Me.txtDirPath)
        Me.tbRule.Controls.Add(Me.txtVarName)
        Me.tbRule.Controls.Add(Me.Label4)
        Me.tbRule.Controls.Add(Me.txtsheetName)
        Me.tbRule.Controls.Add(Me.grdTableCols)
        Me.tbRule.Controls.Add(Me.lblSheetName)
        Me.tbRule.Controls.Add(Me.btnPreview)
        Me.tbRule.Controls.Add(Me.BtnSave)
        Me.tbRule.Controls.Add(Me.cmbVersion)
        Me.tbRule.Controls.Add(Me.txtVersion)
        Me.tbRule.Controls.Add(Me.btnBrowse)
        Me.tbRule.Controls.Add(Me.txtFilePath)
        Me.tbRule.Controls.Add(Me.Label2)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(760, 637)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(768, 666)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(24, 39)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Archivo Excel:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(132, 36)
        Me.txtFilePath.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtFilePath.MaxLength = 4000
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtFilePath.Size = New System.Drawing.Size(489, 27)
        Me.txtFilePath.TabIndex = 17
        Me.txtFilePath.Text = ""
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.ForeColor = System.Drawing.Color.White
        Me.btnBrowse.Location = New System.Drawing.Point(631, 36)
        Me.btnBrowse.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(101, 28)
        Me.btnBrowse.TabIndex = 36
        Me.btnBrowse.Text = "Examinar"
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        'txtVersion
        '
        Me.txtVersion.AutoSize = True
        Me.txtVersion.BackColor = System.Drawing.Color.Transparent
        Me.txtVersion.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.txtVersion.Location = New System.Drawing.Point(435, 89)
        Me.txtVersion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.txtVersion.Name = "txtVersion"
        Me.txtVersion.Size = New System.Drawing.Size(126, 16)
        Me.txtVersion.TabIndex = 37
        Me.txtVersion.Text = "Version de Office:"
        Me.txtVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbVersion
        '
        Me.cmbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVersion.FormattingEnabled = True
        Me.cmbVersion.Location = New System.Drawing.Point(567, 85)
        Me.cmbVersion.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbVersion.Name = "cmbVersion"
        Me.cmbVersion.Size = New System.Drawing.Size(164, 24)
        Me.cmbVersion.TabIndex = 38
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.ForeColor = System.Drawing.Color.White
        Me.BtnSave.Location = New System.Drawing.Point(333, 574)
        Me.BtnSave.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(93, 33)
        Me.BtnSave.TabIndex = 39
        Me.BtnSave.Text = "Guardar"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'btnPreview
        '
        Me.btnPreview.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.ForeColor = System.Drawing.Color.White
        Me.btnPreview.Location = New System.Drawing.Point(344, 81)
        Me.btnPreview.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(83, 33)
        Me.btnPreview.TabIndex = 40
        Me.btnPreview.Text = "Ver"
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'lblSheetName
        '
        Me.lblSheetName.AutoSize = True
        Me.lblSheetName.BackColor = System.Drawing.Color.Transparent
        Me.lblSheetName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSheetName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblSheetName.Location = New System.Drawing.Point(24, 89)
        Me.lblSheetName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSheetName.Name = "lblSheetName"
        Me.lblSheetName.Size = New System.Drawing.Size(117, 16)
        Me.lblSheetName.TabIndex = 41
        Me.lblSheetName.Text = "Nombre de hoja:"
        Me.lblSheetName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtsheetName
        '
        Me.txtsheetName.Location = New System.Drawing.Point(148, 85)
        Me.txtsheetName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtsheetName.MaxLength = 4000
        Me.txtsheetName.Name = "txtsheetName"
        Me.txtsheetName.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtsheetName.Size = New System.Drawing.Size(187, 27)
        Me.txtsheetName.TabIndex = 42
        Me.txtsheetName.Text = ""
        '
        'grdTableCols
        '
        Me.grdTableCols.AllowUserToAddRows = False
        Me.grdTableCols.AllowUserToDeleteRows = False
        Me.grdTableCols.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdTableCols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTableCols.Location = New System.Drawing.Point(28, 178)
        Me.grdTableCols.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdTableCols.Name = "grdTableCols"
        Me.grdTableCols.ReadOnly = True
        Me.grdTableCols.Size = New System.Drawing.Size(704, 256)
        Me.grdTableCols.TabIndex = 43
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(24, 138)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(142, 16)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "Guardar en Variable:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVarName
        '
        Me.txtVarName.Location = New System.Drawing.Point(173, 134)
        Me.txtVarName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtVarName.MaxLength = 4000
        Me.txtVarName.Name = "txtVarName"
        Me.txtVarName.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtVarName.Size = New System.Drawing.Size(557, 27)
        Me.txtVarName.TabIndex = 45
        Me.txtVarName.Text = ""
        '
        'txtDirPath
        '
        Me.txtDirPath.Enabled = False
        Me.txtDirPath.Location = New System.Drawing.Point(173, 491)
        Me.txtDirPath.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtDirPath.MaxLength = 4000
        Me.txtDirPath.Name = "txtDirPath"
        Me.txtDirPath.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtDirPath.Size = New System.Drawing.Size(448, 27)
        Me.txtDirPath.TabIndex = 46
        Me.txtDirPath.Text = ""
        '
        'txtSaveFileName
        '
        Me.txtSaveFileName.Enabled = False
        Me.txtSaveFileName.Location = New System.Drawing.Point(173, 523)
        Me.txtSaveFileName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtSaveFileName.MaxLength = 4000
        Me.txtSaveFileName.Name = "txtSaveFileName"
        Me.txtSaveFileName.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtSaveFileName.Size = New System.Drawing.Size(557, 27)
        Me.txtSaveFileName.TabIndex = 47
        Me.txtSaveFileName.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(24, 495)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 16)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "Ubicación:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(24, 527)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 16)
        Me.Label6.TabIndex = 49
        Me.Label6.Text = "Nombre:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkSaveCopyAs
        '
        Me.chkSaveCopyAs.AutoSize = True
        Me.chkSaveCopyAs.Location = New System.Drawing.Point(28, 453)
        Me.chkSaveCopyAs.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkSaveCopyAs.Name = "chkSaveCopyAs"
        Me.chkSaveCopyAs.Size = New System.Drawing.Size(195, 20)
        Me.chkSaveCopyAs.TabIndex = 50
        Me.chkSaveCopyAs.Text = "Guardar copia del archivo"
        Me.chkSaveCopyAs.UseVisualStyleBackColor = True
        '
        'btnBrowse2
        '
        Me.btnBrowse2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnBrowse2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse2.ForeColor = System.Drawing.Color.White
        Me.btnBrowse2.Location = New System.Drawing.Point(631, 491)
        Me.btnBrowse2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnBrowse2.Name = "btnBrowse2"
        Me.btnBrowse2.Size = New System.Drawing.Size(101, 28)
        Me.btnBrowse2.TabIndex = 51
        Me.btnBrowse2.Text = "Examinar"
        Me.btnBrowse2.UseVisualStyleBackColor = False
        '
        'chkUseSpire
        '
        Me.chkUseSpire.AutoSize = True
        Me.chkUseSpire.Location = New System.Drawing.Point(250, 453)
        Me.chkUseSpire.Margin = New System.Windows.Forms.Padding(4)
        Me.chkUseSpire.Name = "chkUseSpire"
        Me.chkUseSpire.Size = New System.Drawing.Size(138, 20)
        Me.chkUseSpire.TabIndex = 52
        Me.chkUseSpire.Text = "Utilizar con Spire"
        Me.chkUseSpire.UseVisualStyleBackColor = True
        '
        'UCDoImportFromExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCDoImportFromExcel"
        Me.Size = New System.Drawing.Size(768, 666)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        CType(Me.grdTableCols, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents txtFilePath As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents cmbVersion As System.Windows.Forms.ComboBox
    Friend WithEvents txtVersion As ZLabel
    Friend WithEvents btnBrowse As Zamba.AppBlock.ZButton
    Friend WithEvents BtnSave As Zamba.AppBlock.ZButton
    Friend WithEvents btnPreview As Zamba.AppBlock.ZButton
    Friend WithEvents txtsheetName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblSheetName As ZLabel
    Friend WithEvents grdTableCols As System.Windows.Forms.DataGridView
    Friend WithEvents txtVarName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents txtSaveFileName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtDirPath As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents chkSaveCopyAs As System.Windows.Forms.CheckBox
    Friend WithEvents btnBrowse2 As Zamba.AppBlock.ZButton
    Friend WithEvents chkUseSpire As CheckBox
End Class
