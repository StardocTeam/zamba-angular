﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectMailUser
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lstSelectedUsers = New System.Windows.Forms.ListBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.lstSelectedGroups = New System.Windows.Forms.ListBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lstNoSelectedUsers = New System.Windows.Forms.ListBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lstNoSelectedGroups = New System.Windows.Forms.ListBox
        Me.lblExternalUsers = New System.Windows.Forms.Label
        Me.TxtUsuariosExternos = New System.Windows.Forms.TextBox
        Me.btnQuitar = New System.Windows.Forms.Button
        Me.btnAgregar = New System.Windows.Forms.Button
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.lstSelectedUsers)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.btnCancelar)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.btnAceptar)
        Me.GroupBox2.Controls.Add(Me.lstSelectedGroups)
        Me.GroupBox2.Location = New System.Drawing.Point(393, 17)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(231, 413)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Asignados"
        '
        'lstSelectedUsers
        '
        Me.lstSelectedUsers.FormattingEnabled = True
        Me.lstSelectedUsers.Location = New System.Drawing.Point(19, 214)
        Me.lstSelectedUsers.Name = "lstSelectedUsers"
        Me.lstSelectedUsers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstSelectedUsers.Size = New System.Drawing.Size(195, 147)
        Me.lstSelectedUsers.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 195)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Usuarios:"
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(125, 371)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelar.TabIndex = 7
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Grupos:"
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(47, 371)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
        Me.btnAceptar.TabIndex = 6
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'lstSelectedGroups
        '
        Me.lstSelectedGroups.FormattingEnabled = True
        Me.lstSelectedGroups.Location = New System.Drawing.Point(19, 43)
        Me.lstSelectedGroups.Name = "lstSelectedGroups"
        Me.lstSelectedGroups.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstSelectedGroups.Size = New System.Drawing.Size(195, 147)
        Me.lstSelectedGroups.TabIndex = 12
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lstNoSelectedUsers)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lstNoSelectedGroups)
        Me.GroupBox1.Controls.Add(Me.lblExternalUsers)
        Me.GroupBox1.Controls.Add(Me.TxtUsuariosExternos)
        Me.GroupBox1.Location = New System.Drawing.Point(27, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(231, 413)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "No Asignados"
        '
        'lstNoSelectedUsers
        '
        Me.lstNoSelectedUsers.FormattingEnabled = True
        Me.lstNoSelectedUsers.Location = New System.Drawing.Point(27, 214)
        Me.lstNoSelectedUsers.Name = "lstNoSelectedUsers"
        Me.lstNoSelectedUsers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstNoSelectedUsers.Size = New System.Drawing.Size(176, 147)
        Me.lstNoSelectedUsers.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Grupos:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 195)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Usuarios:"
        '
        'lstNoSelectedGroups
        '
        Me.lstNoSelectedGroups.FormattingEnabled = True
        Me.lstNoSelectedGroups.Location = New System.Drawing.Point(27, 43)
        Me.lstNoSelectedGroups.Name = "lstNoSelectedGroups"
        Me.lstNoSelectedGroups.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstNoSelectedGroups.Size = New System.Drawing.Size(176, 147)
        Me.lstNoSelectedGroups.TabIndex = 10
        '
        'lblExternalUsers
        '
        Me.lblExternalUsers.AutoSize = True
        Me.lblExternalUsers.Location = New System.Drawing.Point(27, 371)
        Me.lblExternalUsers.Name = "lblExternalUsers"
        Me.lblExternalUsers.Size = New System.Drawing.Size(95, 13)
        Me.lblExternalUsers.TabIndex = 8
        Me.lblExternalUsers.Text = "Usuarios Externos:"
        '
        'TxtUsuariosExternos
        '
        Me.TxtUsuariosExternos.Location = New System.Drawing.Point(27, 387)
        Me.TxtUsuariosExternos.Name = "TxtUsuariosExternos"
        Me.TxtUsuariosExternos.Size = New System.Drawing.Size(173, 20)
        Me.TxtUsuariosExternos.TabIndex = 9
        '
        'btnQuitar
        '
        Me.btnQuitar.Location = New System.Drawing.Point(286, 275)
        Me.btnQuitar.Name = "btnQuitar"
        Me.btnQuitar.Size = New System.Drawing.Size(75, 23)
        Me.btnQuitar.TabIndex = 18
        Me.btnQuitar.Text = "<< Quitar"
        Me.btnQuitar.UseVisualStyleBackColor = True
        '
        'btnAgregar
        '
        Me.btnAgregar.Location = New System.Drawing.Point(286, 246)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(75, 23)
        Me.btnAgregar.TabIndex = 17
        Me.btnAgregar.Text = "Agregar >>"
        Me.btnAgregar.UseVisualStyleBackColor = True
        '
        'frmSelectMailUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 457)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnQuitar)
        Me.Controls.Add(Me.btnAgregar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSelectMailUser"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lstSelectedUsers As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents lstSelectedGroups As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lstNoSelectedUsers As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstNoSelectedGroups As System.Windows.Forms.ListBox
    Friend WithEvents lblExternalUsers As System.Windows.Forms.Label
    Friend WithEvents TxtUsuariosExternos As System.Windows.Forms.TextBox
    Friend WithEvents btnQuitar As System.Windows.Forms.Button
    Friend WithEvents btnAgregar As System.Windows.Forms.Button

End Class