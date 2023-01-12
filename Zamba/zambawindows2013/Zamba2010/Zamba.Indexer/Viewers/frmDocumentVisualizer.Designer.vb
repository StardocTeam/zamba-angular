<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDocumentVisualizer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDocumentVisualizer))
        Me.tbPages = New System.Windows.Forms.TabControl
        Me.tpPrincipal = New System.Windows.Forms.TabPage
        Me.tbPages.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbPages
        '
        Me.tbPages.Controls.Add(Me.tpPrincipal)
        Me.tbPages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbPages.Location = New System.Drawing.Point(0, 0)
        Me.tbPages.Name = "tbPages"
        Me.tbPages.SelectedIndex = 0
        Me.tbPages.Size = New System.Drawing.Size(284, 262)
        Me.tbPages.TabIndex = 0
        '
        'tpPrincipal
        '
        Me.tpPrincipal.Location = New System.Drawing.Point(4, 22)
        Me.tpPrincipal.Name = "tpPrincipal"
        Me.tpPrincipal.Padding = New System.Windows.Forms.Padding(3)
        Me.tpPrincipal.Size = New System.Drawing.Size(276, 236)
        Me.tpPrincipal.TabIndex = 0
        Me.tpPrincipal.Text = "Principal"
        Me.tpPrincipal.UseVisualStyleBackColor = True
        '
        'frmDocumentVisualizer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.tbPages)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDocumentVisualizer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Visualizador de Documentos"
        Me.tbPages.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbPages As System.Windows.Forms.TabControl
    Friend WithEvents tpPrincipal As System.Windows.Forms.TabPage
End Class
