Imports Zamba.Core.WF.WF
Public Class PlayDoAsign

    Private _myRule As IDoAsign
    Private userId As Int64
    Private userName As String
    Private AlteruserName As String
    Private value As Object
    Private ds As DataSet

    Sub New(ByVal rule As IDoAsign)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Try

            For Each r As ITaskResult In results

                userId = 0
                userName = String.Empty
                value = Nothing
                ' El usuario actual asigna una tarea a un usuario de la etapa...
                If Not String.IsNullOrEmpty(_myRule.AlternateUser.Trim) Then
                    'If Me._myRule.AlternateUser.Trim <> String.Empty Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Contenido de la variable: " & _myRule.AlternateUser)

                    AlteruserName = _myRule.AlternateUser.Trim
                    AlteruserName = TextoInteligente.ReconocerCodigo(AlteruserName, r).Trim
                    value = WFRuleParent.ReconocerVariables(AlteruserName).Trim


                    If IsNumeric(value) Then
                        userId = Int32.Parse(value)
                    ElseIf (TypeOf (value) Is DataSet) Then
                        ds = value
                        'faltaria que en la configuracion se pudiera elegir la columna a tomar, hoy esta fijo en la columna 2 indice 1
                        userId = Int32.Parse(ds.Tables(0).Rows(0)(1).ToString())
                    Else
                        userName = value
                        If (userName = String.Empty) Then
                            userName = AlteruserName
                        End If
                        userId = UserGroupBusiness.GetUserorGroupIdbyName(userName)

                    End If
                Else
                    userId = _myRule.UserId
                End If
                'If Me.userName = String.Empty Then
                If String.IsNullOrEmpty(userName) Then
                    userName = UserGroupBusiness.GetUserorGroupNamebyId(userId)
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "UserName: " & userName & " UserId: " & userId)

                WFTaskBusiness.Asign(r, userId, Membership.MembershipHelper.CurrentUser.ID, userName)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "La asignación se ha aplicado con éxito!")
                UserBusiness.Rights.SaveAction(r.ID, ObjectTypes.WFTask, RightsType.ExecuteRule, _myRule.Name)
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
