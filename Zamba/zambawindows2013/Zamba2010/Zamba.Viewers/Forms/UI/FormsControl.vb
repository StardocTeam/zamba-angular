Imports Zamba.Core
Imports System.IO
Imports FormulariosDinamicos
Imports Zamba.AdminControls
Imports System.Collections.Generic
Imports System.Text


Public Class FormsControl
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        ' AddHandler FormBrowser.RefreshIndexs, AddressOf ActualizarIndices
        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub
    Public Shadows Event Finish()
    'Public Event RefreshIndexs()
    'Private Sub ActualizarIndices()
    '    'RaiseEvent RefreshIndexs()

    'End Sub
    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
        RaiseEvent Finish()
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As ComponentModel.IContainer
    Friend WithEvents Btnexplore As ZButton
    Friend WithEvents lstForms As ListBox
    Friend WithEvents Panel3 As ZPanel
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents ContextMenu1 As ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents chkUseRuleRights As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseBlob As System.Windows.Forms.CheckBox
    Friend WithEvents Panel4 As ZPanel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents ZLabel3 As ZLabel
    Friend WithEvents cbodoctype As ComboBox
    Friend WithEvents ZLabel2 As ZLabel
    Friend WithEvents cbotype As ComboBox
    Friend WithEvents ZLabel1 As ZLabel
    Friend WithEvents txtpath As TextBox
    Friend WithEvents txtname As TextBox
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents ZToolBar1 As ZToolBar
    Friend WithEvents btnadd As ToolStripButton
    Friend WithEvents btnedit As ToolStripButton
    Friend WithEvents ZBPreview As ToolStripButton
    Friend WithEvents ConverToBootstrap As ToolStripButton
    Friend WithEvents btnReplicate As ToolStripButton
    Friend WithEvents btndel As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btncond As ToolStripButton
    Friend WithEvents btnAttributeCondition As ToolStripButton
    Friend WithEvents BtnEditHtml As ToolStripButton
    Friend WithEvents btnOpenTestCases As ToolStripButton
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents btnupdateformsversion As ToolStripButton
    Friend WithEvents lblFormsVersion As ToolStripLabel
    Friend WithEvents btnExportFormsToBlob As ToolStripButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormsControl))
        Me.lstForms = New System.Windows.Forms.ListBox()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.chkUseBlob = New System.Windows.Forms.CheckBox()
        Me.chkUseRuleRights = New System.Windows.Forms.CheckBox()
        Me.Btnexplore = New Zamba.AppBlock.ZButton()
        Me.Panel3 = New Zamba.AppBlock.ZPanel()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Panel4 = New Zamba.AppBlock.ZPanel()
        Me.ZToolBar1 = New Zamba.AppBlock.ZToolBar()
        Me.btnadd = New System.Windows.Forms.ToolStripButton()
        Me.btnedit = New System.Windows.Forms.ToolStripButton()
        Me.ZBPreview = New System.Windows.Forms.ToolStripButton()
        Me.ConverToBootstrap = New System.Windows.Forms.ToolStripButton()
        Me.btnReplicate = New System.Windows.Forms.ToolStripButton()
        Me.btndel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btncond = New System.Windows.Forms.ToolStripButton()
        Me.btnAttributeCondition = New System.Windows.Forms.ToolStripButton()
        Me.BtnEditHtml = New System.Windows.Forms.ToolStripButton()
        Me.btnOpenTestCases = New System.Windows.Forms.ToolStripButton()
        Me.btnExportFormsToBlob = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ZLabel3 = New Zamba.AppBlock.ZLabel()
        Me.cbodoctype = New System.Windows.Forms.ComboBox()
        Me.ZLabel2 = New Zamba.AppBlock.ZLabel()
        Me.cbotype = New System.Windows.Forms.ComboBox()
        Me.ZLabel1 = New Zamba.AppBlock.ZLabel()
        Me.txtpath = New System.Windows.Forms.TextBox()
        Me.txtname = New System.Windows.Forms.TextBox()
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnupdateformsversion = New System.Windows.Forms.ToolStripButton()
        Me.lblFormsVersion = New System.Windows.Forms.ToolStripLabel()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.ZToolBar1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstForms
        '
        Me.lstForms.BackColor = System.Drawing.Color.White
        Me.lstForms.ContextMenu = Me.ContextMenu1
        Me.lstForms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstForms.ItemHeight = 16
        Me.lstForms.Location = New System.Drawing.Point(0, 0)
        Me.lstForms.Name = "lstForms"
        Me.lstForms.Size = New System.Drawing.Size(278, 251)
        Me.lstForms.TabIndex = 0
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem2, Me.MenuItem1})
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 0
        Me.MenuItem2.Text = "Guardar cambios en formulario"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 1
        Me.MenuItem1.Text = "Eliminar"
        '
        'chkUseBlob
        '
        Me.chkUseBlob.AutoSize = True
        Me.chkUseBlob.BackColor = System.Drawing.Color.Transparent
        Me.chkUseBlob.Location = New System.Drawing.Point(324, 145)
        Me.chkUseBlob.Name = "chkUseBlob"
        Me.chkUseBlob.Size = New System.Drawing.Size(245, 20)
        Me.chkUseBlob.TabIndex = 27
        Me.chkUseBlob.Text = "Almacen lógico en base de datos"
        Me.chkUseBlob.UseVisualStyleBackColor = False
        '
        'chkUseRuleRights
        '
        Me.chkUseRuleRights.AutoSize = True
        Me.chkUseRuleRights.BackColor = System.Drawing.Color.Transparent
        Me.chkUseRuleRights.Location = New System.Drawing.Point(7, 145)
        Me.chkUseRuleRights.Name = "chkUseRuleRights"
        Me.chkUseRuleRights.Size = New System.Drawing.Size(289, 20)
        Me.chkUseRuleRights.TabIndex = 23
        Me.chkUseRuleRights.Text = "Utilizar hablitación de botones por regla"
        Me.chkUseRuleRights.UseVisualStyleBackColor = False
        '
        'Btnexplore
        '
        Me.Btnexplore.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Btnexplore.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btnexplore.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btnexplore.ForeColor = System.Drawing.Color.White
        Me.Btnexplore.Location = New System.Drawing.Point(719, 62)
        Me.Btnexplore.Name = "Btnexplore"
        Me.Btnexplore.Size = New System.Drawing.Size(91, 23)
        Me.Btnexplore.TabIndex = 2
        Me.Btnexplore.Text = "Examinar"
        Me.Btnexplore.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(759, 251)
        Me.Panel3.TabIndex = 4
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.Panel4.Controls.Add(Me.ZToolBar1)
        Me.Panel4.Controls.Add(Me.ZLabel3)
        Me.Panel4.Controls.Add(Me.cbodoctype)
        Me.Panel4.Controls.Add(Me.ZLabel2)
        Me.Panel4.Controls.Add(Me.cbotype)
        Me.Panel4.Controls.Add(Me.chkUseBlob)
        Me.Panel4.Controls.Add(Me.ZLabel1)
        Me.Panel4.Controls.Add(Me.txtpath)
        Me.Panel4.Controls.Add(Me.txtname)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.Btnexplore)
        Me.Panel4.Controls.Add(Me.chkUseRuleRights)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1041, 169)
        Me.Panel4.TabIndex = 6
        '
        'ZToolBar1
        '
        Me.ZToolBar1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnadd, Me.btnedit, Me.ZBPreview, Me.ConverToBootstrap, Me.btnReplicate, Me.btndel, Me.ToolStripSeparator1, Me.btncond, Me.btnAttributeCondition, Me.BtnEditHtml, Me.btnOpenTestCases, Me.btnExportFormsToBlob, Me.ToolStripButton1, Me.btnupdateformsversion, Me.lblFormsVersion})
        Me.ZToolBar1.Location = New System.Drawing.Point(0, 0)
        Me.ZToolBar1.Name = "ZToolBar1"
        Me.ZToolBar1.Size = New System.Drawing.Size(1041, 25)
        Me.ZToolBar1.TabIndex = 29
        Me.ZToolBar1.Text = "ZToolBar1"
        '
        'btnadd
        '
        Me.btnadd.Image = Global.Zamba.Viewers.My.Resources.Resources.add2
        Me.btnadd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(69, 22)
        Me.btnadd.Text = "Agregar nuevo Formulario"
        '
        'btnedit
        '
        Me.btnedit.Image = Global.Zamba.Viewers.My.Resources.Resources.disk_blue
        Me.btnedit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(69, 22)
        Me.btnedit.Text = "Guardar cambios en formulario"
        '
        'ZBPreview
        '
        Me.ZBPreview.Image = Global.Zamba.Viewers.My.Resources.Resources.print_preview
        Me.ZBPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ZBPreview.Name = "ZBPreview"
        Me.ZBPreview.Size = New System.Drawing.Size(92, 22)
        Me.ZBPreview.Text = "Previsualizar"
        '
        'ConverToBootstrap
        '
        Me.ConverToBootstrap.Image = Global.Zamba.Viewers.My.Resources.Resources.document_exchange
        Me.ConverToBootstrap.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ConverToBootstrap.Name = "ConverToBootstrap"
        Me.ConverToBootstrap.Size = New System.Drawing.Size(139, 22)
        Me.ConverToBootstrap.Text = "Convertir a Bootstrap"
        '
        'btnReplicate
        '
        Me.btnReplicate.Image = Global.Zamba.Viewers.My.Resources.Resources.blue_documents
        Me.btnReplicate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReplicate.Name = "btnReplicate"
        Me.btnReplicate.Size = New System.Drawing.Size(62, 22)
        Me.btnReplicate.Text = "Copiar"
        '
        'btndel
        '
        Me.btndel.Image = Global.Zamba.Viewers.My.Resources.Resources.delete2
        Me.btndel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btndel.Name = "btndel"
        Me.btndel.Size = New System.Drawing.Size(70, 22)
        Me.btndel.Text = "Eliminar"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btncond
        '
        Me.btncond.Image = Global.Zamba.Viewers.My.Resources.Resources.things_icono
        Me.btncond.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btncond.Name = "btncond"
        Me.btncond.Size = New System.Drawing.Size(93, 22)
        Me.btncond.Text = "Condiciones"
        '
        'btnAttributeCondition
        '
        Me.btnAttributeCondition.Image = Global.Zamba.Viewers.My.Resources.Resources.Iconos_condiciones_Registro_y_validacion_color
        Me.btnAttributeCondition.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAttributeCondition.Name = "btnAttributeCondition"
        Me.btnAttributeCondition.Size = New System.Drawing.Size(159, 22)
        Me.btnAttributeCondition.Text = "Condiciones de atributos"
        '
        'BtnEditHtml
        '
        Me.BtnEditHtml.Image = Global.Zamba.Viewers.My.Resources.Resources.brush1
        Me.BtnEditHtml.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnEditHtml.Name = "BtnEditHtml"
        Me.BtnEditHtml.Size = New System.Drawing.Size(66, 22)
        Me.BtnEditHtml.Text = "Diseñar"
        '
        'btnOpenTestCases
        '
        Me.btnOpenTestCases.Image = Global.Zamba.Viewers.My.Resources.Resources.Graphicloads_100_Flat_Case
        Me.btnOpenTestCases.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOpenTestCases.Name = "btnOpenTestCases"
        Me.btnOpenTestCases.Size = New System.Drawing.Size(69, 22)
        Me.btnOpenTestCases.Text = "Pruebas"
        '
        'btnExportFormsToBlob
        '
        Me.btnExportFormsToBlob.Image = Global.Zamba.Viewers.My.Resources.Resources.gears_run
        Me.btnExportFormsToBlob.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExportFormsToBlob.Name = "btnExportFormsToBlob"
        Me.btnExportFormsToBlob.Size = New System.Drawing.Size(117, 22)
        Me.btnExportFormsToBlob.Text = "Convertir a BLOB"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = Global.Zamba.Viewers.My.Resources.Resources.document_exchange
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(165, 20)
        Me.ToolStripButton1.Text = "Convertir Todos Bootstrap"
        '
        'ZLabel3
        '
        Me.ZLabel3.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel3.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel3.FontSize = 9.75!
        Me.ZLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel3.Location = New System.Drawing.Point(7, 116)
        Me.ZLabel3.Name = "ZLabel3"
        Me.ZLabel3.Size = New System.Drawing.Size(91, 20)
        Me.ZLabel3.TabIndex = 12
        Me.ZLabel3.Text = "Entidad"
        Me.ZLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbodoctype
        '
        Me.cbodoctype.BackColor = System.Drawing.Color.White
        Me.cbodoctype.Location = New System.Drawing.Point(104, 115)
        Me.cbodoctype.Name = "cbodoctype"
        Me.cbodoctype.Size = New System.Drawing.Size(609, 24)
        Me.cbodoctype.TabIndex = 6
        '
        'ZLabel2
        '
        Me.ZLabel2.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel2.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel2.FontSize = 9.75!
        Me.ZLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel2.Location = New System.Drawing.Point(7, 89)
        Me.ZLabel2.Name = "ZLabel2"
        Me.ZLabel2.Size = New System.Drawing.Size(91, 20)
        Me.ZLabel2.TabIndex = 11
        Me.ZLabel2.Text = "Tipo"
        Me.ZLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbotype
        '
        Me.cbotype.BackColor = System.Drawing.Color.White
        Me.cbotype.Location = New System.Drawing.Point(104, 88)
        Me.cbotype.Name = "cbotype"
        Me.cbotype.Size = New System.Drawing.Size(609, 24)
        Me.cbotype.TabIndex = 4
        '
        'ZLabel1
        '
        Me.ZLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel1.FontSize = 9.75!
        Me.ZLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel1.Location = New System.Drawing.Point(7, 37)
        Me.ZLabel1.Name = "ZLabel1"
        Me.ZLabel1.Size = New System.Drawing.Size(91, 20)
        Me.ZLabel1.TabIndex = 10
        Me.ZLabel1.Text = "Nombre"
        Me.ZLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtpath
        '
        Me.txtpath.BackColor = System.Drawing.Color.White
        Me.txtpath.Location = New System.Drawing.Point(104, 62)
        Me.txtpath.Name = "txtpath"
        Me.txtpath.Size = New System.Drawing.Size(609, 23)
        Me.txtpath.TabIndex = 1
        '
        'txtname
        '
        Me.txtname.BackColor = System.Drawing.Color.White
        Me.txtname.Location = New System.Drawing.Point(104, 36)
        Me.txtname.Name = "txtname"
        Me.txtname.Size = New System.Drawing.Size(609, 23)
        Me.txtname.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label1.FontSize = 9.75!
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(7, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Ubicación"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 169)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lstForms)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel3)
        Me.SplitContainer1.Size = New System.Drawing.Size(1041, 251)
        Me.SplitContainer1.SplitterDistance = 278
        Me.SplitContainer1.TabIndex = 7
        '
        'btnupdateformsversion
        '
        Me.btnupdateformsversion.Image = CType(resources.GetObject("btnupdateformsversion.Image"), System.Drawing.Image)
        Me.btnupdateformsversion.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnupdateformsversion.Name = "btnupdateformsversion"
        Me.btnupdateformsversion.Size = New System.Drawing.Size(202, 20)
        Me.btnupdateformsversion.Text = "Actualizar Version de Formularios"
        '
        'lblFormsVersion
        '
        Me.lblFormsVersion.Name = "lblFormsVersion"
        Me.lblFormsVersion.Size = New System.Drawing.Size(0, 0)
        '
        'FormsControl
        '
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel4)
        Me.Name = "FormsControl"
        Me.Size = New System.Drawing.Size(1041, 420)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ZToolBar1.ResumeLayout(False)
        Me.ZToolBar1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim FormBrowser As FormBrowser
    Dim selectedIndex As Int64
    Private _formsComparer As New FormComparer()

    Private RadGrid As New Telerik.WinControls.UI.RadGridView

#Region "Load"
    Private Sub FormsControl_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        LoadFormBrowser()
        LoadDocTypes()
        LoadFormTypes()
        LoadExistingForms()

        Try
            Dim ActualFormVersion As Int32 = ZOptBusiness.GetValueOrDefault("FormsVersion", 1)
            Me.lblFormsVersion.Text = "Version Actual: " & ActualFormVersion
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LoadFormBrowser()
        Try
            If FormBrowser Is Nothing Then
                FormBrowser = New FormBrowser
            End If
            FormBrowser.Dock = DockStyle.Fill
            Panel3.Controls.Add(FormBrowser)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Cargo los tipos de documento en el combo
    ''' </summary>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Marcelo]	13/08/2009	Modified
    ''' </history>
    ''' <remarks></remarks>
    Private Sub LoadDocTypes()
        Try
            cbodoctype.DataSource = DocTypesBusiness.GetDocTypesArrayList(True)
            cbodoctype.DisplayMember = "Name"
            cbodoctype.ValueMember = "Id"
            cbodoctype.SelectedIndex = 0
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LoadFormTypes()
        Try
            cbotype.Items.Add(FormTypes.Search)
            cbotype.Items.Add(FormTypes.Edit)
            cbotype.Items.Add(FormTypes.Show)
            cbotype.Items.Add(FormTypes.WorkFlow)
            cbotype.Items.Add(FormTypes.Insert)
            cbotype.Items.Add(FormTypes.WebInsert)
            cbotype.Items.Add(FormTypes.WebSearch)
            cbotype.Items.Add(FormTypes.WebEdit)
            cbotype.Items.Add(FormTypes.WebShow)
            cbotype.Items.Add(FormTypes.WebWorkFlow)
            cbotype.Items.Add(FormTypes.WebReport)

            cbodoctype.Text = FormTypes.Show
            cbodoctype.SelectedIndex = 2
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Dim ZWebforms As List(Of ZwebForm)

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Recupera todos los formularios
    ''' </summary>
    ''' <param name="load"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	14/08/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub LoadExistingForms(Optional ByVal load As Boolean = True)
        Try


            ZWebforms = FormBusiness.GetForms(False)

            For Each Item As ZwebForm In ZWebforms

                Select Case Item.Type
                    Case FormTypes.All
                        Item.Name += " - Todo"
                    Case FormTypes.Edit
                        Item.Name += " - Editar"
                    Case FormTypes.Insert
                        Item.Name += " - Insertar"
                    Case FormTypes.Search
                        Item.Name += " - Busqueda"
                    Case FormTypes.Show
                        Item.Name += " - Visualizar"
                    Case FormTypes.WorkFlow
                        Item.Name += " - Workflow"
                    Case FormTypes.WebInsert
                        Item.Name += " - InsertarWeb"
                    Case FormTypes.WebEdit
                        Item.Name += " - EditarWeb"
                    Case FormTypes.WebSearch
                        Item.Name += " - BusquedaWeb"
                    Case FormTypes.WebShow
                        Item.Name += " - VisualizarWeb"
                    Case FormTypes.WebWorkFlow
                        Item.Name += " - WorkFlowWeb"
                    Case FormTypes.WebReport
                        Item.Name += " - ReporteWeb"
                End Select

            Next

            'ZWebforms.Sort(_formsComparer)
            lstForms.DataSource = Nothing
            lstForms.Items.Clear()
            lstForms.DataSource = ZWebforms
            lstForms.ValueMember = "ID"
            lstForms.DisplayMember = "ID" & "Name"
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    'Private Sub AddHandlers()
    '    RemoveHandler lstForms.SelectedIndexChanged, AddressOf ListBox1_SelectedIndexChanged
    '    AddHandler lstForms.SelectedIndexChanged, AddressOf ListBox1_SelectedIndexChanged
    'End Sub
#End Region

#Region "Select"

    Dim cancelSelectEvent As Boolean

    ''' <summary>
    ''' Evento que se ejecuta al seleccionar un formulario dinámico de la lista de formularios dinámicos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	03/03/2009	Modified
    '''     [Gaston]	06/05/2009	Modified    Llamada al método "updateFormData"
    ''' </history>
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstForms.SelectedIndexChanged

        If Not cancelSelectEvent Then

            Try
                UpdateFormData()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

        End If

    End Sub

#End Region

#Region "Add"

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnadd.Click
        Add()
    End Sub

    ''' <summary>
    ''' Método que sirve para agregar un formulario dinámico sin condiciones
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	03/03/2009	Modified    Se obtiene un id para el formulario dinámico que se quiere crear provisto por ZWEBFORM. A ese id se
    '''                                         le pasa como parámetro al contructor de frmAbmDynamicForms y al método "AddZWebForm"
    '''     [Gaston]    05/05/2009  Modified    
    '''     [Tomás]     06/05/2009  Modified    Se modifica la inserción de formularios para que cuando se quiera insertar un documento de
    '''                                         Insert, Update o Show se agreguen los otros dos. Ej: Agrego uno de Insert y se agrega 
    '''                                         automáticamente uno de Update y de Show.
    '''[Sebastian] 05-06-2009 Modified se realizaron casteos para salvar warnings
    ''' </history>
    Private Sub Add()

        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se va a agregar un Formulario nuevo.")
        Try
            ErrorProvider1.Clear()
            ' [Tomas]   25/02/09    Compruebo que se haya ingresado un path de formulario,
            '                       en caso no se haya ingresado pregunto si quiere crear un
            '                       form virtual.
            If (txtpath.Text.Trim() <> "") Then

                If (IsValidForm("add")) Then
                    AddZWebForm(ToolsBusiness.GetNewID(IdTypes.ZWEBFORM), txtname.Text.Trim, String.Empty, cbotype.SelectedItem, txtpath.Text.Trim, DirectCast(cbodoctype.SelectedItem, DocType).ID, Nothing)

                End If

            Else

                If (MessageBox.Show("El path del formulario no fue ingresado," & vbCrLf &
                                   "¿Desea crear un formulario dinámico?", "Confirmar acción",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then

                    If (IsValidVirtualForm("add")) Then

                        'Muestro el formulario para agregar
                        Dim doctypeid As Int64 = CLng(cbodoctype.SelectedValue())
                        Dim state As New DynamicFormState(doctypeid)

                        state.FormName = txtname.Text
                        state.DoctypeName = cbodoctype.Text

                        Dim frmAddForm As New frmAbmDynamicForms(state)
                        frmAddForm.ShowDialog()

                        'Realizo los cambios en la base
                        If (state.IsFinish = True) Then
                            FormBusiness.generateDynamicFormId(state)

                            'Realizo los cambios en la base
                            If (state.IsFinish = True) Then

                                'Si el formulario es de tipo Edit, Show o Insert crea los 3
                                'En caso de no serlo crea solamente ese que se eligio
                                'Ej: Si el formulario es Edit se creara una copia de Insert y de Show
                                Dim formType As String = cbotype.Text
                                If String.Compare(formType, FormTypes.Show.ToString) = 0 OrElse
                                String.Compare(formType, FormTypes.Insert.ToString) = 0 OrElse
                                String.Compare(formType, FormTypes.Edit.ToString) = 0 Then
                                    'Comprueba si existe el insert
                                    If Not DoesNameFormExists(txtname.Text & " - " & GetSpanishNameType(FormTypes.Show.ToString)) Then
                                        AddZWebForm(state.Formid, state.FormName, String.Empty, FormTypes.Show, String.Empty, DirectCast(cbodoctype.SelectedItem, DocType).ID, state)
                                    End If
                                    FormBusiness.generateDynamicFormId(state)
                                    'Comprueba si existe el show
                                    If Not DoesNameFormExists(txtname.Text & " - " & GetSpanishNameType(FormTypes.Insert.ToString)) Then
                                        AddZWebForm(state.Formid, state.FormName, String.Empty, FormTypes.Insert, String.Empty, DirectCast(cbodoctype.SelectedItem, DocType).ID, state)
                                    End If
                                    FormBusiness.generateDynamicFormId(state)
                                    'Comprueba si existe el edit
                                    If Not DoesNameFormExists(txtname.Text & " - " & GetSpanishNameType(FormTypes.Edit.ToString)) Then
                                        AddZWebForm(state.Formid, state.FormName, String.Empty, FormTypes.Edit, String.Empty, DirectCast(cbodoctype.SelectedItem, DocType).ID, state)
                                    End If
                                Else
                                    AddZWebForm(state.Formid, state.FormName, String.Empty, cbotype.SelectedItem, String.Empty, DirectCast(cbodoctype.SelectedItem, DocType).ID, state)
                                End If

                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Método que sirve para crear y agregar una instancia de formulario dinámico a la base de datos
    ''' </summary>
    ''' <param name="formId">Id del formulario</param>
    ''' <param name="formname">Nombre del formulario</param>
    ''' <param name="description">Descripción del formulario</param>
    ''' <param name="typestr">Tipo de formulario (Show, Edit, etc) </param>
    ''' <param name="filepath">Camino al formulario</param>
    ''' <param name="doctypeid">Entidad al que pertenece el formulario</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas 03/03/09]    Modified    Al agregar el formulario selecciona el item en la lista
    ''' 	[Gaston]	  29/04/2009	Modified    Corrección para el tipo de formulario
    '''     [Gaston]	  05/05/2009	Modified  El formulario se inserta en la tabla ZFrms y por último en las tablas relacionadas con formularios
    '''                                           dinámicos. Esto se hizo así debido a que se agregaron las relaciones entre las tablas que tienen 
    '''                                           que ver con formularios dinámicos
    ''' [Sebastian] 05-06-2009 Modified se realizaron casteo para salvar los warnings
    ''' </history>
    ''' 
    Private Sub AddZWebForm(ByVal formId As Int64, ByVal formName As String, ByVal description As String, ByVal type As FormTypes, ByVal filepath As String, ByVal doctypeid As Int64, ByVal state As DynamicFormState)
        ' Dim frmProjectQuestion As New FrmProjectAssignQuestion(formId, ObjectTypes.FormulariosElectronicos, formName, "formulario")
        Dim firstTry = True
        Dim formBusinessExt As New FormBusinessExt


        Try
            'If frmProjectQuestion.ShowDialog() = DialogResult.OK Then
            ' Se guardan los datos de dicho formulario en la tabla ZFrms
            Dim zwf As New ZwebForm(formId, formName, description, type, filepath, Int32.Parse(doctypeid.ToString), chkUseRuleRights.Checked, Now)
            zwf.UseBlob = chkUseBlob.Checked
            formBusinessExt.InsertForm(zwf)

            ' Si state no es nothing entonces significa que se está agregando un formulario dinámico
            If (Not IsNothing(state)) Then
                ' Se guardan los datos del formulario dinámico en las tablas relacionadas con formularios dinámicos
                FormBusiness.SaveDynamicForm(state)
            End If

            ZWebforms.Add(zwf)
            ' If Not IsNothing (_formsComparer) Then
            '    ZWebforms.Sort(_formsComparer)
            ' End If
            cancelSelectEvent = True
            LoadExistingForms(False)
            cancelSelectEvent = False

            'Tomas 03/03/09:    Muestra el item recién agregado
            ' [Gaston]  06/05/2009 Int64 a Int32
            lstForms.SelectedValue = Int32.Parse(formId.ToString)
            ' End If
        Finally
            ' If Not IsNothing(frmProjectQuestion) Then
            '     frmProjectQuestion.Dispose()
            '     frmProjectQuestion = Nothing
            ' End If
            'formBusinessExt = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' se le agrego el parametro para saber si se esta editando o no porque no dejaba editar ya que siempre
    ''' comprobaba el nombre en el txtname y tiraba error porque le nombre ya existe [sebastian 16-03-2009]
    ''' </summary>
    ''' <param name="AddOrEdit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsValidForm(ByVal AddOrEdit As String) As Boolean
        Try

            If File.Exists(txtpath.Text.Trim) = False Then
                If MessageBox.Show("La ruta del formulario es inexistente. ¿Desea continuar con la creacion?", "Agregar Formulario", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    Return False
                End If
            End If
            If txtname.Text.Trim.Length = 0 Then
                ErrorProvider1.SetError(txtname, "Debe especificar un nombre valido")
                Return False
            End If
            If DoesNameFormExists(txtname.Text & " - " & GetSpanishNameType(cbotype.Text)) And String.Compare(AddOrEdit.ToLower, "add") = 0 Then
                ErrorProvider1.SetError(txtname, "El formulario '" & txtname.Text & " - " &
                                           cbotype.Text & "' ya existe." & vbCrLf &
                                           "Elija otro nombre para continuar.")
                txtname.Clear()
                txtname.Focus()
                Return False
            End If
            If cbotype.SelectedIndex = -1 Then
                ErrorProvider1.SetError(cbotype, "Debe especificar un tipo de formulario valido")
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' [Tomas] 25/02/09    Valida los datos necesarios para la creación de un formulario virtual.
    ''' <summary>
    ''' se le agrego el parametro para saber si se esta editando o no porque no dejaba editar ya que siempre
    ''' comprobaba el nombre en el txtname y tiraba error porque el nombre ya existe [sebastian 16-03-2009]
    ''' </summary>
    ''' <param name="AddOrEdit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsValidVirtualForm(ByVal AddOrEdit As String) As Boolean
        Try
            If txtname.Text.Trim.Length = 0 Then
                ErrorProvider1.SetError(txtname, "Debe especificar un nombre valido")
                Return False
            End If
            If cbotype.SelectedIndex = -1 Then
                ErrorProvider1.SetError(cbotype, "Debe especificar un tipo de formulario valido")
                Return False
            End If

            If DoesNameFormExists(txtname.Text & " - " & GetSpanishNameType(cbotype.Text)) And String.Compare(AddOrEdit.ToLower, "add") = 0 Then
                ErrorProvider1.SetError(txtname, "El formulario '" & txtname.Text & " - " &
                                           cbotype.Text & "' ya existe." & vbCrLf &
                                           "Elija otro nombre para continuar.")
                txtname.Clear()
                txtname.Focus()
                Return False
            End If
            If cbodoctype.SelectedIndex = -1 Then
                ErrorProvider1.SetError(cbodoctype, "Debe especificar un entidad valido")
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Conditions"

    Private Sub btncond_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btncond.Click, btncond.Click
        AddConditions()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón "Condiciones"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas 02/03/09]    Created     Agrega un formulario a la tabla ZfrmDesc
    '''     [Gaston]	03/03/2009	Modified  Se obtiene un id para el formulario dinámico que se quiere crear provisto por ZWEBFORM. A ese id se
    '''                                       le pasa como parámetro al contructor de frmAbmZfrmDesc y al método "AddZWebForm"
    '''     [Gaston]    03/04/2009  Modified  Validación de formulario dinámico
    '''     [Gaston]    06/04/2009  Modified
    '''     [Gaston]    05/05/2009  Modified    Se agregan condiciones para los formularios normales (formularios que se almacenan en el servidor)
    ''' </history>
    Private Sub AddConditions()

        Try
            ErrorProvider1.Clear()
            If (lstForms.SelectedIndex <> -1) Then

                If (IsValidVirtualForm("edit")) Then

                    Dim doctypeid As Int64 = CLng(cbodoctype.SelectedValue())
                    Dim formid As Int64 = CLng(lstForms.SelectedValue)

                    Dim state As New DynamicFormState(doctypeid, True, formid)

                    FormBusiness.GetDynamicFormState(state)
                    state.FormName = txtname.Text
                    state.DoctypeName = cbodoctype.Text

                    Dim frmAddFrmConditions As New frmAbmZfrmDesc(state)
                    frmAddFrmConditions.Tag = "openFromBtnConditions"
                    frmAddFrmConditions.ShowDialog()

                    If (state.IsFinish) Then

                        ' Si el path está vacío entonces se actualizan las condiciones de un formulario dinámico
                        If (String.IsNullOrEmpty(CType(lstForms.SelectedItem, ZwebForm).Path)) Then
                            FormBusiness.UpdateDynamicForm(state)
                            ' De lo contrario, se actualizan las condiciones de un formulario normal (un formulario almacenado en el servidor)
                        Else
                            FormBusiness.updateNormalForm(state)
                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

#Region "Attributes Conditions"
    Private Sub btnAttributeCondition_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAttributeCondition.Click

        If (lstForms.SelectedIndex <> -1) Then
            Dim doctypeid As Int64 = CLng(cbodoctype.SelectedValue())
            Dim formid As Int64 = CLng(lstForms.SelectedValue)

            Dim frmConditions As New frmAttributeCondition(formid, doctypeid)
            frmConditions.ShowDialog()
        Else
            MessageBox.Show("Debe seleccionar un formulario", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
#End Region

#Region "Delete"

    Private Sub btndel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btndel.Click
        Del_Form()
    End Sub

    ''' <summary>
    ''' Método que sirve para eliminar el formulario seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Sebastian] 05-06-2009 Modified se realizaron cast para salver warnings
    '''     [Gaston]	29/06/2009  Modified     Inserción del mensaje que permite eliminar o no el formulario seleccionado
    ''' </history>
    Private Sub Del_Form()

        Try

            ErrorProvider1.Clear()

            If (lstForms.SelectedIndex <> -1) Then

                If (MessageBox.Show("¿Desea eliminar el formulario seleccionado?", "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes) Then

                    Dim Form As ZwebForm = DirectCast(ZWebforms(lstForms.SelectedIndex), ZwebForm)
                    FormBusiness.DeleteForm(Form)

                    Dim formname As String = Form.Name
                    Dim removeindex As Int32 = formname.LastIndexOf("-")
                    formname = formname.Remove(removeindex - 1, formname.Length - removeindex + 1)
                    Dim filepath As String = Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\" & formname & ".html"

                    cancelSelectEvent = True
                    ZWebforms.Remove(Form)
                    LoadExistingForms(False)
                    cancelSelectEvent = False

                    'si es un formulario dinamico y esta guardado en temp, se borra.
                    If (Form.Path = String.Empty) Then
                        If File.Exists(filepath) Then
                            File.Delete(filepath)
                        End If
                    End If

                    If (lstForms.Items.Count > 1) Then
                        lstForms.SelectedIndex = Int32.Parse((selectedIndex - 1).ToString)
                    Else
                        lstForms.SelectedIndex = 0
                    End If

                    cancelSelectEvent = False

                    If (IsNothing(lstForms.SelectedItem)) Then
                        lstForms.SelectedItem = lstForms.Items(0)
                    End If

                    'updateFormData()

                End If

            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

#Region "Edit"

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnedit.Click
        Edit_Form()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón "Editar/Modificar"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	06/04/2009  Modified     
    ''' </history>
    Private Sub Edit_Form()
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se va a actualizar un formulario")
        Try
            ErrorProvider1.Clear()
            ' [Tomas]   25/02/09    Compruebo que se haya ingresado un path de formulario,
            '                       en caso no se haya ingresado edito un form virtual.
            If (lstForms.SelectedIndex <> -1) Then

                If (txtpath.Text.Trim <> "") Then

                    If (IsValidForm("edit")) Then
                        UpdateZWebForm()
                        '[sebastian 06-03-09] Re load the list of forms to see the last changes
                        LoadExistingForms(True)

                    End If

                Else

                    If (IsValidVirtualForm("edit")) Then

                        'Muestro el fomrulario para editar
                        Dim doctypeid As Int64 = CLng(cbodoctype.SelectedValue())
                        Dim formid As Int64 = CLng(lstForms.SelectedValue)

                        Dim state As New DynamicFormState(doctypeid, True, formid)

                        FormBusiness.GetDynamicFormState(state)
                        state.FormName = txtname.Text
                        state.DoctypeName = cbodoctype.Text


                        Dim _form As New frmAbmDynamicForms(state)

                        _form.ShowDialog()

                        'Realizo los cambios en la base
                        If (state.IsFinish) Then

                            FormBusiness.UpdateDynamicForm(state)
                            UpdateZWebForm()
                            '(pablo) agrego el log
                            UserBusiness.Rights.SaveAction(CLng(cbodoctype.SelectedValue()), ObjectTypes.FormulariosElectronicos, RightsType.Edit, "Se Agrego Formulario: " + txtname.Text)
                            cancelSelectEvent = True
                            LoadExistingForms(False)
                            cancelSelectEvent = False

                            lstForms.SelectedValue = Int32.Parse(formid.ToString)

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' [Tomas 02/03/09]    Modified    Realiza un update del formulario seleccionado
    '''                     Código tomado de otro método.
    ''' [Tomas 03/03/09]    Modified    Al realizar los cambios selecciona el item modificado en la lista
    '''[Sebastian] 05-06-2009 Modified se realizaron cast para salvar warnings
    Private Sub UpdateZWebForm()
        Dim selectedIndex As Int32 = CInt(lstForms.SelectedIndex)
        Dim form As ZwebForm = DirectCast(ZWebforms(lstForms.SelectedIndex), ZwebForm)
        Dim formBusinessExt As FormBusinessExt
        Try
            form.Name = txtname.Text.Trim
            form.Description = String.Empty
            form.Type = DirectCast(cbotype.SelectedItem, FormTypes)
            form.Path = txtpath.Text.Trim
            form.DocTypeId = Int32.Parse(DirectCast(cbodoctype.SelectedItem, DocType).ID.ToString)
            form.useRuleRights = chkUseRuleRights.Checked
            form.UseBlob = chkUseBlob.Checked
            formBusinessExt = New FormBusinessExt()
            formBusinessExt.UpdateForm(form)

            'Tomas 03/03/09:    Muestra el item recién agregado
            lstForms.SelectedIndex = selectedIndex
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            formBusinessExt = Nothing
        End Try
    End Sub

#End Region

#Region "Eventos"
    Private Sub Btnexplore_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Btnexplore.Click
        Try
            Dim Dlg As New OpenFileDialog
            Dlg.ShowDialog()
            txtpath.Text = Dlg.FileName
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            FormBrowser.Navigate(txtpath.Text.Trim)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem1.Click
        Del_Form()
    End Sub
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuItem2.Click
        Edit_Form()
    End Sub


#End Region

    ''' <summary>
    ''' Compara el nombre de un formulario con los items del ListBox1
    ''' </summary>
    ''' <history>
    ''' [Tomas 11/03/09]    Created
    ''' </history>
    Private Function DoesNameFormExists(ByVal formName As String) As Boolean

        ' Comparo si el string ingresado es igual al nombre del formulario del ListBox1
        For Each frmName As Zamba.Core.ZwebForm In lstForms.Items
            If String.Compare(formName, frmName.Name) = 0 Then
                Return True
            End If
        Next
        Return False

    End Function

    ''' <summary>
    ''' Convierte el valor del cbotype (Edit,Insert,Search,Show,WorkFlow)
    ''' al español, tal como se muestra en el ListBox1. En caso de no 
    ''' encontrar ninguno devuelve el mismo valor.
    ''' </summary>
    ''' <history>
    ''' [Tomas 11/03/09]    Created
    ''' </history>
    Private Function GetSpanishNameType(ByVal nameType As String) As String

        Select Case nameType.ToLower()
            Case "edit"
                Return "Editar"
            Case "insert"
                Return "Insertar"
            Case "search"
                Return "Busqueda"
            Case "show"
                Return "Visualizar"
            Case "workflow"
                Return "Workflow"
            Case "WebInsert"
                Return "InsertarWeb"
            Case "WebSearch"
                Return "BusquedaWeb"
            Case "WebEdit"
                Return "WebEdit"
            Case "WebShow"
                Return "WebShow"
            Case "WebWorkFlow"
                Return "WebWorkFlow"
            Case Else
                Return nameType
        End Select

    End Function

    ''' <summary>
    ''' [Sebastian] 05/06/2009 Modified se realizo cast para salvar warnings
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnEditHtml_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnEditHtml.Click
        Try
            If lstForms.SelectedIndex <> -1 Then
                If IsValidForm("edit") Then
                    Dim SelectedForm As ZwebForm = DirectCast(ZWebforms(lstForms.SelectedIndex), ZwebForm)

                    Try

                        Dim UseWebHtmlEditor As Boolean
                        Boolean.TryParse(ZOptBusiness.GetValue("WebHtmlEditorEnabled"), UseWebHtmlEditor)

                        If (UseWebHtmlEditor) Then
                            'Dim Frm As New Form
                            'Dim WB As New WebBrowser
                            ' WB.Dock = DockStyle.Fill
                            'Frm.Controls.Add(WB)
                            'Frm.Name = "Editor de Formularios"
                            'Frm.Text = "Editor de Formularios"
                            'Frm.WindowState = FormWindowState.Maximized

                            Dim Url As String
                            Url = ZOptBusiness.GetValue("WebHtmlEditorUrl")
                            If Not Url Is Nothing AndAlso Url.Length > 0 Then
                                'Dim IE As Object
                                'IE = CreateObject("internetexplorer.application")
                                'IE.visible = True
                                Url += "?FId=" & SelectedForm.ID & "&EId=" & SelectedForm.DocTypeId
                                'IE.navigate(Url)
                                Dim IE As New System.Diagnostics.Process
                                IE.Start(Url)
                            Else
                                MsgBox("El Editor Html no se encuentra habilitado correctamente", MsgBoxStyle.Exclamation, "Edicion de Formularios")
                            End If
                        Else
                            Dim docType As DocType = DocTypesBusiness.GetDocType(DirectCast(cbodoctype.SelectedItem, IDocType).ID, False)
                            docType.Indexs = ZCore.FilterIndex(Int32.Parse(docType.ID.ToString))

                            'Dim HtmlEditor As New Controller.FormsEditor(SelectedForm, docType)
                            'RemoveHandler BtnEditHtml.Click, AddressOf BtnEditHtml_Click
                            'HtmlEditor.Show()
                            'AddHandler BtnEditHtml.Click, AddressOf BtnEditHtml_Click
                        End If

                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try

                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Comparador de formularios electronicos que ordena por nombre
    ''' </summary>
    ''' <remarks></remarks>
    Private Class FormComparer
        Implements System.Collections.IComparer

        Private _first As ZwebForm
        Private _second As ZwebForm

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare

            _first = DirectCast(x, Zamba.Core.ZwebForm)
            _second = DirectCast(y, Zamba.Core.ZwebForm)
            Return (String.Compare(_first.Name, _second.Name))

        End Function

    End Class

    ''' <summary>
    ''' [Sebastian ] Modified 05/06/2009 se realizo cast para salvar warnings
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNewFormHtml_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            Try
                Dim SelectedForm As ZwebForm = DirectCast(ZWebforms(lstForms.SelectedIndex), ZwebForm)

                Dim UseWebHtmlEditor As Boolean
                Boolean.TryParse(ZOptBusiness.GetValue("WebHtmlEditorEnabled"), UseWebHtmlEditor)

                If (UseWebHtmlEditor) Then
                    'Dim Frm As New Form
                    'Dim WB As New WebBrowser
                    'WB.Dock = DockStyle.Fill
                    ' Frm.Controls.Add(WB)
                    'Frm.Name = "Editor de Formularios"
                    'Frm.Text = "Editor de Formularios"
                    'Frm.WindowState = FormWindowState.Maximized
                    'Frm.Show()

                    Dim Url As String
                    Url = ZOptBusiness.GetValue("WebHtmlEditorUrl")
                    If Not Url Is Nothing AndAlso Url.Length > 0 Then
                        'Dim IE As Object
                        'IE = CreateObject("internetexplorer.application")
                        'IE.visible = True
                        Url += "?FId=" & -1 & "&EId=" & SelectedForm.DocTypeId
                        Dim IE As New System.Diagnostics.Process
                        IE.Start(Url)

                        'Shell("C:\Program Files\Internet Explorer\IEXPLORE.EXE " + Url)
                        'WB.Navigate(Url)
                    Else
                        MsgBox("El Editor Html no se encuentra habilitado correctamente", MsgBoxStyle.Exclamation, "Edicion de Formularios")
                    End If
                Else
                    Dim docType As DocType = DocTypesBusiness.GetDocType(DirectCast(cbodoctype.SelectedItem, IDocType).ID, False)
                    docType.Indexs = ZCore.FilterIndex(Int32.Parse(docType.ID.ToString))

                    'Dim HtmlEditor As New Controller.FormsEditor(docType)
                    'RemoveHandler btnNewFormHtml.Click, AddressOf btnNewFormHtml_Click
                    'HtmlEditor.Show()
                    'AddHandler btnNewFormHtml.Click, AddressOf btnNewFormHtml_Click

                    'txtpath.Text = HtmlEditor.FilePath
                End If


            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' evento click del boton replicar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     dalbarellos 17.04.2009
    '''     [Gaston]	06/05/2009	Modified    Adaptado para las modificaciones que se hicieron debido a la inserción de relaciones entre las
    '''                                         tablas relacionadas con formularios dinámicos
    ''' [Sebastian] 05/06/2009 Modified se realizaron cast para salvar warnings
    '''</history>
    Private Sub btnReplicate_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnReplicate.Click

        ErrorProvider1.Clear()

        Dim selectedformid As Int64 = Int32.Parse(lstForms.SelectedValue.ToString)
        Dim form As ZwebForm = FormBusiness.GetForm(selectedformid)

        Dim types As New List(Of String)
        'Me.GetFormTypes(types)
        FormsControl.GetFormTypes(types)

        Dim frmrequesttype As New frmRequestFormType(types, form.Type.ToString)
        If frmrequesttype.ShowDialog() = DialogResult.OK Then

            Dim newformid As Int64 = ToolsBusiness.GetNewID(IdTypes.ZWEBFORM)

            If (form.Path = String.Empty) Then

                Dim state As New DynamicFormState(form.DocTypeId, True, selectedformid)
                FormBusiness.GetDynamicFormState(state)
                state.UpdateFormId(newformid)
                AddZWebForm(state.Formid, form.Name, form.Description, frmrequesttype.SelectedType, form.Path, form.DocTypeId, state)
            Else
                AddZWebForm(newformid, form.Name, form.Description, frmrequesttype.SelectedType, form.Path, form.DocTypeId, Nothing)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Carga una coleccion con los tipos de forms
    ''' </summary>
    ''' <param name="types"></param>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 17.04.2009</history>
    Private Shared Sub GetFormTypes(ByRef types As List(Of String))
        types.Add(FormTypes.Search.ToString)
        types.Add(FormTypes.Edit.ToString)
        types.Add(FormTypes.Show.ToString)
        types.Add(FormTypes.WorkFlow.ToString)
        types.Add(FormTypes.Insert.ToString)
        types.Add(FormTypes.All.ToString)
        types.Add(FormTypes.WebInsert.ToString)
        types.Add(FormTypes.WebSearch.ToString)
        types.Add(FormTypes.WebEdit.ToString)
        types.Add(FormTypes.WebShow.ToString)
        types.Add(FormTypes.WebWorkFlow.ToString)
        types.Add(FormTypes.WebReport.ToString)
    End Sub

    ''' <summary>
    ''' Método que sirve para actualizar los datos presentes en el formulario dinámico seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	06/05/2009	Created     Código original del evento "ListBox1_SelectedIndexChanged"
    ''' [Sebastian] 05/06/2009 Modified se realizo cast para salvar warnings
    ''' </history>
    Private Sub UpdateFormData()

        If Not (IsNothing(lstForms.SelectedItem)) Then
            Dim form As ZwebForm = DirectCast(lstForms.SelectedItem, ZwebForm)

            'guarda el selectedIndex
            selectedIndex = lstForms.SelectedIndex

            txtpath.Text = form.Path
            'LE ASIGNA EL NOMBRE DEL FORMULARIO AL TEXTBOX DE NOMBRE
            txtname.Text = form.Name
            'LE SACO EL TIPO QUE PREVIAMENTE SE HABIA CONCATENADO AL NOMBRE
            txtname.Text = txtname.Text.Remove(txtname.Text.LastIndexOf("-") - 1, (txtname.Text.Length - txtname.Text.LastIndexOf("-")) + 1)

            '' Después de crear el formulario el combobox se modifica, y no deberí
            cbotype.SelectedItem = form.Type

            chkUseRuleRights.Checked = form.useRuleRights

            chkUseBlob.Checked = form.UseBlob

            For i As Int32 = 0 To cbodoctype.Items.Count - 1
                cbodoctype.SelectedIndex = i
                If Int32.Parse(cbodoctype.SelectedValue.ToString) = form.DocTypeId Then Exit For
            Next
        End If


    End Sub

    Private Sub txtname_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtname.Leave
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtpath_Leave(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtpath.Leave
        ErrorProvider1.Clear()
    End Sub

    Private Sub ZBPreview_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ZBPreview.Click
        Try
            If chkUseBlob.Checked Then
                Dim formBusinessExt As New FormBusinessExt
                FormBrowser.AxWebBrowser1.Navigate(formBusinessExt.CopyBlobToTemp(lstForms.SelectedItem, True))
                formBusinessExt = Nothing
            Else
                PreviewPhysicalFile()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ocurrió un error al previsualizar el formulario. Verifique las exceptions.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PreviewPhysicalFile()
        FormBrowser.Navigate(txtpath.Text.Trim(), CType(lstForms.SelectedItem, ZwebForm).ID, txtname.Text)
    End Sub

    Private Sub btnOpenTestCases_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnOpenTestCases.Click
        Try
            Dim ztcApp As String = ZOptBusiness.GetValue("ZTCApplication")
            Dim ztcPath As String = System.Windows.Forms.Application.StartupPath & "\" & ztcApp
            Dim params As String = ObjectTypes.FormulariosElectronicos & " " & CLng(lstForms.SelectedValue) & " " & Membership.MembershipHelper.CurrentUser.Name & " " & Membership.MembershipHelper.CurrentUser.Password
            ZTrace.WriteLineIf(ZTrace.IsInfo, "ZTC: " & ztcPath)
            If File.Exists(ztcPath) Then
                System.Diagnostics.Process.Start(ztcPath, params)
                Dim form As WaitForm = New WaitForm
                form.Show()
            Else
                MessageBox.Show("No se ha encontrado el módulo de casos de prueba", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Error al abrir el módulo de casos de prueba", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExportFormsToBlob_Click(sender As System.Object, e As EventArgs) Handles btnExportFormsToBlob.Click
        If MessageBox.Show("Presione OK para confirmar la exportación de todos" & vbCrLf &
                           "los formularios al almacen lógico de base de datos." & vbCrLf &
                           "Los formularios ya exportados no serán procesados.",
                           "Confirmar acción", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then

            Dim lstExportErrors As New List(Of String)()
            Dim procesados As Boolean
            Dim formBusinessExt As New FormBusinessExt

            For Each form As ZwebForm In lstForms.Items
                If Not form.UseBlob AndAlso Not String.IsNullOrEmpty(form.Path) Then
                    Try
                        formBusinessExt.SetDigitalFile(form)
                        procesados = True
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                        lstExportErrors.Add(form.Name)
                    End Try
                End If
            Next

            If lstExportErrors.Count > 0 Then
                Dim frmErrors As New Form()
                frmErrors.Text = "Formularios no exportados. Verifique las excepciones para mayor información."
                frmErrors.StartPosition = FormStartPosition.CenterScreen
                frmErrors.ShowIcon = False
                frmErrors.Height = 400
                frmErrors.Width = 600

                Dim lstErrors As New ListBox()
                lstErrors.Dock = DockStyle.Fill
                lstErrors.DataSource = lstExportErrors
                frmErrors.Controls.Add(lstErrors)
                frmErrors.ShowDialog()
            Else
                If procesados Then
                    MessageBox.Show("Los formularios han sido exportados", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("No se han encontrado formularios a exportar", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' Nueva funcionalidad que permite la insercion de archivos adicionales de formularios.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAdditionalFiles_Click(sender As System.Object, e As EventArgs)
        Try
            Dim form As New PathForms.FrmFormAdditionalFiles
            form.ShowDialog()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub zRebuildForm_Click(sender As System.Object, e As EventArgs)
        Try
            Dim formBusinessExt As New FormBusinessExt
            formBusinessExt.SetFormToRebuild(DirectCast(lstForms.SelectedItem, IZwebForm).ID)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub zRebuildAllForms_Click(sender As System.Object, e As EventArgs)
        Try
            Dim formBusinessExt As New FormBusinessExt
            formBusinessExt.SetAllFormsToRebuild()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub lstforms_doubleClick(sender As System.Object, e As EventArgs) Handles lstForms.DoubleClick
        Try
            If Not IsNothing(lstForms.SelectedItem) Then
                PreviewPhysicalFile()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ConverToBootstrap_Click(sender As Object, e As EventArgs) Handles ConverToBootstrap.Click

        Dim pathFormularioTemporal As String = String.Empty
        Dim carpetaDeFormulariosVIejos As String = String.Empty
        Dim myForm As ZwebForm

        Try

            If MessageBox.Show(Me, "¿Desea transformar el formulario?", "Convertir a bootstrap", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                CreateAndCopyFiles(pathFormularioTemporal, carpetaDeFormulariosVIejos, myForm)

                Dim nuevoFormulario() As String = File.ReadAllLines(myForm.TempFullPath)

                If Not Directory.Exists(carpetaDeFormulariosVIejos) Then
                    Directory.CreateDirectory(carpetaDeFormulariosVIejos)
                End If


                If Not File.Exists(carpetaDeFormulariosVIejos & "\" & myForm.TempPathName) Then
                    File.Copy(myForm.TempFullPath, carpetaDeFormulariosVIejos & "\" & myForm.TempPathName, False)

                    ReplaceTablesAndTabbers(nuevoFormulario)

                    nuevoFormulario = ReplaceInputsAndTag(nuevoFormulario)

                    File.WriteAllLines(pathFormularioTemporal, nuevoFormulario)

                    nuevoFormulario = File.ReadAllLines(pathFormularioTemporal)

                    SetColumnsInNewFile(nuevoFormulario)

                    File.WriteAllLines(pathFormularioTemporal, nuevoFormulario)

                    nuevoFormulario = File.ReadAllLines(pathFormularioTemporal)

                    ChangeCantCol(nuevoFormulario)

                    SetGroupBtn(nuevoFormulario)

                    SetHeaderForm(nuevoFormulario)

                    File.WriteAllLines(myForm.TempFullPath, nuevoFormulario)


                    File.Delete(pathFormularioTemporal)

                    MessageBox.Show("Se realizaron los cambios al formulario.")
                Else
                    MessageBox.Show("El formulario ya a sido transformado anteriormente.")
                End If
            End If


        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            myForm = Nothing
        End Try
    End Sub

    Private Sub CreateAndCopyFiles(ByRef pathFormularioTemporal As String, ByRef carpetaDeFormulariosVIejos As String, ByRef myForm As ZwebForm)

        Dim directoryFilesBootstrap As DirectoryInfo = Nothing
        Dim carpetaTemp As DirectoryInfo = Nothing
        Dim nombreFormularioTemporal As String = String.Empty

        Try
            myForm = CType(lstForms.SelectedItem, ZwebForm)

            carpetaTemp = New DirectoryInfo(Membership.MembershipHelper.AppTempPath & "\temp\")

            directoryFilesBootstrap = New DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory & "Formularios\")

            nombreFormularioTemporal = myForm.TempPathName.Substring(0, myForm.TempPathName.Length - 5) & "Nuevo.html"

            pathFormularioTemporal = carpetaTemp.FullName & nombreFormularioTemporal

            carpetaDeFormulariosVIejos = carpetaTemp.FullName & "BackUp Formularios"

            DirectoryCopy(directoryFilesBootstrap.FullName, carpetaTemp.FullName, True)


        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CreateAndCopyAllFiles(ByRef pathFormularioTemporal As String, ByRef carpetaDeFormulariosVIejos As String, ByRef myForms As List(Of ZwebForm))

        Dim directoryFilesBootstrap As DirectoryInfo = Nothing
        Dim carpetaTemp As DirectoryInfo = Nothing
        Dim nombreFormularioTemporal As String = String.Empty

        Dim folderbrowser As New FolderBrowserDialog
        If folderbrowser.ShowDialog = DialogResult.OK Then
            Try
                For Each form As ZwebForm In lstForms.Items

                    carpetaTemp = New DirectoryInfo(folderbrowser.SelectedPath)

                    directoryFilesBootstrap = New DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory & "Formularios\")

                    nombreFormularioTemporal = form.TempPathName.Substring(0, form.TempPathName.Length - 5) & " Nuevo.html"

                    pathFormularioTemporal = carpetaTemp.FullName & nombreFormularioTemporal

                    carpetaDeFormulariosVIejos = carpetaTemp.FullName & "BackUp Formularios"

                    DirectoryCopy(directoryFilesBootstrap.FullName, carpetaTemp.FullName, True)

                    myForms.Add(form)

                Next
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
    End Sub

    Private Sub DirectoryCopy(currentDirectory As String, dirToCopy As String, copySubDirs As Boolean)

        Try
            Dim dir As DirectoryInfo = New DirectoryInfo(currentDirectory)


            Dim dirs As DirectoryInfo() = dir.GetDirectories()

            ' If the destination directory doesn't exist, create it.
            If Not Directory.Exists(dirToCopy) Then
                Directory.CreateDirectory(dirToCopy)
            End If
            ' Get the files in the directory and copy them to the new location.

            Dim files As FileInfo() = dir.GetFiles()

            For Each Filee As FileInfo In files
                Dim temppath As String = Path.Combine(dirToCopy, Filee.Name)
                Filee.CopyTo(temppath, True)
            Next

            ' If copying subdirectories, copy them and their contents to new location.
            If copySubDirs Then
                For Each subdir As DirectoryInfo In dirs
                    Dim temppath As String = Path.Combine(dirToCopy, subdir.Name)
                    DirectoryCopy(subdir.FullName & "\", temppath, copySubDirs)
                Next

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub SetGroupBtn(nuevoFormulario() As String)
        Try
            Dim nuevoFile As String() = nuevoFormulario
            Dim posicionActual As Int32 = 0
            Dim indiceProximaLinea As Int32 = 0

            For Each lineaActiva As String In nuevoFormulario

                If lineaActiva.Contains("btn") And lineaActiva.Contains("<input") Then
                    Dim proximaLinea As String = siguienteLinea(nuevoFormulario, posicionActual, indiceProximaLinea)
                    If proximaLinea.Contains("btn") Then
                        nuevoFile(posicionActual) = "<div class=""input-group-btn"">" & lineaActiva
                        nuevoFile(indiceProximaLinea) = nuevoFile(indiceProximaLinea) & "</div>"
                    End If
                End If
                posicionActual += 1
            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub


    Private Sub SetHeaderForm(nuevoFormulario() As String)

        Dim nuevoFile As String() = nuevoFormulario
        Dim posicionActual As Int32 = 0

        For Each lineaActiva As String In nuevoFormulario

            If lineaActiva.Contains("div") And lineaActiva.Contains("id=""header""") Then

                If lineaActiva.Contains("</div>") Then
                    nuevoFile(posicionActual) = lineaActiva.Replace("div", "h4")
                Else
                    If nuevoFile(posicionActual + 2).Contains("</div>") Then
                        nuevoFile(posicionActual + 2) = nuevoFile(posicionActual + 2).Replace("div", "h4")
                        nuevoFile(posicionActual) = nuevoFile(posicionActual).Replace("div", "h4")
                    End If
                End If

                nuevoFile(posicionActual) = nuevoFile(posicionActual).Replace("id=", "class=""text-center"" id=")


                Dim pos As String() = nuevoFile(posicionActual).Split(ControlChars.Quote)
                For indice As Integer = 0 To pos.Length - 1 Step 1
                    If pos(indice).Contains("style=") Then
                        nuevoFile(posicionActual) = nuevoFile(posicionActual).Remove(nuevoFile(posicionActual).IndexOf("style="), pos(indice + 1).Length + 8)
                    End If
                Next


            ElseIf lineaActiva.Contains("div") And lineaActiva.Contains("id=""design""") Then

                If lineaActiva.Contains("</div>") Then
                    nuevoFile(posicionActual) = lineaActiva.Replace("div", "h4")
                Else
                    If nuevoFile(posicionActual + 2).Contains("</div>") Then
                        nuevoFile(posicionActual + 2) = nuevoFile(posicionActual + 2).Replace("div", "h4")
                        nuevoFile(posicionActual) = nuevoFile(posicionActual).Replace("div", "h4")
                    End If
                End If

                nuevoFile(posicionActual) = nuevoFile(posicionActual).Replace("id=", "class=""text-center"" id=")
                nuevoFile(posicionActual) = nuevoFile(posicionActual).Replace("design", "header")


                Dim pos As String() = nuevoFile(posicionActual).Split(ControlChars.Quote)
                For indice As Integer = 0 To pos.Length - 1 Step 1
                    If pos(indice).Contains("style=") Then
                        nuevoFile(posicionActual) = nuevoFile(posicionActual).Remove(nuevoFile(posicionActual).IndexOf("style="), pos(indice + 1).Length + 8)
                    End If
                Next

            End If

            posicionActual += 1
        Next
        nuevoFormulario = nuevoFile

    End Sub

    Private Sub ChangeCantCol(ByRef newForm As String())
        Try


            Dim posicionActual As Int32 = 0
            Dim ExitLine As Int32 = 0
            Dim nuevoFile As String() = newForm

            For Each lineaActiva As String In newForm

                If posicionActual > ExitLine OrElse ExitLine = 0 Then

                    If lineaActiva.Contains("class=""form-group row""") Then
                        Dim TagNameCount As Int32 = 0
                        TagNameCount = GetTagNameCount(newForm, posicionActual, "<div class=""input-group"">", ExitLine)

                        SetCountColumns(newForm, posicionActual, nuevoFile, TagNameCount, ExitLine)

                    End If

                End If

                posicionActual += 1

            Next

            newForm = nuevoFile
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub SetCountColumns(fileOLD() As String, index As Integer, nuevoFile() As String, tagNameCount As Integer, ByVal ExitLine As Integer)
        Try
            Dim CantSeparatorColumns As Integer

            If tagNameCount <> 0 Then
                CantSeparatorColumns = 12 / tagNameCount
            Else
                CantSeparatorColumns = 12
            End If

            For i As Int32 = index To fileOLD.Count - 1
                Try
                    Dim line As String = fileOLD(i)

                    If line.Contains("<div class=""col-md-6 col-sm-6"">") Then
                        If CantSeparatorColumns = 12 Then
                            nuevoFile(i) = fileOLD(i).Replace("md-6", "md-6 col-md-offset-3 ")
                            nuevoFile(i) = fileOLD(i).Replace("sm-6", "sm-6 col-sm-offset-3 ")
                        ElseIf CantSeparatorColumns <> 6 Then
                            nuevoFile(i) = fileOLD(i).Replace("6", CantSeparatorColumns.ToString)
                        End If
                    End If


                    If i = ExitLine Then
                        Exit For
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Next


        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Function GetTagNameCount(ByVal fileOLD() As String, ByVal index As Integer, ByVal TagName As String, ByRef ExitLine As Int32) As Int32
        Try
            Dim DivsCount = 0, TagNameCount As Int32

            For i As Int32 = index To fileOLD.Count - 1
                Dim line As String = fileOLD(i)

                Select Case True
                    Case line.Contains(TagName)
                        TagNameCount += 1
                        DivsCount += 1
                    Case line.Contains("<div")
                        DivsCount += 1
                    Case line.Contains("</div>")
                        DivsCount -= 1
                End Select

                If DivsCount = 0 Then
                    ExitLine = i
                    Return TagNameCount
                End If

            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private Sub SetColumnsInNewFile(ByRef newForm As String())
        Try

            Dim fileToWrite() As String = newForm

            Dim indiceLinea As Integer = 0
            Dim SetColumn As Boolean = False


            For Each lineaActiva As String In newForm

                Select Case True

                    Case lineaActiva.Contains("<span")
                        Dim proximaLinea = siguienteLinea(fileToWrite, indiceLinea)

                        fileToWrite(indiceLinea) = lineaActiva

                        SetColumn = SetColumnForSpan(fileToWrite, indiceLinea, proximaLinea)

                    Case ((lineaActiva.Contains("<input") And Not lineaActiva.Contains("type=""hidden""")) Or lineaActiva.Contains("<select") Or fileToWrite(indiceLinea).Contains("<textarea")) And Not lineaActiva.Contains("zamba_save")

                        Dim proximaLinea = siguienteLinea(fileToWrite, indiceLinea)

                        fileToWrite(indiceLinea) = lineaActiva

                        SetColumn = SetColumnForInput(fileToWrite, indiceLinea, SetColumn, proximaLinea)
                End Select

                indiceLinea += 1
            Next

            newForm = fileToWrite

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Function SetColumnForInput(fileToWrite() As String, indiceLinea As Integer, SetColumn As Boolean, proximaLinea As Object) As Boolean

        If Not ((proximaLinea.Contains("<input") And Not proximaLinea.Contains("type=""hidden""")) Or proximaLinea.Contains("<select") Or proximaLinea.Contains("<textarea")) And SetColumn Then

            If fileToWrite(indiceLinea).Contains("btn") Then
                Dim lineaAnterior = anteriorLinea(fileToWrite, indiceLinea)

                If lineaAnterior.Contains("<textarea") Then
                    fileToWrite(indiceLinea) = fileToWrite(indiceLinea).Replace("<input class=""btn  btn-info btn-sm""", "<span class=""input-group-addon btn btn-info btn-sm""")
                    fileToWrite(indiceLinea) = fileToWrite(indiceLinea).Replace("/>", "></span>")
                    fileToWrite(indiceLinea) = fileToWrite(indiceLinea)
                Else
                    fileToWrite(indiceLinea) = "<span class=""input-group-btn"">" & Chr(13) & fileToWrite(indiceLinea).ToString & Chr(13) & "</span>"
                End If
            End If


            fileToWrite(indiceLinea) = fileToWrite(indiceLinea).ToString & Chr(13) & "</div>" & Chr(13) & "</div>"
            SetColumn = False

        ElseIf proximaLinea.Contains("visibility") And SetColumn Then

            fileToWrite(indiceLinea) = fileToWrite(indiceLinea).ToString & Chr(13) & "</div>" & Chr(13) & "</div>"

            SetColumn = False
        End If

        Return SetColumn
    End Function

    Private Shared Function SetColumnForSpan(fileToWrite() As String, indiceLinea As Integer, proximaLinea As Object) As Boolean

        If (proximaLinea.Contains("<input") And Not proximaLinea.Contains("type=""hidden""")) Or proximaLinea.Contains("<select") Or proximaLinea.Contains("<textarea") Then

            fileToWrite(indiceLinea) = "<div class=""col-md-6 col-sm-6"">" & Chr(13) & "<div class=""input-group"">" & Chr(13) & fileToWrite(indiceLinea).ToString

            Return True
        Else
            Return False
        End If

    End Function

    Private Function anteriorLinea(fileToWrite() As String, indiceLinea As Integer) As Object
        Try

            For indiceAuxiliarDeControl As Integer = fileToWrite.Length - 1 To 0 Step -1

                If indiceAuxiliarDeControl < indiceLinea Then
                    If fileToWrite(indiceAuxiliarDeControl).Contains("<") Then
                        Return fileToWrite(indiceAuxiliarDeControl)
                    End If
                End If

            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private Function siguienteLinea(fileToWrite() As String, indiceLinea As Integer, Optional ByRef indiceDeProximaLinea As Integer = 0) As String
        Dim indiceAuxiliarDeControl As Integer = 0

        For Each lineaActiva As String In fileToWrite

            If indiceAuxiliarDeControl > indiceLinea Then
                If lineaActiva.Contains("<") Then
                    Return lineaActiva
                End If
            End If

            indiceAuxiliarDeControl += 1
            indiceDeProximaLinea = indiceAuxiliarDeControl
        Next




    End Function

    Private Sub ReplaceTablesAndTabbers(newForm As String())
        Try
            Dim namesTabs As New List(Of String)

            CountCantTabs(newForm, namesTabs)

            newForm = ChangeLinksToBootstrap(newForm)

            newForm = SetContainer(newForm)

            newForm = SetTabsNames(newForm, namesTabs)

            newForm = ReplaceTable(newForm)

            newForm = ReplaceRow(newForm)

            newForm = RemoveTd(newForm)

            newForm = SetSeparatorIframe(newForm)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Reemplazo tablas y filas por bootstrap")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Function SetSeparatorIframe(newForm() As String) As String()
        Dim arrayFormTemporal() As String
        Dim indiceLinea As Integer

        Try
            indiceLinea = 0
            arrayFormTemporal = newForm

            For Each lineaActiva As String In newForm

                If lineaActiva.Contains("<center") Then

                    Dim proximaLinea As String = siguienteLinea(newForm, indiceLinea)

                    If proximaLinea.Contains("iframe") Then
                        arrayFormTemporal(indiceLinea) = lineaActiva.Replace("<center", "<hr/> <center")
                    End If
                End If

                indiceLinea += 1
            Next
            Return arrayFormTemporal
        Catch ex As Exception
            ZClass.raiseerror(ex)

        End Try

    End Function

    Private Function RemoveTd(newForm() As String) As String()

        Dim arrayFormTemporal() As String
        Dim indiceLinea As Integer

        Try
            indiceLinea = 0
            arrayFormTemporal = newForm

            For Each lineaActiva As String In newForm
                arrayFormTemporal(indiceLinea) = lineaActiva.Trim

                If arrayFormTemporal(indiceLinea).Contains("<td") Then
                    arrayFormTemporal(indiceLinea) = arrayFormTemporal(indiceLinea).Remove(arrayFormTemporal(indiceLinea).IndexOf("<td"), arrayFormTemporal(indiceLinea).IndexOf(">") + 1)
                End If

                If arrayFormTemporal(indiceLinea).Contains("</td") Then
                    arrayFormTemporal(indiceLinea) = arrayFormTemporal(indiceLinea).Remove(arrayFormTemporal(indiceLinea).IndexOf("</td"), 5)
                End If


                indiceLinea += 1
            Next

            Return arrayFormTemporal

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private Function SetContainer(newForm() As String) As String()

        Dim arrayTemporal As String()
        Dim indiceLinea As Integer
        Try

            arrayTemporal = newForm
            indiceLinea = 0

            For Each lineaActiva As String In newForm

                Select Case True

                    Case lineaActiva.Contains("</form>")
                        arrayTemporal(indiceLinea) = "</div>" & Chr(13) & lineaActiva.ToString

                    Case lineaActiva.Contains("<body")
                        arrayTemporal(indiceLinea) = lineaActiva & Chr(13) & "<div class=""container-fluid"">"

                End Select

                indiceLinea += 1

            Next

            Return arrayTemporal

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private Function SetTabsNames(newForm() As String, namesTabs As List(Of String)) As String()

        Dim arrayTemporal As String()
        Dim indiceLinea As Integer
        Dim isFirst As Boolean
        Try

            arrayTemporal = newForm
            indiceLinea = 0
            isFirst = True

            If namesTabs.Count > 0 Then

                Dim textTabs As New StringBuilder

                CreateDivTabs(namesTabs, textTabs)

                For Each lineaActiva As String In newForm

                    If lineaActiva.Contains("class=""tabber""") Then
                        arrayTemporal(indiceLinea) = textTabs.ToString & Chr(13) & "<div class=""tab-content"">"
                    End If

                    If lineaActiva.Contains("class=""tabbertab""") Then

                        If isFirst Then
                            arrayTemporal(indiceLinea) = lineaActiva.Replace("tabbertab", "tab-pane active") & "<p></p>"
                            isFirst = False
                        Else
                            arrayTemporal(indiceLinea) = lineaActiva.Replace("tabbertab", "tab-pane") & "<p></p>"

                        End If

                    End If

                    indiceLinea += 1
                Next


            End If
            Return arrayTemporal
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private Shared Sub CreateDivTabs(namesTabs As List(Of String), textTabs As StringBuilder)

        Dim isFirst As Boolean = True
        textTabs.AppendLine("<ul class="" nav nav-tabs"">")

        For Each currentTab As String In namesTabs
            If isFirst Then
                textTabs.AppendLine("<li class="" active""><a href=" & Chr(34) & "#" & currentTab & Chr(34) & "  data-toggle=""tab"">" & currentTab & "</a></li>")
                isFirst = False
            Else
                textTabs.AppendLine("<li><a href=" & Chr(34) & "#" & currentTab & Chr(34) & "  data-toggle=""tab"">" & currentTab & "</a></li>")
            End If
        Next

        textTabs.AppendLine("</ul>")

    End Sub

    Private Function ChangeLinksToBootstrap(newForm() As String) As String()
        Dim arrayTemporal As String()
        Dim indiceLinea As Integer
        Try
            arrayTemporal = newForm
            indiceLinea = 0

            For Each lineaActiva As String In newForm

                If lineaActiva.Contains("<script") Then

                    Dim linkToBootstrap As New StringBuilder
                    linkToBootstrap.AppendLine("<script src=""Scripts/Zamba.js"" type=""text/javascript"" language=""JavaScript""></script>")
                    arrayTemporal(indiceLinea) = linkToBootstrap.ToString
                    Exit For
                End If

                If lineaActiva.Contains("<html>") Then
                    arrayTemporal(indiceLinea) = "<!DOCTYPE HTML>" & Chr(13) & lineaActiva
                End If

                indiceLinea += 1
            Next

            Return arrayTemporal
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private Sub CountCantTabs(newForm() As String, listOfTabs As List(Of String))
        Try


            For Each lineaActiva As String In newForm
                If lineaActiva.Contains("<h2>") Then
                    listOfTabs.Add(GetTextHTML(lineaActiva))
                End If

            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Function ReplaceInputsAndTag(newForm As String()) As String()
        Try
            newForm = ReplaceTag(newForm, "<label", "<span class=""input-group-addon""", True)
            newForm = ReplaceTag(newForm, "</label>", "</span>", False)
            newForm = ReplaceTag(newForm, "<select", "<select class=""form-control input-sm""", True)
            newForm = ReplaceTag(newForm, "<textarea", "<textarea class=""form-control input-sm""", True)
            newForm = ReplaceNewInput(newForm)
            newForm = ReplaceSeparator(newForm)
            Return newForm

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private Shared Function ReplaceSeparator(newForm() As String) As String()
        Dim arrayFormTemporal As String() = newForm
        Dim indiceLinea As Integer = 0

        For Each lineaActiva As String In newForm

            Select Case True
                Case lineaActiva.Contains("</br>") Or lineaActiva.Contains("separador") Or lineaActiva.Contains("<br>")
                    arrayFormTemporal(indiceLinea) = ""

            End Select

            indiceLinea += 1
        Next

        Return arrayFormTemporal
    End Function

    Private Function ReplaceTable(newForm() As String) As String()

        Dim arrayFormTemporal() As String
        Dim indiceLinea As Integer
        Try
            indiceLinea = 0
            arrayFormTemporal = newForm

            For Each lineaActiva As String In newForm

                If lineaActiva.Contains("<table") Then
                    arrayFormTemporal(indiceLinea) = lineaActiva.Replace("<table", "<div")
                End If

                If lineaActiva.Contains("</table") Then
                    arrayFormTemporal(indiceLinea) = "</div>"
                End If

                indiceLinea += 1
            Next
            Return arrayFormTemporal
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Function

    Private Function ReplaceRow(newForm() As String) As String()
        Dim arrayFormTemporal() As String
        Dim indiceLinea As Integer
        Try
            indiceLinea = 0
            arrayFormTemporal = newForm

            For Each lineaActiva As String In newForm

                If lineaActiva.Contains("<tr>") And lineaActiva.Contains("<td>") And Not lineaActiva.Contains("<div") Then
                    arrayFormTemporal(indiceLinea) = arrayFormTemporal(indiceLinea).Remove(arrayFormTemporal(indiceLinea).IndexOf("<tr"), (arrayFormTemporal(indiceLinea).IndexOf("td>") + 3) - arrayFormTemporal(indiceLinea).IndexOf("<tr"))

                ElseIf lineaActiva.Contains("<tr") Then
                    arrayFormTemporal(indiceLinea) = "<div class=""form-group row"">"
                End If

                If lineaActiva.Contains("</tr>") Then
                    arrayFormTemporal(indiceLinea) = "</div>"
                End If

                indiceLinea += 1
            Next

            Return arrayFormTemporal

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function
    Private Function ReplaceTag(ByVal newForm As String(), ByVal TagToReplace As String, ByVal NewTag As String, ByVal RemoveClass As Boolean) As String()
        Dim arrayTemporal As String()
        Dim indiceActual As Integer
        Try

            arrayTemporal = newForm
            indiceActual = 0

            For Each lineaActiva As String In newForm

                If lineaActiva.Contains(TagToReplace) Then

                    arrayTemporal(indiceActual) = lineaActiva.Trim


                    If RemoveClass Then arrayTemporal(indiceActual) = RemoveStyleAndClass(arrayTemporal(indiceActual))

                    arrayTemporal(indiceActual) = arrayTemporal(indiceActual).Replace(TagToReplace, NewTag)

                End If

                indiceActual += 1

            Next

            arrayTemporal = RemoveTd(arrayTemporal)



            Return arrayTemporal

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private Shared Function RemoveStyleAndClass(lineaActiva As String) As String
        Try
            Dim indice As Integer = 0
            If lineaActiva.ToLower.Contains("class=""w") Then
                Dim pos As String() = lineaActiva.Split(ControlChars.Quote)
                For indice = 0 To pos.Length - 1
                    If pos(indice).Contains("class=") Then
                        lineaActiva = lineaActiva.Remove(lineaActiva.IndexOf("class="), pos(indice + 1).Length + 8)
                    End If
                Next
                indice = 0
            End If
            If lineaActiva.ToLower.Contains("style=""width") Then
                Dim pos As String() = lineaActiva.Split(ControlChars.Quote)
                For indice = 0 To pos.Length - 1
                    If pos(indice).Contains("style=") Then
                        lineaActiva = lineaActiva.Remove(lineaActiva.IndexOf("style="), pos(indice + 1).Length + 8)
                    End If
                Next
                indice = 0
            End If
            Return lineaActiva

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private Shared Function ReplaceNewInput(ByRef newForm As String()) As String()
        Dim arrayTemporal As String()
        Dim indiceActual As Integer
        Try
            arrayTemporal = newForm
            indiceActual = 0

            For Each lineaActiva As String In newForm

                If lineaActiva.Contains("<input") And Not lineaActiva.Contains("type=""hidden""") Then

                    arrayTemporal(indiceActual) = lineaActiva.Trim


                    arrayTemporal(indiceActual) = RemoveStyleAndClass(arrayTemporal(indiceActual))
                    If arrayTemporal(indiceActual).Contains("zamba_save") Then
                        arrayTemporal(indiceActual) = arrayTemporal(indiceActual).Replace("<input", "<input class=""btn  btn-primary""")
                    ElseIf arrayTemporal(indiceActual).Contains("submit") Then
                        arrayTemporal(indiceActual) = arrayTemporal(indiceActual).Replace("<input", "<input class=""btn  btn-info btn-sm""")
                    Else
                        arrayTemporal(indiceActual) = arrayTemporal(indiceActual).Replace("<input", "<input class=""form-control input-sm""")
                    End If

                End If

                If lineaActiva.Contains("<button") Then
                    arrayTemporal(indiceActual) = lineaActiva.Trim
                    arrayTemporal(indiceActual) = RemoveStyleAndClass(arrayTemporal(indiceActual))

                    arrayTemporal(indiceActual) = ReplaceButton(arrayTemporal(indiceActual))

                End If

                indiceActual += 1

            Next

            Return arrayTemporal

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private Shared Function ReplaceButton(lineaActiva As String) As String
        Dim nameButton As String

        Try

            If lineaActiva.Contains("</button>") Then
                nameButton = lineaActiva.Substring(lineaActiva.IndexOf("/>") + 2, lineaActiva.IndexOf("</") - (lineaActiva.IndexOf("/>") + 2))

                lineaActiva = lineaActiva.Replace("name=", "value=""" & nameButton.ToString & """ name=")

                lineaActiva = lineaActiva.Remove(lineaActiva.IndexOf("/>") + 1, lineaActiva.LastIndexOf(">") - (lineaActiva.IndexOf("/>") + 1))
            End If
            If lineaActiva.Contains("zamba_save") Then
                lineaActiva = lineaActiva.Replace("<button", "<input class=""btn  btn-primary""")
            ElseIf lineaActiva.Contains("onclick") Then
                lineaActiva = lineaActiva.Replace("<button", "<input class=""btn  btn-info btn-sm""")
            Else
                lineaActiva = lineaActiva.Replace("<button", "<input class=""form-control input-sm""")
            End If


            Return lineaActiva

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private Shared Function GetTextHTML(lineaActiva As String) As String
        Try
            Dim longitudNameTab As String = lineaActiva.Trim.Split("</", 3, System.StringSplitOptions.RemoveEmptyEntries)(0)
            Return longitudNameTab.Substring(longitudNameTab.IndexOf(">") + 1)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private Sub ZPanel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim pathFormularioTemporal As String = String.Empty
        Dim carpetaDeFormulariosVIejos As String = String.Empty
        Dim myForms As List(Of ZwebForm)

        Try

            If MessageBox.Show(Me, "¿Desea transformar todos los formularios?", "Convertir a bootstrap", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                CreateAndCopyAllFiles(pathFormularioTemporal, carpetaDeFormulariosVIejos, myForms)

                For Each form As ZwebForm In myForms

                    Dim nuevoFormulario() As String = File.ReadAllLines(form.TempFullPath)

                    If Not Directory.Exists(carpetaDeFormulariosVIejos) Then
                        Directory.CreateDirectory(carpetaDeFormulariosVIejos)
                    End If


                    If Not File.Exists(carpetaDeFormulariosVIejos & "\" & form.TempPathName) Then
                        File.Copy(form.TempFullPath, carpetaDeFormulariosVIejos & "\" & form.TempPathName, False)

                        ReplaceTablesAndTabbers(nuevoFormulario)

                        nuevoFormulario = ReplaceInputsAndTag(nuevoFormulario)

                        File.WriteAllLines(pathFormularioTemporal, nuevoFormulario)

                        nuevoFormulario = File.ReadAllLines(pathFormularioTemporal)

                        SetColumnsInNewFile(nuevoFormulario)

                        File.WriteAllLines(pathFormularioTemporal, nuevoFormulario)

                        nuevoFormulario = File.ReadAllLines(pathFormularioTemporal)

                        ChangeCantCol(nuevoFormulario)

                        SetGroupBtn(nuevoFormulario)

                        SetHeaderForm(nuevoFormulario)

                        File.WriteAllLines(form.TempFullPath, nuevoFormulario)

                        File.Delete(pathFormularioTemporal)

                    End If
                Next
                MessageBox.Show("Se realizaron los cambios a los formularios.")
            End If


        Catch ex As Exception
            ZClass.raiseerror(ex)

        End Try
    End Sub

    Private Sub btnupdateformsversion_Click(sender As Object, e As EventArgs) Handles btnupdateformsversion.Click
        Try
            Dim ActualFormVersion As Int32 = ZOptBusiness.GetValueOrDefault("FormsVersion", 1)
            ZOptBusiness.InsertUpdateValue("FormsVersion", ActualFormVersion + 1)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
