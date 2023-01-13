Imports Zamba.Core
Imports System.Collections.Generic
Imports System.Text
Imports Zamba.Data

Public Class UcForo
    Inherits ZControl
    Implements IDisposable

#Region " Código generado por el Diseñador de Windows Forms "

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If

                If ucAttachs IsNot Nothing Then ucAttachs.Dispose()
                If UcDetails IsNot Nothing Then UcDetails.Dispose()
                If UcMensaje IsNot Nothing Then UcMensaje.Dispose()
                If UcParticipants IsNot Nothing Then UcParticipants.Dispose()
                If UcQuestion IsNot Nothing Then UcQuestion.Dispose()

                If btnEliminar IsNot Nothing Then btnEliminar.Dispose()
                If btnNuevo IsNot Nothing Then btnNuevo.Dispose()
                If btnResponder IsNot Nothing Then btnResponder.Dispose()
                If MensajeNuevo IsNot Nothing Then MensajeNuevo.Dispose()

                If Panel2 IsNot Nothing Then
                    Panel2.Controls.Clear()
                    Panel2.Dispose()
                End If
                If Panel4 IsNot Nothing Then
                    Panel4.Controls.Clear()
                    Panel4.Dispose()
                End If
                If pnlForumToolbar IsNot Nothing Then
                    pnlForumToolbar.Controls.Clear()
                    pnlForumToolbar.Dispose()
                End If
                If pnlMessages IsNot Nothing Then
                    pnlMessages.Controls.Clear()
                    pnlMessages.Dispose()
                End If

                If resultIDAndDocTypeIds IsNot Nothing Then resultIDAndDocTypeIds.Clear()
                If ResultsIds IsNot Nothing Then ResultsIds.Clear()
                If Splitter1 IsNot Nothing Then
                    Splitter1.Controls.Clear()
                    Splitter1.Dispose()
                End If

                If tabAttachs IsNot Nothing Then tabAttachs.Dispose()
                If tabDetails IsNot Nothing Then tabDetails.Dispose()
                If tabParticipants IsNot Nothing Then tabParticipants.Dispose()
                If tabMessage IsNot Nothing Then tabMessage.Dispose()
                If tabParticipants IsNot Nothing Then tabParticipants.Dispose()
                If tabControlMessage IsNot Nothing Then tabControlMessage.Dispose()

            End If
            MyBase.Dispose(disposing)
            isDisposed = True
        End If
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    Private isDisposed As Boolean
    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    '   Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents pnlForumToolbar As ZPanel
    Friend WithEvents Panel4 As ZPanel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents btnNuevo As ZButton
    Friend WithEvents btnResponder As ZButton
    Friend WithEvents btnEliminar As ZButton
    Friend WithEvents tabControlMessage As System.Windows.Forms.TabControl
    Friend WithEvents tabMessage As ZTabPage
    Friend WithEvents tabAttachs As ZTabPage
    Friend WithEvents Panel2 As ZPanel
    Friend WithEvents tabParticipants As ZTabPage
    Friend WithEvents tabDetails As ZTabPage
    Friend WithEvents btnReenviar As ZButton
    Friend WithEvents UcRespuesta1 As UcRespuesta
    Friend WithEvents pnlMessages As ZPanel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel2 = New Zamba.AppBlock.ZPanel()
        Me.pnlMessages = New Zamba.AppBlock.ZPanel()
        Me.pnlForumToolbar = New Zamba.AppBlock.ZPanel()
        Me.btnReenviar = New Zamba.AppBlock.ZButton()
        Me.btnEliminar = New Zamba.AppBlock.ZButton()
        Me.btnResponder = New Zamba.AppBlock.ZButton()
        Me.btnNuevo = New Zamba.AppBlock.ZButton()
        Me.tabControlMessage = New System.Windows.Forms.TabControl()
        Me.tabMessage = New Zamba.AppBlock.ZTabPage()
        Me.tabAttachs = New Zamba.AppBlock.ZTabPage()
        Me.tabParticipants = New Zamba.AppBlock.ZTabPage()
        Me.tabDetails = New Zamba.AppBlock.ZTabPage()
        Me.Panel4 = New Zamba.AppBlock.ZPanel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.UcRespuesta1 = New Zamba.Controls.UcRespuesta()
        Me.Panel2.SuspendLayout()
        Me.pnlMessages.SuspendLayout()
        Me.pnlForumToolbar.SuspendLayout()
        Me.tabControlMessage.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.Panel2.Controls.Add(Me.pnlMessages)
        Me.Panel2.Controls.Add(Me.pnlForumToolbar)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(674, 682)
        Me.Panel2.TabIndex = 8
        '
        'pnlMessages
        '
        Me.pnlMessages.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.pnlMessages.Controls.Add(Me.UcRespuesta1)
        Me.pnlMessages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMessages.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMessages.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.pnlMessages.Location = New System.Drawing.Point(0, 38)
        Me.pnlMessages.Name = "pnlMessages"
        Me.pnlMessages.Size = New System.Drawing.Size(674, 644)
        Me.pnlMessages.TabIndex = 9
        '
        'pnlForumToolbar
        '
        Me.pnlForumToolbar.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.pnlForumToolbar.Controls.Add(Me.btnReenviar)
        Me.pnlForumToolbar.Controls.Add(Me.btnEliminar)
        Me.pnlForumToolbar.Controls.Add(Me.btnResponder)
        Me.pnlForumToolbar.Controls.Add(Me.btnNuevo)
        Me.pnlForumToolbar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlForumToolbar.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlForumToolbar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.pnlForumToolbar.Location = New System.Drawing.Point(0, 0)
        Me.pnlForumToolbar.Name = "pnlForumToolbar"
        Me.pnlForumToolbar.Size = New System.Drawing.Size(674, 38)
        Me.pnlForumToolbar.TabIndex = 8
        '
        'btnReenviar
        '
        Me.btnReenviar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnReenviar.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnReenviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReenviar.ForeColor = System.Drawing.Color.White
        Me.btnReenviar.Location = New System.Drawing.Point(426, 6)
        Me.btnReenviar.Name = "btnReenviar"
        Me.btnReenviar.Size = New System.Drawing.Size(102, 26)
        Me.btnReenviar.TabIndex = 15
        Me.btnReenviar.Text = "REENVIAR"
        Me.btnReenviar.UseVisualStyleBackColor = False
        Me.btnReenviar.Visible = False
        '
        'btnEliminar
        '
        Me.btnEliminar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnEliminar.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEliminar.ForeColor = System.Drawing.Color.White
        Me.btnEliminar.Location = New System.Drawing.Point(138, 6)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(102, 26)
        Me.btnEliminar.TabIndex = 14
        Me.btnEliminar.Text = "ELIMINAR"
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnResponder
        '
        Me.btnResponder.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnResponder.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnResponder.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnResponder.ForeColor = System.Drawing.Color.White
        Me.btnResponder.Location = New System.Drawing.Point(318, 6)
        Me.btnResponder.Name = "btnResponder"
        Me.btnResponder.Size = New System.Drawing.Size(102, 26)
        Me.btnResponder.TabIndex = 13
        Me.btnResponder.Text = "RESPONDER"
        Me.btnResponder.UseVisualStyleBackColor = False
        Me.btnResponder.Visible = False
        '
        'btnNuevo
        '
        Me.btnNuevo.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnNuevo.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNuevo.ForeColor = System.Drawing.Color.White
        Me.btnNuevo.Location = New System.Drawing.Point(7, 6)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(102, 26)
        Me.btnNuevo.TabIndex = 12
        Me.btnNuevo.Text = "NUEVO"
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
        Me.tabControlMessage.Size = New System.Drawing.Size(376, 682)
        Me.tabControlMessage.TabIndex = 0
        '
        'tabMessage
        '
        Me.tabMessage.BackColor = System.Drawing.Color.White
        Me.tabMessage.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.tabMessage.Location = New System.Drawing.Point(4, 25)
        Me.tabMessage.Name = "tabMessage"
        Me.tabMessage.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMessage.Size = New System.Drawing.Size(368, 653)
        Me.tabMessage.TabIndex = 0
        Me.tabMessage.Text = "Mensaje"
        Me.tabMessage.UseVisualStyleBackColor = True
        '
        'tabAttachs
        '
        Me.tabAttachs.BackColor = System.Drawing.Color.White
        Me.tabAttachs.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabAttachs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.tabAttachs.Location = New System.Drawing.Point(4, 25)
        Me.tabAttachs.Name = "tabAttachs"
        Me.tabAttachs.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAttachs.Size = New System.Drawing.Size(368, 653)
        Me.tabAttachs.TabIndex = 1
        Me.tabAttachs.Text = "Adjuntos"
        Me.tabAttachs.UseVisualStyleBackColor = True
        '
        'tabParticipants
        '
        Me.tabParticipants.BackColor = System.Drawing.Color.White
        Me.tabParticipants.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabParticipants.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.tabParticipants.Location = New System.Drawing.Point(4, 25)
        Me.tabParticipants.Name = "tabParticipants"
        Me.tabParticipants.Size = New System.Drawing.Size(368, 653)
        Me.tabParticipants.TabIndex = 2
        Me.tabParticipants.Text = "Participantes"
        Me.tabParticipants.UseVisualStyleBackColor = True
        '
        'tabDetails
        '
        Me.tabDetails.BackColor = System.Drawing.Color.White
        Me.tabDetails.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.tabDetails.Location = New System.Drawing.Point(4, 25)
        Me.tabDetails.Name = "tabDetails"
        Me.tabDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDetails.Size = New System.Drawing.Size(368, 653)
        Me.tabDetails.TabIndex = 3
        Me.tabDetails.Text = "Información"
        Me.tabDetails.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.Panel4.Controls.Add(Me.tabControlMessage)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Panel4.Location = New System.Drawing.Point(677, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(376, 682)
        Me.Panel4.TabIndex = 0
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter1.Location = New System.Drawing.Point(674, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 682)
        Me.Splitter1.TabIndex = 10
        Me.Splitter1.TabStop = False
        '
        'UcRespuesta1
        '
        Me.UcRespuesta1.BackColor = System.Drawing.Color.White
        Me.UcRespuesta1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UcRespuesta1.Enabled = False
        Me.UcRespuesta1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcRespuesta1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.UcRespuesta1.HelpId = Nothing
        Me.UcRespuesta1.Location = New System.Drawing.Point(0, 436)
        Me.UcRespuesta1.Margin = New System.Windows.Forms.Padding(0)
        Me.UcRespuesta1.Name = "UcRespuesta1"
        Me.UcRespuesta1.Size = New System.Drawing.Size(674, 208)
        Me.UcRespuesta1.TabIndex = 0
        '
        'UcForo
        '
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.Panel4)
        Me.Name = "UcForo"
        Me.Size = New System.Drawing.Size(1053, 682)
        Me.Panel2.ResumeLayout(False)
        Me.pnlMessages.ResumeLayout(False)
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
    Private _mailsToNotify As New List(Of String)
    Private NuevoCual As Int32
    Private groupId As Int64
    Private resultIDAndDocTypeIds As New Generic.Dictionary(Of Int64, Int64)
    Private ResultsIds As New Generic.List(Of Int64)
    Private TaskIds As New Generic.List(Of Int64)
    Private ucAttachs As New UcForumAttachsGrid()
    Private showSelectedId As Boolean = False
    Private MensajeNuevo As System.Windows.Forms.Form
    Private loadMessage As Boolean
    Private IdMsg As Int64 = 0

    ' Evento que al invocarse permite la visualización de un formulario de mail
    Public Event showMailForm(ByRef textFor As String, ByRef textSubject As String, ByRef textBody As String, ByVal idMensaje As Int32, ByVal parentId As Int64, ByVal blnAutomaticAttachLink As Boolean, ByVal blnAutomaticSend As Boolean, ByVal attachPaths() As String)
    Public Event showMailFormDoForo(ByRef textFor As String, ByRef textSubject As String, ByRef textBody As String, ByVal idMensaje As Int32, ByVal parentId As Int64, ByVal blnAutomaticAttachLink As Boolean, ByVal blnAutomaticSend As Boolean, ByVal attachPaths() As String, ByVal results As Generic.List(Of IResult))
#End Region

#Region "Propiedades"
    Private _isRule As Boolean
    Private _results As Generic.List(Of IResult)
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
    Public Sub New(ByVal User As IUser, ByVal _resultIDAndDocTypeIds As Generic.Dictionary(Of Int64, Int64), ByVal isRule As Boolean, ByVal results As System.Collections.Generic.List(Of IResult))
        MyBase.New()
        _isRule = isRule
        _results = results
        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        resultIDAndDocTypeIds = _resultIDAndDocTypeIds
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

        resultIDAndDocTypeIds.Add(_resultID, _docTypeID)
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
        Try
            If loadGraphics Then
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
                RemoveHandler UcRespuesta1.NuevoMensaje, AddressOf NuevoMensaje
                AddHandler UcRespuesta1.NuevoMensaje, AddressOf NuevoMensaje

                'Ajusta los splitters
                SetSplitters()

                'Busca por los ids de las tareas
                For Each rId As Int32 In ResultsIds
                    TaskIds.AddRange(WF.WF.WFTaskBusiness.GetTaskIDsByDocId(rId))
                Next
            End If

            LeyendoMensaje = True
            UcMensaje.Bloquear(LeyendoMensaje)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Métodos y eventos"

    Public Shadows Sub ShowInfo(ByVal ResultId As Int64, ByVal DocTypeId As Int64)
        resultIDAndDocTypeIds.Clear()
        resultIDAndDocTypeIds.Add(ResultId, DocTypeId)
        ResultsIds.Clear()
        ResultsIds.Add(ResultId)
        Parent.Text = "Foro (" & UcQuestion.ForoTreeView.GetNodeCount(True) & " mensaje/s)"
        btnEliminar.Visible = UserBusiness.Rights.GetUserRights(User, ObjectTypes.DocTypes, RightsType.DeleteMsgForum, DocTypeId)
        FiltrarAlIniciar()
    End Sub
    Private Sub FiltrarAlIniciar()
        Try
            'Se obtienen los mensajes
            Dim ArrayMensajes As New ArrayList '= ForoBusiness.GetAllMessages(ResultId)
            Dim ArrayRespuestas As New ArrayList '= ForoBusiness.GetAllAnswers(ResultId)
            Dim lastVersionId As Int64

            Zamba.Core.ZForoBusiness.GetAllMessages(ResultsIds(0), resultIDAndDocTypeIds.Item(ResultsIds(0)), ArrayMensajes, ArrayRespuestas, false, 0)

            Try
                If ArrayMensajes.Count = 0 Then
                    UcQuestion.SinResultados(True)
                    UcMensaje.Bloquear(False)
                Else
                    UcQuestion.SinResultados(False)
                End If
            Catch ex As Exception
                UcQuestion.SinResultados(True)
            End Try
            UcQuestion.CargarEnTreeview(ArrayMensajes, ArrayRespuestas)
            '[Pablo] se comenta para incrementar la performance al mostrar los mensajes en el foro
            Parent.Text = "Foro (" & UcQuestion.ForoTreeView.GetNodeCount(True) & " mensaje/s)"
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

            Zamba.Core.ZForoBusiness.GetAllMessages(ResultsIds(0), resultIDAndDocTypeIds(ResultsIds(0)), ArrayMensajes, ArrayRespuestas, False, 0)

            UcQuestion.CargarEnTreeview(ArrayMensajes, ArrayRespuestas)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub PreguntaSeleccionada(ByVal Mensaje As MensajeForo, Optional ByVal forceRefresh As Boolean = False)
        LeyendoMensaje = True
        UcMensaje.Bloquear(LeyendoMensaje)

        'TODO: Para una mejor performance se debería verificar que solapa se encuentra
        'seleccionada y luego cargar unicamente esa solapa. Luego, al seleccionar otra
        'solapa debería cargar los datos de aquella solapa. Los UC deberían tener una
        'variable interna con el MessageId para no cargar 2 veces los mismos datos.
        UcMensaje.FillMensaje(Mensaje)
        ucAttachs.FillAttachs(Mensaje.ID)
        UcDetails.FillDetails(Mensaje)

        Dim resultId As Int64 = ResultsIds(0)
        Dim taskId As Int64 = 0
        If TaskIds.Count > 0 Then
            taskId = TaskIds(0)
        End If
        UcParticipants.FillParticipants(UcQuestion.GetParentMessageId(Mensaje.ID), User.ID, resultId, resultIDAndDocTypeIds(resultId), forceRefresh, taskId, Mensaje.Name)

        Me.UcRespuesta1.Enabled = True
        Dim ArrayMensaje As New ArrayList
        ArrayMensaje = UcQuestion.GetSeleccionado
        Dim idMsjActual As Int64 = Int64.Parse(ArrayMensaje(0).ToString)

        UcRespuesta1.SetAnswer(User, ResultsIds, Nothing, Nothing, Nothing, ArrayMensaje(1).ToString, ZForoBusiness.GetUserAndGroupsParticipantsId(UcQuestion.GetParentMessageId(idMsjActual)))
        NuevoCual = 2

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
                    Log(id, RightsType.Delete, "Se borro un mensaje de foro cuyo ID era: " & id.ToString)
                Next
            End If

            '[Pablo] se comenta para incrementar la performance al mostrar los mensajes en el foro
            'muestro en el tab la cantidad de mensajes que hay en total, sumando mesajes y respuestas.
            Parent.Text = "Foro (" & UcQuestion.ForoTreeView.GetNodeCount(True) & " mensaje/s)"
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEliminar.Click
        Try
            If UcQuestion.ForoTreeView.SelectedNode IsNot Nothing Then
                If MessageBox.Show("¿Desea eliminar el mensaje seleccionado?" + vbCrLf + "Además del seleccionado se eliminarán todas sus respuestas.", _
                       "Eliminar mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    EliminarSelected()
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnResponder_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnResponder.Click
        Try
            'Si el mensaje al cual responder existe
            If Not IsNothing(UcQuestion.GetSeleccionado) Then
                'Se crea y setea el form donde va a estar el 
                'control UCNuevoMensaje
                MensajeNuevo = New System.Windows.Forms.Form
                MensajeNuevo.StartPosition = FormStartPosition.CenterScreen
                MensajeNuevo.FormBorderStyle = FormBorderStyle.FixedDialog
                MensajeNuevo.MaximizeBox = False
                'MensajeNuevo.MinimizeBox = False

                Dim ArrayMensaje As New ArrayList
                ArrayMensaje = UcQuestion.GetSeleccionado
                Dim idMsjActual As Int64 = Int64.Parse(ArrayMensaje(0).ToString)

                Dim UCMsgNew As New UcNuevoMensaje(User, ResultsIds, Nothing, Nothing, Nothing, ArrayMensaje(1).ToString, ZForoBusiness.GetUserAndGroupsParticipantsId(UcQuestion.GetParentMessageId(idMsjActual)))
                UCMsgNew.txtAsunto.Enabled = False

                If User.ID = ZForoBusiness.GetCreatorId(idMsjActual) OrElse UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Foro, RightsType.ChangeParticipants, resultIDAndDocTypeIds(ResultsIds(0))) Then
                    UCMsgNew.UsersButtonEnabled = True
                Else
                    UCMsgNew.UsersButtonEnabled = False
                End If

                MensajeNuevo.Height = UCMsgNew.Height + 25
                MensajeNuevo.Width = UCMsgNew.Width
                MensajeNuevo.Controls.Add(UCMsgNew)
                RemoveHandler UCMsgNew.NuevoMensaje, AddressOf NuevoMensaje
                AddHandler UCMsgNew.NuevoMensaje, AddressOf NuevoMensaje

                NuevoCual = 2

                If IsNothing(MensajeNuevo) = False Then MensajeNuevo.Show()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnReenviar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnReenviar.Click
        If Not IsNothing(UcQuestion.ForoTreeView.SelectedNode) Then
            Dim response As System.Windows.Forms.DialogResult = _
               MessageBox.Show("Se enviará a los participantes una notificacion sobre este mensaje, ¿Desea continuar?", _
                               "Zamba Software", MessageBoxButtons.OKCancel)
            If response = DialogResult.OK Then
                Dim ZNode As ZNode = DirectCast(UcQuestion.ForoTreeView.SelectedNode, ZNode)
                Dim Mensaje As MensajeForo = DirectCast(ZNode.ZambaCore, MensajeForo)
                Dim body As New StringBuilder()
                Dim userMails As New List(Of String)


                Try
                    Dim name As String
                    If Not IsNothing(resultIDAndDocTypeIds) Then
                        If resultIDAndDocTypeIds.Count > 0 Then
                            For Each idAndDocTypeID As KeyValuePair(Of Long, Long) In resultIDAndDocTypeIds
                                name = Results_Business.GetName(idAndDocTypeID.Key, idAndDocTypeID.Value) & " - "
                            Next
                            name = name.Remove(name.Length - 3) & ": " & Mensaje.Name
                        End If
                    End If

                    body.Append("El usuario ")
                    body.Append(Membership.MembershipHelper.CurrentUser.Name)
                    body.AppendLine(" ha agregado/respondido una conversación de foro")
                    If String.IsNullOrEmpty(name) = False Then
                        body.Append(", referente a la siguiente Tarea:")
                        body.Append(name)
                    End If
                    body.AppendLine(", con el siguiente detalle: ")
                    body.Append(Mensaje.Mensaje)

                    'Si es una respuesta y se debe responder unicamente al creador
                    'If DirectCast(MensajeNuevo.Controls(0), UcNuevoMensaje).NotifyCreatorOnly Then
                    'RaiseEvent showMailForm(NotifyBusiness.GetUserMail(Mensaje.ParentId), Mensaje.Name, Mensaje.Mensaje, Mensaje.ID, Mensaje.ParentId, blnAutomaticAttachLink, blnAutomaticSend, ZForoBusiness.GetAttachs(Mensaje.ID))

                    'Caso contrario, se verifica si existen mails a notificar.
                    'Else
                    Dim notifyIds As List(Of Long) = ZForoBusiness.GetUserAndGroupsParticipantsId(UcQuestion.GetParentMessageId(IdMsg))
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
                        Dim attachs As New List(Of String)
                        For Each r As DataRow In ZForoBusiness.GetAttachs(Mensaje.ID).Rows
                            attachs.Add(r("PATH"))
                        Next

                        If _isRule Then
                            RaiseEvent showMailFormDoForo(GetMailList(userMails), name, body.ToString(), Mensaje.ID, Mensaje.ParentId, true, true, attachs.ToArray(), _results)
                        Else
                            RaiseEvent showMailForm(GetMailList(userMails), name, body.ToString(), Mensaje.ID, Mensaje.ParentId, true, true, attachs.ToArray())
                        End If

                        attachs.Clear()
                        attachs = Nothing
                    End If
                    ' End If
                    _mailsToNotify.Clear()
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                Finally
                    body = Nothing
                    If Not IsNothing(userMails) Then
                        userMails.Clear()
                        userMails = Nothing
                    End If
                End Try
            End If
        End If
    End Sub

    'Este método emula lo que hace el evento Click del nuevo, pero 
    'aparte utiliza los valores pasados por parámetro
    Public Function btnNuevo_ClickSinEvento(ByRef _subject As String, ByRef _body As String, _
                                            ByRef _flagSaved As String, ByRef _Participants As Generic.List(Of Long) _
                                            , ByVal RuleID As Int64, ByVal BtnName As String _
                                            , ByVal Result As Core.ITaskResult) As Int64
        Try
            'Se crea y setea el form donde va a estar el 
            'control UCNuevoMensaje
            MensajeNuevo = New System.Windows.Forms.Form()
            MensajeNuevo.StartPosition = FormStartPosition.CenterScreen
            MensajeNuevo.FormBorderStyle = FormBorderStyle.FixedDialog
            MensajeNuevo.MaximizeBox = False
            'MensajeNuevo.MinimizeBox = False

            'Se instancia el nuevo control
            Dim UCMsgNew As UcNuevoMensaje
            If Not _Participants Is Nothing Then
                UCMsgNew = New UcNuevoMensaje(User, ResultsIds, RuleID, BtnName, Result, "", _Participants)
            Else
                UCMsgNew = New UcNuevoMensaje(User, ResultsIds, RuleID, BtnName, Result, "", Nothing)
            End If

            'Se le cargan los valores que se pasan por parámetro
            UCMsgNew.txtAsunto.Text = _subject
            Dim fbody As String
            fbody = _body.Replace(Chr(13), vbCrLf)
            fbody = fbody.Replace(Chr(10), vbCrLf)
            fbody = fbody.Replace((Chr(13) + Chr(10)), vbCrLf)

            UCMsgNew.txtMensaje.Text = fbody
            UCMsgNew.UsersButtonEnabled = True

            MensajeNuevo.Height = UCMsgNew.Height + 35
            MensajeNuevo.Width = UCMsgNew.Width
            MensajeNuevo.Dock = DockStyle.Fill
            MensajeNuevo.Controls.Add(UCMsgNew)

            'Se setean los delegados para guardar el mensaje y guardar y notificar
            RemoveHandler UCMsgNew.NuevoMensaje, AddressOf NuevoMensaje
            AddHandler UCMsgNew.NuevoMensaje, AddressOf NuevoMensaje

            NuevoCual = 1

            If IsNothing(MensajeNuevo) = False Then
                MensajeNuevo.Show()
                'Se setea el flag de salida
                If Not IsNothing(UCMsgNew) Then
                    If DialogResult.OK = UCMsgNew.DialogResult Then
                        'Se le cargan los valores que se pasan por parámetro
                        _subject = UCMsgNew.asunto
                        _body = UCMsgNew.mensaje
                        _flagSaved = "True"
                        Return IdMsg
                    Else
                        _flagSaved = "False"
                        'logueo de accion de cancelacion
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
    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnNuevo.Click
        Try

            'Se crea y setea el form donde va a estar el 
            'control UCNuevoMensaje
            If Not MensajeNuevo Is Nothing Then
                Dim result As DialogResult = MsgBox("Está actualmente creando una nuevo mensaje. ¿Desea salir del existente y crear uno nuevo?", MsgBoxStyle.OkCancel, "Zamba SoftWare")
                If result = DialogResult.OK Then
                    MensajeNuevo.Close()
                Else
                    Exit Sub
                End If
            End If

            MensajeNuevo = New System.Windows.Forms.Form
            MensajeNuevo.StartPosition = FormStartPosition.CenterScreen
            MensajeNuevo.FormBorderStyle = FormBorderStyle.FixedSingle
            MensajeNuevo.Height = 550
            MensajeNuevo.Width = 600
            'MensajeNuevo.MaximizeBox = False
            'MensajeNuevo.MinimizeBox = False

            Dim UCMsgNew As New UcNuevoMensaje(User, ResultsIds, -1, Nothing, Nothing, "", Nothing)
            UCMsgNew.UsersButtonEnabled = True
            MensajeNuevo.Controls.Add(UCMsgNew)
            UCMsgNew.Dock = DockStyle.Fill
            'MensajeNuevo.Height = UCMsgNew.Height + 25
            'MensajeNuevo.Width = UCMsgNew.Width
            RemoveHandler UCMsgNew.NuevoMensaje, AddressOf NuevoMensaje
            AddHandler UCMsgNew.NuevoMensaje, AddressOf NuevoMensaje

            RemoveHandler MensajeNuevo.FormClosed, AddressOf MensajeNuevo_FormClosed
            AddHandler MensajeNuevo.FormClosed, AddressOf MensajeNuevo_FormClosed
            NuevoCual = 1

            If IsNothing(MensajeNuevo) = False Then MensajeNuevo.Show()

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub MensajeNuevo_FormClosed()
        MensajeNuevo = Nothing
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
    Private Sub NuevoMensaje(ByVal Asunto As String, ByVal Texto As String, ByVal User As IUser, ByVal notificar As Boolean, ByVal attachs As List(Of String), ByVal notifyIds As Generic.List(Of Int64), ByVal blnAutomaticAttachLink As Boolean, ByVal blnAutomaticSend As Boolean)
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
                ZForo_Factory.InsertMessage(IdMsg, ParentId, Asunto.Replace("'", "''"), Texto.Replace("'", "''"), User.ID)

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
                For Each id As Generic.KeyValuePair(Of Int64, Int64) In resultIDAndDocTypeIds
                    ZForoBusiness.InsertMessageDoc(IdMsg, id.Key, id.Value)
                Next

                'Se agregan los adjuntos.
                If attachs IsNot Nothing Then
                    ucAttachs.CreateAttachs(IdMsg, attachs)
                    attachPaths = attachs.ToArray
                End If

                Log(IdMsg, RightsType.NuevoTemaGuardar, "Se agregó un nuevo mensaje al foro con asunto: " & Asunto)


                'Se mueve de lugar esta asignacion (antes estaba en la creación del nodo)
                If loadMessage Then
                    NuevoCual = 0
                End If

            ElseIf NuevoCual = 2 Then ' Respuesta
                ArrayMensaje = UcQuestion.GetSeleccionado
                If ArrayMensaje Is Nothing Then
                    MessageBox.Show("Debe seleccionar un mensaje para responder")
                    Exit Sub
                End If
                ParentId = CLng(ArrayMensaje(0))

                'Inserta los datos de la respuesta.
                Zamba.Core.ZForoBusiness.InsertMessage(IdMsg, ParentId, Asunto.Replace("'", "''"), Texto.Replace("'", "''"), User.ID)

                'Si el creador no se encuentra seleccionado, se agrega.
                If Not notifyIds.Contains(User.ID) Then
                    notifyIds.Add(User.ID)
                End If

                'Refresca los participantes en caso de tener permiso de hacerlo
                If User.ID = ZForoBusiness.GetCreatorId(ParentId) OrElse UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Foro, RightsType.ChangeParticipants, resultIDAndDocTypeIds(ResultsIds(0))) Then
                    ZForoBusiness.InsertMessageParticipants(UcQuestion.GetParentMessageId(IdMsg), notifyIds)
                End If

                'Se agrega el mensaje al documento seleccionado.
                ZForoBusiness.InsertMessageDoc(IdMsg, ResultsIds(0), resultIDAndDocTypeIds.Item(ResultsIds(0)))

                'Verifica si debe insertar la respuesta en otros documentos (habrá más de uno con la DoForo).
                Dim relatedDocs As DataTable = ZForoBusiness.GetRealtedDocs(ParentId, ResultsIds(0))
                For Each dr As DataRow In relatedDocs.Rows
                    ZForoBusiness.InsertMessageDoc(IdMsg, dr(0).ToString, dr(1).ToString)
                Next

                'Se agregan los adjuntos.
                If attachs IsNot Nothing Then
                    ucAttachs.CreateAttachs(IdMsg, attachs)
                    attachPaths = attachs.ToArray
                End If

                Log(IdMsg, RightsType.ResponderGuardar, "Se agregó una respuesta con asunto: " & Asunto & " haciendo referencia al mensaje con ID " & ParentId)
            End If

            'Agrega el nodo del mensaje al foro (con la DoForo no pasa).
            If loadMessage Then
                Dim Mensaje As New MensajeForo

                Mensaje.Mensaje = Texto
                Mensaje.UserId = CInt(User.ID)
                Mensaje.Fecha = Now
                Mensaje.UserName = Membership.MembershipHelper.CurrentUser.Apellidos + " " + Membership.MembershipHelper.CurrentUser.Nombres
                Mensaje.DiasVto = 0
                Mensaje.ParentId = ParentId
                Mensaje.ID = IdMsg
                Mensaje.Name = Asunto

                UcQuestion.CargarMsgEnTreeview(Mensaje)
                UcQuestion.SinResultados(False)
                Parent.Text = "Foro (" & UcQuestion.ForoTreeView.GetNodeCount(True) & " mensaje/s)"
                'NuevoCual = 0
                UcMensaje.Bloquear(LeyendoMensaje)
            End If

            If IsNothing(MensajeNuevo) = False Then MensajeNuevo.Hide()

            'Realiza la notificación del mensaje.
            If notificar Then
                Dim name As String
                If Not IsNothing(resultIDAndDocTypeIds) Then
                    If resultIDAndDocTypeIds.Count > 0 Then
                        For Each idAndDocTypeID As KeyValuePair(Of Long, Long) In resultIDAndDocTypeIds
                            name = Results_Business.GetName(idAndDocTypeID.Key, idAndDocTypeID.Value) & " - "
                        Next
                        Asunto = name.Remove(name.Length - 3) & ": " & Asunto
                    End If
                End If

                Dim body As New StringBuilder()
                body.Append("El usuario ")
                body.Append(Membership.MembershipHelper.CurrentUser.Name)
                body.Append(" ha agregado/respondido una conversación de foro")
                If String.IsNullOrEmpty(name) = False Then
                    body.AppendLine(", referente a la siguiente Tarea:")
                    body.Append(name)
                Else
                    body.AppendLine(",")
                End If
                body.AppendLine(" con el siguiente detalle: ")
                body.Append(Texto)

                _mailsToNotify.Clear()
                Dim userMails As New List(Of String)
                'Si es una respuesta y se debe responder unicamente al creador

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
                    If _isRule Then
                        RaiseEvent showMailFormDoForo(GetMailList(userMails), Asunto, body.ToString(), IdMsg, ParentId, blnAutomaticAttachLink, blnAutomaticSend, attachPaths, _results)
                    Else
                        RaiseEvent showMailForm(GetMailList(userMails), Asunto, body.ToString(), IdMsg, ParentId, blnAutomaticAttachLink, blnAutomaticSend, attachPaths)
                    End If
                End If

                _mailsToNotify.Clear()
                body = Nothing
            End If


        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            MensajeNuevo.Close()
            If Not IsNothing(MensajeNuevo) Then
                For i As Int16 = 0 To MensajeNuevo.Controls.Count - 1
                    MensajeNuevo.Controls(i).Dispose()
                Next
                MensajeNuevo.Dispose()
                MensajeNuevo = Nothing
            End If

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
    Private Function GetMailList(ByVal List As List(Of String)) As String
        Dim MailList As String = String.Empty
        For Each x As String In List
            MailList &= x & ";"
        Next
        MailList = MailList.Remove(MailList.Length - 1, 1)
        Return MailList
    End Function

    Public Sub SetSplitters()
        Try
            Splitter1.Top = CInt(Height / 2)
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
        If Not IsNothing(MensajeNuevo) Then
            For i As Int16 = 0 To MensajeNuevo.Controls.Count - 1
                MensajeNuevo.Controls(i).Dispose()
            Next
            MensajeNuevo.Dispose()
            'MensajeNuevo = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Guarda un log de lo ocurrido en el historial del usuario y de la tarea (en caso de poder hacerlo)
    ''' </summary>
    ''' <param name="message"></param>
    ''' <remarks></remarks>
    Private Sub Log(ByVal id As Int64, ByVal rTypes As RightsType, ByVal message As String)
        'Guarda en el historial de tarea
        UserBusiness.Rights.SaveAction(id, ObjectTypes.Foro, rTypes, message)

        'Guarda en las tareas existentes
        For Each taskId As Int64 In TaskIds
            WF.WF.WFTaskBusiness.LogAction(taskId, message)
        Next
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
