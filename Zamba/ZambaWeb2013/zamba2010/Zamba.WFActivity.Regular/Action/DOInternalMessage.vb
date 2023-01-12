Imports System.Collections.Generic
Imports Zamba.Core
Imports Zamba.Controls
Imports System.Xml.Serialization

<RuleCategory("Mensajes"), RuleDescription("Mensaje Interno"), RuleHelp("Permite enviar un mail interno"), RuleFeatures(False)> <Serializable()> _
Public Class DOInternalMessage
    Inherits WFRuleParent
    Implements IDOInternalMessage
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub

#Region "Atributos"
    Private _msg As InternalMessage
    Private _ToStr As String
    Private _CCStr As String
    Private _CCOStr As String
    Private _oneDocPerMail As Boolean
#End Region

#Region "Propiedades"

    Public Property Msg() As IInternalMessage Implements IDOInternalMessage.Msg
        Get
            Return _msg
        End Get
        Set(ByVal value As IInternalMessage)
            _msg = value
        End Set
    End Property

    Public Property OneDocPerMail() As Boolean Implements IDOInternalMessage.OneDocPerMail
        Get
            Return _oneDocPerMail
        End Get
        Set(ByVal value As Boolean)
            _oneDocPerMail = value
        End Set
    End Property
    Public Property CCOStr() As String Implements Core.IDOInternalMessage.CCOStr
        Get
            Return Me._CCOStr

        End Get
        Set(ByVal value As String)
            Me._CCOStr = value
        End Set
    End Property

    Public Property CCStr() As String Implements Core.IDOInternalMessage.CCStr
        Get
            Return Me._CCStr
        End Get
        Set(ByVal value As String)
            Me._CCStr = value
        End Set
    End Property

    Public Property ToStr() As String Implements Core.IDOInternalMessage.ToStr
        Get
            Return Me._ToStr
        End Get
        Set(ByVal value As String)
            Me._ToStr = value
        End Set
    End Property

#End Region

    Public Overrides Sub Dispose()

    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal _ToStr As String, ByVal _CCStr As String, ByVal _CCOStr As String, ByVal subject As String, ByVal body As String, ByVal oneDocPerMail As Boolean)
        MyBase.New(Id, Name, wfstepid)
        Msg = New InternalMessage()
        Try
            Me.ToStr = _ToStr
            Me.CCStr = _CCStr
            Me.CCOStr = _CCOStr
            Msg.Subject = subject
            Msg.Body = body
            Me.OneDocPerMail = oneDocPerMail

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub



#Region "Métodos"
    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As System.Collections.SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDOInternalMessage()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDOInternalMessage()
        Return playRule.Play(results, Me)
    End Function
#End Region


    'Comentado por: Alejandro 09/08/07
    '
    'Private Sub BuildTo(ByVal toStr As String)
    '    toStr = "793;43;98"
    '    If toStr.Length > 0 Then

    '        Dim ids() As String = toStr.Split(";")
    '        For Each str As String In ids

    '            Dim user as iuser = UserBusiness.GetUserById(Int32.Parse(str))

    '            Dim destinatario As New Zamba.Controls.Destinatario(user, MessageType.MailTo)

    '            Msg.Destinatarios.Add(destinatario)


    '            '    Dim user as iuser
    '            '    Try
    '            '        user = UserBusiness.GetUserById(CInt(str))
    '            '    Catch ex As Exception 'Si no encuentra un usuario con este  id tira error =/
    '            '        Throw New Exception("No se pudo encontrar un usuario con id " & str)
    '            '        Exit For
    '            '    End Try
    '            '    If IsNothing(user) Then
    '            '        Throw New Exception("No se pudo encontrar un usuario con id " & str)
    '            '    Else
    '            '        If (s = String.Empty) Then
    '            '            s = user.Name
    '            '        Else
    '            '            Msg.TOUSER.Add(user)
    '            '            s = s & ";" & user.Name
    '            '        End If
    '            '    End If
    '            '    user = Nothing
    '            'Next
    '            'Msg.ToStr = toStr
    '            'toStr = Msg.ToStr+
    '        Next
    '    End If
    'End Sub
    'Private Sub BuildCC(ByVal cC As String)
    '    If cC.Length > 0 Then
    '        Dim ids() As String = cC.Split(";")

    '        For Each str As String In ids
    '            Dim user as iuser
    '            Try
    '                user = UserBusiness.GetUserById(CInt(str))
    '            Catch ex As Exception 'Si no encuentra un usuario con este  id tira error =/
    '                Throw New Exception("No se pudo encontrar un usuario con id " & str)
    '                Exit For
    '            End Try
    '            If IsNothing(user) Then
    '                Throw New Exception("No se pudo encontrar un usuario con id " & str)
    '            Else
    '                Msg.CC.Add(user)
    '                Msg.CCStr &= user.Name & " "
    '            End If
    '            user = Nothing
    '        Next
    '        Msg.CCStr = cC
    '    End If
    'End Sub
    'Private Sub BuildCCO(ByVal cCO As String)
    '    If cCO.Length > 0 Then
    '        Dim ids() As String = cCO.Split(";")
    '        For Each str As String In ids
    '            Dim user as iuser
    '            Try
    '                user = UserBusiness.GetUserById(CInt(str))
    '            Catch ex As Exception 'Si no encuentra un usuario con este  id tira error =/
    '               zamba.core.zclass.raiseerror(New Exception("No se pudo encontrar un usuario con id " & str))
    '                Exit For
    '            End Try

    '            If IsNothing(user) Then
    '               zamba.core.zclass.raiseerror(New Exception("No se pudo encontrar un usuario con id " & str))
    '            Else
    '                Msg.CCO.Add(user)
    '                Msg.CCOStr &= user.Name & " "
    '            End If
    '            user = Nothing
    '        Next
    '    End If
    'End Sub


End Class
