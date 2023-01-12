Public Class UCDoGenerateOutlook
    Inherits ZRuleControl

#Region " Código generado por el Diseñador de Windows Forms "
    Friend WithEvents Label4 As ZLabel
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend Shadows WithEvents Label3 As ZLabel
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents txtTimeOut As TextBox
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents txtPara As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtCC As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtCCO As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents txtAsunto As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblImages As ZLabel
    Friend WithEvents btnOpenImage As ZButton
    Friend WithEvents lstImages As ListBox
    Friend WithEvents btnRemoveImage As ZButton
    Friend WithEvents chkAttachLink As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As ZLabel
    Friend WithEvents ChkAutomatic As System.Windows.Forms.CheckBox
    Friend WithEvents txtReplyMsgPath As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblReply As ZLabel
    Friend WithEvents chkReply As System.Windows.Forms.CheckBox
    Friend WithEvents txtBody As Zamba.AppBlock.TextoInteligenteTextBox

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        Label4 = New ZLabel()
        btnAceptar = New ZButton()
        Label1 = New ZLabel()
        Label2 = New ZLabel()
        Label3 = New ZLabel()
        Label5 = New ZLabel()
        Label6 = New ZLabel()
        txtTimeOut = New TextBox()
        txtPara = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtCC = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtCCO = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtAsunto = New Zamba.AppBlock.TextoInteligenteTextBox()
        txtBody = New Zamba.AppBlock.TextoInteligenteTextBox()
        lstImages = New ListBox()
        btnOpenImage = New ZButton()
        lblImages = New ZLabel()
        btnRemoveImage = New ZButton()
        chkAttachLink = New System.Windows.Forms.CheckBox()
        Label9 = New ZLabel()
        ChkAutomatic = New System.Windows.Forms.CheckBox()
        chkReply = New System.Windows.Forms.CheckBox()
        lblReply = New ZLabel()
        txtReplyMsgPath = New Zamba.AppBlock.TextoInteligenteTextBox()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(txtReplyMsgPath)
        tbRule.Controls.Add(lblReply)
        tbRule.Controls.Add(chkReply)
        tbRule.Controls.Add(ChkAutomatic)
        tbRule.Controls.Add(chkAttachLink)
        tbRule.Controls.Add(Label9)
        tbRule.Controls.Add(btnRemoveImage)
        tbRule.Controls.Add(lblImages)
        tbRule.Controls.Add(btnOpenImage)
        tbRule.Controls.Add(lstImages)
        tbRule.Controls.Add(txtBody)
        tbRule.Controls.Add(txtAsunto)
        tbRule.Controls.Add(txtCCO)
        tbRule.Controls.Add(txtCC)
        tbRule.Controls.Add(txtPara)
        tbRule.Controls.Add(txtTimeOut)
        tbRule.Controls.Add(Label6)
        tbRule.Controls.Add(Label5)
        tbRule.Controls.Add(Label3)
        tbRule.Controls.Add(Label2)
        tbRule.Controls.Add(Label1)
        tbRule.Controls.Add(btnAceptar)
        tbRule.Controls.Add(Label4)
        tbRule.Size = New System.Drawing.Size(508, 509)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(516, 538)
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label4.Location = New System.Drawing.Point(11, 20)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(37, 16)
        Label4.TabIndex = 10
        Label4.Text = "Para"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        '
        'btnAceptar
        '
        btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAceptar.FlatStyle = FlatStyle.Flat
        btnAceptar.ForeColor = System.Drawing.Color.White
        btnAceptar.Location = New System.Drawing.Point(25, 107)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(136, 23)
        btnAceptar.TabIndex = 12
        btnAceptar.Text = "Aceptar"
        btnAceptar.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(11, 47)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(26, 16)
        Label1.TabIndex = 13
        Label1.Text = "CC"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(11, 74)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(36, 16)
        Label2.TabIndex = 14
        Label2.Text = "CCO"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(10, 101)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(54, 16)
        Label3.TabIndex = 15
        Label3.Text = "Asunto"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label5.Location = New System.Drawing.Point(11, 283)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(54, 16)
        Label5.TabIndex = 16
        Label5.Text = "Cuerpo"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.BackColor = System.Drawing.Color.Transparent
        Label6.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label6.Location = New System.Drawing.Point(11, 236)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(264, 16)
        Label6.TabIndex = 17
        Label6.Text = "Segundos de espera para validar envío"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtTimeOut
        '
        txtTimeOut.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtTimeOut.Location = New System.Drawing.Point(281, 234)
        txtTimeOut.Name = "txtTimeOut"
        txtTimeOut.Size = New System.Drawing.Size(0, 23)
        txtTimeOut.TabIndex = 37
        '
        'txtPara
        '
        txtPara.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtPara.Location = New System.Drawing.Point(80, 19)
        txtPara.Name = "txtPara"
        txtPara.Size = New System.Drawing.Size(0, 21)
        txtPara.TabIndex = 18
        txtPara.Text = ""
        '
        'txtCC
        '
        txtCC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtCC.Location = New System.Drawing.Point(80, 46)
        txtCC.Name = "txtCC"
        txtCC.Size = New System.Drawing.Size(0, 21)
        txtCC.TabIndex = 19
        txtCC.Text = ""
        '
        'txtCCO
        '
        txtCCO.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtCCO.Location = New System.Drawing.Point(80, 73)
        txtCCO.Name = "txtCCO"
        txtCCO.Size = New System.Drawing.Size(0, 21)
        txtCCO.TabIndex = 20
        txtCCO.Text = ""
        '
        'txtAsunto
        '
        txtAsunto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtAsunto.Location = New System.Drawing.Point(80, 100)
        txtAsunto.Name = "txtAsunto"
        txtAsunto.Size = New System.Drawing.Size(0, 21)
        txtAsunto.TabIndex = 21
        txtAsunto.Text = ""
        '
        'txtBody
        '
        txtBody.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtBody.Location = New System.Drawing.Point(14, 302)
        txtBody.Name = "txtBody"
        txtBody.Size = New System.Drawing.Size(146, 0)
        txtBody.TabIndex = 23
        txtBody.Text = ""
        '
        'lstImages
        '
        lstImages.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lstImages.FormattingEnabled = True
        lstImages.ItemHeight = 16
        lstImages.Location = New System.Drawing.Point(25, 72)
        lstImages.Name = "lstImages"
        lstImages.Size = New System.Drawing.Size(136, 4)
        lstImages.TabIndex = 26
        '
        'btnOpenImage
        '
        btnOpenImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnOpenImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnOpenImage.FlatStyle = FlatStyle.Flat
        btnOpenImage.ForeColor = System.Drawing.Color.White
        btnOpenImage.Location = New System.Drawing.Point(24, 44)
        btnOpenImage.Name = "btnOpenImage"
        btnOpenImage.Size = New System.Drawing.Size(65, 23)
        btnOpenImage.TabIndex = 27
        btnOpenImage.Text = "Buscar"
        btnOpenImage.UseVisualStyleBackColor = True
        '
        'lblImages
        '
        lblImages.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblImages.AutoSize = True
        lblImages.BackColor = System.Drawing.Color.Transparent
        lblImages.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblImages.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblImages.Location = New System.Drawing.Point(21, 19)
        lblImages.Name = "lblImages"
        lblImages.Size = New System.Drawing.Size(71, 16)
        lblImages.TabIndex = 28
        lblImages.Text = "Imágenes"
        lblImages.TextAlign = ContentAlignment.MiddleLeft
        '
        'btnRemoveImage
        '
        btnRemoveImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnRemoveImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnRemoveImage.FlatStyle = FlatStyle.Flat
        btnRemoveImage.ForeColor = System.Drawing.Color.White
        btnRemoveImage.Location = New System.Drawing.Point(95, 44)
        btnRemoveImage.Name = "btnRemoveImage"
        btnRemoveImage.Size = New System.Drawing.Size(65, 23)
        btnRemoveImage.TabIndex = 29
        btnRemoveImage.Text = "Remover"
        btnRemoveImage.UseVisualStyleBackColor = True
        '
        'chkAttachLink
        '
        chkAttachLink.BackColor = System.Drawing.Color.Transparent
        chkAttachLink.Location = New System.Drawing.Point(14, 162)
        chkAttachLink.Name = "chkAttachLink"
        chkAttachLink.Size = New System.Drawing.Size(15, 16)
        chkAttachLink.TabIndex = 33
        chkAttachLink.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.BackColor = System.Drawing.Color.Transparent
        Label9.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label9.Location = New System.Drawing.Point(30, 162)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(94, 16)
        Label9.TabIndex = 32
        Label9.Text = "Adjuntar Link"
        Label9.TextAlign = ContentAlignment.MiddleLeft
        '
        'ChkAutomatic
        '
        ChkAutomatic.AutoSize = True
        ChkAutomatic.BackColor = System.Drawing.Color.Transparent
        ChkAutomatic.Font = New Font("Verdana", 9.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        ChkAutomatic.Location = New System.Drawing.Point(14, 138)
        ChkAutomatic.Name = "ChkAutomatic"
        ChkAutomatic.Size = New System.Drawing.Size(139, 18)
        ChkAutomatic.TabIndex = 38
        ChkAutomatic.Text = "Envio automatico"
        ChkAutomatic.UseVisualStyleBackColor = False
        '
        'chkReply
        '
        chkReply.AutoSize = True
        chkReply.BackColor = System.Drawing.Color.Transparent
        chkReply.Font = New Font("Verdana", 9.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        chkReply.Location = New System.Drawing.Point(14, 184)
        chkReply.Name = "chkReply"
        chkReply.Size = New System.Drawing.Size(96, 18)
        chkReply.TabIndex = 39
        chkReply.Text = "Responder"
        chkReply.UseVisualStyleBackColor = False
        '
        'lblReply
        '
        lblReply.AutoSize = True
        lblReply.BackColor = System.Drawing.Color.Transparent
        lblReply.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblReply.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblReply.Location = New System.Drawing.Point(30, 205)
        lblReply.Name = "lblReply"
        lblReply.Size = New System.Drawing.Size(175, 16)
        lblReply.TabIndex = 40
        lblReply.Text = "Ruta del mail a responder"
        lblReply.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtReplyMsgPath
        '
        txtReplyMsgPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtReplyMsgPath.Location = New System.Drawing.Point(191, 202)
        txtReplyMsgPath.Name = "txtReplyMsgPath"
        txtReplyMsgPath.Size = New System.Drawing.Size(0, 21)
        txtReplyMsgPath.TabIndex = 41
        txtReplyMsgPath.Text = ""
        '
        'UCDoGenerateOutlook
        '
        Name = "UCDoGenerateOutlook"
        Size = New System.Drawing.Size(516, 538)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Dim currentRule As IDOGenerateOutlook
    Dim ofdDialog As New OpenFileDialog
    'Dim ds As New DataSet


#Region "Constructor"

    Public Sub New(ByRef this As IDOGenerateOutlook, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(this, _wfPanelCircuit)
        InitializeComponent()
        currentRule = this
        this_Load()

        SetControlsPosition()
    End Sub

#End Region

#Region "Propiedades"

    Public Shadows ReadOnly Property MyRule() As IDOGenerateOutlook
        Get
            Return DirectCast(Rule, IDOGenerateOutlook)
        End Get
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta cuando se carga el userControl UCDOGenerateOutlook
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub this_Load()

        Try

            txtPara.Text = currentRule.Para
            txtCC.Text = currentRule.CC
            txtCCO.Text = currentRule.CCO
            txtAsunto.Text = currentRule.Asunto

            txtBody.Text = currentRule.Body
            txtTimeOut.Text = Convert.ToString(currentRule.sendTimeOut)

            chkAttachLink.Checked = currentRule.AttachLink

            ofdDialog.Filter = "Archivos de imagen (*.BMP;*.JPG;*.JPEG;*.GIF;*.TIF;*.TIFF)|*.BMP;*.JPG;*.JPEG;*.GIF;*.TIF;*.TIFF"

            ChkAutomatic.Checked = currentRule.automaticSend

            'LoadDataset()
            If ((currentRule.ImagesNames.Length > 0) AndAlso (currentRule.PathImages.Length > 0)) Then
                recoverImages()
            End If

            Dim reply As Boolean = currentRule.ReplyMail
            chkReply.Checked = reply
            lblReply.Enabled = reply
            txtReplyMsgPath.Enabled = reply

            txtReplyMsgPath.Text = currentRule.ReplyMsgPath

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub FsButton1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        If chkReply.Checked AndAlso String.IsNullOrEmpty(txtReplyMsgPath.Text) Then
            MessageBox.Show("Debe completar la ruta del mail a responder." + vbCrLf + "Podrá utilizar ZVAR para obtener dicha ruta." + "La respuesta deberá ser un archivo MSG.", "Datos insuficientes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            currentRule.Para = txtPara.Text
            currentRule.CC = txtCC.Text
            currentRule.CCO = txtCCO.Text
            currentRule.Asunto = txtAsunto.Text
            currentRule.Body = txtBody.Text
            currentRule.sendTimeOut = Convert.ToInt32(txtTimeOut.Text)
            currentRule.AttachLink = chkAttachLink.Checked
            currentRule.automaticSend = ChkAutomatic.Checked
            currentRule.ReplyMail = chkReply.Checked
            currentRule.ReplyMsgPath = txtReplyMsgPath.Text

            WFRulesBusiness.UpdateParamItem(currentRule.ID, 0, currentRule.Para)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 1, currentRule.CC)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 2, currentRule.CCO)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 3, currentRule.Asunto)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 4, currentRule.SendDocument)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 5, currentRule.Body)

            If Not (IsNothing(currentRule.PathImages)) Then
                currentRule.ImagesNames = String.Empty
                currentRule.PathImages = String.Empty
            End If

            For Each Image As Image In lstImages.Items
                If (currentRule.PathImages = String.Empty) Then
                    currentRule.ImagesNames = Image.Name
                    currentRule.PathImages = Image.Path
                Else
                    currentRule.ImagesNames = currentRule.ImagesNames & ";" & Image.Name
                    currentRule.PathImages = currentRule.PathImages & ";" & Image.Path
                End If
            Next

            ' Se guardan los nombres de las imágenes
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 6, currentRule.ImagesNames)
            ' Se guardan los path de las imágenes
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 7, currentRule.PathImages)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 8, currentRule.AttachLink)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 9, currentRule.sendTimeOut)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 10, currentRule.automaticSend)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 11, currentRule.ReplyMail)
            WFRulesBusiness.UpdateParamItem(currentRule.ID, 12, currentRule.ReplyMsgPath)
            UserBusiness.Rights.SaveAction(currentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & currentRule.Name & "(" & currentRule.ID & ")")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub



#End Region

#Region "Métodos"

    ''' <summary>
    ''' Método que sirve para colocar las instancias Image en el listbox, cada una con su correspondiente 
    ''' nombre y path
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	07/08/2008	Created
    ''' </history>
    Private Sub recoverImages()

        Dim tableImagesNames() As String
        Dim tablePathImages() As String
        Dim n As Integer
        tableImagesNames = Split(currentRule.ImagesNames, ";")
        tablePathImages = Split(currentRule.PathImages, ";")

        For n = 0 To UBound(tableImagesNames, 1)
            ' Parámetros: Nombre y Path de la imagen
            Dim image As New Image(tableImagesNames(n), tablePathImages(n))
            lstImages.Items.Add(image)
        Next

        lstImages.ValueMember = "Name"

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón "Buscar"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub btnOpenImage_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnOpenImage.Click

        If (ofdDialog.ShowDialog() = DialogResult.OK) Then

            ' Parámetros: Nombre y Path de la imágen
            Dim imag As New Image(ofdDialog.SafeFileName.Substring(0, ofdDialog.SafeFileName.Length - 4), ofdDialog.FileName)
            lstImages.Items.Add(imag)
            lstImages.ValueMember = "Name"

        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón "Remover"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub btnRemoveImage_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnRemoveImage.Click

        If Not (IsNothing(lstImages.SelectedItem)) Then
            lstImages.Items.Remove(lstImages.SelectedItem)
        End If

    End Sub

    'Private Sub LoadDataset()
    '    Try
    '        ds = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, "SELECT NAME, CORREO FROM USRTABLE ")
    '    Catch ex As Exception
    '        zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub

    Private Sub SetControlsPosition()
        Dim alto As Int32 = Height
        Dim ancho As Int32 = Width
        Dim topeIzquierdo As Int32

        'Se corrige la posición de los controles de la derecha
        lstImages.Left = ancho - lstImages.Width - 15
        topeIzquierdo = lstImages.Left
        lblImages.Left = topeIzquierdo
        btnRemoveImage.Left = ancho - btnRemoveImage.Width - 15
        btnAceptar.Left = ancho - btnAceptar.Width - 15
        btnAceptar.Top = alto - btnAceptar.Height - 35
        btnOpenImage.Left = topeIzquierdo

        'Se corrige el ancho de los controles de la izquierda
        txtAsunto.Width = topeIzquierdo - txtAsunto.Left - 10
        txtPara.Width = topeIzquierdo - txtPara.Left - 10
        txtCC.Width = topeIzquierdo - txtCC.Left - 10
        txtCCO.Width = topeIzquierdo - txtCCO.Left - 10
        txtReplyMsgPath.Width = topeIzquierdo - txtReplyMsgPath.Left - 10
        txtTimeOut.Width = topeIzquierdo - txtTimeOut.Left - 10
        txtBody.Width = ancho - txtBody.Left - 15
        txtBody.Height = btnAceptar.Top - txtBody.Top - 10
        lstImages.Height = txtBody.Top - lstImages.Top - 75
    End Sub

#End Region

#Region "Clase privada Image"

    ''' <summary>
    ''' Clase utiliza para guardar un elemento Image en el listbox de imágenes
    ''' </summary>
    ''' <remarks></remarks>

    Private Class Image

        Public m_name As String
        Public m_path As String

        Public Sub New(ByVal n As String, ByVal p As String)
            Name = n
            Path = p
        End Sub

        Public Property Name() As String
            Get
                Return (m_name)
            End Get
            Set(ByVal value As String)
                m_name = value
            End Set
        End Property

        Public Property Path() As String
            Get
                Return (m_path)
            End Get
            Set(ByVal value As String)
                m_path = value
            End Set
        End Property

    End Class

#End Region


    Private Sub chkReply_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkReply.CheckedChanged
        lblReply.Enabled = chkReply.Checked
        txtReplyMsgPath.Enabled = chkReply.Checked
    End Sub
End Class