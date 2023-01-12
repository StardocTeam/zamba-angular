Imports Zamba.Core

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Web"), RuleDescription("Ejecutar ruta web"), RuleHelp("Permite Abrir una ruta web especificada"), RuleFeatures(True)> <Serializable()> _
Public Class DoExecuteExplorer
    Inherits WFRuleParent
    Implements IDoExecuteExplorer, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoExecuteExplorer
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
    Private _RuleId As Int64
    Private _route As String
    Private _browserStatus As Boolean
    Private _height As Int16
    Private _width As Int16
    Private _continueWithRule As Boolean
    Private _operador As Comparadores
    Private _variable As String
    Private _valor As String
    Private _habilitar As Boolean
    Private _evaluateRuleID As Int64
    Private _habilitarMensaje As Boolean
    Private _executeElseID As Int64
    Private _horizontalVisualization As Boolean
    Private _openNewWindowBrowser As Boolean

    Public Sub New(ByVal id As Int64, ByVal name As String, ByVal wfStepId As Int64, ByVal Route As String, ByVal BrowserStatus As Boolean, ByVal RuleID As Int32, _
                   ByVal Height As Int16, ByVal Width As Int16, ByVal ContinueWithRule As Boolean, ByVal Operador As Comparadores, ByVal Variable As String, _
                   ByVal Valor As String, ByVal Habilitar As Boolean, ByVal EvaluateRuleID As Int64, ByVal HabilitarMensaje As Boolean, ByVal ExecuteElseID As Int32, ByVal HorizontalVisualization As Boolean, ByVal OpenNewWindowBrowser As Boolean)
        MyBase.New(id, name, wfStepId)
        _route = Route
        _RuleId = RuleID
        _height = Height
        _width = Width
        _browserStatus = BrowserStatus
        _continueWithRule = ContinueWithRule
        _operador = Operador
        _variable = Variable
        _valor = Valor
        _habilitar = Habilitar
        _habilitarMensaje = HabilitarMensaje
        _executeElseID = ExecuteElseID
        _evaluateRuleID = EvaluateRuleID
        _horizontalVisualization = HorizontalVisualization
        _openNewWindowBrowser = OpenNewWindowBrowser
        playRule = New WFExecution.PlayDoExecuteExplorer(Me)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Property Route() As String Implements Core.IDoExecuteExplorer.Route
        Get
            Return _route
        End Get
        Set(ByVal value As String)
            _route = value
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
            Return _browserStatus
        End Get
        Set(ByVal value As Boolean)
            _browserStatus = value
        End Set
    End Property
    Public Property Height() As Int16 Implements Core.IDoExecuteExplorer.Height
        Get
            Return _height
        End Get
        Set(ByVal value As Int16)
            _height = value
        End Set
    End Property
    Public Property Width() As Int16 Implements Core.IDoExecuteExplorer.Width
        Get
            Return _width
        End Get
        Set(ByVal value As Int16)
            _width = value
        End Set
    End Property
    Public Property ContinueWithRule() As Boolean Implements Core.IDoExecuteExplorer.ContinueWithRule
        Get
            Return _continueWithRule
        End Get
        Set(ByVal value As Boolean)
            _continueWithRule = value
        End Set
    End Property
    Public Property Operador() As Core.Comparadores Implements Core.IDoExecuteExplorer.Operador
        Get
            Return _operador
        End Get

        Set(ByVal value As Core.Comparadores)
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

Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
              Return String.Empty
        End Get
    End Property
End Class
