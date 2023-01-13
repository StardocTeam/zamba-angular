Imports System.Text
Imports Zamba.AdminControls

Public Class UCDOAutoMail
    Inherits ZRuleControl
    Private _rule As IDOAutoMail

    Private Property CurrentRule() As IDOAutoMail
        Get
            Return _rule
        End Get
        Set(ByVal value As IDOAutoMail)
            _rule = value
        End Set
    End Property

#Region " Código generado por el Diseñador de Windows Forms "
    Friend WithEvents lstAutoMails As System.Windows.Forms.ListView
    Friend WithEvents btAceptar As ZButton
    Private WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents rbLink As System.Windows.Forms.RadioButton
    Friend WithEvents rbDocumento As System.Windows.Forms.RadioButton
    Friend WithEvents lblTituloIndices As ZLabel
    Friend WithEvents panelIndices As System.Windows.Forms.Panel
    Friend WithEvents btQuitar As ZButton
    Friend WithEvents btAgregar As ZButton
    Friend WithEvents btNuevoAutomail As ZButton
    Friend WithEvents btModificarAutoMail As ZButton
    Friend WithEvents lstIndices As ListBox
    Friend WithEvents gbAutoMail As GroupBox
    Friend WithEvents chkGroup As System.Windows.Forms.CheckBox
    Friend WithEvents ZPanel3 As ZPanel
    Friend WithEvents ZPanel2 As ZPanel
    Friend WithEvents ZPanel1 As ZPanel
    Friend WithEvents chkAddIndexs As System.Windows.Forms.CheckBox

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        lstAutoMails = New System.Windows.Forms.ListView()
        ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        btAceptar = New ZButton()
        chkAddIndexs = New System.Windows.Forms.CheckBox()
        rbLink = New System.Windows.Forms.RadioButton()
        rbDocumento = New System.Windows.Forms.RadioButton()
        panelIndices = New System.Windows.Forms.Panel()
        lstIndices = New ListBox()
        lblTituloIndices = New ZLabel()
        btQuitar = New ZButton()
        btAgregar = New ZButton()
        btNuevoAutomail = New ZButton()
        gbAutoMail = New GroupBox()
        btModificarAutoMail = New ZButton()
        chkGroup = New System.Windows.Forms.CheckBox()
        ZPanel1 = New ZPanel()
        ZPanel2 = New ZPanel()
        ZPanel3 = New ZPanel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        panelIndices.SuspendLayout()
        gbAutoMail.SuspendLayout()
        ZPanel1.SuspendLayout()
        ZPanel2.SuspendLayout()
        ZPanel3.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(gbAutoMail)
        tbRule.Controls.Add(panelIndices)
        tbRule.Controls.Add(ZPanel1)
        tbRule.Size = New System.Drawing.Size(646, 407)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(654, 436)
        '
        'lstAutoMails
        '
        lstAutoMails.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader1})
        lstAutoMails.Dock = System.Windows.Forms.DockStyle.Fill
        lstAutoMails.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lstAutoMails.FullRowSelect = True
        lstAutoMails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        lstAutoMails.HideSelection = False
        lstAutoMails.Location = New System.Drawing.Point(3, 19)
        lstAutoMails.MultiSelect = False
        lstAutoMails.Name = "lstAutoMails"
        lstAutoMails.Size = New System.Drawing.Size(450, 245)
        lstAutoMails.TabIndex = 0
        lstAutoMails.UseCompatibleStateImageBehavior = False
        lstAutoMails.View = System.Windows.Forms.View.List
        '
        'ColumnHeader1
        '
        ColumnHeader1.Text = ""
        ColumnHeader1.Width = 200
        '
        'btAceptar
        '
        btAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btAceptar.FlatStyle = FlatStyle.Flat
        btAceptar.ForeColor = System.Drawing.Color.White
        btAceptar.Location = New System.Drawing.Point(479, 11)
        btAceptar.Name = "btAceptar"
        btAceptar.Size = New System.Drawing.Size(76, 28)
        btAceptar.TabIndex = 11
        btAceptar.Text = "Guardar Configuracion"
        btAceptar.UseVisualStyleBackColor = False
        '
        'chkAddIndexs
        '
        chkAddIndexs.BackColor = System.Drawing.Color.Transparent
        chkAddIndexs.Location = New System.Drawing.Point(12, 42)
        chkAddIndexs.Name = "chkAddIndexs"
        chkAddIndexs.Size = New System.Drawing.Size(169, 24)
        chkAddIndexs.TabIndex = 5
        chkAddIndexs.Text = "Adjuntar Atributos"
        chkAddIndexs.UseVisualStyleBackColor = False
        '
        'rbLink
        '
        rbLink.AutoSize = True
        rbLink.BackColor = System.Drawing.Color.Transparent
        rbLink.Location = New System.Drawing.Point(12, 16)
        rbLink.Name = "rbLink"
        rbLink.Size = New System.Drawing.Size(112, 20)
        rbLink.TabIndex = 3
        rbLink.TabStop = True
        rbLink.Text = "Adjuntar Link"
        rbLink.UseVisualStyleBackColor = False
        '
        'rbDocumento
        '
        rbDocumento.AutoSize = True
        rbDocumento.BackColor = System.Drawing.Color.Transparent
        rbDocumento.Location = New System.Drawing.Point(148, 16)
        rbDocumento.Name = "rbDocumento"
        rbDocumento.Size = New System.Drawing.Size(161, 20)
        rbDocumento.TabIndex = 4
        rbDocumento.TabStop = True
        rbDocumento.Text = "Adjuntar Documento"
        rbDocumento.UseVisualStyleBackColor = False
        '
        'panelIndices
        '
        panelIndices.BackColor = System.Drawing.Color.Transparent
        panelIndices.Controls.Add(lstIndices)
        panelIndices.Controls.Add(ZPanel2)
        panelIndices.Controls.Add(lblTituloIndices)
        panelIndices.Dock = System.Windows.Forms.DockStyle.Right
        panelIndices.Location = New System.Drawing.Point(459, 3)
        panelIndices.Name = "panelIndices"
        panelIndices.Size = New System.Drawing.Size(184, 350)
        panelIndices.TabIndex = 27
        panelIndices.Visible = False
        '
        'lstIndices
        '
        lstIndices.Dock = System.Windows.Forms.DockStyle.Fill
        lstIndices.FormattingEnabled = True
        lstIndices.ItemHeight = 16
        lstIndices.Location = New System.Drawing.Point(0, 16)
        lstIndices.Name = "lstIndices"
        lstIndices.Size = New System.Drawing.Size(184, 285)
        lstIndices.TabIndex = 0
        '
        'lblTituloIndices
        '
        lblTituloIndices.AutoSize = True
        lblTituloIndices.BackColor = System.Drawing.Color.Transparent
        lblTituloIndices.Dock = System.Windows.Forms.DockStyle.Top
        lblTituloIndices.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblTituloIndices.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblTituloIndices.Location = New System.Drawing.Point(0, 0)
        lblTituloIndices.Name = "lblTituloIndices"
        lblTituloIndices.Size = New System.Drawing.Size(198, 16)
        lblTituloIndices.TabIndex = 3
        lblTituloIndices.Text = "Lista de Atributos a adjuntar"
        lblTituloIndices.TextAlign = ContentAlignment.MiddleLeft
        '
        'btQuitar
        '
        btQuitar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btQuitar.FlatStyle = FlatStyle.Flat
        btQuitar.ForeColor = System.Drawing.Color.White
        btQuitar.Location = New System.Drawing.Point(105, 17)
        btQuitar.Name = "btQuitar"
        btQuitar.Size = New System.Drawing.Size(76, 26)
        btQuitar.TabIndex = 0
        btQuitar.Text = "Quitar"
        btQuitar.UseVisualStyleBackColor = True
        '
        'btAgregar
        '
        btAgregar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btAgregar.FlatStyle = FlatStyle.Flat
        btAgregar.ForeColor = System.Drawing.Color.White
        btAgregar.Location = New System.Drawing.Point(16, 17)
        btAgregar.Name = "btAgregar"
        btAgregar.Size = New System.Drawing.Size(83, 26)
        btAgregar.TabIndex = 0
        btAgregar.Text = "Agregar"
        btAgregar.UseVisualStyleBackColor = False
        '
        'btNuevoAutomail
        '
        btNuevoAutomail.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btNuevoAutomail.FlatStyle = FlatStyle.Flat
        btNuevoAutomail.ForeColor = System.Drawing.Color.White
        btNuevoAutomail.Location = New System.Drawing.Point(319, 43)
        btNuevoAutomail.Name = "btNuevoAutomail"
        btNuevoAutomail.Size = New System.Drawing.Size(93, 23)
        btNuevoAutomail.TabIndex = 2
        btNuevoAutomail.Text = "Nuevo AutoMail"
        btNuevoAutomail.UseVisualStyleBackColor = False
        '
        'gbAutoMail
        '
        gbAutoMail.BackColor = System.Drawing.Color.Transparent
        gbAutoMail.Controls.Add(lstAutoMails)
        gbAutoMail.Controls.Add(ZPanel3)
        gbAutoMail.Dock = System.Windows.Forms.DockStyle.Fill
        gbAutoMail.Location = New System.Drawing.Point(3, 3)
        gbAutoMail.Name = "gbAutoMail"
        gbAutoMail.Size = New System.Drawing.Size(456, 350)
        gbAutoMail.TabIndex = 29
        gbAutoMail.TabStop = False
        gbAutoMail.Text = "Seleccione el Automail a enviarse"
        '
        'btModificarAutoMail
        '
        btModificarAutoMail.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btModificarAutoMail.FlatStyle = FlatStyle.Flat
        btModificarAutoMail.ForeColor = System.Drawing.Color.White
        btModificarAutoMail.Location = New System.Drawing.Point(221, 42)
        btModificarAutoMail.Name = "btModificarAutoMail"
        btModificarAutoMail.Size = New System.Drawing.Size(88, 23)
        btModificarAutoMail.TabIndex = 1
        btModificarAutoMail.Text = "Modificar AutoMail"
        btModificarAutoMail.UseVisualStyleBackColor = True
        '
        'chkGroup
        '
        chkGroup.AutoSize = True
        chkGroup.BackColor = System.Drawing.Color.Transparent
        chkGroup.Location = New System.Drawing.Point(17, 11)
        chkGroup.Name = "chkGroup"
        chkGroup.Size = New System.Drawing.Size(188, 20)
        chkGroup.TabIndex = 30
        chkGroup.Text = "Agrupar por Destinatario"
        chkGroup.UseVisualStyleBackColor = False
        '
        'ZPanel1
        '
        ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel1.Controls.Add(btAceptar)
        ZPanel1.Controls.Add(chkGroup)
        ZPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        ZPanel1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel1.Location = New System.Drawing.Point(3, 353)
        ZPanel1.Name = "ZPanel1"
        ZPanel1.Size = New System.Drawing.Size(640, 51)
        ZPanel1.TabIndex = 31
        '
        'ZPanel2
        '
        ZPanel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel2.Controls.Add(btQuitar)
        ZPanel2.Controls.Add(btAgregar)
        ZPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        ZPanel2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel2.Location = New System.Drawing.Point(0, 301)
        ZPanel2.Name = "ZPanel2"
        ZPanel2.Size = New System.Drawing.Size(184, 49)
        ZPanel2.TabIndex = 4
        '
        'ZPanel3
        '
        ZPanel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        ZPanel3.Controls.Add(chkAddIndexs)
        ZPanel3.Controls.Add(btModificarAutoMail)
        ZPanel3.Controls.Add(btNuevoAutomail)
        ZPanel3.Controls.Add(rbLink)
        ZPanel3.Controls.Add(rbDocumento)
        ZPanel3.Dock = System.Windows.Forms.DockStyle.Bottom
        ZPanel3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ZPanel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ZPanel3.Location = New System.Drawing.Point(3, 264)
        ZPanel3.Name = "ZPanel3"
        ZPanel3.Size = New System.Drawing.Size(450, 83)
        ZPanel3.TabIndex = 6
        '
        'UCDOAutoMail
        '
        Name = "UCDOAutoMail"
        Size = New System.Drawing.Size(654, 436)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        panelIndices.ResumeLayout(False)
        panelIndices.PerformLayout()
        gbAutoMail.ResumeLayout(False)
        ZPanel1.ResumeLayout(False)
        ZPanel1.PerformLayout()
        ZPanel2.ResumeLayout(False)
        ZPanel3.ResumeLayout(False)
        ZPanel3.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region
    Public Sub New(ByRef rule As IDOAutoMail, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = rule
        LoadAutoMails()
        LoadRule()
    End Sub
    Private Sub LoadRule()
        rbDocumento.Checked = CurrentRule.AddDocument
        rbLink.Checked = CurrentRule.AddLink
        chkAddIndexs.Checked = CurrentRule.AddIndexs

        If Not IsNothing(CurrentRule.IndexNames) Then
            For Each index As String In CurrentRule.IndexNames
                lstIndices.Items.Add(index)
            Next
        End If

        chkGroup.Checked = CurrentRule.groupMailTo
        lstIndices.Refresh()
    End Sub
    ''' <summary>
    ''' Carga los valores del formulario en la regla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadValues()
        Dim StepItem As StepItem = lstAutoMails.SelectedItems(0)
        If IsNothing(CurrentRule.smtp) Then CurrentRule.smtp = New SMTP_Validada
        If IsNothing(CurrentRule.Automail) Then CurrentRule.Automail = New AutoMail
        CurrentRule.Automail.ID = StepItem.Automail.ID
        CurrentRule.AddDocument = rbDocumento.Checked
        CurrentRule.AddLink = rbLink.Checked
        CurrentRule.AddIndexs = chkAddIndexs.Checked
        CurrentRule.groupMailTo = chkGroup.Checked
    End Sub
    ''' <summary>
    ''' Salva los valores de la regla en la base de datos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveValues()
        WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 0, CurrentRule.Automail.ID)
        WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 1, CurrentRule.AddDocument)
        WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 2, CurrentRule.AddLink)
        WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 3, CurrentRule.AddIndexs)
        WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 4, CurrentRule.smtp.User)
        WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 5, CurrentRule.smtp.Password)
        WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 6, CurrentRule.smtp.Port.ToString())
        WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 7, CurrentRule.smtp.Server)


        Dim ParsedIndexs As New StringBuilder 'Guardo los atributos en la base de la forma "Indice1|Indice2"
        For Each index As Object In CurrentRule.IndexNames
            ParsedIndexs.Append(index)
            ParsedIndexs.Append("|")
        Next
        If ParsedIndexs.Length > 0 Then ParsedIndexs.Remove(ParsedIndexs.Length - 1, 1)
        WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 8, ParsedIndexs.ToString())
        WFRulesBusiness.UpdateParamItem(CurrentRule.ID, 9, CurrentRule.groupMailTo)
        UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
    End Sub
    ''' <summary>
    ''' Carga los todos los automails en lstAutomails
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAutoMails()
        lstAutoMails.Clear()
        For Each automail As AutoMail In AutoMailDBFacade.GetAutoMailList
            If IsNothing(CurrentRule.Automail) = False AndAlso CurrentRule.Automail.ID = automail.ID Then
                lstAutoMails.Items.Add(New StepItem(automail, True))
            Else
                lstAutoMails.Items.Add(New StepItem(automail, False))
            End If
        Next
    End Sub
    ''' <summary>
    ''' Valida el formulario. Se le pasa una lista de String que se cargara con los errores del formulario en el caso que existan
    ''' </summary>
    ''' <returns>True si no se encontraron errores en el formulario , sino False</returns>
    ''' <remarks></remarks>
    Private Function IsSmtpDataValid() As Boolean
        Return True
    End Function
    ''' <summary>
    ''' Muestra al usuario una lista de errores en un MessageBox 
    ''' </summary>
    ''' <param name="errorList"></param>
    ''' <remarks></remarks>
    Private Sub ShowErrors(ByRef errorList As List(Of String))
        Dim ErrorBuilder As New StringBuilder()
        For i As Integer = 0 To errorList.Count - 1
            ErrorBuilder.Append(errorList(i))
            If Not i = errorList.Count Then
                ErrorBuilder.AppendLine()
            End If
        Next

        MessageBox.Show(ErrorBuilder.ToString(), "Error en el formulario", MessageBoxButtons.OK)
    End Sub

#Region " Eventos"
    Private Sub UCDOAutoMail_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            'LoadAutoMails()
            'LoadRule()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub btAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btAceptar.Click
        Try
            If lstAutoMails.SelectedItems.Count > 0 AndAlso Not lstAutoMails.SelectedItems(0) Is Nothing Then
                If IsSmtpDataValid() Then
                    LoadValues()
                    SaveValues()
                    For Each item As ListViewItem In lstAutoMails.Items
                        DirectCast(item, StepItem).SelectedStep = False
                    Next
                End If
            Else
                Dim CurrentError As New List(Of String)(1)
                CurrentError.Add("Seleccione un Automail")
                ShowErrors(CurrentError)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub chkAddIndexs_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkAddIndexs.CheckedChanged
        If chkAddIndexs.Checked Then
            panelIndices.Visible = True
        Else
            panelIndices.Visible = False
        End If
    End Sub
    Private Sub btAgregar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btAgregar.Click
        Try
            Dim indice As String = InputBox("Ingrese el atributo", , , , )
            If indice <> "" Then
                If CurrentRule.IndexNames.Contains(indice) Then
                    Dim CurrentError As New List(Of String)(1)
                    CurrentError.Add("El Atributo ingresado ya existe en la lista")
                    ShowErrors(CurrentError)
                Else
                    lstIndices.Items.Add(indice)
                    CurrentRule.IndexNames.Add(indice)
                    lstIndices.Refresh()
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se elimina un indice en automail
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	11/06/2008	Modified    El item que se recupera de lstIndices se definio como object (puede ser string o index). Después
    '''                                         se verifica si el item es de tipo string o index
    ''' </history>
    Private Sub btQuitar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btQuitar.Click

        Try

            If Not IsNothing(lstAutoMails.SelectedItems) AndAlso lstIndices.SelectedIndices.Count > 0 Then

                'For Each item As ListViewItem In lstIndices.SelectedItems
                'For Each item As Object In lstIndices.SelectedItems

                If (TypeOf lstIndices.SelectedItem Is String) Then
                    CurrentRule.IndexNames.Remove(lstIndices.SelectedItem.ToString())
                ElseIf (TypeOf lstIndices.SelectedItem Is Index) Then
                    CurrentRule.IndexNames.Remove(DirectCast(lstIndices.SelectedItem, Index).Name)
                End If

                lstIndices.Items.Remove(lstIndices.SelectedItem)
                'CurrentRule.IndexNames.Remove(item.text)

                'Next

                lstIndices.Refresh()

            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub btModificarAutoMail_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btModificarAutoMail.Click
        If lstAutoMails.SelectedItems.Count > 0 AndAlso Not IsNothing(lstAutoMails.SelectedItems(0)) Then
            Dim s As StepItem = DirectCast(lstAutoMails.SelectedItems(0), StepItem)
            Dim frm As New frmAutomail(s.Automail)

            frm.StartPosition = FormStartPosition.CenterScreen
            frm.ShowDialog()
        Else
            MessageBox.Show("Seleccione un Automail para modificar")
        End If
    End Sub
    Private Sub btNuevoAutomail_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btNuevoAutomail.Click
        Try
            Dim frm As New frmAutomail(True)
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.ShowDialog()
            frm.Dispose()
            LoadAutoMails()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub client_SendCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        If (Not IsNothing(e.Error)) Then
            Dim ErrorMessage As New StringBuilder()
            ErrorMessage.Append("Error al enviar el mensaje.")
            ErrorMessage.AppendLine()
            ErrorMessage.Append(e.Error.ToString())

            MessageBox.Show(ErrorMessage.ToString(), "Error", MessageBoxButtons.OK)
        ElseIf e.Cancelled Then
            MessageBox.Show("El envio del mensaje se cancelo.", "Cancelado", MessageBoxButtons.OK)
        Else
            MessageBox.Show("Mensaje enviado exitosamente.", "Zamba", MessageBoxButtons.OK)
        End If
    End Sub
#End Region

    Private Class StepItem
        Inherits ListViewItem
        Private _autoMail As AutoMail
        Private _selectedStep As Boolean
        Private _selectedCheck As Boolean
        Public Property Automail() As AutoMail
            Get
                Return _autoMail
            End Get
            Set(ByVal value As AutoMail)
                _autoMail = value
            End Set
        End Property
        Public Property SelectedStep() As Boolean
            Get
                Return _selectedStep
            End Get
            Set(ByVal Value As Boolean)
                _selectedStep = Value
                If Value = 0 Then
                    Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                Else
                    Font = New Font("Tahoma", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
                End If
            End Set
        End Property
        Sub New(ByVal mail As AutoMail, ByVal selectStep As Boolean)
            Automail = mail
            Text = Automail.Name
            SelectedStep = selectStep
            Selected = SelectedStep
        End Sub
    End Class

    Private Sub btNuevoAutomail_HelpRequested(ByVal sender As Object, ByVal hlpevent As System.Windows.Forms.HelpEventArgs) Handles btNuevoAutomail.HelpRequested

    End Sub

    Private Sub btnOpenImage_Click(ByVal sender As System.Object, ByVal e As EventArgs)

    End Sub

End Class
