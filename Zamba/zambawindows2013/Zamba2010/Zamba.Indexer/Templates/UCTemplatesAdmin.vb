Imports Zamba.Core

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.ClientControls
''' Class	 : UCTemplatesAdmin
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase que permite al usuario interactuar con los templates 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
'''     [Gaston]	22/04/2008 29/04/2008   Modified
''' </history>
''' -----------------------------------------------------------------------------

Public Class UCTemplatesAdmin
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

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
    Friend WithEvents lstTemplates As ListBox
    Friend WithEvents ContextMenu1 As ContextMenu
    Friend WithEvents mnuEdit As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDelete As System.Windows.Forms.MenuItem
    Friend WithEvents Panel2 As ZPanel
    Friend WithEvents btnDelete As ZButton
    Friend WithEvents btnEdit As ZButton
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents btnAdd As ZButton
    Friend WithEvents txtName As TextBox
    Friend WithEvents txtPath As TextBox
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Panel3 As ZPanel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents ZButton1 As ZButton
    Friend WithEvents RadSplitContainer1 As Telerik.WinControls.UI.RadSplitContainer
    Friend WithEvents RadSplitContainer2 As Telerik.WinControls.UI.RadSplitContainer
    Friend WithEvents SplitPanel1 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents SplitPanel2 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents txtDescription As TextBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        lstTemplates = New ListBox()
        ContextMenu1 = New ContextMenu()
        mnuEdit = New System.Windows.Forms.MenuItem()
        mnuDelete = New System.Windows.Forms.MenuItem()
        Panel2 = New ZPanel()
        ZButton1 = New ZButton()
        Label3 = New ZLabel()
        btnEdit = New ZButton()
        btnAdd = New ZButton()
        btnDelete = New ZButton()
        txtDescription = New TextBox()
        Label2 = New ZLabel()
        txtName = New TextBox()
        txtPath = New TextBox()
        Label1 = New ZLabel()
        Label5 = New ZLabel()
        Panel3 = New ZPanel()
        RadSplitContainer1 = New Telerik.WinControls.UI.RadSplitContainer()
        RadSplitContainer2 = New Telerik.WinControls.UI.RadSplitContainer()
        SplitPanel1 = New Telerik.WinControls.UI.SplitPanel()
        SplitPanel2 = New Telerik.WinControls.UI.SplitPanel()
        Panel2.SuspendLayout()
        CType(RadSplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        RadSplitContainer1.SuspendLayout()
        CType(RadSplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        RadSplitContainer2.SuspendLayout()
        CType(SplitPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        SplitPanel1.SuspendLayout()
        CType(SplitPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        SplitPanel2.SuspendLayout()
        SuspendLayout()
        '
        'lstTemplates
        '
        lstTemplates.BackColor = System.Drawing.Color.White
        lstTemplates.ContextMenu = ContextMenu1
        lstTemplates.Dock = System.Windows.Forms.DockStyle.Fill
        lstTemplates.ItemHeight = 16
        lstTemplates.Location = New System.Drawing.Point(0, 181)
        lstTemplates.Name = "lstTemplates"
        lstTemplates.Size = New System.Drawing.Size(1034, 154)
        lstTemplates.TabIndex = 10
        '
        'ContextMenu1
        '
        ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {mnuEdit, mnuDelete})
        '
        'mnuEdit
        '
        mnuEdit.Index = 0
        mnuEdit.Text = "Actualizar"
        '
        'mnuDelete
        '
        mnuDelete.Index = 1
        mnuDelete.Text = "Eliminar"
        '
        'Panel2
        '
        Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Panel2.Controls.Add(ZButton1)
        Panel2.Controls.Add(Label3)
        Panel2.Controls.Add(btnEdit)
        Panel2.Controls.Add(btnAdd)
        Panel2.Controls.Add(btnDelete)
        Panel2.Controls.Add(txtDescription)
        Panel2.Controls.Add(Label2)
        Panel2.Controls.Add(txtName)
        Panel2.Controls.Add(txtPath)
        Panel2.Controls.Add(Label1)
        Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Panel2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Panel2.Location = New System.Drawing.Point(0, 30)
        Panel2.Name = "Panel2"
        Panel2.Size = New System.Drawing.Size(1034, 151)
        Panel2.TabIndex = 6
        '
        'ZButton1
        '
        ZButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        ZButton1.FlatStyle = FlatStyle.Flat
        ZButton1.ForeColor = System.Drawing.Color.White
        ZButton1.Location = New System.Drawing.Point(749, 11)
        ZButton1.Name = "ZButton1"
        ZButton1.Size = New System.Drawing.Size(53, 24)
        ZButton1.TabIndex = 11
        ZButton1.Text = "..."
        ZButton1.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!)
        Label3.FontSize = 9.75!
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(7, 85)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(88, 20)
        Label3.TabIndex = 5
        Label3.Text = "Descripción"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'btnEdit
        '
        btnEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnEdit.FlatStyle = FlatStyle.Flat
        btnEdit.ForeColor = System.Drawing.Color.White
        btnEdit.Location = New System.Drawing.Point(207, 113)
        btnEdit.Name = "btnEdit"
        btnEdit.Size = New System.Drawing.Size(74, 33)
        btnEdit.TabIndex = 8
        btnEdit.Text = "Editar"
        btnEdit.UseVisualStyleBackColor = False
        '
        'btnAdd
        '
        btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.ForeColor = System.Drawing.Color.White
        btnAdd.Location = New System.Drawing.Point(114, 113)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New System.Drawing.Size(74, 33)
        btnAdd.TabIndex = 7
        btnAdd.Text = "Agregar"
        btnAdd.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.ForeColor = System.Drawing.Color.White
        btnDelete.Location = New System.Drawing.Point(304, 113)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New System.Drawing.Size(74, 33)
        btnDelete.TabIndex = 9
        btnDelete.Text = "Eliminar"
        btnDelete.UseVisualStyleBackColor = False
        '
        'txtDescription
        '
        txtDescription.BackColor = System.Drawing.Color.WhiteSmoke
        txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtDescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        txtDescription.Location = New System.Drawing.Point(95, 83)
        txtDescription.Name = "txtDescription"
        txtDescription.ReadOnly = True
        txtDescription.Size = New System.Drawing.Size(648, 23)
        txtDescription.TabIndex = 6
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!)
        Label2.FontSize = 9.75!
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(8, 50)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(81, 20)
        Label2.TabIndex = 3
        Label2.Text = "Nombre"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtName
        '
        txtName.BackColor = System.Drawing.Color.WhiteSmoke
        txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        txtName.Location = New System.Drawing.Point(95, 47)
        txtName.Name = "txtName"
        txtName.ReadOnly = True
        txtName.Size = New System.Drawing.Size(648, 23)
        txtName.TabIndex = 4
        '
        'txtPath
        '
        txtPath.BackColor = System.Drawing.Color.WhiteSmoke
        txtPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        txtPath.Location = New System.Drawing.Point(95, 12)
        txtPath.Name = "txtPath"
        txtPath.ReadOnly = True
        txtPath.Size = New System.Drawing.Size(648, 23)
        txtPath.TabIndex = 1
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!)
        Label1.FontSize = 9.75!
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(8, 15)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(71, 16)
        Label1.TabIndex = 0
        Label1.Text = "Plantilla"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Dock = System.Windows.Forms.DockStyle.Top
        Label5.Font = New Font("Verdana", 11.0!)
        Label5.FontSize = 11.0!
        Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label5.Location = New System.Drawing.Point(0, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(1034, 30)
        Label5.TabIndex = 10
        Label5.Text = "Plantillas"
        Label5.TextAlign = ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Panel3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Panel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Panel3.Location = New System.Drawing.Point(0, 0)
        Panel3.Name = "Panel3"
        Panel3.Size = New System.Drawing.Size(1034, 232)
        Panel3.TabIndex = 9
        '
        'RadSplitContainer1
        '
        RadSplitContainer1.Controls.Add(RadSplitContainer2)
        RadSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        RadSplitContainer1.Location = New System.Drawing.Point(0, 0)
        RadSplitContainer1.Name = "RadSplitContainer1"
        '
        '
        '
        RadSplitContainer1.RootElement.MinSize = New System.Drawing.Size(25, 25)
        RadSplitContainer1.Size = New System.Drawing.Size(1034, 571)
        RadSplitContainer1.TabIndex = 10
        RadSplitContainer1.TabStop = False
        RadSplitContainer1.Text = "RadSplitContainer1"
        '
        'RadSplitContainer2
        '
        RadSplitContainer2.Controls.Add(SplitPanel1)
        RadSplitContainer2.Controls.Add(SplitPanel2)
        RadSplitContainer2.Location = New System.Drawing.Point(0, 0)
        RadSplitContainer2.Name = "RadSplitContainer2"
        RadSplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        '
        '
        RadSplitContainer2.RootElement.MinSize = New System.Drawing.Size(25, 25)
        RadSplitContainer2.Size = New System.Drawing.Size(1034, 571)
        RadSplitContainer2.TabIndex = 0
        RadSplitContainer2.TabStop = False
        RadSplitContainer2.Text = "RadSplitContainer2"
        '
        'SplitPanel1
        '
        SplitPanel1.Controls.Add(lstTemplates)
        SplitPanel1.Controls.Add(Panel2)
        SplitPanel1.Controls.Add(Label5)
        SplitPanel1.Location = New System.Drawing.Point(0, 0)
        SplitPanel1.Name = "SplitPanel1"
        '
        '
        '
        SplitPanel1.RootElement.MinSize = New System.Drawing.Size(25, 25)
        SplitPanel1.Size = New System.Drawing.Size(1034, 335)
        SplitPanel1.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0!, 0.09082893!)
        SplitPanel1.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, 2)
        SplitPanel1.TabIndex = 0
        SplitPanel1.TabStop = False
        SplitPanel1.Text = "SplitPanel1"
        '
        'SplitPanel2
        '
        SplitPanel2.Controls.Add(Panel3)
        SplitPanel2.Location = New System.Drawing.Point(0, 339)
        SplitPanel2.Name = "SplitPanel2"
        '
        '
        '
        SplitPanel2.RootElement.MinSize = New System.Drawing.Size(25, 25)
        SplitPanel2.Size = New System.Drawing.Size(1034, 232)
        SplitPanel2.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0!, -0.09082893!)
        SplitPanel2.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, -2)
        SplitPanel2.TabIndex = 1
        SplitPanel2.TabStop = False
        SplitPanel2.Text = "SplitPanel2"
        '
        'UCTemplatesAdmin
        '
        Controls.Add(RadSplitContainer1)
        Name = "UCTemplatesAdmin"
        Size = New System.Drawing.Size(1034, 571)
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(RadSplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        RadSplitContainer1.ResumeLayout(False)
        CType(RadSplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        RadSplitContainer2.ResumeLayout(False)
        CType(SplitPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        SplitPanel1.ResumeLayout(False)
        CType(SplitPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        SplitPanel2.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

#Region "Atributos"

    Dim DsTemplates As New DataSet
    Dim Browser As New Zamba.Browser.WebBrowser

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta cuando se carga el control de usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub UcTemplatesAdmin_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

        loadBrowser()
        loadExistingTemplates()

        If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Templates, RightsType.Create) Then
            btnAdd.Enabled = True
        Else
            btnAdd.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Buscar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExplore_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ZButton1.Click

        Try

            Dim Dlg As New OpenFileDialog
            Dlg.ShowDialog()
            txtPath.Text = Dlg.FileName

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            Browser.ShowDocument(txtPath.Text.Trim)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAdd.Click
        addTemplate()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Actualizar o el botón Eliminar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEdit.Click, btnDelete.Click, mnuEdit.Click,
    mnuDelete.Click

        If ((lstTemplates.SelectedItem IsNot Nothing) Or (sender.GetType.ToString() = "System.Windows.Forms.MenuItem")) Then

            Select Case CType(sender.Text, String)
                Case "Editar"
                    editTemplate()
                Case "Eliminar"
                    deleteTemplate()
            End Select

        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se selecciona un elemento en la lista que muestra los templates
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub lstTemplates_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTemplates.SelectedIndexChanged

    '    Try

    '        If (lstTemplates.SelectedItem IsNot Nothing) Then
    '            updateTextBoxsAndBrowser()
    '        End If

    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try

    'End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Método que sirve para agregar el visualizador de documentos a uno de los paneles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub loadBrowser()

        Try
            Browser.Dock = DockStyle.Fill
            Panel3.Controls.Add(Browser)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Método que carga los templates que existen en la base de datos en la lista de templates
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub loadExistingTemplates()

        Try

            DsTemplates = TemplatesBusiness.GetTemplates()
            lstTemplates.Visible = False
            lstTemplates.DisplayMember = "Name"
            lstTemplates.DataSource = DsTemplates.Tables(0)
            lstTemplates.Visible = True

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Function ValidateExtension(ByVal extension As String) As Boolean
        Dim lstExt As String = ".doc,.dotx,.doc,.docx,.pot,.potx,.ppt,.pptx,.xlt,.xltx,.xls,.xlsx,.html,.htm,.xoml"
        Dim anExtension As String
        For Each anExtension In lstExt.Split(",")
            If extension.Equals(anExtension) Then
                Return True
                Exit For
            End If

        Next
        Return False

    End Function

    ''' <summary>
    ''' Método que agrega un nuevo template
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Sub addTemplate()

        Dim frmAddTemplate As New frmAddOrUpdateTemplate()

        If (frmAddTemplate.ShowDialog() = DialogResult.OK) Then

            Try
                Dim indexOfExtension As Int16 = frmAddTemplate.Path.ToString.LastIndexOf(".")
                Dim extension As String = frmAddTemplate.Path.ToString.Substring(indexOfExtension)
                If ValidateExtension(extension) Then
                    ' Se crea una fila con el esquema de la tabla contenida en el DsTemplates
                    Dim row As DataRow = DsTemplates.Tables(0).NewRow
                    ' Se genera un nuevo id para el Template y se lo coloca en la celda Id de la fila
                    row("Id") = FactoryTemplates.GetNewTemplateId()
                    ' Se colocan los datos ingresados en la fila en base a lo ingresado en la ventana
                    row("Name") = frmAddTemplate.Name
                    row("Path") = frmAddTemplate.Path
                    row("Description") = frmAddTemplate.Description
                    row("Type") = 1

                    txtPath.Text = frmAddTemplate.Path
                    ' Se agrega la fila al DsTemplates
                    DsTemplates.Tables(0).Rows.Add(row)
                    ' El elemento actualmente seleccionado en lstTemplates pasa a ser el nuevo elemento que se agrego en el Dataset
                    lstTemplates.SelectedIndex = lstTemplates.Items.Count - 1
                    TemplatesBusiness.SaveTemplates(DsTemplates)
                    ' Se actualiza la lista que muestra los templates para mostrar el nuevo cambio
                    lstTemplates.Update()

                    ' Si cantidad de elementos que muestra la lista de templates es uno, entonces mostrar el contenido y datos del único template que
                    ' hay en la lista. Esto se realiza porque el evento selectedIndexChanged del lstTemplates no se ejecuta, y al no ejecutarse no
                    ' muestra el contenido, ni los datos del nuevo y único template que se agrego
                    If (lstTemplates.Items.Count = 1) Then
                        updateTextBoxs()
                        'me.doBrowser()
                    End If

                    ' Se actualiza el U_TIME (fecha y hora de la última acción realizada por el cliente) y se registra la acción hecha por el cliente
                    UserBusiness.Rights.SaveAction(CLng(row("Id")), ObjectTypes.Archivos, RightsType.agregarPlantilla, String.Empty)

                    UserBusiness.Rights.SetRight(Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.Templates, RightsType.Edit, CLng(row("Id")))
                    UserBusiness.Rights.SetRight(Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.Templates, RightsType.Use, CLng(row("Id")))
                    UserBusiness.Rights.SetRight(Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.Templates, RightsType.Delete, CLng(row("Id")))
                Else
                    MessageBox.Show("No esta permitida la extensión como plantilla", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End If

    End Sub

    ''' <summary>
    ''' Método que actualiza un template
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub editTemplate()

        Dim frmUpdateTemplate As New frmAddOrUpdateTemplate(txtPath.Text, txtName.Text, txtDescription.Text)

        If (frmUpdateTemplate.ShowDialog() = DialogResult.OK) Then

            Try

                ' Se modifica la fila que se corresponde con el template seleccionado en la lista
                DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Item("Name") = frmUpdateTemplate.Name
                DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Item("Path") = frmUpdateTemplate.Path
                DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Item("Description") = frmUpdateTemplate.Description
                ' Se actualiza la tabla donde están los templates
                TemplatesBusiness.SaveTemplates(DsTemplates)
                txtName.Text = frmUpdateTemplate.Name
                txtPath.Text = frmUpdateTemplate.Path
                txtDescription.Text = frmUpdateTemplate.Description
                lstTemplates.Update()

                ' Se actualiza el U_TIME (fecha y hora de la última acción realizada por el cliente) y se registra la acción hecha por el cliente
                UserBusiness.Rights.SaveAction(CInt(DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Item("Id")), ObjectTypes.Archivos, RightsType.actualizarPlantilla, String.Empty)

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End If

    End Sub

    ''' <summary>
    ''' Método que elimina un template
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub deleteTemplate()

        If (MessageBox.Show("¿Está seguro que desea eliminar la plantilla?", "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then

            Try

                ' Se elimina el template de la base de datos
                Dim idTemplate As Integer = CInt(DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Item("Id"))
                TemplatesBusiness.DeleteTemplate(idTemplate)
                ' La fila que corresponde al template seleccionado del DsTemplates queda vacía
                DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Delete()
                DsTemplates.AcceptChanges()
                lstTemplates.Update()

                ' Si la lista que muestra los templates ya no tiene ningún elemento entonces se colocan las cajas de texto en blanco, al igual que el 
                ' browser que no debe mostrar más nada 
                '(se realiza esto porque sino las cajas de texto mostrarían los datos del último template que hubiera quedado en la lista y que
                ' se elimino, al igual que el browser, que mostraria el contenido del documento)
                If (lstTemplates.Items.Count = 0) Then

                    txtPath.Text = ""
                    txtName.Text = ""
                    txtDescription.Text = ""

                    Browser.CloseWebBrowser(True)

                    ' de lo contrario, actualizar las cajas de texto y el visualizador con el contenido del anterior elemento
                Else
                    updateTextBoxs()
                    'me.doBrowser()
                End If

                ' Se actualiza el U_TIME (fecha y hora de la última acción realizada por el cliente) y se registra la acción hecha por el cliente
                UserBusiness.Rights.SaveAction(idTemplate, ObjectTypes.Archivos, RightsType.eliminarPlantilla, String.Empty)

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End If

    End Sub

    ''' <summary>
    ''' Método utilizado para actualizar las cajas de texto y el visualizador de documentos (el browser) con los datos del nuevo template que se
    ''' agrego o que se modifico
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	02/05/2008  Created
    ''' </history>
    ''' 

    Private Sub doBrowser()
        Browser.ShowDocument(DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Item("path"))
    End Sub

    Private Sub updateTextBoxs()
        txtPath.Text = DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Item("path").ToString
        txtName.Text = DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Item("name").ToString
        txtDescription.Text = DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Item("description").ToString

        Dim IdTemplate As Int64 = DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Item("id").ToString

        If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Templates, RightsType.Delete, IdTemplate) Then
            btnDelete.Enabled = True
        Else
            btnDelete.Enabled = False
        End If

        If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Templates, RightsType.Edit, IdTemplate) Then
            btnEdit.Enabled = True
        Else
            btnEdit.Enabled = False
        End If
    End Sub

    Private Sub btnExaminar_Click(sender As Object, e As EventArgs)
        Dim OpenFileDialog1 As New OpenFileDialog

        Try
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim indexOfExtension As Int16 = OpenFileDialog1.FileName.ToString.LastIndexOf(".")
                Dim extension As String = OpenFileDialog1.FileName.ToString.Substring(indexOfExtension)
                txtName.Text = String.Empty
                txtDescription.Text = String.Empty
                If ValidateExtension(extension) Then
                    txtPath.Text = OpenFileDialog1.FileName
                Else
                    MessageBox.Show("No se encontró la Plantilla", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub lstTemplates_DoubleClick(sender As Object, e As EventArgs) Handles lstTemplates.DoubleClick
        Try
            If (lstTemplates.SelectedItem IsNot Nothing) Then
                updateTextBoxs()
                doBrowser()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub lstTemplates_Click(sender As Object, e As EventArgs) Handles lstTemplates.Click
        Try
            If (lstTemplates.SelectedItem IsNot Nothing) Then
                updateTextBoxs()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub lstTemplates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTemplates.SelectedIndexChanged
        If (lstTemplates.SelectedItem IsNot Nothing) Then
            updateTextBoxs()
        End If
    End Sub



#End Region

End Class

