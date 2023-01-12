<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoWaitForDocument
    'Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
    Private Overloads Sub InitializeComponent()
        Me.Label1 = New ZLabel()
        Me.cmbDocTypes = New System.Windows.Forms.ComboBox()
        Me.btnGuardar = New ZButton()
        Me.IndexController1 = New Zamba.Indexs.IndexController()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.IndexController1)
        Me.tbRule.Controls.Add(Me.btnGuardar)
        Me.tbRule.Controls.Add(Me.cmbDocTypes)
        Me.tbRule.Controls.Add(Me.Label1)
        Me.tbRule.Size = New System.Drawing.Size(504, 378)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(512, 407)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(16, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Seleccione la entidad:"
        Me.Label1.Visible = False
        '
        'cmbDocTypes
        '
        Me.cmbDocTypes.FormattingEnabled = True
        Me.cmbDocTypes.Location = New System.Drawing.Point(187, 24)
        Me.cmbDocTypes.Name = "cmbDocTypes"
        Me.cmbDocTypes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDocTypes.Size = New System.Drawing.Size(190, 24)
        Me.cmbDocTypes.Sorted = True
        Me.cmbDocTypes.TabIndex = 1
        Me.cmbDocTypes.Visible = False
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(248, 342)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(74, 23)
        Me.btnGuardar.TabIndex = 2
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        Me.btnGuardar.Visible = False
        '
        'IndexController1
        '
        Me.IndexController1.AutoScroll = True
        Me.IndexController1.BackColor = System.Drawing.Color.Transparent
        Me.IndexController1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IndexController1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.IndexController1.Location = New System.Drawing.Point(11, 61)
        Me.IndexController1.Name = "IndexController1"
        Me.IndexController1.Size = New System.Drawing.Size(425, 267)
        Me.IndexController1.TabIndex = 3
        Me.IndexController1.Visible = False
        '
        'UCDoWaitForDocument
        '
        Me.Name = "UCDoWaitForDocument"
        Me.Size = New System.Drawing.Size(512, 407)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents cmbDocTypes As System.Windows.Forms.ComboBox
    Friend WithEvents btnGuardar As ZButton
    Friend WithEvents IndexController1 As Zamba.Indexs.IndexController

End Class
