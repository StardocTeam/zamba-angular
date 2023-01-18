Public Class UCRegularExpression
    Inherits Zamba.AppBlock.ZBlueControl

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
    Friend WithEvents GB As System.Windows.Forms.GroupBox
    Friend WithEvents CboExpresiones As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtuserexpression As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnsave As Zamba.AppBlock.ZButton
    Friend WithEvents GPHelp As System.Windows.Forms.GroupBox
    Friend WithEvents lblHelp As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GB = New System.Windows.Forms.GroupBox
        Me.btnsave = New Zamba.AppBlock.ZButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtuserexpression = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.CboExpresiones = New System.Windows.Forms.ComboBox
        Me.chkExpresion = New System.Windows.Forms.CheckBox
        Me.GPHelp = New System.Windows.Forms.GroupBox
        Me.lblHelp = New System.Windows.Forms.Label
        Me.GB.SuspendLayout()
        Me.GPHelp.SuspendLayout()
        Me.SuspendLayout()
        '
        'GB
        '
        Me.GB.BackColor = System.Drawing.Color.Transparent
        Me.GB.Controls.Add(Me.btnsave)
        Me.GB.Controls.Add(Me.Label2)
        Me.GB.Controls.Add(Me.txtuserexpression)
        Me.GB.Controls.Add(Me.Label1)
        Me.GB.Controls.Add(Me.CboExpresiones)
        Me.GB.Controls.Add(Me.chkExpresion)
        Me.GB.Location = New System.Drawing.Point(16, 16)
        Me.GB.Name = "GB"
        Me.GB.Size = New System.Drawing.Size(376, 152)
        Me.GB.TabIndex = 0
        Me.GB.TabStop = False
        Me.GB.Text = "Expresiones Regulares"
        '
        'btnsave
        '
        Me.btnsave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnsave.Location = New System.Drawing.Point(288, 112)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(80, 24)
        Me.btnsave.TabIndex = 5
        Me.btnsave.Text = "Guardar"
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(24, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 24)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Crear"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label2.Visible = False
        '
        'txtuserexpression
        '
        Me.txtuserexpression.Location = New System.Drawing.Point(104, 96)
        Me.txtuserexpression.Name = "txtuserexpression"
        Me.txtuserexpression.Size = New System.Drawing.Size(144, 21)
        Me.txtuserexpression.TabIndex = 3
        Me.txtuserexpression.Text = ""
        Me.txtuserexpression.Visible = False
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(24, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 24)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Seleccionar"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CboExpresiones
        '
        Me.CboExpresiones.Items.AddRange(New Object() {"MAIL (@)", "WEB (WWW)", "Crear"})
        Me.CboExpresiones.Location = New System.Drawing.Point(104, 64)
        Me.CboExpresiones.Name = "CboExpresiones"
        Me.CboExpresiones.Size = New System.Drawing.Size(144, 21)
        Me.CboExpresiones.TabIndex = 1
        '
        'chkExpresion
        '
        Me.chkExpresion.Location = New System.Drawing.Point(24, 32)
        Me.chkExpresion.Name = "chkExpresion"
        Me.chkExpresion.Size = New System.Drawing.Size(240, 24)
        Me.chkExpresion.TabIndex = 0
        Me.chkExpresion.Text = "Asignar una Expresión Regular"
        '
        'GPHelp
        '
        Me.GPHelp.BackColor = System.Drawing.Color.Transparent
        Me.GPHelp.Controls.Add(Me.lblHelp)
        Me.GPHelp.Location = New System.Drawing.Point(16, 184)
        Me.GPHelp.Name = "GPHelp"
        Me.GPHelp.Size = New System.Drawing.Size(376, 80)
        Me.GPHelp.TabIndex = 1
        Me.GPHelp.TabStop = False
        Me.GPHelp.Text = "Funcionalidad"
        '
        'lblHelp
        '
        Me.lblHelp.Location = New System.Drawing.Point(24, 24)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(336, 40)
        Me.lblHelp.TabIndex = 0
        '
        'UCRegularExpression
        '
        Me.Controls.Add(Me.GPHelp)
        Me.Controls.Add(Me.GB)
        Me.Name = "UCRegularExpression"
        Me.Size = New System.Drawing.Size(408, 304)
        Me.GB.ResumeLayout(False)
        Me.GPHelp.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Eventos"
    Public Event ExpresionUsuario(ByVal exp As String)
    Public Event ExpresionSistema(ByVal exp As String)
#End Region
    Private Sub CboExpresiones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboExpresiones.SelectedIndexChanged
        If Me.CboExpresiones.Text.Trim = "Crear" Then
            Me.Label2.Visible = True
            Me.txtuserexpression.Visible = True
        Else
            Me.Label2.Visible = False
            Me.txtuserexpression.Visible = False
        End If
        ShowHelp()
    End Sub
    Private Sub ShowHelp()
        Select Case Me.CboExpresiones.Text.ToUpper
            Case "MAIL (@)"
                Me.lblHelp.Text = "Verifica que los valores ingresados sean del tipo mail." & ControlChars.NewLine & "Ej:  xxxx@xx.com"
            Case "WEB(WWW)"
                Me.lblHelp.Text = "Verifica que los valores ingresados sean del tipo URL." & ControlChars.NewLine & "Ej:  www.xxxx.com"
            Case "CREAR"
                Me.lblHelp.Text = "Expresion generada por el usuario"
        End Select
    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If Me.txtuserexpression.Text.Trim = String.Empty Then
            RaiseEvent ExpresionSistema(Me.CboExpresiones.Text)
        Else
            RaiseEvent ExpresionUsuario(Me.txtuserexpression.Text.Trim)
        End If
    End Sub
End Class
