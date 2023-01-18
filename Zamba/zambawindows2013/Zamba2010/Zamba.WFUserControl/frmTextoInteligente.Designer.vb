Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmTxtInteligente
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
        Me.txtTextoInteligente = New Zamba.AppBlock.TextoInteligenteTextBox
        Me.lblTextoInteligente = New ZLabel
        Me.btnIngTextoInteligente = New ZButton
        Me.btnCancelTextoInteligenete = New ZButton
        Me.SuspendLayout()
        '
        'txtTextoInteligente
        '
        Me.txtTextoInteligente.Location = New System.Drawing.Point(12, 35)
        Me.txtTextoInteligente.MaxLength = 4000
        Me.txtTextoInteligente.Multiline = False
        Me.txtTextoInteligente.Name = "txtTextoInteligente"
        Me.txtTextoInteligente.Size = New System.Drawing.Size(289, 20)
        Me.txtTextoInteligente.TabIndex = 23
        Me.txtTextoInteligente.Text = ""
        '
        'lblTextoInteligente
        '
        Me.lblTextoInteligente.AutoSize = True
        Me.lblTextoInteligente.Location = New System.Drawing.Point(12, 9)
        Me.lblTextoInteligente.Name = "lblTextoInteligente"
        Me.lblTextoInteligente.Size = New System.Drawing.Size(155, 13)
        Me.lblTextoInteligente.TabIndex = 24
        Me.lblTextoInteligente.Text = "Ingrese el valor de la propiedad"
        '
        'btnIngTextoInteligente
        '
        Me.btnIngTextoInteligente.Location = New System.Drawing.Point(45, 61)
        Me.btnIngTextoInteligente.Name = "btnIngTextoInteligente"
        Me.btnIngTextoInteligente.Size = New System.Drawing.Size(75, 23)
        Me.btnIngTextoInteligente.TabIndex = 25
        Me.btnIngTextoInteligente.Text = "Ingresar"
        Me.btnIngTextoInteligente.UseVisualStyleBackColor = True
        '
        'btnCancelTextoInteligenete
        '
        Me.btnCancelTextoInteligenete.Location = New System.Drawing.Point(190, 61)
        Me.btnCancelTextoInteligenete.Name = "btnCancelTextoInteligenete"
        Me.btnCancelTextoInteligenete.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelTextoInteligenete.TabIndex = 26
        Me.btnCancelTextoInteligenete.Text = "Cancelar"
        Me.btnCancelTextoInteligenete.UseVisualStyleBackColor = True
        '
        'FrmTxtInteligente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(311, 99)
        Me.Controls.Add(Me.btnCancelTextoInteligenete)
        Me.Controls.Add(Me.btnIngTextoInteligente)
        Me.Controls.Add(Me.lblTextoInteligente)
        Me.Controls.Add(Me.txtTextoInteligente)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "FrmTxtInteligente"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zamba"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtTextoInteligente As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblTextoInteligente As ZLabel
    Friend WithEvents btnIngTextoInteligente As ZButton
    Friend WithEvents btnCancelTextoInteligenete As ZButton
End Class
