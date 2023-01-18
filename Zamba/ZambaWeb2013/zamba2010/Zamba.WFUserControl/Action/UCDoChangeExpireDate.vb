'Imports Zamba.WFBusiness

Public Class UCDoChangeExpireDate
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
    Friend Shadows WithEvents lblSeleccionarEstado As ZLabel
    Friend Shadows WithEvents btnSeleccionar As ZButton
    Friend Shadows WithEvents Label1 As ZLabel
    Friend Shadows WithEvents Label2 As ZLabel
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents txtValue1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents CmbDireccion1 As ComboBox
    Friend WithEvents CmbDireccion2 As ComboBox
    Friend WithEvents txtValue2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents CmbDireccion4 As ComboBox
    Friend WithEvents txtValue4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents CmbDireccion3 As ComboBox
    Friend WithEvents txtValue3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents CmbDireccion5 As ComboBox
    Friend WithEvents txtValue5 As System.Windows.Forms.NumericUpDown

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        lblSeleccionarEstado = New ZLabel
        btnSeleccionar = New ZButton
        txtValue1 = New System.Windows.Forms.NumericUpDown
        CmbDireccion1 = New ComboBox
        Label1 = New ZLabel
        Label2 = New ZLabel
        CmbDireccion2 = New ComboBox
        txtValue2 = New System.Windows.Forms.NumericUpDown
        Label3 = New ZLabel
        CmbDireccion4 = New ComboBox
        txtValue4 = New System.Windows.Forms.NumericUpDown
        Label4 = New ZLabel
        CmbDireccion3 = New ComboBox
        txtValue3 = New System.Windows.Forms.NumericUpDown
        Label6 = New ZLabel
        CmbDireccion5 = New ComboBox
        txtValue5 = New System.Windows.Forms.NumericUpDown
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        CType(txtValue1, ComponentModel.ISupportInitialize).BeginInit()
        CType(txtValue2, ComponentModel.ISupportInitialize).BeginInit()
        CType(txtValue4, ComponentModel.ISupportInitialize).BeginInit()
        CType(txtValue3, ComponentModel.ISupportInitialize).BeginInit()
        CType(txtValue5, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(Label6)
        tbRule.Controls.Add(CmbDireccion5)
        tbRule.Controls.Add(txtValue5)
        tbRule.Controls.Add(Label3)
        tbRule.Controls.Add(CmbDireccion4)
        tbRule.Controls.Add(txtValue4)
        tbRule.Controls.Add(Label4)
        tbRule.Controls.Add(CmbDireccion3)
        tbRule.Controls.Add(txtValue3)
        tbRule.Controls.Add(Label2)
        tbRule.Controls.Add(CmbDireccion2)
        tbRule.Controls.Add(txtValue2)
        tbRule.Controls.Add(Label1)
        tbRule.Controls.Add(CmbDireccion1)
        tbRule.Controls.Add(txtValue1)
        tbRule.Controls.Add(btnSeleccionar)
        tbRule.Controls.Add(lblSeleccionarEstado)
        tbRule.Size = New System.Drawing.Size(400, 367)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(408, 393)
        '
        'lblSeleccionarEstado
        '
        lblSeleccionarEstado.AutoSize = True
        lblSeleccionarEstado.BackColor = System.Drawing.Color.Transparent
        lblSeleccionarEstado.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        lblSeleccionarEstado.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        lblSeleccionarEstado.Location = New System.Drawing.Point(48, 40)
        lblSeleccionarEstado.Name = "lblSeleccionarEstado"
        lblSeleccionarEstado.Size = New System.Drawing.Size(239, 16)
        lblSeleccionarEstado.TabIndex = 11
        lblSeleccionarEstado.Text = "Modificar Fecha de Vencimiento"
        lblSeleccionarEstado.TextAlign = ContentAlignment.MiddleLeft
        '
        'btnSeleccionar
        '
        btnSeleccionar.Location = New System.Drawing.Point(149, 338)
        btnSeleccionar.Name = "btnSeleccionar"
        btnSeleccionar.Size = New System.Drawing.Size(112, 23)
        btnSeleccionar.TabIndex = 13
        btnSeleccionar.Text = "Aceptar"
        '
        'txtValue1
        '
        txtValue1.Location = New System.Drawing.Point(240, 74)
        txtValue1.Name = "txtValue1"
        txtValue1.Size = New System.Drawing.Size(60, 21)
        txtValue1.TabIndex = 14
        '
        'CmbDireccion1
        '
        CmbDireccion1.DropDownStyle = ComboBoxStyle.DropDownList
        CmbDireccion1.Items.AddRange(New Object() {"Aumentar", "Disminuir"})
        CmbDireccion1.Location = New System.Drawing.Point(134, 75)
        CmbDireccion1.Name = "CmbDireccion1"
        CmbDireccion1.Size = New System.Drawing.Size(96, 21)
        CmbDireccion1.TabIndex = 18
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(47, 78)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(66, 14)
        Label1.TabIndex = 19
        Label1.Text = "Minuto(s)"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(47, 105)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(54, 14)
        Label2.TabIndex = 22
        Label2.Text = "Hora(s)"
        '
        'CmbDireccion2
        '
        CmbDireccion2.DropDownStyle = ComboBoxStyle.DropDownList
        CmbDireccion2.Items.AddRange(New Object() {"Aumentar", "Disminuir"})
        CmbDireccion2.Location = New System.Drawing.Point(134, 102)
        CmbDireccion2.Name = "CmbDireccion2"
        CmbDireccion2.Size = New System.Drawing.Size(96, 21)
        CmbDireccion2.TabIndex = 21
        '
        'txtValue2
        '
        txtValue2.Location = New System.Drawing.Point(240, 101)
        txtValue2.Name = "txtValue2"
        txtValue2.Size = New System.Drawing.Size(60, 21)
        txtValue2.TabIndex = 20
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(47, 159)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(75, 14)
        Label3.TabIndex = 28
        Label3.Text = "Semana(s)"
        '
        'CmbDireccion4
        '
        CmbDireccion4.DropDownStyle = ComboBoxStyle.DropDownList
        CmbDireccion4.Items.AddRange(New Object() {"Aumentar", "Disminuir"})
        CmbDireccion4.Location = New System.Drawing.Point(134, 156)
        CmbDireccion4.Name = "CmbDireccion4"
        CmbDireccion4.Size = New System.Drawing.Size(96, 21)
        CmbDireccion4.TabIndex = 27
        '
        'txtValue4
        '
        txtValue4.Location = New System.Drawing.Point(240, 155)
        txtValue4.Name = "txtValue4"
        txtValue4.Size = New System.Drawing.Size(60, 21)
        txtValue4.TabIndex = 26
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label4.Location = New System.Drawing.Point(47, 132)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(44, 14)
        Label4.TabIndex = 25
        Label4.Text = "Día(s)"
        '
        'CmbDireccion3
        '
        CmbDireccion3.DropDownStyle = ComboBoxStyle.DropDownList
        CmbDireccion3.Items.AddRange(New Object() {"Aumentar", "Disminuir"})
        CmbDireccion3.Location = New System.Drawing.Point(134, 129)
        CmbDireccion3.Name = "CmbDireccion3"
        CmbDireccion3.Size = New System.Drawing.Size(96, 21)
        CmbDireccion3.TabIndex = 24
        '
        'txtValue3
        '
        txtValue3.Location = New System.Drawing.Point(240, 128)
        txtValue3.Name = "txtValue3"
        txtValue3.Size = New System.Drawing.Size(60, 21)
        txtValue3.TabIndex = 23
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.BackColor = System.Drawing.Color.Transparent
        Label6.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label6.Location = New System.Drawing.Point(47, 186)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(57, 14)
        Label6.TabIndex = 31
        Label6.Text = "Mes(es)"
        '
        'CmbDireccion5
        '
        CmbDireccion5.DropDownStyle = ComboBoxStyle.DropDownList
        CmbDireccion5.Items.AddRange(New Object() {"Aumentar", "Disminuir"})
        CmbDireccion5.Location = New System.Drawing.Point(134, 183)
        CmbDireccion5.Name = "CmbDireccion5"
        CmbDireccion5.Size = New System.Drawing.Size(96, 21)
        CmbDireccion5.TabIndex = 30
        '
        'txtValue5
        '
        txtValue5.Location = New System.Drawing.Point(240, 182)
        txtValue5.Name = "txtValue5"
        txtValue5.Size = New System.Drawing.Size(60, 21)
        txtValue5.TabIndex = 29
        '
        'UCDoChangeExpireDate
        '
        Name = "UCDoChangeExpireDate"
        Size = New System.Drawing.Size(408, 393)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        CType(txtValue1, ComponentModel.ISupportInitialize).EndInit()
        CType(txtValue2, ComponentModel.ISupportInitialize).EndInit()
        CType(txtValue4, ComponentModel.ISupportInitialize).EndInit()
        CType(txtValue3, ComponentModel.ISupportInitialize).EndInit()
        CType(txtValue5, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

#End Region

    Private CurrentRule As IDoChangeExpireDate
    Public Sub New(ByRef rule As IDoChangeExpireDate, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()

        Try
            CurrentRule = rule

            CmbDireccion1.SelectedIndex = rule.Direccion1
            CmbDireccion2.SelectedIndex = rule.Direccion2
            CmbDireccion3.SelectedIndex = rule.Direccion3
            CmbDireccion4.SelectedIndex = rule.Direccion4
            CmbDireccion5.SelectedIndex = rule.Direccion5

            txtValue1.Value = rule.Value1
            txtValue2.Value = rule.Value2
            txtValue3.Value = rule.Value3
            txtValue4.Value = rule.Value4
            txtValue5.Value = rule.Value5

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Enum Direcciones
        Aumentar = 0
        Disminuir = 1
    End Enum

    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSeleccionar.Click
        Try

            CurrentRule.Direccion1 = CmbDireccion1.SelectedIndex
            CurrentRule.Direccion2 = CmbDireccion2.SelectedIndex
            CurrentRule.Direccion3 = CmbDireccion3.SelectedIndex
            CurrentRule.Direccion4 = CmbDireccion4.SelectedIndex
            CurrentRule.Direccion5 = CmbDireccion5.SelectedIndex

            CurrentRule.Value1 = txtValue1.Value
            CurrentRule.Value2 = txtValue2.Value
            CurrentRule.Value3 = txtValue3.Value
            CurrentRule.Value4 = txtValue4.Value
            CurrentRule.Value5 = txtValue5.Value

            WFRulesBusiness.UpdateParamItem(CurrentRule, 0, CurrentRule.Direccion1)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 1, CurrentRule.Direccion2)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 2, CurrentRule.Direccion3)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 3, CurrentRule.Direccion4)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 4, CurrentRule.Direccion5)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 5, CurrentRule.Value1)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 6, CurrentRule.Value2)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 7, CurrentRule.Value3)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 8, CurrentRule.Value4)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 9, CurrentRule.Value5)
            UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Public Shadows ReadOnly Property MyRule() As IDoChangeExpireDate
        Get
            Return DirectCast(Rule, IDoChangeExpireDate)
        End Get
    End Property
End Class
