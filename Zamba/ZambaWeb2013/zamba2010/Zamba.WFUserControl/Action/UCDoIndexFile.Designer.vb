<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoIndexFile
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
    Private Shadows Sub InitializeComponent()
        Me.Label1 = New ZLabel()
        Me.Label2 = New ZLabel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label3 = New ZLabel()
        Me.btnAceptar = New ZButton()
        Me.lblDocTypeId = New ZLabel()
        Me.lblDocId = New ZLabel()
        Me.txtDocTypeId = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.btnAccept = New ZButton()
        Me.txtDocId = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblDocumentPath = New ZLabel()
        Me.txtDocumentPath = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.txtVarName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label4 = New ZLabel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.Label4)
        Me.tbRule.Controls.Add(Me.txtVarName)
        Me.tbRule.Controls.Add(Me.txtDocumentPath)
        Me.tbRule.Controls.Add(Me.lblDocumentPath)
        Me.tbRule.Controls.Add(Me.txtDocId)
        Me.tbRule.Controls.Add(Me.btnAccept)
        Me.tbRule.Controls.Add(Me.txtDocTypeId)
        Me.tbRule.Controls.Add(Me.lblDocId)
        Me.tbRule.Controls.Add(Me.lblDocTypeId)
        Me.tbRule.Size = New System.Drawing.Size(431, 330)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(439, 356)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(34, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nombre "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(34, 118)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Valor "
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(129, 77)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(160, 21)
        Me.TextBox1.TabIndex = 2
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(129, 111)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(160, 21)
        Me.TextBox2.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(34, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 20)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Variable"
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(150, 177)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(112, 23)
        Me.btnAceptar.TabIndex = 13
        Me.btnAceptar.Text = "Aceptar"
        '
        'lblDocTypeId
        '
        Me.lblDocTypeId.AutoSize = True
        Me.lblDocTypeId.Location = New System.Drawing.Point(39, 90)
        Me.lblDocTypeId.Name = "lblDocTypeId"
        Me.lblDocTypeId.Size = New System.Drawing.Size(63, 13)
        Me.lblDocTypeId.TabIndex = 0
        Me.lblDocTypeId.Text = "Doc Type id"
        '
        'lblDocId
        '
        Me.lblDocId.AutoSize = True
        Me.lblDocId.Location = New System.Drawing.Point(40, 123)
        Me.lblDocId.Name = "lblDocId"
        Me.lblDocId.Size = New System.Drawing.Size(38, 13)
        Me.lblDocId.TabIndex = 1
        Me.lblDocId.Text = "Doc Id"
        '
        'txtDocTypeId
        '
        Me.txtDocTypeId.Location = New System.Drawing.Point(204, 82)
        Me.txtDocTypeId.Name = "txtDocTypeId"
        Me.txtDocTypeId.Size = New System.Drawing.Size(185, 21)
        Me.txtDocTypeId.TabIndex = 2
        Me.txtDocTypeId.Text = ""
        '
        'btnAccept
        '
        Me.btnAccept.Location = New System.Drawing.Point(156, 243)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(75, 23)
        Me.btnAccept.TabIndex = 5
        Me.btnAccept.Text = "Aceptar"
        Me.btnAccept.UseVisualStyleBackColor = True
        '
        'txtDocId
        '
        Me.txtDocId.Location = New System.Drawing.Point(204, 120)
        Me.txtDocId.MaxLength = 4000
        Me.txtDocId.Name = "txtDocId"
        Me.txtDocId.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtDocId.Size = New System.Drawing.Size(185, 23)
        Me.txtDocId.TabIndex = 14
        Me.txtDocId.Text = ""
        '
        'lblDocumentPath
        '
        Me.lblDocumentPath.AutoSize = True
        Me.lblDocumentPath.Location = New System.Drawing.Point(39, 155)
        Me.lblDocumentPath.Name = "lblDocumentPath"
        Me.lblDocumentPath.Size = New System.Drawing.Size(87, 13)
        Me.lblDocumentPath.TabIndex = 17
        Me.lblDocumentPath.Text = "Ruta Documento"
        '
        'txtDocumentPath
        '
        Me.txtDocumentPath.Location = New System.Drawing.Point(204, 152)
        Me.txtDocumentPath.MaxLength = 4000
        Me.txtDocumentPath.Name = "txtDocumentPath"
        Me.txtDocumentPath.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtDocumentPath.Size = New System.Drawing.Size(185, 23)
        Me.txtDocumentPath.TabIndex = 18
        Me.txtDocumentPath.Text = ""
        '
        'txtVarName
        '
        Me.txtVarName.Location = New System.Drawing.Point(204, 203)
        Me.txtVarName.MaxLength = 4000
        Me.txtVarName.Name = "txtVarName"
        Me.txtVarName.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.txtVarName.Size = New System.Drawing.Size(185, 23)
        Me.txtVarName.TabIndex = 19
        Me.txtVarName.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(40, 213)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Nombre Variable"
        '
        'UCDoIndexFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "UCDoIndexFile"
        Me.Size = New System.Drawing.Size(439, 356)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.TextBox1, 0)
        Me.Controls.SetChildIndex(Me.TextBox2, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.btnAceptar, 0)
        Me.Controls.SetChildIndex(Me.tbctrMain, 0)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents btnAccept As ZButton
    Friend WithEvents txtDocTypeId As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblDocId As ZLabel
    Friend WithEvents lblDocTypeId As ZLabel
    Friend WithEvents txtDocId As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblDocumentPath As ZLabel
    Friend WithEvents txtDocumentPath As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents txtVarName As Zamba.AppBlock.TextoInteligenteTextBox

End Class
