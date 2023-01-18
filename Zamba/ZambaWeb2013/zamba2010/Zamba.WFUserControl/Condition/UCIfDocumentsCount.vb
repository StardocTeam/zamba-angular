Imports ZAMBA.Core
Imports Zamba.AppBlock
Imports Zamba.Data
'Imports Zamba.WFBusiness
Imports System.Windows.Forms

Public Class UCIfDocumentsCount
    Inherits ZRuleControl

#Region " Inicializador Form "
    Private Shadows Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rbDistinto = New System.Windows.Forms.RadioButton
        Me.rbMayorIgual = New System.Windows.Forms.RadioButton
        Me.rbMayor = New System.Windows.Forms.RadioButton
        Me.rbMenor = New System.Windows.Forms.RadioButton
        Me.rbIgual = New System.Windows.Forms.RadioButton
        Me.rbMenorIgual = New System.Windows.Forms.RadioButton
        Me.ctrlCantidadTareas = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New ZLabel
        Me.btnAceptar = New ZButton
        Me.lblEnConstruccion = New ZLabel
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.ctrlCantidadTareas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.lblEnConstruccion)
        Me.tbRule.Size = New System.Drawing.Size(410, 282)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(418, 308)
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.rbDistinto)
        Me.GroupBox1.Controls.Add(Me.rbMayorIgual)
        Me.GroupBox1.Controls.Add(Me.rbMayor)
        Me.GroupBox1.Controls.Add(Me.rbMenor)
        Me.GroupBox1.Controls.Add(Me.rbIgual)
        Me.GroupBox1.Controls.Add(Me.rbMenorIgual)
        Me.GroupBox1.Location = New System.Drawing.Point(72, 33)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(138, 171)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Condición"
        '
        'rbDistinto
        '
        Me.rbDistinto.AutoSize = True
        Me.rbDistinto.Location = New System.Drawing.Point(24, 46)
        Me.rbDistinto.Name = "rbDistinto"
        Me.rbDistinto.Size = New System.Drawing.Size(61, 17)
        Me.rbDistinto.TabIndex = 6
        Me.rbDistinto.TabStop = True
        Me.rbDistinto.Text = "Distinto"
        Me.rbDistinto.UseVisualStyleBackColor = True
        '
        'rbMayorIgual
        '
        Me.rbMayorIgual.AutoSize = True
        Me.rbMayorIgual.Location = New System.Drawing.Point(24, 112)
        Me.rbMayorIgual.Name = "rbMayorIgual"
        Me.rbMayorIgual.Size = New System.Drawing.Size(91, 17)
        Me.rbMayorIgual.TabIndex = 5
        Me.rbMayorIgual.TabStop = True
        Me.rbMayorIgual.Text = "Mayor o Igual"
        Me.rbMayorIgual.UseVisualStyleBackColor = True
        '
        'rbMayor
        '
        Me.rbMayor.AutoSize = True
        Me.rbMayor.Location = New System.Drawing.Point(24, 68)
        Me.rbMayor.Name = "rbMayor"
        Me.rbMayor.Size = New System.Drawing.Size(55, 17)
        Me.rbMayor.TabIndex = 1
        Me.rbMayor.TabStop = True
        Me.rbMayor.Text = "Mayor"
        Me.rbMayor.UseVisualStyleBackColor = True
        '
        'rbMenor
        '
        Me.rbMenor.AutoSize = True
        Me.rbMenor.Location = New System.Drawing.Point(24, 90)
        Me.rbMenor.Name = "rbMenor"
        Me.rbMenor.Size = New System.Drawing.Size(55, 17)
        Me.rbMenor.TabIndex = 2
        Me.rbMenor.TabStop = True
        Me.rbMenor.Text = "Menor"
        Me.rbMenor.UseVisualStyleBackColor = True
        '
        'rbIgual
        '
        Me.rbIgual.AutoSize = True
        Me.rbIgual.Location = New System.Drawing.Point(24, 24)
        Me.rbIgual.Name = "rbIgual"
        Me.rbIgual.Size = New System.Drawing.Size(49, 17)
        Me.rbIgual.TabIndex = 3
        Me.rbIgual.TabStop = True
        Me.rbIgual.Text = "Igual"
        Me.rbIgual.UseVisualStyleBackColor = True
        '
        'rbMenorIgual
        '
        Me.rbMenorIgual.AutoSize = True
        Me.rbMenorIgual.Location = New System.Drawing.Point(24, 134)
        Me.rbMenorIgual.Name = "rbMenorIgual"
        Me.rbMenorIgual.Size = New System.Drawing.Size(91, 17)
        Me.rbMenorIgual.TabIndex = 4
        Me.rbMenorIgual.TabStop = True
        Me.rbMenorIgual.Text = "Menor o Igual"
        Me.rbMenorIgual.UseVisualStyleBackColor = True
        '
        'ctrlCantidadTareas
        '
        Me.ctrlCantidadTareas.Location = New System.Drawing.Point(282, 118)
        Me.ctrlCantidadTareas.Name = "ctrlCantidadTareas"
        Me.ctrlCantidadTareas.Size = New System.Drawing.Size(55, 21)
        Me.ctrlCantidadTareas.TabIndex = 21
        Me.ctrlCantidadTareas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(260, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Cantidad de Tareas"
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(171, 243)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
        Me.btnAceptar.TabIndex = 23
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'lblEnConstruccion
        '
        Me.lblEnConstruccion.AutoSize = True
        Me.lblEnConstruccion.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblEnConstruccion.Location = New System.Drawing.Point(19, 19)
        Me.lblEnConstruccion.Name = "lblEnConstruccion"
        Me.lblEnConstruccion.Size = New System.Drawing.Size(262, 13)
        Me.lblEnConstruccion.TabIndex = 1
        Me.lblEnConstruccion.Text = "Esta regla se encuentra actulmente en construcción. "
        '
        'UCIfDocumentsCount
        '
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ctrlCantidadTareas)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "UCIfDocumentsCount"
        Me.Size = New System.Drawing.Size(418, 308)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.ctrlCantidadTareas, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.btnAceptar, 0)
        Me.Controls.SetChildIndex(Me.tbctrMain, 0)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.ctrlCantidadTareas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

#Region " Variables "
    Friend WithEvents rbMenorIgual As System.Windows.Forms.RadioButton
    Friend WithEvents rbIgual As System.Windows.Forms.RadioButton
    Friend WithEvents ctrlCantidadTareas As System.Windows.Forms.NumericUpDown
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents rbDistinto As System.Windows.Forms.RadioButton
    Friend WithEvents rbMenor As System.Windows.Forms.RadioButton
    Friend WithEvents rbMayor As System.Windows.Forms.RadioButton
    Friend WithEvents rbMayorIgual As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblEnConstruccion As ZLabel
    Private _rule As IIfDOCUMENTSCOUNT
#End Region

#Region " Constructor "
    Public Sub New(ByRef IfDocumentsCount As IIfDOCUMENTSCOUNT, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(IfDocumentsCount, _wfPanelCircuit)
        InitializeComponent()
        Me._rule = IfDocumentsCount
    End Sub
#End Region

#Region " Load "
    Private Sub UCIfDocumentsCount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        'Si hay una CantidadTareas elegida la muestro en el control ctrlCantidadTareas
        If Not IsNothing(Me._rule.CantidadTareas) Then
            Me.ctrlCantidadTareas.Value = CDec(Me._rule.CantidadTareas)
        End If
        'Chequeo en la variable Comparacion de rules si hay algo elegido, sino como default: Igual
        Select Case Me._rule.Comparacion
            Case Comparadores.Igual
                Me.rbIgual.Checked = True
            Case Comparadores.Distinto
                Me.rbDistinto.Checked = True
            Case Comparadores.Mayor
                Me.rbMayor.Checked = True
            Case Comparadores.Menor
                Me.rbMenor.Checked = True
            Case Comparadores.IgualMayor
                Me.rbMayorIgual.Checked = True
            Case Comparadores.IgualMenor
                Me.rbMenorIgual.Checked = True
            Case Else
                Me.rbIgual.Checked = True
        End Select
    End Sub
#End Region

#Region " Eventos "
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        If Me.rbIgual.Checked Then
            _rule.Comparacion = Comparadores.Igual
        ElseIf Me.rbDistinto.Checked Then
            _rule.Comparacion = Comparadores.Distinto
        ElseIf Me.rbMayor.Checked Then
            _rule.Comparacion = Comparadores.Mayor
        ElseIf Me.rbMenor.Checked Then
            _rule.Comparacion = Comparadores.Menor
        ElseIf Me.rbMayorIgual.Checked Then
            _rule.Comparacion = Comparadores.IgualMayor
        ElseIf Me.rbMenorIgual.Checked Then
            _rule.Comparacion = Comparadores.IgualMenor
        End If

        Me._rule.CantidadTareas = Decimal.ToInt16(Me.ctrlCantidadTareas.Value)

        WFRulesBusiness.UpdateParamItem(Me._rule, 0, Int32.Parse(Me._rule.Comparacion))
        WFRulesBusiness.UpdateParamItem(Me._rule, 1, Me._rule.CantidadTareas)
        UserBusiness.Rights.SaveAction(_rule.ID, ObjectTypes.WFRules, Zamba.Core.RightsType.Edit, "Se editaron los datos de la regla " & _rule.Name & "(" & _rule.ID & ")")
    End Sub
#End Region

End Class


'    Implements IRule

'#Region " Código generado por el Diseñador de Windows Forms "

'    'UserControl1 reemplaza a Dispose para limpiar la lista de componentes.
'    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
'        If disposing Then
'            If Not (components Is Nothing) Then
'                components.Dispose()
'            End If
'        End If
'        MyBase.Dispose(disposing)
'    End Sub

'    'Requerido por el Diseñador de Windows Forms
'    Private components As System.ComponentModel.IContainer

'    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
'    'Puede modificarse utilizando el Diseñador de Windows Forms. 
'    'No lo modifique con el editor de código.
'    Friend WithEvents Label1 As ZLabel
'    Friend WithEvents NumericUpDown As System.Windows.Forms.NumericUpDown
'    Friend WithEvents cmbOperador As System.Windows.Forms.ComboBox
'    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
'    Public WithEvents LBLy As ZLabel
'    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
'        Me.Label1 = New ZLabel
'        Me.NumericUpDown = New System.Windows.Forms.NumericUpDown
'        Me.cmbOperador = New System.Windows.Forms.ComboBox
'        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown
'        Me.LBLy = New ZLabel
'        CType(Me.NumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
'        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.SuspendLayout()
'        '
'        'Label1
'        '
'        Me.Label1.AutoSize = True
'        Me.Label1.BackColor = System.Drawing.Color.Transparent
'        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(76,76,76)
'        Me.Label1.Location = New System.Drawing.Point(24, 27)
'        Me.Label1.Name = "Label1"
'        Me.Label1.Size = New System.Drawing.Size(206, 18)
'        Me.Label1.TabIndex = 0
'        Me.Label1.Text = "Cantidad de Documentos de la Etapa"
'        '
'        'NumericUpDown
'        '
'        Me.NumericUpDown.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.NumericUpDown.Location = New System.Drawing.Point(120, 56)
'        Me.NumericUpDown.Name = "NumericUpDown"
'        Me.NumericUpDown.Size = New System.Drawing.Size(56, 22)
'        Me.NumericUpDown.TabIndex = 1
'        Me.NumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'        '
'        'cmbOperador
'        '
'        Me.cmbOperador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
'        Me.cmbOperador.Location = New System.Drawing.Point(24, 56)
'        Me.cmbOperador.Name = "cmbOperador"
'        Me.cmbOperador.Size = New System.Drawing.Size(80, 21)
'        Me.cmbOperador.TabIndex = 8
'        '
'        'NumericUpDown2
'        '
'        Me.NumericUpDown2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.NumericUpDown2.Location = New System.Drawing.Point(208, 56)
'        Me.NumericUpDown2.Name = "NumericUpDown2"
'        Me.NumericUpDown2.Size = New System.Drawing.Size(56, 22)
'        Me.NumericUpDown2.TabIndex = 9
'        Me.NumericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
'        Me.NumericUpDown2.Visible = False
'        '
'        'LBLy
'        '
'        Me.LBLy.AutoSize = True
'        Me.LBLy.BackColor = System.Drawing.Color.Transparent
'        Me.LBLy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.LBLy.ForeColor = System.Drawing.Color.FromArgb(76,76,76)
'        Me.LBLy.Location = New System.Drawing.Point(186, 58)
'        Me.LBLy.Name = "LBLy"
'        Me.LBLy.Size = New System.Drawing.Size(12, 18)
'        Me.LBLy.TabIndex = 10
'        Me.LBLy.Text = "Y"
'        Me.LBLy.Visible = False
'        '
'        'UCIfDocumentsCount
'        '
'        Me.Controls.Add(Me.LBLy)
'        Me.Controls.Add(Me.NumericUpDown2)
'        Me.Controls.Add(Me.cmbOperador)
'        Me.Controls.Add(Me.NumericUpDown)
'        Me.Controls.Add(Me.Label1)
'        Me.Name = "UCIfDocumentsCount"
'        Me.Size = New System.Drawing.Size(288, 104)
'        CType(Me.NumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
'        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.ResumeLayout(False)

'    End Sub

'#End Region

'    Dim Wfstep As WFStep
'    Dim WfRule As WFRule
'    Public Sub New(byref wfstep As WFStep, ByRef WfRule As WFRule, ByVal isreadonly As Boolean)
'        MyBase.New()
'        Me.InitializeComponent()
'        Me.Wfstep = wfstep
'        Me.WfRule = WfRule
'        Me.Enabled = isreadonly
'    End Sub

'    Public Function AddParam() As ZGroupParam Implements IRule.AddParam
'        Dim ZGroupParam As ZGroupParam
'        Try

'            If Me.cmbOperador.Text = "Entre" Then
'                If Me.NumericUpDown2.Value <= Me.NumericUpDown.Value Then
'                    MessageBox.Show("El segundo valor no puede ser menor o igual que el primero")
'                    Return Nothing
'                End If
'                If Me.NumericUpDown2.Text = "" Then Return Nothing
'            End If

'            If Not Me.NumericUpDown.Text = "" Then
'                ZGroupParam = New ZGroupParam(WFBusiness.GetNewParamId)
'                ZGroupParam.AddParam(New ZParam("", "OPERADOR", cmbOperador.Text, ZGroupParam.Id))
'                ZGroupParam.AddParam(New ZParam("", "VALOR", Me.NumericUpDown.Value, ZGroupParam.Id))
'                If Me.cmbOperador.Text = "Entre" Then ZGroupParam.AddParam(New ZParam("", "VALOR 2", Me.NumericUpDown2.Value, ZGroupParam.Id))
'                Return ZGroupParam
'            Else
'                Return Nothing
'            End If
'        Catch ex As Exception
'            zamba.core.zclass.raiseerror(ex)
'            Return Nothing
'        End Try
'    End Function

'    Private Sub FillOperators()
'        Me.cmbOperador.Items.Add("=")
'        Me.cmbOperador.Items.Add(">")
'        Me.cmbOperador.Items.Add("<")
'        Me.cmbOperador.Items.Add(">=")
'        Me.cmbOperador.Items.Add("<=")
'        Me.cmbOperador.Items.Add("<>")
'        Me.cmbOperador.Items.Add("Entre")
'        Me.cmbOperador.Items.Add("Contiene")
'        Me.cmbOperador.Items.Add("Empieza")
'        Me.cmbOperador.Items.Add("Alguno")
'    End Sub

'    Private Sub UCIfDocumentsCount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        Try
'            FillOperators()
'        Catch ex As Exception
'            zamba.core.zclass.raiseerror(ex)
'        End Try
'    End Sub

'    Private Sub cmbOperador_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOperador.SelectedIndexChanged
'        If Me.cmbOperador.Text = "Entre" Then
'            Me.LBLy.Visible = True
'            Me.NumericUpDown2.Visible = True
'        Else
'            Me.LBLy.Visible = False
'            Me.NumericUpDown2.Visible = False
'        End If
'    End Sub
'End Class
