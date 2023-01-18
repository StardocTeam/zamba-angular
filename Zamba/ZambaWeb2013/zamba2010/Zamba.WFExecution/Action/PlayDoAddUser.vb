Imports System.Windows.Forms
Imports Zamba.Data
Public Class PlayDoAddUser
    Private _myRule As IDoAddUser
    Private myNombre As String
    Private myApellido As String
    Private myNameUser As String
    Sub New(ByVal rule As IDoAddUser)
        Me._myRule = rule
    End Sub
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)


        Dim newUser As IUser
        Me.myNameUser = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.nameUser)
        Me.myNameUser = TextoInteligente.ReconocerCodigo(Me._myRule.nameUser, results(0))

        If Me.myNameUser.Contains("@") Then
            Me.myNameUser = (Me.myNameUser.Split("@"))(0)
        End If

        Me.myNombre = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.nombre)
        Me.myNombre = TextoInteligente.ReconocerCodigo(Me._myRule.nombre, results(0))

        Me.myApellido = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.apellido)
        Me.myApellido = TextoInteligente.ReconocerCodigo(Me._myRule.apellido, results(0)).Trim()

            If Not String.IsNullOrEmpty(Me.myNameUser) And Not String.IsNullOrEmpty(Me.myNombre) And Not String.IsNullOrEmpty(Me.myApellido) Then
                Dim UB As New UserBusiness
            newUser = UB.GetNewUser(Me.myNameUser, Me.myNombre, Me.myApellido, _myRule.password)
            UB = Nothing
            End If

            If Not IsNothing(newUser) Then
            newUser.eMail.ProveedorSMTP = "smtp.gmail.com"
            newUser.eMail.Puerto = 587
            newUser.eMail.Type = 1
            newUser.eMail.EnableSsl = True
            newUser.eMail.Mail = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.email)
            newUser.eMail.Mail = TextoInteligente.ReconocerCodigo(Me._myRule.email, results(0)).Trim()
            newUser.eMail.UserName = Me.myNameUser

            If Not String.IsNullOrEmpty(Me._myRule.telefono) Then
                newUser.telefono = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.telefono)
                newUser.telefono = TextoInteligente.ReconocerCodigo(Me._myRule.telefono, results(0)).Trim()
            End If

            If Not String.IsNullOrEmpty(Me._myRule.puesto) Then
                newUser.puesto = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.puesto)
                newUser.puesto = TextoInteligente.ReconocerCodigo(Me._myRule.puesto, results(0)).Trim()
            End If

            If Not String.IsNullOrEmpty(Me._myRule.password) Then
                newUser.Password = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.password)
                newUser.Password = TextoInteligente.ReconocerCodigo(Me._myRule.password, results(0)).Trim()
            End If

            If Not String.IsNullOrEmpty(Me._myRule.avatar) Then
                newUser.Picture = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._myRule.avatar)
                newUser.Picture = TextoInteligente.ReconocerCodigo(Me._myRule.avatar, results(0)).Trim()
            End If
            Dim UB As New UserBusiness
            UB.Update(newUser)
            UB.SetNewUser(newUser)

            ZTrace.WriteLineIf(ZTrace.IsInfo, Me._myRule.varUsr & ".id :" & newUser.ID)
                ZTrace.WriteLineIf(ZTrace.IsInfo, Me._myRule.varUsr & ".nombre :" & newUser.Name)

                If VariablesInterReglas.ContainsKey(Me._myRule.varUsr & ".id") = False AndAlso VariablesInterReglas.ContainsKey(Me._myRule.varUsr & ".nombreUsr") = False Then
                    VariablesInterReglas.Add(Me._myRule.varUsr & ".id", newUser.ID)
                    VariablesInterReglas.Add(Me._myRule.varUsr & ".nombre", newUser.Name)
                Else
                    VariablesInterReglas.Item(Me._myRule.varUsr & ".id") = newUser.ID
                VariablesInterReglas.Item(Me._myRule.varUsr & ".nombre") = newUser.Name
            End If
        End If


            Return results
    End Function

End Class
