Imports zamba.Core

Public Class UCStepCtrl
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Private Sub New()
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

    Private WithEvents TxtDescription As TextBox
    Friend WithEvents Label3 As ZLabel
    Private WithEvents txtId As ZLabel
    Friend WithEvents Label8 As ZLabel

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents BtnSave As ZButton
    Private WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents BtnAdd As ZButton
    Friend WithEvents BtnEdit As ZButton
    Friend WithEvents BtnDel As ZButton
    Friend WithEvents BtnInitial As ZButton
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents txtexpire As TextBox
    Friend WithEvents txtmaxdocs As TextBox
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents Label7 As ZLabel
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents btnEditDescription As ZButton
    Friend WithEvents TxtName As TextBox
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents PanelTop As ZLabel
    Friend WithEvents txtdesc As TextBox
    Friend WithEvents btnSave2 As ZButton
    Friend WithEvents btnRemove As ZButton
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents cbColors As ComboBox
    Friend WithEvents rdbMonth As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDay As System.Windows.Forms.RadioButton
    Friend WithEvents lstNumbersTypesAndColors As ListBox
    Friend WithEvents lblPrueba As ZLabel
    Friend WithEvents cbNumbers As ComboBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel As ZPanel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(UCStepCtrl))
        TxtName = New TextBox()
        TxtDescription = New TextBox()
        Label3 = New ZLabel()
        txtId = New ZLabel()
        Label8 = New ZLabel()
        BtnSave = New ZButton()
        Panel = New ZPanel()
        CheckBox3 = New System.Windows.Forms.CheckBox()
        CheckBox2 = New System.Windows.Forms.CheckBox()
        btnSave2 = New ZButton()
        btnRemove = New ZButton()
        Label4 = New ZLabel()
        txtdesc = New TextBox()
        Label2 = New ZLabel()
        cbColors = New ComboBox()
        PictureBox1 = New System.Windows.Forms.PictureBox()
        rdbMonth = New System.Windows.Forms.RadioButton()
        CheckBox1 = New System.Windows.Forms.CheckBox()
        rdbDay = New System.Windows.Forms.RadioButton()
        txtexpire = New TextBox()
        lstNumbersTypesAndColors = New ListBox()
        txtmaxdocs = New TextBox()
        lblPrueba = New ZLabel()
        cbNumbers = New ComboBox()
        Label7 = New ZLabel()
        Label6 = New ZLabel()
        Label1 = New ZLabel()
        TextBox1 = New TextBox()
        btnEditDescription = New ZButton()
        BtnInitial = New ZButton()
        BtnDel = New ZButton()
        BtnEdit = New ZButton()
        BtnAdd = New ZButton()
        Label5 = New ZLabel()
        ListView1 = New System.Windows.Forms.ListView()
        ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ImageList1 = New ImageList(components)
        PanelTop = New ZLabel()
        Panel.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'TxtName
        '
        TxtName.BackColor = System.Drawing.Color.White
        TxtName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        TxtName.Location = New System.Drawing.Point(96, 15)
        TxtName.MaxLength = 0
        TxtName.Name = "TxtName"
        TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        TxtName.Size = New System.Drawing.Size(256, 21)
        TxtName.TabIndex = 0
        TxtName.WordWrap = False
        '
        'TxtDescription
        '
        TxtDescription.BackColor = System.Drawing.Color.White
        TxtDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl
        TxtDescription.Location = New System.Drawing.Point(96, 43)
        TxtDescription.MaxLength = 0
        TxtDescription.Name = "TxtDescription"
        TxtDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        TxtDescription.Size = New System.Drawing.Size(256, 21)
        TxtDescription.TabIndex = 1
        TxtDescription.WordWrap = False
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label3.Location = New System.Drawing.Point(9, 11)
        Label3.Name = "Label3"
        Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Label3.Size = New System.Drawing.Size(56, 16)
        Label3.TabIndex = 0
        Label3.Text = "Nombre"
        '
        'txtId
        '
        txtId.BackColor = System.Drawing.Color.Transparent
        txtId.ImeMode = System.Windows.Forms.ImeMode.NoControl
        txtId.Location = New System.Drawing.Point(38, 155)
        txtId.Name = "txtId"
        txtId.RightToLeft = System.Windows.Forms.RightToLeft.No
        txtId.Size = New System.Drawing.Size(102, 24)
        txtId.TabIndex = 0
        '
        'Label8
        '
        Label8.BackColor = System.Drawing.Color.Transparent
        Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label8.Location = New System.Drawing.Point(9, 43)
        Label8.Name = "Label8"
        Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Label8.Size = New System.Drawing.Size(81, 24)
        Label8.TabIndex = 0
        Label8.Text = "Descripcion"
        '
        'BtnSave
        '
        BtnSave.DialogResult = System.Windows.Forms.DialogResult.None
        BtnSave.Location = New System.Drawing.Point(269, 230)
        BtnSave.Name = "BtnSave"
        BtnSave.Size = New System.Drawing.Size(80, 24)
        BtnSave.TabIndex = 6
        BtnSave.Text = "Guardar"
        '
        'Panel
        '
        Panel.AutoScroll = True
        Panel.BackColor = System.Drawing.Color.Transparent
        Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel.Controls.Add(CheckBox3)
        Panel.Controls.Add(CheckBox2)
        Panel.Controls.Add(btnSave2)
        Panel.Controls.Add(btnRemove)
        Panel.Controls.Add(Label4)
        Panel.Controls.Add(txtdesc)
        Panel.Controls.Add(Label2)
        Panel.Controls.Add(cbColors)
        Panel.Controls.Add(PictureBox1)
        Panel.Controls.Add(rdbMonth)
        Panel.Controls.Add(CheckBox1)
        Panel.Controls.Add(rdbDay)
        Panel.Controls.Add(txtexpire)
        Panel.Controls.Add(lstNumbersTypesAndColors)
        Panel.Controls.Add(txtmaxdocs)
        Panel.Controls.Add(lblPrueba)
        Panel.Controls.Add(cbNumbers)
        Panel.Controls.Add(Label7)
        Panel.Controls.Add(Label6)
        Panel.Controls.Add(Label1)
        Panel.Controls.Add(TextBox1)
        Panel.Controls.Add(BtnSave)
        Panel.Controls.Add(Label3)
        Panel.Controls.Add(TxtDescription)
        Panel.Controls.Add(TxtName)
        Panel.Controls.Add(txtId)
        Panel.Controls.Add(Label8)
        Panel.Controls.Add(btnEditDescription)
        Panel.Controls.Add(BtnInitial)
        Panel.Controls.Add(BtnDel)
        Panel.Controls.Add(BtnEdit)
        Panel.Controls.Add(BtnAdd)
        Panel.Controls.Add(Label5)
        Panel.Controls.Add(ListView1)
        Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Panel.Location = New System.Drawing.Point(0, 32)
        Panel.Name = "Panel"
        Panel.Size = New System.Drawing.Size(507, 747)
        Panel.TabIndex = 3
        '
        'CheckBox3
        '
        CheckBox3.BackColor = System.Drawing.Color.Transparent
        CheckBox3.Location = New System.Drawing.Point(12, 222)
        CheckBox3.Name = "CheckBox3"
        CheckBox3.Size = New System.Drawing.Size(257, 32)
        CheckBox3.TabIndex = 111
        CheckBox3.Text = "No asignar a usuario al iniciar tarea si el usuario pertenece a otro grupo"
        CheckBox3.UseVisualStyleBackColor = False
        '
        'CheckBox2
        '
        CheckBox2.BackColor = System.Drawing.Color.Transparent
        CheckBox2.Location = New System.Drawing.Point(12, 196)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New System.Drawing.Size(340, 20)
        CheckBox2.TabIndex = 110
        CheckBox2.Text = "No asignar a usuario al iniciar tarea asignada a grupo de usuarios"
        CheckBox2.UseVisualStyleBackColor = False
        '
        'btnSave2
        '
        btnSave2.DialogResult = System.Windows.Forms.DialogResult.None
        btnSave2.Location = New System.Drawing.Point(227, 676)
        btnSave2.Name = "btnSave2"
        btnSave2.Size = New System.Drawing.Size(80, 24)
        btnSave2.TabIndex = 109
        btnSave2.Text = "Guardar"
        '
        'btnRemove
        '
        btnRemove.DialogResult = System.Windows.Forms.DialogResult.None
        btnRemove.Location = New System.Drawing.Point(227, 646)
        btnRemove.Name = "btnRemove"
        btnRemove.Size = New System.Drawing.Size(80, 24)
        btnRemove.TabIndex = 108
        btnRemove.Text = "Remover"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(9, 561)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(216, 13)
        Label4.TabIndex = 106
        Label4.Text = "de haber vencido la tarea, el color debe ser"
        '
        'txtdesc
        '
        txtdesc.BackColor = System.Drawing.Color.White
        txtdesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtdesc.Location = New System.Drawing.Point(9, 426)
        txtdesc.Name = "txtdesc"
        txtdesc.ReadOnly = True
        txtdesc.Size = New System.Drawing.Size(343, 21)
        txtdesc.TabIndex = 12
        txtdesc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label2.Location = New System.Drawing.Point(9, 155)
        Label2.Name = "Label2"
        Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Label2.Size = New System.Drawing.Size(23, 24)
        Label2.TabIndex = 11
        Label2.Text = "Id"
        '
        'cbColors
        '
        cbColors.Enabled = False
        cbColors.FormattingEnabled = True
        cbColors.Location = New System.Drawing.Point(227, 558)
        cbColors.Name = "cbColors"
        cbColors.Size = New System.Drawing.Size(116, 21)
        cbColors.TabIndex = 104
        '
        'PictureBox1
        '
        PictureBox1.Location = New System.Drawing.Point(272, 155)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New System.Drawing.Size(80, 32)
        PictureBox1.TabIndex = 10
        PictureBox1.TabStop = False
        '
        'rdbMonth
        '
        rdbMonth.AutoSize = True
        rdbMonth.Enabled = False
        rdbMonth.Location = New System.Drawing.Point(284, 528)
        rdbMonth.Name = "rdbMonth"
        rdbMonth.Size = New System.Drawing.Size(59, 17)
        rdbMonth.TabIndex = 103
        rdbMonth.TabStop = True
        rdbMonth.Text = "mes/es"
        rdbMonth.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        CheckBox1.BackColor = System.Drawing.Color.Transparent
        CheckBox1.Location = New System.Drawing.Point(146, 155)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New System.Drawing.Size(120, 32)
        CheckBox1.TabIndex = 5
        CheckBox1.Text = "Inicia Tarea al abrir el documento"
        CheckBox1.UseVisualStyleBackColor = False
        '
        'rdbDay
        '
        rdbDay.AutoSize = True
        rdbDay.Enabled = False
        rdbDay.Location = New System.Drawing.Point(227, 528)
        rdbDay.Name = "rdbDay"
        rdbDay.Size = New System.Drawing.Size(48, 17)
        rdbDay.TabIndex = 102
        rdbDay.TabStop = True
        rdbDay.Text = "día/s"
        rdbDay.UseVisualStyleBackColor = True
        '
        'txtexpire
        '
        txtexpire.BackColor = System.Drawing.Color.White
        txtexpire.ImeMode = System.Windows.Forms.ImeMode.NoControl
        txtexpire.Location = New System.Drawing.Point(96, 107)
        txtexpire.MaxLength = 0
        txtexpire.Name = "txtexpire"
        txtexpire.RightToLeft = System.Windows.Forms.RightToLeft.No
        txtexpire.Size = New System.Drawing.Size(48, 21)
        txtexpire.TabIndex = 3
        txtexpire.WordWrap = False
        '
        'lstNumbersTypesAndColors
        '
        lstNumbersTypesAndColors.Enabled = False
        lstNumbersTypesAndColors.FormattingEnabled = True
        lstNumbersTypesAndColors.Location = New System.Drawing.Point(12, 588)
        lstNumbersTypesAndColors.Name = "lstNumbersTypesAndColors"
        lstNumbersTypesAndColors.Size = New System.Drawing.Size(183, 147)
        lstNumbersTypesAndColors.TabIndex = 101
        '
        'txtmaxdocs
        '
        txtmaxdocs.BackColor = System.Drawing.Color.White
        txtmaxdocs.ImeMode = System.Windows.Forms.ImeMode.NoControl
        txtmaxdocs.Location = New System.Drawing.Point(288, 104)
        txtmaxdocs.MaxLength = 0
        txtmaxdocs.Name = "txtmaxdocs"
        txtmaxdocs.RightToLeft = System.Windows.Forms.RightToLeft.No
        txtmaxdocs.Size = New System.Drawing.Size(64, 21)
        txtmaxdocs.TabIndex = 4
        txtmaxdocs.WordWrap = False
        '
        'lblPrueba
        '
        lblPrueba.AutoSize = True
        lblPrueba.ForeColor = Zamba.AppBlock.ZambaUIHelpers.GetFontsColor
        lblPrueba.Location = New System.Drawing.Point(9, 530)
        lblPrueba.Name = "lblPrueba"
        lblPrueba.Size = New System.Drawing.Size(63, 13)
        lblPrueba.TabIndex = 100
        lblPrueba.Text = "Después de"
        '
        'cbNumbers
        '
        cbNumbers.FormattingEnabled = True
        cbNumbers.Location = New System.Drawing.Point(93, 527)
        cbNumbers.Name = "cbNumbers"
        cbNumbers.Size = New System.Drawing.Size(59, 21)
        cbNumbers.TabIndex = 99
        '
        'Label7
        '
        Label7.BackColor = System.Drawing.Color.Transparent
        Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label7.Location = New System.Drawing.Point(169, 104)
        Label7.Name = "Label7"
        Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Label7.Size = New System.Drawing.Size(113, 32)
        Label7.TabIndex = 8
        Label7.Text = "Cupo de Documentos"
        '
        'Label6
        '
        Label6.BackColor = System.Drawing.Color.Transparent
        Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label6.Location = New System.Drawing.Point(9, 107)
        Label6.Name = "Label6"
        Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Label6.Size = New System.Drawing.Size(72, 32)
        Label6.TabIndex = 7
        Label6.Text = "Vencimiento en Horas"
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label1.Location = New System.Drawing.Point(9, 72)
        Label1.Name = "Label1"
        Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Label1.Size = New System.Drawing.Size(48, 24)
        Label1.TabIndex = 3
        Label1.Text = "Ayuda"
        '
        'TextBox1
        '
        TextBox1.BackColor = System.Drawing.Color.White
        TextBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        TextBox1.Location = New System.Drawing.Point(96, 73)
        TextBox1.MaxLength = 0
        TextBox1.Name = "TextBox1"
        TextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        TextBox1.Size = New System.Drawing.Size(254, 21)
        TextBox1.TabIndex = 2
        TextBox1.WordWrap = False
        '
        'btnEditDescription
        '
        btnEditDescription.DialogResult = System.Windows.Forms.DialogResult.None
        btnEditDescription.Font = New Font("Tahoma", 8.25!)
        btnEditDescription.Location = New System.Drawing.Point(169, 477)
        btnEditDescription.Name = "btnEditDescription"
        btnEditDescription.Size = New System.Drawing.Size(183, 24)
        btnEditDescription.TabIndex = 5
        btnEditDescription.Text = "Cambiar Descripcion"
        '
        'BtnInitial
        '
        BtnInitial.DialogResult = System.Windows.Forms.DialogResult.None
        BtnInitial.Location = New System.Drawing.Point(242, 453)
        BtnInitial.Name = "BtnInitial"
        BtnInitial.Size = New System.Drawing.Size(110, 24)
        BtnInitial.TabIndex = 4
        BtnInitial.Text = " Inicial"
        '
        'BtnDel
        '
        BtnDel.DialogResult = System.Windows.Forms.DialogResult.None
        BtnDel.Location = New System.Drawing.Point(124, 453)
        BtnDel.Name = "BtnDel"
        BtnDel.Size = New System.Drawing.Size(115, 24)
        BtnDel.TabIndex = 3
        BtnDel.Text = "Eliminar"
        '
        'BtnEdit
        '
        BtnEdit.DialogResult = System.Windows.Forms.DialogResult.None
        BtnEdit.Location = New System.Drawing.Point(9, 477)
        BtnEdit.Name = "BtnEdit"
        BtnEdit.Size = New System.Drawing.Size(158, 24)
        BtnEdit.TabIndex = 2
        BtnEdit.Text = "Cambiar Nombre"
        '
        'BtnAdd
        '
        BtnAdd.DialogResult = System.Windows.Forms.DialogResult.None
        BtnAdd.Location = New System.Drawing.Point(9, 453)
        BtnAdd.Name = "BtnAdd"
        BtnAdd.Size = New System.Drawing.Size(112, 24)
        BtnAdd.TabIndex = 1
        BtnAdd.Text = "Agregar"
        '
        'Label5
        '
        Label5.Location = New System.Drawing.Point(6, 271)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(343, 23)
        Label5.TabIndex = 3
        Label5.Text = "Estados"
        '
        'ListView1
        '
        ListView1.BackColor = System.Drawing.Color.White
        ListView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader1})
        ListView1.FullRowSelect = True
        ListView1.GridLines = True
        ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        ListView1.Location = New System.Drawing.Point(9, 297)
        ListView1.MultiSelect = False
        ListView1.Name = "ListView1"
        ListView1.Size = New System.Drawing.Size(343, 121)
        ListView1.TabIndex = 2
        ListView1.UseCompatibleStateImageBehavior = False
        ListView1.View = System.Windows.Forms.View.List
        '
        'ColumnHeader1
        '
        ColumnHeader1.Text = ""
        ColumnHeader1.Width = 200
        '
        'ImageList1
        '
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        ImageList1.TransparentColor = System.Drawing.Color.Transparent
        ImageList1.Images.SetKeyName(0, "")
        ImageList1.Images.SetKeyName(1, "")
        ImageList1.Images.SetKeyName(2, "")
        ImageList1.Images.SetKeyName(3, "")
        ImageList1.Images.SetKeyName(4, "")
        ImageList1.Images.SetKeyName(5, "")
        ImageList1.Images.SetKeyName(6, "")
        ImageList1.Images.SetKeyName(7, "")
        ImageList1.Images.SetKeyName(8, "")
        ImageList1.Images.SetKeyName(9, "")
        ImageList1.Images.SetKeyName(10, "")
        ImageList1.Images.SetKeyName(11, "")
        ImageList1.Images.SetKeyName(12, "")
        '
        'PanelTop
        '
        PanelTop.BackColor = System.Drawing.Color.White
        PanelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        PanelTop.Font = New Font("Verdana", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        PanelTop.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        PanelTop.Location = New System.Drawing.Point(0, 0)
        PanelTop.Name = "PanelTop"
        PanelTop.Size = New System.Drawing.Size(507, 32)
        PanelTop.TabIndex = 98
        PanelTop.Text = "  Etapa"
        PanelTop.TextAlign = ContentAlignment.MiddleLeft
        '
        'UCStepCtrl
        '
        AutoScroll = True
        Controls.Add(Panel)
        Controls.Add(PanelTop)
        Name = "UCStepCtrl"
        Size = New System.Drawing.Size(507, 779)
        Panel.ResumeLayout(False)
        Panel.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

#End Region

    Dim WFStep As WFStep

    Public Sub New(ByRef WFStep As WFStep, ByVal isreadonly As Boolean)
        Me.New()
        RefreshStep(WFStep, isreadonly)
    End Sub

    ''' <summary>
    ''' Método que muestra los datos de una etapa
    ''' </summary>
    ''' <param name="WFStep"></param>
    ''' <param name="isreadonly"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    29/09/2008  Modified        Llamada al método loadNumbersAndColors
    ''' </history>
    Public Sub RefreshStep(ByRef WFStep As WFStep, ByVal isreadonly As Boolean)

        Me.WFStep = WFStep
        TxtName.Text = WFStep.Name
        TextBox1.Text = WFStep.Help
        TxtDescription.Text = WFStep.Description
        txtId.Text = WFStep.ID.ToString
        txtexpire.Text = WFStep.MaxHours.ToString()
        txtmaxdocs.Text = WFStep.MaxDocs.ToString()
        CheckBox1.Checked = WFStep.StartAtOpenDoc
        Enabled = isreadonly

        LoadStates()
        loadNumbersAndColors()

        Dim dt As DataTable = WFStepBusiness.getTypesOfPermit(WFStep.ID, False, TypesofPermits.DontAsignTaskAsignedToGroup)

        If dt.Rows.Count > 0 Then
            CheckBox2.Checked = dt.Rows(0)(2)
        Else
            CheckBox2.Checked = False
        End If

        dt = WFStepBusiness.getTypesOfPermit(WFStep.ID, False, TypesofPermits.DontAsignTaskOnInitIfAllowExecuteAllUserTask)

        If dt.Rows.Count > 0 Then
            CheckBox3.Checked = dt.Rows(0)(2)
        Else
            CheckBox3.Checked = False
        End If

        Try
            PictureBox1.Image = ImageList1.Images(WFStep.IconId)
        Catch ex As Exception
        End Try

    End Sub

    Public Event NameChanged(ByVal Name As String, ByRef wfstep As WFStep)

    Private Sub TxtDescription_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TxtDescription.TextChanged
        Try
            WFStep.Description = TxtDescription.Text.Trim
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub TxtName_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TxtName.TextChanged
        Try
            WFStep.Name = TxtName.Text.Trim
            RaiseEvent NameChanged(TxtName.Text.Trim, WFStep)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Event SaveChanges(ByRef wfstep As WFStep)

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            WFStepBusiness.removeTypeOfPermit(WFStep.ID, TypesofPermits.DontAsignTaskAsignedToGroup)
            WFStepBusiness.addTypeOfPermit(WFStep.ID, TypesofPermits.DontAsignTaskAsignedToGroup, CheckBox2.Checked, "0")

            WFStepBusiness.removeTypeOfPermit(WFStep.ID, TypesofPermits.DontAsignTaskOnInitIfAllowExecuteAllUserTask)
            WFStepBusiness.addTypeOfPermit(WFStep.ID, TypesofPermits.DontAsignTaskOnInitIfAllowExecuteAllUserTask, CheckBox3.Checked, "0")

            WFStepBusiness.UpdateStep(WFStep)
            MessageBox.Show("Cambios Guardados", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            RaiseEvent SaveChanges(WFStep)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TextBox1.TextChanged
        Try
            WFStep.Help = TextBox1.Text
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub txtexpire_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtexpire.TextChanged
        Try
            If IsNumeric(txtexpire.Text) Then
                'todo wf step: falta ver que en la base el tipo de dato y todo su tratamiento sea decimal, para poder especificar medias horas
                WFStep.MaxHours = CDec(txtexpire.Text)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub txtmaxdocs_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtmaxdocs.TextChanged
        Try
            WFStep.MaxDocs = Integer.Parse(txtmaxdocs.Text)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CheckBox1.CheckedChanged
        Try
            WFStep.StartAtOpenDoc = CheckBox1.Checked
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "Estados"
    Private Sub LoadStates()
        Try
            ListView1.Clear()
            For Each s As WFStepState In WFStep.States
                If Not s.ID = 0 Then
                    ListView1.Items.Add(New LStateItem(s, s.Initial))
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Class LStateItem
        Inherits ListViewItem
        Public State As wfstepstate
        Public description As String
        Private _Initial As Boolean
        Sub New(ByVal State As wfstepstate, ByVal Initial As Boolean)
            Me.State = State
            Me.Initial = Initial
            Text = Me.State.Name + " [" + Me.State.ID.ToString + "]"
        End Sub
        Public WriteOnly Property Initial() As Boolean
            Set(ByVal Value As Boolean)
                _Initial = Value
                If _Initial = True Then
                    ForeColor = Color.Blue
                Else
                    ForeColor = Color.FromArgb(76, 76, 76)
                End If
            End Set
        End Property
    End Class

    ''' <summary>
    ''' Evento que se ejecuta cuando se agrega un estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Tomás] - 08/05/2009 - Modified - Se realiza una validación en el ingreso del nombre y descripción
    '''                                       del evento para que no se duplique.
    ''' </history>
    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnAdd.Click
        Try
            'Obtiene el nombre
            Dim name As String = InputBox("Ingrese el nombre del Estado", "Nombre del Estado")
            'Valida que los datos sean correctos
            If isValidName(name) Then
                'Ingresa la descripción
                Dim description As String = InputBox("Ingrese la descripción del Estado", "Descripción del Estado")
                'Valida los datos de la descripción
                If isValidDescription(description) Then
                    Dim NewState As New wfstepstate(ToolsBusiness.GetNewID(IdTypes.WFSTEPSTATE), name.Trim, description.Trim, False)

                    WFStep.States.Add(NewState)
                    WFStepStatesBusiness.AddState(NewState, WFStep)
                    'Se agrega el estado a la lista
                    ListView1.Items.Add(New LStateItem(NewState, NewState.Initial))
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Cambiar Nombre
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/06/2008	Modified    Se cambio la llamada al método UpdateState
    ''' 	[Tomás] - 08/05/2009 - Modified - Se realiza una validación en el ingreso del nombre del evento para que no se duplique.
    ''' </history>
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnEdit.Click
        Try
            'Valida que haya uno seleccionado
            If ListView1.SelectedItems.Count > 0 Then
                'Obtiene el nuevo nombre
                Dim LStateItem As LStateItem = DirectCast(ListView1.SelectedItems(0), LStateItem)
                Dim oldName As String = LStateItem.State.Name
                Dim name As String = InputBox("Modifique el nombre del Estado", "Nombre del Estado", LStateItem.State.Name.Trim)
                'Valida que sea correcto
                If isValidName(name) Then
                    'Guarda y muestra los cambios
                    LStateItem.State.Name = name.Trim
                    WFStepStatesBusiness.UpdateState("Name", LStateItem.State.Name, CInt(LStateItem.State.ID), oldName)
                    LStateItem.Text = name.Trim
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Cambiar Descripcion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/06/2008	Modified    Se cambio la llamada al método UpdateState y se agrego txtDesc.Text para la actualización
    ''' 	[Tomás] - 08/05/2009 - Modified - Se realiza una validación en el ingreso de la descripción del evento para que no se duplique.
    ''' </history>
    Private Sub btnEditDescription_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEditDescription.Click
        Try
            'Valida que haya uno seleccionado
            If ListView1.SelectedItems.Count > 0 Then
                'Obtiene la descripcion
                Dim LStateItem As LStateItem = DirectCast(ListView1.SelectedItems(0), LStateItem)
                Dim description As String = InputBox("Modifique la descripción del Estado", "Descripción del Estado", LStateItem.State.Description)
                'Valida que sea correcta
                If isValidDescription(description) AndAlso Not String.Compare(description, LStateItem.State.Description, True) = 0 Then
                    'Guarda y muestra los cambios
                    LStateItem.State.Description = description
                    WFStepStatesBusiness.UpdateState("Description", LStateItem.State.Description, CInt(LStateItem.State.ID), LStateItem.State.Name)
                    txtdesc.Text = description
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnDel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnDel.Click
        Try
            If ListView1.SelectedItems.Count > 0 Then
                Dim LStateItem As LStateItem = DirectCast(ListView1.SelectedItems(0), LStateItem)
                WFStepStatesBusiness.RemoveState(LStateItem.State)
                WFStep.States.Remove(LStateItem.State)
                ListView1.Items.Remove(LStateItem)
                txtdesc.Clear()
                If WFStepStatesBusiness.GetInitialStateExistance(WFStep.ID) = 0 Then
                    MsgBox("La etapa se encuentra sin estado inicial. Configure un nuevo estado inicial por favor.", , "ATENCION")
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnInitial_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnInitial.Click
        If ListView1.SelectedItems.Count > 0 Then
            Try
                If ListView1.SelectedItems.Count > 0 Then
                    Dim LStateItem As LStateItem = DirectCast(ListView1.SelectedItems(0), LStateItem)
                    WFStepStatesBusiness.SetInitialState(LStateItem.State, WFStep)

                    For Each item As LStateItem In ListView1.Items
                        If item Is LStateItem Then
                            item.Initial = CBool(1)
                            item.State.Initial = True
                        Else
                            item.Initial = CBool(0)
                            item.State.Initial = False
                        End If
                    Next
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Valida que el nombre del estado sea correcto
    ''' </summary>
    ''' <param name="name">Nombre del estado</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Tomas] - 08/05/2009 - Created
    ''' </history>
    Private Function isValidName(ByVal name As String) As Boolean
        'Valido que existan datos
        If String.IsNullOrEmpty(name) Then
            Return False
        End If
        'Valido la capacidad
        If name.Trim.Length = 0 Then
            Return False
        End If
        If name.Trim.Length > 50 Then
            MessageBox.Show("El tamaño máximo permitido para el nombre es de 50 caracteres", "Error de ingreso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        'Valido si ya existe en el listbox
        For Each itemName As LStateItem In ListView1.Items
            If String.Compare(name.Trim, itemName.Text.Trim, True) = 0 Then
                MessageBox.Show("El nombre del estado que desea ingresar ya existe.", "Error de ingreso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
        Next
        Return True
    End Function

    ''' <summary>
    ''' Valida que el nombre de la descripción sea correcto
    '''
    ''' </summary>
    ''' <param name="name">Nombre de la descripción</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Tomas] - 08/05/2009 - Created
    '''     [Sebastian] 21/05/2009 MODIFIED se comento la validacion de esta vacio el comentario.
    ''' </history>
    Private Function isValidDescription(ByVal description As String) As Boolean
        'Valido que existan datos
        If String.IsNullOrEmpty(description) = True Then
            If MessageBox.Show("El campo de desripción está vacio. ¿Desea Continuar?", "Zamba Administrador", MessageBoxButtons.YesNo) = DialogResult.No Then
                Return False
            End If
        End If
        'Valido la capacidad
        If description.Trim.Length > 250 Then
            MessageBox.Show("El tamaño máximo permitido para la descripción es de 250 caracteres", "Error de ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

#End Region

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            If ListView1.SelectedItems.Count > 0 Then
                Dim LStateItem As LStateItem = DirectCast(ListView1.SelectedItems(0), LStateItem)
                txtdesc.Text = LStateItem.State.Description
                LStateItem = Nothing
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ' -------------------------------------------------------------------------------------------------------------------------------------
    ' -------------------------------------------------------------------------------------------------------------------------------------
    ' Configuración para mostrar el color de una tarea según la cantidad de días o meses que pasaron con respecto a su fecha de vencimiento

    ' [Gaston]    29/09/2008  Created
    ' -------------------------------------------------------------------------------------------------------------------------------------
    ' -------------------------------------------------------------------------------------------------------------------------------------
#Region "Vencimiento de Tareas"

    Private numbersCompleted As New ArrayList

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor del Atributo seleccionado en el combobox Numbers
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cbNumbers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cbNumbers.SelectedIndexChanged

        If Not IsNothing(cbNumbers.SelectedItem) Then

            For Each elem As NumberTypeAndColor In lstNumbersTypesAndColors.Items

                ' Si el número ya se encuentra en la lista que muestra número, tipo y color
                If (elem.Number = cbNumbers.SelectedItem.ToString()) Then

                    ' Si es tipo día entonces se deshabilita el radioButton "día/s" porque ya se encuentra en la lista
                    If (elem.Type = "día/s") Then
                        rdbDay.Enabled = False
                        rdbMonth.Enabled = True
                    Else
                        rdbDay.Enabled = True
                        rdbMonth.Enabled = False
                    End If

                    Exit Sub

                End If

            Next

            ' Si el elemento no se encuentra en la lista que muestra número, tipo y color entonces habilitar ambos radioButton
            rdbDay.Enabled = True
            rdbMonth.Enabled = True

        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al hacer click sobre el radioButton "día/s" o "mes/es"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdbDay_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdbDay.CheckedChanged, rdbMonth.CheckedChanged
        cbColors.Enabled = True
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor del Atributo seleccionado en el combobox Colors
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    29/09/2008  Created        
    ''' </history>
    Private Sub cbColors_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cbColors.SelectedIndexChanged

        If Not (IsNothing(cbColors.SelectedItem)) Then

            Dim dc As NumberTypeAndColor

            If (rdbDay.Checked = True) Then
                dc = New NumberTypeAndColor(cbNumbers.SelectedItem.ToString(), "día/s", cbColors.SelectedItem.ToString())
            Else
                dc = New NumberTypeAndColor(cbNumbers.SelectedItem.ToString(), "mes/es", cbColors.SelectedItem.ToString())
            End If

            lstNumbersTypesAndColors.Items.Add(dc)

            ' Si alguno de los radioButton es distinto a true entonces el número ya está como día y mes. Por lo tanto, se agrega en una colección
            ' el número que ya se encuentra en su totalidad en la lista (día y mes) y elimina del combobox los números 
            If ((rdbDay.Enabled = False) Or (rdbMonth.Enabled = False)) Then
                numbersCompleted.Add(cbNumbers.SelectedItem.ToString())
                cbNumbers.Items.Remove(cbNumbers.SelectedItem)
            End If

            cbColors.Items.Remove(cbColors.SelectedItem.ToString())

            rdbDay.Enabled = False
            rdbMonth.Enabled = False
            rdbDay.Checked = False
            rdbMonth.Checked = False
            cbColors.Enabled = False
            lstNumbersTypesAndColors.Enabled = True

            cbNumbers.SelectedItem = Nothing
            cbColors.SelectedItem = Nothing

            btnSave2.Enabled = True

        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor del Atributo seleccionado en el listbox NumbersTypesAndColors
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    29/09/2008  Created        
    ''' </history>
    Private Sub lstNumbersTypesAndColors_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstNumbersTypesAndColors.SelectedIndexChanged

        If Not IsNothing(lstNumbersTypesAndColors.SelectedItem) Then
            btnRemove.Enabled = True
        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al hacer un click sobre el botón Remover
    ''' [Sebastian 17-09-09] Modified Remove from Data Base and Hash Table
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    29/09/2008  Created        
    ''' </history>
    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnRemove.Click

        If Not IsNothing(lstNumbersTypesAndColors.SelectedItem) Then

            ' Se agrega el color al combobox de colores
            cbColors.Items.Add(CType(lstNumbersTypesAndColors.SelectedItem, NumberTypeAndColor).Color)
            cbColors.Sorted = True

            ' Si el número no se encuentra en el combobox que muestra los números entonces se agrega al correspondiente combobox
            If Not (cbNumbers.Items.Contains(CType(lstNumbersTypesAndColors.SelectedItem, NumberTypeAndColor).Number)) Then

                cbNumbers.Items.Clear()

                ' Se agrega al combobox los números 
                For counter As Integer = 1 To 31
                    cbNumbers.Items.Add(counter)
                Next

                numbersCompleted.Remove(CType(lstNumbersTypesAndColors.SelectedItem, NumberTypeAndColor).Number)

                ' Se eliminan del combobox los números que se encuentran ya en la lista con día y mes
                For Each elem As Integer In numbersCompleted
                    cbNumbers.Items.Remove(elem)
                Next

            End If

            ' Se elimina el elemento seleccionado perteneciente a la lista que muestra número, tipo y color
            lstNumbersTypesAndColors.Items.Remove(lstNumbersTypesAndColors.SelectedItem)
            'Remover color form Data Base
            WFStepBusiness.removeTypeOfPermit(WFStep.ID, TypesofPermits.Expired)
            SyncLock (Cache.Workflows.hsStepsOpt)
                'Remove color from hash
                Cache.Workflows.hsStepsOpt.Remove(WFStep.ID)
            End SyncLock
        End If

        btnRemove.Enabled = False

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al hacer un click sobre el botón Guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    29/09/2008  Created        
    ''' </history>
    Private Sub btnSave2_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSave2.Click

        If (lstNumbersTypesAndColors.Items.Count > 0) Then

            Try

                ' Se remueve de la base de datos, para remover antiguos valores
                WFStepBusiness.removeTypeOfPermit(WFStep.ID, TypesofPermits.Expired)

                For Each elem As NumberTypeAndColor In lstNumbersTypesAndColors.Items

                    Dim numberOfDays As Integer = CType(elem.Number, Integer)

                    If (elem.Type = "mes/es") Then

                        ' Los meses se reemplazan por la cantidad de días que corresponden con un mes de 30 días
                        Select Case numberOfDays
                            Case 1
                                numberOfDays = 30
                            Case 2
                                numberOfDays = 60
                            Case 3
                                numberOfDays = 90
                            Case 4
                                numberOfDays = 120
                            Case 5
                                numberOfDays = 150
                            Case 6
                                numberOfDays = 180
                            Case 7
                                numberOfDays = 210
                            Case 8
                                numberOfDays = 240
                            Case 9
                                numberOfDays = 270
                            Case 10
                                numberOfDays = 300
                            Case 11
                                numberOfDays = 330
                            Case 12
                                numberOfDays = 360
                            Case Else
                                numberOfDays = 400
                        End Select

                    End If

                    ' Se guarda en la base de datos
                    ' Parámetros : Id de etapa || Vencimiento || cantidad de días || color 
                    WFStepBusiness.addTypeOfPermit(WFStep.ID, TypesofPermits.Expired, numberOfDays, elem.Color)

                Next

                RaiseEvent SaveChanges(WFStep)

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

        Else
            WFStepBusiness.removeTypeOfPermit(WFStep.ID, TypesofPermits.Expired)
        End If

    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Método que sirve para cargar en los respectivos combobox los números y colores
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    29/09/2008  Created        
    '''                 23/10/2008  Modified    Se recupera de la base de datos
    ''' </history>
    Private Sub loadNumbersAndColors()

        lstNumbersTypesAndColors.DisplayMember = "NumberTypeAndColor"

        For counter As Integer = 1 To 31
            cbNumbers.Items.Add(counter)
        Next

        cbColors.Items.Add("Rojo")
        cbColors.Items.Add("Verde")
        cbColors.Items.Add("Amarillo")
        cbColors.Items.Add("Azul")
        cbColors.Items.Add("Violeta")
        cbColors.Items.Add("Gris")

        cbColors.Sorted = True

        Dim dt As DataTable = WFStepBusiness.getTypesOfPermit(WFStep.ID, False, TypesofPermits.Expired)
        Dim ban As Boolean = False

        If Not (IsNothing(dt)) Then

            Dim temp As New ArrayList

            If (dt.Rows.Count > 0) Then

                For Each dr As DataRow In dt.Rows

                    ban = False

                    Select Case (dr("ObjTwo").ToString())
                        Case "30"
                            dr("ObjTwo") = "1"
                        Case "60"
                            dr("ObjTwo") = "2"
                        Case "90"
                            dr("ObjTwo") = "3"
                        Case "120"
                            dr("ObjTwo") = "4"
                        Case "150"
                            dr("ObjTwo") = "5"
                        Case "180"
                            dr("ObjTwo") = "6"
                        Case "210"
                            dr("ObjTwo") = "7"
                        Case "240"
                            dr("ObjTwo") = "8"
                        Case "270"
                            dr("ObjTwo") = "9"
                        Case "300"
                            dr("ObjTwo") = "10"
                        Case "330"
                            dr("ObjTwo") = "11"
                        Case "360"
                            dr("ObjTwo") = "12"
                        Case Else
                            Dim dcDay As New NumberTypeAndColor(dr("ObjTwo").ToString(), "día/s", dr("ObjExtraData").ToString())
                            lstNumbersTypesAndColors.Items.Add(dcDay)
                            ban = True
                    End Select

                    If (ban = False) Then
                        Dim dcMonth As New NumberTypeAndColor(dr("ObjTwo").ToString(), "mes/es", dr("ObjExtraData").ToString())
                        lstNumbersTypesAndColors.Items.Add(dcMonth)
                    End If

                    cbColors.Items.Remove(dr("ObjExtraData").ToString())

                    ' Si el número no se encuentra en la colección entonces se agrega
                    If Not (temp.Contains(dr("ObjTwo").ToString())) Then
                        temp.Add(dr("ObjTwo").ToString())
                        ' de lo contrario si ya se encuentra entonces significa que el mismo número esta como día y mes, y por lo tanto
                        ' hay que eliminarlo del combobox Numbers
                    Else
                        numbersCompleted.Add(dr("ObjTwo").ToString())
                    End If

                Next

                If (numbersCompleted.Count > 0) Then
                    ' Se eliminan del combobox los números que se encuentran ya en la lista con día y mes
                    For Each elem As Integer In numbersCompleted
                        cbNumbers.Items.Remove(elem)
                    Next
                End If

                dt.Dispose()
                temp.Clear()
                lstNumbersTypesAndColors.Enabled = True
                btnRemove.Enabled = True

            End If

        End If

    End Sub

#End Region

#Region "Clase NumberTypeAndColor"

    Private Class NumberTypeAndColor

        Private m_number, m_color, m_type As String

        ' Contructor
        Public Sub New(ByVal number As String, ByVal type As String, ByVal color As String)
            m_number = number
            m_type = type
            m_color = color
        End Sub

        ' Propiedades
        Public ReadOnly Property NumberTypeAndColor() As String

            Get

                If (m_type.Contains("día/s")) Then

                    If (m_number.Length = 1) Then
                        Return (m_number & "    " & m_type & "         " & m_color)
                    Else
                        Return (m_number & "  " & m_type & "         " & m_color)
                    End If

                Else

                    If (m_number.Length = 1) Then
                        Return (m_number & "    " & m_type & "     " & m_color)
                    Else
                        Return (m_number & "  " & m_type & "     " & m_color)
                    End If

                End If

            End Get

        End Property

        Public Property Number() As String

            Get
                Return (m_number)
            End Get

            Set(ByVal value As String)
                m_number = value
            End Set

        End Property

        Public ReadOnly Property Type() As String

            Get
                Return (m_type)
            End Get

        End Property

        Public ReadOnly Property Color() As String

            Get
                Return (m_color)
            End Get

        End Property

    End Class

#End Region

#End Region
End Class