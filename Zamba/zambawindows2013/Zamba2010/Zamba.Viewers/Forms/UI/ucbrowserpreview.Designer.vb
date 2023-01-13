<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucbrowserpreview
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucbrowserpreview))
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.ZToolBar1 = New Zamba.AppBlock.ZToolBar()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnmaximize = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btmrefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.btnopenexternal = New System.Windows.Forms.ToolStripButton()
        Me.ZToolBar1.SuspendLayout()
        Me.SuspendLayout()
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(0, 29)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(752, 229)
        Me.WebBrowser1.TabIndex = 0
        '
        'ZToolBar1
        '
        Me.ZToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ZToolBar1.ImageScalingSize = New System.Drawing.Size(22, 22)
        Me.ZToolBar1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator2, Me.btnmaximize, Me.ToolStripSeparator1, Me.btmrefresh, Me.ToolStripLabel1, Me.btnopenexternal})
        Me.ZToolBar1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.ZToolBar1.Location = New System.Drawing.Point(0, 0)
        Me.ZToolBar1.Name = "ZToolBar1"
        Me.ZToolBar1.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.ZToolBar1.Size = New System.Drawing.Size(752, 29)
        Me.ZToolBar1.TabIndex = 0
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 29)
        '
        'btnmaximize
        '
        Me.btnmaximize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnmaximize.Image = Global.Zamba.Viewers.My.Resources.Resources._1446231902_Full_Screen
        Me.btnmaximize.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnmaximize.Name = "btnmaximize"
        Me.btnmaximize.RightToLeftAutoMirrorImage = True
        Me.btnmaximize.Size = New System.Drawing.Size(26, 26)
        Me.btnmaximize.Text = "Maximizar"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 29)
        '
        'btmrefresh
        '
        Me.btmrefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btmrefresh.Image = Global.Zamba.Viewers.My.Resources.Resources._1446232001_Synchronize
        Me.btmrefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btmrefresh.Name = "btmrefresh"
        Me.btmrefresh.Size = New System.Drawing.Size(26, 26)
        Me.btmrefresh.Text = "Actualizar"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ToolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(91, 26)
        Me.ToolStripLabel1.Text = "Previsualizacion"
        Me.ToolStripLabel1.ToolTipText = "Previsualizar Adjunto de la Tarea"
        '
        'btnopenexternal
        '
        Me.btnopenexternal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnopenexternal.ForeColor = System.Drawing.Color.RoyalBlue
        Me.btnopenexternal.Image = CType(resources.GetObject("btnopenexternal.Image"), System.Drawing.Image)
        Me.btnopenexternal.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnopenexternal.Name = "btnopenexternal"
        Me.btnopenexternal.Size = New System.Drawing.Size(23, 26)
        Me.btnopenexternal.ToolTipText = "Abrir por fuera"
        '
        'ucbrowserpreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.WebBrowser1)
        Me.Controls.Add(Me.ZToolBar1)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Name = "ucbrowserpreview"
        Me.Size = New System.Drawing.Size(752, 258)
        Me.ZToolBar1.ResumeLayout(False)
        Me.ZToolBar1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents ZToolBar1 As ZToolBar
    Friend WithEvents btnmaximize As System.Windows.Forms.ToolStripButton
    Friend WithEvents btmrefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnopenexternal As ToolStripButton
End Class
