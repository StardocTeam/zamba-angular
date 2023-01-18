'Imports Zamba.WFBusiness

''' <summary>
''' User Control de la Regla IfFileExits
''' </summary>
''' <remarks></remarks>
Public Class UCIfFileExists
    Inherits ZRuleControl

    Friend WithEvents btAceptar As ZButton
    Friend WithEvents tbPath As TextBox
    Friend WithEvents tbTextoInteligente As TextBox
    Friend WithEvents cbBuscarSubdirectorios As System.Windows.Forms.CheckBox
    Friend WithEvents btExaminar As ZButton
    Friend WithEvents lbTextoInteligente As ZLabel
    Friend WithEvents gbPath As GroupBox

    Private _rule As IIfFileExists

    Public Sub New(ByRef IfFileExist As IIfFileExists, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(IfFileExist, _wfPanelCircuit)
        InitializeComponent()
        _rule = IfFileExist
    End Sub

    Public Shadows ReadOnly Property MyRule() As IIfFileExists
        Get
            Return DirectCast(Rule, IIfFileExists)
        End Get
    End Property

    Private Overloads Sub InitializeComponent()
        btAceptar = New ZButton()
        tbPath = New TextBox()
        tbTextoInteligente = New TextBox()
        cbBuscarSubdirectorios = New System.Windows.Forms.CheckBox()
        btExaminar = New ZButton()
        lbTextoInteligente = New ZLabel()
        gbPath = New GroupBox()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        gbPath.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(tbTextoInteligente)
        tbRule.Controls.Add(lbTextoInteligente)
        tbRule.Controls.Add(gbPath)
        tbRule.Controls.Add(btAceptar)
        tbRule.Size = New System.Drawing.Size(607, 276)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(615, 305)
        '
        'btAceptar
        '
        btAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btAceptar.FlatStyle = FlatStyle.Flat
        btAceptar.ForeColor = System.Drawing.Color.White
        btAceptar.Location = New System.Drawing.Point(494, 160)
        btAceptar.Name = "btAceptar"
        btAceptar.Size = New System.Drawing.Size(95, 26)
        btAceptar.TabIndex = 1
        btAceptar.Text = "Guardar"
        btAceptar.UseVisualStyleBackColor = False
        '
        'tbPath
        '
        tbPath.Dock = System.Windows.Forms.DockStyle.Top
        tbPath.Location = New System.Drawing.Point(3, 19)
        tbPath.Name = "tbPath"
        tbPath.Size = New System.Drawing.Size(595, 23)
        tbPath.TabIndex = 2
        '
        'tbTextoInteligente
        '
        tbTextoInteligente.Dock = System.Windows.Forms.DockStyle.Top
        tbTextoInteligente.Location = New System.Drawing.Point(3, 119)
        tbTextoInteligente.Name = "tbTextoInteligente"
        tbTextoInteligente.Size = New System.Drawing.Size(601, 23)
        tbTextoInteligente.TabIndex = 3
        '
        'cbBuscarSubdirectorios
        '
        cbBuscarSubdirectorios.AutoSize = True
        cbBuscarSubdirectorios.BackColor = System.Drawing.Color.Transparent
        cbBuscarSubdirectorios.Location = New System.Drawing.Point(6, 56)
        cbBuscarSubdirectorios.Name = "cbBuscarSubdirectorios"
        cbBuscarSubdirectorios.Size = New System.Drawing.Size(189, 20)
        cbBuscarSubdirectorios.TabIndex = 4
        cbBuscarSubdirectorios.Text = "Buscar en subdirectorios"
        cbBuscarSubdirectorios.UseVisualStyleBackColor = False
        '
        'btExaminar
        '
        btExaminar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btExaminar.FlatStyle = FlatStyle.Flat
        btExaminar.ForeColor = System.Drawing.Color.White
        btExaminar.Location = New System.Drawing.Point(491, 48)
        btExaminar.Name = "btExaminar"
        btExaminar.Size = New System.Drawing.Size(104, 24)
        btExaminar.TabIndex = 5
        btExaminar.Text = "Examinar"
        btExaminar.UseVisualStyleBackColor = False
        '
        'lbTextoInteligente
        '
        lbTextoInteligente.AutoSize = True
        lbTextoInteligente.BackColor = System.Drawing.Color.Transparent
        lbTextoInteligente.Dock = System.Windows.Forms.DockStyle.Top
        lbTextoInteligente.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lbTextoInteligente.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lbTextoInteligente.Location = New System.Drawing.Point(3, 103)
        lbTextoInteligente.Name = "lbTextoInteligente"
        lbTextoInteligente.Size = New System.Drawing.Size(479, 16)
        lbTextoInteligente.TabIndex = 6
        lbTextoInteligente.Text = "Nombre de Archivo a buscar en directorio o Ruta Completa del archivo:"
        lbTextoInteligente.TextAlign = ContentAlignment.MiddleLeft
        '
        'gbPath
        '
        gbPath.BackColor = System.Drawing.Color.Transparent
        gbPath.Controls.Add(tbPath)
        gbPath.Controls.Add(cbBuscarSubdirectorios)
        gbPath.Controls.Add(btExaminar)
        gbPath.Dock = System.Windows.Forms.DockStyle.Top
        gbPath.Location = New System.Drawing.Point(3, 3)
        gbPath.Name = "gbPath"
        gbPath.Size = New System.Drawing.Size(601, 100)
        gbPath.TabIndex = 7
        gbPath.TabStop = False
        gbPath.Text = "Directorio de Busqueda"
        '
        'UCIfFileExists
        '
        BackColor = System.Drawing.Color.WhiteSmoke
        Name = "UCIfFileExists"
        Size = New System.Drawing.Size(615, 305)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        gbPath.ResumeLayout(False)
        gbPath.PerformLayout()
        ResumeLayout(False)

    End Sub

    Private Sub btAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btAceptar.Click
        If String.Compare(tbTextoInteligente.Text.Trim(), "") = 0 Then
            MessageBox.Show("Seleccione un archivo a buscar")
            Exit Sub
        End If

        _rule.SearchPath = tbPath.Text

        If cbBuscarSubdirectorios.Checked Then
            _rule.SearchOption = IO.SearchOption.AllDirectories
        Else
            _rule.SearchOption = IO.SearchOption.TopDirectoryOnly
        End If

        _rule.TextoInteligente = tbTextoInteligente.Text

        WFRulesBusiness.UpdateParamItem(_rule, 0, _rule.SearchPath)
        WFRulesBusiness.UpdateParamItem(_rule, 1, CInt(_rule.SearchOption))
        WFRulesBusiness.UpdateParamItem(_rule, 2, _rule.TextoInteligente)
        UserBusiness.Rights.SaveAction(_rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & _rule.Name & "(" & _rule.ID & ")")
    End Sub

    Private Sub UCIfFileExist_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsNothing(_rule.SearchPath) Then
            tbPath.Text = _rule.SearchPath
        End If

        If _rule.SearchOption = IO.SearchOption.AllDirectories Then
            cbBuscarSubdirectorios.Checked = True
        Else
            cbBuscarSubdirectorios.Checked = False
        End If

        If Not IsNothing(_rule.TextoInteligente) Then
            tbTextoInteligente.Text = _rule.TextoInteligente
        End If

    End Sub

    Private Sub btExaminar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btExaminar.Click
        Dim frmBrowseFolder As New FolderBrowserDialog()

        If frmBrowseFolder.ShowDialog() = DialogResult.OK Then
            tbPath.Text = frmBrowseFolder.SelectedPath
        End If

        frmBrowseFolder.Dispose()
    End Sub
End Class