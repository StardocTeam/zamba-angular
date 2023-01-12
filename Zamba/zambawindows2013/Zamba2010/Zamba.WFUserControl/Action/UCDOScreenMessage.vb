'Imports Zamba.WFBusiness

Public Class UCDOScreenMessage
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
    Friend WithEvents txtMensaje As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents ChkDocumentName As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        components = New ComponentModel.Container()
        btnSeleccionar = New ZButton()
        lblMensaje = New ZLabel()
        txtMensaje = New Zamba.AppBlock.TextoInteligenteTextBox()
        ToolTip1 = New System.Windows.Forms.ToolTip(components)
        ChkDocumentName = New System.Windows.Forms.CheckBox()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(ChkDocumentName)
        tbRule.Controls.Add(txtMensaje)
        tbRule.Controls.Add(btnSeleccionar)
        tbRule.Controls.Add(lblMensaje)
        tbRule.Size = New System.Drawing.Size(517, 368)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(525, 397)
        '
        'btnSeleccionar
        '
        btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnSeleccionar.FlatStyle = FlatStyle.Flat
        btnSeleccionar.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnSeleccionar.ForeColor = System.Drawing.Color.White
        btnSeleccionar.Location = New System.Drawing.Point(370, 313)
        btnSeleccionar.Name = "btnSeleccionar"
        btnSeleccionar.Size = New System.Drawing.Size(118, 40)
        btnSeleccionar.TabIndex = 14
        btnSeleccionar.Text = "Guardar"
        btnSeleccionar.UseVisualStyleBackColor = False
        '
        'lblMensaje
        '
        lblMensaje.BackColor = System.Drawing.Color.Transparent
        lblMensaje.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblMensaje.FontSize = 9.75!
        lblMensaje.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblMensaje.Location = New System.Drawing.Point(24, 24)
        lblMensaje.Name = "lblMensaje"
        lblMensaje.Size = New System.Drawing.Size(336, 24)
        lblMensaje.TabIndex = 13
        lblMensaje.Text = "ESCRIBA EL MENSAJE A MOSTRAR EN PANTALLA"
        lblMensaje.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtMensaje
        '
        txtMensaje.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtMensaje.Location = New System.Drawing.Point(26, 59)
        txtMensaje.MaxLength = 0
        txtMensaje.Name = "txtMensaje"
        txtMensaje.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        txtMensaje.ShowSelectionMargin = True
        txtMensaje.Size = New System.Drawing.Size(462, 221)
        txtMensaje.TabIndex = 15
        txtMensaje.Text = ""
        '
        'ChkDocumentName
        '
        ChkDocumentName.AutoSize = True
        ChkDocumentName.Location = New System.Drawing.Point(26, 286)
        ChkDocumentName.Name = "ChkDocumentName"
        ChkDocumentName.Size = New System.Drawing.Size(303, 20)
        ChkDocumentName.TabIndex = 16
        ChkDocumentName.Text = "Ocultar nombre de documento en el título"
        ChkDocumentName.UseVisualStyleBackColor = True
        '
        'UCDOScreenMessage
        '
        Name = "UCDOScreenMessage"
        Size = New System.Drawing.Size(525, 397)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Private DoMsgActual As IDOSCREENMESSAGE
    Public Sub New(ByRef DoMsg As IDOSCREENMESSAGE, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoMsg, _wfPanelCircuit)
        InitializeComponent()
        DoMsgActual = DoMsg
        UCDOScreenMessage_Load()
        HasBeenModified = False
    End Sub

    ''' <summary>
    ''' Loads rule values and sets them in controls
    ''' </summary>
    ''' <history>
    '''     Javier  Modified    02/12/2010  HideDocumentName attribute added
    ''' </history>
    Private Sub UCDOScreenMessage_Load()
        txtMensaje.Text = DoMsgActual.Mensaje
        ChkDocumentName.Checked = DoMsgActual.HideDocumentName
        txtMensaje.ModificarColores()
        ToolTip1.SetToolTip(txtMensaje, "UTILIZANDO TEXTO INTELIGENTE PUEDE CONFIGURAR UN MENSAJE PERSONALIZADO")
    End Sub


    ''' <summary>
    ''' Save rule params
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier  Modified    02/12/2010  HideDocumentName attribute added
    ''' </history>
    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSeleccionar.Click
        Try
            DoMsgActual.Mensaje = txtMensaje.Text
            DoMsgActual.HideDocumentName = ChkDocumentName.Checked

            WFRulesBusiness.UpdateParamItem(DoMsgActual.ID, 0, DoMsgActual.Mensaje)
            WFRulesBusiness.UpdateParamItem(DoMsgActual.ID, 1, DoMsgActual.HideDocumentName, ObjectTypes.None)
            UserBusiness.Rights.SaveAction(DoMsgActual.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & DoMsgActual.Name & "(" & DoMsgActual.ID & ")")
            HasBeenModified = False
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


    Public Shadows ReadOnly Property MyRule() As IDOSCREENMESSAGE
        Get
            Return DirectCast(Rule, IDOSCREENMESSAGE)
        End Get
    End Property

    Private Sub txtMensaje_TextChanged(sender As Object, e As EventArgs) Handles txtMensaje.TextChanged
        Try
            If (txtMensaje.Text <> MyRule.Mensaje) Then
                HasBeenModified = True
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ChkDocumentName_CheckedChanged(sender As Object, e As EventArgs) Handles ChkDocumentName.CheckedChanged
        Try
            If (ChkDocumentName.Checked = MyRule.HideDocumentName) Then
                HasBeenModified = True
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub UCDOScreenMessage_Load(sender As Object, e As EventArgs) Handles Me.Load
        HasBeenModified = False
    End Sub
End Class
