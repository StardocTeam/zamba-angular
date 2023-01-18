Imports Zamba.Core.Enumerators
Imports System.Reflection
Imports Zamba.AdminControls

''' <summary>
'''      Formulario Utilizado para Agregar Reglas al WorkFlow.
'''      Las reglas se habilitan asigando el valor "true" a la propiedad
'''      "Enable" del option box correspondiente y deben registrarse en 
'''      el enumerador de Acciones de Reglas WFRuleParent
''' </summary>
''' <remarks></remarks>
Public Class AddNewRule
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
    Friend WithEvents RulesTree As System.Windows.Forms.TreeView
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(AddNewRule))
        Panel1 = New ZPanel
        Label4 = New ZLabel
        btnAceptar = New ZButton
        Label1 = New ZLabel
        RulesTree = New System.Windows.Forms.TreeView
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
        Label4.Size = New System.Drawing.Size(305, 24)
        Label4.TabIndex = 6
        Label4.Text = "Validaciones y Acciones"
        '
        'btnAceptar
        '
        btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnAceptar.Location = New System.Drawing.Point(423, 473)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(103, 29)
        btnAceptar.TabIndex = 14
        btnAceptar.Text = "Aceptar"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Tahoma", 12.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(14, 385)
        Label1.MaximumSize = New System.Drawing.Size(516, 0)
        Label1.MinimumSize = New System.Drawing.Size(0, 19)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(173, 19)
        Label1.TabIndex = 81
        Label1.Text = "Descripción de la Regla"
        '
        'RulesTree
        '
        RulesTree.BackColor = System.Drawing.Color.White
        RulesTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        RulesTree.HideSelection = False
        RulesTree.HotTracking = True
        RulesTree.Location = New System.Drawing.Point(18, 59)
        RulesTree.Name = "RulesTree"
        RulesTree.ShowNodeToolTips = True
        RulesTree.Size = New System.Drawing.Size(512, 323)
        RulesTree.TabIndex = 82
        '
        'AddNewRule
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        ClientSize = New System.Drawing.Size(552, 507)
        Controls.Add(RulesTree)
        Controls.Add(Label1)
        Controls.Add(btnAceptar)
        Controls.Add(Label4)
        Controls.Add(Panel1)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "AddNewRule"
        ShowInTaskbar = False
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "Agregar nueva regla"
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

#Region "Constructores"

    Dim IL As Zamba.AppBlock.ZIconsList
    Public Sub New(ByVal BaseNode As BaseWFNode, ByVal IL As Zamba.AppBlock.ZIconsList)
        MyBase.New()
        InitializeComponent()
        Me.BaseNode = BaseNode
        Me.IL = IL
        RulesTree.ImageList = IL.ZIconList
    End Sub

    Public Sub New(ByVal BaseNode As BaseWFNode, ByVal blnFilterUIRules As Boolean, ByVal IL As Zamba.AppBlock.ZIconsList)
        MyBase.New()
        InitializeComponent()
        Me.BaseNode = BaseNode
        FilterUIRules = True
        Me.IL = IL
        RulesTree.ImageList = IL.ZIconList
    End Sub

#End Region

#Region "Eventos"

    Private Class RulesDescription
        Public Sub New(ByVal Description As String, ByVal Help As String, ByVal Category As String, ByVal RuleName As String, ByVal IconId As Int32)
            Me.Description = Description
            Me.Help = Help
            Me.Category = Category
            Me.RuleName = RuleName
            Me.iconid = IconId
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
        Dim categories As New SortedList(Of String, Int32)

        Try
            Dim ruleList As New List(Of Object)
            Dim mList As New List(Of System.Type)

            Dim a As Assembly = System.Reflection.Assembly.LoadFile(System.Windows.Forms.Application.StartupPath & "\Zamba.WFActivity.Regular.dll")

            Dim Description As String = String.Empty
            Dim Help As String = String.Empty
            Dim Category As String = String.Empty
            Dim RuleName As String = String.Empty

            For Each M As System.Type In a.GetTypes
                If (M.Name.ToLower.StartsWith("if") OrElse M.Name.ToLower.StartsWith("do")) And String.Compare(M.Name.ToLower, "ifbranch") <> 0 Then
                    Dim IconId As Int32 = 31
                    For Each o As Object In M.GetCustomAttributes(True)
                        Try
                            If FilterUIRules Then
                                If String.Compare(o.GetType().Name.ToLower, "RuleFeatures".ToLower) = 0 Then
                                    If Not DirectCast(o, RuleFeatures).IsUI Then
                                        mList.Add(M)
                                    End If
                                End If
                            Else
                                If String.Compare(o.GetType().Name, "RuleDescription") = 0 Then
                                    If Not String.Compare(DirectCast(o, RuleDescription).Description, "Regla Padre") = 0 Then
                                        '                  Rules.Add(DirectCast(o, RuleDescription).Description, M.Name)
                                        Description = DirectCast(o, RuleDescription).Description
                                        RuleName = M.Name
                                    End If
                                ElseIf String.Compare(o.GetType().Name, "RuleHelp") = 0 Then
                                    If Not String.Compare(DirectCast(o, RuleHelp).Help, "Clase de la que heredan todas las reglas") = 0 Then
                                        '                  RulesHelp.Add(M.Name, DirectCast(o, RuleHelp).Help)
                                        Help = DirectCast(o, RuleHelp).Help
                                    End If
                                ElseIf String.Compare(o.GetType().Name, "RuleCategory") = 0 Then
                                    If Not String.Compare(DirectCast(o, RuleCategory).Category, "Clase de la que heredan todas las reglas") = 0 Then
                                        '                 RulesCategory.Add(M.Name, DirectCast(o, RuleCategory).Category)
                                        Category = DirectCast(o, RuleCategory).Category
                                    End If
                                ElseIf String.Compare(o.GetType().Name, "RuleIconId") = 0 Then
                                    If Not String.Compare(DirectCast(o, RuleIconId).IconId, "Clase de la que heredan todas las reglas") = 0 Then
                                        '                 RulesCategory.Add(M.Name, DirectCast(o, RuleCategory).Category)
                                        IconId = DirectCast(o, RuleIconId).IconId
                                    End If
                                End If
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Next
                    If Not FilterUIRules Then
                        RulesDescriptions.Add(Description, New RulesDescription(Description, Help, Category, RuleName, IconId))
                        If categories.ContainsKey(Category) = False Then
                            categories.Add(Category, IconId)
                        End If
                    End If
                End If
            Next
            For Each M As System.Type In mList
                Dim IconId As Int32 = 31
                If M.Name.ToLower.StartsWith("if") OrElse M.Name.ToLower.StartsWith("do") Then
                    For Each o As Object In M.GetCustomAttributes(True)
                        Try
                            If String.Compare(o.GetType().Name, "RuleDescription") = 0 Then
                                '        Rules.Add(DirectCast(o, RuleDescription).Description, M.Name)
                                Description = DirectCast(o, RuleDescription).Description
                                RuleName = M.Name
                            ElseIf String.Compare(o.GetType().Name, "RuleHelp") = 0 Then
                                '       RulesHelp.Add(M.Name, DirectCast(o, RuleHelp).Help)
                                Help = DirectCast(o, RuleHelp).Help
                            ElseIf String.Compare(o.GetType().Name, "RuleCategory") = 0 Then
                                '      RulesCategory.Add(M.Name, DirectCast(o, RuleCategory).Category)
                                Category = DirectCast(o, RuleCategory).Category
                            ElseIf String.Compare(o.GetType().Name, "RuleIconId") = 0 Then
                                '      RulesCategory.Add(M.Name, DirectCast(o, RuleCategory).Category)
                                IconId = DirectCast(o, RuleIconId).IconId
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Next
                    RulesDescriptions.Add(Description, New RulesDescription(Description, Help, Category, RuleName, IconId))
                    If categories.ContainsKey(Category) = False Then
                        categories.Add(Category, IconId)
                    End If
                End If
            Next

            For Each Cat As KeyValuePair(Of String, Int32) In categories
                RulesTree.Nodes.Add(Cat.Key, Cat.Key, Cat.Value)
            Next

            For Each RD As RulesDescription In RulesDescriptions.Values
                If RulesTree.Nodes.ContainsKey(RD.Category) = False Then RulesTree.Nodes.Add(RD.Category, RD.Category, RD.IconId)
                Dim N As New TreeNode(RD.Description, RD.IconId, RD.IconId)
                N.Tag = RD.RuleName
                N.ToolTipText = RD.Help
                RulesTree.Nodes.Item(RD.Category).Nodes.Add(N)
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            If Not IsNothing(categories) Then
                categories.Clear()
                categories = Nothing
            End If
        End Try
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Aceptar()
    End Sub

    'Private Sub lstRules_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstRules.SelectedIndexChanged
    '    If lstRules.SelectedItem Is Nothing = False Then

    '        Try
    '            If Rules.ContainsKey(Me.lstRules.SelectedItem) Then
    '                Me.SelectedRuleName = Rules(Me.lstRules.SelectedItem)
    '                If RulesHelp.ContainsKey(Me.SelectedRuleName) Then
    '                    Me.Label1.Text = RulesHelp.Item(Me.SelectedRuleName)
    '                Else
    '                    Me.Label1.Text = "Sin Descripcion."
    '                End If
    '            Else
    '                Me.Label1.Text = "Sin Descripcion."
    '            End If
    '        Catch ex As Exception

    '        End Try
    '    End If
    'End Sub

    Private Sub lstRules_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If Chr(13) = e.KeyChar() Then
                Aceptar()
            End If
        Catch
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
End Class