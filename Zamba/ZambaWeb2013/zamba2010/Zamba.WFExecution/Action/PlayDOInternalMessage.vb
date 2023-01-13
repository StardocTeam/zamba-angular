
''' <summary>
''' 
''' </summary>
''' <history>Marcelo Modified 04/07/2008
''' </history>
''' <remarks></remarks>
Public Class PlayDOInternalMessage

    Dim msg As InternalMessage
    Dim currentRule As IDOInternalMessage

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IDOInternalMessage) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try

            Me.currentRule = myrule
            msg = myrule.Msg
            SendInternalMessage()
        Finally

        End Try

        Return results
    End Function

    Public Sub SendInternalMessage()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Configurando el mensaje...")
        msg.Subject = Me.currentRule.Msg.Subject
        msg.Body = Me.currentRule.Msg.Body
        If Me.currentRule.ToStr.Length > 0 Then FillDestinatarios(Me.currentRule.ToStr, MessageType.MailTo)
        If Me.currentRule.CCStr.Length > 0 Then FillDestinatarios(Me.currentRule.CCStr, MessageType.MailCC)
        If Me.currentRule.CCOStr.Length > 0 Then FillDestinatarios(Me.currentRule.CCOStr, MessageType.MailCCO)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Enviando el mensaje...")
        msg.Send()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Mensaje enviado con éxito.")
    End Sub

    Private Sub FillDestinatarios(ByVal _destString As String, ByVal _mType As MessageType)

        Dim destStringArray() As String = _destString.Split(";")
        Dim UserBusiness As New UserBusiness

        For Each s As String In destStringArray

            Dim user As IUser

            Try

                user = UserBusiness.GetUserById(CInt(s))

                Dim dest As New Destinatario(user, _mType)

                msg.Destinatarios.Add(dest)

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

        Next

    End Sub

    'Private Sub Send(ByVal taskResult As Core.TaskResult)
    '    If msg.AttachList Is Nothing Then msg.AttachList = New ArrayList
    '    msg.AttachList.Add(taskResult)
    '    msg.Send()
    'End Sub
    'Private Sub Send(ByRef Results As System.Collections.Generic.List(Of Core.ITaskResult))
    '    If Not IsNothing(Results) Then
    '        RaiseEvent AdjuntarAMensaje(Results)
    '    End If
    'End Sub
    'Dim msg As Zamba.Controls.InternalMessage
    'Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IDOInternalMessage) As System.Collections.Generic.List(Of Core.ITaskResult)
    '    Try
    '        msg = myrule.Msg

    '        If myrule.OneDocPerMail Then
    '            For Each taskResult As Core.TaskResult In results
    '                Send(taskResult)
    '            Next
    '        Else
    '            Send(results)
    '        End If
    '    Catch ex As Exception
    '        Zamba.Core.ZClass.RaiseError(ex)
    '    End Try
    '    Return results
    'End Function

    'Private Sub Send(ByVal taskResult As Core.TaskResult)
    '    If Msg.AttachList Is Nothing Then Msg.AttachList = New ArrayList
    '    Msg.AttachList.Add(taskResult)
    '    Msg.Send()
    'End Sub
    'Private Sub Send(ByRef Results As System.Collections.Generic.List(Of Core.ITaskResult))
    '    If Msg.AttachList Is Nothing Then Msg.AttachList = New ArrayList
    '    For Each item As TaskResult In Results
    '        msg.AttachList.Add(item) 'DirectCast(DirectCast(item, DictionaryEntry).Value, Core.TaskResult))
    '    Next
    '    msg.Send()
    'End Sub

End Class
