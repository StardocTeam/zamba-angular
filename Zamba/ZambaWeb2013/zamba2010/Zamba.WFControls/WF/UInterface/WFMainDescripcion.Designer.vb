<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WFMainDescripcion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WFMainDescripcion))
        Me.lblDescripciòn = New ZLabel()
        Me.lblAyuda = New ZLabel()
        Me.txtAyuda = New System.Windows.Forms.TextBox()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.btnGuardar = New Zamba.AppBlock.ZButton()
        Me.btnCancelar = New Zamba.AppBlock.ZButton()
        Me.SuspendLayout()
        '
        'lblDescripciòn
        '
        Me.lblDescripciòn.AutoSize = True
        Me.lblDescripciòn.Location = New System.Drawing.Point(13, 13)
        Me.lblDescripciòn.Name = "lblDescripciòn"
        Me.lblDescripciòn.Size = New System.Drawing.Size(63, 13)
        Me.lblDescripciòn.TabIndex = 0
        Me.lblDescripciòn.Text = "Descripcion"
        '
        'lblAyuda
        '
        Me.lblAyuda.AutoSize = True
        Me.lblAyuda.Location = New System.Drawing.Point(13, 168)
        Me.lblAyuda.Name = "lblAyuda"
        Me.lblAyuda.Size = New System.Drawing.Size(37, 13)
        Me.lblAyuda.TabIndex = 1
        Me.lblAyuda.Text = "Ayuda"
        '
        'txtAyuda
        '
        Me.txtAyuda.Location = New System.Drawing.Point(12, 184)
        Me.txtAyuda.Multiline = True
        Me.txtAyuda.Name = "txtAyuda"
        Me.txtAyuda.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAyuda.Size = New System.Drawing.Size(314, 121)
        Me.txtAyuda.TabIndex = 4
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Location = New System.Drawing.Point(12, 29)
        Me.txtDescripcion.Multiline = True
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescripcion.Size = New System.Drawing.Size(314, 124)
        Me.txtDescripcion.TabIndex = 5
        '
        'btnGuardar
        '
        Me.btnGuardar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnGuardar.Location = New System.Drawing.Point(41, 318)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(112, 26)
        Me.btnGuardar.TabIndex = 6
        Me.btnGuardar.Text = "Guardar"
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnCancelar.Location = New System.Drawing.Point(181, 318)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(112, 26)
        Me.btnCancelar.TabIndex = 6
        Me.btnCancelar.Text = "Cancelar"
        '
        'WFMainDescripcion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(338, 356)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.txtAyuda)
        Me.Controls.Add(Me.lblAyuda)
        Me.Controls.Add(Me.lblDescripciòn)
        Me.ForeColor = Zamba.AppBlock.ZambaUIHelpers.GetFontsColor
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WFMainDescripcion"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RightToLeftLayout = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblDescripciòn As ZLabel
    Friend WithEvents lblAyuda As ZLabel
    Friend WithEvents txtAyuda As System.Windows.Forms.TextBox
    Friend WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents btnGuardar As Zamba.AppBlock.ZButton
    Friend WithEvents btnCancelar As Zamba.AppBlock.ZButton
End Class
