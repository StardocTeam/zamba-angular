Imports ZAMBA.Servers
Imports Zamba.AppBlock
Public Class UcWhere
    Inherits System.Windows.Forms.UserControl
    Dim chk As Windows.Forms.CheckBox
    Dim txt As Windows.Forms.TextBox
    'Dim con1, con2 As Zamba.Servers.IConnection
    Public key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Public iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Public Shared Event AddWhereColumn(ByVal Column As String)

#Region " Código generado por el Diseñador de Windows Forms "
    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
    End Sub
    Public Sub New(ByVal Columnas As ArrayList)
        MyBase.New()
        InitializeComponent()
        'con1 = _con
        LoadWhere(Columnas)
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
    Friend WithEvents Panel1 As ZBluePanel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New ZBluePanel
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel1.Location = New System.Drawing.Point(8, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(360, 176)
        Me.Panel1.TabIndex = 0
        '
        'UcWhere
        '
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UcWhere"
        Me.Size = New System.Drawing.Size(375, 240)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public Shared Event Removercolum(ByVal column As String)

    Private Sub AddToWhere(ByVal sender As Object, ByVal e As EventArgs)
        If Convert.ToBoolean(sender.checked) Then
            RaiseEvent AddWhereColumn(sender.text.ToString())
        Else
            RaiseEvent Removercolum(sender.text.ToString)
        End If
    End Sub
    Private Sub LoadWhere(ByVal columnas As ArrayList)
        Dim i As Int32
        Dim Iposition As New System.Drawing.Point
        Try
            'Dim dscon As New DsConfig
            'dscon.ReadXml(".\zdbconfig.xml")
            'Dim server As New server
            'server.MakeConnection()
            ' con1 = server.Con
            'server.dispose()
            'server = New server
            'server.MakeConnection(dscon.DsConfig.Rows(0).Item("DBType"), dscon.DsConfig.Rows(0).Item("Server"), dscon.DsConfig.Rows(0).Item("DataBase"), dscon.DsConfig.Rows(0).Item("User"), ZAMBA.Crypto.Encryption.DecryptString(dscon.DsConfig.Rows(0).Item("Password"), key, iv))
            'con2 = server.Con
            Dim j As Int32 = 0
            Dim k As Int32 = 1

            For i = 0 To columnas.Count - 1
                chk = New Windows.Forms.CheckBox
                'txt = New Windows.Forms.TextBox
                chk.SetBounds(Iposition.X, Iposition.Y, 140, 24)
                chk.Text = columnas(i).ToString()
                chk.Name = j.ToString() 'i.ToString
                'txt.Name = k '(i + 1).ToString
                j += 2
                k += 2
                'txt.Enabled = False
                Me.Panel1.Controls.Add(chk)
                Iposition.X += chk.Width + 5
                ' txt.SetBounds(Iposition.X, Iposition.Y, 120, 24)
                Me.Panel1.Controls.Add(txt)
                Iposition.X -= chk.Width + 5
                Iposition.Y += 30
                RemoveHandler chk.CheckedChanged, AddressOf AddToWhere
                AddHandler chk.CheckedChanged, AddressOf AddToWhere
            Next
        Catch
        End Try
    End Sub

    Private Sub UcWhere_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
