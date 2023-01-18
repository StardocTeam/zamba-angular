<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoInsertDocToBlob
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
        Me.btnSaveConfig = New Zamba.AppBlock.ZButton()
        Me.txtDocId = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblMensaje = New ZLabel()
        Me.txtDocType = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label3 = New ZLabel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.txtDocType)
        Me.tbRule.Controls.Add(Me.Label3)
        Me.tbRule.Controls.Add(Me.txtDocId)
        Me.tbRule.Controls.Add(Me.lblMensaje)
        Me.tbRule.Controls.Add(Me.btnSaveConfig)
        '
        'btnSaveConfig
        '
        Me.btnSaveConfig.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSaveConfig.Location = New System.Drawing.Point(11, 102)
        Me.btnSaveConfig.Name = "btnSaveConfig"
        Me.btnSaveConfig.Size = New System.Drawing.Size(72, 27)
        Me.btnSaveConfig.TabIndex = 4
        Me.btnSaveConfig.Text = "Guardar"
        '
        'txtDocId
        '
        Me.txtDocId.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocId.Location = New System.Drawing.Point(11, 29)
        Me.txtDocId.MaxLength = 0
        Me.txtDocId.Multiline = False
        Me.txtDocId.Name = "txtDocId"
        Me.txtDocId.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.txtDocId.ShowSelectionMargin = True
        Me.txtDocId.Size = New System.Drawing.Size(462, 24)
        Me.txtDocId.TabIndex = 17
        Me.txtDocId.Text = ""
        '
        'lblMensaje
        '
        Me.lblMensaje.AutoSize = True
        Me.lblMensaje.BackColor = System.Drawing.Color.Transparent
        Me.lblMensaje.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMensaje.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.lblMensaje.Location = New System.Drawing.Point(8, 13)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(140, 13)
        Me.lblMensaje.TabIndex = 16
        Me.lblMensaje.Text = "ID del documento a insertar"
        Me.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDocType
        '
        Me.txtDocType.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocType.Location = New System.Drawing.Point(11, 72)
        Me.txtDocType.MaxLength = 0
        Me.txtDocType.Multiline = False
        Me.txtDocType.Name = "txtDocType"
        Me.txtDocType.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.txtDocType.ShowSelectionMargin = True
        Me.txtDocType.Size = New System.Drawing.Size(462, 24)
        Me.txtDocType.TabIndex = 19
        Me.txtDocType.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Me.Label3.Location = New System.Drawing.Point(8, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "ID de entidad"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCDoInsertDocToBlob
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoInsertDocToBlob"
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSaveConfig As Zamba.AppBlock.ZButton
    Friend WithEvents txtDocId As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblMensaje As ZLabel
    Friend WithEvents txtDocType As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label3 As ZLabel

End Class
