Public Class UCRegularExpression
    Inherits Zamba.AppBlock.ZControl

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
    Friend WithEvents chkExpresion As System.Windows.Forms.CheckBox
    Friend WithEvents GB As GroupBox
    Friend WithEvents CboExpresiones As ComboBox
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents txtuserexpression As TextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents btnsave As ZButton
    Friend WithEvents GPHelp As GroupBox
    Friend WithEvents lblHelp As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        GB = New GroupBox
        btnsave = New ZButton
        Label2 = New ZLabel
        txtuserexpression = New TextBox
        Label1 = New ZLabel
        CboExpresiones = New ComboBox
        chkExpresion = New System.Windows.Forms.CheckBox
        GPHelp = New GroupBox
        lblHelp = New ZLabel
        GB.SuspendLayout()
        GPHelp.SuspendLayout()
        SuspendLayout()
        '
        'GB
        '
        GB.BackColor = Color.Transparent
        GB.Controls.Add(btnsave)
        GB.Controls.Add(Label2)
        GB.Controls.Add(txtuserexpression)
        GB.Controls.Add(Label1)
        GB.Controls.Add(CboExpresiones)
        GB.Controls.Add(chkExpresion)
        GB.Location = New Point(16, 16)
        GB.Name = "GB"
        GB.Size = New Size(376, 152)
        GB.TabIndex = 0
        GB.TabStop = False
        GB.Text = "Expresiones Regulares"
        '
        'btnsave
        '
        btnsave.DialogResult = System.Windows.Forms.DialogResult.None
        btnsave.Location = New Point(288, 112)
        btnsave.Name = "btnsave"
        btnsave.Size = New Size(80, 24)
        btnsave.TabIndex = 5
        btnsave.Text = "Guardar"
        '
        'Label2
        '
        Label2.BorderStyle = BorderStyle.FixedSingle
        Label2.Location = New Point(24, 96)
        Label2.Name = "Label2"
        Label2.Size = New Size(72, 24)
        Label2.TabIndex = 4
        Label2.Text = "Crear"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        Label2.Visible = False
        '
        'txtuserexpression
        '
        txtuserexpression.Location = New Point(104, 96)
        txtuserexpression.Name = "txtuserexpression"
        txtuserexpression.Size = New Size(144, 21)
        txtuserexpression.TabIndex = 3
        txtuserexpression.Text = ""
        txtuserexpression.Visible = False
        '
        'Label1
        '
        Label1.BorderStyle = BorderStyle.FixedSingle
        Label1.Location = New Point(24, 64)
        Label1.Name = "Label1"
        Label1.Size = New Size(72, 24)
        Label1.TabIndex = 2
        Label1.Text = "Seleccionar"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        '
        'CboExpresiones
        '
        CboExpresiones.Items.AddRange(New Object() {"MAIL (@)", "WEB (WWW)", "Crear"})
        CboExpresiones.Location = New Point(104, 64)
        CboExpresiones.Name = "CboExpresiones"
        CboExpresiones.Size = New Size(144, 21)
        CboExpresiones.TabIndex = 1
        '
        'chkExpresion
        '
        chkExpresion.Location = New Point(24, 32)
        chkExpresion.Name = "chkExpresion"
        chkExpresion.Size = New Size(240, 24)
        chkExpresion.TabIndex = 0
        chkExpresion.Text = "Asignar una Expresión Regular"
        '
        'GPHelp
        '
        GPHelp.BackColor = Color.Transparent
        GPHelp.Controls.Add(lblHelp)
        GPHelp.Location = New Point(16, 184)
        GPHelp.Name = "GPHelp"
        GPHelp.Size = New Size(376, 80)
        GPHelp.TabIndex = 1
        GPHelp.TabStop = False
        GPHelp.Text = "Funcionalidad"
        '
        'lblHelp
        '
        lblHelp.Location = New Point(24, 24)
        lblHelp.Name = "lblHelp"
        lblHelp.Size = New Size(336, 40)
        lblHelp.TabIndex = 0
        '
        'UCRegularExpression
        '
        Controls.Add(GPHelp)
        Controls.Add(GB)
        Name = "UCRegularExpression"
        Size = New Size(408, 304)
        GB.ResumeLayout(False)
        GPHelp.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

#Region "Eventos"
    Public Event ExpresionUsuario(ByVal exp As String)
    Public Event ExpresionSistema(ByVal exp As String)
#End Region
    Private Sub CboExpresiones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CboExpresiones.SelectedIndexChanged
        If CboExpresiones.Text.Trim = "Crear" Then
            Label2.Visible = True
            txtuserexpression.Visible = True
        Else
            Label2.Visible = False
            txtuserexpression.Visible = False
        End If
        ShowHelp()
    End Sub
    Private Sub ShowHelp()
        Select Case CboExpresiones.Text.ToUpper
            Case "MAIL (@)"
                lblHelp.Text = "Verifica que los valores ingresados sean del tipo mail." & ControlChars.NewLine & "Ej:  xxxx@xx.com"
            Case "WEB(WWW)"
                lblHelp.Text = "Verifica que los valores ingresados sean del tipo URL." & ControlChars.NewLine & "Ej:  www.xxxx.com"
            Case "CREAR"
                lblHelp.Text = "Expresion generada por el usuario"
        End Select
    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnsave.Click
        If txtuserexpression.Text.Trim = String.Empty Then
            RaiseEvent ExpresionSistema(CboExpresiones.Text)
        Else
            RaiseEvent ExpresionUsuario(txtuserexpression.Text.Trim)
        End If
    End Sub
End Class
