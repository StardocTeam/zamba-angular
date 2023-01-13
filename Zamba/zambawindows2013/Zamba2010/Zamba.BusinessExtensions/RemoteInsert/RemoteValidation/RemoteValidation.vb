Imports System.Text
Imports Zamba.Core.WF.WF

Public Class RemoteValidation

    Public Shared Sub Validate(ByVal userIds As List(Of Int64), ByVal taskIds As List(Of Int64), ByVal ruleIds As List(Of Int64), ByVal subject As String, ByVal description As String, ByVal linkMail As String)
        Dim Mail As New ValidationMail(userIds, taskIds, ruleIds, subject, description, linkMail)

        Mail.Send()

        Mail.Dispose()
    End Sub

    Private Class ValidationMail
        Implements IDisposable


#Region "Constantes"
        Private Const SEPARATOR As String = ";"
        Private Const QUERYSTRING_TASK_IDS As String = "TaskIds"
        Private Const QUERYSTRING_RULE_IDS As String = "RuleIds"
        Private Const REDIRECT_URL As String = "http://www.google.com.ar/"
        Private Const REDIRECT_BUTTON_MESSAGE As String = "Ver"
        Private Const ERRORS_COUNT_TO_VALIDATE As Int32 = 5
#End Region

#Region "Atributos"
        Private _disposedValue As Boolean = False
        Private _userIds As List(Of Int64) = Nothing
        Private _ruleIds As List(Of Int64) = Nothing
        Private _taskIds As List(Of Int64) = Nothing
        Private _subject As String = Nothing
        Private _description As String = Nothing
        Private _linkMail As String = Nothing

#End Region

#Region "Propiedades"
        Public WriteOnly Property UserIds() As List(Of Int64)
            Set(ByVal value As List(Of Int64))
                _userIds = value
            End Set
        End Property
        Public WriteOnly Property RuleIds() As List(Of Int64)
            Set(ByVal value As List(Of Int64))
                _ruleIds = value
            End Set
        End Property
        Public WriteOnly Property TaskIds() As List(Of Int64)
            Set(ByVal value As List(Of Int64))
                _taskIds = value
            End Set
        End Property
        Public WriteOnly Property Subject() As String
            Set(ByVal value As String)
                _subject = value
            End Set
        End Property
        Public WriteOnly Property Description() As String
            Set(ByVal value As String)
                _description = value
            End Set
        End Property
        Public WriteOnly Property LinkMail() As String
            Set(ByVal value As String)
                _linkMail = value
            End Set
        End Property
#End Region

#Region "Constructores"
        Public Sub New(ByVal userIds As List(Of Int64), ByVal taskIds As List(Of Int64), ByVal ruleIds As List(Of Int64), ByVal subject As String, ByVal description As String, ByVal linkmail As String)
            Me.UserIds = userIds
            Me.TaskIds = taskIds
            Me.RuleIds = ruleIds
            Me.Subject = subject
            Me.Description = description
            Me.LinkMail = linkmail
        End Sub
#End Region


#Region "Send Mail"
        Public Sub Send()
            Dim ErrorList As New List(Of String)(ERRORS_COUNT_TO_VALIDATE)

            If IsValid(ErrorList) Then
                Dim HtmlBuilder As New StringBuilder()

                HtmlBuilder.Append(BuildDescription(_description))
                HtmlBuilder.Append(BuildTaskList(_taskIds))
                HtmlBuilder.Append(BuildRedirectButton(REDIRECT_URL, _ruleIds, _taskIds, REDIRECT_BUTTON_MESSAGE))

                Dim UserList As List(Of IUser) = UserBusiness.GetUsers(_userIds)

                Dim ToList As New List(Of String)(_userIds.Count)
                If Not IsNothing(UserList) Then
                    For Each CurrentUser As IUser In UserList
                        ToList.Add(CurrentUser.eMail.Mail)
                    Next
                End If

                'Zamba.Outlook.Outlook.SendMail(HtmlBuilder.ToString(), ToList, _subject)

                HtmlBuilder.Remove(0, HtmlBuilder.Length)
                HtmlBuilder = Nothing
            Else
                Dim ErrorBuilder As New StringBuilder()

                For Each CurrentError As String In ErrorList
                    ErrorBuilder.Append(CurrentError)
                    ErrorBuilder.AppendLine()
                Next

                Throw New Exception(ErrorBuilder.ToString())
            End If

        End Sub

        Private Shared Function BuildDescription(ByVal description As String) As String
            Return "<blockquote>" + description + "</blockquote>"
        End Function

        Private Shared Function BuildTaskList(ByVal taskIds As List(Of Int64)) As String

            Dim TaskNameTable As DataTable = WFTaskBusiness.GetTasksNamesByTaskIds(taskIds)

            If IsNothing(TaskNameTable) Then
                Return String.Empty
            End If

            Dim TaskListBuilder As New StringBuilder()
            TaskListBuilder.Append("<ul>")

            For Each CurrentTask As DataRow In TaskNameTable.Rows
                TaskListBuilder.Append("<li>")
                TaskListBuilder.Append(CurrentTask("Name"))
                TaskListBuilder.Append("</li>")
            Next

            TaskNameTable.Dispose()
            TaskNameTable = Nothing

            TaskListBuilder.Append("</ul>")

            Dim ReturnValue As String = TaskListBuilder.ToString()

            TaskListBuilder.Remove(0, TaskListBuilder.Length)
            TaskListBuilder = Nothing

            Return ReturnValue
        End Function

        Private Shared Function BuildRedirectButton(ByVal url As String, ByVal ruleIds As List(Of Int64), ByVal taskIds As List(Of Int64), ByVal message As String) As String
            If IsNothing(ruleIds) OrElse ruleIds.Count = 0 OrElse IsNothing(taskIds) OrElse taskIds.Count = 0 Then
                Return Nothing
            End If

            Dim UrlBuilder As New StringBuilder()
            UrlBuilder.Append("<a href=""")

            UrlBuilder.Append(url)
            UrlBuilder.Append("?")
            UrlBuilder.Append(QUERYSTRING_TASK_IDS)
            UrlBuilder.Append("=")

            For Each CurrentTaskId As Int64 In taskIds
                UrlBuilder.Append(CurrentTaskId.ToString())
                UrlBuilder.Append(SEPARATOR)
            Next

            UrlBuilder.Remove(UrlBuilder.Length - SEPARATOR.Length, SEPARATOR.Length)

            UrlBuilder.Append("&")
            UrlBuilder.Append(QUERYSTRING_RULE_IDS)
            UrlBuilder.Append("=")

            For Each CurrentRuleId As Int64 In ruleIds
                UrlBuilder.Append(CurrentRuleId.ToString())
                UrlBuilder.Append(SEPARATOR)
            Next

            UrlBuilder.Append(""">")
            UrlBuilder.Append(message)
            UrlBuilder.Append("</>")


            Dim ParsedUrl As String = UrlBuilder.ToString()

            UrlBuilder.Remove(0, UrlBuilder.Length)
            UrlBuilder = Nothing

            Return ParsedUrl
        End Function
#End Region


        Private Function IsValid(ByRef errorList As List(Of String)) As Boolean

            If IsNothing(errorList) Then
                errorList = New List(Of String)(ERRORS_COUNT_TO_VALIDATE)
            End If

            Dim IsMailValid As Boolean = True

            If String.IsNullOrEmpty(_description) Then
                ErrorList.Add("No se permite enviar mails con descripción vacia.")
                IsMailValid = False
            ElseIf String.IsNullOrEmpty(_subject) Then
                ErrorList.Add("No se permite enviar mails con asunto vacio.")
                IsMailValid = False
            ElseIf IsNothing(_userIds) OrElse _userIds.Count = 0 Then
                ErrorList.Add("No se permite enviar mails con el listado de destinatarios vacio.")
                IsMailValid = False
            ElseIf IsNothing(_taskIds) OrElse _taskIds.Count = 0 Then
                ErrorList.Add("No se permite enviar mails con el listado de tareas vacio.")
                IsMailValid = False
            ElseIf IsNothing(_ruleIds) OrElse _ruleIds.Count = 0 Then
                ErrorList.Add("No se permite enviar mails con el listado de reglas vacio.")
                IsMailValid = False

            End If

            Return IsMailValid
        End Function

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not _disposedValue Then

                _description = Nothing
                _subject = Nothing

                If Not IsNothing(_userIds) Then
                    _userIds.Clear()
                    _userIds = Nothing
                End If

                If Not IsNothing(_ruleIds) Then
                    _ruleIds.Clear()
                    _ruleIds = Nothing
                End If

                If Not IsNothing(_taskIds) Then
                    _taskIds.Clear()
                    _taskIds = Nothing
                End If

            End If

            _disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

    End Class

End Class
