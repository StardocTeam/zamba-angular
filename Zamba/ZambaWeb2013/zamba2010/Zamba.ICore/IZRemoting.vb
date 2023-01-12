''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Interface	 : Core.IZRemoting
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Interfaz para la implementación de Remoting
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Interface IZRemoting
    Function Run(ByVal Action As String, ByVal Argument As String, ByVal obs As Hashtable) As Boolean
    Function IsRunning() As Boolean
    Sub Maximizar()

    Function ExecuteRule(ByVal RuleId As Long, ByRef lista As List(Of ITaskResult)) As Object
    Function ExecuteRule(ByVal ruleId As Int64, ByVal stepId As Int64, ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
End Interface
