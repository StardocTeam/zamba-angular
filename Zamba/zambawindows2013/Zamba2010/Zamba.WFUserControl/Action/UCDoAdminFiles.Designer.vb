Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCDoAdminFiles
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
        Me.lblDesc1 = New Zamba.AppBlock.ZLabel()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.rdoCopy = New System.Windows.Forms.RadioButton()
        Me.rdoMove = New System.Windows.Forms.RadioButton()
        Me.rdoDelete = New System.Windows.Forms.RadioButton()
        Me.lblHelp2 = New Zamba.AppBlock.ZLabel()
        Me.grpDeleteOptions = New System.Windows.Forms.GroupBox()
        Me.rdoUseTargetPath = New System.Windows.Forms.RadioButton()
        Me.rdoUseVars = New System.Windows.Forms.RadioButton()
        Me.Label5 = New Zamba.AppBlock.ZLabel()
        Me.grpErrors = New System.Windows.Forms.GroupBox()
        Me.Label6 = New Zamba.AppBlock.ZLabel()
        Me.rdoAsString = New System.Windows.Forms.RadioButton()
        Me.rdoAsList = New System.Windows.Forms.RadioButton()
        Me.txtErrorVar = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblHelp3 = New Zamba.AppBlock.ZLabel()
        Me.btnSaveChanges = New Zamba.AppBlock.ZButton()
        Me.txtSourceVar = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtTargetPath = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblHelp1 = New Zamba.AppBlock.ZLabel()
        Me.chkOverwrite = New System.Windows.Forms.CheckBox()
        Me.grpOverwrite = New System.Windows.Forms.GroupBox()
        Me.chkWorkWithFiles = New System.Windows.Forms.CheckBox()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.grpDeleteOptions.SuspendLayout()
        Me.grpErrors.SuspendLayout()
        Me.grpOverwrite.SuspendLayout()
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
        Me.tbRule.Controls.Add(Me.grpOverwrite)
        Me.tbRule.Controls.Add(Me.lblDesc1)
        Me.tbRule.Controls.Add(Me.Label2)
        Me.tbRule.Controls.Add(Me.Label3)
        Me.tbRule.Controls.Add(Me.rdoCopy)
        Me.tbRule.Controls.Add(Me.rdoMove)
        Me.tbRule.Controls.Add(Me.rdoDelete)
        Me.tbRule.Controls.Add(Me.grpDeleteOptions)
        Me.tbRule.Controls.Add(Me.grpErrors)
        Me.tbRule.Controls.Add(Me.btnSaveChanges)
        Me.tbRule.Controls.Add(Me.txtSourceVar)
        Me.tbRule.Controls.Add(Me.txtTargetPath)
        Me.tbRule.Controls.Add(Me.lblHelp1)
        Me.tbRule.Controls.Add(Me.lblHelp2)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(948, 637)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(956, 666)
        '
        'lblDesc1
        '
        Me.lblDesc1.AutoSize = True
        Me.lblDesc1.BackColor = System.Drawing.Color.Transparent
        Me.lblDesc1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblDesc1.Location = New System.Drawing.Point(9, 20)
        Me.lblDesc1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDesc1.Name = "lblDesc1"
        Me.lblDesc1.Size = New System.Drawing.Size(119, 16)
        Me.lblDesc1.TabIndex = 0
        Me.lblDesc1.Text = "Ruta/s de Origen"
        Me.lblDesc1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(9, 86)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Ruta de Destino"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(9, 154)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(125, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Acción a ejecutar"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rdoCopy
        '
        Me.rdoCopy.AutoSize = True
        Me.rdoCopy.BackColor = System.Drawing.Color.Transparent
        Me.rdoCopy.Location = New System.Drawing.Point(40, 181)
        Me.rdoCopy.Margin = New System.Windows.Forms.Padding(4)
        Me.rdoCopy.Name = "rdoCopy"
        Me.rdoCopy.Size = New System.Drawing.Size(67, 20)
        Me.rdoCopy.TabIndex = 2
        Me.rdoCopy.TabStop = True
        Me.rdoCopy.Text = "Copiar"
        Me.rdoCopy.UseVisualStyleBackColor = False
        '
        'rdoMove
        '
        Me.rdoMove.AutoSize = True
        Me.rdoMove.BackColor = System.Drawing.Color.Transparent
        Me.rdoMove.Location = New System.Drawing.Point(40, 209)
        Me.rdoMove.Margin = New System.Windows.Forms.Padding(4)
        Me.rdoMove.Name = "rdoMove"
        Me.rdoMove.Size = New System.Drawing.Size(66, 20)
        Me.rdoMove.TabIndex = 2
        Me.rdoMove.TabStop = True
        Me.rdoMove.Text = "Mover"
        Me.rdoMove.UseVisualStyleBackColor = False
        '
        'rdoDelete
        '
        Me.rdoDelete.AutoSize = True
        Me.rdoDelete.BackColor = System.Drawing.Color.Transparent
        Me.rdoDelete.Location = New System.Drawing.Point(40, 239)
        Me.rdoDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.rdoDelete.Name = "rdoDelete"
        Me.rdoDelete.Size = New System.Drawing.Size(272, 20)
        Me.rdoDelete.TabIndex = 2
        Me.rdoDelete.TabStop = True
        Me.rdoDelete.Text = "Eliminar los directorios y archivos de:"
        Me.rdoDelete.UseVisualStyleBackColor = False
        '
        'lblHelp2
        '
        Me.lblHelp2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHelp2.AutoSize = True
        Me.lblHelp2.BackColor = System.Drawing.Color.Transparent
        Me.lblHelp2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHelp2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblHelp2.Location = New System.Drawing.Point(2191, 86)
        Me.lblHelp2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHelp2.Name = "lblHelp2"
        Me.lblHelp2.Size = New System.Drawing.Size(467, 16)
        Me.lblHelp2.TabIndex = 9
        Me.lblHelp2.Text = " Leer la ayuda de la regla para conocer los tipos de datos disponibles"
        Me.lblHelp2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpDeleteOptions
        '
        Me.grpDeleteOptions.BackColor = System.Drawing.Color.Transparent
        Me.grpDeleteOptions.Controls.Add(Me.rdoUseTargetPath)
        Me.grpDeleteOptions.Controls.Add(Me.rdoUseVars)
        Me.grpDeleteOptions.Enabled = False
        Me.grpDeleteOptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDeleteOptions.Location = New System.Drawing.Point(320, 239)
        Me.grpDeleteOptions.Margin = New System.Windows.Forms.Padding(4)
        Me.grpDeleteOptions.Name = "grpDeleteOptions"
        Me.grpDeleteOptions.Padding = New System.Windows.Forms.Padding(4)
        Me.grpDeleteOptions.Size = New System.Drawing.Size(212, 55)
        Me.grpDeleteOptions.TabIndex = 10
        Me.grpDeleteOptions.TabStop = False
        '
        'rdoUseTargetPath
        '
        Me.rdoUseTargetPath.AutoSize = True
        Me.rdoUseTargetPath.BackColor = System.Drawing.Color.Transparent
        Me.rdoUseTargetPath.Location = New System.Drawing.Point(91, 23)
        Me.rdoUseTargetPath.Margin = New System.Windows.Forms.Padding(4)
        Me.rdoUseTargetPath.Name = "rdoUseTargetPath"
        Me.rdoUseTargetPath.Size = New System.Drawing.Size(61, 17)
        Me.rdoUseTargetPath.TabIndex = 3
        Me.rdoUseTargetPath.TabStop = True
        Me.rdoUseTargetPath.Text = "Destino"
        Me.rdoUseTargetPath.UseVisualStyleBackColor = False
        '
        'rdoUseVars
        '
        Me.rdoUseVars.AutoSize = True
        Me.rdoUseVars.BackColor = System.Drawing.Color.Transparent
        Me.rdoUseVars.Location = New System.Drawing.Point(8, 23)
        Me.rdoUseVars.Margin = New System.Windows.Forms.Padding(4)
        Me.rdoUseVars.Name = "rdoUseVars"
        Me.rdoUseVars.Size = New System.Drawing.Size(56, 17)
        Me.rdoUseVars.TabIndex = 3
        Me.rdoUseVars.TabStop = True
        Me.rdoUseVars.Text = "Origen"
        Me.rdoUseVars.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(8, 21)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(557, 16)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "En caso de no poder realizar la acción, se guardará el error en la siguiente vari" &
    "able"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpErrors
        '
        Me.grpErrors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpErrors.BackColor = System.Drawing.Color.Transparent
        Me.grpErrors.Controls.Add(Me.Label6)
        Me.grpErrors.Controls.Add(Me.rdoAsString)
        Me.grpErrors.Controls.Add(Me.rdoAsList)
        Me.grpErrors.Controls.Add(Me.txtErrorVar)
        Me.grpErrors.Controls.Add(Me.Label5)
        Me.grpErrors.Controls.Add(Me.lblHelp3)
        Me.grpErrors.Location = New System.Drawing.Point(8, 315)
        Me.grpErrors.Margin = New System.Windows.Forms.Padding(4)
        Me.grpErrors.Name = "grpErrors"
        Me.grpErrors.Padding = New System.Windows.Forms.Padding(4)
        Me.grpErrors.Size = New System.Drawing.Size(1509, 177)
        Me.grpErrors.TabIndex = 13
        Me.grpErrors.TabStop = False
        Me.grpErrors.Text = "Errores"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(8, 81)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(178, 16)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "El error se guardará como"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rdoAsString
        '
        Me.rdoAsString.AutoSize = True
        Me.rdoAsString.BackColor = System.Drawing.Color.Transparent
        Me.rdoAsString.Location = New System.Drawing.Point(12, 101)
        Me.rdoAsString.Margin = New System.Windows.Forms.Padding(4)
        Me.rdoAsString.Name = "rdoAsString"
        Me.rdoAsString.Size = New System.Drawing.Size(136, 20)
        Me.rdoAsString.TabIndex = 5
        Me.rdoAsString.TabStop = True
        Me.rdoAsString.Text = "Cadena de texto"
        Me.rdoAsString.UseVisualStyleBackColor = False
        '
        'rdoAsList
        '
        Me.rdoAsList.AutoSize = True
        Me.rdoAsList.BackColor = System.Drawing.Color.Transparent
        Me.rdoAsList.Location = New System.Drawing.Point(12, 130)
        Me.rdoAsList.Margin = New System.Windows.Forms.Padding(4)
        Me.rdoAsList.Name = "rdoAsList"
        Me.rdoAsList.Size = New System.Drawing.Size(199, 20)
        Me.rdoAsList.TabIndex = 5
        Me.rdoAsList.TabStop = True
        Me.rdoAsList.Text = "Lista de cadenas de texto"
        Me.rdoAsList.UseVisualStyleBackColor = False
        '
        'txtErrorVar
        '
        Me.txtErrorVar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtErrorVar.Location = New System.Drawing.Point(8, 41)
        Me.txtErrorVar.Margin = New System.Windows.Forms.Padding(4)
        Me.txtErrorVar.MaxLength = 4000
        Me.txtErrorVar.Name = "txtErrorVar"
        Me.txtErrorVar.Size = New System.Drawing.Size(1492, 25)
        Me.txtErrorVar.TabIndex = 27
        Me.txtErrorVar.Text = ""
        '
        'lblHelp3
        '
        Me.lblHelp3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHelp3.AutoSize = True
        Me.lblHelp3.BackColor = System.Drawing.Color.Transparent
        Me.lblHelp3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHelp3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblHelp3.Location = New System.Drawing.Point(1011, 147)
        Me.lblHelp3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHelp3.Name = "lblHelp3"
        Me.lblHelp3.Size = New System.Drawing.Size(343, 32)
        Me.lblHelp3.TabIndex = 14
        Me.lblHelp3.Text = "Los errores de ejecución que puedan darse" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "en esta regla no cortarán la ejecución" &
    " de la misma"
        Me.lblHelp3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSaveChanges
        '
        Me.btnSaveChanges.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveChanges.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSaveChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveChanges.ForeColor = System.Drawing.Color.White
        Me.btnSaveChanges.Location = New System.Drawing.Point(2851, 500)
        Me.btnSaveChanges.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSaveChanges.Name = "btnSaveChanges"
        Me.btnSaveChanges.Size = New System.Drawing.Size(155, 28)
        Me.btnSaveChanges.TabIndex = 6
        Me.btnSaveChanges.Text = "Guardar cambios"
        Me.btnSaveChanges.UseVisualStyleBackColor = True
        '
        'txtSourceVar
        '
        Me.txtSourceVar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSourceVar.Location = New System.Drawing.Point(8, 39)
        Me.txtSourceVar.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSourceVar.MaxLength = 4000
        Me.txtSourceVar.Name = "txtSourceVar"
        Me.txtSourceVar.Size = New System.Drawing.Size(1500, 25)
        Me.txtSourceVar.TabIndex = 25
        Me.txtSourceVar.Text = ""
        '
        'txtTargetPath
        '
        Me.txtTargetPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTargetPath.Location = New System.Drawing.Point(8, 106)
        Me.txtTargetPath.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTargetPath.MaxLength = 4000
        Me.txtTargetPath.Name = "txtTargetPath"
        Me.txtTargetPath.Size = New System.Drawing.Size(1500, 25)
        Me.txtTargetPath.TabIndex = 26
        Me.txtTargetPath.Text = ""
        '
        'lblHelp1
        '
        Me.lblHelp1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHelp1.AutoSize = True
        Me.lblHelp1.BackColor = System.Drawing.Color.Transparent
        Me.lblHelp1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHelp1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblHelp1.Location = New System.Drawing.Point(2191, 20)
        Me.lblHelp1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHelp1.Name = "lblHelp1"
        Me.lblHelp1.Size = New System.Drawing.Size(467, 16)
        Me.lblHelp1.TabIndex = 27
        Me.lblHelp1.Text = " Leer la ayuda de la regla para conocer los tipos de datos disponibles"
        Me.lblHelp1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkOverwrite
        '
        Me.chkOverwrite.AutoSize = True
        Me.chkOverwrite.Location = New System.Drawing.Point(8, 25)
        Me.chkOverwrite.Margin = New System.Windows.Forms.Padding(4)
        Me.chkOverwrite.Name = "chkOverwrite"
        Me.chkOverwrite.Size = New System.Drawing.Size(172, 20)
        Me.chkOverwrite.TabIndex = 28
        Me.chkOverwrite.Text = "Sobreescribir archivos"
        Me.chkOverwrite.UseVisualStyleBackColor = True
        '
        'grpOverwrite
        '
        Me.grpOverwrite.Controls.Add(Me.chkWorkWithFiles)
        Me.grpOverwrite.Controls.Add(Me.chkOverwrite)
        Me.grpOverwrite.Enabled = False
        Me.grpOverwrite.Location = New System.Drawing.Point(123, 174)
        Me.grpOverwrite.Margin = New System.Windows.Forms.Padding(4)
        Me.grpOverwrite.Name = "grpOverwrite"
        Me.grpOverwrite.Padding = New System.Windows.Forms.Padding(4)
        Me.grpOverwrite.Size = New System.Drawing.Size(762, 57)
        Me.grpOverwrite.TabIndex = 29
        Me.grpOverwrite.TabStop = False
        '
        'chkWorkWithFiles
        '
        Me.chkWorkWithFiles.AutoSize = True
        Me.chkWorkWithFiles.Location = New System.Drawing.Point(191, 25)
        Me.chkWorkWithFiles.Margin = New System.Windows.Forms.Padding(4)
        Me.chkWorkWithFiles.Name = "chkWorkWithFiles"
        Me.chkWorkWithFiles.Size = New System.Drawing.Size(525, 20)
        Me.chkWorkWithFiles.TabIndex = 29
        Me.chkWorkWithFiles.Text = "Trabajar con Archivos, La Ruta Origen y Destno son archivos no directorios"
        Me.chkWorkWithFiles.UseVisualStyleBackColor = True
        '
        'UCDoAdminFiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCDoAdminFiles"
        Me.Size = New System.Drawing.Size(956, 666)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.grpDeleteOptions.ResumeLayout(False)
        Me.grpDeleteOptions.PerformLayout()
        Me.grpErrors.ResumeLayout(False)
        Me.grpErrors.PerformLayout()
        Me.grpOverwrite.ResumeLayout(False)
        Me.grpOverwrite.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblDesc1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents rdoCopy As System.Windows.Forms.RadioButton
    Friend WithEvents rdoMove As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDelete As System.Windows.Forms.RadioButton
    Friend WithEvents lblHelp2 As ZLabel
    Friend WithEvents grpDeleteOptions As System.Windows.Forms.GroupBox
    Friend WithEvents rdoUseTargetPath As System.Windows.Forms.RadioButton
    Friend WithEvents rdoUseVars As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents grpErrors As System.Windows.Forms.GroupBox
    Friend WithEvents rdoAsList As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAsString As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents btnSaveChanges As ZButton
    Friend WithEvents lblHelp3 As ZLabel
    Friend WithEvents txtErrorVar As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtSourceVar As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtTargetPath As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblHelp1 As ZLabel
    Friend WithEvents chkOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents grpOverwrite As System.Windows.Forms.GroupBox
    Friend WithEvents chkWorkWithFiles As System.Windows.Forms.CheckBox

End Class
