Imports Zamba.Core
Imports Zamba.AppBlock
'Imports Zamba.WFBusiness
Imports System.Windows.Forms

''' <summary>
''' User Control de la Regla IfFileExits
''' </summary>
''' <remarks></remarks>
Public Class UCIfDoAction
    Inherits ZRuleControl

    Friend WithEvents lblEnConstruccion As ZLabel
    Public Sub New(ByVal rule As IIfDoAction, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
    End Sub

    Private Shadows Sub InitializeComponent()
        Me.lblEnConstruccion = New ZLabel
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.lblEnConstruccion)
        '
        'lblEnConstruccion
        '
        Me.lblEnConstruccion.AutoSize = True
        Me.lblEnConstruccion.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblEnConstruccion.Location = New System.Drawing.Point(21, 19)
        Me.lblEnConstruccion.Name = "lblEnConstruccion"
        Me.lblEnConstruccion.Size = New System.Drawing.Size(262, 13)
        Me.lblEnConstruccion.TabIndex = 1
        Me.lblEnConstruccion.Text = "Esta regla se encuentra actulmente en construcción. "
        '
        'UCIfDoAction
        '
        Me.Name = "UCIfDoAction"
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
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

