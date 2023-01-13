'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Imports System.Windows.Forms
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Zamba.Indexs
Imports System.Text
Partial Class FrmInputText
    Inherits ZForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInputText))
        Me.Accept = New ZButton
        Me.Cancel = New ZButton
        Me.txtCustomItem = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Accept
        '
        Me.Accept.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Accept.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Accept.Location = New System.Drawing.Point(56, 164)
        Me.Accept.Name = "Accept"
        Me.Accept.Size = New System.Drawing.Size(90, 23)
        Me.Accept.TabIndex = 1
        Me.Accept.Text = "Aceptar"
        Me.Accept.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel.Location = New System.Drawing.Point(165, 164)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(87, 23)
        Me.Cancel.TabIndex = 3
        Me.Cancel.Text = "Cancelar"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'txtCustomItem
        '
        Me.txtCustomItem.Location = New System.Drawing.Point(12, 12)
        Me.txtCustomItem.Multiline = True
        Me.txtCustomItem.Name = "txtCustomItem"
        Me.txtCustomItem.Size = New System.Drawing.Size(285, 131)
        Me.txtCustomItem.TabIndex = 4
        '
        'FrmInputText
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(309, 199)
        Me.ControlBox = False
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Accept)
        Me.Controls.Add(Me.txtCustomItem)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmInputText"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ingresar Comentario"
        'Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Accept As ZButton
    Friend WithEvents Cancel As ZButton
    Friend WithEvents txtCustomItem As System.Windows.Forms.TextBox
End Class
