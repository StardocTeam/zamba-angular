<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoLoadDataSetFromXML
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
        Me.tbOpenTag = New System.Windows.Forms.TextBox
        Me.tbEndTag = New System.Windows.Forms.TextBox
        Me.lblXML = New ZLabel
        Me.lblVar = New ZLabel
        Me.lblOpenTag = New ZLabel
        Me.lblEndTask = New ZLabel
        Me.tbXML = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.tbVariable = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.btnGuardar = New ZButton
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.btnGuardar)
        Me.tbRule.Controls.Add(Me.tbVariable)
        Me.tbRule.Controls.Add(Me.tbXML)
        Me.tbRule.Controls.Add(Me.lblEndTask)
        Me.tbRule.Controls.Add(Me.lblOpenTag)
        Me.tbRule.Controls.Add(Me.lblVar)
        Me.tbRule.Controls.Add(Me.lblXML)
        Me.tbRule.Controls.Add(Me.tbEndTag)
        Me.tbRule.Controls.Add(Me.tbOpenTag)
        Me.tbRule.Size = New System.Drawing.Size(499, 478)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(507, 504)
        '
        'tbOpenTag
        '
        Me.tbOpenTag.Location = New System.Drawing.Point(6, 77)
        Me.tbOpenTag.Name = "tbOpenTag"
        Me.tbOpenTag.Size = New System.Drawing.Size(195, 21)
        Me.tbOpenTag.TabIndex = 10
        '
        'tbEndTag
        '
        Me.tbEndTag.Location = New System.Drawing.Point(9, 37)
        Me.tbEndTag.Name = "tbEndTag"
        Me.tbEndTag.Size = New System.Drawing.Size(195, 21)
        Me.tbEndTag.TabIndex = 11
        '
        'lblXML
        '
        Me.lblXML.AutoSize = True
        Me.lblXML.Location = New System.Drawing.Point(6, 141)
        Me.lblXML.Name = "lblXML"
        Me.lblXML.Size = New System.Drawing.Size(58, 13)
        Me.lblXML.TabIndex = 12
        Me.lblXML.Text = "TextoXML:"
        '
        'lblVar
        '
        Me.lblVar.AutoSize = True
        Me.lblVar.Location = New System.Drawing.Point(6, 101)
        Me.lblVar.Name = "lblVar"
        Me.lblVar.Size = New System.Drawing.Size(152, 13)
        Me.lblVar.TabIndex = 13
        Me.lblVar.Text = "Variable a guardar el DataSet:"
        '
        'lblOpenTag
        '
        Me.lblOpenTag.AutoSize = True
        Me.lblOpenTag.Location = New System.Drawing.Point(6, 61)
        Me.lblOpenTag.Name = "lblOpenTag"
        Me.lblOpenTag.Size = New System.Drawing.Size(89, 13)
        Me.lblOpenTag.TabIndex = 14
        Me.lblOpenTag.Text = "Tag de apertura:"
        '
        'lblEndTask
        '
        Me.lblEndTask.AutoSize = True
        Me.lblEndTask.Location = New System.Drawing.Point(6, 21)
        Me.lblEndTask.Name = "lblEndTask"
        Me.lblEndTask.Size = New System.Drawing.Size(74, 13)
        Me.lblEndTask.TabIndex = 15
        Me.lblEndTask.Text = "Tag de cierre:"
        '
        'tbXML
        '
        Me.tbXML.Location = New System.Drawing.Point(9, 158)
        Me.tbXML.Name = "tbXML"
        Me.tbXML.Size = New System.Drawing.Size(484, 285)
        Me.tbXML.TabIndex = 16
        Me.tbXML.Text = ""
        '
        'tbVariable
        '
        Me.tbVariable.Location = New System.Drawing.Point(6, 118)
        Me.tbVariable.Name = "tbVariable"
        Me.tbVariable.Size = New System.Drawing.Size(195, 20)
        Me.tbVariable.TabIndex = 17
        Me.tbVariable.Text = ""
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(9, 449)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(75, 23)
        Me.btnGuardar.TabIndex = 18
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'UCDoLoadDataSetFromXML
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "UCDoLoadDataSetFromXML"
        Me.Size = New System.Drawing.Size(507, 504)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbEndTag As System.Windows.Forms.TextBox
    Friend WithEvents tbOpenTag As System.Windows.Forms.TextBox
    Friend WithEvents lblEndTask As ZLabel
    Friend WithEvents lblOpenTag As ZLabel
    Friend WithEvents lblVar As ZLabel
    Friend WithEvents lblXML As ZLabel
    Friend WithEvents tbVariable As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents tbXML As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnGuardar As ZButton

End Class
