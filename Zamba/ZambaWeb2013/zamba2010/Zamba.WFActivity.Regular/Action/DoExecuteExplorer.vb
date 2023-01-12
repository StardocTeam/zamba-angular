Imports Zamba.Core

<RuleCategory("Aplicaciones"), RuleDescription("Ejecutar ruta web"), RuleHelp("Permite Abrir una ruta web especificada"), RuleFeatures(True)> <Serializable()> _
Public Class DoExecuteExplorer
    Inherits WFRuleParent
    Implements IDoExecuteExplorer
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub

    Private m_SQLSelectId As Int32
    Private _RuleId As Int32
    Private _route As String
    Private _browserStatus As Boolean
    Private _height As Int16
    Private _width As Int16
    Private _continueWithRule As Boolean
    Private _operador As Comparadores
    Private _variable As String
    Private _valor As String
    Private _habilitar As Boolean
    Private _evaluateRuleID As Int32
    Private _habilitarMensaje As Boolean
    Private _executeElseID As Int32
    Private _horizontalVisualization As Boolean
    Private _openNewWindowBrowser As Boolean

    Public Sub New(ByVal id As Int64, ByVal name As String, ByVal wfStepId As Int64, ByVal Route As String, ByVal BrowserStatus As Boolean, ByVal RuleID As Int32, _
                   ByVal Height As Int16, ByVal Width As Int16, ByVal ContinueWithRule As Boolean, ByVal Operador As Comparadores, ByVal Variable As String, _
                   ByVal Valor As String, ByVal Habilitar As Boolean, ByVal EvaluateRuleID As Int32, ByVal HabilitarMensaje As Boolean, ByVal ExecuteElseID As Int32, ByVal HorizontalVisualization As Boolean, ByVal OpenNewWindowBrowser As Boolean)
        MyBase.New(id, name, wfStepId)
        Me._route = Route
        Me._RuleId = RuleID
        Me._height = Height
        Me._width = Width
        Me._browserStatus = BrowserStatus
        Me._continueWithRule = ContinueWithRule
        Me._operador = Operador
        Me._variable = Variable
        Me._valor = Valor
        Me._habilitar = Habilitar
        Me._habilitarMensaje = HabilitarMensaje
        Me._executeElseID = ExecuteElseID
        Me._evaluateRuleID = EvaluateRuleID
        Me._horizontalVisualization = HorizontalVisualization
        Me._openNewWindowBrowser = OpenNewWindowBrowser
    End Sub

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoExecuteExplorer()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoExecuteExplorer()
        Return playRule.Play(results, Me)
    End Function


    Public Property Route() As String Implements Core.IDoExecuteExplorer.Route
        Get
            Return Me._route
        End Get
        Set(ByVal value As String)
            Me._route = value
        End Set
    End Property
    Public Property RuleID() As Int32 Implements Core.IDoExecuteExplorer.RuleID
        Get
            Return _RuleId
        End Get
        Set(ByVal value As Int32)
            _RuleId = value
        End Set
    End Property
    Public Property BrowserStatus() As Boolean Implements Core.IDoExecuteExplorer.BrowserStatus
        Get
            Return Me._browserStatus
        End Get
        Set(ByVal value As Boolean)
            Me._browserStatus = value
        End Set
    End Property
    Public Property Height() As Int16 Implements Core.IDoExecuteExplorer.Height
        Get
            Return Me._height
        End Get
        Set(ByVal value As Int16)
            Me._height = value
        End Set
    End Property
    Public Property Width() As Int16 Implements Core.IDoExecuteExplorer.Width
        Get
            Return Me._width
        End Get
        Set(ByVal value As Int16)
            Me._width = value
        End Set
    End Property
    Public Property ContinueWithRule() As Boolean Implements Core.IDoExecuteExplorer.ContinueWithRule
        Get
            Return Me._continueWithRule
        End Get
        Set(ByVal value As Boolean)
            Me._continueWithRule = value
        End Set
    End Property
    Public Property Operador() As Core.Comparadores Implements Core.IDoExecuteExplorer.Operador
        Get
            Return _operador
        End Get

        Set(ByVal value As Comparadores)
            _operador = value
        End Set
    End Property
    Public Property Variable() As String Implements Core.IDoExecuteExplorer.Variable
        Get
            Return _variable
        End Get

        Set(ByVal value As String)
            _variable = value
        End Set
    End Property
    Public Property Valor() As String Implements Core.IDoExecuteExplorer.Valor
        Get
            Return _valor
        End Get

        Set(ByVal value As String)
            _valor = value
        End Set
    End Property
    Public Property Habilitar() As Boolean Implements Core.IDoExecuteExplorer.Habilitar
        Get
            Return _habilitar
        End Get
        Set(ByVal value As Boolean)
            _habilitar = value
        End Set
    End Property
    Public Property HabilitarMensaje() As Boolean Implements Core.IDoExecuteExplorer.HabilitarMensaje
        Get
            Return _habilitarMensaje
        End Get
        Set(ByVal value As Boolean)
            _habilitarMensaje = value
        End Set
    End Property
    Public Property ExecuteElseID() As Int32 Implements Core.IDoExecuteExplorer.ExecuteElseID
        Get
            Return _executeElseID
        End Get
        Set(ByVal value As Int32)
            _executeElseID = value
        End Set
    End Property
    Public Property EvaluateRuleID() As Int32 Implements Core.IDoExecuteExplorer.EvaluateRuleID
        Get
            Return _evaluateRuleID
        End Get
        Set(ByVal value As Int32)
            _evaluateRuleID = value
        End Set
    End Property
    Public Property HorizontalVisualization() As Boolean Implements Core.IDoExecuteExplorer.HorizontalVisualization
        Get
            Return _horizontalVisualization
        End Get
        Set(ByVal value As Boolean)
            _horizontalVisualization = value
        End Set
    End Property
    Public Property OpenNewWindowBrowser() As Boolean Implements Core.IDoExecuteExplorer.OpenNewWindowBrowser
        Get
            Return _openNewWindowBrowser
        End Get
        Set(ByVal value As Boolean)
            _openNewWindowBrowser = value
        End Set
    End Property

End Class
