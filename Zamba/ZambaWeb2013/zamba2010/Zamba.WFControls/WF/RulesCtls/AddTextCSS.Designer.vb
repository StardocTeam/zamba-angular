<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddTextCSS
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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
        Me.txtAddClass = New System.Windows.Forms.TextBox()
        Me.ZLabel1 = New Zamba.AppBlock.ZLabel()
        Me.SuspendLayout()
        '
        'txtAddClass
        '
        Me.txtAddClass.Location = New System.Drawing.Point(12, 28)
        Me.txtAddClass.Name = "txtAddClass"
        Me.txtAddClass.Size = New System.Drawing.Size(497, 20)
        Me.txtAddClass.TabIndex = 0
        '
        'ZLabel1
        '
        Me.ZLabel1.AutoSize = True
        Me.ZLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel1.FontSize = 9.75!
        Me.ZLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel1.Location = New System.Drawing.Point(12, 9)
        Me.ZLabel1.Name = "ZLabel1"
        Me.ZLabel1.Size = New System.Drawing.Size(285, 16)
        Me.ZLabel1.TabIndex = 1
        Me.ZLabel1.Text = "Clase de CSS que desee agregar a la lista"
        Me.ZLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'AddTextCSS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(553, 67)
        Me.Controls.Add(Me.ZLabel1)
        Me.Controls.Add(Me.txtAddClass)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AddTextCSS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtAddClass As TextBox
    Friend WithEvents ZLabel1 As ZLabel
End Class
