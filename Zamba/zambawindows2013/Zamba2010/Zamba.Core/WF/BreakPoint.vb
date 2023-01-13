Imports System.Collections.Generic
Imports Zamba.Membership

Public Class BreakPointsUtil

    Public Shared Property BreakPoints As New List(Of BreakPoint)

    Public Shared Function CheckBreakPointOnRule(RuleId)
        Try
            For Each br As BreakPoint In BreakPoints
                If br.RuleID = RuleId Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Public Shared Sub SetContinueBreakPointState(currentRuleId As Long, state As Boolean)
        Try
            For Each br As BreakPoint In BreakPoints
                If br.RuleID = currentRuleId Then
                    br.ContinueState = state
                End If
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Function BreakPointContinue(currentRuleId As Long) As Boolean
        Try
            For Each br As BreakPoint In BreakPoints
                If br.RuleID = currentRuleId Then
                    Return br.ContinueState
                End If
            Next
            Return False
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function
End Class

Public Class BreakPoint
    Public ID As Long
    Public RuleID As Long
    Public UserID As Long
    Public Conditions As Object
    Public ContinueState As Boolean
End Class