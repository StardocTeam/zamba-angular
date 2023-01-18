''' <summary>
''' La parte de ejecucion de la regla IFGROUPS que se utiliza para validaciones del tipo usuario - grupo en workflow
''' </summary>
''' <remarks></remarks>
Public Class PlayIfGroups

    Private myRule As IIfGroups
    ''' <summary>
    ''' Ejecuta la regla, esta sobrecarga es utilizada para versiones anteriores de zamba donde no se incluian las llamadas reglas IfBranch
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myrule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    ''' <summary>
    ''' Ejecuta la regla, esta sobrecarga es utilizada para versiones nuevas de zamba. Utilizando las reglas IfBranch
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myrule"></param>
    ''' <param name="ifType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim Tasks As New System.Collections.Generic.List(Of Core.ITaskResult)

        Dim GroupList As List(Of Int64) = myrule.GroupList
        Dim comparator As Comparators = myrule.Comparator
        Dim taskadded As Boolean

        For Each taskResult As TaskResult In results
            taskadded = False
            Select Case comparator
                Case Comparators.AssignedTo, Comparators.NotAsignedTo
                    For Each groupID As Int64 In GroupList
                        Dim users As List(Of Int64) = UserGroupBusiness.GetUsersIds(groupID, True)
                        If users.Contains(taskResult.AsignedToId) Then 'Si encontro al usuario asignado en la lista de usuarios validos lo agrego
                            taskadded = True
                            Exit For
                        End If

                        If taskadded Then Exit For
                    Next
                Case Comparators.CurrentUser, Comparators.NotCurrentUser
                    taskadded = False
                    For Each UserGroup As UserGroup In Membership.MembershipHelper.CurrentUser.Groups
                        If GroupList.Contains(UserGroup.ID) Then
                            taskadded = True
                        End If

                        If taskadded Then Exit For
                    Next
            End Select

            Select Case comparator
                Case Comparators.AssignedTo
                    If taskadded = ifType Then Tasks.Add(taskResult)
                Case Comparators.NotAsignedTo
                    If Not taskadded = ifType Then Tasks.Add(taskResult)
                Case Comparators.CurrentUser
                    If taskadded = ifType Then Tasks.Add(taskResult)
                Case Comparators.NotCurrentUser
                    If Not taskadded = ifType Then Tasks.Add(taskResult)
            End Select
        Next
        Return Tasks
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Enum Comparators
        AssignedTo = 0
        NotAsignedTo = 1
        CurrentUser = 2
        NotCurrentUser = 3
    End Enum

    Public Sub New(ByVal rule As IIfGroups)
        myRule = rule
    End Sub
End Class
