Public Class UCDOReadFile
    Inherits ZRuleControl

    Public Sub New(ByVal rule As IDoReadFile, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
    End Sub
End Class

'Imports ZAMBA.Core
'Imports DocTypesFactory
'Public Class UCDoReadFile
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
'    Friend WithEvents txtpath As System.Windows.Forms.TextBox
'    Friend WithEvents btnsearch As ZAMBA.AppBlock.ZButton
'    Friend WithEvents lbltitulo As ZLabel
'    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
'    Friend WithEvents rdtxt As System.Windows.Forms.RadioButton
'    Friend WithEvents rdxml As System.Windows.Forms.RadioButton
'    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
'    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
'    Friend WithEvents btnadd As ZAMBA.AppBlock.ZButton
'    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
'        Me.txtpath = New System.Windows.Forms.TextBox
'        Me.btnsearch = New Zamba.AppBlock.ZButton
'        Me.lbltitulo = New ZLabel
'        Me.GroupBox1 = New System.Windows.Forms.GroupBox
'        Me.rdxml = New System.Windows.Forms.RadioButton
'        Me.rdtxt = New System.Windows.Forms.RadioButton
'        Me.ComboBox1 = New System.Windows.Forms.ComboBox
'        Me.ComboBox2 = New System.Windows.Forms.ComboBox
'        Me.btnadd = New Zamba.AppBlock.ZButton
'        Me.GroupBox1.SuspendLayout()
'        '
'        'txtpath
'        '
'        Me.txtpath.Location = New System.Drawing.Point(8, 136)
'        Me.txtpath.Name = "txtpath"
'        Me.txtpath.Size = New System.Drawing.Size(248, 21)
'        Me.txtpath.TabIndex = 0
'        Me.txtpath.Text = ""
'        '
'        'btnsearch
'        '
'        Me.btnsearch.DialogResult = System.Windows.Forms.DialogResult.None
'        Me.btnsearch.Location = New System.Drawing.Point(256, 136)
'        Me.btnsearch.Name = "btnsearch"
'        Me.btnsearch.Size = New System.Drawing.Size(64, 24)
'        Me.btnsearch.TabIndex = 1
'        Me.btnsearch.Text = "Buscar"
'        '
'        'lbltitulo
'        '
'        Me.lbltitulo.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily()
'        Me.lbltitulo.Location = New System.Drawing.Point(24, 16)
'        Me.lbltitulo.Name = "lbltitulo"
'        Me.lbltitulo.Size = New System.Drawing.Size(264, 16)
'        Me.lbltitulo.TabIndex = 2
'        Me.lbltitulo.Text = "Seleccione el archivo a leer"
'        Me.lbltitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'        '
'        'GroupBox1
'        '
'        Me.GroupBox1.Controls.Add(Me.rdxml)
'        Me.GroupBox1.Controls.Add(Me.rdtxt)
'        Me.GroupBox1.Location = New System.Drawing.Point(16, 40)
'        Me.GroupBox1.Name = "GroupBox1"
'        Me.GroupBox1.Size = New System.Drawing.Size(280, 40)
'        Me.GroupBox1.TabIndex = 3
'        Me.GroupBox1.TabStop = False
'        Me.GroupBox1.Text = "Tipo de Archivo"
'        '
'        'rdxml
'        '
'        Me.rdxml.Location = New System.Drawing.Point(152, 16)
'        Me.rdxml.Name = "rdxml"
'        Me.rdxml.Size = New System.Drawing.Size(80, 16)
'        Me.rdxml.TabIndex = 1
'        Me.rdxml.Text = "Xml"
'        '
'        'rdtxt
'        '
'        Me.rdtxt.Checked = True
'        Me.rdtxt.Location = New System.Drawing.Point(48, 16)
'        Me.rdtxt.Name = "rdtxt"
'        Me.rdtxt.Size = New System.Drawing.Size(80, 16)
'        Me.rdtxt.TabIndex = 0
'        Me.rdtxt.TabStop = True
'        Me.rdtxt.Text = "Texto"
'        '
'        'ComboBox1
'        '
'        Me.ComboBox1.Location = New System.Drawing.Point(8, 96)
'        Me.ComboBox1.Name = "ComboBox1"
'        Me.ComboBox1.Size = New System.Drawing.Size(112, 21)
'        Me.ComboBox1.TabIndex = 4
'        '
'        'ComboBox2
'        '
'        Me.ComboBox2.Location = New System.Drawing.Point(120, 96)
'        Me.ComboBox2.Name = "ComboBox2"
'        Me.ComboBox2.Size = New System.Drawing.Size(128, 21)
'        Me.ComboBox2.TabIndex = 5
'        '
'        'btnadd
'        '
'        Me.btnadd.DialogResult = System.Windows.Forms.DialogResult.None
'        Me.btnadd.Location = New System.Drawing.Point(248, 96)
'        Me.btnadd.Name = "btnadd"
'        Me.btnadd.Size = New System.Drawing.Size(72, 24)
'        Me.btnadd.TabIndex = 6
'        Me.btnadd.Text = "Agregar"
'        '
'        'UCDoReadFile
'        '
'        Me.Controls.Add(Me.btnadd)
'        Me.Controls.Add(Me.btnsearch)
'        Me.Controls.Add(Me.ComboBox1)
'        Me.Controls.Add(Me.ComboBox2)
'        Me.Controls.Add(Me.GroupBox1)
'        Me.Controls.Add(Me.lbltitulo)
'        Me.Controls.Add(Me.txtpath)

'        Me.Name = "UCDoReadFile"
'        Me.Size = New System.Drawing.Size(328, 464)
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
'#Region "Variables locales"
'    Dim file As String
'    Dim filetype As String
'    Dim DsDocTypes As DSDocType
'    Dim docTypeId as Int64
'    Dim DsIndex As DsIndex
'#End Region
'#Region "Atributos y Documentos"
'    Private Sub UCDOReadFile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        Me.FillDocTypes()
'    End Sub
'    Private Sub FillDocTypes()
'        RemoveHandler ComboBox1.SelectedIndexChanged, AddressOf ComboBox1_SelectedIndexChanged
'        Me.DsDocTypes = GetDocTypesDataset()
'        Me.ComboBox1.DataSource = DsDocTypes.Doc_Type
'        Me.ComboBox1.DisplayMember = "DOC_TYPE_NAME"
'        Me.ComboBox1.ValueMember = "DOC_TYPE_ID"
'        AddHandler ComboBox1.SelectedIndexChanged, AddressOf ComboBox1_SelectedIndexChanged
'    End Sub
'    Private Sub FillIndex(ByVal docTypeId as Int64)
'        Me.DsIndex = ZAMBA.Core.Indexs_Factory.GetIndexSchema(Doctypeid)
'        Me.ComboBox2.DataSource = DsIndex.Doc_Index
'        Me.ComboBox2.DisplayMember = "Index_Name"
'        Me.ComboBox2.ValueMember = "Index_Id"
'    End Sub
'    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
'        Try
'            If Me.ComboBox1.SelectedIndex <> -1 Then
'                DocTYpeId = Me.DsDocTypes.Doc_Type(Me.ComboBox1.SelectedIndex).DOC_TYPE_ID

'                Dim Doctypename As String = Me.ComboBox1.Text
'                ZGroupParam = New ZGroupParam(WFBusiness.GetNewParamId)
'                ZGroupParam.AddParam(New ZParam(Doctypename, "DOCTYPEID", DocTYpeId, ZGroupParam.Id))
'                FillIndex(DocTYpeId)
'            End If
'        Catch ex As Exception
'            zamba.core.zclass.raiseerror(ex)
'        End Try
'    End Sub
'#End Region
'#Region "Eventos Botones"
'    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
'        Try
'            txtpath.Text &= "<<" & ComboBox2.Text & ">> "

'        Catch
'        End Try
'    End Sub
'    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'        Dim clave As String
'        Try
'            file = txtpath.Text
'            Dim ParamId As Int32 = WFBusiness.GetNewParamId
'            If file <> Nothing Then
'                If file.IndexOf("<<") = -1 Then
'                    Try
'                        Dim Arch As New IO.FileInfo(file)
'                        If Not Arch.Exists Then
'                            MessageBox.Show("El archivo ingresado no existe", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
'                            Exit Sub
'                        End If
'                        clave = InputBox("Ingrese un nombre para recuperar el archivo en proximas etapas: ", "Ingrese clave")
'                        If clave = Nothing Then
'                            MessageBox.Show("La clave no puede ser nula", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
'                            Exit Sub
'                        End If
'                        Me.Guardararchivo(file, ParamId, clave)
'                    Catch
'                    End Try
'                Else
'                    Try
'                        Dim campos() As String
'                        Dim fields As New ArrayList
'                        Dim i As Int16
'                        clave = InputBox("Ingrese un nombre para recuperar el archivo en proximas etapas: ", "Ingrese clave")
'                        campos = txtpath.Text.Split(" ")
'                        For i = 0 To campos.Length - 1
'                            If campos(i) <> " " And campos(i) <> "" Then fields.Add(campos(i))
'                        Next
'                        Me.GuardarIndices(fields, ParamId, clave)
'                    Catch ex As Exception
'                    End Try
'                End If
'            Else
'                MessageBox.Show("Debe ingresar un archivo.", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            End If
'        Catch
'        End Try
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
'#End Region

'#Region "Guardar"
'    Private Sub Guardararchivo(ByVal archivo As String, ByVal ParamId As Int32, ByVal clave As String)
'        ZGroupParam = New ZGroupParam(WFBusiness.GetNewParamId)
'        ZGroupParam.AddParam(New ZParam("archivo", "FILE", archivo, ParamId))
'        ZGroupParam.AddParam(New ZParam("Extension", "FILEEXTENSION", Me.filetype, ParamId))
'        ZGroupParam.AddParam(New ZParam("Clave", "KEY", clave, ParamId))
'    End Sub


'    Private Sub GuardarIndices(ByVal atributos As ArrayList, ByVal ParamId As Int32, ByVal clave As String)
'        ZGroupParam = New ZGroupParam(WFBusiness.GetNewParamId)

'        Dim i As Int16
'        For i = 0 To atributos.Count - 1
'            ZGroupParam.AddParam(New ZParam("Index", "INDEX", atributos(i), ParamId))
'        Next
'        ZGroupParam.AddParam(New ZParam("Extension", "FILEEXTENSION", Me.filetype, ParamId))
'        ZGroupParam.AddParam(New ZParam("Clave", "KEY", clave, ParamId))
'    End Sub

'#End Region

'    Private Sub rdtxt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdtxt.CheckedChanged
'        filetype = ".txt"
'    End Sub
'    Private Sub rdxml_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdxml.CheckedChanged
'        filetype = ".xml"
'    End Sub
'#End Region

'End Class
