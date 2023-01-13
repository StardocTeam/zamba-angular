Imports Zamba.Core

Public Class PlayDoReplaceTextInWord

    Private _myRule As IDoReplaceTextInWord


    Sub New(ByVal rule As IDoReplaceTextInWord)
        Me._myRule = rule
    End Sub

    ''' <summary>
    ''' Play de la Regla DoReplaceText
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <history>
    ''' </history>
    Public Function Play(ByVal results As List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
