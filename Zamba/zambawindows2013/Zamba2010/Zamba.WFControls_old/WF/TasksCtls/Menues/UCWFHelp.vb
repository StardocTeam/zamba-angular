Public Class UCWFHelp
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "



    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents txtHelp As TextBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents Label1 As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(UCWFHelp))
        Panel1 = New ZPanel
        Label1 = New ZLabel
        txtHelp = New TextBox
        Label4 = New ZLabel
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(txtHelp)
        Panel1.Controls.Add(Label4)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(240, 160)
        Panel1.TabIndex = 0
        '
        'Label1
        '
        Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Cursor = System.Windows.Forms.Cursors.Hand
        Label1.Font = New Font("Verdana", 11.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.Blue
        Label1.Image = CType(resources.GetObject("Label1.Image"), System.Drawing.Image)
        Label1.Location = New System.Drawing.Point(219, 2)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(15, 19)
        Label1.TabIndex = 23
        '
        'lblHelp
        '
        txtHelp.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtHelp.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        txtHelp.Location = New System.Drawing.Point(3, 34)
        txtHelp.Name = "lblHelp"
        txtHelp.Size = New System.Drawing.Size(231, 115)
        txtHelp.TabIndex = 15
        txtHelp.Multiline = True
        txtHelp.ReadOnly = True
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label4.Location = New System.Drawing.Point(3, 8)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(48, 13)
        Label4.TabIndex = 14
        Label4.Text = "Ayuda:"
        '
        'UCWFHelp
        '
        Controls.Add(Panel1)
        Name = "UCWFHelp"
        Size = New System.Drawing.Size(240, 160)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

    Public Sub New(ByVal name As String, ByVal help As String)
        MyBase.New()
        InitializeComponent()

        SetHelp(name, help)
    End Sub

    Private Sub SetHelp(ByVal name As String, ByVal help As String)
        Try
            Label4.Text = name
            txtHelp.Text = help
        Catch ex As Exception
            txtHelp.Text = String.Empty
        End Try
    End Sub

#Region "Close"
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Label1.Click
        CloseControl()
    End Sub

    Public Sub CloseControl()
        Try
            If Parent IsNot Nothing Then
                Parent.Controls.Remove(Me)
            End If
            Dispose()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub UcInfo_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Leave
        Try
            Dispose()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region
End Class