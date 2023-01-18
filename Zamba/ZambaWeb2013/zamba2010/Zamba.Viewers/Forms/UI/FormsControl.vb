Imports Zamba.AppBlock
Imports Zamba.Core
Imports Zamba.HTMLEditor
Imports System.IO
Imports FormulariosDinamicos

Public Class FormsControl
    Inherits ZBlueControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        FormBrowser = New FormBrowser
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
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents Panel1 As ZBluePanel
    Friend WithEvents Panel2 As ZBluePanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtpath As System.Windows.Forms.TextBox
    Friend WithEvents Btnexplore As ZButton
    Friend WithEvents txtname As System.Windows.Forms.TextBox
    Friend WithEvents cbotype As System.Windows.Forms.ComboBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents btnadd As ZButton
    Friend WithEvents Splitter1 As ZSplitter
    Friend WithEvents Splitter2 As ZSplitter
    Friend WithEvents Panel3 As ZBluePanel
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents cbodoctype As System.Windows.Forms.ComboBox
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnedit As ZButton
    Friend WithEvents btndel As ZButton
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents btnNewFormHtml As Zamba.AppBlock.ZButton
    Friend WithEvents BtnEditHtml As Zamba.AppBlock.ZButton
    Friend WithEvents btnReplicate As Zamba.AppBlock.ZButton
    Friend WithEvents chkUseRuleRights As System.Windows.Forms.CheckBox
    Friend WithEvents btncond As Zamba.AppBlock.ZButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Panel1 = New Zamba.AppBlock.ZBluePanel
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.Panel2 = New Zamba.AppBlock.ZBluePanel
        Me.chkUseRuleRights = New System.Windows.Forms.CheckBox
        Me.btnReplicate = New Zamba.AppBlock.ZButton
        Me.btncond = New Zamba.AppBlock.ZButton
        Me.btnNewFormHtml = New Zamba.AppBlock.ZButton
        Me.BtnEditHtml = New Zamba.AppBlock.ZButton
        Me.btndel = New Zamba.AppBlock.ZButton
        Me.btnedit = New Zamba.AppBlock.ZButton
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cbodoctype = New System.Windows.Forms.ComboBox
        Me.btnadd = New Zamba.AppBlock.ZButton
        Me.cbotype = New System.Windows.Forms.ComboBox
        Me.txtname = New System.Windows.Forms.TextBox
        Me.Btnexplore = New Zamba.AppBlock.ZButton
        Me.txtpath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Splitter1 = New Zamba.AppBlock.ZSplitter
        Me.Splitter2 = New Zamba.AppBlock.ZSplitter
        Me.Panel3 = New Zamba.AppBlock.ZBluePanel
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel1.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel1.Controls.Add(Me.ListBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(286, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(466, 267)
        Me.Panel1.TabIndex = 0
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.Color.White
        Me.ListBox1.ContextMenu = Me.ContextMenu1
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.Location = New System.Drawing.Point(0, 0)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(464, 264)
        Me.ListBox1.TabIndex = 0
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem2, Me.MenuItem1})
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 0
        Me.MenuItem2.Text = "Actualizar"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 1
        Me.MenuItem1.Text = "Eliminar"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel2.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel2.Controls.Add(Me.chkUseRuleRights)
        Me.Panel2.Controls.Add(Me.btnReplicate)
        Me.Panel2.Controls.Add(Me.btncond)
        Me.Panel2.Controls.Add(Me.btnNewFormHtml)
        Me.Panel2.Controls.Add(Me.BtnEditHtml)
        Me.Panel2.Controls.Add(Me.btndel)
        Me.Panel2.Controls.Add(Me.btnedit)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.cbodoctype)
        Me.Panel2.Controls.Add(Me.btnadd)
        Me.Panel2.Controls.Add(Me.cbotype)
        Me.Panel2.Controls.Add(Me.txtname)
        Me.Panel2.Controls.Add(Me.Btnexplore)
        Me.Panel2.Controls.Add(Me.txtpath)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.ForeColor = System.Drawing.Color.Black
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(281, 640)
        Me.Panel2.TabIndex = 1
        '
        'chkUseRuleRights
        '
        Me.chkUseRuleRights.AutoSize = True
        Me.chkUseRuleRights.BackColor = System.Drawing.Color.Transparent
        Me.chkUseRuleRights.Location = New System.Drawing.Point(19, 254)
        Me.chkUseRuleRights.Name = "chkUseRuleRights"
        Me.chkUseRuleRights.Size = New System.Drawing.Size(215, 17)
        Me.chkUseRuleRights.TabIndex = 23
        Me.chkUseRuleRights.Text = "Utilizar hablitación de botones por regla"
        Me.chkUseRuleRights.UseVisualStyleBackColor = False
        '
        'btnReplicate
        '
        Me.btnReplicate.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnReplicate.Location = New System.Drawing.Point(16, 350)
        Me.btnReplicate.Name = "btnReplicate"
        Me.btnReplicate.Size = New System.Drawing.Size(115, 27)
        Me.btnReplicate.TabIndex = 22
        Me.btnReplicate.Text = "Replicar"
        '
        'btncond
        '
        Me.btncond.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btncond.Location = New System.Drawing.Point(16, 320)
        Me.btncond.Name = "btncond"
        Me.btncond.Size = New System.Drawing.Size(115, 27)
        Me.btncond.TabIndex = 21
        Me.btncond.Text = "Condiciones"
        '
        'btnNewFormHtml
        '
        Me.btnNewFormHtml.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnNewFormHtml.Location = New System.Drawing.Point(16, 80)
        Me.btnNewFormHtml.Name = "btnNewFormHtml"
        Me.btnNewFormHtml.Size = New System.Drawing.Size(60, 24)
        Me.btnNewFormHtml.TabIndex = 20
        Me.btnNewFormHtml.Text = "Nuevo"
        '
        'BtnEditHtml
        '
        Me.BtnEditHtml.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnEditHtml.Location = New System.Drawing.Point(102, 80)
        Me.BtnEditHtml.Name = "BtnEditHtml"
        Me.BtnEditHtml.Size = New System.Drawing.Size(66, 24)
        Me.BtnEditHtml.TabIndex = 19
        Me.BtnEditHtml.Text = "Editar"
        '
        'btndel
        '
        Me.btndel.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btndel.Location = New System.Drawing.Point(137, 320)
        Me.btndel.Name = "btndel"
        Me.btndel.Size = New System.Drawing.Size(135, 27)
        Me.btndel.TabIndex = 12
        Me.btndel.Text = "Eliminar"
        '
        'btnedit
        '
        Me.btnedit.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnedit.Location = New System.Drawing.Point(137, 290)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(135, 27)
        Me.btnedit.TabIndex = 11
        Me.btnedit.Text = "Editar/Actualizar"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(279, 22)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Formularios"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(16, 200)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(152, 16)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Tipo de Documento"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(16, 152)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(152, 16)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Tipo de Formulario"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(16, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 16)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Nombre"
        '
        'cbodoctype
        '
        Me.cbodoctype.BackColor = System.Drawing.Color.White
        Me.cbodoctype.Location = New System.Drawing.Point(16, 216)
        Me.cbodoctype.Name = "cbodoctype"
        Me.cbodoctype.Size = New System.Drawing.Size(256, 21)
        Me.cbodoctype.TabIndex = 6
        '
        'btnadd
        '
        Me.btnadd.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnadd.Location = New System.Drawing.Point(16, 290)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(115, 27)
        Me.btnadd.TabIndex = 5
        Me.btnadd.Text = "Agregar"
        '
        'cbotype
        '
        Me.cbotype.BackColor = System.Drawing.Color.White
        Me.cbotype.Location = New System.Drawing.Point(16, 168)
        Me.cbotype.Name = "cbotype"
        Me.cbotype.Size = New System.Drawing.Size(256, 21)
        Me.cbotype.TabIndex = 4
        '
        'txtname
        '
        Me.txtname.BackColor = System.Drawing.Color.White
        Me.txtname.Location = New System.Drawing.Point(16, 120)
        Me.txtname.Name = "txtname"
        Me.txtname.Size = New System.Drawing.Size(256, 21)
        Me.txtname.TabIndex = 3
        '
        'Btnexplore
        '
        Me.Btnexplore.DialogResult = System.Windows.Forms.DialogResult.None
        Me.Btnexplore.Location = New System.Drawing.Point(195, 80)
        Me.Btnexplore.Name = "Btnexplore"
        Me.Btnexplore.Size = New System.Drawing.Size(77, 24)
        Me.Btnexplore.TabIndex = 2
        Me.Btnexplore.Text = "Examinar"
        '
        'txtpath
        '
        Me.txtpath.BackColor = System.Drawing.Color.White
        Me.txtpath.Location = New System.Drawing.Point(16, 56)
        Me.txtpath.Name = "txtpath"
        Me.txtpath.Size = New System.Drawing.Size(256, 21)
        Me.txtpath.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(16, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(160, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Formulario"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(182, Byte), Integer), CType(CType(9, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(281, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(5, 640)
        Me.Splitter1.TabIndex = 2
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(182, Byte), Integer), CType(CType(9, Byte), Integer))
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter2.Location = New System.Drawing.Point(286, 267)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(466, 5)
        Me.Splitter2.TabIndex = 3
        Me.Splitter2.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel3.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.ForeColor = System.Drawing.Color.Black
        Me.Panel3.Location = New System.Drawing.Point(286, 272)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(466, 368)
        Me.Panel3.TabIndex = 4
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'FormsControl
        '
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "FormsControl"
        Me.Size = New System.Drawing.Size(752, 640)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim FormBrowser As FormBrowser
    Dim selectedIndex As Int64
    Private _formsComparer As New FormComparer()

#Region "Load"
    Private Sub FormsControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadFormBrowser()
        LoadDocTypes()
        LoadFormTypes()
        LoadExistingForms()
        AddHandlers()
    End Sub
    Private Sub LoadFormBrowser()
        Try
            FormBrowser.Dock = DockStyle.Fill
            Me.Panel3.Controls.Add(FormBrowser)
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
            Me.cbodoctype.DataSource = DocTypesBusiness.GetDocTypesArrayList(True)
            Me.cbodoctype.DisplayMember = "Name"
            Me.cbodoctype.ValueMember = "Id"
            Me.cbodoctype.SelectedIndex = 0
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LoadFormTypes()
        Try
            Me.cbotype.Items.Add(FormTypes.Search.ToString)
            Me.cbotype.Items.Add(FormTypes.Edit.ToString)
            Me.cbotype.Items.Add(FormTypes.Show.ToString)
            Me.cbotype.Items.Add(FormTypes.WorkFlow.ToString)
            Me.cbotype.Items.Add(FormTypes.Insert.ToString)
            Me.cbodoctype.Text = FormTypes.Show.ToString
            Me.cbodoctype.SelectedIndex = 2
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Dim ZWebforms As ArrayList
    Dim ZWFWebforms() As ZwebForm
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
            'If load = True Then
            '    ZWebforms = FormBusiness.GetForms
            '    '      End If

            '    'CONCATENA AL NOMBRE DEL FORMULARIO EL TIPO
            '    For Each Item As ZwebForm In Me.ZWebforms
            '        Select Case Item.Type
            '            Case FormTypes.All
            '                Item.Name += " - Todo"
            '            Case FormTypes.Edit
            '                Item.Name += " - Editar"
            '            Case FormTypes.Insert
            '                Item.Name += " - Insertar"
            '            Case FormTypes.Search
            '                Item.Name += " - Busqueda"
            '            Case FormTypes.Show
            '                Item.Name += " - Visualizar"
            '            Case FormTypes.WorkFlow
            '                Item.Name += " - Workflow"
            '        End Select

            '    Next

            'End If
            '[sebastian] se le saco el if porque sino a la propiedad name de zwebforms le agregaba nuevamente el
            'concatenaba nuevamente el tipo de archivo, y no es necesario ya que lo tenia anexado. 30/09/2008
            ZWebforms = FormBusiness.GetForms
            '      End If
            'CONCATENA AL NOMBRE DEL FORMULARIO EL TIPO
            For Each Item As ZwebForm In Me.ZWebforms

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
                End Select

            Next

            ZWebforms.Sort(_formsComparer)
            Me.ListBox1.DataSource = Nothing
            Me.ListBox1.Items.Clear()
            Me.ListBox1.DataSource = ZWebforms
            Me.ListBox1.ValueMember = "ID"
            Me.ListBox1.DisplayMember = "Name"
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub AddHandlers()
        RemoveHandler ListBox1.SelectedIndexChanged, AddressOf ListBox1_SelectedIndexChanged
        AddHandler ListBox1.SelectedIndexChanged, AddressOf ListBox1_SelectedIndexChanged
    End Sub
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
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged

        If Not cancelSelectEvent Then

            Try
                updateFormData()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

        End If

    End Sub

#End Region

#Region "Add"

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
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

        Try
            ErrorProvider1.Clear()
            ' [Tomas]   25/02/09    Compruebo que se haya ingresado un path de formulario,
            '                       en caso no se haya ingresado pregunto si quiere crear un
            '                       form virtual.
            If (Me.txtpath.Text.Trim() <> "") Then

                If (IsValidForm("add")) Then
                    AddZWebForm(ToolsBusiness.GetNewID(IdTypes.ZWEBFORM), Me.txtname.Text.Trim, String.Empty, Me.cbotype.SelectedIndex.ToString, Me.txtpath.Text.Trim, DirectCast(Me.cbodoctype.SelectedItem, DocType).ID, Nothing)

                End If

            Else

                If (MessageBox.Show("El path del formulario no fue ingresado," & vbCrLf & _
                                   "¿Desea crear un formulario dinámico?", "Confirmar acción", _
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
                                If String.Compare(Me.cbotype.Text, FormTypes.Show.ToString) = 0 OrElse _
                                String.Compare(Me.cbotype.Text, FormTypes.Insert.ToString) = 0 OrElse _
                                String.Compare(Me.cbotype.Text, FormTypes.Edit.ToString) = 0 Then
                                    'Comprueba si existe el insert
                                    If Not DoesNameFormExists(Me.txtname.Text & " - " & GetSpanishNameType(FormTypes.Show.ToString)) Then
                                        AddZWebForm(state.Formid, state.FormName, String.Empty, Me.cbotype.Items.IndexOf(FormTypes.Show.ToString).ToString, String.Empty, DirectCast(Me.cbodoctype.SelectedItem, DocType).ID, state)
                                    End If
                                    FormBusiness.generateDynamicFormId(state)
                                    'Comprueba si existe el show
                                    If Not DoesNameFormExists(Me.txtname.Text & " - " & GetSpanishNameType(FormTypes.Insert.ToString)) Then
                                        AddZWebForm(state.Formid, state.FormName, String.Empty, Me.cbotype.Items.IndexOf(FormTypes.Insert.ToString).ToString, String.Empty, DirectCast(Me.cbodoctype.SelectedItem, DocType).ID, state)
                                    End If
                                    FormBusiness.generateDynamicFormId(state)
                                    'Comprueba si existe el edit
                                    If Not DoesNameFormExists(Me.txtname.Text & " - " & GetSpanishNameType(FormTypes.Edit.ToString)) Then
                                        AddZWebForm(state.Formid, state.FormName, String.Empty, Me.cbotype.Items.IndexOf(FormTypes.Edit.ToString).ToString, String.Empty, DirectCast(Me.cbodoctype.SelectedItem, DocType).ID, state)
                                    End If
                                Else
                                    AddZWebForm(state.Formid, state.FormName, String.Empty, Me.cbotype.SelectedIndex.ToString, String.Empty, DirectCast(Me.cbodoctype.SelectedItem, DocType).ID, state)
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
    ''' <param name="doctypeid">Tipo de documento al que pertenece el formulario</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas 03/03/09]    Modified    Al agregar el formulario selecciona el item en la lista
    ''' 	[Gaston]	  29/04/2009	Modified    Corrección para el tipo de formulario
    '''     [Gaston]	  05/05/2009	Modified  El formulario se inserta en la tabla ZFrms y por último en las tablas relacionadas con formularios
    '''                                           dinámicos. Esto se hizo así debido a que se agregaron las relaciones entre las tablas que tienen 
    '''                                           que ver con formularios dinámicos
    ''' [Sebastian] 05-06-2009 Modified se realizaron casteo para salvar los warnings
    ''' </history>
    Private Sub AddZWebForm(ByVal formId As Int64, ByVal formName As String, ByVal description As String, ByVal typestr As String, ByVal filepath As String, ByVal doctypeid As Int64, ByVal state As DynamicFormState)

        Dim type As FormTypes

        Select Case typestr
            Case "0"
                type = FormTypes.Search
            Case "1"
                type = FormTypes.Edit
            Case "2"
                type = FormTypes.Show
            Case "3"
                type = FormTypes.WorkFlow
            Case "4"
                type = FormTypes.Insert
        End Select

        ' Se guardan los datos de dicho formulario en la tabla ZFrms
        Dim zwf As New ZwebForm(Int32.Parse(formId.ToString), formName, description, type, filepath, Int32.Parse(doctypeid.ToString), chkUseRuleRights.Checked)
        FormBusiness.InsertForm(zwf)

        ' Si state no es nothing entonces significa que se está agregando un formulario dinámico
        If (Not IsNothing(state)) Then
            ' Se guardan los datos del formulario dinámico en las tablas relacionadas con formularios dinámicos
            FormBusiness.SaveDynamicForm(state)
        End If

        Me.ZWebforms.Add(zwf)
        ZWebforms.Sort(_formsComparer)
        cancelSelectEvent = True
        LoadExistingForms(False)
        cancelSelectEvent = False

        'Tomas 03/03/09:    Muestra el item recién agregado
        ' [Gaston]  06/05/2009 Int64 a Int32
        Me.ListBox1.SelectedValue = Int32.Parse(formId.ToString)

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

            If File.Exists(Me.txtpath.Text.Trim) = False Then
                Me.ErrorProvider1.SetError(Me.txtpath, "Debe especificar un formulario valido")
                Return False
            End If
            If Me.txtname.Text.Trim = "" Then
                Me.ErrorProvider1.SetError(Me.txtname, "Debe especificar un nombre valido")
                Return False
            End If
            If DoesNameFormExists(Me.txtname.Text & " - " & GetSpanishNameType(Me.cbotype.Text)) And String.Compare(AddOrEdit.ToLower, "add") = 0 Then
                Me.ErrorProvider1.SetError(Me.txtname, "El formulario '" & Me.txtname.Text & " - " & _
                                           Me.cbotype.Text & "' ya existe." & vbCrLf & _
                                           "Elija otro nombre para continuar.")
                Me.txtname.Clear()
                Me.txtname.Focus()
                Return False
            End If
            If Me.cbotype.SelectedIndex = -1 Then
                Me.ErrorProvider1.SetError(Me.cbotype, "Debe especificar un tipo de formulario valido")
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
            If Me.txtname.Text.Trim = "" Then
                Me.ErrorProvider1.SetError(Me.txtname, "Debe especificar un nombre valido")
                Return False
            End If
            If Me.cbotype.SelectedIndex = -1 Then
                Me.ErrorProvider1.SetError(Me.cbotype, "Debe especificar un tipo de formulario valido")
                Return False
            End If

            If DoesNameFormExists(Me.txtname.Text & " - " & GetSpanishNameType(Me.cbotype.Text)) And String.Compare(AddOrEdit.ToLower, "add") = 0 Then
                Me.ErrorProvider1.SetError(Me.txtname, "El formulario '" & Me.txtname.Text & " - " & _
                                           Me.cbotype.Text & "' ya existe." & vbCrLf & _
                                           "Elija otro nombre para continuar.")
                Me.txtname.Clear()
                Me.txtname.Focus()
                Return False
            End If
            If Me.cbodoctype.SelectedIndex = -1 Then
                Me.ErrorProvider1.SetError(Me.cbodoctype, "Debe especificar un tipo de documento valido")
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Conditions"

    Private Sub btncond_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncond.Click
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
            If (Me.ListBox1.SelectedIndex <> -1) Then

                If (IsValidVirtualForm("edit")) Then

                    Dim doctypeid As Int64 = CLng(cbodoctype.SelectedValue())
                    Dim formid As Int64 = CLng(Me.ListBox1.SelectedValue)

                    Dim state As New DynamicFormState(doctypeid, True, formid)

                    FormBusiness.GetDynamicFormState(state)
                    state.FormName = txtname.Text
                    state.DoctypeName = cbodoctype.Text

                    Dim frmAddFrmConditions As New frmAbmZfrmDesc(state)
                    frmAddFrmConditions.Tag = "openFromBtnConditions"
                    frmAddFrmConditions.ShowDialog()

                    If (state.IsFinish) Then

                        ' Si el path está vacío entonces se actualizan las condiciones de un formulario dinámico
                        If (CType(Me.ListBox1.SelectedItem, ZwebForm).Path = "") Then
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

#Region "Delete"

    Private Sub btndel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndel.Click
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

            If (Me.ListBox1.SelectedIndex <> -1) Then

                If (MessageBox.Show("¿Desea eliminar el formulario seleccionado?", "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes) Then

                    Dim Form As ZwebForm = DirectCast(Me.ZWebforms(Me.ListBox1.SelectedIndex), ZwebForm)
                    FormBusiness.DeleteForm(Form)

                    Dim formname As String = Form.Name
                    Dim removeindex As Int32 = formname.LastIndexOf("-")
                    formname = formname.Remove(removeindex - 1, formname.Length - removeindex + 1)
                    Dim filepath As String = Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\" & formname & ".html"

                    cancelSelectEvent = True
                    Me.ZWebforms.Remove(Form)
                    LoadExistingForms(False)
                    cancelSelectEvent = False

                    'si es un formulario dinamico y esta guardado en temp, se borra.
                    If (Form.Path = String.Empty) Then
                        If File.Exists(filepath) Then
                            File.Delete(filepath)
                        End If
                    End If

                    If (Me.ListBox1.Items.Count > 1) Then
                        Me.ListBox1.SelectedIndex = Int32.Parse((selectedIndex - 1).ToString)
                    Else
                        Me.ListBox1.SelectedIndex = 0
                    End If

                    Me.cancelSelectEvent = False

                    If (IsNothing(Me.ListBox1.SelectedItem)) Then
                        Me.ListBox1.SelectedItem = Me.ListBox1.Items(0)
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

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
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

        Try
            ErrorProvider1.Clear()
            ' [Tomas]   25/02/09    Compruebo que se haya ingresado un path de formulario,
            '                       en caso no se haya ingresado edito un form virtual.
            If (Me.ListBox1.SelectedIndex <> -1) Then

                If (Me.txtpath.Text.Trim <> "") Then

                    If (IsValidForm("edit")) Then
                        UpdateZWebForm()
                        '[sebastian 06-03-09] Re load the list of forms to see the last changes
                        LoadExistingForms(True)

                    End If

                Else

                    If (IsValidVirtualForm("edit")) Then

                        'Muestro el fomrulario para editar
                        Dim doctypeid As Int64 = CLng(cbodoctype.SelectedValue())
                        Dim formid As Int64 = CLng(Me.ListBox1.SelectedValue)

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
                            UserBusiness.Rights.SaveAction(CLng(cbodoctype.SelectedValue()), ObjectTypes.FormulariosElectronicos, Zamba.Core.RightsType.Edit, "Se Agrego Formulario: " + txtname.Text)
                            cancelSelectEvent = True
                            LoadExistingForms(False)
                            cancelSelectEvent = False

                            Me.ListBox1.SelectedValue = Int32.Parse(formid.ToString)

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
        Dim selectedIndex As Int32 = CInt(Me.ListBox1.SelectedIndex)
        Dim selecttesform As ZwebForm = DirectCast(Me.ZWebforms(Me.ListBox1.SelectedIndex), ZwebForm)
        Try
            selecttesform.Name = Me.txtname.Text.Trim
            selecttesform.Description = ""
            selecttesform.Type = DirectCast(Me.cbotype.SelectedIndex, FormTypes)
            selecttesform.Path = Me.txtpath.Text.Trim
            selecttesform.DocTypeId = Int32.Parse(DirectCast(Me.cbodoctype.SelectedItem, DocType).ID.ToString)
            selecttesform.useRuleRights = chkUseRuleRights.Checked
            selecttesform.Extensions.Clear()
            FormBusiness.UpdateForm(selecttesform)

            'Tomas 03/03/09:    Muestra el item recién agregado
            Me.ListBox1.SelectedIndex = selectedIndex
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Eventos"
    Private Sub Btnexplore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnexplore.Click
        Try
            Dim Dlg As New OpenFileDialog
            Dlg.ShowDialog()
            Me.txtpath.Text = Dlg.FileName
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            FormBrowser.Navigate(Me.txtpath.Text.Trim)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        Del_Form()
    End Sub
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        Edit_Form()
    End Sub

    Private Function CheckIsAsigned(ByVal Id As Int32) As Boolean
        Dim i As Int32
        For i = 0 To Me.ZWFWebforms.Length - 1
            If Me.ZWFWebforms(i).ID = Id Then
                Return True
            End If
        Next
        Return False
    End Function
#End Region

    ''' <summary>
    ''' Compara el nombre de un formulario con los items del ListBox1
    ''' </summary>
    ''' <history>
    ''' [Tomas 11/03/09]    Created
    ''' </history>
    Private Function DoesNameFormExists(ByVal formName As String) As Boolean

        ' Comparo si el string ingresado es igual al nombre del formulario del ListBox1
        For Each frmName As Zamba.Core.ZwebForm In ListBox1.Items
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
    Private Sub BtnEditHtml_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEditHtml.Click
        Try
            If Me.ListBox1.SelectedIndex <> -1 Then
                If IsValidForm("edit") Then
                    Dim selecttesform As ZwebForm = DirectCast(Me.ZWebforms(Me.ListBox1.SelectedIndex), ZwebForm)

                    Try
                        Dim docType As DocType = DocTypesBusiness.GetDocType(DirectCast(Me.cbodoctype.SelectedItem, IDocType).ID, False)
                        docType.Indexs = ZCore.FilterIndex(Int32.Parse(docType.ID.ToString))

                        'Dim HtmlEditor As New Controller.FormsEditor(selecttesform, docType)
                        'RemoveHandler BtnEditHtml.Click, AddressOf BtnEditHtml_Click
                        'HtmlEditor.Show()
                        'AddHandler BtnEditHtml.Click, AddressOf BtnEditHtml_Click

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
    Private Sub btnNewFormHtml_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewFormHtml.Click
        Try
            Try
                Dim docType As DocType = DocTypesBusiness.GetDocType(DirectCast(Me.cbodoctype.SelectedItem, IDocType).ID, False)
                docType.Indexs = ZCore.FilterIndex(Int32.Parse(docType.ID.ToString))

                'Dim HtmlEditor As New Controller.FormsEditor(docType)
                'RemoveHandler btnNewFormHtml.Click, AddressOf btnNewFormHtml_Click
                'HtmlEditor.Show()
                'AddHandler btnNewFormHtml.Click, AddressOf btnNewFormHtml_Click

                'txtpath.Text = HtmlEditor.FilePath
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
    Private Sub btnReplicate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReplicate.Click

        ErrorProvider1.Clear()

        Dim selectedformid As Int64 = Int32.Parse(Me.ListBox1.SelectedValue.ToString)
        Dim form As ZwebForm = FormBusiness.GetForm(selectedformid)

        Dim types As New Generic.List(Of String)
        'Me.GetFormTypes(types)
        FormsControl.GetFormTypes(types)

        Dim frmrequesttype As New frmRequestFormType(types, form.Type.ToString)

        If frmrequesttype.ShowDialog() = DialogResult.OK Then

            Dim newformid As Int64 = ToolsBusiness.GetNewID(IdTypes.ZWEBFORM)

            If (form.Path = String.Empty) Then

                Dim state As New DynamicFormState(form.DocTypeId, True, selectedformid)
                FormBusiness.GetDynamicFormState(state)
                state.UpdateFormId(newformid)

                Dim typestr As String = String.Empty

                Select Case frmrequesttype.SelectedType
                    Case "Search"
                        typestr = "0"
                    Case "Edit"
                        typestr = "1"
                    Case "Show"
                        typestr = "2"
                    Case "Workflow"
                        typestr = "3"
                    Case "Insert"
                        typestr = "4"
                End Select

                AddZWebForm(state.Formid, form.Name, form.Description, typestr, form.Path, form.DocTypeId, state)

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
    Private Shared Sub GetFormTypes(ByRef types As Generic.List(Of String))
        types.Add(FormTypes.Search.ToString)
        types.Add(FormTypes.Edit.ToString)
        types.Add(FormTypes.Show.ToString)
        types.Add(FormTypes.WorkFlow.ToString)
        types.Add(FormTypes.Insert.ToString)
    End Sub

    ''' <summary>
    ''' Método que sirve para actualizar los datos presentes en el formulario dinámico seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	06/05/2009	Created     Código original del evento "ListBox1_SelectedIndexChanged"
    ''' [Sebastian] 05/06/2009 Modified se realizo cast para salvar warnings
    ''' </history>
    Private Sub updateFormData()

        If Not (IsNothing(ListBox1.SelectedItem)) Then

            'guarda el selectedIndex
            Me.selectedIndex = ListBox1.SelectedIndex

            Me.txtpath.Text = CType(Me.ListBox1.SelectedItem, ZwebForm).Path
            'LE ASIGNA EL NOMBRE DEL FORMULARIO AL TEXTBOX DE NOMBRE
            Me.txtname.Text = CType(Me.ListBox1.SelectedItem, ZwebForm).Name
            'LE SACO EL TIPO QUE PREVIAMENTE SE HABIA CONCATENADO AL NOMBRE
            Me.txtname.Text = txtname.Text.Remove(txtname.Text.LastIndexOf("-") - 1, (txtname.Text.Length - txtname.Text.LastIndexOf("-")) + 1)

            '' Después de crear el formulario el combobox se modifica, y no deberí
            'If (bnCreation = False) Then
            Me.cbotype.SelectedIndex = CType(Me.ListBox1.SelectedItem, ZwebForm).Type
            'End If

            Me.chkUseRuleRights.Checked = CType(Me.ListBox1.SelectedItem, ZwebForm).useRuleRights

            For i As Int32 = 0 To Me.cbodoctype.Items.Count - 1
                Me.cbodoctype.SelectedIndex = i
                If Int32.Parse(Me.cbodoctype.SelectedValue.ToString) = CType(Me.ListBox1.SelectedItem, ZwebForm).DocTypeId Then Exit For
            Next

            FormBrowser.Navigate(Me.txtpath.Text.Trim(), CType(Me.ListBox1.SelectedItem, ZwebForm).ID, txtname.Text)

        End If


    End Sub

    Private Sub txtname_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtname.Leave
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtpath_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpath.Leave
        ErrorProvider1.Clear()
    End Sub
End Class
