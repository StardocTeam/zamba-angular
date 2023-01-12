<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFormsListOfDocType
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFormsListOfDocType))
        Me.Label1 = New ZLabel
        Me.lstFormList = New System.Windows.Forms.ListBox
        Me.Label2 = New ZLabel
        Me.btnclose = New ZButton
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(318, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "La entidad posee más de un formulario asociado"
        '
        'lstFormList
        '
        Me.lstFormList.FormattingEnabled = True
        Me.lstFormList.Location = New System.Drawing.Point(25, 67)
        Me.lstFormList.Name = "lstFormList"
        Me.lstFormList.Size = New System.Drawing.Size(268, 147)
        Me.lstFormList.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(59, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(181, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Elija uno de la lista para utilizar"
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(119, 231)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(75, 23)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Aceptar"
        Me.btnclose.UseVisualStyleBackColor = True
        '
        'frmFormsListOfDocType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(327, 266)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lstFormList)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmFormsListOfDocType"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Zamba Software"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents lstFormList As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents btnclose As ZButton
End Class
