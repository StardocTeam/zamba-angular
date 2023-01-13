<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmcreateviews
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
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.chkselectall = New System.Windows.Forms.CheckBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.BackColor = System.Drawing.Color.White
        Me.CheckedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CheckedListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(0, 42)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(284, 386)
        Me.CheckedListBox1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button1.Location = New System.Drawing.Point(0, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(284, 42)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Crear Vistas"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.Color.White
        Me.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.HorizontalScrollbar = True
        Me.ListBox1.Location = New System.Drawing.Point(0, 0)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.ScrollAlwaysVisible = True
        Me.ListBox1.Size = New System.Drawing.Size(568, 431)
        Me.ListBox1.TabIndex = 2
        '
        'chkselectall
        '
        Me.chkselectall.AutoSize = True
        Me.chkselectall.Dock = System.Windows.Forms.DockStyle.Top
        Me.chkselectall.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkselectall.Location = New System.Drawing.Point(0, 42)
        Me.chkselectall.Name = "chkselectall"
        Me.chkselectall.Size = New System.Drawing.Size(284, 17)
        Me.chkselectall.TabIndex = 3
        Me.chkselectall.Text = "Seleccionar todos"
        Me.chkselectall.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 2)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkselectall)
        Me.SplitContainer1.Panel1.Controls.Add(Me.CheckedListBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ListBox1)
        Me.SplitContainer1.Size = New System.Drawing.Size(856, 434)
        Me.SplitContainer1.SplitterDistance = 284
        Me.SplitContainer1.TabIndex = 4
        '
        'frmcreateviews
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(860, 438)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmcreateviews"
        Me.Text = "Creacion de Vistas publicas para consultas externas"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents chkselectall As System.Windows.Forms.CheckBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer

End Class
