Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDOGenerateExcelVis
    Inherits ZControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Overloads Sub InitializeComponent()
        Me.chkIndex = New System.Windows.Forms.CheckBox
        Me.chkSum = New System.Windows.Forms.CheckBox
        Me.chkCount = New System.Windows.Forms.CheckBox
        Me.txtName = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.lblShow = New ZLabel
        Me.lblCount = New ZLabel
        Me.lblSum = New ZLabel
        Me.lblName = New ZLabel
        Me.SuspendLayout()
        '
        'chkIndex
        '
        Me.chkIndex.AutoSize = True
        Me.chkIndex.Location = New System.Drawing.Point(9, 23)
        Me.chkIndex.Name = "chkIndex"
        Me.chkIndex.Size = New System.Drawing.Size(15, 14)
        Me.chkIndex.TabIndex = 0
        Me.chkIndex.UseVisualStyleBackColor = True
        '
        'chkSum
        '
        Me.chkSum.AutoSize = True
        Me.chkSum.Enabled = False
        Me.chkSum.Location = New System.Drawing.Point(150, 23)
        Me.chkSum.Name = "chkSum"
        Me.chkSum.Size = New System.Drawing.Size(15, 14)
        Me.chkSum.TabIndex = 1
        Me.chkSum.UseVisualStyleBackColor = True
        '
        'chkCount
        '
        Me.chkCount.AutoSize = True
        Me.chkCount.Enabled = False
        Me.chkCount.Location = New System.Drawing.Point(182, 23)
        Me.chkCount.Name = "chkCount"
        Me.chkCount.Size = New System.Drawing.Size(15, 14)
        Me.chkCount.TabIndex = 2
        Me.chkCount.UseVisualStyleBackColor = True
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(30, 20)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(114, 20)
        Me.txtName.TabIndex = 3
        Me.txtName.Text = ""
        '
        'lblShow
        '
        Me.lblShow.AutoSize = True
        Me.lblShow.Location = New System.Drawing.Point(3, 4)
        Me.lblShow.Name = "lblShow"
        Me.lblShow.Size = New System.Drawing.Size(23, 13)
        Me.lblShow.TabIndex = 4
        Me.lblShow.Text = "Ver"
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New System.Drawing.Point(170, 4)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(42, 13)
        Me.lblCount.TabIndex = 5
        Me.lblCount.Text = "Cuenta"
        '
        'lblSum
        '
        Me.lblSum.AutoSize = True
        Me.lblSum.Location = New System.Drawing.Point(139, 4)
        Me.lblSum.Name = "lblSum"
        Me.lblSum.Size = New System.Drawing.Size(33, 13)
        Me.lblSum.TabIndex = 6
        Me.lblSum.Text = "Suma"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(32, 4)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(44, 13)
        Me.lblName.TabIndex = 7
        Me.lblName.Text = "Nombre"
        '
        'UCDOGenerateExcelVis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.lblSum)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.lblShow)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.chkCount)
        Me.Controls.Add(Me.chkSum)
        Me.Controls.Add(Me.chkIndex)
        Me.Name = "UCDOGenerateExcelVis"
        Me.Size = New System.Drawing.Size(211, 45)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkIndex As System.Windows.Forms.CheckBox
    Friend WithEvents chkSum As System.Windows.Forms.CheckBox
    Friend WithEvents chkCount As System.Windows.Forms.CheckBox
    Friend WithEvents txtName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblShow As ZLabel
    Friend WithEvents lblCount As ZLabel
    Friend WithEvents lblSum As ZLabel
    Friend WithEvents lblName As ZLabel

End Class
