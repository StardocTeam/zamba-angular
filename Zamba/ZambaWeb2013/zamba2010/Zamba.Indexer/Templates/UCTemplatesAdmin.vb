Imports Zamba.AppBlock
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Browser
Imports System.Windows.Forms

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

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents Panel1 As ZBluePanel
    Friend WithEvents lstTemplates As System.Windows.Forms.ListBox
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuEdit As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDelete As System.Windows.Forms.MenuItem
    Friend WithEvents Panel2 As ZBluePanel
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As ZSplitter
    Friend WithEvents Panel3 As ZBluePanel
    Friend WithEvents Splitter1 As ZSplitter
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New Zamba.AppBlock.ZBluePanel
        Me.lstTemplates = New System.Windows.Forms.ListBox
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.mnuEdit = New System.Windows.Forms.MenuItem
        Me.mnuDelete = New System.Windows.Forms.MenuItem
        Me.Panel2 = New Zamba.AppBlock.ZBluePanel
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnEdit = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnAdd = New System.Windows.Forms.Button
        Me.txtName = New System.Windows.Forms.TextBox
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Splitter2 = New Zamba.AppBlock.ZSplitter
        Me.Panel3 = New Zamba.AppBlock.ZBluePanel
        Me.Splitter1 = New Zamba.AppBlock.ZSplitter
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel1.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel1.Controls.Add(Me.lstTemplates)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(283, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(389, 28)
        Me.Panel1.TabIndex = 5
        '
        'lstTemplates
        '
        Me.lstTemplates.BackColor = System.Drawing.Color.White
        Me.lstTemplates.ContextMenu = Me.ContextMenu1
        Me.lstTemplates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstTemplates.Location = New System.Drawing.Point(0, 0)
        Me.lstTemplates.Name = "lstTemplates"
        Me.lstTemplates.Size = New System.Drawing.Size(389, 17)
        Me.lstTemplates.TabIndex = 10
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuEdit, Me.mnuDelete})
        '
        'mnuEdit
        '
        Me.mnuEdit.Index = 0
        Me.mnuEdit.Text = "Actualizar"
        '
        'mnuDelete
        '
        Me.mnuDelete.Index = 1
        Me.mnuDelete.Text = "Eliminar"
        '
        'Panel2
        '
        Me.Panel2.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel2.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.txtDescription)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.btnEdit)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.btnAdd)
        Me.Panel2.Controls.Add(Me.txtName)
        Me.Panel2.Controls.Add(Me.txtPath)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.ForeColor = System.Drawing.Color.Black
        Me.Panel2.Location = New System.Drawing.Point(3, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(280, 405)
        Me.Panel2.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(8, 162)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Descripcion"
        '
        'txtDescription
        '
        Me.txtDescription.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(8, 186)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReadOnly = True
        Me.txtDescription.Size = New System.Drawing.Size(192, 21)
        Me.txtDescription.TabIndex = 6
        '
        'btnDelete
        '
        Me.btnDelete.ForeColor = System.Drawing.Color.Black
        Me.btnDelete.Location = New System.Drawing.Point(136, 226)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(64, 24)
        Me.btnDelete.TabIndex = 9
        Me.btnDelete.Text = "Eliminar"
        '
        'btnEdit
        '
        Me.btnEdit.ForeColor = System.Drawing.Color.Black
        Me.btnEdit.Location = New System.Drawing.Point(72, 226)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(64, 24)
        Me.btnEdit.TabIndex = 8
        Me.btnEdit.Text = "Actualizar"
        '
        'Label5
        '
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(280, 32)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Templates"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(8, 106)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Nombre"
        '
        'btnAdd
        '
        Me.btnAdd.ForeColor = System.Drawing.Color.Black
        Me.btnAdd.Location = New System.Drawing.Point(8, 226)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(64, 24)
        Me.btnAdd.TabIndex = 7
        Me.btnAdd.Text = "Agregar"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtName.ForeColor = System.Drawing.Color.Black
        Me.txtName.Location = New System.Drawing.Point(8, 130)
        Me.txtName.Name = "txtName"
        Me.txtName.ReadOnly = True
        Me.txtName.Size = New System.Drawing.Size(192, 21)
        Me.txtName.TabIndex = 4
        '
        'txtPath
        '
        Me.txtPath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtPath.ForeColor = System.Drawing.Color.Black
        Me.txtPath.Location = New System.Drawing.Point(8, 72)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.ReadOnly = True
        Me.txtPath.Size = New System.Drawing.Size(248, 21)
        Me.txtPath.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(160, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Template"
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(182, Byte), Integer), CType(CType(9, Byte), Integer))
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter2.Location = New System.Drawing.Point(3, 405)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(669, 3)
        Me.Splitter2.TabIndex = 8
        Me.Splitter2.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel3.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(283, 28)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(389, 377)
        Me.Panel3.TabIndex = 9
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(182, Byte), Integer), CType(CType(9, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 408)
        Me.Splitter1.TabIndex = 7
        Me.Splitter1.TabStop = False
        '
        'UCTemplatesAdmin
        '
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "UCTemplatesAdmin"
        Me.Size = New System.Drawing.Size(672, 408)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

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
    Private Sub UcTemplatesAdmin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        loadBrowser()
        loadExistingTemplates()

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Buscar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExplore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try

            Dim Dlg As New OpenFileDialog
            Dlg.ShowDialog()
            Me.txtPath.Text = Dlg.FileName

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            Browser.ShowDocument(Me.txtPath.Text.Trim)
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
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        addTemplate()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Actualizar o el botón Eliminar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click, btnDelete.Click, mnuEdit.Click, _
    mnuDelete.Click

        If ((lstTemplates.SelectedItem IsNot Nothing) Or (sender.GetType.ToString() = "System.Windows.Forms.MenuItem")) Then

            Select Case CType(sender.Text, String)
                Case "Actualizar"
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
    Private Sub lstTemplates_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTemplates.SelectedIndexChanged

        Try

            If (lstTemplates.SelectedItem IsNot Nothing) Then
                updateTextBoxsAndBrowser()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Método que sirve para agregar el visualizador de documentos a uno de los paneles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub loadBrowser()

        Try
            Browser.Dock = DockStyle.Fill
            Me.Panel3.Controls.Add(Browser)
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

            Me.DsTemplates = TemplatesBusiness.GetTemplates()
            Me.lstTemplates.Visible = False
            Me.lstTemplates.DisplayMember = "Name"
            Me.lstTemplates.DataSource = Me.DsTemplates.Tables(0)
            Me.lstTemplates.Visible = True

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Método que agrega un nuevo template
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub addTemplate()

        Dim frmAddTemplate As New frmAddOrUpdateTemplate()

        If (frmAddTemplate.ShowDialog() = DialogResult.OK) Then

            Try

                ' Se crea una fila con el esquema de la tabla contenida en el DsTemplates
                Dim row As DataRow = Me.DsTemplates.Tables(0).NewRow
                ' Se genera un nuevo id para el Template y se lo coloca en la celda Id de la fila
                row("Id") = FactoryTemplates.GetNewTemplateId()
                ' Se colocan los datos ingresados en la fila en base a lo ingresado en la ventana
                row("Name") = frmAddTemplate.Name
                row("Path") = frmAddTemplate.Path
                row("Description") = frmAddTemplate.Description
                row("Type") = 1
                ' Se agrega la fila al DsTemplates
                Me.DsTemplates.Tables(0).Rows.Add(row)
                ' El elemento actualmente seleccionado en lstTemplates pasa a ser el nuevo elemento que se agrego en el Dataset
                Me.lstTemplates.SelectedIndex = Me.lstTemplates.Items.Count - 1
                TemplatesBusiness.SaveTemplates(DsTemplates)
                ' Se actualiza la lista que muestra los templates para mostrar el nuevo cambio
                Me.lstTemplates.Update()

                ' Si cantidad de elementos que muestra la lista de templates es uno, entonces mostrar el contenido y datos del único template que
                ' hay en la lista. Esto se realiza porque el evento selectedIndexChanged del lstTemplates no se ejecuta, y al no ejecutarse no
                ' muestra el contenido, ni los datos del nuevo y único template que se agrego
                If (lstTemplates.Items.Count = 1) Then
                    updateTextBoxsAndBrowser()
                End If

                ' Se actualiza el U_TIME (fecha y hora de la última acción realizada por el cliente) y se registra la acción hecha por el cliente
                UserBusiness.Rights.SaveAction(CLng(row("Id")), ObjectTypes.ModulePlantillas, RightsType.agregarPlantilla)

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

        Dim frmUpdateTemplate As New frmAddOrUpdateTemplate(Me.txtPath.Text, Me.txtName.Text, Me.txtDescription.Text)

        If (frmUpdateTemplate.ShowDialog() = DialogResult.OK) Then

            Try

                ' Se modifica la fila que se corresponde con el template seleccionado en la lista
                Me.DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Item("Name") = frmUpdateTemplate.Name
                Me.DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Item("Path") = frmUpdateTemplate.Path
                Me.DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Item("Description") = frmUpdateTemplate.Description
                ' Se actualiza la tabla donde están los templates
                TemplatesBusiness.SaveTemplates(DsTemplates)
                Me.txtName.Text = frmUpdateTemplate.Name
                Me.txtPath.Text = frmUpdateTemplate.Path
                Me.txtDescription.Text = frmUpdateTemplate.Description
                Me.lstTemplates.Update()

                ' Se actualiza el U_TIME (fecha y hora de la última acción realizada por el cliente) y se registra la acción hecha por el cliente
                UserBusiness.Rights.SaveAction(CInt(Me.DsTemplates.Tables(0).Rows(lstTemplates.SelectedIndex).Item("Id")), ObjectTypes.ModulePlantillas, RightsType.actualizarPlantilla)

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
                Dim idTemplate As Integer = CInt(Me.DsTemplates.Tables(0).Rows(Me.lstTemplates.SelectedIndex).Item("Id"))
                TemplatesBusiness.DeleteTemplate(idTemplate)
                ' La fila que corresponde al template seleccionado del DsTemplates queda vacía
                Me.DsTemplates.Tables(0).Rows(Me.lstTemplates.SelectedIndex).Delete()
                Me.DsTemplates.AcceptChanges()
                Me.lstTemplates.Update()

                ' Si la lista que muestra los templates ya no tiene ningún elemento entonces se colocan las cajas de texto en blanco, al igual que el 
                ' browser que no debe mostrar más nada 
                '(se realiza esto porque sino las cajas de texto mostrarían los datos del último template que hubiera quedado en la lista y que
                ' se elimino, al igual que el browser, que mostraria el contenido del documento)
                If (lstTemplates.Items.Count = 0) Then

                    Me.txtPath.Text = ""
                    Me.txtName.Text = ""
                    Me.txtDescription.Text = ""
                    Browser.CloseWebBrowser(True)

                    ' de lo contrario, actualizar las cajas de texto y el visualizador con el contenido del anterior elemento
                Else
                    updateTextBoxsAndBrowser()
                End If

                ' Se actualiza el U_TIME (fecha y hora de la última acción realizada por el cliente) y se registra la acción hecha por el cliente
                UserBusiness.Rights.SaveAction(idTemplate, ObjectTypes.ModulePlantillas, RightsType.eliminarPlantilla)

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
    Private Sub updateTextBoxsAndBrowser()
        Me.txtPath.Text = Me.DsTemplates.Tables(0).Rows(Me.lstTemplates.SelectedIndex).Item("path").ToString
        Me.txtName.Text = Me.DsTemplates.Tables(0).Rows(Me.lstTemplates.SelectedIndex).Item("name").ToString
        Me.txtDescription.Text = Me.DsTemplates.Tables(0).Rows(Me.lstTemplates.SelectedIndex).Item("description").ToString
        Me.Browser.ShowDocument(Me.DsTemplates.Tables(0).Rows(Me.lstTemplates.SelectedIndex).Item("path"))
    End Sub

#End Region

End Class

