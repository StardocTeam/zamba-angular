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
Public Class AddNewRuleFuturo
    Inherits ZForm

#Region " Código generado por el Diseñador de Windows Forms "

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If

            parentRule = Nothing
            a = Nothing
            IL = Nothing

            If allRulesDescriptions IsNot Nothing Then
                allRulesDescriptions.Clear()
                allRulesDescriptions = Nothing
            End If
            If rulesDescriptions IsNot Nothing Then
                rulesDescriptions.Clear()
                rulesDescriptions = Nothing
            End If
            If rulesSearch IsNot Nothing Then
                rulesSearch.Clear()
                rulesSearch = Nothing
            End If
            If txtSearch IsNot Nothing Then
                RemoveHandler txtSearch.KeyUp, AddressOf txtSearchRule_KeyUp
                txtSearch.Dispose()
                txtSearch = Nothing
            End If
            If tvwRules IsNot Nothing Then
                tvwRules.Dispose()
                tvwRules = Nothing
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents lblCategory As ZLabel
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents tvwCategories As System.Windows.Forms.TreeView
    Friend WithEvents lblDescription As ZLabel
    Friend WithEvents lblRules As ZLabel
    Friend WithEvents lblSearch As ZLabel
    Friend WithEvents txtSearch As SearchBox
    Friend WithEvents tvwRules As System.Windows.Forms.TreeView
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(AddNewRuleFuturo))
        lblCategory = New ZLabel()
        btnAceptar = New ZButton()
        tvwRules = New System.Windows.Forms.TreeView()
        tvwCategories = New System.Windows.Forms.TreeView()
        lblDescription = New ZLabel()
        lblRules = New ZLabel()
        lblSearch = New ZLabel()
        txtSearch = New Zamba.AdminControls.SearchBox()
        SuspendLayout()
        '
        'lblCategory
        '
        lblCategory.AutoSize = True
        lblCategory.BackColor = System.Drawing.Color.Transparent
        lblCategory.Font = New Font("Tahoma", 11.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblCategory.Location = New System.Drawing.Point(15, 42)
        lblCategory.Name = "lblCategory"
        lblCategory.Size = New System.Drawing.Size(76, 18)
        lblCategory.TabIndex = 6
        lblCategory.Text = "Categorías"
        '
        'btnAceptar
        '
        btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnAceptar.FlatStyle = FlatStyle.Flat
        btnAceptar.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnAceptar.Location = New System.Drawing.Point(513, 380)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(95, 30)
        btnAceptar.TabIndex = 2
        btnAceptar.Text = "Agregar"
        '
        'tvwRules
        '
        tvwRules.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        tvwRules.BackColor = System.Drawing.Color.White
        tvwRules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        tvwRules.HideSelection = False
        tvwRules.HotTracking = True
        tvwRules.Location = New System.Drawing.Point(290, 63)
        tvwRules.Name = "tvwRules"
        tvwRules.ShowNodeToolTips = True
        tvwRules.Size = New System.Drawing.Size(318, 295)
        tvwRules.TabIndex = 1
        '
        'tvwCategories
        '
        tvwCategories.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        tvwCategories.BackColor = System.Drawing.Color.White
        tvwCategories.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        tvwCategories.HideSelection = False
        tvwCategories.HotTracking = True
        tvwCategories.Location = New System.Drawing.Point(18, 63)
        tvwCategories.Name = "tvwCategories"
        tvwCategories.ShowNodeToolTips = True
        tvwCategories.Size = New System.Drawing.Size(255, 295)
        tvwCategories.TabIndex = 0
        '
        'lblDescription
        '
        lblDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblDescription.BackColor = System.Drawing.Color.Transparent
        lblDescription.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblDescription.Location = New System.Drawing.Point(18, 361)
        lblDescription.MinimumSize = New System.Drawing.Size(0, 19)
        lblDescription.Name = "lblDescription"
        lblDescription.Size = New System.Drawing.Size(489, 49)
        lblDescription.TabIndex = 87
        lblDescription.Text = resources.GetString("lblDescription.Text")
        lblDescription.TextAlign = ContentAlignment.BottomLeft
        '
        'lblRules
        '
        lblRules.AutoSize = True
        lblRules.BackColor = System.Drawing.Color.Transparent
        lblRules.Font = New Font("Tahoma", 11.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblRules.Location = New System.Drawing.Point(287, 42)
        lblRules.Name = "lblRules"
        lblRules.Size = New System.Drawing.Size(50, 18)
        lblRules.TabIndex = 88
        lblRules.Text = "Reglas"
        '
        'lblSearch
        '
        lblSearch.AutoSize = True
        lblSearch.BackColor = System.Drawing.Color.Transparent
        lblSearch.Font = New Font("Tahoma", 11.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblSearch.Location = New System.Drawing.Point(15, 9)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New System.Drawing.Size(72, 18)
        lblSearch.TabIndex = 89
        lblSearch.Text = "Búsqueda"
        '
        'txtSearch
        '
        txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        txtSearch.Font = New Font("Tahoma", 9.0!, FontStyle.Italic, GraphicsUnit.Point, CType(0, Byte))
        txtSearch.ForeColor = System.Drawing.Color.Gray
        txtSearch.Location = New System.Drawing.Point(93, 8)
        txtSearch.Name = "txtSearch"
        txtSearch.ShadowText = Nothing
        txtSearch.Size = New System.Drawing.Size(515, 22)
        txtSearch.TabIndex = 3
        '
        'AddNewRuleFuturo
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        ClientSize = New System.Drawing.Size(624, 420)
        Controls.Add(lblSearch)
        Controls.Add(lblRules)
        Controls.Add(lblDescription)
        Controls.Add(txtSearch)
        Controls.Add(tvwCategories)
        Controls.Add(tvwRules)
        Controls.Add(btnAceptar)
        Controls.Add(lblCategory)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        MinimizeBox = False
        Name = "AddNewRuleFuturo"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "Asistente de creación de nueva regla"
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region

#Region "Atributos"
    Public Property SelectedRuleName() As String
    Public Property SelectedRuleClass() As String
    Dim allRulesDescriptions As New SortedList(Of String, RulesDescription)
    Dim rulesDescriptions As New SortedList(Of String, RulesDescription)
    Dim rulesSearch As New List(Of RulesDescription)
    Dim filterUIRules As Boolean = False
    Dim parentRule As IRule
    Dim a As Assembly = Nothing
    Dim IL As Zamba.AppBlock.ZIconsList
    Public Property NewRule() As IRule
#End Region

#Region "Constructores"
    Public Sub New(ByVal parentRule As IRule, ByVal IL As Zamba.AppBlock.ZIconsList)
        Me.New(parentRule, IL, False)
    End Sub


    Public Sub New(ByVal parentRule As IRule, ByVal IL As Zamba.AppBlock.ZIconsList, ByVal blnFilterUIRules As Boolean)
        MyBase.New()
        InitializeComponent()
        Me.parentRule = parentRule
        filterUIRules = blnFilterUIRules
        Me.IL = IL
        tvwRules.ImageList = IL.ZIconList

        txtSearch.ShadowText = "búsqueda manual"
        AddHandler txtSearch.KeyUp, AddressOf txtSearchRule_KeyUp
    End Sub
#End Region

#Region "Eventos y Métodos"
    Private Class RulesDescription
        Public Sub New(ByVal Description As String, ByVal Help As String, ByVal Category As String, ByVal RuleName As String, ByVal IconId As Int32, ByVal Maincategory As String, ByVal SubCategory As String)
            Me.Description = Description
            Me.Help = Help
            Me.Category = Category
            Me.MainCategory = Maincategory
            Me.SubCategory = SubCategory
            Me.RuleName = RuleName
            Me.IconId = IconId
        End Sub
        Public Description As String
        Public Help As String
        Public MainCategory As String
        Public SubCategory As String
        Public Category As String
        Public RuleName As String
        Public IconId As Int32

        ''' <summary>
        ''' Se sobreescribe el metodo ToString para facilitar la busqueda de reglas
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            Dim v As String = "|"
            v = String.Format(RuleName & v & Description & v & MainCategory & v & SubCategory & v & Category & v & Help)
            Return v
        End Function
    End Class

    ''' <summary>
    ''' Carga el arbol de reglas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddNewRule_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            'Se carga la lista de categorias principales
            LoadMainCategories()

            'Se cargan en memoria el detalle de todas las reglas
            FillRulesDescriptions("Todas")

            'Se hace una copia del listado para poder hacer búsquedas
            For Each rd As RulesDescription In rulesDescriptions.Values
                rulesSearch.Add(New RulesDescription(rd.Description, rd.Help, rd.Category, rd.RuleName, rd.IconId, rd.MainCategory, rd.SubCategory))
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            MessageBox.Show("Error al cargar las reglas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub LoadMainCategories()
        Dim Maincategories As New SortedList(Of String, Int32)
        Dim MainCategory As String = String.Empty
        Dim className As String

        Try
            a = System.Reflection.Assembly.LoadFile(System.Windows.Forms.Application.StartupPath & "\Zamba.WFActivity.Regular.dll")

            For Each M As System.Type In a.GetTypes
                className = M.Name.ToLower
                If (className.StartsWith("if") OrElse className.StartsWith("do")) AndAlso String.Compare(className, "ifbranch") <> 0 Then
                    For Each o As Object In M.GetCustomAttributes(True)
                        Try
                            If String.Compare(o.GetType().Name, "RuleMainCategory") = 0 Then
                                MainCategory = DirectCast(o, RuleMainCategory).MainCategory
                                If Not Maincategories.ContainsKey(MainCategory) Then
                                    If Not String.Compare(MainCategory, "Clase de la que heredan todas las reglas") = 0 Then
                                        Maincategories.Add(MainCategory, 31) '31 = icono
                                    Else
                                        MessageBox.Show("Clase de la que heredan todas las reglas")
                                    End If
                                End If
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Next
                End If
            Next

            tvwCategories.Nodes.Add("Todas", "Todas", 31)
            For Each Cat As KeyValuePair(Of String, Int32) In Maincategories
                tvwCategories.Nodes.Add(Cat.Key, Cat.Key, Cat.Value)
            Next

            tvwCategories.SelectedNode = tvwCategories.Nodes(0)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            MessageBox.Show("Error al cargar las reglas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Maincategories IsNot Nothing Then
                Maincategories.Clear()
                Maincategories = Nothing
            End If
        End Try
    End Sub
    Private Sub LoadCategories(selectedMainCategoryNode As TreeNode)
        Dim categories As New SortedList(Of String, Int32)
        Dim mainCategory As String = String.Empty
        Dim category As String = String.Empty
        Dim className As String

        Try
            For Each M As System.Type In a.GetTypes
                className = M.Name.ToLower
                If (className.StartsWith("if") OrElse className.StartsWith("do")) And String.Compare(className, "ifbranch") <> 0 Then
                    mainCategory = String.Empty
                    category = String.Empty

                    For Each o As Object In M.GetCustomAttributes(True)
                        Try
                            If String.Compare(o.GetType().Name, "RuleMainCategory") = 0 Then
                                If Not String.Compare(DirectCast(o, RuleMainCategory).MainCategory, "Clase de la que heredan todas las reglas") = 0 Then
                                    mainCategory = DirectCast(o, RuleMainCategory).MainCategory
                                Else
                                    MessageBox.Show("Clase de la que heredan todas las reglas")
                                End If
                            ElseIf String.Compare(o.GetType().Name, "RuleCategory") = 0 Then
                                If Not String.Compare(DirectCast(o, RuleCategory).Category, "Clase de la que heredan todas las reglas") = 0 Then
                                    category = DirectCast(o, RuleCategory).Category
                                    If mainCategory = selectedMainCategoryNode.Text AndAlso _
                                        Not categories.ContainsKey(category) AndAlso _
                                        Not String.IsNullOrEmpty(category) Then
                                        categories.Add(category, 31) '31 = icono
                                    End If
                                Else
                                    MessageBox.Show("Clase de la que heredan todas las reglas")
                                End If
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Next
                End If
            Next

            For Each Cat As KeyValuePair(Of String, Int32) In categories
                selectedMainCategoryNode.Nodes.Add(Cat.Key, Cat.Key, Cat.Value)
            Next
            selectedMainCategoryNode.Expand()

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            If Not IsNothing(categories) Then
                categories.Clear()
                categories = Nothing
            End If
        End Try
    End Sub
    Private Sub LoadSubCategories(selectedCategoryNode As TreeNode)
        Dim subcategories As New SortedList(Of String, Int32)
        Dim subCategory As String = String.Empty
        Dim category As String = String.Empty
        Dim className As String

        Try
            For Each M As System.Type In a.GetTypes
                className = M.Name.ToLower
                If (className.StartsWith("if") OrElse className.StartsWith("do")) And String.Compare(className, "ifbranch") <> 0 Then
                    subCategory = String.Empty
                    category = String.Empty

                    For Each o As Object In M.GetCustomAttributes(True)
                        Try
                            If String.Compare(o.GetType().Name, "RuleCategory") = 0 Then
                                If Not String.Compare(DirectCast(o, RuleCategory).Category, "Clase de la que heredan todas las reglas") = 0 Then
                                    category = DirectCast(o, RuleCategory).Category
                                Else
                                    MessageBox.Show("Clase de la que heredan todas las reglas")
                                End If
                            ElseIf String.Compare(o.GetType().Name, "RuleSubCategory") = 0 Then
                                If Not String.Compare(DirectCast(o, RuleSubCategory).SubCategory, "Clase de la que heredan todas las reglas") = 0 Then
                                    subCategory = DirectCast(o, RuleSubCategory).SubCategory
                                    If category = selectedCategoryNode.Text AndAlso _
                                        Not subcategories.ContainsKey(subCategory) AndAlso _
                                        Not String.IsNullOrEmpty(subCategory) Then
                                        subcategories.Add(subCategory, 31) '31 = icono
                                    End If
                                Else
                                    MessageBox.Show("Clase de la que heredan todas las reglas")
                                End If
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Next
                End If
            Next

            For Each Cat As KeyValuePair(Of String, Int32) In subcategories
                selectedCategoryNode.Nodes.Add(Cat.Key, Cat.Key, Cat.Value)
            Next
            selectedCategoryNode.Expand()

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            If Not IsNothing(subcategories) Then
                subcategories.Clear()
                subcategories = Nothing
            End If
        End Try
    End Sub
    Private Sub LoadRulesByCategory(ByVal selectedCategoryNode As TreeNode)
        Try
            FillRulesDescriptions(selectedCategoryNode.Text)
            LoadRuleTreeView()
            selectedCategoryNode.Expand()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub FillRulesDescriptions(ByVal nodeText As String)
        Dim mList As New List(Of System.Type)
        Dim description As String = String.Empty
        Dim help As String = String.Empty
        Dim mainCategory As String = String.Empty
        Dim subCategory As String = String.Empty
        Dim category As String = String.Empty
        Dim ruleName As String = String.Empty
        Dim iconId As Int32 = 31
        Dim className As String
        rulesDescriptions.Clear()

        For Each M As System.Type In a.GetTypes
            className = M.Name.ToLower
            If (className.StartsWith("if") OrElse className.StartsWith("do")) And String.Compare(className, "ifbranch") <> 0 Then
                iconId = 31
                mainCategory = String.Empty
                subCategory = String.Empty
                category = String.Empty
                ruleName = String.Empty

                For Each o As Object In M.GetCustomAttributes(True)
                    Try
                        If filterUIRules Then
                            If String.Compare(o.GetType().Name.ToLower, "RuleFeatures".ToLower) = 0 Then
                                If Not DirectCast(o, RuleFeatures).IsUI Then
                                    mList.Add(M)
                                End If
                            End If
                        Else
                            If String.Compare(o.GetType().Name, "RuleDescription") = 0 Then
                                If Not String.Compare(DirectCast(o, RuleDescription).Description, "Regla Padre") = 0 Then
                                    description = DirectCast(o, RuleDescription).Description
                                    ruleName = M.Name
                                End If
                            ElseIf String.Compare(o.GetType().Name, "RuleHelp") = 0 Then
                                If Not String.Compare(DirectCast(o, RuleHelp).Help, "Clase de la que heredan todas las reglas") = 0 Then
                                    help = DirectCast(o, RuleHelp).Help
                                Else
                                    MessageBox.Show("Clase de la que heredan todas las reglas")
                                End If
                            ElseIf String.Compare(o.GetType().Name, "RuleCategory") = 0 Then
                                If Not String.Compare(DirectCast(o, RuleCategory).Category, "Clase de la que heredan todas las reglas") = 0 Then
                                    category = DirectCast(o, RuleCategory).Category
                                Else
                                    MessageBox.Show("Clase de la que heredan todas las reglas")
                                End If
                            ElseIf String.Compare(o.GetType().Name, "RuleMainCategory") = 0 Then
                                If Not String.Compare(DirectCast(o, RuleMainCategory).MainCategory, "Clase de la que heredan todas las reglas") = 0 Then
                                    mainCategory = DirectCast(o, RuleMainCategory).MainCategory
                                Else
                                    MessageBox.Show("Clase de la que heredan todas las reglas")
                                End If
                            ElseIf String.Compare(o.GetType().Name, "RuleSubCategory") = 0 Then
                                If Not String.Compare(DirectCast(o, RuleSubCategory).SubCategory, "Clase de la que heredan todas las reglas") = 0 Then
                                    subCategory = DirectCast(o, RuleSubCategory).SubCategory
                                Else
                                    MessageBox.Show("Clase de la que heredan todas las reglas")
                                End If
                            ElseIf String.Compare(o.GetType().Name, "RuleIconId") = 0 Then
                                If Not String.Compare(DirectCast(o, RuleIconId).IconId, "Clase de la que heredan todas las reglas") = 0 Then
                                    iconId = DirectCast(o, RuleIconId).IconId
                                Else
                                    MessageBox.Show("Clase de la que heredan todas las reglas")
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Next
                If Not filterUIRules Then
                    If nodeText = "Todas" OrElse _
                        nodeText = mainCategory OrElse _
                        nodeText = category OrElse _
                        nodeText = subCategory Then
                        rulesDescriptions.Add(description, New RulesDescription(description, help, category, ruleName, iconId, mainCategory, subCategory))
                    End If
                End If
            End If
        Next
        For Each M As System.Type In mList
            iconId = 31
            className = M.Name.ToLower
            If className.StartsWith("if") OrElse className.StartsWith("do") Then
                For Each o As Object In M.GetCustomAttributes(True)
                    Try
                        If String.Compare(o.GetType().Name, "RuleDescription") = 0 Then
                            description = DirectCast(o, RuleDescription).Description
                            ruleName = M.Name
                        ElseIf String.Compare(o.GetType().Name, "RuleHelp") = 0 Then
                            help = DirectCast(o, RuleHelp).Help
                        ElseIf String.Compare(o.GetType().Name, "RuleMainCategory") = 0 Then
                            mainCategory = DirectCast(o, RuleCategory).Category
                        ElseIf String.Compare(o.GetType().Name, "RuleSubCategory") = 0 Then
                            subCategory = DirectCast(o, RuleCategory).Category
                        ElseIf String.Compare(o.GetType().Name, "RuleCategory") = 0 Then
                            category = DirectCast(o, RuleCategory).Category
                        ElseIf String.Compare(o.GetType().Name, "RuleIconId") = 0 Then
                            iconId = DirectCast(o, RuleIconId).IconId
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Next

                If nodeText = "Todas" OrElse _
                    nodeText = mainCategory OrElse _
                    nodeText = category OrElse _
                    nodeText = subCategory Then
                    rulesDescriptions.Add(description, New RulesDescription(description, help, category, ruleName, iconId, mainCategory, subCategory))
                End If
            End If
        Next
    End Sub
    Private Sub LoadRuleTreeView()
        tvwRules.Nodes.Clear()
        For Each RD As RulesDescription In rulesDescriptions.Values
            Dim N As New TreeNode(RD.Description, RD.IconId, RD.IconId)
            N.Tag = RD.RuleName
            N.ToolTipText = RD.Help
            tvwRules.Nodes.Add(N)
        Next
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Aceptar()
    End Sub
    Private Sub Aceptar()
        If String.IsNullOrEmpty(SelectedRuleName) OrElse String.IsNullOrEmpty(SelectedRuleClass) Then
            lblDescription.Text = "Para continuar primero deberá seleccionar una regla"
        Else
            Dim TypeofRule As TypesofRules
            Dim frmEvents As WFUserControl.frmchooseevent = Nothing
            Dim frmName As frmInputBox = Nothing
            Dim ruleName As String

            Try
                frmName = New frmInputBox("Ingrese el nombre de la regla", 2000, SelectedRuleName, "Nueva Regla")

                If frmName.ShowDialog() = DialogResult.OK Then
                    ruleName = frmName.Name.Replace(Chr(39), String.Empty)
                    TypeofRule = TypesofRules.Regla
                    NewRule = WFRulesBusiness.AddChild(SelectedRuleClass, parentRule, ruleName, TypeofRule)
                    DialogResult = DialogResult.OK
                    Close()
                End If

            Catch ex As Exception
                MessageBox.Show("Error al agregar una nueva regla", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ZClass.raiseerror(ex)
                NewRule = nothing
            Finally
                If frmEvents IsNot Nothing Then
                    frmEvents.Dispose()
                    frmEvents = Nothing
                End If
                If frmName IsNot Nothing Then
                    frmName.Dispose()
                    frmName = Nothing
                End If
            End Try
        End If
    End Sub
    Private Sub tvwRules_KeyUp(sender As Object, e As KeyEventArgs) Handles tvwRules.KeyUp
        If e.KeyData = Keys.Enter Then
            Aceptar()
        End If
    End Sub

    Private Sub RulesTree_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwRules.AfterSelect
        If tvwRules.SelectedNode IsNot Nothing Then
            SelectedRuleName = tvwRules.SelectedNode.Text
            SelectedRuleClass = tvwRules.SelectedNode.Tag.ToString
            lblDescription.Text = tvwRules.SelectedNode.ToolTipText
        End If
    End Sub
    Private Sub RulesTree_MouseHover(ByVal sender As Object, ByVal e As EventArgs) Handles tvwRules.MouseHover
        Try
            If tvwRules.SelectedNode IsNot Nothing Then
                lblDescription.Text = tvwRules.SelectedNode.ToolTipText
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvwCategories.AfterSelect
        LoadRulesByCategory(e.Node)
        If e.Node.Level = 0 AndAlso e.Node.Nodes.Count = 0 Then
            LoadCategories(e.Node)
        ElseIf e.Node.Level = 1 AndAlso e.Node.Nodes.Count = 0 Then
            LoadSubCategories(e.Node)
        End If
    End Sub
#End Region

#Region "Búsqueda de reglas"
    Private Sub txtSearchRule_KeyUp(sender As Object, e As KeyEventArgs)
        FilterRules(txtSearch.Text)
    End Sub

    Private Sub FilterRules(ByVal value As String)
        rulesDescriptions.Clear()

        For Each rd As RulesDescription In rulesSearch
            If rd.ToString.ToLower.Contains(value.ToLower) Then
                rulesDescriptions.Add(rd.Description, _
                                      New RulesDescription(rd.Description, rd.Help, rd.Category, rd.RuleName, rd.IconId, rd.MainCategory, rd.SubCategory))
            End If
        Next

        LoadRuleTreeView()
    End Sub
#End Region

End Class