Imports Zamba.Core

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
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents btnReintentar As System.Windows.Forms.Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lbldetails As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmWaitConnection))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.btnReintentar = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lbldetails = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.LinkLabel1)
        Me.Panel1.Controls.Add(Me.btnReintentar)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.BtnCancel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(482, 128)
        Me.Panel1.TabIndex = 0
        '
        'LinkLabel1
        '
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LinkLabel1.Location = New System.Drawing.Point(357, 59)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(112, 16)
        Me.LinkLabel1.TabIndex = 5
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "mas informacion..."
        '
        'btnReintentar
        '
        Me.btnReintentar.BackColor = System.Drawing.Color.Silver
        Me.btnReintentar.Location = New System.Drawing.Point(313, 85)
        Me.btnReintentar.Name = "btnReintentar"
        Me.btnReintentar.Size = New System.Drawing.Size(75, 23)
        Me.btnReintentar.TabIndex = 4
        Me.btnReintentar.Text = "Reintentar"
        Me.btnReintentar.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(177, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 23)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "60"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(160, 23)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Reintentando en..."
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(466, 74)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Zamba no se ha podido conectar al servidor de base de datos debido a que esté no " & _
            "se encuentra disponible." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Comuniquese por favor con el departamento de Sistemas." & _
            ""
        '
        'BtnCancel
        '
        Me.BtnCancel.BackColor = System.Drawing.Color.Silver
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Location = New System.Drawing.Point(394, 85)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 0
        Me.BtnCancel.Text = "Cancelar"
        Me.BtnCancel.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.lbldetails)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 128)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(482, 0)
        Me.Panel2.TabIndex = 1
        '
        'lbldetails
        '
        Me.lbldetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbldetails.Location = New System.Drawing.Point(0, 0)
        Me.lbldetails.Name = "lbldetails"
        Me.lbldetails.Size = New System.Drawing.Size(482, 0)
        Me.lbldetails.TabIndex = 0
        '
        'FrmWaitConnection
        '
        Me.AcceptButton = Me.btnReintentar
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(482, 118)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmWaitConnection"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Zamba - Servidor inaccesible"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    '  Private ex As Exception

    Public Sub New(ByVal ex As Exception)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        '     Me.ex = ex
        Try
            Me.lbldetails.Text = ex.ToString & " " & ex.InnerException.ToString
        Catch ex1 As Exception
            Me.lbldetails.Text = ex.ToString
        End Try

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Timer1 = New Threading.Timer(TCB, State, 0, Interval)
    End Sub
    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Try
            Timer1.Dispose()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

        End Try
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
    Dim FlagDetails As Boolean = True
    Dim Count As Int32 = 60

    Dim Interval As Int16 = 1000

    Dim State As Object

    Dim Timer1 As Threading.Timer

    Dim TCB As New Threading.TimerCallback(AddressOf CallBack)

    Delegate Sub DSetLabel3Text(ByVal Text As String)

    Private Sub SetLabel3Text(ByVal Text As String)
        Dim D1 As New DSetLabel3Text(AddressOf DelegateSetLabel3Text)
        Me.Invoke(D1, New Object() {Text})
    End Sub

    Private Sub DelegateSetLabel3Text(ByVal Text As String)
        Me.Label3.Text = Text
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
        Me.Invoke(D1)
    End Sub
    Private Sub CloseFrmWait()
        Me.Timer1.Dispose()
        Me.Close()
    End Sub



    Sub ShowDetails()
        If FlagDetails Then
            FlagDetails = False
            Me.Height = 350
        Else
            FlagDetails = True
            Me.Height = 140
        End If
    End Sub

    Private Sub btnReintentar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReintentar.Click
        DelegateClosing()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.ShowDetails()
    End Sub

End Class
