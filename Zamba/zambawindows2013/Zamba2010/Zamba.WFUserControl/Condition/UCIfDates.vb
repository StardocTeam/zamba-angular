'Imports Zamba.WFBusiness

Public Class UCIfDates
    Inherits ZRuleControl

    Private IfDate As IIfDates
    Private Shared sFechas() As String = {TaskResult.DocumentDates.Creacion.ToString, TaskResult.DocumentDates.Edicion.ToString, TaskResult.DocumentDates.Entrada.ToString, TaskResult.DocumentDates.Salida.ToString}
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Private Shared sComparadores() As String = {"Igual", "Distinto", "Menor", "Mayor", "IgualMenor", "IgualMayor"} '{"=", "<>", ">", "<", ">=", "<="}

#Region " Código generado por el Diseñador de Windows Forms "

    'Public Sub New(ByVal rule As IIfDates)
    '    MyBase.New(rule)

    '    'El Diseñador de Windows Forms requiere esta llamada.
    '    InitializeComponent()

    '    'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    'End Sub

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
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents Comp As GroupBox
    Friend WithEvents rdoFechaFija As System.Windows.Forms.RadioButton
    Friend WithEvents rdoEspecifico As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDias As System.Windows.Forms.RadioButton
    Friend WithEvents rdoHoras As System.Windows.Forms.RadioButton
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents CboFechas As ComboBox
    Friend WithEvents CboComparadores As ComboBox
    Friend WithEvents CtrlFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboFechasComp As ComboBox
    Friend WithEvents CtrlDias As System.Windows.Forms.NumericUpDown
    Friend WithEvents CtrlHoras As System.Windows.Forms.NumericUpDown

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        CboFechas = New ComboBox()
        CboComparadores = New ComboBox()
        Label4 = New ZLabel()
        Comp = New GroupBox()
        cboFechasComp = New ComboBox()
        CtrlHoras = New System.Windows.Forms.NumericUpDown()
        CtrlDias = New System.Windows.Forms.NumericUpDown()
        CtrlFecha = New System.Windows.Forms.DateTimePicker()
        rdoHoras = New System.Windows.Forms.RadioButton()
        rdoDias = New System.Windows.Forms.RadioButton()
        rdoEspecifico = New System.Windows.Forms.RadioButton()
        rdoFechaFija = New System.Windows.Forms.RadioButton()
        btnAceptar = New ZButton()
        Label1 = New ZLabel()
        Label2 = New ZLabel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        Comp.SuspendLayout()
        CType(CtrlHoras, ComponentModel.ISupportInitialize).BeginInit()
        CType(CtrlDias, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(Label2)
        tbRule.Controls.Add(Label1)
        tbRule.Controls.Add(btnAceptar)
        tbRule.Controls.Add(Comp)
        tbRule.Controls.Add(Label4)
        tbRule.Controls.Add(CboComparadores)
        tbRule.Controls.Add(CboFechas)
        tbRule.Size = New System.Drawing.Size(518, 437)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(526, 466)
        '
        'CboFechas
        '
        CboFechas.Location = New System.Drawing.Point(136, 44)
        CboFechas.Name = "CboFechas"
        CboFechas.Size = New System.Drawing.Size(152, 24)
        CboFechas.TabIndex = 0
        CboFechas.Text = "Edición"
        '
        'CboComparadores
        '
        CboComparadores.Location = New System.Drawing.Point(136, 82)
        CboComparadores.Name = "CboComparadores"
        CboComparadores.Size = New System.Drawing.Size(247, 24)
        CboComparadores.TabIndex = 1
        '
        'Label4
        '
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label4.Location = New System.Drawing.Point(19, 81)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(100, 24)
        Label4.TabIndex = 3
        Label4.Text = "Comparación:"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        '
        'Comp
        '
        Comp.BackColor = System.Drawing.Color.Transparent
        Comp.Controls.Add(cboFechasComp)
        Comp.Controls.Add(CtrlHoras)
        Comp.Controls.Add(CtrlDias)
        Comp.Controls.Add(CtrlFecha)
        Comp.Controls.Add(rdoHoras)
        Comp.Controls.Add(rdoDias)
        Comp.Controls.Add(rdoEspecifico)
        Comp.Controls.Add(rdoFechaFija)
        Comp.Location = New System.Drawing.Point(48, 120)
        Comp.Name = "Comp"
        Comp.Size = New System.Drawing.Size(335, 208)
        Comp.TabIndex = 4
        Comp.TabStop = False
        Comp.Text = "Comparar contra"
        '
        'cboFechasComp
        '
        cboFechasComp.Enabled = False
        cboFechasComp.Location = New System.Drawing.Point(114, 76)
        cboFechasComp.Name = "cboFechasComp"
        cboFechasComp.Size = New System.Drawing.Size(200, 24)
        cboFechasComp.TabIndex = 18
        '
        'CtrlHoras
        '
        CtrlHoras.Enabled = False
        CtrlHoras.Location = New System.Drawing.Point(114, 167)
        CtrlHoras.Name = "CtrlHoras"
        CtrlHoras.Size = New System.Drawing.Size(56, 23)
        CtrlHoras.TabIndex = 6
        '
        'CtrlDias
        '
        CtrlDias.Enabled = False
        CtrlDias.Location = New System.Drawing.Point(114, 122)
        CtrlDias.Name = "CtrlDias"
        CtrlDias.Size = New System.Drawing.Size(56, 23)
        CtrlDias.TabIndex = 5
        '
        'CtrlFecha
        '
        CtrlFecha.Enabled = False
        CtrlFecha.Location = New System.Drawing.Point(114, 32)
        CtrlFecha.Name = "CtrlFecha"
        CtrlFecha.Size = New System.Drawing.Size(200, 23)
        CtrlFecha.TabIndex = 4
        '
        'rdoHoras
        '
        rdoHoras.Location = New System.Drawing.Point(16, 167)
        rdoHoras.Name = "rdoHoras"
        rdoHoras.Size = New System.Drawing.Size(88, 23)
        rdoHoras.TabIndex = 3
        rdoHoras.Text = "Horas"
        '
        'rdoDias
        '
        rdoDias.Location = New System.Drawing.Point(16, 122)
        rdoDias.Name = "rdoDias"
        rdoDias.Size = New System.Drawing.Size(88, 23)
        rdoDias.TabIndex = 2
        rdoDias.Text = "Días"
        '
        'rdoEspecifico
        '
        rdoEspecifico.Location = New System.Drawing.Point(16, 77)
        rdoEspecifico.Name = "rdoEspecifico"
        rdoEspecifico.Size = New System.Drawing.Size(96, 23)
        rdoEspecifico.TabIndex = 1
        rdoEspecifico.Text = "Especifico"
        '
        'rdoFechaFija
        '
        rdoFechaFija.Checked = True
        rdoFechaFija.Location = New System.Drawing.Point(16, 34)
        rdoFechaFija.Name = "rdoFechaFija"
        rdoFechaFija.Size = New System.Drawing.Size(104, 21)
        rdoFechaFija.TabIndex = 0
        rdoFechaFija.TabStop = True
        rdoFechaFija.Text = "Fecha Fija"
        '
        'btnAceptar
        '
        btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAceptar.FlatStyle = FlatStyle.Flat
        btnAceptar.ForeColor = System.Drawing.Color.White
        btnAceptar.Location = New System.Drawing.Point(309, 351)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(93, 30)
        btnAceptar.TabIndex = 17
        btnAceptar.Text = "Guardar"
        btnAceptar.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(45, 47)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(69, 16)
        Label1.TabIndex = 18
        Label1.Text = "Fecha de"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(306, 47)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(106, 16)
        Label2.TabIndex = 19
        Label2.Text = "del Documento"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'UCIfDates
        '
        Name = "UCIfDates"
        Size = New System.Drawing.Size(526, 466)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        Comp.ResumeLayout(False)
        CType(CtrlHoras, ComponentModel.ISupportInitialize).EndInit()
        CType(CtrlDias, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

#End Region

    Public Sub New(ByVal ID As IIfDates, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(ID, _wfPanelCircuit)
        InitializeComponent()
        IfDate = ID
    End Sub

    Private Sub rdoFechaFija_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdoFechaFija.CheckedChanged
        CtrlFecha.Enabled = rdoFechaFija.Checked
    End Sub

    Private Sub rdoEspecifico_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdoEspecifico.CheckedChanged
        cboFechasComp.Enabled = rdoEspecifico.Checked
    End Sub

    Private Sub rdoDias_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdoDias.CheckedChanged
        CtrlDias.Enabled = rdoDias.Checked
    End Sub

    Private Sub rdoHoras_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdoHoras.CheckedChanged
        CtrlHoras.Enabled = rdoHoras.Checked
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Dim i As Int32
        Dim sError As New System.Text.StringBuilder
        Dim sValor As String = String.Empty
        Dim iError As Int32 = 0

        'Asignamos el comparador
        i = comparadorAEnumerado(CboComparadores.Text)
        If i >= 0 Then
            IfDate.Comparador = i
        Else
            sError.Append("Verifique que el comparador seleccionado sea valido.")
            iError = 1
        End If

        'Asignamos la Fecha del documento
        i = fechaAEnumerado(CboFechas.Text)
        If i >= 0 Then
            IfDate.MiFecha = i
        Else
            sError.Append(Microsoft.VisualBasic.ControlChars.NewLine)
            sError.Append("Verifique que la Fecha del documento seleccionada sea valida")
            iError = 2
        End If

        'Asignamos la fecha por la cual comparar y el tipo de comparación
        If rdoFechaFija.Checked Then
            i = TipoComparaciones.Fija
            IfDate.FechaAComparar = CtrlFecha.Value
            sValor = CtrlFecha.Value.ToString
        ElseIf rdoEspecifico.Checked Then
            i = fechaAEnumerado(cboFechasComp.Text)
            If i >= 0 Then
                IfDate.FechaDocumentoComparar = i
            Else
                sError.Append(Microsoft.VisualBasic.ControlChars.NewLine)
                sError.Append("Verifique que la Fecha especifica seleccionada sea valida")
                iError = 4
            End If
            i = TipoComparaciones.Especifica
            sValor = cboFechasComp.SelectedItem
        ElseIf rdoDias.Checked Then
            i = TipoComparaciones.Dias
            IfDate.CantidadDias = CtrlDias.Value
            sValor = CtrlDias.Value.ToString
        ElseIf rdoHoras.Checked Then
            i = TipoComparaciones.Horas
            IfDate.CantidadHoras = CtrlHoras.Value
            sValor = CtrlHoras.Value.ToString
        Else
            sError.Append(Microsoft.VisualBasic.ControlChars.NewLine)
            sError.Append("Verifique que halla seleccionado un valor por el cual comparar valido")
            iError = 3
            i = -1
        End If

        If i >= 0 Then
            IfDate.TipoComparacion = i
        End If

        'Actualizamos IfDate
        If iError = 0 Then
            'FechaDoc 0, Comparador As Comparadores 1, 
            'TipoComparacion 2, ValorComparativo 3)
            i = IfDate.MiFecha
            WFRulesBusiness.UpdateParamItem(IfDate, 0, i)
            i = IfDate.Comparador
            WFRulesBusiness.UpdateParamItem(IfDate, 1, i)
            i = IfDate.TipoComparacion
            WFRulesBusiness.UpdateParamItem(IfDate, 2, i)
            WFRulesBusiness.UpdateParamItem(IfDate, 3, sValor)
            UserBusiness.Rights.SaveAction(IfDate.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & IfDate.Name & "(" & IfDate.ID & ")")
        Else
            MessageBox.Show(sError.ToString, "Zamba - WorkFlow - Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub


    Private Sub UCIfDates_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load
        CboFechas.Items.AddRange(sFechas)
        cboFechasComp.Items.AddRange(sFechas)
        CboComparadores.Items.AddRange(sComparadores)
        'Seleccionamos el intem correspodientes de los combobox
        'Fecha del documento para comparar
        Dim i As Int32 = -1
        Do
            i += 1
        Loop While i < sFechas.Length AndAlso String.Compare(IfDate.MiFecha.ToString, sFechas(i), True) <> 0
        CboFechas.SelectedItem = sFechas(i)
        'Comparador autilizar
        i = -1
        Do
            i += 1
        Loop While i < sComparadores.Length AndAlso String.Compare(IfDate.Comparador.ToString, sComparadores(i), True) <> 0

        CboComparadores.SelectedItem = sComparadores(i)
        Select Case IfDate.TipoComparacion
            Case TipoComparaciones.Dias
                rdoDias.Checked = True
                CtrlDias.Value = IfDate.CantidadDias
            Case TipoComparaciones.Especifica
                i = -1
                Do
                    i += 1
                Loop While i < sFechas.Length AndAlso String.Compare(IfDate.FechaDocumentoComparar.ToString, sFechas(i), True) <> 0
                cboFechasComp.SelectedItem = sFechas(i)
                rdoEspecifico.Checked = True
            Case TipoComparaciones.Fija
                rdoFechaFija.Checked = True
                CtrlFecha.Value = IfDate.FechaAComparar
            Case TipoComparaciones.Horas
                rdoHoras.Checked = True
                CtrlHoras.Value = IfDate.CantidadHoras
        End Select
    End Sub
    'CboComparador
    Private Shared Function comparadorAEnumerado(ByVal sComp As String) As Comparadores
        Select Case sComp
            Case "Igual"
                Return Comparadores.Igual
            Case "Distinto"
                Return Comparadores.Distinto
            Case "Mayor"
                Return Comparadores.Mayor
            Case "Menor"
                Return Comparadores.Menor
            Case "IgualMayor"
                Return Comparadores.IgualMayor
            Case "IgualMenor"
                Return Comparadores.IgualMenor
            Case Else
                Return -1
        End Select
    End Function
    'CboFechas
    Private Shared Function fechaAEnumerado(ByVal sFecha As String) As TaskResult.DocumentDates
        Select Case sFecha
            Case TaskResult.DocumentDates.Creacion.ToString
                Return Result.DocumentDates.Creacion
            Case TaskResult.DocumentDates.Edicion.ToString
                Return Result.DocumentDates.Edicion
            Case TaskResult.DocumentDates.Entrada.ToString
                Return Result.DocumentDates.Entrada
            Case TaskResult.DocumentDates.Salida.ToString
                Return Result.DocumentDates.Salida
            Case Else
                Return -1
        End Select
    End Function

    Public Shadows ReadOnly Property MyRule() As IIfDates
        Get
            Return DirectCast(Rule, IIfDates)
        End Get
    End Property
End Class
