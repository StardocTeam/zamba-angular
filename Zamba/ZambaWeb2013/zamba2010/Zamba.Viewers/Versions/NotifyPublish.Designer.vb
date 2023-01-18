<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NotifyPublish
    Inherits Zamba.AppBlock.ZForm

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
        Me.BtnAceptar = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.RdbNoIncluirResumen = New System.Windows.Forms.RadioButton
        Me.RdbIncluirResumen = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.RdbAgregarLink = New System.Windows.Forms.RadioButton
        Me.RdbAdjuntar = New System.Windows.Forms.RadioButton
        Me.TxtBody = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnAceptar
        '
        Me.BtnAceptar.Location = New System.Drawing.Point(321, 245)
        Me.BtnAceptar.Name = "BtnAceptar"
        Me.BtnAceptar.Size = New System.Drawing.Size(75, 23)
        Me.BtnAceptar.TabIndex = 2
        Me.BtnAceptar.Text = "Aceptar"
        Me.BtnAceptar.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RdbNoIncluirResumen)
        Me.GroupBox1.Controls.Add(Me.RdbIncluirResumen)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(393, 47)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Cuerpo del Mensaje"
        '
        'RdbNoIncluirResumen
        '
        Me.RdbNoIncluirResumen.AutoSize = True
        Me.RdbNoIncluirResumen.Location = New System.Drawing.Point(269, 19)
        Me.RdbNoIncluirResumen.Name = "RdbNoIncluirResumen"
        Me.RdbNoIncluirResumen.Size = New System.Drawing.Size(118, 17)
        Me.RdbNoIncluirResumen.TabIndex = 1
        Me.RdbNoIncluirResumen.Text = "No Incluir Resumen"
        Me.RdbNoIncluirResumen.UseVisualStyleBackColor = True
        '
        'RdbIncluirResumen
        '
        Me.RdbIncluirResumen.AutoSize = True
        Me.RdbIncluirResumen.Checked = True
        Me.RdbIncluirResumen.Location = New System.Drawing.Point(111, 19)
        Me.RdbIncluirResumen.Name = "RdbIncluirResumen"
        Me.RdbIncluirResumen.Size = New System.Drawing.Size(101, 17)
        Me.RdbIncluirResumen.TabIndex = 0
        Me.RdbIncluirResumen.TabStop = True
        Me.RdbIncluirResumen.Text = "Incluir Resumen"
        Me.RdbIncluirResumen.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RdbAgregarLink)
        Me.GroupBox2.Controls.Add(Me.RdbAdjuntar)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 190)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(393, 49)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Acceso al Documento"
        '
        'RdbAgregarLink
        '
        Me.RdbAgregarLink.AutoSize = True
        Me.RdbAgregarLink.Checked = True
        Me.RdbAgregarLink.Location = New System.Drawing.Point(111, 19)
        Me.RdbAgregarLink.Name = "RdbAgregarLink"
        Me.RdbAgregarLink.Size = New System.Drawing.Size(141, 17)
        Me.RdbAgregarLink.TabIndex = 1
        Me.RdbAgregarLink.TabStop = True
        Me.RdbAgregarLink.Text = "Adjuntar Link de Acceso"
        Me.RdbAgregarLink.UseVisualStyleBackColor = True
        '
        'RdbAdjuntar
        '
        Me.RdbAdjuntar.AutoSize = True
        Me.RdbAdjuntar.Location = New System.Drawing.Point(269, 19)
        Me.RdbAdjuntar.Name = "RdbAdjuntar"
        Me.RdbAdjuntar.Size = New System.Drawing.Size(122, 17)
        Me.RdbAdjuntar.TabIndex = 0
        Me.RdbAdjuntar.Text = "Adjuntar Documento"
        Me.RdbAdjuntar.UseVisualStyleBackColor = True
        '
        'TxtBody
        '
        Me.TxtBody.Location = New System.Drawing.Point(3, 53)
        Me.TxtBody.Multiline = True
        Me.TxtBody.Name = "TxtBody"
        Me.TxtBody.Size = New System.Drawing.Size(393, 131)
        Me.TxtBody.TabIndex = 1
        '
        'NotifyPublish
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.MenuBar
        Me.ClientSize = New System.Drawing.Size(403, 273)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnAceptar)
        Me.Controls.Add(Me.TxtBody)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "NotifyPublish"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Notifica Publicación"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnAceptar As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RdbNoIncluirResumen As System.Windows.Forms.RadioButton
    Friend WithEvents RdbIncluirResumen As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RdbAgregarLink As System.Windows.Forms.RadioButton
    Friend WithEvents RdbAdjuntar As System.Windows.Forms.RadioButton
    Friend WithEvents TxtBody As System.Windows.Forms.TextBox
End Class
