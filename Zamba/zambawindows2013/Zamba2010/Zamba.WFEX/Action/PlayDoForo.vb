Imports Zamba.Controls
Imports System.Windows.Forms
Imports System
Imports System.Windows
Imports Zamba.ClientControls
Imports Zamba.Core

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
                Body = Zamba.Core.TextoInteligente.ReconocerCodigo(myRule.Body, T)
                Body = WFRuleParent.ReconocerVariablesValuesSoloTexto(Body)
                hash.Add("body", Body)

                Dim subject As String = String.Empty
                subject = Zamba.Core.TextoInteligente.ReconocerCodigo(myRule.Subject, T)
                subject = WFRuleParent.ReconocerVariablesValuesSoloTexto(subject)
                hash.Add("subject", subject.Trim())

                hash.Add("flagSaved", flagSaved.ToString)
                hash.Add("idMensaje", myRule.IdMensaje)
                hash.Add("automatic", myRule.Automatic)

                Dim Participantes As Object = String.Empty
                Participantes = Zamba.Core.TextoInteligente.ReconocerCodigo(myRule.Participantes, T)
                Participantes = WFRuleParent.ReconocerVariablesAsObject(Participantes)
                If String.Compare(Participantes.trim(), String.Empty) = 0 Then
                    hash.Add("participantes", Nothing)
                Else
                    hash.Add("participantes", Participantes)
                End If

                'funcionalidad de ejecucion de regla
                Dim BtnRuleName As String = Zamba.Core.TextoInteligente.ReconocerCodigo(myRule.BtnName, results(0))
                If myRule.BtnName.ToString.ToLower.Contains("zvar") Then
                    BtnRuleName = WFRuleParent.ReconocerVariablesValuesSoloTexto(BtnRuleName)
                End If

                hash.Add("RuleID", myRule.ExecuteRuleID)
                hash.Add("BtnName", BtnRuleName)

                If myRule.ExecuteRuleID > 0 Then
                    hash.Add("Result", results(0))
                Else
                    hash.Add("Result", Nothing)
                End If

                '[pablo] 26/06/2010
                'se agraga esta variable para validar una unica ejecucion de la regla al dispararse el evento eHandleModuleRuleAction
                '
                hash.Add("FirstTimeRule", FirstTimeRule)
                Result.HandleRuleModule(ResultActions.Foro_NewMessage, results, hash)

                If VariablesInterReglas.ContainsKey(myRule.IdMensaje) = False Then
                    VariablesInterReglas.Add(myRule.IdMensaje, hash.Item("idMensaje"), False)
                Else
                    VariablesInterReglas.Item(myRule.IdMensaje) = hash.Item("idMensaje")
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
        Me.myRule = rule
    End Sub
End Class
