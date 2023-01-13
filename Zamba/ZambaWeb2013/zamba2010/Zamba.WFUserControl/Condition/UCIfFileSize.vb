Imports Zamba.Core
Imports Zamba.AppBlock
'Imports Zamba.WFBusiness
Imports System.Windows.Forms

''' <summary>
''' User Control de la Regla IfFileExits
''' </summary>
''' <remarks></remarks>
Public Class UCIfFileSize
    Inherits ZRuleControl

#Region " Variables "
    Friend WithEvents nud2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents rbDistinto As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbMayorIgual As System.Windows.Forms.RadioButton
    Friend WithEvents rbMayor As System.Windows.Forms.RadioButton
    Friend WithEvents rbMenor As System.Windows.Forms.RadioButton
    Friend WithEvents rbIgual As System.Windows.Forms.RadioButton
    Friend WithEvents rbMenorIgual As System.Windows.Forms.RadioButton
    Friend WithEvents rbEntre As System.Windows.Forms.RadioButton
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents nud1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents lbDireccion As ZLabel
    Friend WithEvents tbDireccion As System.Windows.Forms.TextBox
    Friend WithEvents btnDireccion As ZButton
    Friend WithEvents btnAceptar As ZButton
    Dim direccionArchivo As OpenFileDialog
    Friend WithEvents lblEnConstruccion As ZLabel
    Dim _rule As IIfFileSize
#End Region

#Region " Inicializacion de WindowsForm "
    Private Shadows Sub InitializeComponent()
        Me.nud2 = New System.Windows.Forms.NumericUpDown
        Me.rbDistinto = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rbEntre = New System.Windows.Forms.RadioButton
        Me.rbMayorIgual = New System.Windows.Forms.RadioButton
        Me.rbMayor = New System.Windows.Forms.RadioButton
        Me.rbMenor = New System.Windows.Forms.RadioButton
        Me.rbIgual = New System.Windows.Forms.RadioButton
        Me.rbMenorIgual = New System.Windows.Forms.RadioButton
        Me.Label1 = New ZLabel
        Me.Label2 = New ZLabel
        Me.btnAceptar = New ZButton
        Me.nud1 = New System.Windows.Forms.NumericUpDown
        Me.lbDireccion = New ZLabel
        Me.tbDireccion = New System.Windows.Forms.TextBox
        Me.btnDireccion = New ZButton
        Me.lblEnConstruccion = New ZLabel
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        CType(Me.nud2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nud1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.lblEnConstruccion)
        Me.tbRule.Size = New System.Drawing.Size(504, 327)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(512, 353)
        '
        'nud2
        '
        Me.nud2.Location = New System.Drawing.Point(309, 181)
        Me.nud2.Maximum = New Decimal(New Integer() {-96, 0, 0, 0})
        Me.nud2.Name = "nud2"
        Me.nud2.Size = New System.Drawing.Size(120, 21)
        Me.nud2.TabIndex = 25
        Me.nud2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.rbEntre)
        Me.GroupBox1.Controls.Add(Me.rbDistinto)
        Me.GroupBox1.Controls.Add(Me.rbMayorIgual)
        Me.GroupBox1.Controls.Add(Me.rbMayor)
        Me.GroupBox1.Controls.Add(Me.rbMenor)
        Me.GroupBox1.Controls.Add(Me.rbIgual)
        Me.GroupBox1.Controls.Add(Me.rbMenorIgual)
        Me.GroupBox1.Location = New System.Drawing.Point(40, 35)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(138, 193)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Condición"
        '
        'rbEntre
        '
        Me.rbEntre.AutoSize = True
        Me.rbEntre.Location = New System.Drawing.Point(24, 157)
        Me.rbEntre.Name = "rbEntre"
        Me.rbEntre.Size = New System.Drawing.Size(51, 17)
        Me.rbEntre.TabIndex = 7
        Me.rbEntre.TabStop = True
        Me.rbEntre.Text = "Entre"
        Me.rbEntre.UseVisualStyleBackColor = True
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(246, 145)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(246, 183)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 13)
        Me.Label2.TabIndex = 28
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(197, 286)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
        Me.btnAceptar.TabIndex = 29
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'nud1
        '
        Me.nud1.Location = New System.Drawing.Point(309, 143)
        Me.nud1.Maximum = New Decimal(New Integer() {-96, 0, 0, 0})
        Me.nud1.Name = "nud1"
        Me.nud1.Size = New System.Drawing.Size(120, 21)
        Me.nud1.TabIndex = 24
        Me.nud1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbDireccion
        '
        Me.lbDireccion.AutoSize = True
        Me.lbDireccion.BackColor = System.Drawing.Color.Transparent
        Me.lbDireccion.Location = New System.Drawing.Point(240, 74)
        Me.lbDireccion.Name = "lbDireccion"
        Me.lbDireccion.Size = New System.Drawing.Size(50, 13)
        Me.lbDireccion.TabIndex = 31
        Me.lbDireccion.Text = "Dirección"
        '
        'tbDireccion
        '
        Me.tbDireccion.Location = New System.Drawing.Point(240, 96)
        Me.tbDireccion.Name = "tbDireccion"
        Me.tbDireccion.Size = New System.Drawing.Size(189, 21)
        Me.tbDireccion.TabIndex = 32
        '
        'btnDireccion
        '
        Me.btnDireccion.Location = New System.Drawing.Point(435, 94)
        Me.btnDireccion.Name = "btnDireccion"
        Me.btnDireccion.Size = New System.Drawing.Size(28, 23)
        Me.btnDireccion.TabIndex = 33
        Me.btnDireccion.Text = "..."
        Me.btnDireccion.UseVisualStyleBackColor = True
        '
        'lblEnConstruccion
        '
        Me.lblEnConstruccion.AutoSize = True
        Me.lblEnConstruccion.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblEnConstruccion.Location = New System.Drawing.Point(24, 21)
        Me.lblEnConstruccion.Name = "lblEnConstruccion"
        Me.lblEnConstruccion.Size = New System.Drawing.Size(262, 13)
        Me.lblEnConstruccion.TabIndex = 1
        Me.lblEnConstruccion.Text = "Esta regla se encuentra actulmente en construcción. "
        '
        'UCIfFileSize
        '
        Me.Controls.Add(Me.btnDireccion)
        Me.Controls.Add(Me.tbDireccion)
        Me.Controls.Add(Me.lbDireccion)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.nud2)
        Me.Controls.Add(Me.nud1)
        Me.Name = "UCIfFileSize"
        Me.Size = New System.Drawing.Size(512, 353)
        Me.Controls.SetChildIndex(Me.nud1, 0)
        Me.Controls.SetChildIndex(Me.nud2, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.btnAceptar, 0)
        Me.Controls.SetChildIndex(Me.lbDireccion, 0)
        Me.Controls.SetChildIndex(Me.tbDireccion, 0)
        Me.Controls.SetChildIndex(Me.btnDireccion, 0)
        Me.Controls.SetChildIndex(Me.tbctrMain, 0)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        CType(Me.nud2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nud1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

#Region " Constructor "
    Public Sub New(ByRef Iffilesize As IIfFileSize, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(Iffilesize, _wfPanelCircuit)
        InitializeComponent()
        Me._rule = Iffilesize
    End Sub
#End Region

#Region " Eventos "
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        'If Not IsNothing(direccionArchivo) AndAlso Not IsNothing(direccionArchivo.FileName) Then
        If rbIgual.Checked Then
            Me._rule.Comparador = Comparacion.Igual
        End If
        If rbDistinto.Checked Then
            Me._rule.Comparador = Comparacion.Distinto
        End If
        If rbMayor.Checked Then
            Me._rule.Comparador = Comparacion.Mayor
        End If
        If rbMenor.Checked Then
            Me._rule.Comparador = Comparacion.Menor
        End If
        If rbMayorIgual.Checked Then
            Me._rule.Comparador = Comparacion.IgualMayor
        End If
        If rbMenorIgual.Checked Then
            Me._rule.Comparador = Comparacion.IgualMenor
        End If
        If rbEntre.Checked Then
            Me._rule.Comparador = Comparacion.Entre
        End If

        Me._rule.path = Me.tbDireccion.Text
        Me._rule.num1 = Decimal.ToUInt32(nud1.Value)
        Me._rule.num2 = Decimal.ToUInt32(nud2.Value)

        'ElseIf Me.tbDireccion.Text = "" Then

        'MessageBox.Show("Introduzca una ruta de archivo")

        'End If

        WFRulesBusiness.UpdateParamItem(Me._rule, 0, Me._rule.Comparador)
        WFRulesBusiness.UpdateParamItem(Me._rule, 1, Me._rule.path)
        WFRulesBusiness.UpdateParamItem(Me._rule, 2, Me._rule.num1)
        WFRulesBusiness.UpdateParamItem(Me._rule, 3, Me._rule.num2)
        UserBusiness.Rights.SaveAction(_rule.ID, ObjectTypes.WFRules, Zamba.Core.RightsType.Edit, "Se editaron los datos de la regla " & _rule.Name & "(" & _rule.ID & ")")

    End Sub

    Private Sub btnDireccion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDireccion.Click
        direccionArchivo = New OpenFileDialog()
        direccionArchivo.Title = "Seleccione un archivo para comparar"
        If (direccionArchivo.ShowDialog() = DialogResult.OK) Then
            If (direccionArchivo.FileName <> "") Then
                tbDireccion.Text = direccionArchivo.FileName
            End If
        End If
    End Sub

    Private Sub rbIgual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbIgual.CheckedChanged
        Me.Label1.Text = "Igual a"
        Me.Label2.Visible = False
        Me.nud2.Visible = False
    End Sub

    Private Sub rbDistinto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDistinto.CheckedChanged
        Me.Label1.Text = "Distinto a"
        Me.Label2.Visible = False
        Me.nud2.Visible = False
    End Sub

    Private Sub rbMayor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMayor.CheckedChanged
        Me.Label1.Text = "Mayor a"
        Me.Label2.Visible = False
        Me.nud2.Visible = False
    End Sub

    Private Sub rbMenor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMenor.CheckedChanged
        Me.Label1.Text = "Menor a"
        Me.Label2.Visible = False
        Me.nud2.Visible = False
    End Sub

    Private Sub rbMayorIgual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMayorIgual.CheckedChanged
        Me.Label1.Text = "My./Ig. a"
        Me.Label2.Visible = False
        Me.nud2.Visible = False
    End Sub

    Private Sub rbMenorIgual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMenorIgual.CheckedChanged
        Me.Label1.Text = "Mn./Ig. a"
        Me.Label2.Visible = False
        Me.nud2.Visible = False
    End Sub

    Private Sub rbEntre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbEntre.CheckedChanged
        Me.Label1.Text = "Mayor a"
        Me.Label2.Visible = True
        Me.Label2.Text = "Menor a"
        Me.nud2.Visible = True
    End Sub

    Private Sub UCIfFileSize_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        Select Case Me._rule.Comparador
            Case Comparacion.Igual
                Me.rbIgual.Checked = True
            Case Comparacion.Distinto
                Me.rbDistinto.Checked = True
            Case Comparacion.Mayor
                Me.rbEntre.Checked = True
            Case Comparacion.Menor
                Me.rbMenor.Checked = True
            Case Comparacion.IgualMayor
                Me.rbMayorIgual.Checked = True
            Case Comparacion.IgualMenor
                Me.rbMenorIgual.Checked = True
            Case Comparacion.Entre
                Me.rbEntre.Checked = True
            Case Else
                Me.rbIgual.Checked = True
        End Select

        If Not IsNothing(Me._rule.num1) Then
            nud1.Value = CDec(Me._rule.num1)
        End If
        If Not IsNothing(Me._rule.num2) Then
            nud2.Value = CDec(Me._rule.num2)
        End If
        tbDireccion.Text = Me._rule.path

    End Sub
#End Region

End Class

'Imports zamba.Core

'Imports Zamba.AppBlock
'Public Class UCIfFileSize
'    Inherits UcRule

'#Region "Metodos Heredados"
'    'Constructor por defecto
'    Public Sub New(byref wfstep As WfStep, ByRef WfRule As WfRule, ByVal isreadonly As Boolean)
'        MyBase.New(wfstep, WfRule, isreadonly)
'        Me.InitializeComponent()
'    End Sub
'#End Region


'#Region " Código generado por el Diseñador de Windows Forms "


'    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
'    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
'    Friend WithEvents Label2 As ZLabel
'    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
'    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
'    Friend WithEvents lblBytes As ZLabel
'    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
'    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
'        Me.ComboBox1 = New System.Windows.Forms.ComboBox
'        Me.TextBox1 = New System.Windows.Forms.TextBox
'        Me.GroupBox1 = New System.Windows.Forms.GroupBox
'        Me.ComboBox2 = New System.Windows.Forms.ComboBox
'        Me.lblBytes = New ZLabel
'        Me.GroupBox1.SuspendLayout()
'        '
'        'ComboBox1
'        '
'        Me.ComboBox1.Location = New System.Drawing.Point(8, 56)
'        Me.ComboBox1.Name = "ComboBox1"
'        Me.ComboBox1.Size = New System.Drawing.Size(72, 21)
'        Me.ComboBox1.TabIndex = 0
'        '
'        'TextBox1
'        '
'        Me.TextBox1.Location = New System.Drawing.Point(80, 56)
'        Me.TextBox1.Name = "TextBox1"
'        Me.TextBox1.Size = New System.Drawing.Size(200, 21)
'        Me.TextBox1.TabIndex = 2
'        Me.TextBox1.Text = ""
'        '
'        'GroupBox1
'        '
'        Me.GroupBox1.Controls.Add(Me.ComboBox2)
'        Me.GroupBox1.Controls.Add(Me.lblBytes)
'        Me.GroupBox1.Controls.Add(Me.ComboBox1)
'        Me.GroupBox1.Controls.Add(Me.TextBox1)
'        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
'        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
'        Me.GroupBox1.Name = "GroupBox1"
'        Me.GroupBox1.Size = New System.Drawing.Size(384, 256)
'        Me.GroupBox1.TabIndex = 5
'        Me.GroupBox1.TabStop = False
'        Me.GroupBox1.Text = "Regla sobre el archivo"
'        '
'        'ComboBox2
'        '
'        Me.ComboBox2.Location = New System.Drawing.Point(8, 24)
'        Me.ComboBox2.Name = "ComboBox2"
'        Me.ComboBox2.Size = New System.Drawing.Size(272, 21)
'        Me.ComboBox2.TabIndex = 7
'        '
'        'lblBytes
'        '
'        Me.lblBytes.Location = New System.Drawing.Point(288, 64)
'        Me.lblBytes.Name = "lblBytes"
'        Me.lblBytes.Size = New System.Drawing.Size(40, 16)
'        Me.lblBytes.TabIndex = 6
'        Me.lblBytes.Text = "Bytes"
'        '
'        'UCIfFileSize
'        '
'        Me.Controls.Add(Me.GroupBox1)
'        Me.AutoScroll = True
'        Me.AutoScrollMinSize = New System.Drawing.Size(384, 40)
'        Me.Name = "UCIfFileSize"
'        Me.Size = New System.Drawing.Size(384, 472)
'        Me.GroupBox1.ResumeLayout(False)

'    End Sub

'#End Region

'#Region "Propietary"
'    Private Sub FillOperators(ByVal s As String)

'        Dim last As String = Me.ComboBox1.Text

'        Me.ComboBox1.Items.Clear()

'        ComboBox1.Items.Add("=")
'        ComboBox1.Items.Add("<>")
'        Select Case s.ToLower
'            Case "tamaño", "fecha de creación", "fecha de modificación"
'                ComboBox1.Items.Add(">")
'                ComboBox1.Items.Add("<")
'                ComboBox1.Items.Add(">=")
'                ComboBox1.Items.Add("<=")
'            Case "extension", "nombre"
'                ComboBox1.Items.Add("Contiene")
'        End Select
'        Me.ComboBox1.Text = last
'    End Sub
'    Private Sub UCIfFileSize_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        Me.FillFileAttributesOptions()
'    End Sub
'    Private Sub FillFileAttributesOptions()
'        Me.ComboBox2.Items.Add("Tamaño")
'        Me.ComboBox2.Items.Add("Fecha de Creación")
'        Me.ComboBox2.Items.Add("Fecha de Modificación")
'        Me.ComboBox2.Items.Add("Extension")
'        Me.ComboBox2.Items.Add("Nombre")
'        Me.ComboBox2.SelectedIndex = 0
'    End Sub
'    Private Sub ComboBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.TextChanged
'        Me.FillOperators(Me.ComboBox2.Text)
'    End Sub
'#End Region

'    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'        Try
'            ZGroupParam = New ZGroupParam(WFBusiness.GetNewParamId)
'            ZGroupParam.AddParam(New ZParam("", "OPERATOR", Me.ComboBox1.Text.ToString, ZGroupParam.Id))
'            ZGroupParam.AddParam(New ZParam("", "FILEDATA", Me.TextBox1.Text, ZGroupParam.Id))
'            ZGroupParam.AddParam(New ZParam("", "FILEATTRIBUTE", CStr(Me.ComboBox2.Text), ZGroupParam.Id))


'            AddParams(ZGroupParam)
'            Me.ZLst.Items.Add(ZGroupParam)
'        Catch ex As Exception
'            zamba.core.zclass.raiseerror(ex)
'        End Try
'    End Sub
'    Friend Overrides Sub FillParamControls(ByVal ZGroupParam As ZGroupParam)
'        Try
'            Me.ComboBox1.Text = ZGroupParam.getParam("OPERATOR").Value.ToString
'            Me.TextBox1.Text = ZGroupParam.getParam("FILEDATA").Value.ToString
'            Me.ComboBox2.Text = ZGroupParam.getParam("FILEATTRIBUTE").Value.ToString

'            If ZGroupParam.getParam("SEPARADOR").Value.ToString = "AND" Then
'                Me.ZAND.Checked = True
'            Else
'                Me.ZOR.Checked = True
'            End If
'        Catch ex As Exception
'            zamba.core.zclass.raiseerror(ex)
'        End Try
'    End Sub
'End Class

