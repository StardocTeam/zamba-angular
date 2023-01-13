<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UcForumDetails
    Inherits System.Windows.Forms.UserControl
    Implements IDisposable

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            If disposing Then
                If Me.components IsNot Nothing Then Me.components.Dispose()
                If Me.Label1 IsNot Nothing Then Me.Label1.Dispose()
                If Me.Label2 IsNot Nothing Then Me.Label2.Dispose()
          
             
                If Me.txtCreationTime IsNot Nothing Then Me.txtCreationTime.Dispose()
        
           
                If Me.txtUserName IsNot Nothing Then Me.txtUserName.Dispose()
            End If
            MyBase.Dispose(disposing)
            isDisposed = True
        End If
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Private isDisposed As Boolean

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New ZLabel
        Me.Label2 = New ZLabel



        Me.txtCreationTime = New System.Windows.Forms.TextBox
        Me.txtUserName = New System.Windows.Forms.TextBox

        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Usuario creador:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Fecha de creación:"



        '
        'txtCreationTime
        '
        Me.txtCreationTime.Location = New System.Drawing.Point(132, 38)
        Me.txtCreationTime.Name = "txtCreationTime"
        Me.txtCreationTime.ReadOnly = True
        Me.txtCreationTime.Size = New System.Drawing.Size(229, 20)
        Me.txtCreationTime.TabIndex = 5
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(132, 12)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.ReadOnly = True
        Me.txtUserName.Size = New System.Drawing.Size(229, 20)
        Me.txtUserName.TabIndex = 6

        '
        'UcForumDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font

        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.txtCreationTime)

        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "UcForumDetails"
        Me.Size = New System.Drawing.Size(417, 122)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel


    Friend WithEvents txtCreationTime As System.Windows.Forms.TextBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
  

End Class
