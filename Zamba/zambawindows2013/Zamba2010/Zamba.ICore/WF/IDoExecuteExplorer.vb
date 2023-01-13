Public Interface IDoExecuteExplorer
    Inherits IRule
    Property RuleID() As Int32
    Property EvaluateRuleID() As Int32
    Property Height() As Int16
    Property Width() As Int16
    Property Route() As String
    Property BrowserStatus() As Boolean
    Property ContinueWithRule() As Boolean
    Property Operador() As Comparadores
    Property Variable() As String
    Property Valor() As String
    Property Habilitar() As Boolean
    Property HabilitarMensaje() As Boolean
    Property ExecuteElseID() As Int32
    Property HorizontalVisualization() As Boolean
    Property OpenNewWindowBrowser() As Boolean
End Interface