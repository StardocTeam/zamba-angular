Public Class UCDOUSERTASK
    Inherits ZRuleControl

    Friend WithEvents lblEnConstruccion As ZLabel
    Public Sub New(ByRef rule As IDOUSERTASK, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        Me.InitializeComponent()
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
        Me.lblEnConstruccion.Location = New System.Drawing.Point(18, 21)
        Me.lblEnConstruccion.Name = "lblEnConstruccion"
        Me.lblEnConstruccion.Size = New System.Drawing.Size(262, 13)
        Me.lblEnConstruccion.TabIndex = 1
        Me.lblEnConstruccion.Text = "Esta regla se encuentra actulmente en construcción. "
        '
        'UCDOUSERTASK
        '
        Me.Name = "UCDOUSERTASK"
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
End Class

'Imports ZAMBA.Core
'Imports DocTypesFactory
'Public Class UCDOScript
'    Inherits UcRule
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
'    Friend WithEvents Button1 As ZAMBA.AppBlock.ZButton
'    Friend WithEvents TXTPATH As System.Windows.Forms.TextBox
'    Friend WithEvents BtnAcept As ZAMBA.AppBlock.ZButton
'    Friend WithEvents Label2 As ZAMBA.AppBlock.ZLabel
'    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
'    Friend WithEvents Rdvbs As System.Windows.Forms.RadioButton
'    Friend WithEvents rdjs As System.Windows.Forms.RadioButton
'    Friend WithEvents rdbat As System.Windows.Forms.RadioButton
'    Friend WithEvents rdcreate As System.Windows.Forms.RadioButton
'    Friend WithEvents rdexisting As System.Windows.Forms.RadioButton
'    Friend WithEvents Button2 As ZAMBA.AppBlock.ZButton
'    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
'    Friend WithEvents btnadd As ZAMBA.AppBlock.ZButton
'    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
'    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
'        Me.Button1 = New Zamba.AppBlock.ZButton
'        Me.TXTPATH = New System.Windows.Forms.TextBox
'        Me.BtnAcept = New Zamba.AppBlock.ZButton
'        Me.Label2 = New Zamba.AppBlock.ZLabel
'        Me.TextBox1 = New System.Windows.Forms.TextBox
'        Me.Rdvbs = New System.Windows.Forms.RadioButton
'        Me.rdjs = New System.Windows.Forms.RadioButton
'        Me.rdbat = New System.Windows.Forms.RadioButton
'        Me.rdcreate = New System.Windows.Forms.RadioButton
'        Me.rdexisting = New System.Windows.Forms.RadioButton
'        Me.Button2 = New Zamba.AppBlock.ZButton
'        Me.ComboBox1 = New System.Windows.Forms.ComboBox
'        Me.btnadd = New Zamba.AppBlock.ZButton
'        Me.ComboBox2 = New System.Windows.Forms.ComboBox
'        '
'        'Button1
'        '
'        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.None
'        Me.Button1.Location = New System.Drawing.Point(264, 56)
'        Me.Button1.Name = "Button1"
'        Me.Button1.Size = New System.Drawing.Size(72, 24)
'        Me.Button1.TabIndex = 0
'        Me.Button1.Text = "Buscar"
'        '
'        'TXTPATH
'        '
'        Me.TXTPATH.Location = New System.Drawing.Point(8, 56)
'        Me.TXTPATH.Name = "TXTPATH"
'        Me.TXTPATH.Size = New System.Drawing.Size(256, 21)
'        Me.TXTPATH.TabIndex = 1
'        Me.TXTPATH.Text = ""
'        '
'        'BtnAcept
'        '
'        Me.BtnAcept.DialogResult = System.Windows.Forms.DialogResult.None
'        Me.BtnAcept.Location = New System.Drawing.Point(264, 88)
'        Me.BtnAcept.Name = "BtnAcept"
'        Me.BtnAcept.Size = New System.Drawing.Size(72, 24)
'        Me.BtnAcept.TabIndex = 2
'        Me.BtnAcept.Text = "Aceptar"
'        '
'        'Label2
'        '
'        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(214, Byte), CType(213, Byte), CType(217, Byte))
'        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!)
'        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(76,76,76)
'        Me.Label2.Location = New System.Drawing.Point(16, 8)
'        Me.Label2.Name = "Label2"
'        Me.Label2.Size = New System.Drawing.Size(288, 16)
'        Me.Label2.TabIndex = 3
'        Me.Label2.Text = "Seleccione el archivo que desea ejecutar"
'        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'        '
'        'TextBox1
'        '
'        Me.TextBox1.Location = New System.Drawing.Point(8, 152)
'        Me.TextBox1.Multiline = True
'        Me.TextBox1.Name = "TextBox1"
'        Me.TextBox1.Size = New System.Drawing.Size(312, 144)
'        Me.TextBox1.TabIndex = 4
'        Me.TextBox1.Text = ""
'        '
'        'Rdvbs
'        '
'        Me.Rdvbs.Enabled = False
'        Me.Rdvbs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.Rdvbs.Location = New System.Drawing.Point(16, 88)
'        Me.Rdvbs.Name = "Rdvbs"
'        Me.Rdvbs.Size = New System.Drawing.Size(64, 16)
'        Me.Rdvbs.TabIndex = 5
'        Me.Rdvbs.Text = "Vbs"
'        '
'        'rdjs
'        '
'        Me.rdjs.Enabled = False
'        Me.rdjs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.rdjs.Location = New System.Drawing.Point(80, 88)
'        Me.rdjs.Name = "rdjs"
'        Me.rdjs.Size = New System.Drawing.Size(48, 16)
'        Me.rdjs.TabIndex = 6
'        Me.rdjs.Text = "Js"
'        '
'        'rdbat
'        '
'        Me.rdbat.Enabled = False
'        Me.rdbat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.rdbat.Location = New System.Drawing.Point(144, 88)
'        Me.rdbat.Name = "rdbat"
'        Me.rdbat.Size = New System.Drawing.Size(64, 16)
'        Me.rdbat.TabIndex = 7
'        Me.rdbat.Text = "Bat"
'        '
'        'rdcreate
'        '
'        Me.rdcreate.Location = New System.Drawing.Point(24, 32)
'        Me.rdcreate.Name = "rdcreate"
'        Me.rdcreate.Size = New System.Drawing.Size(96, 16)
'        Me.rdcreate.TabIndex = 8
'        Me.rdcreate.Text = "Crear Nuevo"
'        '
'        'rdexisting
'        '
'        Me.rdexisting.Location = New System.Drawing.Point(128, 32)
'        Me.rdexisting.Name = "rdexisting"
'        Me.rdexisting.Size = New System.Drawing.Size(120, 24)
'        Me.rdexisting.TabIndex = 9
'        Me.rdexisting.Text = "Ejecutar Existente"
'        '
'        'Button2
'        '
'        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.None
'        Me.Button2.Location = New System.Drawing.Point(144, 304)
'        Me.Button2.Name = "Button2"
'        Me.Button2.Size = New System.Drawing.Size(168, 16)
'        Me.Button2.TabIndex = 10
'        Me.Button2.Text = "Generar Sript"
'        '
'        'ComboBox1
'        '
'        Me.ComboBox1.Location = New System.Drawing.Point(8, 120)
'        Me.ComboBox1.Name = "ComboBox1"
'        Me.ComboBox1.Size = New System.Drawing.Size(136, 21)
'        Me.ComboBox1.TabIndex = 11
'        '
'        'btnadd
'        '
'        Me.btnadd.DialogResult = System.Windows.Forms.DialogResult.None
'        Me.btnadd.Location = New System.Drawing.Point(264, 120)
'        Me.btnadd.Name = "btnadd"
'        Me.btnadd.Size = New System.Drawing.Size(72, 24)
'        Me.btnadd.TabIndex = 12
'        Me.btnadd.Text = "Agregar"
'        '
'        'ComboBox2
'        '
'        Me.ComboBox2.Location = New System.Drawing.Point(144, 120)
'        Me.ComboBox2.Name = "ComboBox2"
'        Me.ComboBox2.Size = New System.Drawing.Size(112, 21)
'        Me.ComboBox2.TabIndex = 13
'        '
'        'UCDOScript
'        '
'        Me.Controls.Add(Me.ComboBox1)
'        Me.Controls.Add(Me.ComboBox2)
'        Me.Controls.Add(Me.BtnAcept)
'        Me.Controls.Add(Me.btnadd)
'        Me.Controls.Add(Me.Button1)
'        Me.Controls.Add(Me.Button2)
'        Me.Controls.Add(Me.TextBox1)
'        Me.Controls.Add(Me.TXTPATH)
'        Me.Controls.Add(Me.Label2)
'        Me.Controls.Add(Me.rdbat)
'        Me.Controls.Add(Me.rdcreate)
'        Me.Controls.Add(Me.rdexisting)
'        Me.Controls.Add(Me.rdjs)
'        Me.Controls.Add(Me.Rdvbs)
'        Me.Name = "UCDOScript"
'        Me.Size = New System.Drawing.Size(352, 544)

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
'    Dim extension As String
'    Dim indixes As New ArrayList
'    Dim archivo As String
'    Dim DsDocTypes As DSDocType
'    Dim DsIndex As DsIndex
'    Dim docTypeId as Int64
'    Private Sub UCDOScript_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        Me.FillDocTypes()
'    End Sub
'    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
'        Dim dlg As New OpenFileDialog
'        Try
'            dlg.Multiselect = False
'            'dlg.Filter = "*.exe,*.vbs,*.js,*.bat"
'            dlg.ShowDialog()
'            If dlg.FileName = String.Empty Then Exit Sub
'            dlg.RestoreDirectory = True
'            dlg.Dispose()
'        Catch
'        End Try
'    End Sub
'    Private Sub rdcreate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdcreate.CheckedChanged
'        If rdcreate.Checked Then
'            TXTPATH.Text = ""
'            TXTPATH.Enabled = False
'            rdjs.Enabled = True
'            Rdvbs.Enabled = True
'            rdbat.Enabled = True
'            Button1.Enabled = False
'            BtnAcept.Enabled = False
'        Else
'            TXTPATH.Enabled = True
'            BtnAcept.Enabled = True
'            Button1.Enabled = True
'            rdjs.Enabled = False
'            Rdvbs.Enabled = False
'            rdbat.Enabled = False
'        End If
'    End Sub
'    Private Sub Rdvbs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rdvbs.CheckedChanged
'        If Rdvbs.Checked = True Then extension = ".vbs"
'    End Sub
'    Private Sub rdjs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdjs.CheckedChanged
'        If rdjs.Checked = True Then extension = ".js"
'    End Sub
'    Private Sub rdbat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbat.CheckedChanged
'        If rdbat.Checked = True Then extension = ".bat"
'    End Sub
'    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
'        Try
'            Dim i As Int16
'            Dim dlg As New FolderBrowserDialog
'            dlg.ShowDialog()
'            Dim nombre As String = InputBox("¿Con que nombre desea guardar el script o archivo de lotes creado?", "Guardar")
'            archivo = dlg.SelectedPath & "\" & nombre & extension
'            Dim File As New IO.FileInfo(archivo)
'            Dim sw As IO.StreamWriter
'            sw = New IO.StreamWriter(File.FullName, False)
'            For i = 0 To TextBox1.Lines.Length - 1
'                sw.WriteLine(TextBox1.Lines(i).ToString)
'            Next
'            sw.Close()
'        Catch ex As Exception
'            zamba.core.zclass.raiseerror(ex)
'        End Try
'    End Sub

'    Private Sub TXTPATH_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTPATH.TextChanged
'        archivo = TXTPATH.Text
'    End Sub
'    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
'        Dim atributo As String = ComboBox2.Text
'        indixes.Add(atributo)
'        TextBox1.Text &= "<<" & atributo & ">>"
'    End Sub
'    Private Sub FillDocTypes()
'        RemoveHandler ComboBox1.SelectedIndexChanged, AddressOf ComboBox1_SelectedIndexChanged
'        Me.DsDocTypes = GetDocTypesDataset()
'        Me.ComboBox1.DataSource = DsDocTypes.DOC_TYPE
'        Me.ComboBox1.DisplayMember = "DOC_TYPE_NAME"
'        Me.ComboBox1.ValueMember = "DOC_TYPE_ID"
'        AddHandler ComboBox1.SelectedIndexChanged, AddressOf ComboBox1_SelectedIndexChanged
'    End Sub
'    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
'        If Me.ComboBox1.SelectedIndex <> -1 Then
'            doctypeid = Me.DsDocTypes.DOC_TYPE(Me.ComboBox1.SelectedIndex).DOC_TYPE_ID

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

'    Private Sub ZButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'        Try
'            ZGroupParam = New ZGroupParam(WFBusiness.GetNewParamId)

'            ZGroupParam.AddParam(New ZParam("archivo", "FILE", archivo, ZGroupParam.Id))
'            Dim i As Int16
'            For i = 0 To indixes.Count - 1
'                ZGroupParam.AddParam(New ZParam("atributo", "INDEX", indixes(i), ZGroupParam.Id))
'            Next

'            AddParams(ZGroupParam)
'            Me.ZLst.Items.Add(ZGroupParam)
'        Catch ex As Exception
'            zamba.core.zclass.raiseerror(ex)
'        End Try
'    End Sub
'    Friend Overrides Sub FillParamControls(ByVal ZGroupParam As ZGroupParam)
'        Try


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
