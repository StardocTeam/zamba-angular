Imports Zamba.Core.Enumerators
Imports Zamba.AdminControls

''' <summary>
'''      Formulario Utilizado para Agregar Reglas al WorkFlow.
'''      Las reglas se habilitan asigando el valor "true" a la propiedad
'''      "Enable" del option box correspondiente y deben registrarse en 
'''      el enumerador de Acciones de Reglas WFRuleParent
''' </summary>
''' <remarks></remarks>
Public Class AddNewProcessFuturo
    Inherits ZForm

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
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents RulesTree As System.Windows.Forms.TreeView
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(AddNewProcessFuturo))
        Panel1 = New ZPanel()
        Label4 = New ZLabel()
        btnAceptar = New ZButton()
        Label1 = New ZLabel()
        RulesTree = New System.Windows.Forms.TreeView()
        TreeView1 = New System.Windows.Forms.TreeView()
        TextBox1 = New TextBox()
        Label2 = New ZLabel()
        Label3 = New ZLabel()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.Location = New System.Drawing.Point(16, 42)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(514, 10)
        Panel1.TabIndex = 5
        '
        'Label4
        '
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Verdana", 14.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label4.Location = New System.Drawing.Point(14, 14)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(414, 24)
        Label4.TabIndex = 6
        Label4.Text = "Seleccione el tipo de proceso"
        '
        'btnAceptar
        '
        btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnAceptar.Location = New System.Drawing.Point(427, 560)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(103, 29)
        btnAceptar.TabIndex = 14
        btnAceptar.Text = "Generar"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Tahoma", 12.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(14, 520)
        Label1.MaximumSize = New System.Drawing.Size(516, 0)
        Label1.MinimumSize = New System.Drawing.Size(0, 19)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(196, 19)
        Label1.TabIndex = 81
        Label1.Text = "Buscar regla manualmente"
        Label1.Visible = False
        '
        'RulesTree
        '
        RulesTree.BackColor = System.Drawing.Color.White
        RulesTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        RulesTree.HideSelection = False
        RulesTree.HotTracking = True
        RulesTree.Location = New System.Drawing.Point(16, 318)
        RulesTree.Name = "RulesTree"
        RulesTree.ShowNodeToolTips = True
        RulesTree.Size = New System.Drawing.Size(514, 193)
        RulesTree.TabIndex = 82
        '
        'TreeView1
        '
        TreeView1.BackColor = System.Drawing.Color.White
        TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        TreeView1.HideSelection = False
        TreeView1.HotTracking = True
        TreeView1.Location = New System.Drawing.Point(18, 59)
        TreeView1.Name = "TreeView1"
        TreeView1.ShowNodeToolTips = True
        TreeView1.Size = New System.Drawing.Size(512, 172)
        TreeView1.TabIndex = 84
        '
        'TextBox1
        '
        TextBox1.Location = New System.Drawing.Point(18, 287)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New System.Drawing.Size(509, 21)
        TextBox1.TabIndex = 85
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Tahoma", 12.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(14, 265)
        Label2.MaximumSize = New System.Drawing.Size(516, 0)
        Label2.MinimumSize = New System.Drawing.Size(0, 19)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(216, 19)
        Label2.TabIndex = 86
        Label2.Text = "Buscar proceso manualmente"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New Font("Tahoma", 12.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(14, 520)
        Label3.MaximumSize = New System.Drawing.Size(516, 0)
        Label3.MinimumSize = New System.Drawing.Size(0, 19)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(279, 19)
        Label3.TabIndex = 87
        Label3.Text = "Proceso que envia mail a destinatarios"
        '
        'AddNewProcessFuturo
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        ClientSize = New System.Drawing.Size(551, 594)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(TextBox1)
        Controls.Add(TreeView1)
        Controls.Add(RulesTree)
        Controls.Add(Label1)
        Controls.Add(btnAceptar)
        Controls.Add(Label4)
        Controls.Add(Panel1)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "AddNewProcessFuturo"
        ShowInTaskbar = False
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "Asistente de creacion de nueva proceso"
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region

#Region "Atributos"

    Dim RulesDescriptions As New SortedList(Of String, RulesDescription)
    Public SelectedRuleName As String
    '   Dim Rules As New Hashtable
    '   Dim RulesHelp As New Hashtable
    '   Dim RulesCategory As New Hashtable
    Private FilterUIRules As Boolean = False
    Dim BaseNode As BaseWFNode

#End Region

    Dim DS As New DsRules

#Region "Constructores"

    Dim IL As Zamba.AppBlock.ZIconsList

    Public Sub New(ByVal BaseNode As BaseWFNode, ByVal IL As Zamba.AppBlock.ZIconsList, ByVal StepId As Int64, ByVal blnFilterUIRules As Boolean)
        MyBase.New()
        InitializeComponent()
        Me.BaseNode = BaseNode
        FilterUIRules = blnFilterUIRules
        Me.IL = IL
        RulesTree.ImageList = IL.ZIconList
        DS = WFRulesBusiness.GetRulesDSByStepID(StepId, False)
    End Sub

#End Region

#Region "Eventos"

    Private Class RulesDescription


        Public Sub New(ByVal Description As String, ByVal Help As String, ByVal Category As String, ByVal RuleName As String, ByVal IconId As Int32)
            Me.Description = Description
            Me.Help = Help
            Me.Category = Category
            Me.RuleName = RuleName
            Me.IconId = IconId

        End Sub
        Public Description As String
        Public Help As String
        Public Category As String
        Public RuleName As String
        Public IconId As Int32
    End Class



    ''' <summary>
    ''' Carga el arbol de reglas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddNewRule_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

        LoadProcessTypes()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click

        SelectedRuleName = RulesTree.SelectedNode.Text
        Dim RuleId As Int64 = RulesTree.SelectedNode.Tag

        Dim TypeofRule As TypesofRules
        Try


            If BaseNode.NodeWFType = NodeWFTypes.TipoDeRegla AndAlso DirectCast(BaseNode, RuleTypeNode).RuleParentType = TypesofRules.Eventos Then
                'presentar un formulario para elegir el evento
                Dim forms As New WFUserControl.frmchooseevent
                forms.ShowDialog()
                TypeofRule = DirectCast(forms.Selected, TypesofRules)
            Else
                TypeofRule = TypesofRules.Regla
            End If

            If SelectedRuleName Is Nothing OrElse SelectedRuleName = String.Empty Then
                MessageBox.Show("Debe seleccionar una regla", "Creacion de Reglas")
            Else

                Dim NewRuleId As Int64 = WFRulesBusiness.Add("DoExecuteRule", BaseNode, "Execute " & SelectedRuleName, TypeofRule)
                WFRulesBusiness.UpdateParamItem(NewRuleId, 0, RuleId)
                WFRulesBusiness.UpdateParamItem(NewRuleId, 1, False)
                Close()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub



    Private Sub lstRules_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If Chr(13) = e.KeyChar() Then
                Aceptar()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Método que se ejecuta tras presionar el botón Aceptar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    03/09/2008  Modified
    '''     [Gaston]    16/04/2009  Modified    Se agrego un límite para la cantidad de caracteres máximo que puede tener el nombre de una regla
    '''      [Sebastián] 21-04-2009 Modified    Se agregó una línea de código para no poner la palabra "regla" como parte del nombre
    ''' </history>
    Private Sub Aceptar()

        Dim TypeofRule As TypesofRules
        Try


            If BaseNode.NodeWFType = NodeWFTypes.TipoDeRegla AndAlso DirectCast(BaseNode, RuleTypeNode).RuleParentType = TypesofRules.Eventos Then
                'presentar un formulario para elegir el evento
                Dim forms As New WFUserControl.frmchooseevent
                forms.ShowDialog()
                TypeofRule = DirectCast(forms.Selected, TypesofRules)
            Else
                TypeofRule = TypesofRules.Regla
            End If

            If SelectedRuleName Is Nothing OrElse SelectedRuleName = String.Empty Then
                MessageBox.Show("Debe seleccionar una regla", "Creacion de Reglas")
            Else

                'Dim RuleNameFromUser As String = InputBox("Ingrese el nombre de la Regla", "Agregar Regla" Me.SelectedRuleName)

                Dim RuleNameFromUser As String
                Dim frm As New frmInputBox("Ingrese el nombre de la regla", 2000, SelectedRuleName, "Ingrese el nombre de la regla")
                frm.StartPosition = FormStartPosition.CenterParent
                frm.BringToFront()
                frm.ShowDialog()
                If frm.DialogResult <> DialogResult.Cancel Then
                    RuleNameFromUser = frm.Name.Replace(Chr(39), "")
                End If


                ' Si el string está vacío significa que se presiono el botón Cancel, aunque también puede ser que la caja de texto este vacía.
                ' En ese caso pasaría lo mismo, es decir no se crea la regla (como si se hubiera presionado el botón Cancelar) 
                If (RuleNameFromUser <> String.Empty) Then
                    '[Sebastián 21-04-09] Se agrego esta validación para que no se pueda poner la 
                    ' palabra "regla" como parte del nombre de la misma.
                    '[Sebastian 03-11-2009] Revisar que sea lo que pide el WI 3527. Donde no dejaba ingresar nada con la pala
                    'bra regla.
                    If RuleNameFromUser.Length <= 2000 And RuleNameFromUser.ToLower.CompareTo("regla") <> 0 Then
                        WFRulesBusiness.Add(SelectedRuleName, BaseNode, RuleNameFromUser, TypeofRule)
                    Else
                        'MessageBox.Show("El tamaño máximo para el nombre de la regla no puede exceder los 2000 caracteres", "Zamba Software")
                        MessageBox.Show("Error en el nombre de la regla o excede los 2000 caracteres", "Zamba Software")
                    End If
                End If

                Close()

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

    ''' <summary>
    ''' [Sebastian 28-07-2009] MODIFIED Se agrego if para validad que el nombre de la regla no venga vacio sino tira Excepction
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RulesTree_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles RulesTree.AfterSelect
        Try
            If IsNothing(RulesTree.SelectedNode.Tag) = False Then
                SelectedRuleName = RulesTree.SelectedNode.Tag.ToString
            Else
                SelectedRuleName = String.Empty
            End If
            Label1.Text = RulesTree.SelectedNode.ToolTipText
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub RulesTree_MouseHover(ByVal sender As Object, ByVal e As EventArgs) Handles RulesTree.MouseHover
        Try
            If Not IsNothing(RulesTree.SelectedNode) Then
                Label1.Text = RulesTree.SelectedNode.ToolTipText
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub RulesTree_NodeMouseHover(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseHoverEventArgs) Handles RulesTree.NodeMouseHover

    End Sub

    Private Sub LoadProcessTypes()

        For Each R As DsRules.WFRulesRow In DS.WFRules.Rows
            If R.ParentId = 0 Then
                Dim N As New TreeNode(R.Name)
                N.Tag = R.Id
                TreeView1.Nodes.Add(N)
            End If
        Next
    End Sub

    Private Sub LoadProcessRules(ProcessNode As TreeNode)
        Dim Dv As New DataView(DS.WFRules)
        Dv.RowFilter = ("ParentId = " & ProcessNode.Tag)

        For Each R As DataRow In Dv.ToTable.Rows
            Dim N As New TreeNode(R("Name"))
            N.Tag = R("Id")
            RulesTree.Nodes.Add(N)
        Next

    End Sub


    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        LoadProcessRules(e.Node)
        'If e.Node.Level = 0 AndAlso e.Node.Nodes.Count = 0 Then
        '    LoadMainCategories(e.Node)
        'ElseIf e.Node.Level = 1 AndAlso e.Node.Nodes.Count = 0 Then
        '    LoadSubCategories(e.Node)
        'End If
    End Sub
End Class