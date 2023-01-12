Imports Zamba.Core
Imports Zamba.AppBlock

Public Class UcForo
    Inherits ZControl

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
    '   Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents pnlForumToolbar As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents btnResponder As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents tabControlMessage As System.Windows.Forms.TabControl
    Friend WithEvents tabMessage As System.Windows.Forms.TabPage
    Friend WithEvents tabAttachs As System.Windows.Forms.TabPage
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents tabParticipants As System.Windows.Forms.TabPage
    Friend WithEvents tabDetails As System.Windows.Forms.TabPage
    Friend WithEvents pnlMessages As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.pnlMessages = New System.Windows.Forms.Panel
        Me.pnlForumToolbar = New System.Windows.Forms.Panel
        Me.btnEliminar = New System.Windows.Forms.Button
        Me.btnResponder = New System.Windows.Forms.Button
        Me.btnNuevo = New System.Windows.Forms.Button
        Me.tabControlMessage = New System.Windows.Forms.TabControl
        Me.tabMessage = New System.Windows.Forms.TabPage
        Me.tabAttachs = New System.Windows.Forms.TabPage
        Me.tabParticipants = New System.Windows.Forms.TabPage
        Me.tabDetails = New System.Windows.Forms.TabPage
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.Panel2.SuspendLayout()
        Me.pnlForumToolbar.SuspendLayout()
        Me.tabControlMessage.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel2.Controls.Add(Me.pnlMessages)
        Me.Panel2.Controls.Add(Me.pnlForumToolbar)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(770, 196)
        Me.Panel2.TabIndex = 8
        '
        'pnlMessages
        '
        Me.pnlMessages.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.pnlMessages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMessages.Location = New System.Drawing.Point(0, 34)
        Me.pnlMessages.Name = "pnlMessages"
        Me.pnlMessages.Size = New System.Drawing.Size(770, 162)
        Me.pnlMessages.TabIndex = 9
        '
        'pnlForumToolbar
        '
        Me.pnlForumToolbar.BackColor = System.Drawing.Color.FromArgb(CType(CType(185, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.pnlForumToolbar.Controls.Add(Me.btnEliminar)
        Me.pnlForumToolbar.Controls.Add(Me.btnResponder)
        Me.pnlForumToolbar.Controls.Add(Me.btnNuevo)
        Me.pnlForumToolbar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlForumToolbar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlForumToolbar.ForeColor = System.Drawing.Color.Black
        Me.pnlForumToolbar.Location = New System.Drawing.Point(0, 0)
        Me.pnlForumToolbar.Name = "pnlForumToolbar"
        Me.pnlForumToolbar.Size = New System.Drawing.Size(770, 34)
        Me.pnlForumToolbar.TabIndex = 8
        '
        'btnEliminar
        '
        Me.btnEliminar.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnEliminar.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminar.Location = New System.Drawing.Point(229, 6)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(102, 22)
        Me.btnEliminar.TabIndex = 14
        Me.btnEliminar.Text = "ELIMINAR"
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnResponder
        '
        Me.btnResponder.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.btnResponder.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnResponder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnResponder.Location = New System.Drawing.Point(118, 6)
        Me.btnResponder.Name = "btnResponder"
        Me.btnResponder.Size = New System.Drawing.Size(102, 22)
        Me.btnResponder.TabIndex = 13
        Me.btnResponder.Text = "RESPONDER"
        Me.btnResponder.UseVisualStyleBackColor = False
        '
        'btnNuevo
        '
        Me.btnNuevo.BackColor = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnNuevo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevo.Location = New System.Drawing.Point(7, 6)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(102, 22)
        Me.btnNuevo.TabIndex = 12
        Me.btnNuevo.Text = "NUEVO TEMA"
        Me.btnNuevo.UseVisualStyleBackColor = False
        '
        'tabControlMessage
        '
        Me.tabControlMessage.Controls.Add(Me.tabMessage)
        Me.tabControlMessage.Controls.Add(Me.tabAttachs)
        Me.tabControlMessage.Controls.Add(Me.tabParticipants)
        Me.tabControlMessage.Controls.Add(Me.tabDetails)
        Me.tabControlMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControlMessage.Location = New System.Drawing.Point(0, 0)
        Me.tabControlMessage.Name = "tabControlMessage"
        Me.tabControlMessage.SelectedIndex = 0
        Me.tabControlMessage.Size = New System.Drawing.Size(770, 205)
        Me.tabControlMessage.TabIndex = 0
        '
        'tabMessage
        '
        Me.tabMessage.Location = New System.Drawing.Point(4, 22)
        Me.tabMessage.Name = "tabMessage"
        Me.tabMessage.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMessage.Size = New System.Drawing.Size(762, 179)
        Me.tabMessage.TabIndex = 0
        Me.tabMessage.Text = "Mensaje"
        Me.tabMessage.UseVisualStyleBackColor = True
        '
        'tabAttachs
        '
        Me.tabAttachs.Location = New System.Drawing.Point(4, 22)
        Me.tabAttachs.Name = "tabAttachs"
        Me.tabAttachs.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAttachs.Size = New System.Drawing.Size(762, 179)
        Me.tabAttachs.TabIndex = 1
        Me.tabAttachs.Text = "Adjuntos"
        Me.tabAttachs.UseVisualStyleBackColor = True
        '
        'tabParticipants
        '
        Me.tabParticipants.Location = New System.Drawing.Point(4, 22)
        Me.tabParticipants.Name = "tabParticipants"
        Me.tabParticipants.Size = New System.Drawing.Size(762, 179)
        Me.tabParticipants.TabIndex = 2
        Me.tabParticipants.Text = "Participantes"
        Me.tabParticipants.UseVisualStyleBackColor = True
        '
        'tabDetails
        '
        Me.tabDetails.Location = New System.Drawing.Point(4, 22)
        Me.tabDetails.Name = "tabDetails"
        Me.tabDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDetails.Size = New System.Drawing.Size(762, 179)
        Me.tabDetails.TabIndex = 3
        Me.tabDetails.Text = "Información"
        Me.tabDetails.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel4.Controls.Add(Me.tabControlMessage)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 199)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(770, 205)
        Me.Panel4.TabIndex = 0
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(0, 196)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(770, 3)
        Me.Splitter1.TabIndex = 10
        Me.Splitter1.TabStop = False
        '
        'UcForo
        '
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.Panel4)
        Me.Name = "UcForo"
        Me.Size = New System.Drawing.Size(770, 404)
        Me.Panel2.ResumeLayout(False)
        Me.pnlForumToolbar.ResumeLayout(False)
        Me.tabControlMessage.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region

#Region "Atributos"
    Private UcQuestion As New UCQuestion
    Private UcMensaje As New UcMensaje
    Private UcDetails As New UcForumDetails
    Private UcParticipants As UcForumParticipants
    Private LeyendoMensaje As Boolean = True
    Private User As IUser
    Private _mailsToNotify As New Generic.List(Of String)
    Private NuevoCual As Int32
    Private groupId As Int64
    Private resultIDAndDocTypeIds As New Generic.Dictionary(Of Int64, Int64)
    Private ResultsIds As New Generic.List(Of Int64)
    Private ucAttachs As New UcForumAttachsGrid()
    Private showSelectedId As Boolean = False
    Private MensajeNuevo As System.Windows.Forms.Form
    Private UCMsgNew As UcNuevoMensaje
    Private loadMessage As Boolean
    Private IdMsg As Int64 = 0

    'Verifica si se deben traer todos los mensajes de las versiones anteriores o posteriores del documento
    Dim CheckIfVersionMessagesShouldShow As Boolean = DocVersionBusiness.ShowConverstationsOfAllVersions

    ' Evento que al invocarse permite la visualización de un formulario de mail
    Public Event showMailForm(ByRef textFor As String, ByRef textSubject As String, ByRef textBody As String, ByVal idMensaje As Int32, ByVal parentId As Int64, ByVal blnAutomaticAttachLink As Boolean, ByVal blnAutomaticSend As Boolean, ByVal attachPaths() As String)
#End Region

#Region "Propiedades"

    Public ReadOnly Property MailsToNotify() As String
        Get
            If _mailsToNotify.Count > 0 Then
                Return GetMailList(_mailsToNotify)
            Else
                Return String.Empty
            End If

        End Get
    End Property


#End Region

#Region "Constructores"
    Public Sub New(ByVal User As IUser, ByVal _resultIDAndDocTypeIds As Generic.Dictionary(Of Int64, Int64))
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        Me.resultIDAndDocTypeIds = _resultIDAndDocTypeIds
        For Each value As Generic.KeyValuePair(Of Int64, Int64) In resultIDAndDocTypeIds
            ResultsIds.Add(value.Key)
        Next
        Me.User = User
        loadMessage = False

        LoadForum(False)
    End Sub

    Public Sub New(ByVal User As IUser, ByVal _resultID As Int64, ByVal _docTypeID As Int64)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        Me.resultIDAndDocTypeIds.Add(_resultID, _docTypeID)
        ResultsIds.Add(_resultID)
        Me.User = User

        loadMessage = True

        LoadForum(True)
    End Sub

    ''' <summary>
    ''' Carga los valores por defecto, permisos y handlers.
    ''' </summary>
    ''' <history>
    '''   Tomas     07/05/2010  Modified
    ''' </history>
    ''' <remarks>
    ''' Con false se obvia la parte grafica para mejorar la performance cuando se llama desde la PlayDoForo.
    ''' </remarks>
    Private Sub LoadForum(ByVal loadGraphics As Boolean)
        If loadGraphics Then
            'Visualización de controles
            Me.btnNuevo.Visible = Not Convert.ToBoolean(UserPreferences.getValue("OcultarNuevoTemaForo", Sections.FormPreferences, False))

            'Treeview con mensajes
            UcQuestion.Dock = DockStyle.Fill
            pnlMessages.Controls.Add(UcQuestion)

            'Texto del mensaje y adjuntos
            UcMensaje.Dock = DockStyle.Fill
            ucAttachs.Dock = DockStyle.Fill
            UcDetails.Dock = DockStyle.Fill
            UcParticipants = New UcForumParticipants()
            UcParticipants.Dock = DockStyle.Fill

            tabAttachs.Controls.Add(ucAttachs)
            tabDetails.Controls.Add(UcDetails)
            tabMessage.Controls.Add(UcMensaje)
            tabParticipants.Controls.Add(UcParticipants)

            'Eventos
            RemoveHandler UcQuestion.MessageSelected, AddressOf PreguntaSeleccionada
            AddHandler UcQuestion.MessageSelected, AddressOf PreguntaSeleccionada

            'Ajusta los splitters
            SetSplitters()
        End If

        LeyendoMensaje = True
        UcMensaje.Bloquear(LeyendoMensaje)
    End Sub
#End Region

#Region "Métodos y eventos"

    Public Shadows Sub ShowInfo(ByVal ResultId As Int64, ByVal DocTypeId As Int64)
        Me.resultIDAndDocTypeIds.Clear()
        Me.resultIDAndDocTypeIds.Add(ResultId, DocTypeId)
        Me.ResultsIds.Clear()
        Me.ResultsIds.Add(ResultId)
        Me.Parent.Text = "Foro (" & UcQuestion.ForoTreeView.GetNodeCount(True) & " mensaje/s)"
        Me.btnEliminar.Visible = UserBusiness.Rights.GetUserRights(Me.User, Zamba.Core.ObjectTypes.DocTypes, RightsType.DeleteMsgForum, DocTypeId)
        FiltrarAlIniciar()
    End Sub
    Private Sub FiltrarAlIniciar()
        Try
            'Se obtienen los mensajes
            Dim ArrayMensajes As New ArrayList '= ForoBusiness.GetAllMessages(ResultId)
            Dim ArrayRespuestas As New ArrayList '= ForoBusiness.GetAllAnswers(ResultId)
            Dim lastVersionId As Int64

            Zamba.Core.ZForoBusiness.GetAllMessages(Me.ResultsIds(0), Me.resultIDAndDocTypeIds.Item(Me.ResultsIds(0)), ArrayMensajes, ArrayRespuestas, CheckIfVersionMessagesShouldShow)

            Try
                If ArrayMensajes.Count = 0 Then
                    UcQuestion.SinResultados(True)
                    Me.UcMensaje.Bloquear(False)
                Else
                    UcQuestion.SinResultados(False)
                End If
            Catch ex As Exception
                UcQuestion.SinResultados(True)
            End Try
            UcQuestion.CargarEnTreeview(ArrayMensajes, ArrayRespuestas)
            '[Pablo] se comenta para incrementar la performance al mostrar los mensajes en el foro
            Me.Parent.Text = "Foro (" & UcQuestion.ForoTreeView.GetNodeCount(True) & " mensaje/s)"
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Carga el arbol del foro
    ''' </summary>
    ''' <history>
    '''   Marcelo     30/06/09  Modified - Ahora se hace una sola llamada a la BD
    '''</history>
    ''' <remarks></remarks>
    Private Sub FillTreeview()
        Try
            Dim ArrayMensajes As New ArrayList
            Dim ArrayRespuestas As New ArrayList

            Zamba.Core.ZForoBusiness.GetAllMessages(Me.ResultsIds(0), Me.resultIDAndDocTypeIds(Me.ResultsIds(0)), ArrayMensajes, ArrayRespuestas, CheckIfVersionMessagesShouldShow)

            UcQuestion.CargarEnTreeview(ArrayMensajes, ArrayRespuestas)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub PreguntaSeleccionada(ByVal Mensaje As MensajeForo)
        LeyendoMensaje = True
        UcMensaje.Bloquear(LeyendoMensaje)

        'TODO: Para una mejor performance se debería verificar que solapa se encuentra
        'seleccionada y luego cargar unicamente esa solapa. Luego, al seleccionar otra
        'solapa debería cargar los datos de aquella solapa. Los UC deberían tener una
        'variable interna con el MessageId para no cargar 2 veces los mismos datos.
        UcMensaje.FillMensaje(Mensaje)
        ucAttachs.FillAttachs(Mensaje.ID)
        UcDetails.FillDetails(Mensaje)
        UcParticipants.FillParticipants(UcQuestion.GetParentMessageId(Mensaje.ID), User.ID, Me.ResultsIds(0))
    End Sub

    ''' <summary>
    ''' Método utilizado para eliminar un mensaje del foro
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	20/05/2008	Modified    Se agrego el registro de la acción
    ''' </history>
    Private Sub EliminarSelected()
        Try
            Dim messages As Generic.List(Of Int32) = UcQuestion.DeleteMessage()

            If messages.Count > 0 Then
                'Se eliminan los adjuntos del seleccionado.
                ucAttachs.DeleteAttachs(messages(0), True)

                'Se elimina el mensaje seleccionado y sus respuestas.
                Dim firstTime As Boolean = True
                For Each id As Int32 In messages
                    'El primer elemento ya fué eliminado en el método ucAttachs.DeleteAttachs.
                    'Es por eso que se filtra la primer pasada.
                    If firstTime Then
                        firstTime = False
                    Else
                        ucAttachs.DeleteAttachs(id, False)
                    End If

                    Zamba.Core.ZForoBusiness.DeleteMessage(id)

                    UserBusiness.Rights.SaveAction(id, ObjectTypes.Foro, RightsType.Delete, "Se borro mensaje con id: " & id.ToString)
                Next
            End If

            '[Pablo] se comenta para incrementar la performance al mostrar los mensajes en el foro
            'muestro en el tab la cantidad de mensajes que hay en total, sumando mesajes y respuestas.
            Me.Parent.Text = "Foro (" & UcQuestion.ForoTreeView.GetNodeCount(True) & " mensaje/s)"
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If Me.UcQuestion.ForoTreeView.SelectedNode IsNot Nothing Then
            If MessageBox.Show("¿Desea eliminar el mensaje seleccionado?" + vbCrLf + "Además del seleccionado se eliminarán todas sus respuestas.", _
                   "Eliminar mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                EliminarSelected()
            End If
        End If
    End Sub

    Private Sub btnResponder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResponder.Click
        Try
            'Si el mensaje al cual responder existe
            If Not IsNothing(UcQuestion.GetSeleccionado) Then
                'Se crea y setea el form donde va a estar el 
                'control UCNuevoMensaje
                MensajeNuevo = New System.Windows.Forms.Form
                MensajeNuevo.StartPosition = FormStartPosition.CenterScreen
                MensajeNuevo.FormBorderStyle = FormBorderStyle.FixedDialog
                MensajeNuevo.MaximizeBox = False
                MensajeNuevo.MinimizeBox = False

                Dim ArrayMensaje As New ArrayList
                ArrayMensaje = UcQuestion.GetSeleccionado

                Me.UCMsgNew = New UcNuevoMensaje(Me.User, Me.ResultsIds, ArrayMensaje(1).ToString)
                Me.UCMsgNew.Dock = DockStyle.Fill
                Me.UCMsgNew.txtAsunto.Enabled = False
                Me.UCMsgNew.UsersButtonEnabled = False
                MensajeNuevo.Height = UCMsgNew.Height + 50
                MensajeNuevo.Width = UCMsgNew.Width
                Me.MensajeNuevo.Controls.Add(Me.UCMsgNew)
                RemoveHandler UCMsgNew.NuevoMensaje, AddressOf NuevoMensaje
                AddHandler UCMsgNew.NuevoMensaje, AddressOf NuevoMensaje

                NuevoCual = 2

                If IsNothing(MensajeNuevo) = False Then MensajeNuevo.ShowDialog()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    'Este método emula lo que hace el evento Click del nuevo, pero 
    'aparte utiliza los valores pasados por parámetro
    Public Function btnNuevo_ClickSinEvento(ByRef _subject As String, ByRef _body As String, ByRef _flagSaved As String) As Int64
        Try
            'Se crea y setea el form donde va a estar el 
            'control UCNuevoMensaje
            MensajeNuevo = New System.Windows.Forms.Form
            MensajeNuevo.StartPosition = FormStartPosition.CenterScreen
            MensajeNuevo.FormBorderStyle = FormBorderStyle.FixedDialog
            MensajeNuevo.MaximizeBox = False
            MensajeNuevo.MinimizeBox = False

            'Se instancia el nuevo control
            Me.UCMsgNew = New UcNuevoMensaje(User, Me.ResultsIds)

            'Se le cargan los valores que se pasan por parámetro
            UCMsgNew.txtAsunto.Text = _subject
            UCMsgNew.txtMensaje.Text = _body

            Me.UCMsgNew.Dock = DockStyle.Fill
            Me.UCMsgNew.UsersButtonEnabled = True
            Me.UCMsgNew.chkNotifyCreator.Enabled = Convert.ToBoolean(UserPreferences.getValue("HabilitarCheckNotificacionCreadorForo", Sections.UserPreferences, False))
            Me.UCMsgNew.chkNotifyCreator.Checked = Convert.ToBoolean(UserPreferences.getValue("CheckNotificacionCreadorForoMarcado", Sections.UserPreferences, True))
            '(pablo) 
            Me.UCMsgNew.chkAttachLink.Checked = Convert.ToBoolean(UserPreferences.getValue("AttachAutomaticLinkInForum", Sections.UserPreferences, False))
            Me.UCMsgNew.chkAutomaticSend.Checked = Convert.ToBoolean(UserPreferences.getValue("AutomaticSendMailInForum", Sections.UserPreferences, False))

            MensajeNuevo.Height = UCMsgNew.Height + 50
            MensajeNuevo.Width = UCMsgNew.Width
            Me.MensajeNuevo.Controls.Add(Me.UCMsgNew)

            'Se setean los delegados para guardar el mensaje y guardar y notificar
            RemoveHandler UCMsgNew.NuevoMensaje, AddressOf NuevoMensaje
            AddHandler UCMsgNew.NuevoMensaje, AddressOf NuevoMensaje

            NuevoCual = 1

            If IsNothing(MensajeNuevo) = False Then
                MensajeNuevo.ShowDialog()
                'Se setea el flag de salida
                If Not IsNothing(Me.UCMsgNew) Then
                    If DialogResult.OK = Me.UCMsgNew.DialogResult Then
                        'Se le cargan los valores que se pasan por parámetro
                        _subject = UCMsgNew.asunto
                        _body = UCMsgNew.mensaje
                        _flagSaved = "True"
                        Return IdMsg
                    Else
                        _flagSaved = "False"
                        Return 0
                    End If
                Else
                    _flagSaved = "False"
                    Return 0
                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function
    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try

            'Se crea y setea el form donde va a estar el 
            'control UCNuevoMensaje
            MensajeNuevo = New System.Windows.Forms.Form
            MensajeNuevo.StartPosition = FormStartPosition.CenterScreen
            MensajeNuevo.FormBorderStyle = FormBorderStyle.FixedDialog
            MensajeNuevo.MaximizeBox = False
            MensajeNuevo.MinimizeBox = False

            Me.UCMsgNew = New UcNuevoMensaje(User, Me.ResultsIds)
            Me.UCMsgNew.Dock = DockStyle.Fill
            Me.UCMsgNew.UsersButtonEnabled = True
            Me.UCMsgNew.chkNotifyCreator.Enabled = Convert.ToBoolean(UserPreferences.getValue("HabilitarCheckNotificacionCreadorForo", Sections.UserPreferences, False))
            Me.UCMsgNew.chkNotifyCreator.Checked = Convert.ToBoolean(UserPreferences.getValue("CheckNotificacionCreadorForoMarcado", Sections.UserPreferences, True))

            Me.UCMsgNew.chkAttachLink.Checked = Convert.ToBoolean(UserPreferences.getValue("AttachAutomaticLinkInForum", Sections.UserPreferences, False))
            Me.UCMsgNew.chkAutomaticSend.Checked = Convert.ToBoolean(UserPreferences.getValue("AutomaticSendMailInForum", Sections.UserPreferences, False))
            MensajeNuevo.Height = UCMsgNew.Height + 50
            MensajeNuevo.Width = UCMsgNew.Width
            Me.MensajeNuevo.Controls.Add(Me.UCMsgNew)
            RemoveHandler UCMsgNew.NuevoMensaje, AddressOf NuevoMensaje
            AddHandler UCMsgNew.NuevoMensaje, AddressOf NuevoMensaje

            NuevoCual = 1

            If IsNothing(MensajeNuevo) = False Then MensajeNuevo.ShowDialog()



        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Método que se ejecuta cuando el cliente quiere guardar un nuevo tema o bien cuando quiere guardar una respuesta
    ''' </summary>
    ''' <param name="Asunto"></param>
    ''' <param name="Texto"></param>
    ''' <param name="User"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	19/05/2008	Modified
    ''' </history>
    Private Sub NuevoMensaje(ByVal Asunto As String, ByVal Texto As String, ByVal User As IUser, ByVal notificar As Boolean, ByVal attachs As Generic.List(Of String), ByVal notifyIds As Generic.List(Of Int64), ByVal blnAutomaticAttachLink As Boolean, ByVal blnAutomaticSend As Boolean)
        Try
            LeyendoMensaje = True
            Dim ArrayMensaje As New ArrayList
            'Dim NextId As Int32
            Dim ParentId As Int32
            IdMsg = CoreBusiness.GetNewID(IdTypes.ForumMessage)
            Dim attachPaths() As String

            If NuevoCual = 1 Then ' Nuevo mensaje
                ParentId = 0

                'Se inserta el mensaje.
                ZForoBusiness.InsertMessage(IdMsg, ParentId, Asunto, Texto, User.ID)

                'Obtiene e inserta los participantes de la conversación.
                'Solo el creador del tema es quien puede asignar los participantes.
                'Dim notifyIds As Generic.List(Of Int64) = Zamba.Core.NotifyBusiness.getAllSelectedUsers(GroupToNotifyTypes.Foro, Me.ResultsIds)
                If IsNothing(notifyIds) OrElse notifyIds.Count < 1 Then
                    'Si no se seleccionaron usuarios, se toma el creador como único participante.
                    ZForoBusiness.InsertMessageParticipant(IdMsg, User.ID)
                Else
                    'Si el creador no se encuentra seleccionado, se agrega.
                    If Not notifyIds.Contains(User.ID) Then
                        notifyIds.Add(User.ID)
                    End If


                    'Agrega los participantes de la conversación.
                    For Each U As Int64 In notifyIds
                        ZForoBusiness.InsertMessageParticipant(IdMsg, U)
                    Next U
                End If

                'Se agrega el mensaje a los documentos seleccionados (habrá más de uno con la DoForo).
                For Each id As Generic.KeyValuePair(Of Int64, Int64) In Me.resultIDAndDocTypeIds
                    ZForoBusiness.InsertMessageDoc(IdMsg, id.Key, id.Value)
                Next

                'Se agregan los adjuntos.
                If attachs IsNot Nothing Then
                    ucAttachs.CreateAttachs(IdMsg, attachs)
                    attachPaths = attachs.ToArray
                End If

                UserBusiness.Rights.SaveAction(IdMsg, ObjectTypes.Foro, RightsType.NuevoTemaGuardar, "Se agregó un nuevo mensaje al foro con asunto: " & Asunto)

            ElseIf NuevoCual = 2 Then ' Respuesta
                ArrayMensaje = UcQuestion.GetSeleccionado
                If ArrayMensaje Is Nothing Then
                    MessageBox.Show("Debe seleccionar un mensaje para responder")
                    Exit Sub
                End If
                ParentId = CLng(ArrayMensaje(0))

                'Inserta los datos de la respuesta.
                Zamba.Core.ZForoBusiness.InsertMessage(IdMsg, ParentId, Asunto, Texto, User.ID)
                Dim ds As DataSet
                '(pablo)
                If ArrayMensaje(3) = 0 Then
                    ds = ZForoBusiness.GetMessageReplyParticipant(ParentId)
                Else
                    ds = ZForoBusiness.GetMessageReplyParticipant(CInt(ArrayMensaje(3)))
                    ParentId = CInt(ArrayMensaje(3))
                End If

                For i As Int32 = 0 To ds.Tables(0).Rows.Count - 1
                    'ZForoBusiness.InsertMessageParticipant(IdMsg, CLng(ds.Tables(0).Rows(t).Item(0)))
                    notifyIds.Add(ds.Tables(0).Rows(i).Item(0))
                Next

                'Se agrega el mensaje al documento seleccionado.
                ZForoBusiness.InsertMessageDoc(IdMsg, Me.ResultsIds(0), Me.resultIDAndDocTypeIds.Item(Me.ResultsIds(0)))

                'Verifica si debe insertar la respuesta en otros documentos (habrá más de uno con la DoForo).
                Dim relatedDocs As DataTable = ZForoBusiness.GetRealtedDocs(ParentId, Me.ResultsIds(0))
                For Each dr As DataRow In relatedDocs.Rows
                    ZForoBusiness.InsertMessageDoc(IdMsg, dr(0).ToString, dr(1).ToString)
                Next

                'Se agregan los adjuntos.
                If attachs IsNot Nothing Then
                    ucAttachs.CreateAttachs(IdMsg, attachs)
                    attachPaths = attachs.ToArray
                End If

                UserBusiness.Rights.SaveAction(IdMsg, ObjectTypes.Foro, RightsType.ResponderGuardar, "Se agregó una respuesta con asunto: " & Asunto & " haciendo referencia al mensaje con ID " & ParentId)

            End If

            'Agrega el nodo del mensaje al foro (con la DoForo no pasa).
            If loadMessage Then
                Dim Mensaje As New MensajeForo

                Mensaje.Mensaje = Texto
                Mensaje.UserId = CInt(User.ID)
                Mensaje.Fecha = Now
                Mensaje.UserName = UserBusiness.CurrentUser.Apellidos + " " + UserBusiness.CurrentUser.Nombres
                Mensaje.DiasVto = 0
                Mensaje.ParentId = ParentId
                Mensaje.ID = IdMsg
                Mensaje.Name = Asunto

                UcQuestion.CargarMsgEnTreeview(Mensaje)
                UcQuestion.SinResultados(False)
                '[Pablo] se comenta para incrementar la performance al mostrar los mensajes en el foro
                Me.Parent.Text = "Foro (" & UcQuestion.ForoTreeView.GetNodeCount(True) & " mensaje/s)"
                'NuevoCual = 0
                UcMensaje.Bloquear(LeyendoMensaje)
            End If

            Me.UCMsgNew.Dispose()
            MensajeNuevo.Close()
            MensajeNuevo.Dispose()

            'Realiza la notificación del mensaje.
            If notificar Then
                _mailsToNotify.Clear()
                Dim userMails As New Generic.List(Of String)
                'Si es una respuesta y se debe responder unicamente al creador
                If NuevoCual = 2 AndAlso Me.UCMsgNew.NotifyCreatorOnly Then
                    RaiseEvent showMailForm(NotifyBusiness.GetUserMail(ParentId), Asunto, Texto, IdMsg, ParentId, blnAutomaticAttachLink, blnAutomaticSend, attachPaths)

                    'Caso contrario, se verifica si existen mails a notificar.
                Else
                    If notifyIds IsNot Nothing AndAlso notifyIds.Count > 0 Then
                        Dim userMail As IUser
                        Dim users As Generic.List(Of IUser)

                        'Se encarga de obtener los mails de los usuarios y de los usuarios dentro de los grupos.
                        For Each id As Int64 In notifyIds
                            userMail = UserBusiness.GetUserById(id)

                            If userMail IsNot Nothing AndAlso Not userMails.Contains(userMail.eMail.Mail) Then
                                If Not String.IsNullOrEmpty(userMail.eMail.Mail) AndAlso User.ID <> id Then
                                    userMails.Add(userMail.eMail.Mail)
                                End If
                            Else
                                users = UserGroupBusiness.GetUsersByGroup(id)
                                For Each userMailGroup As IUser In users
                                    If Not userMails.Contains(userMailGroup.eMail.Mail) AndAlso String.Compare(userMailGroup.eMail.Mail, User.eMail.Mail) <> 0 Then
                                        userMails.Add(userMailGroup.eMail.Mail)
                                    End If
                                Next
                            End If
                        Next
                    End If
                    '(pablo)
                    'si notifico pero no selecciono participantes no realizo envío de mail
                    If userMails.Count > 0 Then
                        RaiseEvent showMailForm(GetMailList(userMails), Asunto, Texto, IdMsg, ParentId, blnAutomaticAttachLink, blnAutomaticSend, attachPaths)
                    End If
                End If
                _mailsToNotify.Clear()

            End If

            'Se mueve de lugar esta asignacion (antes estaba en la creación del nodo)
            If loadMessage Then
                NuevoCual = 0
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Concatena los items de la lista para poder usarla al enviar un mail. Devuelve un string
    ''' </summary>
    ''' <param name="List"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     Ezequiel    Created
    '''     Tomas       15/07/2010  Modified
    ''' </history>
    Private Function GetMailList(ByVal List As Generic.List(Of String)) As String
        Dim MailList As String = String.Empty
        For Each x As String In List
            MailList &= x & ";"
        Next
        MailList = MailList.Remove(MailList.Length - 1, 1)
        Return MailList
    End Function

    Public Sub SetSplitters()
        Try
            Me.Splitter1.Top = CInt(Me.Height / 2)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método que sirve para cerrar el formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	04/02/2009	Created    Código original del método "NuevoMensajeNotificar"
    ''' </history>
    'Public Function closeNuevoMensaje()
    Public Sub closeNuevoMensaje()

        If Not IsNothing(Me.UCMsgNew) Then
            Me.UCMsgNew.Dispose()
            Me.UCMsgNew = Nothing
        End If

        If Not IsNothing(MensajeNuevo) Then
            MensajeNuevo.Dispose()
            MensajeNuevo = Nothing
        End If

    End Sub
#End Region

#Region "Codigo obsoleto de foro"
    'Private Shared Function SiguienteId(ByVal DocId As Int64) As Int32
    '    Return Zamba.Core.ZForoBusiness.SiguienteId(DocId)
    'End Function
    'Private Sub ResponderTema()

    '    Try
    '        NuevoCual = 2
    '        LeyendoMensaje = False
    '        UcMensaje.Bloquear(LeyendoMensaje)
    '    Catch ex As Exception
    '        Zamba.Core.ZClass.raiseerror(ex)
    '    End Try

    '    Try
    '        Dim ArrayMensaje As New ArrayList
    '        ArrayMensaje = UcQuestion.GetSeleccionado
    '        If IsNothing(ArrayMensaje) = True Then
    '            MessageBox.Show("Debe seleccionar un mensaje para responder")
    '        Else
    '            ' UcMensaje.CompletarAsunto(ArrayMensaje(3))
    '        End If

    '    Catch ex As Exception
    '        Zamba.Core.ZClass.raiseerror(ex)
    '    End Try
    'End Sub
#End Region

End Class
