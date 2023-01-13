<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCSelectUsers
    Inherits Zamba.AppBlock.ZControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.lstNoSelectedUsers = New System.Windows.Forms.ListBox()
        Me.lstSelectedUsers = New System.Windows.Forms.ListBox()
        Me.btnAgregar = New ZButton()
        Me.btnQuitar = New ZButton()
        Me.Label1 = New ZLabel()
        Me.Label2 = New ZLabel()
        Me.btnAceptar = New ZButton()
        Me.btnCancelar = New ZButton()
        Me.lblExternalUsers = New ZLabel()
        Me.TxtUsuariosExternos = New System.Windows.Forms.TextBox()
        Me.lstNoSelectedGroups = New System.Windows.Forms.ListBox()
        Me.Label4 = New ZLabel()
        Me.lstSelectedGroups = New System.Windows.Forms.ListBox()
        Me.Label5 = New ZLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstNoSelectedUsers
        '
        Me.lstNoSelectedUsers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstNoSelectedUsers.BackColor = System.Drawing.Color.White
        Me.lstNoSelectedUsers.FormattingEnabled = True
        Me.lstNoSelectedUsers.ItemHeight = 16
        Me.lstNoSelectedUsers.Location = New System.Drawing.Point(8, 244)
        Me.lstNoSelectedUsers.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lstNoSelectedUsers.Name = "lstNoSelectedUsers"
        Me.lstNoSelectedUsers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstNoSelectedUsers.Size = New System.Drawing.Size(282, 180)
        Me.lstNoSelectedUsers.TabIndex = 0
        '
        'lstSelectedUsers
        '
        Me.lstSelectedUsers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstSelectedUsers.BackColor = System.Drawing.Color.White
        Me.lstSelectedUsers.FormattingEnabled = True
        Me.lstSelectedUsers.ItemHeight = 16
        Me.lstSelectedUsers.Location = New System.Drawing.Point(8, 244)
        Me.lstSelectedUsers.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lstSelectedUsers.Name = "lstSelectedUsers"
        Me.lstSelectedUsers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstSelectedUsers.Size = New System.Drawing.Size(282, 180)
        Me.lstSelectedUsers.TabIndex = 1
        '
        'btnAgregar
        '
        Me.btnAgregar.Location = New System.Drawing.Point(346, 224)
        Me.btnAgregar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(100, 28)
        Me.btnAgregar.TabIndex = 2
        Me.btnAgregar.Text = "Agregar >>"
        Me.btnAgregar.UseVisualStyleBackColor = True
        '
        'btnQuitar
        '
        Me.btnQuitar.Location = New System.Drawing.Point(346, 260)
        Me.btnQuitar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnQuitar.Name = "btnQuitar"
        Me.btnQuitar.Size = New System.Drawing.Size(100, 28)
        Me.btnQuitar.TabIndex = 3
        Me.btnQuitar.Text = "<< Quitar"
        Me.btnQuitar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 224)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Usuarios:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 224)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 16)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Usuarios:"
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(21, 432)
        Me.btnAceptar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(100, 28)
        Me.btnAceptar.TabIndex = 6
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(129, 432)
        Me.btnCancelar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(100, 28)
        Me.btnCancelar.TabIndex = 7
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'lblExternalUsers
        '
        Me.lblExternalUsers.AutoSize = True
        Me.lblExternalUsers.Location = New System.Drawing.Point(8, 428)
        Me.lblExternalUsers.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblExternalUsers.Name = "lblExternalUsers"
        Me.lblExternalUsers.Size = New System.Drawing.Size(131, 16)
        Me.lblExternalUsers.TabIndex = 8
        Me.lblExternalUsers.Text = "Usuarios Externos:"
        '
        'TxtUsuariosExternos
        '
        Me.TxtUsuariosExternos.Location = New System.Drawing.Point(8, 448)
        Me.TxtUsuariosExternos.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtUsuariosExternos.Name = "TxtUsuariosExternos"
        Me.TxtUsuariosExternos.Size = New System.Drawing.Size(229, 23)
        Me.TxtUsuariosExternos.TabIndex = 9
        '
        'lstNoSelectedGroups
        '
        Me.lstNoSelectedGroups.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstNoSelectedGroups.BackColor = System.Drawing.Color.White
        Me.lstNoSelectedGroups.FormattingEnabled = True
        Me.lstNoSelectedGroups.ItemHeight = 16
        Me.lstNoSelectedGroups.Location = New System.Drawing.Point(8, 40)
        Me.lstNoSelectedGroups.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lstNoSelectedGroups.Name = "lstNoSelectedGroups"
        Me.lstNoSelectedGroups.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstNoSelectedGroups.Size = New System.Drawing.Size(282, 180)
        Me.lstNoSelectedGroups.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 20)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 16)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Grupos:"
        '
        'lstSelectedGroups
        '
        Me.lstSelectedGroups.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstSelectedGroups.BackColor = System.Drawing.Color.White
        Me.lstSelectedGroups.FormattingEnabled = True
        Me.lstSelectedGroups.ItemHeight = 16
        Me.lstSelectedGroups.Location = New System.Drawing.Point(8, 40)
        Me.lstSelectedGroups.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lstSelectedGroups.Name = "lstSelectedGroups"
        Me.lstSelectedGroups.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstSelectedGroups.Size = New System.Drawing.Size(282, 180)
        Me.lstSelectedGroups.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 20)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 16)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Grupos:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lstNoSelectedUsers)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lstNoSelectedGroups)
        Me.GroupBox1.Controls.Add(Me.lblExternalUsers)
        Me.GroupBox1.Controls.Add(Me.TxtUsuariosExternos)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 6)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(304, 508)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "No Asignados"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.lstSelectedUsers)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.btnCancelar)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.btnAceptar)
        Me.GroupBox2.Controls.Add(Me.lstSelectedGroups)
        Me.GroupBox2.Location = New System.Drawing.Point(476, 6)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(304, 508)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Asignados"
        '
        'UCSelectUsers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.MenuBar
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnQuitar)
        Me.Controls.Add(Me.btnAgregar)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCSelectUsers"
        Me.Size = New System.Drawing.Size(800, 524)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstNoSelectedUsers As System.Windows.Forms.ListBox
    Friend WithEvents lstSelectedUsers As System.Windows.Forms.ListBox
    Friend WithEvents btnAgregar As ZButton
    Friend WithEvents btnQuitar As ZButton
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents btnCancelar As ZButton
    Friend WithEvents lblExternalUsers As ZLabel
    Friend WithEvents TxtUsuariosExternos As System.Windows.Forms.TextBox
    Friend WithEvents lstNoSelectedGroups As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents lstSelectedGroups As System.Windows.Forms.ListBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
End Class
