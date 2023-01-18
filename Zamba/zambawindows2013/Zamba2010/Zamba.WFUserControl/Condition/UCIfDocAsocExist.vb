'Imports zamba.DocTypes.Factory
Imports Zamba.Data
'Imports Zamba.WFBusiness

Public Class UCIfDocAsocExist
    Inherits ZRuleControl
    Dim tipoDeDocumento As Object
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lstIndices As ListBox
    Friend WithEvents lblIndices As ZLabel
    Friend WithEvents grpOperadores As GroupBox
    Friend WithEvents rdoMayorIgual As System.Windows.Forms.RadioButton
    Friend WithEvents rdoMenorIgual As System.Windows.Forms.RadioButton
    Friend WithEvents rdoMenor As System.Windows.Forms.RadioButton
    Friend WithEvents rdoTermina As System.Windows.Forms.RadioButton
    Friend WithEvents rdoEmpieza As System.Windows.Forms.RadioButton
    Friend WithEvents rdoVerdadero As System.Windows.Forms.RadioButton
    Friend WithEvents rdoFalso As System.Windows.Forms.RadioButton
    Friend WithEvents rdoContiene As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDistinto As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIgual As System.Windows.Forms.RadioButton
    Friend WithEvents rdoMayor As System.Windows.Forms.RadioButton
    Friend WithEvents lblSeleccionar As ZLabel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RdoOr As System.Windows.Forms.RadioButton
    Friend WithEvents RdoAnd As System.Windows.Forms.RadioButton
    Friend WithEvents btnEliminar As ZButton
    Friend WithEvents btnAgregar As ZButton
    Friend WithEvents lstCondiciones As ListBox
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents txtValorComparativo As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents ZPanel2 As ZPanel
    Friend WithEvents ZPanel4 As ZPanel
    Friend WithEvents ZPanel3 As ZPanel
    Friend WithEvents ZPanel1 As ZPanel
    Dim existencia As Boolean

#Region " Código generado por el Diseñador de Windows Forms "

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
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents CmbDocType As ComboBox
    Friend WithEvents rbtExiste As System.Windows.Forms.RadioButton
    Friend WithEvents rbtNoExiste As System.Windows.Forms.RadioButton

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        btnAceptar = New ZButton()
        Label2 = New ZLabel()
        CmbDocType = New ComboBox()
        rbtExiste = New System.Windows.Forms.RadioButton()
        rbtNoExiste = New System.Windows.Forms.RadioButton()
        GroupBox1 = New GroupBox()
        lstIndices = New ListBox()
        lblIndices = New ZLabel()
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
        lblSeleccionar = New ZLabel()
        GroupBox2 = New GroupBox()
        RdoOr = New System.Windows.Forms.RadioButton()
        RdoAnd = New System.Windows.Forms.RadioButton()
        btnEliminar = New ZButton()
        btnAgregar = New ZButton()
        lstCondiciones = New ListBox()
        Label3 = New ZLabel()
        txtValorComparativo = New Zamba.AppBlock.TextoInteligenteTextBox()
        Label5 = New ZLabel()
        ZPanel1 = New ZPanel()
        ZPanel2 = New ZPanel()
        ZPanel3 = New ZPanel()
        ZPanel4 = New ZPanel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        GroupBox1.SuspendLayout()
        grpOperadores.SuspendLayout()
        GroupBox2.SuspendLayout()
        ZPanel1.SuspendLayout()
        ZPanel2.SuspendLayout()
        ZPanel3.SuspendLayout()
        ZPanel4.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(ZPanel2)
        tbRule.Controls.Add(txtValorComparativo)
        tbRule.Controls.Add(Label3)
        tbRule.Controls.Add(GroupBox2)
        tbRule.Controls.Add(grpOperadores)
        tbRule.Controls.Add(lblSeleccionar)
        tbRule.Controls.Add(ZPanel4)
        tbRule.Controls.Add(ZPanel3)
        tbRule.Controls.Add(ZPanel1)
        tbRule.Size = New System.Drawing.Size(683, 509)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(691, 538)
        '
        'btnAceptar
        '
        btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAceptar.FlatStyle = FlatStyle.Flat
        btnAceptar.ForeColor = System.Drawing.Color.White
        btnAceptar.Location = New System.Drawing.Point(519, 13)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(116, 29)
        btnAceptar.TabIndex = 22
        btnAceptar.Text = "Guardar"
        btnAceptar.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(6, 18)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(60, 14)
        Label2.TabIndex = 20
        Label2.Text = "Entidad:"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'CmbDocType
        '
        CmbDocType.DropDownStyle = ComboBoxStyle.DropDownList
        CmbDocType.Location = New System.Drawing.Point(8, 37)
        CmbDocType.Name = "CmbDocType"
        CmbDocType.Size = New System.Drawing.Size(505, 24)
        CmbDocType.TabIndex = 30
        '
        'rbtExiste
        '
        rbtExiste.BackColor = System.Drawing.Color.Transparent
        rbtExiste.Checked = True
        rbtExiste.Location = New System.Drawing.Point(30, 19)
        rbtExiste.Name = "rbtExiste"
        rbtExiste.Size = New System.Drawing.Size(65, 24)
        rbtExiste.TabIndex = 31
        rbtExiste.TabStop = True
        rbtExiste.Text = "Existe"
        rbtExiste.UseVisualStyleBackColor = False
        '
        'rbtNoExiste
        '
        rbtNoExiste.BackColor = System.Drawing.Color.Transparent
        rbtNoExiste.Location = New System.Drawing.Point(120, 19)
        rbtNoExiste.Name = "rbtNoExiste"
        rbtNoExiste.Size = New System.Drawing.Size(110, 24)
        rbtNoExiste.TabIndex = 32
        rbtNoExiste.Text = "No Existe"
        rbtNoExiste.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        GroupBox1.BackColor = System.Drawing.Color.Transparent
        GroupBox1.Controls.Add(rbtNoExiste)
        GroupBox1.Controls.Add(rbtExiste)
        GroupBox1.Location = New System.Drawing.Point(8, 67)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(308, 49)
        GroupBox1.TabIndex = 33
        GroupBox1.TabStop = False
        GroupBox1.Text = "Opciones"
        '
        'lstIndices
        '
        lstIndices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lstIndices.DisplayMember = "INDEX_NAME"
        lstIndices.Dock = System.Windows.Forms.DockStyle.Fill
        lstIndices.ItemHeight = 16
        lstIndices.Location = New System.Drawing.Point(0, 24)
        lstIndices.Name = "lstIndices"
        lstIndices.Size = New System.Drawing.Size(222, 298)
        lstIndices.Sorted = True
        lstIndices.TabIndex = 34
        '
        'lblIndices
        '
        lblIndices.BackColor = System.Drawing.Color.Transparent
        lblIndices.Dock = System.Windows.Forms.DockStyle.Top
        lblIndices.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblIndices.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblIndices.Location = New System.Drawing.Point(0, 0)
        lblIndices.Name = "lblIndices"
        lblIndices.Size = New System.Drawing.Size(222, 24)
        lblIndices.TabIndex = 35
        lblIndices.Text = "Seleccione el Atributo"
        lblIndices.TextAlign = ContentAlignment.MiddleLeft
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
        grpOperadores.Size = New System.Drawing.Size(455, 81)
        grpOperadores.TabIndex = 37
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
        'lblSeleccionar
        '
        lblSeleccionar.BackColor = System.Drawing.Color.Transparent
        lblSeleccionar.Dock = System.Windows.Forms.DockStyle.Top
        lblSeleccionar.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblSeleccionar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblSeleccionar.Location = New System.Drawing.Point(3, 129)
        lblSeleccionar.Name = "lblSeleccionar"
        lblSeleccionar.Size = New System.Drawing.Size(455, 14)
        lblSeleccionar.TabIndex = 36
        lblSeleccionar.Text = "Seleccione el operador"
        lblSeleccionar.TextAlign = ContentAlignment.MiddleLeft
        '
        'GroupBox2
        '
        GroupBox2.BackColor = System.Drawing.Color.Transparent
        GroupBox2.Controls.Add(RdoOr)
        GroupBox2.Controls.Add(RdoAnd)
        GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        GroupBox2.Location = New System.Drawing.Point(3, 224)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New System.Drawing.Size(455, 26)
        GroupBox2.TabIndex = 38
        GroupBox2.TabStop = False
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
        'btnEliminar
        '
        btnEliminar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnEliminar.FlatStyle = FlatStyle.Flat
        btnEliminar.ForeColor = System.Drawing.Color.White
        btnEliminar.Location = New System.Drawing.Point(133, 5)
        btnEliminar.Name = "btnEliminar"
        btnEliminar.Size = New System.Drawing.Size(94, 23)
        btnEliminar.TabIndex = 40
        btnEliminar.Text = "Eliminar Seleccionado"
        btnEliminar.UseVisualStyleBackColor = False
        '
        'btnAgregar
        '
        btnAgregar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAgregar.FlatStyle = FlatStyle.Flat
        btnAgregar.ForeColor = System.Drawing.Color.White
        btnAgregar.Location = New System.Drawing.Point(6, 3)
        btnAgregar.Name = "btnAgregar"
        btnAgregar.Size = New System.Drawing.Size(95, 26)
        btnAgregar.TabIndex = 39
        btnAgregar.Text = "Agregar"
        btnAgregar.UseVisualStyleBackColor = False
        '
        'lstCondiciones
        '
        lstCondiciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lstCondiciones.ItemHeight = 16
        lstCondiciones.Location = New System.Drawing.Point(6, 41)
        lstCondiciones.Name = "lstCondiciones"
        lstCondiciones.Size = New System.Drawing.Size(443, 34)
        lstCondiciones.TabIndex = 41
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Dock = System.Windows.Forms.DockStyle.Top
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(3, 250)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(254, 16)
        Label3.TabIndex = 42
        Label3.Text = "Escriba el valor por el cual comparar:"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtValorComparativo
        '
        txtValorComparativo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtValorComparativo.Dock = System.Windows.Forms.DockStyle.Top
        txtValorComparativo.Location = New System.Drawing.Point(3, 266)
        txtValorComparativo.Name = "txtValorComparativo"
        txtValorComparativo.Size = New System.Drawing.Size(455, 21)
        txtValorComparativo.TabIndex = 43
        txtValorComparativo.Text = ""
        '
        'Label5
        '
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label5.Location = New System.Drawing.Point(5, 10)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(195, 24)
        Label5.TabIndex = 44
        Label5.Text = "Entidades"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        '
        'ZPanel1
        '
        ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel1.Controls.Add(Label5)
        ZPanel1.Controls.Add(CmbDocType)
        ZPanel1.Controls.Add(GroupBox1)
        ZPanel1.Dock = System.Windows.Forms.DockStyle.Top
        ZPanel1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel1.Location = New System.Drawing.Point(3, 3)
        ZPanel1.Name = "ZPanel1"
        ZPanel1.Size = New System.Drawing.Size(677, 126)
        ZPanel1.TabIndex = 45
        '
        'ZPanel2
        '
        ZPanel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel2.Controls.Add(btnAgregar)
        ZPanel2.Controls.Add(btnEliminar)
        ZPanel2.Controls.Add(lstCondiciones)
        ZPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        ZPanel2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel2.Location = New System.Drawing.Point(3, 287)
        ZPanel2.Name = "ZPanel2"
        ZPanel2.Size = New System.Drawing.Size(455, 164)
        ZPanel2.TabIndex = 46
        '
        'ZPanel3
        '
        ZPanel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel3.Controls.Add(btnAceptar)
        ZPanel3.Dock = System.Windows.Forms.DockStyle.Bottom
        ZPanel3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel3.Location = New System.Drawing.Point(3, 451)
        ZPanel3.Name = "ZPanel3"
        ZPanel3.Size = New System.Drawing.Size(677, 55)
        ZPanel3.TabIndex = 47
        '
        'ZPanel4
        '
        ZPanel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel4.Controls.Add(lstIndices)
        ZPanel4.Controls.Add(lblIndices)
        ZPanel4.Dock = System.Windows.Forms.DockStyle.Right
        ZPanel4.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel4.Location = New System.Drawing.Point(458, 129)
        ZPanel4.Name = "ZPanel4"
        ZPanel4.Size = New System.Drawing.Size(222, 322)
        ZPanel4.TabIndex = 48
        '
        'UCIfDocAsocExist
        '
        Name = "UCIfDocAsocExist"
        Size = New System.Drawing.Size(691, 538)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        grpOperadores.ResumeLayout(False)
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        ZPanel1.ResumeLayout(False)
        ZPanel2.ResumeLayout(False)
        ZPanel3.ResumeLayout(False)
        ZPanel4.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Dim _myRule As IIfDocAsocExist
    Public Sub New(ByRef this As IIfDocAsocExist, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(this, _wfPanelCircuit)
        InitializeComponent()
        _myRule = this
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Try
            _myRule.TipoDeDocumento = CmbDocType.SelectedValue
            _myRule.Existencia = rbtExiste.Checked

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

                Cadena &= Zamba.Core.IndexsBusiness.GetIndexId(Item.Split("|")(0)) & _
                  "|" & Comp & "|" & _
                  Item.Split("|")(2)
            Next

            WFRulesBusiness.UpdateParamItem(_myRule, 0, _myRule.TipoDeDocumento, ObjectTypes.DocTypes)
            WFRulesBusiness.UpdateParamItem(_myRule, 1, _myRule.Existencia)
            WFRulesBusiness.UpdateParamItem(_myRule, 2, Cadena)
            WFRulesBusiness.UpdateParamItem(_myRule, 3, RdoAnd.Checked)

            UserBusiness.Rights.SaveAction(_myRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & _myRule.Name & "(" & _myRule.ID & ")")

            _myRule.Condiciones = Cadena
            _myRule.OperatorAND = RdoAnd.Checked
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub UCIfDocAsocExists_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load
        Try
            LoadAllDocTypes()
        Catch
        End Try
        Try
            CmbDocType.SelectedValue = _myRule.TipoDeDocumento
            rbtExiste.Checked = _myRule.Existencia
            rbtNoExiste.Checked = Not _myRule.Existencia

            For Each Item As String In _myRule.Condiciones.Split("*")
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

            ' Se asigna el valor de la condición
            RdoAnd.Checked = _myRule.OperatorAND
            RdoOr.Checked = Not _myRule.OperatorAND
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

#Region "DocTypes / Indexs"
    Dim DsDocTypes As DataSet

    Private Sub LoadAllDocTypes()
        Try
            DsDocTypes = DocTypesFactory.GetDocTypesDsDocType()
            If DsDocTypes.Tables("DOC_TYPE").Rows.Count <= 0 Then
                MessageBox.Show("Error 013: No hay definidos Entidades para realizar la indexacion o no tiene Permisos para crear Documentos", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show("Error: 014: Ocurrio un Error al cargar los Entidades para la Indexación ", "Zamba Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            zamba.core.zclass.raiseerror(ex)
        End Try

        Try
            CmbDocType.BeginUpdate()
            CmbDocType.DisplayMember = "DOC_TYPE_NAME"
            CmbDocType.ValueMember = "DOC_TYPE_ID"
            CmbDocType.DataSource = DsDocTypes.Tables("DOC_TYPE")
            CmbDocType.EndUpdate()
            SelectDocType()
        Catch ex As Exception
            MessageBox.Show("Error: 014: Ocurrio un Error al cargar los Entidades ", "Zamba Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub SelectDocType()
        If tipoDeDocumento = 0 Then
            CmbDocType.SelectedIndex = 0
        Else
            CmbDocType.SelectedValue = tipoDeDocumento
        End If
    End Sub
#End Region


    Public Shadows ReadOnly Property MyRule() As IIfDocAsocExist
        Get
            Return DirectCast(Rule, IIfDocAsocExist)
        End Get
    End Property

    Private Sub btnAgregar_Click_1(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAgregar.Click
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

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEliminar.Click
        Try
            lstCondiciones.Items.RemoveAt(lstCondiciones.SelectedIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CmbDocType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CmbDocType.SelectedIndexChanged
        Dim Ind As Index
        Dim i As Integer
        Dim id As Integer

        If (CmbDocType.SelectedIndex > -1) Then
            id = CmbDocType.SelectedValue
            Dim TD As DataSet = IndexsBusiness.GetIndexSchemaAsDataSet(id)

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

            TD.Dispose()
            TD = Nothing
        End If
    End Sub
End Class
