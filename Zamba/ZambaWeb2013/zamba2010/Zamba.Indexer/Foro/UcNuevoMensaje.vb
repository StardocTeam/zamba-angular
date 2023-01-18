Imports Zamba.Core
Imports Zamba.AppBlock
Imports System.Collections.Generic

Public Class UcNuevoMensaje
    Inherits Zamba.AppBlock.ZControl

#Region " Código generado por el Diseñador de Windows Forms "



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
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Public WithEvents txtMensaje As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtAsunto As System.Windows.Forms.TextBox
    Friend WithEvents btnUsuarios As System.Windows.Forms.Button
    Friend WithEvents btnNotificar As System.Windows.Forms.Button
    Friend WithEvents grpAttachs As System.Windows.Forms.GroupBox
    Friend WithEvents btnRemoveAttach As System.Windows.Forms.Button
    Friend WithEvents btnAddAtach As System.Windows.Forms.Button
    Friend WithEvents lstAttachs As System.Windows.Forms.ListBox
    Friend WithEvents btnRemoveAll As System.Windows.Forms.Button
    Friend WithEvents chkNotifyCreator As System.Windows.Forms.CheckBox
    Friend WithEvents chkAutomaticSend As System.Windows.Forms.CheckBox
    Friend WithEvents chkAttachLink As System.Windows.Forms.CheckBox
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.pnlButtons = New System.Windows.Forms.Panel
        Me.btnUsuarios = New System.Windows.Forms.Button
        Me.btnNotificar = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnGuardar = New System.Windows.Forms.Button
        Me.txtMensaje = New System.Windows.Forms.TextBox
        Me.txtAsunto = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.grpAttachs = New System.Windows.Forms.GroupBox
        Me.btnRemoveAll = New System.Windows.Forms.Button
        Me.btnRemoveAttach = New System.Windows.Forms.Button
        Me.btnAddAtach = New System.Windows.Forms.Button
        Me.lstAttachs = New System.Windows.Forms.ListBox
        Me.chkNotifyCreator = New System.Windows.Forms.CheckBox
        Me.chkAutomaticSend = New System.Windows.Forms.CheckBox
        Me.chkAttachLink = New System.Windows.Forms.CheckBox
        Me.pnlButtons.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAttachs.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlButtons
        '
        Me.pnlButtons.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlButtons.Controls.Add(Me.btnUsuarios)
        Me.pnlButtons.Controls.Add(Me.btnNotificar)
        Me.pnlButtons.Controls.Add(Me.PictureBox1)
        Me.pnlButtons.Controls.Add(Me.btnGuardar)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlButtons.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlButtons.ForeColor = System.Drawing.Color.DarkBlue
        Me.pnlButtons.Location = New System.Drawing.Point(0, 478)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(386, 34)
        Me.pnlButtons.TabIndex = 9
        '
        'btnUsuarios
        '
        Me.btnUsuarios.BackColor = System.Drawing.Color.AliceBlue
        Me.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnUsuarios.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUsuarios.Location = New System.Drawing.Point(268, 5)
        Me.btnUsuarios.Name = "btnUsuarios"
        Me.btnUsuarios.Size = New System.Drawing.Size(98, 22)
        Me.btnUsuarios.TabIndex = 19
        Me.btnUsuarios.Text = "Participantes"
        Me.btnUsuarios.UseVisualStyleBackColor = False
        '
        'btnNotificar
        '
        Me.btnNotificar.BackColor = System.Drawing.Color.AliceBlue
        Me.btnNotificar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnNotificar.Location = New System.Drawing.Point(110, 5)
        Me.btnNotificar.Name = "btnNotificar"
        Me.btnNotificar.Size = New System.Drawing.Size(152, 22)
        Me.btnNotificar.TabIndex = 18
        Me.btnNotificar.Text = "Guardar / Notificar"
        Me.btnNotificar.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Black
        Me.PictureBox1.Location = New System.Drawing.Point(-2, 33)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(1000, 1)
        Me.PictureBox1.TabIndex = 15
        Me.PictureBox1.TabStop = False
        '
        'btnGuardar
        '
        Me.btnGuardar.BackColor = System.Drawing.Color.AliceBlue
        Me.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnGuardar.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(17, 5)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(87, 22)
        Me.btnGuardar.TabIndex = 2
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'txtMensaje
        '
        Me.txtMensaje.BackColor = System.Drawing.Color.White
        Me.txtMensaje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMensaje.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMensaje.ForeColor = System.Drawing.Color.Navy
        Me.txtMensaje.Location = New System.Drawing.Point(18, 95)
        Me.txtMensaje.MaxLength = 3999
        Me.txtMensaje.Multiline = True
        Me.txtMensaje.Name = "txtMensaje"
        Me.txtMensaje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMensaje.Size = New System.Drawing.Size(349, 131)
        Me.txtMensaje.TabIndex = 1
        '
        'txtAsunto
        '
        Me.txtAsunto.BackColor = System.Drawing.Color.White
        Me.txtAsunto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAsunto.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAsunto.ForeColor = System.Drawing.Color.Navy
        Me.txtAsunto.Location = New System.Drawing.Point(18, 36)
        Me.txtAsunto.Name = "txtAsunto"
        Me.txtAsunto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAsunto.Size = New System.Drawing.Size(349, 23)
        Me.txtAsunto.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label1.Location = New System.Drawing.Point(15, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 14)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "ASUNTO DEL TEMA"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label2.Location = New System.Drawing.Point(15, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 14)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "MENSAJE"
        '
        'grpAttachs
        '
        Me.grpAttachs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpAttachs.Controls.Add(Me.btnRemoveAll)
        Me.grpAttachs.Controls.Add(Me.btnRemoveAttach)
        Me.grpAttachs.Controls.Add(Me.btnAddAtach)
        Me.grpAttachs.Controls.Add(Me.lstAttachs)
        Me.grpAttachs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpAttachs.ForeColor = System.Drawing.Color.MidnightBlue
        Me.grpAttachs.Location = New System.Drawing.Point(18, 243)
        Me.grpAttachs.Name = "grpAttachs"
        Me.grpAttachs.Size = New System.Drawing.Size(349, 162)
        Me.grpAttachs.TabIndex = 25
        Me.grpAttachs.TabStop = False
        Me.grpAttachs.Text = "ADJUNTOS"
        '
        'btnRemoveAll
        '
        Me.btnRemoveAll.Location = New System.Drawing.Point(171, 22)
        Me.btnRemoveAll.Name = "btnRemoveAll"
        Me.btnRemoveAll.Size = New System.Drawing.Size(103, 23)
        Me.btnRemoveAll.TabIndex = 3
        Me.btnRemoveAll.Text = "Remover todos"
        Me.btnRemoveAll.UseVisualStyleBackColor = True
        '
        'btnRemoveAttach
        '
        Me.btnRemoveAttach.Location = New System.Drawing.Point(89, 22)
        Me.btnRemoveAttach.Name = "btnRemoveAttach"
        Me.btnRemoveAttach.Size = New System.Drawing.Size(75, 23)
        Me.btnRemoveAttach.TabIndex = 2
        Me.btnRemoveAttach.Text = "Remover"
        Me.btnRemoveAttach.UseVisualStyleBackColor = True
        '
        'btnAddAtach
        '
        Me.btnAddAtach.Location = New System.Drawing.Point(7, 22)
        Me.btnAddAtach.Name = "btnAddAtach"
        Me.btnAddAtach.Size = New System.Drawing.Size(75, 23)
        Me.btnAddAtach.TabIndex = 1
        Me.btnAddAtach.Text = "Agregar"
        Me.btnAddAtach.UseVisualStyleBackColor = True
        '
        'lstAttachs
        '
        Me.lstAttachs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstAttachs.FormattingEnabled = True
        Me.lstAttachs.HorizontalScrollbar = True
        Me.lstAttachs.ItemHeight = 14
        Me.lstAttachs.Location = New System.Drawing.Point(7, 51)
        Me.lstAttachs.Name = "lstAttachs"
        Me.lstAttachs.Size = New System.Drawing.Size(336, 102)
        Me.lstAttachs.TabIndex = 0
        '
        'chkNotifyCreator
        '
        Me.chkNotifyCreator.AutoSize = True
        Me.chkNotifyCreator.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkNotifyCreator.Location = New System.Drawing.Point(18, 411)
        Me.chkNotifyCreator.Name = "chkNotifyCreator"
        Me.chkNotifyCreator.Size = New System.Drawing.Size(283, 17)
        Me.chkNotifyCreator.TabIndex = 26
        Me.chkNotifyCreator.Text = "Responder unicamente al creador de la conversación."
        Me.chkNotifyCreator.UseVisualStyleBackColor = True
        '
        'chkAutomaticSend
        '
        Me.chkAutomaticSend.AutoSize = True
        Me.chkAutomaticSend.Location = New System.Drawing.Point(18, 434)
        Me.chkAutomaticSend.Name = "chkAutomaticSend"
        Me.chkAutomaticSend.Size = New System.Drawing.Size(144, 17)
        Me.chkAutomaticSend.TabIndex = 27
        Me.chkAutomaticSend.Text = "Envío automatico de mail"
        Me.chkAutomaticSend.UseVisualStyleBackColor = True
        '
        'chkAttachLink
        '
        Me.chkAttachLink.AutoSize = True
        Me.chkAttachLink.Location = New System.Drawing.Point(42, 455)
        Me.chkAttachLink.Name = "chkAttachLink"
        Me.chkAttachLink.Size = New System.Drawing.Size(164, 17)
        Me.chkAttachLink.TabIndex = 28
        Me.chkAttachLink.Text = "Adjuntar el link al documento"
        Me.chkAttachLink.UseVisualStyleBackColor = True
        '
        'UcNuevoMensaje
        '
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Controls.Add(Me.chkAttachLink)
        Me.Controls.Add(Me.chkAutomaticSend)
        Me.Controls.Add(Me.chkNotifyCreator)
        Me.Controls.Add(Me.grpAttachs)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtMensaje)
        Me.Controls.Add(Me.txtAsunto)
        Me.Controls.Add(Me.pnlButtons)
        Me.Name = "UcNuevoMensaje"
        Me.Size = New System.Drawing.Size(386, 512)
        Me.pnlButtons.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAttachs.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Events"
    Public Event NuevoMensaje(ByVal Asunto As String, ByVal Mensaje As String, ByVal CurrentUser As IUser, ByVal notificar As Boolean, ByVal attachs As List(Of String), ByVal notifyIds As Generic.List(Of Int64), ByVal blnAutomaticAttachLink As Boolean, ByVal blnAutomaticSend As Boolean)
#End Region

#Region "Atributos"
    Dim _Asunto, _mensaje As String
    Private User As IUser
    Private docIds As Generic.List(Of Int64)
    Private flagDialogResult As Boolean = False
    Public DialogResult As DialogResult
    Private attachs As List(Of String)
#End Region

#Region "Propiedades"
    Public WriteOnly Property UsersButtonEnabled() As Boolean
        Set(ByVal value As Boolean)
            Me.btnUsuarios.Enabled = value
            If value Then
                Me.btnUsuarios.BackColor = System.Drawing.Color.AliceBlue
            Else
                Me.btnUsuarios.BackColor = System.Drawing.Color.Gray
            End If
        End Set
    End Property
    Public ReadOnly Property asunto() As String
        Get
            Return _Asunto
        End Get
    End Property
    Public ReadOnly Property mensaje() As String
        Get

            Return _mensaje
        End Get
    End Property
    Public ReadOnly Property NotifyCreatorOnly() As Boolean
        Get
            Return chkNotifyCreator.Checked
        End Get
    End Property


#End Region

#Region "Constructor"
    Public Sub New(ByVal User As IUser, ByVal _docIds As Generic.List(Of Int64), Optional ByVal Asunto As String = "")
        MyBase.New()
        _Asunto = Asunto
        Me.User = User
        Me.docIds = _docIds

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

        'Se obtiene la ruta del servidor de adjuntos.
        Dim serverPath As String = ZOptBusiness.GetValue("ServAdjuntosRuta")
        'Se verifica que se haya configurado la ruta.
        If String.IsNullOrEmpty(serverPath) Then
            Me.grpAttachs.Enabled = False
            'MessageBox.Show("No se ha configurado la ruta de servidor de archivos adjuntos de foro." + vbCrLf + _
            '                "La opción de adjuntar quedará deshabilitada hasta que se configure dicha ruta." + vbCrLf + _
            '                "Consulte con el Departamento de Sistemas.", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.lstAttachs.Items.Add("No se ha configurado la ruta de servidor de archivos")
            Me.lstAttachs.Items.Add("adjuntos de foro. La opción de adjuntar quedará")
            Me.lstAttachs.Items.Add("deshabilitada hasta que dicha ruta sea configurada.")
            Me.lstAttachs.Items.Add(String.Empty)
            Me.lstAttachs.Items.Add("Consulte con el Departamento de Sistemas.")
        End If
    End Sub
#End Region

#Region "Eventos"

    Private Sub UcNuevoMensaje_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If _Asunto.Trim <> "" Then Me.txtAsunto.Text = "Re: " & _Asunto
        Me.flagDialogResult = False
        Me.DialogResult = Windows.Forms.DialogResult.No
        Me.btnRemoveAll.Enabled = False
        Me.btnRemoveAttach.Enabled = False
        'posiciona el check
        chkNotifyCreator.Checked = UserPreferences.getValue("NotifyToForumCreator", Sections.UserPreferences, True)
        chkNotifyCreator.Top = pnlButtons.Top - 20
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If Me.txtAsunto.Text.Trim = String.Empty Then
            MessageBox.Show("Falta completar el campo Asunto.", "Atención", MessageBoxButtons.OK)
            Exit Sub
        End If
        If Me.txtMensaje.Text.Trim = String.Empty Then
            MessageBox.Show("Falta completar el campo Mensaje.", "Atención", MessageBoxButtons.OK)
            Exit Sub
        End If

        If Me.grpAttachs.Enabled Then
            Me.attachs = GetAttachs()
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.flagDialogResult = True

        RaiseEvent NuevoMensaje(Me.txtAsunto.Text, Me.txtMensaje.Text, User, False, attachs, notifyIds, chkAttachLink.Checked, chkAutomaticSend.Checked)

    End Sub

    Private notifyIds As New Generic.List(Of Int64)
    Private Sub btnUsuarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsuarios.Click
        Dim newFrmSelectUsers As New frmSelectUsersForo(GroupToNotifyTypes.Foro, 0)
        newFrmSelectUsers.StartPosition = FormStartPosition.CenterParent
        newFrmSelectUsers.ShowDialog()
        notifyIds = newFrmSelectUsers.Notifyids
        newFrmSelectUsers.Dispose()
        newFrmSelectUsers = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al hacer click sobre el botón "Guardar/Notificar"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	04/02/2009	Modified    Invocación del evento modificado "NuevoMensajeNotificar" y eliminación de código
    ''' </history>
    Private Sub btnNotificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotificar.Click

        If Me.txtAsunto.Text.Trim = String.Empty Then
            MessageBox.Show("Falta completar el campo Asunto.", "Atención", MessageBoxButtons.OK)
            Exit Sub
        End If
        If Me.txtMensaje.Text.Trim = String.Empty Then
            MessageBox.Show("Falta completar el campo Mensaje.", "Atención", MessageBoxButtons.OK)
            Exit Sub
        End If

        If Me.grpAttachs.Enabled Then
            Me.attachs = GetAttachs()
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.flagDialogResult = True

        Me.txtMensaje.Text = Me.txtMensaje.Text
        _Asunto = Me.txtAsunto.Text
        _mensaje = Me.txtMensaje.Text

        RaiseEvent NuevoMensaje(Me.txtAsunto.Text, Me.txtMensaje.Text, Me.User, True, attachs, notifyIds, chkAttachLink.Checked, chkAutomaticSend.Checked)

    End Sub
#End Region

#Region "Adjuntos"
    Private Sub btnAddAtach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAtach.Click
        Dim open As OpenFileDialog
        Dim fileName As String
        Dim fileNameStartPosition As Int16
        Try
            'Configuración del OpenFileDialog.
            open = New OpenFileDialog()
            open.CheckFileExists = True
            open.CheckPathExists = True
            open.DereferenceLinks = True
            open.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            open.Multiselect = True
            open.RestoreDirectory = False

            'Agrega los archivos seleccionados a la lista de adjuntos.
            If open.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If open.FileNames.Length > 0 Then
                    For Each path As String In open.FileNames
                        fileNameStartPosition = path.LastIndexOf("\")
                        fileName = path.Substring(fileNameStartPosition)
                        fileName = fileName.Remove(0, 1)
                        Me.lstAttachs.Items.Add(fileName)
                    Next
                End If

                'Habilitación de los botones de remover.
                Me.btnRemoveAll.Enabled = True
                If Not Me.btnRemoveAttach.Enabled Then
                    RemoveHandler Me.lstAttachs.SelectedIndexChanged, AddressOf lstAttachs_SelectedIndexChanged
                    AddHandler Me.lstAttachs.SelectedIndexChanged, AddressOf lstAttachs_SelectedIndexChanged
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ocurrió un error al cargar el archivo.", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            open.Dispose()
            open = Nothing
        End Try
    End Sub

    Private Sub btnRemoveAttach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAttach.Click
        Dim index As Int32 = lstAttachs.SelectedIndex

        'Si hay un item seleccionado.
        If index > -1 Then
            'Remuevo el item seleccionado.
            Me.lstAttachs.Items.RemoveAt(index)

            'Verifica que existan adjuntos en la lista.
            If Me.lstAttachs.Items.Count > 0 Then
                'Selecciono el item que este a continuación para facilitar la eliminación.
                If index = Me.lstAttachs.Items.Count Then
                    Me.lstAttachs.SelectedIndex = index - 1
                Else
                    Me.lstAttachs.SelectedIndex = index
                End If
            Else
                'Si no hay adjuntos deshabilito los botones para remover
                btnRemoveAll.Enabled = False
                btnRemoveAttach.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnRemoveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAll.Click
        'Remueve todos los adjuntos de la lista.
        If Me.lstAttachs.Items.Count > 0 Then
            Me.lstAttachs.Items.Clear()
            btnRemoveAll.Enabled = False
            btnRemoveAttach.Enabled = False
        End If
    End Sub

    Private Sub lstAttachs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstAttachs.SelectedIndexChanged
        Me.btnRemoveAttach.Enabled = True
        RemoveHandler Me.lstAttachs.SelectedIndexChanged, AddressOf lstAttachs_SelectedIndexChanged
    End Sub

    ''' <summary>
    ''' Carga la lista con adjuntos que será utilizada para enviarla 
    ''' por parámetro al crear el mensaje.
    ''' </summary>
    Private Function GetAttachs() As List(Of String)
        If Me.lstAttachs.Items.Count > 0 Then
            attachs = New List(Of String)

            For Each path As String In Me.lstAttachs.Items
                attachs.Add(path)
            Next
        End If

        Return attachs
    End Function
#End Region

End Class
