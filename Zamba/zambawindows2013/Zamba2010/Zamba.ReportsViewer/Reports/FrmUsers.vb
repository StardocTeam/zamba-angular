Imports Zamba.Servers
Imports Zamba.Core
Public Class FrmUsers
    Inherits System.Windows.Forms.Form

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
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
    Friend WithEvents Cbousers As System.Windows.Forms.ComboBox
    Friend WithEvents btnaccept As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FrmUsers))
        Me.Cbousers = New System.Windows.Forms.ComboBox
        Me.btnaccept = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Cbousers
        '
        Me.Cbousers.Location = New System.Drawing.Point(48, 24)
        Me.Cbousers.Name = "Cbousers"
        Me.Cbousers.Size = New System.Drawing.Size(168, 21)
        Me.Cbousers.TabIndex = 0
        '
        'btnaccept
        '
        Me.btnaccept.Location = New System.Drawing.Point(88, 72)
        Me.btnaccept.Name = "btnaccept"
        Me.btnaccept.TabIndex = 1
        Me.btnaccept.Text = "Aceptar"
        '
        'FrmUsers
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(256, 118)
        Me.Controls.Add(Me.btnaccept)
        Me.Controls.Add(Me.Cbousers)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmUsers"
        Me.Text = "Usuarios"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub FrmUsers_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim ds As DataSet = GetUsersDatasetOrderbyName()
            Cbousers.DataSource = ds.Tables(0)
            Cbousers.DisplayMember = "NAME"
            Cbousers.ValueMember = "ID"
        Catch
        End Try
    End Sub
    Public Shared Function GetUsersDatasetOrderbyName() As DataSet
        Dim ds As New DataSet
        Try
            Dim sql As String = "Select * from usrtable order by NAME"
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Catch
        End Try
        Return ds
    End Function

    Public Event Usuario(ByVal user As Int32)
    Private Sub Cbousers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cbousers.SelectedIndexChanged

    End Sub
    Private Sub btnaccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaccept.Click
        RaiseEvent Usuario(Cbousers.SelectedValue)
        Me.Close()
    End Sub
End Class
