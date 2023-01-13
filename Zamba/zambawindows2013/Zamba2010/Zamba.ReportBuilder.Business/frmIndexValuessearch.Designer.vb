Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmIndexValueSearch
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.cmbIndexValue = New System.Windows.Forms.ComboBox
        Me.btnAcept = New ZButton
        Me.Label1 = New ZLabel
        Me.txtValueCode = New System.Windows.Forms.TextBox
        Me.Label2 = New ZLabel
        Me.SuspendLayout()
        '
        'cmbIndexValue
        '
        Me.cmbIndexValue.FormattingEnabled = True
        Me.cmbIndexValue.Location = New System.Drawing.Point(114, 56)
        Me.cmbIndexValue.Name = "cmbIndexValue"
        Me.cmbIndexValue.Size = New System.Drawing.Size(249, 21)
        Me.cmbIndexValue.TabIndex = 0
        Me.cmbIndexValue.Tag = "Seleccione un valor de la lista para asignarlo al atributo"
        '
        'btnAcept
        '
        Me.btnAcept.Location = New System.Drawing.Point(288, 125)
        Me.btnAcept.Name = "btnAcept"
        Me.btnAcept.Size = New System.Drawing.Size(75, 23)
        Me.btnAcept.TabIndex = 1
        Me.btnAcept.Text = "Aceptar"
        Me.btnAcept.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Código"
        '
        'txtValueCode
        '
        Me.txtValueCode.Location = New System.Drawing.Point(21, 57)
        Me.txtValueCode.Name = "txtValueCode"
        Me.txtValueCode.Size = New System.Drawing.Size(62, 20)
        Me.txtValueCode.TabIndex = 3
        Me.txtValueCode.Tag = "Escriba el codigo del valor para acceder de forma directa a el"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(111, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Valor"
        '
        'frmIndexValueSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(386, 160)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtValueCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnAcept)
        Me.Controls.Add(Me.cmbIndexValue)
        Me.MaximizeBox = False
        Me.Name = "frmIndexValueSearch"
        Me.Text = "Búsqueda de Atributo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbIndexValue As System.Windows.Forms.ComboBox
    Friend WithEvents btnAcept As ZButton
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents txtValueCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As ZLabel
End Class
