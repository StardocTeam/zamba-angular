Public Class PlayDoAddUser
    Private _myRule As IDoAddUser
    Sub New(ByVal rule As IDoAddUser)
        _myRule = rule
    End Sub
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Try
            Dim newUser As IUser
            _myRule.nameUser = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.nameUser)
            _myRule.nameUser = TextoInteligente.ReconocerCodigo(_myRule.nameUser, results(0))

            If _myRule.nameUser.Contains("@") Then
                _myRule.nameUser = (_myRule.nameUser.Split("@"))(0)
            End If

            _myRule.nombre = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.nombre)
            _myRule.nombre = TextoInteligente.ReconocerCodigo(_myRule.nombre, results(0))

            _myRule.apellido = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.apellido)
            _myRule.apellido = TextoInteligente.ReconocerCodigo(_myRule.apellido, results(0)).Trim()

            If Not String.IsNullOrEmpty(_myRule.nameUser) And Not String.IsNullOrEmpty(_myRule.nombre) And Not String.IsNullOrEmpty(_myRule.apellido) Then
                newUser = UserBusiness.GetNewUser(_myRule.nameUser, _myRule.nombre, _myRule.apellido)
            End If

            If Not IsNothing(newUser) Then
                newUser.eMail.ProveedorSmtp = "smtp.gmail.com"
                newUser.eMail.Puerto = 587
                newUser.eMail.Type = 1
                newUser.eMail.EnableSsl = True
                newUser.eMail.Mail = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.email)
                newUser.eMail.Mail = TextoInteligente.ReconocerCodigo(_myRule.email, results(0)).Trim()

                If Not String.IsNullOrEmpty(_myRule.telefono) Then
                    newUser.telefono = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.telefono)
                    newUser.telefono = TextoInteligente.ReconocerCodigo(_myRule.telefono, results(0)).Trim()
                End If

                If Not String.IsNullOrEmpty(_myRule.puesto) Then
                    newUser.puesto = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.puesto)
                    newUser.puesto = TextoInteligente.ReconocerCodigo(_myRule.puesto, results(0)).Trim()
                End If

                If Not String.IsNullOrEmpty(_myRule.password) Then
                    newUser.Password = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.password)
                    newUser.Password = TextoInteligente.ReconocerCodigo(_myRule.password, results(0)).Trim()
                End If

                If Not String.IsNullOrEmpty(_myRule.avatar) Then
                    newUser.Picture = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.avatar)
                    newUser.Picture = TextoInteligente.ReconocerCodigo(_myRule.avatar, results(0)).Trim()
                End If

                UserBusiness.Update(newUser)
                UserBusiness.Mail.SetNewUser(newUser)

                If VariablesInterReglas.ContainsKey(_myRule.varUsr & ".id") = False AndAlso VariablesInterReglas.ContainsKey(_myRule.varUsr & ".nombreUsr") = False Then
                    VariablesInterReglas.Add(_myRule.varUsr & ".id", newUser.ID, False)
                    VariablesInterReglas.Add(_myRule.varUsr & ".nombre", newUser.Name, False)
                Else
                    VariablesInterReglas.Item(_myRule.varUsr & ".id") = newUser.ID
                    VariablesInterReglas.Item(_myRule.varUsr & ".nombre") = newUser.Name
                End If
            End If


        Catch ex As Exception
            Throw New ZambaEx("Ocurrió un error al insertar el usuario a la base de datos.", ex)
        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function

    Function DiscoverParams() As List(Of String)

    End Function
End Class
