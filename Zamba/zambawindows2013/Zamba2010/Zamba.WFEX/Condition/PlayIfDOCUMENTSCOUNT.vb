Imports Zamba.Core

Public Class PlayIfDocumentsCount

    Private myRule As IIfDOCUMENTSCOUNT

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim wfstep As IWFStep
        If results.Count > 0 Then
            wfstep = WFStepBusiness.GetStepById(results(0).StepId, False)
        End If
        Select Case myRule.Comparacion
            Case Comparadores.Igual
                For Each TR As TaskResult In results
                    If (wfstep.TasksCount = myRule.CantidadTareas) = ifType Then
                        Return results
                    Else
                        Return Nothing
                    End If
                Next
            Case Comparadores.Distinto
                For Each TR As TaskResult In results
                    If (wfstep.TasksCount <> myRule.CantidadTareas) = ifType Then
                        Return results
                    Else
                        Return Nothing
                    End If
                Next
            Case Comparadores.Mayor
                For Each TR As TaskResult In results
                    If (wfstep.TasksCount > myRule.CantidadTareas) = ifType Then
                        Return results
                    Else
                        Return Nothing
                    End If
                Next
            Case Comparadores.Menor
                For Each TR As TaskResult In results
                    If (wfstep.TasksCount < myRule.CantidadTareas) = ifType Then
                        Return results
                    Else
                        Return Nothing
                    End If
                Next
            Case Comparadores.IgualMayor
                For Each TR As TaskResult In results
                    If (wfstep.TasksCount >= myRule.CantidadTareas) = ifType Then
                        Return results
                    Else
                        Return Nothing
                    End If
                Next
            Case Comparadores.IgualMenor
                For Each TR As TaskResult In results
                    If (wfstep.TasksCount <= myRule.CantidadTareas) = ifType Then
                        Return results
                    Else
                        Return Nothing
                    End If
                Next
            Case Else
                If ifType = True Then
                    Return Nothing
                Else
                    Return results
                End If
        End Select
        Return Nothing
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IIfDOCUMENTSCOUNT)
        Me.myRule = rule
    End Sub
End Class
