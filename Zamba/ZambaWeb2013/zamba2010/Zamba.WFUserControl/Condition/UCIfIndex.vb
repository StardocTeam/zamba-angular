'Imports zamba.DocTypes.Factory
Imports Zamba.Data
'Imports Zamba.WFBusiness

Public Class UCIfIndex
    'Todo control de regla de condicion debe heredar de ZRuleControl
    Inherits ZRuleControl

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
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents grpOperadores As GroupBox
    Friend WithEvents rdoMayorIgual As System.Windows.Forms.RadioButton
    Friend WithEvents rdoMenorIgual As System.Windows.Forms.RadioButton
    Friend WithEvents rdoMenor As System.Windows.Forms.RadioButton
    Friend WithEvents rdoTermina As System.Windows.Forms.RadioButton
    Friend WithEvents rdoEmpieza As System.Windows.Forms.RadioButton
    Friend WithEvents rdoContiene As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDistinto As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIgual As System.Windows.Forms.RadioButton
    Friend WithEvents rdoMayor As System.Windows.Forms.RadioButton
    Friend WithEvents lstIndices As ListBox
    Friend WithEvents txtValorComparativo As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents lblIndices As ZLabel
    Friend WithEvents lblSeleccionar As ZLabel
    Friend WithEvents rdoFalso As System.Windows.Forms.RadioButton
    Friend WithEvents rdoVerdadero As System.Windows.Forms.RadioButton
    Friend WithEvents lstCondiciones As ListBox
    Friend WithEvents btnAgregar As ZButton
    Friend WithEvents btnEliminar As ZButton
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents lstTD As ListBox
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RdoAnd As System.Windows.Forms.RadioButton
    Friend WithEvents RdoOr As System.Windows.Forms.RadioButton
    Friend WithEvents ZPanel4 As ZPanel
    Friend WithEvents ZPanel5 As ZPanel
    Friend WithEvents ZPanel3 As ZPanel
    Friend WithEvents ZPanel1 As ZPanel
    Friend WithEvents ZPanel2 As ZPanel
    Friend WithEvents txtVariable As Zamba.AppBlock.TextoInteligenteTextBox

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        Label1 = New ZLabel()
        grpOperadores = New GroupBox()
        rdoMayorIgual = New System.Windows.Forms.RadioButton()
        rdoMenorIgual = New System.Windows.Forms.RadioButton()
        rdoMenor = New System.Windows.Forms.RadioButton()
        rdoTermina = New System.Windows.Forms.RadioButton()
        rdoEmpieza = New System.Windows.Forms.RadioButton()
        rdoVerdadero = New System.Windows.Forms.RadioButton()
        rdoFalso = New System.Windows.Forms.RadioButton()
        rdoContiene = New System.Windows.Forms.RadioButton()
        rdoDistinto = New System.Windows.Forms.RadioButton()
        rdoIgual = New System.Windows.Forms.RadioButton()
        rdoMayor = New System.Windows.Forms.RadioButton()
        lstIndices = New ListBox()
        txtValorComparativo = New Zamba.AppBlock.TextoInteligenteTextBox()
        btnAceptar = New ZButton()
        lblIndices = New ZLabel()
        lblSeleccionar = New ZLabel()
        lstCondiciones = New ListBox()
        btnAgregar = New ZButton()
        btnEliminar = New ZButton()
        Label2 = New ZLabel()
        txtVariable = New Zamba.AppBlock.TextoInteligenteTextBox()
        lstTD = New ListBox()
        Label3 = New ZLabel()
        GroupBox1 = New GroupBox()
        RdoOr = New System.Windows.Forms.RadioButton()
        RdoAnd = New System.Windows.Forms.RadioButton()
        ZPanel1 = New ZPanel()
        ZPanel2 = New ZPanel()
        ZPanel3 = New ZPanel()
        ZPanel4 = New ZPanel()
        ZPanel5 = New ZPanel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        grpOperadores.SuspendLayout()
        GroupBox1.SuspendLayout()
        ZPanel1.SuspendLayout()
        ZPanel2.SuspendLayout()
        ZPanel3.SuspendLayout()
        ZPanel4.SuspendLayout()
        ZPanel5.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(ZPanel4)
        tbRule.Controls.Add(ZPanel3)
        tbRule.Controls.Add(txtValorComparativo)
        tbRule.Controls.Add(Label1)
        tbRule.Controls.Add(GroupBox1)
        tbRule.Controls.Add(grpOperadores)
        tbRule.Controls.Add(lblSeleccionar)
        tbRule.Controls.Add(ZPanel1)
        tbRule.Size = New System.Drawing.Size(570, 501)
        '
        'tbctrMain
        '
        tbctrMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        tbctrMain.Dock = System.Windows.Forms.DockStyle.None
        tbctrMain.Size = New System.Drawing.Size(578, 530)
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Dock = System.Windows.Forms.DockStyle.Top
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.FontSize = 9.75!
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(3, 258)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(564, 32)
        Label1.TabIndex = 26
        Label1.Text = "Escriba el valor por el cual comparar:"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'grpOperadores
        '
        grpOperadores.BackColor = System.Drawing.Color.Transparent
        grpOperadores.Controls.Add(rdoMayorIgual)
        grpOperadores.Controls.Add(rdoMenorIgual)
        grpOperadores.Controls.Add(rdoMenor)
        grpOperadores.Controls.Add(rdoTermina)
        grpOperadores.Controls.Add(rdoEmpieza)
        grpOperadores.Controls.Add(rdoVerdadero)
        grpOperadores.Controls.Add(rdoFalso)
        grpOperadores.Controls.Add(rdoContiene)
        grpOperadores.Controls.Add(rdoDistinto)
        grpOperadores.Controls.Add(rdoIgual)
        grpOperadores.Controls.Add(rdoMayor)
        grpOperadores.Dock = System.Windows.Forms.DockStyle.Top
        grpOperadores.Location = New System.Drawing.Point(3, 143)
        grpOperadores.Name = "grpOperadores"
        grpOperadores.Size = New System.Drawing.Size(564, 81)
        grpOperadores.TabIndex = 25
        grpOperadores.TabStop = False
        '
        'rdoMayorIgual
        '
        rdoMayorIgual.Font = New Font("Tahoma", 15.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        rdoMayorIgual.Location = New System.Drawing.Point(350, 18)
        rdoMayorIgual.Name = "rdoMayorIgual"
        rdoMayorIgual.Size = New System.Drawing.Size(80, 26)
        rdoMayorIgual.TabIndex = 7
        rdoMayorIgual.Text = ">="
        '
        'rdoMenorIgual
        '
        rdoMenorIgual.Font = New Font("Tahoma", 15.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        rdoMenorIgual.Location = New System.Drawing.Point(275, 18)
        rdoMenorIgual.Name = "rdoMenorIgual"
        rdoMenorIgual.Size = New System.Drawing.Size(69, 26)
        rdoMenorIgual.TabIndex = 6
        rdoMenorIgual.Text = "<="
        '
        'rdoMenor
        '
        rdoMenor.Font = New Font("Tahoma", 15.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        rdoMenor.Location = New System.Drawing.Point(147, 18)
        rdoMenor.Name = "rdoMenor"
        rdoMenor.Size = New System.Drawing.Size(44, 26)
        rdoMenor.TabIndex = 4
        rdoMenor.Text = "<"
        '
        'rdoTermina
        '
        rdoTermina.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        rdoTermina.Location = New System.Drawing.Point(177, 51)
        rdoTermina.Name = "rdoTermina"
        rdoTermina.Size = New System.Drawing.Size(91, 26)
        rdoTermina.TabIndex = 10
        rdoTermina.Text = "Termina"
        '
        'rdoEmpieza
        '
        rdoEmpieza.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        rdoEmpieza.Location = New System.Drawing.Point(97, 50)
        rdoEmpieza.Name = "rdoEmpieza"
        rdoEmpieza.Size = New System.Drawing.Size(107, 26)
        rdoEmpieza.TabIndex = 9
        rdoEmpieza.Text = "Empieza"
        '
        'rdoVerdadero
        '
        rdoVerdadero.BackColor = System.Drawing.Color.Transparent
        rdoVerdadero.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        rdoVerdadero.Location = New System.Drawing.Point(335, 52)
        rdoVerdadero.Name = "rdoVerdadero"
        rdoVerdadero.Size = New System.Drawing.Size(96, 24)
        rdoVerdadero.TabIndex = 12
        rdoVerdadero.Text = "Verdadero"
        rdoVerdadero.UseVisualStyleBackColor = False
        rdoVerdadero.Visible = False
        '
        'rdoFalso
        '
        rdoFalso.BackColor = System.Drawing.Color.Transparent
        rdoFalso.Font = New Font("Tahoma", 9.75!)
        rdoFalso.Location = New System.Drawing.Point(271, 53)
        rdoFalso.Name = "rdoFalso"
        rdoFalso.Size = New System.Drawing.Size(70, 21)
        rdoFalso.TabIndex = 11
        rdoFalso.Text = "Falso"
        rdoFalso.UseVisualStyleBackColor = False
        rdoFalso.Visible = False
        '
        'rdoContiene
        '
        rdoContiene.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        rdoContiene.Location = New System.Drawing.Point(13, 50)
        rdoContiene.Name = "rdoContiene"
        rdoContiene.Size = New System.Drawing.Size(80, 26)
        rdoContiene.TabIndex = 8
        rdoContiene.Text = "Contiene"
        '
        'rdoDistinto
        '
        rdoDistinto.Font = New Font("Tahoma", 15.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        rdoDistinto.Location = New System.Drawing.Point(72, 18)
        rdoDistinto.Name = "rdoDistinto"
        rdoDistinto.Size = New System.Drawing.Size(80, 26)
        rdoDistinto.TabIndex = 3
        rdoDistinto.Text = "<>"
        '
        'rdoIgual
        '
        rdoIgual.Checked = True
        rdoIgual.Font = New Font("Tahoma", 15.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        rdoIgual.Location = New System.Drawing.Point(13, 18)
        rdoIgual.Name = "rdoIgual"
        rdoIgual.Size = New System.Drawing.Size(40, 26)
        rdoIgual.TabIndex = 2
        rdoIgual.TabStop = True
        rdoIgual.Text = "="
        '
        'rdoMayor
        '
        rdoMayor.Font = New Font("Tahoma", 15.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        rdoMayor.Location = New System.Drawing.Point(206, 18)
        rdoMayor.Name = "rdoMayor"
        rdoMayor.Size = New System.Drawing.Size(69, 26)
        rdoMayor.TabIndex = 5
        rdoMayor.Text = ">"
        '
        'lstIndices
        '
        lstIndices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lstIndices.DisplayMember = "INDEX_NAME"
        lstIndices.Dock = System.Windows.Forms.DockStyle.Fill
        lstIndices.ItemHeight = 16
        lstIndices.Location = New System.Drawing.Point(0, 24)
        lstIndices.Name = "lstIndices"
        lstIndices.Size = New System.Drawing.Size(300, 102)
        lstIndices.Sorted = True
        lstIndices.TabIndex = 1
        '
        'txtValorComparativo
        '
        txtValorComparativo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtValorComparativo.Dock = System.Windows.Forms.DockStyle.Top
        txtValorComparativo.Location = New System.Drawing.Point(3, 290)
        txtValorComparativo.Name = "txtValorComparativo"
        txtValorComparativo.Size = New System.Drawing.Size(564, 31)
        txtValorComparativo.TabIndex = 13
        txtValorComparativo.Text = ""
        '
        'btnAceptar
        '
        btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAceptar.FlatStyle = FlatStyle.Flat
        btnAceptar.ForeColor = System.Drawing.Color.White
        btnAceptar.Location = New System.Drawing.Point(449, 25)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(85, 30)
        btnAceptar.TabIndex = 18
        btnAceptar.Text = "Guardar"
        btnAceptar.UseVisualStyleBackColor = False
        '
        'lblIndices
        '
        lblIndices.BackColor = System.Drawing.Color.Transparent
        lblIndices.Dock = System.Windows.Forms.DockStyle.Top
        lblIndices.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblIndices.FontSize = 9.75!
        lblIndices.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblIndices.Location = New System.Drawing.Point(0, 0)
        lblIndices.Name = "lblIndices"
        lblIndices.Size = New System.Drawing.Size(300, 24)
        lblIndices.TabIndex = 23
        lblIndices.Text = "Seleccione el Atributo"
        lblIndices.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblSeleccionar
        '
        lblSeleccionar.BackColor = System.Drawing.Color.Transparent
        lblSeleccionar.Dock = System.Windows.Forms.DockStyle.Top
        lblSeleccionar.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblSeleccionar.FontSize = 9.75!
        lblSeleccionar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblSeleccionar.Location = New System.Drawing.Point(3, 129)
        lblSeleccionar.Name = "lblSeleccionar"
        lblSeleccionar.Size = New System.Drawing.Size(564, 14)
        lblSeleccionar.TabIndex = 22
        lblSeleccionar.Text = "Seleccione el operador"
        lblSeleccionar.TextAlign = ContentAlignment.MiddleLeft
        '
        'lstCondiciones
        '
        lstCondiciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lstCondiciones.Dock = System.Windows.Forms.DockStyle.Fill
        lstCondiciones.ItemHeight = 16
        lstCondiciones.Location = New System.Drawing.Point(0, 40)
        lstCondiciones.Name = "lstCondiciones"
        lstCondiciones.Size = New System.Drawing.Size(564, 65)
        lstCondiciones.TabIndex = 16
        '
        'btnAgregar
        '
        btnAgregar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAgregar.FlatStyle = FlatStyle.Flat
        btnAgregar.ForeColor = System.Drawing.Color.White
        btnAgregar.Location = New System.Drawing.Point(20, 8)
        btnAgregar.Name = "btnAgregar"
        btnAgregar.Size = New System.Drawing.Size(93, 26)
        btnAgregar.TabIndex = 14
        btnAgregar.Text = "Agregar"
        btnAgregar.UseVisualStyleBackColor = False
        '
        'btnEliminar
        '
        btnEliminar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnEliminar.FlatStyle = FlatStyle.Flat
        btnEliminar.ForeColor = System.Drawing.Color.White
        btnEliminar.Location = New System.Drawing.Point(131, 8)
        btnEliminar.Name = "btnEliminar"
        btnEliminar.Size = New System.Drawing.Size(88, 26)
        btnEliminar.TabIndex = 15
        btnEliminar.Text = "Eliminar Seleccionado"
        btnEliminar.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.FontSize = 9.75!
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(3, 11)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(293, 16)
        Label2.TabIndex = 34
        Label2.Text = "(Opcional) Validar Documentos de Variable:"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtVariable
        '
        txtVariable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtVariable.Location = New System.Drawing.Point(13, 30)
        txtVariable.Name = "txtVariable"
        txtVariable.Size = New System.Drawing.Size(348, 28)
        txtVariable.TabIndex = 17
        txtVariable.Text = ""
        '
        'lstTD
        '
        lstTD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lstTD.DisplayMember = "INDEX_NAME"
        lstTD.Dock = System.Windows.Forms.DockStyle.Fill
        lstTD.ItemHeight = 16
        lstTD.Location = New System.Drawing.Point(0, 24)
        lstTD.MaximumSize = New System.Drawing.Size(206, 119)
        lstTD.Name = "lstTD"
        lstTD.Size = New System.Drawing.Size(206, 102)
        lstTD.Sorted = True
        lstTD.TabIndex = 35
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Dock = System.Windows.Forms.DockStyle.Top
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.FontSize = 9.75!
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(0, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(264, 24)
        Label3.TabIndex = 36
        Label3.Text = "Entidad"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        GroupBox1.BackColor = System.Drawing.Color.Transparent
        GroupBox1.Controls.Add(RdoOr)
        GroupBox1.Controls.Add(RdoAnd)
        GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        GroupBox1.Location = New System.Drawing.Point(3, 224)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(564, 34)
        GroupBox1.TabIndex = 37
        GroupBox1.TabStop = False
        '
        'RdoOr
        '
        RdoOr.AutoSize = True
        RdoOr.Location = New System.Drawing.Point(72, 9)
        RdoOr.Name = "RdoOr"
        RdoOr.Size = New System.Drawing.Size(41, 20)
        RdoOr.TabIndex = 1
        RdoOr.TabStop = True
        RdoOr.Text = "Or"
        RdoOr.UseVisualStyleBackColor = True
        '
        'RdoAnd
        '
        RdoAnd.AutoSize = True
        RdoAnd.Location = New System.Drawing.Point(12, 9)
        RdoAnd.Name = "RdoAnd"
        RdoAnd.Size = New System.Drawing.Size(51, 20)
        RdoAnd.TabIndex = 0
        RdoAnd.TabStop = True
        RdoAnd.Text = "And"
        RdoAnd.UseVisualStyleBackColor = True
        '
        'ZPanel1
        '
        ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel1.Controls.Add(lstTD)
        ZPanel1.Controls.Add(Label3)
        ZPanel1.Controls.Add(ZPanel2)
        ZPanel1.Dock = System.Windows.Forms.DockStyle.Top
        ZPanel1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel1.Location = New System.Drawing.Point(3, 3)
        ZPanel1.Name = "ZPanel1"
        ZPanel1.Size = New System.Drawing.Size(564, 126)
        ZPanel1.TabIndex = 38
        '
        'ZPanel2
        '
        ZPanel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel2.Controls.Add(lstIndices)
        ZPanel2.Controls.Add(lblIndices)
        ZPanel2.Dock = System.Windows.Forms.DockStyle.Right
        ZPanel2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel2.Location = New System.Drawing.Point(264, 0)
        ZPanel2.Name = "ZPanel2"
        ZPanel2.Size = New System.Drawing.Size(300, 126)
        ZPanel2.TabIndex = 37
        '
        'ZPanel3
        '
        ZPanel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel3.Controls.Add(Label2)
        ZPanel3.Controls.Add(txtVariable)
        ZPanel3.Controls.Add(btnAceptar)
        ZPanel3.Dock = System.Windows.Forms.DockStyle.Bottom
        ZPanel3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel3.Location = New System.Drawing.Point(3, 426)
        ZPanel3.Name = "ZPanel3"
        ZPanel3.Size = New System.Drawing.Size(564, 72)
        ZPanel3.TabIndex = 39
        '
        'ZPanel4
        '
        ZPanel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel4.Controls.Add(lstCondiciones)
        ZPanel4.Controls.Add(ZPanel5)
        ZPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        ZPanel4.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel4.Location = New System.Drawing.Point(3, 321)
        ZPanel4.Name = "ZPanel4"
        ZPanel4.Size = New System.Drawing.Size(564, 105)
        ZPanel4.TabIndex = 40
        '
        'ZPanel5
        '
        ZPanel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel5.Controls.Add(btnAgregar)
        ZPanel5.Controls.Add(btnEliminar)
        ZPanel5.Dock = System.Windows.Forms.DockStyle.Top
        ZPanel5.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel5.Location = New System.Drawing.Point(0, 0)
        ZPanel5.Name = "ZPanel5"
        ZPanel5.Size = New System.Drawing.Size(564, 40)
        ZPanel5.TabIndex = 17
        '
        'UCIfIndex
        '
        Name = "UCIfIndex"
        Size = New System.Drawing.Size(581, 530)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        grpOperadores.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ZPanel1.ResumeLayout(False)
        ZPanel2.ResumeLayout(False)
        ZPanel3.ResumeLayout(False)
        ZPanel3.PerformLayout()
        ZPanel4.ResumeLayout(False)
        ZPanel5.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    'Regla que se va a configurar
    Private This As IIfIndex

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Constructor que debera pasar a la clase padre, la regla que se configurara
    ''' </summary>
    ''' <param name="IfIndex"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Martin]	30/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByRef IfIndex As IIfIndex, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(IfIndex, _wfPanelCircuit)
        InitializeComponent()
        This = IfIndex

        'Dim Ind As Index
        Dim indTD As Index
        Dim i As Short

        'cargo todos los entidades 
        TD = DocTypesFactory.GetAllDocTypes

        If (TD.Tables.Count > 0) Then
            'Asigno el displaymember 
            lstTD.DisplayMember = "Name"

            For i = 0 To TD.Tables(0).Rows.Count - 1
                indTD = New Index()
                indTD.ID = TD.Tables(0).Rows(i).Item(1)
                indTD.Name = TD.Tables(0).Rows(i).Item(0)
                lstTD.Items.Add(indTD)
            Next
        End If

        'cargo el Diccionario con los atributos, para que aparezcan en el ListBox lstIndices
        'If cargarIndices() Then

        'Asigno el displaymember 
        'lstIndices.DisplayMember = "Name"
        ''Cargo los atributos
        'For Each Ind In Atributos.Values
        '    lstIndices.Items.Add(Ind)
        'Next


        For Each Item As String In This.Condiciones.Split("*")
            If Item.Trim <> String.Empty Then

                Dim Comp As String = String.Empty
                Select Case Item.Split("|")(1)
                    Case Comparators.Equal
                        Comp = "="
                    Case Comparators.Different
                        Comp = "<>"
                    Case Comparators.Lower
                        Comp = "<"
                    Case Comparators.Upper
                        Comp = ">"
                    Case Comparators.EqualLower
                        Comp = "<="
                    Case Comparators.Contents
                        Comp = "Contiene"
                    Case Comparators.Starts
                        Comp = "Empieza"
                    Case Comparators.Ends
                        Comp = "Termina"
                    Case Comparators.EqualUpper
                        Comp = ">="
                End Select


                lstCondiciones.Items.Add(Zamba.Core.IndexsBusiness.GetIndexName(Item.Split("|")(0), False).Trim & "|" & Comp & "|" & Item.Split("|")(2))
            End If
        Next
        txtVariable.Text = This.Variable

        ' Se asigna el valor de la condición
        RdoAnd.Checked = This.OperatorAND
        RdoOr.Checked = Not This.OperatorAND
        HasBeenModified = False

    End Sub

    'Para guardar los parametros de una regla se usa el siguiente metodo, al que se le pasa:
    'la regla en si, un item y el valor
    'WFRulesBusiness.UpdateParamItemAction(this, 0, StepItem.WFStep.Id)

    'Se llama a este metodo para actualizar el nombre en el administrador al finalizar la configuracion
    'Me.RaiseUpdateMaskName()

#Region "Variables Locales"
    Private Atributos As Hashtable
    Private TD As DataSet
#End Region

#Region "Metodos Locales"
    Private Sub UCIfIndex_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        HasBeenModified = False
    End Sub

    Private Function cargarIndices() As Boolean

        Try
            Atributos = IndexsBusiness.getAllIndexes
            Return True
        Catch ex As Exception
            Dim exn As New Exception("Error al cargar el Data Set DsIndex en UCIfIndex.cargarIndices(), excepción: " & ex.Message)
            Zamba.Core.ZClass.raiseerror(exn)
            Return False
        End Try
    End Function
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Try
            'Actualizo los valores actuales al ifindex
            'rdoClikeado(Nothing, Nothing)
            lstIndices_SelectedIndexChanged(Nothing, Nothing)
            txtValorComparativo_TextChanged(Nothing, Nothing)
            grpOperadores_Enter(Nothing, Nothing)

            Dim Cadena As String = ""
            For Each Item As String In lstCondiciones.Items
                Dim Comp As String = String.Empty
                Select Case Item.Split("|")(1)
                    Case "Contiene"
                        Comp = Comparators.Contents
                    Case "<>"
                        Comp = Comparators.Different
                    Case "Termina"
                        Comp = Comparators.Ends
                    Case "="
                        Comp = Comparators.Equal
                    Case "<="
                        Comp = Comparators.EqualLower
                    Case ">="
                        Comp = Comparators.EqualUpper
                    Case "<"
                        Comp = Comparators.Lower
                    Case "Empieza"
                        Comp = Comparators.Starts
                    Case ">"
                        Comp = Comparators.Upper
                End Select


                If (Not String.Empty.Equals(Cadena)) Then Cadena &= "*"

                Cadena &= Zamba.Core.IndexsBusiness.GetIndexId(Item.Split("|")(0)) &
                  "|" & Comp & "|" &
                  Item.Split("|")(2)
            Next

            WFRulesBusiness.UpdateParamItem(This.ID, 0, Cadena)
            WFRulesBusiness.UpdateParamItem(This.ID, 1, txtVariable.Text)
            'Agregado MNP
            WFRulesBusiness.UpdateParamItem(This.ID, 2, RdoAnd.Checked)
            UserBusiness.Rights.SaveAction(This.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & This.Name & "(" & This.ID & ")")
            This.Condiciones = Cadena
            This.Variable = txtVariable.Text
            This.OperatorAND = RdoAnd.Checked

            '''''Dim i As Int32
            '''''i = Me.This.Comparator
            ''''''Actualizo el comparador
            '''''WFRulesBusiness.UpdateParamItemCondition(Me.This, 1, i)
            ''''''Actualizo el IndexId, del indice con el cual se compara
            '''''WFRulesBusiness.UpdateParamItemCondition(Me.This, 0, Me.This.IndexId.ToString)
            ''''''Actualizo el valor por el cual comparar
            '''''WFRulesBusiness.UpdateParamItemCondition(Me.This, 2, Me.This.Valor)
            ''''''MessageBox.Show("Se actualizó correctamente la regla", "Zamba - Work Flow", MessageBoxButtons.OK, MessageBoxIcon.Information)
            HasBeenModified = False
        Catch ex As Exception
            Dim exn As New Exception("Error al actualizar regla IfIndex en UCIfIndex.btnAceptar_Click(...), excepción: " & ex.Message)
            'MessageBox.Show("Error al actualizar la regla: " & ex.Message, "Zamba - Work Flow - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(exn)
        End Try

    End Sub
    'Private Sub rdoClikeado(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim rdo As RadioButton
    '    Try
    '        rdo = sender
    '        Select Case rdo.Name
    '            Case "rdoIgual"
    '                IndiceActual.Comparator = IfIndex.Comparators.Equal
    '            Case "rdoDistinto"
    '                IndiceActual.Comparator = IfIndex.Comparators.Different
    '            Case "rdoMenor"
    '                IndiceActual.Comparator = IfIndex.Comparators.Lower
    '            Case "rdoMayor"
    '                IndiceActual.Comparator = IfIndex.Comparators.Upper
    '            Case "rdoMenorIgual"
    '                IndiceActual.Comparator = IfIndex.Comparators.EqualLower
    '            Case "rdoContiene"
    '                IndiceActual.Comparator = IfIndex.Comparators.Contents
    '            Case "rdoEmpieza"
    '                IndiceActual.Comparator = IfIndex.Comparators.Starts
    '            Case "rdoTermina"
    '                IndiceActual.Comparator = IfIndex.Comparators.Ends
    '            Case "rdoMayorIgual"
    '                IndiceActual.Comparator = IfIndex.Comparators.EqualUpper
    '        End Select
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Tipo de indice
    Private Sub lstIndices_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) 'Handles lstIndices.SelectedIndexChanged
        Try
            Dim ind As Index
            ind = lstIndices.SelectedItem
            If ind.Type = IndexDataType.Si_No = True Then
                txtValorComparativo.Visible = False
                rdoFalso.Visible = True
                rdoVerdadero.Visible = True
            Else
                txtValorComparativo.Visible = True
                rdoFalso.Visible = False
                rdoVerdadero.Visible = False
            End If
            This.IndexId = ind.ID
        Catch ex As Exception
            Dim exn As New Exception("Error al asignar el IndexID a la regla IfIndex Private Sub lstIndices_SelectedIndexChanged(...) , excepción: " & ex.Message)
            Zamba.Core.ZClass.raiseerror(exn)
        End Try
    End Sub
    'Valor comparativo
    Private Sub txtValorComparativo_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) 'Handles txtValorComparativo.TextChanged
        'If rdoFalso.Checked = False AndAlso rdoVerdadero.Checked = False Then
        '	Me.This.Valor = txtValorComparativo.Text
        'Else
        '	If rdoFalso.Checked = True Then
        '		Me.This.Valor = False
        '	End If
        '	If rdoVerdadero.Checked = True Then
        '		Me.This.Valor = True
        '	End If
        'End If
    End Sub
    Private Sub grpOperadores_Enter(ByVal sender As System.Object, ByVal e As EventArgs) 'Handles grpOperadores.Enter
        Dim rdo As RadioButton = Nothing
        Dim bListo As Boolean = True
        Dim i As Int32 = 0
        Try
            'Busco el rdo checkeado
            While bListo
                rdo = grpOperadores.Controls.Item(i)
                If rdo.Checked Then
                    bListo = False
                End If

                i += 1

                If i < grpOperadores.Controls.Count Then
                Else
                    bListo = False
                End If
            End While

            'Le asigno el comparador al IfIndex
            Select Case rdo.Name
                Case "rdoIgual"
                    This.Comparator = Comparators.Equal
                Case "rdoDistinto"
                    This.Comparator = Comparators.Different
                Case "rdoMenor"
                    This.Comparator = Comparators.Lower
                Case "rdoMayor"
                    This.Comparator = Comparators.Upper
                Case "rdoMenorIgual"
                    This.Comparator = Comparators.EqualLower
                Case "rdoContiene"
                    This.Comparator = Comparators.Contents
                Case "rdoEmpieza"
                    This.Comparator = Comparators.Starts
                Case "rdoTermina"
                    This.Comparator = Comparators.Ends
                Case "rdoMayorIgual"
                    This.Comparator = Comparators.EqualUpper
            End Select
        Catch ex As Exception
        End Try
    End Sub
#End Region

    'Private Sub rdoFalso_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If rdoFalso.Checked = True Then
    '        Me.This.Valor = False
    '    End If
    'End Sub

    'Private Sub rdoVerdadero_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If rdoFalso.Checked = True Then
    '        Me.This.Valor = True
    '    End If
    'End Sub

    Private Sub lstIndices_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstIndices.SelectedIndexChanged
        Try
            Dim ind As Index
            ind = lstIndices.SelectedItem
            If ind.Type = IndexDataType.Si_No = True Then
                txtValorComparativo.Visible = False
                Label1.Visible = False
                For Each control As Object In grpOperadores.Controls
                    control.visible = False
                Next
                rdoFalso.Visible = True
                rdoVerdadero.Visible = True
            ElseIf ind.Type = IndexDataType.Alfanumerico OrElse ind.Type = IndexDataType.Alfanumerico_Largo Then
                For Each control As Object In grpOperadores.Controls
                    control.visible = False
                Next
                rdoIgual.Visible = True
                rdoContiene.Visible = True
                rdoDistinto.Visible = True
                rdoEmpieza.Visible = True
                rdoTermina.Visible = True
                txtValorComparativo.Visible = True
                Label1.Visible = True
                rdoFalso.Visible = False
                rdoVerdadero.Visible = False
            Else
                ' Numerico...
                txtValorComparativo.Visible = True
                Label1.Visible = True
                rdoFalso.Visible = False
                rdoVerdadero.Visible = False
                For Each control As Object In grpOperadores.Controls
                    control.visible = True
                Next
                rdoContiene.Visible = False
                rdoEmpieza.Visible = False
                rdoTermina.Visible = False
            End If
            This.IndexId = ind.ID
            HasBeenModified = True
        Catch ex As Exception
            Dim exn As New Exception("Error al asignar el IndexID a la regla IfIndex Private Sub lstIndices_SelectedIndexChanged(...) , excepción: " & ex.Message)
            Zamba.Core.ZClass.raiseerror(exn)
        End Try
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEliminar.Click
        Try
            lstCondiciones.Items.RemoveAt(lstCondiciones.SelectedIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAgregar.Click
        Dim Comp As String = String.Empty

        If rdoContiene.Checked = True Then
            Comp = "Contiene"
        ElseIf rdoDistinto.Checked = True Then
            Comp = "<>"
        ElseIf rdoTermina.Checked = True Then
            Comp = "Termina"
        ElseIf rdoIgual.Checked = True Then
            Comp = "="
        ElseIf rdoMenorIgual.Checked = True Then
            Comp = "<="
        ElseIf rdoMayorIgual.Checked = True Then
            Comp = ">="
        ElseIf rdoMenor.Checked = True Then
            Comp = "<"
        ElseIf rdoEmpieza.Checked = True Then
            Comp = "Empieza"
        ElseIf rdoMayor.Checked = True Then
            Comp = ">"
        End If



        Dim Valor As String = txtValorComparativo.Text

        ' Si es un valor booleano...
        If rdoVerdadero.Visible = True AndAlso rdoVerdadero.Checked = True Then
            Comp = "="
            Valor = "True"
        End If
        If rdoFalso.Visible = True AndAlso rdoFalso.Checked = True Then
            Comp = "="
            Valor = "False"
        End If

        ' Si no hay valor a comparar...
        'If Comp = String.Empty OrElse Valor = String.Empty Then
        'If Comp = String.Empty Then
        If String.IsNullOrEmpty(Comp) = True Then
            MessageBox.Show("DEBE COMPLETAR TODOS LOS VALORES DE LA CONDICION")
            Exit Sub
        End If

        lstCondiciones.Items.Add(lstIndices.Text.Trim & "|" & Comp & "|" & Valor)
    End Sub

    Public Shadows ReadOnly Property MyRule() As IIfIndex
        Get
            Return DirectCast(Rule, IIfIndex)
        End Get
    End Property

    ''' <summary>
    ''' Maneja el evento de Selección de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier  11/01/2011  Modified    Se agrega el tipo de Atributo al cargar los mismo al listbox de atributos
    ''' </history>
    Private Sub lstTD_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstTD.SelectedIndexChanged

        Dim Ind As Index
        Dim i As Integer
        Dim id As Integer

        If (lstTD.SelectedItems.Count > 0) Then

            Ind = lstTD.SelectedItem
            id = Ind.ID
            TD = IndexsBusiness.GetIndexSchemaAsDataSet(id)
            'lstIndices.DataSource = Zamba.Core.DocTypesFactory.GetIndexs(Zamba.Core.DocTypesFactory.GetDocType(id))

            'Asigno el displaymember 
            lstIndices.Items.Clear()
            lstIndices.DisplayMember = "Name"

            'Cargo los atributos
            For i = 0 To TD.Tables(0).Rows.Count - 1
                Ind = New Index()
                Ind.ID = TD.Tables(0).Rows(i).Item(0)
                Ind.Name = TD.Tables(0).Rows(i).Item(1)
                Ind.Type = TD.Tables(0).Rows(i).Item(2)
                lstIndices.Items.Add(Ind)
            Next

            'For Each Item As String In This.Condiciones.Split("*")
            '    If Item.Trim <> String.Empty Then

            '        Dim Comp As String
            '        Select Case Item.Split("|")(1)
            '            Case IfIndex.Comparators.Equal
            '                Comp = "="
            '            Case IfIndex.Comparators.Different
            '                Comp = "<>"
            '            Case IfIndex.Comparators.Lower
            '                Comp = "<"
            '            Case IfIndex.Comparators.Upper
            '                Comp = ">"
            '            Case IfIndex.Comparators.EqualLower
            '                Comp = "<="
            '            Case IfIndex.Comparators.Contents
            '                Comp = "Contiene"
            '            Case IfIndex.Comparators.Starts
            '                Comp = "Empieza"
            '            Case IfIndex.Comparators.Ends
            '                Comp = "Termina"
            '            Case IfIndex.Comparators.EqualUpper
            '                Comp = ">="
            '        End Select


            '        Me.lstCondiciones.Items.Add(Zamba.Core.Indexs_Factory.GetIndexName(Item.Split("|")(0)) & "|" & Comp & "|" & Item.Split("|")(2))
            '    End If
            'Next
            'Me.txtVariable.Text = This.Variable

            ''Selecciono el atributo de la regla
            'If Me.This.IndexId > 0 Then
            '    'Busco el nombre del indice actual por el id del indice actual
            '    Ind = Atributos.Item(Me.This.IndexId)
            '    If Not IsNothing(Ind) Then
            '        lstIndices.SelectedItem = Ind
            '    End If
            '    If Ind.Type = IndexDataType.Si_No Then
            '        If This.Valor = True Then
            '            Me.rdoVerdadero.Checked = True
            '        ElseIf This.Valor = False Then
            '            Me.rdoFalso.Checked = True
            '        End If
            '    Else
            '        txtValorComparativo.Text = Me.This.Valor
            '    End If
            'End If

            ''Selecciono el comparador de la regla
            'Select Case Me.This.Comparator
            '    Case IfIndex.Comparators.Contents
            '        rdoContiene.Checked = True
            '    Case IfIndex.Comparators.Different
            '        Me.rdoDistinto.Checked = True
            '    Case IfIndex.Comparators.Ends
            '        rdoTermina.Checked = True
            '    Case IfIndex.Comparators.Equal
            '        rdoIgual.Checked = True
            '    Case IfIndex.Comparators.EqualLower
            '        rdoMenorIgual.Checked = True
            '    Case IfIndex.Comparators.EqualUpper
            '        rdoMayorIgual.Checked = True
            '    Case IfIndex.Comparators.Lower
            '        rdoMenor.Checked = True
            '    Case IfIndex.Comparators.Starts
            '        rdoEmpieza.Checked = True
            '    Case IfIndex.Comparators.Upper
            '        rdoMayor.Checked = True
            'End Select


            'Eventos de los rdobtn
            'AddHandler rdoContiene.CheckedChanged, AddressOf rdoClikeado
            'AddHandler rdoDistinto.CheckedChanged, AddressOf rdoClikeado
            'AddHandler rdoIgual.CheckedChanged, AddressOf rdoClikeado
            'AddHandler rdoEmpieza.CheckedChanged, AddressOf rdoClikeado
            'AddHandler rdoTermina.CheckedChanged, AddressOf rdoClikeado
            'AddHandler rdoMayor.CheckedChanged, AddressOf rdoClikeado
            'AddHandler rdoMenor.CheckedChanged, AddressOf rdoClikeado
            'AddHandler rdoMenorIgual.CheckedChanged, AddressOf rdoClikeado
            'AddHandler rdoMayorIgual.CheckedChanged, AddressOf rdoClikeado

        End If
        HasBeenModified = True
    End Sub

    Private Sub txtValorComparativo_TextChanged_1(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtValorComparativo.TextChanged
        HasBeenModified = True
    End Sub

    Private Sub lstCondiciones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstCondiciones.SelectedIndexChanged
        HasBeenModified = True
    End Sub

    Private Sub rdoIgual_CheckedChanged(sender As Object, e As EventArgs) Handles rdoIgual.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub rdoDistinto_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDistinto.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub rdoMenor_CheckedChanged(sender As Object, e As EventArgs) Handles rdoMenor.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub rdoMayor_CheckedChanged(sender As Object, e As EventArgs) Handles rdoMayor.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub rdoMenorIgual_CheckedChanged(sender As Object, e As EventArgs) Handles rdoMenorIgual.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub rdoMayorIgual_CheckedChanged(sender As Object, e As EventArgs) Handles rdoMayorIgual.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub rdoContiene_CheckedChanged(sender As Object, e As EventArgs) Handles rdoContiene.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub rdoEmpieza_CheckedChanged(sender As Object, e As EventArgs) Handles rdoEmpieza.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub rdoTermina_CheckedChanged(sender As Object, e As EventArgs) Handles rdoTermina.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub rdoFalso_CheckedChanged(sender As Object, e As EventArgs) Handles rdoFalso.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub rdoVerdadero_CheckedChanged(sender As Object, e As EventArgs) Handles rdoVerdadero.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub RdoAnd_CheckedChanged(sender As Object, e As EventArgs) Handles RdoAnd.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub RdoOr_CheckedChanged(sender As Object, e As EventArgs) Handles RdoOr.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub txtVariable_TextChanged(sender As Object, e As EventArgs) Handles txtVariable.TextChanged
        HasBeenModified = True
    End Sub
End Class
