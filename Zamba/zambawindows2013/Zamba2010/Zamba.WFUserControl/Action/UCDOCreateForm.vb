Imports Zamba.AdminControls
'Imports Zamba.WFBusiness
Imports zamba.Viewers

Public Class UCDOCreateForm
    Inherits ZRuleControl

    Private WithEvents WebForms As FormsControl
    Friend WithEvents pnlForms As System.Windows.Forms.Panel
    Friend WithEvents splForms As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlSalir As System.Windows.Forms.Panel
    Friend WithEvents btnSalir As ZButton
    Friend WithEvents pnlWFOpt As System.Windows.Forms.Panel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents txtVarDoc_id As TextBox
    Friend WithEvents chkAddToWf As System.Windows.Forms.CheckBox
    Private uc As UCSelectDoc_v2

    Public Sub New(ByRef po_CreateForm As IDOCreateForm, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(po_CreateForm, _wfPanelCircuit)
        InitializeComponent()
    End Sub

#Region "Inicializacion"


    Friend WithEvents Button1 As ZButton

    Private Overloads Sub InitializeComponent()
        pnlForms = New System.Windows.Forms.Panel
        splForms = New System.Windows.Forms.SplitContainer
        pnlSalir = New System.Windows.Forms.Panel
        btnSalir = New ZButton
        pnlWFOpt = New System.Windows.Forms.Panel
        Label2 = New ZLabel
        txtVarDoc_id = New TextBox
        chkAddToWf = New System.Windows.Forms.CheckBox
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        pnlForms.SuspendLayout()
        splForms.Panel2.SuspendLayout()
        splForms.SuspendLayout()
        pnlSalir.SuspendLayout()
        pnlWFOpt.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(pnlForms)
        tbRule.Size = New System.Drawing.Size(568, 310)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(576, 336)
        '
        'pnlForms
        '
        pnlForms.AutoScroll = True
        pnlForms.BackColor = System.Drawing.Color.Transparent
        pnlForms.Controls.Add(splForms)
        pnlForms.Dock = System.Windows.Forms.DockStyle.Fill
        pnlForms.Location = New System.Drawing.Point(3, 3)
        pnlForms.Name = "pnlForms"
        pnlForms.Size = New System.Drawing.Size(562, 304)
        pnlForms.TabIndex = 0
        '
        'splForms
        '
        splForms.Dock = System.Windows.Forms.DockStyle.Fill
        splForms.Location = New System.Drawing.Point(0, 0)
        splForms.Name = "splForms"
        splForms.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splForms.Panel1
        '
        splForms.Panel1.AutoScroll = True
        '
        'splForms.Panel2
        '
        splForms.Panel2.Controls.Add(pnlSalir)
        splForms.Panel2.Controls.Add(pnlWFOpt)
        splForms.Size = New System.Drawing.Size(562, 304)
        splForms.SplitterDistance = 261
        splForms.TabIndex = 1
        '
        'pnlSalir
        '
        pnlSalir.BackColor = System.Drawing.Color.Transparent
        pnlSalir.Controls.Add(btnSalir)
        pnlSalir.Dock = System.Windows.Forms.DockStyle.Fill
        pnlSalir.Location = New System.Drawing.Point(0, 0)
        pnlSalir.Name = "pnlSalir"
        pnlSalir.Size = New System.Drawing.Size(562, 39)
        pnlSalir.TabIndex = 4
        '
        'btnSalir
        '
        btnSalir.BackColor = System.Drawing.Color.PapayaWhip
        btnSalir.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        btnSalir.Location = New System.Drawing.Point(222, 6)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New System.Drawing.Size(75, 23)
        btnSalir.TabIndex = 0
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        '
        'pnlWFOpt
        '
        pnlWFOpt.Controls.Add(Label2)
        pnlWFOpt.Controls.Add(txtVarDoc_id)
        pnlWFOpt.Controls.Add(chkAddToWf)
        pnlWFOpt.Location = New System.Drawing.Point(0, 0)
        pnlWFOpt.Name = "pnlWFOpt"
        pnlWFOpt.Size = New System.Drawing.Size(428, 26)
        pnlWFOpt.TabIndex = 3
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(2, 6)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(79, 13)
        Label2.TabIndex = 4
        Label2.Text = "Variable doc_id"
        '
        'txtVarDoc_id
        '
        txtVarDoc_id.Location = New System.Drawing.Point(87, 3)
        txtVarDoc_id.Name = "txtVarDoc_id"
        txtVarDoc_id.Size = New System.Drawing.Size(140, 21)
        txtVarDoc_id.TabIndex = 3
        '
        'chkAddToWf
        '
        chkAddToWf.AutoSize = True
        chkAddToWf.Location = New System.Drawing.Point(255, 5)
        chkAddToWf.Name = "chkAddToWf"
        chkAddToWf.Size = New System.Drawing.Size(170, 17)
        chkAddToWf.TabIndex = 0
        chkAddToWf.Text = "Agregar Formulario a Worflow"
        chkAddToWf.UseVisualStyleBackColor = True
        '
        'UCDOCreateForm
        '
        Name = "UCDOCreateForm"
        Size = New System.Drawing.Size(576, 336)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        pnlForms.ResumeLayout(False)
        splForms.Panel2.ResumeLayout(False)
        splForms.ResumeLayout(False)
        pnlSalir.ResumeLayout(False)
        pnlWFOpt.ResumeLayout(False)
        pnlWFOpt.PerformLayout()
        ResumeLayout(False)

    End Sub
#End Region

    ''' <summary>
    ''' Evento que se ejecuta cuando se carga el user control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	18/07/2008	Modified    El contenido de HashTable se agrega a txtVarDoc_id
    ''' </history>
    Private Sub UCDOCreateForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load
        uc = New UCSelectDoc_v2(MyRule.DocTypeIdVirtual)
        WebForms = New FormsControl
        SuspendLayout()
        uc.AutoScroll = True
        WebForms.AutoScroll = True
        splForms.Panel1.Controls.Clear()
        splForms.Panel1.AutoScroll = True
        uc.Dock = DockStyle.Fill
        uc.Left = Left + ((Width - uc.Width) / 2)
        uc.Top = Top + ((Height - uc.Height) / 2)
        splForms.Panel1.Controls.Add(uc)
        'WebForms.Width = splForms.Panel1.Width
        WebForms.Visible = False
        splForms.Panel1.Controls.Add(WebForms)
        pnlSalir.Visible = False
        chkAddToWf.Checked = MyRule.AddToWf
        txtVarDoc_id.Text = MyRule.HashTable
        ResumeLayout()

        RemoveHandler uc.ActualizarDatos, AddressOf SeleccionarTipoDocumento
        RemoveHandler uc.VisualizarFormularios, AddressOf VisualizarFormularios
        AddHandler uc.ActualizarDatos, AddressOf SeleccionarTipoDocumento
        AddHandler uc.VisualizarFormularios, AddressOf VisualizarFormularios
    End Sub

    Private Sub RefrescarFormularios()
        uc.RefrescarFormularios()
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Visualiza el Control de formularios
    ''' </summary>
    ''' <param name="p_iDocTypeId"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	16/08/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub VisualizarFormularios(ByVal p_idocTypeId As Int64)
        Try
            Cursor = Cursors.AppStarting
            SuspendLayout()
            WebForms.Visible = True
            pnlSalir.Visible = True
            pnlWFOpt.Visible = False
            uc.Visible = False
            ResumeLayout()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Oculta el control de formularios
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	16/08/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub CerrarVisualizarFormularios()
        Try
            Cursor = Cursors.WaitCursor
            SuspendLayout()
            WebForms.Visible = False
            pnlSalir.Visible = False
            pnlWFOpt.Visible = True
            uc.Visible = True
            ResumeLayout()
            Cursor = Cursors.Arrow
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Guarda el estado de la regla
    ''' </summary>
    ''' <param name="p_iDocTypeIdVirtual">Id del Documento virtual a asociar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	    16/08/2006	Created
    ''' 	[Gaston]	18/07/2008	Modified    Se guarda el valor de txtVarDoc_id 
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub SeleccionarTipoDocumento(ByVal p_iDocTypeIdVirtual As Int32)

        Try

            WFRulesBusiness.UpdateParamItem(Rule, 0, p_iDocTypeIdVirtual)
            WFRulesBusiness.UpdateParamItem(Rule, 1, Convert.ToInt32(chkAddToWf.Checked()))

            If Not (String.IsNullOrEmpty(txtVarDoc_id.Text)) Then
                WFRulesBusiness.UpdateParamItem(Rule, 2, txtVarDoc_id.Text)
            End If
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
            MyRule.DocTypeIdVirtual = p_iDocTypeIdVirtual
            MyRule.AddToWf = chkAddToWf.Checked
            MyRule.HashTable = txtVarDoc_id.Text

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub BtnSalir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalir.Click
        CerrarVisualizarFormularios()
        RefrescarFormularios()
    End Sub

    Public Shadows ReadOnly Property MyRule() As IDOCreateForm
        Get
            Return DirectCast(Rule, IDOCreateForm)
        End Get
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class
