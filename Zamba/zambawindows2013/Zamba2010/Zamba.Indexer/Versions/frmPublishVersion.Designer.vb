<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPublishVersion
    Inherits Zamba.AppBlock.ZForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If



            'Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
            'Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
            'Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
            'Friend WithEvents Label2 As ZLabel
            'Friend WithEvents BntPublish As ZButton
            'Friend WithEvents PanelIndex As System.Windows.Forms.Panel
            'Friend WithEvents BtnNotify As ZButton
            'Friend WithEvents BtnAddUsers As ZButton
            'Friend WithEvents lblCreador As ZLabel
            'Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblCreador = New Zamba.AppBlock.ZLabel()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.BntPublish = New Zamba.AppBlock.ZButton()
        Me.PanelIndex = New System.Windows.Forms.Panel()
        Me.BtnNotify = New Zamba.AppBlock.ZButton()
        Me.BtnAddUsers = New Zamba.AppBlock.ZButton()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.GroupBox1.SuspendLayout
        Me.GroupBox2.SuspendLayout
        Me.SuspendLayout
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.GroupBox1.Location = New System.Drawing.Point(16, 106)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(475, 121)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Comentario"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(4, 20)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Multiline = true
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = true
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(467, 97)
        Me.TextBox1.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.lblCreador)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.GroupBox2.Location = New System.Drawing.Point(16, 4)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(475, 81)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = "Fecha de Creación"
        '
        'lblCreador
        '
        Me.lblCreador.AutoSize = true
        Me.lblCreador.BackColor = System.Drawing.Color.Transparent
        Me.lblCreador.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblCreador.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.lblCreador.Location = New System.Drawing.Point(15, 52)
        Me.lblCreador.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCreador.Name = "lblCreador"
        Me.lblCreador.Size = New System.Drawing.Size(50, 16)
        Me.lblCreador.TabIndex = 2
        Me.lblCreador.Text = "Label1"
        Me.lblCreador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer), CType(CType(76,Byte),Integer))
        Me.Label2.Location = New System.Drawing.Point(15, 24)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Label2"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BntPublish
        '
        Me.BntPublish.BackColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(157,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.BntPublish.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BntPublish.ForeColor = System.Drawing.Color.White
        Me.BntPublish.Location = New System.Drawing.Point(75, 452)
        Me.BntPublish.Margin = New System.Windows.Forms.Padding(4)
        Me.BntPublish.Name = "BntPublish"
        Me.BntPublish.Size = New System.Drawing.Size(100, 28)
        Me.BntPublish.TabIndex = 6
        Me.BntPublish.Text = "Publicar"
        Me.BntPublish.UseVisualStyleBackColor = true
        '
        'PanelIndex
        '
        Me.PanelIndex.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.PanelIndex.Location = New System.Drawing.Point(16, 235)
        Me.PanelIndex.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelIndex.Name = "PanelIndex"
        Me.PanelIndex.Size = New System.Drawing.Size(475, 209)
        Me.PanelIndex.TabIndex = 7
        '
        'BtnNotify
        '
        Me.BtnNotify.BackColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(157,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.BtnNotify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnNotify.ForeColor = System.Drawing.Color.White
        Me.BtnNotify.Location = New System.Drawing.Point(183, 452)
        Me.BtnNotify.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnNotify.Name = "BtnNotify"
        Me.BtnNotify.Size = New System.Drawing.Size(144, 28)
        Me.BtnNotify.TabIndex = 8
        Me.BtnNotify.Text = "Publicar y Notificar"
        Me.BtnNotify.UseVisualStyleBackColor = true
        '
        'BtnAddUsers
        '
        Me.BtnAddUsers.BackColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(157,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.BtnAddUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddUsers.ForeColor = System.Drawing.Color.White
        Me.BtnAddUsers.Location = New System.Drawing.Point(335, 452)
        Me.BtnAddUsers.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnAddUsers.Name = "BtnAddUsers"
        Me.BtnAddUsers.Size = New System.Drawing.Size(100, 28)
        Me.BtnAddUsers.TabIndex = 9
        Me.BtnAddUsers.Text = "Usuarios"
        Me.BtnAddUsers.UseVisualStyleBackColor = true
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = true
        Me.LinkLabel1.Location = New System.Drawing.Point(386, 89)
        Me.LinkLabel1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(105, 16)
        Me.LinkLabel1.TabIndex = 3
        Me.LinkLabel1.TabStop = true
        Me.LinkLabel1.Text = "Ver Versionado"
        Me.LinkLabel1.Visible = false
        Me.LinkLabel1.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'frmPublishVersion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8!, 16!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(507, 495)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.BtnAddUsers)
        Me.Controls.Add(Me.BtnNotify)
        Me.Controls.Add(Me.BntPublish)
        Me.Controls.Add(Me.PanelIndex)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmPublishVersion"
        Me.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Publicar"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents BntPublish As ZButton
    Friend WithEvents PanelIndex As System.Windows.Forms.Panel
    Friend WithEvents BtnNotify As ZButton
    Friend WithEvents BtnAddUsers As ZButton
    Friend WithEvents lblCreador As ZLabel
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
End Class
