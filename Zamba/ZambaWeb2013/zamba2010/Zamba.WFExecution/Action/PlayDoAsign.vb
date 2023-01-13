Imports Zamba.Core.WF.WF
Imports Zamba.Core
Imports Zamba.Membership

Public Class PlayDoAsign

    Private _myRule As IDoAsign
    Private userId As Int64
    Private userName As String
    Private AlteruserName As String
    Private value As Object
    Private ds As DataSet

    Dim UserGroupBusiness As New UserGroupBusiness

    Sub New(ByVal rule As IDoAsign)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim WFTB As New WFTaskBusiness
        Dim UB As New UserBusiness
        Try

            For Each r As ITaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, vbCrLf & "Ejecutando la regla para la tarea " & r.Name)

                Me.userId = 0
                Me.userName = String.Empty
                Me.value = Nothing
                ' El usuario actual asigna una tarea a un usuario de la etapa...
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Usuario alternativo: " & Me._myRule.AlternateUser)
                If Not String.IsNullOrEmpty(Me._myRule.AlternateUser.Trim) Then
                    'If Me._myRule.AlternateUser.Trim <> String.Empty Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Contenido de la variable: " & Me._myRule.AlternateUser)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo el usuario a asignar...")

                    Me.AlteruserName = Me._myRule.AlternateUser.Trim
                    Me.AlteruserName = TextoInteligente.ReconocerCodigo(Me.AlteruserName, r).Trim
                    Dim VarInterReglas As New VariablesInterReglas()
                    Me.value = VarInterReglas.ReconocerVariables(Me.AlteruserName).Trim
                    VarInterReglas = Nothing

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El usuario asignado será: " & Me.AlteruserName)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Contenido de variable antes: " & VariablesInterReglas.Item(Me._myRule.AlternateUser))

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
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Username asignado: ")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, Me.userName)
                        Me.userId = UserGroupBusiness.GetUserorGroupIdbyName(Me.userName)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Username luego de la asignación: ")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, Me.userName)
                    End If
                Else
                    Me.userId = Me._myRule.UserId
                End If
                'If Me.userName = String.Empty Then
                If String.IsNullOrEmpty(Me.userName) Then
                    Dim IsGroup As Boolean
                    Me.userName = UserGroupBusiness.GetUserorGroupNamebyId(Me.userId, IsGroup)
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando UserName: " & Me.userName & " UserId: " & Me.userId)
                WFTB.Asign(r, Me.userId, Zamba.Membership.MembershipHelper.CurrentUser.ID, Me.userName)
                UB.SaveAction(r.ID, Zamba.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, Me._myRule.Name)
            Next
        Finally
            UB = Nothing
            userId = Nothing
            userName = Nothing
            value = Nothing
            AlteruserName = Nothing
            WFTB = Nothing
        End Try
        Return results
    End Function
End Class
