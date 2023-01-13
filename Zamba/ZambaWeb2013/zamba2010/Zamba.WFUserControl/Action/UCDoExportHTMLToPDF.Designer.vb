<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoExportHTMLToPDF
    Inherits Zamba.WFUserControl.ZRuleControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tbToExportContent = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.tbReturnFileName = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.cbEditable = New System.Windows.Forms.CheckBox
        Me.lblReturnFileName = New ZLabel
        Me.lblHTML = New ZLabel
        Me.btnGuardar = New ZButton
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.btnGuardar)
        Me.tbRule.Controls.Add(Me.lblHTML)
        Me.tbRule.Controls.Add(Me.lblReturnFileName)
        Me.tbRule.Controls.Add(Me.cbEditable)
        Me.tbRule.Controls.Add(Me.tbReturnFileName)
        Me.tbRule.Controls.Add(Me.tbToExportContent)
        '
        'tbToExportContent
        '
        Me.tbToExportContent.Location = New System.Drawing.Point(6, 99)
        Me.tbToExportContent.Name = "tbToExportContent"
        Me.tbToExportContent.Size = New System.Drawing.Size(401, 387)
        Me.tbToExportContent.TabIndex = 18
        Me.tbToExportContent.Text = ""
        '
        'tbReturnFileName
        '
        Me.tbReturnFileName.Location = New System.Drawing.Point(6, 37)
        Me.tbReturnFileName.Name = "tbReturnFileName"
        Me.tbReturnFileName.Size = New System.Drawing.Size(195, 20)
        Me.tbReturnFileName.TabIndex = 19
        Me.tbReturnFileName.Text = ""
        '
        'cbEditable
        '
        Me.cbEditable.AutoSize = True
        Me.cbEditable.Location = New System.Drawing.Point(6, 63)
        Me.cbEditable.Name = "cbEditable"
        Me.cbEditable.Size = New System.Drawing.Size(103, 17)
        Me.cbEditable.TabIndex = 20
        Me.cbEditable.Text = "Archivo editable"
        Me.cbEditable.UseVisualStyleBackColor = True
        '
        'lblReturnFileName
        '
        Me.lblReturnFileName.AutoSize = True
        Me.lblReturnFileName.Location = New System.Drawing.Point(6, 21)
        Me.lblReturnFileName.Name = "lblReturnFileName"
        Me.lblReturnFileName.Size = New System.Drawing.Size(155, 13)
        Me.lblReturnFileName.TabIndex = 22
        Me.lblReturnFileName.Text = "Nombre del archivo a retornar:"
        '
        'lblHTML
        '
        Me.lblHTML.AutoSize = True
        Me.lblHTML.Location = New System.Drawing.Point(6, 83)
        Me.lblHTML.Name = "lblHTML"
        Me.lblHTML.Size = New System.Drawing.Size(107, 13)
        Me.lblHTML.TabIndex = 23
        Me.lblHTML.Text = "HTML para exportar:"
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(6, 492)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(75, 23)
        Me.btnGuardar.TabIndex = 24
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'UCDoExportHTMLToPDF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoExportHTMLToPDF"
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbToExportContent As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents cbEditable As System.Windows.Forms.CheckBox
    Friend WithEvents tbReturnFileName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnGuardar As ZButton
    Friend WithEvents lblHTML As ZLabel
    Friend WithEvents lblReturnFileName As ZLabel

End Class
