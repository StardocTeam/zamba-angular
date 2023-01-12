Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCIndexAndUserSelection
    'Inherits System.Windows.Forms.UserControl

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
    Private Overloads Sub InitializeComponent()
        Me.pnlControlsUp = New System.Windows.Forms.Panel()
        Me.tabPrincipal = New System.Windows.Forms.TabControl()
        Me.tabpIndexSelection = New System.Windows.Forms.TabPage()
        Me.btnGuardar = New ZButton()
        Me.Label1 = New ZLabel()
        Me.cmbDocTypes = New System.Windows.Forms.ComboBox()
        Me.IndexController1 = New Zamba.Indexs.IndexController()
        Me.tabpUserSelection = New System.Windows.Forms.TabPage()
        Me.pnlControlsUp.SuspendLayout()
        Me.tabPrincipal.SuspendLayout()
        Me.tabpIndexSelection.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlControlsUp
        '
        Me.pnlControlsUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlControlsUp.Controls.Add(Me.tabPrincipal)
        Me.pnlControlsUp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlControlsUp.Location = New System.Drawing.Point(0, 0)
        Me.pnlControlsUp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlControlsUp.Name = "pnlControlsUp"
        Me.pnlControlsUp.Size = New System.Drawing.Size(1301, 703)
        Me.pnlControlsUp.TabIndex = 5
        '
        'tabPrincipal
        '
        Me.tabPrincipal.Controls.Add(Me.tabpIndexSelection)
        Me.tabPrincipal.Controls.Add(Me.tabpUserSelection)
        Me.tabPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabPrincipal.Location = New System.Drawing.Point(0, 0)
        Me.tabPrincipal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tabPrincipal.Multiline = True
        Me.tabPrincipal.Name = "tabPrincipal"
        Me.tabPrincipal.SelectedIndex = 0
        Me.tabPrincipal.Size = New System.Drawing.Size(1299, 701)
        Me.tabPrincipal.TabIndex = 1
        '
        'tabpIndexSelection
        '
        Me.tabpIndexSelection.Controls.Add(Me.btnGuardar)
        Me.tabpIndexSelection.Controls.Add(Me.Label1)
        Me.tabpIndexSelection.Controls.Add(Me.cmbDocTypes)
        Me.tabpIndexSelection.Controls.Add(Me.IndexController1)
        Me.tabpIndexSelection.Location = New System.Drawing.Point(4, 25)
        Me.tabpIndexSelection.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tabpIndexSelection.Name = "tabpIndexSelection"
        Me.tabpIndexSelection.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tabpIndexSelection.Size = New System.Drawing.Size(1291, 672)
        Me.tabpIndexSelection.TabIndex = 0
        Me.tabpIndexSelection.Text = "Atributos"
        Me.tabpIndexSelection.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(395, 46)
        Me.btnGuardar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(100, 28)
        Me.btnGuardar.TabIndex = 9
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(56, 21)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 16)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Entidad:"
        '
        'cmbDocTypes
        '
        Me.cmbDocTypes.Location = New System.Drawing.Point(56, 50)
        Me.cmbDocTypes.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbDocTypes.Name = "cmbDocTypes"
        Me.cmbDocTypes.Size = New System.Drawing.Size(260, 24)
        Me.cmbDocTypes.TabIndex = 7
        '
        'IndexController1
        '
        Me.IndexController1.AutoScroll = True
        Me.IndexController1.BackColor = System.Drawing.Color.Transparent
        Me.IndexController1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IndexController1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IndexController1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.IndexController1.Location = New System.Drawing.Point(56, 94)
        Me.IndexController1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndexController1.Name = "IndexController1"
        Me.IndexController1.Size = New System.Drawing.Size(677, 326)
        Me.IndexController1.TabIndex = 6
        '
        'tabpUserSelection
        '
        Me.tabpUserSelection.Location = New System.Drawing.Point(4, 25)
        Me.tabpUserSelection.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tabpUserSelection.Name = "tabpUserSelection"
        Me.tabpUserSelection.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tabpUserSelection.Size = New System.Drawing.Size(1291, 672)
        Me.tabpUserSelection.TabIndex = 1
        Me.tabpUserSelection.Text = "Usuarios"
        Me.tabpUserSelection.UseVisualStyleBackColor = True
        '
        'UCIndexAndUserSelection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.pnlControlsUp)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCIndexAndUserSelection"
        Me.Size = New System.Drawing.Size(1301, 703)
        Me.pnlControlsUp.ResumeLayout(False)
        Me.tabPrincipal.ResumeLayout(False)
        Me.tabpIndexSelection.ResumeLayout(False)
        Me.tabpIndexSelection.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlControlsUp As System.Windows.Forms.Panel
    Friend WithEvents tabPrincipal As System.Windows.Forms.TabControl
    Friend WithEvents tabpIndexSelection As System.Windows.Forms.TabPage
    Friend WithEvents tabpUserSelection As System.Windows.Forms.TabPage
    Public WithEvents IndexController1 As Zamba.Indexs.IndexController
    Friend WithEvents btnGuardar As ZButton
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents cmbDocTypes As System.Windows.Forms.ComboBox

End Class
