<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDOADDTOWF
    Inherits ZRuleControl

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
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.FsButton1 = New Zamba.AppBlock.ZButton()
        Me.lblInicial = New Zamba.AppBlock.ZLabel()
        Me.Label4 = New Zamba.AppBlock.ZLabel()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chkShowOrNot = New System.Windows.Forms.CheckBox()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.chkShowOrNot)
        Me.tbRule.Controls.Add(Me.ListView1)
        Me.tbRule.Controls.Add(Me.Label4)
        Me.tbRule.Controls.Add(Me.lblInicial)
        Me.tbRule.Controls.Add(Me.FsButton1)
        Me.tbRule.Controls.Add(Me.Label3)
        Me.tbRule.Size = New System.Drawing.Size(562, 447)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(570, 476)
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(254, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 16)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Inicial"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'FsButton1
        '
        Me.FsButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.FsButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.FsButton1.ForeColor = System.Drawing.Color.White
        Me.FsButton1.Location = New System.Drawing.Point(371, 326)
        Me.FsButton1.Name = "FsButton1"
        Me.FsButton1.Size = New System.Drawing.Size(74, 26)
        Me.FsButton1.TabIndex = 13
        Me.FsButton1.Text = "Guardar"
        Me.FsButton1.UseVisualStyleBackColor = False
        '
        'lblInicial
        '
        Me.lblInicial.AutoSize = True
        Me.lblInicial.BackColor = System.Drawing.Color.Transparent
        Me.lblInicial.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInicial.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblInicial.Location = New System.Drawing.Point(251, 127)
        Me.lblInicial.Name = "lblInicial"
        Me.lblInicial.Size = New System.Drawing.Size(89, 16)
        Me.lblInicial.TabIndex = 14
        Me.lblInicial.Text = "Etapa Inicial"
        Me.lblInicial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(10, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(162, 16)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Seleccione el WorkFlow"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ListView1
        '
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.ListView1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(14, 47)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(210, 304)
        Me.ListView1.TabIndex = 16
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.List
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = ""
        Me.ColumnHeader1.Width = 200
        '
        'chkShowOrNot
        '
        Me.chkShowOrNot.AutoSize = True
        Me.chkShowOrNot.Location = New System.Drawing.Point(254, 160)
        Me.chkShowOrNot.Name = "chkShowOrNot"
        Me.chkShowOrNot.Size = New System.Drawing.Size(252, 20)
        Me.chkShowOrNot.TabIndex = 17
        Me.chkShowOrNot.Text = "Mostrar tarea despues de agregar"
        Me.chkShowOrNot.UseVisualStyleBackColor = True
        '
        'UCDOADDTOWF
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.AutoSize = True
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Name = "UCDOADDTOWF"
        Me.Size = New System.Drawing.Size(570, 476)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents FsButton1 As ZButton
    Friend WithEvents lblInicial As ZLabel
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Private WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkShowOrNot As System.Windows.Forms.CheckBox

End Class
