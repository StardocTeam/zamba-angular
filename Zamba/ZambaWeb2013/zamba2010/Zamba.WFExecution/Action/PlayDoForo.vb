Imports Zamba.Controls
Imports System.Windows.Forms
Imports System
Imports System.Windows
Imports Zamba.ClientControls



Public Class PlayDoForo


    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Myrule As IDoForo) As System.Collections.Generic.List(Of Core.ITaskResult)
        'Creo un flag con la que voy a saber si el usuario
        'guardó o guardo/notifico el mensaje (true)
        'o cerro la ventana (false)    
        Dim flagSaved As Boolean = False
        Dim FirstTimeRule As Boolean = True
        Dim hash As New Hashtable()
        Dim VarInterReglas As New VariablesInterReglas()
        Try
            For Each T As ITaskResult In results

                Dim Body As String = String.Empty
                Body = Zamba.Core.TextoInteligente.ReconocerCodigo(Myrule.Body, T)
                Body = VarInterReglas.ReconocerVariablesValuesSoloTexto(Body)
                hash.Add("body", Body)

                Dim subject As String = String.Empty
                subject = Zamba.Core.TextoInteligente.ReconocerCodigo(Myrule.Subject, T)
                subject = VarInterReglas.ReconocerVariablesValuesSoloTexto(subject)
                hash.Add("subject", Myrule.Subject)

                hash.Add("flagSaved", flagSaved.ToString)
                hash.Add("idMensaje", Myrule.IdMensaje)
                hash.Add("automatic", Myrule.Automatic)

                Dim Participantes As Object = String.Empty
                Participantes = Zamba.Core.TextoInteligente.ReconocerCodigo(Myrule.Participantes, T)
                Participantes = VarInterReglas.ReconocerVariablesAsObject(Participantes)
                hash.Add("participantes", Myrule.Participantes)

                '[pablo] 26/06/2010
                'se agraga esta variable para validar una unica ejecucion de la regla al dispararse el evento eHandleModuleRuleAction
                '
                hash.Add("FirstTimeRule", FirstTimeRule)

                If VariablesInterReglas.ContainsKey(Myrule.IdMensaje) = False Then
                    VariablesInterReglas.Add(Myrule.IdMensaje, hash.Item("idMensaje"))
                Else
                    VariablesInterReglas.Item(Myrule.IdMensaje) = hash.Item("idMensaje")
                End If
            Next

        Finally
            VarInterReglas = Nothing
            If hash IsNot Nothing Then
                hash = Nothing
            End If
        End Try

        Return results
    End Function
End Class
