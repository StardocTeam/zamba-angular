Public Class UCDODeleteFile
    Inherits ZRuleControl

    Friend WithEvents lblEnConstruccion As ZLabel
    Public Sub New(ByRef DoDelFile As IDoDeleteFile, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoDelFile, _wfPanelCircuit)
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
        Me.lblEnConstruccion.Location = New System.Drawing.Point(22, 23)
        Me.lblEnConstruccion.Name = "lblEnConstruccion"
        Me.lblEnConstruccion.Size = New System.Drawing.Size(262, 13)
        Me.lblEnConstruccion.TabIndex = 1
        Me.lblEnConstruccion.Text = "Esta regla se encuentra actulmente en construcci�n. "
        '
        'UCDODeleteFile
        '
        Me.Name = "UCDODeleteFile"
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
End Class

'Imports ZAMBA.Core
'Imports DocTypesFactory
'Public Class UCDoDeleteFile
'    Inherits UcRule

'#Region " C�digo generado por el Dise�ador de Windows Forms "

'    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
'    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
'        If disposing Then
'            If Not (components Is Nothing) Then
'                components.Dispose()
'            End If
'        End If
'        MyBase.Dispose(disposing)
'    End Sub

'    'Requerido por el Dise�ador de Windows Forms
'    Private components As System.ComponentModel.IContainer

'    'NOTA: el Dise�ador de Windows Forms requiere el siguiente procedimiento
'    'Puede modificarse utilizando el Dise�ador de Windows Forms. 
'    'No lo modifique con el editor de c�digo.
'    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
'    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
'    Friend WithEvents btnadd As ZAMBA.AppBlock.ZButton
'    Friend WithEvents btnsearch As ZAMBA.AppBlock.ZButton
'    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
'    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
'    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
'    Friend WithEvents titulo As ZLabel
'    Friend WithEvents txtpath As System.Windows.Forms.TextBox
'    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
'        Me.ComboBox1 = New System.Windows.Forms.ComboBox
'        Me.ComboBox2 = New System.Windows.Forms.ComboBox
'        Me.btnadd = New Zamba.AppBlock.ZButton
'        Me.txtpath = New System.Windows.Forms.TextBox
'        Me.btnsearch = New Zamba.AppBlock.ZButton
'        Me.GroupBox1 = New System.Windows.Forms.GroupBox
'        Me.RadioButton2 = New System.Windows.Forms.RadioButton
'        Me.RadioButton1 = New System.Windows.Forms.RadioButton
'        Me.titulo = New ZLabel
'        Me.GroupBox1.SuspendLayout()
'        '
'        'ComboBox1
'        '
'        Me.ComboBox1.Location = New System.Drawing.Point(16, 128)
'        Me.ComboBox1.Name = "ComboBox1"
'        Me.ComboBox1.Size = New System.Drawing.Size(120, 21)
'        Me.ComboBox1.TabIndex = 0
'        '
'        'ComboBox2
'        '
'        Me.ComboBox2.Location = New System.Drawing.Point(16, 160)
'        Me.ComboBox2.Name = "ComboBox2"
'        Me.ComboBox2.Size = New System.Drawing.Size(120, 21)
'        Me.ComboBox2.TabIndex = 1
'        '
'        'btnadd
'        '
'        Me.btnadd.DialogResult = System.Windows.Forms.DialogResult.None
'        Me.btnadd.Location = New System.Drawing.Point(144, 160)
'        Me.btnadd.Name = "btnadd"
'        Me.btnadd.Size = New System.Drawing.Size(80, 24)
'        Me.btnadd.TabIndex = 2
'        Me.btnadd.Text = "Agregar"
'        '
'        'txtpath
'        '
'        Me.txtpath.Location = New System.Drawing.Point(8, 216)
'        Me.txtpath.Name = "txtpath"
'        Me.txtpath.Size = New System.Drawing.Size(200, 21)
'        Me.txtpath.TabIndex = 4
'        Me.txtpath.Text = ""
'        '
'        'btnsearch
'        '
'        Me.btnsearch.DialogResult = System.Windows.Forms.DialogResult.None
'        Me.btnsearch.Location = New System.Drawing.Point(208, 216)
'        Me.btnsearch.Name = "btnsearch"
'        Me.btnsearch.Size = New System.Drawing.Size(80, 24)
'        Me.btnsearch.TabIndex = 5
'        Me.btnsearch.Text = "Buscar"
'        '
'        'GroupBox1
'        '
'        Me.GroupBox1.Controls.Add(Me.RadioButton2)
'        Me.GroupBox1.Controls.Add(Me.RadioButton1)
'        Me.GroupBox1.Location = New System.Drawing.Point(16, 72)
'        Me.GroupBox1.Name = "GroupBox1"
'        Me.GroupBox1.Size = New System.Drawing.Size(272, 40)
'        Me.GroupBox1.TabIndex = 6
'        Me.GroupBox1.TabStop = False
'        '
'        'RadioButton2
'        '
'        Me.RadioButton2.Location = New System.Drawing.Point(136, 16)
'        Me.RadioButton2.Name = "RadioButton2"
'        Me.RadioButton2.Size = New System.Drawing.Size(128, 16)
'        Me.RadioButton2.TabIndex = 1
'        Me.RadioButton2.Text = "Buscar en Atributos"
'        '
'        'RadioButton1
'        '
'        Me.RadioButton1.Checked = True
'        Me.RadioButton1.Location = New System.Drawing.Point(8, 16)
'        Me.RadioButton1.Name = "RadioButton1"
'        Me.RadioButton1.Size = New System.Drawing.Size(120, 16)
'        Me.RadioButton1.TabIndex = 0
'        Me.RadioButton1.TabStop = True
'        Me.RadioButton1.Text = "Archivo existente"
'        '
'        'titulo
'        '
'        Me.titulo.Font = New System.Drawing.Font("Georgia", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.titulo.ForeColor = System.Drawing.SystemColors.Desktop
'        Me.titulo.Location = New System.Drawing.Point(16, 24)
'        Me.titulo.Name = "titulo"
'        Me.titulo.Size = New System.Drawing.Size(272, 24)
'        Me.titulo.TabIndex = 7
'        Me.titulo.Text = "Borrar Archivo"
'        Me.titulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'        '
'        'UCDoDeleteFile
'        '
'        Me.Controls.Add(Me.ComboBox1)
'        Me.Controls.Add(Me.ComboBox2)
'        Me.Controls.Add(Me.GroupBox1)
'        Me.Controls.Add(Me.titulo)
'        Me.Controls.Add(Me.txtpath)
'        Me.Controls.Add(Me.btnadd)
'        Me.Controls.Add(Me.btnsearch)
'        Me.Name = "UCDoDeleteFile"
'        Me.Size = New System.Drawing.Size(304, 552)
'        Me.GroupBox1.ResumeLayout(False)

'    End Sub

'#End Region
'#Region "Metodos Heredados"
'    'Constructor por defecto
'    Public Sub New(byref wfstep As WfStep, ByRef WfRule As WfRule, ByVal isreadonly As Boolean)
'        MyBase.New(wfstep, WfRule, isreadonly)
'        Me.InitializeComponent()
'    End Sub
'#End Region
'#Region "Propietary"
'#Region "Variables"
'    Dim archivo As String
'    Dim doctypename As String
'    Dim docTypeId as Int64
'    Dim DsDocTypes As DSDocType
'    Dim DsIndex As DsIndex
'    Dim atributos As New ArrayList
'#End Region
'#Region "Cargar Combos"
'    Private Sub FillDocTypes()
'        RemoveHandler ComboBox1.SelectedIndexChanged, AddressOf ComboBox1_SelectedIndexChanged
'        Me.DsDocTypes = GetDocTypesDataset()
'        Me.ComboBox1.DataSource = DsDocTypes.Doc_Type
'        Me.ComboBox1.DisplayMember = "DOC_TYPE_NAME"
'        Me.ComboBox1.ValueMember = "DOC_TYPE_ID"
'        AddHandler ComboBox1.SelectedIndexChanged, AddressOf ComboBox1_SelectedIndexChanged
'    End Sub
'    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
'        If Me.ComboBox1.SelectedIndex <> -1 Then
'            doctypeid = Me.DsDocTypes.Doc_Type(Me.ComboBox1.SelectedIndex).DOC_TYPE_ID

'            Dim Doctypename As String = Me.ComboBox1.Text
'            'Dim ParamId As Int32 = WFBusiness.GetNewParamId
'            'AddParam(Doctypename, "DOCTYPEID", DocTYpeId, ParamId)
'            FillIndex(doctypeid)
'        End If
'    End Sub
'    Private Sub FillIndex(ByVal docTypeId as Int64)
'        Me.DsIndex = Zamba.Core.Indexs_Factory.GetIndexSchema(Doctypeid)
'        Me.ComboBox2.DataSource = DsIndex.DOC_INDEX
'        Me.ComboBox2.DisplayMember = "Index_Name"
'        Me.ComboBox2.ValueMember = "Index_Id"
'    End Sub
'#End Region
'    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
'        txtpath.Text &= "<<" & ComboBox2.Text & ">>"
'        atributos.Add("<<" & ComboBox2.Text & ">>")
'    End Sub
'    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
'        Try
'            Dim dlg As New OpenFileDialog
'            dlg.Multiselect = False
'            dlg.ShowDialog()
'            txtpath.Text = dlg.FileName
'        Catch
'        End Try
'    End Sub
'    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'        Try
'            ZGroupParam = New ZGroupParam(WFBusiness.GetNewParamId)
'            Dim i As Int16
'            If txtpath.Text.Trim <> "" Then
'                If txtpath.Text.IndexOf("<<") = -1 Then
'                    ZGroupParam.AddParam(New ZParam("", "DELETEFILE", Me.txtpath.Text.Trim, ZGroupParam.Id))
'                Else
'                    ZGroupParam.AddParam(New ZParam("", "DELETEFILE", "", ZGroupParam.Id))
'                    For i = 0 To atributos.Count - 1
'                        ZGroupParam.AddParam(New ZParam("", "INDEX", atributos(i), ZGroupParam.Id))
'                    Next
'                End If
'            End If
'        Catch ex As Exception
'            zamba.core.zclass.raiseerror(ex)
'        End Try
'    End Sub
'    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
'        If RadioButton1.Checked Then
'            ComboBox1.Enabled = False
'            ComboBox2.Enabled = False
'            btnadd.Enabled = False
'            btnsearch.Enabled = True
'        Else
'            ComboBox1.Enabled = True
'            ComboBox2.Enabled = True
'            btnadd.Enabled = True
'            btnsearch.Enabled = False
'        End If
'    End Sub
'    Private Sub UCDoDeleteFile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        Me.FillDocTypes()
'    End Sub
'#End Region


'End Class