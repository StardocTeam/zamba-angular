Public Interface IWFRuleParent
    Inherits IZambaCore, IRule, IRuleTest
    Property OldStateEnable() As Boolean   
    Property OldStateTrue() As Boolean
    Property IsUI() As Boolean
    ReadOnly Property MaskName As String
    Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As System.Collections.Generic.List(Of ITaskResult)
    Function ExecuteRule(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal ruleBusiness As IWFTaskBusiness, ByRef refreshTasks As List(Of Int64)) As System.Collections.Generic.List(Of ITaskResult)

End Interface