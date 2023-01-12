Imports zamba.core
Imports System.Collections.Generic
Imports System.Xml.Serialization


<RuleCategory("Reglas"), RuleDescription("Solicitar Tarea"), RuleHelp("Permite solicitar una o más tareas a un usuario de forma remota"), RuleFeatures(False)> <Serializable()> _
Public Class DoRequestAction
    Inherits WFRuleParent
    Implements IDoRequestAction, IDisposable


    Private Const RULE_NAME As String = "Requerir Accion"
    Public Const SEPARATOR As String = ";"

    Dim UserGroupBusiness As New UserGroupBusiness
#Region "Atributos"

    Private _ruleIds As List(Of Int64)
    Private _userIds As List(Of Int64)
    Private _serverLocation As String
    Private _message As String
    Private _requestUserId As Int64
    Private _isLoaded As Boolean = False
    Private _disposedValue As Boolean = False
    Private _name As String
    Private _subject As String
    Private _linkMail As String

#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Listado de ids de reglas que recibiran los usuarios.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RuleIds() As List(Of Int64) Implements IDoRequestAction.RuleIds
        Get
            If IsNothing(_ruleIds) Then
                _ruleIds = New List(Of Int64)
            End If

            Return _ruleIds
        End Get
    End Property
    ''' <summary>
    ''' Listado de ids de usuario que recibiran el mensaje.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UserIds() As List(Of Int64) Implements IDoRequestAction.UserIds
        Get
            If IsNothing(_userIds) Then
                _userIds = New List(Of Int64)
            End If

            Return _userIds
        End Get
    End Property

    ''' <summary>
    ''' Path del servidor donde se encuentra la pagina que va a ser redirigido la notificacion.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ServerLocation() As String Implements Core.IDoRequestAction.ServerLocation
        Get
            Return _serverLocation
        End Get
        Set(ByVal value As String)
            _serverLocation = value
        End Set
    End Property

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            If Not IsNothing(_ruleIds) AndAlso Not IsNothing(_userIds) Then
                _isLoaded = True
            End If

            Return _isLoaded
        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property

    ''' <summary>
    ''' Mensaje que recibiran los destinatarios de la notificacion.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NotificationMessage() As String Implements IDoRequestAction.NotificationMessage
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property

    Public Property Subject() As String Implements IDoRequestAction.Subject
        Get
            Return _subject
        End Get
        Set(ByVal value As String)
            _subject = value
        End Set
    End Property
    Public Property LinkMail() As String Implements IDoRequestAction.LinkMail
        Get
            Return _subject
        End Get
        Set(ByVal value As String)
            _linkMail = value
        End Set
    End Property
#End Region

#Region "Constructores"

    '''' <summary>
    '''' Constructor de DoRequestAction
    '''' </summary>
    '''' <param name="id"></param>
    '''' <param name="name"></param>
    '''' <param name="stepId"></param>
    '''' <param name="requestUserId"></param>
    '''' <param name="ruleIds"></param>
    '''' <param name="userIds"></param>
    '''' <param name="pmessage"></param>
    '''' <param name="serverLocation"></param>
    '''' <param name="nameRequestAction"></param>
    '''' <remarks></remarks>
    '''' <history>
    ''''     [Gaston]	30/07/2008	Modified    
    ''''     [Gaston]	30/10/2008	Modified    Si el usersId contiene userAssigned entonces se coloca en usersIds -1 
    '''' </history>
    'Public Sub New(ByVal id As Int64, ByVal name As String, ByVal stepId As Int64, ByVal requestUserId As Int64, ByVal ruleIds As String, ByVal userIds As String, ByVal pmessage As String, ByVal serverLocation As String, ByVal nameRequestAction As String)

    '    MyBase.New(id, name, stepId)

    '    _userIds = New List(Of Int64)

    '    ' Si el usuario no contiene "userAssigned" entonces se asignan los usuarios a la colección _usersIds (si es que existen). De lo contrario,
    '    ' se coloca en la colección -1, indicando que se debe activar el checkbox "Enviar al usuario asignado"
    '    If Not (userIds.Contains("userAssigned")) Then

    '        Dim CurrentRuleId As Int64
    '        For Each CurrentUser As String In userIds.Split(SEPARATOR)
    '            If Int64.TryParse(CurrentUser, CurrentRuleId) Then
    '                _userIds.Add(CurrentRuleId)
    '            End If
    '        Next

    '    Else
    '        _userIds.Add(-1)
    '    End If

    '    _ruleIds = New List(Of Int64)
    '    Dim CurrentUserId As Int64
    '    For Each CurrentRule As String In ruleIds.Split(SEPARATOR)
    '        If Int64.TryParse(CurrentRule, CurrentUserId) Then
    '            _ruleIds.Add(CurrentUserId)
    '        End If
    '    Next

    '    _requestUserId = requestUserId
    '    _message = pmessage
    '    _serverLocation = serverLocation
    '    _name = nameRequestAction

    'End Sub


    Public Sub New(ByVal id As Int64, ByVal name As String, ByVal stepId As Int64, ByVal requestUserId As Int64, ByVal ruleIds As String, ByVal userIds As String, ByVal pmessage As String, ByVal serverLocation As String, ByVal nameRequestAction As String, ByVal Subject As String, ByVal LinkMail As String)

        MyBase.New(id, name, stepId)

        _userIds = New List(Of Int64)

        ' Si el usuario no contiene "userAssigned" entonces se asignan los usuarios a la colección _usersIds (si es que existen). De lo contrario,
        ' se coloca en la colección -1, indicando que se debe activar el checkbox "Enviar al usuario asignado"
        If Not (userIds.Contains("userAssigned")) Then

            Dim CurrentUserId As Int64
            For Each CurrentUser As String In userIds.Split(SEPARATOR)
                If Int64.TryParse(CurrentUser, CurrentUserId) Then
                    _userIds.Add(CurrentUserId)
                End If
            Next

        Else
            _userIds.Add(-1)
        End If

        _ruleIds = New List(Of Int64)
        Dim CurrentRuleId As Int64
        For Each CurrentRule As String In ruleIds.Split(SEPARATOR)
            If Int64.TryParse(CurrentRule, CurrentRuleId) Then
                _ruleIds.Add(CurrentRuleId)
            End If
        Next

        _requestUserId = requestUserId
        _message = pmessage
        _serverLocation = serverLocation
        _name = nameRequestAction
        _subject = Subject
        _linkMail = LinkMail
    End Sub
#End Region

    Public Overrides Sub FullLoad()
        Load()
    End Sub

    Public Overrides Sub Load()
        If (Not IsFull()) Then
            _userIds = New List(Of Int64)
            _ruleIds = New List(Of Int64)
        End If
    End Sub

    ''' <summary>
    ''' Play de la regla DoRequestAction
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	30/07/2008	Modified    
    '''     [Gaston]	30/10/2008	Modified
    ''' </history>
    Public Overrides Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
        Using CurrentRequestAction As New RequestAction()

            CurrentRequestAction.RequestDate = Date.Now
            CurrentRequestAction.UsersIds.AddRange(_userIds)
            CurrentRequestAction.RulesIds.AddRange(_ruleIds)
            CurrentRequestAction.RequestUserId = _requestUserId
            CurrentRequestAction.ServerLocation = _serverLocation
            CurrentRequestAction.Name = _name

            ' Si UsersIds contiene -1 entonces la regla tiene el checkbox "Enviar a usuario asignado" activado
            If (CurrentRequestAction.UsersIds.Contains(-1)) Then
                Return (SendMailToUserAssigned(results, CurrentRequestAction))
            Else
                Return (SendMailToUsers(results, CurrentRequestAction))
            End If

        End Using

        Return (results)

    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Using CurrentRequestAction As New RequestAction()

            CurrentRequestAction.RequestDate = Date.Now
            CurrentRequestAction.UsersIds.AddRange(_userIds)
            CurrentRequestAction.RulesIds.AddRange(_ruleIds)
            CurrentRequestAction.RequestUserId = _requestUserId
            CurrentRequestAction.ServerLocation = _serverLocation
            CurrentRequestAction.Name = _name

            ' Si UsersIds contiene -1 entonces la regla tiene el checkbox "Enviar a usuario asignado" activado
            If (CurrentRequestAction.UsersIds.Contains(-1)) Then
                Return (SendMailToUserAssigned(results, CurrentRequestAction))
            Else
                Return (SendMailToUsers(results, CurrentRequestAction))
            End If

        End Using

        Return (results)
    End Function
    ''' <summary>
    ''' Método que envia un mail al usuario (usuario o grupo) asignado de cada tarea
    ''' </summary>
    ''' <param name="results">Colección de tareas</param>
    ''' <param name="CurrentRequestAction">Instancia de CurrentRequestAction</param>
    ''' <returns>colección de tareas</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	30/10/2008	Created    
    ''' </history>
    Private Function SendMailToUserAssigned(ByRef results As List(Of ITaskResult), ByRef CurrentRequestAction As RequestAction) As List(Of ITaskResult)

        Dim UB As New UserBusiness
        For Each CurrentTaskResult As ITaskResult In results

            CurrentRequestAction.UsersIds.Clear()

            ' Si lo que retorna el método no está vacío significa que el id al que está asignado la tarea pertenece a un usuario

            If (UB.GetUserNamebyId(CurrentTaskResult.AsignedToId) <> String.Empty) Then
                CurrentRequestAction.UsersIds.Add(CurrentTaskResult.AsignedToId)
                ' Sino, el id debería ser de un grupo
            Else
                ' Se obtienen los usuarios del grupo
                CurrentRequestAction.UsersIds.AddRange(UserGroupBusiness.GetUsersIds(CurrentTaskResult.AsignedToId))
            End If

            CurrentRequestAction.Tasks.Add(New RequestActionTask(CurrentTaskResult.TaskId, CurrentTaskResult.StepId))
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Desde SendMailToUserAssigned invoko a RequestActionBusiness.Notify(CurrentRequestAction, _message, _subject, _linkMail) 'RULE_NAME")
            RequestActionBusiness.Notify(CurrentRequestAction, _message, _subject, _linkMail) 'RULE_NAME
            CurrentRequestAction.Tasks.Clear()
        Next
        UB = Nothing
        Return (results)
    End Function

    ''' <summary>
    ''' Método que envia un mail a la lista de usuarios que se configuro en la regla
    ''' </summary>
    ''' <param name="results">Colección de tareas</param>
    ''' <param name="CurrentRequestAction">Instancia de CurrentRequestAction</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	30/10/2008	Modified    El código original estaba ubicado en el método Play
    ''' </history>
    Private Function SendMailToUsers(ByRef results As List(Of ITaskResult), ByRef CurrentRequestAction As RequestAction) As List(Of ITaskResult)
        'CurrentRequestAction.NotificationMessage = _message

        For Each CurrentTaskResult As ITaskResult In results
            CurrentRequestAction.Tasks.Add(New RequestActionTask(CurrentTaskResult.TaskId, CurrentTaskResult.StepId))
        Next

        'RequestActionBusiness.Insert(CurrentRequestAction)
        RequestActionBusiness.Notify(CurrentRequestAction, _message, _subject, _linkMail) 'RULE_NAME)

        Return (results)

    End Function

#Region "Dispose"
    Protected Sub Dispose2(ByVal disposing As Boolean)
        If Not _disposedValue Then
            If disposing Then
                If (Not IsNothing(_ruleIds)) Then
                    _ruleIds.Clear()
                    _ruleIds = Nothing
                End If
                If (Not IsNothing(_userIds)) Then
                    _userIds.Clear()
                    _userIds = Nothing
                End If
            End If

        End If
        _disposedValue = True
    End Sub

    Public Overrides Sub Dispose()
        Dispose2(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
