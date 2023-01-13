'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Imports System.Windows.Forms
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Zamba.Indexs
Imports System.Text
Partial Class FrmAskDesition
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
    Friend WithEvents ButtonYES As System.Windows.Forms.Button
    Friend WithEvents ButtonNO As System.Windows.Forms.Button
    Friend WithEvents txtQuestion As System.Windows.Forms.TextBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAskDesition))
        Me.ButtonYES = New System.Windows.Forms.Button
        Me.ButtonNO = New System.Windows.Forms.Button
        Me.txtQuestion = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'ButtonYES
        '
        Me.ButtonYES.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonYES.Location = New System.Drawing.Point(70, 118)
        Me.ButtonYES.Name = "ButtonYES"
        Me.ButtonYES.Size = New System.Drawing.Size(75, 23)
        Me.ButtonYES.TabIndex = 0
        Me.ButtonYES.Text = "SI"
        Me.ButtonYES.UseVisualStyleBackColor = True
        '
        'ButtonNO
        '
        Me.ButtonNO.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonNO.Location = New System.Drawing.Point(210, 118)
        Me.ButtonNO.Name = "ButtonNO"
        Me.ButtonNO.Size = New System.Drawing.Size(75, 23)
        Me.ButtonNO.TabIndex = 1
        Me.ButtonNO.Text = "NO"
        Me.ButtonNO.UseVisualStyleBackColor = True
        '
        'txtQuestion
        '
        Me.txtQuestion.AcceptsReturn = True
        Me.txtQuestion.AcceptsTab = True
        Me.txtQuestion.BackColor = System.Drawing.Color.White
        Me.txtQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQuestion.Location = New System.Drawing.Point(12, 12)
        Me.txtQuestion.Multiline = True
        Me.txtQuestion.Name = "txtQuestion"
        Me.txtQuestion.ReadOnly = True
        Me.txtQuestion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtQuestion.Size = New System.Drawing.Size(330, 100)
        Me.txtQuestion.TabIndex = 2
        '
        'FrmAskDesition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(354, 149)
        Me.Controls.Add(Me.txtQuestion)
        Me.Controls.Add(Me.ButtonNO)
        Me.Controls.Add(Me.ButtonYES)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmAskDesition"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
End Class
