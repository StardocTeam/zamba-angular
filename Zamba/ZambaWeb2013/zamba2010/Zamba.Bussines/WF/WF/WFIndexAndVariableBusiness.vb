Imports System.Collections.Generic
Imports Zamba.Data

Public Class WFIndexAndVariableBusiness
    Public Function GetIndexAndVariable() As List(Of IndexAndVariable)
        Dim ds As DataSet = WFIndexAndVariableFactory.GetIndexAndVariable()
        Dim indexsAndVariables As New List(Of IndexAndVariable)

        For Each dr As DataRow In ds.Tables(0).Rows
            indexsAndVariables.Add(New IndexAndVariable(dr("ID"), dr("name"), dr("type")))
        Next

        Return indexsAndVariables
    End Function

    Public Function GetIndexAndVariableNotInRule(ByVal ruleID As Int64) As List(Of IndexAndVariable)
        Dim ds As DataSet = WFIndexAndVariableFactory.GetIndexAndVariableNotInRule(ruleID)
        Dim indexsAndVariables As New List(Of IndexAndVariable)

        For Each dr As DataRow In ds.Tables(0).Rows
            indexsAndVariables.Add(New IndexAndVariable(dr("ID"), dr("name"), dr("type")))
        Next

        Return indexsAndVariables
    End Function

    Public Function GetIndexAndVariableByRuleID(ByVal RuleID As Int64) As List(Of IndexAndVariable)
        Dim ds As DataSet = WFIndexAndVariableFactory.GetIndexAndVariableByRuleID(RuleID)
        Dim indexsAndVariables As New List(Of IndexAndVariable)

        For Each dr As DataRow In ds.Tables(0).Rows
            indexsAndVariables.Add(New IndexAndVariable(dr("ID"), dr("name"), dr("type")))
        Next

        Return indexsAndVariables
    End Function

    Public Shared Sub InsertIndexAndVariableByRuleID(ByVal ID As Int64, ByVal ruleID As Int64)
        WFIndexAndVariableFactory.InsertIndexAndVariableByRuleID(ID, ruleID)
    End Sub

    Public Function GetIndexAndVariableConfiguration(ByVal IndexAndVariable As Int64) As List(Of IndexAndVariableConfiguration)
        Dim ds As DataSet = WFIndexAndVariableFactory.GetIndexAndVariableConfiguration(IndexAndVariable)
        Dim indexsAndVariablesConfig As New List(Of IndexAndVariableConfiguration)

        For Each dr As DataRow In ds.Tables(0).Rows
            If dr("Manual") = "S" Then
                indexsAndVariablesConfig.Add(New IndexAndVariableConfiguration(dr("ID"), dr("Manual"), dr("name"), dr("operator"), dr("value"), dr("name")))
            Else
                indexsAndVariablesConfig.Add(New IndexAndVariableConfiguration(dr("ID"), dr("Manual"), dr("name"), dr("operator"), dr("value"), IndexsBusiness.GetIndexName(dr("name"))))
            End If
        Next

        Return indexsAndVariablesConfig
    End Function

    Public Sub DeleteIndexAndVariable(ByVal IndexAndVariable As Int64)
        WFIndexAndVariableFactory.DeleteIndexAndVariable(IndexAndVariable)
    End Sub

    Public Sub DeleteIndexAndVariableSelection(ByVal IndexAndVariable As Int64, ByVal ruleID As Int64)
        WFIndexAndVariableFactory.DeleteIndexAndVariableSelection(IndexAndVariable, ruleID)
    End Sub

    Public Sub SaveIndexAndVariable(ByVal ID As Int64, ByVal Name As String, ByVal operador As String, ByVal ConfigurationList As List(Of IndexAndVariableConfiguration))
        If ID > 0 Then
            WFIndexAndVariableFactory.DeleteIndexAndVariableConfiguration(ID)

            WFIndexAndVariableFactory.UpdateIndexAndVariable(ID, operador)
        Else
            ID = CoreBusiness.GetNewID(IdTypes.IndexAndVariable)

            WFIndexAndVariableFactory.InsertIndexAndVariable(ID, Name, operador)
        End If

        'Agregar nueva configuracion
        For Each config As IndexAndVariableConfiguration In ConfigurationList
            config.ID = ID
            WFIndexAndVariableFactory.InsertIndexAndVariableConfig(config.ID, config.Manual, config.Name, config.Operador, config.Value)
        Next
    End Sub
End Class
