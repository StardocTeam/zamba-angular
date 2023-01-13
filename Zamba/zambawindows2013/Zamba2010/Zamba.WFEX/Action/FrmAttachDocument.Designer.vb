<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAttachDocument
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
        Me.Adjuntar = New System.Windows.Forms.OpenFileDialog
        Me.txtFullPath = New System.Windows.Forms.TextBox
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.lstFiles = New System.Windows.Forms.ListBox
        Me.btnAccept = New System.Windows.Forms.Button
        Me.lbltotalsize = New System.Windows.Forms.Label
        Me.lblLimit = New System.Windows.Forms.Label
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnClearList = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Adjuntar
        '
        Me.Adjuntar.Multiselect = True
        '
        'txtFullPath
        '
        Me.txtFullPath.Location = New System.Drawing.Point(12, 12)
        Me.txtFullPath.Name = "txtFullPath"
        Me.txtFullPath.Size = New System.Drawing.Size(370, 20)
        Me.txtFullPath.TabIndex = 0
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(388, 10)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowse.TabIndex = 1
        Me.btnBrowse.Text = "Examinar"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'lstFiles
        '
        Me.lstFiles.FormattingEnabled = True
        Me.lstFiles.HorizontalScrollbar = True
        Me.lstFiles.Location = New System.Drawing.Point(12, 48)
        Me.lstFiles.Name = "lstFiles"
        Me.lstFiles.Size = New System.Drawing.Size(370, 147)
        Me.lstFiles.TabIndex = 2
        '
        'btnAccept
        '
        Me.btnAccept.Location = New System.Drawing.Point(389, 205)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(75, 23)
        Me.btnAccept.TabIndex = 3
        Me.btnAccept.Text = "Aceptar"
        Me.btnAccept.UseVisualStyleBackColor = True
        '
        'lbltotalsize
        '
        Me.lbltotalsize.AutoSize = True
        Me.lbltotalsize.Location = New System.Drawing.Point(12, 210)
        Me.lbltotalsize.Name = "lbltotalsize"
        Me.lbltotalsize.Size = New System.Drawing.Size(133, 13)
        Me.lbltotalsize.TabIndex = 4
        Me.lbltotalsize.Text = "Tamaño de adjuntos: 0 KB"
        '
        'lblLimit
        '
        Me.lblLimit.AutoSize = True
        Me.lblLimit.Location = New System.Drawing.Point(250, 210)
        Me.lblLimit.Name = "lblLimit"
        Me.lblLimit.Size = New System.Drawing.Size(54, 13)
        Me.lblLimit.TabIndex = 5
        Me.lblLimit.Text = "Limite KB:"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(389, 48)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 6
        Me.btnAdd.Text = "Agregar"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(388, 77)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Text = "Quitar"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnClearList
        '
        Me.btnClearList.Location = New System.Drawing.Point(388, 134)
        Me.btnClearList.Name = "btnClearList"
        Me.btnClearList.Size = New System.Drawing.Size(75, 23)
        Me.btnClearList.TabIndex = 8
        Me.btnClearList.Text = "Borrar Lista"
        Me.btnClearList.UseVisualStyleBackColor = True
        '
        'FrmAttachDocument
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 240)
        Me.Controls.Add(Me.btnClearList)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lblLimit)
        Me.Controls.Add(Me.lbltotalsize)
        Me.Controls.Add(Me.btnAccept)
        Me.Controls.Add(Me.lstFiles)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.txtFullPath)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAttachDocument"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Adjuntar archivo a documento"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Adjuntar As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtFullPath As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents lstFiles As System.Windows.Forms.ListBox
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents lbltotalsize As System.Windows.Forms.Label
    Friend WithEvents lblLimit As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnClearList As System.Windows.Forms.Button
End Class
