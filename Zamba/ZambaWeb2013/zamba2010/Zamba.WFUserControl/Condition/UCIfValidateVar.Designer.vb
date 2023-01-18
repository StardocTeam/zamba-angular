'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
'Partial Class UCIfValidateVar
'    Inherits System.Windows.Forms.UserControl

'    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
'    <System.Diagnostics.DebuggerNonUserCode()> _
'    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
'        Try
'            If disposing AndAlso components IsNot Nothing Then
'                components.Dispose()
'            End If
'        Finally
'            MyBase.Dispose(disposing)
'        End Try
'    End Sub

'    'Requerido por el Diseñador de Windows Forms
'    Private components As System.ComponentModel.IContainer

'    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
'    'Se puede modificar usando el Diseñador de Windows Forms.  
'    'No lo modifique con el editor de código.
'    <System.Diagnostics.DebuggerStepThrough()> _
'    Private Sub InitializeComponent()
'        components = New System.ComponentModel.Container()
'        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
'    End Sub

'End Class
Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCIfValidateVar
    'Inherits System.Windows.Forms.UserControl
    Inherits ZRuleControl

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
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.TxtVar = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.TxtValue = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.btnSeleccionar = New Zamba.AppBlock.ZButton()
        Me.Label4 = New Zamba.AppBlock.ZLabel()
        Me.TxtOper = New System.Windows.Forms.ComboBox()
        Me.ChkCaseSensitive = New System.Windows.Forms.CheckBox()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Margin = New System.Windows.Forms.Padding(4)
        Me.tbState.Size = New System.Drawing.Size(635, 482)
        '
        'tbHabilitation
        '
        Me.tbHabilitation.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbConfiguration
        '
        Me.tbConfiguration.Margin = New System.Windows.Forms.Padding(4)
        '
        'tbAlerts
        '
        Me.tbAlerts.Margin = New System.Windows.Forms.Padding(4)
        Me.tbAlerts.Padding = New System.Windows.Forms.Padding(4)
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.TxtOper)
        Me.tbRule.Controls.Add(Me.ChkCaseSensitive)
        Me.tbRule.Controls.Add(Me.TxtVar)
        Me.tbRule.Controls.Add(Me.Label1)
        Me.tbRule.Controls.Add(Me.Label3)
        Me.tbRule.Controls.Add(Me.Label4)
        Me.tbRule.Controls.Add(Me.TxtValue)
        Me.tbRule.Controls.Add(Me.btnSeleccionar)
        Me.tbRule.Controls.Add(Me.Label2)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4)
        Me.tbRule.Size = New System.Drawing.Size(635, 482)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4)
        Me.tbctrMain.Size = New System.Drawing.Size(643, 511)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Label1.FontSize = 8.25!
        Me.Label1.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label1.Location = New System.Drawing.Point(17, 58)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Variable"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoEllipsis = True
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Label2.FontSize = 8.25!
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(192, 191)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Operador"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Label3.FontSize = 8.25!
        Me.Label3.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label3.Location = New System.Drawing.Point(17, 226)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Valor"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtVar
        '
        Me.TxtVar.Location = New System.Drawing.Point(21, 78)
        Me.TxtVar.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtVar.Name = "TxtVar"
        Me.TxtVar.Size = New System.Drawing.Size(588, 82)
        Me.TxtVar.TabIndex = 3
        Me.TxtVar.Text = ""
        '
        'TxtValue
        '
        Me.TxtValue.Location = New System.Drawing.Point(21, 246)
        Me.TxtValue.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtValue.Name = "TxtValue"
        Me.TxtValue.Size = New System.Drawing.Size(588, 77)
        Me.TxtValue.TabIndex = 5
        Me.TxtValue.Text = ""
        '
        'btnSeleccionar
        '
        Me.btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSeleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSeleccionar.ForeColor = System.Drawing.Color.White
        Me.btnSeleccionar.Location = New System.Drawing.Point(439, 331)
        Me.btnSeleccionar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSeleccionar.Name = "btnSeleccionar"
        Me.btnSeleccionar.Size = New System.Drawing.Size(149, 28)
        Me.btnSeleccionar.TabIndex = 14
        Me.btnSeleccionar.Text = "Guardar"
        Me.btnSeleccionar.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.FontSize = 9.75!
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(17, 17)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(157, 16)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Validación de Variables"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtOper
        '
        Me.TxtOper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TxtOper.Location = New System.Drawing.Point(311, 187)
        Me.TxtOper.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtOper.Name = "TxtOper"
        Me.TxtOper.Size = New System.Drawing.Size(115, 24)
        Me.TxtOper.TabIndex = 6
        '
        'ChkCaseSensitive
        '
        Me.ChkCaseSensitive.AutoSize = True
        Me.ChkCaseSensitive.BackColor = System.Drawing.Color.White
        Me.ChkCaseSensitive.Location = New System.Drawing.Point(21, 336)
        Me.ChkCaseSensitive.Margin = New System.Windows.Forms.Padding(4)
        Me.ChkCaseSensitive.Name = "ChkCaseSensitive"
        Me.ChkCaseSensitive.Size = New System.Drawing.Size(278, 20)
        Me.ChkCaseSensitive.TabIndex = 17
        Me.ChkCaseSensitive.Text = "No distinguir mayúsculas y minúsculas"
        Me.ChkCaseSensitive.UseVisualStyleBackColor = False
        '
        'UCIfValidateVar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UCIfValidateVar"
        Me.Size = New System.Drawing.Size(643, 511)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents TxtVar As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents TxtValue As Zamba.AppBlock.TextoInteligenteTextBox
    Friend Shadows WithEvents btnSeleccionar As ZButton
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents TxtOper As System.Windows.Forms.ComboBox
    Friend WithEvents ChkCaseSensitive As System.Windows.Forms.CheckBox

End Class
