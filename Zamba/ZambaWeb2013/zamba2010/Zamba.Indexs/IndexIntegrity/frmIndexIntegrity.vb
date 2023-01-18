Imports Zamba.Core
Public Class frmIndexIntegrity
    'Inherits Zamba.appblock.zform
    Inherits Zamba.AppBlock.ZForm
    Implements iLog
    Private Const iAVISO As Int32 = 0
    Private Const iABORTO As Int32 = 1
    Private Const iERRONEO As Int32 = 2
    Private Const cENTER As String = Chr(13)


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
    Friend WithEvents ZBluePanel1 As Zamba.AppBlock.ZBluePanel
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnIniciar As Zamba.AppBlock.ZButton
    Friend WithEvents btnGenerarInforme As Zamba.AppBlock.ZButton
    Friend WithEvents btnAyuda As Zamba.AppBlock.ZButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmIndexIntegrity))
        Me.ZBluePanel1 = New Zamba.AppBlock.ZBluePanel
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnAyuda = New Zamba.AppBlock.ZButton
        Me.txtLog = New System.Windows.Forms.TextBox
        Me.btnGenerarInforme = New Zamba.AppBlock.ZButton
        Me.btnIniciar = New Zamba.AppBlock.ZButton
        Me.ZBluePanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ZIconList
        '

        '
        'ZBluePanel1
        '
        Me.ZBluePanel1.Controls.Add(Me.Label1)
        Me.ZBluePanel1.Controls.Add(Me.btnAyuda)
        Me.ZBluePanel1.Controls.Add(Me.txtLog)
        Me.ZBluePanel1.Controls.Add(Me.btnGenerarInforme)
        Me.ZBluePanel1.Controls.Add(Me.btnIniciar)
        Me.ZBluePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ZBluePanel1.Location = New System.Drawing.Point(2, 2)
        Me.ZBluePanel1.Name = "ZBluePanel1"
        Me.ZBluePanel1.Size = New System.Drawing.Size(628, 297)
        Me.ZBluePanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(8, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Log:"
        '
        'btnAyuda
        '
        Me.btnAyuda.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnAyuda.Location = New System.Drawing.Point(584, 258)
        Me.btnAyuda.Name = "btnAyuda"
        Me.btnAyuda.Size = New System.Drawing.Size(32, 35)
        Me.btnAyuda.TabIndex = 3
        Me.btnAyuda.Text = "?"
        '
        'txtLog
        '
        Me.txtLog.AcceptsReturn = True
        Me.txtLog.AcceptsTab = True
        Me.txtLog.CausesValidation = False
        Me.txtLog.Location = New System.Drawing.Point(8, 34)
        Me.txtLog.MaxLength = 65536
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ReadOnly = True
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtLog.Size = New System.Drawing.Size(608, 216)
        Me.txtLog.TabIndex = 2
        Me.txtLog.Text = ""
        Me.txtLog.WordWrap = False
        '
        'btnGenerarInforme
        '
        Me.btnGenerarInforme.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnGenerarInforme.Enabled = False
        Me.btnGenerarInforme.Location = New System.Drawing.Point(200, 258)
        Me.btnGenerarInforme.Name = "btnGenerarInforme"
        Me.btnGenerarInforme.Size = New System.Drawing.Size(184, 35)
        Me.btnGenerarInforme.TabIndex = 1
        Me.btnGenerarInforme.Text = "Generar informe"
        Me.btnGenerarInforme.Visible = False
        '
        'btnIniciar
        '
        Me.btnIniciar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnIniciar.Location = New System.Drawing.Point(8, 258)
        Me.btnIniciar.Name = "btnIniciar"
        Me.btnIniciar.Size = New System.Drawing.Size(184, 35)
        Me.btnIniciar.TabIndex = 0
        Me.btnIniciar.Text = "Comprobar Integridad"
        '
        'frmIndexIntegrity
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(632, 301)
        Me.Controls.Add(Me.ZBluePanel1)
        Me.DockPadding.All = 2
        Me.Name = "frmIndexIntegrity"
        Me.Text = "Zamba - Comprobar integridad de indices"
        Me.ZBluePanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnIniciar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIniciar.Click
        btnIniciar.Enabled = False
        Try
            Dim Int As New NegociosIntegridad
            Int.Log = Me
            Int.comprobarIntegridad()
        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.ToString, "Zamba - Comprobar integridad de índices")
            btnIniciar.Enabled = True
        End Try
        btnIniciar.Enabled = True
    End Sub

    Public Property ABORTO() As Integer Implements iLog.ABORTO
        Get
            Return iABORTO
        End Get
        Set(ByVal Value As Integer)

        End Set
    End Property

    Public Property AVISO() As Integer Implements iLog.AVISO
        Get
            Return iAVISO
        End Get
        Set(ByVal Value As Integer)

        End Set
    End Property

    Public Property ERRONEO() As Integer Implements iLog.ERRONEO
        Get
            Return iERRONEO
        End Get
        Set(ByVal Value As Integer)

        End Set
    End Property

    Public Function logMensaje(ByVal iTipo As Integer, ByVal sMensaje As String) As Boolean Implements iLog.logMensaje
        Dim sTipo As String = ""
        Select Case iTipo
            Case iAVISO
                sTipo = "AVISO: "
            Case iABORTO
                sTipo = "ABORTO: "
            Case iERRONEO
                sTipo = "ERROR: "
        End Select
        sMensaje = sTipo & sMensaje & vbCrLf
        txtLog.AppendText(sMensaje)

    End Function

    Private Sub ZBluePanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles ZBluePanel1.Paint

    End Sub

    Private Sub btnAyuda_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAyuda.Click
        Dim frmayuda As New frmAyuda
        frmayuda.ShowDialog()
    End Sub
End Class
