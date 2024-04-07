Imports Zamba.Core.WF.WF
Imports Zamba.Core
Public Class PlayDoAsign

    Private _myRule As IDoAsign
    Private userId As Int64
    Private userName As String
    Private AlteruserName As String
    Private value As Object
    Private ds As DataSet

    Sub New(ByVal rule As IDoAsign)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Try

            For Each r As ITaskResult In results
               
                Me.userId = 0
                Me.userName = String.Empty
                Me.value = Nothing
                ' El usuario actual asigna una tarea a un usuario de la etapa...
                If Not String.IsNullOrEmpty(Me._myRule.AlternateUser.Trim) Then
                    'If Me._myRule.AlternateUser.Trim <> String.Empty Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "Contenido de la variable: " & Me._myRule.AlternateUser)

                    Me.AlteruserName = Me._myRule.AlternateUser.Trim
                    Me.AlteruserName = TextoInteligente.ReconocerCodigo(Me.AlteruserName, r).Trim
                    Me.value = WFRuleParent.ReconocerVariables(Me.AlteruserName).Trim


                    If IsNumeric(Me.value) Then
                        Me.userId = Int32.Parse(Me.value)
                    ElseIf (TypeOf (Me.value) Is DataSet) Then
                        Me.ds = Me.value
                        'faltaria que en la configuracion se pudiera elegir la columna a tomar, hoy esta fijo en la columna 2 indice 1
                        Me.userId = Int32.Parse(Me.ds.Tables(0).Rows(0)(1).ToString())
                    Else
                        Me.userName = Me.value
                        If (Me.userName = String.Empty) Then
                            Me.userName = Me.AlteruserName
                        End If
                        Me.userId = UserGroupBusiness.GetUserorGroupIdbyName(Me.userName)

                    End If
                Else
                    Me.userId = Me._myRule.UserId
                End If
                'If Me.userName = String.Empty Then
                If String.IsNullOrEmpty(Me.userName) Then
                    Me.userName = UserGroupBusiness.GetUserorGroupNamebyId(Me.userId)
                End If
                Trace.WriteLineIf(ZTrace.IsInfo, "UserName: " & Me.userName & " UserId: " & Me.userId)

                WFTaskBusiness.Asign(r, Me.userId, Membership.MembershipHelper.CurrentUser.ID, Me.userName)
                Trace.WriteLineIf(ZTrace.IsInfo, "La asignaci�n se ha aplicado con �xito!")
                UserBusiness.Rights.SaveAction(r.ID, Zamba.Core.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, Me._myRule.Name)
            Next
        Finally

            userId = Nothing
            userName = Nothing
            value = Nothing
            AlteruserName = Nothing
        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Public Function DiscoverParams() As List(Of String)

    End Function
End Class