Imports System.Windows.Forms
Imports Zamba.Data
Public Class PlayDoAsignToGroup
    Private _myRule As IDoAsignToGroup
    Private zValue As String
    Private zvar As String
    Private id As Long
    Private name As String

    Dim UserBusiness As New UserBusiness
    Dim UserGroupBusiness As New UserGroupBusiness

    Sub New(ByVal rule As IDoAsignToGroup)
        Me._myRule = rule
    End Sub
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Dim idGroup As Int64

            Me.zValue = String.Empty
            Me.zvar = String.Empty
            For Each R As TaskResult In results

                getValueVar(R)


                If Me.id = 0 And Me.name = String.Empty Then
                    MessageBox.Show("La variable no existe.")
                    Exit Function
                ElseIf Me.id = 0 Then
                    Me.id = UserBusiness.GetUserByname(Me.name, True).ID
                End If

                Dim iuser As IUser = UserBusiness.GetUserById(Me.id)

                If Not Int64.TryParse(Me._myRule.grupo, idGroup) Then
                    idGroup = UserGroupBusiness.GetGroupIdByName(Me._myRule.grupo)
                End If

                Dim group As UserGroup = UserGroupBusiness.GetUserGroup(idGroup)


                If Not IsNothing(group) And Not IsNothing(iuser) Then
                    UserGroupBusiness.AssignUser(iuser, group)
                End If

            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return results

    End Function

    Private Sub getValueVar(R As TaskResult)
        Try
            If Me._myRule.usuario.IndexOf("zvar(", StringComparison.CurrentCultureIgnoreCase) <> -1 Then
                Me.zvar = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.usuario)
                Me.zvar = TextoInteligente.ReconocerCodigo(Me.zvar, R)
                If (Me.zvar.IndexOf("zvar(", StringComparison.CurrentCultureIgnoreCase) <> -1) Then
                    Me.zvar = Me.zvar.Replace("zvar(", String.Empty)
                    If (Me.zvar.IndexOf("))", StringComparison.CurrentCultureIgnoreCase) <> -1) Then
                        Me.zvar = Me.zvar.Replace("))", ")").Trim()
                    Else
                        Me.zvar = Me.zvar.Replace(")", String.Empty).Trim()
                    End If
                End If
            Else
                Me.zvar = Me._myRule.usuario
            End If

            Me.zvar = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.zvar, R)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable original: " & Me.zvar & " Variable pasada por textointeligente: " & Me.zvar)
            Me.zvar = Me.zvar.Trim()

            If VariablesInterReglas.ContainsKey(Me.zvar) Then
                If (TypeOf (VariablesInterReglas.Item(Me.zvar)) Is Long) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable Long, ID usuario")
                    Me.id = VariablesInterReglas.Item(Me.zvar)
                ElseIf (TypeOf (VariablesInterReglas.Item(Me.zvar)) Is String) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable string, nombre del usuario")
                    Me.name = VariablesInterReglas.Item(Me.zvar)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable de otro tipo")
                    Me.name = String.Empty
                    Me.id = 0
                End If
            ElseIf Not Int64.TryParse(Me.zvar, Me.id) Then
                Me.name = String.Empty
                Me.id = 0
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
