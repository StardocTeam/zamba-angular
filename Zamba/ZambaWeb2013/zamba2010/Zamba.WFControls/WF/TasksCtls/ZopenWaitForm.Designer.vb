<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZopenWaitForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZopenWaitForm))
        Me.Panel1 = New Zamba.AppBlock.ZPanel()
        Me.ZLabel1 = New Zamba.AppBlock.ZLabel()
        Me.btnCancel = New Zamba.AppBlock.ZButton()
        Me.btnOk = New Zamba.AppBlock.ZButton()
        Me.Label4 = New Zamba.AppBlock.ZLabel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.RadListView1 = New Telerik.WinControls.UI.RadListView()
        Me.Panel2 = New Zamba.AppBlock.ZPanel()
        Me.ZLabel2 = New Zamba.AppBlock.ZLabel()
        Me.Panel3 = New Zamba.AppBlock.ZPanel()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadListView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.ZLabel1)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.btnOk)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(689, 137)
        Me.Panel1.TabIndex = 4
        '
        'ZLabel1
        '
        Me.ZLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel1.FontSize = 9.75!
        Me.ZLabel1.ForeColor = System.Drawing.Color.Black
        Me.ZLabel1.Location = New System.Drawing.Point(89, 46)
        Me.ZLabel1.Name = "ZLabel1"
        Me.ZLabel1.Size = New System.Drawing.Size(504, 49)
        Me.ZLabel1.TabIndex = 4
        Me.ZLabel1.Text = "Queres ejecutarlas ahora?"
        Me.ZLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(385, 98)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 25)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "NO"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Location = New System.Drawing.Point(216, 98)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(82, 25)
        Me.btnOk.TabIndex = 1
        Me.btnOk.Text = "SI"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label4.FontSize = 9.75!
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(89, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(504, 49)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Se han encontrado tareas programadas, pendientes de ejecucion."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(133, 52)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(221, 20)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'RadListView1
        '
        Me.RadListView1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.RadListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadListView1.Location = New System.Drawing.Point(0, 0)
        Me.RadListView1.Name = "RadListView1"
        '
        '
        '
        Me.RadListView1.RootElement.ControlBounds = New System.Drawing.Rectangle(0, 0, 120, 95)
        Me.RadListView1.Size = New System.Drawing.Size(689, 0)
        Me.RadListView1.TabIndex = 5
        Me.RadListView1.Text = "RadListView1"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.Panel2.Controls.Add(Me.ZLabel2)
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Panel2.Location = New System.Drawing.Point(0, 137)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(689, 84)
        Me.Panel2.TabIndex = 6
        Me.Panel2.Visible = False
        '
        'ZLabel2
        '
        Me.ZLabel2.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel2.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel2.FontSize = 9.75!
        Me.ZLabel2.ForeColor = System.Drawing.Color.Black
        Me.ZLabel2.Location = New System.Drawing.Point(0, 0)
        Me.ZLabel2.Name = "ZLabel2"
        Me.ZLabel2.Size = New System.Drawing.Size(504, 49)
        Me.ZLabel2.TabIndex = 5
        Me.ZLabel2.Text = "Ejecutando Tareas..."
        Me.ZLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.Panel3.Controls.Add(Me.RadListView1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Panel3.Location = New System.Drawing.Point(0, 221)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(689, 0)
        Me.Panel3.TabIndex = 7
        Me.Panel3.Visible = False
        '
        'ZopenWaitForm
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(689, 135)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ZopenWaitForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadListView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ZLabel1 As ZLabel
    Friend WithEvents btnCancel As ZButton
    Friend WithEvents btnOk As ZButton
    Friend WithEvents Label4 As ZLabel
    Public WithEvents Panel1 As ZPanel
    Public WithEvents Panel2 As ZPanel
    Friend WithEvents ZLabel2 As ZLabel
    Public WithEvents Panel3 As ZPanel
    Private WithEvents RadListView1 As Telerik.WinControls.UI.RadListView
End Class
