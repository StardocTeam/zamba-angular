Public Class PlayDoForo


    Private myRule As IDoForo
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        'Creo un flag con la que voy a saber si el usuario
        'guardó o guardo/notifico el mensaje (true)
        'o cerro la ventana (false)    
        Dim flagSaved As Boolean = False
        Dim FirstTimeRule As Boolean = True
        Dim hash As New Hashtable()
        Try
            For Each T As ITaskResult In results

                Dim Body As String = String.Empty
                Body = TextoInteligente.ReconocerCodigo(Myrule.Body, T)
                Body = WFRuleParent.ReconocerVariablesValuesSoloTexto(Body)
                hash.Add("body", Body)

                Dim subject As String = String.Empty
                subject = TextoInteligente.ReconocerCodigo(Myrule.Subject, T)
                subject = WFRuleParent.ReconocerVariablesValuesSoloTexto(subject)
                hash.Add("subject", subject.Trim())

                hash.Add("flagSaved", flagSaved.ToString)
                hash.Add("idMensaje", Myrule.IdMensaje)
                hash.Add("automatic", Myrule.Automatic)

                Dim Participantes As Object = String.Empty
                Participantes = TextoInteligente.ReconocerCodigo(Myrule.Participantes, T)
                Participantes = WFRuleParent.ReconocerVariablesAsObject(Participantes)
                If String.Compare(Participantes.trim(), String.Empty) = 0 Then
                    hash.Add("participantes", Nothing)
                Else
                    hash.Add("participantes", Participantes)
                End If

                'funcionalidad de ejecucion de regla
                Dim BtnRuleName As String = TextoInteligente.ReconocerCodigo(Myrule.BtnName, results(0))
                If Myrule.BtnName.ToString.ToLower.Contains("zvar") Then
                    BtnRuleName = WFRuleParent.ReconocerVariablesValuesSoloTexto(BtnRuleName)
                End If

                hash.Add("RuleID", Myrule.ExecuteRuleID)
                hash.Add("BtnName", BtnRuleName)

                If Myrule.ExecuteRuleID > 0 Then
                    hash.Add("Result", results(0))
                Else
                    hash.Add("Result", Nothing)
                End If

                '[pablo] 26/06/2010
                'se agraga esta variable para validar una unica ejecucion de la regla al dispararse el evento eHandleModuleRuleAction
                '
                hash.Add("FirstTimeRule", FirstTimeRule)
                Result.HandleRuleModule(ResultActions.Foro_NewMessage, results, hash)

                If VariablesInterReglas.ContainsKey(Myrule.IdMensaje) = False Then
                    VariablesInterReglas.Add(Myrule.IdMensaje, hash.Item("idMensaje"), False)
                Else
                    VariablesInterReglas.Item(Myrule.IdMensaje) = hash.Item("idMensaje")
                End If
            Next

        Finally
            If hash IsNot Nothing Then
                hash = Nothing
            End If
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoForo)
        myRule = rule
    End Sub
End Class
