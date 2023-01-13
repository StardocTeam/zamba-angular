<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BarcodeViewer
    Inherits Zamba.AppBlock.ZForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BarcodeViewer))
        Me.cmbItems = New System.Windows.Forms.ComboBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblTitulo = New ZLabel()
        Me.btnCancelar = New ZButton()
        Me.btnAceptar = New ZButton()
        Me.Label1 = New ZLabel()
        Me.Label2 = New ZLabel()
        Me.CmbAlignment = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'cmbItems
        '
        Me.cmbItems.BackColor = System.Drawing.SystemColors.Window
        Me.cmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItems.Location = New System.Drawing.Point(14, 24)
        Me.cmbItems.Name = "cmbItems"
        Me.cmbItems.Size = New System.Drawing.Size(250, 21)
        Me.cmbItems.TabIndex = 8
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(15, 73)
        Me.txtName.MaxLength = 16
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(248, 21)
        Me.txtName.TabIndex = 4
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Location = New System.Drawing.Point(11, 8)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(51, 13)
        Me.lblTitulo.TabIndex = 7
        Me.lblTitulo.Text = "Atributos"
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(144, 157)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(79, 27)
        Me.btnCancelar.TabIndex = 6
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(43, 157)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(79, 27)
        Me.btnAceptar.TabIndex = 5
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Texto"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 109)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Alineamiento"
        '
        'CmbAlignment
        '
        Me.CmbAlignment.BackColor = System.Drawing.SystemColors.Window
        Me.CmbAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbAlignment.Location = New System.Drawing.Point(14, 125)
        Me.CmbAlignment.Name = "CmbAlignment"
        Me.CmbAlignment.Size = New System.Drawing.Size(250, 21)
        Me.CmbAlignment.TabIndex = 11
        '
        'BarcodeViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(274, 201)
        Me.Controls.Add(Me.CmbAlignment)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbItems)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BarcodeViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Elija Barcode"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents cmbItems As System.Windows.Forms.ComboBox
    Private WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents lblTitulo As ZLabel
    Private WithEvents btnCancelar As ZButton
    Private WithEvents btnAceptar As ZButton
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Private WithEvents CmbAlignment As System.Windows.Forms.ComboBox
End Class
