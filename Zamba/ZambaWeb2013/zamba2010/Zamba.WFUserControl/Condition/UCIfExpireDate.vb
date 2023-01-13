'Imports Zamba.WFBusiness

Public Class UCIfExpireDate
    Inherits ZRuleControl

    Private _myRule As IIfExpireDate

#Region " Código generado por el Diseñador de Windows Forms "

    'Public Sub New()
    '    MyBase.New()

    '    'El Diseñador de Windows Forms requiere esta llamada.
    '    InitializeComponent()

    '    'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    'End Sub

    Public Sub New(ByVal ExpireDate As IIfExpireDate, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(ExpireDate, _wfPanelCircuit)
        InitializeComponent()
        _myRule = ExpireDate
        'Selecciono el tipo de comparación y seteo las horas en los controles
        UCIfExpireDate_Load()
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
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RdoExpiro As System.Windows.Forms.RadioButton
    Friend WithEvents RdoNoExpiro As System.Windows.Forms.RadioButton
    Friend WithEvents RdoVenceEn As System.Windows.Forms.RadioButton
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents RdoVencidoPor As System.Windows.Forms.RadioButton
    Friend WithEvents CtrlHorasPor As System.Windows.Forms.NumericUpDown
    Friend WithEvents CtrlHorasEn As System.Windows.Forms.NumericUpDown

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        btnAceptar = New ZButton
        GroupBox1 = New GroupBox
        Label2 = New ZLabel
        CtrlHorasPor = New System.Windows.Forms.NumericUpDown
        RdoVencidoPor = New System.Windows.Forms.RadioButton
        RdoVenceEn = New System.Windows.Forms.RadioButton
        RdoNoExpiro = New System.Windows.Forms.RadioButton
        RdoExpiro = New System.Windows.Forms.RadioButton
        CtrlHorasEn = New System.Windows.Forms.NumericUpDown
        Label1 = New ZLabel
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        GroupBox1.SuspendLayout()
        CType(CtrlHorasPor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(CtrlHorasEn, System.ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(GroupBox1)
        tbRule.Controls.Add(btnAceptar)
        tbRule.Size = New System.Drawing.Size(399, 308)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(407, 334)
        '
        'btnAceptar
        '
        btnAceptar.Location = New System.Drawing.Point(146, 268)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(70, 23)
        btnAceptar.TabIndex = 18
        btnAceptar.Text = "Aceptar"
        '
        'GroupBox1
        '
        GroupBox1.BackColor = System.Drawing.Color.Transparent
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(CtrlHorasPor)
        GroupBox1.Controls.Add(RdoVencidoPor)
        GroupBox1.Controls.Add(RdoVenceEn)
        GroupBox1.Controls.Add(RdoNoExpiro)
        GroupBox1.Controls.Add(RdoExpiro)
        GroupBox1.Controls.Add(CtrlHorasEn)
        GroupBox1.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        GroupBox1.Location = New System.Drawing.Point(48, 48)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(252, 195)
        GroupBox1.TabIndex = 19
        GroupBox1.TabStop = False
        GroupBox1.Text = "Opciones"
        GroupBox1.Controls.SetChildIndex(CtrlHorasEn, 0)
        GroupBox1.Controls.SetChildIndex(RdoExpiro, 0)
        GroupBox1.Controls.SetChildIndex(RdoNoExpiro, 0)
        GroupBox1.Controls.SetChildIndex(RdoVenceEn, 0)
        GroupBox1.Controls.SetChildIndex(RdoVencidoPor, 0)
        GroupBox1.Controls.SetChildIndex(CtrlHorasPor, 0)
        GroupBox1.Controls.SetChildIndex(Label2, 0)
        '
        'Label2
        '
        Label2.Location = New System.Drawing.Point(216, 148)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(32, 24)
        Label2.TabIndex = 22
        Label2.Text = "Hs"
        '
        'CtrlHorasPor
        '
        CtrlHorasPor.DecimalPlaces = 2
        CtrlHorasPor.Enabled = False
        CtrlHorasPor.Location = New System.Drawing.Point(152, 148)
        CtrlHorasPor.Name = "CtrlHorasPor"
        CtrlHorasPor.Size = New System.Drawing.Size(64, 23)
        CtrlHorasPor.TabIndex = 4
        CtrlHorasPor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RdoVencidoPor
        '
        RdoVencidoPor.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        RdoVencidoPor.Location = New System.Drawing.Point(16, 140)
        RdoVencidoPor.Name = "RdoVencidoPor"
        RdoVencidoPor.Size = New System.Drawing.Size(128, 32)
        RdoVencidoPor.TabIndex = 3
        RdoVencidoPor.Text = "Vencido por:"
        '
        'RdoVenceEn
        '
        RdoVenceEn.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        RdoVenceEn.Location = New System.Drawing.Point(16, 104)
        RdoVenceEn.Name = "RdoVenceEn"
        RdoVenceEn.Size = New System.Drawing.Size(112, 32)
        RdoVenceEn.TabIndex = 2
        RdoVenceEn.Text = "Vence en:"
        '
        'RdoNoExpiro
        '
        RdoNoExpiro.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        RdoNoExpiro.Location = New System.Drawing.Point(16, 74)
        RdoNoExpiro.Name = "RdoNoExpiro"
        RdoNoExpiro.Size = New System.Drawing.Size(120, 32)
        RdoNoExpiro.TabIndex = 1
        RdoNoExpiro.Text = "No Vencio"
        '
        'RdoExpiro
        '
        RdoExpiro.Checked = True
        RdoExpiro.Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        RdoExpiro.Location = New System.Drawing.Point(16, 40)
        RdoExpiro.Name = "RdoExpiro"
        RdoExpiro.Size = New System.Drawing.Size(88, 32)
        RdoExpiro.TabIndex = 0
        RdoExpiro.TabStop = True
        RdoExpiro.Text = "Vencio"
        '
        'CtrlHorasEn
        '
        CtrlHorasEn.DecimalPlaces = 2
        CtrlHorasEn.Enabled = False
        CtrlHorasEn.Location = New System.Drawing.Point(152, 112)
        CtrlHorasEn.Name = "CtrlHorasEn"
        CtrlHorasEn.Size = New System.Drawing.Size(64, 23)
        CtrlHorasEn.TabIndex = 20
        CtrlHorasEn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Label1.Location = New System.Drawing.Point(216, 144)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(32, 24)
        Label1.TabIndex = 21
        Label1.Text = "Hs"
        '
        'UCIfExpireDate
        '
        Name = "UCIfExpireDate"
        Size = New System.Drawing.Size(407, 334)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        CType(CtrlHorasPor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(CtrlHorasEn, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        'Asigno el tipo de comparación y la cantidad de horas
        If (RdoExpiro.Checked) Then
            _myRule.Comparacion() = Comparaciones.Vencio
            _myRule.CantidadHoras() = 0
        ElseIf RdoNoExpiro.Checked Then
            _myRule.Comparacion() = Comparaciones.No_Vencio
            _myRule.CantidadHoras() = 0
        ElseIf RdoVenceEn.Checked Then
            _myRule.Comparacion() = Comparaciones.Vence_En
            _myRule.CantidadHoras() = CtrlHorasEn.Value
        ElseIf (RdoVencidoPor.Checked) Then
            _myRule.Comparacion() = Comparaciones.Vencido_Por
            _myRule.CantidadHoras() = Decimal.ToDouble(CtrlHorasPor.Value)
        End If

        'Actualizo el objeto Exp
        Try
            WFRulesBusiness.UpdateParamItem(_myRule.ID, 0, _myRule.Comparacion)
            WFRulesBusiness.UpdateParamItem(_myRule.ID, 1, _myRule.CantidadHoras)
            UserBusiness.Rights.SaveAction(_myRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & _myRule.Name & "(" & _myRule.ID & ")")
        Catch ex As Exception
            Dim exn As New Exception("Error en UCIfExpireDate.btnAceptar_Click(...), excepción" & ex.ToString)
            zamba.core.zclass.raiseerror(exn)
            MessageBox.Show("Error al actualizar los valores, excepción " & ex.Message, "Zamba - WorkFlow - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RdoVenceEn_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles RdoVenceEn.CheckedChanged
        CtrlHorasEn.Enabled = RdoVenceEn.Checked
    End Sub

    Private Sub RdoVencidoPor_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles RdoVencidoPor.CheckedChanged
        CtrlHorasPor.Enabled = RdoVencidoPor.Checked
    End Sub

    Private Sub UCIfExpireDate_Load()
        'Selecciono el tipo de comparación y seteo las horas en los controles
        Select Case _myRule.Comparacion
            Case Comparaciones.No_Vencio
                RdoNoExpiro.Checked = True
            Case Comparaciones.Vence_En
                RdoVenceEn.Checked = True
                CtrlHorasEn.Value = _myRule.CantidadHoras
            Case Comparaciones.Vencido_Por
                RdoVencidoPor.Checked = True
                CtrlHorasPor.Value = _myRule.CantidadHoras
            Case Comparaciones.Vencio
                RdoExpiro.Checked = True
        End Select
    End Sub

    Public Shadows ReadOnly Property MyRule() As IIfExpireDate
        Get
            Return DirectCast(Rule, IIfExpireDate)
        End Get
    End Property
End Class
