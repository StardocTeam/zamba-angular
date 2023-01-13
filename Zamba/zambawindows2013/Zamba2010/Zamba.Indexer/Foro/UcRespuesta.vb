Imports Zamba.Core
Imports System.Collections.Generic

Public Class UcRespuesta
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


                If grpAttachs IsNot Nothing Then grpAttachs.Dispose()
                If lstAttachs IsNot Nothing Then lstAttachs.Dispose()

                If pnlButtons IsNot Nothing Then
                    pnlButtons.Controls.Clear()
                    pnlButtons.Dispose()
                End If
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
    Friend WithEvents btnNotificar As ZButton
    Friend WithEvents grpAttachs As GroupBox
    Friend WithEvents btnRemoveAttach As ZButton
    Friend WithEvents btnAddAtach As ZButton
    Friend WithEvents lstAttachs As ListBox
    Friend WithEvents ZPanel1 As ZPanel
    Friend WithEvents btnRemoveAll As ZButton
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnNotificar = New Zamba.AppBlock.ZButton()
        Me.txtMensaje = New System.Windows.Forms.TextBox()
        Me.grpAttachs = New System.Windows.Forms.GroupBox()
        Me.lstAttachs = New System.Windows.Forms.ListBox()
        Me.ZPanel1 = New Zamba.AppBlock.ZPanel()
        Me.btnAddAtach = New Zamba.AppBlock.ZButton()
        Me.btnRemoveAll = New Zamba.AppBlock.ZButton()
        Me.btnRemoveAttach = New Zamba.AppBlock.ZButton()
        Me.pnlButtons.SuspendLayout()
        Me.grpAttachs.SuspendLayout()
        Me.ZPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlButtons
        '
        Me.pnlButtons.BackColor = System.Drawing.Color.White
        Me.pnlButtons.Controls.Add(Me.btnNotificar)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlButtons.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlButtons.ForeColor = System.Drawing.Color.DarkBlue
        Me.pnlButtons.Location = New System.Drawing.Point(563, 0)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(156, 237)
        Me.pnlButtons.TabIndex = 9
        '
        'btnNotificar
        '
        Me.btnNotificar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnNotificar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnNotificar.ForeColor = System.Drawing.Color.White
        Me.btnNotificar.Location = New System.Drawing.Point(20, 36)
        Me.btnNotificar.Name = "btnNotificar"
        Me.btnNotificar.Size = New System.Drawing.Size(118, 25)
        Me.btnNotificar.TabIndex = 18
        Me.btnNotificar.Text = "Enviar"
        Me.btnNotificar.UseVisualStyleBackColor = False
        '
        'txtMensaje
        '
        Me.txtMensaje.BackColor = System.Drawing.Color.White
        Me.txtMensaje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMensaje.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMensaje.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMensaje.ForeColor = System.Drawing.Color.Navy
        Me.txtMensaje.Location = New System.Drawing.Point(0, 0)
        Me.txtMensaje.MaxLength = 3999
        Me.txtMensaje.Multiline = True
        Me.txtMensaje.Name = "txtMensaje"
        Me.txtMensaje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMensaje.Size = New System.Drawing.Size(563, 90)
        Me.txtMensaje.TabIndex = 1
        '
        'grpAttachs
        '
        Me.grpAttachs.Controls.Add(Me.lstAttachs)
        Me.grpAttachs.Controls.Add(Me.ZPanel1)
        Me.grpAttachs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpAttachs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpAttachs.ForeColor = System.Drawing.Color.MidnightBlue
        Me.grpAttachs.Location = New System.Drawing.Point(0, 90)
        Me.grpAttachs.Name = "grpAttachs"
        Me.grpAttachs.Size = New System.Drawing.Size(563, 147)
        Me.grpAttachs.TabIndex = 25
        Me.grpAttachs.TabStop = False
        Me.grpAttachs.Text = "ADJUNTOS"
        '
        'lstAttachs
        '
        Me.lstAttachs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstAttachs.FormattingEnabled = True
        Me.lstAttachs.HorizontalScrollbar = True
        Me.lstAttachs.ItemHeight = 14
        Me.lstAttachs.Location = New System.Drawing.Point(3, 59)
        Me.lstAttachs.Name = "lstAttachs"
        Me.lstAttachs.Size = New System.Drawing.Size(557, 85)
        Me.lstAttachs.TabIndex = 0
        '
        'ZPanel1
        '
        Me.ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ZPanel1.Controls.Add(Me.btnAddAtach)
        Me.ZPanel1.Controls.Add(Me.btnRemoveAll)
        Me.ZPanel1.Controls.Add(Me.btnRemoveAttach)
        Me.ZPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ZPanel1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZPanel1.Location = New System.Drawing.Point(3, 18)
        Me.ZPanel1.Name = "ZPanel1"
        Me.ZPanel1.Size = New System.Drawing.Size(557, 41)
        Me.ZPanel1.TabIndex = 4
        '
        'btnAddAtach
        '
        Me.btnAddAtach.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAddAtach.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddAtach.ForeColor = System.Drawing.Color.White
        Me.btnAddAtach.Location = New System.Drawing.Point(13, 3)
        Me.btnAddAtach.Name = "btnAddAtach"
        Me.btnAddAtach.Size = New System.Drawing.Size(75, 31)
        Me.btnAddAtach.TabIndex = 1
        Me.btnAddAtach.Text = "Agregar"
        Me.btnAddAtach.UseVisualStyleBackColor = True
        '
        'btnRemoveAll
        '
        Me.btnRemoveAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnRemoveAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveAll.ForeColor = System.Drawing.Color.White
        Me.btnRemoveAll.Location = New System.Drawing.Point(175, 3)
        Me.btnRemoveAll.Name = "btnRemoveAll"
        Me.btnRemoveAll.Size = New System.Drawing.Size(140, 31)
        Me.btnRemoveAll.TabIndex = 3
        Me.btnRemoveAll.Text = "Remover todos"
        Me.btnRemoveAll.UseVisualStyleBackColor = True
        '
        'btnRemoveAttach
        '
        Me.btnRemoveAttach.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnRemoveAttach.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveAttach.ForeColor = System.Drawing.Color.White
        Me.btnRemoveAttach.Location = New System.Drawing.Point(94, 3)
        Me.btnRemoveAttach.Name = "btnRemoveAttach"
        Me.btnRemoveAttach.Size = New System.Drawing.Size(75, 31)
        Me.btnRemoveAttach.TabIndex = 2
        Me.btnRemoveAttach.Text = "Remover"
        Me.btnRemoveAttach.UseVisualStyleBackColor = True
        '
        'UcRespuesta
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.txtMensaje)
        Me.Controls.Add(Me.grpAttachs)
        Me.Controls.Add(Me.pnlButtons)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "UcRespuesta"
        Me.Size = New System.Drawing.Size(719, 237)
        Me.pnlButtons.ResumeLayout(False)
        Me.grpAttachs.ResumeLayout(False)
        Me.ZPanel1.ResumeLayout(False)
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
    Private notifyIds As New Generic.List(Of Int64)

    Private _RuleExecuteID As Int64
    Private _ITaskResult As ITaskResult = Nothing
#End Region

#Region "Propiedades"
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

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

    End Sub

    Public Sub SetAnswer(ByVal User As IUser, ByVal _docIds As Generic.List(Of Int64),
                   ByVal RuleID As Int64, ByVal BtnName As String,
                   ByVal Result As ITaskResult, Optional ByVal Asunto As String = "",
                   Optional ByVal participantIds As Generic.List(Of Int64) = Nothing)

        _Asunto = Asunto
        Me.User = User
        docIds = _docIds
        notifyIds = participantIds


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

        If txtMensaje.Text.Trim = String.Empty Then
            MessageBox.Show("Falta completar el campo Mensaje.", "Atención", MessageBoxButtons.OK)
            Exit Sub
        End If

        If grpAttachs.Enabled Then
            attachs = GetAttachs()
        End If

        DialogResult = DialogResult.OK
        flagDialogResult = True

        RaiseEvent NuevoMensaje("Re: " & _Asunto, txtMensaje.Text, User, notificar, attachs, notifyIds, True, True)
    End Sub

    Private Sub btnUsuarios_Click(ByVal sender As System.Object, ByVal e As EventArgs)
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
