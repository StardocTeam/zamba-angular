
''' <summary>
''' 
''' </summary>
''' <history>Marcelo Modified 04/07/2008
''' </history>
''' <remarks></remarks>
Public Class PlayDOInternalMessage

    Dim msg As InternalMessage
    Private myRule As IDOInternalMessage



    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            msg = myRule.Msg
            SendInternalMessage()
        Finally

        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub SendInternalMessage()
        Trace.WriteLineIf(ZTrace.IsInfo, "Configurando el mensaje...")
        msg.Subject = Me.myRule.Msg.Subject
        msg.Body = Me.myRule.Msg.Body
        If Me.myRule.ToStr.Length > 0 Then FillDestinatarios(Me.myRule.ToStr, MessageType.MailTo)
        If Me.myRule.CCStr.Length > 0 Then FillDestinatarios(Me.myRule.CCStr, MessageType.MailCC)
        If Me.myRule.CCOStr.Length > 0 Then FillDestinatarios(Me.myRule.CCOStr, MessageType.MailCCO)
        Trace.WriteLineIf(ZTrace.IsInfo, "Enviando el mensaje...")
        msg.Send()
        Trace.WriteLineIf(ZTrace.IsInfo, "Mensaje enviado con éxito.")
    End Sub

    Private Sub FillDestinatarios(ByVal _destString As String, ByVal _mType As MessageType)

        Dim destStringArray() As String = _destString.Split(";")

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

    Public Sub New(ByVal rule As IDOInternalMessage)
        Me.myRule = rule
    End Sub
End Class
