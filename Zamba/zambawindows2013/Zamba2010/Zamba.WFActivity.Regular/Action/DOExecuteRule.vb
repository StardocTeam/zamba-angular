Imports Zamba.Core

<RuleMainCategory("Workflow"), RuleCategory("Reglas"), RuleSubCategory(""), RuleDescription("Ejecutar Regla"), RuleHelp("Permite seleccionar y ejecutar una regla en el Work Flow actual al momento de realizar la tarea"), RuleFeatures(False)> _
<Serializable()> Public Class DOExecuteRule
    Inherits WFRuleParent
    Implements IDOExecuteRule, IRuleValidate
    
    Private playRule As Zamba.WFExecution.PlayDOExecuteRule

    Public Property RuleID() As Int64 Implements IDOExecuteRule.RuleID
    Public Property IDRule() As String Implements IDOExecuteRule.IDRule
    Public Property Mode() As Boolean Implements IDOExecuteRule.Mode

#Region "WFRuleParent, IRuleValidate"
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
    Public Property IsValid As Boolean Implements IRuleValidate.isValid

    Public Overrides ReadOnly Property MaskName As String
        Get
              Return String.Empty
        End Get
    End Property

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub
#End Region

    Public Sub New(ByVal Id As Int64, _
                   ByVal Name As String, _
                   ByVal wfstepid As Int64, _
                   ByVal ruleId As String, _
                   ByVal IsRemote As Boolean, _
                   ByVal IDRule As String, _
                   ByVal Mode As Boolean)

        MyBase.New(Id, Name, wfstepid)
        If String.IsNullOrEmpty(ruleId) Then
            Me.RuleID = 0
        Else
            Me.RuleID = Int64.Parse(ruleId)
        End If


        playRule = New Zamba.WFExecution.PlayDOExecuteRule(Me)
        Me.IDRule = IDRule
        Me.Mode = Mode

    End Sub

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results, refreshTasks)
    End Function
    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function
    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
End Class