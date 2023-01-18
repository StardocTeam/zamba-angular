<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowVersionComment
    Inherits Zamba.AppBlock.ZForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblCreador = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.BntPublish = New System.Windows.Forms.Button
        Me.PanelIndex = New System.Windows.Forms.Panel
        Me.BtnNotify = New System.Windows.Forms.Button
        Me.BtnAddUsers = New System.Windows.Forms.Button
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 86)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(266, 98)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Comentario"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(3, 16)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(260, 79)
        Me.TextBox1.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblCreador)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.GroupBox2.Location = New System.Drawing.Point(12, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(266, 66)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Fecha de Creación"
        '
        'lblCreador
        '
        Me.lblCreador.AutoSize = True
        Me.lblCreador.Location = New System.Drawing.Point(6, 41)
        Me.lblCreador.Name = "lblCreador"
        Me.lblCreador.Size = New System.Drawing.Size(39, 13)
        Me.lblCreador.TabIndex = 2
        Me.lblCreador.Text = "Label1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label2.Location = New System.Drawing.Point(6, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Label2"
        '
        'BntPublish
        '
        Me.BntPublish.Location = New System.Drawing.Point(15, 367)
        Me.BntPublish.Name = "BntPublish"
        Me.BntPublish.Size = New System.Drawing.Size(75, 23)
        Me.BntPublish.TabIndex = 6
        Me.BntPublish.Text = "Publicar"
        Me.BntPublish.UseVisualStyleBackColor = True
        '
        'PanelIndex
        '
        Me.PanelIndex.Location = New System.Drawing.Point(15, 191)
        Me.PanelIndex.Name = "PanelIndex"
        Me.PanelIndex.Size = New System.Drawing.Size(263, 170)
        Me.PanelIndex.TabIndex = 7
        '
        'BtnNotify
        '
        Me.BtnNotify.Location = New System.Drawing.Point(93, 367)
        Me.BtnNotify.Name = "BtnNotify"
        Me.BtnNotify.Size = New System.Drawing.Size(108, 23)
        Me.BtnNotify.TabIndex = 8
        Me.BtnNotify.Text = "Publicar y Notificar"
        Me.BtnNotify.UseVisualStyleBackColor = True
        '
        'BtnAddUsers
        '
        Me.BtnAddUsers.Location = New System.Drawing.Point(203, 367)
        Me.BtnAddUsers.Name = "BtnAddUsers"
        Me.BtnAddUsers.Size = New System.Drawing.Size(75, 23)
        Me.BtnAddUsers.TabIndex = 9
        Me.BtnAddUsers.Text = "Usuarios"
        Me.BtnAddUsers.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(200, 72)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(79, 13)
        Me.LinkLabel1.TabIndex = 3
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Ver Versionado"
        Me.LinkLabel1.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'frmShowVersionComment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.MenuBar
        Me.ClientSize = New System.Drawing.Size(287, 402)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.BtnAddUsers)
        Me.Controls.Add(Me.BtnNotify)
        Me.Controls.Add(Me.BntPublish)
        Me.Controls.Add(Me.PanelIndex)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmShowVersionComment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Comentario de Version"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BntPublish As System.Windows.Forms.Button
    Friend WithEvents PanelIndex As System.Windows.Forms.Panel
    Friend WithEvents BtnNotify As System.Windows.Forms.Button
    Friend WithEvents BtnAddUsers As System.Windows.Forms.Button
    Friend WithEvents lblCreador As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
End Class
