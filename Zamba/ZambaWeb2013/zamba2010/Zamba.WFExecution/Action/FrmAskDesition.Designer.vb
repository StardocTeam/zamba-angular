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
    Friend WithEvents ButtonYES As ZButton
    Friend WithEvents ButtonNO As ZButton
    Friend WithEvents txtQuestion As ZLabel
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAskDesition))
        Me.ButtonYES = New ZButton()
        Me.ButtonNO = New ZButton()
        Me.txtQuestion = New ZLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout
        Me.SuspendLayout
        '
        'ButtonYES
        '
        Me.ButtonYES.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonYES.Location = New System.Drawing.Point(88, 3)
        Me.ButtonYES.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonYES.Name = "ButtonYES"
        Me.ButtonYES.Size = New System.Drawing.Size(100, 28)
        Me.ButtonYES.TabIndex = 0
        Me.ButtonYES.Text = "SI"
        Me.ButtonYES.UseVisualStyleBackColor = true
        '
        'ButtonNO
        '
        Me.ButtonNO.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonNO.Location = New System.Drawing.Point(286, 3)
        Me.ButtonNO.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonNO.Name = "ButtonNO"
        Me.ButtonNO.Size = New System.Drawing.Size(100, 28)
        Me.ButtonNO.TabIndex = 1
        Me.ButtonNO.Text = "NO"
        Me.ButtonNO.UseVisualStyleBackColor = true
        '
        'txtQuestion
        '
        Me.txtQuestion.BackColor = System.Drawing.Color.White
        Me.txtQuestion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtQuestion.ForeColor = System.Drawing.Color.Black
        Me.txtQuestion.Location = New System.Drawing.Point(3, 2)
        Me.txtQuestion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtQuestion.Name = "txtQuestion"
        Me.txtQuestion.Size = New System.Drawing.Size(466, 144)
        Me.txtQuestion.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ButtonNO)
        Me.Panel1.Controls.Add(Me.ButtonYES)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(3, 146)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(466, 35)
        Me.Panel1.TabIndex = 3
        '
        'FrmAskDesition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8!, 16!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(472, 183)
        Me.Controls.Add(Me.txtQuestion)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = false
        Me.Name = "FrmAskDesition"
        Me.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Friend WithEvents Panel1 As Panel
End Class
