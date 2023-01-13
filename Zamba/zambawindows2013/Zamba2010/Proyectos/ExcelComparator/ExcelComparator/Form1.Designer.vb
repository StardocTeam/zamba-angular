<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExcelCompare
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtExcel1 = New System.Windows.Forms.TextBox()
        Me.txtExcel2 = New System.Windows.Forms.TextBox()
        Me.btnExcel1 = New System.Windows.Forms.Button()
        Me.btnExcel2 = New System.Windows.Forms.Button()
        Me.btnCompare = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Excel 1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Excel 2"
        '
        'txtExcel1
        '
        Me.txtExcel1.Location = New System.Drawing.Point(78, 9)
        Me.txtExcel1.Name = "txtExcel1"
        Me.txtExcel1.Size = New System.Drawing.Size(260, 20)
        Me.txtExcel1.TabIndex = 2
        '
        'txtExcel2
        '
        Me.txtExcel2.Location = New System.Drawing.Point(78, 39)
        Me.txtExcel2.Name = "txtExcel2"
        Me.txtExcel2.Size = New System.Drawing.Size(260, 20)
        Me.txtExcel2.TabIndex = 3
        '
        'btnExcel1
        '
        Me.btnExcel1.Location = New System.Drawing.Point(345, 9)
        Me.btnExcel1.Name = "btnExcel1"
        Me.btnExcel1.Size = New System.Drawing.Size(30, 20)
        Me.btnExcel1.TabIndex = 4
        Me.btnExcel1.Text = "..."
        Me.btnExcel1.UseVisualStyleBackColor = True
        '
        'btnExcel2
        '
        Me.btnExcel2.Location = New System.Drawing.Point(345, 39)
        Me.btnExcel2.Name = "btnExcel2"
        Me.btnExcel2.Size = New System.Drawing.Size(30, 20)
        Me.btnExcel2.TabIndex = 5
        Me.btnExcel2.Text = "..."
        Me.btnExcel2.UseVisualStyleBackColor = True
        '
        'btnCompare
        '
        Me.btnCompare.Location = New System.Drawing.Point(144, 80)
        Me.btnCompare.Name = "btnCompare"
        Me.btnCompare.Size = New System.Drawing.Size(103, 23)
        Me.btnCompare.TabIndex = 6
        Me.btnCompare.Text = "Comparar"
        Me.btnCompare.UseVisualStyleBackColor = True
        '
        'ExcelCompare
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(380, 115)
        Me.Controls.Add(Me.btnCompare)
        Me.Controls.Add(Me.btnExcel2)
        Me.Controls.Add(Me.btnExcel1)
        Me.Controls.Add(Me.txtExcel2)
        Me.Controls.Add(Me.txtExcel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.Name = "ExcelCompare"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Comparador de Excel"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtExcel1 As System.Windows.Forms.TextBox
    Friend WithEvents txtExcel2 As System.Windows.Forms.TextBox
    Friend WithEvents btnExcel1 As System.Windows.Forms.Button
    Friend WithEvents btnExcel2 As System.Windows.Forms.Button
    Friend WithEvents btnCompare As System.Windows.Forms.Button

End Class
