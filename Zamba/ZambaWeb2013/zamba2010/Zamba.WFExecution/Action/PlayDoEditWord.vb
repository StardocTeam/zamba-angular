Imports System.IO

Public Class PlayDoEditWord

    Private _myRule As IDoEditWord

    Sub New(ByVal rule As IDoEditWord)
        _myRule = rule
    End Sub

    Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
        Return results
    End Function

    Public Function PlayWeb(results As List(Of ITaskResult),
                            ByRef rulePendingEvent As RulePendingEvents,
                            ByRef executionResult As RuleExecutionResult,
                            ByRef params As Hashtable) As List(Of ITaskResult)

        Dim wordPath As String = _myRule.WordPath

        'Busco en el word todas las variables a reemplazar
        Dim varsToReplace As New List(Of String)
        Dim st As New FileTools.SpireTools()

        varsToReplace = st.FindWordsInWord(wordPath, "zvar")

        rulePendingEvent = RulePendingEvents.ShowEditWord
        executionResult = RuleExecutionResult.PendingEventExecution
        params.Add("EditWordTemplatePath", wordPath)
        params.Add("VarsToReplace", varsToReplace)

        Return results
    End Function

    Public Function PlayTest() As Boolean
    End Function


    Function DiscoverParams() As List(Of String)
    End Function
End Class