<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAddOrUpdateTemplate
    Inherits Zamba.AppBlock.ZForm

    'Form overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.lblDescription = New Zamba.AppBlock.ZLabel()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.lblName = New Zamba.AppBlock.ZLabel()
        Me.btnAddOrUpdate = New Zamba.AppBlock.ZButton()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.btnExplore = New Zamba.AppBlock.ZButton()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.lblPath = New Zamba.AppBlock.ZLabel()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ErrorProvider2 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ErrorProvider3 = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(29, 201)
        Me.lblDescription.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(133, 20)
        Me.lblDescription.TabIndex = 5
        Me.lblDescription.Text = "Descripcion"
        Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(29, 230)
        Me.txtDescription.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(255, 23)
        Me.txtDescription.TabIndex = 6
        '
        'lblName
        '
        Me.lblName.Location = New System.Drawing.Point(29, 132)
        Me.lblName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(133, 20)
        Me.lblName.TabIndex = 3
        Me.lblName.Text = "Nombre"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnAddOrUpdate
        '
        Me.btnAddOrUpdate.Enabled = False
        Me.btnAddOrUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddOrUpdate.Location = New System.Drawing.Point(140, 279)
        Me.btnAddOrUpdate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnAddOrUpdate.Name = "btnAddOrUpdate"
        Me.btnAddOrUpdate.Size = New System.Drawing.Size(85, 30)
        Me.btnAddOrUpdate.TabIndex = 7
        Me.btnAddOrUpdate.UseVisualStyleBackColor = False
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(29, 161)
        Me.txtName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(255, 23)
        Me.txtName.TabIndex = 4
        '
        'btnExplore
        '
        Me.btnExplore.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExplore.Location = New System.Drawing.Point(253, 82)
        Me.btnExplore.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnExplore.Name = "btnExplore"
        Me.btnExplore.Size = New System.Drawing.Size(107, 30)
        Me.btnExplore.TabIndex = 2
        Me.btnExplore.Text = "Examinar"
        Me.btnExplore.UseVisualStyleBackColor = False
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(29, 53)
        Me.txtPath.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(329, 23)
        Me.txtPath.TabIndex = 1
        '
        'lblPath
        '
        Me.lblPath.Location = New System.Drawing.Point(29, 23)
        Me.lblPath.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPath.Name = "lblPath"
        Me.lblPath.Size = New System.Drawing.Size(213, 20)
        Me.lblPath.TabIndex = 0
        Me.lblPath.Text = "Plantilla"
        Me.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'ErrorProvider2
        '
        Me.ErrorProvider2.ContainerControl = Me
        '
        'ErrorProvider3
        '
        Me.ErrorProvider3.ContainerControl = Me
        '
        'frmAddOrUpdateTemplate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 334)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.btnAddOrUpdate)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.btnExplore)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.lblPath)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblDescription As ZLabel
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblName As ZLabel
    Friend WithEvents btnAddOrUpdate As ZButton
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents btnExplore As ZButton
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents lblPath As ZLabel
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents ErrorProvider2 As System.Windows.Forms.ErrorProvider
    Friend WithEvents ErrorProvider3 As System.Windows.Forms.ErrorProvider
End Class
