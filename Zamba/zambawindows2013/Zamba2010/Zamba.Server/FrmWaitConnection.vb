Imports Zamba.AppBlock

Public Class FrmWaitConnection
    Inherits System.Windows.Forms.Form

#Region " Código generado por el Diseñador de Windows Forms "

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
    Friend WithEvents Panel1 As Panel
    Friend WithEvents BtnCancel As ZButton
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents btnReintentar As ZButton
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lbldetails As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(FrmWaitConnection))
        Panel1 = New System.Windows.Forms.Panel()
        btnReintentar = New ZButton()
        Label3 = New ZLabel()
        Label2 = New ZLabel()
        Label1 = New ZLabel()
        BtnCancel = New ZButton()
        Panel2 = New System.Windows.Forms.Panel()
        lbldetails = New ZLabel()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.BackColor = System.Drawing.Color.White
        Panel1.Controls.Add(btnReintentar)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(BtnCancel)
        Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(682, 96)
        Panel1.TabIndex = 0
        '
        'btnReintentar
        '
        btnReintentar.BackColor = System.Drawing.Color.FromArgb(0, 157, 224)
        btnReintentar.FlatStyle = FlatStyle.Flat
        btnReintentar.ForeColor = System.Drawing.Color.White
        btnReintentar.Location = New System.Drawing.Point(307, 55)
        btnReintentar.Name = "btnReintentar"
        btnReintentar.Size = New System.Drawing.Size(75, 23)
        btnReintentar.TabIndex = 4
        btnReintentar.Text = "Reintentar"
        btnReintentar.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Label3.Font = New Font("Microsoft Sans Serif", 11.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label3.Location = New System.Drawing.Point(177, 56)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(32, 23)
        Label3.TabIndex = 3
        Label3.Text = "60"
        '
        'Label2
        '
        Label2.Font = New Font("Microsoft Sans Serif", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label2.Location = New System.Drawing.Point(12, 56)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(160, 23)
        Label2.TabIndex = 2
        Label2.Text = "Reintentando en..."
        '
        'Label1
        '
        Label1.Font = New Font("Microsoft Sans Serif", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label1.Location = New System.Drawing.Point(3, 8)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(667, 48)
        Label1.TabIndex = 1
        Label1.Text = "Zamba no se ha podido conectar al servidor de base de datos debido a que esté no " &
    "se encuentra disponible." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Comuniquese por favor con el departamento de Sistemas." &
    ""
        '
        'BtnCancel
        '
        BtnCancel.BackColor = System.Drawing.Color.FromArgb(0, 157, 224)
        BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        BtnCancel.FlatStyle = FlatStyle.Flat
        BtnCancel.ForeColor = System.Drawing.Color.White
        BtnCancel.Location = New System.Drawing.Point(408, 55)
        BtnCancel.Name = "BtnCancel"
        BtnCancel.Size = New System.Drawing.Size(75, 23)
        BtnCancel.TabIndex = 0
        BtnCancel.Text = "Cancelar"
        BtnCancel.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Panel2.BackColor = System.Drawing.Color.White
        Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Panel2.Controls.Add(lbldetails)
        Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Panel2.Location = New System.Drawing.Point(0, 96)
        Panel2.Name = "Panel2"
        Panel2.Size = New System.Drawing.Size(682, 190)
        Panel2.TabIndex = 1
        '
        'lbldetails
        '
        lbldetails.Dock = System.Windows.Forms.DockStyle.Fill
        lbldetails.FlatStyle = FlatStyle.Flat
        lbldetails.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        lbldetails.Location = New System.Drawing.Point(0, 0)
        lbldetails.Name = "lbldetails"
        lbldetails.Size = New System.Drawing.Size(678, 186)
        lbldetails.TabIndex = 0
        '
        'FrmWaitConnection
        '
        AcceptButton = btnReintentar
        AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        BackColor = System.Drawing.Color.White
        CancelButton = BtnCancel
        ClientSize = New System.Drawing.Size(682, 286)
        ControlBox = False
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "FrmWaitConnection"
        ShowInTaskbar = False
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "Zamba - Servidor inaccesible"
        TopMost = True
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    '  Private ex As Exception

    Public Sub New(ByVal ex As Exception)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        '     Me.ex = ex
        Try
            lbldetails.Text = ex.ToString & " " & ex.InnerException.ToString
        Catch ex1 As Exception
            lbldetails.Text = ex.ToString
        End Try

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Timer1 = New Threading.Timer(TCB, State, 0, Interval)
    End Sub
    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnCancel.Click
        Try
            Timer1.Dispose()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

        End Try
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Dim Count As Int32 = 60

    Dim Interval As Int16 = 1000

    Dim State As Object

    Dim Timer1 As Threading.Timer

    Dim TCB As New Threading.TimerCallback(AddressOf CallBack)

    Delegate Sub DSetLabel3Text(ByVal Text As String)

    Private Sub SetLabel3Text(ByVal Text As String)
        Dim D1 As New DSetLabel3Text(AddressOf DelegateSetLabel3Text)
        Invoke(D1, New Object() {Text})
    End Sub

    Private Sub DelegateSetLabel3Text(ByVal Text As String)
        Label3.Text = Text
    End Sub


    Private Sub CallBack(ByVal State As Object)
        Try
            Count += -1
            SetLabel3Text(Count)
            If Count <= 0 Then
                DelegateClosing()
            End If
        Catch ex As Threading.SynchronizationLockException
            Zamba.AppBlock.ZException.Log(ex)
        Catch ex As Threading.ThreadAbortException
            Zamba.AppBlock.ZException.Log(ex)
        Catch ex As Threading.ThreadInterruptedException
            Zamba.AppBlock.ZException.Log(ex)
        Catch ex As Threading.ThreadStateException
            Zamba.AppBlock.ZException.Log(ex)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub
    Delegate Sub DClose()
    Private Sub DelegateClosing()
        Dim D1 As New DClose(AddressOf CloseFrmWait)
        Invoke(D1)
    End Sub
    Private Sub CloseFrmWait()
        Timer1.Dispose()
        Close()
    End Sub

    Private Sub btnReintentar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnReintentar.Click
        DialogResult = DialogResult.OK
        DelegateClosing()
    End Sub


End Class
