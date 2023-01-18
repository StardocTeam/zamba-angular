Imports System.Reflection

''' <summary>
'''      Formulario Utilizado para Agregar Reglas al WorkFlow.
'''      Las reglas se habilitan asigando el valor "true" a la propiedad
'''      "Enable" del option box correspondiente y deben registrarse en 
'''      el enumerador de Acciones de Reglas WFRuleParent
''' </summary>
''' <remarks></remarks>
Public Class UCRules
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCRules))
        Me.Panel1 = New Zamba.AppBlock.ZPanel()
        Me.Label4 = New Zamba.AppBlock.ZLabel()
        Me.btnAceptar = New Zamba.AppBlock.ZButton()
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.RulesTree = New System.Windows.Forms.TreeView()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.Panel1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(22, 48)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(720, 11)
        Me.Panel1.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.FontSize = 14.25!
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(20, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(427, 27)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Validaciones y Acciones"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.ForeColor = System.Drawing.Color.White
        Me.btnAceptar.Location = New System.Drawing.Point(371, 462)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(144, 33)
        Me.btnAceptar.TabIndex = 14
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.FontSize = 12.0!
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(20, 440)
        Me.Label1.MaximumSize = New System.Drawing.Size(516, 0)
        Me.Label1.MinimumSize = New System.Drawing.Size(0, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(173, 19)
        Me.Label1.TabIndex = 81
        Me.Label1.Text = "Descripción de la Regla"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RulesTree
        '
        Me.RulesTree.BackColor = System.Drawing.Color.White
        Me.RulesTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RulesTree.HideSelection = False
        Me.RulesTree.HotTracking = True
        Me.RulesTree.Location = New System.Drawing.Point(25, 67)
        Me.RulesTree.Name = "RulesTree"
        Me.RulesTree.ShowNodeToolTips = True
        Me.RulesTree.Size = New System.Drawing.Size(717, 370)
        Me.RulesTree.TabIndex = 82
        '
        'UCRules
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 16)
        Me.ClientSize = New System.Drawing.Size(552, 500)
        Me.Controls.Add(Me.RulesTree)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UCRules"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Convertir regla"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Public Sub New(ByVal showDoOrIfRules As Boolean)
        InitializeComponent()
        _showDoOrIfRules = showDoOrIfRules
    End Sub

#Region "Atributos"
    Dim RulesDescriptions As New Generic.List(Of RulesDescription)
    Dim _showDoOrIfRules As Boolean
    Private _ClassName As String
    Public ReadOnly Property ClassName() As String
        Get
            Return _ClassName
        End Get
    End Property
#End Region
    Private Class RulesDescription
        Public Sub New(ByVal Description As String, ByVal Help As String, ByVal Category As String, ByVal RuleName As String)
            Me.Description = Description
            Me.Help = Help
            Me.Category = Category
            Me.RuleName = RuleName
        End Sub
        Public Description As String
        Public Help As String
        Public Category As String
        Public RuleName As String
    End Class
#Region "Eventos"

    Private Sub AddNewRule_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            Dim ruleList As New List(Of Object)
            Dim a As Assembly = System.Reflection.Assembly.LoadFile(System.Windows.Forms.Application.StartupPath & "\Zamba.WFActivity.Regular.dll")

            Dim Description As String = String.Empty
            Dim Help As String = String.Empty
            Dim Category As String = String.Empty
            Dim RuleName As String = String.Empty

            For Each M As System.Type In a.GetTypes
                If isRule(M.Name) Then
                    For Each o As Object In M.GetCustomAttributes(True)
                        Try
                            If String.Compare(o.GetType().Name, "RuleDescription") = 0 Then
                                If Not String.Compare(DirectCast(o, RuleDescription).Description, "Regla Padre") = 0 Then
                                    Description = DirectCast(o, RuleDescription).Description
                                    RuleName = M.Name
                                End If
                            ElseIf String.Compare(o.GetType().Name, "RuleHelp") = 0 Then
                                If Not String.Compare(DirectCast(o, RuleHelp).Help, "Clase de la que heredan todas las reglas") = 0 Then
                                    Help = DirectCast(o, RuleHelp).Help
                                End If
                            ElseIf String.Compare(o.GetType().Name, "RuleCategory") = 0 Then
                                If Not String.Compare(DirectCast(o, RuleCategory).Category, "Clase de la que heredan todas las reglas") = 0 Then
                                    Category = DirectCast(o, RuleCategory).Category
                                End If
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Next
                    RulesDescriptions.Add(New RulesDescription(Description, Help, Category, RuleName))
                End If
            Next

            For Each RD As RulesDescription In RulesDescriptions
                If RulesTree.Nodes.ContainsKey(RD.Category) = False Then RulesTree.Nodes.Add(RD.Category, RD.Category)
                Dim N As New TreeNode(RD.Description)
                N.Tag = RD.RuleName
                N.ToolTipText = RD.Help
                RulesTree.Nodes.Item(RD.Category).Nodes.Add(N)
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Dependiendo del nombre, devuelvo si la agrego o no
    ''' </summary>
    ''' <param name="name">nombre del objeto</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function isRule(ByVal name As String) As Boolean
        If _showDoOrIfRules = False Then
            If name.ToLower.StartsWith("do") Then
                Return True
            End If
        ElseIf name.ToLower.StartsWith("if") And String.Compare(name.ToLower, "ifbranch") <> 0 Then
            Return True
        End If
        Return False
    End Function


    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Aceptar()
    End Sub

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
        Try
            If _ClassName Is Nothing OrElse _ClassName = String.Empty Then
                MessageBox.Show("Debe seleccionar una regla", "Creacion de Reglas")
            End If

            Close()
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
                _ClassName = RulesTree.SelectedNode.Tag.ToString
            Else
                _ClassName = String.Empty
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
End Class