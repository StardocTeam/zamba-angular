Imports System.Windows.Forms
Public Class PlayDoAsignToGroup
    Private _myRule As IDoAsignToGroup
    Private zVarGroup As String
    Private zvar As String
    Private id As Long
    Private name As String
    Private idGroup As Long
    Private nameGroup As String


    Sub New(ByVal rule As IDoAsignToGroup)
        _myRule = rule
    End Sub
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try

            zVarGroup = String.Empty
            zvar = String.Empty

            For Each R As TaskResult In results

                getValueUsr(R)

                getValueGroup(R)

                If id = 0 And name = String.Empty Then
                    MessageBox.Show("La variable no existe.")
                    Exit Function
                ElseIf id = 0 Then
                    id = UserBusiness.GetUserByname(name).ID
                End If

                Dim iuser As IUser = UserBusiness.GetUserById(id)

                If Not String.IsNullOrEmpty(nameGroup) Then
                    idGroup = UserGroupBusiness.GetGroupIdByName(nameGroup)
                End If

                Dim group As UserGroup = UserGroupBusiness.GetUserGroupAsUserGroup(idGroup)


                If Not IsNothing(group) And Not IsNothing(iuser) Then
                    UserGroupBusiness.AssignUser(iuser, group)
                End If

            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private Sub getValueGroup(R As TaskResult)
        Try
            If _myRule.grupo.IndexOf("zvar(", StringComparison.CurrentCultureIgnoreCase) <> -1 Then
                zVarGroup = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.grupo)
                zVarGroup = TextoInteligente.ReconocerCodigo(zVarGroup, R)
                If (zVarGroup.IndexOf("zvar(", StringComparison.CurrentCultureIgnoreCase) <> -1) Then
                    zVarGroup = zVarGroup.Replace("zvar(", String.Empty)
                    If (zVarGroup.IndexOf("))", StringComparison.CurrentCultureIgnoreCase) <> -1) Then
                        zVarGroup = zvar.Replace("))", ")").Trim()
                    Else
                        zVarGroup = zvar.Replace(")", String.Empty).Trim()
                    End If
                End If
            Else
                zVarGroup = _myRule.grupo
            End If

            zVarGroup = TextoInteligente.ReconocerCodigo(zVarGroup, R)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable original: " & zVarGroup & " Variable pasada por textointeligente: " & zVarGroup)
            zVarGroup = zVarGroup.Trim()

            If VariablesInterReglas.ContainsKey(zVarGroup) Then
                If (TypeOf (VariablesInterReglas.Item(zVarGroup)) Is Long) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable Long, ID usuario")
                    idGroup = VariablesInterReglas.Item(zVarGroup)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable de otro tipo")
                    nameGroup = zVarGroup
                    idGroup = 0
                End If
            ElseIf Not Int64.TryParse(zVarGroup, idGroup) Then
                nameGroup = zVarGroup
                idGroup = 0
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub getValueUsr(R As TaskResult)
        Try
            If _myRule.usuario.IndexOf("zvar(", StringComparison.CurrentCultureIgnoreCase) <> -1 Then
                zvar = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.usuario)
                zvar = TextoInteligente.ReconocerCodigo(zvar, R)
                If (zvar.IndexOf("zvar(", StringComparison.CurrentCultureIgnoreCase) <> -1) Then
                    zvar = zvar.Replace("zvar(", String.Empty)
                    If (zvar.IndexOf("))", StringComparison.CurrentCultureIgnoreCase) <> -1) Then
                        zvar = zvar.Replace("))", ")").Trim()
                    Else
                        zvar = zvar.Replace(")", String.Empty).Trim()
                    End If
                End If
            Else
                zvar = _myRule.usuario
            End If

            zvar = TextoInteligente.ReconocerCodigo(zvar, R)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable original: " & zvar & " Variable pasada por textointeligente: " & zvar)
            zvar = zvar.Trim()

            If VariablesInterReglas.ContainsKey(zvar) Then
                If (TypeOf (VariablesInterReglas.Item(zvar)) Is Long) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable Long, ID usuario")
                    id = VariablesInterReglas.Item(zvar)
                ElseIf (TypeOf (VariablesInterReglas.Item(zvar)) Is String) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable string, nombre del usuario")
                    name = VariablesInterReglas.Item(zvar)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable de otro tipo")
                    name = String.Empty
                    id = 0
                End If
            ElseIf Not Int64.TryParse(zvar, id) Then
                name = String.Empty
                id = 0
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Function PlayTest() As Boolean

    End Function

    Function DiscoverParams() As List(Of String)

    End Function
End Class
