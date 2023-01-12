<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNotifyPublish
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
        Me.BtnAceptar = New Zamba.AppBlock.ZButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RdbNoIncluirResumen = New System.Windows.Forms.RadioButton()
        Me.RdbIncluirResumen = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RdbAgregarLink = New System.Windows.Forms.RadioButton()
        Me.RdbAdjuntar = New System.Windows.Forms.RadioButton()
        Me.TxtBody = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout
        Me.GroupBox2.SuspendLayout
        Me.SuspendLayout
        '
        'BtnAceptar
        '
        Me.BtnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(157,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.BtnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAceptar.ForeColor = System.Drawing.Color.White
        Me.BtnAceptar.Location = New System.Drawing.Point(428, 302)
        Me.BtnAceptar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnAceptar.Name = "BtnAceptar"
        Me.BtnAceptar.Size = New System.Drawing.Size(100, 28)
        Me.BtnAceptar.TabIndex = 2
        Me.BtnAceptar.Text = "Aceptar"
        Me.BtnAceptar.UseVisualStyleBackColor = true
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RdbNoIncluirResumen)
        Me.GroupBox1.Controls.Add(Me.RdbIncluirResumen)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 0)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(524, 58)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Cuerpo del Mensaje"
        '
        'RdbNoIncluirResumen
        '
        Me.RdbNoIncluirResumen.AutoSize = true
        Me.RdbNoIncluirResumen.Location = New System.Drawing.Point(359, 23)
        Me.RdbNoIncluirResumen.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RdbNoIncluirResumen.Name = "RdbNoIncluirResumen"
        Me.RdbNoIncluirResumen.Size = New System.Drawing.Size(151, 20)
        Me.RdbNoIncluirResumen.TabIndex = 1
        Me.RdbNoIncluirResumen.Text = "No Incluir Resumen"
        Me.RdbNoIncluirResumen.UseVisualStyleBackColor = true
        '
        'RdbIncluirResumen
        '
        Me.RdbIncluirResumen.AutoSize = true
        Me.RdbIncluirResumen.Checked = true
        Me.RdbIncluirResumen.Location = New System.Drawing.Point(148, 23)
        Me.RdbIncluirResumen.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RdbIncluirResumen.Name = "RdbIncluirResumen"
        Me.RdbIncluirResumen.Size = New System.Drawing.Size(129, 20)
        Me.RdbIncluirResumen.TabIndex = 0
        Me.RdbIncluirResumen.TabStop = true
        Me.RdbIncluirResumen.Text = "Incluir Resumen"
        Me.RdbIncluirResumen.UseVisualStyleBackColor = true
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RdbAgregarLink)
        Me.GroupBox2.Controls.Add(Me.RdbAdjuntar)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 234)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(524, 60)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = "Acceso al Documento"
        '
        'RdbAgregarLink
        '
        Me.RdbAgregarLink.AutoSize = true
        Me.RdbAgregarLink.Checked = true
        Me.RdbAgregarLink.Location = New System.Drawing.Point(148, 23)
        Me.RdbAgregarLink.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RdbAgregarLink.Name = "RdbAgregarLink"
        Me.RdbAgregarLink.Size = New System.Drawing.Size(186, 20)
        Me.RdbAgregarLink.TabIndex = 1
        Me.RdbAgregarLink.TabStop = true
        Me.RdbAgregarLink.Text = "Adjuntar Link de Acceso"
        Me.RdbAgregarLink.UseVisualStyleBackColor = true
        '
        'RdbAdjuntar
        '
        Me.RdbAdjuntar.AutoSize = true
        Me.RdbAdjuntar.Location = New System.Drawing.Point(359, 23)
        Me.RdbAdjuntar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RdbAdjuntar.Name = "RdbAdjuntar"
        Me.RdbAdjuntar.Size = New System.Drawing.Size(161, 20)
        Me.RdbAdjuntar.TabIndex = 0
        Me.RdbAdjuntar.Text = "Adjuntar Documento"
        Me.RdbAdjuntar.UseVisualStyleBackColor = true
        '
        'TxtBody
        '
        Me.TxtBody.Location = New System.Drawing.Point(4, 65)
        Me.TxtBody.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtBody.Multiline = true
        Me.TxtBody.Name = "TxtBody"
        Me.TxtBody.Size = New System.Drawing.Size(523, 160)
        Me.TxtBody.TabIndex = 1
        '
        'NotifyPublish
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8!, 16!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(537, 336)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnAceptar)
        Me.Controls.Add(Me.TxtBody)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = false
        Me.Name = "NotifyPublish"
        Me.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Notifica Publicación"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents BtnAceptar As ZButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RdbNoIncluirResumen As System.Windows.Forms.RadioButton
    Friend WithEvents RdbIncluirResumen As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RdbAgregarLink As System.Windows.Forms.RadioButton
    Friend WithEvents RdbAdjuntar As System.Windows.Forms.RadioButton
    Friend WithEvents TxtBody As System.Windows.Forms.TextBox
End Class
