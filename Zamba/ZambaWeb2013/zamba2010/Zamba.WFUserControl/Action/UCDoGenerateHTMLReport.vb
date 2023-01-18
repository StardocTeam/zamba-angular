'Imports Zamba.WFBusiness

Public Class UCDoGenerateHTMLReport
    Inherits ZRuleControl


#Region " Código generado por el Diseñador de Windows Forms "

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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
    Friend WithEvents lblMensaje As ZLabel
    Friend Shadows WithEvents btnSeleccionar As ZButton
    Friend WithEvents cmbForms As ComboBox
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents txtReportName As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents cmbOrientation As ComboBox
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        components = New ComponentModel.Container()
        btnSeleccionar = New ZButton()
        lblMensaje = New ZLabel()
        ToolTip1 = New System.Windows.Forms.ToolTip(components)
        cmbForms = New ComboBox()
        txtReportName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Label3 = New ZLabel()
        cmbOrientation = New ComboBox()
        Label5 = New ZLabel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(cmbOrientation)
        tbRule.Controls.Add(Label5)
        tbRule.Controls.Add(Label3)
        tbRule.Controls.Add(txtReportName)
        tbRule.Controls.Add(cmbForms)
        tbRule.Controls.Add(btnSeleccionar)
        tbRule.Controls.Add(lblMensaje)
        tbRule.Size = New System.Drawing.Size(517, 371)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(525, 397)
        '
        'btnSeleccionar
        '
        btnSeleccionar.BackColor = System.Drawing.Color.Gainsboro
        btnSeleccionar.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnSeleccionar.Location = New System.Drawing.Point(27, 181)
        btnSeleccionar.Name = "btnSeleccionar"
        btnSeleccionar.Size = New System.Drawing.Size(118, 40)
        btnSeleccionar.TabIndex = 14
        btnSeleccionar.Text = "ACEPTAR"
        btnSeleccionar.UseVisualStyleBackColor = False
        '
        'lblMensaje
        '
        lblMensaje.BackColor = System.Drawing.Color.Transparent
        lblMensaje.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblMensaje.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        lblMensaje.Location = New System.Drawing.Point(24, 24)
        lblMensaje.Name = "lblMensaje"
        lblMensaje.Size = New System.Drawing.Size(336, 24)
        lblMensaje.TabIndex = 13
        lblMensaje.Text = "Formulario plantilla"
        lblMensaje.TextAlign = ContentAlignment.MiddleLeft
        '
        'cmbForms
        '
        cmbForms.FormattingEnabled = True
        cmbForms.Location = New System.Drawing.Point(27, 51)
        cmbForms.Name = "cmbForms"
        cmbForms.Size = New System.Drawing.Size(461, 21)
        cmbForms.TabIndex = 16
        '
        'txtReportName
        '
        txtReportName.Location = New System.Drawing.Point(27, 102)
        txtReportName.Name = "txtReportName"
        txtReportName.Size = New System.Drawing.Size(461, 22)
        txtReportName.TabIndex = 17
        txtReportName.Text = ""
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label3.Location = New System.Drawing.Point(24, 75)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(336, 24)
        Label3.TabIndex = 18
        Label3.Text = "Nombre del reporte"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'cmbOrientation
        '
        cmbOrientation.FormattingEnabled = True
        cmbOrientation.Location = New System.Drawing.Point(27, 154)
        cmbOrientation.Name = "cmbOrientation"
        cmbOrientation.Size = New System.Drawing.Size(461, 21)
        cmbOrientation.TabIndex = 20
        '
        'Label5
        '
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label5.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label5.Location = New System.Drawing.Point(24, 127)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(336, 24)
        Label5.TabIndex = 19
        Label5.Text = "Orientacion del reporte"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        '
        'UCDoGenerateHTMLReport
        '
        Name = "UCDoGenerateHTMLReport"
        Size = New System.Drawing.Size(525, 397)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Private _myrule As IDoGenerateHTMLReport
    Public Sub New(ByRef rule As IDoGenerateHTMLReport, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
        _myrule = rule
        Rule_Load()
    End Sub

    ''' <summary>
    ''' Loads rule values and sets them in controls
    ''' </summary>
    ''' <history>
    '''     Javier  Modified    02/12/2010  HideDocumentName attribute added
    ''' </history>
    Private Sub Rule_Load()
        Dim forms As ArrayList = FormBusiness.GetFormsByType(FormTypes.WebReport)

        If forms.Count > 0 Then
            cmbForms.DataSource = forms
            cmbForms.DisplayMember = "Name"
            cmbForms.ValueMember = "ID"
            cmbForms.SelectedValue = _myrule.FormId
            txtReportName.Text = _myrule.ReportName
        Else
            txtReportName.Text = "No existen formularios de tipo WebReport para mostrar"
            btnSeleccionar.Enabled = False
        End If

        Dim orientationValues As KeyValuePair(Of Long, String)()
        orientationValues = New KeyValuePair(Of Long, String)() {New KeyValuePair(Of Long, String)(0, "Vertical"),
                                                                 New KeyValuePair(Of Long, String)(1, "Horizontal")}

        cmbOrientation.DataSource = orientationValues
        cmbOrientation.DisplayMember = "Value"
        cmbOrientation.ValueMember = "Key"
        cmbOrientation.SelectedValue = Long.Parse(_myrule.ReportOrietation.ToString())
    End Sub


    ''' <summary>
    ''' Save rule params
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier  Modified    02/12/2010  HideDocumentName attribute added
    ''' </history>
    Private Sub btnSeleccionar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSeleccionar.Click
        Try
            If cmbForms.SelectedItem Is Nothing Then
                MessageBox.Show("Debe seleccionar un formulario", "Zamba Software")
                Return
            End If

            _myrule.FormId = cmbForms.SelectedItem.ID
            _myrule.ReportName = txtReportName.Text
            _myrule.ReportOrietation = cmbOrientation.SelectedValue

            WFRulesBusiness.UpdateParamItem(_myrule.ID, 0, MyRule.FormId)
            WFRulesBusiness.UpdateParamItem(_myrule.ID, 1, MyRule.ReportName)
            WFRulesBusiness.UpdateParamItem(_myrule.ID, 2, MyRule.ReportOrietation)
            UserBusiness.Rights.SaveAction(_myrule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & _myrule.Name & "(" & _myrule.ID & ")")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


    Public Shadows ReadOnly Property MyRule() As IDoGenerateHTMLReport
        Get
            Return _myrule
        End Get
    End Property

End Class
