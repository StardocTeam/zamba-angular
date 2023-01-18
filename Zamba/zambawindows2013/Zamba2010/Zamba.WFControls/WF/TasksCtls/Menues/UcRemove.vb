Public Class UcRemove
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

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
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents All As ZButton
    Friend WithEvents Task As ZButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(UcRemove))
        Panel1 = New ZPanel
        Label2 = New ZLabel
        All = New ZButton
        Task = New ZButton
        Label1 = New ZLabel
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'ZIconList
        '

        '
        'Panel1
        '
        Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(All)
        Panel1.Controls.Add(Task)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(160, 152)
        Panel1.TabIndex = 0
        '
        'Label2
        '
        Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Cursor = System.Windows.Forms.Cursors.Hand
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(140, 4)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(15, 19)
        Label2.TabIndex = 1
        Label2.Text = "X"
        '
        'All
        '
        All.BackColor = System.Drawing.Color.Transparent
        All.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        All.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        All.Location = New System.Drawing.Point(24, 112)
        All.Name = "All"
        All.Size = New System.Drawing.Size(104, 24)
        All.TabIndex = 22
        All.Text = "Eliminar Todo"
        '
        'Task
        '
        Task.BackColor = System.Drawing.Color.Transparent
        Task.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Task.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Task.Location = New System.Drawing.Point(24, 80)
        Task.Name = "Task"
        Task.Size = New System.Drawing.Size(104, 24)
        Task.TabIndex = 21
        Task.Text = "Solo la Tarea"
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(24, 16)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(104, 56)
        Label1.TabIndex = 0
        Label1.Text = "Seleccione si desea eliminar la tarea incluyendo la documentacion."
        '
        'UcRemove
        '
        Controls.Add(Panel1)
        Name = "UcRemove"
        Size = New System.Drawing.Size(160, 152)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Public Event DeleteTask()
    Public Event DeleteAll()
    Private Sub Task_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Task.Click
        RaiseEvent DeleteTask()
        Dispose()
    End Sub
    Private Sub All_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles All.Click
        RaiseEvent DeleteAll()
        Dispose()
    End Sub

#Region "Close"
    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Label2.Click
        Try
            Dispose()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub UcRemove_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Leave
        Try
            Dispose()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region

End Class
