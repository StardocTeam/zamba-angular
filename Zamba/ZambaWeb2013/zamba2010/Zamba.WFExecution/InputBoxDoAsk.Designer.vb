<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InputBoxDoAsk
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
        Me.txtinput = New System.Windows.Forms.TextBox()
        Me.lblmensaje = New ZLabel()
        Me.Accept = New ZButton()
        Me.lblmsj = New ZLabel()
        Me.btncancel = New ZButton()
        Me.lstValue = New System.Windows.Forms.ListBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtinput
        '
        Me.txtinput.BackColor = System.Drawing.Color.White
        Me.txtinput.Location = New System.Drawing.Point(20, 62)
        Me.txtinput.Margin = New System.Windows.Forms.Padding(4)
        Me.txtinput.Multiline = True
        Me.txtinput.Name = "txtinput"
        Me.txtinput.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtinput.Size = New System.Drawing.Size(571, 75)
        Me.txtinput.TabIndex = 2
        '
        'lblmensaje
        '
        Me.lblmensaje.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblmensaje.Location = New System.Drawing.Point(3, 2)
        Me.lblmensaje.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblmensaje.Name = "lblmensaje"
        Me.lblmensaje.Size = New System.Drawing.Size(659, 47)
        Me.lblmensaje.TabIndex = 3
        '
        'Accept
        '
        Me.Accept.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Accept.Location = New System.Drawing.Point(156, 14)
        Me.Accept.Margin = New System.Windows.Forms.Padding(4)
        Me.Accept.Name = "Accept"
        Me.Accept.Size = New System.Drawing.Size(120, 28)
        Me.Accept.TabIndex = 4
        Me.Accept.Text = "Aceptar"
        Me.Accept.UseVisualStyleBackColor = True
        '
        'lblmsj
        '
        Me.lblmsj.AutoSize = True
        Me.lblmsj.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblmsj.Location = New System.Drawing.Point(41, 28)
        Me.lblmsj.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblmsj.Name = "lblmsj"
        Me.lblmsj.Size = New System.Drawing.Size(0, 16)
        Me.lblmsj.TabIndex = 6
        '
        'btncancel
        '
        Me.btncancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancel.Location = New System.Drawing.Point(373, 14)
        Me.btncancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(120, 28)
        Me.btncancel.TabIndex = 7
        Me.btncancel.Text = "Cancelar"
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'lstValue
        '
        Me.lstValue.BackColor = System.Drawing.Color.White
        Me.lstValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstValue.ForeColor = System.Drawing.Color.Black
        Me.lstValue.FormattingEnabled = True
        Me.lstValue.ItemHeight = 16
        Me.lstValue.Location = New System.Drawing.Point(3, 49)
        Me.lstValue.Margin = New System.Windows.Forms.Padding(4)
        Me.lstValue.Name = "lstValue"
        Me.lstValue.Size = New System.Drawing.Size(659, 229)
        Me.lstValue.TabIndex = 8
        Me.lstValue.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Accept)
        Me.Panel1.Controls.Add(Me.btncancel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(3, 224)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(659, 54)
        Me.Panel1.TabIndex = 9
        '
        'InputBoxDoAsk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(665, 280)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lstValue)
        Me.Controls.Add(Me.lblmsj)
        Me.Controls.Add(Me.lblmensaje)
        Me.Controls.Add(Me.txtinput)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InputBoxDoAsk"
        Me.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ""
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtinput As System.Windows.Forms.TextBox
    Friend WithEvents lblmensaje As ZLabel
    Friend WithEvents Accept As ZButton
    Friend WithEvents lblmsj As ZLabel
    Friend WithEvents btncancel As ZButton
    Friend WithEvents lstValue As System.Windows.Forms.ListBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
