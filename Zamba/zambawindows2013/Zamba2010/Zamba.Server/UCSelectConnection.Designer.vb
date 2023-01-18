Imports System.Windows.Forms
Imports Zamba.AppBlock

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UcSelectConnection
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.RadDropDownList1 = New System.Windows.Forms.ComboBox()
        Me.RadLabel1 = New ZWhiteLabel()
        Me.SuspendLayout()
        '
        'RadDropDownList1
        '
        Me.RadDropDownList1.BackColor = System.Drawing.Color.White
        Me.RadDropDownList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadDropDownList1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadDropDownList1.ForeColor = System.Drawing.Color.DimGray
        Me.RadDropDownList1.ItemHeight = 13
        Me.RadDropDownList1.Location = New System.Drawing.Point(35, 0)
        Me.RadDropDownList1.Name = "RadDropDownList1"
        Me.RadDropDownList1.Size = New System.Drawing.Size(232, 21)
        Me.RadDropDownList1.TabIndex = 0
        '
        'RadLabel1
        '
        Me.RadLabel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.RadLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadLabel1.ForeColor = System.Drawing.Color.DimGray
        Me.RadLabel1.Location = New System.Drawing.Point(0, 0)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(35, 26)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "Base"
        Me.RadLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'UcSelectConnection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.RadDropDownList1)
        Me.Controls.Add(Me.RadLabel1)
        Me.Name = "UcSelectConnection"
        Me.Size = New System.Drawing.Size(267, 26)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadDropDownList1 As ComboBox
    Friend WithEvents RadLabel1 As ZWhiteLabel

End Class
