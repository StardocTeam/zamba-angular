Imports Zamba.Core
Imports System.Collections.Generic

Public Class UcNuevoMensaje
    Inherits Zamba.AppBlock.ZControl
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
                If Not (components Is Nothing) Then components.Dispose()
                If attachs IsNot Nothing Then attachs.Clear()
                If btnAddAtach IsNot Nothing Then btnAddAtach.Dispose()

                If btnNotificar IsNot Nothing Then btnNotificar.Dispose()
                If btnRemoveAll IsNot Nothing Then btnRemoveAll.Dispose()
                If btnRemoveAttach IsNot Nothing Then btnRemoveAttach.Dispose()
                If btnUsuarios IsNot Nothing Then btnUsuarios.Dispose()


                If grpAttachs IsNot Nothing Then grpAttachs.Dispose()
                If Label1 IsNot Nothing Then Label1.Dispose()
                If Label2 IsNot Nothing Then Label2.Dispose()
                If lstAttachs IsNot Nothing Then lstAttachs.Dispose()

                If pnlButtons IsNot Nothing Then
                    pnlButtons.Controls.Clear()
                    pnlButtons.Dispose()
                End If
                If txtAsunto IsNot Nothing Then txtAsunto.Dispose()
                If txtMensaje IsNot Nothing Then txtMensaje.Dispose()

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
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Public WithEvents txtMensaje As TextBox
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Public WithEvents txtAsunto As TextBox
    Friend WithEvents btnUsuarios As ZButton
    Friend WithEvents btnNotificar As ZButton
    Friend WithEvents grpAttachs As GroupBox
    Friend WithEvents btnRemoveAttach As ZButton
    Friend WithEvents btnAddAtach As ZButton
    Friend WithEvents lstAttachs As ListBox
    Friend WithEvents btnRemoveAll As ZButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        pnlButtons = New System.Windows.Forms.Panel()
        btnUsuarios = New ZButton()
        btnNotificar = New ZButton()
        txtMensaje = New TextBox()
        txtAsunto = New TextBox()
        Label1 = New ZLabel()
        Label2 = New ZLabel()
        grpAttachs = New GroupBox()
        btnRemoveAll = New ZButton()
        btnRemoveAttach = New ZButton()
        btnAddAtach = New ZButton()
        lstAttachs = New ListBox()
        pnlButtons.SuspendLayout()
        grpAttachs.SuspendLayout()
        SuspendLayout()
        '
        'pnlButtons
        '
        pnlButtons.BackColor = System.Drawing.Color.White
        pnlButtons.Controls.Add(btnUsuarios)
        pnlButtons.Controls.Add(btnNotificar)
        pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        pnlButtons.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        pnlButtons.ForeColor = System.Drawing.Color.DarkBlue
        pnlButtons.Location = New System.Drawing.Point(0, 446)
        pnlButtons.Name = "pnlButtons"
        pnlButtons.Size = New System.Drawing.Size(550, 54)
        pnlButtons.TabIndex = 9
        '
        'btnUsuarios
        '
        btnUsuarios.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnUsuarios.FlatStyle = FlatStyle.Popup
        btnUsuarios.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnUsuarios.ForeColor = System.Drawing.Color.White
        btnUsuarios.Location = New System.Drawing.Point(143, 12)
        btnUsuarios.Name = "btnUsuarios"
        btnUsuarios.Size = New System.Drawing.Size(118, 25)
        btnUsuarios.TabIndex = 19
        btnUsuarios.Text = "Participantes"
        btnUsuarios.UseVisualStyleBackColor = False
        '
        'btnNotificar
        '
        btnNotificar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnNotificar.FlatStyle = FlatStyle.Popup
        btnNotificar.ForeColor = System.Drawing.Color.White
        btnNotificar.Location = New System.Drawing.Point(19, 12)
        btnNotificar.Name = "btnNotificar"
        btnNotificar.Size = New System.Drawing.Size(118, 25)
        btnNotificar.TabIndex = 18
        btnNotificar.Text = "Enviar"
        btnNotificar.UseVisualStyleBackColor = False
        '
        'txtMensaje
        '
        txtMensaje.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtMensaje.BackColor = System.Drawing.Color.White
        txtMensaje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtMensaje.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtMensaje.ForeColor = System.Drawing.Color.Navy
        txtMensaje.Location = New System.Drawing.Point(18, 69)
        txtMensaje.MaxLength = 3999
        txtMensaje.Multiline = True
        txtMensaje.Name = "txtMensaje"
        txtMensaje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        txtMensaje.Size = New System.Drawing.Size(516, 131)
        txtMensaje.TabIndex = 1
        '
        'txtAsunto
        '
        txtAsunto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtAsunto.BackColor = System.Drawing.Color.White
        txtAsunto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtAsunto.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtAsunto.ForeColor = System.Drawing.Color.Navy
        txtAsunto.Location = New System.Drawing.Point(91, 16)
        txtAsunto.Name = "txtAsunto"
        txtAsunto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        txtAsunto.Size = New System.Drawing.Size(443, 23)
        txtAsunto.TabIndex = 0
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.FontSize = 9.0!
        Label1.ForeColor = System.Drawing.Color.MidnightBlue
        Label1.Location = New System.Drawing.Point(15, 19)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(55, 14)
        Label1.TabIndex = 23
        Label1.Text = "ASUNTO"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.FontSize = 9.0!
        Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Label2.Location = New System.Drawing.Point(16, 52)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(58, 14)
        Label2.TabIndex = 24
        Label2.Text = "MENSAJE"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'grpAttachs
        '
        grpAttachs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grpAttachs.Controls.Add(btnRemoveAll)
        grpAttachs.Controls.Add(btnRemoveAttach)
        grpAttachs.Controls.Add(btnAddAtach)
        grpAttachs.Controls.Add(lstAttachs)
        grpAttachs.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        grpAttachs.ForeColor = System.Drawing.Color.MidnightBlue
        grpAttachs.Location = New System.Drawing.Point(19, 216)
        grpAttachs.Name = "grpAttachs"
        grpAttachs.Size = New System.Drawing.Size(515, 224)
        grpAttachs.TabIndex = 25
        grpAttachs.TabStop = False
        grpAttachs.Text = "ADJUNTOS"
        '
        'btnRemoveAll
        '
        btnRemoveAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnRemoveAll.FlatStyle = FlatStyle.Flat
        btnRemoveAll.ForeColor = System.Drawing.Color.White
        btnRemoveAll.Location = New System.Drawing.Point(171, 22)
        btnRemoveAll.Name = "btnRemoveAll"
        btnRemoveAll.Size = New System.Drawing.Size(103, 31)
        btnRemoveAll.TabIndex = 3
        btnRemoveAll.Text = "Remover todos"
        btnRemoveAll.UseVisualStyleBackColor = True
        '
        'btnRemoveAttach
        '
        btnRemoveAttach.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnRemoveAttach.FlatStyle = FlatStyle.Flat
        btnRemoveAttach.ForeColor = System.Drawing.Color.White
        btnRemoveAttach.Location = New System.Drawing.Point(89, 22)
        btnRemoveAttach.Name = "btnRemoveAttach"
        btnRemoveAttach.Size = New System.Drawing.Size(75, 31)
        btnRemoveAttach.TabIndex = 2
        btnRemoveAttach.Text = "Remover"
        btnRemoveAttach.UseVisualStyleBackColor = True
        '
        'btnAddAtach
        '
        btnAddAtach.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAddAtach.FlatStyle = FlatStyle.Flat
        btnAddAtach.ForeColor = System.Drawing.Color.White
        btnAddAtach.Location = New System.Drawing.Point(7, 22)
        btnAddAtach.Name = "btnAddAtach"
        btnAddAtach.Size = New System.Drawing.Size(75, 31)
        btnAddAtach.TabIndex = 1
        btnAddAtach.Text = "Agregar"
        btnAddAtach.UseVisualStyleBackColor = True
        '
        'lstAttachs
        '
        lstAttachs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lstAttachs.FormattingEnabled = True
        lstAttachs.HorizontalScrollbar = True
        lstAttachs.ItemHeight = 14
        lstAttachs.Location = New System.Drawing.Point(6, 59)
        lstAttachs.Name = "lstAttachs"
        lstAttachs.Size = New System.Drawing.Size(502, 158)
        lstAttachs.TabIndex = 0
        '
        'UcNuevoMensaje
        '
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        BackColor = System.Drawing.Color.White
        Controls.Add(grpAttachs)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(txtMensaje)
        Controls.Add(txtAsunto)
        Controls.Add(pnlButtons)
        Margin = New System.Windows.Forms.Padding(0)
        Name = "UcNuevoMensaje"
        Size = New System.Drawing.Size(550, 500)
        pnlButtons.ResumeLayout(False)
        grpAttachs.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

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
    Private notifyIds As New Generic.List(Of Int64)

    Private _RuleExecuteID As Int64
    Private _ITaskResult As ITaskResult = Nothing
#End Region

#Region "Propiedades"
    Public WriteOnly Property UsersButtonEnabled() As Boolean
        Set(ByVal value As Boolean)
            btnUsuarios.Enabled = value
            '    If value Then
            '        Me.btnUsuarios.BackColor = System.Drawing.Color.White
            '    Else
            '        Me.btnUsuarios.BackColor = System.Drawing.Color.Gray
            '    End If
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

#End Region

#Region "Constructor"
    Public Sub New(ByVal User As IUser, ByVal _docIds As Generic.List(Of Int64),
                   ByVal RuleID As Int64, ByVal BtnName As String,
                   ByVal Result As ITaskResult, Optional ByVal Asunto As String = "",
                   Optional ByVal participantIds As Generic.List(Of Int64) = Nothing)

        MyBase.New()

        _Asunto = Asunto
        Me.User = User
        docIds = _docIds
        notifyIds = participantIds


        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

        'Se obtiene la ruta del servidor de adjuntos.
        Dim serverPath As String = ZOptBusiness.GetValue("ServAdjuntosRuta")

        Dim useBlob As String = ZOptBusiness.GetValue("UseBlobForumAttachments")

        'Se verifica que se haya configurado la ruta.
        If String.IsNullOrEmpty(serverPath) And (String.IsNullOrEmpty(useBlob) OrElse Boolean.Parse(useBlob) = False) Then
            grpAttachs.Enabled = False
            'MessageBox.Show("No se ha configurado la ruta de servidor de archivos adjuntos de foro." + vbCrLf + _
            '                "La opción de adjuntar quedará deshabilitada hasta que se configure dicha ruta." + vbCrLf + _
            '                "Consulte con el Departamento de Sistemas.", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            lstAttachs.Items.Add("No se ha configurado la ruta de servidor de archivos")
            lstAttachs.Items.Add("adjuntos de foro. La opción de adjuntar quedará")
            lstAttachs.Items.Add("deshabilitada hasta que dicha ruta sea configurada.")
            lstAttachs.Items.Add(String.Empty)
            lstAttachs.Items.Add("Consulte con el Departamento de Sistemas.")
        End If

        If RuleID > 0 Then
            'funcionalidad de ejecucion de regla
            _RuleExecuteID = RuleID

            _ITaskResult = DirectCast(Result, ITaskResult)

            AddButton(RuleID, BtnName)
        End If

    End Sub
#End Region

    Private Sub AddButton(ByVal RuleID As Int64, ByVal BtnName As String)

        'creo el nuevo boton
        Dim btnExecuteRule As New Button
        btnExecuteRule.BackColor = System.Drawing.Color.White
        btnExecuteRule.FlatStyle = FlatStyle.Popup
        btnExecuteRule.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnExecuteRule.Location = New System.Drawing.Point(268, 5)
        btnExecuteRule.Name = BtnName
        btnExecuteRule.Text = BtnName
        btnExecuteRule.Size = New System.Drawing.Size(98, 22)
        btnExecuteRule.AutoSize = True
        btnExecuteRule.TabIndex = 19
        btnExecuteRule.UseVisualStyleBackColor = False
        btnExecuteRule.Location = New System.Drawing.Point(372, 5)

        'asocio el evento de ejecucion de regla
        RemoveHandler btnExecuteRule.Click, AddressOf executeRule
        AddHandler btnExecuteRule.Click, AddressOf executeRule

        'lo agrego a los controles
        pnlButtons.Controls.Add(btnExecuteRule)


    End Sub

#Region "Eventos"

    Private Sub UcNuevoMensaje_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        If Not String.IsNullOrEmpty(_Asunto.Trim) Then txtAsunto.Text = "Re: " & _Asunto
        flagDialogResult = False
        DialogResult = DialogResult.No
        btnRemoveAll.Enabled = False
        btnRemoveAttach.Enabled = False
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Nuevo(False)
    End Sub

    Private Sub executeRule()
        Try
            Dim WFstep = WFStepBusiness.GetStepIdByRuleId(_RuleExecuteID, True)
            Dim WFRS As WFRulesBusiness = New WFRulesBusiness
            Dim taskResults As New System.Collections.Generic.List(Of ITaskResult)

            taskResults.Add(_ITaskResult)
            WFRS.ExecuteRule(_RuleExecuteID, WFstep, taskResults, True, Nothing)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
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
    Private Sub btnNotificar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnNotificar.Click
        Try
            Nuevo(True)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub Nuevo(ByVal notificar As Boolean)

        If txtAsunto.Text.Trim = String.Empty Then
            MessageBox.Show("Falta completar el campo Asunto.", "Atención", MessageBoxButtons.OK)
            Exit Sub
        End If
        If txtMensaje.Text.Trim = String.Empty Then
            MessageBox.Show("Falta completar el campo Mensaje.", "Atención", MessageBoxButtons.OK)
            Exit Sub
        End If

        If grpAttachs.Enabled Then
            attachs = GetAttachs()
        End If

        DialogResult = DialogResult.OK
        flagDialogResult = True

        RaiseEvent NuevoMensaje(txtAsunto.Text, txtMensaje.Text, User, notificar, attachs, notifyIds, True, True)
    End Sub

    Private Sub btnUsuarios_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnUsuarios.Click
        Dim newFrmSelectUsers As New frmSelectUsersForo(GroupToNotifyTypes.Foro, 0, notifyIds)
        newFrmSelectUsers.StartPosition = FormStartPosition.CenterParent
        newFrmSelectUsers.ShowDialog()
        notifyIds = newFrmSelectUsers.notifyIds
        newFrmSelectUsers.Dispose()
        newFrmSelectUsers = Nothing
    End Sub
#End Region

#Region "Adjuntos"
    Private Sub btnAddAtach_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAddAtach.Click
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
            If open.ShowDialog() = DialogResult.OK Then
                If open.FileNames.Length > 0 Then
                    For Each path As String In open.FileNames
                        'fileNameStartPosition = path.LastIndexOf("\")
                        'fileName = path.Substring(fileNameStartPosition)
                        'fileName = fileName.Remove(0, 1)
                        If path.Length > 1000 Then
                            Throw New Exception("La ruta del adjunto debe ser menor a los 1000 caracteres.")
                        End If
                        lstAttachs.Items.Add(path)
                    Next
                End If

                'Habilitación de los botones de remover.
                btnRemoveAll.Enabled = True
                If Not btnRemoveAttach.Enabled Then
                    RemoveHandler lstAttachs.SelectedIndexChanged, AddressOf lstAttachs_SelectedIndexChanged
                    AddHandler lstAttachs.SelectedIndexChanged, AddressOf lstAttachs_SelectedIndexChanged
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

    Private Sub btnRemoveAttach_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnRemoveAttach.Click
        Dim index As Int32 = lstAttachs.SelectedIndex

        'Si hay un item seleccionado.
        If index > -1 Then
            'Remuevo el item seleccionado.
            lstAttachs.Items.RemoveAt(index)

            'Verifica que existan adjuntos en la lista.
            If lstAttachs.Items.Count > 0 Then
                'Selecciono el item que este a continuación para facilitar la eliminación.
                If index = lstAttachs.Items.Count Then
                    lstAttachs.SelectedIndex = index - 1
                Else
                    lstAttachs.SelectedIndex = index
                End If
            Else
                'Si no hay adjuntos deshabilito los botones para remover
                btnRemoveAll.Enabled = False
                btnRemoveAttach.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnRemoveAll_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnRemoveAll.Click
        'Remueve todos los adjuntos de la lista.
        If lstAttachs.Items.Count > 0 Then
            lstAttachs.Items.Clear()
            btnRemoveAll.Enabled = False
            btnRemoveAttach.Enabled = False
        End If
    End Sub

    Private Sub lstAttachs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstAttachs.SelectedIndexChanged
        btnRemoveAttach.Enabled = True
        RemoveHandler lstAttachs.SelectedIndexChanged, AddressOf lstAttachs_SelectedIndexChanged
    End Sub

    ''' <summary>
    ''' Carga la lista con adjuntos que será utilizada para enviarla 
    ''' por parámetro al crear el mensaje.
    ''' </summary>
    Private Function GetAttachs() As List(Of String)
        If lstAttachs.Items.Count > 0 Then
            attachs = New List(Of String)

            For Each path As String In lstAttachs.Items
                attachs.Add(path)
            Next
        End If

        Return attachs
    End Function
#End Region

End Class
