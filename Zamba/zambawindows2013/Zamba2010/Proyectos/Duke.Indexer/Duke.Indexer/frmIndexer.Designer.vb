<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIndexer
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
        Me.btnIndexar = New System.Windows.Forms.Button
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblIndexedDocs = New System.Windows.Forms.Label
        Me.lblDocsToIndex = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnIndexar
        '
        Me.btnIndexar.Location = New System.Drawing.Point(93, 12)
        Me.btnIndexar.Name = "btnIndexar"
        Me.btnIndexar.Size = New System.Drawing.Size(75, 23)
        Me.btnIndexar.TabIndex = 0
        Me.btnIndexar.Text = "Indexar"
        Me.btnIndexar.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 41)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(237, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(41, 116)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(179, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Cantidad de Documentos Indexados"
        '
        'lblIndexedDocs
        '
        Me.lblIndexedDocs.AutoSize = True
        Me.lblIndexedDocs.Location = New System.Drawing.Point(124, 139)
        Me.lblIndexedDocs.Name = "lblIndexedDocs"
        Me.lblIndexedDocs.Size = New System.Drawing.Size(13, 13)
        Me.lblIndexedDocs.TabIndex = 6
        Me.lblIndexedDocs.Text = "0"
        '
        'lblDocsToIndex
        '
        Me.lblDocsToIndex.AutoSize = True
        Me.lblDocsToIndex.Location = New System.Drawing.Point(124, 97)
        Me.lblDocsToIndex.Name = "lblDocsToIndex"
        Me.lblDocsToIndex.Size = New System.Drawing.Size(13, 13)
        Me.lblDocsToIndex.TabIndex = 9
        Me.lblDocsToIndex.Text = "0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(41, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(174, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Cantidad de Documentos a Indexar"
        '
        'frmIndexer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(261, 161)
        Me.Controls.Add(Me.lblDocsToIndex)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblIndexedDocs)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.btnIndexar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmIndexer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Indexador de documentos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnIndexar As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblIndexedDocs As System.Windows.Forms.Label
    Friend WithEvents lblDocsToIndex As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
