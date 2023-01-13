Public Class frmMonitorNotify
    Inherits Form

#Region " Código generado por el Diseñador de Windows Forms "

    Private Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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
    Friend WithEvents txtBody As System.Windows.Forms.Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMonitorNotify))
        txtBody = New System.Windows.Forms.Label
        Panel1 = New Panel
        Label1 = New System.Windows.Forms.Label
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'txtBody
        '
        txtBody.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtBody.BackColor = System.Drawing.Color.Transparent
        txtBody.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        txtBody.Location = New System.Drawing.Point(16, 16)
        txtBody.Name = "txtBody"
        txtBody.Size = New System.Drawing.Size(364, 40)
        txtBody.TabIndex = 0
        '
        'Panel1
        '
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(txtBody)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(2, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(396, 82)
        Panel1.TabIndex = 1
        '
        'Label1
        '
        Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(16, 56)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(364, 16)
        Label1.TabIndex = 1
        AutoScaleMode = False
        AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        BackColor = System.Drawing.Color.White
        ClientSize = New System.Drawing.Size(400, 86)
        Controls.Add(Panel1)
        DockPadding.All = 2
        ForeColor = System.Drawing.Color.Black
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmMonitorNotify"
        ShowInTaskbar = False
        SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Text = "Zamba Software - Informacion"
        TopMost = True
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Public Sub New(ByVal Body As String, Optional ByVal Title As String = "Zamba Software - Informacion")
        Me.New()
        Text = Title
        txtBody.Text = Body
        Opacity = 1
    End Sub
    Dim Timer1 As Threading.Timer
    Dim CB As New Threading.TimerCallback(AddressOf Tick)
    Dim State As Object
    Private Sub frmMonitorNotify_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            Location = New Drawing.Point(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - Height)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Try
            Timer1 = New Threading.Timer(CB, State, 3000, 1000)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Refresh()
    End Sub
    Dim Count As Int32
    Dim finished As Boolean
    Private Sub Tick(ByVal State As Object)
        If finished = False Then
            Try
                If Count <= 5000 Then
                    Count += +1000
                    Label1.Text = Opacity * 10
                    Opacity = Opacity - 0.2
                    Refresh()
                Else
                    finished = True
                    Try
                        Opacity = 0
                    Catch
                    End Try
                    Try
                        Timer1.Dispose()
                    Catch ex As Threading.SynchronizationLockException
                    Catch ex As Threading.ThreadAbortException
                    Catch ex As Threading.ThreadInterruptedException
                    Catch ex As Threading.ThreadStateException
                    Catch ex As Exception
                        zamba.core.zclass.raiseerror(ex)
                    End Try
                    Label1.Text = "Cerrando..."
                    Visible = False
                    Close()
                End If
            Catch ex As Exception
                '               zamba.core.zclass.raiseerror(ex)
            End Try
        End If
    End Sub

    Private Sub frmMonitorNotify_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            Timer1.Dispose()
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStateException
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
            'Catch
        End Try
    End Sub
End Class
