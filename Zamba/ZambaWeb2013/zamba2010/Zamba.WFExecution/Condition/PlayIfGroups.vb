Imports Zamba.Core
''' <summary>
''' La parte de ejecucion de la regla IFGROUPS que se utiliza para validaciones del tipo usuario - grupo en workflow
''' </summary>
''' <remarks></remarks>
Public Class PlayIfGroups
    ''' <summary>
    ''' Ejecuta la regla, esta sobrecarga es utilizada para versiones anteriores de zamba donde no se incluian las llamadas reglas IfBranch
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myrule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfGroups) As System.Collections.Generic.List(Of Core.ITaskResult)
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
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfGroups, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim Tasks As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim UGB As New UserGroupBusiness
        Dim GroupList As List(Of Int64) = myrule.GroupList
        Dim comparator As Comparators = myrule.Comparator
        Dim taskadded As Boolean

        For Each taskResult As TaskResult In results
            taskadded = False
            Select Case comparator
                Case Comparators.AssignedTo, Comparators.NotAsignedTo
                    For Each groupID As Int64 In GroupList
                        Dim users As List(Of Int64) = UGB.GetUsersIds(groupID)
                        If users.Contains(taskResult.AsignedToId) Then 'Si encontro al usuario asignado en la lista de usuarios validos lo agrego
                            taskadded = True
                            Exit For
                        End If

                        If taskadded Then Exit For
                    Next
                Case Comparators.CurrentUser, Comparators.NotCurrentUser
                    taskadded = False
                    Dim UserGroupList As List(Of Long) = UGB.GetGroupsAndInheritanceOfGroupsIds(Membership.MembershipHelper.CurrentUser.ID, True)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, $"Grupos a los que pertenece el usuario: {String.Join(",", UserGroupList)}")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, $"Grupos a comparar: {String.Join(",", GroupList)}")

                    For Each UserGroupID As Int64 In UserGroupList
                        If GroupList.Contains(UserGroupID) Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, $"El usuario con Grupo {UserGroupID} pertenece a los grupos buscados")
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
        UGB = Nothing
        Return Tasks
    End Function

    Public Enum Comparators
        AssignedTo = 0
        NotAsignedTo = 1
        CurrentUser = 2
        NotCurrentUser = 3
    End Enum
End Class
