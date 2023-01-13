Public Class UCIfTimePass
    'Los controles de Reglas de Accion deben heredar de ZRuleControl
    Inherits ZRuleControl

    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents NUDMinute As System.Windows.Forms.NumericUpDown
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents NUDDay As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents NUDHour As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents NUDWeek As System.Windows.Forms.NumericUpDown
#Region " Código generado por el Diseñador de Windows Forms "

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        btnAceptar = New ZButton()
        NUDMinute = New System.Windows.Forms.NumericUpDown()
        Label1 = New ZLabel()
        Label2 = New ZLabel()
        Label3 = New ZLabel()
        NUDDay = New System.Windows.Forms.NumericUpDown()
        Label4 = New ZLabel()
        NUDHour = New System.Windows.Forms.NumericUpDown()
        Label5 = New ZLabel()
        NUDWeek = New System.Windows.Forms.NumericUpDown()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        CType(NUDMinute, ComponentModel.ISupportInitialize).BeginInit()
        CType(NUDDay, ComponentModel.ISupportInitialize).BeginInit()
        CType(NUDHour, ComponentModel.ISupportInitialize).BeginInit()
        CType(NUDWeek, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(Label5)
        tbRule.Controls.Add(NUDWeek)
        tbRule.Controls.Add(Label4)
        tbRule.Controls.Add(NUDHour)
        tbRule.Controls.Add(Label3)
        tbRule.Controls.Add(NUDDay)
        tbRule.Controls.Add(Label2)
        tbRule.Controls.Add(Label1)
        tbRule.Controls.Add(NUDMinute)
        tbRule.Controls.Add(btnAceptar)
        tbRule.Size = New System.Drawing.Size(768, 510)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(776, 539)
        '
        'btnAceptar
        '
        btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAceptar.FlatStyle = FlatStyle.Flat
        btnAceptar.ForeColor = System.Drawing.Color.White
        btnAceptar.Location = New System.Drawing.Point(256, 250)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(120, 25)
        btnAceptar.TabIndex = 3
        btnAceptar.Text = "Guardar"
        btnAceptar.UseVisualStyleBackColor = False
        '
        'NUDMinute
        '
        NUDMinute.Location = New System.Drawing.Point(178, 62)
        NUDMinute.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        NUDMinute.Name = "NUDMinute"
        NUDMinute.Size = New System.Drawing.Size(80, 23)
        NUDMinute.TabIndex = 5
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(24, 14)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(106, 16)
        Label1.TabIndex = 6
        Label1.Text = "Ejecutar cada:"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(96, 62)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(65, 16)
        Label2.TabIndex = 7
        Label2.Text = "Minutos:"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(96, 145)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(41, 16)
        Label3.TabIndex = 9
        Label3.Text = "Días:"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'NUDDay
        '
        NUDDay.Location = New System.Drawing.Point(178, 145)
        NUDDay.Maximum = New Decimal(New Integer() {6, 0, 0, 0})
        NUDDay.Name = "NUDDay"
        NUDDay.Size = New System.Drawing.Size(77, 23)
        NUDDay.TabIndex = 8
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label4.Location = New System.Drawing.Point(96, 104)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(51, 16)
        Label4.TabIndex = 11
        Label4.Text = "Horas:"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        '
        'NUDHour
        '
        NUDHour.Location = New System.Drawing.Point(178, 104)
        NUDHour.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        NUDHour.Name = "NUDHour"
        NUDHour.Size = New System.Drawing.Size(77, 23)
        NUDHour.TabIndex = 10
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label5.Location = New System.Drawing.Point(96, 187)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(73, 16)
        Label5.TabIndex = 13
        Label5.Text = "Semanas:"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        '
        'NUDWeek
        '
        NUDWeek.Location = New System.Drawing.Point(178, 187)
        NUDWeek.Maximum = New Decimal(New Integer() {52, 0, 0, 0})
        NUDWeek.Name = "NUDWeek"
        NUDWeek.Size = New System.Drawing.Size(77, 23)
        NUDWeek.TabIndex = 12
        '
        'UCIfTimePass
        '
        BackColor = System.Drawing.Color.Gainsboro
        Name = "UCIfTimePass"
        Size = New System.Drawing.Size(776, 539)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        CType(NUDMinute, ComponentModel.ISupportInitialize).EndInit()
        CType(NUDDay, ComponentModel.ISupportInitialize).EndInit()
        CType(NUDHour, ComponentModel.ISupportInitialize).EndInit()
        CType(NUDWeek, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

#End Region

    'El New debe recibir la regla a configurar
    Public Sub New(ByVal CurrentRule As IIfTimePass, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        this_Load()
    End Sub

    'Para guardar los parametros de una regla se usa el siguiente metodo, al que se le pasa:
    'la regla en si, un item y el valor
    'WFRulesBusiness.UpdateParamItemAction(CurrentRule, 0, StepItem.WFStep.Id)

    'Se llama a este metodo para actualizar el nombre en el administrador al finalizar la configuracion
    'Me.RaiseUpdateMaskName()

    ''' <summary>
    ''' Carga los valores ya asignados a la regla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub this_Load()
        Try
            NUDMinute.Value = MyRule.Minute
            NUDHour.Value = MyRule.Hour
            NUDDay.Value = MyRule.Day
            NUDWeek.Value = MyRule.Week
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shadows ReadOnly Property MyRule() As IIfTimePass
        Get
            Return DirectCast(Rule, IIfTimePass)
        End Get
    End Property

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Try
            If (MyRule.Minute <> NUDMinute.Value) Then
                MyRule.Minute = NUDMinute.Value
                WFRulesBusiness.UpdateParamItem(Rule, 0, MyRule.Minute)
            End If
            If (MyRule.Hour <> NUDHour.Value) Then
                MyRule.Hour = NUDHour.Value
                WFRulesBusiness.UpdateParamItem(Rule.ID, 1, MyRule.Hour)
            End If
            If (MyRule.Day <> NUDDay.Value) Then
                MyRule.Day = NUDDay.Value
                WFRulesBusiness.UpdateParamItem(Rule.ID, 2, MyRule.Day)
            End If
            If (MyRule.Week <> NUDWeek.Value) Then
                MyRule.Week = NUDWeek.Value
                WFRulesBusiness.UpdateParamItem(Rule.ID, 3, MyRule.Week)
            End If
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
End Class

