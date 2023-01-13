Imports Zamba.Core.Enumerators
Imports System.Workflow.ComponentModel

Public Interface IResultActivity
    Property ruleId() As Int64
    Property Results() As System.Collections.Generic.List(Of ITaskResult)
    'Function GetParams() As System.Collections.Generic.List(Of Object)
    Event OnItemListChanging As ItemListChanging
    'Shadows Property MaskName() As String
    Property RuleType() As TypesofRules
    Property WFStepId() As Int64
    'Property RuleType() As TypesofRules
    Property Name() As String
    Property RuleClass() As String
End Interface

Public Delegate Sub ItemListChanging(ByVal e As ActivityCollectionChangeEventArgs)